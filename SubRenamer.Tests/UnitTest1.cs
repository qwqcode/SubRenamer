namespace SubRenamer.Tests;

public class Tests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void Test1()
    {
        Assert.Pass();
    }
    
    [Test]
    public void GetStartPos()
    {
        var pos = Matcher.Diff.GetDiffResult([
            "[Kamigami] Shingeki no Kyojin - 01 [1920x1080 x264 AAC Sub(Chi,Jap)].mkv",
            "[Kamigami] Shingeki no Kyojin - 02 [1920x1080 x264 AAC Sub(GB,Big5,Jap)].mkv",
            "[Kamigami] Shingeki no Kyojin - 03 [1920x1080 x264 AAC Sub(GB,Big5,Jap)].mkv",
        ]);
        Console.WriteLine(pos?.Prefix);
        Console.WriteLine(pos?.Suffix);
    }
}