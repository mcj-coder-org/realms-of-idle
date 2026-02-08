---
type: system
scope: detailed
status: authoritative
version: 1.0.0
created: 2026-02-08
updated: 2026-02-08
subjects: [factions, reputation, standing, personality, alignment, underground]
dependencies: []
---

# Faction & Reputation - Authoritative Game Design

## Executive Summary

The Faction & Reputation system governs how NPCs perceive the player and each other through personality traits, faction standing, and alignment tracking. In Realms of Idle, reputation changes are driven by scenario outcomes rather than witnessed actions — simplifying the predecessor's propagation chain while preserving meaningful consequences. Standing shifts based on completed scenarios, and a separate underground reputation track opens criminal content paths.

**This document resolves:**

- NPC personality trait system (6 axes as static modifiers)
- Alignment tracking (emergent from scenario outcomes)
- Faction standing scale and progression
- Reputation change mechanics (direct, no witness chain)
- Reputation decay (slow drift toward neutral)
- Underground reputation as a separate track
- Deception as scenario choice outcomes

**Design Philosophy:** Reputation is a consequence of choices, not a game of information management. Players see clear cause-and-effect between their scenario selections and faction standing changes. The system avoids hidden propagation chains or complex witness mechanics — when you help a faction, they know. When you betray them, they know. Depth comes from balancing competing faction interests, not from gaming information flow.

---

## 1. NPC Personality Traits

### 1.1 Trait Axes

Every NPC has six personality trait values that serve as static modifiers affecting their behavior, dialogue, and scenario interactions. Traits are assigned at NPC creation and do not evolve.

| Trait       | Low Value (0–30)      | Mid Value (31–70) | High Value (71–100)   | Modifier Domain             |
| ----------- | --------------------- | ----------------- | --------------------- | --------------------------- |
| Courage     | Cowardly, risk-averse | Pragmatic         | Bold, reckless        | Combat morale, flee chance  |
| Generosity  | Greedy, hoarding      | Fair              | Charitable, giving    | Trade prices, gift response |
| Sociability | Reclusive, distant    | Amicable          | Outgoing, charismatic | Recruitment time, loyalty   |
| Honesty     | Deceptive, scheming   | Practical         | Truthful, transparent | Quest reliability, betrayal |
| Patience    | Impulsive, rash       | Measured          | Methodical, slow      | Training speed, craft time  |
| Curiosity   | Conservative, set     | Open-minded       | Adventurous, restless | Exploration bonus, research |

### 1.2 Trait Modifiers

Traits modify NPC behavior through simple multipliers applied to relevant systems:

```
TRAIT MODIFIER FORMULA:

  Modifier = (Trait Value - 50) / 100

  Result Range: -0.50 to +0.50 (applied as percentage modifier)

  Examples:
    Courage 80:    +0.30 combat morale modifier
    Generosity 20: -0.30 trade price modifier (charges more)
    Honesty 90:    +0.40 quest completion reliability

  Application:
    Modifiers are additive with other system modifiers
    Maximum total modifier from traits: +/- 0.50 per stat
```

### 1.3 Trait Interactions

When NPCs interact, their trait compatibility affects outcomes:

```
TRAIT COMPATIBILITY:

  Compatible: Same trait within 20 points of each other
  Conflicting: Same trait differs by 50+ points
  Neutral: Everything else

  Effect on Interactions:
    Compatible traits:    +10% interaction success, +1 loyalty/day
    Conflicting traits:   -10% interaction success, -1 loyalty/day
    Neutral:              No modifier

  Example:
    Player party leader (Courage 70) + NPC (Courage 75) = Compatible (+10%)
    Player party leader (Honesty 80) + NPC (Honesty 20) = Conflicting (-10%)
```

---

## 2. Alignment Tracking

### 2.1 Alignment Axes

Alignment is emergent — it shifts based on scenario outcomes rather than being chosen. Three independent axes track a character's moral position.

| Axis       | Negative Pole | Neutral | Positive Pole | Range |
| ---------- | ------------- | ------- | ------------- | ----- |
| Morality   | Evil (0)      | Neutral | Good (100)    | 0–100 |
| Lawfulness | Unlawful (0)  | Neutral | Lawful (100)  | 0–100 |
| Order      | Chaotic (0)   | Neutral | Orderly (100) | 0–100 |

### 2.2 Alignment Shifts

Scenario outcomes shift alignment based on the nature of the choice:

