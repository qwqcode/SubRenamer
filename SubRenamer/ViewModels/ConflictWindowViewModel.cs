using System.Collections.Generic;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;

namespace SubRenamer.ViewModels;

public partial class ConflictWindowViewModel(List<string> options) : ViewModelBase
{
    [ObservableProperty] private ObservableCollection<string> _options = new ObservableCollection<string>(options);
    [ObservableProperty] private string _selectedItem = "";
}