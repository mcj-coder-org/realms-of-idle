---
title: Combat System
type: system
category: authoritative
status: authoritative
version: 1.0.0
created: 2026-02-03
updated: 2026-02-03
subjects: ['design', 'mechanics', 'gameplay', 'combat', 'AI']
---

# Combat System (Authoritative GDD)

## Overview

Idle-world's combat system is a **hybrid real-time-with-pause system** that supports three distinct modes:

1. **Background Simulation** — Pure tick-based, instant resolution, zero player involvement
2. **Spectator Mode** — Tick-based combat animated to appear real-time, all AI-controlled
3. **Player Control** — True real-time combat with pause capability, player controls one character

The system balances idle-game automation (world runs itself) with player agency (possess characters, direct combat). All combat uses the same underlying mechanics, but presentation and control vary by mode.

---

## Design Principles

| Principle               | Description                                                                                                           |
| ----------------------- | --------------------------------------------------------------------------------------------------------------------- |
| **Idle-First**          | The world simulates automatically. Player can watch, walk away, or jump in. No player input required for progression. |
| **Possession-Based**    | Players can "possess" any favourited character to take direct control. Unpossessed = AI auto-pilots.                  |
| **Mode-Adaptive**       | Combat depth scales with involvement: simple for AI-vs-AI, tactical for player control.                               |
| **Attribute-Driven**    | Six core attributes (STR, FIN, END, WIT, AWR, CHA) are the foundation for all combat calculations.                    |
| **Skill-Multiplied**    | Skills provide either flat additive bonuses or scaling multiplicative bonuses on top of attributes.                   |
| **Consequences Matter** | Soft permadeath (rare escapes), equipment degradation, meaningful resource costs.                                     |

---

## 1. Combat Modes

### 1.1 Mode Overview

```
┌─────────────────────────────────────────────────────────────┐
│                    COMBAT MODE DECISION TREE                │
├─────────────────────────────────────────────────────────────┤
│                                                              │
│  Is ANY player controlling a character in this fight?        │
│    │                                                          │
│    ├─ NO ──► [BACKGROUND SIMULATION]                         │
│  (Pure AI vs AI)                                             │
│    - Tick-based resolution                                   │
│    - Instant calculation (no animation)                      │
│    - Simple aggro targeting                                  │
│    - No positioning (rows abstracted)                        │
│    - Full defense layers (Shields → Armor → Health)          │
│                                                              │
│    ├─ YES ──► Is player CURRENTLY POSSESSING a character?    │
│       │                                                       │
│       ├─ NO (spectating) ──► [SPECTATOR MODE]                │
│     - Tick-based but animated smoothly                       │
│     - Appears real-time (2-second ticks displayed as fluid)  │
│     - All AI-controlled (personality targeting)              │
│     - Full positioning (front/back rows)                     │
│     - Full tactical depth visible                            │
│                                                              │
│       └─ YES (direct control) ──► [PLAYER CONTROL MODE]      │
│         - True real-time with pause                          │
│         - Player controls one character fully                │
│         - Role-based targeting for player party              │
│         - Full tactical positioning                         │
│         - Manual skill/item activation                      │
│         - Can issue orders to party                          │
│                                                              │
└─────────────────────────────────────────────────────────────┘
```

### 1.2 Mode Comparison

| Aspect               | Background             | Spectator           | Player Control           |
| -------------------- | ---------------------- | ------------------- | ------------------------ |
| **Time System**      | Tick (instant)         | Tick (animated)     | Real-time                |
| **Pause**            | No                     | No                  | Yes (player toggle)      |
| **Targeting**        | Simple aggro           | Personality-driven  | Role-based (player)      |
| **Positioning**      | Abstracted             | Front/back rows     | Full rows + flanking     |
| **Skill Activation** | Auto (priority)        | Auto (priority)     | Manual (player)          |
| **Resource Costs**   | Full stamina/mana      | Full stamina/mana   | Full stamina/mana        |
| **Damage Types**     | Full 6 types           | Full 6 types        | Full 6 types             |
| **Status Effects**   | Full system            | Full system         | Full system              |
| **Presentation**     | None (numbers only)    | Animated combat log | Full UI + combat log     |
| **Speed**            | Instant (milliseconds) | 1 tick = 2 seconds  | Real-time (configurable) |

### 1.3 Mode Switching

```
Combat Start:
  IF player.is_possessing_any_combatant:
    MODE = PLAYER_CONTROL
  ELSE IF player.is_watching_combat:
    MODE = SPECTATOR
  ELSE:
    MODE = BACKGROUND

During Combat:
  Player can:
  - Enter spectator mode at any time (click combat notification)
  - Possess any favourited character (click portrait → "Possess")
  - Release possession (return to spectator/observer)

On Possession:
  IF multiplayer AND other_players_involved:
    Switch to TURN-BASED mode (all players take turns)
  ELSE:
    Stay in REAL-TIME mode (player has exclusive control)
```

---

## 2. Attributes & Resources

### 2.1 Six Core Attributes

All characters (players and NPCs) share the same six attributes. Attributes are the **foundation** for all combat calculations.

| Attribute     | Abbr | Combat Functions                                                            |
| ------------- | ---- | --------------------------------------------------------------------------- |
| **Strength**  | STR  | Melee damage, carry capacity, grappling, knockback resistance               |
| **Finesse**   | FIN  | Ranged accuracy, dodge/evasion, stealth, aimed attacks, attack speed        |
| **Endurance** | END  | Health pool, stamina pool, stamina recovery, resistance, fatigue resistance |
| **Wit**       | WIT  | Mana pool, mana regeneration, learning speed, strategy (AI targeting)       |
| **Awareness** | AWR  | Initiative, ambush prevention, detection, combat priority                   |
| **Charm**     | CHA  | Morale effect, intimidation, leadership (order compliance)                  |

#### Attribute Scaling (Thresholds)

**Strength (Melee Damage Modifier)**

```
STR 5:  -15% melee damage
STR 8:  -5% melee damage
STR 10: Baseline (100%)
STR 12: +5% melee damage
STR 15: +12% melee damage
STR 18: +20% melee damage
STR 20+: +25%+ melee damage
```

**Finesse (Dodge & Ranged Accuracy)**

```
FIN 5:  -10% dodge, -15% ranged accuracy
FIN 8:  -3% dodge, -5% ranged accuracy
FIN 10: Baseline
FIN 12: +4% dodge, +5% ranged accuracy
FIN 15: +10% dodge, +12% ranged accuracy
FIN 18: +16% dodge, +20% ranged accuracy
FIN 20+: +20%+ dodge, +25%+ ranged accuracy
```

**Endurance (Health, Stamina, Resistance)**

```
END 5:  -25% health/stamina, -15% resistance
END 8:  -10% health/stamina, -5% resistance
END 10: Baseline
END 12: +10% health/stamina, +5% resistance
END 15: +20% health/stamina, +12% resistance
END 18: +32% health/stamina, +20% resistance
END 20+: +40%+ health/stamina, +25%+ resistance
```

**Example Calculation**

```
Character: Marcus (Warrior)
STR: 14 (+9% melee damage)
FIN: 11 (+3% dodge, +1% ranged accuracy)
END: 13 (+15% health/stamina, +7% resistance)
WIT: 9 (-3% mana)
AWR: 12 (+5% initiative)
CHA: 8 (-5% starting disposition, worse prices)

Weapon: Iron Longsword (12-18 base damage)
STR modifier: +9%
Skill: [Sword Mastery] (+15% damage, multiplicative)
XP Bucket: combat.melee.sword (5000 XP = +10% damage)

Base Damage = 12-18
+ STR (14) = +9%
= 13-19 (baseline)
× Skill (15% bonus) = ×1.15
× Bucket XP (10% bonus) = ×1.10

Final Damage = (13 to 19) × 1.15 × 1.10
            = 16 to 24 damage per hit
```

### 2.2 Resource Pools

#### Stamina (Universal)

All characters have stamina. Drained by physical actions.

| Aspect           | Details                                                  |
| ---------------- | -------------------------------------------------------- |
| **Derived From** | END (Endurance attribute)                                |
| **Base Pool**    | END × 10 (e.g., END 12 = 120 stamina)                    |
| **Drained By**   | Melee attacks, physical skills, movement, combat actions |
| **Zero Stamina** | Character collapses ("naps") until adrenaline burst      |
| **Recovery**     | Rest, food, adrenaline burst (20-30% instant recovery)   |

**Stamina Costs**

```
Basic Attack (melee): 5 stamina
Basic Attack (ranged): 3 stamina
Defend: 2 stamina
Use Skill (physical): 5-15 stamina (varies by skill)
Use Skill (magic): 0 stamina (uses mana instead)
Cast Spell: 0 stamina (uses mana)
Use Item: 0 stamina
Aimed Attack: 8 stamina
Flee: 10 stamina
Fallback: 5 stamina
```

#### Mana (Casters Only)

Only magic-trained characters have mana. Drained by spellcasting.

| Aspect           | Details                                                    |
| ---------------- | ---------------------------------------------------------- |
| **Derived From** | WIT (Wit attribute)                                        |
| **Base Pool**    | WIT × 10 (e.g., WIT 14 = 140 mana)                         |
| **Who Has It**   | Only characters with magic training/classes                |
| **Drained By**   | Spellcasting, channeling, enchanting                       |
| **Zero Mana**    | Cannot cast spells (no collapse, just "out of mana" alert) |
| **Recovery**     | Rest, meditation, mana potions                             |

