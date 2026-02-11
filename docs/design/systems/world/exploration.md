---
title: Exploration
gdd_ref: systems/exploration-system-gdd.md#exploration
---

<!-- ADAPTATION REQUIRED -->
<!-- This file was migrated from source but needs manual review: -->

<!) -->

<!-- - Align with current GDD architecture -->
<!-- - Add missing sections as needed -->
<!-- - Update frontmatter with correct gdd_ref -->

---

title: Exploration and Discovery System
type: system
summary: Discovery mechanics, mapping, navigation, dungeons, and secrets

---

# Exploration & Discovery System

## 1. Overview

Exploration is a core gameplay loop that rewards curiosity and risk-taking. Players discover new areas, resources, secrets, and opportunities through active exploration. The Explorer class and related skills enhance but don't gate exploration—anyone can explore, specialists do it better.

| Aspect     | Description                                  |
| ---------- | -------------------------------------------- |
| Discovery  | Finding new locations, resources, secrets    |
| Mapping    | Revealing and recording the world            |
| Navigation | Finding paths, avoiding hazards              |
| Rewards    | XP, resources, unique items, classes         |
| Risk       | Danger scales with distance from settlements |

---

## 2. World Structure

### Area Hierarchy

```
World
├── Regions (climate zones)
│ ├── Zones (distinct areas)
│ │ ├── Locations (specific places)
│ │ │ ├── Points of Interest (discoverable)
│ │ │ └── Sub-locations (interiors, dungeons)
```

### Area Types

| Type              | Size     | Examples                            |
| ----------------- | -------- | ----------------------------------- |
| Region            | Huge     | The Frozen North, Desert Wastes     |
| Zone              | Large    | Darkwood Forest, Iron Mountains     |
| Location          | Medium   | Abandoned Mine, Ruined Tower        |
| Point of Interest | Small    | Hidden cave, ancient tree, campsite |
| Sub-location      | Variable | Dungeon floors, building interiors  |

### Area States

| State      | Visibility      | Information                   |
| ---------- | --------------- | ----------------------------- |
| Unknown    | Not on map      | Nothing known                 |
| Rumored    | Marked vaguely  | Name, general direction       |
| Discovered | Marked on map   | Location, basic info          |
| Explored   | Detailed on map | Full layout, known contents   |
| Mapped     | Complete        | All secrets, resources marked |

---

## 3. Discovery Mechanics

### Discovery Triggers

| Trigger          | Description                             |
| ---------------- | --------------------------------------- |
| Line of sight    | Walk near enough to see                 |
| Skill detection  | Awareness/Explorer skills reveal hidden |
| NPC information  | Told about location                     |
| Map/document     | Found map reveals area                  |
| Quest            | Mission leads to location               |
| Random encounter | Stumble upon while traveling            |

### Discovery Radius

| Factor           | Effect                                 |
| ---------------- | -------------------------------------- |
| Base awareness   | Default detection range                |
| Explorer class   | Increased radius                       |
| Skills           | Further increase                       |
| Terrain          | Dense forest reduces, plains increases |
| Weather          | Fog, storms reduce                     |
| Time of day      | Night reduces                          |
| Height advantage | Elevation increases                    |

### Hidden Locations

| Concealment       | Discovery Method                 |
| ----------------- | -------------------------------- |
| Camouflaged       | High awareness, close proximity  |
| Underground       | Entrance discovery, mining       |
| Magical           | Magic detection, dispel          |
| Behind obstacle   | Climbing, swimming, flying       |
| Requires key/item | Possess specific item            |
| Time-locked       | Only accessible at certain times |
| Event-triggered   | Specific action unlocks          |

---

## 4. Points of Interest

### POI Categories

