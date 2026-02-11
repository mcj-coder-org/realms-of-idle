---
title: Skill Progression Types
gdd_ref: systems/core-progression-system-gdd.md#general
---

# Skill Progression Types

Skills in the game follow one of three progression patterns. Understanding these patterns helps design consistent skills and set player expectations.

---

## 1. Overview

| Progression Type | How It Advances                  | Example Skills                |
| ---------------- | -------------------------------- | ----------------------------- |
| **Tiered**       | Discrete tiers (Lesser→Enhanced) | Attribute boosts, resistances |
| **Evolving**     | Skill transforms into new skill  | Night Vision → Dark Vision    |
| **Scaling**      | Continuous XP-based improvement  | Weapon Mastery, Crafting      |

---

## 2. Tiered Skills (Stat Boosts)

### Definition

Tiered skills exist in discrete power levels. Higher tiers completely replace lower tiers. Primarily used for attribute boosts and simple passive bonuses.

### Tier Progression

| Tier     | Attribute Bonus        | Example                                   |
| -------- | ---------------------- | ----------------------------------------- |
| Lesser   | +5 primary             | Lesser Strength: +5 STR                   |
| Greater  | +5 primary, +3 related | Greater Strength: +5 STR, +3 END          |
| Enhanced | +5 pri, +3 sec, +2 ter | Enhanced Strength: +5 STR, +3 END, +2 FIN |

### Tier Replacement Rules

- Greater replaces Lesser (only one skill active per line)
- Enhanced replaces Greater (or Lesser)
- Can skip tiers with sufficient preparation
- No wasted skill slots (old tier automatically retired)

### Acquisition Methods

1. **Direct Award** - Based on accumulated XP before unlock
2. **Upgrade Path** - Having lower tier gives +40% chance for higher
3. **Level-Up Influence** - Class level affects tier probabilities

### Example Tiered Skills

| Skill Category | Lesser            | Greater          | Enhanced          | Link                                                               |
| -------------- | ----------------- | ---------------- | ----------------- | ------------------------------------------------------------------ |
| Strength       | Lesser Strength   | Greater Strength | Enhanced Strength | [Skills Index](../content/skills/index.md)                         |
| Fortitude      | Fortitude I       | Fortitude II     | Fortitude III     | [Fortitude](../content/skills/common/fortitude.md)                 |
| Climate        | Climate Tolerance | Greater Climate  | Master Climate    | [Climate Tolerance](../content/skills/common/climate-tolerance.md) |

**Source:** [Class Progression](../systems/character/class-progression.md) Section 5

---

## 3. Evolving Skills

### Definition

Evolving skills transform into entirely different (usually more powerful) skills when certain conditions are met. The new skill is distinct from the old one.

### Evolution Triggers

| Trigger Type      | Description                       | Example                           |
| ----------------- | --------------------------------- | --------------------------------- |
| XP Threshold      | Reach specific XP amount in skill | 5000 XP in Night Vision           |
| Action Count      | Perform X uses of the skill       | Use Track 100 times               |
| Level Requirement | Reach class or total level        | Reach Warrior 15                  |
| Quest/Event       | Complete specific content         | Finish the "Eyes of Shadow" quest |
| Combination       | Multiple triggers together        | 3000 XP AND level 10 AND quest    |

### Evolution vs Upgrade

| Aspect         | Tiered (Upgrade)            | Evolving                     |
| -------------- | --------------------------- | ---------------------------- |
| Skill identity | Same skill, higher tier     | New, different skill         |
| Skill name     | Lesser → Greater → Enhanced | Night Vision → Dark Vision   |
| Effect type    | Same effect, larger numbers | May have different mechanics |
| Automatic      | Yes (tier replaces)         | Often requires choice        |
| Reversible     | No                          | Usually no                   |

### Example Evolution Paths

| Base Skill   | Evolution               | Trigger               | Link                                                     |
| ------------ | ----------------------- | --------------------- | -------------------------------------------------------- |
| Night Eyes   | Dark Vision             | 5000 XP               | [Night Eyes](../content/skills/common/night-eyes.md)     |
| Keen Senses  | Eagle Eye               | Scout level 20        | [Keen Senses](../content/skills/common/keen-senses.md)   |
| Tracker      | Master Tracker          | 200 successful tracks | [Tracker](../content/skills/common/tracker.md)           |
| Danger Sense | Danger Sense (Enhanced) | Quest completion      | [Danger Sense](../content/skills/common/danger-sense.md) |

### Design Considerations

- Evolution should feel like meaningful progression
- New skill should be clearly better (not sidegrade)
- Trigger should be achievable through normal play
- Consider whether evolution is automatic or player-chosen

---

## 4. Scaling Skills

### Definition

Scaling skills improve continuously based on XP accumulation. No discrete tiers or transformations - just smooth, gradual improvement.

### Scaling Formula

```
Effectiveness = Base + (XP × Scaling Factor)

Where:
  Base = Starting effectiveness when skill unlocked
  XP = Total skill XP accumulated
  Scaling Factor = Rate of improvement (varies by skill)
```

### Example Scaling Progression

