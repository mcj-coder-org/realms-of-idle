---
type: system
scope: detailed
status: authoritative
version: 1.0.0
created: 2026-02-03
updated: 2026-02-03
subjects: [npc, goals, ai, behavior, visibility, failure-states]
dependencies:
  [core-progression-system-gdd.md, class-system-gdd.md, skill-recipe-system-gdd.md, npc-design.md]
---

# NPC Core Systems - Authoritative Game Design

## Executive Summary

The NPC Core Systems govern how non-player characters think, plan, and behave through five foundational mechanics: goal re-evaluation triggers, concurrent goal limits, conflict resolution, secret goals, and failure state classification. This system builds upon the unified character model established in `npc-design.md`, providing the specific algorithms and rules that make NPCs feel alive while remaining computationally feasible for a massively multiplayer idle RPG.

**This document resolves:**

- 5 HIGH priority questions from open-questions.md
- Goal re-evaluation timing and triggers
- Goal capacity management with survival override
- Conflicting goal arbitration with personality expression
- Secret goal visibility tiers and information disclosure
- Goal failure vs abandonment state machine

**Design Philosophy:** NPCs are characters, not props. They use the same progression systems as players but are distinguished by their goals and decision-making. The system balances stability (predictable NPC behavior) with reactivity (responsive to world events) while providing depth through personality-based variation and secret mechanics for villains.

---

## 1. Goal Re-evaluation Triggers

### 1.1 Re-evaluation Schedule

NPCs reconsider their goals through two complementary systems:

```
REGULAR SCHEDULED REVIEW:
  Frequency: Weekly (Sunday 00:00 UTC)
  Scope: All active goals
  Purpose: Check progress, adjust priorities, generate new goals
  Duration: ~1 second per NPC (batch processed)

IMMEDIATE TRIGGERS (async):
  When these events occur, NPC re-evaluates immediately:
    ✓ Major goal completed (all subgoals done)
    ✓ Major goal failed (permanent blocker)
    ✓ Major goal abandoned (NPC choice or timeout)
    ✓ Player interaction significantly changes context
    ✓ World event affects NPC's goal area
    ✓ Life-threatening event (survival override)

  Rate Limit: Max 1 re-evaluation per NPC per day
    Prevents thrashing from rapid successive events
    Queue additional triggers for next day
```

### 1.2 Re-evaluation Algorithm

```
NPC GOAL RE-EVALUATION
━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━

Input: Current state, active goals, world events, triggers
Output: Updated goal set, new goals, goal state changes

STEP 1: Check for Immediate Triggers
  IF goal_completed/failed/abandoned:
    → Generate replacement goal(s)
    → Update mood and satisfaction
    → Check for archetype shift (maintainer ↔ ambitious)

  IF world_event impacts NPC:
    → Assess impact on active goals
    → Generate reactive goals if needed
    → Update priorities

  IF player_interaction significant:
    → Evaluate relationship impact
    → Generate new goals (requests, revenge, gratitude)

  IF last_re-eval > 24 hours ago:
    → Process to Step 2

STEP 2: Weekly Scheduled Review (if Sunday)
  FOR each active goal:
    → Calculate progress rate
    → Check if blocked > 4 weeks (auto-abandon/fail)
    → Adjust priority based on age and urgency
    → Update satisfaction based on progress

  FOR each goal category:
    → Check category limits (see §2)
    → Mark lowest-priority goals for potential removal

  Generate new goals based on:
    → Archetype weights (maintainer/hero/villain)
    → Current life circumstances
    → Personality and values
    → Available opportunities

STEP 3: Goal State Updates
  BLOCKED goals:
    → Check if block resolved
    → If 4+ weeks blocked: Mark for abandon/fail

  All goals:
    → Update progress percentages
    → Check milestone completion
    → Trigger success effects if complete

STEP 4: Update NPC State
  → satisfaction: Weighted average of goal progress
  → stress: Increases if goals blocked/failed
  → world_model: Update based on goal outcomes

STEP 5: Generate Daily Loop
  → New goal set → regenerate activity loop
  → See npc-design.md §4 for loop generation
```

### 1.3 Event-Driven Examples

```
EXAMPLE: Blacksmith Tomas Re-evaluates
━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━

Initial State:
  Goals:
    - "Forge customer sword" (priority 70, active, 80% complete)
    - "Learn enchanting" (priority 60, active, 35% complete)
    - "Save 500 gold" (priority 50, active, 40% complete)
  Satisfaction: 0.72
  Last re-eval: 6 days ago

EVENT: Enchanter Mira disappears (world event)

Immediate Trigger Activated:
  Tomas re-evaluates (last was 6 days ago, within rate limit)

Assessment:
  Goal "Learn enchanting" - NOW BLOCKED
    → Mira was the only enchanter in region
    → Cannot progress without teacher
    → Mark as BLOCKED, add block reason
    → -5 mood (frustration)

  Generate reactive goals:
    Option A: "Find out what happened to Mira" (investigation)
    Option B: "Seek new enchanting teacher" (travel)
    Option C: "Abandon enchanting, focus on smithing" (give up)

  Personality check (Tomas: Deliberative, patient):
    → Chooses Option A (loyal, patient)
    → New goal created: "Investigate Mira's disappearance"

  New goal state:
    - "Forge customer sword" (priority 70, active, 80% complete)
    - "Learn enchanting" (priority 60, BLOCKED, 35% complete)
    - "Save 500 gold" (priority 50, active, 40% complete)
    - "Investigate Mira" (priority 75, active, 0% complete) ← NEW

  Satisfaction recalculated: 0.68 (down from 0.72, blocked goal)

Future Outcome Possibilities:
  Week 4: Mira still missing
    → "Learn enchanting" auto-abandoned after 4 weeks blocked
    → Mood: -10 (sadness) for 3 days
    → May generate "Accept loss, move on" goal

  Week 2: Player reveals Mira's location
    → Player interaction trigger
    → "Learn enchanting" unblocked
    → Mood recovers
    → +10 relationship with player
```

### 1.4 Rate Limiting Implementation

```
Rate Limit Data Structure:
{
  npc_id: "npc_tomas_ironhand",
  last_re_evaluation: "2026-02-01T08:00:00Z",
  re_eval_count_today: 1,
  pending_triggers: []  // Queue if exceeded daily limit
}

Rate Limit Rules:
  MAX_DAILY_RE_EVALS = 1

  IF (current_time - last_re_evaluation) > 24 hours:
    count = 0  // Reset daily counter

  IF count < MAX_DAILY_RE_EVALS:
    Execute re-evaluation immediately
    count++
    last_re_evaluation = current_time
  ELSE:
    Add trigger to pending_triggers
    Process tomorrow at scheduled review

Rationale:
  Prevents NPC behavior thrashing
  Ensures stability (players can learn NPC patterns)
  Maintains responsiveness (important triggers still processed within 24h)
```

---

## 2. Concurrent Goal Limits

### 2.1 Goal Hierarchy with Survival Override

NPCs can pursue multiple goals simultaneously but are constrained by both capacity limits and survival needs:

```
GOAL HIERARCHY
━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━

PRIORITY 0: Survival & Immediate Needs (OVERRIDE ALL)

  Trigger Conditions:
    → Hunger < 20%: "Eat immediately"
    → Exhaustion < 15%: "Rest immediately"
    → Life-threatening danger: "Flee or fight immediately"
    → Environmental hazard: "Seek shelter immediately"

  Behavior:
    → INTERRUPT any current goal activity
    → Execute survival action immediately
    → After survival resolved: Resume goal work
    → Survival actions consume minimal time (eating: 15 min, rest: scheduled)

  Examples:
    * Tomas forging sword → Hunger trigger → Pauses forging → Eats → Resumes
    * Elena studying magic → Exhaustion trigger → Stops studying → Sleeps → Continues tomorrow

PRIORITY 1: Voluntary Sacrifice (SPECIAL GOAL TYPE)

  Trigger Conditions:
    → Protecting loved ones (NPCs under attack)
    → Defending settlement/party (protection goal active)
    → Ideology/vengeance (dying for beliefs, martyrdom)
    → High-stakes completion (boss <10% HP, legendary quest final step)

  NPC Decision:
    → Can CHOOSE to ignore survival override
    → Requires: High stress (>0.7) OR strong conviction (values >0.8)
    → Risk: Death is possible (HP reaches 0 = permanent NPC death)

  Reward if SUCCESS:
    → ⚡ LEGENDARY ACHIEVEMENT unlocked
    → +35-50% bonus to higher skill tiers
    → Guaranteed Rare+ skill offer at next level up
    → Memorable story moment ("watercooler talk")

  Examples:
    * Tomas defends shop from bandits, refuses to flee (conviction)
      → Survives with 1 HP against 5 enemies
      → Unlocks ⚔️ [Last Stand] achievement
      → +35% skill rarity bonus
      → Offered [Guardian Angel] skill at next level up

    * Guardsman holds bridge against invading army
      → Dies protecting 50 civilians escape
      → Posthumous: ⚔️ [Martyr's Sacrifice] achievement
      → Statue erected in town square
      → All survivors get +20 relationship with guards faction

PRIORITY 2: Timeframe-Categorized Goals (8 MAX)

  Immediate Goals (0-1 day): Max 2
    Short-term Goals (1-7 days): Max 3
    Medium-term Goals (1-4 weeks): Max 2
    Long-term Goals (1+ months): Max 1

  TOTAL: 8 concurrent goals maximum

  Slot Full Behavior:
    → Cannot generate new goal in full category
    → Must abandon/complete existing goal first
    → Exception: Survival override always possible
```

### 2.2 Timeframe Category Definitions