**Mana Costs**

```
Cantrip: 5 mana
Basic Spell: 10-20 mana
Intermediate Spell: 25-50 mana
Advanced Spell: 60-100 mana
Ultimate Spell: 150+ mana
```

### 2.3 Attribute & Resource Progression

Attributes grow through:

| Source                | Frequency             | Amount                              |
| --------------------- | --------------------- | ----------------------------------- |
| Class level-up        | Each level            | +1-2 to class-relevant attributes   |
| Total level milestone | Every 10 total levels | +1 to all attributes                |
| Skill passive         | When unlocked         | +1-3 to specific attribute          |
| Equipment             | While worn            | Temporary bonus (quality dependent) |
| Consumables           | Duration-limited      | Temporary +3-5                      |
| Training              | Time + cost           | Permanent (+3 max per attribute)    |

---

## 3. Combat Formulas

### 3.1 Damage Calculation

All damage uses the same formula structure: **(Base + Additive) × Multiplicative**

```
┌──────────────────────────────────────────────────────────────┐
│                    DAMAGE CALCULATION PIPELINE               │
├──────────────────────────────────────────────────────────────┤
│                                                               │
│  STEP 1: BASE DAMAGE                                         │
│    Base Damage = Weapon Base Damage + STR Modifier           │
│                                                               │
│    Example: Iron Longsword (12-18) + STR 14 (+9%)           │
│    Base Damage = (12 to 18) × 1.09 = 13 to 19               │
│                                                               │
│  STEP 2: ADDITIVE MODIFIERS (Flat bonuses)                   │
│    Additive = Flat skill bonuses + flat item effects         │
│                                                               │
│    Example: [Brutal Strike] skill (+5 damage)               │
│    Additive = +5 damage                                      │
│                                                               │
│    Subtotal = Base Damage + Additive                         │
│    Subtotal = (13 to 19) + 5 = 18 to 24                     │
│                                                               │
│  STEP 3: MULTIPLICATIVE MODIFIERS (% bonuses)                │
│    Multiplier = 1.0 + All % bonuses (added together)         │
│                                                               │
│    Example:                                                  │
│    - [Sword Mastery] skill: +15% damage                     │
│    - combat.melee.sword XP (5000): +10% damage              │
│    - Silver sword vs undead: +20% damage                    │
│                                                               │
│    Multiplier = 1.0 + 0.15 + 0.10 + 0.20 = 1.45             │
│                                                               │
│  STEP 4: FINAL DAMAGE                                         │
│    Final Damage = Subtotal × Multiplier                      │
│    Final Damage = (18 to 24) × 1.45 = 26 to 35              │
│                                                               │
│  STEP 5: APPLY DEFENSE                                        │
│    Incoming Damage hits Shields → Armor → Health             │
│    (See Section 4: Defense Layers)                           │
│                                                               │
└──────────────────────────────────────────────────────────────┘
```

### 3.2 Skill Integration

Skills modify damage in two distinct ways:

**Flat Additive Skills**

```
Example: [Brutal Strike]
Effect: +5 damage to all melee attacks
Type: Flat additive

Applied in STEP 2 (before multipliers)
Final damage includes this +5 before % multipliers apply
```

**Scaling Multiplicative Skills**

```
Example: [Sword Mastery]
Effect: +15% melee damage with swords
Type: Multiplicative
Scaling: Tracks combat.melee.sword bucket XP
Formula: 15% + (Bucket XP / 1000) × 1%
  - 1000 XP = +16% damage
  - 5000 XP = +20% damage
  - 10000 XP = +25% damage (soft cap)

Applied in STEP 3 (multiplies subtotal)
```

**XP Bucket Integration**

```
Example: combat.melee.sword bucket
XP Gained From: Attacking with swords, training, quests

Bonus Formula:
BucketBonus = min(25%, (BucketXP / 1000) × 2%)
- 1000 XP = +2% damage
- 5000 XP = +10% damage
- 10000 XP = +20% damage
- 12500+ XP = +25% damage (cap)

Applied in STEP 3 (multiplicative)
Stacks with skill bonuses
```

### 3.3 Speed & Cooldowns

Combat uses a **hybrid speed + cooldown system**:

```
┌──────────────────────────────────────────────────────────────┐
│                    ACTION TIMING SYSTEM                      │
├──────────────────────────────────────────────────────────────┤
│                                                               │
│  CHARACTER SPEED (Determines action frequency)               │
│    Speed = FIN + ArmorSpeedPenalty                           │
│                                                               │
│    Armor Speed Penalties:                                    │
│    - Light Armor (10-20 lbs): -0 speed                      │
│    - Medium Armor (30-50 lbs): -2 speed                     │
│    - Heavy Armor (40-80 lbs): -5 speed                      │
│                                                               │
│    Example:                                                  │
│    FIN 12, Medium Armor (-2) = Speed 10                      │
│    FIN 15, Light Armor (0) = Speed 15                       │
│    FIN 10, Heavy Armor (-5) = Speed 5                        │
│                                                               │
│  ACTION COOLDOWN (Determins when next action ready)          │
│    Cooldown = BaseCooldown - (Speed × 0.1)                   │
│    Min Cooldown: 1 second                                    │
│                                                               │
│    Weapon Base Cooldowns:                                    │
│    - Dagger: 2.0s → Fast                                    │
│    - Shortsword: 2.5s                                       │
│    - Longsword: 3.0s                                        │
│    - Spear: 3.5s                                            │
│    - Greatsword: 5.0s → Slow                                │
│                                                               │
│    Example:                                                 │
│    Character with Speed 10 using Longsword (3.0s base)      │
│    Cooldown = 3.0 - (10 × 0.1) = 2.0 seconds per attack     │
│                                                               │
│    Same character, Speed 15:                                 │
│    Cooldown = 3.0 - (15 × 0.1) = 1.5 seconds per attack     │
│                                                               │
│  MULTI-CHARACTER SYNCHRONIZATION                             │
│    - All characters act independently                       │
│    - No fixed "turns" or initiative order                   │
│    - When cooldown complete, character acts immediately    │
│    - Player Control mode: Can pause to reposition/tactic    │
│                                                               │
└──────────────────────────────────────────────────────────────┘
```

**Speed vs Weapon Mismatch Examples**

```
Fast Character + Slow Weapon:
FIN 16 (+6 speed), Heavy Armor (-5) = Speed 11
Greatsword (5.0s cooldown)
Effective Cooldown: 5.0 - 1.1 = 3.9s
Result: Fewer attacks, but each hits HARD

Slow Character + Fast Weapon:
FIN 9 (-1 speed), Medium Armor (-2) = Speed 6
Dagger (2.0s cooldown)
Effective Cooldown: 2.0 - 0.6 = 1.4s (min 1.0s)
Result: Rapid attacks, but lower damage per hit
```

---

## 4. Defense Layers

### 4.1 Three-Layer System

All damage passes through three layers sequentially:

```
┌──────────────────────────────────────────────────────────────┐
│                    DEFENSE LAYER FLOW                        │
├──────────────────────────────────────────────────────────────┤
│                                                               │
│  Incoming Damage (26)                                        │
│      ↓                                                       │
│  ┌─────────────────┐                                        │
│  │ SHIELD LAYER    │ Absorbs damage first                   │
│  │ Current: 30/30  │                                        │
│  │ Absorbs: 26     │                                        │
│  │ Remaining: 4    │                                        │
│  └─────────────────┘                                        │
│      ↓ Overflow (4)                                          │
│  ┌─────────────────┐                                        │
│  │ ARMOR LAYER     │ Absorbs next                           │
│  │ Current: 45/50  │ (degrades with use)                    │
│  │ Absorbs: 4      │                                        │
│  │ Remaining: 41   │                                        │
│  └─────────────────┘                                        │
│      ↓ Overflow (0 - none)                                   │
│  ┌─────────────────┐                                        │
│  │ HEALTH LAYER    │ Last resort                            │
│  │ Current: 85/120 │                                        │
│  │ Damage: 0       │ (no overflow reached this layer)       │
│  │ Remaining: 85   │                                        │
│  └─────────────────┘                                        │
│                                                               │
│  RESULT: No health damage taken (shields + armor blocked)    │
│                                                               │
└──────────────────────────────────────────────────────────────┘
```

### 4.2 Shield Mechanics

**Two Shield Types**

| Type                | Source                      | Behavior                                    | Recovery                                                     |
| ------------------- | --------------------------- | ------------------------------------------- | ------------------------------------------------------------ |
| **Physical Shield** | Equipment (wood, metal)     | Absorbs damage, degrades, can break         | Does NOT regenerate, requires crafter repair                 |
| **Aura Shield**     | Skills, magic, enchantments | Reduces damage by flat amount, never breaks | Regenerates ~5% per round in combat, full in 1-2 minutes out |

**Stacking Rules**

```
Physical Shield + Aura Shield:
- Both stack additively
- Physical degrades first
- Aura remains at full strength until depleted

Example:
Physical Shield: 20 points (degrades)
Aura Shield: 10 points (regenerates)
Total Shield Pool: 30 points

Incoming Damage: 25
- Physical absorbs: 20 (degrades to 0/20, breaks)
- Aura absorbs: 5 (reduces to 5/10)
- Overflow: 0 (all damage blocked)
```

**Shield Regeneration**

| Context            | Aura Regen Rate     | Physical Shield                   |
| ------------------ | ------------------- | --------------------------------- |
| In combat          | ~5% per round       | No regen                          |
| Out of combat      | Full in 1-2 minutes | No regen                          |
| With shield skills | Boosted rate        | No regen                          |
| Repair (crafter)   | N/A                 | Restores to max (costs materials) |

