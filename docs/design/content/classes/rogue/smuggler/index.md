---
title: 'Smuggler'
type: 'class'
category: 'trade'
tier: 2
prerequisite_xp: 5000
prerequisite_actions: smuggling
summary: 'Covert transporter specializing in moving illegal goods across borders and past authorities'
dark_path: true
tags:
  - Trade
  - Criminal
  - Transportation
---

# Smuggler

> **Dark Path Class**: This class involves illegal transport and border violations with severe consequences if caught. Discovery damages reputation, triggers legal penalties including confiscation, imprisonment, or worse. Underground reputation operates separately from public standing.

## Lore

### Origin

Governments restrict trade - outlawing certain goods, imposing tariffs, controlling movement across borders. These restrictions create profit opportunities for those willing to circumvent them. Smugglers are essentially specialized Caravaners operating outside legal frameworks, transporting contraband where legitimate trade cannot. Where Caravaners follow approved routes with declared cargo, Smugglers use secret paths carrying forbidden goods.

The profession emerged alongside trade restrictions. Early Smugglers evaded simple customs barriers, hiding valuables to avoid tariffs. As enforcement improved, smuggling sophisticated - developing hidden compartments, bribing officials, establishing underground routes, creating signals and codes. Modern Smugglers run operations rivaling legitimate shipping in complexity, moving everything from luxury goods avoiding taxes to genuinely dangerous contraband.

Successful smuggling requires skills beyond normal transport. Smugglers must find routes avoiding official checkpoints. They must hide cargo effectively against increasingly thorough searches. They must bribe or evade enforcement officers. They must manage extreme risks - confiscation, imprisonment, execution in harsh jurisdictions. They must maintain operational security - one informant destroys entire smuggling networks. Most importantly, they must judge which jobs to accept - some contraband carries death sentences.

### In the World

Smuggling exists everywhere governments restrict trade. Border regions see constant cat-and-mouse between Smugglers and customs officials. Ports with high tariffs have extensive smuggling operations. Cities prohibiting certain goods develop underground distribution networks. Even internal trade restrictions create smuggling opportunities - transporting goods from low-price to high-price regions despite bans.

Smugglers specialize by contraband type. Some focus on tariff evasion - moving legal goods illicitly to avoid taxes, less dangerous but lower margins. Others transport restricted luxuries - substances, literature, art - serving wealthy clients willing to pay premiums. The most dangerous Smugglers move truly forbidden goods - weapons to restricted areas, dangerous magical items, fugitives, information. Each category requires different routes, methods, and risk tolerance.

The profession demands dual operation modes. Smugglers often maintain legitimate trade operations as cover - licensed Caravaners who occasionally carry hidden cargo, merchants who smuggle alongside legal goods, fishermen supplementing income with coastal smuggling. This provides plausible reasons for travel while generating legitimate income. Others operate purely underground, known only in criminal networks, specializing in highest-risk/highest-reward contraband.

Apprentice Smugglers typically join established operations as cargo handlers or guards, learning routes and methods. They practice hiding goods, reading officials to identify bribable ones versus zealots, and managing border crossings. They discover that successful smuggling depends more on intelligence, planning, and paying the right people than on stealth and daring.

Master Smugglers control entire smuggling routes - knowing every crossing point, safe house, corrupt official, and hidden path. They run networks transporting massive contraband volumes while maintaining plausible legitimacy. They become invaluable to criminal organizations requiring reliable transport. Some accumulate enough wealth and power to effectively operate private logistics companies serving underground economy.

---

## Mechanics

### Prerequisites

| Requirement        | Value                                           |
| ------------------ | ----------------------------------------------- |
| XP Threshold       | 5,000 XP from smuggling tracked actions         |
| Related Foundation | [Rogue](../rogue/) (optional, provides bonuses) |
| Tag Depth Access   | 2 levels (e.g., `[Trade/Illegal]`)              |

### Requirements

| Requirement       | Value                                                                    |
| ----------------- | ------------------------------------------------------------------------ |
| Unlock Trigger    | Successfully transport illegal goods past authorities                    |
| Primary Attribute | AWR (Awareness), CHA (Charisma)                                          |
| Starting Level    | 1                                                                        |
| Tools Required    | Transport, hidden compartments, route knowledge                          |
| Prerequisite      | Often requires [Caravaner](../trader/caravaner/) or [Trader](../trader/) |

### Stats

#### Base Class Stats

| Level | HP Bonus | Stamina Bonus | Trait               |
| ----- | -------- | ------------- | ------------------- |
| 1     | +7       | +13           | Apprentice Smuggler |
| 10    | +21      | +40           | Journeyman Smuggler |
| 25    | +45      | +85           | Master Smuggler     |
| 50    | +80      | +150          | Legendary Smuggler  |

