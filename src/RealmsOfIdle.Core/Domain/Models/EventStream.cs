namespace RealmsOfIdle.Core.Domain.Models;

/// <summary>
/// Represents a stream of events for a specific player
/// </summary>
public class EventStream
{
    public string PlayerId { get; set; } = string.Empty;
    public long CurrentSequence { get; set; }
    public List<GameEvent> Events { get; set; } = new();
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}
