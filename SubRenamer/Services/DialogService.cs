using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Avalonia.Controls;
using SubRenamer.Model;
using SubRenamer.ViewModels;
using SubRenamer.Views;

namespace SubRenamer.Services;

public class UserCancelDialogException: Exception {}

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
        dialog.Closed += (sender, args) => Config.Save();
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
        var cancel = false;
        dialog.Closing += (object? sender, WindowClosingEventArgs e) =>
        {
            if (!e.IsProgrammatic) cancel = true;
        };
        await dialog.ShowDialog(_target);
        if (cancel) throw new UserCancelDialogException();
        var selected = store.GetResult();
        //选择全部保留时自动勾选保留语言后缀选项
        var config = Config.Get(); 
        if (selected == "全部保留") config.KeepLangExt = true;
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