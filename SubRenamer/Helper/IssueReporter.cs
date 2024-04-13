using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
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
                
            var box = MessageBoxManager.GetMessageBoxCustom(new MessageBoxCustomParams
            {
                ButtonDefinitions = new List<ButtonDefinition>
                {
                    new ButtonDefinition { Name = "反馈问题", IsDefault = true },
                    new ButtonDefinition { Name = "忽略", IsCancel = true }
                },
                ContentTitle = "检测到程序崩溃 x_x",
                ContentMessage = $"{title}\n\n点击下方按钮反馈错误报告",
                Icon = MsBox.Avalonia.Enums.Icon.Warning,
                WindowStartupLocation = WindowStartupLocation.CenterScreen,
                CanResize = false,
                MaxWidth = 500,
                MaxHeight = 800,
                ShowInCenter = true,
                Topmost = true
            });
            var result = await box.ShowAsync();
            if (result == "反馈问题")
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