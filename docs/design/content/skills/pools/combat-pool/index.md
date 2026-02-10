---
title: Combat Pool Skills
gdd_ref: systems/skill-recipe-system-gdd.md#skill-pools
---

# Combat Pool Skills

## Synergy Classes

Classes with strong synergies: Warrior, Adventurer, Knight, Guard, Mercenary

**Synergy Benefits**: 2x XP acquisition, enhanced effectiveness, reduced costs (scales with class level)

## Skills

### Weapon Mastery (Tiered)

Specialized proficiency with weapon category.

| Tier     | Effect                                               | Scaling                |
| -------- | ---------------------------------------------------- | ---------------------- |
| Lesser   | +10% damage, +5% accuracy with category              | +1% per class level    |
| Greater  | +18% damage, +10% accuracy, unlock weapon techniques | +0.75% per class level |
| Enhanced | +25% damage, +15% accuracy, advanced techniques      | +0.5% per class level  |

**Categories:** Swords, Axes, Maces, Spears, Bows, Crossbows, Daggers, Unarmed

**Synergy Classes**: Warrior, Adventurer, Knight, Guard, Mercenary

**XP Sources**:

- Combat kills with weapon type: 5-15 XP
- Training with weapon: 10-50 XP/session
- Guided training: 10-100 XP/session
- Unguided training: 5-50 XP/session

**Level-Up Availability**:

- Strong synergy: ~500 XP (~50 actions)
- Moderate synergy: ~1000 XP
- No synergy: ~1500 XP

**Synergy Examples**:

- **Strong (2x XP)**: Warrior Level 15 - Direct combat specialization - +25% effectiveness, -25% stamina cost, 2x XP from weapon combat
- **Moderate (1.5x XP)**: Adventurer Level 10 - Related combat skill - +20% effectiveness, -20% cost
- **None (1x XP)**: Scholar - No logical connection - No bonuses

---

### Shield Wall (Tiered)

Enhanced shield defensive capability.

| Tier     | Effect                                                             | Scaling                |
| -------- | ------------------------------------------------------------------ | ---------------------- |
| Lesser   | +15% block chance, -10% shield degradation                         | +1% per class level    |
| Greater  | +25% block, -20% degradation, protect adjacent ally                | +0.75% per class level |
| Enhanced | +35% block, -30% degradation, protect all adjacent, reflect chance | +0.5% per class level  |

**Synergy Classes**: Warrior, Knight, Guard

**XP Sources**:

- Block attacks with shield: 3-10 XP
- Protect allies: 10-20 XP
- Shield bash: 5-15 XP
- Guided training: 10-100 XP/session
- Unguided training: 5-50 XP/session

**Level-Up Availability**:

- Strong synergy: ~500 XP (~50 actions)
- Moderate synergy: ~1000 XP
- No synergy: ~1500 XP

**Synergy Examples**:

- **Strong (2x XP)**: Knight Level 15 - Defensive specialist - +25% block effectiveness, -25% stamina per block, 2x XP from shield actions
- **Moderate (1.5x XP)**: Warrior Level 10 - Combat generalist - +20% effectiveness
- **None (1x XP)**: Thief - No logical connection - No bonuses

---

### Battle Rage (Tiered)

Controlled fury in combat.

| Tier     | Type     | Effect                                          | Duration |
| -------- | -------- | ----------------------------------------------- | -------- |
| Lesser   | Duration | +20% damage, -10% defense                       | 30 sec   |
| Greater  | Duration | +35% damage, -5% defense, pain resistance       | 45 sec   |
| Enhanced | Duration | +50% damage, no defense penalty, immune to fear | 60 sec   |

**Scaling:** Duration +2 sec per class level

**Synergy Classes**: Warrior, Adventurer, Mercenary

**XP Sources**:

- Take damage in combat: 2-8 XP
- Kill enemies while raging: 10-20 XP
- Fight while wounded: 5-15 XP
- Guided training: 10-100 XP/session
- Unguided training: 5-50 XP/session

**Level-Up Availability**:

- Strong synergy: ~500 XP (~50 actions)
- Moderate synergy: ~1000 XP
- No synergy: ~1500 XP

**Synergy Examples**:

- **Strong (2x XP)**: Warrior Level 15 - Berserker specialization - +25% rage effectiveness, +30 sec duration, 2x XP from rage combat
- **Moderate (1.5x XP)**: Mercenary Level 10 - Combat profession - +20% effectiveness
- **None (1x XP)**: Farmer - No connection - No bonuses

---

### Tactical Assessment (Mechanic Unlock)

Analyze enemy capabilities.

| Effect  | Description                                                                     |
| ------- | ------------------------------------------------------------------------------- |
| Unlock  | Can examine enemies to reveal: Health %, resistances, damage type, threat level |
| Passive | Automatically detect enemy level relative to self                               |

