using RealmsOfIdle.Core.Domain;
using RealmsOfIdle.Core.Domain.Models;
using RealmsOfIdle.Core.Engine;
using RealmsOfIdle.Core.Engine.Spatial;
using RealmsOfIdle.Core.Infrastructure;
using RealmsOfIdle.Core.Scenarios.Inn;
using RealmsOfIdle.Core.Scenarios.Inn.GameLoop;

namespace RealmsOfIdle.SimulationTests.Infrastructure.Drivers;

/// <summary>
/// Driver for interacting with the game engine in simulation tests.
/// Provides a high-level API for executing game actions and processing ticks.
/// </summary>
public class GameEngineDriver
{
    private readonly GameTestContext _context;
    private DeterministicRng _rng;

    public GameEngineDriver(GameTestContext context)
    {
        _context = context;
        _rng = new DeterministicRng(context.TestSeed ?? 42);
    }

    /// <summary>
    /// Creates a new player and initializes the inn game loop with generated layout.
    /// </summary>
    public void CreateNewPlayer(int seed = 42)
    {
        _context.TestSeed = seed;
        _rng = new DeterministicRng(seed);
        _context.PlayerId = new PlayerId("test-player");
        _context.PlayerState = new PlayerState(_context.PlayerId.Value, "Test Player", 1, GameMode.Offline);
        _context.LastActiveTime = DateTimeOffset.UtcNow;

        InitializeInn(seed);
    }

    /// <summary>
    /// Initializes the inn with a generated layout, default facilities, and game loop.
    /// </summary>
    public void InitializeInn(int seed, int gold = 100, int reputation = 0)
    {
        var layout = LayoutGenerator.GenerateInnLayout(seed);
        _context.Layout = layout;

        var facilities = CreateDefaultFacilities();
        var innState = new InnState(layout, facilities,
            Customers: Array.Empty<Customer>(),
            Staff: Array.Empty<StaffMember>(),
            Gold: gold,
            Reputation: reputation);
        _context.GameLoop = new InnGameLoop(innState, new DeterministicRng(seed));
    }

    /// <summary>
    /// Creates default facilities dictionary for a starting inn.
    /// </summary>
    public static Dictionary<string, InnFacility> CreateDefaultFacilities()
    {
        return new Dictionary<string, InnFacility>
        {
            ["kitchen"] = new InnFacility("Kitchen", 1, 1, 1.0, 100),
            ["bar"] = new InnFacility("Bar", 1, 1, 1.0, 100),
            ["table_1"] = new InnFacility("Table", 1, 4, 1.0, 50),
            ["table_2"] = new InnFacility("Table", 1, 4, 1.0, 50),
            ["table_3"] = new InnFacility("Table", 1, 4, 1.0, 50),
            ["guest_bed_1"] = new InnFacility("GuestRoom", 1, 1, 0, 50),
            ["guest_bed_2"] = new InnFacility("GuestRoom", 1, 1, 0, 50),
            ["guest_bed_3"] = new InnFacility("GuestRoom", 1, 1, 0, 50),
            ["staff_bed_1"] = new InnFacility("StaffBed", 1, 1, 0, 30),
            ["staff_bed_2"] = new InnFacility("StaffBed", 1, 1, 0, 30),
            ["staff_bed_3"] = new InnFacility("StaffBed", 1, 1, 0, 30),
        };
    }

    /// <summary>
    /// Simulates the passage of time by processing the specified number of ticks.
    /// </summary>
    public void ProcessTicks(int tickCount)
    {
        if (_context.GameLoop == null)
        {
            throw new InvalidOperationException("GameLoop not initialized. Call CreateNewPlayer first.");
        }

        for (int i = 0; i < tickCount; i++)
        {
            _context.GameLoop.ProcessTick();
            _context.TicksProcessed++;
        }
    }

    /// <summary>
    /// Processes a single game tick.
    /// </summary>
    public void ProcessSingleTick()
    {
        ProcessTicks(1);
    }

    /// <summary>
    /// Simulates an offline period using the OfflineCatchupCalculator.
    /// </summary>
    public void SimulateOfflinePeriod(TimeSpan duration)
    {
        var calculator = new OfflineCatchupCalculator(tickRate: 10); // 10 ticks per second
        var lastActive = DateTime.UtcNow - duration;
        var catchupTicks = calculator.CalculateCatchupTicks(lastActive, DateTime.UtcNow);

        ProcessTicks(catchupTicks);
        _context.LastActiveTime = DateTimeOffset.UtcNow;
    }

    /// <summary>
    /// Executes an inn action through the game loop.
    /// </summary>
    public ActionResult ExecuteAction(InnAction action)
    {
        if (_context.GameLoop == null)
        {
            throw new InvalidOperationException("GameLoop not initialized.");
        }

        var result = _context.GameLoop.ProcessAction(action);
        _context.LastActionResult = result;
        return result;
    }

    /// <summary>
    /// Gets the current InnState from the game loop.
    /// </summary>
    public InnState GetInnState()
    {
        if (_context.GameLoop == null)
        {
            throw new InvalidOperationException("GameLoop not initialized.");
        }

        return _context.GameLoop.State;
    }

    /// <summary>
    /// Adds a staff member to the inn via the game loop.
    /// </summary>
    public void AddStaff(StaffMember staff)
    {
        if (_context.GameLoop == null)
        {
            throw new InvalidOperationException("GameLoop not initialized.");
        }

        var newState = _context.GameLoop.State.AddStaff(staff);
        ReplaceState(newState);
    }

    /// <summary>
    /// Adds a customer to the inn via the game loop.
    /// </summary>
    public void AddCustomer(Customer customer)
    {
        if (_context.GameLoop == null)
        {
            throw new InvalidOperationException("GameLoop not initialized.");
        }

        var newState = _context.GameLoop.State.AddCustomer(customer);
        ReplaceState(newState);
    }

    /// <summary>
    /// Sets gold on the inn state.
    /// </summary>
    public void SetGold(int gold)
    {
        if (_context.GameLoop == null)
        {
            throw new InvalidOperationException("GameLoop not initialized.");
        }

        var state = _context.GameLoop.State;
        var newState = state with { Gold = gold };
        ReplaceState(newState);
    }

    /// <summary>
    /// Sets reputation on the inn state.
    /// </summary>
    public void SetReputation(int reputation)
    {
        if (_context.GameLoop == null)
        {
            throw new InvalidOperationException("GameLoop not initialized.");
        }

        _context.GameLoop.SetReputation(reputation);
    }

    /// <summary>
    /// Replaces the current inn state by reconstructing the game loop with the new state.
    /// </summary>
    public void ReplaceState(InnState newState)
    {
        _context.GameLoop = new InnGameLoop(newState, new DeterministicRng(_context.TestSeed ?? 42));
    }

    /// <summary>
    /// Gets the current deterministic RNG instance for test reproducibility.
    /// </summary>
    public DeterministicRng GetRng() => _rng;
}
