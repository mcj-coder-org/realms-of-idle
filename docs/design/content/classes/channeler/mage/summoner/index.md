---
title: Summoner
gdd_ref: systems/class-system-gdd.md#advanced-classes
parent: classes/channeler/mage/index.md
tree_tier: 3
---

# Summoner

## Tags

> **Required Section:** Tags enable compile-time safe tag access in C# code and drive the skill/class/recipe systems.

### C# Reference

**Strongly Typed Tags:** Use these tags in C# code for compile-time safety.

| Tag Path                      | C# Reference                                  | Purpose               |
| ----------------------------- | --------------------------------------------- | --------------------- |
| `Magic/Conjuration/Summoning` | `SkillTags.Magic.Conjuration.Summoning.Value` | Summoner class access |
| `Magic/Conjuration`           | `SkillTags.Magic.Conjuration.Value`           | Conjuration magic     |
| `Planes/Access`               | `SkillTags.Planes.Access.Value`               | Planar travel         |

**How to find class tags:**

1. Check `ClassDefinition.GrantedTags` in class definition files (e.g., `src/CozyFantasyRpg/Content/Classes/MagicClasses.cs`)
2. Verify against `src/CozyFantasyRpg/Shared/SkillTags.cs` for exact C# reference

### Tag Access

This class requires the following tags for access/synergy:

| Tag                           | Depth | Classes With Access                    |
| ----------------------------- | ----- | -------------------------------------- |
| `Magic/Conjuration/Summoning` | 3     | Summoner, Mage (depth 2 only)          |
| `Magic/Conjuration`           | 2     | Summoner, Mage, Conjuration specialist |
| `Planes/Access`               | 2     | Summoner, Planar specialists           |

**Note:** Tag depth determines which classes can access this content. See [Tag System](../../../../systems/content/tag-system/index.md) for details.

---

## Lore

### Summon Mastery

Summoners are mages who have devoted themselves entirely to the mastery of summoning magic. They understand the planes not as distant abstractions, but as real dimensions populated by powerful beings - a fundamental study of other realities, planar binding, and conjuration. Through years of study and practice, they learn to commune with planar entities themselves, binding powerful allies to their will while respecting the immense power that resides beyond the mortal realm.

The path to Summoning begins with understanding planar nature's dual aspect. Summoned beings are powerful, yes, but they also demand proper binding, strategic use, and careful control. True summoners harness both aspects. They learn that summoning follows laws of planar affinity, binding strength, and magical resonance. By understanding these laws, they conjure with surgical precision or unleash powerful entities with devastating force.

Becoming a Summoner requires more than spell knowledge. It demands a certain personality - those drawn to summoning tend toward leadership, responsibility, and understanding of power dynamics. The greatest summoners carry the weight of command in their spirit as surely as they bind planar entities. They face challenges through strategic deployment of allies, solve problems through diverse conjured support, and inspire others through powerful presence.

### Combat Style

Summoners dominate battlefields through powerful conjured allies and planar support. They open combat by summoning powerful beings to engage enemies directly, then maintain advantage through planar reinforcement and strategic binding. Their spells provide both offensive pressure and defensive support through conjured minion superiority.

Unlike pure destruction mages who rely on personal power, summoners excel at minion-based tactics and planar enhancement. Their conjured allies grow stronger as they persist. Planar binding improves minion capability while reducing enemy effectiveness. Superior conjured allies overcome powerful foes. Battles that seem overwhelming turn favorable when summoners deploy powerful entities.

Defensively, summoners use conjured allies to protect themselves. Planar barriers shield the summoner. Summoned entities absorb hits meant for the summoner. They control space through powerful minion positioning, creating zones of planar presence that intimidate enemies.

In group settings, summoners function as powerful minion controllers and planar connectors. They provide powerful conjured support to allies, enhance ally capabilities through planar binding, and maintain superior conjured presence throughout battles. Smart allies learn to position near summoned entities for tactical advantage. Enemies struggle against coordinated powerful minions.

