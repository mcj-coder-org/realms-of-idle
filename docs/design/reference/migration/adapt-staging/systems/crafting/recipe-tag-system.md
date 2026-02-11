<!-- ADAPTATION REQUIRED -->
<!-- This file was migrated from source but needs manual review: -->
<!-- - Update terminology (dormant classes, XP split, etc.) -->
<!-- - Align with current GDD architecture -->
<!-- - Add missing sections as needed -->
<!-- - Update frontmatter with correct gdd_ref -->

---

title: 'Recipe Tag System'
type: 'system'
category: 'crafting'
summary: 'Hierarchical tag system for recipe access control and discovery'

---

# Recipe Tag System

## Overview

Recipes use **hierarchical tags** (identical to skill tags) to determine which characters can craft them with synergy bonuses. This replaces tier-based folder organization with a flexible, class-aligned access system.

This system is essential for:

- **Automatic recipe filtering** - Characters see recipes matching their tag depth
- **Synergy calculation** - Tag match quality determines crafting bonuses
- **Cross-discipline access** - Consolidation classes inherit multiple tag paths
- **Progressive disclosure** - Higher skills unlock deeper recipe tiers

## Tag Structure

### Hierarchy Format

```
Crafting/[Profession]/[Category]/[Specialization][/Tier]
```

**Example tags:**

- `Crafting/Smithing` - All smithing recipes (depth 2)
- `Crafting/Smithing/Weapons` - All weapon recipes (depth 3)
- `Crafting/Smithing/Weapons/Swords` - Basic sword recipes (depth 4)
- `Crafting/Smithing/Weapons/Swords/Journeyman` - Journeyman sword recipes (depth 5)

### Single Tag Rule (CRITICAL)

**Each recipe has EXACTLY ONE tag - the deepest/most specific in its hierarchy.**

❌ **WRONG - Multiple tags breaks filtering:**

```yaml
tags:
  - Crafting/Smithing
  - Crafting/Smithing/Weapons
  - Crafting/Smithing/Weapons/Swords # This would be depth 4
```

Result: ALL Blacksmiths (depth 2) can see this recipe, defeating specialization!

✅ **CORRECT - Single deepest tag:**

```yaml
tags:
  - Crafting/Smithing/Weapons/Swords # ONLY this tag - depth 4
```

Result: ONLY Sword specialists (depth 4+) can see this recipe!

### Craftsman Tiers

Add tier suffixes to control recipe access by skill progression:

- No tier suffix = Basic/Apprentice level (everyone with specialization)
- `/Journeyman` = Mid-tier recipes (experienced crafters)
- `/Master` = High-tier recipes (master crafters)
- `/Legendary` = Epic recipes (legendary crafters)

**Examples:**

- `Crafting/Smithing/Weapons/Swords` - Basic swords (depth 4)
- `Crafting/Smithing/Weapons/Swords/Journeyman` - Journeyman swords (depth 5)
- `Crafting/Smithing/Weapons/Swords/Master` - Master swords (depth 6)
- `Crafting/Smithing/Weapons/Swords/Legendary` - Legendary swords (depth 7)

## Class Access by Tag Depth + Skill Progression

Classes can see recipes **at or below their tag depth:**

| Class Level           | Tag Depth | Can See Tags    | Example Recipes                                 |
| --------------------- | --------- | --------------- | ----------------------------------------------- |
| Tier 1: Crafter       | 1         | `Crafting` only | Universal recipes (rare)                        |
| Tier 2: Blacksmith    | 2         | Depth 1-2       | `Crafting/Smithing` general recipes             |
| Tier 3: Weaponsmith   | 3         | Depth 1-3       | `Crafting/Smithing/Weapons` basic weapons       |
| Tier 4: Swordsmith    | 4         | Depth 1-4       | `Crafting/Smithing/Weapons/Swords` basic swords |
| Swordsmith Journeyman | 5         | Depth 1-5       | + `/Journeyman` swords                          |
| Swordsmith Master     | 6         | Depth 1-6       | + `/Master` swords                              |
| Swordsmith Legendary  | 7         | Depth 1-7       | + `/Legendary` swords                           |

**Access Rule:** `class_tag_depth >= recipe_tag_depth`

### Progression Example

**Basic Recipe:** `Crafting/Smithing/Weapons/Swords` (depth 4)

- Swordsmith Apprentice (depth 4): **CAN SEE** ✓
- Swordsmith Journeyman (depth 5): **CAN SEE** ✓
- Swordsmith Master (depth 6): **CAN SEE** ✓

**Journeyman Recipe:** `Crafting/Smithing/Weapons/Swords/Journeyman` (depth 5)

- Swordsmith Apprentice (depth 4): **CANNOT SEE** ✗ (4 < 5)
- Swordsmith Journeyman (depth 5): **CAN SEE** ✓
- Swordsmith Master (depth 6): **CAN SEE** ✓

