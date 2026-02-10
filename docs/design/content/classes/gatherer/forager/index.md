---
title: 'Forager'
type: 'class'
category: 'gathering'
tier: 2
prerequisite_xp: 5000
prerequisite_actions: foraging
summary: 'Gatherer of wild plants, herbs, and natural resources'
tags:
  - Gathering/Herbalism
---

# Forager

## Lore

### Origin

Before farms, before trade, before civilization itself, humans foraged. The knowledge of which plants heal, which nourish, which kill - this represents humanity's first science, learned through observation and tragic experimentation. Every culture develops foraging traditions adapted to local ecosystems, knowledge passed through generations like treasured inheritance.

The Forager's path requires no equipment beyond a basket and sharp eyes, yet mastering it demands years of study. A poisonous mushroom can resemble its edible cousin so closely that only subtle details separate them. Medicinal herbs must be harvested at precise times for maximum potency. Some plants grow only in specific conditions - particular soil types, specific elevations, certain amounts of sunlight. Master Foragers carry mental maps of their territories, knowing every productive patch and returning season after season.

Modern Foragers range from casual hobbyists gathering berries for pies to professionals supplying alchemists with rare reagents. The Foragers' Guild promotes sustainable harvesting - taking only what you need, leaving enough to regrow, rotating gathering sites to prevent depletion. Short-sighted greed destroys resources for everyone. Responsible foraging ensures future generations will find the same abundance.

### In the World

Foragers exist everywhere civilization touches wilderness. Village children learn to gather berries and nuts, supplementing family meals with wild abundance. Professional Foragers supply markets with mushrooms, herbs, and specialty items that fetch premium prices. Alchemists rely on Foragers for fresh ingredients - many reagents lose potency rapidly after harvest. Temples employ Foragers to gather plants for ceremonies and medicines.

The work follows natural cycles. Spring brings tender greens, medicinal flowers, and early mushrooms. Summer offers berries, fruits, and herbs at peak potency. Autumn provides nuts, roots, and mushrooms in abundance. Winter requires knowledge of evergreen plants, tree barks, and preserved materials. Successful Foragers maintain detailed mental calendars, knowing exactly when and where specific plants will be ready.

Weather affects foraging significantly. Heavy rains bring mushroom flushes but make river crossings dangerous. Drought concentrates plants around water sources but reduces overall yields. Early frosts kill delicate plants while causing others to sweeten. Storm damage creates new opportunities - fallen trees expose root systems, lightning-struck areas allow sun-loving plants to flourish. Experienced Foragers read weather patterns to predict prime gathering conditions.

Territory knowledge separates casual gatherers from professionals. A master Forager knows hundreds of productive sites - the patch of wild ginger near the old oak, the mushroom ring that fruits every autumn, the stream bank where watercress thrives, the rocky slope hosting rare alpine flowers. They guard prime locations jealously while sharing basic knowledge freely. Competition exists but so does cooperation - Foragers often trade information about different plants.

Identification skills develop through practice and sometimes painful mistakes. Early apprentices taste nothing without supervision. They learn to recognize plants in all seasons - summer's lush growth looks different from autumn's withered stems or spring's first shoots. Flowers, leaves, stems, roots, seeds - every part has identifying features. Smell, taste, texture, growing conditions - all provide clues. One mistake can kill, so Foragers err on the side of caution until absolutely certain.

---

## Mechanics

### Prerequisites

| Requirement        | Value                                                         |
| ------------------ | ------------------------------------------------------------- |
| XP Threshold       | 5,000 XP from foraging tracked actions                        |
| Related Foundation | [Gatherer](../gatherer/index.md) (optional, provides bonuses) |
| Tag Depth Access   | 2 levels (e.g., `[Gathering/Herbalism]`)                      |

### Requirements

| Requirement       | Value                                            |
| ----------------- | ------------------------------------------------ |
| Unlock Trigger    | Gather wild plants, herbs, berries, or mushrooms |
| Primary Attribute | AWR (Awareness), PAT (Patience)                  |
| Starting Level    | 1                                                |
| Tools Required    | Basket, basic gathering tools                    |

### Stats

#### Base Class Stats

| Level | HP Bonus | Stamina Bonus | Trait              |
| ----- | -------- | ------------- | ------------------ |
| 1     | +3       | +10           | Apprentice Forager |
| 10    | +10      | +30           | Journeyman Forager |
| 25    | +20      | +65           | Master Forager     |
| 50    | +40      | +125          | Legendary Forager  |

