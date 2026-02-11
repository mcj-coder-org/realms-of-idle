<!-- ADAPTATION REQUIRED -->
<!-- This file was migrated from source but needs manual review: -->
<!-- - Update terminology (dormant classes, XP split, etc.) -->
<!-- - Align with current GDD architecture -->
<!-- - Add missing sections as needed -->
<!-- - Update frontmatter with correct gdd_ref -->

---

title: Economy Systems
type: index
summary: Trade, pricing, black market, and economic events

---

# Economy Systems

These systems govern how goods flow, prices change, and economic opportunities arise.

## Documents

| Document                                     | Description                                      |
| -------------------------------------------- | ------------------------------------------------ |
| [trade-and-pricing.md](trade-and-pricing.md) | Supply/demand, merchant tiers, trade routes      |
| [black-market.md](black-market.md)           | Underground economy, dual reputation, contraband |
| [fairs-and-events.md](fairs-and-events.md)   | Annual fairs, competitions, special markets      |
| [trade-chains.md](trade-chains.md)           | Multi-hop trade for hostile races                |

## Economic Actors

| Actor      | Role                              | Progression              |
| ---------- | --------------------------------- | ------------------------ |
| Trader     | Basic commerce                    | Entry level              |
| Merchant   | Owns shop, larger transactions    | Requires capital         |
| Specialist | Focused expertise (gems, weapons) | Requires reputation      |
| Caravaner  | Long-distance trade               | Requires party/resources |

## Price Factors

| Factor               | Effect                         |
| -------------------- | ------------------------------ |
| Local Supply         | High supply = lower prices     |
| Local Demand         | High demand = higher prices    |
| Distance from Source | Farther = more expensive       |
| Quality              | Higher quality = premium price |
| Legality             | Contraband = risk premium      |

## Dual Reputation

```
PUBLIC REPUTATION          UNDERGROUND REPUTATION
(Factions, guilds)         (Thieves, smugglers)
        │                          │
        └──────────┬───────────────┘
                   │
            Can maintain both
                   │
        Discovery risk increases
```

## Related Content

- [Trade Skills](../../content/skills/class/trade.md)
- [Materials by Value](../../content/materials/index.md)
