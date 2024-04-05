using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Avalonia.Controls;
using SubRenamer.Model;
using SubRenamer.ViewModels;
using SubRenamer.Views;

namespace SubRenamer.Services;

public class DialogService : IDialogService
{
    private readonly Window _target;

    public DialogService(Window target)
    {
        _target = target;
    }

    public async Task OpenSettings()
    {
        var dialog = new SettingsWindow
        {
            DataContext = new SettingsWindowViewModel()
        };
        var result = await dialog.ShowDialog<SettingsWindowViewModel?>(_target);
    }
    
    public async Task OpenRules()
    {
        var dialog = new RulesWindow
        {
            DataContext = new RulesWindowViewModel()
        };
        var result = await dialog.ShowDialog<RulesWindowViewModel?>(_target);
    }

    public async Task OpenItemEdit(MatchItem item, ObservableCollection<MatchItem> collection)
    {
        var dialog = new ItemEditWindow
        {
            DataContext = new ItemEditWindowViewModel(item, collection)
        };
        var result = await dialog.ShowDialog<ItemEditWindowViewModel?>(_target);
    }

    public async Task<string?> OpenConflict(List<string> options)
    {
        var store = new ConflictWindowViewModel([..options, "全部保留"]);
        var dialog = new ConflictWindow
        {
            DataContext = store
        };
        await dialog.ShowDialog(_target);
        var selected = store.SelectedItem;
        return selected == "全部保留" ? null : selected;
    }
    
    public async Task OpenRegexModeSetting()
    {
        var dialog = new RegexModeSettingWindow
        {
            DataContext = new RegexModeSettingWindowViewModel()
        };
        await dialog.ShowDialog(_target);
    }
    
    public async Task OpenManualModeSetting()
    {
        var dialog = new ManualModeSettingWindow
        {
            DataContext = new ManualModeSettingWindowViewModel()
        };
        await dialog.ShowDialog(_target);
    }
}