---
title: Fighter
gdd_ref: systems/class-system-gdd.md#foundation-classes
tree_tier: 1
---

# Fighter

## Lore

The Fighter represents the first step on the warrior's path. Before specialization comes fundamentals - how to hold a weapon, how to move in combat, how to survive when violence erupts. Every great warrior, knight, and assassin began as a Fighter, learning the basics that all combat roles share.

Fighters emerge from necessity. The farmer who picks up a pitchfork to defend their family. The traveler who learns to swing a sword after bandits attack their caravan. The young recruit drilling with wooden weapons in a militia training yard. Combat finds them, and they answer.

The Fighter's Guild accepts all who seek to learn the art of combat. They don't ask why you want to fight - defense, glory, vengeance, survival - only that you commit to the training. Basic drills, sparring sessions, conditioning exercises. The fundamentals that separate those who flail in panic from those who act with purpose.

## Mechanics

### Requirements

| Requirement    | Value                      |
| -------------- | -------------------------- |
| Unlock Trigger | 100 combat-related actions |
| Primary Tags   | `Combat`                   |
| Tier           | 1 (Foundation)             |
| Max Tag Depth  | 1 level                    |

### Unlock Requirements

**Tracked Actions:**

- Attack enemies in melee or ranged combat
- Defend against incoming attacks
- Spar or train with other fighters
- Practice weapon forms

**Threshold:** 100 actions (guaranteed unlock) or early unlock via probability

### Starting Skills

Skills automatically awarded when accepting this class:

| Skill         | Type            | Link                                                               | Description                                    |
| ------------- | --------------- | ------------------------------------------------------------------ | ---------------------------------------------- |
| Combat Stance | Mechanic Unlock | [Combat Stance](././skills/mechanic-unlock/combat-stance/index.md) | Access to basic combat stances and positioning |
| Basic Strike  | Passive         | [Power Strike](././skills/common/power-strike/index.md)            | +5% damage with all weapons                    |

### XP Progression

Uses Tier 1 formula: `XP = 100 × 1.5^(level - 1)`

| Level | XP to Next | Cumulative |
| ----- | ---------- | ---------- |
| 1→2   | 100        | 100        |
| 5→6   | 506        | 1,131      |
| 10→11 | 3,844      | 6,513      |

### Tracked Actions for XP

| Action                     | XP Value |
| -------------------------- | -------- |
| Attack an enemy            | 5-15 XP  |
| Successfully defend        | 5-10 XP  |
| Win a sparring match       | 10-25 XP |
| Complete training session  | 10-20 XP |
| Defeat a challenging enemy | 20-50 XP |

### Progression Paths

Fighter can unlock eligibility for the following Tier 2 classes upon reaching 5,000 XP:

| Tier 2 Class | Focus                           | Link                                       |
| ------------ | ------------------------------- | ------------------------------------------ |
| Warrior      | Melee combat and weapon mastery | [Warrior](./combat/warrior/index.md)       |
| Archer       | Ranged combat and precision     | [Archer](./combat/archer/index.md)         |
| Scout        | Reconnaissance and skirmishing  | [Scout](./combat/scout/index.md)           |
| Guard        | Defense and protection          | [Guard](./combat/guard/index.md)           |
| Adventurer   | Generalist combat and survival  | [Adventurer](./combat/adventurer/index.md) |

### Tag Access

As a Tier 1 class, Fighter has access to depth 1 tags only:

| Accessible | Not Accessible   |
| ---------- | ---------------- |
| `Combat`   | `Combat/Melee`   |
|            | `Combat/Ranged`  |
|            | `Combat/Tactics` |

## Progression

### Specializations

- [Warrior](./warrior/) - Melee combat specialist, path to Knight
- [Archer](./archer/) - Ranged combat specialist
- [Scout](./scout/) - Reconnaissance and mobility, path to Ranger
- [Guard](./guard/) - Defensive combat specialist
- [Adventurer](./adventurer/) - Versatile explorer and survivor

## Related Content

- **Tier System:** [Class Tiers](./././systems/character/class-tiers/index.md)
- **Combat System:** [Combat Resolution](./././systems/combat/combat-resolution/index.md)
- **See Also:** [Combat Classes Index](./combat/index.md)
