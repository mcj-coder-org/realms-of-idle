# Idle Worlds: A Class-Based Idle MMORPG
## Game Design Document v1.0

### 5.5 Recipes & Spells

Recipes and Spells follow the same tag-based system for discovery and eligibility.

**Recipe Structure:**
```yaml
[Minor Healing Potion]:
  tags: 
    - craft.alchemy.potion.healing
    - magic.restoration.healing
  unlock_requirements:
    - craft.alchemy.potion: 50
    - gather.herbalism.flower: 25
  ingredients:
    - Moonpetal (gather.herbalism.flower.moonpetal)
    - Spring Water (gather.fishing.freshwater)
  crafting_tags_gained:           # Building XP while crafting
    - craft.alchemy.potion: +3
    - craft.alchemy.potion.healing: +2

[Steel Longsword]:
  tags:
    - craft.smithing.weapon.sword
    - combat.melee.sword
  unlock_requirements:
    - craft.smithing.weapon: 100
    - gather.mining.ore.iron: 75
  ingredients:
    - Iron Ingot x3 (craft.smithing.material.ingot.iron)
    - Leather Strip (craft.textile.leather)
  crafting_tags_gained:
    - craft.smithing.weapon: +5
    - craft.smithing.weapon.sword: +3
```

**Spell Structure:**
```yaml
[Fireball]:
  tags:
    - magic.elemental.fire.aoe
  unlock_requirements:
    - magic.elemental.fire: 150
    - magic.elemental.fire.bolt: 75   # Must master basic first
  cast_tags_gained:
    - magic.elemental.fire: +2
    - magic.elemental.fire.aoe: +3
  mana_cost: 25

[Healing Touch]:
  tags:
    - magic.restoration.healing.touch
  unlock_requirements:
    - magic.restoration.healing: 100
  cast_tags_gained:
    - magic.restoration.healing: +2
    - magic.restoration.healing.touch: +1
  mana_cost: 15
```

**Discovery Mechanics:**
- Recipes/Spells enter the "discoverable pool" when tag requirements approach threshold
- Discovery happens through: experimentation, NPC teachers, loot drops, player trading
- Higher tag affinity = higher discovery chance from random sources

---

## 1. Executive Summary

**Idle Worlds** is a persistent multiplayer idle RPG inspired by organic class/skill progression systems like those in *The Wandering Inn*. Players don't choose classes—they *earn* them through their actions. The idle mechanics simulate a character living their life, gaining experience through meaningful activity, and growing in unexpected directions based on player choices and emergent gameplay.

**Core Fantasy:** You are not the hero yet. You are a person in a living world, and what you become depends entirely on what you do.

---

## 2. Core Design Pillars

| Pillar | Description |
|--------|-------------|
| **Emergent Identity** | Classes are discovered, not selected. Players shape who they become. |
| **Meaningful Idling** | Time spent offline matters, but *how* you set up your idle loops determines growth. |
| **Social Ecosystem** | Players form an interconnected economy where [Blacksmiths] need [Miners] need [Merchants]. |
| **Horizontal Depth** | Multi-classing is viable but comes with trade-offs. Specialists and generalists both have value. |
| **Milestone Moments** | Level-ups feel significant. Skill acquisitions are memorable events, not stat noise. |

---

## 3. The Tag Taxonomy System

The foundation of all progression is a **hierarchical tagging system** that connects actions, classes, skills, recipes, and spells through shared semantic relationships.

### 3.1 Tag Structure

Tags follow a hierarchical dot-notation format with 3-4 levels of specificity:

```
[Domain].[Category].[Specialization].[Technique]

Examples:
  combat.melee.sword.parry
  craft.alchemy.potion.healing
  gather.mining.ore.iron
  magic.elemental.fire.aoe
  social.trade.negotiate.bulk
```

### 3.2 Core Tag Domains

