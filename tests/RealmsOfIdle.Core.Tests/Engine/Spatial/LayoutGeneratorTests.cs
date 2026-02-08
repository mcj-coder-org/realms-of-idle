using RealmsOfIdle.Core.Engine.Spatial;
using Xunit;

namespace RealmsOfIdle.Core.Tests.Engine.Spatial;

/// <summary>
/// Unit tests for LayoutGenerator
/// </summary>
[Trait("Category", "Unit")]
public class LayoutGeneratorTests
{
    [Fact]
    public void GenerateInnLayout_WithSeed_ReturnsLayout()
    {
        // Arrange
        const int seed = 42;

        // Act
        var layout = LayoutGenerator.GenerateInnLayout(seed);

        // Assert
        Assert.NotNull(layout);
        Assert.NotEmpty(layout.Areas);
    }

    [Fact]
    public void GenerateInnLayout_SameSeed_ProducesIdenticalLayout()
    {
        // Arrange
        const int seed = 12345;

        // Act
        var layout1 = LayoutGenerator.GenerateInnLayout(seed);
        var layout2 = LayoutGenerator.GenerateInnLayout(seed);

        // Assert
        Assert.Equal(layout1.Areas.Count, layout2.Areas.Count);

        // Check each area has same dimensions
        for (int i = 0; i < layout1.Areas.Count; i++)
        {
            Assert.Equal(layout1.Areas[i].Name, layout2.Areas[i].Name);
            Assert.Equal(layout1.Areas[i].Grid.Width, layout2.Areas[i].Grid.Width);
            Assert.Equal(layout1.Areas[i].Grid.Height, layout2.Areas[i].Grid.Height);
        }
    }

    [Fact]
    public void GenerateInnLayout_DifferentSeed_ProducesDifferentLayout()
    {
        // Arrange
        const int seed1 = 111;
        const int seed2 = 222;

        // Act
        var layout1 = LayoutGenerator.GenerateInnLayout(seed1);
        var layout2 = LayoutGenerator.GenerateInnLayout(seed2);

        // Assert
        // At least one tile should be different
        bool hasDifference = false;
        for (int i = 0; i < Math.Min(layout1.Areas.Count, layout2.Areas.Count); i++)
        {
            var area1 = layout1.Areas[i];
            var area2 = layout2.Areas[i];

            if (area1.Name != area2.Name)
            {
                continue;
            }

            // Check if any tile differs
            var maxWidth = Math.Min(area1.Grid.Width, area2.Grid.Width);
            var maxHeight = Math.Min(area1.Grid.Height, area2.Grid.Height);

            for (int x = 0; x < maxWidth; x++)
            {
                for (int y = 0; y < maxHeight; y++)
                {
                    if (area1.Grid.GetTile(x, y) != area2.Grid.GetTile(x, y))
                    {
                        hasDifference = true;
                        break;
                    }
                }
                if (hasDifference)
                {
                    break;
                }
            }
            if (hasDifference)
            {
                break;
            }
        }

        Assert.True(hasDifference, "Different seeds should produce different layouts");
    }

    [Fact]
    public void GenerateInnLayout_IncludesMainHall()
    {
        // Arrange
        const int seed = 42;

        // Act
        var layout = LayoutGenerator.GenerateInnLayout(seed);

        // Assert
        var mainHall = layout.Areas.FirstOrDefault(a => a.Name.Contains("Main Hall"));
        Assert.NotNull(mainHall);
    }

    [Fact]
    public void GenerateInnLayout_IncludesStaffQuarters()
    {
        // Arrange
        const int seed = 42;

        // Act
        var layout = LayoutGenerator.GenerateInnLayout(seed);

        // Assert
        var staffQuarters = layout.Areas.FirstOrDefault(a => a.Name.Contains("Staff"));
        Assert.NotNull(staffQuarters);
    }

    [Fact]
    public void GenerateInnLayout_IncludesGuestWing()
    {
        // Arrange
        const int seed = 42;

        // Act
        var layout = LayoutGenerator.GenerateInnLayout(seed);

        // Assert
        var guestWing = layout.Areas.FirstOrDefault(a => a.Name.Contains("Guest"));
        Assert.NotNull(guestWing);
    }

    [Fact]
    public void GenerateInnLayout_HasDoorConnections()
    {
        // Arrange
        const int seed = 42;

        // Act
        var layout = LayoutGenerator.GenerateInnLayout(seed);

        // Assert
        Assert.NotEmpty(layout.DoorConnections);
    }

    [Fact]
    public void GenerateInnLayout_AllAreasArePathConnected()
    {
        // Arrange
        const int seed = 42;

        // Act
        var layout = LayoutGenerator.GenerateInnLayout(seed);

        // Assert
        // Each area should be reachable via door connections
        var reachableAreaIds = new HashSet<string>();
        var startArea = layout.Areas[0];
        reachableAreaIds.Add(startArea.Id);

        // BFS through door connections
        var queue = new Queue<string>();
        queue.Enqueue(startArea.Id);

        while (queue.Count > 0)
        {
            var currentAreaId = queue.Dequeue();

            // Find all connected areas
            foreach (var connection in layout.DoorConnections)
            {
                if (connection.Side1.AreaId == currentAreaId && !reachableAreaIds.Contains(connection.Side2.AreaId))
                {
                    reachableAreaIds.Add(connection.Side2.AreaId);
                    queue.Enqueue(connection.Side2.AreaId);
                }
                else if (connection.Side2.AreaId == currentAreaId && !reachableAreaIds.Contains(connection.Side1.AreaId))
                {
                    reachableAreaIds.Add(connection.Side1.AreaId);
                    queue.Enqueue(connection.Side1.AreaId);
                }
            }
        }

        // All areas should be reachable
        Assert.Equal(layout.Areas.Count, reachableAreaIds.Count);
    }

    [Fact]
    public void GenerateInnLayout_MainHallHasEntrance()
    {
        // Arrange
        const int seed = 42;

        // Act
        var layout = LayoutGenerator.GenerateInnLayout(seed);

        // Assert
        var mainHall = layout.Areas.First(a => a.Name.Contains("Main Hall"));

        // Check for entrance door (should be on wall edge)
        bool hasEntrance = false;
        for (int x = 0; x < mainHall.Grid.Width; x++)
        {
            if (mainHall.Grid.GetTile(x, 0) == TileType.Door ||
                mainHall.Grid.GetTile(x, mainHall.Grid.Height - 1) == TileType.Door)
            {
                hasEntrance = true;
                break;
            }
        }

        // Also check side walls
        if (!hasEntrance)
        {
            for (int y = 0; y < mainHall.Grid.Height; y++)
            {
                if (mainHall.Grid.GetTile(0, y) == TileType.Door ||
                    mainHall.Grid.GetTile(mainHall.Grid.Width - 1, y) == TileType.Door)
                {
                    hasEntrance = true;
                    break;
                }
            }
        }

        Assert.True(hasEntrance, "Main Hall should have an entrance door");
    }
}
