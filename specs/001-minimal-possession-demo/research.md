# Research & Technical Decisions: Minimal Possession Demo v1

**Feature**: Minimal Possession Demo
**Date**: 2026-02-09
**Status**: Complete

---

## Research Tasks

### 1. Blazor Timer Strategy for Game Loop

**Question**: What's the best approach for implementing a continuous game loop (10 ticks/second) in Blazor WASM?

**Options Evaluated**:

1. **System.Timers.Timer** (Server-side pattern)
   - Pros: Familiar .NET API, precise intervals, runs on background thread
   - Cons: Requires `InvokeAsync` for UI updates, may cause threading issues in WASM

2. **Task.Delay + while loop**
   - Pros: Async/await friendly, no threading concerns in WASM
   - Cons: Drift over time if processing takes longer than delay

3. **JS Interop + setInterval**
   - Pros: Native browser timer, guaranteed 60 FPS alignment
   - Cons: Requires JS interop, complicates state management

**Decision**: **Use System.Timers.Timer with InvokeAsync**

**Rationale**:

- Blazor WASM runs on a single thread, so Timer events are queued safely
- `InvokeAsync` ensures UI updates happen on correct synchronization context
- Precision is acceptable for 10 ticks/second (100ms intervals)
- Most familiar pattern for .NET developers

**Implementation Pattern**:

```csharp
public class SimulationEngine : IDisposable
{
    private System.Timers.Timer _timer;
    private readonly IServiceProvider _services;

    public void Start()
    {
        _timer = new System.Timers.Timer(100); // 10 ticks/sec
        _timer.Elapsed += async (sender, e) => await OnTick();
        _timer.Start();
    }

    private async Task OnTick()
    {
        // Update simulation
        UpdateSimulation();

        // Notify UI (via event or StateHasChanged)
        await InvokeStateChanged();
    }
}
```

**Alternatives Considered**:

- Task.Delay loop: Rejected due to potential drift and less control over stopping/starting
- JS interop: Rejected to keep C# codebase pure, avoid JS dependency

---

### 2. Blazor State Management for Simulation

**Question**: How should we maintain simulation state (Settlement, NPCs) across component re-renders?

**Options Evaluated**:

1. **Scoped Service** (per-component lifetime)
   - Pros: Automatic disposal, isolated per page
   - Cons: Lost on navigation, inappropriate for global simulation

2. **Singleton Service** (app lifetime)
   - Pros: Survives navigation, shared across components
   - Cons: Requires manual disposal, memory leak risk

3. **State Container Pattern** (Observable singleton)
   - Pros: Explicit state change notifications, testable
   - Cons: More boilerplate, requires event handling

**Decision**: **Extend Existing LocalGameService + LiteDB**

**Rationale**:

- Validates offline-first architecture from tech stack document
- LocalGameService already implements IGameService interface with LiteDB persistence
- Demonstrates state persistence across page refreshes (key architectural requirement)
- Aligns with existing codebase patterns (IGameService, IEventStore)
- LiteDB embedded database matches tech stack choice (in-memory was divergence)

**Implementation Pattern**:

```csharp
// Extend IGameService interface
public interface IGameService
{
    // Existing methods...

    // NEW: Possession operations
    Task<PossessionResult> PossessNPCAsync(string npcId);
    Task ReleaseNPCAsync(string npcId);
    Task<ActionResult> ExecuteActionAsync(string npcId, string actionId);
}

// In SettlementGameService (adapts LocalGameService pattern)
public class SettlementGameService : IGameService
{
    private readonly LiteDatabase _db;
    private readonly SimulationEngine _engine;

    // Implements IGameService with possession support
}

// In Program.cs
builder.Services.AddSingleton<IGameService, SettlementGameService>();
```

**Alternatives Considered**:

- In-memory only singleton: Rejected to validate offline-first architecture
- Scoped service: Rejected because state must persist across navigations
- State container pattern: Deferred to future iteration (over-engineering for MVP)

---

### 3. bUnit Testing for Timer-Based Components

**Question**: How do we test Blazor components that depend on timer-based simulation updates?

**Options Evaluated**:

1. **Mock Timers** (replace System.Timers.Timer with testable interface)
   - Pros: Full control over time progression, fast tests
   - Cons: Requires abstraction layer, not testing real timer behavior

2. **Fast-Forward Time** (manually trigger tick events)
   - Pros: Simple, tests actual timer code path
   - Cons: Still not real-time, may miss threading issues

3. **Integration Tests Only** (real timers with Task.Delay waits)
   - Pros: Tests actual behavior, catches timing bugs
   - Cons: Slow tests (must wait for real intervals)

**Decision**: **Mock Timers for Unit Tests + Real Timers for Integration Tests**

**Rationale**:

- Unit tests need speed and determinism → mock timers via IGameLoop interface
- Integration tests need realism → use actual timers with bUnit's `WaitForState`
- Separation of concerns: test timer logic separately from UI updates

**Implementation Pattern**:

```csharp
// IGameLoop.cs (abstraction)
public interface IGameLoop
{
    event Action OnTick;
    void Start();
    void Stop();
}

// SimulationEngine.cs (implements IGameLoop)
public class SimulationEngine : IGameLoop, IDisposable
{
    private Timer _timer;
    public event Action OnTick;

    public void Start() { /* real timer */ }
    // ...
}

// MockGameLoop.cs (for tests)
public class MockGameLoop : IGameLoop
{
    public event Action OnTick;
    public void TriggerTick() => OnTick?.Invoke();
}

// Test usage
var mockLoop = new MockGameLoop();
var component = RenderComponent<SettlementView>(p => p.Add(x => x.GameLoop, mockLoop));
mockLoop.TriggerTick(); // Manually advance simulation
component.WaitForAssertion(() => Assert.Contains("Mara served", component.Markup));
```

**Alternatives Considered**:

- Integration tests only: Rejected due to slow test suite (waiting for real timers)
- Mock timers only: Rejected because we lose confidence in actual timer behavior

---

### 4. NPC AI Architecture for MVP

**Question**: What's the simplest autonomous behavior pattern that's still extensible?

**Options Evaluated**:

1. **Simple State Machine** (Idle/Working states, choose next action when idle)
   - Pros: Minimal code, easy to understand, sufficient for MVP
   - Cons: Limited flexibility, hard to add complex behaviors later

2. **Goal-Oriented Action Planning (GOAP)** (NPCs have goals, select actions to achieve)
   - Pros: Flexible, scalable to complex AI
   - Cons: Over-engineering for MVP (4 NPCs, 3 actions each)

3. **Behavior Trees** (Hierarchical decision trees)
   - Pros: Industry standard, visual editing tools exist
   - Cons: Significant upfront investment, overkill for MVP

**Decision**: **Simple State Machine with Priority-Based Action Selection**

**Rationale**:

- MVP has 4 NPCs with simple routines (innkeeper serves, blacksmith crafts)
- State machine (Idle → Working → Idle) is sufficient
- Priority-based selection: "If customers waiting, serve; else check income"
- Can evolve to GOAP later without rewriting (states remain, decision logic changes)

**Implementation Pattern**:

```csharp
public class NPCAIService
{
    public void UpdateNPC(NPC npc, Settlement settlement)
    {
        if (npc.State == NPCState.Working)
        {
            CheckActionCompletion(npc);
            return;
        }

        if (npc.State == NPCState.Idle)
        {
            var action = ChooseNextAction(npc, settlement);
            if (action != null)
                StartAction(npc, action);
        }
    }

    private NPCAction ChooseNextAction(NPC npc, Settlement settlement)
    {
        // Simple priority-based selection
        if (npc.ClassName == "Innkeeper")
        {
            if (HasWaitingCustomers(settlement))
                return Actions.ServeCustomer;
            return Actions.CheckIncome; // Idle action
        }
        else if (npc.ClassName == "Blacksmith")
        {
            if (HasMaterials(npc, "IronOre", 2))
                return Actions.CraftSword;
            return Actions.Rest;
        }
        return null;
    }
}
```

**Alternatives Considered**:

- GOAP: Deferred to v1.1+ when more complex behaviors needed (multi-step plans, resource gathering)
- Behavior trees: Rejected as over-engineering for 4 NPCs with scripted routines

---

## Summary of Decisions

| Topic            | Decision                                       | Rationale                                                         |
| ---------------- | ---------------------------------------------- | ----------------------------------------------------------------- |
| Game Loop        | System.Timers.Timer + InvokeAsync              | Familiar .NET pattern, precise, handles WASM single-thread safely |
| State Management | Singleton Service                              | Simulation must survive navigation, simple for MVP                |
| Testing Strategy | Mock timers (unit) + Real timers (integration) | Fast unit tests, realistic integration tests                      |
| NPC AI           | Simple State Machine + Priority Selection      | Sufficient for MVP, easy to understand, extensible                |

---

## Technology Stack Confirmed

**Core**:

- .NET 10.0
- Blazor WASM (client-side)
- System.Timers (game loop)
- C# Records (immutable state)

**Testing**:

- xUnit (unit test framework)
- bUnit (Blazor component testing)
- Moq (mocking, if needed)

**UI**:

- Blazor Components (Razor syntax)
- CSS (no framework for MVP - keep simple)
- No JavaScript (pure C#/Blazor)

**No Backend**:

- Client-only simulation
- No HTTP APIs
- No database (in-memory state)

---

## Open Questions (Resolved)

1. **NPC Movement**: Instant teleport or walking animation?
   - **Resolution**: Instant teleport for MVP (Phase 1), walking in v1.1

2. **Customer NPCs**: Spawn new or cycle single customer?
   - **Resolution**: Single customer that resets after being served (simpler)

3. **Time Scale**: 1 real second = 1 game minute?
   - **Resolution**: Confirmed - makes 30s actions feel like "30 game minutes"

4. **Mobile Support**: Optimize for mobile in MVP?
   - **Resolution**: No - desktop web only for MVP

---

## Next Phase

**Phase 1: Design & Data Model** - Document entity schemas, generate data-model.md and quickstart.md
