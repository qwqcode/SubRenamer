namespace SubRenamer.ViewModels;

public class MatchItem(string key, string video, string subtitle, string status)
{
    public string Key { get; set; } = key;
    public string Video { get; set; } = video;
    public string Subtitle { get; set; } = subtitle;
    public string Status { get; set; } = status;
}