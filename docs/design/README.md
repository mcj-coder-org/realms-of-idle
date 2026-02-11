# Design Documentation

Welcome to the Realms of Idle game design documentation. This directory contains all game design documents (GDDs), content examples, and reference materials.

---

## Quick Start

**For AI Agents**: Start at `index.md` for system overview, then navigate to specific GDD files, then to content examples. This provides efficient progressive loading (<3 file reads for most queries).

**For Humans**: Browse by system domain below, or explore content examples in the `/content/` directory.

---

## Navigation Guide

### Progressive Loading Architecture (3 Levels)

**Level 1 - System Overview** → `docs/design/index.md`

Start here to see all available systems. This single file lists every system GDD with brief descriptions.

**Level 2 - System GDD** → `docs/design/systems/<system-name>-gdd.md`

Complete mechanics for one system domain. Each GDD is authoritative for its domain and includes:

- Core mechanics and rules
- Implementation details
- Complexity ratings
- Design decisions
- Links to related content examples

**Level 3 - Content Examples** → `docs/design/content/<category>/<name>/index.md`

Specific examples (classes, skills, items, creatures). Each content file includes:

- `gdd_ref`: Points to authoritative GDD section
- Concrete values and parameters
- Context-specific details

---

## System GDDs

### Core Systems

| GDD                                                               | Description                               |
| ----------------------------------------------------------------- | ----------------------------------------- |
| [Core Progression System](systems/core-progression-system-gdd.md) | XP buckets, class management, progression |
| [Class System](systems/class-system-gdd.md)                       | Tiers, specializations, eligibility       |
| [Skill & Recipe System](systems/skill-recipe-system-gdd.md)       | Skills, recipes, crafting                 |
| [Action System](systems/action-system-gdd.md)                     | Actions, context, tag resolution          |

### Domain Systems

| GDD                                                           | Description                   |
| ------------------------------------------------------------- | ----------------------------- |
| [Combat System](systems/combat-system-gdd.md)                 | Combat mechanics, weapons     |
| [Crafting System](systems/crafting-system-gdd.md)             | Crafting, quality, tools      |
| [Gathering System](systems/gathering-system-gdd.md)           | Resource gathering, nodes     |
| [Economy System](systems/economy-system-gdd.md)               | Trade, pricing, currency      |
| [Exploration System](systems/exploration-system-gdd.md)       | World navigation, discovery   |
| [Settlement System](systems/settlement-system-gdd.md)         | Towns, services, NPCs         |
| [Magic System](systems/magic-system-gdd.md)                   | Spell casting, mana           |
| [Enchantment System](systems/enchantment-system-gdd.md)       | Item enchanting, effects      |
| [NPC Core Systems](systems/npc-core-systems-gdd.md)           | NPC behaviors, goals          |
| [Faction & Reputation](systems/faction-reputation-gdd.md)     | Faction mechanics, reputation |
| [Personality Traits](systems/personality-traits-gdd.md)       | Character traits, moods       |
| [Morale System](systems/morale-system-gdd.md)                 | Morale, surrender             |
| [Party Mechanics](systems/party-mechanics-gdd.md)             | Party formation, roles        |
| [NPC Simulation](systems/npc-simulation-gdd.md)               | NPC daily routines            |
| [Relationship System](systems/relationship-system-gdd.md)     | NPC relationships, romance    |
| [Seasonal Events](systems/seasonal-events-gdd.md)             | Festivals, seasonal content   |
| [Prophecy System](systems/prophecy-system-gdd.md)             | Prophecies, fate              |
| [Procedural Encounters](systems/procedural-encounters-gdd.md) | Dynamic events                |

---

## Content Examples

Content files are organized by category in `/content/`:

### Categories

