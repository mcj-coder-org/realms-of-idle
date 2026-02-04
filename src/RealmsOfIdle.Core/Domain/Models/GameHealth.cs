#pragma warning disable IDE0005 // Using directive is required by Orleans source generator
using Orleans;
#pragma warning restore IDE0005

namespace RealmsOfIdle.Core.Domain.Models;

[Immutable]
[GenerateSerializer]
public record GameHealth(
    HealthStatus Status,
    GameMode Mode,
    DateTime Timestamp,
    string? Database = null,
    string? SiloStatus = null,
    Dictionary<string, string>? Dependencies = null);
