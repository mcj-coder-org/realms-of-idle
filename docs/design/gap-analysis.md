---
type: system
scope: detailed
status: draft
version: 1.0.0
created: 2026-02-02
updated: 2026-02-02
subjects: [taxonomy, progression, classes]
dependencies: []
---

# Tag Taxonomy Gap Analysis

## Identified Gaps & D&D-Inspired Additions

---

## 1. Gap Analysis Summary

### Coverage Assessment by Domain

| Domain                | Current Coverage | Gaps Identified                 | Priority |
| --------------------- | ---------------- | ------------------------------- | -------- |
| `combat.melee`        | Strong           | Grappling, reach weapons        | Medium   |
| `combat.ranged`       | Moderate         | Firearms, siege, thrown mastery | High     |
| `combat.defense`      | Strong           | Reaction-based, magical defense | Low      |
| `combat.tactical`     | Moderate         | Stealth tactics, naval          | High     |
| `craft.alchemy`       | Strong           | Artifice, rune-crafting         | Medium   |
| `craft.smithing`      | Strong           | Tinkering, clockwork            | Medium   |
| `craft.cooking`       | Strong           | Brewing distinction             | Low      |
| `craft.textile`       | Weak             | Full tree underdeveloped        | High     |
| `craft.construction`  | Moderate         | Traps, mechanisms               | Medium   |
| `gather.mining`       | Strong           | -                               | Low      |
| `gather.herbalism`    | Strong           | Fungi specialization            | Low      |
| `gather.hunting`      | Strong           | Monster specialization          | Medium   |
| `gather.logging`      | Weak             | Full tree underdeveloped        | High     |
| `gather.fishing`      | Weak             | Full tree underdeveloped        | High     |
| `gather.farming`      | Missing          | Entire subtree needed           | Critical |
| `magic.elemental`     | Strong           | Sound, gravity, void            | Medium   |
| `magic.restoration`   | Moderate         | Resurrection, limb regrowth     | Medium   |
| `magic.arcane`        | Strong           | Illusion specialization         | Medium   |
| `magic.summoning`     | Moderate         | Planar, construct types         | High     |
| `magic.death`         | Moderate         | Soul magic, curses              | Medium   |
| `magic.nature`        | Weak             | Full tree needed                | Critical |
| `magic.divine`        | Missing          | Entire domain needed            | Critical |
| `social.trade`        | Strong           | Auction, contracts              | Low      |
| `social.diplomacy`    | Strong           | Deception specialization        | Medium   |
| `social.leadership`   | Strong           | Inspiration mechanics           | Low      |
| `social.performance`  | Weak             | Full tree needed                | High     |
| `social.reputation`   | Moderate         | Infamy mechanics                | Medium   |
| `utility.exploration` | Moderate         | Climbing, swimming, survival    | High     |
| `utility.stealth`     | Moderate         | Disguise, forgery               | Medium   |
| `utility.knowledge`   | Weak             | Research, crafting lore         | High     |
| `utility.service`     | Moderate         | More service types              | Medium   |

---

## 2. Critical Missing Subtrees

### 2.1 `gather.farming` — NEW DOMAIN

```
gather.farming
  ├─ crops
  │   ├─ grain, vegetable, fruit, fiber
  │   └─ [technique]: plant, tend, harvest, rotate
  ├─ livestock
  │   ├─ cattle, poultry, swine, sheep, exotic
  │   └─ [technique]: breed, raise, milk, shear
  ├─ apiary
  │   ├─ honey, wax, royal_jelly
  │   └─ [technique]: tend, harvest, relocate
  └─ aquaculture
      ├─ fish_farm, shellfish, seaweed
      └─ [technique]: stock, feed, harvest
```

### 2.2 `magic.nature` — EXPANDED

