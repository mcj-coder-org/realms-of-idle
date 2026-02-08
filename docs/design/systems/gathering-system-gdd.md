---
type: system
scope: detailed
status: authoritative
version: 1.0.0
created: 2026-02-08
updated: 2026-02-08
subjects: [gathering, resources, mining, logging, farming, fishing, herbalism, idle]
dependencies: [quality-tiers.md, crafting-system-gdd.md]
---

# Gathering System - Authoritative Game Design

## Executive Summary

The Gathering System governs how characters collect raw materials from the world through timer-based gathering queues. Players select resource nodes, assign gatherers with appropriate tools, and queue gathering jobs that cycle over real time. Yield quality, quantity, and bonus drops are calculated at cycle completion using gatherer skill, tool quality, node properties, and environmental modifiers as inputs.

**This document resolves:**

- Seven gathering categories (Mining, Logging, Herbalism, Farming, Fishing, Hunting, Foraging)
- Timer-based gathering queue mechanics
- Resource quality integration with the 1-5 quality tier scale
- Gatherer skill progression and rank advancement
- Node discovery, depletion, and renewal cycles
- Ecosystem dynamics and over-harvesting consequences
- Offline accumulation and batch processing rules
- Tool quality impact on timer reduction and yield
- Seasonal and environmental modifiers
- Cross-system integration with crafting and economy

**Design Philosophy:** Gathering is a strategic resource investment system. Players choose where to gather, which skills to develop, and how to balance short-term yield against long-term sustainability. Execution is automated through timer queues. Higher investment (better tools, skilled gatherers, sustainable practices) yields better long-term outcomes. Offline progress ensures gathering queues always advance.

---

## 1. Gathering Categories

### 1.1 Category Overview

Seven gathering categories provide the raw material inputs for crafting, economy, and settlement systems:

| Category  | Node Examples              | Primary Use          | Matching Tool |
| --------- | -------------------------- | -------------------- | ------------- |
| Mining    | Ore veins, gem deposits    | Smithing, enchanting | Pickaxe       |
| Logging   | Forests, groves            | Woodworking, fuel    | Axe           |
| Herbalism | Wild herbs, mushrooms      | Alchemy, cooking     | Sickle        |
| Farming   | Wild fields, orchards      | Cooking, trade       | Hoe / Basket  |
| Fishing   | Rivers, lakes, coast       | Cooking, alchemy     | Fishing Rod   |
| Hunting   | Herds, dens, nests         | Leather, cooking     | Hunting Bow   |
| Foraging  | Berry bushes, root patches | Alchemy, cooking     | Gathering Bag |

### 1.2 Category Properties

Each category has distinct depletion and regeneration characteristics:

| Category  | Depletion Rate     | Regen Speed              | Overflow Behavior          |
| --------- | ------------------ | ------------------------ | -------------------------- |
| Mining    | Slow (per harvest) | Very slow (days/weeks)   | None (finite until regen)  |
| Logging   | Per tree felled    | Very slow (days/weeks)   | Slow propagation           |
| Herbalism | Per harvest        | Moderate (hours/days)    | Propagates if unharvested  |
| Farming   | Per harvest        | Seasonal/cyclical        | Propagates if unharvested  |
| Fishing   | Per catch          | Moderate (breeds to cap) | Migrates to neighbor nodes |
| Hunting   | Per kill           | Slow (breeds to cap)     | Migrates to neighbor nodes |
| Foraging  | Fast (per harvest) | Fast (hours)             | Propagates nearby          |

### 1.3 Representative Resources

