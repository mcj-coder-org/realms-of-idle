---
type: system
scope: detailed
status: authoritative
version: 1.0.0
created: 2026-02-08
updated: 2026-02-08
subjects: [settlements, buildings, population, territory, defense, resources, idle]
dependencies: [economy-system-gdd.md, gathering-system-gdd.md]
---

# Settlement System - Authoritative Game Design

## Executive Summary

The Settlement System governs how players found, grow, and manage settlements through timer-based construction queues, strategic policy selections, and modifier-driven population mechanics. Settlements progress from camps to cities as players invest resources and make strategic choices about building priorities, specializations, and defense. All execution is automated -- players select actions, start timers, and collect results at check-in.

**This document resolves:**

- Settlement progression tiers (Camp through City) with upgrade requirements
- Timer-based building construction and upgrade queues
- Population growth as modifier-driven batch calculation
- Resource production buildings and offline output accumulation
- Defense structures and territory protection ratings
- Settlement-wide bonuses and specialization systems
- Service buildings and their passive modifier effects
- Settlement upkeep costs as resource sinks
- Offline settlement progression and check-in collection

**Design Philosophy:** Settlements are idle engines that convert strategic decisions into long-term growth. Players choose what to build, which policies to enact, and where to specialize. The settlement then produces, grows, and defends itself over real time. Complexity scales with settlement tier -- camps need minimal attention while cities offer deep strategic layers. Offline progress ensures settlements always advance.

---

## 1. Settlement Tiers

### 1.1 Tier Progression

Settlements progress through four tiers based on population and building requirements:

```
SETTLEMENT TIERS:

Tier 1 -- Camp (2-15 population)
  Building Slots: 4
  Construction Queue: 1 slot
  Governance: None
  Specialization: None
  Unlock: Default starting tier

Tier 2 -- Village (16-100 population)
  Building Slots: 10
  Construction Queue: 1 slot (+1 with Town Hall upgrade)
  Governance: Elder (1 policy slot)
  Specialization: 1 slot
  Unlock: Reach 16 population + build Town Hall (Tier 1)

Tier 3 -- Town (101-500 population)
  Building Slots: 20
  Construction Queue: 2 slots (+1 with Town Hall upgrade)
  Governance: Mayor + Council (3 policy slots)
  Specialization: 2 slots
  Unlock: Reach 101 population + build Town Hall (Tier 2)

Tier 4 -- City (501+ population)
  Building Slots: 35
  Construction Queue: 3 slots
  Governance: Lord + Council (5 policy slots)
  Specialization: 3 slots
  Unlock: Reach 501 population + build Town Hall (Tier 3) + Courthouse
```

### 1.2 Tier Upgrade and Downgrade

Tier changes are evaluated daily through batch processing. Both population threshold and building prerequisites must be met for an upgrade. Downgrades occur only after sustained population loss.

```
TIER UPGRADE CHECK:
  Frequency: Daily (batch processed at check-in or 00:00 UTC)
  Conditions:
    population >= tier_threshold
    AND required_buildings_exist
  Result: Tier upgrade notification, new building slots unlock

TIER DOWNGRADE CHECK:
  Frequency: Daily
  Conditions:
    population < current_tier_threshold
    AND consecutive_days_below >= 7
  Result: Tier downgrade, excess building slots locked (buildings preserved)
```

---

## 2. Building System

### 2.1 Building Categories

Buildings are organized into six functional categories. Each building has upgrade tiers that improve its output or capacity.

| Category       | Purpose                        | Examples                                    |
| -------------- | ------------------------------ | ------------------------------------------- |
| Residential    | Houses population              | Tent, Cottage, House, Manor                 |
| Production     | Produces resources and goods   | Farm, Lumber Mill, Smithy, Tannery          |
| Commercial     | Generates gold, enables trade  | Market Stall, Shop, Warehouse, Trading Post |
| Infrastructure | Provides utilities and defense | Well, Palisade, Watchtower, Granary         |
| Service        | Community and specialist needs | Tavern, Temple, Academy, Hospital           |
| Guild          | Class-based training and perks | Fighter Guild, Mage Tower, Craft Hall       |

### 2.2 Building Upgrade Tiers

Each building has 5 upgrade tiers. Higher tiers provide better bonuses but cost more resources and time.

