---
title: Cryomancer
gdd_ref: systems/class-system-gdd.md#advanced-classes
parent: classes/channeler/mage/index.md
tree_tier: 3
---

# Cryomancer

## Tags

> **Required Section:** Tags enable compile-time safe tag access in C# code and drive the skill/class/recipe systems.

### C# Reference

**Strongly Typed Tags:** Use these tags in C# code for compile-time safety.

| Tag Path              | C# Reference                          | Purpose                 |
| --------------------- | ------------------------------------- | ----------------------- |
| `Magic/Elemental/Ice` | `SkillTags.Magic.Elemental.Ice.Value` | Cryomancer class access |
| `Magic/Elemental`     | `SkillTags.Magic.Elemental.Value`     | Elemental magic         |

**How to find class tags:**

1. Check `ClassDefinition.GrantedTags` in class definition files (e.g., `src/CozyFantasyRpg/Content/Classes/MagicClasses.cs`)
2. Verify against `src/CozyFantasyRpg/Shared/SkillTags.cs` for exact C# reference

### Tag Access

This class requires the following tags for access/synergy:

| Tag                   | Depth | Classes With Access             |
| --------------------- | ----- | ------------------------------- |
| `Magic/Elemental/Ice` | 3     | Cryomancer, Mage (depth 2 only) |
| `Magic/Elemental`     | 2     | Cryomancer, Mage, Elementalist  |

**Note:** Tag depth determines which classes can access this content. See [Tag System](../../../../systems/content/tag-system/index.md) for details.

---

## Lore

### Ice Mastery

While fire mages harness destruction through heat and flame, cryomancers discover mastery through cold's patient power. Ice magic requires different understanding than flame - where pyromancers force reality to burn, cryomancers convince it to crystallize and still. This philosophy attracts methodical mages who appreciate precision and control over brute force.

Cryomancers study the nature of cold beyond simple temperature reduction. They learn that freezing slows molecular motion, that ice amplifies and refracts magical energies, that frozen barriers protect through immobility rather than absorption. They discover that cold preserves - ice keeps things exactly as they were, locked in time. This conservation principle guides their magical philosophy, influencing how they approach magic generally.

The cryomancer tradition values patience. Fire magic demands immediate power - cast now or lose the moment. Ice magic rewards careful preparation. A properly positioned frozen barrier stops enemies as effectively as any wall, given time to manifest. A slowing aura transforms entire battlefields without dramatic spellcasting. Cryomancers learn to think several moves ahead, preparing conditions for eventual triumph rather than seeking immediate victory.

### Combat Style

Cryomancers function as controlling specialists in combat. Rather than maximize damage, they maximize battlefield control through freezing and slowing effects. An enemy frozen solid cannot attack. An enemy moving at half speed cannot close distance. An enemy slowed cannot escape prepared positions.

Their spells prioritize utility over raw damage. Blizzard devastates through area control, forcing enemies into disadvantageous positions. Ice Armor protects not through absorption but through enemy incapacitation - attackers slow enough that defenders counter before threats mature. Freezing Aura turns cryomancers into zones of danger where enemies gradually freeze standing still.

This combat approach requires different thinking than other mages. Rather than asking "how much damage can this spell do," cryomancers ask "what does this spell prevent?" They win fights by making enemy action increasingly impossible rather than removing enemy ability to act through direct damage.

## Mechanics

### Prerequisites

| Requirement  | Value                            |
| ------------ | -------------------------------- |
| Class        | [Mage](../) (Tier 2)             |
| XP Threshold | 50,000 XP from ice magic actions |
| Tag Access   | `Magic/Elemental/Ice` (full)     |

### Starting Skills

| Skill         | Type            | Description                            |
| ------------- | --------------- | -------------------------------------- |
| Ice Mastery   | Tiered (Lesser) | Ice spell power and efficiency         |
| Freezing Aura | Tiered (Lesser) | Passive slow and freeze on nearby foes |

### Progression Paths

Cryomancer is a terminal Tier 3 specialization. No further progression paths available.

### Related Content

- **Prerequisite Class:** [Mage](../)
- **Skills:** [Ice Mastery](../../skills/tiered/ice-mastery/index.md), [Freezing Aura](../../skills/tiered/freezing-aura/index.md)
- **Spells:** [Blizzard](../../spells/destruction/blizzard/index.md), [Ice Armor](../../spells/destruction/ice-armor/index.md)
- **System:** [Magic System](../../../systems/magic/magic-system/index.md)
