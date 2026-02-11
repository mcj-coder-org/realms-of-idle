<!-- ADAPTATION REQUIRED -->
<!-- This file was migrated from source but needs manual review: -->
<!-- - Update terminology (dormant classes, XP split, etc.) -->
<!-- - Align with current GDD architecture -->
<!-- - Add missing sections as needed -->
<!-- - Update frontmatter with correct gdd_ref -->

---

title: Class-to-Tag Associations
type: system
summary: Complete mapping of all character classes to hierarchical skill tags

---

# Class-to-Tag Associations

## 1. Overview

This document maps all 40+ character classes to their associated hierarchical skill tags. These tags determine which skills receive synergy bonuses when that class is active.

### Class Tier System

Classes are organized into three tiers (Foundation → Tier 2 → Tier 3) that determine tag depth access and XP scaling. See [Class Tiers](./class-tiers.md) for complete tier documentation including XP formulas and thresholds.

### How Tags Work for Classes

**Tag Inheritance:**

- Classes with specific tags grant synergy bonuses to skills with matching tags OR parent tags
- Example: Weaponsmith (`Crafting/Smithing/Weapon`) grants bonuses to skills tagged `Crafting`, `Crafting/Smithing`, or `Crafting/Smithing/Weapon`

**Synergy Benefits:**

- **2x XP gain** from relevant actions (scales to 2.5x at high class levels)
- **+15-35% effectiveness** (scales with class level)
- **Reduced costs** (stamina, mana, resources)

**Multiple Tags:**

- Classes can have multiple tags from different categories
- Each tag provides independent synergy bonuses to matching skills
- Enables flexible, hybrid builds

---

## 2. Combat Classes

### 2.1 Warrior

**Tier:** 2 | **Prerequisite:** 5,000 XP from combat actions | **Tag Depth:** 2

**Class Identity:** Melee combat specialist skilled in direct confrontation

**Tags:**

- `Combat/Melee`
- `Combat`
- `Physical`
- `Physical/Strength`

**Skill Access:**

- All Combat skills (24 total)
- Emphasis on melee combat skills
- Physical prowess abilities
- Strength-based actions

**Example Synergies:**

- Weapon Mastery (Combat/Melee): Strong synergy
- Battle Rage (Combat/Melee): Strong synergy
- Armor Expert (Combat/Melee, Combat/Defense): Strong synergy
- Shield Wall (Combat/Melee, Combat/Defense): Strong synergy

---

### 2.2 Knight

**Tier:** 3 | **Prerequisite:** 50,000 XP from honorable combat and leadership actions | **Tag Depth:** 3+

**Class Identity:** Honorable warrior with combat and leadership abilities

**Tags:**

- `Combat/Melee`
- `Combat/Tactics`
- `Leadership/Command`
- `Physical`
- `Social/Persuasion`

**Skill Access:**

- Combat melee skills
- Tactical command abilities
- Leadership and military command
- Social influence

**Example Synergies:**

- Commander's Voice (Combat/Tactics, Leadership/Command): Strong synergy
- Mounted Combat (Combat/Mounted): Moderate synergy
- Authority (Leadership/Command): Strong synergy
- Inspiring Leader (Leadership/Social): Strong synergy

**Notes:**
Knight is a consolidated class (Warrior + honor/service focus), gaining additional Leadership tags.

---

### 2.3 Archer

**Tier:** 2 | **Prerequisite:** 5,000 XP from ranged combat actions | **Tag Depth:** 2

**Class Identity:** Ranged combat specialist with bow mastery

**Tags:**

- `Combat/Ranged`
- `Combat`
- `Physical/Agility`
- `Gathering/Hunting` (tracking prey)

**Skill Access:**

- Ranged combat skills
- Bow and crossbow abilities
- Agility-based actions
- Hunting and tracking

**Example Synergies:**

- Weapon Mastery (Combat/Ranged): Strong synergy
- Precise Shot (Combat/Ranged): Strong synergy
- Tracking (Gathering/Hunting): Moderate synergy

---

### 2.4 Scout

**Tier:** 2 | **Prerequisite:** 5,000 XP from reconnaissance actions | **Tag Depth:** 2

**Class Identity:** Reconnaissance specialist combining stealth and combat

**Tags:**

- `Combat`
- `Stealth`
- `Stealth/Infiltration`
- `Physical/Agility`
- `Gathering` (tracking, scouting)

**Skill Access:**

- Combat skills (moderate access)
- Stealth and infiltration
- Agility-based movement
- Tracking and scouting

**Example Synergies:**

- Shadow Step (Stealth): Strong synergy
- Lockmaster (Stealth/Infiltration): Strong synergy
- Tactical Assessment (Combat/Tactics): Moderate synergy

---

### 2.5 Guard

**Tier:** 2 | **Prerequisite:** 5,000 XP from defensive combat actions | **Tag Depth:** 2

**Class Identity:** Defensive protector and law enforcement

**Tags:**

- `Combat/Defense`
- `Combat/Melee`
- `Leadership/Command`
- `Physical/Endurance`
- `Social/Persuasion` (authority figure)

**Skill Access:**

- Defensive combat skills
- Protective abilities
- Command and authority
- Endurance-based actions

**Example Synergies:**

- Shield Wall (Combat/Melee, Combat/Defense): Strong synergy
- Armor Expert (Combat/Melee, Combat/Defense): Strong synergy
- Authority (Leadership/Command): Strong synergy

---

### 2.6 Assassin

**Tier:** 2 | **Prerequisite:** 5,000 XP from stealth and assassination actions | **Tag Depth:** 2

**Class Identity:** Stealth killer specializing in precision elimination

**Tags:**

- `Stealth/Assassination`
- `Combat/Assassination`
- `Criminal/Assassination`
- `Physical/Agility`
- `Crafting/Alchemy/Poison` (poison use)

**Skill Access:**

- Assassination skills (all three categories)
- Stealth abilities
- Criminal underground
- Poison crafting and use

