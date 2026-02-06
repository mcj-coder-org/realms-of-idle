using Microsoft.Extensions.Hosting;

namespace RealmsOfIdle.Server.Orleans;

#pragma warning disable CA1515
public static class Program
{
    public static async Task Main(string[] args)
    {
        var host = Host.CreateDefaultBuilder(args)
            .UseOrleans(siloBuilder =>
            {
                // Use development clustering by default (Aspire overrides via env vars)
                siloBuilder
                    .UseLocalhostClustering()
                    .AddMemoryGrainStorage("Default");
            })
            .Build();

        await host.RunAsync();
    }
}
