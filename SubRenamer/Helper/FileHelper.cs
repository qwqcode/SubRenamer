using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;

namespace SubRenamer.Helper;

public static class FileHelper
{
    public static void RevealFileInFolder(string path)
    {
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
}