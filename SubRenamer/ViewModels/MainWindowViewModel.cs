using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace SubRenamer.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public static string Greeting => "Welcome to Avalonia!";
        
        public ObservableCollection<MatchItem> MatchList { get; }

        public MainWindowViewModel()
        {
            var people = new List<MatchItem> 
            {
                new MatchItem("01", "test_video_01.mp4", "test_subtitle_01.ass", "已匹配"),
                new MatchItem("02", "test_video_02.mp4", "test_subtitle_02.ass", "已匹配"),
                new MatchItem("03", "test_video_03.mp4", "test_subtitle_03.ass", "已匹配"),
            };
            MatchList = new ObservableCollection<MatchItem>(people);
        }
    }

    public class MatchItem
    {
        public string Key { get; set; }
        public string Video { get; set; }
        public string Subtitle { get; set; }
        public string Status { get; set; }
    
        public MatchItem(string key, string video, string subtitle, string status)
        {
            Key = key;
            Video = video;
            Subtitle = subtitle;
            Status = status;
        }
    }
}
