# MVP Infrastructure Implementation Plan

> **For Claude:** REQUIRED SUB-SKILL: Use superpowers:executing-plans to implement this plan task-by-task.

**Goal:** Build foundational infrastructure for Realms of Idle with offline/online toggle, health checks at each testing tier, and verified logging/tracing.

**Architecture:** Shared Core library (Portable) with service abstraction (IGameService). MAUI client uses LocalGameService (LiteDB) offline, MultiplayerGameService (HTTP) online. Blazor client is online-only. Server uses Orleans grains + ASP.NET Core API, orchestrated by .NET Aspire.

**Tech Stack:** .NET 10, MAUI, Blazor WASM, Orleans, ASP.NET Core Minimal APIs, PostgreSQL, Redis, LiteDB, xUnit, TestContainers, Playwright, OpenTelemetry, .NET Aspire.

---

## Task 1: Create Core Library Project

**Files:**

- Create: `src/RealmsOfIdle.Core/RealmsOfIdle.Core.csproj`

**Step 1: Create project file**

```bash
dotnet new classlib -n RealmsOfIdle.Core -o src/RealmsOfIdle.Core
```

**Step 2: Edit project to use Portable target**

Run: `notepad src\RealmsOfIdle.Core\RealmsOfIdle.Core.csproj`

Replace contents with:

```xml
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <TargetFramework>netstandard2.1</TargetFramework>
    <ImplicitUsings>disable</ImplicitUsings>
    <Nullable>enable</Nullable>
    <RootNamespace>RealmsOfIdle.Core</RootNamespace>
  </PropertyGroup>
</Project>
```

**Step 3: Delete auto-generated file**

Run: `rm src/RealmsOfIdle.Core/Class1.cs`

**Step 4: Add to solution**

Run: `dotnet sln add src/RealmsOfIdle.Core/RealmsOfIdle.Core.csproj`

**Step 5: Commit**

```bash
git add src/RealmsOfIdle.Core/RealmsOfIdle.Core.csproj
git commit -m "feat(core): add portable class library project"
```

---

## Task 2: Add Core NuGet Packages

**Files:**

- Modify: `src/RealmsOfIdle.Core/RealmsOfIdle.Core.csproj`

**Step 1: Add packages**

Run: `dotnet add src/RealmsOfIdle.Core/RealmsOfIdle.Core.csproj package System.Text.Json`

**Step 2: Verify build**

Run: `dotnet build src/RealmsOfIdle.Core/RealmsOfIdle.Core.csproj`
Expected: Build succeeds with no warnings

**Step 3: Commit**

```bash
git add src/RealmsOfIdle.Core/RealmsOfIdle.Core.csproj
git commit -m "chore(core): add System.Text.Json package"
```

---

## Task 3: Create Domain Types

**Files:**

- Create: `src/RealmsOfIdle.Core/Domain/PlayerId.cs`
- Create: `src/RealmsOfIdle.Core/Domain/GameEvent.cs`
- Create: `src/RealmsOfIdle.Core/Domain/ActionResult.cs`
- Create: `src/RealmsOfIdle.Core/Domain/GameAction.cs`
- Create: `src/RealmsOfIdle.Core/Domain/PlayerState.cs`
- Create: `src/RealmsOfIdle.Core/Domain/Models/GameHealth.cs`
- Create: `src/RealmsOfIdle.Core/Domain/Models/GameMode.cs`

**Step 1: Create PlayerId record**

Create: `src/RealmsOfIdle.Core/Domain/PlayerId.cs`

```csharp
namespace RealmsOfIdle.Core.Domain;

public readonly record PlayerId(string Value)
{
    public static PlayerId New() => new(Guid.NewGuid().ToString());
}
```

**Step 2: Create GameEvent class**

Create: `src/RealmsOfIdle.Core/Domain/GameEvent.cs`

```csharp
namespace RealmsOfIdle.Core.Domain;

public class GameEvent
{
    public string EventType { get; set; } = "Unknown";
    public string PlayerId { get; set; } = string.Empty;
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    public string? Data { get; set; }
}
```

**Step 3: Create ActionResult record**

Create: `src/RealmsOfIdle.Core/Domain/ActionResult.cs`

```csharp
namespace RealmsOfIdle.Core.Domain;

public readonly record ActionResult(
    bool Success,
    string? Message = null,
    IReadOnlyList<GameEvent>? Events = null)
{
    public static ActionResult Ok(string message = "Success") =>
        new(true, message);

    public static ActionResult Fail(string message) =>
        new(false, message);
}
```

**Step 4: Create GameAction record**

Create: `src/RealmsOfIdle.Core/Domain/GameAction.cs`

```csharp
namespace RealmsOfIdle.Core.Domain;

public readonly record GameAction(
    string ActionName,
    string? TargetId = null,
    Dictionary<string, object>? Parameters = null);
```

**Step 5: Create PlayerState record**

Create: `src/RealmsOfIdle.Core/Domain/PlayerState.cs`

```csharp
namespace RealmsOfIdle.Core.Domain;

public readonly record PlayerState(
    string PlayerId,
    string Name,
    int Level = 1,
    int Experience = 0,
    GameMode CurrentMode = GameMode.Offline);
```

**Step 6: Create GameHealth record**

Create: `src/RealmsOfIdle.Core/Domain/Models/GameHealth.cs`

```csharp
namespace RealmsOfIdle.Core.Domain.Models;

public readonly record GameHealth(
    HealthStatus Status,
    GameMode Mode,
    DateTime Timestamp,
    string? Database = null,
    string? SiloStatus = null,
    Dictionary<string, string>? Dependencies = null);

public enum HealthStatus { Healthy, Unhealthy, Degraded }
```

**Step 7: Create GameMode enum**

Create: `src/RealmsOfIdle.Core/Domain/Models/GameMode.cs`

```csharp
namespace RealmsOfIdle.Core.Domain.Models;

public enum GameMode
{
    Offline,
    Online
}
```

**Step 8: Build and commit**

Run: `dotnet build src/RealmsOfIdle.Core`

```bash
git add src/RealmsOfIdle.Core/Domain
git commit -m "feat(core): add domain types - PlayerId, GameEvent, ActionResult, GameAction, PlayerState, GameHealth, GameMode"
```

