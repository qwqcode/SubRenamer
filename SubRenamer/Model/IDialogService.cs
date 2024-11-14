using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using SubRenamer.ViewModels;

namespace SubRenamer.Model;

public interface IDialogService
{
    public Task OpenSettings();
    public Task OpenRules();
    public Task OpenRegexModeSetting();
    public Task OpenManualModeSetting();
    public Task OpenItemEdit(MatchItem item, ObservableCollection<MatchItem> collection);
    public Task<string?> OpenConflict(List<string> options);
    public Task<ProgressViewModel> OpenProgressDialog(string title, string progressText, Action onAbort);
}