---
title: 'Electromancer'
type: 'class'
category: 'magic'
tier: 3
prerequisite_xp: 50000
prerequisite_actions: lightning magic mastery through mage progression
prerequisite_classes:
  - Mage
summary: 'Lightning magic specialist with mastery over electrical discharge and chain effects'
tags:
  - Magic/Elemental/Lightning
---

# Electromancer

## Tags

> **Required Section:** Tags enable compile-time safe tag access in C# code and drive the skill/class/recipe systems.

### C# Reference

**Strongly Typed Tags:** Use these tags in C# code for compile-time safety.

| Tag Path                    | C# Reference                                | Purpose                    |
| --------------------------- | ------------------------------------------- | -------------------------- |
| `Magic/Elemental/Lightning` | `SkillTags.Magic.Elemental.Lightning.Value` | Electromancer class access |
| `Magic/Elemental`           | `SkillTags.Magic.Elemental.Value`           | Elemental magic            |

**How to find class tags:**

1. Check `ClassDefinition.GrantedTags` in class definition files (e.g., `src/CozyFantasyRpg/Content/Classes/MagicClasses.cs`)
2. Verify against `src/CozyFantasyRpg/Shared/SkillTags.cs` for exact C# reference

### Tag Access

This class requires the following tags for access/synergy:

| Tag                         | Depth | Classes With Access                |
| --------------------------- | ----- | ---------------------------------- |
| `Magic/Elemental/Lightning` | 3     | Electromancer, Mage (depth 2 only) |
| `Magic/Elemental`           | 2     | Electromancer, Mage, Elementalist  |

**Note:** Tag depth determines which classes can access this content. See [Tag System](../../../../systems/content/tag-system/index.md) for details.

---

## Lore

### Lightning Mastery

Electromancers command the raw power of lightning and electrical forces, harnessing nature's most violent element. They stand apart from other Mages through their mastery of electricity - the element of speed, chain reactions, and stunning power. Where Pyromancers reign through sustained infernos and gradual destruction, Electromancers strike through sudden voltage surges that leap between enemies.

The path to Electromancer begins with understanding lightning's unique properties. Unlike fire's patient burn or frost's gradual freeze, lightning moves at impossible speeds, jumping from target to target in cascading chains. Electromancers learn to amplify these effects, guiding electrical discharge through multiple enemies, overloading defenses with voltage surges, and creating electrical barriers that shock those foolish enough to touch them.

Becoming an Electromancer demands more than casting lightning spells. It requires internalization of electrical principles - how electricity flows through conductive paths, how charge builds and disperses, how energy chains from one point to another. Electromancers develop intuitive understanding of these forces, learning to shape them with precision. They study how electricity behaves differently against armored foes versus unprotected targets, how moisture affects conductivity, how multiple chains interact with each other.

The profession attracts those who prefer precise, aggressive magic over gentle casting. Electromancers act quickly, think rapidly, and strike with overwhelming electrical force. They excel at adapting to changing battlefield conditions - redirecting chains, shifting discharge patterns, overwhelming enemy defenses through rapid succession of lightning attacks. Their combat style emphasizes momentum and chain reactions, building electrical charge into devastating combined effects.

### Combat Style

Electromancers fight through rapid strikes and cascading electrical effects. Their core approach: deliver initial electrical discharge to primary target, use that contact point to chain electricity to additional enemies, build increasingly complex electrical effects as battles progress. Unlike Battlemages who blend weapons and spells equally, Electromancers channel most energy into perfecting their magical assault. Like other specialist Mages, they sacrifice martial versatility for devastating electrical mastery.

In combat, Electromancers control the battlefield through electrical dominance. Chain Lightning bounces between enemies as chains initiate, stunning some while dealing massive damage to others. Thunderstorm rains electrical violence across entire areas, chaining to multiple targets simultaneously. Lightning Shield creates protective barriers that shock attackers while defending the caster. Each spell builds upon previous damage, creating overlapping electrical effects that escalate combat intensity.

Electromancers prefer engaging at medium range, maintaining distance while unleashing electrical devastation. They use terrain features understanding how electricity conducts - water terrain increases chain effectiveness, metal amplifies damage, strategic positioning lets them hit maximum numbers of enemies. They manage mana carefully, knowing that higher-tier lightning spells consume significant resources but deliver proportional devastation.

Advanced Electromancers develop combat awareness around electrical chains. They position themselves to maximize chain bounces, considering distance between enemies and chain transmission mechanics. They stack Thunderstorm into Chain Lightning combinations for maximum effect. They maintain Lightning Shield throughout extended battles, creating continuous electrical deterrent against physical attacks. The greatest Electromancers turn entire enemy formations into electrical conduits, chains of electricity arcing between every combatant while they stand immune at the center.

---

## Mechanics

### Prerequisites

| Requirement            | Value                                                  |
| ---------------------- | ------------------------------------------------------ |
| XP Threshold           | 50,000 XP from lightning magic actions tracked actions |
| Related Tier 2 Classes | [Mage](../) (required)                                 |
| Tag Depth Access       | 3+ levels (e.g., `Magic/Elemental/Lightning`)          |

### Requirements

