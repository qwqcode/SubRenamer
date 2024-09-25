using System.Reflection;
using SubRenamer.Core;

namespace SubRenamer.Tests.MatcherTests;

[TestFixture]
public class FindCommonPrefixTests
{
    private static string FindCommonPrefix(string a, string b)
        => (string)typeof(MatcherDiff)
            .GetMethod("FindCommonPrefix", BindingFlags.NonPublic | BindingFlags.Static)!
            .Invoke(null, new object[] { a, b })!;

    [Test]
    public void Basic()
    {
        Assert.That(FindCommonPrefix("file01", "file02"), Is.EqualTo("file"));
        Assert.That(FindCommonPrefix("file[01]", "file[02]"), Is.EqualTo("file["));
        Assert.That(FindCommonPrefix("file [ 01 ].mp4", "file [ 02 ].mp4"), Is.EqualTo("file [ "));
        Assert.That(FindCommonPrefix("進撃の巨人 第 01 話", "進撃の巨人 第 02 話"), Is.EqualTo("進撃の巨人 第 "));
    }

    [Test]
    public void NoCommon()
    {
        Assert.That(FindCommonPrefix("abc", "def"), Is.EqualTo(""));
    }

    [Test]
    public void ExactMatch()
    {
        Assert.That(FindCommonPrefix("same", "same"), Is.EqualTo("same"));
    }

    [Test]
    public void WithNumbers()
    {
        Assert.That(FindCommonPrefix("file123", "file456"), Is.EqualTo("file"));
    }

    [Test]
    public void DifferentLengths()
    {
        Assert.That(FindCommonPrefix("file123", "file1"), Is.EqualTo("file"));
        Assert.That(FindCommonPrefix("file123", "file6"), Is.EqualTo("file"));
    }

    [Test]
    public void NoEndDigit()
    {
        Assert.That(FindCommonPrefix("file123", "file456"), Is.EqualTo("file"));
        Assert.That(FindCommonPrefix("file12345", "file12345"), Is.EqualTo("file"));
        Assert.That(FindCommonPrefix("進撃の巨人01", "進撃の巨人02"), Is.EqualTo("進撃の巨人"));
        Assert.That(FindCommonPrefix("file S01E01", "file S01E02"), Is.EqualTo("file S01E"), "should keep S01E");
    }

    [Test]
    public void KeepWhitespace()
    {
        Assert.That(FindCommonPrefix("file      01", "file  02"), Is.EqualTo("file  "));
    }
}