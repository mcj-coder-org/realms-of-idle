---
type: system
scope: detailed
status: authoritative
version: 1.0.0
created: 2026-02-08
updated: 2026-02-08
subjects: [enchantment, disenchanting, magical-items, runes, scrolls, idle]
dependencies: [quality-tiers.md, crafting-system-gdd.md, magic-system-gdd.md]
---

# Enchantment System - Authoritative Game Design

## Executive Summary

The Enchantment System governs how characters add, remove, and recycle magical properties on items through timer-based enchanting queues. Players select an item, choose an enchantment recipe, assign magical materials, and queue the job. Results are calculated at timer completion and collected at check-in. Disenchanting closes the material loop by converting enchanted items back into recipes, materials, or clean base items.

**This document resolves:**

- Three enchantment power tiers (Magical, Relic, Artifact) and item quality gates
- Timer-based enchanting queue mechanics
- Enchantment slots per item quality tier
- Disenchanting modes (Learn, Salvage, Strip) with outcome probabilities
- Enchanter skill progression (Apprentice through Master)
- Scroll and rune crafting
- Enchantment discovery and timer-based research
- Risk of failure and item damage
- Offline enchantment queue processing
- Enchanter vs Artificer role distinction

**Design Philosophy:** Enchanting is a strategic enhancement layer on top of crafting. Players decide which items to enhance, which enchanted items to break down, and how to allocate limited enchanting materials. The dual quality model (item quality + enchantment tier) creates meaningful choices: a Superior item with a Magical enchantment is accessible early, while a Legendary item with an Artifact enchantment represents endgame power. Timer lengths and outcome probabilities create trade-offs between the three disenchanting modes. Offline progress ensures enchanting queues always advance.

---

## 1. Enchantment Power Tiers

Enchantments use a three-tier power scale that interacts with the item quality system defined in [quality-tiers.md](../reference/systems/quality-tiers.md).

### 1.1 Tier Definitions

| Enchant Tier | Power Level | Numeric | Enchanter Rank Required | Example Effects              |
| ------------ | ----------- | ------- | ----------------------- | ---------------------------- |
| Magical      | Basic       | 1       | Apprentice              | +Fire damage, +10 HP         |
| Relic        | Moderate    | 2       | Journeyman              | Vampiric, elemental AoE      |
| Artifact     | Powerful    | 3       | Master                  | Multi-enchant, unique powers |

### 1.2 Item Quality Requirements

Not all items can hold enchantments. Item quality determines the maximum enchantment tier (see [quality-tiers.md](../reference/systems/quality-tiers.md) Section 5):

| Item Quality   | Max Enchant Tier | Reason                                  |
| -------------- | ---------------- | --------------------------------------- |
| Poor (1)       | Cannot enchant   | Too fragile to hold magic               |
| Standard (2)   | Cannot enchant   | Insufficient quality for magic          |
| Superior (3)   | Magical only     | Supports basic enchantments             |
| Masterwork (4) | Up to Relic      | Strong enough for moderate enchantments |
| Legendary (5)  | Up to Artifact   | Supports the most powerful enchantments |

This restriction creates demand for high-quality crafted items (see [crafting-system-gdd.md](crafting-system-gdd.md)) and prevents trivialising the enchantment economy.

### 1.3 Enchantment Slots

Higher-quality items support more simultaneous enchantments:

| Item Quality   | Max Enchantments | Who Can Apply                       |
| -------------- | ---------------- | ----------------------------------- |
| Superior (3)   | 1                | Apprentice+ Enchanter               |
| Masterwork (4) | 2                | Master Enchanter (compatible types) |
| Legendary (5)  | 3                | Master Enchanter or Artificer       |

Stacking rules: the same enchantment across different equipped items does not stack (highest value applies). Different enchantments on one item both apply. Incompatible pairs cannot coexist (see Section 7.3).

### 1.4 Enchantment Categories

