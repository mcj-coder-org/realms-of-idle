---
title: Warrior
gdd_ref: systems/class-system-gdd.md#specialization-classes
parent: classes/fighter/index.md
tree_tier: 2
---

# Warrior

## Lore

### Origin

The Warrior class is as old as conflict itself. When the first human picked up a rock to defend their family, the archetype was born. Every culture develops Warriors - those who train specifically for combat, who study violence as craft rather than last resort. Unlike adventurers who fight when necessary, Warriors choose combat as their path, dedicating themselves to martial excellence.

Warriors come from all backgrounds. Some answer family traditions - warrior lineages passing techniques through generations. Others discover talent through necessity - refugees who learned to fight for survival. Military service creates many Warriors, training turning farmers into soldiers. Gladiatorial pits, mercenary companies, and dueling academies all produce Warriors, each with distinct styles reflecting their origins.

The Warriors' Guild does not glorify violence but recognizes combat as inevitable reality. Wars happen. Bandits raid. Monsters threaten. Someone must stand between danger and innocent people. Warriors train so they can protect, not because they enjoy killing. Those who fight purely for pleasure are not true Warriors - they're thugs and psychopaths. The Guild maintains standards: Warriors fight with honor, protect the weak, and only use violence when necessary.

### In the World

Every settlement employs Warriors in some capacity. Villages maintain militias - part-time Warriors who train together and respond to threats. Cities field professional guards and soldiers. Nobles hire Warriors as bodyguards and enforcers. Merchant caravans recruit Warriors for protection. Adventuring parties need Warriors for their martial expertise. The demand for trained combatants never disappears.

Warriors occupy complex social positions. Respected for their skills and necessary protection, yet feared for their capacity for violence. Parents warn children to behave or the Warrior might hear. Taverns serve Warriors carefully, knowing they could turn rowdy quickly. Yet when danger threatens, those same fearful people run toward Warriors begging for protection.

Training defines the Warrior. Unlike natural fighters who rely on instinct, Warriors practice techniques until muscle memory takes over. Hours drilling sword forms, shield work, footwork patterns. Sparring partners to develop timing and distance judgment. Strength training to build power. Endurance work to maintain effectiveness through long battles. True Warriors never stop training - skills decay without constant practice.

Apprentice Warriors start with basics: how to hold weapons without hurting themselves, how to move in armor, how to maintain equipment. They learn that real combat differs wildly from stories. Battles are exhausting, terrifying, confused melees where adrenaline makes fine motor control nearly impossible. Simple, practiced movements work better than complex techniques. Staying alive matters more than looking impressive.

Experienced Warriors develop personal fighting styles reflecting body type, preferences, and experience. Tall Warriors favor reach weapons. Strong Warriors use heavy weapons. Quick Warriors employ speed and precision. Each Warrior finds what works for their strengths and drills it until automatic. The best Warriors adapt their style to opponents and situations, knowing no single approach suits every fight.

---

## Mechanics

### Prerequisites

| Requirement        | Value                                                       |
| ------------------ | ----------------------------------------------------------- |
| XP Threshold       | 5,000 XP from combat tracked actions                        |
| Related Foundation | [Fighter](../fighter/index.md) (optional, provides bonuses) |
| Tag Depth Access   | 2 levels (e.g., `Combat/Melee`)                             |

### Requirements

| Requirement       | Value                           |
| ----------------- | ------------------------------- |
| Unlock Trigger    | Defeat enemies in melee combat  |
| Primary Attribute | STR (Strength), END (Endurance) |
| Starting Level    | 1                               |
| Tools Required    | Melee weapon, basic armor       |

### Stats

#### Base Class Stats

| Level | HP Bonus | Stamina Bonus | Trait              |
| ----- | -------- | ------------- | ------------------ |
| 1     | +12      | +15           | Apprentice Warrior |
| 10    | +35      | +45           | Journeyman Warrior |
| 25    | +75      | +95           | Master Warrior     |
| 50    | +135     | +170          | Legendary Warrior  |

#### Combat Bonuses

