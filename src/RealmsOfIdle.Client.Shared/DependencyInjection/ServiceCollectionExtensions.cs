using LiteDB;
using Microsoft.Extensions.DependencyInjection;
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
