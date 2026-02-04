using OpenTelemetry;
using OpenTelemetry.Metrics;
using OpenTelemetry.Trace;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace RealmsOfIdle.Core.Infrastructure;

/// <summary>
/// Configuration class for OpenTelemetry telemetry setup
/// </summary>
public static class TelemetryConfiguration
{
    /// <summary>
    /// Configures OpenTelemetry tracing and metrics
    /// </summary>
    /// <param name="services">Service collection to add telemetry to</param>
    /// <param name="serviceName">Name of the service for telemetry</param>
    /// <returns>Configured service collection</returns>
    public static IServiceCollection AddOpenTelemetryServices(
        this IServiceCollection services,
        string serviceName = "RealmsOfIdle.Core")
    {
        return services.AddOpenTelemetry()
            .WithTracing(tracing => tracing
                .AddAspNetCoreInstrumentation()
                .AddHttpClientInstrumentation()
                .AddSource(serviceName)
                .SetSampler(new AlwaysOnSampler()))
            .WithMetrics(metrics => metrics
                .AddAspNetCoreInstrumentation()
                .AddHttpClientInstrumentation()
                .AddConsoleExporter())
            .Build();
    }
}