using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;

namespace SubRenamer.Views;

public partial class ManualModeSettingWindow : Window
{
    public ManualModeSettingWindow()
    {
        InitializeComponent();
    }

    private void CancelBtn_OnClick(object? sender, RoutedEventArgs e)
    {
        Close();
    }
}