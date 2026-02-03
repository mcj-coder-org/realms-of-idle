---
type: system
scope: detailed
status: authoritative
version: 1.0.0
created: 2026-02-03
updated: 2026-02-03
subjects: [skills, recipes, spells, acquisition, tiers, slots, rarity, achievements]
dependencies: [core-progression-system-gdd.md, class-system-gdd.md, idle-inn-gdd.md]
---

# Skill & Recipe System - Authoritative Game Design

## Executive Summary

The Skill & Recipe System governs how players acquire, manage, and utilize skills through organic gameplay actions. Skills are discovered through play and offered as level-up rewards, creating meaningful character builds without overwhelming choice. This system integrates tightly with class and progression systems, providing clear advancement paths with strategic depth through the 5-slot quick access system.

**This document resolves:**

- Active skill slot limits (5 quick slots, unlimited total skills)
- Skill tier requirements (tag-based filtering by class tier)
- Skill rarity formulas with achievement bonuses
- Legendary achievement system (universal and class-specific)
- Excluding tag mechanics (soft penalty 2× requirements)
- Recipe and spell discovery mechanics

**Design Philosophy:** Skills are earned through play, not selected from a catalog. Players discover abilities by doing, then make strategic choices about which 5 deserve quick access. Unlimited skill accumulation rewards exploration, while quick slots create meaningful decisions. Extraordinary achievements can trigger early legendary unlocks.

---

## 1. Skill Categories & Types

### 1.1 Skill Classification

```
SKILL TYPES:

Active Skills:
  - Must be triggered by player (instant or with duration)
  - Consume resources or have cooldowns
  - Can be assigned to quick slots for rapid access
  - Also accessible through skill menu (slower)

Passive Skills:
  - Always active once learned
  - Provide permanent bonuses
  - No slot needed, unlimited accumulation
  - Stack with other passives

Toggle Skills (Future Consideration):
  - Can be enabled/disabled
  - Drain resources while active or provide stance
  - Would occupy quick slot if implemented
  - Examples: Defensive Stance, Offensive Aura
```

### 1.2 Skill Sources

```
SOURCES OF SKILLS:

Class Level Up Rewards (Primary):
  → Filtered by class tag + class tier
  → Example: [Warrior - Journeyman] filters by warrior.journeyman
  → Parent tag (warrior) also matches
  → Offered at each level up
  → Can refuse (but skill is learned, goes to backlog)

Bucket XP Discovery (Secondary):
  → High bucket XP unlocks cross-class skills
  → Rare skills discovered through mastery
  → Example: High combat.melee.sword bucket unlocks blade skills
  → No parent class required for basic access

Training (Guaranteed Path):
  → Learn from NPC trainers
  → Pay gold or complete quests
  → Bypasses bucket requirements
  → Predictable but takes time/effort

Discovery Events (Rare):
  → Find ancient tomes in dungeons
  → Learn from secret masters
  → Rare world events
  → Unique one-time skills
```

### 1.3 Recipe & Spell Sources

```
RECIPES (Crafting):

Primary: Crafting Class Level Ups
  → [Blacksmith] levels → weapon/armor recipes
  → [Alchemist] levels → potion recipes
  → [Chef] levels → cooking recipes

Secondary: Bucket XP Thresholds
  → craft.smithing.weapon ≥ 500 XP → Basic Sword Recipe
  → craft.alchemy.potion ≥ 1000 XP → Healing Potion Recipe
  → Higher bucket XP = higher recipe quality discovery chance

Tertiary: Training & Discovery
  → Apprentice with master smith
  → Find recipe book in ruins
  → Reverse-engineer from crafted item

SPELLS (Magic):

Primary: Caster Class Level Ups
  → [Mage] levels → fire/ice/lightning spells
  → [Healer] levels → divine spells
  → [Necromancer] levels → dark spells

Secondary: Magic Bucket XP
  → magic.fire ≥ 500 XP → Fireball Spell
  → magic.divine ≥ 1000 XP → Cure Wounds Spell

Tertiary: Research & Discovery
  → Study at magical academy
  → Find spell scrolls in dungeons
  → Learn from magical creatures
```

---

## 2. Skill Slot System

### 2.1 Quick Slot Limit

Players have **5 quick slots** for rapid skill access:

```
QUICK SLOT SYSTEM:

Total Quick Slots: 5 (fixed for all players)
  → Assign most-used active skills here
  → Instant access via hotbar/keybind
  → Strategic choice: which 5 deserve prime real estate?

Unlimited Skill Accumulation:
  → Learn as many skills as you earn
  → All skills accessible through skill menu
  → Menu access slower than quick slots
  → Search/filter handles UI complexity

Passive Skills: No slots needed
  → Always active once learned
  → Unlimited accumulation
  → No management overhead
```

