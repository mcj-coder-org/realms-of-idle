---
type: system
scope: detailed
status: authoritative
version: 1.0.0
created: 2026-02-08
updated: 2026-02-08
subjects: [party, companions, loyalty, stances, formation, xp-sharing, idle]
dependencies: []
---

# Party Mechanics - Authoritative Game Design

## Executive Summary

The Party Mechanics system governs how characters form groups, coordinate in combat, and progress together through timer-based idle gameplay. Players recruit companions, set party-wide stances and orders, and assign formations that execute automatically. Loyalty operates as a simple multiplier affecting companion effectiveness, and XP sharing models determine how the party grows over time. Offline, the party continues activity through exploration, gathering, and training queues.

**This document resolves:**

- Party composition limits and slot unlocking
- Recruitment flow and timer-based joining conditions
- Loyalty as a modifier-based multiplier system
- Party stances and their combat modifiers
- XP sharing models and configuration
- Formation and row-based positioning
- Party orders and AI behavior presets
- Companion skills and synergy bonuses
- Offline party activity queues

**Design Philosophy:** Party management is a strategic selection system. Players choose who to recruit, how to position them, and what stance to adopt, then the system executes automatically. Deeper investment (higher loyalty, better synergies, upgraded formations) yields stronger outcomes. Offline progress ensures the party is always active between check-ins.

---

## 1. Party Composition

### 1.1 Party Size

Party size is gated by progression milestones, unlocking additional slots as the player advances:

| Milestone        | Max Party Size | Unlock Condition                  |
| ---------------- | -------------- | --------------------------------- |
| Starting         | 2              | Game start (player + 1 companion) |
| First Settlement | 3              | Establish a settlement            |
| Barracks Built   | 4              | Build barracks in settlement      |
| Commander Rank   | 5              | Reach Commander class rank        |
| War Council      | 6              | Build War Council structure       |

### 1.2 Party Roles

Each companion occupies a role that determines their default formation position and AI behavior:

| Role      | Default Row | Primary Function         | Example Classes    |
| --------- | ----------- | ------------------------ | ------------------ |
| Tank      | Front       | Absorb damage, hold line | Knight, Guardian   |
| Melee DPS | Front       | Deal close-range damage  | Berserker, Duelist |
| Ranged    | Back        | Deal damage from safety  | Archer, Mage       |
| Support   | Back        | Heal, buff, utility      | Cleric, Bard       |
| Hybrid    | Either      | Flexible, fills gaps     | Paladin, Ranger    |

### 1.3 Role Composition Bonuses

Balanced parties receive passive bonuses calculated at party formation:

| Composition | Condition                      | Bonus                   |
| ----------- | ------------------------------ | ----------------------- |
| Balanced    | At least 1 Tank + 1 Support    | +5% party survivability |
| Assault     | 3+ DPS roles (melee or ranged) | +10% party damage       |
| Fortress    | 2+ Tanks                       | +15% party defence      |
| Versatile   | All 4 base roles represented   | +5% XP gain             |

---

## 2. Recruitment and Joining

### 2.1 Recruitment Flow

Recruitment uses a timer-based relationship system. Players select a candidate, commit resources, and start a recruitment timer that resolves at check-in:

```
DISCOVER POTENTIAL COMPANION (tavern, quest, field encounter)
    |
    v
ASSIGN TO RECRUITMENT QUEUE (pay cost, timer starts)
    |
    v
RECRUITMENT TIMER RUNS (real-time, continues offline)
    |
    v
TIMER COMPLETES -> RECRUITMENT CHECK (batch-processed at check-in)
    |
    v
SUCCESS: Companion joins party at starting loyalty
FAILURE: Relationship resets to 50% progress, can retry
```

### 2.2 Recruitment Sources

