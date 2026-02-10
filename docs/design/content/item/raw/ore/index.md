---
title: 'Ore'
type: 'item'
category: 'raw'
tier: 'variable'
summary: 'Metal-bearing rock from mining nodes'
---

# Ore

## Lore

### Origin

Ore is rock that contains valuable metals. To the untrained eye, it looks like ordinary stone - perhaps a bit heavier, perhaps with colorful streaks. But miners learn to read the signs: certain rock formations, specific colorations, the weight of a stone in hand. Copper ore glints green-blue. Iron ore appears rusty red. These are the earth's treasures, hidden in plain sight.

### In the World

Miners spend hours breaking rock, sorting ore from worthless stone. A good copper vein might yield twenty pounds of ore from a hundred pounds of rock. Iron is rarer still. The ore is hauled to the surface, sorted by type and quality, then sold to smiths or smelted on-site.

Ore itself is useless for forging. It must be smelted - heated until the metal separates from the stone. This requires fuel (coal), heat (forge), and knowledge. A smelter who rushes the process produces slag, not metal. Patience separates metal from stone.

---

## Mechanics

### Requirements

| Requirement     | Value                                        |
| --------------- | -------------------------------------------- |
| Gathering Class | [Miner](../../../../classes/gatherer/miner/) |
| Tool Required   | Pickaxe                                      |

### Stats

| Property   | Value                |
| ---------- | -------------------- |
| Stackable  | Yes                  |
| Weight     | 2.0 per unit         |
| Base Value | Varies by metal type |

### Variants

| Ore Type   | Tier | Source           | Output       |
| ---------- | ---- | ---------------- | ------------ |
| Copper Ore | 1    | Surface nodes    | Copper Ingot |
| Iron Ore   | 2    | Deep nodes       | Iron Ingot   |
| Coal       | 0    | Various deposits | Fuel         |

## Interactions

| System                                                                 | Interaction                                                            |
| ---------------------------------------------------------------------- | ---------------------------------------------------------------------- |
| [Smelting](../../../../systems/crafting/crafting-progression/index.md) | Required input for [Smelt Ore](../../../../recipe/smelting/smelt-ore/) |

---

## Related Content

- **Gathered By:** [Miner](../../../../classes/gatherer/miner/)
- **Processed By:** [Smelt Ore](../../../../recipe/smelting/smelt-ore/)
- **Produces:** [Ingot](../processed/ingot/)
- **Material Types:** [Copper](../../../../material/metal/copper/), [Iron](../../../../material/metal/iron/)
