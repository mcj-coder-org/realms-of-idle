namespace RealmsOfIdle.Core.Domain;

using RealmsOfIdle.Core.Scenarios.Inn;

public class GameSession
{
    public string SessionId { get; set; } = string.Empty;
    public string PlayerId { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime LastUpdated { get; set; } = DateTime.UtcNow;
    public PlayerState PlayerState { get; set; } = new(string.Empty, "Player", 1, 0);
    public GameConfiguration Configuration { get; set; } = new();
    public bool IsActive { get; set; } = true;

    /// <summary>
    /// Gets or sets the Inn scenario state
    /// Note: Not persisted directly - stored separately via InnStateDto in the persistence layer
    /// </summary>
    public InnState? InnState { get; set; }

    /// <summary>
    /// Gets or sets the current tick number
    /// </summary>
    public int CurrentTick { get; set; }

    /// <summary>
    /// Gets or sets the last time a tick was processed
    /// </summary>
    public DateTime LastTickTime { get; set; } = DateTime.UtcNow;
}
