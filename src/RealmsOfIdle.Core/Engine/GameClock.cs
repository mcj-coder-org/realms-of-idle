namespace RealmsOfIdle.Core.Engine;

/// <summary>
/// Tracks game time including elapsed time, tick accumulation, and last active timestamp
/// </summary>
public class GameClock
{
    /// <summary>
    /// Gets the current time of the game clock
    /// </summary>
    public DateTime CurrentTime { get; private set; }

    /// <summary>
    /// Gets the number of ticks that have been accumulated but not yet processed
    /// </summary>
    public int AccumulatedTicks { get; private set; }

    /// <summary>
    /// Gets the last time the player was active
    /// </summary>
    public DateTime? LastActiveTime { get; private set; }

    /// <summary>
    /// Initializes a new instance of GameClock with the current time
    /// </summary>
    public GameClock()
        : this(DateTime.UtcNow)
    {
    }

    /// <summary>
    /// Initializes a new instance of GameClock with a specific last active time
    /// </summary>
    /// <param name="lastActiveTime">The last time the player was active</param>
    public GameClock(DateTime lastActiveTime)
    {
        CurrentTime = DateTime.UtcNow;
        LastActiveTime = lastActiveTime == DateTime.MinValue ? null : lastActiveTime;
        AccumulatedTicks = 0;
    }

    /// <summary>
    /// Records player activity and updates the last active time
    /// </summary>
    public void RecordActivity()
    {
        LastActiveTime = DateTime.UtcNow;
    }

    /// <summary>
    /// Adds ticks to the accumulated tick count
    /// </summary>
    /// <param name="ticks">The number of ticks to accumulate</param>
    public void AccumulateTicks(int ticks)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(ticks);
        AccumulatedTicks += ticks;
    }

    /// <summary>
    /// Resets the accumulated tick count to zero
    /// </summary>
    public void ResetAccumulatedTicks()
    {
        AccumulatedTicks = 0;
    }

    /// <summary>
    /// Calculates the duration of inactivity since the last active time
    /// </summary>
    /// <param name="currentTime">The current time to compare against</param>
    /// <returns>The duration since the last activity, or TimeSpan.Zero if no activity has been recorded</returns>
    public TimeSpan GetInactiveDuration(DateTime currentTime)
    {
        if (!LastActiveTime.HasValue)
        {
            return TimeSpan.Zero;
        }

        var duration = currentTime - LastActiveTime.Value;
        return duration > TimeSpan.Zero ? duration : TimeSpan.Zero;
    }
}
