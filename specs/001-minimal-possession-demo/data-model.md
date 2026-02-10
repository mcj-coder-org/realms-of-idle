# Data Model: Minimal Possession Demo v1

**Feature**: Minimal Possession Demo
**Date**: 2026-02-09
**Status**: Complete

---

## Entity Overview

```
Settlement (root aggregate)
├── Buildings[] (owned entities)
│   ├── Id, Name, Type
│   ├── Position (GridPosition)
│   └── Resources (Dictionary)
├── NPCs[] (owned entities)
│   ├── Id, Name, ClassName, Level
│   ├── Position (GridPosition)
│   ├── CurrentBuilding (reference)
│   ├── State (enum), CurrentAction
│   └── Gold, Reputation, Priorities
└── WorldTime (DateTime)

NPCAction (value object, not stored in Settlement)
├── Id, Name, Description
├── DurationSeconds
├── ResourceCosts (Dictionary)
├── Rewards (Dictionary)
├── RequiredClasses[]
└── RequiredBuildings[]

ActivityLogEntry (persisted in LiteDB "activityLog" collection)
├── Id (ObjectId, auto-generated)
├── Timestamp
├── ActorId, ActorName
├── ActionName
└── Result
```

---

## Entities

### Settlement

**Purpose**: Root aggregate representing Millbrook settlement with all buildings and NPCs.

**Fields**:

- `Id` (string): Unique identifier, e.g., "millbrook"
- `Name` (string): Display name, e.g., "Millbrook"
- `Buildings` (List<Building>): All buildings in settlement
- `NPCs` (List<NPC>): All characters in settlement
- `WorldTime` (DateTime): Current simulation time

**Validation Rules**:

- `Id` must be non-empty
- `Name` must be non-empty
- `Buildings` must contain at least 1 building
- `NPCs` must contain at least 1 NPC
- `WorldTime` cannot be DateTime.MinValue

**State Management**:

- Immutable record type (use `with` expressions for updates)
- All collections are IReadOnlyList to prevent external modification

**C# Schema**:

```csharp
public sealed record Settlement(
    string Id,
    string Name,
    IReadOnlyList<Building> Buildings,
    IReadOnlyList<NPC> NPCs,
    DateTime WorldTime,
    DateTime LastWagePayment)  // NEW: Track daily wage cycle
{
    // Factory method
    public static Settlement CreateMillbrook()
    {
        return new Settlement(
            Id: "millbrook",
            Name: "Millbrook",
            Buildings: new List<Building>
            {
                new Building("inn", "The Rusty Tankard", BuildingType.Inn, new GridPosition(2, 2), 3, 3,
                    new Dictionary<string, int>(), 120, new List<string> { "mara" }),  // NEW: Gold + EmployedNPCs
                new Building("workshop", "Tomas' Forge", BuildingType.Workshop, new GridPosition(6, 2), 2, 2,
                    new Dictionary<string, int> { { "IronOre", 10 } }, 0, new List<string>())  // NEW: Gold + EmployedNPCs
            },
            NPCs: new List<NPC>
            {
                NPC.CreateMara(),
                NPC.CreateCook(),
                NPC.CreateTomas(),
                NPC.CreateCustomer()
            },
            WorldTime: DateTime.UtcNow,
            LastWagePayment: DateTime.UtcNow  // NEW: Initialize to current time
        );
    }
}
```

---

### Building

**Purpose**: Physical structure within settlement (Inn, Workshop, etc.)

**Fields**:

- `Id` (string): Unique identifier, e.g., "inn", "workshop"
- `Name` (string): Display name, e.g., "The Rusty Tankard"
- `Type` (BuildingType enum): Inn, Workshop, Residential, Market
- `Position` (GridPosition): Top-left corner on 10x10 grid
- `Width` (int): Building width in tiles
- `Height` (int): Building height in tiles
- `Resources` (IReadOnlyDictionary<string, int>): Available resources (e.g., IronOre: 10)

**Validation Rules**:

- `Id` and `Name` must be non-empty
- `Position` must be within settlement bounds (0-9, 0-9)
- `Width` and `Height` must be > 0
- Building rectangle (Position + Width/Height) must not exceed grid bounds

**Relationships**:

