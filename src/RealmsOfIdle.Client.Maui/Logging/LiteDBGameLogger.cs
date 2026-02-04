using LiteDB;
using RealmsOfIdle.Core.Abstractions;

namespace RealmsOfIdle.Client.Maui.Logging;

/// <summary>
/// LiteDB-backed game logger for offline play with 7-day rolling window
/// </summary>
public class LiteDBGameLogger : IGameLogger
{
    private readonly ILiteCollection<StoredLogEntry> _logs;
    private readonly TimeSpan _retentionPeriod = TimeSpan.FromDays(7);

    public LiteDBGameLogger(LiteDatabase database)
    {
        _logs = database.GetCollection<StoredLogEntry>("logs");
        _logs.EnsureIndex(x => x.Timestamp);
        _logs.EnsureIndex(x => x.Level);
        _logs.EnsureIndex(x => x.Category);
    }

    public Task LogEventAsync(string category, string message, LogLevel level = LogLevel.Info)
    {
        var entry = new StoredLogEntry
        {
            Id = ObjectId.NewObjectId(),
            Timestamp = DateTime.UtcNow,
            Category = category,
            Message = message,
            Level = level
        };
        _logs.Insert(entry);
        _CleanupOldLogs();
        return Task.CompletedTask;
    }

    public Task LogPlayerActionAsync(string playerId, string action, string? details = null)
    {
        var entry = new StoredLogEntry
        {
            Id = ObjectId.NewObjectId(),
            Timestamp = DateTime.UtcNow,
            Category = "PlayerAction",
            Message = $"Player {playerId} performed {action}",
            Level = LogLevel.Info,
            PlayerId = playerId,
            Context = details != null ? new Dictionary<string, object> { ["Action"] = action, ["Details"] = details } : null
        };
        _logs.Insert(entry);
        _CleanupOldLogs();
        return Task.CompletedTask;
    }

    public Task LogSystemEventAsync(string system, string operation, bool success, string? details = null)
    {
        var entry = new StoredLogEntry
        {
            Id = ObjectId.NewObjectId(),
            Timestamp = DateTime.UtcNow,
            Category = "SystemEvent",
            Message = $"System {system} operation {operation}: {(success ? "Success" : "Failed")}",
            Level = success ? LogLevel.Info : LogLevel.Warning,
            Context = new Dictionary<string, object>
            {
                ["System"] = system,
                ["Operation"] = operation,
                ["Success"] = success
            }
        };
        if (details != null)
        {
            entry.Context!["Details"] = details;
        }
        _logs.Insert(entry);
        _CleanupOldLogs();
        return Task.CompletedTask;
    }

    public Task LogPerformanceMetricAsync(string operation, long durationMs, long memoryUsageBytes)
    {
        var entry = new StoredLogEntry
        {
            Id = ObjectId.NewObjectId(),
            Timestamp = DateTime.UtcNow,
            Category = "Performance",
            Message = $"Operation {operation} took {durationMs}ms",
            Level = LogLevel.Info,
            Context = new Dictionary<string, object>
            {
                ["Operation"] = operation,
                ["DurationMs"] = durationMs,
                ["MemoryBytes"] = memoryUsageBytes
            }
        };
        _logs.Insert(entry);
        _CleanupOldLogs();
        return Task.CompletedTask;
    }

    public Task LogErrorAsync(string error, string? context = null, Exception? exception = null)
    {
        var entry = new StoredLogEntry
        {
            Id = ObjectId.NewObjectId(),
            Timestamp = DateTime.UtcNow,
            Category = "Error",
            Message = error,
            Level = LogLevel.Error,
            Context = new Dictionary<string, object>
            {
                ["Context"] = context ?? string.Empty,
                ["Exception"] = exception?.ToString() ?? string.Empty
            }
        };
        _logs.Insert(entry);
        _CleanupOldLogs();
        return Task.CompletedTask;
    }

