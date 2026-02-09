# Procedural Area Generation System — Design Document

## Core Concept

Every explorable area in the game is a **directed graph of Nodes connected by Edges**. This single abstraction underpins settlements, dungeons, mines, and forests alike. The generation pipeline is:

```
Seed → Layout Algorithm → Node Placement → Edge Wiring → POI Assignment → Monster Population → Validation
```

Validation guarantees **all points of interest are reachable from the entrance** before the area is accepted.

---

## 1. Shared Data Model

### 1.1 Area Graph

```csharp
public enum AreaType { Settlement, Dungeon, Mine, Forest }
public enum ThreatLevel { Safe, Low, Medium, High, Boss }
public enum NodeType
{
    // Universal
    Entrance, Corridor, Junction, DeadEnd,
    // Settlement
    Plaza, Residential, Market, Workshop, Temple, Tavern, Gate,
    // Dungeon
    Chamber, Cavern, Lair, TreasureRoom, TrapRoom, ShrineRoom,
    // Mine
    Shaft, Vein, Cart Track, Collapse, Excavation,
    // Forest
    Clearing, Thicket, Grove, Stream, Ridge, Hollow, Ruin
}

public class AreaDefinition
{
    public string Id { get; init; } = Guid.NewGuid().ToString("N")[..12];
    public string Name { get; set; }
    public AreaType Type { get; set; }
    public int Seed { get; set; }
    public int Depth { get; set; }           // difficulty tier / distance from civilisation
    public List<AreaNode> Nodes { get; set; } = [];
    public List<AreaEdge> Edges { get; set; } = [];
    public List<MonsterNest> Nests { get; set; } = [];
    public bool IsCleared => Nests.All(n => n.IsCleared);
}

public class AreaNode
{
    public int Index { get; set; }
    public NodeType Type { get; set; }
    public string Label { get; set; }       // display name e.g. "Dusty Cavern"
    public Point GridPosition { get; set; } // logical grid position for rendering
    public ThreatLevel Threat { get; set; }
    public List<PointOfInterest> POIs { get; set; } = [];
    public bool IsEntrance { get; set; }
    public bool Discovered { get; set; }    // fog of war
    public int? NestIndex { get; set; }     // link to MonsterNest if this node hosts one
}

public class AreaEdge
{
    public int FromIndex { get; set; }
    public int ToIndex { get; set; }
    public bool Bidirectional { get; set; } = true;
    public EdgeCondition? Condition { get; set; }  // locked door, collapsed rubble, etc.
    public int TraversalCost { get; set; } = 1;    // movement time units
}

public enum EdgeCondition
{
    None,
    LockedDoor,       // requires key or lockpick
    Collapsed,        // requires mining/clearing
    Overgrown,        // requires chopping
    Submerged,        // requires swimming ability
    BossGuarded       // must defeat guardian
}
```

### 1.2 Points of Interest

```csharp
public enum POIType
{
    // Universal
    Loot, QuestGiver, SavePoint,
    // Settlement
    Shop, Inn, Blacksmith, Alchemist, GuildHall, Storage, TrainingDummy,
    // Dungeon
    TreasureChest, Trap, Puzzle, Shrine, SecretPassage, BossEncounter,
    // Mine
    OreVein, GemDeposit, AbandonedCart, UnstableWall, Underground Spring,
    // Forest
    HerbPatch, FruitTree, TimberStand, HuntingGround, WildlifeNest, AncientTree
}

public class PointOfInterest
{
    public string Id { get; init; } = Guid.NewGuid().ToString("N")[..8];
    public POIType Type { get; set; }
    public string Name { get; set; }
    public bool Exhaustible { get; set; }       // can it be depleted?
    public int? RespawnTurns { get; set; }       // null = permanent, 0 = exhausted
    public int RemainingHarvests { get; set; }   // for resource nodes
    public ResourceYield? Yield { get; set; }    // what you get from it
    public bool RequiresToolTier { get; set; }   // minimum tool quality needed
    public int ToolTierRequired { get; set; }
}

public class ResourceYield
{
    public string ResourceId { get; set; }
    public int MinQuantity { get; set; }
    public int MaxQuantity { get; set; }
    public float RareDropChance { get; set; }
    public string? RareResourceId { get; set; }
}
```

