---
type: reference
scope: high-level
status: review
version: 1.0.0
created: 2026-02-02
updated: 2026-02-02
subjects: [implementation, mobile, mvp]
dependencies: []
---

# Minimal Action Set for Idle Mobile Implementation

## Complete Tag Coverage with 12 Core Actions

---

## Design Philosophy

### Constraints

- **Touch-friendly**: All actions executable via tap, hold, or swipe
- **Idle-compatible**: Actions can auto-execute with minimal input
- **Combinatorial**: Small action set × modifiers = full tag coverage
- **Meaningful**: Each action feels distinct and purposeful

### Core Formula

```
ACTION + TOOL/METHOD + TARGET/LOCATION = Tag Generation
```

The **action** is the verb. The **tool/method** and **target/location** are modifiers selected by the player that determine which specific tags receive XP.

---

## The 12 Core Actions

| #   | Action      | Primary Domain | Idle-able | Input Type  |
| --- | ----------- | -------------- | --------- | ----------- |
| 1   | **Strike**  | combat         | Yes       | Auto/Tap    |
| 2   | **Defend**  | combat         | Yes       | Auto/Toggle |
| 3   | **Gather**  | gather         | Yes       | Auto/Tap    |
| 4   | **Craft**   | craft          | Semi      | Queue       |
| 5   | **Cast**    | magic          | Yes       | Auto/Tap    |
| 6   | **Travel**  | utility        | Yes       | Select      |
| 7   | **Trade**   | social         | Semi      | Select      |
| 8   | **Talk**    | social         | No        | Select      |
| 9   | **Perform** | social         | Yes       | Auto        |
| 10  | **Command** | social/combat  | Yes       | Toggle      |
| 11  | **Study**   | knowledge      | Yes       | Auto        |
| 12  | **Rest**    | utility        | Yes       | Auto        |

---

## Action Definitions & Tag Mappings

### 1. STRIKE

**The primary offensive action.**

```yaml
action: Strike
domains: [combat.melee, combat.ranged]
idle_behavior: Auto-attack current target
input: Tap to attack, Hold for power attack

modifiers:
  weapon_type:
    sword:    [combat.melee.sword]
    axe:      [combat.melee.axe]
    mace:     [combat.melee.mace]
    dagger:   [combat.melee.dagger]
    polearm:  [combat.melee.polearm]
    unarmed:  [combat.melee.unarmed]
    bow:      [combat.ranged.bow]
    crossbow: [combat.ranged.crossbow]
    thrown:   [combat.ranged.thrown]
    staff:    [magic.arcane, combat.melee]

  technique (derived from input):
    tap:           [*.slash] or [*.shoot]
    double_tap:    [*.thrust] or [*.quickshot]
    hold_release:  [*.power] or [*.aimed]
    swipe:         [*.cleave] or [*.volley]

  target_type:
    beast:     [gather.hunting.beast]
    monster:   [gather.hunting.monster]
    humanoid:  [combat.tactical]
    undead:    [magic.divine.smite.undead] (if applicable)

ui_element: Central combat button with weapon icon
idle_output: ~1 strike per 3-5 seconds based on weapon speed
```

### 2. DEFEND

**Defensive stance and reactions.**

```yaml
action: Defend
domains: [combat.defense]
idle_behavior: Auto-block/dodge when attacked
input: Toggle stance, Tap to active block

modifiers:
  method:
    shield:   [combat.defense.shield.block]
    parry:    [combat.defense.technique.parry] (requires melee weapon)
    dodge:    [combat.defense.evasion.dodge]
    armor:    [combat.defense.armor]

  stance (toggle):
    aggressive: 0.5x defense tags, 1.5x offense tags
    balanced:   1.0x all tags
    defensive:  1.5x defense tags, 0.5x offense tags

ui_element: Shield icon toggle + stance slider
idle_output: Tags generated when damage received
```

### 3. GATHER

**Resource collection from the environment.**

