---
type: system
scope: detailed
status: authoritative
version: 1.0.0
created: 2026-02-08
updated: 2026-02-08
subjects: [npc, simulation, occupation, satisfaction, state-machine, batch, idle]
dependencies: []
---

# NPC Simulation System - Authoritative Game Design

## Executive Summary

The NPC Simulation System governs how non-player characters behave, progress, and contribute to the settlement economy. Full tick-by-tick NPC simulation from the predecessor is replaced with a simplified state machine (working/idle/assigned/resting) and batch processing. NPC satisfaction is a single score calculated from their assigned role quality, and social behaviour is expressed as flavour text events rather than simulated interactions.

**This document resolves:**

- NPC state machine (4 states with transitions)
- Satisfaction scoring from assigned role quality
- Occupation system with NPC levelling through assigned tasks
- Motivation tiers as NPC classification (Everyday, Ambitious, Idealistic)
- Batch processing for all NPC updates
- Self-preservation as health threshold triggering auto-retreat
- NPC XP acquisition through task assignment

**Design Philosophy:** NPCs are strategic resources that players assign to tasks. Their value comes from accumulated skills, personality traits, and occupation levels. The simulation is batch-processed at check-in rather than tick-by-tick, making it offline-friendly. Social behaviour and daily schedules are abstracted away in favour of role-based output calculations. Players interact with NPCs through assignment decisions, not moment-to-moment management.

---

## 1. NPC State Machine

### 1.1 States

Every NPC is in exactly one of four states:

| State    | Description                            | Output       |
| -------- | -------------------------------------- | ------------ |
| Working  | Assigned to a task, producing output   | Active       |
| Idle     | No assignment, available for task      | None         |
| Assigned | Travelling to or preparing for task    | None (setup) |
| Resting  | Recovering health/stamina, unavailable | None         |

### 1.2 State Transitions

```
IDLE
  |-- Player assigns task --> ASSIGNED
  |-- Health drops below threshold --> RESTING

ASSIGNED
  |-- Setup timer completes --> WORKING
  |-- Task cancelled --> IDLE

WORKING
  |-- Task completes --> IDLE (results collected)
  |-- Task cancelled --> IDLE
  |-- Health drops below threshold --> RESTING
  |-- Stamina depleted --> RESTING

RESTING
  |-- Health/stamina restored --> IDLE
```

### 1.3 State Durations

| Transition     | Timer                        |
| -------------- | ---------------------------- |
| Assigned setup | 5-15 min (task-dependent)    |
| Resting        | 30-60 min (damage-dependent) |
| Working        | Task-dependent               |

---

## 2. Satisfaction System

### 2.1 Single Satisfaction Score

Each NPC has a single satisfaction score (0-100) calculated from their current assignment:

```
Satisfaction = Base(50) + Role Match Bonus + Trait Alignment + Facility Quality + Social Modifier

Role Match Bonus:
  Assigned to trained occupation = +20
  Assigned to related occupation = +10
  Assigned to unrelated task     = -10
  Idle (no assignment)           = -5

Trait Alignment:
  Task matches personality traits = +5 to +15
  Task conflicts with traits      = -5 to -15
  (See personality-traits-gdd.md for trait modifiers)

Facility Quality:
  Master workspace = +10
  Advanced workspace = +5
  Basic workspace = 0
  No workspace = -10

Social Modifier:
  Friends in same settlement = +5
  No social connections = -5
```

### 2.2 Satisfaction Effects

| Satisfaction | Effect on Output  | Morale Contribution | Departure Risk |
| ------------ | ----------------- | ------------------- | -------------- |
| 0-20         | -30% productivity | -20% to settlement  | High (10%/day) |
| 21-40        | -15% productivity | -10% to settlement  | Low (2%/day)   |
| 41-60        | No modifier       | No modifier         | None           |
| 61-80        | +10% productivity | +5% to settlement   | None           |
| 81-100       | +20% productivity | +10% to settlement  | None           |

### 2.3 Departure Mechanic

NPCs with very low satisfaction may leave the settlement:

- **Daily check**: If satisfaction below 20, roll departure chance
- **Warning**: Player receives notification when NPC satisfaction drops below 30
- **Prevention**: Reassign to better role, upgrade facilities, or dismiss manually

---

## 3. Motivation Tiers

NPCs are classified into three motivation categories at generation. This affects their satisfaction weighting and available occupation preferences.

### 3.1 Motivation Categories

