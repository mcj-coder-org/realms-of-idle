---
title: Adventurer
gdd_ref: systems/class-system-gdd.md#specialization-classes
parent: classes/fighter/index.md
tree_tier: 2
---

# Adventurer

## Lore

### Origin

Adventurers defy simple categorization. They're not quite warriors - they avoid direct combat when possible. Not quite scouts - they go where scouts fear to tread. Not quite thieves - most operate legally. Adventurers are the people who explore ruins, delve dungeons, recover lost artifacts, map wilderness, and generally tackle dangerous jobs nobody else wants. They survive through versatility, adapting to whatever challenges arise.

The Adventurer archetype emerged from necessity. When ancient ruins needed exploring but no professional expedition could be mounted, desperate individuals tackled the job. Some sought treasure, others fame, still others simply couldn't settle into normal lives. Early Adventurers often died stupidly - triggering traps, fighting monsters poorly, starving in wilderness. Survivors learned lessons and passed them to others. Modern Adventurers benefit from accumulated wisdom: always carry rope, never split the party, if it seems too easy it's a trap.

The Adventurers' Guild functions more as mutual aid society than strict organization. Members share information about dangerous areas, trade equipment, and occasionally team up for particularly challenging delves. No formal training exists - Adventurers learn by doing, and those who learn poorly don't survive long enough to teach others. The Guild maintains a memorial wall listing fallen members, a sobering reminder that adventure comes with real risks.

### In the World

Adventurers occupy a unique social niche - romanticized in stories, viewed skeptically in reality. Children dream of becoming Adventurers, imagining heroic exploits and treasure hoards. Adults understand the truth: most Adventurers die young, broke, or both. Successful Adventurers become wealthy enough to retire, but success is rare. The profession attracts dreamers, desperados, and those who simply can't function in normal society.

Towns near ruins or dungeons develop love-hate relationships with Adventurers. They bring coin, spending freely on supplies and services. They also bring trouble - monsters following them, curses from disturbed tombs, unwanted attention from whatever lived in that ruin. Smart towns post clear rules: pay for damages, don't bring cursed items inside walls, dispose of monster parts outside settlement boundaries.

Adventuring parties form from necessity. Solitary Adventurers rarely last long - monsters overwhelm lone travelers, traps catch individuals off-guard, illness or injury becomes death sentence without help. Balanced parties include varied skills: someone who fights well, someone who spots traps, someone who bandages wounds, someone who negotiates with intelligent creatures. Trust matters deeply - you're trusting these people with your life in circumstances where betrayal is easy and profitable.

Apprentice Adventurers often join experienced parties as porters or hirelings, learning by observation. They watch veterans check for traps, handle negotiations, distribute loot fairly. They learn that glory matters less than survival, that retreat beats death, that treasure doesn't help if you're dead. The smart ones absorb lessons quickly. The others contribute to the Guild's memorial wall.

Successful Adventurers develop their own styles. Some focus on dungeon delving, becoming experts in traps and undead. Others specialize in wilderness survival, guiding expeditions through dangerous territory. Some hunt specific monster types, learning their weaknesses intimately. A few become generalists, adequately handling any situation. All share adaptability - when plans fail, Adventurers improvise.

---

## Mechanics

### Prerequisites

| Requirement        | Value                                                       |
| ------------------ | ----------------------------------------------------------- |
| XP Threshold       | 5,000 XP from exploration/survival tracked actions          |
| Related Foundation | [Fighter](../fighter/index.md) (optional, provides bonuses) |
| Tag Depth Access   | 2 levels (e.g., `Combat`, `Gathering`)                      |

### Requirements

| Requirement       | Value                                       |
| ----------------- | ------------------------------------------- |
| Unlock Trigger    | Explore dangerous areas, survive encounters |
| Primary Attribute | AWR (Awareness), END (Endurance)            |
| Starting Level    | 1                                           |
| Tools Required    | Various adventuring gear                    |

### Stats

#### Base Class Stats

| Level | HP Bonus | Stamina Bonus | Trait                 |
| ----- | -------- | ------------- | --------------------- |
| 1     | +8       | +18           | Apprentice Adventurer |
| 10    | +25      | +50           | Journeyman Adventurer |
| 25    | +50      | +105          | Master Adventurer     |
| 50    | +90      | +190          | Legendary Adventurer  |

