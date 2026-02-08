namespace RealmsOfIdle.Core.Engine.Spatial;

/// <summary>
/// Represents the type of a tile in a grid
/// </summary>
public sealed record class TileType(string Name, bool IsWalkable, bool IsDoor)
{
    /// <summary>
    /// Empty tile - no content
    /// </summary>
    public static readonly TileType Empty = new("Empty", IsWalkable: false, IsDoor: false);

    /// <summary>
    /// Floor tile - walkable surface
    /// </summary>
    public static readonly TileType Floor = new("Floor", IsWalkable: true, IsDoor: false);

    /// <summary>
    /// Wall tile - blocks movement
    /// </summary>
    public static readonly TileType Wall = new("Wall", IsWalkable: false, IsDoor: false);

    /// <summary>
    /// Door tile - walkable, connects areas
    /// </summary>
    public static readonly TileType Door = new("Door", IsWalkable: true, IsDoor: true);

    /// <summary>
    /// Furniture tile - blocks movement, represents objects
    /// </summary>
    public static readonly TileType Furniture = new("Furniture", IsWalkable: false, IsDoor: false);
}
