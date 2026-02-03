---
type: reference
scope: detailed
status: review
version: 1.0.0
created: 2026-02-02
updated: 2026-02-02
subjects: [skills, tags, progression, the-wandering-inn]
dependencies: [twi-classes.md]
---

# The Wandering Inn: Skill Tag Mapping

## Extracted Skills with Tag Requirements, Effects & Scaling

---

## Skill Prefix System

TWI uses prefixes to denote skill power tiers:

| Prefix      | Tier | Level Range | Tag Multiplier |
| ----------- | ---- | ----------- | -------------- |
| [Basic]     | 1    | 1-10        | 1.0x           |
| [Lesser]    | 2    | 5-15        | 1.25x          |
| (none)      | 3    | 10-25       | 1.5x           |
| [Greater]   | 4    | 20-35       | 2.0x           |
| [Grand]     | 5    | 30-45       | 3.0x           |
| [Supreme]   | 6    | 40-55       | 5.0x           |
| [Legendary] | 7    | 50+         | 10.0x          |

---

## Combat Skills

### Offensive Melee

```yaml
[Power Strike]:
  tags: [combat.melee.technique.power]
  tag_requirements:
    combat.melee: 50
  type: Active
  effect: "Deal 150-300% weapon damage (scales with tier)"
  cooldown: 30s
  variants:
    - "[Lesser Power Strike]" → 150%
    - "[Power Strike]" → 200%
    - "[Greater Power Strike]" → 250%
    - "[Grand Power Strike]" → 300%

[Cleave]:
  tags: [combat.melee.technique.cleave]
  tag_requirements:
    combat.melee: 75
    combat.melee.axe: 50 OR combat.melee.sword: 50
  type: Active
  effect: "Strike all enemies in arc, damage scales with tier"
  cooldown: 20s

[Minotaur Punch]:
  tags: [combat.melee.unarmed.power, combat.melee.technique.special]
  tag_requirements:
    combat.melee.unarmed: 200
  type: Active
  effect: "Devastating unarmed strike. Massive damage + knockback."
  cooldown: 60s
  special_note: "Erin Solstice signature skill - acquired through extraordinary circumstances"

[Sword Art: (Variant)]:
  tags: [combat.melee.sword.art]
  tag_requirements:
    combat.melee.sword: 150
    combat.melee.sword.[specific]: 100
  type: Active
  effect: "Named sword technique. Effect varies by variant."
  variants:
    - "[Sword Art: Crescent Moon]" → Wide sweeping arc
    - "[Sword Art: Piercing Thrust]" → Armor penetration
    - "[Sword Art: Flowing Water]" → Defensive counter-stance
    - "[Sword Art: Sundering Strike]" → Destroys shields/weapons

[Flurry of Blows]:
  tags: [combat.melee.technique.combo, combat.melee.unarmed.combo]
  tag_requirements:
    combat.melee: 100
    combat.melee.*.combo: 75
  type: Active
  effect: "Rapid successive strikes. 3-7 hits based on tier."
  cooldown: 45s

[Bleeding Cut]:
  tags: [combat.melee.sword.slash, combat.melee.dagger.slash]
  tag_requirements:
    combat.melee.sword: 75 OR combat.melee.dagger: 75
  type: Passive (proc)
  effect: "Slashing attacks have chance to cause bleed DoT"
```

### Defensive Combat

```yaml
[Parry]:
  tags: [combat.melee.sword.parry, combat.defense.technique]
  tag_requirements:
    combat.melee.sword: 100
    combat.defense: 50
  type: Conditional
  effect: "Negate incoming melee attack. Requires timing."
  cooldown: 5s

[Block]:
  tags: [combat.defense.shield.block]
  tag_requirements:
    combat.defense.shield: 75
  type: Active
  effect: "Reduce incoming damage by 50-90% based on tier"
  cooldown: 3s

[Evasion]:
  tags: [combat.defense.evasion.dodge]
  tag_requirements:
    combat.defense.evasion: 100
  type: Conditional
  effect: "Chance to completely avoid attacks"

[Iron Skin]:
  tags: [combat.defense.armor.natural]
  tag_requirements:
    combat.defense: 150
  type: Passive
  effect: "Permanent damage reduction (3-15% by tier)"

[Second Wind]:
  tags: [combat.defense.recovery]
  tag_requirements:
    combat.defense: 100
    combat.melee: 75
  type: Conditional
  effect: "Restore 30% HP when falling below 10%. Once per combat."

[Thick Skin]:
  tags: [combat.defense.armor.natural]
  tag_requirements:
    combat.defense: 75
  type: Passive
  effect: "Minor damage reduction, resistance to cuts"

[Dangersense]:
  tags: [combat.defense.awareness, utility.exploration.detect]
  tag_requirements:
    combat.defense: 150
    utility.stealth: 50 OR gather.hunting: 50
  type: Passive
  effect: "Subconscious warning of nearby threats"
  special_note: "Highly valued survival skill"
```

