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
**Constraints**: Desktop-only UI, client-side simulation with local persistence
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
