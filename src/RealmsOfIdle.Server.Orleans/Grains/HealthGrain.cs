using Orleans;
using RealmsOfIdle.Core;
using RealmsOfIdle.Server.Orleans.Interfaces;

namespace RealmsOfIdle.Server.Orleans.Grains;

public class HealthGrain : Grain, IHealthGrain
{
    public Task<GameHealth> GetHealthStatusAsync()
    {
        return Task.FromResult(new GameHealth
        {
            Status = HealthStatus.Healthy,
            SiloStatus = "Active",
            Timestamp = DateTime.UtcNow
        });
    }
}