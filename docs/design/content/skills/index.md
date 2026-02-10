---
title: Skills Library
type: index
summary: All skill definitions organized by type with hierarchical tag-based access
---

# Skills Library

Skills are earned at class level-up. Common skills can be earned by any class; class-specific skills benefit from synergies with appropriate classes through the hierarchical tag system.

## Structure

Skills are organized by mechanical type with hierarchical tags determining access:

- **Common Skills**: Universal skills in `/common/` directory (60+ individual files)
- **Type-Based Organization**: Skills organized in directories by type (`/tiered/`, `/mechanic-unlock/`, `/passive-generator/`, `/cooldown/`, etc.)
- **Tag-Based Access**: Skills tagged hierarchically (e.g., `Crafting/Smithing/Weapon`) with inheritance-based access
- **Overview Documents**: High-level summaries in `common-skills.md` and `class-skills.md`

## Hierarchical Tag System

Skills use hierarchical tags to organize access and determine synergies:

- **Tag Format**: `Category/Subcategory/Specialization` (up to 3 levels)
- **Inheritance Access**: Classes with specific tags access skills at that level OR any parent level
- **Multi-Tagging**: Skills can have multiple tags from different categories
- **16 Tag Categories**: Combat, Crafting, Gathering, Magic, Stealth, Leadership, Knowledge, Trade, Criminal, Agriculture, Nature, Physical, Social, Tribal, Economy, Universal

See [Hierarchical Skill Tag System](../../systems/character/skill-tags/index.md) for complete tag specifications.

## Acquisition Paths

Skills can be acquired through three methods:

1. **Level-Up Awards**: Offered at class level-up based on tag synergies
2. **Training**: Direct skill development (guided or unguided)
3. **Action-Based**: Performing skill-related actions accumulates progress

