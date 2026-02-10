# Plan.md Updates - Copy-Paste Ready

## Add New Section: Persistence Architecture (after § Technical Context)

---

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

---

## Add New Section: Testing Strategy (before § Phase 2: Implementation Phases)

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

## Add New Section: Component Architecture (before § Phase 2)

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

## Add to § Offline Progress Technical Approach (new section after Component Architecture)

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