| Category | Examples                           | Typical Rewards     |
| -------- | ---------------------------------- | ------------------- |
| Resource | Ore vein, herb patch, fishing spot | Gathering materials |
| Shelter  | Cave, ruins, campsite              | Rest location       |
| Danger   | Monster lair, bandit camp, hazard  | Combat, loot        |
| Treasure | Hidden cache, ancient tomb         | Items, currency     |
| Lore     | Monument, library, shrine          | Knowledge, skills   |
| Social   | Hermit, wanderer, hidden village   | Quests, trade       |
| Passage  | Hidden path, tunnel, portal        | Shortcuts, access   |
| Unique   | One-of-a-kind locations            | Special rewards     |

### POI Persistence

| Type        | Behavior                                  |
| ----------- | ----------------------------------------- |
| Permanent   | Always present (landmarks, settlements)   |
| Renewable   | Respawns after depletion (resource nodes) |
| One-time    | Gone after interaction (treasure, events) |
| Conditional | Appears under specific conditions         |
| Mobile      | Moves around (wandering NPCs, creatures)  |

### Resource Node Discovery

Detection is progressive, not a single check. Time spent in an area accumulates toward discovery.

**Detection Accumulation**

```
Enter area with hidden resource
 ↓
Detection counter starts at 0%
 ↓
Accumulates based on:
├── Time in range (passive)
├── Movement/activity (faster)
├── Active search (fastest)
└── Class/skill bonuses (multiplier)
 ↓
Reaches 100% → Node revealed
```

**Accumulation Rates**

| Activity                          | Base Rate      | Notes              |
| --------------------------------- | -------------- | ------------------ |
| Standing still in range           | 1% per minute  | Minimal            |
| Walking through area              | 3% per minute  | Normal exploration |
| Active searching                  | 10% per minute | Dedicated action   |
| Working nearby (mining, foraging) | 5% per minute  | Related activity   |

**Class Detection Bonuses**

| Class      | Resource Type          | Multiplier |
| ---------- | ---------------------- | ---------- |
| Miner      | Ore, gems, stone       | 3x         |
| Forager    | Herbs, berries, plants | 3x         |
| Hunter     | Animal dens, tracks    | 3x         |
| Fisher     | Fish spawns, shellfish | 3x         |
| Lumberjack | Quality timber         | 2x         |
| Explorer   | All types              | 1.5x       |

**Skill Modifiers**

| Skill Level | Multiplier | Instant Detection  |
| ----------- | ---------- | ------------------ |
| None        | 1x         | —                  |
| Apprentice  | 1.5x       | Poorly hidden only |
| Journeyman  | 2x         | Common hidden      |
| Master      | 3x         | Most hidden        |
| Legendary   | 5x         | All but magical    |

**Example: Hidden Ore Seam**

| Character            | Time to Discover (walking) |
| -------------------- | -------------------------- |
| Unskilled            | ~33 minutes                |
| Apprentice Miner     | ~7 minutes                 |
| Journeyman Miner     | ~5 minutes                 |
| Master Miner         | ~3 minutes                 |
| Explorer (no mining) | ~22 minutes                |

**Class Unlock Triggers**

Discovering resources can trigger class unlocks:

| Discovery                      | Potential Class Trigger    |
| ------------------------------ | -------------------------- |
| Ore seam (while mining nearby) | Miner progress             |
| Herb patch (while foraging)    | Herbalist/Forager progress |
| Animal den (while hunting)     | Hunter/Trapper progress    |
| Hidden path                    | Explorer progress          |
| Ancient ruins                  | Scholar progress           |

**Instant Detection**

High-level specialists may instantly detect resources in their field:

| Condition                     | Result                        |
| ----------------------------- | ----------------------------- |
| Master+ in relevant class     | Auto-reveal on entering range |
| "Keen Sense" skill (Greater+) | Auto-reveal common nodes      |
| Active "Survey" skill         | Reveal all in zone (cooldown) |

---

## 5. Mapping System

### Map Types

