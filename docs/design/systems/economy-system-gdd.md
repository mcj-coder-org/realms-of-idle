---
type: system
scope: detailed
status: authoritative
version: 1.0.0
created: 2026-02-08
updated: 2026-02-08
subjects: [economy, trade, pricing, merchants, currency, inflation, idle]
dependencies: [quality-tiers.md, crafting-system-gdd.md, gathering-system-gdd.md]
---

# Economy System - Authoritative Game Design

## Executive Summary

The Economy System governs all currency flow, item pricing, merchant interaction, and trade logistics through timer-based trade queues and batch-calculated market updates. Players earn Gold through selling goods, complete trade runs via caravan timers, and interact with merchants whose stock and prices shift based on supply, demand, and settlement tier.

**This document resolves:**

- Single currency (Gold) with magnitude-based scaling
- Supply and demand pricing model (batch-calculated at check-in)
- Merchant types and stock distribution by settlement tier
- Trade route system (timer-based caravan runs)
- Resource sinks (repair, crafting, upkeep)
- Inflation control mechanisms (gold sinks, price floors/ceilings)
- Trade skill progression (Merchant, Trader, Caravaner)
- Market cycles (seasonal price fluctuations)
- Offline trade accumulation
- Automated buy/sell orders

**Design Philosophy:** The economy is a strategic optimization layer. Players choose what to sell, where to trade, and which routes to invest in, then the system executes over time. Batch processing at check-in replaces continuous simulation. Price movements create opportunities for attentive players without punishing casual ones. Gold sinks ensure long-term economic health across all progression stages.

---

## 1. Currency

### 1.1 Single Currency: Gold

All transactions use a single currency -- Gold -- displayed with magnitude suffixes to eliminate denomination friction in an idle context:

| Magnitude | Display | Raw Value     | Example Context          |
| --------- | ------- | ------------- | ------------------------ |
| Units     | 50g     | 50            | Basic supplies           |
| Thousands | 2.5K    | 2,500         | Standard equipment       |
| Millions  | 1.2M    | 1,200,000     | Settlement upgrades      |
| Billions  | 3.7B    | 3,700,000,000 | Late-game prestige costs |

| Design Choice       | Rationale                                             |
| ------------------- | ----------------------------------------------------- |
| Single denomination | No copper/silver/gold conversion math during offline  |
| Integer values      | Clean numbers for batch calculations                  |
| Magnitude scaling   | Prices span 1 to 1,000,000+ Gold naturally            |
| No physical risk    | Currency is abstract (no theft/loss mechanic on Gold) |

### 1.2 Gold Value Ranges

Gold values scale with progression tier to maintain meaningful choices at every stage:

| Progression Stage | Typical Income/hr | Typical Purchase Range | Example Items                    |
| ----------------- | ----------------- | ---------------------- | -------------------------------- |
| Early             | 10-50             | 1-500                  | Basic tools, common materials    |
| Mid               | 100-500           | 100-5,000              | Quality gear, workspace upgrades |
| Late              | 500-5,000         | 1,000-50,000           | Master equipment, trade fleets   |
| Endgame           | 5,000-25,000      | 10,000-500,000         | Legendary items, prestige costs  |

### 1.3 Currency Sources and Sinks

```
SOURCES (Gold In):
  Trade route income     (primary passive source)
  Selling to merchants   (manual or automated)
  Quest rewards          (one-time)
  Guild order payments   (crafting system)
  Loot drops             (combat system)
  Gem drops              (gathering system, 5-500 per gem)

SINKS (Gold Out):
  Equipment purchases    (combat, gathering tools)
  Training fees          (crafting apprenticeships, skill training)
  Workspace upgrades     (crafting-system-gdd.md Section 6)
  Settlement upkeep      (recurring daily cost)
  Caravan investment     (Section 5.3)
  Repair costs           (crafting-system-gdd.md Section 9)
  Threat mitigation      (gathering-system-gdd.md Section 5.3)
  Guild membership       (crafting-system-gdd.md Section 8)
  Merchant markup        (buy/sell spread)
```

