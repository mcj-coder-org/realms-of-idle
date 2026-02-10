---
type: system
scope: detailed
status: authoritative
version: 1.0.0
created: 2026-02-10
updated: 2026-02-10
subjects: [cooking, crafting, recipes, food, butchering, brewing, idle]
dependencies: [crafting-system-gdd.md, gathering-system-gdd.md, quality-tiers.md]
---

# Cooking System - Authoritative Game Design

## Executive Summary

The Cooking System governs how characters transform raw ingredients into consumable food, beverages, and alchemical reagents through timer-based recipe execution. Cooking follows the same crafting pattern as item creation: raw materials are gathered, refined into ingredients via processing recipes, then combined through cooking recipes to produce consumables.

**This document resolves:**

- Cooking as a parallel crafting specialty to Blacksmithing/Woodworking
- Multi-stage recipe chains (butchering, brewing, cooking, preserving)
- Food quality impact on buffs and economic value
- Ingredient refinement and batch processing
- Kitchen workspace progression and benefits
- Food spoilage and preservation mechanics
- Consumable buffs and duration system

**Design Philosophy:** Cooking mirrors equipment crafting patterns. Raw food ingredients (Cow Corpse, Raw Wheat, Fresh Fish) are refined into base ingredients (Steak, Flour, Fillet) via processing, then combined through recipes into finished dishes (Cottage Pie, Ale, Smoked Fish). Quality matters: better ingredients and skilled cooks produce superior food with stronger buffs and higher trade value.

---

## 1. Cooking as Crafting Specialty

### 1.1 Rank Progression

Cooking follows the standard crafting progression (see crafting-system-gdd.md Section 1.1):

| Rank       | XP Required | Max Recipe Tier | Max Output Quality | Queue Slots |
| ---------- | ----------- | --------------- | ------------------ | ----------- |
| Untrained  | 0           | Basic           | Standard (2)       | 1           |
| Apprentice | 500         | Apprentice      | Superior (3)       | 2           |
| Journeyman | 2,500       | Journeyman      | Masterwork (4)     | 3           |
| Master     | 10,000      | Master          | Legendary (5)      | 4           |
| Artificer  | Special     | Artificer       | Legendary (5)      | 5           |

### 1.2 Cooking Specializations

Within the Cooking specialty, cooks can focus on sub-disciplines:

| Sub-Specialty | Primary Output                 | Key Materials               |
| ------------- | ------------------------------ | --------------------------- |
| Butchering    | Meat cuts, bones, hides        | Animal corpses, sharp tools |
| Baking        | Bread, pastries, desserts      | Flour, eggs, sugar          |
| Brewing       | Ale, wine, spirits, potions    | Grains, fruits, herbs       |
| Roasting      | Cooked meats, stews, pies      | Meat, vegetables, spices    |
| Preserving    | Smoked meat, pickles, jerky    | Salt, vinegar, smoking wood |
| Confectionery | Candy, preserves, honey treats | Sugar, honey, fruit         |

### 1.3 Learning Paths

Same two-path system as other crafting (see crafting-system-gdd.md Section 1.2):

```
APPRENTICESHIP PATH:
  - Assign to NPC master cook/butcher/brewer
  - Timer: 2 hours to reach Apprentice
  - Passive XP, produces Standard quality during training
  - Cost: Training fee (Gold)

SELF-TAUGHT PATH:
  - Learn by cooking (XP from completed recipes)
  - Timer: ~4 hours equivalent cooking time to Apprentice
  - +5% quality modifier at each rank
  - No fee, but slower and ingredient-intensive
```

---

## 2. Recipe Chain Patterns

### 2.1 Three-Stage Processing Model

Cooking recipes follow a harvest → refine → craft pattern, paralleling equipment crafting:

```
EQUIPMENT CRAFTING (Blacksmithing):
  Iron Ore (raw) → Iron Ingot (refined) → Iron Sword (crafted item)

FOOD CRAFTING (Cooking):
  Cow Corpse (raw) → Steak (refined ingredient) → Cottage Pie (cooked dish)

BEVERAGE CRAFTING (Brewing):
  Raw Wheat (raw) → Flour (milled) → Ale (brewed beverage)
```

### 2.2 Processing Stage (Refinement)

