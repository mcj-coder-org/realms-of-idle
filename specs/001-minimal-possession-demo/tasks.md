---
description: 'Task list for Minimal Possession Demo v1 implementation'
---

# Tasks: Minimal Possession Demo v1

**Input**: Design documents from `/specs/001-minimal-possession-demo/`
**Prerequisites**: plan.md (complete), spec.md (complete), research.md (complete), data-model.md (complete), quickstart.md (complete)

**Tests**: Per [Constitution v1.0.0](../../.specify/memory/constitution.md) Principle I, Test-First Development is NON-NEGOTIABLE for all implementation work. Tests MUST be written before implementation code and MUST fail before implementing features (Red-Green-Refactor cycle).

**Organization**: Tasks are grouped by user story to enable independent implementation and testing of each story.

## Format: `[ID] [P?] [Story] Description`

- **[P]**: Can run in parallel (different files, no dependencies)
- **[Story]**: Which user story this task belongs to (e.g., US1, US2, US3)
- Include exact file paths in descriptions

## Path Conventions

- Project structure: `src/RealmsOfIdle.Client.Blazor/` (Blazor WASM client)
- Core library: `src/RealmsOfIdle.Core/` (shared game engine)
- Tests: `tests/RealmsOfIdle.Client.Blazor.Tests/`
- All paths relative to repository root

---

## Phase 1: Setup (Shared Infrastructure)

**Purpose**: Review existing infrastructure and prepare project for possession demo

- [ ] T001 Review existing IGameService interface in src/RealmsOfIdle.Core/Abstractions/IGameService.cs
- [ ] T002 Review existing IEventStore interface in src/RealmsOfIdle.Core/Abstractions/IEventStore.cs
- [ ] T003 [P] Review existing GridPosition in src/RealmsOfIdle.Core/Engine/Spatial/GridPosition.cs
- [ ] T004 [P] Review LocalGameService pattern in src/RealmsOfIdle.Client.Shared/Services/LocalGameService.cs
- [ ] T005 [P] Review InnGameLoop pattern in src/RealmsOfIdle.Core/Scenarios/Inn/InnGameLoop.cs
- [ ] T006 Create Models directory: src/RealmsOfIdle.Client.Blazor/Models/
- [ ] T007 Create Services directory: src/RealmsOfIdle.Client.Blazor/Services/
- [ ] T008 Create Components directory structure: src/RealmsOfIdle.Client.Blazor/Components/
- [ ] T009 Create Pages directory: src/RealmsOfIdle.Client.Blazor/Pages/
- [ ] T010 Add LiteDB package reference to src/RealmsOfIdle.Client.Blazor/RealmsOfIdle.Client.Blazor.csproj

---

## Phase 2: Foundational (Blocking Prerequisites)

**Purpose**: Core infrastructure that MUST be complete before ANY user story can be implemented

**⚠️ CRITICAL**: No user story work can begin until this phase is complete

### Data Models (Foundational)

- [ ] T011 [P] Create BuildingType enum in src/RealmsOfIdle.Client.Blazor/Models/Building.cs
- [ ] T012 [P] Create Building record in src/RealmsOfIdle.Client.Blazor/Models/Building.cs (reuse GridPosition from Core)
- [ ] T013 [P] Create NPCState enum in src/RealmsOfIdle.Client.Blazor/Models/NPC.cs
- [ ] T014 [P] Create NPC record in src/RealmsOfIdle.Client.Blazor/Models/NPC.cs with factory methods
- [ ] T015 [P] Create NPCAction record in src/RealmsOfIdle.Client.Blazor/Models/NPCAction.cs
- [ ] T016 [P] Create ActionCatalog class in src/RealmsOfIdle.Client.Blazor/Models/NPCAction.cs (Reference: docs/design/content/actions/ for template structure, see plan.md Related Documentation section for details)
- [ ] T017 [P] Create Settlement record in src/RealmsOfIdle.Client.Blazor/Models/Settlement.cs with CreateMillbrook factory
- [ ] T018 [P] Create ActivityLogEntry record in src/RealmsOfIdle.Client.Blazor/Models/ActivityLogEntry.cs

### Core Services (Foundational)

- [ ] T019 Extend IGameService interface with possession operations in src/RealmsOfIdle.Core/Abstractions/IGameService.cs (PossessNPCAsync, ReleaseNPCAsync, ExecuteActionAsync)
- [ ] T020 Create SettlementGameService class in src/RealmsOfIdle.Client.Blazor/Services/SettlementGameService.cs (implements IGameService, adapts LocalGameService pattern)
- [ ] T021 Implement LiteDB persistence in SettlementGameService for Settlement state
- [ ] T022 Implement LiteDB persistence in SettlementGameService for ActivityLog
- [ ] T023 Create SimulationEngine class in src/RealmsOfIdle.Client.Blazor/Services/SimulationEngine.cs (adapts InnGameLoop pattern)
- [ ] T024 Implement game loop timer (System.Timers.Timer, 10 ticks/sec) in SimulationEngine
- [ ] T025 Create NPCAIService class in src/RealmsOfIdle.Client.Blazor/Services/NPCAIService.cs
- [ ] T026 Implement NPCAIService.UpdateNPC with simple state machine (Idle/Working)
- [ ] T027 Implement NPCAIService.ChooseNextAction with priority-based selection
- [ ] T028 Register SettlementGameService as IGameService singleton in src/RealmsOfIdle.Client.Blazor/Program.cs
- [ ] T029 Register SimulationEngine as singleton in src/RealmsOfIdle.Client.Blazor/Program.cs

### UI Foundation (Foundational)

- [ ] T030 Create PossessionDemo.razor page in src/RealmsOfIdle.Client.Blazor/Pages/PossessionDemo.razor with route /possession-demo
- [ ] T031 Create TopBar.razor component in src/RealmsOfIdle.Client.Blazor/Components/TopBar.razor (mode indicator, world time)
- [ ] T032 Inject IGameService and SimulationEngine in PossessionDemo.razor
- [ ] T033 Implement OnInitialized to start SimulationEngine and subscribe to state changes

**Checkpoint**: Foundation ready - user story implementation can now begin in parallel

---

## Phase 2.5: Offline Progress & Tab Visibility (Foundational) ⚠️ BLOCKING

**Purpose**: Implement offline progress calculation and tab visibility detection to enable idle game mechanics (required by FR-014, NFR-001)

**⚠️ CRITICAL**: This phase is BLOCKING for all user stories - implements core idle game mechanics and graceful degradation

