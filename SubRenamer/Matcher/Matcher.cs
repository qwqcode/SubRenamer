using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SubRenamer.Matcher;

public record MatchItem(string Key, string Video, string Subtitle);

public static class Matcher
{
    public static List<MatchItem> Execute(IReadOnlyList<MatchItem> inputItems)
        => Execute(inputItems, new MatcherOptions());

    public static List<MatchItem> Execute(IReadOnlyList<MatchItem> inputItems, MatcherOptions options)
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
        var video2Keys = CalculateFileKeys(videoFiles, customRegex: options.VideoRegex);
        var subtitle2Keys = CalculateFileKeys(subtitleFiles, customRegex: options.SubtitleRegex);

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

    private static Dictionary<string, string> CalculateFileKeys(IReadOnlyList<string> files, string? customRegex)
    {
        var result = new Dictionary<string, string>();

        if (customRegex is null)
        {
            // Method 1. Auto Diff Algorithm
            var filenames = files
                .Select(Path.GetFileNameWithoutExtension)
                .Where(x => !string.IsNullOrEmpty(x))
                .Distinct()
                .ToList();

            // Diff filenames
            var diff = Diff.GetDiffResult(filenames!);
            Logger.Out.WriteLine("[Diff.GetDiffResult]\n\n  {0}\n", (diff != null ? diff : "null"));

            // Extract Match keys
            foreach (var f in files)
            {
                result[f] = Helper.PatchKey(Diff.ExtractMatchKeyByDiff(diff, Path.GetFileNameWithoutExtension(f)));
            }
        }
        else
        {
            // Method 2. Custom Regex
            foreach (var f in files)
            {
                result[f] = Helper.PatchKey(Helper.ExtractMatchKeyRegex(customRegex, Path.GetFileName(f)));
            }
        }

        return result;
    }
}