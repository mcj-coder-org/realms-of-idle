---
title: 'Necromancer'
type: 'class'
category: 'magic'
tier: 2
prerequisite_xp: 5000
prerequisite_actions: necromancy
summary: 'Death magic specialist raising undead and manipulating life force'
dark_path: true
---

# Necromancer

## Tags

> **Required Section:** Tags enable compile-time safe tag access in C# code and drive the skill/class/recipe systems.

### C# Reference

**Strongly Typed Tags:** Use these tags in C# code for compile-time safety.

| Tag Path                   | C# Reference                         | Purpose                  |
| -------------------------- | ------------------------------------ | ------------------------ |
| `Magic/Necromancy`         | `SkillTags.Magic.Necromancy.Value`   | Necromancer class access |
| `Magic/Conjuration/Undead` | `SkillTags.Magic.Conjuration.Undead` | Undead creation          |
| `Death/Forbidden`          | `SkillTags.Death.Forbidden.Value`    | Forbidden magic          |

**How to find class tags:**

1. Check `ClassDefinition.GrantedTags` in class definition files (e.g., `src/CozyFantasyRpg/Content/Classes/MagicClasses.cs`)
2. Verify against `src/CozyFantasyRpg/Shared/SkillTags.cs` for exact C# reference

### Tag Access

This class requires the following tags for access/synergy:

| Tag                   | Depth | Classes With Access                 |
| --------------------- | ----- | ----------------------------------- |
| `Magic/Necromancy`    | 2     | Necromancer, Mage, Channeler        |
| `Magic/Conjuration/*` | 3     | Necromancer (full access to undead) |

**Note:** Tag depth determines which classes can access this content. See [Tag System](../../../systems/content/tag-system.md) for details.

---

> **Dark Path Class**: This class involves forbidden magic with severe consequences if discovered. Most settlements execute Necromancers on sight. Raising undead violates fundamental taboos about death and desecration. Underground reputation operates separately from any remaining public identity.

## Lore

### Origin

Death is natural. Reversing death is abomination. Necromancy is the forbidden art of binding departed souls back to corpses, creating undead servants that mock the natural order. Where Healers preserve life and other Mages work with living energies, Necromancers traffic in death itself - reanimating corpses, draining life force, commanding spirits who should rest peacefully. Every civilized society prohibits these practices, yet Necromancy persists through those who seek power regardless of moral or social costs.

The art emerged from studying death and what lies beyond. Early Necromancers discovered that death doesn't immediately sever all connections between soul and body. For brief periods, departed spirits linger near their corpses before moving to whatever awaits after. During this window, powerful magic can forcibly bind spirits back to their remains, creating undead - corpses animated by trapped souls. More advanced Necromancy extends this binding indefinitely, enslaves spirits without bodies, or even steals life force from living victims to fuel undead armies.

Modern Necromancy divides into theoretical justifications and practical application. Some practitioners argue they provide immortality, allowing souls to continue existing rather than facing unknown afterlife. Others claim military utility - undead soldiers never tire, feel no fear, require no supply lines. Some insist they only use abandoned corpses, harming no one. Yet all face universal condemnation - forcing souls into servitude violates autonomy absolutely, using corpses desecrates the dead, and creating undead spreads corruption that poisons living lands.

### In the World

Necromancers operate in absolute secrecy or open defiance. Those maintaining secret identities hide their practices completely, perhaps posing as Healers or Mages while conducting dark experiments in hidden lairs. They acquire corpses through grave robbing, murder, or battlefield scavenging. They work alone, knowing any witness might expose them. Discovery means execution - no trial, no mercy, just immediate death.

Some Necromancers abandon civilization entirely, establishing themselves in ruins, abandoned regions, or actively hostile territories. They build undead fortresses, raising armies from centuries of corpses. Surrounded by their creations, they pursue Necromantic research without societal constraints. These Necromancer lords become significant threats - their undead legions attack settlements, their mere presence corrupts nearby lands, their dark magic warps reality itself.

The profession attracts specific personality types. Academic Necromancers pursue forbidden knowledge, caring more about understanding death magic than ethics. Power-seeking Necromancers want immortality or undead armies for conquest. Grief-stricken Necromancers attempt raising loved ones, refusing to accept death. Genuinely evil Necromancers simply enjoy wielding power over life and death. Regardless of motivation, all share willingness to violate fundamental taboos.

Apprentice Necromancers learn through stolen texts, corrupted mentors, or dark discoveries. They begin with small reanimations - raising small animals or creating skeletal servants from old bones. They study death magic theory, understanding how life force works and how to manipulate it. They learn to hide their activities, knowing exposure brings death. Early mistakes teach caution - uncontrolled undead attack their creators, botched rituals corrupt practitioners, magical signatures alert witch hunters.

Master Necromancers command vast undead armies, achieve functional immortality through lich transformation, drain entire populations to fuel their magic, and reshape regions into undead wastelands. They become existential threats requiring heroes or armies to confront. Their names inspire terror. Their defeats become legendary tales. Yet new Necromancers always emerge, drawn to forbidden power despite knowing the certain doom that awaits.