```
ALIGNMENT SHIFT EXAMPLES:

  Scenario: Help a merchant recover stolen goods
    Choice A — Return goods honestly:
      Morality +5, Lawfulness +3, Order +2
    Choice B — Return goods, keep some for yourself:
      Morality -2, Lawfulness -3, Order +0
    Choice C — Sell the goods yourself:
      Morality -5, Lawfulness -5, Order -2

  Scenario: Encounter a fugitive from law
    Choice A — Turn them in:
      Lawfulness +5, Order +3
    Choice B — Let them go:
      Morality +2, Lawfulness -3
    Choice C — Help them escape:
      Morality +3, Lawfulness -5, Order -3

  Shift Rate:
    Minor scenarios: +/- 1 to 3 per axis
    Major scenarios: +/- 3 to 8 per axis
    Critical scenarios: +/- 5 to 15 per axis
```

### 2.3 Alignment Effects

Alignment influences NPC reactions and available scenario options:

```
ALIGNMENT THRESHOLDS:

  Good (Morality 70+):
    +10% reputation gain with lawful factions
    Unlocks "heroic" scenario paths
    Religious factions offer discounts

  Evil (Morality 30-):
    +10% reputation gain with criminal factions
    Unlocks "villainous" scenario paths
    Lawful factions charge premium prices

  Lawful (Lawfulness 70+):
    Access to government quests
    Guards are friendly (+5 defense in settlements)
    Criminal factions charge premium

  Chaotic (Order 30-):
    Access to resistance/rebel quests
    -5% prices at black markets
    Increased random event frequency (good and bad)
```

---

## 3. Faction Standing

### 3.1 Standing Scale

Each faction tracks the player's standing on a 0–100 scale with six named levels. Standing is never permanently locked — recovery is always possible.

| Level    | Value Range | Trade Modifier | Quest Access       | Special Effects              |
| -------- | ----------- | -------------- | ------------------ | ---------------------------- |
| Hated    | 0–15        | +50% prices    | Hostile encounters | Attack on sight in territory |
| Disliked | 16–30       | +25% prices    | No quests offered  | Cold reception, limited shop |
| Neutral  | 31–50       | Normal prices  | Basic quests       | Standard interactions        |
| Liked    | 51–70       | -10% prices    | Advanced quests    | Friendly greetings           |
| Honored  | 71–90       | -20% prices    | Elite quests       | Faction gear access          |
| Allied   | 91–100      | -30% prices    | All quests         | Faction title, unique perks  |

### 3.2 Standing Changes

Standing changes directly from scenario outcomes and player actions. There is no witness or propagation chain.

```
STANDING CHANGE SOURCES:

  Scenario Completion:
    Favorable outcome:    +3 to +15 standing
    Neutral outcome:      +0 to +3 standing
    Unfavorable outcome:  -3 to -15 standing

  Direct Actions:
    Gift to faction:      +1 to +5 (based on value)
    Trade with faction:   +1 per 100 gold transacted
    Attack faction member: -10 to -25
    Complete faction quest: +5 to +20

  Indirect Effects:
    Help faction's ally:   +2 to +5 with faction
    Help faction's enemy:  -2 to -5 with faction
    Destroy enemy of faction: +5 to +10

STANDING CHANGE MODIFIERS:
    Alignment match:       +20% standing gain
    Alignment conflict:    -20% standing gain
    Personality match:     +10% standing gain (leader's traits)
```

### 3.3 Reputation Decay

All standing values slowly drift toward neutral (45) over time. This prevents permanent lock-in and encourages ongoing engagement.

```
REPUTATION DECAY:

  Rate: 1 point per week toward 45 (neutral center)

  Decay Direction:
    Standing > 45: Decreases by 1/week
    Standing < 45: Increases by 1/week
    Standing = 45: No change

  Exceptions:
    Allied (91+): Decay rate halved (0.5/week)
    Hated (0–15): Decay rate halved (0.5/week)
    Locked standing: Some quest completions lock minimum standing

  Purpose:
    Prevents "set and forget" reputation
    Encourages ongoing faction interaction
    Allows recovery from bad standing over time
```

---

## 4. Faction Types

### 4.1 Faction Categories

Factions are organized by type, which determines their primary concerns and standing triggers.

| Category  | Examples                        | Primary Standing Driver        |
| --------- | ------------------------------- | ------------------------------ |
| Political | Kingdom of Aldran, Free Cities  | Lawfulness alignment, quests   |
| Religious | Temple of Light, Shadow Cult    | Morality alignment, donations  |
| Trade     | Merchants Guild, Caravan League | Trade volume, quest completion |
| Military  | Iron Guard, Frontier Rangers    | Combat victories, defense aid  |
| Criminal  | Thieves Guild, Smugglers Ring   | Underground rep, criminal acts |
| Academic  | Mages Academy, Scribes Circle   | Research, artifact discovery   |
| Cultural  | Elven Court, Dwarven Holds      | Race/culture alignment, trade  |

### 4.2 Faction Relationships

Factions have fixed relationships with each other that affect how standing changes cascade.

