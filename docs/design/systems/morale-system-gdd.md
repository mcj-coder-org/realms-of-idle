---
type: system
scope: detailed
status: authoritative
version: 1.0.0
created: 2026-02-08
updated: 2026-02-08
subjects: [morale, surrender, prisoners, rout, ransom, combat-psychology]
dependencies: [party-mechanics-gdd.md]
---

# Morale System - Authoritative Game Design

## Executive Summary

The Morale System governs the psychological state of characters in combat and exploration. Rather than tracking morale per-tick during combat, Realms of Idle uses a pre-combat morale modifier calculated from party composition, equipment, and situational factors. This modifier affects auto-resolved combat outcomes, surrender probability, rout chances, and post-combat prisoner handling. The system transforms complex real-time morale tracking into strategic pre-combat decisions.

**This document resolves:**

- Pre-combat morale calculation formula
- Morale threshold effects on combat resolution
- Auto-resolved surrender mechanics
- Prisoner capture, recruitment, ransom, and release
- Rout mechanics and bonus loot
- Entities immune to morale effects
- Settlement-level prisoner policies

**Design Philosophy:** Morale is felt, not micro-managed. Players influence morale through party composition, equipment quality, and leadership — decisions made before combat begins. During auto-resolved combat, morale affects outcomes behind the scenes. The system creates meaningful advantages for well-prepared parties without requiring per-turn psychological tracking. High morale parties fight harder and capture fleeing enemies; low morale parties break and scatter.

---

## 1. Pre-Combat Morale Calculation

### 1.1 Morale Score

Each party has a composite morale score calculated before combat begins. This score modifies combat resolution.

```
PRE-COMBAT MORALE FORMULA:

  Morale Score = Base (50)
    + Party Composition Bonus
    + Equipment Bonus
    + Leadership Bonus
    + Situational Modifiers
    + Status Modifiers

  Range: 0-100
  Neutral: 50 (no modifier to combat)
```

### 1.2 Morale Components

```
PARTY COMPOSITION BONUS:
  Full party (4+ members):          +5
  Healer present:                   +5
  Tank/Defender present:            +3
  Outnumbering enemy:               +10
  Outnumbered by enemy:             -10
  Significantly outnumbered (2:1):  -20
  All members Loyal+ (loyalty 50+): +5

EQUIPMENT BONUS:
  Average equipment tier:
    Tier 1 (Basic):     +0
    Tier 2 (Improved):  +3
    Tier 3 (Advanced):  +5
    Tier 4 (Superior):  +8
    Tier 5 (Masterwork): +12
  Missing critical gear: -5 per missing slot

LEADERSHIP BONUS:
  Party leader present:             +3
  Leader with Inspiring Presence:   +5
  Leader with Tactical Command:     +3
  Leader loyalty Bonded (90+):      +3
  No designated leader:             -5

SITUATIONAL MODIFIERS:
  Defending home settlement:        +10
  Fighting faction enemy:           +5
  Recent victory (last 24 hrs):     +5
  Recent defeat (last 24 hrs):      -8
  Fighting undead/demons:           -3 (fear factor)
  Ambushed:                         -10
  Ambushing enemy:                  +10
  Fighting in known territory:      +3

STATUS MODIFIERS:
  Party fatigue > 70%:              -10
  Party HP < 50% average:           -8
  Poisoned/diseased members:        -3 per affected
  Well-rested (full rest <4 hrs):   +5
  Buffed (magic/potion active):     +3
```

### 1.3 Morale Thresholds

The morale score maps to five threshold levels that modify combat resolution:

| Threshold | Score Range | Combat Modifier | Flee Chance | Special Effect                 |
| --------- | ----------- | --------------- | ----------- | ------------------------------ |
| High      | 80-100      | +15% damage     | 0%          | Bonus loot, surrender demand   |
| Steady    | 60-79       | +5% damage      | 5%          | Normal combat                  |
| Neutral   | 40-59       | +0%             | 10%         | Standard behavior              |
| Shaken    | 20-39       | -10% damage     | 25%         | Reduced skill use              |
| Broken    | 0-19        | -25% damage     | 50%         | Rout likely, no special skills |