### 4.3 Armor Degradation

Armor degrades when absorbing damage:

| Armor Type | Durability   | Degradation Rate           | Repair Cost | Crafter Required  |
| ---------- | ------------ | -------------------------- | ----------- | ----------------- |
| Light      | 50-150 hits  | 1 durability per 10 damage | 1-5 gold    | Leatherworker     |
| Medium     | 100-300 hits | 1 durability per 15 damage | 5-25 gold   | Armorsmith        |
| Heavy      | 200-500 hits | 1 durability per 25 damage | 50-250 gold | Master Armorsmith |

**Mid-Combat Break**

```
Armor durability reaches 0:
- Armor provides 0 protection
- Character becomes vulnerable
- Can continue fighting (naked)
- Must replace/repair after combat
```

**Quality Impact**

| Quality    | Durability Bonus | Repair Cost Multiplier |
| ---------- | ---------------- | ---------------------- |
| Common     | ×1.0             | ×1.0                   |
| Quality    | ×1.2             | ×1.2                   |
| Fine       | ×1.5             | ×1.5                   |
| Superior   | ×2.0             | ×2.0                   |
| Masterwork | ×3.0             | ×3.0                   |
| Legendary  | ×5.0             | ×5.0                   |

### 4.4 Health & Death

**Health Scaling**

| Source        | Health Gain                   |
| ------------- | ----------------------------- |
| Starting      | 80-120 (race-based)           |
| Per level-up  | +5-10 (random)                |
| Class bonus   | Some classes grant bonus HP   |
| Skill passive | Passive health boosts         |
| END modifier  | END affects maximum (see 2.1) |

**Death Mechanic (Soft Permadeath)**

```
Health reaches 0 → Character dies

EXCEPTIONS (Rare Legendary Skills):
- [Second Chance] (Rare): Once/day, survive killing blow at 1 HP
- [Undying Will] (Epic): Below 10%, brief invulnerability + damage boost
- [Phoenix Soul] (Legendary): Once ever, revive after death
- [Sacrifice] (Rare): Party member takes lethal damage for another

These skills trigger automatically when conditions met
Create dramatic "legendary moment" stories
```

**Death is Permanent**

- Dead character is gone
- Inventory drops (can be looted by allies or enemies)
- No automatic resurrection (unless high-level magic available)

---

## 5. Positioning & Range

### 5.1 Range-Based Auto-Positioning

Rows are **auto-determined by effective range** of character's loadout:

```
┌──────────────────────────────────────────────────────────────┐
│                    FRONT ROW (Melee Range)                   │
│  Short range weapons and abilities place character here:     │
│  - Melee weapons (daggers, swords, axes, maces)             │
│  - Thrown weapons (javelins, throwing axes)                  │
│  - Reach weapons can also stand in back row                  │
│                                                               │
│  [Pikeman] [Warrior] [Rogue] [Berserker]                    │
│                                                               │
├──────────────────────────────────────────────────────────────┤
│                    BACK ROW (Ranged/Caster)                  │
│  Long range weapons and abilities place character here:      │
│  - Bows, crossbows                                          │
│  - Magic spells (all schools)                                │
│  - Reach weapons (spears, polearms) can CHOOSE front or back │
│                                                               │
│  [Mage] [Archer] [Healer] [Cleric]                           │
│                                                               │
└──────────────────────────────────────────────────────────────┘

EXAMPLE PARTY FORMATION:
                        ┌─────────────────┐
                        │   ENEMY SIDE    │
                        │ [Front]  [Back] │
                        └─────────────────┘
                               ↑
┌──────────────────────────────────────────────────────────────┐
│ [Back Row]      [Front Row]  ||  [Enemy Front]  [Enemy Back]│
│   Archer        Warrior    ||     Goblin       Shaman       │
│   Mage          Fighter    ||     Orc          Archer       │
│   Healer        Paladin    ││                                │
│   Cleric        Pikeman*   ││  *Pikeman can reach from back │
└──────────────────────────────────────────────────────────────┘
```

### 5.2 Positioning Depth by Mode

| Positioning Feature | Background           | Spectator         | Player Control     |
| ------------------- | -------------------- | ----------------- | ------------------ |
| **Rows**            | Abstracted (no rows) | Front/Back rows   | Front/Back rows    |
| **Flanking Bonus**  | No                   | Yes               | Yes                |
| **Breakthrough**    | No                   | Yes               | Yes                |
| **Reach Advantage** | No                   | Yes               | Yes                |
| **Repositioning**   | No                   | No                | Yes (player order) |
| **Formations**      | Auto (simple)        | Auto (role-based) | Player-controlled  |

**Flanking Bonus** (Spectator + Player Control only)

```
When front row outnumbers enemy front row:
- +10% damage for all attackers
- Represents tactical advantage of surrounding enemies

Example:
Your Front Row: 4 characters
Enemy Front Row: 2 characters
Flanking Bonus: +10% damage for your side
```

**Breakthrough Mechanics** (Spectator + Player Control only)

```
Certain characters can "charge through" to back row:

Charger Types:
- Knight (mounted)
- Centaur (natural)
- Minotaur (racial)
- Berserker with [Charge] skill
- Flying creatures (natural)

Effect:
- Charger moves to enemy back row
- Can attack back row directly (bypassing front)
- Becomes priority target for enemy back row
- Must fight way back to own side (can get trapped)
```

**Reach Weapons** (Spectator + Player Control only)

```
Spears, polearms, lances:
- Can attack from back row (reach front)
- Can stand in front OR back row (player choice)
- Deal bonus damage vs charging enemies
- Vulnerable to flanking (immobile)
```

### 5.3 Range Rules

| Attack Type           | Can Target                  | Notes                      |
| --------------------- | --------------------------- | -------------------------- |
| Melee (standard)      | Enemy front only            | Must be in front row       |
| Melee (reach: spears) | Front from own back row     | Can stand in back          |
| Ranged                | Any row                     | No positioning restriction |
| Magic                 | Any row                     | No positioning restriction |
| AoE                   | Multiple targets, both rows | Affects all in radius      |

---

## 6. AI Targeting

### 6.1 Targeting by Mode

```
┌──────────────────────────────────────────────────────────────┐
│                    AI TARGETING SYSTEM                       │
├──────────────────────────────────────────────────────────────┤
│                                                               │
│  BACKGROUND MODE (Simple Aggro):                             │
│    - Target: Highest damage dealer OR nearest               │
│    - Switch target every 10 seconds                          │
│    - No tactical consideration                               │
│    - Fast, predictable, performance-focused                  │
│                                                               │
│  SPECTATOR MODE (Personality-Driven):                        │
│    - Target based on NPC personality traits                  │
│    - (See Section 6.2: Personality Targeting)                │
│    - Rich simulation, ties into NPC Core Systems             │
│                                                               │
│  PLAYER CONTROL MODE (Role-Based):                           │
│    - Player's party: Role-based targeting (predictable)      │
│    - Enemy NPCs: Personality-based (if uncontrolled)         │
│    - (See Section 6.3: Role-Based Targeting)                 │
│                                                               │
└──────────────────────────────────────────────────────────────┘
```

### 6.2 Personality-Driven Targeting (Spectator Mode)

NPCs target based on personality traits from NPC Core Systems:

| Personality            | Target Priority                       | Example                                                   |
| ---------------------- | ------------------------------------- | --------------------------------------------------------- |
| **Brave/Honest**       | High-threat enemies, demand surrender | Paladin attacks strongest enemy, offers surrender to weak |
| **Cowardly/Dishonest** | Lowest health (easy kills)            | Goblin targets wounded PCs, flees if outmatched           |
| **Impulsive**          | Nearest enemy                         | Barbarian charges closest target immediately              |
| **Deliberative**       | High-threat after assessment          | Tactician mage waits, then assaults enemy healer          |
| **Opportunistic**      | Vulnerable targets (low HP, stunned)  | Rogue waits for openings, then strikes                    |
| **Cautious**           | Keep distance, avoid danger           | Archer maintains range, kites melee enemies               |
| **Protective**         | Enemies threatening guarded target    | Bodyguard intercepts attacks on ward                      |

**Integration with NPC Core Systems**

```
Each NPC has personality traits from NPC Core Systems GDD:
- Bravery (Brave → Cowardly spectrum)
- Honesty (Honest → Dishonest spectrum)
- Impulsiveness (Impulsive → Deliberative spectrum)
- Etc.

Combat targeting uses these traits to drive decisions
```

### 6.3 Role-Based Targeting (Player Control Mode)

Player's party uses predictable role-based targeting:

| Role                  | Target Priority                          | Behavior                                                           |
| --------------------- | ---------------------------------------- | ------------------------------------------------------------------ |
| **Tank** (high armor) | Highest-threat enemies                   | Protects squishies, draws aggro                                    |
| **Melee DPS**         | Low-HP enemies (secure kills)            | Focuses damage, tactical targeting                                 |
| **Ranged DPS**        | Any target (no restriction)              | Flexible, follows player marks                                     |
| **Healer** (only one) | Party members only                       | Priority: Prevent death → Maintain efficiency → Conserve resources |
| **Healer** (multiple) | Split front/back                         | One heals front, one heals back                                    |
| **Mage**              | AoE: multiple targets, or highest threat | Burst damage, or control                                           |

**Healer AI Priority**

