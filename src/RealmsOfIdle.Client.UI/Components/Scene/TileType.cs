namespace RealmsOfIdle.Client.UI.Components;

/// <summary>
/// Tile types for the scene renderer
/// </summary>
public enum TileType
{
    Empty = 0,
    Floor = 1,
    Floor2 = 2,
    Floor3 = 3,
    Wall = 10,
    Wall2 = 11,
    Door = 20,
    Table = 30,
    Chair = 31,
    Counter = 32,
    Oven = 33,
    Bed = 34,
    Fireplace = 35,
    Decoration = 99
}

/// <summary>
/// Direction for 8-way movement
/// </summary>
public enum Direction
{
    None = 0,
    North = 1,
    NorthEast = 2,
    East = 3,
    SouthEast = 4,
    South = 5,
    SouthWest = 6,
    West = 7,
    NorthWest = 8
}

/// <summary>
/// Camera mode for viewport control
/// </summary>
public enum CameraMode
{
    /// <summary>Manual panning with drag/WASD/arrows</summary>
    Manual,
    /// <summary>Camera follows a target (character)</summary>
    Follow
}
