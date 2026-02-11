---
title: Skill Tags
gdd_ref: systems/skill-recipe-system-gdd.md#tag-system
---

<!-- ADAPTATION REQUIRED -->
<!-- This file was migrated from source but needs manual review: -->

<!) -->

<!-- - Align with current GDD architecture -->
<!-- - Add missing sections as needed -->
<!-- - Update frontmatter with correct gdd_ref -->

---

title: Hierarchical Skill Tag System
type: system
summary: Complete specification of the tag-based skill access architecture

---

# Hierarchical Skill Tag System

## 1. Overview

The skill system uses hierarchical tags to organize skills and determine character access. This replaces the previous pool-based system with a flexible, inheritance-driven approach that better reflects the logical relationships between classes and skills.

### Core Principles

| Principle                    | Description                                                                        |
| ---------------------------- | ---------------------------------------------------------------------------------- |
| **Hierarchical Structure**   | Tags use `Category/Subcategory/Specialization` format                              |
| **Inheritance Access**       | Classes with specific tags can access skills with that tag OR any parent tag       |
| **Specificity = Power**      | More specific tags indicate more specialized/powerful skills                       |
| **Universal Access**         | All skills accessible to all characters; tags provide bonuses, not gates           |
| **Multi-Tagging**            | Skills can have multiple hierarchical tags from different categories               |
| **Context-Specific Bonuses** | Synergy strength depends on logical relationship between class tags and skill tags |

---

## 2. Tag Hierarchy Rules

### Inheritance Rule

**Classes with a specific tag can access skills tagged at that level OR any parent level.**

**Example:**

- Class with `Crafting/Smithing/Weapon` can learn skills tagged:
- `Crafting` (top-level parent)
- `Crafting/Smithing` (parent)
- `Crafting/Smithing/Weapon` (exact match)

### Specificity = Power

More specific tags indicate more specialized or powerful skills:

| Tag Level                  | Skill Type               | Example                                |
| -------------------------- | ------------------------ | -------------------------------------- |
| `Crafting`                 | Universal crafting skill | Material Intuition, Tool Bond          |
| `Crafting/Smithing`        | Smithing specialization  | Metal Sense, Forge Fire                |
| `Crafting/Smithing/Weapon` | Weapon-specific smithing | Weapon Enhancement, Balance Perfection |

**The deeper the tag, the more specialized the skill.**

### Multi-Tag Rules

Skills can have multiple hierarchical tags from different categories:

**Example: Silent Kill**

- `Stealth/Assassination` (stealth approach)
- `Combat/Assassination` (combat execution)
- `Criminal/Assassination` (criminal nature)

**Benefits:**

- Multiple classes gain synergies with the skill
- Reflects real-world skill overlap
- Enables cross-class builds naturally

---

## 3. Tag Categories

### 3.1 Combat Tags

**Combat** (24 unique skills)
├── **Combat/Melee** (7 skills) - Close-range physical combat
│ ├── Examples: Weapon Mastery, Battle Rage, Dual Wield
│ └── Classes: Warrior, Knight, Berserker
├── **Combat/Ranged** (3 skills) - Distance weapons
│ ├── Examples: Bow Mastery, Precise Shot
│ └── Classes: Archer, Scout, Hunter
├── **Combat/Mounted** (2 skills) - Fighting while mounted
│ ├── Examples: Mounted Combat, Cavalry Charge
│ └── Classes: Knight, Caravaner
├── **Combat/Tactics** (4 skills) - Strategic combat abilities
│ ├── Examples: Commander's Voice, Tactical Assessment
│ └── Classes: Knight, Guard, Leader
├── **Combat/Assassination** (4 skills) - Lethal precision strikes
│ ├── Examples: Backstab, Silent Kill, Killing Blow
│ └── Classes: Assassin, Scout
└── **Combat/Defense** (4 skills) - Protective abilities
├── Examples: Shield Wall, Armor Expert, Evasion Expert
└── Classes: Warrior, Knight, Guard

**Criteria for Combat Tags:**

