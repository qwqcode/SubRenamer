using SubRenamer.Model;

namespace SubRenamer.Helper;

public static class MatchItemHelper
{
    public static void UpdateMatchItemStatus(MatchItem item)
    {
        if (item.Key != "" && item.Subtitle != "" && item.Video != "") item.Status = MatchItemStatus.Matched;
        else if (item.Key != "" && item.Subtitle != "") item.Status = MatchItemStatus.NoVideo;
        else if (item.Key != "" && item.Video != "") item.Status = MatchItemStatus.NoSubtitle;
        else if (item.Key == "") item.Status = MatchItemStatus.NoMatch;
    }
}