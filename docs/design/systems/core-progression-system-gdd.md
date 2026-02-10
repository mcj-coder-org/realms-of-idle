---
type: system
scope: detailed
status: authoritative
version: 1.0.0
created: 2026-02-03
updated: 2026-02-03
subjects: [progression, xp, buckets, classes, level-up, multi-classing]
dependencies: [idle-inn-gdd.md, idle-game-overview.md]
---

# Core Progression System - Authoritative Game Design

## Executive Summary

The Core Progression System governs all player advancement through the tag-based XP bucket system. It defines how actions translate to experience, how XP accumulates in permanent buckets, how players become eligible for classes, and how levels are calculated after class acceptance. This system is the foundation for class acquisition, skill unlocks, and all vertical progression in Realms of Idle.

**This document resolves:**

- 5 CRITICAL priority questions from open-questions.md
- All questions blocking prototype/alpha development

**Design Philosophy:** Progression follows "organic growth" - players become what they do. XP buckets are permanent, preserving player progress forever. Early levels come quickly to hook players, mid-levels require engagement, and high levels demand dedication. The system rewards consistent play without punishing reasonable breaks.

---

## 1. XP Curve & Level Progression

### 1.1 XP Required Formula

The baseline XP requirement from level 1 to level 50 follows classical MMORPG scaling:

```
XP Required = Base × (Level ^ Exponent) × DifficultyModifier
Base = 100
Exponent = 1.5
DifficultyModifier = 1.0 (standard)

Examples:
Level 1→2:   100 XP
Level 5→6:   1,118 XP
Level 10→11: 3,162 XP (milestone level)
Level 20→21: 8,944 XP (milestone level)
Level 30→31: 15,588 XP (milestone level)
Level 40→41: 25,298 XP (milestone level)
Level 50→51: 35,355 XP (milestone level)
```

### 1.2 Milestone Modifiers

At milestone levels (10, 20, 30, 40, 50), the XP requirement doubles to create meaningful progression gates:

```
Milestone Levels: 10, 20, 30, 40, 50
Milestone Modifier: ×2.0
Cumulative Effect: Level 50 requires ~2.5M total XP from level 1
```

### 1.3 Level Ranges & Population Distribution

| Level Range | Population % | Description                                |
| ----------- | ------------ | ------------------------------------------ |
| 1-10        | 60%          | Beginners and casual players               |
| 11-25       | 25%          | Dedicated players, competent professionals |
| 26-40       | 10%          | Veterans, server-notable figures           |
| 41-50       | 4%           | Elite, regional influence                  |
| 51+         | 0.9%         | Legendary, world events                    |

### 1.4 Rationale

- **Early-game hook:** Levels 1-10 take ~2 hours total for casual players
- **Mid-game engagement:** Levels 11-30 require deliberate play patterns
- **Endgame dedication:** Levels 40+ represent server-notable achievement
- **Familiar curve:** Players intuitively understand power-1.5 scaling from MMOs

---

## 2. XP Bucket System

### 2.1 Action XP Distribution

Every action grants XP distributed across relevant tag buckets:

```
Action: Swing sword (combat.melee.sword.parry)
Base XP: 10

Distribution Rule: Most specific gets most XP
  → combat.melee.sword.parry: +10 XP (100% - most specific)
  → combat.melee.sword: +7 XP (70% - parent)
  → combat.melee: +5 XP (50% - grandparent)

Rule: XP flows up the hierarchy with diminishing returns
  Specific action → All parent buckets get reduced share
```

**Formula:**

```
Most specific tag: 100% of base XP
Each parent level: -30% from previous level
  Level 0 (action): 100%
  Level 1 (parent): 70%
  Level 2 (grandparent): 50%
  Level 3 (great-grandparent): 35%
```

**Example gathering action:**