- `Combat` = Any combat-related ability (universal combat)
- `Combat/Melee` = Close-range physical fighting
- `Combat/Ranged` = Distance weapon usage
- `Combat/Mounted` = Fighting from horseback/mount
- `Combat/Tactics` = Strategic/leadership in battle
- `Combat/Assassination` = Lethal precision killing
- `Combat/Defense` = Protective and defensive abilities

---

### 3.2 Crafting Tags

**Crafting** (35 unique skills)
├── **Crafting** (9 skills) - Universal crafting abilities
│ ├── Examples: Artisan's Focus, Masterwork Chance, Material Intuition
│ └── All crafting classes benefit
├── **Crafting/Smithing** (5 skills) - Metal working
│ ├── **Crafting/Smithing/Weapon** (1 skill) - Weapon smithing specialty
│ │ ├── Example: Weapon Enhancement
│ │ └── Classes: Weaponsmith
│ ├── **Crafting/Smithing/Armor** (1 skill) - Armor smithing specialty
│ │ ├── Example: Armor Fitting
│ │ └── Classes: Armorsmith
│ └── Examples: Forge Fire, Metal Sense, Perfect Temper
└── **Crafting/Alchemy** (11 skills) - Potion and chemical crafting
├── **Crafting/Alchemy/Potion** (3 skills) - Beneficial potions
│ ├── Examples: Potent Brews, Stable Solutions
│ └── Classes: Alchemist, Healer
└── **Crafting/Alchemy/Poison** (2 skills) - Toxins and harmful substances
├── Examples: Poison Craft, Explosive Compounds
└── Classes: Alchemist, Assassin

**Crafting Tag Criteria:**

- `Crafting` = Universal crafting skill (all crafters benefit)
- `Crafting/Smithing` = Metal working (heat, forge, hammering)
- `Crafting/Smithing/Weapon` = Weapon-specific techniques
- `Crafting/Smithing/Armor` = Armor-specific techniques
- `Crafting/Alchemy` = Potion/chemical crafting
- `Crafting/Alchemy/Potion` = Beneficial concoctions
- `Crafting/Alchemy/Poison` = Toxins and harmful substances

**Additional Crafting Specializations (not yet fully implemented):**

- `Crafting/Woodworking` - Carpenter, Fletcher
- `Crafting/Leatherworking` - Leatherworker
- `Crafting/Tailoring` - Tailor
- `Crafting/Cooking` - Cook
- `Crafting/Jewelcrafting` - Jeweler

---

### 3.3 Gathering Tags

**Gathering** (17 unique skills)
├── **Gathering** (4 skills) - Universal gathering abilities
│ ├── Examples: Bountiful Harvest, Efficiency Expert, Master Forager
│ └── All gathering classes benefit
├── **Gathering/Mining** (3 skills) - Ore and mineral extraction
│ ├── Examples: Deep Vein, Prospector, Resource Sense (ore)
│ └── Classes: Miner
├── **Gathering/Herbalism** (2 skills) - Plant gathering
│ ├── Examples: Gentle Harvest, Resource Sense (herbs)
│ └── Classes: Forager, Alchemist
├── **Gathering/Hunting** (5 skills) - Animal tracking and hunting
│ ├── Examples: Creature Lore, Hunting Companion, Scent Mastery
│ └── Classes: Hunter, Scout
├── **Gathering/Fishing** (3 skills) - Aquatic resource gathering
│ ├── Examples: Deep Vein (underwater), Resource Sense (fish)
│ └── Classes: Fisher
└── **Gathering/Lumbering** (3 skills) - Wood harvesting
├── Examples: Resource Sense (trees), Weather Worker
└── Classes: Lumberjack

**Gathering Tag Criteria:**

- `Gathering` = Universal gathering efficiency
- `Gathering/Mining` = Stone/ore/mineral extraction
- `Gathering/Herbalism` = Plant/herb gathering
- `Gathering/Hunting` = Animal tracking and hunting
- `Gathering/Fishing` = Aquatic resource gathering
- `Gathering/Lumbering` = Tree/wood harvesting

---

### 3.4 Magic Tags

