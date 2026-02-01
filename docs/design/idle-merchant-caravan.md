# Merchant Caravan - Idle Game Design Document

## Core Fantasy
You are a [Merchant Prince] building a trading empire across a fantastical world. Send caravans along dangerous routes, exploit price differences between cities, and grow from a single wagon to a network of trade routes spanning continents. This scenario emphasizes economic gameplay, route optimization, and risk management as your caravans traverse monster-infested wilds.

---

## Primary Game Loop

```
BUY → LOAD → DISPATCH → TRAVEL → SELL → PROFIT → EXPAND → BUY (more)
```

1. **Buy Goods** (active selection)
   - Purchase goods at current location
   - Prices vary by city, season, events
   - Limited stock for rare goods

2. **Load Caravan** (management)
   - Assign goods to caravans
   - Balance cargo capacity
   - Add supplies for journey

3. **Dispatch on Route** (active selection, idle travel)
   - Choose destination
   - Select route (safe vs. fast vs. profitable)
   - Caravan departs

4. **Travel** (idle core)
   - Caravan progresses along route
   - Random encounters possible
   - Time based on distance, terrain, caravan speed

5. **Sell Goods** (automatic or active)
   - Arrive at destination
   - Sell at local prices
   - Profit = Sell Price - Buy Price - Costs

6. **Expand Operations**
   - Buy more caravans
   - Hire better guards
   - Unlock new trade routes
   - Establish warehouses

---

## Resource Systems

### Primary Resources
| Resource | Generation | Primary Use |
|----------|------------|-------------|
| **Gold** | Trade profits | Everything |
| **Trade Reputation** | Successful deliveries | Unlock routes, better prices |
| **Contacts** | Relationship building | Information, special deals |
| **Caravan Capacity** | Wagons, beasts | How much you can move |

### Operational Resources
- **Supplies**: Consumed during travel (food, water, fodder)
- **Guard Strength**: Protection against threats
- **Caravan Speed**: How fast routes complete
- **Cargo Space**: How many goods fit

---

## Trade Goods System

### Good Categories
| Category | Examples | Weight | Value | Volatility |
|----------|----------|--------|-------|------------|
| **Basic** | Grain, Salt, Cloth | Light | Low | Low |
| **Crafted** | Tools, Pottery, Furniture | Medium | Medium | Low |
| **Luxury** | Silk, Spices, Jewelry | Light | High | Medium |
| **Exotic** | Monster Parts, Magic Items | Variable | Very High | High |
| **Contraband** | Forbidden goods | Variable | Extreme | Extreme |

### Price Factors
Prices at each city affected by:
- **Local Production**: Produces = low price
- **Local Demand**: Needs = high price
- **Distance**: Farther from source = higher price
- **Season**: Harvest lowers food, winter raises it
- **Events**: War, festivals, disasters shift prices
- **Scarcity**: Limited supply increases price

### Price Discovery
- Basic prices known
- Detailed prices require scouts or contacts
- Real-time price updates cost money
- Opportunity: Buy low, sell high before others adjust

---

## Caravan System

### Caravan Components
| Component | Function | Examples |
|-----------|----------|----------|
| **Transport** | Carries goods | Wagon, Cart, Pack Animals, Airship |
| **Guards** | Protection | Mercenaries, Adventurers, Golems |
| **Driver** | Speed, efficiency | Teamster, Beastmaster, Pilot |
| **Specialist** | Bonus effects | Navigator, Healer, Negotiator |

### Caravan Tiers
| Tier | Capacity | Speed | Guard Slots |
|------|----------|-------|-------------|
| **Pack Mule** | 10 units | Slow | 0 |
| **Small Cart** | 25 units | Slow | 1 |
| **Wagon** | 50 units | Medium | 2 |
| **Large Wagon** | 100 units | Medium | 3 |
| **Caravan Train** | 300 units | Medium | 5 |
| **Merchant Ship** | 500 units | Route-dependent | 4 |
| **Airship** | 200 units | Fast | 3 |

### Guard System
Guards protect against route dangers:
- **Mercenary**: Cheap, moderate effectiveness
- **Adventurer**: Expensive, high effectiveness
- **Monster**: Very effective, scares some customers
- **Golem**: No upkeep, expensive initial cost

---

## Route System

### Route Attributes
| Attribute | Meaning |
|-----------|---------|
| **Distance** | Travel time |
| **Terrain** | Speed modifier, danger type |
| **Danger Level** | Encounter frequency/severity |
| **Toll Costs** | Fixed costs along route |
| **Special Features** | Shortcuts, hazards, opportunities |

### Route Types
| Type | Time | Danger | Cost | Notes |
|------|------|--------|------|-------|
| **Main Road** | Normal | Low | Tolls | Safe, expensive |
| **Back Roads** | Longer | Medium | None | Slower, cheaper |
| **Wilderness** | Shortest | High | None | Dangerous but fast |
| **Sea Route** | Variable | Medium | Port fees | Weather dependent |
| **Air Route** | Fastest | Low | High fuel | Requires airship |
| **Underground** | Hidden | Very High | None | Dwarven tunnels |

### Route Discovery
- Main routes known from start
- Hidden routes discovered through:
  - Exploration (send scouts)
  - Contacts (buy information)
  - Random events
  - Progression unlocks

---

## City & Market System

### City Types
| Type | Characteristics | Best Goods |
|------|-----------------|------------|
| **Capital** | High demand, all goods | Luxury, crafted |
| **Port** | Sea access, foreign goods | Exotic imports |
| **Mining Town** | Metals cheap, food expensive | Buy ore, sell food |
| **Farming Village** | Food cheap, tools expensive | Buy grain, sell tools |
| **Border Town** | Foreign goods, danger | Exotic, contraband |
| **Magic City** | Arcane goods | Magic items, components |

