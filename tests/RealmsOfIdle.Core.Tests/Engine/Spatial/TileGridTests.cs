using RealmsOfIdle.Core.Engine.Spatial;
using Xunit;

namespace RealmsOfIdle.Core.Tests.Engine.Spatial;

/// <summary>
/// Unit tests for TileGrid
/// </summary>
[Trait("Category", "Unit")]
public class TileGridTests
{
    [Fact]
    public void Constructor_WithDimensions_CreatesGrid()
    {
        // Arrange
        const int width = 20;
        const int height = 15;

        // Act
        var grid = new TileGrid(width, height);

        // Assert
        Assert.Equal(width, grid.Width);
        Assert.Equal(height, grid.Height);
    }

    [Fact]
    public void Constructor_InitializesAllTilesAsEmpty()
    {
        // Arrange
        const int width = 10;
        const int height = 10;

        // Act
        var grid = new TileGrid(width, height);

        // Assert
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Assert.Equal(TileType.Empty, grid.GetTile(x, y));
            }
        }
    }

    [Fact]
    public void SetTile_SetsTileAtPosition()
    {
        // Arrange
        var grid = new TileGrid(10, 10);

        // Act
        grid.SetTile(5, 5, TileType.Floor);

        // Assert
        Assert.Equal(TileType.Floor, grid.GetTile(5, 5));
    }

    [Fact]
    public void SetTile_DoesNotAffectOtherTiles()
    {
        // Arrange
        var grid = new TileGrid(10, 10);
        grid.SetTile(3, 3, TileType.Wall);

        // Act
        grid.SetTile(5, 5, TileType.Floor);

        // Assert
        Assert.Equal(TileType.Wall, grid.GetTile(3, 3));
    }

    [Fact]
    public void GetTile_OutOfBounds_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        var grid = new TileGrid(10, 10);

        // Act & Assert
        Assert.Throws<ArgumentOutOfRangeException>(() => grid.GetTile(-1, 0));
        Assert.Throws<ArgumentOutOfRangeException>(() => grid.GetTile(0, -1));
        Assert.Throws<ArgumentOutOfRangeException>(() => grid.GetTile(10, 0));
        Assert.Throws<ArgumentOutOfRangeException>(() => grid.GetTile(0, 10));
    }

    [Fact]
    public void SetTile_OutOfBounds_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        var grid = new TileGrid(10, 10);

        // Act & Assert
        Assert.Throws<ArgumentOutOfRangeException>(() => grid.SetTile(-1, 0, TileType.Floor));
        Assert.Throws<ArgumentOutOfRangeException>(() => grid.SetTile(0, -1, TileType.Floor));
        Assert.Throws<ArgumentOutOfRangeException>(() => grid.SetTile(10, 0, TileType.Floor));
        Assert.Throws<ArgumentOutOfRangeException>(() => grid.SetTile(0, 10, TileType.Floor));
    }

    [Fact]
    public void SetTile_WithNullFacilityId_SetsFacilityIdToNull()
    {
        // Arrange
        var grid = new TileGrid(10, 10);

        // Act
        grid.SetTile(5, 5, TileType.Floor, null);

        // Assert
        Assert.Null(grid.GetFacilityId(5, 5));
    }

    [Fact]
    public void SetTile_WithFacilityId_SetsFacilityId()
    {
        // Arrange
        var grid = new TileGrid(10, 10);
        const string facilityId = "kitchen";

        // Act
        grid.SetTile(5, 5, TileType.Furniture, facilityId);

        // Assert
        Assert.Equal(facilityId, grid.GetFacilityId(5, 5));
    }

    [Fact]
    public void IsWalkable_FloorTile_ReturnsTrue()
    {
        // Arrange
        var grid = new TileGrid(10, 10);
        grid.SetTile(5, 5, TileType.Floor);

        // Act
        var result = grid.IsWalkable(5, 5);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void IsWalkable_WallTile_ReturnsFalse()
    {
        // Arrange
        var grid = new TileGrid(10, 10);
        grid.SetTile(5, 5, TileType.Wall);

        // Act
        var result = grid.IsWalkable(5, 5);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void Fill_WithTileType_FillsAllTiles()
    {
        // Arrange
        var grid = new TileGrid(10, 10);

        // Act
        grid.Fill(TileType.Floor);

        // Assert
        for (int x = 0; x < 10; x++)
        {
            for (int y = 0; y < 10; y++)
            {
                Assert.Equal(TileType.Floor, grid.GetTile(x, y));
            }
        }
    }
}
