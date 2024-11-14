using System;
using System.IO;
using System.Linq;
using Avalonia;
using Avalonia.Markup.Xaml.Styling;
using Avalonia.Threading;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SubRenamer.Helper;
using DynamicData;
using Microsoft.Extensions.DependencyInjection;
using SubRenamer.Model;

namespace SubRenamer.ViewModels;

public partial class SettingsViewModel : ViewModelBase
{
    private bool _backupEnabled = Config.Get().Backup;
    private bool _updateCheckEnabled = Config.Get().UpdateCheck;
    private bool _keepLangExt = Config.Get().KeepLangExt;
    private bool _customLangExtEnabled = !string.IsNullOrEmpty(Config.Get().CustomLangExt);
    private bool _fileConflictFilterEnabled = Config.Get().FileConflictFilter;
    private string _customLangExt = Config.Get().CustomLangExt;
    private string _videoExtAppend = Config.Get().VideoExtAppend;
    private string _subtitleExtAppend = Config.Get().SubtitleExtAppend;
    private bool _subSyncExeDownloaded = false;

    /// Whether enabled the custom extension appending to classify the video and subtitle files
    private bool _fileClsExtAppendEnabled =
        !string.IsNullOrEmpty(Config.Get().VideoExtAppend) || !string.IsNullOrEmpty(Config.Get().SubtitleExtAppend);

    public SettingsViewModel()
    {
        _subSyncExeDownloaded = !string.IsNullOrEmpty(GetSubSyncService().RetrieveExePath());
    }

    public bool BackupEnabled
    {
        get => _backupEnabled;
        set
        {
            Config.Get().Backup = value;
            SetProperty(ref _backupEnabled, value);
        }
    }
    
    public bool UpdateCheckEnabled
    {
        get => _updateCheckEnabled;
        set
        {
            Config.Get().UpdateCheck = value;
            SetProperty(ref _updateCheckEnabled, value);
        }
    }

    public bool KeepLangExt
    {
        get => _keepLangExt;
        set
        {
            Config.Get().KeepLangExt = value;
            SetProperty(ref _keepLangExt, value);
        }
    }

    public bool CustomLangExtEnabled
    {
        get => _customLangExtEnabled;
        set
        {
            if (!value) CustomLangExt = "";
            else KeepLangExt = false;
            SetProperty(ref _customLangExtEnabled, value);
        }
    }

    public string CustomLangExt
    {
        get => _customLangExt;
        set
        {
            Config.Get().CustomLangExt = value;
            SetProperty(ref _customLangExt, value);
        }
    }
    
    public bool FileConflictFilter
    {
        get => _fileConflictFilterEnabled;
        set
        {
            Config.Get().FileConflictFilter = value;
            SetProperty(ref _fileConflictFilterEnabled, value);
        }
    }
    
    public string VideoExtAppend
    {
        get => _videoExtAppend;
        set
        {
            Config.Get().VideoExtAppend = value;
            SetProperty(ref _videoExtAppend, value);
        }
    }

    public string SubtitleExtAppend
    {
        get => _subtitleExtAppend;
        set
        {
            Config.Get().SubtitleExtAppend = value;
            SetProperty(ref _subtitleExtAppend, value);
        }
    }

    public bool FileClsExtAppendEnabled
    {
        get => _fileClsExtAppendEnabled;
        set
        {
            if (!value)
            {
                VideoExtAppend = "";
                SubtitleExtAppend = "";
            }
            SetProperty(ref _fileClsExtAppendEnabled, value);
        }
    }
    
    public bool SubSyncExeDownloaded
    {
        get => _subSyncExeDownloaded;
        set => SetProperty(ref _subSyncExeDownloaded, value);
    }
    
    [RelayCommand]
    private void OpenLink(string url) => BrowserHelper.OpenBrowserAsync(url);

    private ISubSyncService GetSubSyncService() => App.Current!.Services!.GetService<ISubSyncService>()!;
    
    [RelayCommand]
    private void DownloadSubSyncExe()
    {
        Dispatcher.UIThread.Post(async () =>
        {
            try
            {
                await GetSubSyncService().Shutdown();
                await GetSubSyncService().DownloadFFsubsyncBin();
                Console.Write("FFsubsync binary downloaded");
                SubSyncExeDownloaded = true;

                await GetSubSyncService().Bootstrap();
            }
            catch (Exception)
            {
                SubSyncExeDownloaded = false;
            }
        });
    }
    
    #region Language
    public static string[] LanguageNames { get; } = I18NHelper.LanguageNames;
    public static string[] LanguageTitles { get; } = I18NHelper.LanguageTitles;

    private string _language = Config.Get().Language;

    [ObservableProperty] private int _languageIndex = LanguageNames.IndexOf(Config.Get().Language);

    public string Language
    {
        get => _language;
        set
        {
            Config.Get().Language = value;
            Application.Current.Translate(value);
            SetProperty(ref _language, value);
        }
    }

    partial void OnLanguageIndexChanged(int value)
    {
        Language = I18NHelper.Languages[value].Split(":")[0];
    }
    #endregion
}