```
combat
  ├─ melee
  │   ├─ sword, axe, mace, polearm, unarmed, dagger
  │   └─ [technique]: slash, thrust, parry, riposte, cleave, combo
  ├─ ranged
  │   ├─ bow, crossbow, thrown, firearm
  │   └─ [technique]: aimed, volley, quickshot, snipe
  ├─ defense
  │   ├─ shield, armor, evasion, positioning
  │   └─ [technique]: block, deflect, dodge, fortify
  └─ tactical
      ├─ solo, group, siege, ambush
      └─ [technique]: flank, retreat, charge, hold

craft
  ├─ alchemy
  │   ├─ potion, poison, oil, transmutation
  │   └─ [technique]: brew, distill, infuse, stabilize
  ├─ smithing
  │   ├─ weapon, armor, tool, jewelry
  │   └─ [technique]: forge, temper, engrave, repair
  ├─ cooking
  │   ├─ meal, preservation, baking, beverage
  │   └─ [technique]: roast, stew, ferment, spice
  ├─ textile
  │   ├─ cloth, leather, enchanted, dye
  │   └─ [technique]: weave, tan, stitch, embroider
  └─ construction
      ├─ building, furniture, mechanism, fortification
      └─ [technique]: frame, finish, reinforce, automate

gather
  ├─ mining
  │   ├─ ore, gem,ite,ite
  │   └─ [resource]: iron, copper, gold, mythril, diamond
  ├─ herbalism
  │   ├─ flower, root, fungus, moss
  │   └─ [resource]: moonpetal, bloodroot, ghostcap, verdant
  ├─ hunting
  │   ├─ beast, monster, rare, legendary
  │   └─ [technique]: track, trap, skin, butcher
  ├─ logging
  │   ├─ softwood, hardwood, exotic, magical
  │   └─ [resource]: pine, oak, ironwood, eldertree
  └─ fishing
      ├─ freshwater, saltwater, deep, magical
      └─ [technique]: cast, net, spear, lure

magic
  ├─ elemental
  │   ├─ fire, water, earth, air, lightning
  │   └─ [technique]: bolt, wave, aoe, sustained
  ├─ restoration
  │   ├─ healing, cleanse, buff, resurrection
  │   └─ [technique]: touch, aoe, overtime, emergency
  ├─ arcane
  │   ├─ enchant, ward, teleport, divination
  │   └─ [technique]: permanent, temporary, ritual, instant
  └─ summoning
      ├─ creature, elemental, spirit, construct
      └─ [technique]: bind, command, dismiss, empower

social
  ├─ trade
  │   ├─ buy, sell, negotiate, appraise
  │   └─ [technique]: bulk, rare, contract, auction
  ├─ diplomacy
  │   ├─ persuade, intimidate, deceive, charm
  │   └─ [technique]: speech, gesture, gift, alliance
  ├─ leadership
  │   ├─ command, inspire, coordinate, delegate
  │   └─ [technique]: party, guild, army, settlement
  └─ reputation
      ├─ faction, settlement, individual, global
      └─ [technique]: quest, service, donation, feat

utility
  ├─ exploration
  │   ├─ travel, discover, map, survive
  │   └─ [technique]: pathfind, climb, swim, camp
  ├─ stealth
  │   ├─ sneak, hide, disguise, lockpick
  │   └─ [technique]: shadow, crowd, distract, escape
  ├─ knowledge
  │   ├─ lore, identify, research, teach
  │   └─ [technique]: read, experiment, mentor, document
  └─ service
      ├─ innkeep, courier, guide, mercenary
      └─ [technique]: host, deliver, escort, protect
```

### 3.3 Tag Applications

| Entity | Tag Usage |
|--------|-----------|
| **Actions** | Each action has 1-4 tags describing what it is |
| **Classes** | Define required tag affinities for acquisition and evolution |
| **Skills** | Specify tag prerequisites and the tags they enhance |
| **Recipes** | Categorize by craft type and unlock requirements |
| **Spells** | Categorize by magic school and technique |

