---
title: 'Miner'
type: 'class'
category: 'gathering'
tier: 2
prerequisite_xp: 5000
prerequisite_actions: mining and ore extraction
summary: 'Expert in extracting ore, gems, and minerals from the earth'
tags:
  - Gathering/Mining
---

# Miner

## Lore

### Origin

The earth holds wealth beyond imagining - ore for tools and weapons, gems for beauty and magic, minerals for alchemy and medicine. But the earth guards its treasures jealously, hiding them beneath stone and soil. The miner's craft is as old as civilization itself, born the moment someone realized that the right stone, properly worked, made better tools than wood or bone.

Early miners followed surface veins, gathering what erosion exposed. As surface deposits depleted, they learned to dig deeper. The dwarves perfected mining into an art form, their cities carved from living rock, their mines extending miles into mountain hearts. Humans brought innovation and adaptability, developing new techniques for different terrains. Gnomes contributed clever engineering - pumps to remove water, carts on rails, ventilation systems. Each race's contributions merged into modern mining practice.

The Miners' Guild maintains strict safety standards learned through hard experience. Every miner knows the stories: tunnels that collapsed, gas pockets that exploded, floods that drowned entire work crews. Proper shoring, careful planning, and respect for warning signs separate professionals from corpses. Old miners say the mountain speaks to those who listen - creaking timbers warn of instability, strange echoes reveal hidden chambers, particular smells indicate dangerous gases.

### In the World

Without miners, civilization grinds to a halt. No ore means no metal. No metal means no tools, weapons, or machinery. No gems means limited enchanting. Alchemists need specific minerals. Even farmers depend on mined stone for building walls and grinding wheels. Mining communities form the foundation of economic networks that span continents.

Mining towns cluster near rich deposits, their entire economies centered on extraction. The work is hard, dangerous, and well-paid compared to farming. Miners develop distinctive cultures - tight-knit communities who trust each other with their lives underground, robust humor to counterbalance daily danger, and reverence for earth spirits or deities depending on local beliefs. Mine collapse survivors often carry guilt alongside relief, remembering companions who weren't as lucky.

Different mining styles suit different materials. Surface mining strips away earth to expose shallow deposits - efficient but hard on landscapes. Shaft mining drives vertical or angled tunnels to reach deep veins. Drift mining follows horizontal seams. Placer mining sifts riverbed deposits for gold and gems. Each technique requires specialized knowledge and equipment. A miner trained in mountain operations might struggle with beach placer work and vice versa.

Apprentice miners start with surface work, learning to identify promising ground and recognize ore indicators. They progress to basic shaft work - swinging picks, hauling ore carts, shoring tunnel walls. With experience comes the knowledge that separates competent miners from masters: reading stone layers to predict what lies ahead, detecting instability before collapses, finding hidden veins by subtle clues others miss. The best miners seem to have a sixth sense for where valuable deposits hide.

Mine work follows predictable rhythms. Morning checks ensure overnight stability. Crews descend with tools, lights, and lunch. Hours of hard labor - breaking rock, sorting ore, reinforcing supports. Lunch break in semi-darkness, sharing food and gossip. More hours of work. Evening ascent, exhausted but satisfied. Ore processed and sorted. Tomorrow, repeat. Yet despite the routine, every shift brings potential for discovery: an unexpected vein of silver, a pocket of perfect crystals, a cave system leading to unexplored territory.

---

## Mechanics

### Prerequisites

| Requirement        | Value                                                         |
| ------------------ | ------------------------------------------------------------- |
| XP Threshold       | 5,000 XP from mining tracked actions                          |
| Related Foundation | [Gatherer](../gatherer/index.md) (optional, provides bonuses) |
| Tag Depth Access   | 2 levels (e.g., `Gathering/Mining`)                           |

### Requirements

| Requirement       | Value                                       |
| ----------------- | ------------------------------------------- |
| Unlock Trigger    | Mine ore, gems, or minerals from the ground |
| Primary Attribute | STR (Strength), END (Endurance)             |
| Starting Level    | 1                                           |
| Tools Required    | Pickaxe, shovel, mining equipment           |

### Stats

#### Base Class Stats

| Level | HP Bonus | Stamina Bonus | Trait            |
| ----- | -------- | ------------- | ---------------- |
| 1     | +8       | +20           | Apprentice Miner |
| 10    | +25      | +55           | Journeyman Miner |
| 25    | +50      | +110          | Master Miner     |
| 50    | +90      | +190          | Legendary Miner  |

