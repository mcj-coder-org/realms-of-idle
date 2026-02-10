---
title: Enchanter
gdd_ref: systems/class-system-gdd.md#specialization-classes
parent: classes/channeler/index.md
tree_tier: 2
---

# Enchanter

## Tags

> **Required Section:** Tags enable compile-time safe tag access in C# code and drive the skill/class/recipe systems.

### C# Reference

**Strongly Typed Tags:** Use these tags in C# code for compile-time safety.

| Tag Path                    | C# Reference                          | Purpose                   |
| --------------------------- | ------------------------------------- | ------------------------- |
| `Magic/Enchantment`         | `SkillTags.Magic.Enchantment.Value`   | Enchanter class access    |
| `Magic/Enchantment/Weapon`  | `SkillTags.Magic.Enchantment.Weapon`  | Weapon enchanting         |
| `Magic/Enchantment/Armor`   | `SkillTags.Magic.Enchantment.Armor`   | Armor enchanting          |
| `Magic/Enchantment/Utility` | `SkillTags.Magic.Enchantment.Utility` | Utility item enchanting   |
| `Crafting/Enchanting`       | `SkillTags.Crafting.Enchanting.Value` | General enchanting access |

**How to find class tags:**

1. Check `ClassDefinition.GrantedTags` in class definition files (e.g., `src/CozyFantasyRpg/Content/Classes/MagicClasses.cs`)
2. Verify against `src/CozyFantasyRpg/Shared/SkillTags.cs` for exact C# reference

### Tag Access

This class requires the following tags for access/synergy:

| Tag                   | Depth | Classes With Access                 |
| --------------------- | ----- | ----------------------------------- |
| `Magic/Enchantment`   | 2     | Enchanter, Mage, Channeler          |
| `Magic/Enchantment/*` | 3     | Enchanter (full access to sub-tags) |
| `Crafting/Enchanting` | 2     | Enchanter, Crafter                  |

**Note:** Tag depth determines which classes can access this content. See [Tag System](../../../systems/content/tag-system/index.md) for details.

---

## Lore

### Origin

Spells fade - healing dissipates, shields collapse, conjurations vanish. Enchantments endure. Enchanting is the art of binding magic permanently into physical objects, creating items that hold power indefinitely. A sword that never dulls. Armor that turns blades. A ring that grants strength. These aren't temporary magical effects but fundamental alterations to the items themselves. Enchanters transform mundane objects into magical treasures.

The craft emerged when early Mages discovered that certain materials could hold magical energy indefinitely. Metal accepted enchantments readily. Gems stored magical power. Specially prepared wood bonded with nature magic. Crystal focused and amplified effects. By carefully inscribing magical runes and channeling power through precise rituals, Enchanters learned to permanently alter items' properties. The process required both magical knowledge and crafting skill - combining spellcasting with metalworking, gem cutting, or other trades.

Modern Enchanting represents intersection between magic and craftsmanship. Enchanters must understand both the magical effects they're binding and the physical items receiving enchantments. They study material properties - which substances accept which magical energies, how item quality affects enchantment strength, what preparation methods optimize receptiveness. They master rune inscription, carving or etching precise symbols that channel and contain magical power. They develop the stamina and focus required for lengthy enchantment rituals.

### In the World

Every society values enchanted items. Warriors prize magical weapons and armor. Merchants desire bags holding more than physical space allows. Farmers want tools that never break. Nobles collect enchanted jewelry displaying wealth and power. This demand makes Enchanters valuable - skilled practitioners command high fees, and their work holds value indefinitely.

Most Enchanters specialize by item type and enchantment focus. Weapon Enchanters understand combat applications, creating blades that burn or armor that deflects. Utility Enchanters make practical items - self-cleaning clothes, lights that never dim, containers preserving food indefinitely. Jewelry Enchanters craft beautiful magical accessories - rings granting skills, necklaces providing protection. Each specialty requires different material knowledge and magical expertise.

The profession demands significant infrastructure. Enchanters need workshops with proper tools, materials, and safety measures - enchanting failures can be dangerous. They maintain libraries of rune patterns and enchantment formulas. They source quality base items since enchanting trash produces magical trash. They stockpile magical reagents - gems, rare metals, exotic substances providing specific enchantment properties. Successful Enchanters invest heavily in their operations.

