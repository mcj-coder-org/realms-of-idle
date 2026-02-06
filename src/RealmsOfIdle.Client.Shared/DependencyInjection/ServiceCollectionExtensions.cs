using System.Diagnostics;
using LiteDB;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OpenTelemetry.Trace;
using RealmsOfIdle.Client.Shared.Logging;
using RealmsOfIdle.Client.Shared.Services;
using RealmsOfIdle.Client.Shared.Storage;
using RealmsOfIdle.Core.Abstractions;
using RealmsOfIdle.Core.Infrastructure;

namespace RealmsOfIdle.Client.Shared.DependencyInjection;

/// <summary>
/// Dependency injection extensions for MAUI client
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Add MAUI client services with offline/online mode support
    /// </summary>
    public static IServiceCollection AddMauiClient(this IServiceCollection services, string? dbPath = null)
    {
        // LiteDB setup
        services.AddSingleton(sp =>
        {
            var path = dbPath ?? Path.Combine(FileSystem.AppDataDirectory, "realms_of_idle.db");
            return new LiteDatabase(path);
        });

        // Core services
        services.AddSingleton<IGameModeService, GameModeService>();
        services.AddSingleton<LocalGameService>();
        services.AddSingleton<MultiplayerGameService>();
        services.AddSingleton<IGameService>(sp => sp.GetRequiredService<LocalGameService>()); // Default to local

        // Storage
        services.AddSingleton<IEventStore, LiteDBEventStore>();

        // Logging
        services.AddSingleton<LiteDBGameLogger>();
        services.AddSingleton<IGameLogger>(sp => sp.GetRequiredService<LiteDBGameLogger>()); // Default to local

        // OpenTelemetry for online mode
        services.AddSingleton<ActivitySource>(sp => new ActivitySource("RealmsOfIdle.Client"));
        services.AddSingleton<RemoteGameLogger>();

        // Infrastructure
        services.AddSingleton<InMemoryEventStore>();
        services.AddSingleton<DeterministicRng>();

        // HttpClient for online mode
        services.AddHttpClient<MultiplayerGameService>(client =>
        {
            client.BaseAddress = new Uri("https://localhost:5001");
            client.Timeout = TimeSpan.FromSeconds(30);
        });

        return services;
    }

    /// <summary>
    /// Add OpenTelemetry tracing for online mode
    /// </summary>
    public static IServiceCollection AddOpenTelemetryTracing(this IServiceCollection services, string serviceName = "RealmsOfIdle.Client")
    {
        services.AddOpenTelemetry()
            .WithTracing(builder => builder
                .AddSource(serviceName)
                .AddHttpClientInstrumentation()
                .AddConsoleExporter());

        return services;
    }
}

/// <summary>
/// File system abstraction for platform-specific paths
/// </summary>
internal static class FileSystem
{
    public static string AppDataDirectory =>
        Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "RealmsOfIdle");
}
