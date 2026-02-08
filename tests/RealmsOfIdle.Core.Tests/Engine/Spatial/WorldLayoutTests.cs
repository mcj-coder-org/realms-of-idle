using RealmsOfIdle.Core.Engine.Spatial;
using Xunit;

namespace RealmsOfIdle.Core.Tests.Engine.Spatial;

/// <summary>
/// Unit tests for WorldLayout
/// </summary>
[Trait("Category", "Unit")]
public class WorldLayoutTests
{
    [Fact]
    public void Constructor_CreatesEmptyLayout()
    {
        // Act
        var layout = new WorldLayout();

        // Assert
        Assert.Empty(layout.Areas);
        Assert.Empty(layout.DoorConnections);
    }

    [Fact]
    public void AddArea_AddsAreaToLayout()
    {
        // Arrange
        var layout = new WorldLayout();
        var area = new SceneArea("Main Hall", 20, 15);

        // Act
        layout.AddArea(area);

        // Assert
        Assert.Single(layout.Areas);
        Assert.Contains(area, layout.Areas);
    }

    [Fact]
    public void GetArea_ById_ReturnsCorrectArea()
    {
        // Arrange
        var layout = new WorldLayout();
        var area = new SceneArea("main_hall", "Main Hall", 20, 15);
        layout.AddArea(area);

        // Act
        var result = layout.GetArea("main_hall");

        // Assert
        Assert.Same(area, result);
    }

    [Fact]
    public void GetArea_WithUnknownId_ReturnsNull()
    {
        // Arrange
        var layout = new WorldLayout();

        // Act
        var result = layout.GetArea("unknown");

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public void AddDoorConnection_AddsConnection()
    {
        // Arrange
        var layout = new WorldLayout();
        var connection = new DoorConnection(
            new DoorLocation("area1", new GridPosition(5, 0)),
            new DoorLocation("area2", new GridPosition(3, 0))
        );

        // Act
        layout.AddDoorConnection(connection);

        // Assert
        Assert.Single(layout.DoorConnections);
        Assert.Contains(connection, layout.DoorConnections);
    }

    [Fact]
    public void GetConnectedArea_ReturnsConnectedAreaId()
    {
        // Arrange
        var layout = new WorldLayout();
        var connection = new DoorConnection(
            new DoorLocation("area1", new GridPosition(5, 0)),
            new DoorLocation("area2", new GridPosition(3, 0))
        );
        layout.AddDoorConnection(connection);

        // Act
        var result = layout.GetConnectedArea("area1", new GridPosition(5, 0));

        // Assert
        Assert.Equal("area2", result);
    }

    [Fact]
    public void GetConnectedArea_WithNoConnection_ReturnsNull()
    {
        // Arrange
        var layout = new WorldLayout();

        // Act
        var result = layout.GetConnectedArea("area1", new GridPosition(5, 0));

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public void GetDoorAtPosition_ReturnsDoorPosition()
    {
        // Arrange
        var layout = new WorldLayout();
        var doorPos = new GridPosition(5, 0);
        var connection = new DoorConnection(
            new DoorLocation("area1", doorPos),
            new DoorLocation("area2", new GridPosition(3, 0))
        );
        layout.AddDoorConnection(connection);

        // Act
        var result = layout.GetDoorAtPosition("area1", doorPos);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("area2", result?.ConnectedAreaId);
    }
}
