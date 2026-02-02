---
type: system
scope: detailed
status: draft
version: 1.0.0
created: 2026-02-02
updated: 2026-02-02
subjects: [game-design, progression, prestige]
dependencies: []
---

# Unified Prestige & Ascension Framework - Design Document

## Overview

This document defines the prestige systems that allow players to reset progress for permanent bonuses, providing long-term engagement and a sense of growing power across multiple play cycles.

---

## Prestige Architecture

### Three-Tier System

```
┌─────────────────────────────────────────────────────────────┐
│                    TIER 3: TRANSCENDENCE                     │
│         (Reset everything, gain Transcendent Power)          │
├─────────────────────────────────────────────────────────────┤
│                    TIER 2: ASCENSION                         │
│    (Reset all scenarios, gain Ascension Points & Unlocks)    │
├─────────────────────────────────────────────────────────────┤
│                    TIER 1: SCENARIO PRESTIGE                 │
│   (Reset single scenario, gain Legacy Points for that tree)  │
└─────────────────────────────────────────────────────────────┘
```

Each tier resets more but grants proportionally greater permanent power.

---

## Tier 1: Scenario Prestige

### Overview

Reset a single scenario to gain Legacy Points for that scenario's permanent upgrade tree.

### Individual Scenario Prestige Names

| Scenario  | Prestige Name       | Thematic Justification            |
| --------- | ------------------- | --------------------------------- |
| Inn       | "Grand Reopening"   | Rebuild reputation elsewhere      |
| Guild     | "Charter Renewal"   | New guild generation              |
| Farm      | "Legendary Lineage" | Perfect specimen achieved         |
| Alchemy   | "Magnum Opus"       | Ultimate creation complete        |
| Territory | "New Dynasty"       | Kingdom complete, heir takes over |
| Summoner  | "True Name Mastery" | Planar mastery achieved           |
| Caravan   | "Trade Dynasty"     | Empire established, start fresh   |

### What Resets (Per Scenario)

| Resets               | Retained                   |
| -------------------- | -------------------------- |
| Scenario level → 1   | Player level               |
| Facilities → basic   | Universal skills           |
| Resources → starting | Codex entries              |
| NPCs → dispersed     | Recipe/blueprint knowledge |
| Progress → zero      | Other scenarios untouched  |

### Legacy Points Formula

```
Legacy Points = Base Value × (Scenario Level / 10) × Prestige Multiplier × Efficiency Bonus

Where:
- Base Value = 100
- Prestige Multiplier = 1 + (0.1 × Previous Prestiges)
- Efficiency Bonus = Time Factor (faster = more points)
```

### Scenario Legacy Trees

Each scenario has unique permanent upgrades:

#### Inn Legacy Tree (Example)

| Upgrade                   | Cost    | Effect                          |
| ------------------------- | ------- | ------------------------------- |
| Loyal Regulars I-V        | 50-500  | Start with 1-5 regular guests   |
| Famous Recipes I-III      | 100-300 | Begin with 3-9 recipes unlocked |
| Experienced Staff I-V     | 75-400  | Staff start at level 2-10       |
| Storied Walls I-III       | 150-450 | +5-15% base reputation          |
| Innkeeper's Intuition I-V | 100-500 | +2-10% guest satisfaction       |
| Grand Legacy              | 1000    | Unique inn cosmetic + title     |

#### Guild Legacy Tree (Example)

| Upgrade                 | Cost    | Effect                         |
| ----------------------- | ------- | ------------------------------ |
| Veteran Network I-V     | 50-500  | Better starting applicants     |
| Established Name I-III  | 100-300 | Base reputation in all regions |
| Inherited Armory I-V    | 75-400  | Start with rare equipment      |
| Training Doctrine I-III | 150-450 | +5-15% XP gain for adventurers |
| Tactical Archives I-V   | 100-500 | Quest success rate +2-10%      |
| Legendary Charter       | 1000    | Unique guild cosmetic + title  |

### Prestige Requirements

Minimum thresholds to prestige:

| Scenario  | Minimum Requirement                            |
| --------- | ---------------------------------------------- |
| Inn       | Tier 4 establishment OR 50,000 reputation      |
| Guild     | Rank 8 OR 100 quests completed                 |
| Farm      | SS-grade monster OR complete collection tier   |
| Alchemy   | Legendary recipe crafted OR all Expert recipes |
| Territory | 30 regions OR Wonder constructed               |
| Summoner  | Epic entity evolved OR 50 contracts lifetime   |
| Caravan   | 500,000 gold profit OR all major routes        |

---

## Tier 2: Ascension

### Overview

Reset ALL scenarios simultaneously for Ascension Points, which provide powerful global bonuses and unlock new content.

### Ascension Requirements

To Ascend, must complete at least ONE of:

- Prestige 3+ scenarios (any combination)
- Reach scenario level 75+ in any single scenario
- Complete the "World Event" storyline (spans all scenarios)
- Accumulate 1,000,000 lifetime gold across all scenarios

### What Resets

| Resets                      | Retained                       |
| --------------------------- | ------------------------------ |
| ALL scenario progress       | Legacy upgrades purchased      |
| Player level → 1            | Codex (partial)                |
| Universal skills → refunded | Achievement points             |
| Gold → starting             | Ascension Points from this run |
| All facilities/units        | Previous Ascension upgrades    |

### Ascension Points Formula

```
Ascension Points = Σ(Scenario Contribution) × Ascension Multiplier

Where:
- Scenario Contribution = (Level × Prestige Count × 10) + (Legacy Points Spent × 0.5)
- Ascension Multiplier = 1 + (0.25 × Previous Ascensions)
```

### Ascension Upgrade Tree

Global bonuses affecting all scenarios:

#### Foundation Branch

| Upgrade             | Cost     | Effect                              |
| ------------------- | -------- | ----------------------------------- |
| Quick Start I-X     | 100-1000 | +5-50% early game speed             |
| Resource Boost I-X  | 150-1500 | +2-20% all resource generation      |
| XP Acceleration I-X | 200-2000 | +3-30% all XP gain                  |
| Offline Mastery I-V | 500-2500 | +10-50% offline progress efficiency |

#### Synergy Branch

| Upgrade              | Cost      | Effect                                     |
| -------------------- | --------- | ------------------------------------------ |
| Enhanced Synergy I-V | 300-1500  | +20-100% synergy bonuses                   |
| Cross-Training I-V   | 400-2000  | Scenario XP partially shared               |
| Resource Flow I-V    | 350-1750  | Inter-scenario resource transfer improved  |
| United Front I-III   | 1000-3000 | All scenarios count as "active" for events |

#### Mastery Branch

| Upgrade                   | Cost      | Effect                              |
| ------------------------- | --------- | ----------------------------------- |
| Legacy Acceleration I-V   | 500-2500  | +10-50% Legacy Point gain           |
| Prestige Readiness I-V    | 600-3000  | Prestige requirements reduced 5-25% |
| Knowledge Retention I-III | 1000-3000 | Retain 10-30% of codex on Ascension |
| Ascendant Wisdom I-III    | 2000-6000 | +10-30% Ascension Point gain        |

### Ascension Unlocks

New content available only after Ascending:

| Ascension Count | Unlock                                               |
| --------------- | ---------------------------------------------------- |
| 1               | "Ascended" title, Ascension portrait frame           |
| 2               | Eighth scenario hint: "The Academy" (training focus) |
| 3               | Hardcore mode option                                 |
| 5               | Eighth scenario unlock: "The Academy"                |
| 7               | Challenge modes for all scenarios                    |
| 10              | "Transcendence" tier available                       |

---

## Tier 3: Transcendence

### Overview

The ultimate reset for players who have deeply engaged with Ascension. Resets Ascension progress for Transcendent Power—permanent multipliers that fundamentally change game scaling.

### Transcendence Requirements

- Complete 5+ Ascensions
- Reach combined Ascension upgrades of 50+
- Complete "The Eternal Cycle" storyline

### What Resets

| Resets                | Retained                           |
| --------------------- | ---------------------------------- |
| Ascension upgrades    | Transcendent Power                 |
| Ascension points      | Legacy upgrades (discounted rebuy) |
| All scenario progress | Full Codex                         |
| Player level          | Achievements                       |
| Universal skills      | Cosmetics                          |

### Transcendent Power

Permanent multipliers that persist forever:

| Power                   | Effect                   | Scaling                            |
| ----------------------- | ------------------------ | ---------------------------------- |
| **Eternal Wisdom**      | All XP gain multiplied   | ×1.5 base, +0.1 per Transcendence  |
| **Infinite Prosperity** | All gold gain multiplied | ×1.5 base, +0.1 per Transcendence  |
| **Timeless Efficiency** | All timers reduced       | ×0.9 base, -0.02 per Transcendence |
| **Boundless Potential** | All caps increased       | ×1.2 base, +0.05 per Transcendence |

### Transcendence Unlocks

| Transcendence Count | Unlock                                      |
| ------------------- | ------------------------------------------- |
| 1                   | "Transcendent" title, unique visual effects |
| 2                   | New Game+ story variants                    |
| 3                   | Ultimate challenge modes                    |
| 5                   | Developer commentary unlocked               |
| 10                  | "Eternal" cosmetic set                      |

---

## Prestige Strategy & Pacing

### Recommended Progression Path

#### Early Game (0-50 hours)

- Focus on 1-2 scenarios
- Reach prestige threshold in primary scenario
- First Tier 1 prestige around hour 20-30
- Unlock 3-4 scenarios total