```
Action: Gather Moonpetal Flower (gather.herbalism.flower.moonpetal)
Base XP: 10

Bucket distribution:
  → gather.herbalism.flower.moonpetal: +10 XP (specific action)
  → gather.herbalism.flower: +7 XP (flower type)
  → gather.herbalism: +5 XP (general skill)
```

### 2.2 Bucket Persistence

**XP buckets are permanent.** Once earned, XP never decays or decreases.

**Benefits:**

- Players can take breaks without penalty
- Progress is always preserved
- Encourages experimentation (no "wrong" paths)

**Time inactive examples:**

| Time inactive | Bucket XP remaining (started at 100) |
| ------------- | ------------------------------------ |
| 1 week        | 100 (no decay)                       |
| 1 month       | 100                                  |
| 3 months      | 100                                  |
| 6 months      | 100                                  |

**Note:** Bucket XP never decreases.

### 2.3 Purposes of XP Buckets

XP buckets serve multiple progression systems:

**1. Class Acquisition (Primary Purpose)**

```
[Herbalist] Class Requirement:
  gather.herbalism ≥ 1,000 XP

When threshold reached → Eligible at next Level Up Event
```

**2. Class Evolution**

```
[Warrior] at Level 10 evolves based on bucket distribution:
  combat.melee.sword = 60% of combat.* buckets → [Blade Dancer]
  combat.defense.shield = 40% of combat.* buckets → [Shield Bearer]
```

**3. Recipe & Spell Discovery/Eligibility**

```
[Minor Healing Potion] Recipe:
  Unlock: craft.alchemy.potion bucket ≥ 500 XP
        AND gather.herbalism.flower bucket ≥ 250 XP

Higher bucket XP = Higher discovery chance from random sources
```

**4. Skill Eligibility (Training & Level-Up Rewards)**

```
[Power Strike] Skill:
  Requires: combat.melee bucket ≥ 1,500 XP
  Class: [Warrior] OR bucket threshold alone
```

---

## 3. Class Eligibility & Acquisition

### 3.1 Rest Triggers Level Up Events

```
Rest Cycle:
  10 minutes rest per 4 hours of activity
  Character cannot take actions while resting

Level Up Event occurs during rest when:
  ✓ Any XP bucket reaches class eligibility threshold
  ✓ Current class(es) have enough XP for next level
  → Event interrupts rest, presents choices
  → Rest resumes after player makes selections
```

### 3.2 Eligibility Check

Classes are unlocked when XP buckets reach specific thresholds:

```
[Herbalist] Class Requirements:
  gather.herbalism bucket ≥ 1,000 XP

Player Status:
  gather.herbalism bucket: 1,247 XP
  → ELIGIBLE for [Herbalist]

At next rest: Level Up Event triggered
```

### 3.3 Level Up Event Interface

```
╔══════════════════════════════════════════════════╗
║          LEVEL UP EVENT - Resting...             ║
╠══════════════════════════════════════════════════╣
║                                                  ║
║ NEW CLASS OFFERS:                                ║
║ ☑ [Herbalist]                                   ║
║   Requires: gather.herbalism ≥ 1,000 XP         ║
║   └─ Skills offered if accepted:                ║
║       ☑ [Green Thumb]                           ║
║       ☑ [Herb Identification]                   ║
║                                                  ║
║ EXISTING CLASS LEVEL UPS:                        ║
║                                                  ║
║ [Warrior] Level 5 → Level 6                      ║
║   Evolution Choice: ( ) Blade Dancer            ║
║                    ( ) Shield Bearer            ║
║   Skill Rewards:                                 ║
║     ☑ [Power Strike]                             ║
║     ☑ [Parry]                                   ║
║                                                  ║
║ [Blacksmith] Level 3 → Level 4                  ║
║   No evolution available until Level 10         ║
║   Skill Rewards:                                 ║
║     ☑ [Efficient Forge]                         ║
║                                                  ║
║ [Accept Selected]  [View Backlog →]             ║
╚══════════════════════════════════════════════════╝

Default: All checkboxes CHECKED (accept)
Uncheck to REFUSE (adds to backlog)
```

