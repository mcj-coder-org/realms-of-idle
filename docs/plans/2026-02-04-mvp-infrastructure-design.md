---
type: design
scope: mvp-infrastructure
status: proposed
version: 1.0.0
created: 2026-02-04
updated: 2026-02-04
subjects: [mvp, infrastructure, architecture, testing, logging]
dependencies: []
---

# MVP Infrastructure Design

## Purpose

Establish the foundational infrastructure for Realms of Idle that validates the architecture, enables offline/online play with a toggle, provides examples at each testing tier, and verifies logging and tracing are working.

## Success Criteria

- [ ] All components start and run healthily
- [ ] Each testing tier has at least one passing example
- [ ] Offline mode logs to LiteDB (7-day rolling window)
- [ ] Online mode logs appear in Aspire dashboard
- [ ] MAUI app can toggle between offline/online modes
- [ ] Blazor app works in online mode only
- [ ] E2E stack verification passes

---

## 1. Project Structure

```
RealmsOfIdle/
├── src/
│   ├── RealmsOfIdle.Core/              # Shared game engine (Portable)
│   │   ├── Domain/
│   │   │   ├── PlayerId.cs
│   │   │   ├── GameEvent.cs
│   │   │   └── ActionResult.cs
│   │   ├── Abstractions/
│   │   │   ├── IGameService.cs
│   │   │   ├── IEventStore.cs
│   │   │   └── IGameLogger.cs
│   │   └── Infrastructure/
│   │       ├── DeterministicRng.cs
│   │       ├── InMemoryEventStore.cs
│   │       └── TelemetryConfiguration.cs
│   │
│   ├── RealmsOfIdle.Client.Maui/       # MAUI client (offline-capable)
│   │   ├── Services/
│   │   │   ├── LocalGameService.cs
│   │   │   ├── MultiplayerGameService.cs
│   │   │   └── GameModeService.cs
│   │   ├── Storage/
│   │   │   ├── LiteDBEventStore.cs
│   │   │   └── LiteDBGameLogger.cs
│   │   ├── Models/
│   │   │   └── GameModeToggle.cs
│   │   └── MainPage.xaml
│   │
│   ├── RealmsOfIdle.Client.Blazor/     # Blazor WASM (online-only)
│   │   ├── Services/
│   │   │   └── MultiplayerGameService.cs
│   │   └── Pages/
│   │       └── Index.razor
│   │
│   ├── RealmsOfIdle.Server.Api/        # ASP.NET Core API
│   │   ├── Endpoints/
│   │   │   ├── HealthEndpoints.cs
│   │   │   ├── PlayerEndpoints.cs
│   │   │   └── ActionEndpoints.cs
│   │   ├── Hubs/
│   │   │   └── GameHub.cs
│   │   └── Program.cs
│   │
│   ├── RealmsOfIdle.Server.Orleans/    # Orleans grains
│   │   ├── Grains/
│   │   │   ├── PlayerGrain.cs
│   │   │   └── HealthGrain.cs
│   │   ├── Persistence/
│   │   │   └── MartenEventStore.cs
│   │   └── Program.cs
│   │
│   └── RealmsOfIdle.AppHost/           # .NET Aspire orchestrator
│       └── Program.cs
│
├── tests/
│   ├── RealmsOfIdle.Core.Tests/
│   │   └── HealthTests.cs
│   ├── RealmsOfIdle.IntegrationTests/
│   │   ├── OrleansFixture.cs
│   │   └── OrleansHealthTests.cs
│   ├── RealmsOfIdle.E2E.Tests/
│   │   └── StackHealthTests.cs
│   └── RealmsOfIdle.Performance.Tests/
│       └── HealthScenarios.cs
│
├── scripts/
│   ├── start-stack.ps1
│   └── verify-mvp.ps1
│
└── deploy/
    └── docker/
```

---

## 2. Service Abstraction & Game Mode Toggle

### 2.1 IGameService Interface

