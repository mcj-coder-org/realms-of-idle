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

**NFR-002f - Known Limitations (Not Requirements)**:

- Desktop/mobile game - requires vision and manual dexterity (NOT accessible to blind/motor-impaired users)
- No keyboard-only navigation required (mouse/touch expected)
- No screen reader optimization required (ARIA primarily for test automation)
- No color contrast requirements (visual aesthetics prioritized)
- No text-only browser support required
- Minimum resolution: 1280x720 (smaller devices may have degraded UX)

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
