namespace RealmsOfIdle.Core.Engine.Spatial;

/// <summary>
/// Represents a graph of scene nodes with calculated travel times
/// </summary>
public class SceneGraph
{
    private readonly List<SceneNode> _nodes;
    private readonly Dictionary<string, SceneNode> _nodesByName;

    /// <summary>
    /// Gets all nodes in the graph
    /// </summary>
    public IReadOnlyList<SceneNode> Nodes => _nodes;

    /// <summary>
    /// Initializes a new instance of SceneGraph
    /// </summary>
    public SceneGraph()
    {
        _nodes = new List<SceneNode>();
        _nodesByName = new Dictionary<string, SceneNode>(StringComparer.OrdinalIgnoreCase);
    }

    /// <summary>
    /// Adds a node to the graph
    /// </summary>
    public void AddNode(SceneNode node)
    {
        ArgumentNullException.ThrowIfNull(node);
        _nodesByName[node.Name] = node;
        _nodes.Add(node);
    }

    /// <summary>
    /// Gets a node by name
    /// </summary>
    public SceneNode? GetNode(string name)
    {
        _nodesByName.TryGetValue(name, out var node);
        return node;
    }

    /// <summary>
    /// Calculates the travel time between two nodes using Manhattan distance
    /// </summary>
    public int CalculateTravelTime(string fromNode, string toNode)
    {
        if (!_nodesByName.TryGetValue(fromNode, out var from) ||
            !_nodesByName.TryGetValue(toNode, out var to))
        {
            return -1;
        }

        return from.Position.ManhattanDistanceTo(to.Position);
    }

    /// <summary>
    /// Generates a scene graph from a tile grid by creating nodes for furniture tiles
    /// </summary>
    public static SceneGraph GenerateFromTileGrid(TileGrid grid, string areaId)
    {
        ArgumentNullException.ThrowIfNull(grid);
        ArgumentNullException.ThrowIfNull(areaId);

        var graph = new SceneGraph();

        for (int x = 0; x < grid.Width; x++)
        {
            for (int y = 0; y < grid.Height; y++)
            {
                var facilityId = grid.GetFacilityId(x, y);
                if (!string.IsNullOrEmpty(facilityId))
                {
                    var node = new SceneNode(facilityId, new GridPosition(x, y), areaId);
                    graph.AddNode(node);
                }
            }
        }

        return graph;
    }
}
