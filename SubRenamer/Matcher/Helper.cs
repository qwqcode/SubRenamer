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
            //筛选出所有Subtitles用于匹配多个字幕
            if (item.Subtitle != "")
            {
                for (var j = 0; j < items.Count; j++)
                {
                    var other = items[j];
                    if (item.Key == other.Key && other.Subtitle == "")
                    {
                        item.Video = !string.IsNullOrEmpty(item.Video) ? item.Video : other.Video;
                        other.Status = "Paired";
                    }
                }
            }
            //将已匹配完成的只包含Video的item删除
            if (i == (items.Count - 1))
            {
                for (i = 0; i < items.Count; i++)
                {
                    if (items[i].Status == "Paired")
                    {
                        items.RemoveAt(i);
                        i--;
                    }
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