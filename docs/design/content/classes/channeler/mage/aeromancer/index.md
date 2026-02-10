---
title: 'Aeromancer'
type: 'class'
category: 'magic'
tier: 3
prerequisite_xp: 50000
prerequisite_actions: air magic mastery through mage progression
summary: 'Air magic specialist with mastery over winds, storms, and aerial forces'
tags:
  - Magic/Elemental/Air
---

# Aeromancer

## Tags

> **Required Section:** Tags enable compile-time safe tag access in C# code and drive the skill/class/recipe systems.

### C# Reference

**Strongly Typed Tags:** Use these tags in C# code for compile-time safety.

| Tag Path              | C# Reference                          | Purpose                 |
| --------------------- | ------------------------------------- | ----------------------- |
| `Magic/Elemental/Air` | `SkillTags.Magic.Elemental.Air.Value` | Aeromancer class access |
| `Magic/Elemental`     | `SkillTags.Magic.Elemental.Value`     | Elemental magic         |

**How to find class tags:**

1. Check `ClassDefinition.GrantedTags` in class definition files (e.g., `src/CozyFantasyRpg/Content/Classes/MagicClasses.cs`)
2. Verify against `src/CozyFantasyRpg/Shared/SkillTags.cs` for exact C# reference

### Tag Access

This class requires the following tags for access/synergy:

| Tag                   | Depth | Classes With Access             |
| --------------------- | ----- | ------------------------------- |
| `Magic/Elemental/Air` | 3     | Aeromancer, Mage (depth 2 only) |
| `Magic/Elemental`     | 2     | Aeromancer, Mage, Elementalist  |

**Note:** Tag depth determines which classes can access this content. See [Tag System](../../../../systems/content/tag-system/index.md) for details.

---

## Lore

### Wind Mastery

Aeromancers are mages who have devoted themselves entirely to the mastery of air magic. They understand wind not as mere emptiness, but as a living element with boundless energy - a fundamental force of motion, freedom, and transformation. Through years of study and practice, they learn to commune with the winds themselves, bending air currents to their will while respecting the untamed power that shapes the world.

The path to Aeromancy begins with understanding air's dual nature. Wind moves swiftly, yes, but it also creates structure, carries, and endures. True aeromancers harness both aspects. They learn that wind follows laws of physics, pressure gradients, and magical resonance. By understanding these laws, they manipulate air with surgical precision or unleash it with devastating force.

Becoming an Aeromancer requires more than spell knowledge. It demands a certain personality - those drawn to air tend toward agility, adaptability, and boundless curiosity. The greatest aeromancers carry the freedom of the wind in their spirit as surely as they command air currents. They face challenges through swift adaptation, solve problems through creative approaches, and inspire others through dynamic presence.

### Combat Style

Aeromancers dominate battlefields through mobility and ranged attacks. They open combat with devastating hurricane winds that strike at range, then maintain aerial advantage through levitation and wind-enhanced mobility. Their spells provide both offensive pressure and defensive utility through movement enhancement.

Unlike pure destruction mages who prioritize area effects, aeromancers excel at ranged precision and mobility. Their wind attacks strike from unexpected angles. Movement enhancement allows repositioning during combat. Aerial advantages compound as they control positioning. Encounters that depend on static defense become disadvantageous when aeromancers control the skies.

Defensively, aeromancers use wind to protect themselves. Wind barriers deflect incoming attacks. Flight and levitation provide positional advantage. They control space through mobility, forcing opponents into difficult positions or accepting movement penalties.

In group settings, aeromancers function as mobile strikers and repositioning specialists. They provide movement enhancement to allies, strike from unexpected angles, and maintain aerial advantage. Smart allies learn to benefit from aeromancer wind effects. Enemies struggle to catch mobile aeromancers.

---

## Mechanics

### Prerequisites

| Requirement     | Value                                 |
| --------------- | ------------------------------------- |
| XP Threshold    | 50,000 XP from air magic actions      |
| Related Classes | [Mage](../) (Tier 2)                  |
| Tag Access      | `Magic/Elemental/Air` (full)          |
| Unlock Trigger  | Master air spells and understand wind |

### Requirements

| Requirement       | Value                                   |
| ----------------- | --------------------------------------- |
| Primary Attribute | INT (Intelligence), DEX (Dexterity)     |
| Starting Level    | 1                                       |
| Tools Required    | Spell focus, air magic knowledge, mana  |
| Recommended Items | Air-attuned staff, Wind-resistant items |

### Stats

#### Base Class Stats

| Level | HP Bonus | Mana Bonus | Trait                 |
| ----- | -------- | ---------- | --------------------- |
| 1     | +10      | +22        | Apprentice Aeromancer |
| 10    | +22      | +60        | Journeyman Aeromancer |
| 25    | +50      | +135       | Master Aeromancer     |
| 50    | +95      | +240       | Legendary Aeromancer  |

#### Combat Bonuses

| Class Level | Air Spell Power | Movement Speed | Ranged Accuracy | Efficiency |
| ----------- | --------------- | -------------- | --------------- | ---------- |
| 1-9         | +15%            | +15%           | +10%            | -10%       |
| 10-24       | +35%            | +30%           | +25%            | -5%        |
| 25-49       | +60%            | +50%           | +50%            | +0%        |
| 50+         | +100%           | +80%           | +100%           | +15%       |

### Starting Skills

| Skill                 | Type   | Effect                                 |
| --------------------- | ------ | -------------------------------------- |
| Wind Mastery (Lesser) | Tiered | Air spell power and efficiency         |
| Levitation (Lesser)   | Tiered | Enhanced movement and aerial advantage |

### Progression Paths

Aeromancer is a terminal Tier 3 specialization. No further progression paths available. Aeromancers may consolidate with other classes to unlock new capabilities, but cannot advance further within the Mage tree.

### Tracked Actions

Actions that grant XP to the Aeromancer class:

| Action Category      | Specific Actions                         | XP Value               |
| -------------------- | ---------------------------------------- | ---------------------- |
| Air Spell Casting    | Cast air spells in combat                | 10-70 per spell        |
| Ranged Attacks       | Strike enemies from range with air magic | 20-100 per encounter   |
| Aerial Movement      | Use levitation and wind effects          | 15-80 per usage        |
| Wind Mastery         | Use Wind Mastery skill effectively       | 20-100 per session     |
| Mobility Combat      | Maintain levitation during combat        | 15-90 per battle       |
| Hurricane Casting    | Successfully cast Hurricane spell        | 50-200 per cast        |
| Movement Enhancement | Enhance ally movement with Wind Walk     | 20-100 per enhancement |
| Legendary Storms     | Achieve major aerial control feats       | 100-500 per victory    |

### Interactions

| System                                                       | Interaction                           |
| ------------------------------------------------------------ | ------------------------------------- |
| [Magic System](../../../systems/magic/magic-system/index.md) | Air element specialization            |
| [Combat](../../../systems/combat/combat-resolution/index.md) | Ranged damage and mobility control    |
| [Party](../../../systems/combat/party-mechanics/index.md)    | Striker specialist, mobility provider |
| [Equipment](../../../systems/character/index.md)             | Air-attuned magical items             |

---

## Related Content

- **Prerequisite Class:** [Mage](../)
- **Skills:** [Wind Mastery](../../skills/tiered/wind-mastery/index.md), [Levitation](../../skills/tiered/levitation/index.md)
- **Spells:** [Hurricane](../../spells/transmutation/hurricane/index.md), [Wind Walk](../../spells/transmutation/wind-walk/index.md)
- **System:** [Magic System](../../../systems/magic/magic-system/index.md)
