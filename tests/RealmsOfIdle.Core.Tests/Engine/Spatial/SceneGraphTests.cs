using RealmsOfIdle.Core.Engine.Spatial;
using Xunit;

namespace RealmsOfIdle.Core.Tests.Engine.Spatial;

/// <summary>
/// Unit tests for SceneGraph
/// </summary>
[Trait("Category", "Unit")]
public class SceneGraphTests
{
    [Fact]
    public void Constructor_CreatesEmptyGraph()
    {
        // Act
        var graph = new SceneGraph();

        // Assert
        Assert.Empty(graph.Nodes);
    }

    [Fact]
    public void AddNode_AddsNodeToGraph()
    {
        // Arrange
        var graph = new SceneGraph();
        var node = new SceneNode("kitchen", new GridPosition(5, 10));

        // Act
        graph.AddNode(node);

        // Assert
        Assert.Single(graph.Nodes);
        Assert.Contains(node, graph.Nodes);
    }

    [Fact]
    public void GetNode_ByName_ReturnsNode()
    {
        // Arrange
        var graph = new SceneGraph();
        var node = new SceneNode("kitchen", new GridPosition(5, 10));
        graph.AddNode(node);

        // Act
        var result = graph.GetNode("kitchen");

        // Assert
        Assert.Same(node, result);
    }

    [Fact]
    public void GetNode_WithUnknownName_ReturnsNull()
    {
        // Arrange
        var graph = new SceneGraph();

        // Act
        var result = graph.GetNode("unknown");

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public void CalculateTravelTime_WithWalkablePath_ReturnsManhattanDistance()
    {
        // Arrange
        var graph = new SceneGraph();
        var node1 = new SceneNode("node1", new GridPosition(0, 0));
        var node2 = new SceneNode("node2", new GridPosition(5, 10));
        graph.AddNode(node1);
        graph.AddNode(node2);

        // Act
        var travelTime = graph.CalculateTravelTime(node1.Name, node2.Name);

        // Assert
        Assert.Equal(15, travelTime); // Manhattan distance: |5-0| + |10-0| = 15
    }

    [Fact]
    public void CalculateTravelTime_SameNode_ReturnsZero()
    {
        // Arrange
        var graph = new SceneGraph();
        var node = new SceneNode("node", new GridPosition(5, 10));
        graph.AddNode(node);

        // Act
        var travelTime = graph.CalculateTravelTime(node.Name, node.Name);

        // Assert
        Assert.Equal(0, travelTime);
    }

    [Fact]
    public void CalculateTravelTime_WithUnknownNode_ReturnsMinusOne()
    {
        // Arrange
        var graph = new SceneGraph();
        var node = new SceneNode("node", new GridPosition(5, 10));
        graph.AddNode(node);

        // Act
        var travelTime = graph.CalculateTravelTime(node.Name, "unknown");

        // Assert
        Assert.Equal(-1, travelTime);
    }

    [Fact]
    public void GenerateFromTileGrid_CreatesNodesForFacilities()
    {
        // Arrange
        var grid = new TileGrid(10, 10);
        grid.SetTile(2, 3, TileType.Furniture, "kitchen");
        grid.SetTile(5, 5, TileType.Furniture, "bar");

        // Act
        var graph = SceneGraph.GenerateFromTileGrid(grid, "main_hall");

        // Assert
        Assert.Equal(2, graph.Nodes.Count);
        Assert.NotNull(graph.GetNode("kitchen"));
        Assert.NotNull(graph.GetNode("bar"));
    }

    [Fact]
    public void GenerateFromTileGrid_SetsNodePositionsCorrectly()
    {
        // Arrange
        var grid = new TileGrid(10, 10);
        grid.SetTile(2, 3, TileType.Furniture, "kitchen");

        // Act
        var graph = SceneGraph.GenerateFromTileGrid(grid, "main_hall");
        var node = graph.GetNode("kitchen");

        // Assert
        Assert.Equal(new GridPosition(2, 3), node?.Position);
    }

    [Fact]
    public void GenerateFromTileGrid_SetsAreaId()
    {
        // Arrange
        var grid = new TileGrid(10, 10);
        grid.SetTile(2, 3, TileType.Furniture, "kitchen");

        // Act
        var graph = SceneGraph.GenerateFromTileGrid(grid, "main_hall");
        var node = graph.GetNode("kitchen");

        // Assert
        Assert.Equal("main_hall", node?.AreaId);
    }
}
