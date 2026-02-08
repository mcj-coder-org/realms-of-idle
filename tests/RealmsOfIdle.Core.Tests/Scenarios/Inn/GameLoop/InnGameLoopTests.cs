using RealmsOfIdle.Core.Engine.Spatial;
using RealmsOfIdle.Core.Infrastructure;
using RealmsOfIdle.Core.Scenarios.Inn;
using RealmsOfIdle.Core.Scenarios.Inn.GameLoop;
using Xunit;

namespace RealmsOfIdle.Core.Tests.Scenarios.Inn.GameLoop;

/// <summary>
/// Unit tests for InnGameLoop
/// </summary>
[Trait("Category", "Unit")]
public class InnGameLoopTests
{
    [Fact]
    public void Constructor_WithInitialState_CreatesLoop()
    {
        // Arrange
        var layout = new WorldLayout();
        var facilities = new Dictionary<string, InnFacility>();
        var state = new InnState(layout, facilities);
        var rng = new DeterministicRng(42);

        // Act
        var gameLoop = new InnGameLoop(state, rng);

        // Assert
        Assert.NotNull(gameLoop);
        Assert.Equal(state, gameLoop.State);
        Assert.Equal(0, gameLoop.CurrentTick);
    }

    [Fact]
    public void ProcessTick_AdvancesTickCounter()
    {
        // Arrange
        var gameLoop = CreateTestGameLoop();

        // Act
        gameLoop.ProcessTick();

        // Assert
        Assert.Equal(1, gameLoop.CurrentTick);
    }

    [Fact]
    public void ProcessTick_MultipleTicks_Accumulates()
    {
        // Arrange
        var gameLoop = CreateTestGameLoop();

        // Act
        gameLoop.ProcessTick();
        gameLoop.ProcessTick();
        gameLoop.ProcessTick();

        // Assert
        Assert.Equal(3, gameLoop.CurrentTick);
    }

    [Fact]
    public void ProcessTick_UpdatesState()
    {
        // Arrange
        var gameLoop = CreateTestGameLoop();
        var originalState = gameLoop.State;

        // Act
        gameLoop.ProcessTick();

        // Assert
        Assert.NotSame(originalState, gameLoop.State);
    }

    [Fact]
    public void AddGold_UpdatesState()
    {
        // Arrange
        var gameLoop = CreateTestGameLoop();

        // Act
        gameLoop.AddGold(100);

        // Assert
        Assert.Equal(100, gameLoop.State.Gold);
    }

    [Fact]
    public void ProcessAction_WithInvalidAction_ReturnsFailure()
    {
        // Arrange
        var gameLoop = CreateTestGameLoop();
        var action = InnAction.UpgradeBar();
        var state = gameLoop.State.AddGold(10); // Not enough for upgrade

        // Act
        var result = gameLoop.ProcessAction(action);

        // Assert
        Assert.False(result.Success);
    }

    private static InnGameLoop CreateTestGameLoop()
    {
        var layout = new WorldLayout();
        var facilities = new Dictionary<string, InnFacility>();
        var state = new InnState(layout, facilities);
        var rng = new DeterministicRng(42);
        return new InnGameLoop(state, rng);
    }
}