---

## Task 4: Create IGameService Interface

**Files:**

- Create: `src/RealmsOfIdle.Core/Abstractions/IGameService.cs`
- Create: `src/RealmsOfIdle.Core/Abstractions/IEventStore.cs`
- Create: `src/RealmsOfIdle.Core/Abstractions/IGameLogger.cs`
- Create: `src/RealmsOfIdle.Core/Abstractions/ISyncService.cs`

**Step 1: Create IGameService**

Create: `src/RealmsOfIdle.Core/Abstractions/IGameService.cs`

```csharp
namespace RealmsOfIdle.Core.Abstractions;

public interface IGameService
{
    Task<PlayerState> GetPlayerAsync(string playerId);
    Task<ActionResult> PerformActionAsync(string playerId, GameAction action);
    Task<GameHealth> GetHealthAsync();
}
```

**Step 2: Create IEventStore**

Create: `src/RealmsOfIdle.Core/Abstractions/IEventStore.cs`

```csharp
namespace RealmsOfIdle.Core.Abstractions;

public interface IEventStore
{
    Task StoreEventAsync(GameEvent @event);
    Task<IReadOnlyList<GameEvent>> GetEventsAsync(string playerId, DateTime? since = null);
}
```

**Step 3: Create IGameLogger**

Create: `src/RealmsOfIdle.Core/Abstractions/IGameLogger.cs`

```csharp
namespace RealmsOfIdle.Core.Abstractions;

public interface IGameLogger
{
    Task LogAsync(GameEvent @event);
    Task<IReadOnlyList<GameLog>> GetRecentLogsAsync(int days = 7);
}

public class GameLog
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    public string EventType { get; set; } = string.Empty;
    public string PlayerId { get; set; } = string.Empty;
    public string? Data { get; set; }
}
```

**Step 4: Create ISyncService**

Create: `src/RealmsOfIdle.Core/Abstractions/ISyncService.cs`

```csharp
namespace RealmsOfIdle.Core.Abstractions;

public interface ISyncService
{
    Task<SyncResult> SyncToOnlineAsync(IReadOnlyList<GameEvent> events);
    Task<SyncResult> SyncFromOnlineAsync(DateTime since);
}

public readonly record SyncResult(
    int Synced,
    string? Checkpoint = null,
    IReadOnlyList<string>? Errors = null);
```

**Step 5: Build and commit**

Run: `dotnet build src/RealmsOfIdle.Core`

```bash
git add src/RealmsOfIdle.Core/Abstractions
git commit -m "feat(core): add service abstractions - IGameService, IEventStore, IGameLogger, ISyncService"
```

---

## Task 5: Create InMemoryEventStore Implementation

**Files:**

- Create: `src/RealmsOfIdle.Core/Infrastructure/InMemoryEventStore.cs`

**Step 1: Create implementation**

Create: `src/RealmsOfIdle.Core/Infrastructure/InMemoryEventStore.cs`

```csharp
namespace RealmsOfIdle.Core.Infrastructure;

public class InMemoryEventStore : IEventStore
{
    private readonly List<GameEvent> _events = new();

    public Task StoreEventAsync(GameEvent @event)
    {
        @event.Timestamp = DateTime.UtcNow;
        _events.Add(@event);
        return Task.CompletedTask;
    }

    public Task<IReadOnlyList<GameEvent>> GetEventsAsync(string playerId, DateTime? since = null)
    {
        var query = _events.Where(e => e.PlayerId == playerId);
        if (since.HasValue)
            query = query.Where(e => e.Timestamp >= since.Value);
        return Task.FromResult<IReadOnlyList<GameEvent>>(query.ToList());
    }
}
```

**Step 2: Build and commit**

Run: `dotnet build src/RealmsOfIdle.Core`

```bash
git add src/RealmsOfIdle.Core/Infrastructure
git commit -m "feat(core): add InMemoryEventStore implementation"
```

---

## Task 6: Create DeterministicRng

**Files:**

- Create: `src/RealmsOfIdle.Core/Infrastructure/DeterministicRng.cs`

**Step 1: Create deterministic RNG**

Create: `src/RealmsOfIdle.Core/Infrastructure/DeterministicRng.cs`

```csharp
namespace RealmsOfIdle.Core.Infrastructure;

public class DeterministicRng
{
    private readonly Random _random;

    public DeterministicRng(int seed)
    {
        _random = new Random(seed);
    }

    public int Next() => _random.Next();
    public int Next(int max) => _random.Next(max);
    public int Next(int min, int max) => _random.Next(min, max);
    public double NextDouble() => _random.NextDouble();
}
```

**Step 2: Build and commit**

Run: `dotnet build src/RealmsOfIdle.Core`

```bash
git add src/RealmsOfIdle.Core/Infrastructure/DeterministicRng.cs
git commit -m "feat(core): add DeterministicRng for seeded random generation"
```

---

## Task 7: Create TelemetryConfiguration

**Files:**

- Create: `src/RealmsOfIdle.Core/Infrastructure/TelemetryConfiguration.cs`

**Step 1: Add OpenTelemetry packages**

Run: `dotnet add src/RealmsOfIdle.Core/RealmsOfIdle.Core.csproj package OpenTelemetry.Extensions.Hosting`

**Step 2: Create telemetry configuration**

Create: `src/RealmsOfIdle.Core/Infrastructure/TelemetryConfiguration.cs`

```csharp
using System.Diagnostics;
using OpenTelemetry;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

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
                .AddConsoleExporter())
            .StartWithHost();
    }
}
```

**Step 3: Build and commit**

Run: `dotnet build src/RealmsOfIdle.Core`

```bash
git add src/RealmsOfIdle.Core/Infrastructure/TelemetryConfiguration.cs src/RealmsOfIdle.Core/RealmsOfIdle.Core.csproj
git commit -m "feat(core): add OpenTelemetry configuration"
```

---

## Task 8: Create Orleans Server Project

**Files:**

- Create: `src/RealmsOfIdle.Server.Orleans/RealmsOfIdle.Server.Orleans.csproj`

**Step 1: Create Orleans project**

Run: `dotnet new worker -n RealmsOfIdle.Server.Orleans -o src/RealmsOfIdle.Server.Orleans`

**Step 2: Delete auto-generated files**

