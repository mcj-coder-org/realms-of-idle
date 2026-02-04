using Orleans;
using RealmsOfIdle.Core.Domain.Models;

namespace IdleWorld.Server.Interfaces;

public interface IHealthGrain : IGrainWithIntegerKey
{
    Task<GameHealth> GetHealthStatusAsync();
}