| Category    | Examples                                   | Typical Use          |
| ----------- | ------------------------------------------ | -------------------- |
| Offensive   | Flaming, Frost, Lightning, Slaying         | Weapon damage boosts |
| Defensive   | Protection, Resistance, Thorns             | Armour enhancement   |
| Utility     | Holding, Preservation, Sorting, Swift Step | Quality of life      |
| Restoration | Regeneration, Auto-Repair                  | Passive recovery     |
| Social      | Presence                                   | NPC interaction      |

---

## 2. Enchanter Skill Progression

### 2.1 Rank Progression

Characters advance through Enchanter ranks by accumulating enchanting experience:

| Rank       | XP Required | Max Enchant Tier | Queue Slots | Research Slots |
| ---------- | ----------- | ---------------- | ----------- | -------------- |
| Apprentice | 500         | Magical          | 1           | 1              |
| Journeyman | 2,500       | Relic            | 2           | 2              |
| Master     | 10,000      | Artifact         | 3           | 3              |

Bonus queue slots: +1 from Enchanter Guild membership, +1 from Master-tier workspace. Maximum 5 enchanting queue slots.

### 2.2 Learning Paths

Two paths to Enchanter advancement, consistent with the crafting system (see [crafting-system-gdd.md](crafting-system-gdd.md) Section 1.2):

```
APPRENTICESHIP PATH:
  - Assign character to NPC Enchanter
  - Timer: 2 hours real-time to reach Apprentice
  - Can only enchant Magical tier during training
  - Passive XP gain while assigned (offline-safe)
  - Cost: Training fee (Gold)

SELF-TAUGHT PATH:
  - Learn by enchanting (XP from each completed job)
  - Timer: ~4 hours equivalent enchanting time to Apprentice
  - Higher chance of Great Success once skilled
  - No fee, but slower and material-intensive
  - Bonus: +5% success modifier at each rank
```

### 2.3 XP Sources

| Activity                   | XP Multiplier |
| -------------------------- | ------------- |
| Successful enchantment     | 1.0x base     |
| Great Success enchantment  | 1.5x base     |
| Masterwork enchantment     | 2.0x base     |
| Failed enchantment         | 0.5x base     |
| Learn (disenchant) success | 2.0x base     |
| Learn (disenchant) failure | 0.25x base    |
| Scroll crafting            | 0.75x base    |
| Rune crafting              | 1.0x base     |
| Research experiment        | 0.5x base     |

### 2.4 Enchanter Skills

Passive bonuses unlocked at rank milestones:

| Skill              | Effect                           | Unlocked At           |
| ------------------ | -------------------------------- | --------------------- |
| Magical Analysis   | +10% learn success rate          | Apprentice (1,000 XP) |
| Material Insight   | +15% material recovery (Salvage) | Journeyman            |
| Careful Extraction | -50% item damage risk (Strip)    | Journeyman (5,000 XP) |
| Rune Mastery       | -20% rune crafting timer         | Master                |
| Arcane Efficiency  | -10% material costs              | Master (15,000 XP)    |

Cross-class skills that affect enchanting:

| Skill           | Effect                      | Source       |
| --------------- | --------------------------- | ------------ |
| Appraisal       | Reveals enchantment details | Trader class |
| Mana Efficiency | -20% Dispel mana cost       | Mage class   |

---

## 3. Enchanter vs Artificer

Two distinct roles interact with the enchantment system:

| Aspect       | Enchanter                          | Artificer                               |
| ------------ | ---------------------------------- | --------------------------------------- |
| Process      | Applies enchantments to items      | Builds enchantments into items at craft |
| Requirements | Enchanting specialization          | Master rank in 3+ crafting specialties  |
| Flexibility  | Can enchant any compatible item    | Only items they craft                   |
| Strength     | Services, disenchanting, recycling | Stronger integrated enchantments        |
| Max enchants | 2 (Master)                         | 3 (built-in)                            |

### 3.1 Synergy

Artificers and Enchanters can stack their work:

1. Artificer crafts item with 1-3 integrated enchantments
2. Enchanter applies 1-2 additional enchantments on top
3. Result: up to 5 enchantments on a single item (extremely rare, requires Master Enchanter + Artificer + Legendary base item)

See [crafting-system-gdd.md](crafting-system-gdd.md) Section 1.3 for Artificer rank requirements.

