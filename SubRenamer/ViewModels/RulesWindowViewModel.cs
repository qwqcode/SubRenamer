using System.Collections;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SubRenamer.Model;

namespace SubRenamer.ViewModels;

public partial class RulesWindowViewModel : ViewModelBase
{
    [ObservableProperty]
    private EMatchMode _matchMode = EMatchMode.Diff;
}