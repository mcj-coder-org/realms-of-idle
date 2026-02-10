---
title: 'Summoner'
type: 'class'
category: 'magic'
tier: 2
prerequisite_xp: 5000
prerequisite_actions: summoning
summary: 'Conjuration specialist summoning creatures and entities to serve temporarily'
---

# Summoner

## Tags

> **Required Section:** Tags enable compile-time safe tag access in C# code and drive the skill/class/recipe systems.

### C# Reference

**Strongly Typed Tags:** Use these tags in C# code for compile-time safety.

| Tag Path                      | C# Reference                            | Purpose               |
| ----------------------------- | --------------------------------------- | --------------------- |
| `Magic/Conjuration`           | `SkillTags.Magic.Conjuration.Value`     | Summoner class access |
| `Magic/Conjuration/Summoning` | `SkillTags.Magic.Conjuration.Summoning` | Creature summoning    |
| `Planes/Access`               | `SkillTags.Planes.Access.Value`         | Planar travel         |

**How to find class tags:**

1. Check `ClassDefinition.GrantedTags` in class definition files (e.g., `src/CozyFantasyRpg/Content/Classes/MagicClasses.cs`)
2. Verify against `src/CozyFantasyRpg/Shared/SkillTags.cs` for exact C# reference

### Tag Access

This class requires the following tags for access/synergy:

| Tag                           | Depth | Classes With Access                 |
| ----------------------------- | ----- | ----------------------------------- |
| `Magic/Conjuration`           | 2     | Summoner, Mage, Channeler           |
| `Magic/Conjuration/Summoning` | 3     | Summoner (full access to summoning) |
| `Planes/Access`               | 2     | Summoner, Planar specialists        |

**Note:** Tag depth determines which classes can access this content. See [Tag System](../../../systems/content/tag-system.md) for details.

---

## Lore

### Origin

Reality has layers. Beyond the physical world lie other planes - realms of elementals, domains of spirits, dimensions housing strange creatures. Conjuration magic creates temporary bridges between these planes, allowing Summoners to call beings from elsewhere to serve briefly in the physical world. A Summoner never fights alone - they command elemental servants, spirit allies, and conjured creatures to act on their behalf.

The art emerged from understanding planar boundaries and the nature of temporary existence. Early conjurers discovered that certain spells could pierce the veil between worlds, creating openings through which beings could pass. More importantly, they learned to establish contracts - magical agreements binding summoned entities to temporary service. Without proper binding, summoned creatures either return instantly or become dangerous uncontrolled threats. Mastering both summoning and binding makes the difference between Summoners and reckless fools who accidentally unleash horrors.

Modern Summoning requires extensive knowledge beyond spell formulae. Summoners study other planes - their nature, inhabitants, and rules. They learn what beings can be summoned and what services they can perform. They understand binding contracts - how to establish clear terms, maintain control, and properly dismiss summons when service ends. They develop the mental discipline to maintain multiple summoned entities simultaneously. This scholarly foundation distinguishes Summoners from general Mages.

### In the World

Summoners occupy unusual social positions. Their power is respected - commanding multiple servants provides enormous versatility. Yet their craft inspires unease - tearing holes between dimensions seems dangerous, and many worry about what might slip through unauthorized. Most settlements tolerate Summoners but restrict dangerous summonings near populated areas. Careless Summoners who lose control of dangerous entities face harsh penalties.

The profession attracts those who prefer indirect engagement. Where Warriors fight personally and Mages cast spells directly, Summoners direct subordinates. This appeals to strategic minds who enjoy commanding forces and tactical thinkers who value having disposable assets. It also suits those physically fragile - Summoners can remain safe behind summoned defenders while those servants engage threats.

Summoner specializations reflect different planar focuses. Elementalists summon elemental beings - fire, water, earth, air creatures providing diverse abilities. Spirit Callers work with spiritual entities, often incorporating ancestor worship or nature religion. Planar Specialists focus on specific dimensions, developing deep relationships with particular planes' inhabitants. Each specialty requires different knowledge, though all share core summoning mechanics.

Apprentice Summoners begin with simple summonings - minor elementals, small spirits, weak creatures. They learn to establish binding contracts, maintain control over summoned entities, and properly dismiss them when service concludes. They study planar theory, understanding what they're actually doing when piercing dimensional boundaries. Early mistakes teach important lessons - losing control of even weak summons creates dangerous situations.

Master Summoners command impressive forces. They summon powerful elementals, major spirits, even demon or angel equivalents from appropriate planes. They maintain multiple summons simultaneously, creating small armies under their control. They negotiate sophisticated contracts, obtaining greater services from more intelligent entities. The most powerful develop reputations across planes - beings know their names and may refuse or eagerly accept summons based on past interactions.

---

## Mechanics

### Prerequisites

