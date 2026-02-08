using RealmsOfIdle.Core.Engine.Spatial;
using Xunit;

namespace RealmsOfIdle.Core.Tests.Engine.Spatial;

/// <summary>
/// Unit tests for MovementProcessor
/// </summary>
[Trait("Category", "Unit")]
public class MovementProcessorTests
{
    [Fact]
    public void ProcessMovement_NotTraveling_ReturnsSamePosition()
    {
        // Arrange
        var position = new EntityPosition("kitchen");
        var graph = new SceneGraph();
        graph.AddNode(new SceneNode("kitchen", new GridPosition(0, 0)));

        // Act
        var result = MovementProcessor.ProcessMovement(position, graph, speed: 1.0, deltaTime: 1.0);

        // Assert
        Assert.Equal("kitchen", result.CurrentNode);
        Assert.False(result.IsTraveling);
    }

    [Fact]
    public void ProcessMovement_Traveling_IncreasesProgress()
    {
        // Arrange
        var position = new EntityPosition("kitchen", "bar");
        var graph = new SceneGraph();
        graph.AddNode(new SceneNode("kitchen", new GridPosition(0, 0)));
        graph.AddNode(new SceneNode("bar", new GridPosition(10, 0)));

        // Act
        var result = MovementProcessor.ProcessMovement(position, graph, speed: 1.0, deltaTime: 0.1);

        // Assert
        Assert.True(result.TravelProgress > 0);
        Assert.True(result.IsTraveling);
    }

    [Fact]
    public void ProcessMovement_TravelComplete_UpdatesCurrentNode()
    {
        // Arrange
        var position = new EntityPosition("kitchen", "bar");
        var graph = new SceneGraph();
        graph.AddNode(new SceneNode("kitchen", new GridPosition(0, 0)));
        graph.AddNode(new SceneNode("bar", new GridPosition(10, 0)));

        // Use enough delta time to complete travel
        var travelTime = graph.CalculateTravelTime("kitchen", "bar");
        var deltaTime = travelTime + 1.0;

        // Act
        var result = MovementProcessor.ProcessMovement(position, graph, speed: 1.0, deltaTime: deltaTime);

        // Assert
        Assert.Equal("bar", result.CurrentNode);
        Assert.Null(result.TargetNode);
        Assert.Equal(0.0, result.TravelProgress);
    }

    [Fact]
    public void ProcessMovement_WithSpeedModifier_ProgressesFaster()
    {
        // Arrange
        var position = new EntityPosition("kitchen", "bar");
        var graph = new SceneGraph();
        graph.AddNode(new SceneNode("kitchen", new GridPosition(0, 0)));
        graph.AddNode(new SceneNode("bar", new GridPosition(10, 0)));

        // Act - Process with speed 2.0
        var result = MovementProcessor.ProcessMovement(position, graph, speed: 2.0, deltaTime: 1.0);

        // Act - Process with speed 1.0 for comparison
        var resultNormal = MovementProcessor.ProcessMovement(position, graph, speed: 1.0, deltaTime: 1.0);

        // Assert
        Assert.True(result.TravelProgress > resultNormal.TravelProgress);
    }

    [Fact]
    public void ProcessMovement_ZeroDeltaTime_NoProgress()
    {
        // Arrange
        var position = new EntityPosition("kitchen", "bar");
        var graph = new SceneGraph();
        graph.AddNode(new SceneNode("kitchen", new GridPosition(0, 0)));
        graph.AddNode(new SceneNode("bar", new GridPosition(10, 0)));

        // Act
        var result = MovementProcessor.ProcessMovement(position, graph, speed: 1.0, deltaTime: 0.0);

        // Assert
        Assert.Equal(0.0, result.TravelProgress);
    }

    [Fact]
    public void ProcessMovement_WithNullGraph_ThrowsArgumentNullException()
    {
        // Arrange
        var position = new EntityPosition("kitchen", "bar");

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() =>
            MovementProcessor.ProcessMovement(position, null!, speed: 1.0, deltaTime: 1.0));
    }

    [Fact]
    public void ProcessMovement_WithUnknownCurrentNode_ThrowsArgumentException()
    {
        // Arrange
        var position = new EntityPosition("unknown", "bar");
        var graph = new SceneGraph();
        graph.AddNode(new SceneNode("bar", new GridPosition(10, 0)));

        // Act & Assert
        Assert.Throws<ArgumentException>(() =>
            MovementProcessor.ProcessMovement(position, graph, speed: 1.0, deltaTime: 1.0));
    }

    [Fact]
    public void CalculateTravelTime_ProportionalToDistance()
    {
        // Arrange
        var graph = new SceneGraph();
        graph.AddNode(new SceneNode("node1", new GridPosition(0, 0)));
        graph.AddNode(new SceneNode("node2", new GridPosition(5, 0)));
        graph.AddNode(new SceneNode("node3", new GridPosition(10, 0)));

        // Act
        var time1 = MovementProcessor.CalculateTravelTime("node1", "node2", graph, speed: 1.0);
        var time2 = MovementProcessor.CalculateTravelTime("node2", "node3", graph, speed: 1.0);

        // Assert
        // Distance from node1 to node2 is 5, node2 to node3 is 5, so times should be equal
        Assert.Equal(time1, time2);

        // Distance from node1 to node3 is 10, so time should be double
        var time3 = MovementProcessor.CalculateTravelTime("node1", "node3", graph, speed: 1.0);
        Assert.Equal(time1 * 2, time3);
    }

    [Fact]
    public void CalculateTravelTime_InverselyProportionalToSpeed()
    {
        // Arrange
        var graph = new SceneGraph();
        graph.AddNode(new SceneNode("node1", new GridPosition(0, 0)));
        graph.AddNode(new SceneNode("node2", new GridPosition(10, 0)));

        // Act
        var timeNormal = MovementProcessor.CalculateTravelTime("node1", "node2", graph, speed: 1.0);
        var timeFast = MovementProcessor.CalculateTravelTime("node1", "node2", graph, speed: 2.0);

        // Assert
        Assert.Equal(timeNormal, timeFast * 2);
    }
}