---

## 4. Enchanting Queue Mechanics

### 4.1 Queue Flow

```
SELECT ITEM (must meet quality requirement, Section 1.2)
    |
    v
SELECT ENCHANTMENT RECIPE (must be learned, Section 6)
    |
    v
ASSIGN MATERIALS (essences, catalysts, optional rune)
    |
    v
QUEUE JOB (slot occupied, timer starts)
    |
    v
TIMER RUNS (real-time, continues offline)
    |
    v
TIMER COMPLETES -> OUTCOME CALCULATED
    |
    v
COLLECT RESULT (next check-in)
```

### 4.2 Enchanting Timers

Base enchanting timers scale with enchantment tier:

| Enchant Tier | Base Timer | Energy Cost |
| ------------ | ---------- | ----------- |
| Magical      | 30-60 min  | 15          |
| Relic        | 2-4 hours  | 30          |
| Artifact     | 8-16 hours | 50          |

### 4.3 Timer Modifiers

Timer modifiers follow the same structure as crafting (see [crafting-system-gdd.md](crafting-system-gdd.md) Section 3.3):

```
Effective Timer = Base Timer x Tool Modifier x Workspace Modifier x Energy Modifier

Tool Modifier (enchanting focus quality):
  Poor       = 1.30 (30% slower)
  Standard   = 1.00 (baseline)
  Superior   = 0.90 (10% faster)
  Masterwork = 0.80 (20% faster)
  Legendary  = 0.70 (30% faster)

Workspace Modifier (enchanting table tier):
  None       = 1.40 (40% slower, improvised)
  Basic      = 1.00 (baseline)
  Improved   = 0.85 (15% faster)
  Advanced   = 0.70 (30% faster)
  Master     = 0.60 (40% faster)

Energy Modifier:
  Full energy   = 1.00
  Low energy    = 1.20 (20% slower)
  Exhausted     = 1.50 (50% slower, no new jobs)
```

---

## 5. Enchanting Outcomes and Failure Risk

### 5.1 Outcome Tiers

Each completed enchanting job produces one of five outcomes:

| Outcome          | Effect                                | XP Multiplier | Probability Range |
| ---------------- | ------------------------------------- | ------------- | ----------------- |
| Critical Failure | Item damaged (-1 quality), no enchant | 0.25x         | 2-10%             |
| Failure          | No enchant applied, materials lost    | 0.50x         | 5-15%             |
| Success          | Enchantment applied as expected       | 1.00x         | 50-70%            |
| Great Success    | Enchantment applied with +10% power   | 1.50x         | 15-25%            |
| Masterwork       | Enchantment at +25% power             | 2.00x         | 3-8%              |

### 5.2 Success Rate Formula

```
Base Success Rate = 55% + (Enchanter Skill x 5) - (Enchant Tier x 10)

Modifiers:
  Workspace Tier  : +/- 15% (None=-15%, Master=+15%)
  Material Quality: +/- 10% (Standard=-10%, Perfect=+10%)
  Rune Bonus      : +5% to +15% (see Section 8.2)
  Skill Bonuses   : See Section 2.4

Final Success Rate = clamp(Base + Modifiers, 15%, 95%)
```

### 5.3 Failure Consequences

| Outcome          | Item Effect     | Materials | Recovery Option            |
| ---------------- | --------------- | --------- | -------------------------- |
| Critical Failure | -1 quality tier | Lost      | Repair via crafting system |
| Failure          | No change       | Lost      | Retry with new materials   |

Critical Failure on a Poor-quality item destroys it entirely (cannot drop below Poor). This makes enchanting inherently risky on lower-quality items and incentivises using the best base items available.

### 5.4 Quality Degradation from Enchanting

Failed enchanting interacts with the quality degradation rules in [quality-tiers.md](../reference/systems/quality-tiers.md) Section 8:

| Starting Quality | After Critical Failure | Can Still Enchant? |
| ---------------- | ---------------------- | ------------------ |
| Legendary (5)    | Masterwork (4)         | Yes (up to Relic)  |
| Masterwork (4)   | Superior (3)           | Yes (Magical only) |
| Superior (3)     | Standard (2)           | No                 |

