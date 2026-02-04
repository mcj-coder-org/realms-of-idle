using LiteDB;
using RealmsOfIdle.Core.Abstractions;
using RealmsOfIdle.Core.Domain;
using RealmsOfIdle.Core.Domain.Models;

namespace RealmsOfIdle.Client.Maui.Storage;

/// <summary>
/// LiteDB-backed event store for offline play
/// </summary>
public class LiteDBEventStore : IEventStore
{
    private readonly ILiteCollection<StoredEvent> _events;

    public LiteDBEventStore(LiteDatabase database)
    {
        _events = database.GetCollection<StoredEvent>("events");
        _events.EnsureIndex(x => x.PlayerId);
        _events.EnsureIndex(x => x.Timestamp);
        _events.EnsureIndex(x => x.EventType);
    }

    public Task AppendEventAsync(GameEvent @event)
    {
        var stored = new StoredEvent
        {
            Id = ObjectId.NewObjectId(),
            EventType = @event.GetType().Name,
            EventData = System.Text.Json.JsonSerializer.Serialize(@event),
            PlayerId = @event.PlayerId,
            Timestamp = DateTime.UtcNow
        };
        _events.Insert(stored);
        return Task.CompletedTask;
    }

    public Task<IEnumerable<GameEvent>> GetEventsAsync(string playerId, DateTime? startTime = null, DateTime? endTime = null, int limit = 100)
    {
        var query = _events.Query()
            .Where(x => x.PlayerId == playerId);

        if (startTime.HasValue)
        {
            query = query.Where(x => x.Timestamp >= startTime.Value);
        }

        if (endTime.HasValue)
        {
            query = query.Where(x => x.Timestamp <= endTime.Value);
        }

        var results = query
            .OrderByDescending(x => x.Timestamp)
            .Limit(limit)
            .ToList();

        var events = results
            .Select(ToGameEvent)
            .OfType<GameEvent>();
        return Task.FromResult<IEnumerable<GameEvent>>(events);
    }

    public Task<IEnumerable<GameEvent>> GetEventsByTypeAsync(string eventType, DateTime? startTime = null, DateTime? endTime = null, int limit = 100)
    {
        var query = _events.Query()
            .Where(x => x.EventType == eventType);

        if (startTime.HasValue)
        {
            query = query.Where(x => x.Timestamp >= startTime.Value);
        }

        if (endTime.HasValue)
        {
            query = query.Where(x => x.Timestamp <= endTime.Value);
        }

        var results = query
            .OrderByDescending(x => x.Timestamp)
            .Limit(limit)
            .ToList();

        var events = results
            .Select(ToGameEvent)
            .OfType<GameEvent>();
        return Task.FromResult<IEnumerable<GameEvent>>(events);
    }

    public Task<IEnumerable<GameEvent>> GetSessionEventsAsync(string sessionId)
    {
        // For MVP, return empty since GameEvent doesn't have SessionId
        return Task.FromResult(Enumerable.Empty<GameEvent>());
    }

    public Task<EventStream> GetEventStreamAsync(string playerId)
    {
        var events = _events.Query()
            .Where(x => x.PlayerId == playerId)
            .OrderBy(x => x.Timestamp)
            .ToList();

        var gameEvents = events.Select(ToGameEvent).Where(e => e != null).ToList();

        var stream = new EventStream
        {
            PlayerId = playerId,
            CurrentSequence = events.Count,
            Events = gameEvents!,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        return Task.FromResult(stream);
    }

    public Task<IEnumerable<GameEvent>> ReplayEventsAsync(string playerId, long fromSequence)
    {
        var results = _events.Query()
            .Where(x => x.PlayerId == playerId)
            .Skip((int)fromSequence)
            .ToList();

        var events = results
            .Select(ToGameEvent)
            .OfType<GameEvent>();
        return Task.FromResult<IEnumerable<GameEvent>>(events);
    }

    public Task<EventSummary> GetEventSummaryAsync(string playerId, DateTime start, DateTime end)
    {
        var count = _events.Count(Query.And(
            Query.EQ("PlayerId", playerId),
            Query.GTE("Timestamp", start),
            Query.LTE("Timestamp", end)
        ));

        var summary = new EventSummary
        {
            PlayerId = playerId,
            PeriodStart = start,
            PeriodEnd = end,
            TotalEvents = count
        };

        return Task.FromResult(summary);
    }

    public Task CleanupOldEventsAsync(DateTime cutoffDate)
    {
        _events.DeleteMany(x => x.Timestamp < cutoffDate);
        return Task.CompletedTask;
    }

    private static GameEvent? ToGameEvent(StoredEvent stored)
    {
        var type = System.Type.GetType(stored.EventType);
        if (type == null)
        {
            return null;
        }
        return (GameEvent?)System.Text.Json.JsonSerializer.Deserialize(stored.EventData, type);
    }
}

/// <summary>
/// Stored event for LiteDB persistence
/// </summary>
internal class StoredEvent
{
    public ObjectId Id { get; set; } = ObjectId.NewObjectId();
    public string EventType { get; set; } = string.Empty;
    public string EventData { get; set; } = string.Empty;
    public string PlayerId { get; set; } = string.Empty;
    public DateTime Timestamp { get; set; }
}