- Contains NPCs (via NPC.CurrentBuilding reference)

**C# Schema**:

```csharp
public sealed record Building(
    string Id,
    string Name,
    BuildingType Type,
    GridPosition Position,
    int Width,
    int Height,
    IReadOnlyDictionary<string, int> Resources,
    int Gold,                           // NEW: Building treasury
    IReadOnlyList<string> EmployedNPCs, // NEW: NPC IDs employed here
    int Level = 1,                      // NEW: Building upgrade level (1-3)
    BuildingUpgradeStatus? UpgradeInProgress = null)  // NEW: Active upgrade tracking
{
    // Upgrade status record
    public sealed record BuildingUpgradeStatus(
        int TargetLevel,
        DateTime StartTime,
        int DurationSeconds);
}

public enum BuildingType
{
    Inn,
    Workshop,
    Residential,
    Market
}

// REUSE existing spatial model from RealmsOfIdle.Core.Engine.Spatial
// using RealmsOfIdle.Core.Engine.Spatial;
```

---

### NPC

**Purpose**: Character (player-controllable or autonomous) in the settlement

**Fields**:

- `Id` (string): Unique identifier, e.g., "mara", "tomas"
- `Name` (string): Display name, e.g., "Mara", "Tomas"
- `ClassName` (string): NPC occupation, e.g., "Innkeeper", "Blacksmith", "Cook"
- `Level` (int): Character level (affects action effectiveness)
- `Position` (GridPosition): Current position on grid
- `CurrentBuilding` (string): Building ID where NPC is located
- `State` (NPCState enum): Idle, Working, Traveling, Resting
- `CurrentAction` (string?): Action name if State == Working, else null
- `ActionStartTime` (DateTime?): When current action started
- `ActionDurationSeconds` (int?): How long action takes
- `Gold` (int): NPC's wealth
- `Reputation` (int): NPC's standing (affects customer attraction for innkeepers)
- `Priorities` (IReadOnlyDictionary<string, object>): Configurable behavior (e.g., "QualityFocus": 80)
- `IsPossessed` (bool): True if player currently controls this NPC

**Validation Rules**:

- `Id` and `Name` must be non-empty
- `ClassName` must be non-empty
- `Level` must be > 0
- `Gold` must be >= 0
- If `State == Working`, `CurrentAction` must not be null
- If `State != Working`, `CurrentAction` must be null

**State Transitions**:

- Idle → Working (action started)
- Working → Idle (action completed or cancelled)
- Any State → Resting (health/stamina low - not implemented in MVP)

**C# Schema**:

```csharp
public sealed record NPC(
    string Id,
    string Name,
    string ClassName,
    int Level,
    GridPosition Position,
    string? CurrentBuilding,  // NULL if unemployed (in Town Square)
    NPCState State,
    string? CurrentAction,
    DateTime? ActionStartTime,
    int? ActionDurationSeconds,
    int Gold,  // Personal wealth (not used in MVP, for future)
    int Reputation,
    IReadOnlyDictionary<string, object> Priorities,
    bool IsPossessed,
    NPCEmploymentStatus EmploymentStatus,  // NEW
    DateTime? HireDate,                      // NEW
    IReadOnlyList<NPCTrait> Traits)         // NEW: Personality traits
{
    // Factory methods
    public static NPC CreateMara() => new NPC(
        "mara", "Mara", "Innkeeper", 5,
        new GridPosition(3, 3), "inn", NPCState.Idle,
        null, null, null, 100, 50,
        new Dictionary<string, object>(), false,
        NPCEmploymentStatus.Employed, DateTime.UtcNow,
        new List<NPCTrait> { NPCTrait.Efficient });

    public static NPC CreateCook() => new NPC(
        "cook", "Cook", "Cook", 3,
        new GridPosition(5, 5), null, NPCState.Idle,  // NULL building = available to hire
        null, null, null, 0, 0,
        new Dictionary<string, object>(), false,
        NPCEmploymentStatus.Available, null,
        new List<NPCTrait> { NPCTrait.Perfectionist });

    public static NPC CreateTomas() => new NPC(
        "tomas", "Tomas", "Blacksmith", 8,
        new GridPosition(7, 3), null, NPCState.Idle,  // NULL building = available to hire
        null, null, null, 200, 30,
        new Dictionary<string, object>(), false,
        NPCEmploymentStatus.Available, null,
        new List<NPCTrait> { NPCTrait.Stubborn });
}

public enum NPCState
{
    Idle,
    Working,
    Traveling,
    Resting
}

public enum NPCEmploymentStatus
{
    Available,   // Not hired, idle in Town Square
    Employed,    // Hired, working at building
    Unpaid       // Employed but wages not paid (grace period)
}

public sealed record NPCTrait(
    string Id,        // "efficient", "perfectionist", "stubborn"
    string Name,      // "Efficient", "Perfectionist", "Stubborn"
    string Icon,      // "⚡", "✨", "🛡️"
    string Description, // "Completes actions 20% faster"
    TraitEffect Effect  // { Type: "ActionSpeed", Modifier: 1.2 }
)
{
    // Pre-defined traits
    public static NPCTrait Efficient => new(
        "efficient", "Efficient", "⚡", "Completes actions 20% faster",
        new TraitEffect("ActionSpeed", 1.2));

    public static NPCTrait Perfectionist => new(
        "perfectionist", "Perfectionist", "✨", "Quality +10%, Speed -10%",
        new TraitEffect("ActionSpeed", 0.9));

    public static NPCTrait Stubborn => new(
        "stubborn", "Stubborn", "🛡️", "Ignores priority changes for 60 seconds",
        new TraitEffect("PriorityCooldown", 60));
}

public sealed record TraitEffect(
    string Type,      // "ActionSpeed", "Quality", "PriorityCooldown"
    double Modifier   // 1.2 (20% faster), 0.9 (10% slower), 60 (seconds)
);
```

---

### NPCAction

**Purpose**: Definition of an executable action (not stored in Settlement, used for action catalog)

**Fields**:

- `Id` (string): Unique identifier, e.g., "serve_customer", "craft_sword"
- `Name` (string): Display name, e.g., "Serve Customer", "Craft Iron Sword"
- `Description` (string): Tooltip text
- `DurationSeconds` (int): How long action takes
- `ResourceCosts` (IReadOnlyDictionary<string, int>): Materials consumed (e.g., IronOre: 2)
- `Rewards` (IReadOnlyDictionary<string, int>): Benefits granted (e.g., Gold: 20, Reputation: 5)
- `RequiredClasses` (IReadOnlyList<string>): NPCs must have one of these classes
- `RequiredBuildings` (IReadOnlyList<string>): NPC must be in one of these buildings

**Validation Rules**:

- `Id` and `Name` must be non-empty
- `DurationSeconds` must be > 0
- `RequiredClasses` and `RequiredBuildings` must not be empty

**C# Schema**:

```csharp
public sealed record NPCAction(
    string Id,
    string Name,
    string Description,
    int DurationSeconds,
    IReadOnlyDictionary<string, int> ResourceCosts,   // {"Food": 1} or {"IronOre": 2}
    IReadOnlyDictionary<string, int> ResourceProduced, // NEW: {"Food": 1} or {"IronTools": 1}
    IReadOnlyDictionary<string, int> Rewards,
    IReadOnlyList<string> RequiredClasses,
    IReadOnlyList<string> RequiredBuildings);

// Example action catalog
public static class ActionCatalog
{
    public static NPCAction ServeCustomer => new(
        "serve_customer",
        "Serve Customer",
        "Serve a waiting customer food and drink",
        DurationSeconds: 5,
        ResourceCosts: new Dictionary<string, int> { { "Food", 1 } },  // NEW: Consumes 1 Food
        ResourceProduced: new Dictionary<string, int>(),
        Rewards: new Dictionary<string, int> { { "Gold", 5 }, { "Reputation", 2 } },
        RequiredClasses: new[] { "Innkeeper" },
        RequiredBuildings: new[] { "Inn" }
    );

    public static NPCAction ProduceFood => new(
        "produce_food",
        "Prepare Food",
        "Cook prepares meals for the inn",
        DurationSeconds: 10,
        ResourceCosts: new Dictionary<string, int>(),  // No cost
        ResourceProduced: new Dictionary<string, int> { { "Food", 1 } },  // NEW: Produces 1 Food
        Rewards: new Dictionary<string, int>(),  // No gold reward for production
        RequiredClasses: new[] { "Cook" },
        RequiredBuildings: new[] { "Inn" }
    );

    public static NPCAction CraftSword => new(
        "craft_sword",
        "Craft Iron Sword",
        "Forge an iron sword from raw ore",
        DurationSeconds: 30,
        ResourceCosts: new Dictionary<string, int> { { "IronOre", 2 } },
        ResourceProduced: new Dictionary<string, int> { { "IronTools", 1 } },  // NEW: Produces 1 Tool
        Rewards: new Dictionary<string, int> { { "Gold", 20 } },
        RequiredClasses: new[] { "Blacksmith" },
        RequiredBuildings: new[] { "Workshop" }
    );
}
```