### 3.4 Accept/Refuse Rules

```
NEW CLASS OFFERS:
  ☑ Class checked = Accept class AND all checked skills
  ☐ Class unchecked = Refuse class (skills auto-refused)

  Rules:
  - Can uncheck individual skills while accepting class
  - Unchecking the class unchecks ALL its skills automatically

EXISTING CLASS LEVEL UPS:
  ✓ Level up is MANDATORY (cannot refuse)
  ✓ Can ONLY refuse:
    - Evolution choice (defer to next level up)
    - Individual skill rewards
    - Class consolidation (prestige paths)

  Rules:
  - Evolution choice required if available
  - Can refuse skills (they go to backlog)
  - Cannot refuse the level itself
```

### 3.5 Backlog Screen (Separate)

```
╔══════════════════════════════════════╗
║          CLASS & SKILL BACKLOG       ║
╠══════════════════════════════════════╣
║                                      ║
║ PREVIOUSLY REFUSED CLASSES:          ║
║ ☑ [Warrior]                          ║
║   Note: "Want to focus on crafting   ║
║         first"                        ║
║ ☑ [Blacksmith]                       ║
║                                      ║
║ PREVIOUSLY REFUSED SKILLS:           ║
║                                      ║
║ WARRIOR SKILLS:                      ║
║ ☑ [Power Strike] - Refused Lv.5      ║
║ ☑ [Cleave] - Refused Lv.7            ║
║                                      ║
║ BLACKSMITH SKILLS:                   ║
║ ☑ [Efficient Forge] - Refused Lv.3  ║
║                                      ║
║ [Accept Selected]  [← Back to Event] ║
╚══════════════════════════════════════╝
```

### 3.6 Backlog Mechanics

```
Backlog Rules:
- Persists across all Level Up events
- No limit to backlog size
- Items remain until accepted
- Can be accessed from character sheet between events
- Items show "Why you refused" notes (player annotation)
- Grouped by class for clarity
```

### 3.7 Skill Pool Logic by Class

```
Skill Assignment by Class:
  → Each class has its own skill pool
  → Skills filtered by class tags
  → When [Warrior] levels: Offer warrior skills only
  → When [Blacksmith] levels: Offer blacksmith skills only

Multiple Classes Leveling:
  → Each class gets separate skill offer list
  → Skills are class-specific, not shared

Example:
  [Warrior] Lv.5 → [Power Strike], [Parry]
  [Blacksmith] Lv.3 → [Efficient Forge]

  Clear which skills belong to which class
```

### 3.8 Important: What Players See

```
✓ Players ONLY see skills they've been OFFERED as rewards
✓ NOT a catalog of all possible skills/recipes/spells
✓ Discovery happens through gameplay, not browsing

Example:
  Player's gather.herbalism bucket = 1,000 XP
  → Eligible for [Herbalist] class
  → Level Up event OFFERS [Herbalist]
  → Player now knows this class exists

  Player does NOT see all 50+ possible classes
  Discovery = organic exploration
```

---

## 4. Class Level Calculation

### 4.1 Dual XP Tracking

When a player accepts a class, **two separate XP counters** begin tracking:

```
Action: Swing sword (combat.melee.sword)
Base XP: 10

BEFORE accepting [Warrior]:
  → combat.melee bucket: +10 XP (permanent)
  → No class XP yet

AFTER accepting [Warrior]:
  → combat.melee bucket: +10 XP (permanent, continues)
  → [Warrior] class XP: +10 XP (starts at 0, separate counter)
```

**Key Point:** Bucket XP and Class XP are **separate and independent**.

```
Bucket XP:
  - Permanent, never resets
  - Used for class eligibility
  - Used for recipe/spell unlocks
  - Shared across all classes using that tag

Class XP:
  - Starts at 0 when class is accepted
  - Used only for that class's level
  - Separate for each held class
  - Does NOT include historical bucket XP
```