```csharp
namespace RealmsOfIdle.Core.Abstractions;

public interface IGameService
{
    Task<PlayerState> GetPlayerAsync(string playerId);
    Task<ActionResult> PerformActionAsync(string playerId, GameAction action);
    Task<GameHealth> GetHealthAsync();
    Task<SyncResult> SyncAsync(SyncCheckpoint checkpoint);
}
```

### 2.2 Game Mode Enum

```csharp
namespace RealmsOfIdle.Core.Models;

public enum GameMode
{
    Offline,
    Online
}
```

### 2.3 GameModeService

```csharp
namespace RealmsOfIdle.Client.Maui.Services;

public class GameModeService
{
    public GameMode CurrentMode { get; private set; }
    public event EventHandler<GameMode>? ModeChanged;

    private readonly IServiceProvider _services;
    private readonly LiteDatabase _offlineDb;

    public GameModeService(IServiceProvider services, LiteDatabase offlineDb)
    {
        _services = services;
        _offlineDb = offlineDb;
        CurrentMode = GameMode.Offline; // Default
    }

    public async Task<GameMode> SwitchModeAsync(GameMode newMode)
    {
        if (CurrentMode == newMode) return newMode;

        // Trigger sync before switching
        if (CurrentMode == GameMode.Online && newMode == GameMode.Offline)
        {
            await SyncForOfflineAsync();
        }
        if (CurrentMode == GameMode.Offline && newMode == GameMode.Online)
        {
            await SyncToOnlineAsync();
        }

        CurrentMode = newMode;
        ModeChanged?.Invoke(this, newMode);
        return newMode;
    }

    private async Task SyncForOfflineAsync()
    {
        // Download events from server to LiteDB
        var onlineService = _services.GetRequiredService<MultiplayerGameService>();
        var checkpoint = await onlineService.GetSyncCheckpointAsync();
        await onlineService.SyncEventsAsync(checkpoint, _offlineDb);
    }

    private async Task SyncToOnlineAsync()
    {
        // Upload local events from LiteDB to server
        var offlineService = _services.GetRequiredService<LocalGameService>();
        var onlineService = _services.GetRequiredService<MultiplayerGameService>();
        var events = await offlineService.GetUnsyncedEventsAsync();
        await onlineService.UploadEventsAsync(events);
    }
}
```

### 2.4 Service Factory

```csharp
namespace RealmsOfIdle.Client.Maui.Services;

public interface IGameServiceFactory
{
    IGameService CreateService(GameMode mode);
}

public class GameServiceFactory : IGameServiceFactory
{
    private readonly IServiceProvider _services;

    public GameServiceFactory(IServiceProvider services)
    {
        _services = services;
    }

    public IGameService CreateService(GameMode mode)
    {
        return mode switch
        {
            GameMode.Offline => _services.GetRequiredService<LocalGameService>(),
            GameMode.Online => _services.GetRequiredService<MultiplayerGameService>(),
            _ => throw new ArgumentException("Invalid game mode")
        };
    }
}
```

---

## 3. Offline Mode (LocalGameService)

### 3.1 LocalGameService Implementation

```csharp
namespace RealmsOfIdle.Client.Maui.Services;

public class LocalGameService : IGameService
{
    private readonly GameEngine _engine;
    private readonly LiteDBEventStore _eventStore;
    private readonly LiteDBGameLogger _logger;

    public LocalGameService(LiteDatabase db)
    {
        _eventStore = new LiteDBEventStore(db);
        _logger = new LiteDBGameLogger(db);
        _engine = new GameEngine(_eventStore, new DeterministicRng(0));
    }

    public Task<PlayerState> GetPlayerAsync(string playerId)
    {
        return _eventStore.LoadPlayerAsync(playerId);
    }

    public async Task<ActionResult> PerformActionAsync(string playerId, GameAction action)
    {
        var result = await _engine.ProcessActionAsync(playerId, action);
        await _logger.LogAsync(new GameEvent { Action = action });
        return result;
    }

    public Task<GameHealth> GetHealthAsync()
    {
        return Task.FromResult(new GameHealth
        {
            Status = HealthStatus.Healthy,
            Mode = GameMode.Offline,
            Database = _eventStore.IsHealthy ? "Healthy" : "Error"
        });
    }

    public Task<SyncResult> SyncAsync(SyncCheckpoint checkpoint)
    {
        // Offline mode: no-op
        return Task.FromResult(new SyncResult { Synced = 0 });
    }

    internal async Task<IReadOnlyList<GameEvent>> GetUnsyncedEventsAsync()
    {
        return await _eventStore.GetUnsyncedEventsAsync();
    }
}
```

