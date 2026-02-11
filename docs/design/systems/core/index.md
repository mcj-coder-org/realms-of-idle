---
title: Index
gdd_ref: systems/core-progression-system-gdd.md#overview
---

<!-- ADAPTATION REQUIRED -->
<!-- This file was migrated from source but needs manual review: -->

<!) -->

<!-- - Align with current GDD architecture -->
<!-- - Add missing sections as needed -->
<!-- - Update frontmatter with correct gdd_ref -->

---

title: Core Systems
type: index
summary: Fundamental game mechanics - time, stamina, rest, currency

---

# Core Systems

These are the foundational mechanics that all other systems build upon.

## Documents

| Document                               | Description                                     |
| -------------------------------------- | ----------------------------------------------- |
| [overview.md](overview.md)             | High-level game vision and design pillars       |
| [time-and-rest.md](time-and-rest.md)   | Day/night cycle, stamina, rest mechanics        |
| [currency.md](currency.md)             | Gold/silver/copper economy and physical money   |
| [regional-tones.md](regional-tones.md) | Geography-gated content and difficulty scaling  |
| [data-tables.md](data-tables.md)       | Quick reference tables for all numerical values |

## Key Concepts

### Time Flow

- 1 real hour = 24 game hours
- Day (6am-8pm) = ~35 real minutes
- Night (8pm-6am) = ~25 real minutes

### Stamina Loop

```
Actions drain stamina
 ↓
Zero stamina = collapse into nap
 ↓
Rest restores stamina
 ↓
Fully Rested bonus grants XP boost
```

### Currency Tiers

| Denomination | Value      | Typical Use            |
| ------------ | ---------- | ---------------------- |
| Copper       | 1          | Common goods, food     |
| Silver       | 10 copper  | Quality goods, wages   |
| Gold         | 100 copper | Luxury items, property |
