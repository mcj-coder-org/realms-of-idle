---
title: 'Pickaxe'
type: 'item'
category: 'tool'
tier: 1
summary: 'Mining tool for ore extraction'
---

# Pickaxe

## Lore

### Origin

The pickaxe is the miner's partner. One sharp end for breaking rock, one blunt end for driving wedges. Miners spend their lives swinging pickaxes, their muscles adapting to the peculiar rhythm of mining: swing, strike, pull back, swing again. A good pickaxe balances perfectly, becoming an extension of the miner's arm.

### In the World

Every miner owns multiple pickaxes. Primary pick, backup pick, specialized picks for different rock types. Copper picks are adequate for surface mining. Iron picks are needed for deep work. Steel picks are luxuries for master miners.

Pickaxes wear down. The head dulls, the handle loosens, the metal bends. Miners sharpen their picks daily, checking for cracks. A broken pickaxe underground can mean a long climb to the surface with a heavy load of useless ore.

---

## Mechanics

### Requirements

| Requirement | Value                                                 |
| ----------- | ----------------------------------------------------- |
| Crafted By  | [Blacksmith](../../../../classes/crafter/blacksmith/) |
| Recipe      | [Pickaxe](../../../../recipe/blacksmithing/pickaxe/)  |

### Stats

| Property   | Value                   |
| ---------- | ----------------------- |
| Weight     | 3.0                     |
| Durability | Medium                  |
| Damage     | 1d4 (improvised weapon) |

### Materials

| Material | Tier | Effect            |
| -------- | ---- | ----------------- |
| Copper   | 1    | -10% mining speed |
| Iron     | 2    | Standard          |
| Steel    | 3    | +20% mining speed |

## Interactions

| System                                                    | Interaction                      |
| --------------------------------------------------------- | -------------------------------- |
| [Mining](../../../../systems/crafting/gathering/index.md) | Required tool for ore extraction |

---

## Related Content

- **Crafted By:** [Blacksmith](../../../../classes/crafter/blacksmith/)
- **Recipe:** [Pickaxe](../../../../recipe/blacksmithing/pickaxe/)
- **Used By:** [Miner](../../../../classes/gatherer/miner/)
- **Materials:** [Ingot](../processed/ingot/), [Wood Handle](../component/wood-handle/)