| Map          | Source                   | Accuracy      | Coverage                 |
| ------------ | ------------------------ | ------------- | ------------------------ |
| Mental       | Auto-generated           | Player memory | Visited areas            |
| Drawn        | Player creates           | Variable      | What player records      |
| Purchased    | Merchants, guilds        | Variable      | Specific regions         |
| Found        | Loot, rewards            | Variable      | Often treasure locations |
| NPC-provided | Quest givers, informants | Variable      | Relevant areas           |

### Map Information Layers

| Layer       | Shows                              |
| ----------- | ---------------------------------- |
| Terrain     | Geography, elevation, water        |
| Roads       | Paths, roads, bridges              |
| Settlements | Towns, villages, camps             |
| Resources   | Known gathering nodes              |
| Dangers     | Known hazards, monster territories |
| POIs        | Discovered points of interest      |
| Personal    | Player notes, markers              |

### Map Accuracy

| Quality  | Description                   |
| -------- | ----------------------------- |
| Rough    | General area, possibly wrong  |
| Basic    | Correct location, few details |
| Good     | Accurate, some details        |
| Detailed | Full layout, most features    |
| Perfect  | Complete, all secrets         |

Purchased/found maps may be outdated, incomplete, or deliberately false.

---

## 6. Navigation

### Travel Modes

| Mode    | Speed     | Stamina   | Discovery                 |
| ------- | --------- | --------- | ------------------------- |
| Careful | Slow      | Low       | High (bonus to detection) |
| Normal  | Standard  | Standard  | Standard                  |
| Fast    | Quick     | High      | Low (may miss things)     |
| Sprint  | Very fast | Very high | Minimal                   |

### Navigation Skills

| Skill           | Effect                                    |
| --------------- | ----------------------------------------- |
| Pathfinding     | Reduce terrain movement penalties         |
| Orienteering    | Always know direction, reduce lost chance |
| Trailblazing    | Create paths through difficult terrain    |
| Landmark Memory | Auto-mark significant locations           |
| Shortcut Sense  | Detect hidden passages, faster routes     |

### Getting Lost

| Cause             | Effect                  |
| ----------------- | ----------------------- |
| No landmarks      | Gradual direction drift |
| Bad weather       | Can't see, wrong turns  |
| Dense terrain     | Disorientation          |
| Magical confusion | Active misdirection     |
| Night travel      | Reduced visibility      |

### Lost Recovery

| Method           | Requirement                 |
| ---------------- | --------------------------- |
| Find landmark    | Recognize known location    |
| Climb for view   | Elevation, clear weather    |
| Use compass      | Have compass item           |
| Track back       | Following own trail         |
| Navigation skill | Orienteering check          |
| NPC help         | Find someone who knows area |

---

## 7. Exploration Classes

### Explorer Class

| Aspect     | Detail                               |
| ---------- | ------------------------------------ |
| Unlock     | Discover 10+ new locations           |
| Focus      | Discovery, navigation, mapping       |
| XP sources | Finding new places, mapping, guiding |

### Explorer Skills

| Skill        | Tier     | Effect                                     |
| ------------ | -------- | ------------------------------------------ |
| Keen Sight   | Lesser   | +15% discovery radius                      |
| Keen Sight   | Greater  | +30% radius, spot hidden                   |
| Keen Sight   | Enhanced | +50% radius, spot magical concealment      |
| Survey       | Active   | Reveal all POIs in current zone (cooldown) |
| Trailblazer  | Passive  | Create paths, others can follow            |
| Cartographer | Passive  | Maps auto-update with accuracy             |
| First Footer | Passive  | Bonus XP/loot for first discoveries        |
| Danger Sense | Passive  | Warning before entering high-danger areas  |

### Related Classes

| Class       | Exploration Benefit                          |
| ----------- | -------------------------------------------- |
| Ranger      | Wilderness tracking, reduced terrain penalty |
| Hunter      | Animal tracking, territory knowledge         |
| Survivalist | Hazard mitigation, resource finding          |
| Scholar     | Lore interpretation, ancient language        |
| Adventurer  | Combat readiness, dungeon navigation         |

