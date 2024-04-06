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
            DataContext = new SettingsViewModel()
        };
        var result = await dialog.ShowDialog<SettingsViewModel?>(_target);
    }
    
    public async Task OpenRules()
    {
        var dialog = new RulesWindow
        {
            DataContext = new RulesViewModel()
        };
        var result = await dialog.ShowDialog<RulesViewModel?>(_target);
    }

    public async Task OpenItemEdit(MatchItem item, ObservableCollection<MatchItem> collection)
    {
        var dialog = new ItemEditWindow
        {
            DataContext = new ItemEditViewModel(item, collection)
        };
        var result = await dialog.ShowDialog<ItemEditViewModel?>(_target);
    }

    public async Task<string?> OpenConflict(List<string> options)
    {
        var store = new ConflictViewModel([..options, "全部保留"]);
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
        var dialog = new RegexRuleWindow
        {
            DataContext = new RegexRuleViewModel()
        };
        await dialog.ShowDialog(_target);
    }
    
    public async Task OpenManualModeSetting()
    {
        var dialog = new ManualRuleWindow
        {
            DataContext = new ManualRuleViewModel()
        };
        await dialog.ShowDialog(_target);
    }
}