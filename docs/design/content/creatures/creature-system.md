---
title: Mobs and Creatures System
type: creature
summary: Creature classification, behavior, harvestables, taming, bosses, and spawning
---

# Mobs & Creatures System

## 1. Overview

Creatures in the world follow consistent rules based on their intelligence, magic capability, and social behavior. All creatures have harvestable components used in crafting, alchemy, and trade.

### Classification Axes

| Axis         | Description                                              | Gameplay Effect          |
| ------------ | -------------------------------------------------------- | ------------------------ |
| Intelligence | Beast → Cunning → Sapient                                | Tactics, skill access    |
| Magic Type   | None / Innate / Mana / Tribal                            | Spell access             |
| Social       | Independent / Pack / Herd / Swarm                        | Encounter composition    |
| Aggression   | Passive / Defensive / Territorial / Aggressive / Hostile | Attack triggers          |
| Rarity       | Common / Uncommon / Rare / Legendary                     | Spawn rate, loot quality |

### Aggression Ratings

| Rating      | Behavior                                         |
| ----------- | ------------------------------------------------ |
| Passive     | Never attacks, flees when threatened             |
| Defensive   | Only attacks if attacked or cornered             |
| Territorial | Attacks if you enter territory or approach young |
| Aggressive  | Attacks on sight if hungry or threatened         |
| Hostile     | Always attacks on sight                          |

### Social Behavior

| Type        | Group Size   | Behavior                            |
| ----------- | ------------ | ----------------------------------- |
| Independent | 1 (rarely 2) | Acts alone, no coordination         |
| Pack        | 3-8          | Coordinated tactics, alpha leads    |
| Herd        | 5-20+        | Safety in numbers, stampede flee    |
| Swarm       | 10-50+       | Overwhelming numbers, hive behavior |

---

## 2. Common Wildlife

### Mammals - Prey Animals

#### Deer

| Attribute    | Value          |
| ------------ | -------------- |
| Environment  | Forest, Plains |
| Social       | Herd (5-12)    |
| Aggression   | Passive        |
| Activity     | Crepuscular    |
| Intelligence | Beast          |
| Magic        | None           |

**Harvestable Parts**

| Part      | Quality Range   | Uses                                                     |
| --------- | --------------- | -------------------------------------------------------- |
| Venison   | Common-Fine     | Cooking (meat dishes)                                    |
| Deer Hide | Common-Quality  | Leatherworking (light armor, clothing)                   |
| Antlers   | Common-Superior | Alchemy (stamina potions), Crafting (tools, decorations) |
| Sinew     | Common-Quality  | Bowyer (bowstrings), Leatherworking                      |
| Deer Fat  | Common-Quality  | Alchemy (bases), Cooking (preservation)                  |
| Hooves    | Common          | Alchemy (ground for potions), Glue making                |

**Magical Variant: Moonlit Stag** (Rare)

| Attribute           | Value                                         |
| ------------------- | --------------------------------------------- |
| Rarity              | Rare                                          |
| Distinguishing      | Silver-white coat, glowing antlers at night   |
| Magic               | Innate (passive aura)                         |
| Additional Behavior | Only appears at night, drawn to magical areas |

_Additional Parts_

| Part          | Quality Range    | Uses                                                          |
| ------------- | ---------------- | ------------------------------------------------------------- |
| Lunar Antlers | Fine-Legendary   | Enchanting (moonlight effects), Alchemy (night vision, sleep) |
| Moonlit Hide  | Quality-Superior | Tailoring (magical light armor, invisibility components)      |
| Stag Heart    | Rare drop        | Alchemy (restoration potions, grace elixirs)                  |

---

#### Rabbit/Hare

| Attribute    | Value                                |
| ------------ | ------------------------------------ |
| Environment  | Forest, Plains, Hills                |
| Social       | Independent (burrow colonies nearby) |
| Aggression   | Passive                              |
| Activity     | Crepuscular                          |
| Intelligence | Beast                                |
| Magic        | None                                 |

**Harvestable Parts**

| Part        | Quality Range  | Uses                                                  |
| ----------- | -------------- | ----------------------------------------------------- |
| Rabbit Meat | Common-Quality | Cooking (stews, roasts)                               |
| Rabbit Fur  | Common-Quality | Tailoring (lining, trim), Alchemy (softening agents)  |
| Rabbit Foot | Common         | Alchemy (luck charms - minor effect)                  |
| Small Bones | Common         | Alchemy (calcium powder), Crafting (needles, buttons) |

**Magical Variant: Blink Hare** (Uncommon)

| Attribute           | Value                                              |
| ------------------- | -------------------------------------------------- |
| Rarity              | Uncommon                                           |
| Distinguishing      | Faint shimmer, seems to flicker                    |
| Magic               | Innate (short-range teleport)                      |
| Additional Behavior | Teleports when threatened, very difficult to catch |

_Additional Parts_

| Part              | Quality Range    | Uses                                                      |
| ----------------- | ---------------- | --------------------------------------------------------- |
| Blink Gland       | Quality-Fine     | Alchemy (teleportation potions), Enchanting (blink items) |
| Phasing Fur       | Quality-Superior | Tailoring (evasion bonuses), Alchemy                      |
| Displacement Foot | Uncommon drop    | Alchemy (dodge elixirs), Trinkets                         |

---

#### Wild Boar

| Attribute    | Value                                    |
| ------------ | ---------------------------------------- |
| Environment  | Forest, Swamp edges                      |
| Social       | Pack (sounder, 4-10)                     |
| Aggression   | Defensive (Territorial if young present) |
| Activity     | Diurnal                                  |
| Intelligence | Cunning                                  |
| Magic        | None                                     |

**Harvestable Parts**

| Part      | Quality Range   | Uses                                         |
| --------- | --------------- | -------------------------------------------- |
| Boar Meat | Common-Fine     | Cooking (rich dishes, sausages)              |
| Boar Hide | Common-Quality  | Leatherworking (tough leather, medium armor) |
| Tusks     | Common-Superior | Crafting (weapons, tools), Jewelry           |
| Boar Fat  | Common-Quality  | Cooking, Alchemy (bases), Candle making      |
| Bristles  | Common          | Brushes, Crafting                            |

**Magical Variant: Ironhide Boar** (Rare)

| Attribute      | Value                                 |
| -------------- | ------------------------------------- |
| Rarity         | Rare                                  |
| Distinguishing | Metallic sheen to hide, larger tusks  |
| Magic          | Innate (natural armor, charge attack) |
| Aggression     | Territorial                           |

_Additional Parts_

| Part             | Quality Range     | Uses                                                     |
| ---------------- | ----------------- | -------------------------------------------------------- |
| Ironhide Leather | Fine-Superior     | Leatherworking (exceptional armor)                       |
| Steel Tusks      | Quality-Legendary | Weaponsmithing (incorporated into weapons)               |
| Ironheart Core   | Rare drop         | Alchemy (fortification potions), Artificer (golem cores) |

---

### Mammals - Predators

#### Wolf

| Attribute    | Value                                 |
| ------------ | ------------------------------------- |
| Environment  | Forest, Tundra, Mountains             |
| Social       | Pack (5-12)                           |
| Aggression   | Aggressive (when hungry), Territorial |
| Activity     | Crepuscular/Nocturnal                 |
| Intelligence | Cunning                               |
| Magic        | None                                  |

**Harvestable Parts**

| Part       | Quality Range  | Uses                                           |
| ---------- | -------------- | ---------------------------------------------- |
| Wolf Meat  | Common         | Cooking (survival food, pet food)              |
| Wolf Pelt  | Common-Fine    | Leatherworking (fur armor, cloaks)             |
| Wolf Fangs | Common-Quality | Alchemy (aggression potions), Jewelry, Weapons |
| Wolf Claws | Common-Quality | Alchemy, Crafting (tools)                      |
| Wolf Heart | Quality        | Alchemy (courage potions, pack bonding)        |
| Wolf Eyes  | Common         | Alchemy (night vision)                         |

**Magical Variant: Dire Wolf** (Uncommon)

| Attribute      | Value                                 |
| -------------- | ------------------------------------- |
| Rarity         | Uncommon                              |
| Distinguishing | Larger size (horse-sized), darker fur |
| Magic          | None (but enhanced physical)          |
| Pack Role      | Alpha of large packs, or independent  |

_Additional Parts_

| Part           | Quality Range    | Uses                                       |
| -------------- | ---------------- | ------------------------------------------ |
| Dire Wolf Pelt | Quality-Superior | Leatherworking (premium fur armor)         |
| Dire Fangs     | Quality-Fine     | Weaponsmithing, Alchemy (enhanced potency) |
| Alpha Heart    | Rare drop        | Alchemy (dominance elixirs, leadership)    |

**Magical Variant: Phantom Wolf** (Rare)

| Attribute      | Value                              |
| -------------- | ---------------------------------- |
| Rarity         | Rare                               |
| Distinguishing | Semi-transparent, leaves no tracks |
| Magic          | Innate (phasing, fear aura)        |
| Activity       | Nocturnal only                     |

_Additional Parts_

| Part            | Quality Range  | Uses                                        |
| --------------- | -------------- | ------------------------------------------- |
| Phantom Essence | Fine-Superior  | Alchemy (invisibility, phasing), Enchanting |
| Spectral Pelt   | Quality-Fine   | Tailoring (stealth cloaks)                  |
| Fear Gland      | Rare drop      | Alchemy (terror potions, intimidation)      |
| Ghost Fang      | Fine-Legendary | Enchanting (weapons that bypass armor)      |

---

#### Bear

| Attribute    | Value                                        |
| ------------ | -------------------------------------------- |
| Environment  | Forest, Mountains, Tundra                    |
| Social       | Independent (mother with cubs = Territorial) |
| Aggression   | Defensive (Territorial near den/cubs)        |
| Activity     | Diurnal (hibernates in winter regions)       |
| Intelligence | Cunning                                      |
| Magic        | None                                         |

**Harvestable Parts**

| Part       | Quality Range  | Uses                                          |
| ---------- | -------------- | --------------------------------------------- |
| Bear Meat  | Common-Quality | Cooking (rich, fatty meat)                    |
| Bear Pelt  | Common-Fine    | Leatherworking (heavy cloaks, bedding, armor) |
| Bear Claws | Common-Quality | Crafting (weapons, jewelry), Alchemy          |
| Bear Fat   | Common-Fine    | Alchemy (healing salves), Cooking, Lubricants |
| Bear Skull | Quality        | Decoration, Shamanic crafting (totems)        |
| Bear Heart | Quality        | Alchemy (strength, endurance potions)         |
| Bear Gall  | Quality-Fine   | Alchemy (medicinal, highly valued)            |

**Magical Variant: Stonebear** (Rare)