### 4.2 XP Required per Class Level

```
XP Required = Base × (Level ^ Exponent) × DifficultyModifier
Base = 100
Exponent = 1.5
DifficultyModifier = 1.0 (standard)

Examples:
Level 1 → 2: 100 XP
Level 2 → 3: 283 XP
Level 5 → 6: 1,118 XP
Level 10 → 11: 3,162 XP (milestone)
Level 20 → 21: 8,944 XP (milestone)
Level 30 → 31: 15,588 XP (milestone)
```

### 4.3 Example Progression

```
Player Journey:
━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
1. Play for 20 hours, combat.melee actions
   → combat.melee bucket: 2,500 XP earned
   → Eligible for [Warrior] class (requires 1,000 XP)

2. Level Up Event: Accept [Warrior]
   → [Warrior] Level 1 granted
   → [Warrior] class XP counter: 0 (starts fresh)

3. Continue playing combat actions
   → combat.melee bucket: 2,520 XP (+20 from new actions)
   → [Warrior] class XP: 20 XP (only new actions count)

4. After 100 more combat XP
   → [Warrior] class XP: 100 XP
   → [Warrior] Level Up: Level 1 → Level 2 ✓
```

### 4.4 Multi-Class Automatic Hierarchical XP

**All accepted classes are always active** - XP distribution is **automatic** based on tag hierarchy:

```
Player has: [Fighter], [Warrior], [Blade Dancer], [Blacksmith]

Action: Attack with Sword → Tag: combat.melee.sword → Base 100 XP

Bucket XP (always full):
  → combat.melee.sword bucket: +100 XP (full amount, no split)

Class XP (hierarchical split):
  → Blade Dancer: +50 XP (exact match: combat.melee.sword)
  → Warrior: +25 XP (parent: combat.melee)
  → Fighter: +12.5 XP (grandparent: combat)
  → Blacksmith: 0 XP (no tag match)

Action: Forge Iron Sword → Tag: craft.smithing.weapon.sword → Base 100 XP

Bucket XP:
  → craft.smithing.weapon.sword bucket: +100 XP (full)

Class XP:
  → Blacksmith: +50 XP (exact match: craft.smithing)
  → Fighter: 0 XP (no match)
  → Warrior: 0 XP (no match)
  → Blade Dancer: 0 XP (no match)
```

**Formula**:

1. Find most specific matching tag
2. Award 50% to exact match
3. Award 50% to parent level (split if multiple)
4. Continue subdividing remainder up hierarchy

**Important:** Buckets always get full XP. Class XP uses automatic hierarchical split.

---

## 5. Multi-Class Mechanics

### 5.1 All Classes Always Active

**Core Principle**: No class slots, no dormant classes - every accepted class is always active.

```
Character with 5 Classes:
  - [Fighter - Apprentice]
  - [Warrior - Journeyman]
  - [Blade Dancer - Apprentice]
  - [Blacksmith - Journeyman]
  - [Miner - Apprentice]

All 5 classes gain XP from every action (hierarchical split)
No activation required, no slots to manage
```

### 5.2 Primary Class Determination

**Primary Class**: Highest-leveled **Tier 2** class (fallback: Tier 1)

```
Example: Multiple Tier 2 Classes
  Classes:
    [Warrior - Level 15] (Tier 2) ← PRIMARY
    [Blade Dancer - Level 8] (Tier 3)
    [Blacksmith - Level 12] (Tier 2)

  Primary: Warrior (highest Tier 2)

Benefits:
- Displayed in character nameplate
- Determines UI class icon
- No mechanical advantages (all classes always active)
```

### 5.3 Foundational Class Coverage

If no class tracks an action's tag, XP flows to **Foundational** classes (Tier 1):

```
Action: Attack with Sword → combat.melee.sword
Character has: [Cook], [Miner] (neither track combat tags)

Result:
  Cook:  50 XP (Foundational class)
  Miner: 50 XP (Foundational class)

Rationale: XP always counts, even for non-matching actions
```