**Magic** (26 unique skills)
├── **Magic** (4 skills) - Universal magic abilities
│ ├── Examples: Mana Well, Mana Transfer, Ritual Casting, Spell Focus
│ └── All spellcasting classes benefit
├── **Magic/Arcane** (2 skills) - Utility and counterspells
│ ├── Examples: Counter Magic, Spell Weaving
│ └── Classes: Mage, Enchanter
├── **Magic/Elemental** (depth 2) - Elemental magic
│ ├── **Magic/Elemental/Fire** (depth 3) ← Pyromancer
│ ├── **Magic/Elemental/Ice** (depth 3) ← Cryomancer
│ ├── **Magic/Elemental/Lightning** (depth 3) ← Electromancer
│ ├── **Magic/Elemental/Earth** (depth 3) ← Geomancer
│ ├── **Magic/Elemental/Air** (depth 3) ← Aeromancer
│ └── **Magic/Elemental/Water** (depth 3) ← Hydromancer
├── **Magic/Conjuration** (depth 2) - Summoning magic
│ ├── **Magic/Conjuration/Summoning** (depth 3) ← Summoner
│ └── **Magic/Conjuration/Undead** (depth 3) ← Necromancer
├── **Magic/Illusion** (depth 2) - Deception magic
│ └── **Magic/Illusion/Mind** (depth 3) ← Illusionist
└── **Magic/Enchantment** (depth 2) - Item enchantment
└── Classes: Enchanter (consolidation)

**Magic Tag Criteria:**

- `Magic` = Universal spellcasting ability
- `Magic/Arcane` = Pure magical manipulation, counterspells
- `Magic/Elemental` = Elemental schools (Mage depth-2 access)
- `Magic/Elemental/*` = Specific element mastery (Tier 3 specialists)
- `Magic/Conjuration` = Summoning and binding (Mage depth-2 access)
- `Magic/Conjuration/*` = Specific conjuration mastery (Tier 3 specialists)
- `Magic/Illusion` = Deception and mind magic (Mage depth-2 access)
- `Magic/Illusion/Mind` = Deep illusion mastery (Illusionist)
- `Magic/Enchantment` = Item enchantment (Enchanter consolidation)

**Removed Tags:**

- `Magic/Healing` → Healer is now consolidation (Mage + Herbalist)
- `Magic/Necromancy` → Merged into `Magic/Conjuration/Undead`
- `Magic/Shamanic` → Replaced by root-level `Shamanic`

---

### 3.5 Stealth Tags

**Stealth** (11 unique skills)
├── **Stealth** (2 skills) - Universal stealth abilities
│ ├── Examples: Shadow Step, Vanish
│ └── All stealth-focused classes benefit
├── **Stealth/Infiltration** (2 skills) - Breaking in and bypassing security
│ ├── Examples: Lockmaster, Smuggler's Instinct
│ └── Classes: Thief, Assassin, Smuggler
├── **Stealth/Deception** (2 skills) - Misdirection and lies
│ ├── Examples: Misdirection, Thieves' Cant
│ └── Classes: Thief, Fence
└── **Stealth/Assassination** (3 skills) - Killing from shadows
├── Examples: Backstab, Poison Craft, Silent Kill
└── Classes: Assassin

**Stealth Tag Criteria:**

- `Stealth` = Universal sneaking and hiding
- `Stealth/Infiltration` = Breaking into secured areas
- `Stealth/Deception` = Lying, misleading, false identities
- `Stealth/Assassination` = Killing from stealth

---

### 3.6 Leadership Tags

**Leadership** (13 unique skills)
├── **Leadership** (2 skills) - Universal leadership abilities
│ ├── Examples: Authority, Inspiring Leader
│ └── All leadership classes benefit
├── **Leadership/Command** (3 skills) - Military and combat leadership
│ ├── Examples: Authority, Commander's Voice, Crisis Leadership
│ └── Classes: Knight, Guard Captain, General
├── **Leadership/Management** (3 skills) - Administrative and organizational
│ ├── Examples: Delegation, Policy Implementation, Succession Planning
│ └── Classes: Mayor, Guild Master, Settlement Leader
└── **Leadership/Social** (2 skills) - Social influence and charisma
├── Examples: Inspiring Leader, Loyalty Cultivation
└── Classes: Merchant, Noble, Diplomat

**Leadership Tag Criteria:**

