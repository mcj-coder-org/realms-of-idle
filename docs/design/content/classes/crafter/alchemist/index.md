---
title: 'Alchemist'
type: 'class'
category: 'crafting'
tier: 2
prerequisite_xp: 5000
prerequisite_actions: alchemy
summary: 'Brewer of potions and elixirs, transforming natural reagents into powerful effects'
---

# Alchemist

## Lore

### Origin

Alchemy straddles the line between science and magic, craft and art. The first alchemists were herbalists who noticed that boiling certain plants together created effects greater than the sum of their parts. Over centuries, the practice evolved from simple teas and tinctures into a sophisticated discipline with its own language, tools, and theories.

The Alchemists' Guild maintains that true alchemy requires understanding the essential nature of ingredients - what they call the "inner fire" or "quintessence." A careless brewer simply mixes ingredients and hopes for results. A true alchemist understands why moonbell petals mixed with silver dust create a sleeping draught, and can adjust the formula for different strengths, durations, and side effects.

University-trained alchemists look down on hedge brewers who learn through trial and error, but those hedge brewers often discover combinations that the universities would never approve for study. Some of the most effective (if unpredictable) potions come from village wise-folk who learned alchemy from their grandmothers, who learned from their grandmothers, in an unbroken chain stretching back before the Guild existed.

### In the World

Walk into any alchemist's shop and your nose immediately knows where you are - a complex aroma of dried herbs, mineral salts, brewing potions, and occasionally something that makes your eyes water. Shelves overflow with labeled bottles, ceramic jars, and mysterious bundles of dried plants. Careful notes detail successful formulas and disastrous failures alike.

Common folk visit the alchemist for healing draughts, energy tonics, and remedies for everyday ailments. Adventurers stock up on healing potions before dangerous journeys, paying premium prices for quality they can trust when bleeding out in a dungeon. Farmers buy pest repellents and growth accelerants. Craftspeople purchase solvents and adhesives. Even the poor can afford basic tinctures for common colds or upset stomachs.

Young apprentices learn to identify hundreds of plants, minerals, and other reagents by sight and smell. They memorize reagent interactions, practice precise measurement, and develop the patience to tend a potion that must simmer for three days while maintaining exact temperature. The difference between a healing potion and a deadly poison often comes down to proportions measured in pinches and timing counted in heartbeats.

Successful alchemists develop signature formulas - a healing potion that tastes of honey and cinnamon, an energy tonic that fizzes pleasantly on the tongue, a sleeping draught that brings peaceful dreams instead of groggy mornings. These small touches separate adequate alchemists from masters of the craft.

---

## Mechanics

### Prerequisites

| Requirement        | Value                                                             |
| ------------------ | ----------------------------------------------------------------- |
| XP Threshold       | 5,000 XP from alchemy tracked actions                             |
| Related Foundation | [Crafter](../../foundation/index.md) (optional, provides bonuses) |
| Tag Depth Access   | 2 levels (e.g., `[Crafting/Alchemy]`)                             |

### Requirements

| Requirement       | Value                                   |
| ----------------- | --------------------------------------- |
| Unlock Trigger    | Successfully brew potions using alchemy |
| Primary Attribute | WIT (Wits), PAT (Patience)              |
| Starting Level    | 1                                       |
| Tools Required    | Alchemist's kit, brewing equipment      |

### Stats

#### Base Class Stats

| Level | HP Bonus | Mana Bonus | Trait                |
| ----- | -------- | ---------- | -------------------- |
| 1     | +3       | +15        | Apprentice Alchemist |
| 10    | +8       | +40        | Journeyman Alchemist |
| 25    | +15      | +75        | Master Alchemist     |
| 50    | +25      | +125       | Legendary Alchemist  |

#### Crafting Bonuses

| Class Level | Potency Bonus | Success Rate | Batch Size | Side Effects |
| ----------- | ------------- | ------------ | ---------- | ------------ |
| 1-9         | +0%           | 70%          | 1          | 30% chance   |
| 10-24       | +15%          | 85%          | 2          | 15% chance   |
| 25-49       | +35%          | 95%          | 3          | 5% chance    |
| 50+         | +60%          | 99%          | 5          | 0% chance    |

#### Starting Skill

| Skill         | Type    | Effect                                     |
| ------------- | ------- | ------------------------------------------ |
| Basic Brewing | Passive | Can brew basic healing and utility potions |

