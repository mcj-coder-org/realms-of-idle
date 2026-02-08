using RealmsOfIdle.Core.Engine.Spatial;
using Xunit;

namespace RealmsOfIdle.Core.Tests.Engine.Spatial;

/// <summary>
/// Unit tests for DoorConnection and DoorLocation
/// </summary>
[Trait("Category", "Unit")]
public class DoorConnectionTests
{
    [Fact]
    public void DoorLocation_Constructor_SetsProperties()
    {
        // Arrange
        const string areaId = "main_hall";
        var position = new GridPosition(5, 0);

        // Act
        var location = new DoorLocation(areaId, position);

        // Assert
        Assert.Equal(areaId, location.AreaId);
        Assert.Equal(position, location.Position);
    }

    [Fact]
    public void DoorConnection_Constructor_SetsBothSides()
    {
        // Arrange
        var side1 = new DoorLocation("area1", new GridPosition(5, 0));
        var side2 = new DoorLocation("area2", new GridPosition(3, 0));

        // Act
        var connection = new DoorConnection(side1, side2);

        // Assert
        // The constructor sets ConnectedAreaId on both sides
        Assert.Equal(side1.AreaId, connection.Side1.AreaId);
        Assert.Equal(side1.Position, connection.Side1.Position);
        Assert.Equal(side2.AreaId, connection.Side1.ConnectedAreaId);

        Assert.Equal(side2.AreaId, connection.Side2.AreaId);
        Assert.Equal(side2.Position, connection.Side2.Position);
        Assert.Equal(side1.AreaId, connection.Side2.ConnectedAreaId);
    }

    [Fact]
    public void GetOtherSide_WithSide1_ReturnsSide2()
    {
        // Arrange
        var side1 = new DoorLocation("area1", new GridPosition(5, 0));
        var side2 = new DoorLocation("area2", new GridPosition(3, 0));
        var connection = new DoorConnection(side1, side2);

        // Act
        var result = connection.GetOtherSide(side1);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(side2.AreaId, result?.AreaId);
        Assert.Equal(side2.Position, result?.Position);
    }

    [Fact]
    public void GetOtherSide_WithSide2_ReturnsSide1()
    {
        // Arrange
        var side1 = new DoorLocation("area1", new GridPosition(5, 0));
        var side2 = new DoorLocation("area2", new GridPosition(3, 0));
        var connection = new DoorConnection(side1, side2);

        // Act
        var result = connection.GetOtherSide(side2);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(side1.AreaId, result?.AreaId);
        Assert.Equal(side1.Position, result?.Position);
    }

    [Fact]
    public void GetOtherSide_WithUnrelatedLocation_ReturnsNull()
    {
        // Arrange
        var side1 = new DoorLocation("area1", new GridPosition(5, 0));
        var side2 = new DoorLocation("area2", new GridPosition(3, 0));
        var unrelated = new DoorLocation("area3", new GridPosition(0, 0));
        var connection = new DoorConnection(side1, side2);

        // Act
        var result = connection.GetOtherSide(unrelated);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public void ConnectsAreas_ReturnsTrueForConnectedAreas()
    {
        // Arrange
        var connection = new DoorConnection(
            new DoorLocation("area1", new GridPosition(5, 0)),
            new DoorLocation("area2", new GridPosition(3, 0))
        );

        // Act
        var result = connection.ConnectsAreas("area1", "area2");

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void ConnectsAreas_ReturnsTrueInReverseOrder()
    {
        // Arrange
        var connection = new DoorConnection(
            new DoorLocation("area1", new GridPosition(5, 0)),
            new DoorLocation("area2", new GridPosition(3, 0))
        );

        // Act
        var result = connection.ConnectsAreas("area2", "area1");

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void ConnectsAreas_ReturnsFalseForUnrelatedArea()
    {
        // Arrange
        var connection = new DoorConnection(
            new DoorLocation("area1", new GridPosition(5, 0)),
            new DoorLocation("area2", new GridPosition(3, 0))
        );

        // Act
        var result = connection.ConnectsAreas("area1", "area3");

        // Assert
        Assert.False(result);
    }
}