| Resource       | Category  | Base Quality    | Gather Timer | Regen Rate | Crafting Use              |
| -------------- | --------- | --------------- | ------------ | ---------- | ------------------------- |
| Iron Ore       | Mining    | Standard (2)    | 30 min       | 4 hr       | Blacksmithing ingots      |
| Mithril Ore    | Mining    | Fine (3)        | 2 hr         | 24 hr      | High-tier smithing        |
| Oak Log        | Logging   | Standard (2)    | 25 min       | 12 hr      | Woodworking, fuel         |
| Ironwood Log   | Logging   | Fine (3)        | 1.5 hr       | 48 hr      | Masterwork bows/staves    |
| Healing Herb   | Herbalism | Standard (2)    | 15 min       | 2 hr       | Alchemy (Healing Draught) |
| Moonpetal      | Herbalism | Fine (3)        | 1 hr         | 12 hr      | Advanced alchemy          |
| Wild Wheat     | Farming   | Poor (1)        | 10 min       | 1 hr       | Cooking staples           |
| Apple          | Farming   | Standard (2)    | 10 min       | 8 hr       | Cooking, trade            |
| River Trout    | Fishing   | Standard (2)    | 20 min       | 3 hr       | Cooking                   |
| Deep Sea Pearl | Fishing   | Exceptional (4) | 4 hr         | 72 hr      | Enchanting, jewelry       |
| Deer Hide      | Hunting   | Standard (2)    | 45 min       | 6 hr       | Leatherwork               |
| Blackfang Pelt | Hunting   | Fine (3)        | 2 hr         | 24 hr      | Masterwork leather        |
| Wild Berries   | Foraging  | Poor (1)        | 5 min        | 30 min     | Cooking, alchemy reagent  |
| Starbloom Root | Foraging  | Fine (3)        | 45 min       | 8 hr       | Advanced alchemy          |

---

## 2. Gathering Queue Mechanics

### 2.1 Queue Flow

```
SELECT NODE (from discovered nodes)
    |
    v
ASSIGN GATHERER (must meet skill requirement)
    |
    v
EQUIP TOOL (matching category, quality affects timer)
    |
    v
GATHERING SLOT OCCUPIED -> TIMER STARTS
    |
    v
TIMER RUNS (real-time, continues offline)
    |
    +-- EVENT CHECK (per cycle, based on Threat Level)
    |     +-- No event -> normal yield
    |     +-- Event triggered -> resolve (see Section 8)
    |
    v
CYCLE COMPLETE -> YIELD CALCULATED
    |
    v
REPEAT (auto-cycles until supply depleted or slot freed)
    |
    v
COLLECT RESULTS (at check-in)
```

### 2.2 Gathering Slots

Slots determine how many concurrent gathering assignments a player can run:

| Unlock Source      | Slots Granted | Condition                |
| ------------------ | ------------- | ------------------------ |
| Base               | 2             | Starting allocation      |
| Gathering skill    | +1 per rank   | Apprentice, Journey, etc |
| Settlement upgrade | +1            | Gathering lodge built    |
| Prestige (future)  | +1            | Per prestige cycle       |
| Maximum            | 8             | Hard cap                 |

### 2.3 Auto-Cycle Behavior

Assignments automatically repeat gathering cycles until:

- Node supply is depleted
- Gatherer inventory is full (50 items per slot default)
- Player manually cancels the assignment
- A severe threat event forces withdrawal

Results accumulate and are presented at the next check-in.

### 2.4 Timer Modifiers

Base gather timer is modified by skill and equipment:

```
Effective Timer = Base Timer x Skill Modifier x Tool Modifier x Season Modifier

Skill Modifier (by rank):
  None       = 1.25 (25% slower)
  Apprentice = 1.00 (baseline)
  Journeyman = 0.90 (10% faster)
  Master     = 0.75 (25% faster)

Tool Modifier (by quality tier, per quality-tiers.md Section 10):
  Poor       = 1.30 (30% slower)
  Standard   = 1.00 (baseline)
  Superior   = 0.90 (10% faster)
  Masterwork = 0.80 (20% faster)
  Legendary  = 0.70 (30% faster)

Season Modifier (see Section 9):
  Peak season   = 0.85 (15% faster)
  Normal season = 1.00 (baseline)
  Off season    = 1.20 (20% slower)
  Harsh season  = 1.40 (40% slower)
```

