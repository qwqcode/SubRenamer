using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace SubRenamer.Model;

public interface IRenameService
{
    void UpdateRenameTaskList(IReadOnlyList<MatchItem> matchList, Collection<RenameTask> destList);
    void ExecuteRename(IReadOnlyList<RenameTask> taskList);
    string GenerateRenameCommands(IReadOnlyList<MatchItem> list);
}