Raw gathered materials are processed into base ingredients:

| Raw Material   | Processing Recipe | Output Ingredient    | Yield | Timer  |
| -------------- | ----------------- | -------------------- | ----- | ------ |
| Cow Corpse     | Butcher           | 10× Steak            | 10    | 15 min |
| Cow Corpse     | Butcher           | 5× Ground Beef       | 5     | 15 min |
| Cow Corpse     | Butcher (skilled) | 1× Prime Cut (bonus) | 1     | 15 min |
| Pig Corpse     | Butcher           | 8× Pork Chop         | 8     | 12 min |
| Chicken Corpse | Butcher           | 4× Chicken Breast    | 4     | 8 min  |
| River Trout    | Fillet            | 2× Fish Fillet       | 2     | 5 min  |
| Raw Wheat      | Mill              | 3× Flour             | 3     | 10 min |
| Raw Wheat      | Brew (malting)    | 2× Malt              | 2     | 20 min |
| Wild Berries   | Press             | 1× Berry Juice       | 1     | 8 min  |
| Apple          | Press             | 1× Apple Cider       | 1     | 10 min |
| Potato         | Prepare           | 2× Peeled Potato     | 2     | 5 min  |
| Carrot         | Prepare           | 2× Chopped Carrot    | 2     | 5 min  |

**Processing Properties**:

- Processing recipes consume 1 raw material, produce multiple base ingredients (yield ratio)
- Timer-based queue execution (like all crafting)
- Quality of output matches quality of input (Standard Cow → Standard Steak)
- Skilled butchers/millers can produce bonus ingredients (e.g., Prime Cut from Master rank)
- Bone, hide, and offal byproducts can be kept for crafting/alchemy

### 2.3 Cooking Stage (Recipe Execution)

Base ingredients are combined through cooking recipes into finished dishes:

| Recipe             | Tier       | Timer  | Ingredients                           | Output           | Buff                       |
| ------------------ | ---------- | ------ | ------------------------------------- | ---------------- | -------------------------- |
| Grilled Steak      | Basic      | 10 min | 1× Steak                              | Food (meal)      | +5 HP regen for 30 min     |
| Cottage Pie        | Apprentice | 30 min | 1× Ground Beef, 2× Potato, 1× Carrot  | Food (meal)      | +10 HP, +5% Stamina regen  |
| Roast Chicken      | Apprentice | 25 min | 1× Chicken Breast, 1× Herb Mix        | Food (meal)      | +8 HP regen for 45 min     |
| Fish Stew          | Apprentice | 20 min | 2× Fish Fillet, 1× Carrot, 1× Onion   | Food (meal)      | +6 HP regen, +3% XP gain   |
| Meat Pie           | Journeyman | 45 min | 2× Pork Chop, 3× Flour, 1× Lard       | Food (meal)      | +15 HP, +10% Work Speed    |
| Hunter's Feast     | Master     | 2 hr   | 1× Prime Cut, 3× Veg, 2× Spice, Sauce | Food (feast)     | +25 HP, +15% All Stats 2hr |
| Ale                | Apprentice | 1 hr   | 2× Malt, 1× Hops, 1× Yeast            | Beverage (drink) | +5 Morale, -2% Work Timer  |
| Honey Mead         | Journeyman | 3 hr   | 3× Honey, 1× Yeast, 1× Spice          | Beverage (drink) | +10 Morale, +5% XP gain    |
| Smoked Salmon      | Journeyman | 1.5 hr | 2× Fish Fillet, 1× Salt, 1× Oak Wood  | Food (preserved) | +8 HP regen for 2 hours    |
| Pickled Vegetables | Apprentice | 45 min | 4× Vegetable Mix, 1× Vinegar, 1× Salt | Food (preserved) | +5 HP, lasts 7 days        |

**Cooking Properties**:

- Multi-ingredient recipes (2-6 components)
- Output quality capped by lowest ingredient quality (per crafting-system-gdd.md Section 5.3)
- Timer modifiers from skill rank and kitchen workspace (see Section 5)
- Outcome system applies: Success/Great Success/Masterwork (see crafting-system-gdd.md Section 4)
- Masterwork food provides enhanced buffs (+50% effectiveness, +50% duration)

### 2.4 Preservation Stage (Optional)