```
magic.nature
  ├─ growth
  │   ├─ plant, fungus, bloom, entangle
  │   └─ [technique]: accelerate, shape, command
  ├─ beast
  │   ├─ speak, command, charm, summon
  │   └─ [technique]: calm, enrage, bond
  ├─ weather
  │   ├─ rain, wind, storm, fog
  │   └─ [technique]: call, disperse, intensify
  ├─ terrain
  │   ├─ earth, water, forest, mountain
  │   └─ [technique]: shape, traverse, sense
  └─ shapeshift
      ├─ beast_form, elemental_form, hybrid
      └─ [technique]: partial, full, sustained
```

### 2.3 `magic.divine` — NEW DOMAIN

```
magic.divine
  ├─ blessing
  │   ├─ protection, fortune, strength, wisdom
  │   └─ [technique]: bestow, sustain, share
  ├─ smite
  │   ├─ undead, fiend, heretic, evil
  │   └─ [technique]: weapon, bolt, aura
  ├─ prayer
  │   ├─ petition, intercession, miracle
  │   └─ [technique]: commune, channel, invoke
  └─ consecration
      ├─ ground, item, creature, building
      └─ [technique]: sanctify, ward, cleanse
```

### 2.4 `social.performance` — EXPANDED

```
social.performance
  ├─ music
  │   ├─ vocal, string, wind, percussion
  │   └─ [technique]: solo, ensemble, improvise
  ├─ theater
  │   ├─ acting, comedy, tragedy, mime
  │   └─ [technique]: monologue, ensemble, improv
  ├─ dance
  │   ├─ formal, folk, ritual, combat
  │   └─ [technique]: solo, partner, group
  ├─ oratory
  │   ├─ speech, sermon, debate, storytelling
  │   └─ [technique]: inspire, persuade, entertain
  └─ acrobatics
      ├─ tumbling, aerial, contortion, juggling
      └─ [technique]: perform, escape, evade
```

### 2.5 `utility.knowledge` — EXPANDED

```
utility.knowledge → knowledge (promoted to domain)
  ├─ lore
  │   ├─ history, religion, arcana, nature, military, geography
  │   └─ [technique]: recall, research, deduce
  ├─ identify
  │   ├─ item, creature, spell, material, poison
  │   └─ [technique]: examine, compare, appraise
  ├─ research
  │   ├─ experiment, document, theorize, discover
  │   └─ [technique]: systematic, intuitive, collaborative
  ├─ language
  │   ├─ common, exotic, ancient, coded
  │   └─ [technique]: speak, read, write, decipher
  └─ teaching
      ├─ mentor, lecture, demonstrate, guide
      └─ [technique]: individual, group, practical
```

---

## 3. D&D Archetype Classes to Add

### 3.1 Divine/Holy Classes

```yaml
[Cleric]:
  tags_primary: [magic.divine, magic.restoration]
  tags_secondary: [combat.melee, knowledge.lore.religion]
  acquisition:
    magic.divine: 100
    magic.restoration: 75
  evolution_paths:
    Lv.10: [Battle Priest], [Healer Priest], [Exorcist], [Missionary]
    Lv.20: [High Priest], [Templar], [Inquisitor], [Saint]
    Lv.30: [Arch-Priest], [Divine Champion]
  source: "D&D Cleric"

[Paladin]:
  tags_primary: [combat.melee, magic.divine.smite]
  tags_secondary: [magic.divine.blessing, social.leadership]
  acquisition:
    combat.melee: 150
    magic.divine: 100
    social.reputation.faction: 100  # Sworn oath
  evolution_paths:
    Lv.10: [Crusader], [Defender], [Avenger], [Redeemer]
    Lv.20: [Holy Champion], [Oath Knight], [Divine Blade]
    Lv.30: [Paragon], [Saint-Warrior]
  source: "D&D Paladin"

[Monk]:
  tags_primary: [combat.melee.unarmed, magic.divine.inner]
  tags_secondary: [combat.defense.evasion, utility.exploration]
  acquisition:
    combat.melee.unarmed: 150
    magic.divine.inner: 75  # Or meditation tag
  evolution_paths:
    Lv.10: [Disciple], [Ascetic], [Temple Guard]
    Lv.20: [Master Monk], [Way of (Style)], [Enlightened One]
    Lv.30: [Grandmaster], [Transcendent]
  source: "D&D Monk"
```

