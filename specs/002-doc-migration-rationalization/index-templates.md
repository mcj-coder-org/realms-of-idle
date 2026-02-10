# Index Page Templates

**Feature**: 002-doc-migration-rationalization
**Phase**: Phase 1 (Design Artifacts)
**Date**: 2026-02-10

---

## Overview

This document provides templates for the three types of index pages used in the documentation structure. Index pages enable progressive loading by providing rich navigation hubs that answer 80% of queries without requiring detail page reads.

**Three Index Types**:

1. **System-Overview** - Top-level navigation for content categories
2. **Category-Index** - Mid-level navigation for subcategories
3. **Detail-Page** - Individual entity documentation (for reference, not technically an "index")

---

## Template 1: System-Overview Index

**Purpose**: Top-level navigation for a major content category (classes, actions, skills, items)

**Location Pattern**: `docs/design/content/{category}/index.md`

**Example**: `docs/design/content/classes/index.md`

### Template

```markdown
---
title: '[Category] Index'
type: 'system-overview'
summary: 'Brief one-line description of the category'
---

# [Category] Index

## Overview

[1-2 paragraph introduction explaining what this category contains and how it's organized]

## Navigation

### By [Primary Dimension]

Complete list organized by primary classification (tier, role, domain, etc.):

| [Item]                          | Summary           | [Key Attribute 1] | [Key Attribute 2] | [Key Attribute 3] |
| ------------------------------- | ----------------- | ----------------- | ----------------- | ----------------- |
| [[Name]](path/to/item/index.md) | Brief description | Value             | Value             | Value             |
| [[Name]](path/to/item/index.md) | Brief description | Value             | Value             | Value             |

### By [Secondary Dimension] (optional)

Alternative organization (by usage, by acquisition, by tags):

- **[Group 1]**: [Items] - Description
- **[Group 2]**: [Items] - Description

## Quick Selection Guide

**For [Use Case A]**: Recommendation with reasoning
**For [Use Case B]**: Recommendation with reasoning
**For [Use Case C]**: Recommendation with reasoning

## Common Patterns

[Description of frequently used combinations or workflows]:

- [Pattern 1]: Brief explanation
- [Pattern 2]: Brief explanation

## Related Content

- [Authoritative GDD](../systems/[system]/index.md) - Mechanics reference
- [Related Category](../content/[category]/index.md) - Examples
```

### Concrete Example: Classes Index

```markdown
---
title: 'Character Classes'
type: 'system-overview'
summary: 'Complete class system with acquisition, evolution, and tag requirements'
---

# Character Classes

## Overview

Classes define character progression paths and determine which actions grant XP. Characters can learn multiple classes simultaneously (all active), with the highest-leveled Tier 2 class determining their primary identity. Classes do NOT provide bonuses - skills provide all bonuses (speed, efficiency, quality).

## Navigation

### By Tree Tier

Complete class listing organized by specialization level:

| Class                                                 | Tree Tier          | Acquisition    | Tracked Tags       | Evolves To                |
| ----------------------------------------------------- | ------------------ | -------------- | ------------------ | ------------------------- |
| [Fighter](fighter/index.md)                           | 1 (Foundation)     | Starting class | combat             | Warrior, Archer, Guardian |
| [Warrior](fighter/warrior/index.md)                   | 2 (Specialization) | 100 combat XP  | combat.melee       | Knight, Berserker         |
| [Blade Dancer](fighter/warrior/blade-dancer/index.md) | 3 (Advanced)       | Warrior Lv.10  | combat.melee.sword | Blade Master              |

### By Role

Alternative organization by gameplay role:

- **Damage Dealers**: Warrior, Archer, Mage - Front-line and ranged DPS
- **Defenders**: Knight, Guardian - Tank and protection roles
- **Supporters**: Cleric, Bard - Healing and buffs
- **Crafters**: Blacksmith, Cook, Alchemist - Item creation
- **Gatherers**: Miner, Lumberjack, Farmer - Resource collection
- **Service**: Innkeeper, Server, Housekeeper - Hospitality management

## Quick Selection Guide

**For New Players**: Start with Foundation classes (Fighter, Crafter, Gatherer) - easy to unlock
**For Combat Focus**: Warrior → Knight progression for melee, Archer → Hunter for ranged
**For Crafting Focus**: Blacksmith for weapons/armor, Cook for consumables, Alchemist for potions
**For Service/Economy**: Innkeeper for management, Server/Housekeeper for specialists

## Common Patterns

**Combat Progression**:

- Fighter (Tier 1) → Warrior (Tier 2) → Knight (Tier 3) - Defensive melee specialist
- Fighter (Tier 1) → Warrior (Tier 2) → Berserker (Tier 3) - Offensive melee specialist

**Crafting Progression**:

- Crafter (Tier 1) → Blacksmith (Tier 2) → Weaponsmith (Tier 3) - Weapon specialization
- Crafter (Tier 1) → Blacksmith (Tier 2) → Armorsmith (Tier 3) - Armor specialization

**Multi-Class Synergies**:

- Warrior + Blacksmith = Self-sufficient combatant (craft own weapons, maintain equipment)
- Cook + Innkeeper = Complete hospitality provider (prepare meals, manage inn)
- Miner + Blacksmith = Vertical integration (mine ore, forge items)

## Related Content

- [Class System GDD](../../systems/class-system/index.md) - Authoritative mechanics
- [Actions Index](../actions/index.md) - Actions that grant class XP
- [Skills Index](../skills/index.md) - Skills that modify action effectiveness
```