**Master Recipe:** `Crafting/Smithing/Weapons/Swords/Master` (depth 6)

- Swordsmith Apprentice (depth 4): **CANNOT SEE** ✗
- Swordsmith Journeyman (depth 5): **CANNOT SEE** ✗ (5 < 6)
- Swordsmith Master (depth 6): **CAN SEE** ✓

### Skill-Based Depth Progression

As a Swordsmith levels up and gains skill, their tag depth increases:

- Level 1-10: depth 4 (basic swords only)
- Level 11-25: depth 5 (+ journeyman recipes)
- Level 26-50: depth 6 (+ master recipes)
- Level 50+: depth 7 (+ legendary recipes)

## NO Multi-Tagging

Recipes have EXACTLY ONE tag. NO cross-discipline tags.

❌ **Don't do this:**

```yaml
tags:
  - Crafting/Alchemy/Potions/Healing
  - Restoration # NO - breaks filtering!
  - Gathering/Herbalism # NO - breaks filtering!
```

✅ **Instead use:**

- **One tag:** `Crafting/Alchemy/Potions/Healing`
- **Metadata fields:** `effect: healing`, `intended_for: clerics`
- **Consolidation classes:** Healer gets BOTH `Crafting/Alchemy` AND `Magic/Restoration` tag paths automatically

**Consolidation Class Example:** Healer (Mage + Herbalist)

- Has access to: `Crafting/Alchemy` tags (from Herbalist)
- Has access to: `Magic/Restoration` tags (from Mage)
- Can craft healing potions AND cast healing spells
- Gets synergy bonuses from BOTH paths where applicable

## Recipe Organization Structure

### Folder Organization

Recipes organized by **function/purpose**, not by tier:

```
recipes/
├── blacksmithing/
│   └── weapons/
│       ├── index.md
│       ├── daggers/
│       │   ├── index.md
│       │   ├── copper-dagger/index.md
│       │   ├── iron-dagger/index.md
│       │   └── ...
│       ├── swords/
│       │   ├── index.md
│       │   ├── copper-shortsword/index.md
│       │   └── ...
│       └── ...
├── alchemy/
│   └── potions/
│       ├── index.md
│       ├── healing/
│       │   ├── index.md
│       │   ├── minor-healing-draught/index.md
│       │   └── ...
│       └── ...
└── ...
```

**Key Points:**

- Organize by **weapon type** (daggers, swords), **potion type** (healing, buff), etc.
- NOT by material tier (copper, iron, steel)
- NOT by difficulty tier (basic, journeyman, master)
- Tag depth controls access, not folder structure

## Recipe Frontmatter Format

**Minimum required fields:**

```yaml
---
title: 'Iron Longsword'
type: 'recipe'
category: 'weapon'
profession: 'blacksmithing'
tags:
  - Crafting/Smithing/Weapons/Swords # ONLY ONE TAG
materials:
  - Iron Ingot (3)
  - Leather Grip (1)
  - Pommel (1)
difficulty: basic
crafting_time: '4 hours'
output_quantity: 1
output_quality: 'standard'
value: 50 gold
---
```

### Optional Metadata Fields

```yaml
# Armor classification
armor_type: 'heavy' # for armor recipes
armor_piece: 'sword'

# Meal classification
meal_type: 'stew'
meal_servings: 4
buff_effect: '+25 Stamina for 6 hours'

# Item categorization
item_rarity: 'common'
intended_for: 'warriors, knights' # Description, not a tag
material_primary: 'iron'
material_secondary: 'leather'

# Enchanting info
requires_enchanting: true
enchanting_difficulty: 'basic'

# Recipe relationships
prerequisite_recipes: [] # Other recipes that should be known first
variant_of: null # If this is a variant
variants: [] # Other variants of this recipe
```

## Frontmatter Examples with Craftsman Tiers

### Example 1: Basic Recipe (No Tier Suffix)

```yaml
---
title: 'Copper Dagger'
type: 'recipe'
category: 'weapon'
profession: 'blacksmithing'
tags:
  - Crafting/Smithing/Weapons/Daggers # Basic - no tier suffix
materials:
  - Copper Ingot (2)
  - Leather Strip (1)
  - Wood Handle (1)
difficulty: basic
crafting_time: '30 minutes'
output_quantity: 1
value: 5 gold
---
```

**Access:** Any Dagger specialist (depth 4+)

### Example 2: Journeyman Recipe

```yaml
---
title: 'Damascus Steel Dagger'
type: 'recipe'
category: 'weapon'
profession: 'blacksmithing'
tags:
  - Crafting/Smithing/Weapons/Daggers/Journeyman # Journeyman tier
materials:
  - Damascus Steel Ingot (2)
  - Fine Leather (1)
  - Ironwood Handle (1)
  - Silver Pommel (1)
difficulty: intermediate
crafting_time: '4 hours'
output_quantity: 1
value: 50 gold
---
```

**Access:** Journeyman+ Dagger specialists (depth 5+)

