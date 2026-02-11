using RealmsOfIdle.Core.Engine.Spatial;

namespace RealmsOfIdle.Client.Blazor.Models;

/// <summary>
/// Building type enumeration
/// </summary>
public enum BuildingType
{
    Inn,
    Workshop,
    Residential,
    Market
}

/// <summary>
/// Upgrade status tracking for buildings
/// </summary>
public sealed record BuildingUpgradeStatus(
    int TargetLevel,
    DateTime StartTime,
    int DurationSeconds);

/// <summary>
/// Physical structure within settlement (Inn, Workshop, etc.)
/// </summary>
public sealed record Building(
    string Id,
    string Name,
    BuildingType Type,
    GridPosition Position,
    int Width,
    int Height,
    IReadOnlyDictionary<string, int> Resources,
    int Gold,
    IReadOnlyList<string> EmployedNPCs,
    int Level = 1,
    BuildingUpgradeStatus? UpgradeInProgress = null);
