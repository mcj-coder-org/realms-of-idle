#pragma warning disable CA1515

namespace RealmsOfIdle.Server.Orleans.Grains;

public class HealthGrain : Grain, Interfaces.IHealthGrain
{
    private readonly ILogger<HealthGrain> _logger;
    private readonly TimeProvider _timeProvider;

    public HealthGrain(ILogger<HealthGrain> logger, TimeProvider? timeProvider = null)
    {
        _logger = logger;
        _timeProvider = timeProvider ?? TimeProvider.System;
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
            Timestamp: _timeProvider.GetUtcNow().DateTime,
            Database: "postgresql", // TODO: actual DB health check
            SiloStatus: siloStatus,
            Dependencies: dependencies
        ));
    }
}
