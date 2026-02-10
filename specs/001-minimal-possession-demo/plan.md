# Implementation Plan: Minimal Possession Demo v1

**Branch**: `001-minimal-possession-demo` | **Date**: 2026-02-09 | **Spec**: [spec.md](./spec.md)
**Input**: Feature specification from `/specs/001-minimal-possession-demo/spec.md`

**Note**: This plan follows the Speckit workflow. Technical details extracted from design document at `docs/plans/2026-02-09-minimal-possession-demo.md`.

## Summary

Build a minimal playable web application (Blazor WASM) demonstrating core possession mechanics: a living world simulation where NPCs work autonomously, players can possess any NPC to take control, perform context-aware actions, and release control to watch NPCs continue independently. Validates the architecture documented in `docs/design/core-architecture.md`.

**Key Deliverables:**

- Blazor WASM app with 1 settlement (Millbrook), 2 buildings (Inn, Workshop), 4 NPCs
- Game loop running at 10 ticks/second with autonomous NPC behavior
- Possession system: click NPC → take control → execute actions → release → NPC resumes
- Context-aware actions: Innkeeper actions at Inn, Blacksmith actions at Workshop
- Favorites system for bookmarking NPCs

## Technical Context

**Language/Version**: C# / .NET 10.0 (as per global.json)
**Primary Dependencies**: Blazor WASM, System.Timers (game loop), LiteDB (persistence)
**Storage**: LiteDB embedded database (validates offline-first architecture)
**Testing**: xUnit, bUnit (Blazor component testing)
**Target Platform**: Blazor WASM (desktop web browsers, Chrome/Firefox/Edge)
**Project Type**: Web application (Blazor client-side)
**Performance Goals**: Game loop 10 ticks/second, <100ms possession switch, <2s page load
**Constraints**: Responsive UI (mobile to ultrawide desktop), client-side simulation with local persistence
**Scale/Scope**: 4 NPCs, 2 buildings, 10x10 grid, 20-entry activity log
**Existing Infrastructure**: Reuse IGameService, IEventStore, Spatial models (GridPosition)

## Constitution Check

_GATE: Must pass before Phase 0 research. Re-check after Phase 1 design._

Verify compliance with [Constitution v1.0.0](../../.specify/memory/constitution.md):

