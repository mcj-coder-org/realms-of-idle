---
type: system
scope: detailed
status: draft
version: 1.0.0
created: 2026-02-02
updated: 2026-02-02
subjects: [game-design, monster-farm, idle-game]
dependencies: []
---

# Monster Farm/Ranch - Idle Game Design Document

## Core Fantasy

You are a [Monster Rancher] or [Beast Tamer] who raises, breeds, and harvests creatures that others fear. From docile slimes to majestic dragons, your ranch produces valuable materials, combat-ready beasts, and rare specimens. This scenario blends creature collection, breeding mechanics, and resource production into a satisfying idle loop.

---

## Primary Game Loop

```
ACQUIRE → RAISE → HARVEST/BREED → PROCESS → SELL/USE → EXPAND → ACQUIRE (better)
```

1. **Acquire Monsters** (multiple methods)
   - Capture wild monsters (active expeditions or passive traps)
   - Purchase from traders
   - Hatch from eggs
   - Breed from existing stock

2. **Raise & Care** (semi-idle with active optimization)
   - Assign to habitats
   - Feed appropriate diet
   - Train for stat growth
   - Monitor health and happiness

3. **Harvest Resources** (passive with timing bonuses)
   - Renewable: Milk, wool, scales, venom (regenerates)
   - Periodic: Eggs, shed materials
   - One-time: Butchering (removes monster)

4. **Breed for Improvement** (active selection, idle gestation)
   - Pair compatible monsters
   - Genetics system determines offspring traits
   - Rare combinations produce unique variants

5. **Process & Sell** (idle production)
   - Raw materials → Refined goods
   - Fill orders from various buyers
   - Special commissions for rare items

---

## Resource Systems

### Primary Resources

| Resource              | Generation                     | Primary Use               |
| --------------------- | ------------------------------ | ------------------------- |
| **Gold**              | Sales, commissions             | Purchases, upgrades, feed |
| **Feed**              | Purchased, grown, hunted       | Monster sustenance        |
| **Monster Materials** | Harvesting                     | Crafting, sales, quests   |
| **Ranch Points**      | Daily operations, achievements | Facility upgrades         |

### Monster-Specific Resources

- **Affection**: Built through care, affects yield and breeding success
- **Health**: Maintained through proper habitat and diet
- **Energy**: Depleted by harvesting, regenerates with rest and food
- **Maturity**: Growth stage affecting what can be harvested

---

## Monster System

### Acquisition Methods

| Method           | Cost                | Quality              | Speed    |
| ---------------- | ------------------- | -------------------- | -------- |
| **Wild Capture** | Time + risk         | Variable             | Slow     |
| **Egg Hatching** | Incubation time     | Predictable          | Medium   |
| **Breeding**     | Two monsters + time | Potentially superior | Slow     |
| **Purchase**     | Gold                | Guaranteed stats     | Instant  |
| **Quest Reward** | Effort              | Often rare           | Variable |

### Monster Categories

| Category        | Examples                   | Primary Products     | Difficulty   |
| --------------- | -------------------------- | -------------------- | ------------ |
| **Slimes**      | Basic, Elemental, King     | Gel, cores, essence  | Beginner     |
| **Beasts**      | Wolves, Bears, Boars       | Meat, pelts, fangs   | Beginner     |
| **Avians**      | Cockatrice, Phoenix, Roc   | Feathers, eggs, down | Intermediate |
| **Reptiles**    | Basilisk, Drake, Hydra     | Scales, venom, hide  | Intermediate |
| **Magical**     | Unicorn, Kirin, Carbuncle  | Horn, blood, tears   | Advanced     |
| **Dragons**     | Wyvern, Drake, True Dragon | Everything valuable  | Expert       |
| **Aberrations** | Mimic, Beholder, Chimera   | Exotic components    | Expert       |

### Monster Stats

- **Yield**: How much material produced per harvest
- **Quality**: Grade of materials (affects price)
- **Temperament**: Ease of handling, escape risk
- **Growth Rate**: Speed to maturity
- **Lifespan**: How long before natural death
- **Fertility**: Breeding success rate

---

## Breeding System

### Genetics Framework

Each monster has:

- **Dominant Traits**: Always passed to offspring
- **Recessive Traits**: May appear with matching pair
- **Mutations**: Small chance of random new traits
- **Inherited Stats**: Blend of parents with variance

### Breeding Goals

- **Stat Maximization**: Breed for highest yield/quality
- **Trait Hunting**: Combine for rare trait combinations
- **Variant Discovery**: Certain pairings create unique species
- **Color Breeding**: Aesthetic variants (collector appeal)

### Breeding Mechanics

1. Select compatible pair (species, opposite sex or magical means)
2. Ensure high affection for both
3. Provide breeding habitat
4. Wait gestation period (varies by species)
5. Offspring inherits traits with some randomness

### Rare Outcomes

- **Twins/Triplets**: Multiple offspring
- **Mutation**: New trait appears
- **Throwback**: Ancestral trait resurfaces
- **Hybrid**: Cross-species breeding (rare, requires items)

---

## Habitat System

### Habitat Types

| Habitat             | Suitable For           | Capacity | Special Features             |
| ------------------- | ---------------------- | -------- | ---------------------------- |
| **Pasture**         | Beasts, basic monsters | High     | Cheap, self-feeding (grass)  |
| **Cavern**          | Reptiles, slimes       | Medium   | Cool, dark, mineral deposits |
| **Aviary**          | Flying creatures       | Medium   | Height requirements          |
| **Aquatic**         | Water creatures        | Low      | Water quality management     |
| **Volcanic**        | Fire creatures         | Low      | Heat maintenance             |
| **Enchanted Grove** | Magical creatures      | Low      | Mana upkeep required         |
| **Dragon Roost**    | Dragons                | Very Low | Massive, expensive           |

