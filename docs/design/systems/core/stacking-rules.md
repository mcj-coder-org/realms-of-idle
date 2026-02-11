---
title: Bonus Stacking Rules
gdd_ref: systems/core-progression-system-gdd.md#general
---

# Bonus Stacking Rules

## 1. Overview

This document defines how bonuses from multiple sources combine. Consistent stacking rules ensure predictable outcomes and prevent exploits while rewarding diverse bonus sources.

| Principle          | Description                              |
| ------------------ | ---------------------------------------- |
| Multiplicative %   | Percentage bonuses multiply together     |
| Additive absolutes | Absolute bonuses add to base first       |
| Predictable        | Same formula everywhere                  |
| Soft caps          | Extreme stacking has diminishing returns |

---

## 2. The Stacking Formula

### Core Formula

```
Final Value = (Base + Absolute Bonuses) × Percentage₁ × Percentage₂ × ... × Percentageₙ
```

### Bonus Types

| Type       | Notation  | Behavior               | Examples                  |
| ---------- | --------- | ---------------------- | ------------------------- |
| Percentage | +X%       | Multiplies with others | +30% tool, +40% workspace |
| Absolute   | +X (flat) | Adds to base first     | +5 damage, +10 health     |

---

## 3. Example Calculations

### Example 1: Crafting Success

```
Base crafting success: 50%
Absolute bonus from trait: +5%
Tool bonus: +30% (1.30)
Workspace bonus: +40% (1.40)
Skill bonus: +15% (1.15)

Final = (50 + 5) × 1.30 × 1.40 × 1.15
     = 55 × 1.30 × 1.40 × 1.15
     = 55 × 2.093
     = 115.1% → capped at 95% (crafting always has 5% fail chance)
```

### Example 2: Damage Calculation

```
Base weapon damage: 20
Absolute bonus from enchant: +5
Strength modifier: +15% (1.15)
Skill bonus: +10% (1.10)
Quality bonus: +30% (1.30)

Final = (20 + 5) × 1.15 × 1.10 × 1.30
     = 25 × 1.6445
     = 41.1 damage
```

### Example 3: Movement Speed

```
Base speed: 100%
Encumbrance penalty: -20% (0.80)
Boots bonus: +15% (1.15)
Spell bonus: +25% (1.25)

Final = 100 × 0.80 × 1.15 × 1.25
     = 115% of base speed
```

---

## 4. System-Specific Rules

### Combat Stacking

| Bonus Source        | Type       | Stacks With                    |
| ------------------- | ---------- | ------------------------------ |
| Weapon base damage  | Base       | All                            |
| Weapon enchantment  | Absolute   | All                            |
| Strength bonus      | Percentage | All                            |
| Skill damage bonus  | Percentage | All                            |
| Quality bonus       | Percentage | All                            |
| Critical multiplier | Percentage | Applied last (after all above) |

### Crafting Stacking

| Bonus Source      | Type       | Notes                        |
| ----------------- | ---------- | ---------------------------- |
| Base success rate | Base       | Determined by recipe/crafter |
| Tool quality      | Percentage | -10% to +30%                 |
| Workspace tier    | Percentage | -20% to +40%                 |
| Skill bonus       | Percentage | Based on relevant skill XP   |
| Material quality  | Percentage | Affects ceiling, not rate    |
| Trait bonus       | Absolute   | Rare, from personality       |

### Social Stacking

| Bonus Source     | Type       | Notes                     |
| ---------------- | ---------- | ------------------------- |
| Base disposition | Base       | Starting NPC attitude     |
| Charm modifier   | Percentage | From CHA attribute        |
| Reputation bonus | Percentage | From faction standing     |
| Skill bonus      | Percentage | Persuasion, Bribery, etc. |
| Gift/bribe value | Absolute   | Flat disposition change   |

---

## 5. Same-Source Stacking

When multiple bonuses come from the same source type:

### Same Type, Different Sources

| Scenario                   | Behavior              |
| -------------------------- | --------------------- |
| Two different skills       | Both apply (multiply) |
| Two different enchantments | Both apply (multiply) |
| Two quality bonuses        | Both apply (multiply) |

### Same Type, Same Source

