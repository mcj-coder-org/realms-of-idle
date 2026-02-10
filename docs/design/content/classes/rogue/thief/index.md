---
title: 'Thief'
type: 'class'
category: 'trade'
tier: 2
prerequisite_xp: 5000
prerequisite_actions: theft
summary: 'Stealth-based acquisitioner taking property without permission or payment'
dark_path: true
tags:
  - Criminal
  - Stealth
  - Theft
---

# Thief

> **Dark Path Class**: This class involves illegal activities with severe consequences if caught. Discovery damages reputation and triggers legal penalties. Underground reputation operates separately from public standing.

## Lore

### Origin

Property ownership creates opportunity for theft. Where goods have value and security has gaps, Thieves operate. Unlike Traders who acquire through exchange or Merchants who purchase inventory, Thieves simply take. Society condemns them universally - theft violates fundamental social contracts. Yet theft persists across all societies, driven by poverty, greed, thrill-seeking, or survival necessity.

The Thief class encompasses diverse practitioners. Desperate poor stealing food to survive. Professional criminals targeting valuable goods. Pickpockets working crowds. Burglars entering homes. Organized theft rings operating systematically. Each type shares core skills - moving unseen, bypassing security, acquiring goods without detection - but motivations and methods vary enormously. Some Thieves rationalize by targeting wealthy who won't miss losses. Others embrace criminality without guilt. Most occupy uncomfortable middle ground.

Successful thievery requires more than boldness. Thieves must case targets, identifying valuable goods and security weaknesses. They must move silently and remain unnoticed in prohibited spaces. They must bypass locks, disable traps, and overcome physical barriers. They must fence stolen goods since directly selling identifiable items invites arrest. They must manage risk - knowing when jobs are too dangerous versus when rewards justify risks. Amateur Thieves get caught quickly; professionals survive through careful planning and strict discipline.

### In the World

Every settlement has Thieves, from desperate children stealing bread to professional criminals targeting merchant warehouses. Poverty drives much theft - those without legitimate economic access resort to illegitimate methods. Inequality creates resentment - why should nobles have excess while others starve? Opportunity invites crime - wealthy travelers displaying valuables become targets. Weak enforcement encourages theft - if guards rarely catch criminals, deterrence fails.

Urban environments favor Thieves. Crowds enable anonymous movement. Buildings provide hiding spots and entry points. Markets offer numerous targets. Complex layouts create escape routes. Rural areas present fewer opportunities but also less security - isolated farms are vulnerable when occupants leave. Roads between settlements attract bandits who are essentially specialized Thieves preying on travelers.

Thieves Guilds organize professional criminals in major cities. They establish territories, resolve disputes between members, fence stolen goods, corrupt officials, and occasionally enforce codes against freelance operators. Membership offers protection and resources but demands tribute and obedience. Independent Thieves face hostility from both law and organized crime.

Apprentice Thieves often start as lookouts or accomplices, learning from experienced criminals. They practice lockpicking, stealth movement, security assessment, and escape techniques. Smart apprentices study guard patterns, learn fence contacts, and develop caution about job selection. Many get caught during early crimes - harsh punishments deter some while hardening others.

Veteran Thieves develop specialized expertise. Master locksmiths open any lock. Acrobatic burglars enter through seemingly impossible entry points. Social engineers gain access through deception rather than stealth. Safe crackers defeat complex security mechanisms. Each specialty commands respect and higher shares from collaborative jobs. The best Thieves retire wealthy - most die in custody or during botched jobs.

---

## Mechanics

### Prerequisites

| Requirement        | Value                                           |
| ------------------ | ----------------------------------------------- |
| XP Threshold       | 5,000 XP from theft tracked actions             |
| Related Foundation | [Rogue](../rogue/) (optional, provides bonuses) |
| Tag Depth Access   | 2 levels (e.g., `[Criminal/Theft]`)             |

### Requirements

| Requirement       | Value                                      |
| ----------------- | ------------------------------------------ |
| Unlock Trigger    | Successfully steal items without detection |
| Primary Attribute | FIN (Finesse), AWR (Awareness)             |
| Starting Level    | 1                                          |
| Tools Required    | Lockpicks, dark clothing, theft tools      |

### Stats

#### Base Class Stats

| Level | HP Bonus | Stamina Bonus | Trait            |
| ----- | -------- | ------------- | ---------------- |
| 1     | +5       | +12           | Apprentice Thief |
| 10    | +15      | +35           | Journeyman Thief |
| 25    | +30      | +75           | Master Thief     |
| 50    | +55      | +135          | Legendary Thief  |

#### Criminal Bonuses