### Tests for Offline Progress (REQUIRED - TDD NON-NEGOTIABLE) ⚠️

> **NOTE: Write these tests FIRST, ensure they FAIL before implementation**

- [ ] T130 [P] Unit test for OfflineProgressCalculator with <60s elapsed (no calculation) in tests/RealmsOfIdle.Client.Blazor.Tests/Services/OfflineProgressCalculatorTests.cs
- [ ] T131 [P] Unit test for OfflineProgressCalculator with 5 minutes elapsed (correct cycles) in tests/RealmsOfIdle.Client.Blazor.Tests/Services/OfflineProgressCalculatorTests.cs
- [ ] T132 [P] Unit test for OfflineProgressCalculator with 25 hours elapsed (24hr cap) in tests/RealmsOfIdle.Client.Blazor.Tests/Services/OfflineProgressCalculatorTests.cs
- [ ] T133 [P] Unit test for OfflineProgressCalculator clearing possessed NPC state in tests/RealmsOfIdle.Client.Blazor.Tests/Services/OfflineProgressCalculatorTests.cs
- [ ] T134 [P] Unit test for OfflineProgressCalculator with no resources (no actions) in tests/RealmsOfIdle.Client.Blazor.Tests/Services/OfflineProgressCalculatorTests.cs
- [ ] T135 [P] Unit test for OfflineProgressCalculator with multiple NPCs (independent calculation) in tests/RealmsOfIdle.Client.Blazor.Tests/Services/OfflineProgressCalculatorTests.cs
- [ ] T136 [P] Unit test for OfflineProgressCalculator immutability verification in tests/RealmsOfIdle.Client.Blazor.Tests/Services/OfflineProgressCalculatorTests.cs
- [ ] T137 [P] Unit test for TabVisibilityHandler event firing in tests/RealmsOfIdle.Client.Blazor.Tests/Services/TabVisibilityHandlerTests.cs
- [ ] T138 [P] bUnit component test for OfflineProgressModal rendering in tests/RealmsOfIdle.Client.Blazor.Tests/Components/OfflineProgressModalTests.cs

### Implementation for Offline Progress

- [ ] T139 [P] Create OfflineProgressCalculator.cs service in src/RealmsOfIdle.Client.Blazor/Services/OfflineProgressCalculator.cs
- [ ] T140 [P] Create TabVisibilityHandler.cs service in src/RealmsOfIdle.Client.Blazor/Services/TabVisibilityHandler.cs
- [ ] T141 [P] Create tab-visibility.js JavaScript module in src/RealmsOfIdle.Client.Blazor/wwwroot/js/tab-visibility.js
- [ ] T142 [P] Create OfflineProgressModal.razor component in src/RealmsOfIdle.Client.Blazor/Components/OfflineProgressModal.razor
- [ ] T143 Implement OfflineProgressCalculator.CalculateProgress with 60s minimum, 24hr cap, and cycle calculation (see plan.md PR-004 pseudocode)
- [ ] T144 Implement TabVisibilityHandler with JavaScript interop for Page Visibility API (document.hidden detection)
- [ ] T145 Implement OfflineProgressModal with time elapsed, actions per NPC, total rewards, and auto-dismiss after 30s
- [ ] T146 Register OfflineProgressCalculator as singleton in src/RealmsOfIdle.Client.Blazor/Program.cs
- [ ] T147 Register TabVisibilityHandler as singleton in src/RealmsOfIdle.Client.Blazor/Program.cs
- [ ] T148 Integrate TabVisibilityHandler in PossessionDemo.razor (OnAfterRenderAsync, OnDispose lifecycle)
- [ ] T149 Implement OnTabHidden handler in PossessionDemo.razor (stop SimulationEngine, persist Settlement state to LiteDB)
- [ ] T150 Implement OnTabVisible handler in PossessionDemo.razor (calculate offline progress if >= 60s, restart SimulationEngine)
- [ ] T151 Implement ErrorBoundary wrapper for PossessionDemo.razor with reload functionality (see plan.md CL-003)
- [ ] T152 Verify offline progress calculation with 5-minute tab hidden test (manual validation)
- [ ] T153 Verify activity log summary entry creation after offline progress (format: "While away: X actions, +Y gold")
- [ ] T154 Verify state persistence during tab hidden sequence (settlement state saved to LiteDB with WorldTime snapshot)

**Checkpoint**: Offline progress infrastructure complete - tab visibility detection works, offline progress calculation tested, error boundaries in place

---

## Phase 3: User Story 1 - Observer Mode (Priority: P1) 🎯 MVP

**Goal**: Experience the living world simulation running autonomously before any player interaction

**Independent Test**: Open the web app, watch for 30 seconds without clicking anything. NPCs perform actions, gold increases, activity log updates. World is alive.

### Tests for User Story 1 (REQUIRED - TDD NON-NEGOTIABLE) ⚠️

> **NOTE: Write these tests FIRST, ensure they FAIL before implementation**

- [ ] T034 [P] [US1] Unit test for Settlement.CreateMillbrook factory in tests/RealmsOfIdle.Client.Blazor.Tests/Models/SettlementTests.cs
- [ ] T035 [P] [US1] Unit test for SimulationEngine.Start and tick processing in tests/RealmsOfIdle.Client.Blazor.Tests/Services/SimulationEngineTests.cs
- [ ] T036 [P] [US1] Unit test for NPCAIService.ChooseNextAction for Innkeeper in tests/RealmsOfIdle.Client.Blazor.Tests/Services/NPCAIServiceTests.cs
- [ ] T037 [P] [US1] Unit test for NPCAIService.ChooseNextAction for Blacksmith in tests/RealmsOfIdle.Client.Blazor.Tests/Services/NPCAIServiceTests.cs
- [ ] T038 [P] [US1] bUnit component test for SettlementView rendering grid in tests/RealmsOfIdle.Client.Blazor.Tests/Components/SettlementViewTests.cs
- [ ] T039 [P] [US1] bUnit component test for ActivityLog displaying entries in tests/RealmsOfIdle.Client.Blazor.Tests/Components/ActivityLogTests.cs

### Implementation for User Story 1

