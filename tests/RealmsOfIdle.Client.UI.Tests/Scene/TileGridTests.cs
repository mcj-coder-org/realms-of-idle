using AwesomeAssertions;
using RealmsOfIdle.Client.UI.Components;

namespace RealmsOfIdle.Client.UI.Tests.Scene;

[Trait("Category", "Unit")]
public class TileGridTests
{
    [Fact]
    public void TilePosition_ShouldAdd_Direction()
    {
        // Arrange
        var pos = new TilePosition(5, 5);

        // Act
        var result = pos + Direction.North;

        // Assert
        result.Should().Be(new TilePosition(5, 4));
    }

    [Fact]
    public void TilePosition_ShouldAdd_South()
    {
        // Arrange
        var pos = new TilePosition(5, 5);

        // Act
        var result = pos + Direction.South;

        // Assert
        result.Should().Be(new TilePosition(5, 6));
    }

    [Fact]
    public void TilePosition_ShouldAdd_East()
    {
        // Arrange
        var pos = new TilePosition(5, 5);

        // Act
        var result = pos + Direction.East;

        // Assert
        result.Should().Be(new TilePosition(6, 5));
    }

    [Fact]
    public void TilePosition_ShouldAdd_West()
    {
        // Arrange
        var pos = new TilePosition(5, 5);

        // Act
        var result = pos + Direction.West;

        // Assert
        result.Should().Be(new TilePosition(4, 5));
    }

    [Fact]
    public void TilePosition_Zero_ShouldBe_Origin()
    {
        // Act
        var zero = TilePosition.Zero;

        // Assert
        zero.Should().Be(new TilePosition(0, 0));
    }

    [Fact]
    public void Tile_FactoryMethods_ShouldCreate_CorrectTiles()
    {
        // Assert
        Tile.Floor().Type.Should().Be(TileType.Floor);
        Tile.Wall().Type.Should().Be(TileType.Wall);
        Tile.Door.Type.Should().Be(TileType.Door);
        Tile.Table.Type.Should().Be(TileType.Table);
        Tile.Chair.Type.Should().Be(TileType.Chair);
        Tile.Counter.Type.Should().Be(TileType.Counter);
        Tile.Oven.Type.Should().Be(TileType.Oven);
        Tile.Bed.Type.Should().Be(TileType.Bed);
        Tile.Fireplace.Type.Should().Be(TileType.Fireplace);
        Tile.Empty.Type.Should().Be(TileType.Empty);
    }

    [Fact]
    public void Tile_WithVariant_ShouldPreserveVariant()
    {
        // Arrange & Act
        var tile = Tile.Floor(variant: 2);

        // Assert
        tile.Type.Should().Be(TileType.Floor);
        tile.Variant.Should().Be(2);
    }

    [Fact]
    public void World_WithArea_ShouldIncludeArea()
    {
        // Arrange
        var area = new Area("area1", "Test Area", 10, 10, new TilePosition(0, 0));

        // Act
        var world = new World(100, 50).WithArea(area);

        // Assert
        world.Areas.Count.Should().Be(1);
        world.Areas.Should().Contain(area);
    }

    [Fact]
    public void World_ShouldHave_Dimensions()
    {
        // Arrange & Act
        var world = new World(100, 50);

        // Assert
        world.Width.Should().Be(100);
        world.Height.Should().Be(50);
    }
}