| Class Level | Stealth | Lockpicking | Detection Avoid | Theft Success |
| ----------- | ------- | ----------- | --------------- | ------------- |
| 1-9         | +15%    | +20%        | +10%            | +15%          |
| 10-24       | +35%    | +50%        | +25%            | +40%          |
| 25-49       | +65%    | +85%        | +50%            | +75%          |
| 50+         | +100%   | +100%       | +85%            | +100%         |

#### Essential Starting Skills

| Skill       | Type   | Source  | Effect                                    |
| ----------- | ------ | ------- | ----------------------------------------- |
| Lockmaster  | Lesser | Stealth | Picking locks and bypassing security      |
| Shadow Step | Lesser | Stealth | Stealth movement and remaining undetected |

#### Synergy Skills

Skills that have strong synergies with Thief. These skills can be learned by any character, but Thieves gain significant bonuses (2x XP acquisition, enhanced effectiveness, reduced costs). Higher Thief levels provide stronger synergy bonuses.

**Stealth Skills**:

- [Shadow Step](../../skills/tiered/shadow-step/index.md) - Lesser/Greater/Enhanced - Blend into shadows and move unseen
- [Lockmaster](../../skills/tiered/lockmaster/index.md) - Lesser/Greater/Enhanced - Pick locks with exceptional skill
- [Backstab](../../skills/tiered/backstab/index.md) - Lesser/Greater/Enhanced - Strike from hiding for massive damage
- [Vanish](../../skills/cooldown/vanish/index.md) - Lesser/Greater/Enhanced - Disappear from sight instantly
- [Evasion Expert](../../skills/tiered/evasion-expert/index.md) - Lesser/Greater/Enhanced - Avoid attacks with supernatural agility
- [Misdirection](../../skills/tiered/misdirection/index.md) - Lesser/Greater/Enhanced - Deceive and mislead targets

**Dark Path Skills**:

- [Underworld Connections](../../skills/passive-generator/underworld-connections/index.md) - Passive Generator - Criminal network contacts
- [False Identity](../../skills/mechanic-unlock/false-identity/index.md) - Lesser/Greater/Enhanced - Create and maintain fake identities
- [Evidence Elimination](../../skills/tiered/evidence-elimination/index.md) - Lesser/Greater/Enhanced - Remove traces of crimes

**Trade Skills**:

- [Market Sense](../../skills/tiered/market-sense/index.md) - Lesser/Greater/Enhanced - Know prices and opportunities
- [Fencing](../../skills/mechanic-unlock/fencing/index.md) - Lesser/Greater/Enhanced - Sell stolen goods efficiently
- [Trade Contacts](../../skills/passive-generator/trade-contacts/index.md) - Passive Generator - Network of buyers and sellers

#### Synergy Bonuses

Thief provides context-specific bonuses to theft and stealth skills based on logical specialization:

**Core Thief Skills** (Direct specialization - Strong synergy):

- **Lockpicking**: Essential for accessing secured locations
  - Faster learning: 2x XP from lockpicking actions (scales with class level)
  - Better effectiveness: +25% success rate at Thief 15, +35% at Thief 30+
  - Reduced cost: -25% stamina at Thief 15, -35% at Thief 30+
- **Silent Movement**: Directly tied to undetected theft
  - Faster learning: 2x XP from stealth actions
  - Better stealth: +30% silence at Thief 15
  - Lower detection: -50% chance of being heard
- **Pickpocket**: Core skill for theft from people
  - Faster learning: 2x XP from pickpocketing
  - Better success: +20% theft success at Thief 15
  - Lower detection: -20% chance of target noticing at Thief 15
- **Security Analysis**: Fundamental to successful burglary
  - Faster learning: 2x XP from security assessment
  - Better analysis: +25% threat detection at Thief 15
  - More details: +50% chance to identify all security measures

**Dark Pool Synergies** (Criminal expertise - Strong synergy):

- **Shadow Network**: Natural overlap with criminal contacts
  - Faster learning: 2x XP from underground networking
  - Better connections: +20% access to criminal services at Thief 15
- **Lockmaster**: Core thief skill with enhanced effectiveness
  - Faster learning: 2x XP from advanced lockpicking
  - Better mastery: +25% complex lock success at Thief 15
- **Backstab**: Useful for self-defense during thefts
  - Faster learning: 1.5x XP from stealth attacks
  - Better damage: +15% backstab damage at Thief 15

**Stealth Pool Synergies** (Covert operations - Strong synergy):

- **Silent Step**: Essential for burglary and theft
  - Faster learning: 2x XP from silent movement
  - Better stealth: +25% movement silence at Thief 15
