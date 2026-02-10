---
title: Battlemage
gdd_ref: systems/class-system-gdd.md#advanced-classes
parent: classes/channeler/mage/index.md
tree_tier: 3
---

# Battlemage

## Tags

> **Required Section:** Tags enable compile-time safe tag access in C# code and drive the skill/class/recipe systems.

### C# Reference

**Strongly Typed Tags:** Use these tags in C# code for compile-time safety.

| Tag Path            | C# Reference                        | Purpose                 |
| ------------------- | ----------------------------------- | ----------------------- |
| `Magic/Combat`      | `SkillTags.Magic.Combat.Value`      | Battlemage class access |
| `Combat/Spell`      | `SkillTags.Combat.Spell.Value`      | Magical combat          |
| `Magic/Destruction` | `SkillTags.Magic.Destruction.Value` | Destructive magic       |

**How to find class tags:**

1. Check `ClassDefinition.GrantedTags` in class definition files (e.g., `src/CozyFantasyRpg/Content/Classes/MagicClasses.cs`)
2. Verify against `src/CozyFantasyRpg/Shared/SkillTags.cs` for exact C# reference

### Tag Access

This class requires the following tags for access/synergy:

| Tag                 | Depth | Classes With Access                      |
| ------------------- | ----- | ---------------------------------------- |
| `Magic/Combat`      | 2     | Battlemage, Mage, Warrior                |
| `Combat/Spell`      | 2     | Battlemage, Spell-sword classes          |
| `Magic/Destruction` | 2     | Battlemage, Mage, Destruction specialist |

**Note:** Tag depth determines which classes can access this content. See [Tag System](../../../../systems/content/tag-system/index.md) for details.

---

## Lore

### Origin

Warriors fight with steel. Mages fight with spells. Battlemages master both, combining martial combat with destructive magic into devastating synthesis. They cast while wielding weapons, wear armor while slinging spells, and seamlessly transition between physical and magical attacks. This hybrid approach makes them exceptionally dangerous - enemies who defend against swords face fireballs, those who shield against magic meet steel.

The profession emerged from military necessity. Traditional Mages make poor frontline combatants - physically fragile, dangerously vulnerable if enemies close distance, requiring concentration that melee chaos disrupts. Pure Warriors lack magical options against supernatural threats. Battlemages solve both problems, developing techniques letting them fight effectively at any range while maintaining combat effectiveness few specialists match. Military forces prize them as elite shock troops and versatile tactical assets.

Becoming a Battlemage demands mastering two distinct disciplines. The martial component requires weapons training, armor proficiency, combat tactics, and physical conditioning. The magical component requires spell knowledge, mana management, casting discipline, and magical theory. Synthesizing these creates unique challenges - casting spells in heavy armor, maintaining concentration while dodging attacks, managing both stamina and mana simultaneously. Most who attempt this hybrid fail, lacking either martial or magical aptitude. Those who succeed become exceptional combatants.

### In the World

Battlemages occupy elite positions in military hierarchies. Armies employ them as officer-specialists, combining combat leadership with magical firepower. City guards recruit them for dangerous assignments requiring versatility. Noble houses hire them as personal champions, appreciating their adaptability. Adventuring parties value them as flexible combatants who fill multiple roles simultaneously. Their rarity and training cost means Battlemages command premium compensation.

The profession divides into doctrinal approaches. Aggressive Battlemages emphasize offense, wielding destructive magic alongside weapon attacks to overwhelm enemies through sheer damage output. Defensive Battlemages focus on durability, using protective spells while fighting, making them nearly unkillable. Tactical Battlemages emphasize versatility, carrying diverse spells for different situations while maintaining solid martial capability. Each approach suits different personalities and combat philosophies.

Combat doctrine for Battlemages differs from pure Warriors or Mages. They engage at medium range, using magic while closing or maintaining distance. They target priorities dynamically - using spells against magical threats, weapons against spell-resistant enemies, whichever proves most effective. They manage resources carefully, balancing mana expenditure with stamina, knowing both pools might be needed. They exploit hybrid advantages, surprising opponents who prepare for either warrior or mage but not both.

