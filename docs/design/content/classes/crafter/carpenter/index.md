---
title: 'Carpenter'
type: 'class'
category: 'crafting'
tier: 2
prerequisite_xp: 5000
prerequisite_actions: woodworking
summary: 'Master of woodworking, building furniture, structures, and wooden tools'
---

# Carpenter

## Lore

### Origin

Wood built the world. Before stone castles rose and metal bridges spanned rivers, wood gave shelter, tools, and vessels. The carpenter's craft stretches back to the first human who realized a shaped branch worked better than a raw one. From that simple beginning grew an art that encompasses furniture makers, house builders, shipwrights, and wheelwrights - all united by their respect for wood and their skill in shaping it.

Wood lives even after the tree falls, carpenters say. Each plank remembers the seasons that formed it, carrying the tree's strength and flexibility. Oak endures, ash bends without breaking, pine works easily but splits if rushed. Master carpenters read wood like scholars read books, seeing in the grain patterns how a piece will behave under stress, where it will want to crack, which way it prefers to be worked.

The Carpenters' Guild recognizes specializations but insists every member master fundamentals. Before you build a house, you must prove you can build a chair. Before you craft a ship, you must show you understand how wood moves with moisture and temperature. The greatest carpenter is not the one who builds the largest structure, but the one whose work stands firm after a hundred years.

### In the World

Civilization is built on the carpenter's work, often so well-made it becomes invisible. The floorboards that don't creak. The door that swings true after decades. The chair that supports weight without complaint. The roof that sheds rain for generations. People notice carpentry only when it fails - a testament to how well good carpenters do their job.

Village carpenters are jacks-of-all-trades: repairing tools one day, building a barn the next, crafting furniture for a wedding gift. City carpenters often specialize - some focus on fine furniture for nobles, others on the practical needs of shops and warehouses. Coastal carpenters learn the special demands of shipbuilding, where a poor joint can sink a vessel and kill everyone aboard.

Apprentices start with the simplest tasks: planing boards smooth, learning to saw straight, understanding how to read grain direction. They progress to joints - the puzzle-like connections that hold wood together without nails. A mortise and tenon joint, properly cut, becomes stronger as the wood ages and swells. Dovetails in a drawer mean it will last lifetimes. Master carpenters can cut joints so precise they need no glue, holding purely through perfect fit.

The workshop smells of fresh sawdust and wood oils. Shavings curl on the floor like golden ribbons. Tools hang with the care warriors give weapons - sharp chisels, precise saws, planes adjusted to take whisper-thin shavings. Stacks of lumber age quietly, drying to just the right moisture content. A carpenter's reputation is measured in their standing work: "See that inn? The beams are two centuries old and straight as the day they were raised. Ironhart family work."

---

## Mechanics

### Prerequisites

| Requirement        | Value                                                             |
| ------------------ | ----------------------------------------------------------------- |
| XP Threshold       | 5,000 XP from woodworking tracked actions                         |
| Related Foundation | [Crafter](../../foundation/index.md) (optional, provides bonuses) |
| Tag Depth Access   | 2 levels (e.g., `[Crafting/Woodworking]`)                         |

### Requirements

| Requirement       | Value                                    |
| ----------------- | ---------------------------------------- |
| Unlock Trigger    | Craft wooden items using carpentry tools |
| Primary Attribute | STR (Strength), PAT (Patience)           |
| Starting Level    | 1                                        |
| Tools Required    | Saw, chisel, plane, measuring tools      |

### Stats

#### Base Class Stats

| Level | HP Bonus | Stamina Bonus | Trait                |
| ----- | -------- | ------------- | -------------------- |
| 1     | +6       | +15           | Apprentice Carpenter |
| 10    | +18      | +40           | Journeyman Carpenter |
| 25    | +35      | +75           | Master Carpenter     |
| 50    | +60      | +130          | Legendary Carpenter  |

#### Crafting Bonuses

| Class Level | Quality Bonus | Speed Bonus | Material Efficiency | Durability Bonus |
| ----------- | ------------- | ----------- | ------------------- | ---------------- |
| 1-9         | +0%           | 0%          | 0%                  | +0%              |
| 10-24       | +10%          | +10%        | +8%                 | +15%             |
| 25-49       | +25%          | +25%        | +20%                | +30%             |
| 50+         | +50%          | +50%        | +35%                | +50%             |

#### Starting Skill

| Skill           | Type    | Effect                                     |
| --------------- | ------- | ------------------------------------------ |
| Basic Carpentry | Passive | Can craft basic wooden items and furniture |

#### Starting Skills (Auto-Awarded on Class Acceptance)