```
IMMEDIATE GOALS (0-1 day, Max 2):
  Purpose: Urgent needs and opportunities
  Examples:
    - "Buy food today" (hunger deadline)
    - "Talk to blacksmith about sword" (customer waiting)
    - "Attend festival tonight" (time-limited event)

  Characteristics:
    - Clear deadline (specific time/day)
    - High priority (typically 70-100)
    - Fails if deadline missed
    - Fast progress (completable in <1 day)

SHORT-TERM GOALS (1-7 days, Max 3):
  Purpose: Current projects and week-scale objectives
  Examples:
    - "Learn mining basics" (skill acquisition)
    - "Save 50 gold" (economic)
    - "Visit capital city" (exploration)

  Characteristics:
    - Flexible deadlines (within week)
    - Medium priority (typically 40-80)
    - Multiple subgoals
    - Moderate progress rate

MEDIUM-TERM GOALS (1-4 weeks, Max 2):
  Purpose: Ongoing projects and month-scale ambitions
  Examples:
    - "Build friendship with Maria" (social relationship)
    - "Reach [Blacksmith] level 30" (class progression)
    - "Expand shop size" (construction)

  Characteristics:
    - Extended timelines
    - Low-medium priority (30-70)
    - Complex goal chains
    - Slow progress (requires many actions)

LONG-TERM GOALS (1+ months, Max 1):
  Purpose: Life aspirations and character-defining objectives
  Examples:
    - "Become master craftsman" (lifetime achievement)
    - "Earn enough to retire" (economic security)
    - "Avenge my brother's death" (vengeance)

  Characteristics:
    - Very extended timelines (months to years)
    - Variable priority (10-100 based on NPC)
    - Decomposes into medium/short-term goals
    - Defines NPC's "story arc"
```

### 2.3 Goal Slot Management

```
GOAL SLOT SYSTEM
━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━

NPC Goal State:
{
  npc_id: "npc_tomas_ironhand",

  goals: {
    immediate: [
      { id: "goal_eat_lunch", priority: 90, deadline: "12:00 today" },
      { id: "goal_finish_sword", priority: 75, deadline: "17:00 today" }
    ],  // 2/2 slots full

    short_term: [
      { id: "goal_learn_mining", priority: 60, deadline: "7 days" },
      { id: "goal_save_50_gold", priority: 50, deadline: "5 days" },
      { id: "goal_visit_mira", priority: 70, deadline: "3 days" }
    ],  // 3/3 slots full

    medium_term: [
      { id: "goal_befriend_maria", priority: 55, deadline: "3 weeks" },
      { id: "goal_reach_bs_30", priority: 65, deadline: "4 weeks" }
    ],  // 2/2 slots full

    long_term: [
      { id: "goal_master_craftsman", priority: 80, deadline: "2 years" }
    ]  // 1/1 slot full
  },

  total_active_goals: 8,
  survival_override: null  // Not currently active
}

New Goal Generation:
  IF target_category has open slot:
    → Add goal immediately

  IF target_category is FULL:
    → Compare new goal priority vs lowest existing in category
    → IF new priority > existing priority + 15:
      Abandon lowest priority goal
      Add new goal
    → ELSE:
      Queue goal for reconsideration at weekly review
      May be activated when slot opens

Survival Override:
  survival_active = true blocks all goal actions
  Survival action executes immediately
  After survival: survival_active = false, resume goals
```

### 2.4 Voluntary Sacrifice Mechanics

```
SACRIFICE DECISION SYSTEM
━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━

Trigger Conditions Check:
  FOR each active goal:
    IF goal.type == "protection" AND target_imminent_danger:
      sacrifice_opportunity = true
    IF goal.type == "ideology" AND belief_challenged:
      sacrifice_opportunity = true
    IF goal.type == "high_stakes_completion" AND close_to_success:
      sacrifice_opportunity = true

Sacrifice Eligibility:
  IF NPC.stress > 0.7 OR NPC.conviction > 0.8:
    sacrifice_allowed = true
  ELSE:
    sacrifice_allowed = false  // Must have strong motivation

Player Choice (for important NPCs):
  IF NPC.is_story_critical:
    → Present choice to player
    → "Tomas is risking his life defending his shop. Interfere?"
    → Options: [Help him] [Let him fight] [Warn him to flee]

  Regular NPCs:
    → Automatic decision based on personality/values
    → No player prompt (too many NPCs)

Sacrifice Outcome Calculation:
  base_survival_chance = 0.3 (30% survive by default)

  Modifiers:
    +0.2 if NPC.level > opponent_level
    +0.1 if NPC has appropriate class/gear
    +0.15 if player intervenes
    -0.2 if outnumbered >3:1
    -0.1 if NPC.exhaustion < 30%

  IF sacrifice_attempt AND random() < survival_chance:
    SUCCESS → Legendary Achievement unlocked
  ELSE:
    NPC death (permanent, generate new NPC to replace role)

Achievement Examples:
  ⚔️ Last Stand
    Requirement: Survive with <10% HP against 5+ enemies while protecting ally
    Bonus: +35% to higher skill tiers
    Unlocks: [Guardian Angel] skill

  ⚔️ Against All Odds
    Requirement: Kill enemy 20+ levels higher while at <20% HP
    Bonus: +50% to higher skill tiers, guaranteed Rare+ skill offer
    Unlocks: [Miracle Worker] skill

  🏰 Martyr's Sacrifice
    Requirement: Die protecting settlement (save 10+ NPCs)
    Posthumous: Title granted, NPC memorialized
    Effect: Survivors get +20 relationship boost, morale bonus
```

### 2.5 Example Goal Configuration

```
EXAMPLE: Tomas Ironhand's Goals
━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━

Character Context:
  Class: [Blacksmith - Journeyman] Lv.34
  Archetype: Maintainer
  Personality: Deliberative (low openness, high conscientiousness)
  Values: Family 0.8, Craft 0.7, Reputation 0.5

Active Goals (8/8 slots):

IMMEDIATE (2/2):
  [1] "Complete Duke's sword order"
      Priority: 95 (customer is important)
      Deadline: Today 17:00
      Progress: 85% (almost done)
      Type: economic

  [2] "Eat lunch at tavern"
      Priority: 80 (survival need, not urgent)
      Deadline: Today 13:00
      Progress: 0% (not started)
      Type: survival

SHORT-TERM (3/3):
  [3] "Study enchanting with Mira"
      Priority: 70 (advancement goal)
      Deadline: 7 days
      Progress: 35%
      Type: skill_acquisition

  [4] "Save 100 gold for shop expansion"
      Priority: 50 (economic, flexible)
      Deadline: 30 days
      Progress: 60% (have 60 gold)
      Type: economic

  [5] "Attend town festival on Saturday"
      Priority: 65 (social event)
      Deadline: 3 days
      Progress: 0% (just needs to attend)
      Type: social_relationship

MEDIUM-TERM (2/2):
  [6] "Build friendship with Maria the Healer"
      Priority: 55 (romantic interest)
      Deadline: 4 weeks
      Progress: 40% (trust at 40/100)
      Type: social_relationship

  [7] "Reach [Blacksmith] level 40"
      Priority: 70 (career advancement)
      Deadline: 8 weeks
      Progress: 34/40 (85% there)
      Type: class_evolution

LONG-TERM (1/1):
  [8] "Become Master Smith of the region"
      Priority: 80 (life purpose)
      Deadline: 2 years
      Progress: 15% (long journey ahead)
      Type: reputation

Daily Loop Generated from Goals:
  Morning: Forge Duke's sword (Goal 1)
  Midday: Eat lunch (Goal 2)
  Afternoon: Continue sword OR study enchanting if sword done (Goals 1, 3)
  Evening: Social time at festival if Saturday (Goal 5)
  Night: Sleep (survival)

  Through all actions: Passive progress on Goals 4, 6, 7, 8
```

---

## 3. Conflicting Goal Resolution

### 3.1 Priority + Age + Personality Algorithm

When NPCs must choose between competing goals, a three-stage algorithm determines the winner:

```
CONFLICT RESOLUTION ALGORITHM
━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━

STEP 1: Calculate Base Scores

FOR each conflicting goal:
  base_score = goal.priority

  // Age bonus rewards neglected goals
  age_days = current_date - goal.created_date
  IF age_days >= 15:
    age_bonus = +20
  ELSE IF age_days >= 7:
    age_bonus = +10
  ELSE:
    age_bonus = 0

  total_score = base_score + age_bonus

STEP 2: Compare Scores

score_difference = abs(goal_a.total_score - goal_b.total_score)

IF score_difference > 15:
  → Clear winner: Higher score goal wins
  → No personality influence (strong preference)

ELSE (scores within 15 points):
  → Close competition: Personality tiebreaker applies
  → Proceed to STEP 3

STEP 3: Personality Tiebreaker

Calculate personality modifier based on traits:

IMPULSIVE (high openness >0.7, low conscientiousness <0.4):
  newer_goal_bonus = +15
  → "I'm excited about this new opportunity!"

  Example:
    Goal A (old): Score 70
    Goal B (new): Score 65 + 15 = 80
    → Goal B wins (impulsive preference)

DELIBERATIVE (low openness <0.4, high conscientiousness >0.6):
  older_goal_bonus = +15
  → "I should finish what I started"

  Example:
    Goal A (old): Score 70 + 15 = 85
    Goal B (new): Score 65
    → Goal A dominates (deliberative persistence)

OPPORTUNISTIC (high neuroticism >0.6, low agreeableness <0.4):
  higher_reward_bonus = +20
  → "Which one gives me more gold/power?"

  Example:
    Goal A: Priority 60, Reward: 10 gold
    Goal B: Priority 55, Reward: 50 gold
    Goal B bonus: +20 = 75
    → Goal B wins (greedy choice)

CAUTIOUS (high neuroticism >0.7, low openness <0.3):
  lower_risk_bonus = +15
  → "I don't want to fail, take the safe option"

  Example:
    Goal A: Priority 70, Risk: high (might fail)
    Goal B: Priority 55, Risk: low (guaranteed success)
    Goal B bonus: +15 = 70
    → Goal B wins (safe choice)

FINAL DECISION:
  winner_score = max(score_a, score_b) after personality modifiers
  Execute winner goal, defer loser goal
```

### 3.2 Conflict Resolution Examples