| Attribute      | Value                                 |
| -------------- | ------------------------------------- |
| Rarity         | Rare                                  |
| Environment    | Mountains, Caves                      |
| Distinguishing | Stone-like hide patches, glowing eyes |
| Magic          | Innate (earth magic, tremor stomp)    |
| Aggression     | Territorial                           |

_Additional Parts_

| Part          | Quality Range     | Uses                                              |
| ------------- | ----------------- | ------------------------------------------------- |
| Stone Hide    | Fine-Superior     | Armor (exceptional protection)                    |
| Earth Heart   | Rare drop         | Alchemy (earth resistance), Enchanting, Artificer |
| Crystal Claws | Quality-Legendary | Weaponsmithing, Mining tools                      |
| Tremor Gland  | Fine              | Alchemy (earthquake effects), Enchanting          |

**Magical Variant: Spirit Bear** (Legendary)

| Attribute      | Value                                            |
| -------------- | ------------------------------------------------ |
| Rarity         | Legendary                                        |
| Distinguishing | Pure white, ethereal glow, speaks telepathically |
| Magic          | Innate (healing aura, nature magic)              |
| Aggression     | Passive (attacks only corrupt creatures)         |
| Note           | Killing incurs nature faction penalty            |

_Additional Parts_

| Part              | Quality Range      | Uses                                        |
| ----------------- | ------------------ | ------------------------------------------- |
| Spirit Essence    | Legendary          | Alchemy (resurrection adjacent), Enchanting |
| Blessed Pelt      | Superior-Legendary | Tailoring (holy protection)                 |
| Heart of the Wild | Legendary          | Artificer (nature golems), Ultimate alchemy |

---

#### Mountain Lion / Cougar

| Attribute    | Value                        |
| ------------ | ---------------------------- |
| Environment  | Mountains, Forest, Desert    |
| Social       | Independent                  |
| Aggression   | Aggressive (ambush predator) |
| Activity     | Crepuscular                  |
| Intelligence | Cunning                      |
| Magic        | None                         |

**Harvestable Parts**

| Part       | Quality Range  | Uses                                |
| ---------- | -------------- | ----------------------------------- |
| Lion Meat  | Common         | Cooking                             |
| Tawny Pelt | Common-Quality | Leatherworking (supple leather)     |
| Fangs      | Common-Quality | Alchemy, Jewelry                    |
| Claws      | Common-Quality | Alchemy (agility potions), Crafting |
| Sinew      | Common-Fine    | Bowyer, Leatherworking              |
| Cat Eyes   | Common         | Alchemy (night vision, perception)  |

**Magical Variant: Shadowcat** (Rare)

| Attribute      | Value                              |
| -------------- | ---------------------------------- |
| Rarity         | Rare                               |
| Distinguishing | Black coat, merges with shadows    |
| Magic          | Innate (shadow step, silence aura) |
| Activity       | Nocturnal                          |

_Additional Parts_

| Part           | Quality Range | Uses                                                  |
| -------------- | ------------- | ----------------------------------------------------- |
| Shadow Pelt    | Fine-Superior | Tailoring (stealth gear, assassin equipment)          |
| Void Eyes      | Quality-Fine  | Alchemy (true darkness vision), Enchanting            |
| Silence Gland  | Rare drop     | Alchemy (silence potions), Enchanting (muffled armor) |
| Shadow Essence | Fine          | Enchanting (shadow magic items)                       |

---

### Reptiles

#### Snake (Venomous)

| Attribute    | Value                                                  |
| ------------ | ------------------------------------------------------ |
| Environment  | All (varies by species)                                |
| Social       | Independent                                            |
| Aggression   | Defensive                                              |
| Activity     | Varies (often diurnal in cool areas, nocturnal in hot) |
| Intelligence | Beast                                                  |
| Magic        | None                                                   |

**Harvestable Parts**

| Part       | Quality Range  | Uses                                       |
| ---------- | -------------- | ------------------------------------------ |
| Snake Meat | Common         | Cooking (specialty dish)                   |
| Snakeskin  | Common-Quality | Leatherworking (flexible leather, fashion) |
| Venom Sac  | Common-Fine    | Alchemy (poisons, antidotes)               |
| Fangs      | Common         | Alchemy, Crafting (needles, darts)         |
| Snake Eyes | Common         | Alchemy (perception potions)               |

**Magical Variant: Ashviper** (Uncommon)

| Attribute      | Value                                |
| -------------- | ------------------------------------ |
| Rarity         | Uncommon                             |
| Environment    | Volcanic, Desert                     |
| Distinguishing | Red-orange scales, heat shimmer      |
| Magic          | Innate (fire venom, heat resistance) |

_Additional Parts_

| Part         | Quality Range    | Uses                                       |
| ------------ | ---------------- | ------------------------------------------ |
| Fire Venom   | Quality-Fine     | Alchemy (burning poisons, fire resistance) |
| Ember Scales | Quality-Superior | Armor (fire protection), Alchemy           |
| Flame Fang   | Fine             | Enchanting (fire weapons)                  |

**Magical Variant: Basilisk** (Rare)

| Attribute      | Value                                                |
| -------------- | ---------------------------------------------------- |
| Rarity         | Rare                                                 |
| Environment    | Swamp, Caves                                         |
| Distinguishing | Eight legs, crown-like crest, petrifying gaze        |
| Magic          | Innate (petrification gaze - partial, poison breath) |
| Aggression     | Aggressive                                           |

_Additional Parts_

| Part           | Quality Range  | Uses                                                |
| -------------- | -------------- | --------------------------------------------------- |
| Basilisk Eye   | Fine-Legendary | Alchemy (petrification, flesh to stone), Enchanting |
| Stone Gland    | Quality-Fine   | Alchemy (stoneskin potions)                         |
| Basilisk Blood | Fine-Superior  | Alchemy (universal solvent, acid)                   |
| Crown Crest    | Fine           | Jewelry (intimidation), Shamanic crafting           |

---

#### Crocodile

| Attribute    | Value                                      |
| ------------ | ------------------------------------------ |
| Environment  | Swamp, Rivers, Coastal                     |
| Social       | Independent (congregation at food sources) |
| Aggression   | Aggressive (ambush predator)               |
| Activity     | Diurnal                                    |
| Intelligence | Cunning                                    |
| Magic        | None                                       |

**Harvestable Parts**

| Part           | Quality Range  | Uses                                          |
| -------------- | -------------- | --------------------------------------------- |
| Croc Meat      | Common-Quality | Cooking                                       |
| Crocodile Hide | Common-Fine    | Leatherworking (tough, water-resistant armor) |
| Teeth          | Common-Quality | Crafting, Jewelry, Alchemy                    |
| Stomach Stones | Common         | Alchemy (digestion aids)                      |
| Tail Meat      | Quality        | Cooking (delicacy)                            |

**Magical Variant: Deathroll Croc** (Uncommon)

| Attribute      | Value                                  |
| -------------- | -------------------------------------- |
| Rarity         | Uncommon                               |
| Distinguishing | Much larger, bony ridges glow faintly  |
| Magic          | Innate (enhanced grip, drowning curse) |

_Additional Parts_

| Part            | Quality Range | Uses                                                    |
| --------------- | ------------- | ------------------------------------------------------- |
| Drowning Gland  | Quality-Fine  | Alchemy (water breathing inverse - suffocation), Curses |
| Deathgrip Teeth | Fine-Superior | Weaponsmithing, Alchemy (binding effects)               |
| Primal Hide     | Fine-Superior | Exceptional water-resistant armor                       |

---

### Birds

#### Eagle / Hawk

| Attribute    | Value                            |
| ------------ | -------------------------------- |
| Environment  | Mountains, Plains, Coastal       |
| Social       | Independent (mated pairs)        |
| Aggression   | Defensive (aggressive near nest) |
| Activity     | Diurnal                          |
| Intelligence | Cunning                          |
| Magic        | None                             |

**Harvestable Parts**

| Part        | Quality Range  | Uses                                    |
| ----------- | -------------- | --------------------------------------- |
| Raptor Meat | Common         | Cooking (survival)                      |
| Feathers    | Common-Fine    | Fletching, Alchemy (flight), Decoration |
| Talons      | Common-Quality | Crafting (weapons, tools), Alchemy      |
| Beak        | Common         | Crafting                                |
| Eagle Eyes  | Quality        | Alchemy (perception, far-sight potions) |

**Magical Variant: Thunderhawk** (Rare)

| Attribute      | Value                                  |
| -------------- | -------------------------------------- |
| Rarity         | Rare                                   |
| Environment    | Mountains, Storm areas                 |
| Distinguishing | Blue-tinged feathers, sparks in flight |
| Magic          | Innate (lightning dive, storm calling) |

_Additional Parts_

| Part           | Quality Range     | Uses                                              |
| -------------- | ----------------- | ------------------------------------------------- |
| Storm Feathers | Fine-Superior     | Alchemy (lightning resistance, speed), Enchanting |
| Thunder Talons | Quality-Legendary | Weaponsmithing (shocking weapons)                 |
| Static Heart   | Rare drop         | Enchanting (storm magic), Artificer               |

---

#### Crow / Raven

| Attribute    | Value                             |
| ------------ | --------------------------------- |
| Environment  | All (prefer forests, settlements) |
| Social       | Pack (murder, 10-30)              |
| Aggression   | Passive (defensive if provoked)   |
| Activity     | Diurnal                           |
| Intelligence | Cunning (high for birds)          |
| Magic        | None                              |

**Harvestable Parts**

| Part          | Quality Range | Uses                                |
| ------------- | ------------- | ----------------------------------- |
| Crow Feathers | Common        | Fletching, Decoration, Alchemy      |
| Beak          | Common        | Crafting                            |
| Crow Eye      | Common        | Alchemy (dark vision, omen reading) |

**Magical Variant: Omen Raven** (Uncommon)

| Attribute      | Value                             |
| -------------- | --------------------------------- |
| Rarity         | Uncommon                          |
| Distinguishing | Three eyes, speaks in riddles     |
| Magic          | Innate (foresight, death sense)   |
| Behavior       | Appears before significant deaths |

_Additional Parts_

| Part          | Quality Range | Uses                                                  |
| ------------- | ------------- | ----------------------------------------------------- |
| Third Eye     | Quality-Fine  | Alchemy (divination potions), Enchanting              |
| Omen Feathers | Quality       | Alchemy (fate manipulation), Scribe (fortune scrolls) |
| Prophet Beak  | Rare drop     | Shamanic crafting (seeing totems)                     |

---

### Insects & Arachnids

#### Giant Spider

| Attribute    | Value                             |
| ------------ | --------------------------------- |
| Environment  | Forest, Caves, Ruins              |
| Social       | Independent (some swarm variants) |
| Aggression   | Aggressive                        |
| Activity     | Nocturnal                         |
| Intelligence | Beast (hive variants: Cunning)    |
| Magic        | None                              |

