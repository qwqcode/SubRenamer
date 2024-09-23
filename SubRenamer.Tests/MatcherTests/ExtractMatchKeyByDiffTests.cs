using SubRenamer.Matcher;
using static SubRenamer.Matcher.Diff;

namespace SubRenamer.Tests.MatcherTests;

[TestFixture]
public class ExtractMatchKeyByDiffTests
{
    [Test]
    public void Basic()
    {
        var diff = new DiffResult("file", "");
        var input = "file123";
        var key = "123";
        Assert.That(ExtractMatchKeyByDiff(diff, input), Is.EqualTo(key));
    }

    [Test]
    public void WithSuffix()
    {
        var diff = new DiffResult("file", "_end");
        var input = "file456_end";
        var key = "456";
        Assert.That(ExtractMatchKeyByDiff(diff, input), Is.EqualTo(key));
    }

    [Test]
    public void NoDiff_MatchNumeric()
    {
        DiffResult? diff = null;
        var input = "example789";
        var key = "789";
        Assert.That(ExtractMatchKeyByDiff(diff, input), Is.EqualTo(key));
    }

    [Test]
    public void NonDigit()
    {
        var diff = new DiffResult("file", "_end");
        var input = "fileABC_end";
        var key = "ABC";
        Assert.That(ExtractMatchKeyByDiff(diff, input), Is.EqualTo(key));
    }

    [Test]
    public void ComplexName()
    {
        var diff = new DiffResult("episode_", "_final");
        var input = "episode_12_final";
        var key = "12";
        Assert.That(ExtractMatchKeyByDiff(diff, input), Is.EqualTo(key));
    }

    [Test]
    public void NoMatch()
    {
        var diff = new DiffResult("file", "_end");
        var input = "otherfile";
        var key = "";
        Assert.That(ExtractMatchKeyByDiff(diff, input), Is.EqualTo(key));
    }
}