### Consolidations

| Classes               | Result        | Special Ability                             |
| --------------------- | ------------- | ------------------------------------------- |
| Explorer + Adventurer | Pathfinder    | "Uncharted" — bonus in never-explored areas |
| Explorer + Scholar    | Archaeologist | "Ancient Secrets" — find hidden lore        |
| Explorer + Hunter     | Wayfinder     | "Natural Guide" — party movement bonus      |
| Explorer + Trader     | Prospector    | "Trade Routes" — find valuable resources    |

---

## 8. First Discovery Rewards

### Discovery Bonuses

| Discovery Type   | Reward                    |
| ---------------- | ------------------------- |
| New zone         | Large XP, map update      |
| New location     | Medium XP                 |
| New POI          | Small XP                  |
| Hidden location  | Bonus XP                  |
| Unique/legendary | Large XP + special reward |

### First Discovery Claim

```
Player discovers new location
 ↓
Location flagged as "discovered"
 ↓
First discoverer recorded
 ↓
Bonus rewards granted:
├── XP bonus (scales with rarity)
├── Possible unique loot
├── Naming rights (some locations)
└── Reputation boost
```

### Shared Discovery

Party members present share discovery credit but at reduced individual bonus.

---

## 9. Dungeons & Interiors

### Dungeon Types

| Type         | Characteristics                          |
| ------------ | ---------------------------------------- |
| Natural cave | Winding, uneven, natural hazards         |
| Mine         | Structured, resource-rich, collapse risk |
| Ruins        | Ancient, traps, lore, undead             |
| Fortress     | Defended, strategic, faction-held        |
| Tomb         | Trapped, undead, treasure                |
| Lair         | Monster home, organic layout             |
| Magical      | Reality-warping, puzzles, unique hazards |

### Dungeon Features

| Feature    | Function                           |
| ---------- | ---------------------------------- |
| Entrance   | Access point, may be hidden        |
| Rooms      | Distinct areas with contents       |
| Corridors  | Connecting passages                |
| Traps      | Hazards requiring detection/disarm |
| Puzzles    | Mental challenges for progress     |
| Shortcuts  | Hidden passages, faster routes     |
| Safe rooms | Rest points within dungeon         |
| Boss room  | Final challenge, major rewards     |

### Dungeon Progression

| Element      | Behavior                                      |
| ------------ | --------------------------------------------- |
| Difficulty   | Increases deeper in                           |
| Loot quality | Better further in                             |
| Respawn      | Varies (some clear permanently, some respawn) |
| Shortcuts    | Unlock for faster return                      |
| Checkpoints  | Save progress (some dungeons)                 |

### Trap System

| Trap Type      | Detection           | Effect                     |
| -------------- | ------------------- | -------------------------- |
| Pressure plate | Visual, awareness   | Varies (darts, pit, alarm) |
| Tripwire       | Visual, awareness   | Blade, alarm, collapse     |
| Magical        | Magic detection     | Spell effect               |
| Environmental  | Logic, clues        | Falling rocks, flooding    |
| Creature       | Awareness, tracking | Ambush                     |

| Detection Method | Requirement                   |
| ---------------- | ----------------------------- |
| Visual           | Looking carefully, good light |
| Awareness skill  | Passive detection chance      |
| Search action    | Active, slower, higher chance |
| Thief skills     | Specialized detection         |
| Magic            | Detect traps spell            |

---

## 10. Secrets & Hidden Content

### Secret Types

| Type          | Concealment            | Reward                  |
| ------------- | ---------------------- | ----------------------- |
| Hidden door   | Camouflaged, mechanism | Shortcut, treasure room |
| Secret cache  | Buried, concealed      | Items, currency         |
| Lore fragment | Obscured, coded        | Knowledge, skill unlock |
| Hidden NPC    | Disguised, reclusive   | Quests, rare trades     |
| Easter egg    | Obscure trigger        | Unique items, humor     |

### Secret Discovery Methods

