using System.Reflection;
using SubRenamer.Core;

namespace SubRenamer.Tests.MatcherTests;

[TestFixture]
public class CalculateFileKeysTests
{
    private static Dictionary<string, string> CalculateFileKeys(IReadOnlyList<string> files, string? regexPattern)
        => (Dictionary<string, string>)typeof(Matcher)
            .GetMethod("CalculateFileKeys", BindingFlags.NonPublic | BindingFlags.Static)!
            .Invoke(null, new object[] { files, regexPattern! })!;

    [Test]
    public void UsesDiffAlgorithm_WithNullRegexPattern()
    {
        List<string> inputFiles = ["dir6/file1.txt", "dir7/file2.txt", "dir8/file3.txt"];
        var output = new Dictionary<string, string>
        {
            { "dir6/file1.txt", "1" },
            { "dir7/file2.txt", "2" },
            { "dir8/file3.txt", "3" }
        };

        Assert.That(CalculateFileKeys(inputFiles, regexPattern: null), Is.EqualTo(output));
    }

    [Test]
    public void UsesRegexAlgorithm_WithRegexPattern()
    {
        List<string> inputFiles = ["dir4/A1.txt", "dir5/B2.txt", "dir6/C3.txt", "dir7/_4.txt"];
        const string regexPattern = @"[A-Z](\d)\.txt";
        var output = new Dictionary<string, string>
        {
            { "dir4/A1.txt", "1" },
            { "dir5/B2.txt", "2" },
            { "dir6/C3.txt", "3" },
            { "dir7/_4.txt", "" }
        };

        Assert.That(CalculateFileKeys(inputFiles, regexPattern), Is.EqualTo(output));
    }

    [Test]
    public void InputEmptyFileList_ReturnsEmptyDictionary()
    {
        Assert.That(CalculateFileKeys(files: [], regexPattern: null), Is.Empty);
    }
}