using LiteDB;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using RealmsOfIdle.Client.Blazor;
using RealmsOfIdle.Client.Blazor.Services;
using RealmsOfIdle.Client.UI.Components.Observability;
using RealmsOfIdle.Core.Abstractions;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Configure HttpClient with API base address
builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
});

// Register HTTP game service
builder.Services.AddScoped<HttpGameService>();

// Register LiteDB for browser storage (WASM uses in-memory LiteDB)
builder.Services.AddSingleton<LiteDatabase>(_ =>
{
    var db = new LiteDatabase("Filename=:memory:");
    return db;
});

// Register possession demo services
builder.Services.AddSingleton<IGameService, SettlementGameService>();
builder.Services.AddSingleton<SimulationEngine>();
builder.Services.AddSingleton<NPCAIService>();
builder.Services.AddSingleton<OfflineProgressCalculator>();
builder.Services.AddSingleton<TabVisibilityHandler>();

// Configure observability
builder.Services.AddLogging(builder => builder.SetMinimumLevel(LogLevel.Debug));
builder.Services.AddScoped<UiMetrics>();

// OpenTelemetry OTLP export for WASM is deferred to Phase 7 (Online Mode)
// due to browser sandbox limitations and experimental WASM OTLP support.
// Metrics are collected via System.Diagnostics.Metrics and available
// for in-browser inspection via DevTools Performance API.

await builder.Build().RunAsync();
