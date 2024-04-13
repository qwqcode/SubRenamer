using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Platform.Storage;
using Avalonia.ReactiveUI;
using SubRenamer.Helper;
using SubRenamer.Model;
using SubRenamer.ViewModels;

namespace SubRenamer.Views;

public partial class MainWindow : ReactiveWindow<Window>
{
    public MainWindow()
    {
        InitializeComponent();

        Activated += OnActivated;
        AddHandler(DragDrop.DropEvent, OnDrop);
    }

    private async void OnDrop(object? sender, DragEventArgs e)
    {
        if (!e.Data.Contains(DataFormats.Files)) return;
        
        var items = e.Data.GetFiles() ?? Array.Empty<IStorageItem>();
        var files = new List<IStorageFile>();
        
        foreach (var item in items)
        {
            if (item is IStorageFile file)
                files.Add(file);
            else if (item is IStorageFolder folder)
                files.AddRange(await FileHelper.ConvertFoldersToFilesAsync(new []{ folder }));
        }

        if (DataContext is MainViewModel store) _ = store.Import(files);
    }

    private void OnActivated(object? sender, EventArgs args)
    {
        if (DataContext is MainViewModel store) store.SyncCurrentStatusText();
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