**Example Synergies:**

- Silent Kill (Stealth/Assassination, Combat/Assassination, Criminal/Assassination): Strong synergy (all 3 tags match!)
- Backstab (Stealth/Assassination, Combat/Assassination): Strong synergy
- Poison Craft (Stealth/Assassination, Crafting/Alchemy/Poison, Criminal/Assassination): Strong synergy
- Shadow Step (Stealth): Strong synergy

**Notes:**
Assassin is the ONLY class with all three assassination tags, making them supreme masters of killing from shadows.

---

### 2.7 Adventurer

**Tier:** 2 | **Prerequisite:** 5,000 XP from exploration and dungeon actions | **Tag Depth:** 2

**Class Identity:** Generalist explorer and survivalist

**Tags:**

- `Combat`
- `Gathering`
- `Physical`
- `Knowledge` (exploration knowledge)

**Skill Access:**

- General combat skills
- Gathering and survival
- Physical abilities
- Exploration knowledge

**Example Synergies:**

- Varied access to many skill categories
- Jack-of-all-trades build
- Good base for multiclassing

---

## 3. Crafting Classes

### 3.1 Blacksmith

**Tier:** 2 | **Prerequisite:** 5,000 XP from smithing actions | **Tag Depth:** 2

**Class Identity:** Master of metal, forging weapons, armor, and tools

**Tags:**

- `Crafting/Smithing`
- `Crafting`
- `Physical/Strength`

**Skill Access:**

- All smithing skills (5 specialized)
- General crafting skills (9 universal)
- Strength-based actions

**Example Synergies:**

- Metal Sense (Crafting/Smithing): Strong synergy
- Forge Fire (Crafting/Smithing): Strong synergy
- Perfect Temper (Crafting/Smithing): Strong synergy
- Material Intuition (Crafting): Strong synergy
- Artisan's Focus (Crafting): Strong synergy

**Specialization Path:**

- Weaponsmith: Add `Crafting/Smithing/Weapon` tag
- Armorsmith: Add `Crafting/Smithing/Armor` tag

---

### 3.2 Weaponsmith

**Tier:** 3 | **Prerequisite:** 50,000 XP from weapon forging actions | **Tag Depth:** 3+

**Class Identity:** Specialized in crafting weapons

**Tags:**

- `Crafting/Smithing/Weapon`
- `Crafting/Smithing`
- `Crafting`
- `Physical/Strength`
- `Combat/Melee` (weapon understanding)

**Skill Access:**

- All weapon smithing skills
- All smithing skills
- All general crafting skills
- Weapon knowledge

**Example Synergies:**

- Weapon Enhancement (Crafting/Smithing/Weapon): Strong synergy
- All Blacksmith synergies
- Plus weapon-specific techniques

**Notes:**
Consolidated class (Blacksmith + weapon specialization focus).

---

### 3.3 Armorsmith

**Tier:** 3 | **Prerequisite:** 50,000 XP from armor forging actions | **Tag Depth:** 3+

**Class Identity:** Specialized in crafting armor

**Tags:**

- `Crafting/Smithing/Armor`
- `Crafting/Smithing`
- `Crafting`
- `Physical/Strength`
- `Combat/Defense` (armor understanding)

**Skill Access:**

- All armor smithing skills
- All smithing skills
- All general crafting skills
- Defensive knowledge

**Example Synergies:**

- Armor Fitting (Crafting/Smithing/Armor): Strong synergy
- All Blacksmith synergies
- Plus armor-specific techniques

**Notes:**
Consolidated class (Blacksmith + armor specialization focus).

---

### 3.4 Alchemist

**Tier:** 2 | **Prerequisite:** 5,000 XP from alchemy actions | **Tag Depth:** 2

**Class Identity:** Potion crafter and chemical experimenter

**Tags:**

- `Crafting/Alchemy`
- `Crafting`
- `Knowledge` (chemical understanding)
- `Gathering/Herbalism` (reagent gathering)

**Skill Access:**

- All alchemy skills (11 total)
- General crafting skills
- Chemical and magical knowledge
- Herb gathering

**Example Synergies:**

- Potent Brews (Crafting/Alchemy/Potion): Strong synergy
- Stable Solutions (Crafting/Alchemy/Potion): Strong synergy
- Transmutation Basics (Crafting/Alchemy): Strong synergy
- Reagent Cultivation (Crafting/Alchemy): Strong synergy

**Specialization Path:**

- Poisoner: Add `Crafting/Alchemy/Poison` and `Criminal/Assassination` tags

---

### 3.5 Carpenter

**Tier:** 2 | **Prerequisite:** 5,000 XP from woodworking actions | **Tag Depth:** 2

**Class Identity:** Wood crafting specialist

**Tags:**

- `Crafting/Woodworking`
- `Crafting`
- `Physical/Strength`
- `Gathering/Lumbering` (wood selection)

**Skill Access:**

- Woodworking skills
- General crafting skills
- Strength-based actions
- Wood gathering knowledge

**Example Synergies:**

- All general Crafting skills
- Woodworking-specific skills
- Wood material knowledge

---

### 3.6 Tailor

**Tier:** 2 | **Prerequisite:** 5,000 XP from tailoring actions | **Tag Depth:** 2

**Class Identity:** Cloth and leather clothing crafter

**Tags:**

- `Crafting/Tailoring`
- `Crafting`
- `Physical/Finesse` (fine detail work)
- `Social` (fashion awareness)

**Skill Access:**

- Tailoring skills
- General crafting skills
- Fine detail work
- Fashion and style

**Example Synergies:**

- All general Crafting skills
- Tailoring-specific skills
- Social presentation

---

### 3.7 Leatherworker

**Tier:** 2 | **Prerequisite:** 5,000 XP from leatherworking actions | **Tag Depth:** 2

**Class Identity:** Leather goods crafter

**Tags:**

- `Crafting/Leatherworking`
- `Crafting`
- `Gathering/Hunting` (leather source knowledge)

