using SubRenamer.Model;

namespace SubRenamer.Helper;

public static class MatchItemHelper
{
    public static void UpdateMatchItemStatus(MatchItem item)
    {
        if (item.Key != "" && item.Subtitle != "" && item.Video != "") item.Status = "已匹配";
        else if (item.Key != "" && item.Subtitle != "") item.Status = "缺视频";
        else if (item.Key != "" && item.Video != "") item.Status = "缺字幕";
        else if (item.Key == "") item.Status = "";
    }
}