---

## Mechanics

### Prerequisites

| Requirement        | Value                                                 |
| ------------------ | ----------------------------------------------------- |
| XP Threshold       | 5,000 XP from necromancy tracked actions              |
| Related Foundation | [Channeler](../index.md) (optional, provides bonuses) |
| Tag Depth Access   | 2 levels (e.g., `[Magic/Necromancy]`)                 |

### Requirements

| Requirement       | Value                                        |
| ----------------- | -------------------------------------------- |
| Unlock Trigger    | Reanimate corpses, cast death magic          |
| Primary Attribute | INT (Intelligence), CHA (Charisma)           |
| Starting Level    | 1                                            |
| Tools Required    | Necromantic focus, corpses, dark rituals     |
| Prerequisite      | Often requires [Mage](./mage/index.md) class |

### Stats

#### Base Class Stats

| Level | HP Bonus | Mana Bonus | Trait                  |
| ----- | -------- | ---------- | ---------------------- |
| 1     | +4       | +18        | Apprentice Necromancer |
| 10    | +12      | +55        | Journeyman Necromancer |
| 25    | +25      | +115       | Master Necromancer     |
| 50    | +45      | +210       | Lich Lord              |

#### Necromancy Bonuses

| Class Level | Undead Power | Control Capacity | Life Drain | Death Resist |
| ----------- | ------------ | ---------------- | ---------- | ------------ |
| 1-9         | +20%         | 2 undead         | +10%       | +20%         |
| 10-24       | +50%         | 5 undead         | +30%       | +50%         |
| 25-49       | +100%        | 15 undead        | +60%       | +85%         |
| 50+         | +200%        | 50 undead        | +120%      | +100%        |

#### Starting Skills

| Skill                                                                       | Type    | Effect                                        |
| --------------------------------------------------------------------------- | ------- | --------------------------------------------- |
| Raise Dead                                                                  | Active  | Can reanimate corpses as weak undead servants |
| [Undead Binding](../../skills/mechanic-unlock/undead-binding.md) (Mechanic) | Passive | Core ability to control and command undead    |
| [Mana Well](../../skills/tiered/mana-well.md) (Lesser)                      | Passive | Expanded mana for sustaining undead servants  |

#### Synergy Skills

Skills that have strong synergies with Necromancer. These skills can be learned by any character, but Necromancers gain significant bonuses (2x XP acquisition, enhanced effectiveness, reduced costs). Higher Necromancer levels provide stronger synergy bonuses.

**Core Magic Skills**:

- [Spell Focus](../../skills/tiered/spell-focus.md) - Lesser/Greater/Enhanced - Enhanced spell effectiveness
- [Mana Well](../../skills/tiered/mana-well.md) - Lesser/Greater/Enhanced - Expanded magical reserves
- [School Mastery](../../skills/mechanic-unlock/school-mastery.md) - Mechanic - Deep expertise in one school
- [Undead Binding](../../skills/mechanic-unlock/undead-binding.md) - Lesser/Greater/Enhanced - Control undead creatures
- [Ritual Casting](../../skills/mechanic-unlock/ritual-casting.md) - Lesser/Greater/Enhanced - Perform elaborate magical rituals
- [Mana Transfer](../../skills/mechanic-unlock/mana-transfer.md) - Lesser/Greater/Enhanced - Share magical energy

**Core Necromancy Skills**:

- Raise Undead - Lesser/Greater/Enhanced - Reanimate corpses
- Undead Control - Lesser/Greater/Enhanced - Command multiple undead
- Life Drain - Lesser/Greater/Enhanced - Steal life force
- Death Bolt - Lesser/Greater/Enhanced - Necrotic damage spells
- Undead Fortitude - Lesser/Greater/Enhanced - Enhanced undead durability
- Soul Binding - Lesser/Greater/Enhanced - Bind spirits to service
- Corpse Explosion - Lesser/Greater/Enhanced - Detonate undead
- Death Resistance - Lesser/Greater/Enhanced - Resist death magic
- Mass Reanimation - Lesser/Greater/Enhanced - Raise multiple corpses
- Lich Form - Lesser/Greater/Enhanced - Undead immortality
- Plague Touch - Lesser/Greater/Enhanced - Spread diseases
- Spirit Sight - Lesser/Greater/Enhanced - Perceive spirits
- Bone Armor - Lesser/Greater/Enhanced - Skeletal protection
- Soul Harvest - Lesser/Greater/Enhanced - Collect and use souls
- Death's Master - Epic - Ultimate death magic control

**Note**: All skills listed have strong synergies because they are core necromancy skills. Characters without Necromancer class can still learn these skills but progress at base rate (1x XP) without effectiveness bonuses.

#### Synergy Bonuses

Necromancer provides context-specific bonuses to death magic skills based on logical specialization:

**Core Necromancy Skills** (Direct specialization - Strong synergy):

- **Raise Undead**: Essential for reanimating and commanding the dead
  - Faster learning: 2x XP from reanimation actions (scales with class level)
  - Better effectiveness: +25% undead power at Necromancer 15, +35% at Necromancer 30+
  - Reduced cost: -25% mana at Necromancer 15, -35% at Necromancer 30+
