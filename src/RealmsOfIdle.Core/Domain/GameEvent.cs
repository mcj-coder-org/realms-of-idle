namespace RealmsOfIdle.Core.Domain;

using System;

public class GameEvent
{
    public string EventType { get; set; } = "Unknown";
    public string PlayerId { get; set; } = string.Empty;
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    public string? Data { get; set; }
}
