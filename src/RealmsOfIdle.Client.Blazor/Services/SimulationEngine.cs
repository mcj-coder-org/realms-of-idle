using System.Timers;

namespace RealmsOfIdle.Client.Blazor.Services;

/// <summary>
/// Simulation engine with game loop timer (adapts InnGameLoop pattern)
/// Runs at 10 ticks/second (100ms intervals)
/// </summary>
public class SimulationEngine : IDisposable
{
    private System.Timers.Timer? _timer;
    private readonly int _tickInterval = 100; // milliseconds (10 ticks/sec)
    private bool _isRunning;
    private bool _disposed;

    /// <summary>
    /// Event fired on each tick (10 times per second)
    /// </summary>
    public event Action? OnTick;

    /// <summary>
    /// Gets whether the simulation is currently running
    /// </summary>
    public bool IsRunning => _isRunning;

    /// <summary>
    /// Starts the game loop timer
    /// </summary>
    public void Start()
    {
        if (_isRunning || _disposed)
            return;

        _timer = new System.Timers.Timer(_tickInterval);
        _timer.Elapsed += OnTimerElapsed;
        _timer.AutoReset = true;
        _timer.Start();

        _isRunning = true;
    }

    /// <summary>
    /// Stops the game loop timer
    /// </summary>
    public void Stop()
    {
        if (!_isRunning)
            return;

        _timer?.Stop();
        _timer?.Dispose();
        _timer = null;

        _isRunning = false;
    }

    /// <summary>
    /// Handles timer elapsed event and invokes tick handlers
    /// </summary>
    private void OnTimerElapsed(object? sender, ElapsedEventArgs e)
    {
        OnTick?.Invoke();
    }

    /// <summary>
    /// Disposes the simulation engine and stops the timer
    /// </summary>
    public void Dispose()
    {
        if (_disposed)
            return;

        Stop();
        _disposed = true;

        GC.SuppressFinalize(this);
    }
}
