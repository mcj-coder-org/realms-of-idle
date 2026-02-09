# Procedural Map Generation System — Design Document

**Project:** Cozy Retro Pixel Art Idle RPG  
**System:** Multi-Scale Procedural World Generation  
**Version:** 0.1 Draft  
**Date:** February 2026

---

## 1. Overview

This document describes a three-tier procedural map generation system for a cozy idle RPG built in MAUI/Blazor. The system generates coherent game worlds across three zoom levels — World, Region, and Settlement — using deterministic seed derivation so that every location is infinitely reproducible without persistent storage.

The prototype was validated as a React/Canvas interactive mockup. This document captures the algorithms, data structures, and architectural decisions needed to port the system into the production game.

---

## 2. Design Goals

**Deterministic reproducibility.** A single world seed produces the same world, regions, and settlements every time. Child maps are never stored — they are regenerated on demand from derived seeds.

**Coherent cross-scale continuity.** A forest on the world map should produce forested regions when zoomed in. A coastal town should sit on a beach with docks. Parent biome context flows downward through the zoom hierarchy.

**Cozy aesthetic.** Maps should feel hand-crafted and inviting. Settlements have organic layouts with winding roads, riverside bridges, and walls with gates — not sterile grid cities.

**Idle RPG integration.** Every generated structure has gameplay meaning. Mines produce ore. Farms produce food. Taverns restore health. The map _is_ the game board.

---

## 3. Architecture

### 3.1 Zoom Hierarchy

```
WORLD MAP (96×72 tiles)
│   Continents, oceans, biome regions
│   POIs: Capitals, Cities, Fortresses, Ports, Dungeons
│   Connections: Trade routes between major settlements
│
├── REGION MAP (80×60 tiles)  ← derived seed from world tile coords
│   Detailed terrain within a world tile's area
│   POIs: Towns, Villages, Mines, Caves, Ruins, Shrines, Farms, Camps, Towers, Docks
│   Connections: Roads between settlements
│
└── SETTLEMENT MAP (48×40 tiles)  ← derived seed from region POI
    Interior layout of a town or village
    Ground: Roads, dirt, grass, stone, water, bridges
    Buildings: Houses, shops, taverns, smithies, temples, barracks, markets, wells, gardens
    Structures: Walls, gates
```

### 3.2 Seed Derivation

The critical mechanism enabling on-demand generation without storage:

```
worldSeed           = user-provided or random integer
regionSeed          = worldSeed * 31 + tileX * 997 + tileY * 1301
settlementSeed      = regionPOI.seed  (= regionSeed * 31 + poiX * 997 + poiY * 1301)
```

Constants (31, 997, 1301) are chosen as primes to minimize hash collisions across the coordinate space. Any deterministic hash function works — the requirement is that `f(seed, x, y)` always returns the same value.

### 3.3 PRNG

All randomness flows through a seeded Mulberry32 PRNG:

```
function mulberry32(seed):
    return function():
        seed = (seed + 0x6D2B79F5) | 0
        t = imul(seed ^ (seed >>> 15), 1 | seed)
        t = (t + imul(t ^ (t >>> 7), 61 | t)) ^ t
        return ((t ^ (t >>> 14)) >>> 0) / 4294967296
```

This is a 32-bit generator with a period of 2³² and good distribution. For C#/.NET, `System.Random` with a fixed seed provides equivalent determinism, or use a direct port of Mulberry32 for cross-platform parity with the prototype.

---

## 4. Terrain Generation

### 4.1 Noise Pipeline

Terrain uses two independent noise channels combined into a biome lookup:

```
Elevation channel:  FBM(perlinNoise(seed), 6 octaves, lacunarity=2, gain=0.5)
Moisture channel:   FBM(perlinNoise(seed+1000), 4 octaves, lacunarity=2, gain=0.5)
```

Both channels are normalized to `[0, 1]`.

**FBM (Fractal Brownian Motion)** layers multiple octaves of Perlin noise at increasing frequency and decreasing amplitude. Six octaves for elevation provides continent-scale shapes with fine detail. Four octaves for moisture is sufficient since moisture patterns are broader.

**Island Masking (World Map only):** A radial falloff pushes edges toward water to create natural coastlines:

```
dx = (x / width - 0.5) * 2
dy = (y / height - 0.5) * 2
distance = sqrt(dx² + dy²)
elevation *= (1 - 0.6 * distance^2.2)
```

The exponent 2.2 creates a gentle coastal shelf rather than an abrupt circle. Region maps skip this mask to allow terrain that extends to edges.

### 4.2 Biome Classification

Biomes are determined by a threshold lookup on the elevation × moisture grid:

| Elevation | Moisture  | Biome        |
| --------- | --------- | ------------ |
| < 0.28    | any       | Deep Water   |
| 0.28–0.35 | any       | Water        |
| 0.35–0.38 | any       | Beach / Sand |
| 0.38–0.50 | < 0.30    | Desert       |
| 0.38–0.50 | 0.30–0.50 | Plains       |
| 0.38–0.50 | 0.50–0.65 | Meadow       |
| 0.38–0.50 | > 0.65    | Swamp        |
| 0.50–0.65 | < 0.35    | Hills        |
| 0.50–0.65 | 0.35–0.60 | Forest       |
| 0.50–0.65 | > 0.60    | Dense Forest |
| 0.65–0.78 | any       | Mountain     |
| > 0.78    | any       | Snow Peak    |

This produces 12 biomes. The thresholds are tuned so that a typical world seed yields roughly 25–35% water, 30–40% lowlands (plains/meadow/desert/swamp), 20–30% midlands (forest/hills), and 10–15% highlands (mountain/snow).

### 4.3 Scale Parameter

The `scale` parameter controls noise sampling frequency. Lower values produce jagged, detailed terrain; higher values produce smooth, continental shapes.

| Zoom Level | Scale | Effect                                             |
| ---------- | ----- | -------------------------------------------------- |
| World      | 28    | Large continents, broad biome regions              |
| Region     | 22    | More terrain variety per tile, local hills/valleys |
| Settlement | 12    | Fine detail (not used for biome terrain)           |

---

## 5. Point of Interest Placement

### 5.1 Algorithm

POI placement uses rejection sampling with biome affinity and minimum-distance constraints:

```
for each POI type with target count:
    for up to 400 attempts:
        pick random (x, y) from PRNG
        if biome at (x,y) matches type's allowed biomes
           AND (if nearWater required) water exists within radius
           AND no existing POI within minimum distance:
            place POI at (x, y)
            break
```

### 5.2 World-Level POIs

| Type     | Count | Allowed Biomes                | Constraints      | Min Distance |
| -------- | ----- | ----------------------------- | ---------------- | ------------ |
| Capital  | 1     | Plains, Meadow                | Near water (r=5) | 14           |
| City     | 4     | Plains, Meadow, Forest        | —                | 14           |
| Fortress | 3     | Hills, Mountain               | —                | 7            |
| Port     | 3     | Sand, Plains                  | Near water (r=4) | 7            |
| Dungeon  | 3     | Mountain, Hills, Dense Forest | —                | 7            |

### 5.3 Region-Level POIs

| Type    | Count | Allowed Biomes                 | Constraints                     | Min Distance |
| ------- | ----- | ------------------------------ | ------------------------------- | ------------ |
| Town    | 2     | Plains, Meadow                 | —                               | 10           |
| Village | 5     | Plains, Meadow, Forest         | —                               | 7            |
| Mine    | 3     | Mountain, Hills                | —                               | 7            |
| Cave    | 2     | Mountain, Hills                | —                               | 7            |
| Ruins   | 2     | Any land (not water/snow)      | —                               | 7            |
| Shrine  | 2     | Forest, Dense Forest, Mountain | —                               | 7            |
| Farm    | 4     | Plains, Meadow                 | Within 10 tiles of Town/Village | 5            |
| Camp    | 2     | Forest, Dense Forest, Desert   | —                               | 7            |
| Tower   | 2     | Hills, Mountain                | —                               | 7            |
| Dock    | 2     | Sand, Plains                   | Near water (r=2)                | 7            |

### 5.4 Path Generation

Trade routes connect settlements using nearest-neighbor linking. Each settlement connects to its 1–2 closest peers. A bresenham-style line walk generates the path points. Duplicate paths (A→B and B→A) are filtered by a canonical key.

Future improvement: A\* pathfinding with terrain cost weights (water = impassable, mountain = high cost, road = low cost) would produce more natural routes that follow valleys and avoid water.

---

## 6. Settlement Generation

Settlement maps represent the interior of a town or village at high zoom. The generation pipeline runs in five phases.

### 6.1 Ground Layer

The base ground type inherits from the parent biome (desert parents → dirt base, everything else → grass). A secondary noise pass adds stone patches for variety.

### 6.2 River (Optional)

50% of settlements have a river. The river runs vertically with slight horizontal wander per row, 3 tiles wide. A 2-tile bridge crosses the river at a random point in the middle third of the map.

### 6.3 Road Network

A main crossroads is placed near the map center (with random jitter). The horizontal and vertical roads are 2 tiles wide. Three additional side streets connect partial spans at random Y positions. Roads avoid overwriting water tiles.

### 6.4 Building Placement

Buildings are placed using a constraint solver:

```
canPlace(x, y, w, h):
    - Building fits within map bounds (1-tile margin)
    - No overlap with water tiles
    - No overlap with existing buildings (including 1-tile buffer)

nearRoad(x, y, w, h):
    - At least one adjacent tile (including diagonals) is a road
```

