# Option B Additions: Full MVP Game Design Enhancements

**Status**: Draft for Review
**Created**: 2026-02-10
**Alignment**: Core Architecture, NPC Design, User Feedback

## Overview

This document specifies additions to the minimal possession demo to transform it from a **technical demo** into a **playable idle RPG MVP** aligned with Realms of Idle game design vision.

**Core Principle**: Preserve observer mode (passive gameplay) while adding progression hooks for active players who possess NPCs.

---

## Addition 1: NPC Hiring & Contract System

### Design Alignment

**From core-architecture.md**:

- "If you hired a new staff member, they remain employed" (line 256)
- "Business Management: Quest contracts, member salaries, equipment costs" (Guild example, line 274)

**From npc-design.md**:

- NPCs have roles: Producers, Processors, Distributors, Social
- 95% Maintainers (stable workers), 5% Ambitious (heroes/villains)

### Functional Requirements

#### FR-015: NPC Availability & Hiring

**FR-015a - Starting State**:

- Settlement starts with **1 employed NPC**: Mara (Innkeeper) at Rusty Tankard Inn
- **3 NPCs available for hire** (not employed):
  - Cook (Processor role) - standing idle at Town Square
  - Tomas (Processor - Blacksmith) - standing idle at Workshop
  - Customer (Social role) - wandering between buildings

**FR-015b - Contract Costs**:

- **Cook Contract**: 50 gold upfront, 5 gold/day wage
- **Tomas Contract**: 100 gold upfront, 10 gold/day wage
- **Customer**: Not hireable (wandering ambient NPC for atmosphere)

**FR-015c - Hiring Requirements**:

- Inn MUST have accumulated sufficient gold to cover contract cost
- Hire button shows:
  - Green + "Hire Cook (50g)" if Inn gold >= 50
  - Red + "Insufficient Funds (25/50g)" if Inn gold < 50
- Hiring deducts gold from **building's treasury**, not settlement-wide pool

**FR-015d - Wage Payment**:

- Wages deducted **daily** (every 1440 real-world minutes = 24 hours)
- If building cannot pay wage: NPC becomes "Unpaid" state
  - Unpaid NPCs work at 50% speed for 1 day grace period
  - After 1 day unpaid, NPC quits and returns to "available to hire" pool
- Wage payment notification: "Paid Cook 5g (Inn balance: 95g)"

**FR-015e - Employment Persistence**:

- Hired NPCs remain employed across page refreshes (persisted in LiteDB)
- Offline progress applies wage deductions for elapsed time
- If offline wages would bankrupt building, NPC quits (notification on return)

---

### UI Requirements

#### UI-001: Available NPCs Panel

**Location**: New panel below Activity Log (or tabbed with Activity Log)

**Contents**:

- Header: "Available for Hire"
- List of unemployed NPCs:

  ```
  [Portrait] Cook
  Role: Processor (Cooking)
  Contract: 50g upfront + 5g/day
  [Hire] button (green if affordable, red if not)
  ```

- Tooltip on hover: "Cook will produce Food for the Inn. Innkeeper uses Food to Serve Customers."

**Interaction**:

- Click [Hire] → Confirmation modal:
  - "Hire Cook for 50 gold?"
  - "Wage: 5 gold/day"
  - "Inn Balance: 120g → 70g"
  - [Confirm] [Cancel]
- On confirm:
  - Deduct gold from Inn building treasury
  - Move Cook from "Available" to Inn staff roster
  - Cook appears inside Inn building on grid
  - Cook begins autonomous work (produce Food)

---

#### UI-002: Building Treasury Display

**Update TopBar** to show building treasuries:

```
Rusty Tankard Inn: 120g | Tomas' Forge: 0g (unowned)
```

**Or create Building Info Panel** (right sidebar):

```
Rusty Tankard Inn
Owner: Mara
Balance: 120g
Staff: Mara (Innkeeper), Cook
Daily Expenses: 5g/day (Cook wage)
Income: ~15g/day (customer revenue)
```

