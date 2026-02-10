# Feature Specification: Minimal Possession Demo v1

**Feature Branch**: `001-minimal-possession-demo`
**Created**: 2026-02-09
**Status**: Draft
**Input**: Demonstration of core possession mechanics in a living world simulation

## User Scenarios & Testing _(mandatory)_

### User Story 1 - Observer Mode (Priority: P1)

Experience the living world simulation running autonomously before any player interaction.

**Why this priority**: Core concept validation - users must see that NPCs have their own lives and the world runs without player input.

**Independent Test**: Open the web app, watch for 30 seconds without clicking anything. NPCs perform actions, gold increases, activity log updates. World is alive.

**Acceptance Scenarios**:

1. **Given** I open the web app, **When** the settlement loads, **Then** I see 2 buildings (Inn, Workshop) and 4 NPCs on a 10x10 grid
2. **Given** I watch in observer mode, **When** 10 seconds pass, **Then** activity log shows at least 2 NPC actions ("Mara served Customer", "Tomas crafted Iron Sword")
3. **Given** I'm observing Mara, **When** she completes serving a customer, **Then** her gold counter increases by 5

---

### User Story 2 - Possess and Control NPC (Priority: P1)

Take direct control of an NPC to experience their role and perform actions manually.

**Why this priority**: Core mechanic - possession is the fundamental interaction model.

**Independent Test**: Click Mara's portrait → click "Possess" → see action panel with inn-specific actions → execute "Serve Customer" → watch timer count down → see result (+5 gold).

**Acceptance Scenarios**:

1. **Given** I click Mara's portrait in sidebar, **When** I click "Possess", **Then** view changes to "Possessed: Mara [Innkeeper] Lvl 5" and action panel appears
2. **Given** I'm possessing Mara at the Inn, **When** I view available actions, **Then** I see "Serve Customer", "Manage Cook", "Check Income", "Release Control"
3. **Given** I click "Serve Customer", **When** the 5-second timer completes, **Then** I see "+5 gold, +2 reputation" and Mara's gold increases from 100 to 105
4. **Given** I click "Release Control", **When** I return to observer mode, **Then** Mara continues serving customers autonomously

---

### User Story 3 - Context-Aware Actions (Priority: P2)

Experience how available actions change based on NPC class and current building location.

**Why this priority**: Demonstrates the "scenarios as emergent gameplay" concept - same world, different role = different experience.

**Independent Test**: Possess Mara at Inn (see innkeeper actions) → release → possess Tomas at Workshop (see blacksmith actions) → understand context dependency.

**Acceptance Scenarios**:

1. **Given** I possess Tomas at Tomas' Forge, **When** I view available actions, **Then** I see "Craft Iron Sword", "Check Materials", "Set Priority", "Release Control"
2. **Given** I possess Tomas, **When** I click "Craft Iron Sword", **Then** timer starts at 30 seconds and completes with "Iron Sword crafted! Worth 20 gold"
3. **Given** I possess blacksmith Tomas at the Inn, **When** I view available actions, **Then** blacksmith actions are NOT available (only customer actions like "Order Drink")

---

### User Story 4 - Persistent Priority Changes (Priority: P2)

Adjust NPC priorities while possessed and confirm changes persist after releasing control.

**Why this priority**: Demonstrates lasting influence - you're guiding NPCs, not just puppeting them temporarily.

**Independent Test**: Possess Mara → click "Manage Cook" → adjust priority slider to 80% Quality → release → observe Cook's behavior follows new priority.

**Acceptance Scenarios**:

1. **Given** I possess Mara, **When** I click "Manage Cook", **Then** priority panel opens showing current balance (50/50 Speed vs Quality)
2. **Given** I adjust Cook's priority to 80% Quality, **When** I click "Apply", **Then** I see confirmation "Cook priorities updated. Changes persist after release."
3. **Given** I release possession of Mara after changing Cook's priority, **When** I observe Cook working, **Then** Cook's actions reflect the new priority (takes longer but produces higher quality)