Placement order matters — important buildings get priority for good locations:

1. **Central buildings** (Guild Hall, Temple, Market) — placed within ±4 tiles of crossroads, must be road-adjacent. Size: 3–4 × 3–4 tiles.
2. **Commercial buildings** (Tavern, Smithy, Shops) — anywhere road-adjacent. Size: 2–3 × 2–3 tiles.
3. **Houses** (×18) — anywhere that fits. Size: 2–3 × 2 tiles.
4. **Amenities** (Well, Gardens) — fill gaps. Size: 1×1.

### 6.5 Walls and Gates

70% of settlements have perimeter walls. Walls are placed along the map edges with 15% random gaps for visual interest. Wall tiles that align with road positions on the map edge are replaced with gate tiles.

### 6.6 Building Types and Game Function

| Building   | Size      | Game Role                         |
| ---------- | --------- | --------------------------------- |
| Guild Hall | 3–4 × 3–4 | Quest hub, faction reputation     |
| Temple     | 3–4 × 3–4 | Healing, blessings, revive        |
| Market     | 3–4 × 3–4 | Buy/sell items, trade goods       |
| Tavern     | 2–3 × 2–3 | Rest (HP recovery), rumors/quests |
| Smithy     | 2–3 × 2–3 | Craft/upgrade equipment           |
| Shop       | 2–3 × 2–3 | Specialized item vendors          |
| Barracks   | 2–3 × 2–3 | Recruit party members             |
| House      | 2–3 × 2   | NPC homes, minor interactions     |
| Well       | 1 × 1     | Minor healing, gathering point    |
| Garden     | 1 × 1     | Herb gathering, ambiance          |

---

## 7. Rendering

### 7.1 Canvas Approach (Prototype)

The prototype renders directly to an HTML Canvas at a configurable tile size (4–10px per tile). Each tile is filled with its biome/ground color, then per-biome pixel effects are applied:

- **Forest/Dense Forest:** Darker canopy pixels on every 4th tile
- **Water/Deep Water:** White shimmer line on every 5th tile
- **Snow:** Random sparkle pixels
- **Sand/Desert:** Grain texture dots
- **Mountain:** Dark crevasse lines

POIs render as colored circles with monospace emoji labels. Paths render as dashed lines.

### 7.2 Production Rendering (MAUI/Blazor)

Two viable approaches for the production game:

**Option A: SkiaSharp (SKCanvasView).** Direct port of the canvas rendering logic. `SKCanvas.DrawRect` replaces `ctx.fillRect`. Best for pixel-perfect control and the retro aesthetic. Use `SKBitmap` for pre-rendered tile textures if performance is a concern.

**Option B: Blazor CSS Grid.** Render each tile as a `<div>` with background color. Works well for small maps (settlement scale) but may struggle with 96×72 world maps (6,912 elements). Consider virtualization or chunked rendering.

**Recommended:** SkiaSharp for world/region maps (large tile counts, pixel effects), Blazor components for settlement maps (smaller tile count, interactive building UI).

### 7.3 Tile Size by Zoom Level

| Zoom Level | Tile Size | Canvas Dimensions | Notes                                 |
| ---------- | --------- | ----------------- | ------------------------------------- |
| World      | 7px       | 672 × 504         | Fits most screens without scrolling   |
| Region     | 8px       | 640 × 480         | Slightly larger tiles for more detail |
| Settlement | 12px      | 576 × 480         | Large enough to read building labels  |

---

## 8. Name Generation

Procedural names use a combinatorial word-part system seeded per location:

**World names:** `"The" + Adjective + Noun`  
Example: _The Whispering Vale_, _The Crimson Expanse_

**Region names:** `Adjective + Noun`  
Example: _Emerald Reach_, _Frostborn Peaks_

**Settlement names:** `Prefix + Middle + (optional Suffix)`  
Example: _Ironwick_, _Stormholmbury_, _Oakdale_

Word pools:

- **Prefixes (20):** Elm, Oak, Iron, Storm, Moon, Star, Silver, Gold, Green, Dark, White, Raven, Wolf, Frost, Dawn, Dusk, Amber, Thorn, Shadow, Copper
- **Middles (20):** wood, vale, ford, wick, holm, dale, mere, stead, bridge, haven, watch, crest, fell, brook, moor, gate, stone, field, marsh, cliff
- **Suffixes (10):** ton, bury, ham, shire, land, keep, hold, spire, wall, rest

This gives ~8,000 possible settlement names (20 × 20 × 20 with 50% suffix probability). Sufficient for typical world sizes. Can be extended by adding race-specific word pools (Elvish, Dwarven, etc.) keyed to the local biome.

---

