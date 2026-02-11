---
title: Formula Glossary
gdd_ref: systems/core-progression-system-gdd.md#general
---

# Formula Glossary

This document consolidates all numerical formulas used across game systems for easy reference and consistency verification.

---

## 1. Bonus Stacking (Universal)

### Core Stacking Formula

```
Final Value = (Base + Absolute Bonuses) × Percentage₁ × Percentage₂ × ... × Percentageₙ
```

**Source:** [Stacking Rules](../systems/core/stacking-rules.md)

### Soft Cap Formula

```
If total multiplier > 2.0:
  Effective = 2.0 + (Actual - 2.0) × 0.5

If total multiplier > 3.0:
  Effective = 2.5 + (Actual - 3.0) × 0.25
```

---

## 2. Character Progression

### Class Tier System

Classes are organized into three tiers with different XP scaling:

| Tier | Name       | XP Threshold           | Max Tag Depth |
| ---- | ---------- | ---------------------- | ------------- |
| 1    | Foundation | 500 XP from actions    | 1             |
| 2    | —          | 5,000 XP from actions  | 2             |
| 3    | —          | 50,000 XP from actions | 3+            |

**Source:** [Class Tiers](../systems/character/class-tiers.md)

### Tier 1 XP Per Level (Foundation)

```
XP = 100 × 1.5^(level-1)
```

| Level | XP Required | Cumulative |
| ----- | ----------- | ---------- |
| 1     | 100         | 100        |
| 5     | 506         | 1,588      |
| 10    | 3,844       | 8,619      |

### Tier 2 XP Per Level

```
XP = 150 × 3^(level/10) × (1 + level × 0.1)
```

| Level | XP Required | Cumulative |
| ----- | ----------- | ---------- |
| 1     | 165         | 165        |
| 10    | 742         | 4,205      |
| 25    | 4,494       | 41,729     |

### Tier 3 XP Per Level

```
XP = 500 × e^(level/25) × (1 + level × 0.15)
```

| Level | XP Required | Cumulative |
| ----- | ----------- | ---------- |
| 1     | 575         | 575        |
| 10    | 1,489       | 8,923      |
| 25    | 5,899       | 58,294     |

### XP Penalty Formula

```
XP Needed = Base XP × (1 + (Total Levels × 0.15))
```

Creates specialist vs generalist trade-off.

**Source:** [Class Progression](../systems/character/class-progression.md)

### Class Unlock Probability

```
Unlock Chance = Accumulated Probability × (1 + Prerequisite Bonus + Skill Bonus)

Where:
  Prerequisite Bonus = 0.5 if has prerequisite class, else 0
  Skill Bonus = Related Skill Total / 500 (max 0.2)
```

**Note:** System also supports deterministic unlock at 100 actions.

### Action-Based Unlock Accumulation

| Action Result | Probability Added |
| ------------- | ----------------- |
| Failed        | +0.5%             |
| Partial       | +1.0%             |
| Success       | +2.0%             |
| Great Success | +3.0%             |
| Exceptional   | +5.0%             |

**Source:** [Class Progression](../systems/character/class-progression.md)

---

## 3. Attribute System

### Attribute Check Formula

```
Success Chance = Base Difficulty + (Attribute × Scaling) + Modifiers
```

### Attribute Scaling

| Attribute Value | Modifier to Base |
| --------------- | ---------------- |
| 5               | -15%             |
| 8               | -5%              |
| 10              | +0%              |
| 12              | +6%              |
| 15              | +15%             |
| 18              | +24%             |
| 20              | +30%             |

### Opposed Check Formula

```
Roll = Base 50 + (Attribute × 3) + Skill Bonus + Modifiers + Random(-10 to +10)
Higher roll wins (ties favor defender)
```

### Attribute Soft Cap (Diminishing Returns)

| Range | Effectiveness  |
| ----- | -------------- |
| 1-20  | 100% per point |
| 21-25 | 75% per point  |
| 26-30 | 50% per point  |
| 31+   | 25% per point  |

**Source:** [Attributes](../systems/character/attributes.md)

---

## 4. Combat System

### Damage Formula