| Requirement       | Value                                       |
| ----------------- | ------------------------------------------- |
| Unlock Trigger    | Master lightning magic through Mage class   |
| Primary Attribute | INT (Intelligence), WIS (Wisdom)            |
| Starting Level    | 1                                           |
| Tools Required    | Spell focus, mana pool, electrical training |

### Stats

#### Base Class Stats

| Level | HP Bonus | Mana Bonus | Trait                    |
| ----- | -------- | ---------- | ------------------------ |
| 1     | +5       | +35        | Apprentice Electromancer |
| 10    | +15      | +105       | Journeyman Electromancer |
| 25    | +35      | +245       | Master Electromancer     |
| 50    | +65      | +450       | Legendary Electromancer  |

#### Combat Bonuses

| Class Level | Lightning Damage | Chain Bounces | Casting Speed | Mana Efficiency |
| ----------- | ---------------- | ------------- | ------------- | --------------- |
| 1-9         | +10%             | +1            | +5%           | -10%            |
| 10-24       | +25%             | +2            | +15%          | -20%            |
| 25-49       | +50%             | +3            | +30%          | -30%            |
| 50+         | +85%             | +5            | +50%          | -40%            |

#### Starting Skills

| Skill                                                                    | Type    | Effect                                     |
| ------------------------------------------------------------------------ | ------- | ------------------------------------------ |
| Lightning Affinity                                                       | Passive | Natural connection to electrical magic     |
| [Storm Mastery](../../skills/tiered/storm-mastery/index.md) (Lesser)     | Tiered  | Lightning spell power and chain efficiency |
| [Chain Lightning](../../skills/tiered/chain-lightning/index.md) (Lesser) | Tiered  | Electricity chains between foes            |

#### Synergy Skills

Skills that have strong synergies with Electromancer. These skills can be learned by any character, but Electromancers gain significant bonuses (2x XP acquisition, enhanced effectiveness, reduced costs). Higher Electromancer levels provide stronger synergy bonuses.

**Core Lightning Skills**:

- [Storm Mastery](../../skills/tiered/storm-mastery/index.md) - Lesser/Greater/Enhanced - Increased lightning spell power
- [Chain Lightning](../../skills/tiered/chain-lightning/index.md) - Lesser/Greater/Enhanced - Bounce electricity between enemies
- Electrical Expertise - Lesser/Greater/Enhanced - Deepen understanding of electrical forces
- Overload - Lesser/Greater/Enhanced - Build electrical charge for devastating release
- Static Field - Lesser/Greater/Enhanced - Create electrical aura around caster
- Conductive Path - Lesser/Greater/Enhanced - Guide chains through terrain
- Voltage Amplification - Lesser/Greater/Enhanced - Enhance electrical damage output
- Quick Discharge - Lesser/Greater/Enhanced - Accelerate spell casting
- Electrical Resistance - Lesser/Greater/Enhanced - Reduce lightning damage taken
- Chain Optimization - Lesser/Greater/Enhanced - Maximize chain bounce efficiency

#### Tracked Actions

Actions that grant XP to the Electromancer class:

| Action Category        | Specific Actions                                | XP Value                |
| ---------------------- | ----------------------------------------------- | ----------------------- |
| Lightning Spellcasting | Cast lightning spells effectively               | 15-100 per spell        |
| Electrical Chaining    | Execute successful chain lightning bounces      | 20-80 per action        |
| Thunderstorm Mastery   | Deploy area-effect lightning attacks            | 25-120 per cast         |
| Protective Discharge   | Use lightning for defense and protection        | 15-70 per action        |
| Chain Optimization     | Maximize chain bounces in single attack         | 20-90 per combat        |
| Electrical Dominance   | Eliminate threats through lightning mastery     | 30-150 per enemy        |
| Rapid Casting          | Execute multiple lightning spells in succession | 15-85 per round         |
| Stun Mastery           | Stun enemies with electrical discharge          | 20-80 per stun          |
| Legendary Lightning    | Achieve battlefield electrical dominance        | 100-500 per achievement |

### Interactions

| System                                                           | Interaction                                      |
| ---------------------------------------------------------------- | ------------------------------------------------ |
| [Combat](../../../systems/combat/combat-resolution/index.md)     | Electrical devastation; area effects with chains |
| [Magic System](../../../systems/character/magic-system/index.md) | Specializes in lightning and chain effects       |
| [Party](../../../systems/combat/party-mechanics/index.md)        | Ranged damage dealer; area control specialist    |
| [Equipment](../../../systems/character/index.md)                 | Uses spell focus and magical items               |
| [Tactics](../../../systems/combat/index.md)                      | Chain reaction combat style                      |

---

## Related Content

- **Requires:** Spell focus, mana pool, electrical training
- **Prerequisites:** Consolidates from [Mage](../)
- **Starting Skills:** [Storm Mastery](../../skills/tiered/storm-mastery/index.md), [Chain Lightning](../../skills/tiered/chain-lightning/index.md)
- **Spells:** [Thunderstorm](../../spells/destruction/thunderstorm/index.md), [Lightning Shield](../../spells/destruction/lightning-shield/index.md)
- **System:** [Magic System](../../../systems/character/magic-system/index.md)
- **See Also:** [Combat System](../../../systems/combat/combat-resolution/index.md)
