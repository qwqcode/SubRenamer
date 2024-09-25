using System.Reflection;
using SubRenamer.Core;

namespace SubRenamer.Tests.MatcherTests;

[TestFixture]
public class FindCommonSuffixTests
{
    private static string FindCommonSuffix(string a, string b)
        => (string)typeof(MatcherDiff)
            .GetMethod("FindCommonSuffix", BindingFlags.NonPublic | BindingFlags.Static)!
            .Invoke(null, new object[] { a, b })!;

    [Test]
    public void Basic()
    {
        Assert.That(FindCommonSuffix("01]", "02]"), Is.EqualTo("]"));
        Assert.That(FindCommonSuffix("01]end", "02]end"), Is.EqualTo("]"));
        Assert.That(FindCommonSuffix(" 01 ] end", " 02 ] end"), Is.EqualTo("]"));
    }

    [Test]
    public void SkipMatchCharacter()
    {
        Assert.That(FindCommonSuffix("01A", "02A"), Is.EqualTo(""),
            "should skip Digit and Uppercase Letter, then no match");
        Assert.That(FindCommonSuffix("01A]", "02A]"), Is.EqualTo("]"),
            "should skip Digit and Uppercase Letter, then match");

        Assert.That(FindCommonSuffix("01a", "02a"), Is.EqualTo(""), "should skip Lowercase Letter, then no match");
        Assert.That(FindCommonSuffix("01a]", "02a]"), Is.EqualTo("]"), "should skip Lowercase Letter, then match");

        Assert.That(FindCommonSuffix("01 ", "02  "), Is.EqualTo(""), "should skip Whitespace, then no match");
        Assert.That(FindCommonSuffix("01 ]", "02  ]"), Is.EqualTo("]"), "should skip Whitespace, then match");
    }

    [Test]
    public void NoCommon()
    {
        Assert.That(FindCommonSuffix("01$", "02]"), Is.EqualTo(""));
        Assert.That(FindCommonSuffix("01 abc", "02 def"), Is.EqualTo(""));
    }

    [Test]
    public void ExactMatch()
    {
        Assert.That(FindCommonSuffix("]end", "]end"), Is.EqualTo("]"));
    }

    [Test]
    public void WithMultipleSign()
    {
        Assert.That(FindCommonSuffix("01]_$*@end", "02]_$*@end"), Is.EqualTo("]"));
    }

    [Test]
    public void DifferentLengths()
    {
        Assert.That(FindCommonSuffix("01] abc abc", "01]"), Is.EqualTo("]"));
        Assert.That(FindCommonSuffix("01] abc abc", "1234567]"), Is.EqualTo("]"));
    }

    [Test]
    public void MatchChineseCharacter()
    {
        // @see https://github.com/qwqcode/SubRenamer/pull/45
        Assert.That(FindCommonSuffix("01話", "02話"), Is.EqualTo("話"));
        Assert.That(FindCommonSuffix("01B 集", "02B 集"), Is.EqualTo("集"));

        var result = FindCommonSuffix("01B 話 番外篇", "02B 話 番外篇");
        Assert.That(result.Length, Is.EqualTo(1), "should match only single character");
        Assert.That(result, Is.EqualTo("話"));
    }
}