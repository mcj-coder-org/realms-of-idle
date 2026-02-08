---
type: system
scope: detailed
status: authoritative
version: 1.0.0
created: 2026-02-08
updated: 2026-02-08
subjects: [magic, mana, spells, shamanic, enchanting, spirit-pool, crafting]
dependencies: []
---

# Magic System - Authoritative Game Design

## Executive Summary

The Magic System defines two distinct magical traditions — Mana Magic and Shamanic Magic — each with unique resource pools, progression paths, and crafting subsystems. Mana Magic is individual-focused, powered by personal mana pools that regenerate over time. Shamanic Magic is community-focused, drawing from a shared spirit pool tied to settlements. Both systems are adapted for idle gameplay: mana recharges as a slowly filling resource, spells are assigned for auto-use, and shamanic crafting operates on timer-based production queues.

**This document resolves:**

- Mana pool mechanics and regeneration rates
- Spell assignment for auto-combat and utility queuing
- Mana Magic class progression (Channeler to Specialist)
- 9 specialist paths with representative spells
- Shamanic spirit pool as a settlement-level resource
- Spirit mood as a satisfaction modifier
- Shamanic crafting specialization paths
- Wand/staff charge system as consumable production
- Enchanting system with tiers and timers

**Design Philosophy:** Magic is a resource management puzzle, not a reaction-time challenge. Players decide which spells to assign, which specialization to pursue, and how to balance personal mana versus shared spirit pool. Magic users are idle engines — they produce value over time through regeneration, auto-casting, and crafting. The two traditions create distinct playstyles: mana mages are self-sufficient specialists, while shamans are community multipliers.

---

## 1. Mana Magic

### 1.1 Mana Pool

Every character with magical training has a personal mana pool. Mana regenerates slowly over time and is consumed by casting spells.

```
MANA POOL FORMULA:

  Max Mana = Base Mana + (WIT x 5) + (Level x 3) + Equipment Bonus

  Base Mana by Training Level:
    Untrained:    0 (cannot use mana)
    Novice:       20
    Apprentice:   50
    Journeyman:   100
    Mage:         200
    Archmage:     400

  Regeneration Rate:
    Base: 1% of max mana per minute
    Meditation (idle, not in combat): 3% per minute
    Settlement mana well: 5% per minute
    Combat: 0.5% per minute (reduced)

  Example:
    Journeyman Mage, WIT 15, Level 10:
    Max Mana = 100 + (15 x 5) + (10 x 3) + 0 = 205
    Regen (idle): 2.05 mana/min = 123 mana/hr
    Full recharge from empty: ~100 minutes
```

### 1.2 Spell Casting

Spells are assigned to auto-use slots. During combat, assigned spells are used automatically based on priority and conditions. Outside combat, utility spells can be queued.

```
SPELL SLOT SYSTEM:

  Combat Slots: 3 (spells used automatically in combat)
  Utility Slots: 2 (spells queued for out-of-combat use)

  Combat Slot Priority:
    Slot 1: Highest priority, used first when conditions met
    Slot 2: Used when Slot 1 is on cooldown or conditions not met
    Slot 3: Fallback / situational spell

  Auto-Cast Conditions (per slot):
    - Always (use on cooldown)
    - HP threshold (heal when HP < X%)
    - Enemy count (AoE when 3+ enemies)
    - Mana threshold (only if mana > X%)
    - Boss encounter (save for strong enemies)

  Utility Queue:
    Spells queued execute in order with cast timers
    Example: Enchant Weapon (10 min) → Ward Camp (5 min)
```

### 1.3 Spell Learning

Spells are learned through three methods:

```
SPELL ACQUISITION:

  Spellbook Discovery:
    Found as loot, purchased from merchants, or quest rewards
    Learn time: 1-4 hours (based on spell tier)
    Consumes the spellbook item

  Trainer:
    NPC trainers in settlements teach spells
    Cost: 50-500 gold (based on spell tier)
    Learn time: 2-8 hours
    Requires minimum training level

  Level-Up:
    Class level-ups offer spell choices
    Filtered by class specialization
    Free, instant acquisition
    1-2 spells offered per level
```

