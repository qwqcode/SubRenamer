using System;
using System.Collections.ObjectModel;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.ReactiveUI;
using Microsoft.VisualBasic;
using SubRenamer.Model;
using SubRenamer.ViewModels;

namespace SubRenamer.Views
{
    public partial class MainWindow : ReactiveWindow<Window>
    {
        public MainWindow()
        {
            InitializeComponent();

            Activated += (sender, args) =>
            {
                if (DataContext is MainViewModel store) store.SyncStatusText();
            };
        }

        private void DataGrid_OnSelectionChanged(object? sender, SelectionChangedEventArgs e)
        {
            var items = new Collection<MatchItem>();
            
            foreach (var el in DataGrid.SelectedItems)
            {
                if (el is MatchItem matchItem) items.Add(matchItem);
            }
            
            if (DataContext is MainViewModel store)
                store.SelectedItems = items;
        }

        private void SelectAllMenuItem_OnClick(object? sender, RoutedEventArgs e)
        {
            DataGrid.SelectAll();
        }
    }
}