| Source           | Base Timer | Success Rate | Starting Loyalty | Gold Cost           |
| ---------------- | ---------- | ------------ | ---------------- | ------------------- |
| Tavern encounter | 30 min     | 70%          | Neutral (40)     | 50 x candidate lvl  |
| Quest reward     | Instant    | 100%         | Friendly (60)    | Free                |
| Rescued NPC      | 15 min     | 85%          | Friendly (60)    | Free                |
| Hired mercenary  | 5 min      | 95%          | Reluctant (20)   | 100 x candidate lvl |
| Defeated enemy   | 2 hours    | 30%          | Hostile (10)     | Free                |

### 2.3 Recruitment Modifiers

Base success rate is modified before the recruitment check:

```
Effective Rate = Base Rate + Charisma Bonus + Reputation Bonus + Gift Bonus

Charisma Bonus:     +2% per point above 10 CHA
Reputation Bonus:   +1% per 10 reputation with companion's faction
Gift Bonus:         +5% per gift given during recruitment timer (max 3 gifts)

Final Rate = clamp(Effective Rate, 5%, 99%)
```

### 2.4 Dismissal and Departure

Companions leave through player choice or loyalty collapse:

```
DISMISSAL:
  Voluntary:   Instant, companion returns to recruitment pool
  Re-recruit:  Dismissed companions retain 50% of earned loyalty

AUTOMATIC DEPARTURE:
  Loyalty drops below 5:          Companion leaves immediately
  Loyalty 5-15 for 7+ days:      50% daily departure check
  Faction conflict (persistent):  -5 loyalty/day until departure or resolution

PREVENTION:
  Gold bonus:   Spend 100 gold x level to prevent one departure check
  Favour quest: Complete companion quest to lock loyalty above 30
```

---

## 3. Loyalty System

### 3.1 Loyalty Scale

Loyalty is a 0-100 numeric value that acts as a simple multiplier on companion effectiveness. This replaces deep NPC simulation with a single modifier that scales all companion outputs:

| Range  | Label     | Effectiveness Multiplier | Order Compliance | Departure Risk |
| ------ | --------- | ------------------------ | ---------------- | -------------- |
| 0-9    | Hostile   | 0.50x                    | 30%              | Immediate      |
| 10-24  | Reluctant | 0.70x                    | 50%              | 25%/day        |
| 25-49  | Neutral   | 0.85x                    | 70%              | None           |
| 50-74  | Friendly  | 1.00x                    | 85%              | None           |
| 75-89  | Loyal     | 1.10x                    | 95%              | None           |
| 90-100 | Devoted   | 1.20x                    | 100%             | Never          |

### 3.2 Effectiveness Multiplier Application

The loyalty multiplier applies uniformly to all companion outputs:

- **Combat damage output**: Base Damage x Loyalty Multiplier
- **Skill effectiveness**: Healing, buffs, debuffs scaled by multiplier
- **Order compliance rate**: Chance to follow orders = Base Compliance x Loyalty Multiplier
- **Offline activity efficiency**: Gathering/training yields scaled by multiplier

### 3.3 Loyalty Growth

Loyalty changes are accumulated during play and applied as a batch at check-in:

| Event                        | Loyalty Change | Frequency Limit |
| ---------------------------- | -------------- | --------------- |
| Passive time in party        | +1             | Per 24 hours    |
| Survived combat together     | +2             | Per combat      |
| Shared loot fairly           | +1             | Per loot event  |
| Protected in combat (low HP) | +3             | Per combat      |
| Completed companion quest    | +10            | Per quest       |
| Rested at settlement         | +1             | Once per day    |
| Gift given (matching taste)  | +3             | 1 per day       |
| Gift given (generic)         | +1             | 1 per day       |
| Same-faction alignment       | +0.5           | Per day         |
| Leader charisma bonus        | +(CHA / 20)    | Per day         |

### 3.4 Loyalty Decay

| Event                           | Loyalty Change | Notes                              |
| ------------------------------- | -------------- | ---------------------------------- |
| Companion knocked out (no heal) | -3             | Per combat                         |
| Unfair loot distribution        | -2             | Hoarding triggers this             |
| Left idle with no task          | -1             | Per 24 hours with no activity      |
| Party defeat                    | -3             | Per loss                           |
| Ally killed in combat           | -5             | Per death                          |
| Conflicting faction action      | -5             | Acting against companion's faction |
| Faction conflict in party       | -1             | Per day while unresolved           |
| Dismissed then re-recruited     | -25            | Applied to stored loyalty          |