- [ ] T040 [P] [US1] Create SettlementView.razor component in src/RealmsOfIdle.Client.Blazor/Components/SettlementView.razor (top-level container)
- [ ] T041 [P] [US1] Create SettlementMap.razor component in src/RealmsOfIdle.Client.Blazor/Components/SettlementMap.razor (10x10 grid with buildings/NPCs)
- [ ] T042 [P] [US1] Create ActivityLog.razor component in src/RealmsOfIdle.Client.Blazor/Components/ActivityLog.razor (scrolling action history)
- [ ] T043 [US1] Implement SettlementMap rendering of 2 buildings (Inn, Workshop) with GridPosition
- [ ] T044 [US1] Implement SettlementMap rendering of 4 NPCs (Mara, Cook, Tomas, Customer) with positions
- [ ] T045 [US1] Implement ActivityLog with auto-scroll and max 20 entries
- [ ] T046 [US1] Wire SettlementView to SettlementGameService for state updates
- [ ] T047 [US1] Implement autonomous NPC action execution in NPCAIService (Innkeeper serves customers)
- [ ] T048 [US1] Implement autonomous NPC action execution in NPCAIService (Blacksmith crafts swords)
- [ ] T049 [US1] Add gold/reputation reward application on action completion
- [ ] T050 [US1] Add activity log entries when NPCs complete actions
- [ ] T051 [US1] Verify state persists in LiteDB across page refreshes

**Checkpoint**: At this point, User Story 1 should be fully functional and testable independently. NPCs work autonomously, state persists, activity log updates.

---

## Phase 4: User Story 2 - Possess and Control NPC (Priority: P1) 🎯 MVP

**Goal**: Take direct control of an NPC to experience their role and perform actions manually

**Independent Test**: Click Mara's portrait → click "Possess" → see action panel with inn-specific actions → execute "Serve Customer" → watch timer count down → see result (+5 gold).

### Tests for User Story 2 (REQUIRED - TDD NON-NEGOTIABLE) ⚠️

- [ ] T052 [P] [US2] Unit test for SettlementGameService.PossessNPCAsync in tests/RealmsOfIdle.Client.Blazor.Tests/Services/SettlementGameServiceTests.cs
- [ ] T053 [P] [US2] Unit test for SettlementGameService.ReleaseNPCAsync in tests/RealmsOfIdle.Client.Blazor.Tests/Services/SettlementGameServiceTests.cs
- [ ] T054 [P] [US2] Unit test for SettlementGameService.ExecuteActionAsync in tests/RealmsOfIdle.Client.Blazor.Tests/Services/SettlementGameServiceTests.cs
- [ ] T055 [P] [US2] bUnit component test for NPCSidebar rendering NPC portraits in tests/RealmsOfIdle.Client.Blazor.Tests/Components/NPCSidebarTests.cs
- [ ] T056 [P] [US2] bUnit component test for ActionPanel showing available actions in tests/RealmsOfIdle.Client.Blazor.Tests/Components/ActionPanelTests.cs
- [ ] T057 [P] [US2] Integration test for possession flow (possess → execute → release) in tests/RealmsOfIdle.Client.Blazor.Tests/Integration/PossessionFlowTests.cs

### Implementation for User Story 2

- [ ] T058 [P] [US2] Create NPCSidebar.razor component in src/RealmsOfIdle.Client.Blazor/Components/NPCSidebar.razor (NPC list with portraits)
- [ ] T059 [P] [US2] Create ActionPanel.razor component in src/RealmsOfIdle.Client.Blazor/Components/ActionPanel.razor (available actions when possessed)
- [ ] T060 [US2] Implement NPCSidebar rendering 4 NPC cards with names, classes, locations
- [ ] T061 [US2] Implement "Possess" button on each NPC portrait in NPCSidebar
- [ ] T062 [US2] Implement PossessNPCAsync in SettlementGameService (set IsPossessed = true, pause autonomous AI)
- [ ] T063 [US2] Implement ActionPanel to show context-appropriate actions for possessed NPC
- [ ] T064 [US2] Implement action button click handler in ActionPanel
- [ ] T065 [US2] Implement ExecuteActionAsync in SettlementGameService (start action timer, update NPC state)
- [ ] T066 [US2] Implement action timer countdown display in ActionPanel
- [ ] T067 [US2] Implement action completion handler (apply rewards, update UI, log activity)
- [ ] T068 [US2] Implement "Release Control" button in ActionPanel
- [ ] T069 [US2] Implement ReleaseNPCAsync in SettlementGameService (set IsPossessed = false, resume autonomous AI)
- [ ] T070 [US2] Update TopBar to show possession status ("Possessed: Mara [Innkeeper] Lvl 5")
- [ ] T071 [US2] Verify possessed NPC continues autonomous behavior after release

**Checkpoint**: At this point, User Stories 1 AND 2 should both work independently. Can possess NPCs, execute actions manually, release control.

---

## Phase 5: User Story 3 - Context-Aware Actions (Priority: P2)

**Goal**: Experience how available actions change based on NPC class and current building location

**Independent Test**: Possess Mara at Inn (see innkeeper actions) → release → possess Tomas at Workshop (see blacksmith actions) → understand context dependency.

### Tests for User Story 3 (REQUIRED - TDD NON-NEGOTIABLE) ⚠️

- [ ] T072 [P] [US3] Unit test for action filtering by NPC class in tests/RealmsOfIdle.Client.Blazor.Tests/Services/ActionFilterTests.cs
- [ ] T073 [P] [US3] Unit test for action filtering by building type in tests/RealmsOfIdle.Client.Blazor.Tests/Services/ActionFilterTests.cs
- [ ] T074 [P] [US3] Integration test for context-aware action display (Innkeeper at Inn vs Workshop) in tests/RealmsOfIdle.Client.Blazor.Tests/Integration/ContextAwareActionsTests.cs

### Implementation for User Story 3

- [ ] T075 [P] [US3] Add RequiredClasses validation to NPCAction filtering logic in SettlementGameService
- [ ] T076 [P] [US3] Add RequiredBuildings validation to NPCAction filtering logic in SettlementGameService
- [ ] T077 [US3] Implement GetAvailableActions method in SettlementGameService (filters by class + building)
- [ ] T078 [US3] Update ActionPanel to call GetAvailableActions and render filtered list
- [ ] T079 [US3] Add Innkeeper-specific actions to ActionCatalog (Serve Customer, Manage Cook, Check Income)
- [ ] T080 [US3] Add Blacksmith-specific actions to ActionCatalog (Craft Iron Sword, Check Materials, Set Priority)
- [ ] T081 [US3] Add Customer-generic actions to ActionCatalog (Order Drink)
- [ ] T082 [US3] Verify Tomas at Forge sees blacksmith actions
- [ ] T083 [US3] Verify Tomas at Inn does NOT see blacksmith actions (only customer actions)
- [ ] T084 [US3] Add resource cost validation (disable "Craft Iron Sword" if insufficient IronOre)