```
FACTION RELATIONSHIP MAP:

  Allied Factions:
    Gain with one = smaller gain with ally (+30% of change)
    Loss with one = smaller loss with ally (+30% of change)

  Rival Factions:
    Gain with one = smaller loss with rival (-20% of change)
    Loss with one = smaller gain with rival (+20% of change)

  Neutral Factions:
    No cascade effect

  Example:
    Merchants Guild and Trade Council are Allied
    Player completes quest for Merchants Guild: +10 standing
    Trade Council also gains: +3 standing (30% cascade)

    Merchants Guild and Smugglers Ring are Rivals
    Player completes quest for Merchants Guild: +10 standing
    Smugglers Ring loses: -2 standing (20% cascade)
```

---

## 5. Underground Reputation

### 5.1 Underground Track

A separate reputation track for criminal and underworld activities. This operates independently of surface faction standing.

| Level   | Value Range | Access                         | Risk Level |
| ------- | ----------- | ------------------------------ | ---------- |
| Unknown | 0–15        | No criminal content available  | None       |
| Noticed | 16–35       | Petty crime scenarios          | Low        |
| Known   | 36–55       | Mid-tier criminal quests       | Medium     |
| Trusted | 56–80       | Major heists, smuggling routes | High       |
| Insider | 81–100      | Criminal faction leadership    | Very High  |

### 5.2 Underground Standing Changes

```
UNDERGROUND REPUTATION SOURCES:

  Gaining Underground Rep:
    Complete criminal scenario:     +5 to +15
    Sell stolen goods:              +1 to +3
    Bribe official:                 +2 to +5
    Smuggle contraband:             +3 to +8
    Betray lawful faction:          +5 to +10

  Losing Underground Rep:
    Turn in criminal to authorities: -5 to -15
    Report criminal activity:        -3 to -8
    Refuse criminal quest:           -2 to -5
    Work with law enforcement:       -5 to -10

  Decay:
    Underground rep drifts toward 0 (not neutral)
    Rate: -1 per week
    Reason: Criminal networks forget you if inactive
```

### 5.3 Underground vs Surface Tension

```
REPUTATION TENSION:

  High Underground + High Lawful Standing:
    Requires careful balancing
    Risk: Random "investigation" events that may expose criminal ties
    Investigation chance: Underground Rep / 200 per week
    If exposed: Lawful standing -20, Underground standing +5

  Mitigation:
    High Patience trait reduces investigation chance (-10%)
    Certain scenario choices can compartmentalize reputation
```

---

## 6. Deception Mechanics

### 6.1 Simplified Deception

Deception is handled as scenario choice outcomes rather than a complex skill system. When scenarios offer deceptive options, success is determined by character stats and faction standing.

```
DECEPTION IN SCENARIOS:

  Scenario choices may include deceptive options:
    - Lie to an NPC
    - Disguise identity
    - Forge documents
    - Bribe officials
    - Smuggle goods

  Success Formula:
    Success Chance = Base (50%) + Charisma Modifier + Skill Modifier + Standing Modifier

    Charisma Modifier: (CHA - 10) x 2%
    Skill Modifier: Relevant skill level x 5%
    Standing Modifier:
      Liked+:   +10% (trusted)
      Neutral:  +0%
      Disliked: -15% (suspected)

  Failure Consequences:
    Minor failure: Scenario fails, small standing loss (-3)
    Major failure: Standing loss (-10), possible combat encounter
    Critical failure: Standing loss (-15), bounty placed
```

---

## 7. Integration Points

### 7.1 System Dependencies

```
FACTION & REPUTATION INTEGRATIONS:

  Party System:
    - Party leader's standing used for faction interactions
    - Mixed-faction parties have loyalty penalties
    - NPC personality affects recruitment and loyalty

  Settlement System:
    - Settlement faction alignment affects available buildings
    - Faction standing affects trade prices in settlements
    - Allied factions may provide settlement defense aid

  Combat System:
    - Faction standing determines hostile/friendly encounters
    - Hated standing = attack on sight in faction territory
    - Allied standing = faction NPCs join combat as reinforcements

  Scenario System:
    - Scenarios are the primary driver of standing changes
    - Alignment determines available scenario choices
    - Faction standing gates quest availability

  Economy System:
    - Standing modifies trade prices with faction merchants
    - Underground rep unlocks black market access
    - Allied factions offer exclusive items
```

### 7.2 Data Model Summary

```
CharacterReputation:
  character_id:       CharacterId
  personality_traits:
    courage:          Integer (0-100)
    generosity:       Integer (0-100)
    sociability:      Integer (0-100)
    honesty:          Integer (0-100)
    patience:         Integer (0-100)
    curiosity:        Integer (0-100)
  alignment:
    morality:         Integer (0-100)
    lawfulness:       Integer (0-100)
    order:            Integer (0-100)
  faction_standings:  Map<FactionId, Integer (0-100)>
  underground_rep:    Integer (0-100)
  locked_standings:   Map<FactionId, Integer> (minimum locked values)
```
