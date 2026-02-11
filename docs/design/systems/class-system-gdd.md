---
type: system
scope: detailed
status: authoritative
version: 1.0.0
created: 2026-02-03
updated: 2026-02-03
subjects: [classes, tiers, specializations, multi-classing, progression]
dependencies: [core-progression-system-gdd.md, idle-inn-gdd.md, idle-game-overview.md]
---

# Class System - Authoritative Game Design

## Executive Summary

The Class System defines how players acquire, progress, and specialize classes through the combination of tier progression (Apprentice/Journeyman/Master) and specialization paths. Specializations are separate classes that players discover through focused actions or training, expanding their available class collection rather than replacing their base class. This system creates deep build customization while maintaining clear progression milestones.

**This document resolves:**

- Class tier progression mechanics
- Specialization discovery and requirements
- Multi-class XP distribution system
- Specialization award methods (actions, training, discovery)

**Design Philosophy:** Classes are discovered, not selected. Players earn classes through their actions, then expand their options through specializations. Tiers provide mastery depth while specializations provide build variety. Players can hold multiple classes simultaneously, creating unique combinations of abilities.

---

## 1. Class Tier Structure

### 1.1 Three-Tier Progression

All classes progress through three distinct tiers:

```
Apprentice → Journeyman → Master
```

**Tier Advancement:**

- Automatic when class XP reaches required threshold
- Triggered during Level Up Events (rest cycle)
- No additional bucket requirements for tier progression
- Original class always advances; specializations are separate choices

### 1.2 Tier Display Format

Classes display as `[Class Name - Tier]`:

```
Examples:
  [Warrior - Apprentice]
  [Warrior - Journeyman]
  [Blacksmith - Master]
  [Blade Dancer - Apprentice]
```

**UI Simplification:** When no specialization chosen, displays as `[Warrior - Apprentice]` rather than showing specialization field.

### 1.3 Tier Benefits

```
Apprentice Tier:
  - Basic class abilities
  - Core mechanics unlocked
  - Foundation for specialization discovery

Journeyman Tier:
  - Advanced techniques
  - Tier-appropriate recipes/spells
  - Access to intermediate specializations

Master Tier:
  - Master-tier abilities
  - Unique signature skills
  - Teaching/mentoring capabilities (future feature)
  - Access to advanced specializations
```

---

## 2. Specialization System

### 2.1 Two Independent Progression Systems

```
System 1: Class Tier Progression
  [Warrior - Apprentice] → [Warrior - Journeyman] → [Warrior - Master]
  Driven by: Class XP (earned through actions with matching tags)
  Trigger: Class XP reaches level thresholds
  Progresses: Independent of specializations

System 2: Specialization Discovery
  [Blade Dancer - Apprentice] becomes eligible
  Driven by: Bucket XP in specific tags OR training
  Trigger: Bucket XP threshold OR completing training
  Progresses: As separate, independent class
```

**Key Insight:** Specializations are NEW classes, not modifications to existing classes.

### 2.2 Specialization Award Methods

Specialization classes can be discovered through multiple paths:

#### Path 1: Triggering Actions (Bucket XP)

```
If Player HAS parent class [Warrior]:
  [Blade Dancer] Requirements:
    - combat.melee.sword bucket ≥ 5,000 XP (standard threshold)
    - Discover through focused sword use
    - Offered at next Level Up Event

If Player DOES NOT have parent class:
  [Blade Dancer] Requirements:
    - combat.melee.sword bucket ≥ 15,000 XP (3× higher threshold)
    - Harder to discover without base class foundation
    - Direct path to specialization without parent
    - Allows skipping base class with extra effort
```

**Rationale:** Having the parent class provides foundation and accelerates specialization discovery.

#### Path 2: Training

```
Training Method:
  - Seek out NPC trainer who specializes in Blade Dancing
  - Complete training quest/curriculum
  - Granted [Blade Dancer - Apprentice] regardless of bucket XP
  - Takes time/effort but guaranteed path
  - May require gold, items, or quest completion

Example Training Flow:
  1. Find Blade Master NPC in city
  2. Accept training quest: "Spar with 10 opponents"
  3. Complete quest
  4. Granted [Blade Dancer - Apprentice]
```

#### Path 3: Discovery (Rare)

```
Discovery Methods:
  - Find ancient training manual in dungeon
  - Learn from secret master in hidden location
  - Complete rare world event
  - Receive as reward from unique encounter

These are intentionally rare and flavorful.
```

