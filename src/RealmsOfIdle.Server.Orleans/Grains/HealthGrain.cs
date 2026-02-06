using Microsoft.Extensions.Logging;
using RealmsOfIdle.Server.Orleans.Interfaces;

namespace RealmsOfIdle.Server.Orleans.Grains;

#pragma warning disable CA1515
public class HealthGrain : Grain, IHealthGrain
{
    private readonly ILogger<HealthGrain> _logger;

    public HealthGrain(ILogger<HealthGrain> logger)
    {
        _logger = logger;
    }

    public Task<Core.Domain.Models.GameHealth> GetHealthStatusAsync()
    {
        // Grain is executing, so silo is active
        var siloStatus = "Active";

        var dependencies = new Dictionary<string, string>
        {
            ["grain"] = "HealthGrain[0] responding"
        };

        _logger.LogDebug("Health check: Status=Active");

        return Task.FromResult(new Core.Domain.Models.GameHealth(
            Status: Core.Domain.Models.HealthStatus.Healthy,
            Mode: Core.Domain.Models.GameMode.Online,
            Timestamp: DateTime.UtcNow,
            Database: "postgresql", // TODO: actual DB health check
            SiloStatus: siloStatus,
            Dependencies: dependencies
        ));
    }
}
