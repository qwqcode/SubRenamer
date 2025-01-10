using SubRenamer.Core;
using SubRenamer.Helper;

namespace SubRenamer.Tests.MatcherTests;

/// <summary>
/// Test for filename normalization
/// 
/// NFKC is means Unicode Normalization Form KC (Compatibility Composition)
/// https://unicode.org/reports/tr15/
/// </summary>
[TestFixture]
public class FilenameNfkcTests
{
    [Test]
    public void Basic()
    {
        var normalizer = new MatcherFilenameNormalizer();
        List<MatchItem> originalItems = [
            new("", "\u30CF\u309A", "\u30D5\u3099"),
        ];

        var normalizedItems = normalizer.Normalize(originalItems);
        
        Assert.That(normalizedItems, Is.EqualTo([
            new MatchItem("", "\u30D1", "\u30D6"),
        ]), "Normalize");
        
        Assert.That(normalizer.Denormalize(normalizedItems), Is.EqualTo(originalItems), "Denormalize");
        
        normalizer.Clear();
    }
}
