using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;

namespace SubRenamer.Views;

public partial class ConflictWindow : Window
{
    public ConflictWindow()
    {
        InitializeComponent();
    }

    private void ConfirmBtn_OnClick(object? sender, RoutedEventArgs e)
    {
        Close();
    }
}