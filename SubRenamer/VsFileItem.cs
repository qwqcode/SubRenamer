using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubRenamer
{
    public enum VsFileStatus
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
    public class VsFileItem
    {
        /// <summary>
        /// 匹配桥梁值
        /// </summary>
        public string MatchKey { get; set; }

        /// <summary>
        /// 视频文件路径
        /// </summary>
        public string VideoFile { get; set; }


        /// <summary>
        /// 字幕文件路径
        /// </summary>
        public string SubFile { get; set; }

        /// <summary>
        /// 当前状态
        /// </summary>
        public VsFileStatus Status { get; set; }

        public string GetStatusStr()
        {
            var transDict = new Dictionary<VsFileStatus, string> {
                { VsFileStatus.Done, "成功" },
                { VsFileStatus.Fatal, "失败" },
                { VsFileStatus.Ready, "已匹配" },
                { VsFileStatus.Unmatched, "无匹配" },
                { VsFileStatus.VideoLack, "缺视频" },
                { VsFileStatus.SubLack, "缺字幕" },
            };

            return transDict[this.Status];
        }
    }
}