Finished food can be further processed for extended shelf life:

| Preservation Recipe | Input              | Output             | Timer  | Shelf Life      |
| ------------------- | ------------------ | ------------------ | ------ | --------------- |
| Smoke Meat          | 2× Cooked Meat     | 1× Smoked Meat     | 30 min | 7 days          |
| Pickle              | 4× Vegetable       | 1× Pickled Veg     | 45 min | 14 days         |
| Dry Fruit           | 3× Fresh Fruit     | 1× Dried Fruit     | 1 hr   | 30 days         |
| Cure Meat           | 2× Raw Meat, Salt  | 1× Cured Meat      | 2 hr   | 21 days         |
| Make Jerky          | 3× Steak, Salt     | 2× Jerky           | 1.5 hr | 60 days         |
| Preserve Jam        | 5× Berries, Sugar  | 2× Berry Jam       | 1 hr   | 90 days         |
| Salt Fish           | 2× Fish, 1× Salt   | 1× Salted Fish     | 45 min | 14 days         |
| Can Vegetables      | 4× Veg, 1× Vinegar | 2× Canned Veg      | 1.5 hr | 180 days        |
| Age Cheese          | 3× Milk, Rennet    | 1× Aged Cheese     | 12 hr  | 60 days (aging) |
| Ferment Cabbage     | 4× Cabbage, Salt   | 2× Sauerkraut      | 24 hr  | 90 days         |
| Render Lard         | 3× Pork Fat        | 2× Lard (crafting) | 30 min | No spoil        |
| Make Stock          | 4× Bone, 1× Veg    | 2× Stock (cooking) | 2 hr   | 3 days          |

---

## 3. Food Quality and Buffs

### 3.1 Quality Tier Impact

Food quality follows quality-tiers.md 1-5 scale and affects buff strength:

| Food Quality   | Buff Strength | Buff Duration | Trade Value | Morale Bonus |
| -------------- | ------------- | ------------- | ----------- | ------------ |
| Poor (1)       | 0.50× base    | 0.75× base    | 0.5× base   | +0 Morale    |
| Standard (2)   | 1.00× base    | 1.00× base    | 1× base     | +2 Morale    |
| Superior (3)   | 1.25× base    | 1.25× base    | 2× base     | +5 Morale    |
| Masterwork (4) | 1.50× base    | 1.50× base    | 5× base     | +10 Morale   |
| Legendary (5)  | 2.00× base    | 2.00× base    | 10× base    | +20 Morale   |

**Example**:

```
Cottage Pie (Apprentice recipe):
  Base buff: +10 HP, +5% Stamina regen for 60 min

Standard (2) Cottage Pie:
  +10 HP, +5% Stamina regen for 60 min, +2 Morale

Masterwork (4) Cottage Pie:
  +15 HP, +7.5% Stamina regen for 90 min, +10 Morale
```

### 3.2 Buff Categories

| Buff Type        | Examples                                | Stacking Rules                   |
| ---------------- | --------------------------------------- | -------------------------------- |
| HP Regeneration  | +5 HP/min for duration                  | Does NOT stack (highest applies) |
| Flat HP Bonus    | +15 HP instant heal                     | Stacks (additive)                |
| Work Speed       | +10% crafting/gathering timer reduction | Stacks (multiplicative)          |
| XP Gain          | +5% XP from all sources                 | Stacks (multiplicative)          |
| Morale           | +10 Morale (affects NPC happiness)      | Stacks (additive)                |
| Stamina Regen    | +5% stamina recovery rate               | Stacks (multiplicative)          |
| Combat Stats     | +5% Strength, Dexterity, etc.           | Stacks (multiplicative)          |
| Energy Recharge  | +20% crafting energy recharge rate      | Stacks (multiplicative)          |
| Threat Reduction | -10% gathering threat level             | Stacks (additive)                |

### 3.3 Consumable Types

| Type      | Duration      | Buff Strength  | Use Case                  |
| --------- | ------------- | -------------- | ------------------------- |
| Snack     | 10-15 min     | Minor (+2-5)   | Quick energy boost        |
| Meal      | 30-60 min     | Moderate (+10) | Standard sustenance       |
| Feast     | 2-4 hours     | Strong (+25)   | Pre-adventure preparation |
| Preserved | 1-2 hours     | Moderate (+8)  | Long journeys, storage    |
| Beverage  | 30-90 min     | Mixed (Morale) | Socializing, inn service  |
| Potion    | Instant/5 min | Specific       | Combat, emergency healing |

