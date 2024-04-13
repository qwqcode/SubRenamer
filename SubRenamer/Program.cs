using Avalonia;
using Avalonia.ReactiveUI;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using SubRenamer.Helper;

namespace SubRenamer;

internal class Program
{
    // Initialization code. Don't use any Avalonia, third-party APIs or any
    // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
    // yet and stuff might break.
    [STAThread]
    public static void Main(string[] args)
    {
        try
        {
            BuildAvaloniaApp()
                .StartWithClassicDesktopLifetime(args);
        }
        catch (Exception e)
        {
            File.WriteAllText(App.CrashLogFile, $"{e.Message}\n${e.StackTrace ?? ""}", Encoding.UTF8);
            throw;
        }
        finally
        {
            // This block is optional. 
            // Use the finally-block if you need to clean things up or similar
            // Log.CloseAndFlush();
        }
    }

    // Avalonia configuration, don't remove; also used by visual designer.
    public static AppBuilder BuildAvaloniaApp()
        => AppBuilder.Configure<App>()
            .UsePlatformDetect()
            .WithInterFont()
            .LogToTrace()
            .UseReactiveUI()
            .With(new MacOSPlatformOptions
            {
                DisableDefaultApplicationMenuItems = true
            });
}
