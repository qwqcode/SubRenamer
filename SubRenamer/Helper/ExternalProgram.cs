using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Avalonia.Threading;

namespace SubRenamer.Helper;

public class ExternalProgram(string filename, string arguments)
{
    public class ServerResult
    {
        public const string ServerJsonPrefix = "[SERVER] ";
        public string Status { get; set; } = "";
        public string Message { get; set; } = "";
        public string Command { get; set; } = "";
    }

    private ProgramProcess? _process;
    private TerminalWin? _terminalWin;
    public event Action? OnAbort;
    
    public async Task StartServer()
    {
        _process = ProgramProcess.Create(filename, arguments);
        
        // Wait for ready
        var (isReady, data) = await _process.WaitForStatus("ready", true);
        if (!isReady) throw new Exception($"Server initialization failed, Data: {data}");
        
        // Create terminal win
        _terminalWin = new TerminalWin();
        
        // Bind standard output receiver
        var handler = new DataReceivedEventHandler((sender, args) =>
        {
            Dispatcher.UIThread.Post(() =>
            {
                if (args.Data == null || args.Data.StartsWith(ServerResult.ServerJsonPrefix)) return;
                _terminalWin?.Log(args.Data);
            });
        });
        _process.OutputDataReceived += handler;
        _process.ErrorDataReceived += handler;
    }

    public async Task<(bool ok, string result)> StartTask(string text, string expectedStatus)
    {
        if (_process == null) throw new Exception("Server not started");
        if (_terminalWin == null) throw new Exception("Must start a terminal window first");
        
        var cancelTokenSource = new CancellationTokenSource();
        var abortHandler = () =>
        {
            cancelTokenSource.Cancel();
            OnAbort?.Invoke();
        };
 
        try
        {
            _terminalWin.OnClosed += abortHandler; // If closed and in progress, then abort it
            await _process.Send(text);
            return await _process.WaitForStatus(expectedStatus, false, cancelTokenSource.Token);
        }
        finally
        {
            _terminalWin.OnClosed -= abortHandler; // Unbind event because task is finished, not abort
        }
    }
    
    public async Task<(bool isOK, string data)> SendAndWaitForStatus(string text, string expectedStatus, CancellationToken cancellationToken = default)
    {
        if (_process == null) throw new Exception("Server not started");
        await _process.Send(text);
        return await _process.WaitForStatus(expectedStatus, true, cancellationToken);
    }

    public void Log(string text)
    {
        Dispatcher.UIThread.Post(() => _terminalWin?.Log(text));
    }
    
    public void Dispose()
    {
        _process?.Dispose();
    }
}