```yaml
action: Gather
domains: [gather.*]
idle_behavior: Auto-collect from current node/area
input: Tap to gather, Hold for careful gather

modifiers:
  location_type → resource_type:
    mine:        [gather.mining.ore.*]
    gem_vein:    [gather.mining.gem.*]
    forest:      [gather.herbalism.*, gather.logging.*]
    garden:      [gather.herbalism.flower.*, gather.farming.*]
    cave:        [gather.mining.*, gather.herbalism.fungus.*]
    field:       [gather.farming.crops.*]
    pasture:     [gather.farming.livestock.*]
    river:       [gather.fishing.freshwater.*]
    ocean:       [gather.fishing.saltwater.*]
    wilderness:  [gather.hunting.*]

  technique (derived from input):
    tap:         [*.harvest] - standard yield
    hold:        [*.careful] - quality bonus
    rapid_tap:   [*.quick] - speed bonus, quality penalty

  tool_equipped:
    pickaxe:     [gather.mining] efficiency bonus
    sickle:      [gather.herbalism] efficiency bonus
    axe:         [gather.logging] efficiency bonus
    rod:         [gather.fishing] efficiency bonus
    trap:        [gather.hunting.trap]
    net:         [gather.fishing.net]

ui_element: Context-sensitive gather button based on location
idle_output: ~1 gather per 5-10 seconds based on node
```

### 4. CRAFT

**Item creation from resources.**

```yaml
action: Craft
domains: [craft.*]
idle_behavior: Process craft queue
input: Select recipe → Add to queue → Auto-craft

modifiers:
  recipe_category:
    weapon: [craft.smithing.weapon.*]
    armor: [craft.smithing.armor.*]
    tool: [craft.smithing.tool.*]
    jewelry: [craft.smithing.jewelry.*]
    potion: [craft.alchemy.potion.*]
    poison: [craft.alchemy.poison.*]
    oil: [craft.alchemy.oil.*]
    meal: [craft.cooking.meal.*]
    preserved: [craft.cooking.preservation.*]
    baked: [craft.cooking.baking.*]
    cloth: [craft.textile.cloth.*]
    leather: [craft.textile.leather.*]
    furniture: [craft.construction.furniture.*]
    mechanism: [craft.construction.mechanism.*]
    trap: [craft.construction.trap.*]

  quality_focus (player toggle):
    speed: 1.5x craft speed, quality penalty
    balanced: 1.0x speed, normal quality
    quality: 0.5x speed, quality bonus, +[*.technique.quality]

  recipe_specific_tags:
    # Each recipe adds its specific tags
    'Iron Sword': [craft.smithing.weapon.sword, combat.melee.sword]
    'Health Potion': [craft.alchemy.potion.healing, magic.restoration]
    'Beef Stew': [craft.cooking.meal.stew]

ui_element: Crafting panel with recipe list + queue
idle_output: Crafts complete based on recipe time (30s - 10min)
```

### 5. CAST

**Magic and ability usage.**

```yaml
action: Cast
domains: [magic.*]
idle_behavior: Auto-cast selected spell when conditions met
input: Tap to cast, Select spell from loadout

modifiers:
  spell_school:
    fire:        [magic.elemental.fire.*]
    water/ice:   [magic.elemental.water.*, magic.elemental.ice.*]
    earth:       [magic.elemental.earth.*]
    air:         [magic.elemental.air.*]
    lightning:   [magic.elemental.lightning.*]
    healing:     [magic.restoration.healing.*]
    buff:        [magic.restoration.buff.*]
    ward:        [magic.arcane.ward.*]
    enchant:     [magic.arcane.enchant.*]
    illusion:    [magic.arcane.illusion.*]
    summon:      [magic.summoning.*]
    death:       [magic.death.*]
    nature:      [magic.nature.*]
    divine:      [magic.divine.*]
    pact:        [magic.pact.*]

  cast_type (from spell definition):
    bolt:        [*.bolt]
    aoe:         [*.aoe]
    touch:       [*.touch]
    self:        [*.self]
    sustained:   [*.sustained]
    ritual:      [*.ritual]

  auto_cast_conditions:
    on_cooldown: Cast when available
    hp_threshold: Cast heal when HP < X%
    enemy_count: Cast AoE when enemies > X
    buff_expired: Recast buff when duration ends

ui_element: Spell bar (4-6 slots) + auto-cast toggles
idle_output: Varies by spell cooldown (5s - 5min)
```

### 6. TRAVEL

**Movement between locations.**

```yaml
action: Travel
domains: [utility.exploration]
idle_behavior: Auto-travel on set routes
input: Select destination from map

modifiers:
  travel_method:
    walk:        [utility.exploration.travel.foot]
    run:         [utility.exploration.travel.foot.speed]
    mount:       [utility.exploration.travel.mount]
    ship:        [utility.exploration.travel.sea]
    teleport:    [utility.exploration.travel.teleport, magic.arcane.teleport]
    climb:       [utility.exploration.climb.*]
    swim:        [utility.exploration.swim.*]

  route_type:
    safe:        Standard travel, no combat
    dangerous:   [combat.*] encounters possible
    exploratory: [utility.exploration.discover.*] bonus
    stealthy:    [utility.stealth.*] required/gained

  destination_discovery:
    new_area:    [utility.exploration.discover] + [utility.exploration.map]

ui_element: World map with node selection
idle_output: Travel time varies (10s - 30min based on distance)
```

