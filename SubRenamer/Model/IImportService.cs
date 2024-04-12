using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SubRenamer.Model;

public interface IImportService
{
    Task ImportMultipleFiles(List<string> files, ICollection<MatchItem> dataSource, Func<List<string>, Task<string?>> onOpenSolveConflictDialog);
}