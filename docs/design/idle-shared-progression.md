---
type: system
scope: detailed
status: draft
version: 1.0.0
created: 2026-02-02
updated: 2026-02-02
subjects: [game-design, progression, systems]
dependencies: []
---

# Shared Progression Systems - Design Document

## Overview

This document defines the unified progression mechanics that span all scenarios, creating cohesive advancement while respecting each scenario's identity.

---

## Player Level System

### Universal Experience

All activities contribute to Player Level:

```
Player XP = Σ(Scenario XP × Scenario Weight × Synergy Multiplier)
```

### XP Sources by Scenario

| Scenario  | Primary XP Activities                                      |
| --------- | ---------------------------------------------------------- |
| Inn       | Guests served, reputation gained, events completed         |
| Guild     | Quests completed, adventurer level-ups                     |
| Farm      | Harvests, breeding successes, evolutions                   |
| Alchemy   | Potions brewed, recipes discovered                         |
| Territory | Buildings constructed, population growth, threats defeated |
| Summoner  | Contracts formed, evolutions, trust milestones             |
| Caravan   | Deliveries completed, profit margins, routes discovered    |

### Player Level Benefits

| Level  | Unlock                              |
| ------ | ----------------------------------- |
| 1-10   | Tutorial complete, basic features   |
| 11-25  | Second scenario unlock available    |
| 26-50  | Third scenario, advanced features   |
| 51-75  | Fourth scenario, prestige options   |
| 76-100 | All scenarios, master features      |
| 101+   | Infinite scaling, cosmetic prestige |

### Level-Up Rewards

Each level grants:

- **Skill Point**: Allocate to universal skills
- **Scenario Token**: Spend in any active scenario
- **Occasional**: Premium currency, cosmetics, special unlocks

---

## Universal Skill Tree

### Skill Categories

Player skill points invest in cross-scenario abilities:

#### Management Branch

| Skill                     | Effect                            | Max Rank |
| ------------------------- | --------------------------------- | -------- |
| **Efficient Operations**  | -2% upkeep costs per rank         | 10       |
| **Rapid Construction**    | -3% build time per rank           | 10       |
| **Staff Mastery**         | +5% NPC efficiency per rank       | 10       |
| **Resource Conservation** | -2% resource consumption per rank | 10       |

#### Commerce Branch

| Skill                  | Effect                                         | Max Rank |
| ---------------------- | ---------------------------------------------- | -------- |
| **Shrewd Bargaining**  | +2% buy/sell margins per rank                  | 10       |
| **Market Insight**     | Price information updates +10% faster per rank | 10       |
| **Bulk Dealing**       | +3% bulk transaction efficiency per rank       | 10       |
| **Reputation Builder** | +5% reputation gain per rank                   | 10       |

#### Combat Branch

| Skill                  | Effect                               | Max Rank |
| ---------------------- | ------------------------------------ | -------- |
| **Tactical Wisdom**    | +2% combat success per rank          | 10       |
| **Defensive Planning** | +3% defense effectiveness per rank   | 10       |
| **Recovery Speed**     | +5% healing/rest speed per rank      | 10       |
| **Risk Assessment**    | Better encounter prediction per rank | 10       |

#### Production Branch

| Skill                  | Effect                               | Max Rank |
| ---------------------- | ------------------------------------ | -------- |
| **Quality Focus**      | +2% quality chance per rank          | 10       |
| **Yield Optimization** | +2% production yield per rank        | 10       |
| **Process Efficiency** | -3% production time per rank         | 10       |
| **Waste Reduction**    | +5% failed attempt recovery per rank | 10       |

#### Mastery Branch (Unlocks at Player Level 50)

| Skill                | Effect                         | Max Rank |
| -------------------- | ------------------------------ | -------- |
| **Scenario Synergy** | +2% synergy bonuses per rank   | 5        |
| **Prestige Wisdom**  | +5% legacy point gain per rank | 5        |
| **Time Dilation**    | +5% offline progress per rank  | 5        |
| **Fortune's Favor**  | +2% rare outcomes per rank     | 5        |

---

## Achievement System

### Achievement Categories

#### Scenario Milestones

Each scenario has tiered achievements:

**Inn Example:**

- Bronze: Serve 100 guests
- Silver: Serve 1,000 guests
- Gold: Serve 10,000 guests
- Platinum: Serve 100,000 guests
- Diamond: Serve 1,000,000 guests