### 2.2 Quick Slot Management UI

```
┌────────────────────────────────────────────┐
│         QUICK SKILL SLOTS (5/5)            │
├────────────────────────────────────────────┤
│                                            │
│ [1] [Power Strike]    Active    Cooldown: 5s│
│ [2] [Quick Parry]     Active    Cooldown: 8s│
│ [3] [Berserker Rage]  Toggle    Cost: 2/s  │
│ [4] [Whirlwind]       Active    Cooldown: 30s│
│ [5] [Shield Wall]     Active    Cooldown: 60s│
│                                            │
│ Click slot to assign different skill       │
│                                            │
│ [Open Full Skill Menu →]                   │
└────────────────────────────────────────────┘

Rules:
- Only active skills can occupy quick slots
- Passives never use slots (always on)
- Quick slots provide instant access
- Skills not in quick slots: access via menu
- Can reassign anytime (except during combat/rest)
```

### 2.3 Full Skill Menu UI

```
┌────────────────────────────────────────────┐
│            ALL SKILLS (47 learned)         │
├────────────────────────────────────────────┤
│                                            │
│ 🔍 [Search skills...]                      │
│                                            │
│ Filter: [All▼] [Active] [Passive]          │
│ Sort:   [Name▼] [Tier] [Class] [Rarity]    │
│                                            │
│ ┌──────────────────────────────────────┐  │
│ │ ⭐ [Power Strike]     Active         │  │
│ │    Warrior • Common                  │  │
│ │    Currently in: Quick Slot 1        │  │
│ │    [Remove from Quick Slot]          │  │
│ └──────────────────────────────────────┘  │
│                                            │
│ ┌──────────────────────────────────────┐  │
│ │ [Cleave]            Active           │  │
│ │    Warrior • Rare                    │  │
│ │    [Add to Quick Slot →]             │  │
│ └──────────────────────────────────────┘  │
│                                            │
│ ┌──────────────────────────────────────┐  │
│ │ [Weapon Mastery]    Passive          │  │
│ │    Warrior • Common                  │  │
│ │    ✅ Always active - no slot needed  │  │
│ └──────────────────────────────────────┘  │
│                                            │
│ [Load More...]                             │
└────────────────────────────────────────────┘

Features:
- Search by name, class, tier
- Filter by active/passive
- Sort by various criteria
- Quick slot assignment/removal
- No limit on total skills learned
```

### 2.4 Idle Loop Skill Configuration

```
┌────────────────────────────────────────────┐
│         IDLE LOOP SKILL CONFIGURATION      │
├────────────────────────────────────────────┤
│                                            │
│ When idle, use these skills in order:      │
│                                            │
│ Priority 1: [Power Strike]                 │
│   Trigger: Enemy HP > 80%                  │
│                                            │
│ Priority 2: [Quick Parry]                  │
│   Trigger: Under attack                    │
│                                            │
│ Priority 3: [Berserker Rage]               │
│   Trigger: Toggle - Always On              │
│                                            │
│ Fallback: [Basic Attack]                   │
│   Trigger: No other skill available        │
│                                            │
│ [Save Configuration]                       │
└────────────────────────────────────────────┘

Rules:
- Only quick slot skills can be used in idle loop
- Condition-based triggers
- Priority order: 1 → 2 → 3 → Fallback
- Passive skills always apply (not shown here)
```

---

## 3. Skill Acquisition & Rewards

### 3.1 Level Up Skill Offers

When a class levels up, players are offered skills as rewards:

```
╔════════════════════════════════════════════════╗
║          [Warrior - Journeyman]                 ║
║          Level 5 → Level 6                      ║
╠════════════════════════════════════════════════╣
║                                                ║
║ ACHIEVEMENT BONUS ACTIVE:                       ║
║ ⚔️ Defeated Level 35 mob as Level 15            ║
║    +50% chance for higher tier skill            ║
║                                                ║
║ SKILL REWARDS (Choose at least 1):             ║
║                                                ║
║ ☑ [Power Strike]     Active  Common            ║
║    "A powerful overhead strike dealing 150%    ║
║     weapon damage. Cooldown: 5s"                ║
║    → warrior.journeyman tag match               ║
║                                                ║
║ ☑ [Quick Parry]      Active  Common            ║
║    "Riposte after successful parry.            ║
║     Cooldown: 8s"                               ║
║    → warrior tag match (parent)                 ║
║                                                ║
║ ☑ [Godslayer Strike] Active  LEGENDARY ⚡      ║
║    "Strike with divine power. Deals 500%       ║
║     damage to enemies 20+ levels higher.        ║
║     Cooldown: 120s"                             ║
║    → warrior.journeyman tag match               ║
║    → ⚡ Achievement unlocked: Legendary skill!  ║
║                                                ║
║ ☐ [Intimidating Shout] Active  Rare             ║
║    "Fear nearby enemies for 3s.                 ║
║     Cooldown: 30s"                              ║
║                                                ║
║   [Accept Selected Skills]                     ║
╚════════════════════════════════════════════════╝

Default: All checkboxes CHECKED (accept all)
Uncheck to REFUSE (skill still learned, goes to backlog)
```

