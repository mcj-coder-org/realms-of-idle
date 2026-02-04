using Orleans;
using RealmsOfIdle.Core;

namespace RealmsOfIdle.Server.Orleans.Grains;

public class HealthGrain : Grain, IHealthGrain
{
    public Task<GameHealth> GetHealthStatusAsync()
    {
        return Task.FromResult(new GameHealth
        {
            Status = HealthStatus.Healthy,
            SiloStatus = this.SiloAddress.Status.ToString(),
            Timestamp = DateTime.UtcNow
        });
    }
}