### 1.3 Monster Nests

```csharp
public class MonsterNest
{
    public int Index { get; set; }
    public string Name { get; set; }          // "Spider Den", "Goblin Camp"
    public int HostNodeIndex { get; set; }    // the node this nest occupies
    public List<int> InfluenceNodeIndices { get; set; } = []; // surrounding nodes affected
    public string MonsterPoolId { get; set; } // which monster table to roll from
    public int MonsterCount { get; set; }     // remaining monsters
    public int OriginalCount { get; set; }
    public ThreatLevel Threat { get; set; }
    public bool IsCleared => MonsterCount <= 0;
    public NestReward ClearReward { get; set; }
}

public class NestReward
{
    public int Experience { get; set; }
    public int Gold { get; set; }
    public List<string> GuaranteedLootIds { get; set; } = [];
    public string? UnlocksAreaId { get; set; }   // clearing may open a new area
}
```

---

## 2. Generation Pipeline

### 2.1 Master Generator

```csharp
public class AreaGenerator
{
    private readonly Random _rng;

    public AreaDefinition Generate(AreaType type, int depth, int? seed = null)
    {
        _rng = new Random(seed ?? Random.Shared.Next());
        var area = type switch
        {
            AreaType.Settlement => GenerateSettlement(depth),
            AreaType.Dungeon    => GenerateDungeon(depth),
            AreaType.Mine       => GenerateMine(depth),
            AreaType.Forest     => GenerateForest(depth),
            _ => throw new ArgumentException($"Unknown area type: {type}")
        };

        // Guarantee: every node reachable from entrance
        EnsureConnectivity(area);
        // Guarantee: names are unique within area
        AssignUniqueLabels(area);

        area.Seed = seed ?? _rng.Next();
        return area;
    }
}
```

### 2.2 Connectivity Guarantee

After every generation pass, run BFS/DFS from the entrance. Any unreachable nodes get a new edge added back to the nearest reachable node. This is the safety net — individual generators should _try_ to produce connected graphs, but this catch ensures it.

```
Algorithm EnsureConnectivity(area):
    reachable = BFS(area.Entrance)
    unreachable = area.Nodes \ reachable

    while unreachable is not empty:
        pick u from unreachable
        find r in reachable that minimises GridDistance(u, r)
        add Edge(r, u, bidirectional=true)
        new_reachable = BFS(u)  // may connect several unreachable nodes
        move new_reachable from unreachable to reachable
```

---

## 3. Settlement Generation

**Character:** Settlements are safe hubs. No monsters. Interconnected clusters of buildings around plazas, connected by roads/paths.

### 3.1 Layout Algorithm — Clustered Plaza Model

```
Parameters:
    size = clamp(depth + 2, 3, 8)  // number of plazas
    buildings_per_plaza = 3..6
    total_nodes ≈ size * (1 + buildings_per_plaza_avg)

Algorithm:
    1. Place `size` plaza nodes on a coarse grid with jitter
       - grid spacing = 4, jitter = ±1
       - ensures plazas don't overlap

    2. For each plaza, spawn buildings around it:
       - pick count from buildings_per_plaza range
       - place on adjacent grid cells (N/S/E/W/diag)
       - assign NodeType from weighted table based on settlement size:

         Tiny (depth 0-1):  2-3 buildings
           - Tavern (guaranteed), General Shop, Residential
         Small (depth 2-3):  4-5 buildings
           + Blacksmith, Temple
         Medium (depth 4-5): 6-8 buildings
           + Alchemist, GuildHall, Training
         Large (depth 6+):   8-12 buildings
           + Specialized shops, multiple residential

    3. Connect each building to its parent plaza (bidirectional edge)

    4. Connect plazas to each other:
       - Compute MST of plaza positions → guaranteed connectivity
       - Add 1-2 random shortcut edges for loops

    5. Place 1-2 Gate nodes on the periphery as entrances/exits
       - Connect to nearest plaza

    6. Assign POIs to buildings by type:
       - Tavern → Inn (rest/save), QuestGiver
       - Market → Shop (buy/sell)
       - Workshop → Blacksmith (craft/repair)
       - Temple → SavePoint, healing
       - GuildHall → QuestGiver, TrainingDummy
```