**Example:**

```
Iron Ore (Mining):
  Base Timer    = 30 min
  Skill         = Journeyman (0.90)
  Tool          = Masterwork Pickaxe (0.80)
  Season        = Normal (1.00)
  Effective     = 30 x 0.90 x 0.80 x 1.00 = 21.6 min per cycle
```

---

## 3. Resource Quality

### 3.1 Quality Tier Integration

Gathered resources use the 1-5 quality scale defined in quality-tiers.md:

| Numeric | Material Quality | Crafting Ceiling | Value Multiplier |
| ------- | ---------------- | ---------------- | ---------------- |
| 1       | Poor             | Standard items   | 0.5x             |
| 2       | Standard         | Superior items   | 1x               |
| 3       | Fine             | Masterwork items | 2x               |
| 4       | Exceptional      | Legendary items  | 5x               |
| 5       | Perfect          | Legendary+ items | 10x+             |

### 3.2 Output Quality Determination

The quality of gathered resources depends on node base quality and gatherer skill:

```
Output Quality = Node Base Quality + Quality Modifier

Quality Modifier (by gatherer rank):
  None       = -1 (capped at Node Quality - 1)
  Apprentice =  0 (matches node quality)
  Journeyman = 20% chance of +1 tier
  Master     = 40% chance of +1 tier, 5% chance of +2

Constraints:
  Minimum output quality = 1 (Poor)
  Maximum output quality = 5 (Perfect)
  Tool quality does NOT affect output quality (only timer and yield)
```

### 3.3 Quality and Crafting Integration

Gathered material quality directly constrains crafting output (see crafting-system-gdd.md Section 5):

- Poor (1) materials produce at most Standard (2) crafted items
- Fine (3) materials enable Masterwork (4) crafted items
- Perfect (5) materials are required for the best Legendary results
- Material refinement (crafting-system-gdd.md Section 5.1) upgrades quality at a 3:1 ratio

---

## 4. Gathering Skill Progression

### 4.1 Skill Ranks

Each of the seven gathering categories has its own skill track that advances through use:

| Rank       | XP Required | Quality Modifier | Yield Modifier | Timer Modifier | Node Access |
| ---------- | ----------- | ---------------- | -------------- | -------------- | ----------- |
| None       | 0           | -1 tier          | 0.75x          | +25% slower    | Common only |
| Apprentice | 300         | 0 (baseline)     | 1.00x          | Baseline       | + Uncommon  |
| Journeyman | 1,500       | +1 tier chance   | 1.25x          | -10% faster    | + Rare      |
| Master     | 6,000       | +2 tier chance   | 1.50x          | -25% faster    | + Legendary |

### 4.2 XP Gain

Gathering XP accumulates from completed gather cycles:

```
XP PER CYCLE:
  Base XP      = 10 x Node Tier Multiplier
  Tier Multiplier:
    Common    = 1x  (10 XP)
    Uncommon  = 2x  (20 XP)
    Rare      = 4x  (40 XP)
    Legendary = 8x  (80 XP)

  Bonus XP:
    Quality bonus drop   = +50% XP for that cycle
    Rare resource find   = +100% XP for that cycle
    Threat event survived = +25% XP for that cycle
```

### 4.3 Learning Paths

Two paths to rank advancement, matching the crafting system's pattern (see crafting-system-gdd.md Section 1.2):

```
APPRENTICESHIP PATH:
  - Assign character to NPC master gatherer
  - Timer: 1.5 hours real-time to reach Apprentice
  - Produces Standard quality during training
  - Passive XP gain while assigned (offline-safe)
  - Cost: Training fee (Gold)

SELF-TAUGHT PATH:
  - Learn by gathering (XP from each completed cycle)
  - Timer: ~3 hours equivalent gathering time to Apprentice
  - No fee, but slower and tool-wear intensive
  - Bonus: +5% yield modifier at each rank
```

