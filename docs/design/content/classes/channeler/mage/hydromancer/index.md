---
title: 'Hydromancer'
type: 'class'
category: 'magic'
tier: 3
prerequisite_xp: 50000
prerequisite_actions: water magic mastery through mage progression
summary: 'Water magic specialist with mastery over tides, currents, and aquatic forces'
tags:
  - Magic/Elemental/Water
---

# Hydromancer

## Tags

> **Required Section:** Tags enable compile-time safe tag access in C# code and drive the skill/class/recipe systems.

### C# Reference

**Strongly Typed Tags:** Use these tags in C# code for compile-time safety.

| Tag Path                | C# Reference                            | Purpose                  |
| ----------------------- | --------------------------------------- | ------------------------ |
| `Magic/Elemental/Water` | `SkillTags.Magic.Elemental.Water.Value` | Hydromancer class access |
| `Magic/Elemental`       | `SkillTags.Magic.Elemental.Value`       | Elemental magic          |

**How to find class tags:**

1. Check `ClassDefinition.GrantedTags` in class definition files (e.g., `src/CozyFantasyRpg/Content/Classes/MagicClasses.cs`)
2. Verify against `src/CozyFantasyRpg/Shared/SkillTags.cs` for exact C# reference

### Tag Access

This class requires the following tags for access/synergy:

| Tag                     | Depth | Classes With Access              |
| ----------------------- | ----- | -------------------------------- |
| `Magic/Elemental/Water` | 3     | Hydromancer, Mage (depth 2 only) |
| `Magic/Elemental`       | 2     | Hydromancer, Mage, Elementalist  |

**Note:** Tag depth determines which classes can access this content. See [Tag System](../../../../systems/content/tag-system/index.md) for details.

---

## Lore

### Water Mastery

Hydromancers are mages who have devoted themselves entirely to the mastery of water magic. They understand water not as passive liquid, but as a living element with transformative power - a fundamental force of flow, adaptation, and healing. Through years of study and practice, they learn to commune with water itself, bending tides and currents to their will while respecting the vital essence that sustains all life.

The path to Hydromancy begins with understanding water's dual nature. Water flows gently, yes, but it also crashes with tremendous force, adapts to any shape, and nurtures life. True hydromancers harness both aspects. They learn that water follows laws of physics, buoyancy, and magical resonance. By understanding these laws, they manipulate water with surgical precision or unleash it with devastating force.

Becoming a Hydromancer requires more than spell knowledge. It demands a certain personality - those drawn to water tend toward emotional depth, adaptability, and nurturing instincts. The greatest hydromancers carry the compassion and flexibility of water in their spirit as surely as they command tides. They face challenges through understanding and adaptation, solve problems through lateral thinking, and inspire others through healing presence.

### Combat Style

Hydromancers dominate battlefields through adaptive spellcasting and healing support. They open combat with devastating tidal waves that strike multiple enemies, then maintain presence through healing and protective barriers. Their water magic provides both offensive pressure and defensive support through healing effects.

Unlike pure destruction mages who prioritize damage, hydromancers excel at balanced offense and defense. Their spells damage enemies while protecting allies. Healing effects sustain the party through extended encounters. Protective barriers stack to improve survivability. Battles that deplete a group become manageable when hydromancers maintain healing presence.

Defensively, hydromancers use water to protect themselves and allies. Water shields absorb damage and provide healing. Aquatic breathing allows exploration of submerged areas. They control space through water barriers, creating zones of protection and strategic advantage.

In group settings, hydromancers function as hybrid damage dealers and healers. They provide offensive pressure while maintaining party health, balance aggressive pushes with defensive retreats, and enable exploration of water-based areas. Smart allies learn to position near hydromancers for healing support. Enemies struggle against party-wide healing.

---

## Mechanics

### Prerequisites

| Requirement     | Value                                    |
| --------------- | ---------------------------------------- |
| XP Threshold    | 50,000 XP from water magic actions       |
| Related Classes | [Mage](../) (Tier 2)                     |
| Tag Access      | `Magic/Elemental/Water` (full)           |
| Unlock Trigger  | Master water spells and understand tides |

### Requirements

| Requirement       | Value                                      |
| ----------------- | ------------------------------------------ |
| Primary Attribute | INT (Intelligence), WIS (Wisdom)           |
| Starting Level    | 1                                          |
| Tools Required    | Spell focus, water magic knowledge, mana   |
| Recommended Items | Water-attuned staff, Water-resistant items |

### Stats

#### Base Class Stats

| Level | HP Bonus | Mana Bonus | Trait                  |
| ----- | -------- | ---------- | ---------------------- |
| 1     | +10      | +24        | Apprentice Hydromancer |
| 10    | +24      | +65        | Journeyman Hydromancer |
| 25    | +55      | +145       | Master Hydromancer     |
| 50    | +105     | +260       | Legendary Hydromancer  |

#### Combat Bonuses

| Class Level | Water Spell Power | Healing Potency | Defensive Barrier | Efficiency |
| ----------- | ----------------- | --------------- | ----------------- | ---------- |
| 1-9         | +15%              | +20%            | +15%              | -10%       |
| 10-24       | +35%              | +40%            | +30%              | -5%        |
| 25-49       | +60%              | +70%            | +60%              | +0%        |
| 50+         | +100%             | +120%           | +100%             | +15%       |

### Starting Skills

| Skill                      | Type   | Effect                              |
| -------------------------- | ------ | ----------------------------------- |
| Water Mastery (Lesser)     | Tiered | Water spell power and efficiency    |
| Aquatic Breathing (Lesser) | Tiered | Underwater exploration and survival |

### Progression Paths

Hydromancer is a terminal Tier 3 specialization. No further progression paths available. Hydromancers may consolidate with other classes to unlock new capabilities, but cannot advance further within the Mage tree.

### Tracked Actions

Actions that grant XP to the Hydromancer class:

| Action Category     | Specific Actions                            | XP Value             |
| ------------------- | ------------------------------------------- | -------------------- |
| Water Spell Casting | Cast water spells in combat                 | 10-70 per spell      |
| Healing Actions     | Heal allies with water magic                | 20-100 per encounter |
| Barrier Creation    | Create protective water barriers            | 15-80 per usage      |
| Water Mastery       | Use Water Mastery skill effectively         | 20-100 per session   |
| Healing Combat      | Maintain healing during combat              | 15-90 per battle     |
| Tidal Wave Casting  | Successfully cast Tidal Wave spell          | 50-200 per cast      |
| Shield Support      | Create protective shields with Water Shield | 20-100 per shield    |
| Legendary Tides     | Achieve major water magic feats             | 100-500 per victory  |

### Interactions

| System                                                       | Interaction                          |
| ------------------------------------------------------------ | ------------------------------------ |
| [Magic System](../../../systems/magic/magic-system/index.md) | Water element specialization         |
| [Combat](../../../systems/combat/combat-resolution/index.md) | Healing and defensive magic support  |
| [Party](../../../systems/combat/party-mechanics/index.md)    | Support specialist, healing provider |
| [Equipment](../../../systems/character/index.md)             | Water-attuned magical items          |

---

## Related Content

- **Prerequisite Class:** [Mage](../)
- **Skills:** [Water Mastery](../../skills/tiered/water-mastery/index.md), [Aquatic Breathing](../../skills/tiered/aquatic-breathing/index.md)
- **Spells:** [Tidal Wave](../../spells/destruction/tidal-wave/index.md), [Water Shield](../../spells/abjuration/water-shield/index.md)
- **System:** [Magic System](../../../systems/magic/magic-system/index.md)