```
BUILDING TIER PROGRESSION:

Tier 1 -- Basic
  Cost Multiplier: 1.0x
  Timer: 1-5 minutes
  Output: Base value

Tier 2 -- Improved
  Cost Multiplier: 3.0x
  Timer: 15-60 minutes
  Output: 2.0x base

Tier 3 -- Advanced
  Cost Multiplier: 8.0x
  Timer: 1-4 hours
  Output: 3.5x base

Tier 4 -- Superior
  Cost Multiplier: 20.0x
  Timer: 8-24 hours
  Output: 5.5x base

Tier 5 -- Masterwork
  Cost Multiplier: 50.0x
  Timer: 2-7 days
  Output: 8.0x base
```

### 2.3 Construction Queue

Each settlement has a construction queue for building and upgrading. Queue slots increase with Town Hall upgrades.

```
CONSTRUCTION QUEUE:
  Default Slots: 1
  Max Slots: 3 (unlocked via Town Hall upgrades at Village and Town tier)
  Queue Behavior: FIFO processing per slot, slots run in parallel
  Timer continues offline

  Timer Modifiers:
    Builder NPC assigned:     -15% construction time
    Master Builder assigned:  -30% construction time
    Construction Policy:      -10% construction time
    Missing materials:        Queue paused until supplied
    Cultural trait bonus:     -15% if Industrious trait

  Effective Timer = Base Timer x Builder Modifier x Policy Modifier x Trait Modifier
```

### 2.4 Representative Buildings

#### Residential Buildings

| Building | Tier | Pop. Capacity | Timer  | Materials                      |
| -------- | ---- | ------------- | ------ | ------------------------------ |
| Tent     | 1    | +2            | 1 min  | 5 Cloth, 3 Wood                |
| Cottage  | 2    | +6            | 20 min | 30 Wood, 20 Stone, 5 Iron      |
| House    | 3    | +12           | 2 hrs  | 50 Wood, 40 Stone, 15 Iron     |
| Manor    | 4    | +25           | 12 hrs | 100 Stone, 50 Iron, 20 Lumber  |
| Estate   | 5    | +40           | 3 days | 200 Stone, 100 Iron, 50 Marble |

#### Production Buildings

| Building    | Tier | Output        | Rate/hr | Timer  | Materials                  |
| ----------- | ---- | ------------- | ------- | ------ | -------------------------- |
| Farm        | 1    | Food          | 10      | 5 min  | 20 Wood, 10 Stone          |
| Lumber Mill | 1    | Lumber        | 8       | 5 min  | 25 Wood, 15 Stone          |
| Smithy      | 2    | Iron Goods    | 5       | 30 min | 40 Stone, 20 Iron          |
| Tannery     | 2    | Leather Goods | 6       | 30 min | 30 Wood, 15 Iron           |
| Brewery     | 3    | Ale (+morale) | 3       | 2 hrs  | 50 Wood, 30 Stone, 10 Iron |

#### Commercial Buildings

| Building     | Tier | Function       | Passive Income | Timer  |
| ------------ | ---- | -------------- | -------------- | ------ |
| Market Stall | 1    | Basic trade    | 5 Gold/hr      | 3 min  |
| Shop         | 2    | Retail goods   | 15 Gold/hr     | 25 min |
| Warehouse    | 2    | Storage (+200) | --             | 30 min |
| Trading Post | 3    | Route trade    | 40 Gold/hr     | 2 hrs  |

#### Infrastructure Buildings

| Building   | Tier | Function          | Effect                    | Timer  |
| ---------- | ---- | ----------------- | ------------------------- | ------ |
| Well       | 1    | Water supply      | Supports 50 population    | 3 min  |
| Granary    | 1    | Food storage      | +100 food storage         | 5 min  |
| Palisade   | 1    | Basic defense     | +10 defense rating        | 10 min |
| Stone Wall | 2    | Strong defense    | +25 defense rating        | 45 min |
| Watchtower | 2    | Detection + range | +5 defense, early warning | 30 min |

---

## 3. Population Management

### 3.1 Growth Rate Formula

Population grows passively based on settlement conditions. Growth is calculated hourly as a batch operation at check-in.