---

### User Story 5 - Favorites System (Priority: P3)

Bookmark frequently-used NPCs for quick access and receive notifications about their activities.

**Why this priority**: Quality of life feature that demonstrates multi-NPC management pattern.

**Independent Test**: Click star icon on Mara → she appears in Favorites panel → quick-possess from favorites → notification badge appears when she completes action while not possessed.

**Acceptance Scenarios**:

1. **Given** I click the star icon (⭐) on Mara's portrait, **When** she's added to favorites, **Then** Favorites panel shows "⭐ Mara"
2. **Given** Mara is favorited, **When** I click her name in Favorites panel, **Then** I immediately possess her (skips portrait click step)
3. **Given** Mara is favorited and not possessed, **When** she completes an action, **Then** notification badge appears on her Favorites entry

---

### Edge Cases

- What happens when **NPC is already working** and you try to start another action? → Action button disabled until current action completes
- What happens when **no materials available** for crafting? → "Craft Iron Sword" shows red with tooltip "Insufficient materials: need 2 Iron Ore"
- What happens when **you possess NPC mid-action**? → Timer continues, you see progress bar with remaining time
- What happens when **you release possession mid-action**? → Action continues autonomously, completes as scheduled
- What happens when **multiple NPCs need the same resource simultaneously**? → First-come-first-served (for MVP, no resource contention)

### Offline Progress Edge Cases

- **What happens when tab hidden for <60 seconds?** → No offline calculation, treat as continuous play, no modal displayed
- **What happens when tab hidden for >24 hours?** → Calculate only 24 hours of progress (cap to prevent overflow), modal shows "24+ hours"
- **What happens if NPC was possessed when tab hidden?** → Clear possession state, NPC does NOT earn offline rewards, resumes as autonomous
- **What happens if NPC was mid-action when tab hidden?** → Action cancelled, NPC returns to Idle state, offline calculation starts from Idle
- **What happens if offline progress calculation fails?** → Load last saved state, display warning, continue without catch-up
- **What happens if player quickly switches tabs repeatedly (<60s each)?** → No offline calculation triggered, continuous gameplay
- **What happens if LiteDB is corrupted when loading offline state?** → Trigger recovery flow (see NFR-001a), offer reset or read-only mode
- **What happens if Settlement.CreateMillbrook() fails during recovery?** → Display error, provide "Retry" and "Contact Support" options

## Requirements _(mandatory)_

### Functional Requirements

- **FR-001**: System MUST display a 10x10 tile grid with 2 buildings (Inn, Workshop) and 4 NPCs
- **FR-002**: System MUST run game loop at 10 ticks/second advancing simulation time
- **FR-003**: NPCs MUST autonomously select and perform actions when not possessed
- **FR-004**: System MUST allow player to possess any NPC by clicking portrait + "Possess" button
- **FR-005**: System MUST display context-appropriate actions based on NPC class and current building
- **FR-006**: Actions MUST have execution timers (5-30 seconds) with visible countdown
- **FR-007**: Action completion MUST grant rewards (gold, reputation) and update UI
- **FR-008**: System MUST allow releasing possession, returning to observer mode
- **FR-009**: NPC priority changes MUST persist after possession release
- **FR-010**: System MUST maintain activity log showing last 20 NPC actions
- **FR-011**: System MUST support favorites list for bookmarking NPCs
- **FR-012**: System MUST show notification badges for favorited NPC activity

### FR-013: Game Loop Lifecycle Management

**FR-013a - Initialization**:

- SimulationEngine MUST initialize timer on PossessionDemo.OnInitializedAsync
- Timer interval MUST be set to 100ms (10 ticks/second)
- First tick MUST occur within 100-200ms of Start() call
- Initialization failure MUST log error and display user-facing message: "Game loop failed to start"

**FR-013b - Offline Progress on Tab Visibility Loss**:

- When browser tab loses visibility (document.hidden == true):
  - Game loop timer MUST be stopped to conserve resources
  - WorldTime snapshot MUST be persisted to LiteDB
  - Last known Settlement state MUST be persisted to LiteDB
- When tab regains visibility (document.hidden == false):
  - Calculate elapsed time: CurrentTime - LastWorldTime
  - Apply offline progress calculation for elapsed time (see FR-014)
  - Update all NPC states as if simulation continued autonomously
  - Generate activity log summary entry
  - Resume normal game loop operation within 100ms

**FR-013c - Disposal on Component Unmount**:

- SimulationEngine.Dispose() MUST be called in PossessionDemo.OnDispose
- Timer MUST be stopped and disposed before component destruction
- Final Settlement state MUST be persisted before disposal
- No timer events MUST fire after Dispose() called
- Failure to dispose MUST NOT leave orphaned timers or memory leaks

### FR-014: Offline Progress Calculation

**FR-014a - Calculation Algorithm**:

- Input: Settlement state at tab hidden, elapsed time in seconds
- For each NPC in Settlement:
  - IF NPC.IsPossessed == true THEN:
    - Clear possession state (set IsPossessed = false, State = Idle)
    - Do NOT calculate offline progress for possessed NPCs
  - IF NPC.IsPossessed == false THEN:
    - Determine primary autonomous action for NPC class
    - Calculate action cycles: floor(elapsedSeconds / action.DurationSeconds)
    - Apply rewards: Gold += (cycleCount × action.Rewards.Gold)
    - Apply reputation: Reputation += (cycleCount × action.Rewards.Reputation)
    - Update NPC state to Idle (not mid-action)
- Generate activity log entry: "Offline progress: {totalActions} actions, +{totalGold} gold"

**FR-014b - Offline Progress Limits**:

- Minimum offline time to trigger calculation: 60 seconds
- Maximum offline time considered: 24 hours (86400 seconds)
- IF elapsed time < 60 seconds THEN treat as continuous play (no catch-up)
- IF elapsed time > 24 hours THEN cap calculation at 24 hours
- Resource consumption: NPCs do NOT consume building resources during offline calculation (MVP simplification)

**FR-014c - Offline Progress Display**:

- Show modal on tab regain visibility (if elapsed >= 60 seconds)
- Modal MUST appear within 500ms of visibility change
- Display summary:
  - Time elapsed: "X hours Y minutes" or "24+ hours" if capped
  - Actions completed per NPC: "Mara: 54 customers served, Tomas: 12 swords crafted"
  - Total rewards: "+270 gold, +108 reputation"
- "Continue" button closes modal and resumes normal gameplay
- Modal MUST NOT block simulation (game loop running in background)
- Modal auto-dismisses after 30 seconds if user does not interact

### Non-Functional Requirements

#### NFR-001: Exception Handling & Recovery

**NFR-001a - LiteDB Corruption Recovery**:

- IF LiteDB file is corrupted on connection open THEN:
  1. Log error with corruption details
  2. Display user message: "Settlement data corrupted. Reset to default?"
  3. IF user confirms THEN delete database file and reinitialize with Settlement.CreateMillbrook()
  4. IF user cancels THEN run in read-only mode (no persistence, in-memory only)
- Corruption detection: Catch LiteException on database connection

**NFR-001b - Settlement Initialization Failures**:

- IF Settlement.CreateMillbrook() throws exception THEN:
  1. Log stack trace with full exception details
  2. Display error message: "Failed to initialize settlement. Please refresh page."
  3. Disable all UI interactions except "Refresh Page" button
  4. Do NOT start SimulationEngine
- Validation failures (e.g., invalid GridPosition) MUST throw InvalidOperationException with descriptive message

**NFR-001c - Timer Initialization Failures**:

- IF SimulationEngine.Start() fails THEN:
  1. Log exception details
  2. Display warning: "Game loop failed to start. Settlement may not update."
  3. Retry Start() after 5 seconds (max 3 attempts with exponential backoff)
  4. After 3 failures, display: "Game loop unavailable. Manual refresh required."

