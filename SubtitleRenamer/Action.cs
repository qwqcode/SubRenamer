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
using System.Windows.Forms;

namespace SubtitleRenamer
{
    public partial class MainForm
    {
        private static string OpenPath = "";
        private static List<FileInfo> VideoFileList = new List<FileInfo> { };
        private static List<FileInfo> SubtitleFileList = new List<FileInfo> { };

        private static List<string> VideoExts = new List<string> { ".mp4", ".mkv", ".rmvb", ".mov" };
        private static List<string> SubtitleExts = new List<string> { ".srt", ".ass", ".ssa", ".sub", ".idx" };

        private static Dictionary<string, string> VideoEpisDict = new Dictionary<string, string> { }; // 文件名 -> 集数
        private static Dictionary<string, string> SubtitleEpisDict = new Dictionary<string, string> { };

        private static Dictionary<string, string> SubtitleRenameDict = new Dictionary<string, string> { }; // 之前文件名 -> 修改后文件名

        private void ReloadFiles()
        {
            if (OpenPath == null || OpenPath.Trim() == "") return;

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
            VideoEpisDict.Clear();
            SubtitleEpisDict.Clear();

            // 刷新文件列表
            RefreshFileListBox();
        }

        private void RefreshFileListBox()
        {
            // 字幕 listBox
            SubtitleFileListBox.Items.Clear();
            foreach (var file in SubtitleFileList)
            {
                var str = "";
                if (SubtitleEpisDict.ContainsKey(file.Name))
                    str += $"[集数:{SubtitleEpisDict[file.Name]}] ";
                str += file.Name;
                SubtitleFileListBox.Items.Add(str);
                if (SubtitleRenameDict.ContainsKey(file.Name))
                {
                    SubtitleFileListBox.Items.Add(String.Format("{0,12}", " => ") + SubtitleRenameDict[file.Name]);
                }
            }

            // 视频 listBox
            VideoFileListBox.Items.Clear();
            foreach (var file in VideoFileList)
            {
                var str = "";
                if (VideoEpisDict.ContainsKey(file.Name))
                    str += $"[集数:{VideoEpisDict[file.Name]}] ";
                str += file.Name;
                VideoFileListBox.Items.Add(str);
            }
        }

        // 匹配 视频 & 字幕 集数位置
        private void MatchEpisode()
        {
            int beginPos = GetEpisodePosByTwoStr(VideoFileList[0].Name, VideoFileList[1].Name);  // 视频文件名集数开始位置
            int subBeginPos = GetEpisodePosByTwoStr(SubtitleFileList[0].Name, SubtitleFileList[1].Name); // 字幕文件名集数开始位置

            if (beginPos <= -1 || subBeginPos <= -1)
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

            return beginPos;
        }

        /// 获取集数
        private string GetEpisByFileName(string fileName, int beginPos)
        {
            if (beginPos <= -1)
            {
                return null;
            }

            var str = fileName.Substring(beginPos);
            return Regex.Matches(str, @"(\d+)")[0].Value; // 为了获得完整的数字，无论多少位
        }

        /// 执行改名操作
        private void StartRename()
        {
            MessageBox.Show("修改文件名代码还没写... 睡觉了...");
        }
    }
}
