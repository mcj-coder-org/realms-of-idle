using RealmsOfIdle.Core.Abstractions;
using RealmsOfIdle.Core.Domain;
using RealmsOfIdle.Core.Domain.Models;
using RealmsOfIdle.Client.Maui.Storage;

namespace RealmsOfIdle.Client.Maui.Services;

/// <summary>
/// Local/offline game service using LiteDB for persistence
/// </summary>
public class LocalGameService : IGameService
{
    private readonly LiteDBEventStore _eventStore;

    public LocalGameService(LiteDatabase db)
    {
        _eventStore = new LiteDBEventStore(db);
    }

    public Task<PlayerState> GetPlayerAsync(string playerId)
    {
        return Task.FromResult(new PlayerState(playerId, "Local Player"));
    }

    public Task<ActionResult> PerformActionAsync(string playerId, GameAction action)
    {
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