```
EXAMPLE 1: Tomas's Dilemma
━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━

Context: Tomas the Blacksmith, Tuesday afternoon
Conflicting goals for 13:00-17:00 time slot:

Goal A: "Complete Duke's sword order"
  Priority: 95
  Created: 10 days ago
  Age bonus: +10 (7-14 days)
  Base score: 105

Goal B: "Study enchanting with Mira"
  Priority: 70
  Created: 2 days ago
  Age bonus: 0 (<7 days)
  Base score: 70

Step 1: Base scores calculated
  Goal A: 105
  Goal B: 70
  Difference: 35 points (>15)

Step 2: Clear winner
  Goal A wins by 35 points
  No personality tiebreaker needed

Decision: Tomas forges the Duke's sword (customer satisfaction > personal learning)

---

EXAMPLE 2: Elena's Choice (Personality Matters)
━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━

Context: Elena the Baker's Daughter, Friday evening
Personality: Impulsive (openness 0.8, conscientiousness 0.3)
Conflicting goals:

Goal A: "Help father with bakery rush"
  Priority: 70
  Created: 12 days ago
  Age bonus: +10
  Base score: 80

Goal B: "Attend traveling mage's lecture"
  Priority: 65
  Created: 1 day ago
  Age bonus: 0
  Base score: 65

Step 1: Base scores calculated
  Goal A: 80
  Goal B: 65
  Difference: 15 points (at threshold)

Step 2: Close competition
  Within 15 points → Personality tiebreaker applies

Step 3: Personality modifier
  Elena is IMPULSIVE (high openness, low conscientiousness)
  Newer goal bonus: +15

  Goal A: 80
  Goal B: 65 + 15 = 80
  Tie! → Newer goal wins (tiebreaker rule)

Decision: Elena attends the mage lecture
  Result: She discovers magic interest path, father annoyed
  Long-term: This may lead to her becoming [Mage] NPC

If Elena were DELIBERATIVE instead:
  Older goal bonus: +15
  Goal A: 80 + 15 = 95
  Goal B: 65
  → Goal A dominates (dutiful daughter)

Same goals, different personality → different outcomes
```

### 3.3 Data Structure

```
Conflict Resolution Data:
{
  npc_id: "npc_elena_baker",

  personality: {
    openness: 0.8,        // High → Impulsive
    conscientiousness: 0.3,  // Low → Impulsive
    extraversion: 0.5,
    agreeableness: 0.6,
    neuroticism: 0.4
  },

  personality_type: "IMPULSIVE",  // Derived from traits

  conflict_rules: {
    newer_goal_bonus: 15,  // Impulsive modifier
    older_goal_bonus: 0,
    higher_reward_bonus: 0,
    lower_risk_bonus: 0
  },

  active_conflicts: [
    {
      goal_a_id: "goal_help_bakery",
      goal_b_id: "goal_attend_lecture",
      time_slot: "Friday 18:00-20:00",
      resolution: "goal_b_attend_lecture",
      reason: "Impulsive personality (+15 to newer goal)"
    }
  ]
}
```

---

## 4. Secret NPC Goals & Information Disclosure

### 4.1 Goal Visibility Tiers

NPCs have four tiers of goal visibility, creating depth and discovery opportunities for players:

```
GOAL VISIBILITY TIERS
━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━

PUBLIC (Always visible):
  Characteristics:
    → Shown in dialogue without requirements
    → Auto-revealed when talking to NPC
    → Non-sensitive, everyday objectives

  Examples:
    - "I need to earn enough to eat today"
    - "I'm trying to become a master smith"
    - "The shop is open 9-5 every day"

  Player sees: No restrictions

REVEALABLE (Conditionally visible):
  Characteristics:
    → Hidden until conditions met
    → Conditions: Trust threshold OR information skills
    → Personal but not dangerous information

  Examples:
    - "I'm saving gold to propose to Maria"
    - "I'm secretly practicing magic"
    - "I'm afraid of the city guard"

  Unlock conditions:
    OPTION A: Trust ≥ 50
    OPTION B: [Observation] skill level 11+
    OPTION C: Special item (Ring of Revelation)

  Player sees: After trust built or skill used

SECRET (Never shown in dialogue):
  Characteristics:
    → Never voluntarily revealed
    → Must discover through investigation
    → Not necessarily malicious, just private

  Examples:
    - "I'm a member of the resistance"
    - "I'm hiding my real class from everyone"
    - "I'm actually a woman disguised as a man"

  Discovery methods:
    → [Observation] skill level 15+ (check during actions)
    → Finding letters/notes (0.1% loot chance)
    → Hearing rumors from other NPCs
    → Catching NPC in action (stalking, waiting)

  Player sees: Only through active investigation

VILLAIN (Hidden, malicious, dangerous):
  Characteristics:
    → Actively hidden from discovery
    → Malicious intent (harm, domination, destruction)
    → Discovering is dangerous (NPC may attack)

  Examples:
    - "I'm plotting to assassinate the Duke"
    - "I'm slowly poisoning the town's water supply"
    - "I'm gathering an army to overthrow the kingdom"

  Discovery methods:
    → [Observation] level 21+ OR [True Sight] spell
    → Catching NPC in act (very dangerous)
    → Finding incontrovertible evidence (letter, witness)
    → Being told by another NPC (rare betrayal)

  Discovery consequences:
    → NPC becomes hostile if discovered
    → May attempt to eliminate witness (player)
    → Can trigger quest line to stop villain
    → Success: Legendary achievement, world saved
    → Failure: Player death or reputation loss

  Player sees: Only with high-level skills, at great risk
```

### 4.2 Information Disclosure System

Players start with minimal information about NPCs and can invest in capabilities to reveal more:

```
BASE VISIBILITY (No special requirements):
━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━

Player can see:
  → NPC Name
  → NPC Displayed Class (what THEY choose to show)
  → Basic appearance and visible equipment
  → Current location (if known)
  → General mood indicator (if talked to recently)

Example: "Tomas Ironhand, Blacksmith"
  Tomas displays: [Blacksmith] class
  Real class: [Blacksmith] Lv.34 (not hidden in this case)

Example: "Serena, Merchant"
  Serena displays: [Merchant] class
  Real class: [Spy] Lv.19 - HIDDEN (she's undercover)

INFORMATION-GATHERING TOOLS:
━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━

SKILLS:

[Observation] (perception.based):
  Level 1-5:
    → See active mood (happy, sad, stressed, angry)
    → See general health status
    → Notice if NPC is lying (basic detection)

  Level 6-10:
    → See goal count: "Has 5 active goals"
    → See if any goals are REVEALABLE or SECRET
    → Detect disguised NPCs (20% chance per level)

  Level 11-15:
    → REVEALABLE goals shown in dialogue
    → See NPC's personality traits
    → Detect disguised NPCs (50% chance)
    → Can observe SECRET goals (10% chance per interaction)

  Level 16-20:
    → REVEALABLE + SECRET goals shown in dialogue
    → See NPC's values and priorities
    → Detect disguised NPCs (80% chance)
    → Can observe SECRET goals (30% chance per interaction)
    → Can detect VILLAIN goals (5% chance, DANGEROUS)

  Level 21+ (Master):
    → All non-VILLAIN goals visible
    → Detect disguised NPCs (100% chance)
    → Can observe SECRET goals (50% chance per interaction)
    → Can detect VILLAIN goals (15% chance, very dangerous)
    → NPC may notice observation: relationship penalty

[Insight] (social.empathy.based):
  Level 1-10:
    → See target's personality traits
    → Detect if NPC is lying (better than Observation)
    → Estimate NPC's priority weights

  Level 11-20:
    → See NPC's values hierarchy
    → Understand NPC's motivations
    → Predict NPC's likely decisions

  Level 21+ (Master):
    → Deep understanding of NPC psychology
    → Can predict NPC reactions to proposals
    → Can manipulate NPC decisions (social checks)

SPELLS:

[Mind Read] (magic.arcane, illegal in most regions):
  Requirements: magic.arcane ≥ 5000 XP
  Effect: Briefly see all active goals for 10 seconds
  Risk:
    → Detection chance: 10% per use
    → If detected: -50 relationship, criminal charges
    → Illegal in most lawful regions

  Duration: Instant (one-time glimpse)
  Cooldown: 1 hour
  Range: Touch or close proximity (5 meters)

[True Sight] (magic.divine, high-level):
  Requirements: magic.divine ≥ 20000 XP, [Priest] or [Paladin] class
  Effect:
    → See NPC's real class (not displayed class)
    → See VILLAIN-tier goals safely (cannot backfire)
    → See through all disguises and illusions
  Duration: Sustained (1 minute per 100 mana)
  Cooldown: 10 minutes
  Range: Line of sight (20 meters)

  Safety: Divine protection prevents retaliation

ITEMS:

Ring of Revelation:
  Slot: Accessory
  Rarity: Rare (can drop from elite mobs)
  Effect: While worn, reveals SECRET goals
  Limitations:
    → Does NOT reveal VILLAIN goals (too dangerous)
    → NPC may sense ring: "Why are you staring at me?"
    → -5 relationship per hour of continuous use

  Cost: 500 gold from special merchants
  Crafting: Requires [Jeweler] with special recipe

Amulet of Truth:
  Slot: Accessory
  Rarity: Epic (rare drop or quest reward)
  Effect:
    → Shows if NPC is lying about displayed class
    → Detects hidden class tiers
    → Reveals if NPC has VILLAIN goals (doesn't show what)
  Limitations:
    → Only indicates deception, not details
    → Requires [Insight] 11+ to interpret

  Cost: 2000 gold (very expensive)
  Crafting: Requires [Enchanter] with special materials

Pendant of Privacy:
  Slot: Accessory
  Rarity: Uncommon
  Effect:
    → Protects wearer from [Observation] and [Mind Read]
    → NPCs cannot see your goals or class
    → Adds +20 to all checks against detection
  Useful for: Players with VILLAIN goals or secrets

DISCOVERY MECHANICS:
━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━

Method 1: Observation (Skill-based)
  During any interaction with NPC:
  IF player.[Observation] level >= required_level:
    chance = base_chance + (skill_level * 2%)

    roll = random(0, 100)
    IF roll < chance:
      Discovery successful!
      Add goal to player's knowledge of NPC
      Player sees: "[SECRET] Discovered: Plotting against Duke"

Method 2: Evidence Gathering (World-based)
  Finding letters/notes:
    → 0.1% base chance when looting enemies
    → +0.1% per [Observation] level
    → Letters reveal specific goals and plans
    Example: "Coded letter from Serena to contact: 'The Duke dies tomorrow'"

  Hearing rumors:
    → Talk to NPCs who know the target
    → "Have you noticed anything strange about Serena?"
    → Chance based on target's relationship with rumor source
    → Friends might warn you, enemies might spread rumors

  Stalking/waiting:
    → Follow NPC and observe their actions
    → Wait near suspected meeting spots
    → Risk: NPC notices stalking → hostility
    → Reward: Can see SECRET/VILLAIN goals in action

Method 3: Betrayal (NPC-based)
  Rare: NPCs turn on each other
  → Villain's henchman feels remorse
  → Approaches player with information
  → "Serena is planning something terrible. I can't be part of it."
  → Betrayal risk: Henchman might be testing your loyalty
```