### 3.2 Skill Backlog System

```
╔══════════════════════════════════════╗
║         SKILL BACKLOG (12 items)     ║
╠══════════════════════════════════════╣
║                                      ║
║ 🔍 [Search backlog...]               ║
║                                      ║
║ WARRIOR SKILLS (4):                  ║
║ ☑ [Cleave] - Refused at Lv.7         ║
║   Note: "Want more defense first"    ║
║ ☑ [Shield Wall] - Refused at Lv.10   ║
║                                      ║
║ BLACKSMITH SKILLS (5):               ║
║ ☑ [Efficient Forge] - Refused at Lv.3║
║ ☑ [Material Expert] - Refused at Lv.8║
║                                      ║
║ MERCHANT SKILLS (3):                 ║
║ ☑ [Haggle] - Refused at Lv.5         ║
║                                      ║
║ [Accept Selected Skills]             ║
║ [← Back to Level Up Event]           ║
╚══════════════════════════════════════╝

Backlog Rules:
- Persists across all level up events
- No limit to backlog size
- Items remain until accepted
- Can be accessed from character sheet anytime
- Shows notes for why refused
- Grouped by class for clarity
- Searchable and filterable
```

### 3.3 Tag-Based Skill Filtering

Skills are filtered during level up by class tag + class tier:

```
FILTERING LOGIC:

Player: [Warrior - Journeyman] (Level 25)
Class Tag: warrior.journeyman

Level Up Event:
  1. Query all skills with matching tags:
     - Exact match: warrior.journeyman.*
     - Parent match: warrior.*

  2. Filter by class-specific skills:
     - Skills tagged warrior.journeyman (highest priority)
     - Skills tagged warrior (also eligible)

  3. Apply rarity formula:
     - Base weighted random
     - Tag affinity bonus
     - Achievement bonus

  4. Present filtered skills as rewards

EXAMPLE FILTER:
┌─────────────────────────────────────────┐
│ Skills matching: warrior.journeyman     │
├─────────────────────────────────────────┤
│ [Power Strike]     warrior.journeyman ✅│
│ [Quick Parry]      warrior.*          ✅│
│ [Berserker Rage]   warrior.journeyman ✅│
│                                            │
│ Skills NOT matching:                      │
│ [Fireball]          magic.fire          ❌│
│ [Efficient Forge]   blacksmith          ❌│
└─────────────────────────────────────────┘
```

---

## 4. Skill Rarity & Achievement System

### 4.1 Rarity Tiers

Skills have 6 tiers of increasing power:

```
RARITY TIERS:

1. Common      (Most frequent, basic effects)
2. Uncommon    (Slightly improved, minor bonuses)
3. Rare        (Significant power, notable effects)
4. Epic        (Major impact, unique mechanics)
5. Legendary   (Extremely rare, game-changing)
6. Mythic      (Unique, one-of-a-kind, future content)

Power Scaling:
- Common: 100% baseline
- Uncommon: 150% effectiveness
- Rare: 250% effectiveness
- Epic: 500% effectiveness
- Legendary: 1000% effectiveness
- Mythic: Variable, unique implementations
```

### 4.2 Rarity Formula

```
RARITY DETERMINATION:

Base Chances:
  Common:      60%
  Uncommon:    25%
  Rare:        12%
  Epic:        2.5%
  Legendary:   0.5%
  Mythic:      0% (not in base pool)

Modifiers:

1. Tag Affinity Bonus:
   For every 1000 XP above threshold:
   → +5% to next higher tier
   → -5% from current tier

   Example:
   Threshold: 1,000 XP
   Your bucket: 10,000 XP
   Excess: 9,000 XP
   Bonus: +45% to higher tiers
   Capped at: +50% maximum

2. Achievement Bonus (Extraordinary Feats):
   See §4.3 for full achievement list

3. Class Level Bonus (minor):
   Class Level × 0.2% to higher tiers
   Level 50: +10% to higher tiers

FINAL CALCULATION:
  Roll = BaseChance + TagAffinity + AchievementBonus + ClassLevel
  (maximum +100% total bonus from all sources)
```

### 4.3 Legendary Achievement System

