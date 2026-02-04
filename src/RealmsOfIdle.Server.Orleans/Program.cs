using RealmsOfIdle.Server.Orleans.Grains;
using Orleans;
using Orleans.Configuration;
using Orleans.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Configure Orleans Silo with localhost clustering
builder.Host.UseOrleans(siloBuilder =>
{
    siloBuilder
        .UseLocalhostClustering()
        .Configure<ClusterOptions>(options =>
        {
            options.ClusterId = "realms-of-idle-cluster";
            options.ServiceId = "realms-of-idle-service";
        })
        .Configure<EndpointOptions>(options =>
        {
            options.AdvertisedIPAddress = System.Net.IPAddress.Loopback;
            options.SiloPort = 11111;
            options.GatewayPort = 30000;
        })
        .Configure<MemoryGrainStorageOptions>(options =>
        {
            options.NumStorageGrains = 1;
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.MapGet("/health", async (IGrainFactory grainFactory) =>
{
    var healthGrain = grainFactory.GetGrain<IHealthGrain>("health");
    var health = await healthGrain.GetHealthAsync();

    return Results.Ok(new
    {
        Status = health.Status,
        SiloStatus = health.SiloStatus,
        Timestamp = health.Timestamp
    });
});

app.Run();