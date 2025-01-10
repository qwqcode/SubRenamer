using System.Collections.Generic;
using System.Linq;

namespace SubRenamer.Helper;

public static class MatcherDataConverter
{
    public static List<Core.MatchItem> ConvertMatchItems(IReadOnlyList<Model.MatchItem> matchItems)
    {
        return matchItems.Select(item => new Core.MatchItem(item.Key, item.Video, item.Subtitle)).ToList();
    }
    
    public static List<Model.MatchItem> ConvertMatchItems(IReadOnlyList<Core.MatchItem> matchItems)
    {
        return matchItems.Select(item => new Model.MatchItem(item.Key, item.Video, item.Subtitle)).ToList();
    }
}