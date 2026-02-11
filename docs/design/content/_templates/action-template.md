---
title: '[Action Name]'
gdd_ref: 'systems/action-system-gdd.md#[domain]-actions'
domain: '[combat/crafting/gathering/service/trade/knowledge/social]'
ui_trigger: '[button/hotkey/context-menu]'
base_duration: '[X] seconds'
---

# [Action Name]

**Domain**: [Domain]
**UI Trigger**: [How players initiate this action]
**Base Duration**: [How long it takes to complete]

## Description

[Detailed description of what this action does and when players use it]

---

## Context Requirements

What context is needed to perform this action:

| Context Type | Required | Examples                |
| ------------ | -------- | ----------------------- |
| Target       | [Yes/No] | Enemy, NPC, Object      |
| Tool/Weapon  | [Yes/No] | Sword, Pickaxe, etc.    |
| Location     | [Yes/No] | Combat zone, Mine, etc. |
| Item         | [Yes/No] | Reagent, Material, etc. |

---

## Tag Resolution

How context determines which tags fire for XP distribution:

### Base Tag

`[domain].[category]` - Always fires

### Context-Specific Tags

| Context              | Tag Fired                 | Example Classes That Gain XP |
| -------------------- | ------------------------- | ---------------------------- |
| [Context 1]          | `[domain].[category].[X]` | [Class A], [Class B]         |
| [Context 2]          | `[domain].[category].[Y]` | [Class C], [Class D]         |
| [Context 3 (Weapon)] | `[domain].[weapon-type]`  | [Class E]                    |

**Tag Hierarchy**: More specific tags fire alongside parent tags (e.g., `combat.melee.sword` fires WITH `combat.melee` and `combat`)

---

## Modified By Skills

Skills that affect this action's effectiveness:

| Skill               | Effect                               | Source                                            |
| ------------------- | ------------------------------------ | ------------------------------------------------- |
| [Skill Name]        | [+X% speed/damage/success rate/etc.] | [Skill Type](../../skills/[type]/[name]/index.md) |
| [Skill Name 2]      | [Effect description]                 | [Skill Type](../../skills/[type]/[name]/index.md) |
| [Passive Generator] | [Ongoing benefit]                    | [Skill Type](../../skills/[type]/[name]/index.md) |

**Note**: Skills modify effectiveness (speed, quality, success rate), NOT XP gained. XP is determined by tags only.

---

## Prerequisites

Requirements to perform this action:

- **Minimum Requirements**: [None / Level X in Class Y / Skill Z unlocked]
- **Resource Requirements**: [None / Consumes X stamina / Requires Y item]
- **Location Requirements**: [None / Must be in X location type]
- **State Requirements**: [Combat state / Peace / Mounted / etc.]

---

## Failure Conditions

When this action can fail:

| Condition              | Probability | Consequence                |
| ---------------------- | ----------- | -------------------------- |
| [Condition 1]          | [X%]        | [Action fails, no XP]      |
| [Condition 2]          | [Y%]        | [Partial completion, 50%]  |
| [Low Skill/Tool Level] | [Higher]    | [Increased failure chance] |

**Failure Mitigation**: Skills reduce failure probability

---

## Related Actions

| Relationship | Action                                    | Description               |
| ------------ | ----------------------------------------- | ------------------------- |
| Precedes     | [Next Action](../[domain]/[action].md)    | [Often followed by]       |
| Requires     | [Setup Action](../[domain]/[action].md)   | [Must be done first]      |
| Alternative  | [Similar Action](../[domain]/[action].md) | [Different approach same] |

---

## Examples

### Example 1: [Common Scenario]

**Context**:

- Target: [Specific target]
- Tool: [Specific tool]
- Location: [Specific location]

**Tag Resolution**: `[domain].[category].[specific]`

**XP Distribution**:

- [Class A] (tracks `[domain].[category]`): 50 XP
- [Class B] (tracks `[domain]`): 25 XP

**Result**: [What happens]

### Example 2: [Different Context]

**Context**:

- Target: [Different target]
- Tool: [Different tool]

**Tag Resolution**: `[domain].[category].[different-specific]`

**XP Distribution**:

- [Class C] (tracks `[domain].[category].[different]`): 50 XP
- [Class A] (tracks `[domain].[category]`): 25 XP
- [Class B] (tracks `[domain]`): 12.5 XP

**Result**: [What happens]

---

## Implementation Notes

### UI Integration

- **Button Location**: [Where in UI]
- **Hotkey**: [Default keybinding]
- **Context Menu**: [Right-click on target shows action]
- **Visual Feedback**: [Animation, sound, etc.]

### Performance Considerations

- **Cooldown**: [None / X seconds between uses]
- **Animation Duration**: [Match base_duration]
- **Network Sync**: [If multiplayer considerations]

---

_Template for action documentation - Part of 002-doc-migration-rationalization_
