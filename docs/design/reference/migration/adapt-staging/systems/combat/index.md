<!-- ADAPTATION REQUIRED -->
<!-- This file was migrated from source but needs manual review: -->
<!-- - Update terminology (dormant classes, XP split, etc.) -->
<!-- - Align with current GDD architecture -->
<!-- - Add missing sections as needed -->
<!-- - Update frontmatter with correct gdd_ref -->

---

title: Combat Systems
type: index
summary: Combat resolution, morale, parties, and equipment

---

# Combat Systems

These systems govern how combat works, from individual fights to party tactics.

## Documents

| Document                                           | Description                                |
| -------------------------------------------------- | ------------------------------------------ |
| [combat-resolution.md](combat-resolution.md)       | Damage, defense, positioning, AI           |
| [morale-and-surrender.md](morale-and-surrender.md) | Rout, capture, ransom, prisoner mechanics  |
| [party-mechanics.md](party-mechanics.md)           | Party formation, loyalty, orders, training |
| [weapons-and-armor.md](weapons-and-armor.md)       | Equipment categories, mastery, degradation |

## Combat Overview

### Health Layers

```
SHIELD (temporary, absorbs first)
    ↓
ARMOUR (equipment-based DR)
    ↓
HEALTH (actual hit points)
```

### Row Positioning

- **Front Row**: Melee access, vulnerable
- **Middle Row**: Limited melee, some ranged
- **Back Row**: Ranged only, protected

### Combat Modes

| Mode   | Description                                         |
| ------ | --------------------------------------------------- |
| Auto   | AI controls all actions based on personality/orders |
| Manual | Player controls every decision                      |
| Hybrid | Auto with pause points for player override          |

## Related Content

- [Combat Skills](../../content/skills/class/combat.md)
- [Weapons Library](../../content/items/weapons/)
- [Armor Library](../../content/items/armor/)
