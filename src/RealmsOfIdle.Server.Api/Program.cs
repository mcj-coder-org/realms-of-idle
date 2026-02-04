using Microsoft.Extensions.Diagnostics.HealthChecks;
using RealmsOfIdle.Server.Orleans.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Configure Orleans client to connect to local silo
builder.Services.AddOrleansClient(client =>
{
    client.UseLocalhostClustering();
});

// Add standard health checks with custom Orleans health check
builder.Services.AddHealthChecks()
    .AddCheck<OrleansHealthCheck>("orleans")
    .AddCheck("api", () => HealthCheckResult.Healthy("API is running"));

var app = builder.Build();

// Standard health check endpoint at /health
app.MapHealthChecks("/health");

app.Run();

#pragma warning disable CA1515
// Expose Program class for testing
public partial class Program { }
#pragma warning restore CA1515

/// <summary>
/// Custom health check for Orleans cluster connectivity.
/// </summary>
internal class OrleansHealthCheck : IHealthCheck
{
    private readonly IGrainFactory _grainFactory;
    private readonly ILogger<OrleansHealthCheck> _logger;

    public OrleansHealthCheck(IGrainFactory grainFactory, ILogger<OrleansHealthCheck> logger)
    {
        _grainFactory = grainFactory;
        _logger = logger;
    }

    public async Task<HealthCheckResult> CheckHealthAsync(
        HealthCheckContext context,
        CancellationToken cancellationToken = default)
    {
        try
        {
            // Try to get a grain to verify Orleans connectivity
            var healthGrain = _grainFactory.GetGrain<IHealthGrain>(0);
            await healthGrain.GetHealthStatusAsync();

            return HealthCheckResult.Healthy("Orleans cluster is reachable");
        }
#pragma warning disable CA1031
        catch (Exception ex)
#pragma warning restore CA1031
        {
            _logger.LogWarning(ex, "Orleans health check failed");
            return HealthCheckResult.Degraded($"Orleans cluster unavailable: {ex.Message}");
        }
    }
}
