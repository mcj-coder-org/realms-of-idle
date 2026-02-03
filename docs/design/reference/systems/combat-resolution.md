---
title: Combat Resolution
type: system
category: reference
summary: Defense layers, damage types, combat flow, positioning, and AI targeting
---

# Combat Resolution

## 1. Defense Layers

```
Incoming Damage
    ↓
Shields (absorbs first)
    ↓ (overflow)
Armour (absorbs next)
    ↓ (overflow)
Health (last resort)
    ↓ (zero = death)
```

| Layer   | Source                                   | Recovery                   |
| ------- | ---------------------------------------- | -------------------------- |
| Shields | Equipment (physical), Aura skills, Magic | Regenerates, skill-boosted |
| Armour  | Clothing, gear                           | Repaired, replaced         |
| Health  | Base stat                                | Rest, food, healing        |

### Shield Mechanics

- Physical shields and aura shields stack
- Physical: Absorbs damage, degrades, can break
- Aura: Reduces damage by flat amount, never breaks
- If aura reduction ≥ remaining damage, complete protection

### Shield Regeneration

**Aura Shields** (from skills/magic):

| Context       | Regeneration Rate             |
| ------------- | ----------------------------- |
| In combat     | ~5% per round                 |
| Out of combat | Full in 1-2 minutes           |
| With skills   | Rate boosted by shield skills |

**Physical Shields** (equipment):

- Do NOT auto-regenerate
- Degrade when absorbing damage
- Require crafter repair:
  - Metal shields: [Blacksmith](../../content/classes/crafting/blacksmith.md)
  - Wood shields: [Carpenter](../../content/classes/crafting/carpenter.md)
- Repair quality depends on crafter skill level
- Higher quality shields have higher durability (see [Quality Tiers](../core/quality-tiers.md))

### Armour Degradation

- Degrades when absorbing damage
- Can break mid-combat
- Repaired by craftsmen (quality depends on skill/price)
- Skills can reduce degradation rate
- No "naked penalty" beyond having no protection

---

## 2. Health

### Scaling

| Source       | Health Gain                   |
| ------------ | ----------------------------- |
| Starting     | Common range (80-120)         |
| Per level-up | Random in range (+5-10)       |
| Class bonus  | Some classes grant bonus      |
| Skills       | Passive health boosts         |
| Attributes   | Endurance/Vitality affect max |

### Death

- Zero health = permadeath
- Rare skills can save from lethal damage

### Near-Death Skills

| Skill         | Tier      | Effect                                       |
| ------------- | --------- | -------------------------------------------- |
| Second Chance | Rare      | Once/day, survive killing blow at 1 HP       |
| Undying Will  | Epic      | Below 10%, brief invuln + damage boost       |
| Phoenix Soul  | Legendary | Once ever, revive after death                |
| Sacrifice     | Rare      | Party member takes lethal damage for another |

---

## 3. Damage Types

| Type     | Examples               |
| -------- | ---------------------- |
| Physical | Swords, arrows, fists  |
| Fire     | Torches, fire magic    |
| Cold     | Ice magic, environment |
| Poison   | Venom, traps           |
| Electric | Lightning magic        |
| Arcane   | Pure magic             |

High-level skills can resist multiple types ("Greater Elemental Ward").

---

## 4. Combat Flow

### Hybrid System (Real-time with Pause)

```
Combat Initiated
    ↓
Free attack (if ambush) OR Auto-pause (if enabled)
    ↓
┌─────────────────────────────────────────┐
│            COMBAT LOOP                   │
│  - Characters act based on stance/role  │
│  - AI manages skills if "Auto" enabled  │
│  - Quick slot items auto-use            │
│  - Player can PAUSE anytime             │
└─────────────────────────────────────────┘
    ↓
Resolution: Victory / Defeat / Stalemate
```

### On Pause, Player Can

- Issue party orders
- Change stances
- Manually use skills/spells/items
- Attempt enemy orders (surrender)
- Reposition characters

### Combat Speed Settings

- Slow, Normal, Fast, Instant (skip trivial fights)
- Skills/spells can affect perceived time flow

---

## 5. Combat Actions

| Action                      | Stamina Cost     |
| --------------------------- | ---------------- |
| Basic Attack (melee/ranged) | Low              |
| Defend                      | Very Low         |
| Use Skill (physical)        | Varies           |
| Use Skill (magic)           | Zero or Low      |
| Cast Spell                  | Zero (uses mana) |
| Use Item                    | Zero             |
| Aimed Attack                | Higher           |
| Fallback                    | Low              |
| Flee                        | Moderate         |
| Surrender                   | None             |

### Zero Stamina in Combat

Character collapses ("naps") until attacked → wakes with [Adrenaline](../core/adrenaline.md) burst (20-30% stamina instantly).

See the [Adrenaline Mechanic](../core/adrenaline.md) for full details on recovery amounts, skill bonuses, and related consumables.

---

## 6. Aimed Attack Outcomes

| Outcome    | Difficulty | Effect                     |
| ---------- | ---------- | -------------------------- |
| Disarm     | Moderate   | Target drops weapon        |
| Knockdown  | Moderate   | Target falls, loses action |
| Stun       | Hard       | Brief incapacitation       |
| Cripple    | Hard       | Reduce movement/speed      |
| Disrobe    | Moderate   | Damage/remove armour       |
| Non-lethal | Easy       | Knockout at zero HP        |