### 7. TRADE

**Economic transactions.**

```yaml
action: Trade
domains: [social.trade]
idle_behavior: Auto-sell configured items at current location
input: Select items → Set prices → Confirm

modifiers:
  transaction_type:
    buy:         [social.trade.buy]
    sell:        [social.trade.sell]
    negotiate:   [social.trade.negotiate] (interactive)
    auction:     [social.trade.auction]
    contract:    [social.trade.contract]

  trade_volume:
    single:      Base tags
    bulk:        [social.trade.negotiate.bulk]

  goods_category:
    common:      Base trade tags
    rare:        [social.trade.rare]
    illegal:     [social.trade.smuggle] (red class risk)

  auto_trade_rules:
    sell_threshold: "Sell [item] when inventory > X"
    buy_threshold:  "Buy [item] when inventory < X"
    price_limit:    "Only trade when price [above/below] X"

ui_element: Shop interface + auto-trade rule builder
idle_output: Per transaction (player-initiated or auto-rule triggered)
```

### 8. TALK

**Social interaction and dialogue.**

```yaml
action: Talk
domains: [social.diplomacy, social.reputation]
idle_behavior: NOT idle-able (requires choices)
input: Select dialogue options

modifiers:
  approach:
    friendly:    [social.diplomacy.persuade, social.diplomacy.charm]
    neutral:     [social.diplomacy] base
    intimidate:  [social.diplomacy.intimidate]
    deceive:     [social.diplomacy.deceive] (red class risk)

  context:
    quest:       [social.reputation.faction.*]
    negotiate:   [social.trade.negotiate]
    gather_info: [knowledge.lore.*]
    recruit:     [social.leadership.*]

  relationship_outcome:
    improved:    [social.reputation] +
    maintained:  Neutral
    damaged:     [social.reputation] -

ui_element: Dialogue box with branching options
idle_output: N/A - Active interaction only
```

### 9. PERFORM

**Entertainment and artistic expression.**

```yaml
action: Perform
domains: [social.performance]
idle_behavior: Auto-perform at current venue
input: Select performance type → Select venue

modifiers:
  performance_type:
    sing:        [social.performance.music.vocal]
    instrument:  [social.performance.music.*] (based on equipped)
    dance:       [social.performance.dance.*]
    act:         [social.performance.theater.*]
    story:       [social.performance.oratory.storytelling]
    comedy:      [social.performance.theater.comedy]
    acrobatics:  [social.performance.acrobatics.*]

  venue:
    street:      Low income, high exposure
    tavern:      [utility.service.innkeep] synergy
    theater:     High income, reputation requirement
    court:       [social.reputation.faction] bonus

  audience_reaction:
    success:     [social.reputation] +, income
    neutral:     Base tags only
    failure:     [social.reputation] -, possible hostility

ui_element: Performance minigame or auto-perform toggle
idle_output: ~1 performance per 2-5 minutes
```

### 10. COMMAND

**Leadership and tactical direction.**

```yaml
action: Command
domains: [social.leadership, combat.tactical]
idle_behavior: Auto-issue commands based on rules
input: Toggle command mode, Select tactics

modifiers:
  command_scope:
    party:       [social.leadership.party]
    followers:   [social.leadership.command]
    guild:       [social.leadership.guild]
    army:        [social.leadership.army, combat.tactical.group]
    settlement:  [social.leadership.settlement]

  tactical_orders:
    attack:      [combat.tactical.command.attack]
    defend:      [combat.tactical.command.defend]
    formation:   [combat.tactical.group.formation.*]
    retreat:     [combat.tactical.command.retreat]
    flank:       [combat.tactical.group.flank]

  leadership_style:
    inspiring:   [social.leadership.inspire]
    tactical:    [combat.tactical] focus
    delegating:  [social.leadership.delegate]

ui_element: Party/follower management panel + tactics dropdown
idle_output: Tags generated per command issued or combat round
```

### 11. STUDY

**Learning and research.**

