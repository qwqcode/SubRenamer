using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using SubRenamer.Helper;
using SubRenamer.Model;

namespace SubRenamer.Matcher;

public static class Helper
{
    public static string? ExtractMatchKeyRegex(string pattern, string filename)
    {
        try {
            var match = Regex.Match(filename, pattern, RegexOptions.IgnoreCase);
            if (match.Success) return match.Groups[1].Value;
        } catch (Exception e) {
            Console.WriteLine(e.Message);
        }
        return "";
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

    public static void MoveEmptyKeyItemsToLast(List<MatchItem> items)
    {
        var emptyKeyItems = items.Where(x => string.IsNullOrEmpty(x.Key)).ToList();
        foreach (var item in emptyKeyItems)
        {
            items.Remove(item);
            items.Add(item);
        }
    }

    public static void SortItemsByKeys(List<MatchItem> items)
    {
        items.Sort((a, b) => new MixedStringComparer().Compare(a.Key, b.Key));
    }
}