## 9. Idle RPG Integration

### 9.1 Resource Production

Each POI type maps to idle resource generation:

| POI / Building | Resource            | Base Rate              |
| -------------- | ------------------- | ---------------------- |
| Mine           | Ore, Gems           | 1/min per mine level   |
| Farm           | Food, Fiber         | 2/min per farm level   |
| Forest (biome) | Wood                | 0.5/min per lumberjack |
| Dock           | Fish, Trade Goods   | 1.5/min                |
| Smithy         | Equipment (crafted) | Per recipe             |
| Market         | Gold (trade)        | Based on supply/demand |

### 9.2 Character Movement

Idle characters auto-traverse paths between settlements. Travel time is computed from path length and terrain difficulty modifiers:

| Terrain         | Movement Cost              |
| --------------- | -------------------------- |
| Road            | 1× (base)                  |
| Plains / Meadow | 1.5×                       |
| Forest          | 2×                         |
| Hills           | 2.5×                       |
| Mountain        | 4×                         |
| Swamp           | 3×                         |
| Desert          | 2×                         |
| Water           | Impassable (requires dock) |

### 9.3 Settlement Growth

Settlements can grow through player investment, adding buildings in phases:

1. **Hamlet** (initial): 4–6 houses, 1 well, no walls
2. **Village**: +tavern, +shop, +farm, partial walls
3. **Town**: +smithy, +market, +temple, full walls with gates
4. **City**: +guild hall, +barracks, expanded housing, stone roads

Each growth phase re-runs the settlement generator with an increased building count parameter, preserving existing structure by seeding consistently.

---

## 10. Data Structures

### 10.1 Core Types (C# Translation)

```csharp
record Biome(string Id, string Name, string HexColor);

record POIType(string Name, string Emoji, string HexColor,
               string[] AllowedBiomeIds, bool RequiresNearWater);

record POI(int X, int Y, POIType Type, string Key, int DerivedSeed);

record PathSegment(POI From, POI To, List<(int X, int Y)> Points);

record MapData(
    Biome[,] Tiles,
    float[,] Elevations,
    float[,] Moistures,
    List<POI> POIs,
    List<PathSegment> Paths,
    int Width, int Height
);

record Settlement(
    GroundType[,] Ground,
    List<Building> Buildings,
    int Width, int Height
);

record Building(int X, int Y, int Width, int Height, BuildingType Type);
```

### 10.2 Noise Implementation

For .NET, consider **FastNoiseLite** (MIT license, single-file C# port available) which provides Perlin, Simplex, and FBM out of the box with seed support. Alternatively, port the Mulberry32 + Perlin implementation directly from the prototype for guaranteed cross-platform parity.

---

## 11. Performance Considerations

**World generation** (96×72 = 6,912 tiles, 6 FBM octaves each) takes ~2–5ms in JavaScript. C# with `float` math should be comparable or faster.

**POI placement** (rejection sampling, up to 400 attempts × ~15 POI types) takes < 1ms. The min-distance check is O(n²) on placed POIs but n < 50 so this is negligible.

**Settlement generation** (~600 building placement attempts) takes < 1ms.

**Rendering** is the bottleneck. SkiaSharp `DrawRect` calls for 6,912 tiles should stay under 16ms for 60fps. Caching the terrain to an `SKBitmap` and only re-rendering the POI/path overlay on interaction changes is the main optimization.

For very large maps or zooming animations, consider generating terrain in background threads (`Task.Run`) and presenting a loading indicator.

---

## 12. Future Extensions

**Dungeon/cave interiors.** A fourth zoom level for mines and caves using BSP (Binary Space Partitioning) or drunkard's walk algorithms to generate connected rooms and corridors.

**Biome transitions.** Blend biome colors at boundaries using a weighted average of neighboring tiles for smoother visual transitions.

**Seasonal variation.** Modulate the moisture channel and color palette by a time-of-year parameter. Snow expands downhill in winter; deserts bloom in spring.

**Fog of war.** Track which tiles the player has explored. Unexplored tiles render as parchment-colored with a hand-drawn map aesthetic.

**Multi-biome settlements.** Settlements near biome boundaries could reflect both — a forest-edge village with logging camps on one side and farmland on the other.

**NPC generation.** Each building could spawn NPCs with procedural names, roles tied to the building type, and simple dialogue trees seeded from the building's coordinates.

---

## 13. Appendix: Prototype File Reference

| File                | Purpose                                                   |
| ------------------- | --------------------------------------------------------- |
| `map-generator.jsx` | v1 — single-level world map with POIs and paths           |
| `map-zoom.jsx`      | v2 — three-tier zoom system (world → region → settlement) |

Both files are self-contained React components with no external dependencies beyond React itself. All noise, PRNG, generation, and rendering logic is inline.
