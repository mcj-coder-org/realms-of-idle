# Territory/Domain Building - Idle Game Design Document

## Core Fantasy
You are a [Lord], [Chieftain], or [Dungeon Master] developing a domain from untamed wilderness into a thriving realm. Claim land, build settlements, manage population, and defend against threats while your domain grows in power and influence. This scenario combines city-building, resource management, and strategic expansion into an idle-compatible framework.

---

## Primary Game Loop

```
CLAIM → BUILD → POPULATE → PRODUCE → EXPAND → DEFEND → CLAIM (larger)
```

1. **Claim Territory** (active selection)
   - Scout adjacent regions
   - Spend resources to claim
   - Each region has unique features (resources, hazards, bonuses)

2. **Build Infrastructure** (queue-based, idle construction)
   - Place buildings in regions
   - Construction takes time
   - Buildings produce resources or provide services

3. **Populate & Assign** (semi-active management)
   - Attract settlers based on prosperity
   - Assign workers to buildings
   - Balance population needs vs. production

4. **Produce Resources** (idle core)
   - Buildings generate resources over time
   - Workers increase efficiency
   - Resources stockpile for use

5. **Expand & Upgrade** (active investment)
   - Upgrade buildings for better output
   - Unlock new building types
   - Expand into new regions

6. **Defend Territory** (active during threats, passive otherwise)
   - Build defenses
   - Station guards/troops
   - Respond to invasions and events

---

## Resource Systems

### Primary Resources
| Resource | Generation | Primary Use |
|----------|------------|-------------|
| **Gold** | Taxes, trade, tribute | Construction, wages, purchases |
| **Food** | Farms, hunting, fishing | Population sustenance |
| **Wood** | Lumber camps, forests | Construction, fuel |
| **Stone** | Quarries, mines | Construction, fortification |
| **Iron** | Mines, trade | Equipment, advanced buildings |
| **Mana** | Ley lines, towers | Magic buildings, enchantments |

### Population Resources
- **Population**: Total inhabitants (growth based on food/housing)
- **Workers**: Assigned population producing resources
- **Soldiers**: Assigned population for defense
- **Specialists**: Skilled workers for advanced buildings
- **Happiness**: Affects productivity and growth

### Advanced Resources (Unlocked)
- Gems, Rare Metals, Enchanted Materials, Knowledge, Influence

---

## Region System

### Region Types
| Type | Features | Best For |
|------|----------|----------|
| **Plains** | Fertile, easy building | Farms, settlements |
| **Forest** | Wood, wildlife | Lumber, hunting |
| **Mountains** | Minerals, defensible | Mining, fortresses |
| **Wetlands** | Fish, herbs | Fishing, alchemy |
| **Desert** | Gems, ancient ruins | Mining, exploration |
| **Coastal** | Trade, fish | Ports, fishing |
| **Magical** | Mana, rare materials | Magic production |

### Region Attributes
- **Size**: How many building slots
- **Fertility**: Food production bonus
- **Mineral Wealth**: Mining yields
- **Defensibility**: Combat bonuses
- **Special Features**: Unique resources or effects

### Scouting & Claiming
1. Send scouts to reveal adjacent regions (time-based)
2. Review region details
3. Spend Influence + resources to claim
4. Initial development unlocks building

---

## Building System

### Building Categories

**Production Buildings**
| Building | Produces | Workers | Region Preference |
|----------|----------|---------|-------------------|
| Farm | Food | 2-10 | Plains |
| Lumber Camp | Wood | 2-8 | Forest |
| Quarry | Stone | 3-10 | Mountains |
| Mine | Iron, Gems | 4-15 | Mountains |
| Fishing Dock | Food | 2-6 | Coastal, Wetlands |
| Mana Well | Mana | 1-3 | Magical |

**Population Buildings**
| Building | Function | Capacity |
|----------|----------|----------|
| Cottage | Housing | 4 pop |
| House | Housing | 10 pop |
| Manor | Housing + happiness | 25 pop |
| Inn | Attracts settlers | Varies |
| Temple | Happiness, healing | N/A |
| School | Creates specialists | 5 students |

**Military Buildings**
| Building | Function | Strength |
|----------|----------|----------|
| Watchtower | Early warning | Low |
| Barracks | Trains soldiers | Medium |
| Fortress | Defense hub | High |
| Walls | Region protection | Passive |

**Special Buildings**
| Building | Function | Requirements |
|----------|----------|--------------|
| Market | Enables trade | Population threshold |
| Workshop | Crafting | Specialists |
| Mage Tower | Magic production | Mana, knowledge |
| Monument | Major bonuses | Massive resources |

### Building Upgrades
Each building has upgrade tiers (I → II → III → IV → V):
- Higher production
- More worker slots
- Reduced upkeep
- Special abilities at max tier

---

## Population Management

### Population Growth
Growth formula based on:
- Available food surplus
- Available housing
- Happiness level
- Immigration from events/buildings

### Worker Assignment
- Drag population to buildings
- Each building has min/max workers
- More workers = more production (diminishing returns)
- Specialists required for advanced buildings