### 3.5 Personality Modifier

Each companion has a personality tag that modifies loyalty gain and decay rates. This is the extent of personality simulation -- a pair of simple multipliers:

| Personality | Gain Modifier | Decay Modifier | Notes                             |
| ----------- | ------------- | -------------- | --------------------------------- |
| Steadfast   | 0.8x          | 0.5x           | Slow to warm, slow to leave       |
| Eager       | 1.5x          | 1.5x           | Quick bonds, quick grudges        |
| Mercenary   | 0.5x          | 0.8x           | Loyalty from gold, not deeds      |
| Stoic       | 1.0x          | 0.7x           | Baseline gain, resistant to decay |

### 3.6 Loyalty Threshold Unlocks

Beyond the effectiveness multiplier, loyalty milestones unlock capabilities:

```
LOYALTY THRESHOLDS:

  Loyalty 30+ (Neutral):
    Member shares personal quest information
    Can be assigned to training slots

  Loyalty 50+ (Friendly):
    +5% XP sharing efficiency bonus
    Unlocks Guard order for this member
    Shares faction reputation benefits

  Loyalty 70+ (Loyal):
    +10% XP sharing efficiency bonus
    Will not flee from combat
    Can be assigned as party second-in-command

  Loyalty 90+ (Devoted):
    +15% XP sharing efficiency bonus
    Loyalty no longer decays
    Unique synergy bonus with party leader
```

---

## 4. Party Stances

### 4.1 Stance Types

Party stance is a single global setting that modifies all party members during auto-resolved combat:

| Stance     | Damage Modifier | Defence Modifier | Speed Modifier | Retreat Threshold |
| ---------- | --------------- | ---------------- | -------------- | ----------------- |
| Aggressive | +20%            | -15%             | +10%           | 10% HP            |
| Balanced   | 0%              | 0%               | 0%             | 25% HP            |
| Defensive  | -15%            | +20%             | -10%           | 40% HP            |
| Cautious   | -25%            | +30%             | -20%           | 60% HP            |

### 4.2 Stance Selection

- **Default stance**: Balanced (applied at party creation)
- **Changing stance**: Instant, no timer, no cost
- **Stance persists**: Remains set until player changes it (survives offline sessions)
- **Per-party, not per-member**: All companions use the same stance

### 4.3 Stance Interaction with Combat Resolution

Stance modifiers feed directly into the combat resolution pipeline (see combat-system-gdd.md):

```
Effective Attack  = Base Attack  x Stance Damage Modifier  x Loyalty Multiplier
Effective Defence = Base Defence x Stance Defence Modifier x Loyalty Multiplier
Effective Speed   = Base Speed   x Stance Speed Modifier
```

### 4.4 Retreat Behavior

When a companion's HP drops below the stance's retreat threshold:

- **Aggressive**: Fights to near-death, retreats only at 10% HP
- **Balanced**: Standard retreat at 25% HP, repositions to back row
- **Defensive**: Early retreat at 40% HP, prioritizes survival
- **Cautious**: Very early retreat at 60% HP; entire party withdraws if 50%+ members are retreating

---

## 5. XP Sharing Models

### 5.1 Available Models

Players select one XP sharing model for the entire party. The model determines how XP from combat, quests, and activities is distributed across members:

| Model                 | Distribution Method                     | Best For               |
| --------------------- | --------------------------------------- | ---------------------- |
| Equal Split           | Total XP / party size, same for each    | Even leveling          |
| Contribution-Weighted | XP proportional to damage/healing done  | Rewarding active roles |
| Proximity-Based       | Full XP to participants, 50% to reserve | Rotating roster        |

### 5.2 Equal Split

```
Member XP = Total XP Earned / Active Party Size

Example:
  Combat yields 1000 XP, party of 4
  Each member receives 250 XP
```