### 2.3 Specialization Mechanics

```
Specialization Rules:
✓ Original class advances tier independently
✓ Specialization offered as SEPARATE NEW class
✓ Specialization always starts at Apprentice tier
✓ Player can accept or refuse (backlog system)
✓ Specializations have their own unique tag requirements
✓ Specializations count toward the 3-class active limit
```

### 2.4 Example Timeline

```
Player Journey:
━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
1. Play for 10 hours, sword-focused combat
   → combat.melee.sword bucket: 5,000 XP
   → [Warrior - Apprentice] class XP: 1,000 XP

2. Level Up Event: Specialization Eligible
   → combat.melee.sword bucket ≥ 5,000 XP ✅
   → Player HAS [Warrior] class → standard threshold met
   → [Blade Dancer - Apprentice] offered as NEW class

3. Player accepts [Blade Dancer - Apprentice]
   Current Classes:
     - [Warrior - Apprentice]
     - [Blade Dancer - Apprentice]

4. Continue playing sword combat
   → Both classes gain XP (automatic hierarchical split: Blade Dancer 50%, Warrior 25%)
   → combat.melee.sword bucket continues growing

5. Later: [Warrior] reaches tier threshold
   → Level Up Event: Auto-advance to Journeyman
   → [Warrior - Apprentice] → [Warrior - Journeyman]
   → [Blade Dancer - Apprentice] remains unchanged
```

### 2.5 Alternative Discovery Paths

```
Path A: Direct Discovery (No Parent Class)
━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
1. Never took [Warrior] class
2. Pure sword fighting for 30+ hours
3. combat.melee.sword bucket: 15,000 XP
4. [Blade Dancer - Apprentice] offered (higher threshold met)
5. Accept as first combat class
6. Result: Started with specialized class without foundation

Path B: Training (Bypass Bucket Requirements)
━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
1. Seek out Blade Master NPC
2. Complete 5 training sessions
3. Granted [Blade Dancer - Apprentice] (no bucket XP required)
4. Result: Guaranteed path through effort, not grinding
```

---

## 3. Multi-Class System

### 3.1 All Classes Always Active

**Core Principle**: All accepted classes are **always active** and gaining XP simultaneously.

```
All Classes Always Active:
- No maximum limit on number of classes
- No activation/deactivation system
- Every accepted class remains perpetually active
- All classes gain XP from every action (based on tag matching)

Character with 5 Classes:
  - [Warrior - Journeyman]      ← Always active
  - [Blade Dancer - Apprentice] ← Always active
  - [Blacksmith - Apprentice]   ← Always active
  - [Miner - Apprentice]        ← Always active
  - [Merchant - Apprentice]     ← Always active

All 5 classes gain XP from every action (hierarchical split)
```

### 3.2 Automatic Hierarchical XP Distribution

XP is distributed **automatically** based on tag hierarchy, NOT player configuration.

**Formula**:

1. Find most specific matching tag for action
2. Award **50%** to exact match class
3. Award **50%** to parent tag level (split if multiple)
4. Continue subdividing remainder up hierarchy

**Example**: Attack with Sword action fires tag `combat.melee.sword`

```
Character Classes:
  - [Warrior] (tracks: combat.melee)
  - [Blade Dancer] (tracks: combat.melee.sword)
  - [Fighter] (tracks: combat)

Action: Attack with Sword → Tag: combat.melee.sword → Base 100 XP

Distribution:
  Blade Dancer: 50 XP   (exact match: combat.melee.sword)
  Warrior:      25 XP   (parent: combat.melee gets 50%, split among parents)
  Fighter:      12.5 XP (grandparent: combat gets 25%, split among grandparents)
  Remaining:    12.5 XP (flows to general pool, see 3.3)

Total XP awarded: 100 XP
```

**Key Rules**:

- **Downward flow only**: Specific → General, never upward
- **50% exact match**: Most specific tag always gets half
- **Subdivide remainder**: Each parent level gets half of previous, split among siblings
- **Multiplicative at same depth**: Multiple classes at same tag depth each get full percentage

### 3.3 Foundational Class Coverage

If no class tracks an action's tag, XP flows to **Foundational** classes (Tier 1):

