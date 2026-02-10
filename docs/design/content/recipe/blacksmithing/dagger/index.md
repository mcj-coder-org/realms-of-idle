---
title: 'Dagger'
type: 'recipe'
category: 'blacksmithing'
tier: 2
summary: 'Forge a short blade for close combat'
---

# Dagger

## Overview

Forge a dagger from metal ingots, with leather grip and wooden core. A versatile weapon and tool.

---

## Mechanics

### Requirements

| Requirement | Value                                                             |
| ----------- | ----------------------------------------------------------------- |
| Class       | [Blacksmith](../../../../classes/crafter/blacksmith/) (Level 10+) |
| Workspace   | Forge, Anvil                                                      |
| Tools       | Hammer, Tongs                                                     |
| Time        | 4 hours                                                           |

### Ingredients

| Ingredient                                                 | Quantity | Notes             |
| ---------------------------------------------------------- | -------- | ----------------- |
| `<Material/Metal/*>` Ingot                                 | 2 units  | Copper/Iron/Steel |
| [Leather Strip](../../../../item/component/leather-strip/) | 1 unit   | Grip wrapping     |
| [Wood Handle](../../../../item/component/wood-handle/)     | 1 unit   | Grip core         |

### Output

| Item                        | Quantity | Variants            |
| --------------------------- | -------- | ------------------- |
| `<Material/Metal/*>` Dagger | 1 unit   | Matches input metal |

| Input Ingot  | Output Dagger | Damage |
| ------------ | ------------- | ------ |
| Copper Ingot | Copper Dagger | 1d4    |
| Iron Ingot   | Iron Dagger   | 1d4    |
| Steel Ingot  | Steel Dagger  | 1d6    |

### Stats

| Property        | Value                |
| --------------- | -------------------- |
| Difficulty      | Apprentice           |
| Stamina Cost    | 15                   |
| Base XP         | 25                   |
| Failure Rate    | 15%                  |
| Failure Salvage | Scrap Metal (1 unit) |

## Process

1. **Heat Metal** - Bring ingots to forging temperature
2. **Draw Out Blade** - Hammer metal into blade shape
3. **Form Tang** - Create extension for handle attachment
4. **Sharpen Edge** - Grind blade edge to sharpness
5. **Attach Handle** - Fit wood handle to tang
6. **Wrap Grip** - Wrap leather strip around handle
7. **Quench** - Heat treat blade
8. **Finish** - Final sharpening and polish

## Related Classes

- **[Blacksmith](../../../../classes/crafter/blacksmith/)** - Forges dagger

## Related Content

- **Inputs:** [Ingot](../../../../item/processed/ingot/), [Leather Strip](../../../../item/component/leather-strip/), [Wood Handle](../../../../item/component/wood-handle/)
- **Output:** [Dagger](../../../../item/weapon/dagger/)
- **Materials:** [Copper](../../../../material/metal/copper/), [Iron](../../../../material/metal/iron/)
