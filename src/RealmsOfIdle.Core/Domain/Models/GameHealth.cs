namespace RealmsOfIdle.Core.Domain.Models;

using System;
using System.Collections.Generic;

public record GameHealth(
    HealthStatus Status,
    GameMode Mode,
    DateTime Timestamp,
    string? Database = null,
    string? SiloStatus = null,
    Dictionary<string, string>? Dependencies = null);