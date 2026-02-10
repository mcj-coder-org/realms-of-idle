# Systems Index

This index maps game systems to their documentation and related content.

## Purpose

- **System lookup**: Find documentation for any game system
- **Content mapping**: See which content relates to each system
- **Cross-reference**: Navigate between systems and content
- **Architecture**: Understand system relationships

## Core Game Systems

### Character Systems

| System                 | Documentation                         | Related Content              | Tags            |
| ---------------------- | ------------------------------------- | ---------------------------- | --------------- |
| **Character System**   | [Character](../../systems/character/) | Classes, Skills, Progression | `Character/*`   |
| **Class System**       | (TODO: System documentation pending)  | All classes                  | `Class/*`       |
| **Progression**        | (TODO: System documentation pending)  | XP, Levels, Ranks            | `Progression/*` |
| **Personality Traits** | (TODO: System documentation pending)  | NPC behavior                 | `Personality/*` |

### Combat Systems

| System             | Documentation                        | Related Content               | Tags             |
| ------------------ | ------------------------------------ | ----------------------------- | ---------------- |
| **Combat System**  | [Combat](../../systems/combat/)      | Weapons, Armor, Combat Skills | `Combat/*`       |
| **Damage System**  | (TODO: System documentation pending) | Health, Wounds                | `Combat/Damage`  |
| **Morale System**  | (TODO: System documentation pending) | Cozy combat mechanics         | `Combat/Morale`  |
| **Defense System** | (TODO: System documentation pending) | Shields, Armor, Dodge         | `Combat/Defense` |

### Crafting Systems