A Superior item that suffers a Critical Failure drops to Standard, losing enchantability entirely. This creates meaningful risk when enchanting at the minimum quality threshold.

---

## 6. Enchantment Discovery and Research

### 6.1 Recipe Sources

Enchantment recipes are discovered through play, not purchased from catalogs:

| Source                | Description                                    | Timer          |
| --------------------- | ---------------------------------------------- | -------------- |
| Learn (disenchant)    | Destroy enchanted item to learn its recipe     | 1-2 hours      |
| Research (experiment) | Combine materials to discover new enchantments | 4-12 hours     |
| Rank-up rewards       | 1-2 recipes offered at Enchanter milestones    | N/A            |
| NPC teaching          | Apprenticeship masters teach basic recipes     | Training timer |
| Loot drops            | Rare recipe scrolls from exploration/combat    | N/A            |
| Guild library         | Copy guild-shared recipes (consumes materials) | 30 min         |

### 6.2 Research System (Timer-Based Experimentation)

Research allows Enchanters to discover new enchantments by experimenting with material combinations. Research slots are separate from enchanting queue slots -- a character can enchant and research simultaneously.

```
RESEARCH FLOW:
  SELECT RESEARCH SLOT (see Section 2.1 for slot count)
      |
      v
  ASSIGN MATERIALS (2-4 experimental reagents)
      |
      v
  START EXPERIMENT TIMER (4-12 hours, continues offline)
      |
      v
  TIMER COMPLETES -> DISCOVERY CHECK
      |
      v
  COLLECT RESULT (next check-in)
```

### 6.3 Research Outcomes

| Outcome      | Probability | Result                                    |
| ------------ | ----------- | ----------------------------------------- |
| No Discovery | 40-60%      | Materials consumed, small XP gained       |
| Hint         | 20-30%      | Partial recipe clue, narrows next attempt |
| Discovery    | 10-25%      | New enchantment recipe learned            |
| Breakthrough | 1-5%        | Recipe learned + bonus rare material      |

### 6.4 Material-Guided Discovery

Specific material combinations weight discovery toward certain enchantment types:

| Materials Used     | Biases Toward          |
| ------------------ | ---------------------- |
| Fire Essence       | Fire enchantments      |
| Protective Crystal | Defensive enchantments |
| Life Essence       | Restoration enchants   |
| Chaos Essence      | Utility enchantments   |

Hints from failed experiments narrow the discovery space for subsequent attempts, rewarding persistent research investment.

---

## 7. Enchantment Types

### 7.1 Representative Weapon Enchantments

| Enchantment       | Tier     | Effect                            | Materials Required           |
| ----------------- | -------- | --------------------------------- | ---------------------------- |
| of Fire           | Magical  | +Fire damage, chance to ignite    | 2x Fire Essence, 1x Catalyst |
| of Sharpness      | Magical  | +Crit chance, +Crit damage        | 2x Steel Dust, 1x Catalyst   |
| of Leeching       | Relic    | Heal on hit (% of damage dealt)   | 3x Life Essence, 2x Catalyst |
| of Slaying [Type] | Relic    | +Damage vs specific creature type | 3x Type Essence, 1x Rune     |
| of Disruption     | Artifact | Extra damage vs undead/constructs | 4x Holy Essence, 2x Rune     |

### 7.2 Representative Utility Enchantments

| Enchantment     | Tier     | Effect                           | Materials Required             |
| --------------- | -------- | -------------------------------- | ------------------------------ |
| of Holding      | Magical  | +10 storage slots                | 2x Space Essence, 1x Catalyst  |
| of Preservation | Magical  | Prevents stored item decay       | 2x Time Essence, 1x Catalyst   |
| of Plenty       | Relic    | Generates food at intervals      | 3x Nature Essence, 2x Catalyst |
| of Compounding  | Artifact | Chance to duplicate stored items | 4x Chaos Essence, 2x Rune      |

### 7.3 Enchantment Compatibility

When applying multiple enchantments to a single item (Masterwork+ only, see Section 1.3):