```
PRIORITY 1: Prevent Death
  └─ Can die from single attack? → Heal immediately
  └─ Below 25% HP? → Heal soon

PRIORITY 2: Maintain Effectiveness
  └─ Keep damage dealers above 50% HP
  └─ Don't overheal (waste of resources)

PRIORITY 3: Resource Efficiency
  └─ Cooldown skills before consumables
  └─ Rechargeable before limited items

OUT OF COMBAT:
  └─ Use free/rechargeable healing first
  └─ Target 100% HP for all party members
  └─ Recover own mana before next fight
```

---

## 7. Damage Types & Resistances

### 7.1 Six Damage Types

Full damage type system creates tactical depth:

| Damage Type  | Sources                        | Vulnerable Examples      | Resistant Examples           |
| ------------ | ------------------------------ | ------------------------ | ---------------------------- |
| **Physical** | Swords, axes, arrows, fists    | Unarmored creatures      | Heavy armor, golems          |
| **Fire**     | Torches, fire magic, dragons   | Ice creatures, trolls    | Fire elementals, red dragons |
| **Cold**     | Ice magic, environment         | Fire creatures, demons   | Ice elementals, yetis        |
| **Poison**   | Venom, traps, poisoned weapons | Healthy creatures        | Undead, constructs           |
| **Electric** | Lightning magic                | Metal-armored foes       | Electric elementals          |
| **Arcane**   | Pure magic                     | Most creatures (neutral) | Magic-resistant creatures    |

### 7.2 Resistance System

**Resistance Values**

```
Resistance Range: -100% to +100%
-100%: Vulnerable (2× damage)
0%: Neutral (normal damage)
+50%: Resistant (half damage)
+100%: Immune (zero damage)
>+100%: Heals from damage type

Examples:
Fire Elemental: Fire +100% (immune), Cold -50% (vulnerable)
Ice Golem: Cold +100% (immune), Fire -100% (2× damage)
Undead: Poison +100% (immune), Physical +20% (resistant)
```

**Damage Formula with Resistance**

```
Final Damage = Base Damage × (1 - Resistance)

Example:
Fireball: 50 damage
Target: Ice Golem (Fire -100% resistance)

Final Damage = 50 × (1 - (-1.0))
            = 50 × 2.0
            = 100 damage (2× effectiveness)

Target: Fire Elemental (Fire +100% resistance)

Final Damage = 50 × (1 - 1.0)
            = 50 × 0
            = 0 damage (immune)
```

### 7.3 Tactical Implications

```
Player Choices:
- Equip fire weapons vs ice creatures (2× damage)
- Use poison vs healthy targets (ineffective vs undead)
- Switch damage types based on enemy resistances
- Craft elemental weapons for specific scenarios

Creature Variety:
- Elementals (immune to own element, vulnerable to opposite)
- Undead (poison immune, physical resistant)
- Constructs (poison immune, magic vulnerable)
- Beasts (no special resistances, physical neutral)
```

---

## 8. Status Effects

### 8.1 Full Status System

| Status        | Effect                                         | Duration             | Source                           | Cure                  |
| ------------- | ---------------------------------------------- | -------------------- | -------------------------------- | --------------------- |
| **Stun**      | Skip next action, -10 defense                  | 1 tick/attack        | Physical trauma, some skills     | Time, ally aid        |
| **Poison**    | DOT: 5 damage/tick, -2 END                     | 5 ticks              | Poisoned weapons, creature bites | Antidote, healer      |
| **Burn**      | Fire DOT: 8 damage/tick, -10% defense          | 3 ticks              | Fire magic, torches              | Water, heal magic     |
| **Freeze**    | Can't act, vulnerable to shatter (2× physical) | Until hit or 3 ticks | Ice magic                        | Fire damage, ally aid |
| **Slow**      | -5 speed, -20% attack speed                    | 5 ticks              | Ice magic, webs                  | Time, haste effect    |
| **Silence**   | Cannot cast spells                             | 3 ticks              | Certain magic, skills            | Time                  |
| **Bleed**     | Physical DOT: 3 damage/tick, stacks            | 3 stacks max         | Slashing weapons                 | Bandages, healer      |
| **Knockdown** | Prone, -20 defense, must stand up (1 action)   | 1 tick               | Powerful blows, skills           | Stand up action       |
| **Sleep**     | Cannot act, wake on damage                     | Until damaged        | Sleep magic, certain skills      | Damage (any)          |
| **Confusion** | 50% chance: attack wrong target (ally or self) | 3 ticks              | Mind magic                       | Time, protection      |

**Stacking Rules**

```
Most effects: Duration refreshes (no stacking)
Exception: Bleed (stacks up to 3× damage)
Exception: Poison (different types can stack)

Example:
Poisoned (stack 1): 5 damage/tick, 5 ticks
Hit with new poison: Refreshes to 5 ticks (no damage increase)

Bleeding (stack 1): 3 damage/tick
Hit again: Bleeding (stack 2): 6 damage/tick
Hit again: Bleeding (stack 3): 9 damage/tick (max)
```

### 8.2 Status Application

```
On Hit:
  IF attacker has status effect on weapon/spell:
    Roll chance to apply (varies by status)
    IF success:
      Check target resistance (if applicable)
      IF resistance check fails:
        Apply status to target

Example:
Attacker: Poisoned Dagger (+20% poison chance)
Target: Creature (poison resistance 20%)

Roll: 20% chance
Random roll: 45 (fail, no poison)

Next attack:
Random roll: 15 (success!)
Target resist check: 20% resist
Random roll: 65 (fail resist check)
Status applied: Poisoned (5 ticks, 5 damage/tick)
```

### 8.3 Status Effects by Mode

| Status Feature         | Background  | Spectator         | Player Control  |
| ---------------------- | ----------- | ----------------- | --------------- |
| **All statuses**       | Full system | Full system       | Full system     |
| **Visual feedback**    | None        | Animations, icons | Full UI, icons  |
| **Tactical decisions** | Auto (AI)   | Auto (AI)         | Manual (player) |

---

## 9. Combat End Conditions

### 9.1 Context-Aware Endings

```
┌──────────────────────────────────────────────────────────────┐
│                   COMBAT END CONDITIONS                      │
├──────────────────────────────────────────────────────────────┤
│                                                               │
│  BOSS BATTLES / IMPORTANT FIGHTS                             │
│    → Total Elimination                                        │
│      Fight until one side has zero conscious characters      │
│      No morale, no surrender (high stakes)                   │
│      Dead enemies drop valuable loot                         │
│                                                               │
│  RANDOM ENCOUNTERS                                           │
│    → Morale Break System                                      │
│      Each death/fall causes morale check                     │
│      Broken side flees (survives, no loot)                  │
│      Faster resolution, fewer TPKs                           │
│                                                               │
│  OUTMATCHED FIGHTS (Player 3× stronger)                      │
│    → Surrender System                                         │
│      Weaker side can surrender (personality-based)           │
│      Player can accept (spare lives, maybe recruit)          │
│      Or reject (finish combat, take loot)                    │
│                                                               │
│  NPC-ONLY COMBAT (Background mode)                           │
│    → Statistical Resolution                                   │
│      Power comparison, no tick-by-tick simulation            │
│      Instant outcome, no loot (simplified)                  │
│                                                               │
└──────────────────────────────────────────────────────────────┘
```

### 9.2 Morale Break System

**Morale Calculation**

```
Base Morale: 100% (full)

Morale Losses:
- Ally dies: -25% morale
- Ally falls (0 HP, survives): -10% morale
- Ally flees: -15% morale
- Character is below 25% HP: -5% morale per turn

Morale Checks:
Trigger: After each death/fall
Check: Roll d100 vs Current Morale
Success: Combat continues
Failure: Side breaks, all flee

Example:
Party of 4, 1 dies (75% morale)
2nd dies (50% morale)
Morale check: Roll 50 vs 50 = tied (continue)
3rd dies (25% morale)
Morale check: Roll 82 vs 25 = FAIL
Side breaks, survivors flee
```

**Personality Influence**

```
Brave characters: +20% morale (harder to break)
Cowardly characters: -20% morale (easier to break)
Leadership (CHA): +1% morale per CHA above 10
```

### 9.3 Surrender System

**Surrender Triggers**

```
When one side is outmatched (Power Ratio < 0.5):
Weaker side makes surrender decision (personality-based)

Surrender Chance = Base Chance + Personality Modifier

Base Chance: 50% (if outmatched 2:1)
Personality Modifiers:
- Brave: -30% (less likely to surrender)
- Cowardly: +30% (more likely to surrender)
- Honest: -10% (fights to the end)
- Dishonest: +10% (pragmatic surrender)
```

**Player Choice**

```
When enemy surrenders:
┌────────────────────────────────┐
│  ENEMY SURRENDERS               │
│  [Goblin Scout] begs for mercy │
│                                │
│  [Accept] - Spare lives        │
│    → May recruit (CHA check)   │
│    → Gain reputation bonus     │
│    → No loot                   │
│                                │
│  [Reject] - Finish combat      │
│    → Take all loot            │
│    → -5 reputation             │
│    → Enemies fight to death    │
└────────────────────────────────┘
```

---

## 10. Skill Activation in Combat

### 10.1 Activation by Mode

| Mode               | Skill Activation                | Player Control       |
| ------------------ | ------------------------------- | -------------------- |
| **Background**     | AI auto-uses (priority system)  | None                 |
| **Spectator**      | AI auto-uses (priority system)  | None (watching only) |
| **Player Control** | Player chooses when to activate | Full manual control  |