```
POPULATION GROWTH FORMULA:

  Base Growth Rate = 0.5% per hour (of current population, minimum +1)

  Final Growth = Base x Housing Modifier x Food Modifier x Satisfaction Modifier
                 x Specialization Bonus x Policy Modifier

  Housing Modifier:
    Available housing > 120% of population: 1.5x
    Available housing > 100% of population: 1.0x
    Available housing = population:         0.5x
    Available housing < population:         0.0x (no growth)

  Food Modifier:
    Food surplus > 50%:  1.3x
    Food surplus > 0%:   1.0x
    Food deficit:        0.5x (decline begins)
    Severe deficit:      0.0x + population loss

  Satisfaction Modifier:
    High (80-100):   1.2x
    Normal (40-79):  1.0x
    Low (20-39):     0.7x
    Critical (<20):  0.3x
```

### 3.2 NPC Satisfaction

NPC satisfaction replaces deep personality simulation with simple multipliers. Satisfaction is a single 0-100 value influenced by settlement conditions.

```
SATISFACTION CALCULATION (recalculated hourly):

  Base Satisfaction = 50

  Modifiers (additive):
    Adequate housing:       +10
    Food surplus:           +10
    Tavern present:         +5
    Temple present:         +5
    Hospital present:       +5
    Low crime/high defense: +5
    Festival active:        +10 (temporary)
    No housing:             -20
    Food deficit:           -20
    Recent raid damage:     -15
    No water supply:        -15
    Overcrowding:           -10
    High upkeep unpaid:     -10

  Final Satisfaction = clamp(Base + sum(Modifiers), 0, 100)
```

### 3.3 Population Decline

Population declines when conditions deteriorate. Decline is calculated faster than growth.

```
DECLINE TRIGGERS:
  No food:             -1% population/hour
  No housing:          -0.5% population/hour (emigration)
  Critical satisfaction: -0.3% population/hour
  Active plague event: -2% population/day
  Active siege:        -1% population/day
  Stacking:            Multiple triggers stack additively
```

### 3.4 Population Capacity

Total population capacity is the sum of all residential building capacities. Population cannot exceed capacity.

```
CAPACITY FORMULA:
  Max Population = sum(Residential Building Capacities)

  Growth Cap: Population growth stops at capacity
  Overcrowding: If buildings destroyed/downgraded and pop > capacity,
                emigration begins (-0.5% per hour until pop <= capacity)
```

---

## 4. Resource Production Buildings

### 4.1 Production Mechanics

Production buildings generate resources passively over time. Output accumulates while offline and is collected at check-in.

```
PRODUCTION FLOW:

  Building constructed/upgraded
      |
      v
  Production starts automatically (rate based on building tier)
      |
      v
  Output accumulates in settlement storage
      |
      v
  Player collects at check-in

  Effective Rate = Base Rate x Building Tier Multiplier x Worker Modifier
                   x Specialization Bonus x Policy Modifier

  Worker Modifier:
    Fully staffed:    1.0x
    Understaffed:     0.5x (population < building requirement)
    Overstaffed:      1.0x (no bonus, excess workers reassigned)

  Staffing Requirement = Building Tier (Tier 1 = 1 worker, Tier 5 = 5 workers)
```

### 4.2 Storage Limits

Produced resources accumulate in settlement storage. Excess production beyond storage capacity is lost.

```
STORAGE:
  Base Storage: 200 per resource type
  Granary:      +100 food storage per tier
  Warehouse:    +200 general storage per tier

  Overflow: Production stops for that resource until storage freed
  Offline Cap: Maximum 24 hours of accumulated production stored
               (prevents infinite offline stockpiling)
```

### 4.3 Resource Production Summary

| Building    | Resource   | Base Rate | Tier 3 Rate | Tier 5 Rate | Workers |
| ----------- | ---------- | --------- | ----------- | ----------- | ------- |
| Farm        | Food       | 10/hr     | 35/hr       | 80/hr       | 1-5     |
| Lumber Mill | Lumber     | 8/hr      | 28/hr       | 64/hr       | 1-5     |
| Smithy      | Iron Goods | 5/hr      | 17/hr       | 40/hr       | 1-5     |
| Tannery     | Leather    | 6/hr      | 21/hr       | 48/hr       | 1-5     |
| Brewery     | Ale        | 3/hr      | 10/hr       | 24/hr       | 1-5     |