### 4.3 Visibility Examples

```
EXAMPLE: Discovering a Villain
━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━

NPC: Serena Blackwood
Displayed to player: "Serena, Merchant"
Real class: [Spy] Lv.19, [Saboteur] Lv.12
Archetype: Villain (The Crime Lord)

Player WITHOUT information tools:
  Sees: "Serena, Merchant" (that's it)
  Assumes: Normal merchant NPC

Player WITH [Observation] Lv.15:
  Sees:
    - Name: "Serena Blackwood"
    - Displayed Class: [Merchant]
    - Mood: Neutral (hiding true feelings)
    - Goals: "Has 7 active goals"
    - Alert: "2 SECRET goals detected"

  Action required: Use observation repeatedly during interactions
  After 5 interactions: 30% cumulative chance
  Discovery: "[SECRET] Serena controls the local thieves' guild"

Player WITH [True Sight] spell:
  Sees (instantly, no RNG):
    - Name: "Serena Blackwood"
    - Real Class: [Spy] Lv.19, [Saboteur] Lv.12 (NOT Merchant!)
    - Mood: Calculating (true emotion)
    - Goals:
      PUBLIC: "Run merchant shop" (cover)
      REVEALABLE: None (trusts no one)
      SECRET: "Control thieves' guild"
      VILLAIN: "Assassinate Duke to take over city"

  Now player can:
    → Confront Serena (dangerous)
    → Gather evidence and report to guards
    → Blackmail Serena (risky)
    → Ignore (Serena's plot succeeds, world changes)

Player discovers by finding letter:
  Looting bandit corpse: 0.1% chance
  [Observation] Lv.15 bonus: +0.3% (0.4% total)
  Drop: "Coded letter from Serena: 'Phase 2 begins at midnight. Duke's guards bribed.'"

  Evidence is incontrovertible. Can present to:
    → City guards (arrest Serena)
    → Duke personally (quest line)
    → Public (expose Serena, reputation boost)

Consequences of Discovery:
  IF player confronts Serena without proof:
    → Serena denies everything
    → Relationship: -50 (now enemies)
    → Serena accelerates plot (must act before exposed)

  IF player presents evidence to Duke:
    → Quest line: "Stop Serena's Assassination"
    → Serena becomes hostile, attacks player
    → Victory: Legendary achievement [City Savior]
    → Reward: Duke's gratitude, legendary item
```

### 4.4 UI Mockups

```
┌────────────────────────────────────────────┐
│            NPC INSPECTED                   │
├────────────────────────────────────────────┤
│                                            │
│  👤 Serena Blackwood                        │
│  ─────────────────────────────────────     │
│  Class: [Merchant] (displayed)             │
│  Location: Market District                 │
│  Mood: Neutral ⚠️ (seems guarded)          │
│                                            │
│  📋 ACTIVE GOALS: 7                        │
│     • 6 PUBLIC goals visible               │
│     • 1 REVEALABLE goal (trust required)   │
│     ⚠️ 1 SECRET goal detected              │
│     ⚠️ 1 VILLAIN goal suspected (!!)        │
│                                            │
│  [💬 Talk to Serena]  [👁️ Observe]  [⚔️ Attack] │
└────────────────────────────────────────────┘

Player with [Observation] Lv.15 viewing Serena


┌────────────────────────────────────────────┐
│         SERENA'S GOALS (Revealed)           │
├────────────────────────────────────────────┤
│                                            │
│  PUBLIC GOALS:                              │
│  ☑ "Maintain merchant shop"                │
│     Priority: 80 | Status: Active           │
│  ☑ "Build merchant network"                │
│     Priority: 60 | Status: 40% complete     │
│  ☑ "Earn 1000 gold"                        │
│     Priority: 50 | Status: 60% complete     │
│                                            │
│  REVEALABLE GOAL (Trust ≥50):              │
│  🔒 "Expand crime ring" - LOCKED            │
│     Increase trust to see this goal         │
│     Current trust: 30/50                    │
│                                            │
│  SECRET GOAL (Discovered):                  │
│  ⚠️ "Control thieves' guild"               │
│     Priority: 90 | Status: 75% complete    │
│     Discovered via: Observation skill       │
│     Evidence: Confirmed sighting            │
│                                            │
│  VILLAIN GOAL (Suspected):                  │
│  ☠️ "Assassinate Duke" - SUSPECTED          │
│     Priority: 95 | Status: Planning        │
│     Certainty: 40% (need more evidence)     │
│     ⚠️ DANGEROUS - confirming may risk safety │
│                                            │
│  [Gather Evidence]  [Report to Guards]      │
└────────────────────────────────────────────┘

Player with [True Sight] viewing all goals


┌────────────────────────────────────────────┐
│          INFORMATION DISCOVERY              │
├────────────────────────────────────────────┤
│                                            │
│  Your Information-Gathering Tools:         │
│                                            │
│  👁️ [Observation] Lv.15                    │
│     • See mood, goal count, secret types    │
│     • 30% chance to discover SECRET goals  │
│     • Can detect VILLAIN goals (risky)      │
│                                            │
│  💍 Ring of Revelation (NOT EQUIPPED)       │
│     • Reveals SECRET goals while worn      │
│     • Does not work on VILLAIN goals        │
│     [Equip]                                 │
│                                            │
│  🔮 True Sight (READY)                      │
│     • See all goals safely                  │
│     • Reveal real class, not displayed      │
│     • Duration: 5 minutes                   │
│     Mana Cost: 500/1000                     │
│     [Cast]                                  │
│                                            │
│  Target: Serena Blackwood                  │
│  Risk Level: ☠️☠️☠️ HIGH (VILLAIN SUSPECTED)     │
│                                            │
│  [Cast True Sight]  [Cancel]                │
└────────────────────────────────────────────┘
```

### 4.5 Information Disclosure Data Structure

```
NPC Information Disclosure State:
{
  npc_id: "npc_serena_blackwood",

  visibility_tier: "VILLAIN",  // HIGHEST tier goal

  displayed_class: {
    class: "[Merchant]",
    level: 28,
    is_real_class: false  // LIE!
  },

  real_class: {
    class: "[Spy]",
    level: 19,
    additional_class: "[Saboteur]",
    additional_level: 12
  },

  goals_by_visibility: {
    public: [
      { id: "goal_maintain_shop", priority: 80, progress: 0.9 }
    ],

    revealable: [
      {
        id: "goal_expand_crime_ring",
        priority: 90,
        progress: 0.4,
        unlock_condition: { type: "trust", threshold: 50 },
        current_player_trust: 30,
        locked: true
      }
    ],

    secret: [
      {
        id: "goal_control_thieves",
        priority: 90,
        progress: 0.75,
        discovered: true,
        discovery_method: "observation_skill",
        discovery_date: "2026-02-03"
      }
    ],

    villain: [
      {
        id: "goal_assassinate_duke",
        priority: 95,
        progress: 0.15,
        discovered: false,
        suspected: true,
        certainty: 0.4,  // 40% sure
        evidence_count: 2
      }
    ]
  },

  player_knowledge: {
    has_observed: true,
    observation_skill_level: 15,
    discovered_goals: ["goal_control_thieves"],
    suspected_goals: ["goal_assassinate_duke"],
    knows_real_class: false,
    has_evidence: false
  }
}
```

---

## 5. Goal Failure vs Abandonment States

### 5.1 Goal State Machine

NPC goals progress through multiple states with distinct emotional responses:

```
GOAL STATE MACHINE
━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━

ACTIVE → Normal pursuit
  ↓
  ↓ (external blocker appears)
  ↓
BLOCKED → Temporary obstacle, might resolve
  ↓ (after 4+ weeks blocked)
  ↓
ABANDONED OR FAILED (based on blocker type)

ABANDONED → NPC chose to stop (no penalty)
  ↓
  ↓ (external permanent blocker)
  ↓
FAILED → External defeat, NPC feels sadness

State Transitions:
━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━

ACTIVE → BLOCKED:
  Conditions:
    → External blocker emerges
    → Examples:
      * Mine overrun by monsters
      * Blacksmith shop closed temporarily
      • Road blocked by bandits
      * NPC trainer disappears
  Effects:
    → -5 mood (frustration) while blocked
    → Goal progress paused
    → NPC cannot pursue goal actions
    → Re-evaluates weekly for resolution
  Duration:
    → Can be days to weeks
    → If 4+ weeks: Auto-transition to ABANDONED or FAILED

BLOCKED → ABANDONED:
  Conditions:
    → 4+ weeks blocked AND new opportunity arises
    → NPC chooses to stop pursuing (voluntary)
    → Goal no longer aligns with personality/values
  Examples:
    → "Learning enchanting" blocked 4 weeks
    → New blacksmith teacher offers apprenticeship
    → NPC: "I'll pursue this new path instead"
  Effects:
    → No mood penalty (neutral life change)
    → Goal marked ABANDONED in history
    → May generate similar replacement goal
    → NPC moves on, no emotional impact

BLOCKED → FAILED:
  Conditions:
    → 4+ weeks blocked AND blocker is PERMANENT
    → External factor makes goal IMPOSSIBLE
  Examples:
    → "Kill 10 rats" → rats exterminated from area
    → "Buy sword from Tomas" → Tomas died, shop closed forever
    → "Deliver letter to Maria" → Maria died of illness
  Effects:
    → -10 mood (sadness) for 3 days
    → NPC feels FAILURE (emotional impact)
    → Goal marked FAILED in history
    → May set new similar goal (redemption attempt)
    → Stress increases (+0.15)

ACTIVE → ABANDONED (direct):
  Conditions:
    → New higher-priority goal conflicts
    → Goal no longer aligns with personality/values
    → NPC's life circumstances changed significantly
  Examples:
    → Got married, abandon "visit tavern nightly"
    → Religious conversion, abandon "gambling goals"
    → Had child, abandon "adventuring goals"
  Effects:
    → No mood penalty (autonomous choice)
    → Goal marked ABANDONED
    → New conflicting goal activates
    → Life goes on

ACTIVE → FAILED (direct):
  Rare: Most failures go through BLOCKED state first
  Conditions:
    → Catastrophic immediate failure
  Examples:
    → Enemy NPC killed goal target before completion
    → Building burned down during construction
    → Race lost (competitor finished first)
  Effects:
    → -10 mood (shock) for 3 days
    → Immediate emotional impact
    → Goal marked FAILED
    → Stress spike (+0.25)

ABANDONED → FINAL STATE:
  → No further action on goal
  → Remains in NPC goal history
  → Can reference: "I tried that once"
  → No penalty, life continued

FAILED → FINAL STATE:
  → No further action on goal
  → Remains in NPC goal history
  → Can reference: "I failed at that"
  → Mood recovered after 3 days
  → May attempt redemption (new similar goal)
```

### 5.2 State Duration and Mood Effects

```
State Duration Rules:
━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━

BLOCKED State:
  Max duration: 4 weeks (28 days)
  Re-evaluation: Weekly check for resolution

  Weekly check:
    IF block_resolved:
      → Return to ACTIVE
      → Clear mood penalty
      → Resume progress

    IF block_persists:
      → Continue BLOCKED
      → Maintain -5 mood penalty
      → Check if 4 weeks reached

  After 4 weeks blocked:
    IF blocker_permanent:
      → Transition to FAILED
    ELSE IF new_opportunity_available:
      → Transition to ABANDONED
    ELSE IF npc_personality.opportunistic:
      → Transition to ABANDONED (moved on)
    ELSE:
      → Transition to FAILED (gave up)

Mood Effects:
━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━

BLOCKED:
  Penalty: -5 mood
  Duration: While blocked
  Stacks: No (only one blocked mood penalty at a time)
  Recovery: Immediately when unblocked

ABANDONED:
  Penalty: None (neutral)
  Duration: N/A
  Emotional impact: "Life happens, moving on"

FAILED:
  Penalty: -10 mood
  Duration: 3 days (72 hours)
  Stacks: Yes (-10 per failed goal, max -30)
  Recovery: Natural recovery after 3 days
  Secondary effects:
    → Stress: +0.15 (persistent)
    → Satisfaction: -0.10 (persistent)
    → May generate "coping" goals (drinking, isolation)

Example: Tomas's "Learn Enchanting" Goal
━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━

Week 1-4: ACTIVE (35% progress)
  Mood: Normal (0.72)
  Status: Making good progress

Week 5: BLOCKED (Mira disappears)
  Mood: 0.67 (-0.05 from blocked)
  Status: Frustrated, waiting for news

Week 8: BLOCKED (3 weeks blocked)
  Mood: 0.67 (still frustrated)
  Status: Hope declining, still waiting

Week 12: BLOCKED (4+ weeks, threshold reached)
  Re-evaluation triggered

Possible Outcomes:

  Outcome A: ABANDONED (new opportunity)
    → New enchanter arrives in town
    → Tomas: "I'll study with the new teacher"
    → Goal "Learn enchanting" → ABANDONED
    → New goal: "Study with new enchanter" created
    → Mood: No penalty (neutral transition)

  Outcome B: FAILED (permanent blocker)
    → News: Mira died in accident
    → Goal "Learn enchanting" → FAILED
    → Mood: 0.67 → 0.57 (-0.10, sadness)
    → Duration: 3 days of sadness
    → Stress: 0.23 → 0.38 (+0.15)
    → After 3 days: Mood recovers to 0.65
    → May generate: "Try again with different teacher" (redemption)
```

### 5.3 Failure State Examples

```
EXAMPLE 1: Permanent Blocker → FAILED
━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━

NPC: Guardsman Rowan
Goal: "Kill 10 wolves" (protection quest for village)
Progress: 7/10 wolves killed

Event: Hunter guild clears wolf den
  → Wolves exterminated from region
  → Permanent blocker: No more wolves to kill

Week 1: BLOCKED
  Rowan: "Where did all the wolves go?"
  Mood: -5 (frustrated)

Week 2: Still BLOCKED
  Rowan: "This quest is impossible now"

Week 3: Still BLOCKED
  Rowan: "I can't complete this"

Week 4: Threshold reached → FAILED
  Goal: "Kill 10 wolves" → FAILED
  Mood: 0.70 → 0.60 (-10 sadness)
  Duration: 3 days

Day 3: Mood recovers to 0.65
  Stress: Increased from 0.15 to 0.30
  New goal: "Protect village from other threats"

History: Rowan remembers failed quest
  Dialogue: "I tried to hunt the wolves once, but they were all killed by hunters first."

---

EXAMPLE 2: Voluntary Abandonment → NO PENALTY
━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━

NPC: Elena the Baker's Daughter
Goal: "Learn magic" (skill acquisition)
Progress: 15% complete

Event: Elena falls in love with local farmer
  → New life priority: Marriage, family
  → Goal "Learn magic" no longer aligns with values
  → Voluntary abandonment

Decision: ABANDONED
  Goal: "Learn magic" → ABANDONED
  Mood: No penalty (neutral choice)
  Reason: "Found new purpose in life"

New Goals:
  "Help husband with farm" (economic)
  "Raise a family" (social, long-term)

History: Elena remembers magical curiosity
  Dialogue: "I once wanted to be a mage, but then I met Thomas. No regrets!"

---

EXAMPLE 3: Redemption After Failure
━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━

NPC: Tomas the Blacksmith
Goal: "Forge legendary sword" (creation)
Progress: 80% complete

Event: Workshop burns down
  → Equipment destroyed, materials lost
  → Permanent blocker: Cannot afford to restart

Week 1: BLOCKED
  Tomas: "My life's work... gone."
  Mood: -5 (frustrated)

Week 2-4: Still BLOCKED
  Tomas: "I can't afford to replace everything"

Week 5: FAILED
  Goal: "Forge legendary sword" → FAILED
  Mood: 0.75 → 0.65 (-10 sadness)
  Stress: 0.10 → 0.25 (+0.15)
  Duration: 3 days

Day 3: Mood recovers
  Satisfaction: 0.70 → 0.60 (persistent loss)

Week 6: Redemption goal generated
  New goal: "Rebuild workshop and try again"
  Type: Similar to failed goal
  Motivation: "I won't let defeat stop me"
  Priority: 85 (high conviction)

This shows NPC resilience and growth
```

### 5.4 State Transition UI

```
┌────────────────────────────────────────────┐
│          NPC GOAL STATUS CHANGED            │
├────────────────────────────────────────────┤
│                                            │
│  Tomas Ironhand's Goal Status:              │
│                                            │
│  "Learn Enchanting"                        │
│  Status: BLOCKED → FAILED                  │
│                                            │
│  Reason:                                    │
│  Enchanter Mira has been confirmed dead.    │
│  No other enchanting teacher available.     │
│                                            │
│  Effects:                                   │
│  ❌ Mood: -10 (sadness) for 3 days          │
│  ⚠️ Stress: +15% (persistent)               │
│  💔 Satisfaction: -10% (persistent)         │
│                                            │
│  Tomas's Reaction:                          │
│  "She was the only one who could teach me.  │
│   I don't know what to do now."             │
│                                            │
│  [Offer comfort]  [Suggest new teacher]     │
│  [Leave him alone]                          │
└────────────────────────────────────────────┘


┌────────────────────────────────────────────┐
│           NPC: Tomas Ironhand               │
├────────────────────────────────────────────┤
│                                            │
│  Status: Sad 💔 (Day 1 of 3)                │
│  Mood: 0.57 / 1.00 (-0.15 from failed)     │
│  Stress: 0.38 / 1.00 (+0.15 from failed)    │
│                                            │
│  Active Goals (7/8 slots):                  │
│  ✅ "Complete Duke's sword" (95%)           │
│  ✅ "Save 500 gold" (60%)                   │
│  ✅ "Attend festival Saturday" (pending)    │
│  ...                                        │
│  ❌ "Learn Enchanting" - FAILED             │
│                                            │
│  Mood Recovery: 2 days remaining           │
│  Expected mood: 0.67 after recovery        │
│                                            │
│  [Talk to Tomas]  [Give gift]               │
└────────────────────────────────────────────┘
```

### 5.5 Data Structure

