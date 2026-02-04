namespace RealmsOfIdle.Server.Orleans.Interfaces;

public interface IHealthGrain : IGrainWithIntegerKey
{
    Task<Core.Domain.Models.GameHealth> GetHealthStatusAsync();
}