Legendary achievements trigger early unlocks and bonus rarity chances:

```
UNIVERSAL LEGENDARY ACHIEVEMENTS:

Combat:
  ⚔️ David's Victory
     Requirement: Kill enemy 20+ levels higher
     Bonus: +50% to next tier, guaranteed Rare+
     Effect: Can unlock Legendary skills early

  ⚔️ Untouchable
     Requirement: Win fight without taking damage
     Bonus: +30% to next tier
     Effect: Demonstrates mastery

  ⚔️ Last Stand
     Requirement: Survive with <10% HP against 5+ enemies
     Bonus: +35% to next tier
     Effect: Against all odds

Dungeon:
  🏰 Solo Conqueror
     Requirement: Complete dungeon solo (any class)
     Bonus: +40% to next tier, guaranteed Rare+
     Effect: Individual achievement

  🏰 Speed Runner
     Requirement: Complete dungeon in <50% of expected time
     Bonus: +25% to next tier
     Effect: Efficiency rewarded

Crafting:
  🔱 Masterwork Discovery
     Requirement: Craft rare item at 50% of required level
     Bonus: +30% to next tier
     Effect: Early crafting success

  🔱 Perfect Craft
     Requirement: Craft maximum quality item
     Bonus: +20% to next tier
     Effect: Perfectionism

Exploration:
  🗺️ Pioneer
     Requirement: Discover location before 99% of server
     Bonus: +35% to next tier
     Effect: Exploration rewarded

Social:
  🤝 Network Builder
     Requirement: Complete trade with 100 unique players
     Bonus: +25% to next tier
     Effect: Community engagement
```

```
CLASS-SPECIFIC LEGENDARY ACHIEVEMENTS:

Warrior:
  ⚔️ Bladestorm
     Kill 5 enemies with single attack within 2 seconds
     Bonus: +40% to next tier
     Unlocks: [Hurricane of Steel] skill

  ⚔️ Immortal Defender
     Prevent 1000 damage to allies in single combat
     Bonus: +35% to next tier
     Unlocks: [Guardian Angel] skill

Blacksmith:
  🔱 Artisan's Touch
     Forge weapon with 100+ quality
     Bonus: +45% to next tier
     Unlocks: [Masterwork Forge] skill

  🔱 Resource Efficiency
     Craft item using 50% less materials
     Bonus: +30% to next tier
     Unlocks: [Efficient Creation] skill

Merchant:
  💰 Deal of a Lifetime
     Complete single trade with 10,000+ gold profit
     Bonus: +50% to next tier
     Unlocks: [Market Manipulator] skill

  💰 Galactic Trade Network
     Trade with players in 10 different regions
     Bonus: +35% to next tier
     Unlocks: [Cosmic Commerce] skill

(Each class has 3-5 legendary achievements)
```

### 4.4 Achievement UI

```
┌────────────────────────────────────────────┐
│            LEGENDARY ACHIEVEMENTS           │
├────────────────────────────────────────────┤
│                                            │
│ 🔓 UNLOCKED TODAY:                         │
│ ⚔️ David's Victory                         │
│    "Defeated Level 40 mob as Level 15"     │
│    → +50% bonus to skill rarity            │
│    → Guaranteed Rare+ skill offer          │
│                                            │
│ 📋 IN PROGRESS:                            │
│ 🏰 Solo Conqueror (0/1)                    │
│    "Complete dungeon solo"                 │
│    Progress: Not attempted                 │
│                                            │
│ ⚔️ Bladestorm (2/5)                        │
│    "Kill 5 enemies within 2 seconds"       │
│    Progress: 2/5 completed                 │
│    Best: 3 enemies (65% of goal)           │
│                                            │
│ 🔱 Masterwork Discovery (1/1) ✅           │
│    "Craft rare item early"                 │
│    Completed: Yesterday                    │
│                                            │
│ 🔒 LOCKED:                                 │
│ 🤝 Network Builder (12/100)                 │
│    "Trade with 100 unique players"         │
│                                            │
│ [View Full Achievement List →]             │
└────────────────────────────────────────────┘
```

### 4.5 Early Legendary Unlock Example

```
SCENARIO: Underdog Victory

Player State:
  → Level 15 [Warrior]
  → combat.melee bucket: 2,000 XP
  → No legendary skills unlocked yet

Event:
  → Encounters Level 40 boss mob
  → Takes 20 minutes, uses all resources
  → Narrow victory with 1 HP remaining

Achievement Unlocked:
  ⚔️ David's Victory (+25 level difference)

Level Up Event Triggered:
  → Achievement bonus: +50% to higher tiers
  → Tag affinity bonus: +5% (2000 XP vs 1000 threshold)
  → Total bonus: +55%

Skill Offered:
  → [Godslayer Strike] (LEGENDARY)
  → "Strike with divine fury. Deals 500% weapon
     damage. Cooldown: 120 seconds"
  → Normally requires Level 40+
  → Achievement unlocked it 25 levels early!

Player Impact:
  → Powerful skill enables tackling harder content
  → Creates memorable story ("I got Godslayer at level 15!")
  → Incentivizes attempting difficult challenges
  → Rewards skill over grinding
```