---

## 2. Pricing Model

### 2.1 Base Prices

Every item has a base price determined by its category and tier:

```
Base Price = Category Base x Tier Multiplier x Quality Multiplier

Category Base (representative):
  Raw Material     = 5g
  Refined Material = 15g
  Basic Equipment  = 50g
  Advanced Equip   = 200g
  Consumable       = 10g
  Luxury Good      = 100g

Tier Multiplier:
  Basic      = 1x
  Apprentice = 3x
  Journeyman = 8x
  Master     = 20x
  Legendary  = 50x
  Artificer  = 100x

Quality Multiplier (from quality-tiers.md Section 9):
  Poor       = 0.5x
  Standard   = 1.0x
  Superior   = 2.0x
  Masterwork = 5.0x
  Legendary  = 10.0x
```

### 2.2 Supply and Demand Modifiers

Each settlement has supply/demand modifiers that shift prices. Values are recalculated at each check-in based on settlement state:

| Factor               | Effect                                   | Modifier Range |
| -------------------- | ---------------------------------------- | -------------- |
| Local supply         | Abundant locally = cheaper               | 0.70x-1.00x    |
| Local demand         | High need = more expensive               | 1.00x-1.50x    |
| Distance from source | Further from production = costlier       | 1.00x-1.40x    |
| Settlement tier      | Larger settlements have more competition | 0.85x-1.15x    |
| Season modifier      | Seasonal price cycles (Section 7)        | 0.80x-1.20x    |
| Trade route status   | Disrupted routes cause price spikes      | 1.00x-2.00x    |
| Event modifier       | Temporary price shocks (Section 7.3)     | 0.80x-1.60x    |

```
SUPPLY CALCULATION:
  Local Supply = Local Production + Caravan Imports - Recent Sales
  Supply Modifier = clamp(1.0 - (Local Supply / Demand Baseline) x 0.3, 0.70, 1.00)

DEMAND CALCULATION:
  Local Demand = Population Need + Craft Consumption + Export Orders
  Demand Modifier = clamp(1.0 + (Demand Deficit / Supply Baseline) x 0.5, 1.00, 1.50)

Batch Update:
  Supply and demand shift by at most 10% per check-in
  Prevents wild swings from single large transactions
  Converges toward equilibrium over 5-10 check-in cycles
```

### 2.3 Effective Price Formula

```
Effective Price = Base Price x Supply/Demand Mod x Settlement Size Mod x Season Mod x Event Mod

Buy Price  = Effective Price x Merchant Markup
Sell Price = Effective Price x Vendor Buy Rate

Merchant Markup (player buying from merchant):
  Village vendor  = 1.50 (50% markup)
  Town merchant   = 1.35 (35% markup)
  City specialist  = 1.25 (25% markup)
  Guild trader    = 1.15 (15% markup, members only)

Vendor Buy Rate (player selling to merchant):
  Base             = 0.40 (merchant pays 40% of effective price)
  + Haggling skill = +5% per rank (max +20%)
  + Guild member   = +10%
  + Merchant rep   = +5% per tier
  + Demand surplus = +0-15% (high demand items)
  Maximum          = 0.75 (75% of effective price)
```

### 2.4 Regional Specialization

Each region produces certain goods cheaply and imports others at premium:

| Region Type  | Exports (cheap locally) | Imports (expensive locally) |
| ------------ | ----------------------- | --------------------------- |
| Coastal      | Fish, salt, pearls      | Ore, lumber, leather        |
| Mining       | Ore, gems, stone        | Food, textiles, wood        |
| Agricultural | Crops, fruit, livestock | Metal goods, luxury items   |
| Forest       | Lumber, herbs, game     | Fish, ore, refined goods    |
| Desert/Arid  | Spices, crystals, sand  | Water, food, lumber         |

Export goods have a -20% local price modifier. Import goods have a +30% local price modifier. This differential creates the profit margin for trade routes.

### 2.5 Price Bounds

Hard limits prevent prices from becoming trivial or unobtainable:

```
PRICE BOUNDS:
  Floor   = Base Price x 0.25 (items never sell for less than 25% base)
  Ceiling = Base Price x 4.00 (items never cost more than 400% base)

  Exception: Legendary items have no ceiling (rarity-driven pricing)
  Exception: Quest-essential items have a ceiling of 200% base
```

---

## 3. Merchant System

### 3.1 Merchant Types

NPCs in settlements that buy and sell goods, classified by specialization:

| Merchant Type  | Specialization   | Buy Bonus | Stock Focus              |
| -------------- | ---------------- | --------- | ------------------------ |
| General Store  | None             | 0%        | Basic supplies           |
| Blacksmith     | Mineral, Artisan | +15%      | Weapons, armour, tools   |
| Alchemist      | Alchemical       | +15%      | Potions, reagents        |
| Farmer Market  | Agricultural     | +15%      | Food, crops, seeds       |
| Exotic Trader  | Exotic           | +15%      | Rare goods, luxury items |
| Flesh Merchant | Animals          | +15%      | Hides, meats, livestock  |

### 3.2 Merchant Properties by Settlement Tier

| Merchant Location | Stock Size | Restock Timer | Gold Reserve | Price Variance |
| ----------------- | ---------- | ------------- | ------------ | -------------- |
| Village           | 10-20      | 4 hr          | 500g         | +/-30%         |
| Town              | 20-40      | 2 hr          | 5K           | +/-20%         |
| City              | 30-60      | 1 hr          | 50K          | +/-10%         |
| Capital           | 40-80      | 30 min        | 500K         | +/-5%          |

### 3.3 Stock Distribution by Quality

Merchant stock quality follows quality-tiers.md Section 7 merchant distributions:

| Merchant Location | Poor | Standard | Superior | Masterwork | Legendary |
| ----------------- | ---- | -------- | -------- | ---------- | --------- |
| Village vendor    | 20%  | 60%      | 18%      | 2%         | 0%        |
| Town merchant     | 10%  | 50%      | 30%      | 9%         | 1%        |
| City specialist   | 0%   | 30%      | 45%      | 20%        | 5%        |
| Master crafter    | 0%   | 10%      | 30%      | 45%        | 15%       |

### 3.4 Gold Reserve

Merchants have finite Gold that replenishes over time:

```
Gold Reserve:
  Depletes when buying from player
  Replenishes at 10% of max per hour (real-time, offline-safe)
  When reserve = 0, merchant cannot buy (only sell)
  Replenish continues offline

  Full replenish: ~10 hours from empty
  Partial replenish: +25% of max per restock cycle
```

### 3.5 Inventory Restock

```
RESTOCK FLOW:
  Restock Timer expires
      |
      v
  Remove 20% of current stock (random)
      |
      v
  Add new items up to max capacity (weighted by quality distribution)
      |
      v
  Add caravan imports (if trade route active to this settlement)
      |
      v
  Apply seasonal stock adjustments
      |
      v
  Replenish Gold reserve (+25% of max)

  Offline: Restocks accumulate (max 3 pending restocks)
  Unsold stock carries over between restocks
  Caravan dependency: Import-heavy regions stock fewer goods if routes disrupted
```

### 3.6 Representative Merchants

| Merchant           | Location         | Specialty  | Notable Stock                     |
| ------------------ | ---------------- | ---------- | --------------------------------- |
| Gretta's Supplies  | Starting village | General    | Basic tools, food, common mats    |
| Ironhold Armory    | Mining town      | Weapons    | Steel weapons, mining picks       |
| Seaside Provisions | Coastal town     | Fish/Alch  | Fish, salt, healing herbs         |
| Arcanum Exchange   | Capital city     | Enchanting | Crystals, scrolls, enchanted gear |
| Wandering Jasper   | Travelling       | Exotic     | Rare imports, luxury goods        |

---

## 4. Trade Skill Progression

### 4.1 Trade Ranks

Trade skill advances through completed transactions:

| Rank        | XP Required | Sell Price Bonus | Unlock                 |
| ----------- | ----------- | ---------------- | ---------------------- |
| Barter      | 0           | 0%               | Basic trading          |
| Trader      | 500         | +5%              | Automated orders       |
| Merchant    | 2,500       | +10%             | Specialization choice  |
| Specialist  | 10,000      | +15%             | Bulk order access      |
| Guildmaster | 25,000      | +20%             | Trade route governance |

### 4.2 Unlock Path

Trade classes unlock through economic activity, not combat:

```
Complete 10+ sales/purchases
    |
    v
Trader class offered (passive unlock)
    |
    v
Accumulate 1,000+ Gold in trade profit
    |
    v
Merchant class offered
    |
    v
At Merchant rank, choose specialization
    |
    v
<Specialty> Apprentice -> Journeyman -> Master
    |
    v
Caravaner class (requires owning a caravan)
```

### 4.3 Trader Skills

| Skill        | Rank        | Effect                                     |
| ------------ | ----------- | ------------------------------------------ |
| Keen Eye     | Trader      | See true item value (quality + pricing)    |
| Haggler      | Trader      | +5% buy/sell rate at vendors               |
| Bulk Deal    | Merchant    | +10% rate on 10+ quantity sales            |
| Market Sense | Merchant    | See regional price differences             |
| Price Memory | Merchant    | Track price history per item               |
| Supply Chain | Specialist  | -10% caravan timer on established routes   |
| Price Setter | Guildmaster | Influence regional prices (small modifier) |

### 4.4 Merchant Specializations

At Merchant rank, choose one specialization for bonus pricing:

| Specialization | Category Focus     | Bonus                         |
| -------------- | ------------------ | ----------------------------- |
| Agricultural   | Food, crops, seeds | +20% sell, -10% buy for focus |
| Mineral        | Ores, ingots, gems | +20% sell, -10% buy for focus |
| Artisan        | Crafted equipment  | +20% sell, -10% buy for focus |
| Alchemical     | Potions, reagents  | +20% sell, -10% buy for focus |
| Exotic         | Rare, luxury goods | +20% sell, -10% buy for focus |

### 4.5 Caravaner Progression

The Caravaner class focuses on trade route optimization and unlocks through caravan ownership:

| Caravaner Level | Unlocks                                  |
| --------------- | ---------------------------------------- |
| 1-3             | Pack Animal, basic route planning        |
| 4-6             | Small Cart, 1 guard slot, route scouting |
| 7-9             | Wagon, 2 guard slots, speed bonus        |
| 10-12           | Large Wagon, 3 guard slots               |
| 13-15           | Convoy, 5 guard slots, multi-route       |
| 16-20           | Trade Fleet, 8 guard slots, trade empire |

### 4.6 Caravaner Skills

| Skill           | Level | Effect                                    |
| --------------- | ----- | ----------------------------------------- |
| Pathfinder      | 1     | -10% route timer                          |
| Cargo Master    | 4     | +20% cargo capacity                       |
| Danger Sense    | 7     | -15% risk event probability               |
| Efficient Route | 10    | -15% route timer (stacks with Pathfinder) |
| Trade Network   | 13    | Unlock 2 simultaneous routes (bonus)      |
| Fleet Admiral   | 16    | Unlock 3 simultaneous routes (bonus)      |

### 4.7 Caravaner Income Streams

| Service             | Payment                            |
| ------------------- | ---------------------------------- |
| Trading             | Profit margin (buy low, sell high) |
| Goods transport     | Fee per weight/distance            |
| Passenger transport | Fee per person/distance            |
| Special delivery    | Premium fee (quest-related)        |

---

## 5. Trade Routes

### 5.1 Route Establishment

Trade routes connect two settlements for automated caravan trade:

```
ESTABLISH ROUTE:
  Requirements:
    - Both settlements discovered
    - Trader rank or higher
    - Route establishment fee (scales with distance)

  Route Properties:
    Origin:      Settlement A
    Destination: Settlement B
    Distance:    Determines travel time (30 min - 16 hours)
    Risk Level:  Based on terrain and region threats
    Goods:       Configured by player (what to buy/sell where)
```

