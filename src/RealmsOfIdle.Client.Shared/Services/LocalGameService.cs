using LiteDB;
using Microsoft.Extensions.Logging;
using RealmsOfIdle.Core.Abstractions;
using RealmsOfIdle.Core.Domain;
using RealmsOfIdle.Core.Engine.Spatial;
using RealmsOfIdle.Core.Infrastructure;
using RealmsOfIdle.Core.Scenarios.Inn;
using RealmsOfIdle.Core.Scenarios.Inn.GameLoop;
using RealmsOfIdle.Core.Scenarios.Inn.Persistence;

namespace RealmsOfIdle.Client.Shared.Services;

/// <summary>
/// Local game service for offline play - stores data in LiteDB
/// </summary>
public class LocalGameService : IGameService
{
    private readonly LiteDatabase _database;
    private readonly ILogger<LocalGameService> _logger;
    private readonly Dictionary<string, InnGameLoop> _activeGames = new();
    private readonly TimeSpan _tickInterval = TimeSpan.FromMilliseconds(100);

    public LocalGameService(LiteDatabase database, ILogger<LocalGameService> logger)
    {
        _database = database;
        _logger = logger;
    }

    public Task<GameSession> InitializeGameAsync(string playerId, GameConfiguration configuration)
    {
        _logger.LogInformation("Initializing new game for player: {PlayerId}", playerId);

        // Create RNG with seed for deterministic results
        var seed = Random.Shared.Next();
        var layout = LayoutGenerator.GenerateInnLayout(seed);
        var facilities = CreateInitialFacilities();
        var initialState = new InnState(layout, facilities);

        var rng = new DeterministicRng(seed);

        // Create game loop
        var gameLoop = new InnGameLoop(initialState, rng);
        _activeGames[playerId] = gameLoop;

        // Create session
        var session = new GameSession
        {
            SessionId = Guid.NewGuid().ToString(),
            PlayerId = playerId,
            CreatedAt = DateTime.UtcNow,
            LastUpdated = DateTime.UtcNow,
            LastTickTime = DateTime.UtcNow,
            CurrentTick = 0,
            InnState = gameLoop.State,
            Configuration = configuration
        };

        // Persist initial state
        var innStateDto = InnStateDto.FromDomain(gameLoop.State, playerId, 0);
        var innStates = _database.GetCollection<InnStateDto>("inn_states");
        innStates.EnsureIndex(x => x.PlayerId);
        innStates.Upsert(innStateDto);

        // Persist session without InnState (to avoid circular reference issues)
        var sessions = _database.GetCollection<GameSession>("sessions");
        sessions.EnsureIndex(x => x.PlayerId);

        // Create a shallow session for persistence (without InnState)
        var sessionForPersistence = new GameSession
        {
            SessionId = session.SessionId,
            PlayerId = session.PlayerId,
            CreatedAt = session.CreatedAt,
            LastUpdated = session.LastUpdated,
            PlayerState = session.PlayerState,
            Configuration = session.Configuration,
            IsActive = session.IsActive,
            CurrentTick = session.CurrentTick,
            LastTickTime = session.LastTickTime
            // Note: InnState is not persisted here - it's stored separately via InnStateDto
        };
        sessions.Upsert(new BsonValue(session.SessionId), sessionForPersistence);

        _logger.LogInformation("Game initialized for player: {PlayerId}, SessionId: {SessionId}", playerId, session.SessionId);

        return Task.FromResult(session);
    }

    public Task<GameSession?> GetActiveSessionAsync(string playerId)
    {
        var sessions = _database.GetCollection<GameSession>("sessions");
        var session = sessions.FindOne(x => x.PlayerId == playerId && x.IsActive);
        return Task.FromResult<GameSession?>(session);
    }

