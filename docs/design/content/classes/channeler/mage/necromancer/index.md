---
title: Necromancer
gdd_ref: systems/class-system-gdd.md#advanced-classes
parent: classes/channeler/mage/index.md
tree_tier: 3
---

# Necromancer

## Tags

> **Required Section:** Tags enable compile-time safe tag access in C# code and drive the skill/class/recipe systems.

### C# Reference

**Strongly Typed Tags:** Use these tags in C# code for compile-time safety.

| Tag Path                   | C# Reference                               | Purpose                  |
| -------------------------- | ------------------------------------------ | ------------------------ |
| `Magic/Conjuration/Undead` | `SkillTags.Magic.Conjuration.Undead.Value` | Necromancer class access |
| `Magic/Necromancy`         | `SkillTags.Magic.Necromancy.Value`         | Death magic              |

**How to find class tags:**

1. Check `ClassDefinition.GrantedTags` in class definition files (e.g., `src/CozyFantasyRpg/Content/Classes/MagicClasses.cs`)
2. Verify against `src/CozyFantasyRpg/Shared/SkillTags.cs` for exact C# reference

### Tag Access

This class requires the following tags for access/synergy:

| Tag                        | Depth | Classes With Access              |
| -------------------------- | ----- | -------------------------------- |
| `Magic/Conjuration/Undead` | 3     | Necromancer, Mage (depth 2 only) |
| `Magic/Necromancy`         | 2     | Necromancer, Mage, Channeler     |

**Note:** Tag depth determines which classes can access this content. See [Tag System](../../../../systems/content/tag-system/index.md) for details.

---

## Lore

### Undead Mastery

Necromancers are mages who have devoted themselves entirely to the mastery of death magic. They understand undeath not as evil corruption, but as a fundamental force - a study of mortality, transformation, and the boundary between life and death. Through years of study and practice, they learn to commune with the forces of death itself, commanding undead and manipulating life force while respecting the delicate balance that separates the living from the dead.

The path to Necromancy begins with understanding death's dual nature. Death ends life, yes, but it also transforms, preserves, and opens possibilities. True necromancers harness both aspects. They learn that undeath follows laws of soul binding, life force, and magical resonance. By understanding these laws, they manipulate death magic with surgical precision or unleash it with devastating force.

Becoming a Necromancer requires more than spell knowledge. It demands a certain personality - those drawn to death magic tend toward contemplation, understanding of sacrifice, and acceptance of inevitable change. The greatest necromancers carry the weight of mortality in their spirit as surely as they command undeath. They face challenges through strategic thinking, solve problems through sacrifice when necessary, and inspire others through pragmatic acceptance.

### Combat Style

Necromancers dominate battlefields through undead minions and life force manipulation. They open combat by summoning armies of undead to overwhelm enemies, then maintain advantage through life drain effects that weaken foes while strengthening themselves. Their spells provide both offensive pressure and defensive utility through minion control.

Unlike pure destruction mages who rely on direct damage, necromancers excel at minion-based tactics and vampiric effects. Their undead servants multiply in strength as they persist. Life drain effects reduce enemy capability while improving necromancer durability. Coordinated minion tactics overcome powerful single foes. Battles that seem hopeless turn favorable when necromancers command undead armies.

Defensively, necromancers use undeath to protect themselves. Death shrouds reduce incoming damage. Undead minions absorb hits meant for the necromancer. They control space through minion positioning, creating zones of undead presence that threaten enemies.

In group settings, necromancers function as minion controllers and life force managers. They provide minion support to allies, drain enemy capabilities while empowering allies, and maintain undead presence throughout battles. Smart allies learn to position near necromancer minions for tactical advantage. Enemies struggle against coordinated undead forces.

---

## Mechanics

### Prerequisites

| Requirement     | Value                                      |
| --------------- | ------------------------------------------ |
| XP Threshold    | 50,000 XP from death magic actions         |
| Related Classes | [Mage](../) (Tier 2)                       |
| Tag Access      | `Magic/Conjuration/Undead` (full)          |
| Unlock Trigger  | Master death spells and understand undeath |

### Requirements

| Requirement       | Value                                     |
| ----------------- | ----------------------------------------- |
| Primary Attribute | INT (Intelligence), WIS (Wisdom)          |
| Starting Level    | 1                                         |
| Tools Required    | Spell focus, death magic knowledge, mana  |
| Recommended Items | Death-attuned staff, Undead-binding items |

### Stats

#### Base Class Stats

| Level | HP Bonus | Mana Bonus | Trait                  |
| ----- | -------- | ---------- | ---------------------- |
| 1     | +10      | +24        | Apprentice Necromancer |
| 10    | +24      | +65        | Journeyman Necromancer |
| 25    | +55      | +145       | Master Necromancer     |
| 50    | +105     | +260       | Legendary Necromancer  |

#### Combat Bonuses

| Class Level | Death Spell Power | Minion Control | Life Drain Potency | Efficiency |
| ----------- | ----------------- | -------------- | ------------------ | ---------- |
| 1-9         | +15%              | +20%           | +15%               | -10%       |
| 10-24       | +35%              | +40%           | +30%               | -5%        |
| 25-49       | +60%              | +70%           | +60%               | +0%        |
| 50+         | +100%             | +120%          | +100%              | +15%       |

### Starting Skills

| Skill                   | Type   | Effect                            |
| ----------------------- | ------ | --------------------------------- |
| Undead Mastery (Lesser) | Tiered | Undead summoning and control      |
| Life Drain (Lesser)     | Tiered | Vampiric damage and life stealing |

### Progression Paths

Necromancer is a terminal Tier 3 specialization. No further progression paths available. Necromancers may consolidate with other classes to unlock new capabilities, but cannot advance further within the Mage tree.

### Tracked Actions

Actions that grant XP to the Necromancer class:

| Action Category     | Specific Actions                       | XP Value             |
| ------------------- | -------------------------------------- | -------------------- |
| Death Spell Casting | Cast death spells in combat            | 10-70 per spell      |
| Summoning           | Summon and control undead minions      | 20-100 per encounter |
| Life Drain          | Use vampiric effects to damage enemies | 15-80 per usage      |
| Undead Mastery      | Use Undead Mastery skill effectively   | 20-100 per session   |
| Minion Combat       | Coordinate minions during battle       | 15-90 per battle     |
| Army Summoning      | Successfully cast Army of Dead spell   | 50-200 per cast      |
| Death Protection    | Use Death Shroud for defense           | 20-100 per shroud    |
| Legendary Armies    | Achieve major undead control feats     | 100-500 per victory  |

### Interactions

| System                                                       | Interaction                            |
| ------------------------------------------------------------ | -------------------------------------- |
| [Magic System](../../../systems/magic/magic-system/index.md) | Death element specialization           |
| [Combat](../../../systems/combat/combat-resolution/index.md) | Minion control and life force drain    |
| [Party](../../../systems/combat/party-mechanics/index.md)    | Minion controller, tactical specialist |
| [Equipment](../../../systems/character/index.md)             | Death-attuned magical items            |

---

## Related Content

- **Prerequisite Class:** [Mage](../)
- **Skills:** [Undead Mastery](../../skills/tiered/undead-mastery/index.md), [Life Drain](../../skills/tiered/life-drain/index.md)
- **Spells:** [Army of Dead](../../spells/necromancy/army-of-dead/index.md), [Death Shroud](../../spells/transmutation/death-shroud/index.md)
- **System:** [Magic System](../../../systems/magic/magic-system/index.md)