```
Goal State Machine Data:
{
  goal_id: "goal_learn_enchanting",
  npc_id: "npc_tomas_ironhand",

  current_state: "FAILED",
  previous_state: "BLOCKED",
  state_changed: "2026-02-03T10:00:00Z",

  state_history: [
    {
      state: "ACTIVE",
      from: "2026-01-01T00:00:00Z",
      to: "2026-02-01T00:00:00Z",
      duration: "31 days"
    },
    {
      state: "BLOCKED",
      from: "2026-02-01T00:00:00Z",
      to: "2026-02-03T10:00:00Z",
      duration: "2.4 days",
      blocker: "Enchanter Mira disappeared",
      mood_penalty: -5
    },
    {
      state: "FAILED",
      from: "2026-02-03T10:00:00Z",
      to: null,
      blocker: "Mira confirmed dead (permanent)",
      mood_penalty: -10,
      mood_penalty_duration: "3 days",
      stress_increase: +0.15
    }
  ],

  blocker: {
    type: "NPC_DEATH",
    description: "Enchanter Mira died in accident",
    is_permanent: true,
    discovered: "2026-02-03T09:00:00Z",
    confirmed_by: "Town guard report"
  },

  consequences: {
    mood_impact: -10,
    mood_duration: 72,  // hours
    stress_impact: +0.15,
    satisfaction_impact: -0.10,
    may_generate_redemption_goal: true
  },

  recovery: {
    expected_completion: "2026-02-06T10:00:00Z",
    expected_mood_after: 0.67,
    days_remaining: 3
  }
}
```

---

## 6. Resolved Open Questions

This document resolves the following HIGH priority questions from open-questions.md:

| #   | Question                    | Resolution                                                   | Status      |
| --- | --------------------------- | ------------------------------------------------------------ | ----------- |
| 5.1 | Goal Re-evaluation Triggers | Time-Based + Event Triggers with rate limiting               | ✅ Resolved |
| 5.2 | Concurrent Goal Limit       | Timeframe categories (8 max) with survival override          | ✅ Resolved |
| 5.3 | Conflicting Goal Resolution | Personality + Priority + Age scoring algorithm               | ✅ Resolved |
| 5.4 | Secret NPC Goals            | Visibility tiers (PUBLIC/REVEALABLE/SECRET/VILLAIN) + skills | ✅ Resolved |
| 5.5 | Goal Failure vs Abandonment | State-based classification (BLOCKED/ABANDONED/FAILED)        | ✅ Resolved |

**All 5 HIGH priority NPC Core Systems questions resolved.**

---

## 7. Cross-References

Related Documents:

- **[npc-design.md](./npc-design.md)** - Foundational NPC architecture (unified character model, archetypes, goal types)
- **[Core Progression System GDD](./core-progression-system-gdd.md)** - XP buckets, class acquisition, skill eligibility (NPCs use same system)
- **[Class System GDD](./class-system-gdd.md)** - Class tiers, specializations (NPCs progress through these)
- **[Skill & Recipe System GDD](./skill-recipe-system-gdd.md)** - Legendary achievements (sacrifice rewards tie into this)
- **[idle-game-overview.md](../idle-game-overview.md)** - Game design pillars and NPC role in world

---

## 8. Implementation Notes

### 8.1 Data Structure

```csharp
public class NPCGoalSystem
{
    public string NpcId { get; set; }

    // Goal Capacity
    public int MaxImmediateGoals => 2;
    public int MaxShortTermGoals => 3;
    public int MaxMediumTermGoals => 2;
    public int MaxLongTermGoals => 1;
    public int MaxTotalGoals => 8;

    // Active Goals by Category
    public List<ActiveGoal> ImmediateGoals { get; set; }
    public List<ActiveGoal> ShortTermGoals { get; set; }
    public List<ActiveGoal> MediumTermGoals { get; set; }
    public List<ActiveGoal> LongTermGoals { get; set; }

    // Survival Override
    public bool SurvivalOverrideActive { get; set; }
    public SurvivalTrigger SurvivalTrigger { get; set; }

    // Re-evaluation
    public DateTime LastReEvaluation { get; set; }
    public int ReEvaluationsToday { get; set; }
    public List<ReEvaluationTrigger> PendingTriggers { get; set; }
}

public class ActiveGoal
{
    public string GoalId { get; set; }
    public string Description { get; set; }
    public GoalType Type { get; set; }
    public GoalVisibility Visibility { get; set; }  // PUBLIC, REVEALABLE, SECRET, VILLAIN

    // Priority & Progress
    public int Priority { get; set; }  // 0-100
    public double Progress { get; set; }  // 0.0-1.0
    public DateTime CreatedDate { get; set; }
    public DateTime? Deadline { get; set; }

    // State
    public GoalState State { get; set; }  // ACTIVE, BLOCKED, ABANDONED, FAILED
    public GoalBlocker Blocker { get; set; }
    public DateTime? BlockedSince { get; set; }

    // Personality modifiers for conflict resolution
    public PersonalityTrait PersonalityModifier { get; set; }
}

public class GoalBlocker
{
    public string BlockerId { get; set; }
    public string Description { get; set; }
    public bool IsPermanent { get; set; }
    public DateTime Discovered { get; set; }
}

public enum GoalState
{
    ACTIVE,
    BLOCKED,
    ABANDONED,
    FAILED
}

public enum GoalVisibility
{
    PUBLIC,
    REVEALABLE,
    SECRET,
    VILLAIN
}

public class NPCInformationDisclosure
{
    public string NpcId { get; set; }

    // What the player sees
    public string DisplayedClass { get; set; }
    public bool IsDisplayingRealClass { get; set; }

    // Player's capabilities
    public int ObservationSkillLevel { get; set; }
    public bool HasTrueSight { get; set; }
    public List<string> InformationItems { get; set; }  // Rings, amulets, etc.

    // What player has discovered
    public List<string> DiscoveredGoals { get; set; }
    public List<string> SuspectedGoals { get; set; }
    public Dictionary<string, double> GoalCertainty { get; set; }

    // Discovery risk
    public bool DetectionRisk { get; set; }
    public string LastObservationDate { get; set; }
}
```

### 8.2 Re-evaluation Algorithm

```csharp
public void TriggerReEvaluation(ReEvaluationTrigger trigger)
{
    var npc = GetNPC(trigger.NpcId);

    // Rate limiting check
    if ((DateTime.UtcNow - npc.LastReEvaluation).TotalHours < 24)
    {
        if (npc.ReEvaluationsToday >= 1)
        {
            npc.PendingTriggers.Add(trigger);
            return;  // Queue for tomorrow
        }
    }

    // Execute re-evaluation
    ProcessReEvaluation(npc, trigger);

    // Update rate limit state
    npc.LastReEvaluation = DateTime.UtcNow;
    npc.ReEvaluationsToday++;

    // Reset counter at midnight
    ScheduleMidnightReset(npc);
}

private void ProcessReEvaluation(NPC npc, ReEvaluationTrigger trigger)
{
    // Step 1: Check trigger type
    switch (trigger.Type)
    {
        case TriggerType.GoalCompleted:
            GenerateReplacementGoals(npc, trigger.CompletedGoal);
            UpdateMoodAndSatisfaction(npc);
            CheckArchetypeShift(npc);
            break;

        case TriggerType.WorldEvent:
            AssessWorldEventImpact(npc, trigger.WorldEvent);
            GenerateReactiveGoals(npc, trigger.WorldEvent);
            break;

        case TriggerType.WeeklyReview:
            PerformWeeklyReview(npc);
            break;
    }

    // Step 2: Update goal states
    UpdateGoalStates(npc);

    // Step 3: Update NPC state
    UpdateNPCState(npc);

    // Step 4: Regenerate daily loop
    RegenerateDailyLoop(npc);
}
```

### 8.3 Conflict Resolution Algorithm

```csharp
public ActiveGoal ResolveConflict(ActiveGoal goalA, ActiveGoal goalB, NPC npc)
{
    // Step 1: Calculate base scores
    int scoreA = CalculateGoalScore(goalA);
    int scoreB = CalculateGoalScore(goalB);

    int scoreDifference = Math.Abs(scoreA - scoreB);

    // Step 2: Check if clear winner
    if (scoreDifference > 15)
    {
        return scoreA > scoreB ? goalA : goalB;
    }

    // Step 3: Personality tiebreaker
    int personalityBonus = CalculatePersonalityModifier(goalA, goalB, npc);

    int finalScoreA = scoreA;
    int finalScoreB = scoreB + personalityBonus;

    return finalScoreA > finalScoreB ? goalA : goalB;
}

private int CalculateGoalScore(ActiveGoal goal)
{
    int ageDays = (DateTime.UtcNow - goal.CreatedDate).Days;

    int ageBonus = 0;
    if (ageDays >= 15) ageBonus = 20;
    else if (ageDays >= 7) ageBonus = 10;

    return goal.Priority + ageBonus;
}

private int CalculatePersonalityModifier(ActiveGoal goalA, ActiveGoal goalB, NPC npc)
{
    // Determine personality type
    var personality = npc.Personality;

    if (personality.Openness > 0.7 && personality.Conscientiousness < 0.4)
    {
        // Impulsive: prefer newer goals
        return goalA.CreatedDate < goalB.CreatedDate ? 15 : 0;
    }

    if (personality.Openness < 0.4 && personality.Conscientiousness > 0.6)
    {
        // Deliberative: prefer older goals
        return goalA.CreatedDate > goalB.CreatedDate ? 15 : 0;
    }

    if (personality.Neuroticism > 0.6 && personality.Agreeableness < 0.4)
    {
        // Opportunistic: prefer higher reward
        return goalB.ExpectedReward > goalA.ExpectedReward ? 20 : 0;
    }

    if (personality.Neuroticism > 0.7 && personality.Openness < 0.3)
    {
        // Cautious: prefer lower risk
        return goalB.RiskLevel < goalA.RiskLevel ? 15 : 0;
    }

    return 0;  // No personality modifier
}
```

### 8.4 Goal State Machine

