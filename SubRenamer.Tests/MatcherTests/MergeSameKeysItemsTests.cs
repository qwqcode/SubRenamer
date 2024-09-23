using SubRenamer.Matcher;
using static SubRenamer.Matcher.Helper;

namespace SubRenamer.Tests.MatcherTests;

[TestFixture]
public class MergeSameKeysItemsTests
{
    [Test]
    public void Basic()
    {
        var input = new List<MatchItem>
        {
            new("k", "v.mp4", ""),
            new("k", "", "s1.srt"),
            new("k", "", "s2.srt"),
        };
        var output = new List<MatchItem>
        {
            new("k", "v.mp4", "s1.srt"),
            new("k", "v.mp4", "s2.srt"),
        };

        Assert.That(MergeSameKeysItems(input), Is.EqualTo(output));
    }

    [Test]
    public void ItemsFirstOneIsComplete()
    {
        var input = new List<MatchItem>
        {
            new("k", "v1.mp4", "s1.srt"),
            new("k", "", "s2.srt"),
            new("k", "", "s3.srt"),
        };
        var output = new List<MatchItem>
        {
            new("k", "v1.mp4", "s1.srt"),
            new("k", "v1.mp4", "s2.srt"),
            new("k", "v1.mp4", "s3.srt"),
        };

        Assert.That(MergeSameKeysItems(input), Is.EqualTo(output));
    }

    [Test]
    public void ItemsAreAlreadyMerged()
    {
        var input = new List<MatchItem>
        {
            new("k", "v.mp4", "s1.srt"),
            new("k", "v.mp4", "s2.srt")
        };
        var output = new List<MatchItem>(input);

        Assert.That(MergeSameKeysItems(input), Is.EqualTo(output));
    }

    [Test]
    public void SelectFirstVideo_WhenGivenSameKeyButDifferentVideos()
    {
        var input = new List<MatchItem>
        {
            new("k1", "v1.mp4", "s1.srt"),
            new("k1", "v2.mp4", "s2.srt"),
            new("k1", "v3.mp4", "s3.srt")
        };
        var output = new List<MatchItem>
        {
            new("k1", "v1.mp4", "s1.srt"),
            new("k1", "v1.mp4", "s2.srt"),
            new("k1", "v1.mp4", "s3.srt"),
        };

        Assert.That(MergeSameKeysItems(input), Is.EqualTo(output));
    }

    [Test]
    public void HandleEmptyKeys()
    {
        var input = new List<MatchItem>
        {
            new("", "v1.mp4", "s1.srt"),
            new("k1", "v1.mp4", ""),
            new("k1", "", "s2.srt"),
            new("k1", "", "s3.srt"),
            new("", "v2.mp4", "s4.srt")
        };
        var output = new List<MatchItem>
        {
            new("k1", "v1.mp4", "s2.srt"),
            new("k1", "v1.mp4", "s3.srt"),
            new("", "v1.mp4", "s1.srt"),
            new("", "v2.mp4", "s4.srt")
        };

        Assert.That(MergeSameKeysItems(input), Is.EqualTo(output));
    }

    [Test]
    public void HandleEmptyVideos()
    {
        var input = new List<MatchItem>
        {
            new("k1", "", "s1.srt"),
            new("k1", "", "s2.srt"),
            new("k1", "", "s3.srt"),
            new("k2", "v.mp4", ""),
            new("k2", "", "s4.srt")
        };
        var output = new List<MatchItem>
        {
            new("k1", "", "s1.srt"),
            new("k1", "", "s2.srt"),
            new("k1", "", "s3.srt"),
            new("k2", "v.mp4", "s4.srt")
        };

        Assert.That(MergeSameKeysItems(input), Is.EqualTo(output));
    }

    [Test]
    public void NoValidKeys()
    {
        var input = new List<MatchItem>
        {
            new("", "v1.mp4", ""),
            new("", "v1.mp4", "s1.srt"),
            new("", "v2.mp4", "s2.srt"),
            new("", "v2.mp4", "s3.srt")
        };

        Assert.That(MergeSameKeysItems(input), Is.EqualTo(new List<MatchItem>(input)));
    }

    [Test]
    public void EmptyInput()
    {
        Assert.That(MergeSameKeysItems(new List<MatchItem>()), Is.Empty);
    }
}