### 3.2 LiteDBGameLogger (7-Day Rolling Window)

```csharp
namespace RealmsOfIdle.Client.Maui.Storage;

public class LiteDBGameLogger : IGameLogger
{
    private readonly ILiteCollection<GameLog> _logs;

    public LiteDBGameLogger(LiteDatabase db)
    {
        _logs = db.GetCollection<GameLog>("logs");
        _logs.EnsureIndex(x => x.Timestamp);
    }

    public async Task LogAsync(GameEvent @event)
    {
        var log = new GameLog
        {
            Id = ObjectId.NewId(),
            Timestamp = DateTime.UtcNow,
            EventType = @event.Type,
            PlayerId = @event.PlayerId,
            Data = JsonSerializer.Serialize(@event),
            Level = MapLogLevel(@event)
        };
        _logs.Insert(log);

        // Clean up logs older than 7 days
        var cutoff = DateTime.UtcNow.AddDays(-7);
        _logs.DeleteMany(x => x.Timestamp < cutoff);
    }

    public async Task<IReadOnlyList<GameLog>> GetRecentLogsAsync(int days = 7)
    {
        var cutoff = DateTime.UtcNow.AddDays(-days);
        return _logs.Query()
            .Where(x => x.Timestamp >= cutoff)
            .OrderByDescending(x => x.Timestamp)
            .ToList();
    }

    private LogLevel MapLogLevel(GameEvent @event) =>
        @event.Type switch
        {
            "Error" => LogLevel.Error,
            "Warning" => LogLevel.Warning,
            _ => LogLevel.Information
        };
}
```

---

## 4. Online Mode (MultiplayerGameService)

### 4.1 MultiplayerGameService Implementation

```csharp
namespace RealmsOfIdle.Client.Maui.Services;

public class MultiplayerGameService : IGameService
{
    private readonly HttpClient _httpClient;
    private readonly HubConnection _hubConnection;
    private readonly TelemetryGameLogger _logger;

    public MultiplayerGameService(HttpClient httpClient, TelemetryGameLogger logger)
    {
        _httpClient = httpClient;
        _logger = logger;
        _hubConnection = new HubConnectionBuilder()
            .WithUrl("https://localhost:7001/gamehub")
            .WithAutomaticReconnect()
            .Build();
    }

    public async Task<PlayerState> GetPlayerAsync(string playerId)
    {
        var response = await _httpClient.GetAsync($"/api/players/{playerId}");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<PlayerState>();
    }

    public async Task<ActionResult> PerformActionAsync(string playerId, GameAction action)
    {
        var response = await _httpClient.PostAsJsonAsync($"/api/players/{playerId}/actions", action);
        response.EnsureSuccessStatusCode();
        var result = await response.Content.ReadFromJsonAsync<ActionResult>();
        await _logger.LogAsync(new GameEvent { Action = action });
        return result;
    }

    public async Task<GameHealth> GetHealthAsync()
    {
        var response = await _httpClient.GetAsync("/health");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<GameHealth>();
    }

    public async Task<SyncResult> SyncAsync(SyncCheckpoint checkpoint)
    {
        return await _httpClient.PostAsJsonAsync("/api/sync", checkpoint)
            .ContinueWith(t => t.Result.Content.ReadFromJsonAsync<SyncResult>())
            .Result;
    }

    internal async Task UploadEventsAsync(IReadOnlyList<GameEvent> events)
    {
        await _httpClient.PostAsJsonAsync("/api/events/batch", events);
    }
}
```

### 4.2 TelemetryGameLogger (Aspire Dashboard)

