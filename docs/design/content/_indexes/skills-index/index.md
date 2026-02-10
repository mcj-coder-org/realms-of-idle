# Skills Index

This index provides a comprehensive lookup of all skills in the game, their required tags/classes, and effects.

> **Status:** Skills documentation not yet created
> **Phase:** Planned for future content creation

## Purpose

- **Skill lookup**: Find documentation for any skill
- **Tag requirements**: See which tags are required for skills
- **Class access**: Understand which classes grant which skills
- **Skill effects**: Track what each skill does

## Skill Categories

### Crafting Skills

| Skill                | Required Tags                  | Classes With Access | Documentation |
| -------------------- | ------------------------------ | ------------------- | ------------- |
| Basic Forging        | `Crafting/Smithing`            | Blacksmith          | _(TBD)_       |
| Advanced Forging     | `Crafting/Smithing/Apprentice` | Blacksmith (Lv10+)  | _(TBD)_       |
| Basic Leatherworking | `Crafting/Leatherworking`      | Leatherworker       | _(TBD)_       |
| Basic Woodworking    | `Crafting/Woodworking`         | Woodworker          | _(TBD)_       |

### Combat Skills

| Skill        | Required Tags           | Classes With Access | Documentation |
| ------------ | ----------------------- | ------------------- | ------------- |
| Sword Combat | `Combat/Melee/Sword`    | Warrior, Knight     | _(TBD)_       |
| Archery      | `Combat/Ranged`         | Archer              | _(TBD)_       |
| Shield Block | `Combat/Defense/Shield` | Guard, Knight       | _(TBD)_       |

### Gathering Skills

| Skill     | Required Tags         | Classes With Access | Documentation |
| --------- | --------------------- | ------------------- | ------------- |
| Herbalism | `Gathering/Herbalism` | Herbalist           | _(TBD)_       |
| Mining    | `Gathering/Mining`    | _(TBD)_             | _(TBD)_       |

## Skills by Class

### Crafter Classes

**Crafter (Tier 1):**

- _(Foundation skills TBD)_

**Blacksmith (Tier 2):**

- Basic Forging (passive)
- Forge Operation
- Material Knowledge: Metal

**Weaponsmith (Tier 3):**

- Advanced Weapon Forging
- Weapon Balance
- Masterwork Weapons

**Armorsmith (Tier 3):**

- Advanced Armor Forging
- Armor Fitting
- Masterwork Armor

### Fighter Classes

**Fighter (Tier 1):**

- _(Foundation skills TBD)_

**Warrior (Tier 2):**

- Melee Combat Mastery
- Weapon Proficiency

**Archer (Tier 2):**

- Ranged Combat Mastery
- Bow Proficiency

**Guard (Tier 2):**

- Defensive Tactics
- Shield Proficiency

## Skill Mechanics

### Skill Tiers

| Tier      | Description          | Example            |
| --------- | -------------------- | ------------------ |
| Basic     | Foundation ability   | Basic Forging      |
| Advanced  | Enhanced ability     | Advanced Forging   |
| Master    | Expert-level ability | Masterwork Forging |
| Legendary | Peak ability         | Legendary Forging  |

### Skill Acquisition

Skills are acquired through:

1. **Class acceptance**: Auto-awarded foundational skills
2. **Level progression**: Skills unlock at specific levels
3. **Skill training**: Manual skill learning
4. **Mastery**: Advanced skills through practice

## How to Use This Index

### For Content Creators

1. **Find skill**: Locate skill documentation and requirements
2. **Check access**: Verify which classes can learn skill
3. **Document effects**: Ensure skill mechanics are clear
4. **Tag requirements**: Match skill tags to class grants

### For Developers

1. **Skill lookup**: Find skill definitions and effects
2. **Tag validation**: Verify skill tag requirements
3. **Class mapping**: Understand skill-to-class relationships
4. **Implementation**: Match documentation to code

### For Auditors

1. **Cross-reference**: Verify skills match class documentation
2. **Completeness**: Check all skills have documentation
3. **Consistency**: Ensure tag requirements match class grants
4. **Coverage**: Track undocumented skills

## Maintenance

This index should be updated when:

- New skills are created
- Skill requirements change
- Class skill grants are modified
- Skill effects are updated

**Update process:**

```bash
# Skills documentation location
docs/design/content/skills/

# When skills are implemented, update:
# 1. Required tags
# 2. Classes with access
# 3. Documentation links
# 4. Skill effects
```

## Related Documentation

- [Tag System Documentation](../../systems/content/tag-system/index.md)
- [Classes Index](classes-index.md)
- [Tags Index](tags-index.md)
- [Content Template](../_templates/content-template/index.md)

---

**Last Updated:** 2026-01-28
**Maintained By:** Content Curator persona
**Status:** Placeholder - awaiting skill documentation creation