### 10.2 AI Skill Priority (Background & Spectator)

```
┌──────────────────────────────────────────────────────────────┐
│                   AI SKILL PRIORITY SYSTEM                    │
├──────────────────────────────────────────────────────────────┤
│                                                               │
│  Priority 1: Survival Skills                                 │
│    └─ Health below 25%? → Use heal/shield skill             │
│    └─ About to take lethal damage? → Use defense skill       │
│                                                               │
│  Priority 2: Buff Skills (at start or when expire)           │
│    └─ Combat starts → Use [Berserker Rage], [Protective Aura]│
│    └─ Buff expires → Re-cast if affordable                  │
│                                                               │
│  Priority 3: Damage Skills (when available)                  │
│    └─ High-damage skills off cooldown → Use                 │
│    └─ Multiple targets? → Use AoE skills                     │
│                                                               │
│  Priority 4: Utility Skills (situational)                    │
│    └─ Enemy fleeing? → Use [Cleave], [Snipe]                │
│    └─ Need reposition? → Use [Charge], [Retreat]            │
│                                                               │
│  Resource Management:                                        │
│    - AI refuses to use skill if stamina < 30% (reserve)      │
│    - AI refuses to use spell if mana < 20% (reserve)         │
│    - Exception: Survival skills ignore reserve              │
│                                                               │
└──────────────────────────────────────────────────────────────┘
```

### 10.3 Player Control Activation

**Manual Skill Use**

```
Player controls one possessed character directly:
- Click skill icon → Activate immediately
- Press hotkey (1-5 for quick slots) → Activate
- Right-click skill → Toggle auto-use (let AI handle)

Quick Slots (5 total):
- Slot 1-5: Player-configured skills/items
- Auto-use toggle per slot
- Priority order (if auto-use enabled)

Example Setup:
Slot 1: [Power Strike] - Auto ON (use when available)
Slot 2: [Heal] - Auto OFF (manual only)
Slot 3: [Fireball] - Auto ON
Slot 4: Health Potion - Auto ON when HP < 25%
Slot 5: [Shield Bash] - Auto OFF (manual only)
```

**Resource Awareness**

```
Player chooses when to spend resources:
- High-stamina skills → Use early (before fatigue)
- Low-mana spells → Conserve for emergencies
- Burst damage → Save for critical moments

AI respects player's manual choice:
- Player uses skill manually → AI won't override
- Player toggles auto ON → AI uses when appropriate
```

---

## 11. Combat UI Design

### 11.1 UI Philosophy

**Mobile/Web-First Visual Design**

The combat UI is designed primarily for **mobile and web platforms** with the following principles:

| Principle                  | Description                                                                                                                    |
| -------------------------- | ------------------------------------------------------------------------------------------------------------------------------ |
| **Visual-First**           | Combat displayed through character portraits, health bars, skill icons, and animations. Minimal text in main view.             |
| **Contextual Information** | Detailed combat logs, damage numbers, and status effects available via context menus, tooltips, and tap-and-hold interactions. |
| **Touch-Optimized**        | Large tap targets, swipe gestures for party selection, drag-and-drop for skill configuration.                                  |
| **At-a-Glance Readable**   | Health bars, status icons, and cooldown indicators visible without tapping.                                                    |
| **Progressive Disclosure** | Simple view by default, detailed information available on demand.                                                              |

**Information Hierarchy**

```
ALWAYS VISIBLE (Primary View):
  - Character portraits with class icons
  - Health bars (percentage + current/max on tap)
  - Status effect icons (poison, stun, etc.)
  - Skill quick slots with cooldown indicators
  - Combat timer / tick counter

ON TAP (Secondary View):
  - Detailed health/stamina/mana numbers
  - Active buffs/debuffs with durations
  - Damage numbers from last action
  - Equipment condition

ON TAP-AND-HOLD / CONTEXT MENU (Tertiary View):
  - Full combat log (scrollable)
  - Character stats sheet
  - Skill descriptions
  - Equipment details
```

### 11.2 Spectator Mode UI (Mobile/Web)

**Primary View (Visual-First)**

```
┌─────────────────────────────────────────────────────────────────┐
│  ⚔️ COMBAT                                    [⋯] [Pause] [×]    │
├─────────────────────────────────────────────────────────────────┤
│                                                                   │
│                    ┌─────────────────────┐                      │
│                    │   YOUR PARTY        │                      │
│                    │                     │                      │
│  ┌────────────────┐ │ ┌─────┐ ┌─────┐    │    ┌─────────────┐  │
│  │ 🛡️ Marcus      │ │ │🔥   │ │💚   │    │    │ ENEMY PARTY │  │
│  │ ████████░░ 85% │ │ │Elena│ │Kira │    │    │             │  │
│  │ [⚡⚡⚡⚡⚡]      │ │ └─────┘ └─────┘    │    │ ┌─────────┐ │  │
│  │ [⏱️ 1.2s]      │ │                     │    │ │⚔️ Goblin│ │  │
│  └────────────────┘ │ ┌─────┐ ┌─────┐    │    │ │█████░░  │ │  │
│  [Tap for details]  │ │⚔️   │ │🎯   │    │    │ │Warrior  │ │  │
│                     │ │Toren│ │???  │    │    │ └─────────┘ │  │
│  Tap character to   │ └─────┘ └─────┘    │    │ ┌─────────┐ │  │
│  possess ↓          │                     │    │ │⚔️ Goblin│ │  │
│                     │ [Tap any portrait]  │    │ │██░░░░░  │ │  │
│  ┌─────────────────┐│                     │    │ │Spearman │ │  │
│  │ 💚 HEALING     ││                     │    │ └─────────┘ │  │
│  │ Elena casts    ││                     │    │ ┌─────────┐ │  │
│  │ Heal on Marcus!││                     │    │ │📜 Goblin│ │  │
│  │ +18 HP         ││                     │    │ │██░░░░░  │ │  │
│  └─────────────────┘│                     │    │ │Shaman   │ │  │
│                     │                     │    │ └─────────┘ │  │
│                     └─────────────────────┘                      │
│                                                                   │
│  Quick Skills: [⚔️] [🛡️] [🔥] [💨] [🧪]                            │
│                                                                   │
│  Tick: 23  │  Time: 00:45  │  [Full Log →]                      │
└─────────────────────────────────────────────────────────────────┘

LEGEND:
🛡️ = Tank class icon
⚔️ = Warrior/Fighter class icon
🔥 = Mage/Caster class icon
💚 = Healer class icon
🎯 = Ranger/Ranged class icon
███████░░ = Health bar (colored: green > yellow > red)
[⚡⚡⚡⚡⚡] = Stamina segments (5/5 full)
[⏱️ 1.2s] = Cooldown until next action
```

**Context Menu (Tap-and-Hold Character)**

```
┌─────────────────────────────────────────────────────────────────┐
│  Marcus - Level 8 Warrior                              [×] Close │
├─────────────────────────────────────────────────────────────────┤
│                                                                   │
│  ┌─────────────────────────────────────────────────────────────┐ │
│  │  STATS                                                       │ │
│  ├─────────────────────────────────────────────────────────────┤ │
│  │  HP:    85/120  ████████░░ (71%)                            │ │
│  │  STAM:  75/90   ███████░░░ (83%)                            │ │
│  │  MANA:  0/0     (none)                                      │ │
│  │                                                               │ │
│  │  STR 14 (+9% melee)   FIN 11 (+1%)                          │ │
│  │  END 13 (+15% HP)    WIT 9 (-3% mana)                       │ │
│  │  AWR 10 (+5% init)    CHA 8 (-5% prices)                    │ │
│  └─────────────────────────────────────────────────────────────┘ │
│                                                                   │
│  ┌─────────────────────────────────────────────────────────────┐ │
│  │  EQUIPMENT                                                   │ │
│  ├─────────────────────────────────────────────────────────────┤ │
│  │  ⚔️ Iron Longsword    (12-18 dmg, 3.0s speed)              │ │
│  │  🛡️ Chain Shirt       (45/50 durability)                   │ │
│  │  💍 Iron Ring         (+1 FIN)                              │ │
│  └─────────────────────────────────────────────────────────────┘ │
│                                                                   │
│  ┌─────────────────────────────────────────────────────────────┐ │
│  │  ACTIVE EFFECTS                                              │ │
│  ├─────────────────────────────────────────────────────────────┤ │
│  │  [Sword Mastery] +12% damage (passive)                      │ │
│  │  combat.melee.sword 3500 XP → +7% damage                    │ │
│  └─────────────────────────────────────────────────────────────┘ │
│                                                                   │
│  ┌─────────────────────────────────────────────────────────────┐ │
│  │  COMBAT LOG (Last 5 actions)                  [Full Log →]  │ │
│  ├─────────────────────────────────────────────────────────────┤ │
│  │  [14:32:09] Used [Power Strike] → 32 damage                 │ │
│  │  [14:32:07] Hit by Spearman → 12 blocked, 2 taken          │ │
│  │  [14:32:05] Hit Warrior → 18 damage                         │ │
│  │  [14:32:03] Used [Power Strike] → 35 damage                 │ │
│  │  [14:32:01] Hit by Shaman → 8 damage                        │ │
│  └─────────────────────────────────────────────────────────────┘ │
│                                                                   │
│  [Possess Marcus] [View Full Sheet]                               │
└─────────────────────────────────────────────────────────────────┘
```

**Full Combat Log (Separate Screen/Modal)**

