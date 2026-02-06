using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Time.Testing;
using RealmsOfIdle.Server.Orleans.Grains;
using Xunit;
using Xunit.Abstractions;

namespace RealmsOfIdle.Server.Orleans.Tests;

/// <summary>
/// Integration tests for Orleans HealthGrain
/// MVP: Simplified tests without full cluster setup
/// </summary>
public class HealthGrainTests
{
    private readonly ITestOutputHelper _output;

    public HealthGrainTests(ITestOutputHelper output)
    {
        _output = output;
    }

    [Fact]
    public void HealthGrain_CanBeInstantiated()
    {
        // Act & Assert - should not throw
        var grain = new HealthGrain(NullLogger<HealthGrain>.Instance, TimeProvider.System);
        Assert.NotNull(grain);
    }

    [Fact]
    public async Task GetHealthStatusAsync_ReturnsHealthyStatus()
    {
        // Arrange
        var grain = new HealthGrain(NullLogger<HealthGrain>.Instance, TimeProvider.System);

        // Act
        var health = await grain.GetHealthStatusAsync();

        // Assert
        Assert.NotNull(health);
        Assert.Equal(Core.Domain.Models.HealthStatus.Healthy, health.Status);
        Assert.Equal(Core.Domain.Models.GameMode.Online, health.Mode);
        Assert.Equal("Active", health.SiloStatus);
    }

    [Fact]
    public async Task MultipleCalls_ReturnsConsistentResults()
    {
        // Arrange
        var grain = new HealthGrain(NullLogger<HealthGrain>.Instance, TimeProvider.System);

        // Act
        var health1 = await grain.GetHealthStatusAsync();
        await Task.Delay(10);
        var health2 = await grain.GetHealthStatusAsync();

        // Assert
        Assert.Equal(health1.Status, health2.Status);
        Assert.Equal(health1.Mode, health2.Mode);
    }

    [Fact]
    public async Task HealthTimestamp_IsRecent()
    {
        // Arrange
        var grain = new HealthGrain(NullLogger<HealthGrain>.Instance, TimeProvider.System);
        var before = DateTime.UtcNow;

        // Act
        var health = await grain.GetHealthStatusAsync();

        // Assert
        Assert.True(health.Timestamp >= before);
        Assert.True(health.Timestamp <= DateTime.UtcNow.AddSeconds(1));
    }

    [Fact]
    public async Task Health_ContainsAllRequiredFields()
    {
        // Arrange
        var grain = new HealthGrain(NullLogger<HealthGrain>.Instance, TimeProvider.System);

        // Act
        var health = await grain.GetHealthStatusAsync();

        // Assert
        Assert.NotNull(health.Status);
        Assert.NotNull(health.Mode);
        Assert.True(health.Timestamp > DateTime.MinValue);
    }

    [Fact]
    public async Task GetHealthStatusAsync_UsesInjectedTimeProvider()
    {
        // Arrange
        var fakeTime = new DateTimeOffset(2025, 6, 15, 12, 0, 0, TimeSpan.Zero);
        var fakeTimeProvider = new FakeTimeProvider(fakeTime);
        var grain = new HealthGrain(NullLogger<HealthGrain>.Instance, fakeTimeProvider);

        // Act
        var health = await grain.GetHealthStatusAsync();

        // Assert
        Assert.Equal(fakeTime.UtcDateTime, health.Timestamp);
    }
}