### 4.4 Specialized Abilities

Each gathering skill unlocks passive abilities at rank milestones:

| Skill     | Apprentice Ability | Journeyman Ability   | Master Ability       |
| --------- | ------------------ | -------------------- | -------------------- |
| Mining    | Keen Eye (+yield)  | Ore Sense (rare ore) | Efficient Extraction |
| Logging   | Clean Cut (+yield) | Heartwood Sense      | Master Lumberjack    |
| Herbalism | Root Knowledge     | Season Sense         | Alchemist's Bounty   |
| Farming   | Careful Pick       | Orchard Blessing     | Bountiful Harvest    |
| Fishing   | Patient Angler     | Deep Cast (+range)   | Legendary Catch      |
| Hunting   | Track Prey         | Silent Approach      | Trophy Hunter        |
| Foraging  | Gentle Harvest     | Rare Find (+quality) | Abundant Growth      |

**Ability effects:**

- **+yield abilities**: +1 bonus resource per cycle
- **+quality abilities**: Additional 10% chance for +1 quality tier
- **Rare resource abilities**: Unlock rare drops from nodes (e.g., gems from mining, rare herbs from foraging)
- **Efficiency abilities**: -15% additional timer reduction

---

## 5. Node Discovery and Depletion

### 5.1 Discovery Mechanics

Resource nodes must be discovered before they can be assigned:

- **Region exploration**: Entering a new region reveals Common tier nodes automatically
- **Skill-based**: Higher gathering ranks reveal hidden nodes in explored regions
- **Quest rewards**: Some nodes unlocked through quest completion
- **NPC tips**: Gathering guild NPCs hint at node locations (costs Gold)

| Node Tier | Discovery Method                        |
| --------- | --------------------------------------- |
| Common    | Automatic on region entry               |
| Uncommon  | Apprentice skill + 1 hour exploration   |
| Rare      | Journeyman skill + quest or NPC tip     |
| Legendary | Master skill + special unlock condition |

### 5.2 Node Properties

Each resource node has properties that govern its behavior:

```
NODE PROPERTIES:
  Resource Type     : What it produces (e.g., Iron Ore, Oak Log)
  Category          : Gathering category (Mining, Logging, etc.)
  Base Quality      : Default quality tier of output (1-5)
  Gather Timer      : Time per harvest cycle
  Supply            : Current available units (depletes on gather)
  Max Supply        : Maximum units before depletion
  Regen Rate        : Time to regenerate 1 unit of supply
  Threat Level      : Chance of event interruption (0-100%)
  Skill Requirement : Minimum skill rank to access
  Ecosystem Link    : Connected nodes affected by this node's state
```

### 5.3 Node Tiers

Nodes are tiered by the region they appear in, controlling progression:

| Node Tier | Region        | Quality Range | Skill Required | Threat Level | Max Supply |
| --------- | ------------- | ------------- | -------------- | ------------ | ---------- |
| Common    | Starting area | Poor-Standard | None           | 0-10%        | 10-20      |
| Uncommon  | Mid regions   | Standard-Fine | Apprentice     | 10-25%       | 5-15       |
| Rare      | Far regions   | Fine-Except.  | Journeyman     | 25-50%       | 3-10       |
| Legendary | End regions   | Except.-Perf. | Master         | 50-75%       | 1-5        |

### 5.4 Depletion and Renewal

Nodes have finite supply that regenerates over time:

```
DEPLETION:
  Each gather cycle consumes 1 supply
  When supply = 0, node is depleted (cannot gather)
  Depleted nodes show "Regenerating" with timer
  Assignments on depleted nodes auto-pause (slot freed)

REGENERATION:
  Supply regenerates 1 unit per Regen Rate interval
  Regeneration continues offline
  Full renewal timer = Max Supply x Regen Rate

  Example (Iron Ore vein):
    Max Supply = 15 units
    Regen Rate = 4 hours per unit
    Full renewal = 15 x 4 = 60 hours (2.5 days)

OVER-HARVESTING PENALTY (see Section 6):
  Repeatedly depleting a node to 0 supply triggers degradation
  Degraded nodes have reduced Max Supply and slower Regen Rate
  Recovery from degradation requires leaving node untouched for a full renewal cycle
```

