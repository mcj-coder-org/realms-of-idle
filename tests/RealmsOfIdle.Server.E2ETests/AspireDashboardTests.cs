using System.Net;
using System.Net.Http.Json;
using System.Text.Json.Serialization;
using AwesomeAssertions;

namespace RealmsOfIdle.Server.E2ETests;

/// <summary>
/// E2E tests verifying Aspire dashboard shows all expected resources.
/// Requires the full stack to be running via Aspire AppHost.
/// </summary>
[Trait("Category", "E2E")]
[Trait("Type", "Smoke")]
public sealed class AspireDashboardTests : IDisposable
{
    private readonly HttpClient _client;
    private readonly HttpClientHandler _handler;
    private readonly string _resourceUrl;

    public AspireDashboardTests()
    {
        // Load .env from repo root
        var dir = AppContext.BaseDirectory;
        while (dir != null && !File.Exists(Path.Combine(dir, "RealmsOfIdle.slnx")))
        {
            dir = Directory.GetParent(dir)?.FullName;
        }

        if (dir != null)
        {
            var envPath = Path.Combine(dir, ".env");
            if (File.Exists(envPath))
            {
                DotNetEnv.Env.Load(envPath);
            }
        }

        _resourceUrl = Environment.GetEnvironmentVariable("E2E_ASPIRE_RESOURCE_URL")
            ?? "https://localhost:22241";

        // Bypass SSL for self-signed dev certs
        _handler = new HttpClientHandler
        {
            ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator,
            CheckCertificateRevocationList = true
        };
        _client = new HttpClient(_handler)
        {
            BaseAddress = new Uri(_resourceUrl),
            Timeout = TimeSpan.FromSeconds(30)
        };
    }

    [Fact(Skip = "Aspire dashboard API requires authentication token - use service health endpoints instead")]
    public async Task Resources_ReturnsNonEmptyList()
    {
        var resources = await GetResourcesAsync();
        resources.Should().NotBeEmpty("Aspire should report at least one resource");
    }

    [Fact(Skip = "Aspire dashboard API requires authentication token - use service health endpoints instead")]
    public async Task AllResources_AreHealthy()
    {
        var resources = await GetResourcesAsync();
        resources.Should().AllSatisfy(r =>
            r.State.Should().BeOneOf("Running", "running", "Finished", "finished"),
            "All resources should be in a healthy state");
    }

    [Fact(Skip = "Aspire dashboard API requires authentication token - use service health endpoints instead")]
    public async Task ExpectedResources_Exist()
    {
        var resources = await GetResourcesAsync();
        var names = resources.Select(r => r.Name).ToList();

        names.Should().Contain("postgres");
        names.Should().Contain("redis");
        names.Should().Contain("orleans-silo");
        names.Should().Contain("api");
    }

    [Fact(Skip = "Aspire dashboard API requires authentication token - use service health endpoints instead")]
    public async Task OrleansSilo_IsRunning()
    {
        var resources = await GetResourcesAsync();
        var silo = resources.FirstOrDefault(r => r.Name == "orleans-silo");
        silo.Should().NotBeNull("Orleans silo resource should exist");
        silo!.State.Should().BeOneOf("Running", "running");
    }

    [Fact(Skip = "Aspire dashboard API requires authentication token - use service health endpoints instead")]
    public async Task Api_IsRunning()
    {
        var resources = await GetResourcesAsync();
        var api = resources.FirstOrDefault(r => r.Name == "api");
        api.Should().NotBeNull("API resource should exist");
        api!.State.Should().BeOneOf("Running", "running");
    }

    [Fact(Skip = "Aspire dashboard API requires authentication token - use service health endpoints instead")]
    public async Task PostgreSql_IsRunning()
    {
        var resources = await GetResourcesAsync();
        var pg = resources.FirstOrDefault(r => r.Name == "postgres");
        pg.Should().NotBeNull("PostgreSQL resource should exist");
        pg!.State.Should().BeOneOf("Running", "running");
    }

    [Fact(Skip = "Aspire dashboard API requires authentication token - use service health endpoints instead")]
    public async Task Redis_IsRunning()
    {
        var resources = await GetResourcesAsync();
        var redis = resources.FirstOrDefault(r => r.Name == "redis");
        redis.Should().NotBeNull("Redis resource should exist");
        redis!.State.Should().BeOneOf("Running", "running");
    }

    private async Task<List<ResourceInfo>> GetResourcesAsync()
    {
        var response = await _client.GetAsync(new Uri("/api/v1/resources", UriKind.Relative));
        response.StatusCode.Should().Be(HttpStatusCode.OK,
            $"Aspire resource API at {_resourceUrl} should be reachable");
        var resources = await response.Content.ReadFromJsonAsync<List<ResourceInfo>>();
        return resources ?? [];
    }

    public void Dispose()
    {
        _client.Dispose();
        _handler.Dispose();
    }
}

/// <summary>
/// DTO matching Aspire dashboard resource API format.
/// </summary>
internal sealed class ResourceInfo
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = "";

    [JsonPropertyName("state")]
    public string State { get; set; } = "";

    [JsonPropertyName("resourceType")]
    public string ResourceType { get; set; } = "";
}