---

#### UI-003: Wage Payment Notifications

**Add to Activity Log**:

- "[Daily] Paid Cook 5g wage (Inn: 115g)"
- "[Warning] Cannot pay Cook wage (Inn: 2g, need 5g)"
- "[Notice] Cook quit due to unpaid wages"

**Modal on Tab Return** (if NPC quit while offline):

```
Staff Changes
- Cook quit (unpaid wages for 3 days)
- Rehire Cook from Available NPCs panel
```

---

### Data Model Changes

#### Settlement Record (Updated)

```csharp
public sealed record Settlement(
    string Id,
    string Name,
    IReadOnlyList<Building> Buildings,
    IReadOnlyList<NPC> NPCs,  // All NPCs (employed + available)
    DateTime WorldTime,
    DateTime LastWagePayment   // NEW: Track daily wage cycle
);
```

#### NPC Record (Updated)

```csharp
public sealed record NPC(
    string Id,
    string Name,
    string ClassName,
    int Level,
    GridPosition Position,
    string? CurrentBuilding,  // NULL if unemployed (in Town Square)
    NPCState State,
    string? CurrentAction,
    DateTime? ActionStartTime,
    int? ActionDurationSeconds,
    int Gold,  // Personal wealth (not used in MVP, for future)
    int Reputation,
    IReadOnlyDictionary<string, object> Priorities,
    bool IsPossessed,
    NPCEmploymentStatus EmploymentStatus,  // NEW
    DateTime? HireDate                      // NEW
);

public enum NPCEmploymentStatus
{
    Available,   // Not hired, idle in Town Square
    Employed,    // Hired, working at building
    Unpaid       // Employed but wages not paid (grace period)
}
```

#### Building Record (Updated)

```csharp
public sealed record Building(
    string Id,
    string Name,
    BuildingType Type,
    GridPosition Position,
    int Width,
    int Height,
    IReadOnlyDictionary<string, int> Resources,
    int Gold,                           // NEW: Building treasury
    IReadOnlyList<string> EmployedNPCs // NEW: NPC IDs employed here
);
```

---

### Task Additions

**New Phase: Phase 9 - NPC Hiring & Wage System**

```markdown
### Phase 9: NPC Hiring & Wage System (REC-001, REC-003 combined)

**Purpose**: Add economic pressure via NPC contracts and wages, creating hiring decisions

#### Tests (TDD Required)

- [ ] T160 [P] Unit test for contract cost validation in SettlementGameService (can afford, insufficient funds)
- [ ] T161 [P] Unit test for wage payment cycle (daily deduction, grace period, quit after 1 day unpaid)
- [ ] T162 [P] Unit test for hiring NPC (gold deduction, employment status change, staff roster update)
- [ ] T163 [P] Unit test for NPC quit on unpaid wages (status change, return to available pool)
- [ ] T164 [P] bUnit test for Available NPCs panel rendering (unemployed NPCs, hire button states)
- [ ] T165 [P] bUnit test for building treasury display in TopBar or sidebar

#### Implementation

- [ ] T166 [P] Add Gold and EmployedNPCs fields to Building record in Models/Building.cs
- [ ] T167 [P] Add EmploymentStatus and HireDate fields to NPC record in Models/NPC.cs
- [ ] T168 [P] Add LastWagePayment field to Settlement record in Models/Settlement.cs
- [ ] T169 Create HiringService class in Services/HiringService.cs (validate contract cost, hire NPC, update building gold)
- [ ] T170 Create WageService class in Services/WageService.cs (daily cycle check, deduct wages, handle unpaid state)
- [ ] T171 Integrate WageService into SimulationEngine daily tick (check elapsed days, trigger wage payment)
- [ ] T172 [P] Create AvailableNPCsPanel.razor in Components/ (list unemployed NPCs, hire buttons)
- [ ] T173 [P] Create BuildingTreasuryDisplay.razor in Components/TopBar.razor or sidebar
- [ ] T174 [P] Create HireNPCModal.razor confirmation dialog in Components/
- [ ] T175 Update Settlement.CreateMillbrook factory to set initial employment states (Mara employed, others available)
- [ ] T176 Update offline progress calculation to apply wage deductions for elapsed days
- [ ] T177 Add wage payment notifications to ActivityLog (daily payments, warnings, quit events)
- [ ] T178 Add hire/fire actions to ActionCatalog (available when possessing building owner)
- [ ] T179 Update NPCAIService to handle unemployed NPCs (idle in Town Square, no work actions)
- [ ] T180 Persist employment status and building gold to LiteDB (update SaveSettlementAsync)
```