**Foundational Classes** (Tier 1):

- Fighter, Crafter, Gatherer, Host, Trader, Scholar, Socialite

**Rule**: If no classes match action tags, split XP equally among all Foundational classes.

### 5.4 Class Synergy Bonuses

Some class combinations unlock unique hybrid skills:

```
[Chef] + [Alchemist]:
  Shared tags: craft.*.infuse
  Unlocks: [Culinary Potions]
  "Brew potions that provide both healing and buffs"

[Warrior] + [Merchant]:
  Shared tags: combat.melee + social.trade
  Unlocks: [Caravan Guard]
  "Better combat pay rates, merchant reputation bonuses"

[Miner] + [Blacksmith]:
  Shared tags: gather.mining.ore + craft.smithing
  Unlocks: [Material Intuition]
  "Can sense ore quality before mining"
```

---

## 6. Idle Loop Efficiency

### 6.1 Diminishing Returns Curve

Extended AFK play has reduced efficiency to reward engagement:

```
Hours 1-4:     100% efficiency (full XP, full drops)
Hours 4-8:     75% efficiency
Hours 8-16:    50% efficiency
Hours 16-24:   25% efficiency
24+ hours:     10% efficiency (maintenance mode)

Efficiency affects:
- Class XP gain (reduced)
- Rare drop chances (reduced)
- Quality of results (reduced)

Efficiency does NOT affect:
- Bucket XP (always 100%)
- Class eligibility
```

### 6.2 Resetting Efficiency

Efficiency resets to 100% through player engagement:

```
Reset Triggers:
✓ Logging in and taking any manual action
✓ Changing activity loop configuration
✓ Completing a level-up event
✓ Social interaction (trade, party, guild)
✓ Premium "Emergency Refresh" (consumable)
```

### 6.3 Rest Cycles (Mandatory)

Rest is required for class/skill acquisition and provides rested bonuses:

```
Rest Requirement:
  10 minutes rest per 4 hours of activity
  Rest can be queued in activity loops
  Character cannot take actions while resting

Level-Up & Class Grant Timing:
  ✓ Only occurs during rest cycles
  ✓ Backlogged if player delays resting
  ✓ Bulk processing: Multiple level-ups process sequentially

Rested Bonus:
  +25% XP for 1 hour after completing rest
  Stacks up to 2 hours (maximum +50% bonus)
```

---

## 7. Resolved Open Questions

This document resolves the following CRITICAL priority questions from open-questions.md:

| #   | Question                     | Resolution                                               | Status      |
| --- | ---------------------------- | -------------------------------------------------------- | ----------- |
| 1.1 | XP Curve Formula             | Classical MMORPG scaling: Base × (Level ^ 1.5)           | ✅ Resolved |
| 1.2 | Tag Affinity Decay           | **N/A** - XP buckets are permanent, no decay             | ✅ Resolved |
| 1.3 | Tag XP Per Action            | Hierarchical distribution: 100%/70%/50%/35%              | ✅ Resolved |
| 1.4 | Wildcard Tag Resolution      | Any Single Match: Max(matching tags) ≥ Requirement       | ✅ Resolved |
| 1.5 | Idle Loop Efficiency         | Decoupled: Efficiency affects class XP only, not buckets | ✅ Resolved |
| 2.1 | Multi-Class XP Distribution  | Automatic hierarchical split (50/25/12.5...)             | ✅ Resolved |
| 2.2 | Class Evolution Presentation | Level Up Event with choices, deferred to backlog         | ✅ Resolved |
| 2.3 | Class Evolution Rejection    | Yes, can defer evolution choices to backlog              | ✅ Resolved |

---

## 8. Cross-References

Related Documents:

