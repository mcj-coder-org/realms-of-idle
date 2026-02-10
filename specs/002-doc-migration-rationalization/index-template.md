# Index Page Template

**Feature**: 002-doc-migration-rationalization
**Purpose**: Standard structure for documentation navigation hub pages
**Last Updated**: 2026-02-10

---

## Overview

This template defines the standard structure for `index.md` files throughout the documentation. Index pages serve as navigation hubs, enabling agents and humans to quickly locate relevant content.

**Design Goals**:

- ✅ **Descriptive**: Each item has 1-sentence description (not just link list)
- ✅ **Scannable**: Clear sections with semantic grouping
- ✅ **Progressive**: Links to deeper content, not all details inline
- ✅ **Bidirectional**: Links to related GDD and content
- ✅ **Target**: 200-300 lines per index (not too short, not too long)

---

## Template Structure

```markdown
# [Category] Index

[Brief introduction paragraph: 2-3 sentences explaining what this category contains and its purpose within the documentation]

## Navigation

### [Primary Dimension]

[Group items by primary organization axis - e.g., by tier, by role, by type]

- [Item Name](path/to/item/index.md) - [1 sentence description of what this is and its role]
- [Item Name](path/to/item/index.md) - [1 sentence description]
- ...

### [Secondary Dimension]

[Optional: Alternative grouping for same items - e.g., by tier then by role]

- [Item Name](path/to/item/index.md) - [1 sentence description]
- ...

## Related

[Links to authoritative GDD sections and related content categories]

- [GDD Section](../../systems/[system]/index.md) - Authoritative mechanics specification
- [Related Content](../../content/[category]/index.md) - Examples and implementations
- [Reference Material](../../reference/[topic]/index.md) - Supporting documentation

---

_Index for [category] - Part of [Feature Name]_
```

---

## Concrete Examples

### Example 1: Root Index (docs/design/index.md)

```markdown
# Realms of Idle - Design Documentation

Comprehensive game design documentation for Realms of Idle, including authoritative Game Design Documents (GDD), content examples, and reference materials. This documentation supports both human developers and AI agents through progressive loading architecture.

## Navigation

### Game Design Documents (Authoritative)

Authoritative specifications for all game systems - these are the single source of truth for mechanics.

- [Class System](systems/class-system/index.md) - Character class mechanics, progression, and multi-class system
- [Skill & Recipe System](systems/skill-recipe-system-gdd.md) - Skill acquisition, progression, and recipe discovery
- [Progression System](systems/core-progression-system-gdd.md) - XP distribution, leveling, and character advancement
- [Combat System](systems/combat-system-gdd.md) - Combat mechanics, damage calculation, and status effects
- [Crafting System](systems/crafting-system-gdd.md) - Item creation, material requirements, and quality tiers
- [Economy System](systems/economy-system-gdd.md) - Currency, trading, and market dynamics

### Content Examples (Implementations)

Concrete implementations and examples demonstrating how GDD mechanics are applied.

- [Classes](content/classes/index.md) - All character classes with stats, skills, and progression paths
- [Skills](content/skills/index.md) - Individual skill specifications with effects and requirements
- [Items](content/items/index.md) - Item catalog including weapons, armor, consumables, and materials
- [Creatures](content/creatures/index.md) - NPC and creature definitions with behaviors and loot tables
- [Locations](content/locations/index.md) - World locations with resources, encounters, and quests

### Reference Materials

Supporting documentation for terminology, cross-references, and migration notes.

- [Terminology](reference/terminology/index.md) - Glossary of game terms and their precise definitions
- [Cross-Reference](reference/cross-reference/index.md) - GDD ↔ Content mappings for navigation
- [Migration](reference/migration/index.md) - Documentation migration notes and audit trail

## Documentation Standards

- **Progressive Loading**: Start at index pages, navigate via links (avoid deep paths)
- **Frontmatter**: Check `gdd_ref` for authoritative mechanics
- **GDD vs Content**: systems/ = authoritative, content/ = examples
- **Section IDs**: Use kebab-case (e.g., `#specialization-system`) for GitHub Pages compatibility

