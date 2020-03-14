using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubRenamer
{
    public enum VsStatus
    {
        /// <summary>
        /// 预备
        /// </summary>
        Ready,

        /// <summary>
        /// 完成
        /// </summary>
        Done,

        /// <summary>
        /// 错误
        /// </summary>
        Fatal,

        /// <summary>
        /// 未匹配
        /// </summary>
        Unmatched,

        /// <summary>
        /// 缺少视频文件
        /// </summary>
        VideoLack,

        /// <summary>
        /// 缺少字幕文件
        /// </summary>
        SubLack
    }

    /// <summary>
    /// 视频/字幕文件项目
    /// </summary>
    public class VsItem
    {
        /// <summary>
        /// 匹配桥梁值
        /// </summary>
        public string MatchKey { get; set; }

        /// <summary>
        /// 视频文件
        /// </summary>
        public FileInfo VideoFile { get; set; }


        /// <summary>
        /// 字幕文件
        /// </summary>
        public FileInfo SubFile { get; set; }

        /// <summary>
        /// 当前状态
        /// </summary>
        public VsStatus Status { get; set; }

        public string GetStatusStr()
        {
            var transDict = new Dictionary<VsStatus, string> {
                { VsStatus.Done, "成功" },
                { VsStatus.Fatal, "失败" },
                { VsStatus.Ready, "已匹配" },
                { VsStatus.Unmatched, "未匹配" },
                { VsStatus.VideoLack, "缺视频" },
                { VsStatus.SubLack, "缺字幕" },
            };

            return transDict[this.Status];
        }
    }
}
