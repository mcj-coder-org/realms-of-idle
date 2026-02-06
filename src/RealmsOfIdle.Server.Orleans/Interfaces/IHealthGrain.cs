namespace RealmsOfIdle.Server.Orleans.Interfaces;

#pragma warning disable CA1515
public interface IHealthGrain : IGrainWithIntegerKey
{
    Task<Core.Domain.Models.GameHealth> GetHealthStatusAsync();
}