```
Action: Attack with Sword → combat.melee.sword
Character has: [Cook], [Miner] (neither track combat tags)

Result:
  Cook:  50 XP (Foundational class, gets 50%)
  Miner: 50 XP (Foundational class, gets 50%)

Rationale: XP always counts, even for non-matching actions
```

**Foundational Classes** (Tier 1):

- Fighter, Crafter, Gatherer, Host, Trader, Scholar, Socialite

**Rule**: If no classes match action tags, distribute XP equally among all Foundational classes.

### 3.4 Primary Class Determination

**Primary Class**: Highest-leveled **Tier 2** class (fallback: Tier 1)

```
Example 1: Tier 2 Dominant
  Classes:
    [Warrior - Level 15] (Tier 2) ← PRIMARY
    [Blade Dancer - Level 8] (Tier 3)
    [Blacksmith - Level 5] (Tier 2)

  Primary: Warrior (highest Tier 2)

Example 2: Only Tier 1 Classes
  Classes:
    [Fighter - Level 20] (Tier 1) ← PRIMARY
    [Crafter - Level 10] (Tier 1)

  Primary: Fighter (highest Tier 1, no Tier 2)

Example 3: Tie at Tier 2
  Classes:
    [Warrior - Level 15] (Tier 2) ← PRIMARY (first acquired)
    [Blacksmith - Level 15] (Tier 2)

  Primary: Warrior (tie broken by acquisition order)
```

**Primary Class Benefits**:

- Displayed in character nameplate
- Determines UI class icon
- No mechanical advantages (all classes always active)

---

## 4. Specialization Requirements & Examples

### 4.1 Warrior Class Specializations

```
Parent Class: [Warrior]
Primary Tag: combat.melee

[Blade Dancer] Specialization:
  Trigger: combat.melee.sword bucket ≥ 5,000 XP
           OR training with Blade Master
  Focus: Aggressive offense, evasion
  Signature: Whirlwind Strike

[Shield Bearer] Specialization:
  Trigger: combat.defense.shield bucket ≥ 5,000 XP
           OR training with Shield Master
  Focus: Defense, party protection
  Signature: Shield Wall

[Berserker] Specialization:
  Trigger: combat.melee.* (multiple weapons) ≥ 5,000 XP
           OR combat.tactical.solo bucket ≥ 5,000 XP
  Focus: Raw damage, risk/reward
  Signature: Frenzy

[Weapon Master] Specialization:
  Trigger: combat.melee.* (3+ weapon types) ≥ 8,000 XP each
           OR training with Weapon Grandmaster
  Focus: Versatility with all weapons
  Signature: Instant Weapon Switch
```

### 4.2 Blacksmith Class Specializations

```
Parent Class: [Blacksmith]
Primary Tag: craft.smithing

[Weapon Smith] Specialization:
  Trigger: craft.smithing.weapon bucket ≥ 5,000 XP
           OR apprenticeship at Weapon Forge
  Focus: Swords, axes, polearms
  Signature: Perfect Balance

[Armor Smith] Specialization:
  Trigger: craft.smithing.armor bucket ≥ 5,000 XP
           OR apprenticeship at Armor Forge
  Focus: Helmets, chestplates, gauntlets
  Signature: Reinforced Joints

[Tool Maker] Specialization:
  Trigger: craft.smithing.tool bucket ≥ 5,000 XP
           OR apprenticeship with Artisan
  Focus: Mining picks, hammers, specialty tools
  Signature: Durable Craft

[Jeweler] Specialization:
  Trigger: craft.smithing.jewelry bucket ≥ 5,000 XP
           OR apprenticeship with Gem Master
  Focus: Rings, amulets, gem setting
  Signature: Gemstone Resonance
```

### 4.3 Herbalist Class Specializations

```
Parent Class: [Herbalist]
Primary Tag: gather.herbalism

[Healer] Specialization:
  Trigger: gather.herbalism.flower.* (3+ types) ≥ 3,000 XP each
           OR training at Temple
  Focus: Healing herbs, remedies
  Signature: Potent Salve

[Poison Master] Specialization:
  Trigger: gather.herbalism.root.toxic ≥ 5,000 XP
           OR training with Assassins Guild
  Focus: Toxic plants, poisons
  Signature: Lethal Extract

[Tea Brewer] Specialization:
  Trigger: craft.cooking.beverage + gather.herbalism ≥ 3,000 XP each
           OR training at Tea House
  Focus: Tea blends, buffs
  Signature: Serene Tea

[Botanist] Specialization:
  Trigger: gather.herbalism.* (10+ species) ≥ 8,000 total XP
           OR academic study
  Focus: Plant knowledge, classification
  Signature: Plant Lore
```