| Skill XP | Effectiveness Bonus | Stamina Cost | Notes                    |
| -------- | ------------------- | ------------ | ------------------------ |
| 0        | +0%                 | 10           | Base effectiveness       |
| 500      | +15%                | 8            | Smooth improvement       |
| 2000     | +40%                | 6            | Continuous scaling       |
| 5000     | +75%                | 4            | Expert-level performance |

### XP Sources for Scaling Skills

| Source                    | XP Gain |
| ------------------------- | ------- |
| Skill-related actions     | 1-10    |
| Successful skill use      | 5-25    |
| Great/exceptional results | 15-50   |
| Guided training session   | 10-100  |
| Unguided practice         | 5-50    |

### Diminishing Returns

Most scaling skills have soft caps:

```
If XP > 10000:
  Effective Bonus = Standard Formula × 0.75

If XP > 25000:
  Effective Bonus = Standard Formula × 0.5
```

### Example Scaling Skills

| Skill              | Scales What             | Cap    | Link                                                           |
| ------------------ | ----------------------- | ------ | -------------------------------------------------------------- |
| Weapon Mastery     | Damage with weapon type | +100%  | [Class Skills](../content/skills/class-skills.md)              |
| Lockpicking        | Success rate            | 95%    | [Quick Hands](../content/skills/common/quick-hands.md)         |
| Haggler            | Trade price improvement | ±30%   | [Haggler](../content/skills/common/haggler.md)                 |
| Swift              | Movement speed          | +50%   | [Swift](../content/skills/common/swift.md)                     |
| Crafting (various) | Quality/success rate    | Varies | [Crafting Skills](../systems/crafting/crafting-progression.md) |

---

## 5. Hybrid Skills

Some skills combine progression types:

### Tiered + Scaling

Skill has discrete tiers AND scales within each tier:

| Example             | Tiers                   | Scaling Within Tier     |
| ------------------- | ----------------------- | ----------------------- |
| Artisan's Focus     | Lesser/Greater/Enhanced | XP improves focus bonus |
| Resource Efficiency | Lesser/Greater/Enhanced | XP improves save rate   |

**Link:** [Artisan's Focus](../content/skills/tiered/artisans-focus.md)

### Evolving + Scaling

Skill scales until evolution threshold, then transforms:

| Example          | Scaling Phase         | Evolution                |
| ---------------- | --------------------- | ------------------------ |
| Apprentice Smith | Scales to 5000 XP     | Becomes Journeyman Smith |
| Basic Alchemy    | Scales until level 10 | Becomes Advanced Alchemy |

---

## 6. Skill Type by Category

### Common Skills (Primarily Scaling)

Most [common skills](../content/skills/common/index.md) use scaling progression:

- Physical skills (Swift, Climber, Swimmer)
- Perception skills (Keen Senses, Tracker)
- Social skills (Haggler, Persuasive, Diplomat)

### Tiered Skills (Attribute/Resistance Focus)

[Tiered skills](../content/skills/tiered/index.md) for stat boosts:

- Attribute boosts (Lesser/Greater/Enhanced Strength)
- Resistances (Elemental Ward tiers)
- Crafting bonuses (Masterwork Chance tiers)

### Mechanic Unlocks (Binary)

[Mechanic unlock skills](../content/skills/mechanic-unlock/index.md) are binary - you have them or you don't:

- Ambidexterity (can dual-wield)
- Beast Tongue (can communicate with animals)
- Blackmail (can blackmail NPCs)

These don't progress - they unlock capabilities.

### Class Skills (Mixed)

[Class-specific skills](../content/skills/class-skills.md) vary by class design:

- Combat classes: Often scaling (weapon mastery)
- Crafting classes: Often tiered (quality bonuses)
- Magic classes: Often evolving (spell upgrades)

---

## 7. Design Guidelines

### When to Use Each Type

| Use This Type | When                                                |
| ------------- | --------------------------------------------------- |
| **Tiered**    | Simple bonuses, clear power jumps, attribute boosts |
| **Evolving**  | Narrative progression, skill identity changes       |
| **Scaling**   | Continuous improvement, most active skills          |
| **Binary**    | Unlocking capabilities, mechanic access             |

### Progression Feel

| Type     | Player Feel                                 |
| -------- | ------------------------------------------- |
| Tiered   | "I got a major upgrade!"                    |
| Evolving | "My skill transformed into something new!"  |
| Scaling  | "I'm getting better every time I use this." |
| Binary   | "Now I can do something I couldn't before." |

---

## 8. Related Documentation

### Skill Content

- [Skills Index](../content/skills/index.md) - All skill definitions
- [Common Skills](../content/skills/common/index.md) - Universal skills
- [Tiered Skills](../content/skills/tiered/index.md) - Tier-based skills
- [Mechanic Unlocks](../content/skills/mechanic-unlock/index.md) - Binary capabilities
- [Class Skills](../content/skills/class-skills.md) - Class-specific skills

### System Documentation

- [Class Progression](../systems/character/class-progression.md) - Skill acquisition
- [Formula Glossary](formula-glossary.md) - XP and progression formulas
- [Stacking Rules](../systems/core/stacking-rules.md) - How bonuses combine
