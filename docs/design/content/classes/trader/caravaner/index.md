---
title: Caravaner
gdd_ref: systems/class-system-gdd.md#specialization-classes
parent: classes/trader/index.md
tree_tier: 2
---

# Caravaner

## Lore

### Origin

Local trade moves goods within regions. Long-distance trade connects distant markets, exploiting massive price differences between locations. A good worth coppers in one city sells for silver elsewhere. Spices abundant in southern ports command premium prices in northern kingdoms. Luxury goods unavailable locally become status symbols imported from afar. Caravaners bridge these gaps, enduring dangerous journeys for substantial profits.

The profession emerged when settlements grew wealthy enough to afford distant goods. Early Caravaners were adventurous Traders willing to risk long journeys. They learned routes through wilderness, negotiated with bandits and local powers, survived environmental hazards, and managed logistics of moving valuable cargo hundreds of miles. Success brought wealth; failure brought death or bankruptcy.

Caravaning requires skills beyond normal trading. Route planning considering distance, terrain, water sources, safe camps, and political borders. Security management protecting valuable cargo from bandits and monsters. Animal handling keeping draft beasts healthy through long journeys. Weather prediction avoiding dangerous storms. Diplomatic skills negotiating passage through various territories. Crisis management when things inevitably go wrong. Few Traders possess all these abilities - successful Caravaners either learn through hard experience or die trying.

### In the World

Caravaners form the economic bloodstream connecting distant regions. They transport trade goods, luxury items, news, and occasionally passengers. Major trade routes see regular caravans - dozens of wagons traveling together for mutual protection. Lesser-known routes attract small independent operators seeking higher margins through riskier paths.

The profession divides into specialists. Established caravan companies run regular routes with large wagons, guards, and predictable schedules. Independent Caravaners operate flexibly, adjusting routes based on opportunities and dangers. Luxury goods specialists transport small, valuable cargos requiring careful handling. Bulk haulers move commodity goods in massive quantities. Each approach has advantages - large operations offer security and reliability, small operators provide flexibility and higher profit margins.

Life as a Caravaner means extended periods traveling between settlements. Weeks or months on the road, sleeping in camps or roadside inns, facing bandits, bad weather, mechanical failures, and endless logistics. Between journeys, Caravaners spend time in trade cities - selling goods, buying new cargo, repairing wagons, hiring guards, gathering route intelligence. The work is exhausting but lucrative when successful.

Apprentice Caravaners typically join established operations as assistants or guards, learning practical skills through experience. They study route planning, cargo securing, animal care, and threat assessment. They discover that successful caravaning involves constant small decisions - when to push forward versus making camp, which route variant to take, how to handle suspicious travelers. Good judgment develops through surviving bad situations.

Veteran Caravaners know routes intimately - every river crossing, mountain pass, dangerous stretch, and safe camp. They maintain networks of contacts along routes - settlement officials offering fair passage fees, merchants providing reliable goods, informants warning of bandit activity. They develop sixth senses about danger, knowing when something feels wrong even if they can't articulate why. The best Caravaners become wealthy from decades of successful journeys, though many die before reaching that point.

---

## Mechanics

### Prerequisites

| Requirement        | Value                                             |
| ------------------ | ------------------------------------------------- |
| XP Threshold       | 5,000 XP from caravan trading tracked actions     |
| Related Foundation | [Trader](../trader/) (optional, provides bonuses) |
| Tag Depth Access   | 2 levels (e.g., `[Trade/Caravan]`)                |

### Requirements

| Requirement       | Value                                            |
| ----------------- | ------------------------------------------------ |
| Unlock Trigger    | Complete long-distance trade routes              |
| Primary Attribute | END (Endurance), INT (Intelligence)              |
| Starting Level    | 1                                                |
| Tools Required    | Wagon/pack animals, trade goods, route knowledge |

### Stats

#### Base Class Stats