**Skill Access:**

- Leatherworking skills
- General crafting skills
- Animal material knowledge

**Example Synergies:**

- All general Crafting skills
- Leatherworking-specific skills
- Field dressing and leather preparation

---

### 3.8 Cook

**Tier:** 2 | **Prerequisite:** 5,000 XP from cooking actions | **Tag Depth:** 2

**Class Identity:** Food preparation specialist

**Tags:**

- `Crafting/Cooking`
- `Crafting`
- `Agriculture` (ingredient knowledge)
- `Gathering/Herbalism` (spice and herb knowledge)

**Skill Access:**

- Cooking skills
- General crafting skills
- Food and ingredient knowledge
- Herb gathering for spices

**Example Synergies:**

- All general Crafting skills
- Cooking-specific skills
- Ingredient quality assessment

---

### 3.9 Jeweler

**Tier:** 2 | **Prerequisite:** 5,000 XP from jewelcrafting actions | **Tag Depth:** 2

**Class Identity:** Gem cutting and jewelry crafting specialist

**Tags:**

- `Crafting/Jewelcrafting`
- `Crafting`
- `Physical/Finesse` (precision work)
- `Gathering/Mining` (gem knowledge)
- `Trade` (high-value goods)

**Skill Access:**

- Jewelcrafting skills
- General crafting skills
- Fine precision work
- Gem and precious metal knowledge
- Trade and appraisal

**Example Synergies:**

- All general Crafting skills
- Jewelcrafting-specific skills
- Gem quality assessment
- Trade and valuation

---

## 4. Gathering Classes

### 4.1 Farmer

**Tier:** 2 | **Prerequisite:** 5,000 XP from agriculture actions | **Tag Depth:** 2

**Class Identity:** Crop cultivation and livestock management

**Tags:**

- `Agriculture`
- `Agriculture/Crops`
- `Agriculture/Livestock`
- `Nature`
- `Physical/Endurance`

**Skill Access:**

- All agriculture skills (10 total)
- Nature understanding
- Endurance-based work
- Animal and plant knowledge

**Example Synergies:**

- Green Thumb (Agriculture/Crops): Strong synergy
- Animal Husbandry (Agriculture/Livestock): Strong synergy
- Season Sense (Agriculture, Nature): Strong synergy
- Surplus Generator (Agriculture): Strong synergy

---

### 4.2 Miner

**Tier:** 2 | **Prerequisite:** 5,000 XP from mining actions | **Tag Depth:** 2

**Class Identity:** Ore and mineral extraction specialist

**Tags:**

- `Gathering/Mining`
- `Gathering`
- `Physical/Strength`
- `Physical/Endurance`
- `Knowledge` (geology)

**Skill Access:**

- Mining skills (3 specialized)
- General gathering skills
- Physical labor
- Geological knowledge

**Example Synergies:**

- Deep Vein (Gathering/Mining): Strong synergy
- Prospector (Gathering/Mining): Strong synergy
- Resource Sense (Gathering): Strong synergy
- Bountiful Harvest (Gathering): Strong synergy

---

### 4.3 Hunter

**Tier:** 2 | **Prerequisite:** 5,000 XP from hunting actions | **Tag Depth:** 2

**Class Identity:** Animal tracking and hunting specialist

**Tags:**

- `Gathering/Hunting`
- `Gathering`
- `Nature/Animals`
- `Combat/Ranged` (hunting weapons)
- `Physical/Agility`

**Skill Access:**

- Hunting skills (5 specialized)
- General gathering skills
- Animal knowledge
- Ranged combat
- Agility-based movement

**Example Synergies:**

- Creature Lore (Nature/Animals, Gathering/Hunting, Knowledge): Strong synergy
- Hunting Companion (Nature/Animals, Gathering/Hunting): Strong synergy
- Scent Mastery (Nature/Animals, Gathering/Hunting): Strong synergy
- Tracking (Gathering/Hunting): Strong synergy

---

### 4.4 Forager

**Tier:** 2 | **Prerequisite:** 5,000 XP from foraging actions | **Tag Depth:** 2

**Class Identity:** Wild plant and herb gathering specialist

**Tags:**

- `Gathering/Herbalism`
- `Gathering`
- `Nature`
- `Knowledge` (plant identification)
- `Physical/Awareness`

**Skill Access:**

- Herbalism skills (2 specialized)
- General gathering skills
- Nature knowledge
- Plant identification

**Example Synergies:**

- Gentle Harvest (Gathering/Herbalism): Strong synergy
- Master Forager (Gathering, Gathering/Herbalism): Strong synergy
- Resource Sense (Gathering): Strong synergy

---

### 4.5 Fisher

**Tier:** 2 | **Prerequisite:** 5,000 XP from fishing actions | **Tag Depth:** 2

**Class Identity:** Aquatic resource gathering specialist

**Tags:**

- `Gathering/Fishing`
- `Gathering`
- `Nature` (aquatic ecosystems)
- `Physical/Patience`

**Skill Access:**

- Fishing skills (3 specialized)
- General gathering skills
- Water and weather knowledge
- Patience-based actions

**Example Synergies:**

- Resource Sense (Gathering/Fishing): Strong synergy
- Weather Worker (Gathering): Strong synergy
- Deep Vein (Gathering/Mining - underwater nodes): Moderate synergy

---

### 4.6 Lumberjack

**Tier:** 2 | **Prerequisite:** 5,000 XP from lumbering actions | **Tag Depth:** 2

**Class Identity:** Tree harvesting specialist

**Tags:**

- `Gathering/Lumbering`
- `Gathering`
- `Physical/Strength`
- `Physical/Endurance`

**Skill Access:**

- Lumbering skills (3 specialized)
- General gathering skills
- Physical labor

**Example Synergies:**

- Resource Sense (Gathering/Lumbering): Strong synergy
- Weather Worker (Gathering): Strong synergy
- Efficiency Expert (Gathering): Strong synergy

