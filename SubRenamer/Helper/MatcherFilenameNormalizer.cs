using System.Collections.Generic;
using System.Text;

namespace SubRenamer.Helper;

/// <summary>
/// Handles normalization and denormalization of filenames in MatchItems to ensure consistent Unicode handling.
/// Uses NormalizationForm.FormKC for compatibility normalization with composition.
/// </summary>
public class MatcherFilenameNormalizer
{
    private readonly Dictionary<string, string> _normalizedToRawVideos = new();
    private readonly Dictionary<string, string> _normalizedToRawSubtitles = new();

    /// <summary>
    /// Normalizes the filenames in a list of MatchItems using NormalizationForm.FormKC.
    /// </summary>
    /// <param name="matchItems">The list of MatchItems to normalize.</param>
    /// <returns>A new list of MatchItems with normalized filenames.</returns>
    public List<Core.MatchItem> Normalize(IReadOnlyList<Core.MatchItem> matchItems)
    {
        if (matchItems.Count == 0) return [];

        var result = new List<Core.MatchItem>(matchItems.Count);
        foreach (var item in matchItems)
        {
            var normalizedVideo = item.Video.Normalize(NormalizationForm.FormKC);
            var normalizedSubtitle = item.Subtitle.Normalize(NormalizationForm.FormKC);

            if (!string.IsNullOrEmpty(item.Video))
                _normalizedToRawVideos[normalizedVideo] = item.Video;
            if (!string.IsNullOrEmpty(item.Subtitle))
                _normalizedToRawSubtitles[normalizedSubtitle] = item.Subtitle;

            result.Add(new Core.MatchItem(item.Key, normalizedVideo, normalizedSubtitle));
        }

        return result;
    }

    /// <summary>
    /// Denormalizes the filenames in a list of MatchItems back to their original form.
    /// </summary>
    /// <param name="matchItems">The list of MatchItems to denormalize.</param>
    /// <returns>A new list of MatchItems with original filenames.</returns>
    /// <exception cref="KeyNotFoundException">Thrown when a normalized filename cannot be mapped back to its original form.</exception>
    public List<Core.MatchItem> Denormalize(IReadOnlyList<Core.MatchItem> matchItems)
    {
        if (matchItems.Count == 0) return [];

        var result = new List<Core.MatchItem>(matchItems.Count);
        foreach (var item in matchItems)
        {
            var originalVideo = !string.IsNullOrEmpty(item.Video) ? _normalizedToRawVideos[item.Video] : string.Empty;
            var originalSubtitle = !string.IsNullOrEmpty(item.Subtitle)
                ? _normalizedToRawSubtitles[item.Subtitle]
                : string.Empty;
            result.Add(new Core.MatchItem(item.Key, originalVideo, originalSubtitle));
        }

        return result;
    }

    /// <summary>
    /// Clears the internal mapping dictionaries.
    /// </summary>
    public void Clear()
    {
        _normalizedToRawVideos.Clear();
        _normalizedToRawSubtitles.Clear();
    }
}