---

## Template 2: Category-Index

**Purpose**: Mid-level navigation for a content subcategory

**Location Pattern**: `docs/design/content/{category}/{subcategory}/index.md`

**Example**: `docs/design/content/classes/combat/index.md`

### Template

```markdown
---
title: '[Subcategory] [Category]'
type: 'category-index'
summary: 'Brief one-line description of the subcategory'
parent: '[parent-category]'
---

# [Subcategory] [Category]

## Overview

[1 paragraph explaining this subcategory's focus and scope]

## Available [Items]

Complete subcategory listing:

| [Item]                  | Tier  | [Key Attribute] | [Key Attribute] | Best For |
| ----------------------- | ----- | --------------- | --------------- | -------- |
| [[Name]](name/index.md) | Value | Value           | Value           | Use case |
| [[Name]](name/index.md) | Value | Value           | Value           | Use case |

## [Domain]-Specific Guidance

**Acquisition**: How to unlock these [items]
**Evolution Paths**: Common progression routes
**Synergies**: Combinations that work well together

## Common Usage Patterns

**[Pattern 1]**: Description and example
**[Pattern 2]**: Description and example

## Back to Main Index

[Return to [Category] Index](../index.md)
```

### Concrete Example: Combat Classes Index

```markdown
---
title: 'Combat Classes'
type: 'category-index'
summary: 'Melee, ranged, and tactical combat classes'
parent: 'classes'
---

# Combat Classes

## Overview

Combat classes specialize in defeating enemies through melee, ranged, or magical combat. All combat classes track actions with `combat.*` tags (and children), gaining XP from fighting, training, and tactical positioning.

## Available Combat Classes

| Class                                     | Tier | Primary Tags                 | Acquisition    | Evolution Options               |
| ----------------------------------------- | ---- | ---------------------------- | -------------- | ------------------------------- |
| [Fighter](../fighter/index.md)            | 1    | combat                       | Starting class | Warrior, Archer, Guardian       |
| [Warrior](fighter/warrior/index.md)       | 2    | combat.melee                 | 100 combat XP  | Knight, Berserker, Blade Dancer |
| [Archer](fighter/archer/index.md)         | 2    | combat.ranged.bow            | 100 ranged XP  | Hunter, Sniper, Ranger          |
| [Knight](fighter/warrior/knight/index.md) | 3    | combat.melee, defense.shield | Warrior Lv.10  | Paladin, Champion               |

## Combat-Specific Guidance

**Acquisition**:

- Foundation (Fighter): Available at character creation
- Specialization (Warrior/Archer): Requires 100-150 combat XP from fighting
- Advanced (Knight/Berserker): Requires Tier 2 class at level 10+

**Evolution Paths**:

- Defensive: Fighter → Warrior → Knight → Paladin (shield focus, high survivability)
- Offensive: Fighter → Warrior → Berserker → Slayer (damage focus, glass cannon)
- Ranged: Fighter → Archer → Hunter → Ranger (wilderness specialist)

**Synergies**:

- Warrior + Blacksmith = Weapon mastery + crafting (self-sufficient)
- Archer + Hunter = Ranged combat + tracking (wilderness exploration)
- Knight + Guardian = Defense specialist (ultimate tank)

## Common Usage Patterns

**Solo Combat**: Warrior or Knight (high survivability, moderate damage)
**Group Combat**: Warrior as tank, Archer as DPS, Guardian as protector
**PvE Farming**: Berserker for fast kills, Archer for safe ranged combat
**Boss Fights**: Knight to tank, Archer for consistent DPS, support classes for buffs

## Back to Main Index

[Return to Classes Index](../index.md)
```