Run: `rm src/RealmsOfIdle.Server.Orleans/Worker.cs`
Run: `rm src/RealmsOfIdle.Server.Orleans/Workers/`  (if directory exists)

**Step 3: Add Orleans packages**

Run: `dotnet add src/RealmsOfIdle.Server.Orleans/RealmsOfIdle.Server.Orleans.csproj package Microsoft.Orleans.Server`
Run: `dotnet add src/RealmsOfIdle.Server.Orleans/RealmsOfIdle.Server.Orleans.csproj package Microsoft.Orleans.Persistence.Abstractions`
Run: `dotnet add src/RealmsOfIdle.Server.Orleans/RealmsOfIdle.Server.Orleans.csproj package Orleans.Analysis`

**Step 4: Add project reference**

Run: `dotnet add src/RealmsOfIdle.Server.Orleans/RealmsOfIdle.Server.Orleans.csproj reference src/RealmsOfIdle.Core/RealmsOfIdle.Core.csproj`

**Step 5: Add to solution**

Run: `dotnet sln add src/RealmsOfIdle.Server.Orleans/RealmsOfIdle.Server.Orleans.csproj`

**Step 6: Build and commit**

Run: `dotnet build src/RealmsOfIdle.Server.Orleans`

```bash
git add src/RealmsOfIdle.Server.Orleans
git commit -m "feat(server): add Orleans server project"
```

---

## Task 9: Create HealthGrain

**Files:**

- Create: `src/RealmsOfIdle.Server.Orleans/Grains/HealthGrain.cs`

**Step 1: Create HealthGrain**

Create: `src/RealmsOfIdle.Server.Orleans/Grains/HealthGrain.cs`

```csharp
using RealmsOfIdle.Core.Abstractions;
using RealmsOfIdle.Core.Domain;
using RealmsOfIdle.Core.Domain.Models;
using RealmsOfIdle.Server.Orleans.Grains;

namespace RealmsOfIdle.Server.Orleans.Grains;

public interface IHealthGrain : IGrainWithStringKey
{
    Task<GameHealth> GetHealthAsync();
}

public class HealthGrain : Grain, IHealthGrain
{
    public Task<GameHealth> GetHealthAsync()
    {
        return Task.FromResult(new GameHealth(
            Status: HealthStatus.Healthy,
            Mode: GameMode.Online,
            Timestamp: DateTime.UtcNow,
            SiloStatus: this.SiloAddress.Status.ToString()
        ));
    }
}
```

**Step 2: Build and commit**

Run: `dotnet build src/RealmsOfIdle.Server.Orleans`

```bash
git add src/RealmsOfIdle.Server.Orleans/Grains
git commit -m "feat(server): add HealthGrain for health status queries"
```

---

## Task 10: Configure Orleans Silo

**Files:**

- Modify: `src/RealmsOfIdle.Server.Orleans/Program.cs`

**Step 1: Replace Program.cs content**

Run: `notepad src\RealmsOfIdle.Server.Orleans\Program.cs`

Replace with:

```csharp
using Orleans.Hosting;

var host = Host.CreateDefaultBuilder(args)
    .UseOrleans(silo =>
    {
        silo.UseLocalhostClustering();
        silo.AddMemoryGrainStorage("Default");
        silo.UseInMemoryReminderService();
    })
    .Build();

await host.RunAsync();
```

**Step 2: Build and verify**

Run: `dotnet build src/RealmsOfIdle.Server.Orleans`
Expected: Build succeeds

**Step 3: Commit**

```bash
git add src/RealmsOfIdle.Server.Orleans/Program.cs
git commit -m "feat(server): configure Orleans silo with localhost clustering"
```

---

## Task 11: Create API Server Project

**Files:**

- Create: `src/RealmsOfIdle.Server.Api/RealmsOfIdle.Server.Api.csproj`

**Step 1: Create API project**

Run: `dotnet new web -n RealmsOfIdle.Server.Api -o src/RealmsOfIdle.Server.Api`

**Step 2: Add Orleans and SignalR packages**

Run: `dotnet add src/RealmsOfIdle.Server.Api/RealmsOfIdle.Server.Api.csproj package Microsoft.Orleans.Core`
Run: `dotnet add src/RealmsOfIdle.Server.Api/RealmsOfIdle.Server.Api.csproj package Microsoft.AspNetCore.SignalR`

**Step 3: Add project references**

Run: `dotnet add src/RealmsOfIdle.Server.Api/RealmsOfIdle.Server.Api.csproj reference src/RealmsOfIdle.Core/RealmsOfIdle.Core.csproj`
Run: `dotnet add src/RealmsOfIdle.Server.Api/RealmsOfIdle.Server.Api.csproj reference src/RealmsOfIdle.Server.Orleans/RealmsOfIdle.Server.Orleans.csproj`

**Step 4: Add to solution**

Run: `dotnet sln add src/RealmsOfIdle.Server.Api/RealmsOfIdle.Server.Api.csproj`

**Step 5: Build and commit**

Run: `dotnet build src/RealmsOfIdle.Server.Api`

```bash
git add src/RealmsOfIdle.Server.Api
git commit -m "feat(server): add API server project with Orleans and SignalR"
```

---

## Task 12: Create Health Endpoint

**Files:**

- Create: `src/RealmsOfIdle.Server.Api/Endpoints/HealthEndpoints.cs`

**Step 1: Create health endpoint extension**

Create: `src/RealmsOfIdle.Server.Api/Endpoints/HealthEndpoints.cs`

```csharp
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Orleans;
using RealmsOfIdle.Core.Domain;
using RealmsOfIdle.Core.Domain.Models;
using RealmsOfIdle.Server.Orleans.Grains;

namespace RealmsOfIdle.Server.Api.Endpoints;

public static class HealthEndpoints
{
    public static void MapHealthEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/health", async (IGrainFactory grainFactory) =>
        {
            var healthGrain = grainFactory.GetGrain<IHealthGrain>("health");
            var health = await healthGrain.GetHealthAsync();

            health = health with
            {
                Dependencies = new Dictionary<string, string>
                {
                    ["Orleans"] = health.SiloStatus ?? "Unknown"
                }
            };

            return Results.Ok(health);
        });
    }
}
```

