using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RealmsOfIdle.Core.Domain;
using RealmsOfIdle.Core.Domain.Models;

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
}