---

## 2. Mana Magic Progression

### 2.1 Class Progression

Mana magic users progress through three class tiers, with specialization at Tier 3.

```
MANA MAGIC CLASS TREE:

  Tier 1 — Channeler (Level 1-10)
    Requirements: Novice mana training
    Focus: Basic elemental spells, cantrips
    Mana efficiency: 1.0x
    Spells available: 10 basic spells

  Tier 2 — Mage (Level 11-25)
    Requirements: Apprentice mana training, Channeler Level 10
    Focus: Intermediate spells, multi-target
    Mana efficiency: 1.2x (spells cost 20% less)
    Spells available: 25 intermediate spells

  Tier 3 — Specialist (Level 26+)
    Requirements: Journeyman mana training, Mage Level 25
    Focus: School-specific mastery
    Mana efficiency: 1.5x for specialist school
    Choose one of 9 specialist paths
```

### 2.2 Specialist Paths

At Tier 3, mages choose a specialization that provides unique spells and bonuses within their school.

| Specialist    | Element/Domain | Combat Role    | Passive Bonus              | Key Spell Example        |
| ------------- | -------------- | -------------- | -------------------------- | ------------------------ |
| Pyromancer    | Fire           | Burst DPS      | +25% fire damage           | Inferno (AoE burn)       |
| Cryomancer    | Ice            | Control/DPS    | +25% ice damage, slow proc | Blizzard (AoE slow)      |
| Electromancer | Lightning      | Chain DPS      | +25% lightning, chain hit  | Chain Lightning (bounce) |
| Geomancer     | Earth          | Tank/Support   | +15% defense, +10% HP      | Stone Wall (shield)      |
| Aeromancer    | Wind           | Speed/Utility  | +20% party speed           | Gale Force (push/pull)   |
| Hydromancer   | Water          | Heal/Support   | +25% healing effectiveness | Tidal Heal (AoE heal)    |
| Necromancer   | Death          | Summon/Drain   | Summon undead minions      | Raise Dead (summon)      |
| Summoner      | Planar         | Summon/Buff    | Summon elemental allies    | Call Elemental (summon)  |
| Illusionist   | Mind           | Debuff/Control | +20% debuff duration       | Mass Confusion (AoE)     |

### 2.3 Consolidation Classes

In addition to specialists, three consolidation classes combine multiple schools:

| Class      | Schools Combined  | Role               | Unique Mechanic              |
| ---------- | ----------------- | ------------------ | ---------------------------- |
| Battlemage | Fire + Earth      | Melee/Magic hybrid | Melee attacks trigger spells |
| Enchanter  | Ice + Wind + Mind | Support/Buffer     | Permanent buff slots (3)     |
| Healer     | Water + Earth     | Pure healer        | Over-heal creates shield     |

---

## 3. Shamanic Magic

### 3.1 Spirit Pool

Shamanic magic draws from a shared spirit pool — a settlement-level resource that tribal characters contribute to and consume from.

```
SPIRIT POOL FORMULA:

  Pool Size = Base (100) + (Settlement Population x 5) + Totem Bonus

  Totem Bonus:
    Minor Totem:   +50 pool
    Major Totem:   +150 pool
    Grand Totem:   +400 pool

  Regeneration:
    Base: 2% of max pool per hour
    Active shaman performing ritual: +5% per hour per shaman
    Spirit mood bonus: See Section 3.2

  Example:
    Village (100 population), Major Totem:
    Pool = 100 + (100 x 5) + 150 = 750 spirit points
    Base regen: 15 spirit/hour
    With 2 shamans on ritual duty: 15 + (750 x 0.10) = 90 spirit/hour
```

### 3.2 Spirit Mood

The spirit mood is a satisfaction modifier that affects spirit pool regeneration and shamanic spell effectiveness. Mood is influenced by ritual actions and settlement decisions.