---

## 5. Level Up Event UI for Specializations

### 5.1 Specialization Offer Interface

```
╔══════════════════════════════════════════════════╗
║          LEVEL UP EVENT - Resting...             ║
╠══════════════════════════════════════════════════╣
║                                                  ║
║ NEW SPECIALIZATION AVAILABLE:                    ║
║ ☑ [Blade Dancer - Apprentice]                    ║
║   Triggered by: combat.melee.sword ≥ 5,000 XP   ║
║   └─ Skills offered if accepted:                ║
║       ☑ [Sword Flourish]                         ║
║       ☑ [Quick Parry]                            ║
║                                                  ║
║ EXISTING CLASS LEVEL UPS:                        ║
║                                                  ║
║ [Warrior - Apprentice] Level 9 → Level 10        ║
║   Next tier: Journeyman (available at threshold) ║
║   Skill Rewards:                                 ║
║     ☑ [Power Strike]                             ║
║                                                  ║
║ [Blade Dancer - Apprentice] Level 1 → Level 2     ║
║   Skill Rewards:                                 ║
║     ☑ [Dancing Blade]                            ║
║                                                  ║
║ [Accept Selected]  [View Backlog →]             ║
╚══════════════════════════════════════════════════╝

Note: All accepted classes are always active and gaining XP
```

### 5.2 Specialization Backlog

```
╔══════════════════════════════════════╗
║     SPECIALIZATION BACKLOG            ║
╠══════════════════════════════════════╣
║                                      ║
║ PREVIOUSLY REFUSED:                  ║
║                                      ║
║ WARRIOR SPECIALIZATIONS:             ║
║ ☑ [Shield Bearer - Apprentice]       ║
║   Trigger: Shield practice           ║
║   Note: "Want offensive focus first"  ║
║                                      ║
║ BLACKSMITH SPECIALIZATIONS:          ║
║ ☑ [Armor Smith - Apprentice]         ║
║   Trigger: Armor crafting XP          ║
║                                      ║
║ [Accept Selected]  [← Back to Event] ║
╚══════════════════════════════════════╝
```

---

## 6. Cross-Reference to Related Systems

### 6.1 Integration with Core Progression System

```
From: core-progression-system-gdd.md

XP Buckets:
  → Used for class eligibility (parent class)
  → Used for specialization discovery (bucket thresholds)
  → Permanent, no decay

Class XP:
  → Separate from bucket XP
  → Used for tier progression (Apprentice → Journeyman → Master)
  → Starts at 0 when class accepted
  → Splits when multiple classes held

Level Up Events:
  → Triggered by rest cycles
  → Present class tier advancements
  → Present specialization offers
  → Manage backlog of refused classes/skills
```

### 6.2 Class Synergy Bonuses

Some class combinations unlock unique hybrid skills:

```
[Warrior - Journeyman] + [Merchant - Apprentice]:
  Shared tags: combat.melee + social.trade
  Unlocks: [Caravan Guard]
  "Better combat pay rates, merchant reputation bonuses"

[Blacksmith - Journeyman] + [Merchant - Journeyman]:
  Shared tags: craft.smithing + social.trade
  Unlocks: [Master Artisan]
  "Can sell crafted goods at premium prices"

[Herbalist - Apprentice] + [Alchemist - Apprentice]:
  Shared tags: gather.herbalism + craft.alchemy
  Unlocks: [Wild Crafter]
  "Can craft potions while gathering"
```

---

## 7. Resolved Open Questions

This document resolves the following CRITICAL and HIGH priority questions from open-questions.md:

| #   | Question                           | Resolution                                                     | Status      |
| --- | ---------------------------------- | -------------------------------------------------------------- | ----------- |
| 2.1 | Multi-Class XP Distribution        | Automatic hierarchical split (50/25/12.5...)                   | ✅ Resolved |
| 2.2 | Class Evolution Presentation       | Level Up Event with choices, deferred backlog                  | ✅ Resolved |
| 2.3 | Class Evolution Rejection          | Yes, can defer to backlog                                      | ✅ Resolved |
| 3.X | Specialization Discovery Mechanics | Multiple paths: actions (3× if no parent), training, discovery | ✅ Resolved |
| 3.X | All Classes Always Active          | No slots, no dormant, all classes gain XP                      | ✅ Resolved |