| Requirement        | Value                                                 |
| ------------------ | ----------------------------------------------------- |
| XP Threshold       | 5,000 XP from summoning tracked actions               |
| Related Foundation | [Channeler](../index.md) (optional, provides bonuses) |
| Tag Depth Access   | 2 levels (e.g., `[Magic/Summoning]`)                  |

### Requirements

| Requirement       | Value                                           |
| ----------------- | ----------------------------------------------- |
| Unlock Trigger    | Successfully summon creatures from other planes |
| Primary Attribute | INT (Intelligence), CHA (Charisma)              |
| Starting Level    | 1                                               |
| Tools Required    | Summoning focus, ritual materials               |
| Prerequisite      | Often requires [Mage](./mage/index.md) class    |

### Stats

#### Base Class Stats

| Level | HP Bonus | Mana Bonus | Trait               |
| ----- | -------- | ---------- | ------------------- |
| 1     | +4       | +18        | Apprentice Summoner |
| 10    | +12      | +55        | Journeyman Summoner |
| 25    | +25      | +115       | Master Summoner     |
| 50    | +45      | +210       | Legendary Summoner  |

#### Summoning Bonuses

| Class Level | Summon Power | Duration | Control Capacity | Bind Success |
| ----------- | ------------ | -------- | ---------------- | ------------ |
| 1-9         | +15%         | +20%     | 1 creature       | 75%          |
| 10-24       | +40%         | +50%     | 2 creatures      | 90%          |
| 25-49       | +85%         | +100%    | 4 creatures      | 98%          |
| 50+         | +150%        | +180%    | 8 creatures      | 100%         |

#### Starting Skills

| Skill                                                      | Type    | Effect                                           |
| ---------------------------------------------------------- | ------- | ------------------------------------------------ |
| Basic Summoning                                            | Active  | Can summon weak creatures temporarily            |
| [Spell Focus](../../skills/tiered/spell-focus.md) (Lesser) | Passive | Enhanced summoning control and binding precision |
| [Mana Well](../../skills/tiered/mana-well.md) (Lesser)     | Passive | Expanded mana for sustaining summoned creatures  |

#### Synergy Skills

Skills that have strong synergies with Summoner. These skills can be learned by any character, but Summoners gain significant bonuses (2x XP acquisition, enhanced effectiveness, reduced costs). Higher Summoner levels provide stronger synergy bonuses.

**Core Magic Skills**:

- [Spell Focus](../../skills/tiered/spell-focus.md) - Lesser/Greater/Enhanced - Enhanced spell effectiveness
- [Mana Well](../../skills/tiered/mana-well.md) - Lesser/Greater/Enhanced - Expanded magical reserves
- [School Mastery](../../skills/mechanic-unlock/school-mastery.md) - Mechanic - Deep expertise in one school
- [Ritual Casting](../../skills/mechanic-unlock/ritual-casting.md) - Lesser/Greater/Enhanced - Perform elaborate magical rituals
- [Mana Transfer](../../skills/mechanic-unlock/mana-transfer.md) - Lesser/Greater/Enhanced - Share magical energy

**Core Summoning Skills**:

- Summon Elemental - Lesser/Greater/Enhanced - Call elemental beings
- Summon Spirit - Lesser/Greater/Enhanced - Call spiritual entities
- Extended Duration - Lesser/Greater/Enhanced - Longer summon time
- Multiple Summons - Lesser/Greater/Enhanced - Control multiple creatures
- Summon Power - Lesser/Greater/Enhanced - Stronger summons
- Fast Summon - Lesser/Greater/Enhanced - Reduced cast time
- Binding Mastery - Lesser/Greater/Enhanced - Better control
- Planar Knowledge - Lesser/Greater/Enhanced - Access more planes
- Summon Specialization - Lesser/Greater/Enhanced - Master creature type
- Dismissal Speed - Lesser/Greater/Enhanced - Quick banishment
- Summon Resilience - Lesser/Greater/Enhanced - Tougher summons
- Intelligent Summons - Lesser/Greater/Enhanced - Smarter creatures
- Permanent Summon - Lesser/Greater/Enhanced - Long-term binding
- Mass Summoning - Lesser/Greater/Enhanced - Summon many at once
- Planar Gateway - Epic - Stable summoning portal

**Note**: All skills listed have strong synergies because they are core summoning skills. Characters without Summoner class can still learn these skills but progress at base rate (1x XP) without effectiveness bonuses.

#### Synergy Bonuses

Summoner provides context-specific bonuses to conjuration-related skills based on logical specialization:

**Core Summoning Skills** (Direct specialization - Strong synergy):

- **Summon Power**: Essential for commanding powerful entities
  - Faster learning: 2x XP from summoning actions (scales with class level)
  - Better effectiveness: +25% summon strength at Summoner 15, +35% at Summoner 30+
  - Reduced cost: -25% mana at Summoner 15, -35% at Summoner 30+
