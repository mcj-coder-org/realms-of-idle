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

#### NFR-002: Accessibility & WCAG 2.1 AA Compliance

**NFR-002a - Keyboard Navigation**:

- ALL interactive elements MUST be keyboard accessible (no mouse-only interactions)
- Tab order MUST follow logical reading order: Top Bar → NPC Sidebar → Settlement Map → Action Panel → Activity Log
- Focus indicators MUST be visible with minimum 3:1 contrast ratio against background
- Keyboard shortcuts MUST be provided:
  - `P` key: Possess focused NPC (when NPC portrait has focus)
  - `Escape` key: Release possession and return to observer mode
  - `Space` or `Enter`: Execute focused action
  - `Arrow keys`: Navigate between NPC portraits in sidebar
  - `Tab` / `Shift+Tab`: Navigate between interactive regions
- Focus MUST be managed on state changes:
  - On possession: Focus moves to first action button in Action Panel
  - On release: Focus returns to previously possessed NPC portrait
  - On modal display: Focus moves to modal, focus trap active within modal
- All keyboard shortcuts MUST be documented in help text or tooltips

**NFR-002b - Screen Reader Support (ARIA)**:

- All interactive components MUST have ARIA labels:
  - NPC portraits: `aria-label="Mara, Innkeeper, Level 5, 100 gold, currently idle. Press P to possess."`
  - Action buttons: `aria-label="Serve Customer. Duration: 5 seconds. Reward: +5 gold, +2 reputation."`
  - Release button: `aria-label="Release control of Mara and return to observer mode"`
  - Activity log: `aria-label="Activity log showing last 20 NPC actions"`
- ARIA roles MUST be assigned to custom components:
  - Settlement grid: `role="grid"`, `aria-label="10x10 settlement map with 2 buildings and 4 NPCs"`
  - NPC sidebar: `role="list"`, `aria-label="NPC roster"`
  - NPC portrait: `role="listitem"` and `role="button"`
  - Action panel: `role="toolbar"`, `aria-label="Available actions"`
- ARIA live regions MUST announce dynamic content:
  - Activity log: `aria-live="polite"`, `aria-atomic="false"` (announces new entries)
  - Timer countdown: `aria-live="off"` (do NOT announce every tick, too verbose)
  - Action completion: `aria-live="assertive"` (announce immediately on completion)
  - Offline progress modal: `aria-live="polite"`, `role="dialog"`, `aria-modal="true"`
- ARIA states MUST reflect current UI state:
  - Possessed NPC portrait: `aria-pressed="true"`, `aria-current="true"`
  - Disabled actions: `aria-disabled="true"` with `aria-label` explaining why (e.g., "Craft Sword - disabled, insufficient Iron Ore")
  - Loading states: `aria-busy="true"` during initialization
- Landmarks MUST be defined:
  - Top bar: `role="banner"`
  - NPC sidebar: `role="complementary"`, `aria-label="NPC roster"`
  - Settlement map: `role="main"`, `aria-label="Settlement view"`
  - Action panel: `role="complementary"`, `aria-label="Actions"`
  - Activity log: `role="complementary"`, `aria-label="Activity log"`

**NFR-002c - Visual Accessibility (WCAG 2.1 AA)**:

- Color contrast MUST meet WCAG AA standards:
  - Normal text (< 18pt): Minimum 4.5:1 contrast ratio
  - Large text (≥ 18pt or 14pt bold): Minimum 3:1 contrast ratio
  - Interactive elements: Minimum 3:1 contrast ratio for borders/icons
  - Focus indicators: Minimum 3:1 contrast ratio against adjacent colors
- Color MUST NOT be the only indicator of state:
  - Possessed state: Border color + icon + text label ("POSSESSED")
  - Disabled actions: Greyed out + strikethrough + disabled cursor + tooltip
  - Action completion: Color + checkmark icon + text ("Complete!")
- Text sizing MUST support:
  - Browser zoom up to 200% without horizontal scrolling
  - Minimum base font size: 14px for body text
  - Relative units (rem/em) for all font sizes (not px)
