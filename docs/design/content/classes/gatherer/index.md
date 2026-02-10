---
title: Gatherer
gdd_ref: systems/class-system-gdd.md#foundation-classes
tree_tier: 1
---

# Gatherer

## Lore

The Gatherer represents the oldest profession - finding and collecting what the world provides. Before agriculture, before crafting, there was gathering: knowing where to find resources, how to harvest them sustainably, and how to bring them home safely. Every specialist gatherer builds upon this foundation.

Gatherers emerge from the land itself. The child who accompanies elders into the forest to collect mushrooms. The settler learning which plants are edible in their new home. The survivor who discovers they must harvest their own resources or go without. The world offers abundance to those who know how to look.

The Gatherers' Guild teaches the fundamentals that all resource collection shares. How to identify quality specimens. When to harvest for best results. How to preserve what you collect. These basics apply whether you're picking berries, mining ore, or catching fish.

## Mechanics

### Requirements

| Requirement    | Value                         |
| -------------- | ----------------------------- |
| Unlock Trigger | 100 gathering-related actions |
| Primary Tags   | `Gathering`                   |
| Tier           | 1 (Foundation)                |
| Max Tag Depth  | 1 level                       |

### Unlock Requirements

**Tracked Actions:**

- Harvest any resource node
- Mine ore or stone
- Fish in any body of water
- Hunt or trap animals
- Forage plants, herbs, or mushrooms

**Threshold:** 100 actions (guaranteed unlock) or early unlock via probability

### Starting Skills

Skills automatically awarded when accepting this class:

| Skill        | Type            | Link                                                           | Description                       |
| ------------ | --------------- | -------------------------------------------------------------- | --------------------------------- |
| Basic Tools  | Mechanic Unlock | [Basic Tools](././skills/mechanic-unlock/basic-tools/index.md) | Access to basic gathering tools   |
| Resource Eye | Passive         | [Gatherer's Eye](././skills/common/gatherers-eye/index.md)     | +5% chance to spot resource nodes |

### XP Progression

Uses Tier 1 formula: `XP = 100 × 1.5^(level - 1)`

| Level | XP to Next | Cumulative |
| ----- | ---------- | ---------- |
| 1→2   | 100        | 100        |
| 5→6   | 506        | 1,131      |
| 10→11 | 3,844      | 6,513      |

### Tracked Actions for XP

| Action                      | XP Value       |
| --------------------------- | -------------- |
| Harvest a resource node     | 5-15 XP        |
| Find a rare resource        | 15-30 XP       |
| Complete a gathering trip   | 10-25 XP       |
| Harvest exceptional quality | 20-40 XP bonus |
| Discover new gathering spot | 10-20 XP       |

### Progression Paths

Gatherer can unlock eligibility for the following Tier 2 classes upon reaching 5,000 XP:

| Tier 2 Class | Focus                       | Link                                          |
| ------------ | --------------------------- | --------------------------------------------- |
| Miner        | Ore and mineral extraction  | [Miner](./gathering/miner/index.md)           |
| Forager      | Wild plant gathering        | [Forager](./gathering/forager/index.md)       |
| Hunter       | Animal tracking and hunting | [Hunter](./gathering/hunter/index.md)         |
| Lumberjack   | Tree harvesting             | [Lumberjack](./gathering/lumberjack/index.md) |
| Fisher       | Aquatic resources           | [Fisher](./gathering/fisher/index.md)         |
| Farmer       | Crop cultivation            | [Farmer](./gathering/farmer/index.md)         |
| Herbalist    | Medicinal plants            | [Herbalist](./gathering/herbalist/index.md)   |

### Tag Access

As a Tier 1 class, Gatherer has access to depth 1 tags only:

| Accessible  | Not Accessible        |
| ----------- | --------------------- |
| `Gathering` | `Gathering/Mining`    |
|             | `Gathering/Herbalism` |
|             | `Gathering/Hunting`   |

## Progression

### Specializations

- [Miner](./miner/) - Ore and mineral extraction
- [Forager](./forager/) - Wild plant collection
- [Hunter](./hunter/) - Beast tracking and hunting, path to Ranger
- [Lumberjack](./lumberjack/) - Timber harvesting
- [Fisher](./fisher/) - Aquatic resource harvesting
- [Farmer](./farmer/) - Agricultural cultivation
- [Herbalist](./herbalist/) - Medicinal plant collection

## Related Content

- **Tier System:** [Class Tiers](./././systems/character/class-tiers/index.md)
- **Gathering System:** [Resource Gathering](./././systems/world/resource-gathering/index.md)
- **See Also:** [Gathering Classes Index](./gathering/index.md)