### 3.4 Tag Affinity Tracking

The system maintains a rolling **Tag Affinity Score** for each player:

```
Player Tag Affinity Map:
{
  "combat.melee.sword": 847,
  "combat.melee.sword.parry": 234,
  "combat.defense.shield": 412,
  "craft.smithing.weapon": 156,
  "gather.mining.ore.iron": 523,
  ...
}
```

**Affinity Decay:** Tags decay at 5% per week of inactivity to reflect current playstyle over historical.

---

## 4. The Class System

### 4.1 Class Acquisition

Players begin **classless**. Classes are granted when the system detects sufficient **Tag Affinity** in relevant domains.

**Class Tag Requirements:**
Each class defines minimum tag affinity thresholds:

```yaml
[Herbalist]:
  required_tags:
    - gather.herbalism: 100        # Primary requirement
  supporting_tags:                  # Boost acquisition speed
    - craft.alchemy: 50
    - knowledge.identify: 25
  excluding_tags:                   # Delay/prevent if too high
    - combat.*: < 200              # Not primarily a fighter

[Warrior]:
  required_tags:
    - combat.melee: 100
  supporting_tags:
    - combat.defense: 50
    - combat.tactical: 25
```

**Acquisition Flow:**
```
Player spends first 2 hours:
  → Gathers herbs (67 actions)
      +67 gather.herbalism
      +67 gather.herbalism.flower (moonpetal)
      +34 gather.herbalism.root (bloodroot)
  → Sells herbs to NPC (12 actions)
      +12 social.trade.sell
  → Brews 3 simple potions (21 actions)
      +21 craft.alchemy.potion
      +21 craft.alchemy.potion.healing
  → Identifies a plant for another player (1 action)
      +5 knowledge.identify (social bonus multiplier)
      +2 social.diplomacy.service

[REST CYCLE - Tag Affinity Check]
  gather.herbalism: 134 ✓ (threshold: 100)
  craft.alchemy: 21 (supporting)
  
"You have gained the class: [Herbalist] - Level 1"
"You have gained the skill: [Green Thumb]"
```

### 4.2 Class Categories & Primary Tags

| Category | Example Classes | Primary Tag Domain |
|----------|-----------------|-------------------|
| **Combat** | [Warrior], [Archer], [Duelist], [Guardian] | `combat.*` |
| **Craft** | [Blacksmith], [Alchemist], [Weaver], [Chef] | `craft.*` |
| **Gather** | [Miner], [Herbalist], [Hunter], [Fisher] | `gather.*` |
| **Social** | [Merchant], [Diplomat], [Bard], [Leader] | `social.*` |
| **Magic** | [Mage], [Healer], [Enchanter], [Summoner] | `magic.*` |
| **Utility** | [Scout], [Innkeeper], [Courier], [Scribe] | `utility.*` |

### 4.3 Class Evolution

Classes evolve at milestone levels (10, 20, 30, 40, 50, 60) based on **tag affinity distribution** within the class's domain.

**Evolution is determined by tag weights:**

```yaml
[Warrior] at Level 10 evolves based on tag distribution:

[Blade Dancer]:
  condition: combat.melee.sword > 60% of combat.melee.*
             combat.defense.evasion > combat.defense.shield
  
[Shield Bearer]:
  condition: combat.defense.shield > 40% of combat.*
             combat.tactical.group > combat.tactical.solo
  
[Berserker]:
  condition: combat.melee.*.cleave high
             combat.defense.* < 20% of combat.*
             combat.tactical.solo > combat.tactical.group
  
[Mercenary]:
  condition: social.trade.* > 50
             combat.melee.* diverse (3+ weapon types)
             utility.service.mercenary > 25
```