**Harvestable Parts**

| Part        | Quality Range  | Uses                                          |
| ----------- | -------------- | --------------------------------------------- |
| Spider Meat | Common         | Cooking (acquired taste), Pet food            |
| Spider Silk | Common-Fine    | Tailoring (lightweight strong fiber), Alchemy |
| Venom Sac   | Common-Quality | Alchemy (poisons, paralysis)                  |
| Fangs       | Common         | Alchemy, Crafting                             |
| Chitin      | Common-Quality | Armor (lightweight plates)                    |
| Spider Eyes | Common         | Alchemy (multi-vision)                        |

**Magical Variant: Phase Spider** (Rare)

| Attribute      | Value                                         |
| -------------- | --------------------------------------------- |
| Rarity         | Rare                                          |
| Distinguishing | Semi-transparent, flickers between dimensions |
| Magic          | Innate (phase shift, ethereal webbing)        |

_Additional Parts_

| Part           | Quality Range | Uses                                                |
| -------------- | ------------- | --------------------------------------------------- |
| Phase Silk     | Fine-Superior | Tailoring (dimensional anchoring, phasing cloaks)   |
| Ethereal Venom | Quality-Fine  | Alchemy (affects ethereal creatures, ghost poisons) |
| Phase Gland    | Rare drop     | Enchanting (blink items), Alchemy                   |

**Magical Variant: Broodmother** (Legendary)

| Attribute      | Value                                           |
| -------------- | ----------------------------------------------- |
| Rarity         | Legendary                                       |
| Social         | Swarm controller (50+ offspring)                |
| Distinguishing | Massive size, egg sac, psychic link to brood    |
| Magic          | Innate (poison spray, summon swarm, web domain) |

_Additional Parts_

| Part           | Quality Range      | Uses                                        |
| -------------- | ------------------ | ------------------------------------------- |
| Queen Silk     | Superior-Legendary | Tailoring (best spider silk items)          |
| Brood Venom    | Fine-Legendary     | Alchemy (master poisons)                    |
| Hive Mind Node | Legendary          | Enchanting (communication items), Artificer |

---

#### Giant Scorpion

| Attribute    | Value         |
| ------------ | ------------- |
| Environment  | Desert, Caves |
| Social       | Independent   |
| Aggression   | Territorial   |
| Activity     | Nocturnal     |
| Intelligence | Beast         |
| Magic        | None          |

**Harvestable Parts**

| Part          | Quality Range  | Uses                                  |
| ------------- | -------------- | ------------------------------------- |
| Scorpion Meat | Common         | Cooking                               |
| Chitin        | Common-Quality | Armor (plates)                        |
| Venom Sac     | Common-Fine    | Alchemy (paralytic poisons)           |
| Stinger       | Common-Quality | Weaponsmithing (dagger tips), Alchemy |
| Claws         | Common         | Crafting (tools)                      |

**Magical Variant: Crystal Scorpion** (Rare)

| Attribute      | Value                                |
| -------------- | ------------------------------------ |
| Rarity         | Rare                                 |
| Environment    | Desert, Crystal caves                |
| Distinguishing | Crystalline carapace, refracts light |
| Magic          | Innate (light beam, crystal armor)   |

_Additional Parts_

| Part             | Quality Range  | Uses                                   |
| ---------------- | -------------- | -------------------------------------- |
| Crystal Carapace | Fine-Superior  | Armor (beautiful, protective), Jewelry |
| Prismatic Venom  | Quality-Fine   | Alchemy (light-based effects)          |
| Light Stinger    | Fine-Legendary | Weaponsmithing (light damage weapons)  |

---

## 3. Monsters

### Elemental Creatures

#### Fire Elemental

| Attribute    | Value                                       |
| ------------ | ------------------------------------------- |
| Environment  | Volcanic, Desert (rare), Fire-touched areas |
| Social       | Independent                                 |
| Aggression   | Territorial                                 |
| Activity     | Always                                      |
| Intelligence | Cunning                                     |
| Magic        | Innate (fire magic)                         |

**Abilities (Mana-less)**

| Ability             | Effect                           |
| ------------------- | -------------------------------- |
| Burn                | Passive fire damage on contact   |
| Flame Jet           | Ranged fire attack               |
| Ignite              | Sets flammable materials on fire |
| Fire Immunity       | Cannot be damaged by fire        |
| Water Vulnerability | Extra damage from water          |

**Harvestable Parts**

| Part                    | Quality Range  | Uses                                             |
| ----------------------- | -------------- | ------------------------------------------------ |
| Fire Essence            | Quality-Fine   | Alchemy (fire potions), Enchanting               |
| Ember Core              | Fine-Superior  | Artificer (heating elements), Enchanting         |
| Living Flame (captured) | Fine           | Alchemy (preserving fire), Smithing (forge fuel) |
| Ash Residue             | Common-Quality | Alchemy (fire resistance)                        |

**Greater Variant: Inferno Elemental** (Rare)

| Attribute            | Value                                           |
| -------------------- | ----------------------------------------------- |
| Rarity               | Rare                                            |
| Distinguishing       | Larger, white-hot core, creates fire terrain    |
| Additional Abilities | Eruption (AoE), Heat Aura, Summon Lesser Flames |

_Additional Parts_

| Part                | Quality Range      | Uses                                       |
| ------------------- | ------------------ | ------------------------------------------ |
| Inferno Core        | Superior-Legendary | Major enchanting, Artificer                |
| White Flame Essence | Fine-Legendary     | Alchemy (superior fire), Forge enhancement |

---

#### Other Elementals (Summary)

| Type      | Environment           | Vulnerability           | Key Parts                                      |
| --------- | --------------------- | ----------------------- | ---------------------------------------------- |
| Water     | Coastal, Lakes, Swamp | Lightning, Cold         | Water Essence, Tide Core, Pure Water           |
| Earth     | Mountains, Caves      | Water (erosion)         | Earth Essence, Stone Core, Living Rock         |
| Air       | Mountains, Plains     | Fire (heated air rises) | Wind Essence, Storm Core, Captured Gust        |
| Ice       | Tundra, Mountains     | Fire                    | Frost Essence, Permafrost Core, Eternal Ice    |
| Lightning | Storm areas           | Earth (grounding)       | Spark Essence, Thunder Core, Bottled Lightning |

---

### Undead

#### Zombie

| Attribute    | Value                                        |
| ------------ | -------------------------------------------- |
| Environment  | Graveyards, Ruins, Swamps                    |
| Social       | Swarm (if created together) or Independent   |
| Aggression   | Hostile                                      |
| Activity     | Nocturnal (can act in day, slower)           |
| Intelligence | Beast (follows basic commands if controlled) |
| Magic        | None (animated by magic)                     |

**Harvestable Parts**

| Part          | Quality Range  | Uses                                 |
| ------------- | -------------- | ------------------------------------ |
| Rotting Flesh | Common         | Alchemy (pestilence), Necromancy     |
| Bones         | Common-Quality | Alchemy, Necromancy components       |
| Death Essence | Common-Quality | Alchemy (undead related), Necromancy |
| Intact Organs | Rare           | Alchemy (preservation)               |

**Magical Variant: Plague Zombie** (Uncommon)

| Attribute         | Value                      |
| ----------------- | -------------------------- |
| Rarity            | Uncommon                   |
| Distinguishing    | Green miasma, more decayed |
| Magic             | Innate (disease spread)    |
| Additional Danger | Attacks may cause disease  |

_Additional Parts_

| Part         | Quality Range | Uses                                      |
| ------------ | ------------- | ----------------------------------------- |
| Plague Gland | Quality-Fine  | Alchemy (disease potions, cures by study) |
| Miasma Sac   | Quality       | Alchemy (pestilence bombs)                |

---

#### Skeleton

| Attribute    | Value                                  |
| ------------ | -------------------------------------- |
| Environment  | Dungeons, Ruins, Graveyards            |
| Social       | Pack (military formation if warriors)  |
| Aggression   | Hostile                                |
| Activity     | Always                                 |
| Intelligence | Cunning (retains combat training)      |
| Magic        | None (may use weapons/armor from life) |

**Harvestable Parts**

| Part          | Quality Range  | Uses                               |
| ------------- | -------------- | ---------------------------------- |
| Bones         | Common-Quality | Alchemy, Necromancy, Crafting      |
| Death Essence | Common-Quality | Alchemy, Necromancy                |
| Equipment     | Varies         | Salvage (weapons, armor from life) |
| Bone Dust     | Common         | Alchemy (various)                  |

**Magical Variant: Skeletal Mage** (Uncommon)

| Attribute    | Value                                                      |
| ------------ | ---------------------------------------------------------- |
| Rarity       | Uncommon                                                   |
| Intelligence | Sapient                                                    |
| Magic        | Mana (retains spellcasting from life)                      |
| Spells       | Destruction (Frostbolt, Shadow Bolt), Conjuration (Summon) |

_Additional Parts_

| Part                | Quality Range | Uses                                  |
| ------------------- | ------------- | ------------------------------------- |
| Mage Bones          | Quality-Fine  | Enchanting (mana storage), Necromancy |
| Phylactery Fragment | Rare drop     | Necromancy (binding), Enchanting      |

---

#### Ghost / Specter

| Attribute    | Value                                      |
| ------------ | ------------------------------------------ |
| Environment  | Ruins, Graveyards, Haunted locations       |
| Social       | Independent                                |
| Aggression   | Territorial (bound to location) or Hostile |
| Activity     | Nocturnal                                  |
| Intelligence | Sapient                                    |
| Magic        | Innate (incorporeal, fear, possession)     |

**Abilities**

| Ability           | Effect                        |
| ----------------- | ----------------------------- |
| Incorporeal       | Physical attacks reduced 75%  |
| Fear Aura         | Causes fear status            |
| Life Drain        | Heals by damaging living      |
| Possession (rare) | Can attempt to control living |

**Harvestable Parts**

| Part             | Quality Range  | Uses                                          |
| ---------------- | -------------- | --------------------------------------------- |
| Ectoplasm        | Common-Quality | Alchemy (ghost-affecting potions), Enchanting |
| Spirit Essence   | Quality-Fine   | Alchemy (ethereal effects), Necromancy        |
| Anchoring Object | Varies         | Breaking curse, Enchanting                    |

**Magical Variant: Wraith** (Rare)

| Attribute      | Value                                  |
| -------------- | -------------------------------------- |
| Rarity         | Rare                                   |
| Distinguishing | More solid shadow form, visible hatred |
| Magic          | Enhanced (life drain, death touch)     |
| Aggression     | Hostile (seeks to kill)                |

_Additional Parts_

| Part                | Quality Range | Uses                                         |
| ------------------- | ------------- | -------------------------------------------- |
| Wraith Essence      | Fine-Superior | Alchemy (powerful undead effects)            |
| Death Touch Residue | Fine          | Alchemy (instant damage poisons), Necromancy |

