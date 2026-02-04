using LiteDB;
using RealmsOfIdle.Core.Abstractions;
using RealmsOfIdle.Core.Domain;

namespace RealmsOfIdle.Client.Maui.Storage;

/// <summary>
/// LiteDB implementation of IEventStore for offline local storage
/// </summary>
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

    public bool IsHealthy => _events.Count() >= 0;
}