| Type       | Population | Core Drive                  | Satisfaction Emphasis      |
| ---------- | ---------- | --------------------------- | -------------------------- |
| Everyday   | ~85%       | Comfortable, stable life    | Facility quality, social   |
| Ambitious  | ~12%       | Status, wealth, recognition | Role prestige, output      |
| Idealistic | ~3%        | Meaning, purpose, cause     | Task significance, variety |

### 3.2 Motivation Effects on Satisfaction

| Factor             | Everyday Weight | Ambitious Weight | Idealistic Weight |
| ------------------ | --------------- | ---------------- | ----------------- |
| Facility quality   | High (+15)      | Low (+5)         | Very Low (+2)     |
| Social connections | High (+10)      | Medium (+5)      | Low (+3)          |
| Role prestige      | Low (+3)        | Very High (+15)  | Low (+3)          |
| Task variety       | Low (+3)        | Medium (+5)      | High (+15)        |
| Output volume      | Medium (+5)     | High (+10)       | Low (+3)          |

### 3.3 Motivation as UI Label

Motivation tier is displayed on the NPC detail panel as a classification label. It helps players understand what assignment will keep an NPC satisfied.

---

## 4. Occupation System

### 4.1 Occupation Categories

| Category  | Examples                                 |
| --------- | ---------------------------------------- |
| Primary   | Farmer, Fisher, Hunter, Miner, Logger    |
| Craft     | Blacksmith, Tailor, Carpenter, Alchemist |
| Service   | Merchant, Innkeeper, Healer, Guard       |
| Knowledge | Scribe, Teacher, Priest, Sage            |

### 4.2 Occupation Progression

NPCs level up their occupation by performing assigned tasks:

```
Apprentice -> Journeyman -> Master -> Renowned

XP per task completion:
  Basic task      = 10-20 XP
  Standard task   = 20-40 XP
  Complex task    = 40-80 XP
  Masterwork task = 80-150 XP

Rank thresholds:
  Apprentice  = 0 XP
  Journeyman  = 500 XP
  Master      = 2,500 XP
  Renowned    = 10,000 XP
```

### 4.3 Occupation Level Effects

| Rank       | Output Quality | Speed Bonus | Queue Slots | Special               |
| ---------- | -------------- | ----------- | ----------- | --------------------- |
| Apprentice | Standard       | None        | 1           | Can fail tasks        |
| Journeyman | Superior       | +10%        | 2           | Reduced failure       |
| Master     | Masterwork     | +20%        | 3           | Can train apprentices |
| Renowned   | Legendary      | +30%        | 4           | Settlement prestige   |

### 4.4 NPC XP Acquisition

NPCs gain XP from their assigned tasks using the same progression formulas as player-controlled characters. XP is calculated in batch at check-in.

| Assignment Status | XP Calculation                       |
| ----------------- | ------------------------------------ |
| Working           | XP based on task type and completion |
| Training          | Passive XP at 50% rate               |
| Idle              | No XP                                |
| Resting           | No XP                                |

---

## 5. Self-Preservation

### 5.1 Health Threshold

NPCs automatically transition to RESTING state when health drops below a threshold. The threshold varies by personality:

| Courage Range | Health Threshold | Behaviour           |
| ------------- | ---------------- | ------------------- |
| 0-15          | 40% HP           | Very cautious       |
| 16-30         | 30% HP           | Cautious            |
| 31-55         | 25% HP           | Standard            |
| 56-85         | 20% HP           | Push through        |
| 86-100        | 15% HP           | Fight to near-death |

### 5.2 Combat Auto-Retreat

During combat expeditions, NPCs auto-retreat when their health drops below their personal threshold:

- NPC drops below threshold during combat
- NPC retreats from current combat encounter
- NPC enters RESTING state on return to settlement
- Rest timer begins (recovery time based on health lost)

### 5.3 Override

Party-level retreat settings can override individual thresholds (see death-mechanics-gdd.md Section 2.2). Party retreat always takes priority over individual self-preservation.

---

## 6. Social Behaviour

### 6.1 Flavour Text Events

Social behaviour is expressed as flavour text notifications rather than simulated interactions:

| Event Type | Trigger                          | Display                               |
| ---------- | -------------------------------- | ------------------------------------- |
| Friendship | Two NPCs assigned together often | "[NPC] and [NPC] have become friends" |
| Rivalry    | Ambitious NPCs in same role      | "[NPC] and [NPC] are competing"       |
| Romance    | High sociability + time together | "[NPC] and [NPC] are courting"        |
| Mentorship | Master + Apprentice assignment   | "[NPC] is mentoring [NPC]"            |
| Complaint  | Low satisfaction NPC             | "[NPC] is unhappy with their role"    |