---

### ActivityLogEntry

**Purpose**: Record of NPC action for display in activity log UI

**Fields**:

- `Id` (ObjectId): Auto-generated unique identifier (LiteDB)
- `Timestamp` (DateTime): When action occurred
- `ActorId` (string): NPC ID who performed action
- `ActorName` (string): NPC name for display
- `ActionName` (string): Action performed
- `Result` (string): Human-readable outcome, e.g., "+5 gold, +2 reputation"

**Validation Rules**:

- `Timestamp` must be <= current WorldTime
- `ActorId` should reference valid NPC (soft validation, not enforced)
- `Id` auto-generated by LiteDB on insert

**Persistence** (UPDATED - resolves ambiguity):

- Stored in LiteDB "activityLog" collection (NOT transient)
- Retention: Last 100 entries (automatic FIFO cleanup on insert)
- Indexed by Timestamp (descending) for efficient recent-first queries
- No TTL expiration (manual cleanup only)
- Query: `db.GetCollection<ActivityLogEntry>("activityLog").Find(Query.All(Query.Descending), limit: 20)`

**C# Schema**:

```csharp
using LiteDB;

public sealed record ActivityLogEntry(
    ObjectId Id,           // Auto-generated by LiteDB
    DateTime Timestamp,
    string ActorId,
    string ActorName,
    string ActionName,
    string Result);

// Factory method for creating new entries (Id assigned on insert)
public static ActivityLogEntry Create(string actorId, string actorName, string actionName, string result)
{
    return new ActivityLogEntry(
        Id: ObjectId.Empty,  // LiteDB assigns on insert
        Timestamp: DateTime.UtcNow,
        ActorId: actorId,
        ActorName: actorName,
        ActionName: actionName,
        Result: result
    );
}
```

**Rationale for Persistence Change**:

- Original spec said "transient, not persisted" to simplify MVP
- LiteDB storage enables:
  - Activity log survives page refresh (better UX)
  - Offline progress summary can reference historical actions
  - Future feature: Export activity history
- Performance impact: Negligible (<5ms per insert, see PR-003)
- Storage impact: ~100 entries × ~200 bytes = 20 KB (acceptable)

---

## Entity Relationships

```
Settlement 1 ──┬── * Building
               └── * NPC

NPC * ──► 1 Building (via CurrentBuilding reference)

NPCAction ◄─ NPC (NPC.CurrentAction references Action.Id)

ActivityLogEntry ─► NPC (ActorId references NPC.Id)
```

**Ownership**: Settlement owns Buildings and NPCs (aggregates). Actions are stateless value objects. ActivityLogEntry is persisted independently in LiteDB (separate collection with auto-cleanup retention policy).

---

## Immutability Pattern

All entities are **immutable records**. Updates create new instances:

```csharp
// Update NPC gold
var updatedNPC = currentNPC with { Gold = currentNPC.Gold + 5 };

// Replace NPC in settlement
var updatedSettlement = settlement with
{
    NPCs = settlement.NPCs
        .Select(n => n.Id == updatedNPC.Id ? updatedNPC : n)
        .ToList()
};
```

**Rationale**: Immutability prevents accidental state mutations, makes testing easier, enables undo/redo patterns in future.

---

## Next Steps

**Phase 1 Deliverables**:

- ✅ data-model.md (this file)
- ⏭️ quickstart.md (developer setup guide)
- ⏭️ Update agent context (run update script)