**Compatible Pairs:**

| Type 1           | Compatible With       |
| ---------------- | --------------------- |
| Elemental damage | Slaying, Leeching     |
| Attribute boost  | Any other attribute   |
| Regeneration     | Protection, Attribute |
| Skill grant      | Related attribute     |

**Incompatible Pairs:**

| Type 1         | Conflicts With             |
| -------------- | -------------------------- |
| of Fire        | of Ice                     |
| of Speed       | of Weight                  |
| of Slaying [X] | of Slaying [Y] (different) |

Attempting to apply an incompatible enchantment cancels the job before materials are consumed.

---

## 8. Scroll and Rune Crafting

Enchanters can create portable enchantment items that store magical effects for later use.

### 8.1 Scrolls

Scrolls are single-use items that apply a known enchantment without requiring an Enchanter to be present.

| Scroll Tier | Enchant Tier Applied | Crafting Timer | Materials Required                           |
| ----------- | -------------------- | -------------- | -------------------------------------------- |
| Minor       | Magical              | 20-45 min      | 1x Parchment, 1x Rune Ink, 1x Essence        |
| Greater     | Relic                | 1-3 hours      | 1x Fine Parchment, 2x Rune Ink, 2x Essence   |
| Supreme     | Artifact             | 4-8 hours      | 1x Master Parchment, 3x Rune Ink, 3x Essence |

**Scroll Properties:**

- Single-use: consumed on application
- No Enchanter required to use (anyone can apply)
- Tradeable: scrolls are valuable trade goods
- Success rate: Fixed at 80% (not modified by user skill)
- On failure: Scroll consumed, item unchanged, no damage

**Scroll Crafting Requirements:**

| Scroll Tier | Enchanter Rank Required | Workspace Required |
| ----------- | ----------------------- | ------------------ |
| Minor       | Apprentice              | Basic              |
| Greater     | Journeyman              | Improved           |
| Supreme     | Master                  | Advanced           |

### 8.2 Runes

Runes are reusable enchanting catalysts that improve enchantment outcomes when used as an optional material slot in enchanting jobs.

| Rune Type    | Effect When Used in Enchanting | Crafting Timer | Durability |
| ------------ | ------------------------------ | -------------- | ---------- |
| Lesser Rune  | +5% success rate               | 30 min         | 5 uses     |
| Greater Rune | +10% success rate, -10% timer  | 2 hours        | 10 uses    |
| Master Rune  | +15% success rate, -20% timer  | 6 hours        | 20 uses    |

**Rune Crafting Materials:**

```
Lesser Rune:  1x Runestone + 1x Magical Dust + 1x Catalyst
Greater Rune: 1x Runestone + 2x Enchanting Essence + 2x Catalyst
Master Rune:  1x Runestone + 3x Rare Essence + 3x Catalyst + 1x Lesser Rune
```

**Rune Properties:**

- Reusable: durability decreases by 1 per enchanting job
- Occupies an optional material slot in the enchanting recipe
- Not consumed fully per job (unlike essences and catalysts)
- Rune Mastery skill (Master rank) reduces rune crafting timer by 20%
- Tradeable: runes are valuable enchanting aids

**Rune Crafting Requirements:**

| Rune Type    | Enchanter Rank Required | Workspace Required |
| ------------ | ----------------------- | ------------------ |
| Lesser Rune  | Apprentice              | Basic              |
| Greater Rune | Journeyman              | Improved           |
| Master Rune  | Master                  | Advanced           |

---

## 9. Enchanting Materials

### 9.1 Material Categories

| Material         | Source                           | Used For                    |
| ---------------- | -------------------------------- | --------------------------- |
| Essence (typed)  | Disenchant (Salvage), gathering  | Primary enchanting reagent  |
| Catalyst Crystal | Mining, purchasing               | Required for all enchanting |
| Magical Dust     | Disenchant (any mode), gathering | General reagent             |
| Runestone        | Mining rare nodes                | Rune crafting base          |
| Parchment        | Crafted (Tailoring/Woodwork)     | Scroll crafting base        |
| Rune Ink         | Crafted (Alchemy)                | Scroll crafting reagent     |

