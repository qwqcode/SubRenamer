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
using SubRenamer.Helper;
using SubRenamer.Model;

namespace SubRenamer.ViewModels;

public partial class RegexRuleViewModel : ViewModelBase
{
    [ObservableProperty] private string _videoRegexStr = Config.Get().VideoRegex;
    [ObservableProperty] private string _subtitleRegexStr = Config.Get().SubtitleRegex;
    
    [ObservableProperty] private string _videoTestStr = "";
    [ObservableProperty] private string _subtitleTestStr = "";

    [ObservableProperty] private string _videoTestResult = "";
    [ObservableProperty] private string _subtitleTestResult = "";

    [ObservableProperty] private string _errorMessage = "";

    [RelayCommand]
    private void Save(Window window)
    {
        Config.Get().MatchMode = MatchMode.Regex;
        Config.Get().VideoRegex = VideoRegexStr;
        Config.Get().SubtitleRegex = SubtitleRegexStr;
        window.Close();
    }

    [RelayCommand]
    private async Task OpenFile(FileType type)
    {
        var d = App.Current?.Services?.GetService<IFilesService>();
        if (d == null) return;
        var file = await d.OpenSingleFileAsync();
        if (file == null) return;
        if (type == FileType.Subtitle) SubtitleTestStr = file.Name;
        else if (type == FileType.Video) VideoTestStr = file.Name;
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
            if (!match.Success || match.Groups.Count == 0)
                return Application.Current.GetResource<string>("App.Strings.RegexRuleNoMatch") ?? "No Match";
            return match.Groups[1].Value;
        }
        catch (Exception e)
        {
            ErrorMessage = e.Message;
            return Application.Current.GetResource<string>("App.Strings.RegexRuleMatchErr") ?? "Error";
        }
    }

    [RelayCommand]
    private void OpenLink(string url) => BrowserHelper.OpenBrowserAsync(url);
}