---

## Addition 2: Resource Economy & Material Consumption

### Design Alignment

**From core-architecture.md**:

- "Inventory: Items, materials" (line 142)
- Inn/Guild/Blacksmith supply chain example (lines 313-338)

**From npc-design.md**:

- Producers create materials, Processors consume materials to create goods
- Economic health = supply/demand balance

### Functional Requirements

#### FR-016: Resource Production & Consumption

**FR-016a - Resource Types**:

- **Food** (consumable) - Produced by Cook, consumed by Innkeeper serving customers
- **Iron Ore** (material) - Initial stock at Workshop, consumed by Blacksmith crafting
- **Iron Tools** (product) - Produced by Blacksmith, used by Cook (Iron Pots improve cooking speed)

**FR-016b - Initial Resource Allocation**:

- Rusty Tankard Inn: 0 Food (must hire Cook to produce)
- Tomas' Forge: 10 Iron Ore (starter stock)
- Town Square: 0 resources (unemployed NPCs have no building)

**FR-016c - Resource Consumption Rules**:

- **Serve Customer** action consumes 1 Food from Inn inventory
  - If Inn has 0 Food: Innkeeper idle, "Serve Customer" button red + tooltip "No food available"
  - Notification: "Inn out of food! Hire Cook or wait for delivery"
- **Craft Iron Sword** action consumes 2 Iron Ore from Workshop inventory
  - If Workshop has <2 Iron Ore: Tomas idle, "Craft Iron Sword" button red + tooltip "Insufficient Iron Ore (0/2)"

**FR-016d - Resource Production Rates**:

- **Cook produces Food**: 1 Food per 10 seconds (6 Food/minute)
- **Innkeeper consumes Food**: 1 Food per customer (Serve Customer = 5 seconds, consumes 1 Food)
- **Balance**: Cook slightly outpaces single Innkeeper (creates surplus for growth)
- **Tomas produces Iron Tools**: 1 Tool per 30 seconds (consumes 2 Iron Ore)

**FR-016e - Resource Capacity Limits**:

- Buildings have storage caps:
  - Inn Food Storage: 50 Food max
  - Workshop Iron Ore Storage: 100 Iron Ore max
- When storage full, production NPCs idle with notification: "Food storage full (50/50)"

**FR-016f - Offline Resource Consumption**:

- Offline progress calculation:
  1. Calculate action cycles for each NPC
  2. Check if resources available for that many cycles
  3. Cap cycles at resource depletion point
  4. Example: Inn has 20 Food, Innkeeper can serve 20 customers offline (20 cycles max)
  5. Display: "While away: Mara served 20 customers (limited by food supply)"

---

### UI Requirements

#### UI-004: Building Resource Display

**Update SettlementMap building tooltips**:

```
Rusty Tankard Inn
Staff: Mara (Innkeeper), Cook
Resources: Food 15/50, Gold 120g
Status: ✓ Operating normally
```

**Or add Resource Bar** to Building Info Panel:

```
Resources
[████████░░] Food 40/50
[██████████] Gold 120g (no cap)
```

---

#### UI-005: Resource-Blocked Action Indicators

**Update ActionPanel** to show resource requirements:

```
[Serve Customer]
Duration: 5s | Reward: +5g, +2 rep
Requires: 1 Food ✓ (15 available)

[Craft Iron Sword]
Duration: 30s | Reward: +20g
Requires: 2 Iron Ore ✗ (0 available - DISABLED)
```

**Disabled state**:

- Button greyed out
- Red border
- Tooltip: "Insufficient Iron Ore. Tomas has no ore to craft."

---

#### UI-006: Resource Production Notifications

**Add to Activity Log**:

- "[Cook] Produced 1 Food (Inn: 16/50)"
- "[Mara] Served customer, consumed 1 Food (Inn: 15/50)"
- "[Warning] Inn out of Food! Mara is idle."
- "[Tomas] Idle - Workshop out of Iron Ore"

---

### Data Model Changes

#### Building Record (Already Updated in Addition 1)

```csharp
public sealed record Building(
    string Id,
    string Name,
    BuildingType Type,
    GridPosition Position,
    int Width,
    int Height,
    IReadOnlyDictionary<string, int> Resources,  // Now actively used: {"Food": 15, "IronOre": 10}
    int Gold,
    IReadOnlyList<string> EmployedNPCs
);
```

#### NPCAction Record (Updated)

```csharp
public sealed record NPCAction(
    string Id,
    string Name,
    string Description,
    int DurationSeconds,
    IReadOnlyDictionary<string, int> ResourceCosts,   // {"Food": 1} or {"IronOre": 2}
    IReadOnlyDictionary<string, int> ResourceProduced, // NEW: {"Food": 1} or {"IronTools": 1}
    IReadOnlyDictionary<string, int> Rewards,
    IReadOnlyList<string> RequiredClasses,
    IReadOnlyList<string> RequiredBuildings
);
```

**Example Actions**:

```csharp
public static NPCAction ProduceFood => new(
    "produce_food",
    "Prepare Food",
    "Cook prepares meals for the inn",
    DurationSeconds: 10,
    ResourceCosts: new Dictionary<string, int>(),  // No cost
    ResourceProduced: new Dictionary<string, int> { { "Food", 1 } },
    Rewards: new Dictionary<string, int>(),  // No gold reward for production
    RequiredClasses: new[] { "Cook" },
    RequiredBuildings: new[] { "Inn" }
);

public static NPCAction ServeCustomer => new(
    "serve_customer",
    "Serve Customer",
    "Serve a waiting customer food and drink",
    DurationSeconds: 5,
    ResourceCosts: new Dictionary<string, int> { { "Food", 1 } },  // Consumes 1 Food
    ResourceProduced: new Dictionary<string, int>(),
    Rewards: new Dictionary<string, int> { { "Gold", 5 }, { "Reputation", 2 } },
    RequiredClasses: new[] { "Innkeeper" },
    RequiredBuildings: new[] { "Inn" }
);
```

---

### Task Additions

**Extend Phase 9 with Resource Economy**:

```markdown
#### Resource Economy (Extends Phase 9)

**Tests**:

- [ ] T181 [P] Unit test for resource consumption validation (has resources, insufficient resources)
- [ ] T182 [P] Unit test for resource production (Cook produces Food, adds to building inventory)
- [ ] T183 [P] Unit test for action blocking when resources unavailable (Serve Customer fails if no Food)
- [ ] T184 [P] Unit test for resource capacity limits (Food production stops at 50/50)
- [ ] T185 [P] Unit test for offline resource consumption (limit cycles by resource availability)

**Implementation**:

- [ ] T186 [P] Add ResourceProduced field to NPCAction record in Models/NPCAction.cs
- [ ] T187 Update ActionCatalog with resource costs and production (ServeCustomer consumes Food, ProduceFood produces Food)
- [ ] T188 Create ResourceService class in Services/ResourceService.cs (check availability, consume, produce, enforce caps)
- [ ] T189 Integrate ResourceService into SettlementGameService.ExecuteActionAsync (validate resources before action, consume on start, produce on completion)
- [ ] T190 Update NPCAIService to check resource availability before choosing action (skip actions with insufficient resources)
- [ ] T191 Update offline progress calculation (OfflineProgressCalculator) to cap cycles by resource availability
- [ ] T192 [P] Update SettlementMap building tooltips to show resource counts
- [ ] T193 [P] Update ActionPanel to show resource requirements and availability
- [ ] T194 Add resource production/consumption notifications to ActivityLog
- [ ] T195 Update Settlement.CreateMillbrook to set initial resources (Inn: 0 Food, Workshop: 10 IronOre)
```

