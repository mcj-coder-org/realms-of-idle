using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using RealmsOfIdle.Client.Blazor;
using RealmsOfIdle.Client.UI.Components.Observability;

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

// Configure observability
builder.Services.AddLogging(builder => builder.SetMinimumLevel(LogLevel.Debug));
builder.Services.AddScoped<UiMetrics>();

// OpenTelemetry OTLP export for WASM is deferred to Phase 7 (Online Mode)
// due to browser sandbox limitations and experimental WASM OTLP support.
// Metrics are collected via System.Diagnostics.Metrics and available
// for in-browser inspection via DevTools Performance API.

await builder.Build().RunAsync();