---

## 5. Excluding Tags Mechanics

### 5.1 Soft Penalty System

Some classes have excluding tags that make acquisition harder:

```
EXCLUDING TAG MECHANICS:

Class: [Paladin]
  Excluding Tag: magic.dark

  Logic:
    IF player.magic.dark ≥ 50 XP:
        [Paladin] requirements = 2× normal
        Show: "Your affinity with Dark Magic clouds
               your path to the Light. Greater devotion
               required to overcome this."

        Normal requirement: combat.divine ≥ 1000 XP
        With dark magic: combat.divine ≥ 2000 XP (2×)

    Can still acquire, but requires 2× effort

  Rationale:
    → Allows redemption arcs
    → Fallen paladin can return to light
    → Harder but not impossible
```

### 5.2 Exclusion Thresholds

```
EXCLUSION THRESHOLDS BY CLASS TIER:

Basic Classes (Apprentice):
  → Excluding tag threshold: 50 XP
  → Penalty: 2× requirements
  → Example: [Priest] excludes magic.dark at 50+

Advanced Classes (Journeyman):
  → Excluding tag threshold: 100 XP
  → Penalty: 2× requirements
  → Example: [High Priest] excludes magic.dark at 100+

Master Classes (Master):
  → Excluding tag threshold: 200 XP
  → Penalty: 3× requirements
  → Example: [Saint] excludes magic.dark at 200+

Rationale:
  → Higher classes require stronger commitment
  → Harder to unlock if player has conflicting affinity
  → Prevents "dabbling in everything"
```

### 5.3 Redemption Path UI

```
┌────────────────────────────────────────────┐
│              CLASS LOCKED                  │
├────────────────────────────────────────────┤
│                                            │
│ [Paladin] class is currently locked        │
│                                            │
│ Reason:                                    │
│ Your magic.dark affinity (850 XP) prevents │
│ the path of the Paladin.                   │
│                                            │
│ Requirements to unlock:                    │
│ • combat.divine ≥ 2000 XP (2× normal)     │
│ • OR reduce magic.dark below 50 XP         │
│                                            │
│ Redemption Options:                        │
│                                            │
│ [1] Atonement Quest                        │
│     "Redeem yourself through 10+ hours of  │
│      dedicated service to the Light"       │
│     → Completing removes magic.dark tag    │
│                                            │
│ [2] Wait for Decay                         │
│     "Stop using dark magic for 30 days"    │
│     → magic.dark will naturally decay      │
│     Note: XP buckets are permanent, but    │
│           decay applies to excluding tags  │
│                                            │
│ [3] Premium Respec (1000 gems)             │
│     "Instantly remove all excluding tags"  │
│     → Immediate access to Paladin path    │
│                                            │
│ [Cancel]                                   │
└────────────────────────────────────────────┘
```

---

## 6. Recipe & Spell Discovery

### 6.1 Recipe Discovery Mechanics

```
RECIPE DISCOVERY:

Bucket XP Thresholds:
  → craft.smithing.weapon ≥ 500 XP
     Unlocks: Basic Sword Recipe, Basic Axe Recipe
  → craft.smithing.armor ≥ 1000 XP
     Unlocks: Chainmail Recipe, Plate Helm Recipe
  → craft.smithing.weapon ≥ 5000 XP
     Unlocks: Superior Sword Recipe (quality tier 2)

Quality Tiers:
  → Basic (quality 1-10): 500 XP threshold
  → Standard (quality 11-25): 2000 XP threshold
  → Superior (quality 26-50): 5000 XP threshold
  → Epic (quality 51-75): 15000 XP threshold
  → Legendary (quality 76-100): 50000 XP threshold

Discovery Chance:
  → Base: 5% chance per rest cycle when threshold met
  → Bonus: +1% per 1000 XP above threshold
  → Cap: 25% maximum discovery chance

Example:
  craft.smithing.weapon: 8000 XP
  Threshold for Superior: 5000 XP
  Excess: 3000 XP → +3% bonus
  Final: 5% + 3% = 8% discovery chance per rest

Achievement Bonus Also Applies:
  → Crafting masterwork early triggers bonus
  → Same achievement system as skills
```

