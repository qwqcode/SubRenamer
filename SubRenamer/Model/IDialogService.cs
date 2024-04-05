using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace SubRenamer.Model;

public interface IDialogService
{
    public Task OpenSettings();
    public Task OpenRules();
    public Task OpenRegexModeSetting();
    public Task OpenManualModeSetting();
    public Task OpenItemEdit(MatchItem item, ObservableCollection<MatchItem> collection);
    public Task<string?> OpenConflict(List<string> options);
}