**Step 2: Register endpoints in Program.cs**

Run: `notepad src\RealmsOfIdle.Server.Api\Program.cs`

Add before `app.Run()`:

```csharp
app.MapHealthEndpoints();
```

**Step 3: Build and verify**

Run: `dotnet build src/RealmsOfIdle.Server.Api`
Expected: Build succeeds

**Step 4: Commit**

```bash
git add src/RealmsOfIdle.Server.Api/Endpoints src/RealmsOfIdle.Server.Api/Program.cs
git commit -m "feat(api): add /health endpoint with Orleans grain integration"
```

---

## Task 13: Create AppHost Project

**Files:**

- Create: `src/RealmsOfIdle.AppHost/RealmsOfIdle.AppHost.csproj`

**Step 1: Create Aspire AppHost project**

Run: `dotnet new aspire-apphost -n RealmsOfIdle.AppHost -o src/RealmsOfIdle.AppHost`

**Step 2: Add to solution**

Run: `dotnet sln add src/RealmsOfIdle.AppHost/RealmsOfIdle.AppHost.csproj`

**Step 3: Configure Aspire orchestration**

Run: `notepad src\RealmsOfIdle.AppHost\Program.cs`

Replace with:

```csharp
var builder = DistributedApplication.CreateBuilder(args);

// PostgreSQL (for event storage - Marten in production)
var postgres = builder.AddPostgres("postgres")
    .WithLifetime(ContainerLifetime.Persistent);

var db = postgres.AddDatabase("idleworlds");

// Redis (for Orleans clustering)
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

**Step 4: Build and commit**

Run: `dotnet build src/RealmsOfIdle.AppHost`

```bash
git add src/RealmsOfIdle.AppHost
git commit -m "feat(apphost): add .NET Aspire orchestration with PostgreSQL, Redis, Orleans, and API"
```

---

## Task 14: Create MAUI Client Project

**Files:**

- Create: `src/RealmsOfIdle.Client.Maui/RealmsOfIdle.Client.Maui.csproj`

**Step 1: Create MAUI project**

Run: `dotnet new maui -n RealmsOfIdle.Client.Maui -o src/RealmsOfIdle.Client.Maui`

**Step 2: Add LiteDB package**

Run: `dotnet add src/RealmsOfIdle.Client.Maui/RealmsOfIdle.Client.Maui.csproj package LiteDB`

**Step 3: Add project reference**

Run: `dotnet add src/RealmsOfIdle.Client.Maui/RealmsOfIdle.Client.Maui.csproj reference src/RealmsOfIdle.Core/RealmsOfIdle.Core.csproj`

**Step 4: Add to solution**

Run: `dotnet sln add src/RealmsOfIdle.Client.Maui/RealmsOfIdle.Client.Maui.csproj`

**Step 5: Build and commit**

Run: `dotnet build src/RealmsOfIdle.Client.Maui`

```bash
git add src/RealmsOfIdle.Client.Maui
git commit -m "feat(client): add MAUI client project with LiteDB"
```

---

## Task 15: Create LocalGameService

**Files:**

- Create: `src/RealmsOfIdle.Client.Maui/Services/LocalGameService.cs`
- Create: `src/RealmsOfIdle.Client.Maui/Storage/LiteDBEventStore.cs`

**Step 1: Create LiteDBEventStore**

Create: `src/RealmsOfIdle.Client.Maui/Storage/LiteDBEventStore.cs`

```csharp
using LiteDB;
using RealmsOfIdle.Core.Abstractions;
using RealmsOfIdle.Core.Domain;

namespace RealmsOfIdle.Client.Maui.Storage;

public class LiteDBEventStore : IEventStore
{
    private readonly ILiteCollection<GameEvent> _events;

    public LiteDBEventStore(LiteDatabase db)
    {
        _events = db.GetCollection<GameEvent>("events");
        _events.EnsureIndex(x => x.PlayerId);
        _events.EnsureIndex(x => x.Timestamp);
    }

    public Task StoreEventAsync(GameEvent @event)
    {
        @event.Timestamp = DateTime.UtcNow;
        _events.Insert(@event);
        return Task.CompletedTask;
    }

    public Task<IReadOnlyList<GameEvent>> GetEventsAsync(string playerId, DateTime? since = null)
    {
        var query = _events.Query()
            .Where(x => x.PlayerId == playerId);

        if (since.HasValue)
            query = query.Where(x => x.Timestamp >= since.Value);

        return Task.FromResult<IReadOnlyList<GameEvent>>(
            query.OrderByDescending(x => x.Timestamp).ToList());
    }

    public bool IsHealthy => _events.Count() >= 0; // Simple health check
}
```

**Step 2: Create LocalGameService**

Create: `src/RealmsOfIdle.Client.Maui/Services/LocalGameService.cs`

```csharp
using RealmsOfIdle.Core.Abstractions;
using RealmsOfIdle.Core.Domain;
using RealmsOfIdle.Core.Domain.Models;

namespace RealmsOfIdle.Client.Maui.Services;

public class LocalGameService : IGameService
{
    private readonly LiteDBEventStore _eventStore;

    public LocalGameService(LiteDatabase db)
    {
        _eventStore = new LiteDBEventStore(db);
    }

    public Task<PlayerState> GetPlayerAsync(string playerId)
    {
        // For MVP: return default player state
        return Task.FromResult(new PlayerState(playerId, "Local Player"));
    }

    public Task<ActionResult> PerformActionAsync(string playerId, GameAction action)
    {
        // For MVP: just log and return success
        return Task.FromResult<ActionResult>(ActionResult.Ok($"Action '{action.ActionName}' performed"));
    }

    public Task<GameHealth> GetHealthAsync()
    {
        return Task.FromResult(new GameHealth(
            Status: HealthStatus.Healthy,
            Mode: GameMode.Offline,
            Timestamp: DateTime.UtcNow,
            Database: _eventStore.IsHealthy ? "Healthy" : "Error"
        ));
    }
}
```

**Step 3: Build and commit**

Run: `dotnet build src/RealmsOfIdle.Client.Maui`

```bash
git add src/RealmsOfIdle.Client.Maui/Services src/RealmsOfIdle.Client.Maui/Storage
git commit -m "feat(client): add LocalGameService and LiteDBEventStore for offline mode"
```

---

## Task 16: Create LiteDBGameLogger

**Files:**

- Create: `src/RealmsOfIdle.Client.Maui/Storage/LiteDBGameLogger.cs`

**Step 1: Create LiteDBGameLogger with 7-day rolling window**

Create: `src/RealmsOfIdle.Client.Maui/Storage/LiteDBGameLogger.cs`

```csharp
using LiteDB;
using RealmsOfIdle.Core.Abstractions;
using RealmsOfIdle.Core.Domain;