#### Gathering Bonuses

| Class Level | Yield Bonus | Quality Bonus | Detection Range | Rare Find Chance |
| ----------- | ----------- | ------------- | --------------- | ---------------- |
| 1-9         | +0%         | +0%           | 10m             | +0%              |
| 10-24       | +15%        | +10%          | 20m             | +5%              |
| 25-49       | +35%        | +25%          | 40m             | +15%             |
| 50+         | +60%        | +50%          | 80m             | +30%             |

#### Starting Skills

| Skill             | Type    | Source | Effect                                           |
| ----------------- | ------- | ------ | ------------------------------------------------ |
| Basic Mining      | Passive | Class  | Can mine common ore and stone from surface nodes |
| Resource Sense    | Passive | Lesser | Finding ore veins more easily                    |
| Efficiency Expert | Passive | Lesser | Reduced stamina for mining                       |

#### Synergy Skills

Skills that have strong synergies with Miner. These skills can be learned by any character, but Miners gain significant bonuses (2x XP acquisition, enhanced effectiveness, reduced costs). Higher Miner levels provide stronger synergy bonuses.

**Gathering Skills**:

- [Ore Sense](../../skills/index.md) - Lesser/Greater/Enhanced - Detect ore nodes of various types
- [Efficient Mining](../../skills/index.md) - Lesser/Greater/Enhanced - Chance to gain extra ore per strike
- [Rapid Extraction](../../skills/index.md) - Lesser/Greater/Enhanced - Reduced mining time per node
- [Gem Finding](../../skills/index.md) - Lesser/Greater/Enhanced - Increased gem discovery rate
- [Deep Knowledge](../../skills/index.md) - Lesser/Greater/Enhanced - Mine deeper nodes below surface
- [Stone Reading](../../skills/index.md) - Lesser/Greater/Enhanced - Read geological signs to locate deposits
- [Safety Sense](../../skills/common/index.md) - Lesser/Greater/Enhanced - Detect hazards and dangers
- [Vein Tracking](../../skills/index.md) - Lesser/Greater/Enhanced - Follow ore veins to richest deposits
- [Quality Assessment](../../skills/mechanic-unlock/index.md) - Lesser/Greater/Enhanced - Judge ore quality instantly
- [Endurance Mining](../../skills/index.md) - Lesser/Greater/Enhanced - Reduced stamina cost per swing
- [Rare Deposits](../../skills/index.md) - No tiers - Can detect and mine rare metals
- [Cave Sight](../../skills/index.md) - Lesser/Greater/Enhanced - See in darkness
- [Structural Sense](../../skills/index.md) - Lesser/Greater/Enhanced - Reinforce tunnels with better stability
- [Precious Finds](../../skills/index.md) - Lesser/Greater/Enhanced - Increased chance for gems, crystals, rare minerals
- [Expedition Leader](../../skills/index.md) - Lesser/Greater/Enhanced - Lead mining expeditions with better group efficiency

**Note**: All skills listed have strong synergies because they are core mining skills. Characters without Miner class can still learn these skills but progress at base rate (1x XP) without effectiveness bonuses.

#### Synergy Bonuses

Miner provides context-specific bonuses to mining-related skills based on logical specialization:

**Core Trade Skills** (Direct specialization - Strong synergy):

- **Ore Sense**: Essential for locating valuable deposits
  - Faster learning: 2x XP from mining and prospecting actions (scales with class level)
  - Better effectiveness: +25% detection range at Miner 15, +35% at Miner 30+
  - Reduced cost: -25% focus required at Miner 15, -35% at Miner 30+
- **Stone Reading**: Directly tied to geological understanding
  - Faster learning: 2x XP from prospecting actions
  - Better accuracy: +30% deposit prediction at Miner 15
  - Lower false positives: -50% wasted time on poor sites
- **Vein Tracking**: Core skill for maximizing ore extraction
  - Faster learning: 2x XP from vein following
  - Better yields: +20% ore from tracked veins at Miner 15
  - Lower depletion: -20% from suboptimal extraction paths

**Synergy Strength Scales with Class Level**:

| Miner Level | XP Multiplier | Effectiveness Bonus | Cost Reduction | Example: Ore Sense  |
| ----------- | ------------- | ------------------- | -------------- | ------------------- |
| Level 5     | 1.5x (+50%)   | +15%                | -15% focus     | Good but moderate   |
| Level 10    | 1.75x (+75%)  | +20%                | -20% focus     | Strong improvements |
| Level 15    | 2.0x (+100%)  | +25%                | -25% focus     | Excellent synergy   |
| Level 30+   | 2.5x (+150%)  | +35%                | -35% focus     | Masterful synergy   |

