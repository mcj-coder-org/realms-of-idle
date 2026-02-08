using RealmsOfIdle.Core.Engine;
using Xunit;

namespace RealmsOfIdle.Core.Tests.Engine;

/// <summary>
/// Unit tests for GameClock
/// </summary>
[Trait("Category", "Unit")]
public class GameClockTests
{
    [Fact]
    public void Constructor_InitializesWithCurrentTime()
    {
        // Arrange & Act
        var before = DateTime.UtcNow;
        var clock = new GameClock();
        var after = DateTime.UtcNow;

        // Assert
        Assert.InRange(clock.CurrentTime, before, after);
        Assert.Equal(0, clock.AccumulatedTicks);
        // LastActiveTime is initialized to current time when no parameter is provided
        Assert.NotNull(clock.LastActiveTime);
        Assert.InRange(clock.LastActiveTime.Value, before, after);
    }

    [Fact]
    public void Constructor_WithLastActiveTime_SetsLastActiveTime()
    {
        // Arrange
        var lastActive = DateTime.UtcNow.AddHours(-2);

        // Act
        var clock = new GameClock(lastActive);

        // Assert
        Assert.Equal(lastActive, clock.LastActiveTime);
        Assert.Equal(0, clock.AccumulatedTicks);
    }

    [Fact]
    public void RecordActivity_UpdatesLastActiveTime()
    {
        // Arrange
        var clock = new GameClock();
        var before = DateTime.UtcNow;

        // Act
        clock.RecordActivity();
        var after = DateTime.UtcNow;

        // Assert
        Assert.NotNull(clock.LastActiveTime);
        Assert.InRange(clock.LastActiveTime.Value, before, after);
    }

    [Fact]
    public void AccumulateTicks_IncreasesAccumulatedTicks()
    {
        // Arrange
        var clock = new GameClock();

        // Act
        clock.AccumulateTicks(10);

        // Assert
        Assert.Equal(10, clock.AccumulatedTicks);
    }

    [Fact]
    public void AccumulateTicks_MultipleCalls_Accumulates()
    {
        // Arrange
        var clock = new GameClock();

        // Act
        clock.AccumulateTicks(5);
        clock.AccumulateTicks(3);
        clock.AccumulateTicks(2);

        // Assert
        Assert.Equal(10, clock.AccumulatedTicks);
    }

    [Fact]
    public void ResetAccumulatedTicks_ResetsToZero()
    {
        // Arrange
        var clock = new GameClock();
        clock.AccumulateTicks(100);

        // Act
        clock.ResetAccumulatedTicks();

        // Assert
        Assert.Equal(0, clock.AccumulatedTicks);
    }

    [Fact]
    public void GetInactiveDuration_WithNoLastActiveTime_ReturnsZero()
    {
        // Arrange
        var lastActive = DateTime.MinValue; // Special value indicating no last active time
        var clock = new GameClock(lastActive);

        // Act
        var duration = clock.GetInactiveDuration(DateTime.UtcNow);

        // Assert
        Assert.Equal(TimeSpan.Zero, duration);
    }

    [Fact]
    public void GetInactiveDuration_WithLastActiveTime_ReturnsCorrectDuration()
    {
        // Arrange
        var lastActive = DateTime.UtcNow.AddHours(-3);
        var clock = new GameClock(lastActive);
        var now = DateTime.UtcNow.AddHours(-1);

        // Act
        var duration = clock.GetInactiveDuration(now);

        // Assert
        Assert.True(ApproximatelyEqual(TimeSpan.FromHours(2), duration));
    }

    private static bool ApproximatelyEqual(TimeSpan expected, TimeSpan actual, double toleranceSeconds = 1.0)
    {
        var diff = Math.Abs((expected - actual).TotalSeconds);
        return diff < toleranceSeconds;
    }
}
