using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace SubRenamer.Model;

public interface IRenameService
{
    void UpdateRenameTaskList(IEnumerable<MatchItem> matchList, Collection<RenameTask> destList);
    void ExecuteRename(IEnumerable<RenameTask> taskList);
    string GenerateRenameCommands(IEnumerable<MatchItem> list);
}