---
title: 'Mage'
type: 'class'
category: 'magic'
tier: 2
prerequisite_xp: 5000
prerequisite_actions: spellcasting and magical study
summary: 'Generalist spellcaster with broad access to all magical disciplines'
specializations:
  - Pyromancer
  - Cryomancer
  - Electromancer
  - Geomancer
  - Aeromancer
  - Hydromancer
  - Necromancer
  - Summoner
  - Illusionist
consolidations:
  - Battlemage
  - Enchanter
  - Healer
---

# Mage

## Tags

> **Required Section:** Tags enable compile-time safe tag access in C# code and drive the skill/class/recipe systems.

### C# Reference

**Strongly Typed Tags:** Use these tags in C# code for compile-time safety.

| Tag Path            | C# Reference                        | Purpose            |
| ------------------- | ----------------------------------- | ------------------ |
| `Magic`             | `SkillTags.Magic.Value`             | Magic class access |
| `Magic/Arcane`      | `SkillTags.Magic.Arcane.Value`      | Arcane magic       |
| `Magic/Elemental`   | `SkillTags.Magic.Elemental.Value`   | Elemental spells   |
| `Magic/Conjuration` | `SkillTags.Magic.Conjuration.Value` | Conjuration magic  |
| `Magic/Illusion`    | `SkillTags.Magic.Illusion.Value`    | Illusion spells    |

**How to find class tags:**

1. Check `ClassDefinition.GrantedTags` in class definition files (e.g., `src/CozyFantasyRpg/Content/Classes/MagicClasses.cs`)
2. Verify against `src/CozyFantasyRpg/Shared/SkillTags.cs` for exact C# reference

### Tag Access

This class requires the following tags for access/synergy:

| Tag                 | Depth | Classes With Access                     |
| ------------------- | ----- | --------------------------------------- |
| `Magic`             | 1     | Mage, Channeler                         |
| `Magic/Arcane`      | 2     | Mage (full access)                      |
| `Magic/Elemental`   | 2     | Mage (basic, specialists get depth 3+)  |
| `Magic/Conjuration` | 2     | Mage (basic, specialists get depth 3+)  |
| `Magic/Illusion`    | 2     | Mage (basic, Illusionist gets depth 3+) |

**Note:** Tag depth determines which classes can access this content. See [Tag System](../../../systems/content/tag-system.md) for details.

---

## Lore

### Origin

Magic is power made manifest through will and knowledge. While anyone might possess latent magical potential, transforming that potential into controlled spellcasting requires years of study, practice, and discipline. Mages are those who dedicate themselves to understanding magic's fundamental principles, learning to shape raw magical energy into useful effects. They're scholars first, spellcasters second - their power comes from comprehension, not innate talent.

The Mage tradition emerged when early spellcasters realized magic followed patterns and rules. Wild magic - uncontrolled bursts of power - injured or killed as often as it helped. Systematic study revealed that specific gestures, words, and mental focuses reliably produced desired effects. These discoveries codified into magical disciplines - organized bodies of knowledge teaching practitioners how to safely and effectively cast specific spell types.

Modern Mages approach magic academically. They study arcane theory, learning why spells work rather than just memorizing incantations. They practice precise control over their mana, the internal magical energy fueling spells. They experiment carefully, testing variations to understand limits and possibilities. They maintain spell books documenting their knowledge. This scholarly approach distinguishes Mages from instinctive casters - Mages understand their craft deeply enough to adapt and innovate rather than merely repeating learned formulas.

### In the World

Mages occupy unique social positions. Their power makes them valuable - healing injuries, providing protection, solving problems mundane methods can't address. Their knowledge makes them respected - consulting on magical matters, identifying enchanted items, advising on supernatural threats. Yet their abilities also inspire wariness - people who can harm with gestures demand careful treatment.

Most settlements welcome Mages, though restrictions apply. Cities often require registered Mages to demonstrate control and peaceful intent. Some spells are prohibited in populated areas - destructive magic, mind control, harmful conjurations. Mages who violate these restrictions face harsh penalties. Compliance is generally easy - responsible Mages understand why firing lightning bolts in crowded markets invites legal trouble.

The profession divides into specialists and generalists. Specialist Mages master one magical discipline deeply, becoming experts in specific magical domains. Generalist Mages study multiple disciplines broadly, maintaining flexibility at the cost of maximum power. Both approaches have merits - specialists achieve effects generalists can't match, while generalists adapt to varied situations better. Career paths also vary: some Mages research magic in academic settings, others provide magical services commercially, still others adventure to study exotic magical phenomena.

