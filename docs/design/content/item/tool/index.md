---
title: Tools
gdd_ref: systems/core-progression-system-gdd.md#progression
---

# Tools

Tools enable characters to interact with the world productively. Gathering requires tools. Crafting requires tools. Without tools, characters are limited to what their bare hands can accomplish.

## Tool Categories

### Gathering Tools

- **[Pickaxe](pickaxe/)** - Mining tool for ore extraction
- **[Axe](axe/)** - Logging tool for wood harvesting
- **[Knife](knife/)** - Skinning tool for hide processing

### Crafting Tools

- **[Hammer](hammer/)** - Forging tool for metalworking
- **[Tongs](tongs/)** - Forge tool for handling hot metal

## Tool Sources

| Tool    | Made By                                            | Recipe                                                | Used By                                             |
| ------- | -------------------------------------------------- | ----------------------------------------------------- | --------------------------------------------------- |
| Pickaxe | [Blacksmith](../../../classes/crafter/blacksmith/) | [Pickaxe](../../../recipe/blacksmithing/pickaxe/)     | [Miner](../../../classes/gatherer/miner/)           |
| Axe     | [Blacksmith](../../../classes/crafter/blacksmith/) | [Axe](../../../recipe/blacksmithing/axe/)             | [Lumberjack](../../../classes/gatherer/lumberjack/) |
| Knife   | [Blacksmith](../../../classes/crafter/blacksmith/) | (Basic)                                               | [Hunter](../../../classes/gatherer/hunter/)         |
| Hammer  | [Blacksmith](../../../classes/crafter/blacksmith/) | [Basic Hammer](../../../recipe/blacksmithing/hammer/) | [Blacksmith](../../../classes/crafter/blacksmith/)  |
| Tongs   | [Blacksmith](../../../classes/crafter/blacksmith/) | [Basic Tongs](../../../recipe/blacksmithing/hammer/)  | [Blacksmith](../../../classes/crafter/blacksmith/)  |

## Bootstrap Problem

The bootstrap problem: tools require metal, but mining/forging requires tools.

**Solutions:**

1. **Starter Kit** - New characters receive basic tools
2. **Primitive Tools** - Stone pickaxes, wooden mallets (low efficiency)
3. **Trading** - Purchase tools from settlements
