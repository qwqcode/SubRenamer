using System;
using CommunityToolkit.Mvvm.ComponentModel;

namespace SubRenamer.ViewModels;

public partial class TerminalViewModel : ViewModelBase
{
    [ObservableProperty]
    private string _terminalText = "";
    
    public void WriteLine(string text)
    {
        TerminalText += text + Environment.NewLine;
    }
}