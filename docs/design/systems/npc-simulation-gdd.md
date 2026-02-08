---
type: system
scope: detailed
status: authoritative
version: 1.0.0
created: 2026-02-08
updated: 2026-02-08
subjects: [npc-simulation, needs, schedules, satisfaction, decisions, idle]
dependencies: [personality-traits-gdd.md]
---

# NPC Simulation System - Authoritative Game Design

## Executive Summary

The NPC Simulation System governs how NPCs behave over time through needs-driven motivation, schedule-based activity cycles, satisfaction tracking, and autonomous decision-making. Where `npc-core-systems-gdd.md` defines _what_ NPCs want (goals, conflicts, failure states) and `npc-design.md` defines _who_ NPCs are (attributes, classes, relationships), this document defines _how_ NPCs live day-to-day: eating, working, resting, socializing, and choosing what to do when idle.

All simulation is batch-processed at player check-in. NPCs do not tick in real time. The system calculates elapsed time since the last check-in, runs schedule resolution, updates need levels, processes autonomous decisions, and presents results.

**This document resolves:**

- NPC needs hierarchy and satisfaction mechanics
- Need-to-performance multiplier system
- Daily schedule abstraction (batch-calculated work/rest/social cycles)
- Autonomous decision-making when NPCs have free time
- Level of Detail system for simulation fidelity scaling
- Settlement satisfaction aggregation
- NPC migration (attraction and departure)
- Service NPC availability and schedules
- Passive skill development during offline time
- Offline simulation batch processing

**Design Philosophy:** NPCs are not real-time agents. They are state machines whose transitions are calculated in batches. Personality and satisfaction act as simple multipliers on outcomes, not as inputs to complex AI. The player's strategic choices (assign NPCs to roles, upgrade facilities, ensure supply) matter; the execution is automated.

---

## 1. NPC Needs Hierarchy

### 1.1 Need Categories

NPCs have five needs arranged in priority order. Lower needs must be minimally satisfied before higher needs contribute meaningfully to performance:

| Priority | Need    | Description                     | Decay Rate (per cycle) | Satisfaction Source              |
| -------- | ------- | ------------------------------- | ---------------------- | -------------------------------- |
| 1        | Food    | Nutrition and sustenance        | -20 per cycle          | Eating (quality matters)         |
| 2        | Shelter | Protection from elements        | -10 per cycle          | Assigned housing quality         |
| 3        | Safety  | Freedom from threats            | -5 per cycle           | Settlement defenses, guard ratio |
| 4        | Social  | Interaction with other NPCs     | -15 per cycle          | Social activity during rest      |
| 5        | Purpose | Meaningful work or contribution | -10 per cycle          | Assigned role, skill use         |

A "cycle" is one in-game day, batch-calculated at check-in.

### 1.2 Need Value Range

Each need is tracked as an integer from 0 to 100:

```
NEED STATES:
  Critical (0-19)    : NPC drops all non-survival activity
  Low (20-39)        : NPC prioritizes this need over discretionary time
  Moderate (40-69)   : Normal operation, mild performance penalty
  Satisfied (70-100) : Full performance, NPC pursues goals freely
```

### 1.3 Need Decay and Recovery

Need values decay each cycle and recover based on available resources:

```
DECAY CALCULATION (per elapsed cycle):
  New Value = Previous Value - Decay Rate

RECOVERY CALCULATION (per elapsed cycle):
  Food Recovery     = Food Quality Tier × 25 (if food available, else 0)
  Shelter Recovery  = Housing Quality Tier × 15
  Safety Recovery   = min(100, Settlement Defense Score)
  Social Recovery   = Social Activity Points earned during rest block
  Purpose Recovery  = Role Satisfaction Score (see Section 1.4)

NET CHANGE = Recovery - Decay (clamped to 0-100)
```

### 1.4 Role Satisfaction

Purpose need recovery depends on whether the NPC's assigned role matches their aptitude:

| Match Quality    | Purpose Recovery per Cycle | Condition                               |
| ---------------- | -------------------------- | --------------------------------------- |
| Ideal match      | +30                        | Top skill matches role requirement      |
| Good match       | +20                        | Relevant skill in top 3                 |
| Acceptable match | +10                        | Has minimum skill for role              |
| Poor match       | +5                         | Assigned to role with no relevant skill |
| Unassigned       | +0                         | No role assigned                        |

See `personality-traits-gdd.md` for how personality modifies role preferences.

---

## 2. Need-to-Performance Multipliers

### 2.1 Performance Modifier Formula

Each need contributes a multiplier to NPC performance. The final modifier is the product of all individual need multipliers:

