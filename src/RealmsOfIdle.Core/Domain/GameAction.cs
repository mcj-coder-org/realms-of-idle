namespace RealmsOfIdle.Core.Domain;

public readonly record GameAction(
    string ActionName,
    string? TargetId = null,
    Dictionary<string, object>? Parameters = null);