| Level | HP Bonus | Stamina Bonus | Trait                |
| ----- | -------- | ------------- | -------------------- |
| 1     | +8       | +15           | Apprentice Caravaner |
| 10    | +24      | +45           | Journeyman Caravaner |
| 25    | +50      | +95           | Master Caravaner     |
| 50    | +90      | +170          | Legendary Caravaner  |

#### Commerce Bonuses

| Class Level | Distance Trade | Route Efficiency | Cargo Security | Survival |
| ----------- | -------------- | ---------------- | -------------- | -------- |
| 1-9         | +10%           | +10%             | +5%            | +10%     |
| 10-24       | +30%           | +25%             | +15%           | +25%     |
| 25-49       | +65%           | +50%             | +35%           | +50%     |
| 50+         | +120%          | +85%             | +65%           | +85%     |

#### Essential Starting Skills

| Skill          | Type     | Source | Effect                                         |
| -------------- | -------- | ------ | ---------------------------------------------- |
| Caravan Master | Mechanic | Trade  | Leading and managing caravans efficiently      |
| Market Sense   | Lesser   | Trade  | Understanding trade routes and regional prices |

#### Synergy Skills

Skills that have strong synergies with Caravaner. These skills can be learned by any character, but Caravaners gain significant bonuses (2x XP acquisition, enhanced effectiveness, reduced costs). Higher Caravaner levels provide stronger synergy bonuses.

**Trade Skills**:

- [Market Sense](../../skills/tiered/market-sense/index.md) - Lesser/Greater/Enhanced - Know regional price differences
- [Silver Tongue](../../skills/tiered/silver-tongue/index.md) - Lesser/Greater/Enhanced - Negotiate passage and deals
- [Trade Network](../../skills/passive-generator/trade-network/index.md) - Passive Generator - Contacts along trade routes
- [Trade Contacts](../../skills/passive-generator/trade-contacts/index.md) - Passive Generator - Route-specific suppliers and buyers
- [Caravan Master](../../skills/mechanic-unlock/caravan-master/index.md) - Lesser/Greater/Enhanced - Lead and manage caravans efficiently
- [Commodity Speculation](../../skills/mechanic-unlock/commodity-speculation/index.md) - Lesser/Greater/Enhanced - Profit from regional arbitrage

#### Synergy Bonuses

Caravaner provides context-specific bonuses to long-distance trade skills based on logical specialization:

**Core Trade Skills** (Direct specialization - Strong synergy):

- **Route Knowledge**: Essential for efficient long-distance travel
  - Faster learning: 2x XP from travel actions (scales with class level)
  - Better effectiveness: +25% route efficiency at Caravaner 15, +35% at Caravaner 30+
  - Reduced cost: -25% stamina at Caravaner 15, -35% at Caravaner 30+
- **Cargo Security**: Directly tied to protecting valuable goods
  - Faster learning: 2x XP from protection actions
  - Better protection: +30% cargo security at Caravaner 15
  - Lower loss rate: -50% cargo damage from threats
- **Beast Handling**: Core skill for managing pack animals
  - Faster learning: 2x XP from animal care actions
  - Better effectiveness: +20% animal health at Caravaner 15
  - Lower costs: -20% feed and care costs at Caravaner 15
- **Trade Route Mastery**: Fundamental to profitable caravaning
  - Faster learning: 2x XP from route optimization
  - Better efficiency: +25% travel speed at Caravaner 15
  - More profits: +50% chance to discover lucrative shortcuts

**Synergy Strength Scales with Class Level**:

| Caravaner Level | XP Multiplier | Effectiveness Bonus | Cost Reduction | Example: Route Knowledge |
| --------------- | ------------- | ------------------- | -------------- | ------------------------ |
| Level 5         | 1.5x (+50%)   | +15%                | -15% stamina   | Good but moderate        |
| Level 10        | 1.75x (+75%)  | +20%                | -20% stamina   | Strong improvements      |
| Level 15        | 2.0x (+100%)  | +25%                | -25% stamina   | Excellent synergy        |
| Level 30+       | 2.5x (+150%)  | +35%                | -35% stamina   | Masterful synergy        |