---

## Addition 3: Building Progression & Upgrades

### Design Alignment

**From core-architecture.md**:

- "Upgrade Levels 1-5: Determines capacity, output quality, features" (line 108)
- "If you queued kitchen upgrades, they continue building" (line 257)

### Functional Requirements

#### FR-017: Building Upgrade System

**FR-017a - Upgrade Levels**:

- All buildings start at **Level 1**
- Max level: **Level 3** (MVP scope)
- Upgrade benefits:
  - **Inn Level 2** (100g cost): Food storage +50 (50→100), staff capacity +1 (allows hiring Server)
  - **Inn Level 3** (250g cost): Customer attraction +50% (more customers = more revenue)
  - **Workshop Level 2** (150g cost): Iron Ore storage +100 (100→200), crafting speed +20%
  - **Workshop Level 3** (300g cost): Unlock advanced recipes (Iron Armor)

**FR-017b - Upgrade Requirements**:

- Building treasury must have sufficient gold
- Upgrade time: 60 seconds (real-time)
- Cannot start new upgrade while one in progress
- Offline progress: Upgrade completes if elapsed time > upgrade duration

**FR-017c - Upgrade UI**:

- Building Info Panel shows "Upgrade to Level 2" button
- Tooltip: "Cost: 100g | Time: 60s | Benefit: Food storage +50"
- While upgrading: Progress bar + "Upgrading... 30s remaining"
- On completion: Notification "Inn upgraded to Level 2!"

---

### Task Additions

**New Phase: Phase 10 - Building Upgrades**

```markdown
### Phase 10: Building Upgrades (REC-003 extension)

**Tests**:

- [ ] T196 [P] Unit test for upgrade cost validation (can afford, insufficient funds)
- [ ] T197 [P] Unit test for upgrade time tracking (start, progress, completion)
- [ ] T198 [P] Unit test for upgrade benefits application (food storage increase, speed boost)
- [ ] T199 [P] bUnit test for BuildingUpgradePanel rendering (upgrade button, progress bar)

**Implementation**:

- [ ] T200 [P] Add Level and UpgradeInProgress fields to Building record in Models/Building.cs
- [ ] T201 Create BuildingUpgrade record in Models/BuildingUpgrade.cs (cost, duration, benefits)
- [ ] T202 Create UpgradeService class in Services/UpgradeService.cs (validate cost, start upgrade, apply benefits)
- [ ] T203 Integrate UpgradeService into SimulationEngine (track upgrade timers, complete on elapsed)
- [ ] T204 [P] Create BuildingUpgradePanel.razor in Components/ (upgrade button, progress bar)
- [ ] T205 Add upgrade actions to ActionCatalog (available when possessing building owner)
- [ ] T206 Update offline progress calculation to complete upgrades if elapsed time > duration
- [ ] T207 Add upgrade notifications to ActivityLog (started, completed)
```

---

## Addition 4: Observer-Friendly Tutorial System

### Design Alignment

**From core-architecture.md**:

- "Players are observers with god-like influence who can possess NPCs"
- "Observe, possess, and influence"

**User Requirement**:

- Observer mode must remain viable (no forced tutorials)
- Tutorial only triggers on **first Mara possession**, not on page load

### Functional Requirements

