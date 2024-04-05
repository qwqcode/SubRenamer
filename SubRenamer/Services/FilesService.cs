using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Platform.Storage;
using static SubRenamer.Common.Constants;
using SubRenamer.Model;

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
    
    public async Task<IReadOnlyList<IStorageFile>> OpenFilesAsync()
    {
        var files = await _target.StorageProvider.OpenFilePickerAsync(new FilePickerOpenOptions()
        {
            Title = "选择并导入文件",
            AllowMultiple = true,
            FileTypeFilter = new []{ VideosAndSubtitles },
        });

        return files;
    }

    public async Task<IReadOnlyList<IStorageFile>> OpenFolderAsync()
    {
        var folders = await _target.StorageProvider.OpenFolderPickerAsync(new FolderPickerOpenOptions()
        {
            Title = "导入文件夹",
            AllowMultiple = true,
        });

        var files = new List<IStorageFile>();
        foreach (var folder in folders)
        {
            await foreach (var v in folder.GetItemsAsync())
            {
                if (v is IStorageFile file) files.Add(file);
            }
        }
        
        return files;
    }

    public async Task<IStorageFile?> OpenSingleFileAsync()
    {
        var files = await _target.StorageProvider.OpenFilePickerAsync(new FilePickerOpenOptions()
        {
            Title = "选择并打开文件",
            AllowMultiple = false
        });

        return files.Count == 0 ? null : files[0];
    }


    public async Task<IStorageFile?> SaveFileAsync()
    {
        return await _target.StorageProvider.SaveFilePickerAsync(new FilePickerSaveOptions()
        {
            Title = "Save Text File"
        });
    }
}