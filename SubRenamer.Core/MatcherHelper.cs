using System.Text.RegularExpressions;

namespace SubRenamer.Core;

public static class MatcherHelper
{
    public static string ExtractMatchKeyRegex(string pattern, string filename)
    {
        try
        {
            var match = Regex.Match(filename, pattern, RegexOptions.IgnoreCase);
            if (match.Success) return match.Groups[1].Value;
        }
        catch (Exception e)
        {
            MatcherLogger.Out.WriteLine("[ExtractMatchKeyRegex] Exception: {0}", e.Message);
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
            // One-to-Many mapping (Video-Subtitles)
            var video = group.FirstOrDefault(item => !string.IsNullOrEmpty(item.Video))?.Video ?? "";
            var subtitles = group.Where(item => !string.IsNullOrEmpty(item.Subtitle)).Select(item => item.Subtitle);

            // Add a new MatchItem for each subtitle under the same key and video
            result.AddRange(subtitles.Select(subtitle => new MatchItem(group.Key, video, subtitle))
                .DefaultIfEmpty(new MatchItem(group.Key, video, ""))); // Keep video if no subtitles
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
    
    /// <summary>
    /// Compares two strings using a mixed comparison algorithm.
    /// </summary>
    /// <remarks>
    /// The algorithm compares strings by their numeric and non-numeric parts.
    /// </remarks>
    public class MixedStringComparer : IComparer<string>
    {
        public int Compare(string? x, string? y)
        {
            if (x is null || y is null) return 0;
            return NaturalCompare(x, y);
        }

        private int NaturalCompare(string str1, string str2)
        {
            int length1 = str1.Length;
            int length2 = str2.Length;
            int index1 = 0;
            int index2 = 0;

            while (index1 < length1 && index2 < length2)
            {
                if (char.IsDigit(str1[index1]) && char.IsDigit(str2[index2]))
                {
                    int num1 = 0;
                    int num2 = 0;

                    while (index1 < length1 && char.IsDigit(str1[index1]))
                    {
                        num1 = num1 * 10 + (str1[index1] - '0');
                        index1++;
                    }

                    while (index2 < length2 && char.IsDigit(str2[index2]))
                    {
                        num2 = num2 * 10 + (str2[index2] - '0');
                        index2++;
                    }

                    if (num1 != num2)
                    {
                        return num1.CompareTo(num2);
                    }
                }
                else
                {
                    if (str1[index1] != str2[index2])
                    {
                        return str1[index1].CompareTo(str2[index2]);
                    }

                    index1++;
                    index2++;
                }
            }

            return length1.CompareTo(length2);
        }
    }
}