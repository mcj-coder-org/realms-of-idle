# Requirements Quality Checklist: MVP (US1 + US2)

**Purpose**: Validate requirements quality for Minimal Possession Demo MVP - Observer Mode (US1) and Possession & Control (US2)

**Created**: 2026-02-09

**Scope**: Implementation gate checklist focusing on Core Mechanics, Data Model Integrity, and Testing Requirements

**Focus Areas**: Core possession mechanics, autonomous NPC behavior, immutable state management, LiteDB persistence, TDD requirements

---

## Requirement Completeness

Requirements documentation coverage for MVP user stories:

- [ ] CHK001 - Are autonomous NPC behavior requirements completely specified for all MVP NPC classes (Innkeeper, Blacksmith, Cook, Customer)? [Completeness, Spec §FR-003]
- [ ] CHK002 - Are possession lifecycle requirements defined for all transitions (idle → possessed → working → released → autonomous)? [Completeness, Spec §FR-004, §FR-008]
- [ ] CHK003 - Are game loop initialization and shutdown requirements documented (including offline progress triggers)? [Gap → See FR-013]
- [ ] CHK004 - Are NPC state persistence requirements defined for page refresh scenarios? [Gap]
- [ ] CHK005 - Are action availability requirements specified when NPC is mid-action during possession attempt? [Completeness, Edge Case]
- [ ] CHK006 - Are component lifecycle requirements defined for Blazor WASM context (OnInitialized, Dispose)? [Gap]
- [ ] CHK007 - Are LiteDB database initialization and connection requirements specified? [Gap]
- [ ] CHK008 - Are activity log retention requirements defined (max entries, cleanup strategy)? [Spec §FR-010 - clarify "last 20"]

## Requirement Clarity

Precision and measurability of requirements:

- [ ] CHK009 - Is "10 ticks/second" game loop requirement unambiguous about timer precision, drift tolerance, and fallback behavior? [Clarity, Spec §FR-002]
- [ ] CHK010 - Are action execution timers quantified with specific durations for each action type? [Clarity, Spec §FR-006 - "5-30 seconds" range needs specifics]
- [ ] CHK011 - Is "context-appropriate actions" operationally defined with explicit class-to-action mappings? [Clarity, Spec §FR-005]
- [ ] CHK012 - Are gold and reputation reward amounts precisely specified for each action? [Clarity, Spec §FR-007]
- [ ] CHK013 - Is "visible countdown" requirement quantified (update frequency, display format)? [Ambiguity, Spec §FR-006]
- [ ] CHK014 - Are NPCState enum transitions explicitly defined with entry/exit conditions? [Clarity, Data Model]
- [ ] CHK015 - Is "<50ms possession switch" requirement measurable with specific timing checkpoints? [Measurability, Plan §Performance Goals]
- [ ] CHK016 - Is "interesting enough to watch for 5+ minutes" operationalized with measurable criteria? [Ambiguity, Spec §SC-007]

## Requirement Consistency

Alignment and conflict resolution:

- [ ] CHK017 - Are NPC action requirements consistent between autonomous AI (NPCAIService) and manual possession (ExecuteActionAsync)? [Consistency, Spec §FR-003, §FR-006]
- [ ] CHK018 - Are action duration requirements consistent across spec (§FR-006: "5-30 seconds"), plan (§Performance: "<100ms switch"), and data model? [Consistency]
- [ ] CHK019 - Do Building.Resources requirements align with NPCAction.ResourceCosts validation rules? [Consistency, Data Model]
- [ ] CHK020 - Are GridPosition requirements consistent between Core.Engine.Spatial reuse and new Settlement/Building/NPC models? [Consistency, Plan §Reuse]
- [ ] CHK021 - Are IGameService extension requirements compatible with existing LocalGameService implementation pattern? [Consistency, Plan §Alignment]
- [ ] CHK022 - Do acceptance criteria (Spec §Acceptance Scenarios) align with functional requirements (§FR-001 through §FR-012)? [Traceability]

