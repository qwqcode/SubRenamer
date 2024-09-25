namespace SubRenamer.Core;

public class MatcherOptions
{
    /// <summary>
    /// Custom regex for extracting keys from video files.
    /// (If not set, the diff algorithm will be used)
    /// </summary>
    public string? VideoRegex { get; init; }

    /// <summary>
    /// Custom regex for extracting keys from subtitle files
    /// (If not set, the diff algorithm will be used)
    /// </summary>
    public string? SubtitleRegex { get; init; }
}