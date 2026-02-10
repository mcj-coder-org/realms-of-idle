---
title: Battle Hardened
gdd_ref: systems/skill-recipe-system-gdd.md#skills
---

# Battle Hardened

## Overview

Experience generates combat readiness that provides damage resistance bonuses at the start of each day, with benefits extending to multiple combats at higher tiers.

## Synergy Classes

**Strong Synergy (2x XP)**: Warrior, Adventurer, Knight, Guard, Mercenary

Classes with strong synergies get faster learning, better effectiveness, and lower costs.

## Tiers

**No scaling—fixed generation**

| Tier     | Daily Effect                                               |
| -------- | ---------------------------------------------------------- |
| Lesser   | Start each day with +5% damage resistance for first combat |
| Greater  | +10% resistance first combat, +5% second combat            |
| Enhanced | +15% first, +10% second, +5% all subsequent                |

## XP Progression

**XP Sources**:

- Survive multiple combats daily: 5-15 XP per day with 3+ combats
- Endure extended campaigns: 10-25 XP per week in continuous combat
- Take damage across multiple fights: 2-5 XP per combat when damaged
- Guided training: 10-100 XP per session
- Unguided training: 5-50 XP per session

**Level-Up Availability Thresholds**:

- Strong synergy classes: ~500 XP (~50 actions)
- Moderate synergy classes: ~1000 XP (~75 actions)
- No synergy classes: ~1500 XP (~100-150 actions)

## Synergy Bonuses

**Strong Synergy Example** (Warrior Level 15):

- Faster learning: 2x XP from sustained combat
- Better effectiveness: +25% resistance bonus
- Reduced cost: N/A (passive generation)

**Scaling with Class Level**:

| Class Level | XP Multiplier | Effectiveness | Cost Reduction |
| ----------- | ------------- | ------------- | -------------- |
| Level 5     | 1.5x (+50%)   | +15%          | -15%           |
| Level 15    | 2.0x (+100%)  | +25%          | -25%           |
| Level 30+   | 2.5x (+150%)  | +35%          | -35%           |

## Mechanics

**Trigger Actions:** Survive multiple combats daily, endure extended campaigns

**Daily Reset:** Bonuses refresh at the start of each new day (after full rest).

**Combat Sequence:** Bonuses apply in sequence:

- Lesser: First combat only
- Greater: First combat (higher bonus), second combat (lower bonus)
- Enhanced: First combat (highest), second combat (medium), all subsequent (baseline)

**Stacking:** Does not stack with other sources of damage resistance; takes highest value.

**Passive Generation:** No activation required—bonuses automatically apply to designated combats.

**Campaign Bonus:** Extended periods of daily combat (weeks) provide accelerated XP gain for this skill.

## Related Content

- **Strong Synergy:** [Warrior](../../classes/fighter/warrior/), [Adventurer](../../classes/fighter/adventurer/), [Knight](../../classes/fighter/knight/), [Guard](../../classes/fighter/guard/), [Mercenary](../../classes/fighter/mercenary/)
- **See Also:** [Combat System](../../systems/combat/index.md), [Armor Expert](../tiered/armor-expert/index.md)
