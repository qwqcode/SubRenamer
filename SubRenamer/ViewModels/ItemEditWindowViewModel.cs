using CommunityToolkit.Mvvm.ComponentModel;
using SubRenamer.Model;

namespace SubRenamer.ViewModels;

public partial class ItemEditWindowViewModel : ViewModelBase
{
    [ObservableProperty]
    private MatchItem _matchItem;
    
    public ItemEditWindowViewModel(MatchItem target)
    {
        MatchItem = target;
    }
}