---

## 4. Kitchen Workspace System

### 4.1 Kitchen Tiers

Kitchens follow the standard workspace progression (see crafting-system-gdd.md Section 6.1):

| Tier        | Unlock Cost | Timer Modifier | Quality Bonus | Capacity | Special Features          |
| ----------- | ----------- | -------------- | ------------- | -------- | ------------------------- |
| Campfire    | Free        | +40% slower    | -10%          | 1 cook   | Can burn food             |
| Basic       | 100 Gold    | Baseline       | 0%            | 1 cook   | Standard kitchen          |
| Improved    | 500 Gold    | -15% faster    | +5%           | 2 cooks  | Oven + stove              |
| Advanced    | 2,500 Gold  | -30% faster    | +10%          | 3 cooks  | Smokehouse, brewery       |
| Master      | 10,000 Gold | -40% faster    | +15%          | 4 cooks  | Aging room, cold storage  |
| Grand Feast | 25,000 Gold | -50% faster    | +20%          | 6 cooks  | Banquet hall, wine cellar |

### 4.2 Kitchen Specialization

Kitchens can be specialized (same rules as crafting-system-gdd.md Section 6.2):

- **Bakery**: +10% timer, +5% quality for bread/pastry recipes
- **Butchery**: +10% yield from butchering corpses
- **Brewery**: +10% timer, +5% quality for beverage recipes
- **Smokehouse**: +15% timer for preservation recipes, no spoilage penalty
- **Tavern Kitchen**: +5% quality for meal recipes, +10 Morale bonus on food
- **Confectionery**: +10% quality for desserts, candy

### 4.3 Kitchen Equipment

Specialized equipment provides additional bonuses:

| Equipment        | Cost       | Benefit                                 | Tier Required |
| ---------------- | ---------- | --------------------------------------- | ------------- |
| Quality Knives   | 50 Gold    | +5% butchering yield                    | Basic         |
| Cast Iron Pot    | 100 Gold   | +10% stew/soup quality                  | Improved      |
| Stone Oven       | 300 Gold   | +15% baking timer reduction             | Improved      |
| Smoking Rack     | 200 Gold   | 2x preservation batch size              | Advanced      |
| Brewing Vat      | 400 Gold   | 3x beverage batch size                  | Advanced      |
| Cold Cellar      | 800 Gold   | +50% food shelf life                    | Advanced      |
| Aging Barrels    | 600 Gold   | Enables wine/cheese aging recipes       | Master        |
| Master Cookware  | 1,500 Gold | +10% quality all recipes                | Master        |
| Enchanted Hearth | 5,000 Gold | -20% timer, no burn chance, +5% quality | Grand Feast   |

---

## 5. Food Spoilage and Preservation

### 5.1 Spoilage Mechanics

