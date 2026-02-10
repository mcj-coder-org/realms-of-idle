---
type: scenario
scope: detailed
status: draft
version: 1.0.0
created: 2026-02-02
updated: 2026-02-10
subjects: [territory, building, strategy]
dependencies: [idle-game-overview]
---

# Territory/Domain Building - Idle Game Design Document

## Core Fantasy

You are a [Domain Steward] transforming wilderness into a thriving realm. Expand borders, build infrastructure, and balance prosperity with defense to turn a small outpost into a powerful territory.

---

## Primary Game Loop

```
CLAIM → BUILD → POPULATE → PRODUCE → DEFEND → EXPAND → CLAIM (better)
```

1. **Claim Land** (active expansion)
   - Secure tiles and regions
   - Resolve disputes or conflicts

2. **Build Infrastructure** (active planning)
   - Place buildings and roads
   - Balance production and housing

3. **Populate & Govern** (semi-idle)
   - Attract settlers and specialists
   - Set policies and taxes

4. **Produce & Collect** (idle core)
   - Resources generated over time
   - Trade and storage managed automatically

5. **Defend & Maintain** (active response)
   - Upgrade defenses
   - Handle raids and disasters

---

## Resource Systems

### Primary Resources

| Resource       | Generation           | Primary Use          |
| -------------- | -------------------- | -------------------- |
| **Gold**       | Taxes, trade, rents  | Construction, upkeep |
| **Materials**  | Production buildings | Buildings, upgrades  |
| **Population** | Housing, migration   | Workforce, taxes     |
| **Influence**  | Events, reputation   | Expansion, policies  |

### Secondary Resources

- **Stability**: Reduces unrest and disaster risk
- **Supply**: Food and necessities for population growth
- **Military**: Defense strength and patrol efficiency

---

## Territory Systems

### Zone Types

| Zone Type        | Function                | Output               |
| ---------------- | ----------------------- | -------------------- |
| **Residential**  | Housing and population  | Taxes                |
| **Industrial**   | Resource processing     | Materials, goods     |
| **Agricultural** | Food and supply         | Food, stability      |
| **Civic**        | Governance and services | Influence, stability |
| **Military**     | Defense and patrols     | Security             |

### Building Progression

- Basic → Improved → Specialized tiers
- Synergies between adjacent buildings
- Infrastructure (roads, bridges) improves efficiency

---

## Governance & Policy

### Policy Examples

- **Low Taxes**: Higher growth, lower income
- **Trade Incentives**: Increased merchant visits
- **Militia Focus**: Strong defense, slower growth

### Public Order

- Events impact stability and productivity
- Resolve unrest with investments or policy changes

---

## Prestige System: "Realm Reformation"

### Trigger Conditions

- Reach maximum territory tier
- Complete all regional upgrades
- Maintain stability above 95% for 30 days

### Reset Mechanics

- Territory resets to core outpost
- Retain: Blueprints, a portion of materials, policy unlocks
- Gain: **Reformation Seals** for permanent bonuses

### Legacy Bonuses

- "Planned Expansion" - Cheaper land claims
- "Efficient Bureaucracy" - Faster construction
- "Stable Rule" - Higher base stability
- "Veteran Militia" - Stronger defense baseline

---

## Active vs. Idle Balance

### Idle Mechanics

- Resource production runs automatically
- Population grows based on housing and stability
- Trade revenue accumulates

### Active Engagement Rewards

- **Crisis Response**: Resolve raids and disasters
- **Urban Planning**: Optimize layout for bonuses
- **Policy Tuning**: Adjust taxes and incentives
- **Expansion Push**: Timed land grabs for rare tiles

---

## Event System

### Regular Events

- **Trade Boom**: Increased gold income
- **Bandit Raids**: Defense checks
- **Migration Wave**: Population spike

### Story Events

- A neighboring lord offers alliance or conflict
- Discovery of ancient ruins in your territory
- A rebellion threatens stability

### Seasonal Events

- **Harvest Season**: Food surplus
- **Winter Storms**: Infrastructure strain
- **Spring Renewal**: Growth bonuses

---

## Synergies with Other Scenarios

- **Inn/Tavern**: More territory population increases guests
- **Merchant Caravan**: Trade posts boost route profits
- **Adventurer Guild**: Defenses and quests protect territory
- **Monster Farm**: Ranch expansion into wilderness tiles

---

## Sample Play Session

**Morning (5 min)**

- Collect taxes: 2,100 gold
- Queue housing expansion
- Resolve a minor raid event

**Midday (10 min)**

- Set policy to trade incentives
- Upgrade industrial district for material output
- Claim a new border tile

**Evening (5 min)**

- Check stability and adjust taxes
- Place a new watchtower
- Plan overnight production focus

**Overnight (idle)**

- Production and population growth tick
- Trade income accumulates
- Stability recalculates
