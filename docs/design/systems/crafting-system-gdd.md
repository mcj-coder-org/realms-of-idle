---
type: system
scope: detailed
status: authoritative
version: 1.0.0
created: 2026-02-08
updated: 2026-02-08
subjects: [crafting, progression, recipes, materials, workshops, idle]
dependencies: [quality-tiers.md]
---

# Crafting System - Authoritative Game Design

## Executive Summary

The Crafting System governs how characters transform raw materials into equipment, consumables, and trade goods through timer-based production queues. Players select recipes, assign materials, and queue crafting jobs that complete over real time. Outcomes are calculated at timer completion using crafter skill, tool quality, and workspace tier as inputs.

**This document resolves:**

- Crafting progression path (Apprentice through Artificer)
- Timer-based production queue mechanics
- Outcome probability calculations
- Material refinement and batch processing
- Tool and workspace modifier systems
- Crafting energy and recharge mechanics
- Guild membership and benefits

**Design Philosophy:** Crafting is a strategic investment system. Players choose what to craft and with what materials, then the system executes over time. Higher investment (better materials, upgraded workspaces, skilled crafters) yields better outcomes. Offline progress ensures crafting queues always advance.

---

## 1. Crafting Progression

### 1.1 Rank Progression

Characters advance through crafting ranks by accumulating experience within a specialty:

| Rank       | XP Required | Max Recipe Tier | Max Output Quality | Queue Slots |
| ---------- | ----------- | --------------- | ------------------ | ----------- |
| Untrained  | 0           | Basic           | Standard (2)       | 1           |
| Apprentice | 500         | Apprentice      | Superior (3)       | 2           |
| Journeyman | 2,500       | Journeyman      | Masterwork (4)     | 3           |
| Master     | 10,000      | Master          | Legendary (5)      | 4           |
| Artificer  | Special     | Artificer       | Legendary (5)      | 5           |

### 1.2 Learning Paths

Two paths to rank advancement, each with idle-appropriate trade-offs:

```
APPRENTICESHIP PATH:
  - Assign character to NPC master crafter
  - Timer: 2 hours real-time to reach Apprentice
  - Produces Standard quality during training
  - Passive XP gain while assigned (offline-safe)
  - Cost: Training fee (Gold)

SELF-TAUGHT PATH:
  - Learn by crafting (XP from each completed job)
  - Timer: ~4 hours equivalent crafting time to Apprentice
  - Higher chance of Superior+ results once skilled
  - No fee, but slower and material-intensive
  - Bonus: +5% quality modifier at each rank
```

### 1.3 Artificer Consolidation

Artificer is a prestige rank requiring mastery across multiple specialties:

- **Requirement**: Master rank in 3 or more crafting specialties
- **Unlock trigger**: Completing a Masterwork item after meeting the requirement
- **Benefits**: Access to Artificer-tier recipes, cross-specialty crafting bonuses, +1 queue slot

### 1.4 Crafting Specialties

| Specialty     | Primary Output           | Key Materials        |
| ------------- | ------------------------ | -------------------- |
| Blacksmithing | Weapons, armour, tools   | Ores, ingots, fuel   |
| Woodworking   | Bows, staves, furniture  | Lumber, resins       |
| Tailoring     | Cloth armour, enchanting | Fabrics, threads     |
| Alchemy       | Potions, reagents        | Herbs, minerals      |
| Leatherwork   | Leather armour, bags     | Hides, tanning agent |
| Enchanting    | Enchantments, scrolls    | Crystals, ink        |

---

## 2. Recipe System

### 2.1 Recipe Tiers

Recipes unlock as crafters advance in rank:

| Tier       | Crafter Rank Required | Typical Timer | Material Complexity |
| ---------- | --------------------- | ------------- | ------------------- |
| Basic      | Untrained             | 5–15 min      | 1–2 materials       |
| Apprentice | Apprentice            | 15–60 min     | 2–3 materials       |
| Journeyman | Journeyman            | 1–4 hours     | 3–5 materials       |
| Master     | Master                | 4–12 hours    | 4–6 materials       |
| Legendary  | Master (rare unlock)  | 12–24 hours   | 5–8 materials       |
| Artificer  | Artificer             | 24–48 hours   | 6–10 materials      |

### 2.2 Recipe Discovery

Recipes are discovered through play, not purchased from catalogs:

- **Rank-up rewards**: 2–3 recipes offered at each rank milestone
- **Crafting discovery**: Small chance to discover related recipe on Great Success or better
- **NPC teaching**: Apprenticeship masters teach specialty recipes
- **Loot drops**: Rare recipe scrolls from gathering or combat
- **Guild library**: Guild members can copy recipes (consumes materials)

### 2.3 Representative Recipes

| Recipe            | Tier       | Timer  | Materials                               | Output         |
| ----------------- | ---------- | ------ | --------------------------------------- | -------------- |
| Iron Dagger       | Basic      | 10 min | 2× Iron Ingot                           | Weapon         |
| Steel Longsword   | Apprentice | 45 min | 3× Steel Ingot, 1× Leather Strip        | Weapon         |
| Healing Draught   | Apprentice | 20 min | 2× Healing Herb, 1× Purified Water      | Consumable     |
| Mithril Chainmail | Journeyman | 3 hr   | 5× Mithril Links, 2× Padding, 1× Clasp  | Armour         |
| Golem Core        | Artificer  | 48 hr  | 4× Crystal, 3× Enchanted Metal, 2× Rune | Construct part |