---

## 6. Ecosystem Dynamics

### 6.1 Ecosystem Balance

Resource nodes exist within interconnected ecosystems. Over-harvesting one resource type creates ripple effects across connected nodes:

```
ECOSYSTEM CHAIN:
  Vegetation (Herbalism, Foraging, Farming)
      |
      v supports
  Prey Animals (Hunting: deer, rabbits)
      |
      v feeds
  Predator Population (Threat system: wolves, bears)
      |
      v controls
  Prey Population (cycle restarts)

  Separately:
  Trees (Logging)
      |
      v shelters
  Wildlife + Herbs (Hunting, Herbalism, Foraging)
```

### 6.2 Over-Harvesting Consequences

Depleting nodes repeatedly without allowing full regeneration triggers ecosystem penalties:

| Over-Harvest Action      | Consequence                           | Recovery Time      |
| ------------------------ | ------------------------------------- | ------------------ |
| Deplete node 3x in a row | Max Supply reduced by 25%             | 1 full renewal     |
| Deplete node 5x in a row | Max Supply reduced by 50%, regen -25% | 2 full renewals    |
| Clear-cut logging node   | Herbalism/Foraging yields -30% nearby | 3 full renewals    |
| Over-hunt prey in region | Predator threats increase +20%        | Prey pop. recovers |
| Over-fish a lake         | Fish quality drops -1 tier            | 2 full renewals    |
| Strip-mine a vein        | Gem/rare drops disabled temporarily   | 1 full renewal     |

### 6.3 Ecosystem Health Indicator

Each region has an ecosystem health score (0-100) calculated from node states:

```
ECOSYSTEM HEALTH:
  100 = Pristine (all nodes at 75%+ supply)
  75  = Healthy (most nodes above 50% supply)
  50  = Stressed (multiple nodes below 25% supply)
  25  = Degraded (frequent depletion, reduced yields)
  0   = Collapsed (nodes stop regenerating until recovery)

Health Effects:
  Pristine/Healthy: +10% rare drop chance, normal regen
  Stressed:         Normal drops, regen slowed by 20%
  Degraded:         -25% all yields, regen slowed by 50%
  Collapsed:        No gathering possible, recovery timer starts (24-72 hours)
```

### 6.4 Sustainable Gathering Bonus

Players who maintain ecosystem health receive ongoing bonuses:

| Health Level   | Duration Maintained | Bonus                          |
| -------------- | ------------------- | ------------------------------ |
| Healthy (75+)  | 24 hours            | +5% yield all nodes in region  |
| Pristine (90+) | 48 hours            | +10% yield, +15% rare drops    |
| Pristine (90+) | 7 days              | Unique "Flourishing" resources |

"Flourishing" resources are quality 4-5 variants that only appear in well-maintained ecosystems, creating long-term incentive for sustainable gathering rather than maximum short-term extraction.

### 6.5 Population Effects (Hunting and Fishing)

Animal and fish populations follow simplified predator/prey dynamics:

| Event                    | Consequence               | Duration        |
| ------------------------ | ------------------------- | --------------- |
| Predators over-hunted    | Prey population grows     | Until rebalance |
| Prey over-hunted         | Predator threats increase | Until recovery  |
| Prey overpopulated       | Vegetation yields drop    | Until balanced  |
| Vegetation depleted      | Prey population drops     | Until regrowth  |
| All populations balanced | Normal yields, low threat | Ongoing         |

These dynamics are calculated as simple multipliers at check-in, not as continuous simulation. Population states shift in discrete steps (Low / Normal / High / Overpopulated) based on cumulative player actions since last check-in.