```csharp
public void UpdateGoalStates(NPC npc)
{
    var allGoals = GetAllGoals(npc);

    foreach (var goal in allGoals)
    {
        switch (goal.State)
        {
            case GoalState.ACTIVE:
                CheckForBlockers(goal, npc);
                break;

            case GoalState.BLOCKED:
                ProcessBlockedGoal(goal, npc);
                break;

            case GoalState.ABANDONED:
            case GoalState.FAILED:
                // Final states, no processing
                break;
        }
    }
}

private void ProcessBlockedGoal(ActiveGoal goal, NPC npc)
{
    var daysBlocked = (DateTime.UtcNow - goal.BlockedSince).Value.Days;

    // Check if block resolved
    if (IsBlockResolved(goal))
    {
        goal.State = GoalState.ACTIVE;
        goal.BlockedSince = null;
        goal.Blocker = null;
        npc.Mood += 0.05f;  // Recovery from frustration
        return;
    }

    // Check if threshold reached (4 weeks)
    if (daysBlocked >= 28)
    {
        if (goal.Blocker.IsPermanent)
        {
            // Transition to FAILED
            goal.State = GoalState.FAILED;
            ApplyFailureEffects(npc, goal);
        }
        else if (HasNewOpportunity(npc, goal))
        {
            // Transition to ABANDONED
            goal.State = GoalState.ABANDONED;
            // No mood penalty
        }
        else if (npc.Personality.Openness > 0.7)
        {
            // Opportunistic: Move on
            goal.State = GoalState.ABANDONED;
        }
        else
        {
            // Gave up
            goal.State = GoalState.FAILED;
            ApplyFailureEffects(npc, goal);
        }
    }
}

private void ApplyFailureEffects(NPC npc, ActiveGoal goal)
{
    // Immediate mood penalty
    npc.Mood -= 0.10f;

    // Stress increase
    npc.Stress += 0.15f;

    // Schedule mood recovery (3 days)
    ScheduleMoodRecovery(npc, 0.10f, TimeSpan.FromDays(3));

    // Check for redemption goal generation
    if (ShouldGenerateRedemptionGoal(npc, goal))
    {
        GenerateRedemptionGoal(npc, goal);
    }
}
```

### 8.5 Information Disclosure

```csharp
public class NPCInformationSystem
{
    public NPCGoalVisibility GetGoalVisibility(
        NPC npc,
        ActiveGoal goal,
        Player player)
    {
        // VILLAIN goals: highest security
        if (goal.Visibility == GoalVisibility.VILLAIN)
        {
            if (player.HasTrueSightActive)
                return NPCGoalVisibility.Full;

            if (player.ObservationSkillLevel >= 21)
            {
                // 15% chance, dangerous
                if (Random.Value < 0.15)
                {
                    CheckForDetection(npc, player);
                    return NPCGoalVisibility.Full;
                }
            }

            return NPCGoalVisibility.Hidden;
        }

        // SECRET goals
        if (goal.Visibility == GoalVisibility.SECRET)
        {
            // Trust-based
            var relationship = GetRelationship(npc, player);
            if (relationship.Trust >= 80)
                return NPCGoalVisibility.Full;

            // Skill-based
            if (player.ObservationSkillLevel >= 15)
            {
                // 50% cumulative chance per interaction
                if (Random.Value < 0.50)
                    return NPCGoalVisibility.Full;
            }

            // Item-based
            if (player.WearingItem("Ring of Revelation"))
                return NPCGoalVisibility.Full;

            return NPCGoalVisibility.Hidden;
        }

        // REVEALABLE goals
        if (goal.Visibility == GoalVisibility.REVEALABLE)
        {
            var relationship = GetRelationship(npc, player);
            if (relationship.Trust >= 50)
                return NPCGoalVisibility.Full;

            if (player.ObservationSkillLevel >= 11)
                return NPCGoalVisibility.Full;

            return NPCGoalVisibility.Hidden;
        }

        // PUBLIC goals
        return NPCGoalVisibility.Full;
    }

    private void CheckForDetection(NPC npc, Player player)
    {
        // 5% chance NPC notices observation
        if (Random.Value < 0.05)
        {
            // NPC becomes hostile
            npc.RelationshipWithPlayer -= 0.50;
            npc.Archetype = NPCArchetype.Villain;  // If villain goal revealed

            // May attack
            if (npc.IsDangerous)
            {
                InitiateCombat(npc, player);
            }
        }
    }
}
```

### 8.6 Complexity Ratings

| Component              | Implementation Complexity | Notes                              |
| ---------------------- | ------------------------- | ---------------------------------- |
| Re-evaluation Triggers | Medium (3/5)              | Event system + rate limiting       |
| Goal Slot Management   | Low (2/5)                 | Category tracking with limits      |
| Survival Override      | Medium (3/5)              | Interrupt system + sacrifice logic |
| Conflict Resolution    | Medium (3/5)              | Scoring algorithm + personality    |
| Secret Goals           | Medium-High (4/5)         | Visibility tiers + skill checks    |
| Information Disclosure | Medium (3/5)              | Skill/item modifiers + detection   |
| Goal State Machine     | Medium (3/5)              | State transitions + mood effects   |
| Sacrifice System       | Medium-High (4/5)         | Risk calculation + achievements    |

---

## 9. Design Decisions Record

### 9.1 Weekly Re-evaluation with Immediate Triggers

**Decision:** NPCs re-evaluate goals weekly (Sunday 00:00 UTC) plus immediate triggers for major events.

**Rationale:**

- Stability: Players can learn NPC behavior patterns
- Reactivity: Important events trigger immediate reassessment
- Performance: Batch weekly processing is efficient
- Rate limiting prevents thrashing from rapid successive events

**Trade-offs:**

- Less organic than continuous probabilistic checks
- Mitigated by: Immediate triggers for important events

### 9.2 Timeframe-Categorized Goal Limits

**Decision:** 8 concurrent goals max (2 immediate, 3 short-term, 2 medium-term, 1 long-term) with survival override.

**Rationale:**

- Clear hierarchy prevents goal overflow
- Timeframe distribution matches human planning
- Survival override prevents stupid deaths
- Sacrifice exception enables heroic moments

**Trade-offs:**

- Less flexible than dynamic slot expansion
- Mitigated by: Voluntary sacrifice for dramatic moments

### 9.3 Personality + Priority + Age Conflict Resolution

**Decision:** Three-stage algorithm (priority scoring → age bonus → personality tiebreaker).

**Rationale:**

- Predictable base system (players can learn patterns)
- Age bonus prevents neglected goals from languishing
- Personality adds individual character expression
- Same goals, different NPCs = different outcomes

**Trade-offs:**

- More complex than pure priority or pure personality
- Mitigated by: Clear 15-point threshold before personality applies

### 9.4 Four-Tier Visibility System with Skill Requirements

**Decision:** PUBLIC/REVEALABLE/SECRET/VILLAIN tiers with information-gathering skills.

**Rationale:**

- Creates progression for information discovery
- Makes [Observation] and [Insight] skills valuable
- Villain tier provides high-stakes discovery gameplay
- Supports spy/thief playstyles

**Trade-offs:**

- Requires significant skill investment to see all goals
- Mitigated by: Multiple discovery paths (skills, items, evidence)

### 9.5 State-Based Failure Classification

**Decision:** BLOCKED → ABANDONED or FAILED with distinct emotional responses.

**Rationale:**

- Rich NPC psychology (stress, sadness, frustration)
- BLOCKED state allows for recovery (not permanent)
- Distinction between voluntary (ABANDONED) and involuntary (FAILED)
- Mood effects create dramatic moments

**Trade-offs:**

- More complex than simple timeout system
- Mitigated by: Clear rules (4-week threshold)

---

## 10. Appendix: Complete NPC Examples

### 10.1 Maintainer NPC Example

```
NPC: Tomas Ironhand
Archetype: Maintainer
Class: [Blacksmith - Journeyman] Lv.34
Personality: Deliberative (low openness, high conscientiousness)
Values: Family 0.8, Craft 0.7, Reputation 0.5

Day: Tuesday, Week 6 of year

Active Goals (8/8 slots):
━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━

IMMEDIATE (2/2):
  [1] "Complete Duke's sword order"
      Priority: 95 | Deadline: Today 17:00
      Progress: 85% | Status: ACTIVE
      Type: economic

  [2] "Eat lunch at tavern"
      Priority: 80 | Deadline: Today 13:00
      Progress: 0% | Status: ACTIVE
      Type: survival

SHORT-TERM (3/3):
  [3] "Study enchanting with Mira"
      Priority: 70 | Deadline: 7 days
      Progress: 35% | Status: ACTIVE
      Type: skill_acquisition

  [4] "Save 100 gold for shop expansion"
      Priority: 50 | Deadline: 30 days
      Progress: 60% (have 60 gold)
      Type: economic

  [5] "Attend town festival on Saturday"
      Priority: 65 | Deadline: 3 days
      Progress: 0%
      Type: social_relationship

MEDIUM-TERM (2/2):
  [6] "Build friendship with Maria"
      Priority: 55 | Deadline: 4 weeks
      Progress: 40% (trust 40/100)
      Type: social_relationship

  [7] "Reach [Blacksmith] level 40"
      Priority: 70 | Deadline: 8 weeks
      Progress: 34/40 (85%)
      Type: class_evolution

LONG-TERM (1/1):
  [8] "Become Master Smith"
      Priority: 80 | Deadline: 2 years
      Progress: 15%
      Type: reputation

Daily Loop Generated:
  06:00-07:00: Wake, travel to smithy
  07:00-12:00: Forge Duke's sword (Goal 1)
  12:00-13:00: Eat lunch (Goal 2)
  13:00-17:00: Complete sword OR study if done (Goals 1, 3)
  17:00-19:00: Continue work OR visit Maria (Goals 1, 6)
  19:00-21:00: Evening social at tavern
  21:00-06:00: Sleep (survival)

Conflict Example:
  Saturday morning: Duke's sword OR festival preparation?

  Goal A: "Complete Duke's sword"
    Priority: 95, Age: 2 days
    Score: 95 + 0 = 95

  Goal B: "Help prepare festival"
    Priority: 60, Age: 0 days (just created)
    Score: 60 + 0 = 60

  Difference: 35 points (>15)
  → Clear winner: Goal A
  → Tomas works, skips festival prep
  → (His deliberative personality values duty over fun)
```

### 10.2 Villain NPC Example