```csharp
namespace RealmsOfIdle.Client.Maui.Services;

public class TelemetryGameLogger : IGameLogger
{
    private readonly ILogger _logger;
    private readonly ActivitySource _activitySource;

    public TelemetryGameLogger(ILogger logger, ActivitySource activitySource)
    {
        _logger = logger;
        _activitySource = activitySource;
    }

    public async Task LogAsync(GameEvent @event)
    {
        // Log to console (visible in Aspire dashboard)
        _logger.LogInformation("GameEvent: {EventType} - {PlayerId}",
            @event.Type, @event.PlayerId);

        // Add to distributed trace
        using var activity = _activitySource.StartActivity(@event.Type);
        activity?.SetTag("player.id", @event.PlayerId);
        activity?.SetTag("event.data", JsonSerializer.Serialize(@event));
    }

    public async Task<IReadOnlyList<GameLog>> GetRecentLogsAsync(int days = 7)
    {
        // Online mode: query from server via API
        // Returns logs from PostgreSQL/Marten event store
        throw new NotImplementedException("Query server logs via API");
    }
}
```

---

## 5. Blazor WASM (Online-Only)

```csharp
// RealmsOfIdle.Client.Blazor/Program.cs
builder.Services.AddScoped<MultiplayerGameService>();
builder.Services.AddScoped<IGameService>(sp => sp.GetRequiredService<MultiplayerGameService>());
builder.Services.AddScoped<TelemetryGameLogger>();

// No GameModeService - always online
// No LiteDB - no offline storage
```

---

## 6. Server Components

### 6.1 .NET Aspire AppHost

```csharp
// RealmsOfIdle.AppHost/Program.cs
var builder = DistributedApplication.CreateBuilder(args);

// PostgreSQL for event storage
var postgres = builder.AddPostgres("postgres")
    .WithLifetime(ContainerLifetime.Persistent);

var db = postgres.AddDatabase("idleworlds");

// Redis for Orleans clustering
var redis = builder.AddRedis("redis")
    .WithLifetime(ContainerLifetime.Persistent);

// Orleans Silo
var orleans = builder.AddProject<Projects.RealmsOfIdle_Server.Orleans>("orleans")
    .WithReference(db)
    .WithReference(redis)
    .WithEnvironment("ORLEANS_SILO_PRIMARY", "true")
    .WithHealthCheck("/health", timeout: TimeSpan.FromSeconds(30));

// API
var api = builder.AddProject<Projects.RealmsOfIdle.Server.Api>("api")
    .WithReference(orleans)
    .WithExternalHttpEndpoints(port: 7001);

builder.Build().Run();
```

### 6.2 HealthGrain (Orleans)

```csharp
// RealmsOfIdle.Server.Orleans/Grains/HealthGrain.cs
public interface IHealthGrain : IGrainWithStringKey
{
    Task<GameHealth> GetHealthAsync();
}

public class HealthGrain : Grain, IHealthGrain
{
    public Task<GameHealth> GetHealthAsync()
    {
        return Task.FromResult(new GameHealth
        {
            Status = HealthStatus.Healthy,
            SiloStatus = this.SiloAddress.Status.ToString(),
            Timestamp = DateTime.UtcNow
        });
    }
}
```

### 6.3 Health Endpoint (API)

```csharp
// RealmsOfIdle.Server.Api/Endpoints/HealthEndpoints.cs
public static class HealthEndpoints
{
    public static void MapHealthEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/health", async (IGrainFactory grainFactory) =>
        {
            var healthGrain = grainFactory.GetGrain<IHealthGrain>("health");
            var health = await healthGrain.GetHealthAsync();

            health.Dependencies = new Dictionary<string, string>
            {
                ["Database"] = "Healthy", // Check PostgreSQL
                ["Orleans"] = health.SiloStatus
            };

            return Results.Ok(health);
        });
    }
}
```

---

## 7. Logging & Tracing

### 7.1 OpenTelemetry Configuration

