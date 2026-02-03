---
type: system
scope: detailed
status: draft
version: 2.1.0
created: 2026-02-02
updated: 2026-02-03
subjects: ['design', 'mechanics', 'gameplay', 'technical', 'economy']
---

# Open Questions Register

## Organized by Priority with Specific Recommendations

---

## Summary

| Priority     | Questions | Resolved | Recommendations | Status      |
| ------------ | --------- | -------- | --------------- | ----------- |
| **CRITICAL** | 12        | 12       | 36              | ✅ Complete |
| **HIGH**     | 43        | 22       | 77              | In Progress |
| **MEDIUM**   | 45        | 0        | 108             | Beta Block  |
| **LOW**      | 26        | 0        | 52              | Post-Launch |
| **Total**    | **126**   | **34**   | **273**         |             |

**Recently Resolved:**

- ✅ 2026-02-03: Combat System Formula Gaps (7 CRITICAL) → [combat-system-gdd.md v1.1.0](design/systems/combat-system-gdd.md) — **All 7 formula gaps resolved** (§16)
- ✅ 2026-02-03: NPC Core Systems (5 HIGH) → [npc-core-systems-gdd.md](design/systems/npc-core-systems-gdd.md)
- ✅ 2026-02-03: Core Progression System (7 CRITICAL) → [core-progression-system-gdd.md](design/systems/core-progression-system-gdd.md)
- ✅ 2026-02-03: Class System & Specializations (CRITICAL 2.1-2.3) → [class-system-gdd.md](design/systems/class-system-gdd.md)
- ✅ 2026-02-03: Skill & Recipe System (5 CRITICAL) → [skill-recipe-system-gdd.md](design/systems/skill-recipe-system-gdd.md)

**🎉 All CRITICAL priority questions resolved!**
**🚀 HIGH priority in progress: 22/43 resolved (51%)**
**✅ Combat System GDD v1.1.0 - All 7 critical formulas now documented (§16)**

**Legend:**

- **CRITICAL**: Must resolve for prototype/alpha - core game loop depends on these
- **HIGH**: Blocking key features - needed for playable alpha experience
- **MEDIUM**: Important for polish - needed for beta and quality
- **LOW**: Can be deferred - expansion content, advanced features

---

## CRITICAL PRIORITY (Must Resolve for Prototype/Alpha)

These questions block core gameplay systems. The game cannot function without answers to these.

## 1. Core Formulas & Progression

### 1.1 XP Curve Formula ✅ RESOLVED

**Question:** What is the exact XP curve formula from level 1 to level 50?