---

_Root index for Realms of Idle design documentation_
```

---

### Example 2: Classes Index (docs/design/content/classes/index.md)

```markdown
# Classes Index

Comprehensive catalog of all character classes in Realms of Idle, organized by class tree tier and role. Each class specification includes lore, mechanics, stat progression, associated skills, and example builds.

## Navigation

### Foundation Classes (Tree Tier 1)

Entry-level classes that form the foundation for specializations. Characters typically start with one of these classes.

- [Fighter](fighter/index.md) - Basic combat specialist with balanced offense and defense, foundation for melee combat classes
- [Crafter](crafter/index.md) - Item creation specialist focusing on tools and basic equipment, foundation for crafting classes
- [Gatherer](gatherer/index.md) - Resource collection specialist with foraging and harvesting skills, foundation for gathering classes
- [Rogue](rogue/index.md) - Stealth and agility specialist with lockpicking and trap detection, foundation for subterfuge classes
- [Trader](trader/index.md) - Commerce specialist with bargaining and market access, foundation for economic classes

### Specialization Classes (Tree Tier 2)

Intermediate classes that specialize in specific aspects of their foundation class. Require specific XP thresholds to unlock.

- [Warrior](fighter/warrior/index.md) - Melee combat expert specializing in weapon mastery and sustained combat
- [Blade Dancer](fighter/blade-dancer/index.md) - Agile melee fighter combining speed and precision with dual-wielding expertise
- [Blacksmith](crafter/blacksmith/index.md) - Metal crafting specialist creating weapons and armor with superior quality
- [Alchemist](crafter/alchemist/index.md) - Potion and elixir creator with transmutation abilities
- [Hunter](gatherer/hunter/index.md) - Animal tracking and harvesting specialist with bow proficiency
- [Herbalist](gatherer/herbalist/index.md) - Plant gathering expert with knowledge of medicinal and magical herbs
- [Thief](rogue/thief/index.md) - Expert pickpocket and burglar with stealth mechanics
- [Scout](rogue/scout/index.md) - Reconnaissance specialist with tracking and terrain navigation
- [Merchant](trader/merchant/index.md) - Advanced trader with caravan management and bulk trading

### Advanced Classes (Tree Tier 3)

Elite classes representing mastery in specific domains. Require high XP thresholds and multiple prerequisite classes.

- [Knight](fighter/warrior/knight/index.md) - Elite melee combatant with honor-based progression and leadership abilities
- [Berserker](fighter/warrior/berserker/index.md) - Rage-focused warrior with high-risk, high-reward combat mechanics
- [Weaponsmith](crafter/blacksmith/weaponsmith/index.md) - Master craftsman specializing in legendary weapon creation
- [Armorsmith](crafter/blacksmith/armorsmith/index.md) - Master craftsman specializing in legendary armor creation
- [Ranger](gatherer/hunter/ranger/index.md) - Wilderness expert combining hunting, survival, and nature magic
- [Assassin](rogue/thief/assassin/index.md) - Deadly combatant specializing in critical strikes and poison

## By Role

Alternative organization by primary gameplay role.

### Combat

- [Fighter](fighter/index.md), [Warrior](fighter/warrior/index.md), [Blade Dancer](fighter/blade-dancer/index.md), [Knight](fighter/warrior/knight/index.md), [Berserker](fighter/warrior/berserker/index.md)

### Crafting

- [Crafter](crafter/index.md), [Blacksmith](crafter/blacksmith/index.md), [Alchemist](crafter/alchemist/index.md), [Weaponsmith](crafter/blacksmith/weaponsmith/index.md), [Armorsmith](crafter/blacksmith/armorsmith/index.md)

### Gathering

- [Gatherer](gatherer/index.md), [Hunter](gatherer/hunter/index.md), [Herbalist](gatherer/herbalist/index.md), [Ranger](gatherer/hunter/ranger/index.md)

