---
title: Content Libraries
type: index
summary: Index of all game content definitions
---

# Content Libraries

This section contains **what** exists in the game - specific classes, skills, items, creatures, and races.

## Libraries

| Library                               | Description                       | Status      |
| ------------------------------------- | --------------------------------- | ----------- |
| [Classes](classes/index.md)           | Character class definitions       | In Progress |
| [Skills](skills/index.md)             | Common and class-specific skills  | In Progress |
| [Materials](materials/index.md)       | Crafting materials by type        | Stub        |
| [Items](items/index.md)               | Weapons, armor, tools, containers | Stub        |
| [Recipes](recipes/index.md)           | Crafting recipes by specialty     | Stub        |
| [Enchantments](enchantments/index.md) | Enchantment definitions           | Stub        |
| [Creatures](creatures/index.md)       | Wildlife, monsters, bosses        | In Progress |
| [Races](races/index.md)               | Playable races and tribes         | In Progress |

## Content Status Legend

| Status      | Meaning                           |
| ----------- | --------------------------------- |
| Complete    | All entries defined               |
| In Progress | Core entries defined, expanding   |
| Stub        | Structure exists, entries pending |

## Adding Content

Content files use this frontmatter format:

```yaml
---
title: Entry Name
type: class|skill|material|item|recipe|enchantment|creature|race
summary: One-line description
---
```

Each library has its own index with specific fields relevant to that content type.