### 6.2 Spell Discovery Mechanics

```
SPELL DISCOVERY:

Magic Bucket XP:
  → magic.fire ≥ 500 XP
     Unlocks: Fireball, Flame Ward
  → magic.ice ≥ 1000 XP
     Unlocks: Ice Lance, Frost Armor
  → magic.fire ≥ 10000 XP
     Unlocks: Inferno (Epic tier spell)

Rarity Tiers:
  → Novice (Level 1-10): 500 XP threshold
  → Adept (Level 11-25): 2500 XP threshold
  → Expert (Level 26-40): 10000 XP threshold
  → Master (Level 41-50): 40000 XP threshold

Research Mechanic:
  → Active research action
  → Consumes time and gold
  → Higher research skill = faster discovery
  → Can research specific spell types

Example Research:
  Target: Fireball spell
  Player magic.fire: 3000 XP
  Base time: 8 hours
  Bonus: -1 hour per 500 XP above threshold
  Final: 8 - (3000-2500)/500 = 7 hours research time
```

### 6.3 Cross-Class Recipe Access

```
CROSS-CLASS RECIPES:

Blacksmith + Merchant:
  → [Weapon Smith] class + [Merchant] class
  → Unlocks: Premium Weapon Pricing recipe
  → Effect: Sell crafted weapons at +20% price

Alchemist + Herbalist:
  → [Alchemist] class + [Herbalist] class
  → Unlocks: Wild Craft recipe
  → Effect: Can craft potions while gathering

Chef + Merchant:
  → [Chef] class + [Merchant] class
  → Unlocks: Catering Service recipe
  → Effect: Sell food buff services

Rules:
  → Requires both classes active
  → Recipes vanish if class deactivated
  → Reactivating class restores recipes
  → Synergy bonuses encourage multi-classing
```

---

## 7. Implementation Notes

### 7.1 Data Structure

```csharp
public class Skill
{
    public string SkillId { get; set; }
    public string Name { get; set; }
    public SkillType Type { get; set; }  // Active, Passive
    public SkillRarity Rarity { get; set; }  // Common, Rare, Legendary, etc.
    public List<string> Tags { get; set; }  // warrior.journeyman, warrior, etc.
    public List<TagRequirement> Requirements { get; set; }
    public List<string> ExcludingTags { get; set; }
    public string Description { get; set; }
    public int CooldownSeconds { get; set; }
    public int ResourceCost { get; set; }
    public bool IsLegendary { get; set; }
}

public class PlayerSkill
{
    public string SkillId { get; set; }
    public int QuickSlotPosition { get; set; }  // 1-5, or 0 if not in quick slot
    public DateTime LearnedDate { get; set; }
    public bool IsBacklogged { get; set; }
    public string BacklogNote { get; set; }
}

public class QuickSlotManager
{
    public const int MaxQuickSlots = 5;
    public List<string> QuickSlotSkills { get; set; }  // Exactly 5 slots

    public bool CanAssignToQuickSlot(string skillId)
    {
        var skill = GetSkill(skillId);
        return skill.Type == SkillType.Active;
    }
}

public class Achievement
{
    public string AchievementId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public bool IsUniversal { get; set; }  // True = all classes, False = class-specific
    public List<string> RequiredClasses { get; set; }
    public AchievementRequirement Requirement { get; set; }
    public int RarityBonusPercent { get; set; }  // Bonus to higher tiers
    public List<string> GuaranteedSkillUnlocks { get; set; }
}

public class PlayerAchievement
{
    public string AchievementId { get; set; }
    public bool IsUnlocked { get; set; }
    public DateTime UnlockDate { get; set; }
    public Dictionary<string, int> Progress { get; set; }  // For multi-step achievements
}

public class Recipe
{
    public string RecipeId { get; set; }
    public string Name { get; set; }
    public RecipeType Type { get; set; }  // Crafting, Cooking, Alchemy
    public List<TagRequirement> BucketThresholds { get; set; }
    public List<RecipeIngredient> Ingredients { get; set; }
    public RecipeOutput Output { get; set; }
    public int QualityTier { get; set; }  // 1-100
}
```

### 7.2 Rarity Roll Algorithm

