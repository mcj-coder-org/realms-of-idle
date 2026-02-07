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

// Note: OpenTelemetry integration for WASM requires additional packages
// For now, metrics are collected via System.Diagnostics.Metrics
// and can be exported via OpenTelemetry in the future

await builder.Build().RunAsync();