---

### Fey Creatures

#### Sprite / Pixie

| Attribute    | Value                                    |
| ------------ | ---------------------------------------- |
| Environment  | Forest (ancient), Magical groves         |
| Social       | Swarm (5-20)                             |
| Aggression   | Territorial (mischievous, rarely lethal) |
| Activity     | Diurnal                                  |
| Intelligence | Sapient                                  |
| Magic        | Innate (illusion, nature, flight)        |

**Abilities**

| Ability        | Effect                      |
| -------------- | --------------------------- |
| Flight         | Permanent                   |
| Glamour        | Minor illusions             |
| Sleep Dust     | Causes drowsiness           |
| Nature's Touch | Minor healing to plants/fey |

**Harvestable Parts**

| Part         | Quality Range  | Uses                                        |
| ------------ | -------------- | ------------------------------------------- |
| Fairy Dust   | Common-Fine    | Alchemy (illusion, sleep, flight potions)   |
| Sprite Wings | Common-Quality | Alchemy (lightness), Tailoring (decoration) |
| Fey Essence  | Quality        | Enchanting (illusion items)                 |

**Magical Variant: Faerie Dragon** (Rare)

| Attribute      | Value                                                    |
| -------------- | -------------------------------------------------------- |
| Rarity         | Rare                                                     |
| Distinguishing | Tiny dragon with butterfly wings                         |
| Magic          | Enhanced (illusion mastery, breath weapon: euphoria gas) |
| Social         | Independent                                              |

_Additional Parts_

| Part             | Quality Range | Uses                                     |
| ---------------- | ------------- | ---------------------------------------- |
| Prismatic Scales | Fine-Superior | Alchemy (color spray, illusion), Jewelry |
| Euphoria Gland   | Quality-Fine  | Alchemy (mood potions), Recreation       |
| Fey Dragon Heart | Rare drop     | Enchanting (powerful illusion items)     |

---

#### Treant / Treefolk

| Attribute    | Value                                              |
| ------------ | -------------------------------------------------- |
| Environment  | Ancient Forest                                     |
| Social       | Independent (groves may cooperate)                 |
| Aggression   | Defensive (Hostile to those harming forest)        |
| Activity     | Diurnal (dormant at night)                         |
| Intelligence | Sapient                                            |
| Magic        | Innate (nature magic, entangle, speak with plants) |

**Abilities**

| Ability            | Effect                          |
| ------------------ | ------------------------------- |
| Barkskin           | Natural armor                   |
| Entangle           | Roots grasp enemies             |
| Animate Trees      | Temporarily awaken nearby trees |
| Regeneration       | Heals in sunlight               |
| Fire Vulnerability | Extra fire damage               |

**Harvestable Parts**

| Part         | Quality Range    | Uses                                                |
| ------------ | ---------------- | --------------------------------------------------- |
| Living Wood  | Quality-Superior | Bowyer (exceptional bows), Staves, Crafting         |
| Heartwood    | Fine-Superior    | Enchanting (nature magic), Artificer                |
| Amber Sap    | Quality-Fine     | Alchemy (healing, preservation), Jewelry            |
| Ancient Bark | Quality          | Alchemy (protection), Armor                         |
| Treant Seed  | Rare drop        | Growing new treant (decades), Ultimate nature craft |

---

### Giants

#### Hill Giant

| Attribute    | Value                       |
| ------------ | --------------------------- |
| Environment  | Hills, Plains, Forest edges |
| Social       | Pack (family groups, 2-6)   |
| Aggression   | Aggressive                  |
| Activity     | Diurnal                     |
| Intelligence | Cunning (simple)            |
| Magic        | None                        |

**Harvestable Parts**

| Part        | Quality Range  | Uses                                   |
| ----------- | -------------- | -------------------------------------- |
| Giant Meat  | Common         | Cooking (large quantity)               |
| Giant Hide  | Common-Quality | Leatherworking (thick leather)         |
| Giant Bones | Common-Quality | Crafting (structural), Alchemy         |
| Giant Blood | Common-Quality | Alchemy (size/strength potions)        |
| Giant Fat   | Common         | Alchemy, Crafting (candles, lubricant) |

**Magical Variant: Stone Giant** (Uncommon)

| Attribute      | Value                               |
| -------------- | ----------------------------------- |
| Rarity         | Uncommon                            |
| Environment    | Mountains, Caves                    |
| Distinguishing | Rock-like skin, more intelligent    |
| Intelligence   | Sapient                             |
| Magic          | Innate (stone shaping, earth magic) |

_Additional Parts_

| Part              | Quality Range    | Uses                                 |
| ----------------- | ---------------- | ------------------------------------ |
| Stone Skin        | Quality-Superior | Armor (exceptional), Alchemy         |
| Earth Blood       | Fine             | Alchemy (petrification, earth magic) |
| Giant Heart Stone | Rare drop        | Artificer (earth golems), Enchanting |

---

#### Frost Giant (Rare)

| Attribute    | Value                                 |
| ------------ | ------------------------------------- |
| Rarity       | Rare                                  |
| Environment  | Tundra, High Mountains                |
| Social       | Pack (tribal, 5-15)                   |
| Intelligence | Sapient                               |
| Magic        | Innate (ice magic) + Tribal (shamans) |

**Abilities**

| Ability            | Effect                    |
| ------------------ | ------------------------- |
| Ice Breath         | Cone cold damage          |
| Cold Immunity      | Cannot be damaged by cold |
| Fire Vulnerability | Extra fire damage         |
| Ice Shaping        | Create weapons/barriers   |

**Harvestable Parts**

| Part             | Quality Range  | Uses                              |
| ---------------- | -------------- | --------------------------------- |
| Frost Giant Hide | Quality-Fine   | Armor (cold resistant)            |
| Ice Blood        | Fine-Superior  | Alchemy (cold potions, ice magic) |
| Frozen Heart     | Fine-Legendary | Enchanting (ice magic), Artificer |
| Permafrost Bone  | Quality-Fine   | Crafting (never melts), Weapons   |

---

## 4. Corrupted Enlightened Races (Brutes)

When members of enlightened races become corrupted through dark magic, possession, prolonged exposure to void energies, or catastrophic trauma, they may devolve into monstrous "Brute" forms. These creatures retain their physical racial traits but lose classes, skills, and higher reasoning.

### Corruption Mechanics

| Factor       | Effect                                      |
| ------------ | ------------------------------------------- |
| Classes      | All lost - cannot use class abilities       |
| Skills       | All lost - rely on instinct                 |
| Intelligence | Drops to Cunning or Beast                   |
| Magic        | Mana lost, may gain innate corruption magic |
| Aggression   | Always Hostile                              |
| Acceptance   | Permanent Hostile to all factions           |
| Recovery     | Extremely rare, requires major intervention |

---

### Human Brute

| Attribute     | Value                                 |
| ------------- | ------------------------------------- |
| Original Race | Human                                 |
| Size          | Large (mutated growth)                |
| Intelligence  | Beast                                 |
| Magic         | None or Innate (corruption)           |
| Social        | Pack (drawn to others) or Independent |
| Aggression    | Hostile                               |

**Physical Changes**

- Hunched, muscular frame
- Elongated arms
- Distorted features, visible veins
- Often retains scraps of clothing/equipment

**Abilities**

| Ability                   | Effect                    |
| ------------------------- | ------------------------- |
| Berserk Strength          | +50% damage, ignores pain |
| Maddened Charge           | Rushing attack            |
| Corruption Aura (magical) | Sickness to nearby living |

**Harvestable Parts**

| Part               | Quality Range  | Uses                                               |
| ------------------ | -------------- | -------------------------------------------------- |
| Corrupted Flesh    | Common-Quality | Alchemy (corruption study), Necromancy             |
| Tainted Blood      | Common-Quality | Alchemy (berserk potions, dangerous), Curses       |
| Warped Bones       | Common-Quality | Alchemy, Crafting (unusual properties)             |
| Corruption Node    | Quality-Fine   | Alchemy (purification by study), Enchanting (dark) |
| Salvaged Equipment | Varies         | May retain original gear, damaged                  |

**Magical Variant: Void-Touched Human** (Rare)

| Attribute      | Value                                         |
| -------------- | --------------------------------------------- |
| Rarity         | Rare                                          |
| Distinguishing | Black veins, void portals flicker around body |
| Magic          | Innate (void magic: teleport, entropy damage) |

_Additional Parts_

| Part         | Quality Range | Uses                                       |
| ------------ | ------------- | ------------------------------------------ |
| Void Essence | Fine-Superior | Enchanting (void magic), Alchemy (entropy) |
| Reality Scar | Rare drop     | Enchanting (teleportation), Artificer      |

---

### Elven Brute (Blighted Elf)

| Attribute     | Value                                 |
| ------------- | ------------------------------------- |
| Original Race | Elf                                   |
| Size          | Medium (twisted, angular)             |
| Intelligence  | Cunning                               |
| Magic         | Corrupted Innate (anti-nature, decay) |
| Social        | Pack (hunting groups)                 |
| Aggression    | Hostile                               |

**Physical Changes**

- Pallid, cracked skin
- Eyes glow sickly green/purple
- Elongated limbs, sharp movements
- Nature dies in their presence

**Abilities**

| Ability          | Effect                      |
| ---------------- | --------------------------- |
| Decay Touch      | Damages and rots            |
| Anti-Magic Field | Disrupts mana near them     |
| Feral Speed      | Enhanced agility retained   |
| Nature's Bane    | Plants wither, animals flee |

**Harvestable Parts**

| Part                   | Quality Range | Uses                                         |
| ---------------------- | ------------- | -------------------------------------------- |
| Blighted Skin          | Quality-Fine  | Tailoring (anti-magic), Alchemy              |
| Anti-Mana Gland        | Fine-Superior | Enchanting (mana drain), Alchemy (dispel)    |
| Decay Essence          | Quality-Fine  | Alchemy (rot, entropy), Necromancy           |
| Corrupted Arcane Blood | Fine          | Alchemy (corrupted magic), Enchanting (dark) |
| Hollow Eyes            | Quality       | Alchemy (true seeing through corruption)     |

---

### Dwarven Brute (Stone Husk)

| Attribute     | Value                              |
| ------------- | ---------------------------------- |
| Original Race | Dwarf                              |
| Size          | Medium (dense, heavy)              |
| Intelligence  | Beast                              |
| Magic         | Innate (earth corruption)          |
| Social        | Independent (drawn to deep places) |
| Aggression    | Territorial → Hostile              |

**Physical Changes**

- Skin partially petrified
- Beard fused into stone
- Eyes are empty pits
- Moves with grinding sounds

**Abilities**