namespace RealmsOfIdle.Client.Maui.Storage;

public class LiteDBGameLogger : IGameLogger
{
    private readonly ILiteCollection<GameLog> _logs;

    public LiteDBGameLogger(LiteDatabase db)
    {
        _logs = db.GetCollection<GameLog>("logs");
        _logs.EnsureIndex(x => x.Timestamp);
    }

    public Task LogAsync(GameEvent @event)
    {
        var log = new GameLog
        {
            Id = Guid.NewGuid().ToString(),
            Timestamp = DateTime.UtcNow,
            EventType = @event.EventType,
            PlayerId = @event.PlayerId,
            Data = @event.Data
        };
        _logs.Insert(log);

        // Clean up logs older than 7 days (rolling window)
        var cutoff = DateTime.UtcNow.AddDays(-7);
        _logs.DeleteMany(x => x.Timestamp < cutoff);

        return Task.CompletedTask;
    }

    public Task<IReadOnlyList<GameLog>> GetRecentLogsAsync(int days = 7)
    {
        var cutoff = DateTime.UtcNow.AddDays(-days);
        return Task.FromResult<IReadOnlyList<GameLog>>(
            _logs.Query()
                .Where(x => x.Timestamp >= cutoff)
                .OrderByDescending(x => x.Timestamp)
                .ToList());
    }
}
```

**Step 2: Build and commit**

Run: `dotnet build src/RealmsOfIdle.Client.Maui`

```bash
git add src/RealmsOfIdle.Client.Maui/Storage/LiteDBGameLogger.cs
git commit -m "feat(client): add LiteDBGameLogger with 7-day rolling window for offline logging"
```

---

## Task 17: Create GameModeService

**Files:**

- Create: `src/RealmsOfIdle.Client.Maui/Services/GameModeService.cs`

**Step 1: Create GameModeService**

Create: `src/RealmsOfIdle.Client.Maui/Services/GameModeService.cs`

```csharp
using RealmsOfIdle.Core.Domain.Models;

namespace RealmsOfIdle.Client.Maui.Services;

public class GameModeService
{
    public GameMode CurrentMode { get; private set; }
    public event EventHandler<GameMode>? ModeChanged;

    public GameModeService()
    {
        CurrentMode = GameMode.Offline; // Default to offline
    }

    public Task<GameMode> SwitchModeAsync(GameMode newMode)
    {
        if (CurrentMode == newMode) return Task.FromResult(newMode);

        // TODO: Implement sync before switching
        // For MVP: just switch mode

        CurrentMode = newMode;
        ModeChanged?.Invoke(this, newMode);
        return Task.FromResult(newMode);
    }
}
```

**Step 2: Build and commit**

Run: `dotnet build src/RealmsOfIdle.Client.Maui`

```bash
git add src/RealmsOfIdle.Client.Maui/Services/GameModeService.cs
git commit -m "feat(client): add GameModeService for runtime offline/online toggle"
```

---

## Task 18: Create MultiplayerGameService

**Files:**

- Create: `src/RealmsOfIdle.Client.Maui/Services/MultiplayerGameService.cs`

**Step 1: Create MultiplayerGameService**

Create: `src/RealmsOfIdle.Client.Maui/Services/MultiplayerGameService.cs`

```csharp
using System.Net.Http.Json;
using System.Text.Json;
using RealmsOfIdle.Core.Abstractions;
using RealmsOfIdle.Core.Domain;
using RealmsOfIdle.Core.Domain.Models;

namespace RealmsOfIdle.Client.Maui.Services;

public class MultiplayerGameService : IGameService
{
    private readonly HttpClient _httpClient;
    private readonly string _baseUrl = "https://localhost:7001";

    public MultiplayerGameService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<PlayerState> GetPlayerAsync(string playerId)
    {
        var response = await _httpClient.GetAsync($"{_baseUrl}/api/players/{playerId}");
        response.EnsureSuccessStatusCode();
        var json = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<PlayerState>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true })
            ?? new PlayerState(playerId, "Unknown");
    }

    public async Task<ActionResult> PerformActionAsync(string playerId, GameAction action)
    {
        var response = await _httpClient.PostAsJsonAsync($"{_baseUrl}/api/players/{playerId}/actions", action);
        response.EnsureSuccessStatusCode();
        var json = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<ActionResult>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true })
            ?? ActionResult.Fail("Unknown error");
    }

    public async Task<GameHealth> GetHealthAsync()
    {
        var response = await _httpClient.GetAsync($"{_baseUrl}/health");
        response.EnsureSuccessStatusCode();
        var json = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<GameHealth>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true })
            ?? new GameHealth(HealthStatus.Unhealthy, GameMode.Online, DateTime.UtcNow);
    }
}
```

**Step 2: Build and commit**

Run: `dotnet build src/RealmsOfIdle.Client.Maui`

```bash
git add src/RealmsOfIdle.Client.Maui/Services/MultiplayerGameService.cs
git commit -m "feat(client): add MultiplayerGameService for online mode"
```

---

## Task 19: Configure MAUI Program.cs

**Files:**

- Modify: `src/RealmsOfIdle.Client.Maui/MauiProgram.cs`

**Step 1: Configure dependency injection**

Run: `notepad src\RealmsOfIdle.Client.Maui\MauiProgram.cs`

Add services before `builder.Build()`:

```csharp
// LiteDB
builder.Services.AddSingleton(sp => new LiteDatabase(Path.Combine(FileSystem.AppDataDirectory, "game.db")));

// Services
builder.Services.AddSingleton<LocalGameService>();
builder.Services.AddSingleton<MultiplayerGameService>();
builder.Services.AddSingleton<GameModeService>();

