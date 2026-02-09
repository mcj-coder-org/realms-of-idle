using Microsoft.Extensions.Diagnostics.HealthChecks;
using RealmsOfIdle.Core.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenTelemetryServices("RealmsOfIdle.Server.Orleans");

builder.UseOrleans(siloBuilder =>
{
    // Fixed ports workaround for Aspire 13.1 + Orleans 10 dynamic port issue
    // See: https://github.com/dotnet/aspire/issues/6940
    siloBuilder.UseLocalhostClustering(
        siloPort: 11111,
        gatewayPort: 30000);

    siloBuilder.AddMemoryGrainStorageAsDefault();
    siloBuilder.AddMemoryGrainStorage("PubSubStore");
});

// Health checks: silo self + PostgreSQL + Redis connectivity
var healthChecks = builder.Services.AddHealthChecks()
    .AddCheck("silo", () => HealthCheckResult.Healthy("Orleans silo is running"));

// Add PostgreSQL health check if connection string is available (injected by Aspire)
var pgConn = builder.Configuration.GetConnectionString("realmsOfIdle");
if (!string.IsNullOrEmpty(pgConn))
{
    healthChecks.AddNpgSql(pgConn, name: "postgresql");
}

// Add Redis health check if connection string is available (injected by Aspire)
var redisConn = builder.Configuration.GetConnectionString("redis");
if (!string.IsNullOrEmpty(redisConn))
{
    healthChecks.AddRedis(redisConn, name: "redis");
}

var app = builder.Build();
app.MapGet("/ping", () => Results.Text("pong", "text/plain"));
app.MapHealthChecks("/health");
await app.RunAsync();
