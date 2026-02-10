# Data Model - Documentation Entities

**Feature**: 002-doc-migration-rationalization
**Phase**: Phase 1 (Design Artifacts)
**Date**: 2026-02-10

---

## Overview

This document defines the entities (content types) in the documentation system. These are NOT code data models - they are documentation content types that will be represented as markdown files.

**Key Distinction**: This is a documentation migration feature. Entities are documentation pages, not database schemas or code classes.

---

## Core Entities

### 1. Class (Content)

**Purpose**: Document a character class (Warrior, Cook, Innkeeper, etc.)

**Location**: `docs/design/content/classes/{category}/{name}/index.md`

**Frontmatter**:

```yaml
---
title: 'Warrior' # Display name
type: 'class-detail' # Entity type
summary: 'Front-line melee combatant with heavy armor proficiency'
tags: [combat, melee, starter] # Searchable keywords
tier: 'Apprentice' # Acquisition tier (Apprentice, Journeyman, Master)
tree_tier: 2 # Class tree position (1=Foundation, 2=Specialization, 3=Advanced)
parent: 'fighter' # Parent class in tree
gdd_ref: 'systems/class-system/index.md#specialization-classes'
---
```

**Content Sections**:

- Lore (Origin, In the World)
- Mechanics (Prerequisites, Requirements, Stats, Skills)
- Tracked Actions (actions that grant XP, with links to action pages)
- Progression (Specializations)
- Related Content

**Key Attributes**:

- `tree_tier`: 1 (Foundation like Fighter), 2 (Specialization like Warrior), 3 (Advanced like Blade Dancer)
- `tracked_actions`: References to action pages (NOT inline definitions)
- `skills`: Starting skills auto-awarded on class acceptance
- `gdd_ref`: Link to authoritative GDD section

**Note**: Classes DO NOT provide bonuses (speed, efficiency, quality). Skills provide all bonuses. Classes only track actions and grant XP.

---

### 2. Action (Content)

**Purpose**: Document a generic player action (Attack, Serve, Forge, Mine)

**Location**: `docs/design/content/actions/{domain}/{name}/index.md`

**Frontmatter**:

```yaml
---
title: 'Attack'                     # Display name
type: 'action'                      # Entity type
summary: 'Engage target in combat using equipped weapon'
domain: 'combat'                    # Primary domain (combat, crafting, gathering, service, trade, knowledge, social)
ui_trigger: 'Attack Button'         # UI element that triggers action
base_duration: 3s                   # Default duration (before skill modifiers)
required_context:                   # Context required to perform
  - weapon: [Sword, Bow, Spear, Staff, Unarmed]
  - target: [Enemy, Practice Dummy]
tag_resolution:                     # How context determines tags
  - context: {weapon: Sword} → tag: combat.melee.sword
  - context: {weapon: Bow} → tag: combat.ranged.bow
gdd_ref: 'systems/action-system/index.md#combat-actions'
---
```

**Content Sections**:

- Description (what the action does)
- Context Requirements (weapon, target, location, item, etc.)
- Tag Resolution (how context determines which tags fire)
- XP Distribution (which classes gain XP based on tags)
- Modified By Skills (which skills affect this action)
- Prerequisites (requirements to perform)
- Failure Conditions (when action fails)
- Related Actions (precedes, follows)

**Key Attributes**:

- **Generic**: One "Attack" action, not separate "Swing Sword" and "Shoot Bow" actions
- **Context-Driven**: Context (weapon type) determines tag resolution
- **Tag-Based XP**: Classes gain XP based on tag matching (dynamic, not hardcoded lists)
- **Total Count**: ~37 generic actions across all domains

**Example Tag Resolution**:

- Attack + Sword context → `combat.melee.sword` tag → Warrior class gains XP (tracks `combat.melee`)
- Attack + Bow context → `combat.ranged.bow` tag → Archer class gains XP (tracks `combat.ranged`)

---

### 3. Skill (Content - Already Migrated)

**Purpose**: Document an ability that modifies action effectiveness

**Location**: `docs/design/content/skills/{type}/{name}/index.md`

**Frontmatter**:

```yaml
---
title: 'Power Strike'
type: 'skill'
tier: 'Tiered' # Skill tier (Lesser, Greater, Enhanced)
skill_type: 'Cooldown' # Passive, Cooldown, Toggle, Tiered, etc.
tags: [Combat/Melee] # Hierarchical skill tags
summary: 'Increased melee damage on demand with cooldown'
gdd_ref: 'systems/skill-system/index.md#cooldown-skills'
---
```

**Content Sections**:

- Description
- Tiers (Lesser, Greater, Enhanced with scaling)
- Acquisition (how to earn the skill)
- Synergy Classes (which classes benefit)
- Related Content

**Key Attributes**:

- **Modifies Actions**: Skills affect action effectiveness (damage, speed, accuracy), NOT grant actions
- **Already Migrated**: ~171 skills already documented (60 common + 111 class-specific)
- **Not Part of This Feature**: Skills are already complete, don't need migration

**Example**:

- **Power Strike** (skill): Activated before Attack action → +20-50% damage to next attack
- **Weapon Mastery** (skill): Passive → +10-25% damage/accuracy with weapon category

---

### 4. Item (Content)

**Purpose**: Document game items (weapons, armor, consumables, materials)

**Location**: `docs/design/content/items/{category}/{name}/index.md`

**Frontmatter**:

```yaml
---
title: 'Iron Sword'
type: 'item-detail'
category: 'weapon' # weapon, armor, consumable, material, tool
quality_tier: 2 # Quality level (1-5, was ambiguously called "tier")
summary: 'Standard melee weapon forged from iron'
tags: [weapon, melee, iron, crafted]
gdd_ref: 'systems/item-system/index.md#weapons'
---
```

**Content Sections**:

- Description
- Properties (damage, durability, weight)
- Acquisition (how to obtain)
- Crafting Recipe (if craftable)
- Related Content

**Key Issue to Fix**:

- Source files use `tier:` ambiguously (sometimes quality tier, sometimes skill tier)
- **Solution**: Rename to `quality_tier` for item quality, `skill_tier` for skill requirements

---

### 5. System (GDD)

**Purpose**: Authoritative game design specification

**Location**: `docs/design/systems/{system-name}/index.md`

**Frontmatter**:

```yaml
---
title: 'Class System'
type: 'system-overview'
summary: 'Complete character class mechanics with multi-class support'
version: '1.0.0'
status: 'active' # active, deprecated, proposed
---
```

**Content Sections**:

- Terminology (definitions)
- Core Mechanics
- Formulas
- Examples (co-located in examples/)
- Related Systems

**Key Attributes**:

- **Authoritative**: GDD is source of truth, content files are examples
- **Co-located Examples**: Examples live in `examples/` subfolder
- **Corrections Needed**: 7 corrections identified in R1 research

---

## Index Pages (Navigation Hubs)

### System-Overview Index

**Purpose**: Top-level navigation for a content category

**Location**: `docs/design/content/classes/index.md`

**Frontmatter**:

```yaml
---
title: 'Character Classes'
type: 'system-overview'
summary: 'Complete class system with acquisition, evolution, and tag requirements'
---
```

**Content**:

- Overview paragraph (1-2 sentences)
- Complete navigation table (all child items with descriptions)
- Quick selection guide ("Best for X" recommendations)
- Common patterns (frequently used combinations)

**Target**: Enable 80% of queries without reading detail pages

---

### Category-Index

**Purpose**: Mid-level navigation for a content subcategory

**Location**: `docs/design/content/classes/combat/index.md`

**Frontmatter**:

```yaml
---
title: 'Combat Classes'
type: 'category-index'
summary: 'Melee, ranged, and tactical combat classes'
parent: 'classes'
---
```

**Content**:

- Category overview
- All items table (with key attributes)
- Evolution paths summary
- Tag requirements overview

---

### Detail-Page

**Purpose**: Individual entity documentation

**Location**: `docs/design/content/classes/combat/warrior/index.md`

**Frontmatter**:

```yaml
---
title: 'Warrior'
type: 'class-detail'
summary: 'Front-line melee combatant'
parent: 'fighter'
tree_tier: 2
gdd_ref: 'systems/class-system/index.md#specialization-classes'
---
```

**Content**:

- Full specification with all sections
- Deep dive mechanics
- Examples (co-located in examples/)

---

