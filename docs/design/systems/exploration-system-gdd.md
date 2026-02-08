---
type: system
scope: detailed
status: authoritative
version: 1.0.0
created: 2026-02-08
updated: 2026-02-08
subjects: [exploration, expeditions, discovery, dungeons, mapping, travel, idle]
dependencies: [gathering-system-gdd.md, settlement-system-gdd.md]
---

# Exploration System - Authoritative Game Design

## Executive Summary

The Exploration System governs how players discover new areas, unlock resources, and progress through the world map. Active real-time exploration is replaced with expedition queues: players select a destination, assign a party, and start a timer. At timer completion, the expedition resolves with discovery results, loot, and potential hazard outcomes. Progressive area unlock creates a long-term discovery arc as expeditions complete.

**This document resolves:**

- World hierarchy and area state progression
- Expedition queue mechanics with timer-based resolution
- Progressive discovery accumulation per expedition
- Dungeon expeditions with multi-stage timers and loot tables
- Risk factors and navigation penalties during expeditions
- First discovery bonuses and exploration rewards
- Auto-mapping as areas are explored

**Design Philosophy:** Exploration is a strategic resource allocation problem. Players choose where to send expeditions, who to include in the party, and how to balance risk against reward. Higher-risk destinations offer better loot but longer timers and greater failure chance. Expedition results accumulate offline, giving returning players meaningful progress to review.

---

## 1. World Structure

### 1.1 Area Hierarchy

```
World
+-- Regions (climate zones, 5-8 per world)
    +-- Zones (distinct areas, 3-6 per region)
        +-- Locations (specific places, 5-10 per zone)
            +-- Points of Interest (discoverable, 2-5 per location)
```

### 1.2 Area Types

| Type              | Scale  | Examples                            |
| ----------------- | ------ | ----------------------------------- |
| Region            | Huge   | The Frozen North, Desert Wastes     |
| Zone              | Large  | Darkwood Forest, Iron Mountains     |
| Location          | Medium | Abandoned Mine, Ruined Tower        |
| Point of Interest | Small  | Hidden cave, ancient tree, campsite |

### 1.3 Area States

Areas progress through states as expeditions are completed:

| State      | Map Display     | Information Available         | Expedition Bonus |
| ---------- | --------------- | ----------------------------- | ---------------- |
| Unknown    | Not on map      | Nothing known                 | None             |
| Rumoured   | Marked vaguely  | Name, general direction       | None             |
| Discovered | Marked on map   | Location, basic info          | -10% timer       |
| Explored   | Detailed on map | Full layout, known contents   | -20% timer       |
| Mapped     | Complete        | All secrets, resources marked | -30% timer       |

State transitions require expedition completion:

```
Unknown -> Rumoured    (NPC information, quest hints, purchased maps)
Rumoured -> Discovered (first expedition reaches area)
Discovered -> Explored (2-3 completed expeditions)
Explored -> Mapped     (5+ expeditions with survey focus)
```

---

## 2. Expedition System

### 2.1 Expedition Queue Flow

```
SELECT DESTINATION (unlocked area or adjacent unknown)
    |
    v
ASSIGN PARTY (1-4 characters)
    |
    v
SELECT EXPEDITION TYPE (explore, gather, dungeon, survey)
    |
    v
QUEUE EXPEDITION (timer starts)
    |
    v
TIMER RUNS (real-time, continues offline)
    |
    v
TIMER COMPLETES -> OUTCOMES CALCULATED
    |
    v
COLLECT RESULTS (next check-in)
```

### 2.2 Expedition Types

| Type    | Purpose                                     | Timer Range | Primary Outcome               |
| ------- | ------------------------------------------- | ----------- | ----------------------------- |
| Explore | Discover new areas, advance state           | 30 min-2 hr | Area state progression        |
| Gather  | Collect resources from known areas          | 15 min-1 hr | Materials (see gathering GDD) |
| Dungeon | Multi-stage expedition into dangerous areas | 1-8 hr      | Combat loot, rare items       |
| Survey  | Detailed mapping of explored areas          | 1-4 hr      | Map completion, hidden finds  |