---

## 7. Tool Quality Impact

### 7.1 Gathering Tools

Each gathering category has a matching tool type. Tool quality follows the quality-tiers.md 1-5 scale:

| Category  | Tool          | Crafted By          | Key Material     |
| --------- | ------------- | ------------------- | ---------------- |
| Mining    | Pickaxe       | Blacksmithing       | Iron/Steel Ingot |
| Logging   | Axe           | Blacksmithing       | Iron/Steel Ingot |
| Herbalism | Sickle        | Blacksmithing       | Iron/Steel Ingot |
| Farming   | Hoe / Basket  | Blacksmith/Woodwork | Mixed            |
| Fishing   | Fishing Rod   | Woodworking         | Wood, String     |
| Hunting   | Hunting Bow   | Woodworking         | Wood, Sinew      |
| Foraging  | Gathering Bag | Tailoring           | Fabric, Leather  |

### 7.2 Tool Quality Effects

Tool quality affects three dimensions of gathering performance:

| Tool Quality   | Timer Modifier | Yield Modifier | Durability (cycles) |
| -------------- | -------------- | -------------- | ------------------- |
| Poor (1)       | +30% slower    | 0.90x          | 20 cycles           |
| Standard (2)   | Baseline       | 1.00x          | 50 cycles           |
| Superior (3)   | -10% faster    | 1.05x          | 80 cycles           |
| Masterwork (4) | -20% faster    | 1.10x          | 120 cycles          |
| Legendary (5)  | -30% faster    | 1.15x          | 200 cycles          |

### 7.3 Tool Durability

Tools wear down with use and must be repaired through the crafting system (see crafting-system-gdd.md Section 9.1):

```
DURABILITY:
  Each gather cycle consumes 1 durability
  At 0 durability: tool breaks, reverts to "No Tool" penalties
  Broken tool: +50% timer, -25% yield (equivalent to worse than Poor)

REPAIR:
  Via crafting queue (crafting-system-gdd.md Section 9.1)
  Timer: 25% of original craft time
  Materials: 1x matching material (any quality)
  Maintains original tool quality

OFFLINE TOOL MANAGEMENT:
  Tools break mid-assignment during offline gathering
  System auto-switches to "bare hands" for remaining cycles
  Check-in summary flags broken tools for repair
```

### 7.4 Combined Yield Formula

```
Yield Per Cycle = Base Yield x Skill Modifier x Tool Modifier x Season Modifier

Base Yield:
  Most resources = 1-2 units per cycle
  Foraging/Farming = 2-4 units per cycle (lower value, higher volume)

Example:
  Mining Iron Ore:
    Base Yield    = 2 ore
    Skill         = Master (1.50x)
    Tool          = Masterwork Pickaxe (1.10x)
    Season        = Normal (1.00x)
    Total         = 2 x 1.50 x 1.10 x 1.00 = 3.3 -> 3 ore (floor)
    Remainder     = 0.3 carries forward as fractional accumulator
```

---

## 8. Threat Events

### 8.1 Event System

Gathering in dangerous areas can trigger random events that interrupt the assignment:

```
EVENT CHECK:
  Per gather cycle, roll against node Threat Level
  If triggered, resolve event before yield calculation

  Event Impact:
    Minor  (60% of events) -> Reduced yield this cycle (-50%)
    Major  (30% of events) -> No yield, 1 cycle timer penalty
    Severe (10% of events) -> Assignment cancelled, partial inventory lost
```

### 8.2 Event Types

