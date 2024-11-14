using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using SubRenamer.Helper;
using SubRenamer.Model;

namespace SubRenamer.Services;

public class SubSyncService(Window target) : ISubSyncService
{
    public class BinaryNotFoundException() : Exception("FFsubsync binary not found");

    private readonly Window _target = target;
    private ExternalProgram? _externalProgram;
    private bool _isBootstrapped = false;
    private string? _exePath = "";
    public event Action? OnBootstrapped;
    public event Action? OnShutdown;

    public bool GetIsAvailable() => _externalProgram != null;
    public bool GetIsBootstrapped() => _isBootstrapped;
    public string? GetExePath() => _exePath;

    public string RetrieveExePath()
    {
        var pathsForCheck = new[]
        {
            Path.Combine(AppContext.BaseDirectory, "ffsubsync_bin"),
            Path.Combine(AppContext.BaseDirectory, "ffsubsync_bin.exe"),
            Path.Combine(Config.ConfigDir, "ffsubsync_bin"),
            Path.Combine(Config.ConfigDir, "ffsubsync_bin.exe"),
        };
        return pathsForCheck.FirstOrDefault(File.Exists, "");
    }

    public async Task Bootstrap()
    {
        var exePath = RetrieveExePath();
        if (string.IsNullOrEmpty(exePath))
        {
            Console.WriteLine("FFsubsync binary not found");
            throw new BinaryNotFoundException();
        }

        _exePath = exePath;

        try
        {
            _externalProgram = new ExternalProgram(exePath, "--server");
            _externalProgram.OnLoaded += () => _isBootstrapped = true;
            _externalProgram.OnLoaded += () => OnBootstrapped?.Invoke();
            Console.WriteLine("Bootstrapping SubSync server...");
            await _externalProgram.StartServer();
        }
        catch (Exception e)
        {
            MessageBoxHelper.ShowError($"Failed to launch SubSync server:\n\n{e.Message}\n\n{e.StackTrace}");
            await Shutdown();
            throw;
        }
    }

    public Task Shutdown()
    {
        if (_externalProgram == null) return Task.CompletedTask;
        _isBootstrapped = false;
        _externalProgram?.Dispose();
        _externalProgram = null;
        _exePath = null;
        OnShutdown?.Invoke();
        return Task.CompletedTask;
    }

    public async Task WaitForLoaded()
    {
        if (!_isBootstrapped)
        {
            var stopwatch = Stopwatch.StartNew();
            while (!_isBootstrapped)
            {
                if (stopwatch.ElapsedMilliseconds > 20 * 1000)
                {
                    throw new Exception("Server initialization timeout");
                }

                await Task.Delay(100);
            }
        }
    }

    public async Task ExecuteSubSync(IReadOnlyList<RenameTask> taskList)
    {
        await ExecuteSubSync(taskList.Where(x => x.Status != RenameTaskStatus.Failed)
            .Select(x => (x.MatchItem?.Video ?? "", x.Alter)).ToList());
    }

    public async Task ExecuteSubSync(IReadOnlyList<(string video, string subtitle)> taskList)
    {
        if (_externalProgram == null) throw new Exception("External program not initialized");
        await WaitForLoaded();

        foreach (var item in taskList)
        {
            await AddPostTaskQueue(item.video, item.subtitle);
        }

        var timeDuring = Stopwatch.StartNew();
        var (ready, data) = await _externalProgram.Send("start", "ready", false);
        timeDuring.Stop();
        _externalProgram.Log((ready
                                 ? $"\ud83d\ude09 {Application.Current.GetResource<string>("App.Strings.SubSyncTasksComplete")}"
                                 : $"\ud83e\udd72 {Application.Current.GetResource<string>("App.Strings.SubSyncTasksFail")} Data: {data}")
                             + $" [{Application.Current.GetResource<string>("App.Strings.SubSyncTasksDuration")}{timeDuring.ElapsedMilliseconds}ms]\n");
        await Task.Delay(300);
        _externalProgram.SetAllowTerminalClose(true);
    }

    private async Task AddPostTaskQueue(string video, string subtitle)
    {
        if (_externalProgram == null) throw new Exception("External program not initialized");

        // var postTask = Config.Get().SubSyncCommand;
        var postTask = "\"{video}\" -i \"{subtitle}\" --overwrite-input";
        if (string.IsNullOrEmpty(postTask)) return;

        var command = postTask
            .Replace("{subtitle}", subtitle)
            .Replace("{video}", video);

        var (added, data) = await _externalProgram.Send("add:" + command, "added");
        if (!added) _externalProgram.Log($"Add post task to queue failed, Data: {data}");
    }

    public async Task DownloadFFsubsyncBin()
    {
        var path = Path.Combine(Config.ConfigDir, "ffsubsync_bin");
        var url =
            $"https://github.com/qwqcode/ffsubsync-bin/releases/latest/download/ffsubsync_bin_{SystemInfo.GetOSArchPair().Replace("windows", "win")}";
        var dialog = MessageBoxHelper.ShowProgress(_target,
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