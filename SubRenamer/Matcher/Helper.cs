using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using SubRenamer.Helper;

namespace SubRenamer.Matcher;

public static class Helper
{
    public static string ExtractMatchKeyRegex(string pattern, string filename)
    {
        try {
            var match = Regex.Match(filename, pattern, RegexOptions.IgnoreCase);
            if (match.Success) return match.Groups[1].Value;
        } catch (Exception e) {
            Logger.Out.WriteLine(e.Message);
        }
        return "";
    }

    public static string PatchKey(string key)
    {
        // check is pure number
        if (!string.IsNullOrWhiteSpace(key) && key.All(char.IsDigit))
            key = int.Parse(key).ToString(); // '01' -> '1'

        return key;
    }

    /// <summary>
    /// Merges items with the same non-empty keys by grouping them.
    /// Assuming the mapping between the video and the subtitles is one-to-many (1 to N).
    /// Each group is merged into a new list where the first non-empty video
    /// is selected and all non-empty subtitles are kept.
    /// </summary>
    /// <param name="items">A read-only list of MatchItem objects to be merged by key.</param>
    /// <returns>
    /// A list of MatchItem objects where keys are merged, each key associated with
    /// its first non-empty video and corresponding subtitles.
    /// </returns>
    public static List<MatchItem> MergeSameKeysItems(IReadOnlyList<MatchItem> items)
    {
        // Group items by non-empty keys, filtering out items with null or empty keys
        var groupedItems = items
            .Where(item => !string.IsNullOrEmpty(item.Key))
            .GroupBy(item => item.Key);

        // Create the merged list of MatchItems
        var result = new List<MatchItem>();
        foreach (var group in groupedItems)
        {
            var video = group.FirstOrDefault(item => !string.IsNullOrEmpty(item.Video))?.Video ?? "";
            var subtitles = group.Where(item => !string.IsNullOrEmpty(item.Subtitle)).Select(item => item.Subtitle);

            // Add a new MatchItem for each subtitle under the same key and video
            result.AddRange(subtitles.Select(subtitle => new MatchItem(group.Key, video, subtitle)));
        }

        // Keep items with empty keys
        result.AddRange(items.Where(item => string.IsNullOrEmpty(item.Key)));

        return result;
    }

    public static List<MatchItem> MoveEmptyKeyItemsToLast(IReadOnlyList<MatchItem> items)
    {
        var keyedItems = items.Where(x => !string.IsNullOrEmpty(x.Key));
        var emptyKeyItems = items.Where(x => string.IsNullOrEmpty(x.Key));
        return [..keyedItems, ..emptyKeyItems];
    }

    public static List<MatchItem> SortItemsByKeys(IReadOnlyList<MatchItem> items)
    {
        List<MatchItem> result = [..items];
        result.Sort((a, b) => new MixedStringComparer().Compare(a.Key, b.Key));
        return result;
    }
}