Production rates scale with building tier multiplier (see Section 2.2).

---

## 5. Defense Structures and Territory Protection

### 5.1 Defense Rating

Each settlement has a passive defense rating that reduces the severity of negative events (raids, monster attacks, bandit incursions). Defense is not active combat -- it modifies event outcomes through batch calculation.

```
DEFENSE RATING FORMULA:

  Defense Rating = Base + Wall Bonus + Guard Bonus + Building Bonus + Policy Bonus

  Base Defense by Tier:
    Camp:     5
    Village:  15
    Town:     35
    City:     60

  Wall Bonus:
    Palisade:      +10
    Stone Wall:    +25
    Fortified Wall: +50
    Castle Wall:   +80

  Guard Bonus:
    Per guard NPC:       +1
    Per trained guard:   +2
    Guard captain:       +5

  Building Bonus:
    Watchtower:   +5 (also provides early warning notification)
    Barracks:     +10
    Armory:       +8 (also improves guard equipment quality)
```

### 5.2 Event Mitigation

Defense rating directly reduces damage from negative events. Events are resolved at check-in if they occurred during offline time.

```
EVENT DAMAGE REDUCTION:
  Reduction % = min(Defense Rating / (Event Severity x 2), 0.80)
  Maximum reduction: 80% (events always have some impact)

  Example:
    Defense Rating: 60
    Raid Severity: 50
    Reduction: 60 / (50 x 2) = 60% damage reduction
```

### 5.3 Threat Events

Negative events occur on random timers and scale with settlement wealth and tier:

| Event          | Base Severity | Frequency                 | Effect                          |
| -------------- | ------------- | ------------------------- | ------------------------------- |
| Bandit Raid    | 30            | Every 3-7 days            | Resource loss, population risk  |
| Monster Attack | 40-80         | Every 5-14 days           | Population/building damage      |
| Plague         | 50            | Every 14-30 days          | Population decline, morale loss |
| Fire           | 40            | Every 7-21 days           | Building damage, resource loss  |
| Famine         | 60            | Triggered by food deficit | Population decline              |

### 5.4 Representative Defense Buildings

| Building       | Defense Value | Cost                           | Timer  |
| -------------- | ------------- | ------------------------------ | ------ |
| Palisade       | +10           | 30 Wood, 10 Iron               | 10 min |
| Stone Wall     | +25           | 60 Stone, 20 Iron              | 45 min |
| Watchtower     | +5            | 40 Wood, 20 Stone              | 30 min |
| Barracks       | +10           | 50 Stone, 30 Iron              | 1 hr   |
| Armory         | +8            | 40 Stone, 40 Iron              | 1 hr   |
| Fortified Wall | +50           | 120 Stone, 60 Iron             | 4 hrs  |
| Castle Wall    | +80           | 200 Stone, 100 Iron, 30 Marble | 12 hrs |

---

## 6. Settlement Specializations

### 6.1 Specialization Types

Each settlement can select specializations based on its tier (1 slot at Village, 2 at Town, 3 at City). Specializations provide passive bonuses and require specific buildings.

| Specialization | Primary Bonus        | Secondary Bonus        | Required Buildings       |
| -------------- | -------------------- | ---------------------- | ------------------------ |
| Agricultural   | +30% food production | +10% population growth | 3+ Farms                 |
| Mining         | +30% ore extraction  | +10% crafting speed    | 2+ Mines                 |
| Trade Hub      | +25% gold from trade | +15% merchant arrival  | Trading Post + Warehouse |
| Military       | +20% defense rating  | +15% training speed    | Barracks + Armory        |
| Religious      | +20% satisfaction    | +10% healing rate      | Temple (Tier 2+)         |
| Academic       | +20% research speed  | +15% skill discovery   | Academy + Library        |

### 6.2 Specialization Selection and Change

```
SPECIALIZATION RULES:
  Selection: Player chooses from available specializations
  Prerequisites: Required buildings must exist before selection
  Change Cost: 50% of required building costs (resource sink)
  Change Cooldown: 7 days between specialization changes
  Loss Condition: If required buildings are destroyed, bonus suspends
                  until buildings restored (no automatic removal)
```

---

## 7. Service Buildings

### 7.1 Service Types

