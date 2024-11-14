using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace SubRenamer.Views;

public partial class TerminalWindow : Window
{
    public TerminalWindow()
    {
        InitializeComponent();
    }

    private void TextBox_OnTextChanged(object? sender, TextChangedEventArgs e)
    {
        ScrollViewer.ScrollToEnd();
    }
}