    public async Task<GameSession> ProcessTickAsync(string playerId, TimeSpan deltaTime)
    {
        if (!_activeGames.TryGetValue(playerId, out var gameLoop))
        {
            _logger.LogWarning("No active game found for player: {PlayerId}", playerId);
            return await LoadAndResumeGame(playerId);
        }

        // Offline catch-up: simulate ticks based on time since last tick
        var session = await GetActiveSessionAsync(playerId);
        if (session == null)
        {
            _logger.LogWarning("No session found for player: {PlayerId}", playerId);
            return new GameSession { PlayerId = playerId };
        }

        var timeSinceLastTick = DateTime.UtcNow - session.LastTickTime;
        var ticksToSimulate = (int)(timeSinceLastTick.TotalMilliseconds / _tickInterval.TotalMilliseconds);

        // Cap at 1000 ticks to prevent excessive simulation
        ticksToSimulate = Math.Min(ticksToSimulate, 1000);

        _logger.LogInformation("Simulating {TickCount} ticks for player: {PlayerId}", ticksToSimulate, playerId);

        for (var i = 0; i < ticksToSimulate; i++)
        {
            gameLoop.ProcessTick();
        }

        session.CurrentTick += ticksToSimulate;
        session.LastTickTime = DateTime.UtcNow;
        session.LastUpdated = DateTime.UtcNow;
        session.InnState = gameLoop.State;

        // Auto-save after batch of ticks
        if (ticksToSimulate > 0)
        {
            await SaveGameAsync(playerId);
        }

        // Update session without InnState for persistence
        await UpdateSessionAsync(session);

        return session;
    }

    public Task<ActionResult> HandleActionAsync(string playerId, GameAction action)
    {
        if (!_activeGames.TryGetValue(playerId, out var gameLoop))
        {
            return Task.FromResult(ActionResult.Fail("No active game. Please start a new game."));
        }

        // Map GameAction to InnAction based on action name
        var innAction = MapToInnAction(action);
        if (innAction == null)
        {
            return Task.FromResult(ActionResult.Fail($"Unknown action: {action.ActionName}"));
        }

        return HandleInnAction(gameLoop, innAction);
    }

    private static InnAction? MapToInnAction(GameAction action)
    {
        return action.ActionName switch
        {
            InnActionTypes.UpgradeKitchen => InnAction.UpgradeKitchen(),
            InnActionTypes.UpgradeBar => InnAction.UpgradeBar(),
            InnActionTypes.UpgradeRooms => InnAction.UpgradeRooms(),
            InnActionTypes.AddGuestRoom => InnAction.AddGuestRoom(),
            InnActionTypes.AddStaffBed => InnAction.AddStaffBed(),
            InnActionTypes.Advertise => InnAction.Advertise(),
            _ => null
        };
    }

    public async Task SaveGameAsync(string playerId)
    {
        if (!_activeGames.TryGetValue(playerId, out var gameLoop))
        {
            _logger.LogWarning("Cannot save: No active game for player: {PlayerId}", playerId);
            return;
        }

        var innStateDto = InnStateDto.FromDomain(gameLoop.State, playerId, gameLoop.CurrentTick);
        var innStates = _database.GetCollection<InnStateDto>("inn_states");
        innStates.Upsert(innStateDto);

        var session = await GetActiveSessionAsync(playerId);
        if (session != null)
        {
            session.LastUpdated = DateTime.UtcNow;
            session.CurrentTick = gameLoop.CurrentTick;
            await UpdateSessionAsync(session);
        }

        _logger.LogDebug("Game saved for player: {PlayerId}", playerId);
    }

    /// <summary>
    /// Updates the session in the database without serializing InnState
    /// </summary>
    private Task UpdateSessionAsync(GameSession session)
    {
        var sessions = _database.GetCollection<GameSession>("sessions");

        // Create a shallow session for persistence (without InnState)
        var sessionForPersistence = new GameSession
        {
            SessionId = session.SessionId,
            PlayerId = session.PlayerId,
            CreatedAt = session.CreatedAt,
            LastUpdated = session.LastUpdated,
            PlayerState = session.PlayerState,
            Configuration = session.Configuration,
            IsActive = session.IsActive,
            CurrentTick = session.CurrentTick,
            LastTickTime = session.LastTickTime
        };
        sessions.Upsert(new BsonValue(session.SessionId), sessionForPersistence);
        return Task.CompletedTask;
    }