### 9.2 Essence Types

Essences determine what enchantment types can be created or discovered via research:

| Essence Type   | Enchantment Category | Gathering Source      |
| -------------- | -------------------- | --------------------- |
| Fire Essence   | Fire enchantments    | Volcanic areas        |
| Ice Essence    | Cold enchantments    | Frozen regions        |
| Life Essence   | Healing, leeching    | Sacred groves         |
| Holy Essence   | Anti-undead, purity  | Temples, holy sites   |
| Nature Essence | Growth, food gen     | Forests, fertile land |
| Chaos Essence  | Duplication, random  | Rift zones, dungeons  |
| Space Essence  | Storage, dimensional | Caves, deep mines     |
| Time Essence   | Preservation, speed  | Ancient ruins         |

### 9.3 Material Quality Impact

Higher-quality enchanting materials improve outcomes (interacts with quality-tier stat modifiers in [quality-tiers.md](../reference/systems/quality-tiers.md) Section 3):

| Material Quality | Success Modifier | Timer Modifier | Enchant Strength |
| ---------------- | ---------------- | -------------- | ---------------- |
| Poor             | -10%             | +10% slower    | -10% effect      |
| Standard         | Baseline         | Baseline       | Baseline         |
| Fine             | +5%              | -5% faster     | +5% effect       |
| Exceptional      | +10%             | -10% faster    | +10% effect      |
| Perfect          | +15%             | -15% faster    | +15% effect      |

### 9.4 Material Cycle

```
Gathering -> Crafting -> Enchanting -> Use -> Disenchant -> Materials
     ^                                           |
     |                                           |
     +------------- Essences + Dust <------------+
```

This cycle ensures enchanted items are never dead ends. Every enchanted item can be broken down to fuel further enchanting, creating a meaningful recycling economy.

---

## 10. Disenchanting System

Disenchanting allows Enchanters to remove enchantments from items. Three modes offer different risk/reward trade-offs, each queued as a timed job.

### 10.1 Mode Overview

| Mode    | Item Fate | Recipe Learned | Materials Recovered | Timer     |
| ------- | --------- | -------------- | ------------------- | --------- |
| Learn   | Destroyed | Yes            | None                | 1-2 hr    |
| Salvage | Destroyed | No             | 40-85% recovery     | 30-60 min |
| Strip   | Preserved | No             | None                | 15-30 min |

### 10.2 Learn Mode

Destroy an enchanted item to permanently learn its enchantment recipe.

**Success Rate by Enchanter Rank:**

| Rank       | Learn Success | Accessible Tiers         |
| ---------- | ------------- | ------------------------ |
| Apprentice | 70%           | Magical only             |
| Journeyman | 85%           | Magical + Relic          |
| Master     | 95%           | All tiers incl. Artifact |

**On failure:** Item is destroyed, recipe is NOT learned, small XP gained (0.25x base). Can retry with another item carrying the same enchantment.

**On success:** Enchantment recipe permanently added to known recipes. Item destroyed.

### 10.3 Salvage Mode

Destroy an enchanted item to recover enchanting materials.

**Recovery Rate by Enchanter Rank:**

| Rank       | Base Recovery | Max Recovery | Material Types Recovered |
| ---------- | ------------- | ------------ | ------------------------ |
| Apprentice | 40%           | 55%          | Common essences only     |
| Journeyman | 55%           | 70%          | Includes rare essences   |
| Master     | 70%           | 85%          | All material types       |

**Materials recovered:** Typed essences (matching enchant type), catalyst residue, magical dust.

### 10.4 Strip Mode

Remove enchantment while preserving the base item.

**Success Rate by Enchanter Rank:**

| Rank       | Strip Success | On Failure            |
| ---------- | ------------- | --------------------- |
| Apprentice | 60%           | Item loses -1 quality |
| Journeyman | 80%           | Durability loss only  |
| Master     | 95%           | No damage             |

**Use cases:** Repurpose high-quality base items, remove unwanted enchantments, prepare items for re-enchanting with a better recipe.