```csharp
// RealmsOfIdle.Core/Infrastructure/TelemetryConfiguration.cs
namespace RealmsOfIdle.Core.Infrastructure;

public static class TelemetryConfiguration
{
    public static void ConfigureOpenTelemetry(
        IServiceCollection services,
        string serviceName,
        string environment)
    {
        services.AddOpenTelemetry()
            .ConfigureResource(r => r
                .AddService(serviceName)
                .AddAttributes(new[]
                {
                    new KeyValuePair<string, object>("environment", environment)
                }))
            .WithTracing(t => t
                .AddSource(serviceName)
                .AddHttpClientInstrumentation()
                .AddAspNetCoreInstrumentation()
                .AddOrleansInstrumentation()
                .AddConsoleExporter()
                .AddOtlpExporter())
            .WithMetrics(m => m
                .AddMeter(serviceName)
                .AddHttpClientInstrumentation()
                .AddAspNetCoreInstrumentation()
                .AddConsoleExporter()
                .AddOtlpExporter());
    }
}
```

### 7.2 Logger Selection by Mode

```csharp
// MAUI Program.cs
builder.Services.AddSingleton<LiteDBGameLogger>();
builder.Services.AddSingleton<TelemetryGameLogger>();

builder.Services.AddSingleton<IGameLogger>(sp =>
{
    var mode = sp.GetRequiredService<GameModeService>().CurrentMode;
    return mode switch
    {
        GameMode.Offline => sp.GetRequiredService<LiteDBGameLogger>(),
        GameMode.Online => sp.GetRequiredService<TelemetryGameLogger>(),
        _ => throw new ArgumentException("Invalid mode")
    };
});
```

---

## 8. Testing Examples (Health Check Style)

### 8.1 Unit Test

```csharp
// RealmsOfIdle.Core.Tests/HealthTests.cs
[Trait("Category", "Unit")]
public class CoreHealthTests
{
    [Fact]
    public async Task Core_Components_Initialize()
    {
        // Arrange & Act
        var rng = new DeterministicRng(12345);
        var store = new InMemoryEventStore();
        var engine = new GameEngine(store, rng);

        // Assert
        engine.Should().NotBeNull();
        store.Should().NotBeNull();
    }
}
```

### 8.2 Integration Test

```csharp
// RealmsOfIdle.IntegrationTests/OrleansHealthTests.cs
[Trait("Category", "Integration")]
public class OrleansHealthTests : IClassFixture<OrleansFixture>
{
    private readonly OrleansFixture _fixture;

    public OrleansHealthTests(OrleansFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task Orleans_HealthGrain_ReturnsHealthy()
    {
        // Arrange
        var grain = _fixture.GrainFactory.GetGrain<IHealthGrain>("health");

        // Act
        var health = await grain.GetHealthAsync();

        // Assert
        health.Status.Should().Be(HealthStatus.Healthy);
        health.SiloStatus.Should().Be("Active");
    }
}
```

### 8.3 E2E Test

```csharp
// RealmsOfIdle.E2E.Tests/StackHealthTests.cs
[Trait("Category", "E2E")]
public class StackHealthTests : IClassInstance<PlaywrightInstance>
{
    private readonly IPage _page;

    public StackHealthTests(PlaywrightInstance instance)
    {
        _page = instance.Page;
    }

    [Fact]
    public async Task FullStack_HealthEndpoint_ReturnsHealthy()
    {
        // Arrange
        await _page.GotoAsync("https://localhost:7001/health");

        // Act
        var response = await _page.APIResponseAsync();
        var body = await response.BodyAsync();
        var health = JsonSerializer.Deserialize<HealthResponse>(body);

        // Assert
        response.Status.Should().Be(200);
        health.Status.Should().Be("Healthy");
        health.Dependencies["Database"].Should().Be("Healthy");
        health.Dependencies["Orleans"].Should().Be("Healthy");
    }
}
```

---

## 9. Verification Targets