Cooked food has limited shelf life (unlike equipment which doesn't decay):

```
FRESHNESS DECAY:
  Fresh food starts at 100% freshness
  Decays at 1% per hour (real-time, continues offline)
  At 50% freshness: -25% buff effectiveness
  At 25% freshness: -50% buff effectiveness, Morale penalty
  At 0% freshness: Spoiled (unusable, -10 Morale if consumed)

BASE SHELF LIFE (without preservation):
  Snacks/Meals      = 24 hours (1 day)
  Feast dishes      = 48 hours (2 days)
  Beverages (drink) = 72 hours (3 days)
  Preserved foods   = 7-180 days (see Section 2.4)
  Raw ingredients   = 12-48 hours depending on type
```

### 5.2 Storage Solutions

| Storage Type     | Cost        | Freshness Decay | Capacity    | Tier Required |
| ---------------- | ----------- | --------------- | ----------- | ------------- |
| None (inventory) | Free        | Normal (1%/hr)  | 20 items    | Any           |
| Pantry           | 50 Gold     | -25% decay      | 50 items    | Basic         |
| Larder           | 200 Gold    | -50% decay      | 100 items   | Improved      |
| Cold Cellar      | 800 Gold    | -75% decay      | 200 items   | Advanced      |
| Ice House        | 2,000 Gold  | -90% decay      | 500 items   | Master        |
| Enchanted Vault  | 10,000 Gold | No decay        | 1,000 items | Grand Feast   |

### 5.3 Preservation Techniques

| Technique      | Shelf Life Extension  | Quality Impact | Requirements                |
| -------------- | --------------------- | -------------- | --------------------------- |
| Smoking        | +7 days               | -0 tiers       | Smokehouse, wood fuel       |
| Salting        | +14 days              | -1 tier        | Salt (abundant)             |
| Pickling       | +14 days              | -0 tiers       | Vinegar, salt               |
| Drying         | +30 days              | -1 tier        | Time (slow), no equipment   |
| Canning        | +180 days             | -0 tiers       | Jars, vinegar, Advanced     |
| Freezing       | +90 days              | -0 tiers       | Ice House (expensive)       |
| Aging          | Special (cheese/wine) | +1 tier        | Aging room, time (12-48 hr) |
| Vacuum Sealing | +60 days              | -0 tiers       | Artifcer tech (late game)   |

---

## 6. Cooking Energy

### 6.1 Energy Costs

Cooking uses the same energy system as crafting (see crafting-system-gdd.md Section 7):

```
Max Energy: 100 (base) + 10 per cooking rank
Recharge: 1 energy per 3 minutes (real-time, offline-safe)

Energy Cost per Recipe:
  Processing (Butcher, Mill)  = 3 energy
  Basic recipes               = 5 energy
  Apprentice recipes          = 10 energy
  Journeyman recipes          = 20 energy
  Master recipes              = 35 energy
  Legendary recipes           = 50 energy
  Artificer recipes           = 60 energy (e.g., Transmutation Feast)
```

### 6.2 Energy Recovery (Food Synergy)

Consuming high-quality food provides energy recovery:

| Food Quality   | Energy Restored | Cooldown |
| -------------- | --------------- | -------- |
| Standard (2)   | +5 energy       | 30 min   |
| Superior (3)   | +10 energy      | 30 min   |
| Masterwork (4) | +20 energy      | 30 min   |
| Legendary (5)  | +35 energy      | 30 min   |
| Feast          | +50 energy      | 1 hour   |

This creates positive feedback: skilled cooks produce quality food → consume their own food → faster energy recharge → cook more efficiently.

---

## 7. Inn and Tavern Integration

### 7.1 Customer Service

Cooking integrates with inn/tavern gameplay (see idle-inn-tavern.md):

```
CUSTOMER ORDER FLOW:
  Customer enters tavern -> Orders food/drink
      |
      v
  Kitchen checks inventory for matching food
      |
      +-- Food available -> Serve (2-5 min timer)
      |     +-- Customer satisfaction based on food quality
      |     +-- Gold earned based on food value
      |     +-- Reputation gain
      |
      +-- Food unavailable -> Customer waits or leaves
            +-- -1 Reputation if wait too long
```

### 7.2 Menu Pricing

| Food Quality   | Base Price Multiplier | Customer Satisfaction | Reputation Gain |
| -------------- | --------------------- | --------------------- | --------------- |
| Poor (1)       | 0.5×                  | -5 Satisfaction       | +0 Reputation   |
| Standard (2)   | 1×                    | +0 Satisfaction       | +1 Reputation   |
| Superior (3)   | 2×                    | +10 Satisfaction      | +3 Reputation   |
| Masterwork (4) | 5×                    | +25 Satisfaction      | +8 Reputation   |
| Legendary (5)  | 10×+                  | +50 Satisfaction      | +20 Reputation  |

**Example**:

```
Cottage Pie (Apprentice tier):
  Base Price = 15 Gold

Standard (2) Cottage Pie:
  Sell Price = 15 Gold
  Customer: +0 Satisfaction, +1 Reputation

Masterwork (4) Cottage Pie:
  Sell Price = 75 Gold
  Customer: +25 Satisfaction, +8 Reputation

Effect: Master cooks earn 5x revenue and build reputation faster
```

### 7.3 Special Events

| Event Type       | Trigger                       | Effect                                             | Duration  |
| ---------------- | ----------------------------- | -------------------------------------------------- | --------- |
| Feast Night      | Serve 3+ feast dishes         | +50% customer attraction, +20% prices              | 4 hours   |
| Ale Festival     | Brew 10+ beverages            | +100% beverage sales, +10 Morale region-wide       | 24 hours  |
| Food Critic      | Random (Masterwork+ required) | +100 Reputation if quality 4+, -50 if below        | One visit |
| Harvest Festival | Autumn season                 | +25% food quality, customer demand doubles         | 7 days    |
| Royal Visit      | Quest unlock                  | Serve Legendary feast → +1000 Gold, unlock recipes | One event |

---

## 8. Idle Integration

### 8.1 Offline Behavior

| System           | Offline Behavior                                      |
| ---------------- | ----------------------------------------------------- |
| Cooking queues   | Continue running, results stored for collection       |
| Food spoilage    | Freshness decays at normal rate                       |
| Inn service      | Customers served automatically (if food available)    |
| Butchering batch | Process multiple corpses sequentially                 |
| Brewing timers   | Long-duration beverages (ale, wine) complete normally |
| Energy recharge  | Recharges at normal rate                              |

### 8.2 Check-in Collection

On player check-in, system presents:

- Completed cooking results with quality outcomes
- Food spoilage status (warnings if items near expiration)
- Inn revenue from food service (if applicable)
- Corpse butchering results (if queued)
- Brewing completion (ale, wine, mead)
- Recipe discovery notifications

### 8.3 Batch Processing

Cooking supports batch queuing (like material refinement in crafting-system-gdd.md Section 5.2):

- Queue up to 10 identical recipes per slot
- All batches process sequentially offline
- Results accumulate for collection at check-in
- Failed batches return partial ingredients (50% refund)

---

## 9. Progression Hooks

### 9.1 System Integration

| Hook              | Integration                                                         |
| ----------------- | ------------------------------------------------------------------- |
| Gathering system  | Provides raw food ingredients (corpses, wheat, fish, herbs)         |
| Crafting system   | Follows same progression, queue, workspace patterns                 |
| Quality tiers     | Food quality uses quality-tiers.md 1-5 scale                        |
| Economy system    | Cooked food sold at inn or via trade routes                         |
| Morale system     | Food quality affects NPC morale (morale-system-gdd.md)              |
| Settlement system | Kitchen upgrades require settlement building progression            |
| Inn/Tavern        | Food service drives inn revenue and reputation (idle-inn-tavern.md) |
| Alchemy system    | Cooking produces reagents, shares herb processing (alchemy overlap) |
| Combat system     | Food buffs enhance combat stats                                     |
| Prestige (future) | Cooking mastery counts toward prestige                              |

### 9.2 Material Flow

```
GATHERING -> Raw Ingredients (corpses, grains, fish)
    |
    +-- Processing -> Base Ingredients (steak, flour, fillet)
    |       |
    |       +-- Cooking -> Finished Dishes (pies, stews, ale)
    |               |
    |               +-- Consumption (buff application)
    |               +-- Sale (inn service, trade routes)
    |               +-- Preservation -> Long-shelf-life food
    |
    +-- Butchering -> Byproducts (bones, hide, fat)
            |
            +-- Crafting (leatherwork uses hides)
            +-- Alchemy (bones for reagents)
            +-- Rendering (fat -> lard for cooking)
```

### 9.3 Recipe Discovery

Recipes discovered through same methods as crafting (see crafting-system-gdd.md Section 2.2):

- **Rank-up rewards**: 2-3 recipes per cooking rank milestone
- **Cooking discovery**: Small chance on Great Success/Masterwork
- **NPC teaching**: Apprenticeship masters teach specialty recipes
- **Loot drops**: Rare recipe scrolls from inn guests, traveling merchants
- **Guild library**: Cooking guild shares recipes (copy cost)
- **Quest rewards**: Special recipes from quest completion

---

## 10. Representative Recipe Chains

### 10.1 Beef Production Chain

```
GATHERING (Hunting):
  Cow (hunted) -> Cow Corpse (raw, Quality 2)

PROCESSING (Butchering):
  Cow Corpse -> 10× Steak (Quality 2), 5× Ground Beef, 1× Bone, 1× Hide
  Timer: 15 min | Energy: 3

COOKING (Roasting):
  1× Steak -> Grilled Steak (meal, Quality 2)
  Timer: 10 min | Energy: 5
  Buff: +5 HP regen for 30 min

COOKING (Baking):
  1× Ground Beef + 2× Potato + 1× Carrot -> Cottage Pie (meal, Quality 2)
  Timer: 30 min | Energy: 10
  Buff: +10 HP, +5% Stamina regen for 60 min

PRESERVATION (Smoking):
  2× Grilled Steak + 1× Oak Wood -> 1× Smoked Steak (preserved, Quality 2)
  Timer: 30 min | Energy: 5
  Shelf Life: 7 days
```

### 10.2 Brewing Chain

```
GATHERING (Farming):
  Wild Wheat (harvested) -> Raw Wheat (raw, Quality 1)

PROCESSING (Malting):
  Raw Wheat -> 2× Malt (ingredient, Quality 1)
  Timer: 20 min | Energy: 3

BREWING (Ale Production):
  2× Malt + 1× Hops + 1× Yeast -> Ale (beverage, Quality 1)
  Timer: 1 hour | Energy: 10
  Buff: +5 Morale, -2% Work Timer for 45 min

AGING (Optional):
  Ale + Aging Barrel -> Aged Ale (beverage, Quality 2+)
  Timer: 12 hours | Energy: 5
  Buff: +10 Morale, -5% Work Timer for 90 min
```

### 10.3 Fish Chain

```
GATHERING (Fishing):
  River Trout (caught) -> River Trout (raw, Quality 2)

PROCESSING (Filleting):
  River Trout -> 2× Fish Fillet (ingredient, Quality 2)
  Timer: 5 min | Energy: 3

COOKING (Stew):
  2× Fish Fillet + 1× Carrot + 1× Onion -> Fish Stew (meal, Quality 2)
  Timer: 20 min | Energy: 10
  Buff: +6 HP regen, +3% XP gain for 45 min

PRESERVATION (Salting):
  2× Fish Fillet + 1× Salt -> 1× Salted Fish (preserved, Quality 2)
  Timer: 45 min | Energy: 5
  Shelf Life: 14 days
```

---

## 11. Advanced Cooking Mechanics (Future)

### 11.1 Flavor Profiles (Expansion)

Future expansion could add flavor combinations:

- **Sweet + Savory**: +10% buff duration
- **Spicy + Sour**: +15% XP gain bonus
- **Herbal + Earthy**: +10% Stamina regen
- **Bitter + Sweet**: +20% Morale gain
- **Umami + Salt**: +10% HP regen

### 11.2 Masterwork Dishes (Artificer Tier)

Artificer cooks can create legendary dishes with unique effects:

- **Transmutation Feast**: Temporarily boosts gathering yield +25% for 4 hours
- **Alchemist's Banquet**: +50% Alchemy XP gain for 6 hours
- **Warrior's Repast**: +15% combat stats for 3 hours
- **Scholar's Meal**: +30% XP gain all skills for 2 hours
- **Endurance Platter**: +50 max Stamina for 12 hours

### 11.3 Cooking Competitions (Events)

Seasonal events where cooks compete:

- Judges rate dishes on quality, presentation, rarity
- Winners receive unique recipes, titles, gold prizes
- Reputation boost region-wide for winners

---

## Conclusion

Cooking now parallels equipment crafting as a full crafting specialty. The three-stage model (harvest → refine → craft) matches blacksmithing patterns:

- **Blacksmith**: Iron Ore → Iron Ingot → Iron Sword
- **Cook**: Cow Corpse → Steak → Cottage Pie
- **Brewer**: Raw Wheat → Malt → Ale

Both systems share:

- Rank progression (Apprentice → Master → Artificer)
- Timer-based queue execution
- Quality tier integration (1-5 scale)
- Workspace progression (Basic → Master → Grand)
- Outcome probability (Success/Great Success/Masterwork)
- Offline accumulation and batch processing
- Energy management and recharge

Unique to cooking:

- Spoilage mechanics (food has limited shelf life)
- Preservation techniques (smoking, salting, aging)
- Buff application (consumables provide temporary bonuses)
- Inn/tavern service integration (food drives revenue)
- Morale system integration (quality food improves NPC happiness)