### 3.2 Settlement Naming

Use compound name generation:

```
Prefix:  Oak, Iron, Stone, Raven, Silver, Frost, Ember, Thorn, Copper, Ash
Suffix:  -holm, -stead, -haven, -ford, -wick, -dale, -gate, -keep, -fall, -rest
```

Building names are functional: "Greybeard's Forge", "The Gilded Mug", "Hall of the Wardens"

---

## 4. Dungeon Generation

**Character:** Monster-filled underground spaces. Branching corridors, chambers of varying size, dead ends with loot, a boss lair at the deepest point. Monster nests make sections dangerous.

### 4.1 Layout Algorithm — Tunnelling + Room Placement

Dungeons use a **room-first** approach on a grid, then tunnel corridors between them.

```
Parameters:
    grid_w, grid_h = 12 + depth*2 (capped at 30)
    room_count = 5 + depth*2 (capped at 20)
    room_size = 1..3 (in grid cells, abstracted to single nodes)
    nest_count = 1 + floor(depth / 2) (capped at 5)
    has_boss = depth >= 2

Algorithm:
    1. ROOM PLACEMENT
       - Attempt to place room_count rooms via rejection sampling:
         - Pick random position and size on grid
         - If no overlap with existing rooms (1-cell buffer), accept
         - After 100 failures, stop
       - Each room → 1 AreaNode (type = Chamber, Cavern, or special)
       - First room placed near grid edge → Entrance

    2. CORRIDOR GENERATION (connect rooms)
       - Build MST over room centres (Prim's algorithm, Euclidean weight)
       - For each MST edge, create L-shaped corridor:
         - Horizontal then vertical (or vice versa, coin flip)
         - Each corridor segment → 1 Corridor node
         - Wire: Room → Corridor → ... → Corridor → Room
       - Add loop_count = floor(room_count * 0.2) extra random edges
         between non-adjacent rooms for alternate paths

    3. DEAD ENDS
       - Pick 2-4 corridor nodes, extend a 1-3 node spur off them
       - Terminal node → DeadEnd with TreasureChest or Trap POI

    4. BOSS LAIR
       - If has_boss, pick the room furthest from entrance
       - Set type = Lair, add BossEncounter POI
       - Edge leading in gets EdgeCondition.BossGuarded
       - Place a TreasureRoom node adjacent to the lair (boss reward)

    5. SPECIAL ROOMS (assign to remaining rooms)
       - 1 ShrineRoom with SavePoint POI
       - 1-2 TrapRooms with Trap POIs
       - Rest become Chambers/Caverns with loot/empty

    6. MONSTER NEST PLACEMENT
       - Select nest_count rooms (not entrance, not shrine)
       - Create MonsterNest per selection
       - Influence radius = 1-2 edges from host node
       - All influenced nodes get elevated ThreatLevel
       - Clearing nest → influenced nodes drop to Safe
```

### 4.2 Dungeon Flavour Variants

Roll a theme per dungeon that affects naming, monster pools, and POI distribution:

| Theme         | Corridor Naming                        | Monster Pool              | Special POIs                             |
| ------------- | -------------------------------------- | ------------------------- | ---------------------------------------- |
| Natural Cave  | "Damp Tunnel", "Echoing Cavern"        | Bats, Spiders, Slimes     | Underground Spring, Mushroom Cluster     |
| Ancient Ruin  | "Crumbling Hall", "Sealed Chamber"     | Undead, Golems, Spirits   | Puzzle Door, Ancient Library, Rune Altar |
| Beast Den     | "Gnawed Passage", "Bone-strewn Hollow" | Wolves, Bears, Drakes     | Egg Clutch, Alpha Den, Cached Kill       |
| Goblin Warren | "Crude Tunnel", "Stinking Pit"         | Goblins, Hobgoblins, Rats | Stolen Goods, Crude Trap, Alarm Bell     |

---

## 5. Mine Generation

**Character:** Linear primary shaft with branching side tunnels. Narrow, directional. Resource veins are the primary draw. May have monsters that have moved in.

### 5.1 Layout Algorithm — Spine-and-Branch

Mines are more **linear** than dungeons, reflecting excavated tunnels following ore veins.

```
Parameters:
    shaft_length = 4 + depth (capped at 15)
    branch_count = 2 + floor(depth * 1.5) (capped at 8)
    branch_length = 1..4
    vein_count = 3 + depth*2
    nest_count = 0..floor(depth / 3) (many mines are monster-free at low depth)
    collapse_count = 0..2

Algorithm:
    1. PRIMARY SHAFT
       - Create shaft_length nodes in a roughly linear sequence
       - Node 0 = Entrance (mine mouth)
       - Types alternate: Shaft, CartTrack, Shaft, Junction, ...
       - Each node connects to next (bidirectional)
       - Slight grid position jitter to avoid perfectly straight line

    2. SIDE BRANCHES
       - At each Junction node (every 3rd-4th shaft node), roll for branches
       - Each branch:
         - Pick direction (left or right, perpendicular to shaft)
         - Create branch_length nodes
         - Types: Shaft → Shaft → ... → Excavation (or DeadEnd)
         - Wire: Junction → branch_node_1 → ... → terminal

    3. RESOURCE VEIN PLACEMENT
       - Distribute vein_count OreVein/GemDeposit POIs across nodes:
         - Prefer branch terminals and excavation nodes (60%)
         - Some on shaft nodes (40%)
       - Vein type determined by depth tier:

         Depth 0-2:  Copper, Tin, Coal, Clay
         Depth 3-5:  Iron, Silver, Quartz, Sulphur
         Depth 6-8:  Gold, Mithril, Ruby, Sapphire
         Depth 9+:   Adamantite, Dragonstone, Void Crystal

       - Each vein has RemainingHarvests = 3..8
       - Higher tier veins require higher ToolTierRequired

    4. COLLAPSES
       - Pick collapse_count edges along branches
       - Set EdgeCondition.Collapsed
       - Content beyond collapse is richer (bonus veins) as reward

    5. INFRASTRUCTURE POIs
       - AbandonedCart near entrance (small loot)
       - Underground Spring (rest point, minor healing)
       - UnstableWall on some dead ends (breakable → hidden vein)

    6. MONSTER NESTS (if any)
       - Place deeper in the mine (node index > shaft_length/2)
       - Typically: cave-dwelling creatures (bats, rock elementals, spiders)
       - Influence covers the branch they're on
       - Clearing makes the branch safe for idle harvesting
```

### 5.2 Mine Resource Refresh

Mines have a **partial respawn** mechanic:

```
On world tick (e.g. every N idle minutes):
    for each vein in mine:
        if vein.RemainingHarvests < vein.MaxHarvests:
            roll respawn chance (10-30% per tick depending on rarity)
            if success: vein.RemainingHarvests += 1
```

Rare veins respawn slower. Fully depleted veins have a small chance to "discover a new seam" (reset to partial).

---

## 6. Forest Generation

