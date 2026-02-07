namespace RealmsOfIdle.Client.UI.Components;

/// <summary>
/// Represents a single tile in a tile grid
/// </summary>
public record Tile
{
    public TileType Type { get; init; } = TileType.Empty;
    public int Variant { get; init; }
    public int Decoration { get; init; }

    public static Tile Floor(int variant = 0) => new() { Type = TileType.Floor, Variant = variant };
    public static Tile Wall(int variant = 0) => new() { Type = TileType.Wall, Variant = variant };
    public static Tile Door => new() { Type = TileType.Door };
    public static Tile Table => new() { Type = TileType.Table };
    public static Tile Chair => new() { Type = TileType.Chair };
    public static Tile Counter => new() { Type = TileType.Counter };
    public static Tile Oven => new() { Type = TileType.Oven };
    public static Tile Bed => new() { Type = TileType.Bed };
    public static Tile Fireplace => new() { Type = TileType.Fireplace };
    public static Tile Empty => new() { Type = TileType.Empty };
}

/// <summary>
/// Position in tile grid coordinates
/// </summary>
public record TilePosition(int X, int Y)
{
    public static TilePosition Zero => new(0, 0);

    public static TilePosition Add(TilePosition left, Direction right) => right switch
    {
        Direction.None => left,
        Direction.North => new TilePosition(left.X, left.Y - 1),
        Direction.NorthEast => new TilePosition(left.X + 1, left.Y - 1),
        Direction.East => new TilePosition(left.X + 1, left.Y),
        Direction.SouthEast => new TilePosition(left.X + 1, left.Y + 1),
        Direction.South => new TilePosition(left.X, left.Y + 1),
        Direction.SouthWest => new TilePosition(left.X - 1, left.Y + 1),
        Direction.West => new TilePosition(left.X - 1, left.Y),
        Direction.NorthWest => new TilePosition(left.X - 1, left.Y - 1),
        _ => left
    };

    public static TilePosition operator +(TilePosition left, Direction right) => Add(left, right);

    public TilePosition Neighbor(Direction direction) => this + direction;
}

/// <summary>
/// Represents an area/room in the game world
/// </summary>
public record Area(string Id, string Name, int Width, int Height, TilePosition Origin)
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Performance", "CA1819:Properties should not return arrays", Justification = "2D array is appropriate for tile grid representation")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Performance", "CA1814:Multidimensional arrays should not be used", Justification = "2D array is appropriate for tile grid representation")]
    public Tile[,] Tiles { get; init; } = new Tile[Width, Height];

    public Area() : this(string.Empty, string.Empty, 0, 0, TilePosition.Zero) { }
}

/// <summary>
/// Represents the entire game world composed of areas
/// </summary>
public record World(int Width, int Height)
{
    public IReadOnlyCollection<Area> Areas { get; init; } = Array.Empty<Area>();

    public World() : this(0, 0) { }

    public World WithArea(Area area) => this with { Areas = Areas.Concat(new[] { area }).ToList() };
}