### Happiness Factors
| Positive | Negative |
|----------|----------|
| Food surplus | Food shortage |
| Housing quality | Overcrowding |
| Temples, entertainment | High taxes |
| Safety | Attacks, disasters |
| Prosperity | Unemployment |

### Population Types
- **Peasants**: Basic workers
- **Craftsmen**: Production specialists
- **Scholars**: Research and magic
- **Soldiers**: Defense
- **Nobles**: Administration bonuses

---

## Prestige System: "New Dynasty"

### Trigger Conditions
- Control 50+ regions
- Build a Wonder
- Defeat a major invasion
- Achieve maximum domain level

### Reset Mechanics
- Territory resets to starting region
- Population disperses
- Retain: Ruler skills, technology unlocks, monument blueprints
- Gain: "Legacy Power" points

### Legacy Bonuses
- "Founder's Blood" - Starting region has bonus resources
- "Remembered Prosperity" - Faster initial population growth
- "Ancient Roads" - Reduced expansion costs
- "Legendary Builders" - Construction time reduced

---

## Active vs. Idle Balance

### Idle Mechanics
- Resources accumulate (caps apply)
- Population grows (if conditions met)
- Buildings produce automatically
- Basic defense is automated
- Scouts explore assigned regions

### Active Engagement Rewards
- **Strategic Placement**: Manual building placement optimizes synergies
- **Crisis Response**: Handle disasters, invasions for bonus rewards
- **Diplomacy**: Negotiate with neighbors for trade/peace
- **Development Planning**: Set production priorities
- **Exploration**: Personally explore ruins for treasures
- **Festival Events**: Organize events for happiness/bonuses

### Automation Options
- Auto-assign workers (less efficient)
- Auto-build queue
- Auto-defend (no special tactics)
- Auto-expand (follows preset priorities)

---

## Combat & Defense

### Threat Types
| Threat | Frequency | Strength | Warning |
|--------|-----------|----------|---------|
| Bandits | Common | Low | Short |
| Monster Raid | Regular | Medium | Medium |
| Rival Lord | Uncommon | High | Long |
| Dragon | Rare | Extreme | Long |
| Demon Invasion | Event | Catastrophic | Story |

### Defense Mechanics
- Military buildings provide defense score
- Soldiers add to defense
- Walls multiply defensive strength
- Heroes can lead defense (synergy with Guild)

### Combat Resolution
When attack occurs:
- Compare attacker strength vs. defense score
- Modify by terrain, fortifications, leadership
- Resolve with damage to buildings/population or victory

### Active Defense Bonus
If online during attack:
- Tactical options (focus defense, counterattack, evacuate)
- Bonus from personal involvement
- Reduced losses from quick response

---

## Event System

### Regular Events
- **Merchant Caravan**: Trade opportunity
- **Settlers Arrive**: Population boost
- **Resource Discovery**: New deposits found
- **Building Opportunity**: Discounted construction

### Crisis Events
- **Famine**: Food production disrupted
- **Plague**: Population loss, happiness drop
- **Natural Disaster**: Building damage
- **Invasion**: Military threat

### Story Events
- Neighboring lord offers alliance or demands tribute
- Ancient ruins discovered—expedition opportunity
- Dragon claims mountain region—drive it out or negotiate
- Refugees seek shelter—accept for population or refuse

### Seasonal Cycles
- **Spring**: Planting bonuses, population growth
- **Summer**: Production bonuses, trade
- **Autumn**: Harvest, preparation
- **Winter**: Reduced production, increased consumption

---

## Synergies with Other Scenarios

- **Inn/Tavern**: Build an inn in your territory; inn attracts settlers
- **Adventurer Guild**: Guild operates from your domain; adventurers clear threats
- **Monster Farm**: Ranch occupies territory; provides monster materials
- **Alchemy**: Alchemist workshop in domain; territory herbs supply ingredients
- **Summoner**: Summoned creatures defend territory
- **Merchant Caravan**: Your caravans operate from territory; trade routes

---

## Progression Milestones

| Milestone | Unlock |
|-----------|--------|
| First Village (5 buildings) | Market, basic trade |
| Town Status (15 buildings) | Specialists, advanced buildings |
| City (30 buildings) | Mage tower, major trade |
| Regional Power (20 regions) | Diplomacy options |
| Kingdom (50 regions) | Wonder construction |
| Empire (100 regions) | Prestige available |

---

## Sample Play Session

**Morning Check (5 min)**
- Collect accumulated resources: 5,000 food, 2,000 wood, 800 stone
- Population grew by 12 overnight
- Assign new workers to understaffed mine
- Scout report: Rich forest region available

**Midday Management (10 min)**
- Claim new forest region (spend resources)
- Queue lumber camp construction (4 hours)
- Upgrade main farm to level 3
- Review happiness: Build temple to offset recent tax increase

**Evening Active Play (15 min)**
- Invasion warning: Orc raiders approaching
- Reinforce border fortress
- Move soldiers to threatened region
- Combat event: Choose tactics (defend walls)
- Victory: Minimal losses, bonus loot from raiders
- Story choice: Captured orc offers information for freedom

**Overnight (idle)**
- Resources accumulate
- Construction completes
- Population grows
- Scouts continue exploration
- Defense holds (no attacks)
