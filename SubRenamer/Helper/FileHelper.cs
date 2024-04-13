using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Avalonia.Platform.Storage;

namespace SubRenamer.Helper;

public static class FileHelper
{
    public static void RenameFile(string originPath, string alterPath)
    {
        File.Move(originPath, alterPath);
    }
    
    public static void BackupFile(string path, string folderName = "SubtitleBackup")
    {
        // create new 'SubtitleBackup' directory if not exists
        var backupDir = Path.Combine(Path.GetDirectoryName(path) ?? "", folderName);
        if (!string.IsNullOrEmpty(backupDir) && !Directory.Exists(backupDir))
            Directory.CreateDirectory(backupDir);
                    
        // backup original file to 'SubtitleBackup' directory
        var backupFile = Path.Combine(backupDir, Path.GetFileName(path));
        // if file is already exists, then rename it with timestamp
        if (File.Exists(backupFile))
            backupFile = Path.Combine(backupDir, $"{Path.GetFileNameWithoutExtension(path)}_{DateTime.Now:yyyyMMddHHmmss}{Path.GetExtension(path)}");
        File.Copy(path, backupFile);
    }
    
    public static void RevealFileInFolder(string path)
    {
        if (string.IsNullOrEmpty(path)) return;
        if (!File.Exists(path)) return;
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        { 
            string args = $"/select, \"{path}\"";
            Process.Start("explorer.exe", args);
        }
        else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
        {
            try {
                Process.Start("dbus-send", $"--print-reply --dest=org.freedesktop.FileManager1 /org/freedesktop/FileManager1 org.freedesktop.FileManager1.ShowItems array:string:\"{path}\" string:\"\"");
            } catch {
                Process.Start("xdg-open", $"\"{Path.GetDirectoryName(path)}\"");
            }
        }
        else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
        {
            Process.Start("open", $"-R \"{path}\"");
        }
    }

    public static async Task<IReadOnlyList<IStorageFile>> ConvertFoldersToFilesAsync(IReadOnlyList<IStorageFolder> folders)
    {
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
}