Apprentice Mages begin with basic cantrips - simple spells teaching fundamental magical manipulation. They learn to sense and channel their mana. They study magical theory, understanding spell components and why specific combinations produce particular effects. They practice extensively, building the mental discipline spellcasting requires. Early mistakes teach importance of precision - mispronounced words or imprecise gestures cause spell failures, wasted mana, or dangerous misfires.

Master Mages accumulate vast knowledge across multiple magical disciplines. They cast complex spells reliably, manage large mana reserves efficiently, and understand magic's nuances deeply enough to modify standard spells situationally. The greatest become Archmages - legendary practitioners whose mastery spans all disciplines, allowing them to tackle magical challenges others consider impossible.

---

## Mechanics

### Prerequisites

| Requirement        | Value                                                 |
| ------------------ | ----------------------------------------------------- |
| XP Threshold       | 5,000 XP from spellcasting tracked actions            |
| Related Foundation | [Channeler](../index.md) (optional, provides bonuses) |
| Tag Depth Access   | 2 levels (e.g., `Magic/Arcane`, `Magic/Elemental`)    |

### Requirements

| Requirement       | Value                                     |
| ----------------- | ----------------------------------------- |
| Unlock Trigger    | Cast spells, study magic systematically   |
| Primary Attribute | INT (Intelligence), WIS (Wisdom)          |
| Starting Level    | 1                                         |
| Tools Required    | Spell book, arcane focus, study materials |

### Tag Access

| Tag                   | Depth | Access                            |
| --------------------- | ----- | --------------------------------- |
| `Magic`               | 1     | Full synergy                      |
| `Magic/Arcane`        | 2     | Full synergy                      |
| `Magic/Elemental`     | 2     | Full synergy (basic elemental)    |
| `Magic/Conjuration`   | 2     | Full synergy (basic summoning)    |
| `Magic/Illusion`      | 2     | Full synergy (basic illusion)     |
| `Magic/Elemental/*`   | 3     | No synergy (requires specialist)  |
| `Magic/Conjuration/*` | 3     | No synergy (requires specialist)  |
| `Magic/Illusion/Mind` | 3     | No synergy (requires Illusionist) |

### Stats

#### Base Class Stats

| Level | HP Bonus | Mana Bonus | Trait           |
| ----- | -------- | ---------- | --------------- |
| 1     | +4       | +20        | Apprentice Mage |
| 10    | +12      | +60        | Journeyman Mage |
| 25    | +25      | +130       | Master Mage     |
| 50    | +45      | +240       | Archmage        |

#### Magic Bonuses

| Class Level | Spell Power | Mana Efficiency | Disciplines     | Cast Speed |
| ----------- | ----------- | --------------- | --------------- | ---------- |
| 1-9         | +5%         | +10%            | 2 disciplines   | +0%        |
| 10-24       | +20%        | +25%            | 3 disciplines   | +10%       |
| 25-49       | +45%        | +50%            | 5 disciplines   | +25%       |
| 50+         | +80%        | +85%            | All disciplines | +50%       |

#### Starting Skills

| Skill                                                      | Type    | Effect                                                |
| ---------------------------------------------------------- | ------- | ----------------------------------------------------- |
| Basic Spellcasting                                         | Passive | Can cast cantrips and basic spells from known schools |
| [Spell Focus](../../skills/tiered/spell-focus.md) (Lesser) | Passive | Enhanced spellcasting effectiveness and control       |
| [Mana Well](../../skills/tiered/mana-well.md) (Lesser)     | Passive | Expanded mana pool for sustained casting              |

#### Synergy Skills

Skills that have strong synergies with Mage. These skills can be learned by any character, but Mages gain significant bonuses (2x XP acquisition, enhanced effectiveness, reduced costs). Higher Mage levels provide stronger synergy bonuses.

**Core Magic Skills**:

- [Spell Focus](../../skills/tiered/spell-focus.md) - Lesser/Greater/Enhanced - Enhanced spell effectiveness
- [Mana Well](../../skills/tiered/mana-well.md) - Lesser/Greater/Enhanced - Expanded magical reserves
- [School Mastery](../../skills/mechanic-unlock/school-mastery.md) - Mechanic - Deep expertise in one school
- [Counter Magic](../../skills/tiered/counter-magic.md) - Lesser/Greater/Enhanced - Dispel and resist hostile magic
- [Spell Weaving](../../skills/mechanic-unlock/spell-weaving.md) - Lesser/Greater/Enhanced - Combine multiple spells
- [Ritual Casting](../../skills/mechanic-unlock/ritual-casting.md) - Lesser/Greater/Enhanced - Perform elaborate magical rituals
- [Mana Transfer](../../skills/mechanic-unlock/mana-transfer.md) - Lesser/Greater/Enhanced - Share magical energy

