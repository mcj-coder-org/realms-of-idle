using RealmsOfIdle.Core.Engine.Spatial;

namespace RealmsOfIdle.Client.Blazor.Models;

/// <summary>
/// NPC state enumeration
/// </summary>
public enum NPCState
{
    Idle,
    Working,
    Traveling,
    Resting
}

/// <summary>
/// NPC employment status
/// </summary>
public enum NPCEmploymentStatus
{
    Available,   // Not hired, idle in Town Square
    Employed,    // Hired, working at building
    Unpaid       // Employed but wages not paid (grace period)
}

/// <summary>
/// Character (player-controllable or autonomous) in the settlement
/// </summary>
public sealed record NPC(
    string Id,
    string Name,
    string ClassName,
    int Level,
    GridPosition Position,
    string? CurrentBuilding,
    NPCState State,
    string? CurrentAction,
    DateTime? ActionStartTime,
    int? ActionDurationSeconds,
    int Gold,
    int Reputation,
    IReadOnlyDictionary<string, object> Priorities,
    bool IsPossessed,
    NPCEmploymentStatus EmploymentStatus,
    DateTime? HireDate,
    IReadOnlyList<NPCTrait> Traits)
{
    /// <summary>
    /// Factory method - Create Mara (Innkeeper, Level 5)
    /// </summary>
    public static NPC CreateMara() => new(
        "mara", "Mara", "Innkeeper", 5,
        new GridPosition(3, 3), "inn", NPCState.Idle,
        null, null, null, 100, 50,
        new Dictionary<string, object>(), false,
        NPCEmploymentStatus.Employed, DateTime.UtcNow,
        new List<NPCTrait> { NPCTrait.Efficient });

    /// <summary>
    /// Factory method - Create Cook (Level 3, Available)
    /// </summary>
    public static NPC CreateCook() => new(
        "cook", "Cook", "Cook", 3,
        new GridPosition(5, 5), null, NPCState.Idle,
        null, null, null, 0, 0,
        new Dictionary<string, object>(), false,
        NPCEmploymentStatus.Available, null,
        new List<NPCTrait> { NPCTrait.Perfectionist });

    /// <summary>
    /// Factory method - Create Tomas (Blacksmith, Level 8, Available)
    /// </summary>
    public static NPC CreateTomas() => new(
        "tomas", "Tomas", "Blacksmith", 8,
        new GridPosition(7, 3), null, NPCState.Idle,
        null, null, null, 200, 30,
        new Dictionary<string, object>(), false,
        NPCEmploymentStatus.Available, null,
        new List<NPCTrait> { NPCTrait.Stubborn });

    /// <summary>
    /// Factory method - Create Customer (Level 1)
    /// </summary>
    public static NPC CreateCustomer() => new(
        "customer", "Customer", "Customer", 1,
        new GridPosition(2, 3), "inn", NPCState.Idle,
        null, null, null, 50, 0,
        new Dictionary<string, object>(), false,
        NPCEmploymentStatus.Available, null,
        new List<NPCTrait>());
}

/// <summary>
/// NPC personality trait affecting behavior
/// </summary>
public sealed record NPCTrait(
    string Id,
    string Name,
    string Icon,
    string Description,
    TraitEffect Effect)
{
    /// <summary>
    /// Efficient trait - Completes actions 20% faster
    /// </summary>
    public static NPCTrait Efficient => new(
        "efficient", "Efficient", "⚡", "Completes actions 20% faster",
        new TraitEffect("ActionSpeed", 1.2));

    /// <summary>
    /// Perfectionist trait - Quality +10%, Speed -10%
    /// </summary>
    public static NPCTrait Perfectionist => new(
        "perfectionist", "Perfectionist", "✨", "Quality +10%, Speed -10%",
        new TraitEffect("ActionSpeed", 0.9));

    /// <summary>
    /// Stubborn trait - Ignores priority changes for 60 seconds
    /// </summary>
    public static NPCTrait Stubborn => new(
        "stubborn", "Stubborn", "🛡️", "Ignores priority changes for 60 seconds",
        new TraitEffect("PriorityCooldown", 60));
}

/// <summary>
/// Trait effect modifiers
/// </summary>
public sealed record TraitEffect(
    string Type,      // "ActionSpeed", "Quality", "PriorityCooldown"
    double Modifier); // 1.2 (20% faster), 0.9 (10% slower), 60 (seconds)
