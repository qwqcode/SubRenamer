using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using Avalonia.Controls;
using SubRenamer.Helper;
using SubRenamer.Model;

namespace SubRenamer.Services;

public class RenameService(Window target) : IRenameService
{
    private readonly Window _target = target;

    public void UpdateRenameTaskList(IReadOnlyList<MatchItem> matchList, Collection<RenameTask> destList)
    {
        destList.Clear();

        // 检查是否有重复的 Key (存在视频字幕一对多的情况)，若有则保留语言后缀
        // @see https://github.com/qwqcode/SubRenamer/pull/54
        // @file https://github.com/qwqcode/SubRenamer/blob/main/SubRenamer.Tests/MatcherTests/MergeSameKeysItemsTests.cs
        var hasDuplicateKey = matchList.GroupBy(x => x.Key).Any(g => g.Count() > 1);
        var keepLangExt = Config.Get().KeepLangExt || hasDuplicateKey;
        var customLangExt = Config.Get().CustomLangExt.Trim();
        var hasCustomLangExt = !hasDuplicateKey && !string.IsNullOrEmpty(customLangExt);

        foreach (var item in matchList)
        {
            if (string.IsNullOrEmpty(item.Subtitle) || string.IsNullOrEmpty(item.Video)) continue;

            // 字幕文件语言后缀
            var subSuffix = "";
            if (keepLangExt)
            {
                // 从原始字幕文件名中提取语言后缀
                var subSplit = Path.GetFileNameWithoutExtension(item.Subtitle).Split('.');
                if (subSplit.Length > 1) subSuffix = "." + subSplit[^1];
            }
            if (hasCustomLangExt) {
                // 自定义追加的后缀
                subSuffix += "." + customLangExt.TrimStart('.');
            }

            // 拼接新的字幕文件路径
            var videoFolder = Path.GetDirectoryName(item.Video) ?? "";
            var subFilename = Path.GetFileNameWithoutExtension(item.Video) + subSuffix + Path.GetExtension(item.Subtitle);
            var altered = Path.Combine(videoFolder, subFilename);

            // 添加到重命名任务列表中
            destList.Add(new RenameTask(item.Subtitle, altered, item.Status == "已修改" ? "已修改" : "待修改")
            {
                MatchItem = item
            });
        }
    }

    public void ExecuteRename(IReadOnlyList<RenameTask> taskList)
    {
        var backupEnabled = Config.Get().Backup;
        
        // Record files that have been backed up,
        // avoid duplicate backups when mapping is one-to-many (video-subtitle)
        var filesHadBackup = new Dictionary<string, bool>();
        
        foreach (var task in taskList)
        {
            if (task.Status == "已修改") continue;
            if (task.Status == "已跳过") continue;
            
            try
            {
                // Whether the origin and alter files are in the same folder
                // If they are, rename in-place; otherwise, copy the file
                var isSameFolder = Path.GetDirectoryName(task.Origin) == Path.GetDirectoryName(task.Alter);
                
                if (isSameFolder)
                {
                    // Backup (only rename in-place)
                    if (backupEnabled && filesHadBackup.TryAdd(task.Origin, true))
                        FileHelper.BackupFile(task.Origin);
                    
                    // Rename in-place (like mv in linux)
                    FileHelper.RenameFile(task.Origin, task.Alter);
                }
                else
                {
                    // Copy (like cp in linux)
                    FileHelper.CopyFile(task.Origin, task.Alter);
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

    public string GenerateRenameCommands(IReadOnlyList<MatchItem> list)
    {
        var command = "";
        
        foreach (var item in list)
        {
            var subtitle = !string.IsNullOrEmpty(item.Subtitle) ? item.Subtitle : "?";
            var video = !string.IsNullOrEmpty(item.Video) ? item.Video : "?";
            command += $"mv {subtitle} {video}\n";
        }

        return command.Trim();
    }
}