---

## 2. Morale in Combat Resolution

### 2.1 Combat Modifier Application

During auto-resolved combat, the morale threshold modifies damage output and defensive capabilities:

```
MORALE COMBAT APPLICATION:

  Damage Output = Base Damage x (1 + Morale Combat Modifier)
  Flee Check = Random(0-100) < Flee Chance (per combat round)

  Example:
    Party morale: 85 (High threshold)
    Base damage: 100
    Modified damage: 100 x 1.15 = 115

    Enemy morale: 25 (Shaken threshold)
    Enemy base damage: 80
    Enemy modified damage: 80 x 0.90 = 72
```

### 2.2 Morale Shift During Combat

Morale can shift during extended auto-resolved combats (3+ rounds):

```
IN-COMBAT MORALE SHIFTS:

  Per Round:
    Ally killed:           -5 morale
    Enemy killed:          +3 morale
    Critical hit landed:   +2 morale
    Critical hit received: -2 morale
    Leader downed:         -15 morale

  Recalculated at:
    Round 3, Round 6, Round 9 (long combats only)
    Threshold can change mid-combat
```

---

## 3. Surrender Mechanics

### 3.1 Surrender Probability

When one side's morale drops to Shaken or Broken, surrender becomes possible. Surrender is auto-resolved based on the morale differential between the two sides.

```
SURRENDER FORMULA:

  Surrender Chance = Base (0%) + Morale Differential Bonus + Situation Bonus

  Morale Differential Bonus:
    Attacker High, Defender Broken:  +40%
    Attacker Steady, Defender Broken: +25%
    Attacker High, Defender Shaken:   +15%
    Otherwise:                         +0%

  Situation Bonus:
    Defender HP < 25% average:   +15%
    Defender outnumbered 2:1:    +10%
    Defender leader killed:      +10%
    Defender has escape route:   -20% (prefer flee over surrender)

  Maximum surrender chance: 80% (some always fight to the end)
```

### 3.2 Entities That Never Surrender

Certain entity types are immune to morale effects and never surrender:

```
MORALE-IMMUNE ENTITIES:

  Undead:       No morale system, fight until destroyed
  Constructs:   Golems, animated objects, no psychology
  Summoned:     Bound to summoner, fight until dispelled
  Mindless:     Beasts (feral), slimes, swarms
  Fanatics:     NPCs with Courage 95+ and faction-specific flag

  Effect: These entities:
    - Always have effective morale of 50 (Neutral)
    - Never flee or surrender
    - Cannot be intimidated
    - Do not benefit from morale bonuses
```

### 3.3 Post-Surrender Options

When enemies surrender, the player receives auto-resolved options (selected based on pre-set policy or manual choice):

| Option       | Effect                                     | Standing Impact                |
| ------------ | ------------------------------------------ | ------------------------------ |
| Demand Items | Take equipment/gold (50-80% of enemy loot) | -3 enemy faction               |
| Dismiss      | Release without penalty                    | +2 enemy faction               |
| Recruit      | Attempt to recruit (success based on CHA)  | -5 enemy faction               |
| Attack       | Continue fighting surrendered enemies      | -15 enemy faction, -5 morality |

---

## 4. Rout Mechanics

### 4.1 Rout Triggers

When a side's morale hits Broken and flee checks succeed, a rout occurs. Routing enemies scatter, ending the combat.

```
ROUT CONDITIONS:

  Trigger: 50%+ of a side's members pass flee check in same round
  Effect: Entire side attempts to flee

  Rout Success:
    Base flee success: 60%
    Per level above pursuer: +5%
    Per level below pursuer: -5%
    Light armor bonus: +10%
    Heavy armor penalty: -10%
    Scout/Ranger skill: +15% (individual only)
```

### 4.2 Pursuit and Capture

When enemies rout, the winning side can capture fleeing NPCs:

```
CAPTURE MECHANICS:

  Capture Check (per routing NPC):
    Capture Chance = 50% - (Target Speed - Pursuer Speed) x 5%

  Speed Factors:
    Base speed by armor type
    Skill bonuses (Sprint, Escape Artist)
    Terrain modifier (forest -10% capture, open +10%)

  Result:
    Captured: NPC becomes a prisoner
    Escaped: NPC flees, may be encountered later
```

### 4.3 Bonus Loot from Rout

Routing enemies drop extra loot compared to a standard combat victory:

```
ROUT LOOT BONUS:

  Standard combat victory: 100% base loot
  Rout victory: 130% base loot (enemies drop supplies while fleeing)
  High morale rout (morale 80+): 150% base loot

  Dropped items include:
    - Consumables (potions, food)
    - Gold pouches
    - Non-equipped gear
    - Quest items (never lost to rout)
```

---

## 5. Prisoner System

### 5.1 Prisoner Status

Captured NPCs become prisoners. Prisoners are a resource that can be managed through settlement policy or manual decisions.

```
PRISONER MANAGEMENT:

  Prisoner Capacity:
    Party: 2 prisoners per party member (carry along)
    Settlement: 5 per jail cell building
    No jail: Cannot hold prisoners in settlement

  Prisoner State:
    HP: Restored to 25% after capture
    Equipment: Confiscated (added to player inventory)
    Morale: Broken (0)
    Status: Cannot fight, cannot flee (guarded)
    Duration: Indefinite until player decides
```

### 5.2 Prisoner Options

Players can manage prisoners through the following actions:

| Action   | Timer     | Effect                                      | Requirements                      |
| -------- | --------- | ------------------------------------------- | --------------------------------- |
| Recruit  | 24-72 hrs | Attempt to convert prisoner to party member | CHA check, loyalty starts at Wary |
| Ransom   | 4-12 hrs  | Exchange prisoner for gold                  | Prisoner's faction exists         |
| Release  | Instant   | Free prisoner, gain faction standing        | None                              |
| Imprison | Ongoing   | Hold in settlement jail                     | Jail building                     |
| Execute  | Instant   | Remove prisoner permanently                 | -10 morality alignment            |

### 5.3 Ransom System

Ransom value depends on the prisoner's importance and their faction's wealth:

```
RANSOM VALUE FORMULA:

  Base Value = Prisoner Level x 10 gold

  Modifiers:
    Named NPC:              x3.0
    Faction leader:         x5.0
    Common soldier:         x1.0
    Wealthy faction:        x1.5
    Poor faction:           x0.5

  Ransom Timer:
    Negotiation: 4 hours (base)
    Wealthy faction: 2 hours (pay quickly)
    Poor faction: 12 hours (gather funds)

  Standing Effect:
    Successful ransom: +3 with prisoner's faction (returned alive)
    Failed negotiation: No standing change, prisoner remains
```

### 5.4 Recruitment from Prisoners

Converting prisoners to party members is a slower but potentially rewarding path:

```
PRISONER RECRUITMENT:

  Success Chance = Base (30%) + CHA Modifier + Time Modifier + Alignment Modifier

  CHA Modifier: (CHA - 10) x 2%
  Time Modifier: +5% per 24 hours imprisoned (max +20%)
  Alignment Modifier:
    Same alignment: +15%
    Opposing alignment: -15%

  On Success:
    Prisoner joins party at Loyalty Level 1 (Wary)
    Loyalty growth rate: 0.5x normal (trust deficit)
    After reaching Loyal (50+): Normal loyalty growth rate

  On Failure:
    Can retry after 48 hours
    Each failure: -5% base chance (bitter)
    After 3 failures: Prisoner refuses permanently
```

---

## 6. Settlement Prisoner Policies

### 6.1 Policy Options

Settlements with a Town Hall (Tier 2+) can set a default prisoner policy that auto-resolves captured NPCs:

```
SETTLEMENT PRISONER POLICIES:

  Lenient:
    Default action: Release after 24 hours
    Standing bonus: +5 with prisoner factions
    Gold cost: None
    Morale effect: +5 settlement morale

  Pragmatic:
    Default action: Ransom if possible, release otherwise
    Standing bonus: +3 with prisoner factions
    Gold income: Ransom value
    Morale effect: +0

  Strict:
    Default action: Imprison indefinitely
    Standing bonus: -5 with prisoner factions
    Gold cost: 5 gold/day per prisoner (food/guards)
    Morale effect: -3 settlement morale
    Benefit: Prisoners can be recruited over time

  Harsh:
    Default action: Execute after 48 hours
    Standing bonus: -15 with prisoner factions
    Gold cost: None
    Morale effect: -8 settlement morale
    Alignment: -5 morality per execution
    Benefit: Deters future attacks (defense +5 per execution, max +20)
```

---

## 7. Morale Recovery

### 7.1 Post-Combat Recovery

After combat ends, morale recovers based on the outcome and party state:

```
MORALE RECOVERY:

  Victory:
    Morale returns to pre-combat level + 5 (confidence boost)
    Recovery time: Instant

  Defeat (survived):
    Morale drops to 30 (Shaken)
    Recovery rate: +5 per hour of rest
    Full recovery: ~4-6 hours

  Rout (fled):
    Morale drops to 15 (Broken)
    Recovery rate: +3 per hour of rest
    Full recovery: ~8-12 hours

  Recovery Modifiers:
    Settlement inn rest: 2x recovery speed
    Rally Cry skill: +15% recovery rate
    Bard in party: +10% recovery rate
    Low party fatigue: +5% recovery rate
```

### 7.2 Persistent Morale Effects

Repeated defeats create lingering morale penalties:

```
PERSISTENT MORALE EFFECTS:

  Defeat Streak:
    2 defeats in 24 hours: -5 base morale for 48 hours
    3 defeats in 24 hours: -10 base morale for 72 hours
    5+ defeats in 24 hours: -15 base morale for 7 days

  Victory Streak:
    3 victories in 24 hours: +5 base morale for 48 hours
    5 victories in 24 hours: +10 base morale for 72 hours

  Reset:
    Long rest (8+ hours in settlement): Clears all streak effects
```

---

## 8. Integration Points

### 8.1 System Dependencies

```
MORALE SYSTEM INTEGRATIONS:

  Party System:
    - Party composition directly feeds morale formula
    - Loyalty levels affect morale bonuses
    - Party skills (Inspiring Presence, Rally Cry) modify morale
    - Stance configuration affects morale through combat outcomes

  Combat System:
    - Morale modifies damage output and flee chance
    - In-combat events shift morale (kills, crits, leader down)
    - Surrender/rout determined by morale thresholds
    - Morale-immune entities bypass the system entirely

  Settlement System:
    - Settlement defense rating affects morale in defense scenarios
    - Prisoner policies are settlement-level configuration
    - Jail buildings enable prisoner holding
    - Settlement morale affected by prisoner policy choice

  Faction System:
    - Surrender/prisoner actions affect faction standing
    - Ransom interacts with faction wealth
    - Prisoner execution affects morality alignment

  Progression System:
    - Morale victories grant bonus XP
    - Rout victories grant bonus loot
    - Leadership skills provide morale bonuses
```

### 8.2 Data Model Summary

```
CombatMorale:
  party_id:           PartyId
  base_score:         Integer (0-100)
  current_score:      Integer (0-100)
  threshold:          Enum(High, Steady, Neutral, Shaken, Broken)
  streak_modifier:    Integer
  streak_expiry:      Timestamp?

Prisoner:
  id:                 UUID
  character_id:       CharacterId
  captured_by:        PartyId
  captured_at:        Timestamp
  location:           Enum(Party, Settlement)
  settlement_id:      SettlementId?
  status:             Enum(Held, Ransoming, Recruiting, Released, Executed)
  recruitment_attempts: Integer
  ransom_value:       Integer

SettlementPrisonerPolicy:
  settlement_id:      SettlementId
  policy:             Enum(Lenient, Pragmatic, Strict, Harsh)
  prisoner_count:     Integer
  jail_capacity:      Integer
```
