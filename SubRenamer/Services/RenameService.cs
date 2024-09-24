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

        // Check for duplicate keys
        // (there are cases where video subtitles have a one-to-many relationship),
        // if found, retain the language suffix.
        // @see https://github.com/qwqcode/SubRenamer/pull/54
        // @file https://github.com/qwqcode/SubRenamer/blob/main/SubRenamer.Tests/MatcherTests/MergeSameKeysItemsTests.cs
        var hasDuplicateKey = matchList.GroupBy(x => x.Key).Any(g => g.Count() > 1);
        var keepLangExt = Config.Get().KeepLangExt || hasDuplicateKey;
        var customLangExt = Config.Get().CustomLangExt.Trim();
        var hasCustomLangExt = !hasDuplicateKey && !string.IsNullOrEmpty(customLangExt);

        foreach (var item in matchList)
        {
            if (string.IsNullOrEmpty(item.Subtitle) || string.IsNullOrEmpty(item.Video)) continue;

            // Subtitle file language suffix
            var subSuffix = "";
            if (keepLangExt)
            {
                // Extract language suffix from original subtitle file name
                var subSplit = Path.GetFileNameWithoutExtension(item.Subtitle).Split('.');
                if (subSplit.Length > 1) subSuffix = "." + subSplit[^1];
            }
            if (hasCustomLangExt) {
                // Custom appended suffix
                subSuffix += "." + customLangExt.TrimStart('.');
            }

            // Splice new subtitle file path
            var videoFolder = Path.GetDirectoryName(item.Video) ?? "";
            var subFilename = Path.GetFileNameWithoutExtension(item.Video) + subSuffix + Path.GetExtension(item.Subtitle);
            var altered = Path.Combine(videoFolder, subFilename);

            // No need to alter
            var noNeedAlter = item.Subtitle == altered;

            // Add to rename task list
            var status = !noNeedAlter
                ? (item.Status != MatchItemStatus.Altered ? RenameTaskStatus.Ready : RenameTaskStatus.Altered)
                : RenameTaskStatus.NoNeed;

            destList.Add(new RenameTask(item.Subtitle, altered)
            {
                MatchItem = item,
                Status = status
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
            RenameTaskStatus?[] skipStatus = [RenameTaskStatus.Altered, RenameTaskStatus.NoNeed];
            if (skipStatus.Contains(task.Status)) continue;

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

                task.Status = RenameTaskStatus.Altered;
                if (task.MatchItem != null) task.MatchItem.Status = MatchItemStatus.Altered;
            }
            catch (Exception e)
            {
                task.ErrorMessage = e.Message;
                task.Status = RenameTaskStatus.Failed;
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
            command += $"mv \"{subtitle.Replace("\"", "\\\"")}\" \"{video.Replace("\"", "\\\"")}\"\n";
        }

        return command.Trim();
    }
}