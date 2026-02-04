using System.Diagnostics;
using RealmsOfIdle.Core.Abstractions;

namespace RealmsOfIdle.Client.Maui.Logging;

/// <summary>
/// Remote logger using OpenTelemetry for online play
/// </summary>
public class RemoteGameLogger : IGameLogger
{
    private readonly ActivitySource _activitySource;

    public RemoteGameLogger(ActivitySource activitySource)
    {
        _activitySource = activitySource;
    }

    public Task LogEventAsync(string category, string message, LogLevel level = LogLevel.Info)
    {
        using var activity = _activitySource.StartActivity($"{category}:{message}");
        if (activity != null)
        {
            activity.SetTag("level", level.ToString());
        }
        return Task.CompletedTask;
    }

    public Task LogPlayerActionAsync(string playerId, string action, string? details = null)
    {
        using var activity = _activitySource.StartActivity($"PlayerAction:{action}");
        if (activity != null)
        {
            activity.SetTag("playerId", playerId);
            activity.SetTag("action", action);
            if (details != null)
            {
                activity.SetTag("details", details);
            }
        }
        return Task.CompletedTask;
    }

    public Task LogSystemEventAsync(string system, string operation, bool success, string? details = null)
    {
        using var activity = _activitySource.StartActivity($"SystemEvent:{system}.{operation}");
        if (activity != null)
        {
            activity.SetTag("system", system);
            activity.SetTag("operation", operation);
            activity.SetTag("success", success);
            if (details != null)
            {
                activity.SetTag("details", details);
            }
        }
        return Task.CompletedTask;
    }

    public Task LogPerformanceMetricAsync(string operation, long durationMs, long memoryUsageBytes)
    {
        using var activity = _activitySource.StartActivity($"Performance:{operation}");
        if (activity != null)
        {
            activity.SetTag("operation", operation);
            activity.SetTag("durationMs", durationMs);
            activity.SetTag("memoryBytes", memoryUsageBytes);
        }
        return Task.CompletedTask;
    }

    public Task LogErrorAsync(string error, string? context = null, Exception? exception = null)
    {
        using var activity = _activitySource.StartActivity("Error");
        if (activity != null)
        {
            activity.SetTag("error", error);
            if (context != null)
            {
                activity.SetTag("context", context);
            }
            if (exception != null)
            {
                activity.SetTag("exception", exception.ToString());
            }
            activity.SetStatus(ActivityStatusCode.Error, error);
        }
        return Task.CompletedTask;
    }

    public Task LogGameStateAsync(string sessionId, Dictionary<string, object> state)
    {
        using var activity = _activitySource.StartActivity("GameState");
        if (activity != null)
        {
            activity.SetTag("sessionId", sessionId);
            foreach (var kvp in state)
            {
                activity.SetTag($"state.{kvp.Key}", kvp.Value?.ToString() ?? "null");
            }
        }
        return Task.CompletedTask;
    }

    public ILoggingScope CreateScope(string operation, Dictionary<string, object>? properties = null)
    {
        var activity = _activitySource.StartActivity(operation);
        if (activity != null && properties != null)
        {
            foreach (var kvp in properties)
            {
                activity.SetTag(kvp.Key, kvp.Value?.ToString() ?? "null");
            }
        }
        return new RemoteLoggingScope(activity);
    }

    public Task<IEnumerable<LogEntry>> GetSessionLogsAsync(string sessionId)
    {
        // Remote logs are queried via Aspire dashboard, not locally
        return Task.FromResult(Enumerable.Empty<LogEntry>());
    }

    public Task<IEnumerable<LogEntry>> GetErrorLogsAsync(DateTime? start = null, DateTime? end = null, int limit = 100)
    {
        // Remote logs are queried via Aspire dashboard, not locally
        return Task.FromResult(Enumerable.Empty<LogEntry>());
    }

    public Task ArchiveLogsAsync(DateTime cutoffDate)
    {
        // Remote logs are managed server-side
        return Task.CompletedTask;
    }
}

/// <summary>
/// Remote logging scope using OpenTelemetry Activity
/// </summary>
internal class RemoteLoggingScope : ILoggingScope
{
    private readonly Activity? _activity;

    public RemoteLoggingScope(Activity? activity)
    {
        _activity = activity;
    }

    public void AddProperty(string key, object value)
    {
        _activity?.SetTag(key, value?.ToString() ?? "null");
    }

    public Task LogAsync(string category, string message, LogLevel level = LogLevel.Info)
    {
        _activity?.AddEvent(new ActivityEvent($"{category}: {message}"));
        _activity?.SetTag("level", level.ToString());
        return Task.CompletedTask;
    }

    public void Dispose()
    {
        _activity?.Stop();
    }
}
