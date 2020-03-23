using SubRenamer.Lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SubRenamer
{
    public class AppSettings
    {
        public static bool RawSubtitleBuckup
        {
            get { return GetBoolVal(defaultVal: true); }
            set { WriteBoolVal(value); }
        }

        public static bool ListItemRemovePrompt
        {
            get { return GetBoolVal(defaultVal: true); }
            set { WriteBoolVal(value); }
        }

        public static bool ListShowFileFullName
        {
            get { return GetBoolVal(defaultVal: false); }
            set { WriteBoolVal(value); }
        }

        #region Utils
        public static IniFile IniFile = new IniFile();

        private static bool GetBoolVal(bool defaultVal = false, [CallerMemberName]string key = null)
        {
            if (string.IsNullOrWhiteSpace(key)) return defaultVal;
            string defaultValStr = defaultVal ? "1" : "0";
            return IniFile.Read(key, defaultValStr).Equals("1");
        }

        private static void WriteBoolVal(bool val, [CallerMemberName]string key = null)
        {
            if (string.IsNullOrWhiteSpace(key)) return;
            IniFile.Write(key, val ? "1" : "0");
        }
        #endregion
    }
}