- **[idle-inn-gdd.md](../idle-inn-gdd.md)** - Core game design with class system overview
- **[idle-game-overview.md](../idle-game-overview.md)** - Complete game overview with progression philosophy
- **[Class System GDD](./class-system-gdd.md)** - Detailed class mechanics (TODO)
- **[Skill & Recipe System GDD](./skill-recipe-system-gdd.md)** - Skill acquisition and crafting (TODO)

---

## 9. Implementation Notes

### 9.1 Data Structure

```csharp
public class PlayerProgression
{
    // XP Buckets (permanent, never decay)
    public Dictionary<string, long> XPBuckets { get; set; }

    // All Classes (always active)
    public List<PlayerClass> Classes { get; set; }

    // Primary class determination
    public PlayerClass GetPrimaryClass()
    {
        // Highest-leveled Tier 2 class, fallback to Tier 1
        var tier2Classes = Classes.Where(c => c.TreeTier == 2).OrderByDescending(c => c.Level);
        if (tier2Classes.Any())
            return tier2Classes.First();

        return Classes.Where(c => c.TreeTier == 1).OrderByDescending(c => c.Level).FirstOrDefault();
    }

    // Level Up Event State
    public LevelUpEvent PendingLevelUpEvent { get; set; }
    public List<BacklogItem> ClassSkillBacklog { get; set; }
}

public class ActiveClass
{
    public string ClassId { get; set; }
    public int Level { get; set; }
    public long ClassXP { get; set; }  // Separate from bucket XP
    public int XPSplitPercentage { get; set; }  // Player-configured
}

public class XPBucket
{
    public string TagPath { get; set; }  // e.g., "combat.melee.sword"
    public long TotalXP { get; set; }  // Permanent, never decays
}
```

### 9.2 Complexity Ratings

| Component                 | Implementation Complexity | Notes                        |
| ------------------------- | ------------------------- | ---------------------------- |
| XP Bucket Distribution    | Low (2/5)                 | Simple hierarchical math     |
| Class Eligibility Check   | Low (1/5)                 | Threshold comparison         |
| Level Up Event Logic      | Medium (3/5)              | UI state management, backlog |
| Class XP Calculation      | Low (2/5)                 | Standard XP curve            |
| Hierarchical XP Split     | High (4/5)                | Recursive tag tree traversal |
| Primary Class Calculation | Low (2/5)                 | Sort by tier + level         |
| Idle Efficiency           | Low (2/5)                 | Time-based multiplier        |

---

## 10. Design Decisions Record

### 10.1 Permanent XP Buckets (No Decay)

**Decision:** XP buckets never decay or decrease.

**Rationale:**

- Player-friendly: No penalty for taking breaks
- Encourages experimentation: No "wrong" paths
- Simpler to understand: What you earn, you keep
- Reduces anxiety: Progress is always preserved

**Trade-offs:**

- Players can "hoard" XP in buckets without committing to classes
- Mitigated by: Class level XP is separate, starts fresh on acceptance

### 10.2 Hierarchical XP Distribution

**Decision:** Actions grant XP to all parent buckets with diminishing returns (100%/70%/50%/35%).

**Rationale:**

- Focused practice rewarded: Specific actions give most XP to specific tags
- General competency maintained: Parent buckets still grow
- Organic progression: Doing sword fighting improves both "sword" AND "melee" AND "combat"
- Simple formula: Easy for players to understand

### 10.3 Dual XP Tracking (Buckets + Class XP)

**Decision:** Separate tracking for bucket XP (permanent) and class XP (starts at 0 on acceptance).

**Rationale:**

- Buckets represent lifetime experience with a tag
- Class XP represents commitment to that class
- Prevents "level 50 class on day 1" from hoarded bucket XP
- Clear distinction: Eligibility vs. Progression

### 10.4 Level Up Events During Rest

**Decision:** Level up events only occur during mandatory rest cycles.

**Rationale:**

- Pacing: Prevents constant interruption of gameplay
- Ritual significance: Rest = reflection = growth
- Bulk processing: Multiple level-ups handled efficiently
- Strategic: Players plan rest timing for important unlocks

