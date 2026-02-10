---
title: Crafter
type: class
tier: 1
category: foundation
tags:
  - Crafting
tracked_actions:
  - Craft items
  - Use workshops
  - Repair equipment
  - Process materials
unlocks:
  - Blacksmith
  - Alchemist
  - Tailor
  - Carpenter
  - Cook
  - Jeweler
  - Leatherworker
  - Glassblower
  - Weaver
---

# Crafter

## Lore

The Crafter represents humanity's fundamental drive to shape the world - to take raw materials and transform them into useful objects. Before anyone becomes a master smith or renowned alchemist, they first learn to be a Crafter: understanding tools, respecting materials, and developing the patience that all making requires.

Crafters emerge wherever people need things made. The child helping their parent in the family workshop. The apprentice sweeping floors and watching masters work. The survivor who learns to repair their own gear when no specialist is available. The hands-on work of creation calls to them.

The Crafters' Guild welcomes all who wish to learn the art of making. Basic tool use, material identification, workshop safety, quality assessment. These fundamentals form the foundation upon which all crafting specialties build. Every master began by learning to sharpen tools and organize workspaces.

## Mechanics

### Requirements

| Requirement    | Value                        |
| -------------- | ---------------------------- |
| Unlock Trigger | 100 crafting-related actions |
| Primary Tags   | `Crafting`                   |
| Tier           | 1 (Foundation)               |
| Max Tag Depth  | 1 level                      |

### Unlock Requirements

**Tracked Actions:**

- Craft any type of item
- Use any crafting workshop or workspace
- Repair damaged equipment
- Process raw materials into crafting components

**Threshold:** 100 actions (guaranteed unlock) or early unlock via probability

### Starting Skills

Skills automatically awarded when accepting this class:

| Skill                 | Type            | Link                                                                                 | Description                                      |
| --------------------- | --------------- | ------------------------------------------------------------------------------------ | ------------------------------------------------ |
| Basic Workshop Access | Mechanic Unlock | [Basic Workshop Access](../../skills/mechanic-unlock/basic-workshop-access/index.md) | Can use basic crafting workspaces                |
| Material Sense        | Passive         | [Material Knowledge](../../skills/common/material-knowledge/index.md)                | +5% success rate when assessing material quality |

### XP Progression

Uses Tier 1 formula: `XP = 100 × 1.5^(level - 1)`

| Level | XP to Next | Cumulative |
| ----- | ---------- | ---------- |
| 1→2   | 100        | 100        |
| 5→6   | 506        | 1,131      |
| 10→11 | 3,844      | 6,513      |

### Tracked Actions for XP

| Action                        | XP Value       |
| ----------------------------- | -------------- |
| Craft a simple item           | 5-15 XP        |
| Craft a complex item          | 15-30 XP       |
| Successfully repair equipment | 5-15 XP        |
| Process rare materials        | 10-25 XP       |
| Create quality item           | 20-40 XP bonus |

### Progression Paths

Crafter can transition to the following Tier 2 classes upon reaching 5,000 XP:

| Tier 2 Class  | Focus                         | Link                                                |
| ------------- | ----------------------------- | --------------------------------------------------- |
| Blacksmith    | Metal working and forging     | [Blacksmith](../crafting/blacksmith/index.md)       |
| Alchemist     | Potions and chemical crafting | [Alchemist](../crafting/alchemist/index.md)         |
| Tailor        | Cloth and clothing            | [Tailor](../crafting/tailor/index.md)               |
| Carpenter     | Woodworking                   | [Carpenter](../crafting/carpenter/index.md)         |
| Cook          | Food preparation              | [Cook](../crafting/cook/index.md)                   |
| Jeweler       | Gems and jewelry              | [Jeweler](../crafting/jeweler/index.md)             |
| Leatherworker | Leather goods                 | [Leatherworker](../crafting/leatherworker/index.md) |
| Glassblower   | Glass crafting                | [Glassblower](../crafting/glassblower/index.md)     |
| Weaver        | Textiles                      | [Weaver](../crafting/weaver/index.md)               |

### Tag Access

As a Tier 1 class, Crafter has access to depth 1 tags only:

| Accessible | Not Accessible         |
| ---------- | ---------------------- |
| `Crafting` | `Crafting/Smithing`    |
|            | `Crafting/Alchemy`     |
|            | `Crafting/Woodworking` |

## Progression

### Specializations

- [Blacksmith](./blacksmith/) - Metal smithing, path to Weaponsmith and Armorsmith
- [Alchemist](./alchemist/) - Potions and transmutation
- [Tailor](./tailor/) - Cloth and fabric crafting
- [Carpenter](./carpenter/) - Wood and furniture crafting
- [Cook](./cook/) - Food preparation and preservation
- [Jeweler](./jeweler/) - Gems and jewelry crafting
- [Glassblower](./glassblower/) - Glass and crystal crafting
- [Leatherworker](./leatherworker/) - Leather and hide crafting
- [Weaver](./weaver/) - Textile and fiber crafting

## Related Content

- **Tier System:** [Class Tiers](../../../systems/character/class-tiers/index.md)
- **Crafting System:** [Crafting Progression](../../../systems/crafting/crafting-progression/index.md)
- **See Also:** [Crafting Classes Index](../crafting/index.md)
