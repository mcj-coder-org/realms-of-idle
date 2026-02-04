using RealmsOfIdle.Core.Domain.Models;

namespace IdleWorld.Server.Interfaces;

internal interface IHealthGrain : IGrainWithIntegerKey
{
    Task<GameHealth> GetHealthStatusAsync();
}
