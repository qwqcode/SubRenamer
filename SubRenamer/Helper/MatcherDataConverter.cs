using System.Collections.Generic;
using System.Linq;
using SubRenamer.Core;

namespace SubRenamer.Helper;

public static class MatcherDataConverter
{
    public static List<MatchItem> ConvertMatchItems(IReadOnlyList<Model.MatchItem> matchItems)
    {
        return matchItems.Select(item => new MatchItem(item.Key, item.Video, item.Subtitle)).ToList();
    }
    
    public static List<Model.MatchItem> ConvertMatchItems(IReadOnlyList<MatchItem> matchItems)
    {
        return matchItems.Select(item => new Model.MatchItem(item.Key, item.Video, item.Subtitle)).ToList();
    }
}