- **Binding Mastery**: Directly tied to maintaining control over summons
  - Faster learning: 2x XP from binding actions
  - Better control: +30% binding strength at Summoner 15
  - Lower cost: Additional -20% mana maintenance at Summoner 15
- **Planar Knowledge**: Core skill for understanding other dimensions
  - Faster learning: 2x XP from planar study
  - Better access: +20% plane accessibility at Summoner 15
  - More variety: +50% chance to discover new summons
- **Extended Duration**: Fundamental to practical summoning
  - Faster learning: 2x XP from duration management
  - Better persistence: +25% summon duration at Summoner 15
  - More stability: +50% resistance to early dismissal

**Synergy Strength Scales with Class Level**:

| Summoner Level | XP Multiplier | Effectiveness Bonus | Cost Reduction | Example: Summon Power |
| -------------- | ------------- | ------------------- | -------------- | --------------------- |
| Level 5        | 1.5x (+50%)   | +15%                | -15% mana      | Good but moderate     |
| Level 10       | 1.75x (+75%)  | +20%                | -20% mana      | Strong improvements   |
| Level 15       | 2.0x (+100%)  | +25%                | -25% mana      | Excellent synergy     |
| Level 30+      | 2.5x (+150%)  | +35%                | -35% mana      | Masterful synergy     |

**Example Progression**:

A Summoner 15 learning Summon Power:

- Performs 50 summoning actions (earning 2x XP = 1000 XP total, vs 500 XP for non-Summoner)
- Summon Power available at level-up after just 500 XP (vs 1500 XP for non-summoning class)
- When using Summon Power: Base +50% summon strength becomes +75% summon strength (base +25% synergy)
- Mana cost: 30 mana instead of 40 (25% reduction)

#### Tracked Actions

Actions that grant XP to the Summoner class:

| Action Category    | Specific Actions                            | XP Value                |
| ------------------ | ------------------------------------------- | ----------------------- |
| Summoning          | Successfully summon creatures               | 10-80 per summon        |
| Combat Summoning   | Use summons effectively in battle           | 15-100 per battle       |
| Binding Control    | Maintain control over challenging summons   | 10-60 per maintenance   |
| Planar Study       | Study other planes and their inhabitants    | 15-80 per session       |
| Advanced Summons   | Summon powerful or rare entities            | 30-200 per summon       |
| Multiple Control   | Successfully control multiple summons       | 20-120 per event        |
| Problem Solving    | Use summons creatively to solve challenges  | 15-80 per solution      |
| Planar Negotiation | Establish favorable contracts with entities | 20-100 per negotiation  |
| Summon Research    | Discover new summonable creatures           | 40-200 per discovery    |
| Master Summoning   | Command legendary entities or summon armies | 100-500 per achievement |

#### Class Consolidation

See [Class Consolidation System](../../../systems/character/class-consolidation.md) for full mechanics.

| Consolidation Path                    | Requirements                                | Result Class  | Tier |
| ------------------------------------- | ------------------------------------------- | ------------- | ---- |
| [Planar Master](../../consolidation/) | Summoner + master multiple planes           | Planar Master | 1    |
| [Elementalist](../../consolidation/)  | Summoner + elemental specialization         | Elementalist  | 1    |
| [Beastmaster](../../consolidation/)   | Summoner + [Hunter](../../gatherer/hunter/) | Beastmaster   | 1    |
| [Archon](../../consolidation/)        | Summoner + command legendary entities       | Archon        | 2    |

### Interactions

| System                                                     | Interaction                                 |
| ---------------------------------------------------------- | ------------------------------------------- |
| [Magic System](../../../systems/character/magic-system.md) | Uses conjuration school of magic            |
| [Summoning](../../../systems/magic/index.md)               | Core summoning and binding mechanics        |
| [Planes](../../../systems/world/index.md)                  | Accesses other planes and dimensions        |
| [Combat](../../../systems/combat/combat-resolution.md)     | Summons fight on Summoner's behalf          |
| [Party](../../../systems/combat/party-mechanics.md)        | Summons count as additional party members   |
| [Mana](../../../systems/magic/index.md)                    | Summoning costs mana; maintenance over time |

---

## Related Content

- **Requires:** Summoning focus, ritual materials, planar knowledge, mana pool
- **Equipment:** [Summoning Circle](../../../item/), [Ritual Components](../../../item/), [Planar Focus](../../../item/)
- **Prerequisite:** Often consolidates from [Mage](./mage/index.md) with conjuration focus
- **Synergy Classes:** [Mage](./mage/index.md), [Hunter](../../gatherer/hunter/), [Enchanter](../enchanter/)
- **Consolidates To:** [Planar Master](../../consolidation/), [Elementalist](../../consolidation/), [Beastmaster](../../consolidation/)
- **See Also:** [Magic System](../../../systems/character/magic-system.md), [Summoning Mechanics](../../../systems/magic/index.md), [Planar Travel](../../../systems/world/index.md)