### Ranged Combat

```yaml
[Aimed Shot]:
  tags: [combat.ranged.bow.aimed, combat.ranged.crossbow.aimed]
  tag_requirements:
    combat.ranged: 75
  type: Active
  effect: 'High accuracy shot with bonus damage'
  cooldown: 15s

[Volley]:
  tags: [combat.ranged.bow.volley]
  tag_requirements:
    combat.ranged.bow: 150
  type: Active
  effect: 'Fire multiple arrows in rapid succession'
  cooldown: 30s

[Quick Shot]:
  tags: [combat.ranged.technique.speed]
  tag_requirements:
    combat.ranged: 100
  type: Passive
  effect: 'Reduced draw/reload time'

[Snipe]:
  tags: [combat.ranged.bow.snipe, combat.ranged.crossbow.snipe]
  tag_requirements:
    combat.ranged: 200
    combat.ranged.*.aimed: 100
  type: Active
  effect: 'Extreme range shot. Massive damage to unaware targets.'
  cooldown: 120s
```

### Tactical Skills

```yaml
[Formation: (Type)]:
  tags: [combat.tactical.group.formation]
  tag_requirements:
    combat.tactical.group: 150
    social.leadership: 75
  type: Active (Aura)
  effect: "Grant formation bonuses to nearby allies"
  variants:
    - "[Formation: Shield Wall]" → +Defense to group
    - "[Formation: Wedge]" → +Charge damage
    - "[Formation: Circle]" → 360° defense

[Taunt]:
  tags: [combat.tactical.aggro, social.diplomacy.intimidate]
  tag_requirements:
    combat.tactical: 50
    social.diplomacy.intimidate: 25
  type: Active
  effect: "Force enemies to target you"
  cooldown: 30s

[Battlefield Awareness]:
  tags: [combat.tactical.awareness]
  tag_requirements:
    combat.tactical: 200
  type: Passive
  effect: "Perceive enemy positions, predict movements"

[Command: (Order)]:
  tags: [combat.tactical.command, social.leadership.army]
  tag_requirements:
    social.leadership: 150
    combat.tactical: 100
  type: Active
  effect: "Issue tactical commands that buff allies"
  variants:
    - "[Command: Charge]" → +Movement, +Damage on approach
    - "[Command: Hold]" → +Defense, immunity to fear
    - "[Command: Retreat]" → +Speed, reduced opportunity attacks
```

---

## Magic Skills

### Elemental

```yaml
[Fireball]:
  tags: [magic.elemental.fire.aoe]
  tag_requirements:
    magic.elemental.fire: 150
  type: Active
  effect: 'Launch explosive fire projectile'
  mana_cost: 25-60 by tier

[Ice Shard]:
  tags: [magic.elemental.ice.bolt]
  tag_requirements:
    magic.elemental.water: 75
    magic.elemental.ice: 50
  type: Active
  effect: 'Piercing ice projectile'
  mana_cost: 15-40 by tier

[Lightning Bolt]:
  tags: [magic.elemental.lightning.bolt]
  tag_requirements:
    magic.elemental.lightning: 125
  type: Active
  effect: 'Instant-hit electrical damage'
  mana_cost: 30-70 by tier

[Stone Skin]:
  tags: [magic.elemental.earth.buff]
  tag_requirements:
    magic.elemental.earth: 100
  type: Active (Buff)
  effect: 'Massive armor increase, movement penalty'
  duration: 60s
  mana_cost: 40

[Elemental Affinity: (Element)]:
  tags: [magic.elemental.*.mastery]
  tag_requirements:
    magic.elemental.*: 300
  type: Passive
  effect: 'Reduced mana cost, increased damage for element'
```

### Restoration