## Entity Relationships

```
System (GDD)
  ↓ defines mechanics for
Content (Classes, Actions, Skills, Items)
  ↓ references via
gdd_ref frontmatter field
  ↓ generates
Cross-Reference Maps (bidirectional)
```

**Example Flow**:

1. `systems/class-system/index.md` (GDD) defines how classes work
2. `content/classes/combat/warrior/index.md` (Class) references GDD via `gdd_ref`
3. `reference/cross-reference/gdd-to-content.md` (Generated Map) lists all classes implementing class-system
4. Agent can navigate: GDD → Map → Content OR Content → gdd_ref → GDD

---

## Cross-Reference Model

### GDD → Content (Generated)

**File**: `docs/design/reference/cross-reference/gdd-to-content.md`

**Format**:

```markdown
## systems/class-system/index.md

### § specialization-classes

Referenced by:

- [Warrior](../../content/classes/combat/warrior/index.md)
- [Knight](../../content/classes/combat/knight/index.md)
```

**Generation**: Automated script scans all content frontmatter `gdd_ref` fields

---

### Content → GDD (Frontmatter)

**Format**: Each content file has `gdd_ref` in frontmatter:

```yaml
gdd_ref: 'systems/class-system/index.md#specialization-classes'
```

**Validation**: Pre-commit hook verifies all `gdd_ref` targets exist

---

## Frontmatter Fields Summary

### Required (All Files)

- `title`: Display name (non-empty string)
- `type`: Entity type (system-overview, category-index, class-detail, action, skill, item-detail)
- `summary`: One-line description (for search results, link previews)

### Conditional (Content Files Only)

- `gdd_ref`: Link to authoritative GDD section (REQUIRED for all content)
- `parent`: Link to parent in tree (OPTIONAL for hierarchical content)
- `tree_tier`: 1/2/3 for class tree position (REQUIRED for classes only)
- `tags`: Searchable keywords (OPTIONAL but recommended)

### Domain-Specific

- **Classes**: `tier` (Apprentice/Journeyman/Master), `tree_tier` (1/2/3)
- **Actions**: `domain`, `ui_trigger`, `base_duration`, `required_context`, `tag_resolution`
- **Skills**: `skill_type` (Passive/Cooldown/etc), `tier` (Lesser/Greater/Enhanced)
- **Items**: `category`, `quality_tier` (1-5)
- **Systems**: `version`, `status` (active/deprecated/proposed)

---

## Validation Rules

### Frontmatter Schema

- `title` must be non-empty string
- `type` must be from allowed values list
- `gdd_ref` must point to existing GDD file + section ID
- `parent` must point to existing file (if present)
- `tree_tier` must be 1, 2, or 3 (if present for classes)

### Cross-Reference Integrity

- All `gdd_ref` targets must exist (file + section)
- No circular parent references
- Generated maps must be current (timestamp check)

### Terminology Consistency

- No use of "dormant classes" (deprecated)
- No use of "XP splitting" (deprecated)
- No use of "active class slots" (deprecated)
- Consistent use of "class rank" vs "class tree tier" vs "skill tier"

---

## Entity Count Estimates

| Entity Type       | Count             | Source                                            |
| ----------------- | ----------------- | ------------------------------------------------- |
| Classes           | 679 + 4 new = 683 | Migrated + newly created                          |
| Actions           | 37                | Identified in R5 research                         |
| Skills            | ~171              | Already migrated (60 common + 111 class-specific) |
| Items             | ~150              | Estimated from source                             |
| Systems (GDD)     | ~60               | Source count                                      |
| **Total Content** | ~1,100 files      |                                                   |
| Index Pages       | ~50               | System + category indexes                         |
| **Grand Total**   | ~1,150 files      |                                                   |

---

## Migration Status

- ✅ **Skills**: Already migrated (~171 files complete)
- ✅ **Some Classes**: 626 already migrated (need updates)
- ⏳ **Actions**: 37 to create (Phase 2)
- ⏳ **Classes**: 53 remaining + 4 new to migrate (Phase 2)
- ⏳ **Items**: TBD count to migrate (Phase 2)
- ⏳ **Systems (GDD)**: 7 corrections needed (Phase 2)

---

_Data model complete - defines all documentation entities for migration_
