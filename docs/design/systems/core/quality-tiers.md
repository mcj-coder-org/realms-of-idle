---
title: Quality Tier System
gdd_ref: systems/core-progression-system-gdd.md#general
---

# Quality Tier System

## 1. Overview

The quality tier system provides a unified numeric framework for comparing quality across different systems (items, materials, enchantments) while preserving contextually appropriate naming.

| Principle       | Description                                        |
| --------------- | -------------------------------------------------- |
| Numeric backing | All quality tiers map to numeric values 1-5        |
| Context naming  | Items, materials use different names for same tier |
| Cross-system    | Quality tiers interact predictably across systems  |
| Stat impact     | Higher quality = better stats, durability, value   |

---

## 2. Quality Tier Mapping

### Numeric to Name Mapping

| Numeric | Item Quality | Material Quality | Description                     |
| ------- | ------------ | ---------------- | ------------------------------- |
| 1       | Poor         | Poor             | Lowest quality, barely usable   |
| 2       | Standard     | Standard         | Average quality, baseline       |
| 3       | Superior     | Fine             | Above average, good quality     |
| 4       | Masterwork   | Exceptional      | Exceptional quality, rare       |
| 5       | Legendary    | Perfect          | Highest quality, extremely rare |

---

## 3. Quality Stat Modifiers

All quality tiers provide scaled stat bonuses:

| Quality    | Numeric | Stat Modifier | Durability | Value Multiplier |
| ---------- | ------- | ------------- | ---------- | ---------------- |
| Poor       | 1       | -20%          | -30%       | 0.5x             |
| Standard   | 2       | 0% (baseline) | 0%         | 1x               |
| Superior   | 3       | +15%          | +20%       | 2x               |
| Masterwork | 4       | +30%          | +40%       | 5x               |
| Legendary  | 5       | +50%          | +60%       | 10x+             |

### What "Stats" Means by Item Type

| Item Type   | Stat Affected               |
| ----------- | --------------------------- |
| Weapons     | Damage output               |
| Armor       | Protection value            |
| Tools       | Efficiency/bonus to actions |
| Containers  | Capacity                    |
| Consumables | Effect strength/duration    |
| Accessories | Bonus magnitude             |

---

## 4. Material Quality Impact on Crafting

Material quality affects the maximum quality achievable when crafting:

| Material Quality | Max Item Quality | Notes                           |
| ---------------- | ---------------- | ------------------------------- |
| Poor             | Standard         | Can't make quality items        |
| Standard         | Superior         | Most common crafting            |
| Fine             | Masterwork       | Enables high-quality items      |
| Exceptional      | Legendary        | Required for legendary crafting |
| Perfect          | Legendary+       | Best possible results           |

### Material-to-Item Quality Formula

```
Max Item Quality = min(Material Quality + 1, Crafter Tier Ceiling)

Where Crafter Tier Ceiling:
  Apprentice  = Superior (3)
  Journeyman  = Masterwork (4)
  Master      = Legendary (5)
```

---

## 5. Enchantment Tiers (Separate System)

Enchantments use a separate tier system that interacts with item quality:

| Enchant Tier | Power Level | Description           |
| ------------ | ----------- | --------------------- |
| Magical      | 1           | Basic enchantments    |
| Relic        | 2           | Moderate enchantments |
| Artifact     | 3           | Powerful enchantments |

### Enchantment Restrictions by Item Quality

| Item Quality | Max Enchant Tier | Example                              |
| ------------ | ---------------- | ------------------------------------ |
| Poor         | Cannot enchant   | Too fragile to hold magic            |
| Standard     | Cannot enchant   | Insufficient quality for magic       |
| Superior     | Magical only     | Superior Sword of Fire (Magical)     |
| Masterwork   | Up to Relic      | Masterwork Blade of Vampiric (Relic) |
| Legendary    | Up to Artifact   | Legendary Greatsword of Disruption   |

### Why This Restriction?

- Poor/Standard items can't withstand magical infusion
- Superior items can hold basic enchantments
- Higher quality = stronger magical resonance
- Creates demand for quality crafted items
- Prevents trivializing enchantment economy

---

## 6. Quality Interaction Formulas

### Crafting Result Quality

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

### Repair Quality Retention

```
Repaired Quality = min(Original Quality, Crafter Maximum)

Crafter Maximum:
  Apprentice  = Superior (3)
  Journeyman  = Masterwork (4)
  Master      = Legendary (5)
```

---

## 7. Quality Distribution (World Generation)

### Natural Drop Rates

| Quality    | Drop Rate | Notes                       |
| ---------- | --------- | --------------------------- |
| Poor       | 30%       | Common loot, worn items     |
| Standard   | 45%       | Most common quality         |
| Superior   | 18%       | Uncommon, valuable          |
| Masterwork | 6%        | Rare, from skilled crafters |
| Legendary  | 1%        | Extremely rare, named items |

### Merchant Stock Distribution

| Merchant Type   | Poor | Standard | Superior | Masterwork | Legendary |
| --------------- | ---- | -------- | -------- | ---------- | --------- |
| Village vendor  | 20%  | 60%      | 18%      | 2%         | 0%        |
| Town merchant   | 10%  | 50%      | 30%      | 9%         | 1%        |
| City specialist | 0%   | 30%      | 45%      | 20%        | 5%        |
| Master crafter  | 0%   | 10%      | 30%      | 45%        | 15%       |

---

## 8. Identifying Quality

### Visual Indicators

| Quality    | Visual Cue                          |
| ---------- | ----------------------------------- |
| Poor       | Worn, dull, visible damage          |
| Standard   | Clean, functional appearance        |
| Superior   | Well-crafted, some ornamentation    |
| Masterwork | Exceptional detail, fine materials  |
| Legendary  | Unique appearance, may glow/shimmer |

### Appraisal Skill

| Skill Level | Can Identify                       |
| ----------- | ---------------------------------- |
| None        | Poor vs Not-Poor                   |
| Basic       | Poor, Standard, Superior+          |
| Moderate    | All except Legendary vs Masterwork |
| Expert      | All qualities accurately           |
| Master      | Quality + hidden enchantments      |

---

## 9. Quality Degradation

Items can lose quality through use and damage:

| Degradation Event        | Quality Impact                    |
| ------------------------ | --------------------------------- |
| Normal use               | No quality loss (durability only) |
| Repair by lower tier     | -1 quality per repair             |
| Severe damage (combat)   | Risk of -1 quality                |
| Failed upgrade attempt   | -1 quality                        |
| Masterwork can't degrade | Masterwork is minimum for repair  |

---

## 10. Quality-Based Economy

### Price Scaling

```
Price = Base Price × Quality Multiplier × Condition Modifier × Market Modifier

Quality Multiplier:
  Poor       = 0.5x
  Standard   = 1.0x
  Superior   = 2.0x
  Masterwork = 5.0x
  Legendary  = 10.0x (minimum, often higher for unique items)
```

### Selling to Merchants

| Merchant Type  | Buys Up To | Price Offered          |
| -------------- | ---------- | ---------------------- |
| General vendor | Superior   | 40-60% value           |
| Specialist     | Masterwork | 50-70% value           |
| Collector      | Legendary  | 60-80% value           |
| Auction house  | Any        | Player-set (minus fee) |

---

## Related Documentation

- [Crafting Progression](../crafting/crafting-progression.md) - How quality is achieved
- [Enchantment Mechanics](../crafting/enchantment-mechanics.md) - Enchantment tier system
- [Trade and Pricing](../economy/trade-and-pricing.md) - Quality impact on economy
- [Stacking Rules](stacking-rules.md) - How bonuses combine
