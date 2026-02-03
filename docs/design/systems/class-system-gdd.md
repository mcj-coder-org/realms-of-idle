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
- Multi-class slot management
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
   Active Classes (2/3):
     - [Warrior - Apprentice]
     - [Blade Dancer - Apprentice]

4. Continue playing sword combat
   → Both classes gain class XP (split configured 60%/40%)
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

## 3. Multi-Class Slot Management

### 3.1 Maximum Active Classes

Players may hold up to **3 active classes** simultaneously:

```
1 Active Class:  100% class XP to primary
2 Active Classes: Player configures split (60%/40%)
3 Active Classes: Player configures split (50%/30%/20%)

Default distribution when 2nd class added:
  Primary: 60%
  Secondary: 40%

Default distribution when 3rd class added:
  Primary: 50%
  Secondary: 30%
  Tertiary: 20%
```

### 3.2 Class Slot Strategy

When accepting specializations, players must manage their 3 active slots:

```
Scenario: Already have 3 active classes
━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
Current Active Classes (3/3):
  - [Warrior - Journeyman]
  - [Blacksmith - Apprentice]
  - [Merchant - Apprentice]

Level Up Event: [Blade Dancer - Apprentice] offered

Options:
1. Make [Blade Dancer] active, move one class to dormant
2. Keep [Blade Dancer] dormant (activate later)

Dormant Classes:
- Class XP accumulation paused
- Class level/tier retained
- Can reactivate anytime (no penalty)
- Max 3 active at once
```

### 3.3 XP Split Configuration

Players can customize the split at any time outside of rest:

```
Class XP Split Configuration:
┌─────────────────────────────────┐
│ Active Classes:                 │
│                                 │
│ [Warrior - Journeyman] ████████ 60% │
│ [Blacksmith - Apprentice]  ████   40% │
│                                 │
│ Adjust: [─] [────────] [+]      │
│                                 │
│ [Save Changes]                  │
└─────────────────────────────────┘

Rules:
- Must total 100% across all active classes
- Changes take effect next action
- Cannot change during rest/combat
- Configuration saved per class set
```

### 3.4 Dormant Class Management

```
Dormant Class Screen:
┌────────────────────────────────────────────┐
│            DORMANT CLASSES                 │
├────────────────────────────────────────────┤
│                                            │
│ Available to Activate (select one):        │
│                                            │
│ ○ [Merchant - Apprentice]                 │
│    Last active: 2 days ago                 │
│    Class XP paused at 450                  │
│                                            │
│ ○ [Alchemist - Journeyman]                │
│    Last active: 1 week ago                 │
│    Class XP paused at 3,200                 │
│                                            │
│ [Activate Selected]  [Cancel]              │
└────────────────────────────────────────────┘

Activation Rules:
- Can activate anytime (no cooldown)
- Replaces currently active class
- Deactivated class goes to dormant pool
- No penalty for switching
```

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
║ CLASS SLOT MANAGEMENT:                           ║
║ Active Slots: (2/3) - One slot available         ║
║                                                  ║
║ [Accept Selected]  [View Backlog →]             ║
╚══════════════════════════════════════════════════╝

Note: Accepting [Blade Dancer] uses 1 class slot
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
| 2.1 | Dormant Class Handling             | Persistent, reactivatable anytime, no penalty                  | ✅ Resolved |
| 2.2 | Class Evolution Presentation       | Level Up Event with choices, deferred backlog                  | ✅ Resolved |
| 2.3 | Class Evolution Rejection          | Yes, can defer to backlog                                      | ✅ Resolved |
| 3.X | Specialization Discovery Mechanics | Multiple paths: actions (3× if no parent), training, discovery | ✅ Resolved |
| 3.X | Multi-Class Slot Strategy          | Max 3 active, dormant pool, no switching penalty               | ✅ Resolved |

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

public class ClassSlotManager
{
    public int MaxActiveClasses => 3;
    public List<PlayerClass> ActiveClasses { get; set; }
    public List<PlayerClass> DormantClasses { get; set; }

    public bool CanAcceptNewClass()
    {
        return ActiveClasses.Count < MaxActiveClasses;
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

| Component                | Implementation Complexity | Notes                       |
| ------------------------ | ------------------------- | --------------------------- |
| Tier Progression         | Low (2/5)                 | Simple threshold check      |
| Specialization Discovery | Medium (3/5)              | Bucket monitoring, 3× logic |
| Multi-Class XP Split     | Medium (3/5)              | Configurable distribution   |
| Dormant Class Management | Low (2/5)                 | State flags, activation UI  |
| Training Paths           | Medium (3/5)              | NPC interaction, quests     |

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

- Uses up limited class slots (3 max)
- Players must make strategic choices about which classes to hold
- Mitigated by: Dormant class system (no penalty for switching)

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

### 9.3 Three Class Slot Limit

**Decision:** Maximum 3 active classes, with dormant pool for additional.

**Rationale:**

- Forces strategic decisions about class combinations
- Prevents "collect everything" mentality
- Makes each class choice meaningful
- Dormant system allows experimentation without penalty

**Trade-offs:**

- Limits build diversity at any given time
- Mitigated by: No switching cooldown, can swap anytime

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
Active Classes (3/3):
  [Warrior - Journeyman] (50%)
  [Blade Dancer - Apprentice] (30%)
  [Shield Bearer - Apprentice] (20%)

Strategy:
- Use [Warrior] for general combat (50% XP)
- Switch to [Blade Dancer] when DPS needed (30% XP)
- Switch to [Shield Bearer] when tanking (20% XP)

Benefit:
- Versatile in all combat situations
- All sword actions feed all three classes
- Rapid progression across all
```

### Example 2: Crafter-Gatherer Build

```
Active Classes (3/3):
  [Blacksmith - Journeyman] (50%)
  [Miner - Apprentice] (30%)
  [Merchant - Apprentice] (20%)

Strategy:
- Mine ore → feeds [Miner] and [Blacksmith]
- Forge items → feeds [Blacksmith]
- Sell goods → feeds [Merchant]

Benefit:
- Self-sufficient crafting loop
- Can gather own materials
- Can sell crafted goods at profit
- [Material Intuition] synergy unlocked
```

### Example 3: Specialist Build

```
Active Classes (2/3):
  [Blade Dancer - Master] (70%)
  [Weapon Smith - Journeyman] (30%)

Dormant:
  [Warrior - Journeyman] (not needed)

Strategy:
- Pure sword combat focus
- Can craft own weapons
- Ignoring general [Warrior] class

Benefit:
- Highly specialized, powerful build
- Not "wasting" XP on general class
- Two slots open for future specializations
```

---

_Document Version 1.0 - Authoritative specification for Class System_
_Core tier and specialization mechanics resolved_
_Future versions will add: prestige paths, class consolidation, advanced specializations_
