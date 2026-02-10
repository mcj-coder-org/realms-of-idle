---
title: 'Pyromancer'
type: 'class'
category: 'magic'
tier: 3
prerequisite_xp: 50000
prerequisite_actions: fire magic mastery through mage progression
summary: 'Fire magic specialist with mastery over infernal flames and burning magic'
tags:
  - Magic/Elemental/Fire
---

# Pyromancer

## Tags

> **Required Section:** Tags enable compile-time safe tag access in C# code and drive the skill/class/recipe systems.

### C# Reference

**Strongly Typed Tags:** Use these tags in C# code for compile-time safety.

| Tag Path               | C# Reference                           | Purpose                 |
| ---------------------- | -------------------------------------- | ----------------------- |
| `Magic/Elemental/Fire` | `SkillTags.Magic.Elemental.Fire.Value` | Pyromancer class access |
| `Magic/Elemental`      | `SkillTags.Magic.Elemental.Value`      | Elemental magic         |

**How to find class tags:**

1. Check `ClassDefinition.GrantedTags` in class definition files (e.g., `src/CozyFantasyRpg/Content/Classes/MagicClasses.cs`)
2. Verify against `src/CozyFantasyRpg/Shared/SkillTags.cs` for exact C# reference

### Tag Access

This class requires the following tags for access/synergy:

| Tag                    | Depth | Classes With Access             |
| ---------------------- | ----- | ------------------------------- |
| `Magic/Elemental/Fire` | 3     | Pyromancer, Mage (depth 2 only) |
| `Magic/Elemental`      | 2     | Pyromancer, Mage, Elementalist  |

**Note:** Tag depth determines which classes can access this content. See [Tag System](../../../../systems/content/tag-system.md) for details.

---

## Lore

### Fire Mastery

Pyromancers are mages who have devoted themselves entirely to the mastery of fire magic. They understand flame not as mere destruction, but as a fundamental force of nature - a living element demanding respect, skill, and intimate understanding. Through years of study and practice, they learn to commune with fire itself, bending its will to their own while respecting its raw, untamable power.

The path to Pyromancy begins with understanding fire's dual nature. Fire destroys, yes, but it also transforms, purifies, and illuminates. True pyromancers harness both aspects. They learn that fire is not chaos - it follows laws of physics, thermodynamics, and magical resonance. By understanding these laws, they manipulate flame with surgical precision or unleash it with cataclysmic force.

Becoming a Pyromancer requires more than spell knowledge. It demands a certain personality - those drawn to fire tend toward passion, intensity, and drive. The greatest pyromancers burn with inner conviction as hot as their external flames. They face challenges head-on, solve problems through direct action, and inspire others through sheer force of will. Yet the best also learn restraint - to know when a small flame suffices rather than an inferno.

### Combat Style

Pyromancers dominate battlefields through overwhelming fire magic. They open combat by igniting the entire enemy formation with area-of-effect spells, then maintain pressure with burning auras that damage enemies simply by proximity. Their spells chain effects - initial explosions ignite enemies, burning damage spreads, and follow-up casts finish weakened foes.

Unlike pure destruction mages who prioritize massive single hits, pyromancers excel at sustained magical pressure. Their burning effects accumulate over time. Multiple enemies caught in their auras take escalating damage. Spells stack burning durations. Encounters that seem manageable turn catastrophic as fire damage compounds and spreads.

Defensively, pyromancers use fire to protect themselves. Protective barriers that reflect fire damage back to attackers deter melee engagement. Personal flames that damage nearby enemies create zones of denial. They control space through fire, forcing opponents into suboptimal positions or accepting damage.

In group settings, pyromancers function as area-denial specialists. They clear crowds of weak enemies, soften powerful foes for allies, and control battlefield space. Smart allies learn to position away from pyromancer areas of effect. Enemies don't have that luxury - they either endure the flames or flee.

---

## Mechanics

### Prerequisites

