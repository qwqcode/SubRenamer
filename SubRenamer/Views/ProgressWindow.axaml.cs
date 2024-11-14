using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;

namespace SubRenamer.Views;

public partial class ProgressWindow : Window
{
    public ProgressWindow()
    {
        InitializeComponent();
        
        Closed += (sender, args) =>
        {
            if (DataContext is not ViewModels.ProgressViewModel vm) return;
            
            if (vm.IsDone) vm.Done(this);
            else vm.Abort(this);
        };
    }
}