Service buildings provide passive bonuses to settlement satisfaction, population health, and character progression. They do not produce resources directly but improve settlement multipliers.

```
SERVICE BUILDINGS:

Tavern (unlocks at Camp):
  Tier 1: +5 satisfaction, social hub
  Tier 2: +10 satisfaction, rumor board (quest discovery)
  Tier 3: +15 satisfaction, +5% gold income (entertainment spending)

Temple (unlocks at Village):
  Tier 1: +5 satisfaction, basic healing (+5% recovery rate)
  Tier 2: +10 satisfaction, blessing (+5% all production for 4 hours)
  Tier 3: +15 satisfaction, sanctuary (+10% event damage reduction)

Hospital (unlocks at Village):
  Tier 1: -10% disease event duration
  Tier 2: -20% disease duration, +10% recovery rate
  Tier 3: -35% disease duration, +20% recovery rate

Academy (unlocks at Town):
  Tier 1: +5% XP gain for settlement NPCs
  Tier 2: +10% XP, unlocks Apprentice-tier training assignments
  Tier 3: +20% XP, unlocks Journeyman-tier training assignments

Guild Hall (unlocks at Town):
  Tier 1: Access to guild quests, +1 party recruitment slot
  Tier 2: Guild shop (specialty items), +5% class XP
  Tier 3: Guild training (+10% class XP), guild bulk orders
```

### 7.2 Service Availability by Tier

| Service  | Camp  | Village | Town     | City        |
| -------- | ----- | ------- | -------- | ----------- |
| Tavern   | Basic | Full    | Multiple | Specialized |
| Temple   | --    | Basic   | Full     | Cathedral   |
| Hospital | --    | Basic   | Full     | Specialized |
| Academy  | --    | --      | Basic    | Full        |
| Guild    | --    | --      | Basic    | Full        |
| Bank     | --    | --      | Basic    | Full        |

---

## 8. Settlement Upkeep and Resource Sinks

### 8.1 Upkeep Formula

Settlements consume resources continuously. Upkeep is deducted hourly from settlement storage during batch processing.

```
UPKEEP CALCULATION (per hour):

  Food Upkeep = Population x 0.5 food/person/hour
  Gold Upkeep = sum(Building Maintenance Costs)
  Material Upkeep = sum(Infrastructure Maintenance)

  Building Maintenance (Gold/hour by tier):
    Tier 1:  1 Gold/hr
    Tier 2:  3 Gold/hr
    Tier 3:  8 Gold/hr
    Tier 4:  20 Gold/hr
    Tier 5:  50 Gold/hr

  Infrastructure Maintenance (Materials/day):
    Palisade:   1 Wood/day
    Stone Wall: 1 Stone/day
    Roads:      1 Stone/week
```

### 8.2 Upkeep Failure

When upkeep cannot be paid, consequences escalate gradually:

```
UPKEEP FAILURE ESCALATION:

  Gold deficit (1-24 hours):
    - Warning notification
    - No immediate effect

  Gold deficit (24-72 hours):
    - Building output reduced by 50%
    - Satisfaction -10

  Gold deficit (72+ hours):
    - Lowest-tier building auto-shutters (stops producing, no upkeep)
    - Satisfaction -20
    - Population emigration begins

  Food deficit:
    - Immediate: Satisfaction -20
    - After 12 hours: Population decline (-1%/hour)
    - After 48 hours: Starvation event (severity 60)
```

### 8.3 Resource Sink Summary

| Sink             | Resource  | Rate                    | Purpose                           |
| ---------------- | --------- | ----------------------- | --------------------------------- |
| Population food  | Food      | 0.5/person/hr           | Prevents infinite population      |
| Building upkeep  | Gold      | 1-50/building/hr        | Balances production income        |
| Wall maintenance | Materials | 1/segment/day           | Rewards active defense investment |
| Construction     | Mixed     | Per building (one-time) | Primary resource consumption      |
| Specialization   | Gold      | Change cost (one-time)  | Prevents constant switching       |

---

## 9. Governance and Policies

### 9.1 Policy Slots

Policy slots unlock with settlement tier. Each policy is a multiplier selection that modifies settlement behavior.

| Settlement Tier | Policy Slots | Decision Timer |
| --------------- | ------------ | -------------- |
| Camp            | 0            | N/A            |
| Village         | 1            | Instant        |
| Town            | 3            | 1 hour         |
| City            | 5            | 4 hours        |

