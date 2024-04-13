using Avalonia;
using Avalonia.ReactiveUI;
using System;
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
#if !DEBUG
        try
        {
#endif
            BuildAvaloniaApp()
                .StartWithClassicDesktopLifetime(args);
#if !DEBUG
        }
        catch (Exception e)
        {
            // here we can work with the exception, for example add it to our log file
            // Log.Fatal(e, "Something very bad happened");

            CreateGitHubIssue(e.Message, e.StackTrace ?? "");
        }
        finally
        {
            // This block is optional. 
            // Use the finally-block if you need to clean things up or similar
            // Log.CloseAndFlush();
        }
#endif
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
    
    private static void CreateGitHubIssue(string caption, string details)
    {
        Task.Run(() =>
        {
            var title = $"[PANIC][{"v" + Config.AppVersion}] {caption}";
            BrowserHelper.OpenBrowserAsync(
                $"https://github.com/qwqcode/SubRenamer/issues/new?title={HttpUtility.UrlEncode(title, Encoding.UTF8)}&body={HttpUtility.UrlEncode(caption + "\n\n```\n" + details + "\n```", Encoding.UTF8)}");
        });
    }
}
