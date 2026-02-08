---
title: Environment & Hazards
type: reference
scope: detailed
status: authoritative
version: 1.0.0
created: 2026-02-08
updated: 2026-02-08
subjects: [environment, weather, terrain, hazards, climate, seasons]
dependencies: []
---

# Environment & Hazards

## 1. Overview

The environment is an active modifier layer that affects all idle systems -- gathering, travel, combat, and resource output. Climate, terrain, weather, and seasonal cycles apply multipliers to timer durations, output quantities, and hazard risk. All environmental state is batch-calculated at check-in rather than continuously simulated.

| Aspect  | Idle Mechanism                                      |
| ------- | --------------------------------------------------- |
| Climate | Fixed per zone; base modifiers to all activities    |
| Weather | Rolled per zone at check-in; modifies current batch |
| Terrain | Fixed per tile; affects travel timers and combat    |
| Hazards | Triggered by climate + weather; damage or delays    |
| Seasons | 4-season cycle; shifts resource tables and hazards  |

---

## 2. Climate Zones

Each map zone has a fixed climate that sets baseline modifiers for all activities within it. Climate does not change -- it is a property of the zone.

| Climate   | Gathering Modifier | Travel Modifier | Combat Modifier | Hazard Profile         |
| --------- | ------------------ | --------------- | --------------- | ---------------------- |
| Temperate | +0% (baseline)     | +0% (baseline)  | +0% (baseline)  | Low                    |
| Tropical  | +10% output        | +10% slower     | -5% accuracy    | Disease, flash floods  |
| Arctic    | -20% output        | +25% slower     | -10% all stats  | Frostbite, blizzards   |
| Desert    | -15% output        | +20% slower     | -5% stamina     | Dehydration, sandstorm |
| Swamp     | -10% output        | +30% slower     | -10% movement   | Disease, difficult     |
| Mountain  | -10% output        | +25% slower     | +5% defence     | Avalanche, altitude    |
| Coastal   | +5% output         | +5% slower      | +0%             | Storm surge, wind      |
| Volcanic  | -25% output        | +30% slower     | -15% all stats  | Toxic air, lava flows  |

### Climate Resource Availability

| Climate   | Water    | Harvestable Flora | Ore/Stone | Wildlife |
| --------- | -------- | ----------------- | --------- | -------- |
| Temperate | Common   | Common            | Common    | Common   |
| Tropical  | Common   | Abundant          | Uncommon  | Abundant |
| Arctic    | Scarce   | Rare              | Common    | Scarce   |
| Desert    | Rare     | Scarce            | Uncommon  | Scarce   |
| Swamp     | Impure   | Common            | Rare      | Common   |
| Mountain  | Moderate | Scarce            | Abundant  | Uncommon |
| Coastal   | Salt     | Moderate          | Uncommon  | Moderate |
| Volcanic  | Rare     | Rare              | Rare ores | Rare     |

---

## 3. Weather System

Weather is not continuously simulated. At each check-in, the system rolls a weather state per zone using climate-weighted probability tables and applies it as a modifier to the current batch calculation.

### Weather State Probability by Climate

| Climate   | Clear | Overcast | Rain | Storm | Extreme |
| --------- | ----- | -------- | ---- | ----- | ------- |
| Temperate | 40%   | 30%      | 20%  | 8%    | 2%      |
| Tropical  | 25%   | 25%      | 35%  | 12%   | 3%      |
| Arctic    | 30%   | 25%      | 10%  | 15%   | 20%     |
| Desert    | 70%   | 15%      | 5%   | 8%    | 2%      |
| Swamp     | 20%   | 40%      | 30%  | 8%    | 2%      |
| Mountain  | 35%   | 25%      | 15%  | 18%   | 7%      |
| Coastal   | 30%   | 30%      | 22%  | 14%   | 4%      |
| Volcanic  | 40%   | 30%      | 10%  | 10%   | 10%     |

### Weather State Definitions

| Weather State | Description                                                   |
| ------------- | ------------------------------------------------------------- |
| Clear         | No weather effects; baseline conditions apply                 |
| Overcast      | Minimal impact; slight mood modifier to NPC satisfaction      |
| Rain          | Moderate impact; affects gathering and travel                 |
| Storm         | Significant impact; affects all activities, minor hazard risk |
| Extreme       | Climate-specific severe event (blizzard, sandstorm, etc.)     |

### Batch Weather Calculation

```text
On check-in:
  1. For each zone with active tasks:
     a. Roll weather state from climate probability table
     b. Apply seasonal modifier to probabilities (see Section 5)
     c. Calculate weather modifier for elapsed time
     d. Apply modifier to all queued task outputs
  2. Weather persists until next check-in (new roll)
```