| System              | Documentation                                          | Related Content                        | Tags                   |
| ------------------- | ------------------------------------------------------ | -------------------------------------- | ---------------------- |
| **Crafting System** | [Crafting](../../systems/crafting/)                    | Recipes, Materials, Stations           | `Crafting/*`           |
| **Recipe System**   | (TODO: System documentation pending)                   | All recipes                            | `Recipe/*`             |
| **Quality System**  | (TODO: System documentation pending)                   | Item quality tiers                     | `Crafting/Quality`     |
| **Station System**  | [Tags Index](tags-index.md#stations)                   | Crafting stations, tag-based access    | `Crafting/*/Stations`  |
| **Workshop System** | [Tags Index](tags-index.md#workshop-and-station-model) | Workshop buildings, multiclass support | See settlement systems |

### Economic Systems

| System             | Documentation                        | Related Content              | Tags              |
| ------------------ | ------------------------------------ | ---------------------------- | ----------------- |
| **Economy System** | [Economy](../../systems/economy/)    | Trading, Merchants, Currency | `Economy/*`       |
| **Pricing System** | (TODO: System documentation pending) | Item values, Trade           | `Economy/Pricing` |
| **Trade System**   | (TODO: System documentation pending) | Merchants, Markets           | `Economy/Trade`   |

### Gathering Systems

| System               | Documentation                        | Related Content                | Tags          |
| -------------------- | ------------------------------------ | ------------------------------ | ------------- |
| **Gathering System** | (TODO: System documentation pending) | Resources, Nodes, Regeneration | `Gathering/*` |
| **Resource System**  | (TODO: System documentation pending) | Materials, Ores, Herbs         | `Material/*`  |

### Magic Systems

| System                 | Documentation                        | Related Content           | Tags                |
| ---------------------- | ------------------------------------ | ------------------------- | ------------------- |
| **Magic System**       | [Magic](../../systems/magic/)        | Spells, Mana, Enchantment | `Magic/*`           |
| **Enchantment System** | (TODO: System documentation pending) | Enchanted items           | `Magic/Enchantment` |
| **Spell System**       | (TODO: System documentation pending) | Spell lists, Effects      | `Magic/Spell`       |

### Settlement Systems

| System                | Documentation                                          | Related Content                        | Tags                    |
| --------------------- | ------------------------------------------------------ | -------------------------------------- | ----------------------- |
| **Settlement System** | (TODO: System documentation pending)                   | Buildings, Population                  | `Settlement/*`          |
| **Building System**   | (TODO: System documentation pending)                   | Workshops, Homes                       | `Settlement/Building`   |
| **Workshop System**   | [Tags Index](tags-index.md#workshop-and-station-model) | Workshop buildings, multiclass support | See settlement systems  |
| **Population System** | (TODO: System documentation pending)                   | NPCs, Families                         | `Settlement/Population` |

### NPC Systems

| System                | Documentation                        | Related Content     | Tags            |
| --------------------- | ------------------------------------ | ------------------- | --------------- |
| **NPC System**        | (TODO: System documentation pending) | AI, Behavior, Needs | `NPC/*`         |
| **Needs System**      | (TODO: System documentation pending) | NPC motivations     | `NPC/Needs`     |
| **AI System**         | (TODO: System documentation pending) | NPC decisions       | `NPC/AI`        |
| **Daily Life System** | (TODO: System documentation pending) | NPC schedules       | `NPC/DailyLife` |

### World Systems

| System               | Documentation                        | Related Content          | Tags              |
| -------------------- | ------------------------------------ | ------------------------ | ----------------- |
| **World System**     | (TODO: System documentation pending) | Time, Weather, Geography | `World/*`         |
| **Time System**      | (TODO: System documentation pending) | Days, Seasons, Calendar  | `World/Time`      |
| **Weather System**   | (TODO: System documentation pending) | Climate effects          | `World/Weather`   |
| **Geography System** | (TODO: System documentation pending) | Regions, Travel          | `World/Geography` |

### Inventory Systems

| System               | Documentation                        | Related Content            | Tags                  |
| -------------------- | ------------------------------------ | -------------------------- | --------------------- |
| **Inventory System** | (TODO: System documentation pending) | Items, Storage, Containers | `Inventory/*`         |
| **Item System**      | (TODO: System documentation pending) | All items                  | `Item/*`              |
| **Container System** | (TODO: System documentation pending) | Storage, Bags              | `Inventory/Container` |

## Content to System Mapping

### Items

- **Weapons** → Combat System, Crafting System
- **Armor** → Combat System, Crafting System
- **Tools** → Crafting System, Gathering System
- **Materials** → Crafting System, Gathering System
- **Consumables** → Economy System, Crafting System

### Classes

- **Combat Classes** → Combat System, Character System
- **Crafting Classes** → Crafting System, Character System
- **Gathering Classes** → Gathering System, Character System
- **Magic Classes** → Magic System, Character System

### Recipes

- **Crafting Recipes** → Crafting System, Economy System
- **Alchemy Recipes** → Magic System, Crafting System
- **Cooking Recipes** → Economy System, Crafting System

## System Dependencies

```
Character System (Foundation)
    ├─> Class System
    ├─> Progression System
    ├─> Combat System
    ├─> Crafting System
    ├─> Magic System
    └─> Gathering System

Economy System
    ├─> Crafting System (depends on)
    ├─> Gathering System (depends on)
    └─> Trading System

Settlement System
    ├─> NPC System
    ├─> Building System
    └─> Population System

World System
    ├─> Time System (affects all)
    ├─> Weather System (affects gathering/combat)
    └─> Geography System (travel/resources)
```

## How to Use This Index

### For Content Creators

1. **Find system**: Locate system documentation
2. **Related content**: See which content uses each system
3. **Cross-reference**: Navigate from systems to content
4. **Understand dependencies**: See how systems interact

### For Developers

1. **System lookup**: Find system architecture documentation
2. **Content mapping**: Understand which content uses systems
3. **Dependencies**: Track system relationships
4. **Implementation**: Match code to system design

### For Auditors

1. **Cross-reference**: Verify content matches system docs
2. **Completeness**: Check all systems are documented
3. **Consistency**: Ensure system terminology matches
4. **Coverage**: Track undocumented systems

## Maintenance

This index should be updated when:

- New systems are designed
- System documentation is added/modified
- Content is added/removed
- System relationships change

**Update process:**

```bash
# Systems documentation location
docs/design/systems/

# When adding new systems:
# 1. Create system documentation
# 2. Add entry to this index
# 3. Map related content
# 4. Document dependencies
```

## Related Documentation

- (TODO: System documentation pending)
- Architecture Documentation (TODO: Create architecture documentation directory and docs)
- [Classes Index](classes-index.md)
- [Tags Index](tags-index.md)

---

**Last Updated:** 2026-01-28
**Maintained By:** Content Curator persona
**Source:** System documentation in `docs/design/systems/`
