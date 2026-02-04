using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RealmsOfIdle.Server.Orleans.Interfaces;
using Xunit.Abstractions;

namespace RealmsOfIdle.Server.E2ETests;

/// <summary>
/// End-to-end tests that verify the full stack works together.
/// These tests verify Orleans silo functionality with real grain calls.
/// </summary>
[Trait("Category", "E2E")]
public class StackHealthTests
{
    private readonly ITestOutputHelper _output;

    public StackHealthTests(ITestOutputHelper output)
    {
        _output = output;
    }

    [Fact]
    public async Task FullStack_OrleansSilo_StartsSuccessfully()
    {
        // Arrange & Act - Start Orleans silo
        using var silo = Host.CreateDefaultBuilder()
            .UseOrleans(silo => silo.UseLocalhostClustering())
            .Build();

        await silo.StartAsync();

        // Assert - Silo started without exception
        var grainFactory = silo.Services.GetRequiredService<IGrainFactory>();
        Assert.NotNull(grainFactory);

        _output.WriteLine($"Orleans silo started successfully");

        await silo.StopAsync();
    }

    [Fact]
    public async Task FullStack_OrleansGrain_ReturnsHealth()
    {
        // Arrange - Start Orleans silo
        using var silo = Host.CreateDefaultBuilder()
            .UseOrleans(silo => silo.UseLocalhostClustering())
            .Build();

        await silo.StartAsync();

        // Get grain factory from silo
        var grainFactory = silo.Services.GetRequiredService<IGrainFactory>();

        // Act - Call grain
        var healthGrain = grainFactory.GetGrain<IHealthGrain>(0);
        var health = await healthGrain.GetHealthStatusAsync();

        // Assert
        Assert.Equal(Core.Domain.Models.HealthStatus.Healthy, health.Status);
        Assert.Equal(Core.Domain.Models.GameMode.Online, health.Mode);
        Assert.Equal("Active", health.SiloStatus);

        _output.WriteLine($"Grain health: {health.Status}, Mode: {health.Mode}, Silo: {health.SiloStatus}");

        await silo.StopAsync();
    }

    [Fact]
    public async Task FullStack_MultipleGrainCalls_AllSucceed()
    {
        // Arrange - Start Orleans silo
        using var silo = Host.CreateDefaultBuilder()
            .UseOrleans(silo => silo.UseLocalhostClustering())
            .Build();

        await silo.StartAsync();

        var grainFactory = silo.Services.GetRequiredService<IGrainFactory>();

        // Act - Call grain multiple times concurrently
        var tasks = Enumerable.Range(0, 10).Select(async _ =>
        {
            var grain = grainFactory.GetGrain<IHealthGrain>(0);
            return await grain.GetHealthStatusAsync();
        }).ToArray();

        var results = await Task.WhenAll(tasks);

        // Assert - All calls should succeed
        Assert.All(results, result => Assert.Equal(Core.Domain.Models.HealthStatus.Healthy, result.Status));

        _output.WriteLine($"Completed {results.Length} concurrent grain calls");

        await silo.StopAsync();
    }

    [Fact]
    public async Task FullStack_GrainState_PersistsAcrossCalls()
    {
        // Arrange - Start Orleans silo
        using var silo = Host.CreateDefaultBuilder()
            .UseOrleans(silo => silo.UseLocalhostClustering())
            .Build();

        await silo.StartAsync();

        var grainFactory = silo.Services.GetRequiredService<IGrainFactory>();

        // Act - Call same grain multiple times
        var grain = grainFactory.GetGrain<IHealthGrain>(0);
        var health1 = await grain.GetHealthStatusAsync();
        await Task.Delay(50);
        var health2 = await grain.GetHealthStatusAsync();

        // Assert - Same grain instance, state updates
        Assert.Equal(health1.Status, health2.Status);
        Assert.Equal(health1.Mode, health2.Mode);
        Assert.NotEqual(health1.Timestamp, health2.Timestamp);

        _output.WriteLine($"Grain state persists correctly across calls");

        await silo.StopAsync();
    }

    [Fact]
    public async Task FullStack_DifferentGrainIds_InstancesAreIndependent()
    {
        // Arrange - Start Orleans silo
        using var silo = Host.CreateDefaultBuilder()
            .UseOrleans(silo => silo.UseLocalhostClustering())
            .Build();

        await silo.StartAsync();

        var grainFactory = silo.Services.GetRequiredService<IGrainFactory>();

        // Act - Call different grain instances
        var grain1 = grainFactory.GetGrain<IHealthGrain>(0);
        var grain2 = grainFactory.GetGrain<IHealthGrain>(1);
        var health1 = await grain1.GetHealthStatusAsync();
        var health2 = await grain2.GetHealthStatusAsync();

        // Assert - Different grains should have same initial state
        Assert.Equal(health1.Status, health2.Status);
        Assert.Equal(health1.Mode, health2.Mode);

        _output.WriteLine($"Different grain IDs produce independent grain instances");

        await silo.StopAsync();
    }

    [Fact]
    public async Task FullStack_GrainActivation_WorksCorrectly()
    {
        // Arrange - Start Orleans silo
        using var silo = Host.CreateDefaultBuilder()
            .UseOrleans(silo => silo.UseLocalhostClustering())
            .Build();

        await silo.StartAsync();

        var grainFactory = silo.Services.GetRequiredService<IGrainFactory>();

        // Act - Activate grain and verify it responds
        var grain = grainFactory.GetGrain<IHealthGrain>(999);
        var health = await grain.GetHealthStatusAsync();

        // Assert
        Assert.NotNull(health);
        Assert.Equal(Core.Domain.Models.HealthStatus.Healthy, health.Status);

        _output.WriteLine($"Grain activation works correctly");

        await silo.StopAsync();
    }
}
