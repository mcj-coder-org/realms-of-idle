---
title: Channeler
type: class-category
tier: 1
category: foundation-alternative
tags:
  - Magic
  - Channeling
summary: Foundation magic class providing universal magic bonuses
---

# Channeler

The Channeler represents one path into magic - focusing on the ability to access and channel magical energy rather than systematic study. While Mages approach magic academically, Channelers rely on natural magical sensitivity and disciplined practice to develop spellcasting ability.

## Tags

> **Required Section:** Tags enable compile-time safe tag access in C# code and drive the skill/class/recipe systems.

### C# Reference

**Strongly Typed Tags:** Use these tags in C# code for compile-time safety.

| Tag Path        | C# Reference                    | Purpose              |
| --------------- | ------------------------------- | -------------------- |
| `Magic`         | `SkillTags.Magic.Value`         | Magic access         |
| `Channeling`    | `SkillTags.Channeling.Value`    | Channeling ability   |
| `Magic/General` | `SkillTags.Magic.General.Value` | General magic skills |

**How to find class tags:**

1. Check `ClassDefinition.GrantedTags` in class definition files (e.g., `src/CozyFantasyRpg/Content/Classes/MagicClasses.cs`)
2. Verify against `src/CozyFantasyRpg/Shared/SkillTags.cs` for exact C# reference

### Tag Access

This foundation class provides the following tags for access/synergy:

| Tag          | Depth | Classes With Access                    |
| ------------ | ----- | -------------------------------------- |
| `Magic`      | 1     | Channeler, all magic classes           |
| `Channeling` | 1     | Channeler, Mage, specialist classes    |
| `Magic/*`    | 2+    | Channeler (limited access to sub-tags) |

**Note:** Tag depth determines which classes can access this content. See [Tag System](../../../systems/content/tag-system.md) for details.

---

## Specializations

Channeler unlocks access to all magic-based classes and provides bonuses to magical disciplines:

| Specialization              | Type                | Focus                   |
| --------------------------- | ------------------- | ----------------------- |
| [Mage](mage/)               | Tier 2 - Generalist | All magical disciplines |
| [Enchanter](enchanter/)     | Consolidation       | Magic + Crafting        |
| [Healer](healer/)           | Consolidation       | Magic + Gathering       |
| [Illusionist](illusionist/) | Tier 2 - Specialist | Illusion magic          |
| [Necromancer](necromancer/) | Tier 2 - Specialist | Undead magic            |
| [Summoner](summoner/)       | Tier 2 - Specialist | Creature summoning      |

## Related Content

- **See Also:** [Mage](mage/), [Spell Schools](../../../systems/magic/), [Magic System](../../../systems/character/magic-system.md), [Consolidation Classes](../../../systems/character/class-consolidation.md)
