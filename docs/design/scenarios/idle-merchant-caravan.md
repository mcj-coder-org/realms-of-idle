---
type: scenario
scope: detailed
status: draft
version: 1.0.0
created: 2026-02-02
updated: 2026-02-10
subjects: [trade, commerce, economic]
dependencies: [idle-game-overview]
---

# Merchant Caravan - Idle Game Design Document

## Core Fantasy

You are a [Caravan Master] managing routes, goods, and risks across a thriving trade network. Turn modest wagons into a vast commercial empire by predicting markets, optimizing logistics, and outmaneuvering bandits and rivals.

---

## Primary Game Loop

```
SOURCE → LOAD → TRAVEL → TRADE → PROFIT → EXPAND → SOURCE (better)
```

1. **Source Goods** (active purchasing + passive contracts)
   - Buy low at production hubs
   - Secure bulk supplier deals

2. **Load Caravans** (active planning)
   - Allocate cargo space
   - Choose escort and provisions

3. **Travel Routes** (idle core)
   - Time passes based on route length
   - Risk events resolved automatically

4. **Trade & Sell** (active timing)
   - Sell high in demand regions
   - Negotiate contracts and tariffs

5. **Expand Operations**
   - Upgrade wagons, hire crews
   - Unlock new trade hubs and exotic goods

---

## Resource Systems

### Primary Resources

| Resource             | Generation            | Primary Use                  |
| -------------------- | --------------------- | ---------------------------- |
| **Gold**             | Trading profits       | Goods, upgrades, escorts     |
| **Goods**            | Purchases, production | Trade inventory              |
| **Route Intel**      | Scouting, reputation  | Safer routes, better margins |
| **Caravan Capacity** | Wagons, animals       | More goods per trip          |

### Secondary Resources

- **Reputation**: Unlocks contracts and trade privileges
- **Security**: Reduces losses and delays
- **Influence**: Negotiation power in major cities

---

## Trade & Logistics Systems

### Goods Categories

| Category    | Examples                | Market Behavior        |
| ----------- | ----------------------- | ---------------------- |
| **Staples** | Grain, salt, cloth      | Stable, low margins    |
| **Crafts**  | Tools, weapons, pottery | Moderate margins       |
| **Luxury**  | Spices, wine, silk      | High margins, volatile |
| **Exotics** | Relics, rare reagents   | Premium, event-driven  |

### Route Planning

- Choose shortest vs safest paths
- Pay tolls or bribe officials
- Seasonal hazards affect travel time

### Crew & Escort

- **Drivers**: Increase speed
- **Guards**: Reduce bandit losses
- **Scouts**: Improve route intel
- **Quartermasters**: Reduce spoilage

---

## Market & Contract System

### Market Dynamics

- Supply/demand shifts weekly
- Events trigger price spikes
- Reputation grants access to restricted markets

### Contract Types

| Contract Type      | Duration | Risk   | Reward     |
| ------------------ | -------- | ------ | ---------- |
| **Bulk Shipment**  | Short    | Low    | Medium     |
| **Priority Cargo** | Medium   | Medium | High       |
| **Smuggling**      | Variable | High   | Very High  |
| **Relief Aid**     | Short    | Low    | Reputation |

---

## Prestige System: "Caravan Charter"

### Trigger Conditions

- Control trade routes in all regions
- Complete the Grand Relay contract chain
- Achieve top merchant rank

### Reset Mechanics

- Caravans reset to basic wagons
- Retain: Trade licenses, a portion of gold, route intel
- Gain: **Charter Tokens** for permanent bonuses

### Legacy Bonuses

- "Known Trader" - Better base prices
- "Reliable Crew" - Faster travel speed
- "Safe Routes" - Reduced loss events
- "Bulk Deals" - Increased cargo capacity

---

## Active vs. Idle Balance

### Idle Mechanics

- Caravans travel and trade automatically
- Contracts tick down and complete
- Passive income from trade posts

### Active Engagement Rewards

- **Market Timing**: Sell at peak demand windows
- **Route Intervention**: Reroute to avoid hazards
- **Negotiation**: Improve contract payouts
- **Risk Plays**: High-risk routes for premium goods

---

## Event System

### Regular Events

- **Bandit Surge**: Increased escort demand
- **Harvest Boom**: Staple prices drop
- **Festival Trade**: Luxury prices spike

### Story Events

- A rival merchant attempts to undercut your routes
- A city imposes sudden tariffs
- A lost caravan reveals a secret shortcut

### Seasonal Events

- **Winter Pass**: Some routes close
- **Spring Reopening**: Trade volume spikes
- **Monsoon Season**: Travel delays and spoilage risk

---

## Synergies with Other Scenarios

- **Inn/Tavern**: Supply deals reduce costs, increase margins
- **Adventurer Guild**: Escorts and security contracts
- **Alchemy**: Transport rare reagents for premium profit
- **Territory**: Trade posts improve regional influence

---

## Sample Play Session

**Morning (5 min)**

- Check market board: silk prices rising in the capital
- Assign guards to a long route
- Load high-margin luxury goods

**Midday (10 min)**

- Negotiate a priority cargo contract
- Reroute around bandit activity
- Upgrade wagons for +20% capacity

**Evening (5 min)**

- Sell at peak demand for 3,200 gold profit
- Set overnight route with scouts and low risk

**Overnight (idle)**

- Caravans travel and resolve events
- Trade post generates passive income
- Contract progress advances
