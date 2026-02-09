using Reqnroll;

namespace RealmsOfIdle.SimulationTests.Infrastructure;

/// <summary>
/// Reqnroll hooks for test setup and teardown.
/// Manages the lifecycle of the GameTestContext.
/// </summary>
[Binding]
public class TestHooks
{
    // Reqnroll 2.0 uses a different DI container approach
    // We'll use scenario context for storing the test context
    private readonly ScenarioContext _scenarioContext;

    public TestHooks(ScenarioContext scenarioContext)
    {
        _scenarioContext = scenarioContext;
    }

    /// <summary>
    /// Initializes the test context before each scenario.
    /// </summary>
    [BeforeScenario]
    public void BeforeScenario()
    {
        var context = new GameTestContext();
        _scenarioContext.Set(context);
    }

    /// <summary>
    /// Cleans up the test context after each scenario.
    /// </summary>
    [AfterScenario]
    public void AfterScenario()
    {
        if (_scenarioContext.TryGetValue<GameTestContext>(out var context))
        {
            context.Reset();
        }
    }
}