- `Leadership` = Universal leadership ability
- `Leadership/Command` = Military/combat command
- `Leadership/Management` = Administration and organization
- `Leadership/Social` = Social influence and persuasion

---

### 3.7 Knowledge Tags

**Knowledge** (13 unique skills)
├── **Knowledge** (3 skills) - Universal knowledge abilities
│ ├── Examples: Ancient Knowledge, Creature Lore, Research
│ └── All knowledge-focused classes benefit
├── **Knowledge/Languages** (1 skill) - Linguistic mastery
│ ├── Example: Linguistic Mastery
│ └── Classes: Scholar, Diplomat, Merchant
├── **Knowledge/Memory** (1 skill) - Perfect recall
│ ├── Example: Perfect Memory
│ └── Classes: Scholar
├── **Knowledge/Learning** (1 skill) - Accelerated learning
│ ├── Example: Rapid Learning
│ └── Classes: Scholar, Apprentice
└── **Knowledge/Teaching** (2 skills) - Knowledge transfer
├── Examples: Teaching, Skill Transference
└── Classes: Scholar, Master Craftsman

**Knowledge Tag Criteria:**

- `Knowledge` = Universal learning and recall
- `Knowledge/Languages` = Language acquisition
- `Knowledge/Memory` = Perfect recall abilities
- `Knowledge/Learning` = Accelerated learning
- `Knowledge/Teaching` = Teaching others

---

### 3.8 Trade Tags

**Trade** (14 unique skills)
├── **Trade** (3 skills) - Universal commerce abilities
│ ├── Examples: Market Sense, Trade Contacts, Trade Network
│ └── All trade classes benefit
├── **Trade/Caravan** (1 skill) - Long-distance trade
│ ├── Example: Caravan Master
│ └── Classes: Caravaner
├── **Trade/Negotiation** (1 skill) - Bargaining and deals
│ ├── Example: Silver Tongue
│ └── Classes: Merchant, Trader
├── **Trade/Speculation** (2 skills) - Market prediction
│ ├── Examples: Commodity Speculation, Market Sense
│ └── Classes: Merchant, Trader
└── **Trade/Illegal** (1 skill) - Black market trade
├── Example: Fencing
└── Classes: Fence, Thief

**Trade Tag Criteria:**

- `Trade` = Universal commerce ability
- `Trade/Caravan` = Long-distance trade operations
- `Trade/Negotiation` = Bargaining and deal-making
- `Trade/Speculation` = Market prediction and timing
- `Trade/Illegal` = Black market transactions

---

### 3.9 Criminal Tags

**Criminal** (14 unique skills)
├── **Criminal** (3 skills) - Universal criminal abilities
│ ├── Examples: Evidence Elimination, False Identity, Underworld Connections
│ └── All dark path classes benefit
├── **Criminal/Theft** (2 skills) - Stealing property
│ ├── Examples: Lockmaster, Thieves' Cant
│ └── Classes: Thief
├── **Criminal/Assassination** (3 skills) - Murder for hire
│ ├── Examples: Contract Killing, Poison Craft, Silent Kill
│ └── Classes: Assassin
├── **Criminal/Extortion** (2 skills) - Intimidation and coercion
│ ├── Examples: Blackmail, Extortion
│ └── Classes: Extortionist, Crime Boss
└── **Criminal/Finance** (2 skills) - Illegal financial operations
├── Examples: Fencing, Money Laundering
└── Classes: Fence, Crime Boss

**Criminal Tag Criteria:**

- `Criminal` = Universal illegal activity
- `Criminal/Theft` = Property theft
- `Criminal/Assassination` = Contract killing
- `Criminal/Extortion` = Intimidation-based crime
- `Criminal/Finance` = Illegal financial operations

---

### 3.10 Agriculture Tags

**Agriculture** (10 unique skills)
├── **Agriculture** (3 skills) - Universal farming abilities
│ ├── Examples: Pest Resistance, Season Sense, Surplus Generator
│ └── All farming classes benefit
├── **Agriculture/Livestock** (2 skills) - Animal husbandry
│ ├── Examples: Animal Husbandry, Beekeeper's Bond
│ └── Classes: Farmer, Rancher
└── **Agriculture/Crops** (1 skill) - Plant cultivation
├── Example: Green Thumb
└── Classes: Farmer, Gardener