### 3.2 Nature/Primal Classes

```yaml
[Ranger]:
  tags_primary: [gather.hunting, combat.ranged, utility.exploration]
  tags_secondary: [magic.nature, utility.stealth]
  acquisition:
    gather.hunting: 100
    combat.ranged: 75
    utility.exploration: 75
  evolution_paths:
    Lv.10: [Beast Master], [Horizon Walker], [Monster Slayer], [Gloom Stalker]
    Lv.20: [Master Ranger], [Wild Warden], [Bounty Hunter]
    Lv.30: [Legendary Ranger], [Nature's Blade]
  source: "D&D Ranger"

[Druid]:  # Expanded from core concept
  tags_primary: [magic.nature]
  tags_secondary: [gather.herbalism, knowledge.lore.nature]
  acquisition:
    magic.nature: 150
    gather.herbalism: 75
  evolution_paths:
    Lv.10: [Circle of Land], [Circle of Moon], [Circle of Spores], [Wildfire Druid]
    Lv.20: [Archdruid], [Shapeshifter], [Storm Caller]
    Lv.30: [Nature's Avatar], [Primal Ancient]
  source: "D&D Druid (expanded)"

[Barbarian]:
  tags_primary: [combat.melee, combat.tactical.solo]
  tags_secondary: [gather.hunting, utility.exploration.survive]
  acquisition:
    combat.melee: 150
    combat.tactical.solo: 100
    # Low magic affinity requirement
  evolution_paths:
    Lv.10: [Berserker], [Totem Warrior], [Storm Herald], [Ancestral Guardian]
    Lv.20: [Warlord], [Primal Champion], [Rage Master]
    Lv.30: [Legendary Barbarian], [Avatar of Fury]
  source: "D&D Barbarian"
```

### 3.3 Arcane Specialist Classes

```yaml
[Warlock]:
  tags_primary: [magic.arcane, magic.pact]
  tags_secondary: [social.diplomacy, knowledge.lore.planar]
  acquisition:
    magic.arcane: 100
    magic.pact: 75  # Pact with entity
  evolution_paths:
    Lv.10: [Fiend Pact], [Fey Pact], [Great Old One Pact], [Celestial Pact]
    Lv.20: [Pact Master], [Eldritch Knight], [Hex Blade]
    Lv.30: [Archwarlock], [Pact Lord]
  source: "D&D Warlock"

[Sorcerer]:
  tags_primary: [magic.arcane.innate]
  tags_secondary: [magic.elemental, magic.arcane]
  acquisition:
    magic.arcane.innate: 100  # Bloodline-based
  evolution_paths:
    Lv.10: [Draconic Bloodline], [Wild Magic], [Storm Sorcery], [Shadow Magic]
    Lv.20: [Arcane Conduit], [Bloodline Master]
    Lv.30: [Arch-Sorcerer], [Living Magic]
  source: "D&D Sorcerer"

[Illusionist]:
  tags_primary: [magic.arcane.illusion]
  tags_secondary: [social.performance, utility.stealth]
  acquisition:
    magic.arcane.illusion: 150
  evolution_paths:
    Lv.10: [Phantasmist], [Shadow Weaver], [Mirror Mage]
    Lv.20: [Grand Illusionist], [Reality Bender]
    Lv.30: [Master of Illusions], [Dream Walker]
  source: "D&D Illusionist Wizard"
```

### 3.4 Skill Specialist Classes