| Method              | Examples                            |
| ------------------- | ----------------------------------- |
| Thorough search     | Check everything, slow              |
| Skill detection     | Awareness, thief skills             |
| Environmental clues | Odd wall, draft, sound              |
| Lore hints          | Documents mention secret            |
| NPC tips            | Someone tells you                   |
| Experimentation     | Try unusual actions                 |
| Accident            | Random chance during other activity |

### Secret Rarity

| Rarity    | Frequency               | Reward Quality |
| --------- | ----------------------- | -------------- |
| Common    | 1 per location          | Minor          |
| Uncommon  | 1 per several locations | Moderate       |
| Rare      | 1 per zone              | Good           |
| Epic      | 1 per region            | Excellent      |
| Legendary | Few in world            | Unique         |

---

## 11. Travel & Routes

### Route Types

| Route       | Safety       | Speed    | Discovery        |
| ----------- | ------------ | -------- | ---------------- |
| Main road   | High         | Fast     | Low (well-known) |
| Side road   | Moderate     | Normal   | Moderate         |
| Trail       | Low-Moderate | Slower   | Higher           |
| Wilderness  | Low          | Slow     | Highest          |
| Hidden path | Variable     | Variable | Shortcut benefit |

### Route Discovery

| Method          | Result                         |
| --------------- | ------------------------------ |
| Follow roads    | Find connected settlements     |
| Ask NPCs        | Learn about paths, dangers     |
| Explorer skills | Detect hidden routes           |
| Trial and error | Find paths through exploration |
| Maps            | Show known routes              |

### Fast Travel

| Type            | Requirement             | Cost                         |
| --------------- | ----------------------- | ---------------------------- |
| None            | Default                 | Must walk everywhere         |
| Known locations | Discovered + safe route | Time passes, events possible |
| Caravan         | Pay for transport       | Gold, fixed routes           |
| Mount           | Own/rent mount          | Faster, stamina to mount     |
| Magic           | Teleport spell/item     | Rare, expensive              |

---

## 12. Exploration Events

### Random Encounters

| Type          | Examples                       |
| ------------- | ------------------------------ |
| Combat        | Bandits, monsters, wildlife    |
| Social        | Travelers, merchants, refugees |
| Discovery     | Stumble on hidden location     |
| Environmental | Weather change, hazard         |
| Opportunity   | Rare resource, abandoned goods |

### Encounter Frequency

| Factor              | Effect                                  |
| ------------------- | --------------------------------------- |
| Region danger level | Higher = more encounters                |
| Travel mode         | Fast travel = fewer                     |
| Party size          | Larger = different encounters           |
| Time of day         | Night = more dangerous                  |
| Weather             | Storms = fewer travelers                |
| Road vs wilderness  | Roads = more social, wild = more combat |

### Special Events

| Event              | Trigger                      | Outcome                   |
| ------------------ | ---------------------------- | ------------------------- |
| Caravan attack     | Hear combat while traveling  | Intervene or ignore       |
| Lost traveler      | Random encounter             | Help, ignore, rob         |
| Hidden cache       | High awareness + luck        | Free loot                 |
| Ambush             | Low awareness in danger zone | Combat at disadvantage    |
| Weather shift      | Time/random                  | Environmental change      |
| Creature migration | Seasonal/random              | Unusual creatures present |

---

## 13. Information Gathering

### Sources

| Source            | Information Type         | Reliability             |
| ----------------- | ------------------------ | ----------------------- |
| NPCs (general)    | Rumors, directions       | Variable                |
| NPCs (specialist) | Specific knowledge       | High                    |
| Documents         | Lore, locations, secrets | Depends on age          |
| Maps              | Geography, routes        | Depends on quality      |
| Signs/markers     | Warnings, directions     | Usually accurate        |
| Environment       | Clues, tracks, signs     | Requires interpretation |

### Asking for Directions

