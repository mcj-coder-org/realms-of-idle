using System.Collections.Concurrent;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using RealmsOfIdle.Core.Abstractions;
using RealmsOfIdle.Core.Domain;
using RealmsOfIdle.Core.Domain.Models;
using RealmsOfIdle.Core.Core.Exceptions;

#pragma warning disable CA1050 // Declare types in namespaces
#pragma warning disable CA1851 // Possible multiple enumerations of 'IEnumerable' collection

/// <summary>
/// In-memory implementation of IEventStore for testing and development scenarios.
/// Stores events in a concurrent dictionary for fast access but does not persist beyond application lifetime.
/// </summary>
public class InMemoryEventStore : IEventStore
{
    private readonly ConcurrentDictionary<string, List<GameEvent>> _store = new();
    private readonly ILogger<InMemoryEventStore> _logger;

    public InMemoryEventStore(ILogger<InMemoryEventStore> logger)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    /// <summary>
    /// Append a new event to the store
    /// </summary>
    public async Task AppendEventAsync(GameEvent @event)
    {
        ArgumentNullException.ThrowIfNull(@event);

        // Simulate async operation
        await Task.Delay(1);

        var eventList = _store.GetOrAdd(@event.PlayerId, new List<GameEvent>());
        eventList.Add(@event);

        _logger.LogDebug("Appended event {EventType} for player {PlayerId}, total events: {TotalCount}",
            @event.EventType, @event.PlayerId, eventList.Count);

        _logger.LogInformation("Appended event {EventType} for player {PlayerId}", @event.EventType, @event.PlayerId);
    }

    /// <summary>
    /// Get events by player within time range
    /// </summary>
    public async Task<IEnumerable<GameEvent>> GetEventsAsync(
        string playerId,
        DateTime? startTime = null,
        DateTime? endTime = null,
        int limit = 100)
    {
        // Simulate async operation
        await Task.Delay(1);

        if (_store.TryGetValue(playerId, out var events))
        {
            var filteredEvents = events.AsEnumerable();

            if (startTime.HasValue)
            {
                filteredEvents = filteredEvents.Where(e => e.Timestamp >= startTime.Value);
            }

            if (endTime.HasValue)
            {
                filteredEvents = filteredEvents.Where(e => e.Timestamp <= endTime.Value);
            }

            filteredEvents = filteredEvents
                .OrderBy(e => e.Timestamp)
                .Take(limit);

            _logger.LogDebug("Retrieved {EventCount} events for player {PlayerId} with time filtering",
                filteredEvents.Count(), playerId);

            return filteredEvents;
        }

        _logger.LogDebug("No events found for player {PlayerId}", playerId);
        return Enumerable.Empty<GameEvent>();
    }

    /// <summary>
    /// Get events by type within time range
    /// </summary>
    public async Task<IEnumerable<GameEvent>> GetEventsByTypeAsync(
        string eventType,
        DateTime? startTime = null,
        DateTime? endTime = null,
        int limit = 100)
    {
        // Simulate async operation
        await Task.Delay(1);

        var allEvents = _store.Values.SelectMany(e => e);
        var filteredEvents = allEvents.Where(e => e.EventType == eventType);

        if (startTime.HasValue)
        {
            filteredEvents = filteredEvents.Where(e => e.Timestamp >= startTime.Value);
        }

        if (endTime.HasValue)
        {
            filteredEvents = filteredEvents.Where(e => e.Timestamp <= endTime.Value);
        }

        filteredEvents = filteredEvents
            .OrderBy(e => e.Timestamp)
            .Take(limit);

        _logger.LogDebug("Retrieved {EventCount} events of type {EventType}",
            filteredEvents.Count(), eventType);

        return filteredEvents;
    }

    /// <summary>
    /// Get player session events
    /// </summary>
    public async Task<IEnumerable<GameEvent>> GetSessionEventsAsync(string sessionId)
    {
        // Simulate async operation
        await Task.Delay(1);

        // This is a simplified implementation - in a real system,
        // sessionId would be stored in GameEvent or a separate lookup
        var sessionEvents = _store.Values.SelectMany(e => e)
            .Where(e => e.Data != null && e.Data.Contains(sessionId))
            .OrderBy(e => e.Timestamp);

        _logger.LogDebug("Retrieved {EventCount} events for session {SessionId}",
            sessionEvents.Count(), sessionId);

        return sessionEvents;
    }