```
Final Damage = (Base Weapon + Absolute Bonuses) × STR Modifier × Skill Bonus × Quality Bonus
```

### Adrenaline Recovery

```
Stamina Recovery = 20% + (END - 10) × 0.5%

Range: 20-30% based on Endurance attribute
```

**Source:** [Adrenaline](../systems/core/adrenaline.md)

### Health Pool Formula

```
Max Health = Base Health × (1 + END Modifier)

Where END Modifier (from attributes table):
  END 10 = Baseline (1.0)
  END 15 = +20% (1.20)
  END 20+ = +40%+ (1.40+)
```

**Source:** [Attributes](../systems/character/attributes.md)

---

## 5. Quality System

### Quality Stat Modifiers

| Quality    | Numeric | Stat Modifier | Durability | Value |
| ---------- | ------- | ------------- | ---------- | ----- |
| Poor       | 1       | -20%          | -30%       | 0.5x  |
| Standard   | 2       | 0%            | 0%         | 1.0x  |
| Superior   | 3       | +15%          | +20%       | 2.0x  |
| Masterwork | 4       | +30%          | +40%       | 5.0x  |
| Legendary  | 5       | +50%          | +60%       | 10.0x |

### Crafting Result Quality Formula

```
Result Quality = floor(
  (Material Quality + Crafter Bonus + Tool Bonus + Workspace Bonus) / 3
)

Where:
  Material Quality = 1-5 (numeric tier)
  Crafter Bonus = Class tier (Apprentice=1, Journeyman=2, Master=3)
  Tool Bonus = Tool quality tier / 2
  Workspace Bonus = Workspace tier / 2
```

**Source:** [Quality Tiers](../systems/core/quality-tiers.md)

---

## 6. Crafting System

### Tool Quality Bonuses

| Quality    | Bonus |
| ---------- | ----- |
| Crude      | -10%  |
| Common     | 0%    |
| Quality    | +5%   |
| Fine       | +10%  |
| Superior   | +15%  |
| Masterwork | +20%  |
| Legendary  | +30%  |

### Workspace Bonuses

| Tier            | Bonus |
| --------------- | ----- |
| Improvised      | -20%  |
| Basic           | 0%    |
| Standard        | +10%  |
| Professional    | +20%  |
| Master Workshop | +30%  |
| Legendary       | +40%  |

### Masterwork Chance by Tier

| Class Tier | Minimum Outcome | Masterwork Chance |
| ---------- | --------------- | ----------------- |
| Apprentice | Partial Success | <5%               |
| Journeyman | Success         | 10-20%            |
| Master     | Success         | >50%              |

**Source:** [Crafting Progression](../systems/crafting/crafting-progression.md)

---

## 7. Enchantment System

### Disenchant Recovery Rate

| Enchanter Tier | Base Recovery | Max Recovery |
| -------------- | ------------- | ------------ |
| Apprentice     | 40%           | 55%          |
| Journeyman     | 55%           | 70%          |
| Master         | 70%           | 85%          |

### Item Value Formula (Enchanted)

```
Value = Base Value × Quality Mult × Enchant Tier Mult × Enchant Type Modifier

Enchant Type Modifiers:
  Utility:    1.5x
  Defensive:  2.0x
  Attribute:  2.5x
  Offensive:  3.0x
  Unique:     5.0x+
```

**Source:** [Enchantment Mechanics](../systems/crafting/enchantment-mechanics.md), [Disenchanting](../systems/enchantment/disenchanting.md)

---

## 8. Economy System

### Price Formula

```
Price = Base Price × Quality Mult × Condition Modifier × Market Modifier
```

### Trade Modifier (Charm)

| CHA | Buy Modifier | Sell Modifier |
| --- | ------------ | ------------- |
| 5   | +15%         | -15%          |
| 10  | 0%           | 0%            |
| 15  | -10%         | +10%          |
| 20+ | -20%+        | +20%+         |

**Source:** [Attributes](../systems/character/attributes.md)

---

## 9. Social System

### Reputation Decay Formula

| Severity  | Decay Time |
| --------- | ---------- |
| Trivial   | 1-3 days   |
| Minor     | 1-4 weeks  |
| Moderate  | 1-6 months |
| Severe    | 1-3 years  |
| Extreme   | 5-10 years |
| Legendary | Never      |