**Checkpoint**: All context-aware action filtering should now be functional. Different NPCs in different buildings see different actions.

---

## Phase 6: User Story 4 - Persistent Priority Changes (Priority: P2)

**Goal**: Adjust NPC priorities while possessed and confirm changes persist after releasing control

**Independent Test**: Possess Mara → click "Manage Cook" → adjust priority slider to 80% Quality → release → observe Cook's behavior follows new priority.

### Tests for User Story 4 (REQUIRED - TDD NON-NEGOTIABLE) ⚠️

- [ ] T085 [P] [US4] Unit test for NPC priority updates in tests/RealmsOfIdle.Client.Blazor.Tests/Models/NPCTests.cs
- [ ] T086 [P] [US4] Unit test for priority persistence in LiteDB in tests/RealmsOfIdle.Client.Blazor.Tests/Services/SettlementGameServiceTests.cs
- [ ] T087 [P] [US4] Integration test for priority affecting NPC behavior in tests/RealmsOfIdle.Client.Blazor.Tests/Integration/PriorityPersistenceTests.cs

### Implementation for User Story 4

- [ ] T088 [P] [US4] Create PriorityPanel.razor component in src/RealmsOfIdle.Client.Blazor/Components/PriorityPanel.razor
- [ ] T089 [US4] Implement "Manage Cook" action to open PriorityPanel for Cook NPC
- [ ] T090 [US4] Implement priority slider (Speed vs Quality) in PriorityPanel
- [ ] T091 [US4] Implement UpdateNPCPriorities method in SettlementGameService
- [ ] T092 [US4] Wire "Apply" button to UpdateNPCPriorities and persist to LiteDB
- [ ] T093 [US4] Update NPCAIService to respect NPC priorities when choosing actions
- [ ] T094 [US4] Add confirmation message "Cook priorities updated. Changes persist after release."
- [ ] T095 [US4] Verify priority changes survive page refresh (LiteDB persistence)
- [ ] T096 [US4] Verify Cook's autonomous behavior reflects new priority after release

**Checkpoint**: Priority changes should persist and affect NPC behavior. Demonstrates lasting influence on NPCs.

---

## Phase 7: User Story 5 - Favorites System (Priority: P3)

**Goal**: Bookmark frequently-used NPCs for quick access and receive notifications about their activities

**Independent Test**: Click star icon on Mara → she appears in Favorites panel → quick-possess from favorites → notification badge appears when she completes action while not possessed.

### Tests for User Story 5 (REQUIRED - TDD NON-NEGOTIABLE) ⚠️

- [ ] T097 [P] [US5] Unit test for favorites list management in tests/RealmsOfIdle.Client.Blazor.Tests/Services/FavoritesServiceTests.cs
- [ ] T098 [P] [US5] Unit test for notification badge logic in tests/RealmsOfIdle.Client.Blazor.Tests/Services/NotificationServiceTests.cs
- [ ] T099 [P] [US5] bUnit component test for FavoritesPanel rendering in tests/RealmsOfIdle.Client.Blazor.Tests/Components/FavoritesPanelTests.cs

### Implementation for User Story 5

- [ ] T100 [P] [US5] Create FavoritesService class in src/RealmsOfIdle.Client.Blazor/Services/FavoritesService.cs
- [ ] T101 [P] [US5] Create NotificationService class in src/RealmsOfIdle.Client.Blazor/Services/NotificationService.cs
- [ ] T102 [P] [US5] Create FavoritesPanel.razor component in src/RealmsOfIdle.Client.Blazor/Components/FavoritesPanel.razor
- [ ] T103 [US5] Implement FavoritesService.AddFavorite and RemoveFavorite with LiteDB persistence
- [ ] T104 [US5] Add star icon (⭐) to each NPC portrait in NPCSidebar
- [ ] T105 [US5] Wire star icon click to FavoritesService.AddFavorite
- [ ] T106 [US5] Implement FavoritesPanel to display favorited NPCs
- [ ] T107 [US5] Implement quick-possess from FavoritesPanel (click name → immediate possession)
- [ ] T108 [US5] Implement NotificationService to track unread actions for favorited NPCs
- [ ] T109 [US5] Add notification badge to FavoritesPanel entries when NPC completes action while not possessed
- [ ] T110 [US5] Register FavoritesService and NotificationService in Program.cs
- [ ] T111 [US5] Verify favorites persist across page refreshes (LiteDB)

**Checkpoint**: All user stories should now be independently functional. Favorites system provides quality-of-life improvement.

---

## Phase 8: Polish & Cross-Cutting Concerns

**Purpose**: Improvements that affect multiple user stories

- [ ] T112 [P] Add CSS styling for possession demo in src/RealmsOfIdle.Client.Blazor/wwwroot/css/possession-demo.css
- [ ] T113 [P] Add responsive layout adjustments for desktop browsers
- [ ] T114 [P] Add loading spinner while settlement initializes
- [ ] T115 [P] Add error boundary component for graceful error handling in src/RealmsOfIdle.Client.Blazor/Components/ErrorBoundary.razor
- [ ] T116 [P] Add tooltips for action buttons explaining requirements
- [ ] T117 [P] Add visual feedback for disabled actions (red with tooltip)
- [ ] T118 [P] Add animation for NPC portraits when possessed
- [ ] T119 [P] Add sound effects for action completion (optional, low priority)
- [ ] T120 Code cleanup: Remove console.log statements, add proper logging
- [ ] T121 Code cleanup: Refactor duplicate code in NPCAIService
- [ ] T122 Performance: Optimize SettlementMap rendering to avoid re-rendering entire grid
- [ ] T123 Performance: Add memoization for GetAvailableActions filtering
- [ ] T124 [P] Add XML documentation comments to public APIs
- [ ] T125 [P] Add README.md to possession demo explaining architecture
- [ ] T126 [P] Run quickstart.md validation manually
- [ ] T127 Security: Validate NPC IDs before possession to prevent invalid access

### Test Automation Hooks & Basic Accessibility (NFR-002)

**Purpose**: Add ARIA labels and semantic markup primarily for automated UI testing (Playwright, Selenium) rather than full accessibility compliance

#### NFR-002a - ARIA Labels for Test Selectors

