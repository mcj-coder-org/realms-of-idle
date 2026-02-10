---
title: Blacksmith
gdd_ref: systems/class-system-gdd.md#specialization-classes
parent: classes/crafter/index.md
tree_tier: 2
---

# Blacksmith

## Overview

The Blacksmith is a Tier 2 crafting class specializing in metalworking. Masters of the forge, blacksmiths create weapons, armor, shields, and tools from raw ore and refined metals. This class requires 5000 XP in metal forging and smithing actions to unlock.

## Tags

> **Required Section:** Tags enable compile-time safe tag access in C# code and drive the skill/class/recipe systems.

### C# Reference

**Strongly Typed Tags:** Use these tags in C# code for compile-time safety.

| Tag Path                   | C# Reference                         | Purpose               |
| -------------------------- | ------------------------------------ | --------------------- |
| `Crafting`                 | `SkillTags.Crafting.Value`           | Base crafting access  |
| `Crafting/Smithing`        | `SkillTags.Crafting.Smithing.Value`  | Smithing skill access |
| `Crafting/Smithing/Weapon` | `SkillTags.Crafting.Smithing.Weapon` | Weapon crafting       |
| `Crafting/Smithing/Armor`  | `SkillTags.Crafting.Smithing.Armor`  | Armor crafting        |
| `Crafting/Smithing/Shield` | `SkillTags.Crafting.Smithing.Shield` | Shield crafting       |
| `Crafting/Smithing/Tools`  | `SkillTags.Crafting.Smithing.Tools`  | Tool crafting         |

**How to find class tags:**

1. Check `ClassDefinition.GrantedTags` in class definition files (e.g., `src/CozyFantasyRpg/Content/Classes/CraftingClasses.cs`)
2. Verify against `src/CozyFantasyRpg/Shared/SkillTags.cs` for exact C# reference

### Tag Access

This class grants the following tags:

| Tag                        | Depth | Granted By                 |
| -------------------------- | ----- | -------------------------- |
| `Crafting`                 | 1     | Blacksmith, All Crafters   |
| `Crafting/Smithing`        | 2     | Blacksmith                 |
| `Crafting/Smithing/Weapon` | 3     | Weaponsmith specialization |
| `Crafting/Smithing/Armor`  | 3     | Armorsmith specialization  |
| `Crafting/Smithing/Shield` | 3     | Blacksmith                 |
| `Crafting/Smithing/Tools`  | 3     | Blacksmith                 |

**Note:** Tag depth determines access to recipes and stations. See [Tag System](././././systems/content/tag-system/index.md) for details.

---

## Lore

### Origin

The blacksmith stands at the heart of civilization - where others gather and trade, the blacksmith creates. The rhythmic clang of hammer on anvil is the heartbeat of every settlement, a sound that means safety, prosperity, and progress. From the first crude copper tools to legendary adamantine blades, every piece of metalwork begins with a smith who knows fire, metal, and patience.

The Ironhold dwarves claim they invented smithing when the world was young, teaching the craft to other races as an act of charity. Human smiths laugh at this, pointing to ancient forges in the Rusthills that predate the dwarven kingdoms. Elven metalworkers simply smile and continue their work, creating pieces of such beauty that arguments about origin seem petty. The truth is simpler: anywhere there's metal and fire, someone will learn to forge.

### In the World

Every village needs a blacksmith. Not just for weapons and armor, though those matter when wolves prowl close or bandits grow bold, but for the hundred small necessities of daily life. Nails for building, shoes for horses, hinges for doors, pots for cooking, tools for farming - civilization literally holds together with the blacksmith's work.

Children gather outside the smithy on hot afternoons, fascinated by the shower of sparks and the transformation of glowing metal. Farmers arrive with broken plowshares, hoping for quick repair. Adventurers commission weapons, describing their needs with grand gestures. The blacksmith listens to all of them, mentally calculating materials, time, and fair prices.