**No tiers or scaling—binary unlock**

**Synergy Classes**: All Combat pool, Scout

**XP Sources**:

- Fight varied enemy types: 5-10 XP per new type
- Survive difficult encounters: 15-25 XP
- Guided training: 10-100 XP/session
- Unguided training: 5-50 XP/session

**Level-Up Availability**:

- Strong synergy: ~500 XP (~50 actions)
- Moderate synergy: ~1000 XP
- No synergy: ~1500 XP

**Synergy Examples**:

- **Strong (2x XP)**: Knight Level 15 - Tactical fighter - Enhanced assessment speed, 2x XP from combat analysis
- **Moderate (1.5x XP)**: Scout Level 10 - Observation specialist - Faster assessment
- **None (1x XP)**: Crafter - No connection - No bonuses

---

### Commander's Voice (Tiered)

Combat orders affect more allies at greater range.

| Tier     | Range    | Compliance Bonus | Additional                           |
| -------- | -------- | ---------------- | ------------------------------------ |
| Lesser   | Medium   | +15%             | Orders execute faster                |
| Greater  | Far      | +25%             | Can issue two orders per pause       |
| Enhanced | Very Far | +35%             | Orders persist through morale breaks |

**Scaling:** +1% compliance per class level

**Synergy Classes**: Knight, Guard, Leader, Chieftain

**XP Sources**:

- Issue orders in combat: 5-10 XP per order
- Have orders followed: 3-8 XP
- Lead parties: 10-20 XP per combat
- Guided training: 10-100 XP/session
- Unguided training: 5-50 XP/session

**Level-Up Availability**:

- Strong synergy: ~500 XP (~50 actions)
- Moderate synergy: ~1000 XP
- No synergy: ~1500 XP

**Synergy Examples**:

- **Strong (2x XP)**: Knight Level 15 - Command specialist - +25% compliance, extended range, 2x XP from issuing orders
- **Moderate (1.5x XP)**: Guard Level 10 - Authority figure - +20% compliance
- **None (1x XP)**: Alchemist - No connection - No bonuses

---

### Killing Blow (Mechanic Unlock)

Execute wounded enemies instantly.

| Effect  | Description                                                               |
| ------- | ------------------------------------------------------------------------- |
| Unlock  | When enemy below 15% HP, can attempt execution (instant kill if succeeds) |
| Success | Based on STR vs target END                                                |
| Failure | Normal attack instead                                                     |

**No scaling—threshold fixed at 15%**

**Synergy Classes**: Warrior, Assassin, Knight

**XP Sources**:

- Land killing blows: 5-15 XP
- Finish wounded enemies: 3-10 XP
- Guided training: 10-100 XP/session
- Unguided training: 5-50 XP/session

**Level-Up Availability**:

- Strong synergy: ~500 XP (~50 actions)
- Moderate synergy: ~1000 XP
- No synergy: ~1500 XP

**Synergy Examples**:

- **Strong (2x XP)**: Warrior Level 15 - Execution specialist - +10% success chance, 2x XP from executions
- **Moderate (1.5x XP)**: Assassin Level 10 - Lethal fighter - +5% success
- **None (1x XP)**: Trader - No connection - No bonuses

---

### Sentinel's Watch (Mechanic Unlock)

Cannot be ambushed while awake.

| Effect  | Description                                 |
| ------- | ------------------------------------------- |
| Unlock  | Immune to surprise attacks while conscious  |
| Passive | Always act in first combat round            |
| Party   | Adjacent allies gain +25% ambush resistance |

**No scaling—binary unlock**

**Prerequisite:** Combat Awareness (Greater) or equivalent AWR

**Synergy Classes**: Guard, Knight, Scout

**XP Sources**:

- Detect ambushes: 15-25 XP
- Stand watch: 5-10 XP
- Maintain vigilance: 3-8 XP
- Guided training: 10-100 XP/session
- Unguided training: 5-50 XP/session

**Level-Up Availability**:

- Strong synergy: ~500 XP (~50 actions)
- Moderate synergy: ~1000 XP
- No synergy: ~1500 XP

**Synergy Examples**:

- **Strong (2x XP)**: Guard Level 15 - Vigilance specialist - Enhanced detection range, 2x XP from watch duty
- **Moderate (1.5x XP)**: Scout Level 10 - Awareness expert - Better detection
- **None (1x XP)**: Merchant - No connection - No bonuses

---

### Armor Expert (Tiered)

Improved heavy armor usage.