```yaml
[Rogue]:  # Expanded from core archetype
  tags_primary: [utility.stealth, combat.melee.dagger]
  tags_secondary: [social.trade, utility.exploration]
  acquisition:
    utility.stealth: 100
    combat.melee.dagger: 50  # OR utility.stealth.lockpick
  evolution_paths:
    Lv.10: [Thief], [Assassin], [Scout], [Swashbuckler], [Mastermind]
    Lv.20: [Shadow Master], [Guild Master], [Phantom]
    Lv.30: [Legendary Rogue], [Shadow Lord]
  source: "D&D Rogue"
  note: "Non-red version - criminal acts push toward [Thief] red class"

[Artificer]:
  tags_primary: [craft.construction.mechanism, magic.arcane.enchant]
  tags_secondary: [craft.smithing, knowledge.research]
  acquisition:
    craft.construction.mechanism: 100
    magic.arcane.enchant: 75
  evolution_paths:
    Lv.10: [Alchemist-Artificer], [Armorer], [Artillerist], [Battle Smith]
    Lv.20: [Master Artificer], [Golem Crafter], [Siege Engineer]
    Lv.30: [Grand Artificer], [Wonder Maker]
  source: "D&D Artificer"

[Tinkerer]:
  tags_primary: [craft.construction.mechanism]
  tags_secondary: [craft.smithing, knowledge.research]
  acquisition:
    craft.construction.mechanism: 100
  evolution_paths:
    Lv.10: [Clockwork Smith], [Trap Maker], [Gadgeteer]
    Lv.20: [Master Tinkerer], [Automaton Crafter]
    Lv.30: [Grand Inventor]
  source: "Gnome Tinkerer archetype"
```

### 3.5 Social/Support Classes

```yaml
[Warlord]:
  tags_primary: [combat.tactical, social.leadership]
  tags_secondary: [combat.melee, knowledge.lore.military]
  acquisition:
    combat.tactical: 150
    social.leadership: 150
  evolution_paths:
    Lv.10: [Tactical Leader], [Inspiring Commander], [Resourceful Leader]
    Lv.20: [Battle Lord], [Grand Marshal]
    Lv.30: [Legendary Warlord], [Conqueror]
  source: "D&D 4e Warlord"

[Noble]:
  tags_primary: [social.reputation, social.leadership]
  tags_secondary: [social.diplomacy, knowledge.lore]
  acquisition:
    social.reputation.faction: 150
    social.leadership: 100
  evolution_paths:
    Lv.10: [Knight Banneret], [Courtier], [Heir]
    Lv.20: [Lord/Lady], [Baron/Baroness], [Ambassador]
    Lv.30: [Duke/Duchess], [Prince/Princess]
  source: "D&D Noble background"

[Charlatan]:
  tags_primary: [social.diplomacy.deceive, utility.stealth.disguise]
  tags_secondary: [social.performance, social.trade]
  acquisition:
    social.diplomacy.deceive: 100
    utility.stealth.disguise: 75
  evolution_paths:
    Lv.10: [Con Artist], [Impersonator], [Fortune Teller]
    Lv.20: [Master of Disguise], [Grifter Lord]
    Lv.30: [Legendary Charlatan], [Face Stealer]
  source: "D&D Charlatan background"
  red_class_risk: Moderate if used for serious fraud
```

---

## 4. New Skills to Fill Gaps

### 4.1 Divine Skills

```yaml
[Smite Evil]:
  tags: [magic.divine.smite]
  tag_requirements:
    magic.divine: 100
    combat.melee: 75
  type: Active
  effect: 'Add divine damage to weapon attack vs evil creatures'
  source: 'D&D Paladin'

[Turn Undead]:
  tags: [magic.divine.smite.undead]
  tag_requirements:
    magic.divine: 75
  type: Active
  effect: 'Force undead to flee or be destroyed'
  source: 'D&D Cleric'

[Divine Favor]:
  tags: [magic.divine.blessing]
  tag_requirements:
    magic.divine: 50
  type: Active (Buff)
  effect: 'Bless self or ally with combat bonuses'
  source: 'D&D Cleric'

[Lay on Hands]:
  tags: [magic.divine.healing.touch]
  tag_requirements:
    magic.divine: 75
    magic.restoration.healing: 50
  type: Active
  effect: 'Heal through touch using divine power pool'
  source: 'D&D Paladin'

[Aura of Protection]:
  tags: [magic.divine.blessing.aura]
  tag_requirements:
    magic.divine: 200
  type: Passive (Aura)
  effect: 'Allies near you gain saving throw bonuses'
  source: 'D&D Paladin'
```

### 4.2 Nature/Druid Skills