```
INDIVIDUAL NEED MULTIPLIER:
  Critical (0-19)  = 0.40
  Low (20-39)      = 0.65
  Moderate (40-69) = 0.85
  Satisfied (70-100) = 1.00

FINAL PERFORMANCE MODIFIER:
  = Food Mult × Shelter Mult × Safety Mult × Social Mult × Purpose Mult

EXAMPLE (well-cared-for NPC):
  Food=85 (1.00) × Shelter=75 (1.00) × Safety=90 (1.00) × Social=50 (0.85) × Purpose=80 (1.00)
  = 0.85 (15% penalty from moderate social need)

EXAMPLE (neglected NPC):
  Food=15 (0.40) × Shelter=30 (0.65) × Safety=60 (0.85) × Social=10 (0.40) × Purpose=25 (0.65)
  = 0.057 (94% penalty — NPC is barely functional)
```

### 2.2 Performance Application

The final performance modifier scales these NPC outputs:

| Output               | Effect of Modifier                       |
| -------------------- | ---------------------------------------- |
| Crafting speed       | Timer multiplied by 1/modifier (slower)  |
| Gathering yield      | Output quantity multiplied by modifier   |
| Combat effectiveness | Damage and defense scaled by modifier    |
| XP gain rate         | XP earned multiplied by modifier         |
| Skill training rate  | Training progress multiplied by modifier |

### 2.3 Personality Trait Modifiers

Personality traits (see `personality-traits-gdd.md`) adjust need decay rates, not need multipliers:

| Trait      | Need Affected | Decay Adjustment                  |
| ---------- | ------------- | --------------------------------- |
| Sociable   | Social        | +5 decay (needs more social time) |
| Solitary   | Social        | -5 decay (less social need)       |
| Ambitious  | Purpose       | +5 decay (needs meaningful work)  |
| Content    | Purpose       | -5 decay (satisfied with less)    |
| Gluttonous | Food          | +5 decay (hungrier)               |
| Ascetic    | Food          | -5 decay (needs less food)        |
| Paranoid   | Safety        | +5 decay (needs more security)    |
| Brave      | Safety        | -5 decay (less concerned)         |

---

## 3. Daily Schedule Abstraction

### 3.1 Schedule Blocks

Each in-game day is divided into three blocks. NPCs are assigned to activity categories per block, not per tick:

| Block | Duration | Default Activity | Notes                   |
| ----- | -------- | ---------------- | ----------------------- |
| Work  | 8 hours  | Assigned role    | Primary production time |
| Rest  | 8 hours  | Sleep + recovery | Need recovery occurs    |
| Free  | 8 hours  | NPC chooses      | Autonomous decision     |

### 3.2 Batch Schedule Resolution

At check-in, the system calculates outcomes for all elapsed cycles:

```
FOR EACH ELAPSED CYCLE:
  1. WORK BLOCK:
     - Calculate production output = Base Output × Performance Modifier
     - Grant role XP = Base XP × Performance Modifier
     - Apply food decay (work is hungry business)

  2. REST BLOCK:
     - Apply need recovery (food from eating, shelter from housing, etc.)
     - Process social interactions (if social facilities available)
     - Health recovery if injured

  3. FREE BLOCK:
     - Run autonomous decision (see Section 4)
     - Apply chosen activity outcome

  4. END OF CYCLE:
     - Update all need values (decay + recovery)
     - Check migration threshold (see Section 7)
     - Check skill development (see Section 9)
```

### 3.3 Schedule Modifiers

Certain conditions modify the schedule block allocation:

| Condition           | Schedule Change                      |
| ------------------- | ------------------------------------ |
| Critical food need  | Free block becomes foraging          |
| Critical safety     | Work block becomes guard duty        |
| Injured (below 50%) | Work block becomes rest              |
| Festival day        | Work block becomes social            |
| Emergency event     | All blocks become emergency response |

---

## 4. Autonomous Decisions

### 4.1 Decision Framework

During the Free block, NPCs choose an activity based on their current needs and personality. This is not a real-time AI loop; it is a single weighted random selection per cycle:

```
DECISION ALGORITHM:
  1. Calculate weight for each available activity
  2. Weight = Base Weight × Need Pressure × Personality Modifier
  3. Select activity via weighted random choice
  4. Apply activity outcome to NPC state
```

### 4.2 Available Activities

