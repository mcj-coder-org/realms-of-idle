using RealmsOfIdle.Core.Engine.Spatial;
using Xunit;

namespace RealmsOfIdle.Core.Tests.Engine.Spatial;

/// <summary>
/// Unit tests for SceneArea
/// </summary>
[Trait("Category", "Unit")]
public class SceneAreaTests
{
    [Fact]
    public void Constructor_WithNameAndDimensions_CreatesArea()
    {
        // Arrange
        const string name = "Main Hall";
        const int width = 20;
        const int height = 15;

        // Act
        var area = new SceneArea(name, width, height);

        // Assert
        Assert.Equal(name, area.Name);
        Assert.Equal(width, area.Grid.Width);
        Assert.Equal(height, area.Grid.Height);
    }

    [Fact]
    public void Constructor_CreatesEmptyGrid()
    {
        // Arrange
        var area = new SceneArea("Test", 10, 10);

        // Act & Assert
        // All tiles should be Empty by default
        for (int x = 0; x < 10; x++)
        {
            for (int y = 0; y < 10; y++)
            {
                Assert.Equal(TileType.Empty, area.Grid.GetTile(x, y));
            }
        }
    }

    [Fact]
    public void Constructor_WithId_SetsIdProperty()
    {
        // Arrange
        const string id = "area_main_hall";

        // Act
        var area = new SceneArea(id, "Main Hall", 20, 15);

        // Assert
        Assert.Equal(id, area.Id);
    }

    [Fact]
    public void Constructor_WithoutId_GeneratesId()
    {
        // Act
        var area = new SceneArea("Main Hall", 20, 15);

        // Assert
        Assert.NotNull(area.Id);
        Assert.NotEmpty(area.Id);
    }

    [Fact]
    public void DoorTiles_StartsEmpty()
    {
        // Arrange
        var area = new SceneArea("Test", 10, 10);

        // Act & Assert
        Assert.Empty(area.DoorTiles);
    }

    [Fact]
    public void AddDoorTile_AddsToDoorTilesCollection()
    {
        // Arrange
        var area = new SceneArea("Test", 10, 10);
        var position = new GridPosition(5, 0);

        // Act
        area.AddDoorTile(position);

        // Assert
        Assert.Contains(position, area.DoorTiles);
    }

    [Fact]
    public void IsDoorTile_WithAddedDoor_ReturnsTrue()
    {
        // Arrange
        var area = new SceneArea("Test", 10, 10);
        var position = new GridPosition(5, 0);
        area.AddDoorTile(position);

        // Act
        var result = area.IsDoorTile(position);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void IsDoorTile_WithoutDoor_ReturnsFalse()
    {
        // Arrange
        var area = new SceneArea("Test", 10, 10);
        var position = new GridPosition(5, 5);

        // Act
        var result = area.IsDoorTile(position);

        // Assert
        Assert.False(result);
    }
}
