using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Avalonia.Controls;
using SubRenamer.Common;
using SubRenamer.Model;

namespace SubRenamer.Services;

public class ImportService(Window target) : IImportService
{
    private readonly Window _target = target;

    public async Task ImportMultipleFiles(
        List<string> files,
        ICollection<MatchItem> dataSource,
        Func<List<string>, Task<string?>> onOpenSolveConflictDialog
    )
    {
        // Copy
        files = files.ToList();
        
        // merge original exists files with MatchList
        dataSource.ToList().ForEach(x =>
        {
            files.Add(x.Video);
            files.Add(x.Subtitle);
        });
        
        // distinct and remove empty
        files = files.Distinct().Where(x => !string.IsNullOrEmpty(x)).ToList();
        
        // Group files by type
        var (videos, subtitles) = FilesGroupByType(files);
        
        // Solve subtitle conflict
        subtitles = await SolveSubtitleConflict(subtitles, onOpenSolveConflictDialog);
        
        // Import to list
        dataSource.Clear();
        videos.ForEach(x => dataSource.Add(new MatchItem("", x, "", "")));
        subtitles.ForEach(x => dataSource.Add(new MatchItem("", "", x, "")));
    }

    private static async Task<List<string>> SolveSubtitleConflict(
        List<string> subtitles,
        Func<List<string>, Task<string?>> onOpenDialog
    )
    {
        List<string> subtitleFilenames = subtitles
            .Select(Path.GetFileName)
            .Where(x => !string.IsNullOrEmpty(x))
            .ToList()!;
        
        var subtitleExtTypes = GetExtensionTypes(subtitleFilenames);
        if (subtitleExtTypes.Count == 0) return subtitles;
        
        var keepExt = await onOpenDialog(subtitleExtTypes);
        
        // Filter subtitle file by user selection
        if (keepExt == null) return subtitles;
        
        var subtitlesFiltered = new List<string>();

        foreach (var f in subtitles)
        {
            var ext = GetFileExtension(f).TrimStart('.');
            if (!string.IsNullOrEmpty(ext) && ext.Equals(keepExt.TrimStart('.'), 
                    StringComparison.CurrentCultureIgnoreCase))
                subtitlesFiltered.Add(f);
        }

        return subtitlesFiltered;
    }

    private static (List<string> videos, List<string> subtitles) FilesGroupByType(IEnumerable<string> fileNames)
    {
        List<string> videos = [];
        List<string> subtitles = [];
        
        foreach (var f in fileNames)
        {
            var type = GetFileTypeByExtension(Path.GetExtension(f));
            var list = type switch
            {
                FileType.Video => videos,
                FileType.Subtitle => subtitles,
                _ => null
            };
            list?.Add(f);
        }

        return (videos, subtitles);
    }
    
    private static List<string> GetExtensionTypes(List<string> filenames)
    {
        var result = new List<string>();
        foreach (var name in filenames)
        {
            var extension = GetFileExtension(name);
            if (string.IsNullOrEmpty(extension)) continue;
            result.Add(extension.ToLower());
        }

        return result.Distinct().ToList();
    }
    
    private static string GetFileExtension(string filename)
    {
        try
        {
            var parts = filename.Split('.');
            if (parts.Length > 2) return parts[^2] + "." + parts[^1];
            if (parts.Length > 0) return parts[^1];
            return "";
        }
        catch
        {
            return "";
        }
    }
    
    private static FileType? GetFileTypeByExtension(string extension)
    {
        extension = extension.TrimStart('.').ToLower();
        if (Constants.GetVideoExtensions().Contains(extension)) return FileType.Video;
        if (Constants.GetSubtitleExtensions().Contains(extension)) return FileType.Subtitle;
        return null;
    }
}