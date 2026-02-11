---
title: Socialize
gdd_ref: systems/action-system-gdd.md#social-actions
domain: social
ui_trigger: context-menu
base_duration: 10-60 seconds
---

# Socialize

**Domain**: Social
**UI Trigger**: context-menu
**Base Duration**: 10-60 seconds

## Description

Interact with NPCs to build relationships

---

## Context Requirements

What context is needed to perform this action:

| Context Type | Required | Examples          |
| ------------ | -------- | ----------------- |
| Target       | Varies   | Depends on use    |
| Tool/Weapon  | Often    | Appropriate tools |
| Location     | No       | Any suitable area |
| Item         | Varies   | Context-dependent |

---

## Tag Resolution

How context determines which tags fire for XP distribution:

### Base Tag

`social` - Always fires

### Context-Specific Tags

Context determines specific tags based on tools, targets, and methods used.

**Tag Hierarchy**: More specific tags fire alongside parent tags

---

## Modified By Skills

Skills that affect this action's effectiveness:

Skills in the social domain may modify speed, quality, or success rate.

**Note**: Skills modify effectiveness, NOT XP gained. XP is determined by tags only.

---

## Prerequisites

Requirements to perform this action:

- **Minimum Requirements**: None (available to all characters)
- **Resource Requirements**: May consume stamina or resources
- **Location Requirements**: Appropriate context
- **State Requirements**: Varies by context

---

## Failure Conditions

When this action can fail:

| Condition       | Probability | Consequence           |
| --------------- | ----------- | --------------------- |
| Low Skill Level | Higher      | Increased failure     |
| Poor Tools      | Moderate    | Reduced effectiveness |
| Hostile Context | Varies      | Action interrupted    |

**Failure Mitigation**: Skills reduce failure probability

---

## Related Actions

Actions in the social domain often work together or follow sequences.

---

_Generated action page - Part of 002-doc-migration-rationalization Phase D2_
