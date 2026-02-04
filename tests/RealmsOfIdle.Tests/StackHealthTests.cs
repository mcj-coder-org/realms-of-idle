using RealmsOfIdle.Core.Domain.Models;
using Xunit;
using Xunit.Abstractions;

namespace RealmsOfIdle.Tests;

/// <summary>
/// End-to-end tests for stack health verification
/// MVP: Basic health check that all components can respond
/// </summary>
public class StackHealthTests
{
    private readonly ITestOutputHelper _output;

    public StackHealthTests(ITestOutputHelper output)
    {
        _output = output;
    }

    [Fact]
    public void DomainModels_AreAccessible()
    {
        // Act & Assert - domain models should be accessible
        Assert.NotNull(HealthStatus.Healthy);
        Assert.NotNull(HealthStatus.Degraded);
        Assert.NotNull(HealthStatus.Unhealthy);
        Assert.NotNull(GameMode.Online);
        Assert.NotNull(GameMode.Offline);
    }

    [Fact]
    public void GameHealth_CanBeCreated()
    {
        // Arrange
        var timestamp = DateTime.UtcNow;

        // Act
        var health = new GameHealth(
            HealthStatus.Healthy,
            GameMode.Offline,
            timestamp
        );

        // Assert
        Assert.Equal(HealthStatus.Healthy, health.Status);
        Assert.Equal(GameMode.Offline, health.Mode);
        Assert.Equal(timestamp, health.Timestamp);
    }

    [Fact]
    public void StackComponents_CanBeInstantiated()
    {
        // Act & Assert - verify we can create core domain objects
        var healthOnline = new GameHealth(
            HealthStatus.Healthy,
            GameMode.Online,
            DateTime.UtcNow,
            "postgresql",
            "Active"
        );

        var healthOffline = new GameHealth(
            HealthStatus.Healthy,
            GameMode.Offline,
            DateTime.UtcNow
        );

        Assert.NotNull(healthOnline);
        Assert.NotNull(healthOffline);
    }

    [Fact]
    public void HealthStatusTransition_WorkFlow()
    {
        // Arrange
        var healthy = new GameHealth(
            HealthStatus.Healthy,
            GameMode.Online,
            DateTime.UtcNow
        );

        // Act - simulate status change
        var degraded = new GameHealth(
            HealthStatus.Degraded,
            GameMode.Online,
            DateTime.UtcNow,
            Dependencies: new Dictionary<string, string>
            {
                ["cache"] = "unhealthy"
            }
        );

        // Assert
        Assert.Equal(HealthStatus.Healthy, healthy.Status);
        Assert.Equal(HealthStatus.Degraded, degraded.Status);
    }

    [Fact]
    public void GameModeTransition_WorkFlow()
    {
        // Arrange
        var online = new GameHealth(
            HealthStatus.Healthy,
            GameMode.Online,
            DateTime.UtcNow
        );

        // Act - simulate offline mode
        var offline = new GameHealth(
            HealthStatus.Healthy,
            GameMode.Offline,
            DateTime.UtcNow
        );

        // Assert
        Assert.Equal(GameMode.Online, online.Mode);
        Assert.Equal(GameMode.Offline, offline.Mode);
        Assert.Null(offline.Database);  // Offline has no database
        Assert.Null(offline.SiloStatus); // Offline has no silo
    }
}