**Core Spellcasting Skills**:

- Mana Efficiency - Lesser/Greater/Enhanced - Reduced spell costs
- Spell Power - Lesser/Greater/Enhanced - Increased effectiveness
- Fast Casting - Lesser/Greater/Enhanced - Reduced cast time
- Mana Regeneration - Lesser/Greater/Enhanced - Faster mana recovery
- Spell Modification - Lesser/Greater/Enhanced - Adjust spell parameters
- Multi-School - Lesser/Greater/Enhanced - Access multiple schools
- Arcane Knowledge - Lesser/Greater/Enhanced - Magical theory mastery
- Concentration - Lesser/Greater/Enhanced - Extended spell duration
- Spell Precision - Lesser/Greater/Enhanced - Improved accuracy
- Combat Casting - Lesser/Greater/Enhanced - Cast under threat
- Metamagic - Lesser/Greater/Enhanced - Transform spell properties
- Archmage Potential - Epic - Full access to all schools

**Note**: All skills listed have strong synergies because they are core spellcasting skills. Characters without Mage class can still learn these skills but progress at base rate (1x XP) without effectiveness bonuses.

#### Synergy Bonuses

Mage provides context-specific bonuses to spellcasting-related skills based on logical specialization:

**Core Spellcasting Skills** (Direct specialization - Strong synergy):

- **Spell Power**: Essential for effective magical output
  - Faster learning: 2x XP from spellcasting actions (scales with class level)
  - Better effectiveness: +25% spell power at Mage 15, +35% at Mage 30+
  - Reduced cost: -25% mana at Mage 15, -35% at Mage 30+
- **Mana Efficiency**: Directly tied to sustainable spellcasting
  - Faster learning: 2x XP from mana management
  - Better efficiency: +30% mana conservation at Mage 15
  - Lower cost: Additional -20% spell mana cost at Mage 15
- **School Mastery**: Core skill for specialized magical expertise
  - Faster learning: 2x XP from school-focused casting
  - Better effectiveness: +20% school specialization at Mage 15
  - More power: +50% effectiveness in chosen school
- **Arcane Knowledge**: Fundamental to understanding magic theory
  - Faster learning: 2x XP from magical study
  - Better comprehension: +25% theory understanding at Mage 15
  - More discovery: +50% chance to learn new spells

**Synergy Strength Scales with Class Level**:

| Mage Level | XP Multiplier | Effectiveness Bonus | Cost Reduction | Example: Spell Power |
| ---------- | ------------- | ------------------- | -------------- | -------------------- |
| Level 5    | 1.5x (+50%)   | +15%                | -15% mana      | Good but moderate    |
| Level 10   | 1.75x (+75%)  | +20%                | -20% mana      | Strong improvements  |
| Level 15   | 2.0x (+100%)  | +25%                | -25% mana      | Excellent synergy    |
| Level 30+  | 2.5x (+150%)  | +35%                | -35% mana      | Masterful synergy    |

**Example Progression**:

A Mage 15 learning Spell Power:

- Performs 50 spellcasting actions (earning 2x XP = 1000 XP total, vs 500 XP for non-Mage)
- Spell Power available at level-up after just 500 XP (vs 1500 XP for non-mage class)
- When using Spell Power: Base +30% spell effectiveness becomes +55% spell effectiveness (base +25% synergy)
- Mana cost: 18 mana instead of 24 (25% reduction)

#### Tracked Actions

Actions that grant XP to the Mage class:

| Action Category         | Specific Actions                                  | XP Value                |
| ----------------------- | ------------------------------------------------- | ----------------------- |
| Spellcasting            | Cast spells successfully                          | 5-40 per spell          |
| Magical Study           | Study arcane theory and spell mechanics           | 10-50 per session       |
| Spell Learning          | Learn new spells from scrolls, books, or teachers | 20-100 per spell        |
| Magical Research        | Research new magical effects or spell variations  | 30-150 per breakthrough |
| Combat Casting          | Cast spells effectively in combat                 | 10-60 per battle        |
| Ritual Casting          | Perform complex ritual magic                      | 40-200 per ritual       |
| Magical Problem Solving | Use magic to solve non-combat challenges          | 15-80 per solution      |
| Teaching Magic          | Train others in magical arts                      | 15-60 per student       |
| Discipline Mastery      | Achieve deep mastery in magical disciplines       | 50-250 per mastery      |
| Arcane Innovation       | Develop entirely new spells or magical techniques | 100-500 per innovation  |