### Habitat Upgrades

- **Capacity**: House more monsters
- **Comfort**: Increases affection gain, health
- **Efficiency**: Faster resource regeneration
- **Automation**: Auto-feeding, auto-harvesting
- **Security**: Prevents escapes, protects from predators

---

## Harvesting & Production

### Harvest Types

| Type            | Regeneration  | Action             | Value   |
| --------------- | ------------- | ------------------ | ------- |
| **Passive**     | Continuous    | Auto-collect       | Low     |
| **Timed**       | Set intervals | Collect when ready | Medium  |
| **Active**      | On demand     | Manual trigger     | Higher  |
| **Consumptive** | One-time      | Removes monster    | Highest |

### Processing Chain

```
Raw Material → Processing Facility → Refined Product → Market/Use
```

Examples:

- Slime Gel → Refinery → Alchemical Base
- Dragon Scale → Forge → Armor Plate
- Phoenix Feather → Enchanting Table → Spell Component
- Monster Meat → Kitchen → Preserved Rations

### Quality Grades

Materials come in grades: F → E → D → C → B → A → S → SS
Higher grades from: better monsters, higher affection, optimal conditions, skills

---

## Prestige System: "Legendary Lineage"

### Trigger Conditions

- Breed an SS-grade monster
- Complete the "Mythic Beast" collection
- Raise a monster to maximum possible stats

### Reset Mechanics

- Ranch resets to basic facilities
- Monster stock released (converted to Legacy Points)
- Retain: Rancher skills, breeding records, recipe knowledge
- Gain: "Bloodline Points" for permanent genetic bonuses

### Legacy Bonuses

- "Renowned Breeder" - Higher base stats on all offspring
- "Gentle Touch" - Faster affection gain
- "Keen Eye" - See hidden genetic traits
- "Mythic Ancestry" - Rare traits appear more often

---

## Active vs. Idle Balance

### Idle Mechanics

- Monsters eat, rest, and grow automatically (if fed)
- Passive harvesting collects low-tier resources
- Eggs incubate
- Breeding progresses
- Orders auto-fill from stockpile

### Active Engagement Rewards

- **Optimal Harvest Timing**: Harvest at peak for bonus yield
- **Training Minigames**: Boost stats through active play
- **Affection Activities**: Pet, play, groom for relationship boost
- **Breeding Selection**: Manual pairing beats auto-breed
- **Capture Expeditions**: Hunt for wild specimens
- **Rare Spawns**: Limited-time monsters appear, require quick response

### Care Consequences

- Neglected monsters: Lower yield, health problems, escape
- Overharvested: Exhaustion, reduced quality
- Perfect care: Bonus yields, rare drops, breeding bonuses

---

## Event System

### Regular Events

- **Mating Season**: Breeding success rates increased
- **Monster Migration**: Rare wild captures available
- **Market Boom**: Certain materials worth more

### Story Events

- A noble wants a tamed dragon—can you deliver?
- Disease outbreak threatens your stock
- Rare egg discovered—what hatches?
- Monster rights activists protest your ranch

### Seasonal Events

- **Spring Hatching**: Egg drop rates increased
- **Summer Grazing**: Pasture monsters produce more
- **Autumn Harvest**: Processing yields doubled
- **Winter Survival**: Resource management challenge

---

## Market & Economy

### Buyers

| Buyer Type        | Wants                 | Pays      | Special                  |
| ----------------- | --------------------- | --------- | ------------------------ |
| **General Store** | Common materials      | Low       | Always available         |
| **Alchemist**     | Monster parts, fluids | Medium    | Specific requests        |
| **Blacksmith**    | Scales, bones, hide   | Medium    | Bulk orders              |
| **Wizard**        | Magical components    | High      | Rare materials only      |
| **Collector**     | Live specimens        | Very High | Specific traits required |
| **Arena**         | Combat-ready beasts   | Variable  | Stats matter             |

### Commission System

- Special orders with deadlines
- Higher rewards for meeting specifications
- Reputation affects available commissions

---

## Synergies with Other Scenarios

- **Adventurer Guild**: Adventurers capture monsters for you; you supply them with combat beasts
- **Alchemy**: Monster materials are key ingredients; alchemist products help ranching
- **Inn/Tavern**: Exotic monster cuisine attracts guests; inn leftovers become feed
- **Territory**: Ranch can expand into territory; territory monsters can be captured

---

## Sample Play Session

**Morning Routine (5 min)**

- Collect overnight passive harvests: 200 slime gel, 50 beast pelts
- Check breeding pen: Twins born! (Rare occurrence)
- Feed rounds: Auto-feeders handled most, manually feed dragons for affection
- Review health: One sick monster → apply treatment

**Midday Management (5 min)**

- Process materials: Queue 100 gel → alchemical base
- Fill commission: Deliver 20 A-grade scales for 5,000 gold
- Plan breeding: Pair high-yield beasts for next generation
- Capture expedition: Send trappers for reported rare spawn

**Evening Active Play (15 min)**

- Training minigame: Boost young dragon's stats
- Optimal harvest: Catch phoenix feather drop at peak quality
- Affection time: Groom prized unicorn (unlocks special harvest)
- Hatch rare egg: Choose incubation bonuses, wait for result

**Overnight (idle)**

- Monsters feed and rest
- Passive harvesting continues
- Breeding gestation progresses
- Orders auto-fill from stockpile
- Trappers return with captures