- Simplest model, ensures no member falls behind
- Idle-friendly: minimal calculation complexity at check-in

### 5.3 Contribution-Weighted

```
Member XP = Total XP x (Member Contribution / Total Contribution)

Contribution Score:
  Damage dealt     = 1 point per damage
  Damage healed    = 1 point per healing
  Damage absorbed  = 0.5 points per damage taken (tanks)
  Buffs applied    = 10 points per buff cast
  Minimum share    = 10% of equal split (prevents zero-XP)

Example:
  Combat yields 1000 XP, party of 4
  Tank contribution: 30%  -> 300 XP
  DPS contribution:  45%  -> 450 XP
  Healer:            15%  -> 150 XP
  Support:           10%  -> 100 XP
```

- Rewards specialists, but can leave support roles behind
- Minimum share floor prevents companions from stalling

### 5.4 Proximity-Based

```
Active participants    = 100% of their share
Reserve/offline members = 50% of equal share

Example:
  Combat yields 1000 XP, party of 4 (3 active, 1 reserve)
  Active share: 1000 / 3 = 333 XP each
  Reserve share: (1000 / 4) x 0.50 = 125 XP
```

- Useful for rotating companions through the active roster
- Reserve members still progress, at a reduced rate

### 5.5 Bonus XP Modifiers

Additional XP modifiers stack multiplicatively with the sharing model:

| Source            | XP Modifier | Condition                           |
| ----------------- | ----------- | ----------------------------------- |
| Loyalty (Devoted) | +10%        | Loyalty 90+                         |
| Versatile comp.   | +5%         | All 4 base roles in party           |
| Training stance   | +15%        | Party set to training (no combat)   |
| Mentor companion  | +10%        | Companion with Mentor trait present |
| Rested bonus      | +5%         | Party rested at settlement recently |

---

## 6. Formation and Positioning

### 6.1 Row System

Formation uses a two-row system: Front and Back. Each row holds up to 3 companions.

```
FRONT ROW                    BACK ROW
 +-------------+             +-------------+
 | Slot 1      |             | Slot 4      |
 | (Tank)      |             | (Ranged)    |
 +-------------+             +-------------+
 | Slot 2      |             | Slot 5      |
 | (Melee DPS) |             | (Support)   |
 +-------------+             +-------------+
 | Slot 3      |             | Slot 6      |
 | (Hybrid)    |             | (Ranged)    |
 +-------------+             +-------------+
       ^                           ^
  Takes direct hits          Protected from melee
  Melee attack range         Ranged attack range
```

### 6.2 Row Mechanics

| Aspect             | Front Row                        | Back Row                                         |
| ------------------ | -------------------------------- | ------------------------------------------------ |
| Melee targeting    | Can be targeted by melee attacks | Cannot be targeted while front row holds         |
| Ranged targeting   | Can be targeted                  | Can be targeted                                  |
| Melee attacks      | Can perform melee attacks        | Cannot perform melee attacks                     |
| Ranged attacks     | Can perform ranged attacks       | Can perform ranged attacks                       |
| Defence bonus      | None                             | +10% damage reduction                            |
| Front row collapse | N/A                              | If front row empties, back row becomes front row |

### 6.3 Auto-Assignment

By default, the system auto-assigns formation based on companion role:

```
AUTO-ASSIGN RULES:
  1. Tanks      -> Front Row (highest priority)
  2. Melee DPS  -> Front Row
  3. Support    -> Back Row (highest priority)
  4. Ranged     -> Back Row
  5. Hybrid     -> Fill whichever row has fewer members

  Overflow: If a row is full (3 slots), excess placed in the other row
```

### 6.4 Manual Override

Players can manually assign companions to specific slots:

- **Override persists** until the player changes it or the companion leaves
- **Invalid placement warning**: System warns but allows (e.g., healer in front row)
- **New recruits**: Auto-assigned to best available slot based on role

---

## 7. Party Orders

### 7.1 Order Types

