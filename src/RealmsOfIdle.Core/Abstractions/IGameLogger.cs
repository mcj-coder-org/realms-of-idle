using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RealmsOfIdle.Core.Abstractions;

/// <summary>
/// Game logger interface for tracking game-specific events and metrics
/// </summary>
public interface IGameLogger
{
    /// <summary>
    /// Log a game event with severity level
    /// </summary>
    Task LogEventAsync(string category, string message, LogLevel level = LogLevel.Info);

    /// <summary>
    /// Log a player action
    /// </summary>
    Task LogPlayerActionAsync(string playerId, string action, string? details = null);

    /// <summary>
    /// Log a system event
    /// </summary>
    Task LogSystemEventAsync(string system, string operation, bool success, string? details = null);

    /// <summary>
    /// Log performance metrics
    /// </summary>
    Task LogPerformanceMetricAsync(string operation, long durationMs, long memoryUsageBytes);

    /// <summary>
    /// Log error with context
    /// </summary>
    Task LogErrorAsync(string error, string? context = null, Exception? exception = null);

    /// <summary>
    /// Log game state snapshot
    /// </summary>
    Task LogGameStateAsync(string sessionId, Dictionary<string, object> state);

    /// <summary>
    /// Create a logging scope for operation tracking
    /// </summary>
    ILoggingScope CreateScope(string operation, Dictionary<string, object>? properties = null);

    /// <summary>
    /// Get player session logs
    /// </summary>
    Task<IEnumerable<LogEntry>> GetSessionLogsAsync(string sessionId);

    /// <summary>
    /// Get error logs
    /// </summary>
    Task<IEnumerable<LogEntry>> GetErrorLogsAsync(DateTime? start = null, DateTime? end = null, int limit = 100);

    /// <summary>
    /// Archive old logs
    /// </summary>
    Task ArchiveLogsAsync(DateTime cutoffDate);
}

/// <summary>
/// Log level enumeration
/// </summary>
public enum LogLevel
{
    Debug = 0,
    Info = 1,
    Warning = 2,
    Error = 3,
    Critical = 4
}

/// <summary>
/// Log entry model
/// </summary>
public record LogEntry(
    string Id,
    DateTime Timestamp,
    string Category,
    string Message,
    LogLevel Level,
    string? PlayerId,
    string? SessionId,
    Dictionary<string, object>? Context);

/// <summary>
/// Logging scope interface
/// </summary>
public interface ILoggingScope : IDisposable
{
    /// <summary>
    /// Add property to scope
    /// </summary>
    void AddProperty(string key, object value);

    /// <summary>
    /// Log within scope
    /// </summary>
    Task LogAsync(string category, string message, LogLevel level = LogLevel.Info);
}