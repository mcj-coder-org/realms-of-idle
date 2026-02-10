---
title: Archer
gdd_ref: systems/class-system-gdd.md#specialization-classes
parent: classes/fighter/index.md
tree_tier: 2
---

# Archer

## Lore

### Origin

The bow transformed warfare. Before ranged weapons, combat meant closing to melee distance - dangerous, exhausting, limiting tactical options. Arrows kill from safety, striking enemies before they reach allies. A skilled Archer eliminates threats while vulnerable comrades remain unharmed. This capability makes Archers invaluable in any conflict.

Archery requires different skills than melee combat. Instead of strength and aggression, it demands precision, patience, and breath control. Drawing a bow requires significant strength, but consistency matters more than raw power. Archers train for years to hit targets reliably at distance, accounting for wind, target movement, arrow drop. The best Archers make it look effortless - smooth draws, steady aims, clean releases producing uncanny accuracy.

Different cultures develop distinct archery traditions. Longbowmen train from childhood, building strength to draw powerful bows. Mounted archers shoot from horseback, combining riding and shooting skills. Crossbowmen trade skill requirements for raw power - crossbows are easier to master but slower. Elven Archers achieve supernatural accuracy through centuries of practice. Each tradition insists its method is superior, though all produce effective combatants.

### In the World

Armies prize Archers for their force multiplication. A hundred Archers can decimate approaching forces before melee begins. City defenders use Archers on walls to harass besiegers. Hunters become Archers naturally, applying animal-tracking skills to human targets. Some Archers work as assassins, though most consider such use dishonorable - killing from hiding feels cowardly compared to open battle.

Archery requires constant practice. The muscle memory degrades quickly without regular shooting. Professional Archers drill daily, shooting hundreds of arrows to maintain precision. This dedication costs significant time and resources - arrows aren't cheap, and practice ranges need space. Communities that field Archer forces must support this ongoing training.

Tactical Archers focus on more than hitting targets. They choose positions offering clear shots while providing cover. They conserve arrows, knowing resupply is difficult mid-battle. They prioritize targets - enemy commanders, spellcasters, other Archers. They coordinate volleys, creating arrow storms overwhelming enemy defenses. They shift positions when enemies close, refusing to let melee combatants reach them.

Apprentice Archers start with target practice at close range, building proper form. Draw, anchor, aim, release - the sequence becomes automatic through endless repetition. They progress to longer distances, learning to judge range and adjust aim. They practice shooting while moving, from concealment, in bad weather, under pressure. Eventually they join actual combat, discovering that shooting at living targets who shoot back differs enormously from practice ranges.

Experienced Archers develop specialties. Some focus on rapid shooting, launching arrows quickly with decent accuracy. Others pursue perfect precision, making impossible shots at extreme distances. Some master trick shots - curving arrows around obstacles, hitting tiny weak points, shooting through narrow gaps. Each Archer finds their style through experience and preference.

---

## Mechanics

### Prerequisites

| Requirement        | Value                                                       |
| ------------------ | ----------------------------------------------------------- |
| XP Threshold       | 5,000 XP from ranged combat tracked actions                 |
| Related Foundation | [Fighter](../fighter/index.md) (optional, provides bonuses) |
| Tag Depth Access   | 2 levels (e.g., `Combat/Ranged`)                            |

### Requirements

| Requirement       | Value                                         |
| ----------------- | --------------------------------------------- |
| Unlock Trigger    | Defeat enemies at range using bow or crossbow |
| Primary Attribute | FIN (Finesse), AWR (Awareness)                |
| Starting Level    | 1                                             |
| Tools Required    | Bow or crossbow, ammunition                   |

### Stats

#### Base Class Stats

| Level | HP Bonus | Stamina Bonus | Trait             |
| ----- | -------- | ------------- | ----------------- |
| 1     | +6       | +12           | Apprentice Archer |
| 10    | +18      | +35           | Journeyman Archer |
| 25    | +40      | +75           | Master Archer     |
| 50    | +75      | +140          | Legendary Archer  |

#### Combat Bonuses