```
┌─────────────────────────────────────────────────────────────────┐
│  📜 COMBAT LOG                                      [←] [Auto] │
├─────────────────────────────────────────────────────────────────┤
│                                                                   │
│  Filters: [All] [Damage] [Healing] [Status] [Skills]            │
│                                                                   │
│  ┌─────────────────────────────────────────────────────────────┐ │
│  │  14:32:11  Elena (Mage) casts [Fireball]                    │ │
│  │            → 45 fire damage to Goblin Warrior               │ │
│  │            → 38 fire damage to Goblin Spearman              │ │
│  │            → 52 fire damage to Goblin Shaman                │ │
│  │            (3 enemies hit!)                                 │ │
│  │                                                               │ │
│  │  14:32:09  Marcus (Warrior) uses [Power Strike]             │ │
│  │            → 32 physical damage to Goblin Spearman          │ │
│  │            (CRITICAL! +50% damage)                          │ │
│  │                                                               │ │
│  │  14:32:07  Goblin Spearman attacks Marcus                   │ │
│  │            → 12 damage blocked by shield (6/30 remaining)  │ │
│  │            → 2 damage to armor (47→45 durability)           │ │
│  │            → 0 damage to health                             │ │
│  │                                                               │ │
│  │  14:32:05  Marcus (Warrior) attacks Goblin Warrior          │ │
│  │            → 18 physical damage                             │ │
│  │            → Goblin Warrior HP: 45 → 27                     │ │
│  │                                                               │ │
│  │  14:32:03  Goblin Shaman casts [Poison Cloud]                │ │
│  │            → Kira (Healer) poisoned! 5 dmg/tick for 5 ticks │ │
│  │            → Toren (Ranger) poisoned! 5 dmg/tick for 5 ticks│ │
│  │                                                               │ │
│  │  14:32:01  Kira (Healer) casts [Heal] on Marcus             │ │
│  │            → +18 HP restored                                 │ │
│  │            → Marcus HP: 67 → 85                             │ │
│  │                                                               │ │
│  │  [Scroll to top...]                                          │ │
│  └─────────────────────────────────────────────────────────────┘ │
│                                                                   │
│  [Export Log] [Share] [Close]                                     │
└─────────────────────────────────────────────────────────────────┘
```

### 11.2 Player Control Mode UI (Mobile/Web)

**Primary View (Possessing Marcus)**

```
┌─────────────────────────────────────────────────────────────────┐
│  ⚔️ COMBAT - Your Turn                            [Exit] [⋯]   │
├─────────────────────────────────────────────────────────────────┤
│                                                                   │
│  ┌─────────────────────┐    ┌─────────────────────────────────┐ │
│  │   YOUR PARTY        │    │   ENEMY PARTY                  │ │
│  │                     │    │                                 │ │
│  │  ┌────────────────┐ │    │  ┌─────────┐  Tap to target     │
│  │  │ 🎮 Marcus      │ │    │  │ ⚔️ Goblin│ ↓                 │
│  │  │ ████████░░ 85% │ │    │  │ ██████░ │                    │
│  │  │ [⚡⚡⚡⚡⚡]      │ │    │  │ Warrior  │ 45/60           │
│  │  │ YOUR TURN      │ │    │  └─────────┘                    │
│  │  └────────────────┘ │    │                                 │
│  │  [ACTION SELECTED]  │    │  ┌─────────┐                    │
│  │                     │    │  │ ⚔️ Goblin│                    │
│  │  ┌─────┐ ┌─────┐    │    │  │ ██░░░░░ │                    │
│  │  │ 🔥   │ │ 💚   │    │    │  │Spearman │ 22/50 ← WOUNDED │
│  │  │Elena│ │Kira  │    │    │  └─────────┘                    │
│  │  │█████░│ │██████│    │    │                                 │
│  │  └─────┘ └─────┘    │    │  ┌─────────┐                    │
│  │                     │    │  │ 📜 Goblin│                    │
│  │  ┌─────┐ ┌─────┐    │    │  │ ██████░ │                    │
│  │  │ ⚔️   │ │ 🎯   │    │    │  │ Shaman  │ 35/45            │
│  │  │Toren │ │ ???  │    │    │  └─────────┘                    │
│  │  │████░░│ │      │    │    │                                 │
│  │  └─────┘ └─────┘    │    │  ← Tap portrait to target      │
│  │                     │    │                                 │
│  └─────────────────────┘    └─────────────────────────────────┘ │
│                                                                   │
│  ┌─────────────────────────────────────────────────────────────┐ │
│  │  QUICK SKILLS (Marcus)              Drag to reorder         │ │
│  ├─────────────────────────────────────────────────────────────┤ │
│  │  ┌─────────┬─────────┬─────────┬─────────┬─────────┐       │ │
│  │  │ ⚔️      │ 🛡️      │ 🌀      │ 💨      │ 🧪      │       │ │
│  │  │ Power   │ Shield  │ Whirl   │ Charge  │ Health  │       │ │
│  │  │ Strike  │ Bash    │ wind    │         │ Potion  │       │ │
│  │  │         │         │         │         │         │       │ │
│  │  │ ⚡ 15    │ ⚡ 10    │ ⚡ 25    │ ⚡ 8     │ ∞       │       │ │
│  │  │ READY   │ 2.3s    │ READY   │ 1.1s    │ READY   │       │ │
│  │  └─────────┴─────────┴─────────┴─────────┴─────────┘       │ │
│  │                                                           │ │
│  │  Selected: [Power Strike] → Targets [Goblin Warrior]       │ │
│  └─────────────────────────────────────────────────────────────┘ │
│                                                                   │
│  [CONFIRM ACTION]                                  [Cancel]     │
│                                                                   │
│  [Pause] [Speed: ▮▮▮▯▯] [Orders] [Flee]                           │
└─────────────────────────────────────────────────────────────────┘

TAP TARGETS:
  - Tap enemy portrait to select target
  - Tap ally portrait to see context menu (defend, follow, etc.)

GESTURES:
  - Swipe left/right on skills → Reorder quick slots
  - Swipe up on skill → Show skill description
  - Tap-and-hold enemy → Show enemy stats + resistances

ACTION FEEDBACK:
  - Successful attack → Screen shake + damage number popup
  - Critical hit → Larger damage number + "CRITICAL!" text
  - Kill → Enemy portrait fades out + plays death animation
  - Status effect → Icon appears on portrait with duration
```

**Context Menu (Tap-and-Hold Ally)**

```
┌─────────────────────────────────────────────────────────────────┐
│  Elena - Level 7 Mage                                   [×]    │
├─────────────────────────────────────────────────────────────────┤
│                                                                   │
│  HP: 60/80  ████████░░  STAM: 50/60  MANA: 65/70                 │
│                                                                   │
│  ┌─────────────────────────────────────────────────────────────┐ │
│  │  ORDERS (Tap to assign)                                     │ │
│  ├─────────────────────────────────────────────────────────────┤ │
│  │  [⚔️ Attack Nearest]   [🎯 Target Weakest]                │ │
│  │  [💚 Heal Priority]    [🛡️ Defend Marcus]                 │ │
│  │  [💨 Keep Distance]    [🔥 Focus Fire]                    │ │
│  └─────────────────────────────────────────────────────────────┘ │
│                                                                   │
│  ┌─────────────────────────────────────────────────────────────┐ │
│  │  ACTIVE SKILLS (Toggle Auto-Use)                            │ │
│  ├─────────────────────────────────────────────────────────────┤ │
│  │  [🔥 Fireball]      Auto: [ON]  [OFF]                      │ │
│  │  [❄️ Ice Spike]      Auto: [ON]  [OFF]                      │ │
│  │  [🛡️ Shield]         Auto: [ON]  [OFF]                      │ │
│  │  [💧 Mana Potion]    Auto: [ON]  [OFF] (when < 20%)         │ │
│  └─────────────────────────────────────────────────────────────┘ │
│                                                                   │
│  [View Full Sheet] [Take Control]                                │
└─────────────────────────────────────────────────────────────────┘
```

### 11.3 Multiplayer Turn-Based UI (Mobile/Web)

**Primary View (Your Turn, Waiting for Others)**