### Stealth

- [Rogue](rogue/index.md), [Thief](rogue/thief/index.md), [Scout](rogue/scout/index.md), [Assassin](rogue/thief/assassin/index.md)

### Economy

- [Trader](trader/index.md), [Merchant](trader/merchant/index.md)

## Related

- [Class System GDD](../../systems/class-system/index.md) - Authoritative class mechanics, multi-class system, and progression rules
- [Skills](../skills/index.md) - Class-associated skills and abilities
- [Progression System GDD](../../systems/core-progression-system-gdd.md) - XP distribution and leveling mechanics

---

_Index for character classes - Part of 002-doc-migration-rationalization_
```

---

### Example 3: Skills Index (docs/design/content/skills/index.md)

```markdown
# Skills Index

Comprehensive catalog of all skills in Realms of Idle, organized by type and progression. Skills are acquired through class progression, recipe discovery, or achievement unlocks.

## Navigation

### By Type

#### Common Skills

Single-tier skills with consistent effects across all classes.

- [Power Strike](common/power-strike/index.md) - Basic melee attack skill with bonus damage
- [Quick Hands](common/quick-hands/index.md) - Passive skill increasing action speed by 10%
- [Keen Eye](common/keen-eye/index.md) - Passive skill improving gathering yield by 15%
- [Steady Hands](common/steady-hands/index.md) - Passive skill reducing crafting failure chance

#### Tiered Skills

Multi-tier skills that progress from Lesser → Greater → Enhanced as they level up.

- [Weapon Mastery](tiered/weapon-mastery/index.md) - Progressive weapon proficiency with scaling damage bonuses
- [Armor Proficiency](tiered/armor-proficiency/index.md) - Progressive armor expertise reducing encumbrance penalties
- [Crafting Precision](tiered/crafting-precision/index.md) - Progressive crafting quality improvements
- [Gathering Efficiency](tiered/gathering-efficiency/index.md) - Progressive gathering speed and yield bonuses

#### Mechanic Unlock Skills

Skills that unlock game mechanics when acquired.

- [Recipe Discovery](mechanic-unlock/recipe-discovery/index.md) - Unlocks crafting recipe system
- [Market Access](mechanic-unlock/market-access/index.md) - Unlocks trading interface and auction house
- [Tool Bond](mechanic-unlock/tool-bond/index.md) - Unlocks tool enhancement system
- [Spirit Pool Access](mechanic-unlock/spirit-pool-access/index.md) - Unlocks mana and spellcasting system

#### Toggled Skills

Skills that can be enabled/disabled with persistent resource drain.

- [Combat Stance](toggled/combat-stance/index.md) - Toggleable stance affecting attack/defense balance
- [Crafting Focus](toggled/crafting-focus/index.md) - Toggleable mode improving quality at reduced speed
- [Stealth Mode](toggled/stealth-mode/index.md) - Toggleable invisibility with stamina drain

#### Triggered Skills

Active skills requiring manual activation with cooldowns.

- [Shield Bash](triggered/shield-bash/index.md) - Active melee stun with 10-second cooldown
- [Healing Potion](triggered/healing-potion/index.md) - Consumable heal with 30-second cooldown
- [Sprint](triggered/sprint/index.md) - Movement speed burst with stamina cost

### By Class Synergy

Skills organized by which classes gain bonus XP or enhanced effects.

#### Fighter Synergies

- [Weapon Mastery](tiered/weapon-mastery/index.md), [Power Strike](common/power-strike/index.md), [Shield Bash](triggered/shield-bash/index.md)

#### Crafter Synergies

- [Crafting Precision](tiered/crafting-precision/index.md), [Steady Hands](common/steady-hands/index.md), [Tool Bond](mechanic-unlock/tool-bond/index.md)

#### Gatherer Synergies

- [Gathering Efficiency](tiered/gathering-efficiency/index.md), [Keen Eye](common/keen-eye/index.md)

## Related