---

## 8. Implementation Notes

### 8.1 Data Structure

```csharp
public class PlayerClass
{
    public string ClassId { get; set; }  // e.g., "warrior", "blade_dancer"
    public string Tier { get; set; }     // "apprentice", "journeyman", "master"
    public int Level { get; set; }
    public long ClassXP { get; set; }
    public int XPSplitPercentage { get; set; }  // Player-configured
    public bool IsActive { get; set; }
    public List<string> SpecializationTagPaths { get; set; }
}

public class SpecializationOffer
{
    public string SpecializationId { get; set; }
    public string ParentClassId { get; set; }
    public List<TagRequirement> BucketThresholds { get; set; }
    public bool TrainingAvailable { get; set; }
    public List<NPCTrainer> Trainers { get; set; }
}

public class ClassManager
{
    // All accepted classes are always active
    public List<PlayerClass> Classes { get; set; }

    public PlayerClass GetPrimaryClass()
    {
        // Highest-leveled Tier 2 class, fallback to Tier 1
        var tier2Classes = Classes.Where(c => c.TreeTier == 2).OrderByDescending(c => c.Level);
        if (tier2Classes.Any())
            return tier2Classes.First();

        return Classes.Where(c => c.TreeTier == 1).OrderByDescending(c => c.Level).FirstOrDefault();
    }
}
```

### 8.2 Specialization Discovery Algorithm

```
On each action completion:
1. Update relevant XP buckets
2. Check if any bucket threshold crossed
3. If threshold crossed AND parent class held:
   → Add specialization to Level Up Event pool
4. If threshold crossed AND NO parent class:
   → Check if bucket >= 3× threshold
   → If yes, add to Level Up Event pool
5. Level Up Event presents all eligible specializations
```

### 8.3 Complexity Ratings

| Component                    | Implementation Complexity | Notes                        |
| ---------------------------- | ------------------------- | ---------------------------- |
| Tier Progression             | Low (2/5)                 | Simple threshold check       |
| Specialization Discovery     | Medium (3/5)              | Bucket monitoring, 3× logic  |
| Hierarchical XP Distribution | High (4/5)                | Recursive tag tree traversal |
| Primary Class Calculation    | Low (2/5)                 | Sort by tier + level         |
| Training Paths               | Medium (3/5)              | NPC interaction, quests      |

---

## 9. Design Decisions Record

### 9.1 Specializations as Separate Classes

**Decision:** Specializations are new classes, not modifications to existing classes.

**Rationale:**

- Players can hold both parent and specialization
- Creates build variety through class combinations
- Clearer understanding of what each class provides
- Easier to balance (each class is independent)

**Trade-offs:**

- More classes to manage
- Players may feel overwhelmed with many classes
- Mitigated by: Primary class UI simplification, hierarchical display

### 9.2 Higher Threshold Without Parent Class

**Decision:** Specializations require 3× bucket XP if player doesn't hold parent class.

**Rationale:**

- Parent class provides foundation and should accelerate specialization
- Prevents "skipping straight to OP spec"
- Allows alternative paths with extra effort
- Rewards dedicated specialization without forcing parent class

**Trade-offs:**

- May feel grindy to skip parent class
- Mitigated by: Training paths provide guaranteed alternative

### 9.3 All Classes Always Active

**Decision:** No activation limits - all accepted classes remain perpetually active.

**Rationale:**

- Eliminates micromanagement of class activation
- Ensures XP always counts (hierarchical distribution)
- Simplifies mental model: "you are all your classes"
- Reduces UI complexity (no slot management screens)
- Encourages experimentation (no penalty for accepting classes)

**Trade-offs:**

- Less strategic decision-making about which classes to activate
- May encourage "collect everything" mentality
- Mitigated by: Primary class display, hierarchical XP dilution as natural limiter

### 9.4 No Explicit Level Ranges for Tiers

**Decision:** Tiers exist (Apprentice/Journeyman/Master) but levels per tier not fixed.

**Rationale:**

- Flexible progression pacing
- Can balance tier thresholds independently
- Allows different progression rates for different classes
- Future-proof for adding/removing tiers

---

## Appendix A: Complete Specialization Discovery Example

### New Player Experience (First 20 Hours)

