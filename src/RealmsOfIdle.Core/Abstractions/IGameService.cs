using RealmsOfIdle.Core.Domain;

namespace RealmsOfIdle.Core.Abstractions;

/// <summary>
/// Core game service interface providing high-level game operations
/// </summary>
public interface IGameService
{
    /// <summary>
    /// Initialize a new game session
    /// </summary>
    Task<GameSession> InitializeGameAsync(string playerId, GameConfiguration configuration);

    /// <summary>
    /// Get active game session for a player
    /// </summary>
    Task<GameSession?> GetActiveSessionAsync(string playerId);

    /// <summary>
    /// Process game tick (time-based progression)
    /// </summary>
    Task<GameSession> ProcessTickAsync(string playerId, TimeSpan deltaTime);

    /// <summary>
    /// Handle player action
    /// </summary>
    Task<ActionResult> HandleActionAsync(string playerId, GameAction action);

    /// <summary>
    /// Save game state
    /// </summary>
    Task SaveGameAsync(string playerId);

    /// <summary>
    /// Load game state
    /// </summary>
    Task<GameSession?> LoadGameAsync(string playerId);

    /// <summary>
    /// Create or get player profile
    /// </summary>
    Task<PlayerProfile> GetOrCreatePlayerProfileAsync(string playerId);

    /// <summary>
    /// Get game statistics
    /// </summary>
    Task<GameStats> GetGameStatsAsync(string playerId);

    /// <summary>
    /// Possess an NPC for direct control
    /// </summary>
    Task<ActionResult> PossessNPCAsync(string npcId);

    /// <summary>
    /// Release possession of an NPC, returning to autonomous behavior
    /// </summary>
    Task ReleaseNPCAsync(string npcId);

    /// <summary>
    /// Execute an action while possessing an NPC
    /// </summary>
    Task<ActionResult> ExecuteActionAsync(string npcId, string actionId);
}
