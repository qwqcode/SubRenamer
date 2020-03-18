using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SubRenamer
{
    public class AppSettings
    {
        public static IniFile IniFile = new IniFile();
        public static bool RawSubtitleBuckup
        {
            get { return IniFile.Read("RawSubtitleBuckup", "1").Equals("1"); }
            set { RawSubtitleBuckup = value; IniFile.Write("RawSubtitleBuckup", value ? "1" : "0"); }
        }
        
        public static bool OpenFolderFinished
        {
            get { return IniFile.Read("OpenFolderFinished", "0").Equals("1"); }
            set { OpenFolderFinished = value; IniFile.Write("OpenFolderFinished", value ? "1" : "0"); }
        }

        public static bool ListItemRemovePrompt
        {
            get { return IniFile.Read("ListItemRemovePrompt", "1").Equals("1"); }
            set { ListItemRemovePrompt = value; IniFile.Write("ListItemRemovePrompt", value ? "1" : "0"); }
        }

        public static bool ListShowFileFullName
        {
            get { return IniFile.Read("ListShowFileFullName", "0").Equals("1"); }
            set { ListShowFileFullName = value; IniFile.Write("ListShowFileFullName", value ? "1" : "0"); }
        }
    }

    public class IniFile
    {
        string Path;
        public static string APP_NAME = Assembly.GetExecutingAssembly().GetName().Name;

        [DllImport("kernel32", CharSet = CharSet.Ansi)]
        static extern long WritePrivateProfileString(string Section, string Key, string Value, string FilePath);

        [DllImport("kernel32", CharSet = CharSet.Ansi)]
        static extern int GetPrivateProfileString(string Section, string Key, string Default, StringBuilder RetVal, int Size, string FilePath);

        public IniFile(string IniPath = null)
        {
            Path = new FileInfo(IniPath ?? APP_NAME + ".ini").FullName.ToString();
        }

        public string Read(string Key, string DefaultVal = "", string Section = null)
        {
            var RetVal = new StringBuilder(255);
            GetPrivateProfileString(Section ?? APP_NAME, Key, DefaultVal, RetVal, 255, Path);
            return RetVal.ToString();
        }

        public void Write(string Key, string Value, string Section = null)
        {
            WritePrivateProfileString(Section ?? APP_NAME, Key, Value, Path);
        }

        public void DeleteKey(string Key, string Section = null)
        {
            Write(Key, null, Section ?? APP_NAME);
        }

        public void DeleteSection(string Section = null)
        {
            Write(null, null, Section ?? APP_NAME);
        }

        public bool KeyExists(string Key, string Section = null)
        {
            return Read(Key, Section).Length > 0;
        }
    }
}
