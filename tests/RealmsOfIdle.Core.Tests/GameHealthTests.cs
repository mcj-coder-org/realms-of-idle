using RealmsOfIdle.Core.Domain.Models;
using Xunit;
using Xunit.Abstractions;

namespace RealmsOfIdle.Core.Tests;

/// <summary>
/// Unit tests for GameHealth domain model
/// </summary>
public class GameHealthTests
{
    private readonly ITestOutputHelper _output;

    public GameHealthTests(ITestOutputHelper output)
    {
        _output = output;
    }

    [Fact]
    public void Constructor_WithAllParameters_CreatesValidInstance()
    {
        // Arrange
        var status = HealthStatus.Healthy;
        var mode = GameMode.Online;
        var timestamp = DateTime.UtcNow;
        var database = "postgresql";
        var siloStatus = "Active";
        var dependencies = new Dictionary<string, string>
        {
            ["cache"] = "redis",
            ["db"] = "postgresql"
        };

        // Act
        var health = new GameHealth(
            status,
            mode,
            timestamp,
            database,
            siloStatus,
            dependencies
        );

        // Assert
        Assert.Equal(status, health.Status);
        Assert.Equal(mode, health.Mode);
        Assert.Equal(timestamp, health.Timestamp);
        Assert.Equal(database, health.Database);
        Assert.Equal(siloStatus, health.SiloStatus);
        Assert.Equal(dependencies, health.Dependencies);
    }

    [Fact]
    public void Constructor_WithOnlyRequiredParameters_CreatesValidInstance()
    {
        // Arrange
        var status = HealthStatus.Degraded;
        var mode = GameMode.Offline;
        var timestamp = DateTime.UtcNow;

        // Act
        var health = new GameHealth(
            status,
            mode,
            timestamp
        );

        // Assert
        Assert.Equal(status, health.Status);
        Assert.Equal(mode, health.Mode);
        Assert.Equal(timestamp, health.Timestamp);
        Assert.Null(health.Database);
        Assert.Null(health.SiloStatus);
        Assert.Null(health.Dependencies);
    }

    [Fact]
    public void WithTypicalOnlineConfiguration_CreatesHealthyStatus()
    {
        // Act
        var health = new GameHealth(
            HealthStatus.Healthy,
            GameMode.Online,
            DateTime.UtcNow,
            "postgresql",
            "Active",
            new Dictionary<string, string>
            {
                ["cache"] = "healthy",
                ["db"] = "healthy"
            }
        );

        // Assert
        Assert.Equal(HealthStatus.Healthy, health.Status);
        Assert.Equal(GameMode.Online, health.Mode);
        Assert.Equal("postgresql", health.Database);
        Assert.Equal("Active", health.SiloStatus);
        Assert.NotNull(health.Dependencies);
        Assert.Equal("healthy", health.Dependencies["cache"]);
        Assert.Equal("healthy", health.Dependencies["db"]);
    }

    [Fact]
    public void WithTypicalOfflineConfiguration_CreatesHealthyStatus()
    {
        // Act
        var health = new GameHealth(
            HealthStatus.Healthy,
            GameMode.Offline,
            DateTime.UtcNow
        );

        // Assert
        Assert.Equal(HealthStatus.Healthy, health.Status);
        Assert.Equal(GameMode.Offline, health.Mode);
        Assert.Null(health.Database);
        Assert.Null(health.SiloStatus);
    }

    [Fact]
    public void IsUnhealthy_WhenStatusIsUnhealthy_ReturnsTrue()
    {
        // Arrange & Act
        var health = new GameHealth(
            HealthStatus.Unhealthy,
            GameMode.Online,
            DateTime.UtcNow
        );

        // Assert
        Assert.Equal(HealthStatus.Unhealthy, health.Status);
    }

    [Fact]
    public void IsDegraded_WhenStatusIsDegraded_ReturnsTrue()
    {
        // Arrange & Act
        var health = new GameHealth(
            HealthStatus.Degraded,
            GameMode.Online,
            DateTime.UtcNow,
            Dependencies: new Dictionary<string, string>
            {
                ["cache"] = "unhealthy"
            }
        );

        // Assert
        Assert.Equal(HealthStatus.Degraded, health.Status);
        Assert.NotNull(health.Dependencies);
        Assert.Equal("unhealthy", health.Dependencies["cache"]);
    }

    [Fact]
    public void HealthyStatus_CanBeAccessed()
    {
        // Act & Assert
        Assert.NotNull(HealthStatus.Healthy);
        Assert.Equal("Healthy", HealthStatus.Healthy.Name);
    }

    [Fact]
    public void UnhealthyStatus_CanBeAccessed()
    {
        // Act & Assert
        Assert.NotNull(HealthStatus.Unhealthy);
        Assert.Equal("Unhealthy", HealthStatus.Unhealthy.Name);
    }

    [Fact]
    public void DegradedStatus_CanBeAccessed()
    {
        // Act & Assert
        Assert.NotNull(HealthStatus.Degraded);
        Assert.Equal("Degraded", HealthStatus.Degraded.Name);
    }

    [Fact]
    public void OfflineMode_CanBeAccessed()
    {
        // Act & Assert
        Assert.NotNull(GameMode.Offline);
        Assert.Equal("Offline", GameMode.Offline.Name);
    }

    [Fact]
    public void OnlineMode_CanBeAccessed()
    {
        // Act & Assert
        Assert.NotNull(GameMode.Online);
        Assert.Equal("Online", GameMode.Online.Name);
    }

    [Fact]
    public void WithManyDependencies_RecordsAllDependencies()
    {
        // Arrange
        var dependencies = new Dictionary<string, string>
        {
            ["cache"] = "healthy",
            ["db"] = "healthy",
            ["api"] = "healthy",
            ["queue"] = "healthy",
            ["storage"] = "healthy"
        };

        // Act
        var health = new GameHealth(
            HealthStatus.Healthy,
            GameMode.Online,
            DateTime.UtcNow,
            Dependencies: dependencies
        );

        // Assert
        Assert.Equal(5, health.Dependencies!.Count);
    }

    [Fact]
    public void Record_WithSameValues_AreEqual()
    {
        // Arrange
        var timestamp = DateTime.UtcNow;
        var health1 = new GameHealth(
            HealthStatus.Healthy,
            GameMode.Offline,
            timestamp
        );
        var health2 = new GameHealth(
            HealthStatus.Healthy,
            GameMode.Offline,
            timestamp
        );

        // Assert
        Assert.Equal(health1, health2);
    }
}