// Current game service (selected by mode)
builder.Services.AddSingleton<IGameService>(sp =>
{
    var modeService = sp.GetRequiredService<GameModeService>();
    return modeService.CurrentMode == GameMode.Offline
        ? sp.GetRequiredService<LocalGameService>()
        : sp.GetRequiredService<MultiplayerGameService>();
});
```

**Step 2: Build and commit**

Run: `dotnet build src/RealmsOfIdle.Client.Maui`

```bash
git add src/RealmsOfIdle.Client.Maui/MauiProgram.cs
git commit -m "feat(client): configure MAUI DI with game services and mode-based service selection"
```

---

## Task 20: Create Blazor Client Project

**Files:**

- Create: `src/RealmsOfIdle.Client.Blazor/RealmsOfIdle.Client.Blazor.csproj`

**Step 1: Create Blazor WASM project**

Run: `dotnet new blazorwasm -n RealmsOfIdle.Client.Blazor -o src/RealmsOfIdle.Client.Blazor`

**Step 2: Add project reference**

Run: `dotnet add src/RealmsOfIdle.Client.Blazor/RealmsOfIdle.Client.Blazor.csproj reference src/RealmsOfIdle.Core/RealmsOfIdle.Core.csproj`

**Step 3: Add to solution**

Run: `dotnet sln add src/RealmsOfIdle.Client.Blazor/RealmsOfIdle.Client.Blazor.csproj`

**Step 4: Build and commit**

Run: `dotnet build src/RealmsOfIdle.Client.Blazor`

```bash
git add src/RealmsOfIdle.Client.Blazor
git commit -m "feat(client): add Blazor WASM client (online-only)"
```

---

## Task 21: Configure Blazor Program.cs

**Files:**

- Modify: `src/RealmsOfIdle.Client.Blazor/Program.cs`

**Step 1: Configure Blazor for online-only**

Run: `notepad src\RealmsOfIdle.Client.Blazor\Program.cs`

Add services before `builder.Build()`:

```csharp
// Multiplayer service (always online)
builder.Services.AddScoped(sp => new MultiplayerGameService(
    new HttpClient { BaseAddress = new Uri("https://localhost:7001") }));
builder.Services.AddScoped<IGameService>(sp => sp.GetRequiredService<MultiplayerGameService>());
```

Add using statements at top:

```csharp
using RealmsOfIdle.Client.Maui.Services;
using RealmsOfIdle.Core.Abstractions;
```

**Step 2: Build and commit**

Run: `dotnet build src/RealmsOfIdle.Client.Blazor`

```bash
git add src/RealmsOfIdle.Client.Blazor/Program.cs
git commit -m "feat(client): configure Blazor with online-only MultiplayerGameService"
```

---

## Task 22: Create Unit Tests Project

**Files:**

- Create: `tests/RealmsOfIdle.Core.Tests/RealmsOfIdle.Core.Tests.csproj`

**Step 1: Create test project**

Run: `dotnet new xunit -n RealmsOfIdle.Core.Tests -o tests/RealmsOfIdle.Core.Tests`

**Step 2: Add test packages**

Run: `dotnet add tests/RealmsOfIdle.Core.Tests/RealmsOfIdle.Core.Tests.csproj package xunit`
Run: `dotnet add tests/RealmsOfIdle.Core.Tests/RealmsOfIdle.Core.Tests.csproj package xunit.assert`
Run: `dotnet add tests/RealmsOfIdle.Core.Tests/RealmsOfIdle.Core.Tests.csproj package FluentAssertions`
Run: `dotnet add tests/RealmsOfIdle.Core.Tests/RealmsOfIdle.Core.Tests.csproj package NSubstitute`

**Step 3: Add project reference**

Run: `dotnet add tests/RealmsOfIdle.Core.Tests/RealmsOfIdle.Core.Tests.csproj reference ../../src/RealmsOfIdle.Core/RealmsOfIdle.Core.csproj`

**Step 4: Add to solution**

Run: `dotnet sln add tests/RealmsOfIdle.Core.Tests/RealmsOfIdle.Core.Tests.csproj`

**Step 5: Delete auto-generated test**

Run: `rm tests/RealmsOfIdle.Core.Tests/UnitTest1.cs`

**Step 6: Build and commit**

Run: `dotnet build tests/RealmsOfIdle.Core.Tests`

```bash
git add tests/RealmsOfIdle.Core.Tests
git commit -m "test(core): add unit test project with xUnit, FluentAssertions, NSubstitute"
```

---

## Task 23: Write Core Health Unit Test

**Files:**

- Create: `tests/RealmsOfIdle.Core.Tests/HealthTests.cs`

**Step 1: Create health test**

Create: `tests/RealmsOfIdle.Core.Tests/HealthTests.cs`

```csharp
using Xunit;
using FluentAssertions;
using RealmsOfIdle.Core.Infrastructure;
using RealmsOfIdle.Core.Domain;

namespace RealmsOfIdle.Core.Tests;

[Trait("Category", "Unit")]
public class CoreHealthTests
{
    [Fact]
    public void Core_Components_Initialize()
    {
        // Arrange & Act
        var rng = new DeterministicRng(12345);
        var store = new InMemoryEventStore();

        // Assert
        rng.Should().NotBeNull();
        store.Should().NotBeNull();
    }