### 5.2 Route Flow

```
SELECT SOURCE SETTLEMENT
    |
    v
PURCHASE CARGO (buy at local prices)
    |
    v
SELECT DESTINATION SETTLEMENT
    |
    v
ASSIGN CARAVAN -> TIMER STARTS
    |
    v
TIMER RUNS (real-time, continues offline)
    |
    +--> RISK CHECK (per route segment)
    |      |-- No event: continue
    |      |-- Event: resolve (see Section 6)
    |
    v
ARRIVAL -> CARGO SOLD (at destination prices)
    |
    v
RETURN TRIP (same timer, reverse direction)
    |
    v
PROFIT CALCULATED -> COLLECT AT CHECK-IN
```

### 5.3 Caravan Tiers

Caravans are upgradeable assets that determine cargo capacity and risk mitigation:

| Caravan Tier | Unlock Cost | Cargo Capacity | Guard Slots | Speed Modifier |
| ------------ | ----------- | -------------- | ----------- | -------------- |
| Pack Animal  | 100g        | 10 units       | 0           | +25% slower    |
| Small Cart   | 500g        | 25 units       | 1           | Baseline       |
| Wagon        | 2,000g      | 50 units       | 2           | Baseline       |
| Large Wagon  | 8,000g      | 100 units      | 3           | -10% faster    |
| Convoy       | 25,000g     | 250 units      | 5           | -10% faster    |
| Trade Fleet  | 100,000g    | 500 units      | 8           | -20% faster    |

### 5.4 Route Configuration

Each route has buy/sell instructions for both ends:

```
ROUTE: Oakvale <-> Ironforge (4-hour round trip)

  At Oakvale:
    BUY:  Wheat (up to 20 units, max 5g each)
    BUY:  Healing Herbs (up to 10 units, max 10g each)
    SELL: Iron Ingots (min 15g each)
    SELL: Steel Swords (min 100g each)

  At Ironforge:
    BUY:  Iron Ore (up to 30 units, max 8g each)
    BUY:  Coal (up to 20 units, max 3g each)
    SELL: Wheat (min 8g each)
    SELL: Healing Herbs (min 15g each)
```

### 5.5 Route Income Calculation

```
Route Profit = Sum(Sell Revenue) - Sum(Buy Costs) - Operating Costs

Operating Costs:
  Caravan upkeep   = 10g per trip (base)
  Guard salary     = 5g per guard per trip
  Route toll       = 2% of cargo value (some regions)

Net Income per Trip = Route Profit - Operating Costs
Income per Hour     = Net Income / Round Trip Time
```

### 5.6 Route Slots

| Unlock Source      | Routes Granted | Condition            |
| ------------------ | -------------- | -------------------- |
| Base               | 1              | At Trader rank       |
| Trade progression  | +1 per rank    | Merchant, Specialist |
| Settlement upgrade | +1             | Trading post built   |
| Caravaner skills   | +2-3           | Trade Network/Fleet  |
| Maximum            | 8              | Hard cap             |

### 5.7 Representative Trade Routes

| Route                    | Timer  | Base Margin | Risk   | Best Cargo           |
| ------------------------ | ------ | ----------- | ------ | -------------------- |
| Village to Town (short)  | 30 min | 10-15%      | Low    | Common materials     |
| Town to Town (medium)    | 2 hr   | 15-20%      | Medium | Refined goods        |
| Town to City (long)      | 4 hr   | 20-30%      | Medium | Quality equipment    |
| Cross-region (extended)  | 8 hr   | 25-40%      | High   | Regional specialties |
| Exotic route (overnight) | 16 hr  | 35-50%      | High   | Rare materials       |

---

## 6. Trade Route Events

### 6.1 Event System

Each caravan trip rolls against the route's risk level:

```
EVENT CHECK:
  Per trip, roll against route Risk Level
  Risk Level = Base (terrain) + Region Threat - Guard Mitigation

  Event Impact:
    Safe Trip    (weighted by risk) -> Full profit
    Minor Delay  (common)           -> +25% travel time this trip
    Cargo Damage (uncommon)         -> -25% cargo value
    Bandit Raid  (rare)             -> -50% cargo, durability loss
    Total Loss   (very rare)        -> All cargo lost, major durability
```

### 6.2 Event Types

| Event         | Frequency | Impact                      | Mitigation       |
| ------------- | --------- | --------------------------- | ---------------- |
| Clear roads   | Common    | No effect (bonus XP)        | N/A              |
| Bad weather   | Common    | +25% travel time            | Reinforced wagon |
| Wheel break   | Uncommon  | +50% travel time, -10% dura | Reinforcement    |
| Minor theft   | Uncommon  | -10% cargo value            | +1 guard         |
| Bandit ambush | Rare      | -50% cargo, -20% dura       | +2 guards        |
| Catastrophe   | Very rare | All cargo lost, -50% dura   | +3 guards + luck |

### 6.3 Guard Mitigation

| Guards | Threat Reduction | Cost per Trip |
| ------ | ---------------- | ------------- |
| 0      | 0%               | 0g            |
| 1      | -20%             | 5g            |
| 2      | -40%             | 15g           |
| 3      | -60%             | 30g           |
| 5      | -75%             | 60g           |
| 8      | -90%             | 120g          |

```
Cargo Loss Reduction = Guards Filled / Guard Slots x 0.7
Example: 2/3 guards filled = 47% of cargo loss prevented
```

### 6.4 Caravan Maintenance

```
Durability:
  Loses 5% per trip (base)
  Risk events cause additional durability loss
  At 0% durability: caravan cannot travel (must repair)

Repair:
  Cost: 10% of total upgrade investment
  Timer: 1 hour
  Happens at origin settlement
  Continues offline
```

---

## 7. Market Cycles

### 7.1 Seasonal Price Fluctuations

The game uses four seasons that modify prices in predictable cycles. Season transitions are batch-calculated at check-in based on real time elapsed, not continuously simulated:

| Season | Duration    | Price Effects                                |
| ------ | ----------- | -------------------------------------------- |
| Spring | 7 real days | Crops -15%, lumber -10%, herbs -10%          |
| Summer | 7 real days | Fish -10%, fruit -15%, travel routes faster  |
| Autumn | 7 real days | Crops -20% (harvest glut), ore +10%          |
| Winter | 7 real days | Food +20%, lumber +15%, travel routes slower |

### 7.2 Seasonal Modifier Application

```
SEASON MODIFIER:
  Applied to base price before other modifiers
  Only affects relevant item categories
  Modifier applied: multiply base price by (1.0 + season_adjustment)

  Example (Winter, food):
    Base Price = 50g
    Season Modifier = +20% = 1.20
    Adjusted Base = 50 x 1.20 = 60g
    (Then apply supply/demand/quality on top)

  Travel Impact (Summer/Winter):
    Summer: -10% route timer (better roads)
    Winter: +15% route timer (worse conditions)
```

### 7.3 Event-Driven Price Shocks

Temporary events override normal pricing for affected goods:

| Event         | Duration | Effect                       | Recovery           |
| ------------- | -------- | ---------------------------- | ------------------ |
| Mine collapse | 3 days   | Ore +50%, gems +30%          | Gradual (-10%/day) |
| Blight        | 5 days   | Crops +40%, herbs +30%       | Gradual (-8%/day)  |
| Bandit surge  | 2 days   | All trade route risk +25%    | Instant at end     |
| Festival      | 1 day    | Food -20%, luxury goods +25% | Instant at end     |
| Trade embargo | 4 days   | Import goods +60%            | Gradual (-15%/day) |

Events are determined by the world simulation and communicated to the player at check-in. Multiple events can overlap.

---

## 8. Resource Sinks and Inflation Control

### 8.1 Sink Categories

Gold sinks are critical to preventing inflation. Every major system drains Gold:

| Sink Category      | System Reference             | Gold Drain Level  |
| ------------------ | ---------------------------- | ----------------- |
| Repair costs       | crafting-system-gdd.md S9    | Recurring         |
| Crafting materials | crafting-system-gdd.md S5    | Recurring         |
| Workspace upgrades | crafting-system-gdd.md S6    | One-time          |
| Tool purchases     | crafting-system-gdd.md       | Periodic          |
| Training fees      | crafting-system-gdd.md S1.2  | One-time per rank |
| Settlement upkeep  | Section 8.3                  | Recurring         |
| Caravan purchases  | Section 5.3                  | One-time          |
| Guard salaries     | gathering-system-gdd.md S5.3 | Recurring         |
| Threat mitigation  | gathering-system-gdd.md S5.3 | One-time/Periodic |
| Guild membership   | crafting-system-gdd.md S8    | One-time + dues   |
| Merchant markup    | Section 2.3                  | Per purchase      |

### 8.2 Repair Costs

Equipment degrades through use and must be repaired (see crafting-system-gdd.md Section 9):

```
Repair Cost = Base Item Value x 0.15 x Damage Level

Damage Level:
  Light   = 1.0 (standard repair)
  Heavy   = 1.5 (significant use)
  Severe  = 2.5 (combat damage or failed upgrade)

Example:
  Steel Longsword (base value 200g)
  Light damage repair = 200 x 0.15 x 1.0 = 30g (materials + service)
```

### 8.3 Settlement Upkeep

Settlements drain Gold through maintenance costs, scaling with size:

| Settlement Tier | Daily Upkeep | Covers                             |
| --------------- | ------------ | ---------------------------------- |
| Camp            | 0            | No infrastructure                  |
| Village         | 25g          | Basic buildings, paths             |
| Town            | 100g         | Walls, market, services            |
| City            | 500g         | Full infrastructure, guard patrols |
| Capital         | 2,000g       | All services, trade infrastructure |

Upkeep is deducted at each check-in proportional to elapsed time. Unpaid upkeep causes building degradation (reduced merchant stock, slower restocks, lower guard effectiveness).

### 8.4 Market Saturation

Repeated actions in the same market produce diminishing returns:

```
MARKET SATURATION:
  Selling the same item type repeatedly at one merchant:
    1st-5th sale   = 100% price
    6th-10th sale  = 90% price
    11th-20th sale = 75% price
    21st+ sale     = 60% price (floor)

  Saturation resets partially each restock cycle (+5 slots cleared)
  Different merchants have independent saturation counters
```

### 8.5 Sink Balance Targets

```
TARGET GOLD FLOW:
  Income sources should exceed sinks by 10-20% at each progression stage
  This margin provides growth capital for upgrades and investments

  Early game:  Gathering sales > repair + basic purchases
  Mid game:    Trade routes > upkeep + equipment upgrades
  Late game:   Trade empire > settlement + fleet maintenance
  Endgame:     Legendary trade > prestige investments
```

### 8.6 Gold Sinks by Progression Stage

| Stage   | Primary Sinks                        | Sink/Income Ratio |
| ------- | ------------------------------------ | ----------------- |
| Early   | Tools, training, basic repairs       | 70-80%            |
| Mid     | Workspace upgrades, caravan purchase | 75-85%            |
| Late    | Settlement upkeep, fleet expansion   | 80-90%            |
| Endgame | Prestige resets, legendary crafting  | 85-90%            |

---

## 9. Automated Trading

### 9.1 Automated Buy/Sell Orders

Players can configure automated trading rules (unlocked at Trader rank):

```
SELL ORDER:
  Item:        Iron Ore
  Condition:   When inventory > 50
  Price Floor:  8g per unit (won't sell below this)
  Merchant:    Blacksmith in Oakvale
  Frequency:   Check every 30 min

BUY ORDER:
  Item:        Healing Herb
  Condition:   When inventory < 20
  Price Ceiling: 12g per unit (won't buy above this)
  Merchant:     Alchemist in Oakvale
  Frequency:    Check every 30 min
```

