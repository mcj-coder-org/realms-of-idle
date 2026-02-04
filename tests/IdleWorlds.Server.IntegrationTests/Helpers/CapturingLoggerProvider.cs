using Microsoft.Extensions.Logging;
using System.Collections.Concurrent;
using xunit;

namespace IdleWorlds.Server.IntegrationTests.Helpers;

/// <summary>
/// Interface for accessing captured log entries from tests.
/// </summary>
public interface ILogCollector
{
    /// <summary>
    /// All captured logs, ordered by timestamp.
    /// </summary>
    IReadOnlyList<LogEntry> CapturedLogs { get; }

    /// <summary>
    /// Clears all captured logs.
    /// </summary>
    void Clear();

    /// <summary>
    /// Gets all logs at a specific log level.
    /// </summary>
    LogEntry[] GetLogsForLevel(LogLevel level);

    /// <summary>
    /// Gets all logs containing a specific message fragment.
    /// </summary>
    LogEntry[] GetLogsContaining(string message);
}

/// <summary>
/// Represents a single log entry.
/// </summary>
/// <param name="Level">The log level.</param>
/// <param name="Message">The log message.</param>
/// <param name="Exception">Optional exception.</param>
/// <param name="Timestamp">When the log was created.</param>
/// <param name="Category">The logger category.</param>
public record LogEntry(
    LogLevel Level,
    string Message,
    Exception? Exception = null,
    DateTime Timestamp = default,
    string? Category = null
)
{
    public LogEntry()
        : this(LogLevel.Information, string.Empty, null, DateTime.UtcNow, null) { }
}

/// <summary>
/// ILoggerProvider that captures log entries for test assertions.
/// Outputs all captured logs to ITestOutputHelper on disposal.
/// </summary>
public sealed class CapturingLoggerProvider : ILoggerProvider, ILogCollector, IDisposable
{
    private readonly ITestOutputHelper? _output;
    private readonly ConcurrentBag<LogEntry> _logs = new();
    private readonly AsyncLocal<string> _testName = new();

    /// <inheritdoc />
    public IReadOnlyList<LogEntry> CapturedLogs => _logs.OrderBy(l => l.Timestamp).ToList();

    /// <summary>
    /// Creates a new logger instance.
    /// </summary>
    public ILogger CreateLogger(string categoryName) =>
        new CapturingLogger(categoryName, this, _output, _testName);

    /// <summary>
    /// Sets the current test name for logging purposes.
    /// </summary>
    public void SetCurrentTest(string testName) => _testName.Value = testName;

    /// <summary>
    /// Adds a log entry to the captured collection.
    /// </summary>
    internal void AddLog(LogEntry entry) => _logs.Add(entry);

    /// <inheritdoc />
    public void Clear() => _logs.Clear();

    /// <inheritdoc />
    public LogEntry[] GetLogsForLevel(LogLevel level) =>
        CapturedLogs.Where(l => l.Level == level).ToArray();

    /// <inheritdoc />
    public LogEntry[] GetLogsContaining(string message) =>
        CapturedLogs.Where(l => l.Message.Contains(message, StringComparison.OrdinalIgnoreCase)).ToArray();

    /// <inheritdoc />
    public void Dispose()
    {
        if (_output is not null && CapturedLogs.Count > 0)
        {
            _output.WriteLine($"=== Logs for test: {_testName.Value} ===");
            foreach (var log in CapturedLogs)
            {
                _output.WriteLine($"[{log.Level}] {log.Category}: {log.Message}");
                if (log.Exception is not null)
                    _output.WriteLine($"  Exception: {log.Exception}");
            }
        }
    }

    private sealed class CapturingLogger : ILogger
    {
        private readonly string _categoryName;
        private readonly CapturingLoggerProvider _provider;
        private readonly ITestOutputHelper? _output;
        private readonly AsyncLocal<string> _testName;

        public CapturingLogger(
            string categoryName,
            CapturingLoggerProvider provider,
            ITestOutputHelper? output,
            AsyncLocal<string> testName)
        {
            _categoryName = categoryName;
            _provider = provider;
            _output = output;
            _testName = testName;
        }

        public IDisposable BeginScope<TState>(TState state) where TState : notnull
            => NullScope.Instance;

        public bool IsEnabled(LogLevel logLevel) => true;

        public void Log<TState>(
            LogLevel logLevel,
            EventId eventId,
            TState state,
            Exception? exception,
            Func<TState, Exception?, string> formatter)
        {
            if (!IsEnabled(logLevel))
                return;

            var message = formatter(state, exception);
            var entry = new LogEntry(
                logLevel,
                message,
                exception,
                DateTime.UtcNow,
                _categoryName
            );

            _provider.AddLog(entry);

            // Also write to test output immediately for real-time viewing
            _output?.WriteLine($"[{logLevel}] {_categoryName}: {message}");
        }

        private sealed class NullScope : IDisposable
        {
            public static readonly NullScope Instance = new();
            public void Dispose() { }
        }
    }
}
