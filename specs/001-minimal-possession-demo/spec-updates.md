# Spec.md Updates - Copy-Paste Ready

## Add to § Functional Requirements (after FR-012)

---

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

---

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

---

## Add to § Non-Functional Requirements (new section or after existing NFRs)

---

### NFR-001: Exception Handling & Recovery

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

---

## Update § Success Criteria (replace SC-008, SC-009, SC-010, add new)

---

**SC-008**: Game loop starts within 200ms of page load and maintains 10 ticks/second (±5% variance acceptable)

**SC-009**: Offline progress correctly calculates and applies rewards for time away (verified with 5min, 1hr, and 24hr offline tests showing expected gold totals ±1%)

**SC-010**: Offline progress modal displays within 500ms of tab regaining visibility

**SC-011**: Component disposal completes without memory leaks (verified with browser DevTools Memory Profiler - heap size returns to baseline after component unmount)

**SC-012**: Corrupted database triggers recovery flow without application crash (manual corruption test passes)

**SC-013**: Settlement initialization failure displays user-friendly error message without exposing stack traces

**SC-014**: All exception scenarios log detailed diagnostics to browser console for debugging

---

## Update § Edge Cases (add to existing edge cases)

---

### Offline Progress Edge Cases

- **What happens when tab hidden for <60 seconds?** → No offline calculation, treat as continuous play, no modal displayed
- **What happens when tab hidden for >24 hours?** → Calculate only 24 hours of progress (cap to prevent overflow), modal shows "24+ hours"
- **What happens if NPC was possessed when tab hidden?** → Clear possession state, NPC does NOT earn offline rewards, resumes as autonomous
- **What happens if NPC was mid-action when tab hidden?** → Action cancelled, NPC returns to Idle state, offline calculation starts from Idle
- **What happens if offline progress calculation fails?** → Load last saved state, display warning, continue without catch-up
- **What happens if player quickly switches tabs repeatedly (<60s each)?** → No offline calculation triggered, continuous gameplay
- **What happens if LiteDB is corrupted when loading offline state?** → Trigger recovery flow (see NFR-001a), offer reset or read-only mode
- **What happens if Settlement.CreateMillbrook() fails during recovery?** → Display error, provide "Retry" and "Contact Support" options

---
