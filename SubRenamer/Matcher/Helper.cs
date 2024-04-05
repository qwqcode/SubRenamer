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
    
    public static void MergeSameKeysItems(List<MatchItem> items)
    {
        for (var i = 0; i < items.Count; i++)
        {
            var item = items[i];
            if (string.IsNullOrEmpty(item.Key)) continue;
            
            for (var j = i + 1; j < items.Count; j++)
            {
                var other = items[j];
                if (item.Key == other.Key)
                {
                    item.Video = !string.IsNullOrEmpty(item.Video) ? item.Video : other.Video;
                    item.Subtitle = !string.IsNullOrEmpty(item.Subtitle) ? item.Subtitle : other.Subtitle;
                    item.Key = item.Key;
                    items.RemoveAt(j);
                    j--;
                }
            }
        }
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