**Example Evolution Paths for [Warrior]:**
```
[Warrior] Lv.10 →
  ├─ [Blade Dancer]    (sword-focused + evasion)
  ├─ [Shield Bearer]   (shield-focused + group tactics)
  ├─ [Berserker]       (offense-heavy + solo)
  └─ [Mercenary]       (weapon variety + trade tags)
```

### 4.4 Multi-Classing

Players may hold up to **3 active classes**. Additional classes beyond the first split incoming experience.

| Active Classes | XP Distribution |
|----------------|-----------------|
| 1 Class | 100% to primary |
| 2 Classes | 60% / 40% (player assigns) |
| 3 Classes | 50% / 30% / 20% (player assigns) |

**Tag-Based XP Routing:** Actions automatically contribute XP to classes whose tag requirements they match. A `craft.smithing.weapon.forge` action contributes to both [Blacksmith] and [Warrior] if both are held.

**Class Synergy Bonuses:** Certain tag overlaps between classes unlock unique hybrid skills.
- [Chef] + [Alchemist] → Shared `craft.*.infuse` tags → Unlocks [Culinary Potions]
- [Warrior] + [Merchant] → Shared `social.trade` + `combat` → Unlocks [Caravan Guard]
- [Miner] + [Blacksmith] → Shared `gather.mining.ore` + `craft.smithing` → Unlocks [Material Intuition]

---

## 5. The Skill System

### 5.1 Skill Acquisition

Skills are granted on level-up. The skill pool is **filtered by tag affinity**, then weighted by recent activity.

**Skill Tag Requirements:**
Each skill defines tag prerequisites that must be met for eligibility:

```yaml
[Power Strike]:
  class_requirement: [Warrior] or combat.melee >= 200
  tag_requirements:
    - combat.melee: 150
  tag_weights:                    # Higher affinity = higher offer chance
    - combat.melee.*.slash: 2x
    - combat.melee.*.cleave: 1.5x
  tags_granted:                   # Using this skill builds these tags
    - combat.melee.technique.power

[Vein Sense]:
  class_requirement: [Miner] or gather.mining >= 300
  tag_requirements:
    - gather.mining.ore: 200
  tag_weights:
    - gather.mining.ore.rare: 3x  # More likely if mining rare ores
  tags_granted:
    - gather.mining.detect
    
[Bulk Discount]:
  class_requirement: [Merchant] or social.trade >= 250
  tag_requirements:
    - social.trade.buy: 100
    - social.trade.negotiate: 50
  tag_weights:
    - social.trade.negotiate.bulk: 2x
  tags_granted:
    - social.trade.efficiency
```

**Skill Selection Algorithm:**
```
On Level Up:
  1. Build eligible skill pool:
     - Filter by class requirements (if any)
     - Filter by minimum tag requirements
     
  2. Weight eligible skills:
     - Apply tag_weights based on player affinity
     - Apply recency bonus (last 50 actions)
     - Apply rarity roll modified by level
     
  3. Select skill(s):
     - Standard level: 1 skill (weighted random)
     - Milestone level: 2-3 choices offered to player
```

### 5.2 Skill Categories

