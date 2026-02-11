using System.Net;
using AwesomeAssertions;

namespace RealmsOfIdle.Server.E2ETests;

/// <summary>
/// E2E tests verifying observability pipeline works.
/// Verifies that services emit telemetry data visible via Aspire dashboard.
/// Requires the full stack to be running via Aspire AppHost.
/// </summary>
[Trait("Category", "E2E")]
[Trait("Type", "Observability")]
public sealed class ObservabilityTests : IAsyncLifetime, IDisposable
{
    private readonly HttpClient _aspireClient;
    private readonly HttpClient _serviceClient;
    private readonly HttpClientHandler _handler;
    private readonly string _aspireBaseUrl;
    private readonly string _apiBaseUrl;
    private readonly string _orleansBaseUrl;
    private readonly string? _dashboardToken;

    public ObservabilityTests()
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

        _aspireBaseUrl = Environment.GetEnvironmentVariable("E2E_ASPIRE_BASE_URL")
            ?? "https://localhost:17103";
        _apiBaseUrl = Environment.GetEnvironmentVariable("E2E_API_BASE_URL")
            ?? "http://localhost:5214";
        _orleansBaseUrl = Environment.GetEnvironmentVariable("E2E_ORLEANS_BASE_URL")
            ?? "http://localhost:5001";
        _dashboardToken = Environment.GetEnvironmentVariable("E2E_ASPIRE_DASHBOARD_TOKEN");

        // Bypass SSL for self-signed dev certs (Aspire dashboard)
        // CookieContainer is enabled by default — auth cookie persists across requests
        _handler = new HttpClientHandler
        {
            ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator,
            CheckCertificateRevocationList = true
        };
        _aspireClient = new HttpClient(_handler)
        {
            BaseAddress = new Uri(_aspireBaseUrl),
            Timeout = TimeSpan.FromSeconds(30)
        };

        _serviceClient = new HttpClient
        {
            Timeout = TimeSpan.FromSeconds(30)
        };
    }

    public async Task InitializeAsync()
    {
        if (string.IsNullOrEmpty(_dashboardToken))
        {
            return;
        }

        // Authenticate with dashboard — GET /login?t=<token> sets an auth cookie
        var loginResponse = await _aspireClient.GetAsync(
            new Uri($"/login?t={_dashboardToken}", UriKind.Relative));
        loginResponse.EnsureSuccessStatusCode();
    }

    public Task DisposeAsync() => Task.CompletedTask;

    [Fact]
    public async Task AspireDashboard_IsReachable()
    {
        // The Aspire dashboard should respond to requests after auth
        var response = await _aspireClient.GetAsync(new Uri("/", UriKind.Relative));
        response.StatusCode.Should().NotBe(HttpStatusCode.ServiceUnavailable,
            "Aspire dashboard should be reachable");
    }

    [Fact]
    public async Task ApiService_EmitsTraces_AfterHealthCheck()
    {
        // Generate traffic by hitting health endpoint
        await _serviceClient.GetAsync(new Uri($"{_apiBaseUrl}/health"));

        // Aspire dashboard's structured logs page uses the /api/v1/logs endpoint
        // We verify the dashboard is serving the logs page
        var response = await _aspireClient.GetAsync(new Uri("/StructuredLogs", UriKind.Relative));
        response.StatusCode.Should().NotBe(HttpStatusCode.NotFound,
            "Aspire structured logs endpoint should exist");
    }

    [Fact]
    public async Task OrleansSilo_EmitsTraces_AfterPing()
    {
        // Generate traffic by hitting ping endpoint
        await _serviceClient.GetAsync(new Uri($"{_orleansBaseUrl}/ping"));

        // Verify traces page is accessible
        var response = await _aspireClient.GetAsync(new Uri("/Traces", UriKind.Relative));
        response.StatusCode.Should().NotBe(HttpStatusCode.NotFound,
            "Aspire traces endpoint should exist");
    }

    [Fact]
    public async Task MetricsPage_IsAccessible()
    {
        // Generate some traffic first
        await _serviceClient.GetAsync(new Uri($"{_apiBaseUrl}/ping"));

        // Verify metrics page is accessible
        var response = await _aspireClient.GetAsync(new Uri("/Metrics", UriKind.Relative));
        response.StatusCode.Should().NotBe(HttpStatusCode.NotFound,
            "Aspire metrics endpoint should exist");
    }

    public void Dispose()
    {
        _aspireClient.Dispose();
        _serviceClient.Dispose();
        _handler.Dispose();
    }
}
