using RealmsOfIdle.Core.Engine;
using Xunit;

namespace RealmsOfIdle.Core.Tests.Engine;

/// <summary>
/// Unit tests for OfflineCatchupCalculator
/// </summary>
[Trait("Category", "Unit")]
public class OfflineCatchupCalculatorTests
{
    [Fact]
    public void CalculateCatchupTicks_WithSameTime_ReturnsZero()
    {
        // Arrange
        var calculator = new OfflineCatchupCalculator(tickRate: 10);
        var lastActive = DateTime.UtcNow;
        var now = lastActive;

        // Act
        var ticks = calculator.CalculateCatchupTicks(lastActive, now);

        // Assert
        Assert.Equal(0, ticks);
    }

    [Fact]
    public void CalculateCatchupTicks_WithOneSecondDifference_CalculatesCorrectTicks()
    {
        // Arrange
        const int ticksPerSecond = 10;
        var calculator = new OfflineCatchupCalculator(tickRate: ticksPerSecond);
        var lastActive = DateTime.UtcNow.AddSeconds(-1);

        // Act
        var ticks = calculator.CalculateCatchupTicks(lastActive, DateTime.UtcNow);

        // Assert
        Assert.Equal(ticksPerSecond, ticks);
    }

    [Fact]
    public void CalculateCatchupTicks_WithMultipleSeconds_CalculatesCorrectTicks()
    {
        // Arrange
        const int ticksPerSecond = 10;
        const int seconds = 5;
        var calculator = new OfflineCatchupCalculator(tickRate: ticksPerSecond);
        var lastActive = DateTime.UtcNow.AddSeconds(-seconds);

        // Act
        var ticks = calculator.CalculateCatchupTicks(lastActive, DateTime.UtcNow);

        // Assert
        Assert.Equal(ticksPerSecond * seconds, ticks);
    }

    [Fact]
    public void CalculateCatchupTicks_WithNegativeTimeDifference_ReturnsZero()
    {
        // Arrange
        var calculator = new OfflineCatchupCalculator(tickRate: 10);
        var lastActive = DateTime.UtcNow.AddHours(1); // Future time
        var now = DateTime.UtcNow;

        // Act
        var ticks = calculator.CalculateCatchupTicks(lastActive, now);

        // Assert
        Assert.Equal(0, ticks);
    }

    [Fact]
    public void CalculateCatchupTicks_WithNoLastActiveTime_ReturnsZero()
    {
        // Arrange
        var calculator = new OfflineCatchupCalculator(tickRate: 10);

        // Act
        var ticks = calculator.CalculateCatchupTicks(null, DateTime.UtcNow);

        // Assert
        Assert.Equal(0, ticks);
    }

    [Fact]
    public void CalculateCatchupTicks_WithMaxCatchupTicks_LimitsResult()
    {
        // Arrange
        const int ticksPerSecond = 10;
        const int maxTicks = 100;
        var calculator = new OfflineCatchupCalculator(tickRate: ticksPerSecond, maxCatchupTicks: maxTicks);
        var lastActive = DateTime.UtcNow.AddSeconds(-100); // Would produce 1000 ticks

        // Act
        var ticks = calculator.CalculateCatchupTicks(lastActive, DateTime.UtcNow);

        // Assert
        Assert.Equal(maxTicks, ticks);
    }

    [Fact]
    public void CalculateCatchupTicks_WithDifferentTickRates_CalculatesCorrectly()
    {
        // Arrange
        const int ticksPerSecond = 60;
        var calculator = new OfflineCatchupCalculator(tickRate: ticksPerSecond);
        var lastActive = DateTime.UtcNow.AddSeconds(-10);

        // Act
        var ticks = calculator.CalculateCatchupTicks(lastActive, DateTime.UtcNow);

        // Assert
        Assert.Equal(ticksPerSecond * 10, ticks);
    }

    [Fact]
    public void Constructor_WithZeroTickRate_ThrowsArgumentOutOfRangeException()
    {
        // Act & Assert
        Assert.Throws<ArgumentOutOfRangeException>(() => new OfflineCatchupCalculator(0));
    }

    [Fact]
    public void Constructor_WithNegativeTickRate_ThrowsArgumentOutOfRangeException()
    {
        // Act & Assert
        Assert.Throws<ArgumentOutOfRangeException>(() => new OfflineCatchupCalculator(-10));
    }

    [Fact]
    public void Constructor_WithValidParameters_CreatesInstance()
    {
        // Act
        var calculator = new OfflineCatchupCalculator(tickRate: 30, maxCatchupTicks: 10000);

        // Assert
        Assert.NotNull(calculator);
    }
}