#### Cross-Scenario Achievements

Require activity across multiple scenarios:

| Achievement               | Requirement                        | Reward                 |
| ------------------------- | ---------------------------------- | ---------------------- |
| **Diversified Portfolio** | Reach level 10 in 3 scenarios      | +10% XP globally       |
| **Jack of All Trades**    | Reach level 25 in 5 scenarios      | Unique title, cosmetic |
| **Master of All**         | Reach level 50 in all scenarios    | Legendary unlock       |
| **The First Loop**        | Complete prestige in any scenario  | Prestige currency      |
| **Full Circle**           | Complete prestige in all scenarios | Ultimate cosmetic set  |

#### Hidden Achievements

Discovered through play:

| Achievement             | Hidden Requirement                     | Reward              |
| ----------------------- | -------------------------------------- | ------------------- |
| **Unexpected Guest**    | Serve a dragon at your inn             | Dragon-themed décor |
| **Ethical Merchant**    | Complete 100 trades without contraband | Reputation bonus    |
| **Peaceful Resolution** | Resolve invasion through diplomacy     | Unique advisor NPC  |
| **The Long Game**       | Play for 365 consecutive days          | Anniversary rewards |

### Achievement Points

- Each achievement grants points
- Points unlock cosmetic tiers
- Leaderboard rankings (optional)
- Points persist through prestige

---

## Codex System

### Knowledge Collection

A shared encyclopedia built through play:

#### Creature Codex

| Entry Source            | Information Unlocked       |
| ----------------------- | -------------------------- |
| Farm: Raise creature    | Biology, care requirements |
| Guild: Defeat creature  | Combat stats, loot tables  |
| Alchemy: Use parts      | Alchemical properties      |
| Summoner: Bind creature | Summoning requirements     |

**Completion Bonus**: Full entry for a creature grants bonuses when interacting with it across all scenarios.

#### Location Codex

| Entry Source              | Information Unlocked |
| ------------------------- | -------------------- |
| Territory: Control region | Resources, terrain   |
| Caravan: Trade there      | Prices, demand       |
| Guild: Quest there        | Dangers, dungeons    |
| Inn: Guests from there    | Culture, rumors      |

**Completion Bonus**: Full location entry improves all activities in that region.

#### NPC Codex

| Entry Source          | Information Unlocked    |
| --------------------- | ----------------------- |
| Any: Meet NPC         | Basic info, preferences |
| Multiple interactions | Backstory, secrets      |
| Quest completion      | Full history            |

**Completion Bonus**: NPCs with full entries offer better deals, more quests, special dialogue.

#### Recipe/Blueprint Codex

Unified crafting knowledge:

- Alchemy recipes
- Territory building blueprints
- Farm breeding combinations
- Summoner ritual circles
- Caravan route maps

---

## Seasonal Content System

### Season Structure

Each real-world season brings:

| Component          | Duration       | Content                             |
| ------------------ | -------------- | ----------------------------------- |
| **Season Theme**   | 3 months       | Aesthetic changes, themed events    |
| **Season Pass**    | 3 months       | Progression track with rewards      |
| **Limited Events** | 1-2 weeks each | Special challenges, exclusive items |
| **Season Story**   | Ongoing        | Narrative content across scenarios  |

### Season Pass Tracks

#### Free Track

Available to all players:

- Basic resources
- Cosmetic samples
- Season currency
- One exclusive item

#### Premium Track

Optional purchase:

- Enhanced resources
- Full cosmetic sets
- Exclusive scenarios content
- Bonus season currency
- Unique title/badge

### Season Rewards Distribution

Rewards spread across scenarios:

| Track Level | Reward Type       | Example                   |
| ----------- | ----------------- | ------------------------- |
| 1-10        | Universal         | Gold, XP boosters         |
| 11-20       | Inn focused       | Themed decorations        |
| 21-30       | Guild focused     | Unique adventurer skin    |
| 31-40       | Farm focused      | Seasonal creature variant |
| 41-50       | Alchemy focused   | Limited recipes           |
| 51-60       | Territory focused | Special building          |
| 61-70       | Summoner focused  | Seasonal entity           |
| 71-80       | Caravan focused   | Themed caravan skin       |
| 81-100      | Universal premium | Legendary rewards         |

