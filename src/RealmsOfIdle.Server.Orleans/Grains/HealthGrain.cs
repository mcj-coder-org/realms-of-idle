using RealmsOfIdle.Server.Orleans.Interfaces;

namespace RealmsOfIdle.Server.Orleans.Grains;

public class HealthGrain : Grain, IHealthGrain
{
    public Task<Core.Domain.Models.GameHealth> GetHealthStatusAsync()
    {
        return Task.FromResult(new Core.Domain.Models.GameHealth(
            Status: Core.Domain.Models.HealthStatus.Healthy,
            Mode: Core.Domain.Models.GameMode.Online,
            Timestamp: DateTime.UtcNow,
            SiloStatus: "Active"
        ));
    }
}