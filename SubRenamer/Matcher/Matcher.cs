using System.Collections.Generic;
using System.IO;
using System.Linq;
using SubRenamer.Common;

namespace SubRenamer.Matcher;

public record MatchItem(string Key, string Video, string Subtitle);

public static class Matcher
{
    public static List<MatchItem> Execute(IReadOnlyList<MatchItem> inputItems)
    {
        // Create new collection
        List<MatchItem> result = [];
        List<string> videoFiles = [];
        List<string> subtitleFiles = [];

        // Separate Video files and Subtitle files
        inputItems.Where(x => !string.IsNullOrEmpty(x.Video)).ToList().ForEach(item =>
        {
            result.Add(new MatchItem("", item.Video, ""));
            videoFiles.Add(item.Video);
        });

        inputItems.Where(x => !string.IsNullOrEmpty(x.Subtitle)).ToList().ForEach(item =>
        {
            result.Add(new MatchItem("", "", item.Subtitle));
            subtitleFiles.Add(item.Subtitle);
        });

        // Get file keys
        var m = Config.Get().MatchMode;
        var videoRegex = (m != MatchMode.Diff) ? (m == MatchMode.Manual ? Config.Get().ManualVideoRegex : Config.Get().VideoRegex) : null;
        var subtitleRegex = (m != MatchMode.Diff) ? (m == MatchMode.Manual ? Config.Get().ManualSubtitleRegex : Config.Get().SubtitleRegex) : null;

        var video2Keys = CalculateFileKeys(videoFiles, videoRegex);
        var subtitle2Keys = CalculateFileKeys(subtitleFiles, subtitleRegex);

        // Apply keys
        List<MatchItem> keyedItems = [];
        foreach (var item in result)
        {
            string? k = null;

            if (!string.IsNullOrEmpty(item.Video)) video2Keys.TryGetValue(item.Video, out k);
            else if (!string.IsNullOrEmpty(item.Subtitle)) subtitle2Keys.TryGetValue(item.Subtitle, out k);

            keyedItems.Add(new MatchItem(k ?? "", item.Video, item.Subtitle));
        }

        result = keyedItems;

        // Merge items with same keys
        result = Helper.MergeSameKeysItems(result);

        // Sort
        result = Helper.SortItemsByKeys(result);

        // Move empty keys to last
        result = Helper.MoveEmptyKeyItemsToLast(result);

        return result;
    }

    private static Dictionary<string, string> CalculateFileKeys(IReadOnlyList<string> files, string? regexPattern)
    {
        var result = new Dictionary<string, string>();

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
            Logger.Out.WriteLine("[Diff.GetDiffResult]\n\n  {0}\n", (diff != null ? diff : "null"));

            // Extract Match keys
            foreach (var f in files)
            {
                result[f] = Diff.ExtractMatchKeyByDiff(diff, Path.GetFileNameWithoutExtension(f));
            }
        }
        else
        {
            // 2. Regex Algorithm
            foreach (var f in files)
            {
                result[f] = Helper.ExtractMatchKeyRegex(regexPattern, Path.GetFileName(f));
            }
        }

        return result;
    }
}