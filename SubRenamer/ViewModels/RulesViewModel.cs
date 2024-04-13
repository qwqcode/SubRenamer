using System.Collections;
using System.Threading.Tasks;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using SubRenamer.Model;
using SubRenamer.Common;

namespace SubRenamer.ViewModels;

public partial class RulesViewModel : ViewModelBase
{
    private MatchMode _matchMode = Config.Get().MatchMode;
    
    public MatchMode MatchMode
    {
        get => _matchMode;
        set
        {
            Config.Get().MatchMode = value;
            SetProperty(ref _matchMode, value);
        }
    }

    [RelayCommand]
    private async Task OpenRegexModeSetting(Window window)
    {
        window.Close();
        if (App.Current?.Services?.GetService<IDialogService>() is { } d)
            await d.OpenRegexModeSetting();
    }

    [RelayCommand]
    private async Task OpenManualModeSetting(Window window)
    {
        window.Close();
        if (App.Current?.Services?.GetService<IDialogService>() is { } d)
            await d.OpenManualModeSetting();
    }
}