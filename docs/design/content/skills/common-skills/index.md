---
title: Common Skills
gdd_ref: systems/skill-recipe-system-gdd.md#skills
---

# Common Skills

> **Note**: Individual skill files are now organized in the `/common/` directory (60+ files).
> This document provides overview and mechanics. For specific skills, browse the [common directory](common/).

## 1. Overview

Common skills are universally accessible abilities that any character can develop. They represent fundamental capabilities applicable across all classes and playstyles. "Common" means widely applicable, not limited in power or importance.

| Principle            | Description                                                         |
| -------------------- | ------------------------------------------------------------------- |
| Universal Access     | Any character can learn through level-up, training, or actions      |
| Foundation           | Fundamental competencies that support all playstyles                |
| Tiered Growth        | Most have Lesser → Greater → Enhanced progression                   |
| No Prerequisites     | No class or race requirements                                       |
| Multiple Paths       | Acquire via level-up, training (guided/unguided), or action-based   |
| XP-Based Progression | Skills track XP and improve continuously (no discrete skill levels) |
| Class Synergies      | Some common skills have moderate synergies with related classes     |

---

## 2. Skill Types Reference

| Type     | Behavior                    | Stamina                    |
| -------- | --------------------------- | -------------------------- |
| Passive  | Always active               | None                       |
| Toggle   | On/off until switched       | Ongoing drain while active |
| Cooldown | Trigger, wait, reuse        | On activation              |
| Duration | Active for time period      | On activation              |
| Charges  | Limited uses, then depleted | Per use                    |

---

## 3. Skill Progression System

### Acquisition Paths

Common skills can be acquired through three methods:

**1. Level-Up Awards** (from common pool):

- Skills offered at any class level-up
- **Universal threshold**: ~500 XP (~50 actions) for most common skills
- Some skills may have moderate synergies with certain classes (lower threshold)
- Excess contributing actions/XP improve tier quality

**2. Training** (direct skill development):

- Any character can train for any common skill
- **Guided Training** (with trainer): Standard progression (~200 XP to unlock), normal initial boost
- **Unguided Training** (self-taught): Slower (~400 XP to unlock), but **+60% better initial boost**
- No class requirement, training accessible to all
- Skills with moderate class synergies train slightly faster

**3. Action-Based** (performing required actions):

- Performing skill-related actions accumulates progress
- **Hybrid threshold**: ~50 actions ±20% variance (40-60 actual range)
- When threshold reached, skill is unlocked
- Not deterministic - adds unpredictability

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

**Example**: Quick Reflexes at 0 XP vs 500 XP vs 2000 XP shows continuously improving dodge chance, not discrete levels.

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

### Class Synergies (Moderate, Where Applicable)

Most common skills are truly universal with no synergies. However, some common skills have moderate synergies with related classes:

**Examples of Moderate Synergies**:

| Common Skill     | Synergy Classes   | XP Multiplier | Reasoning                      |
| ---------------- | ----------------- | ------------- | ------------------------------ |
| Quick Reflexes   | Thief, Warrior    | 1.25x (+25%)  | Combat/stealth classes benefit |
| Endurance Runner | Caravaner, Scout  | 1.25x (+25%)  | Travel-focused classes         |
| Heavy Lifter     | Miner, Blacksmith | 1.25x (+25%)  | Strength-focused trades        |
| Keen Observer    | Scout, Scholar    | 1.25x (+25%)  | Observation-focused classes    |

**Most common skills have NO synergies** - they benefit equally from all experience.

### Tier Acquisition

**Three ways to gain tiered skills**:

1. **Direct Award**: Can be awarded any tier at unlock based on accumulated XP
2. **Upgrade Path**: Having Lesser makes Greater +40% easier, Greater makes Enhanced +40% easier
3. **Level-Up Influence**: Excess contributing actions improve tier award chances