---

## 3. Crafting Queue Mechanics

### 3.1 Queue Flow

```
SELECT RECIPE
    │
    ▼
ASSIGN MATERIALS (player chooses quality)
    │
    ▼
QUEUE JOB (slot occupied, timer starts)
    │
    ▼
TIMER RUNS (real-time, continues offline)
    │
    ▼
TIMER COMPLETES → OUTCOME CALCULATED
    │
    ▼
COLLECT RESULT (next check-in)
```

### 3.2 Queue Slots

Queue slots determine how many concurrent crafting jobs a character can run:

- **Base slots**: Determined by rank (see Section 1.1)
- **Bonus slots**: +1 from Guild membership, +1 from premium workspace
- **Maximum**: 7 slots (5 rank + 1 guild + 1 workspace)
- **Queue behavior**: Jobs execute sequentially within a slot; multiple slots run in parallel

### 3.3 Timer Modifiers

Base timer is modified by equipment and workspace:

```
Effective Timer = Base Timer × Tool Modifier × Workspace Modifier × Energy Modifier

Tool Modifier (by quality tier):
  Poor       = 1.30 (30% slower)
  Standard   = 1.00 (baseline)
  Superior   = 0.90 (10% faster)
  Masterwork = 0.80 (20% faster)
  Legendary  = 0.70 (30% faster)

Workspace Modifier (by tier):
  None        = 1.40 (40% slower, crafting outdoors)
  Basic       = 1.00 (baseline)
  Improved    = 0.85 (15% faster)
  Advanced    = 0.70 (30% faster)
  Master      = 0.60 (40% faster)

Energy Modifier:
  Full energy   = 1.00
  Low energy    = 1.20 (20% slower)
  Exhausted     = 1.50 (50% slower, no new jobs until recharged)
```

---

## 4. Outcome System

### 4.1 Outcome Tiers

Each completed crafting job produces one of six outcomes:

| Outcome          | Quality Impact | XP Multiplier | Probability Range |
| ---------------- | -------------- | ------------- | ----------------- |
| Critical Failure | No output      | 0.25×         | 2–15%             |
| Failure          | No output      | 0.50×         | 5–20%             |
| Partial Success  | -1 quality     | 0.75×         | 10–25%            |
| Success          | As expected    | 1.00×         | 30–50%            |
| Great Success    | +0 (bonus XP)  | 1.50×         | 10–25%            |
| Masterwork       | +1 quality     | 2.00×         | 1–10%             |

### 4.2 Outcome Probability Formula

```
Base Success Rate = 50% + (Crafter Skill × 5) - (Recipe Difficulty × 3)

Modifiers:
  Tool Quality    : +/- 15% (Poor=-15%, Legendary=+15%)
  Workspace Tier  : +/- 20% (None=-20%, Master=+20%)
  Material Quality: +/- 10% (Poor=-10%, Perfect=+10%)

Final Success Rate = clamp(Base + Tool + Workspace + Material, 10%, 95%)
```

Outcome distribution based on final success rate:

```
Critical Failure = max(2%, (100% - Final) × 0.15)
Failure          = max(3%, (100% - Final) × 0.35)
Partial Success  = max(5%, (100% - Final) × 0.50)
Success          = Final × 0.60
Great Success    = Final × 0.30
Masterwork       = Final × 0.10
```

### 4.3 Masterwork Trigger

Creating a Masterwork-quality item (quality tier 4+) while at Journeyman rank triggers:

- **Master rank unlock** for that specialty
- One-time bonus: choice of 1 Master-tier recipe
- Achievement notification

---

## 5. Material System

### 5.1 Material Refinement

Raw materials can be refined to higher quality grades through batch processing:

```
REFINEMENT RECIPE:
  Input:  3× Material (Quality N) + 1× Reagent
  Output: 1× Material (Quality N+1)
  Timer:  15 min per batch

  Maximum refinement: Perfect (Quality 5)
  Reagent type varies by material category
```

### 5.2 Batch Processing (Offline)

Material refinement supports batch queuing for offline processing:

- Queue up to 10 refinement batches per slot
- All batches process sequentially during offline time
- Results accumulate in inventory for collection at check-in
- Failed batches return 1× original material (no reagent refund)

### 5.3 Material Quality Impact

Material quality constrains maximum output quality (see quality-tiers.md Section 4):

| Material Quality | Max Item Quality | Refinement Cost (3:1 ratio) |
| ---------------- | ---------------- | --------------------------- |
| Poor (1)         | Standard (2)     | N/A (base grade)            |
| Standard (2)     | Superior (3)     | 3× Poor + Reagent           |
| Fine (3)         | Masterwork (4)   | 3× Standard + Reagent       |
| Exceptional (4)  | Legendary (5)    | 3× Fine + Reagent           |
| Perfect (5)      | Legendary (5)    | 3× Exceptional + Reagent    |