### 6.2 Social Modifier Calculation

Social connections affect satisfaction score:

| Connection        | Satisfaction Modifier |
| ----------------- | --------------------- |
| 3+ friends nearby | +10                   |
| 1-2 friends       | +5                    |
| No connections    | -5                    |
| Rival nearby      | -5                    |
| Romantic partner  | +10                   |

### 6.3 Relationship Formation

Relationships form automatically based on assignment proximity:

```
Two NPCs assigned to same area for 3+ days -> Acquaintance
Acquaintance + 7+ days -> Friend (if compatible traits)
Friend + 14+ days -> Close Friend (sociability-dependent)
```

---

## 7. Decision Making

### 7.1 Automated Decisions

NPC decision-making is fully automated based on assigned role:

| Decision Point  | Resolution                           |
| --------------- | ------------------------------------ |
| What to work on | Determined by assigned task          |
| When to rest    | Health/stamina threshold (automatic) |
| When to flee    | Courage-based threshold (automatic)  |
| What to produce | Recipe queue set by player           |
| Where to go     | Task location determines movement    |

### 7.2 Player-Controlled Decisions

Players make all strategic decisions for NPCs:

| Decision             | Player Action                      |
| -------------------- | ---------------------------------- |
| Task assignment      | Select task from available options |
| Expedition inclusion | Add to party for expedition        |
| Equipment            | Assign equipment from inventory    |
| Training             | Assign to training queue           |
| Dismissal            | Remove NPC from settlement         |

---

## 8. Batch Processing

### 8.1 All NPCs Use Batch Processing

There is no tick-by-tick simulation. All NPC updates are calculated in batch at check-in:

```
On check-in:
  1. Calculate elapsed real time since last check-in
  2. For each NPC:
     a. Calculate task output based on elapsed time
     b. Calculate XP gained
     c. Check for state transitions (rest, completion)
     d. Calculate satisfaction score
     e. Roll for social events
     f. Roll for departure (if low satisfaction)
  3. Present results to player
```

### 8.2 Offline Behaviour

| System            | Offline Behaviour                       |
| ----------------- | --------------------------------------- |
| Task output       | Calculated at check-in for elapsed time |
| XP accumulation   | Batch calculated                        |
| Satisfaction      | Recalculated at check-in                |
| Social events     | Rolled at check-in                      |
| State transitions | Applied at check-in                     |
| Self-preservation | NPCs auto-rest if health would reach 0  |

### 8.3 Long Offline Periods

For extended offline periods, batch processing uses statistical averages:

| Offline Duration | Processing Method                    |
| ---------------- | ------------------------------------ |
| 0-2 hours        | Exact calculation per task cycle     |
| 2-12 hours       | Average output per cycle x cycles    |
| 12+ hours        | Daily average output x days (capped) |

---

## 9. Life Events

### 9.1 Random Life Events

Random events occur during batch processing to add variety:

| Event Category | Examples                         | Chance per Day |
| -------------- | -------------------------------- | -------------- |
| Social         | New friendship, rivalry, romance | 5%             |
| Work           | Breakthrough, setback, discovery | 3%             |
| Health         | Minor illness, injury recovery   | 2%             |
| Personal       | Mood shift, preference change    | 1%             |

### 9.2 Event Effects

| Event            | Mechanical Effect             |
| ---------------- | ----------------------------- |
| Breakthrough     | Bonus XP, +10 satisfaction    |
| Work setback     | Lost output, -5 satisfaction  |
| New friendship   | +5 social modifier            |
| Illness          | Forced rest for 2-4 hours     |
| Mood improvement | +10 satisfaction for 24 hours |

---

## 10. Idle Integration

### 10.1 Check-in Collection

On player check-in:

- NPC task output summaries
- XP gains and level-up notifications
- Social event notifications
- Satisfaction warnings
- State transition summaries

### 10.2 Progression Hooks

| Hook               | System Integration                        |
| ------------------ | ----------------------------------------- |
| Settlement system  | NPCs fill roles, contribute to economy    |
| Crafting system    | Artisan NPCs produce crafted goods        |
| Combat system      | NPCs follow same combat rules as player   |
| Economy system     | NPC output feeds trade and settlement     |
| Expedition system  | NPCs join parties for expeditions         |
| Personality traits | Traits affect satisfaction and output     |
| Class system       | NPCs have and level classes through tasks |