| Ability       | Effect                          |
| ------------- | ------------------------------- |
| Stone Form    | Extreme damage resistance       |
| Crushing Grip | Grapple deals continuous damage |
| Earth Sense   | Detects vibrations perfectly    |
| Tunnel        | Can burrow through earth        |

**Harvestable Parts**

| Part            | Quality Range | Uses                                 |
| --------------- | ------------- | ------------------------------------ |
| Living Stone    | Quality-Fine  | Armor, Crafting (unique material)    |
| Petrified Heart | Fine-Superior | Artificer (construct cores), Alchemy |
| Void Ore        | Quality-Fine  | Smithing (corrupted metal)           |
| Stone Beard     | Quality       | Decoration, Alchemy (preservation)   |
| Grinding Teeth  | Quality       | Crafting (cutting tools)             |

---

### Lizardfolk Brute (Razorscale)

| Attribute     | Value                                  |
| ------------- | -------------------------------------- |
| Original Race | Lizardfolk                             |
| Size          | Large (elongated)                      |
| Intelligence  | Cunning                                |
| Magic         | Innate (blood magic, water corruption) |
| Social        | Pack (hunting pods, 3-6)               |
| Aggression    | Aggressive                             |

**Physical Changes**

- Scales hardened into blade-like edges
- Extended claws and teeth
- Tail becomes weapon
- Red-tinged eyes

**Abilities**

| Ability         | Effect                      |
| --------------- | --------------------------- |
| Razor Scales    | Passive damage to attackers |
| Blood Frenzy    | Heals from dealing damage   |
| Water Breathing | Retained                    |
| Death Roll      | Devastating grapple attack  |

**Harvestable Parts**

| Part                       | Quality Range | Uses                                |
| -------------------------- | ------------- | ----------------------------------- |
| Razor Scales               | Quality-Fine  | Weaponsmithing, Armor (spiked)      |
| Blood-Drinker Fang         | Fine          | Alchemy (vampiric effects), Weapons |
| Frenzy Gland               | Quality-Fine  | Alchemy (berserker potions)         |
| Corrupted Amphibian Organs | Quality       | Alchemy (water corruption)          |
| Blade Tail                 | Quality       | Weaponsmithing (unique weapons)     |

---

### Gnoll Brute (Ravager)

| Attribute     | Value                              |
| ------------- | ---------------------------------- |
| Original Race | Gnoll                              |
| Size          | Large                              |
| Intelligence  | Beast (hunting instinct only)      |
| Magic         | Corrupted Shamanic (blood spirits) |
| Social        | Pack (feral pack, 5-10)            |
| Aggression    | Hostile                            |

**Physical Changes**

- Overgrown, matted fur
- Exposed bone and muscle
- Constant snarling
- Exudes fear pheromones

**Abilities**

| Ability      | Effect                            |
| ------------ | --------------------------------- |
| Fear Howl    | AoE fear effect                   |
| Scent Hunter | Cannot be hidden from             |
| Pack Frenzy  | Bonus when others nearby          |
| Blood Spirit | Gains temporary spirit from kills |

**Harvestable Parts**

| Part                 | Quality Range | Uses                                             |
| -------------------- | ------------- | ------------------------------------------------ |
| Ravager Pelt         | Quality-Fine  | Armor (intimidating), Shamanic crafting          |
| Fear Gland           | Fine          | Alchemy (terror potions, courage antidote study) |
| Blood Spirit Residue | Quality-Fine  | Shamanic crafting (dark totems)                  |
| Massive Fangs        | Quality-Fine  | Weapons, Jewelry (intimidation)                  |
| Corrupted Heart      | Fine          | Alchemy (rage), Shamanic                         |

---

### Goblin Brute (Plague Goblin)

| Attribute     | Value                                |
| ------------- | ------------------------------------ |
| Original Race | Goblin                               |
| Size          | Small (bloated)                      |
| Intelligence  | Cunning (malicious)                  |
| Magic         | Corrupted Shamanic (disease spirits) |
| Social        | Swarm (infested colonies, 20-50)     |
| Aggression    | Aggressive                           |

**Physical Changes**

- Bloated, pustulent skin
- Oozing sores
- Shared Memory corrupted into disease-spread
- Giggling, erratic behavior

**Abilities**

| Ability                 | Effect                            |
| ----------------------- | --------------------------------- |
| Disease Cloud           | AoE sickness                      |
| Corrupted Shared Memory | Spreads madness to nearby goblins |
| Explosive Death         | Releases plague burst when killed |
| Swarm Tactics           | Overwhelming numbers              |

**Harvestable Parts**

| Part                   | Quality Range  | Uses                           |
| ---------------------- | -------------- | ------------------------------ |
| Plague Sac             | Common-Quality | Alchemy (disease study, cures) |
| Corrupted Goblin Brain | Quality        | Alchemy (shared memory study)  |
| Disease-Riddled Flesh  | Common         | Alchemy (pestilence)           |
| Madness Residue        | Quality-Fine   | Alchemy (confusion potions)    |

---

### Orc Brute (Doom Orc)

| Attribute     | Value                                     |
| ------------- | ----------------------------------------- |
| Original Race | Orc                                       |
| Size          | Large (massive)                           |
| Intelligence  | Cunning                                   |
| Magic         | Corrupted Shamanic (war spirits gone mad) |
| Social        | Pack (war band remnants, 3-8)             |
| Aggression    | Hostile                                   |

**Physical Changes**

- Extreme muscle growth
- Tusks elongated into weapons
- War paint fused into skin (glows)
- Constantly bleeding but doesn't die

**Abilities**

| Ability          | Effect                                      |
| ---------------- | ------------------------------------------- |
| Unstoppable      | Doesn't fall until truly dead               |
| War Cry          | Buff to nearby corrupted, debuff to enemies |
| Mad Shaman Magic | Erratic war spirit effects                  |
| Blood for Blood  | Damage heals nearby allies                  |

**Harvestable Parts**

| Part                | Quality Range | Uses                                     |
| ------------------- | ------------- | ---------------------------------------- |
| Doom Hide           | Quality-Fine  | Armor (intimidating, tough)              |
| War Spirit Fragment | Fine-Superior | Shamanic crafting (powerful, dangerous)  |
| Unstoppable Heart   | Fine          | Alchemy (persistence), Artificer         |
| Fused War Paint     | Quality-Fine  | Shamanic (permanent but dangerous paint) |
| Massive Tusks       | Quality-Fine  | Weapons, Decoration                      |

---

### Kobold Brute (Tunnel Horror)

| Attribute     | Value                            |
| ------------- | -------------------------------- |
| Original Race | Kobold                           |
| Size          | Medium (stretched, spindly)      |
| Intelligence  | Cunning (trap obsession remains) |
| Magic         | Corrupted Innate (trap magic)    |
| Social        | Swarm (infested warrens, 15-30)  |
| Aggression    | Territorial                      |

**Physical Changes**

- Elongated limbs for climbing
- Multiple eyes
- Body partially merged with traps
- Clicking, mechanical sounds

**Abilities**

| Ability                | Effect                       |
| ---------------------- | ---------------------------- |
| Trap Body              | Body parts function as traps |
| Ceiling Crawler        | Perfect climbing             |
| Mass Trap              | Creates traps rapidly        |
| Mechanical Integration | Merged with constructs       |

**Harvestable Parts**

| Part              | Quality Range | Uses                                   |
| ----------------- | ------------- | -------------------------------------- |
| Trap Gland        | Quality-Fine  | Artificer (trap creation), Engineering |
| Multi-Eyes        | Quality       | Alchemy (perception, paranoia)         |
| Mechanical Chitin | Quality-Fine  | Artificer (construct parts)            |
| Trap Components   | Varies        | Engineering (salvage)                  |

---

## 5. Magic-Capable Mobs

### Mana Casters

Creatures with access to mana magic (like enlightened races) have spell lists organized by school.

#### Imp

| Attribute    | Value              |
| ------------ | ------------------ |
| Environment  | Volcanic, Summoned |
| Social       | Pack (3-7)         |
| Aggression   | Aggressive         |
| Intelligence | Cunning            |
| Magic        | Mana (limited)     |

**Spell List (Destruction/Conjuration)**

| Spell       | School      | Effect                    |
| ----------- | ----------- | ------------------------- |
| Fire Bolt   | Destruction | Single target fire damage |
| Flame Burst | Destruction | Small AoE fire            |
| Summon Imp  | Conjuration | Call one ally             |
| Blink       | Conjuration | Short teleport            |

**Harvestable Parts**

| Part       | Quality Range  | Uses                            |
| ---------- | -------------- | ------------------------------- |
| Imp Horns  | Common-Quality | Alchemy (fire), Crafting        |
| Fire Gland | Common-Quality | Alchemy (fire potions)          |
| Imp Blood  | Common-Quality | Alchemy (summoning), Enchanting |
| Bat Wings  | Common         | Alchemy (flight)                |

---

#### Harpy

| Attribute    | Value                     |
| ------------ | ------------------------- |
| Environment  | Mountains, Coastal cliffs |
| Social       | Pack (flock, 4-12)        |
| Aggression   | Aggressive                |
| Intelligence | Cunning                   |
| Magic        | Mana (Illusion focused)   |

**Spell List (Illusion)**

| Spell            | School   | Effect            |
| ---------------- | -------- | ----------------- |
| Captivating Song | Illusion | Charm effect      |
| Dissonant Shriek | Illusion | Confusion, damage |
| Mirror Image     | Illusion | Create duplicates |
| Fear             | Illusion | Causes fear       |

**Harvestable Parts**

| Part           | Quality Range  | Uses                               |
| -------------- | -------------- | ---------------------------------- |
| Harpy Feathers | Common-Quality | Alchemy (charm, flight), Fletching |
| Voice Box      | Quality-Fine   | Enchanting (sound magic), Alchemy  |
| Harpy Talons   | Common-Quality | Weapons, Crafting                  |
| Charm Gland    | Quality        | Alchemy (charm potions)            |

---

### Tribal (Shamanic) Casters

Creatures with tribal magic draw from a spirit pool rather than personal mana.

#### Goblin Shaman

| Attribute    | Value                   |
| ------------ | ----------------------- |
| Environment  | With goblin tribes      |
| Social       | Pack (leads or advises) |
| Aggression   | Aggressive              |
| Intelligence | Sapient                 |
| Magic        | Tribal (shamanic)       |

**Spell List (Shamanic)**

| Spell              | Type         | Effect               |
| ------------------ | ------------ | -------------------- |
| Curse of Weakness  | Spirit Curse | Stat reduction       |
| Ancestral Guidance | Spirit Buff  | Tribe accuracy boost |
| Swamp Breath       | Nature       | Poison cloud         |
| Healing Ritual     | Restoration  | Heal over time       |
| Summon Pest Swarm  | Conjuration  | Summon insects       |

**Harvestable Parts**

