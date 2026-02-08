namespace RealmsOfIdle.Core.Engine.Spatial;

/// <summary>
/// Represents a single area in a scene with its own tile grid
/// </summary>
public class SceneArea
{
    private static readonly HashSet<string> _usedIds = new();
    private static int _idCounter;
    private static readonly object _lock = new();

    /// <summary>
    /// Gets the unique identifier for this area
    /// </summary>
    public string Id { get; }

    /// <summary>
    /// Gets the display name of this area
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// Gets the tile grid for this area
    /// </summary>
    public TileGrid Grid { get; }

    /// <summary>
    /// Gets the door tiles in this area
    /// </summary>
    public ISet<GridPosition> DoorTiles { get; }

    /// <summary>
    /// Initializes a new instance of SceneArea with an auto-generated ID
    /// </summary>
    public SceneArea(string name, int width, int height)
        : this(GenerateUniqueId(), name, width, height)
    {
    }

    /// <summary>
    /// Initializes a new instance of SceneArea with a specific ID
    /// </summary>
    public SceneArea(string id, string name, int width, int height)
    {
        Id = id;
        Name = name;
        Grid = new TileGrid(width, height);
        DoorTiles = new HashSet<GridPosition>();
    }

    /// <summary>
    /// Adds a door tile to this area
    /// </summary>
    public void AddDoorTile(GridPosition position)
    {
        DoorTiles.Add(position);
        Grid.SetTile(position.X, position.Y, TileType.Door);
    }

    /// <summary>
    /// Checks if a position is a door tile
    /// </summary>
    public bool IsDoorTile(GridPosition position)
    {
        return DoorTiles.Contains(position);
    }

    private static string GenerateUniqueId()
    {
        lock (_lock)
        {
            string id;
            do
            {
                id = $"area_{_idCounter++}";
            } while (_usedIds.Contains(id));

            _usedIds.Add(id);
            return id;
        }
    }
}
