---
title: '[Skill Name]'
type: 'skill'
skill_type: '[Passive/Cooldown/Toggle/Tiered/Mechanic-Unlock/Passive-Generator]'
tier: '[Lesser/Greater/Enhanced]' # For tiered skills only
summary: 'Brief one-line description of skill effect'
gdd_ref: 'systems/skill-recipe-system-gdd.md#[skill-type-section]'
tags: [Skill/Category/Subcategory] # Hierarchical tags
---

# [Skill Name]

**Pool**: [Common/Class-Specific/Racial]
**Type**: [Passive/Cooldown/Toggle/Tiered/Mechanic-Unlock/Passive-Generator]
**Universal Access**: [Yes - Available to all classes] or [No - Class-specific]

## Description

[Detailed description of what the skill does, how it affects gameplay, and when it activates]

---

## Tiers

_This section only applies to Tiered skills. Remove for non-tiered skills._

| Tier     | Base Effect          | Scaling         | Cost/Cooldown |
| -------- | -------------------- | --------------- | ------------- |
| Lesser   | [Effect description] | [+X% per level] | [Cost/CD]     |
| Greater  | [Enhanced effect]    | [+X% per level] | [Cost/CD]     |
| Enhanced | [Maximum effect]     | [+X% per level] | [Cost/CD]     |

**Scaling**: All tiers scale with [total class levels / specific class level / skill level]

**Tier Upgrades**: [How to unlock Greater and Enhanced tiers]

---

## Mechanics

_For non-tiered skills, describe mechanics here instead of in Tiers section_

### Effect Details

**Primary Effect**: [Detailed description]

- **Magnitude**: [Specific numbers, percentages, or ranges]
- **Duration**: [How long effect lasts, if applicable]
- **Trigger Condition**: [What causes this skill to activate]

### Interaction Rules

- **Stacks With**: [Other skills/effects this combines with]
- **Does Not Stack With**: [Conflicting skills/effects]
- **Special Interactions**: [Unique behaviors or edge cases]

---

## Acquisition

### Trigger Actions

Actions that increase influence toward this skill:

- [Action name]: [Influence rate]
- [Action name]: [Influence rate]
- [Action name]: [Influence rate]

**Influence Rate**: [X.XX] per [action completion/specific outcome]

**Threshold**: [Total influence needed to unlock] (typically 100-500 for common skills, 1000+ for rare)

### Alternative Acquisition

_If applicable:_

- **Class Starting Skill**: Automatically granted when accepting [Class Name]
- **Racial Passive**: All [Race] characters start with this skill
- **Quest Reward**: Complete [Quest Name] to learn this skill
- **Training**: Learn from [NPC Name] at [Location] for [Cost]

---

## Synergy Classes

Classes that particularly benefit from this skill:

| Class        | Synergy Reason                           |
| ------------ | ---------------------------------------- |
| [Class Name] | [Why this skill is especially effective] |
| [Class Name] | [Explanation of benefit]                 |

---

## Scaling

_Describe how skill effectiveness increases:_

**Scales With**:

- Total combined class levels (generalist scaling)
- Specific class level (specialist scaling)
- Skill level (independent progression)

**Formula**: [If applicable, provide exact scaling formula]

Example: At total class levels 50: [Effect magnitude]

---

## Usage Examples

### Example 1: [Scenario Name]

**Setup**: [Context - character class, level, situation]

**Skill Effect**: [What the skill does in this scenario]

**Outcome**: [Result demonstrating skill value]

### Example 2: [Scenario Name]

**Setup**: [Different context]

**Skill Effect**: [How skill behaves differently]

**Outcome**: [Different result]

---

## Related Content

- **Works Well With**: [Complementary skills, classes that synergize]
- **Modified By**: [Other effects that alter this skill's behavior]
- **See Also**: [Related skills, similar mechanics]
- **GDD Reference**: [Link to authoritative GDD section](../../../systems/skill-recipe-system-gdd.md#[section-id])

---

## Notes

_Optional section for:_

- Design rationale
- Balance considerations
- Historical changes
- Edge cases

---

_Template for skill documentation - Part of 002-doc-migration-rationalization_