```
NPC: Serena Blackwood
Archetype: Villain (Crime Lord)
Real Class: [Spy] Lv.19, [Saboteur] Lv.12
Displayed Class: [Merchant] Lv.28 (cover identity)
Personality: Opportunistic (high neuroticism, low agreeableness)
Values: Power 0.9, Wealth 0.8, Control 0.9

Player WITHOUT information tools sees:
  "Serena, Merchant" - that's it

Player WITH [Observation] Lv.21 sees:
━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━

Name: Serena Blackwood
Class: [Merchant] (DISPLAYED) - ⚠️ SUSPECTED FAKE
Real Class: [Spy] Lv.19, [Saboteur] Lv.12 - REVEALED

Active Goals (9 total):
  PUBLIC (1 shown):
    ✅ "Run merchant shop" (cover operation)

  REVEALABLE (0, trust too low):
    🔒 No revealable goals (trusts no one)

  SECRET (3 discovered via skill):
    ⚠️ "Control thieves' guild" (90% complete)
    ⚠️ "Bribe town guards" (60% complete)
    ⚠️ "Establish smuggling ring" (40% complete)

  VILLAIN (2 suspected, 1 confirmed):
    ☠️ "Assassinate Duke" (15% complete) - CERTAIN
    ☠️ "Take over city government" (5% complete) - SUSPECTED

Danger: ☠️☠️☠️ HIGH
Discovery risk: 15% chance Serena notices observation
If detected: Serena becomes hostile, may eliminate witness

Player Choice:
  [Gather more evidence] [Confront Serena]
  [Report to Duke immediately] [Do nothing for now]
```

### 10.3 Sacrifice Example

```
NPC: Guardsman Rowan
Class: [Warrior - Apprentice] Lv.12
Archetype: Maintainer (but with heroic potential)
Personality: High conviction (values: Protection 0.9)

Situation: Bandit attack on village
  → 5 bandits overwhelming
  → Villagers fleeing
  → Rowan alone at bridge

Survival Check:
  Rowan HP: 25/100 (low)
  Bandits: 5 enemies, levels 8-12
  Escape route: Available

Rowan's Decision:
  Active Goals:
    [1] "Protect village" (priority 95, PROTECTION)
    [2] "Survive" (priority 100, survival)

  Survival Override would normally trigger: Flee to survive

  BUT: Voluntary Sacrifice check
    → Goal is PROTECTION type (defending villagers)
    → Conviction 0.9 (>0.8 threshold)
    → Stress 0.75 (>0.7 threshold)
    → Sacrifice ALLOWED

  Rowan's choice: Stay and fight
    → Overrides survival instinct
    → Risk: Death is possible

Battle outcome calculation:
  Base survival chance: 30%
  Modifiers:
    +20% (level advantage)
    +10% (appropriate equipment)
    +0% (no player help)
    -20% (outnumbered 5:1)
    -10% (exhaustion from long shift)
  Final: 30% survival chance

Roll: 72 (SUCCESS!) - Beat the odds

Result:
  ⚔️ LEGENDARY ACHIEVEMENT UNLOCKED: [Last Stand]
  Bonus: +35% to higher skill tiers
  Reward: [Guardian Angel] skill offered at next level
  Story: "Held the bridge alone against 5 bandits"

  HP: 5/100 (survived by 5 HP!)
  Mood: +0.20 (heroic triumph)
  Stress: -0.30 (relief)
  Villagers saved: 12

  Villager reactions:
    +20 relationship with all saved NPCs
    Bard writes song: "The Bridge of Heroes"
    Statue erected: "Rowan the Brave"

If roll had failed (death):
  🏰 POSTHUMOUS ACHIEVEMENT: [Martyr's Sacrifice]
  Villagers saved: 8
  Memorial service held
  New guardsman recruited to replace Rowan
  Players get relationship bonus with village

This is how legendary stories emerge in the world!
```

---

## 11. Testing & Verification

### 11.1 Unit Test Cases

```
GOAL RE-EVALUATION TRIGGERS:
✓ Weekly review occurs at scheduled time
✓ Immediate trigger processes within 24 hours
✓ Rate limit prevents more than 1 re-eval per day
✓ Pending triggers queue when rate limited
✓ World event triggers re-eval for affected NPCs

CONCURRENT GOAL LIMITS:
✓ Cannot exceed 2 immediate goals
✓ Cannot exceed 3 short-term goals
✓ Cannot exceed 2 medium-term goals
✓ Cannot exceed 1 long-term goal
✓ Survival override interrupts all goal actions
✓ Sacrifice allows voluntary survival override

CONFLICT RESOLUTION:
✓ Priority + Age scoring calculates correctly
✓ Clear winner when scores differ by >15
✓ Personality tiebreaker applies within 15 points
✓ Impulsive NPCs prefer newer goals
✓ Deliberative NPCs prefer older goals
✓ Opportunistic NPCs prefer higher rewards
✓ Cautious NPCs prefer lower risks

SECRET GOALS:
✓ PUBLIC goals always visible
✓ REVEALABLE goals require trust ≥50 OR [Observation] Lv.11+
✓ SECRET goals require [Observation] Lv.15+ OR evidence
✓ VILLAIN goals require [Observation] Lv.21+ OR [True Sight]
✓ Discovery detection chance applies (5% per observation)
✓ Detection triggers hostility for VILLAIN goals

FAILURE STATES:
✓ ACTIVE → BLOCKED when blocker appears
✓ BLOCKED → ACTIVE when blocker resolves
✓ BLOCKED → ABANDONED after 4 weeks if new opportunity
✓ BLOCKED → FAILED after 4 weeks if permanent blocker
✓ FAILED applies -10 mood for 3 days
✓ ABANDONED applies no mood penalty
✓ Stress increases (+0.15) on failure
```

### 11.2 Integration Test Cases

```
NPC LIFECYCLE:
✓ NPC generates initial goals on creation
✓ Goals progress through daily actions
✓ Weekly review adjusts priorities
✓ Major events trigger immediate re-eval
✓ Goals complete, replacements generated
✓ Blocked goals monitored for resolution
✓ Failed/abandoned goals archived
✓ NPC state updates correctly (mood, stress, satisfaction)

PLAYER INTERACTION:
✓ Player can talk to NPC (PUBLIC goals visible)
✓ High trust unlocks REVEALABLE goals
✓ [Observation] skill reveals SECRET goals
✓ [True Sight] reveals VILLAIN goals safely
✓ Player actions can trigger NPC re-eval
✓ Gifts improve relationship, may unlock goals
✓ Quest completion advances NPC goals
✓ Player combat affects NPC goals

WORLD EVENTS:
✓ Invasion triggers AMBITIOUS NPC goal shifts
✓ Economic crash affects merchant goals
✓ NPC death triggers goals in related NPCs
✓ New NPC arrival generates new goals
✓ Festival triggers social goals
✓ Discovery unlocks new goal opportunities

SACRIFICE SYSTEM:
✓ Survival override normally interrupts goals
✓ Protection goals enable sacrifice choice
✓ High conviction NPCs choose sacrifice
✓ Low conviction NPCs choose survival
✓ Sacrifice success unlocks legendary achievement
✓ Sacrifice failure results in NPC death
✓ Legendary achievement gives skill bonuses
```

---

## 12. Performance Considerations

### 12.1 Scalability

```
NPC Population Simulation Targets:

Tier 1 (Real-time): 100 NPCs
  Update rate: Real-time with player
  Detail: Full action resolution
  Features: Dialogue, goals, combat, trade
  CPU budget: ~5ms per tick

Tier 2 (5-minute): 1,000 NPCs
  Update rate: Every 5 minutes
  Detail: Simplified daily loop
  Features: Goal processing, basic AI
  CPU budget: ~50ms per batch

Tier 3 (Hourly): 10,000 NPCs
  Update rate: Every hour
  Detail: Statistical outcomes
  Features: Goal state changes only
  CPU budget: ~100ms per batch

Tier 4 (Daily): 100,000+ NPCs
  Update rate: Once per day
  Detail: Aggregate simulation
  Features: Population-level changes
  CPU budget: ~200ms per batch

Optimization Techniques:
- Spatial partitioning (only simulate nearby NPCs in detail)
- Behavior caching (routine actions stored, not recalculated)
- Event-driven updates (only process on world events)
- Batch processing (group similar NPCs)
- Goal evaluation throttling (limit re-evals)
```

### 12.2 Memory Usage

```
Per-NPC Memory Requirements:

Static Data (cacheable): ~500 bytes
  - Identity, personality, class, skills
  - Loaded once, shared across instances

Dynamic Data (per-instance): ~2 KB
  - Current goals (8 max × ~250 bytes each)
  - State, relationships, inventory
  - Action history (rolling 1000)

History Data: ~5 KB (optional)
  - Complete goal history
  - Major events log
  - Relationship history

Total per NPC: ~7.5 KB (with history)

World Estimates:
- Small town (50 NPCs): ~375 KB
- Large town (500 NPCs): ~3.75 MB
- City (2,000 NPCs): ~15 MB
- Region (10,000 NPCs): ~75 MB
- World (100,000 NPCs): ~750 MB

Memory Optimization:
- Lazy load history data (only load when viewed)
- Compress old goal states
- Archive inactive NPCs to cold storage
```

---

## 13. Future Enhancements

### 13.1 Planned Features (Post-Launch)

1. **NPC Relationship System Expansion**
   - Marriage, family formation
   - Generational NPCs (children inherit traits)
   - Romance and breakup mechanics

2. **NPC Organizations**
   - Guilds, factions, religions
   - Group goals and collective actions
   - Political alliances and wars

3. **NPC Economy System**
   - NPC-to-NPC trading (not just player-to-NPC)
   - Market formation and price evolution
   - Supply chains and production networks

4. **Advanced AI Behaviors**
   - Learning from player actions
   - Adaptive strategies (villains learn from failures)
   - Dynamic personality evolution

5. **NPC-Driven Content**
   - NPCs generate quests organically
   - NPCs initiate events and storylines
   - Player becomes NPC in someone else's story

---

_Document Version 1.0 - Authoritative specification for NPC Core Systems_
_All HIGH priority NPC questions resolved with complete algorithms, examples, and UI mockups_
