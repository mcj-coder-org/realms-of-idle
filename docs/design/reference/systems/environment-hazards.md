---
type: reference
scope: detailed
status: authoritative
version: 1.0.0
created: 2026-02-08
updated: 2026-02-08
subjects: [environment, hazards, climate, weather, terrain, creatures, idle]
dependencies: []
---

# Environment & Hazards - Authoritative Reference

## Overview

This reference document defines the environmental modifiers that affect expedition timers, risk events, and zone access requirements. Real-time weather simulation and survival mechanics are replaced with static zone modifiers and equipment prerequisites. Climate, terrain, and hazards combine into a single "environment difficulty" modifier per zone that scales expedition timers and risk probabilities.

---

## 1. Climate Zones

Climate is a fixed property of each region. It determines the base environment difficulty modifier.

| Climate   | Temperature Profile   | Base Difficulty Modifier | Equipment Required     |
| --------- | --------------------- | ------------------------ | ---------------------- |
| Temperate | Mild                  | 1.0x (baseline)          | None                   |
| Coastal   | Mild, windy           | 1.05x                    | None                   |
| Tropical  | Hot, humid            | 1.15x                    | Light gear             |
| Swamp     | Warm, humid           | 1.20x                    | Disease protection     |
| Mountain  | Cold, variable        | 1.20x                    | Climbing gear          |
| Desert    | Hot days, cold nights | 1.25x                    | Water supply, sun gear |
| Tundra    | Cold to extreme cold  | 1.30x                    | Cold weather gear      |
| Volcanic  | Hot, toxic            | 1.35x                    | Heat protection, mask  |

### 1.1 Climate Effects on Expeditions

| Climate   | Timer Penalty | Risk Modifier | Additional Hazards      |
| --------- | ------------- | ------------- | ----------------------- |
| Temperate | None          | 1.0x          | None                    |
| Coastal   | +5%           | 1.1x          | Storm surge             |
| Tropical  | +15%          | 1.2x          | Disease, flash floods   |
| Swamp     | +20%          | 1.3x          | Disease, difficult path |
| Mountain  | +20%          | 1.3x          | Avalanche, altitude     |
| Desert    | +25%          | 1.4x          | Dehydration, sandstorms |
| Tundra    | +30%          | 1.5x          | Frostbite, blizzards    |
| Volcanic  | +35%          | 1.6x          | Toxic air, lava flows   |

### 1.2 Climate Mitigation

Appropriate equipment reduces climate penalties:

| Mitigation        | Timer Reduction | Risk Reduction |
| ----------------- | --------------- | -------------- |
| No equipment      | 0%              | 0%             |
| Basic gear        | -40%            | -30%           |
| Specialized gear  | -60%            | -50%           |
| Survivalist skill | -75%            | -65%           |

---

## 2. Terrain Types

Terrain modifies expedition speed within a zone.

| Terrain         | Movement Modifier | Expedition Timer Impact |
| --------------- | ----------------- | ----------------------- |
| Road            | +20% speed        | -15% timer              |
| Path/Trail      | Normal            | No change               |
| Grassland       | Normal            | No change               |
| Forest          | -10% speed        | +10% timer              |
| Dense Forest    | -25% speed        | +20% timer              |
| Hills           | -15% speed        | +15% timer              |
| Mountains       | -30% speed        | +25% timer              |
| Swamp           | -40% speed        | +30% timer              |
| Desert Sand     | -20% speed        | +20% timer              |
| Snow (light)    | -10% speed        | +10% timer              |
| Snow (deep)     | -35% speed        | +30% timer              |
| Rocky           | -15% speed        | +15% timer              |
| Water (shallow) | -30% speed        | +25% timer              |
| Water (deep)    | Impassable        | Requires swimming/boat  |

### 2.1 Terrain Features

| Feature       | Effect on Expeditions            |
| ------------- | -------------------------------- |
| Cliff         | Blocks routes, requires climbing |
| River         | Crossing delays, bridge shortcut |
| Cave entrance | Dungeon access point             |
| Ruins         | Exploration opportunity          |
| Resource node | Gathering opportunity            |
| Landmark      | Improves mapping speed           |