**Character:** Broad, open-air area. Less constrained movement. Clearings connected by trails through thickets. Harvestable flora, huntable fauna, potential monster presence.

### 6.1 Layout Algorithm — Poisson-Disc Clearings with Trail Network

Forests feel **open and organic**, so we use spatially distributed clearings connected by a relaxed graph.

```
Parameters:
    area_radius = 6 + depth*2 (logical units)
    clearing_count = 6 + depth*2 (capped at 20)
    thicket_density = 0.3..0.5 (proportion of edges that get intermediate thicket nodes)
    nest_count = 1 + floor(depth / 2) (capped at 4)
    has_ancient_tree = depth >= 4 (special landmark)

Algorithm:
    1. CLEARING PLACEMENT (Poisson disc sampling)
       - Generate clearing_count points within a circle of area_radius
       - Minimum spacing = 2.5 units (prevents clumping)
       - Each point → AreaNode (type = Clearing, Grove, Hollow, or Ridge)
       - Assign types by local characteristics:
         - Lowest y-values → Stream (near water)
         - Highest y-values → Ridge (elevated)
         - Clustered near centre → Grove
         - Isolated → Hollow

    2. TRAIL NETWORK
       - Delaunay triangulation of clearing positions
       - Keep all Delaunay edges → dense, natural-feeling connectivity
       - Remove edges that cross more than 2 others (reduce visual clutter)
       - Each remaining edge → trail between clearings

    3. THICKET INSERTION
       - For thicket_density fraction of trails:
         - Insert a Thicket node at the midpoint
         - Split edge: Clearing → Thicket → Clearing
       - Thickets may have EdgeCondition.Overgrown (needs chopping)

    4. ENTRANCE PLACEMENT
       - Pick the clearing closest to the edge of the area circle
       - Mark as Entrance, connect to world map

    5. RUINS (optional)
       - If depth >= 3, place 1 Ruin node adjacent to a random clearing
       - Contains loot, possible quest trigger, possible secret dungeon entrance

    6. RESOURCE DISTRIBUTION
       - Each clearing gets 1-3 POIs from weighted table:

         HerbPatch (30%)    - common medicinal/alchemical herbs
         FruitTree (20%)    - food resources
         TimberStand (20%)  - wood harvesting
         HuntingGround (15%)- animal pelts, meat
         WildlifeNest (10%) - rare animal products (feathers, silk)
         AncientTree (5%)   - unique, depth-gated, one per forest

       - Thicket nodes may have hidden HerbPatches (discovered on traversal)

    7. MONSTER NESTS
       - Place in Hollows or deep Groves (furthest from entrance)
       - Forest monsters: Wolves, Treants, Boars, Fae creatures, Bandits
       - Influence radius = 2-3 edges (larger than dungeon, reflecting open space)
       - Clearing nests makes the zone safe for idle harvesting
```

### 6.2 Seasonal Variation (Optional Extension)

Forests can shift resources by season:

| Season | Effect                                                       |
| ------ | ------------------------------------------------------------ |
| Spring | +50% herb respawn, new FruitTree blooms                      |
| Summer | HuntingGround has more prey, Timber at full yield            |
| Autumn | FruitTrees at peak, rare mushrooms appear                    |
| Winter | Reduced yields, but rare frost herbs, wolves more aggressive |

---

## 7. Monster Nest Mechanics

### 7.1 Nest Lifecycle

```
                    ┌─────────────┐
         discover   │   ACTIVE    │  monsters patrol influence zone
        ─────────>  │             │  characters encounter random fights
                    └──────┬──────┘
                           │  reduce MonsterCount via combat
                           ▼
                    ┌─────────────┐
                    │  WEAKENED   │  fewer patrols, reduced encounter rate
                    │ count < 50% │  visual change (fewer enemies visible)
                    └──────┬──────┘
                           │  MonsterCount reaches 0
                           ▼
                    ┌─────────────┐
                    │   CLEARED   │  zone becomes safe
                    │             │  reward granted, resources accessible
                    └──────┬──────┘
                           │  optional: after N days without visit
                           ▼
                    ┌─────────────┐
                    │  RESPAWNED  │  new monsters move in (lower count)
                    │  (optional) │  can be re-cleared
                    └─────────────┘
```