---

## 4. Weather Effects on Gameplay

Weather modifiers stack with climate modifiers multiplicatively. All effects apply to the batch calculation for the elapsed period.

### Gathering Efficiency Modifiers

| Weather  | Flora Gathering | Mining/Ore | Fishing | Foraging |
| -------- | --------------- | ---------- | ------- | -------- |
| Clear    | +0%             | +0%        | +0%     | +0%      |
| Overcast | +0%             | +0%        | +5%     | +0%      |
| Rain     | -10%            | -5%        | +15%    | -15%     |
| Storm    | -25%            | -15%       | -30%    | -30%     |
| Extreme  | -50%            | -30%       | Blocked | Blocked  |

### Travel Time Modifiers

| Weather  | Road | Trail | Wilderness | Water Crossing |
| -------- | ---- | ----- | ---------- | -------------- |
| Clear    | +0%  | +0%   | +0%        | +0%            |
| Overcast | +0%  | +0%   | +0%        | +0%            |
| Rain     | +5%  | +10%  | +15%       | +20%           |
| Storm    | +15% | +25%  | +35%       | +50%           |
| Extreme  | +30% | +50%  | +75%       | Blocked        |

### Combat Modifiers

Weather applies percentage modifiers to combat resolution rolls. See [combat-resolution.md](combat-resolution.md) for base combat formulas.

| Weather  | Melee | Ranged | Magic | Defence |
| -------- | ----- | ------ | ----- | ------- |
| Clear    | +0%   | +0%    | +0%   | +0%     |
| Overcast | +0%   | +0%    | +0%   | +0%     |
| Rain     | -5%   | -15%   | -5%   | +0%     |
| Storm    | -10%  | -30%   | -10%  | -5%     |
| Extreme  | -20%  | -50%   | -20%  | -15%    |

---

## 5. Seasonal Cycles

The game world cycles through four seasons. Season length is configurable; the default is 7 real-time days per season (28-day full cycle). Seasons modify resource availability, hazard frequency, and weather probabilities.

### Season Definitions

| Season | Duration (default) | Thematic Effect                   |
| ------ | ------------------ | --------------------------------- |
| Spring | 7 days             | Growth, renewal, moderate weather |
| Summer | 7 days             | Abundance, heat, longer days      |
| Autumn | 7 days             | Harvest, preparation, cooling     |
| Winter | 7 days             | Scarcity, cold, harsh weather     |

### Seasonal Resource Modifiers

| Resource Type    | Spring | Summer | Autumn | Winter |
| ---------------- | ------ | ------ | ------ | ------ |
| Flora/Herbs      | +15%   | +10%   | -5%    | -30%   |
| Crops            | +10%   | +20%   | +25%   | -50%   |
| Wildlife/Hunting | +5%    | +0%    | +10%   | -15%   |
| Fishing          | +10%   | +5%    | +15%   | -20%   |
| Mining/Ore       | +0%    | +0%    | +0%    | +0%    |
| Foraging         | +10%   | +15%   | +20%   | -40%   |

### Seasonal Weather Probability Shifts

Seasons shift the weather probability table before rolling. Applied as additive adjustments to base climate probabilities (clamped to valid range, then renormalized):

| Season | Clear | Overcast | Rain | Storm | Extreme |
| ------ | ----- | -------- | ---- | ----- | ------- |
| Spring | -5%   | +0%      | +5%  | +0%   | +0%     |
| Summer | +10%  | -5%      | -5%  | +0%   | +0%     |
| Autumn | -5%   | +5%      | +5%  | +0%   | -5%     |
| Winter | -10%  | +0%      | -5%  | +5%   | +10%    |

### Seasonal Hazard Changes

| Season | Added Hazards           | Reduced Hazards      |
| ------ | ----------------------- | -------------------- |
| Spring | Flooding, mudslides     | Frostbite, blizzards |
| Summer | Heatwave, wildfire      | Flooding, frostbite  |
| Autumn | Early frost, windstorms | Heatwave, wildfire   |
| Winter | Blizzards, frostbite    | Heatwave, sandstorms |

---

## 6. Terrain Types and Movement Modifiers

Terrain is fixed per map tile and modifies travel timer duration when characters move through.

### Terrain Movement Table

