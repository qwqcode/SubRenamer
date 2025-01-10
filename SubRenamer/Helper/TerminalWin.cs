using System;
using SubRenamer.ViewModels;
using SubRenamer.Views;

namespace SubRenamer.Helper;

public class TerminalWin
{
    private TerminalViewModel? _terminalViewModel;
    private TerminalWindow? _terminalWindow;
    public event Action? OnClosed;

    public void Log(string? text)
    {
        if (_terminalWindow == null) Init();
        _terminalViewModel?.WriteLine(text ?? "");
    }

    private void Init()
    {
        _terminalViewModel = new TerminalViewModel();
        _terminalWindow = new TerminalWindow
        {
            DataContext = _terminalViewModel
        };
        _terminalWindow.Closed += (sender, args) =>
        {
            _terminalWindow = null;
            _terminalViewModel = null;
            OnClosed?.Invoke();
        };
        _terminalWindow.Show();
    }
}