### 10.5 Mage Dispel Alternative

Mages with the Dispel spell can strip enchantments without being Enchanters:

| Enchant Tier | Mana Cost | Spell Required    |
| ------------ | --------- | ----------------- |
| Magical      | 30        | Dispel (Lesser)   |
| Relic        | 60        | Dispel (Greater)  |
| Artifact     | 100       | Dispel (Enhanced) |

**Mage Dispel vs Enchanter Strip:**

| Aspect      | Enchanter Strip          | Mage Dispel             |
| ----------- | ------------------------ | ----------------------- |
| Can learn   | No (use Learn mode)      | No                      |
| Can salvage | No (use Salvage mode)    | No                      |
| Resource    | Time + workspace         | Mana (instant)          |
| Item risk   | Depends on rank          | No damage risk          |
| Flexibility | Requires Enchanter class | Only needs Dispel spell |
| Idle fit    | Timer-based queue        | Instant on check-in     |

Mage Dispel always preserves the item but never learns recipes or recovers materials. It trades mana for safety, making it the preferred option when the base item is the priority.

See magic-system-gdd.md for Dispel spell details.

### 10.6 Disenchanting Restrictions

| Restriction    | Reason                              |
| -------------- | ----------------------------------- |
| Equipped items | Must unequip first                  |
| Cursed items   | Require special curse removal first |
| Bound items    | Some legendaries are soul-bound     |
| Quest items    | Protected by quest system           |

### 10.7 Bound Enchantments

Some powerful enchantments resist removal:

| Bound Type      | Can Learn? | Can Salvage? | Can Strip? |
| --------------- | ---------- | ------------ | ---------- |
| Normal          | Yes        | Yes          | Yes        |
| Soul-bound      | No         | Partial      | No         |
| Legendary-bound | Yes\*      | No           | No         |
| Cursed          | No         | No           | Special    |

\*Legendary-bound items can only be learned by Master Enchanters.

### 10.8 Disenchanting Decision Matrix

When processing enchanted loot, the optimal action depends on context:

| Situation               | Recommended Action            |
| ----------------------- | ----------------------------- |
| Unknown enchantment     | Use Appraisal first           |
| Good base, bad enchant  | Strip (preserve base item)    |
| Bad base, good enchant  | Learn (if unknown) or Salvage |
| Need the enchant recipe | Learn (destroys item)         |
| Need materials now      | Salvage (quick resources)     |
| Item is valuable as-is  | Sell (do not disenchant)      |

---

## 11. Workspace Requirements

### 11.1 Enchanting Workspace Tiers

Enchanting uses the same workspace system as crafting (see [crafting-system-gdd.md](crafting-system-gdd.md) Section 6) but requires an enchanting-specialised workspace:

| Workspace Tier | Unlock Cost | Enchanting Bonus         | Disenchanting Bonus   |
| -------------- | ----------- | ------------------------ | --------------------- |
| None           | Free        | Cannot enchant           | -20% success rate     |
| Basic          | 150 Gold    | Baseline                 | Baseline              |
| Improved       | 750 Gold    | +5% success, -15% timer  | +5% recovery/success  |
| Advanced       | 3,500 Gold  | +10% success, -25% timer | +10% recovery/success |
| Master         | 15,000 Gold | +15% success, -35% timer | +15% recovery/success |

### 11.2 Minimum Workspace by Activity

| Activity              | Minimum Workspace |
| --------------------- | ----------------- |
| Enchant (Magical)     | Basic             |
| Enchant (Relic)       | Improved          |
| Enchant (Artifact)    | Advanced          |
| Disenchant (Learn)    | Basic             |
| Disenchant (Salvage)  | Basic             |
| Disenchant (Strip)    | Improved          |
| Research (experiment) | Improved          |
| Scroll crafting       | Basic             |
| Rune crafting         | Advanced          |

---

## 12. Offline Queue Processing

### 12.1 Offline Behavior

