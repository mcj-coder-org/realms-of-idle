using RealmsOfIdle.Core.Domain;
using RealmsOfIdle.Core.Domain.Models;

namespace RealmsOfIdle.Core.Abstractions;

/// <summary>
/// Event store interface for tracking game events and state changes
/// </summary>
public interface IEventStore
{
    /// <summary>
    /// Append a new event to the store
    /// </summary>
    Task AppendEventAsync(GameEvent @event);

    /// <summary>
    /// Get events by player within time range
    /// </summary>
    Task<IEnumerable<GameEvent>> GetEventsAsync(
        string playerId,
        DateTime? startTime = null,
        DateTime? endTime = null,
        int limit = 100);

    /// <summary>
    /// Get events by type within time range
    /// </summary>
    Task<IEnumerable<GameEvent>> GetEventsByTypeAsync(
        string eventType,
        DateTime? startTime = null,
        DateTime? endTime = null,
        int limit = 100);

    /// <summary>
    /// Get player session events
    /// </summary>
    Task<IEnumerable<GameEvent>> GetSessionEventsAsync(string sessionId);

    /// <summary>
    /// Create or get player event stream
    /// </summary>
    Task<EventStream> GetEventStreamAsync(string playerId);

    /// <summary>
    /// Replay events from a specific sequence number
    /// </summary>
    Task<IEnumerable<GameEvent>> ReplayEventsAsync(
        string playerId,
        long fromSequence);

    /// <summary>
    /// Get event summary for analytics
    /// </summary>
    Task<EventSummary> GetEventSummaryAsync(
        string playerId,
        DateTime start,
        DateTime end);

    /// <summary>
    /// Clear old events (cleanup)
    /// </summary>
    Task CleanupOldEventsAsync(DateTime cutoffDate);
}