**Resolution:** Classical MMORPG Scaling (Option A) - Implemented in [Core Progression System GDD §1](design/systems/core-progression-system-gdd.md#1-xp-curve--level-progression)

**Status:** ✅ **RESOLVED** - XP buckets are permanent, dual tracking system implemented

**Previous State:** Mentioned as `Base XP = 100 × (Level ^ 1.5)` but needs full specification including modifiers.

**Recommendations:**

#### Option A: Classical MMORPG Scaling (Recommended)

```
XP Required = Base × (Level ^ Exponent) × DifficultyModifier
Base = 100
Exponent = 1.5
DifficultyModifier = 1.0 (standard)

Examples:
Level 1:  100 XP
Level 5:  1,118 XP
Level 10: 3,162 XP
Level 20: 8,944 XP
Level 30: 15,588 XP
Level 40: 25,298 XP
Level 50: 35,355 XP
```

- **Completeness:** 5/5 - Fully specified
- **Game Fit:** 5/5 - Classic idle/MMO progression
- **Implementation:** 1/5 - Trivial (one-line formula)

**Rationale:** Familiar curve, easy to balance, supports idle pacing (early levels quick, later levels satisfying).

#### Option B: Piecewise Curve

```
Levels 1-10:   Linear (100 × Level) - tutorial pacing
Levels 11-30:  Exponential (100 × Level ^ 1.3) - main game
Levels 31-50:  Steep exponential (100 × Level ^ 1.7) - endgame
```

- **Completeness:** 5/5 - Fully specified
- **Game Fit:** 4/5 - Good pacing but more complex
- **Implementation:** 2/5 - Simple conditional logic

**Rationale:** Better pacing control, but more balance work.

#### Option C: Idle-Specific Time-Based

```
XP Required = BaseXP × (1.15 ^ Level)
Target: ~1 hour per level 1-10, ~8 hours per level 40-50
```

- **Completeness:** 4/5 - Needs playtesting validation
- **Game Fit:** 5/5 - Perfect for idle games
- **Implementation:** 2/5 - Simple compound growth

**Rationale:** Directly ties progression to time expectations.

---

### 1.2 Tag Affinity Decay Formula ✅ RESOLVED

**Question:** What are the exact formulas for tag affinity decay, including linear vs compound, floor values, and activity reset mechanics?

**Resolution:** **N/A** - XP buckets are permanent, no decay implemented. See [Core Progression System GDD §2.2](design/systems/core-progression-system-gdd.md#22-bucket-persistence)

**Status:** ✅ **RESOLVED** - Design decision: Permanent buckets (no decay) for player-friendly progression

**Previous State:** Mentioned as "5% per week" but mechanics undefined.

**Recommendations:**

#### Option A: Weekly Compound Decay with Activity Floor (Recommended)

```
DecayRate = 0.05 (5% per week)
FloorValue = 10 (minimum affinity)
DecayInterval = 7 days (168 hours)

WeeklyCalculation:
CurrentAffinity = CurrentAffinity × (1 - DecayRate)
If CurrentAffinity < FloorValue AND RecentActivity > 0:
    CurrentAffinity = FloorValue

ActivityReset (Any action with tag):
CurrentAffinity = Max(CurrentAffinity, FloorValue + 1)
Prevents decay for 7 days from last action
```

- **Completeness:** 5/5 - Fully specified with edge cases
- **Game Fit:** 5/5 - Balanced for idle playstyle
- **Implementation:** 2/5 - Simple timer arithmetic

**Rationale:** Players don't lose progress for taking breaks (activity protection), but decay prevents permanent tag hoarding.

#### Option B: Linear Decay

```
DailyDecay = 0.71% (5% ÷ 7 days)
CurrentAffinity = CurrentAffinity - (DailyDecay × DaysInactive)
Floor = 10
```

- **Completeness:** 4/5 - Simpler but less strategic
- **Game Fit:** 4/5 - More predictable
- **Implementation:** 1/5 - Very simple

**Rationale:** Easier to understand, but less interesting decisions.

#### Option C: Usage-Based Decay (Alternative)

```
Decay only when OTHER tags gain affinity
TotalTagGrowth = Sum of all affinity gains
ProportionalDecay = (TagAffinity ÷ TotalTagGrowth) × 0.05
```

- **Completeness:** 3/5 - Complex, needs testing
- **Game Fit:** 3/5 - More dynamic but harder to predict
- **Implementation:** 4/5 - Requires tracking all tag changes

**Rationale:** Keeps total tag affinity balanced, but may be confusing.

---

### 1.3 Tag XP Per Action Formula ✅ RESOLVED

**Question:** What is the tag XP scaling formula per action, including base XP and all modifiers?

**Resolution:** Hierarchical distribution with diminishing returns (100%/70%/50%/35%). See [Core Progression System GDD §2.1](design/systems/core-progression-system-gdd.md#21-action-xp-distribution)

**Status:** ✅ **RESOLVED** - Most specific tag gets most XP, parent buckets get reduced share

**Previous State:** "Base XP per action × modifiers" not defined.

**Recommendations:**

#### Option A: Simple Linear Scaling (Recommended)

```
BaseTagXP = 10
TagXPGained = BaseTagXP × (1 + SkillLevelBonus) × AffinityMultiplier

SkillLevelBonus = RelatedSkillLevel × 0.1 (level 20 skill = +200% XP)
AffinityMultiplier = 1.0 + (CurrentAffinity ÷ 100)

Example: Level 10 skill, 50 affinity
10 × (1 + 1.0) × 1.5 = 30 tag XP
```

- **Completeness:** 5/5 - Fully specified
- **Game Fit:** 5/5 - Rewards specialization
- **Implementation:** 1/5 - Straightforward calculation

**Rationale:** Creates positive feedback loop (higher tags = faster XP = higher tags).

#### Option B: Diminishing Returns

```
BaseTagXP = 10
DiminishingFactor = 1 ÷ (1 + (CurrentAffinity ÷ 200))
TagXPGained = BaseTagXP × DiminishingFactor

Prevents tag exploitation while still rewarding focus
```

- **Completeness:** 4/5 - Needs tuning
- **Game Fit:** 4/5 - Prevents min-maxing
- **Implementation:** 2/5 - Simple formula

**Rationale:** Anti-exploitation, but may feel unrewarding.

#### Option C: Flat Rate with Milestones

```
BaseTagXP = 10 (always)
BonusXP = 0 unless CurrentAffinity crosses threshold (25, 50, 75, 100)
At threshold: +100 bonus XP (one-time)
```

- **Completeness:** 5/5 - Fully specified
- **Game Fit:** 3/5 - Less continuous progression
- **Implementation:** 2/5 - Threshold checking

**Rationale:** Simpler but less engaging moment-to-moment.

---

### 1.4 Wildcard Tag Resolution ✅ RESOLVED

**Question:** How are "wildcard" tags (e.g., `craft.*`) resolved in requirements? Does any craft tag satisfy, or sum of all?

**Resolution:** Any Single Match logic: `Max(all matching tags) ≥ Requirement`. See [Core Progression System GDD §2.3](design/systems/core-progression-system-gdd.md#23-purposes-of-xp-buckets)

**Status:** ✅ **RESOLVED** - Wildcards use best match, rewards specialization

**Previous State:** Unresolved.

**Recommendations:**

#### Option A: Any Single Match (Recommended)

```
Requirement: "craft.* at 50"
Player Has: craft.blacksmith = 60, craft.woodworking = 20
Result: SATISFIED (60 >= 50)

Logic: Max(all matching tags) >= Requirement
```

- **Completeness:** 5/5 - Fully specified
- **Game Fit:** 5/5 - Flexible build paths
- **Implementation:** 2/5 - Tag filtering + max check

**Rationale:** Allows player choice and specialization, feels rewarding.

#### Option B: Sum of All Tags

```
Requirement: "craft.* at 50 (total)"
Player Has: craft.blacksmith = 30, craft.woodworking = 25
Result: SATISFIED (55 >= 50)

Logic: Sum(all matching tags) >= Requirement
```

- **Completeness:** 5/5 - Fully specified
- **Game Fit:** 3/5 - Encourages generalist play
- **Implementation:** 2/5 - Tag filtering + sum

**Rationale:** Encourages diversity, but may feel dilutive.

#### Option C: Count-Based

```
Requirement: "3 craft.* tags at 25+"
Player Has: craft.blacksmith = 40, craft.woodworking = 30, craft.leather = 25
Result: SATISFIED (3 tags >= 25)

Logic: Count(matching tags >= threshold) >= RequiredCount
```

- **Completeness:** 5/5 - Fully specified
- **Game Fit:** 4/5 - Hybrid approach
- **Implementation:** 3/5 - More complex condition

**Rationale:** Balanced between specialization and diversity.

---

### 1.5 Idle Loop Efficiency Curve Interaction ✅ RESOLVED

**Question:** How do tag affinities interact with the idle loop efficiency curve? Do tags decay faster during low-efficiency idle periods?

**Resolution:** Decoupled systems - Efficiency affects class XP only, buckets always get 100%. See [Core Progression System GDD §6.1](design/systems/core-progression-system-gdd.md#61-diminishing-returns-curve)

**Status:** ✅ **RESOLVED** - Buckets permanent, efficiency only affects class progression

**Previous State:** Unresolved interaction.

**Recommendations:**

#### Option A: Decoupled Systems (Recommended)

```
EfficiencyCurve: Based on rest bonus, idle time, premium status
TagDecay: Always 5% per week, regardless of efficiency

Rationale: Tags represent long-term identity, not short-term optimization
```

- **Completeness:** 5/5 - Fully specified
- **Game Fit:** 5/5 - Clear separation of concerns
- **Implementation:** 1/5 - Independent systems

**Rationale:** Tags = who you are, efficiency = how rested you are. Don't conflate.

#### Option B: Bonus Decay During Inefficiency

```
If IdleEfficiency < 0.5:
    TagDecayRate = 0.05 × (2 × (1 - IdleEfficiency))
    At 0% efficiency: 10% weekly decay
    At 50% efficiency: 5% weekly decay (normal)
    At 100% efficiency: 0% weekly decay (growth instead)
```

- **Completeness:** 4/5 - Needs tuning
- **Game Fit:** 3/5 - Punishes inactivity doubly
- **Implementation:** 2/5 - Variable rate calculation

**Rationale:** Encourages returning, but may feel punitive.

#### Option C: Efficiency Boosts Tag Gain, Not Decay

```
TagDecay: Fixed 5% per week (always)
TagGainMultiplier = BaseGain × (1 + IdleEfficiency)
At 100% efficiency: 2× tag XP
At 0% efficiency: 1× tag XP (normal)
```

- **Completeness:** 5/5 - Fully specified
- **Game Fit:** 5/5 - Positive reinforcement
- **Implementation:** 2/5 - Multiplier on gains

**Rationale:** Reward active play without punishing casual players.

---

## 2. Class System Mechanics

### 2.1 Dormant Class Handling ✅ RESOLVED

**Question:** What happens to dormant classes? Can they be reactivated? Do they decay?

**Resolution:** Persistent, reactivatable anytime with no penalty. See [Core Progression System GDD §5.3](design/systems/core-progression-system-gdd.md#53-inactivedormant-classes)

**Status:** ✅ **RESOLVED** - XP paused while dormant, levels retained, no cooldown

**Previous State:** Unresolved.

**Recommendations:**

#### Option A: Persistent, Reactivatable (Recommended)

```
Dormant Classes:
- Remain unlocked (no re-requirements)
- Tag affinities frozen (no decay, no growth)
- Can be reactivated anytime via 5-minute cooldown
- Max 1 active class at a time (prevents rapid switching)

Rationale: Encourages experimentation without penalty
```

- **Completeness:** 5/5 - Fully specified
- **Game Fit:** 5/5 - Player-friendly
- **Implementation:** 2/5 - State flags + cooldown

**Rationale:** Players should feel safe trying new classes.

#### Option B: Decaying Dormancy

```
Dormant Classes:
- Reactivation requires 50% of original tag requirements
- If tags decay below threshold, class becomes "locked" again
- 24-hour cooldown on reactivation
```

- **Completeness:** 5/5 - Fully specified
- **Game Fit:** 3/5 - Adds maintenance pressure
- **Implementation:** 3/5 - Re-validation logic

**Rationale:** Prevents class hoarding, adds strategic weight.

#### Option C: Limited Slots

```
- 3 class slots: 1 active, 2 dormant
- Unlocking 4th class requires releasing 1st
- Released classes are lost (must re-earn)
- Premium: +1 dormant slot
```

- **Completeness:** 5/5 - Fully specified
- **Game Fit:** 4/5 - Adds decision weight
- **Implementation:** 2/5 - Slot management

**Rationale:** Meaningful choices about build diversity.

---

### 2.2 Class Evolution Choice Presentation ✅ RESOLVED

**Question:** How is class evolution choice presented to the player? Automatic based on tags, or player choice from qualified options?

**Resolution:** Level Up Event with choice presentation, can defer to backlog. See [Core Progression System GDD §3.3](design/systems/core-progression-system-gdd.md#33-level-up-event-interface)

**Status:** ✅ **RESOLVED** - Player choice with default accept, backlog system for deferral

**Previous State:** Unresolved.

**Recommendations:**

#### Option A: Present Qualified Options, Player Chooses (Recommended)

```
When Player Qualifies for Evolution:
1. Check all evolution requirements
2. Create list of qualified evolutions (may be multiple)
3. Show modal: "You've grown! Your [Warrior] can evolve:"
   - Option A: [Berserker] - Focus: combat.aggression
   - Option B: [Guardian] - Focus: combat.protection
   - Option C: [Remain Warrior]
4. Player selects or defers (can choose later from class panel)
```

- **Completeness:** 5/5 - Fully specified UX
- **Game Fit:** 5/5 - Player agency
- **Implementation:** 3/5 - UI + state management

**Rationale:** Feels rewarding, gives strategic control.

#### Option B: Automatic Based on Highest Tags

```
Evolution = Max(qualified evolutions by tag score)
Tie-break: Random
Player can undo via respec (cost: premium currency)
```

- **Completeness:** 4/5 - Needs respec cost definition
- **Game Fit:** 3/5 - Less agency
- **Implementation:** 2/5 - Auto-select logic

**Rationale:** Simpler flow, but may feel railroading.

#### Option C: Preview and Plan System

```
- Always show "Next Evolution Path" panel
- Displays all possible evolutions and current progress
- Player "locks in" choice early (gives progress warning)
- Upon qualifying, automatic evolution to locked choice
```

- **Completeness:** 5/5 - Fully specified
- **Game Fit:** 5/5 - Strategic planning
- **Implementation:** 4/5 - Preview UI + commit system

**Rationale:** Best of both worlds - planning + automatic execution.

---

### 2.3 Class Evolution Rejection ✅ RESOLVED

**Question:** Can players reject a class evolution and stay at current class?

**Resolution:** Yes, can defer evolution choices to backlog. Level up is mandatory but evolution can be postponed. See [Core Progression System GDD §3.4](design/systems/core-progression-system-gdd.md#34-acceptrefuse-rules)

**Status:** ✅ **RESOLVED** - Existing classes must level, but evolution/skills can be refused

**Previous State:** Strategic choice for tag optimization unclear.

**Recommendations:**

#### Option A: Yes, with Benefits (Recommended)

```
Player Can:
- Remain at current class indefinitely
- Continue gaining XP (no cap)
- Tag requirements for next evolution unchanged

Why Do This:
- Wait for more tag diversity before evolving
- Keep current class abilities that may be lost
- Strategic optimization of build

Tradeoff:
- Miss out on evolution bonuses (stats, skills)
- Evolution requirements still scale with level
```

- **Completeness:** 5/5 - Fully specified
- **Game Fit:** 5/5 - Strategic depth
- **Implementation:** 1/5 - Optional progression

**Rationale:** Some players may prefer base class abilities.

#### Option B: Evolution Forced at Level Threshold

```
- At level 10/20/30, must evolve to continue leveling
- Cannot remain base class beyond thresholds
- Prevents stagnation, ensures progression
```

- **Completeness:** 4/5 - Needs threshold definition
- **Game Fit:** 3/5 - Forces progression
- **Implementation:** 2/5 - Level gate check

**Rationale:** Prevents decision paralysis, ensures variety.

#### Option C: Hybrid - Temporary Delay

```
- Can delay evolution up to 5 levels past threshold
- After level 15 (for level 10 evolution), forced evolution
- Auto-select based on current tags if no choice made
```

- **Completeness:** 5/5 - Fully specified
- **Game Fit:** 4/5 - Balanced
- **Implementation:** 3/5 - Delay + auto-select

**Rationale:** Grace period without permanent stagnation.

---

### 2.4 Excluding Tags for Class Acquisition ✅ RESOLVED

**Question:** How do "excluding_tags" work for class acquisition? Hard block or soft penalty?

**Resolution:** Soft Penalty (2× requirements) with redemption paths. See [Skill & Recipe System GDD §5](design/systems/skill-recipe-system-gdd.md#5-excluding-tags-mechanics)

**Status:** ✅ **RESOLVED** - Excluding tags multiply requirements by 2×, allows redemption arcs

**Previous State:** Unclear.

**Recommendations:**

#### Option A: Hard Block (Recommended)

```
Excluding_Tag: "magic.dark"

Logic:
IF player.magic.dark >= threshold:
    ClassRequirement = NOT_MET
    Show: "Your affinity with [Dark Magic] prevents this path"

Rationale: Narrative integrity - Paladin should not have dark affinity
```

- **Completeness:** 5/5 - Fully specified
- **Game Fit:** 5/5 - Clear narrative logic
- **Implementation:** 1/5 - Simple condition check

**Rationale:** Some classes thematically conflict with others.

#### Option B: Soft Penalty (Higher Requirement)

```
Excluding_Tag: "magic.dark" with 2× multiplier

Logic:
IF player.magic.dark >= threshold:
    RequiredPrimaryTags = BaseRequirement × 2

Can still acquire, but significantly harder
```

- **Completeness:** 4/5 - Needs multiplier tuning
- **Game Fit:** 3/5 - Allows redemption stories
- **Implementation:** 2/5 - Dynamic requirement calc

**Rationale:** Fallen paladin can become priest again, but hard.

#### Option C: Decay Exclusion Tags First

```
When acquiring class with excluding_tags:
- Excluded tags decay 2× faster
- When below threshold, class unlocks
- Takes time, not impossible
```

- **Completeness:** 4/5 - Needs decay rate
- **Game Fit:** 4/5 - Redemption arc
- **Implementation:** 3/5 - Boosted decay logic

**Rationale:** Narrative path from dark to light (and vice versa).

---

## 3. Skill System Mechanics

### 3.1 Active Skill Limit ✅ RESOLVED

**Question:** How many skills can a player have active simultaneously?

**Resolution:** Unlimited skills, 5 quick slots for rapid access. See [Skill & Recipe System GDD §2](design/systems/skill-recipe-system-gdd.md#2-skill-slot-system)

**Status:** ✅ **RESOLVED** - Unlimited skill accumulation, 5 quick slots for instant access, passives always on

**Previous State:** Unlimited? Slot-based? Per-class limit?

**Recommendations:**

#### Option A: 8 Active Slots + Passives Unlimited (Recommended)

```
Active Skills: 8 slots maximum
- Must manually equip/unequip
- Only equipped skills can be used in idle loop
- Cooldowns run only on equipped skills

Passive Skills: Unlimited
- Always active
- No slot limit
- Stacking effects allowed

Rationale: 8 slots = classic MMO feel, passives free
```

- **Completeness:** 5/5 - Fully specified
- **Game Fit:** 5/5 - Familiar and balanced
- **Implementation:** 2/5 - Slot management UI

**Rationale:** Active choices matter, passives feel rewarding.

#### Option B: Tier-Based Slots

```
Level 1-10: 4 slots
Level 11-20: 6 slots
Level 21-30: 8 slots
Level 31-40: 10 slots
Level 41-50: 12 slots

Passives: 50% of active slot count (rounded up)
```

- **Completeness:** 5/5 - Fully specified
- **Game Fit:** 5/5 - Progression feeling
- **Implementation:** 2/5 - Level-based capacity

**Rationale:** More slots = more complexity as you progress.

#### Option C: Unlimited All Skills

```
No slot limits
All learned skills always available
Tradeoff: Diminishing returns on similar skills
```

- **Completeness:** 3/5 - Needs diminishing returns formula
- **Game Fit:** 2/5 - Removes strategic choice
- **Implementation:** 3/5 - Stacking balance logic

**Rationale:** Simplest, but removes build variety.

---

### 3.2 Skill Forgetting/Respec ✅ RESOLVED

**Question:** Can skills be forgotten/replaced? What are respec mechanics?

**Resolution:** Skip - no forgetting mechanic, skills accumulate forever with search/filter UI. See [Skill & Recipe System GDD §2](design/systems/skill-recipe-system-gdd.md#2-skill-slot-system)

**Status:** ✅ **RESOLVED** - No forgetting needed, unlimited skill accumulation with robust search/filter

**Previous State:** Unresolved.

**Recommendations:**

#### Option A: Free Forget, Limited Relearn (Recommended)

```
Can Forget Any Skill:
- Always free
- Instant removal from skill list
- Tag XP invested is lost

Relearning Forgotten Skills:
- Must re-acquire via normal means
- No "remembered" bonus
- Prevents rapid respec abuse

Rationale: Let players experiment, but prevent exploitative respecing
```

- **Completeness:** 5/5 - Fully specified
- **Game Fit:** 5/5 - Encourages experimentation
- **Implementation:** 2/5 - Remove + reacquire logic

**Rationale:** Players shouldn't be trapped by bad choices.

#### Option B: Premium Currency Respec

```
Respec Cost: 100 premium currency per skill
Full Respec: 1000 currency (reset all skills)
Preserves: Tag XP, Class progress
Resets: Skill list, allows fresh selection
```

- **Completeness:** 5/5 - Fully specified
- **Game Fit:** 4/5 - Monetization opportunity
- **Implementation:** 2/5 - Currency transaction

**Rationale:** Revenue while preventing abuse.

#### Option C: Time-Based Cooldown

```
Forget Skill: Free
Relearn Same Skill: 7-day cooldown
Prevents: Rapid hot-swapping between skills
Allows: Genuine mistakes to be corrected
```

- **Completeness:** 5/5 - Fully specified
- **Game Fit:** 4/5 - Balanced approach
- **Implementation:** 2/5 - Cooldown timestamp

**Rationale:** No paywall, but prevents exploitation.

---

### 3.3 Skill Tier Tag Requirements ✅ RESOLVED

**Question:** How do skill tiers (Basic → Legendary) interact with tag requirements? Does [Greater Heal] require more tags than [Heal]?

**Resolution:** Already solved - tag hierarchy handles filtering by class tier (warrior.journeyman). See [Skill & Recipe System GDD §3.3](design/systems/skill-recipe-system-gdd.md#33-tag-based-skill-filtering)

**Status:** ✅ **RESOLVED** - Skill tiers use tag-based filtering (class.tag + class.tier), no separate bucket thresholds

**Previous State:** Unresolved.

**Recommendations:**

#### Option A: Scaling Tag Requirements (Recommended)

```
Basic Skills:     Tag requirement 15+
Intermediate:     Tag requirement 30+
Advanced:         Tag requirement 50+
Expert:           Tag requirement 70+
Master:           Tag requirement 85+
Legendary:        Tag requirement 95+

Example: magic.divine.skills
[Heal] (Basic):           magic.divine >= 15
[Greater Heal] (Adv):     magic.divine >= 50
[Mass Heal] (Master):     magic.divine >= 85
[Miracle] (Legendary):    magic.divine >= 95
```

- **Completeness:** 5/5 - Fully specified
- **Game Fit:** 5/5 - Natural progression
- **Implementation:** 1/5 - Threshold checks

**Rationale:** Higher tier = higher mastery demonstrated.

#### Option B: Multiple Tag Requirements for Higher Tiers

```
Basic:     1 tag at 15+
Advanced: 2 tags at 30+ (primary + secondary)
Legendary: 3 tags at 50+ + 1 exclusive tag

Example: [Legendary Fire Spell]
magic.fire >= 50
magic.destruction >= 50
combat.intelligence >= 50
magic.drawing >= 30 (unique to legendary tier)
```

- **Completeness:** 5/5 - Fully specified
- **Game Fit:** 5/5 - Rewards diverse play
- **Implementation:** 2/5 - Multi-tag validation

**Rationale:** Legendary skills require legendary mastery across domains.

#### Option C: Class Level Gated

```
Basic:     Class level 1+
Advanced:  Class level 20+
Legendary: Class level 40+

Tags still required for initial class acquisition
But skills gated by class progress
```

- **Completeness:** 5/5 - Fully specified
- **Game Fit:** 4/5 - Simpler tracking
- **Implementation:** 1/5 - Class level check

**Rationale:** Ties skills to class progression rather than tags.

---

### 3.4 Skill Rarity Roll Formula ✅ RESOLVED

**Question:** What is the rarity roll formula for skill acquisition? Level modifier, tag affinity modifier, luck stat?

**Resolution:** Tag affinity + Achievement bonus system with legendary early unlocks. See [Skill & Recipe System GDD §4](design/systems/skill-recipe-system-gdd.md#4-skill-rarity--achievement-system)

**Status:** ✅ **RESOLVED** - Base weighted random + tag affinity bonus (+5% per 1000 XP) + achievement bonuses (up to +50%) for extraordinary feats

**Previous State:** Unresolved.

**Recommendations:**

#### Option A: Weighted Random with Tag Boost (Recommended)

```
Base Weights:
Common:      60% (Basic skills)
Uncommon:    25% (Intermediate)
Rare:        10% (Advanced)
Epic:        4% (Expert)
Legendary:   1% (Master/Legendary)

Modifiers:
TagAffinityBonus = (TagAffinity - RequiredThreshold) × 0.5
  Example: Tag 70, Threshold 30 → +20% to higher tiers

FinalRoll = BaseRoll + TagAffinityBonus
```

- **Completeness:** 5/5 - Fully specified
- **Game Fit:** 5/5 - Rewards investment
- **Implementation:** 2/5 - Weighted RNG with modifiers

**Rationale:** Invested in tags? Better skills.

#### Option B: Pity System

```
Base: 1% legendary chance
Pity Counter: +1% per skill acquired (no legendary)
At 100 skills: Guaranteed legendary next
Reset after legendary

All tiers use separate pity counters
```

- **Completeness:** 5/5 - Fully specified
- **Game Fit:** 5/5 - Prevents bad RNG
- **Implementation:** 3/5 - Counter tracking

**Rationale:** Idle players hate pure RNG - give guarantees.

#### Option C: Choose Lowest Tier, Roll Higher

```
Player always offered: Basic skill (guaranteed)
+ Roll for higher tier with bonus from tags

Tags 50+: 25% chance for Advanced
Tags 70+: 10% chance for Expert
Tags 90+: 2% chance for Legendary

Risk/Reward: Take guaranteed Basic, or gamble on tier?
```

- **Completeness:** 5/5 - Fully specified
- **Game Fit:** 4/5 - Adds decision moment
- **Implementation:** 3/5 - Choice UI + roll logic

**Rationale:** Player agency in risk-taking.

---

## HIGH PRIORITY (Blocking Key Features)

These questions prevent implementation of major game systems but don't block the basic loop.

## 4. Economy & Resource Balance

### 4.1 Resource Sink Mechanisms

**Question:** What are the resource sink mechanisms? Repair costs, taxes, consumables mentioned but not quantified.

**Current State:** Unquantified sinks.

**Recommendations:**

#### Option A: Triple Sink System (Recommended)

```
1. CONSUMABLE SINKS:
   - Food: 10 copper / day
   - Basic tools: Break after 100 uses (cost: 50 silver)
   - Potions: 1 gold per use

2. REPAIR SINKS:
   - Equipment durability 100-0
   - Repair cost: 10% of original value per 10 durability
   - Full break: Item lost, must replace

3. TAX SINK:
   - Inn/Settlement tax: 5% of idle income
   - Guild tax: 2% (if guild member)
   - Trading post fee: 3%
```

- **Completeness:** 5/5 - Fully quantified
- **Game Fit:** 5/5 - Balanced MMO economy
- **Implementation:** 3/5 - Multiple transaction types

**Rationale:** Multiple, small sinks feel better than one big tax.

#### Option B: Usage-Based Decay

```
Every action costs:
- Base resource cost (materials consumed)
- Tool durability (equipment wears)
- 1% of value goes to "wear and tear" sink

Example: Mining action
- Consumes: 1 pickaxe durability
- Produces: 10 ore
- Sink: 5% ore lost to waste
```

- **Completeness:** 4/5 - Needs percent tuning
- **Game Fit:** 4/5 - Naturalistic
- **Implementation:** 2/5 - Percentage deductions

**Rationale:** Sinks feel integrated to gameplay, not artificial.

#### Option C: Premium Currency Drain

```
All sinks purchasable with premium currency
- Instant repair: 10 premium
- Tax exemption pass: 500 premium / month
- Durability preserver: 1000 premium (permanent)
```

- **Completeness:** 3/5 - Needs base game sinks too
- **Game Fit:** 3/5 - Pay-to-win risk
- **Implementation:** 2/5 - Currency transactions

**Rationale:** Monetization, but must be balanced with free options.

---

### 4.2 Inflation Control

**Question:** How does inflation control work in a persistent MMO economy?

**Current State:** Not addressed.

**Recommendations:**

#### Option A: Dynamic Sink Scaling (Recommended)

```
Base Sinks scale with server money supply:

WeeklyCalculation:
ServerMoneySupply = Sum(all player gold)
TargetSupply = PlayerCount × 1000 gold
If ServerMoneySupply > TargetSupply:
    SinkMultiplier = 1 + ((ServerSupply - Target) ÷ Target) × 0.5
    Example: 2× target money = 1.5× all sink costs

Applies to: Taxes, repair costs, fees
Does NOT apply: Fixed prices (NPC goods)
```

- **Completeness:** 5/5 - Fully specified algorithm
- **Game Fit:** 5/5 - Self-balancing economy
- **Implementation:** 3/5 - Server-wide tracking

**Rationale:** Economy self-corrects without manual intervention.

#### Option B: Gold Cap with Veteran Rewards

```
Player Gold Cap: 10,000 gold (level 1)
+ 1,000 gold cap per level
Level 50 cap: 60,000 gold

Excess gold: Must be spent or converted to premium currency (1:100)

Veteran Status (6+ months):
- Access to "Gold Bank" - unlimited storage
- Bank gold cannot be traded (prevents hoarding for trading)
```

- **Completeness:** 5/5 - Fully specified
- **Game Fit:** 4/5 - Prevents runaway inflation
- **Implementation:** 3/5 - Cap + separate bank system

**Rationale:** Hard caps prevent infinite accumulation.

#### Option C: Consumable-Only Economy

```
No permanent wealth accumulation
All items are consumable or decay
Resources cannot be hoarded indefinitely (decay in storage)
Money itself decays: -1% per week (floor at 0)
```

- **Completeness:** 5/5 - Fully specified
- **Game Fit:** 3/5 - May feel punishing
- **Implementation:** 2/5 - Decay timers on everything

**Rationale:** Radical solution - economy is flow, not stock.

---

### 4.3 Resource-to-XP Conversion Rate

**Question:** What is the resource-to-XP conversion rate? Gathering 100 ore = how much XP?

**Current State:** Undefined.

**Recommendations:**

#### Option A: Value-Based XP (Recommended)

```
BaseXP = ResourceValue × 0.1

Resource Values:
Copper ore: 1 copper → 0.1 XP
Iron ore: 10 copper → 1 XP
Gold ore: 100 copper → 10 XP
Dragon scale: 1000 copper → 100 XP

Gathering 100 iron ore:
100 × 1 XP = 100 XP (~3% of level 1 requirement)
```

- **Completeness:** 5/5 - Fully specified
- **Game Fit:** 5/5 - Higher value = more XP
- **Implementation:** 1/5 - Simple multiplier

**Rationale:** Valuable resources = more learning (XP).

#### Option B: Flat Rate with Bonuses

```
Base: 1 XP per item gathered
Skill Bonus: +0.1 XP per skill level
Rarity Bonus: ×2 for rare, ×5 for epic

Example: Level 20 skill, gathering rare iron ore
1 × (1 + 2.0) × 2 = 6 XP per ore
```

- **Completeness:** 5/5 - Fully specified
- **Game Fit:** 5/5 - Progresses with character
- **Implementation:** 2/5 - Multiplier stacking

**Rationale:** Skill mastery = faster learning.

#### Option C: Action-Based, Not Resource-Based

```
XP per action attempt, not per resource gathered
Mining action: 10 XP (regardless of yield)
Bonuses for:
- Rare find: +50 XP
- Critical success: +20 XP
- New discovery: +100 XP

Prevents: Exploiting low-value resources for spam XP
```

- **Completeness:** 5/5 - Fully specified
- **Game Fit:** 5/5 - Prevents grind exploits
- **Implementation:** 2/5 - Action-based tracking

**Rationale:** Effort matters, not just output.

---

### 4.4 Time-to-Level Targets

**Question:** How long should it take to reach level 10? 20? 30?

**Current State:** Unspecified.

**Recommendations:**

#### Option A: Casual Idle Pace (Recommended)

```
Target Hours (Cumulative Played Time):
Level 10:  8 hours   (1 day @ 8h/day or 1 week @ 1h/day)
Level 20:  40 hours  (1 week @ 8h/day)
Level 30:  120 hours (2 weeks @ 8h/day)
Level 40:  300 hours (1 month @ 10h/day)
Level 50:  600 hours (2 months @ 10h/day)

Assumes: Idle progress active 16h/day, active play 2h/day
```

- **Completeness:** 5/5 - Fully specified targets
- **Game Fit:** 5/5 - Accessible to casual players
- **Implementation:** 4/5 - Requires tuning of all XP sources

**Rationale:** Casual-friendly but long-term engagement.

#### Option B: Fast Mobile Game Pace

```
Level 10:  2 hours
Level 20:  8 hours
Level 30:  24 hours
Level 40:  72 hours
Level 50:  168 hours (1 week)

Target: Reach endgame in 1-2 weeks
Appeals to: Hyper-casual mobile players
```

- **Completeness:** 5/5 - Fully specified
- **Game Fit:** 3/5 - Very fast, may lack staying power
- **Implementation:** 4/5 - High XP values needed

**Rationale:** Mobile game retention = quick progression.

#### Option C: Hardcore MMO Pace

```
Level 10:  20 hours
Level 20:  100 hours
Level 30:  300 hours
Level 40:  700 hours
Level 50:  1500 hours (6 months @ 8h/day)

Target: 6+ months to max level
Appeals to: Traditional MMO players
```

- **Completeness:** 5/5 - Fully specified
- **Game Fit:** 4/5 - May be too slow for idle
- **Implementation:** 4/5 - Careful balance needed

**Rationale:** Slow progression = long-term investment.

---

### 4.5 Class Tier Tag Thresholds

**Question:** What are the tag thresholds for each class tier? Level 10 evolution requires X tags, level 20 requires Y?

**Current State:** Undefined.

**Recommendations:**

#### Option A: Scaling Single Tag (Recommended)

```
Base Class Acquisition:
- Primary tag: 15+
- Secondary tag: 10+ (if required)

Evolution Tiers:
Level 10 Evolution:
  - Primary tag: 40+
  - Secondary: 25+ (if applicable)

Level 20 Evolution:
  - Primary tag: 70+
  - Secondary: 50+

Level 30 Evolution:
  - Primary tag: 90+
  - Secondary: 75+

Level 40 Evolution:
  - Primary tag: 95+
  - Secondary: 90+
```

- **Completeness:** 5/5 - Fully specified
- **Game Fit:** 5/5 - Clear progression
- **Implementation:** 1/5 - Threshold checks

**Rationale:** Natural escalation of mastery requirements.

#### Option B: Tag Diversity Requirement

```
Level 10 Evolution:
  - 1 tag at 40+ OR 2 tags at 30+

Level 20 Evolution:
  - 2 tags at 50+ OR 3 tags at 40+

Level 30 Evolution:
  - 3 tags at 70+ OR 5 tags at 50+

Allows: Either specialization or diversity
```

- **Completeness:** 5/5 - Fully specified
- **Game Fit:** 5/5 - Multiple build paths
- **Implementation:** 3/5 - Complex condition checking

**Rationale:** Players can specialize or generalize - both viable.

#### Option C: Class-Specific Thresholds

```
Each class defines custom evolution requirements:

[Berserker Evolution]:
- combat.aggression: 60+
- combat.strength: 50+
- Total combat tags: 200+

[Healer Evolution]:
- magic.divine: 60+
- magic.restoration: 50+
- Total magic tags: 200+

Defined in: Class data files, not global formula
```

- **Completeness:** 5/5 - Fully specified approach
- **Game Fit:** 5/5 - Thematic requirements
- **Implementation:** 4/5 - Per-class data structure

**Rationale:** Each class has unique flavor and requirements.

---

### 4.6 Diminishing Returns + Premium Interaction

**Question:** How does the diminishing returns curve interact with premium players? Can rest skips reset efficiency?

**Current State:** Unresolved.

**Recommendations:**

#### Option A: Premium Skips Don't Reset Curve (Recommended)

```
Idle Efficiency Curve:
0-2 hours idle: 100% efficiency
2-8 hours: 80%
8-24 hours: 60%
24+ hours: 40% (floor)

Premium Rest Skip:
- Skips wait time, instantly applies rest bonus
- Does NOT reset efficiency curve
- Still capped at 40% if offline 24+ hours

Premium Benefit: Time convenience, not power advantage
```

- **Completeness:** 5/5 - Fully specified
- **Game Fit:** 5/5 - Non-P2W design
- **Implementation:** 2/5 - Curve calculations

**Rationale:** Premium = convenience, not game-breaking power.

#### Option B: Premium Resets to 80%

```
Free Players: Efficiency curve as above (40% floor)
Premium Players: Rest skip resets to 80% efficiency cap

Cannot reach full 100% offline, but better than free floor
```

- **Completeness:** 5/5 - Fully specified
- **Game Fit:** 3/5 - Mild P2W element
- **Implementation:** 2/5 - Conditional caps

**Rationale:** Small power advantage for supporters.

#### Option C: Premium Bonus Multiplier

```
Efficiency Curve: Same for all players
Premium Multiplier: All offline production ×1.5

Example: 40% efficiency × 1.5 premium = 60% effective
```

- **Completeness:** 5/5 - Fully specified
- **Game Fit:** 3/5 - Clear P2W mechanic
- **Implementation:** 1/5 - Simple multiplier

**Rationale:** Transparent monetization, albeit pay-to-win.

---

### 4.7 Catch-Up Mechanic Bonus

**Question:** What is the catch-up mechanic XP bonus percentage? Mentorship bonus?

**Current State:** Unspecified.

**Recommendations:**

#### Option A: Dynamic Boost Based on Gap (Recommended)

```
If PlayerLevel < ServerAverageLevel:
    CatchUpBonus = (ServerAvg - PlayerLevel) × 10%
    Max bonus: +100% XP (2×)

Example:
Server avg: 20
Player level: 10
Bonus: (20 - 10) × 10% = +100% XP

Player level: 15
Bonus: (20 - 15) × 10% = +50% XP
```

- **Completeness:** 5/5 - Fully specified
- **Game Fit:** 5/5 - Auto-balancing
- **Implementation:** 3/5 - Server average tracking

**Rationale:** Automatic, no manual intervention needed.

#### Option B: Mentorship System

```
Veteran Player (level 40+) can "Sponsor" new player:
- Sponsored player gets +25% XP
- Sponsor gets +5% of sponsored player's XP as bonus
- Max 3 sponsors per new player
- Lasts until new player reaches level 20

Social mechanic + catch-up combined
```

- **Completeness:** 5/5 - Fully specified
- **Game Fit:** 5/5 - Encourages social connection
- **Implementation:** 4/5 - Relationship tracking

**Rationale:** Veterans have incentive to help new players.

#### Option C: Purchaseable Boost

```
Boost Item: 1000 premium currency
Effect: +50% XP for 24 hours
Max duration: 7 days (must repurchase)
Alternative: Earn via achievements

Temporary catch-up for players who want to invest time/money
```

- **Completeness:** 5/5 - Fully specified
- **Game Fit:** 4/5 - Monetization + option
- **Implementation:** 2/5 - Item + timer system

**Rationale:** Player choice in catch-up speed.

---

## 5. NPC Core Systems

### 5.1 Goal Re-evaluation Triggers ✅ RESOLVED

**Question:** How frequently are NPC goals re-evaluated? Weekly mentioned, but what triggers immediate re-eval?

**Resolution:** Time-Based + Event Triggers with rate limiting. See [NPC Core Systems GDD §1](design/systems/npc-core-systems-gdd.md#1-goal-re-evaluation-triggers)

**Status:** ✅ **RESOLVED** - Weekly scheduled reviews plus immediate triggers for major events, rate-limited to 1 per day

**Previous State:** Partially defined, triggers unclear.

**Recommendations:**

#### Option A: Time-Based + Event Triggers (Recommended)

```
Regular Re-evaluation:
- Weekly (Sunday 00:00 UTC)
- Check all goals, adjust priorities

Immediate Triggers:
- Major goal completed (all subgoals done)
- Major goal failed (blocking obstacle)
- Major goal abandoned (timeout or choice)
- Player interaction significantly changes context
- World event affects goal area

Cost: 1 goal re-evaluation per NPC per day max (prevents thrashing)
```

- **Completeness:** 5/5 - Fully specified
- **Game Fit:** 5/5 - Responsive but stable
- **Implementation:** 3/5 - Trigger system + rate limit

**Rationale:** Regular review plus reactive changes.

#### Option B: Continuous Low-Probability Check

```
Every in-game hour (30 seconds):
- 1% chance to re-evaluate goals
- If goal blocked/stuck: 10% chance
- If player nearby: 5% chance

Natural-feeling adjustment without rigid schedules
```

- **Completeness:** 5/5 - Fully specified
- **Game Fit:** 4/5 - More organic
- **Implementation:** 3/5 - Probabilistic system

**Rationale:** NPCs feel more alive/less robotic.

#### Option C: Player-Initiated Only

```
NPCs only re-evaluate when:
- Player talks to them about goals
- Player affects their goal context
- Weekly scheduled check

No automatic reactivity, player-driven influence
```

- **Completeness:** 4/5 - Less dynamic
- **Game Fit:** 3/5 - More predictable
- **Implementation:** 2/5 - Event-driven only

**Rationale:** Player agency focus, NPCs less autonomous.

---

### 5.2 Concurrent Goal Limit ✅ RESOLVED

**Question:** What is the maximum number of concurrent goals per NPC? Per timeframe category? Total?

**Resolution:** Timeframe categories with survival override (8 max total). See [NPC Core Systems GDD §2](design/systems/npc-core-systems-gdd.md#2-concurrent-goal-limits)

**Status:** ✅ **RESOLVED** - 2 immediate, 3 short-term, 2 medium-term, 1 long-term. Survival interrupts all (except voluntary sacrifice for legendary achievements)

**Previous State:** Unspecified.

**Recommendations:**

#### Option A: Timeframe Categories with Limits (Recommended)

```
Immediate Goals (0-1 in-game day):     Max 2
Short-term Goals (1-7 days):           Max 3
Medium-term Goals (1-4 weeks):         Max 2
Long-term Goals (1+ months):           Max 1
Total Cap: 8 concurrent goals

Example:
Immediate: "Buy food", "Talk to blacksmith"
Short-term: "Learn mining", "Save 50 gold", "Visit capital"
Medium-term: "Build friendship with Maria"
Long-term: "Become master craftsman"
```

- **Completeness:** 5/5 - Fully specified
- **Game Fit:** 5/5 - Balanced complexity
- **Implementation:** 2/5 - Category tracking

**Rationale:** Diverse goals across time scales.

#### Option B: Priority-Weighted Slots

```
Total Goal Slots: 6

Priority Distribution:
- Critical: 2 slots (survival, urgent needs)
- Important: 2 slots (progression, relationships)
- Casual: 1 slot (hobbies, exploration)
- Dream: 1 slot (aspirations, long-term)

NPC can shift goals between slots, but max per priority fixed
```

- **Completeness:** 5/5 - Fully specified
- **Game Fit:** 5/5 - Clear priorities
- **Implementation:** 3/5 - Priority queue

**Rationale:** NPCs have limited attention, must prioritize.

#### Option C: Dynamic Slot Expansion

```
Base Slots: 3 goals
+1 slot per 10 intelligence
+1 slot per 20 level
Max cap: 10 goals

Example: Level 20, Int 15 NPC
3 + 1 + 2 = 6 goal slots

Smart NPCs = more complex lives
```

- **Completeness:** 5/5 - Fully specified
- **Game Fit:** 5/5 - Individualized NPC capacity
- **Implementation:** 3/5 - Stat-based calculation

**Rationale:** Character-driven capacity.

---

### 5.3 Conflicting Goal Resolution ✅ RESOLVED

**Question:** How are conflicting goals resolved? Priority only, or more complex resolution?

**Resolution:** Personality + Priority + Age scoring algorithm. See [NPC Core Systems GDD §3](design/systems/npc-core-systems-gdd.md#3-conflicting-goal-resolution)

**Status:** ✅ **RESOLVED** - Three-stage algorithm: priority score + age bonus → personality tiebreaker within 15 points

**Previous State:** Unresolved.

**Recommendations:**

#### Option A: Priority + Time Decay (Recommended)

```
Each Goal Has:
- Priority Score (0-100)
- Creation Time
- Urgency Factor

Conflict Resolution:
1. Compare Priority Scores
2. If within 10 points: Prefer newer goal
3. If older goal > 7 days: +20 priority (decay boost)
4. Re-evaluate weekly

Example:
Goal A: Priority 50, created 10 days ago → Effective 70
Goal B: Priority 60, created 2 days ago → Effective 60
Winner: Goal A (age bonus)
```

- **Completeness:** 5/5 - Fully specified
- **Game Fit:** 5/5 - Dynamic, nuanced
- **Implementation:** 3/5 - Scoring algorithm

**Rationale:** Neither pure priority nor pure recency - hybrid approach.

#### Option B: Personality-Based Tiebreaker

```
Each NPC has personality traits affecting decisions:
- Impulsive: Prefers newer goals
- Deliberative: Prefers older, established goals
- Opportunistic: Prefers higher-reward goals
- Cautious: Prefers lower-risk goals

Conflicts resolved by personality roll
```

- **Completeness:** 5/5 - Fully specified
- **Game Fit:** 5/5 - Character-driven behavior
- **Implementation:** 4/5 - Personality system + weighted RNG

**Rationale:** NPCs feel more individual/less generic.

#### Option C: Player Influence

```
NPC asks player for advice when conflicted:
"I want to [Goal A] but also [Goal B]. What do you think?"

Options:
- Suggest Goal A (NPC follows advice)
- Suggest Goal B (NPC follows advice)
- "Follow your heart" (NPC decides based on personality)

Relationship boost: +5 affinity for advice given
```

- **Completeness:** 5/5 - Fully specified
- **Game Fit:** 5/5 - Player involvement
- **Implementation:** 4/5 - Choice UI + outcome tracking

**Rationale:** Player becomes mentor/guide to NPCs.

---

### 5.4 Secret NPC Goals ✅ RESOLVED

**Question:** Can NPCs have secret goals hidden from players? Villain covert operations implied, but mechanics?

**Resolution:** Goal Visibility Tiers with information disclosure skills. See [NPC Core Systems GDD §4](design/systems/npc-core-systems-gdd.md#4-secret-npc-goals--information-disclosure)

**Status:** ✅ **RESOLVED** - PUBLIC/REVEALABLE/SECRET/VILLAIN tiers with [Observation], [Insight], [True Sight] skills and item requirements

**Previous State:** Implied but undefined.

**Recommendations:**

#### Option A: Goal Visibility Tiers (Recommended)

```
Goal Visibility Levels:
1. PUBLIC: Shown in dialogue, auto-revealed
2. REVEALABLE: Hidden until trust threshold met
3. SECRET: Never shown in dialogue, must discover through observation
4. VILLAIN: Hidden, malicious, discovery dangerous

Discovery Mechanics:
- Observation skill reveals SECRET goals
- High trustNPC reveals REVEALABLE
- Villain goals: Only discoverable by catching them in act
- Betrayal: Discovering villain goal may trigger combat

Example: Villain "Gather army" (SECRET)
Player discovers by:
- Finding letter (0.1% chance on loot)
- Seeing NPC meet with shady figures (observation)
- Being told by another NPC (rumors)
```

- **Completeness:** 5/5 - Fully specified
- **Game Fit:** 5/5 - Deep gameplay systems
- **Implementation:** 4/5 - Visibility system + discovery conditions

**Rationale:** Rich NPC psychology, player investigation matters.

#### Option B: Trust-Based Revelation

```
All goals potentially visible, but locked behind trust:
- Trust 0-20: Only 1 goal shown (immediate needs)
- Trust 20-50: +2 goals revealed
- Trust 50-80: +3 goals revealed
- Trust 80-100: All goals revealed

Villains: Lie about goals even at high trust
- Show fake benevolent goals
- Hide true malicious goals
- Only discoverable through actions/observation
```

- **Completeness:** 5/5 - Fully specified
- **Game Fit:** 5/5 - Relationship-driven
- **Implementation:** 3/5 - Trust gates + fake goals

**Rationale:** Deep relationships = deep knowledge, except liars.

#### Option C: No Secret Goals

```
All NPC goals visible and honest
Simpler system, less ambiguity
Focus on: Helping NPCs achieve goals, not uncovering secrets
```

- **Completeness:** 5/5 - Fully specified (simple)
- **Game Fit:** 3/5 - Less intrigue
- **Implementation:** 1/5 - All goals public

**Rationale:** Cozy game, less deception/drama.

---

### 5.5 Goal Failure vs Abandonment ✅ RESOLVED

**Question:** How is goal "failure" determined vs "abandoned"? Timeout? Blocked progress?

**Resolution:** State-based classification (BLOCKED/ABANDONED/FAILED). See [NPC Core Systems GDD §5](design/systems/npc-core-systems-gdd.md#5-goal-failure-vs-abandonment-states)

**Status:** ✅ **RESOLVED** - BLOCKED (temporary, 4-week threshold) → ABANDONED (voluntary, no penalty) or FAILED (permanent blocker, -10 mood for 3 days)

**Previous State:** Unclear.

**Recommendations:**

#### Option A: State-Based Classification (Recommended)

```
GOAL ABANDONED:
- NPC chooses to stop pursuing
- Trigger: New higher-priority goal conflicts
- Trigger: Goal no longer aligns with personality/values
- No penalty, just state change

GOAL FAILED:
- External blocking factor cannot be overcome
- Example: "Kill 10 rats" → rats exterminated from area
- Example: "Buy sword" → blacksmith died, shop closed
- NPC feels failure (mood debuff), may set new similar goal

GOAL BLOCKED (temporary):
- Currently cannot pursue, but might change
- Example: "Mine iron" → mine overrun by monsters
- Re-evaluates weekly
- If blocked 4+ weeks: auto-abandoned or failed

Mood Effects:
Abandoned: Neutral (life happens)
Failed: -10 mood (sadness) for 3 days
Blocked: -5 mood (frustration) while blocked
```

- **Completeness:** 5/5 - Fully specified
- **Game Fit:** 5/5 - Rich NPC psychology
- **Implementation:** 3/5 - State machine + mood system

**Rationale:** NPCs have emotional responses to setbacks.

#### Option B: Timeout System Only

```
All goals have deadline based on timeframe:
Immediate: 1 day
Short-term: 7 days
Medium-term: 30 days
Long-term: 90 days

At deadline: Auto-abandoned (no failure state)
NPC may immediately set similar goal with fresh deadline
```

- **Completeness:** 5/5 - Fully specified
- **Game Fit:** 3/5 - Less emotional depth
- **Implementation:** 1/5 - Simple timeout

**Rationale:** Simple, predictable, less emotional complexity.

#### Option C: Player Choice System

```
NPC cannot abandon/fail without player input
When blocked/timeout, NPC asks player:
"I can't achieve [Goal]. Should I give up or find another way?"

Player options:
- "Yes, give it up" → NPC abandons, sets new goal
- "No, keep trying" → NPC persists, may gain inspiration
- "Let me help" → Player can unblock goal

Player involved in all major NPC decisions
```

- **Completeness:** 5/5 - Fully specified
- **Game Fit:** 5/5 - Player-centric
- **Implementation:** 4/5 - Dialogue prompts + branching

**Rationale:** Player is mentor/guide to NPCs.

---

## 6. Content Creation Minimums

### 6.1 Recipe Count for Launch

**Question:** How many recipes are needed for launch? Per craft category?

**Current State:** Unspecified.

**Recommendations:**

#### Option A: Lightweight Launch (Recommended for Prototype)

```
CRAFTING CATEGORIES:

Blacksmithing: 15 recipes
  5 Basic (nails, hinges, simple tools)
  5 Intermediate (swords, armor, shield)
  5 Advanced (masterwork items, special materials)

Woodworking: 12 recipes
  4 Basic (planks, simple furniture)
  4 Intermediate (bows, staves, complex furniture)
  4 Advanced (masterwork bows, magical staves)

Alchemy: 15 recipes
  5 Health potions (tiers)
  5 Mana potions (tiers)
  5 Buff potions (strength, speed, etc.)

Cooking: 10 recipes
  3 Basic (stew, bread, roasted meat)
  4 Intermediate (complex meals, buff food)
  3 Advanced (feasts, party food)

Enchanting: 8 recipes
  3 Basic (simple enchants)
  3 Intermediate (elemental enchants)
  2 Advanced (legendary enchants)

TOTAL: 60 recipes for prototype
TARGET: 150 recipes for alpha launch
```

- **Completeness:** 5/5 - Fully specified
- **Game Fit:** 5/5 - Incremental approach
- **Implementation:** 3/5 - Recipe data structure

**Rationale:** Start lean, expand based on player feedback.

#### Option B: Feature-Complete Launch

```
10 crafting categories × 30 recipes each = 300 recipes

Categories:
- Blacksmithing, Woodworking, Alchemy, Cooking, Enchanting
- Leatherworking, Tailoring, Jewelcrafting, Engineering, Inscription

Per category:
10 Basic, 10 Intermediate, 10 Advanced
```

- **Completeness:** 5/5 - Fully specified
- **Game Fit:** 3/5 - Heavy content creation burden
- **Implementation:** 4/5 - Massive recipe database

**Rationale:** Launch with full feature set, slower development.

#### Option C: Procedural Generation

```
Base recipes: 30 per category
Procedural variants: Material combinations

Example: Sword recipe
Base: Iron Sword
Variants: Steel, Silver, Gold, Mithril, Adamantine, etc.

Total: 30 base × 5 variants = 150 effective recipes
Less manual balancing, more systematic generation
```

- **Completeness:** 4/5 - Needs generation rules
- **Game Fit:** 4/5 - Scalable approach
- **Implementation:** 4/5 - Procedural system

**Rationale:** More content with less manual work.

---

### 6.2 Spell Count per Magic School

**Question:** How many spells per magic school? Minimum viable spell list?

**Current State:** Unspecified.

**Recommendations:**

#### Option A: Core Spell Set (Recommended)

```
SPELLS PER SCHOOL (6 schools total)

Magic Schools:
- Divine, Elemental, Nature, Dark, Arcane, Mind

Per School (15 spells):
5 Basic (levels 1-10)
  2 damage, 2 utility, 1 buff

5 Intermediate (levels 11-25)
  2 damage, 2 utility, 1 buff

5 Advanced (levels 26-50)
  2 damage, 2 utility, 1 AoE/ultimate

TOTAL: 90 spells for prototype
Examples - DIVINE SCHOOL:
Basic: Heal, Smite, Bless, Cure Wounds, Light
Intermediate: Greater Heal, Holy Fire, Protection, Cure Disease, Sanctuary
Advanced: Mass Heal, Divine Storm, Resurrection, Holy Shield, Miracle
```

- **Completeness:** 5/5 - Fully specified
- **Game Fit:** 5/5 - Balanced distribution
- **Implementation:** 3/5 - Spell database

**Rationale:** Every school feels complete and useful.

#### Option B: Progression-Unlocked

```
Per School: 20 spells total
Tier 1 (level 1-10): 10 spells
Tier 2 (level 11-25): 6 spells
Tier 3 (level 26-50): 4 spells

TOTAL: 120 spells (6 schools × 20)
More spells early, fewer rare spells
```

- **Completeness:** 5/5 - Fully specified
- **Game Fit:** 4/5 - Front-loaded variety
- **Implementation:** 3/5 - Level-gated access

**Rationale:** Early game has tons of variety, endgame is about mastery.

#### Option C: Overlapping Schools

```
Total spells: 60
Each spell belongs to 1-3 schools

Example: [Fireball]
- Primary: Elementalist (fire)
- Secondary: Mage (arcane)
- Tertiary: Destroyer (combat)

Player learns spell once, can use with any qualified school

Reduces total unique spells to create, increases build flexibility
```

- **Completeness:** 4/5 - Needs overlap rules
- **Game Fit:** 5/5 - Systemic depth
- **Implementation:** 4/5 - Multi-tag spell system

**Rationale:** More strategic depth, less content burden.

---

### 6.3 Recipe/Spell Discovery Rate

**Question:** What is the recipe/spell discovery rate? Random drops vs guaranteed progression?

**Current State:** Unresolved.

**Recommendations:**

#### Option A: Hybrid System (Recommended)

```
CORE RECIPES (50%): Guaranteed progression
- Unlock via skill level milestones
- Example: Blacksmithing level 5 → Unlock [Iron Sword] recipe

RARE RECIPES (30%): Random discovery
- World drops, dungeon chests, rare NPCs
- Example: 1% drop chance from elite mobs

LEGENDARY RECIPES (10%): Special events
- World bosses, achievements, quests
- Example: Complete "Master Blacksmith" quest → [Mithril Armor]

QUEST RECIPES (10%): NPC rewards
- Help NPC achieve goal → They teach you recipe
- Example: Blacksmith NPC grateful → Teaches secret technique

Distribution:
- Every 5 skill levels: 1 guaranteed recipe
- Random drops throughout gameplay
- Special recipes from world content
```

- **Completeness:** 5/5 - Fully specified
- **Game Fit:** 5/5 - Multiple progression paths
- **Implementation:** 3/5 - Multiple unlock systems

**Rationale:** Reliable progression + exciting discoveries.

#### Option B: All Guaranteed

```
No RNG in recipe/spell acquisition
All unlocks tied to:
- Skill level thresholds
- Class progression
- Quest completion
- NPC relationship milestones

Predictable, planning-friendly
```

- **Completeness:** 5/5 - Fully specified
- **Game Fit:** 4/5 - Less exciting discovery
- **Implementation:** 2/5 - Threshold checks only

**Rationale:** Idle players love predictable progression.

#### Option C: Heavy RNG with Pity

```
All recipes via random drops
Base drop rate: 0.1% per action
Pity timer: +0.1% per action without discovery (guarantee in 1000 actions)
Rarity affects drop rate:
Basic: 0.5% (guarantee in 200 actions)
Legendary: 0.01% (guarantee in 10,000 actions)
```

- **Completeness:** 5/5 - Fully specified
- **Game Fit:** 3/5 - May feel frustrating
- **Implementation:** 3/5 - Pity system + RNG

**Rationale:** Gambling excitement, but with safety net.

---

## 7. UI/UX Core Requirements

### 7.1 Context-Sensitive Action Bar Algorithm

**Question:** How does the context-sensitive action bar determine which actions to show? Priority algorithm needed.

**Current State:** Undefined.

**Recommendations:**

#### Option A: Weighted Score System (Recommended)

```
Each Action Has Score Components:
1. LOCATION RELEVANCE: 100 points if valid here, 0 if not
2. TAG AFFINITY: +1 point per affinity
3. RECENT USE: +20 points if used last 5 minutes
4. SKILL LEVEL: +0.5 points per skill level
5. RESOURCE READINESS: +50 points if have resources, -100 if not

Top 6 actions shown (or scrollable)

Example: Mining action (cave location, player has mining 15, 50 affinity, pickaxe equipped)
Score = 100 (location) + 50 (affinity) + 10 (skill) + 50 (ready) = 210
```

- **Completeness:** 5/5 - Fully specified
- **Game Fit:** 5/5 - Smart, adaptive UI
- **Implementation:** 3/5 - Scoring algorithm + sorting

**Rationale:** Always shows most relevant actions.

#### Option B: Player-Customized Hotbar

```
Player pins 6 favorite actions to hotbar
Always shown regardless of context
Plus 2 "context slots" that auto-fill based on location

Default: All 8 slots auto-populated
Player can override by dragging actions to slots
```

- **Completeness:** 5/5 - Fully specified
- **Game Fit:** 5/5 - Player control + smart defaults
- **Implementation:** 3/5 - Saved preferences + auto-fill

**Rationale:** Player agency with helpful automation.

#### Option C: Location-Based Tabs

```
Actions organized by context:
- COMBAT tab (shows in combat zones)
- CRAFTING tab (shows near crafting stations)
- GATHERING tab (shows near resources)
- SOCIAL tab (shows near NPCs)

Player selects active tab, or tabs auto-switch based on location
Within tab: Show all valid actions, sorted by player preference
```

- **Completeness:** 5/5 - Fully specified
- **Game Fit:** 4/5 - More manual organization
- **Implementation:** 3/5 - Tab system + location detection

**Rationale:** Clear organization, less algorithmic.

---

### 7.2 Idle Progress Notification Strategy

**Question:** What is the notification strategy for idle progress? Push notifications? In-game mail?

**Current State:** Undefined.

**Recommendations:**

#### Option A: Smart Notification Filtering (Recommended)

```
NOTIFICATION TIERS:

URGENT (Push notification):
- Major events completed
- Rare loot acquired
- NPC relationship milestone
- Class evolution available

IMPORTANT (In-game summary):
- Level up
- Skill learned
- Resource threshold reached
- Quest completed

INFO (Log only):
- Common loot
- Small resource gains
- NPC minor events

SMART FILTERING:
- Group notifications: Max 1 push per 30 minutes
- Digest mode: Summary every 4 hours instead of individual pushes
- Quiet hours: No push notifications 10pm-8am (user setting)

Player controls all settings
```

- **Completeness:** 5/5 - Fully specified
- **Game Fit:** 5/5 - Respectful of player attention
- **Implementation:** 4/5 - Notification system + preferences

**Rationale:** Players feel informed without being spammed.

#### Option B: Opt-In Only

```
All notifications disabled by default
Player chooses what to enable:
- Push notifications (opt-in per category)
- In-game popups (opt-in per category)
- Mail summaries (daily/weekly digest)

Default: Only show summary on app open
```

- **Completeness:** 5/5 - Fully specified
- **Game Fit:** 5/5 - Maximum player control
- **Implementation:** 3/5 - Granular permissions

**Rationale:** Some players hate notifications, let them choose.

#### Option C: Rich Summary System

```
No push notifications
Instead: Beautiful "Return Screen" when player opens app

Shows:
- Time away
- Resources gained (animated counters)
- Level progress (visual bar)
- Events that happened (story log)
- NPCs who missed player (relationship updates)

Feels rewarding, not annoying
```

- **Completeness:** 5/5 - Fully specified
- **Game Fit:** 5/5 - Delightful UX
- **Implementation:** 4/5 - Rich UI + offline tracking

**Rationale:** Turn "what did I miss?" into "wow, look at all this!"

---

## 8. Combat System GDD - Formula Gaps (Design Review Findings)

**Context:** The Combat System GDD received a comprehensive design review (2026-02-03) with an overall rating of **4.5/5**. The following critical and important gaps were identified and have now been **RESOLVED** in version 1.1.0.

### 8.1 Combat XP Reward Formula ✅ RESOLVED

**Question:** How much XP does a character gain from combat actions? Base XP per hit, kill, skill use?

**Resolution:** Action-based XP with enemy level modifier, party size modifier, and first-kill bonus. See [Combat System GDD §16.1](design/systems/combat-system-gdd.md#161-combat-xp-reward-formula)

**Status:** ✅ **RESOLVED** - Full formula with base XP rewards, modifiers, and examples documented

**Previous State:** Combat system mentioned "XP Gained: combat.melee.sword +25 XP" but no formula defining rewards.

**Recommendations:**

#### Option A: Action-Based XP (Recommended)

```
Base XP Rewards:
- Basic attack (hit): 5 XP to relevant bucket
- Basic attack (kill): +10 XP bonus
- Skill use (hit): 8 XP to relevant bucket
- Skill use (kill): +15 XP bonus
- Taking damage: +2 XP to combat.toughness bucket

Modifiers:
× Enemy Level Modifier (if enemy level > character level):
  - Same level: ×1.0
  - 1-3 levels higher: ×1.2
  - 4-6 levels higher: ×1.5
  - 7+ levels higher: ×2.0 (cap)

× Party Size Modifier:
  - Solo: ×1.0
  - 2 characters: ×0.8
  - 3 characters: ×0.7
  - 4+ characters: ×0.6

Example: Level 5 Marcus kills Level 6 Goblin Warrior
Basic attack: 5 XP
Kill bonus: +10 XP
Enemy level modifier: ×1.2 (goblin is 1 level higher)
Total: (5 + 10) × 1.2 = 18 XP to combat.melee.sword
```

- **Completeness:** 5/5 - Fully specified formula
- **Game Fit:** 5/5 - Rewards challenging combat
- **Implementation:** 2/5 - Simple calculation on combat actions

**Rationale:** Players progress faster fighting challenging enemies solo, slower in groups (balance idle farming).

---

### 8.2 Armor Absorption Formula ✅ RESOLVED

**Question:** What determines how much damage armor absorbs? Is it a DEF stat? Material-based? Fixed value?

**Resolution:** BaseArmor × (1 + ENDModifier) formula with degradation rate per armor type. See [Combat System GDD §16.2](design/systems/combat-system-gdd.md#162-armor-absorption-formula)

**Status:** ✅ **RESOLVED** - Full armor calculation with END modifier table, durability loss, and shield integration

**Previous State:** Combat example shows "Armor layer: Absorbs 8" but no formula for calculating absorption.

**Recommendations:**

#### Option A: Armor Value + END Modifier (Recommended)

```
Armor Absorption Formula:
BaseArmor = Equipment Armor Value (from item stats)
ENDModifier = END ÷ 10 (END 12 = +20% absorption)

TotalAbsorption = BaseArmor × (1 + ENDModifier)
MaxAbsorption = BaseArmor × 1.5 (hard cap)

Example: Chain Shirt (Armor Value: 8) + END 13 (+30%)
TotalAbsorption = 8 × 1.3 = 10.4 → 10 damage absorbed

Incoming Damage: 25
→ Shield absorbs first (if present)
→ Armor absorbs: min(25, 10) = 10 damage
→ Armor durability decreases: 10 ÷ 15 = 1 durability lost
→ Overflow: 25 - 10 = 15 damage to health
```

- **Completeness:** 5/5 - Fully specified
- **Game Fit:** 5/5 - END matters for tanks
- **Implementation:** 2/5 - Simple formula

**Rationale:** High-END characters get more value from armor, reinforcing tank builds.

---

### 8.3 AI Threat Calculation ✅ RESOLVED

**Question:** How does AI determine which enemy is "highest threat" for targeting?

**Resolution:** Composite threat score (DamageDealt × 1.0 + Presence × 0.5 + Debuffs × 0.3 + Level × 0.2 + ThreatBonus). See [Combat System GDD §16.3](design/systems/combat-system-gdd.md#163-ai-threat-calculation)

**Status:** ✅ **RESOLVED** - Full threat formula with decay, recalculation timing, and personality-based overrides

**Previous State:** Role-based targeting mentions "High-threat enemies" but no threat formula.

**Recommendations:**

#### Option A: Composite Threat Score (Recommended)

```
Threat Score Calculation:
Threat = (DamageDealt × 1.0) + (Presence × 0.5) + (Debuffs × 0.3) + (Level × 0.2)

DamageDealt: Total damage to this character in last 10 seconds
Presence: Character is in front row (+20 threat) or attacking ally (+10)
Debuffs: Number of debuffs applied (poison, stun, etc.)
Level: Character level (higher level = more threat)

Example:
Elena (Mage): 45 damage dealt + front row (20) + 1 poison (3) + level 7 (1.4)
Threat = 45 + 10 + 3 + 1.4 = 59.4

Marcus (Warrior): 12 damage dealt + front row (20) + 0 debuffs + level 8 (1.6)
Threat = 12 + 10 + 0 + 1.6 = 23.6

AI Target: Elena (higher threat)
```

- **Completeness:** 5/5 - Fully specified algorithm
- **Game Fit:** 5/5 - Smart AI targeting
- **Implementation:** 3/5 - Track damage over time window

**Rationale:** Threat reflects actual danger, not just raw damage output.

---

### 8.4 Power Ratio Calculation ✅ RESOLVED

**Question:** How does the game detect "outmatched 3× stronger" for surrender/morale triggers?

**Resolution:** Character Power = (Attack × 1.0) + (Defense × 0.5) + (MaxHP × 0.1) + (Speed × 0.3) + (Level × 5). See [Combat System GDD §16.4](design/systems/combat-system-gdd.md#164-power-ratio-calculation)

**Status:** ✅ **RESOLVED** - Complete power calculation formula with thresholds for surrender/morale triggers

**Previous State:** Surrender system mentions "Power Ratio < 0.5" but no power calculation formula.

**Recommendations:**

#### Option A: Stat-Based Power Rating (Recommended)

```
Character Power Rating:
Power = (Attack × 1.0) + (Defense × 0.5) + (MaxHP × 0.1) + (Level × 5)

Attack: Average damage per second (weapon damage × attack speed)
Defense: Armor value + shield value
MaxHP: Maximum health pool
Level: Character level

Party Power = Sum of all member power ratings

Power Ratio = YourPartyPower ÷ EnemyPartyPower

Example:
Your Party: Marcus (150) + Elena (120) + Kira (90) + Toren (110) = 470
Enemy Party: Goblin Warrior (45) + Goblin Spearman (35) + Goblin Shaman (40) = 120
Power Ratio = 470 ÷ 120 = 3.92

Outmatched Check: 3.92 > 3.0 → Enemies may surrender
Morale Check: Each death causes morale check (see Combat GDD §9.2)
```

- **Completeness:** 5/5 - Fully specified
- **Game Fit:** 5/5 - Accurate strength assessment
- **Implementation:** 2/5 - Simple stat aggregation

**Rationale:** Power rating reflects overall combat capability, not just damage.

---

### 8.5 Offline Combat Resolution ✅ RESOLVED

**Question:** How does combat resolve during offline simulation? Statistical formula or tick simulation?

**Resolution:** Simplified turn-based simulation (max 50 turns) with full combat formulas. Uses deterministic RNG seed for event sourcing compatibility. Skills, buffs, debuffs apply tactically. See [Combat System GDD §16.5](design/systems/combat-system-gdd.md#165-offline-combat-resolution)

**Status:** ✅ **RESOLVED** - Simulated combat with unique outcomes, full XP, deterministic RNG (event source compatible)

**Previous State:** Background mode mentioned but no resolution algorithm specified.

**Recommendations:**

#### Option B: Simulated Turn-Based (SELECTED) ✅

```
OFFLINE COMBAT SIMULATION:

Run simplified combat loop (max 50 turns per combat):
- Turn 1: Your party acts (initiative order)
- Turn 2: Enemy party acts
- Repeat until one side eliminated or 50 turns max

Features:
- Uses same combat formulas as online (damage, skills, buffs)
- AI controls all characters (personality targeting)
- Skills auto-cast by priority (AI logic)
- Deterministic RNG seed (PlayerID + EnemyID + Timestamp)
- Max 50 turns prevents infinite loops

Outcomes:
- Victory: Full loot table (no rare/equipment offline)
- Defeat: Characters fall (survive), no loot
- Morale break: Losing side flees (survives)
- Turn 50: Draw (both flee, no loot)

Time to resolve: ~5-50ms per combat (depends on turns)
```

- **Completeness:** 5/5 - Fully specified simulation system
- **Game Fit:** 5/5 - Authentic to online combat, unique outcomes
- **Implementation:** 3/5 - Combat simulation loop with deterministic RNG

**Rationale:** Event-sourced RNG ensures combat results sync between players. Skills matter tactically. Each combat is unique but reproducible.

---

### 8.6 Attribute Progression per Class Level ✅ RESOLVED

**Question:** Which attributes do classes grant at level-up? Is it random or fixed?

**Resolution:** Fixed per-class progression tables with primary/secondary attributes and 20% random variation bonus. See [Combat System GDD §16.6](design/systems/combat-system-gdd.md#166-attribute-progression-per-class-level)

**Status:** ✅ **RESOLVED** - Complete progression tables for Martial, Magic, and Hybrid classes with multi-classing rules

**Previous State:** Combat GDD shows "+1-2 to class-relevant attributes" but no mapping table.

**Recommendations:**

#### Option A: Class-Specific Attribute Tables (Recommended)

```
Class Attribute Progression (per level-up):

[Warrior]:
  Primary: STR +1, END +1 (every level)
  Secondary: AWR +1 (every 2 levels)

[Mage]:
  Primary: WIT +1 (every level)
  Secondary: END +1 (every 2 levels), AWR +1 (every 3 levels)

[Healer]:
  Primary: CHA +1, WIT +1 (every level)
  Secondary: END +1 (every 2 levels)

[Ranger]:
  Primary: FIN +1, AWR +1 (every level)
  Secondary: STR +1 (every 2 levels)

[Rogue]:
  Primary: FIN +1 (every level)
  Secondary: AWR +1 (every 2 levels), CHA +1 (every 3 levels)

Multi-Classing:
  - XP split applies to attribute gains
  - Example: 60/40 split → [Warrior] gets STR+1 END+1, [Mage] gets WIT+0
```

- **Completeness:** 5/5 - Fully specified per class
- **Game Fit:** 5/5 - Classes have distinct identities
- **Implementation:** 2/5 - Class level-up tables

**Rationale:** Clear character advancement, predictable build paths.

---

### 8.7 Multiplayer Possession Scenarios ✅ RESOLVED

**Question:** What happens if 2+ players possess characters in the same fight?

**Resolution:** Turn-based mode with initiative order, 30s timer per turn, chat/suggest/ping systems, and conflict resolution rules. See [Combat System GDD §16.7](design/systems/combat-system-gdd.md#167-multiplayer-possession-scenarios)

**Status:** ✅ **RESOLVED** - Complete multiplayer rules including possession priority, timeout handling, and AFK detection

**Previous State:** Multiplayer section mentions "if multiplayer AND other_players_involved: Switch to TURN-BASED" but doesn't address multiple possessions.

**Recommendations:**

#### Option A: First-Come First-Served (Recommended)

```
Multiplayer Possession Rules:

If 2+ players possess characters in same combat:
→ Turn-based mode activates (as specified)

Possession Priority:
1. First player to possess gets "Party Leader" role
2. Party Leader can issue orders to AI-controlled allies
3. Other possessed characters act on their turns
4. No direct control over other players' possessed characters

Possession Transfer:
- Player can release possession at any time
- Released character returns to AI control
- Another player can then possess (if favourited)

Communication:
- Built-in chat for coordination
- "Suggest Action" feature (non-binding recommendation to ally)
- Ping system (highlight enemy, request heal)
```

- **Completeness:** 5/5 - Fully specified multiplayer rules
- **Game Fit:** 5/5 - Fair multiplayer experience
- **Implementation:** 3/5 - Party role + chat system

**Rationale:** Prevents conflicts, enables coordination, maintains agency.

---

## 9. Technical Architecture Decisions

These questions affect game quality and player experience but can be iterated on post-prototype.

### 9.1 Maximum Offline Simulation Duration

**Question:** What is the maximum offline time supported? 24h? 48h? Unlimited with diminishing returns?

**Recommendations:**

#### Option A: 48-Hour Cap with Diminishing Returns (Recommended)

```
Offline Simulation:
0-8 hours: 100% efficiency
8-24 hours: 50% efficiency
24-48 hours: 25% efficiency
48+ hours: Cap at 48 hours (no more progress)

Rationale:
- Prevents server calculation abuse
- Players rewarded for daily check-ins
- Long offline doesn't break progression

UI Message: "You were away 5 days! Simulated 48 hours of progress."
```

- **Completeness:** 5/5 - Fully specified
- **Game Fit:** 5/5 - Balanced retention mechanic
- **Implementation:** 2/5 - Time cap calculation

**Rationale:** Encourages regular play without punishing casual players.

#### Option B: Weekly Cap

```
Maximum offline simulation: 7 days (168 hours)
Linear scaling: 100% efficiency for entire duration
After 7 days: Hard cap, no more progress

Accommodates: Weekly play patterns (weekend warriors)
```

- **Completeness:** 5/5 - Fully specified
- **Game Fit:** 4/5 - More casual-friendly
- **Implementation:** 1/5 - Simple time cap

**Rationale:** Some players only play on weekends.

#### Option C: Unlimited with Compression

```
No hard cap on offline time
Compression ratio: 1 hour real time = 10 hours game time
Example: Away 48 hours = 480 hours (20 days) simulated

Downside: Very long absences create huge jumps in progression
Mitigation: Server-side validation limits (prevent exploits)
```

- **Completeness:** 4/5 - Needs validation details
- **Game Fit:** 3/5 - Risky for balance
- **Implementation:** 4/5 - Complex validation system

**Rationale:** Maximum flexibility, maximum risk.

---

### 8.2 Offline Combat Resolution ✅ RESOLVED

**Question:** How are combat encounters resolved during offline simulation? Simplified combat model needed.

**Resolution:** Three-mode combat system implemented - Background Simulation (tick-based instant), Spectator Mode (animated tick-based), Player Control (real-time with pause). Full combat mechanics with mode-adaptive complexity. See [Combat System GDD §1](design/systems/combat-system-gdd.md#1-combat-modes)

**Status:** ✅ **RESOLVED** - Combat system fully specified with all mechanics

**Recommendations:**

#### Option A: Statistical Resolution (Recommended)

```
OFFLINE COMBAT MODEL:

Player Stats: Attack, Defense, HP
Enemy Stats: Same (from database)

Resolution Algorithm:
1. Calculate PlayerPower = Attack + (Defense × 0.5)
2. Calculate EnemyPower = Attack + (Defense × 0.5)
3. PowerRatio = PlayerPower ÷ EnemyPower

4. Outcomes:
   - PowerRatio > 1.5: Win (0-10% damage taken)
   - 1.0 < PowerRatio < 1.5: Win (10-30% damage taken)
   - 0.8 < PowerRatio < 1.0: 50% win, 50% flee
   - PowerRatio < 0.8: Flee (no loot, no death)

5. Loot: Simplified drop table (no rare items offline)

Time to resolve: Instant (math only)
```

- **Completeness:** 5/5 - Fully specified
- **Game Fit:** 5/5 - Fair, predictable
- **Implementation:** 2/5 - Power calculation

**Rationale:** Deterministic, server-efficient, no RNG abuse.

#### Option B: Simulated Turn-Based

```
Run simplified combat loop:
- Turn 1: Player attacks (damage - enemy defense)
- Turn 2: Enemy attacks (damage - player defense)
- Repeat until one dies

Max 50 turns per combat (prevents infinite loops)
After 50: Draw, both flee

More accurate, but more computationally expensive
```

- **Completeness:** 5/5 - Fully specified
- **Game Fit:** 4/5 - More detailed
- **Implementation:** 3/5 - Combat simulation loop

**Rationale:** More authentic to real combat experience.

#### Option C: Skip Combat Offline

```
No combat during offline simulation
Only gathering, crafting, resting
Player must be online for combat encounters

Encounters paused: "Awaiting your return"
On login: Resolve combat with real-time combat system
```

- **Completeness:** 5/5 - Fully specified
- **Game Fit:** 3/5 - Breaks immersion
- **Implementation:** 2/5 - Encounter pausing

**Rationale:** Combat is fun, don't automate it.

---

### 8.3 Tag Affinity Database Schema

**Question:** What is the database schema for tag affinity storage? Sparse vs dense?

**Recommendations:**

#### Option A: Sparse Storage (Recommended)

```
TABLE: PlayerTags
- PlayerID (GUID)
- TagName (string) - e.g., "combat.sword", "craft.blacksmith"
- Affinity (int) - 0-100
- LastUpdated (timestamp)

INDEX: PlayerID + TagName (unique)

Only stores tags with affinity > 0
Player with 50 tags = 50 rows
Player with 5 tags = 5 rows

Query: "Get all tags for player"
SELECT * FROM PlayerTags WHERE PlayerID = ?

Query: "Get specific tag"
SELECT Affinity FROM PlayerTags WHERE PlayerID = ? AND TagName = ?
```

- **Completeness:** 5/5 - Fully specified schema
- **Game Fit:** 5/5 - Scalable to unlimited tags
- **Implementation:** 2/5 - Standard DB table

**Rationale:** Most players only use fraction of available tags.

#### Option B: Dense Storage (JSON Column)

```
TABLE: Players
- PlayerID (GUID)
- AllTags (JSON blob) - {"combat.sword": 50, "craft.blacksmith": 30, ...}

Query: Load all tags on login, cache in memory
Update: Serialize changes, save entire blob

Pros: Single query for all tags
Cons: Entire blob must deserialize/serialize
```

- **Completeness:** 4/5 - JSON structure needed
- **Game Fit:** 3/5 - Doesn't scale well
- **Implementation:** 2/5 - JSON column

**Rationale:** Simpler for small tag counts, problematic at scale.

#### Option C: Hybrid Approach

```
Hot tags (last 100 actions): In-memory cache
Warm tags (affinity > 10): Sparse DB storage
Cold tags (affinity <= 10): Not stored (recalculated if needed)

Player.TagCache = Dictionary<string, int> (runtime)
DB: Only stores tags that changed since last save

Combines performance + persistence
```

- **Completeness:** 4/5 - Needs cache invalidation rules
- **Game Fit:** 5/5 - Optimal performance
- **Implementation:** 4/5 - Cache + DB sync

**Rationale:** Best of both worlds, more complex.

---

### 8.4 Action History Storage

**Question:** How is the action history (rolling 1000) managed? Ring buffer? Archival?

**Recommendations:**

#### Option A: Ring Buffer + Archive Summary (Recommended)

```
IN-MEMORY (Recent):
- Circular buffer of 1000 recent actions
- Stored in player session
- Logged out to DB on save

DATABASE (Archived):
- ActionHistorySummary table
- Aggregate stats per day/week/month
- Example: Day 2025-01-15: 50 mining, 30 crafting, 20 combat

DISCARD POLICY:
- When buffer full: Overwrite oldest action
- Weekly: Archive buffer to summary, clear buffer
- Players can query summaries, not individual old actions

RETENTION:
- Detailed: 1000 actions (approximately 1-2 weeks)
- Daily summary: 365 days
- Weekly summary: Forever
```

- **Completeness:** 5/5 - Fully specified
- **Game Fit:** 5/5 - Data for analytics without infinite storage
- **Implementation:** 3/5 - Buffer + aggregation jobs

**Rationale:** Keep useful data, discard noise.

#### Option B: Full Retention with Compression

```
All actions stored indefinitely
Compressed storage (gzip)
Partitioned by month

Queryable: Player can see full history
Data mining: Full dataset available for analytics

Cost: Higher storage, but enables rich features
```

- **Completeness:** 5/5 - Fully specified
- **Game Fit:** 4/5 - More expensive but powerful
- **Implementation:** 2/5 - Append-only log + compression

**Rationale:** Data is valuable - keep it all.

#### Option C: Client-Side Storage

```
Recent 100 actions: Server-side (for sync)
Full history: Client-side device storage (SQLite)
Cross-device: Only recent history syncs

Privacy: Players own their full history
Cost: Offloaded storage to client devices
```

- **Completeness:** 5/5 - Fully specified
- **Game Fit:** 3/5 - Cross-device limitations
- **Implementation:** 4/5 - Sync protocol + local DB

**Rationale:** Privacy-first, reduces server costs.

---

## 10. Performance & Testing

### 9.1 CI/CD Pipeline Structure

**Question:** What is the CI/CD pipeline structure?

**Recommendations:**

#### Option A: Multi-Stage GitHub Actions (Recommended)

```
STAGE 1: PR CHECK (on every pull request)
- Lint: Format validation, markdown lint, C# format
- Unit tests: Fast subset (< 5 minutes)
- Build verification: Ensure code compiles
- Result: Pass/fail, blocks merge if fail

STAGE 2: NIGHTLY BUILD (every night at 2am UTC)
- Full test suite: Unit + integration
- Code coverage: Generate report, block if < 80%
- Security scan: Vulnerability check
- Performance baseline: Benchmark regression check
- Result: Email to dev team, create issue if fail

STAGE 3: WEEKLY RELEASE CANDIDATE (every Friday)
- All nightly checks +
- E2E tests: Full game flow simulation
- Load testing: Simulate 1000 concurrent players
- Deployment to staging environment
- Result: Staging URL for QA team

STAGE 4: PRODUCTION DEPLOY (manual trigger after QA approval)
- Smoke tests on production
- Canary deployment: 10% of traffic first
- Monitor: Error rates, latency
- Full rollout or rollback based on metrics
```

- **Completeness:** 5/5 - Fully specified
- **Game Fit:** 5/5 - Industry best practices
- **Implementation:** 3/5 - GitHub Actions workflows

**Rationale:** Progressive quality gates, fast feedback.

#### Option B: Simple Pipeline

```
On Push:
- Run all tests
- If pass: Deploy to production

No staging, no canary, no nightly builds
Faster deployment, higher risk
```

- **Completeness:** 4/5 - Less comprehensive
- **Game Fit:** 3/5 - Risky for multiplayer
- **Implementation:** 1/5 - Single workflow

**Rationale:** MVP approach, suitable for early development.

---

### 9.2 BDD Test Organization

**Question:** How are BDD tests organized by feature area?

**Recommendations:**

#### Option A: Feature Directory Structure (Recommended)

```
/tests/acceptance
  /ClassSystem
    ClassAcquisition.feature
    ClassEvolution.feature
    ClassSwitching.feature
  /SkillSystem
    SkillLearning.feature
    SkillProgression.feature
    SkillUsage.feature
  /TagSystem
    TagAcquisition.feature
    TagDecay.feature
    TagRequirements.feature
  /Economy
    ResourceGathering.feature
    Crafting.feature
    Trading.feature

Each .feature file:
- Feature description
- Scenarios with Given/When/Then
- Background context setup
- Examples table for data-driven tests
```

- **Completeness:** 5/5 - Fully specified
- **Game Fit:** 5/5 - Mirrors game architecture
- **Implementation:** 2/5 - Directory structure

**Rationale:** Tests organized same way as code is organized.

#### Option B: User Journey Organization

```
/tests/acceptance
  /NewPlayerExperience
    First30Minutes.feature
    FirstClassAcquisition.feature
    FirstSkillLearned.feature
  /MidGame
    ClassEvolution.feature
    CraftingMastery.feature
    SocialFeatures.feature
  /EndGame
    LegendaryProgression.feature
    WorldEvents.feature

Organized by player progression, not game systems
```

- **Completeness:** 5/5 - Fully specified
- **Game Fit:** 4/5 - User-focused
- **Implementation:** 2/5 - Alternative structure

**Rationale:** Tests validate complete experiences.

---

### 9.3 Test Coverage Target

**Question:** What is the test coverage target?

**Recommendations:**

#### Option A: 80% Line Coverage Minimum (Recommended)

```
COVERAGE TARGETS:
- Overall: 80% line coverage
- Critical paths (economy, progression): 90%+
- UI code: 70% (harder to test)
- Data models: 95%+ (easy to test, critical)

MEASUREMENT:
- Tool: Coverlet for .NET
- Report: Generated in CI, must pass thresholds
- Enforced: PR checks fail if below target

QUALITY GATES:
- New code: Must have tests before merge
- Bug fixes: Must add regression test
- Features: Must have BDD scenarios
```

- **Completeness:** 5/5 - Fully specified
- **Game Fit:** 5/5 - Industry standard
- **Implementation:** 2/5 - Coverage tool configuration

**Rationale:** 80% is sweet spot - achievable without over-testing.

#### Option B: Tiered Coverage

```
Core systems: 95% coverage
Game mechanics: 85% coverage
UI/UX: 60% coverage
Infrastructure: 90% coverage

Different expectations for different risk areas
```

- **Completeness:** 5/5 - Fully specified
- **Game Fit:** 5/5 - Risk-based approach
- **Implementation:** 3/5 - Per-directory targets

**Rationale:** Focus testing effort where failure costs are highest.

---

### 9.4 Test Data Management

**Question:** How is test data managed and seeded?

**Recommendations:**

#### Option A: Bogus + Snapshots (Recommended)

````
FAKER GENERATORS:
- PlayerData: Bogus.Player()
- NPCData: Bogus.NPC() with personality faker
- WorldData: Bogus.World() with location generator
- EconomyData: Bogus.Economy() with price curves

EXAMPLE:
```csharp
var testPlayer = PlayerFaker.Generate()
    .WithLevel(20)
    .WithTag("combat.sword", 65)
    .WithClass("Warrior")
    .WithSkill("Sword Mastery", 15);
````

SNAPSHOTS:

- Verify: Snapshot testing for complex outputs
- Stored: /tests/snapshots/
- Updated: Only when intentional change detected

SEEDING:

- Deterministic RNG seed for reproducible tests
- Each test has known data setup
- No external dependencies (database replaced with fakes)

```
- **Completeness:** 5/5 - Fully specified
- **Game Fit:** 5/5 - Fast, reliable tests
- **Implementation:** 3/5 - Faker + Verify setup

**Rationale:** Fake data is faster, more reliable than DB seeds.

#### Option B: Database Fixtures
```

Test database with predefined data sets

- Fixtures in /tests/fixtures/\*.sql
- Each test loads required fixture
- Transactions rolled back after test

Pros: Real database behavior
Cons: Slower, setup complexity

```
- **Completeness:** 4/5 - Needs fixture schemas
- **Game Fit:** 4/5 - More realistic
- **Implementation:** 3/5 - Fixture management

**Rationale:** True integration testing at cost of speed.

---

# LOW PRIORITY (Can be Deferred)

These questions are important but not blocking for prototype/alpha. Can be addressed during beta or post-launch.

## 11. Social & Multiplayer Features

### 10.1 PvP System

**Question:** Is there PvP? If so, how does it work?

**Recommendations:**

#### Option A: No PvP (Recommended for Launch)
```

Focus on cooperative, social gameplay
Player interaction: Trading, guilds, shared events
No combat between players

Rationale:

- Cozy idle game, PvP doesn't fit theme
- Development complexity: Balance PvP + PvE is huge undertaking
- Community: More welcoming to casual players without PvP
- Can add later as expansion if demand exists

```
- **Completeness:** 5/5 - Fully specified (as a decision)
- **Game Fit:** 5/5 - Fits cozy theme
- **Implementation:** 1/5 - No implementation needed

**Rationale:** Focus on core strengths, add PvP later if needed.

#### Option B: Consensual Dueling
```

No open-world PvP
Players can challenge each other to duels:

- Both players must accept
- No loot/rewards (just bragging rights)
- Can decline without penalty
- Optional feature

Keeps social aspect without toxicity

```
- **Completeness:** 5/5 - Fully specified
- **Game Fit:** 4/5 - Low-risk PvP
- **Implementation:** 3/5 - Challenge system

**Rationale:** Players who want PvP can have it, opt-in only.

---

### 10.2 Player Trading

**Question:** How does player trading work? Auction house? Direct trade?

**Recommendations:**

#### Option A: Async Auction House (Recommended)
```

FEATURES:

- Players list items for sale
- Set price or accept bids
- Auctions last 24-48 hours
- 5% listing fee (returned if item sells)
- 5% sale fee (taken from final price)

NO DIRECT TRADING:

- Prevents scamming
- Prevents RMT (real money trading) abuse
- Simpler to implement and moderate

UI:

- Search by item, category, price
- Sort by price, time remaining
- "Buy Now" price or bid system

```
- **Completeness:** 5/5 - Fully specified
- **Game Fit:** 5/5 - Classic MMO feature
- **Implementation:** 4/5 - Auction system + UI

**Rationale:** Auction houses are proven, scalable, less problematic than direct trading.

#### Option B: Mail-Based Trading
```

Players send items via in-game mail

- Attach items to mail
- Set price (gold or premium currency)
- Recipient accepts or declines
- Cooldown: 5 trades per day per player

Simpler than auction house
More restrictive to prevent abuse

```
- **Completeness:** 5/5 - Fully specified
- **Game Fit:** 4/5 - More intimate trading
- **Implementation:** 3/5 - Mail system

**Rationale:** Lower development cost, sufficient for alpha.

---

### 10.3 Guild-Exclusive Class Evolutions

**Question:** How do guild-exclusive class evolutions work? Requirements, restrictions?

**Recommendations:**

#### Option A: Deferred to Post-Launch (Recommended)
```

No guild-exclusive classes at launch
Focus on: Individual progression, core class system
Guilds: Social, shared activities, cosmetic benefits

Can add in expansion:

- "Guild Alliance" expansion
- "Faction Wars" update
- Introduces guild-based content and classes

Rationale: Guild content is entire feature suite, defer to focus on core

```
- **Completeness:** 5/5 - Fully specified (as deferral)
- **Game Fit:** 5/5 - Manageable scope
- **Implementation:** 1/5 - No implementation

**Rationale:** Launch with solid core, expand later.

#### Option B: Simple Guild Bonus (If implementing at launch)
```

Guild Level unlocks bonuses:

- Guild level 5: +5% XP for all members
- Guild level 10: Guild hall access
- Guild level 15: Shared storage
- Guild level 20: Guild-exclusive class evolution

Requirements:

- 50 members in guild
- Guild cumulative level: 1000 (sum of all member levels)
- Guild leader chooses evolution path
- All members can unlock (if they meet individual requirements)

```
- **Completeness:** 5/5 - Fully specified
- **Game Fit:** 4/5 - Encourages large guilds
- **Implementation:** 4/5 - Guild progression system

**Rationale:** If launching with guilds, give them meaningful progression.

---

## 12. Monetization Specifics

### 11.1 Premium Currency Name & Rate

**Question:** What is the premium currency name and earn rate?

**Recommendations:**

#### Option A: "Gems" with Slow Free Earn (Recommended)
```

CURRENCY NAME: Gems

FREE EARNING:

- Daily login: 5 gems
- Achievement: 10-100 gems (based on difficulty)
- Level-up: 1 gem per level (level 20 = 20 gems)
- Weekly event: 25 gems

Expected free rate: ~50 gems/week for active player

PURCHASE (USD):

- 100 gems: $0.99
- 500 gems: $4.99
- 1000 gems: $9.99
- 5000 gems: $39.99

PRICING (gems):

- Rest skip: 50 gems
- Cosmetic outfit: 500 gems
- Extra class slot: 1000 gems
- Character rename: 100 gems

```
- **Completeness:** 5/5 - Fully specified
- **Game Fit:** 5/5 - Industry standard
- **Implementation:** 2/5 - Currency system

**Rationale:** "Gems" is universally understood, pricing is competitive.

#### Option B: "Crystals" - More Premium Feel
```

CURRENCY NAME: Crystals
Higher value perception than "gems"

Free earn: ~30 crystals/week (slower)
Pricing: Same USD amounts, fewer crystals per dollar

Creates higher perceived value, more premium positioning

```
- **Completeness:** 5/5 - Fully specified
- **Game Fit:** 4/5 - More premium positioning
- **Implementation:** 2/5 - Currency system

**Rationale:** Differentiation from competitors.

---

### 11.2 Exact Prices for Purchases

**Question:** What are the exact prices for allowed purchases? Cosmetics, slots, etc.

**Recommendations:**

#### Option A: Price Tiers (Recommended)
```

COSMETICS (permanent):

- Outfit: 500 gems ($5)
- Weapon skin: 300 gems ($3)
- Pet: 400 gems ($4)
- Emote pack: 200 gems ($2)
- Mount: 800 gems ($8)

CONVENIENCE (permanent unlocks):

- Extra class slot: 1000 gems ($10)
- Extra storage: 500 gems ($5)
- Auto-loot: 1500 gems ($15)

CONSUMABLES:

- Rest skip (24h): 50 gems ($0.50)
- XP boost (24h, 2×): 100 gems ($1)
- Respec: 200 gems ($2)

LIMITED:

- Monthly pass: 2000 gems ($20) - exclusive rewards over 30 days
- Battle pass: 1500 gems ($15) - 100 tiers, 90 days

IMPACT ANALYSIS:

- Free player: Earn enough for 1-2 rest skips/week
- Whales: Can spend $100+ per month if desired
- Ethical: No pay-to-win, only convenience and cosmetics

```
- **Completeness:** 5/5 - Fully specified
- **Game Fit:** 5/5 - Balanced F2P + monetization
- **Implementation:** 2/5 - Pricing table

**Rationale:** Clear value proposition, ethical monetization.

---

### 11.3 Battle Pass / Seasonal Content

**Question:** Is there a battle pass or seasonal content?

**Recommendations:**

#### Option A: Quarterly Season Pass (Recommended)
```

STRUCTURE:

- 90-day seasons (4 per year)
- Each season has theme: "Dragon Season", "Frost Festival", etc.
- 100 reward tiers per season

FREE TIERS (30):

- Basic resources
- Common cosmetics
- Some gems

PREMIUM TIERS (70):

- Unlock for 1500 gems ($15)
- Exclusive cosmetics
- Premium currency bonuses
- Unique items

PRESTIGE TIERS (optional extra):

- 25 bonus tiers for 500 gems ($5)
- Only available after completing premium track
- Highest rarity rewards

SEASONAL CONTENT:

- Special events each season
- Limited-time NPCs
- Seasonal recipes/spells
- Cosmetic overrides for existing content

RETENTION:

- Free track encourages engagement
- Premium generates revenue
- Seasons create FOMO (fear of missing out)
- Older season returns occasionally as "throwback" events

```
- **Completeness:** 5/5 - Fully specified
- **Game Fit:** 5/5 - Industry standard retention mechanic
- **Implementation:** 4/5 - Season system + reward logic

**Rationale:** Battle passes are proven retention drivers.

#### Option B: No Battle Pass (Alternative)
```

Focus on: Core gameplay, not seasonal FOMO
Monetization through: Direct purchases, cosmetics
Seasonal events: Free for all players, no paywall

Less aggressive monetization, more casual-friendly

```
- **Completeness:** 5/5 - Fully specified
- **Game Fit:** 4/5 - Less pressure on players
- **Implementation:** 2/5 - Event system only

**Rationale:** Aligns with cozy, casual theme.

---

### 11.4 Limited Rest Cycle Skips

**Question:** How do "limited rest cycle skips" work? Daily limit? Diminishing effect?

**Recommendations:**

#### Option A: Daily Limit with Weekly Reset (Recommended)
```

MECHANIC:

- Premium currency can skip rest waiting period
- Instantly apply rested bonus instead of waiting

LIMITS:

- Free: 1 skip per day
- Premium: 3 skips per day
- Weekly cap: 15 skips total (prevents hoarding)

COST:

- 1st skip: 25 gems (half price)
- 2nd skip: 50 gems (full price)
- 3rd skip: 100 gems (double price - diminishing returns)

ALTERNATIVE: Skip Pass

- 500 gems: 7-day pass (unlimited skips for week)
- Encourages active play periods

RATIONALE:

- Prevents P2W (limited skips)
- Convenience, not infinite power
- Daily engagement encouraged

```
- **Completeness:** 5/5 - Fully specified
- **Game Fit:** 5/5 - Balanced convenience
- **Implementation:** 3/5 - Limit tracking + pricing

**Rationale:** Players can pay for convenience, not dominance.

---

## 13. Art & Asset Pipeline

### 12.1 Cozy Fantasy Art Style Definition

**Question:** What art style will be used? Specific definition needed.

**Recommendations:**

#### Option A: Stylized 2D Vector (Recommended)
```

STYLE REFERENCE:

- Stardew Valley (pixel art) but vector scalable
- Cozy Grove: Warm colors, soft shapes
- Infinity Nikki: Chibi proportions, expressive

COLOR PALETTE:

- Warm primaries: Coral, Sunny Yellow, Sky Blue
- Soft backgrounds: Cream, Peach, Lavender
- Nature tones: Forest Green, Earth Brown, River Blue

CHARACTER DESIGN:

- Chibi proportions: 2.5 heads tall
- Large eyes, expressive faces
- Soft edges, no sharp angles
- Clothing: Detailed but readable at small sizes

UI STYLE:

- Rounded corners (8px radius)
- Semi-transparent overlays (80% opacity)
- Hand-drawn icons
- Wood/parchment textures for panels

TOOLCHAIN:

- Aseprite for pixel art
- Inkscape for vector scaling
- Pixellab.ai for generation

```
- **Completeness:** 5/5 - Fully specified
- **Game Fit:** 5/5 - Fits cozy theme perfectly
- **Implementation:** 4/5 - Style guide creation

**Rationale:** Cozy games need warm, inviting aesthetics.

---

### 12.2 Target Resolution/DPI

**Question:** What is the target resolution/DPI for assets?

**Recommendations:**

#### Option A: Multi-Resolution Asset System (Recommended)
```

BASE RESOLUTION: 1920×1080 (1080p)

ASSET DENSITY:

- UI Elements: 2× density (@2x) for retina displays
- Character sprites: 64×64 base (scales to 128×128, 256×256)
- Item icons: 32×32 base (scales to 64×64)
- Environment tiles: 128×128 base

EXPORT FORMAT:

- Vector source: SVG
- Raster export: PNG with transparency
- Compression: TinyPNG for optimization
- Spritesheets: TexturePacker for performance

DEVICE TARGETS:

- Desktop: 1080p native, up to 4K
- Tablet: 2048×1536 (iPad Pro)
- Mobile: 1080×1920 (portrait), 1920×1080 (landscape)

SCALING STRATEGY:

- Vector assets scale infinitely
- Raster assets: 3 versions (1×, 2×, 4×)
- Runtime: Select based on device DPI

```
- **Completeness:** 5/5 - Fully specified
- **Game Fit:** 5/5 - Modern multi-platform approach
- **Implementation:** 3/5 - Asset pipeline setup

**Rationale:** Future-proof, supports all target devices.

---

### 12.3 NPC Portrait Variety Management

**Question:** How will NPC portrait variety be managed?

**Recommendations:**

#### Option A: Procedural Assembly System (Recommended)
```

COMPONENT-BASED GENERATION:

Base Layers (50 options each):

- Face shape: 50 variants
- Eyes: 50 variants
- Nose: 50 variants
- Mouth: 50 variants
- Hair: 100 variants (50 male, 50 female)
- Accessories: 30 variants

COLOR VARIATIONS:

- Skin: 20 tones
- Hair: 30 colors
- Eyes: 15 colors

COMBINATIONS: 50^4 × 100 × 50 × 30 × 20 × 30 × 15 = 50,625,000,000,000 unique NPCs

GENERATION PIPELINE:

- Pixellab.ai prompts for base components
- Manual curation of quality
- Procedural assembly at runtime
- Cache generated portraits (don't regenerate)

UNIQUE HEROES/VILLAINS:

- Hand-crafted, not procedural
- 100 unique portraits for launch
- Used for named NPCs, quest givers

```
- **Completeness:** 5/5 - Fully specified
- **Game Fit:** 5/5 - Infinite variety, finite storage
- **Implementation:** 4/5 - Generation + assembly system

**Rationale:** Scalable to thousands of NPCs without manual art.

---

### 12.4 Darker Elements in Cozy Style

**Question:** How do "darker" elements (villains, dungeons, red classes) fit the cozy style?

**Recommendations:**

#### Option A: "Ghibli Approach" - Scary but Not Terrifying (Recommended)
```

PRINCIPLE: Dark elements are serious/threatening, not horror

VILLAINS:

- Menacing presence, not gory
- Sharp angles vs soft heroes (contrast)
- Cool colors (purples, dark blues) vs warm heroes
- Still readable, clearly evil but not nightmare material

DUNGEONS:

- Dim lighting, but not pitch black
- Spooky atmosphere (cobwebs, ancient stone)
- Monsters: Imperfect but not disgusting
- Example: Studio Ghibli villains (memorable but not traumatizing)

RED CLASSES (Assassin, Necromancer):

- Edgy but not grotesque
- Dark humor, not grim darkness
- Powers look cool/flashy, not visceral
- Example: "Team Rocket" vibe - clearly villains but cartoony

COLOR PALETTE FOR DARK:

- Shadows: Deep indigo, not pure black
- Accents: Magenta, not blood red
- Magic effects: Purple, teal, emerald (not gore colors)

````
- **Completeness:** 5/5 - Fully specified
- **Game Fit:** 5/5 - Maintains tone while including conflict
- **Implementation:** 3/5 - Style guide + examples

**Rationale:** Conflict requires antagonists, but keep it family-friendly.

---

# Implementation Priority Summary

## Immediate (Required for Prototype - 2-3 weeks)

1. **Core Formulas**:
   - XP curve formula (Option A: Classical MMORPG)
   - Tag decay formula (Option A: Weekly compound with floor)
   - Tag XP per action (Option A: Linear scaling)

2. **Class System**:
   - Dormant class handling (Option A: Persistent, reactivatable)
   - Evolution presentation (Option A: Present qualified options)

3. **Skill System**:
   - Active skill limit (Option A: 8 active slots)
   - Skill respec (Option A: Free forget)

4. **Economy**:
   - Resource sinks (Option A: Triple sink system)
   - Resource-to-XP rate (Option A: Value-based)

## Short-term (Required for Alpha - 1-2 months)

5. **NPC Core**:
   - Goal re-evaluation (Option A: Time + event triggers)
   - Goal limits (Option A: Timeframe categories)
   - Conflict resolution (Option A: Priority + time decay)

6. **Content Minimums**:
   - Recipe counts (Option A: 60 recipes prototype)
   - Spell counts (Option A: 15 spells per school)
   - Discovery rate (Option A: Hybrid system)

7. **UI/UX**:
   - Action bar algorithm (Option A: Weighted score)
   - Notification strategy (Option A: Smart filtering)

8. **Technical**:
   - Offline simulation cap (Option A: 48-hour cap)
   - Offline combat (Option A: Statistical resolution)
   - Tag storage (Option A: Sparse storage)

## Medium-term (Required for Beta - 3-4 months)

9. **Social**:
   - Trading (Option A: Auction house)
   - PvP decision (Option A: No PvP at launch)

10. **Monetization**:
    - Premium currency (Option A: Gems)
    - Pricing (Option A: Price tiers)
    - Battle pass (Option A: Quarterly seasons)

11. **Art**:
    - Style definition (Option A: Stylized 2D vector)
    - Asset pipeline (Option A: Multi-resolution)
    - Portrait system (Option A: Procedural assembly)

## Deferred (Post-Launch)

12. **Advanced Features**:
    - Guild-exclusive classes (defer to expansion)
    - Settlement building
    - Territory control
    - Advanced social features

---

# Decision Log Template

For each resolved question, document:

```markdown
## [Question Name]

**Decision**: [Option X - Title]
**Date**: YYYY-MM-DD
**Decided By**: [Name/Role]
**Rationale**: [Why this option]

Implementation Notes:
- [Technical details]
- [Dependencies]
- [Risks]

Validation:
- [How will we verify this works]
- [Success metrics]
````

---

_Document Version 2.0 - 111 Questions with 266 Specific Recommendations_

**Total Recommendations**: 266
**Critical Recommendations**: 36 (must resolve for prototype)
**High Priority Recommendations**: 70 (blocking alpha features)
**Medium Priority Recommendations**: 108 (polish for beta)
**Low Priority Recommendations**: 52 (post-launch features)

**Next Step**: Schedule design review meeting to vote on critical priority questions (sections 1-3).
