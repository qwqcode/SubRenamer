using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SubRenamer.Services;
using Microsoft.Extensions.DependencyInjection;
using SubRenamer.Model;
using SubRenamer.Views;

namespace SubRenamer.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    [ObservableProperty] private ObservableCollection<MatchItem> _matchList;
    [ObservableProperty] private Collection<MatchItem> _selectedItems = [];
    
    public MainWindowViewModel()
    {
        _matchList = new ObservableCollection<MatchItem> 
        {
            new ("01", "test_video_01.mp4", "test_subtitle_01.ass", "已匹配"),
            new ("02", "test_video_02.mp4", "test_subtitle_02.ass", "已匹配"),
            new ("03", "test_video_03.mp4", "test_subtitle_03.ass", "已匹配"),
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
        await dialogService.OpenItemEdit(SelectedItems.First());
    }

    [RelayCommand]
    private async Task OpenFile(CancellationToken token)
    {
        ErrorMessages?.Clear();
        try
        {
            var filesService = App.Current?.Services?.GetService<IFilesService>();
            if (filesService is null) throw new NullReferenceException("Missing File Service instance.");

            var files = await filesService.OpenFilesAsync();
            
            foreach (var f in files)
            {
                MatchList.Add(new MatchItem("01", f.Name, "", "已匹配"));
            }
        }
        catch (Exception e)
        {
            ErrorMessages?.Add(e.Message);
        }
    }
}
