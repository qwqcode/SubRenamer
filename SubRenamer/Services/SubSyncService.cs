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
    private ExternalProgram? _program;
    private bool _isBootstrapped;
    public event Action? OnBootstrapped;
    public event Action? OnShutdown;

    public bool GetIsAvailable() => _program != null;
    public bool GetIsBootstrapped() => _isBootstrapped;

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
        if (_isBootstrapped) return;
        
        // try find exe file
        var exePath = RetrieveExePath();
        if (string.IsNullOrEmpty(exePath))
        {
            Console.WriteLine("FFsubsync binary not found");
            throw new BinaryNotFoundException();
        }

        // start program
        try
        {
            _program = new ExternalProgram(exePath, "--server");
            _program.OnAbort += () =>
            {
                // Restart when abort
                Shutdown();
                _ = Bootstrap();
            };
            Console.WriteLine("Bootstrapping SubSync server...");
            await _program.StartServer();
        }
        catch (Exception e)
        {
            MessageBoxHelper.ShowError($"Failed to launch SubSync server:\n\n{e.Message}\n\n{e.StackTrace}");
            Shutdown();
            throw;
        }
        
        _isBootstrapped = true;
        OnBootstrapped?.Invoke();
    }

    public void Shutdown()
    {
        _isBootstrapped = false;
        _program?.Dispose();
        _program = null;
        OnShutdown?.Invoke();
    }

    private async Task WaitForBootstrap(CancellationToken cancellationToken = default)
    {
        if (_isBootstrapped) return;

        var tcs = new TaskCompletionSource();
        Action handler = () => tcs.TrySetResult();
        OnBootstrapped += handler;

        using var cts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
        cts.CancelAfter(TimeSpan.FromSeconds(20));

        try
        {
            await tcs.Task.WaitAsync(cts.Token);
        }
        catch (OperationCanceledException)
        {
            throw new Exception("Server initialization timeout");
        }
        finally
        {
            OnBootstrapped -= handler;
        }
    }

    public async Task ExecuteSubSync(IReadOnlyList<RenameTask> taskList)
    {
        await ExecuteSubSync(taskList.Where(x => x.Status != RenameTaskStatus.Failed)
            .Select(x => (x.MatchItem?.Video ?? "", x.Alter)).ToList());
    }

    public async Task ExecuteSubSync(IReadOnlyList<(string video, string subtitle)> taskList)
    {
        if (_program == null) throw new Exception("External program not initialized");

        // Wait until bootstrapped
        await WaitForBootstrap();

        // Add tasks to queue
        foreach (var item in taskList)
        {
            await AddPostTaskQueue(item.video, item.subtitle);
        }

        // Start all sub-sync tasks
        var timeDuring = Stopwatch.StartNew();
        var (ok, result) = await _program.StartTask("start", "ready");
        timeDuring.Stop();

        _program?.Log((ok ? $"\ud83d\ude09 {Application.Current.GetResource<string>("App.Strings.SubSyncTasksComplete")}"
                          : $"\ud83e\udd72 {Application.Current.GetResource<string>("App.Strings.SubSyncTasksFail")} Data: {result}")
                          + $" [{Application.Current.GetResource<string>("App.Strings.SubSyncTasksDuration")}{timeDuring.ElapsedMilliseconds}ms]\n");
    }

    private async Task AddPostTaskQueue(string video, string subtitle)
    {
        if (_program == null) throw new Exception("External program not initialized");

        // var postTask = Config.Get().SubSyncCommand;
        var postTask = "\"{video}\" -i \"{subtitle}\" --overwrite-input";
        if (string.IsNullOrEmpty(postTask)) return;

        var command = postTask
            .Replace("{subtitle}", subtitle)
            .Replace("{video}", video);

        var (added, data) = await _program.SendAndWaitForStatus("add:" + command, "added");
        if (!added) _program.Log($"Add post task to queue failed, Data: {data}");
    }

    public Task DownloadFFsubsyncBin() => FFsubsyncDownloader.Download(_target);
}