**Agriculture Tag Criteria:**

- `Agriculture` = Universal farming ability
- `Agriculture/Livestock` = Animal raising and care
- `Agriculture/Crops` = Plant growing and harvesting

---

### 3.11 Nature Tags

**Nature** (7 unique skills)
├── **Nature** (1 skill) - Universal nature understanding
│ ├── Example: Season Sense
│ └── Nature-focused classes benefit
├── **Nature/Animals** (6 skills) - Animal understanding and bonding
│ ├── Examples: Beast Tongue, Creature Lore, Taming, Pack Leader
│ └── Classes: Hunter, Ranger, Druid
├── **Nature/Insects** (1 skill) - Insect understanding
│ ├── Example: Beekeeper's Bond
│ └── Classes: Farmer, Forager
└── **Nature/Spirits** (4 skills) - Spirit world interaction
├── Examples: Spirit Binding, Spirit Communion, Ancestral Memory
└── Classes: Shaman, Druid

**Nature Tag Criteria:**

- `Nature` = Universal nature understanding
- `Nature/Animals` = Animal communication and bonding
- `Nature/Insects` = Insect understanding and manipulation
- `Nature/Spirits` = Spirit world interaction

---

### 3.12 Physical Tags

**Physical** (5 unique skills)
├── **Physical** (2 skills) - Universal physical abilities
│ ├── Examples: Ambidexterity, Night Vision
│ └── All physically-focused classes benefit
├── **Physical/Strength** (0 explicit skills) - Strength-based abilities
│ └── Classes: Warrior, Blacksmith, Miner
├── **Physical/Agility** (1 skill) - Agility-based abilities
│ ├── Example: Evasion Expert
│ └── Classes: Thief, Assassin, Scout
└── **Physical/Endurance** (1 skill) - Endurance-based abilities
├── Example: Battle Hardened
└── Classes: Warrior, Farmer, Miner

**Physical Tag Criteria:**

- `Physical` = Universal physical prowess
- `Physical/Strength` = Strength-based actions
- `Physical/Agility` = Speed and dexterity
- `Physical/Endurance` = Stamina and resilience

---

### 3.13 Social Tags

**Social** (7 unique skills)
├── **Social** (2 skills) - Universal social abilities
│ ├── Examples: Inspiring Leader, Teaching
│ └── All socially-focused classes benefit
├── **Social/Persuasion** (2 skills) - Convincing and influencing
│ ├── Examples: Silver Tongue, Authority
│ └── Classes: Merchant, Leader, Noble
└── **Social/Deception** (2 skills) - Lying and misleading
├── Examples: False Identity, Misdirection
└── Classes: Thief, Spy, Assassin

**Social Tag Criteria:**

- `Social` = Universal social interaction
- `Social/Persuasion` = Honest influence and convincing
- `Social/Deception` = Dishonest manipulation and lies

---

### 3.14 Tribal Tags

**Tribal** (8 unique skills)
├── **Tribal** (1 skill) - Universal tribal abilities
│ ├── Example: War Painter
│ └── Tribal-focused classes benefit
└── **Tribal/Spirits** (7 skills) - Shamanic spirit magic
├── Examples: Spirit Binding, Totem Crafting, Fetish Creation
└── Classes: Shaman, Witch Doctor

**Tribal Tag Criteria:**

- `Tribal` = Universal tribal cultural practices
- `Tribal/Spirits` = Shamanic spirit world interaction

---

### 3.15 Economy Tags

**Economy** (4 unique skills)
├── **Economy** (1 skill) - Universal economic understanding
│ ├── Example: Market Sense
│ └── Economy-focused classes benefit
├── **Economy/Speculation** (1 skill) - Market prediction
│ ├── Example: Commodity Speculation
│ └── Classes: Merchant, Trader
└── **Economy/Finance** (1 skill) - Financial operations
├── Example: Money Laundering
└── Classes: Banker, Crime Boss, Fence

**Economy Tag Criteria:**

- `Economy` = Universal economic understanding
- `Economy/Speculation` = Market timing and prediction
- `Economy/Finance` = Financial operations and management

---