### 9.2 Policy Categories

```
POLICY CATEGORIES:

Economic Policy:
  - Free Trade:      +20% gold income, -10% local production
  - Protectionism:   +15% local production, -10% trade income
  - Balanced:        No modifiers (default)

Growth Policy:
  - Open Immigration:  +25% population growth, -5% satisfaction
  - Closed Borders:    -15% population growth, +10% satisfaction
  - Selective:         +10% population growth, +5% satisfaction (requires Academy)

Military Policy:
  - Aggressive:     +20% training speed, +15% defense, -10% production
  - Defensive:      +25% defense, -5% training speed
  - Pacifist:       +15% production, +10% satisfaction, -20% defense

Construction Policy:
  - Rapid Build:    -20% construction time, +15% material cost
  - Quality Build:  +15% building output, +25% construction time
  - Standard:       No modifiers (default)

POLICY CHANGE:
  Cooldown: 24 hours after changing any policy
  Decision Timer: Based on settlement tier (see table above)
  Timer runs offline, policy activates at completion
```

---

## 10. Cultural Traits

### 10.1 Trait Selection

At settlement founding, the player selects one cultural trait. Traits provide permanent passive bonuses. Traits cannot be changed after founding.

| Trait       | Passive Bonus                    | Flavor                           |
| ----------- | -------------------------------- | -------------------------------- |
| Industrious | +15% production speed            | Founded by hardworking craftsmen |
| Scholarly   | +15% research and XP gain        | Founded near ancient ruins       |
| Militant    | +15% defense and training speed  | Founded by veteran soldiers      |
| Mercantile  | +15% gold income and trade range | Founded at a crossroads          |
| Devout      | +15% satisfaction and healing    | Founded at a sacred site         |
| Resilient   | +15% event damage reduction      | Founded by survivors of hardship |
| Agrarian    | +15% food production and growth  | Founded in fertile lands         |

### 10.2 Cultural Festivals

Each trait unlocks an automatic festival event that provides a temporary bonus:

```
FESTIVALS:

  Industrious  -- Forge Festival:     +25% crafting speed for 24 hours
  Scholarly    -- Scholars' Symposium: +25% XP for 24 hours
  Militant     -- Tournament:          +25% training speed for 24 hours
  Mercantile   -- Trade Fair:          +25% gold income for 24 hours
  Devout       -- Holy Day:            +25% satisfaction for 48 hours
  Resilient    -- Remembrance:         +25% defense for 24 hours
  Agrarian     -- Harvest Festival:    +25% food production for 24 hours

  Trigger: Automatic, every 30 days after founding
  Duration: 24-48 hours
  Stacking: Does not stack with other festivals
  Offline: Festival timer runs offline, bonus applied at check-in
```

---

## 11. Offline Settlement Progression

### 11.1 Offline Behavior

All settlement systems continue running during offline time. Results are batch-calculated at check-in.

| System              | Offline Behavior                                    |
| ------------------- | --------------------------------------------------- |
| Construction queues | Timers continue, completed buildings ready to place |
| Resource production | Output accumulates in storage (capped at 24 hours)  |
| Population growth   | Calculated hourly, applied in batch at check-in     |
| Upkeep costs        | Deducted from storage, deficit tracked              |
| Defense events      | Resolved using defense rating, damage applied       |
| Policy timers       | Decision timers complete, policies activate         |
| Festival timers     | Cycle continues, bonus applied if active            |
| Satisfaction        | Recalculated based on state at check-in             |

### 11.2 Check-in Collection

On player check-in, the system presents:

- Construction completions and available building placements
- Accumulated resource production totals
- Population change summary (growth/decline with reasons)
- Event log with defense outcomes and damage taken
- Upkeep status and any deficit warnings
- Festival notifications and active bonuses
- Satisfaction trend (improved/declined)

### 11.3 Offline Cap

To prevent degenerate strategies, offline accumulation caps at 24 hours for resource production. Construction and population timers have no cap.

```
OFFLINE CAPS:
  Resource production:  24-hour accumulation maximum
  Construction timers:  No cap (completes regardless of offline duration)
  Population growth:    No cap (but bounded by housing/food)
  Upkeep deduction:     No cap (deficit accumulates fully)
  Event frequency:      Maximum 3 events per offline period
```

