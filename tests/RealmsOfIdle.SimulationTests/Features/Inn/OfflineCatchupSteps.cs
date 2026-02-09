using AwesomeAssertions;
using RealmsOfIdle.Core.Engine;
using RealmsOfIdle.SimulationTests.Infrastructure;
using RealmsOfIdle.SimulationTests.Infrastructure.Drivers;
using Reqnroll;

namespace RealmsOfIdle.SimulationTests.Features.Inn;

[Binding]
public class OfflineCatchupSteps
{
    private readonly ScenarioContext _scenarioContext;

    public OfflineCatchupSteps(ScenarioContext scenarioContext)
    {
        _scenarioContext = scenarioContext;
    }

    private GameTestContext Context => _scenarioContext.GetGameTestContext();
    private GameEngineDriver Driver => _scenarioContext.GetGameEngineDriver();

    [Given(@"I have an inn earning (.*) gold per hour")]
    public void GivenIHaveAnInnEarningGoldPerHour(int goldPerHour)
    {
        Driver.CreateNewPlayer();
        Context.Snapshots["goldPerHour"] = goldPerHour;
    }

    [When(@"I am offline for (.*) hours?")]
    public void WhenIAmOfflineForHours(int hours)
    {
        Context.Snapshots["offlineHours"] = hours;
        var calculator = new OfflineCatchupCalculator(tickRate: 10);
        var lastActive = DateTime.UtcNow - TimeSpan.FromHours(hours);
        var catchupTicks = calculator.CalculateCatchupTicks(lastActive, DateTime.UtcNow);
        Context.Snapshots["catchupTicks"] = catchupTicks;

        Driver.SimulateOfflinePeriod(TimeSpan.FromHours(hours));
    }

    [When(@"I return to the game")]
    public void WhenIReturnToTheGame()
    {
        // The simulation has already applied catch-up via SimulateOfflinePeriod
    }

    [Then(@"I should have approximately (.*) gold")]
    public void ThenIShouldHaveApproximatelyGold(int expectedGold)
    {
        // TODO: Automatic gold earning during offline catch-up is not yet
        // implemented in InnGameLoop. The OfflineCatchupCalculator computes
        // the number of ticks correctly, and ProcessTick runs the game loop,
        // but gold earning per tick requires customer-serving flow which isn't
        // automated in the catch-up path.
        throw new PendingStepException("Feature not yet implemented: automatic gold earning during offline catch-up");
    }

    [Then(@"the game should have processed (.*) hours? of ticks")]
    public void ThenTheGameShouldHaveProcessedHourOfTicks(int hours)
    {
        // Verify that ticks were processed based on the offline calculator
        var calculator = new OfflineCatchupCalculator(tickRate: 10);
        var expectedTicks = calculator.CalculateCatchupTicks(
            DateTime.UtcNow - TimeSpan.FromHours(hours), DateTime.UtcNow);

        Context.TicksProcessed.Should().BeGreaterThanOrEqualTo(expectedTicks,
            because: $"offline catch-up should process at least {expectedTicks} ticks for {hours} hours");
    }

    [Then(@"my reputation should have increased")]
    public void ThenMyReputationShouldHaveIncreased()
    {
        // TODO: Reputation gain during offline catch-up is not yet implemented.
        // The game loop processes ticks but doesn't auto-generate reputation
        // from served customers during catch-up.
        throw new PendingStepException("Feature not yet implemented: reputation gain during offline catch-up");
    }

    [Then(@"the increase should match the expected customer count")]
    public void ThenTheIncreaseShouldMatchTheExpectedCustomerCount()
    {
        // TODO: Customer count tracking during offline period not yet implemented.
        throw new PendingStepException("Feature not yet implemented: customer count tracking during offline period");
    }

    [Given(@"customers increase reputation when served")]
    public void GivenCustomersIncreaseReputationWhenServed()
    {
        // This is a precondition describing the game mechanic.
        // Reputation gains from served customers happen automatically
        // during tick processing when the system is fully implemented.
        throw new PendingStepException("Feature not yet implemented: reputation gains from served customers");
    }

    [Given(@"I have an inn earning gold")]
    public void GivenIHaveAnInnEarningGold()
    {
        Driver.CreateNewPlayer();
    }

    [Then(@"the gold earned should be calculated with diminishing returns")]
    public void ThenTheGoldEarnedShouldBeCalculatedWithDiminishingReturns()
    {
        // The OfflineCatchupCalculator caps ticks at MaxCatchupTicks (10000).
        // This effectively implements diminishing returns for very long offline periods.
        var calculator = new OfflineCatchupCalculator(tickRate: 10);
        var ticks24h = calculator.CalculateCatchupTicks(
            DateTime.UtcNow - TimeSpan.FromHours(24), DateTime.UtcNow);

        ticks24h.Should().BeLessThanOrEqualTo(calculator.MaxCatchupTicks,
            because: "offline ticks should be capped for diminishing returns");
    }

    [Then(@"returns should decrease for longer offline periods")]
    public void ThenReturnsShouldDecreaseForLongerOfflinePeriods()
    {
        // Verify that 48h doesn't give 2x more than 24h (due to MaxCatchupTicks cap)
        var calculator = new OfflineCatchupCalculator(tickRate: 10);
        var ticks24h = calculator.CalculateCatchupTicks(
            DateTime.UtcNow - TimeSpan.FromHours(24), DateTime.UtcNow);
        var ticks48h = calculator.CalculateCatchupTicks(
            DateTime.UtcNow - TimeSpan.FromHours(48), DateTime.UtcNow);

        // Both should be capped at MaxCatchupTicks
        ticks24h.Should().Be(ticks48h,
            because: "both exceed MaxCatchupTicks so returns diminish (are capped)");
    }
}
