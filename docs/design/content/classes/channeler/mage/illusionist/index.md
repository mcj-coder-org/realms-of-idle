---
title: Illusionist
gdd_ref: systems/class-system-gdd.md#advanced-classes
parent: classes/channeler/mage/index.md
tree_tier: 3
---

# Illusionist

## Tags

> **Required Section:** Tags enable compile-time safe tag access in C# code and drive the skill/class/recipe systems.

### C# Reference

**Strongly Typed Tags:** Use these tags in C# code for compile-time safety.

| Tag Path              | C# Reference                          | Purpose                  |
| --------------------- | ------------------------------------- | ------------------------ |
| `Magic/Illusion/Mind` | `SkillTags.Magic.Illusion.Mind.Value` | Illusionist class access |
| `Magic/Illusion`      | `SkillTags.Magic.Illusion.Value`      | Illusion magic           |

**How to find class tags:**

1. Check `ClassDefinition.GrantedTags` in class definition files (e.g., `src/CozyFantasyRpg/Content/Classes/MagicClasses.cs`)
2. Verify against `src/CozyFantasyRpg/Shared/SkillTags.cs` for exact C# reference

### Tag Access

This class requires the following tags for access/synergy:

| Tag                   | Depth | Classes With Access                    |
| --------------------- | ----- | -------------------------------------- |
| `Magic/Illusion/Mind` | 3     | Illusionist, Mage (depth 2 only)       |
| `Magic/Illusion`      | 2     | Illusionist, Mage, Illusion specialist |

**Note:** Tag depth determines which classes can access this content. See [Tag System](../../../../systems/content/tag-system/index.md) for details.

---

## Lore

### Illusion Mastery

Illusionists are mages who have devoted themselves entirely to the mastery of illusion magic. They understand deception not as mere trickery, but as a fundamental force - a study of perception, reality, and the boundary between what is and what appears to be. Through years of study and practice, they learn to commune with perception itself, bending minds and senses to their will while respecting the power of belief and consciousness.

The path to Illusion begins with understanding deception's dual nature. Illusions deceive, yes, but they also reveal truth about perception, create opportunities, and protect through misdirection. True illusionists harness both aspects. They learn that illusions follow laws of perception, belief strength, and magical resonance. By understanding these laws, they create with surgical precision or unleash overwhelming deceptions with devastating force.

Becoming an Illusionist requires more than spell knowledge. It demands a certain personality - those drawn to illusion tend toward creativity, subtle thinking, and understanding of perspective. The greatest illusionists carry the insight of shifted perspective in their spirit as surely as they command illusions. They face challenges through creative solutions, solve problems through perception manipulation, and inspire others through revealed truth.

### Combat Style

Illusionists dominate battlefields through deception and perception control. They open combat by rendering themselves invisible or confusing enemy perceptions, then strike from unexpected angles or manipulate enemy decisions. Their spells provide both offensive pressure and defensive utility through tactical deception.

Unlike pure destruction mages who rely on direct power, illusionists excel at tactical positioning and enemy disruption. Their illusions create confusion that cascades into tactical mistakes. Invisibility and misdirection allow positioning advantage. Enemy demoralization reduces effectiveness. Battles that depend on direct confrontation become disadvantageous when illusionists control perception and confusion.

Defensively, illusionists use deception to protect themselves. Invisibility provides complete evasion. Illusions of decoys absorb attention and attacks. They control space through perception manipulation, creating zones of confusion that threaten enemies.

In group settings, illusionists function as deception specialists and perception controllers. They provide tactical positioning advantage to allies, manipulate enemy perception to confuse and demoralize, and maintain deceptive presence throughout battles. Smart allies learn to position near illusionists for tactical misdirection. Enemies struggle against coordinated deception and confusion.

---

## Mechanics

### Prerequisites

| Requirement     | Value                                            |
| --------------- | ------------------------------------------------ |
| XP Threshold    | 50,000 XP from illusion magic actions            |
| Related Classes | [Mage](../) (Tier 2)                             |
| Tag Access      | `Magic/Illusion/Mind` (full)                     |
| Unlock Trigger  | Master illusion spells and understand perception |

### Requirements

| Requirement       | Value                                 |
| ----------------- | ------------------------------------- |
| Primary Attribute | INT (Intelligence), CHA (Charisma)    |
| Starting Level    | 1                                     |
| Tools Required    | Spell focus, illusion knowledge, mana |
| Recommended Items | Mind-attuned staff, Perception items  |

### Stats

#### Base Class Stats

| Level | HP Bonus | Mana Bonus | Trait                  |
| ----- | -------- | ---------- | ---------------------- |
| 1     | +8       | +26        | Apprentice Illusionist |
| 10    | +20      | +70        | Journeyman Illusionist |
| 25    | +48      | +155       | Master Illusionist     |
| 50    | +95      | +275       | Legendary Illusionist  |

#### Combat Bonuses

| Class Level | Illusion Power | Deception Effectiveness | Invisibility Duration | Efficiency |
| ----------- | -------------- | ----------------------- | --------------------- | ---------- |
| 1-9         | +15%           | +20%                    | +15%                  | -10%       |
| 10-24       | +35%           | +40%                    | +30%                  | -5%        |
| 25-49       | +60%           | +70%                    | +60%                  | +0%        |
| 50+         | +100%          | +120%                   | +100%                 | +15%       |

### Starting Skills

| Skill                     | Type   | Effect                                   |
| ------------------------- | ------ | ---------------------------------------- |
| Illusion Mastery (Lesser) | Tiered | Illusion power and deception strength    |
| Mind Shield (Lesser)      | Tiered | Mental protection and perception defense |

### Progression Paths

Illusionist is a terminal Tier 3 specialization. No further progression paths available. Illusionists may consolidate with other classes to unlock new capabilities, but cannot advance further within the Mage tree.

### Tracked Actions

Actions that grant XP to the Illusionist class:

| Action Category     | Specific Actions                         | XP Value             |
| ------------------- | ---------------------------------------- | -------------------- |
| Illusion Casting    | Cast illusion spells in combat           | 10-70 per spell      |
| Deception           | Use illusions to deceive enemies         | 20-100 per encounter |
| Invisibility        | Maintain invisibility in tactical combat | 15-80 per usage      |
| Illusion Mastery    | Use Illusion Mastery skill effectively   | 20-100 per session   |
| Mind Control        | Use mind-affecting illusions in battle   | 15-90 per battle     |
| Mass Invisibility   | Cast Mass Invisibility spell             | 50-200 per cast      |
| Nightmare Effects   | Use Nightmare spell for control          | 20-100 per nightmare |
| Legendary Deception | Achieve major illusion control feats     | 100-500 per victory  |

### Interactions

| System                                                       | Interaction                         |
| ------------------------------------------------------------ | ----------------------------------- |
| [Magic System](../../../systems/magic/magic-system/index.md) | Illusion element specialization     |
| [Combat](../../../systems/combat/combat-resolution/index.md) | Tactical deception and control      |
| [Party](../../../systems/combat/party-mechanics/index.md)    | Deception specialist, scout support |
| [Equipment](../../../systems/character/index.md)             | Mind-attuned magical items          |

---

## Related Content

- **Prerequisite Class:** [Mage](../)
- **Skills:** [Illusion Mastery](../../skills/tiered/illusion-mastery/index.md), [Mind Shield](../../skills/tiered/mind-shield/index.md)
- **Spells:** [Mass Invisibility](../../spells/illusion/mass-invisibility/index.md), [Nightmare](../../spells/illusion/nightmare/index.md)
- **System:** [Magic System](../../../systems/magic/magic-system/index.md)
