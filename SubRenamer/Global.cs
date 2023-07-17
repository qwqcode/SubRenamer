using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SubRenamer
{
    public class Global
    {
        #region 常量
        public static readonly string LOG_FILENAME = Path.Combine(Application.StartupPath, $"{Program.GetAppName()}.log");
        public static HashSet<string> VideoExts = new HashSet<string> { ".mkv", ".mp4", ".flv", ".avi", ".mov", ".rmvb", ".wmv", ".mpg", ".avs" };
        public static HashSet<string> SubExts = new HashSet<string> { ".srt", ".ass", ".ssa", ".sub", ".idx" };
        #endregion

        /// <summary>
        /// 文件类型
        /// </summary>
        public enum AppFileType
        {
            Video,
            Sub
        }
    }
}