| System               | Offline Behavior                                |
| -------------------- | ----------------------------------------------- |
| Enchanting queues    | Continue running, results stored for collection |
| Disenchanting jobs   | Timer progresses, outcomes calculated           |
| Research experiments | Timer progresses, discovery checked at login    |
| Material recovery    | Salvage results accumulate in inventory         |
| Recipe learning      | Success/failure determined at timer completion  |
| Energy recharge      | Recharges at normal rate                        |

### 12.2 Batch Processing

For bulk disenchanting (Salvage mode), players can queue multiple items:

- Up to 10 salvage jobs per queue slot
- All jobs process sequentially during offline time
- Results accumulate in inventory for collection at check-in
- Individual failures do not halt the batch

### 12.3 Check-in Collection

On player check-in, the enchanting system presents:

- Completed enchantment results with outcomes (success/failure/masterwork)
- Completed disenchanting results (recipes learned, materials recovered)
- Research experiment outcomes (discoveries, hints, failures)
- Energy status and any stalled queues
- Rune durability warnings (approaching 0 uses)

---

## 13. Economic Integration

### 13.1 Enchanted Item Values

```
Value = Base Item Value x Quality Multiplier x Enchant Tier Multiplier x Enchant Type Modifier

Enchant Tier Multipliers:
  Magical  = 2.0x
  Relic    = 4.0x
  Artifact = 8.0x

Enchant Type Modifiers:
  Utility    = 1.5x
  Defensive  = 2.0x
  Attribute  = 2.5x
  Offensive  = 3.0x
  Unique     = 5.0x+
```

Quality multipliers are defined in [quality-tiers.md](../reference/systems/quality-tiers.md) Section 9.

### 13.2 Enchanting Services

NPC Enchanters and player Enchanters offer services:

| Service           | Typical Fee            | Provider       |
| ----------------- | ---------------------- | -------------- |
| Enchant (Magical) | Materials + flat fee   | Enchanter NPCs |
| Salvage (service) | 10% of recovered value | Enchanter NPCs |
| Strip (service)   | Flat fee by item tier  | Enchanter NPCs |
| Dispel (service)  | Mana cost + small fee  | Mage NPCs      |

### 13.3 Guild Benefits

Enchanter Guild membership provides benefits consistent with the crafting guild system (see [crafting-system-gdd.md](crafting-system-gdd.md) Section 8):

| Benefit                  | Effect                                    |
| ------------------------ | ----------------------------------------- |
| +1 Queue Slot            | Additional concurrent enchanting job      |
| Recipe library access    | Copy guild-shared recipes (material cost) |
| Bulk salvage processing  | Queue multiple disenchant jobs            |
| Material trading network | Access to recovered materials market      |
| Research collaboration   | +10% discovery chance from guild data     |
| Reduced workspace fees   | 50% off guild workshop use                |

---

## 14. Cross-System Dependencies

| System        | Interaction                                          | Reference                                                       |
| ------------- | ---------------------------------------------------- | --------------------------------------------------------------- |
| Quality Tiers | Item quality gates enchantment tier and slot count   | [quality-tiers.md](../reference/systems/quality-tiers.md) Sec 5 |
| Crafting      | Produces base items, scroll/rune materials           | [crafting-system-gdd.md](crafting-system-gdd.md) Sec 1-5        |
| Magic System  | Mage Dispel as alternative strip method (mana-based) | magic-system-gdd.md                                             |
| Economy       | Enchanted item pricing, services, material markets   | [quality-tiers.md](../reference/systems/quality-tiers.md) Sec 9 |
| Combat        | Enchantment effects modify combat calculations       | ---                                                             |
| Gathering     | Source of essences, runestones, and catalysts        | ---                                                             |

### 14.1 Progression Hooks

| Hook              | System Integration                               |
| ----------------- | ------------------------------------------------ |
| Quality tiers     | Item quality gates enchantment tier access       |
| Crafting system   | Crafted items feed into enchanting pipeline      |
| Gathering system  | Essences and crystals sourced from gathering     |
| Economy system    | Enchanted item values drive trade                |
| Combat system     | Enchantment effects modify combat stats          |
| Magic system      | Dispel spell as alternative disenchanting method |
| Prestige (future) | Enchanting mastery counts toward prestige        |
