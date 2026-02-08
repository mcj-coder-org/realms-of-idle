using RealmsOfIdle.Core.Domain;

namespace RealmsOfIdle.Core.Engine;

/// <summary>
/// Result of processing ticks on a game state
/// </summary>
public class TickProcessResult
{
    /// <summary>
    /// Gets the number of ticks that were processed
    /// </summary>
    public int TicksProcessed { get; init; }

    /// <summary>
    /// Gets the events generated during tick processing
    /// </summary>
    public IReadOnlyList<GameEvent> Events { get; init; }

    /// <summary>
    /// Initializes a new instance of TickProcessResult
    /// </summary>
    public TickProcessResult(int ticksProcessed, IReadOnlyList<GameEvent> events)
    {
        TicksProcessed = ticksProcessed;
        Events = events;
    }
}

/// <summary>
/// Processes accumulated ticks and applies them to game state
/// </summary>
public class TickProcessor
{
    /// <summary>
    /// Callback delegate for custom tick processing
    /// </summary>
    /// <param name="state">The current game state</param>
    /// <param name="tickNumber">The current tick number (1-indexed)</param>
    public delegate void TickCallback(GameState state, int tickNumber);

    /// <summary>
    /// Processes the specified number of ticks on the given game state
    /// </summary>
    /// <param name="state">The game state to process ticks on</param>
    /// <param name="tickCount">The number of ticks to process</param>
    /// <param name="tickCallback">Optional callback invoked for each tick</param>
    /// <returns>A result containing processed tick count and generated events</returns>
    public TickProcessResult ProcessTicks(GameState state, int tickCount, TickCallback? tickCallback = null)
    {
        ArgumentNullException.ThrowIfNull(state);
        ArgumentOutOfRangeException.ThrowIfNegative(tickCount);

        var events = new List<GameEvent>();

        for (int i = 1; i <= tickCount; i++)
        {
            // Invoke custom callback if provided
            tickCallback?.Invoke(state, i);

            // Generate a tick event
            events.Add(new GameEvent
            {
                EventType = "Tick",
                PlayerId = state.PlayerId.ToString(),
                Timestamp = DateTime.UtcNow,
                Data = $"Tick {i}"
            });
        }

        return new TickProcessResult(tickCount, events);
    }
}
