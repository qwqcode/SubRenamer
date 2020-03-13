using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Web;
using System.Windows.Forms;

namespace SubRenamer
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            if (!IsSupportedRuntimeVersion())
            {
                MessageBox.Show("当前 .NET Framework 版本过低，请升级至 4.5 或更新版本",
                "运行库版本过低", MessageBoxButtons.OK, MessageBoxIcon.Error);

                Process.Start(
                    "https://www.microsoft.com/zh-cn/download/details.aspx?id=53344");
                return;
            }

#if !DEBUG
                Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
                Application.ThreadException += Application_ThreadException;
                AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
#endif

            Application.ApplicationExit += Application_ApplicationExit;

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }

        public static RegistryKey OpenRegKey(string name, bool writable, RegistryHive hive = RegistryHive.CurrentUser)
        {
            // we are building x86 binary for both x86 and x64, which will
            // cause problem when opening registry key
            // detect operating system instead of CPU
            if (string.IsNullOrEmpty(name)) throw new ArgumentException(nameof(name));
            try
            {
                RegistryKey userKey = RegistryKey.OpenBaseKey(hive,
                        Environment.Is64BitOperatingSystem ? RegistryView.Registry64 : RegistryView.Registry32)
                    .OpenSubKey(name, writable);
                return userKey;
            }
            catch (ArgumentException ae)
            {
                MessageBox.Show("OpenRegKey: " + ae.ToString());
                return null;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
                return null;
            }
        }

        // See: https://msdn.microsoft.com/en-us/library/hh925568(v=vs.110).aspx
        public static bool IsSupportedRuntimeVersion()
        {
            const int minSupportedRelease = 378389; // NET 4.5

            const string subkey = @"SOFTWARE\Microsoft\NET Framework Setup\NDP\v4\Full\";
            using (var ndpKey = OpenRegKey(subkey, false, RegistryHive.LocalMachine))
            {
                if (ndpKey?.GetValue("Release") != null)
                {
                    var releaseKey = (int)ndpKey.GetValue("Release");

                    if (releaseKey >= minSupportedRelease)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private static int exited = 0;

        private static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            if (Interlocked.Increment(ref exited) == 1)
            {
                string errMsg = $"异常细节: {Environment.NewLine}{e.Exception}";
                ErrorCatchAction("UI Error", errMsg);
                // Application.Exit();
            }
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            if (Interlocked.Increment(ref exited) == 1)
            {
                string errMsg = $"异常细节: {Environment.NewLine}{e.ExceptionObject.ToString()}";
                ErrorCatchAction("non-UI Error", errMsg);
                // Application.Exit();
            }
        }

        private static void ErrorCatchAction(string type, string errorMsg)
        {
            string title = $"意外错误：{Application.ProductName} {"v" + Application.ProductVersion.ToString()}";
            Process.Start($"https://github.com/qwqcode/SubRenamer/issues/new?title={ HttpUtility.UrlEncode(title, Encoding.UTF8) }&body={ HttpUtility.UrlEncode(type+"\n"+errorMsg, Encoding.UTF8) }");
            MessageBox.Show(
                $"{title} 程序即将退出，请发起 issue 来反馈，谢谢 {Environment.NewLine}{errorMsg}",
                $"{Application.ProductName} {type}", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private static void Application_ApplicationExit(object sender, EventArgs e)
        {
            // detach static event handlers
            Application.ApplicationExit -= Application_ApplicationExit;
            Application.ThreadException -= Application_ThreadException;
        }

        public static string GetVersionStr()
        {
            return "v" + Application.ProductVersion.ToString();
        }
    }
}
