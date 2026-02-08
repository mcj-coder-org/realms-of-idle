namespace RealmsOfIdle.Core.Engine.Spatial;

/// <summary>
/// Represents a 2D grid of tiles for a single scene area
/// </summary>
public class TileGrid
{
    private readonly TileType[][] _tiles;
    private readonly string?[][] _facilityIds;

    /// <summary>
    /// Gets the width of the grid (number of columns)
    /// </summary>
    public int Width { get; }

    /// <summary>
    /// Gets the height of the grid (number of rows)
    /// </summary>
    public int Height { get; }

    /// <summary>
    /// Initializes a new instance of TileGrid
    /// </summary>
    /// <param name="width">The width of the grid</param>
    /// <param name="height">The height of the grid</param>
    public TileGrid(int width, int height)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(width);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(height);

        Width = width;
        Height = height;
        _tiles = new TileType[width][];
        _facilityIds = new string?[width][];

        for (int x = 0; x < width; x++)
        {
            _tiles[x] = new TileType[height];
            _facilityIds[x] = new string?[height];

            // Initialize all tiles as Empty
            for (int y = 0; y < height; y++)
            {
                _tiles[x][y] = TileType.Empty;
            }
        }
    }

    /// <summary>
    /// Gets the tile type at the specified position
    /// </summary>
    public TileType GetTile(int x, int y)
    {
        ValidateCoordinates(x, y);
        return _tiles[x][y];
    }

    /// <summary>
    /// Sets the tile type at the specified position
    /// </summary>
    public void SetTile(int x, int y, TileType tileType, string? facilityId = null)
    {
        ValidateCoordinates(x, y);
        _tiles[x][y] = tileType;
        _facilityIds[x][y] = facilityId;
    }

    /// <summary>
    /// Gets the facility ID associated with the tile at the specified position
    /// </summary>
    public string? GetFacilityId(int x, int y)
    {
        ValidateCoordinates(x, y);
        return _facilityIds[x][y];
    }

    /// <summary>
    /// Checks if a tile at the specified position is walkable
    /// </summary>
    public bool IsWalkable(int x, int y)
    {
        ValidateCoordinates(x, y);
        return _tiles[x][y].IsWalkable;
    }

    /// <summary>
    /// Fills the entire grid with the specified tile type
    /// </summary>
    public void Fill(TileType tileType)
    {
        for (int x = 0; x < Width; x++)
        {
            for (int y = 0; y < Height; y++)
            {
                _tiles[x][y] = tileType;
            }
        }
    }

    private void ValidateCoordinates(int x, int y)
    {
        if (x < 0 || x >= Width)
        {
            throw new ArgumentOutOfRangeException(nameof(x), $"X coordinate {x} is outside grid bounds [0, {Width})");
        }
        if (y < 0 || y >= Height)
        {
            throw new ArgumentOutOfRangeException(nameof(y), $"Y coordinate {y} is outside grid bounds [0, {Height})");
        }
    }
}
