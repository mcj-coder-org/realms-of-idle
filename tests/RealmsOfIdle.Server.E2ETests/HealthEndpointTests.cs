using System.Net;
using AwesomeAssertions;

namespace RealmsOfIdle.Server.E2ETests;

/// <summary>
/// Black-box E2E tests for health and ping endpoints.
/// Requires the full stack to be running (via Aspire AppHost).
/// Uses .env for endpoint configuration.
/// </summary>
[Trait("Category", "E2E")]
public sealed class HealthEndpointTests : IDisposable
{
    private readonly HttpClient _client;
    private readonly string _apiBaseUrl;
    private readonly string _orleansBaseUrl;
    private readonly int _timeoutSeconds;

    public HealthEndpointTests()
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

        _apiBaseUrl = Environment.GetEnvironmentVariable("E2E_API_BASE_URL") ?? "http://localhost:5214";
        _orleansBaseUrl = Environment.GetEnvironmentVariable("E2E_ORLEANS_BASE_URL") ?? "http://localhost:5001";
        _timeoutSeconds = int.TryParse(Environment.GetEnvironmentVariable("E2E_TIMEOUT_SECONDS"), out var t) ? t : 30;

        _client = new HttpClient { Timeout = TimeSpan.FromSeconds(_timeoutSeconds) };
    }

    [Fact]
    public async Task Api_Health_ReturnsHealthy()
    {
        var response = await _client.GetAsync(new Uri($"{_apiBaseUrl}/health"));
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var content = await response.Content.ReadAsStringAsync();
        content.Should().Be("Healthy");
    }

    [Fact]
    public async Task Api_Ping_ReturnsPong()
    {
        var response = await _client.GetAsync(new Uri($"{_apiBaseUrl}/ping"));
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var content = await response.Content.ReadAsStringAsync();
        content.Should().Be("pong");
    }

    [Fact]
    public async Task Orleans_Health_ReturnsHealthy()
    {
        var response = await _client.GetAsync(new Uri($"{_orleansBaseUrl}/health"));
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var content = await response.Content.ReadAsStringAsync();
        content.Should().Be("Healthy");
    }

    [Fact]
    public async Task Orleans_Ping_ReturnsPong()
    {
        var response = await _client.GetAsync(new Uri($"{_orleansBaseUrl}/ping"));
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var content = await response.Content.ReadAsStringAsync();
        content.Should().Be("pong");
    }

    [Fact]
    public async Task Health_RespondsWithinTimeout()
    {
        var sw = System.Diagnostics.Stopwatch.StartNew();
        await _client.GetAsync(new Uri($"{_apiBaseUrl}/health"));
        sw.Stop();
        sw.Elapsed.Should().BeLessThan(TimeSpan.FromSeconds(5));
    }

    [Fact]
    public async Task ConcurrentRequests_AllSucceed()
    {
        var tasks = Enumerable.Range(0, 10).Select(_ =>
            _client.GetAsync(new Uri($"{_apiBaseUrl}/ping")));
        var responses = await Task.WhenAll(tasks);
        responses.Should().AllSatisfy(r => r.StatusCode.Should().Be(HttpStatusCode.OK));
    }

    [Fact]
    public async Task InvalidEndpoint_Returns404()
    {
        var response = await _client.GetAsync(new Uri($"{_apiBaseUrl}/nonexistent"));
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    public void Dispose()
    {
        _client.Dispose();
        GC.SuppressFinalize(this);
    }
}
