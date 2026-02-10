---
type: scenario
scope: detailed
status: draft
version: 1.0.0
created: 2026-02-02
updated: 2026-02-10
subjects: [summoning, contracts, planar]
dependencies: [idle-game-overview]
---

# Summoner's Contract - Idle Game Design Document

## Core Fantasy

You are a [Summoner] forging pacts with entities from other planes. Bind spirits, demons, and elemental beings to perform tasks, gather resources, and fight for your cause. Balance power and risk as you grow from minor rituals to world-altering compacts.

---

## Primary Game Loop

```
INVOKE → BIND → ASSIGN → WAIT → COLLECT → EMPOWER → INVOKE (better)
```

1. **Invoke Entities** (active ritual selection)
   - Choose ritual circle, offerings, and target plane
   - Higher risk yields stronger summons

2. **Bind Contracts** (active decisions)
   - Set duration, duties, and constraints
   - Negotiate for better terms or lower risk

3. **Assign Tasks** (semi-idle)
   - Gathering, crafting assistance, combat support
   - Unique abilities tied to entity type

4. **Await Results** (idle core)
   - Contracts tick down
   - Events and hazards resolve automatically

5. **Collect & Empower**
   - Gain rewards, essence, and reputation
   - Upgrade summoning circles and bindings

---

## Resource Systems

### Primary Resources

| Resource     | Generation                   | Primary Use               |
| ------------ | ---------------------------- | ------------------------- |
| **Gold**     | Contracts, services          | Offerings, upgrades       |
| **Essence**  | Contract completion, rituals | Summon strength, upgrades |
| **Sigils**   | Ritual crafting              | Higher-tier summons       |
| **Bindings** | Research and practice        | Risk reduction            |

### Secondary Resources

- **Favor**: Determines access to unique planar entities
- **Stability**: Reduces contract failure and backlash
- **Arcane Reputation**: Unlocks special clients and rituals

---

## Summoning Systems

### Entity Types

| Type           | Strengths           | Risks     | Typical Tasks            |
| -------------- | ------------------- | --------- | ------------------------ |
| **Elementals** | Reliable, efficient | Low       | Gathering, processing    |
| **Spirits**    | Versatile, subtle   | Medium    | Scouting, crafting       |
| **Demons**     | Powerful, fast      | High      | Combat, high yield tasks |
| **Celestials** | Safe, consistent    | Medium    | Protection, healing      |
| **Aberrants**  | Exotic effects      | Very High | Rare materials           |

### Contract Terms

- **Duration**: Short (safe) → long (risky)
- **Scope**: Single task vs multi-task
- **Clauses**: Wards, fail-safes, performance bonuses

### Binding Quality

- Affects output quality and failure chances
- Improved via research, ritual tools, and practice

---

## Ritual & Facility Progression

### Core Facilities

| Facility             | Function                    | Upgrade Benefits      |
| -------------------- | --------------------------- | --------------------- |
| **Summoning Circle** | Ritual power                | Stronger entities     |
| **Ward Array**       | Safety and stability        | Lower backlash        |
| **Archive**          | Research and sigil crafting | More contract options |
| **Binding Chamber**  | Contract enforcement        | Higher success rates  |
| **Planar Gate**      | Access to rare planes       | Unique entity unlocks |

---

## Prestige System: "Pact Renewal"

### Trigger Conditions

- Bind a legendary entity
- Complete a grand contract chain
- Maximize summoning stability

### Reset Mechanics

- Facilities reset to basic tiers
- Retain: Sigil library, a portion of essence, research
- Gain: **Pact Seals** for permanent bonuses

### Legacy Bonuses

- "Trusted Invoker" - Higher base success rate
- "Reinforced Wards" - Reduced backlash damage
- "Eloquent Contracts" - Better rewards per contract
- "Planar Access" - Early access to rare entities

---

## Active vs. Idle Balance

### Idle Mechanics

- Contracts execute automatically
- Essence and rewards accumulate
- Stability recovers over time

### Active Engagement Rewards

- **Ritual Timing**: Optimize offerings for bonuses
- **Negotiation**: Improve contract terms
- **Emergency Banish**: Prevent catastrophic failure
- **Sigil Crafting**: Active minigame for quality boosts

---

## Event System

### Regular Events

- **Planar Alignment**: Increased summon quality
- **Arcane Audit**: Temporary contract restrictions
- **Entity Migration**: New summon types available

### Story Events

- A client requests a forbidden summoning
- A rival summoner challenges your authority
- A bound entity seeks renegotiation

### Seasonal Events

- **Eclipse Night**: Rare entities appear
- **Solstice Balance**: Stability bonuses
- **Void Tide**: High-risk, high-reward contracts

---

## Synergies with Other Scenarios

- **Alchemy**: Essence fuels advanced recipes
- **Adventurer Guild**: Contracted summons support quests
- **Territory**: Planar gates boost regional bonuses
- **Monster Farm**: Summoned beasts aid breeding and protection

---

## Sample Play Session

**Morning (5 min)**

- Complete 2 elemental contracts: +120 essence
- Craft new sigils for higher-tier summons
- Upgrade ward array to reduce backlash

**Midday (10 min)**

- Negotiate a demon contract with safety clause
- Assign spirit to scouting task
- Begin long-duration summoning ritual

**Evening (5 min)**

- Resolve a contract dispute, gain reputation
- Collect rewards and stabilize bindings

**Overnight (idle)**

- Contracts tick down
- Essence and sigils accumulate
- Stability recovers