```csharp
public SkillRarity RollSkillRarity(
    string classTag,
    long bucketXp,
    long thresholdXp,
    int classLevel,
    List<Achievement> activeAchievements)
{
    // Base weights
    var baseWeights = new Dictionary<SkillRarity, double>
    {
        { SkillRarity.Common, 60.0 },
        { SkillRarity.Uncommon, 25.0 },
        { SkillRarity.Rare, 12.0 },
        { SkillRarity.Epic, 2.5 },
        { SkillRarity.Legendary, 0.5 }
    };

    // 1. Tag affinity bonus
    var excessXp = Math.Max(0, bucketXp - thresholdXp);
    var affinityBonus = Math.Min(50, (int)(excessXp / 1000) * 5);

    // 2. Achievement bonus
    var achievementBonus = activeAchievements
        .Where(a => a.IsUnlocked)
        .Sum(a => a.RarityBonusPercent);

    // 3. Class level bonus (minor)
    var classBonus = classLevel * 0.2;

    // Total bonus capped at 100%
    var totalBonus = Math.Min(100, affinityBonus + achievementBonus + classBonus);

    // Apply bonus to shift weights toward higher tiers
    // (Implementation: weighted random with bonus adjustment)

    return RollWeightedRandom(baseWeights, totalBonus);
}
```

### 7.3 Complexity Ratings

| Component              | Implementation Complexity | Notes                          |
| ---------------------- | ------------------------- | ------------------------------ |
| Quick Slot Management  | Low (2/5)                 | Fixed 5 slots, simple UI       |
| Skill Menu (Unlimited) | Medium (3/5)              | Search, filter, pagination     |
| Tag-Based Filtering    | Low (2/5)                 | Tag hierarchy matching         |
| Rarity Formula         | Medium (3/5)              | Weighted RNG, multi-modifier   |
| Achievement System     | Medium-High (4/5)         | Tracking, triggers, UI display |
| Excluding Tags (Soft)  | Low (2/5)                 | Multiplier on requirements     |
| Recipe Discovery       | Medium (3/5)              | Bucket monitoring, RNG         |
| Cross-Class Synergy    | Medium (3/5)              | Multi-class state validation   |

---

## 8. Resolved Open Questions

This document resolves the following CRITICAL priority questions from open-questions.md:

| #   | Question                      | Resolution                                               | Status      |
| --- | ----------------------------- | -------------------------------------------------------- | ----------- |
| 2.4 | Excluding Tags for Class Acq. | Soft penalty (2× requirements) with redemption paths     | ✅ Resolved |
| 3.1 | Active Skill Limit            | Unlimited skills, 5 quick slots for rapid access         | ✅ Resolved |
| 3.2 | Skill Forgetting/Respec       | Skip - no forgetting mechanic, skills accumulate forever | ✅ Resolved |
| 3.3 | Skill Tier Tag Requirements   | Already solved - tag hierarchy handles filtering         | ✅ Resolved |
| 3.4 | Skill Rarity Roll Formula     | Tag affinity + Achievement bonus system                  | ✅ Resolved |

---

## 9. Design Decisions Record

### 9.1 Unlimited Skills with 5 Quick Slots

**Decision:** Players can accumulate unlimited skills, but only 5 can be in quick slots for instant access.

**Rationale:**

- Rewards exploration and playtime
- No "wrong" choices - can always learn more
- Quick slots create meaningful strategic choices
- Search/filter handles UI complexity
- Passive skills don't use slots (always on)

**Trade-offs:**

- Large skill lists could be overwhelming
- Mitigated by: Robust search, filtering, favorites

### 9.2 No Forgetting Mechanic

**Decision:** Skills cannot be forgotten - they accumulate forever.

**Rationale:**

- No gameplay benefit to forgetting (unlimited slots)
- Search/filter handles UI management
- Players shouldn't be punished for experimenting
- Removes complex respec mechanics

**Trade-offs:**

- Skill lists grow very large over time
- Mitigated by: Good UI, search, filters, favorites

### 9.3 Achievement-Based Rarity Bonus

**Decision:** Extraordinary achievements provide significant bonuses to skill rarity rolls, including potential early legendary unlocks.

**Rationale:**

- Creates memorable "watercooler moments" ("I got a Legendary at level 15!")
- Rewards skill over grinding
- Incentivizes attempting difficult content
- Makes gameplay feel dynamic and responsive

**Trade-offs:**

- May devalue high-tier skills if too common
- Mitigated by: Very high thresholds for achievements, low base Legendary chance

### 9.4 Soft Penalty for Excluding Tags

**Decision:** Excluding tags multiply requirements by 2× rather than hard-blocking acquisition.

**Rationale:**

- Allows redemption arcs and character growth
- Fallen paladin can return to light path
- Narrative flexibility without hard locks
- More effort required, but not impossible

**Trade-offs:**

- Reduces class identity exclusivity
- Mitigated by: 2× penalty is significant effort

### 9.5 Tag-Based Tier Filtering

**Decision:** Skill tiers are handled through tag hierarchy, not separate bucket XP thresholds.

**Rationale:**

- Leverages existing tag system
- Clean and consistent with class system
- No separate math or tracking needed
- Parent tag matching provides flexibility

