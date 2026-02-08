using RealmsOfIdle.Core.Infrastructure;

namespace RealmsOfIdle.Core.Engine.Spatial;

/// <summary>
/// Generates procedural layouts for game scenarios
/// </summary>
public static class LayoutGenerator
{
    /// <summary>
    /// Generates a complete Inn layout with main hall, staff quarters, and guest wing
    /// </summary>
    /// <param name="seed">The seed for deterministic generation</param>
    /// <returns>A WorldLayout containing the Inn areas and connections</returns>
    public static WorldLayout GenerateInnLayout(int seed)
    {
        var layout = new WorldLayout();
        var rng = new DeterministicRng(seed);

        // Create the three main areas
        var mainHall = GenerateMainHall(rng.WithOffset(0));
        var staffQuarters = GenerateStaffQuarters(rng.WithOffset(1000));
        var guestWing = GenerateGuestWing(rng.WithOffset(2000));

        // Add areas to layout
        layout.AddArea(mainHall);
        layout.AddArea(staffQuarters);
        layout.AddArea(guestWing);

        // Connect areas with doors
        // Main Hall <-> Staff Quarters
        ConnectAreas(layout, mainHall, staffQuarters, rng.WithOffset(3000));

        // Main Hall <-> Guest Wing
        ConnectAreas(layout, mainHall, guestWing, rng.WithOffset(4000));

        return layout;
    }

    private static SceneArea GenerateMainHall(DeterministicRng rng)
    {
        const int width = 20;
        const int height = 15;
        var area = new SceneArea("area_main_hall", "Main Hall", width, height);

        // Fill with floor tiles
        area.Grid.Fill(TileType.Floor);

        // Add walls around perimeter
        for (int x = 0; x < width; x++)
        {
            area.Grid.SetTile(x, 0, TileType.Wall);
            area.Grid.SetTile(x, height - 1, TileType.Wall);
        }
        for (int y = 0; y < height; y++)
        {
            area.Grid.SetTile(0, y, TileType.Wall);
            area.Grid.SetTile(width - 1, y, TileType.Wall);
        }

        // Add entrance door (on north wall, slightly off-center based on seed)
        int entranceX = 5 + (rng.Next(3) * 5); // 5, 10, or 15
        area.Grid.SetTile(entranceX, 0, TileType.Door);
        area.AddDoorTile(new GridPosition(entranceX, 0));

        // Add kitchen in corner (south-west)
        AddKitchen(area, 1, height - 4, rng);

        // Add bar near kitchen
        AddBar(area, 5, height - 4, rng);

        // Add tables in open floor
        AddTables(area, 7, 5, 3, rng);

        // Add fireplace on east wall
        int fireplaceY = 3 + rng.Next(5);
        area.Grid.SetTile(width - 1, fireplaceY, TileType.Furniture, "fireplace");
        area.Grid.SetTile(width - 1, fireplaceY + 1, TileType.Furniture, "fireplace");

        return area;
    }

    private static SceneArea GenerateStaffQuarters(DeterministicRng rng)
    {
        const int width = 12;
        const int height = 10;
        var area = new SceneArea("area_staff_quarters", "Staff Quarters", width, height);

        // Fill with floor tiles
        area.Grid.Fill(TileType.Floor);

        // Add walls around perimeter
        for (int x = 0; x < width; x++)
        {
            area.Grid.SetTile(x, 0, TileType.Wall);
            area.Grid.SetTile(x, height - 1, TileType.Wall);
        }
        for (int y = 0; y < height; y++)
        {
            area.Grid.SetTile(0, y, TileType.Wall);
            area.Grid.SetTile(width - 1, y, TileType.Wall);
        }

        // Add staff beds (3 beds for 3 staff members)
        AddStaffBeds(area, 2, 2, 3, rng);

        return area;
    }

    private static SceneArea GenerateGuestWing(DeterministicRng rng)
    {
        const int width = 12;
        const int height = 10;
        var area = new SceneArea("area_guest_wing", "Guest Wing", width, height);

        // Fill with floor tiles
        area.Grid.Fill(TileType.Floor);

        // Add walls around perimeter
        for (int x = 0; x < width; x++)
        {
            area.Grid.SetTile(x, 0, TileType.Wall);
            area.Grid.SetTile(x, height - 1, TileType.Wall);
        }
        for (int y = 0; y < height; y++)
        {
            area.Grid.SetTile(0, y, TileType.Wall);
            area.Grid.SetTile(width - 1, y, TileType.Wall);
        }

        // Add guest rooms (3 rooms initially)
        AddGuestRooms(area, 2, 2, 3, rng);

        return area;
    }

    private static void ConnectAreas(WorldLayout layout, SceneArea area1, SceneArea area2, DeterministicRng rng)
    {
        // Find valid wall positions for doors
        // For simplicity, we'll put doors on the east wall of area1 and west wall of area2
        // Use the minimum height to ensure both areas can have the door at the same Y position
        int minHeight = Math.Min(area1.Grid.Height, area2.Grid.Height);
        int doorY = 2 + rng.Next(minHeight - 4);

        var pos1 = new GridPosition(area1.Grid.Width - 1, doorY);
        var pos2 = new GridPosition(0, doorY);

        // Set door tiles
        area1.Grid.SetTile(pos1.X, pos1.Y, TileType.Door);
        area2.Grid.SetTile(pos2.X, pos2.Y, TileType.Door);

        // Create connection
        var connection = new DoorConnection(
            new DoorLocation(area1.Id, pos1),
            new DoorLocation(area2.Id, pos2)
        );

        layout.AddDoorConnection(connection);
    }

    private static void AddKitchen(SceneArea area, int startX, int startY, DeterministicRng _)
    {
        // 2x3 kitchen area
        for (int x = startX; x < startX + 2; x++)
        {
            for (int y = startY; y < startY + 3; y++)
            {
                area.Grid.SetTile(x, y, TileType.Furniture, "kitchen");
            }
        }
    }

    private static void AddBar(SceneArea area, int startX, int startY, DeterministicRng _)
    {
        // 1x3 bar counter
        for (int y = startY; y < startY + 3; y++)
        {
            area.Grid.SetTile(startX, y, TileType.Furniture, "bar");
        }
    }

    private static void AddTables(SceneArea area, int startX, int startY, int count, DeterministicRng _)
    {
        for (int i = 0; i < count; i++)
        {
            int x = startX + (i % 3) * 3;
            int y = startY + (i / 3) * 3;
            area.Grid.SetTile(x, y, TileType.Furniture, $"table_{i + 1}");
        }
    }

    private static void AddStaffBeds(SceneArea area, int startX, int startY, int count, DeterministicRng _)
    {
        for (int i = 0; i < count; i++)
        {
            int x = startX + (i % 3) * 3;
            int y = startY + (i / 3) * 2;
            area.Grid.SetTile(x, y, TileType.Furniture, $"staff_bed_{i + 1}");
        }
    }

    private static void AddGuestRooms(SceneArea area, int startX, int startY, int count, DeterministicRng _)
    {
        for (int i = 0; i < count; i++)
        {
            int x = startX + (i % 3) * 3;
            int y = startY + (i / 3) * 2;
            area.Grid.SetTile(x, y, TileType.Furniture, $"guest_bed_{i + 1}");
        }
    }
}
