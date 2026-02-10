---
title: 'Smelting Recipes'
type: 'category'
category: 'recipe'
summary: 'Extracting metal from ore'
---

# Smelting

Smelting extracts pure metal from ore using heat and fuel. The process separates valuable metal from worthless stone, producing ingots ready for forging.

## Smelting Recipes

- **[Smelt Ore](smelt-ore/)** - Generic recipe for any metal ore

## Requirements

| Requirement | Value                             |
| ----------- | --------------------------------- |
| Workspace   | Forge                             |
| Fuel        | [Coal](../../material/fuel/coal/) |
| Skill       | Basic Smelting                    |

## Outputs

| Input                                                          | Output                               | Byproduct |
| -------------------------------------------------------------- | ------------------------------------ | --------- |
| [Ore](../../item/raw/ore/) + [Coal](../../material/fuel/coal/) | [Ingot](../../item/processed/ingot/) | Slag      |

## Related Classes

- **[Blacksmith](../../classes/crafter/blacksmith/)** - Performs smelting
- **[Miner](../../classes/gatherer/miner/)** - Provides ore