```yaml
action: Study
domains: [knowledge.*]
idle_behavior: Auto-study selected topic
input: Select study material/topic

modifiers:
  study_source:
    book:        [knowledge.lore.*] based on book topic
    scroll:      [magic.arcane.*] or [knowledge.lore.*]
    experiment:  [knowledge.research.experiment]
    observation: [knowledge.research.observe]
    meditation:  [magic.divine.inner] or [magic.nature]
    prayer:      [magic.divine.prayer.*]

  topic:
    history:     [knowledge.lore.history]
    arcana:      [knowledge.lore.arcana, magic.arcane]
    nature:      [knowledge.lore.nature, magic.nature]
    religion:    [knowledge.lore.religion, magic.divine]
    combat:      [knowledge.lore.military, combat.tactical]
    creature:    [knowledge.identify.creature]
    language:    [knowledge.language.*]

  study_intensity:
    casual:      Slow XP, can multitask
    focused:     Normal XP, no other actions
    intensive:   Fast XP, fatigue buildup

ui_element: Library/study panel with topic selection
idle_output: ~1 study tick per 30-60 seconds
```

### 12. REST

**Recovery, reflection, and class advancement.**

```yaml
action: Rest
domains: [utility.service, system]
idle_behavior: Auto-rest when resources depleted
input: Select rest location, Initiate rest

modifiers:
  rest_location:
    wilderness:  [utility.exploration.survive.camp]
    inn:         [utility.service.innkeep] (NPC), faster recovery
    home:        Fastest recovery, storage access
    sanctuary:   [magic.divine.consecration], bonus effects

  rest_duration:
    short:       HP/MP recovery only (5 min)
    long:        Full recovery + class/skill check (30 min)
    extended:    Full recovery + bonus XP processing (2+ hours)

special_functions:
  - Level up processing
  - Class acquisition check
  - Class evolution check
  - Skill acquisition
  - Dream events (narrative)
  - Offline progress calculation

ui_element: Rest button + rest location selection
idle_output: Triggers system events, recovery over time
```

---

## Tag Coverage Matrix

### Combat Domain Coverage

| Tag Path               | Actions That Generate          |
| ---------------------- | ------------------------------ |
| combat.melee.\*        | Strike (melee weapon)          |
| combat.melee.sword     | Strike + sword equipped        |
| combat.melee.unarmed   | Strike + no weapon             |
| combat.ranged.\*       | Strike (ranged weapon)         |
| combat.defense.shield  | Defend + shield equipped       |
| combat.defense.evasion | Defend (dodge stance)          |
| combat.tactical.\*     | Command, Strike (group combat) |
| combat.tactical.group  | Command (party/army)           |
| combat.tactical.solo   | Strike (solo combat)           |

### Gather Domain Coverage

| Tag Path            | Actions That Generate                              |
| ------------------- | -------------------------------------------------- |
| gather.mining.\*    | Gather (mine location)                             |
| gather.herbalism.\* | Gather (forest/garden)                             |
| gather.hunting.\*   | Strike (beast/monster target), Gather (wilderness) |
| gather.logging.\*   | Gather (forest) + axe                              |
| gather.fishing.\*   | Gather (water location)                            |
| gather.farming.\*   | Gather (field/pasture)                             |

### Craft Domain Coverage

| Tag Path              | Actions That Generate              |
| --------------------- | ---------------------------------- |
| craft.smithing.\*     | Craft (metal recipes)              |
| craft.alchemy.\*      | Craft (potion/poison recipes)      |
| craft.cooking.\*      | Craft (food recipes)               |
| craft.textile.\*      | Craft (cloth/leather recipes)      |
| craft.construction.\* | Craft (building/mechanism recipes) |

### Magic Domain Coverage

| Tag Path             | Actions That Generate                 |
| -------------------- | ------------------------------------- |
| magic.elemental.\*   | Cast (elemental spells)               |
| magic.restoration.\* | Cast (healing/buff spells)            |
| magic.arcane.\*      | Cast (utility spells), Study (arcana) |
| magic.summoning.\*   | Cast (summon spells)                  |
| magic.death.\*       | Cast (necromancy spells)              |
| magic.nature.\*      | Cast (druid spells), Study (nature)   |
| magic.divine.\*      | Cast (divine spells), Study (prayer)  |
| magic.pact.\*        | Cast (warlock abilities)              |

### Social Domain Coverage

| Tag Path              | Actions That Generate           |
| --------------------- | ------------------------------- |
| social.trade.\*       | Trade                           |
| social.diplomacy.\*   | Talk                            |
| social.leadership.\*  | Command                         |
| social.performance.\* | Perform                         |
| social.reputation.\*  | Talk, Perform, Quest completion |

### Utility Domain Coverage

| Tag Path               | Actions That Generate                        |
| ---------------------- | -------------------------------------------- |
| utility.exploration.\* | Travel                                       |
| utility.stealth.\*     | Travel (stealthy), Strike (ambush)           |
| utility.service.\*     | Rest, Perform (venue), Craft (service items) |
| knowledge.\*           | Study                                        |