---

## 7. Combat Positioning

### Row System

```
[Back Row]  [Front Row]  ||  [Enemy Front]  [Enemy Back]
  Archer      Warrior    ||     Goblin       Shaman
  Mage        Fighter    ||     Orc          Archer
```

- No limit on characters per row
- Multiple rows possible for large parties
- Terrain can limit rows/layout

### Auto-Positioning Logic

| Role               | Placement                |
| ------------------ | ------------------------ |
| Tank (high armour) | Front                    |
| Melee DPS          | Front                    |
| Healer (only one)  | Back (priority: healing) |
| Healer (multiple)  | Split front/back         |
| Ranged DPS         | Back                     |
| Mage               | Back                     |

### Range Rules

| Attack Type           | Can Target                  |
| --------------------- | --------------------------- |
| Melee (standard)      | Enemy front only            |
| Melee (reach: spears) | Front from own back row     |
| Ranged                | Any row                     |
| Magic                 | Any row                     |
| AoE                   | Multiple targets, both rows |

### Flanking Bonus

Outnumbering enemy front row grants damage bonus.

### Breakthrough

| Character Type    | Ability                |
| ----------------- | ---------------------- |
| Knight (mounted)  | Charge through to back |
| Centaur           | Natural charge         |
| Minotaur          | Bull rush              |
| Berserker (skill) | Reckless charge        |
| Flying            | Bypass front entirely  |

After breakthrough:

- Charger in enemy back row
- Can attack back directly
- Becomes priority target
- Must fight way back (can get stuck)

---

## 8. Retreat Mechanics

| Action            | Effect                        |
| ----------------- | ----------------------------- |
| Fallback          | Disengage but stay with party |
| All Fallback      | Party flees together          |
| Flee (individual) | Leave party, escape alone     |
| Flee prevented    | Enemy skill stops retreat     |
| Forced Surrender  | Failed flee → surrender       |

Flee decision based on personality + loyalty.

---

## 9. AI Targeting

Based on personality and situation:

| Character Type        | Target Priority                  |
| --------------------- | -------------------------------- |
| Cowardly/Dishonest    | Lowest health (easy kills)       |
| Brave/Honest          | Highest threat, demand surrender |
| Low level/Short reach | Nearest                          |
| Charger               | Healers, casters                 |
| Protective            | Whoever threatens guarded target |

---

## 10. Healer AI

### In Combat

```
Priority 1: Prevent death
├── Can die from single attack? → Heal immediately
├── Below 25%? → Heal soon
└── No danger? → Hold resources

Priority 2: Maintain effectiveness
├── Keep damage dealers above 50%
└── Don't overheal (waste)

Priority 3: Resource efficiency
├── Cooldown skills before consumables
└── Rechargeable before limited
```

### Out of Combat

- Use free/rechargeable healing first
- Target 100% health for all
- Recover own mana before next fight

### Self-Healing

Individuals use own healing items if close to death and healer unavailable/busy.

---

## 11. Quick Slots

| Slot                                                                             | Auto-Use Trigger        | Recipe Link                                                                 |
| -------------------------------------------------------------------------------- | ----------------------- | --------------------------------------------------------------------------- |
| Health Potion                                                                    | Health below 25%        | [Healing Potions](../../content/recipes/alchemy/healing-potions/index.md)   |
| Stamina Potion                                                                   | Stamina below threshold | [Buff Potions](../../content/recipes/alchemy/buff-potions/index.md)         |
| Mana Potion                                                                      | Mana below threshold    | [Buff Potions](../../content/recipes/alchemy/buff-potions/index.md)         |
| Antidote                                                                         | Poisoned                | [Curative Potions](../../content/recipes/alchemy/curative-potions/index.md) |
| [Adrenaline Shot](../../content/recipes/alchemy/buff-potions/adrenaline-shot.md) | Zero stamina / ambushed | [Recipe](../../content/recipes/alchemy/buff-potions/adrenaline-shot.md)     |

### Quick Slot Configuration

| Setting          | Options                                    |
| ---------------- | ------------------------------------------ |
| Auto-use trigger | Zero stamina / Ambush / Both / Manual only |
| Priority         | Before/After health potion                 |
| Reserve          | Keep X in inventory (don't auto-use last)  |

See [Adrenaline Mechanic](../core/adrenaline.md) for detailed configuration options.

---

## Related Documentation

### Core Systems

- [Adrenaline Mechanic](../core/adrenaline.md) - Instant stamina recovery on combat exhaustion
- [Quality Tiers](../core/quality-tiers.md) - Equipment quality and durability
- [Stacking Rules](../core/stacking-rules.md) - How bonuses combine
- [Time and Rest](../core/time-and-rest.md) - Stamina recovery outside combat

### Character Systems

- [Attributes](../character/attributes.md) - Health, stamina, and mana pools
- [Class Progression](../character/class-progression.md) - Combat class abilities

### Crafting & Items

- [Blacksmith](../../content/classes/crafting/blacksmith.md) - Metal armor and shield repair
- [Carpenter](../../content/classes/crafting/carpenter.md) - Wooden shield repair
- [Alchemist](../../content/classes/crafting/alchemist.md) - Combat consumables
- [Buff Potions Index](../../content/recipes/alchemy/buff-potions/index.md) - Combat enhancement recipes