| Part                | Quality Range | Uses                          |
| ------------------- | ------------- | ----------------------------- |
| Shaman Staff        | Quality-Fine  | Shamanic crafting, Enchanting |
| Spirit Fetish       | Quality-Fine  | Shamanic crafting             |
| Ritual Components   | Varies        | Various shamanic              |
| Goblin Shaman Brain | Fine          | Alchemy (shared memory study) |

---

#### Orc War Shaman

| Attribute    | Value                     |
| ------------ | ------------------------- |
| Environment  | With orc tribes           |
| Social       | Pack (war leader advisor) |
| Aggression   | Aggressive                |
| Intelligence | Sapient                   |
| Magic        | Tribal (war spirits)      |

**Spell List (War Shamanic)**

| Spell                 | Type         | Effect                 |
| --------------------- | ------------ | ---------------------- |
| War Blessing          | Spirit Buff  | Damage boost to allies |
| Blood Rage            | Spirit Buff  | Berserk state          |
| Ancestor's Strength   | Spirit Buff  | Temporary stat boost   |
| Spirit Weapon         | Conjuration  | Summon fighting spirit |
| Intimidating Presence | Spirit Curse | Fear to enemies        |

**Harvestable Parts**

| Part              | Quality Range | Uses                                |
| ----------------- | ------------- | ----------------------------------- |
| War Totem         | Fine-Superior | Shamanic crafting                   |
| Spirit Drum       | Quality-Fine  | Shamanic crafting, Performance      |
| Blessed War Paint | Fine          | Shamanic crafting (powerful paints) |
| War Spirit Vessel | Fine          | Shamanic binding                    |

---

## 6. Aquatic Creatures

#### Giant Crab

| Attribute    | Value                  |
| ------------ | ---------------------- |
| Environment  | Coastal, Shallow water |
| Social       | Independent            |
| Aggression   | Defensive              |
| Activity     | Always                 |
| Intelligence | Beast                  |
| Magic        | None                   |

**Harvestable Parts**

| Part         | Quality Range  | Uses                      |
| ------------ | -------------- | ------------------------- |
| Crab Meat    | Common-Fine    | Cooking (delicacy)        |
| Crab Shell   | Common-Quality | Armor (plates), Alchemy   |
| Claws        | Common-Quality | Crafting (tools), Weapons |
| Crab Innards | Common         | Fishing bait, Alchemy     |

---

#### Shark

| Attribute    | Value                              |
| ------------ | ---------------------------------- |
| Environment  | Coastal, Open water                |
| Social       | Independent (feeding frenzy: Pack) |
| Aggression   | Aggressive (blood triggers)        |
| Intelligence | Beast                              |
| Magic        | None                               |

**Harvestable Parts**

| Part            | Quality Range  | Uses                              |
| --------------- | -------------- | --------------------------------- |
| Shark Meat      | Common-Quality | Cooking                           |
| Shark Skin      | Common-Quality | Leatherworking (tough, sandpaper) |
| Shark Teeth     | Common-Fine    | Weapons, Jewelry, Alchemy         |
| Shark Fin       | Quality        | Cooking (controversial), Alchemy  |
| Shark Liver Oil | Common-Quality | Alchemy (waterproofing, health)   |

**Magical Variant: Storm Shark** (Rare)

| Attribute      | Value                                   |
| -------------- | --------------------------------------- |
| Rarity         | Rare                                    |
| Distinguishing | Electric blue markings, sparks in water |
| Magic          | Innate (lightning, storm sense)         |

_Additional Parts_

| Part            | Quality Range  | Uses                                |
| --------------- | -------------- | ----------------------------------- |
| Storm Skin      | Fine-Superior  | Armor (lightning resist), Alchemy   |
| Lightning Teeth | Fine           | Weapons (shocking), Enchanting      |
| Tempest Organ   | Fine-Legendary | Alchemy (storm calling), Enchanting |

---

## 7. Mob Encounter Tables by Environment

### Forest (Temperate)

| Time  | Common             | Uncommon         | Rare                 |
| ----- | ------------------ | ---------------- | -------------------- |
| Day   | Deer, Rabbit, Boar | Wolf, Bear       | Moonlit Stag, Treant |
| Night | Wolf, Giant Spider | Dire Wolf, Ghost | Phantom Wolf, Wraith |
| Any   | Goblin patrol      | Orc scouts       | Blighted Elf         |

### Mountains

| Time  | Common                     | Uncommon               | Rare                     |
| ----- | -------------------------- | ---------------------- | ------------------------ |
| Day   | Goat, Eagle, Mountain Lion | Bear, Hill Giant       | Stone Giant, Thunderhawk |
| Night | Wolf, Harpy                | Dire Wolf, Stone Giant | Frost Giant              |
| Any   | Orc tribe                  | Ogre group             | Dragon (legendary)       |

### Swamp

| Time  | Common                         | Uncommon                   | Rare                |
| ----- | ------------------------------ | -------------------------- | ------------------- |
| Day   | Snake, Crocodile, Giant Insect | Deathroll Croc, Zombie     | Basilisk, Hag       |
| Night | Ghost, Giant Spider            | Plague Zombie, Will-o-wisp | Wraith, Razorscale  |
| Any   | Lizardfolk patrol              | Goblin tribe               | Plague Goblin swarm |

### Desert

| Time  | Common                   | Uncommon                 | Rare                             |
| ----- | ------------------------ | ------------------------ | -------------------------------- |
| Day   | Scorpion, Snake, Vulture | Giant Scorpion, Ashviper | Crystal Scorpion, Fire Elemental |
| Night | Scorpion, Undead         | Giant Scorpion, Ghost    | Inferno Elemental                |
| Any   | Nomad bandits            | Sand Elemental           | Void-Touched Human               |

### Tundra

| Time  | Common              | Uncommon                 | Rare                      |
| ----- | ------------------- | ------------------------ | ------------------------- |
| Day   | Wolf, Bear, Mammoth | Dire Wolf, Ice Elemental | Spirit Bear, Frost Giant  |
| Night | Wolf, Ghost         | Phantom Wolf             | Wendigo, Frost Giant raid |
| Any   | Ice Elemental       | Frost Giant scout        | Frost Giant shaman        |

---

## 8. Domesticated Animals

These creatures are typically not hostile and are raised by settlements or can be tamed.

### Horse

| Attribute    | Value                            |
| ------------ | -------------------------------- |
| Environment  | Settlements, Plains (wild herds) |
| Social       | Herd (domesticated: individual)  |
| Aggression   | Passive (Defensive if cornered)  |
| Intelligence | Cunning                          |
| Magic        | None                             |
| Tameable     | Yes                              |

**Harvestable Parts** (deceased)

| Part         | Quality Range  | Uses                                    |
| ------------ | -------------- | --------------------------------------- |
| Horse Meat   | Common-Quality | Cooking (regional), Pet food            |
| Horse Hide   | Common-Fine    | Leatherworking (supple leather)         |
| Horsehair    | Common-Quality | Bowyer (bowstrings), Brushes, Tailoring |
| Horse Hooves | Common         | Glue, Alchemy                           |
| Sinew        | Common-Quality | Crafting                                |

**Living Products**

| Product             | Frequency | Uses                  |
| ------------------- | --------- | --------------------- |
| Horsehair (groomed) | Weekly    | Crafting (non-lethal) |
| Manure              | Daily     | Farming (fertilizer)  |

**Magical Variant: Nightmare** (Rare - not tameable)

| Attribute      | Value                           |
| -------------- | ------------------------------- |
| Rarity         | Rare                            |
| Distinguishing | Black coat, flaming mane/hooves |
| Magic          | Innate (fire, planar travel)    |
| Aggression     | Hostile                         |

_Additional Parts_

| Part            | Quality Range      | Uses                                |
| --------------- | ------------------ | ----------------------------------- |
| Nightmare Mane  | Fine-Legendary     | Alchemy (fire immunity), Enchanting |
| Hellfire Hooves | Fine-Superior      | Smithing (fire weapons)             |
| Planar Essence  | Superior-Legendary | Enchanting (transportation)         |

---

### Cattle (Cow/Bull)

| Attribute    | Value                              |
| ------------ | ---------------------------------- |
| Environment  | Settlements, Plains (wild aurochs) |
| Social       | Herd                               |
| Aggression   | Passive (Bulls: Defensive)         |
| Intelligence | Beast                              |
| Magic        | None                               |

**Harvestable Parts** (deceased)

| Part        | Quality Range  | Uses                              |
| ----------- | -------------- | --------------------------------- |
| Beef        | Common-Fine    | Cooking (versatile meat)          |
| Cattle Hide | Common-Quality | Leatherworking (standard leather) |
| Horns       | Common-Quality | Crafting, Alchemy, Decoration     |
| Bones       | Common         | Crafting, Alchemy                 |
| Tallow      | Common-Quality | Candles, Alchemy, Cooking         |
| Stomach     | Common         | Crafting (waterskins, containers) |

**Living Products**

| Product | Frequency | Uses                     |
| ------- | --------- | ------------------------ |
| Milk    | Daily     | Cooking, Alchemy (bases) |
| Manure  | Daily     | Farming                  |

---

### Sheep

| Attribute    | Value              |
| ------------ | ------------------ |
| Environment  | Settlements, Hills |
| Social       | Herd               |
| Aggression   | Passive            |
| Intelligence | Beast              |
| Magic        | None               |

**Harvestable Parts** (deceased)

| Part       | Quality Range  | Uses                              |
| ---------- | -------------- | --------------------------------- |
| Mutton     | Common-Quality | Cooking                           |
| Sheep Hide | Common         | Leatherworking (soft leather)     |
| Lanolin    | Common-Quality | Alchemy (waterproofing), Skincare |

**Living Products**

| Product | Frequency           | Uses                      |
| ------- | ------------------- | ------------------------- |
| Wool    | Seasonal (shearing) | Tailoring (primary fiber) |
| Milk    | Daily               | Cooking, Cheese           |

**Magical Variant: Cloudwool Sheep** (Uncommon - rare domestic breed)

| Attribute      | Value                                  |
| -------------- | -------------------------------------- |
| Rarity         | Uncommon                               |
| Distinguishing | Extremely fluffy, wool has slight glow |
| Magic          | Innate (featherfall on self)           |

_Additional Living Products_

| Product   | Quality Range | Uses                                         |
| --------- | ------------- | -------------------------------------------- |
| Cloudwool | Quality-Fine  | Tailoring (featherfall items, magical robes) |

---

### Chicken

| Attribute    | Value        |
| ------------ | ------------ |
| Environment  | Settlements  |
| Social       | Herd (flock) |
| Aggression   | Passive      |
| Intelligence | Beast        |
| Magic        | None         |

**Harvestable Parts** (deceased)

| Part         | Quality Range | Uses                      |
| ------------ | ------------- | ------------------------- |
| Chicken Meat | Common        | Cooking (versatile)       |
| Feathers     | Common        | Fletching (bulk), Pillows |
| Bones        | Common        | Alchemy (divination)      |

