# Alchemy/Potion Brewing - Idle Game Design Document

## Core Fantasy
You are an [Alchemist] mastering the transformation of mundane ingredients into magical elixirs, potions, and transmuted materials. From a humble workshop brewing health potions, you'll grow to create legendary concoctions that can raise the dead, grant immortality, or turn lead into gold. This scenario emphasizes recipe discovery, production optimization, and the satisfaction of complex crafting chains.

---

## Primary Game Loop

```
GATHER → BREW → DISCOVER → OPTIMIZE → SELL/USE → EXPAND → GATHER (better)
```

1. **Gather Ingredients** (multiple sources, semi-idle)
   - Herb garden (passive growth)
   - Foraging expeditions (timed)
   - Monster parts (purchase or synergy)
   - Mineral extraction (passive)
   - Merchant purchases (active)

2. **Brew Potions** (queue-based, idle processing)
   - Select recipe
   - Assign ingredients
   - Wait brewing time
   - Collect results

3. **Discover Recipes** (active experimentation)
   - Combine unknown ingredients
   - Analyze successful combinations
   - Unlock advanced formulas through progression

4. **Optimize Production** (active management)
   - Upgrade equipment for speed/quality
   - Train assistants
   - Batch processing
   - Quality control

5. **Sell or Use Products** (multiple outlets)
   - Shop front (passive sales)
   - Commissions (specific orders)
   - Personal use (buffs for other activities)
   - Trade with other scenarios

---

## Resource Systems

### Primary Resources
| Resource | Generation | Primary Use |
|----------|------------|-------------|
| **Gold** | Sales, commissions | Ingredients, equipment |
| **Ingredients** | Gathering, purchase | Brewing |
| **Mana Essence** | Distillation, collection | Advanced recipes |
| **Research Points** | Experimentation, discoveries | Recipe unlocks |

### Ingredient Categories
| Category | Examples | Source | Rarity Range |
|----------|----------|--------|--------------|
| **Herbs** | Moonpetal, Fireblossom, Nightshade | Garden, foraging | Common-Rare |
| **Minerals** | Sulfite, Mercury, Mithril dust | Mining, purchase | Common-Epic |
| **Monster Parts** | Slime core, Dragon blood, Phoenix ash | Purchase, synergy | Uncommon-Legendary |
| **Essences** | Fire, Water, Life, Death | Extraction, events | Rare-Legendary |
| **Catalysts** | Philosopher's salt, Void crystal | Crafted, found | Epic-Legendary |

---

## Recipe System

### Recipe Discovery
Three paths to new recipes:
1. **Experimentation**: Combine ingredients, chance of discovery
2. **Research**: Spend Research Points to unlock known recipes
3. **Acquisition**: Find recipe scrolls, learn from NPCs, quest rewards

### Recipe Tiers
| Tier | Examples | Brewing Time | Complexity |
|------|----------|--------------|------------|
| **Apprentice** | Health Potion, Stamina Tonic | Minutes | 2-3 ingredients |
| **Journeyman** | Strength Elixir, Invisibility Draught | Hours | 3-4 ingredients |
| **Expert** | Polymorph Potion, Elemental Resistance | Hours | 4-5 ingredients |
| **Master** | Resurrection Elixir, Time Stop Potion | Days | 5-7 ingredients |
| **Legendary** | Philosopher's Stone, Immortality Serum | Weeks | 7+ ingredients |

### Recipe Quality Factors
Final potion quality (F to SS) determined by:
- Ingredient quality
- Equipment level
- Alchemist skill
- Optional: Active brewing minigame

### Recipe Examples
```
HEALTH POTION (Apprentice)
├─ Red Herb (1)
├─ Purified Water (1)
└─ Vitality Moss (1)
Brewing Time: 5 minutes
Effect: Restores 100 HP

GREATER INVISIBILITY (Expert)
├─ Chameleon Scale (2)
├─ Moonpetal Extract (1)
├─ Void Essence (1)
├─ Distilled Mana (2)
└─ Catalyst: Shadow Salt
Brewing Time: 4 hours
Effect: Complete invisibility for 30 minutes
```

---

## Workshop & Equipment

### Brewing Stations
| Station | Function | Queue Size | Speed |
|---------|----------|------------|-------|
| **Basic Cauldron** | Apprentice recipes | 1 | 1x |
| **Alembic Setup** | Distillation, extracts | 1 | 1x |
| **Enchanted Cauldron** | Up to Expert | 3 | 1.5x |
| **Master's Laboratory** | All recipes | 5 | 2x |
| **Arcane Apparatus** | Legendary, experiments | 10 | 3x |

### Support Facilities
| Facility | Function | Idle Benefit |
|----------|----------|--------------|
| **Ingredient Storage** | Capacity, preservation | Prevents spoilage |
| **Herb Garden** | Grows basic herbs | Passive ingredients |
| **Greenhouse** | Grows rare herbs | Better passive ingredients |
| **Mineral Extractor** | Processes ore | Passive minerals |
| **Essence Condenser** | Collects ambient mana | Passive mana essence |

