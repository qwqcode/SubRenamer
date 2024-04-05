using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using SubRenamer.Common;
using SubRenamer.Model;

namespace SubRenamer.Matcher;

public static class Matcher
{
    public static List<MatchItem> Execute(IReadOnlyList<MatchItem> matchItems)
    {
        // Create new collection
        var items = new List<MatchItem>();
        var videoFiles = new List<string>();
        var subtitleFiles = new List<string>();
        
        // Separate Video files and Subtitle files
        matchItems.Where(x => !string.IsNullOrEmpty(x.Video)).ToList().ForEach(item =>
        {
            items.Add(new MatchItem("", item.Video, "", ""));
            videoFiles.Add(item.Video);
        });
        
        matchItems.Where(x => !string.IsNullOrEmpty(x.Subtitle)).ToList().ForEach(item =>
        {
            items.Add(new MatchItem("", "", item.Subtitle, ""));
            subtitleFiles.Add(item.Subtitle);
        });
        
        // Get file keys
        var m = Config.MatchMode;
        var videoRegex = (m != MatchMode.Diff) ? (m == MatchMode.Manual ? Config.ManualVideoRegex : Config.VideoRegex) : null;
        var subtitleRegex = (m != MatchMode.Diff) ? (m == MatchMode.Manual ? Config.ManualSubtitleRegex : Config.SubtitleRegex) : null;
        
        var video2Keys = CalculateFileKeys(videoFiles, videoRegex);
        var subtitle2Keys = CalculateFileKeys(subtitleFiles, subtitleRegex);
        
        // Apply keys
        foreach (var item in items)
        {
            string? k = null;
            
            if (!string.IsNullOrEmpty(item.Video)) video2Keys.TryGetValue(item.Video, out k);
            else if (!string.IsNullOrEmpty(item.Subtitle)) subtitle2Keys.TryGetValue(item.Subtitle, out k);

            item.Key = k ?? "";
        }
        
        // Merge items with same keys
        Helper.MergeSameKeysItems(items);
        
        // Sort
        Helper.SortItemsByKeys(items);
        
        // Move empty keys to last
        Helper.MoveEmptyKeyItemsToLast(items);

        return items;
    }
    
    private static Dictionary<string, string?> CalculateFileKeys(List<string> files, string? regexPattern)
    {
        var dict = new Dictionary<string, string?>();
        
        if (regexPattern is null)
        {
            // 1. Auto Diff Algorithm
            
            // Diff filenames
            var names = files
                .Select(Path.GetFileNameWithoutExtension)
                .Where(x => !string.IsNullOrEmpty(x))
                .Distinct()
                .ToList();
        
            var diff = Diff.GetDiffResult(names!);
        
            // Extract Match keys
            foreach (var f in files)
            {
                var key = Diff.ExtractMatchKeyByDiff(diff, Path.GetFileNameWithoutExtension(f));
                dict[f] = key;
            }
        }
        else
        {
            // 2. Regex Algorithm
            foreach (var f in files)
            {
                var key = Helper.ExtractMatchKeyRegex(regexPattern, Path.GetFileName(f));
                dict[f] = key;
            }
        }

        return dict;
    }
}