#### Mid Game (50-200 hours)

- Prestige 2-3 scenarios each
- Build Legacy trees
- Explore synergies
- First Ascension around hour 100-150

#### Late Game (200-500 hours)

- Multiple Ascensions
- All scenarios prestiged multiple times
- Optimization focus
- Challenge content

#### End Game (500+ hours)

- Transcendence achieved
- Mastery of all systems
- Collection completion
- Speed run / efficiency focus

### Prestige Timing Considerations

#### When to Prestige (Scenario)

Prestige when:

- Progress slows significantly
- Legacy upgrades would help more than continued progress
- You want to experience early game with bonuses
- Achievement/challenge requires fresh start

Don't prestige when:

- Close to major unlock
- Mid-event with scenario-specific rewards
- Haven't explored all content yet

#### When to Ascend

Ascend when:

- Legacy trees feel "complete enough"
- Global bonuses would help more than scenario bonuses
- Ready for fresh experience across all scenarios
- Achievement requires Ascension

Don't Ascend when:

- Haven't prestiged scenarios you enjoy
- Major event ongoing
- Close to Ascension unlock threshold with current progress

---

## Prestige Currency Economy

### Currency Types

| Currency                     | Earned From       | Spent On               |
| ---------------------------- | ----------------- | ---------------------- |
| Legacy Points (per scenario) | Scenario Prestige | Scenario Legacy Tree   |
| Ascension Points             | Ascension         | Ascension Upgrade Tree |
| Transcendent Essence         | Transcendence     | Transcendent Power     |

### Currency Persistence

| Event             | Legacy Points                 | Ascension Points | Transcendent Essence |
| ----------------- | ----------------------------- | ---------------- | -------------------- |
| Scenario Prestige | Gained                        | Unchanged        | Unchanged            |
| Ascension         | Unchanged (spent stays spent) | Gained           | Unchanged            |
| Transcendence     | Can rebuy at 50% cost         | Reset            | Gained               |

### Anti-Hoarding Mechanics

- No benefit to holding currency unspent
- Diminishing returns on single upgrade paths
- Breadth rewarded over depth
- Soft caps on individual upgrade effects

---

## Visual Progression Indicators

### Prestige Cosmetics

Each prestige tier adds visual flair:

| Tier                 | Visual Element                 |
| -------------------- | ------------------------------ |
| Scenario Prestige 1  | Bronze border on scenario icon |
| Scenario Prestige 3  | Silver border                  |
| Scenario Prestige 5  | Gold border                    |
| Scenario Prestige 10 | Platinum border with glow      |
| Ascension 1          | Star above player portrait     |
| Ascension 5          | Constellation pattern          |
| Ascension 10         | Celestial aura                 |
| Transcendence 1      | Reality-warping visual effect  |
| Transcendence 5      | Unique particle system         |

### Profile Display

Player profile shows:

- Total prestige count (all scenarios)
- Ascension count
- Transcendence count
- Highest single-scenario prestige
- Favorite scenario (most time spent)
- Rarest achievement

---

## Prestige Achievements

### Scenario Prestige Achievements

| Achievement     | Requirement                            |
| --------------- | -------------------------------------- |
| First Steps     | Complete first prestige (any)          |
| Dedicated       | Prestige same scenario 5 times         |
| Well-Rounded    | Prestige all 7 scenarios at least once |
| Prestige Hunter | 25 total scenario prestiges            |
| Prestige Master | 50 total scenario prestiges            |

### Ascension Achievements

| Achievement      | Requirement                     |
| ---------------- | ------------------------------- |
| Ascendant        | Complete first Ascension        |
| Repeat Ascender  | Complete 5 Ascensions           |
| Ascension Master | Complete 10 Ascensions          |
| Speed Ascender   | Ascend within 24 hours playtime |

### Transcendence Achievements

| Achievement   | Requirement                  |
| ------------- | ---------------------------- |
| Beyond Mortal | Complete first Transcendence |
| Eternal Cycle | Complete 3 Transcendences    |
| True Immortal | Complete 10 Transcendences   |

---

## Related Documents

- **[Idle Game Overview](idle-game-overview.md)** — Game context for why prestige systems exist
- **[Shared Progression Systems](idle-shared-progression.md)** — Universal progression that prestige resets and enhances
- **[Meta-Game Integration Layer](idle-meta-integration.md)** — How prestige works across all scenarios
- **[Adventurer Guild](idle-adventurer-guild.md)** — Example "Guild Charter Renewal" prestige
- **[Alchemy/Potion Brewing](idle-alchemy.md)** — Example "Magnum Opus" prestige
- **[Monster Farm/Ranch](idle-monster-farm.md)** — Example "Legendary Lineage" prestige
- **[Idle Inn/Tavern Management](idle-inn-gdd.md)** — Core GDD showing how classes and progression lead to prestige triggers
