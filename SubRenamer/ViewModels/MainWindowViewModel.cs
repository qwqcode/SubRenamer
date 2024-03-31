using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Input;
using ReactiveUI;
using SubRenamer.Services;
using Microsoft.Extensions.DependencyInjection;
using MsBox.Avalonia;
using MsBox.Avalonia.Enums;

namespace SubRenamer.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    public static string Greeting => "Welcome to Avalonia!";
    
    public ObservableCollection<MatchItem> MatchList { get; }

    public Interaction<Unit, string?> ShowOpenFileDialog { get; }
    
    public MainWindowViewModel()
    {
        var matchList = new List<MatchItem> 
        {
            new MatchItem("01", "test_video_01.mp4", "test_subtitle_01.ass", "已匹配"),
            new MatchItem("02", "test_video_02.mp4", "test_subtitle_02.ass", "已匹配"),
            new MatchItem("03", "test_video_03.mp4", "test_subtitle_03.ass", "已匹配"),
        };
        MatchList = new ObservableCollection<MatchItem>(matchList);
        
        ShowOpenFileDialog = new Interaction<Unit, string?>();
    }

    [RelayCommand]
    private async Task OpenFile(CancellationToken token)
    {
        ErrorMessages?.Clear();
        try
        {
            var filesService = App.Current?.Services?.GetService<IFilesService>();
            if (filesService is null) throw new NullReferenceException("Missing File Service instance.");

            var file = await filesService.OpenFileAsync();
            if (file is null) return;
            
            var box = MessageBoxManager
                 .GetMessageBoxStandard("Caption", file.Path.ToString(),
                     ButtonEnum.YesNo);

            var result = await box.ShowAsync();
        }
        catch (Exception e)
        {
            ErrorMessages?.Add(e.Message);
        }
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