- [Skill & Recipe System GDD](../../systems/skill-recipe-system-gdd.md) - Authoritative skill mechanics, acquisition, and progression
- [Classes](../classes/index.md) - Class specifications with associated skills
- [Recipes](../recipes/index.md) - Crafting recipes unlocked by skills

---

_Index for skills - Part of 002-doc-migration-rationalization_
```

---

## Writing Guidelines

### Introduction Paragraph (2-3 sentences)

✅ **Good**:

```
Comprehensive catalog of all character classes in Realms of Idle, organized by
class tree tier and role. Each class specification includes lore, mechanics,
stat progression, associated skills, and example builds.
```

❌ **Bad** (too brief):

```
All character classes.
```

❌ **Bad** (too verbose):

```
This comprehensive and exhaustive catalog contains all of the character classes
that exist within the Realms of Idle game world. Each class has been carefully
designed to provide unique gameplay experiences and strategic options. Players
can choose from a wide variety of classes spanning multiple tiers and roles...
```

---

### Item Descriptions (1 sentence)

✅ **Good**:

```
- [Warrior](warrior/index.md) - Melee combat expert specializing in weapon
  mastery and sustained combat
```

**Why**: Concise, specific, explains role and distinguishing features

❌ **Bad** (no description):

```
- [Warrior](warrior/index.md)
```

❌ **Bad** (too long):

```
- [Warrior](warrior/index.md) - A powerful melee combat class that focuses on
  weapon mastery skills and sustained combat situations. Warriors are known for
  their high health pools and defensive capabilities, making them ideal for
  frontline combat roles in both solo and group scenarios.
```

---

### Section Headings

Use **semantic groupings**:

✅ **Good**: "Foundation Classes (Tree Tier 1)"
✅ **Good**: "By Role" (alternative organization)
✅ **Good**: "Common Skills" (skill type)

❌ **Bad**: "Group 1" (meaningless)
❌ **Bad**: "Miscellaneous" (avoid catch-all)

---

### Related Section

Always include:

- **Link to authoritative GDD** (where mechanics are specified)
- **Links to related content** (cross-category navigation)
- **No more than 5 links** (keep focused)

✅ **Good**:

```markdown
## Related

- [Class System GDD](../../systems/class-system/index.md) - Authoritative class mechanics
- [Skills](../skills/index.md) - Class-associated skills
- [Progression System GDD](../../systems/core-progression-system-gdd.md) - XP mechanics
```

---

## Length Guidelines

| Index Type        | Target Length | Min | Max |
| ----------------- | ------------- | --- | --- |
| Root index        | 250 lines     | 200 | 300 |
| Category index    | 250 lines     | 200 | 300 |
| Subcategory index | 200 lines     | 150 | 250 |

**If too short**: Add more grouping dimensions (e.g., "By Role" in addition to "By Tier")
**If too long**: Split into subcategories with their own indexes

---

## Validation Checklist

Before committing an index page, verify:

- [ ] Introduction paragraph (2-3 sentences) clearly explains category
- [ ] Navigation section with semantic groupings
- [ ] Every item has 1-sentence description
- [ ] Related section links to GDD and related content
- [ ] Length within target range (200-300 lines for major indexes)
- [ ] All links use relative paths from index location
- [ ] Frontmatter includes `title` (no `gdd_ref` for indexes)

---

## Frontmatter for Index Pages

Index pages use **minimal frontmatter** (no `gdd_ref` since they're not content):

```yaml
---
title: Classes Index
---
```

**Rationale**: Indexes are navigation tools, not content implementations. They don't need to reference GDD sections.

---

## Automation Support

The `generate-indexes.sh` script uses this template to create indexes:

- **Intro paragraph**: Generated based on directory name and inferred category
- **Navigation items**: Extracted from subdirectories/files with descriptions from frontmatter or first paragraph
- **Related section**: Inferred from category (e.g., classes → class-system GDD)

**Manual refinement**: Generated indexes should be reviewed and enhanced with better descriptions and groupings.

---

_Index page template for 002-doc-migration-rationalization feature_