| Scenario                  | Behavior                |
| ------------------------- | ----------------------- |
| Two fire enchantments     | Highest only (no stack) |
| Same skill from two items | Highest only +10% bonus |
| Duplicate attribute buffs | Highest only (no stack) |

### Skill + Item Enchant (Same Type)

```
Rule: Highest value applies, +10% bonus for having both

Example:
  Skill provides: +20% fire damage
  Weapon enchant: +25% fire damage

  Result: 25% (higher) × 1.10 (bonus) = 27.5% fire damage
```

---

## 6. Soft Caps and Diminishing Returns

To prevent runaway power, extreme stacking faces soft caps:

### General Soft Cap Formula

```
If total multiplier > 2.0:
  Effective = 2.0 + (Actual - 2.0) × 0.5

If total multiplier > 3.0:
  Effective = 2.5 + (Actual - 3.0) × 0.25
```

### Specific System Caps

| System           | Hard Cap | Notes                           |
| ---------------- | -------- | ------------------------------- |
| Crafting success | 95%      | 5% minimum fail chance          |
| Dodge chance     | 75%      | Can't become unhittable         |
| Damage resist    | 80%      | Some damage always gets through |
| Speed bonus      | 200%     | Can't move infinitely fast      |
| XP multiplier    | 300%     | Prevents trivial leveling       |

### No Caps

| System         | Notes                                  |
| -------------- | -------------------------------------- |
| Raw damage     | No cap (balance via defense soft caps) |
| Health pool    | No cap (but rare to exceed 2x)         |
| Carry capacity | No cap (limited by available bonuses)  |

---

## 7. Penalty Stacking

Penalties follow the same multiplicative rules:

### Penalty as Percentage

```
Movement with armor (heavy): 0.85 base
Encumbrance penalty: 0.90
Weather penalty: 0.95

Final = 100% × 0.85 × 0.90 × 0.95 = 72.7% speed
```

### Penalty Mitigation

Bonuses and penalties are multiplied together:

```
Heavy armor: 0.85
Speed enchant: 1.20
Athletics skill: 1.10

Final = 100% × 0.85 × 1.20 × 1.10 = 112.2% speed
```

---

## 8. Order of Operations

When calculating final values:

### Standard Order

1. Start with base value
2. Add all absolute bonuses
3. Apply all percentage multipliers (order doesn't matter for multiplication)
4. Apply soft caps if exceeded
5. Round to appropriate precision

### Special Cases

| Case              | Order                             |
| ----------------- | --------------------------------- |
| Critical damage   | Apply after all other multipliers |
| Resistances       | Apply after damage calculation    |
| Shield absorption | Before resistances                |
| Armor reduction   | After shields, before resistances |

---

## 9. UI Display

### Showing Stacked Bonuses

```
Damage Breakdown:
  Base damage:       20
  Enchantment:       +5  (absolute)
  ─────────────────────
  Subtotal:          25

  Strength (+15%):   ×1.15
  Skill (+10%):      ×1.10
  Quality (+30%):    ×1.30
  ─────────────────────
  Total multiplier:  ×1.64

  Final damage:      41
```

### Tooltip Format

```
+30% crafting quality
  Tool (Masterwork hammer): +20%
  Workspace (Master forge): +30%
  Skill (Blacksmithing):    +15%
  Total:                    ×1.87
```

---

## 10. Design Implications

### Why Multiplicative?

- Rewards diverse bonus sources (not just stacking one type)
- Creates meaningful choices (one +50% vs two +25%)
- Allows for soft caps without hard walls
- More intuitive than additive (diminishing returns feel natural)

### Balance Considerations

| Implication              | Design Response                        |
| ------------------------ | -------------------------------------- |
| High stacking = powerful | Limit max bonuses per source type      |
| Low base + high % = weak | Ensure bases are meaningful            |
| Absolute bonuses early   | More impact early game, less late game |
| Percentage bonuses late  | Scales with progression                |

---

## Related Documentation

- [Quality Tiers](quality-tiers.md) - Quality bonus percentages
- [Attributes](../character/attributes.md) - Attribute-based bonuses
- [Combat Resolution](../combat/combat-resolution.md) - Combat damage stacking
- [Crafting Progression](../crafting/crafting-progression.md) - Crafting bonus sources
