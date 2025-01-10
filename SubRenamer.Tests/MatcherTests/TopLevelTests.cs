using System.Text.Json;
using SubRenamer.Core;
using SubRenamer.Helper;

namespace SubRenamer.Tests.MatcherTests;

[TestFixture]
public class TopLevelTests
{
    private record TestCase(string Name, List<MatchItem> Input, List<MatchItem> Output);

    private static IEnumerable<TestCaseData> TestData
    {
        get
        {
            var jsonData = File.ReadAllText(Path.Combine(TestContext.CurrentContext.TestDirectory,
                "MatcherTests/TopLevelTests.json"));
            var testCases = JsonSerializer.Deserialize<List<TestCase>>(jsonData)!;

            foreach (var testCase in testCases)
            {
                yield return new TestCaseData(testCase.Name, testCase.Input, testCase.Output).SetName(testCase.Name);
            }
        }
    }

    [Test, TestCaseSource(nameof(TestData))]
    public void TestCasesFromJson(string name, List<MatchItem> input, List<MatchItem> expected)
    {
        var normalizer = new MatcherFilenameNormalizer();
        var actual = Matcher.Execute(normalizer.Normalize(input));

        var jsonOpts = new JsonSerializerOptions { WriteIndented = true, Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping };
        TestContext.Progress.WriteLine("{1}\n\n  \ud83c\udf1f Matcher Test Case: {0}\n\n{1}", name, new string('=', 50));
        TestContext.Progress.WriteLine("{2}\n  {0}\n{2}\n{1}", "Input", JsonSerializer.Serialize(input, jsonOpts),
            new string('-', 50));
        TestContext.Progress.WriteLine("{2}\n  {0}\n{2}\n{1}", "Expected", JsonSerializer.Serialize(expected, jsonOpts),
            new string('-', 50));
        TestContext.Progress.WriteLine("{2}\n  {0}\n{2}\n{1}", "Actual", JsonSerializer.Serialize(actual, jsonOpts),
            new string('-', 50));

        Assert.That(normalizer.Denormalize(actual), Is.EqualTo(expected));
        normalizer.Clear();
    }
}