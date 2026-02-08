using RealmsOfIdle.Core.Engine.Spatial;
using Xunit;

namespace RealmsOfIdle.Core.Tests.Engine.Spatial;

/// <summary>
/// Unit tests for TileType
/// </summary>
[Trait("Category", "Unit")]
public class TileTypeTests
{
    [Fact]
    public void FloorTileType_HasCorrectProperties()
    {
        // Act
        var tileType = TileType.Floor;

        // Assert
        Assert.Equal("Floor", tileType.Name);
        Assert.True(tileType.IsWalkable);
        Assert.False(tileType.IsDoor);
    }

    [Fact]
    public void WallTileType_HasCorrectProperties()
    {
        // Act
        var tileType = TileType.Wall;

        // Assert
        Assert.Equal("Wall", tileType.Name);
        Assert.False(tileType.IsWalkable);
        Assert.False(tileType.IsDoor);
    }

    [Fact]
    public void DoorTileType_HasCorrectProperties()
    {
        // Act
        var tileType = TileType.Door;

        // Assert
        Assert.Equal("Door", tileType.Name);
        Assert.True(tileType.IsWalkable);
        Assert.True(tileType.IsDoor);
    }

    [Fact]
    public void FurnitureTileType_HasCorrectProperties()
    {
        // Act
        var tileType = TileType.Furniture;

        // Assert
        Assert.Equal("Furniture", tileType.Name);
        Assert.False(tileType.IsWalkable);
        Assert.False(tileType.IsDoor);
    }

    [Fact]
    public void EmptyTileType_HasCorrectProperties()
    {
        // Act
        var tileType = TileType.Empty;

        // Assert
        Assert.Equal("Empty", tileType.Name);
        Assert.False(tileType.IsWalkable);
        Assert.False(tileType.IsDoor);
    }

    [Fact]
    public void TileTypeEquality_SameType_AreEqual()
    {
        // Arrange
        var tile1 = TileType.Floor;
        var tile2 = TileType.Floor;

        // Act & Assert
        Assert.Equal(tile1, tile2);
        Assert.Equal(tile1.GetHashCode(), tile2.GetHashCode());
    }

    [Fact]
    public void TileTypeEquality_DifferentType_AreNotEqual()
    {
        // Arrange
        var tile1 = TileType.Floor;
        var tile2 = TileType.Wall;

        // Act & Assert
        Assert.NotEqual(tile1, tile2);
    }
}