- **Undead Control**: Directly tied to commanding undead armies
  - Faster learning: 2x XP from undead management
  - Better control: +30% control capacity at Necromancer 15
  - Lower cost: Additional -20% mana maintenance at Necromancer 15
- **Life Drain**: Core skill for stealing life force
  - Faster learning: 2x XP from life drain actions
  - Better power: +20% drain effectiveness at Necromancer 15
  - More transfer: +50% life force harvested
- **Soul Binding**: Fundamental to advanced necromancy
  - Faster learning: 2x XP from soul manipulation
  - Better binding: +25% binding strength at Necromancer 15
  - More resistance: +50% resistance to soul escape

**Synergy Strength Scales with Class Level**:

| Necromancer Level | XP Multiplier | Effectiveness Bonus | Cost Reduction | Example: Raise Undead |
| ----------------- | ------------- | ------------------- | -------------- | --------------------- |
| Level 5           | 1.5x (+50%)   | +15%                | -15% mana      | Good but moderate     |
| Level 10          | 1.75x (+75%)  | +20%                | -20% mana      | Strong improvements   |
| Level 15          | 2.0x (+100%)  | +25%                | -25% mana      | Excellent synergy     |
| Level 30+         | 2.5x (+150%)  | +35%                | -35% mana      | Masterful synergy     |

**Example Progression**:

A Necromancer 15 learning Raise Undead:

- Performs 50 reanimation actions (earning 2x XP = 1000 XP total, vs 500 XP for non-Necromancer)
- Raise Undead available at level-up after just 500 XP (vs 1500 XP for non-necromancy class)
- When using Raise Undead: Base undead power becomes +25% stronger (synergy bonus)
- Mana cost: 22.5 mana instead of 30 (25% reduction)

#### Tracked Actions

Actions that grant XP to the Necromancer class:

| Action Category      | Specific Actions                             | XP Value                |
| -------------------- | -------------------------------------------- | ----------------------- |
| Reanimation          | Successfully raise undead from corpses       | 15-100 per undead       |
| Undead Control       | Command undead effectively                   | 10-60 per session       |
| Life Drain           | Drain life force from living victims         | 15-80 per drain         |
| Death Magic          | Cast necrotic spells successfully            | 10-70 per spell         |
| Corpse Acquisition   | Obtain bodies for reanimation                | 5-40 per corpse         |
| Dark Rituals         | Perform necromantic rituals                  | 20-120 per ritual       |
| Undead Army          | Command large undead forces                  | 30-200 per army         |
| Lich Transformation  | Progress toward or achieve lich immortality  | 100-500 per stage       |
| Territorial Control  | Establish undead-controlled territory        | 50-300 per region       |
| Legendary Necromancy | Command vast armies or achieve death mastery | 150-600 per achievement |

#### Class Consolidation

See [Class Consolidation System](../../../systems/character/class-consolidation.md) for full mechanics.

| Consolidation Path                   | Requirements                                    | Result Class | Tier |
| ------------------------------------ | ----------------------------------------------- | ------------ | ---- |
| [Lich](../../consolidation/)         | Necromancer + achieve undead immortality        | Lich         | 1    |
| [Death Knight](../../consolidation/) | Necromancer + [Warrior](../../fighter/warrior/) | Death Knight | 1    |
| [Plague Lord](../../consolidation/)  | Necromancer + disease magic focus               | Plague Lord  | 1    |
| [Archlich](../../consolidation/)     | Necromancer + supreme death magic mastery       | Archlich     | 2    |

### Interactions

| System                                                       | Interaction                                                |
| ------------------------------------------------------------ | ---------------------------------------------------------- |
| [Magic System](../../../systems/character/magic-system.md)   | Uses forbidden death magic school                          |
| [Undead](../../../systems/social/death-and-undead.md)        | Creates and commands undead creatures                      |
| [Crime](../../../systems/social/index.md)                    | Forbidden magic; execution if discovered                   |
| [Reputation](../../../systems/social/factions-reputation.md) | Separate underground reputation; public persona impossible |
| [Corruption](../../../systems/world/index.md)                | Necromancy corrupts surrounding lands                      |
| [Combat](../../../systems/combat/combat-resolution.md)       | Commands undead armies in battle                           |

---

## Related Content

- **Requires:** Necromantic focus, corpses, dark ritual knowledge, mana pool
- **Prerequisites:** Often consolidates from [Mage](./mage/index.md) into forbidden practices
- **Equipment:** [Necromantic Staff](../../../item/), [Dark Grimoire](../../../item/), [Soul Gems](../../../item/)
- **Synergy Classes:** [Mage](./mage/index.md), [Summoner](./summoner/), [Warrior](../../fighter/warrior/)
- **Consolidates To:** [Lich](../../consolidation/), [Death Knight](../../consolidation/), [Archlich](../../consolidation/)
- **See Also:** [Death & Undead System](../../../systems/social/death-and-undead.md), [Forbidden Magic](../../../systems/magic/index.md), [Corruption](../../../systems/world/index.md)
