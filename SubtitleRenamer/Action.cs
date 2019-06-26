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

namespace SubtitleRenamer
{
    public partial class MainForm
    {
        private static string OpenPath = "";
        private static bool isFilesLoading = false;
        private static List<FileInfo> VideoFileList = new List<FileInfo> { };
        private static List<FileInfo> SubtitleFileList = new List<FileInfo> { };

        private static List<string> VideoExts = new List<string> { ".mp4", ".mkv", ".rmvb", ".mov" };
        private static List<string> SubtitleExts = new List<string> { ".srt", ".ass", ".ssa", ".sub", ".idx" };

        private static Dictionary<string, string> VideoEpisDict = new Dictionary<string, string> { }; // 文件名 -> 集数
        private static Dictionary<string, string> SubtitleEpisDict = new Dictionary<string, string> { };

        private static Dictionary<string, string> SubtitleRenameDict = new Dictionary<string, string> { }; // 之前文件名 -> 修改后文件名
        private static Dictionary<string, string> SubtitleChangedStatus = new Dictionary<string, string> { }; // 已经修改了文件名的字幕（原始文件名）

        private static string CurtStatus = "";
        private void SetCurtStatus(string status)
        {
            CurtStatus = status.Trim();
            RefreshFileListBox();
        }

        private static FileSystemWatcher OpenPathWatcher = null;

        // 实时检测 OpenPath 文件变化，并重载文件列表
        private void OpenPathWatcherRestart()
        {
            if (OpenPathWatcher == null)
            {
                OpenPathWatcher = new FileSystemWatcher();
                OpenPathWatcher.Created += OpenPathFileChanged;
                OpenPathWatcher.Deleted += OpenPathFileChanged;
                OpenPathWatcher.Renamed += OpenPathFileChanged;
            }

            OpenPathWatcher.Path = OpenPath;
            OpenPathWatcher.EnableRaisingEvents = false;
        }

        private void OpenPathFileChanged(object sender, FileSystemEventArgs e)
        {
            ReloadFiles();
        }

        // 重新加载 OpenPath 中的文件
        protected void ReloadFiles() => Task.Factory.StartNew(() => _ReloadFiles());

        private void _ReloadFiles()
        {
            if (OpenPath == null || OpenPath.Trim() == "") return;
            // 显示加载中...
            isFilesLoading = true;
            CurtStatus = "";
            RefreshFileListBox();
            OpenPathWatcherRestart();

            var folder = new DirectoryInfo(OpenPath);
            var files = folder.GetFiles("*");

            // 视频文件
            VideoFileList.Clear();
            foreach (var file in files.Where(s => VideoExts.Contains(s.Extension.ToString().ToLower())))
                VideoFileList.Add(file);

            // 字幕文件
            SubtitleFileList.Clear();
            foreach (var file in files.Where(s => SubtitleExts.Contains(s.Extension.ToString().ToLower())))
                SubtitleFileList.Add(file);

            // 清空历史数据
            SubtitleRenameDict.Clear();
            SubtitleChangedStatus.Clear();
            VideoEpisDict.Clear();
            SubtitleEpisDict.Clear();

            // 刷新文件列表
            isFilesLoading = false;
            RefreshFileListBox();

            // 处理文件
            MatchEpisode();

            // 启动 OpenPath 实时监测，刷新列表
            OpenPathWatcher.EnableRaisingEvents = true;
        }

        private void RefreshFileListBox() => BeginInvoke((MethodInvoker)delegate
        {
            _RefreshFileListBox();
        });

        private void _RefreshFileListBox()
        {
            SubtitleFileListBox.Items.Clear();
            VideoFileListBox.Items.Clear();

            // Head
            SubtitleFileListBox.Items.Add($" ◈  字幕文件 - {SubtitleFileList.Count} 个项目 {(!CurtStatus.Equals("") ? $"[{CurtStatus}]" : "")}");
            VideoFileListBox.Items.Add($" ◈  视频文件 - {VideoFileList.Count} 个项目");

            // Loading
            if (isFilesLoading)
            {
                SubtitleFileListBox.Items.Add("      加载中...");
                VideoFileListBox.Items.Add("      加载中...");
                return;
            }

            // 字幕文件
            foreach (var file in SubtitleFileList)
            {
                var str = "      ";
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
            }
        }

        protected void MatchEpisode() => Task.Factory.StartNew(() => _MatchEpisode());

        // 匹配 视频 & 字幕 集数位置
        private void _MatchEpisode()
        {
            if (VideoFileList.Count < 2 || SubtitleFileList.Count < 2) return;

            SetCurtStatus("匹配集数中...");

            int beginPos;
            int subBeginPos;
            try
            {
                beginPos = GetEpisodePosByTwoStr(VideoFileList[0].Name, VideoFileList[1].Name);  // 视频文件名集数开始位置
                subBeginPos = GetEpisodePosByTwoStr(SubtitleFileList[0].Name, SubtitleFileList[1].Name); // 字幕文件名集数开始位置
            }
            catch
            {
                MessageBox.Show("无法正确匹配集数位置，需进一步手动确认", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // 集数字典更新
            VideoEpisDict.Clear();
            foreach (var file in VideoFileList)
                VideoEpisDict[file.Name] = GetEpisByFileName(file.Name, beginPos: beginPos);

            SubtitleEpisDict.Clear();
            foreach (var file in SubtitleFileList)
                SubtitleEpisDict[file.Name] = GetEpisByFileName(file.Name, beginPos: subBeginPos);

            // 生成改名字典
            SubtitleRenameDict.Clear();
            foreach (var item in SubtitleEpisDict)
            {
                var subtitleFileName = item.Key;
                var subtitleFileExt = Path.GetExtension(subtitleFileName);
                var subtitleEpis = item.Value;
                // 查询对应集数的视频文件名
                var videoItems = VideoEpisDict.Where(s => s.Value == subtitleEpis); // TODO: 首字符多0少0需处理一下
                if (videoItems.Count() > 0)
                {
                    var videoFileName = videoItems.First().Key.ToString();
                    var afterSubtitleFileName = videoFileName.Remove(videoFileName.LastIndexOf(".")) + subtitleFileExt;
                    SubtitleRenameDict[subtitleFileName] = afterSubtitleFileName;
                }
            }

            // 刷新文件列表
            SetCurtStatus("集数已匹配");
            RefreshFileListBox();
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

            if (beginPos == -1)
            {
                throw new Exception("beginPos == -1");
            }

            return beginPos;
        }

        /// 获取集数
        private string GetEpisByFileName(string fileName, int beginPos)
        {
            if (beginPos <= -1)
                return null;

            var str = fileName.Substring(beginPos);
            var grp = Regex.Matches(str, @"(\d+)");
            return (grp.Count >= 1) ? grp[0].Value : null; // 为了获得完整的数字，无论多少位
        }

        protected void StartRename() => Task.Factory.StartNew(() => _StartRename());

        /// 执行改名操作
        private void _StartRename()
        {
            OpenPathWatcher.EnableRaisingEvents = false; // 暂时关闭目录变化列表实时更新
            SubtitleChangedStatus.Clear();

            foreach (var item in SubtitleRenameDict)
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
            OpenPathWatcher.EnableRaisingEvents = true;

            if (MainSettings.Default.OpenFolderFinished)
            {
                Process.Start(OpenPath);
            }
        }
    }
}
