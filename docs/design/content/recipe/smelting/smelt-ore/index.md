---
title: 'Smelt Ore'
type: 'recipe'
category: 'smelting'
tier: 1
summary: 'Extract metal from ore using heat and fuel'
---

# Smelt Ore

## Overview

Transform raw ore into pure metal ingots through heat and refinement. This is the foundation of all metalworking.

---

## Mechanics

### Requirements

| Requirement | Value                                                            |
| ----------- | ---------------------------------------------------------------- |
| Class       | [Blacksmith](../../../../classes/crafter/blacksmith/) (Level 1+) |
| Workspace   | Forge                                                            |
| Fuel        | [Coal](../../../../material/fuel/coal/)                          |
| Time        | 2 hours per batch                                                |

### Ingredients

| Ingredient                              | Quantity | Notes                      |
| --------------------------------------- | -------- | -------------------------- |
| `<Material/Metal/*>` Ore                | 3 units  | Copper Ore, Iron Ore, etc. |
| [Coal](../../../../material/fuel/coal/) | 1 unit   | Required fuel              |

### Output

| Material                   | Quantity | Variants               |
| -------------------------- | -------- | ---------------------- |
| `<Material/Metal/*>` Ingot | 1 unit   | Matches input ore type |

| Input Ore  | Output Ingot |
| ---------- | ------------ |
| Copper Ore | Copper Ingot |
| Iron Ore   | Iron Ingot   |

### Byproducts

| Byproduct | Quantity | Chance |
| --------- | -------- | ------ |
| Slag      | 1 unit   | 100%   |

### Stats

| Property     | Value |
| ------------ | ----- |
| Difficulty   | Basic |
| Stamina Cost | 10    |
| Base XP      | 15    |
| Failure Rate | 5%    |

## Process

1. **Prepare Forge** - Start coal fire, bring to temperature
2. **Add Ore** - Place ore in crucible or on hearth
3. **Maintain Heat** - Keep fire hot for 2 hours
4. **Separate Metal** - Pour molten metal into ingot mold
5. **Cool** - Let ingots solidify
6. **Store** - Stack ingots for forging

## Related Classes

- **[Blacksmith](../../../../classes/crafter/blacksmith/)** - Executes recipe
- **[Miner](../../../../classes/gatherer/miner/)** - Gathers ore

## Related Content

- **Input:** [Ore](../../../../item/raw/ore/), [Coal](../../../../material/fuel/coal/)
- **Output:** [Ingot](../../../../item/processed/ingot/)
- **Used In:** All [Blacksmithing](../blacksmithing/) recipes