```
┌─────────────────────────────────────────────────────────────────┐
│  ⚔️ MULTIPLAYER COMBAT                    [Exit] [Chat] [⋯]    │
├─────────────────────────────────────────────────────────────────┤
│                                                                   │
│  TURN: 15  │  YOUR TURN  │  Time: 0:28 remaining                 │
│                                                                   │
│  ┌─────────────────────────────────────────────────────────────┐ │
│  │   OTHER PLAYERS                                              │ │
│  ├─────────────────────────────────────────────────────────────┤ │
│  │  ┌────────────────┐  ┌────────────────┐                    │ │
│  │  │ 👤 Sarah       │  │ 👤 Mike        │                    │ │
│  │  │ ⏳ Thinking... │  │ ✅ Ready       │                    │ │
│  │  │ (Elena)        │  │ (Toren)        │                    │ │
│  │  └────────────────┘  └────────────────┘                    │ │
│  │                                                               │ │
│  │  💬 Chat: 3 new messages                                     │ │
│  └─────────────────────────────────────────────────────────────┘ │
│                                                                   │
│  ┌─────────────────────┐    ┌─────────────────────────────────┐ │
│  │   YOUR PARTY        │    │   ENEMY PARTY                  │ │
│  │                     │    │                                 │ │
│  │  ┌────────────────┐ │    │  Tap enemies to target ↓        │ │
│  │  │ 🎮 Marcus      │ │    │                                 │
│  │  │ ████████░░ 85% │ │    │  ┌─────────┐                    │
│  │  │ [⚡⚡⚡⚡⚡]      │ │    │  │ ⚔️ Goblin│                    │
│  │  │ YOUR TURN      │ │    │  │ ██████░ │                    │
│  │  └────────────────┘ │    │  │ Warrior  │                    │
│  │                     │    │  └─────────┘                    │
│  │  ┌─────┐ ┌─────┐    │    │                                 │
│  │  │ 🔥   │ │ 💚   │    │    │  (AI-controlled enemies)         │
│  │  │Elena│ │Kira  │    │    │                                 │
│  │  │█████░│ │██████│    │    │  ┌─────────┐                    │
│  │  └─────┘ └─────┘    │    │  │ 📜 Goblin│                    │
│  │                     │    │  │ ██████░ │                    │
│  │  ┌─────┐ ┌─────┐    │    │  │ Shaman  │                    │
│  │  │ ⚔️   │ │ 🎯   │    │    │  └─────────┘                    │
│  │  │Toren │ │ ???  │    │    │                                 │
│  │  │████░░│ │      │    │    │  ← Tap to target                │
│  │  └─────┘ └─────┘    │    │                                 │
│  │                     │    │                                 │
│  └─────────────────────┘    └─────────────────────────────────┘ │
│                                                                   │
│  ┌─────────────────────────────────────────────────────────────┐ │
│  │  CHOOSE ACTION (Swipe skills for more)                      │ │
│  ├─────────────────────────────────────────────────────────────┤ │
│  │  [⚔️ Attack]    [💨 Skill]     [🎯 Items]                   │ │
│  │                                                               │ │
│  │  Target: [Goblin Warrior] ← Selected                         │ │
│  └─────────────────────────────────────────────────────────────┘ │
│                                                                   │
│  [END TURN]                                                      │
│                                                                   │
│  ─────────────────────────────────────────────────────────────  │
│  💬 Sarah: "Focus fire on the Shaman first!"                     │
│  💬 Mike: "I can take the Warrior if you handle the Shaman"     │
└─────────────────────────────────────────────────────────────────┘

WHEN IT'S NOT YOUR TURN:
  - Screen dims slightly
  - "Waiting for other players..." message
  - Can still view character sheets via tap-and-hold
  - Can use chat to coordinate

TURN NOTIFICATIONS:
  - Push notification when turn starts
  - Vibration + sound effect (if enabled)
  - 30-second timer to take action
  - Auto-pass if no action (can be disabled in settings)
```

### 11.4 Combat Animations & Visual Feedback

**Mobile/Web-First Animation System**

| Action             | Visual Feedback                                                                    | Duration |
| ------------------ | ---------------------------------------------------------------------------------- | -------- |
| **Basic Attack**   | Character lunges toward target, weapon swing animation, damage number popup        | 0.5s     |
| **Critical Hit**   | Larger damage number, "CRITICAL!" text, screen shake, flash effect                 | 0.8s     |
| **Spell Cast**     | Casting animation (glowing, gestures), projectile travels to target, impact effect | 1.0-1.5s |
| **Heal**           | Green cross/sparkle effect on target, "+XX HP" floats up                           | 0.8s     |
| **Status Applied** | Icon appears on portrait with brief animation (poison drip, freeze shatter)        | 0.5s     |
| **Death**          | Character portrait fades to gray, falls over animation, disappears from formation  | 1.0s     |
| **Shield Break**   | Shield icon cracks, shatters, disappears                                           | 0.6s     |
| **Armor Break**    | Armor icon shows cracks, damage number                                             | 0.4s     |

**Damage Number Colors**

```
WHITE  = Physical damage
ORANGE = Critical hit
RED    = Fire damage
BLUE   = Cold/Ice damage
GREEN  = Poison damage
YELLOW = Electric/Lightning damage
PURPLE = Arcane/Magic damage
PINK   = Healing
GRAY   = Blocked/Absorbed (0 damage after defense)
```

**Status Effect Icons (Portrait Overlays)**

```
☠️ = Poison (green drip)
💤 = Sleep (Zzz animation)
💫 = Stun (stars spinning)
❄️ = Freeze (ice crystals)
🔥 = Burn (flames)
⏱️ = Slow (clock icon)
🚫 = Silence (prohibited sign)
🩸 = Bleed (red drops)
```

**Performance Considerations**

- **Mobile Devices**: Limit particle effects, use sprite animations instead of 3D
- **Low-End Mode**: Toggle to disable animations, show damage numbers only
- **Battery Saver**: Reduce frame rate, simplify effects
- **Data Saver**: Disable spell projectile animations, instant effects only

```
┌─────────────────────────────────────────────────────────────────┐
│  IDLE WORLD - Multiplayer Combat (Turn-Based)                   │
├─────────────────────────────────────────────────────────────────┤
│                                                                   │
│  TURN: 15  │  YOUR TURN  │  Time Remaining: 0:28                  │
│                                                                   │
│  ┌─────────────────────────────────────────────────────────────┐ │
│  │   OTHER PLAYERS                                               │ │
│  ├─────────────────────────────────────────────────────────────┤ │
│  │  [Player2] Sarah → Thinking...                               │ │
│  │  [Player3] Mike → Ready (waiting for you)                    │ │
│  └─────────────────────────────────────────────────────────────┘ │
│                                                                   │
│  YOUR PARTY (Possessed: Marcus)                                   │
│  ┌─────────────────────────────────────────────────────────────┐ │
│  │ [🎮] Marcus 85/120  STAM: 75/90  MANA: 0/0                   │ │
│  │                                                                  │
│  │  ACTION: Choose action for this turn                           │ │
│  │                                                                  │
│  │  ┌─────────────┬─────────────┬─────────────┐                 │
│  │  │ [Attack]    │ [Skill]     │ [Item]      │                 │
│  │  │ Goblin      │ ▼           │ ▼           │                 │
│  │  │ Warrior     │ (3 skills)  │ (2 items)   │                 │
│  │  └─────────────┴─────────────┴─────────────┘                 │
│  └─────────────────────────────────────────────────────────────┘ │
│                                                                   │
│  COMBAT LOG:                                                      │
│  [Turn 14] Mike's Toren cast [Lightning Bolt] → 38 damage       │
│  [Turn 14] Goblin Warrior attacks Marcus → 15 damage            │
│  [Turn 14] Sarah's Kira heals Marcus → +18 HP                    │
│                                                                   │
│  [End Turn] [Chat] [Offer Trade]                                  │
└─────────────────────────────────────────────────────────────────┘
```

---

## 12. Integration Points

### 12.1 XP Buckets Integration

Combat feeds into the Core Progression System:

```
Combat Actions Grant XP to Buckets:

Attacking with sword:
  → XP to: combat.melee.sword
  → XP to: combat.aggression (if aggressive skills used)

Blocking with shield:
  → XP to: combat.defense
  → XP to: combat.shield

Casting fire spell:
  → XP to: magic.fire
  → XP to: magic.destruction

Being hit:
  → XP to: combat.toughness (small amount)

Example:
Marcus attacks with sword 100 times:
  combat.melee.sword: +500 XP (5 XP per hit)
  combat.aggression: +200 XP (2 XP per hit, using aggressive stance)

At 1000 XP in combat.melee.sword:
  Unlocks: [Sword Mastery] skill eligibility
  Unlocks: [Warrior] class eligibility (if other reqs met)
```

### 12.2 Class System Integration

Combat classes modify attributes and skills:

```
Class Level-Up Bonuses:
[Warrior] Level 5:
  +2 STR
  +1 END
  Unlocks: [Power Strike] skill

[Berserker] Level 10 (evolution):
  +3 STR
  +2 END
  Unlocks: [Rage] skill
  Bonus: +10% melee damage when HP < 50%

Multi-Classing:
Active Classes: [Warrior] + [Ranger] + [Healer]
XP Split: 50% / 30% / 20%

Combat using bow:
- Uses [Ranger] class level
- Gains XP for: [Ranger] class AND combat.archery bucket
- Damage modified by: FIN (Ranger primary) + skills
```

### 12.3 Skill System Integration

5 Quick Slots + unlimited learned skills:

```
Learned Skills:
- Combat skills (class-granted, bucket-unlocked, discovered)
- Magic spells (school-based)
- Passive skills (always active)

Quick Slot Configuration (5 slots):
Slot 1: Active combat skill (e.g., [Power Strike])
Slot 2: Defense skill (e.g., [Shield Bash])
Slot 3: Utility skill (e.g., [Charge])
Slot 4: Burst damage (e.g., [Whirlwind])
Slot 5: Consumable (e.g., Health Potion)

Unlimited Skills:
All learned skills available for use (just not quick-slotted)
Player can reconfigure quick slots anytime out of combat
```

### 12.4 NPC Core Systems Integration

NPCs use same combat rules + personality-driven behavior:

```
NPC Combat Behavior:
1. Uses same attributes, skills, formulas
2. Targets based on personality (see Section 6.2)
3. Goals influence combat (from NPC Core Systems):
   - Survival goal → Flees if outmatched
   - Protection goal → Defends ward aggressively
   - Ambition goal → Takes risks for kills
   - Secret goal → May betray allies (villain scenarios)

Example:
NPC: Guardsman (Brave, Honest)
Personality: Targets high-threat enemies, demands surrender
Combat: Protects civilians, fights to death, never flees

NPC: Thief (Cowardly, Dishonest)
Personality: Targets low-HP enemies, flees if outmatched
Combat: Uses stealth, targets wounded enemies, flees early
```

