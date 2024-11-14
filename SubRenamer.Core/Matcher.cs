namespace SubRenamer.Core;

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

        // Return directly if only 1 video and 1 subtitle
        if (videoFiles.Count == 1 && subtitleFiles.Count == 1)
            return [new MatchItem("1", videoFiles[0], subtitleFiles[0])];

        // Get file keys
        var video2Keys = CalculateFileKeys(videoFiles, customRegex: options.VideoRegex);
        var subtitle2Keys = CalculateFileKeys(subtitleFiles, customRegex: options.SubtitleRegex);

        // Merge items with same filename
        result = MatcherHelper.MergeSameFilenameItems(result);
        
        // Apply keys
        List<MatchItem> keyedItems = [];
        foreach (var item in result)
        {
            if (item.Key != "")
            {
                keyedItems.Add(item);
                continue;
            }
            
            string? k = null;

            if (!string.IsNullOrEmpty(item.Video)) video2Keys.TryGetValue(item.Video, out k);
            else if (!string.IsNullOrEmpty(item.Subtitle)) subtitle2Keys.TryGetValue(item.Subtitle, out k);

            keyedItems.Add(new MatchItem(k ?? "", item.Video, item.Subtitle));
        }

        result = keyedItems;

        // Merge items with same keys
        result = MatcherHelper.MergeSameKeysItems(result);

        // Sort
        result = MatcherHelper.SortItemsByKeys(result);

        // Move empty keys to last
        result = MatcherHelper.MoveEmptyKeyItemsToLast(result);

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
            var diff = MatcherDiff.GetDiffResult(filenames!);
            MatcherLogger.Out.WriteLine("[Diff.GetDiffResult]\n\n  {0}\n", (diff != null ? diff : "null"));

            // Extract Match keys
            foreach (var f in files)
            {
                result[f] = MatcherHelper.PatchKey(
                    MatcherDiff.ExtractMatchKeyByDiff(diff, Path.GetFileNameWithoutExtension(f)));
            }
        }
        else
        {
            // Method 2. Custom Regex
            foreach (var f in files)
            {
                result[f] = MatcherHelper.PatchKey(
                    MatcherHelper.ExtractMatchKeyRegex(customRegex, Path.GetFileName(f)));
            }
        }

        return result;
    }
}