namespace RealmsOfIdle.Core.Engine.Spatial;

/// <summary>
/// Represents a door location in a specific area
/// </summary>
public readonly record struct DoorLocation(string AreaId, GridPosition Position)
{
    /// <summary>
    /// Gets the area this door leads to
    /// </summary>
    public string ConnectedAreaId { get; init; } = string.Empty;
}

/// <summary>
/// Represents a bidirectional connection between two door locations
/// </summary>
public class DoorConnection
{
    /// <summary>
    /// Gets the first side of the door connection
    /// </summary>
    public DoorLocation Side1 { get; }

    /// <summary>
    /// Gets the second side of the door connection
    /// </summary>
    public DoorLocation Side2 { get; }

    /// <summary>
    /// Initializes a new instance of DoorConnection
    /// </summary>
    public DoorConnection(DoorLocation side1, DoorLocation side2)
    {
        Side1 = side1 with { ConnectedAreaId = side2.AreaId };
        Side2 = side2 with { ConnectedAreaId = side1.AreaId };
    }

    /// <summary>
    /// Gets the other side of the door connection
    /// </summary>
    public DoorLocation? GetOtherSide(DoorLocation side)
    {
        // Compare only AreaId and Position, ignoring ConnectedAreaId
        if (side.AreaId == Side1.AreaId && side.Position.Equals(Side1.Position))
        {
            return Side2;
        }
        if (side.AreaId == Side2.AreaId && side.Position.Equals(Side2.Position))
        {
            return Side1;
        }
        return null;
    }

    /// <summary>
    /// Checks if this connection connects two specific areas
    /// </summary>
    public bool ConnectsAreas(string areaId1, string areaId2)
    {
        return (Side1.AreaId == areaId1 && Side2.AreaId == areaId2) ||
               (Side1.AreaId == areaId2 && Side2.AreaId == areaId1);
    }
}