| Tier     | Effect                                                                 | Scaling                |
| -------- | ---------------------------------------------------------------------- | ---------------------- |
| Lesser   | -25% armor penalties (speed, stamina)                                  | +1% per class level    |
| Greater  | -50% armor penalties, +10% armor effectiveness                         | +0.75% per class level |
| Enhanced | -75% armor penalties, +20% effectiveness, armor repairs slower degrade | +0.5% per class level  |

**Synergy Classes**: Warrior, Knight, Guard

**XP Sources**:

- Fight in heavy armor: 3-8 XP per combat
- Take hits while armored: 2-5 XP
- Guided training: 10-100 XP/session
- Unguided training: 5-50 XP/session

**Level-Up Availability**:

- Strong synergy: ~500 XP (~50 actions)
- Moderate synergy: ~1000 XP
- No synergy: ~1500 XP

**Synergy Examples**:

- **Strong (2x XP)**: Knight Level 15 - Heavy armor specialist - -35% penalties, +25% effectiveness, 2x XP from armored combat
- **Moderate (1.5x XP)**: Warrior Level 10 - Combat fighter - -20% penalties
- **None (1x XP)**: Rogue - No connection - No bonuses

---

### Mounted Combat (Mechanic Unlock)

Fight effectively from mount.

| Effect  | Description                                                       |
| ------- | ----------------------------------------------------------------- |
| Unlock  | No penalty when fighting mounted                                  |
| Charge  | Can perform mounted charge (bonus damage, breakthrough potential) |
| Control | Mount follows combat commands without check                       |

**No scaling—binary unlock**

**Tier 2:** Mounted Mastery—charge damage +50%, can fight while mount moves

**Synergy Classes**: Knight, Adventurer, Caravaner

**XP Sources**:

- Ride mounts: 2-5 XP
- Fight while mounted: 10-20 XP
- Train with mounts: 5-15 XP
- Guided training: 10-100 XP/session
- Unguided training: 5-50 XP/session

**Level-Up Availability**:

- Strong synergy: ~500 XP (~50 actions)
- Moderate synergy: ~1000 XP
- No synergy: ~1500 XP

**Synergy Examples**:

- **Strong (2x XP)**: Knight Level 15 - Cavalry specialist - +25% mounted damage, 2x XP from mounted combat
- **Moderate (1.5x XP)**: Adventurer Level 10 - Mobile fighter - +15% mounted effectiveness
- **None (1x XP)**: Smith - No connection - No bonuses

---

### Dual Wield (Tiered)

Fight with two weapons effectively.

| Tier     | Effect                                  | Scaling                |
| -------- | --------------------------------------- | ---------------------- |
| Lesser   | -15% penalty (reduced from -30%)        | +1% per class level    |
| Greater  | No penalty, +10% attack speed           | +0.75% per class level |
| Enhanced | +15% attack speed, off-hand damage +25% | +0.5% per class level  |

**Synergy Classes**: Warrior, Adventurer, Thief, Assassin

**XP Sources**:

- Fight with two weapons: 5-10 XP per combat
- Land off-hand attacks: 3-8 XP
- Guided training: 10-100 XP/session
- Unguided training: 5-50 XP/session

**Level-Up Availability**:

- Strong synergy: ~500 XP (~50 actions)
- Moderate synergy: ~1000 XP
- No synergy: ~1500 XP

**Synergy Examples**:

- **Strong (2x XP)**: Warrior Level 15 - Dual weapon specialist - No penalties, +20% attack speed, 2x XP from dual wielding
- **Moderate (1.5x XP)**: Thief Level 10 - Agile fighter - Reduced penalties
- **None (1x XP)**: Farmer - No connection - No bonuses

---

### Battle Hardened (Passive Generator)

Experience generates combat readiness.

| Tier     | Daily Effect                                               |
| -------- | ---------------------------------------------------------- |
| Lesser   | Start each day with +5% damage resistance for first combat |
| Greater  | +10% resistance first combat, +5% second combat            |
| Enhanced | +15% first, +10% second, +5% all subsequent                |

**No scaling—fixed generation**

**Synergy Classes**: All Combat pool

**XP Sources**:

- Survive multiple combats daily: 5-10 XP per day
- Endure extended campaigns: 10-25 XP per week
- Guided training: 10-100 XP/session
- Unguided training: 5-50 XP/session

**Level-Up Availability**:

- Strong synergy: ~500 XP (~50 actions)
- Moderate synergy: ~1000 XP
- No synergy: ~1500 XP

**Synergy Examples**:

- **Strong (2x XP)**: Mercenary Level 15 - Veteran fighter - +20% daily resistance, 2x XP from surviving combat
- **Moderate (1.5x XP)**: Guard Level 10 - Regular fighter - +15% resistance
- **None (1x XP)**: Herbalist - No connection - No bonuses

## Related Content

- **See Also:** [Skills Index](../index.md), [Character Development](../../systems/character/index.md)
