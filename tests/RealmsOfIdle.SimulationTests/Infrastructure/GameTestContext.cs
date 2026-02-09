using RealmsOfIdle.Core.Domain;
using RealmsOfIdle.Core.Engine.Spatial;
using RealmsOfIdle.Core.Scenarios.Inn;
using RealmsOfIdle.Core.Scenarios.Inn.GameLoop;

namespace RealmsOfIdle.SimulationTests.Infrastructure;

/// <summary>
/// Shared test context for all game simulation tests.
/// Maintains game state across scenario steps and provides access to test drivers.
/// </summary>
public class GameTestContext
{
    /// <summary>
    /// The current player state being tested.
    /// </summary>
    public PlayerState? PlayerState { get; set; }

    /// <summary>
    /// The current player ID for the test session.
    /// </summary>
    public PlayerId? PlayerId { get; set; }

    /// <summary>
    /// The inn game loop driving the simulation.
    /// </summary>
    public InnGameLoop? GameLoop { get; set; }

    /// <summary>
    /// The current inn state (shorthand for GameLoop.State).
    /// </summary>
    public InnState InnState => GameLoop?.State ?? throw new InvalidOperationException("GameLoop not initialized");

    /// <summary>
    /// The number of ticks that have been processed in the current test.
    /// </summary>
    public long TicksProcessed { get; set; }

    /// <summary>
    /// The last timestamp when the game was active (for offline catch-up calculations).
    /// </summary>
    public DateTimeOffset LastActiveTime { get; set; } = DateTimeOffset.UtcNow;

    /// <summary>
    /// Any exception that occurred during game action processing.
    /// </summary>
    public Exception? LastException { get; set; }

    /// <summary>
    /// Whether the last action was successful.
    /// </summary>
    public bool LastActionSucceeded => LastException is null;

    /// <summary>
    /// The last action result from executing an inn action.
    /// </summary>
    public ActionResult? LastActionResult { get; set; }

    /// <summary>
    /// Collection of events generated during the test.
    /// </summary>
    public List<GameEvent> GeneratedEvents { get; } = new();

    /// <summary>
    /// The seed used for deterministic random number generation in tests.
    /// </summary>
    public int? TestSeed { get; set; }

    /// <summary>
    /// The generated world layout for layout-related tests.
    /// </summary>
    public WorldLayout? Layout { get; set; }

    /// <summary>
    /// A second layout used for comparison in determinism tests.
    /// </summary>
    public WorldLayout? ComparisonLayout { get; set; }

    /// <summary>
    /// Named staff members tracked across steps.
    /// </summary>
    public Dictionary<string, StaffMember> NamedStaff { get; } = new();

    /// <summary>
    /// Named customers tracked across steps.
    /// </summary>
    public Dictionary<string, Customer> NamedCustomers { get; } = new();

    /// <summary>
    /// Snapshot values for before/after comparisons.
    /// </summary>
    public Dictionary<string, object> Snapshots { get; } = new();

    /// <summary>
    /// Resets the context to a clean state for a new scenario.
    /// </summary>
    public void Reset()
    {
        PlayerState = null;
        PlayerId = null;
        GameLoop = null;
        TicksProcessed = 0;
        LastActiveTime = DateTimeOffset.UtcNow;
        LastException = null;
        LastActionResult = null;
        GeneratedEvents.Clear();
        TestSeed = null;
        Layout = null;
        ComparisonLayout = null;
        NamedStaff.Clear();
        NamedCustomers.Clear();
        Snapshots.Clear();
    }
}