#### Combat Bonuses

| Class Level | Damage Bonus | Defense Bonus | Survival Bonus | Versatility |
| ----------- | ------------ | ------------- | -------------- | ----------- |
| 1-9         | +0%          | +0%           | +0%            | +0%         |
| 10-24       | +10%         | +10%          | +15%           | +10%        |
| 25-49       | +25%         | +25%          | +35%           | +25%        |
| 50+         | +45%         | +45%          | +60%           | +50%        |

#### Starting Skills (Auto-Awarded on Class Acceptance)

Skills automatically awarded when accepting this class:

| Skill          | Tier   | Link                                                     | Reasoning                                         |
| -------------- | ------ | -------------------------------------------------------- | ------------------------------------------------- |
| Quick Reflexes | Lesser | [See Skill](../../skills/common/quick-reflexes/index.md) | Survival and adaptability in dangerous situations |

#### Synergy Skills

Skills with strong synergies for Adventurer:

- [Danger Sense](../../skills/common/danger-sense/index.md) - Lesser/Greater/Enhanced - Detect traps and ambushes with increasing awareness
- [Versatile Fighter](../../skills/index.md) - Lesser/Greater/Enhanced - Use any weapon with reduced penalty
- [Wilderness Survival](../../skills/common/index.md) - Lesser/Greater/Enhanced - Survive and thrive in outdoor environments
- [Quick Reflexes](../../skills/common/quick-reflexes/index.md) - Lesser/Greater/Enhanced - Increased dodge and initiative
- [Improvisation](../../skills/index.md) - Lesser/Greater/Enhanced - Use improvised tools and weapons effectively
- [Trap Detection](../../skills/index.md) - Lesser/Greater/Enhanced - Spot traps from common to magical
- [Resource Management](../../skills/index.md) - Lesser/Greater/Enhanced - Make supplies last longer
- [Monster Lore](../../skills/index.md) - Lesser/Greater/Enhanced - Know creature weaknesses
- [Lucky](../../skills/index.md) - Lesser/Greater/Enhanced - Chance to avoid death or disaster
- [Climber](../../skills/common/climber/index.md) - Lesser/Greater/Enhanced - Climb various surface types
- [Swimmer](../../skills/common/swimmer/index.md) - Lesser/Greater/Enhanced - Swim in different water conditions
- [Party Leader](../../skills/index.md) - Lesser/Greater/Enhanced - Lead party with increased effectiveness
- [Dungeon Delver](../../skills/index.md) - Lesser/Greater/Enhanced - Enhanced performance in enclosed areas
- [Adaptable](../../skills/index.md) - Lesser/Greater/Enhanced - Switch combat styles without penalty
- [Second Chances](../../skills/index.md) - No tiers - Survive lethal damage once per day

**Note**: All skills listed have strong synergies because they are core adventuring skills. Characters without Adventurer class can still learn these skills but progress at base rate (1x XP) without effectiveness bonuses.

#### Synergy Bonuses

Adventurer provides context-specific bonuses based on logical specialization:

**Core Adventuring Skills** (Direct specialization - Strong synergy):

- **Danger Sense**: Essential for detecting threats in dangerous environments
  - Faster learning: 2x XP from exploration and survival actions (scales with class level)
  - Better effectiveness: +25% detection accuracy at Adventurer 15, +35% at Adventurer 30+
  - Reduced cost: -25% stamina at Adventurer 15, -35% at Adventurer 30+
- **Versatile Fighter**: Directly tied to adaptability in varied combat situations
  - Faster learning: 2x XP from using different weapon types
  - Better versatility: Additional -15% weapon penalty reduction at Adventurer 15
  - Quick switching: -50% switching penalty between weapons
- **Wilderness Survival**: Core skill for independent operations
  - Faster learning: 2x XP from survival actions
  - Better effectiveness: +30% survival bonus at Adventurer 15
  - Lower resource cost: -25% supply consumption at Adventurer 15
- **Improvisation**: Fundamental to adaptable problem-solving
  - Faster learning: 2x XP from improvised solutions
  - Better results: +25% improvised tool effectiveness at Adventurer 15
  - More options: +50% chance to find improvised solutions