| Activity   | Base Weight | Need Addressed | Outcome                             |
| ---------- | ----------- | -------------- | ----------------------------------- |
| Train      | 20          | Purpose        | +XP in primary skill, +5 purpose    |
| Socialize  | 20          | Social         | +15 social need, relationship gains |
| Rest       | 15          | Food/Shelter   | +10 food recovery, +10 shelter      |
| Work Extra | 15          | Purpose        | +50% work output, +10 purpose       |
| Explore    | 10          | Purpose        | Small chance of discovery/event     |
| Shop       | 10          | Food/Shelter   | Buy supplies if Gold available      |
| Worship    | 10          | Purpose/Social | +5 purpose, +5 social               |

### 4.3 Need Pressure Multiplier

Activities that address the NPC's lowest need receive a bonus:

```
NEED PRESSURE:
  If activity addresses a Critical need  = 4.0× weight
  If activity addresses a Low need       = 2.5× weight
  If activity addresses a Moderate need  = 1.5× weight
  If activity addresses a Satisfied need = 1.0× weight (no bonus)
```

### 4.4 Personality Influence on Decisions

Personality traits bias activity selection (additive to base weight):

| Personality Trait | Activity Bonus            |
| ----------------- | ------------------------- |
| Industrious       | +15 Work Extra, +10 Train |
| Sociable          | +15 Socialize             |
| Curious           | +15 Explore               |
| Devout            | +15 Worship               |
| Lazy              | +15 Rest, -10 Work Extra  |

See `personality-traits-gdd.md` for full trait definitions.

---

## 5. Level of Detail System

### 5.1 LOD Tiers

NPCs are simulated at different fidelity levels based on their proximity to the player's active settlement:

| LOD Tier | Scope                | Simulation Detail                 | Cost per NPC |
| -------- | -------------------- | --------------------------------- | ------------ |
| Full     | Active settlement    | Per-cycle schedule resolution     | High         |
| Summary  | Adjacent settlements | Daily aggregate (skip Free block) | Medium       |
| Abstract | Distant settlements  | Weekly batch (averages only)      | Low          |
| Dormant  | Unvisited regions    | No simulation, frozen state       | None         |

### 5.2 LOD Simulation Rules

```
FULL (active settlement):
  - All 3 schedule blocks resolved per cycle
  - Individual need tracking
  - Autonomous decisions calculated
  - Personality modifiers applied
  - Events can trigger

SUMMARY (adjacent settlements):
  - Work + Rest blocks only (Free block skipped, needs recover at 75% rate)
  - Needs updated with averaged modifiers
  - No autonomous decisions (NPC defaults to role)
  - Major events still trigger (death, migration, level-up)

ABSTRACT (distant settlements):
  - Weekly batch: NPC produces average output for role
  - Needs clamped to settlement average (no individual tracking)
  - Only migration and death events processed
  - Skill progression at 50% rate

DORMANT (unvisited):
  - No updates until player visits region
  - On first visit: fast-forward using Abstract rules for elapsed time
```

### 5.3 LOD Transition

When a player shifts focus to a different settlement:

| Transition       | Handling                                              |
| ---------------- | ----------------------------------------------------- |
| Dormant to Full  | Fast-forward Abstract simulation, then switch to Full |
| Abstract to Full | Resolve pending weekly batch, switch to Full          |
| Full to Summary  | Snapshot current state, reduce update frequency       |
| Full to Abstract | Snapshot state, aggregate needs to settlement average |

---

## 6. Settlement Satisfaction

### 6.1 Aggregate Satisfaction Score

Settlement satisfaction is the average of all resident NPC satisfactions, calculated at check-in:

```
NPC SATISFACTION:
  = (Food + Shelter + Safety + Social + Purpose) / 5

SETTLEMENT SATISFACTION:
  = Sum of all NPC Satisfactions / NPC Count
  = Integer 0-100

SATISFACTION TIERS:
  Thriving (80-100)  : Attracts migrants, bonus production
  Stable (60-79)     : Normal operation
  Struggling (40-59) : Reduced productivity, occasional departures
  Declining (20-39)  : Regular departures, no new migrants
  Crisis (0-19)      : Mass exodus, facilities degrade
```

### 6.2 Satisfaction Effects

| Tier       | Production Modifier | Migration  | Event Chance         |
| ---------- | ------------------- | ---------- | -------------------- |
| Thriving   | +10%                | +1/week    | Positive events +20% |
| Stable     | Baseline            | Neutral    | Normal               |
| Struggling | -10%                | -1/2 weeks | Negative events +10% |
| Declining  | -25%                | -1/week    | Negative events +30% |
| Crisis     | -50%                | -2/week    | Crisis events +50%   |

### 6.3 Facility Contribution

Settlement facilities directly address NPC needs, improving satisfaction:

| Facility       | Need Addressed | Bonus per Tier                     |
| -------------- | -------------- | ---------------------------------- |
| Farm / Kitchen | Food           | +5 food recovery per facility tier |
| Housing        | Shelter        | +5 shelter recovery per tier       |
| Walls / Guards | Safety         | +5 safety recovery per tier        |
| Tavern / Plaza | Social         | +5 social recovery per tier        |
| Workshop       | Purpose        | +5 purpose recovery per tier       |

See `settlement-system-gdd.md` for facility construction and upgrade mechanics.

---

## 7. NPC Migration

### 7.1 Departure Mechanics

Unhappy NPCs may leave the settlement. Migration checks run once per cycle at end-of-day:

```
DEPARTURE CHECK:
  Eligible: NPC Satisfaction < 30 for 3+ consecutive cycles
  Probability: (30 - NPC Satisfaction) × 2  [percent per cycle]

  EXAMPLE:
    NPC Satisfaction = 15
    Departure chance = (30 - 15) × 2 = 30% per cycle

  EXEMPT FROM DEPARTURE:
    - NPCs with family in settlement (departure chance halved)
    - NPCs assigned to critical roles (departure delayed 7 cycles, then normal)
    - NPCs with "Loyal" personality trait (departure chance ×0.5)
```

### 7.2 Attraction Mechanics

Happy settlements attract new NPCs. Attraction checks run weekly:

```
ATTRACTION CHECK:
  Eligible: Settlement Satisfaction >= 60 AND available housing exists
  Attraction Rate: (Settlement Satisfaction - 60) / 10  [NPCs per week, fractional accumulated]

  EXAMPLE:
    Satisfaction = 85
    Attraction rate = (85 - 60) / 10 = 2.5 NPCs per week

  MIGRANT GENERATION:
    - New NPC generated using npc-core-systems-gdd.md generation rules
    - Class weighted toward settlement's unfilled roles
    - Skills at Apprentice level (not fully trained)
    - Arrives with basic equipment
```

### 7.3 Migration Events

| Event           | Trigger                                    | Effect                          |
| --------------- | ------------------------------------------ | ------------------------------- |
| NPC Departs     | Failed departure check                     | NPC removed, satisfaction dip   |
| Migrant Arrives | Passed attraction check                    | New NPC added, needs assignment |
| Refugee Wave    | World event (war, disaster)                | 3-5 NPCs arrive, low skills     |
| Poaching        | Rival settlement satisfaction > yours + 30 | Chance to lose skilled NPC      |

---

## 8. Service NPC Behavior

### 8.1 Service Roles

Service NPCs (shopkeepers, healers, trainers) provide player-facing services on schedules:

| Role       | Service Provided          | Availability       |
| ---------- | ------------------------- | ------------------ |
| Shopkeeper | Buy/sell goods            | Work block only    |
| Healer     | Heal party, cure ailments | Work + Free blocks |
| Trainer    | Train player skills       | Work block only    |
| Innkeeper  | Rest bonus, food purchase | All blocks         |
| Blacksmith | Repair, upgrade equipment | Work block only    |
| Alchemist  | Potion crafting services  | Work block only    |

### 8.2 Service Availability

Service availability depends on the NPC's schedule and satisfaction:

```
SERVICE AVAILABLE:
  = NPC is in correct block (see table above)
  AND NPC Satisfaction >= 20 (Critical NPCs refuse service)
  AND NPC is not injured (below 50% health)

SERVICE QUALITY MODIFIER:
  Satisfaction 70-100 = 1.00 (full service)
  Satisfaction 40-69  = 0.90 (slightly reduced)
  Satisfaction 20-39  = 0.75 (noticeably reduced)
  Below 20            = Unavailable

REDUCED SERVICE EFFECTS:
  Shopkeeper  : Higher prices (+10-25%)
  Healer      : Slower healing (+15-30% timer)
  Trainer     : Reduced XP gain (-10-25%)
  Blacksmith  : Lower repair quality
```

### 8.3 Service NPC Replacement

If a service NPC departs or dies, the settlement must fill the role:

| Replacement Method | Timer       | Condition                        |
| ------------------ | ----------- | -------------------------------- |
| Promote resident   | 1-3 cycles  | Resident has relevant skill      |
| Attract migrant    | 7-14 cycles | Settlement satisfaction >= 60    |
| Player assignment  | Immediate   | Player assigns NPC to role       |
| Vacancy            | Indefinite  | Service unavailable until filled |

---

## 9. NPC Skill Development

### 9.1 Passive Training

NPCs gain skill XP passively through their assigned roles and activities:

```
SKILL XP PER CYCLE:
  Work Block:
    Primary Role Skill XP = Base Role XP × Performance Modifier
    Secondary Skill XP    = Base × 0.25 × Performance Modifier (if role uses 2+ skills)

  Free Block (if Train chosen):
    Chosen Skill XP = 15 × Performance Modifier

  Rest Block:
    No skill XP gained
```