---

## 5. Magic Classes

### 5.1 Mage

**Tier:** 2 | **Prerequisite:** 5,000 XP from spellcasting actions | **Tag Depth:** 2

**Class Identity:** Generalist spellcaster with broad access to all magical disciplines

**Tags:**

- `Magic`
- `Magic/Arcane`
- `Magic/Elemental`
- `Magic/Conjuration`
- `Magic/Illusion`
- `Knowledge`
- `Knowledge/Learning`

**Skill Access:**

- All depth-1 and depth-2 magic tags
- Basic spells from all schools at reduced power
- Arcane manipulation and counterspells
- Magical knowledge and research

**Example Synergies:**

- Spell Focus (Magic): Strong synergy
- Mana Well (Magic): Strong synergy
- Counter Magic (Magic/Arcane): Strong synergy
- Spell Weaving (Magic/Arcane): Strong synergy
- Basic elemental spells (Magic/Elemental): Strong synergy

**Progression Paths:**

- Specializations: Pyromancer, Cryomancer, Electromancer, Geomancer, Aeromancer, Hydromancer, Necromancer, Summoner, Illusionist
- Consolidations: Battlemage (+ Warrior), Enchanter (+ Crafter), Healer (+ Herbalist)

---

### 5.2 Healer

**Tier:** 3 | **Prerequisite:** Mage + 50,000 XP from healing magic actions | **Tag Depth:** 3+

**Class Identity:** Master healer and restoration magic specialist

**Tags:**

- `Magic/Healing`
- `Magic`
- `Knowledge` (anatomy, medicine)
- `Social` (bedside manner)
- `Gathering/Herbalism` (healing herbs)

**Skill Access:**

- Advanced healing magic
- General magic skills
- Medical knowledge
- Patient care
- Herbal remedies

**Example Synergies:**

- School Mastery (Magic/Healing): Strong synergy
- All general Magic skills: Strong synergy
- Spell Focus: Strong synergy
- Herbalism (Gathering/Herbalism): Moderate synergy

**Notes:**

Specialized class evolved from Mage with focus on healing and restoration.

---

### 5.3 Battlemage

**Tier:** 3 | **Prerequisite:** 50,000 XP from combat spellcasting actions | **Tag Depth:** 3+

**Class Identity:** Combat-focused spellcaster combining magic and warfare

**Tags:**

- `Magic/Elemental`
- `Magic/Arcane`
- `Magic`
- `Combat`
- `Physical`

**Skill Access:**

- Elemental combat magic
- Arcane manipulation
- General magic
- Combat abilities
- Physical resilience

**Example Synergies:**

- All magic skills: Strong synergy
- Combat skills: Strong synergy
- Hybrid build synergies

**Notes:**
Consolidated class (Mage + Warrior/combat focus).

---

### 5.4 Enchanter

**Tier:** 2 | **Prerequisite:** 5,000 XP from enchanting actions | **Tag Depth:** 2

**Class Identity:** Item enchantment specialist

**Tags:**

- `Magic/Enchantment`
- `Magic`
- `Crafting` (item understanding)
- `Knowledge`

**Skill Access:**

- Enchantment magic
- General magic skills
- Crafting knowledge
- Magical theory

**Example Synergies:**

- Enchant Object (Magic/Enchantment): Strong synergy
- All general Magic skills: Strong synergy
- Crafting skills: Moderate synergy

**Notes:**
Consolidated class (Mage + Blacksmith/item focus or Mage + enchanting specialization).

---

### 5.5 Necromancer

**Tier:** 2 | **Prerequisite:** 5,000 XP from necromancy actions | **Tag Depth:** 2

**Class Identity:** Death magic and undead master

**Tags:**

- `Magic/Necromancy`
- `Magic`
- `Criminal` (forbidden magic)
- `Knowledge` (death and undeath)

**Skill Access:**

- Death magic
- Undead creation and control
- General magic
- Dark knowledge

**Example Synergies:**

- Undead Binding (Magic/Necromancy): Strong synergy
- School Mastery (Magic/Necromancy): Strong synergy
- All general Magic skills: Strong synergy
- Dark magic skills: Strong synergy

**Notes:**
Dark Path class. Often consolidated from Mage + necromancy focus.

---

### 5.6 Summoner

**Tier:** 3 | **Prerequisite:** Mage + 50,000 XP from summoning actions | **Tag Depth:** 3+

**Class Identity:** Master summoner commanding supernatural entities

**Tags:**

- `Magic/Summoning`
- `Magic`
- `Nature/Animals` (creature understanding)
- `Knowledge`

**Skill Access:**

- Advanced summoning magic
- General magic
- Creature knowledge
- Magical theory
- Entity binding and control

**Example Synergies:**

- Summoning skills: Strong synergy
- Creature Lore: Strong synergy
- All general Magic skills: Strong synergy

**Notes:**

Specialized class evolved from Mage with focus on summoning and entity mastery.

---

### 5.7 Illusionist

**Tier:** 3 | **Prerequisite:** Mage + 50,000 XP from illusion magic actions | **Tag Depth:** 3+

**Class Identity:** Master of illusion and perception manipulation

**Tags:**

- `Magic/Illusion`
- `Magic`
- `Social/Deception`
- `Knowledge`

**Skill Access:**

- Advanced illusion magic
- General magic
- Deception and misdirection
- Magical theory
- Complex perception alteration

**Example Synergies:**

- Illusion skills: Strong synergy
- Misdirection: Strong synergy
- All general Magic skills: Strong synergy
- Social deception: Moderate synergy

**Notes:**

Specialized class evolved from Mage with focus on illusion and deception.

---

## 6. Trade Classes

### 6.1 Trader

**Tier:** 2 | **Prerequisite:** 5,000 XP from trading actions | **Tag Depth:** 2

**Class Identity:** Basic commerce and bartering specialist

**Tags:**