### 3.16 Universal Tags

**Universal** (5 unique skills)
├── **Universal/Physical** (3 skills) - Cross-class physical abilities
│ ├── Examples: Ambidexterity, Night Vision, Water Breathing
│ └── All classes can access
└── **Universal/Mental** (2 skills) - Cross-class mental abilities
├── Examples: Danger Sense, Iron Will
└── All classes can access

**Universal Tag Criteria:**

- `Universal/Physical` = Cross-class physical abilities (not class-specific)
- `Universal/Mental` = Cross-class mental abilities (not class-specific)

---

### 3.17 Shamanic Tags

**Shamanic** (new root-level tag for tribal spirit magic)
├── **Shamanic** (depth 1) - Universal spirit magic
│ └── Classes: Spiritcaller (Foundation)
├── **Shamanic/Elemental** (depth 2) - Elemental spirits
│ ├── **Shamanic/Elemental/Fire** (depth 3) ← Flame Keeper
│ ├── **Shamanic/Elemental/Ice** (depth 3) ← Frost Shaman
│ ├── **Shamanic/Elemental/Lightning** (depth 3) ← Storm Caller
│ ├── **Shamanic/Elemental/Earth** (depth 3) ← Earth Shaman
│ ├── **Shamanic/Elemental/Air** (depth 3) ← Wind Walker
│ └── **Shamanic/Elemental/Water** (depth 3) ← Tide Caller
├── **Shamanic/Spirit** (depth 2) - Spirit communion
│ ├── **Shamanic/Spirit/Ancestor** (depth 3) ← Ancestor Shaman
│ ├── **Shamanic/Spirit/Beast** (depth 3) ← Beast Speaker
│ └── **Shamanic/Spirit/Dream** (depth 3) ← Spirit Walker
└── **Shamanic/Nature** (depth 2) - Herbalism, healing, charms
└── Classes: Shaman (built-in abilities)

**Shamanic Tag Criteria:**

- `Shamanic` = Universal spirit pool access
- `Shamanic/Elemental` = Elemental spirit magic (Shaman depth-2 access)
- `Shamanic/Elemental/*` = Specific element spirit mastery (Tier 3 specialists)
- `Shamanic/Spirit` = Spirit communion and binding (Shaman depth-2 access)
- `Shamanic/Spirit/*` = Specific spirit domain mastery (Tier 3 specialists)
- `Shamanic/Nature` = Herbalism, healing, charms (Shaman built-in)

---

## 4. Multi-Tag Examples

Skills with multiple hierarchical tags from different categories:

### Silent Kill

- `Stealth/Assassination` - Stealth approach and silence
- `Combat/Assassination` - Lethal combat execution
- `Criminal/Assassination` - Criminal nature of contract killing

**Classes that benefit:**

- Assassin (all three tags) - Strong synergy
- Thief (`Criminal`, `Stealth`) - Moderate synergy
- Scout (`Combat`, `Stealth`) - Moderate synergy

### Poison Craft

- `Stealth/Assassination` - Used for stealth kills
- `Crafting/Alchemy/Poison` - Alchemical crafting
- `Criminal/Assassination` - Criminal application

**Classes that benefit:**

- Assassin (`Stealth/Assassination`, `Criminal/Assassination`) - Strong synergy
- Alchemist (`Crafting/Alchemy`) - Strong synergy
- Thief (`Criminal`) - Weak synergy

### Backstab

- `Stealth/Assassination` - Attacking from stealth
- `Combat/Assassination` - Combat execution technique

**Classes that benefit:**

- Assassin (both tags) - Strong synergy
- Thief (`Stealth`) - Moderate synergy
- Warrior (`Combat`) - Weak synergy

### Lockmaster

- `Stealth/Infiltration` - Bypassing security
- `Criminal/Theft` - Criminal application

**Classes that benefit:**

- Thief (both tags) - Strong synergy
- Assassin (`Stealth`) - Moderate synergy
- Any class (no tags) - Base rate

---

## 5. Tagging Guidelines for New Skills

When creating new skills, follow these guidelines:

### Step 1: Identify Primary Function

What does the skill do at its core?