#### Gathering Bonuses

| Class Level | Yield Bonus | Quality Bonus | Detection Range | Rare Find Chance |
| ----------- | ----------- | ------------- | --------------- | ---------------- |
| 1-9         | +0%         | +0%           | 15m             | +0%              |
| 10-24       | +20%        | +15%          | 30m             | +8%              |
| 25-49       | +45%        | +35%          | 60m             | +20%             |
| 50+         | +75%        | +60%          | 120m            | +40%             |

#### Starting Skills

| Skill             | Type    | Source | Effect                                           |
| ----------------- | ------- | ------ | ------------------------------------------------ |
| Basic Foraging    | Passive | Class  | Can gather common plants, berries, and mushrooms |
| Resource Sense    | Passive | Lesser | Finding wild resources more easily               |
| Bountiful Harvest | Passive | Lesser | Better yields from gathering                     |

#### Synergy Skills

Skills that have strong synergies with Forager. These skills can be learned by any character, but Foragers gain significant bonuses (2x XP acquisition, enhanced effectiveness, reduced costs). Higher Forager levels provide stronger synergy bonuses.

**Gathering Skills**:

- [Plant Identification](../../skills/index.md) - Lesser/Greater/Enhanced - Identify plants instantly
- [Keen Eyes](../../skills/index.md) - Lesser/Greater/Enhanced - Increased detection range for gathering nodes
- [Gentle Touch](../../skills/index.md) - Lesser/Greater/Enhanced - Chance for bonus yield without damaging plant
- [Seasonal Knowledge](../../skills/index.md) - Lesser/Greater/Enhanced - Know optimal harvest times
- [Rapid Gathering](../../skills/index.md) - Lesser/Greater/Enhanced - Reduced gathering time per node
- [Quality Assessment](../../skills/mechanic-unlock/index.md) - Lesser/Greater/Enhanced - Judge plant quality/potency instantly
- [Sustainable Harvest](../../skills/common/index.md) - Lesser/Greater/Enhanced - Plants regrow faster after harvest
- [Rare Specimens](../../skills/index.md) - Lesser/Greater/Enhanced - Increased chance to find rare/magical plants
- [Herbalism](../../skills/index.md) - Lesser/Greater/Enhanced - Identify medicinal properties
- [Territory Mapping](../../skills/index.md) - Lesser/Greater/Enhanced - Remember locations of productive sites
- [Weather Reading](../../skills/common/index.md) - Lesser/Greater/Enhanced - Predict best gathering conditions
- [Poison Detection](../../skills/index.md) - Lesser/Greater/Enhanced - Identify toxic plants/properties
- [Mushroom Mastery](../../skills/index.md) - Lesser/Greater/Enhanced - Increased mushroom yield and quality
- [Wild Sense](../../skills/index.md) - Lesser/Greater/Enhanced - Detect hidden/camouflaged plants
- [Alchemical Eye](../../skills/index.md) - Lesser/Greater/Enhanced - Identify reagent properties for alchemy instantly

**Note**: All skills listed have strong synergies because they are core foraging skills. Characters without Forager class can still learn these skills but progress at base rate (1x XP) without effectiveness bonuses.

#### Synergy Bonuses

Forager provides context-specific bonuses to foraging-related skills based on logical specialization:

**Core Trade Skills** (Direct specialization - Strong synergy):

- **Plant Identification**: Essential for safe and effective foraging
  - Faster learning: 2x XP from gathering actions (scales with class level)
  - Better effectiveness: +25% identification speed at Forager 15, +35% at Forager 30+
  - Reduced cost: -25% mental focus required at Forager 15, -35% at Forager 30+
- **Keen Eyes**: Directly tied to finding gathering nodes
  - Faster learning: 2x XP from exploration actions
  - Better detection: +30% range bonus at Forager 15
  - Lower fatigue: -50% stamina drain during searching
- **Seasonal Knowledge**: Core skill for optimal harvesting
  - Faster learning: 2x XP from seasonal gathering
  - Better timing: +20% yield bonus when harvesting at perfect time
  - Lower waste: -20% spoilage during off-season storage

**Synergy Strength Scales with Class Level**:

| Forager Level | XP Multiplier | Effectiveness Bonus | Cost Reduction | Example: Keen Eyes  |
| ------------- | ------------- | ------------------- | -------------- | ------------------- |
| Level 5       | 1.5x (+50%)   | +15%                | -15% stamina   | Good but moderate   |
| Level 10      | 1.75x (+75%)  | +20%                | -20% stamina   | Strong improvements |
| Level 15      | 2.0x (+100%)  | +25%                | -25% stamina   | Excellent synergy   |
| Level 30+     | 2.5x (+150%)  | +35%                | -35% stamina   | Masterful synergy   |

**Example Progression**:

A Forager 15 learning Keen Eyes:

- Performs 50 foraging exploration actions (earning 2x XP = 1000 XP total, vs 500 XP for non-Forager)
- Keen Eyes available at level-up after just 500 XP (vs 1500 XP for non-foraging class)
- When using Keen Eyes: Base +80% range becomes +105% range (base +25% synergy)
- Stamina cost: 7.5 stamina instead of 10 stamina per hour (25% reduction)

#### Tracked Actions

Actions that grant XP to the Forager class:

| Action Category    | Specific Actions                                 | XP Value          |
| ------------------ | ------------------------------------------------ | ----------------- |
| Gathering          | Gather berries, nuts, fruits, edible plants      | 2-20 per node     |
| Herb Gathering     | Gather medicinal herbs, alchemical reagents      | 5-40 per node     |
| Mushroom Hunting   | Gather mushrooms (edible, medicinal, alchemical) | 3-30 per node     |
| Root Digging       | Harvest roots, tubers, underground materials     | 5-25 per node     |
| Flower Picking     | Gather flowers for decoration, medicine, alchemy | 2-15 per node     |
| Territory Scouting | Survey areas for productive gathering sites      | 10-30 per survey  |
| Seasonal Gathering | Gather seasonal specialties at peak times        | 5-35 per session  |
| Identification     | Identify unknown plants, assess properties       | 5-20 per plant    |
| Rare Discovery     | Discover rare or magical plant specimens         | 30-100 per find   |
| Teaching           | Teach others about plants and foraging           | 15-40 per session |

#### Class Consolidation

See [Class Consolidation System](../../../systems/character/class-consolidation.md) for full mechanics.

| Consolidation Path                            | Requirements                                    | Result Class     | Tier |
| --------------------------------------------- | ----------------------------------------------- | ---------------- | ---- |
| [Herbalist](../consolidation/index.md)        | Forager + medicinal focus                       | Herbalist        | 1    |
| [Survivalist](../consolidation/index.md)      | Forager + [Explorer](../combat/index.md)        | Survivalist      | 1    |
| [Herbalist-Brewer](../consolidation/index.md) | Forager + [Alchemist](../crafting/alchemist.md) | Herbalist-Brewer | 1    |
| [Ranger](../consolidation/index.md)           | Forager + [Hunter](./hunter.md)                 | Ranger           | 1    |
| [Naturalist](../consolidation/index.md)       | Multiple gathering classes                      | Naturalist       | 2    |

### Interactions

| System                                                        | Interaction                                                                               |
| ------------------------------------------------------------- | ----------------------------------------------------------------------------------------- |
| [Gathering](../../../systems/crafting/gathering.md)           | Primary plant gathering class; provides herbs and reagents                                |
| [Crafting](../../../systems/crafting/crafting-progression.md) | Essential supplier for [Alchemist](../crafting/alchemist.md), [Cook](../crafting/cook.md) |
| [Economy](../../../systems/economy/index.md)                  | Can sell herbs, reagents, and specialty plants                                            |
| [Exploration](../../../systems/world/exploration.md)          | Discovers plant resources while exploring                                                 |
| [Survival](../../../systems/world/index.md)                   | Provides food and medicine in wilderness                                                  |
| [Social](../../../systems/social/index.md)                    | Village foragers are valued community members                                             |
| [Magic](../../../systems/character/magic-system.md)           | Supplies components for spell reagents                                                    |

---

## Related Content

- **Requires:** Basket or gathering bag, basic tools
- **Gathers:** [Herbs](../../materials/index.md), [Berries](../../materials/index.md), [Mushrooms](../../materials/index.md), [Flowers](../../materials/index.md)
- **Supplies:** [Alchemist](../crafting/alchemist.md), [Cook](../crafting/cook.md), [Healer](../magic/index.md)
- **Synergy Classes:** [Alchemist](../crafting/alchemist.md), [Cook](../crafting/cook.md), [Hunter](./hunter.md)
- **See Also:** [Gathering System](../../../systems/crafting/gathering.md), [Plant Resources](../../materials/index.md), [Seasonal Cycles](../../../systems/core/time-and-rest.md)