| Mood    | Value Range | Regen Modifier | Spell Modifier | Trigger                    |
| ------- | ----------- | -------------- | -------------- | -------------------------- |
| Pleased | 80-100      | +20%           | +20%           | Regular rituals, offerings |
| Content | 60-79       | +10%           | +10%           | Occasional rituals         |
| Neutral | 40-59       | +0%            | +0%            | Default state              |
| Uneasy  | 20-39       | -20%           | -10%           | Neglected rituals          |
| Angry   | 0-19        | -50%           | -25%           | Desecration, long neglect  |

```
SPIRIT MOOD MANAGEMENT:

  Mood Increase:
    Daily ritual performed:        +3 mood
    Offering placed at totem:      +5 mood
    Sacred grove maintained:       +2 mood/day (passive)
    Enemy shaman defeated:         +5 mood

  Mood Decrease:
    No ritual for 3+ days:         -5 mood/day
    Totem damaged:                 -15 mood
    Undead in settlement radius:   -3 mood/day
    Industrial building near totem: -2 mood/day

  Ritual Timer:
    Daily ritual: 30 minutes (requires shaman NPC)
    Offering: Instant (consumes ritual materials)
```

### 3.3 Shaman Progression

Shamans follow a parallel progression to mana mages, with nature-focused specializations:

```
SHAMAN CLASS TREE:

  Tier 1 — Spiritcaller (Level 1-10)
    Requirements: Novice shamanic training
    Focus: Basic nature spells, beast influence
    Spirit efficiency: 1.0x

  Tier 2 — Shaman (Level 11-25)
    Requirements: Apprentice shamanic training
    Focus: Communal healing, environmental control
    Spirit efficiency: 1.2x

  Tier 3 — Specialist (Level 26+)
    Requirements: Journeyman shamanic training
    Choose specialization:

    Beast Lord:
      Focus: Beast summoning and enhancement
      Passive: +2 animal companion slots
      Key spell: Pack Call (summon wolf pack)

    Storm Caller:
      Focus: Weather and lightning
      Passive: +20% lightning damage, weather control
      Key spell: Storm Front (area weather change)

    Spirit Walker:
      Focus: Healing and spirit communication
      Passive: +30% healing, spirit sight
      Key spell: Ancestral Guidance (party buff)

    Earth Warden:
      Focus: Terrain and plant manipulation
      Passive: +25% gathering yield, terrain control
      Key spell: Living Wall (vine barrier)
```

---

## 4. Shamanic Crafting

### 4.1 Crafting Specialization

Shamans can craft unique items using natural materials. Crafting operates on timer-based queues.

| Item Type    | Materials Required         | Craft Time | Effect                          |
| ------------ | -------------------------- | ---------- | ------------------------------- |
| War Paint    | Pigment, Beast Fat         | 30 min     | +10% combat stat for 4 hours    |
| Charm        | Bone, Feather, Thread      | 1 hour     | +5% specific resistance         |
| Totem        | Sacred Wood, Spirit Stone  | 4 hours    | +Spirit Pool bonus (permanent)  |
| Poultice     | Herbs, Water, Moss         | 15 min     | Heal 30% HP (consumable)        |
| Spirit Token | Silver, Crystal, Bone Dust | 2 hours    | +10 spirit points (consumable)  |
| Fetish       | Rare Bone, Gem, Sacred Ash | 8 hours    | Unique passive buff (permanent) |

### 4.2 Regional Ingredients

Shamanic crafting materials vary by region, encouraging exploration:

```
REGIONAL INGREDIENTS:

  Forest:    Sacred Heartwood, Moonmoss, Wolf Bone
  Mountain:  Crystal Quartz, Eagle Feather, Bear Fat
  Swamp:     Bog Iron, Toad Skin, Spirit Lily
  Desert:    Sun Crystal, Snake Fang, Cactus Resin
  Coast:     Pearl Dust, Whale Bone, Kelp Fiber
  Tundra:    Frost Crystal, Mammoth Ivory, Lichen Extract

  Effect: Items crafted with regional materials gain +10% effectiveness
  Rare regional materials: +25% effectiveness, 5% drop rate
```

---

## 5. Wands, Staves & Charge System