---

## 6. Workspace System

### 6.1 Workspace Tiers

Workspaces are permanent upgrades that improve crafting efficiency:

| Tier     | Unlock Cost | Timer Modifier | Quality Bonus | Capacity   |
| -------- | ----------- | -------------- | ------------- | ---------- |
| None     | Free        | +40% slower    | -10%          | 1 crafter  |
| Basic    | 100 Gold    | Baseline       | 0%            | 1 crafter  |
| Improved | 500 Gold    | -15% faster    | +5%           | 2 crafters |
| Advanced | 2,500 Gold  | -30% faster    | +10%          | 3 crafters |
| Master   | 10,000 Gold | -40% faster    | +15%          | 4 crafters |

### 6.2 Workspace Specialization

Each workspace can be specialized for one crafting category:

- **Specialization bonus**: Additional -10% timer and +5% quality for matching recipes
- **Non-matching penalty**: None (just no bonus)
- **Respec cost**: 50% of upgrade cost to change specialization

---

## 7. Crafting Energy

### 7.1 Energy Mechanics

Crafting energy replaces stamina/fatigue with an idle-friendly recharge system:

```
Max Energy: 100 (base) + 10 per crafter rank
Recharge Rate: 1 energy per 3 minutes (real-time, including offline)
Full Recharge: ~5 hours from empty

Energy Cost per Job:
  Basic recipes      = 5 energy
  Apprentice recipes = 10 energy
  Journeyman recipes = 20 energy
  Master recipes     = 35 energy
  Legendary recipes  = 50 energy
  Artificer recipes  = 60 energy
```

### 7.2 Energy States

| State     | Energy Range | Effect                              |
| --------- | ------------ | ----------------------------------- |
| Full      | 80–100%      | No modifier                         |
| Normal    | 40–79%       | No modifier                         |
| Low       | 10–39%       | +20% timer duration                 |
| Exhausted | 0–9%         | Cannot start new jobs, +50% current |

### 7.3 Energy Recovery Boosts

- **Rest at inn**: Instant +50 energy (costs Gold, 1-hour cooldown)
- **Food consumable**: +20 energy (crafted via Alchemy)
- **Guild perk**: +10% max energy, +20% recharge rate

---

## 8. Crafting Guilds

### 8.1 Guild Benefits

| Benefit        | Description                                |
| -------------- | ------------------------------------------ |
| +1 Queue Slot  | Additional concurrent crafting job         |
| Recipe Library | Access to guild-shared recipes (copy cost) |
| Bulk Orders    | Accept NPC bulk orders for bonus Gold/XP   |
| Energy Perk    | +10% max energy, +20% recharge rate        |
| Rank Discount  | -20% training fees for apprenticeship path |

### 8.2 Guild Ranks

| Rank        | Requirement                     | Unlock                 |
| ----------- | ------------------------------- | ---------------------- |
| Initiate    | Join fee (50 Gold)              | Recipe library access  |
| Member      | 10 guild orders completed       | +1 queue slot          |
| Artisan     | Journeyman rank + 25 orders     | Bulk order access      |
| Elder       | Master rank + 50 orders         | Energy perk            |
| Guildmaster | Artificer rank + guild election | All perks + governance |

---

## 9. Repair and Upgrade

### 9.1 Repair System

Damaged equipment can be repaired through the crafting queue:

```
Repair Job:
  Timer: 25% of original craft time
  Materials: 1× matching material (any quality)
  Energy: 50% of original recipe cost

  Repair Quality:
    Crafter rank >= item quality → full restoration
    Crafter rank < item quality  → -1 quality per tier gap
```

### 9.2 Upgrade System

Items can be upgraded to the next quality tier:

```
Upgrade Job:
  Timer: 50% of original craft time
  Materials: 2× material at target quality
  Energy: 75% of original recipe cost

  Success Rate: 60% + (Crafter Skill × 5%)
  Failure: -1 quality (can be repaired)
  Max Upgrade: Crafter's quality ceiling
```

---

## 10. Idle Integration

### 10.1 Offline Behavior

| System             | Offline Behavior                                |
| ------------------ | ----------------------------------------------- |
| Crafting queues    | Continue running, results stored for collection |
| Material refining  | Batch queues process sequentially               |
| Energy recharge    | Recharges at normal rate                        |
| Guild orders       | Timer progresses, rewards accumulate            |
| Workspace upgrades | Construction timers continue                    |

### 10.2 Check-in Collection

On player check-in, the system presents:

- Completed crafting results with outcomes
- Accumulated refined materials
- Guild order rewards
- Energy status and any stalled queues

### 10.3 Progression Hooks

| Hook              | System Integration                        |
| ----------------- | ----------------------------------------- |
| Quality tiers     | Output quality feeds into economy pricing |
| Gathering system  | Materials sourced from gathering queues   |
| Economy system    | Crafted goods sold through trade routes   |
| Combat system     | Equipment quality affects combat stats    |
| Prestige (future) | Crafting mastery counts toward prestige   |
