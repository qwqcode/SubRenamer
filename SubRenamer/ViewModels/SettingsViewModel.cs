using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.DependencyInjection;
using CommunityToolkit.Mvvm.Input;
using SubRenamer.Services;
using Microsoft.Extensions.DependencyInjection;
using SubRenamer.Model;

namespace SubRenamer.ViewModels;

public partial class SettingsViewModel : ViewModelBase
{
    private bool _backupEnabled = Config.BackupEnabled;
    private string _videoExtAppend = Config.VideoExtAppend;
    private string _subtitleExtAppend = Config.SubtitleExtAppend;

    public bool BackupEnabled
    {
        get => _backupEnabled;
        set
        {
            Config.BackupEnabled = value;
            SetProperty(ref _backupEnabled, value);
        }
    }

    public string VideoExtAppend
    {
        get => _videoExtAppend;
        set
        {
            Config.VideoExtAppend = value;
            SetProperty(ref _videoExtAppend, value);
        }
    }
    
    public string SubtitleExtAppend
    {
        get => _subtitleExtAppend;
        set
        {
            Config.SubtitleExtAppend = value;
            SetProperty(ref _subtitleExtAppend, value);
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