**Synergy Strength Scales with Class Level**:

| Adventurer Level | XP Multiplier | Effectiveness Bonus | Cost Reduction | Example: Danger Sense  |
| ---------------- | ------------- | ------------------- | -------------- | ---------------------- |
| Level 5          | 1.5x (+50%)   | +15%                | -15% stamina   | Good awareness         |
| Level 10         | 1.75x (+75%)  | +20%                | -20% stamina   | Strong detection       |
| Level 15         | 2.0x (+100%)  | +25%                | -25% stamina   | Excellent senses       |
| Level 30+        | 2.5x (+150%)  | +35%                | -35% stamina   | Supernatural awareness |

#### Tracked Actions

Actions that grant XP to the Adventurer class:

| Action Category    | Specific Actions                                    | XP Value            |
| ------------------ | --------------------------------------------------- | ------------------- |
| Exploration        | Explore ruins, dungeons, wilderness                 | 10-60 per area      |
| Survival           | Survive dangerous situations, environmental hazards | 5-30 per day        |
| Combat             | Defeat enemies (any method)                         | 5-50 per kill       |
| Trap Handling      | Detect, disarm, or survive traps                    | 10-40 per trap      |
| Discovery          | Find hidden areas, treasure, artifacts              | 15-80 per find      |
| Problem Solving    | Overcome obstacles through clever solutions         | 10-50 per solution  |
| Party Coordination | Lead or coordinate party activities successfully    | 10-40 per session   |
| Monster Encounter  | Handle monster encounters (fight, flee, negotiate)  | 10-60 per encounter |
| Escape             | Successfully escape deadly situations               | 20-100 per escape   |
| Quest Completion   | Complete adventuring quests or contracts            | 50-200 per quest    |

#### Class Consolidation

See [Class Consolidation System](../../../systems/character/class-consolidation/index.md) for full mechanics.

| Consolidation Path                             | Requirements                                                                                    | Result Class      | Tier |
| ---------------------------------------------- | ----------------------------------------------------------------------------------------------- | ----------------- | ---- |
| [Ranger](../consolidation/index.md)            | Adventurer + [Hunter](../gathering/hunter/index.md) or [Forager](../gathering/forager/index.md) | Ranger            | 1    |
| [Dungeon Delver](../consolidation/index.md)    | Adventurer + [Miner](../gathering/miner/index.md)                                               | Dungeon Delver    | 1    |
| [Monster Hunter](../consolidation/index.md)    | Adventurer + monster focus                                                                      | Monster Hunter    | 1    |
| [Explorer](../consolidation/index.md)          | Adventurer + [Scout](./scout/index.md)                                                          | Explorer          | 1    |
| [Expedition Leader](../consolidation/index.md) | Multiple exploration-related classes                                                            | Expedition Leader | 2    |

### Interactions

| System                                                          | Interaction                                    |
| --------------------------------------------------------------- | ---------------------------------------------- |
| [Exploration](../../../systems/world/exploration/index.md)      | Primary exploration class; discovers new areas |
| [Combat](../../../systems/combat/combat-resolution/index.md)    | Versatile fighter; adapts to situations        |
| [Survival](../../../systems/world/environment-hazards/index.md) | Handles environmental challenges effectively   |
| [Party](../../../systems/combat/party-mechanics/index.md)       | Flexible role; fills gaps in party composition |
| [Economy](../../../systems/economy/index.md)                    | Earns through treasure, quest rewards          |
| [Settlement](../../../systems/world/settlements/index.md)       | Brings news of distant places, unique goods    |

---

## Related Content

- **Requires:** Adventuring gear (rope, torches, rations, etc.)
- **Equipment:** [Adventuring Gear](../../items/index.md), [Weapons](../../items/index.md), [Armor](../../items/index.md)
- **Synergy Classes:** [Scout](./scout/index.md), [Hunter](../gathering/hunter/index.md), [Miner](../gathering/miner/index.md)
- **Consolidates To:** [Ranger](../consolidation/index.md), [Monster Hunter](../consolidation/index.md), [Explorer](../consolidation/index.md)
- **See Also:** [Exploration System](../../../systems/world/exploration/index.md), [Hazards](../../../systems/world/environment-hazards/index.md), [Dungeon Delving](../../../systems/world/index.md)
