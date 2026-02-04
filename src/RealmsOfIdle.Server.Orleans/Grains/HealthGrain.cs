using Orleans;
using Orleans.Runtime;
using RealmsOfIdle.Core;

namespace RealmsOfIdle.Server.Orleans.Grains;

public interface IHealthGrain : IGrainWithStringKey
{
    Task<GameHealth> GetHealthAsync();
}

public class HealthGrain : Grain, IHealthGrain
{
    public Task<GameHealth> GetHealthAsync()
    {
        return Task.FromResult(new GameHealth
        {
            Status = HealthStatus.Healthy,
            SiloStatus = this.SiloAddress.Status.ToString(),
            Timestamp = DateTime.UtcNow
        });
    }
}