---

## UI Layout Recommendation

```
┌─────────────────────────────────────────────────────┐
│  [Location Banner]            [Currency] [Settings] │
├─────────────────────────────────────────────────────┤
│                                                     │
│           ┌─────────────────────────┐              │
│           │                         │              │
│           │    MAIN VIEWPORT        │              │
│           │    (Character/Scene)    │              │
│           │                         │              │
│           │   [Context Actions]     │              │
│           └─────────────────────────┘              │
│                                                     │
│  [Gather]  [Strike]  [Defend]  [Cast]  [Rest]      │
│     ○         ◉         ○        ○       ○         │
│                                                     │
├─────────────────────────────────────────────────────┤
│  [Map]  [Craft]  [Trade]  [Social]  [Character]    │
│    📍     🔨       💰       💬         👤           │
└─────────────────────────────────────────────────────┘

PRIMARY ACTION BAR (Context-Sensitive):
- Shows 5 most relevant actions for current location
- Selected action highlighted
- Tap to activate, hold for options

SECONDARY NAV BAR (Always Available):
- Opens full-screen panels for complex actions
- Craft queue, Trade rules, Talk options, Character sheet
```

### Action Button States

```
○ Available (tap to use)
◉ Active/Selected (currently executing)
◐ Cooldown (shows timer)
● Unavailable (greyed, shows requirement)
```

---

## Idle Loop Configuration

### Player-Configurable Idle Priorities

```yaml
idle_loop:
  name: 'Mining & Smithing Route'

  phases:
    - phase: gather
      location: 'Iron Mine'
      action: Gather
      duration: until_inventory_full

    - phase: travel
      destination: 'Smithy'
      action: Travel

    - phase: craft
      recipes: ['Iron Ingot', 'Iron Sword']
      action: Craft
      duration: until_resources_depleted

    - phase: travel
      destination: 'Market'
      action: Trade

    - phase: sell
      action: Trade
      rules:
        - sell: 'Iron Sword'
          when: inventory > 5

    - phase: rest
      action: Rest
      condition: hp < 50% OR fatigue > 80%

    - phase: repeat
      goto: gather

  interrupts:
    combat_encounter:
      actions: [Strike, Defend]
      flee_threshold: hp < 20%
    level_up:
      action: Rest (forced)
```

### Offline Calculation

```
On Return:
1. Calculate elapsed time
2. Simulate idle loop iterations:
   - Apply diminishing returns (see main GDD)
   - Roll for random events
   - Process combat encounters (simplified)
   - Accumulate resources and XP
3. Queue level-ups and skill acquisitions
4. Present summary to player
```

---

## Action → Class Eligibility Quick Reference

| Class         | Primary Actions                    | Secondary Actions         |
| ------------- | ---------------------------------- | ------------------------- |
| [Warrior]     | Strike                             | Defend, Command           |
| [Mage]        | Cast                               | Study                     |
| [Archer]      | Strike (ranged)                    | Gather (hunt)             |
| [Blacksmith]  | Craft (smithing)                   | Gather (mining), Trade    |
| [Alchemist]   | Craft (alchemy)                    | Gather (herbalism), Study |
| [Chef]        | Craft (cooking)                    | Gather, Trade             |
| [Merchant]    | Trade                              | Travel, Talk              |
| [Herbalist]   | Gather (herbs)                     | Craft (alchemy)           |
| [Miner]       | Gather (mining)                    | Craft (smithing)          |
| [Hunter]      | Strike (beasts), Gather            | Travel                    |
| [Bard]        | Perform                            | Talk, Cast                |
| [Diplomat]    | Talk                               | Trade, Command            |
| [Scout]       | Travel                             | Gather, Strike            |
| [Innkeeper]   | Rest (provide), Craft (cooking)    | Trade, Talk               |
| [Cleric]      | Cast (divine)                      | Study (prayer), Talk      |
| [Paladin]     | Strike, Cast (divine)              | Defend, Command           |
| [Druid]       | Cast (nature)                      | Gather, Study             |
| [Ranger]      | Strike, Gather (hunt)              | Travel, Cast (nature)     |
| [Rogue]       | Strike (stealth), Travel (stealth) | Trade, Gather             |
| [Artificer]   | Craft (mechanism)                  | Cast (enchant), Study     |
| [Necromancer] | Cast (death)                       | Study, Strike             |
| [Warlock]     | Cast (pact)                        | Study, Talk               |

---

_Document Version 1.0 — Minimal Action Set Definition_