**Living Products**

| Product           | Frequency  | Uses                             |
| ----------------- | ---------- | -------------------------------- |
| Eggs              | Daily      | Cooking, Alchemy (binding agent) |
| Feathers (molted) | Occasional | Crafting                         |

---

### Pig

| Attribute    | Value        |
| ------------ | ------------ |
| Environment  | Settlements  |
| Social       | Herd (drove) |
| Aggression   | Passive      |
| Intelligence | Cunning      |
| Magic        | None         |

**Harvestable Parts** (deceased)

| Part         | Quality Range  | Uses                          |
| ------------ | -------------- | ----------------------------- |
| Pork         | Common-Fine    | Cooking (versatile)           |
| Pig Skin     | Common-Quality | Leatherworking (soft leather) |
| Pig Fat/Lard | Common-Quality | Cooking, Alchemy              |
| Bristles     | Common         | Brushes                       |
| Bladder      | Common         | Containers, Balls             |

**Living Uses**

| Use             | Application           |
| --------------- | --------------------- |
| Truffle hunting | Locate rare mushrooms |
| Waste disposal  | Eat scraps            |

---

## 9. Legendary Creatures

These creatures are unique or extremely rare encounters, often serving as major challenges or quest objectives.

### Dragon

| Attribute    | Value                              |
| ------------ | ---------------------------------- |
| Environment  | Mountains, Volcanic, Hoards        |
| Social       | Independent (fiercely territorial) |
| Aggression   | Territorial → Hostile              |
| Intelligence | Sapient (highly intelligent)       |
| Magic        | Innate (breath weapon, spells)     |
| Rarity       | Legendary                          |

**Dragon Types**

| Type          | Environment            | Breath    | Affinity    |
| ------------- | ---------------------- | --------- | ----------- |
| Fire Dragon   | Volcanic, Mountains    | Fire      | Destruction |
| Frost Dragon  | Tundra, High Mountains | Ice       | Abjuration  |
| Storm Dragon  | Coastal, Mountains     | Lightning | Conjuration |
| Shadow Dragon | Caves, Corrupted lands | Necrotic  | Necromancy  |
| Forest Dragon | Ancient Forest         | Poison    | Nature      |

**Abilities (Fire Dragon example)**

| Ability              | Effect                         |
| -------------------- | ------------------------------ |
| Fire Breath          | Cone fire damage               |
| Flight               | Aerial combat                  |
| Fire Immunity        | Cannot be damaged by fire      |
| Frightful Presence   | AoE fear                       |
| Ancient Magic        | Destruction, Abjuration spells |
| Legendary Resistance | Can ignore status effects      |

**Harvestable Parts**

| Part              | Quality Range      | Uses                                |
| ----------------- | ------------------ | ----------------------------------- |
| Dragon Scales     | Superior-Legendary | Armorcrafting (best armor)          |
| Dragon Bones      | Superior-Legendary | Weaponsmithing, Crafting            |
| Dragon Heart      | Legendary          | Artificer (ultimate cores), Alchemy |
| Dragon Blood      | Fine-Legendary     | Alchemy (power potions), Enchanting |
| Dragon Fire Gland | Legendary          | Smithing (dragon forge), Alchemy    |
| Dragon Eye        | Superior-Legendary | Enchanting (true seeing), Alchemy   |
| Dragon Teeth      | Fine-Superior      | Weapons (daggers, arrow tips)       |
| Dragon Hide       | Superior-Legendary | Leatherworking (magical leather)    |

---

### Phoenix

| Attribute    | Value                               |
| ------------ | ----------------------------------- |
| Environment  | Volcanic, Sacred sites              |
| Social       | Independent                         |
| Aggression   | Passive (Defensive of sacred sites) |
| Intelligence | Sapient                             |
| Magic        | Innate (fire, resurrection)         |
| Rarity       | Legendary                           |

**Abilities**

| Ability        | Effect                        |
| -------------- | ----------------------------- |
| Resurrection   | Reborn from ashes when killed |
| Healing Flames | Fire heals allies             |
| Purifying Fire | Burns corruption/undead       |
| Flight         | Aerial                        |
| Fire Immunity  | Cannot be damaged by fire     |

**Harvestable Parts** (shed/gifted—killing is temporary)

| Part                     | Quality Range      | Uses                                        |
| ------------------------ | ------------------ | ------------------------------------------- |
| Phoenix Feather          | Legendary          | Alchemy (resurrection, healing), Enchanting |
| Phoenix Tears            | Legendary          | Alchemy (ultimate healing)                  |
| Phoenix Ash              | Superior-Legendary | Alchemy (rebirth potions)                   |
| Eternal Flame (captured) | Legendary          | Smithing (blessed forge), Alchemy           |

---

### Hydra

| Attribute    | Value                         |
| ------------ | ----------------------------- |
| Environment  | Swamp, Coastal caves          |
| Social       | Independent                   |
| Aggression   | Hostile                       |
| Intelligence | Cunning                       |
| Magic        | Innate (regeneration, poison) |
| Rarity       | Legendary                     |

**Abilities**

| Ability            | Effect                                       |
| ------------------ | -------------------------------------------- |
| Multiple Heads     | 5-9 heads, each attacks                      |
| Regeneration       | Rapidly heals, regrows heads                 |
| Poison Breath      | Each head can breathe poison                 |
| Head Severance     | Cut head = two grow back (if not cauterized) |
| Fire Vulnerability | Fire prevents regeneration                   |

**Harvestable Parts**

| Part                     | Quality Range      | Uses                                    |
| ------------------------ | ------------------ | --------------------------------------- |
| Hydra Blood              | Fine-Legendary     | Alchemy (regeneration), Poisons (toxic) |
| Hydra Scales             | Superior-Legendary | Armor (regenerating?)                   |
| Hydra Heart              | Legendary          | Alchemy (immortality adjacent)          |
| Hydra Head               | Fine-Superior      | Trophy, Alchemy                         |
| Poison Glands (multiple) | Fine-Legendary     | Alchemy (potent poisons)                |
| Hydra Teeth              | Quality-Fine       | Alchemy (legendary: dragon teeth myth)  |

---

### Behemoth

| Attribute    | Value                             |
| ------------ | --------------------------------- |
| Environment  | Remote wilderness, Ancient lands  |
| Social       | Independent                       |
| Aggression   | Territorial                       |
| Intelligence | Cunning                           |
| Magic        | Innate (earth, unstoppable force) |
| Rarity       | Legendary                         |

**Abilities**

| Ability             | Effect                    |
| ------------------- | ------------------------- |
| Colossal Size       | Building-sized            |
| Unstoppable Charge  | Destroys terrain          |
| Earthquake Stomp    | Massive AoE               |
| Impenetrable Hide   | Extreme damage resistance |
| Primordial Strength | Can destroy structures    |

**Harvestable Parts**

| Part             | Quality Range      | Uses                        |
| ---------------- | ------------------ | --------------------------- |
| Behemoth Hide    | Legendary          | Armor (ultimate protection) |
| Behemoth Bones   | Superior-Legendary | Construction, Weapons       |
| Primordial Heart | Legendary          | Artificer (titan golems)    |
| Earth Blood      | Legendary          | Alchemy (earth mastery)     |
| Behemoth Tusks   | Superior-Legendary | Weapons, Construction       |

---

### Kraken

| Attribute    | Value                                |
| ------------ | ------------------------------------ |
| Environment  | Deep ocean                           |
| Social       | Independent                          |
| Aggression   | Aggressive                           |
| Intelligence | Sapient                              |
| Magic        | Innate (water, storms, mind control) |
| Rarity       | Legendary                            |

**Abilities**

| Ability           | Effect                     |
| ----------------- | -------------------------- |
| Massive Tentacles | Multiple grappling attacks |
| Ink Cloud         | Blindness, escape          |
| Storm Calling     | Creates storms at will     |
| Ship Destroyer    | Can sink vessels           |
| Mind Control      | Dominates weak-willed      |

**Harvestable Parts**

| Part            | Quality Range      | Uses                                  |
| --------------- | ------------------ | ------------------------------------- |
| Kraken Tentacle | Superior-Legendary | Weapons (whips), Alchemy              |
| Kraken Ink      | Fine-Legendary     | Alchemy (blindness, shadow), Scribe   |
| Kraken Eye      | Legendary          | Enchanting (far seeing, mind control) |
| Kraken Beak     | Superior-Legendary | Weapons, Armor                        |
| Storm Gland     | Legendary          | Alchemy (weather control)             |

---

## 10. Harvest Skill Effects

| Skill Level | Quality Bonus | Yield Bonus | Rare Part Chance |
| ----------- | ------------- | ----------- | ---------------- |
| None        | -1 tier       | -30%        | 1%               |
| Apprentice  | 0             | 0%          | 5%               |
| Journeyman  | +1 potential  | +15%        | 12%              |
| Master      | +2 potential  | +30%        | 25%              |

### Harvest Tool Requirements

| Creature Type    | Required Tool            | Quality Effect               |
| ---------------- | ------------------------ | ---------------------------- |
| Beast (small)    | Skinning knife           | Higher = cleaner harvest     |
| Beast (large)    | Skinning knife + saw     | Reduces time                 |
| Monster          | Specialized tools        | Required for exotic parts    |
| Elemental        | Essence container        | Required to capture essences |
| Undead           | Blessed tools (optional) | +quality for death essence   |
| Magical creature | Magic-treated tools      | Required for magical parts   |

---

## 11. Creature Summary Table

### All Creatures Quick Reference

