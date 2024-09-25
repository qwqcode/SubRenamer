using SubRenamer.Core;

namespace SubRenamer.Tests;

[SetUpFixture]
public class GlobalSetup
{
    [OneTimeSetUp]
    public void RunBeforeAnyTests()
    {
        MatcherLogger.SetWriter(TestContext.Progress);
    }

    [OneTimeTearDown]
    public void RunAfterAnyTests()
    {
        // ...
    }
}
