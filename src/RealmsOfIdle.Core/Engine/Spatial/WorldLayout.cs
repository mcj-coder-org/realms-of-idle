namespace RealmsOfIdle.Core.Engine.Spatial;

/// <summary>
/// Represents the complete world layout containing multiple areas and connections
/// </summary>
public class WorldLayout
{
    private readonly List<SceneArea> _areas;
    private readonly List<DoorConnection> _doorConnections;
    private readonly Dictionary<string, SceneArea> _areasById;
    private readonly Dictionary<string, Dictionary<GridPosition, DoorConnection>> _doorsByArea;

    /// <summary>
    /// Gets all areas in the layout
    /// </summary>
    public IReadOnlyList<SceneArea> Areas => _areas;

    /// <summary>
    /// Gets all door connections in the layout
    /// </summary>
    public IReadOnlyList<DoorConnection> DoorConnections => _doorConnections;

    /// <summary>
    /// Initializes a new instance of WorldLayout
    /// </summary>
    public WorldLayout()
    {
        _areas = new List<SceneArea>();
        _doorConnections = new List<DoorConnection>();
        _areasById = new Dictionary<string, SceneArea>();
        _doorsByArea = new Dictionary<string, Dictionary<GridPosition, DoorConnection>>();
    }

    /// <summary>
    /// Adds an area to the layout
    /// </summary>
    public void AddArea(SceneArea area)
    {
        ArgumentNullException.ThrowIfNull(area);

        _areasById[area.Id] = area;
        _areas.Add(area);
        _doorsByArea[area.Id] = new Dictionary<GridPosition, DoorConnection>();
    }

    /// <summary>
    /// Gets an area by its ID
    /// </summary>
    public SceneArea? GetArea(string areaId)
    {
        _areasById.TryGetValue(areaId, out var area);
        return area;
    }

    /// <summary>
    /// Adds a door connection between two areas
    /// </summary>
    public void AddDoorConnection(DoorConnection connection)
    {
        ArgumentNullException.ThrowIfNull(connection);

        _doorConnections.Add(connection);

        // Add to side 1
        if (!_doorsByArea.TryGetValue(connection.Side1.AreaId, out var side1Doors))
        {
            side1Doors = new Dictionary<GridPosition, DoorConnection>();
            _doorsByArea[connection.Side1.AreaId] = side1Doors;
        }
        side1Doors[connection.Side1.Position] = connection;

        // Add to side 2
        if (!_doorsByArea.TryGetValue(connection.Side2.AreaId, out var side2Doors))
        {
            side2Doors = new Dictionary<GridPosition, DoorConnection>();
            _doorsByArea[connection.Side2.AreaId] = side2Doors;
        }
        side2Doors[connection.Side2.Position] = connection;

        // Update door tiles in areas
        var area1 = GetArea(connection.Side1.AreaId);
        var area2 = GetArea(connection.Side2.AreaId);

        area1?.AddDoorTile(connection.Side1.Position);
        area2?.AddDoorTile(connection.Side2.Position);
    }

    /// <summary>
    /// Gets the ID of the area connected through a door at the specified position
    /// </summary>
    public string? GetConnectedArea(string areaId, GridPosition doorPosition)
    {
        if (_doorsByArea.TryGetValue(areaId, out var doors))
        {
            if (doors.TryGetValue(doorPosition, out var connection))
            {
                var otherSide = connection.GetOtherSide(new DoorLocation(areaId, doorPosition));
                return otherSide?.AreaId;
            }
        }
        return null;
    }

    /// <summary>
    /// Gets the door connection at a specific position in an area
    /// </summary>
    public DoorLocation? GetDoorAtPosition(string areaId, GridPosition doorPosition)
    {
        if (_doorsByArea.TryGetValue(areaId, out var doors))
        {
            if (doors.TryGetValue(doorPosition, out var connection))
            {
                return connection.Side1.AreaId == areaId ? connection.Side1 : connection.Side2;
            }
        }
        return null;
    }
}
