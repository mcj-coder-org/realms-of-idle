---
title: Trader
gdd_ref: systems/class-system-gdd.md#foundation-classes
tree_tier: 1
---

# Trader

## Lore

The Trader represents the foundation of commerce - the basic understanding that goods have value and that exchanging them benefits all parties. Before becoming a wealthy merchant or renowned appraiser, every commercial specialist first learns to trade: understanding value, negotiating fairly, and building relationships with buyers and sellers.

Traders emerge wherever people exchange goods. The farmer who sells surplus crops at market. The traveler who buys low in one town and sells high in another. The crafted goods peddler who walks between villages. Commerce flows through them, connecting communities through exchange.

The Merchants' Guild teaches trading fundamentals to all who seek commercial success. Price assessment, negotiation basics, record keeping, and understanding supply and demand. These skills form the foundation upon which mercantile empires are built.

## Mechanics

### Requirements

| Requirement    | Value                     |
| -------------- | ------------------------- |
| Unlock Trigger | 100 trade-related actions |
| Primary Tags   | `Trade`                   |
| Tier           | 1 (Foundation)            |
| Max Tag Depth  | 1 level                   |

### Unlock Requirements

**Tracked Actions:**

- Buy goods from vendors or other characters
- Sell goods to vendors or other characters
- Negotiate prices or terms
- Travel between markets or trade routes

**Threshold:** 100 actions (guaranteed unlock) or early unlock via probability

### Starting Skills

Skills automatically awarded when accepting this class:

| Skill           | Type            | Link                                                                 | Description                        |
| --------------- | --------------- | -------------------------------------------------------------------- | ---------------------------------- |
| Market Access   | Mechanic Unlock | [Market Access](../../skills/mechanic-unlock/market-access/index.md) | Access to basic market information |
| Price Awareness | Passive         | [Price Awareness](../../skills/common/appraiser/index.md)            | +5% accuracy in price estimation   |

### XP Progression

Uses Tier 1 formula: `XP = 100 × 1.5^(level - 1)`

| Level | XP to Next | Cumulative |
| ----- | ---------- | ---------- |
| 1→2   | 100        | 100        |
| 5→6   | 506        | 1,131      |
| 10→11 | 3,844      | 6,513      |

### Tracked Actions for XP

| Action                      | XP Value |
| --------------------------- | -------- |
| Complete a trade            | 5-15 XP  |
| Negotiate a better price    | 10-20 XP |
| Discover market opportunity | 15-30 XP |
| Travel to new market        | 10-20 XP |
| Large volume transaction    | 20-50 XP |

### Progression Paths

Trader can transition to the following Tier 2 classes upon reaching 5,000 XP:

| Tier 2 Class | Focus                          | Link                                     |
| ------------ | ------------------------------ | ---------------------------------------- |
| Merchant     | Advanced commerce and business | [Merchant](../trade/merchant/index.md)   |
| Caravaner    | Long-distance trade routes     | [Caravaner](../trade/caravaner/index.md) |
| Appraiser    | Item valuation and assessment  | [Appraiser](../trade/appraiser/index.md) |

### Tag Access

As a Tier 1 class, Trader has access to depth 1 tags only:

| Accessible | Not Accessible      |
| ---------- | ------------------- |
| `Trade`    | `Trade/Negotiation` |
|            | `Trade/Caravan`     |
|            | `Trade/Speculation` |

## Progression

### Specializations

- [Merchant](./merchant/) - Shop operation and sales
- [Caravaner](./caravaner/) - Trade route management
- [Appraiser](./appraiser/) - Item valuation and authentication

## Related Content

- **Tier System:** [Class Tiers](../../../systems/character/class-tiers/index.md)
- **Economy System:** [Economy](../../../systems/economy/index.md)
- **See Also:** [Trade Classes Index](../trade/index.md)