**NFR-001d - Offline Progress Calculation Failures**:

- IF OfflineProgressCalculator throws exception THEN:
  1. Log error: "Offline progress calculation failed: {exception}"
  2. Fall back to loading last saved Settlement state (no catch-up)
  3. Display warning: "Could not calculate offline progress. Loaded last saved state."
  4. Continue normal gameplay without modal

### FR-015: NPC Availability & Hiring

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

### FR-016: Resource Production & Consumption

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

### FR-017: Building Upgrade System

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

### FR-018: Contextual Tutorial System

**FR-018a - Tutorial Trigger**:

- Tutorial does NOT trigger on page load (preserves observer mode)
- Tutorial triggers **only** on first possession of Mara:
  1. Player clicks Mara's portrait
  2. Player clicks "Possess" button
  3. **On possession success**, tutorial modal appears

**FR-018b - Tutorial Content** (Progressive Disclosure):

**Step 1: Possession Confirmation** (Modal 1):

- Message: "🎭 You are now possessing Mara. You see the world through her eyes. Try executing an action!"
- Highlight: Action Panel with "Serve Customer" glowing
- Button: [Got it]

**Step 2: Action Execution** (After clicking "Serve Customer"):

- Message: "⏱️ Action in Progress. Mara is serving a customer (5s). You can wait or release control."
- Highlight: Release Control button
- Button: [Got it]

**Step 3: Action Complete** (After timer finishes):

- Message: "✅ Action Complete! +5 gold, +2 reputation. Mara earned resources while you controlled her. Try possessing other NPCs to see different roles!"
- Highlight: Other NPC portraits
- Button: [Finish Tutorial]

**FR-018c - Tutorial Persistence**:

- Tutorial completion saved to browser localStorage (key: `tutorial_completed`)
- Once completed, tutorial never shows again
- Reset via dev console: `localStorage.removeItem('tutorial_completed')`

**FR-018d - Tutorial Skipping**:

- All modals have [Skip Tutorial] button in top-right corner
- Clicking skip marks tutorial as completed (won't show again)

---

### FR-019: NPC Personality Traits

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

### FR-020: Performance Benchmarks

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

### FR-021: Playtesting Validation

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

#### NFR-002: Test Automation Hooks & Basic Accessibility

**Purpose**: Provide ARIA labels and semantic markup primarily to enable automated UI testing frameworks (Playwright, Selenium, etc.) rather than full accessibility compliance for disabled users. This is a visual mobile game requiring sight and manual dexterity.

**NFR-002a - ARIA Labels for Test Automation**:

- Interactive components SHOULD have `aria-label` attributes for reliable test selectors:
  - NPC portraits: `aria-label="Mara, Innkeeper, Level 5"` (enables `getByLabel()` queries)
  - Action buttons: `aria-label="Serve Customer"` (distinct from visual text which may change)
  - Settlement map: `aria-label="Settlement map"` (identifies component in test assertions)
- ARIA labels prioritize test stability over verbosity (no need for lengthy descriptions)
- Components without ARIA labels MUST have `data-testid` attributes as fallback

**NFR-002b - Semantic HTML for Component Identification**:

- Buttons SHOULD use `<button>` elements (improves test reliability vs `<div>` click handlers)
- Lists SHOULD use `<ul>`/`<ol>` elements (test frameworks can query list structure)
- Forms SHOULD use `<label>` elements (enables form validation tests)
- Headings SHOULD follow hierarchy (h1 → h2 → h3) for landmark navigation in tests

**NFR-002c - ARIA Roles for Component Hierarchy**:

- Custom components SHOULD declare roles for test framework navigation:
  - Settlement grid: `role="grid"` (enables grid cell selection in tests)
  - Action panel: `role="toolbar"` (groups related actions for batch assertions)
  - Modals: `role="dialog"`, `aria-modal="true"` (enables modal detection in tests)
- Roles provide test framework hints, not full screen reader support

**NFR-002d - ARIA States for Test Assertions**:

- Interactive elements SHOULD expose state via ARIA attributes:
  - Possessed NPC: `aria-pressed="true"` (test can assert possession state)
  - Disabled actions: `aria-disabled="true"` (test can verify action unavailable)
  - Loading states: `aria-busy="true"` (test can wait for load completion)
- States enable test frameworks to assert UI state without parsing CSS classes

**NFR-002e - Test-Friendly Error Handling**:

- Error messages SHOULD have `role="alert"` for test framework detection
- Actionable errors SHOULD include guidance text for test validation
- Errors SHOULD use `aria-live="assertive"` to trigger test framework listeners

**NFR-002f - Known Limitations & Resolution Support**:

- **Resolution Support**: Mobile devices (phones, tablets) through ultra widescreen desktop
  - Minimum: 360x640 (mobile portrait)
  - Typical ranges: 360x640 (phone), 768x1024 (tablet), 1920x1080 (desktop), 3440x1440 (ultrawide)
  - Responsive design MUST adapt layout for mobile, tablet, and desktop breakpoints
- **High Pixel Density Support**: UI MUST render correctly on high DPI displays
  - Pixel density ranges: 1x (standard), 2x (Retina/high DPI), 3x (high-end mobile), 4x (premium mobile)
  - Graphics and UI elements MUST scale appropriately without pixelation
  - Use vector graphics (SVG) or provide multiple resolution assets where appropriate
- **Accessibility Limitations**: Requires vision and manual dexterity (NOT accessible to blind/motor-impaired users)
- No keyboard-only navigation required (mouse/touch expected)
- No screen reader optimization required (ARIA primarily for test automation)
- No color contrast requirements (visual aesthetics prioritized)
- No text-only browser support required

**Rationale**: This approach provides test automation benefits (stable selectors, state assertions, component hierarchy) without the development overhead of full WCAG 2.1 AA compliance. ARIA attributes serve dual purpose: minor accessibility improvements at no extra cost, primary benefit to automated testing.

### Key Entities _(include if feature involves data)_

- **Settlement**: Represents Millbrook with buildings and NPCs, tracks world time
- **Building**: Physical structure (Inn, Workshop) with position, type, and resources
- **NPC**: Character with name, class, level, position, state, gold, reputation, priorities
- **NPCAction**: Executable action with duration, resource costs, rewards, class/building requirements
- **ActivityLogEntry**: Record of NPC action with timestamp, actor, action type, result

## Success Criteria _(mandatory)_

### Measurable Outcomes

- **SC-001**: User can identify that NPCs work autonomously within 30 seconds of observing
- **SC-002**: User can successfully possess an NPC and execute one action within 60 seconds
- **SC-003**: User can identify that innkeeper vs blacksmith actions feel distinct within 2 minutes
- **SC-004**: 100% of playtesters understand "you control NPCs, not yourself" concept after one possession
- **SC-005**: Game loop maintains 10 ticks/second without visible lag (60 FPS UI updates)
- **SC-006**: Action execution feels responsive (<50ms from click to timer start)
- **SC-007**: User finds demo "interesting enough to watch for 5+ minutes" (playtesting metric)
- **SC-008**: Game loop starts within 200ms of page load and maintains 10 ticks/second (±5% variance acceptable)
- **SC-009**: Offline progress correctly calculates and applies rewards for time away (verified with 5min, 1hr, and 24hr offline tests showing expected gold totals ±1%)
- **SC-010**: Offline progress modal displays within 500ms of tab regaining visibility
- **SC-011**: Component disposal completes without memory leaks (verified with browser DevTools Memory Profiler - heap size returns to baseline after component unmount)
- **SC-012**: Corrupted database triggers recovery flow without application crash (manual corruption test passes)
- **SC-013**: Settlement initialization failure displays user-friendly error message without exposing stack traces
- **SC-014**: All exception scenarios log detailed diagnostics to browser console for debugging