### Example 3: Master Recipe

```yaml
---
title: 'Mithril Shadow Blade'
type: 'recipe'
category: 'weapon'
profession: 'blacksmithing'
tags:
  - Crafting/Smithing/Weapons/Daggers/Master # Master tier
materials:
  - Mithril Ingot (3)
  - Shadow Essence (1)
  - Enchanted Leather (1)
  - Dragonbone Handle (1)
difficulty: advanced
crafting_time: '12 hours'
output_quantity: 1
value: 500 gold
requires_enchanting: true
---
```

**Access:** Master+ Dagger specialists (depth 6+)

### Example 4: Cooking Recipe with Consolidation Benefits

```yaml
---
title: 'Hearty Vegetable Stew'
type: 'recipe'
category: 'meal'
profession: 'cooking'
meal_type: 'stew'
tags:
  - Crafting/Cooking/Meals/Stews  # Basic - no tier suffix
materials:
  - Potatoes (3)
  - Carrots (2)
  - Onion (1)
  - [Venison](../../materials/game-meat/venison/index.md) (1 lb)
  - Vegetable Stock (1 quart)
  - [Rosemary](../../materials/reagents/common-herbs/rosemary/index.md) (1 bundle)
difficulty: intermediate
crafting_time: '3 hours'
buff_effect: '+25 Stamina for 6 hours'
output_quantity: 6 servings
value: 5 gold per serving
---
```

**Consolidation Note:** Cook (Crafter + Gatherer) gets:

- `Crafting/Cooking` from Crafter side → can craft meals
- `Gathering` from Gatherer side → knows about ingredients
- Can reference ingredient pages directly with synergy

## Access Control & Synergy Calculation

### Recipe Visibility (Tag Depth Check)

**Rule:** Characters can **see** recipes where `class_tag_depth >= recipe_tag_depth`

**Example:**

- Blacksmith (depth 2) sees: `Crafting` (depth 1), `Crafting/Smithing` (depth 2)
- Blacksmith CANNOT see: `Crafting/Smithing/Weapons` (depth 3+)

### Crafting Synergy (Tag Match Quality)

Once a recipe is visible, synergy depends on tag match:

| Tag Match Type   | Tag Depth Diff | Synergy Bonus  | Effect                                  |
| ---------------- | -------------- | -------------- | --------------------------------------- |
| **Exact match**  | 0              | Full (+50%)    | +50% quality, -25% materials, -50% time |
| **Parent match** | -1             | Partial (+25%) | +25% quality, -10% materials, -25% time |
| **Grandparent+** | -2+            | Minimal (+10%) | +10% quality, no material/time savings  |
| **No match**     | N/A            | None           | Base rates, potential penalties         |

**Example:**

- **Recipe:** `Crafting/Smithing/Weapons/Swords` (depth 4)
- **Swordsmith** (specialization depth 4): **Exact match** → Full synergy
- **Weaponsmith** (depth 3): **Parent match** → Partial synergy
- **Blacksmith** (depth 2): **Grandparent match** → Minimal synergy
- **Alchemist** (no match): **No match** → Base rates

## Implementation Checklist

All recipes MUST have:

- ✅ **Single hierarchical tag** - ONLY the deepest/most specific tag
- ✅ **Profession field** - For UI categorization (blacksmithing, alchemy, etc.)
- ✅ **Difficulty field** - For skill level prerequisites (basic, intermediate, advanced)
- ✅ **Materials list** - With links to authoritative material pages
- ✅ **Crafting requirements** - Workspace, tools, time
- ✅ **Metadata fields** - For secondary categorization (armor_type, meal_type, effect, etc.)
- ✅ **Output specification** - Quantity, quality, value

### Tag Depth Calculation

```
"Crafting" = depth 1
"Crafting/Smithing" = depth 2
"Crafting/Smithing/Weapons" = depth 3
"Crafting/Smithing/Weapons/Swords" = depth 4
"Crafting/Smithing/Weapons/Swords/Journeyman" = depth 5
```

**Rule:** Count slashes, add 1. Verify tag exists in official hierarchy.

## Benefits Over Tier-Based Organization

1. **Natural Organization** - Recipes grouped by function (swords, stews, robes) not artificial tiers
2. **Automatic Access Control** - Class tag depth determines visible recipes (no manual gating)
3. **Proper Filtering** - Single-tag rule enables clean recipe lists by specialization
4. **Progressive Discovery** - Higher-tier classes see more recipes as depth increases
5. **Consistent System** - Same tag architecture as skills, spells, and class features
6. **Extensible** - Easy to add new specializations without reorganizing folders
7. **Cross-Discipline via Consolidation** - Hybrid classes inherit multiple tag paths naturally

## Related Content

- **Crafting System:** [Crafting Progression](crafting-progression.md)
- **Skill System:** [Skill Tags & Progression](../../progression/skill-tags.md)
- **Class System:** [Consolidation Classes](../../classes/consolidation/)