#### Class Consolidation

See [Class Consolidation System](../../../systems/character/class-consolidation.md) for full mechanics.

| Consolidation Path               | Requirements                                                 | Result Class | Tier |
| -------------------------------- | ------------------------------------------------------------ | ------------ | ---- |
| [Battlemage](./battlemage/)      | Mage + [Warrior](../../fighter/warrior/) or combat focus     | Battlemage   | 1    |
| [Enchanter](../enchanter/)       | Mage + [Blacksmith](../../crafter/blacksmith/) or item focus | Enchanter    | 1    |
| [Archmage](../../consolidation/) | Mage + master all spell schools                              | Archmage     | 2    |
| [Sage](../../consolidation/)     | Mage + extensive magical knowledge                           | Sage         | 1    |

### Interactions

| System                                                        | Interaction                                   |
| ------------------------------------------------------------- | --------------------------------------------- |
| [Magic System](../../../systems/character/magic-system.md)    | Core spellcasting mechanics                   |
| [Spell Schools](../../../systems/magic/index.md)              | Learns and casts spells from multiple schools |
| [Mana](../../../systems/magic/index.md)                       | Manages mana pool for spellcasting            |
| [Combat](../../../systems/combat/combat-resolution.md)        | Versatile magical damage and utility          |
| [Crafting](../../../systems/crafting/crafting-progression.md) | Can craft magical items with proper skills    |
| [Economy](../../../systems/economy/index.md)                  | Provides magical services for income          |

---

## Progression

### Progression Paths

Mage can specialize into Tier 3 classes upon reaching 50,000 XP:

#### Specializations

| Tier 3 Class  | Focus                   | Tag Path                      | Link                              |
| ------------- | ----------------------- | ----------------------------- | --------------------------------- |
| Pyromancer    | Fire magic mastery      | `Magic/Elemental/Fire`        | [Pyromancer](./pyromancer/)       |
| Cryomancer    | Ice magic mastery       | `Magic/Elemental/Ice`         | [Cryomancer](./cryomancer/)       |
| Electromancer | Lightning magic mastery | `Magic/Elemental/Lightning`   | [Electromancer](./electromancer/) |
| Geomancer     | Earth magic mastery     | `Magic/Elemental/Earth`       | [Geomancer](./geomancer/)         |
| Aeromancer    | Air magic mastery       | `Magic/Elemental/Air`         | [Aeromancer](./aeromancer/)       |
| Hydromancer   | Water magic mastery     | `Magic/Elemental/Water`       | [Hydromancer](./hydromancer/)     |
| Necromancer   | Undead magic mastery    | `Magic/Conjuration/Undead`    | [Necromancer](./necromancer/)     |
| Summoner      | Summoning magic mastery | `Magic/Conjuration/Summoning` | [Summoner](./summoner/)           |
| Illusionist   | Illusion magic mastery  | `Magic/Illusion/Mind`         | [Illusionist](./illusionist/)     |

#### Consolidations

| Tier 3 Class | Requirements            | Focus                   | Link                        |
| ------------ | ----------------------- | ----------------------- | --------------------------- |
| Battlemage   | Mage + Warrior          | Combat spellcasting     | [Battlemage](./battlemage/) |
| Enchanter    | Mage + Crafter (Tier 2) | Item enchantment        | [Enchanter](./enchanter/)   |
| Healer       | Mage + Herbalist        | Restoration + herbalism | [Healer](./healer/)         |

---

## Related Content

- **Requires:** Spell book, arcane focus (staff/wand), study materials, mana pool
- **Equipment:** [Spell Book](../../../item/), [Staff](../../../item/tool/), [Wand](../../../item/tool/), [Robes](../../../item/)
- **Synergy Classes:** [Enchanter](../enchanter/), [Healer](../healer/), [Battlemage](./battlemage/)
- **Consolidates To:** [Battlemage](./battlemage/), [Enchanter](../enchanter/), [Archmage](../../consolidation/)
- **See Also:** [Magic System](../../../systems/character/magic-system.md), [Spell Schools](../../../systems/magic/), [Mana Management](../../../systems/magic/)
