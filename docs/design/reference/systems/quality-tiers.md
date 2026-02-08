---
title: Quality Tier System
type: reference
scope: detailed
status: authoritative
version: 1.0.0
created: 2026-02-08
updated: 2026-02-08
subjects: [quality, items, materials, crafting, equipment, tiers]
dependencies: []
---

# Quality Tier System

## 1. Overview

The quality tier system provides a unified numeric framework for comparing quality across different systems (items, materials, enchantments). All equipment, crafted goods, and gathered materials use this shared scale, enabling consistent interactions between crafting, combat, economy, and progression systems.

| Principle       | Description                                                |
| --------------- | ---------------------------------------------------------- |
| Numeric backing | All quality tiers map to numeric values 1–5                |
| Context naming  | Items and materials use different names for same tier      |
| Cross-system    | Quality tiers interact predictably across systems          |
| Stat impact     | Higher quality = better stats, durability, value           |
| Idle relevance  | Quality determines timer efficiency and output multipliers |

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

All quality tiers provide scaled stat bonuses relative to the Standard baseline:

| Quality    | Numeric | Stat Modifier | Durability | Value Multiplier |
| ---------- | ------- | ------------- | ---------- | ---------------- |
| Poor       | 1       | -20%          | -30%       | 0.5×             |
| Standard   | 2       | 0% (baseline) | 0%         | 1×               |
| Superior   | 3       | +15%          | +20%       | 2×               |
| Masterwork | 4       | +30%          | +40%       | 5×               |
| Legendary  | 5       | +50%          | +60%       | 10×+             |

### Stat Meaning by Item Type

| Item Type   | Primary Stat Affected        |
| ----------- | ---------------------------- |
| Weapons     | Damage output                |
| Armour      | Protection value             |
| Tools       | Efficiency / timer reduction |
| Consumables | Effect strength / duration   |
| Accessories | Bonus magnitude              |

---

## 4. Material Quality Impact on Crafting

Material quality constrains the maximum achievable item quality:

| Material Quality | Max Item Quality | Notes                           |
| ---------------- | ---------------- | ------------------------------- |
| Poor             | Standard         | Can't produce quality items     |
| Standard         | Superior         | Most common crafting ceiling    |
| Fine             | Masterwork       | Enables high-quality output     |
| Exceptional      | Legendary        | Required for legendary crafting |
| Perfect          | Legendary+       | Best possible results           |

### Material-to-Item Quality Formula

```text
Max Item Quality = min(Material Quality + 1, Crafter Tier Ceiling)

Where Crafter Tier Ceiling:
  Apprentice  = Superior (3)
  Journeyman  = Masterwork (4)
  Master      = Legendary (5)
```

---

## 5. Enchantment Tier Restrictions

Enchantments use a separate three-tier power scale that interacts with item quality:

| Enchant Tier | Power Level | Description           |
| ------------ | ----------- | --------------------- |
| Magical      | 1           | Basic enchantments    |
| Relic        | 2           | Moderate enchantments |
| Artifact     | 3           | Powerful enchantments |

### Enchantment by Item Quality

| Item Quality | Max Enchant Tier | Reason                                  |
| ------------ | ---------------- | --------------------------------------- |
| Poor         | Cannot enchant   | Too fragile to hold magic               |
| Standard     | Cannot enchant   | Insufficient quality for magic          |
| Superior     | Magical only     | Supports basic enchantments             |
| Masterwork   | Up to Relic      | Strong enough for moderate enchantments |
| Legendary    | Up to Artifact   | Supports the most powerful enchantments |

This restriction creates demand for high-quality crafted items and prevents trivialising the enchantment economy.

---

## 6. Crafting Result Quality

### Crafting Formula

```text
Result Quality = floor(
  (Material Quality + Crafter Bonus + Tool Bonus + Workspace Bonus) / 3
)

Where:
  Material Quality = 1–5 (numeric tier)
  Crafter Bonus    = Class tier (Apprentice=1, Journeyman=2, Master=3)
  Tool Bonus       = Tool quality tier / 2
  Workspace Bonus  = Workspace tier / 2
```

### Repair Quality Retention

```text
Repaired Quality = min(Original Quality, Crafter Maximum)

Crafter Maximum:
  Apprentice  = Superior (3)
  Journeyman  = Masterwork (4)
  Master      = Legendary (5)
```

---

## 7. Quality Distribution

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

## 8. Quality Degradation

Items can lose quality through use and damage:

| Degradation Event      | Quality Impact                    |
| ---------------------- | --------------------------------- |
| Normal use             | No quality loss (durability only) |
| Repair by lower tier   | -1 quality per repair             |
| Severe damage (combat) | Risk of -1 quality                |
| Failed upgrade attempt | -1 quality                        |

---

## 9. Quality-Based Pricing

```text
Price = Base Price × Quality Multiplier × Condition Modifier × Market Modifier

Quality Multiplier:
  Poor       = 0.5×
  Standard   = 1.0×
  Superior   = 2.0×
  Masterwork = 5.0×
  Legendary  = 10.0× (minimum, often higher for unique items)
```

---

## 10. Idle-Specific Adaptations

### Timer Efficiency by Quality

Higher-quality tools and equipment reduce idle timers:

| Tool Quality | Timer Reduction | Example                          |
| ------------ | --------------- | -------------------------------- |
| Poor         | +10% slower     | Mining takes longer              |
| Standard     | Baseline        | Normal timer duration            |
| Superior     | -10% faster     | Slightly faster gathering        |
| Masterwork   | -20% faster     | Noticeable speed improvement     |
| Legendary    | -30% faster     | Significant idle efficiency gain |

### Offline Accumulation

Quality affects the rate at which resources accumulate during offline periods. Higher-quality equipment produces more output per batch calculation tick.

### Quality as Prestige Axis

Quality tiers serve as a secondary progression axis — players progress not just by unlocking new items, but by improving the quality of existing ones through better materials and crafter advancement.