### 5.1 Charge-Based Items

Wands and staves are consumable magical items that store spell charges. They are produced via crafting or purchased from vendors.

```
CHARGE ITEM SYSTEM:

  Wand:
    Charges: 5-20 (based on tier)
    Spell stored: Single spell (fixed)
    Cast speed: Instant (no mana cost)
    Recharge: Cannot recharge, consumed when empty
    Production: Enchanter crafts in 1-4 hours

  Staff:
    Charges: 10-50 (based on tier)
    Spell stored: Up to 3 spells
    Cast speed: Normal (enhanced by staff)
    Recharge: Can recharge at mana well (1 charge per 10 minutes)
    Equipment: Occupies weapon slot, provides mana bonus

  Charge Tiers:
    Basic (5/10 charges):     Common spells, Tier 1
    Quality (10/25 charges):  Intermediate spells, Tier 2
    Superior (15/35 charges): Advanced spells, Tier 3
    Masterwork (20/50 charges): Expert spells, Tier 4
```

### 5.2 Wand Production

Wands are produced via the crafting system, providing a steady supply of consumable magic items:

```
WAND CRAFTING:

  Materials: Wood (type affects element) + Crystal + Mana Essence
  Crafting Time:
    Basic: 1 hour
    Quality: 2 hours
    Superior: 4 hours
    Masterwork: 8 hours

  Wood Types:
    Oak:       Fire spells
    Birch:     Ice spells
    Ash:       Lightning spells
    Ironwood:  Earth spells
    Willow:    Water/Healing spells
    Yew:       Death/Necromancy spells
```

---

## 6. Enchanting System

### 6.1 Enchantment Tiers

The enchanting system allows applying magical effects to equipment. Enchantments consume mana and materials, operating on timer-based crafting.

| Tier      | Success Rate | Materials Required             | Craft Time | Effect Power              |
| --------- | ------------ | ------------------------------ | ---------- | ------------------------- |
| Minor     | 90%          | 1 Mana Crystal, base mat       | 30 min     | +5% stat                  |
| Standard  | 75%          | 3 Mana Crystal, quality mat    | 2 hours    | +10% stat                 |
| Greater   | 60%          | 5 Mana Crystal, superior mat   | 6 hours    | +15% stat                 |
| Superior  | 40%          | 10 Mana Crystal, rare mat      | 12 hours   | +20% stat                 |
| Legendary | 15%          | 25 Mana Crystal, legendary mat | 24 hours   | +30% stat + unique effect |

### 6.2 Enchantment Rules

```
ENCHANTMENT SYSTEM:

  Slots per Item:
    Common gear:    1 enchantment slot
    Uncommon gear:  2 enchantment slots
    Rare gear:      3 enchantment slots
    Legendary gear: 4 enchantment slots

  Failure:
    Failed enchantment: Materials consumed, item unchanged
    Critical failure (5% of failures): Item loses 1 existing enchantment

  Stacking:
    Same enchantment type: Does NOT stack on same item
    Different types: Stack freely within slot limits
    Example: +Fire Damage + +Ice Resistance = Valid
             +Fire Damage + +Fire Damage = Invalid

  Enchanting Requirements:
    Enchanter class or Enchanting skill (Journeyman+)
    Enchanting station building in settlement
    Sufficient mana (100-500 per attempt based on tier)
```

### 6.3 Enchantment Examples

```
SAMPLE ENCHANTMENTS:

  Weapon Enchantments:
    Flame Tongue:    +10% fire damage
    Frost Bite:      +10% ice damage, 5% slow chance
    Thunder Strike:  +10% lightning damage, 3% stun chance
    Life Steal:      Heal 5% of damage dealt
    Keen Edge:       +5% critical hit chance

  Armor Enchantments:
    Fire Ward:       +15% fire resistance
    Ice Ward:        +15% ice resistance
    Fortification:   +10% max HP
    Swift:           +10% movement speed
    Thorns:          Reflect 5% melee damage

  Accessory Enchantments:
    Mana Well:       +15% mana regeneration
    Spirit Bond:     +10% spirit pool contribution
    Lucky:           +5% loot rarity
    Scholar:         +10% XP gain
    Merchant:        +10% trade value
```