- [ ] T128 [P] Add aria-label to NPC portraits (enables test getByLabel queries) in src/RealmsOfIdle.Client.Blazor/Components/NPCSidebar.razor
- [ ] T129 [P] Add aria-label to action buttons (stable test selectors) in src/RealmsOfIdle.Client.Blazor/Components/ActionPanel.razor
- [ ] T130 [P] Add aria-label to settlement map and major components in src/RealmsOfIdle.Client.Blazor/Components/SettlementMap.razor
- [ ] T131 [P] Add data-testid fallback attributes to components without aria-label throughout all components

#### NFR-002b - Semantic HTML for Test Framework Compatibility

- [ ] T132 [P] Use <button> elements for all interactive actions (improves test reliability) throughout all components
- [ ] T133 [P] Use <ul>/<ol> for lists (NPC roster, actions, activity log) for test framework navigation
- [ ] T134 [P] Add heading hierarchy (h1, h2, h3) for landmark navigation in tests in src/RealmsOfIdle.Client.Blazor/Pages/PossessionDemo.razor

#### NFR-002c - ARIA Roles for Component Identification

- [ ] T135 [P] Add role="grid" to settlement map in src/RealmsOfIdle.Client.Blazor/Components/SettlementMap.razor
- [ ] T136 [P] Add role="toolbar" to action panel in src/RealmsOfIdle.Client.Blazor/Components/ActionPanel.razor
- [ ] T137 [P] Add role="dialog" and aria-modal="true" to modals in src/RealmsOfIdle.Client.Blazor/Components/OfflineProgressModal.razor

#### NFR-002d - ARIA States for Test Assertions

- [ ] T138 [P] Add aria-pressed="true" to possessed NPC portraits (test can assert possession state) in src/RealmsOfIdle.Client.Blazor/Components/NPCSidebar.razor
- [ ] T139 [P] Add aria-disabled="true" to unavailable actions (test can verify state) in src/RealmsOfIdle.Client.Blazor/Components/ActionPanel.razor
- [ ] T140 [P] Add aria-busy="true" to loading states (test can wait for completion) in src/RealmsOfIdle.Client.Blazor/Pages/PossessionDemo.razor

#### NFR-002e - Error Detection for Tests

- [ ] T141 [P] Add role="alert" to error messages (test framework can detect errors) in src/RealmsOfIdle.Client.Blazor/Components/ErrorBoundary.razor
- [ ] T142 [P] Add aria-live="assertive" to critical errors (triggers test listeners) throughout error handling components

#### NFR-002f - Documentation

- [ ] T143 [P] Document resolution support (mobile 360x640 to ultrawide 3440x1440, high DPI 1x-4x) and accessibility limitations in README.md (vision/dexterity required, no screen reader support)

---

## Phase 9: NPC Hiring & Wage System + Resource Economy

**Purpose**: Add economic pressure via NPC contracts and wages, creating hiring decisions. Add resource production/consumption to create supply chain dependencies.

**Alignment**: Implements FR-015 (NPC Hiring & Contract System) and FR-016 (Resource Economy)

### Tests for NPC Hiring & Wage System (REQUIRED - TDD NON-NEGOTIABLE) ⚠️

- [ ] T155 [P] Unit test for contract cost validation in SettlementGameService (can afford, insufficient funds) in tests/RealmsOfIdle.Client.Blazor.Tests/Services/SettlementGameServiceTests.cs
- [ ] T156 [P] Unit test for wage payment cycle (daily deduction, grace period, quit after 1 day unpaid) in tests/RealmsOfIdle.Client.Blazor.Tests/Services/WageServiceTests.cs
- [ ] T157 [P] Unit test for hiring NPC (gold deduction, employment status change, staff roster update) in tests/RealmsOfIdle.Client.Blazor.Tests/Services/HiringServiceTests.cs
- [ ] T158 [P] Unit test for NPC quit on unpaid wages (status change, return to available pool) in tests/RealmsOfIdle.Client.Blazor.Tests/Services/WageServiceTests.cs
- [ ] T159 [P] bUnit test for Available NPCs panel rendering (unemployed NPCs, hire button states) in tests/RealmsOfIdle.Client.Blazor.Tests/Components/AvailableNPCsPanelTests.cs
- [ ] T160 [P] bUnit test for building treasury display in TopBar or sidebar in tests/RealmsOfIdle.Client.Blazor.Tests/Components/BuildingTreasuryDisplayTests.cs

### Implementation for NPC Hiring & Wage System

- [ ] T161 [P] Add Gold and EmployedNPCs fields to Building record in src/RealmsOfIdle.Client.Blazor/Models/Building.cs
- [ ] T162 [P] Add EmploymentStatus, HireDate, and Traits fields to NPC record in src/RealmsOfIdle.Client.Blazor/Models/NPC.cs
- [ ] T163 [P] Add LastWagePayment field to Settlement record in src/RealmsOfIdle.Client.Blazor/Models/Settlement.cs
- [ ] T164 [P] Create NPCTrait and TraitEffect records in src/RealmsOfIdle.Client.Blazor/Models/NPC.cs
- [ ] T165 Create HiringService class in src/RealmsOfIdle.Client.Blazor/Services/HiringService.cs (validate contract cost, hire NPC, update building gold)
- [ ] T166 Create WageService class in src/RealmsOfIdle.Client.Blazor/Services/WageService.cs (daily cycle check, deduct wages, handle unpaid state)
- [ ] T167 Integrate WageService into SimulationEngine daily tick (check elapsed days, trigger wage payment)
- [ ] T168 [P] Create AvailableNPCsPanel.razor in src/RealmsOfIdle.Client.Blazor/Components/AvailableNPCsPanel.razor (list unemployed NPCs, hire buttons)
- [ ] T169 [P] Create BuildingTreasuryDisplay.razor in src/RealmsOfIdle.Client.Blazor/Components/BuildingTreasuryDisplay.razor or update TopBar.razor
- [ ] T170 [P] Create HireNPCModal.razor confirmation dialog in src/RealmsOfIdle.Client.Blazor/Components/HireNPCModal.razor
- [ ] T171 Update Settlement.CreateMillbrook factory to set initial employment states (Mara employed, others available)
- [ ] T172 Update offline progress calculation to apply wage deductions for elapsed days in src/RealmsOfIdle.Client.Blazor/Services/OfflineProgressCalculator.cs
- [ ] T173 Add wage payment notifications to ActivityLog (daily payments, warnings, quit events)
- [ ] T174 Add hire/fire actions to ActionCatalog (available when possessing building owner)
- [ ] T175 Update NPCAIService to handle unemployed NPCs (idle in Town Square, no work actions)
- [ ] T176 Persist employment status and building gold to LiteDB (update SaveSettlementAsync)

