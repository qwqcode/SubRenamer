using System.IO;
using CommunityToolkit.Mvvm.ComponentModel;

namespace SubRenamer.Model;

public partial class MatchItem(string key = "", string video = "", string subtitle = "", string status = "") : ObservableObject
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
    [ObservableProperty] private string _status = status;
}