using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using Avalonia.Controls;
using SubRenamer.Helper;
using SubRenamer.Model;
using SubRenamer.ViewModels;
using System.Text.RegularExpressions;
using MsBox.Avalonia.Base;
using SubRenamer.Model;

namespace SubRenamer.Services;

public class RenameService(Window target) : IRenameService
{
    private readonly Window _target = target;

    //原始版本
    //public void UpdateRenameTaskList(IEnumerable<MatchItem> matchList, Collection<RenameTask> destList)
    //{
    //    destList.Clear();

    //    foreach (var item in matchList)
    //    {
    //        if (string.IsNullOrEmpty(item.Subtitle) || string.IsNullOrEmpty(item.Video)) continue;

    //        var alter = "/" + Path.GetFileNameWithoutExtension(item.Video) +
    //                    Path.GetExtension(item.Subtitle);

    //        if(Config.Get().RenameStrategy == Common.RenameStrategy.Copy)
    //        {
    //            alter = Path.GetDirectoryName(item.Video) + alter;
    //        } else
    //        {
    //            alter = Path.GetDirectoryName(item.Subtitle) + alter;
    //        }

    //        destList.Add(new RenameTask(item.Subtitle, alter, item.Status == "已修改" ? "已修改" : "待修改")
    //        {
    //            MatchItem = item
    //        });
    //    }
    //}

    public void UpdateRenameTaskList(IEnumerable<MatchItem> matchList, Collection<RenameTask> destList)
    {
        destList.Clear();

        foreach (var item in matchList)
        {
            if (string.IsNullOrEmpty(item.Subtitle) || string.IsNullOrEmpty(item.Video)) continue;

            // 提取字幕文件的基本名称和扩展名
            var subtitleNameWithoutExtension = Path.GetFileNameWithoutExtension(item.Subtitle);
            var subtitleExtension = Path.GetExtension(item.Subtitle);

            // 默认语言后缀为空
            var languageSuffix = "";

            // 使用正则表达式提取最后一个点之间的部分作为语言后缀
            string pattern = @"(?<=\.)[^.]+$";  // 匹配最后一个点后的部分
            var match = Regex.Match(subtitleNameWithoutExtension, pattern);

            if (match.Success)
            {
                languageSuffix = match.Value;
                subtitleNameWithoutExtension = subtitleNameWithoutExtension.Substring(0, subtitleNameWithoutExtension.Length - (languageSuffix.Length + 1)); // 去掉语言后缀
            }

            SettingsViewModel settingsViewModel = new SettingsViewModel();
            bool isFormatSaveModeEnabled = settingsViewModel.FormatSaveMode;

            if (isFormatSaveModeEnabled){
            }
            else {
                languageSuffix = "";
            }

            // 重新构造字幕文件名（保留语言后缀）
            var newSubtitleName = $"{subtitleNameWithoutExtension}.{languageSuffix}{subtitleExtension}";
            Console.WriteLine($"New Subtitle Name: {newSubtitleName}");

            // 构建目标路径
            var alter = "/" + Path.GetFileNameWithoutExtension(item.Video) + (string.IsNullOrEmpty(languageSuffix) ? "" : "." + languageSuffix) + Path.GetExtension(item.Subtitle);

            if (Config.Get().RenameStrategy == Common.RenameStrategy.Copy)
            {
                alter = Path.Combine(Path.GetDirectoryName(item.Video), Path.GetFileName(alter));
            }
            else
            {
                alter = Path.Combine(Path.GetDirectoryName(item.Subtitle), Path.GetFileName(alter));
            }

            // 输出最终生成的路径
            Console.WriteLine($"New File Path: {alter}");

            // 添加到重命名任务列表中
            destList.Add(new RenameTask(item.Subtitle, alter, item.Status == "已修改" ? "已修改" : "待修改")
            {
                MatchItem = item
            });
        }
    }


        public void ExecuteRename(IEnumerable<RenameTask> taskList)
    {
        foreach (var task in taskList)
        {
            if (task.Status == "已修改") continue;
            if (task.Status == "已跳过") continue;
            
            try
            {
                if (Config.Get().RenameStrategy == Common.RenameStrategy.Copy)
                {
                    FileHelper.CopyFile(task.Origin, task.Alter);
                } else
                {
                    if (Config.Get().Backup) FileHelper.BackupFile(task.Origin);
                    FileHelper.RenameFile(task.Origin, task.Alter);
                }
                task.Status = "已修改";
                if (task.MatchItem != null) task.MatchItem.Status = "已修改";
            }
            catch (Exception e)
            {
                task.Status = $"失败：{e.Message}";
                // task.Error = e.Message;
            }
        }
    }

    //public string GenerateRenameCommands(IEnumerable<MatchItem> list)
    //{
    //    var command = "";

    //    foreach (var item in list)
    //    {
    //        var subtitle = !string.IsNullOrEmpty(item.Subtitle) ? item.Subtitle : "?";
    //        var video = !string.IsNullOrEmpty(item.Video) ? item.Video : "?";
    //        command += $"mv {subtitle} {video}\n";
    //    }

    //    return command.Trim();
    //}

    public string GenerateRenameCommands(IEnumerable<MatchItem> list)
    {
        var command = "";

        foreach (var item in list)
        {
            var subtitle = !string.IsNullOrEmpty(item.Subtitle) ? item.Subtitle : "?";
            var video = !string.IsNullOrEmpty(item.Video) ? item.Video : "?";

            // 提取字幕文件的语言标识（假设它位于文件扩展名之前）
            var subtitleNameWithoutExtension = Path.GetFileNameWithoutExtension(subtitle);
            var subtitleExtension = Path.GetExtension(subtitle);
            var parts = subtitleNameWithoutExtension.Split('.');

            // 默认语言标识为空
            var languageSuffix = "";

            // 判断是否有语言标识
            if (parts.Length > 1 && parts.Last().Length == 2)  // 假设语言标识长度为2
            {
                languageSuffix = parts.Last();
                var baseName = string.Join('.', parts.Take(parts.Length - 1));
                subtitleNameWithoutExtension = baseName;
            }

            // 重新构造字幕文件名（保留语言标识）
            var newSubtitleName = $"{subtitleNameWithoutExtension}.{languageSuffix}{subtitleExtension}";

            command += $"mv \"{subtitle}\" \"{video}\"\n";
        }

        return command.Trim();
    }
}