### Tests for Resource Economy (REQUIRED - TDD NON-NEGOTIABLE) ⚠️

- [ ] T177 [P] Unit test for resource consumption validation (has resources, insufficient resources) in tests/RealmsOfIdle.Client.Blazor.Tests/Services/ResourceServiceTests.cs
- [ ] T178 [P] Unit test for resource production (Cook produces Food, adds to building inventory) in tests/RealmsOfIdle.Client.Blazor.Tests/Services/ResourceServiceTests.cs
- [ ] T179 [P] Unit test for action blocking when resources unavailable (Serve Customer fails if no Food) in tests/RealmsOfIdle.Client.Blazor.Tests/Services/ResourceServiceTests.cs
- [ ] T180 [P] Unit test for resource capacity limits (Food production stops at 50/50) in tests/RealmsOfIdle.Client.Blazor.Tests/Services/ResourceServiceTests.cs
- [ ] T181 [P] Unit test for offline resource consumption (limit cycles by resource availability) in tests/RealmsOfIdle.Client.Blazor.Tests/Services/OfflineProgressCalculatorTests.cs

### Implementation for Resource Economy

- [ ] T182 [P] Add ResourceProduced field to NPCAction record in src/RealmsOfIdle.Client.Blazor/Models/NPCAction.cs
- [ ] T183 Update ActionCatalog with resource costs and production (ServeCustomer consumes Food, ProduceFood produces Food)
- [ ] T184 Create ResourceService class in src/RealmsOfIdle.Client.Blazor/Services/ResourceService.cs (check availability, consume, produce, enforce caps)
- [ ] T185 Integrate ResourceService into SettlementGameService.ExecuteActionAsync (validate resources before action, consume on start, produce on completion)
- [ ] T186 Update NPCAIService to check resource availability before choosing action (skip actions with insufficient resources)
- [ ] T187 Update offline progress calculation (OfflineProgressCalculator) to cap cycles by resource availability
- [ ] T188 [P] Update SettlementMap building tooltips to show resource counts
- [ ] T189 [P] Update ActionPanel to show resource requirements and availability
- [ ] T190 Add resource production/consumption notifications to ActivityLog
- [ ] T191 Update Settlement.CreateMillbrook to set initial resources (Inn: 0 Food, Workshop: 10 IronOre)

**Checkpoint**: Economic systems (hiring + resources) functional. Hiring decisions matter, resource scarcity creates gameplay tension.

---

## Phase 10: Building Upgrades

**Purpose**: Add building progression system to provide long-term goals and settlement growth

**Alignment**: Implements FR-017 (Building Upgrade System)

### Tests for Building Upgrades (REQUIRED - TDD NON-NEGOTIABLE) ⚠️

- [ ] T192 [P] Unit test for upgrade cost validation (can afford, insufficient funds) in tests/RealmsOfIdle.Client.Blazor.Tests/Services/UpgradeServiceTests.cs
- [ ] T193 [P] Unit test for upgrade time tracking (start, progress, completion) in tests/RealmsOfIdle.Client.Blazor.Tests/Services/UpgradeServiceTests.cs
- [ ] T194 [P] Unit test for upgrade benefits application (food storage increase, speed boost) in tests/RealmsOfIdle.Client.Blazor.Tests/Services/UpgradeServiceTests.cs
- [ ] T195 [P] bUnit test for BuildingUpgradePanel rendering (upgrade button, progress bar) in tests/RealmsOfIdle.Client.Blazor.Tests/Components/BuildingUpgradePanelTests.cs

### Implementation for Building Upgrades

- [ ] T196 [P] Add Level and UpgradeInProgress fields to Building record in src/RealmsOfIdle.Client.Blazor/Models/Building.cs
- [ ] T197 [P] Create BuildingUpgrade record in src/RealmsOfIdle.Client.Blazor/Models/BuildingUpgrade.cs (cost, duration, benefits)
- [ ] T198 Create UpgradeService class in src/RealmsOfIdle.Client.Blazor/Services/UpgradeService.cs (validate cost, start upgrade, apply benefits)
- [ ] T199 Integrate UpgradeService into SimulationEngine (track upgrade timers, complete on elapsed)
- [ ] T200 [P] Create BuildingUpgradePanel.razor in src/RealmsOfIdle.Client.Blazor/Components/BuildingUpgradePanel.razor (upgrade button, progress bar)
- [ ] T201 Add upgrade actions to ActionCatalog (available when possessing building owner)
- [ ] T202 Update offline progress calculation to complete upgrades if elapsed time > duration
- [ ] T203 Add upgrade notifications to ActivityLog (started, completed)

**Checkpoint**: Building upgrades functional. Players can invest building gold to unlock capacity/speed increases.

---

## Phase 11: Observer-Friendly Tutorial System

**Purpose**: Guide first-time players through possession mechanic WITHOUT disrupting observer mode

**Alignment**: Implements FR-018 (Contextual Tutorial System)

### Tests for Tutorial System (REQUIRED - TDD NON-NEGOTIABLE) ⚠️

- [ ] T204 [P] Unit test for tutorial state persistence (localStorage save/load/reset) in tests/RealmsOfIdle.Client.Blazor.Tests/Services/TutorialServiceTests.cs
- [ ] T205 [P] bUnit test for TutorialModal rendering (steps, highlights, skip button) in tests/RealmsOfIdle.Client.Blazor.Tests/Components/TutorialModalTests.cs
- [ ] T206 Integration test for tutorial trigger (only on first Mara possession, not page load) in tests/RealmsOfIdle.Client.Blazor.Tests/Integration/TutorialFlowTests.cs

### Implementation for Tutorial System

