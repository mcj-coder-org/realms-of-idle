---
title: Rogue
gdd_ref: systems/class-system-gdd.md#foundation-classes
tree_tier: 1
---

# Rogue

## Lore

The Rogue represents those who operate outside society's accepted boundaries - the shadow-dwellers who find opportunity where others see rules. Before becoming a master thief or deadly assassin, every dark path specialist first learns roguish basics: moving unseen, taking what isn't offered, and surviving when the law is against them.

Rogues emerge from desperation or opportunity. The orphan who steals to survive on unforgiving streets. The servant who learns which secrets have value. The rebel who discovers that working outside the system offers freedoms denied to the obedient. Society's margins produce those who question its rules.

The Thieves' Guild - if such a thing exists - teaches the fundamentals of shadowy work. How to move without being noticed. How to acquire things that aren't yours. How to disappear when attention turns your way. These skills keep rogues alive in their dangerous trade.

## Mechanics

### Requirements

| Requirement    | Value                           |
| -------------- | ------------------------------- |
| Unlock Trigger | 100 stealth or criminal actions |
| Primary Tags   | `Criminal`, `Stealth`           |
| Tier           | 1 (Foundation)                  |
| Max Tag Depth  | 1 level                         |

### Unlock Requirements

**Tracked Actions:**

- Steal items from NPCs or locations
- Move undetected while sneaking
- Pick locks or bypass security
- Deceive others through lies or misdirection

**Threshold:** 100 actions (guaranteed unlock) or early unlock via probability

### Starting Skills

Skills automatically awarded when accepting this class:

| Skill         | Type            | Link                                                             | Description                                   |
| ------------- | --------------- | ---------------------------------------------------------------- | --------------------------------------------- |
| Shadow Sense  | Mechanic Unlock | [Shadow Sense](././skills/mechanic-unlock/shadow-sense/index.md) | Awareness of hiding spots and patrol patterns |
| Light Fingers | Passive         | [Light Fingers](././skills/passive/light-fingers/index.md)       | +5% success rate for pickpocketing            |

### XP Progression

Uses Tier 1 formula: `XP = 100 × 1.5^(level - 1)`

| Level | XP to Next | Cumulative |
| ----- | ---------- | ---------- |
| 1→2   | 100        | 100        |
| 5→6   | 506        | 1,131      |
| 10→11 | 3,844      | 6,513      |

### Tracked Actions for XP

| Action                           | XP Value |
| -------------------------------- | -------- |
| Successfully steal an item       | 10-25 XP |
| Remain undetected while sneaking | 5-15 XP  |
| Pick a lock                      | 10-20 XP |
| Successfully deceive someone     | 10-20 XP |
| Escape from pursuit              | 20-40 XP |

### Progression Paths

Rogue can unlock eligibility for the following Tier 2 classes upon reaching 5,000 XP:

| Tier 2 Class | Focus                           | Link                                   |
| ------------ | ------------------------------- | -------------------------------------- |
| Thief        | Property theft and infiltration | [Thief](./trade/thief/index.md)        |
| Assassin     | Stealth killing                 | [Assassin](./combat/assassin/index.md) |
| Fence        | Stolen goods trading            | [Fence](./trade/fence/index.md)        |
| Smuggler     | Illegal transportation          | [Smuggler](./trade/smuggler/index.md)  |

### Tag Access

As a Tier 1 class, Rogue has access to depth 1 tags only:

| Accessible | Not Accessible           |
| ---------- | ------------------------ |
| `Criminal` | `Criminal/Theft`         |
| `Stealth`  | `Criminal/Assassination` |
|            | `Stealth/Infiltration`   |

### Dark Path Warning

Rogue is the foundation for Dark Path classes. Actions taken while progressing this class may:

- Damage reputation with law-abiding factions
- Create enemies among those wronged
- Lock out certain legitimate class options

However, Dark Path classes offer unique abilities and access to underground networks unavailable to legitimate classes.

## Progression

### Specializations

- [Thief](./thief/) - Stealth and theft specialist
- [Assassin](./assassin/) - Lethal stealth specialist
- [Fence](./fence/) - Black market trading
- [Smuggler](./smuggler/) - Contraband transport

## Related Content

- **Tier System:** [Class Tiers](./././systems/character/class-tiers/index.md)
- **Dark Path:** [Dark Path Classes](./././systems/character/class-progression/index.md#10-dark-path-classes)
- **See Also:** [Trade Classes Index](./trade/index.md), [Combat Classes Index](./combat/index.md)
