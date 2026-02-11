namespace RealmsOfIdle.Client.Blazor.Models;

/// <summary>
/// Definition of an executable action (catalog value object)
/// </summary>
public sealed record NPCAction(
    string Id,
    string Name,
    string Description,
    int DurationSeconds,
    IReadOnlyDictionary<string, int> ResourceCosts,
    IReadOnlyDictionary<string, int> ResourceProduced,
    IReadOnlyDictionary<string, int> Rewards,
    IReadOnlyList<string> RequiredClasses,
    IReadOnlyList<string> RequiredBuildings);

/// <summary>
/// Action catalog with predefined actions
/// Reference: docs/design/content/actions/ for template structure
/// </summary>
public static class ActionCatalog
{
    /// <summary>
    /// Serve Customer - Innkeeper action at Inn
    /// Reference: docs/design/content/actions/service/serve/index.md
    /// </summary>
    public static NPCAction ServeCustomer => new(
        "serve_customer",
        "Serve Customer",
        "Serve a waiting customer food and drink",
        DurationSeconds: 5,
        ResourceCosts: new Dictionary<string, int> { { "Food", 1 } },
        ResourceProduced: new Dictionary<string, int>(),
        Rewards: new Dictionary<string, int> { { "Gold", 5 }, { "Reputation", 2 } },
        RequiredClasses: new[] { "Innkeeper" },
        RequiredBuildings: new[] { "inn" }
    );

    /// <summary>
    /// Produce Food - Cook action at Inn
    /// Reference: docs/design/content/actions/crafting/cook/index.md
    /// </summary>
    public static NPCAction ProduceFood => new(
        "produce_food",
        "Prepare Food",
        "Cook prepares meals for the inn",
        DurationSeconds: 10,
        ResourceCosts: new Dictionary<string, int>(),
        ResourceProduced: new Dictionary<string, int> { { "Food", 1 } },
        Rewards: new Dictionary<string, int>(),
        RequiredClasses: new[] { "Cook" },
        RequiredBuildings: new[] { "inn" }
    );

    /// <summary>
    /// Craft Iron Sword - Blacksmith action at Workshop
    /// Reference: docs/design/content/actions/crafting/forge/index.md
    /// </summary>
    public static NPCAction CraftSword => new(
        "craft_sword",
        "Craft Iron Sword",
        "Forge an iron sword from raw ore",
        DurationSeconds: 30,
        ResourceCosts: new Dictionary<string, int> { { "IronOre", 2 } },
        ResourceProduced: new Dictionary<string, int> { { "IronTools", 1 } },
        Rewards: new Dictionary<string, int> { { "Gold", 20 } },
        RequiredClasses: new[] { "Blacksmith" },
        RequiredBuildings: new[] { "workshop" }
    );

    /// <summary>
    /// Check Income - Idle innkeeper action
    /// </summary>
    public static NPCAction CheckIncome => new(
        "check_income",
        "Check Income",
        "Review the inn's financial records",
        DurationSeconds: 3,
        ResourceCosts: new Dictionary<string, int>(),
        ResourceProduced: new Dictionary<string, int>(),
        Rewards: new Dictionary<string, int>(),
        RequiredClasses: new[] { "Innkeeper" },
        RequiredBuildings: new[] { "inn" }
    );

    /// <summary>
    /// Manage Cook - Innkeeper action to adjust cook priorities
    /// </summary>
    public static NPCAction ManageCook => new(
        "manage_cook",
        "Manage Cook",
        "Adjust cook's priorities and workflow",
        DurationSeconds: 2,
        ResourceCosts: new Dictionary<string, int>(),
        ResourceProduced: new Dictionary<string, int>(),
        Rewards: new Dictionary<string, int>(),
        RequiredClasses: new[] { "Innkeeper" },
        RequiredBuildings: new[] { "inn" }
    );

    /// <summary>
    /// Check Materials - Blacksmith action
    /// </summary>
    public static NPCAction CheckMaterials => new(
        "check_materials",
        "Check Materials",
        "Review available crafting materials",
        DurationSeconds: 2,
        ResourceCosts: new Dictionary<string, int>(),
        ResourceProduced: new Dictionary<string, int>(),
        Rewards: new Dictionary<string, int>(),
        RequiredClasses: new[] { "Blacksmith" },
        RequiredBuildings: new[] { "workshop" }
    );

    /// <summary>
    /// Rest - Universal idle action
    /// </summary>
    public static NPCAction Rest => new(
        "rest",
        "Rest",
        "Take a short break",
        DurationSeconds: 5,
        ResourceCosts: new Dictionary<string, int>(),
        ResourceProduced: new Dictionary<string, int>(),
        Rewards: new Dictionary<string, int>(),
        RequiredClasses: new[] { "Innkeeper", "Blacksmith", "Cook", "Customer" },
        RequiredBuildings: Array.Empty<string>()
    );
}
