using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Avalonia.Platform.Storage;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DynamicData;
using Microsoft.Extensions.DependencyInjection;
using SubRenamer.Common;
using SubRenamer.Helper;
using SubRenamer.Matcher;
using SubRenamer.Model;
using SubRenamer.Services;
using MatchItem = SubRenamer.Model.MatchItem;

namespace SubRenamer.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    [ObservableProperty] private ObservableCollection<MatchItem> _matchList = [];
    [ObservableProperty] private Collection<MatchItem> _selectedItems = [];
    [ObservableProperty] private ObservableCollection<RenameTask> _renameTasks = [];
    [ObservableProperty] private bool _showRenameTasks;
    [ObservableProperty] private bool _winTopmost;

    #region Services
    private static IDialogService GetDialogService() => App.Current!.Services!.GetService<IDialogService>()!;
    private static IFilesService GetFilesService() => App.Current!.Services!.GetService<IFilesService>()!;
    private static IRenameService GetRenameService() => App.Current!.Services!.GetService<IRenameService>()!;
    private static IImportService GetImportService() => App.Current!.Services!.GetService<IImportService>()!;
    #endregion
    
    public MainViewModel()
    {
        SyncCurrentStatusText();
    }

    #region TableCurd
    /**
     * Delete selected Items
     */
    [RelayCommand]
    private void DeleteItem() => MatchList.RemoveMany(SelectedItems);

    /**
     * Clear entire Table
     */
    [RelayCommand]
    private void ClearAll() {
        MatchList.Clear();
        RenameTasks.Clear();
        ShowRenameTasks = false;
    }

    /**
     * Edit an Item
     */
    [RelayCommand]
    private void EditItem() => _ = SelectedItems.FirstOrDefault(item =>
    {
        GetDialogService().OpenItemEdit(item, MatchList);
        return false;
    });
    #endregion
    
    #region Import
    /**
     * Open a file
     */
    [RelayCommand]
    private async Task OpenFile() =>
        await Import(await GetFilesService().OpenFilesAsync());
    
    /**
     * Open a folder
     */
    [RelayCommand]
    private async Task OpenFolder(CancellationToken token) =>
        await Import(await GetFilesService().OpenFolderAsync());

    /**
     * Import from storage file list
     */
    public async Task Import(IReadOnlyList<IStorageFile> files)
    {
        if (files.Count == 0) return;
        var fileNames = files.Select<IStorageFile, string>(x => x.Path.LocalPath).ToList();
        await ImportFromFileNames(fileNames);
    }
    
    /**
     * Import from file name list
     */
    public async Task ImportFromFileNames(List<string> fileNames)
    {
        if (fileNames.Count == 0) return;

        try
        {
            await GetImportService().ImportMultipleFiles(
                fileNames, MatchList, async options =>
                    await GetDialogService().OpenConflict(options));
        }
        catch (UserCancelDialogException)
        {
            return;
        }

        PerformMatch();
    }
    #endregion
    
    #region Rename
    /**
     * Update when preview button clicked
     */
    partial void OnShowRenameTasksChanged(bool value)
    {
        GetRenameService().UpdateRenameTaskList(MatchList, RenameTasks);
    }

    /**
     * Perform Rename Task
     */
    [RelayCommand]
    private void PerformRename()
    {
        ShowRenameTasks = true;
        GetRenameService().ExecuteRename(RenameTasks);
    }
    
    #endregion
    
    #region Match
    /**
     * Perform Match
     */
    [RelayCommand]
    private void PerformMatch()
    {
        ShowRenameTasks = false;
        var inputItems = MatcherDataConverter.ConvertMatchItems(MatchList);
        var m = Config.Get().MatchMode;
        var resultRaw = Matcher.Matcher.Execute(inputItems, new MatcherOptions()
        {
            // Convert Config to MatcherOptions
            VideoRegex = (m != MatchMode.Diff) ? (m == MatchMode.Manual ? Config.Get().ManualVideoRegex : Config.Get().VideoRegex) : null,
            SubtitleRegex = (m != MatchMode.Diff) ? (m == MatchMode.Manual ? Config.Get().ManualSubtitle : Config.Get().SubtitleRegex) : null,
        });
        var result =  MatcherDataConverter.ConvertMatchItems(resultRaw);
        result.ForEach(UpdateMatchItemStatus);
        MatchList = new ObservableCollection<MatchItem>(result);
    }

    /**
     * Update the Status text of a MatchItem
     */
    private static void UpdateMatchItemStatus(MatchItem item) => MatchItemHelper.UpdateMatchItemStatus(item);
    #endregion
    
    #region TableRightClick
    /**
     * Drop content
     */
    [RelayCommand]
    private void DropItemsField(FileType type)
    {
        foreach (var item in SelectedItems)
        {
            if (type == FileType.Video) item.Video = "";
            else if (type == FileType.Subtitle) item.Subtitle = "";
            UpdateMatchItemStatus(item);
        }
    }
    
    /**
     * Copy the rename commands
     */
    [RelayCommand]
    private void CopyCommands()
    {
        App.Current!.Services!.GetService<IClipboardService>()!.CopyToClipboard(
            GetRenameService().GenerateRenameCommands(MatchList));
    }
    
    /**
     * Reveal file in folder
     */
    [RelayCommand]
    private void RevealFileInFolder(string type) =>
        _ = SelectedItems.FirstOrDefault(item =>
        {
            FileHelper.RevealFileInFolder(type switch
            {
                "video" => item.Video,
                "subtitle" => item.Subtitle,
                _ => ""
            });
            return false;
        });
    #endregion
    
    #region MenuBar
    [ObservableProperty] private string _currMatchModeText = "";
    [ObservableProperty] private string _currVersionText = $"v{Config.AppVersion}";
    [ObservableProperty] private string _currVersionBtnLink = "https://github.com/qwqcode/SubRenamer";
    
    /**
     * Sync current status text
     */
    public void SyncCurrentStatusText() =>
        CurrMatchModeText = Config.Get().MatchMode switch
        {
            MatchMode.Diff => "自动匹配",
            MatchMode.Manual => "手动匹配",
            MatchMode.Regex => "正则匹配",
            _ => ""
        };
    
    /**
     * Open version link
     */
    [RelayCommand]
    private void OpenVersionLink() =>
        BrowserHelper.OpenBrowserAsync(CurrVersionBtnLink);
    #endregion
    
    #region Dialogs

    /**
    * Open Settings dialog
    */
    [RelayCommand]
    private void OpenSettings() => GetDialogService().OpenSettings();
    
    /**
     * Open Rules dialog
     */
    [RelayCommand]
    private void OpenRules() => GetDialogService().OpenRules();

    /**
     * Toggle Window Topmost
     */
    [RelayCommand]
    private void ToggleTopmost()
    {
        WinTopmost = !WinTopmost;
        App.Current?.Services?.GetService<IWindowService>()?.SetTopmost(WinTopmost);
    }
    #endregion
}