#### Starting Skills (Auto-Awarded on Class Acceptance)

Skills automatically awarded when accepting this class:

| Skill                 | Tier     | Link                                                               | Reasoning                                       |
| --------------------- | -------- | ------------------------------------------------------------------ | ----------------------------------------------- |
| Potent Brews          | Lesser   | [See Skill](../../skills/tiered/potent-brews.md)                   | Core alchemy capability for effective potions   |
| Potion Identification | Mechanic | [See Skill](../../skills/mechanic-unlock/potion-identification.md) | Understanding potions is fundamental to alchemy |

#### Synergy Skills

Skills with strong synergies for Alchemist:

**General Crafting Skills**:

- [Artisan's Focus](../../skills/tiered/artisans-focus.md) - Lesser/Greater/Enhanced - Enhanced crafting concentration
- [Rapid Crafting](../../skills/tiered/rapid-crafting.md) - Lesser/Greater/Enhanced - Faster creation speed
- [Material Intuition](../../skills/mechanic-unlock/material-intuition.md) - Lesser/Greater/Enhanced - Assess material quality instantly
- [Resource Efficiency](../../skills/tiered/resource-efficiency.md) - Lesser/Greater/Enhanced - Chance to save materials
- [Masterwork Chance](../../skills/tiered/masterwork-chance.md) - Lesser/Greater/Enhanced - Increased chance for exceptional quality
- [Repair Mastery](../../skills/tiered/repair-mastery.md) - Lesser/Greater/Enhanced - Expert repair capabilities
- [Recipe Innovator](../../skills/passive-generator/recipe-innovator.md) - Lesser/Greater/Enhanced - Discover and adapt recipes

**Alchemy Skills**:

- [Potion Identification](../../skills/mechanic-unlock/potion-identification.md) - Lesser/Greater/Enhanced - Identify potions and reagents
- [Potent Brews](../../skills/tiered/potent-brews.md) - Lesser/Greater/Enhanced - Increase potion effectiveness
- [Stable Solutions](../../skills/tiered/stable-solutions.md) - Lesser/Greater/Enhanced - Reduce side effects and improve stability
- [Reagent Cultivation](../../skills/passive-generator/reagent-cultivation.md) - Lesser/Greater/Enhanced - Grow and cultivate alchemical reagents
- [Transmutation Basics](../../skills/mechanic-unlock/transmutation-basics.md) - Mechanic - Basic alchemical transmutations
- [Explosive Compounds](../../skills/mechanic-unlock/explosive-compounds.md) - Lesser/Greater/Enhanced - Create alchemical bombs and explosives

**Note**: All skills listed have strong synergies because they are core alchemy skills. Characters without Alchemist class can still learn these skills but progress at base rate (1x XP) without effectiveness bonuses.

#### Synergy Bonuses

Alchemist provides context-specific bonuses to alchemy-related skills based on logical specialization:

**Core Trade Skills** (Direct specialization - Strong synergy):

- **Ingredient Lore**: Essential for reagent identification and quality assessment
  - Faster learning: 2x XP from identification actions (scales with class level)
  - Better effectiveness: +25% accuracy at Alchemist 15, +35% at Alchemist 30+
  - Reduced cost: -25% time at Alchemist 15, -35% at Alchemist 30+
- **Potency Boost**: Directly tied to potion effectiveness and quality
  - Faster learning: 2x XP from brewing enhanced potions
  - Better boost: +30% potency improvement at Alchemist 15
  - Lower ingredient cost: -20% extra reagent cost at Alchemist 15
- **Efficient Extract**: Core skill for material conservation in brewing
  - Faster learning: 2x XP from brewing actions
  - Better save rate: +30% reagent preservation rate at Alchemist 15
  - Lower failure waste: -50% material loss on failures
- **Stability Control**: Fundamental to safe and reliable potion production
  - Faster learning: 2x XP from complex brewing
  - Better stability: +25% side effect reduction at Alchemist 15
  - More consistent: +50% chance to avoid unexpected reactions

**Synergy Strength Scales with Class Level**:

| Alchemist Level | XP Multiplier | Effectiveness Bonus | Cost Reduction | Example: Potency Boost |
| --------------- | ------------- | ------------------- | -------------- | ---------------------- |
| Level 5         | 1.5x (+50%)   | +15%                | -15% cost      | Good but moderate      |
| Level 10        | 1.75x (+75%)  | +20%                | -20% cost      | Strong improvements    |
| Level 15        | 2.0x (+100%)  | +25%                | -25% cost      | Excellent synergy      |
| Level 30+       | 2.5x (+150%)  | +35%                | -35% cost      | Masterful synergy      |

**Example Progression**:

An Alchemist 15 learning Potency Boost:

- Performs 50 enhanced brewing actions (earning 2x XP = 1000 XP total, vs 500 XP for non-Alchemist)
- Potency Boost available at level-up after just 500 XP (vs 1500 XP for non-alchemist class)
- When using Potency Boost: Base +20% potency becomes +50% potency (base +30% synergy)
- Extra reagent cost: 16 reagents instead of 20 (20% reduction)

#### Tracked Actions

Actions that grant XP to the Alchemist class:

| Action Category     | Specific Actions                                  | XP Value           |
| ------------------- | ------------------------------------------------- | ------------------ |
| Brewing             | Brew healing, buff, utility, or specialty potions | 10-100 per batch   |
| Experimentation     | Test new reagent combinations, discover recipes   | 20-50 per attempt  |
| Refining            | Purify reagents, extract essences                 | 5-30 per batch     |
| Identification      | Identify unknown reagents, analyze potions        | 5-15 per item      |
| Transmutation       | Perform alchemical transmutations                 | 50-200 per attempt |
| Reagent Gathering   | Harvest herbs, minerals, magical components       | 2-10 per gather    |
| Antidote Creation   | Create antidotes to poisons, diseases             | 20-60 per antidote |
| Commission Work     | Complete customer orders                          | Bonus +50%         |
| Masterwork Creation | Create exceptional quality potion                 | 150-400            |

#### Class Consolidation

See [Class Consolidation System](../../../systems/character/class-consolidation.md) for full mechanics.

| Consolidation Path                            | Requirements                                   | Result Class     | Tier |
| --------------------------------------------- | ---------------------------------------------- | ---------------- | ---- |
| [Artisan](../consolidation/index.md)          | Alchemist + any Crafter class                  | Artisan          | 1    |
| [Chemist](../consolidation/index.md)          | Alchemist + [Cook](../consolidation/index.md)  | Chemist          | 1    |
| [Herbalist-Brewer](../consolidation/index.md) | Alchemist + [Herbalist](../gathering/index.md) | Herbalist-Brewer | 1    |
| [Battlemage](../magic/index.md)               | Alchemist + [Mage](../magic/index.md)          | Battlemage       | 1    |
| [Artificer](../consolidation/index.md)        | Master in 3+ crafting classes                  | Artificer        | 3    |

### Interactions

| System                                                        | Interaction                                                                     |
| ------------------------------------------------------------- | ------------------------------------------------------------------------------- |
| [Crafting](../../../systems/crafting/crafting-progression.md) | Primary potion crafting class; unlocks alchemy recipes                          |
| [Economy](../../../systems/economy/index.md)                  | Can establish alchemy shop; steady demand for consumables                       |
| [Combat](../../../systems/combat/index.md)                    | Potions provide crucial buffs and healing                                       |
| [Magic](../../../systems/character/magic-system.md)           | Some potions replicate spell effects without mana cost                          |
| [Gathering](../../../systems/crafting/gathering.md)           | Synergy: [Herbalist](../gathering/index.md) + Alchemist = powerful combinations |
| [Settlement](../../../systems/world/settlements.md)           | Alchemy shops provide health and buff services                                  |
| [Exploration](../../../systems/world/exploration.md)          | Potions enable deeper delves into dangerous areas                               |

---

## Related Content

- **Requires:** Alchemy kit, brewing equipment, knowledge of reagents
- **Materials Used:** [Reagents](../../materials/index.md), [Herbs](../../materials/index.md), [Magical Components](../../materials/index.md)
- **Creates:** Healing potions, buff potions, utility elixirs, transmutation reagents
- **Recipes:** [Alchemy Recipes](../../../systems/crafting/index.md)
- **Synergy Classes:** [Herbalist](../gathering/index.md), [Mage](../magic/index.md), [Merchant](../trade/index.md)
- **See Also:** [Crafting Progression](../../../systems/crafting/crafting-progression.md), [Magic System](../../../systems/character/magic-system.md)

## Tags

- `Crafting`
- `Crafting/Alchemy`
- `Potion Brewing`
- `Transmutation`
