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
        private List<VsFileItem> VsFileList = new List<VsFileItem> { };
        private List<FileInfo> VideoFileList = new List<FileInfo> { };
        private List<FileInfo> SubFileList = new List<FileInfo> { };
        private readonly Dictionary<string, string> SubRenameDict = new Dictionary<string, string> { }; // 之前文件名 -> 修改后文件名
        private string CurtStatus = "";

        private static readonly List<string> VideoExts = new List<string> { ".mp4", ".mkv", ".rmvb", ".mov" };
        private static readonly List<string> SubExts = new List<string> { ".srt", ".ass", ".ssa", ".sub", ".idx" };

        private void SetCurtStatus(string status)
        {
            CurtStatus = status.Trim();
        }

        public void RefreshFileListUi(bool showPreview = false) => BeginInvoke((MethodInvoker)delegate
        {
            _RefreshFileListUi(showPreview);
        });

        private void _RefreshFileListUi(bool showPreview)
        {
            FileListUi.Items.Clear();
            var showFullName = MainSettings.Default.ListShowFileFullName;
            var subRenameDict = GetSubRenameDict(); // 重命名字幕文件路径词典
            foreach (VsFileItem vsFile in VsFileList)
            {
                var subFileNameShow = vsFile.SubFile != null ? (showFullName ? vsFile.SubFile.FullName : vsFile.SubFile.Name) : "";
                
                // 显示预览内容
                if (showPreview == true)
                {
                    if (vsFile.SubFile != null && subRenameDict.ContainsKey(vsFile.SubFile.FullName))
                        subFileNameShow = showFullName ? subRenameDict[vsFile.SubFile.FullName] : Path.GetFileName(subRenameDict[vsFile.SubFile.FullName]);
                    else
                        subFileNameShow = "(不修改)";
                }

                var item = new ListViewItem(new string[]
                {
                    vsFile.MatchKey ?? "",
                    vsFile.VideoFile != null ? (showFullName ? vsFile.VideoFile.FullName : vsFile.VideoFile.Name) : "",
                    subFileNameShow,
                    vsFile.GetStatusStr()
                });
                item.Tag = vsFile;
                FileListUi.Items.Add(item);
            }
        }

        protected void MatchVideoSub() => Task.Factory.StartNew(() => _MatchVideoSub());

        // 匹配 视频 & 字幕 集数位置
        private void _MatchVideoSub()
        {
            SetCurtStatus("匹配集数中...");

            // 文件去重
            VideoFileList = VideoFileList.Distinct().ToList();
            SubFileList = SubFileList.Distinct().ToList();

            // VsFileItem
            TryUpdateVsFileListOnce(VideoFileList);
            TryUpdateVsFileListOnce(SubFileList);


            // 刷新文件列表
            SetCurtStatus("集数已匹配");
            RefreshFileListUi();
        }

        private void TryUpdateVsFileListOnce(List<FileInfo> FileList)
        {
            if (FileList != VideoFileList && FileList != SubFileList)
                throw new Exception("Not allow param value.");

            var beginPos = GetEpisodePosByList(FileList); // 视频文件名集数开始位置
            var endStr = GetEndStrByList(FileList, beginPos);

            VsFileList.RemoveAll(o =>
            {
                bool condition2 = false;
                if (FileList == VideoFileList)
                    condition2 = o.VideoFile != null;
                else if (FileList == SubFileList)
                    condition2 = o.SubFile != null;
                return o.MatchKey == null && condition2;
            });

            foreach (var file in FileList)
            {
                string matchKey = null;
                if (beginPos > -1 && endStr != null)
                    matchKey = GetEpisByFileName(file.Name, beginPos, endStr); // 匹配字符

                var vsItem = (matchKey != null) ? VsFileList.Find(o => o.MatchKey == matchKey) : null;
                if (vsItem == null)
                {
                    var status = VsFileStatus.Unmatched;
                    if (matchKey != null)
                    {
                        if (FileList == VideoFileList) status = VsFileStatus.SubLack;
                        else if (FileList == SubFileList) status = VsFileStatus.VideoLack;
                    }

                    // 创建新项目
                    vsItem = new VsFileItem()
                    {
                        MatchKey = matchKey ?? null,
                        VideoFile = (FileList == VideoFileList) ? file : null,
                        SubFile = (FileList == SubFileList) ? file : null,
                        Status = status
                    };

                    VsFileList.Add(vsItem);
                }
                else
                {
                    if (FileList == VideoFileList)
                    {
                        vsItem.VideoFile = file;
                        vsItem.Status = VsFileStatus.SubLack;
                    }
                    else if (FileList == SubFileList)
                    {
                        vsItem.SubFile = file;
                        vsItem.Status = VsFileStatus.VideoLack;
                    }

                    if (vsItem.VideoFile != null && vsItem.SubFile != null)
                        vsItem.Status = VsFileStatus.Ready;
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
            var grp = Regex.Matches(str, @"(\d+)");
            if (grp.Count <= 0 || grp[0].Index != 0)
            {
                var result = "";
                // 通过 endStr 获得集数
                for (int i = 0; i < str.Length; i++)
                {
                    if (str[i].ToString() == endStr) break;
                    result += str[i];
                }
                return result;

            }
            return grp[0].Value; // 为了获得完整的数字，无论多少位
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

        protected void StartRename() => Task.Factory.StartNew(() => _StartRename());

        /// 执行改名操作
        private void _StartRename()
        {
            TaskDialog td = new TaskDialog();
            td.Caption = "一键改名操作";
            td.Text = "执行一键改名操作中，请稍后...";

            TaskDialogProgressBar tdp = new TaskDialogProgressBar(0, 100, 34);
            td.ProgressBar = tdp;

            td.StandardButtons = TaskDialogStandardButtons.Cancel;
            td.DetailsExpandedLabel = "查看详情";
            td.DetailsExpandedText = "23333333 - View informatio";

            td.Show();

            foreach (var item in VsFileList)
            {

            }

            /*foreach (var item in VsFileList)
            {
                string before = item.Key;
                string after = item.Value;
                string beforeFullPath = Path.Combine(OpenPath, before);
                string afterFullPath = Path.Combine(OpenPath, after);

                SubtitleChangedStatus[before] = "更名中";
                RefreshFileListBox();

                // 无需更名
                if (before.Equals(after) || SubtitleChangedStatus[before] == "✔")
                {
                    SubtitleChangedStatus[before] = "✔";
                    continue;
                }

                // 原文件是否存在
                if (!File.Exists(beforeFullPath))
                {
                    SubtitleChangedStatus[before] = "✖";
                    continue;
                }

                // 文件备份功能
                if (MainSettings.Default.RawSubtitleBuckup)
                {
                    string buckupPath = Path.Combine(OpenPath, "SubtitleFileBackup");
                    try
                    {
                        if (!Directory.Exists(buckupPath))
                            Directory.CreateDirectory(buckupPath);
                        File.Copy(beforeFullPath, Path.Combine(buckupPath, before), true);
                    }
                    catch (Exception e)
                    {
                        BeginInvoke((MethodInvoker)delegate
                        {
                            MessageBox.Show("原始字幕备份失败\n\n" + e.Message, "字幕备份失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        });
                        SubtitleChangedStatus[before] = "✖";
                        continue;
                    }
                }

                // 执行更名
                try
                {
                    File.Move(beforeFullPath, afterFullPath);
                    SubtitleChangedStatus[before] = "✔";
                }
                catch
                {
                    // 更名失败
                    SubtitleChangedStatus[before] = "✖";
                }

                RefreshFileListBox();
            }

            SetCurtStatus("更名完毕");
            RefreshFileListBox();

            if (MainSettings.Default.OpenFolderFinished)
            {
                Process.Start(OpenPath);
            }*/
        }

        // 获取修改的字幕文件名 (原始完整路径->修改后完整路径)
        private Dictionary<string, string> GetSubRenameDict()
        {
            var dict = new Dictionary<string, string>() { };
            foreach (var item in VsFileList)
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