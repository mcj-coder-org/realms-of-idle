---
title: Geomancer
gdd_ref: systems/class-system-gdd.md#advanced-classes
parent: classes/channeler/mage/index.md
tree_tier: 3
---

# Geomancer

## Tags

> **Required Section:** Tags enable compile-time safe tag access in C# code and drive the skill/class/recipe systems.

### C# Reference

**Strongly Typed Tags:** Use these tags in C# code for compile-time safety.

| Tag Path                | C# Reference                            | Purpose                |
| ----------------------- | --------------------------------------- | ---------------------- |
| `Magic/Elemental/Earth` | `SkillTags.Magic.Elemental.Earth.Value` | Geomancer class access |
| `Magic/Elemental`       | `SkillTags.Magic.Elemental.Value`       | Elemental magic        |

**How to find class tags:**

1. Check `ClassDefinition.GrantedTags` in class definition files (e.g., `src/CozyFantasyRpg/Content/Classes/MagicClasses.cs`)
2. Verify against `src/CozyFantasyRpg/Shared/SkillTags.cs` for exact C# reference

### Tag Access

This class requires the following tags for access/synergy:

| Tag                     | Depth | Classes With Access            |
| ----------------------- | ----- | ------------------------------ |
| `Magic/Elemental/Earth` | 3     | Geomancer, Mage (depth 2 only) |
| `Magic/Elemental`       | 2     | Geomancer, Mage, Elementalist  |

**Note:** Tag depth determines which classes can access this content. See [Tag System](../../../../systems/content/tag-system/index.md) for details.

---

## Lore

### Earth Mastery

Geomancers are mages who have devoted themselves entirely to the mastery of earth magic. They understand stone not as inert matter, but as a living element with ancient power - a fundamental force of stability, endurance, and transformation. Through years of study and practice, they learn to commune with the earth itself, bending stone and soil to their will while respecting the primal strength that underlies the world.

The path to Geomancy begins with understanding earth's dual nature. Stone stands eternal, yes, but it also shifts, reshapes, and crumbles. True geomancers harness both aspects. They learn that stone follows laws of geology, pressure, and magical resonance. By understanding these laws, they manipulate terrain with surgical precision or unleash it with devastating force.

Becoming a Geomancer requires more than spell knowledge. It demands a certain personality - those drawn to earth tend toward steadfastness, reliability, and grounding presence. The greatest geomancers carry the weight of mountains in their spirit as surely as they command stone. They face challenges through resolute endurance, solve problems through stability and support, and inspire others through unwavering conviction.

### Combat Style

Geomancers dominate battlefields through controlling terrain and protecting allies. They open combat by reshaping the earth itself, creating barriers and traps that impede enemy movement. Stone walls rise to protect vulnerable allies while earthquake tremors topple distant foes. Their earth magic creates persistent zones of control.

Unlike pure destruction mages who prioritize massive single hits, geomancers excel at battlefield manipulation and defense. Their stone barriers endure over time. Multiple barriers stack to create impenetrable fortifications. Earthquake effects linger to hinder enemy advances. Encounters that seem overwhelming become manageable when geomancers reshape the battlefield.

Defensively, geomancers use earth to protect themselves and allies. Stone barriers absorb damage. Personal stone armor reinforces defenses. They control space through obstacles, forcing opponents into suboptimal positions or accepting terrain penalties.

In group settings, geomancers function as defensive specialists and terrain controllers. They protect vulnerable allies, create barriers for retreats, and manipulate terrain to disadvantage enemies. Smart allies learn to position behind geomancer barriers. Enemies must adapt or be trapped.

---

## Mechanics

### Prerequisites

| Requirement     | Value                                    |
| --------------- | ---------------------------------------- |
| XP Threshold    | 50,000 XP from earth magic actions       |
| Related Classes | [Mage](../) (Tier 2)                     |
| Tag Access      | `Magic/Elemental/Earth` (full)           |
| Unlock Trigger  | Master earth spells and understand stone |

### Requirements

| Requirement       | Value                                      |
| ----------------- | ------------------------------------------ |
| Primary Attribute | INT (Intelligence), CON (Constitution)     |
| Starting Level    | 1                                          |
| Tools Required    | Spell focus, earth magic knowledge, mana   |
| Recommended Items | Stone-attuned staff, Earth-resistant items |

### Stats

#### Base Class Stats

| Level | HP Bonus | Mana Bonus | Trait                |
| ----- | -------- | ---------- | -------------------- |
| 1     | +12      | +20        | Apprentice Geomancer |
| 10    | +30      | +55        | Journeyman Geomancer |
| 25    | +60      | +125       | Master Geomancer     |
| 50    | +110     | +225       | Legendary Geomancer  |

#### Combat Bonuses

| Class Level | Earth Spell Power | Barrier Durability | Area Control | Efficiency |
| ----------- | ----------------- | ------------------ | ------------ | ---------- |
| 1-9         | +15%              | +25%               | +10%         | -10%       |
| 10-24       | +35%              | +50%               | +25%         | -5%        |
| 25-49       | +60%              | +100%              | +50%         | +0%        |
| 50+         | +100%             | +150%              | +100%        | +15%       |

### Starting Skills

| Skill                  | Type   | Effect                              |
| ---------------------- | ------ | ----------------------------------- |
| Earth Mastery (Lesser) | Tiered | Earth spell power and efficiency    |
| Stone Skin (Lesser)    | Tiered | Passive damage reduction from stone |

### Progression Paths

Geomancer is a terminal Tier 3 specialization. No further progression paths available. Geomancers may consolidate with other classes to unlock new capabilities, but cannot advance further within the Mage tree.

### Tracked Actions

Actions that grant XP to the Geomancer class:

| Action Category      | Specific Actions                           | XP Value             |
| -------------------- | ------------------------------------------ | -------------------- |
| Earth Spell Casting  | Cast earth spells in combat                | 10-70 per spell      |
| Terrain Manipulation | Create barriers and obstacles              | 20-100 per encounter |
| Area Control         | Control large areas with earth magic       | 15-80 per usage      |
| Earth Mastery        | Use Earth Mastery skill effectively        | 20-100 per session   |
| Defense              | Block damage with Stone Skin during combat | 15-90 per battle     |
| Earthquake Casting   | Successfully cast Earthquake spell         | 50-200 per cast      |
| Barrier Creation     | Create protective walls with Stone Wall    | 20-100 per wall      |
| Legendary Tremors    | Achieve major terrain control feats        | 100-500 per victory  |

### Interactions

| System                                                       | Interaction                          |
| ------------------------------------------------------------ | ------------------------------------ |
| [Magic System](../../../systems/magic/magic-system/index.md) | Earth element specialization         |
| [Combat](../../../systems/combat/combat-resolution/index.md) | Terrain control and barrier defense  |
| [Party](../../../systems/combat/party-mechanics/index.md)    | Defensive specialist, terrain mapper |
| [Equipment](../../../systems/character/index.md)             | Earth-attuned magical items          |

---

## Related Content

- **Prerequisite Class:** [Mage](../)
- **Skills:** [Earth Mastery](../../skills/tiered/earth-mastery/index.md), [Stone Skin](../../skills/tiered/stone-skin/index.md)
- **Spells:** [Earthquake](../../spells/destruction/earthquake/index.md), [Stone Wall](../../spells/abjuration/stone-wall/index.md)
- **System:** [Magic System](../../../systems/magic/magic-system/index.md)
