---
title: '[Entry Name]'
type: '[class|skill|material|item|recipe|enchantment|creature|race]'
category: '[subcategory]'
tier: '[basic|apprentice|journeyman|master|legendary]'
summary: 'One-line mechanical description'
---

# [Entry Name]

## Tags

> **Required Section:** Tags enable compile-time safe tag access in C# code and drive the skill/class/recipe systems.

### C# Reference

**Strongly Typed Tags:** Use these tags in C# code for compile-time safety.

| Tag Path                   | C# Reference                         | Purpose               |
| -------------------------- | ------------------------------------ | --------------------- |
| `Crafting/Smithing`        | `SkillTags.Crafting.Smithing.Value`  | Smithing skill access |
| `Crafting/Smithing/Weapon` | `SkillTags.Crafting.Smithing.Weapon` | Weapon crafting       |

**How to find class tags:**

1. Check `ClassDefinition.GrantedTags` in class definition files (e.g., `src/CozyFantasyRpg/Content/Classes/CraftingClasses.cs`)
2. Verify against `src/CozyFantasyRpg/Shared/SkillTags.cs` for exact C# reference

### Tag Access

This content requires the following tags for access/synergy:

| Tag                        | Depth | Classes With Access                 |
| -------------------------- | ----- | ----------------------------------- |
| `Crafting/Smithing`        | 2     | Blacksmith, Weaponsmith, Armorsmith |
| `Crafting/Smithing/Weapon` | 3     | Weaponsmith                         |

**Note:** Tag depth determines which classes can access this content. See [Tag System](../../systems/content/tag-system.md) for details.

---

## Lore

### Origin

[1-2 paragraphs: where this comes from, who uses it, cultural significance]

### In the World

[How common folk interact with this - cozy slice-of-life perspective]

---

## Mechanics

### Requirements

| Requirement | Value                     |
| ----------- | ------------------------- |
| Class/Skill | [required class or skill] |
| Level       | [minimum level]           |
| Materials   | [list if applicable]      |

### Base Stats

> **Required Section:** Document the baseline statistics for this content type at its default quality/power level.

#### Stats Table

| Stat        | Base Value | Description               |
| ----------- | ---------- | ------------------------- |
| [Stat Name] | [value]    | [What this stat controls] |
| [Stat Name] | [value]    | [What this stat controls] |
| [Stat Name] | [value]    | [What this stat controls] |

#### Example Tables by Content Type

**Items (Weapons/Armor):**

| Stat       | Base Value | Scale Stat | Per Point |
| ---------- | ---------- | ---------- | --------- |
| Damage     | 10         | Strength   | +1.5      |
| Accuracy   | 85%        | Dexterity  | +0.5%     |
| Durability | 100        | Crafting   | +10       |

**Materials:**

| Stat     | Base Value | Quality Tier | Bonus |
| -------- | ---------- | ------------ | ----- |
| Hardness | 5          | +1 per tier  | +20%  |
| Purity   | 80%        | +5% per tier | N/A   |
| Rarity   | Common     | N/A          | N/A   |

**Recipes:**

| Stat         | Base Value | Difficulty   | Time    |
| ------------ | ---------- | ------------ | ------- |
| Success Rate | 75%        | -5% per tier | 2 hours |
| Output       | 1 item     | +1 at master | N/A     |
| XP Gain      | 50         | +10 per tier | N/A     |

**Classes:**

| Stat  | Base Value | Per Level | Max Bonus |
| ----- | ---------- | --------- | --------- |
| HP    | 100        | +10       | +500      |
| MP    | 50         | +5        | +250      |
| Carry | 30 kg      | +2 kg     | +50 kg    |

### Stat Scaling

> **Required Section:** Explain how stats scale with quality, level, proficiency, or other factors.

#### Scaling Formulas

**Linear Scaling:**

```
FinalValue = BaseValue + (ScaleFactor × Modifier)
Example: Iron Sword Damage = 10 + (1.5 × Strength)
```

**Percentage Scaling:**

```
FinalValue = BaseValue × (1 + (BonusPercent / 100))
Example: Critical Chance = 5% × (1 + (Luck / 10))
```

**Tier-Based Scaling:**

```
TierValue = BaseValue × (1 + ((Tier - 1) × TierMultiplier))
Example: Master Quality = Base × (1 + (3 × 0.2)) = 1.6× Base
```

**Quality Multipliers:**

| Tier       | Multiplier | Requires           |
| ---------- | ---------- | ------------------ |
| Basic      | 1.0×       | None               |
| Apprentice | 1.2×       | Level 10           |
| Journeyman | 1.4×       | Level 25           |
| Master     | 1.6×       | Level 40           |
| Legendary  | 2.0×       | Level 50 + special |

#### Scaling Examples

**Weapon Damage Scaling:**

```
Base Damage: 10
Strength Modifier: 1.5 per point
Quality Multiplier: 1.2× (Apprentice)

Final Damage = (10 + (1.5 × Strength)) × 1.2
At Strength 15: (10 + 22.5) × 1.2 = 39 damage
```

**Material Quality Scaling:**

```
Base Hardness: 5
Quality Tier: Journeyman (+2 tiers)
Tier Bonus: +20% per tier

Final Hardness = 5 × (1 + (2 × 0.2)) = 7 hardness
```

**Recipe Success Scaling:**

```
Base Success: 75%
Difficulty Penalty: -5% per tier above recipe
Skill Bonus: +2% per recipe level

Final Success = 75 - (5 × (TierDiff)) + (2 × RecipeLevel)
```

### Stats

[Additional type-specific statistics or notes - use Base Stats table for primary data]

### Interactions

| System           | Interaction          |
| ---------------- | -------------------- |
| [Related System] | [How this interacts] |

---

## Related Content

- **Requires:** [prerequisites]
- **Used By:** [dependent content]
- **See Also:** [related items]
