---
title: Chop
gdd_ref: systems/action-system-gdd.md#gathering-actions
domain: gathering
ui_trigger: button
base_duration: 10-30 seconds
---

# Chop

**Domain**: Gathering
**UI Trigger**: button
**Base Duration**: 10-30 seconds

## Description

Harvest wood from trees

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

`gathering` - Always fires

### Context-Specific Tags

Context determines specific tags based on tools, targets, and methods used.

**Tag Hierarchy**: More specific tags fire alongside parent tags

---

## Modified By Skills

Skills that affect this action's effectiveness:

Skills in the gathering domain may modify speed, quality, or success rate.

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

Actions in the gathering domain often work together or follow sequences.

---

_Generated action page - Part of 002-doc-migration-rationalization Phase D2_