```yaml
[Heal]:
  tags: [magic.restoration.healing.touch]
  tag_requirements:
    magic.restoration.healing: 50
  type: Active
  effect: "Restore HP to target"
  mana_cost: 20-50 by tier
  variants:
    - "[Lesser Heal]" → 50 HP
    - "[Heal]" → 150 HP
    - "[Greater Heal]" → 400 HP
    - "[Grand Heal]" → 1000 HP

[Cleanse]:
  tags: [magic.restoration.cleanse]
  tag_requirements:
    magic.restoration: 100
  type: Active
  effect: "Remove poison, disease, or curse"
  mana_cost: 35

[Regeneration]:
  tags: [magic.restoration.healing.overtime]
  tag_requirements:
    magic.restoration.healing: 150
  type: Active (Buff)
  effect: "Continuous HP restoration over time"
  duration: 120s
  mana_cost: 50

[Sanctuary]:
  tags: [magic.restoration.buff.area, magic.arcane.ward]
  tag_requirements:
    magic.restoration: 250
    magic.arcane.ward: 100
  type: Active (Zone)
  effect: "Create protected area, healing + damage reduction"
  duration: 30s
  mana_cost: 100
```

### Arcane

```yaml
[Mana Shield]:
  tags: [magic.arcane.ward.personal]
  tag_requirements:
    magic.arcane.ward: 100
  type: Active (Buff)
  effect: "Absorb damage using mana"
  duration: Until mana depleted

[Teleport]:
  tags: [magic.arcane.teleport]
  tag_requirements:
    magic.arcane.teleport: 200
  type: Active
  effect: "Instant travel to known location"
  mana_cost: 150+
  variants:
    - "[Short Range Teleport]" → Line of sight
    - "[Teleport]" → Within region
    - "[Greater Teleport]" → Anywhere known

[Identify]:
  tags: [magic.arcane.divination, knowledge.identify]
  tag_requirements:
    magic.arcane: 50
    knowledge.identify: 50
  type: Active
  effect: "Reveal properties of items/creatures"
  mana_cost: 10

[Detect Magic]:
  tags: [magic.arcane.divination.detect]
  tag_requirements:
    magic.arcane: 75
  type: Active
  effect: "Sense magical auras and enchantments"
  mana_cost: 15
  duration: 60s

[Enchant Weapon]:
  tags: [magic.arcane.enchant.temporary]
  tag_requirements:
    magic.arcane.enchant: 100
  type: Active (Buff)
  effect: "Add elemental damage to weapon"
  duration: 300s
  mana_cost: 50
```

### Summoning & Necromancy

```yaml
[Summon: (Creature)]:
  tags: [magic.summoning.creature]
  tag_requirements:
    magic.summoning: 100
  type: Active
  effect: 'Summon creature to fight for you'
  duration: Varies
  mana_cost: 50-200

[Raise Dead]:
  tags: [magic.death.undead.raise]
  tag_requirements:
    magic.death: 150
    magic.summoning: 75
  type: Active
  effect: 'Animate corpse as undead minion'
  mana_cost: 75
  special_note: 'Red skill in many regions'

[Death Bolt]:
  tags: [magic.death.bolt]
  tag_requirements:
    magic.death: 100
  type: Active
  effect: 'Necrotic damage projectile. Heals undead.'
  mana_cost: 30
```

---

## Crafting Skills

### General Crafting

```yaml
[Quick Craft]:
  tags: [craft.*.technique.speed]
  tag_requirements:
    craft.*: 100
  type: Passive
  effect: 'Reduce crafting time by 10-30%'

[Quality Control]:
  tags: [craft.*.technique.quality]
  tag_requirements:
    craft.*: 150
  type: Passive
  effect: 'Increased chance of higher quality results'

[Material Efficiency]:
  tags: [craft.*.technique.efficiency]
  tag_requirements:
    craft.*: 125
  type: Passive
  effect: 'Chance to not consume materials'

[Repair]:
  tags: [craft.smithing.technique.repair, craft.textile.technique.repair]
  tag_requirements:
    craft.*: 75
  type: Active
  effect: 'Restore durability to items'
```

### Cooking Specific