    public async Task<GameSession?> LoadGameAsync(string playerId)
    {
        _logger.LogInformation("Loading game for player: {PlayerId}", playerId);

        var innStates = _database.GetCollection<InnStateDto>("inn_states");
        var innStateDto = innStates.FindOne(x => x.PlayerId == playerId);

        if (innStateDto == null)
        {
            _logger.LogWarning("No saved game found for player: {PlayerId}", playerId);
            return null;
        }

        var sessions = _database.GetCollection<GameSession>("sessions");
        var session = sessions.FindOne(x => x.PlayerId == playerId && x.IsActive);

        if (session == null)
        {
            _logger.LogWarning("No active session found for player: {PlayerId}", playerId);
            return null;
        }

        // Restore Inn state
        var innState = innStateDto.ToDomain();

        // Recreate game loop with restored state
        var seed = innStateDto.LastTickTime.Millisecond;
        var rng = new DeterministicRng(seed);
        var gameLoop = new InnGameLoop(innState, rng);

        // Fast-forward to saved tick count
        for (var i = 0; i < innStateDto.CurrentTick; i++)
        {
            gameLoop.ProcessTick();
        }

        _activeGames[playerId] = gameLoop;

        session.InnState = gameLoop.State;
        session.CurrentTick = gameLoop.CurrentTick;

        _logger.LogInformation("Game loaded for player: {PlayerId}, Tick: {TickCount}", playerId, gameLoop.CurrentTick);

        return session;
    }

    public Task<PlayerProfile> GetOrCreatePlayerProfileAsync(string playerId)
    {
        var profiles = _database.GetCollection<PlayerProfile>("profiles");
        var profile = profiles.FindById(playerId);

        if (profile == null)
        {
            profile = new PlayerProfile
            {
                PlayerId = playerId,
                FirstLogin = DateTime.UtcNow,
                LastLogin = DateTime.UtcNow
            };
            profiles.Insert(profile);
        }
        else
        {
            profile.LastLogin = DateTime.UtcNow;
            profiles.Update(profile);
        }

        return Task.FromResult(profile);
    }

    public Task<GameStats> GetGameStatsAsync(string playerId)
    {
        var innStates = _database.GetCollection<InnStateDto>("inn_states");
        var innState = innStates.FindOne(x => x.PlayerId == playerId);

        if (innState == null)
        {
            return Task.FromResult(new GameStats
            {
                FirstPlayed = DateTime.UtcNow,
                LastPlayed = DateTime.UtcNow
            });
        }

        return Task.FromResult(new GameStats
        {
            FirstPlayed = innState.LastTickTime,
            LastPlayed = DateTime.UtcNow,
            TotalTicks = innState.CurrentTick
        });
    }

    private Task<ActionResult> HandleInnAction(InnGameLoop gameLoop, InnAction action)
    {
        var result = gameLoop.ProcessAction(action);
        return Task.FromResult(result);
    }

    private async Task<GameSession> LoadAndResumeGame(string playerId)
    {
        var session = await LoadGameAsync(playerId);
        if (session == null)
        {
            return new GameSession { PlayerId = playerId };
        }
        return session;
    }

    private static Dictionary<string, InnFacility> CreateInitialFacilities()
    {
        return new Dictionary<string, InnFacility>
        {
            { "kitchen", new InnFacility("Kitchen", Level: 1, Capacity: 1, ProductionRate: 1.0, UpgradeCost: 100) },
            { "bar", new InnFacility("Bar", Level: 1, Capacity: 2, ProductionRate: 0.5, UpgradeCost: 150) },
            { "tables", new InnFacility("Tables", Level: 1, Capacity: 4, ProductionRate: 0, UpgradeCost: 200) }
        };
    }
}