---

## Daily/Weekly Systems

### Daily Activities

Refreshing content encouraging regular play:

| Activity           | Scenario  | Reward                        |
| ------------------ | --------- | ----------------------------- |
| **Daily Special**  | Inn       | Bonus gold from featured dish |
| **Urgent Quest**   | Guild     | Extra XP, rare loot chance    |
| **Feeding Round**  | Farm      | Affection bonus               |
| **Daily Brew**     | Alchemy   | Free rare ingredient          |
| **Census**         | Territory | Population bonus              |
| **Daily Offering** | Summoner  | Trust boost for all summons   |
| **Market Tip**     | Caravan   | Revealed profitable route     |

### Weekly Challenges

Larger objectives across scenarios:

| Challenge Type | Example                  | Reward                 |
| -------------- | ------------------------ | ---------------------- |
| **Production** | Brew 500 potions         | Premium currency       |
| **Combat**     | Complete 50 quests       | Rare equipment         |
| **Collection** | Harvest 1000 materials   | Unique cosmetic        |
| **Social**     | Serve 200 guests         | Reputation boost       |
| **Economic**   | Earn 100,000 gold profit | Gold multiplier (temp) |

### Monthly Goals

Long-term objectives:

| Goal               | Requirement                     | Reward                      |
| ------------------ | ------------------------------- | --------------------------- |
| **Scenario Focus** | Max progression in one scenario | Scenario-specific legendary |
| **Broad Growth**   | Level all scenarios             | Universal legendary         |
| **Prestige Push**  | Complete any prestige           | Bonus prestige currency     |
| **Community**      | Participate in all events       | Exclusive title             |

---

## Milestone Rewards

### First-Time Bonuses

One-time rewards for reaching milestones:

| Milestone               | Reward                  |
| ----------------------- | ----------------------- |
| First scenario level 10 | 500 premium currency    |
| First scenario level 25 | Unique cosmetic         |
| First scenario level 50 | Legendary item          |
| First prestige          | Prestige portrait frame |
| All scenarios unlocked  | "Polymath" title        |
| First synergy activated | Synergy guide unlock    |

### Cumulative Milestones

Rewards for total progress:

| Total Levels | Reward                 |
| ------------ | ---------------------- |
| 50 combined  | Resource boost package |
| 100 combined | Automation unlock      |
| 200 combined | Speed boost permanent  |
| 350 combined | Ultimate cosmetic set  |

---

## Statistics & Tracking

### Global Statistics

Tracked across all scenarios:

| Statistic                  | Tracks                    |
| -------------------------- | ------------------------- |
| **Total Playtime**         | Hours active              |
| **Idle Time**              | Hours generating offline  |
| **Gold Earned (Lifetime)** | All gold ever generated   |
| **Gold Spent (Lifetime)**  | All gold ever spent       |
| **Quests Completed**       | Guild quests total        |
| **Creatures Raised**       | Farm lifetime count       |
| **Potions Brewed**         | Alchemy lifetime count    |
| **Regions Controlled**     | Territory high water mark |
| **Contracts Formed**       | Summoner lifetime count   |
| **Trade Profit**           | Caravan lifetime earnings |
| **Guests Served**          | Inn lifetime count        |

### Per-Session Statistics

For recent activity review:

- Today's earnings by scenario
- Today's XP by scenario
- Active time vs idle time
- Best single transaction
- Rare events encountered

---

## Related Documents

- **[Idle Game Overview](idle-game-overview.md)** — Overview of the game and its progression philosophy
- **[Meta-Game Integration Layer](idle-meta-integration.md)** — How scenarios share resources and contribute to universal progression
- **[Prestige Framework](idle-prestige-framework.md)** — How prestige interacts with shared progression
- **[Idle Inn/Tavern Management](idle-inn-gdd.md)** — Core GDD with tag-based class and skill systems
- **[The Wandering Inn: Class Tag Mapping](twi-classes.md)** — Tag taxonomy that defines progression paths
- **[The Wandering Inn: Skill Tag Mapping](twi-skills.md)** — Skill system integrated with universal progression
- **[Adventurer Guild](idle-adventurer-guild.md)** — Example of scenario-specific progression feeding into universal systems

### Comparative Statistics

Optional comparisons:

- Friend leaderboards
- Global percentiles
- Personal bests
- Efficiency metrics