| Class Level | Damage Bonus | Defense Bonus | Crit Chance | Weapon Proficiency |
| ----------- | ------------ | ------------- | ----------- | ------------------ |
| 1-9         | +0%          | +0%           | +0%         | +0%                |
| 10-24       | +15%         | +10%          | +5%         | +10%               |
| 25-49       | +35%         | +25%          | +12%        | +25%               |
| 50+         | +60%         | +45%          | +20%        | +50%               |

#### Starting Skills (Auto-Awarded on Class Acceptance)

Skills automatically awarded when accepting this class:

| Skill          | Tier   | Link                                                     | Reasoning                                     |
| -------------- | ------ | -------------------------------------------------------- | --------------------------------------------- |
| Weapon Mastery | Lesser | [See Skill](../../skills/tiered/weapon-mastery/index.md) | Basic weapon proficiency essential for combat |
| Power Strike   | Lesser | [See Skill](../../skills/common/power-strike/index.md)   | Core offensive ability                        |

#### Synergy Skills

Skills with strong synergies for Warrior:

- [Weapon Mastery](../../skills/tiered/weapon-mastery/index.md) - Lesser/Greater/Enhanced - Increased damage with chosen weapon type
- [Shield Block](../../skills/index.md) - Lesser/Greater/Enhanced - Increased block chance and damage reduction
- [Power Strike](../../skills/common/power-strike/index.md) - Lesser/Greater/Enhanced - Cooldown attack with massive damage
- [Combat Reflexes](../../skills/index.md) - Lesser/Greater/Enhanced - Increased dodge and parry chance
- [Armor Proficiency](../../skills/index.md) - Lesser/Greater/Enhanced - Reduced armor penalties
- [Battle Cry](../../skills/index.md) - Lesser/Greater/Enhanced - Boost ally damage temporarily
- [Cleave](../../skills/index.md) - Lesser/Greater/Enhanced - Hit multiple adjacent enemies
- [Second Wind](../../skills/common/second-wind/index.md) - Lesser/Greater/Enhanced - Recover stamina once per battle
- [Defensive Stance](../../skills/common/defensive-stance/index.md) - Lesser/Greater/Enhanced - Toggle increased defense for reduced damage
- [Weapon Versatility](../../skills/index.md) - Lesser/Greater/Enhanced - Reduced penalties with non-specialty weapons
- [Berserker Rage](../../skills/cooldown/battle-rage/index.md) - Lesser/Greater/Enhanced - Temporary damage boost with defense penalty
- [Disarm](../../skills/index.md) - Lesser/Greater/Enhanced - Chance to disarm enemy on hit
- [Counter Attack](../../skills/index.md) - Lesser/Greater/Enhanced - Attack after successful block or parry
- [Intimidating Presence](../../skills/common/intimidating-presence/index.md) - Lesser/Greater/Enhanced - Cause weaker enemies to flee
- [Master of Arms](../../skills/index.md) - No tiers - Proficient with all weapon types

**Note**: All skills listed have strong synergies because they are core warrior combat skills. Characters without Warrior class can still learn these skills but progress at base rate (1x XP) without effectiveness bonuses.

#### Synergy Bonuses

Warrior provides context-specific bonuses based on logical specialization:

**Core Combat Skills** (Direct specialization - Strong synergy):

- **Weapon Mastery**: Essential for specialized weapon proficiency
  - Faster learning: 2x XP from using chosen weapon (scales with class level)
  - Better effectiveness: +25% damage bonus at Warrior 15, +35% at Warrior 30+
  - Reduced cost: -25% stamina for weapon attacks at Warrior 15, -35% at Warrior 30+
- **Shield Block**: Directly tied to defensive combat
  - Faster learning: 2x XP from blocking actions
  - Better block rate: +20% block chance at Warrior 15
  - Lower stamina cost: -30% stamina per block at Warrior 15
- **Power Strike**: Core skill for dealing massive damage
  - Faster learning: 2x XP from high-damage attacks
  - Better effectiveness: +35% power strike damage at Warrior 15
  - Faster cooldown: -25% cooldown time at Warrior 15
- **Battle Cry**: Fundamental to battlefield presence
  - Faster learning: 2x XP from group combat
  - Better effectiveness: +15% ally damage bonus at Warrior 15
  - Longer duration: +30 seconds at Warrior 15

**Synergy Strength Scales with Class Level**:

| Warrior Level | XP Multiplier | Effectiveness Bonus | Cost Reduction | Example: Weapon Mastery |
| ------------- | ------------- | ------------------- | -------------- | ----------------------- |
| Level 5       | 1.5x (+50%)   | +15%                | -15% stamina   | Good proficiency        |
| Level 10      | 1.75x (+75%)  | +20%                | -20% stamina   | Strong mastery          |
| Level 15      | 2.0x (+100%)  | +25%                | -25% stamina   | Excellent skill         |
| Level 30+     | 2.5x (+150%)  | +35%                | -35% stamina   | Legendary weapon master |

#### Tracked Actions

Actions that grant XP to the Warrior class:

| Action Category    | Specific Actions                            | XP Value         |
| ------------------ | ------------------------------------------- | ---------------- |
| Melee Combat       | Defeat enemies in melee                     | 10-100 per kill  |
| Training           | Practice weapon forms, spar with others     | 5-20 per session |
| Defense            | Successfully block, parry, or dodge attacks | 2-10 per action  |
| Weapon Maintenance | Sharpen blades, repair armor                | 3-15 per session |
| Group Combat       | Fight effectively in formation or party     | 10-50 per battle |
| Duel               | Win one-on-one combat                       | 15-60 per duel   |
| Tactical Fighting  | Use terrain, positioning for advantage      | 5-25 per use     |
| Protect Others     | Defend allies, take hits for others         | 10-40 per battle |
| Difficult Enemy    | Defeat challenging or dangerous foes        | 30-150 per kill  |
| Critical Victory   | Win battle through skill rather than luck   | Bonus +50%       |

#### Class Consolidation

See [Class Consolidation System](../../../systems/character/class-consolidation/index.md) for full mechanics.

| Consolidation Path                         | Requirements                                            | Result Class  | Tier |
| ------------------------------------------ | ------------------------------------------------------- | ------------- | ---- |
| [Knight](./knight/index.md)                | Warrior + honor/service focus                           | Knight        | 1    |
| [Berserker](../consolidation/index.md)     | Warrior + rage/aggression focus                         | Berserker     | 1    |
| [Weapon Master](../consolidation/index.md) | Warrior + weapon specialization                         | Weapon Master | 1    |
| [Warden](../consolidation/index.md)        | Warrior + [Blacksmith](../crafting/blacksmith/index.md) | Warden        | 1    |
| [Battlemage](../magic/index.md)            | Warrior + [Mage](../magic/index.md)                     | Battlemage    | 1    |

### Interactions

| System                                                             | Interaction                                           |
| ------------------------------------------------------------------ | ----------------------------------------------------- |
| [Combat](../../../systems/combat/combat-resolution/index.md)       | Primary melee combat class; frontline fighter         |
| [Equipment](../../../systems/combat/weapons-and-armor/index.md)    | Benefits significantly from quality weapons and armor |
| [Party](../../../systems/combat/party-mechanics/index.md)          | Tank or DPS role; protects weaker party members       |
| [Reputation](../../../systems/social/factions-reputation/index.md) | Military service and victories build reputation       |
| [Settlement](../../../systems/world/settlements/index.md)          | Guards, soldiers, and trainers in communities         |
| [Economy](../../../systems/economy/index.md)                       | Earns through mercenary work, guard contracts         |

---

## Progression

### Specializations

- [Knight](./knight/) - Honorable combat and leadership

### Consolidations

- [Battlemage](../../channeler/mage/battlemage/) - with [Mage](../../channeler/mage/) (combat spellcasting)

---

## Related Content

- **Requires:** Melee weapon, armor (optional but recommended)
- **Equipment:** [Weapons](../../items/index.md), [Armor](../../items/index.md), [Shields](../../items/index.md)
- **Synergy Classes:** [Blacksmith](../crafting/blacksmith/index.md), [Knight](./knight/index.md), [Guard](./guard/index.md)
- **Consolidates To:** [Knight](./knight/index.md), [Berserker](../consolidation/index.md), [Weapon Master](../consolidation/index.md)
- **See Also:** [Combat System](../../../systems/combat/combat-resolution/index.md), [Weapons & Armor](../../../systems/combat/weapons-and-armor/index.md), [Party Roles](../../../systems/combat/party-mechanics/index.md)