## Data Model Requirements Quality

Entity schema, validation, and persistence requirements:

- [ ] CHK023 - Are Settlement immutability requirements explicitly documented (use of `with` expressions, IReadOnlyList)? [Completeness, Data Model]
- [ ] CHK024 - Are validation rules specified for all NPC state transitions (Idle ↔ Working, state-action consistency)? [Completeness, Data Model §NPC]
- [ ] CHK025 - Are Building resource capacity constraints defined (min/max values, validation on consumption)? [Gap, Data Model §Building]
- [ ] CHK026 - Are NPCAction.RequiredClasses and RequiredBuildings filter requirements clearly defined? [Clarity, Data Model §NPCAction]
- [ ] CHK027 - Are LiteDB collection schema requirements documented (indexes, uniqueness constraints)? [Gap]
- [ ] CHK028 - Are entity relationship integrity requirements specified (e.g., NPC.CurrentBuilding → Building.Id validation)? [Completeness, Data Model]
- [ ] CHK029 - Is the Settlement factory method (CreateMillbrook) specification complete with all initial NPC positions, resources, and state? [Completeness, Data Model §Settlement]
- [ ] CHK030 - Are ActivityLogEntry persistence requirements defined (transient vs persisted, LiteDB storage)? [Ambiguity, Data Model - "transient, not persisted" conflicts with LiteDB usage]

## Testing Requirements Quality (TDD Non-Negotiable)

Completeness and clarity of test requirements:

- [ ] CHK031 - Are TDD red-green-refactor requirements explicitly mandated for all implementation tasks? [Completeness, Tasks.md §Tests]
- [ ] CHK032 - Are unit test requirements specified for all foundational services (SimulationEngine, SettlementGameService, NPCAIService)? [Completeness, Tasks §Phase 2]
- [ ] CHK033 - Are bUnit component test requirements defined for all Blazor components (SettlementView, NPCSidebar, ActionPanel, ActivityLog)? [Completeness, Tasks §Phase 3, §Phase 4]
- [ ] CHK034 - Are integration test requirements specified for possession flow (possess → execute → release → verify autonomous resume)? [Completeness, Tasks T057]
- [ ] CHK035 - Are mock timer requirements documented for unit testing timer-based simulation? [Completeness, Research §3]
- [ ] CHK036 - Are test fixture requirements defined for Settlement, NPC, and Building test data? [Gap]
- [ ] CHK037 - Are assertion requirements specified for state immutability verification in tests? [Gap]
- [ ] CHK038 - Is the test failure requirement ("tests MUST fail before implementation") operationally defined with verification checkpoints? [Clarity, Tasks §Tests]

## Core Mechanics Requirements

Possession system, autonomous behavior, and game loop:

- [ ] CHK039 - Are possession request requirements defined (click target, validation, state change sequence)? [Completeness, Spec §FR-004]
- [ ] CHK040 - Are possession blocking requirements specified (e.g., cannot possess NPC that is already possessed)? [Gap, Edge Case]
- [ ] CHK041 - Are autonomous NPC action selection requirements algorithmically defined (priority-based selection logic)? [Clarity, Research §4]
- [ ] CHK042 - Are action completion notification requirements specified (UI update trigger, activity log entry creation)? [Gap, Spec §FR-007]
- [ ] CHK043 - Are offline progress requirements defined for tab visibility loss (calculation algorithm, time caps, state persistence)? [Gap → See FR-013b, FR-014]
- [ ] CHK044 - Are SimulationEngine timer disposal requirements specified for component unmount (stop timer, persist state for offline calculation)? [Gap → See FR-013c, CL-001]
- [ ] CHK045 - Are NPC AI update requirements quantified (update frequency, tick processing budget)? [Gap]
- [ ] CHK046 - Are "release control" requirements complete (state restoration, AI resumption, persistence)? [Completeness, Spec §FR-008]

