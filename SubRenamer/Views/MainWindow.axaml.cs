using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.ReactiveUI;
using ReactiveUI;
using SubRenamer.ViewModels;

namespace SubRenamer.Views
{
    public partial class MainWindow : ReactiveWindow<Window>
    {
        public MainWindow()
        {
            InitializeComponent();
        }
    }
}