**Source:** [Factions and Reputation](../systems/social/factions-reputation.md)

---

## 10. NPC Simulation

### NPC XP by LOD

| LOD Level | Calculation Method |
| --------- | ------------------ |
| Full      | Exact per action   |
| Moderate  | Hourly sum         |
| Low       | Daily average      |
| Minimal   | Weekly batch       |

### Occupation XP Averages (per day)

| Occupation | Daily XP Range |
| ---------- | -------------- |
| Farmer     | 10-20          |
| Guard      | 15-25          |
| Crafter    | 20-40          |
| Merchant   | 15-30          |
| Adventurer | 30-100         |
| Scholar    | 10-20          |

### Self-Preservation Thresholds

| Resource | Threshold | Behavior            |
| -------- | --------- | ------------------- |
| Health   | <25%      | Seek safety/healing |
| Stamina  | <10%      | Seek rest           |

**Source:** [NPC Simulation](../systems/social/npc-simulation.md)

---

## 11. Skill System

### Skill Tier Bonuses (Stat Boost Skills)

| Tier     | Primary | Secondary | Tertiary |
| -------- | ------- | --------- | -------- |
| Lesser   | +5      | —         | —        |
| Greater  | +5      | +3        | —        |
| Enhanced | +5      | +3        | +2       |

### Skill XP Gain

| Source                    | XP Gain |
| ------------------------- | ------- |
| Skill-related actions     | 1-10    |
| Successful skill use      | 5-25    |
| Great/exceptional results | 15-50   |
| Guided training session   | 10-100  |
| Unguided practice         | 5-50    |

### Tier Probability by Class Level

| Class Level | Lesser | Greater | Enhanced |
| ----------- | ------ | ------- | -------- |
| 1-5         | 80%    | 18%     | 2%       |
| 6-10        | 65%    | 30%     | 5%       |
| 11-15       | 45%    | 45%     | 10%      |
| 16-20       | 25%    | 55%     | 20%      |
| 21-30       | 10%    | 50%     | 40%      |
| 31+         | 5%     | 35%     | 60%      |

**Source:** [Class Progression](../systems/character/class-progression.md)

---

## 12. Time and Rest

### Fully Rested Bonus

```
XP Modifier = 1.15 (15% bonus)
Duration = 4-6 game hours after full sleep
```

### Time Conversion

```
1 real hour = 24 game hours
Day (6am-8pm) = ~35 real minutes
Night (8pm-6am) = ~25 real minutes
```

**Source:** [Time and Rest](../systems/core/time-and-rest.md), [Overview](../systems/core/overview.md)

---

## Quick Reference Tables

### Universal Multipliers

| Bonus Type | Common Values    | Stacking   |
| ---------- | ---------------- | ---------- |
| Tool       | -10% to +30%     | Percentage |
| Workspace  | -20% to +40%     | Percentage |
| Skill      | +5% to +30%      | Percentage |
| Quality    | -20% to +50%     | Percentage |
| Enchant    | +10% to +50%     | Percentage |
| Absolute   | +1 to +10 (flat) | Additive   |

### System Caps

| System           | Hard Cap | Notes                     |
| ---------------- | -------- | ------------------------- |
| Crafting success | 95%      | 5% minimum fail           |
| Dodge chance     | 75%      | Can't be unhittable       |
| Damage resist    | 80%      | Some damage passes        |
| Speed bonus      | 200%     | Movement cap              |
| XP multiplier    | 300%     | Prevents trivial leveling |

---

## Related Documentation

- [Class Tiers](../systems/character/class-tiers.md) - Three-tier class hierarchy and XP formulas
- [Stacking Rules](../systems/core/stacking-rules.md) - Detailed stacking behavior
- [Quality Tiers](../systems/core/quality-tiers.md) - Quality system details
- [Attributes](../systems/character/attributes.md) - Full attribute tables
- [Class Progression](../systems/character/class-progression.md) - XP and skill formulas
- [Skill Tags](../systems/character/skill-tags.md) - Tag depth and class tier access
- [Data Tables](../systems/core/data-tables.md) - Additional numerical reference