Apprentice Battlemages typically begin as either Warriors learning magic or Mages developing martial skills. They practice coordinating both skill sets - casting while holding weapons, fighting while maintaining spell concentration, switching combat modes fluidly. They build physical endurance to fight in armor while maintaining mana reserves for spellcasting. They learn which spells complement melee combat versus which interfere. Early training is exhausting - developing both physical and mental discipline simultaneously.

Master Battlemages achieve frightening combat effectiveness. They cast devastating spells while engaging multiple melee opponents. They enhance their martial abilities with personal buffs before battles. They use magic creatively during combat - summoning barriers, creating favorable terrain, eliminating distant threats while fighting nearby ones. The greatest Battlemages become legendary, their names synonymous with unstoppable military force.

---

## Mechanics

### Prerequisites

| Requirement            | Value                                                                            |
| ---------------------- | -------------------------------------------------------------------------------- |
| XP Threshold           | 50,000 XP from combat spellcasting tracked actions                               |
| Related Tier 2 Classes | [Warrior](../combat/warrior/index.md), [Mage](./mage/index.md) (provide bonuses) |
| Tag Depth Access       | 3+ levels (e.g., `Magic/Elemental`, `Combat/Melee`)                              |

### Requirements

| Requirement       | Value                                       |
| ----------------- | ------------------------------------------- |
| Unlock Trigger    | Cast spells effectively during melee combat |
| Primary Attribute | STR (Strength), INT (Intelligence)          |
| Starting Level    | 1                                           |
| Tools Required    | Weapon, armor, spell focus, combat training |

### Stats

#### Base Class Stats

| Level | HP Bonus | Mana/Stamina Bonus | Trait                 |
| ----- | -------- | ------------------ | --------------------- |
| 1     | +10      | +15/+12            | Apprentice Battlemage |
| 10    | +30      | +45/+35            | Journeyman Battlemage |
| 25    | +65      | +95/+75            | Master Battlemage     |
| 50    | +120     | +175/+140          | Legendary Battlemage  |

#### Combat Bonuses

| Class Level | Melee Damage | Spell Damage | Defense | Armor Casting |
| ----------- | ------------ | ------------ | ------- | ------------- |
| 1-9         | +8%          | +10%         | +8%     | -30%          |
| 10-24       | +20%         | +25%         | +20%    | -10%          |
| 25-49       | +45%         | +50%         | +45%    | +0%           |
| 50+         | +80%         | +85%         | +80%    | +20%          |

#### Starting Skills

| Skill                                                                      | Type    | Effect                                     |
| -------------------------------------------------------------------------- | ------- | ------------------------------------------ |
| Combat Casting                                                             | Passive | Can cast spells while armed and armored    |
| [Spell Focus](../../skills/tiered/spell-focus/index.md) (Lesser)           | Passive | Enhanced combat spellcasting effectiveness |
| [Combat Awareness](../../skills/common/combat-awareness/index.md) (Lesser) | Passive | Maintain awareness while casting in melee  |

#### Synergy Skills

Skills that have strong synergies with Battlemage. These skills can be learned by any character, but Battlemages gain significant bonuses (2x XP acquisition, enhanced effectiveness, reduced costs). Higher Battlemage levels provide stronger synergy bonuses.

**Core Magic Skills**:

- [Spell Focus](../../skills/tiered/spell-focus/index.md) - Lesser/Greater/Enhanced - Enhanced spell effectiveness
- [Mana Well](../../skills/tiered/mana-well/index.md) - Lesser/Greater/Enhanced - Expanded magical reserves
- [School Mastery](../../skills/mechanic-unlock/school-mastery/index.md) - Mechanic - Deep expertise in one school
- [Counter Magic](../../skills/tiered/counter-magic/index.md) - Lesser/Greater/Enhanced - Dispel and resist hostile magic
- [Spell Weaving](../../skills/mechanic-unlock/spell-weaving/index.md) - Lesser/Greater/Enhanced - Combine multiple spells
- [Ritual Casting](../../skills/mechanic-unlock/ritual-casting/index.md) - Lesser/Greater/Enhanced - Perform elaborate magical rituals
- [Mana Transfer](../../skills/mechanic-unlock/mana-transfer/index.md) - Lesser/Greater/Enhanced - Share magical energy

**Core Combat Skills**:

- Spell Blade - Lesser/Greater/Enhanced - Weapon attacks with magical damage
- Armored Caster - Lesser/Greater/Enhanced - Reduce armor spell interference
- Battle Magic - Lesser/Greater/Enhanced - Enhanced combat spellcasting
- Weapon Mastery - Lesser/Greater/Enhanced - Improved weapon damage
- Quick Cast - Lesser/Greater/Enhanced - Faster combat casting
- Shield Ward - Lesser/Greater/Enhanced - Magical shield defense
- Spell Strike - Lesser/Greater/Enhanced - Attack-triggered spell effects
- Resource Balance - Lesser/Greater/Enhanced - Stamina and mana efficiency
- Elemental Weapon - Lesser/Greater/Enhanced - Weapon elemental enchantment
- Battle Aura - Lesser/Greater/Enhanced - Ally combat effectiveness boost
- Spell Parry - Lesser/Greater/Enhanced - Block spells with weapons
- Combat Teleport - Lesser/Greater/Enhanced - Instant combat movement
- Dual Focus - Lesser/Greater/Enhanced - Cast and attack simultaneously
- War Incarnate - Epic - Perfect magic and martial fusion

**Note**: All skills listed have strong synergies because they are core hybrid combat skills. Characters without Battlemage class can still learn these skills but progress at base rate (1x XP) without effectiveness bonuses.

#### Synergy Bonuses

Battlemage provides context-specific bonuses to hybrid combat skills based on logical specialization:

**Core Hybrid Skills** (Direct specialization - Strong synergy):

- **Spell Blade**: Essential for weapon-and-magic integration
  - Faster learning: 2x XP from hybrid combat actions (scales with class level)
  - Better effectiveness: +25% magical weapon damage at Battlemage 15, +35% at Battlemage 30+
  - Reduced cost: -25% mana at Battlemage 15, -35% at Battlemage 30+
- **Armored Caster**: Directly tied to casting in combat gear
  - Faster learning: 2x XP from armored spellcasting
  - Better casting: +30% spell effectiveness in armor at Battlemage 15
  - Lower penalty: Additional -20% armor interference at Battlemage 15
- **Battle Magic**: Core skill for combat spellcasting
  - Faster learning: 2x XP from combat spellcasting
  - Better power: +20% combat spell damage at Battlemage 15
  - Lower cost: Additional -20% mana cost at Battlemage 15
- **Resource Balance**: Fundamental to managing stamina and mana
  - Faster learning: 2x XP from dual resource management
  - Better efficiency: +25% resource efficiency at Battlemage 15
  - More capacity: +50% effective pool sizes

**Synergy Strength Scales with Class Level**:

| Battlemage Level | XP Multiplier | Effectiveness Bonus | Cost Reduction | Example: Spell Blade |
| ---------------- | ------------- | ------------------- | -------------- | -------------------- |
| Level 5          | 1.5x (+50%)   | +15%                | -15% mana      | Good but moderate    |
| Level 10         | 1.75x (+75%)  | +20%                | -20% mana      | Strong improvements  |
| Level 15         | 2.0x (+100%)  | +25%                | -25% mana      | Excellent synergy    |
| Level 30+        | 2.5x (+150%)  | +35%                | -35% mana      | Masterful synergy    |

**Example Progression**:

A Battlemage 15 learning Spell Blade:

- Performs 50 hybrid combat actions (earning 2x XP = 1000 XP total, vs 500 XP for non-Battlemage)
- Spell Blade available at level-up after just 500 XP (vs 1500 XP for non-hybrid class)
- When using Spell Blade: Base +30% magical damage becomes +55% magical damage (base +25% synergy)
- Mana cost: 7.5 mana instead of 10 (25% reduction)

#### Tracked Actions

Actions that grant XP to the Battlemage class:

| Action Category     | Specific Actions                                             | XP Value                |
| ------------------- | ------------------------------------------------------------ | ----------------------- |
| Hybrid Combat       | Fight using both weapons and spells                          | 15-100 per battle       |
| Combat Spellcasting | Cast spells effectively during melee                         | 10-70 per spell         |
| Martial Excellence  | Defeat enemies through weapon skills                         | 10-60 per kill          |
| Magical Destruction | Eliminate threats with destructive magic                     | 10-70 per kill          |
| Tactical Switching  | Seamlessly alternate between martial and magical combat      | 15-80 per battle        |
| Elite Combat        | Defeat powerful enemies using hybrid techniques              | 30-200 per victory      |
| Armored Casting     | Successfully cast complex spells in heavy armor              | 15-80 per session       |
| Resource Management | Effectively balance stamina and mana in extended combat      | 10-60 per battle        |
| Leadership          | Lead troops while providing magical support                  | 20-120 per command      |
| Legendary Battle    | Achieve battlefield dominance through perfect hybrid mastery | 100-500 per achievement |

#### Class Consolidation

See [Class Consolidation System](../../../systems/character/class-consolidation/index.md) for full mechanics.

| Consolidation Path                           | Requirements                                     | Result Class    | Tier |
| -------------------------------------------- | ------------------------------------------------ | --------------- | ---- |
| [Spellblade](../consolidation/index.md)      | Battlemage + weapon specialization               | Spellblade      | 1    |
| [War Mage](../consolidation/index.md)        | Battlemage + military leadership                 | War Mage        | 1    |
| [Mystic Warrior](../consolidation/index.md)  | Battlemage + [Knight](../combat/knight/index.md) | Mystic Warrior  | 1    |
| [Arcane Champion](../consolidation/index.md) | Battlemage + legendary hybrid mastery            | Arcane Champion | 2    |

### Interactions

| System                                                           | Interaction                                   |
| ---------------------------------------------------------------- | --------------------------------------------- |
| [Combat](../../../systems/combat/combat-resolution/index.md)     | Elite hybrid combatant; melee and magic       |
| [Magic System](../../../systems/character/magic-system/index.md) | Specializes in combat-applicable spells       |
| [Party](../../../systems/combat/party-mechanics/index.md)        | Versatile frontline fighter and damage dealer |
| [Equipment](../../../systems/character/index.md)                 | Uses both weapons/armor and magical items     |
| [Tactics](../../../systems/combat/index.md)                      | Dynamic combat style switching                |
| [Military](../../../systems/social/index.md)                     | Elite military specialist                     |

---

## Related Content

- **Requires:** Weapon, armor, spell focus, combat training, mana pool
- **Prerequisites:** Consolidates from [Warrior](../combat/warrior/index.md) + [Mage](./mage/index.md)
- **Equipment:** [Weapons](../../items/index.md), [Battle Armor](../../items/index.md), [Combat Staff](../../items/index.md), [Spell Focus](../../items/index.md)
- **Synergy Classes:** [Warrior](../combat/warrior/index.md), [Mage](./mage/index.md), [Knight](../combat/knight/index.md)
- **Consolidates To:** [Spellblade](../consolidation/index.md), [War Mage](../consolidation/index.md), [Arcane Champion](../consolidation/index.md)
- **See Also:** [Combat System](../../../systems/combat/combat-resolution/index.md), [Magic System](../../../systems/character/magic-system/index.md), [Hybrid Combat](../../../systems/combat/index.md)