## Non-Functional Requirements (Performance, Timing, Responsiveness)

Measurability and testability of NFRs:

- [ ] CHK047 - Can the "10 ticks/second" requirement be objectively verified with automated tests? [Measurability, Spec §FR-002, §SC-005]
- [ ] CHK048 - Is the "<100ms possession switch" requirement measurable with specific timing instrumentation? [Measurability, Plan §Performance Goals]
- [ ] CHK049 - Is the "<50ms action execution response" requirement testable with benchmarks? [Measurability, Spec §SC-006]
- [ ] CHK050 - Are browser performance requirements specified (target browsers, minimum hardware specs)? [Gap, Plan §Target Platform]
- [ ] CHK051 - Are LiteDB performance requirements defined (read/write latency targets, database size limits)? [Gap]
- [ ] CHK052 - Are UI frame rate requirements specified for 10 ticks/second simulation rendering? [Gap, Plan §Performance - "60 FPS UI updates"]
- [ ] CHK053 - Is the "<2s page load" requirement measurable with specific loading checkpoints? [Measurability, Plan §Performance Goals]

## Edge Case & Exception Coverage

Boundary conditions and error scenarios:

- [ ] CHK054 - Are "NPC already working" requirements defined (action button disabled, visual feedback)? [Completeness, Spec §Edge Cases]
- [ ] CHK055 - Are "insufficient materials" requirements specified (button state, tooltip content, resource validation)? [Completeness, Spec §Edge Cases]
- [ ] CHK056 - Are "possess NPC mid-action" requirements defined (timer continuation, progress display)? [Completeness, Spec §Edge Cases]
- [ ] CHK057 - Are "release possession mid-action" requirements specified (action continuation, completion notification)? [Completeness, Spec §Edge Cases]
- [ ] CHK058 - Are resource contention requirements documented (first-come-first-served for MVP)? [Completeness, Spec §Edge Cases]
- [ ] CHK059 - Are zero-state requirements defined (empty activity log, no favorited NPCs on first load)? [Gap]
- [ ] CHK060 - Are LiteDB corruption recovery requirements specified? [Gap, Exception Flow]
- [ ] CHK061 - Are requirements defined for Settlement.CreateMillbrook failure scenarios? [Gap, Exception Flow]
- [ ] CHK062 - Are timer overflow requirements specified (WorldTime advancing beyond DateTime.MaxValue)? [Gap, Edge Case]

## Acceptance Criteria Quality

Testability and measurability of success criteria:

- [ ] CHK063 - Can "user can identify autonomous NPCs within 30 seconds" be objectively measured? [Measurability, Spec §SC-001]
- [ ] CHK064 - Is "successfully possess and execute one action within 60 seconds" testable with automated scenarios? [Measurability, Spec §SC-002]
- [ ] CHK065 - Are "Given/When/Then" acceptance scenarios complete for all critical possession flows? [Completeness, Spec §US1, §US2]
- [ ] CHK066 - Are success criteria aligned with functional requirements (FR-001 through FR-012)? [Traceability, Spec §Success Criteria]
- [ ] CHK067 - Is "100% of playtesters understand concept" measurable with specific questionnaire items? [Measurability, Spec §SC-004]
- [ ] CHK068 - Are acceptance criteria defined for all edge cases documented in §Edge Cases? [Coverage, Gap]

## Dependencies & Assumptions

Infrastructure reuse and integration requirements:

- [ ] CHK069 - Are IGameService extension requirements backward-compatible with existing implementations? [Completeness, Plan §Reuse]
- [ ] CHK070 - Are IEventStore integration requirements specified for activity log persistence? [Gap, Plan §Reuse]
- [ ] CHK071 - Are GridPosition reuse requirements validated against Core.Engine.Spatial API? [Completeness, Plan §Reuse]
- [ ] CHK072 - Are InnGameLoop pattern adaptation requirements explicitly documented? [Clarity, Plan §Pattern]
- [ ] CHK073 - Are LocalGameService + LiteDB pattern dependencies validated? [Completeness, Plan §Alignment]
- [ ] CHK074 - Is the assumption "LiteDB works in Blazor WASM" validated with proof-of-concept? [Assumption]
- [ ] CHK075 - Is the assumption "System.Timers.Timer works in Blazor WASM single-threaded context" validated? [Assumption, Research §1]

