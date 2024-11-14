using System;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Threading;
using MsBox.Avalonia;
using MsBox.Avalonia.Dto;
using MsBox.Avalonia.Enums;
using SubRenamer.ViewModels;
using SubRenamer.Views;

namespace SubRenamer.Helper;

public static class MessageBoxHelper
{
    public static void ShowError(string text)
    {
        Dispatcher.UIThread.Post(() =>
        {
            var box = MessageBoxManager.GetMessageBoxStandard(new MessageBoxStandardParams
            {
                ButtonDefinitions = ButtonEnum.Ok,
                ContentTitle = "Error",
                ContentMessage = text,
                Icon = Icon.Error,
                MaxWidth = 500,
                MaxHeight = 800,
                Topmost = true,
                WindowStartupLocation = WindowStartupLocation.CenterScreen
            });
            box.ShowAsync();
        });
    }

    public static ProgressViewModel ShowProgress(Window target, string title, string desc, Action? onAbort = null)
    {
        var store = new ProgressViewModel(title, desc);
        if (onAbort != null) store.OnAbort += onAbort;
        var dialog = new ProgressWindow()
        {
            DataContext = store
        };
        dialog.Show(target);
        return store;
    }
}