### 12.5 Equipment & Items Integration

Combat requires equipment (weapons, armor) and creates economy:

```
Weapon Stats:
- Base damage
- Speed/cooldown
- Material (affects damage vs certain enemies)
- Quality (affects durability, repair cost)

Armor Stats:
- Protection amount
- Weight (affects speed)
- Durability (degrades)
- Repair cost

Crafting Integration:
- Blacksmiths craft/repair metal weapons/armor
- Leatherworkers craft/repair light armor
- Crafting guilds provide repair services
- Economy: Players pay for repairs, crafters earn gold
```

---

## 13. Examples

### 13.1 Solo Combat Example

**Scenario**: Marcus (Warrior) vs Goblin Warrior

```
CHARACTER: Marcus (Level 8 Warrior)
Attributes: STR 14 (+9%), FIN 11 (+1%), END 13 (+15%)
Health: 120/120  Stamina: 90/90
Equipment: Iron Longsword (12-18 dmg), Chain Shirt (medium armor)
Skills: [Power Strike] (+15 dmg), [Sword Mastery] (+12% damage)
XP Bucket: combat.melee.sword (3500 XP → +7% damage)

ENEMY: Goblin Warrior
Attributes: STR 12 (+5%), END 11 (+10%)
Health: 60/60  Stamina: 70/70
Equipment: Rusty Scimitar (8-12 dmg), Leather Armor
```

**Combat Flow (Spectator Mode)**

```
Tick 1 (0:00):
[INITIATIVE] Marcus (AWR 10) vs Goblin (AWR 8)
→ Marcus acts first

[Tick 1 - Marcus Action]
Basic Attack with Iron Longsword
Base Damage: 12-18
+ STR modifier (14): ×1.09
= 13-19
+ Additive skills: none
Subtotal: 13-19
× Multiplier: [Sword Mastery] ×1.12 + Bucket XP ×1.07 = ×1.19
Final Damage: 15-22

Roll: 18 damage
→ Hits Goblin (defense 5)
→ Goblin takes: 18 - 5 = 13 damage
→ Goblin HP: 60 → 47

[Tick 1 - Goblin Action]
Basic Attack with Scimitar
Base Damage: 8-12
+ STR modifier (12): ×1.05
= 8-12
Final Damage: 8-12 (no skills)

Roll: 10 damage
→ Hits Marcus (armor 8, shield 0)
→ Shield layer: 0 (no shield)
→ Armor layer: Absorbs 8 (degrades 48→47)
→ Overflow: 2 damage to health
→ Marcus HP: 120 → 118

Tick 2 (0:02):
Marcus attacks: 16 damage → Goblin HP 47 → 31
Goblin attacks: 9 damage → Armor absorbs 8 → Marcus HP 118 → 117

Tick 3 (0:04):
Marcus uses [Power Strike] (costs 15 stamina)
Base: 12-18 + 15 (skill) = 27-33
× Multiplier: ×1.19
Final: 32-39

Roll: 35 damage
→ Goblin HP 31 → 0 (DEAD)

COMBAT END
Marcus: 117/120 HP, 75/90 stamina
Goblin: DEAD
Loot: 12 copper, Rusty Scimitar (worthless)
XP Gained: combat.melee.sword +25 XP
```

### 13.2 Party Combat Example (Player Control)

**Scenario**: Player's party (Marcus, Elena, Kira, Toren) vs Enemy Group

```
YOUR PARTY:
[🎮] Marcus (Warrior) - PLAYER POSSESSED
  STR 14, FIN 11, END 13, AWR 10, CHA 8
  HP 85/120, STAM 75/90, MANA 0/0
  Equipment: Iron Longsword, Chain Shirt (med)
  Skills: [Power Strike], [Shield Bash], [Whirlwind], [Charge], Health Potion

[🔥] Elena (Mage) - AI Controlled
  STR 8, FIN 10, END 9, WIT 15, AWR 12, CHA 11
  HP 60/80, STAM 50/60, MANA 65/70
  Equipment: Staff, Robes (light)
  Skills: [Fireball], [Ice Spike], [Shield], [Mana Potion]

[💚] Kira (Healer) - AI Controlled
  STR 9, FIN 10, END 12, WIT 13, AWR 11, CHA 14
  HP 70/90, STAM 60/80, MANA 50/60
  Equipment: Mace, Leather Armor
  Skills: [Heal], [Cure Wounds], [Bless], [Smite]

[⚔️] Toren (Ranger) - AI Controlled
  STR 11, FIN 14, END 10, WIT 10, AWR 13, CHA 9
  HP 55/75, STAM 50/70, MANA 0/0
  Equipment: Longbow, Shortsword, Leather Armor
  Skills: [Precise Shot], [Multi-Shot], [Tracking]

ENEMY PARTY:
[Goblin Warrior] HP 45/60
[Goblin Spearman] HP 22/50
[Goblin Shaman] HP 35/45
```

**Player's Turn Choices**

```
Player (controlling Marcus) sees:

1. Goblin Warrior (front row, 45/60 HP)
2. Goblin Spearman (front row, 22/50 HP) - WOUNDED
3. Goblin Shaman (back row, 35/45 HP)

Player chooses:
→ Target Goblin Spearman (wounded, easiest kill)
→ Use [Power Strike] (15 damage, 15 stamina)

Combat Log:
[14:45:12] Marcus uses [Power Strike] on Goblin Spearman
  → 32 damage
  → Goblin Spearman HP 22 → 0 (KILLED)

AI responds:
Elena (Mage): Casts [Fireball] on back row
  → 28 fire damage to Goblin Shaman
  → Goblin Shaman HP 35 → 7

Kira (Healer): Heals Marcus (85/120 → 103/120)
  → Uses 15 mana

Toren (Ranger): Uses [Precise Shot] on Goblin Warrior
  → 22 damage
  → Goblin Warrior HP 45 → 23

Next Turn:
Player chooses:
→ Target Goblin Shaman (low HP, back row, but can reach)
→ Use basic attack (charge into back row via breakthrough)
```

---

## 14. Open Questions Resolved

This Combat System GDD resolves the following open questions:

| Question ID | Question                  | Resolution                                                                                   |
| ----------- | ------------------------- | -------------------------------------------------------------------------------------------- |
| **8.2**     | Offline Combat Resolution | Three-mode system (Background/Spectator/Player Control) with appropriate complexity per mode |
| **HIGH**    | Combat formula structure  | (Base + Additive) × Multiplicative formula with clear skill integration                      |
| **HIGH**    | Damage types              | Full 6-type system (Physical, Fire, Cold, Poison, Electric, Arcane)                          |
| **HIGH**    | Status effects            | Full system (Stun, Poison, Burn, Freeze, Slow, etc.)                                         |
| **HIGH**    | Defense layers            | Shields → Armor → Health with degradation and repair                                         |
| **HIGH**    | Speed/cooldown system     | Hybrid approach (character speed + weapon cooldown)                                          |
| **HIGH**    | Positioning               | Range-based auto-rows, tactical depth in player control                                      |
| **HIGH**    | AI targeting              | Mode-adaptive (simple aggro / personality / role-based)                                      |
| **HIGH**    | Skill activation          | Manual when controlling, auto when AI                                                        |
| **HIGH**    | Combat end conditions     | Context-aware (elimination / morale / surrender)                                             |

**Total HIGH Priority Questions Resolved: 10/28**

---

## 15. Future Systems (Not Yet Defined)

The following systems interact with combat but need separate GDDs:

1. **Equipment & Items System** — Weapons, armor, item tiers, crafting integration
2. **Economy System** — Gold, repair costs, loot tables, trading
3. **Quest System** — Combat quests, bounties, encounters
4. **Magic System** — Spell schools, mana regeneration, enchanting
5. **Crafting System** — Equipment repair, item creation, material costs

---

## Related Documentation

### Authoritative GDDs (Core Systems)

- [Core Progression System](core-progression-system-gdd.md) — XP buckets, dual tracking, multi-classing
- [Class System](class-system-gdd.md) — Tiers, specializations, class slots
- [Skill & Recipe System](skill-recipe-system-gdd.md) — 5 quick slots, rarity, achievements
- [NPC Core Systems](npc-core-systems-gdd.md) — Goals, conflicts, personality

### Reference Design Documents

- [Idle Game Overview](../idle-game-overview.md) — Complete game concept, 7 scenarios
- [Idle Inn GDD](../idle-inn-gdd.md) — Primary scenario with combat encounters
- [Idle Adventurer Guild GDD](../scenarios/idle-adventurer-guild.md) — Hero coordination scenario

### Reference Documentation

Key combat mechanics reference documentation (migrated from cozy-fantasy-rpg project):

- [Attributes System](reference/systems/attributes.md) — Six core attributes (STR, FIN, END, WIT, AWR, CHA) with thresholds, checks, and progression
- [Combat Resolution](reference/systems/combat-resolution.md) — Defense layers, damage types, combat flow, positioning, AI targeting
- [Armor Systems](reference/systems/armor-systems.md) — Protection vs mobility, weight classes, degradation, maintenance
- [Weapons Systems](reference/systems/weapons-systems.md) — Progression, materials, enchantment affinity, training

---

**Document Status**: ✅ AUTHORITATIVE
**Last Updated**: 2026-02-03
**Next Review**: After Equipment & Items System GDD completion
