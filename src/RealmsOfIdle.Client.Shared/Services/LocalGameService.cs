using LiteDB;
using RealmsOfIdle.Core.Abstractions;
using RealmsOfIdle.Core.Domain;

namespace RealmsOfIdle.Client.Shared.Services;

/// <summary>
/// Local game service for offline play - stores data in LiteDB
/// </summary>
public class LocalGameService : IGameService
{
    private readonly LiteDatabase _database;

    public LocalGameService(LiteDatabase database)
    {
        _database = database;
    }

    public Task<GameSession> InitializeGameAsync(string playerId, GameConfiguration configuration)
    {
        var session = new GameSession
        {
            SessionId = Guid.NewGuid().ToString(),
            PlayerId = playerId,
            CreatedAt = DateTime.UtcNow,
            LastUpdated = DateTime.UtcNow,
            Configuration = configuration
        };

        var sessions = _database.GetCollection<GameSession>("sessions");
        sessions.EnsureIndex(x => x.PlayerId);
        sessions.Insert(session);

        return Task.FromResult(session);
    }

    public Task<GameSession?> GetActiveSessionAsync(string playerId)
    {
        var sessions = _database.GetCollection<GameSession>("sessions");
        var session = sessions.FindOne(x => x.PlayerId == playerId && x.IsActive);
        return Task.FromResult<GameSession?>(session);
    }

    public Task<GameSession> ProcessTickAsync(string playerId, TimeSpan deltaTime)
    {
        // MVP: Return stub session
        return Task.FromResult(new GameSession { PlayerId = playerId });
    }

    public Task<ActionResult> HandleActionAsync(string playerId, GameAction action)
    {
        // MVP: Just log and return success
        var result = ActionResult.Ok($"Action '{action.GetType().Name}' handled locally");
        return Task.FromResult(result);
    }

    public Task SaveGameAsync(string playerId)
    {
        // MVP: LiteDB auto-saves on write
        return Task.CompletedTask;
    }

    public Task<GameSession?> LoadGameAsync(string playerId)
    {
        return GetActiveSessionAsync(playerId);
    }

    public Task<PlayerProfile> GetOrCreatePlayerProfileAsync(string playerId)
    {
        var profiles = _database.GetCollection<PlayerProfile>("profiles");
        var profile = profiles.FindById(playerId);

        if (profile == null)
        {
            profile = new PlayerProfile { PlayerId = playerId, FirstLogin = DateTime.UtcNow, LastLogin = DateTime.UtcNow };
            profiles.Insert(profile);
        }

        return Task.FromResult(profile);
    }

    public Task<GameStats> GetGameStatsAsync(string playerId)
    {
        // MVP: Return stub stats
        return Task.FromResult(new GameStats { FirstPlayed = DateTime.UtcNow, LastPlayed = DateTime.UtcNow });
    }
}