- `Trade`
- `Trade/Negotiation`
- `Social/Persuasion`
- `Knowledge` (market knowledge)

**Skill Access:**

- Basic trade skills
- Negotiation
- Social persuasion
- Market knowledge

**Example Synergies:**

- Market Sense (Trade, Economy): Strong synergy
- Silver Tongue (Trade/Negotiation, Social/Persuasion): Strong synergy
- Trade Contacts (Trade): Strong synergy

---

### 6.2 Merchant

**Tier:** 2 | **Prerequisite:** 5,000 XP from merchant actions | **Tag Depth:** 2

**Class Identity:** Advanced commerce and business specialist

**Tags:**

- `Trade`
- `Trade/Negotiation`
- `Trade/Speculation`
- `Economy`
- `Social/Persuasion`
- `Leadership/Management`

**Skill Access:**

- All trade skills
- Market speculation
- Business management
- Social influence

**Example Synergies:**

- All trade skills: Strong synergy
- Commodity Speculation (Trade/Speculation, Economy/Speculation): Strong synergy
- Authority (Leadership): Moderate synergy

**Notes:**
Advanced version of Trader with broader business skills.

---

### 6.3 Caravaner

**Tier:** 2 | **Prerequisite:** 5,000 XP from caravan actions | **Tag Depth:** 2

**Class Identity:** Long-distance trade route specialist

**Tags:**

- `Trade/Caravan`
- `Trade`
- `Physical/Endurance` (long journeys)
- `Leadership` (caravan management)
- `Gathering` (route knowledge)

**Skill Access:**

- Caravan management
- Trade skills
- Journey endurance
- Route and navigation

**Example Synergies:**

- Caravan Master (Trade/Caravan): Strong synergy
- All trade skills: Strong synergy
- Endurance-based skills: Moderate synergy

---

### 6.4 Appraiser

**Tier:** 2 | **Prerequisite:** 5,000 XP from appraisal actions | **Tag Depth:** 2

**Class Identity:** Item valuation specialist

**Tags:**

- `Trade`
- `Knowledge`
- `Crafting` (item quality understanding)

**Skill Access:**

- Trade and valuation
- Item knowledge
- Crafting quality assessment

**Example Synergies:**

- Market Sense: Strong synergy
- Material Intuition: Moderate synergy
- Crafting knowledge: Moderate synergy

---

### 6.5 Thief

**Tier:** 2 | **Prerequisite:** 5,000 XP from theft and stealth actions | **Tag Depth:** 2

**Class Identity:** Stealth-based property acquisitioner

**Tags:**

- `Criminal/Theft`
- `Criminal`
- `Stealth`
- `Stealth/Infiltration`
- `Physical/Agility`

**Skill Access:**

- Theft skills
- Criminal abilities
- Stealth and infiltration
- Agility-based movement

**Example Synergies:**

- Lockmaster (Stealth/Infiltration, Criminal/Theft): Strong synergy
- Shadow Step (Stealth): Strong synergy
- Evidence Elimination (Criminal): Strong synergy
- False Identity (Criminal, Social/Deception): Strong synergy

**Notes:**
Dark Path class.

---

### 6.6 Fence

**Tier:** 2 | **Prerequisite:** 5,000 XP from illegal trade actions | **Tag Depth:** 2

**Class Identity:** Stolen goods broker

**Tags:**

- `Trade/Illegal`
- `Trade`
- `Criminal/Finance`
- `Criminal`
- `Social/Deception`

**Skill Access:**

- Black market trade
- Criminal finance
- Deception and cover
- Trade skills

**Example Synergies:**

- Fencing (Trade/Illegal, Criminal/Finance): Strong synergy
- All trade skills: Strong synergy
- Criminal network: Strong synergy
- False Identity: Strong synergy

**Notes:**
Dark Path class.

---

### 6.7 Smuggler

**Tier:** 2 | **Prerequisite:** 5,000 XP from smuggling actions | **Tag Depth:** 2

**Class Identity:** Illegal goods transportation specialist

**Tags:**

- `Trade/Illegal`
- `Trade`
- `Criminal`
- `Stealth/Infiltration`
- `Physical/Agility`

**Skill Access:**

- Smuggling skills
- Illegal trade
- Criminal network
- Infiltration and evasion

**Example Synergies:**

- Smuggler's Instinct (Stealth/Infiltration): Strong synergy
- Fencing (Trade/Illegal): Strong synergy
- Shadow Step: Moderate synergy

**Notes:**
Dark Path class.

---

## 7. Specialized and Consolidated Classes

### 7.1 Druid

**Tier:** 2 | **Prerequisite:** 5,000 XP from nature magic actions | **Tag Depth:** 2

**Class Identity:** Nature magic and wild shaping specialist

**Tags:**

- `Magic/Shamanic`
- `Magic`
- `Nature`
- `Nature/Animals`
- `Nature/Spirits`

**Skill Access:**

- Shamanic magic
- Nature magic
- Animal communication
- Spirit interaction

**Example Synergies:**

- Spirit Binding (Magic/Shamanic, Tribal/Spirits): Strong synergy
- Beast Tongue (Nature/Animals): Strong synergy
- All nature skills: Strong synergy

---

### 7.2 Spiritcaller

**Tier:** 1 | **Prerequisite:** 100 spirit-related actions | **Tag Depth:** 1

**Class Identity:** Foundation shamanic class with basic spirit communion

**Tags:**

- `Shamanic`

**Progression Paths:**

- Shaman (Tier 2)

---

### 7.3 Shaman

**Tier:** 2 | **Prerequisite:** 5,000 XP from shamanic actions | **Tag Depth:** 2

**Class Identity:** Tribal spirit magic practitioner with herbalism and charm-making

**Tags:**

- `Shamanic`
- `Gathering/Herbalism`
- `Shamanic/Nature`

**Progression Paths:**

