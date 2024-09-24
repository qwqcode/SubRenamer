using System.IO;
using Avalonia;
using CommunityToolkit.Mvvm.ComponentModel;
using SubRenamer.Helper;

namespace SubRenamer.Model;

public partial class RenameTask(string origin = "", string alter = "") : ObservableObject
{
    /**
     * Original Full Path
     */
    [ObservableProperty] private string _origin = origin;

    [ObservableProperty] private string _originName = Path.GetFileName(origin);

    partial void OnOriginChanged(string value) => OriginName = Path.GetFileName(value);

    /**
     * Alter to Full Path
     */
    [ObservableProperty] private string _alter = alter;

    [ObservableProperty] private string _alterName = Path.GetFileName(alter);

    partial void OnAlterChanged(string value) => AlterName = Path.GetFileName(value);

    /**
     * Current Status
     */
    [ObservableProperty] private RenameTaskStatus? _status;

    /**
     * Current Status Text
     */
    [ObservableProperty] private string _statusText = "";

    partial void OnStatusChanged(RenameTaskStatus? value)
    {
        StatusText = GetStatusText();
    }

    public string GetStatusText()
    {
        return Status switch
        {
            RenameTaskStatus.Altered => Application.Current.GetResource<string>("App.Strings.RenameTaskStatusAltered") ?? "Altered",
            RenameTaskStatus.Ready => Application.Current.GetResource<string>("App.Strings.RenameTaskStatusReady") ?? "Ready",
            RenameTaskStatus.Failed => $"{Application.Current.GetResource<string>("App.Strings.RenameTaskStatusFailed") ?? "Failed"} - {ErrorMessage}",
            RenameTaskStatus.NoNeed => Application.Current.GetResource<string>("App.Strings.RenameTaskStatusNoNeed") ?? "NoNeed",
            _ => ""
        };
    }

    /**
     * Error Message
     */
    [ObservableProperty] private string _errorMessage = "";

    /**
     * Associated MatchItem
     */
    [ObservableProperty] private MatchItem? _matchItem;
}