| Class Level | Ranged Damage | Accuracy | Crit Chance | Range Bonus |
| ----------- | ------------- | -------- | ----------- | ----------- |
| 1-9         | +5%           | +10%     | +2%         | +0%         |
| 10-24       | +20%          | +25%     | +8%         | +25%        |
| 25-49       | +45%          | +50%     | +18%        | +50%        |
| 50+         | +80%          | +85%     | +30%        | +100%       |

#### Starting Skills (Auto-Awarded on Class Acceptance)

Skills automatically awarded when accepting this class:

| Skill      | Tier   | Link                                                 | Reasoning                                    |
| ---------- | ------ | ---------------------------------------------------- | -------------------------------------------- |
| Steady Aim | Lesser | [See Skill](../../skills/common/steady-aim/index.md) | Essential for archery accuracy and precision |
| Rapid Shot | Lesser | [See Skill List](../../skills/tiered/index.md)       | Core archer technique for effective combat   |

#### Synergy Skills

Skills with strong synergies for Archer:

- [Steady Aim](../../skills/common/steady-aim/index.md) - Lesser/Greater/Enhanced - Increased accuracy with ranged weapons
- [Rapid Shot](../../skills/tiered/index.md) - Lesser/Greater/Enhanced - Reduced time between shots
- [Power Draw](../../skills/index.md) - Lesser/Greater/Enhanced - Increased ranged damage
- [Precise Shot](../../skills/index.md) - Lesser/Greater/Enhanced - Increased critical hit chance
- [Long Range](../../skills/index.md) - Lesser/Greater/Enhanced - Extended effective range
- [Moving Target](../../skills/index.md) - Lesser/Greater/Enhanced - Reduced penalty when hitting moving targets
- [Piercing Arrow](../../skills/index.md) - Lesser/Greater/Enhanced - Arrows penetrate armor
- [Multi-Shot](../../skills/index.md) - Lesser/Greater/Enhanced - Fire multiple arrows simultaneously
- [Pinning Shot](../../skills/index.md) - Lesser/Greater/Enhanced - Chance to immobilize targets
- [Trick Shot](../../skills/index.md) - Lesser/Greater/Enhanced - Make impossible shots
- [Wind Reading](../../skills/index.md) - Lesser/Greater/Enhanced - Compensate for wind conditions
- [Ambush Archer](../../skills/index.md) - Lesser/Greater/Enhanced - Bonus damage from hiding
- [Arrow Recovery](../../skills/index.md) - Lesser/Greater/Enhanced - Recover fired arrows
- [Snapshot](../../skills/index.md) - Lesser/Greater/Enhanced - Quick shots with reduced penalty
- [Perfect Release](../../skills/index.md) - No tiers - Guaranteed critical hit once per battle

**Note**: All skills listed have strong synergies because they are core archery skills. Characters without Archer class can still learn these skills but progress at base rate (1x XP) without effectiveness bonuses.

#### Synergy Bonuses

Archer provides context-specific bonuses based on logical specialization:

**Core Archery Skills** (Direct specialization - Strong synergy):

- **Steady Aim**: Essential for precision shooting
  - Faster learning: 2x XP from ranged combat actions (scales with class level)
  - Better effectiveness: +25% accuracy bonus at Archer 15, +35% at Archer 30+
  - Reduced cost: -25% stamina at Archer 15, -35% at Archer 30+
- **Rapid Shot**: Directly tied to bow proficiency
  - Faster learning: 2x XP from rapid fire situations
  - Better fire rate: Additional -20% time between shots at Archer 15
  - Lower stamina drain: -30% stamina cost per rapid shot
- **Power Draw**: Core skill for maximum damage
  - Faster learning: 2x XP from high-damage shots
  - Better effectiveness: +30% damage bonus at Archer 15
  - Reduced strain: -25% fatigue from power draws at Archer 15
- **Precise Shot**: Fundamental to critical hits
  - Faster learning: 2x XP from precision shooting
  - Better critical chance: +10% critical hit chance at Archer 15
  - More consistency: +25% critical damage at Archer 15

**Synergy Strength Scales with Class Level**:

| Archer Level | XP Multiplier | Effectiveness Bonus | Cost Reduction | Example: Steady Aim  |
| ------------ | ------------- | ------------------- | -------------- | -------------------- |
| Level 5      | 1.5x (+50%)   | +15%                | -15% stamina   | Good accuracy        |
| Level 10     | 1.75x (+75%)  | +20%                | -20% stamina   | Strong precision     |
| Level 15     | 2.0x (+100%)  | +25%                | -25% stamina   | Excellent aim        |
| Level 30+    | 2.5x (+150%)  | +35%                | -35% stamina   | Perfect marksmanship |

#### Tracked Actions

Actions that grant XP to the Archer class:

| Action Category      | Specific Actions                                     | XP Value         |
| -------------------- | ---------------------------------------------------- | ---------------- |
| Ranged Combat        | Defeat enemies using bows or crossbows               | 10-80 per kill   |
| Precision Shooting   | Hit difficult targets, long-distance shots           | 5-30 per hit     |
| Practice             | Target practice, drilling techniques                 | 3-15 per session |
| Arrow Craft          | Craft or repair arrows, maintain bow                 | 2-10 per session |
| Tactical Positioning | Choose effective shooting positions                  | 5-20 per battle  |
| Moving Shot          | Hit targets while moving                             | 10-40 per hit    |
| Covering Fire        | Suppress enemies, protect allies with ranged support | 10-50 per battle |
| Extreme Range        | Successfully hit targets at extreme distances        | 20-80 per shot   |
| Critical Shot        | Eliminate key targets with precise shots             | 30-120 per kill  |
| Volley Coordination  | Coordinate group archery for maximum effect          | 20-60 per volley |

#### Class Consolidation

See [Class Consolidation System](../../../systems/character/class-consolidation/index.md) for full mechanics.

| Consolidation Path                          | Requirements                                                                 | Result Class   | Tier |
| ------------------------------------------- | ---------------------------------------------------------------------------- | -------------- | ---- |
| [Ranger](../consolidation/index.md)         | Archer + [Scout](./scout/index.md) or [Hunter](../gathering/hunter/index.md) | Ranger         | 1    |
| [Sniper](../consolidation/index.md)         | Archer + precision specialization                                            | Sniper         | 1    |
| [Mounted Archer](../consolidation/index.md) | Archer + mounted combat                                                      | Mounted Archer | 1    |
| [Master Bowyer](../consolidation/index.md)  | Archer + [Carpenter](../crafting/carpenter/index.md)                         | Master Bowyer  | 1    |

### Interactions

| System                                                              | Interaction                                                 |
| ------------------------------------------------------------------- | ----------------------------------------------------------- |
| [Combat](../../../systems/combat/combat-resolution/index.md)        | Ranged DPS; strikes before enemies close                    |
| [Party](../../../systems/combat/party-mechanics/index.md)           | Backline damage dealer; requires protection                 |
| [Hunting](../../../systems/crafting/gathering/index.md)             | Crossover with [Hunter](../gathering/hunter/index.md) class |
| [Crafting](../../../systems/crafting/crafting-progression/index.md) | Can craft arrows and maintain bows                          |
| [Tactics](../../../systems/combat/index.md)                         | Positioning and range management critical                   |
| [Economy](../../../systems/economy/index.md)                        | Arrow costs are ongoing expense                             |

---

## Related Content

- **Requires:** Bow or crossbow, arrows or bolts
- **Equipment:** [Bows](../../items/index.md), [Crossbows](../../items/index.md), [Arrows](../../items/index.md), [Quivers](../../items/index.md)
- **Synergy Classes:** [Hunter](../gathering/hunter/index.md), [Scout](./scout/index.md), [Carpenter](../crafting/carpenter/index.md)
- **Consolidates To:** [Ranger](../consolidation/index.md), [Sniper](../consolidation/index.md), [Master Bowyer](../consolidation/index.md)
- **See Also:** [Combat System](../../../systems/combat/combat-resolution/index.md), [Ranged Combat](../../../systems/combat/index.md), [Bow Crafting](../../../systems/crafting/index.md)
