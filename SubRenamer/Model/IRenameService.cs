using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace SubRenamer.Model;

public interface IRenameService
{
    void UpdateRenameTaskList(IReadOnlyList<MatchItem> matchList, Collection<RenameTask> destList);
    Task ExecuteRename(IReadOnlyList<RenameTask> taskList);
    string GenerateRenameCommands(IReadOnlyList<MatchItem> list);
}