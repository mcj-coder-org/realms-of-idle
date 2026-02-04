using Orleans;
using RealmsOfIdle.Core.Domain.Models;

namespace RealmsOfIdle.Server.Orleans.Interfaces;

public interface IHealthGrain : IGrainWithIntegerKey
{
    Task<GameHealth> GetHealthStatusAsync();
}