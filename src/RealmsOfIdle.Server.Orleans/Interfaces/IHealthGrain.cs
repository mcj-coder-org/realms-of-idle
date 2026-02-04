using Orleans;
using RealmsOfIdle.Core;

namespace RealmsOfIdle.Server.Orleans.Interfaces;

public interface IHealthGrain : IGrainWithIntegerKey
{
    Task<GameHealth> GetHealthStatusAsync();
}