using RealmsOfIdle.Core.Engine.Spatial;
using Xunit;

namespace RealmsOfIdle.Core.Tests.Engine.Spatial;

/// <summary>
/// Unit tests for EntityPosition
/// </summary>
[Trait("Category", "Unit")]
public class EntityPositionTests
{
    [Fact]
    public void Constructor_WithCurrentNode_CreatesPosition()
    {
        // Arrange
        const string currentNode = "kitchen";

        // Act
        var position = new EntityPosition(currentNode);

        // Assert
        Assert.Equal(currentNode, position.CurrentNode);
        Assert.Null(position.TargetNode);
        Assert.Equal(0.0, position.TravelProgress);
    }

    [Fact]
    public void Constructor_WithTargetNode_SetsTargetAndProgress()
    {
        // Arrange
        const string currentNode = "kitchen";
        const string targetNode = "bar";

        // Act
        var position = new EntityPosition(currentNode, targetNode);

        // Assert
        Assert.Equal(currentNode, position.CurrentNode);
        Assert.Equal(targetNode, position.TargetNode);
        Assert.Equal(0.0, position.TravelProgress);
    }

    [Fact]
    public void IsTraveling_WithNoTarget_ReturnsFalse()
    {
        // Arrange
        var position = new EntityPosition("kitchen");

        // Act
        var result = position.IsTraveling;

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void IsTraveling_WithTargetNode_ReturnsTrue()
    {
        // Arrange
        var position = new EntityPosition("kitchen", "bar");

        // Act
        var result = position.IsTraveling;

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void IsTraveling_TravelComplete_ReturnsFalse()
    {
        // Arrange
        var position = new EntityPosition("kitchen", "bar");
        position = position.WithTravelProgress(1.0);

        // Act
        var result = position.IsTraveling;

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void CanPerformActions_AtCurrentNode_ReturnsTrue()
    {
        // Arrange
        var position = new EntityPosition("kitchen");

        // Act
        var result = position.CanPerformActions;

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void CanPerformActions_WhileTraveling_ReturnsFalse()
    {
        // Arrange
        var position = new EntityPosition("kitchen", "bar");

        // Act
        var result = position.CanPerformActions;

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void WithTravelProgress_SetsNewProgress()
    {
        // Arrange
        var position = new EntityPosition("kitchen", "bar");

        // Act
        var newPosition = position.WithTravelProgress(0.5);

        // Assert
        Assert.Equal(0.5, newPosition.TravelProgress);
        Assert.Same(position.CurrentNode, newPosition.CurrentNode);
    }

    [Fact]
    public void WithTravelProgress_ProgressReaches1_UpdatesCurrentNode()
    {
        // Arrange
        var position = new EntityPosition("kitchen", "bar");

        // Act
        var newPosition = position.WithTravelProgress(1.0);

        // Assert
        Assert.Equal("bar", newPosition.CurrentNode);
        Assert.Null(newPosition.TargetNode);
        Assert.Equal(0.0, newPosition.TravelProgress);
    }

    [Fact]
    public void WithTravelProgress_ProgressBelow1_DoesNotUpdateCurrentNode()
    {
        // Arrange
        var position = new EntityPosition("kitchen", "bar");

        // Act
        var newPosition = position.WithTravelProgress(0.9);

        // Assert
        Assert.Equal("kitchen", newPosition.CurrentNode);
        Assert.Equal("bar", newPosition.TargetNode);
        Assert.Equal(0.9, newPosition.TravelProgress);
    }

    [Fact]
    public void StartTravel_SetsTargetNode()
    {
        // Arrange
        var position = new EntityPosition("kitchen");

        // Act
        var newPosition = position.StartTravel("bar");

        // Assert
        Assert.Equal("bar", newPosition.TargetNode);
        Assert.Equal(0.0, newPosition.TravelProgress);
    }

    [Fact]
    public void StartTravel_WithNullTarget_RemovesTarget()
    {
        // Arrange
        var position = new EntityPosition("kitchen", "bar");

        // Act
        var newPosition = position.StartTravel(null);

        // Assert
        Assert.Null(newPosition.TargetNode);
        Assert.False(newPosition.IsTraveling);
    }
}
