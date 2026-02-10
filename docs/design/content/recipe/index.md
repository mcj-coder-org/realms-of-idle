---
title: 'Recipes'
type: 'category'
category: 'recipe'
summary: 'Instructions for crafting items from materials'
---

# Recipes

Recipes are the knowledge of how to transform raw materials into finished items. Characters learn recipes through training, experimentation, and discovery.

## Categories

- **[Smelting](smelting/)** - Extracting metal from ore
- **[Processing](processing/)** - Preparing components for crafting
- **[Blacksmithing](blacksmithing/)** - Forging metal items

## Recipe Learning

| Method          | Description                                                   |
| --------------- | ------------------------------------------------------------- |
| Training        | Learn from [Crafters](../../classes/crafter/) as class skills |
| Experimentation | Discover through practice                                     |
| Discovery       | Find in the world                                             |

## Recipe Properties

Every recipe includes:

- **Ingredients** - Required materials and quantities
- **Output** - Item produced and quantity
- **Workspace** - Required facility (Forge, etc.)
- **Tools** - Required tools
- **Time** - Crafting duration
- **Difficulty** - Required skill level
- **Tier** - Recipe complexity (Basic, Apprentice, Journeyman, Master, Legendary)

## Workflow

```
[Raw Material] → [Processing Recipe] → [Component]
                                              ↓
[Refined Material] + [Component] → [Crafting Recipe] → [Item]
```

## Related Classes

- **[Crafters](../../classes/crafter/)** - Learn and execute recipes
- **[Gatherers](../../classes/gatherer/)** - Provide raw materials
