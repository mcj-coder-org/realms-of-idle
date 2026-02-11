using RealmsOfIdle.Core.Engine.Spatial;

namespace RealmsOfIdle.Client.Blazor.Models;

/// <summary>
/// Root aggregate representing settlement with all buildings and NPCs
/// </summary>
public sealed record Settlement(
    string Id,
    string Name,
    IReadOnlyList<Building> Buildings,
    IReadOnlyList<NPC> NPCs,
    DateTime WorldTime,
    DateTime LastWagePayment)
{
    /// <summary>
    /// Factory method - Create Millbrook settlement with initial state
    /// </summary>
    public static Settlement CreateMillbrook()
    {
        return new Settlement(
            Id: "millbrook",
            Name: "Millbrook",
            Buildings: new List<Building>
            {
                new Building(
                    "inn",
                    "The Rusty Tankard",
                    BuildingType.Inn,
                    new GridPosition(2, 2),
                    3,
                    3,
                    new Dictionary<string, int> { { "Food", 10 } },
                    120,
                    new List<string> { "mara" }),
                new Building(
                    "workshop",
                    "Tomas' Forge",
                    BuildingType.Workshop,
                    new GridPosition(6, 2),
                    2,
                    2,
                    new Dictionary<string, int> { { "IronOre", 10 } },
                    0,
                    new List<string>())
            },
            NPCs: new List<NPC>
            {
                NPC.CreateMara(),
                NPC.CreateCook(),
                NPC.CreateTomas(),
                NPC.CreateCustomer()
            },
            WorldTime: DateTime.UtcNow,
            LastWagePayment: DateTime.UtcNow
        );
    }
}
