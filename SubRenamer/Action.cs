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
using static SubRenamer.Global;

namespace SubRenamer
{
    public partial class MainForm
    {
        private List<VsItem> VsList = new List<VsItem> { };
        public MatchMode CurtMatchMode = MatchMode.Auto;

        public static readonly List<string> VideoExts = new List<string> { ".mkv", ".mp4", "flv", ".avi", ".mov", ".rmvb", ".wmv", ".mpg", ".avs" };
        public static readonly List<string> SubExts = new List<string> { ".srt", ".ass", ".ssa", ".sub", ".idx" };

        // 匹配模式
        public enum MatchMode
        {
            Auto,
            Manu,
            Regex
        }

        private string[] GetItemValues(VsItem vsItem)
        {
            var showFullName = AppSettings.ListShowFileFullName;
            var subRenameDict = GetSubRenameDict(); // 重命名字幕文件路径词典
            var videoText = vsItem.Video != null ? (showFullName ? vsItem.Video : Path.GetFileName(vsItem.Video)) : "";
            var subText = vsItem.Sub != null ? (showFullName ? vsItem.Sub : Path.GetFileName(vsItem.Sub)) : "";

            // 显示预览内容
            if (PreviewCheckBox.Checked)
            {
                videoText = subText;

                if (vsItem.Sub != null && subRenameDict.ContainsKey(vsItem.Sub))
                    subText = showFullName ? subRenameDict[vsItem.Sub] : Path.GetFileName(subRenameDict[vsItem.Sub]);
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

        public void RefreshFileListUi(bool removeNull = true) => BeginInvoke((MethodInvoker)delegate
        {
            _RefreshFileListUi(removeNull);
        });

        private void _RefreshFileListUi(bool removeNull = true)
        {
            // 删除无效项
            if (removeNull) VsList.RemoveAll(o => o.IsEmpty);
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

        private List<FileInfo> GetFileListByVsList(AppFileType FileType)
        {
            if (FileType == AppFileType.Video)
                return new List<FileInfo>(VsList.Where(o => o.Video != null).Select(o => o.VideoFileInfo));
            if (FileType == AppFileType.Sub)
                return new List<FileInfo>(VsList.Where(o => o.Sub != null).Select(o => o.SubFileInfo));
            return null;
        }

        // 匹配 视频 & 字幕 集数位置
        public void MatchVideoSub()
        {
            if (VsList.Count <= 0) return;

            // VsItem
            TryHandleVsListMatch(AppFileType.Video);
            TryHandleVsListMatch(AppFileType.Sub);

            // 刷新文件列表
            RefreshFileListUi();
        }

        // 自动匹配配置
        private int M_Auto_Begin = int.MinValue;
        private string M_Auto_End = null;
        
        // 手动匹配配置
        public string M_Manu_V_Begin = null;
        public string M_Manu_V_End = null;
        public string M_Manu_S_Begin = null;
        public string M_Manu_S_End = null;

        // 正则表达式匹配配置
        public Regex M_Regx_V = null;
        public Regex M_Regx_S = null;

        private void TryHandleVsListMatch(AppFileType FileType)
        {
            // 自动匹配数据归零
            M_Auto_Begin = int.MinValue;
            M_Auto_End = null;

            var FileList = GetFileListByVsList(FileType);
            foreach (var file in FileList)
            {
                string matchKey = GetMatchKeyByFileName(file.Name, FileType, FileList);

                var findVsItem = (matchKey != null) ? VsList.Find(o => o.MatchKey == matchKey) : null; // By matchKey
                
                if (findVsItem == null)
                    findVsItem = VsList.Find(o =>
                    {
                        if (FileType == AppFileType.Video) return o.Video == file.FullName;
                        if (FileType == AppFileType.Sub) return o.Sub == file.FullName;
                        return false;
                     }); // 通过文件名查找到的现成的 VsItem

                // 仅更新数据
                if (findVsItem != null)
                {
                    if (FileType == AppFileType.Video)
                    {
                        findVsItem.Video = file.FullName;
                        findVsItem.Status = VsStatus.SubLack;
                        VsList.RemoveAll(o => o != findVsItem && o.Video == findVsItem.Video); // 删除其他同类项目
                    }
                    else if (FileType == AppFileType.Sub)
                    {
                        findVsItem.Sub = file.FullName;
                        findVsItem.Status = VsStatus.VideoLack;
                        VsList.RemoveAll(o => o != findVsItem && o.Sub == findVsItem.Sub); // 删除其他同类项目
                    }

                    if (findVsItem.Video != null && findVsItem.Sub != null)
                        findVsItem.Status = VsStatus.Ready;

                    findVsItem.MatchKey = matchKey;
                    if (string.IsNullOrWhiteSpace(findVsItem.MatchKey))
                        findVsItem.Status = VsStatus.Unmatched;
                }
            }
        }

        // 获取匹配字符
        private string GetMatchKeyByFileName(string fileName, AppFileType fileType, List<FileInfo> FileList)
        {
            string matchKey = null;
            if (CurtMatchMode == MatchMode.Auto)
            {
                if (M_Auto_Begin == int.MinValue) M_Auto_Begin = GetEpisodePosByList(FileList); // 视频文件名集数开始位置
                if (M_Auto_End == null) M_Auto_End = GetEndStrByList(FileList, M_Auto_Begin);
                if (M_Auto_Begin > -1 && M_Auto_End != null)
                    matchKey = GetEpisByFileName(fileName, M_Auto_Begin, M_Auto_End); // 匹配字符
            }
            else if (CurtMatchMode == MatchMode.Manu)
            {
                if (fileType == AppFileType.Video)
                    matchKey = GetMatchKeyByBeginEndStr(fileName, M_Manu_V_Begin, M_Manu_V_End);
                else if (fileType == AppFileType.Sub)
                    matchKey = GetMatchKeyByBeginEndStr(fileName, M_Manu_S_Begin, M_Manu_S_End);
            }
            else if (CurtMatchMode == MatchMode.Regex)
            {
                if (fileType == AppFileType.Video && M_Regx_V != null)
                    matchKey = M_Regx_V.Match(fileName).Groups[1].Value;
                else if (fileType == AppFileType.Sub && M_Regx_S != null)
                    matchKey = M_Regx_S.Match(fileName).Groups[1].Value;
            }

            if (string.IsNullOrWhiteSpace(matchKey)) matchKey = null;
            return matchKey;
        }

        public static string GetMatchKeyByBeginEndStr(string fileName, string start, string end)
        {
            if (string.IsNullOrWhiteSpace(fileName)
                || string.IsNullOrWhiteSpace(start)
                || string.IsNullOrWhiteSpace(end))
                return null;

            fileName = fileName.Trim();

            start = EscapeRegExp(start).Replace(@"\*", ".*");
            end = EscapeRegExp(end).Replace(@"\*", ".*");

            var calcPattern = $@"^{start}0*(.+?){end}$";
            var str = Regex.Match(fileName, calcPattern).Groups[1].Value;

            return str;
        }

        static string EscapeRegExp(string str)
        {
            return Regex.Replace(str, @"[.*+?^${}()|[\]\\]", @"\$&");
        }

        #region 自动匹配模式

        // 获取集数
        private string GetEpisByFileName(string fileName, int beginPos, string endStr)
        {
            if (string.IsNullOrWhiteSpace(fileName)) return null;
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

        // 获取终止字符
        private string GetEndStrByList(List<FileInfo> list, int beginPos)
        {
            if (list.Count() < 2) return null;
            if (beginPos <= -1) return null;

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
        #endregion

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
            var vsFile = VsList.Find(o => o.Sub == subRename.Key);
            if (vsFile == null) throw new Exception("找不到修改项");
            if (vsFile.Status != VsStatus.Ready) throw new Exception("当然状态无法修改");
            if (vsFile.Video == null || vsFile.Sub == null) throw new Exception("字幕/视频文件不完整");

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
            if (VsList.Count <= 0)
                return dict;

            foreach (var item in VsList)
            {
                if (item.Video == null || item.Sub == null) continue;
                string videoName = Path.GetFileNameWithoutExtension(item.VideoFileInfo.Name); // 去掉后缀的视频文件名
                string subAfterFilename = videoName + item.SubFileInfo.Extension; // 修改的字幕文件名
                dict[item.SubFileInfo.FullName] = Path.Combine(item.SubFileInfo.DirectoryName, subAfterFilename);
            }

            return dict;
        }
    }
}