- Specializations: Flame Keeper, Frost Shaman, Storm Caller, Earth Shaman, Wind Walker, Tide Caller, Ancestor Shaman, Beast Speaker, Spirit Walker
- Consolidations: War Shaman (+ Warrior), Totem Carver (+ Carpenter)

---

---

## 5.1 Tier 3 Mage Specializations

### 5.8 Pyromancer

**Tier:** 3 | **Prerequisite:** Mage + 50,000 XP from fire magic actions | **Tag Depth:** 3+

**Class Identity:** Master of fire and heat magic

**Tags:**

- `Magic/Elemental/Fire`
- `Magic/Elemental`
- `Magic`

**Skill Access:**

- Advanced fire spells
- All elemental magic skills
- All general magic skills
- Flame mastery techniques

**Example Synergies:**

- Inferno (Magic/Elemental/Fire): Strong synergy
- All Elemental spells: Strong synergy
- All general Magic skills: Strong synergy

---

### 5.9 Cryomancer

**Tier:** 3 | **Prerequisite:** Mage + 50,000 XP from frost magic actions | **Tag Depth:** 3+

**Class Identity:** Master of ice and cold magic

**Tags:**

- `Magic/Elemental/Frost`
- `Magic/Elemental`
- `Magic`

**Skill Access:**

- Advanced frost spells
- All elemental magic skills
- All general magic skills
- Cold mastery techniques

**Example Synergies:**

- Blizzard (Magic/Elemental/Frost): Strong synergy
- All Elemental spells: Strong synergy
- All general Magic skills: Strong synergy

---

### 5.10 Electromancer

**Tier:** 3 | **Prerequisite:** Mage + 50,000 XP from lightning magic actions | **Tag Depth:** 3+

**Class Identity:** Master of lightning and electricity

**Tags:**

- `Magic/Elemental/Lightning`
- `Magic/Elemental`
- `Magic`

**Skill Access:**

- Advanced lightning spells
- All elemental magic skills
- All general magic skills
- Electrical mastery techniques

**Example Synergies:**

- Chain Lightning (Magic/Elemental/Lightning): Strong synergy
- All Elemental spells: Strong synergy
- All general Magic skills: Strong synergy

---

### 5.11 Geomancer

**Tier:** 3 | **Prerequisite:** Mage + 50,000 XP from earth magic actions | **Tag Depth:** 3+

**Class Identity:** Master of earth and stone magic

**Tags:**

- `Magic/Elemental/Earth`
- `Magic/Elemental`
- `Magic`

**Skill Access:**

- Advanced earth spells
- All elemental magic skills
- All general magic skills
- Earth mastery techniques

**Example Synergies:**

- Tremor (Magic/Elemental/Earth): Strong synergy
- All Elemental spells: Strong synergy
- All general Magic skills: Strong synergy

---

### 5.12 Aeromancer

**Tier:** 3 | **Prerequisite:** Mage + 50,000 XP from air magic actions | **Tag Depth:** 3+

**Class Identity:** Master of wind and air magic

**Tags:**

- `Magic/Elemental/Air`
- `Magic/Elemental`
- `Magic`

**Skill Access:**

- Advanced air spells
- All elemental magic skills
- All general magic skills
- Air mastery techniques

**Example Synergies:**

- Whirlwind (Magic/Elemental/Air): Strong synergy
- All Elemental spells: Strong synergy
- All general Magic skills: Strong synergy

---

### 5.13 Hydromancer

**Tier:** 3 | **Prerequisite:** Mage + 50,000 XP from water magic actions | **Tag Depth:** 3+

**Class Identity:** Master of water and fluid magic

**Tags:**

- `Magic/Elemental/Water`
- `Magic/Elemental`
- `Magic`

**Skill Access:**

- Advanced water spells
- All elemental magic skills
- All general magic skills
- Water mastery techniques

**Example Synergies:**

- Tidal Wave (Magic/Elemental/Water): Strong synergy
- All Elemental spells: Strong synergy
- All general Magic skills: Strong synergy

---

## 7.3 Tier 3 Shaman Specializations

### 7.3.1 Flame Keeper

**Tier:** 3 | **Prerequisite:** Shaman + 50,000 XP from fire shamanism actions | **Tag Depth:** 3+

**Class Identity:** Fire-aspected shaman with warmth and purification magic

**Tags:**

- `Shamanic/Fire`
- `Shamanic`
- `Gathering/Herbalism`

**Skill Access:**

- Fire shamanic magic
- All shamanic skills
- Herbal remedies
- Fire purification rituals

**Example Synergies:**

- Fire Blessing (Shamanic/Fire): Strong synergy
- All shamanic skills: Strong synergy

---

### 7.3.2 Frost Shaman

**Tier:** 3 | **Prerequisite:** Shaman + 50,000 XP from frost shamanism actions | **Tag Depth:** 3+

**Class Identity:** Ice-aspected shaman with preservation and stillness magic

**Tags:**

- `Shamanic/Frost`
- `Shamanic`
- `Gathering/Herbalism`

**Skill Access:**

- Frost shamanic magic
- All shamanic skills
- Herbal remedies
- Preservation rituals

**Example Synergies:**

- Frost Blessing (Shamanic/Frost): Strong synergy
- All shamanic skills: Strong synergy

---

### 7.3.3 Storm Caller

**Tier:** 3 | **Prerequisite:** Shaman + 50,000 XP from storm shamanism actions | **Tag Depth:** 3+

**Class Identity:** Lightning-aspected shaman calling tempests and power

**Tags:**

- `Shamanic/Storm`
- `Shamanic`
- `Gathering/Herbalism`

**Skill Access:**

- Storm shamanic magic
- All shamanic skills
- Herbal remedies
- Weather rituals

**Example Synergies:**

- Storm Blessing (Shamanic/Storm): Strong synergy
- All shamanic skills: Strong synergy

---

### 7.3.4 Earth Shaman

**Tier:** 3 | **Prerequisite:** Shaman + 50,000 XP from earth shamanism actions | **Tag Depth:** 3+

