using SubRenamer.Matcher;
using static SubRenamer.Matcher.Diff;

namespace SubRenamer.Tests.MatcherTests;

[TestFixture]
public class GetDiffResultTests
{
    [Test]
    public void Basic()
    {
        var names = new List<string> { "file01_end", "file02_end", "file03_end" };
        var result = GetDiffResult(names);
        Assert.That(result, Is.EqualTo(new DiffResult("file", "_")));
    }


    [Test]
    public void OnlyPrefix()
    {
        var names = new List<string> { "file01", "file02", "file03" };
        var result = GetDiffResult(names);
        Assert.That(result, Is.EqualTo(new DiffResult("file", "")));
    }

    [Test]
    public void OnlySuffix()
    {
        var names = new List<string> { "01_end", "02_end", "03_end" };
        var result = GetDiffResult(names);
        Assert.That(result, Is.EqualTo(null));
    }

    [Test]
    public void NoCommon()
    {
        var names = new List<string> { "fileA", "testB", "exampleC" };
        var result = GetDiffResult(names);
        Assert.That(result, Is.Null);
    }

    [Test]
    public void SingleFile()
    {
        var names = new List<string> { "singlefile" };
        var result = GetDiffResult(names);
        Assert.That(result, Is.Null);
    }

    [Test]
    public void DuplicateNames()
    {
        var names = new List<string> { "file01", "file01", "file02" };
        var result = GetDiffResult(names);
        Assert.That(result, Is.EqualTo(new DiffResult("file", "")));
    }
}