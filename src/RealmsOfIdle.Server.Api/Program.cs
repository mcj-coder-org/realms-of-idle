using Microsoft.Extensions.Diagnostics.HealthChecks;

var builder = WebApplication.CreateBuilder(args);

// Configure Orleans client to connect to local silo
builder.Services.AddOrleansClient(client =>
{
    client.UseLocalhostClustering();
});

// Register named HttpClient for Orleans silo health check
builder.Services.AddHttpClient("OrleansSilo", client =>
{
    // Service discovery URL with env var fallback
    var orleansUrl = builder.Configuration["services:orleans-silo:http:0"]
        ?? Environment.GetEnvironmentVariable("E2E_ORLEANS_BASE_URL")
        ?? "http://localhost:5001";
    client.BaseAddress = new Uri(orleansUrl);
    client.Timeout = TimeSpan.FromSeconds(5);
});

// Health checks: api self + Orleans silo + PostgreSQL + Redis
var healthChecks = builder.Services.AddHealthChecks()
    .AddCheck("api", () => HealthCheckResult.Healthy("API is running"))
    .AddCheck<OrleansHttpHealthCheck>("orleans");

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

app.Run();

#pragma warning disable CA1515
// Expose Program class for testing
public partial class Program;
#pragma warning restore CA1515

/// <summary>
/// Health check that verifies Orleans silo is reachable via HTTP /ping endpoint.
/// Returns Degraded (not throws) on failure to prevent cascading health failures.
/// </summary>
internal sealed class OrleansHttpHealthCheck : IHealthCheck
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ILogger<OrleansHttpHealthCheck> _logger;

    public OrleansHttpHealthCheck(IHttpClientFactory httpClientFactory, ILogger<OrleansHttpHealthCheck> logger)
    {
        _httpClientFactory = httpClientFactory;
        _logger = logger;
    }

    public async Task<HealthCheckResult> CheckHealthAsync(
        HealthCheckContext context,
        CancellationToken cancellationToken = default)
    {
        try
        {
            using var client = _httpClientFactory.CreateClient("OrleansSilo");
            var response = await client.GetAsync(new Uri("/ping", UriKind.Relative), cancellationToken);
            response.EnsureSuccessStatusCode();
            return HealthCheckResult.Healthy("Orleans silo is reachable");
        }
#pragma warning disable CA1031
        catch (Exception ex)
#pragma warning restore CA1031
        {
            _logger.LogWarning(ex, "Orleans health check failed");
            return HealthCheckResult.Degraded($"Orleans silo unavailable: {ex.Message}");
        }
    }
}
