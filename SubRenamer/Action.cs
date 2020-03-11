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
        private readonly List<VsFileItem> VsFileList = new List<VsFileItem> { };
        private readonly List<FileInfo> VideoFileList = new List<FileInfo> { };
        private readonly List<FileInfo> SubFileList = new List<FileInfo> { };
        private readonly Dictionary<string, string> SubRenameDict = new Dictionary<string, string> { }; // 之前文件名 -> 修改后文件名
        private string CurtStatus = "";

        private static readonly List<string> VideoExts = new List<string> { ".mp4", ".mkv", ".rmvb", ".mov" };
        private static readonly List<string> SubExts = new List<string> { ".srt", ".ass", ".ssa", ".sub", ".idx" };

        private void SetCurtStatus(string status)
        {
            CurtStatus = status.Trim();
            RefreshFileListUi();
        }

        private void RefreshFileListUi() => BeginInvoke((MethodInvoker)delegate
        {
            _RefreshFileListUi();
        });

        private void _RefreshFileListUi()
        {
            foreach (VsFileItem vsFile in VsFileList)
            {
                // 查找 Ui 的 Tag 中保存的 VsFileItem
                ListViewItem findItemFromUi = null;
                foreach (ListViewItem item in FileListUi.Items)
                {
                    if (item.Tag != null && (VsFileItem)item.Tag == vsFile)
                    {
                        findItemFromUi = item;
                        break;
                    }
                }

                var itemTextArr = new string[] { vsFile.MatchKey, vsFile.VideoFile, vsFile.SubFile, vsFile.GetStatusStr() };
                if (findItemFromUi == null)
                {
                    // Ui 列表增加
                    var item = new ListViewItem(itemTextArr);
                    item.Tag = vsFile;
                    FileListUi.Items.Add(item);
                }
                else
                {
                    // Ui 列表修改
                    findItemFromUi.Text = vsFile.MatchKey;
                    findItemFromUi.SubItems.Clear();
                    findItemFromUi.SubItems.AddRange(new string[] { vsFile.VideoFile, vsFile.SubFile, vsFile.GetStatusStr() });
                }
            }

            /*foreach (var file in SubFileList)
            {
                var str = "";
                if (SubtitleEpisDict.ContainsKey(file.Name))
                    str += $"[集数:{SubtitleEpisDict[file.Name]}] ";
                str += file.Name;
                SubtitleFileListBox.Items.Add(str);
                if (SubtitleRenameDict.ContainsKey(file.Name))
                {
                    string renameStatus = SubtitleChangedStatus.ContainsKey(file.Name) ? SubtitleChangedStatus[file.Name] : "";
                    SubtitleFileListBox.Items.Add(String.Format("{0,17}", renameStatus + " => ") + SubtitleRenameDict[file.Name]);
                }
            }

            // 视频文件
            foreach (var file in VideoFileList)
            {
                var str = "      ";
                if (VideoEpisDict.ContainsKey(file.Name))
                    str += $"[集数:{VideoEpisDict[file.Name]}] ";
                str += file.Name;
                VideoFileListBox.Items.Add(str);
            }*/
        }

        protected void MatchVideoSub() => Task.Factory.StartNew(() => _MatchVideoSub());

        // 匹配 视频 & 字幕 集数位置
        private void _MatchVideoSub()
        {
            if (VideoFileList.Count < 2 || SubFileList.Count < 2) return;

            SetCurtStatus("匹配集数中...");

            int beginPos, subBeginPos;
            string endStr, subEndStr;
            try
            {
                beginPos = GetEpisodePosByList(VideoFileList); // 视频文件名集数开始位置
                subBeginPos = GetEpisodePosByList(SubFileList); // 字幕文件名集数开始位置

                endStr = GetEndStrByList(VideoFileList, beginPos);
                subEndStr = GetEndStrByList(SubFileList, subBeginPos);
            }
            catch
            {
                MessageBox.Show("无法正确匹配集数位置，需进一步手动确认", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // 集数字典更新
            var videoMatchDict = new Dictionary<string, string> { };
            var subMatchDict = new Dictionary<string, string> { };

            foreach (var file in VideoFileList)
                videoMatchDict[file.Name] = GetEpisByFileName(file.Name, beginPos: beginPos, endStr: endStr);

            foreach (var file in SubFileList)
                subMatchDict[file.Name] = GetEpisByFileName(file.Name, beginPos: subBeginPos, endStr: subEndStr);

            // VsFileItem
            foreach (var videoMatch in videoMatchDict)
            {
                // VsFileList.Find(o => o.MatchKey == videoMatch.Key)
                // TODO.....
            }

            // 刷新文件列表
            SetCurtStatus("集数已匹配");
            RefreshFileListUi();
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

            if (beginPos == -1) throw new Exception("beginPos == -1");

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
            string fileName = list.Where(o => Regex.IsMatch(o.Name.Substring(beginPos)[0].ToString(), @"^\d+$")).ToList()[0].Name; // 获取开始即是数字的文件名
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
            /*foreach (var item in SubtitleRenameDict)
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
            }
            */
        }
    }
}