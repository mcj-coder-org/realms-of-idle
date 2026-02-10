---
title: 'Merchant'
type: 'class'
category: 'trade'
tier: 2
prerequisite_xp: 5000
prerequisite_actions: trading
summary: 'Established shop owner conducting substantial commerce from permanent locations'
tags:
  - Trade
  - Commerce
  - Business
---

# Merchant

## Lore

### Origin

While Traders conduct basic commerce anywhere, Merchants establish permanent commercial operations. The distinction matters - Traders are mobile and flexible, Merchants are rooted and substantial. A Merchant owns or rents shop space, maintains significant inventory, employs assistants, and builds lasting customer relationships. This requires more capital, skill, and commitment than basic trading, but offers stability and reputation that transient Traders never achieve.

The Merchant class emerged when settlements grew large enough to support specialized shops. Early merchants were successful Traders who accumulated enough capital to rent permanent space and stock diverse inventory. This transition represents significant risk - rent, utilities, employees, and large inventory mean substantial fixed costs. Failed Merchants often lose everything. Successful ones become wealthy pillars of their communities.

Merchantry requires skills beyond trading. Merchants must manage employees, handling hiring, training, scheduling, and inevitable personnel problems. They must maintain premises - keeping shops clean, organized, secure, and appealing. They must manage larger inventories across multiple goods categories. They must handle complex finances including accounts, debts, and investments. Many successful Traders fail as Merchants, overwhelmed by operational complexity.

### In the World

Merchants define commercial districts in any substantial settlement. The general store offering everyday necessities. The weapons dealer serving adventurers and guards. The clothier providing garments and fabrics. The jeweler trading precious goods. Each Merchant carves their market niche, competing on price, quality, selection, or service.

Established Merchants wield significant social influence. They employ people, generating livelihoods. They sponsor community events. They extend credit, creating financial relationships. They gather information from countless customer interactions. Local leaders consult successful Merchants on economic matters. The wealthiest join mercantile guilds or noble ranks.

The profession involves constant balancing acts. Stock too much inventory and capital locks up uselessly while rent accumulates. Stock too little and customers find empty shelves, damaging reputation. Price too high and competitors steal business. Price too low and profit disappears. Hire too many employees and payroll crushes margins. Hire too few and service suffers. Successful Merchants navigate these tensions through experience and judgment.

Apprentice Merchants usually begin as shop assistants, learning operational details. They practice customer service, handle transactions, manage inventory, and observe the owner's decision-making. Many work years before attempting their own shops, accumulating capital and knowledge. Smart apprentices study their employer's successes and failures, learning what works before risking their own resources.

Experienced Merchants develop extensive market knowledge and supplier relationships. They know which goods sell reliably versus flashy items that look profitable but move slowly. They recognize seasonal patterns and plan inventory accordingly. They build networks with other Merchants, sharing information and occasionally collaborating on large orders. The best Merchants seem to predict market shifts before they happen, stocking goods weeks before demand spikes.

---

## Mechanics

### Prerequisites

| Requirement        | Value                                             |
| ------------------ | ------------------------------------------------- |
| XP Threshold       | 5,000 XP from trading tracked actions             |
| Related Foundation | [Trader](../trader/) (optional, provides bonuses) |
| Tag Depth Access   | 2 levels (e.g., `[Trade/Negotiation]`)            |

### Requirements

| Requirement       | Value                                            |
| ----------------- | ------------------------------------------------ |
| Unlock Trigger    | Establish shop, conduct substantial transactions |
| Primary Attribute | CHA (Charisma), INT (Intelligence)               |
| Starting Level    | 1                                                |
| Tools Required    | Shop space, substantial inventory, capital       |
| Prerequisite      | Often requires [Trader](../trader/) class        |

### Stats

#### Base Class Stats

| Level | HP Bonus | Stamina Bonus | Trait               |
| ----- | -------- | ------------- | ------------------- |
| 1     | +6       | +12           | Apprentice Merchant |
| 10    | +18      | +35           | Journeyman Merchant |
| 25    | +38      | +75           | Master Merchant     |
| 50    | +70      | +135          | Legendary Merchant  |

#### Commerce Bonuses

| Class Level | Buy Price | Sell Price | Inventory Size | Customer Base |
| ----------- | --------- | ---------- | -------------- | ------------- |
| 1-9         | -8%       | +8%        | +25%           | +10%          |
| 10-24       | -20%      | +20%       | +60%           | +30%          |
| 25-49       | -40%      | +40%       | +120%          | +65%          |
| 50+         | -65%      | +65%       | +200%          | +120%         |

#### Essential Starting Skills

| Skill         | Type              | Source | Effect                                       |
| ------------- | ----------------- | ------ | -------------------------------------------- |
| Market Sense  | Lesser            | Trade  | Understanding markets and demand forecasting |
| Trade Network | Passive Generator | Trade  | Business connections and relationships       |

#### Synergy Skills

Skills that have strong synergies with Merchant. These skills can be learned by any character, but Merchants gain significant bonuses (2x XP acquisition, enhanced effectiveness, reduced costs). Higher Merchant levels provide stronger synergy bonuses.

**Trade Skills**:

- [Market Sense](../../skills/tiered/market-sense.md) - Lesser/Greater/Enhanced - Know prices and forecast demand
- [Silver Tongue](../../skills/tiered/silver-tongue.md) - Lesser/Greater/Enhanced - Build customer loyalty and close sales
- [Trade Network](../../skills/passive-generator/trade-network.md) - Passive Generator - Supplier and customer relationships
- [Trade Contacts](../../skills/passive-generator/trade-contacts.md) - Passive Generator - Specialized wholesale contacts
- [Commodity Speculation](../../skills/mechanic-unlock/commodity-speculation.md) - Lesser/Greater/Enhanced - Stock profitable inventory

