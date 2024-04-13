using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;

namespace SubRenamer.Views;

public partial class ManualRuleWindow : Window
{
    public ManualRuleWindow()
    {
        InitializeComponent();
    }

    private void CancelBtn_OnClick(object? sender, RoutedEventArgs e)
    {
        Close();
    }
}