- Combat? Combat damage, defense, tactics
- Crafting? Creating items, refining materials
- Gathering? Collecting resources
- Magic? Spellcasting, mana manipulation
- Stealth? Hiding, sneaking, infiltration
- Social? Persuasion, deception, leadership

### Step 2: Determine Specificity

Is this a broad or specialized skill?

- **Broad** → Top-level tag only (`Crafting`, `Combat`)
- **Specialized** → Add subcategory (`Crafting/Smithing`, `Combat/Melee`)
- **Highly Specialized** → Add specialization (`Crafting/Smithing/Weapon`)

### Step 3: Identify Cross-Category Applications

Does the skill apply to multiple domains?

- Add additional tags from different categories
- Example: Poison Craft = `Crafting/Alchemy/Poison` + `Stealth/Assassination` + `Criminal/Assassination`

### Step 4: Verify Logical Consistency

Does each tag make sense for this skill?

- Each tag should represent a genuine aspect of the skill
- Avoid adding tags just to give more classes access
- Test: "Would a class with this tag logically benefit from this skill?"

### Step 5: Consider Power Level

More specific tags = more specialized/powerful

- Universal skills: Top-level tags only
- Specialist skills: 2-3 levels deep
- Master skills: 3+ levels deep with multiple tags

---

## 6. Cross-Reference

**See Also:**

- [REFACTOR-AUDIT.md](././content/skills/REFACTOR-AUDIT.md) - Complete skill-to-tag mapping
- [Class Tag Associations](class-tag-associations.md) - Class-to-tag mappings
- [Racial Synergies](racial-synergies.md) - Racial affinity system
- [Class Progression](class-progression.md) - Level-up and skill acquisition mechanics
- [Skills Library Index](././content/skills/index.md) - Browse all skills

---

## 7. Implementation Notes

### Tag Format

Tags must follow the hierarchical format:

```
Category
Category/Subcategory
Category/Subcategory/Specialization
```

**Enforced Rules:**

- Use forward slash `/` as separator
- Use CamelCase for multi-word categories
- No spaces in tag names
- Maximum 3 levels deep (Category/Sub/Specialization)

### Skill Definition Example

```yaml
skill:
  name: 'Weapon Enhancement'
  tags:
    - 'Crafting' # Top-level access
    - 'Crafting/Smithing' # Smithing access
    - 'Crafting/Smithing/Weapon' # Weapon smithing specialization
  type: 'Mechanic Unlock'
  description: 'Enhance weapons during creation with special techniques'
```

### Class Tag Example

```yaml
class:
  name: 'Weaponsmith'
  tags:
    - 'Crafting/Smithing/Weapon' # Weapon smithing specialization
    - 'Crafting/Smithing' # General smithing (redundant but explicit)
    - 'Physical/Strength' # Physical strength component
```

With these tags, Weaponsmith can learn:

- Skills tagged `Crafting`
- Skills tagged `Crafting/Smithing`
- Skills tagged `Crafting/Smithing/Weapon`
- Skills tagged `Physical` or `Physical/Strength`

---

## 8. Tag Depth Access by Class Tier

Class tier determines the maximum tag depth accessible for synergy bonuses. See [Class Tiers](class-tiers.md) for complete tier system documentation.

### Core Rule

A class can only receive full synergy bonuses from skills at or above their tier's depth limit:

- **Tier 1 (Foundation):** Depth 1 tags only (e.g., `Crafting`)
- **Tier 2:** Depth 1-2 tags (e.g., `Crafting/Smithing`)
- **Tier 3:** All depths (e.g., `Crafting/Smithing/Weapon`)

### Example

```
Crafting (depth 1) ← All tiers access
└── Crafting/Smithing (depth 2) ← Tier 2+ access
 └── Crafting/Smithing/Weapon (depth 3) ← Tier 3 only
```

A Tier 2 Blacksmith gains synergy bonuses for `Crafting` and `Crafting/Smithing` skills, but not `Crafting/Smithing/Weapon` skills (learns at base rate only).

### Cross-Reference

- [Class Tiers](class-tiers.md) - Full tier system, XP formulas, and synergy bonuses
- [Class Tag Associations](class-tag-associations.md) - Which classes have which tags

---

**End of Skill Tags Documentation**
