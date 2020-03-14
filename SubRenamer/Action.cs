using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SubRenamer
{
    public partial class MainForm
    {
        private List<VsItem> VsList = new List<VsItem> { };
        private List<FileInfo> VideoFileList = new List<FileInfo> { };
        private List<FileInfo> SubFileList = new List<FileInfo> { };
        private readonly Dictionary<string, string> SubRenameDict = new Dictionary<string, string> { }; // 之前文件名 -> 修改后文件名
        private string CurtStatus = "";

        public static readonly List<string> VideoExts = new List<string> { ".mp4", ".mkv", ".rmvb", ".mov" };
        public static readonly List<string> SubExts = new List<string> { ".srt", ".ass", ".ssa", ".sub", ".idx" };

        private void SetCurtStatus(string status)
        {
            CurtStatus = status.Trim();
        }

        private string[] GetItemValues(VsItem vsItem)
        {
            var showFullName = MainSettings.Default.ListShowFileFullName;
            var subRenameDict = GetSubRenameDict(); // 重命名字幕文件路径词典
            var videoText = vsItem.VideoFile != null ? (showFullName ? vsItem.VideoFile.FullName : vsItem.VideoFile.Name) : "";
            var subText = vsItem.SubFile != null ? (showFullName ? vsItem.SubFile.FullName : vsItem.SubFile.Name) : "";

            // 显示预览内容
            if (PreviewCheckBox.Checked)
            {
                videoText = subText;

                if (vsItem.SubFile != null && subRenameDict.ContainsKey(vsItem.SubFile.FullName))
                    subText = showFullName ? subRenameDict[vsItem.SubFile.FullName] : Path.GetFileName(subRenameDict[vsItem.SubFile.FullName]);
                else
                    subText = "(不修改)";
            }

            return new string[]
            {
                vsItem.MatchKey ?? "",
                videoText,
                subText,
                vsItem.GetStatusStr()
            };
        }

        public void RefreshFileListUi() => BeginInvoke((MethodInvoker)delegate
        {
            _RefreshFileListUi();
        });

        private void _RefreshFileListUi()
        {
            // 删除无效项
            foreach (ListViewItem item in FileListUi.Items)
            {
                if (item.Tag == null || !VsList.Contains(item.Tag))
                    item.Remove();
            }

            foreach (VsItem vsItem in VsList)
            {
                var itemValues = GetItemValues(vsItem);
                var findItem = FileListUi.Items.Cast<ListViewItem>().ToList().Find(o => o.Tag != null && o.Tag == vsItem);
                if (findItem == null)
                {
                    var item = new ListViewItem(itemValues);
                    item.Tag = vsItem;
                    FileListUi.Items.Add(item);
                }
                else
                {
                    UpdateFileListUiItem(findItem);
                }
            }
        }

        private void UpdateFileListUiItem (ListViewItem item)
        {
            if (item.Tag == null) return;
            var itemValues = GetItemValues((VsItem)item.Tag);
            if (item.SubItems[0].Text != itemValues[0])
                item.SubItems[0].Text = itemValues[0];
            for (int i = 1; i < itemValues.ToArray().Length; i++)
            {
                if (item.SubItems[i].Text != itemValues[i])
                    item.SubItems[i].Text = itemValues[i];
            }
        }

        // protected void MatchVideoSub() => Task.Factory.StartNew(() => _MatchVideoSub());

        // 匹配 视频 & 字幕 集数位置
        private void MatchVideoSub()
        {
            SetCurtStatus("匹配集数中...");

            // 文件去重
            VideoFileList = VideoFileList.Distinct().ToList();
            SubFileList = SubFileList.Distinct().ToList();

            // VsItem
            TryUpdateVsListOnce(VideoFileList);
            TryUpdateVsListOnce(SubFileList);


            // 刷新文件列表
            SetCurtStatus("集数已匹配");
            RefreshFileListUi();
        }

        private void TryUpdateVsListOnce(List<FileInfo> FileList)
        {
            if (FileList != VideoFileList && FileList != SubFileList)
                throw new Exception("Not allow param value.");

            var beginPos = GetEpisodePosByList(FileList); // 视频文件名集数开始位置
            var endStr = GetEndStrByList(FileList, beginPos);

            foreach (var file in FileList)
            {
                string matchKey = null;
                if (beginPos > -1 && endStr != null)
                    matchKey = GetEpisByFileName(file.Name, beginPos, endStr); // 匹配字符

                var matchedVsItem = (matchKey != null) ? VsList.Find(o => o.MatchKey == matchKey) : null;
                var vsItemByFileName = VsList.Find(o =>
                {
                    if (FileList == VideoFileList) return o.VideoFile != null && o.VideoFile.FullName == file.FullName;
                    if (FileList == SubFileList) return o.SubFile != null && o.SubFile.FullName == file.FullName;
                    return false;
                }); // 通过文件名查找到的现成的 VsItem

                // 实例化新对象
                if (matchedVsItem == null && vsItemByFileName == null)
                {
                    var status = VsStatus.Unmatched;
                    if (matchKey != null)
                    {
                        if (FileList == VideoFileList) status = VsStatus.SubLack;
                        if (FileList == SubFileList) status = VsStatus.VideoLack;
                    }

                    // 创建新项目
                    var vsItem = new VsItem()
                    {
                        MatchKey = matchKey ?? null,
                        VideoFile = (FileList == VideoFileList) ? file : null,
                        SubFile = (FileList == SubFileList) ? file : null,
                        Status = status
                    };

                    VsList.Add(vsItem);
                }

                // 仅更新数据
                if (matchedVsItem != null)
                {
                    if (FileList == VideoFileList)
                    {
                        matchedVsItem.VideoFile = file;
                        matchedVsItem.Status = VsStatus.SubLack;
                    }
                    else if (FileList == SubFileList)
                    {
                        matchedVsItem.SubFile = file;
                        matchedVsItem.Status = VsStatus.VideoLack;
                    }

                    if (matchedVsItem.VideoFile != null && matchedVsItem.SubFile != null)
                        matchedVsItem.Status = VsStatus.Ready;
                }
            }
        }

        // 遍历所有 list 中的项目，尝试得到集数开始位置
        private int GetEpisodePosByList(List<FileInfo> list)
        {
            int aIndex = 0;
            int bIndex = 1;
            int beginPos = -1;

            while (true)
            {
                try
                {
                    int result = GetEpisodePosByTwoStr(list[aIndex].Name, list[bIndex].Name);
                    beginPos = result;
                    break;
                }
                catch
                {
                    aIndex++;
                    bIndex++;
                    if (aIndex >= list.Count || bIndex >= list.Count) break;
                }
            }

            return beginPos;
        }

        // 通过比对两个文件名中 数字 不同的部分来得到 集数 的位置
        private int GetEpisodePosByTwoStr(string strA, string strB)
        {
            var numGrpA = Regex.Matches(strA.ToString(), @"(\d+)");
            var numGrpB = Regex.Matches(strB.ToString(), @"(\d+)");
            int beginPos = -1;

            for (int i = 0; i < numGrpA.Count; i++)
            {
                var A = numGrpA[i];
                var B = numGrpB[i];
                if (A.Value != B.Value && A.Index == B.Index)
                {
                    // 若两个 val 不同，则记录位置
                    beginPos = numGrpA[i].Index;
                    break;
                }
            }

            if (beginPos == -1) throw new Exception("beginPos == -1");

            return beginPos;
        }

        // 获取集数
        private string GetEpisByFileName(string fileName, int beginPos, string endStr)
        {
            if (beginPos <= -1) return null;
            var str = fileName.Substring(beginPos);

            var result = "";
            // 通过 endStr 获得集数
            for (int i = 0; i < str.Length; i++)
            {
                if (str[i].ToString() == endStr) break;
                result += str[i];
            }
            return result;
        }

        // 获取终止字符
        private string GetEndStrByList(List<FileInfo> list, int beginPos)
        {
            if (list.Count() < 2) return null;
            if (beginPos < 0) return null;

            string fileName = list.Where(o => {
                if (o.Name == null || o.Name.Length <= beginPos) return false;
                return Regex.IsMatch(o.Name.Substring(beginPos)[0].ToString(), @"^\d+$");
            }).ToList()[0].Name; // 获取开始即是数字的文件名
            var grp = Regex.Matches(fileName, @"(\d+)");
            if (grp.Count <= 0) return null;
            Match firstNum = grp[0];
            int afterNumStrIndex = firstNum.Index + firstNum.Length; // 数字后面的第一个字符 index

            // 不把特定字符（空格等）作为结束字符
            string strTmp = fileName.Substring(afterNumStrIndex);
            string result = null;
            for (int i = 0; i < strTmp.Length; i++)
            {
                if (strTmp[i].ToString() != " ")
                {
                    result = strTmp[i].ToString();
                    break;
                }
            }
            return result;
        }

        protected void StartRename()
        {
            if (GetSubRenameDict().Count() <= 0) return;
            Task.Factory.StartNew(() => _StartRename());
        }

        /// 执行改名操作
        private void _StartRename()
        {
            var subRenameDict = GetSubRenameDict();
            int renTotal = subRenameDict.Count();
            int errTotal = 0;
            void SetCurrentProgress(KeyValuePair<string, string> subRename, int index)
            {
                if (InvokeRequired)
                {
                    Invoke((MethodInvoker)delegate { SetCurrentProgress(subRename, index); });
                    return;
                }

                double percentage = (index / (double)renTotal) * 100;
                //td.Text = $"执行一键改名操作中，请稍后... ({index}/{renTotal})" + Environment.NewLine;
                //td.Text += $"\"{Path.GetFileName(subRename.Key)}\"{Environment.NewLine}=> \"{Path.GetFileName(subRename.Value)}\"";
                // td.ProgressBar.Value = int.Parse(Math.Truncate(percentage).ToString());
            }

            int i = 0;
            foreach (var subRename in subRenameDict)
            {
                try
                {
                    _RenameOnce(subRename);
                    // td.DetailsExpandedText += $"[成功] [\"{subRename.Key}\"=>\"{subRename.Value}\"]{Environment.NewLine}";
                }
                catch (Exception e)
                {
                    // td.DetailsExpandedText += $"[失败] [\"{subRename.Key}\"=>\"{subRename.Value}\"] {e.Message}{Environment.NewLine}";
                    errTotal++;
                }
                finally
                {
                    i++;
                    SetCurrentProgress(subRename, i);
                }
            }

            RefreshFileListUi();
        }

        private void _RenameOnce(KeyValuePair<string, string> subRename)
        {
            var vsFile = VsList.Find(o => o.SubFile.FullName == subRename.Key);
            if (vsFile == null) throw new Exception("找不到修改项");
            if (vsFile.Status != VsStatus.Ready) throw new Exception("当然状态无法修改");
            if (vsFile.VideoFile == null || vsFile.SubFile == null) throw new Exception("字幕/视频文件不完整");

            var before = new FileInfo(subRename.Key);
            var after = new FileInfo(subRename.Value);

            // 若无需修改
            if (before.FullName.Equals(after.FullName))
            {
                vsFile.Status = VsStatus.Done;
                throw new Exception("字幕文件名无需修改");
            }

            // 若原文件不存在
            if (!before.Exists)
            {
                vsFile.Status = VsStatus.Fatal;
                throw new Exception($"字幕源文件不存在");
            }

            // 执行更名
            try
            {
                File.Move(before.FullName, after.FullName);
                vsFile.Status = VsStatus.Done;
            }
            catch (Exception e)
            {
                // 更名失败
                vsFile.Status = VsStatus.Fatal;
                throw new Exception($"改名发生错误 {e.GetType().FullName} {e.ToString()}");
            }
        }

        // 获取修改的字幕文件名 (原始完整路径->修改后完整路径)
        private Dictionary<string, string> GetSubRenameDict()
        {
            var dict = new Dictionary<string, string>() { };
            if (VideoFileList.Count <= 0 || SubFileList.Count <= 0 || VsList.Count <= 0)
                return dict;

            foreach (var item in VsList)
            {
                if (item.VideoFile == null || item.SubFile == null) continue;
                string videoName = Path.GetFileNameWithoutExtension(item.VideoFile.Name); // 去掉后缀的视频文件名
                string subAfterFilename = videoName + item.SubFile.Extension; // 修改的字幕文件名
                dict.Add(item.SubFile.FullName, Path.Combine(item.SubFile.DirectoryName, subAfterFilename));
            }

            return dict;
        }
    }
}