Young apprentices start by working the bellows until their arms ache, learning to judge heat by color before they're allowed to touch hammer or tongs. The journey from pumping bellows to master smith takes years of burnt fingers, aching backs, and countless failures. But there's something magical about seeing a lump of raw ore unlocks eligibility for perfectly balanced sword, a sturdy horseshoe, or a delicate set of decorative hinges. Each piece bears the smith's mark - their reputation forged as surely as the metal itself.

---

## Specifications

### Prerequisites

| Requirement        | Value                                                           |
| ------------------ | --------------------------------------------------------------- |
| XP Threshold       | 5,000 XP from metal forging tracked actions                     |
| Related Foundation | [Crafter](././foundation/index.md) (optional, provides bonuses) |
| Tag Depth Access   | 2 levels (e.g., `Crafting/Smithing`)                            |

### Requirements

| Requirement       | Value                                  |
| ----------------- | -------------------------------------- |
| Unlock Trigger    | Forge metal items using a proper forge |
| Primary Attribute | STR (Strength), PAT (Patience)         |
| Starting Level    | 1                                      |
| Tools Required    | Forge, anvil, hammer, tongs            |

### Stats

#### Base Class Stats

| Level | HP Bonus | Stamina Bonus | Trait            |
| ----- | -------- | ------------- | ---------------- |
| 1     | +5       | +10           | Apprentice Smith |
| 10    | +15      | +30           | Journeyman Smith |
| 25    | +30      | +60           | Master Smith     |
| 50    | +50      | +100          | Legendary Smith  |

#### Crafting Bonuses

| Class Level | Quality Bonus | Speed Bonus | Material Efficiency | Durability Bonus |
| ----------- | ------------- | ----------- | ------------------- | ---------------- |
| 1-9         | +0%           | 0%          | 0%                  | +0%              |
| 10-24       | +10%          | +10%        | +5%                 | +10%             |
| 25-49       | +25%          | +25%        | +15%                | +25%             |
| 50+         | +50%          | +50%        | +30%                | +50%             |

#### Starting Skill

| Skill         | Type    | Effect                                    |
| ------------- | ------- | ----------------------------------------- |
| Basic Forging | Passive | Can forge basic metal items (copper, tin) |

#### Starting Skills (Auto-Awarded on Class Acceptance)

Skills automatically awarded when accepting this class:

| Skill        | Tier     | Documentation                                                  | Reasoning                                       |
| ------------ | -------- | -------------------------------------------------------------- | ----------------------------------------------- |
| Heat Reading | Lesser   | [Heat Reading](././skills/tiered/heat-reading/index.md)        | Can't forge without temperature control         |
| Metal Sense  | Mechanic | [Metal Sense](././skills/mechanic-unlock/metal-sense/index.md) | Understanding metals is fundamental to smithing |

#### Synergy Skills

Skills with strong synergies for Blacksmith:

**General Crafting Skills**:

- [Artisan's Focus](././skills/tiered/artisans-focus/index.md) - Lesser/Greater/Enhanced - Enhanced crafting concentration
- [Rapid Crafting](././skills/tiered/rapid-crafting/index.md) - Lesser/Greater/Enhanced - Faster creation speed
- [Material Intuition](././skills/mechanic-unlock/material-intuition/index.md) - Lesser/Greater/Enhanced - Assess material quality instantly
- [Resource Efficiency](././skills/tiered/resource-efficiency/index.md) - Lesser/Greater/Enhanced - Chance to save materials
- [Masterwork Chance](././skills/tiered/masterwork-chance/index.md) - Lesser/Greater/Enhanced - Increased chance for exceptional quality
- [Repair Mastery](././skills/tiered/repair-mastery/index.md) - Lesser/Greater/Enhanced - Expert repair capabilities
- [Tool Bond](././skills/mechanic-unlock/tool-bond/index.md) - Lesser/Greater/Enhanced - Enhanced effectiveness with favored tools

**Smithing Skills**:

- [Metal Sense](././skills/mechanic-unlock/metal-sense/index.md) - Mechanic - Intuitive understanding of metals
- [Perfect Temper](././skills/tiered/perfect-temper/index.md) - Lesser/Greater/Enhanced - Superior heat treatment for blades
- [Forge Fire](././skills/mechanic-unlock/forge-fire/index.md) - Lesser/Greater/Enhanced - Precise temperature control
- [Weapon Enhancement](././skills/mechanic-unlock/weapon-enhancement/index.md) - Lesser/Greater/Enhanced - Improve weapon balance and effectiveness
- [Armor Fitting](././skills/mechanic-unlock/armor-fitting/index.md) - Lesser/Greater/Enhanced - Custom-fit armor for maximum protection

**Note**: All skills listed have strong synergies because they are core smithing skills. Characters without Blacksmith class can still learn these skills but progress at base rate (1x XP) without effectiveness bonuses.

#### Synergy Bonuses

Blacksmith provides context-specific bonuses to smithing-related skills based on logical specialization:

**Core Trade Skills** (Direct specialization - Strong synergy):

- **Heat Reading**: Essential for temperature-critical forging
- Faster learning: 2x XP from forging actions (scales with class level)
- Better effectiveness: +25% accuracy at Blacksmith 15, +35% at Blacksmith 30+
- Reduced cost: -25% stamina at Blacksmith 15, -35% at Blacksmith 30+
- **Efficient Smith**: Directly tied to material management in smithing
- Faster learning: 2x XP from crafting actions
- Better save rate: +30% material save rate at Blacksmith 15
- Lower failure waste: -50% material loss on failures
- **Repair Expert**: Core skill for maintaining metalwork
- Faster learning: 2x XP from repair actions
- Better quality: +20% repair quality at Blacksmith 15
- Lower material cost: Additional -20% material cost at Blacksmith 15
- **Alloy Knowledge**: Fundamental to advanced smithing
- Faster learning: 2x XP from alloy experimentation
- Better results: +25% alloy quality at Blacksmith 15
- More discovery: +50% chance to discover new alloys

**Synergy Strength Scales with Class Level**:

| Blacksmith Level | XP Multiplier | Effectiveness Bonus | Cost Reduction | Example: Heat Reading |
| ---------------- | ------------- | ------------------- | -------------- | --------------------- |
| Level 5          | 1.5x (+50%)   | +15%                | -15% stamina   | Good but moderate     |
| Level 10         | 1.75x (+75%)  | +20%                | -20% stamina   | Strong improvements   |
| Level 15         | 2.0x (+100%)  | +25%                | -25% stamina   | Excellent synergy     |
| Level 30+        | 2.5x (+150%)  | +35%                | -35% stamina   | Masterful synergy     |

**Example Progression**:

A Blacksmith 15 learning Heat Reading:

- Performs 50 forging actions (earning 2x XP = 1000 XP total, vs 500 XP for non-Blacksmith)
- Heat Reading available at level-up after just 500 XP (vs 1500 XP for non-smithing class)
- When using Heat Reading: Base +15% accuracy becomes +40% accuracy (base +25% synergy)
- Stamina cost: 7.5 stamina instead of 10 (25% reduction)

#### Tracked Actions

Actions that grant XP to the Blacksmith class:

| Action Category     | Specific Actions                                                  | XP Value          |
| ------------------- | ----------------------------------------------------------------- | ----------------- |
| Forging             | Forge weapons, armor, tools, nails, horseshoes, decorative pieces | 10-100 per item   |
| Smelting            | Smelt ore into ingots, create alloys                              | 5-20 per batch    |
| Repairing           | Repair weapons, armor, tools, metal objects                       | 5-50 per repair   |
| Refining            | Refine material quality (poor → standard → fine)                  | 10-30 per batch   |
| Experimentation     | Attempt new alloys, test techniques, discover recipes             | 20-50 per attempt |
| Maintenance         | Maintain forge, anvil, tools                                      | 2-5 per session   |
| Training            | Teach apprentices, demonstrate techniques                         | 15-40 per session |
| Commission Work     | Complete customer orders                                          | Bonus +50%        |
| Masterwork Creation | Create Masterwork quality item                                    | 200-500           |

#### Class Consolidation

See [Class Consolidation System](./././systems/character/class-consolidation/index.md) for full mechanics.

| Consolidation Path                       | Requirements                                 | Result Class | Tier |
| ---------------------------------------- | -------------------------------------------- | ------------ | ---- |
| [Artisan](./consolidation/index.md)      | Blacksmith + any Crafter class               | Artisan      | 1    |
| [Weaponsmith](./consolidation/index.md)  | Blacksmith + specialization focus            | Weaponsmith  | 1    |
| [Armorsmith](./consolidation/index.md)   | Blacksmith + specialization focus            | Armorsmith   | 1    |
| [Master Smith](./consolidation/index.md) | Weaponsmith/Armorsmith both Master           | Master Smith | 2    |
| [Warden](./consolidation/index.md)       | Blacksmith + [Adventurer](./combat/index.md) | Warden       | 1    |
| [Artificer](./consolidation/index.md)    | Master in 3+ crafting classes                | Artificer    | 3    |

### Interactions

| System                                                              | Interaction                                                                |
| ------------------------------------------------------------------- | -------------------------------------------------------------------------- |
| [Crafting](./././systems/crafting/crafting-progression/index.md)    | Primary metal crafting class; unlocks metal recipes                        |
| [Economy](./././systems/economy/index.md)                           | Can establish smithy business; NPC customers                               |
| [Reputation](./././systems/social/factions-reputation/index.md)     | Quality work builds reputation; affects commissions                        |
| [Combat](./././systems/combat/index.md)                             | Can craft own weapons/armor; maintain equipment                            |
| [Settlement](./././systems/world/settlements/index.md)              | Smithies provide settlement production bonuses                             |
| [Gathering](./././systems/crafting/gathering/index.md)              | Synergy: [Miner](./gathering/index.md) + Blacksmith = efficient production |
| [Enchanting](./././systems/crafting/enchantment-mechanics/index.md) | Creates base items for enchanters to work on                               |

---

## Progression

### Specializations

- [Weaponsmith](./weaponsmith/) - Weapon forging specialist
- [Armorsmith](./armorsmith/) - Armor forging specialist

---

## Related Content

- **Requires:** Forge access, [Hammer](././item/tool/hammer/), [Tongs](././item/tool/tongs/)
- **Materials Used:**
- [Metals](././material/metal/) - [Copper](././material/metal/copper/), [Iron](././material/metal/iron/), [Steel](././material/metal/steel/)
- [Fuel](././material/fuel/coal/) - Coal for smelting
- [Components](././item/component/) - [Leather Strip](././item/component/leather-strip/), [Wood Handle](././item/component/wood-handle/)
- **Creates:**
- [Weapons](././item/weapon/) - [Dagger](././item/weapon/dagger/), [Longsword](././item/weapon/longsword/)
- [Tools](././item/tool/) - [Pickaxe](././item/tool/pickaxe/), [Axe](././item/tool/axe/), [Hammer](././item/tool/hammer/)
- **Recipes:**
- [Smelting](././recipe/smelting/) - [Smelt Ore](././recipe/smelting/smelt-ore/)
- [Blacksmithing](././recipe/blacksmithing/) - [Dagger](././recipe/blacksmithing/dagger/), [Longsword](././recipe/blacksmithing/longsword/)
- **Synergy Classes:** [Miner](./gatherer/miner/), [Enchanter](./channeler/), [Merchant](./trader/merchant/)
- **See Also:** [Crafting Progression](./././systems/crafting/crafting-progression/index.md)

## Related Tags

- `Crafting`
- `Crafting/Smithing`
- `Metal Working`
- `Forging`

## Examples

### Basic Weapon Crafting

A Level 10 Blacksmith can forge a basic iron longsword:

1. Smelt iron ore at a forge (requires `Crafting/Smithing` tag)
2. Select the longsword recipe
3. Apply Quality Bonuses: +10% quality, +10% speed
4. Result: Iron Longsword with above-average durability

### Specialization Path

At Level 25, the Blacksmith can choose a specialization:

- **Weaponsmith:** Enhanced weapon crafting (+25% quality on weapons)
- **Armorsmith:** Enhanced armor crafting (+25% quality on armor)
- **General Smith:** +15% quality on all metal items