### 2.3 Expedition Timers

Base timer depends on destination distance and type:

```
Effective Timer = Base Timer x Distance Modifier x Area Difficulty x Party Modifier

Distance Modifier (from nearest settlement):
  Adjacent zone   = 1.0x
  Same region     = 1.5x
  Adjacent region = 2.0x
  Distant region  = 3.0x

Area Difficulty:
  Safe       = 0.8x
  Low        = 1.0x
  Moderate   = 1.2x
  High       = 1.5x
  Extreme    = 2.0x
  Legendary  = 3.0x

Party Modifier:
  Explorer in party     = 0.85x (15% faster)
  Ranger in party       = 0.90x (10% faster, wilderness)
  Area already Explored = 0.80x (20% faster)
  Area Mapped           = 0.70x (30% faster)
```

### 2.4 Expedition Slots

| Progression               | Available Slots |
| ------------------------- | --------------- |
| Starting                  | 1               |
| First settlement upgraded | 2               |
| Guild membership          | 3               |
| Advanced settlement       | 4               |

---

## 3. Discovery Mechanics

### 3.1 Progressive Discovery

Discovery is accumulative rather than instant. Each expedition to an area adds discovery progress:

```
Discovery Progress per Expedition:
  Base: 20-40% (depends on expedition type)
  Explorer bonus: +15% per explorer class level
  Survey expedition: +50% additional
  Curiosity trait: +discovery_bonus (see personality-traits-gdd.md)

Area reaches 100% -> advances to next state
```

### 3.2 Discovery Triggers

| Trigger              | Effect                           |
| -------------------- | -------------------------------- |
| Expedition completes | Base discovery progress added    |
| Survey expedition    | Bonus discovery + hidden reveals |
| NPC information      | Area advances to Rumoured        |
| Purchased map        | Area advances to Discovered      |
| Quest completion     | Area may advance 1-2 states      |

### 3.3 Hidden Location Discovery

Hidden locations require additional conditions beyond standard exploration:

| Concealment Type | Discovery Method                   | Expedition Requirement |
| ---------------- | ---------------------------------- | ---------------------- |
| Camouflaged      | High cumulative discovery progress | 3+ explore expeditions |
| Underground      | Mining-focused expedition          | Miner in party         |
| Magical          | Magic detection                    | Mage in party          |
| Behind obstacle  | Specialist skills                  | Climber/swimmer needed |
| Event-triggered  | Specific quest or item             | Quest prerequisite     |

---

## 4. Points of Interest

### 4.1 POI Categories

| Category | Examples                           | Typical Rewards     |
| -------- | ---------------------------------- | ------------------- |
| Resource | Ore vein, herb patch, fishing spot | Gathering materials |
| Shelter  | Cave, ruins, campsite              | Rest bonus location |
| Danger   | Monster lair, bandit camp          | Combat loot         |
| Treasure | Hidden cache, ancient tomb         | Items, currency     |
| Lore     | Monument, library, shrine          | Knowledge, skills   |
| Social   | Hermit, wanderer, hidden village   | Quests, trade       |
| Passage  | Hidden path, tunnel, portal        | Shortcuts, access   |

### 4.2 POI Persistence

| Type        | Behaviour                                 |
| ----------- | ----------------------------------------- |
| Permanent   | Always present (landmarks, settlements)   |
| Renewable   | Respawns after depletion (resource nodes) |
| One-time    | Gone after interaction (treasure, events) |
| Conditional | Appears under specific conditions         |

### 4.3 Resource Node Discovery

Resource detection accumulates with expedition time spent in an area:

| Expedition Count | Cumulative Detection        |
| ---------------- | --------------------------- |
| 1st expedition   | Common resources revealed   |
| 2nd expedition   | Uncommon resources revealed |
| 3rd expedition   | Rare resources revealed     |
| 5+ expeditions   | Hidden resources revealed   |

Class bonuses accelerate detection:

| Class in Party | Resource Type          | Detection Multiplier |
| -------------- | ---------------------- | -------------------- |
| Miner          | Ore, gems, stone       | 3x                   |
| Forager        | Herbs, berries, plants | 3x                   |
| Hunter         | Animal dens, tracks    | 3x                   |
| Explorer       | All types              | 1.5x                 |

---

## 5. Mapping System

### 5.1 Auto-Mapping

Maps auto-complete as areas are explored, removing the manual mapping mechanic:

| Area State | Map Detail                      |
| ---------- | ------------------------------- |
| Unknown    | Blank                           |
| Rumoured   | Vague marker, general direction |
| Discovered | Correct location, basic outline |
| Explored   | Full layout, known contents     |
| Mapped     | All secrets, resources marked   |

### 5.2 Map Efficiency Bonuses

Higher map states reduce future expedition timers to that area:

| Map State  | Timer Reduction | Loot Bonus |
| ---------- | --------------- | ---------- |
| Unknown    | None            | None       |
| Rumoured   | None            | None       |
| Discovered | -10%            | None       |
| Explored   | -20%            | +10%       |
| Mapped     | -30%            | +20%       |

### 5.3 Map Sources

| Source       | Effect                               | Availability   |
| ------------ | ------------------------------------ | -------------- |
| Auto-mapping | Free, progressive with exploration   | Default        |
| Purchased    | Advances specific area to Discovered | Merchant NPCs  |
| NPC-provided | Advances area to Rumoured            | Quest givers   |
| Loot drops   | Advances area to Discovered          | Treasure finds |

---

## 6. Dungeons

### 6.1 Dungeon Types

| Type         | Characteristics              | Typical Rewards         |
| ------------ | ---------------------------- | ----------------------- |
| Natural cave | Winding, natural hazards     | Minerals, creature loot |
| Mine         | Structured, resource-rich    | Ore, gems               |
| Ruins        | Ancient, traps, lore         | Artifacts, recipes      |
| Fortress     | Defended, strategic          | Military loot, faction  |
| Tomb         | Trapped, undead              | Treasure, undead loot   |
| Lair         | Monster home, organic layout | Boss loot               |
| Magical      | Reality-warping, puzzles     | Enchanting materials    |

### 6.2 Multi-Stage Dungeon Expeditions

Dungeon expeditions use multi-stage timers rather than a single timer:

```
STAGE 1: ENTRANCE (15-30 min)
    -> Outcomes: Minor loot, trap encounters
    -> Risk: Low
    |
    v
STAGE 2: SHALLOW (30-60 min)
    -> Outcomes: Moderate loot, combat encounters
    -> Risk: Moderate
    |
    v
STAGE 3: DEEP (1-2 hr)
    -> Outcomes: Good loot, rare encounters
    -> Risk: High
    |
    v
STAGE 4: BOSS (30-60 min, if applicable)
    -> Outcomes: Boss loot, unique items
    -> Risk: Very High
```

Players can choose how deep to push. Deeper stages have better loot but higher risk of party damage/failure.

### 6.3 Dungeon Difficulty Scaling

| Depth    | Enemy Level Bonus | Loot Quality   |
| -------- | ----------------- | -------------- |
| Entrance | +0                | Common         |
| Shallow  | +2-3              | Uncommon       |
| Mid      | +5-7              | Rare           |
| Deep     | +10-15            | Epic           |
| Boss     | +15-20            | Epic/Legendary |

---

## 7. Risk System

### 7.1 Expedition Risks

Expeditions carry risk proportional to area danger and party strength:

| Risk Event           | Effect                           | Mitigation                     |
| -------------------- | -------------------------------- | ------------------------------ |
| Getting lost         | Timer extended by 20-50%         | Explorer in party, mapped area |
| Ambush               | Combat encounter at disadvantage | Ranger in party, high Courage  |
| Environmental hazard | Party health damage              | Survivalist, appropriate gear  |
| Trap                 | Individual health damage         | Thief in party, high awareness |
| Resource depletion   | Expedition must abort early      | Better supplies, shorter route |

### 7.2 Risk Probability