### 7.2 Influence System

When a nest is active, its influence nodes have:

- **Elevated encounter rate** — characters traversing or idling there trigger combat
- **Blocked idle harvesting** — resource POIs in influenced nodes can't be safely harvested until cleared (or characters must fight between harvests)
- **Visual indicator** — map shows danger zone overlay

When cleared:

- Encounter rate drops to zero (or ambient wildlife level for forests)
- Idle harvesting unlocked
- Possible new POIs revealed (the nest was hiding something)

### 7.3 Nest Difficulty Scaling

```csharp
public static MonsterNest CreateNest(int depth, ThreatLevel threat, string biome)
{
    var baseCount = threat switch
    {
        ThreatLevel.Low    => 3 + depth,
        ThreatLevel.Medium => 6 + depth * 2,
        ThreatLevel.High   => 10 + depth * 3,
        ThreatLevel.Boss   => 1,  // single powerful enemy
        _ => 5
    };

    return new MonsterNest
    {
        MonsterCount = baseCount,
        OriginalCount = baseCount,
        Threat = threat,
        MonsterPoolId = $"{biome}_{threat}",
        ClearReward = new NestReward
        {
            Experience = baseCount * 10 * (int)threat,
            Gold = baseCount * 5 * (int)threat,
        }
    };
}
```

---

## 8. Navigation & Pathfinding

### 8.1 Player Movement Model

Characters navigate node-to-node along edges. For an idle RPG, movement is **automatic** — the player selects a destination and characters pathfind there.

```csharp
public class AreaNavigator
{
    /// Returns ordered list of node indices from current to target, or null if unreachable.
    public List<int>? FindPath(AreaDefinition area, int fromNode, int toNode)
    {
        // Dijkstra using TraversalCost weights
        // Respects EdgeConditions: skip edges the party can't pass
        // Returns null only if genuinely disconnected (shouldn't happen post-validation)
    }

    /// Returns all nodes the party can currently reach given their abilities.
    public HashSet<int> GetReachableNodes(AreaDefinition area, int fromNode, PartyCapabilities caps)
    {
        // BFS but only traverse edges where:
        //   condition == None
        //   OR condition is satisfiable by caps (has key, has pickaxe, etc.)
    }
}

public class PartyCapabilities
{
    public bool HasKey { get; set; }
    public bool CanMine { get; set; }      // clear collapses
    public bool CanChop { get; set; }      // clear overgrowth
    public bool CanSwim { get; set; }
    public int CombatPower { get; set; }   // for boss-guarded checks
}
```

### 8.2 Discovery / Fog of War

Nodes start `Discovered = false`. When a character enters a node or an adjacent node, it becomes discovered. The player's map only shows discovered nodes.

```
On enter node N:
    N.Discovered = true
    for each neighbor M of N:
        M.Discovered = true  // can see adjacent rooms
```

### 8.3 Auto-Explore for Idle Play

Since this is an idle RPG, provide an auto-explore mode:

```
Algorithm AutoExplore(area, party):
    while undiscovered reachable nodes exist:
        target = nearest undiscovered node (by Dijkstra from party position)
        path = FindPath(current, target)
        move along path (triggers encounters, discovers nodes)
        if encounter: resolve combat
        if party health low: retreat to nearest SavePoint/Entrance
```

---

## 9. Map Rendering Approach

For the retro pixel art aesthetic, the node graph maps to a **tile-based mini-map**:

```
Node → fixed-size tile (e.g. 16x16 or 32x32 pixels)
Edge → connecting line/corridor tile between node positions
Fog  → darkened/hidden tiles for undiscovered nodes

Visual per AreaType:
    Settlement: top-down building sprites on dirt/grass tiles
    Dungeon:    stone floor tiles, dark rock walls
    Mine:       wooden supports, rail tracks, stone walls
    Forest:     grass tiles, tree canopy sprites, trail paths

POI indicators: small icon overlay on the node tile
    ⛏ ore vein    🌿 herbs    💀 monster nest
    🏪 shop       ⚔ boss      📦 treasure
```

The grid positions assigned during generation map directly to pixel positions on the mini-map canvas. Since nodes are on a logical grid with spacing, they naturally avoid overlap.

---

## 10. Generation Parameters Summary

| Parameter           | Settlement        | Dungeon                | Mine                    | Forest                   |
| ------------------- | ----------------- | ---------------------- | ----------------------- | ------------------------ |
| **Node count**      | 10-40             | 15-50                  | 10-35                   | 12-45                    |
| **Layout**          | Clustered plazas  | Rooms + corridors      | Linear spine + branches | Poisson disc clearings   |
| **Connectivity**    | High (roads)      | Medium (MST + loops)   | Low (tree-like)         | High (Delaunay)          |
| **Monster nests**   | 0                 | 1-5                    | 0-3                     | 1-4                      |
| **Boss**            | No                | Yes (depth ≥ 2)        | No                      | Optional (depth ≥ 6)     |
| **Resource POIs**   | Shops (infinite)  | Treasure (one-time)    | Veins (depletable)      | Flora/fauna (respawning) |
| **Gating**          | None              | Locked doors, boss     | Collapses               | Overgrowth               |
| **Primary purpose** | Services & quests | Combat & loot          | Resource gathering      | Harvesting & hunting     |
| **Idle activity**   | Shopping, resting | Grinding cleared zones | Auto-mining veins       | Auto-harvesting          |

---

## 11. World Integration

### 11.1 World Map Connections

Areas connect to each other via a higher-level world graph:

```
Settlement ←→ Forest ←→ Mine
                ↕
             Dungeon

Each area's Entrance/Gate nodes map to edges on the world graph.
Discovering exits in an area reveals adjacent areas on the world map.
```

### 11.2 Area Persistence

Generated areas should be **seeded and deterministic** — same seed produces same layout. Store only:

```csharp
public class AreaSaveState
{
    public int Seed { get; set; }
    public AreaType Type { get; set; }
    public int Depth { get; set; }
    // Only mutable state:
    public HashSet<int> DiscoveredNodes { get; set; }
    public Dictionary<string, int> POIRemainingHarvests { get; set; }
    public Dictionary<int, int> NestRemainingMonsters { get; set; }
    public List<string> CollectedLootIds { get; set; }
    public List<int> UnlockedEdges { get; set; }  // cleared collapses, opened doors
}
```

Regenerate the full `AreaDefinition` from seed, then apply save state deltas. This keeps save files tiny.

---

## 12. Example Generated Dungeon

```
Seed: 42, Depth: 3, Theme: Ancient Ruin

       [Entrance]
            |
       [Corridor 1]
            |
   [Corridor 2]──[Dead End: Trap]
            |
      [Chamber 1]──[Corridor 5]──[Shrine Room]
            |                        (SavePoint)
      [Junction]
       /        \
[Corridor 3]  [Corridor 4]
      |              |
[Cavern: NEST 1]  [Sealed Chamber]
  (Spider Den)     (Puzzle Door)
  influence: ±1         |
      |           [Treasure Room]
[Dead End: Chest]       |
                  [Lair: BOSS]
                   (Stone Golem)
                        |
                  [Treasury]
                   (Boss Reward)

Nests: 1 (Spider Den, 12 monsters, Medium threat)
Total Nodes: 13
Edges: 12 (all bidirectional except Lair entrance = BossGuarded)
```