### 9.2 Order Constraints

- Maximum 5 active orders per settlement
- Orders execute during offline processing
- Failed orders (price/reserve mismatch) retry next cycle
- Orders respect merchant Gold reserve limits
- Price thresholds prevent unprofitable trades during price spikes

---

## 10. Offline and Batch Processing

### 10.1 Offline Behavior

| System             | Offline Behavior                                 |
| ------------------ | ------------------------------------------------ |
| Trade routes       | Caravans complete trips, income accumulates      |
| Automated orders   | Execute per cycle, results stored for collection |
| Shop inventory     | Restocks on schedule (max 3 pending)             |
| Merchant reserve   | Replenishes at normal rate                       |
| Caravan durability | Degrades per trip (may stall if reaches 0%)      |
| Settlement upkeep  | Deducted proportionally at next check-in         |
| Market prices      | Recalculated at check-in based on elapsed time   |
| Season transitions | Progress based on real time elapsed              |

### 10.2 Check-in Summary

On player check-in, the economy system presents:

- Trade route income (Gold earned per route)
- Caravan trip count and events encountered
- Automated order executions (buys/sells completed)
- Failed orders (price/reserve mismatch)
- Settlement upkeep deducted
- Caravans needing repair
- Market price changes since last check-in
- Season transition notifications
- Notable price shocks or events

### 10.3 Batch Calculation

```
BATCH PROCESSING:
  Offline Duration = time since last check-in

  Trade Routes:
    Trips Completed = floor(Offline Duration / Round Trip Time)
    For each trip: roll event, calculate profit, apply durability
    Cap: 50 trips per route per offline session

  Automated Orders:
    Checks = floor(Offline Duration / Order Frequency)
    For each check: evaluate conditions, execute if met
    Cap: 100 checks per order per offline session

  Settlement Upkeep:
    Deduct: Upkeep Rate x (Elapsed Hours / 24)
    If Gold insufficient: flag degradation

  Cap: Maximum 48 hours of offline trade accumulation
  (prevents exploitation of very long offline periods)
```

---

## 11. Progression Hooks

### 11.1 System Integration

| Hook              | System Integration                               |
| ----------------- | ------------------------------------------------ |
| Quality tiers     | Price multipliers follow quality-tiers.md S9     |
| Crafting system   | Crafted goods are highest-value trade items      |
| Gathering system  | Raw materials are primary trade commodities      |
| Settlement        | Settlement size affects merchant variety/reserve |
| Combat system     | Loot drops feed sell income stream               |
| Prestige (future) | Trade mastery counts toward prestige             |

### 11.2 Economic Flow

```
GATHERING (gathering-system-gdd.md)
    |
    +--> Raw Materials
    |       |
    |       +--> Direct sale (lower value)
    |       +--> Crafting input (crafting-system-gdd.md)
    |               |
    |               +--> Finished goods
    |                       |
    |                       +--> Sell to merchant (moderate value)
    |                       +--> Trade route export (highest value)
    |
    v
GOLD EARNED
    |
    +--> Repair costs (crafting-system-gdd.md Section 9)
    +--> Material purchases (crafting inputs)
    +--> Workspace upgrades (crafting-system-gdd.md Section 6)
    +--> Caravan investment (Section 5.3)
    +--> Settlement upkeep (Section 8.3)
    +--> Training fees (crafting-system-gdd.md Section 1.2)
    |
    v
GOLD DRAINED (sinks maintain economic balance)
```

### 11.3 Value Chain

The economy rewards deeper processing -- raw materials are worth less than refined goods, which are worth less than finished equipment:

```
VALUE CHAIN MULTIPLIERS:
  Raw material (gathering)    = 1.0x base value
  Refined material (crafting) = 2.0x base value
  Finished good (crafting)    = 4.0x-8.0x base value
  Enchanted good (enchanting) = 8.0x-20.0x base value
  Trade route premium         = +20-40% on top of local value
```

This chain incentivizes investment in crafting and trade infrastructure rather than raw material dumping.