---

## 3. Environmental Hazards

Hazards are risk events that can occur during expeditions in specific zone types. Each hazard has a trigger condition, effect, and mitigation.

### 3.1 Natural Hazards

| Hazard      | Zone Types            | Effect on Expedition            | Mitigation                    |
| ----------- | --------------------- | ------------------------------- | ----------------------------- |
| Falling     | Mountain, cliff       | Party member health damage      | Climbing gear, climbing skill |
| Avalanche   | Mountain, snow        | Major health damage, timer +50% | Awareness, shelter timing     |
| Rockslide   | Mountain, cave        | Health damage, route blocked    | Awareness, agility            |
| Flash Flood | Canyon, tropical      | Health damage, loot loss        | Weather awareness             |
| Quicksand   | Swamp, desert         | Expedition delay +30%           | Awareness, rope               |
| Wildfire    | Forest, dry           | Health damage, route blocked    | Firebreak, escape route       |
| Toxic Gas   | Swamp, volcanic, cave | Poison damage to party          | Detection skill, mask         |
| Lightning   | Any (storm zones)     | Instant damage to one member    | Shelter, no metal armour      |
| Drowning    | Water zones           | Health damage or death          | Swimming skill, flotation     |

### 3.2 Hazard Probability per Expedition

| Zone Danger Level | Hazard Chance per Expedition |
| ----------------- | ---------------------------- |
| Safe              | 2%                           |
| Low               | 5%                           |
| Moderate          | 10%                          |
| High              | 20%                          |
| Extreme           | 35%                          |
| Legendary         | 50%                          |

### 3.3 Status Conditions from Hazards

Hazards may inflict status conditions that persist after the expedition:

| Condition   | Cause                | Effect                   | Recovery        |
| ----------- | -------------------- | ------------------------ | --------------- |
| Poisoned    | Toxic exposure       | -10% stats for 12 hours  | Antidote, rest  |
| Frostbitten | Cold exposure        | -15% stats for 24 hours  | Warmth, healing |
| Exhausted   | Harsh terrain        | -20% for next expedition | Rest timer      |
| Diseased    | Swamp, contamination | -10% stats for 48 hours  | Treatment, time |
| Injured     | Fall, combat         | -25% stats until healed  | Healing, rest   |

---

## 4. Zone Difficulty Rating

Each zone has a composite difficulty rating that combines climate, terrain, and creature presence.

### 4.1 Danger Levels

| Level     | Typical Location   | Recommended Party Level | Expedition Risk |
| --------- | ------------------ | ----------------------- | --------------- |
| Safe      | Inside settlements | Any                     | Minimal         |
| Low       | Near settlements   | 1-5                     | Low             |
| Moderate  | Wilderness         | 5-10                    | Moderate        |
| High      | Remote areas       | 10-15                   | High            |
| Extreme   | Dungeons, volcanic | 15-20                   | Very High       |
| Legendary | Special areas      | 20+                     | Extreme         |

### 4.2 Composite Difficulty Formula

```
Zone Difficulty = Climate Modifier x Terrain Modifier x Creature Danger Modifier

Applied to expeditions as:
  Timer Multiplier = Zone Difficulty
  Risk Multiplier  = Zone Difficulty x 1.2
```

---

## 5. Creature Distribution by Environment

Creature encounter tables are zone-based. Environment type determines which creatures appear.

### 5.1 Common Creatures by Habitat

| Environment | Common Creatures                     |
| ----------- | ------------------------------------ |
| Forest      | Wolves, bears, deer, boars           |
| Plains      | Horses, cattle, small predators      |
| Mountains   | Goats, eagles, mountain lions        |
| Swamp       | Snakes, insects, crocodiles          |
| Desert      | Scorpions, snakes, camels            |
| Tundra      | Wolves, bears, mammoths              |
| Coastal     | Seabirds, crabs, sea creatures       |
| Caves       | Bats, spiders, underground creatures |