- [x] **Test-First Development**: Feature spec includes testable acceptance criteria (5 user stories with Given/When/Then scenarios)
- [x] **Quality Standards**: All quality gates applicable (pre-commit hooks: Prettier, markdown lint, C# format, unit tests)
- [x] **Git Discipline**: Feature branch `001-minimal-possession-demo` created, conventional commits planned, FF-only merge to main
- [x] **Simplicity First**: MVP scope explicitly defined, OUT OF SCOPE list prevents feature creep, no premature abstractions
- [x] **Automation**: Git hooks already configured, npm scripts for quality checks, test automation via xUnit

**Complexity Justification** (complete ONLY if adding complexity):

| Added Complexity            | Justification                                         | Simpler Alternative Rejected Because                      |
| --------------------------- | ----------------------------------------------------- | --------------------------------------------------------- |
| System.Timers for game loop | Need continuous simulation even when UI not updating  | setTimeout/setInterval in JS would couple to UI rendering |
| Possession state management | Core mechanic requires tracking who controls each NPC | Direct UI binding would lose state on component re-render |

**GATE STATUS**: ✅ PASS - All constitution principles satisfied, complexity justified

## Persistence Architecture (LiteDB)

### PR-001: Database Connection & Initialization

**Requirements**:

- Database provider: LiteDB.WebAssembly (NuGet package for Blazor WASM support)
- Database location: Browser IndexedDB via LiteDB abstraction
- Database file name: `RealmsOfIdle.db`
- Connection type: `LiteConnectionType.Direct` (required for WASM)
- Connection string: `Filename=RealmsOfIdle.db;Connection=direct`

**Initialization Sequence**:

1. On SettlementGameService instantiation (singleton lifetime):
   - Attempt to open LiteDatabase connection
   - IF connection fails THEN:
     - Log error: "LiteDB initialization failed: {exception}"
     - Fall back to in-memory mode (no persistence)
     - Display warning banner: "Settlement progress will not be saved"
2. On first successful connection:
   - Verify collections exist (settlements, activityLog)
   - Create indexes if missing (see PR-002)
   - Log: "LiteDB initialized successfully"

**Error Handling**:

- Connection timeout: 5 seconds
- Retry attempts: 3 (with exponential backoff: 1s, 2s, 4s)
- Final failure: Graceful degradation to in-memory mode

---

### PR-002: Collection Schemas & Indexes

**Collection: `settlements`**

Purpose: Store Settlement aggregate root with all NPCs and Buildings

Schema:

```json
{
  "_id": "millbrook",
  "Name": "Millbrook",
  "WorldTime": "2026-02-09T12:00:00Z",
  "Buildings": [
    {
      "Id": "inn",
      "Name": "The Rusty Tankard",
      "Type": "Inn",
      "Position": { "X": 2, "Y": 2 },
      "Width": 3,
      "Height": 3,
      "Resources": {}
    }
  ],
  "NPCs": [
    {
      "Id": "mara",
      "Name": "Mara",
      "ClassName": "Innkeeper",
      "Level": 5,
      "Position": { "X": 3, "Y": 3 },
      "CurrentBuilding": "inn",
      "State": "Idle",
      "CurrentAction": null,
      "ActionStartTime": null,
      "ActionDurationSeconds": null,
      "Gold": 100,
      "Reputation": 50,
      "Priorities": {},
      "IsPossessed": false
    }
  ]
}
```

Indexes:

- Primary: `_id` (unique, auto-created by LiteDB)
- No secondary indexes (single settlement for MVP)

Constraints:

- Max document size: 16 MB (LiteDB limit)
- Expected size: ~10 KB per settlement (well within limit)

---

**Collection: `activityLog`**

Purpose: Store activity log entries for display in UI

Schema:

```json
{
  "_id": ObjectId("507f1f77bcf86cd799439011"),
  "Timestamp": "2026-02-09T12:00:00Z",
  "ActorId": "mara",
  "ActorName": "Mara",
  "ActionName": "Serve Customer",
  "Result": "+5 gold, +2 reputation"
}
```

Indexes:

- Primary: `_id` (auto-created)
- Secondary: `Timestamp` (descending) - for efficient recent-first queries

Retention Policy:

- Max entries: 100 (FIFO cleanup on insert)
- Cleanup trigger: On insert, if count > 100, delete oldest entry
- No TTL expiration (manual cleanup only)

Query Example:

```csharp
var recentEntries = db.GetCollection<ActivityLogEntry>("activityLog")
    .Find(Query.All(Query.Descending), limit: 20)
    .ToList();
```

---

### PR-003: Performance Requirements & Limits

**Latency Targets** (95th percentile):

- Settlement read: <10 ms
- Settlement write (full replacement): <50 ms
- Activity log append: <5 ms
- Activity log query (last 20 entries): <10 ms

**Throughput Requirements**:

- Settlement writes: 10/second (game loop tick rate)
- Activity log appends: 40/second (4 NPCs × 10 actions/min avg)

**Storage Limits**:

- Database size warning threshold: 10 MB
- Database size hard limit: 50 MB (browser quota)
- IF quota exceeded THEN:
  - Log warning: "Storage quota exceeded"
  - Attempt cleanup: Delete activity log entries older than 7 days
  - IF still exceeded THEN prompt user: "Clear settlement history?"

**Connection Pooling**:

- Single shared LiteDatabase instance (singleton lifetime via DI)
- No connection pooling (LiteDB file-based, single writer)
- Thread safety: LiteDB handles internal locking (WASM is single-threaded)

**Query Timeout**:

- Default: 1000 ms
- On timeout: Log error, return empty result, display "Data temporarily unavailable"

---

### PR-004: Persistence Operations

**Save Settlement** (Called on: SimulationEngine tick, tab hidden, possession change):

```csharp
public async Task SaveSettlementAsync(Settlement settlement)
{
    var collection = _db.GetCollection<Settlement>("settlements");

    // Upsert: Insert if new, replace if exists
    collection.Upsert(settlement.Id, settlement);

    // Verify write succeeded
    var saved = collection.FindById(settlement.Id);
    if (saved == null)
        throw new InvalidOperationException("Settlement save failed: verification returned null");
}
```

**Load Settlement** (Called on: App startup, page refresh):

```csharp
public async Task<Settlement?> LoadSettlementAsync(string settlementId)
{
    var collection = _db.GetCollection<Settlement>("settlements");
    return collection.FindById(settlementId);
}
```

**Append Activity Log** (Called on: NPC action completion):

```csharp
public async Task AppendActivityLogAsync(ActivityLogEntry entry)
{
    var collection = _db.GetCollection<ActivityLogEntry>("activityLog");

    // Insert new entry
    collection.Insert(entry);

    // Cleanup if > 100 entries (FIFO)
    var count = collection.Count();
    if (count > 100)
    {
        var oldest = collection
            .Find(Query.All(Query.Ascending))
            .Take(count - 100)
            .Select(e => e.Id);

        foreach (var id in oldest)
            collection.Delete(id);
    }
}
```

---

### PR-005: Data Migration & Versioning

**MVP Approach**: No migrations (clean slate on breaking changes)

**Version 1.0 Schema**:

- Current schema defined in PR-002
- No version field in documents (implicit v1.0)

**Future Migration Strategy** (v1.1+):

- Add `SchemaVersion` field to Settlement document
- On load, check version: `if (settlement.SchemaVersion < currentVersion) → Migrate()`
- Migration functions: `MigrateV1ToV2(Settlement)`, etc.

**Breaking Changes in MVP**:

- If schema changes during development: Drop database, reinitialize
- User message: "Data model updated. Settlement reset required."
- Production: No breaking changes after v1.0 release

---

### PR-006: Backup & Recovery

**MVP Approach**: No automated backups (browser storage is primary)

**Manual Export** (Future feature):

- "Export Settlement" button → Download JSON file
- "Import Settlement" button → Upload JSON, validate, restore

**Corruption Recovery**:

- See NFR-001a in spec.md (Exception Handling requirements)
- On corruption: Prompt user for reset or read-only mode

**Browser Storage Clearing**:

- User clears browser data → Settlement lost
- Display on next load: "No settlement found. Start new settlement?"

## Project Structure

### Documentation (this feature)

```text
specs/001-minimal-possession-demo/
├── plan.md              # This file
├── spec.md              # User stories and requirements
├── research.md          # Phase 0 output (technical decisions)
├── data-model.md        # Phase 1 output (entity schemas)
├── quickstart.md        # Phase 1 output (developer guide)
└── contracts/           # Phase 1 output (API contracts - N/A for client-only MVP)
```

### Source Code (repository root)

```text
src/
├── RealmsOfIdle.Client.Blazor/
│   ├── Pages/
│   │   └── PossessionDemo.razor    # Main demo page
│   ├── Components/
│   │   ├── SettlementView.razor    # Top-level container
│   │   ├── SettlementMap.razor     # Grid display with buildings/NPCs
│   │   ├── NPCSidebar.razor        # NPC list with portraits
│   │   ├── ActionPanel.razor       # Available actions when possessed
│   │   ├── ActivityLog.razor       # Scrolling action history
│   │   └── TopBar.razor            # Mode indicator, world time
│   ├── Services/
│   │   ├── SettlementGameService.cs # Extends IGameService for possession
│   │   ├── SimulationEngine.cs      # Game loop (adapts InnGameLoop pattern)
│   │   └── NPCAIService.cs          # Autonomous NPC behavior
│   └── Models/
│       ├── Settlement.cs            # Contains Buildings + NPCs
│       ├── Building.cs              # Type, Position, Resources
│       ├── NPC.cs                   # State, Class, Location, Gold
│       ├── NPCAction.cs             # Duration, Costs, Rewards
│       └── ActivityLogEntry.cs      # Timestamp, Actor, Action
│
├── RealmsOfIdle.Core/
│   ├── Abstractions/
│   │   ├── IGameService.cs          # EXTEND: Add possession operations
│   │   └── IEventStore.cs           # REUSE: For activity logging
│   ├── Engine/Spatial/
│   │   └── GridPosition.cs          # REUSE: Existing spatial model
│   └── Scenarios/Inn/
│       └── InnGameLoop.cs           # PATTERN: Adapt for SimulationEngine
│
tests/
└── RealmsOfIdle.Client.Blazor.Tests/
    ├── Services/
    │   ├── SimulationEngineTests.cs
    │   ├── PossessionManagerTests.cs
    │   └── NPCAIServiceTests.cs
    ├── Components/
    │   ├── NPCSidebarTests.cs
    │   └── ActionPanelTests.cs
    └── Integration/
        └── PossessionDemoE2ETests.cs  # End-to-end using bUnit
```

**Structure Decision**: Extend existing Blazor client project (`RealmsOfIdle.Client.Blazor`) with new `PossessionDemo.razor` page and supporting components. **Reuse existing Core infrastructure**: `IGameService` interface (extend with possession ops), `IEventStore` (activity logging), `GridPosition` (spatial positioning). **Leverage LocalGameService + LiteDB** to validate offline-first architecture from tech stack. **Adapt InnGameLoop pattern** for SimulationEngine implementation.

## Complexity Tracking

> **Fill ONLY if Constitution Check has violations that must be justified**

No violations - all complexity justified above.

---

## Alignment with Existing Infrastructure

**Codebase Analysis** (2026-02-09): Alignment review identified several existing patterns to leverage:

### Reuse Existing Models

| Existing Component   | Location                                                  | Usage                                                                     |
| -------------------- | --------------------------------------------------------- | ------------------------------------------------------------------------- |
| **IGameService**     | `RealmsOfIdle.Core/Abstractions/IGameService.cs`          | Extend with possession operations (PossessNPC, ReleaseNPC, ExecuteAction) |
| **IEventStore**      | `RealmsOfIdle.Core/Abstractions/IEventStore.cs`           | Use for activity log persistence                                          |
| **GridPosition**     | `RealmsOfIdle.Core/Engine/Spatial/GridPosition.cs`        | Reuse for NPC/Building positioning (already exists)                       |
| **LocalGameService** | `RealmsOfIdle.Client.Shared/Services/LocalGameService.cs` | Adapt pattern for SettlementGameService + LiteDB                          |

### Adapt Existing Patterns

| Pattern Source     | Location                                         | Adaptation                                                   |
| ------------------ | ------------------------------------------------ | ------------------------------------------------------------ |
| **InnGameLoop**    | `RealmsOfIdle.Core/Scenarios/Inn/InnGameLoop.cs` | Template for SimulationEngine (game loop + state management) |
| **InnState**       | `RealmsOfIdle.Core/Scenarios/Inn/InnState.cs`    | Template for Settlement immutable state pattern              |
| **Spatial System** | `RealmsOfIdle.Core/Engine/Spatial/`              | Use SceneGraph, WorldLayout for settlement grid              |

### Key Adjustments from Original Spec

1. **Storage**: Changed from "in-memory only" to LiteDB to validate offline-first architecture
2. **Service Pattern**: Changed from standalone services to extending IGameService interface
3. **Spatial Models**: Reuse existing GridPosition instead of creating new one
4. **Game Loop**: Adapt InnGameLoop pattern instead of creating from scratch

**Rationale**: These adjustments improve architectural consistency and leverage existing work validated in the Inn scenario.

---

## Phase 0: Research & Technical Decisions

**Goal**: Resolve all unknowns from Technical Context, document technology choices

### Research Tasks

1. **Blazor Timer Strategy**: Best practice for game loop in Blazor WASM
   - Options: System.Timers.Timer vs Task.Delay loop vs JS interop setInterval
   - Decision criteria: Performance, reliability, state management

2. **Blazor State Management**: How to maintain simulation state across component re-renders
   - Options: Scoped service, singleton service, state container pattern, existing LocalGameService
   - Decision criteria: Component lifecycle compatibility, memory leaks, alignment with existing infrastructure

3. **bUnit Testing**: How to test timer-based components
   - Options: Mock timers, fast-forward time, integration tests only
   - Decision criteria: Test reliability, speed, maintainability

4. **NPC AI Architecture**: Simplest autonomous behavior pattern for MVP
   - Options: Simple state machine, goal-oriented action planning, behavior trees
   - Decision criteria: Simplicity, extensibility, testability

**Output**: `research.md` with decisions, rationales, alternatives considered

---

## Testing Strategy

### TR-001: Test Fixtures

**Requirements**:

- Test fixture class: `SettlementTestFixtures.cs` in `tests/RealmsOfIdle.Client.Blazor.Tests/Fixtures/`
- Factory methods required:
  - `CreateTestSettlement()`: Returns Settlement with 1 building, 2 NPCs, minimal setup
  - `CreateTestNPC(string id, NPCState state)`: Returns NPC with specified state
  - `CreateTestBuilding(string id, BuildingType type)`: Returns Building with default resources
  - `CreateTestAction(string id, int durationSeconds)`: Returns NPCAction with specified duration
- All fixtures MUST use deterministic IDs (no GUIDs) for test repeatability
- Fixtures MUST be immutable (return new instances each call)

**Example**:

```csharp
public static class SettlementTestFixtures
{
    public static Settlement CreateTestSettlement()
    {
        return new Settlement(
            Id: "test-settlement",
            Name: "Test Settlement",
            Buildings: new List<Building> { CreateTestBuilding("inn", BuildingType.Inn) },
            NPCs: new List<NPC> { CreateTestNPC("mara", NPCState.Idle) },
            WorldTime: DateTime.UtcNow
        );
    }

    public static NPC CreateTestNPC(string id, NPCState state)
    {
        return new NPC(id, "Test NPC", "Innkeeper", 5, new GridPosition(3, 3),
            "inn", state, null, null, null, 100, 50, new Dictionary<string, object>(), false);
    }
}
```

---

### TR-002: Immutability Assertions

**Requirements**:

- Test helper: `ImmutabilityAssertions.cs` in `tests/RealmsOfIdle.Client.Blazor.Tests/Helpers/`
- Method: `AssertImmutable<T>(T original, T modified, string changedProperty)`
- Verification steps:
  1. Assert original != modified (reference equality fails)
  2. Assert original.[changedProperty] != modified.[changedProperty] (value changed)
  3. Assert all other properties unchanged (deep equality check)
- Example: `ImmutabilityAssertions.AssertImmutable(originalNPC, updatedNPC, nameof(NPC.Gold))`
- MUST verify immutability for: Settlement updates, NPC state changes, Building resource changes

**Example**:

```csharp
public static class ImmutabilityAssertions
{
    public static void AssertImmutable<T>(T original, T modified, string changedProperty)
        where T : class
    {
        // Reference inequality
        Assert.NotSame(original, modified);

        // Changed property is different
        var prop = typeof(T).GetProperty(changedProperty);
        Assert.NotEqual(prop.GetValue(original), prop.GetValue(modified));

        // All other properties unchanged
        foreach (var property in typeof(T).GetProperties())
        {
            if (property.Name == changedProperty) continue;
            Assert.Equal(property.GetValue(original), property.GetValue(modified));
        }
    }
}
```

---

### TR-003: Red-Green-Refactor Verification

**Requirements**:

- BEFORE implementing feature:
  1. Write test with expected behavior
  2. Run test → MUST FAIL (RED)
  3. Take screenshot of failure or save test output
  4. Commit test with message: "test: add failing test for [feature] (RED)"
- DURING implementation: 5. Implement minimal code to pass test 6. Run test → MUST PASS (GREEN) 7. Commit implementation with message: "feat: implement [feature] (GREEN - test now passes)"
- AFTER test passes: 8. Refactor code for clarity/performance 9. Run test → MUST STILL PASS 10. Commit refactoring with message: "refactor: improve [aspect] (test still GREEN)"

**Verification**:

- Document verification in commit message
- Example: "Test failed at commit abc123, passed after implementation at def456"
- Use `git log` to verify RED → GREEN → REFACTOR sequence

---

## Component Architecture

### CL-001: PossessionDemo.razor Lifecycle

**OnInitializedAsync()**:

1. Inject IGameService, SimulationEngine, OfflineProgressCalculator, TabVisibilityHandler from DI
2. Load Settlement from SettlementGameService.LoadSettlementAsync("millbrook")
3. IF Settlement not found THEN initialize with Settlement.CreateMillbrook()
4. Call SimulationEngine.Start()
5. Subscribe to SimulationEngine.OnTick event
6. **Total time budget: <500ms** (measured from method entry to exit)

**OnAfterRenderAsync(firstRender: true)**:

1. Initialize TabVisibilityHandler (sets up Page Visibility API listeners)
2. Subscribe to TabVisibilityHandler.OnTabHidden event
3. Subscribe to TabVisibilityHandler.OnTabVisible event

**OnDispose()**:

1. Unsubscribe from SimulationEngine.OnTick
2. Call SimulationEngine.Stop()
3. Persist final Settlement state: await SettlementGameService.SaveSettlementAsync(settlement)
4. Call SimulationEngine.Dispose()
5. Unsubscribe from TabVisibilityHandler events
6. Call await TabVisibilityHandler.DisposeAsync()
7. **Total time budget: <100ms**

---

### CL-002: Component State Management

**Requirements**:

- Components MUST use Blazor's StateHasChanged() for UI updates
- StateHasChanged() MUST be called on SimulationEngine.OnTick
- MUST use InvokeAsync() when updating UI from timer thread: `await InvokeAsync(StateHasChanged)`
- Component render MUST complete in <16ms (60 FPS target)
- Avoid re-rendering entire settlement on every tick (use component granularity)

**Pattern**:

```csharp
private void OnSimulationTick()
{
    // Update simulation state
    UpdateSimulation();

    // Trigger UI update on Blazor's sync context
    InvokeAsync(StateHasChanged);
}
```

---

### CL-003: Error Boundaries

**Requirements**:

- PossessionDemo MUST be wrapped in ErrorBoundary component
- ErrorBoundary.OnError handler MUST log exception and display fallback UI
- Fallback UI MUST show: "Settlement encountered an error. [Reload Settlement]" button
- Error boundary MUST NOT prevent other components from rendering
- "Reload Settlement" button MUST:
  1. Clear corrupted state
  2. Call Settlement.CreateMillbrook()
  3. Restart SimulationEngine
  4. Hide error UI

**Example**:

```razor
<ErrorBoundary @ref="_errorBoundary">
    <ChildContent>
        <PossessionDemo />
    </ChildContent>
    <ErrorContent Context="exception">
        <div class="error-panel">
            <h2>Settlement Error</h2>
            <p>@exception.Message</p>
            <button @onclick="ReloadSettlement">Reload Settlement</button>
        </div>
    </ErrorContent>
</ErrorBoundary>
```

---

## Offline Progress Technical Approach

**Strategy**: Stop Timer + Calculate on Return (RECOMMENDED for MVP)

**Rationale**:

- Conserves CPU/battery when tab hidden
- Simpler implementation than continuous background simulation
- Deterministic results (calculation reproducible)
- Aligns with idle game genre conventions

**Tab Hidden Sequence**:

1. User switches tabs or minimizes browser
2. Page Visibility API fires `visibilitychange` event (document.hidden == true)
3. TabVisibilityHandler detects change via JavaScript interop
4. TabVisibilityHandler.OnTabHidden event fires
5. PossessionDemo handler:
   - Stops SimulationEngine timer
   - Persists current Settlement state to LiteDB with WorldTime snapshot
   - Logs: "Tab hidden at {timestamp}. State persisted."

**Tab Visible Sequence**:

1. User returns to tab
2. Page Visibility API fires `visibilitychange` event (document.hidden == false)
3. TabVisibilityHandler detects change
4. TabVisibilityHandler.OnTabVisible(elapsedTime) event fires
5. PossessionDemo handler:
   - IF elapsed >= 60 seconds THEN:
     - Call OfflineProgressCalculator.CalculateProgress(lastState, elapsed, actionCatalog)
     - Apply result to Settlement (update NPCs with rewards)
     - Save updated Settlement to LiteDB
     - Display OfflineProgressModal with summary
     - Generate activity log summary entry
   - Restart SimulationEngine timer
   - Logs: "Tab visible after {elapsedSeconds}s. Offline progress applied."

**Algorithm Pseudocode**:

```csharp
public OfflineProgressResult CalculateProgress(Settlement lastState, TimeSpan elapsed, ActionCatalog catalog)
{
    if (elapsed.TotalSeconds < 60) return NoProgress(lastState);

    var cappedElapsed = TimeSpan.FromSeconds(Math.Min(elapsed.TotalSeconds, 86400)); // 24hr cap
    var updatedNPCs = new List<NPC>();
    var totalGold = 0;
    var totalReputation = 0;

    foreach (var npc in lastState.NPCs)
    {
        if (npc.IsPossessed)
        {
            // Clear possession, no rewards for possessed NPCs
            updatedNPCs.Add(npc with { IsPossessed = false, State = NPCState.Idle });
            continue;
        }

        // Get NPC's primary action (e.g., Innkeeper → Serve Customer)
        var action = GetPrimaryActionForNPC(npc, catalog, lastState);
        if (action == null) continue;

        // Calculate complete action cycles
        var cycleCount = (int)(cappedElapsed.TotalSeconds / action.DurationSeconds);
        var goldReward = cycleCount * action.Rewards["Gold"];
        var repReward = cycleCount * action.Rewards.GetValueOrDefault("Reputation", 0);

        updatedNPCs.Add(npc with
        {
            Gold = npc.Gold + goldReward,
            Reputation = npc.Reputation + repReward,
            State = NPCState.Idle
        });

        totalGold += goldReward;
        totalReputation += repReward;
    }

    return new OfflineProgressResult(
        UpdatedSettlement: lastState with { NPCs = updatedNPCs, WorldTime = DateTime.UtcNow },
        TotalGoldEarned: totalGold,
        TotalReputationEarned: totalReputation,
        CappedElapsedTime: cappedElapsed
    );
}
```

**Alternative Rejected**: Continue Timer in Background

- Cons: Wastes CPU/battery, may be throttled by browser, activity log grows unbounded
- Decision: Rejected for MVP due to unnecessary complexity and resource waste

---

## Phase 1: Design & Data Model

**Prerequisites**: research.md complete

### Entities (data-model.md)

**Settlement**:

- Fields: Id (string), Name (string), Buildings (List<Building>), NPCs (List<NPC>), WorldTime (DateTime)
- Validation: Must have at least 1 building, at least 1 NPC
- State: Immutable (use `with` expressions for updates)

**Building**:

- Fields: Id, Name, Type (enum: Inn/Workshop), Position (GridPosition), Width, Height, Resources (Dictionary<string, int>)
- Validation: Position must be within settlement bounds, Width/Height > 0
- Relationships: Contains NPCs (by CurrentBuilding reference)

**NPC**:

- Fields: Id, Name, ClassName (string), Level (int), Position (GridPosition), CurrentBuilding (string), State (enum), CurrentAction (string), ActionStartTime (DateTime), ActionDurationSeconds (int), Gold (int), Reputation (int), Priorities (Dictionary<string, object>), IsPossessed (bool)
- Validation: Level > 0, Gold >= 0, State must match CurrentAction (Working requires action)
- State Transitions: Idle → Working (action started), Working → Idle (action completed)

**NPCAction**:

- Fields: Id, Name, Description, DurationSeconds, ResourceCosts (Dictionary<string, int>), Rewards (Dictionary<string, int>), RequiredClasses (List<string>), RequiredBuildings (List<string>)
- Validation: DurationSeconds > 0, Required lists not empty
- Relationships: Referenced by NPC.CurrentAction

**ActivityLogEntry**:

- Fields: Timestamp (DateTime), ActorId (string), ActorName (string), ActionName (string), Result (string)
- Validation: Timestamp <= WorldTime, ActorId must reference valid NPC

### API Contracts

**N/A for MVP** - Client-only simulation, no HTTP endpoints. All logic runs in-browser via Blazor WASM.

### Quickstart (quickstart.md)

**Developer Setup**:

1. Clone repo, checkout `001-minimal-possession-demo` branch
2. `dotnet restore`
3. `dotnet run --project src/RealmsOfIdle.Client.Blazor`
4. Navigate to `/possession-demo` route
5. Watch NPCs work autonomously, click Mara to possess, execute action

**Testing**:

1. Unit tests: `dotnet test --filter "FullyQualifiedName~RealmsOfIdle.Client.Blazor.Tests"`
2. Run all quality gates: `npm run quality`

---

## Phase 2: Implementation Phases (from design doc)

### Phase 1: Foundation (Week 1)

- ✅ Review existing Core infrastructure (IGameService, IEventStore, GridPosition)
- ✅ Create data models (Settlement, Building, NPC, Action) - reuse GridPosition from Core
- ✅ Extend IGameService with possession operations (PossessNPC, ReleaseNPC, ExecuteAction)
- ✅ Implement SettlementGameService (adapts LocalGameService + LiteDB pattern)
- ✅ Implement SimulationEngine (adapts InnGameLoop pattern)
- ✅ Basic Blazor page with settlement view
- ✅ Display 2 buildings, 4 NPCs on a grid
- ✅ NPCs have fixed positions (no movement yet)

**Acceptance Criteria**:

- Open app → see Millbrook with 2 buildings
- See 4 NPC cards in sidebar with names, classes, locations
- Game loop runs (check console logs for ticks)
- State persists in LiteDB (refresh page → settlement reloads)

### Phase 2: Autonomous Behavior (Week 1)

- ✅ Implement NPC AI state machine
- ✅ Define 2-3 actions per NPC class
- ✅ NPCs automatically perform actions when idle
- ✅ Activity log shows NPC actions

**Acceptance Criteria**:

- Watch Mara automatically serve customers
- Watch Tomas automatically craft swords
- Activity log updates with each action
- Gold increases for NPCs performing actions

### Phase 3: Possession (Week 2)

- ✅ Implement PossessionManager
- ✅ Click NPC → possession modal appears
- ✅ Action panel shows available actions
- ✅ Execute action → see timer and result
- ✅ Release → NPC resumes autonomously

**Acceptance Criteria**:

- Possess Mara → see "Serve Customer" action
- Click action → timer counts down → gold increases
- Release → Mara continues serving autonomously
- Possess Tomas → see "Craft Sword" action → execute → see result

### Phase 4: Context Awareness (Week 2)

- ✅ Implement action filtering by building + class
- ✅ Actions change when NPC moves buildings (manual trigger)
- ✅ Demonstrate: Blacksmith has no actions at Inn

**Acceptance Criteria**:

- While possessing Tomas at Forge, see blacksmith actions
- Move Tomas to Inn (via button)
- Action list updates: no blacksmith actions available
- Customer actions appear instead

### Phase 5: Polish & Favorites (Week 3)

- ✅ Favorites system (star NPCs)
- ✅ Notifications for favorited NPCs
- ✅ Improve UI/UX (animations, icons, layout)
- ✅ Activity log scrolling and filtering

**Acceptance Criteria**:

- Star Mara → she appears in Favorites panel
- Quick-possess from Favorites panel
- Notification badge when Mara completes action while not possessed
- UI feels responsive and polished

---

### Phase 6: NPC Hiring & Resource Economy (Week 4) - Option B Addition

**Purpose**: Add economic pressure via contracts/wages and resource production/consumption to create meaningful idle game decisions

- ✅ Implement NPC employment system (Available/Employed/Unpaid states)
- ✅ Add building treasury and contract costs (Cook: 50g, Tomas: 100g)
- ✅ Implement daily wage payment cycle with grace period
- ✅ Add resource production (Cook produces Food, Tomas crafts Tools)
- ✅ Add resource consumption (Innkeeper consumes Food to serve customers)
- ✅ Implement resource capacity limits (Food: 50 max, Iron Ore: 100 max)
- ✅ Update offline progress to apply wages and cap cycles by resources
- ✅ Create HiringService, WageService, ResourceService
- ✅ Add Available NPCs panel with hire buttons
- ✅ Update ActionCatalog with resource costs/production

**Acceptance Criteria**:

- Mara starts employed at Inn with 120g building treasury
- Cook and Tomas available to hire from Town Square
- Can hire Cook for 50g → Cook begins producing Food autonomously
- Inn charges daily wage (5g/day) from building treasury
- If Inn cannot pay wage → Cook becomes Unpaid → quits after 1 day
- Innkeeper cannot serve customers without Food in Inn inventory
- Cook produces Food until storage full (50/50)
- Offline progress correctly deducts wages and caps actions by resources

**Rationale**: Adds core idle game loop - hire staff, manage resources, watch settlement grow. Aligns with game design documents (core-architecture.md, npc-design.md).

---

### Phase 7: Building Upgrades (Week 4) - Option B Addition

**Purpose**: Provide long-term progression goals via building level system

- ✅ Add Level field to Building record (1-3)
- ✅ Implement upgrade system (cost, time, benefits)
- ✅ Inn Level 2: +50 Food storage, +1 staff capacity
- ✅ Inn Level 3: +50% customer attraction
- ✅ Workshop Level 2: +100 Iron Ore storage, +20% crafting speed
- ✅ Workshop Level 3: Unlock advanced recipes
- ✅ Create UpgradeService with time tracking
- ✅ Integrate upgrade timers into SimulationEngine
- ✅ Add BuildingUpgradePanel UI component
- ✅ Update offline progress to complete pending upgrades

**Acceptance Criteria**:

- Buildings start at Level 1
- Can initiate upgrade (100g for Inn L2, 60s duration)
- Progress bar shows upgrade time remaining
- Upgrade completes and benefits apply (Food storage 50→100)
- Offline progress completes upgrades if elapsed time > duration
- Cannot start new upgrade while one in progress

**Rationale**: Settlement health progression (not player XP) via building improvements. Creates gold sink and visible progress.

---

### Phase 8: Observer-Friendly Tutorial (Week 5) - Option B Addition

**Purpose**: Onboard new players without disrupting passive gameplay option

- ✅ Implement TutorialService with localStorage persistence
- ✅ Tutorial triggers ONLY on first Mara possession (not page load)
- ✅ Progressive disclosure: 3-step modal sequence
  - Step 1: Possession confirmed → highlight Action Panel
  - Step 2: Action executing → highlight Release Control
  - Step 3: Action complete → highlight other NPC portraits
- ✅ Create TutorialModal.razor with highlight system
- ✅ Add tutorial-highlights.css for glowing/pulsing animations
- ✅ Implement skip functionality (button + Escape key)
- ✅ Mark tutorial complete in localStorage after final step

**Acceptance Criteria**:

- Open page → watch NPCs work → NO tutorial appears (observer mode preserved)
- Possess Mara → tutorial modal appears
- Complete 3 steps or skip → tutorial never shows again
- Tutorial completion persists across page refreshes
- Can reset via dev console: `localStorage.removeItem('tutorial_completed')`

**Rationale**: User clarification - preserve observer mode, only teach possession on first use.

---

### Phase 9: Performance & Playtesting (Week 5) - Option B Addition

**Purpose**: Validate technical performance and player engagement before release

**Performance Benchmarks**:

- ✅ Game loop maintains 10 ticks/sec ±5% variance (10 min test)
- ✅ Heap growth <10MB over 10 minutes (no memory leaks)
- ✅ CPU usage <20% on mid-range device
- ✅ Cross-browser tests pass on Chrome, Firefox, Edge

**Playtesting Validation**:

- ✅ Recruit 5 users, measure session length (target: avg >5 min)
- ✅ Survey comprehension (target: 80% understand possession)
- ✅ Survey engagement (target: 60% would play 30 min)
- ✅ Analyze click heatmaps, identify confusing UI areas
- ✅ Document findings in playtest-results.md

**Acceptance Criteria**:

- All performance benchmarks pass (tick rate, memory, CPU, browsers)
- At least 80% playtest comprehension rate
- At least 60% would-play-longer rate
- UX improvements implemented based on heatmap analysis

**Rationale**: Ensure MVP meets quality bar before public release. Validate core assumption: "possession mechanic is understandable and engaging."

---

## Success Metrics

### Technical Metrics

- ✅ Game loop runs at 10 ticks/second without lag
- ✅ Possession switch happens in <100ms
- ✅ Action execution feels responsive (<50ms to start timer)
- ✅ No errors in browser console
- ✅ Page load <2 seconds

### User Experience Metrics

- ✅ User can understand possession mechanic in <1 minute
- ✅ User can possess, execute action, and release in <30 seconds
- ✅ User can see autonomous behavior resuming after release
- ✅ User can experience 2 different "scenarios" (inn vs blacksmith)
- ✅ Demo is interesting enough to watch for 5+ minutes

---

## Next Steps After MVP Completion

**v1.1 - More Depth**:

- Add 2 more buildings (Market, Temple)
- Add 4 more NPCs (Merchant, Priest, Guard, Farmer)
- Add resource flow (inn needs food from farmer)
- Add simple customer AI (they order, eat, pay, leave)

**v1.2 - Persistence**:

- Save state to browser localStorage
- Load state on page refresh
- Offline progress calculation (simple)

**v1.3 - World Expansion**:

- Add second settlement (travel between)
- Add regional map view
- Add traveling NPCs

**v2.0 - Multiplayer Foundation**:

- Add player identity system
- Separate favorites per player
- Shared world state (backend required)

---

## References

- **Core Architecture**: `docs/design/core-architecture.md` (authoritative)
- **Design Document**: `docs/plans/2026-02-09-minimal-possession-demo.md` (detailed technical specs)
- **Constitution**: `.specify/memory/constitution.md` (governance)