```yaml
[Wild Shape]:
  tags: [magic.nature.shapeshift]
  tag_requirements:
    magic.nature: 150
  type: Active
  effect: 'Transform into beast form'
  source: 'D&D Druid'

[Speak with Animals]:
  tags: [magic.nature.beast.speak]
  tag_requirements:
    magic.nature: 50
  type: Active
  effect: 'Communicate with beasts'
  source: 'D&D Druid/Ranger'

[Entangle]:
  tags: [magic.nature.growth.entangle]
  tag_requirements:
    magic.nature.growth: 75
  type: Active
  effect: 'Cause plants to restrain creatures in area'
  source: 'D&D Druid'

[Call Lightning]:
  tags: [magic.nature.weather.storm]
  tag_requirements:
    magic.nature.weather: 150
  type: Active
  effect: 'Summon storm and call down lightning bolts'
  source: 'D&D Druid'

[Animal Companion]:
  tags: [magic.nature.beast.bond]
  tag_requirements:
    magic.nature.beast: 100
    gather.hunting: 75
  type: Passive
  effect: 'Bond with beast that fights alongside you'
  source: 'D&D Ranger'
```

### 4.3 Rogue/Stealth Skills

```yaml
[Sneak Attack]:
  tags: [combat.tactical.ambush, utility.stealth]
  tag_requirements:
    utility.stealth: 100
    combat.melee: 75
  type: Conditional
  effect: 'Massive bonus damage when striking unaware foes'
  source: 'D&D Rogue'

[Cunning Action]:
  tags: [utility.stealth.technique, combat.defense.evasion]
  tag_requirements:
    utility.stealth: 75
  type: Passive
  effect: 'Hide, dash, or disengage as bonus action'
  source: 'D&D Rogue'

[Evasion]:
  tags: [combat.defense.evasion.reflex]
  tag_requirements:
    combat.defense.evasion: 150
  type: Passive
  effect: 'Take no damage on successful dodge of AoE'
  source: 'D&D Rogue/Monk'

[Uncanny Dodge]:
  tags: [combat.defense.evasion.reaction]
  tag_requirements:
    combat.defense.evasion: 125
  type: Conditional
  effect: 'Halve damage from attack you can see'
  source: 'D&D Rogue'

[Reliable Talent]:
  tags: [utility.*.mastery]
  tag_requirements:
    utility.*: 250
  type: Passive
  effect: 'Cannot roll below 10 on proficient skills'
  source: 'D&D Rogue'
```

### 4.4 Artificer/Crafting Skills

```yaml
[Infuse Item]:
  tags: [magic.arcane.enchant.temporary, craft.smithing]
  tag_requirements:
    magic.arcane.enchant: 100
    craft.*: 100
  type: Active
  effect: 'Temporarily enchant mundane items'
  source: 'D&D Artificer'

[Construct Homunculus]:
  tags: [magic.summoning.construct, craft.construction.mechanism]
  tag_requirements:
    magic.summoning.construct: 100
    craft.construction.mechanism: 100
  type: Active
  effect: 'Create small construct servant'
  source: 'D&D Artificer'

[Trap Mastery]:
  tags: [craft.construction.trap]
  tag_requirements:
    craft.construction.trap: 100
  type: Passive
  effect: 'Create and detect traps with expertise'
  source: 'D&D Rogue/Artificer'

[Clockwork Precision]:
  tags: [craft.construction.mechanism.clockwork]
  tag_requirements:
    craft.construction.mechanism: 150
  type: Passive
  effect: 'Crafted mechanisms are more reliable and precise'
  source: 'Tinkerer archetype'
```

### 4.5 Warlock/Pact Skills