See [Class-Specific Skills](class-skills/index.md#2-skill-progression-mechanics) for detailed mechanics.

## Synergy Model

Class-specific skills use a tag-based synergy model:

- **Strong Synergy** (Matching tags): 2x XP, scaled bonuses (e.g., Blacksmith with `Crafting/Smithing` tag)
- **Moderate Synergy** (Parent tag match): 1.5x XP, moderate bonuses (e.g., Crafter with `Crafting` tag)
- **No Synergy** (No tag match): 1x XP, no bonuses

Any character can learn any skill - tags enhance learning speed but don't restrict access.

## Common Skills

Universal skills available to all classes - see [Common Skills Overview](common-skills/index.md) for mechanics.

### Common Skill Categories

Individual skill files organized by category in `/common/` directory:

| Category   | Example Skills               | File Count |
| ---------- | ---------------------------- | ---------- |
| Survival   | Second Wind, Fortitude       | 9 skills   |
| Combat     | Power Strike, Quick Reflexes | 9 skills   |
| Awareness  | Keen Senses, Danger Sense    | 7 skills   |
| Social     | Haggler, Persuasive          | 7 skills   |
| Movement   | Swift, Climber, Pathfinder   | 8 skills   |
| Crafting   | Careful Hands, Lucky Find    | 8 skills   |
| Utility    | Pack Mule, Quick Hands       | 8 skills   |
| Leadership | Inspiring Presence           | 4 skills   |

**Browse**: See [Common Skills Directory](common/) for all individual skill files.

## Class-Specific Skills by Type

Synergy-based skills organized by mechanical type - see [Class-Specific Skills Overview](class-skills/index.md) for mechanics.

### Skill Types

Skills are organized in type-specific directories:

| Type              | Description                                    | Directory                                | Count |
| ----------------- | ---------------------------------------------- | ---------------------------------------- | ----- |
| Tiered            | Progressive power scaling through tiers        | [tiered/](tiered/)                       | 38    |
| Mechanic Unlock   | Binary abilities that unlock new mechanics     | [mechanic-unlock/](mechanic-unlock/)     | 56    |
| Passive Generator | Generate resources/opportunities over time     | [passive-generator/](passive-generator/) | 8     |
| Cooldown          | Active abilities with recharge periods         | [cooldown/](cooldown/)                   | 3     |
| Duration          | Active effects lasting for time period         | [duration/](duration/)                   | 1     |
| Toggle            | On/off states consuming resources while active | [toggle/](toggle/)                       | 0     |
| Charges           | Limited uses per rest/day                      | [charges/](charges/)                     | 0     |
| Passive           | Always active, no trigger required             | [passive/](passive/)                     | 14    |
| Multi-Tier Unlock | Mechanic unlocks with progressive tiers        | [mechanic-unlock/](mechanic-unlock/)     | 5     |

**Total Class-Specific Skills**: 111 (migrated to type-based organization)

**Browse**: Each type directory contains individual skill files organized by tags.

### Skills by Tag Category

See [Hierarchical Skill Tag System](../../systems/character/skill-tags/index.md) for complete listings by tag:

| Tag Category | Skill Count | Primary Classes               | Notable Skills                                         |
| ------------ | ----------- | ----------------------------- | ------------------------------------------------------ |
| Combat       | 24          | Warrior, Knight, Assassin     | Weapon Mastery, Battle Rage, Tactical Assessment       |
| Crafting     | 35          | Blacksmith, Alchemist, Tailor | Masterwork Chance, Metal Sense, Potent Brews           |
| Gathering    | 17          | Miner, Forager, Hunter        | Resource Sense, Deep Vein, Master Forager              |
| Magic        | 26          | Mage, Enchanter, Shaman       | Spell Focus, Ritual Casting, Spirit Binding            |
| Stealth      | 11          | Thief, Assassin, Scout        | Shadow Step, Lockmaster, Silent Kill                   |
| Leadership   | 13          | Knight, Mayor, Guild Master   | Authority, Inspiring Leader, Delegation                |
| Knowledge    | 13          | Scholar, Trainer              | Ancient Knowledge, Perfect Memory, Teaching            |
| Trade        | 14          | Merchant, Trader, Caravaner   | Market Sense, Silver Tongue, Commodity Speculation     |
| Criminal     | 14          | Thief, Assassin, Fence        | Evidence Elimination, False Identity, Contract Killing |
| Agriculture  | 10          | Farmer, Rancher               | Green Thumb, Animal Husbandry, Surplus Generator       |
| Nature       | 7           | Hunter, Ranger, Shaman        | Beast Tongue, Creature Lore, Spirit Communion          |
| Physical     | 5           | Warrior, Miner                | Ambidexterity, Battle Hardened, Evasion Expert         |
| Social       | 7           | Merchant, Leader, Noble       | Inspiring Leader, Silver Tongue, Misdirection          |
| Tribal       | 8           | Shaman, Witch Doctor          | War Painter, Totem Crafting, Fetish Creation           |
| Economy      | 4           | Merchant, Banker              | Market Sense, Commodity Speculation                    |
| Universal    | 5           | All classes                   | Night Vision, Iron Will, Water Breathing               |

## Class-to-Tag Mappings

Classes receive specific hierarchical tags that determine which skills they have synergies with:

- [Class Tag Associations](../../systems/character/class-tag-associations/index.md) - Complete mapping of classes to tags
- [Racial Synergies](../../systems/character/racial-synergies/index.md) - Racial affinity system and bonus tags

**Example**: A Blacksmith receives `Crafting/Smithing`, `Crafting`, and `Physical/Strength` tags, granting strong synergies with all smithing skills and moderate synergies with general crafting skills.

## Skill Mechanics Reference

See [Skills Overview](../../systems/character/skills-overview/index.md) for:

- Skill types (Passive, Cooldown, Toggle, etc.)
- Tier progression (Lesser → Greater → Enhanced)
- XP-based continuous improvement
- Scaling formulas
- Acquisition thresholds

## System Documentation

- [Hierarchical Skill Tag System](../../systems/character/skill-tags/index.md) - Complete tag specifications
- [REFACTOR-AUDIT.md](REFACTOR-AUDIT/index.md) - Original audit and migration documentation
- [Class-Specific Skills](class-skills/index.md) - Mechanics and progression
- [Common Skills](common-skills/index.md) - Universal skill overview