---

## Mechanics

### Prerequisites

| Requirement     | Value                                                 |
| --------------- | ----------------------------------------------------- |
| XP Threshold    | 50,000 XP from summoning magic actions                |
| Related Classes | [Mage](../) (Tier 2)                                  |
| Tag Access      | `Magic/Conjuration/Summoning` (full)                  |
| Unlock Trigger  | Master summoning spells and understand planar binding |

### Requirements

| Requirement       | Value                                  |
| ----------------- | -------------------------------------- |
| Primary Attribute | INT (Intelligence), CHA (Charisma)     |
| Starting Level    | 1                                      |
| Tools Required    | Spell focus, summoning knowledge, mana |
| Recommended Items | Planar-attuned staff, Binding items    |

### Stats

#### Base Class Stats

| Level | HP Bonus | Mana Bonus | Trait               |
| ----- | -------- | ---------- | ------------------- |
| 1     | +10      | +26        | Apprentice Summoner |
| 10    | +26      | +70        | Journeyman Summoner |
| 25    | +58      | +155       | Master Summoner     |
| 50    | +110     | +275       | Legendary Summoner  |

#### Combat Bonuses

| Class Level | Conjuration Power | Minion Strength | Planar Binding | Efficiency |
| ----------- | ----------------- | --------------- | -------------- | ---------- |
| 1-9         | +15%              | +25%            | +15%           | -10%       |
| 10-24       | +35%              | +50%            | +30%           | -5%        |
| 25-49       | +60%              | +100%           | +60%           | +0%        |
| 50+         | +100%             | +150%           | +100%          | +15%       |

### Starting Skills

| Skill                   | Type   | Effect                                  |
| ----------------------- | ------ | --------------------------------------- |
| Summon Mastery (Lesser) | Tiered | Summoning power and minion control      |
| Planar Binding (Lesser) | Tiered | Binding strength and minion enhancement |

### Progression Paths

Summoner is a terminal Tier 3 specialization. No further progression paths available. Summoners may consolidate with other classes to unlock new capabilities, but cannot advance further within the Mage tree.

### Tracked Actions

Actions that grant XP to the Summoner class:

| Action Category     | Specific Actions                             | XP Value             |
| ------------------- | -------------------------------------------- | -------------------- |
| Summoning           | Summon planar entities in combat             | 10-70 per spell      |
| Conjuration         | Maintain and control conjured allies         | 20-100 per encounter |
| Planar Control      | Use planar binding effectively               | 15-80 per usage      |
| Summon Mastery      | Use Summon Mastery skill effectively         | 20-100 per session   |
| Minion Combat       | Coordinate conjured allies in battle         | 15-90 per battle     |
| Greater Summoning   | Successfully cast Greater Summon spell       | 50-200 per cast      |
| Lord Summoning      | Summon powerful entities with Elemental Lord | 20-100 per lord      |
| Legendary Conjuring | Achieve major conjuration feats              | 100-500 per victory  |

### Interactions

| System                                                       | Interaction                           |
| ------------------------------------------------------------ | ------------------------------------- |
| [Magic System](../../../systems/magic/magic-system/index.md) | Conjuration element specialization    |
| [Combat](../../../systems/combat/combat-resolution/index.md) | Minion superiority and planar control |
| [Party](../../../systems/combat/party-mechanics/index.md)    | Minion controller, ally supporter     |
| [Equipment](../../../systems/character/index.md)             | Planar-attuned magical items          |

---

## Related Content

- **Prerequisite Class:** [Mage](../)
- **Skills:** [Summon Mastery](../../skills/tiered/summon-mastery/index.md), [Planar Binding](../../skills/tiered/planar-binding/index.md)
- **Spells:** [Greater Summon](../../spells/conjuration/greater-summon/index.md), [Elemental Lord](../../spells/conjuration/elemental-lord/index.md)
- **System:** [Magic System](../../../systems/magic/magic-system/index.md)