```yaml
[Eldritch Blast]:
  tags: [magic.pact.bolt]
  tag_requirements:
    magic.pact: 50
  type: Active
  effect: 'Force damage cantrip that scales with level'
  source: 'D&D Warlock'

[Pact Boon]:
  tags: [magic.pact.boon]
  tag_requirements:
    magic.pact: 100
  type: Passive
  effect: 'Gain pact weapon, familiar, or tome from patron'
  source: 'D&D Warlock'

[Dark One's Blessing]:
  tags: [magic.pact.fiend]
  tag_requirements:
    magic.pact: 100
    magic.pact.fiend: 75
  type: Conditional
  effect: 'Gain temporary HP when reducing enemy to 0 HP'
  source: 'D&D Warlock Fiend Patron'

[Hexblade's Curse]:
  tags: [magic.pact.curse]
  tag_requirements:
    magic.pact: 100
  type: Active
  effect: 'Curse target for bonus damage and crit chance'
  source: 'D&D Warlock Hexblade'
```

---

## 5. New Tag Subtrees Required

### 5.1 `magic.pact` — NEW

```
magic.pact
  ├─ fiend
  │   └─ [technique]: bargain, channel, corrupt
  ├─ fey
  │   └─ [technique]: charm, beguile, escape
  ├─ celestial
  │   └─ [technique]: heal, smite, protect
  ├─ eldritch
  │   └─ [technique]: madness, tentacle, void
  └─ [general]
      ├─ boon, curse, invocation
      └─ [technique]: channel, invoke, sacrifice
```

### 5.2 `magic.arcane.illusion` — EXPANDED

```
magic.arcane.illusion
  ├─ visual
  │   ├─ image, disguise, invisibility
  │   └─ [technique]: static, moving, interactive
  ├─ auditory
  │   ├─ sound, voice, silence
  │   └─ [technique]: localized, area, ventriloquism
  ├─ mental
  │   ├─ phantasm, fear, dream
  │   └─ [technique]: single_target, area, persistent
  └─ shadow
      ├─ quasi_real, shadow_conjuration
      └─ [technique]: damage, create, transport
```

### 5.3 `craft.construction` — EXPANDED

```
craft.construction
  ├─ building
  │   ├─ residential, commercial, fortification, monument
  │   └─ [technique]: frame, finish, reinforce
  ├─ furniture
  │   ├─ seating, storage, decorative, functional
  │   └─ [technique]: carve, join, finish
  ├─ mechanism
  │   ├─ clockwork, trap, lock, automation
  │   └─ [technique]: design, assemble, calibrate
  ├─ vehicle
  │   ├─ cart, wagon, ship, siege
  │   └─ [technique]: frame, propulsion, steering
  └─ instrument
      ├─ musical, scientific, navigation
      └─ [technique]: tune, calibrate, craft
```

### 5.4 `utility.exploration` — EXPANDED

```
utility.exploration
  ├─ travel
  │   ├─ foot, mount, sea, air, teleport
  │   └─ [technique]: pathfind, navigate, endure
  ├─ discover
  │   ├─ secret, trap, treasure, path
  │   └─ [technique]: search, detect, reveal
  ├─ survive
  │   ├─ forage, shelter, fire, water
  │   └─ [technique]: improvise, prepare, endure
  ├─ climb
  │   ├─ natural, artificial, magical
  │   └─ [technique]: free, assisted, rapid
  ├─ swim
  │   ├─ surface, diving, current
  │   └─ [technique]: endurance, speed, stealth
  └─ map
      ├─ chart, memory, magical
      └─ [technique]: survey, record, share
```

---

## 6. Implementation Priority

### Phase 1 (Critical)

1. Add `gather.farming` domain with full subtree
2. Add `magic.divine` domain with full subtree
3. Expand `magic.nature` fully
4. Add [Cleric], [Paladin], [Ranger], [Barbarian] classes

### Phase 2 (High)

1. Expand `social.performance` fully
2. Expand `utility.exploration` fully
3. Add `magic.pact` domain
4. Add [Warlock], [Monk], [Artificer], [Rogue] classes

### Phase 3 (Medium)

1. Expand `craft.construction` with mechanisms
2. Expand `magic.arcane.illusion`
3. Add [Sorcerer], [Illusionist], [Warlord] classes
4. Add all divine and nature skills

### Phase 4 (Polish)

1. Fill remaining gathering subtrees
2. Add specialized evolution paths
3. Balance pass on all tag requirements
4. Add cross-domain synergy skills

---

_Document Version 1.0 — Gap Analysis Complete_