| Event Type     | Category Affected  | Severity | Description                    |
| -------------- | ------------------ | -------- | ------------------------------ |
| Wolf attack    | Hunting, Foraging  | Minor    | Scares away game, spoils herbs |
| Bear encounter | Logging, Mining    | Major    | Forces retreat from area       |
| Boar charge    | Farming, Herbalism | Minor    | Tramples some harvest          |
| Bandit ambush  | All                | Severe   | Steals gathered resources      |
| Landslide      | Mining             | Major    | Blocks access to vein          |
| Storm          | Fishing, Logging   | Minor    | Reduces catch/yield            |
| Blight         | Farming, Herbalism | Major    | Destroys node supply           |
| Drought        | Fishing, Farming   | Major    | Reduces node supply by 50%     |

### 8.3 Threat Mitigation

Players can reduce threat impact through investment:

| Mitigation        | Effect                       | Cost            |
| ----------------- | ---------------------------- | --------------- |
| Guard assignment  | -25% threat level            | Guard salary/hr |
| Trap placement    | -15% animal threats          | 50 Gold + craft |
| Watchtower (node) | -20% bandit threats          | 200 Gold        |
| Gathering skill   | -5% per rank above None      | XP investment   |
| Equipment quality | -2% per quality tier above 2 | Better gear     |

---

## 9. Seasonal and Environmental Modifiers

### 9.1 Seasonal Cycle

The game world operates on a four-season cycle. Each season lasts 7 real-time days (28-day full year):

| Season | Duration   | General Effect              |
| ------ | ---------- | --------------------------- |
| Spring | Days 1-7   | Growth, planting, migration |
| Summer | Days 8-14  | Peak yields, long days      |
| Autumn | Days 15-21 | Harvest, preparation        |
| Winter | Days 22-28 | Scarcity, harsh conditions  |

### 9.2 Seasonal Impact by Category

| Category  | Peak Season | Off Season | Peak Bonus         | Off Penalty         |
| --------- | ----------- | ---------- | ------------------ | ------------------- |
| Mining    | None        | None       | No seasonal effect | No seasonal effect  |
| Logging   | Autumn      | Spring     | -15% timer         | +20% timer          |
| Herbalism | Spring      | Winter     | +25% yield         | -40% yield, -1 qual |
| Farming   | Summer      | Winter     | +30% yield         | No growth (paused)  |
| Fishing   | Spring      | Winter     | +20% yield         | -30% yield          |
| Hunting   | Autumn      | Summer     | +15% yield         | -20% yield          |
| Foraging  | Summer      | Winter     | +25% yield         | -50% yield          |

Mining is unaffected by seasons as mineral veins exist underground regardless of surface conditions.

### 9.3 Environmental Modifiers

Beyond seasons, specific environmental conditions apply modifiers:

| Condition        | Affected Categories | Effect                            | Duration    |
| ---------------- | ------------------- | --------------------------------- | ----------- |
| Rainfall         | Herbalism, Farming  | +10% yield, +15% regen rate       | 1-3 days    |
| Drought          | Fishing, Farming    | -20% yield, regen halved          | 3-7 days    |
| Frost            | Herbalism, Foraging | -30% yield, quality -1 tier       | 1-5 days    |
| Full Moon        | Herbalism           | +20% rare drop chance             | 1 day       |
| Thunderstorm     | Fishing, Mining     | +40% threat level                 | 12-24 hours |
| Bloom Event      | Foraging, Herbalism | +50% yield, unique resources      | 2-4 days    |
| Animal Migration | Hunting, Fishing    | +30% yield, new species available | 3-5 days    |

Environmental conditions are rolled at each check-in based on season probabilities. They stack with seasonal modifiers multiplicatively.

### 9.4 Weather Probability by Season

| Condition        | Spring | Summer | Autumn | Winter |
| ---------------- | ------ | ------ | ------ | ------ |
| Rainfall         | 30%    | 15%    | 20%    | 10%    |
| Drought          | 5%     | 20%    | 10%    | 0%     |
| Frost            | 10%    | 0%     | 15%    | 40%    |
| Full Moon        | 8%     | 8%     | 8%     | 8%     |
| Thunderstorm     | 15%    | 25%    | 10%    | 5%     |
| Bloom Event      | 20%    | 10%    | 5%     | 0%     |
| Animal Migration | 25%    | 5%     | 25%    | 10%    |