- **Shadow Meld**: Core skill for hiding from guards
  - Faster learning: 2x XP from shadow concealment
  - Better hiding: +20% shadow concealment at Thief 15
- **Night Vision**: Useful for operating in darkness
  - Faster learning: 1.5x XP from night operations
  - Better vision: +15% dark vision at Thief 15

**Synergy Strength Scales with Class Level**:

| Thief Level | XP Multiplier | Effectiveness Bonus | Cost Reduction | Example: Lockpicking |
| ----------- | ------------- | ------------------- | -------------- | -------------------- |
| Level 5     | 1.5x (+50%)   | +15%                | -15% stamina   | Good but moderate    |
| Level 10    | 1.75x (+75%)  | +20%                | -20% stamina   | Strong improvements  |
| Level 15    | 2.0x (+100%)  | +25%                | -25% stamina   | Excellent synergy    |
| Level 30+   | 2.5x (+150%)  | +35%                | -35% stamina   | Masterful synergy    |

**Example Progression**:

A Thief 15 learning Lockpicking:

- Performs 50 lockpicking actions (earning 2x XP = 1000 XP total, vs 500 XP for non-Thief)
- Lockpicking available at level-up after just 500 XP (vs 1500 XP for non-criminal class)
- When using Lockpicking: Base success rate becomes +25% better (synergy bonus)
- Stamina cost: 7.5 stamina instead of 10 (25% reduction)

#### Tracked Actions

Actions that grant XP to the Thief class:

| Action Category    | Specific Actions                                 | XP Value             |
| ------------------ | ------------------------------------------------ | -------------------- |
| Theft              | Successfully steal items without detection       | 10-100 per theft     |
| Pickpocketing      | Steal from people directly                       | 5-40 per target      |
| Lockpicking        | Pick locks on doors, chests, safes               | 5-30 per lock        |
| Burglary           | Enter and steal from secured locations           | 20-120 per job       |
| Security Bypass    | Overcome security measures                       | 10-60 per bypass     |
| Fence Transactions | Sell stolen goods through black market           | 5-50 per transaction |
| Heist              | Execute complex multi-stage thefts               | 50-300 per heist     |
| Escape             | Evade capture after crimes                       | 15-80 per escape     |
| Underground Work   | Complete jobs for Thieves Guild                  | 20-100 per job       |
| Legendary Theft    | Steal extremely valuable or well-protected items | 100-500 per theft    |

#### Class Consolidation

See [Class Consolidation System](../../../systems/character/class-consolidation/index.md) for full mechanics.

| Consolidation Path                           | Requirements                                        | Result Class    | Tier |
| -------------------------------------------- | --------------------------------------------------- | --------------- | ---- |
| [Master Thief](../consolidation/index.md)    | Thief + extensive successful thefts                 | Master Thief    | 1    |
| [Infiltrator](../consolidation/index.md)     | Thief + [Scout](../combat/scout/index.md)           | Infiltrator     | 1    |
| [Treasure Hunter](../consolidation/index.md) | Thief + [Adventurer](../combat/adventurer/index.md) | Treasure Hunter | 1    |
| [Guild Master](../consolidation/index.md)    | Thief + thieves guild leadership                    | Guild Master    | 2    |

### Interactions

| System                                                             | Interaction                                        |
| ------------------------------------------------------------------ | -------------------------------------------------- |
| [Crime](../../../systems/social/index.md)                          | Primary criminal class; severe penalties if caught |
| [Stealth](../../../systems/combat/index.md)                        | Master of remaining undetected                     |
| [Reputation](../../../systems/social/factions-reputation/index.md) | Separate public and underground reputations        |
| [Black Market](../../../systems/economy/black-market/index.md)     | Primary source of stolen goods                     |
| [Security](../../../systems/world/index.md)                        | Bypasses locks, traps, guards                      |
| [Law Enforcement](../../../systems/social/index.md)                | Major target of guard attention                    |

---

## Related Content

- **Requires:** Lockpicks, dark clothing, stealth tools, fence contacts
- **Equipment:** [Lockpicks](../../items/index.md), [Dark Cloak](../../items/index.md), [Climbing Gear](../../items/index.md)
- **Synergy Classes:** [Fence](./fence/index.md), [Scout](../combat/scout/index.md), [Assassin](../combat/assassin/index.md)
- **Consolidates To:** [Master Thief](../consolidation/index.md), [Infiltrator](../consolidation/index.md), [Treasure Hunter](../consolidation/index.md)
- **See Also:** [Crime System](../../../systems/social/index.md), [Black Market](../../../systems/economy/black-market/index.md), [Stealth Mechanics](../../../systems/combat/index.md)