#### Synergy Bonuses

Merchant provides context-specific bonuses to shop management skills based on logical specialization:

**Core Trade Skills** (Direct specialization - Strong synergy):

- **Stock Prediction**: Essential for profitable inventory management
  - Faster learning: 2x XP from inventory actions (scales with class level)
  - Better effectiveness: +25% forecast accuracy at Merchant 15, +35% at Merchant 30+
  - Reduced cost: -25% stamina at Merchant 15, -35% at Merchant 30+
- **Supplier Relations**: Directly tied to securing good wholesale terms
  - Faster learning: 2x XP from supplier interactions
  - Better terms: +30% supplier favorability at Merchant 15
  - Lower costs: -50% relationship maintenance costs
- **Customer Loyalty**: Core skill for building repeat business
  - Faster learning: 2x XP from customer service
  - Better retention: +20% return customer rate at Merchant 15
  - Higher spending: +20% average purchase value at Merchant 15
- **Financial Acumen**: Fundamental to profitable operations
  - Faster learning: 2x XP from financial management
  - Better profitability: +25% profit margins at Merchant 15
  - More insights: +50% chance to identify financial opportunities

**Synergy Strength Scales with Class Level**:

| Merchant Level | XP Multiplier | Effectiveness Bonus | Cost Reduction | Example: Stock Prediction |
| -------------- | ------------- | ------------------- | -------------- | ------------------------- |
| Level 5        | 1.5x (+50%)   | +15%                | -15% stamina   | Good but moderate         |
| Level 10       | 1.75x (+75%)  | +20%                | -20% stamina   | Strong improvements       |
| Level 15       | 2.0x (+100%)  | +25%                | -25% stamina   | Excellent synergy         |
| Level 30+      | 2.5x (+150%)  | +35%                | -35% stamina   | Masterful synergy         |

**Example Progression**:

A Merchant 15 learning Stock Prediction:

- Performs 50 inventory management actions (earning 2x XP = 1000 XP total, vs 500 XP for non-Merchant)
- Stock Prediction available at level-up after just 500 XP (vs 1500 XP for non-trade class)
- When using Stock Prediction: Base forecast accuracy becomes +25% better (synergy bonus)
- Stamina cost: 7.5 stamina instead of 10 (25% reduction)

#### Tracked Actions

Actions that grant XP to the Merchant class:

| Action Category      | Specific Actions                           | XP Value                |
| -------------------- | ------------------------------------------ | ----------------------- |
| Shop Operation       | Maintain profitable shop operations        | 10-40 per day           |
| Large Transactions   | Complete substantial sales                 | 15-80 per transaction   |
| Inventory Management | Efficiently manage shop inventory          | 10-40 per session       |
| Employee Management  | Hire, train, manage staff effectively      | 10-50 per period        |
| Supplier Relations   | Build and maintain supplier relationships  | 10-60 per supplier      |
| Customer Service     | Provide excellent service, build loyalty   | 5-30 per customer       |
| Financial Management | Balance books, manage profits and expenses | 15-60 per period        |
| Market Expansion     | Open new shop locations or product lines   | 50-200 per expansion    |
| Guild Activities     | Participate in merchant guild activities   | 10-50 per activity      |
| Commercial Success   | Achieve exceptional profitability          | 100-400 per achievement |

#### Class Consolidation

See [Class Consolidation System](../../../systems/character/class-consolidation.md) for full mechanics.

| Consolidation Path                                 | Requirements                                   | Result Class          | Tier |
| -------------------------------------------------- | ---------------------------------------------- | --------------------- | ---- |
| [Guild Master](../consolidation/index.md)          | Merchant + guild leadership                    | Guild Master          | 1    |
| [Trading Company Owner](../consolidation/index.md) | Merchant + multiple locations                  | Trading Company Owner | 1    |
| [Specialized Dealer](../consolidation/index.md)    | Merchant + expertise in specific goods         | Specialized Dealer    | 1    |
| [Trade Baron](../consolidation/index.md)           | Multiple merchant classes + substantial wealth | Trade Baron           | 2    |

### Interactions

| System                                                       | Interaction                                    |
| ------------------------------------------------------------ | ---------------------------------------------- |
| [Economy](../../../systems/economy/index.md)                 | Major economic actor; stabilizes local markets |
| [Merchants](../../../systems/economy/merchants.md)           | Core merchant mechanics; shop operations       |
| [Settlement](../../../systems/world/settlements.md)          | Essential service provider for communities     |
| [Reputation](../../../systems/social/factions-reputation.md) | High reputation increases business             |
| [Guilds](../../../systems/social/index.md)                   | Often guild members with benefits              |
| [Employment](../../../systems/social/index.md)               | Employs workers; creates livelihoods           |

---

## Related Content

- **Requires:** Shop space, substantial capital, diverse inventory, employees
- **Equipment:** [Shop](../../items/index.md), [Display Cases](../../items/index.md), [Ledgers](../../items/index.md)
- **Prerequisite:** Often consolidates from [Trader](../trader/) with shop establishment
- **Synergy Classes:** [Trader](../trader/), [Appraiser](./appraiser/), [Caravaner](./caravaner/)
- **Consolidates To:** [Guild Master](../consolidation/index.md), [Trading Company Owner](../consolidation/index.md), [Trade Baron](../consolidation/index.md)
- **See Also:** [Economy System](../../../systems/economy/index.md), [Merchant System](../../../systems/economy/merchants.md), [Guild System](../../../systems/social/index.md)