| Component              | Health Check              | Command                                       |
| ---------------------- | ------------------------- | --------------------------------------------- |
| **Unit Tests**         | All pass                  | `dotnet test --filter "Category=Unit"`        |
| **Integration Tests**  | All pass                  | `dotnet test --filter "Category=Integration"` |
| **E2E Tests**          | All pass                  | `dotnet test --filter "Category=E2E"`         |
| **Aspire Stack**       | All containers healthy    | Dashboard shows green                         |
| **API Health**         | Returns 200               | `curl https://localhost:7001/health`          |
| **Orleans Grains**     | Silo active               | Dashboard shows grains                        |
| **Tracing (Online)**   | Traces visible            | Aspire dashboard shows trace timeline         |
| **Logging (Offline)**  | Logs in LiteDB            | Query logs collection, see last 7 days        |
| **Logging (Online)**   | Logs in dashboard         | Aspire console/logs show game events          |
| **MAUI Offline**       | App runs, actions work    | Manual verification                           |
| **MAUI Online Toggle** | Sync triggers on switch   | Manual verification                           |
| **Blazor**             | App loads, API calls work | Manual verification                           |

### 9.1 Verification Script

```powershell
# scripts/verify-mvp.ps1
Write-Host "🔍 Verifying MVP Infrastructure..." -ForegroundColor Cyan

# Unit tests
Write-Host "`n▶ Unit Tests" -ForegroundColor Yellow
dotnet test --filter "Category=Unit" --logger "console;verbosity=detailed"

# Integration tests
Write-Host "`n▶ Integration Tests" -ForegroundColor Yellow
dotnet test --filter "Category=Integration" --logger "console;verbosity=detailed"

# E2E tests
Write-Host "`n▶ E2E Tests" -ForegroundColor Yellow
dotnet test --filter "Category=E2E" --logger "console;verbosity=detailed"

# Start Aspire stack
Write-Host "`n▶ Starting Aspire Stack..." -ForegroundColor Yellow
Start-Process powershell -ArgumentList "-NoExit", "-Command", "dotnet run --project src/RealmsOfIdle.AppHost"

Write-Host "`n✨ MVP verification complete. Manual checks required:" -ForegroundColor Green
Write-Host "  - MAUI app offline mode (check LiteDB logs)" -ForegroundColor Gray
Write-Host "  - MAUI app online toggle (check sync triggers)" -ForegroundColor Gray
Write-Host "  - Blazor app (check API connectivity)" -ForegroundColor Gray
Write-Host "  - Aspire dashboard (check traces/logs)" -ForegroundColor Gray
```

---

## 10. Implementation Checklist

### Phase 1: Core & Abstractions

- [ ] `RealmsOfIdle.Core` project with minimal domain types
- [ ] `IGameService`, `IEventStore`, `IGameLogger` interfaces
- [ ] `DeterministicRng`, `InMemoryEventStore` implementations
- [ ] `TelemetryConfiguration` for OpenTelemetry

### Phase 2: Server Components

- [ ] `RealmsOfIdle.Server.Orleans` with `HealthGrain`
- [ ] `RealmsOfIdle.Server.Api` with health endpoint
- [ ] `RealmsOfIdle.AppHost` with Docker compose setup

### Phase 3: Client - Offline

- [ ] `RealmsOfIdle.Client.Maui` project structure
- [ ] `LocalGameService` with `LiteDBEventStore`
- [ ] `LiteDBGameLogger` with 7-day rolling window

### Phase 4: Client - Online

- [ ] `MultiplayerGameService` with HTTP client
- [ ] `TelemetryGameLogger` for Aspire
- [ ] `GameModeService` for toggle handling

### Phase 5: Blazor Client

- [ ] `RealmsOfIdle.Client.Blazor` project
- [ ] Online-only `MultiplayerGameService` usage

### Phase 6: Testing

- [ ] Unit test for Core health
- [ ] Integration test for Orleans health
- [ ] E2E test for full stack health

### Phase 7: Verification

- [ ] Verification script runs all tests
- [ ] Manual verification of offline logging (LiteDB)
- [ ] Manual verification of online logging (Aspire)
- [ ] Manual verification of MAUI toggle
- [ ] Manual verification of Blazor connectivity

---

## 11. Open Questions

1. Should the Aspire stack commit `docker-compose.yml` for users without .NET Aspire installed?
2. Should health checks include metrics (CPU, memory) or just status?
3. Should the 7-day log cleanup be time-triggered or on-demand?
4. Should sync conflicts be auto-resolved or require user intervention?

---

_Document Version 1.0 — MVP Infrastructure Design_