- Reduced motion MUST be respected:
  - Detect `prefers-reduced-motion: reduce` media query
  - IF reduced motion preferred THEN disable:
    - NPC portrait animations on possession
    - Action timer countdown animations
    - Transition effects on modal open/close
  - Keep essential animations: Timer progress bar (slowed), critical state changes
- Focus indicators MUST be:
  - Visible and clearly distinguishable (2px solid outline minimum)
  - Maintained when navigating with keyboard
  - Not hidden by CSS (`outline: none` prohibited without alternative)

**NFR-002d - Semantic HTML**:

- Buttons MUST use `<button>` elements (not `<div>` with click handlers)
- Headings MUST follow logical hierarchy:
  - Page title: `<h1>Millbrook Settlement</h1>`
  - Section headings: `<h2>NPCs</h2>`, `<h2>Actions</h2>`
  - Subsection headings: `<h3>` for NPC names, action categories
- Lists MUST use proper list elements:
  - NPC roster: `<ul>` with `<li>` for each NPC
  - Action buttons: `<ul role="toolbar">` with `<li role="none">` wrappers
  - Activity log: `<ol>` (ordered by time) with `<li>` for each entry
- Form elements MUST have explicit labels:
  - Priority sliders: `<label for="quality-slider">Quality Priority</label>`
  - All inputs associated with `<label>` via `for` attribute or wrapping
- Images MUST have alt text:
  - NPC portraits: `alt="Portrait of Mara, Innkeeper"`
  - Building icons: `alt="Inn building icon"`
  - Decorative images: `alt=""` (empty string, not missing)

**NFR-002e - Error Messages & Feedback**:

- Error messages MUST be:
  - Announced to screen readers via `aria-live="assertive"` or `role="alert"`
  - Visible with clear visual distinction (color + icon, not color alone)
  - Actionable with specific guidance: "Insufficient materials: need 2 Iron Ore. Visit the mine to gather more."
  - Persistent until user acknowledges or error resolves
- Success feedback MUST be provided:
  - Action completion: Visual + audible (optional) + screen reader announcement
  - State changes: Possession acquired/released announced to screen reader
  - Offline progress: Modal summary + screen reader announcement of total rewards
- Loading states MUST indicate progress:
  - Initial load: Spinner + text "Loading settlement..." + `aria-busy="true"`
  - Long actions: Progress bar + remaining time + screen reader updates (sparingly)
- Timeout warnings MUST be announced:
  - 30 seconds before auto-dismiss: "Modal will close in 30 seconds" (screen reader + visual)

**NFR-002f - Responsive Design Constraints**:

- Desktop-only UI (per Technical Context) documented as accessibility limitation
- Minimum supported resolution: 1280x720 (HD)
- Browser zoom MUST support 100%-200% without:
  - Horizontal scrolling
  - Content overlap
  - Loss of functionality
- Text reflow MUST work at 200% zoom:
  - Multi-line wrapping for long NPC names or action descriptions
  - Buttons remain clickable and readable

**NFR-002g - Cognitive Accessibility**:

- Interaction patterns MUST be consistent:
  - All action buttons follow same visual style and behavior
  - Possession flow identical for all NPCs
  - Modal dismiss behavior consistent (Escape, X button, auto-dismiss)
- UI affordances MUST be clear:
  - Buttons appear raised/clickable (visual depth cues)
  - Interactive elements change cursor to pointer
  - Disabled elements clearly distinguished (not just lower opacity)
- Language MUST be simple and clear:
  - Avoid jargon: Use "Serve Customer" not "Execute Service Transaction"
  - Action descriptions concise: "5 seconds, +5 gold" not "This action takes 5 seconds to complete and grants 5 gold upon successful completion"
  - Error messages in plain language
- Help/tooltips MUST be available:
  - Hover or focus on action buttons shows tooltip with requirements
  - First-time user sees brief tutorial or help text
  - Keyboard shortcuts documented in accessible location

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
