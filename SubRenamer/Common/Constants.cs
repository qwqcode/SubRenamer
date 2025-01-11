using System.Collections.Generic;
using System.Linq;

namespace SubRenamer.Common;

public static class Constants
{
    private static readonly HashSet<string> VideoExtensions = [
        "mkv", "mp4", "flv", "avi", "mov", "rmvb", "wmv", "mpg", "avs", "m4v", "ts",
        "3gp", "asf", "divx", "f4v", "m2ts", "mpeg", "mts", "ogv", "qt", "rm", "rv",
        "swf", "vob", "webm", "xvid", "strm",
    ];
    private static readonly HashSet<string> SubtitleExtensions = [
        "srt", "ass", "ssa", "sub", "idx", "txt", "ttxt", "vtt", "smi", "xml", "json",
        "dfxp", "ttml", "mpl2", "aqt", "jss", "psb", "pjs", "stl", "usf", "sbv", "lrc", "cap",
    ];

    public static HashSet<string> GetVideoExtensions()
    {
        var append = Config.Get().VideoExtAppend.Split(",");
        if (append.Length > 0) return [..VideoExtensions.Concat(append)];
        return VideoExtensions;
    }
    
    public static HashSet<string> GetSubtitleExtensions()
    {
        var append = Config.Get().SubtitleExtAppend.Split(",");
        if (append.Length > 0) return [..SubtitleExtensions.Concat(append)];
        return SubtitleExtensions;
    }
}
