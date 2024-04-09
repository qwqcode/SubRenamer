using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Avalonia.Platform.Storage;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.DependencyInjection;
using CommunityToolkit.Mvvm.Input;
using DynamicData.Binding;
using SubRenamer.Services;
using Microsoft.Extensions.DependencyInjection;
using SubRenamer.Common;
using SubRenamer.Helper;
using SubRenamer.Model;
using SubRenamer.Views;
using static SubRenamer.Common.Constants;

namespace SubRenamer.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    [ObservableProperty] private ObservableCollection<MatchItem> _matchList = [];
    [ObservableProperty] private Collection<MatchItem> _selectedItems = [];

    [ObservableProperty] private bool _showRenameTasks = false;
    [ObservableProperty] private ObservableCollection<RenameTask> _renameTasks = [];
    
    [ObservableProperty] private string _currMatchModeText = "";
    [ObservableProperty] private string _currVersionText = $"v{Config.AppVersion}";
    [ObservableProperty] private string _currVersionBtnLink = "https://github.com/qwqcode/SubRenamerNG";
    
    public MainViewModel()
    {
        SyncStatusText();
        
        #if DEBUG
        _matchList = new ObservableCollection<MatchItem> 
        {
            new ("", "test_video_01.mp4", "", ""),
            new ("", "test_video_02.mp4", "", ""),
            new ("", "test_video_03.mp4", "", ""),
            new ("", "", "test_subtitle_01.ass", ""),
            new ("", "", "test_subtitle_02.ass", ""),
            new ("", "", "test_subtitle_03.ass", ""),
        };
        #endif
    }

    public void SyncStatusText()
    {
        CurrMatchModeText = Config.MatchMode switch
        {
            MatchMode.Diff => "自动匹配",
            MatchMode.Manual => "手动匹配",
            MatchMode.Regex => "正则匹配",
            _ => ""
        };
    }

    [RelayCommand]
    private void DeleteItem()
    {
        foreach (var item in SelectedItems) MatchList.Remove(item);
    }

    [RelayCommand]
    private void ClearAll()
    {
        MatchList.Clear();
    }

    [RelayCommand]
    private async Task OpenSettings()
    {
        var dialogService = App.Current?.Services?.GetService<IDialogService>();
        if (dialogService is null) throw new NullReferenceException("Missing Dialog Service instance.");
        await dialogService.OpenSettings();
    }
    
    [RelayCommand]
    private async Task OpenRules()
    {
        var dialogService = App.Current?.Services?.GetService<IDialogService>();
        if (dialogService is null) throw new NullReferenceException("Missing Dialog Service instance.");
        await dialogService.OpenRules();
    }

    [RelayCommand]
    private async Task EditItem()
    {
        if (SelectedItems.Count == 0) return;
        
        var dialogService = App.Current?.Services?.GetService<IDialogService>();
        if (dialogService is null) throw new NullReferenceException("Missing Dialog Service instance.");
        await dialogService.OpenItemEdit(SelectedItems.First(), MatchList);
    }

    [RelayCommand]
    private async Task OpenFile()
    {
        ErrorMessages?.Clear();
        try
        {
            var filesService = App.Current?.Services?.GetService<IFilesService>()!;
            var files = await filesService.OpenFilesAsync();
            await ImportFiles(files.Select(x => x.Path.LocalPath).ToList());
        }
        catch (Exception e)
        {
            ErrorMessages?.Add(e.Message);
        }
        
        await PerformMatch();
    }

    private async Task ImportFiles(List<string> files)
    {
        // Copy
        files = files.ToList();
        
        // merge files with MatchList
        MatchList.ToList().ForEach(x =>
        {
            files.Add(x.Video);
            files.Add(x.Subtitle);
        });
        
        // distinct and remove empty
        files = files.Distinct().Where(x => !string.IsNullOrEmpty(x)).ToList();
        
        // Group files by type
        List<string> videos = [];
        List<string> subtitles = [];
        
        foreach (var f in files)
        {
            var type = GetFileTypeByExtension(Path.GetExtension(f));
            var list = type switch
            {
                FileType.Video => videos,
                FileType.Subtitle => subtitles,
                _ => null
            };
            list?.Add(f);
        }
        
        // Solve subtitle conflict
        List<string> subtitleFilenames = subtitles
            .Select(Path.GetFileName)
            .Where(x => !string.IsNullOrEmpty(x))
            .ToList()!;
        
        var subtitleExtTypes = GetExtensionTypes(subtitleFilenames);
        if (GetExtensionTypes(subtitleFilenames).Count > 1)
        {
            var dialogService = App.Current?.Services?.GetService<IDialogService>()!;
            var keepExt = await dialogService.OpenConflict(subtitleExtTypes);
            
            // Filter subtitle file by user selection
            if (keepExt != null)
            {
                var subtitlesFiltered = new List<string>();

                foreach (var f in subtitles)
                {
                    var ext = GetFileExtension(f).TrimStart('.');
                    if (!string.IsNullOrEmpty(ext) && ext.Equals(keepExt.TrimStart('.'), 
                            StringComparison.CurrentCultureIgnoreCase))
                        subtitlesFiltered.Add(f);
                }

                subtitles = subtitlesFiltered;
            }
        }
        
        // Import to list
        MatchList.Clear();
        videos.ForEach(x => MatchList.Add(new MatchItem("", x, "", "")));
        subtitles.ForEach(x => MatchList.Add(new MatchItem("", "", x, "")));
    }

    private static string GetFileExtension(string filename)
    {
        try
        {
            var parts = filename.Split('.');
            if (parts.Length > 2) return parts[^2] + "." + parts[^1];
            if (parts.Length > 0) return parts[^1];
            return "";
        }
        catch
        {
            return "";
        }
    }
    
    private static List<string> GetExtensionTypes(List<string> filenames)
    {
        var result = new List<string>();
        foreach (var name in filenames)
        {
            var extension = GetFileExtension(name);
            if (string.IsNullOrEmpty(extension)) continue;
            result.Add(extension.ToLower());
        }

        return result.Distinct().ToList();
    }

    private FileType? GetFileTypeByExtension(string extension)
    {
        extension = extension.TrimStart('.').ToLower();
        if (GetVideoExtensions().Contains(extension)) return FileType.Video;
        if (GetSubtitleExtensions().Contains(extension)) return FileType.Subtitle;
        return null;
    }

    [RelayCommand]
    private void CopyCommands()
    {
        var command = "";
        
        foreach (var item in SelectedItems)
        {
            var subtitle = !string.IsNullOrEmpty(item.Subtitle) ? item.Subtitle : "?";
            var video = !string.IsNullOrEmpty(item.Video) ? item.Video : "?";
            command += $"mv {subtitle} {video}\n";
        }

        App.Current?.Services?.GetService<IClipboardService>()!.CopyToClipboard(command.Trim());
    }

    [RelayCommand]
    private async Task OpenFolder(CancellationToken token)
    {
        var filesService = App.Current?.Services?.GetService<IFilesService>();
        var files = await filesService!.OpenFolderAsync();
        await ImportFiles(files.Select(x => x.Path.LocalPath).ToList());
        
        await PerformMatch();
    }

    partial void OnShowRenameTasksChanged(bool value)
    {
        UpdateRenameTaskList();
    }

    private void UpdateRenameTaskList()
    {
        RenameTasks.Clear();

        foreach (var item in MatchList)
        {
            if (string.IsNullOrEmpty(item.Subtitle) || string.IsNullOrEmpty(item.Video)) continue;
            
            var alteredSubtitle = Path.GetDirectoryName(item.Subtitle)
                                  + "/"
                                  + Path.GetFileNameWithoutExtension(item.Video)
                                  + Path.GetExtension(item.Subtitle);
            
            RenameTasks.Add(new RenameTask(item.Subtitle, alteredSubtitle, "未修改"));
        }
    }

    [RelayCommand]
    private async Task PerformRename()
    {
        App.Current?.Services?.GetService<IDialogService>()!.OpenManualModeSetting();
    }

    [RelayCommand]
    private async Task PerformMatch()
    {
        var result = Matcher.Matcher.Execute(MatchList.ToList());

        foreach (var item in result) MatchItemHelper.UpdateMatchItemStatus(item);
        
        MatchList = new ObservableCollection<MatchItem>(result);
    }

    [RelayCommand]
    private async Task DropSelected(string type)
    {
        if (SelectedItems.Count == 0) return;
        foreach (var item in SelectedItems)
        {
            if (type == "video") item.Video = "";
            else if (type == "subtitle") item.Subtitle = "";

            MatchItemHelper.UpdateMatchItemStatus(item);
        }
    }
    
    [RelayCommand]
    private async Task RevealFileInFolder(string type)
    {
        if (SelectedItems.Count == 0) return;
        var item = SelectedItems[0];
        var path = type switch
        {
            "video" => item.Video,
            "subtitle" => item.Subtitle,
            _ => null
        };

        if (path == null) return;
        
        FileHelper.RevealFileInFolder(path);
    }

    [RelayCommand]
    private async Task OpenVersionLink()
    {
        if (string.IsNullOrEmpty(CurrVersionBtnLink)) return;
        
        try
        {
            await Ioc.Default.GetService<IBrowserService>()!.OpenBrowserAsync(new Uri(CurrVersionBtnLink));
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }

    [RelayCommand]
    private async Task About()
    {
        
    }
}
