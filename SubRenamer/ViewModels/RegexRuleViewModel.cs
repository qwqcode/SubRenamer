using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.DependencyInjection;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using SubRenamer.Common;
using SubRenamer.Model;

namespace SubRenamer.ViewModels;

public partial class RegexRuleViewModel : ViewModelBase
{
    [ObservableProperty] private string _videoRegexStr = Config.VideoRegex;
    [ObservableProperty] private string _subtitleRegexStr = Config.SubtitleRegex;
    
    [ObservableProperty] private string _videoTestStr = "";
    [ObservableProperty] private string _subtitleTestStr = "";

    [ObservableProperty] private string _videoTestResult = "";
    [ObservableProperty] private string _subtitleTestResult = "";

    [ObservableProperty] private string _errorMessage = "";

    [RelayCommand]
    private async Task Save(Window window)
    {
        Config.MatchMode = MatchMode.Regex;
        Config.VideoRegex = VideoRegexStr;
        Config.SubtitleRegex = SubtitleRegexStr;
        window.Close();
    }

    [RelayCommand]
    private async Task OpenVideoFile()
    {
        var file = await App.Current?.Services?.GetService<IFilesService>()!.OpenSingleFileAsync();
        if (file != null) VideoTestStr = file.Name;
    }

    [RelayCommand]
    private async Task OpenSubtitleFile()
    {
        var file = await App.Current?.Services?.GetService<IFilesService>()!.OpenSingleFileAsync();
        if (file != null) SubtitleTestStr = file.Name;
    }

    partial void OnVideoRegexStrChanged(string value) => VideoTestResult = MatchByInputRegex(value, VideoTestStr);
    partial void OnSubtitleRegexStrChanged(string value) => SubtitleTestResult = MatchByInputRegex(value, SubtitleTestStr);
    partial void OnVideoTestStrChanged(string value) => VideoTestResult = MatchByInputRegex(VideoRegexStr, value);
    partial void OnSubtitleTestStrChanged(string value) => SubtitleTestResult = MatchByInputRegex(VideoRegexStr, value);

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
    private async Task OpenLink(string url)
    {
        var service = Ioc.Default.GetService<IBrowserService>();
        if (service is null) throw new NullReferenceException("Missing Browser Service instance.");
        await service.OpenBrowserAsync(new Uri(url));
    }
}