## Offline Progress Requirements Quality

Idle game mechanics and offline calculation requirements:

- [ ] CHK076 - Are offline progress calculation requirements algorithmically defined with clear inputs/outputs? [Completeness → See FR-014a]
- [ ] CHK077 - Are offline time thresholds explicitly specified (minimum 60s, maximum 24h)? [Clarity → See FR-014b]
- [ ] CHK078 - Is the offline progress algorithm deterministic and testable? [Measurability, Algorithm Quality]
- [ ] CHK079 - Are possessed NPC handling requirements defined for offline calculation (clear possession state)? [Completeness → See FR-014a]
- [ ] CHK080 - Are resource consumption requirements specified during offline progress (no resources consumed for MVP)? [Clarity, Gap]
- [ ] CHK081 - Are requirements defined for NPCs with insufficient resources during offline calculation? [Completeness, Edge Case]
- [ ] CHK082 - Is the offline progress modal UI specification complete (layout, content, timing)? [Completeness → See FR-014c]
- [ ] CHK083 - Are offline progress persistence requirements specified (when to save, what to persist)? [Completeness, Gap]
- [ ] CHK084 - Are tab visibility detection requirements defined (Page Visibility API usage, fallback behavior)? [Completeness, Technical]
- [ ] CHK085 - Can offline progress timing accuracy be objectively verified (<100ms detection delay)? [Measurability]
- [ ] CHK086 - Are activity log requirements specified for offline progress (batch summary vs individual entries)? [Clarity]
- [ ] CHK087 - Are offline progress calculation performance requirements defined (max computation time <500ms)? [Gap, Non-Functional]
- [ ] CHK088 - Are requirements defined for concurrent offline calculations (rapid tab switching)? [Edge Case, Gap]
- [ ] CHK089 - Are offline progress error handling requirements specified (calculation failures, overflow scenarios)? [Gap, Exception Flow]
- [ ] CHK090 - Is the "Welcome back" modal dismissal behavior clearly defined (auto-dismiss timer, user interaction)? [Clarity]

---

## Summary

**Total Items**: 90
**Categories**: 10 (Added: Offline Progress Requirements Quality)
**Traceability**: 80/90 items (89%) include references to Spec, Plan, Data Model, Tasks, or Research docs

**Critical Gaps Addressed**:

- ✅ Game loop lifecycle requirements → Updated CHK003, CHK043, CHK044 with offline progress focus
- ⏳ LiteDB schema and persistence requirements (CHK007, CHK027, CHK051) - Next
- ⏳ Exception handling and recovery requirements (CHK060, CHK061) - Proposed requirements ready
- ⏳ Test fixture and assertion requirements (CHK036, CHK037) - Proposed requirements ready
- ⏳ Component lifecycle requirements (CHK006) - Proposed requirements ready

**New Critical Gaps (Offline Progress)**:

- Offline progress persistence strategy (CHK083)
- Resource consumption during offline calculation (CHK080)
- Offline progress error handling (CHK089)
- Performance requirements for calculation (CHK087)

**Ambiguities Requiring Clarification**:

- "Interesting to watch for 5+ minutes" measurability (CHK016)
- ActivityLogEntry persistence model (CHK030 - "transient" conflicts with LiteDB)
- "Visible countdown" update frequency (CHK013)
- Timer precision and drift tolerance (CHK009)

**Constitution Compliance**: All checklist items validate requirements quality (not implementation) per TDD Non-Negotiable principle.