```yaml
[Advanced Cooking]:
  tags: [craft.cooking.technique.advanced]
  tag_requirements:
    craft.cooking: 100
  type: Passive
  effect: 'Unlock advanced recipes, improve food buffs'

[Wondrous Fare]:
  tags: [craft.cooking.technique.magical]
  tag_requirements:
    craft.cooking: 250
    magic.arcane: 50
  type: Passive
  effect: 'Cooked food provides magical effects'
  special_note: 'Erin Solstice skill variant'

[Preservation]:
  tags: [craft.cooking.preservation]
  tag_requirements:
    craft.cooking.preservation: 75
  type: Passive
  effect: 'Food lasts longer without spoiling'

[Crowd Cooking]:
  tags: [craft.cooking.technique.mass]
  tag_requirements:
    craft.cooking: 150
    utility.service: 75
  type: Passive
  effect: 'Cook large quantities without quality loss'
```

### Alchemy Specific

```yaml
[Stable Mixture]:
  tags: [craft.alchemy.technique.stabilize]
  tag_requirements:
    craft.alchemy: 100
  type: Passive
  effect: 'Reduced chance of potion failure/explosion'

[Potent Brew]:
  tags: [craft.alchemy.technique.enhance]
  tag_requirements:
    craft.alchemy: 175
  type: Passive
  effect: 'Potion effects increased by 25-50%'

[Detect Poison]:
  tags: [craft.alchemy.poison.detect, knowledge.identify]
  tag_requirements:
    craft.alchemy.poison: 50
  type: Active/Passive
  effect: 'Identify poisoned food, drink, or items'

[Transmutation]:
  tags: [craft.alchemy.transmutation]
  tag_requirements:
    craft.alchemy: 300
    magic.arcane: 100
  type: Active
  effect: 'Convert materials from one type to another'
  mana_cost: High
```

### Smithing Specific

```yaml
[Forge Fire]:
  tags: [craft.smithing.technique.heat]
  tag_requirements:
    craft.smithing: 100
  type: Passive
  effect: 'Maintain optimal forge temperature automatically'

[Hammer Rhythm]:
  tags: [craft.smithing.technique.speed]
  tag_requirements:
    craft.smithing: 125
  type: Passive
  effect: 'Faster smithing, better metal working'

[Weapon Specialization: (Type)]:
  tags: [craft.smithing.weapon.*]
  tag_requirements:
    craft.smithing.weapon: 150
    craft.smithing.weapon.*: 100
  type: Passive
  effect: 'Bonus quality when crafting specific weapon type'

[Masterwork]:
  tags: [craft.smithing.technique.masterwork]
  tag_requirements:
    craft.smithing: 350
  type: Passive (Proc)
  effect: 'Chance to create masterwork quality items'
```

---

## Gathering Skills

```yaml
[Vein Sense]:
  tags: [gather.mining.detect]
  tag_requirements:
    gather.mining: 150
  type: Passive
  effect: 'Sense nearby ore veins'

[Green Thumb]:
  tags: [gather.herbalism.quality]
  tag_requirements:
    gather.herbalism: 75
  type: Passive
  effect: 'Gathered herbs have higher quality'

[Bountiful Harvest]:
  tags: [gather.*.technique.yield]
  tag_requirements:
    gather.*: 125
  type: Passive
  effect: 'Increased resource yield per node'

[Gentle Harvest]:
  tags: [gather.herbalism.technique.preserve]
  tag_requirements:
    gather.herbalism: 100
  type: Passive
  effect: 'Plants regrow faster after harvesting'

[Keen Eye]:
  tags: [gather.*.detect.rare]
  tag_requirements:
    gather.*: 175
  type: Passive
  effect: 'Increased chance to spot rare resources'

[Efficient Extraction]:
  tags: [gather.mining.technique.efficiency]
  tag_requirements:
    gather.mining: 100
  type: Passive
  effect: 'Less stamina consumed while mining'

[Tracking]:
  tags: [gather.hunting.track]
  tag_requirements:
    gather.hunting: 75
  type: Active
  effect: 'Follow creature trails, detect recent activity'
  duration: 300s

[Silent Approach]:
  tags: [gather.hunting.stealth, utility.stealth.sneak]
  tag_requirements:
    gather.hunting: 100
    utility.stealth: 75
  type: Passive
  effect: 'Reduced detection by prey animals'
```

---

## Social Skills