### 10.5 Default Accept with Opt-Out

**Decision:** All level up offers pre-checked (accept), uncheck to refuse.

**Rationale:**

- Fast-forward: Quick "Confirm" accepts everything
- Reduced friction: Most players want to accept rewards
- Choice preserved: Can still refuse specific items
- Psychology: "I earned this" vs. "Do I want this?"

---

## Appendix A: Complete Progression Example

### New Player Journey (First 3 Hours)

```
1. Character Creation: Spawn in starting village
   → All XP buckets at 0
   → No classes held

2. Hour 1: Experimentation (mixed actions)
   → Swing practice sword (×20 actions)
     combat.melee: +200 XP
     combat.melee.sword: +140 XP
     combat.melee.sword.parry: +100 XP

   → Gather herbs (×10 actions)
     gather.herbalism: +100 XP
     gather.herbalism.flower: +70 XP

   → Chat with merchant (×5 actions)
     social.trade: +50 XP

3. Hour 2: Focus on combat
   → Sword practice (×50 actions)
     combat.melee: +500 XP (total: 700 XP)
     combat.melee.sword: +350 XP (total: 490 XP)
     combat.melee.sword.parry: +250 XP (total: 350 XP)

4. Rest Cycle (first mandatory rest)
   → Eligibility check:
     combat.melee: 700 XP ✗ ([Warrior] needs 1,000 XP)
     No classes eligible yet
   → No level up event
   → Rested bonus: +25% XP for next hour

5. Hour 3: Combat focus with rested bonus
   → Sword practice (×60 actions)
     combat.melee: +750 XP (total: 1,450 XP) ✅ ELIGIBLE
     combat.melee.sword: +525 XP (total: 1,015 XP)

6. Rest Cycle (second rest)
   → Eligibility check:
     combat.melee: 1,450 XP ✅ [Warrior] eligible
   → LEVEL UP EVENT TRIGGERED
   → Present [Warrior] class offer
   → Player accepts [Warrior]
   → [Warrior] Level 1 granted
   → [Warrior] class XP starts at 0

7. Continue playing: All combat XP now splits
   → combat.melee bucket: +10 XP
   → [Warrior] class XP: +10 XP
   → Level 2 requires 100 XP: 10 sword swings to level up
```

---

## Appendix B: Multi-Class Example

### Experienced Player Journey

```
Starting State:
  [Warrior] Level 15 (active)
  combat.melee bucket: 15,000 XP

1. Player decides to learn blacksmithing
   → 2 hours of forging actions
   → craft.smithing bucket: +1,500 XP
   → craft.smithing.weapon: +1,050 XP

2. Rest Cycle
   → Eligibility check:
     craft.smithing: 1,500 XP ✅ [Blacksmith] eligible
   → LEVEL UP EVENT
   → Player accepts [Blacksmith]
   → [Blacksmith] Level 1 granted

3. Multi-class automatic XP:
   Current Classes: [Warrior], [Blacksmith]

   Action: Sword combat → Tag: combat.melee
     → combat.melee bucket: +10 XP (full)
     → Warrior class XP: +5 XP (exact match: combat.melee)
     → Blacksmith class XP: 0 XP (no match)
     → [Warrior] class XP: +6 XP (60%)
     → [Blacksmith] class XP: 0 XP (no tag match)

   Action: Forge sword
     → craft.smithing bucket: +10 XP (full)
     → [Warrior] class XP: 0 XP (no tag match)
     → [Blacksmith] class XP: +6 XP (40%)

4. Skill synergy unlocked:
   → Shared tags detected: combat.melee + craft.smithing.weapon
   → Hybrid skill [Weapon Master] offered
   → "Swords you forged deal +10% damage"
```

---

_Document Version 1.0 - Authoritative specification for Core Progression System_
_All CRITICAL priority progression questions resolved_
