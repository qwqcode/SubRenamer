using System.IO;
using CommunityToolkit.Mvvm.ComponentModel;

namespace SubRenamer.Model;

public partial class RenameTask(string origin = "", string alter = "", string status = "") : ObservableObject
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
    [ObservableProperty] private string _status = status;
}