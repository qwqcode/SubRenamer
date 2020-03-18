using SubRenamer.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
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
}