Skills automatically awarded when accepting this class:

| Skill              | Tier   | Link                                                                  | Reasoning                                             |
| ------------------ | ------ | --------------------------------------------------------------------- | ----------------------------------------------------- |
| Material Intuition | Lesser | [See Skill](../../skills/mechanic-unlock/material-intuition/index.md) | Understanding wood quality is essential for carpentry |
| Artisan's Focus    | Lesser | [See Skill](../../skills/tiered/artisans-focus/index.md)              | Precision work requires concentration                 |

#### Synergy Skills

Skills with strong synergies for Carpenter:

**General Crafting Skills**:

- [Artisan's Focus](../../skills/tiered/artisans-focus/index.md) - Lesser/Greater/Enhanced - Enhanced crafting concentration
- [Rapid Crafting](../../skills/tiered/rapid-crafting/index.md) - Lesser/Greater/Enhanced - Faster creation speed
- [Material Intuition](../../skills/mechanic-unlock/material-intuition/index.md) - Lesser/Greater/Enhanced - Assess material quality instantly
- [Resource Efficiency](../../skills/tiered/resource-efficiency/index.md) - Lesser/Greater/Enhanced - Chance to save materials
- [Masterwork Chance](../../skills/tiered/masterwork-chance/index.md) - Lesser/Greater/Enhanced - Increased chance for exceptional quality
- [Repair Mastery](../../skills/tiered/repair-mastery/index.md) - Lesser/Greater/Enhanced - Expert repair capabilities
- [Recipe Innovator](../../skills/passive-generator/recipe-innovator/index.md) - Lesser/Greater/Enhanced - Discover and adapt recipes
- [Tool Bond](../../skills/mechanic-unlock/tool-bond/index.md) - Lesser/Greater/Enhanced - Enhanced effectiveness with favored tools
- [Signature Style](../../skills/mechanic-unlock/signature-style/index.md) - Lesser/Greater/Enhanced - Develop recognizable craft signature

**Carpentry Skills** (specialized skills to be defined):

- Wood Reading - Lesser/Greater/Enhanced - Judge wood quality and properties
- Perfect Joints - Lesser/Greater/Enhanced - Superior joinery strength
- Structural Sense - Lesser/Greater/Enhanced - Design stable load-bearing structures
- Weatherproofing - Lesser/Greater/Enhanced - Protect wood from elements
- Carving Artistry - Lesser/Greater/Enhanced - Decorative woodwork

**Note**: All skills listed have strong synergies because they are core carpentry skills. Characters without Carpenter class can still learn these skills but progress at base rate (1x XP) without effectiveness bonuses.

#### Synergy Bonuses

Carpenter provides context-specific bonuses to woodworking-related skills based on logical specialization:

**Core Trade Skills** (Direct specialization - Strong synergy):

- **Wood Reading**: Essential for understanding wood properties and behavior
  - Faster learning: 2x XP from woodworking actions (scales with class level)
  - Better effectiveness: +25% accuracy at Carpenter 15, +35% at Carpenter 30+
  - Reduced cost: -25% time at Carpenter 15, -35% at Carpenter 30+
- **Perfect Joints**: Directly tied to structural integrity of woodwork
  - Faster learning: 2x XP from joinery work
  - Better strength: +30% joint strength at Carpenter 15
  - Lower failure rate: -50% chance of joint failure
- **Efficient Cuts**: Core skill for material conservation in carpentry
  - Faster learning: 2x XP from cutting and shaping
  - Better save rate: +30% wood preservation rate at Carpenter 15
  - Lower waste: -50% material loss on cutting errors
- **Structural Sense**: Fundamental to building safe, stable structures
  - Faster learning: 2x XP from construction work
  - Better stability: +25% load-bearing capacity at Carpenter 15
  - More accurate: +50% chance to identify weak points before building

**Synergy Strength Scales with Class Level**:

| Carpenter Level | XP Multiplier | Effectiveness Bonus | Cost Reduction | Example: Perfect Joints |
| --------------- | ------------- | ------------------- | -------------- | ----------------------- |
| Level 5         | 1.5x (+50%)   | +15%                | -15% stamina   | Good but moderate       |
| Level 10        | 1.75x (+75%)  | +20%                | -20% stamina   | Strong improvements     |
| Level 15        | 2.0x (+100%)  | +25%                | -25% stamina   | Excellent synergy       |
| Level 30+       | 2.5x (+150%)  | +35%                | -35% stamina   | Masterful synergy       |

**Example Progression**:

A Carpenter 15 learning Perfect Joints:

- Performs 50 joinery actions (earning 2x XP = 1000 XP total, vs 500 XP for non-Carpenter)
- Perfect Joints available at level-up after just 500 XP (vs 1500 XP for non-carpenter class)
- When using Perfect Joints: Base +30% joint strength becomes +60% strength (base +30% synergy)
- Stamina cost: 7.5 stamina instead of 10 (25% reduction)

#### Tracked Actions

Actions that grant XP to the Carpenter class:

| Action Category     | Specific Actions                                       | XP Value           |
| ------------------- | ------------------------------------------------------ | ------------------ |
| Crafting            | Build furniture, doors, windows, barrels, carts, boats | 10-150 per item    |
| Construction        | Build or repair structures, roofs, floors, walls       | 20-200 per project |
| Tool Making         | Craft tool handles, wooden tools, bows, staves         | 10-80 per item     |
| Joinery             | Cut and fit joints (dovetails, mortise-tenon, etc.)    | 5-25 per joint     |
| Carving             | Add decorative elements, reliefs, sculptures           | 10-60 per piece    |
| Repairing           | Repair furniture, structures, wooden items             | 5-50 per repair    |
| Weatherproofing     | Apply protective treatments to wood                    | 5-20 per treatment |
| Experimentation     | Test new techniques, wood types, joinery methods       | 15-45 per attempt  |
| Commission Work     | Complete customer orders                               | Bonus +50%         |
| Masterwork Creation | Create exceptional quality wooden item                 | 200-500            |

#### Class Consolidation

See [Class Consolidation System](../../../systems/character/class-consolidation/index.md) for full mechanics.

| Consolidation Path                       | Requirements                                                   | Result Class | Tier |
| ---------------------------------------- | -------------------------------------------------------------- | ------------ | ---- |
| [Artisan](../consolidation/index.md)     | Carpenter + any Crafter class                                  | Artisan      | 1    |
| [Woodmaster](../consolidation/index.md)  | Carpenter + [Bowyer](../consolidation/index.md) specialization | Woodmaster   | 1    |
| [Shipwright](../consolidation/index.md)  | Carpenter + maritime focus                                     | Shipwright   | 1    |
| [Builder](../consolidation/index.md)     | Carpenter + [Mason](../consolidation/index.md)                 | Builder      | 1    |
| [Homesteader](../consolidation/index.md) | Carpenter + [Farmer](../gathering/index.md)                    | Homesteader  | 1    |
| [Artificer](../consolidation/index.md)   | Master in 3+ crafting classes                                  | Artificer    | 3    |

### Interactions

| System                                                              | Interaction                                                                     |
| ------------------------------------------------------------------- | ------------------------------------------------------------------------------- |
| [Crafting](../../../systems/crafting/crafting-progression/index.md) | Primary wood crafting class; unlocks carpentry recipes                          |
| [Economy](../../../systems/economy/index.md)                        | Can establish workshop; essential for settlement growth                         |
| [Settlement](../../../systems/world/settlements/index.md)           | Builds structures, furniture; critical for expansion                            |
| [Combat](../../../systems/combat/index.md)                          | Crafts bows, staves, wooden shields, siege equipment                            |
| [Gathering](../../../systems/crafting/gathering/index.md)           | Synergy: [Lumberjack](../gathering/index.md) + Carpenter = efficient production |
| [Housing](../../../systems/world/housing-and-storage/index.md)      | Builds and upgrades player housing                                              |
| [Social](../../../systems/social/index.md)                          | Quality furniture improves home comfort and status                              |

---

## Related Content

- **Requires:** Carpentry tools (saw, plane, chisel), woodworking space
- **Materials Used:** [Wood](../../materials/index.md), [Nails](../../materials/index.md), [Wood Glue](../../materials/index.md), [Finishes](../../materials/index.md)
- **Creates:** [Furniture](../../items/index.md), [Doors](../../items/index.md), [Bows](../../items/index.md), [Staves](../../items/index.md), [Tools](../../items/index.md)
- **Recipes:** [Carpentry Recipes](../../../systems/crafting/index.md)
- **Synergy Classes:** [Lumberjack](../gathering/index.md), [Blacksmith](./blacksmith/index.md), [Merchant](../trade/index.md), [Farmer](../gathering/index.md)
- **See Also:** [Crafting Progression](../../../systems/crafting/crafting-progression/index.md), [Housing System](../../../systems/world/housing-and-storage/index.md), [Settlement Building](../../../systems/world/settlements/index.md)

## Tags

- `Crafting`
- `Crafting/Woodworking`
- `Carpentry`
- `Construction`