---

## 10. Offline Accumulation and Batch Processing

### 10.1 Offline Behavior

| System                | Offline Behavior                                   |
| --------------------- | -------------------------------------------------- |
| Gathering assignments | Cycles continue, yields accumulate                 |
| Node supply           | Depletes as gathered, regenerates naturally        |
| Threat events         | Rolled per cycle, results applied to yield         |
| Skill XP              | Accumulates from completed cycles                  |
| Tool durability       | Decrements per cycle (repair at crafting workshop) |
| Ecosystem health      | Adjusts based on cumulative gathering activity     |
| Seasonal transitions  | Apply at next check-in if season boundary crossed  |

### 10.2 Batch Calculation

Offline time is processed as batch cycles rather than continuous simulation:

```
BATCH PROCESSING:
  Offline Duration = time since last check-in
  Cycles Completed = floor(Offline Duration / Effective Timer)

  For each cycle:
    1. Check supply (skip remaining cycles if depleted)
    2. Roll threat event (based on Threat Level)
    3. Calculate yield (base x skill x tool x season modifiers)
    4. Determine quality (base + skill modifier roll)
    5. Accumulate XP
    6. Decrement supply
    7. Decrement tool durability

  Cap: Maximum 200 cycles per assignment per offline session
  (prevents runaway accumulation on very long offline periods)
```

### 10.3 Check-in Summary

On player check-in, the system presents:

- Total resources gathered per assignment (by type and quality)
- Threat events that occurred (with outcome summary)
- Node supply status (remaining, depleted, regenerating)
- Skill XP gained and any rank-ups
- Tool durability status and any broken tools
- Ecosystem health changes
- Seasonal transitions and current modifiers
- Assignments that stopped (depletion, inventory full, severe event, broken tool)

### 10.4 Long Offline Caps

To maintain game balance during extended offline periods:

```
OFFLINE CAPS:
  Max cycles per assignment     = 200
  Max offline duration credited = 24 hours
  Excess time beyond cap        = ignored (no penalty, no bonus)

  Rationale: Prevents players who check in once per week from
  accumulating vastly more than daily players. 24-hour cap means
  checking in once per day captures maximum value.
```

---

## 11. Progression Hooks

### 11.1 System Integration

| Hook              | System Integration                                                                         |
| ----------------- | ------------------------------------------------------------------------------------------ |
| Quality tiers     | Output quality follows quality-tiers.md 1-5 scale                                          |
| Crafting system   | Gathered materials feed crafting recipe inputs (crafting-system-gdd.md Section 5)          |
| Material refining | Raw materials refined via crafting queue at 3:1 ratio (crafting-system-gdd.md Section 5.1) |
| Economy system    | Raw materials sold directly or via trade routes                                            |
| Settlement        | Gathering lodge upgrades grant bonus slots                                                 |
| Combat system     | Hunting skill overlaps with combat tracking                                                |
| Prestige (future) | Gathering mastery counts toward prestige                                                   |

### 11.2 Material Flow

```
GATHERING -> Raw Materials (quality-tiered)
    |
    +-- Direct sale (economy system, lower value)
    +-- Crafting input (crafting-system-gdd.md, higher value as finished goods)
    +-- Refinement (crafting system, 3:1 quality upgrade ratio)
    +-- Settlement construction (building materials)
```

### 11.3 Crafting System Dependencies

The gathering and crafting systems form a tight production loop:

- **Gathering produces materials** consumed by crafting recipes
- **Crafting produces tools** that improve gathering efficiency
- **Material quality** from gathering constrains crafting output quality
- **Tool quality** from crafting reduces gathering timers and increases yield
- **Both systems** share the quality-tiers.md framework for consistent value scaling

This creates a virtuous investment cycle: better tools produce better materials, which enable better crafted items, including better tools.