---

## 7. Magic in Auto-Combat

### 7.1 Auto-Cast Behavior

During auto-resolved combat, magic users follow their spell slot assignments:

```
AUTO-COMBAT MAGIC LOGIC:

  Priority Order:
    1. Check Slot 1 conditions → Cast if met and mana available
    2. Check Slot 2 conditions → Cast if met and mana available
    3. Check Slot 3 conditions → Cast if met and mana available
    4. Basic attack (if all slots on cooldown or insufficient mana)

  Mana Conservation:
    If mana < 20%: Only cast Slot 1 (highest priority)
    If mana < 10%: Do not cast, basic attack only
    Exception: Healing spells ignore mana conservation for self

  Cooldown Management:
    Each spell has a cooldown timer
    Auto-cast respects cooldowns
    No spell can be cast twice consecutively
```

### 7.2 Shamanic Magic in Combat

Shamanic spells draw from the spirit pool rather than personal mana:

```
SHAMANIC COMBAT BEHAVIOR:

  Spirit Pool Access:
    In settlement: Full pool access
    Near totem (within 1 zone): 75% pool access
    Far from settlement (2+ zones): 50% pool access
    Remote (3+ zones): 25% pool access

  Distance Penalty:
    Shamanic spell cost increases with distance from settlement
    Base cost x Distance Modifier:
      In settlement: 1.0x
      1 zone: 1.3x
      2 zones: 1.7x
      3+ zones: 2.5x

  Implication: Shamans are strongest near their settlements
  Far-ranging parties should rely on mana magic instead
```

---

## 8. Integration Points

### 8.1 System Dependencies

```
MAGIC SYSTEM INTEGRATIONS:

  Combat System:
    - Spell slots configure auto-combat magic behavior
    - Mana/spirit as combat resources alongside stamina
    - Enchanted equipment modifies combat stats
    - Specialist paths determine combat role

  Party System:
    - Shamanic spells can heal/buff entire party
    - Party composition affects magic effectiveness
    - Mana users and shamans complement each other

  Settlement System:
    - Spirit pool is settlement-level resource
    - Enchanting station and mana well are settlement buildings
    - Totem buildings increase spirit pool
    - Shamanic rituals are settlement activities

  Crafting System:
    - Wand production via crafting queues
    - Shamanic item crafting (war paint, charms, totems)
    - Enchanting uses crafting materials
    - Regional ingredients encourage exploration

  Progression System:
    - Magic training levels unlock class tiers
    - Spell learning via level-up rewards
    - Specialist path chosen at Tier 3
    - Enchanting skill progression
```

### 8.2 Data Model Summary

```
MagicUser:
  character_id:      CharacterId
  magic_type:        Enum(Mana, Shamanic, None)
  training_level:    Enum(Untrained, Novice, Apprentice, Journeyman, Mage, Archmage)
  class_tier:        Enum(Tier1, Tier2, Tier3)
  specialist_path:   String? (null until Tier 3)
  max_mana:          Integer
  current_mana:      Integer
  combat_slots:      List<SpellSlot> (max 3)
  utility_slots:     List<SpellSlot> (max 2)
  known_spells:      List<SpellId>

SpellSlot:
  slot_index:        Integer
  spell_id:          SpellId
  condition:         Enum(Always, HPThreshold, EnemyCount, ManaThreshold, BossOnly)
  condition_value:   Integer? (threshold percentage)

SpiritPool:
  settlement_id:     SettlementId
  max_pool:          Integer
  current_pool:      Integer
  spirit_mood:       Integer (0-100)
  totem_count:       Integer
  active_rituals:    Integer
  regen_rate:        Float (per hour)

Enchantment:
  id:                UUID
  item_id:           ItemId
  type:              String (e.g., "Flame Tongue")
  tier:              Enum(Minor, Standard, Greater, Superior, Legendary)
  stat_bonus:        Map<Stat, Float>
  unique_effect:     String? (Legendary only)
```
