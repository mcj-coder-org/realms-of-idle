using RealmsOfIdle.Client.Blazor.Models;
using RealmsOfIdle.Core.Engine.Spatial;

namespace RealmsOfIdle.Client.Blazor.Tests.Fixtures;

/// <summary>
/// Test fixtures for Settlement, NPC, Building, and NPCAction
/// All fixtures use deterministic IDs and return new instances each call
/// </summary>
public static class SettlementTestFixtures
{
    private static readonly DateTime FixedTime = new(2026, 1, 1, 12, 0, 0, DateTimeKind.Utc);

    public static Settlement CreateTestSettlement(
        string id = "test-settlement",
        DateTime? worldTime = null,
        IReadOnlyList<Building>? buildings = null,
        IReadOnlyList<NPC>? npcs = null)
    {
        return new Settlement(
            Id: id,
            Name: "Test Settlement",
            Buildings: buildings ?? new List<Building>
            {
                CreateTestBuilding("inn", BuildingType.Inn),
                CreateTestBuilding("workshop", BuildingType.Workshop)
            },
            NPCs: npcs ?? new List<NPC>
            {
                CreateTestNPC("mara", NPCState.Idle, className: "Innkeeper", currentBuilding: "inn"),
                CreateTestNPC("cook", NPCState.Idle, className: "Cook", currentBuilding: "inn"),
                CreateTestNPC("tomas", NPCState.Idle, className: "Blacksmith", currentBuilding: "workshop"),
                CreateTestNPC("customer", NPCState.Idle, className: "Customer", currentBuilding: "inn")
            },
            WorldTime: worldTime ?? FixedTime,
            LastWagePayment: worldTime ?? FixedTime
        );
    }

    public static NPC CreateTestNPC(
        string id = "test-npc",
        NPCState state = NPCState.Idle,
        string className = "Innkeeper",
        string? currentBuilding = "inn",
        bool isPossessed = false,
        int gold = 100,
        int reputation = 50,
        string? currentAction = null,
        DateTime? actionStartTime = null,
        int? actionDurationSeconds = null)
    {
        return new NPC(
            id, $"Test {className}", className, 5,
            new GridPosition(3, 3), currentBuilding, state,
            currentAction, actionStartTime, actionDurationSeconds,
            gold, reputation,
            new Dictionary<string, object>(), isPossessed,
            NPCEmploymentStatus.Employed, FixedTime,
            new List<NPCTrait>());
    }

    public static Building CreateTestBuilding(
        string id = "inn",
        BuildingType type = BuildingType.Inn,
        IReadOnlyDictionary<string, int>? resources = null,
        int gold = 100)
    {
        var defaultResources = type switch
        {
            BuildingType.Inn => new Dictionary<string, int> { { "Food", 10 } },
            BuildingType.Workshop => new Dictionary<string, int> { { "IronOre", 10 } },
            _ => new Dictionary<string, int>()
        };

        return new Building(
            id,
            $"Test {type}",
            type,
            new GridPosition(2, 2),
            3, 3,
            resources ?? defaultResources,
            gold,
            new List<string>());
    }

    public static NPCAction CreateTestAction(
        string id = "test_action",
        int durationSeconds = 5,
        IReadOnlyDictionary<string, int>? rewards = null,
        IReadOnlyDictionary<string, int>? resourceCosts = null)
    {
        return new NPCAction(
            id,
            "Test Action",
            "A test action",
            durationSeconds,
            resourceCosts ?? new Dictionary<string, int>(),
            new Dictionary<string, int>(),
            rewards ?? new Dictionary<string, int> { { "Gold", 5 }, { "Reputation", 2 } },
            new[] { "Innkeeper" },
            new[] { "inn" });
    }

    /// <summary>
    /// Creates a settlement with no resources in any building (for testing resource-gated behavior)
    /// </summary>
    public static Settlement CreateTestSettlementWithNoResources()
    {
        return CreateTestSettlement(
            buildings: new List<Building>
            {
                CreateTestBuilding("inn", BuildingType.Inn, resources: new Dictionary<string, int>()),
                CreateTestBuilding("workshop", BuildingType.Workshop, resources: new Dictionary<string, int>())
            });
    }

    /// <summary>
    /// Creates a settlement with a possessed NPC
    /// </summary>
    public static Settlement CreateTestSettlementWithPossessedNPC(string possessedNpcId = "mara")
    {
        var settlement = CreateTestSettlement();
        return settlement with
        {
            NPCs = settlement.NPCs
                .Select(n => n.Id == possessedNpcId ? n with { IsPossessed = true } : n)
                .ToList()
        };
    }
}