```yaml
[Charming Smile]:
  tags: [social.diplomacy.charm]
  tag_requirements:
    social.diplomacy: 75
  type: Passive
  effect: 'Improved NPC disposition'

[Intimidating Presence]:
  tags: [social.diplomacy.intimidate]
  tag_requirements:
    social.diplomacy.intimidate: 100
    combat.*: 75
  type: Passive/Active
  effect: 'Frighten enemies, cow NPCs into compliance'

[Appraisal]:
  tags: [social.trade.appraise, knowledge.identify]
  tag_requirements:
    social.trade: 100
  type: Active
  effect: 'Determine true value of items'

[Bulk Discount]:
  tags: [social.trade.negotiate.bulk]
  tag_requirements:
    social.trade.negotiate: 100
  type: Passive
  effect: 'Reduced prices when buying in quantity'

[Silver Tongue]:
  tags: [social.diplomacy.persuade]
  tag_requirements:
    social.diplomacy: 150
  type: Passive
  effect: 'Improved persuasion success rate'

[Loud Voice]:
  tags: [social.leadership.command.voice, social.performance]
  tag_requirements:
    social.leadership: 50
  type: Active
  effect: 'Project voice over long distances or noise'

[Inspiring Presence]:
  tags: [social.leadership.inspire]
  tag_requirements:
    social.leadership: 150
  type: Passive (Aura)
  effect: 'Allies near you gain morale bonus'

[Perfect Recall]:
  tags: [knowledge.lore.memory]
  tag_requirements:
    knowledge.lore: 200
    knowledge.research: 100
  type: Passive
  effect: 'Remember all read/heard information perfectly'
  special_note: '[Scholar] signature skill variant'
```

---

## Utility Skills

```yaml
[Basic Cleaning]:
  tags: [utility.service.clean]
  tag_requirements:
    utility.service: 25
  type: Active
  effect: "Efficiently clean an area"
  special_note: "Often first skill for [Innkeeper]"

[Quick Movement]:
  tags: [utility.exploration.travel.speed]
  tag_requirements:
    utility.exploration.travel: 75
  type: Passive
  effect: "Increased movement speed"

[Recall]:
  tags: [utility.exploration.travel.teleport]
  tag_requirements:
    utility.exploration.travel: 200
  type: Active
  effect: "Return to bound location"
  cooldown: 3600s

[Nightvision]:
  tags: [utility.exploration.sense.sight]
  tag_requirements:
    utility.exploration: 100
    utility.stealth: 50 OR gather.mining: 50
  type: Passive
  effect: "See in darkness"

[Language: (Language)]:
  tags: [knowledge.lore.language]
  tag_requirements:
    knowledge.lore: 75
    social.diplomacy: 50
  type: Passive
  effect: "Understand and speak specific language"

[Barkskin]:
  tags: [magic.nature.defense]
  tag_requirements:
    magic.nature: 100
  type: Active (Buff)
  effect: "Skin hardens like bark, +armor"
  duration: 300s

[Water Breathing]:
  tags: [magic.elemental.water.buff, utility.exploration.travel.sea]
  tag_requirements:
    magic.elemental.water: 150
  type: Active (Buff)
  effect: "Breathe underwater"
  duration: 600s
```

---

## Innkeeper-Specific Skills (Erin Solstice Notable)

```yaml
[Inn's Aura]:
  tags: [utility.service.innkeep.aura, magic.arcane.ward.area]
  tag_requirements:
    utility.service.innkeep: 200
    social.diplomacy: 100
  type: Passive (Aura)
  effect: 'Inn provides comfort, safety, and various buffs to guests'
  special_note: "Rare [Innkeeper] skill - Erin's signature"

[Immortal Moment]:
  tags: [magic.arcane.time, combat.tactical.special]
  tag_requirements:
    # Extraordinary circumstances - not normal acquisition
  type: Active
  effect: 'Freeze a moment in time. Legendary skill.'
  special_note: "Erin Solstice's most notable skill - acquired through extreme circumstances"

[Wondrous Fare]:
  tags: [craft.cooking.technique.magical, utility.service.innkeep]
  tag_requirements:
    craft.cooking: 200
    utility.service.innkeep: 150
  type: Passive
  effect: 'Food cooked has magical properties and enhanced effects'

[Grand Theatre]:
  tags: [utility.service.innkeep.performance, social.performance]
  tag_requirements:
    utility.service.innkeep: 250
    social.performance: 150
  type: Active
  effect: 'Transform inn into performance space with magical enhancements'
```

---

_Document Version 1.0 — Skills extracted from The Wandering Inn_