---

## 12. Settlement Events

### 12.1 Positive Events

Positive events occur on random timers and provide temporary bonuses. Higher-tier settlements attract rarer events.

| Event              | Trigger                  | Effect                     | Duration  |
| ------------------ | ------------------------ | -------------------------- | --------- |
| Trade Boom         | Trade Hub specialization | +30% gold income           | 48 hrs    |
| Bumper Harvest     | Agricultural, 3+ Farms   | +50% food production       | 72 hrs    |
| Wandering Merchant | Any, random              | Rare items for purchase    | 24 hrs    |
| Festival           | Cultural trait cycle     | See Section 10.2           | 24-48 hrs |
| New Settlers       | Open Immigration policy  | +5-15 population (instant) | One-time  |

### 12.2 Negative Events

Negative events test settlement preparedness. Defense rating mitigates damage (see Section 5.2).

| Event            | Base Severity | Effect                          | Mitigation          |
| ---------------- | ------------- | ------------------------------- | ------------------- |
| Bandit Raid      | 30            | Resource loss, population risk  | Defense rating      |
| Plague           | 50            | Population decline, morale loss | Hospital tier       |
| Fire             | 40            | Building damage, resource loss  | Infrastructure tier |
| Monster Attack   | 40-80         | Population/building damage      | Defense + guards    |
| Political Unrest | 35            | Satisfaction loss, output drop  | Governance tier     |

---

## 13. Integration Points

### 13.1 System Dependencies

```
SETTLEMENT SYSTEM INTEGRATIONS:

  Gathering System (gathering-system-gdd.md):
    - Production buildings consume raw materials from gathering assignments
    - Gathering nodes within settlement radius provide passive resource income
    - Settlement specialization modifies gathering yields in territory
    - Gathered materials feed building construction costs

  Economy System (economy-system-gdd.md):
    - Settlement gold income feeds into global economy
    - Trade between settlements follows economy pricing rules
    - Building costs use economy-defined resource values
    - Commercial buildings generate gold through economy trade routes
    - Upkeep costs serve as primary gold sink

  Crafting System (crafting-system-gdd.md):
    - Smithy/Workshop buildings provide crafting workspace tiers
    - Settlement-level workspace upgrades improve crafter efficiency
    - Guild buildings unlock guild membership benefits
    - Production buildings supply refined materials for crafting

  Combat System:
    - Defense rating modifies raid and attack event outcomes
    - Guard NPCs contribute to defense rating
    - Barracks and Armory improve guard effectiveness

  Progression System:
    - Academy buildings provide XP bonuses to settlement NPCs
    - Guild buildings provide class-specific training timers
    - Settlement tier unlocks higher-tier service availability
```

### 13.2 Data Model Summary

```
Settlement:
  id:                UUID
  name:              String
  tier:              Enum(Camp, Village, Town, City)
  specializations:   List<Enum(Agricultural, Mining, TradeHub, Military, Religious, Academic)>
  cultural_trait:    Enum(Industrious, Scholarly, Militant, Mercantile, Devout, Resilient, Agrarian)
  population:        Integer
  population_cap:    Integer (sum of residential capacity)
  defense_rating:    Integer
  satisfaction:      Integer (0-100)
  gold:              Integer
  food_stored:       Integer
  buildings:         List<Building>
  policies:          List<Policy>
  construction_queue: Queue<BuildOrder>
  storage:           Map<ResourceType, Integer>
  event_log:         List<SettlementEvent>
  festival_timer:    Duration (seconds until next festival)
  upkeep_deficit:    Duration (time in deficit, if any)
  created_at:        Timestamp
  last_processed:    Timestamp

Building:
  id:          UUID
  type:        Enum(Residential, Production, Commercial, Infrastructure, Service, Guild)
  name:        String
  tier:        Integer (1-5)
  output_rate: Float
  upkeep_cost: Integer (Gold/hour)
  workers:     Integer

BuildOrder:
  building_id:   UUID
  target_tier:   Integer
  materials:     Map<ResourceType, Integer>
  timer_start:   Timestamp
  timer_end:     Timestamp
  modifiers:     List<TimerModifier>
```