```
1. Character Creation
   → No classes held
   → All XP buckets at 0

2. Hour 0-5: Mixed experimentation
   → Swing sword: combat.melee bucket +500 XP
   → Chop tree: gather.logging bucket +300 XP
   → Chat with merchant: social.trade bucket +100 XP

3. Rest Cycle #1
   → combat.melee: 500 XP ✗ ([Warrior] needs 1,000 XP)
   → No classes eligible yet

4. Hour 5-15: Focus on combat
   → Sword practice (200 actions)
   → combat.melee: +2,000 XP (total: 2,500 XP)
   → combat.melee.sword: +1,400 XP (total: 1,500 XP)

5. Rest Cycle #2
   → combat.melee: 2,500 XP ✅ [Warrior - Apprentice] eligible
   → LEVEL UP EVENT
   → Accept [Warrior - Apprentice]
   → [Warrior] class XP starts at 0

6. Hour 15-20: Intense sword training
   → Sword practice (150 actions)
   → combat.melee bucket: +1,500 XP (total: 4,000 XP)
   → combat.melee.sword bucket: +1,050 XP (total: 2,550 XP)
   → [Warrior] class XP: +1,500 XP (Level 5)

7. Rest Cycle #3
   → combat.melee.sword: 2,550 XP ✗ ([Blade Dancer] needs 5,000 XP)
   → Not yet eligible for specialization

8. Hour 20-30: Continue sword focus
   → More sword practice (300 actions)
   → combat.melee.sword bucket: +2,100 XP (total: 4,650 XP)
   → Getting close to [Blade Dancer]!

9. Alternative Path: Training
   → Instead of grinding, seek Blade Master NPC
   → Complete training quest (1 hour)
   → Granted [Blade Dancer - Apprentice]
   → Result: Saved 10+ hours of grinding
```

---

## Appendix B: Multi-Class Strategy Examples

### Example 1: Combat-Focused Build

```
Current Classes:
  [Fighter - Apprentice]
  [Warrior - Journeyman] ← Primary (highest Tier 2)
  [Blade Dancer - Apprentice]
  [Shield Bearer - Apprentice]

XP Distribution (Attack with Sword):
  Tag: combat.melee.sword → Base 100 XP
  - Blade Dancer: 50 XP (exact match: combat.melee.sword)
  - Warrior: 25 XP (parent: combat.melee)
  - Fighter: 12.5 XP (grandparent: combat)

Benefit:
- Versatile in all combat situations
- All sword actions feed all three classes
- Specializations progress faster (50% exact match)
- Foundational classes still grow (hierarchical split)
```

### Example 2: Crafter-Gatherer Build

```
Current Classes:
  [Crafter - Apprentice]
  [Blacksmith - Journeyman] ← Primary (highest Tier 2)
  [Miner - Apprentice]
  [Merchant - Apprentice]

XP Distribution (Mine Iron Ore):
  Tag: gather.mining.metal → Base 100 XP
  - Miner: 50 XP (exact match: gather.mining)
  - Crafter: 25 XP (parent: gather)

XP Distribution (Forge Iron Sword):
  Tag: craft.smithing.weapon.sword → Base 100 XP
  - Blacksmith: 50 XP (exact match: craft.smithing.weapon)
  - Crafter: 25 XP (parent: craft)

Benefit:
- Self-sufficient crafting loop
- All classes always progressing
- Merchant gains XP from trade actions
- [Material Intuition] synergy unlocked
```

### Example 3: Specialist Build

```
Current Classes:
  [Fighter - Journeyman]
  [Warrior - Journeyman]
  [Blade Dancer - Master] ← Primary (highest level Tier 2)
  [Weapon Smith - Journeyman]

XP Distribution (Attack with Sword):
  Tag: combat.melee.sword → Base 100 XP
  - Blade Dancer: 50 XP (exact match)
  - Warrior: 25 XP (parent)
  - Fighter: 12.5 XP (grandparent)

Strategy:
- Pure sword combat focus
- Can craft own weapons
- All combat classes benefit from every attack
- No "dormant" classes - all gaining XP

Benefit:
- Highly specialized, powerful build
- Not "wasting" XP on general class
- Two slots open for future specializations
```

---

_Document Version 1.0 - Authoritative specification for Class System_
_Core tier and specialization mechanics resolved_
_Future versions will add: prestige paths, class consolidation, advanced specializations_