Apprentice Enchanters spend years learning before attempting serious enchantments. They study magical theory, particularly how to bind and stabilize magical effects. They learn crafting skills appropriate to their specialization - smithing for weapons, lapidary for gems, leatherwork for armor. They practice rune inscription until their carving becomes perfect. They assist experienced Enchanters with preparations, observing master-level work before attempting it themselves.

Master Enchanters create legendary items - weapons that reshape battlefields, armor that seems invincible, artifacts with unique powers. Their workshops become sought destinations for wealthy clients commissioning custom enchantments. They develop proprietary techniques and signature enchantments distinguishing their work. Some focus on pushing enchanting boundaries, attempting effects others consider impossible. Their greatest creations become heirlooms passed through generations.

---

## Mechanics

### Prerequisites

| Requirement        | Value                                                 |
| ------------------ | ----------------------------------------------------- |
| XP Threshold       | 5,000 XP from enchanting tracked actions              |
| Related Foundation | [Channeler](../index.md) (optional, provides bonuses) |
| Tag Depth Access   | 2 levels (e.g., `[Magic/Enchantment]`)                |

### Requirements

| Requirement       | Value                                                    |
| ----------------- | -------------------------------------------------------- |
| Unlock Trigger    | Successfully enchant items with magic                    |
| Primary Attribute | INT (Intelligence), FIN (Finesse)                        |
| Starting Level    | 1                                                        |
| Tools Required    | Workshop, enchanting tools, magical reagents             |
| Prerequisite      | Often requires [Mage](./mage/index.md) or crafting class |

### Stats

#### Base Class Stats

| Level | HP Bonus | Mana Bonus | Trait                |
| ----- | -------- | ---------- | -------------------- |
| 1     | +5       | +15        | Apprentice Enchanter |
| 10    | +15      | +45        | Journeyman Enchanter |
| 25    | +30      | +95        | Master Enchanter     |
| 50    | +55      | +175       | Legendary Enchanter  |

#### Enchanting Bonuses

| Class Level | Enchant Power | Success Rate | Rune Precision | Item Quality |
| ----------- | ------------- | ------------ | -------------- | ------------ |
| 1-9         | +10%          | 70%          | +10%           | +5%          |
| 10-24       | +30%          | 85%          | +25%           | +15%         |
| 25-49       | +65%          | 95%          | +50%           | +35%         |
| 50+         | +120%         | 100%         | +85%           | +65%         |

#### Starting Skills

| Skill                                                                             | Type    | Effect                                              |
| --------------------------------------------------------------------------------- | ------- | --------------------------------------------------- |
| Basic Enchanting                                                                  | Passive | Can enchant items with minor permanent effects      |
| [Enchant Object](../../skills/mechanic-unlock/enchant-object/index.md) (Mechanic) | Active  | Core ability to imbue items with magical properties |
| [Spell Focus](../../skills/tiered/spell-focus/index.md) (Lesser)                  | Passive | Enhanced magical control for precise enchanting     |

#### Synergy Skills

Skills that have strong synergies with Enchanter. These skills can be learned by any character, but Enchanters gain significant bonuses (2x XP acquisition, enhanced effectiveness, reduced costs). Higher Enchanter levels provide stronger synergy bonuses.

**Core Magic Skills**:

- [Spell Focus](../../skills/tiered/spell-focus/index.md) - Lesser/Greater/Enhanced - Enhanced spell effectiveness
- [Mana Well](../../skills/tiered/mana-well/index.md) - Lesser/Greater/Enhanced - Expanded magical reserves
- [School Mastery](../../skills/mechanic-unlock/school-mastery/index.md) - Mechanic - Deep expertise in one school
- [Enchant Object](../../skills/mechanic-unlock/enchant-object/index.md) - Lesser/Greater/Enhanced - Imbue items with magic
- [Ritual Casting](../../skills/mechanic-unlock/ritual-casting/index.md) - Lesser/Greater/Enhanced - Perform elaborate magical rituals
- [Mana Transfer](../../skills/mechanic-unlock/mana-transfer/index.md) - Lesser/Greater/Enhanced - Share magical energy

