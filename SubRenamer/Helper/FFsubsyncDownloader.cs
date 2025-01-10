using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;

namespace SubRenamer.Helper;

public class FFsubsyncDownloader
{
    public static async Task Download(Window parentWindow)
    {
        var path = Path.Combine(Config.ConfigDir, "ffsubsync_bin");
        var url =
            $"https://github.com/qwqcode/ffsubsync-bin/releases/latest/download/ffsubsync_bin_{SystemInfo.GetOSArchPair().Replace("windows", "win")}";
        var dialog = MessageBoxHelper.ShowProgress(parentWindow,
            Application.Current.GetResource<string>("App.Strings.SubSyncBinDownloadTitle") ?? "",
            Application.Current.GetResource<string>("App.Strings.SubSyncBinDownloadDesc") ?? "");

        var tokenSource = new CancellationTokenSource();
        dialog.OnAbort += () => tokenSource.Cancel();

        try
        {
            await DownloadHelper.DownloadFileAsync(url, path, (progress, downloadedSize, totalSize) =>
                {
                    dialog.Update(progress, progress >= 100);
                    dialog.Desc = $"{Application.Current.GetResource<string>("App.Strings.SubSyncBinDownloadDesc")} [{downloadedSize}/{totalSize}]";
                },
                tokenSource.Token);

            // add execute permission for unix
            if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                File.SetUnixFileMode(path, UnixFileMode.UserExecute | UnixFileMode.UserRead | UnixFileMode.UserWrite);
            }
        }
        catch (TaskCanceledException)
        {
            throw new TaskCanceledException("Download canceled");
        }
        catch (Exception e)
        {
            MessageBoxHelper.ShowError($"Failed to download ffsubsync binary: {e.Message}");
            throw;
        }

        dialog.Desc = $"{Application.Current.GetResource<string>("App.Strings.SubSyncBinDownloadDone")}";
    }
}