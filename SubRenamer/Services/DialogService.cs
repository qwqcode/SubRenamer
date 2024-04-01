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

    public async Task OpenItemEdit(MatchItem item)
    {
        var dialog = new ItemEditWindow
        {
            DataContext = new ItemEditWindowViewModel(item)
        };
        var result = await dialog.ShowDialog<ItemEditWindowViewModel?>(_target);
    }
}