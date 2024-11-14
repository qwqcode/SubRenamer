using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Platform.Storage;
using SubRenamer.Helper;
using static SubRenamer.Common.Constants;
using SubRenamer.Model;
using Avalonia;

namespace SubRenamer.Services;

public class FilesService : IFilesService
{
    private readonly Window _target;

    public FilesService(Window target)
    {
        _target = target;
    }

    public static FilePickerFileType VideosAndSubtitles { get; } = new("Videos and Subtitles")
    {
        Patterns = GetVideoExtensions().Concat(GetSubtitleExtensions()).Select(x => $"*.{x}").ToArray(),
    };

    public Task<IReadOnlyList<IStorageFile>> OpenFilesAsync() => OpenFilesAsync([]);
    
    public async Task<IReadOnlyList<IStorageFile>> OpenFilesAsync(FilePickerFileType[] fileTypes)
    {
        var files = await _target.StorageProvider.OpenFilePickerAsync(new FilePickerOpenOptions()
        {
            Title = Application.Current.GetResource<string>("App.Strings.OpenFileDialogTitle"),
            AllowMultiple = true,
            FileTypeFilter = fileTypes,
        });

        return files;
    }

    public async Task<IReadOnlyList<IStorageFile>> OpenFolderAsync()
    {
        var folders = await _target.StorageProvider.OpenFolderPickerAsync(new FolderPickerOpenOptions()
        {
            Title = Application.Current.GetResource<string>("App.Strings.OpenFolderDialogTitle"),
            AllowMultiple = true,
        });
        
        return await FileHelper.ConvertFoldersToFilesAsync(folders);
    }

    public async Task<IStorageFile?> OpenSingleFileAsync()
    {
        var files = await _target.StorageProvider.OpenFilePickerAsync(new FilePickerOpenOptions()
        {
            Title = Application.Current.GetResource<string>("App.Strings.OpenFileDialogTitle"),
            AllowMultiple = false
        });

        return files.Count == 0 ? null : files[0];
    }


    public async Task<IStorageFile?> SaveFileAsync()
    {
        return await _target.StorageProvider.SaveFilePickerAsync(new FilePickerSaveOptions()
        {
            Title = Application.Current.GetResource<string>("App.Strings.SaveFileDialogTitle")
        });
    }
}