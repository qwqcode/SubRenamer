using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Threading;
using MsBox.Avalonia;
using MsBox.Avalonia.Dto;
using MsBox.Avalonia.Models;
using SubRenamer.Services;

namespace SubRenamer.Helper;

public class IssueReporter
{
    public static void CheckCrashAndShowDialog()
    {
        Dispatcher.UIThread.Post(async () =>
        {
            if (!File.Exists(App.CrashLogFile)) return;
                    
            var crashLog = await File.ReadAllTextAsync(App.CrashLogFile);
            var title = crashLog.Split('\n').FirstOrDefault() ?? "";

            var reportBtnName = Application.Current.GetResource<string>("App.Strings.IssueReporterReport");
            var box = MessageBoxManager.GetMessageBoxCustom(new MessageBoxCustomParams
            {
                ButtonDefinitions = new List<ButtonDefinition>
                {
                    new ButtonDefinition { Name = reportBtnName, IsDefault = true },
                    new ButtonDefinition { Name = Application.Current.GetResource<string>("App.Strings.IssueReporterIgnore"), IsCancel = true }
                },
                ContentTitle = Application.Current.GetResource<string>("App.Strings.IssueReporterCapital"),
                ContentMessage = $"{title}\n\n{Application.Current.GetResource<string>("App.Strings.IssueReporterMessage")}",
                Icon = MsBox.Avalonia.Enums.Icon.Warning,
                WindowStartupLocation = WindowStartupLocation.CenterScreen,
                CanResize = false,
                MaxWidth = 500,
                MaxHeight = 800,
                ShowInCenter = true,
                Topmost = true
            });
            var result = await box.ShowAsync();
            if (result == reportBtnName)
            {
                CreateGitHubIssue(title, $"{crashLog}\n\nWheel: {SystemInfo.GetOSArchPair()} Version: v{Config.AppVersion}");
            }
            
            // rename CrashLog
            File.Move(App.CrashLogFile, Path.Combine(Config.ConfigDir, $"crash.{DateTime.Now:yyyyMMddHHmmss}.solved.log"));
        }, DispatcherPriority.Background);
    }

    public static void CreateGitHubIssue(Exception e)
    {
        CreateGitHubIssue(e.Message, e.StackTrace ?? "");
    }
    
    public static void CreateGitHubIssue(string caption, string details)
    {
        var title = $"[CRASH][{"v" + Config.AppVersion}] {caption}";
        BrowserHelper.OpenBrowserAsync(
            $"https://github.com/qwqcode/SubRenamer/issues/new?title={HttpUtility.UrlEncode(title, Encoding.UTF8)}&body={HttpUtility.UrlEncode(caption + "\n\n```\n" + details + "\n```", Encoding.UTF8)}");
    }
}