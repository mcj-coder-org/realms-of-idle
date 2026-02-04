using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RealmsOfIdle.Server.Orleans.Interfaces;
using Xunit;
using Xunit.Abstractions;

namespace RealmsOfIdle.Server.IntegrationTests;

/// <summary>
/// Integration tests for Orleans cluster.
/// These tests spin up a real Orleans silo and verify grain functionality.
/// </summary>
[Trait("Category", "Integration")]
public class OrleansClusterTests
{
    private readonly ITestOutputHelper _output;

    public OrleansClusterTests(ITestOutputHelper output)
    {
        _output = output;
    }

    [Fact]
    public async Task Cluster_StartsSuccessfully()
    {
        // Arrange
        using var host = Host.CreateDefaultBuilder()
            .UseOrleans(silo =>
            {
                silo.UseLocalhostClustering();
            })
            .Build();

        // Act
        await host.StartAsync();

        // Assert - host started without exception
        Assert.True(true);

        await host.StopAsync();
    }

    [Fact]
    public async Task HealthGrain_WithRealSilo_ReturnsHealthyStatus()
    {
        // Arrange
        using var host = Host.CreateDefaultBuilder()
            .UseOrleans(silo =>
            {
                silo.UseLocalhostClustering();
            })
            .Build();

        await host.StartAsync();

        // Act
        var grainFactory = host.Services.GetRequiredService<IGrainFactory>();
        var healthGrain = grainFactory.GetGrain<IHealthGrain>(0);
        var health = await healthGrain.GetHealthStatusAsync();

        // Assert
        Assert.NotNull(health);
        Assert.Equal(Core.Domain.Models.HealthStatus.Healthy, health.Status);
        Assert.Equal(Core.Domain.Models.GameMode.Online, health.Mode);
        Assert.Equal("Active", health.SiloStatus);

        await host.StopAsync();
    }

    [Fact]
    public async Task GrainFactory_CanResolveGrains()
    {
        // Arrange
        using var host = Host.CreateDefaultBuilder()
            .UseOrleans(silo =>
            {
                silo.UseLocalhostClustering();
            })
            .Build();

        await host.StartAsync();

        // Act
        var grainFactory = host.Services.GetRequiredService<IGrainFactory>();
        var grain1 = grainFactory.GetGrain<IHealthGrain>(0);
        var grain2 = grainFactory.GetGrain<IHealthGrain>(0);

        // Assert
        Assert.NotNull(grain1);
        Assert.NotNull(grain2);
        // Same grain ID should return same reference
        Assert.Equal(grain1, grain2);

        await host.StopAsync();
    }

    [Fact]
    public async Task MultipleGrainActivations_WorkCorrectly()
    {
        // Arrange
        using var host = Host.CreateDefaultBuilder()
            .UseOrleans(silo =>
            {
                silo.UseLocalhostClustering();
            })
            .Build();

        await host.StartAsync();

        // Act - get grain multiple times
        var grainFactory = host.Services.GetRequiredService<IGrainFactory>();
        var grain1 = grainFactory.GetGrain<IHealthGrain>(0);
        var health1 = await grain1.GetHealthStatusAsync();

        await Task.Delay(10);

        var grain2 = grainFactory.GetGrain<IHealthGrain>(0);
        var health2 = await grain2.GetHealthStatusAsync();

        // Assert
        Assert.Equal(health1.Status, health2.Status);
        Assert.Equal(health1.Mode, health2.Mode);
        // Timestamps should be different since they were called at different times
        Assert.NotEqual(health1.Timestamp, health2.Timestamp);

        await host.StopAsync();
    }
}
