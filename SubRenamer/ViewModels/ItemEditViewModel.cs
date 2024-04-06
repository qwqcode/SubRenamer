using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using SubRenamer.Helper;
using SubRenamer.Model;

namespace SubRenamer.ViewModels;

public partial class ItemEditViewModel : ViewModelBase
{
    [ObservableProperty] private ObservableCollection<MatchItem> _matchList;
    [ObservableProperty] private MatchItem _currItem;
    [ObservableProperty] private int _currPos = -1;
    [ObservableProperty] private string _currPosText = "";
    
    public ItemEditViewModel(MatchItem target, ObservableCollection<MatchItem> collection)
    {
        MatchList = collection;
        CurrItem = target;
        CurrPos = MatchList.IndexOf(CurrItem);
    }
    
    [RelayCommand]
    private void Prev()
    {
        if (CurrPos <= 0) return;
        CurrItem = MatchList[--CurrPos];
    }

    [RelayCommand]
    private void Next()
    {
        if (CurrPos + 1 >= MatchList.Count) return;
        CurrItem = MatchList[++CurrPos];
    }

    [RelayCommand]
    private void DeleteCurrent()
    {
        if (CurrPos < 0) return;
        MatchList.RemoveAt(CurrPos);
        if (MatchList.Count > 0)
        {
            if (CurrPos >= MatchList.Count) CurrPos--;
            CurrItem = MatchList[CurrPos];
        }
        else
        {
            CreateItem();
        }
    }

    [RelayCommand]
    private void CreateItem()
    {
        MatchList.Add(new MatchItem());
        CurrPos = MatchList.Count - 1;
        CurrItem = MatchList[CurrPos];
    }

    [RelayCommand]
    private async Task OpenFile(string type)
    {
        var dialogService = App.Current?.Services?.GetService<IFilesService>()!;
        var file = await dialogService.OpenSingleFileAsync();
        if (file is null) return;
        
        var path = file.Path.LocalPath;
        
        if (type == "video") CurrItem.Video = path;
        else if (type == "subtitle") CurrItem.Subtitle = path;
        MatchItemHelper.UpdateMatchItemStatus(CurrItem);
    }
    
    [RelayCommand]
    private void DropVideo()
    {
        CurrItem.Video = "";
        MatchItemHelper.UpdateMatchItemStatus(CurrItem);
    }

    [RelayCommand]
    private void DropSubtitle()
    {
        CurrItem.Subtitle = "";
        MatchItemHelper.UpdateMatchItemStatus(CurrItem);
    }
    
    partial void OnMatchListChanged(ObservableCollection<MatchItem> value) => UpdateCurrPosText();
    partial void OnCurrPosChanged(int value) => UpdateCurrPosText();
    private void UpdateCurrPosText() => CurrPosText = $"{CurrPos+1}/{MatchList.Count}";
}