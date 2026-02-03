---
type: scenario
scope: detailed
status: draft
version: 1.0.0
created: 2026-02-02
updated: 2026-02-02
subjects: [management, strategic, combat]
dependencies: [idle-game-overview]
---

# Adventurer Guild - Idle Game Design Document

## Core Fantasy

You are a [Guildmaster] managing a growing organization of adventurers. Recruit heroes, dispatch them on quests, manage resources, and build your guild's reputation until it becomes the premier force against the world's threats. This scenario emphasizes team management, strategic deployment, and watching your recruits grow from novices to legends.

---

## Primary Game Loop

```
RECRUIT → EQUIP → DISPATCH → WAIT → COLLECT → IMPROVE → RECRUIT (better)
```

1. **Recruit Adventurers** (active selection with passive applications)
   - Review applicants (random generation with traits, classes, potential)
   - Spend recruitment resources or reputation to attract better candidates
   - Balance party composition needs

2. **Equip & Prepare** (active management)
   - Assign gear from guild armory
   - Form parties with complementary skills
   - Apply consumables (potions, scrolls, rations)

3. **Dispatch on Quests** (active selection)
   - Match party strength to quest difficulty
   - Choose risk/reward balance
   - Set duration (longer = better rewards but ties up adventurers)

4. **Quest Progress** (idle core)
   - Parties progress through quest stages automatically
   - Encounters resolved based on party stats, gear, and RNG
   - Status updates available but not required

5. **Collect Results** (active review)
   - Loot distribution
   - Experience gains
   - Reputation changes
   - Potential injuries, deaths, or retirements

6. **Improve & Expand**
   - Level up adventurers
   - Upgrade guild facilities
   - Unlock new quest types and regions

---

## Resource Systems

### Primary Resources

| Resource         | Generation                   | Primary Use                        |
| ---------------- | ---------------------------- | ---------------------------------- |
| **Gold**         | Quest rewards, contracts     | Wages, equipment, facilities       |
| **Reputation**   | Successful quests, events    | Unlocks quests, recruits, regions  |
| **Guild Points** | Daily operations, milestones | Facility upgrades, special unlocks |
| **Materials**    | Quest loot, salvage          | Crafting, upgrades                 |

### Adventurer Resources

- **Health**: Depleted by quests, regenerates over time or via healing
- **Morale**: Affected by success/failure, wages, facilities
- **Experience**: Accumulated toward level-ups
- **Fatigue**: Builds up over consecutive quests, requires rest

---

## Adventurer System

### Recruitment Pool

Applicants generated with:

- **Class**: [Warrior], [Mage], [Rogue], [Healer], [Ranger], etc.
- **Rarity**: Common → Uncommon → Rare → Epic → Legendary
- **Traits**: Brave, Cautious, Greedy, Loyal, Lucky, etc.
- **Potential**: Growth rate cap (S/A/B/C/D ranks)
- **Starting Level**: Based on guild reputation

### Class Archetypes

| Role       | Examples                              | Party Function                  |
| ---------- | ------------------------------------- | ------------------------------- |
| Tank       | [Knight], [Barbarian], [Paladin]      | Absorbs damage, protects others |
| DPS        | [Berserker], [Assassin], [Battlemage] | Primary damage dealer           |
| Support    | [Cleric], [Bard], [Enchanter]         | Healing, buffs, utility         |
| Control    | [Wizard], [Warlock], [Monk]           | Debuffs, crowd control          |
| Specialist | [Ranger], [Alchemist], [Summoner]     | Situational advantages          |

### Adventurer Progression

- Level up through quest experience
- Class evolution at milestone levels (10, 25, 50, 75, 100)
- Skill points allocated to personal abilities
- Equipment slots unlock with levels
- Retirement at max level converts to permanent guild bonuses

---

## Quest System

### Quest Types

| Type            | Duration    | Risk     | Reward    | Notes                                |
| --------------- | ----------- | -------- | --------- | ------------------------------------ |
| **Patrol**      | 1-2 hours   | Low      | Low       | Safe grinding, good for new recruits |
| **Subjugation** | 4-8 hours   | Medium   | Medium    | Monster hunting, standard fare       |
| **Exploration** | 12-24 hours | Variable | High      | Dungeon delving, discovery           |
| **Escort**      | 6-12 hours  | Medium   | Medium+   | Merchant/noble protection            |
| **Raid**        | 24-72 hours | High     | Very High | Multi-party coordinated assault      |
| **Expedition**  | 1-7 days    | Extreme  | Legendary | Major story events                   |

### Quest Difficulty Scaling

- **Threat Level**: 1-10 stars, compared against party power
- **Recommended Party Size**: 1-6 adventurers
- **Special Requirements**: Sometimes needs specific classes, items, or skills
- **Failure Conditions**: TPK, timeout, objective failed

### Quest Resolution

Quests divided into stages (e.g., 5 stages for a dungeon):

1. Travel → 2. Entry Encounter → 3. Exploration → 4. Boss → 5. Return

Each stage rolls against party stats:

- Success: Progress + loot
- Partial: Progress + reduced loot + injuries
- Failure: Retreat or continue at risk