- **`/actions/`** - Generic action definitions (Attack, Craft, Gather, etc.)
- **`/classes/`** - Playable classes (Fighter, Crafter, Host, etc.)
- **`/skills/`** - Skill definitions with tags and effects
- **`/items/`** - Equipment, consumables, materials
- **`/recipes/`** - Crafting recipes
- **`/creatures/`** - NPCs and enemies
- **`/enchantments/`** - Enchantment effects
- **`/locations/`** - Zones, settlements, dungeons
- **`/races/`** - Playable and NPC races
- **`/material/`** - Raw materials for crafting

### Content File Structure

All content files use hierarchical structure:

```
content/<category>/<name>/
  ├── index.md          # Main definition
  └── <subtypes>/       # Optional specializations
      └── index.md
```

Example:

```
content/classes/fighter/
  ├── index.md          # Fighter foundation class
  └── warrior/
      └── index.md      # Warrior specialization
```

### Frontmatter Schema

Every content file includes minimal frontmatter:

```yaml
---
title: 'Human-readable name'
gdd_ref: 'systems/<system-name>-gdd.md#section'
---
```

Optional fields:

- `tree_tier`: 1, 2, or 3 (for hierarchical classes/skills)
- `parent`: "parent-name/index.md" (for tree navigation)
- `type`: class-detail, skill-detail, item-detail, creature-detail
- `summary`: Brief one-line description

---

## Reference Materials

### Cross-References

- **`/reference/cross-reference/`** - Mappings between GDDs and content
- **`/reference/migration/`** - Migration documentation and source mappings

### Terminology

**Current Architecture** (as of 002-doc-migration-rationalization):

- ✅ **All classes always active**: No slots, no activation/deactivation
- ✅ **Automatic XP distribution**: Based on action tags, not player configuration
- ✅ **Hierarchical tag matching**: 50% exact match, 50% distributed to parents
- ✅ **Classes track actions**: Classes gain XP from tag-matching actions
- ✅ **Skills provide bonuses**: Skills modify action effectiveness, not XP

**Deprecated Terms** (removed):

- ❌ "dormant classes"
- ❌ "XP split" or "XP splitting"
- ❌ "active class slots"
- ❌ "transition to" (use "unlock eligibility for")
- ❌ "player-configured XP"

---

## For Contributors

### Adding New Content

1. **Create file** in appropriate `/content/<category>/` directory
2. **Use hierarchical structure**: `<name>/index.md`
3. **Include frontmatter**: Minimum `title` + `gdd_ref`
4. **Follow templates**: See `/content/_templates/` for examples
5. **Link to GDD**: Every content file must reference its authoritative GDD

### Updating GDDs

1. **System GDDs are authoritative**: Changes here propagate to content
2. **Update cross-references**: Run `.specify/scripts/validate-gdd-references.sh`
3. **Update terminology**: Keep consistent with current architecture
4. **Run validators**: Use `.specify/scripts/validate-all.sh` before committing

### Validation Scripts

Located in `.specify/scripts/`:

- `validate-all.sh` - Master validation suite
- `validate-terminology.sh` - Check for deprecated terms
- `validate-frontmatter.sh` - Verify frontmatter schema
- `validate-gdd-references.sh` - Check `gdd_ref` validity
- `validate-action-references.sh` - Verify action links
- `validate-navigation-links.sh` - Test progressive loading

---

## Migration History

This documentation was migrated and rationalized in feature `002-doc-migration-rationalization`. See [MIGRATION-REPORT.md](MIGRATION-REPORT.md) for complete details.

**Key Changes**:

- 430 files restructured to hierarchical format
- 447 files updated with minimal frontmatter
- 37 generic action pages created
- 65 source files migrated from cozy-fantasy-rpg
- All deprecated terminology removed
- Progressive loading architecture established

---

## Questions?

- **Architecture**: See `index.md` for system overview
- **Specific System**: Navigate to relevant GDD in `/systems/`
- **Content Examples**: Browse `/content/` by category
- **Migration Details**: See [MIGRATION-REPORT.md](MIGRATION-REPORT.md)