**Passive Skills** — Always active, provide constant benefits
- [Green Thumb] — `gather.herbalism.*` +10% quality
- [Iron Skin] — `combat.defense.*` damage reduced by 3%
- [Merchant's Eye] — `social.trade.appraise` reveals hidden values

**Active Skills** — Triggered manually or via automation rules, have cooldowns
- [Power Strike] — `combat.melee.technique.power` 200% damage, 30s CD
- [Quick Brew] — `craft.alchemy.technique.instant` complete one potion, 10m CD
- [Recall] — `utility.exploration.travel.teleport` return to bind, 1h CD

**Conditional Skills** — Trigger automatically when conditions are met
- [Second Wind] — `combat.defense.recovery` restore 30% HP below 10%
- [Lucky Find] — `gather.*.technique.fortune` 5% double resources
- [Barter] — `social.trade.negotiate.auto` counter-offer on bad prices

### 4.3 Skill Rarity Tiers

| Tier | Bracket Color | Acquisition Rate | Power Level |
|------|---------------|------------------|-------------|
| Common | White | 70% | Minor utility |
| Uncommon | Green | 20% | Notable advantage |
| Rare | Blue | 7% | Build-defining |
| Epic | Purple | 2.5% | Class-defining |
| Legendary | Gold | 0.5% | World-notable |

### 4.4 Signature Skills

At milestone levels (10, 20, 30, etc.), players may receive a **Signature Skill** unique to their specific journey. These are procedurally named based on the player's history.

**Example:**
> Player "Thornwood" has primarily gathered nightshade, operated at night, and avoided combat.
> 
> **Signature Skill Granted:** [Thornwood's Midnight Harvest]
> *"Gathering at night yields +50% rare herbs. You are invisible to hostile creatures while gathering."*

---

## 6. Idle Mechanics

### 6.1 Activity Loops

Players configure **Activity Loops** that execute while offline or idle. Loops consist of prioritized action queues.

**Loop Builder Interface:**
```
ACTIVITY LOOP: "Herb Farm Route"
━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
1. Travel to Moonpetal Grove
2. Gather until inventory 80% full OR 2 hours elapsed
3. IF [Rare Herb] found → Store in vault
4. Travel to Market District
5. Sell common herbs
6. Return to Step 1

LOOP SETTINGS:
  ☑ Stop if HP < 30%
  ☑ Stop if equipment durability < 20%
  ☐ Allow combat encounters
```

### 6.2 Idle Efficiency

Pure repetitive idling has **diminishing returns** to prevent pure AFK grinding from outpacing engaged play.

**Diminishing Returns Curve:**
- Hours 1-4: 100% efficiency
- Hours 4-8: 75% efficiency  
- Hours 8-16: 50% efficiency
- Hours 16-24: 25% efficiency
- 24+ hours: 10% efficiency (maintenance mode)

**Resetting Efficiency:**
- Logging in and taking manual actions
- Changing activity loop
- Completing a significant milestone
- Social interaction (trade, party, guild activity)

### 6.3 Rest Cycles

Rest is mandatory for class/skill acquisition and provides bonuses.

**Rest Mechanics:**
- Characters must rest 10 minutes per 4 hours of activity
- Rest can be queued in activity loops
- Level-ups and class grants occur during rest
- Rested bonus: +25% XP for 1 hour after rest

---

## 7. Progression & Power Scaling

### 7.1 Level Ranges

| Level Range | Population % | Description |
|-------------|--------------|-------------|
| 1-10 | 60% | Beginners and casual players |
| 11-25 | 25% | Dedicated players, competent professionals |
| 26-40 | 10% | Veterans, server-notable figures |
| 41-50 | 4% | Elite, regional influence |
| 51-60 | 0.9% | Legendary, world events |
| 61+ | 0.1% | Mythic, permanent server impact |

### 7.2 XP Curve Philosophy

Early levels come quickly to hook players. Mid-levels require genuine engagement. High levels demand exceptional dedication or achievement.

**XP Required Formula:**
```
Base XP = 100 × (Level ^ 1.5)
Milestone Modifier = ×2 at levels 10, 20, 30, 40, 50, 60
Achievement Shortcuts = Major accomplishments grant level progress directly
```

### 7.3 Catch-Up Mechanics

To prevent insurmountable gaps in a living MMO:
- **Mentorship XP:** Lower-level players in parties with veterans gain bonus XP
- **Era Skills:** New players gain access to skills that help them engage with current content
- **Economic Mobility:** Crafting/gathering remains valuable regardless of combat level

---

## 8. Social Systems

### 8.1 Reputation & Titles

Player actions generate **Reputation** with factions, settlements, and the general populace. High reputation unlocks titles that function as pseudo-classes.

**Example Titles:**
- [Friend of Millbrook] — Discounts in Millbrook, access to village quests
- [The Generous] — Earned by significant charitable actions, +social skill efficacy
- [Oathbreaker] — Earned by betraying contracts, restricted from certain classes

### 8.2 Red Classes

Certain actions grant **Red Classes** that carry social consequences.

| Red Class | Acquisition | Consequence |
|-----------|-------------|-------------|
| [Thief] | Stealing from players/NPCs | Visible to [Merchant] class, banned from guilds |
| [Murderer] | PKing non-hostile players | Bounty system activation, NPC hostility |
| [Swindler] | Fraudulent trades | Trade restrictions, reputation damage |

Red classes can be shed through redemption quests, but the path is long.

### 8.3 Guild & Settlement Systems

Players can form **Guilds** that function as progression multipliers and unlock settlement-building content at high collective levels.

**Guild Perks:**
- Shared activity loops (guild mining operations, etc.)
- Guild-exclusive class evolutions (e.g., [Guild Artisan])
- Territory control and passive resource generation
- Guild achievements grant all members cosmetic titles

---

## 9. Monetization Philosophy

**Core Principle:** Time can be bought, but power and identity cannot.

| Allowed | Prohibited |
|---------|------------|
| Cosmetic class brackets (colored names) | Exclusive classes |
| Activity loop slots | XP multipliers beyond cosmetic thresholds |
| Inventory/vault expansion | Skills or skill rerolls |
| Cosmetic equipment skins | Stat advantages |
| Rest cycle skips (limited) | Bypassing red class consequences |
| Name changes | Reputation purchases |

---

## 10. Technical Considerations

### 10.1 Idle Simulation

- Server-side tick rate: 1 simulation tick per 5 minutes for offline players
- Real-time tick rate: 1 tick per 5 seconds for online players
- Catch-up calculation on login: Compressed simulation with randomized variance

### 10.2 Anti-Exploitation

- Activity pattern analysis to detect botting
- Diminishing returns prevent pure AFK optimization
- Economic sinks (repair costs, taxes, consumables) maintain balance
- Skill acquisition randomness prevents "solved" optimal paths

### 10.3 Data Architecture

```
Player State:
  ├─ Identity (name, appearance, titles)
  ├─ Classes[] (max 3 active + dormant list)
  ├─ Skills[] (categorized by class and type)
  ├─ Inventory & Equipment
  ├─ Tag Affinity Map (tag → score, with timestamps)
  ├─ Activity History (rolling 1000 actions with tags)
  ├─ Reputation Map (faction → value)
  ├─ Known Recipes[] (with tag prerequisites met)
  ├─ Known Spells[] (with tag prerequisites met)
  ├─ Current Activity Loop
  └─ Offline Simulation Queue

Tag Registry (Server-side):
  ├─ Tag Hierarchy Definition
  ├─ Class → Tag Requirements mapping
  ├─ Skill → Tag Requirements mapping
  ├─ Recipe → Tag Requirements mapping
  ├─ Spell → Tag Requirements mapping
  └─ Action → Tags Generated mapping
```

---

## 11. Sample Moment-to-Moment Experience

### New Player First Hour

1. **Spawn** in starting village. Brief tutorial explains actions = identity.
2. **Explore** freely. NPC dialogues hint at possible paths without directing.
3. **Experiment** with gathering, combat, crafting, or social interaction.
4. **First Rest** after ~45 minutes of mixed activity.
5. **Class Granted** based on dominant activity pattern.
6. **First Skill** received — immediate tangible benefit.
7. **Hook:** "What would I have gotten if I'd done something different?"

### Veteran Player Session

1. **Login** — Review overnight idle results. 847 iron ore gathered, 2 levels gained.
2. **Check Skills** — New passive [Vein Sense] acquired. Adjust activity loop to prioritize detected veins.
3. **Market Check** — Iron prices down. Switch loop to copper for better margins.
4. **Guild Coordination** — Guild needs flux for group project. Adjust contribution.
5. **Set New Loop** — 6-hour copper route with flux detour.
6. **Manual Play** — 20 minutes of dungeon delving for combat XP variety.
7. **Logout** — Character continues journey autonomously.

---

## Appendix A: Sample Class Evolution Trees

```
[Warrior]
  └─ Lv.10 → [Blade Dancer] / [Shield Bearer] / [Berserker] / [Mercenary]
      └─ Lv.20 → [Sword Saint] / [Bulwark] / [Blood Rager] / [Blade for Hire]
          └─ Lv.30 → [UNIQUE based on journey]
              └─ Lv.40+ → Further specialization / Prestige paths

[Herbalist]  
  └─ Lv.10 → [Apothecary] / [Poison Expert] / [Garden Tender] / [Forager]
      └─ Lv.20 → [Master Alchemist] / [Toxicologist] / [Greenhouse Keeper] / [Wilderness Sage]
          └─ Lv.30 → [UNIQUE based on journey]
              └─ Lv.40+ → Further specialization / Prestige paths

[Merchant]
  └─ Lv.10 → [Shopkeeper] / [Trader] / [Broker] / [Smuggler]
      └─ Lv.20 → [Magnate] / [Caravan Master] / [Information Dealer] / [Black Marketeer]
          └─ Lv.30 → [UNIQUE based on journey]
              └─ Lv.40+ → Further specialization / Prestige paths
```

---

## Appendix B: Skill Example Library

| Skill | Class | Type | Tags | Effect |
|-------|-------|------|------|--------|
| [Power Strike] | [Warrior] | Active | `combat.melee.technique.power` | 200% damage, 30s CD |
| [Parry] | [Duelist] | Conditional | `combat.melee.sword.parry` | Negate next attack if timed |
| [Bulk Discount] | [Merchant] | Passive | `social.trade.negotiate.bulk` | -15% purchase prices |
| [Green Thumb] | [Herbalist] | Passive | `gather.herbalism.quality` | +10% herb quality |
| [Flash Forge] | [Blacksmith] | Active | `craft.smithing.technique.instant` | Instant craft, 1h CD |
| [Soothing Words] | [Diplomat] | Active | `social.diplomacy.persuade` | De-escalate hostile NPCs |
| [Lucky Strike] | Any | Conditional | `gather.*.technique.fortune` | 5% chance critical gather |
| [Traveler's Endurance] | [Courier] | Passive | `utility.exploration.travel.speed` | +30% movement speed |
| [Mana Efficiency] | [Mage] | Passive | `magic.arcane.efficiency` | -20% mana costs |
| [Chef's Special] | [Chef] | Active | `craft.cooking.technique.enhance` | Next meal grants 2x buff |

---

## Appendix C: Tag-Based Progression Examples

**Example: Pure Combat Player**
```
After 20 hours of sword-focused gameplay:

Tag Affinity:
  combat.melee: 1,847
  combat.melee.sword: 1,523
  combat.melee.sword.slash: 892
  combat.melee.sword.parry: 631
  combat.defense.evasion: 445
  combat.tactical.solo: 312

Result: [Warrior] → [Blade Dancer] evolution at Lv.10
Skill Pool: Heavily weighted toward sword techniques, evasion skills
```

**Example: Hybrid Crafter-Gatherer**
```
After 20 hours of mixed mining and smithing:

Tag Affinity:
  gather.mining.ore: 1,245
  gather.mining.ore.iron: 876
  gather.mining.ore.copper: 369
  craft.smithing.weapon: 534
  craft.smithing.tool: 289
  social.trade.sell: 156

Result: Qualifies for both [Miner] and [Blacksmith]
        [Material Intuition] synergy skill unlocked
Skill Pool: Mix of gathering efficiency and crafting quality skills
```

---

*Document Version 1.1 — Tag Taxonomy System Added*
