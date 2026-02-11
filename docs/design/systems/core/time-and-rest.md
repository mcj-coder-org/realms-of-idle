---
title: Time and Rest Systems
gdd_ref: systems/core-progression-system-gdd.md#general
---

# Time and Rest Systems

## Overview

A single-player focused slice-of-life RPG written in C# for mobile and web platforms. Features organic class progression, personality-driven NPCs, and meaningful consequences for player actions.

---

## 1. Time & World

### Day/Night Cycle

| Aspect                 | Value                          |
| ---------------------- | ------------------------------ |
| Real-time to game-time | 1 hour real = 24 hours game    |
| Minutes per game hour  | ~2.5 real minutes              |
| Daytime (6am-8pm)      | ~35 real minutes               |
| Nighttime (8pm-6am)    | ~25 real minutes               |
| Seasons                | None (climate is region-based) |

### Night Restrictions

- Shops close
- More thieves/assassins active
- Nocturnal mobs spawn
- Reduced visibility

### Climate Zones

Climate is fixed per region (not seasonal):

- Temperate, Desert, Tundra, Tropical, Swamp, Mountain, etc.
- Climate affects stamina drain, available resources, creature types
- Skills/classes can mitigate climate penalties

---

## 2. Stamina System

### Drain Sources

| Source                             | Drain Rate                 |
| ---------------------------------- | -------------------------- |
| Base (being awake)                 | Constant slow drain        |
| Walking                            | Minimal                    |
| Sprinting                          | Moderate                   |
| Physical labor (planting, lifting) | Moderate                   |
| Combat                             | High                       |
| Crafting                           | Moderate-High              |
| Activated skills                   | Variable (skill-dependent) |
| Toggle skills                      | Ongoing drain while active |

### Drain Reduction

- Class levels (Farmer reduces planting cost)
- Passive skills ("Efficient Labor")
- Equipment (lighter tools)

### Restoration

| Method                 | Amount                    |
| ---------------------- | ------------------------- |
| Full Rest (bed)        | 100% + Fully Rested bonus |
| Nap (anywhere)         | Partial                   |
| Meals                  | Variable by quality       |
| Potions                | Instant burst             |
| Skills ("Second Wind") | Variable                  |

### Zero Stamina

- Character collapses into forced "nap"
- Outdoor naps trigger encounter roll
- Wake vulnerable to thieves/mobs

### Fully Rested Bonus (lasts one game day)

- XP gain boost (all classes)
- Health recovery boost

---

## 3. Rest System

| Rest Type | Location | Duration (Real) | Time Skip        | Stamina | Fully Rested? |
| --------- | -------- | --------------- | ---------------- | ------- | ------------- |
| Full Rest | Bed      | 2-3 min         | Yes (to morning) | 100%    | Yes           |
| Nap       | Anywhere | Shorter         | No               | Partial | No            |

### Rest Encounters

Detection chance based on stamina level when resting:

| Your Stamina | Low-Level Thief | High-Level Thief  |
| ------------ | --------------- | ----------------- |
| 50%+         | Always wake     | Might not wake    |
| 20-50%       | Usually wake    | Rarely wake       |
| 5-20%        | Sometimes wake  | Almost never wake |
| <5%          | Rarely wake     | Never wake        |

Same mechanics for approaching mobs (detection on approach).

### Adrenaline Mechanic

Triggers when woken by encounter during rest:

| Trigger                | Stamina Granted        |
| ---------------------- | ---------------------- |
| Wake during encounter  | Small (10-15% of max)  |
| Adrenaline Shot (item) | Larger (30-50% of max) |
| Class skill            | Variable               |

- Boosted stamina regen for short duration
- Modified by class, skills, items
- Prevents death spiral from exhaustion

---

## 4. Modifier Stacking

All modifiers stack **additively**:

```
10% base + 5% skill + 10% item = 25% total
```

### Modifier Hierarchy

| Source        | Relative Power                     |
| ------------- | ---------------------------------- |
| Base action   | Baseline (100%)                    |
| Equipment     | Low-Medium (+5-15%)                |
| Passive skill | Low (+5% scaling to +15%)          |
| Active skill  | Medium-High (+15% scaling to +30%) |
| Consumable    | Medium, temporary (+20%)           |
| Fully Rested  | Low, temporary (+10%)              |

### Skill Scaling

Passive skills scale linearly with small increments.
Active skills scale differently (balanced to prevent early skills becoming overpowered).

Skills level via use-weighted XP pool distributed at class level-up.

---

## 5. Currency System

See [currency.md](currency.md) for currency denominations and physical money mechanics.

---

## 6. Guild Federation System

All guilds follow the same federated structure:

```
Federation Council (cross-settlement)
├── Sets inter-chapter policies
├── Resolves disputes
├── Coordinates information sharing
├── Manages trade secrets (crafting guilds)
└── No direct authority over chapters

Chapter (per settlement)
├── Independent operations
├── Local leadership
├── Own finances/membership
└── Follows federation guidelines
```

Guild types: Merchant, Crafting (with specialties), Thieves, Assassins, Adventurers, etc.