    public Task LogGameStateAsync(string sessionId, Dictionary<string, object> state)
    {
        var entry = new StoredLogEntry
        {
            Id = ObjectId.NewObjectId(),
            Timestamp = DateTime.UtcNow,
            Category = "GameState",
            Message = $"Game state snapshot for session {sessionId}",
            Level = LogLevel.Debug,
            SessionId = sessionId,
            Context = state
        };
        _logs.Insert(entry);
        _CleanupOldLogs();
        return Task.CompletedTask;
    }

    public ILoggingScope CreateScope(string operation, Dictionary<string, object>? properties = null)
    {
        return new LoggingScope(this, operation, properties);
    }

    public Task<IEnumerable<LogEntry>> GetSessionLogsAsync(string sessionId)
    {
        var results = _logs.Query()
            .Where(x => x.SessionId == sessionId)
            .OrderByDescending(x => x.Timestamp)
            .ToList();

        var entries = results.Select(ToLogEntry);
        return Task.FromResult(entries);
    }

    public Task<IEnumerable<LogEntry>> GetErrorLogsAsync(DateTime? start = null, DateTime? end = null, int limit = 100)
    {
        var query = _logs.Query()
            .Where(x => x.Level >= LogLevel.Error);

        if (start.HasValue)
        {
            query = query.Where(x => x.Timestamp >= start.Value);
        }

        if (end.HasValue)
        {
            query = query.Where(x => x.Timestamp <= end.Value);
        }

        var results = query
            .OrderByDescending(x => x.Timestamp)
            .Limit(limit)
            .ToList();

        var entries = results.Select(ToLogEntry);
        return Task.FromResult(entries);
    }

    public Task ArchiveLogsAsync(DateTime cutoffDate)
    {
        _logs.DeleteMany(x => x.Timestamp < cutoffDate);
        return Task.CompletedTask;
    }

    private void _CleanupOldLogs()
    {
        var cutoff = DateTime.UtcNow.Subtract(_retentionPeriod);
        _logs.DeleteMany(x => x.Timestamp < cutoff);
    }

    private static LogEntry ToLogEntry(StoredLogEntry stored) =>
        new(
            stored.Id.ToString(),
            stored.Timestamp,
            stored.Category,
            stored.Message,
            stored.Level,
            stored.PlayerId,
            stored.SessionId,
            stored.Context
        );
}

/// <summary>
/// Stored log entry for LiteDB persistence
/// </summary>
internal class StoredLogEntry
{
    public ObjectId Id { get; set; } = ObjectId.NewObjectId();
    public DateTime Timestamp { get; set; }
    public string Category { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public LogLevel Level { get; set; }
    public string? PlayerId { get; set; }
    public string? SessionId { get; set; }
    public Dictionary<string, object>? Context { get; set; }
}

/// <summary>
/// Logging scope for operation tracking
/// </summary>
internal class LoggingScope : ILoggingScope
{
    private readonly LiteDBGameLogger _logger;
    private readonly string _operation;
    private readonly Dictionary<string, object> _properties;
    private readonly DateTime _startTime;

    public LoggingScope(LiteDBGameLogger logger, string operation, Dictionary<string, object>? properties)
    {
        _logger = logger;
        _operation = operation;
        _properties = properties ?? new Dictionary<string, object>();
        _startTime = DateTime.UtcNow;
    }

    public void AddProperty(string key, object value)
    {
        _properties[key] = value;
    }

    public async Task LogAsync(string category, string message, LogLevel level = LogLevel.Info)
    {
        var entry = new StoredLogEntry
        {
            Id = ObjectId.NewObjectId(),
            Timestamp = DateTime.UtcNow,
            Category = category,
            Message = $"[{_operation}] {message}",
            Level = level,
            Context = new Dictionary<string, object>(_properties)
        };

        // Add duration to context
        var duration = (long)(DateTime.UtcNow - _startTime).TotalMilliseconds;
        entry.Context["ScopeDurationMs"] = duration;

        await Task.CompletedTask; // LiteDB operations are sync, but interface requires Task
    }

    public void Dispose()
    {
        // Log scope completion on dispose
        var duration = (long)(DateTime.UtcNow - _startTime).TotalMilliseconds;
        _logger.LogPerformanceMetricAsync(_operation, duration, 0).Wait();
    }
}
