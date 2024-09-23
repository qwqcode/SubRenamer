using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using SubRenamer.Common;
using SubRenamer.Model;

namespace SubRenamer.ViewModels;

public partial class ManualRuleViewModel : ViewModelBase
{
    [ObservableProperty] private string _videoRaw = Config.Get().ManualVideoRaw;
    [ObservableProperty] private string _video = Config.Get().ManualVideo;
    [ObservableProperty] private string _videoRegex = Config.Get().ManualVideoRegex;
    [ObservableProperty] private string _videoMatchResult = "";
    
    [ObservableProperty] private string _subtitleRaw = Config.Get().ManualSubtitleRaw;
    [ObservableProperty] private string _subtitle = Config.Get().ManualSubtitle;
    [ObservableProperty] private string _subtitleRegex = Config.Get().ManualSubtitleRegex;
    [ObservableProperty] private string _subtitleMatchResult = "";
    
    [ObservableProperty] private string _errorMessage = "";
    
    [RelayCommand]
    private void Save(Window window)
    {
        Config.Get().MatchMode = MatchMode.Manual;
        Config.Get().ManualVideoRaw = VideoRaw;
        Config.Get().ManualVideo = Video;
        Config.Get().ManualVideoRegex = VideoRegex;
        Config.Get().ManualSubtitleRaw = SubtitleRaw;
        Config.Get().ManualSubtitle = Subtitle;
        Config.Get().ManualSubtitleRegex = SubtitleRegex;
        
        window.Close();
    }

    partial void OnVideoChanged(string value)
    {
        VideoRegex = GenerateRegex(value);
        VideoMatchResult = MatchByInputRegex(VideoRegex, VideoRaw);
    }

    partial void OnSubtitleChanged(string value)
    {
        SubtitleRegex = GenerateRegex(value);
        SubtitleMatchResult = MatchByInputRegex(SubtitleRegex, SubtitleRaw);
    }

    private string GenerateRegex(string input)
    {
        var pattern = Regex.Escape(input)
            .Replace(@"\$\$", @"(.+?)")
            .Replace(@"\*", @".*?");
        return pattern;
    }

    private string MatchByInputRegex(string pattern, string testCase)
    {
        ErrorMessage = "";
        try
        {
            var match = Regex.Match(testCase, pattern);
            if (!match.Success || match.Groups.Count == 0) return "未匹配";
            return match.Groups[1].Value;
        }
        catch (Exception e)
        {
            ErrorMessage = e.Message;
            return "错误";
        }
    }

    [RelayCommand]
    private async Task OpenFile(FileType type)
    {
        var d = App.Current?.Services?.GetService<IFilesService>();
        if (d == null) return;
        var file = await d.OpenSingleFileAsync();
        if (file == null) return;
        if (type == FileType.Video)
        {
            VideoRaw = file.Name;
            Video = file.Name;
        }
        else if (type == FileType.Subtitle)
        {
            SubtitleRaw = file.Name;
            Subtitle = file.Name;
        }
    }
}