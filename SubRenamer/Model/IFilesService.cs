using System.Collections.Generic;
using System.Threading.Tasks;
using Avalonia.Platform.Storage;

namespace SubRenamer.Model;

public interface IFilesService
{
    public Task<IReadOnlyList<IStorageFile>> OpenFilesAsync();

    public Task<IStorageFile?> OpenSingleFileAsync();

    public Task<IReadOnlyList<IStorageFile>> OpenFolderAsync();
    
    public Task<IStorageFile?> SaveFileAsync();
}