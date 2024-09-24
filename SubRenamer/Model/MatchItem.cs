using System.IO;
using Avalonia;
using CommunityToolkit.Mvvm.ComponentModel;
using SubRenamer.Helper;

namespace SubRenamer.Model;

public partial class MatchItem(string key = "", string video = "", string subtitle = "") : ObservableObject
{
    /**
     * Match Key
     */
    [ObservableProperty] private string _key = key;

    /**
     * Video Absolute Path
     */
    [ObservableProperty] private string _video = video;

    [ObservableProperty] private string _videoName = Path.GetFileName(video);

    partial void OnVideoChanged(string value) => VideoName = Path.GetFileName(value);

    /**
     * Subtitle Absolute Path
     */
    [ObservableProperty] private string _subtitle = subtitle;

    [ObservableProperty] private string _subtitleName = Path.GetFileName(subtitle);

    partial void OnSubtitleChanged(string value) => SubtitleName = Path.GetFileName(value);

    /**
     * Current Status
     */
    [ObservableProperty] private MatchItemStatus? _status;

    /**
     * Current Status Text
     */
    [ObservableProperty] private string _statusText = "";

    partial void OnStatusChanged(MatchItemStatus? value)
    {
        StatusText = GetStatusText();
    }

    public string GetStatusText()
    {
        return Status switch
        {
            MatchItemStatus.Matched => Application.Current.GetResource<string>("App.Strings.MatchItemStatusMatched") ?? "Matched",
            MatchItemStatus.NoVideo => Application.Current.GetResource<string>("App.Strings.MatchItemStatusNoVideo") ?? "NoVideo",
            MatchItemStatus.NoSubtitle => Application.Current.GetResource<string>("App.Strings.MatchItemStatusNoSubtitle") ?? "NoSubtitle",
            MatchItemStatus.NoMatch => Application.Current.GetResource<string>("App.Strings.MatchItemStatusNoMatch") ?? "NoMatch",
            MatchItemStatus.Altered => Application.Current.GetResource<string>("App.Strings.MatchItemStatusAltered") ?? "Altered",
            _ => ""
        };
    }
}