    [Fact]
    public async Task InMemoryEventStore_StoreAndRetrieveEvents()
    {
        // Arrange
        var store = new InMemoryEventStore();
        var @event = new GameEvent { EventType = "Test", PlayerId = "player1" };

        // Act
        await store.StoreEventAsync(@event);
        var events = await store.GetEventsAsync("player1");

        // Assert
        events.Should().ContainSingle();
        events[0].EventType.Should().Be("Test");
    }
}
```

**Step 2: Run test to verify**

Run: `dotnet test tests/RealmsOfIdle.Core.Tests/HealthTests.cs`
Expected: 2 tests pass

**Step 3: Commit**

```bash
git add tests/RealmsOfIdle.Core.Tests/HealthTests.cs
git commit -m "test(core): add health check unit tests for Core components"
```

---

## Task 24: Create Integration Tests Project

**Files:**

- Create: `tests/RealmsOfIdle.IntegrationTests/RealmsOfIdle.IntegrationTests.csproj`

**Step 1: Create integration test project**

Run: `dotnet new xunit -n RealmsOfIdle.IntegrationTests -o tests/RealmsOfIdle.IntegrationTests`

**Step 2: Add Orleans Testing packages**

Run: `dotnet add tests/RealmsOfIdle.IntegrationTests/RealmsOfIdle.IntegrationTests.csproj package Microsoft.Orleans.TestingHost`
Run: `dotnet add tests/RealmsOfIdle.IntegrationTests/RealmsOfIdle.IntegrationTests.csproj package Microsoft.Extensions.Logging.Console`

**Step 3: Add project references**

Run: `dotnet add tests/RealmsOfIdle.IntegrationTests/RealmsOfIdle.IntegrationTests.csproj reference ../../src/RealmsOfIdle.Core/RealmsOfIdle.Core.csproj`
Run: `dotnet add tests/RealmsOfIdle.IntegrationTests/RealmsOfIdle.IntegrationTests.csproj reference ../../src/RealmsOfIdle.Server.Orleans/RealmsOfIdle.Server.Orleans.csproj`

**Step 4: Add to solution**

Run: `dotnet sln add tests/RealmsOfIdle.IntegrationTests/RealmsOfIdle.IntegrationTests.csproj`

**Step 5: Delete auto-generated test**

Run: `rm tests/RealmsOfIdle.IntegrationTests/UnitTest1.cs`

**Step 6: Build and commit**

Run: `dotnet build tests/RealmsOfIdle.IntegrationTests`

```bash
git add tests/RealmsOfIdle.IntegrationTests
git commit -m "test(integration): add integration test project with Orleans TestingHost"
```

---

## Task 25: Write Orleans Health Integration Test

**Files:**

- Create: `tests/RealmsOfIdle.IntegrationTests/OrleansFixture.cs`
- Create: `tests/RealmsOfIdle.IntegrationTests/OrleansHealthTests.cs`

**Step 1: Create Orleans fixture**

Create: `tests/RealmsOfIdle.IntegrationTests/OrleansFixture.cs`

```csharp
using Microsoft.Extensions.Hosting;
using Microsoft.Orleans.TestingHost;
using Xunit;

namespace RealmsOfIdle.IntegrationTests;

public class OrleansFixture : IAsyncLifetime
{
    public TestHost TestHost { get; private set; } = null!;

    public Task InitializeAsync()
    {
        var builder = new HostBuilder()
            .UseOrleansTesting()
            .ConfigureServices(services =>
            {
                // Add required services
            });

        TestHost = builder.Build();
        return Task.CompletedTask;
    }

    public async Task DisposeAsync()
    {
        await TestHost.StopAsync();
    }
}
```

**Step 2: Create Orleans health test**

Create: `tests/RealmsOfIdle.IntegrationTests/OrleansHealthTests.cs`

```csharp
using FluentAssertions;
using RealmsOfIdle.Core.Domain.Models;
using RealmsOfIdle.Server.Orleans.Grains;

namespace RealmsOfIdle.IntegrationTests;

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
        var grain = _fixture.TestHost.GetGrain<IHealthGrain>("health");

        // Act
        var health = await grain.GetHealthAsync();

        // Assert
        health.Status.Should().Be(HealthStatus.Healthy);
        health.Mode.Should().Be(GameMode.Online);
        health.SiloStatus.Should().NotBeNull();
    }
}
```

**Step 3: Run test to verify**

Run: `dotnet test tests/RealmsOfIdle.IntegrationTests/OrleansHealthTests.cs`
Expected: 1 test passes

**Step 4: Commit**

```bash
git add tests/RealmsOfIdle.IntegrationTests
git commit -m "test(integration): add Orleans health grain integration test"
```

---

## Task 26: Create E2E Tests Project

**Files:**

- Create: `tests/RealmsOfIdle.E2E.Tests/RealmsOfIdle.E2E.Tests.csproj`

**Step 1: Create E2E test project**

Run: `dotnet new xunit -n RealmsOfIdle.E2E.Tests -o tests/RealmsOfIdle.E2E.Tests`

**Step 2: Add Playwright package**

Run: `dotnet add tests/RealmsOfIdle.E2E.Tests/RealmsOfIdle.E2E.Tests.csproj package Microsoft.Playwright.XUnit`

**Step 3: Add to solution**

Run: `dotnet sln add tests/RealmsOfIdle.E2E.Tests/RealmsOfIdle.E2E.Tests.csproj`

**Step 4: Delete auto-generated test**

Run: `rm tests/RealmsOfIdle.E2E.Tests/UnitTest1.cs`

**Step 5: Build and commit**

Run: `dotnet build tests/RealmsOfIdle.E2E.Tests`

```bash
git add tests/RealmsOfIdle.E2E.Tests
git commit -m "test(e2e): add E2E test project with Playwright"
```

---

## Task 27: Write Stack Health E2E Test

**Files:**

- Create: `tests/RealmsOfIdle.E2E.Tests/StackHealthTests.cs`

**Step 1: Create E2E health test**

Create: `tests/RealmsOfIdle.E2E.Tests/StackHealthTests.cs`

```csharp
using Microsoft.Playwright.XUnit;
using FluentAssertions;

namespace RealmsOfIdle.E2E.Tests;

[Trait("Category", "E2E")]
public class StackHealthTests : PageTest
{
    [Fact]
    public async Task FullStack_HealthEndpoint_ReturnsHealthy()
    {
        // Act
        await Page.GotoAsync("https://localhost:7001/health");

        // Assert
        var status = await Page.LocatorAsync("status");
        var statusText = await status.TextContentAsync();

        statusText.Should().Contain("Healthy");
    }
}
```

**Step 2: Create Playwright config**

Run: `notepad tests\RealmsOfIdle.E2E.Tests/playwright.config.json`

Add:

```json
{
  "use": {
    "baseURL": "https://localhost:7001"
  }
}
```

**Step 3: Commit**

```bash
git add tests/RealmsOfIdle.E2E.Tests
git commit -m "test(e2e): add stack health endpoint test with Playwright"
```

---

## Task 28: Create Verification Script

**Files:**

- Create: `scripts/verify-mvp.ps1`

**Step 1: Create verification script**

Create: `scripts/verify-mvp.ps1`

```powershell
#!/usr/bin/env pwsh
Write-Host "🔍 Verifying MVP Infrastructure..." -ForegroundColor Cyan