### Market Mechanics
- Each city has buy/sell prices for all goods
- Prices update on schedule (daily, weekly)
- Stock limits for rare goods
- Reputation affects prices (better deals with higher rep)

### Establishing Presence
In each city, build:
- **Warehouse**: Store goods between trips
- **Trade Office**: Better prices, market information
- **Contacts**: Special deals, inside information
- **Trade Agreement**: Bulk discounts, exclusive goods

---

## Prestige System: "Trade Dynasty"

### Trigger Conditions
- Establish trade offices in all major cities
- Complete the "Continental Route" connecting all regions
- Accumulate 1,000,000 gold profit in one reset

### Reset Mechanics
- Caravans sold (converted to Legacy Points)
- Warehouses and offices lost
- Routes remain discovered
- Retain: Trading skills, contact network, route knowledge
- Gain: "Dynasty Points"

### Legacy Bonuses
- "Established Name" - Better starting reputation everywhere
- "Family Secrets" - Start with rare route knowledge
- "Inherited Fleet" - Begin with upgraded caravan
- "Trade Instinct" - See price trends earlier

---

## Active vs. Idle Balance

### Idle Mechanics
- Caravans travel automatically
- Auto-sell at destination (if enabled)
- Auto-buy based on shopping lists
- Passive income from trade offices
- Route completion continues offline

### Active Engagement Rewards
- **Arbitrage Hunting**: Spot price differences for big profits
- **Negotiation**: Haggle for better prices
- **Route Selection**: Optimize path for current conditions
- **Encounter Decisions**: Handle events for bonus/avoid loss
- **Market Timing**: Buy/sell at optimal moments
- **Contract Bidding**: Compete for lucrative special deliveries

### Notification System
- Caravan arrived
- Exceptional price opportunity
- Caravan under attack
- Contract available
- Market shift detected

---

## Events & Encounters

### Route Encounters
| Encounter | Risk | Possible Outcomes |
|-----------|------|-------------------|
| **Bandits** | Loss of goods | Fight, bribe, flee |
| **Monster Attack** | Damage, losses | Fight, flee |
| **Broken Wheel** | Delay | Repair, abandon goods |
| **Fellow Traveler** | None | Information, trade |
| **Shortcut Found** | Opportunity | Save time |
| **Toll Increase** | Extra cost | Pay, detour, negotiate |
| **Weather** | Delay/danger | Wait, push through |

### Market Events
- **Festival**: Demand for luxury goods spikes
- **War**: Weapons valuable, routes dangerous
- **Plague**: Medicine prices soar
- **Harvest**: Food prices crash then recover
- **Discovery**: New exotic goods available

### Story Events
- A noble offers exclusive contract for dangerous delivery
- Trade war with rival merchant—undercut or cooperate?
- Smuggling opportunity with huge profit and huge risk
- Discover lost trade route from ancient civilization

---

## Competition & Contracts

### Rival Merchants
Simulated AI competitors:
- Compete for limited stock
- Race to deliver contracts
- Affect prices through their actions
- Can ally or war with

### Contract System
Special delivery missions:
- **Standard**: Move X goods to Y city by Z date
- **Exclusive**: Only you can fulfill (reputation locked)
- **Competitive**: First to deliver wins
- **Dangerous**: High-risk route, premium pay
- **Bulk**: Massive quantity, logistics challenge

### Guild Mechanics
Join Merchant Guild for:
- Group contracts
- Price information network
- Protection services
- Bulk purchasing power
- Rankings and competition

---

## Synergies with Other Scenarios

- **Inn/Tavern**: Caravans rest at player inns; inn provides rumors on prices
- **Adventurer Guild**: Hire adventurers as guards; deliver guild supplies
- **Alchemy**: Transport potions; alchemist needs exotic ingredients
- **Territory**: Trade routes through your territory; territory produces trade goods
- **Monster Farm**: Monster materials as exotic goods; beasts as transport

---

## Economic Formulas

### Basic Profit
```
Profit = Sell Price - Buy Price - Transport Costs - Guard Wages - Tolls - Losses
```

### Transport Costs
```
Cost = Distance × (Fuel Rate + Wear Rate) × Cargo Weight Modifier
```

### Risk Calculation
```
Encounter Chance = Base Route Danger × (1 - Guard Effectiveness) × Season Modifier
```

### Price Modeling
```
City Price = Base Price × Local Modifier × Season Modifier × Event Modifier × Reputation Discount
```

---

## Sample Play Session

**Morning Market Check (5 min)**
- Review price boards: Spices cheap in Port Azure, expensive in Capital
- Check completed caravans: 2 arrived overnight, 12,000 gold profit
- Unload returning caravan, load with Capital goods for return trip

**Midday Route Planning (5 min)**
- Scout reports: Bandit activity on main road
- Reroute vulnerable caravan to safer back road
- Dispatch new caravan on spice run
- Accept lucrative contract: Deliver weapons to border town

**Evening Active Trading (15 min)**
- Price alert: Magic crystals crashed in Magic City (oversupply)
- Rush caravan to buy before recovery
- Negotiate bulk deal (active haggling minigame)
- Handle encounter: Caravan meets bandits—choose to fight (guards strong)
- Victory: Bonus loot from bandit supplies

**Overnight (idle)**
- 3 caravans traveling
- One arrival, auto-sells goods
- Prices update
- Contract deadline approaches (notification queued)
- Warehouse goods appreciate slightly