```
Approach NPC
 ↓
Ask about location/area
 ↓
NPC knowledge check:
├── Knows well → Accurate directions, details
├── Knows somewhat → General direction, some info
├── Doesn't know → No help or wrong info
└── Knows but won't tell → Requires persuasion/payment
 ↓
Information added to journal/map
```

### Information Quality

| Factor             | Effect                       |
| ------------------ | ---------------------------- |
| NPC expertise      | Local knows local area       |
| NPC honesty        | May lie or mislead           |
| Information age    | Old info may be outdated     |
| Your reputation    | Affects willingness to share |
| Payment/persuasion | Better info for price        |

---

## 14. Exploration Rewards

### Direct Rewards

| Reward    | Source                         |
| --------- | ------------------------------ |
| XP        | Discovery, mapping, navigation |
| Items     | Treasure, hidden caches        |
| Resources | Found nodes, rare materials    |
| Currency  | Hidden treasure, ancient coins |
| Equipment | Abandoned gear, tomb loot      |

### Indirect Rewards

| Reward             | Benefit                      |
| ------------------ | ---------------------------- |
| Shortcuts          | Faster travel                |
| Safe routes        | Avoid dangers                |
| Resource knowledge | Efficient gathering          |
| Strategic info     | Military/political advantage |
| Quest access       | New opportunities            |
| Class unlocks      | Explorer, related classes    |
| Reputation         | Known as discoverer          |

### Naming Rights

| Discovery        | Naming             |
| ---------------- | ------------------ |
| Unnamed location | Player can name    |
| Resource node    | Player can name    |
| Hidden path      | Player can name    |
| Already named    | Keep existing name |

Named locations use player's name in world (NPCs may reference).

---

## 15. Integration Points

| System         | Integration                         |
| -------------- | ----------------------------------- |
| Classes        | Explorer class, related skills      |
| Environment    | Terrain, weather affect exploration |
| Combat         | Encounters, dungeon enemies         |
| Gathering      | Resource node discovery             |
| Factions       | Territory, information sharing      |
| NPC Daily Life | NPCs share information              |
| Quests         | Exploration objectives              |
| Economy        | Valuable discoveries, trade routes  |
| Settlements    | Finding new settlements             |
| Party          | Shared exploration benefits         |

---

## 16. Data Tables

### Discovery XP Rewards

| Discovery        | Base XP | First Bonus |
| ---------------- | ------- | ----------- |
| POI (common)     | 10      | +50%        |
| POI (uncommon)   | 25      | +50%        |
| Location         | 50      | +100%       |
| Hidden location  | 100     | +100%       |
| Zone             | 200     | +150%       |
| Unique/legendary | 500     | +200%       |

### Detection Radius by Skill

| Skill Level | Base Radius | Hidden Detection |
| ----------- | ----------- | ---------------- |
| None        | 50m         | 10% chance       |
| Apprentice  | 75m         | 25% chance       |
| Journeyman  | 100m        | 45% chance       |
| Master      | 150m        | 70% chance       |
| Legendary   | 200m        | 90% chance       |

### Dungeon Difficulty Scaling

| Depth    | Enemy Level | Trap Difficulty | Loot Quality   |
| -------- | ----------- | --------------- | -------------- |
| Entrance | Area level  | Easy            | Common         |
| Shallow  | +2-3        | Moderate        | Uncommon       |
| Mid      | +5-7        | Hard            | Rare           |
| Deep     | +10-15      | Very Hard       | Epic           |
| Boss     | +15-20      | Extreme         | Epic/Legendary |

### Travel Time Modifiers

| Factor            | Modifier      |
| ----------------- | ------------- |
| Road              | 0.8x time     |
| Trail             | 1.0x time     |
| Wilderness        | 1.5x time     |
| Difficult terrain | 2.0x time     |
| Mount             | 0.5x time     |
| Encumbered        | 1.3x time     |
| Explorer skills   | 0.8-0.9x time |
| Weather (bad)     | 1.2-1.5x time |
