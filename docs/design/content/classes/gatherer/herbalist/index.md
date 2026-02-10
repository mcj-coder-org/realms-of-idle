---
title: 'Herbalist'
type: 'class'
category: 'gathering'
tier: 2
prerequisite_xp: 5000
prerequisite_actions: herb gathering
summary: 'Expert in finding, identifying, and preparing plants for medicine, alchemy, and cooking'
tags:
  - Gathering/Herbalism
---

# Herbalist

## Lore

### Origin

Long before alchemists distilled potions in glass vessels, herbalists gathered plants under moonlight. The first healers were herbalists, knowing which leaf stopped bleeding, which root eased fever, which bark brought sleep. This knowledge predates civilization itself - even animals know to eat certain plants when sick. Humans simply learned to remember, to cultivate, to prepare.

Herbal traditions developed in every culture, each shaped by local flora. Forest peoples know fungi and ferns; desert dwellers master succulents and bitter roots; coastal communities harvest seaweed and salt-loving plants. The knowledge passes from master to apprentice, often through families where grandmothers teach granddaughters the secrets their own grandmothers taught them. Written herbals exist, but the true knowledge lives in practiced hands and trained eyes.

The Herbalists' Guild straddles multiple worlds. Some members focus on medicine, working alongside healers in hospitals and homes. Others supply alchemists with the raw materials for potions and elixirs. Still others work in kitchens, providing the herbs that transform bland food into cuisine. A master herbalist might do all three, understanding that the same plant serves different purposes depending on preparation.

### In the World

Walk through any market and you'll pass the herbalist's stall - bundles of dried plants hanging from the awning, baskets of fresh-picked greens, mysterious roots and bark arranged in careful order. The herbalist knows each customer's needs: chamomile for the anxious merchant, willowbark for the arthritic smith, rosemary for the forgetful scholar. They prescribe and prepare, often serving as the village's first line of medical care.

Gathering herbs requires intimate knowledge of the land. The same plant growing in shade versus sun contains different compounds. Morning dew affects potency. Moon phase matters for some species. An expert herbalist knows where every useful plant grows within a day's walk, returning to the same patches year after year, harvesting sustainably to ensure future supply. They notice when something's wrong - drought stress, disease, animal damage - and adjust their routes accordingly.

Preparation is half the art. Some herbs work fresh, others must be dried. Some need heat, others require cold extraction. Timing matters: harvest too early and potency suffers, too late and the plant has gone to seed. Master herbalists maintain gardens for common plants while ranging far afield for rare specimens. Their workshops smell of green things and earth, of drying bundles and steeping tinctures.

---

## Mechanics

### Prerequisites

| Requirement        | Value                                                         |
| ------------------ | ------------------------------------------------------------- |
| XP Threshold       | 5,000 XP from herb gathering tracked actions                  |
| Related Foundation | [Gatherer](../gatherer/index.md) (optional, provides bonuses) |
| Tag Depth Access   | 2 levels (e.g., `[Gathering/Herbalism]`)                      |

### Requirements

| Requirement       | Value                                           |
| ----------------- | ----------------------------------------------- |
| Unlock Trigger    | Gather and correctly identify medicinal plants  |
| Primary Attribute | AWR (Awareness), WIT (Wit)                      |
| Starting Level    | 1                                               |
| Tools Required    | Gathering knife, drying rack, mortar and pestle |

### Stats

#### Base Class Stats

| Level | HP Bonus | Stamina Bonus | Trait                |
| ----- | -------- | ------------- | -------------------- |
| 1     | +3       | +8            | Apprentice Herbalist |
| 10    | +8       | +20           | Journeyman Herbalist |
| 25    | +15      | +40           | Master Herbalist     |
| 50    | +25      | +70           | Legendary Herbalist  |

#### Gathering Bonuses

| Class Level | Yield Bonus | Quality Bonus | Rare Find Chance | Identification |
| ----------- | ----------- | ------------- | ---------------- | -------------- |
| 1-9         | +0%         | +0%           | +0%              | Basic          |
| 10-24       | +15%        | +10%          | +10%             | Apprentice     |
| 25-49       | +30%        | +25%          | +25%             | Journeyman     |
| 50+         | +50%        | +50%          | +50%             | Master         |

#### Starting Skill

| Skill                 | Type    | Effect                                       |
| --------------------- | ------- | -------------------------------------------- |
| Basic Plant Knowledge | Passive | Can identify and safely gather common plants |

#### Starting Skills (Auto-Awarded on Class Acceptance)

Skills automatically awarded when accepting this class:

| Skill          | Tier   | Link                                                              | Reasoning                                   |
| -------------- | ------ | ----------------------------------------------------------------- | ------------------------------------------- |
| Gatherer's Eye | Lesser | [See Skill](../../skills/common/gatherers-eye/index.md)           | Spotting plants is fundamental to herbalism |
| Gentle Harvest | Lesser | [See Skill](../../skills/mechanic-unlock/gentle-harvest/index.md) | Proper harvesting preserves plant and yield |

#### Synergy Skills

Skills with strong synergies for Herbalist:

**Gathering Skills**:

- [Gatherer's Eye](../../skills/common/gatherers-eye/index.md) - Spot resources more easily
- [Gentle Harvest](../../skills/mechanic-unlock/gentle-harvest/index.md) - Preserve plants while harvesting
- [Resource Sense](../../skills/tiered/resource-sense/index.md) - Locate resources at a distance
- [Environmental Reading](../../skills/common/environmental-reading/index.md) - Understand habitat conditions

**Herbalism Skills** (specialized):

- Plant Identification - Lesser/Greater/Enhanced - Identify plants and their properties
- Sustainable Harvest - Lesser/Greater/Enhanced - Gather without depleting patches
- Processing Knowledge - Lesser/Greater/Enhanced - Prepare herbs for various uses
- Toxin Awareness - Lesser/Greater/Enhanced - Safely handle dangerous plants
- Cultivation - Lesser/Greater/Enhanced - Grow herbs in gardens
- Seasonal Timing - Lesser/Greater/Enhanced - Know optimal harvest times
- Preservation - Lesser/Greater/Enhanced - Store herbs for maximum potency

#### Tracked Actions

Actions that grant XP to the Herbalist class:

| Action Category | Specific Actions                              | XP Value          |
| --------------- | --------------------------------------------- | ----------------- |
| Gathering       | Collect herbs, roots, bark, flowers           | 5-30 per batch    |
| Identification  | Correctly identify unknown plants             | 10-50 per plant   |
| Processing      | Dry, grind, extract, or prepare herbs         | 5-25 per batch    |
| Cultivation     | Plant, tend, or harvest garden herbs          | 5-15 per action   |
| Teaching        | Share herbal knowledge with others            | 15-40 per session |
| Discovery       | Find new plant species or uses                | 50-200            |
| Rare Gathering  | Successfully harvest rare or dangerous plants | 30-100            |

#### Class Consolidation

See [Class Consolidation System](../../../systems/character/class-consolidation/index.md) for full mechanics.

| Consolidation Path                      | Requirements                                            | Result Class  | Tier |
| --------------------------------------- | ------------------------------------------------------- | ------------- | ---- |
| [Artisan](../consolidation/index.md)    | Herbalist + [Forager](forager/index.md)                 | Nature Expert | 1    |
| [Apothecary](../consolidation/index.md) | Herbalist + [Alchemist](../crafting/alchemist/index.md) | Apothecary    | 1    |
| [Healer](../consolidation/index.md)     | Herbalist + healing focus                               | Hedge Healer  | 1    |

### Interactions

| System                                                              | Interaction                                              |
| ------------------------------------------------------------------- | -------------------------------------------------------- |
| [Crafting](../../../systems/crafting/crafting-progression/index.md) | Supplies raw materials for Alchemy and Medicine          |
| [Economy](../../../systems/economy/index.md)                        | Can establish herb shop; supplies healers and alchemists |
| [Medicine](../../../systems/character/health/index.md)              | Prepared herbs provide healing and treatments            |
| [Alchemy](../crafting/alchemist/index.md)                           | Primary supplier of alchemical reagents                  |
| [Cooking](../crafting/cook/index.md)                                | Provides culinary herbs and spices                       |
| [Settlement](../../../systems/world/settlements/index.md)           | Herb gardens and gathering grounds support health        |

---

## Related Content

- **Requires:** Gathering tools, knowledge of local flora, drying/processing equipment
- **Materials Gathered:** Herbs, roots, bark, flowers, fungi, seeds
- **Prepares For:** [Alchemist](../crafting/alchemist/index.md), [Cook](../crafting/cook/index.md), [Healer](../magic/healer/index.md)
- **Recipes:** Herb Processing, Tinctures, Dried Herbs
- **Synergy Classes:** [Forager](forager/index.md), [Alchemist](../crafting/alchemist/index.md), [Healer](../magic/healer/index.md)
- **See Also:** [Gathering System](../../../systems/crafting/gathering/index.md), [Alchemy Recipes](../../recipes/alchemy/index.md)
