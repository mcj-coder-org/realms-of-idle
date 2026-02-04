namespace RealmsOfIdle.Core;

public enum HealthStatus
{
    Healthy,
    Degraded,
    Unhealthy
}

public class GameHealth
{
    public HealthStatus Status { get; set; }
    public string? SiloStatus { get; set; }
    public DateTime Timestamp { get; set; }
    public Dictionary<string, string>? Dependencies { get; set; }
}