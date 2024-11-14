using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Avalonia.Threading;
using SubRenamer.ViewModels;
using SubRenamer.Views;

namespace SubRenamer.Helper;

public class ExternalProgram(string filename, string arguments)
{
    private ProgramProcess? _process;
    private TerminalWin? _terminalWin;
    
    public event Action? OnLoaded;
    
    public async Task StartServer()
    {
        _process = CreateProcess(filename, arguments);
        var (isReady, data) = await _process.WaitForStatus("ready", true);
        if (!isReady) throw new Exception($"Server initialization failed, Data: {data}");
        _terminalWin = new TerminalWin();
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
        OnLoaded?.Invoke();
    }
    
    public async Task<(bool isOK, string data)> Send(string text, string expectedStatus, bool fastExit = true)
    {
        if (_process == null) throw new Exception("Server not started");
        await _process.Send(text);
        return await _process.WaitForStatus(expectedStatus, fastExit);
    }

    public void Log(string text)
    {
        Dispatcher.UIThread.Post(() => _terminalWin?.Log(text));
    }
    
    public void SetAllowTerminalClose(bool allowClose)
    {
        _terminalWin?.SetAllowClose(allowClose);
    }
    
    public void Dispose()
    {
        _process?.Dispose();
    }

    private class ProgramProcess
    {
        private Process _process;
        
        public event DataReceivedEventHandler? OutputDataReceived;
        public event DataReceivedEventHandler? ErrorDataReceived;
        
        public ProgramProcess(Process process)
        {
            _process = process;
            process.OutputDataReceived += OutputDataReceivedEventHandler;
            process.ErrorDataReceived += ErrorDataReceivedEventHandler;
            
            // Begin asynchronous read
            process.BeginOutputReadLine();
            process.BeginErrorReadLine();
        }

        private ServerResult? _lastResult;
        private string? _lastDataRaw;

        private void OutputDataReceivedEventHandler(object sender, DataReceivedEventArgs e)
        {
            // trigger all OutputDataReceived events
            OutputDataReceived?.Invoke(sender, e);
            Console.WriteLine("[OutputDataReceived] " + e.Data);
            
            // parse server result
            if (e.Data != null && e.Data.StartsWith(ServerResult.ServerJsonPrefix))
            {
                var result = ParseResult(e.Data.Substring(ServerResult.ServerJsonPrefix.Length));
                _lastResult = result;
                _lastDataRaw = e.Data;
            }
        }
        
        private void ErrorDataReceivedEventHandler(object sender, DataReceivedEventArgs e)
        {
            ErrorDataReceived?.Invoke(sender, e);
            Console.WriteLine("[ErrorDataReceived] " + e.Data);

            _lastResult = new ServerResult
            {
                Status = "error",
                Message = e.Data ?? "Unknown error"
            };
            _lastDataRaw = e.Data ?? "Unknown error";
        }
        
        public async Task<(bool isOK, string data)> WaitForStatus(string status, bool fastExit = true, int timeoutMs = 0)
        {
            var timer = new Stopwatch();
            timer.Start();
            
            while (_lastResult is null || (!fastExit && _lastResult.Status != status))
            {
                await Task.Delay(100);
                if (timeoutMs != 0 && timer.ElapsedMilliseconds < timeoutMs)
                {
                    _lastResult = null;
                    _lastDataRaw = null;
                    return (false, "Timeout waiting for server");
                }
            }
            
            var result = (_lastResult?.Status == status, _lastDataRaw ?? "");
            _lastResult = null;
            _lastDataRaw = null;
            return result;
        }

        private ServerResult ParseResult(string json)
        {
            using var jsonStream = new MemoryStream(Encoding.UTF8.GetBytes(json));
            return JsonHelper.ParseJsonSync<ServerResult>(jsonStream) ?? new ServerResult();
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
    
    private static ProgramProcess CreateProcess(string filename, string args)
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

    private class TerminalWin
    {
        private TerminalViewModel? _terminalViewModel;
        private TerminalWindow? _terminalWindow;
        private bool _allowClose = false;

        public void Log(string? text)
        {
            if (_terminalWindow == null) Init();
            _terminalViewModel?.WriteLine(text ?? "");
        }
        
        public void SetAllowClose(bool allowClose)
        {
            _allowClose = allowClose;
        }

        private void Init()
        {
            _allowClose = false;
            _terminalViewModel = new TerminalViewModel();
            _terminalWindow = new TerminalWindow
            {
                DataContext = _terminalViewModel
            };
            _terminalWindow.Closing += (sender, args) =>
            {
                if (!_allowClose) args.Cancel = true;
            };
            _terminalWindow.Closed += (sender, args) =>
            {
                _terminalWindow = null;
                _terminalViewModel = null;
                _allowClose = false;
            };
            _terminalWindow.Show();
        }
    }
    
    public class ServerResult
    {
        public const string ServerJsonPrefix = "[SERVER] ";
        public string Status { get; set; } = "";
        public string Message { get; set; } = "";
        public string Command { get; set; } = "";
    }
}