| Creature                | Environment         | Social      | Aggression  | Magic         | Rarity    |
| ----------------------- | ------------------- | ----------- | ----------- | ------------- | --------- |
| Deer                    | Forest, Plains      | Herd        | Passive     | None          | Common    |
| Moonlit Stag            | Forest (night)      | Herd        | Passive     | Innate        | Rare      |
| Rabbit                  | Forest, Plains      | Independent | Passive     | None          | Common    |
| Blink Hare              | Forest              | Independent | Passive     | Innate        | Uncommon  |
| Wild Boar               | Forest, Swamp       | Pack        | Defensive   | None          | Common    |
| Ironhide Boar           | Forest              | Pack        | Territorial | Innate        | Rare      |
| Wolf                    | Forest, Tundra      | Pack        | Aggressive  | None          | Common    |
| Dire Wolf               | Forest, Tundra      | Pack/Indep  | Aggressive  | None          | Uncommon  |
| Phantom Wolf            | Forest (night)      | Pack        | Hostile     | Innate        | Rare      |
| Bear                    | Forest, Mountains   | Independent | Defensive   | None          | Common    |
| Stonebear               | Mountains, Caves    | Independent | Territorial | Innate        | Rare      |
| Spirit Bear             | Ancient Forest      | Independent | Passive     | Innate        | Legendary |
| Mountain Lion           | Mountains, Forest   | Independent | Aggressive  | None          | Common    |
| Shadowcat               | Forest (night)      | Independent | Aggressive  | Innate        | Rare      |
| Snake                   | All                 | Independent | Defensive   | None          | Common    |
| Ashviper                | Volcanic, Desert    | Independent | Defensive   | Innate        | Uncommon  |
| Basilisk                | Swamp, Caves        | Independent | Aggressive  | Innate        | Rare      |
| Crocodile               | Swamp, Rivers       | Independent | Aggressive  | None          | Common    |
| Deathroll Croc          | Swamp               | Independent | Aggressive  | Innate        | Uncommon  |
| Eagle                   | Mountains, Coastal  | Independent | Defensive   | None          | Common    |
| Thunderhawk             | Mountains, Storms   | Independent | Defensive   | Innate        | Rare      |
| Crow                    | All                 | Pack        | Passive     | None          | Common    |
| Omen Raven              | All                 | Independent | Passive     | Innate        | Uncommon  |
| Giant Spider            | Forest, Caves       | Independent | Aggressive  | None          | Common    |
| Phase Spider            | Caves, Ruins        | Independent | Aggressive  | Innate        | Rare      |
| Broodmother             | Caves               | Swarm       | Hostile     | Innate        | Legendary |
| Giant Scorpion          | Desert, Caves       | Independent | Territorial | None          | Common    |
| Crystal Scorpion        | Desert, Caves       | Independent | Territorial | Innate        | Rare      |
| Fire Elemental          | Volcanic            | Independent | Territorial | Innate        | Uncommon  |
| Inferno Elemental       | Volcanic            | Independent | Hostile     | Innate        | Rare      |
| Zombie                  | Graveyards, Ruins   | Swarm/Indep | Hostile     | None          | Common    |
| Plague Zombie           | Swamp, Ruins        | Swarm       | Hostile     | Innate        | Uncommon  |
| Skeleton                | Dungeons, Ruins     | Pack        | Hostile     | None          | Common    |
| Skeletal Mage           | Dungeons            | Independent | Hostile     | Mana          | Uncommon  |
| Ghost                   | Ruins, Haunted      | Independent | Territorial | Innate        | Uncommon  |
| Wraith                  | Ruins               | Independent | Hostile     | Innate        | Rare      |
| Sprite                  | Forest (magical)    | Swarm       | Territorial | Innate        | Uncommon  |
| Faerie Dragon           | Forest (magical)    | Independent | Territorial | Innate        | Rare      |
| Treant                  | Ancient Forest      | Independent | Defensive   | Innate        | Rare      |
| Hill Giant              | Hills, Plains       | Pack        | Aggressive  | None          | Uncommon  |
| Stone Giant             | Mountains, Caves    | Pack        | Territorial | Innate        | Uncommon  |
| Frost Giant             | Tundra, Mountains   | Pack        | Hostile     | Innate+Tribal | Rare      |
| Imp                     | Volcanic, Summoned  | Pack        | Aggressive  | Mana          | Common    |
| Harpy                   | Mountains, Coastal  | Pack        | Aggressive  | Mana          | Uncommon  |
| Goblin Shaman           | With tribes         | Pack        | Aggressive  | Tribal        | Uncommon  |
| Orc War Shaman          | With tribes         | Pack        | Aggressive  | Tribal        | Uncommon  |
| Giant Crab              | Coastal             | Independent | Defensive   | None          | Common    |
| Shark                   | Coastal, Ocean      | Independent | Aggressive  | None          | Common    |
| Storm Shark             | Ocean               | Independent | Aggressive  | Innate        | Rare      |
| Human Brute             | Corrupted areas     | Pack/Indep  | Hostile     | None/Innate   | Uncommon  |
| Void-Touched Human      | Corrupted areas     | Independent | Hostile     | Innate        | Rare      |
| Blighted Elf            | Corrupted forest    | Pack        | Hostile     | Innate        | Rare      |
| Stone Husk (Dwarf)      | Deep caves          | Independent | Hostile     | Innate        | Rare      |
| Razorscale (Lizardfolk) | Corrupted swamp     | Pack        | Aggressive  | Innate        | Rare      |
| Ravager (Gnoll)         | Corrupted wilds     | Pack        | Hostile     | Tribal        | Rare      |
| Plague Goblin           | Corrupted areas     | Swarm       | Aggressive  | Tribal        | Uncommon  |
| Doom Orc                | Corrupted areas     | Pack        | Hostile     | Tribal        | Rare      |
| Tunnel Horror (Kobold)  | Corrupted warrens   | Swarm       | Territorial | Innate        | Uncommon  |
| Horse                   | Settlements, Plains | Herd        | Passive     | None          | Common    |
| Nightmare               | Planar              | Independent | Hostile     | Innate        | Rare      |
| Cattle                  | Settlements         | Herd        | Passive     | None          | Common    |
| Sheep                   | Settlements         | Herd        | Passive     | None          | Common    |
| Cloudwool Sheep         | Settlements (rare)  | Herd        | Passive     | Innate        | Uncommon  |
| Chicken                 | Settlements         | Herd        | Passive     | None          | Common    |
| Pig                     | Settlements         | Herd        | Passive     | None          | Common    |
| Dragon                  | Mountains, Volcanic | Independent | Territorial | Innate+Mana   | Legendary |
| Phoenix                 | Volcanic, Sacred    | Independent | Passive     | Innate        | Legendary |
| Hydra                   | Swamp, Coastal      | Independent | Hostile     | Innate        | Legendary |
| Behemoth                | Remote wilds        | Independent | Territorial | Innate        | Legendary |
| Kraken                  | Deep ocean          | Independent | Aggressive  | Innate        | Legendary |

---

## 12. Ecology & Respawn

### Population Dynamics

Creatures follow ecological rules that affect their presence in an area.

| Factor             | Effect                                    |
| ------------------ | ----------------------------------------- |
| Overhunting        | Reduces spawn rate, may eliminate locally |
| Predator removal   | Prey populations boom temporarily         |
| Corruption spread  | Normal creatures flee, corrupted spawn    |
| Seasonal migration | Some herds move between regions           |
| Breeding seasons   | Increased young, more territorial adults  |

### Respawn Rates

| Rarity    | Respawn Time | Notes                        |
| --------- | ------------ | ---------------------------- |
| Common    | 1-3 days     | Stable populations           |
| Uncommon  | 3-7 days     | Slower recovery              |
| Rare      | 1-4 weeks    | May require conditions       |
| Legendary | Unique/Never | Quest triggers, world events |

### Extinction Risk

| Warning Sign  | Effect                       |
| ------------- | ---------------------------- |
| Sparse spawns | Population stressed          |
| No young      | Population declining         |
| No spawns     | Locally extinct              |
| Recovery      | Stop hunting, wait 2-4 weeks |

### Corrupted Area Mechanics

| Stage            | Effect                                |
| ---------------- | ------------------------------------- |
| Early corruption | Normal creatures become aggressive    |
| Spreading        | Magical variants appear, normals flee |
| Established      | Brutes spawn, normal creatures gone   |
| Deep corruption  | Void creatures, elemental corruption  |

---

## 13. Taming & Capture

### Tameable Creatures

| Creature              | Difficulty | Method            | Uses              |
| --------------------- | ---------- | ----------------- | ----------------- |
| Horse                 | Moderate   | Break/Bond        | Mount, labor      |
| Wolf                  | Hard       | Raise from pup    | Combat companion  |
| Bear                  | Very Hard  | Raise from cub    | Combat, labor     |
| Eagle                 | Hard       | Falconry training | Hunting, scouting |
| Dog (wolf descendant) | Easy       | Domesticated      | Hunting, guarding |
| Cat                   | Easy       | Semi-domesticated | Pest control      |

### Capture Methods

| Method          | Creatures         | Requirements             |
| --------------- | ----------------- | ------------------------ |
| Live trap       | Small creatures   | Trapping skill, bait     |
| Net             | Medium creatures  | Net, Athletics           |
| Lasso           | Large creatures   | Rope, skill              |
| Cage            | Various           | Cage appropriate to size |
| Magical binding | Magical creatures | Enchanted restraints     |

### Taming Skill Effects

| Level      | Bonus           | New Abilities            |
| ---------- | --------------- | ------------------------ |
| None       | Cannot tame     | -                        |
| Apprentice | Basic creatures | Simple commands          |
| Journeyman | +20% success    | Combat commands          |
| Master     | +40% success    | Magical creature attempt |

---

## 14. Part Quality & Preservation

### Quality Degradation

| Condition           | Effect              |
| ------------------- | ------------------- |
| Fresh (0-2 hours)   | Full quality        |
| Recent (2-8 hours)  | -1 tier if organic  |
| Old (8-24 hours)    | -2 tiers if organic |
| Spoiled (24+ hours) | Unusable (organic)  |

### Preservation Methods

| Method               | Duration Extension | Materials                 |
| -------------------- | ------------------ | ------------------------- |
| Ice/cold             | +24 hours          | Ice, cold storage         |
| Salt                 | +1 week            | Salt                      |
| Smoking              | +2 weeks           | Smoke, time               |
| Magical preservation | Indefinite         | Preservation spell/scroll |
| Alchemy jar          | +1 month           | Alchemical container      |

### Non-Organic Parts

| Part Type       | Degradation              |
| --------------- | ------------------------ |
| Bones           | None                     |
| Scales/Chitin   | None                     |
| Teeth/Claws     | None                     |
| Magical essence | Slow (1 week to degrade) |
| Elemental cores | Very slow (1 month)      |

---

## 15. Hunting Contracts & Bounties

### Standard Contracts

| Target                            | Difficulty | Typical Reward             |
| --------------------------------- | ---------- | -------------------------- |
| Pest control (rats, etc.)         | Easy       | 5-15 silver                |
| Predator (wolf, lion)             | Moderate   | 50-150 silver              |
| Monster (giant spider, etc.)      | Hard       | 1-5 gold                   |
| Dangerous beast (bear, dire wolf) | Hard       | 2-8 gold                   |
| Magical creature                  | Very Hard  | 10-50 gold                 |
| Legendary                         | Extreme    | 100+ gold, special rewards |

### Bounty Sources

| Source             | Target Types                       |
| ------------------ | ---------------------------------- |
| Settlements        | Local threats                      |
| Adventurer's Guild | Monsters, dangerous beasts         |
| Hunter's Guild     | Specific creatures, rare materials |
| Private contracts  | Special requests                   |

### Proof Requirements

| Target       | Required Proof              |
| ------------ | --------------------------- |
| Common beast | Specific part (head, pelt)  |
| Monster      | Head or identifying part    |
| Corrupted    | Corruption node             |
| Legendary    | Specific trophy + witnesses |