**Trade-offs:**

- Less granular control over tier availability
- Mitigated by: Rarity system provides additional gating

---

## Appendix A: Complete Skill Acquisition Example

### New Player Journey (First 10 Hours)

```
1. Character Creation
   → No skills, all buckets at 0
   → Quick slots: 5 empty slots

2. Hour 0-2: Sword combat
   → combat.melee: +500 XP
   → combat.melee.sword: +350 XP
   → No class yet

3. Rest Cycle #1
   → combat.melee ≥ 1000 XP? ✗
   → No class yet, no skill offers

4. Hour 2-5: Intense sword practice
   → combat.melee: +1500 XP (total: 2000 XP)
   → combat.melee.sword: +1050 XP (total: 1400 XP)

5. Rest Cycle #2
   → combat.melee ≥ 1000 XP ✅
   → LEVEL UP EVENT
   → Accept [Warrior - Apprentice] class
   → Skill offers (filter: warrior apprentice):

     ☑ [Basic Strike]    Common  warrior.apprentice
     ☑ [Quick Slash]     Common  warrior

   → Accept both
   → Skills learned: 2 total
   → Quick slots: Assign both to slots 1-2

6. Hour 5-10: Continue combat + level up
   → [Warrior] levels to 5
   → combat.melee bucket: 3000 XP

7. Level Up Event (Level 5)
   → Skill offers (filter: warrior):
     ☑ [Power Strike]    Common   warrior
     ☑ [Cleave]          Rare     warrior
     ☐ [Whirlwind]       Epic     warrior

   → Rarity roll: Tag affinity bonus +15% (3000 vs 1000 threshold)
   → Result: Offered Rare skill [Cleave]
   → Accept all three
   → Skills learned: 5 total
   → Quick slots: Assign to slots 1-3
   → Backlog: 0 (accepted all)

8. Bonus Event: Achievement Unlocked!
   → Encounter Level 25 boss as Level 8
   → Against all odds, win with 1 HP
   → ⚔️ David's Victory achievement unlocked!
   → +50% bonus to next skill rarity

9. Rest Cycle with Achievement Bonus
   → Level Up Event with +50% rarity bonus
   → Skill offered: [Godslayer Strike] (LEGENDARY!)
   → "Strike with divine fury. 500% damage."
   → Normally requires Level 40+
   → Achievement unlocked it 32 levels early!
   → Accept legendary skill
   → Assign to Quick Slot 1
   → Skills learned: 6 total (including 1 Legendary!)

Player Status:
  → Level 8 [Warrior - Apprentice]
  → 6 skills learned (1 Legendary!)
  → Quick slots: 4 assigned, 1 empty
  → Powerful for level, memorable story
```

---

## Appendix B: Multi-Class Skill Management Example

### Experienced Player with 3 Classes

```
Current State:
  [Warrior - Journeyman] (Level 25)
  [Blacksmith - Apprentice] (Level 12)
  [Merchant - Apprentice] (Level 8)

Skills Learned: 47 total
Quick Slots: 5/5 assigned

Quick Slot Configuration:
  [1] [Godslayer Strike]     Legendary  Warrior
  [2] [Power Strike]         Common     Warrior
  [3] [Quick Parry]          Common     Warrior
  [4] [Efficient Forge]      Common     Blacksmith
  [5] [Haggle]               Common     Merchant

Skill Menu (Full List):
  Active Skills: 31 learned
    - 5 in quick slots (instant access)
    - 26 accessible via menu (slower)

  Passive Skills: 16 learned
    - All active, no slots needed
    - [Weapon Mastery] +15% melee damage
    - [Forge Mastery] +20% crafting speed
    - [Merchant Network] +10% sell prices
    - [Iron Skin] +5 defense
    - etc.

Menu Features:
  - Search: "sword" → shows 5 sword-related skills
  - Filter: Active, Warrior, Rare+ → shows 8 skills
  - Sort: Name, Rarity, Class, Tier
  - Quick slot assignment: Click skill → Select slot 1-5
  - No limit on total skills learned

Backlog: 12 previously refused skills
  - Can accept anytime from character sheet
  - Shows notes for why refused
  - Searchable like main skill list

Legendary Achievements Unlocked:
  ⚔️ David's Victory (Level 8 vs Level 25)
  🏰 Solo Conqueror (Completed dungeon solo)
  🔱 Masterwork Discovery (Crafted rare item early)

Bonus: +25% active to skill rarity from achievements
```

---

_Document Version 1.0 - Authoritative specification for Skill & Recipe System_
_All CRITICAL priority skill questions resolved through collaborative design_
_Future versions will add: advanced synergy mechanics, mythic tier skills, guild achievements_