| Requirement     | Value                                   |
| --------------- | --------------------------------------- |
| XP Threshold    | 50,000 XP from fire magic actions       |
| Related Classes | [Mage](../) (Tier 2)                    |
| Tag Access      | `Magic/Elemental/Fire` (full)           |
| Unlock Trigger  | Master fire spells and understand flame |

### Requirements

| Requirement       | Value                                     |
| ----------------- | ----------------------------------------- |
| Primary Attribute | INT (Intelligence), WIS (Wisdom)          |
| Starting Level    | 1                                         |
| Tools Required    | Spell focus, fire magic knowledge, mana   |
| Recommended Items | Flame-attuned staff, Fire-resistant items |

### Stats

#### Base Class Stats

| Level | HP Bonus | Mana Bonus | Trait                 |
| ----- | -------- | ---------- | --------------------- |
| 1     | +8       | +25        | Apprentice Pyromancer |
| 10    | +25      | +65        | Journeyman Pyromancer |
| 25    | +55      | +140       | Master Pyromancer     |
| 50    | +100     | +250       | Legendary Pyromancer  |

#### Combat Bonuses

| Class Level | Fire Spell Power | Burning Duration | Area Damage | Efficiency |
| ----------- | ---------------- | ---------------- | ----------- | ---------- |
| 1-9         | +15%             | +25%             | +10%        | -10%       |
| 10-24       | +35%             | +50%             | +25%        | -5%        |
| 25-49       | +60%             | +100%            | +50%        | +0%        |
| 50+         | +100%            | +150%            | +100%       | +15%       |

### Starting Skills

| Skill                  | Type   | Effect                             |
| ---------------------- | ------ | ---------------------------------- |
| Flame Mastery (Lesser) | Tiered | Fire spell power and efficiency    |
| Burning Aura (Lesser)  | Tiered | Passive fire damage to nearby foes |

### Progression Paths

Pyromancer is a terminal Tier 3 specialization. No further progression paths available. Pyromancers may consolidate with other classes to unlock new capabilities, but cannot advance further within the Mage tree.

### Tracked Actions

Actions that grant XP to the Pyromancer class:

| Action Category    | Specific Actions                         | XP Value             |
| ------------------ | ---------------------------------------- | -------------------- |
| Fire Spell Casting | Cast fire spells in combat               | 10-70 per spell      |
| Area Destruction   | Eliminate multiple enemies with fire AoE | 20-150 per encounter |
| Burning Damage     | Defeat enemies using burning effects     | 15-80 per kill       |
| Flame Mastery      | Use Flame Mastery skill effectively      | 20-100 per session   |
| Aura Mastery       | Maintain Burning Aura during combat      | 15-90 per battle     |
| Inferno Casting    | Successfully cast Inferno spell          | 50-200 per cast      |
| Fire Reflection    | Reflect fire damage with Flame Shield    | 20-100 per reflect   |
| Legendary Infernos | Achieve massive area destruction feats   | 100-500 per victory  |

### Interactions

| System                                                 | Interaction                          |
| ------------------------------------------------------ | ------------------------------------ |
| [Magic System](../../../systems/magic/magic-system.md) | Fire element specialization          |
| [Combat](../../../systems/combat/combat-resolution.md) | Area denial and burning DoT damage   |
| [Party](../../../systems/combat/party-mechanics.md)    | Area damage dealer, crowd controller |
| [Equipment](../../../systems/character/index.md)       | Fire-attuned magical items           |

---

## Related Content

- **Prerequisite Class:** [Mage](../)
- **Skills:** [Flame Mastery](../../skills/tiered/flame-mastery.md), [Burning Aura](../../skills/tiered/burning-aura.md)
- **Spells:** [Inferno](../../spells/destruction/inferno.md), [Flame Shield](../../spells/abjuration/flame-shield.md)
- **System:** [Magic System](../../../systems/magic/magic-system.md)
