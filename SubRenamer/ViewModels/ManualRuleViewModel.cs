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
    [ObservableProperty] private string _videoRaw = Config.ManualVideoRaw;
    [ObservableProperty] private string _video = Config.ManualVideo;
    [ObservableProperty] private string _videoRegex = Config.ManualVideoRegex;
    [ObservableProperty] private string _videoMatchResult = "";
    
    [ObservableProperty] private string _subtitleRaw = Config.ManualSubtitleRaw;
    [ObservableProperty] private string _subtitle = Config.ManualSubtitle;
    [ObservableProperty] private string _subtitleRegex = Config.ManualSubtitleRegex;
    [ObservableProperty] private string _subtitleMatchResult = "";
    
    [ObservableProperty] private string _errorMessage = "";
    
    [RelayCommand]
    private async Task Save(Window window)
    {
        Config.MatchMode = MatchMode.Manual;
        Config.ManualVideoRaw = VideoRaw;
        Config.ManualVideo = Video;
        Config.ManualVideoRegex = VideoRegex;
        Config.ManualSubtitleRaw = SubtitleRaw;
        Config.ManualSubtitle = Subtitle;
        Config.ManualSubtitleRegex = SubtitleRegex;
        
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
            .Replace(@"\*", @"(.*?)");
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
    private async Task OpenVideoFile()
    {
        var file = await App.Current?.Services?.GetService<IFilesService>()!.OpenSingleFileAsync();
        if (file == null) return;
        VideoRaw = file.Name;
        Video = file.Name;
    }

    [RelayCommand]
    private async Task OpenSubtitleFile()
    {
        var file = await App.Current?.Services?.GetService<IFilesService>()!.OpenSingleFileAsync();
        if (file == null) return;
        SubtitleRaw = file.Name;
        Subtitle = file.Name;
    }
}