#### Smuggling Bonuses

| Class Level | Contraband Profit | Detection Avoid | Route Knowledge | Corruption |
| ----------- | ----------------- | --------------- | --------------- | ---------- |
| 1-9         | +30%              | +15%            | +10%            | +10%       |
| 10-24       | +80%              | +35%            | +30%            | +25%       |
| 25-49       | +150%             | +65%            | +65%            | +50%       |
| 50+         | +250%             | +100%           | +120%           | +85%       |

#### Essential Starting Skills

| Skill               | Type     | Source  | Effect                               |
| ------------------- | -------- | ------- | ------------------------------------ |
| Smuggler's Instinct | Mechanic | Stealth | Hiding contraband and sensing danger |
| Shadow Step         | Lesser   | Stealth | Avoiding detection and moving unseen |

#### Synergy Skills

Skills that have strong synergies with Smuggler. These skills can be learned by any character, but Smugglers gain significant bonuses (2x XP acquisition, enhanced effectiveness, reduced costs). Higher Smuggler levels provide stronger synergy bonuses.

**Stealth Skills**:

- [Shadow Step](../../skills/tiered/shadow-step.md) - Lesser/Greater/Enhanced - Move unseen through dangerous areas
- [Vanish](../../skills/cooldown/vanish.md) - Lesser/Greater/Enhanced - Disappear when detection is imminent
- [Evasion Expert](../../skills/tiered/evasion-expert.md) - Lesser/Greater/Enhanced - Avoid pursuit and capture
- [Smugglers Instinct](../../skills/mechanic-unlock/smugglers-instinct.md) - Lesser/Greater/Enhanced - Sense danger and opportunities

**Dark Path Skills**:

- [Underworld Connections](../../skills/passive-generator/underworld-connections.md) - Passive Generator - Criminal network contacts
- [False Identity](../../skills/mechanic-unlock/false-identity.md) - Lesser/Greater/Enhanced - Create cover identities and fake papers
- [Money Laundering](../../skills/mechanic-unlock/money-laundering.md) - Lesser/Greater/Enhanced - Clean contraband profits

**Trade Skills**:

- [Market Sense](../../skills/tiered/market-sense.md) - Lesser/Greater/Enhanced - Know contraband prices and demand
- [Silver Tongue](../../skills/tiered/silver-tongue.md) - Lesser/Greater/Enhanced - Deceive officials and negotiate
- [Trade Network](../../skills/passive-generator/trade-network.md) - Passive Generator - Underground distribution contacts
- [Caravan Master](../../skills/mechanic-unlock/caravan-master.md) - Lesser/Greater/Enhanced - Manage smuggling operations

#### Synergy Bonuses

Smuggler provides context-specific bonuses to contraband transport skills based on logical specialization:

**Core Trade Skills** (Direct specialization - Strong synergy):

- **Route Knowledge**: Essential for avoiding authorities
  - Faster learning: 2x XP from route navigation (scales with class level)
  - Better effectiveness: +25% route efficiency at Smuggler 15, +35% at Smuggler 30+
  - Reduced cost: -25% stamina at Smuggler 15, -35% at Smuggler 30+
- **Customs Evasion**: Directly tied to successful smuggling
  - Faster learning: 2x XP from evasion actions
  - Better success rate: +30% checkpoint evasion at Smuggler 15
  - Lower risk: -50% chance of detection
- **Hidden Compartments**: Core skill for concealing cargo
  - Faster learning: 2x XP from concealment actions
  - Better hiding: +20% concealment effectiveness at Smuggler 15
  - Larger capacity: +20% hidden cargo space at Smuggler 15
- **Border Crossing**: Fundamental to international smuggling
  - Faster learning: 2x XP from border crossings
  - Better success: +25% crossing success at Smuggler 15
  - More routes: +50% chance to discover new crossing points

**Dark Pool Synergies** (Criminal expertise - Strong synergy):

- **Shadow Network**: Natural overlap with criminal contacts
  - Faster learning: 2x XP from underground networking
  - Better connections: +20% access to criminal services at Smuggler 15
- **Lockmaster**: Useful for accessing secure areas and cargo
  - Faster learning: 1.5x XP from lock-related actions
  - Better effectiveness: +15% lockpicking at Smuggler 15

**Stealth Pool Synergies** (Covert operations - Strong synergy):

- **Silent Step**: Essential for avoiding detection
  - Faster learning: 2x XP from stealth movement
  - Better stealth: +20% movement silence at Smuggler 15
