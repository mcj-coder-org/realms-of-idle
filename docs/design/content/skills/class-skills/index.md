---
title: Class-Specific Skills
gdd_ref: systems/skill-recipe-system-gdd.md#skills
---

# Class-Specific Skills

> **Note**: Individual skill files are now organized in type directories under `/tiered/`, `/mechanic-unlock/`, `/passive-generator/`, and `/cooldown/`.
> This document provides overview and mechanics. For specific skills, browse the type directories.

## 1. Overview

Class-specific skills have strong synergies with appropriate classes but are not restricted. Any character can learn these skills through training or action-based acquisition, but characters with relevant classes gain significant bonuses.

| Principle                | Description                                                                           |
| ------------------------ | ------------------------------------------------------------------------------------- |
| Synergy-Based Access     | Skills benefit from relevant classes, not restricted by them                          |
| Context-Specific Bonuses | Bonuses scale with logical relationship (direct specialization > related > unrelated) |
| Multiple Paths           | Acquire via level-up (class-based thresholds), training (guided/unguided), or actions |
| Specialized Power        | Stronger effects than common skills in their domain                                   |
| Mechanic Unlocks         | Some skills grant abilities rather than bonuses                                       |
| XP-Based Progression     | Skills track XP and improve continuously (no discrete levels)                         |
| Cross-Class Access       | Some skills synergize with multiple related classes                                   |

---

## 2. Skill Progression Mechanics

### Acquisition Paths

Skills can be acquired through three methods:

**1. Level-Up Awards** (from class synergy pools):

- Skills offered at class level-up from relevant pools
- **Availability threshold varies by class relationship**:
  - Skills with strong synergy: Lower threshold (~500 XP, ~50 actions)
  - Skills with no synergy: Higher threshold (~1500 XP, ~100-150 actions)
- Classes determine which pools have higher offer rates
- Excess contributing actions/XP improve tier quality

**2. Training** (direct skill development):

- Characters can train for any skill directly
- **Guided Training** (with trainer): Standard progression, faster unlock (~200 XP)
- **Unguided Training** (self-taught): Slower unlock (~400 XP), but **+60% better initial boost**
- No class requirement, but relevant classes make training faster
- Training tracks XP progress toward skill unlock

**3. Action-Based** (performing required actions):

- Performing skill-related actions accumulates progress
- **Hybrid threshold**: ~50 actions ±20% variance (40-60 actual range)
- Characters with relevant classes progress faster due to synergies
- Not deterministic: adds variance and unpredictability

### XP-Based Progression

Skills track XP and improve continuously (no discrete levels):

**XP Sources** (varies by skill):

- Performing skill-related actions: 1-10 XP per action
- Successful skill use: 5-25 XP
- Great/exceptional results: 15-50 XP bonus
- Guided training sessions: 10-100 XP per session
- Unguided practice: 5-50 XP per session

**Continuous Improvement**:

- Skills accumulate XP from usage
- XP drives smooth, continuous improvement
- No level thresholds - improvements scale with XP amount
- More XP = better effectiveness, lower costs, higher success rates
- Certain techniques/variants unlock at specific XP milestones

**Example**: Heat Reading at 0 XP vs 500 XP vs 2000 XP shows continuously improving effectiveness, not discrete levels.

### Training Types

**Guided Training** (With trainer):

- Standard XP progression (10-100 XP per session)
- Normal initial boost when skill unlocks
- Faster route to skill acquisition (~200 XP to unlock)

**Unguided Training** (Self-taught):

- Slower XP progression (5-50 XP per session)
- Requires more XP to unlock (~400 XP threshold)
- **Significantly better initial boost** when unlocked (~60% stronger)
- Reward for self-discovery (similar to unguided class training)

### Level-Up Availability Thresholds

Skills become available for level-up awards based on XP thresholds:

| Class Relationship                     | XP Threshold                | Example                                          |
| -------------------------------------- | --------------------------- | ------------------------------------------------ |
| Strong synergy (direct specialization) | ~500 XP (~50 actions)       | Blacksmith: Heat Reading available after ~500 XP |
| Moderate synergy (related skill)       | ~1000 XP (~75 actions)      | Miner: Heat Reading available after ~1000 XP     |
| No synergy (unrelated)                 | ~1500 XP (~100-150 actions) | Warrior: Heat Reading available after ~1500 XP   |

This creates natural specialization without hard gates.

### Class Synergy Model (Context-Specific)

Synergy bonuses depend on logical relationship between class and skill:

**Strong Synergy** (Direct specialization):

- **XP Multiplier**: 2x (e.g., +100% XP from relevant actions)
- **Examples**:
  - Blacksmith + Heat Reading: Core trade skill
  - Farmer + Crop Growth: Direct specialization
  - Miner + Ore Harvesting: Direct specialization
- **Benefits**: Faster learning, better effectiveness, reduced costs
- **Scales with class level**: Level 15 > Level 5

**Moderate Synergy** (Related skill):

- **XP Multiplier**: 1.5x (e.g., +50% XP from relevant actions)
- **Examples**:
  - Miner + Heat Reading: Related (uses for smelting)
  - Hunter + Tracker: Related to hunting
- **Benefits**: Moderate learning bonus, some effectiveness improvements

**No Synergy** (Unrelated):

- **XP Multiplier**: 1x (base rate, no bonuses)
- **Examples**:
  - Warrior + Heat Reading: No logical connection
  - Scholar + Ore Harvesting: No connection
- **Can still learn**: Just without synergy bonuses

**Synergy Strength Scales with Class Level**:

| Class Level | Strong Synergy | Moderate Synergy | Example (Blacksmith + Heat Reading) |
| ----------- | -------------- | ---------------- | ----------------------------------- |
| Level 5     | 1.5x XP        | 1.25x XP         | +50% XP, +15% effect, -15% cost     |
| Level 10    | 1.75x XP       | 1.4x XP          | +75% XP, +20% effect, -20% cost     |
| Level 15    | 2.0x XP        | 1.5x XP          | +100% XP, +25% effect, -25% cost    |
| Level 30+   | 2.5x XP        | 1.75x XP         | +150% XP, +35% effect, -35% cost    |

### Tier Acquisition

**Three ways to gain tiered skills**:

1. **Direct Award**: Can be awarded any tier at unlock based on accumulated XP
2. **Upgrade Path**: Having Lesser makes Greater +40% easier, Greater makes Enhanced +40% easier
3. **Level-Up Influence**: Excess contributing actions improve tier award chances

