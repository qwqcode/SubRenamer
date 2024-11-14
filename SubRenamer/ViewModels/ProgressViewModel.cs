using System;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace SubRenamer.ViewModels;

public partial class ProgressViewModel(string title, string desc) : ViewModelBase
{
    [ObservableProperty] private string _title = title;
    [ObservableProperty] private string _desc = desc;
    [ObservableProperty] private int _progress;
    [ObservableProperty] private bool _isDone;
    [ObservableProperty] private string _progressText = "0%";

    partial void OnProgressChanged(int value)
        => ProgressText = $"{value}%";

    public event Action? OnAbort;
    
    public void Update(int value, bool isDone)
    {
        Progress = value;
        IsDone = isDone;
    }
    
    [RelayCommand]
    public void Abort(Window? window)
    {
        OnAbort?.Invoke();
        window?.Close();
    }
    
    [RelayCommand]
    public void Done(Window? window)
    {
        window?.Close();
    }
}