- [ ] T207 Create TutorialService class in src/RealmsOfIdle.Client.Blazor/Services/TutorialService.cs (check completion, mark complete, get next step)
- [ ] T208 [P] Create TutorialModal.razor in src/RealmsOfIdle.Client.Blazor/Components/TutorialModal.razor (modal dialog, step content, highlight system)
- [ ] T209 [P] Create tutorial-highlights.css for glowing/pulsing UI elements in src/RealmsOfIdle.Client.Blazor/wwwroot/css/tutorial-highlights.css
- [ ] T210 Integrate TutorialService into PossessionDemo.razor (trigger on first Mara possession only)
- [ ] T211 Add localStorage persistence for tutorial completion state
- [ ] T212 Add tutorial skip functionality (button + Escape key)
- [ ] T213 Test that observer mode works without tutorial interruption (open page, don't possess, watch NPCs work)

**Checkpoint**: Tutorial guides new players through possession mechanic without disrupting observer mode.

---

## Phase 12: Performance & Playtesting Validation

**Purpose**: Validate technical performance and player engagement before MVP release

**Alignment**: Implements FR-020 (Performance Benchmarks) and FR-021 (Playtesting Validation)

### Performance Tests (REQUIRED - TDD NON-NEGOTIABLE) ⚠️

- [ ] T214 [P] Performance test: Game loop tick rate stability (10 min run, assert <5% variance) in tests/RealmsOfIdle.Client.Blazor.Tests/Performance/GameLoopPerformanceTests.cs
- [ ] T215 [P] Performance test: Memory leak detection (10 min run, assert <10MB heap growth) in tests/RealmsOfIdle.Client.Blazor.Tests/Performance/MemoryLeakTests.cs
- [ ] T216 [P] Performance test: CPU usage (assert <20% CPU on mid-range device) in tests/RealmsOfIdle.Client.Blazor.Tests/Performance/CPUUsageTests.cs
- [ ] T217 Cross-browser E2E: Run US1-US5 tests on Chrome (assert all pass)
- [ ] T218 Cross-browser E2E: Run US1-US5 tests on Firefox (assert all pass)
- [ ] T219 Cross-browser E2E: Run US1-US5 tests on Edge (assert all pass)

### Playtesting Tasks

- [ ] T220 Manual playtest: Recruit 5 users, record session length (target: avg >5 min)
- [ ] T221 Manual playtest: Survey comprehension (target: 80% understand possession)
- [ ] T222 Manual playtest: Survey engagement (target: 60% would play 30 min)
- [ ] T223 Analyze heatmap clicks (identify confusing UI areas)
- [ ] T224 Document playtest findings in playtest-results.md

### Fixes Based on Performance/Playtesting

- [ ] T225 Fix performance regressions identified in T214-T216
- [ ] T226 Fix cross-browser compatibility issues from T217-T219
- [ ] T227 Improve UX based on playtest feedback from T220-T224

**Checkpoint**: MVP passes performance benchmarks, cross-browser tests, and playtesting validation. Ready for release.

---

## Dependencies & Execution Order

### Phase Dependencies

- **Setup (Phase 1)**: No dependencies - can start immediately
- **Foundational (Phase 2)**: Depends on Setup completion - BLOCKS all user stories
- **Offline Progress (Phase 2.5)**: Depends on Foundational completion - BLOCKS all user stories (idle game mechanics + error handling)
- **User Stories (Phase 3-7)**: All depend on Foundational (Phase 2) AND Offline Progress (Phase 2.5) completion
  - User Story 1 (P1): Can start after Phase 2.5 - No dependencies on other stories
  - User Story 2 (P1): Can start after Phase 2.5 - Builds on US1 but independently testable
  - User Story 3 (P2): Can start after Phase 2.5 - Enhances US2 possession with context filtering
  - User Story 4 (P2): Can start after Phase 2.5 - Enhances US2 possession with priority changes
  - User Story 5 (P3): Can start after Phase 2.5 - Quality of life feature, no story dependencies
- **Polish (Phase 8)**: Depends on all desired user stories being complete
- **NPC Hiring & Resource Economy (Phase 9)**: Depends on Phases 2, 2.5, 3, 4 completion (requires observer mode + possession + resource display)
- **Building Upgrades (Phase 10)**: Depends on Phase 9 completion (requires building treasury from hiring system)
- **Tutorial System (Phase 11)**: Depends on Phase 4 completion (requires possession mechanics), can run in parallel with Phases 9-10
- **Performance & Validation (Phase 12)**: Depends on all desired features being complete (final validation phase)

### User Story Dependencies

- **User Story 1 (P1) - Observer Mode**: Can start after Foundational (Phase 2) AND Offline Progress (Phase 2.5) - No dependencies on other stories - **RECOMMENDED MVP**
- **User Story 2 (P1) - Possess and Control**: Can start after Foundational (Phase 2) AND Offline Progress (Phase 2.5) - Builds on US1 simulation but independently testable - **RECOMMENDED MVP**
- **User Story 3 (P2) - Context-Aware Actions**: Can start after Foundational (Phase 2) AND Offline Progress (Phase 2.5) - Enhances US2 but can be tested independently
- **User Story 4 (P2) - Persistent Priorities**: Can start after Foundational (Phase 2) AND Offline Progress (Phase 2.5) - Enhances US2 but can be tested independently
- **User Story 5 (P3) - Favorites System**: Can start after Foundational (Phase 2) AND Offline Progress (Phase 2.5) - No dependencies on other stories, purely additive

### Within Each User Story

- Tests MUST be written and FAIL before implementation (TDD NON-NEGOTIABLE)
- Models before services (T011-T018 before T019-T029 in Phase 2)
- Services before components (T019-T029 before T030-T033 in Phase 2)
- Core implementation before enhancements
- Story complete before moving to next priority

### Parallel Opportunities

- All Setup tasks marked [P] can run in parallel (T003, T004, T005)
- All Foundational data models marked [P] can run in parallel (T011-T018)
- All Foundational tests for a user story marked [P] can run in parallel
- Once Foundational phase completes, all user stories CAN start in parallel (if team capacity allows)
- Models within a story marked [P] can run in parallel
- Different user stories can be worked on in parallel by different team members

---

## Parallel Example: User Story 1

```bash
# Launch all tests for User Story 1 together:
Task T034: "Unit test for Settlement.CreateMillbrook factory"
Task T035: "Unit test for SimulationEngine.Start and tick processing"
Task T036: "Unit test for NPCAIService.ChooseNextAction for Innkeeper"
Task T037: "Unit test for NPCAIService.ChooseNextAction for Blacksmith"
Task T038: "bUnit component test for SettlementView rendering grid"
Task T039: "bUnit component test for ActivityLog displaying entries"

# Launch all Razor components for User Story 1 together:
Task T040: "Create SettlementView.razor component"
Task T041: "Create SettlementMap.razor component"
Task T042: "Create ActivityLog.razor component"
```

---

## Parallel Example: User Story 2

```bash
# Launch all tests for User Story 2 together:
Task T052: "Unit test for SettlementGameService.PossessNPCAsync"
Task T053: "Unit test for SettlementGameService.ReleaseNPCAsync"
Task T054: "Unit test for SettlementGameService.ExecuteActionAsync"
Task T055: "bUnit component test for NPCSidebar rendering NPC portraits"
Task T056: "bUnit component test for ActionPanel showing available actions"

# Launch components together:
Task T058: "Create NPCSidebar.razor component"
Task T059: "Create ActionPanel.razor component"
```

---

## Implementation Strategy

### MVP First (User Stories 1 + 2 Only) - RECOMMENDED

1. Complete Phase 1: Setup (T001-T010)
2. Complete Phase 2: Foundational (T011-T033) - CRITICAL - blocks all stories
3. Complete Phase 2.5: Offline Progress & Tab Visibility (T130-T154) - CRITICAL - blocks all stories, implements idle game mechanics
4. **STOP and VALIDATE**: Test offline progress with 5-minute tab hidden test - progress calculated correctly
5. Complete Phase 3: User Story 1 - Observer Mode (T034-T051)
6. **STOP and VALIDATE**: Test User Story 1 independently - NPCs work autonomously, tab visibility works
7. Complete Phase 4: User Story 2 - Possess and Control (T052-T071)
8. **STOP and VALIDATE**: Test User Stories 1 AND 2 independently - Can possess and control NPCs, offline progress persists
9. Deploy/demo if ready - **THIS IS A COMPLETE, PLAYABLE MVP**

**Rationale**: User Stories 1 + 2 together demonstrate the core possession mechanic (autonomous NPCs + player control). Offline progress (Phase 2.5) is essential for idle game genre and graceful error handling. This is the minimum viable feature set to validate the architecture.

### Incremental Delivery

1. Complete Setup + Foundational + Offline Progress → Foundation ready with idle game mechanics
2. Add User Story 1 → Test independently → Deploy/Demo (Autonomous world with offline progress)
3. Add User Story 2 → Test independently → Deploy/Demo (MVP! Possession works with tab visibility handling)
4. Add User Story 3 → Test independently → Deploy/Demo (Context-aware actions)
5. Add User Story 4 → Test independently → Deploy/Demo (Persistent priorities)
6. Add User Story 5 → Test independently → Deploy/Demo (Favorites system)
7. Each story adds value without breaking previous stories

### Parallel Team Strategy

With multiple developers:

1. Team completes Setup + Foundational + Offline Progress together (T001-T033, T130-T154) - CRITICAL blocking infrastructure
2. Once Foundational AND Offline Progress are done:
   - Developer A: User Story 1 (T034-T051)
   - Developer B: User Story 2 (T052-T071) - can start in parallel if team capacity
   - Developer C: User Story 3 (T072-T084) - can start in parallel if team capacity
3. Stories complete and integrate independently

**Note**: User Stories 1 and 2 are both P1 (high priority) and together form the MVP. Recommend completing them sequentially to ensure autonomous behavior and offline progress work before adding possession.

---

## Notes

- [P] tasks = different files, no dependencies
- [Story] label maps task to specific user story for traceability
- Each user story should be independently completable and testable
- **TDD NON-NEGOTIABLE**: Verify tests fail (RED) before implementing (GREEN)
- Commit after each task or logical group
- Stop at any checkpoint to validate story independently
- Avoid: vague tasks, same file conflicts, cross-story dependencies that break independence
- **LiteDB persistence**: All state changes must persist to validate offline-first architecture
- **Reuse Core infrastructure**: GridPosition, IGameService, IEventStore, InnGameLoop patterns

---

## Task Count Summary

- **Total Tasks**: 227 (original 170 + 57 new tasks for Option B Full MVP)
- **Phase 1 (Setup)**: 10 tasks (T001-T010)
- **Phase 2 (Foundational)**: 23 tasks (T011-T033)
- **Phase 2.5 (Offline Progress & Tab Visibility)**: 25 tasks (T130-T154) ⚠️ BLOCKING
- **Phase 3 (User Story 1 - Observer Mode)**: 18 tasks (T034-T051)
- **Phase 4 (User Story 2 - Possess and Control)**: 20 tasks (T052-T071)
- **Phase 5 (User Story 3 - Context-Aware Actions)**: 13 tasks (T072-T084)
- **Phase 6 (User Story 4 - Persistent Priorities)**: 12 tasks (T085-T096)
- **Phase 7 (User Story 5 - Favorites System)**: 15 tasks (T097-T111)
- **Phase 8 (Polish & Test Automation Hooks)**: 34 tasks (T112-T143)
- **Phase 9 (NPC Hiring & Resource Economy)**: 37 tasks (T155-T191) - 11 tests + 26 implementation
- **Phase 10 (Building Upgrades)**: 12 tasks (T192-T203) - 4 tests + 8 implementation
- **Phase 11 (Tutorial System)**: 10 tasks (T204-T213) - 3 tests + 7 implementation
- **Phase 12 (Performance & Validation)**: 14 tasks (T214-T227) - 6 performance tests + 5 playtesting + 3 fixes

**MVP Scope (Original - Technical Demo)**: Phases 1 + 2 + 2.5 + 3 + 4 = 96 tasks (Setup + Foundation + Offline Progress + US1 + US2)

**Full MVP Scope (Option B - Playable Idle RPG)**: All phases (1-12) = 227 tasks total

- **Estimated Effort**: Original MVP (96 tasks) = 2-3 weeks | Full MVP (227 tasks) = 4-5 weeks total (+5-7 days for Phases 9-12)

**Test Automation Scope (NFR-002)**: 16 tasks focused on ARIA labels for Playwright/Selenium test stability (not full accessibility)

- NFR-002a: ARIA Labels for Test Selectors (4 tasks: T128-T131)
- NFR-002b: Semantic HTML for Test Compatibility (3 tasks: T132-T134)
- NFR-002c: ARIA Roles for Component Identification (3 tasks: T135-T137)
- NFR-002d: ARIA States for Test Assertions (3 tasks: T138-T140)
- NFR-002e: Error Detection for Tests (2 tasks: T141-T142)
- NFR-002f: Documentation (1 task: T143)

**Parallel Opportunities**: Approximately 90 tasks marked [P] can run in parallel when dependencies allow (includes new Option B tasks)

**Independent Test Criteria**:

- US1: NPCs work autonomously, activity log updates, state persists
- US2: Can possess NPC, execute action, release control, NPC resumes autonomous behavior
- US3: Different NPCs in different buildings see different actions
- US4: Priority changes persist after release and affect NPC behavior
- US5: Favorites persist, quick-possess works, notifications appear