```
Risk Event Chance = Base Risk x Danger Modifier x Party Modifier

Base Risk (per expedition):
  Explore = 15%
  Gather  = 10%
  Dungeon = 30%
  Survey  = 20%

Danger Modifier:
  Safe      = 0.5x
  Low       = 1.0x
  Moderate  = 1.5x
  High      = 2.0x
  Extreme   = 3.0x

Party Modifier:
  Explorer present    = 0.7x
  Ranger present      = 0.8x
  Area Mapped         = 0.5x
  Underlevelled party = 2.0x
```

### 7.3 Getting Lost

Getting lost adds a timer penalty rather than a separate mechanic:

| Factor           | Timer Penalty |
| ---------------- | ------------- |
| Unknown area     | +30-50%       |
| Bad weather zone | +15-25%       |
| Dense terrain    | +10-20%       |
| No Explorer      | +10%          |

Mitigation: Explorer class removes getting lost penalty entirely. Mapped areas cannot cause getting lost.

---

## 8. Exploration Rewards

### 8.1 Discovery XP Rewards

| Discovery        | Base XP | First Discovery Bonus |
| ---------------- | ------- | --------------------- |
| POI (common)     | 10      | +50%                  |
| POI (uncommon)   | 25      | +50%                  |
| Location         | 50      | +100%                 |
| Hidden location  | 100     | +100%                 |
| Zone             | 200     | +150%                 |
| Unique/legendary | 500     | +200%                 |

### 8.2 First Discovery

The first expedition to reach a new area grants bonus rewards:

- XP bonus scaled by area rarity (see table above)
- Chance for unique loot drops
- Reputation boost with exploration-focused factions

### 8.3 Expedition Loot

| Expedition Type | Common Loot               | Rare Loot                  |
| --------------- | ------------------------- | -------------------------- |
| Explore         | Maps, minor items         | Hidden location reveals    |
| Gather          | Materials (area-specific) | Rare material nodes        |
| Dungeon         | Equipment, currency       | Boss loot, enchanted items |
| Survey          | Map data, resource info   | Secret location reveals    |

---

## 9. Travel Routes

### 9.1 Route Types

| Route      | Safety       | Speed  | Discovery Chance |
| ---------- | ------------ | ------ | ---------------- |
| Main road  | High         | Fast   | Low              |
| Side road  | Moderate     | Normal | Moderate         |
| Trail      | Low-Moderate | Slower | Higher           |
| Wilderness | Low          | Slow   | Highest          |

### 9.2 Route Selection

Players choose route type when starting an expedition. Route affects both timer and risk:

| Route      | Timer Modifier | Risk Modifier | Loot Modifier |
| ---------- | -------------- | ------------- | ------------- |
| Main road  | 0.8x           | 0.5x          | 0.5x          |
| Side road  | 1.0x           | 1.0x          | 1.0x          |
| Trail      | 1.2x           | 1.5x          | 1.5x          |
| Wilderness | 1.5x           | 2.0x          | 2.0x          |

---

## 10. Idle Integration

### 10.1 Offline Behaviour

| System             | Offline Behaviour                               |
| ------------------ | ----------------------------------------------- |
| Expedition timers  | Continue running, results stored for collection |
| Discovery progress | Accumulates with completed expeditions          |
| Area state changes | Applied when expedition timer completes         |
| Dungeon stages     | Process sequentially during offline             |

### 10.2 Check-in Collection

On player check-in, the system presents:

- Completed expedition results with loot
- Area state changes from discovery progress
- Risk event outcomes (damage taken, items found)
- Dungeon stage-by-stage results
- New areas unlocked or advanced

### 10.3 Progression Hooks

| Hook               | System Integration                            |
| ------------------ | --------------------------------------------- |
| Gathering system   | Explored areas unlock gathering nodes         |
| Settlement system  | Discovered settlements enable trade routes    |
| Combat system      | Dungeon expeditions use combat resolution     |
| Economy system     | Exploration loot feeds trade                  |
| Class system       | Discovery triggers Explorer class progression |
| Personality traits | Curiosity/Courage affect expedition outcomes  |
