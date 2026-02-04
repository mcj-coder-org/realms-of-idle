namespace RealmsOfIdle.Core.Domain.Models;

public readonly record GameHealth(
    HealthStatus Status,
    GameMode Mode,
    DateTime Timestamp,
    string? Database = null,
    string? SiloStatus = null,
    Dictionary<string, string>? Dependencies = null);

public enum HealthStatus { Healthy, Unhealthy, Degraded }