---

## Template 3: Detail-Page (Reference)

**Purpose**: Individual entity documentation with full specification

**Location Pattern**: `docs/design/content/{category}/{subcategory}/{name}/index.md`

**Example**: `docs/design/content/classes/combat/warrior/index.md`

**Note**: This is NOT an index page, but included for completeness as the third navigation level.

### Template

```markdown
---
title: '[Item Name]'
type: '[detail-type]' # class-detail, action, skill, item-detail
summary: 'Brief one-line description'
parent: '[parent-name]'
tree_tier: [1|2|3] # For classes only
gdd_ref: 'systems/[system]/index.md#[section-id]'
tags: [keyword1, keyword2]
---

# [Item Name]

## Lore (for classes/items with story)

### Origin

[Background story, context in world]

### In the World

[How this appears in gameplay, usage context]

---

## Mechanics

### Prerequisites

| Requirement     | Value   |
| --------------- | ------- |
| [Requirement 1] | [Value] |
| [Requirement 2] | [Value] |

### [Domain]-Specific Stats/Properties

[Tables, formulas, mechanics specific to this item]

### [Key Feature 1]

[Detailed description]

### [Key Feature 2]

[Detailed description]

---

## [Progression/Usage/Variants]

[How this item evolves, variations, or usage patterns]

---

## Related Content

- **Requires**: [Prerequisites]
- **Works With**: [Synergies]
- **Provides**: [Outputs/Services]
- **See Also**: [Related items]
```

---

## Design Principles

### 1. Progressive Disclosure

**Goal**: Answer common questions at index level without forcing detail page reads

**Implementation**:

- Include key attributes in navigation tables
- Provide selection guides inline
- Show common patterns and synergies
- Link to details only for deep dives

**Example**: Classes index shows acquisition costs, evolution options → 80% of queries satisfied

### 2. Hierarchical Navigation

**3-Level Maximum**:

1. System-Overview → See all classes
2. Category-Index → See all combat classes + selection guide
3. Detail-Page → Deep dive on Warrior mechanics

**Target**: <3 file reads for most queries

### 3. Rich Content

**What to Include on Index Pages**:

- ✅ Complete navigation tables
- ✅ Quick selection guides ("Best for X")
- ✅ Common patterns and synergies
- ✅ Comparison tables (key attributes)
- ✅ Evolution paths (class trees, upgrade paths)

**What to Exclude** (move to detail pages):

- ❌ Detailed mechanics (>400 words)
- ❌ Edge cases and exceptions
- ❌ Code examples
- ❌ Historical design notes

### 4. Consistent Structure

**All Index Pages Follow Same Pattern**:

1. Frontmatter (required fields)
2. Overview paragraph (1-2 sentences, system context)
3. Navigation section (complete table with descriptions)
4. Quick selection guide ("Best for X")
5. Common patterns (frequently used combinations)
6. Related content links

**Benefits**:

- Predictable for agents
- Easier to generate programmatically
- Consistent user experience

---

## Generation Strategy

### Manual Creation (High Priority)

Create manually for:

- Top-level indexes (classes, actions, skills, items) - ~10 files
- Major category indexes (combat classes, crafting classes) - ~20 files

**Why Manual**: These are high-value navigation hubs requiring domain knowledge

### Automated Generation (Lower Priority)

Generate programmatically for:

- Minor category indexes (rarely visited subcategories)
- Regeneration after bulk updates

**Script**: `.specify/scripts/generate-indexes.sh`

**Template Engine**: Use templates above with placeholders:

- `{CATEGORY}` → class, action, skill, item
- `{ITEMS}` → List of items in category (from frontmatter scan)
- `{ATTRIBUTES}` → Key attributes extracted from item frontmatter

---

## Validation Checklist

**For Each Index Page**:

- [ ] Has required frontmatter (title, type, summary)
- [ ] Navigation table is complete (lists ALL child items)
- [ ] Each navigation link is valid (points to existing file)
- [ ] Quick selection guide provides actionable recommendations
- [ ] Common patterns section shows 3-5 frequently used combinations
- [ ] Related content links are valid
- [ ] Target: Answers 80% of queries without detail page reads

**Test Method**: Agent query simulation

- Run 10 common queries ("What classes are good for combat?")
- Measure: How many require only index page reads?
- Target: ≥8/10 (80%) satisfied by index alone

---

_Index templates complete - ready for Phase 2 implementation_