**Class Identity:** Earth-aspected shaman grounded in stone and soil

**Tags:**

- `Shamanic/Earth`
- `Shamanic`
- `Gathering/Herbalism`

**Skill Access:**

- Earth shamanic magic
- All shamanic skills
- Herbal remedies
- Grounding rituals

**Example Synergies:**

- Earth Blessing (Shamanic/Earth): Strong synergy
- All shamanic skills: Strong synergy

---

### 7.3.5 Wind Walker

**Tier:** 3 | **Prerequisite:** Shaman + 50,000 XP from air shamanism actions | **Tag Depth:** 3+

**Class Identity:** Air-aspected shaman moving with wind and breath

**Tags:**

- `Shamanic/Air`
- `Shamanic`
- `Gathering/Herbalism`

**Skill Access:**

- Air shamanic magic
- All shamanic skills
- Herbal remedies
- Flight and movement rituals

**Example Synergies:**

- Air Blessing (Shamanic/Air): Strong synergy
- All shamanic skills: Strong synergy

---

### 7.3.6 Tide Caller

**Tier:** 3 | **Prerequisite:** Shaman + 50,000 XP from water shamanism actions | **Tag Depth:** 3+

**Class Identity:** Water-aspected shaman flowing with currents and tides

**Tags:**

- `Shamanic/Water`
- `Shamanic`
- `Gathering/Herbalism`

**Skill Access:**

- Water shamanic magic
- All shamanic skills
- Herbal remedies
- Purification through water

**Example Synergies:**

- Water Blessing (Shamanic/Water): Strong synergy
- All shamanic skills: Strong synergy

---

### 7.3.7 Ancestor Shaman

**Tier:** 3 | **Prerequisite:** Shaman + 50,000 XP from ancestral shamanism actions | **Tag Depth:** 3+

**Class Identity:** Keeper of ancestors and lineage magic

**Tags:**

- `Shamanic/Ancestors`
- `Shamanic`
- `Knowledge`

**Skill Access:**

- Ancestral shamanic magic
- All shamanic skills
- Ancestral knowledge
- Lineage rituals

**Example Synergies:**

- Ancestral Blessing (Shamanic/Ancestors): Strong synergy
- All shamanic skills: Strong synergy

---

### 7.3.8 Beast Speaker

**Tier:** 3 | **Prerequisite:** Shaman + 50,000 XP from animal shamanism actions | **Tag Depth:** 3+

**Class Identity:** Communicator with beasts and animal spirits

**Tags:**

- `Shamanic/Animals`
- `Shamanic`
- `Nature/Animals`

**Skill Access:**

- Animal shamanic magic
- All shamanic skills
- Animal communion
- Beast communication

**Example Synergies:**

- Beast Blessing (Shamanic/Animals): Strong synergy
- All shamanic skills: Strong synergy
- Nature/Animals skills: Moderate synergy

---

### 7.3.9 Spirit Walker

**Tier:** 3 | **Prerequisite:** Shaman + 50,000 XP from spirit shamanism actions | **Tag Depth:** 3+

**Class Identity:** Master of spirit realms and ethereal planes

**Tags:**

- `Shamanic/Spirits`
- `Shamanic`
- `Knowledge`

**Skill Access:**

- Spirit shamanic magic
- All shamanic skills
- Spirit world knowledge
- Ethereal traversal

**Example Synergies:**

- Spirit Blessing (Shamanic/Spirits): Strong synergy
- All shamanic skills: Strong synergy

---

### 7.3.10 War Shaman

**Tier:** 3 | **Prerequisite:** Shaman + Warrior + 50,000 XP from shamanic combat actions | **Tag Depth:** 3+

**Class Identity:** Battle-hardened shaman combining combat and spirits

**Tags:**

- `Shamanic`
- `Gathering/Herbalism`
- `Combat/Melee`
- `Physical/Strength`

**Skill Access:**

- All shamanic skills
- Melee combat
- Herbal remedies and combat potions
- Battle rituals

**Example Synergies:**

- All shamanic skills: Strong synergy
- All combat skills: Strong synergy
- Hybrid warrior-shaman techniques

**Notes:**

Consolidated class (Shaman + Warrior). Rare fusion of spiritual and martial power.

---

### 7.3.11 Totem Carver

**Tier:** 3 | **Prerequisite:** Shaman + Carpenter + 50,000 XP from totem crafting actions | **Tag Depth:** 3+

**Class Identity:** Crafter of spiritual totems and charms

**Tags:**

- `Shamanic`
- `Crafting/Woodworking`
- `Crafting`
- `Knowledge`

**Skill Access:**

- All shamanic skills
- All woodworking skills
- Totem creation
- Charm crafting

**Example Synergies:**

- All shamanic skills: Strong synergy
- All crafting skills: Strong synergy
- Totem enchantment techniques

**Notes:**

Consolidated class (Shaman + Carpenter). Master creators of spiritual objects.

---

### 7.4 Scholar

**Tier:** 3 | **Prerequisite:** 50,000 XP from knowledge and research actions | **Tag Depth:** 3+

**Class Identity:** Knowledge acquisition and teaching specialist

**Tags:**

- `Knowledge`
- `Knowledge/Learning`
- `Knowledge/Teaching`
- `Knowledge/Memory`
- `Social` (teaching)

**Skill Access:**

- All knowledge skills (13 total)
- Learning acceleration
- Teaching abilities
- Memory techniques
- Social interaction

**Example Synergies:**

- Rapid Learning (Knowledge/Learning): Strong synergy
- Teaching (Knowledge/Teaching, Social): Strong synergy
- Perfect Memory (Knowledge/Memory): Strong synergy
- Research (Knowledge): Strong synergy
- Skill Transference (Knowledge/Teaching): Strong synergy

---

### 7.5 Artificer

**Tier:** 3 | **Prerequisite:** 50,000 XP from magical crafting actions | **Tag Depth:** 3+

**Class Identity:** Magic-infused crafting master