See [class-progression.md](../../systems/character/class-progression/index.md#5-skill-tiers) for full tier mechanics.

### Universal Accessibility

Common skills represent fundamental capabilities that all characters develop:

- **No class gates**: Any character can learn any common skill
- **Equal XP rates**: Most have 1x multiplier for all characters
- **Modest synergies**: A few skills have 1.25x multiplier for related classes
- **Breadth rewards generalists**: Characters with varied experience excel at common skills

---

## 4. Action-Based XP and Availability

Performing relevant actions accumulates XP toward skills, affecting both **level-up availability** and **tier quality**.

### XP Accumulation Flow

```
Perform relevant action
    ↓
Gain skill XP (1-10 XP per action)
    ↓
XP accumulation affects:
├── Level-up availability (skill appears when threshold reached)
├── Tier quality (higher XP = better tier chances)
├── Action-based unlock (direct unlock at ~50 actions)
└── Continues accumulating even after unlock
```

### Level-Up Availability by XP

| Accumulated XP | Level-Up Status    | Tier Probability                                           |
| -------------- | ------------------ | ---------------------------------------------------------- |
| 0-200 XP       | Not yet available  | N/A                                                        |
| 200-500 XP     | Becoming available | Offered if level-up: 70% Lesser, 25% Greater, 5% Enhanced  |
| 500-1000 XP    | Readily available  | Offered if level-up: 45% Lesser, 45% Greater, 10% Enhanced |
| 1000+ XP       | High availability  | Offered if level-up: 25% Lesser, 50% Greater, 25% Enhanced |

### XP Accumulation Examples

**Example 1: Quick Reflexes**

- Character dodges attacks in combat: +5 XP per dodge
- After 50 dodges: 250 XP accumulated
- At next level-up: Quick Reflexes now available as option
- If not chosen, XP persists and continues accumulating

**Example 2: Heavy Lifter**

- Character carries heavy loads: +3 XP per heavy carry
- After 100 heavy carries: 300 XP accumulated
- Reaches action-based threshold (~50 significant actions): Skill unlocks directly
- Character now has Heavy Lifter skill without level-up

**Example 3: Night Vision** (common skill)

- Character spends time in darkness: +2 XP per extended dark period
- After 200 XP accumulated: Available at level-up (Lesser likely)
- After 800 XP accumulated: Available at level-up (Greater likely)

### XP Persistence

- **XP persists**: Accumulated XP never decays
- **Accepting skill**: XP resets to 0 for that specific skill (you now have it)
- **Refusing at level-up**: XP persists, continues accumulating
- **Action-based unlock**: Skill unlocked automatically when threshold reached

---

## 5. Common Skills by Category

The sections below have been preserved for reference, but **individual skill files are now in the `/common/` directory**.

### Survival Skills

Individual files in [common/](common/):

- [second-wind.md](common/second-wind/index.md) - Emergency stamina recovery when exhausted
- [hearty-constitution.md](common/hearty-constitution/index.md) - Increased health pool
- [deep-reserves.md](common/deep-reserves/index.md) - Increased stamina pool
- [quick-recovery.md](common/quick-recovery/index.md) - Faster rest effectiveness
- [fortitude.md](common/fortitude/index.md) - Resistance to poison and disease
- [iron-stomach.md](common/iron-stomach/index.md) - Resistance to food-based ailments
- [climate-tolerance.md](common/climate-tolerance/index.md) - Reduced penalties from temperature extremes
- [light-sleeper.md](common/light-sleeper/index.md) - Improved awareness while resting
- [survivor.md](common/survivor/index.md) - General hardiness in adverse conditions

### Combat Skills

Individual files in [common/](common/):

- [power-strike.md](common/power-strike/index.md) - Increased melee damage on demand
- [steady-aim.md](common/steady-aim/index.md) - Improved ranged accuracy
- [defensive-stance.md](common/defensive-stance/index.md) - Reduced damage taken at cost of offense
- [quick-reflexes.md](common/quick-reflexes/index.md) - Improved dodge chance
- [thick-skin.md](common/thick-skin/index.md) - Natural damage resistance
- [adrenaline-surge.md](common/adrenaline-surge/index.md) - Combat stamina recovery when threatened
- [combat-awareness.md](common/combat-awareness/index.md) - Improved reaction in battle
- [brawler.md](common/brawler/index.md) - Improved unarmed combat
- [last-stand.md](common/last-stand/index.md) - Improved performance when near death

### Awareness & Detection Skills

Individual files in [common/](common/):

- [keen-senses.md](common/keen-senses/index.md) - Improved detection of hidden things
- [danger-sense.md](common/danger-sense/index.md) - Intuitive threat awareness
- [tracker.md](common/tracker/index.md) - Ability to follow trails
- [appraiser.md](common/appraiser/index.md) - Assess item value accurately
- [insight.md](common/insight/index.md) - Read people and detect deception
- [environmental-reading.md](common/environmental-reading/index.md) - Notice changes in surroundings
- [treasure-sense.md](common/treasure-sense/index.md) - Improved ability to find valuables

### Social Skills

Individual files in [common/](common/):

- [haggler.md](common/haggler/index.md) - Better prices when trading
- [persuasive.md](common/persuasive/index.md) - Improved ability to convince others
- [intimidating-presence.md](common/intimidating-presence/index.md) - Use fear to influence others
- [silver-tongue.md](common/silver-tongue/index.md) - Improved deception ability
- [likeable.md](common/likeable/index.md) - Naturally pleasant demeanor
- [reputation-builder.md](common/reputation-builder/index.md) - Gain standing faster with factions
- [diplomat.md](common/diplomat/index.md) - Navigate social conflicts

### Movement & Exploration Skills

Individual files in [common/](common/):

- [swift.md](common/swift/index.md) - Improved movement speed
- [sure-footed.md](common/sure-footed/index.md) - Reduced terrain penalties
- [climber.md](common/climber/index.md) - Improved climbing ability
- [swimmer.md](common/swimmer/index.md) - Improved water movement
- [pathfinder.md](common/pathfinder/index.md) - Navigate efficiently through wilderness
- [explorers-instinct.md](common/explorers-instinct/index.md) - Drawn to discoveries
- [night-eyes.md](common/night-eyes/index.md) - Improved vision in darkness
- [sprinter.md](common/sprinter/index.md) - Improved burst movement

### Crafting & Gathering Skills

Individual files in [common/](common/):

- [careful-hands.md](common/careful-hands/index.md) - Improved crafting quality
- [efficient-worker.md](common/efficient-worker/index.md) - Reduced stamina cost for labor
- [lucky-find.md](common/lucky-find/index.md) - Improved chance of rare results
- [gatherers-eye.md](common/gatherers-eye/index.md) - Improved resource detection
- [salvager.md](common/salvager/index.md) - Extract more from breakdowns
- [improviser.md](common/improviser/index.md) - Work with suboptimal conditions
- [quick-learner.md](common/quick-learner/index.md) - Faster skill and recipe acquisition
- [material-knowledge.md](common/material-knowledge/index.md) - Better understanding of components

### Utility Skills

Individual files in [common/](common/):

- [pack-mule.md](common/pack-mule/index.md) - Increased carrying capacity
- [quick-hands.md](common/quick-hands/index.md) - Faster item manipulation
- [organized.md](common/organized/index.md) - Better inventory management
- [maintenance.md](common/maintenance/index.md) - Keep equipment in good condition
- [focused-mind.md](common/focused-mind/index.md) - Resist mental disruption
- [resilient-spirit.md](common/resilient-spirit/index.md) - Recover from setbacks
- [jack-of-all-trades.md](common/jack-of-all-trades/index.md) - Competence in unfamiliar tasks
- [opportunist.md](common/opportunist/index.md) - Capitalize on favorable circumstances

### Leadership Skills (Common Pool)

Individual files in [common/](common/):

- [inspiring-presence.md](common/inspiring-presence/index.md) - Passive benefit to nearby allies
- [clear-orders.md](common/clear-orders/index.md) - Improved command effectiveness
- [shared-wisdom.md](common/shared-wisdom/index.md) - Party learning bonus
- [group-fortitude.md](common/group-fortitude/index.md) - Party defensive bonus

---

## 6. Detailed Skill Descriptions (Preserved for Reference)

**Note**: These detailed descriptions are preserved in this document, but the canonical versions are now in individual files in the `/common/` directory.

### Second Wind

Emergency stamina recovery when exhausted.

| Tier     | Type     | Base Effect                                   | Scaling          | Cooldown |
| -------- | -------- | --------------------------------------------- | ---------------- | -------- |
| Lesser   | Cooldown | Recover 15% max stamina                       | +1% per level    | 10 min   |
| Greater  | Cooldown | Recover 25% max stamina                       | +0.75% per level | 8 min    |
| Enhanced | Cooldown | Recover 35% max stamina, +10% regen for 2 min | +0.5% per level  | 6 min    |

**Trigger Actions:** Collapse from zero stamina, rest from exhaustion, use stamina potions
**Influence Rate:** 1 point per collapse, 0.5 per exhausted rest

---

### Hearty Constitution

Increased health pool.

| Tier     | Type    | Base Effect                       | Scaling          |
| -------- | ------- | --------------------------------- | ---------------- |
| Lesser   | Passive | +5% max health                    | +1% per level    |
| Greater  | Passive | +10% max health                   | +0.75% per level |
| Enhanced | Passive | +15% max health, +5% health regen | +0.5% per level  |

**Trigger Actions:** Take damage, survive near-death, complete combat encounters
**Influence Rate:** 0.1 per combat, 1.0 per near-death survival

---

### Deep Reserves

Increased stamina pool.

| Tier     | Type    | Base Effect                         | Scaling          |
| -------- | ------- | ----------------------------------- | ---------------- |
| Lesser   | Passive | +5% max stamina                     | +1% per level    |
| Greater  | Passive | +10% max stamina                    | +0.75% per level |
| Enhanced | Passive | +15% max stamina, +5% stamina regen | +0.5% per level  |

**Trigger Actions:** Perform extended labor, sprint long distances, craft for extended periods
**Influence Rate:** 0.1 per hour of heavy stamina use

---

### Quick Recovery

Faster rest effectiveness.

| Tier     | Type    | Base Effect                              | Scaling          |
| -------- | ------- | ---------------------------------------- | ---------------- |
| Lesser   | Passive | Rest recovers stamina 10% faster         | +1% per level    |
| Greater  | Passive | Rest 20% faster, naps 15% more effective | +0.75% per level |
| Enhanced | Passive | Rest 30% faster, naps 25% more effective | +0.5% per level  |

**Trigger Actions:** Complete rest cycles, take naps, wake naturally (not interrupted)
**Influence Rate:** 0.25 per full rest, 0.1 per nap

---

### Fortitude

Resistance to poison and disease.

| Tier     | Type    | Base Effect                       | Scaling          |
| -------- | ------- | --------------------------------- | ---------------- |
| Lesser   | Passive | +10% poison/disease resistance    | +1% per level    |
| Greater  | Passive | +20% poison/disease resistance    | +0.75% per level |
| Enhanced | Passive | +30% resistance, reduced duration | +0.5% per level  |

**Trigger Actions:** Survive poison, recover from disease, consume antidotes
**Influence Rate:** 1.0 per poison survived, 2.0 per disease recovered

---

### Iron Stomach

Resistance to food-based ailments.

| Tier     | Type    | Base Effect                              | Scaling          |
| -------- | ------- | ---------------------------------------- | ---------------- |
| Lesser   | Passive | +15% food poison resistance              | +1% per level    |
| Greater  | Passive | Eat spoiled food safely, +30% resistance | +0.75% per level |
| Enhanced | Passive | Immune to food poisoning, eat anything   | +0.5% per level  |

**Trigger Actions:** Eat poor quality food, eat questionable items, survive food poisoning
**Influence Rate:** 0.5 per poor meal, 2.0 per food poisoning survived

---

### Climate Tolerance

Reduced penalties from temperature extremes.

| Tier     | Type    | Base Effect                          | Scaling          |
| -------- | ------- | ------------------------------------ | ---------------- |
| Lesser   | Passive | -15% stamina drain from heat OR cold | +1% per level    |
| Greater  | Passive | -25% chosen, -10% opposite           | +0.75% per level |
| Enhanced | Passive | -35% all temperature extremes        | +0.5% per level  |

**Trigger Actions:** Spend time in extreme climates, suffer exposure conditions
**Influence Rate:** 0.25 per hour in extreme climate, 1.0 per exposure condition survived

---

### Light Sleeper

Improved awareness while resting.

| Tier     | Type    | Base Effect                                      | Scaling          |
| -------- | ------- | ------------------------------------------------ | ---------------- |
| Lesser   | Passive | +15% wake chance from threats                    | +1% per level    |
| Greater  | Passive | +30% wake chance, +5% stamina on wake            | +0.75% per level |
| Enhanced | Passive | +45% wake chance, +10% stamina, adrenaline boost | +0.5% per level  |

**Trigger Actions:** Wake from rest due to threats, stand watch duty, interrupted sleep
**Influence Rate:** 2.0 per threat wake, 0.5 per watch shift

---

### Survivor

General hardiness in adverse conditions.

| Tier     | Type    | Base Effect                          | Scaling          |
| -------- | ------- | ------------------------------------ | ---------------- |
| Lesser   | Passive | -10% stamina drain from hazards      | +1% per level    |
| Greater  | Passive | -20% drain, +10% exposure resistance | +0.75% per level |
| Enhanced | Passive | -30% drain, +20% resistance          | +0.5% per level  |

**Trigger Actions:** Survive environmental hazards, travel through dangerous terrain, endure harsh conditions
**Influence Rate:** 0.5 per hazard exposure, 1.0 per condition survived

---

## 6. Combat Skills

### Power Strike

Increased melee damage on demand.

| Tier     | Type     | Base Effect                | Scaling          | Cost        |
| -------- | -------- | -------------------------- | ---------------- | ----------- |
| Lesser   | Cooldown | +20% damage next attack    | +1% per level    | Low, 30s CD |
| Greater  | Cooldown | +35% damage next attack    | +0.75% per level | Mod, 25s CD |
| Enhanced | Cooldown | +50% damage next 2 attacks | +0.5% per level  | Mod, 20s CD |

**Trigger Actions:** Land melee attacks, kill enemies with melee, use heavy weapons
**Influence Rate:** 0.05 per melee kill, 0.1 per heavy weapon kill

---

### Steady Aim

Improved ranged accuracy.

| Tier     | Type   | Base Effect               | Scaling          | Cost           |
| -------- | ------ | ------------------------- | ---------------- | -------------- |
| Lesser   | Toggle | +10% accuracy, -10% speed | +1% per level    | Low drain      |
| Greater  | Toggle | +18% accuracy, -5% speed  | +0.75% per level | Low drain      |
| Enhanced | Toggle | +25% accuracy, no penalty | +0.5% per level  | Very low drain |

**Trigger Actions:** Land ranged attacks, kill enemies with ranged, use bows/crossbows
**Influence Rate:** 0.05 per ranged kill, 0.2 per long-range kill

---

### Defensive Stance

Reduced damage taken at cost of offense.

| Tier     | Type   | Base Effect            | Scaling          | Cost           |
| -------- | ------ | ---------------------- | ---------------- | -------------- |
| Lesser   | Toggle | -15% taken, -20% dealt | +1% per level    | Low drain      |
| Greater  | Toggle | -25% taken, -15% dealt | +0.75% per level | Low drain      |
| Enhanced | Toggle | -35% taken, -10% dealt | +0.5% per level  | Very low drain |

**Trigger Actions:** Block attacks, survive while outnumbered, use shields
**Influence Rate:** 0.1 per combat survived while wounded, 0.5 per outnumbered survival

---

### Quick Reflexes

Improved dodge chance.

| Tier     | Type    | Base Effect                      | Scaling          |
| -------- | ------- | -------------------------------- | ---------------- |
| Lesser   | Passive | +5% dodge chance                 | +1% per level    |
| Greater  | Passive | +10% dodge chance                | +0.75% per level |
| Enhanced | Passive | +15% dodge, +5% vs aimed attacks | +0.5% per level  |

**Trigger Actions:** Dodge attacks (successful), fight without armor, evade traps
**Influence Rate:** 0.1 per successful dodge, 0.5 per trap evaded

---

### Thick Skin

Natural damage resistance.

| Tier     | Type    | Base Effect                         | Scaling         |
| -------- | ------- | ----------------------------------- | --------------- |
| Lesser   | Passive | -2 flat damage from physical        | +0.2 per level  |
| Greater  | Passive | -4 flat damage from physical        | +0.15 per level |
| Enhanced | Passive | -6 flat damage, -5% critical damage | +0.1 per level  |

**Trigger Actions:** Take physical damage, fight without heavy armor, endure hits
**Influence Rate:** 0.02 per hit taken, 0.2 per combat as damage sponge

---

### Adrenaline Surge

Combat stamina recovery when threatened.

| Tier     | Type    | Base Effect                               | Scaling         |
| -------- | ------- | ----------------------------------------- | --------------- |
| Lesser   | Passive | 5% stamina when below 25% HP              | +0.5% per level |
| Greater  | Passive | 10% stamina when below 30% HP             | +0.4% per level |
| Enhanced | Passive | 15% stamina below 35% HP, +10% damage 10s | +0.3% per level |

**Trigger Actions:** Drop below 25% health in combat, survive near-death, fight while wounded
**Influence Rate:** 2.0 per near-death combat survival, 0.5 per wounded combat

---

### Combat Awareness

Improved reaction in battle.

| Tier     | Type    | Base Effect                                      | Scaling          |
| -------- | ------- | ------------------------------------------------ | ---------------- |
| Lesser   | Passive | +5% initiative, -5% flanked chance               | +1% per level    |
| Greater  | Passive | +10% initiative, -10% flanked                    | +0.75% per level |
| Enhanced | Passive | +15% initiative, -15% flanked, +5% detect hidden | +0.5% per level  |

**Trigger Actions:** Win initiative, avoid being flanked, detect ambushes
**Influence Rate:** 0.2 per initiative win, 1.0 per ambush detected

---

### Brawler

Improved unarmed combat.

| Tier     | Type    | Base Effect                             | Scaling          |
| -------- | ------- | --------------------------------------- | ---------------- |
| Lesser   | Passive | +15% unarmed damage                     | +1% per level    |
| Greater  | Passive | +30% damage, +10% speed                 | +0.75% per level |
| Enhanced | Passive | +50% damage, +15% speed, damage armored | +0.5% per level  |

**Trigger Actions:** Fight unarmed, kill enemies unarmed, grapple
**Influence Rate:** 0.5 per unarmed combat, 2.0 per unarmed kill

---

### Last Stand

Improved performance when near death.

| Tier     | Type    | Base Effect                                      | Scaling          |
| -------- | ------- | ------------------------------------------------ | ---------------- |
| Lesser   | Passive | +10% damage below 20% HP                         | +1% per level    |
| Greater  | Passive | +20% damage, +10% dodge below 25% HP             | +0.75% per level |
| Enhanced | Passive | +30% damage, +15% dodge, -10% taken below 30% HP | +0.5% per level  |

**Trigger Actions:** Fight while below 25% HP, kill enemies while wounded, survive near-death
**Influence Rate:** 0.5 per kill while wounded, 3.0 per near-death victory

---

## 7. Awareness & Detection Skills

### Keen Senses

Improved detection of hidden things.

| Tier     | Type    | Base Effect                            | Scaling          |
| -------- | ------- | -------------------------------------- | ---------------- |
| Lesser   | Passive | +10% detection                         | +1% per level    |
| Greater  | Passive | +20% detection, light concealment      | +0.75% per level |
| Enhanced | Passive | +30% detection, sense invisible nearby | +0.5% per level  |

**Trigger Actions:** Detect hidden enemies, find traps, discover hidden objects
**Influence Rate:** 1.0 per hidden enemy detected, 0.5 per trap found

---

### Danger Sense

Intuitive threat awareness.

| Tier     | Type    | Base Effect                                | Scaling          |
| -------- | ------- | ------------------------------------------ | ---------------- |
| Lesser   | Passive | +15% avoid ambush                          | +1% per level    |
| Greater  | Passive | +25% avoid ambush, trap warning            | +0.75% per level |
| Enhanced | Passive | +40% avoid ambush, auto-detect close traps | +0.5% per level  |

**Trigger Actions:** Avoid ambushes, detect traps before triggering, survive surprise attacks
**Influence Rate:** 2.0 per ambush avoided, 1.0 per trap pre-detected

---

### Tracker

Ability to follow trails.

| Tier     | Type    | Base Effect                                   | Scaling            |
| -------- | ------- | --------------------------------------------- | ------------------ |
| Lesser   | Passive | Follow fresh trails (<6 hrs)                  | +0.5 hrs per level |
| Greater  | Passive | Follow older trails (<24 hrs), ID type        | +0.5 hrs per level |
| Enhanced | Passive | Follow faint trails (<72 hrs), ID individuals | +0.5 hrs per level |

**Trigger Actions:** Successfully track creatures, follow trails, hunt animals
**Influence Rate:** 0.5 per successful track, 1.0 per difficult track

---

### Appraiser

Assess item value accurately.

| Tier     | Type    | Base Effect                     | Scaling                |
| -------- | ------- | ------------------------------- | ---------------------- |
| Lesser   | Passive | Value within 25% (common items) | -1% margin per level   |
| Greater  | Passive | Value within 10%, detect fakes  | -0.5% margin per level |
| Enhanced | Passive | Exact value, ID enchantments    | Expanded item types    |

**Trigger Actions:** Trade items, identify values correctly, detect forgeries
**Influence Rate:** 0.2 per trade, 2.0 per forgery detected

---

### Insight

Read people and detect deception.

| Tier     | Type    | Base Effect                        | Scaling          |
| -------- | ------- | ---------------------------------- | ---------------- |
| Lesser   | Passive | +10% detect lies                   | +1% per level    |
| Greater  | Passive | +20% detect lies, sense intentions | +0.75% per level |
| Enhanced | Passive | +35% detect lies, sense motives    | +0.5% per level  |

**Trigger Actions:** Detect lies, read NPC intentions correctly, avoid deception
**Influence Rate:** 1.5 per lie detected, 0.5 per successful read

---

### Environmental Reading

Notice changes in surroundings.

| Tier     | Type    | Base Effect                              | Scaling            |
| -------- | ------- | ---------------------------------------- | ------------------ |
| Lesser   | Passive | Predict weather 2-4 hrs ahead            | +0.5 hrs per level |
| Greater  | Passive | Predict weather 6-12 hrs, sense threats  | +0.5 hrs per level |
| Enhanced | Passive | Predict weather 24 hrs, detect unnatural | +0.5 hrs per level |

**Trigger Actions:** Travel through varied terrain, experience weather changes, avoid weather hazards
**Influence Rate:** 0.2 per weather prediction used, 1.0 per hazard avoided via prediction

---

### Treasure Sense

Improved ability to find valuables.

| Tier     | Type    | Base Effect                                | Scaling          |
| -------- | ------- | ------------------------------------------ | ---------------- |
| Lesser   | Passive | +10% find hidden loot                      | +1% per level    |
| Greater  | Passive | +20% find loot, sense valuables            | +0.75% per level |
| Enhanced | Passive | +30% find loot, detect secret compartments | +0.5% per level  |

**Trigger Actions:** Find hidden loot, discover secret containers, loot thoroughly
**Influence Rate:** 1.0 per hidden loot found, 0.2 per thorough search

---

## 8. Social Skills

### Haggler

Better prices when trading.

| Tier     | Type    | Base Effect                      | Scaling         |
| -------- | ------- | -------------------------------- | --------------- |
| Lesser   | Passive | -5% buy, +5% sell                | +0.5% per level |
| Greater  | Passive | -10% buy, +10% sell              | +0.4% per level |
| Enhanced | Passive | -15% buy, +15% sell, bonus deals | +0.3% per level |

**Trigger Actions:** Complete trades, negotiate prices, trade in volume
**Influence Rate:** 0.1 per trade, 0.5 per large trade, 1.0 per exceptional deal

---

### Persuasive

Improved ability to convince others.

| Tier     | Type    | Base Effect                              | Scaling          |
| -------- | ------- | ---------------------------------------- | ---------------- |
| Lesser   | Passive | +10% persuasion success                  | +1% per level    |
| Greater  | Passive | +20% persuasion, harder attempts         | +0.75% per level |
| Enhanced | Passive | +30% persuasion, reduced failure penalty | +0.5% per level  |

**Trigger Actions:** Successfully persuade NPCs, change NPC decisions, mediate disputes
**Influence Rate:** 1.0 per successful persuasion, 0.5 per difficult persuasion

---

### Intimidating Presence

Use fear to influence others.

| Tier     | Type    | Base Effect                            | Scaling          |
| -------- | ------- | -------------------------------------- | ---------------- |
| Lesser   | Passive | +10% intimidation, minor morale effect | +1% per level    |
| Greater  | Passive | +20% intimidation, moderate morale     | +0.75% per level |
| Enhanced | Passive | +30% intimidation, +10% flee/surrender | +0.5% per level  |

**Trigger Actions:** Successfully intimidate, cause enemies to flee, demand surrender
**Influence Rate:** 1.0 per successful intimidation, 2.0 per enemy surrender

---

### Silver Tongue

Improved deception ability.

| Tier     | Type    | Base Effect                               | Scaling          |
| -------- | ------- | ----------------------------------------- | ---------------- |
| Lesser   | Passive | +10% lying/bluffing success               | +1% per level    |
| Greater  | Passive | +20% success, harder to detect magically  | +0.75% per level |
| Enhanced | Passive | +30% success, +15% resist truth detection | +0.5% per level  |

**Trigger Actions:** Successfully deceive NPCs, bluff in negotiations, avoid detection of lies
**Influence Rate:** 1.0 per successful deception, 0.5 per bluff

---

### Likeable

Naturally pleasant demeanor.

| Tier     | Type    | Base Effect                                      | Scaling        |
| -------- | ------- | ------------------------------------------------ | -------------- |
| Lesser   | Passive | +5 starting disposition                          | +0.5 per level |
| Greater  | Passive | +10 disposition, faster relationships            | +0.4 per level |
| Enhanced | Passive | +15 disposition, reduced first offense hostility | +0.3 per level |

**Trigger Actions:** Positive NPC interactions, make friends, improve relationships
**Influence Rate:** 0.2 per positive interaction, 1.0 per new friendship

---

### Reputation Builder

Gain standing faster with factions.

| Tier     | Type    | Base Effect             | Scaling          |
| -------- | ------- | ----------------------- | ---------------- |
| Lesser   | Passive | +10% faction gains      | +1% per level    |
| Greater  | Passive | +20% gains, -10% losses | +0.75% per level |
| Enhanced | Passive | +30% gains, -20% losses | +0.5% per level  |

**Trigger Actions:** Complete faction quests, improve faction standing, interact with faction members
**Influence Rate:** 0.5 per faction quest, 0.2 per positive faction interaction

---

### Diplomat

Navigate social conflicts.

| Tier     | Type    | Base Effect                                     | Scaling          |
| -------- | ------- | ----------------------------------------------- | ---------------- |
| Lesser   | Passive | +10% mediation, calm minor hostilities          | +1% per level    |
| Greater  | Passive | +20% mediation, calm moderate hostilities       | +0.75% per level |
| Enhanced | Passive | +30% mediation, negotiate with hostile factions | +0.5% per level  |

**Trigger Actions:** Mediate disputes, calm hostile situations, negotiate peace
**Influence Rate:** 2.0 per successful mediation, 1.0 per calmed hostility

---

## 9. Movement & Exploration Skills

### Swift

Improved movement speed.

| Tier     | Type    | Base Effect               | Scaling         |
| -------- | ------- | ------------------------- | --------------- |
| Lesser   | Passive | +5% movement speed        | +0.5% per level |
| Greater  | Passive | +10% movement speed       | +0.4% per level |
| Enhanced | Passive | +15% movement, +5% sprint | +0.3% per level |

**Trigger Actions:** Travel long distances, run/sprint frequently, chase or flee
**Influence Rate:** 0.1 per hour of travel, 0.5 per chase/flee situation

---

### Sure-Footed

Reduced terrain penalties.

| Tier     | Type    | Base Effect                                   | Scaling          |
| -------- | ------- | --------------------------------------------- | ---------------- |
| Lesser   | Passive | -15% terrain penalty                          | +1% per level    |
| Greater  | Passive | -30% terrain penalty, +10% balance            | +0.75% per level |
| Enhanced | Passive | -50% terrain penalty, immune minor knockdowns | +0.5% per level  |

**Trigger Actions:** Travel through difficult terrain, maintain footing, avoid falls
**Influence Rate:** 0.2 per hour in difficult terrain, 1.0 per fall avoided

---

### Climber

Improved climbing ability.

| Tier     | Type    | Base Effect                                        | Scaling          |
| -------- | ------- | -------------------------------------------------- | ---------------- |
| Lesser   | Passive | Climb basic surfaces, +15% speed                   | +1% per level    |
| Greater  | Passive | Climb difficult surfaces, +30% speed               | +0.75% per level |
| Enhanced | Passive | Climb any surface, +50% speed, reduced fall damage | +0.5% per level  |

**Trigger Actions:** Climb surfaces, scale heights, descend safely
**Influence Rate:** 0.5 per climb, 1.0 per difficult climb

---

### Swimmer

Improved water movement.

| Tier     | Type    | Base Effect                                        | Scaling          |
| -------- | ------- | -------------------------------------------------- | ---------------- |
| Lesser   | Passive | +20% swim speed, +25% breath                       | +1% per level    |
| Greater  | Passive | +40% swim speed, +50% breath                       | +0.75% per level |
| Enhanced | Passive | +60% swim speed, +100% breath, swim in light armor | +0.5% per level  |

**Trigger Actions:** Swim, hold breath underwater, cross water obstacles
**Influence Rate:** 0.3 per water crossing, 1.0 per underwater action

---

### Pathfinder

Navigate efficiently through wilderness.

| Tier     | Type    | Base Effect                           | Scaling         |
| -------- | ------- | ------------------------------------- | --------------- |
| Lesser   | Passive | -10% travel time, harder to get lost  | +0.5% per level |
| Greater  | Passive | -20% travel time, find shorter routes | +0.4% per level |
| Enhanced | Passive | -30% travel time, party benefits      | +0.3% per level |

**Trigger Actions:** Travel through wilderness, find shortcuts, navigate without getting lost
**Influence Rate:** 0.2 per wilderness journey, 1.0 per shortcut found

---

### Explorer's Instinct

Drawn to discoveries.

| Tier     | Type    | Base Effect                                 | Scaling          |
| -------- | ------- | ------------------------------------------- | ---------------- |
| Lesser   | Passive | +10% discover locations, +5% exploration XP | +1% per level    |
| Greater  | Passive | +20% discover, +10% XP, sense POIs          | +0.75% per level |
| Enhanced | Passive | +30% discover, +15% XP, identify POIs       | +0.5% per level  |

**Trigger Actions:** Discover new locations, explore unknown areas, find points of interest
**Influence Rate:** 1.0 per new location, 0.5 per POI found

---

### Night Eyes

Improved vision in darkness.

| Tier     | Type    | Base Effect             | Scaling          |
| -------- | ------- | ----------------------- | ---------------- |
| Lesser   | Passive | -25% darkness penalties | +1% per level    |
| Greater  | Passive | -50% darkness penalties | +0.75% per level |
| Enhanced | Passive | -75% darkness penalties | +0.5% per level  |

**Trigger Actions:** Operate in darkness, travel at night, fight in low light
**Influence Rate:** 0.2 per hour in darkness, 0.5 per night combat

---

### Sprinter

Improved burst movement.

| Tier     | Type    | Base Effect                               | Scaling         |
| -------- | ------- | ----------------------------------------- | --------------- |
| Lesser   | Passive | +10% sprint speed, -10% sprint cost       | +0.5% per level |
| Greater  | Passive | +20% sprint speed, -20% sprint cost       | +0.4% per level |
| Enhanced | Passive | +30% sprint speed, -30% cost, quick accel | +0.3% per level |

**Trigger Actions:** Sprint, chase enemies, flee from threats
**Influence Rate:** 0.1 per sprint use, 1.0 per successful chase/flee

---

## 10. Crafting & Gathering Skills

### Careful Hands

Improved crafting quality.

| Tier     | Type    | Base Effect                            | Scaling         |
| -------- | ------- | -------------------------------------- | --------------- |
| Lesser   | Passive | +5% crafting quality                   | +0.5% per level |
| Greater  | Passive | +10% crafting quality                  | +0.4% per level |
| Enhanced | Passive | +15% quality, reduced critical failure | +0.3% per level |

**Trigger Actions:** Craft items, achieve quality results, complete fine work
**Influence Rate:** 0.2 per craft, 1.0 per quality+ result

---

### Efficient Worker

Reduced stamina cost for labor.

| Tier     | Type    | Base Effect                        | Scaling         |
| -------- | ------- | ---------------------------------- | --------------- |
| Lesser   | Passive | -10% stamina cost (craft/gather)   | +0.5% per level |
| Greater  | Passive | -20% stamina cost                  | +0.4% per level |
| Enhanced | Passive | -30% stamina cost, +10% work speed | +0.3% per level |

**Trigger Actions:** Perform extended labor, craft multiple items, gather for long periods
**Influence Rate:** 0.1 per hour of labor, 0.3 per extended session

---

### Lucky Find

Improved chance of rare results.

| Tier     | Type    | Base Effect                              | Scaling         |
| -------- | ------- | ---------------------------------------- | --------------- |
| Lesser   | Passive | +5% rare materials                       | +0.5% per level |
| Greater  | Passive | +10% rare, +5% bonus yield               | +0.4% per level |
| Enhanced | Passive | +15% rare, +10% bonus, exceptional finds | +0.3% per level |

**Trigger Actions:** Find rare materials, get bonus yields, discover exceptional resources
**Influence Rate:** 2.0 per rare find, 0.5 per bonus yield

---

### Gatherer's Eye

Improved resource detection.

| Tier     | Type    | Base Effect                                  | Scaling          |
| -------- | ------- | -------------------------------------------- | ---------------- |
| Lesser   | Passive | +15% node detection                          | +1% per level    |
| Greater  | Passive | +30% detection, ID quality before gather     | +0.75% per level |
| Enhanced | Passive | +50% detection, sense rare nodes at distance | +0.5% per level  |

**Trigger Actions:** Find resource nodes, identify resources, locate gathering spots
**Influence Rate:** 0.3 per node found, 1.0 per rare node found

---

### Salvager

Extract more from breakdowns.

| Tier     | Type    | Base Effect                             | Scaling         |
| -------- | ------- | --------------------------------------- | --------------- |
| Lesser   | Passive | +10% materials from breakdown           | +0.5% per level |
| Greater  | Passive | +20% materials, salvage damaged items   | +0.4% per level |
| Enhanced | Passive | +30% materials, salvage heavily damaged | +0.3% per level |

**Trigger Actions:** Break down items, salvage equipment, recover materials
**Influence Rate:** 0.2 per salvage, 1.0 per valuable salvage

---

### Improviser

Work with suboptimal conditions.

| Tier     | Type    | Base Effect                                         | Scaling          |
| -------- | ------- | --------------------------------------------------- | ---------------- |
| Lesser   | Passive | -10% improvised penalty                             | +1% per level    |
| Greater  | Passive | -25% penalty, substitute some materials             | +0.75% per level |
| Enhanced | Passive | -40% penalty, substitute freely, work without tools | +0.5% per level  |

**Trigger Actions:** Craft without proper tools, substitute materials, work in poor conditions
**Influence Rate:** 1.0 per improvised craft, 0.5 per material substitution

---

### Quick Learner

Faster skill and recipe acquisition.

| Tier     | Type    | Base Effect                         | Scaling         |
| -------- | ------- | ----------------------------------- | --------------- |
| Lesser   | Passive | +10% skill XP gain                  | +0.5% per level |
| Greater  | Passive | +20% skill XP, +10% recipe learning | +0.4% per level |
| Enhanced | Passive | +30% skill XP, +20% recipe learning | +0.3% per level |

**Trigger Actions:** Learn new skills, acquire recipes, train with others
**Influence Rate:** 1.0 per new skill/recipe, 0.3 per training session

---

### Material Knowledge

Better understanding of components.

| Tier     | Type    | Base Effect                         | Scaling         |
| -------- | ------- | ----------------------------------- | --------------- |
| Lesser   | Passive | ID material quality, +5% efficiency | +0.5% per level |
| Greater  | Passive | ID properties, +10% efficiency      | +0.4% per level |
| Enhanced | Passive | ID optimal uses, +15% efficiency    | +0.3% per level |

**Trigger Actions:** Work with varied materials, identify material properties, optimize material use
**Influence Rate:** 0.2 per material type used, 0.5 per new material mastered

---

## 11. Utility Skills

### Pack Mule

Increased carrying capacity.

| Tier     | Type    | Base Effect                             | Scaling          |
| -------- | ------- | --------------------------------------- | ---------------- |
| Lesser   | Passive | +15% carry capacity                     | +1% per level    |
| Greater  | Passive | +30% carry capacity                     | +0.75% per level |
| Enhanced | Passive | +50% carry, reduced encumbrance penalty | +0.5% per level  |

**Trigger Actions:** Carry heavy loads, travel while encumbered, transport goods
**Influence Rate:** 0.2 per hour encumbered, 0.5 per heavy transport

---

### Quick Hands

Faster item manipulation.

| Tier     | Type    | Base Effect                                     | Scaling          |
| -------- | ------- | ----------------------------------------------- | ---------------- |
| Lesser   | Passive | +15% item use speed                             | +1% per level    |
| Greater  | Passive | +30% item speed, faster equip swap              | +0.75% per level |
| Enhanced | Passive | +50% item speed, use while moving, instant swap | +0.5% per level  |

**Trigger Actions:** Use items in combat, swap equipment, quick item access
**Influence Rate:** 0.3 per combat item use, 0.5 per equipment swap in combat

---

### Organized

Better inventory management.

| Tier     | Type    | Base Effect                            | Scaling         |
| -------- | ------- | -------------------------------------- | --------------- |
| Lesser   | Passive | +10% effective inventory (stacking)    | +0.5% per level |
| Greater  | Passive | +20% effective inventory, auto-sort    | +0.4% per level |
| Enhanced | Passive | +30% effective inventory, quick-access | +0.3% per level |

**Trigger Actions:** Manage inventory, organize storage, access items efficiently
**Influence Rate:** 0.1 per inventory organization, 0.3 per efficient access

---

### Maintenance

Keep equipment in good condition.

| Tier     | Type    | Base Effect                              | Scaling         |
| -------- | ------- | ---------------------------------------- | --------------- |
| Lesser   | Passive | -10% equipment degradation               | +0.5% per level |
| Greater  | Passive | -20% degradation, basic field repairs    | +0.4% per level |
| Enhanced | Passive | -30% degradation, moderate field repairs | +0.3% per level |

**Trigger Actions:** Repair equipment, maintain gear, use equipment extensively
**Influence Rate:** 0.5 per repair, 0.1 per day of equipment use

---

### Focused Mind

Resist mental disruption.

| Tier     | Type    | Base Effect                                 | Scaling          |
| -------- | ------- | ------------------------------------------- | ---------------- |
| Lesser   | Passive | +10% resist fear/confusion/charm            | +1% per level    |
| Greater  | Passive | +20% resist mental, faster recovery         | +0.75% per level |
| Enhanced | Passive | +30% resist mental, break free from effects | +0.5% per level  |

**Trigger Actions:** Resist mental effects, overcome fear, break charm/confusion
**Influence Rate:** 2.0 per mental effect resisted, 1.0 per effect overcome

---

### Resilient Spirit

Recover from setbacks.

| Tier     | Type    | Base Effect                                    | Scaling         |
| -------- | ------- | ---------------------------------------------- | --------------- |
| Lesser   | Passive | -10% negative status duration                  | +0.5% per level |
| Greater  | Passive | -20% duration, +10% condition recovery         | +0.4% per level |
| Enhanced | Passive | -30% duration, +20% recovery, resist recurring | +0.3% per level |

**Trigger Actions:** Recover from status effects, endure negative conditions, bounce back
**Influence Rate:** 0.5 per condition recovered, 1.0 per severe condition survived

---

### Jack of All Trades

Competence in unfamiliar tasks.

| Tier     | Type    | Base Effect                                        | Scaling          |
| -------- | ------- | -------------------------------------------------- | ---------------- |
| Lesser   | Passive | -15% untrained penalty                             | +1% per level    |
| Greater  | Passive | -30% untrained penalty                             | +0.75% per level |
| Enhanced | Passive | -50% penalty, untrained success counts for unlocks | +0.5% per level  |

**Trigger Actions:** Attempt untrained actions, succeed at unfamiliar tasks, try new activities
**Influence Rate:** 0.5 per untrained attempt, 2.0 per untrained success

---

### Opportunist

Capitalize on favorable circumstances.

| Tier     | Type    | Base Effect                             | Scaling          |
| -------- | ------- | --------------------------------------- | ---------------- |
| Lesser   | Passive | +10% bonus from favorable circumstances | +1% per level    |
| Greater  | Passive | +20% bonus from favorable circumstances | +0.75% per level |
| Enhanced | Passive | +30% bonus, create more opportunities   | +0.5% per level  |

**Trigger Actions:** Exploit advantages, flank enemies, capitalize on openings
**Influence Rate:** 0.3 per opportunity exploited, 1.0 per created opportunity

---

## 12. Leadership Skills (Common Pool)

### Inspiring Presence

Passive benefit to nearby allies.

| Tier     | Type    | Base Effect                      | Scaling         | Range  |
| -------- | ------- | -------------------------------- | --------------- | ------ |
| Lesser   | Passive | Allies +5% stamina regen         | +0.5% per level | Close  |
| Greater  | Passive | Allies +10% stamina, +5% morale  | +0.4% per level | Medium |
| Enhanced | Passive | Allies +15% stamina, +10% morale | +0.3% per level | Far    |

**Trigger Actions:** Lead party, support allies, boost party morale
**Influence Rate:** 0.2 per party action led, 1.0 per morale-boosting event

---

### Clear Orders

Improved command effectiveness.

| Tier     | Type    | Base Effect                                    | Scaling          |
| -------- | ------- | ---------------------------------------------- | ---------------- |
| Lesser   | Passive | +10% order compliance                          | +1% per level    |
| Greater  | Passive | +20% compliance, precise orders                | +0.75% per level |
| Enhanced | Passive | +30% compliance, complex orders, reduced delay | +0.5% per level  |

**Trigger Actions:** Issue orders, have orders followed, command in combat
**Influence Rate:** 0.3 per order followed, 1.0 per complex order executed

---

### Shared Wisdom

Party learning bonus.

| Tier     | Type    | Base Effect   | Scaling          | Range  |
| -------- | ------- | ------------- | ---------------- | ------ |
| Lesser   | Passive | Party +3% XP  | +0.3% per level  | Close  |
| Greater  | Passive | Party +6% XP  | +0.25% per level | Medium |
| Enhanced | Passive | Party +10% XP | +0.2% per level  | Far    |

**Trigger Actions:** Train party members, share knowledge, teach skills
**Influence Rate:** 0.5 per training session given, 1.0 per skill taught

---

### Group Fortitude

Party defensive bonus.

| Tier     | Type    | Base Effect                            | Scaling         | Range  |
| -------- | ------- | -------------------------------------- | --------------- | ------ |
| Lesser   | Passive | Party +5% condition resistance         | +0.5% per level | Close  |
| Greater  | Passive | Party +10% resistance, shared recovery | +0.4% per level | Medium |
| Enhanced | Passive | Party +15% resistance, faster recovery | +0.3% per level | Far    |

**Trigger Actions:** Protect party members, help party recover, endure hardship together
**Influence Rate:** 0.3 per party protection action, 1.0 per party hardship survived

---

## 13. Skill Acquisition Summary

### How Common Skills Are Obtained

| Method           | Description                                  |
| ---------------- | -------------------------------------------- |
| Class level-up   | 0-3 skills offered (may include common pool) |
| Milestone levels | 1-5 skills offered (higher chance of common) |
| Training         | Pay trainer to learn specific skill          |
| Discovery        | Rare event-based acquisition                 |

### Base Offer Weights

| Situation                          | Common Pool Chance    |
| ---------------------------------- | --------------------- |
| Normal level-up                    | 30-40%                |
| First 5 class levels               | 50-60% (foundational) |
| Milestone (10, 20, 30)             | 40-50%                |
| No relevant class skills available | 80%+                  |

### Influence Modification to Offers

| Influence Level | Additional Offer Chance       |
| --------------- | ----------------------------- |
| Threshold 1     | +5% chance this skill offered |
| Threshold 2     | +10% chance                   |
| Threshold 3     | +20% chance                   |
| Threshold 4     | +30% chance                   |

### Refusing Skills

- Refused skills return to pool
- Can be offered again at future level-ups
- Luck counter increases (+2% per refusal) for better tier
- Influence retained when refusing

---

## 14. Tier Mechanics

### Tier Progression

| Acquisition                             | Result                     |
| --------------------------------------- | -------------------------- |
| Roll Lesser when none owned             | Gain Lesser                |
| Roll Greater when Lesser owned          | Greater replaces Lesser    |
| Roll Enhanced when Lesser/Greater owned | Enhanced replaces existing |
| Roll Lesser when Greater owned          | No effect                  |

### Base Tier Odds by Level

| Level Range | Lesser | Greater | Enhanced |
| ----------- | ------ | ------- | -------- |
| 1-5         | 80%    | 18%     | 2%       |
| 6-10        | 65%    | 30%     | 5%       |
| 11-15       | 50%    | 40%     | 10%      |
| 16-20       | 35%    | 45%     | 20%      |
| 21+         | 20%    | 45%     | 35%      |

### Influence Modification to Tier

| Influence Level | Tier Quality Bonus |
| --------------- | ------------------ |
| Threshold 1     | +5% better tier    |
| Threshold 2     | +10% better tier   |
| Threshold 3     | +20% better tier   |
| Threshold 4     | +35% better tier   |

### Luck Counter Effect

| Refused Skills | Tier Bonus |
| -------------- | ---------- |
| 1              | +2%        |
| 3              | +6%        |
| 5              | +10%       |
| 10             | +20%       |

---

## 15. Scaling Summary Table

### Common Skills: Scale with Total Combined Class Levels

| Effect Type        | Lesser                       | Greater                        | Enhanced                      |
| ------------------ | ---------------------------- | ------------------------------ | ----------------------------- |
| Percentage bonuses | +1% of base per total level  | +0.75% of base per total level | +0.5% of base per total level |
| Flat bonuses       | +10% of base per total level | +7.5% of base per total level  | +5% of base per total level   |
| Duration (hours)   | +0.5 hr per 10 total levels  | +0.5 hr per 10 total levels    | +0.5 hr per 10 total levels   |
| Disposition points | +0.5 per 10 total levels     | +0.4 per 10 total levels       | +0.3 per 10 total levels      |

### Specialist vs Generalist Comparison

| Build              | Classes               | Total Levels | Enhanced Hearty Constitution |
| ------------------ | --------------------- | ------------ | ---------------------------- |
| Deep Specialist    | Warrior 30            | 30           | 15% + 2.25% = 17.25%         |
| Balanced           | Warrior 15 + Miner 15 | 30           | 15% + 2.25% = 17.25%         |
| Wide Generalist    | 6 classes at 10 each  | 60           | 15% + 4.5% = 19.5%           |
| Extreme Generalist | 10 classes at 8 each  | 80           | 15% + 6.0% = 21.0%           |

Note: While generalists gain stronger common skills, specialists gain stronger class-specific skills and reach higher tiers in their focused class.

### Example Progression: Quick Reflexes (Dodge Bonus)

Based on total combined class levels:

| Total Levels | Lesser (5% base) | Greater (10% base) | Enhanced (15% base) |
| ------------ | ---------------- | ------------------ | ------------------- |
| 1            | 5.05%            | 10.075%            | 15.075%             |
| 10           | 5.5%             | 10.75%             | 15.75%              |
| 20           | 6.0%             | 11.5%              | 16.5%               |
| 30           | 6.5%             | 12.25%             | 17.25%              |
| 50           | 7.5%             | 13.75%             | 18.75%              |
| 100          | 10.0%            | 17.5%              | 22.5%               |

Note: Generalists with many classes reach higher total levels faster, accelerating common skill growth.

---

## 16. Integration Points

| System        | Integration                                                          |
| ------------- | -------------------------------------------------------------------- |
| Classes       | All classes can roll common skills                                   |
| Attributes    | Skills often scale with relevant attribute                           |
| Combat        | Combat skills modify damage, defense, initiative                     |
| Crafting      | Crafting skills affect quality, efficiency                           |
| Gathering     | Gathering skills affect detection, yield                             |
| Social        | Social skills modify prices, persuasion, faction                     |
| Party         | Leadership skills affect party members                               |
| Rest          | Survival skills affect recovery                                      |
| Exploration   | Movement skills affect travel, discovery                             |
| Races         | All races have equal access                                          |
| Actions       | Relevant actions influence offer chance and tier                     |
| Total Levels  | Common skills scale with combined class levels (rewards generalists) |
| Consolidation | Total levels preserved through consolidation                         |
