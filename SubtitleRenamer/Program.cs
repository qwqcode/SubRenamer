using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;

namespace SubtitleRenamer
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
    }
}