**Tags:**

- `Crafting` (all specializations)
- `Magic/Enchantment`
- `Magic`
- `Knowledge`

**Skill Access:**

- All crafting skills
- Enchantment magic
- General magic
- Magical theory

**Example Synergies:**

- All Crafting skills: Strong synergy
- Enchant Object: Strong synergy
- All Magic skills: Strong synergy
- Hybrid build mastery

**Notes:**
Tier 3 consolidated class (Master in 3+ crafting specializations). Extremely rare and powerful.

---

### 7.6 Ranger

**Tier:** 3 | **Prerequisite:** 50,000 XP from wilderness combat actions | **Tag Depth:** 3+

**Class Identity:** Wilderness survival and combat specialist

**Tags:**

- `Nature`
- `Nature/Animals`
- `Gathering`
- `Gathering/Hunting`
- `Combat/Ranged`
- `Stealth`

**Skill Access:**

- Nature skills
- Animal handling
- Hunting and tracking
- Ranged combat
- Stealth

**Example Synergies:**

- All nature and gathering skills: Strong synergy
- Tracking: Strong synergy
- Ranged combat: Strong synergy
- Hybrid wilderness warrior

**Notes:**
Consolidated class (Hunter + Explorer or Hunter + Scout).

---

## 8. Verification Table

Verification of skill access counts comparing old pool system to new tag system:

| Class          | Old Pool Access                            | New Tag Access                                                   | Change     | Notes                               |
| -------------- | ------------------------------------------ | ---------------------------------------------------------------- | ---------- | ----------------------------------- |
| **Warrior**    | Combat (11)                                | Combat (24) + Physical (5) = 29 unique                           | +18 skills | Broader combat access               |
| **Blacksmith** | Smithing (5) + Crafting-General (9) = 14   | Crafting (35 total) = 35 unique                                  | +21 skills | All crafting accessible             |
| **Alchemist**  | Alchemy (6) + Crafting-General (9) = 15    | Crafting (35) = 35 unique                                        | +20 skills | All crafting accessible             |
| **Thief**      | Stealth (10) + Dark (7) = 17               | Stealth (11) + Criminal (14) = 25 unique                         | +8 skills  | Broader criminal access             |
| **Mage**       | Magic (9)                                  | Magic (26) = 26 unique                                           | +17 skills | All magic schools accessible        |
| **Assassin**   | Stealth (10) + Combat (11) + Dark (7) = 28 | Stealth (11) + Combat (24) + Criminal (14) = ~40 unique          | +12 skills | Broader skill access                |
| **Farmer**     | Agriculture (6) + Gathering (8) = 14       | Agriculture (10) + Nature (7) + Gathering (17) = ~25 unique      | +11 skills | Nature synergy added                |
| **Hunter**     | Gathering (8) + Animal (6) = 14            | Gathering (17) + Nature/Animals (6) + Combat/Ranged = ~23 unique | +9 skills  | Broader gathering access            |
| **Scholar**    | Scholar (7)                                | Knowledge (13) + Social (7) = ~17 unique                         | +10 skills | Teaching and social added           |
| **Shaman**     | Shamanic (8)                               | Magic/Shamanic (8) + Tribal (8) + Nature/Spirits = ~8 unique\*   | Similar    | \*Most overlap due to multi-tagging |

**Key Findings:**

1. Most classes gain access to MORE skills through hierarchical tags
2. No class loses significant access
3. Skill counts increase due to tag inheritance (accessing parent-level skills)
4. Multi-tagged skills create natural synergies across classes
5. Specialization preserved through specific tags (e.g., `Crafting/Smithing/Weapon`)

**Balance Notes:**

- Increased skill access doesn't mean overpowered - still need XP to unlock
- Class synergy bonuses (+100% XP, +25% effectiveness) are the real power
- Tag system enables more flexible, hybrid builds naturally
- Players can still focus on specialist builds for maximum power

---

## 9. Tag Assignment Guidelines

When creating new classes or updating existing ones:

### Step 1: Identify Core Function

What is the class's primary role?

- Combat specialist → Combat tags
- Crafter → Crafting tags
- Gatherer → Gathering tags
- Spellcaster → Magic tags
- Trader → Trade tags

### Step 2: Determine Specialization Level

How specialized is this class?

- **Broad specialist:** Top-level tag + category (`Combat`, `Crafting`)
- **Focused specialist:** Subcategory tags (`Combat/Melee`, `Crafting/Smithing`)
- **Deep specialist:** Specialization tags (`Crafting/Smithing/Weapon`)

### Step 3: Add Secondary Tags

What other skills does this class excel at?

- Physical component → `Physical/*` tags
- Social component → `Social/*` tags
- Knowledge component → `Knowledge/*` tags
- Nature component → `Nature/*` tags

### Step 4: Consider Hybrid Nature

Is this a consolidated or hybrid class?

- Add tags from both parent classes
- Example: Battlemage = Mage tags + Combat tags
- Example: Ranger = Hunter tags + Scout tags

### Step 5: Verify Logical Consistency

Does each tag make sense for this class?

- Would practitioners of this class logically excel at skills with these tags?
- Do the tags reflect the class's cultural, biological, or training focus?
- Are there any missing tags that should be included?

---

## 10. Cross-References

**See Also:**

- [skill-tags.md](/workspaces/cozy-fantasy-rpg/docs/design/systems/character/skill-tags.md) - Hierarchical tag system
- [racial-synergies.md](/workspaces/cozy-fantasy-rpg/docs/design/systems/character/racial-synergies.md) - Racial affinity system
- [class-progression.md](/workspaces/cozy-fantasy-rpg/docs/design/systems/character/class-progression.md) - Class synergy mechanics
- [REFACTOR-AUDIT.md](/workspaces/cozy-fantasy-rpg/docs/design/content/skills/REFACTOR-AUDIT.md) - Complete skill audit with tags

---

**End of Class-to-Tag Associations Documentation**
