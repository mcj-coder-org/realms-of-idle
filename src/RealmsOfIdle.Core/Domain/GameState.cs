namespace RealmsOfIdle.Core.Domain;

/// <summary>
/// Base class for game state across all scenarios
/// </summary>
public abstract class GameState
{
    /// <summary>
    /// Gets the player ID associated with this game state
    /// </summary>
    public PlayerId PlayerId { get; init; }

    /// <summary>
    /// Gets the current game version
    /// </summary>
    public string GameVersion { get; init; } = "1.0.0";

    /// <summary>
    /// Gets the timestamp when this state was last modified
    /// </summary>
    public DateTime LastModified { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Initializes a new instance of GameState
    /// </summary>
    protected GameState()
    {
    }

    /// <summary>
    /// Initializes a new instance of GameState with a player ID
    /// </summary>
    protected GameState(PlayerId playerId)
    {
        PlayerId = playerId;
    }
}