#### FR-018: Contextual Tutorial System

**FR-018a - Tutorial Trigger**:

- Tutorial does NOT trigger on page load (preserves observer mode)
- Tutorial triggers **only** on first possession of Mara:
  1. Player clicks Mara's portrait
  2. Player clicks "Possess" button
  3. **On possession success**, tutorial modal appears

**FR-018b - Tutorial Content** (Progressive Disclosure):

**Step 1: Possession Confirmation** (Modal 1):

```
🎭 You are now possessing Mara
You see the world through her eyes.
Try executing an action!

[Highlight: Action Panel with "Serve Customer" glowing]
[Got it] button
```

**Step 2: Action Execution** (After clicking "Serve Customer"):

```
⏱️ Action in Progress
Mara is serving a customer (5s).
You can wait or release control.

[Highlight: Release Control button]
[Got it] button
```

**Step 3: Action Complete** (After timer finishes):

```
✅ Action Complete!
+5 gold, +2 reputation
Mara earned resources while you controlled her.

Try possessing other NPCs to see different roles!
[Highlight: Other NPC portraits]
[Finish Tutorial] button
```

**FR-018c - Tutorial Persistence**:

- Tutorial completion saved to browser localStorage (key: `tutorial_completed`)
- Once completed, tutorial never shows again
- Reset via dev console: `localStorage.removeItem('tutorial_completed')`

**FR-018d - Tutorial Skipping**:

- All modals have [Skip Tutorial] button in top-right corner
- Clicking skip marks tutorial as completed (won't show again)

---

### UI Requirements

#### UI-007: Tutorial Modal System

**Design**:

- Semi-transparent overlay (backdrop-blur)
- Modal centered on screen
- Highlighted elements glow + pulsing animation
- Arrow pointing from modal to highlighted UI element

**Accessibility**:

- Modal has `role="dialog"`, `aria-modal="true"`
- Focus trap (Tab cycles within modal)
- Escape key closes modal (same as [Got it] button)

---

### Task Additions

**New Phase: Phase 11 - Tutorial System**

```markdown
### Phase 11: Observer-Friendly Tutorial (REC-002)

**Purpose**: Guide first-time players through possession mechanic WITHOUT disrupting observer mode

**Tests**:

- [ ] T208 [P] Unit test for tutorial state persistence (localStorage save/load/reset)
- [ ] T209 [P] bUnit test for TutorialModal rendering (steps, highlights, skip button)
- [ ] T210 Integration test for tutorial trigger (only on first Mara possession, not page load)

**Implementation**:

- [ ] T211 Create TutorialService class in Services/TutorialService.cs (check completion, mark complete, get next step)
- [ ] T212 [P] Create TutorialModal.razor in Components/ (modal dialog, step content, highlight system)
- [ ] T213 [P] Create tutorial-highlights.css for glowing/pulsing UI elements
- [ ] T214 Integrate TutorialService into PossessionDemo.razor (trigger on first Mara possession only)
- [ ] T215 Add localStorage persistence for tutorial completion state
- [ ] T216 Add tutorial skip functionality (button + Escape key)
- [ ] T217 Test that observer mode works without tutorial interruption (open page, don't possess, watch NPCs work)
```

---

## Addition 5: NPC Personality Traits

### Design Alignment

**From npc-design.md**:

- "95% Maintainers: Content with their role, stabilize the world"
- "5% Ambitious: Heroes/Villains who drive world events"
- Tag affinities accumulate toward unlocks

### Functional Requirements

#### FR-019: NPC Personality Traits

**FR-019a - Trait Definitions**:

- **Mara (Innkeeper)**: "Efficient: Actions 20% faster"
  - Implementation: Reduce action duration by 20% (5s → 4s)
- **Cook**: "Perfectionist: Quality +10%, Speed -10%"
  - Implementation: Increase action duration by 10% (10s → 11s), increase Food quality (future: better customer satisfaction)
- **Tomas (Blacksmith)**: "Stubborn: Ignores priority changes for 60 seconds"
  - Implementation: Priority adjustments don't apply until 60s cooldown elapsed

**FR-019b - Trait Display**:

- NPC portrait tooltip shows trait: "Mara | Innkeeper Lv.5 | ⚡ Efficient"
- Trait icon next to name in NPC Sidebar
- Trait description in hover tooltip: "⚡ Efficient: Completes actions 20% faster"

**FR-019c - Trait Effects**:

- Traits apply to **all** actions by that NPC
- Observer mode: Traits visible in autonomous behavior
- Possession mode: Player benefits from traits (Mara possessed = faster Serve Customer)

---

### Data Model Changes

#### NPC Record (Further Updated)

```csharp
public sealed record NPC(
    string Id,
    string Name,
    string ClassName,
    int Level,
    GridPosition Position,
    string? CurrentBuilding,
    NPCState State,
    string? CurrentAction,
    DateTime? ActionStartTime,
    int? ActionDurationSeconds,
    int Gold,
    int Reputation,
    IReadOnlyDictionary<string, object> Priorities,
    bool IsPossessed,
    NPCEmploymentStatus EmploymentStatus,
    DateTime? HireDate,
    IReadOnlyList<NPCTrait> Traits  // NEW
);

public sealed record NPCTrait(
    string Id,        // "efficient", "perfectionist", "stubborn"
    string Name,      // "Efficient", "Perfectionist", "Stubborn"
    string Icon,      // "⚡", "✨", "🛡️"
    string Description, // "Completes actions 20% faster"
    TraitEffect Effect  // { Type: "ActionSpeed", Modifier: 1.2 }
);

public sealed record TraitEffect(
    string Type,      // "ActionSpeed", "Quality", "PriorityCooldown"
    double Modifier   // 1.2 (20% faster), 0.9 (10% slower), 60 (seconds)
);
```

---

### Task Additions

**Extend Phase 8 (Polish) with Traits**:

```markdown
#### NPC Personality Traits (REC-004)

**Tests**:

- [ ] T218 [P] Unit test for trait effect application (Efficient reduces action duration by 20%)
- [ ] T219 [P] Unit test for Stubborn trait (priority changes delayed by 60s)
- [ ] T220 [P] bUnit test for trait display in NPC portrait tooltips

**Implementation**:

- [ ] T221 [P] Create NPCTrait and TraitEffect records in Models/NPC.cs
- [ ] T222 Create TraitService class in Services/TraitService.cs (apply trait effects to actions, check cooldowns)
- [ ] T223 Update SettlementGameService.ExecuteActionAsync to apply trait modifiers (speed, quality)
- [ ] T224 Update Settlement.CreateMillbrook to assign initial traits to NPCs
- [ ] T225 [P] Update NPCSidebar to display trait icons and tooltips
- [ ] T226 Add trait effect descriptions to action tooltips (e.g., "5s → 4s (Efficient)")
```

---

## Addition 6: Performance & Playtesting Validation

### Functional Requirements

#### FR-020: Performance Benchmarks

**FR-020a - Game Loop Performance**:

- Target: 10 ticks/second with <5% variance
- Test: Run simulation for 10 minutes, measure tick rate every second
- Failure condition: Tick rate drops below 9.5 ticks/sec or exceeds 10.5 ticks/sec

**FR-020b - Memory Leak Detection**:

- Target: <10MB heap growth over 10 minutes
- Test: Load page, wait 10 minutes, measure heap size increase
- Failure condition: Heap grows >10MB (indicates memory leak)

**FR-020c - Cross-Browser Compatibility**:

- Target: User Stories 1-5 pass on Chrome, Firefox, Edge
- Test: Run Playwright E2E tests on all 3 browsers
- Failure condition: Any test fails on any browser

---

#### FR-021: Playtesting Validation

**FR-021a - Session Length Metric**:

- Target: Average session length >5 minutes
- Test: 5 users, measure time from page load to tab close
- Failure condition: Average <5 minutes

**FR-021b - Comprehension Survey**:

- Target: 80% of users understand possession mechanic
- Test: Post-play survey question: "Did you understand how to control NPCs?"
- Failure condition: <4 out of 5 users answer "Yes"

**FR-021c - Engagement Survey**:

- Target: 60% would play for 30 minutes
- Test: Survey question: "Would you play this for 30 minutes?"
- Failure condition: <3 out of 5 users answer "Yes"

---

### Task Additions

**New Phase: Phase 12 - Performance & Validation**

```markdown
### Phase 12: Performance & Playtesting (REC-006)

**Purpose**: Validate technical performance and player engagement before MVP release

**Performance Tests**:

- [ ] T227 [P] Performance test: Game loop tick rate stability (10 min run, assert <5% variance)
- [ ] T228 [P] Performance test: Memory leak detection (10 min run, assert <10MB heap growth)
- [ ] T229 [P] Performance test: CPU usage (assert <20% CPU on mid-range device)
- [ ] T230 Cross-browser E2E: Run US1-US5 tests on Chrome (assert all pass)
- [ ] T231 Cross-browser E2E: Run US1-US5 tests on Firefox (assert all pass)
- [ ] T232 Cross-browser E2E: Run US1-US5 tests on Edge (assert all pass)

**Playtesting**:

- [ ] T233 Manual playtest: Recruit 5 users, record session length (target: avg >5 min)
- [ ] T234 Manual playtest: Survey comprehension (target: 80% understand possession)
- [ ] T235 Manual playtest: Survey engagement (target: 60% would play 30 min)
- [ ] T236 Analyze heatmap clicks (identify confusing UI areas)
- [ ] T237 Document playtest findings in playtest-results.md

**Implementation** (if tests reveal issues):

- [ ] T238 Fix performance regressions identified in T227-T229
- [ ] T239 Fix cross-browser compatibility issues from T230-T232
- [ ] T240 Improve UX based on playtest feedback from T233-T237
```

---

## Summary: Updated Task Count

**Original MVP**: 170 tasks
**Option B Additions**: 80 new tasks
**Total**: **250 tasks**

**Breakdown**:

- Phase 9: NPC Hiring & Wage System (20 tasks: T160-T179)
- Phase 9: Resource Economy (15 tasks: T180-T195)
- Phase 10: Building Upgrades (12 tasks: T196-T207)
- Phase 11: Tutorial System (10 tasks: T208-T217)
- Phase 8: NPC Personality Traits (9 tasks: T218-T226)
- Phase 12: Performance & Validation (14 tasks: T227-T240)

**Estimated Effort**: +5-7 days (as originally predicted for Option B)

**Parallel Opportunities**: 45 of the new tasks marked [P] can run in parallel

---

## Alignment Verification

✅ **Observer Mode Preserved**: Tutorial only on first Mara possession, not page load
✅ **NPC Hiring (Not Unlocking)**: Cook available to hire for contract cost, not "level unlocked"
✅ **Economic Pressure**: Wages create decisions (hire Cook now or wait?), resource consumption creates scarcity
✅ **Settlement Health Progression**: Building upgrades + employed staff count = settlement health metric
✅ **Living World**: Autonomous NPCs continue working, wages deducted offline, resources consumed
✅ **Favorite NPCs Status**: Hired NPCs are player's responsibility (must pay wages), personality traits make them interesting

---

## Next Steps

1. **Review this document** - Does it align with your vision?
2. **Approve or modify** - Any adjustments needed?
3. **Integrate into spec.md** - Add FR-015 through FR-021
4. **Integrate into tasks.md** - Add Phases 9-12 (T160-T240)
5. **Update plan.md** - Add new phases to implementation timeline
6. **Update data-model.md** - Add updated Building/NPC/Settlement schemas

Ready to proceed with integration?