- **Shadow Meld**: Useful for hiding from patrols
  - Faster learning: 1.5x XP from concealment actions
  - Better hiding: +15% shadow concealment at Smuggler 15

**Synergy Strength Scales with Class Level**:

| Smuggler Level | XP Multiplier | Effectiveness Bonus | Cost Reduction | Example: Route Knowledge |
| -------------- | ------------- | ------------------- | -------------- | ------------------------ |
| Level 5        | 1.5x (+50%)   | +15%                | -15% stamina   | Good but moderate        |
| Level 10       | 1.75x (+75%)  | +20%                | -20% stamina   | Strong improvements      |
| Level 15       | 2.0x (+100%)  | +25%                | -25% stamina   | Excellent synergy        |
| Level 30+      | 2.5x (+150%)  | +35%                | -35% stamina   | Masterful synergy        |

**Example Progression**:

A Smuggler 15 learning Route Knowledge:

- Performs 50 navigation actions (earning 2x XP = 1000 XP total, vs 500 XP for non-Smuggler)
- Route Knowledge available at level-up after just 500 XP (vs 1500 XP for non-trade class)
- When using Route Knowledge: Base route efficiency becomes +25% better (synergy bonus)
- Stamina cost: 7.5 stamina instead of 10 (25% reduction)

#### Tracked Actions

Actions that grant XP to the Smuggler class:

| Action Category      | Specific Actions                                | XP Value                |
| -------------------- | ----------------------------------------------- | ----------------------- |
| Contraband Transport | Successfully move illegal goods                 | 20-150 per run          |
| Border Crossing      | Cross guarded borders with contraband           | 15-80 per crossing      |
| Customs Evasion      | Avoid or deceive official inspections           | 10-60 per evasion       |
| Route Development    | Establish new smuggling routes                  | 50-250 per route        |
| Official Corruption  | Successfully bribe enforcement officers         | 15-70 per bribe         |
| Emergency Escape     | Evade capture with cargo intact                 | 30-150 per escape       |
| Network Building     | Establish smuggling contacts and infrastructure | 20-100 per contact      |
| High-Risk Cargo      | Transport extremely dangerous contraband        | 80-400 per run          |
| Large Operation      | Move substantial contraband volumes             | 50-300 per operation    |
| Route Mastery        | Dominate major smuggling route                  | 100-500 per achievement |

#### Class Consolidation

See [Class Consolidation System](../../../systems/character/class-consolidation.md) for full mechanics.

| Consolidation Path                            | Requirements                                 | Result Class     | Tier |
| --------------------------------------------- | -------------------------------------------- | ---------------- | ---- |
| [Smuggling Lord](../consolidation/index.md)   | Smuggler + control major routes              | Smuggling Lord   | 1    |
| [Blockade Runner](../consolidation/index.md)  | Smuggler + naval focus                       | Blockade Runner  | 1    |
| [Shadow Caravaner](../consolidation/index.md) | Smuggler + [Scout](../combat/scout.md)       | Shadow Caravaner | 1    |
| [Contraband Baron](../consolidation/index.md) | Multiple smuggling operations + vast network | Contraband Baron | 2    |

### Interactions

| System                                                       | Interaction                                    |
| ------------------------------------------------------------ | ---------------------------------------------- |
| [Black Market](../../../systems/economy/black-market.md)     | Essential transport for underground economy    |
| [Travel](../../../systems/world/travel.md)                   | Uses covert routes avoiding official paths     |
| [Crime](../../../systems/social/index.md)                    | Major criminal activity; severe penalties      |
| [Reputation](../../../systems/social/factions-reputation.md) | Dual reputations: public cover and underground |
| [Borders & Customs](../../../systems/world/index.md)         | Violates trade restrictions and borders        |
| [Law Enforcement](../../../systems/social/index.md)          | High-priority target for authorities           |

---

## Related Content

- **Requires:** Transport (wagon/ship), hidden compartments, route knowledge, contacts
- **Equipment:** [Cargo Wagon](../../items/index.md), [Hidden Compartments](../../items/index.md), [False Papers](../../items/index.md)
- **Prerequisite:** Often consolidates from [Caravaner](../trader/caravaner/) or [Trader](../trader/)
- **Synergy Classes:** [Fence](./fence.md), [Thief](./thief.md), [Scout](../combat/scout.md)
- **Consolidates To:** [Smuggling Lord](../consolidation/index.md), [Blockade Runner](../consolidation/index.md), [Contraband Baron](../consolidation/index.md)
- **See Also:** [Black Market System](../../../systems/economy/black-market.md), [Trade Routes](../../../systems/economy/index.md), [Crime & Punishment](../../../systems/social/index.md)