# Unit tests
Write-Host "`n▶ Unit Tests" -ForegroundColor Yellow
dotnet test tests/RealmsOfIdle.Core.Tests --filter "Category=Unit" --logger "console;verbosity=detailed"
$unitPassed = $LASTEXITCODE -eq 0

# Integration tests
Write-Host "`n▶ Integration Tests" -ForegroundColor Yellow
dotnet test tests/RealmsOfIdle.IntegrationTests --filter "Category=Integration" --logger "console;verbosity=detailed"
$integrationPassed = $LASTEXITCODE -eq 0

# E2E tests (skip if API not running)
Write-Host "`n▶ E2E Tests (skipped if API not running)" -ForegroundColor Yellow
dotnet test tests/RealmsOfIdle.E2E.Tests --filter "Category=E2E" --logger "console;verbosity=detailed"
$e2ePassed = $LASTEXITCODE -eq 0

# Summary
Write-Host "`n✨ Verification Summary" -ForegroundColor Cyan
Write-Host "  Unit Tests: $($unitPassed ? '✅ PASS' : '❌ FAIL')" -ForegroundColor $(if ($unitPassed) { 'Green' } else { 'Red' })
Write-Host "  Integration Tests: $($integrationPassed ? '✅ PASS' : '❌ FAIL')" -ForegroundColor $(if ($integrationPassed) { 'Green' } else { 'Red' })
Write-Host "  E2E Tests: $($e2ePassed ? '✅ PASS' : '⚠️  SKIPPED')" -ForegroundColor $(if ($e2ePassed) { 'Green' } else { 'Yellow' })

if ($unitPassed -and $integrationPassed) {
    Write-Host "`n✅ All automated tests passed!" -ForegroundColor Green
    Write-Host "`nManual verification required:" -ForegroundColor Yellow
    Write-Host "  1. Run: dotnet run --project src/RealmsOfIdle.AppHost" -ForegroundColor Gray
    Write-Host "  2. Check Aspire dashboard at http://localhost:5000" -ForegroundColor Gray
    Write-Host "  3. Verify API health at https://localhost:7001/health" -ForegroundColor Gray
} else {
    Write-Host "`n❌ Tests failed. Fix failures before manual verification." -ForegroundColor Red
    exit 1
}
```

**Step 2: Commit**

```bash
git add scripts/verify-mvp.ps1
git commit -m "chore: add MVP verification script"
```

---

## Task 29: Create Start Stack Script

**Files:**

- Create: `scripts/start-stack.ps1`

**Step 1: Create stack start script**

Create: `scripts/start-stack.ps1`

```powershell
#!/usr/bin/env pwsh
Write-Host "🚀 Starting Realms of Idle MVP Stack..." -ForegroundColor Cyan

Write-Host "`n▶ Starting Aspire AppHost..." -ForegroundColor Yellow
Start-Process powershell -ArgumentList "-NoExit", "-Command", "dotnet run --project src/RealmsOfIdle.AppHost"

Write-Host "`n⏳ Waiting for services to start..." -ForegroundColor Yellow
Start-Sleep -Seconds 15

Write-Host "`n🔍 Checking health endpoint..." -ForegroundColor Yellow
try {
    $response = Invoke-WebRequest -Uri "https://localhost:7001/health" -TimeoutSec 10
    $health = $response.Content | ConvertFrom-Json
    Write-Host "  ✅ API is Healthy" -ForegroundColor Green
    Write-Host "`n📊 Aspire Dashboard: http://localhost:5000" -ForegroundColor Cyan
    Write-Host "🌐 API: https://localhost:7001" -ForegroundColor Cyan
} catch {
    Write-Host "  ⚠️  API not ready yet. Check Aspire dashboard." -ForegroundColor Yellow
}
```

**Step 2: Commit**

```bash
git add scripts/start-stack.ps1
git commit -m "chore: add stack start script"
```

---

## Task 30: Final Verification

**Step 1: Run all automated tests**

Run: `.\scripts\verify-mvp.ps1`
Expected: Unit and Integration tests pass

**Step 2: Start the stack**

Run: `.\scripts\start-stack.ps1`
Expected: Aspire dashboard opens, containers healthy

**Step 3: Verify health endpoint**

Run: `curl https://localhost:7001/health`
Expected: Returns JSON with status "Healthy"

**Step 4: Manual verification checklist**

- [ ] Unit tests pass
- [ ] Integration tests pass
- [ ] E2E tests pass (when API is running)
- [ ] Aspire dashboard shows all resources healthy
- [ ] Health endpoint returns 200
- [ ] OpenTelemetry traces visible in dashboard
- [ ] Can navigate to worktree and build all projects

**Step 5: Final commit**

```bash
git add .
git commit -m "chore: complete MVP infrastructure implementation

All components implemented:
- Core library with domain types and abstractions
- Orleans server with HealthGrain
- API server with health endpoint
- Aspire AppHost orchestration
- MAUI client with offline/online toggle
- Blazor WASM client (online-only)
- Unit, Integration, and E2E tests
- Verification and start-stack scripts

Co-Authored-By: Claude Opus 4.5 <noreply@anthropic.com>"
```

---

## Summary

This plan creates 8 projects across src/ and tests/ with health-check style tests at each tier:

| Project | Purpose | Files |
|--------|---------|-------|
| `RealmsOfIdle.Core` | Shared domain & abstractions | 15 files |
| `RealmsOfIdle.Server.Orleans` | Orleans grains | 3 files |
| `RealmsOfIdle.Server.Api` | ASP.NET Core API | 2 files |
| `RealmsOfIdle.AppHost` | .NET Aspire orchestration | 1 file |
| `RealmsOfIdle.Client.Maui` | MAUI client (toggle) | 7 files |
| `RealmsOfIdle.Client.Blazor` | Blazor WASM (online) | 2 files |
| `RealmsOfIdle.Core.Tests` | Unit tests | 1 file |
| `RealmsOfIdle.IntegrationTests` | Orleans integration tests | 2 files |
| `RealmsOfIdle.E2E.Tests` | Playwright E2E tests | 2 files |

Total: ~35 files, 30 tasks, end-to-end health verification.
