using Reqnroll;

namespace RealmsOfIdle.SimulationTests.Infrastructure;

/// <summary>
/// Extension methods for accessing test context from step bindings.
/// </summary>
public static class TestContextExtensions
{
    /// <summary>
    /// Gets or creates the GameTestContext from the scenario context.
    /// </summary>
    public static GameTestContext GetGameTestContext(this ScenarioContext context)
    {
        if (context.TryGetValue<GameTestContext>(out var testContext))
        {
            return testContext;
        }

        // This should never happen if TestHooks is working correctly
        var newContext = new GameTestContext();
        context.Set(newContext);
        return newContext;
    }

    /// <summary>
    /// Gets or creates the GameEngineDriver from the scenario context.
    /// </summary>
    public static Drivers.GameEngineDriver GetGameEngineDriver(this ScenarioContext context)
    {
        if (context.TryGetValue<Drivers.GameEngineDriver>(out var driver))
        {
            return driver;
        }

        var testContext = context.GetGameTestContext();
        var newDriver = new Drivers.GameEngineDriver(testContext);
        context.Set(newDriver);
        return newDriver;
    }
}