### 9.2 Skill XP by Role

| Role       | Primary Skill | Base XP/Cycle | Secondary Skill    | Base XP/Cycle |
| ---------- | ------------- | ------------- | ------------------ | ------------- |
| Farmer     | Agriculture   | 12            | Herbalism          | 3             |
| Guard      | Combat        | 15            | Athletics          | 4             |
| Blacksmith | Smithing      | 18            | Mining (knowledge) | 4             |
| Merchant   | Trading       | 14            | Appraisal          | 3             |
| Healer     | Medicine      | 16            | Herbalism          | 4             |

### 9.3 Offline Skill Development

Skill development continues during offline time using the same batch calculation:

```
OFFLINE SKILL CALCULATION:
  Elapsed Cycles = Floor(Offline Minutes / Cycle Duration in Minutes)
  Total Skill XP = Elapsed Cycles × (Skill XP per Cycle)

  Note: Performance Modifier is snapshot at last check-in and held constant
  for the entire offline period (no need recalculation mid-offline).

  SKILL LEVEL-UP:
    Processed at check-in
    Notification queued for player
    New abilities unlocked immediately
```

### 9.4 Skill Progression Diminishing Returns

To prevent runaway NPC power during long offline periods:

```
DIMINISHING RETURNS:
  Cycles 1-24 (first day)  : 100% XP rate
  Cycles 25-72 (days 2-3)  : 75% XP rate
  Cycles 73-168 (days 4-7) : 50% XP rate
  Cycles 169+ (week+)      : 25% XP rate
```

---

## 10. Offline NPC Simulation

### 10.1 Check-in Batch Processing

When the player opens the game, the system processes all elapsed time:

```
CHECK-IN SEQUENCE:
  1. Calculate elapsed cycles since last check-in
  2. Apply diminishing returns scaling (Section 9.4)
  3. FOR EACH settlement (by LOD tier):
     a. Process cycles at appropriate detail level (Section 5.2)
     b. Update NPC need values
     c. Calculate production outputs
     d. Process skill development
     e. Run migration checks
     f. Queue events and notifications
  4. Present summary to player
```

### 10.2 Check-in Summary

The check-in report presents:

| Category     | Information Shown                               |
| ------------ | ----------------------------------------------- |
| Time elapsed | Total offline duration and cycles processed     |
| Production   | Resources gathered, items crafted, goods traded |
| NPC changes  | Level-ups, departures, arrivals, injuries       |
| Satisfaction | Settlement satisfaction trend (up/down/stable)  |
| Needs alerts | NPCs with Critical needs requiring attention    |
| Events       | Notable events that occurred during offline     |

### 10.3 Offline Caps

To prevent extreme state drift during long absences:

| Mechanic          | Offline Cap                              |
| ----------------- | ---------------------------------------- |
| Cycle processing  | 168 cycles (7 days) maximum              |
| Need decay        | Needs cannot drop below 5 offline        |
| Production output | Capped at 7 days of storage capacity     |
| Skill XP          | Diminishing returns (Section 9.4)        |
| Migration         | Maximum 3 departures, 5 arrivals per cap |

Beyond the cap, the game displays "Your settlement has been maintaining itself" and applies no further changes.

---

## 11. Cross-References

| System Document                  | Relationship to NPC Simulation                                                                                   |
| -------------------------------- | ---------------------------------------------------------------------------------------------------------------- |
| `npc-core-systems-gdd.md`        | Goals, conflict resolution, failure states (complementary — this doc covers daily life, that doc covers goal AI) |
| `npc-design.md`                  | NPC generation, attributes, classes, relationships (foundational — simulation operates on these entities)        |
| `personality-traits-gdd.md`      | Trait definitions that modify need decay, decision weights, and migration thresholds                             |
| `settlement-system-gdd.md`       | Facility tiers, housing, defenses that feed need recovery; settlement-level mechanics                            |
| `crafting-system-gdd.md`         | Crafting queue timers scaled by NPC performance modifier                                                         |
| `gathering-system-gdd.md`        | Gathering yields scaled by NPC performance modifier                                                              |
| `economy-system-gdd.md`          | Trade and pricing affected by service NPC availability                                                           |
| `combat-system-gdd.md`           | Combat effectiveness scaled by NPC performance modifier                                                          |
| `core-progression-system-gdd.md` | XP formulas used by skill development calculations                                                               |
| `skill-recipe-system-gdd.md`     | Skill definitions referenced by role assignment and training                                                     |
