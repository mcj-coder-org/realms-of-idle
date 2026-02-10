---
title: '[Class Name]'
type: 'class-detail'
summary: 'Brief one-line description of class role and focus'
parent: '[parent-class-name]/index.md' # Optional - only for Tier 2+ classes
tree_tier: 1 # 1=Foundation, 2=Specialization, 3=Advanced
gdd_ref: 'systems/class-system-gdd.md#[section-id]'
tags: [keyword1, keyword2, keyword3] # Optional but recommended
---

# [Class Name]

## Lore

### Origin

[Background story explaining how this class came to be, cultural context, and role in the world]

### In the World

[How characters with this class appear and function in the game world, daily activities, and social perception]

---

## Mechanics

### Prerequisites

| Requirement     | Value                                           |
| --------------- | ----------------------------------------------- |
| Tree Tier       | [1/2/3]                                         |
| Parent Class    | [Parent Class Name] or None (for Tier 1)        |
| XP Requirement  | [Amount] XP in [tag path] bucket                |
| Training Option | [NPC Trainer Name] at [Location] (if available) |

### Tracked Actions

This class gains XP from actions tagged with `[primary.tag.path]` (and its children):

| Action        | XP Gained | Link                                                    |
| ------------- | --------- | ------------------------------------------------------- |
| [Action Name] | [X] XP    | [Action Spec](../../actions/[domain]/[action]/index.md) |
| [Action Name] | [X] XP    | [Action Spec](../../actions/[domain]/[action]/index.md) |

**Note**: All XP values are base amounts. Skills modify action effectiveness (speed, quality), NOT XP gained.

**Tag Hierarchy**: Classes track tag paths hierarchically. Actions with more specific tags (e.g., `combat.melee.sword`) grant XP to all matching parent paths (`combat.melee`, `combat`).

### Starting Skills

Skills automatically awarded when accepting this class:

- **[Skill Name]** ([Tier]): [Brief description]
  - Link: [Skill Spec](../../skills/[type]/[skill]/index.md)

### Specialization Paths

Available Tier 3 classes unlocked by advancing this class:

| Specialization | Requirement             | Focus                  |
| -------------- | ----------------------- | ---------------------- |
| [[Spec Name]]  | [Class] Level 10 + Tags | [Specialization focus] |
| [[Spec Name]]  | [Class] Level 10 + Tags | [Specialization focus] |

**Note**: Specializations are separate classes added to your collection, NOT replacements. You retain the parent class.

---

## Progression

### Level Milestones

| Level | Unlock                              |
| ----- | ----------------------------------- |
| 1     | Starting skills, base action access |
| 5     | [Skill/Feature unlock]              |
| 10    | Specialization paths available      |
| 20    | [Advanced feature]                  |

### Tier Advancement

Advancement through tiers (Apprentice → Journeyman → Master) is automatic when class XP reaches thresholds. Tier advancement is independent of specialization choices.

---

## Synergies

### Multi-Class Synergies

Effective class combinations:

- **[Class A] + [Class B]**: [Why this combination works well, shared tags, complementary skills]
- **[Class A] + [Class C]**: [Synergy description]

### Skill Synergies

Skills that particularly benefit this class:

- **[Skill Name]**: [Why this skill is especially effective for this class]

---

## Related Content

- **Requires**: [Prerequisites, parent classes]
- **Works With**: [Synergistic classes, complementary skills]
- **Provides**: [What this class enables, specialization paths]
- **GDD Reference**: [Link to authoritative GDD section](../../../systems/class-system-gdd.md#[section-id])

---

## Examples

See co-located examples in `examples/` directory for:

- Character builds featuring this class
- Progression paths from Tier 1 → Tier 3
- Multi-class combinations

---

_Template for class documentation - Part of 002-doc-migration-rationalization_
