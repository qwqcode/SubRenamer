using SubRenamer.Core;
using static SubRenamer.Core.MatcherHelper;

namespace SubRenamer.Tests.MatcherTests;

[TestFixture]
public class MergeSameFilenameItemsTests
{
    [Test]
    public void Basic()
    {
        var input = new List<MatchItem>
        {
            new("", "a.mp4", ""),
            new("", "", "b.srt"),
            
            new("", "", "a.srt"),
            new("", "b.mp4", ""),
           
            
            new("", "v1.mp4", ""),
            new("", "", "s1.srt"),
            new("2", "v2.mp4", ""),
            new("2", "", "s2.srt"),
        };
        var output = new List<MatchItem>
        {
            new("a", "a.mp4", "a.srt"),
            new("b", "b.mp4", "b.srt"),
            
            new("", "v1.mp4", ""),
            new("", "", "s1.srt"),
            new("2", "v2.mp4", ""),
            new("2", "", "s2.srt"),
        };

        var result = MergeSameFilenameItems(input);
        Assert.That(result, Is.EqualTo(output));
    }
}