---

## Guild Facilities

### Core Buildings

| Facility             | Function                   | Upgrade Benefits                   |
| -------------------- | -------------------------- | ---------------------------------- |
| **Guild Hall**       | Central hub, capacity      | More active adventurers            |
| **Barracks**         | Adventurer housing, rest   | Faster recovery, morale boost      |
| **Armory**           | Equipment storage          | More gear slots, quality bonus     |
| **Training Grounds** | Passive XP, skill practice | XP rate, training minigames        |
| **Infirmary**        | Heal injured adventurers   | Healing speed, revival chance      |
| **Tavern**           | Recruitment, morale        | Better applicants, morale recovery |

### Advanced Buildings (Unlocked via Progression)

- **Library**: Skill books, class change items
- **Forge**: Craft and upgrade equipment
- **Quest Board Expansion**: Access to more simultaneous quests
- **Embassy**: Inter-guild relations, special contracts
- **Hall of Legends**: Retired adventurer bonuses displayed

---

## Prestige System: "Guild Charter Renewal"

### Trigger Conditions

- Reach maximum guild rank
- Complete a "World Threat" questline
- Train an adventurer to max level and retire them as Guildmaster

### Reset Mechanics

- Guild resets to Rank 1
- Adventurers "graduate" to NPC status (may appear as allies/cameos)
- Retain: Guildmaster skills, blueprints, a percentage of materials
- Gain: "Legacy Tokens" for permanent unlocks

### Legacy Bonuses

- "Veteran Network" - Higher quality starting applicants
- "Established Reputation" - Base reputation in all regions
- "Inherited Armory" - Start with rare equipment
- "Training Doctrine" - All adventurers gain XP faster

---

## Active vs. Idle Balance

### Idle Mechanics

- Quests progress automatically
- Adventurers rest and recover
- Passive recruitment applications accumulate
- Training grounds provide trickle XP
- Gold from guild investments

### Active Engagement Rewards

- **Tactical Intervention**: During quest, spend resource to assist (bonus roll, emergency heal)
- **Recruitment Scouting**: Active minigame for better candidates
- **Training Supervision**: Direct training for accelerated XP
- **Party Optimization**: Manual team composition beats auto-assign
- **Contract Negotiation**: Better quest rewards through dialogue choices
- **Crisis Response**: Emergency quests with high rewards, limited time

### Notification System

- Quest completion
- Adventurer level-up
- Injury/death events
- Special applicants
- Limited-time quests

---

## Event System

### Regular Events

- **Monster Surge**: Increased quest availability, bonus rewards
- **Guild Competition**: Compete against other guilds (simulated) for rankings
- **Recruitment Drive**: Better candidates for limited time

### Story Events

- A [Demon Lord] army approaches—multi-phase defense
- A legendary adventurer requests to join—special requirements
- Guild politics: Rival guild conflict, alliance opportunities
- Ancient dungeon discovered—exclusive exploration rights

### Seasonal Events

- **Culling Season**: Monster population control, quantity over quality
- **Tournament Arc**: PvE competition with bracket progression
- **Expedition Season**: Long-duration high-reward quests available

---

## Party Composition Mechanics

### Synergy System

- Class combinations provide bonuses:
  - [Knight] + [Cleric] = "Bulwark" (defense boost)
  - [Rogue] + [Ranger] = "Ambush" (first strike bonus)
  - [Mage] + [Mage] + [Mage] = "Coven" (spell power surge)
- Trait interactions:
  - Multiple "Brave" = Morale bonus but reckless penalties
  - "Cautious" + "Brave" conflict = Efficiency penalty

### Role Requirements

Some quests require specific roles:

- Dungeon: Tank required
- Assassination: Rogue required
- Cursed location: Healer required
- Investigation: Specialist required

---

## Synergies with Other Scenarios

- **Inn/Tavern**: Your guild can be headquartered at a player-run inn; adventurers boost inn traffic
- **Monster Farm**: Captured monsters from quests can be sent to farm; farm products equip adventurers
- **Alchemy**: Guild alchemist produces potions for quests; rare ingredients from quests
- **Territory**: Guild can claim and defend territory; territory provides passive bonuses

---

## Sample Play Session

**Morning Check (3 min)**

- Collect 3 completed quests: 2,400 gold, 180 XP distributed, +8 reputation
- One adventurer leveled up—choose new skill
- Review injuries: One [Warrior] needs 4 hours recovery

**Midday Management (5 min)**

- 5 new applicants: Recruit promising [Ranger] with "Eagle Eye" trait
- Form new party for 8-hour dungeon quest
- Dispatch 2 patrol quests for lower-level adventurers
- Queue training for resting adventurers

**Evening Active Play (15 min)**

- Raid quest reaching boss stage—use Tactical Intervention (spend gold for damage boost)
- Special event: Guild Competition rankings—optimize party for tournament quest
- Story moment: Mysterious figure offers forbidden quest—moral choice
- Plan tomorrow: Set overnight expedition, ensure coverage

**Overnight (idle)**

- 3 quests progress
- Training continues
- Recovery completes
- New applicants generated
