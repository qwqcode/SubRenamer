namespace SubRenamer.Tests;

[SetUpFixture]
public class GlobalSetup
{
    [OneTimeSetUp]
    public void RunBeforeAnyTests()
    {
        Matcher.Logger.SetWriter(TestContext.Progress);
    }

    [OneTimeTearDown]
    public void RunAfterAnyTests()
    {
        // ...
    }
}