See [class-progression.md](../../systems/character/class-progression/index.md#5-skill-tiers) for full tier mechanics.

---

## 3. Skill Categories

### Tiered Skills (Scale)

Standard progression: Lesser → Greater → Enhanced
Scale with the offering class's levels at standard rates.

### Mechanic Unlocks (No Scale)

Grant a specific ability or mechanic permanently.
No tiers—either you have it or you don't.
Often have prerequisites or special acquisition conditions.

### Passive Generators

Produce resources, effects, or opportunities over time.
May have tiers affecting generation rate.

---

## 4. Type-Based Organization

Skills are organized by their type rather than by class synergy pools. Each type directory contains an index file listing all skills of that type.

### Skill Types

**[Tiered Skills](/tiered/)** (`/tiered/`):

- Scale in power: Lesser → Greater → Enhanced
- Each tier provides stronger effects than the previous
- Scale with class level at standard rates
- Examples: Weapon Mastery, Heat Reading, Bountiful Harvest

**[Mechanic Unlocks](/mechanic-unlock/)** (`/mechanic-unlock/`):

- Grant specific abilities or mechanics permanently
- No tiers—either you have it or you don't
- Often have prerequisites or special acquisition conditions
- Examples: Tactical Assessment, Vanish, Beast Tongue

**[Passive Generators](/passive-generator/)** (`/passive-generator/`):

- Produce resources, effects, or opportunities over time
- May have tiers affecting generation rate
- Operate continuously without active use
- Examples: Master Forager, Surplus Generator, Trade Network

**[Cooldown Skills](/cooldown/)** (`/cooldown/`):

- Powerful effects with cooldown periods
- May have limited uses per day/encounter
- Balance powerful effects with usage restrictions
- Examples: Battle Rage (duration-based), Vanish (10-minute cooldown)

### Finding Skills

Browse the type directories to find specific skills, or use the historical pool summaries below as reference for thematic organization.

---

## 5. Tag-Based Organization

Skills use hierarchical tags to define class synergies, replacing the previous pool system.

### Hierarchical Tag Format

Tags follow a `Category/Subcategory/Specialization` format:

- **Category**: Broad domain (Combat, Crafting, Magic, etc.)
- **Subcategory**: Specific area (Smithing, Stealth, Gathering/Mining, etc.)
- **Specialization**: Narrow focus (Smithing/Weapons, Gathering/Mining/Ore, etc.)

### Tag Inheritance

Classes with specific tags automatically access all parent tags:

- Class with `Crafting/Smithing` tag can access:
  - `Crafting` (general crafting skills)
  - `Crafting/Smithing` (smithing skills)
  - `Crafting/Smithing/*` (all smithing specializations)

### Multi-Tag Support

Skills can have multiple tags for cross-domain synergies:

- **Heat Reading**: Tags `Crafting/Smithing`, `Gathering/Mining`
  - Blacksmiths get synergy (direct specialization)
  - Miners get synergy (related use for smelting)

### Tag Hierarchy Examples

**Combat Tags**:

- `Combat` → `Combat/Melee` → `Combat/Melee/Swords`
- `Combat` → `Combat/Ranged` → `Combat/Ranged/Bows`
- `Combat` → `Combat/Defense` → `Combat/Defense/Shields`

**Crafting Tags**:

- `Crafting` → `Crafting/Smithing` → `Crafting/Smithing/Weapons`
- `Crafting` → `Crafting/Alchemy` → `Crafting/Alchemy/Potions`

**Gathering Tags**:

- `Gathering` → `Gathering/Mining` → `Gathering/Mining/Ore`
- `Gathering` → `Gathering/Foraging` → `Gathering/Foraging/Herbs`

### Tag Mappings

For complete tag mappings from previous pools to new tag-based organization, see:
**[REFACTOR-AUDIT.md](REFACTOR-AUDIT/index.md)**

This document tracks the transformation from pool-based to tag-based organization with detailed mappings.

---

## 6. Previous Pool-Based Organization - Archived for Reference

**The sections below are preserved for historical reference. Individual skill files are now organized in type directories (`/tiered/`, `/mechanic-unlock/`, `/passive-generator/`, `/cooldown/`) and use hierarchical tags instead of pools.**

### Combat Pool (Historical)

### Weapon Mastery (Tiered)

Specialized proficiency with weapon category.

| Tier     | Effect                                               | Scaling                |
| -------- | ---------------------------------------------------- | ---------------------- |
| Lesser   | +10% damage, +5% accuracy with category              | +1% per class level    |
| Greater  | +18% damage, +10% accuracy, unlock weapon techniques | +0.75% per class level |
| Enhanced | +25% damage, +15% accuracy, advanced techniques      | +0.5% per class level  |

**Categories:** Swords, Axes, Maces, Spears, Bows, Crossbows, Daggers, Unarmed
**Trigger Actions:** Combat kills with weapon type, training with weapon
**Classes:** All Combat pool

---

### Shield Wall (Tiered)

Enhanced shield defensive capability.

| Tier     | Effect                                                             | Scaling                |
| -------- | ------------------------------------------------------------------ | ---------------------- |
| Lesser   | +15% block chance, -10% shield degradation                         | +1% per class level    |
| Greater  | +25% block, -20% degradation, protect adjacent ally                | +0.75% per class level |
| Enhanced | +35% block, -30% degradation, protect all adjacent, reflect chance | +0.5% per class level  |

**Trigger Actions:** Block attacks with shield, protect allies, shield bash
**Classes:** Warrior, Knight, Guard

---

### Battle Rage (Tiered)

Controlled fury in combat.

| Tier     | Type     | Effect                                          | Duration |
| -------- | -------- | ----------------------------------------------- | -------- |
| Lesser   | Duration | +20% damage, -10% defense                       | 30 sec   |
| Greater  | Duration | +35% damage, -5% defense, pain resistance       | 45 sec   |
| Enhanced | Duration | +50% damage, no defense penalty, immune to fear | 60 sec   |

**Scaling:** Duration +2 sec per class level
**Trigger Actions:** Take damage in combat, kill enemies, fight while wounded
**Classes:** Warrior, Adventurer, Mercenary

---

### Tactical Assessment (Mechanic Unlock)

Analyze enemy capabilities.

| Effect  | Description                                                                     |
| ------- | ------------------------------------------------------------------------------- |
| Unlock  | Can examine enemies to reveal: Health %, resistances, damage type, threat level |
| Passive | Automatically detect enemy level relative to self                               |

**No tiers or scaling—binary unlock**
**Trigger Actions:** Fight varied enemy types, survive difficult encounters
**Classes:** All Combat pool, Scout

---

### Commander's Voice (Tiered)

Combat orders affect more allies at greater range.

| Tier     | Range    | Compliance Bonus | Additional                           |
| -------- | -------- | ---------------- | ------------------------------------ |
| Lesser   | Medium   | +15%             | Orders execute faster                |
| Greater  | Far      | +25%             | Can issue two orders per pause       |
| Enhanced | Very Far | +35%             | Orders persist through morale breaks |

**Scaling:** +1% compliance per class level
**Trigger Actions:** Issue orders in combat, have orders followed, lead parties
**Classes:** Knight, Guard, Leader, Chieftain

---

### Killing Blow (Mechanic Unlock)

Execute wounded enemies instantly.

| Effect  | Description                                                               |
| ------- | ------------------------------------------------------------------------- |
| Unlock  | When enemy below 15% HP, can attempt execution (instant kill if succeeds) |
| Success | Based on STR vs target END                                                |
| Failure | Normal attack instead                                                     |

**No scaling—threshold fixed at 15%**
**Trigger Actions:** Land killing blows, finish wounded enemies
**Classes:** Warrior, Assassin, Knight

---

### Sentinel's Watch (Mechanic Unlock)

Cannot be ambushed while awake.

| Effect  | Description                                 |
| ------- | ------------------------------------------- |
| Unlock  | Immune to surprise attacks while conscious  |
| Passive | Always act in first combat round            |
| Party   | Adjacent allies gain +25% ambush resistance |

**No scaling—binary unlock**
**Trigger Actions:** Detect ambushes, stand watch, maintain vigilance
**Classes:** Guard, Knight, Scout
**Prerequisite:** Combat Awareness (Greater) or equivalent AWR

---

### Armor Expert (Tiered)

Improved heavy armor usage.

| Tier     | Effect                                                                 | Scaling                |
| -------- | ---------------------------------------------------------------------- | ---------------------- |
| Lesser   | -25% armor penalties (speed, stamina)                                  | +1% per class level    |
| Greater  | -50% armor penalties, +10% armor effectiveness                         | +0.75% per class level |
| Enhanced | -75% armor penalties, +20% effectiveness, armor repairs slower degrade | +0.5% per class level  |

**Trigger Actions:** Fight in heavy armor, take hits while armored
**Classes:** Warrior, Knight, Guard

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
**Trigger Actions:** Ride mounts, fight while mounted, train with mounts
**Classes:** Knight, Adventurer, Caravaner

---

### Dual Wield (Tiered)

Fight with two weapons effectively.

| Tier     | Effect                                  | Scaling                |
| -------- | --------------------------------------- | ---------------------- |
| Lesser   | -15% penalty (reduced from -30%)        | +1% per class level    |
| Greater  | No penalty, +10% attack speed           | +0.75% per class level |
| Enhanced | +15% attack speed, off-hand damage +25% | +0.5% per class level  |

**Trigger Actions:** Fight with two weapons, land off-hand attacks
**Classes:** Warrior, Adventurer, Thief, Assassin

---

### Battle Hardened (Passive Generator)

Experience generates combat readiness.

| Tier     | Daily Effect                                               |
| -------- | ---------------------------------------------------------- |
| Lesser   | Start each day with +5% damage resistance for first combat |
| Greater  | +10% resistance first combat, +5% second combat            |
| Enhanced | +15% first, +10% second, +5% all subsequent                |

**No scaling—fixed generation**
**Trigger Actions:** Survive multiple combats daily, endure extended campaigns
**Classes:** All Combat pool

---

## 7. Stealth Pool (Historical)

### Shadow Step (Tiered)

Enhanced stealth movement.

| Tier     | Effect                                                        | Scaling                |
| -------- | ------------------------------------------------------------- | ---------------------- |
| Lesser   | +15% stealth, no penalty for slow movement                    | +1% per class level    |
| Greater  | +25% stealth, half penalty for normal movement                | +0.75% per class level |
| Enhanced | +35% stealth, no movement penalty, brief invisibility on kill | +0.5% per class level  |

**Trigger Actions:** Move undetected, sneak past enemies, stealth kills
**Classes:** Thief, Assassin, Scout

---

### Backstab (Tiered)

Bonus damage from stealth or behind.

| Tier     | Effect                                                    | Scaling               |
| -------- | --------------------------------------------------------- | --------------------- |
| Lesser   | +50% damage from stealth, +25% from behind                | +2% per class level   |
| Greater  | +100% damage from stealth, +50% from behind               | +1.5% per class level |
| Enhanced | +150% damage from stealth, +75% from behind, ignore armor | +1% per class level   |

**Trigger Actions:** Attack from stealth, attack from behind, assassinate
**Classes:** Thief, Assassin

---

### Lockmaster (Tiered)

Superior lockpicking ability.

| Tier     | Effect                                             | Scaling                |
| -------- | -------------------------------------------------- | ---------------------- |
| Lesser   | +20% lockpick success, -20% lockpick breakage      | +1% per class level    |
| Greater  | +35% success, -40% breakage, pick magical locks    | +0.75% per class level |
| Enhanced | +50% success, -60% breakage, sense lock complexity | +0.5% per class level  |

**Trigger Actions:** Pick locks successfully, open difficult locks
**Classes:** Thief, Scout

---

### Vanish (Mechanic Unlock)

Emergency stealth entry.

| Effect      | Description                                                 |
| ----------- | ----------------------------------------------------------- |
| Unlock      | Once per encounter, instantly enter stealth (even observed) |
| Requirement | Must have escape route (can't vanish if surrounded)         |
| Cooldown    | 10 minutes                                                  |

**No scaling—fixed cooldown**
**Trigger Actions:** Escape detection, successfully flee, break line of sight
**Classes:** Thief, Assassin, Scout

---

### Poison Craft (Mechanic Unlock)

Create and apply poisons.

| Effect      | Description                           |
| ----------- | ------------------------------------- |
| Unlock      | Can craft poisons from ingredients    |
| Application | Apply poison to weapons (3-5 attacks) |
| Knowledge   | Identify poison types, know antidotes |

**Tier 2:** Virulent Poisons—poisons 50% stronger, last 5-8 attacks
**Tier 3:** Master Toxicologist—create unique poisons, poison immunity
**Trigger Actions:** Use poisons, harvest venoms, study toxins
**Classes:** Assassin, Alchemist

---

### Thieves' Cant (Mechanic Unlock)

Secret criminal communication.

| Effect      | Description                                             |
| ----------- | ------------------------------------------------------- |
| Unlock      | Understand Thieves' Cant (criminal code language)       |
| Recognition | Identify other criminals, find black market             |
| Marking     | Read and leave thieves' marks (warnings, opportunities) |

**No scaling—binary unlock**
**Trigger Actions:** Interact with criminals, operate in underground
**Classes:** Thief, Assassin, Fence, Smuggler

---

### Silent Kill (Mechanic Unlock)

Eliminate without alerting others.

| Effect     | Description                                                 |
| ---------- | ----------------------------------------------------------- |
| Unlock     | Stealth kills don't alert nearby enemies (within reason)    |
| Body       | Target dies silently, body can be hidden instantly          |
| Limitation | Doesn't work if target in conversation or directly observed |

**No scaling—binary unlock**
**Trigger Actions:** Kill without detection, remain unnoticed after kills
**Classes:** Assassin

---

### Misdirection (Tiered)

Create diversions and false trails.

| Tier     | Effect                                                         | Scaling                |
| -------- | -------------------------------------------------------------- | ---------------------- |
| Lesser   | +20% chance to mislead pursuers, create false trails           | +1% per class level    |
| Greater  | +35% mislead, throw voice/sound, plant false evidence          | +0.75% per class level |
| Enhanced | +50% mislead, frame others convincingly, escape almost certain | +0.5% per class level  |

**Trigger Actions:** Escape pursuit, plant false evidence, mislead investigators
**Classes:** Thief, Assassin, Smuggler

---

### Smuggler's Instinct (Mechanic Unlock)

Innate sense for hiding contraband.

| Effect    | Description                                           |
| --------- | ----------------------------------------------------- |
| Unlock    | Know when searches are coming (brief warning)         |
| Hiding    | +30% effectiveness hiding items on person or in cargo |
| Detection | Sense hidden compartments in others' cargo            |

**No scaling—fixed bonuses**
**Trigger Actions:** Smuggle goods, avoid searches, find hidden items
**Classes:** Smuggler, Thief, Caravaner

---

### Evasion Expert (Tiered)

Superior ability to avoid attacks.

| Tier     | Effect                                                        | Scaling                |
| -------- | ------------------------------------------------------------- | ---------------------- |
| Lesser   | +10% dodge, +15% vs AOE attacks                               | +1% per class level    |
| Greater  | +18% dodge, +30% vs AOE, half damage on failed dodge          | +0.75% per class level |
| Enhanced | +25% dodge, +50% vs AOE, no damage on successful dodge vs AOE | +0.5% per class level  |

**Trigger Actions:** Dodge attacks, avoid traps, evade AOE
**Classes:** Thief, Assassin, Scout, Adventurer

---

## 8. Gathering Pool (Historical)

### Resource Sense (Tiered)

Detect gathering nodes at distance.

| Tier     | Range    | Detail                                | Scaling                   |
| -------- | -------- | ------------------------------------- | ------------------------- |
| Lesser   | Medium   | Sense presence of relevant nodes      | +5% range per class level |
| Greater  | Far      | Sense presence and quality            | +4% range per class level |
| Enhanced | Very Far | Sense presence, quality, and quantity | +3% range per class level |

**Classes:** All Gathering pool (each senses their specialty)

---

### Bountiful Harvest (Tiered)

Increased yield from gathering.

| Tier     | Effect                                                    | Scaling                |
| -------- | --------------------------------------------------------- | ---------------------- |
| Lesser   | +15% yield from gathering                                 | +1% per class level    |
| Greater  | +25% yield, chance of double harvest                      | +0.75% per class level |
| Enhanced | +40% yield, increased double chance, rare bonus materials | +0.5% per class level  |

**Trigger Actions:** Gather resources, maximize node yield
**Classes:** All Gathering pool

---

### Gentle Harvest (Mechanic Unlock)

Gather without depleting.

| Effect      | Description                                       |
| ----------- | ------------------------------------------------- |
| Unlock      | 25% chance node doesn't deplete when gathered     |
| Sustainable | Node regeneration +50% faster if gentle harvested |
| Quality     | No quality penalty                                |

**No scaling—fixed 25% chance**
**Trigger Actions:** Gather from same nodes repeatedly, practice sustainable harvesting
**Classes:** Forager, Herbalist, Farmer

---

### Prospector (Mechanic Unlock)

Discover new resource nodes.

| Effect | Description                                       |
| ------ | ------------------------------------------------- |
| Unlock | Can survey areas to discover hidden/unknown nodes |
| Survey | Takes 1-2 hours per area, reveals all resources   |
| Claim  | First discoverer gets temporary claim             |

**Tier 2:** Master Prospector—survey time halved, detect rare nodes, estimate yields
**Trigger Actions:** Find new nodes, explore unmapped areas
**Classes:** Miner, Forager, Explorer

---

### Deep Vein (Mechanic Unlock)

Access difficult resource deposits.

| Effect      | Description                                           |
| ----------- | ----------------------------------------------------- |
| Unlock      | Can mine/gather from normally inaccessible deposits   |
| Types       | Underwater, cliffsides, deep underground, tree canopy |
| Requirement | Appropriate equipment (rope, breathing, climbing)     |

**No scaling—binary unlock**
**Trigger Actions:** Gather from difficult locations, use specialized techniques
**Classes:** Miner, Forager, Fisher

---

### Efficiency Expert (Tiered)

Reduced stamina and time for gathering.

| Tier     | Effect                                               | Scaling               |
| -------- | ---------------------------------------------------- | --------------------- |
| Lesser   | -15% stamina, -10% time                              | +0.5% per class level |
| Greater  | -25% stamina, -20% time                              | +0.4% per class level |
| Enhanced | -35% stamina, -30% time, auto-gather while traveling | +0.3% per class level |

**Trigger Actions:** Extended gathering sessions, optimize gathering routes
**Classes:** All Gathering pool

---

### Weather Worker (Mechanic Unlock)

Gather in adverse conditions.

| Effect | Description                                                  |
| ------ | ------------------------------------------------------------ |
| Unlock | No gathering penalty from weather conditions                 |
| Bonus  | +10% yield during storms (washed-up materials, flushed game) |
| Safety | Immune to weather damage while gathering                     |

**No scaling—binary unlock**
**Trigger Actions:** Gather during bad weather, adapt to conditions
**Classes:** All Gathering pool

---

### Master Forager (Passive Generator)

Daily passive resource discovery.

| Tier     | Daily Effect                                                  |
| -------- | ------------------------------------------------------------- |
| Lesser   | Find 1-3 common herbs/materials during normal travel          |
| Greater  | Find 2-5 materials including uncommon during travel           |
| Enhanced | Find 3-8 materials including rare, chance of exceptional find |

**No scaling—fixed generation based on tier**
**Trigger Actions:** Consistently gather, develop foraging habits
**Classes:** Forager, Herbalist

---

## 9. Agriculture Pool (Historical)

### Green Thumb (Tiered)

Improved crop growth.

| Tier     | Effect                                               | Scaling                |
| -------- | ---------------------------------------------------- | ---------------------- |
| Lesser   | +15% crop growth speed, +10% yield                   | +1% per class level    |
| Greater  | +25% growth, +20% yield, resist weather damage       | +0.75% per class level |
| Enhanced | +40% growth, +35% yield, crops survive drought/frost | +0.5% per class level  |

**Trigger Actions:** Tend crops, achieve good harvests
**Classes:** Farmer

---

### Animal Husbandry (Tiered)

Better livestock management.

| Tier     | Effect                                               | Scaling                |
| -------- | ---------------------------------------------------- | ---------------------- |
| Lesser   | +15% animal product yield, +10% breeding success     | +1% per class level    |
| Greater  | +25% yield, +20% breeding, animals healthier         | +0.75% per class level |
| Enhanced | +40% yield, +35% breeding, animals bond (won't flee) | +0.5% per class level  |

**Trigger Actions:** Tend animals, breed livestock, manage herds
**Classes:** Rancher, Farmer

---

### Season Sense (Mechanic Unlock)

Perfect agricultural timing.

| Effect     | Description                                         |
| ---------- | --------------------------------------------------- |
| Unlock     | Know optimal planting/harvesting times for any crop |
| Prediction | Predict weather effects on crops 1 week ahead       |
| Adaptation | Know which crops suit current conditions            |

**No scaling—binary unlock**
**Trigger Actions:** Farm through multiple seasons, adapt to conditions
**Classes:** Farmer, Rancher

---

### Pest Resistance (Mechanic Unlock)

Protect crops and animals from pests.

| Effect        | Description                                    |
| ------------- | ---------------------------------------------- |
| Unlock        | -50% pest/disease occurrence on your farm      |
| Early Warning | Detect infestations before they spread         |
| Treatment     | Know remedies for common agricultural diseases |

**No scaling—binary unlock**
**Trigger Actions:** Deal with infestations, protect harvests
**Classes:** Farmer, Rancher, Beekeeper

---

### Surplus Generator (Passive Generator)

Farm produces extra over time.

| Tier     | Weekly Effect                                                    |
| -------- | ---------------------------------------------------------------- |
| Lesser   | Farm produces 5% extra yield passively                           |
| Greater  | 10% extra yield, occasional quality bonus                        |
| Enhanced | 15% extra yield, regular quality bonus, rare exceptional produce |

**Trigger Actions:** Maintain productive farm, optimize operations
**Classes:** Farmer, Rancher

---

### Beekeeper's Bond (Mechanic Unlock)

Special connection with bees.

| Effect        | Description                                     |
| ------------- | ----------------------------------------------- |
| Unlock        | Bees never sting you, +50% honey/wax yield      |
| Communication | Bees warn of dangers, scout local flowers       |
| Defense       | Can direct swarm against threats (once per day) |

**No scaling—binary unlock**
**Classes:** Beekeeper

---

## 10. Animal Pool (Historical)

### Beast Tongue (Mechanic Unlock)

Communicate with animals.

| Effect        | Description                                              |
| ------------- | -------------------------------------------------------- |
| Unlock        | Understand animal emotional states and basic intent      |
| Communication | Convey simple concepts to animals (danger, food, follow) |
| Limitation    | Not mind control—animals may refuse                      |

**Tier 2:** Animal Empathy—understand complex emotions, communicate detailed concepts
**Tier 3:** Beast Speaker—near-verbal communication with intelligent animals
**Trigger Actions:** Work with animals, train creatures, bond with beasts
**Classes:** Animal Trainer, Beastmaster, Rancher

---

### Taming (Mechanic Unlock)

Domesticate wild creatures.

| Effect     | Description                                      |
| ---------- | ------------------------------------------------ |
| Unlock     | Can attempt to tame wild animals                 |
| Process    | Takes days/weeks depending on creature           |
| Limitation | Only tameable species, must be appropriate level |

**Tier 2:** Advanced Taming—tame difficult creatures, reduce time by 50%
**Tier 3:** Master Tamer—tame exotic creatures, instant bond with common animals
**Trigger Actions:** Successfully tame animals, work with wild creatures
**Classes:** Animal Trainer, Beastmaster

---

### Pack Leader (Tiered)

Command animal companions effectively.

| Tier     | Animals | Compliance               | Scaling                |
| -------- | ------- | ------------------------ | ---------------------- |
| Lesser   | 2       | +15%                     | +1% per class level    |
| Greater  | 4       | +25%                     | +0.75% per class level |
| Enhanced | 6       | +35%, animals coordinate | +0.5% per class level  |

**Trigger Actions:** Work with multiple animals, coordinate animal actions
**Classes:** Animal Trainer, Beastmaster, Hunter

---

### Hunting Companion (Mechanic Unlock)

Bond with hunting animal.

| Effect    | Description                                                   |
| --------- | ------------------------------------------------------------- |
| Unlock    | Form permanent bond with one hunting animal (dog, hawk, etc.) |
| Benefits  | Animal has enhanced stats, shares XP, can learn commands      |
| Telepathy | Sense companion's location and status at any distance         |

**No scaling—single bond (can rebond if companion dies)**
**Trigger Actions:** Work with hunting animals, hunt together
**Classes:** Hunter, Trapper, Beastmaster

---

### Scent Mastery (Tiered)

Control and use scent effectively.

| Tier     | Effect                                                             | Scaling                |
| -------- | ------------------------------------------------------------------ | ---------------------- |
| Lesser   | Mask your scent, +15% tracking via scent                           | +1% per class level    |
| Greater  | Create false scent trails, +25% tracking                           | +0.75% per class level |
| Enhanced | Scent invisible to animals, +40% tracking, ID individuals by scent | +0.5% per class level  |

**Trigger Actions:** Track by scent, avoid animal detection
**Classes:** Hunter, Trapper, Beastmaster

---

### Creature Lore (Mechanic Unlock)

Encyclopedic creature knowledge.

| Effect     | Description                                            |
| ---------- | ------------------------------------------------------ |
| Unlock     | Know weaknesses, behaviors, habitats of creature types |
| Assessment | Examine creature to learn stats, abilities             |
| Expansion  | Learn new creatures by studying (dead or alive)        |

**No scaling—expands through study**
**Trigger Actions:** Hunt varied creatures, study animal behavior
**Classes:** Hunter, Scholar, Beastmaster

---

## 11. Crafting Pool (General) (Historical)

### Artisan's Focus (Tiered)

Enhanced crafting concentration.

| Tier     | Effect                                                       | Scaling               |
| -------- | ------------------------------------------------------------ | --------------------- |
| Lesser   | +10% quality, -15% failure chance                            | +0.5% per class level |
| Greater  | +18% quality, -30% failure chance, sense flaws               | +0.4% per class level |
| Enhanced | +25% quality, -50% failure chance, auto-correct minor errors | +0.3% per class level |

**Trigger Actions:** Complete quality crafts, achieve exceptional results
**Classes:** All Crafting specialties

---

### Rapid Crafting (Tiered)

Faster creation speed.

| Tier     | Effect                                   | Scaling               |
| -------- | ---------------------------------------- | --------------------- |
| Lesser   | -15% crafting time                       | +0.5% per class level |
| Greater  | -25% crafting time, batch processing     | +0.4% per class level |
| Enhanced | -40% crafting time, instant simple items | +0.3% per class level |

**Trigger Actions:** Craft under time pressure, complete many items
**Classes:** All Crafting specialties

---

### Material Intuition (Mechanic Unlock)

Sense material potential.

| Effect      | Description                                         |
| ----------- | --------------------------------------------------- |
| Unlock      | Touch materials to know their quality and best uses |
| Combination | Sense which materials combine well                  |
| Hidden      | Detect hidden properties in materials               |

**No scaling—binary unlock**
**Trigger Actions:** Work with varied materials, experiment with combinations
**Classes:** All Crafting specialties

---

### Signature Style (Mechanic Unlock)

Create recognizable work.

| Effect     | Description                                       |
| ---------- | ------------------------------------------------- |
| Unlock     | Your crafted items have identifiable maker's mark |
| Reputation | Items traced to you build crafting reputation     |
| Value      | Recognized work sells for +10-30% more            |
| Commission | NPCs may seek you out for your style              |

**No scaling—binary unlock (value scales with reputation)**
**Trigger Actions:** Create many items, develop consistent quality
**Classes:** All Crafting specialties

---

### Repair Mastery (Tiered)

Superior item repair.

| Tier     | Effect                                                  | Scaling               |
| -------- | ------------------------------------------------------- | --------------------- |
| Lesser   | Repairs restore +15% more durability                    | +0.5% per class level |
| Greater  | Repairs restore +25% more, can repair "destroyed" items | +0.4% per class level |
| Enhanced | Repairs restore +40% more, repairs may improve items    | +0.3% per class level |

**Trigger Actions:** Repair items, restore damaged equipment
**Classes:** All Crafting specialties (for their specialty)

---

### Masterwork Chance (Tiered)

Increased exceptional results.

| Tier     | Effect                                             | Scaling                |
| -------- | -------------------------------------------------- | ---------------------- |
| Lesser   | +3% masterwork chance on crafts                    | +0.2% per class level  |
| Greater  | +6% masterwork chance, +10% superior chance        | +0.15% per class level |
| Enhanced | +10% masterwork, +20% superior, sense when to push | +0.1% per class level  |

**Trigger Actions:** Achieve masterwork results, push for quality
**Classes:** All Crafting specialties

---

### Tool Bond (Mechanic Unlock)

Form connection with crafting tools.

| Effect      | Description                                                |
| ----------- | ---------------------------------------------------------- |
| Unlock      | Bond with one set of tools                                 |
| Benefit     | Bonded tools: +15% quality, -50% degradation, feel "right" |
| Replacement | Can transfer bond (1 week process)                         |

**No scaling—single bond**
**Trigger Actions:** Use same tools extensively, maintain tools carefully
**Classes:** All Crafting specialties

---

### Resource Efficiency (Tiered)

Use fewer materials.

| Tier     | Effect                                      | Scaling               |
| -------- | ------------------------------------------- | --------------------- |
| Lesser   | -10% materials required                     | +0.5% per class level |
| Greater  | -18% materials, salvage from failures       | +0.4% per class level |
| Enhanced | -25% materials, no material loss on failure | +0.3% per class level |

**Trigger Actions:** Craft efficiently, minimize waste
**Classes:** All Crafting specialties

---

### Recipe Innovator (Passive Generator)

Develop new recipes over time.

| Tier     | Effect                                                       |
| -------- | ------------------------------------------------------------ |
| Lesser   | Monthly chance to conceive new recipe variant                |
| Greater  | Bi-weekly chance, variants may be improvements               |
| Enhanced | Weekly chance, can deliberately experiment with +50% success |

**Trigger Actions:** Experiment with crafting, create variations
**Classes:** All Crafting specialties

---

## 12. Smithing Pool (Historical)

### Metal Sense (Mechanic Unlock)

Intuitive understanding of metals.

| Effect      | Description                                               |
| ----------- | --------------------------------------------------------- |
| Unlock      | Know optimal temperature, timing, technique for any metal |
| Assessment  | Identify metal type, purity, origin by inspection         |
| Combination | Know which alloys work (and which are impossible)         |

**No scaling—binary unlock**
**Trigger Actions:** Work with varied metals, experiment with alloys
**Classes:** Blacksmith, Weaponsmith, Armorer

---

### Perfect Temper (Tiered)

Superior heat treatment.

| Tier     | Effect                                                   | Scaling               |
| -------- | -------------------------------------------------------- | --------------------- |
| Lesser   | +10% weapon damage OR +10% armor protection              | +0.5% per class level |
| Greater  | +18% damage/protection, +15% durability                  | +0.4% per class level |
| Enhanced | +25% damage/protection, +25% durability, resist breaking | +0.3% per class level |

**Trigger Actions:** Forge quality weapons/armor, practice heat treatment
**Classes:** Blacksmith, Weaponsmith, Armorer

---

### Forge Fire (Mechanic Unlock)

Work with magical heat.

| Effect      | Description                               |
| ----------- | ----------------------------------------- |
| Unlock      | Can work materials requiring magical fire |
| Materials   | Dragon bone, star metal, demon iron, etc. |
| Requirement | Access to magical fire source             |

**Prerequisite:** Master rank in smithing specialty
**Trigger Actions:** Encounter exotic materials, seek magical forges
**Classes:** Blacksmith, Weaponsmith, Armorer

---

### Weapon Enhancement (Mechanic Unlock)

Add properties during forging.

| Effect     | Description                                              |
| ---------- | -------------------------------------------------------- |
| Unlock     | Can incorporate special materials during weapon creation |
| Properties | Sharpness, balance, weight distribution                  |
| Limitation | Not magical enchantment—physical properties only         |

**Tier 2:** Advanced Enhancement—multiple properties, synergistic combinations
**Trigger Actions:** Forge enhanced weapons, experiment with techniques
**Classes:** Weaponsmith

---

### Armor Fitting (Mechanic Unlock)

Create perfectly fitted armor.

| Effect     | Description                                    |
| ---------- | ---------------------------------------------- |
| Unlock     | Can create armor fitted to specific individual |
| Benefit    | Fitted armor: -50% penalties, +15% protection  |
| Limitation | Fitted armor only works for intended wearer    |

**Trigger Actions:** Create armor, study armor fitting
**Classes:** Armorer

---

## 13. Alchemy Pool (Historical)

### Potent Brews (Tiered)

Stronger alchemical creations.

| Tier     | Effect                                             | Scaling                |
| -------- | -------------------------------------------------- | ---------------------- |
| Lesser   | +15% potion/poison effectiveness                   | +1% per class level    |
| Greater  | +25% effectiveness, +20% duration                  | +0.75% per class level |
| Enhanced | +40% effectiveness, +35% duration, no side effects | +0.5% per class level  |

**Trigger Actions:** Create potions/poisons, experiment with recipes
**Classes:** Alchemist

---

### Stable Solutions (Tiered)

Long-lasting alchemical products.

| Tier     | Effect                                                | Scaling             |
| -------- | ----------------------------------------------------- | ------------------- |
| Lesser   | Potions last 2x longer before expiring                | +5% per class level |
| Greater  | Potions last 5x longer, resist temperature            | +4% per class level |
| Enhanced | Potions effectively permanent, survive any conditions | +3% per class level |

**Trigger Actions:** Store potions long-term, create stable formulas
**Classes:** Alchemist

---

### Transmutation Basics (Mechanic Unlock)

Convert materials.

| Effect     | Description                                      |
| ---------- | ------------------------------------------------ |
| Unlock     | Can convert common materials (iron→copper, etc.) |
| Ratio      | 3:1 or worse conversion ratio                    |
| Limitation | Cannot create rare from common                   |

**Tier 2:** Advanced Transmutation—2:1 ratio, can create uncommon
**Tier 3:** Master Transmutation—1:1 ratio, can create rare (with rare catalyst)
**Trigger Actions:** Experiment with transmutation, study material properties
**Classes:** Alchemist
**Prerequisite:** Master rank

---

### Reagent Cultivation (Passive Generator)

Grow alchemical ingredients.

| Tier     | Weekly Effect                            |
| -------- | ---------------------------------------- |
| Lesser   | Produce 5-10 common reagents             |
| Greater  | Produce 8-15 reagents including uncommon |
| Enhanced | Produce 12-20 reagents including rare    |

**Requires:** Dedicated growing space
**Trigger Actions:** Cultivate reagent garden, tend alchemical plants
**Classes:** Alchemist, Herbalist

---

### Potion Identification (Mechanic Unlock)

Identify unknown potions.

| Effect      | Description                                              |
| ----------- | -------------------------------------------------------- |
| Unlock      | Identify any potion by smell/appearance (no consumption) |
| Analysis    | Determine exact effects, strength, creator skill         |
| Counterfeit | Detect fake or diluted potions                           |

**No scaling—binary unlock**
**Trigger Actions:** Analyze potions, detect fakes
**Classes:** Alchemist

---

### Explosive Compounds (Mechanic Unlock)

Create alchemical explosives.

| Effect | Description                                     |
| ------ | ----------------------------------------------- |
| Unlock | Can craft bombs, grenades, explosive traps      |
| Types  | Fire, smoke, poison, flashbang                  |
| Safety | Handle explosives without accidental detonation |

**No scaling—binary unlock (damage scales with recipe)**
**Trigger Actions:** Experiment with volatile compounds, study explosives
**Classes:** Alchemist
**Note:** May be restricted or monitored

---

## 14. Trade Pool (Historical)

### Market Sense (Tiered)

Know prices and opportunities.

| Tier     | Effect                                               | Scaling                |
| -------- | ---------------------------------------------------- | ---------------------- |
| Lesser   | Know local prices, sense good deals (+15%)           | +1% per class level    |
| Greater  | Know regional prices, predict price changes          | +0.75% per class level |
| Enhanced | Know inter-regional prices, manipulate local markets | +0.5% per class level  |

**Trigger Actions:** Trade frequently, track prices, exploit opportunities
**Classes:** Trader, Merchant, Caravaner

---

### Silver Tongue (Trade) (Tiered)

Superior negotiation in trade.

| Tier     | Effect                                       | Scaling                |
| -------- | -------------------------------------------- | ---------------------- |
| Lesser   | +15% to buy/sell price negotiations          | +1% per class level    |
| Greater  | +25% negotiations, can haggle "fixed" prices | +0.75% per class level |
| Enhanced | +40% negotiations, create exclusive deals    | +0.5% per class level  |

**Trigger Actions:** Negotiate trades, achieve exceptional deals
**Classes:** Trader, Merchant

---

### Trade Network (Passive Generator)

Generate trade opportunities.

| Tier     | Effect                                                 |
| -------- | ------------------------------------------------------ |
| Lesser   | Weekly: Learn of 1-2 trade opportunities               |
| Greater  | Weekly: 2-4 opportunities, including profitable routes |
| Enhanced | Weekly: 3-6 opportunities, including exclusive deals   |

**Trigger Actions:** Maintain trade contacts, build reputation
**Classes:** Trader, Merchant, Caravaner

---

### Caravan Master (Mechanic Unlock)

Lead trade expeditions.

| Effect     | Description                                   |
| ---------- | --------------------------------------------- |
| Unlock     | Can lead caravan of any size without penalty  |
| Efficiency | -25% travel time, -30% supply consumption     |
| Protection | +25% detect ambush, better guard coordination |

**No scaling—binary unlock**
**Trigger Actions:** Lead caravans, complete trade routes
**Classes:** Caravaner

---

### Fencing (Mechanic Unlock)

Move stolen goods.

| Effect  | Description                                 |
| ------- | ------------------------------------------- |
| Unlock  | Can sell stolen items to appropriate buyers |
| Network | Know fences in visited settlements          |
| Price   | Get 40% of value (vs 20% for non-fences)    |

**Tier 2:** Expert Fence—get 60% value, can fence notable items
**Tier 3:** Master Fence—get 80% value, can fence anything, no questions
**Trigger Actions:** Sell stolen goods, build underground contacts
**Classes:** Fence, Thief

---

### Commodity Speculation (Mechanic Unlock)

Invest in future prices.

| Effect      | Description                                |
| ----------- | ------------------------------------------ |
| Unlock      | Can buy commodity futures in major markets |
| Risk        | May profit greatly or lose investment      |
| Information | Market Sense skill affects success         |

**No scaling—returns based on market conditions**
**Trigger Actions:** Trade in volume, predict market changes
**Classes:** Merchant
**Prerequisite:** Merchant specialization

---

### Trade Contacts (Passive Generator)

Build useful contacts over time.

| Tier     | Monthly Effect                                        |
| -------- | ----------------------------------------------------- |
| Lesser   | Gain 1 trade contact in visited settlements           |
| Greater  | Gain 2 contacts, contacts offer better deals          |
| Enhanced | Gain 3 contacts, contacts share exclusive information |

**Trigger Actions:** Trade with varied merchants, maintain relationships
**Classes:** Trader, Merchant, Caravaner

---

## 15. Magic Pool (Historical)

### Spell Focus (Tiered)

Enhanced spell effectiveness.

| Tier     | Effect                                      | Scaling               |
| -------- | ------------------------------------------- | --------------------- |
| Lesser   | +10% spell damage/healing, -10% mana cost   | +0.5% per class level |
| Greater  | +18% effect, -18% mana cost                 | +0.4% per class level |
| Enhanced | +25% effect, -25% mana cost, faster casting | +0.3% per class level |

**Trigger Actions:** Cast spells, practice magic
**Classes:** All Magic pool

---

### Mana Well (Tiered)

Expanded magical reserves.

| Tier     | Effect                                             | Scaling                |
| -------- | -------------------------------------------------- | ---------------------- |
| Lesser   | +15% mana pool                                     | +1% per class level    |
| Greater  | +25% mana pool, +10% regen                         | +0.75% per class level |
| Enhanced | +40% mana pool, +20% regen, deep reserves mechanic | +0.5% per class level  |

**Trigger Actions:** Deplete mana fully, practice extended casting
**Classes:** All Magic pool

---

### Spell Weaving (Mechanic Unlock)

Combine spell effects.

| Effect     | Description                                   |
| ---------- | --------------------------------------------- |
| Unlock     | Can combine two known spells into single cast |
| Cost       | Combined mana cost +25%                       |
| Limitation | Spells must be compatible schools             |

**Prerequisite:** Journeyman in two spell schools
**Trigger Actions:** Master multiple schools, experiment with combinations
**Classes:** Mage

---

### Ritual Casting (Mechanic Unlock)

Cast powerful extended spells.

| Effect  | Description                                       |
| ------- | ------------------------------------------------- |
| Unlock  | Can perform ritual versions of spells             |
| Benefit | 3x effect, extended duration, area effect         |
| Cost    | 10x casting time, 2x mana, may require components |

**No scaling—binary unlock**
**Trigger Actions:** Study rituals, perform complex magic
**Classes:** All Magic pool

---

### School Mastery (Mechanic Unlock)

Deep expertise in one school.

| Effect     | Description                                         |
| ---------- | --------------------------------------------------- |
| Unlock     | Choose one spell school for mastery                 |
| Benefit    | School spells: +30% effect, -30% cost, learn faster |
| Limitation | One school only (can be changed with great effort)  |

**No scaling—fixed bonus**
**Trigger Actions:** Focus on single school, achieve Mage rank in school
**Classes:** Mage, Enchanter, Necromancer

---

### Mana Transfer (Mechanic Unlock)

Share mana with others.

| Effect | Description                                       |
| ------ | ------------------------------------------------- |
| Unlock | Can transfer mana to willing magic-capable target |
| Ratio  | 2:1 (spend 2 mana to give 1)                      |
| Touch  | Requires physical contact                         |

**Tier 2:** Efficient Transfer—1.5:1 ratio, short range
**Tier 3:** Master Transfer—1:1 ratio, medium range
**Trigger Actions:** Assist other casters, practice mana manipulation
**Classes:** All Magic pool

---

### Counter Magic (Tiered)

Disrupt enemy spells.

| Tier     | Effect                                      | Scaling                |
| -------- | ------------------------------------------- | ---------------------- |
| Lesser   | Can attempt to counter spells (30% success) | +1% per class level    |
| Greater  | 45% counter success, sense incoming spells  | +0.75% per class level |
| Enhanced | 60% counter success, reflect chance         | +0.5% per class level  |

**Trigger Actions:** Attempt counters, fight enemy casters
**Classes:** Mage, Enchanter

---

### Enchant Object (Mechanic Unlock)

Permanently enchant items.

| Effect | Description                               |
| ------ | ----------------------------------------- |
| Unlock | Can place permanent enchantments on items |
| Cost   | Materials, mana, time (hours to days)     |
| Limit  | Skill determines enchantment power        |

**Note:** Core Enchanter class mechanic—offered early
**Trigger Actions:** Study enchanted items, practice enchanting
**Classes:** Enchanter

---

### Undead Binding (Mechanic Unlock)

Control undead creatures.

| Effect | Description                                   |
| ------ | --------------------------------------------- |
| Unlock | Can bind and control raised undead            |
| Limit  | Control limit based on class level            |
| Types  | Starts with basic (zombie, skeleton), expands |

**Note:** Core Necromancer class mechanic
**Trigger Actions:** Raise undead, maintain undead servants
**Classes:** Necromancer

---

## 16. Shamanic Pool (Historical)

### Spirit Communion (Mechanic Unlock)

Communicate with tribal spirits.

| Effect   | Description                                               |
| -------- | --------------------------------------------------------- |
| Unlock   | Can communicate with spirits in tribal pool               |
| Benefits | Request guidance, learn tribal history, sense spirit mood |
| Ritual   | Requires brief ritual (10-30 minutes)                     |

**Note:** Core Shaman mechanic—offered at class acceptance
**Trigger Actions:** Perform rituals, tend to spirits
**Classes:** Shaman

---

### Expanded Spirit Reach (Tiered)

Share shamanic benefits beyond tribe.

| Tier     | Effect                                             | Scaling                |
| -------- | -------------------------------------------------- | ---------------------- |
| Lesser   | Can apply war paint to non-tribal allies           | Duration +5% per level |
| Greater  | Can create charms for non-tribal, half potency     | Duration +4% per level |
| Enhanced | Full potency for non-tribal, can teach basic rites | Duration +3% per level |

**Trigger Actions:** Work with non-tribal allies, share tribal benefits
**Classes:** Shaman

---

### Spirit Binding (Mechanic Unlock)

Bind new spirits to tribe.

| Effect  | Description                                       |
| ------- | ------------------------------------------------- |
| Unlock  | Can bind defeated/willing spirits to tribal pool  |
| Process | Ritual combat or negotiation                      |
| Limit   | Pool can only hold spirits up to tribe's capacity |

**Prerequisite:** Master Shaman rank
**Trigger Actions:** Encounter spirits, grow tribal pool
**Classes:** Shaman

---

### War Painter (Tiered)

Enhanced war paint creation.

| Tier     | Effect                                              | Scaling                |
| -------- | --------------------------------------------------- | ---------------------- |
| Lesser   | +15% war paint effectiveness, +25% duration         | +1% per class level    |
| Greater  | +25% effectiveness, +50% duration, complex patterns | +0.75% per class level |
| Enhanced | +40% effectiveness, 2x duration, layered paints     | +0.5% per class level  |

**Trigger Actions:** Create war paints, paint warriors for battle
**Classes:** Shaman, War Painter

---

### Totem Crafting (Mechanic Unlock)

Create tribal totems.

| Effect | Description                                   |
| ------ | --------------------------------------------- |
| Unlock | Can craft totems that provide area benefits   |
| Types  | Camp totem, war totem, sacred totem           |
| Power  | Totem strength based on spirit pool and skill |

**Note:** Core Shaman crafting mechanic
**Trigger Actions:** Create totems, maintain sacred items
**Classes:** Shaman

---

### Ancestral Memory (Mechanic Unlock)

Access tribal knowledge.

| Effect      | Description                                          |
| ----------- | ---------------------------------------------------- |
| Unlock      | Can consult ancestors for knowledge                  |
| Information | History, locations, techniques known to honored dead |
| Limitation  | Ancestors may not know, or may not share             |

**No scaling—quality based on relationship with ancestors**
**Trigger Actions:** Honor ancestors, maintain tribal traditions
**Classes:** Shaman

---

### Spirit Walk (Mechanic Unlock)

Project spirit to observe.

| Effect     | Description                                         |
| ---------- | --------------------------------------------------- |
| Unlock     | Can send spirit to observe distant locations        |
| Range      | Within tribal territory (further with higher skill) |
| Limitation | Body vulnerable while projecting                    |

**Tier 2:** Extended Walk—observe anywhere known to tribe
**Tier 3:** Spirit Journey—interact minimally while projected
**Trigger Actions:** Practice projection, commune with spirits
**Classes:** Shaman

---

### Fetish Creation (Mechanic Unlock)

Create permanent spirit items.

| Effect  | Description                                    |
| ------- | ---------------------------------------------- |
| Unlock  | Can create fetishes (permanent shamanic items) |
| Process | Bind spirit into item permanently              |
| Power   | Equivalent to enchanted items                  |

**Prerequisite:** Master Shaman rank
**Trigger Actions:** Create powerful shamanic items, bind willing spirits
**Classes:** Shaman

---

## 17. Leadership Pool (Historical)

### Inspiring Leader (Tiered)

Enhance follower capabilities.

| Tier     | Effect                                            | Range    | Scaling               |
| -------- | ------------------------------------------------- | -------- | --------------------- |
| Lesser   | Followers +10% all stats                          | Medium   | +0.5% per class level |
| Greater  | Followers +18% stats, +15% morale                 | Far      | +0.4% per class level |
| Enhanced | Followers +25% stats, +25% morale, immune to fear | Very Far | +0.3% per class level |

**Trigger Actions:** Lead followers, inspire through action
**Classes:** Leader, Chieftain, Knight

---

### Authority (Tiered)

Commands carry weight.

| Tier     | Effect                                                                           | Scaling                |
| -------- | -------------------------------------------------------------------------------- | ---------------------- |
| Lesser   | +20% order compliance, orders from distance                                      | +1% per class level    |
| Greater  | +35% compliance, complex orders, non-party listens                               | +0.75% per class level |
| Enhanced | +50% compliance, enemies may follow orders (surrender), instant obedience option | +0.5% per class level  |

**Trigger Actions:** Issue commands, maintain authority
**Classes:** Leader, Chieftain, Guildmaster

---

### Delegation (Mechanic Unlock)

Assign tasks efficiently.

| Effect     | Description                                              |
| ---------- | -------------------------------------------------------- |
| Unlock     | Can assign tasks to followers that complete autonomously |
| Efficiency | Delegated tasks complete at 80% your efficiency          |
| Limit      | Number of simultaneous delegations = Leadership skill    |

**Tier 2:** Efficient Delegation—90% efficiency, more simultaneous tasks
**Trigger Actions:** Manage followers, assign complex tasks
**Classes:** Leader, Guildmaster, Settlement Leader

---

### Loyalty Cultivation (Tiered)

Build deeper loyalty faster.

| Tier     | Effect                                         | Scaling                |
| -------- | ---------------------------------------------- | ---------------------- |
| Lesser   | +25% loyalty gain rate                         | +1% per class level    |
| Greater  | +40% loyalty gain, -25% loyalty loss           | +0.75% per class level |
| Enhanced | +60% loyalty gain, -50% loss, inspire devotion | +0.5% per class level  |

**Trigger Actions:** Build relationships, maintain follower trust
**Classes:** Leader, Chieftain

---

### Crisis Leadership (Mechanic Unlock)

Excel in emergencies.

| Effect    | Description                                      |
| --------- | ------------------------------------------------ |
| Unlock    | During crises, all leadership bonuses doubled    |
| Detection | Automatically recognize crisis situations        |
| Cooldown  | Cannot be constantly "in crisis"—must be genuine |

**No scaling—binary unlock**
**Trigger Actions:** Lead through crises, maintain composure under pressure
**Classes:** Leader, Chieftain, Knight, Settlement Leader

---

### Policy Implementation (Mechanic Unlock)

Enact settlement policies.

| Effect        | Description                                              |
| ------------- | -------------------------------------------------------- |
| Unlock        | Can set and modify settlement policies                   |
| Types         | Taxes, corruption, enforcement, trade, guild permissions |
| Effectiveness | Policy effects scale with leadership skill               |

**Note:** Core Settlement Leader mechanic
**Trigger Actions:** Manage settlements, govern populations
**Classes:** Settlement Leader, Guildmaster

---

### Succession Planning (Mechanic Unlock)

Prepare for leadership transition.

| Effect     | Description                                              |
| ---------- | -------------------------------------------------------- |
| Unlock     | Can designate and train successor                        |
| Benefit    | Successor gains +25% leadership XP when working with you |
| Transition | Smooth succession (no loyalty loss) when you step down   |

**No scaling—binary unlock**
**Trigger Actions:** Mentor subordinates, plan for future
**Classes:** Leader, Chieftain, Guildmaster

---

## 18. Scholar Pool (Historical)

### Rapid Learning (Tiered)

Accelerated knowledge acquisition.

| Tier     | Effect                                               | Scaling                |
| -------- | ---------------------------------------------------- | ---------------------- |
| Lesser   | +20% XP from study/training                          | +1% per class level    |
| Greater  | +35% study XP, learn recipes/skills faster           | +0.75% per class level |
| Enhanced | +50% study XP, can learn skills above "normal" level | +0.5% per class level  |

**Trigger Actions:** Study, train, learn new things
**Classes:** Scholar, Trainer

---

### Perfect Memory (Mechanic Unlock)

Never forget learned information.

| Effect   | Description                                       |
| -------- | ------------------------------------------------- |
| Unlock   | Perfectly recall anything intentionally memorized |
| Capacity | Limited by WIT (10 × WIT "memory slots")          |
| Types    | Texts, maps, conversations, faces, recipes        |

**No scaling—capacity based on WIT**
**Trigger Actions:** Memorize information, practice recall
**Classes:** Scholar, Scribe

---

### Research (Mechanic Unlock)

Discover information through study.

| Effect   | Description                                              |
| -------- | -------------------------------------------------------- |
| Unlock   | Can research topics in libraries/archives                |
| Outcomes | Learn hidden information, recipes, locations, weaknesses |
| Time     | Hours to days depending on topic rarity                  |

**Note:** Core Scholar mechanic
**Trigger Actions:** Use libraries, investigate topics
**Classes:** Scholar, Scribe

---

### Teaching (Tiered)

Train others effectively.

| Tier     | Effect                                                  | Scaling                |
| -------- | ------------------------------------------------------- | ---------------------- |
| Lesser   | Students learn +15% faster                              | +1% per class level    |
| Greater  | Students +25% faster, can teach advanced material       | +0.75% per class level |
| Enhanced | Students +40% faster, can teach multiple simultaneously | +0.5% per class level  |

**Trigger Actions:** Train others, teach skills
**Classes:** Trainer, Scholar

---

### Skill Transference (Mechanic Unlock)

Teach skills directly.

| Effect  | Description                                    |
| ------- | ---------------------------------------------- |
| Unlock  | Can teach skills you know to eligible students |
| Process | Time based on skill tier and student aptitude  |
| Limit   | Can only teach skills at your tier or lower    |

**Note:** Core Trainer mechanic
**Trigger Actions:** Teach skills, mentor students
**Classes:** Trainer

---

### Linguistic Mastery (Mechanic Unlock)

Learn and use languages.

| Effect | Description                                         |
| ------ | --------------------------------------------------- |
| Unlock | Can learn new languages through study               |
| Speed  | Learn basic communication in days, fluency in weeks |
| Bonus  | +25% to learning additional languages               |

**No scaling—binary unlock**
**Trigger Actions:** Study languages, communicate across cultures
**Classes:** Scholar

---

### Ancient Knowledge (Passive Generator)

Uncover forgotten lore.

| Tier     | Monthly Effect                                  |
| -------- | ----------------------------------------------- |
| Lesser   | Recall 1-2 relevant obscure facts               |
| Greater  | Recall 2-4 facts, including useful applications |
| Enhanced | Recall 3-6 facts, may include lost techniques   |

**Trigger Actions:** Study ancient texts, research history
**Classes:** Scholar

---

## 19. Dark Pool (Historical)

### Underworld Connections (Passive Generator)

Criminal network contacts.

| Tier     | Effect                                                |
| -------- | ----------------------------------------------------- |
| Lesser   | Weekly: Learn of 1-2 criminal opportunities           |
| Greater  | Weekly: 2-4 opportunities, fence access, safe houses  |
| Enhanced | Weekly: 3-6 opportunities, exclusive jobs, protection |

**Trigger Actions:** Operate in underground, build criminal reputation
**Classes:** All Dark pool

---

### Extortion (Mechanic Unlock)

Extract payment through threats.

| Effect      | Description                                |
| ----------- | ------------------------------------------ |
| Unlock      | Can extort NPCs for regular payments       |
| Calculation | Based on target wealth, your threat level  |
| Risk        | May report to authorities, hire protection |

**Tier 2:** Professional Extortion—better rates, less resistance
**Trigger Actions:** Threaten for profit, maintain intimidation
**Classes:** Extortionist, Thief

---

### Contract Killing (Mechanic Unlock)

Accept assassination jobs.

| Effect     | Description                                     |
| ---------- | ----------------------------------------------- |
| Unlock     | Access to assassination contracts through guild |
| Payment    | Based on target difficulty and profile          |
| Reputation | Build assassin reputation (separate track)      |

**Note:** Requires Assassins Guild membership
**Trigger Actions:** Complete assassinations, build reputation
**Classes:** Assassin

---

### Money Laundering (Mechanic Unlock)

Clean dirty money.

| Effect  | Description                                          |
| ------- | ---------------------------------------------------- |
| Unlock  | Can convert "marked" or suspicious currency to clean |
| Rate    | Lose 15-25% in process                               |
| Benefit | Clean money can't be traced                          |

**Tier 2:** Efficient Laundering—lose only 10-15%
**Trigger Actions:** Handle large amounts of criminal money
**Classes:** Fence, Merchant (dark path)

---

### Blackmail (Mechanic Unlock)

Leverage secrets for gain.

| Effect  | Description                                               |
| ------- | --------------------------------------------------------- |
| Unlock  | Can blackmail NPCs with discovered secrets                |
| Returns | Regular payments, favors, information                     |
| Risk    | Target may retaliate, expose you first, or pay to silence |

**No scaling—returns based on secret value**
**Trigger Actions:** Discover secrets, leverage information
**Classes:** Thief, Assassin, Spy

---

### False Identity (Mechanic Unlock)

Maintain alternate persona.

| Effect      | Description                                       |
| ----------- | ------------------------------------------------- |
| Unlock      | Can create and maintain false identity            |
| Benefits    | Separate reputation, escape consequences          |
| Maintenance | Must spend time in persona, keep stories straight |

**Tier 2:** Multiple Identities—maintain 2-3 false identities
**Tier 3:** Master of Masks—seamless switching, nearly undetectable
**Trigger Actions:** Operate under false pretenses, maintain cover
**Classes:** Thief, Assassin, Smuggler, Spy

---

### Evidence Elimination (Tiered)

Remove traces of crimes.

| Tier     | Effect                                                | Scaling                |
| -------- | ----------------------------------------------------- | ---------------------- |
| Lesser   | -25% chance of evidence at crime scene                | +1% per class level    |
| Greater  | -50% evidence chance, can remove existing evidence    | +0.75% per class level |
| Enhanced | -75% evidence, remove evidence remotely, frame others | +0.5% per class level  |

**Trigger Actions:** Commit crimes cleanly, remove evidence
**Classes:** All Dark pool

---

## 20. Cross-Class Skills (Historical)

Some skills are available to multiple unrelated classes based on thematic fit.

### Water Breathing (Mechanic Unlock)

Breathe underwater indefinitely.

| Effect        | Description                           |
| ------------- | ------------------------------------- |
| Unlock        | Can breathe underwater without limit  |
| Depth         | No pressure damage to moderate depths |
| Communication | Can speak underwater (muffled)        |

**Classes:** Fisher, Sailor, certain Mage schools
**Trigger Actions:** Extended underwater activity, near-drowning survival

---

### Night Vision (Mechanic Unlock)

See in complete darkness.

| Effect     | Description                         |
| ---------- | ----------------------------------- |
| Unlock     | Full vision in any darkness         |
| Quality    | Color vision, full detail           |
| Limitation | Bright light causes brief blindness |

**Classes:** Thief, Scout, Miner, certain races (improved version)
**Trigger Actions:** Extended time in darkness, adapt to underground

---

### Ambidexterity (Mechanic Unlock)

Use either hand equally.

| Effect   | Description                        |
| -------- | ---------------------------------- |
| Unlock   | No off-hand penalty for any action |
| Combat   | Full damage with either weapon     |
| Crafting | Can work with either hand          |

**Classes:** Warrior, Thief, Crafter specialties
**Trigger Actions:** Practice off-hand, train both hands

---

### Danger Sense (Enhanced) (Mechanic Unlock)

Supernatural threat awareness.

| Effect  | Description                                  |
| ------- | -------------------------------------------- |
| Unlock  | Sense danger before it manifests             |
| Types   | Traps, ambushes, betrayal, natural disasters |
| Warning | Brief (seconds) but reliable                 |

**Classes:** Scout, Assassin, Adventurer, certain Mage paths
**Trigger Actions:** Survive ambushes, avoid disasters

---

### Iron Will (Mechanic Unlock)

Immunity to mental control.

| Effect     | Description                               |
| ---------- | ----------------------------------------- |
| Unlock     | Immune to charm, domination, mind control |
| Resistance | +50% vs other mental effects              |
| Detection  | Know when mental attack attempted         |

**Classes:** Knight, certain Mage schools, Shaman
**Trigger Actions:** Resist mental attacks, maintain mental discipline

---

## 21. XP-Based Progression and Synergy Scaling

### Continuous Improvement (No Discrete Levels)

Skills track XP and improve continuously without level thresholds:

**Key Principles**:

- Skills accumulate XP from usage and training
- Benefits scale smoothly with XP (not in discrete jumps)
- No "skill levels" - just continuous improvement
- XP determines effectiveness, costs, and success rates
- Techniques unlock at specific XP milestones

### Synergy Impact on XP Acquisition

Characters with relevant classes gain XP faster based on logical relationship:

| Synergy Strength   | XP Multiplier | Class Level Scaling     | Example Classes + Skills                         |
| ------------------ | ------------- | ----------------------- | ------------------------------------------------ |
| Strong (Direct)    | 1.5x - 2.5x   | Scales with class level | Blacksmith + Heat Reading, Farmer + Crop Growth  |
| Moderate (Related) | 1.25x - 1.75x | Scales with class level | Miner + Heat Reading, Hunter + Tracker           |
| None (Unrelated)   | 1.0x (base)   | No scaling              | Warrior + Heat Reading, Scholar + Ore Harvesting |

### Class Level Impact on Synergies

Higher class levels provide stronger synergy bonuses:

**Example: Blacksmith + Heat Reading (Strong Synergy)**

| Class Level | XP Multiplier | Effectiveness Bonus | Cost Reduction |
| ----------- | ------------- | ------------------- | -------------- |
| Level 5     | 1.5x (+50%)   | +15%                | -15% stamina   |
| Level 10    | 1.75x (+75%)  | +20%                | -20% stamina   |
| Level 15    | 2.0x (+100%)  | +25%                | -25% stamina   |
| Level 30+   | 2.5x (+150%)  | +35%                | -35% stamina   |

### Example Progression: Heat Reading

**Character**: Blacksmith 15 (strong synergy) vs Warrior 15 (no synergy)

| Accumulated XP          | Blacksmith 15 (2x XP)          | Warrior 15 (1x XP)       |
| ----------------------- | ------------------------------ | ------------------------ |
| Actions to 500 XP       | ~50 actions (10 XP each)       | ~100 actions (5 XP each) |
| Effectiveness at 500 XP | +20% (base +15% + synergy +5%) | +15% (base only)         |
| Stamina cost at 500 XP  | 7.5 (25% reduction)            | 10 (no reduction)        |
| XP to availability      | ~500 XP (~50 actions)          | ~1500 XP (~300 actions)  |

### Specialist Advantage

Synergy-based skills reward specialization:

- **Specialist** (Blacksmith 30): Gets 2.5x XP multiplier + 35% effectiveness bonus + 35% cost reduction
- **Generalist** (Blacksmith 10/Miner 10/Warrior 10): Gets 1.75x XP multiplier + 20% effectiveness + 20% cost reduction (based on Blacksmith 10)
- **Unrelated** (Warrior 30): Gets 1x XP multiplier, no effectiveness/cost bonuses

The specialist learns faster AND performs better with synergy skills.

### Tier Benefits

Tier determines base effect power, but XP drives continuous improvement:

| Tier     | Base Effect Example | XP-Driven Improvement                             |
| -------- | ------------------- | ------------------------------------------------- |
| Lesser   | +10% base bonus     | Scales to +15% with XP, then upgrades to Greater  |
| Greater  | +18% base bonus     | Scales to +25% with XP, then upgrades to Enhanced |
| Enhanced | +25% base bonus     | Scales to +40%+ with XP, no upgrade cap           |

Higher tiers start stronger and scale better with XP accumulation.

---

## 22. Acquisition Summary

### Three Acquisition Paths

**1. Level-Up Awards**:

- Skills offered at class level-up from synergy pools
- Offer rates influenced by class relationship and accumulated XP
- Strong synergy skills: Higher offer rate (~60-70%)
- Unrelated skills: Lower offer rate (~20-30%)
- Excess XP improves tier quality

**2. Training** (Direct skill development):

- Any character can train for any skill
- Guided training (with trainer): ~200 XP to unlock, standard initial boost
- Unguided training (self-taught): ~400 XP to unlock, +60% better initial boost
- Synergy classes train faster (2x XP multiplier for strong synergy)

**3. Action-Based** (Performance unlocks):

- Performing skill-related actions accumulates progress
- Hybrid threshold: ~50 actions ±20% variance (40-60 actual)
- Synergy classes progress faster and may unlock at higher tier
- Not deterministic - adds unpredictability

### Level-Up Availability Thresholds

Skills become available at level-up based on accumulated XP:

| Synergy Strength   | XP Threshold | Typical Actions  |
| ------------------ | ------------ | ---------------- |
| Strong (Direct)    | ~500 XP      | ~50 actions      |
| Moderate (Related) | ~1000 XP     | ~75 actions      |
| None (Unrelated)   | ~1500 XP     | ~100-150 actions |

### Prerequisites

Some skills require:

- Minimum class level (for level-up awards)
- Previous skill tier (for upgrades)
- Specific class rank (Journeyman, Master)
- Other unlocked mechanics

### Mechanic Unlocks

Core class mechanics often offered at:

- Class acceptance (fundamental abilities)
- Milestone levels (10, 20, 30)
- Achievement of specific actions
- Master rank in class
- Can also be trained or unlocked through actions

---

## 23. Integration Points

| System               | Integration                                                                             |
| -------------------- | --------------------------------------------------------------------------------------- |
| Classes              | Provide context-specific synergies through hierarchical tags, not gates                 |
| Hierarchical Tags    | Define skill-class relationships with inheritance (Category/Subcategory/Specialization) |
| Tag-Based Synergies  | Classes get bonuses based on tag matches (2x XP, effectiveness, cost reductions)        |
| Multi-Tag Skills     | Skills can have multiple tags for cross-domain synergies                                |
| Common Skills        | Class skills generally superior in specialty with synergies                             |
| XP-Based Progression | Skills improve continuously with accumulated XP, no discrete levels                     |
| Multiple Paths       | Level-up (class-based thresholds), training (guided/unguided), actions                  |
| Training Types       | Guided (faster) vs Unguided (slower unlock, +60% better initial boost)                  |
| Class Level Scaling  | Synergy strength scales with class level (Level 30 > Level 5)                           |
| Tag Inheritance      | Classes with specific tags automatically access parent tags                             |
| Mechanic Unlocks     | Enable new gameplay possibilities, acquirable via all three paths                       |
| Generators           | Provide passive resource/opportunity flow                                               |
| Cross-Domain         | Multi-tag skills synergize with multiple related classes                                |
| Tier System          | Direct award, upgrade path, or level-up influence - no tier locking                     |
| Universal Access     | Any character can learn any skill - synergies enhance but don't restrict                |
| Type Organization    | Skills organized by type (tiered, mechanic-unlock, passive-generator, cooldown)         |