| Terrain        | Travel Modifier | Notes                            |
| -------------- | --------------- | -------------------------------- |
| Road           | -20% faster     | Fastest travel; well-maintained  |
| Plains         | +0% (baseline)  | Open ground, standard speed      |
| Forest         | +15% slower     | Dense vegetation, reduced paths  |
| Dense Forest   | +30% slower     | Very limited routes              |
| Hills          | +20% slower     | Elevation changes                |
| Mountains      | +40% slower     | Steep terrain, climbing required |
| Swamp          | +35% slower     | Waterlogged, difficult footing   |
| Desert Sand    | +25% slower     | Shifting ground                  |
| Snow (light)   | +15% slower     | Cold exposure                    |
| Snow (deep)    | +40% slower     | Exhausting movement              |
| Rocky          | +20% slower     | Uneven footing                   |
| River Crossing | +30% slower     | Fording or bridge required       |
| Coastal        | +10% slower     | Sandy/rocky shore terrain        |

### Terrain Gathering Modifiers

Terrain type modifies the base gathering output for resources found in that tile:

| Terrain      | Flora | Ore/Stone | Wildlife | Water |
| ------------ | ----- | --------- | -------- | ----- |
| Plains       | +0%   | +0%       | +0%      | +0%   |
| Forest       | +15%  | -10%      | +10%     | +5%   |
| Dense Forest | +25%  | -20%      | +15%     | +10%  |
| Hills        | +0%   | +10%      | +5%      | +0%   |
| Mountains    | -10%  | +25%      | -5%      | -5%   |
| Swamp        | +10%  | -15%      | +5%      | +15%  |
| Desert Sand  | -20%  | +5%       | -15%     | -30%  |
| Coastal      | +0%   | +0%       | +0%      | +10%  |

---

## 7. Environmental Hazards

Hazards are triggered by climate, weather, and seasonal conditions. When a hazard triggers during a batch calculation, it applies damage or delays to characters in the affected zone.

### Hazard Types

| Hazard            | Trigger Conditions          | Damage Type | Severity  |
| ----------------- | --------------------------- | ----------- | --------- |
| Extreme Cold      | Arctic + Winter or Extreme  | Cold        | Moderate  |
| Frostbite         | Arctic + Extreme weather    | Cold        | High      |
| Heatwave          | Desert + Summer + Clear     | Heat        | Moderate  |
| Dehydration       | Desert + any non-Rain       | Heat        | Low-High  |
| Flooding          | Tropical/Swamp + Storm      | Water       | Moderate  |
| Flash Flood       | Mountain/Canyon + Storm     | Water       | High      |
| Sandstorm         | Desert + Extreme weather    | Physical    | High      |
| Blizzard          | Arctic/Mountain + Extreme   | Cold        | High      |
| Volcanic Activity | Volcanic + any              | Fire/Toxic  | Very High |
| Toxic Gas         | Volcanic/Swamp + any        | Poison      | Moderate  |
| Avalanche         | Mountain + Storm/Extreme    | Physical    | Very High |
| Wildfire          | Temperate/Tropical + Summer | Fire        | High      |
| Mudslide          | Hills/Mountain + Rain/Storm | Physical    | Moderate  |

### Hazard Trigger Probability

At each check-in, for each zone with active characters, hazards are rolled based on matching conditions:

| Severity  | Base Trigger Chance | With Extreme Weather |
| --------- | ------------------- | -------------------- |
| Low       | 15%                 | 30%                  |
| Moderate  | 10%                 | 25%                  |
| High      | 5%                  | 15%                  |
| Very High | 2%                  | 8%                   |

---

## 8. Hazard Damage and Mitigation

When a hazard triggers, it applies a health percentage penalty to characters in the zone. Mitigation reduces the damage through equipment, skills, and shelter.

### Base Hazard Damage

| Severity  | Health Loss (% max) | Timer Delay  |
| --------- | ------------------- | ------------ |
| Low       | 5%                  | +10% to task |
| Moderate  | 10%                 | +20% to task |
| High      | 20%                 | +35% to task |
| Very High | 35%                 | +50% to task |

### Mitigation Sources

Each mitigation source provides a percentage reduction to hazard damage. Multiple sources stack additively up to a maximum of 90% reduction.

| Mitigation Source        | Damage Reduction | Notes                              |
| ------------------------ | ---------------- | ---------------------------------- |
| Climate-appropriate gear | 25-40%           | Tier depends on equipment quality  |
| Climate Adaptation skill | 15-30%           | Scales with skill level            |
| Shelter (settlement)     | 50%              | Characters in settlements          |
| Shelter (camp)           | 25%              | Temporary camp during travel       |
| Consumables              | 20%              | Warming/cooling potions, antidotes |
| Terrain knowledge        | 10%              | Pathfinding or related skills      |

### Mitigation by Equipment Quality

Equipment quality (see [quality-tiers.md](quality-tiers.md)) affects the mitigation percentage of climate-appropriate gear:

| Gear Quality | Hazard Mitigation |
| ------------ | ----------------- |
| Poor         | 15%               |
| Standard     | 25%               |
| Superior     | 30%               |
| Masterwork   | 35%               |
| Legendary    | 40%               |

---

## 9. Terrain-Based Combat Modifiers

Terrain applies modifiers to combat resolution when encounters occur on specific tile types. These stack with weather combat modifiers from Section 4.

| Terrain      | Melee | Ranged | Defence | Special                       |
| ------------ | ----- | ------ | ------- | ----------------------------- |
| Road         | +0%   | +0%    | +0%     | None                          |
| Plains       | +0%   | +5%    | +0%     | Open sightlines favour ranged |
| Forest       | +5%   | -10%   | +10%    | Cover bonus, limited range    |
| Dense Forest | +0%   | -25%   | +15%    | Heavy cover, melee congestion |
| Hills        | +0%   | +10%   | +5%     | Elevation advantage           |
| Mountains    | -5%   | +5%    | +10%    | Difficult positioning         |
| Swamp        | -10%  | -5%    | -5%     | Slowed movement for all       |
| Desert Sand  | -5%   | +0%    | -5%     | Footing penalty               |
| Snow         | -5%   | -5%    | +0%     | Cold exposure during combat   |
| Rocky        | +0%   | +0%    | +10%    | Natural fortification         |

See [combat-resolution.md](combat-resolution.md) for how terrain modifiers integrate into the combat formula.

---

## 10. Environmental Resource Modifiers

The final gathering output is calculated by stacking climate, terrain, weather, and seasonal modifiers:

```text
Final Output = Base Output
  x Climate Gathering Modifier
  x Terrain Gathering Modifier
  x Weather Gathering Modifier
  x Seasonal Resource Modifier
  x Equipment Efficiency Modifier

All modifiers expressed as (1 + percentage/100).
Example: +15% = 1.15 multiplier, -20% = 0.80 multiplier.
```

### Example Calculations

**Herb gathering in Temperate Forest during Spring with Rain:**

```text
Base: 100 herbs
Climate (Temperate): x1.00
Terrain (Forest Flora): x1.15
Weather (Rain Flora): x0.90
Season (Spring Flora): x1.15
= 100 x 1.00 x 1.15 x 0.90 x 1.15
= 119 herbs (rounded)
```

**Ore mining in Mountain during Winter with Storm:**

```text
Base: 50 ore
Climate (Mountain): x0.90
Terrain (Mountain Ore): x1.25
Weather (Storm Mining): x0.85
Season (Winter Mining): x1.00
= 50 x 0.90 x 1.25 x 0.85 x 1.00
= 48 ore (rounded)
```

See [gathering-system-gdd.md](../systems/gathering-system-gdd.md) for base gathering rates and resource tables.

---

## 11. Offline Environmental Effects

When a player is offline, environmental effects are batch-calculated on their next check-in across the entire elapsed period.

### Offline Weather Simulation

```text
Offline period processing:
  1. Divide elapsed time into intervals (default: 1 hour each)
  2. For each interval, roll weather state per zone
  3. Aggregate weather modifiers across all intervals
  4. Apply averaged modifier to total offline output
  5. Roll hazard checks per interval (reduced frequency)
```

### Offline Hazard Rules

| Rule                  | Detail                                                  |
| --------------------- | ------------------------------------------------------- |
| Hazard frequency      | Halved during offline periods (characters take shelter) |
| Maximum hazard damage | Capped at 50% of max health per offline session         |
| Lethal hazards        | Cannot kill characters offline; reduce to 1 HP minimum  |
| Task interruption     | Hazards add delay to timers, never cancel queued tasks  |
| Shelter bonus         | Characters in settlements are immune to offline hazards |

### Offline Seasonal Progression

Seasons advance during offline periods. If a player is offline long enough for a season to change, the system splits the batch calculation at the season boundary and applies the correct seasonal modifiers to each portion.

---

## 12. Cross-References

| System            | Relationship                                       | Document                                                        |
| ----------------- | -------------------------------------------------- | --------------------------------------------------------------- |
| Combat Resolution | Weather and terrain modifiers feed combat formulas | [combat-resolution.md](combat-resolution.md)                    |
| Gathering System  | Environmental modifiers scale gathering output     | [gathering-system-gdd.md](../systems/gathering-system-gdd.md)   |
| Quality Tiers     | Equipment quality affects hazard mitigation        | [quality-tiers.md](quality-tiers.md)                            |
| Combat System     | Terrain and weather affect encounter difficulty    | [combat-system-gdd.md](../systems/combat-system-gdd.md)         |
| Settlement System | Climate zone affects settlement growth and shelter | [settlement-system-gdd.md](../systems/settlement-system-gdd.md) |