    /// <summary>
    /// Create or get player event stream
    /// </summary>
    public async Task<EventStream> GetEventStreamAsync(string playerId)
    {
        // Simulate async operation
        await Task.Delay(1);

        if (_store.TryGetValue(playerId, out var events))
        {
            var stream = new EventStream
            {
                PlayerId = playerId,
                CurrentSequence = events.Count,
                Events = events.OrderBy(e => e.Timestamp).ToList(),
                UpdatedAt = DateTime.UtcNow
            };

            _logger.LogDebug("Retrieved event stream for player {PlayerId} with {EventCount} events",
                playerId, stream.Events.Count);

            return stream;
        }

        var emptyStream = new EventStream
        {
            PlayerId = playerId,
            CurrentSequence = 0,
            Events = new List<GameEvent>(),
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        _logger.LogDebug("Created new event stream for player {PlayerId}", playerId);

        return emptyStream;
    }

    /// <summary>
    /// Replay events from a specific sequence number
    /// </summary>
    public async Task<IEnumerable<GameEvent>> ReplayEventsAsync(
        string playerId,
        long fromSequence)
    {
        // Simulate async operation
        await Task.Delay(1);

        if (_store.TryGetValue(playerId, out var events))
        {
            var replayEvents = events
                .Skip((int)fromSequence)
                .ToList();

            _logger.LogDebug("Replaying {EventCount} events for player {PlayerId} from sequence {FromSequence}",
                replayEvents.Count, playerId, fromSequence);

            return replayEvents;
        }

        _logger.LogDebug("No events found for replay for player {PlayerId}", playerId);
        return Enumerable.Empty<GameEvent>();
    }

    /// <summary>
    /// Get event summary for analytics
    /// </summary>
    public async Task<EventSummary> GetEventSummaryAsync(
        string playerId,
        DateTime start,
        DateTime end)
    {
        // Simulate async operation
        await Task.Delay(1);

        if (_store.TryGetValue(playerId, out var events))
        {
            var periodEvents = events.Where(e => e.Timestamp >= start && e.Timestamp <= end);

            var summary = new EventSummary
            {
                PlayerId = playerId,
                PeriodStart = start,
                PeriodEnd = end,
                TotalEvents = periodEvents.Count(),
                EventCounts = periodEvents
                    .GroupBy(e => e.EventType)
                    .ToDictionary(g => g.Key, g => g.Count()),
                UniqueEventTypes = periodEvents
                    .Select(e => e.EventType)
                    .Distinct()
                    .ToList()
            };

            _logger.LogDebug("Generated summary for player {PlayerId}: {TotalEvents} total events",
                playerId, summary.TotalEvents);

            return summary;
        }

        var emptySummary = new EventSummary
        {
            PlayerId = playerId,
            PeriodStart = start,
            PeriodEnd = end,
            TotalEvents = 0,
            EventCounts = new Dictionary<string, int>(),
            UniqueEventTypes = new List<string>()
        };

        _logger.LogDebug("Generated empty summary for player {PlayerId}", playerId);

        return emptySummary;
    }

    /// <summary>
    /// Clear old events (cleanup)
    /// </summary>
    public async Task CleanupOldEventsAsync(DateTime cutoffDate)
    {
        // Simulate async operation
        await Task.Delay(1);

        int totalRemoved = 0;

        foreach (var kvp in _store.ToList())
        {
            var playerId = kvp.Key;
            var events = kvp.Value;

            var beforeCount = events.Count;
            events.RemoveAll(e => e.Timestamp < cutoffDate);

            if (events.Count == 0)
            {
                _store.TryRemove(playerId, out _);
            }

            var removed = beforeCount - events.Count;
            totalRemoved += removed;

            if (removed > 0)
            {
                _logger.LogDebug("Removed {RemovedCount} old events for player {PlayerId}", removed, playerId);
            }
        }

        if (totalRemoved > 0)
        {
            _logger.LogInformation("Cleanup completed: removed {TotalRemoved} old events", totalRemoved);
        }
        else
        {
            _logger.LogDebug("Cleanup completed: no old events found to remove");
        }
    }
}