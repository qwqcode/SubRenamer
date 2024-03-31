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
        var dialog = new SettingsWindow();
        dialog.DataContext = new SettingsWindowViewModel();
        var result = await dialog.ShowDialog<SettingsWindowViewModel?>(_target);
    }
}