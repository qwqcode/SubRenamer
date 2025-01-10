using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace SubRenamer.Helper;

public class ProgramProcess
{
    private Process _process;

    public event DataReceivedEventHandler? OutputDataReceived;
    public event DataReceivedEventHandler? ErrorDataReceived;

    public static ProgramProcess Create(string filename, string args)
    {
        // Initialize the process
        var processStartInfo = new ProcessStartInfo
        {
            FileName = filename,
            Arguments = args,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            RedirectStandardInput = true,
            UseShellExecute = false, // To redirect output
            CreateNoWindow = true
        };

        // Create the process
        var proc = Process.Start(processStartInfo)!;

        return new ProgramProcess(proc);
    }

    public ProgramProcess(Process process)
    {
        _process = process;
        process.OutputDataReceived += OutputDataReceivedEventHandler;
        process.ErrorDataReceived += ErrorDataReceivedEventHandler;

        // Begin asynchronous read
        process.BeginOutputReadLine();
        process.BeginErrorReadLine();
    }

    private ExternalProgram.ServerResult? _lastResult;
    private string? _lastDataRaw;

    private void OutputDataReceivedEventHandler(object sender, DataReceivedEventArgs e)
    {
        // trigger all OutputDataReceived events
        OutputDataReceived?.Invoke(sender, e);
        Console.WriteLine("[OutputDataReceived] " + e.Data);

        // parse server result
        if (e.Data != null && e.Data.StartsWith(ExternalProgram.ServerResult.ServerJsonPrefix))
        {
            var result = ParseResult(e.Data.Substring(ExternalProgram.ServerResult.ServerJsonPrefix.Length));
            _lastResult = result;
            _lastDataRaw = e.Data;
        }
    }

    private void ErrorDataReceivedEventHandler(object sender, DataReceivedEventArgs e)
    {
        ErrorDataReceived?.Invoke(sender, e);
        Console.WriteLine("[ErrorDataReceived] " + e.Data);

        _lastResult = new ExternalProgram.ServerResult
        {
            Status = "error",
            Message = e.Data ?? "Unknown error"
        };
        _lastDataRaw = e.Data ?? "Unknown error";
    }

    /// <summary>
    /// Waits for a specific status from the process output.
    /// </summary>
    /// <param name="status">The status string to wait for</param>
    /// <param name="fastExit">If true, returns immediately when any result is received. If false, waits until the exact status matches</param>
    /// <param name="cancellationToken">Optional cancellation token to cancel the waiting operation</param>
    /// <returns>A tuple containing:
    /// - isOK: true if the expected status was received
    /// - data: The raw data received, or error message if the operation failed
    /// </returns>
    public async Task<(bool isOK, string data)> WaitForStatus(string status, bool fastExit = true, CancellationToken cancellationToken = default)
    {
        try
        {
            await WaitUntilAsync(() => _lastResult != null && (fastExit || _lastResult.Status == status), cancellationToken);
            
            return Result(_lastResult?.Status == status, _lastDataRaw ?? "");
        }
        catch (OperationCanceledException)
        {
            return Result(false, "Operation cancelled");
        }
        
        // ------------------------------------------------------------------------
        async Task WaitUntilAsync(Func<bool> condition, CancellationToken ct)
        {
            while (!condition()) await Task.Delay(100, ct);
        }
        
        (bool ok, string data) Result(bool ok, string data)
        {
            _lastResult = null;
            _lastDataRaw = null;
            return (ok, data);
        }
    }
    
    private ExternalProgram.ServerResult ParseResult(string json)
    {
        using var jsonStream = new MemoryStream(Encoding.UTF8.GetBytes(json));
        return JsonHelper.ParseJsonSync<ExternalProgram.ServerResult>(jsonStream) ?? new ExternalProgram.ServerResult();
    }

    public async Task Send(string text)
    {
        await _process.StandardInput.WriteLineAsync(text);
    }

    public void Dispose()
    {
        _process.Kill();
        _process.Dispose();
    }
}