### 5.2 Monster Distribution by Environment

| Environment | Monster Types                 |
| ----------- | ----------------------------- |
| Forest      | Goblins, treants, fey         |
| Mountains   | Giants, harpies, dragons      |
| Swamp       | Undead, swamp creatures, hags |
| Desert      | Elementals, giant insects     |
| Tundra      | Ice creatures, wendigos       |
| Volcanic    | Fire elementals, demons       |
| Ruins       | Undead, constructs            |

---

## 6. Equipment Requirements

### 6.1 Climate Gear

Equipment required to access or reduce penalties in specific climates:

| Climate  | Required Gear               | Effect When Equipped       |
| -------- | --------------------------- | -------------------------- |
| Cold     | Furs, insulated clothing    | Removes cold penalties     |
| Hot      | Light, breathable clothing  | Removes heat penalties     |
| Wet      | Waterproof cloak, boots     | Removes wet penalties      |
| Desert   | Robes, head covering        | Removes desert penalties   |
| Mountain | Climbing gear, warm layers  | Removes climbing barriers  |
| Volcanic | Heat-resistant armour, mask | Reduces volcanic penalties |

### 6.2 Survival Consumables

| Item           | Effect                        | Availability      |
| -------------- | ----------------------------- | ----------------- |
| Rations        | Prevents expedition abort     | Standard supply   |
| Water supply   | Required for desert zones     | Purchased/crafted |
| Antidote       | Cures poison condition        | Alchemy crafting  |
| Warming potion | Cold resistance, 1 expedition | Alchemy           |
| Cooling potion | Heat resistance, 1 expedition | Alchemy           |

---

## 7. Environmental Skills

### 7.1 Relevant Classes

| Class       | Environmental Focus       |
| ----------- | ------------------------- |
| Explorer    | Navigation, route-finding |
| Ranger      | Wilderness mastery        |
| Survivalist | Harsh condition tolerance |
| Hunter      | Tracking, wildlife        |

### 7.2 Skill Effects

| Skill              | Effect                                  |
| ------------------ | --------------------------------------- |
| Climate Adaptation | Reduces climate timer penalty by 25-40% |
| Pathfinding        | Reduces terrain timer penalty by 20-30% |
| Foraging           | Extends expedition range, fewer aborts  |
| Swimming           | Enables water zone access               |
| Climbing           | Enables mountain/cliff zone access      |
| Poison Resistance  | Reduces toxic hazard damage by 50%      |

---

## 8. Data Tables

### 8.1 Climate Penalty Summary

| Climate   | Base Timer Penalty | With Gear | With Skill |
| --------- | ------------------ | --------- | ---------- |
| Temperate | +0%                | +0%       | +0%        |
| Desert    | +25%               | +10%      | +5%        |
| Tundra    | +30%               | +12%      | +6%        |
| Tropical  | +15%               | +6%       | +3%        |
| Swamp     | +20%               | +10%      | +5%        |
| Mountain  | +20%               | +8%       | +4%        |
| Volcanic  | +35%               | +20%      | +15%       |

### 8.2 Weather as Zone Modifier

Weather is abstracted as a static zone property rather than a dynamic system:

| Weather Profile | Zone Modifier | Applied As                       |
| --------------- | ------------- | -------------------------------- |
| Clear           | 1.0x          | No additional penalty            |
| Rainy           | 1.1x          | +10% timer, -10% ranged combat   |
| Stormy          | 1.25x         | +25% timer, -25% ranged, hazards |
| Extreme         | 1.5x          | +50% timer, -30% all combat      |

Each zone has a fixed weather profile determined by climate. Weather does not change dynamically.

### 8.3 Regional Danger Indicators

| Indicator          | Meaning                     |
| ------------------ | --------------------------- |
| No wildlife        | Something scared them away  |
| Abundant wildlife  | Relatively safe             |
| Old bones          | Past deaths, ongoing threat |
| Warning signs      | NPCs know danger            |
| Lack of travellers | Avoided area                |
| Patrol presence    | Monitored, safer            |