**Example Progression**:

A Miner 15 learning Ore Sense:

- Performs 50 mining actions (earning 2x XP = 1000 XP total, vs 500 XP for non-Miner)
- Ore Sense available at level-up after just 500 XP (vs 1500 XP for non-mining class)
- When using Ore Sense: Base rare type detection becomes +25% more accurate (base +25% synergy)
- Focus cost: 7.5 mental stamina instead of 10 (25% reduction)

#### Tracked Actions

Actions that grant XP to the Miner class:

| Action Category | Specific Actions                                 | XP Value             |
| --------------- | ------------------------------------------------ | -------------------- |
| Mining          | Mine ore nodes (copper, iron, gold, etc.)        | 5-50 per node        |
| Gem Extraction  | Extract gems, crystals, precious stones          | 10-100 per gem       |
| Prospecting     | Survey areas for deposits, test samples          | 5-20 per survey      |
| Tunnel Work     | Dig tunnels, reinforce shafts, build supports    | 10-40 per session    |
| Deep Mining     | Mine in dangerous/deep locations                 | 15-60 per session    |
| Vein Discovery  | Discover new ore veins or deposits               | 30-100 per find      |
| Safety Work     | Shore tunnels, clear hazards, rescue operations  | 10-50 per task       |
| Ore Processing  | Sort, grade, and prepare mined materials         | 5-15 per batch       |
| Expedition      | Participate in mining expeditions                | 20-80 per expedition |
| Rare Find       | Discover exceptional specimens or rare materials | 50-200 per discovery |

#### Class Consolidation

See [Class Consolidation System](../../../systems/character/class-consolidation.md) for full mechanics.

| Consolidation Path                             | Requirements                                    | Result Class      | Tier |
| ---------------------------------------------- | ----------------------------------------------- | ----------------- | ---- |
| [Prospector](../consolidation/index.md)        | Miner + [Explorer](../combat/index.md)          | Prospector        | 1    |
| [Smith-Miner](../consolidation/index.md)       | Miner + [Blacksmith](../crafting/blacksmith.md) | Smith-Miner       | 1    |
| [Gem Merchant](../consolidation/index.md)      | Miner + [Merchant](../trade/index.md)           | Gem Merchant      | 1    |
| [Dungeon Delver](../consolidation/index.md)    | Miner + [Adventurer](../combat/index.md)        | Dungeon Delver    | 1    |
| [Master Prospector](../consolidation/index.md) | Multiple gathering classes                      | Master Prospector | 2    |

### Interactions

| System                                                        | Interaction                                                                                       |
| ------------------------------------------------------------- | ------------------------------------------------------------------------------------------------- |
| [Gathering](../../../systems/crafting/gathering.md)           | Primary mining class; provides ore and gems for crafting                                          |
| [Crafting](../../../systems/crafting/crafting-progression.md) | Essential supplier for [Blacksmith](../crafting/blacksmith.md), [Jeweler](../crafting/jeweler.md) |
| [Economy](../../../systems/economy/index.md)                  | Mining operations generate significant wealth                                                     |
| [Exploration](../../../systems/world/exploration.md)          | Discovers underground areas and cave systems                                                      |
| [Settlement](../../../systems/world/settlements.md)           | Mining towns provide valuable resources                                                           |
| [Combat](../../../systems/combat/index.md)                    | Can use pickaxe as improvised weapon                                                              |
| [Environment](../../../systems/world/environment-hazards.md)  | Must manage underground hazards                                                                   |

---

## Related Content

- **Requires:** [Pickaxe](../../item/tool/pickaxe/), mining tools, light source
- **Gathers:**
  - [Ore](../../item/raw/ore/) - Copper Ore, Iron Ore
  - [Coal](../../material/fuel/coal/) - Fuel for smelting
  - [Metals](../../material/metal/) - [Copper](../../material/metal/copper/), [Iron](../../material/metal/iron/)
- **Supplies:** [Blacksmith](../crafter/blacksmith/), [Jeweler](../crafter/jeweler/), [Alchemist](../crafter/alchemist/)
- **Synergy Classes:** [Blacksmith](../crafter/blacksmith/), [Jeweler](../crafter/jeweler/), [Merchant](../trader/merchant/)
- **See Also:** [Gathering System](../../../systems/crafting/gathering.md), [Smelting Recipes](../../recipe/smelting/)
