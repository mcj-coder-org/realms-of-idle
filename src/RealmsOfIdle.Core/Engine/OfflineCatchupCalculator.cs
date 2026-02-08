namespace RealmsOfIdle.Core.Engine;

/// <summary>
/// Calculates offline catch-up ticks based on elapsed time
/// </summary>
public class OfflineCatchupCalculator
{
    private const int DefaultMaxCatchupTicks = 10000;

    /// <summary>
    /// Gets the number of ticks per second
    /// </summary>
    public int TicksPerSecond { get; }

    /// <summary>
    /// Gets the maximum number of ticks that can be caught up
    /// </summary>
    public int MaxCatchupTicks { get; }

    /// <summary>
    /// Initializes a new instance of OfflineCatchupCalculator
    /// </summary>
    /// <param name="tickRate">The number of ticks per second</param>
    /// <param name="maxCatchupTicks">The maximum number of ticks to catch up (default: 10000)</param>
    /// <exception cref="ArgumentOutOfRangeException">Thrown if tickRate is less than or equal to zero</exception>
    public OfflineCatchupCalculator(int tickRate, int maxCatchupTicks = DefaultMaxCatchupTicks)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(tickRate);
        ArgumentOutOfRangeException.ThrowIfNegative(maxCatchupTicks);

        TicksPerSecond = tickRate;
        MaxCatchupTicks = maxCatchupTicks;
    }

    /// <summary>
    /// Calculates the number of catch-up ticks based on the elapsed time
    /// </summary>
    /// <param name="lastActiveTime">The last time the player was active</param>
    /// <param name="currentTime">The current time</param>
    /// <returns>The number of ticks to catch up, capped at MaxCatchupTicks</returns>
    public int CalculateCatchupTicks(DateTime? lastActiveTime, DateTime currentTime)
    {
        if (!lastActiveTime.HasValue)
        {
            return 0;
        }

        var elapsed = currentTime - lastActiveTime.Value;

        if (elapsed <= TimeSpan.Zero)
        {
            return 0;
        }

        var calculatedTicks = (int)(elapsed.TotalSeconds * TicksPerSecond);

        return Math.Min(calculatedTicks, MaxCatchupTicks);
    }
}