**Example Progression**:

A Caravaner 15 learning Route Knowledge:

- Performs 50 travel actions (earning 2x XP = 1000 XP total, vs 500 XP for non-Caravaner)
- Route Knowledge available at level-up after just 500 XP (vs 1500 XP for non-trade class)
- When using Route Knowledge: Base route efficiency becomes +25% better (synergy bonus)
- Stamina cost: 7.5 stamina instead of 10 (25% reduction)

#### Tracked Actions

Actions that grant XP to the Caravaner class:

| Action Category      | Specific Actions                                  | XP Value              |
| -------------------- | ------------------------------------------------- | --------------------- |
| Long-Distance Trade  | Complete trade routes between distant settlements | 30-150 per route      |
| Route Travel         | Travel established trade routes                   | 10-50 per journey     |
| Cargo Protection     | Successfully protect cargo from threats           | 15-80 per threat      |
| Navigation           | Navigate difficult terrain or weather             | 10-40 per challenge   |
| Animal Management    | Care for pack animals successfully                | 5-20 per journey      |
| Route Discovery      | Find new trade routes or shortcuts                | 50-200 per discovery  |
| Diplomatic Relations | Negotiate passage through territories             | 10-60 per negotiation |
| Crisis Management    | Handle emergencies on the road                    | 20-100 per crisis     |
| Profitable Journey   | Complete extremely profitable trade runs          | 50-300 per journey    |
| Caravan Leadership   | Lead caravan groups successfully                  | 30-150 per journey    |

#### Class Consolidation

See [Class Consolidation System](../../../systems/character/class-consolidation/index.md) for full mechanics.

| Consolidation Path                                 | Requirements                                  | Result Class      | Tier |
| -------------------------------------------------- | --------------------------------------------- | ----------------- | ---- |
| [Caravan Master](../consolidation/index.md)        | Caravaner + lead large caravans               | Caravan Master    | 1    |
| [Route Specialist](../consolidation/index.md)      | Caravaner + master specific route             | Route Specialist  | 1    |
| [Explorer-Merchant](../consolidation/index.md)     | Caravaner + [Scout](../combat/scout/index.md) | Explorer-Merchant | 1    |
| [Trade Consortium Head](../consolidation/index.md) | Caravaner + multiple trade operations         | Trade Consortium  | 2    |

### Interactions

| System                                                          | Interaction                               |
| --------------------------------------------------------------- | ----------------------------------------- |
| [Trade Routes](../../../systems/economy/index.md)               | Defines and travels long-distance routes  |
| [Travel](../../../systems/world/travel/index.md)                | Extended travel mechanics; route planning |
| [Survival](../../../systems/world/environment-hazards/index.md) | Must survive environmental challenges     |
| [Combat](../../../systems/combat/combat-resolution/index.md)    | Defends against bandits and monsters      |
| [Economy](../../../systems/economy/index.md)                    | Connects distant markets economically     |
| [Settlements](../../../systems/world/settlements/index.md)      | Links settlements through trade           |

---

## Related Content

- **Requires:** Wagon or pack animals, trade goods, route knowledge, supplies
- **Equipment:** [Wagons](../../items/index.md), [Pack Animals](../../creatures/index.md), [Camping Gear](../../items/index.md)
- **Synergy Classes:** [Trader](../trader/), [Scout](../combat/scout/index.md), [Adventurer](../combat/adventurer/index.md)
- **Consolidates To:** [Caravan Master](../consolidation/index.md), [Explorer-Merchant](../consolidation/index.md), [Trade Consortium](../consolidation/index.md)
- **See Also:** [Travel System](../../../systems/world/travel/index.md), [Trade Routes](../../../systems/economy/index.md), [Environment Hazards](../../../systems/world/environment-hazards/index.md)