### Equipment Upgrades
- **Purity**: Increases quality floor
- **Speed**: Reduces brewing time
- **Capacity**: More simultaneous brews
- **Efficiency**: Reduces ingredient consumption
- **Discovery**: Increases experimentation success

---

## Production Optimization

### Batch Processing
- Queue multiple of same recipe
- Bulk discounts on brewing time
- Requires sufficient ingredients upfront

### Assistant System
Hire and train assistants:
- **Gatherer**: Improves foraging yields
- **Brewer's Apprentice**: Handles simple recipes
- **Quality Inspector**: Increases average quality
- **Researcher**: Generates passive Research Points

### Automation Tiers
1. **Manual**: Select each brew individually
2. **Queue**: Set up brewing queue
3. **Auto-Restock**: Automatically brew when stock low
4. **Smart Production**: AI optimizes based on demand

---

## Prestige System: "Magnum Opus"

### Trigger Conditions
- Create a Philosopher's Stone
- Discover all recipes in a tier
- Achieve SS quality on a Legendary potion

### Reset Mechanics
- Workshop resets to basic equipment
- Recipes locked (re-discovery faster)
- Retain: Alchemist level, research notes percentage
- Gain: "Alchemical Wisdom" points

### Legacy Bonuses
- "Intuitive Brewer" - Recipe discovery chance increased
- "Efficient Processes" - Permanent brewing time reduction
- "Quality Foundation" - Higher minimum quality
- "Rare Insight" - Monster/Essence ingredients more effective

---

## Active vs. Idle Balance

### Idle Mechanics
- Brewing queues process automatically
- Gardens and extractors produce
- Shop sells from inventory
- Assistants work at reduced efficiency

### Active Engagement Rewards
- **Brewing Minigame**: Stir, heat control, timing for quality bonus
- **Optimal Harvesting**: Gather herbs at peak potency
- **Experimentation**: Manual combination for discoveries
- **Flash Sales**: Limited-time high-value orders
- **Quality Inspection**: Catch impurities for bonus
- **Ingredient Trading**: Negotiate better prices

### Discovery System
- Experiment by combining 2-5 ingredients
- Success: New recipe learned + sample product
- Failure: Ingredients lost (partial recovery possible)
- Near-miss: Hints toward correct combination

---

## Market & Sales

### Sales Channels
| Channel | Volume | Price | Effort |
|---------|--------|-------|--------|
| **Shop Front** | Passive | Base | None |
| **Adventurer Supply** | Medium | Good | Light |
| **Noble Contracts** | Low | Premium | Medium |
| **Wholesale** | High | Discount | Light |
| **Black Market** | Variable | High | Risk |

### Demand Fluctuation
- Seasons affect what's needed
- Events create surges (war = health potions)
- Reputation unlocks better buyers
- Rare potions always in demand

### Commission System
Specific orders with bonuses:
- "50 Health Potions, B-grade or better, by tomorrow"
- "One Resurrection Elixir for the Duke's son"
- "Experimental potion matching these symptoms"

---

## Event System

### Regular Events
- **Ingredient Bloom**: Rare herbs available
- **Alchemist's Convention**: Recipe trading, competitions
- **Epidemic**: Health potion demand spikes

### Story Events
- A plague requires inventing a cure
- Noble poisoned—create antidote under pressure
- Discover forbidden recipes with moral implications
- Rival alchemist challenges your expertise

### Seasonal Events
- **Spring Growth**: Garden yields doubled
- **Summer Distillation**: Essence collection boosted
- **Autumn Harvest**: All ingredients abundant
- **Winter Brewing**: Indoor production bonuses

---

## Synergies with Other Scenarios

- **Monster Farm**: Monster materials as ingredients; potions improve monster stats
- **Adventurer Guild**: Supply adventurers; quest rewards include rare ingredients
- **Inn/Tavern**: Specialty drinks and tonics; innkeeper sells your potions
- **Territory**: Claim resource-rich lands; territory buildings provide ingredients

---

## Sample Play Session

**Morning (5 min)**
- Collect overnight production: 50 health potions, 20 mana elixirs
- Harvest herb garden: Peak potency bonus on moonpetals
- Check shop: 2,400 gold from overnight sales
- Review commission board: Accept high-value antidote order

**Midday Experimentation (10 min)**
- Experimentation session: Combine new ingredients
- First attempt: Failure, but got hint
- Second attempt: Success! New recipe discovered
- Queue new recipe for production

**Evening Production (5 min)**
- Optimize queues: Prioritize commission order
- Brewing minigame: Perfect timing on expert potion (quality boost)
- Restock shop inventory
- Plan overnight: Set up 8-hour legendary brew

**Overnight (idle)**
- Brewing queues process
- Gardens grow
- Shop continues sales
- Legendary potion brewing completes by morning