Orders are party-wide commands issued at check-in that direct AI behavior until changed:

| Order   | Effect                                            | Duration      |
| ------- | ------------------------------------------------- | ------------- |
| Attack  | Party seeks and engages enemies in current area   | Until changed |
| Defend  | Party holds position, fights only if attacked     | Until changed |
| Retreat | Party withdraws from current area, avoids combat  | One-time      |
| Hold    | Party stays in place, engages nearby enemies only | Until changed |
| Explore | Party explores current area, fights if necessary  | Until changed |
| Train   | Party trains skills, no combat (see Section 10)   | Until changed |

### 7.2 Order Compliance

Order compliance depends on loyalty. Non-compliant companions fall back to default AI behavior:

```
Compliance Rate = Base Rate x Loyalty Multiplier

Base Rates by Order:
  Attack  = 90% (most companions willing to fight)
  Defend  = 95% (natural self-preservation)
  Retreat = 80% (aggressive personalities resist)
  Hold    = 85% (impatient personalities resist)
  Explore = 95% (generally welcomed)
  Train   = 90% (lazy personalities resist)

Failed Compliance:
  Companion acts on AI default behavior instead
  No penalty applied, just less predictable outcomes
```

### 7.3 Order and Stance Combinations

Orders and stances combine to define behavior during auto-resolved encounters:

| Order + Stance      | Resulting Behavior                                |
| ------------------- | ------------------------------------------------- |
| Attack + Aggressive | Maximum offense, pursues fleeing enemies          |
| Attack + Cautious   | Engages but retreats early, avoids strong enemies |
| Defend + Defensive  | Maximum survivability, holds position firmly      |
| Defend + Aggressive | Counterattacks hard when engaged                  |
| Explore + Balanced  | Standard exploration, fights when needed          |
| Explore + Cautious  | Avoids dangerous areas, retreats from strong foes |

---

## 8. AI Behavior Presets

### 8.1 Preset System

Since combat is auto-managed in idle mode, AI behavior presets control how each companion acts without player input:

| Preset        | Targeting Priority  | Ability Usage   | Positioning         |
| ------------- | ------------------- | --------------- | ------------------- |
| Auto-Optimal  | Weakest enemy       | Cooldown-based  | Role-appropriate    |
| Focus Fire    | Same target as lead | Burst priority  | Clustered           |
| Spread Damage | Distribute evenly   | AoE priority    | Spread              |
| Protect Weak  | Threats to allies   | Defensive first | Near lowest HP ally |
| Conserve      | Only when attacked  | Minimal usage   | Safe positioning    |

### 8.2 Preset Selection

- **Default preset**: Auto-Optimal (applied to all new companions)
- **Per-companion**: Each companion can have a different preset
- **Persistence**: Presets persist across sessions and offline activity
- **Preset vs. stance**: Preset controls targeting and ability use; stance controls stat modifiers

### 8.3 Ability Priority Queues

Each preset defines an ability priority queue for automated combat:

```
AUTO-OPTIMAL QUEUE:
  1. Use healing if any ally below 30% HP (Support role)
  2. Use buff if not active on primary target (Support role)
  3. Use strongest available damage ability (DPS role)
  4. Use taunt if not active (Tank role)
  5. Use basic attack as fallback

CONSERVE QUEUE:
  1. Use healing if any ally below 15% HP
  2. Use basic attack only
  3. Avoid abilities with cooldowns > 60 seconds
```

---

## 9. Companion Skills and Synergies

### 9.1 Companion Skill Slots

Each companion has a limited number of active and passive skill slots that grow with level:

| Companion Level | Active Skill Slots | Passive Slots |
| --------------- | ------------------ | ------------- |
| 1-5             | 2                  | 1             |
| 6-10            | 3                  | 1             |
| 11-15           | 3                  | 2             |
| 16-20           | 4                  | 2             |
| 21+             | 4                  | 3             |

### 9.2 Party-Wide Passive Skills

Certain skills provide bonuses to the entire party. These activate automatically when the skill holder is present:

| Skill              | Effect                           | Tier Required | Class Source        |
| ------------------ | -------------------------------- | ------------- | ------------------- |
| Inspiring Presence | +10% morale, +5% loyalty/day     | Journeyman    | Leader, Bard        |
| Tactical Command   | +10% party combat efficiency     | Journeyman    | Warrior, Strategist |
| Shared Wisdom      | +15% party XP sharing rate       | Apprentice    | Scholar, Mentor     |
| Group Fortitude    | +10% party HP, +5% stamina regen | Apprentice    | Cleric, Warden      |
| Scout Ahead        | -15% ambush chance for party     | Novice        | Ranger, Rogue       |

Same skill from multiple members does not stack. Different party skills stack additively up to a +50% cap per stat.

### 9.3 Synergy System

Certain companion pairings produce synergy bonuses when both are in the active party:

| Synergy Type  | Condition                        | Bonus                        |
| ------------- | -------------------------------- | ---------------------------- |
| Elemental Duo | Fire + Ice companions            | +10% elemental damage        |
| Shield Wall   | 2+ Tank role companions          | +15% front row defence       |
| Battle Medics | Support + Tank in same party     | +20% healing received (tank) |
| Ranger Pair   | 2+ Ranged companions in back row | +10% ranged attack speed     |
| Veteran Bond  | 2 companions with Loyal+ loyalty | +5% all stats for both       |

### 9.4 Synergy Discovery

Synergies are discovered through play, not revealed upfront:

- **Discovery trigger**: Companions fight together in 5+ combats
- **Notification**: "Synergy Discovered" shown at next check-in
- **Persistence**: Once discovered, synergy activates automatically when conditions are met
- **Stacking**: Multiple synergies can be active simultaneously, bonuses stack additively

### 9.5 Representative Companion Skills

| Skill           | Role    | Type    | Effect                                  | Cooldown |
| --------------- | ------- | ------- | --------------------------------------- | -------- |
| Shield Bash     | Tank    | Active  | Stun target for 1 turn, moderate damage | 3 rounds |
| Rallying Cry    | Tank    | Active  | +10% party damage for 3 rounds          | 5 rounds |
| Power Strike    | Melee   | Active  | 2x damage to single target              | 2 rounds |
| Volley          | Ranged  | Active  | Hit all enemies for 50% damage          | 4 rounds |
| Mend            | Support | Active  | Heal target for 30% max HP              | 2 rounds |
| Aura of Resolve | Support | Passive | +5% party damage resistance             | Always   |

---

## 10. Offline Party Activity

### 10.1 Activity Queues

When the player is offline, the party continues working through activity queues. Activities are selected at check-in and run on real-time timers:

| Activity    | Timer Range | Output                               | Risk   |
| ----------- | ----------- | ------------------------------------ | ------ |
| Exploration | 1-4 hours   | Map reveals, resource node discovery | Low    |
| Gathering   | 30 min-2 hr | Raw materials based on area          | None   |
| Training    | 1-8 hours   | XP gain for selected skill           | None   |
| Patrol      | 2-6 hours   | Combat encounters, loot, XP          | Medium |
| Rest        | 1-4 hours   | Full HP/stamina recovery, +1 loyalty | None   |

### 10.2 Offline Activity Flow

```
SELECT ACTIVITY (player choice at check-in)
    |
    v
ASSIGN PARTY OR SUBSET (who participates)
    |
    v
TIMER RUNS (real-time, continues offline)
    |
    v
RESULTS CALCULATED AT CHECK-IN (batch processing)
    |
    v
COLLECT: loot, XP, materials, map data
APPLY: loyalty changes, HP changes, skill gains
REPORT: summary of events during absence
```

### 10.3 Offline Efficiency

Offline activity efficiency depends on party state at time of assignment:

```
Offline Efficiency = Base Rate x Loyalty Average x Stance Modifier x Fatigue Modifier

Loyalty Average:  Mean loyalty of participating members / 100
Stance Modifier:
  Aggressive = 1.2x (exploration/patrol), 0.8x (gathering/training)
  Balanced   = 1.0x (all activities)
  Defensive  = 0.8x (exploration/patrol), 1.2x (gathering/training)
  Cautious   = 0.6x (exploration/patrol), 1.1x (gathering/training)

Fatigue Modifier:
  Fresh       = 1.0x
  Tired       = 0.8x (after 8+ hours of continuous activity)
  Exhausted   = 0.5x (after 16+ hours, auto-switches to Rest)
```

### 10.4 Activity Chaining

Players can queue up to 3 activities that execute sequentially:

- Activities run in order, each starting when the previous completes
- If a Patrol results in party wipe, remaining queue is cancelled and party auto-rests
- Results from all completed activities are presented together at check-in

### 10.5 Watch Duty During Rest

When the party rests (offline or online), members can be assigned to watch duty:

```
WATCH DUTY:
  Slots: Up to 2 members on watch simultaneously
  Effect per Watcher:
    -20% ambush chance
    Watcher does not recover fatigue
    Watcher gains Perception XP (slow)

  Combined with Scout Ahead skill:
    Scout Ahead (-15%) + 1 Watcher (-20%) = -35% ambush chance
    Maximum reduction: -60% (2 watchers + Scout Ahead)

  No Watch Duty:
    Base ambush chance per rest period: 15%
    Ambush severity: Based on zone danger level
```

---

## 11. Cross-References

### 11.1 Combat System Integration

Party mechanics feed directly into the combat resolution pipeline defined in combat-system-gdd.md:

| Party Mechanic      | Combat System Reference                        |
| ------------------- | ---------------------------------------------- |
| Stance modifiers    | Applied during damage calculation phase        |
| Formation rows      | Determine targeting eligibility                |
| AI behavior presets | Control Background Simulation AI decisions     |
| Loyalty multiplier  | Scales damage output and healing effectiveness |
| Companion skills    | Enter the skill resolution queue               |
| Synergy bonuses     | Applied as passive modifiers during combat     |
| Retreat thresholds  | Trigger flee behavior in combat AI             |

### 11.2 Other System Dependencies

| System                         | Integration Point                                   |
| ------------------------------ | --------------------------------------------------- |
| crafting-system-gdd.md         | Companions can be assigned to crafting queues       |
| gathering-system-gdd.md        | Offline gathering activity uses gathering mechanics |
| core-progression-system-gdd.md | XP sharing feeds into character progression         |
| settlement-system-gdd.md       | Barracks and War Council unlock party size          |
| class-system-gdd.md            | Companion roles derived from class assignments      |
| npc-core-systems-gdd.md        | Recruitment sources and personality tags            |

### 11.3 Data Model Summary

```
Party:
  id:                 UUID
  leader:             CharacterId
  members:            List<PartyMember>
  stance:             Enum(Aggressive, Balanced, Defensive, Cautious)
  xp_model:           Enum(EqualSplit, ContributionWeighted, ProximityBased)
  current_order:      Enum(Attack, Defend, Retreat, Hold, Explore, Train)
  activity_queue:     List<ActivityAssignment> (max 3)
  watch_config:       WatchConfig

PartyMember:
  character_id:       CharacterId
  role:               Enum(Tank, MeleeDPS, Ranged, Support, Hybrid)
  loyalty:            Integer (0-100)
  personality:        Enum(Steadfast, Eager, Mercenary, Stoic)
  ai_preset:          Enum(AutoOptimal, FocusFire, SpreadDamage, ProtectWeak, Conserve)
  formation_slot:     Integer (1-6)
  formation_row:      Enum(Front, Back)
  joined_at:          Timestamp

ActivityAssignment:
  activity:           Enum(Exploration, Gathering, Training, Patrol, Rest)
  participants:       List<CharacterId>
  started_at:         Timestamp
  estimated_complete: Timestamp
  training_skill:     SkillId? (only for Training activity)

WatchConfig:
  watchers:           List<CharacterId> (max 2)
  active:             Boolean
```
