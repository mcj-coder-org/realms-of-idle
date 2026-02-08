using RealmsOfIdle.Core.Domain;
using RealmsOfIdle.Core.Engine;
using Xunit;

namespace RealmsOfIdle.Core.Tests.Engine;

/// <summary>
/// Unit tests for TickProcessor
/// </summary>
[Trait("Category", "Unit")]
public class TickProcessorTests
{
    [Fact]
    public void ProcessTicks_WithZeroTicks_ReturnsEmptyStateChanges()
    {
        // Arrange
        var processor = new TickProcessor();
        var state = new TestGameState();

        // Act
        var changes = processor.ProcessTicks(state, 0);

        // Assert
        Assert.NotNull(changes);
        Assert.Empty(changes.Events);
        Assert.Equal(0, changes.TicksProcessed);
    }

    [Fact]
    public void ProcessTicks_WithPositiveTicks_ProcessesAllTicks()
    {
        // Arrange
        var processor = new TickProcessor();
        var state = new TestGameState();
        const int tickCount = 10;

        // Act
        var changes = processor.ProcessTicks(state, tickCount, (s, tickNum) =>
        {
            if (s is TestGameState testState)
            {
                testState.IncrementTick();
            }
        });

        // Assert
        Assert.Equal(tickCount, changes.TicksProcessed);
        Assert.Equal(tickCount, state.TickCount);
    }

    [Fact]
    public void ProcessTicks_RegistersTickEvents()
    {
        // Arrange
        var processor = new TickProcessor();
        var state = new TestGameState();
        const int tickCount = 5;

        // Act
        var changes = processor.ProcessTicks(state, tickCount);

        // Assert
        Assert.Equal(tickCount, changes.Events.Count);
    }

    [Fact]
    public void ProcessTicks_WithEventCallback_InvokesCallbackForEachTick()
    {
        // Arrange
        var processor = new TickProcessor();
        var state = new TestGameState();
        const int tickCount = 3;
        var callbackInvocations = 0;

        void OnTick(GameState state, int tickNumber)
        {
            callbackInvocations++;
        }

        // Act
        processor.ProcessTicks(state, tickCount, OnTick);

        // Assert
        Assert.Equal(tickCount, callbackInvocations);
    }

    /// <summary>
    /// Minimal test game state for TickProcessor tests
    /// </summary>
    private class TestGameState : GameState
    {
        public int TickCount { get; private set; }

        public TestGameState()
        {
            TickCount = 0;
        }

        public void IncrementTick()
        {
            TickCount++;
        }
    }
}