**Core Crafting Skills**:

- [Artisan's Focus](../../skills/tiered/artisans-focus/index.md) - Lesser/Greater/Enhanced - Enhanced crafting concentration
- [Masterwork Chance](../../skills/tiered/masterwork-chance/index.md) - Lesser/Greater/Enhanced - Create superior quality items
- [Material Intuition](../../skills/mechanic-unlock/material-intuition/index.md) - Lesser/Greater/Enhanced - Understand material properties
- [Rapid Crafting](../../skills/tiered/rapid-crafting/index.md) - Lesser/Greater/Enhanced - Faster creation speed
- [Recipe Innovator](../../skills/passive-generator/recipe-innovator/index.md) - Lesser/Greater/Enhanced - Develop new techniques
- [Repair Mastery](../../skills/tiered/repair-mastery/index.md) - Lesser/Greater/Enhanced - Expert item repair
- [Resource Efficiency](../../skills/tiered/resource-efficiency/index.md) - Lesser/Greater/Enhanced - Minimize material waste
- [Signature Style](../../skills/mechanic-unlock/signature-style/index.md) - Lesser/Greater/Enhanced - Unique recognizable work
- [Tool Bond](../../skills/mechanic-unlock/tool-bond/index.md) - Lesser/Greater/Enhanced - Mastery with specific tools

**Core Enchanting Skills**:

- Enchantment Power - Lesser/Greater/Enhanced - Stronger enchant effects
- Rune Mastery - Lesser/Greater/Enhanced - Precise rune inscription
- Enchant Weapon - Lesser/Greater/Enhanced - Weapon combat enchants
- Enchant Armor - Lesser/Greater/Enhanced - Armor defense enchants
- Enchant Utility - Lesser/Greater/Enhanced - Utility item enchants
- Dual Enchantment - Lesser/Greater/Enhanced - Multiple effects per item
- Enchant Efficiency - Lesser/Greater/Enhanced - Reduced mana cost
- Quality Enhancement - Lesser/Greater/Enhanced - Improve base item quality
- Permanent Magic - Lesser/Greater/Enhanced - Extended enchantment duration
- Gem Integration - Lesser/Greater/Enhanced - Enhanced gem enchants
- Magical Aesthetics - Lesser/Greater/Enhanced - Visual effect control
- Curse Breaking - Lesser/Greater/Enhanced - Remove curse effects
- Artifact Creation - Epic - Create legendary artifacts

**Note**: All skills listed have strong synergies because they are core enchantment skills. Characters without Enchanter class can still learn these skills but progress at base rate (1x XP) without effectiveness bonuses.

#### Synergy Bonuses

Enchanter provides context-specific bonuses to enchanting-related skills based on logical specialization:

**Core Enchanting Skills** (Direct specialization - Strong synergy):

- **Enchantment Power**: Essential for imbuing items with magic
  - Faster learning: 2x XP from enchanting actions (scales with class level)
  - Better effectiveness: +25% enchant strength at Enchanter 15, +35% at Enchanter 30+
  - Reduced cost: -25% mana at Enchanter 15, -35% at Enchanter 30+
- **Rune Mastery**: Directly tied to magical inscription precision
  - Faster learning: 2x XP from rune inscription
  - Better accuracy: +30% rune precision at Enchanter 15
  - Lower failure: -50% chance of inscription errors
- **Material Knowledge**: Core skill for understanding enchantable materials
  - Faster learning: 2x XP from material study
  - Better insight: +20% material assessment at Enchanter 15
  - More discovery: +50% chance to identify rare material properties
- **Enchant Efficiency**: Fundamental to sustainable enchanting practice
  - Faster learning: 2x XP from mana management in enchanting
  - Better efficiency: +25% mana reduction at Enchanter 15
  - More stability: +50% enchantment stability

**Synergy Strength Scales with Class Level**:

| Enchanter Level | XP Multiplier | Effectiveness Bonus | Cost Reduction | Example: Enchant Power |
| --------------- | ------------- | ------------------- | -------------- | ---------------------- |
| Level 5         | 1.5x (+50%)   | +15%                | -15% mana      | Good but moderate      |
| Level 10        | 1.75x (+75%)  | +20%                | -20% mana      | Strong improvements    |
| Level 15        | 2.0x (+100%)  | +25%                | -25% mana      | Excellent synergy      |
| Level 30+       | 2.5x (+150%)  | +35%                | -35% mana      | Masterful synergy      |

**Example Progression**:

An Enchanter 15 learning Enchantment Power:

- Performs 50 enchanting actions (earning 2x XP = 1000 XP total, vs 500 XP for non-Enchanter)
- Enchantment Power available at level-up after just 500 XP (vs 1500 XP for non-enchanting class)
- When using Enchantment Power: Base +40% enchant strength becomes +65% enchant strength (base +25% synergy)
- Mana cost: 15 mana instead of 20 (25% reduction)

#### Tracked Actions

Actions that grant XP to the Enchanter class:

| Action Category      | Specific Actions                               | XP Value                |
| -------------------- | ---------------------------------------------- | ----------------------- |
| Item Enchanting      | Successfully enchant items                     | 15-100 per enchant      |
| Rune Inscription     | Inscribe magical runes accurately              | 5-30 per inscription    |
| Workshop Preparation | Prepare materials and workspace for enchanting | 10-40 per session       |
| Enchant Research     | Research new enchantment effects and formulas  | 30-150 per breakthrough |
| Quality Work         | Create high-quality enchantments               | 20-120 per item         |
| Client Commissions   | Complete custom enchantment orders             | 30-200 per commission   |
| Disenchanting        | Remove or break enchantments safely            | 10-60 per disenchant    |
| Material Sourcing    | Acquire rare magical reagents and materials    | 15-80 per acquisition   |
| Master Work          | Create exceptionally powerful enchantments     | 50-300 per creation     |
| Artifact Creation    | Develop legendary enchanted artifacts          | 150-600 per artifact    |

#### Class Consolidation

See [Class Consolidation System](../../../systems/character/class-consolidation/index.md) for full mechanics.

| Consolidation Path                                 | Requirements                                        | Result Class      | Tier |
| -------------------------------------------------- | --------------------------------------------------- | ----------------- | ---- |
| [Master Enchanter](../../consolidation/enchanter/) | Enchanter + multiple enchantment specializations    | Master Enchanter  | 1    |
| [Artificer](../../consolidation/index.md)          | Enchanter + [Blacksmith](../../crafter/blacksmith/) | Artificer         | 1    |
| [Rune Master](../../consolidation/index.md)        | Enchanter + rune magic focus                        | Rune Master       | 1    |
| [Legendary Artisan](../../consolidation/index.md)  | Multiple crafting/enchanting classes                | Legendary Artisan | 2    |

### Interactions

| System                                                              | Interaction                                    |
| ------------------------------------------------------------------- | ---------------------------------------------- |
| [Enchantment System](../../../systems/enchantment/)                 | Core enchanting mechanics                      |
| [Crafting](../../../systems/crafting/crafting-progression/index.md) | Requires quality base items to enchant         |
| [Magic](../../../systems/character/magic-system/index.md)           | Uses magical energy for enchantments           |
| [Item Quality](../../../systems/crafting/)                          | Base item quality affects enchantment strength |
| [Economy](../../../systems/economy/)                                | Enchanted items command premium prices         |
| [Materials](../../material/)                                        | Requires magical reagents and rare materials   |

---

## Related Content

- **Requires:** Workshop, enchanting tools, runes, magical reagents, base items
- **Equipment:** [Enchanting Bench](../../item/), [Rune Tools](../../item/tool/), [Magical Gems](../../material/), [Reagents](../../material/)
- **Prerequisite:** Often consolidates from [Mage](./mage/index.md) or crafting classes
- **Synergy Classes:** [Mage](./mage/index.md), [Blacksmith](../../crafter/blacksmith/), [Jeweler](../../crafter/jeweler/)
- **Consolidates To:** [Master Enchanter](../../consolidation/enchanter/), [Artificer](../../consolidation/), [Rune Master](../../consolidation/)
- **See Also:** [Enchantment System](../../../systems/enchantment/), [Crafting System](../../../systems/crafting/), [Item Quality](../../../systems/crafting/)
