# Testing Requirements Quality Checklist: Automation First & E2E

**Purpose**: Validate requirements quality for Automation First approach, E2E test infrastructure, clean builds, process lifecycle management, logging, and observability to support diagnostics and debugging.

**Scope**: Complete testing ecosystem requirements (test infrastructure + production testability)
**Gate**: PR review gate (blocking merge, moderate rigor)
**Created**: 2026-02-10
**Feature**: Minimal Possession Demo v1

**Convention Enforcement**: Validates requirements coverage against:

- [testing-conventions.md](../../../docs/testing-conventions.md) - Test categorization, naming, validation gates
- [testing-strategy.md](../../../docs/testing-strategy.md) - BDD feature specifications, test execution modes, automation philosophy

---

## E2E Test Infrastructure Requirements Quality

### AppHost Lifecycle & Orchestration

- [ ] CHK001 - Are AppHost startup requirements defined with specific initialization sequence? [Completeness, Gap]
- [ ] CHK002 - Are AppHost shutdown requirements specified with cleanup order and timeout limits? [Completeness, Gap]
- [ ] CHK003 - Are AppHost dependency orchestration requirements documented (PostgreSQL, Redis, Orleans silo, API)? [Completeness]
- [ ] CHK004 - Are AppHost health check requirements defined for all orchestrated services? [Coverage, Gap]
- [ ] CHK005 - Is the AppHost port configuration requirement specified (must use .env file per MEMORY.md)? [Completeness, Spec §Technical Context]
- [ ] CHK006 - Are AppHost startup failure scenarios addressed with recovery requirements? [Coverage, Exception Flow]
- [ ] CHK007 - Are AppHost graceful shutdown timeout requirements quantified (specific seconds)? [Clarity, Gap]

### E2E Test Isolation & Fixtures

- [ ] CHK008 - Are E2E test isolation requirements defined to prevent test pollution? [Completeness, Gap]
- [ ] CHK009 - Are E2E test fixture lifecycle requirements specified (setup, teardown, disposal)? [Completeness, Gap]
- [ ] CHK010 - Are test data cleanup requirements defined for E2E tests? [Completeness, Gap]
- [ ] CHK011 - Are E2E test database reset requirements specified between test runs? [Gap]
- [ ] CHK012 - Are parallel E2E test execution safety requirements addressed? [Coverage, Gap]
- [ ] CHK013 - Are E2E test timeout requirements quantified for each test phase (setup, execution, teardown)? [Clarity, Gap]

### E2E Test Execution Requirements

- [ ] CHK014 - Are E2E test category trait requirements consistent with [testing-conventions.md](../../../docs/testing-conventions.md) §Test Categorization? [Consistency, Convention]
- [ ] CHK015 - Are E2E test execution time requirements quantified (30s-2min per test per convention)? [Completeness, Convention]
- [ ] CHK016 - Are E2E test failure diagnostic requirements defined (logs, screenshots, traces)? [Completeness, Gap]
- [ ] CHK017 - Are E2E test flakiness mitigation requirements specified (retry policies, wait strategies)? [Coverage, Gap]
- [ ] CHK018 - Are E2E test browser automation requirements defined (Playwright configuration per strategy)? [Completeness, Convention]
- [ ] CHK019 - Are E2E test validation gate requirements aligned with [testing-conventions.md](../../../docs/testing-conventions.md) §Validation Gates (manual execution)? [Consistency, Convention]

---

## Build Requirements Quality

### Clean Build Definition

- [ ] CHK020 - Are "clean build" success criteria explicitly defined with measurable thresholds? [Clarity, Gap]
- [ ] CHK021 - Are zero-warning build requirements specified across all project types? [Completeness, Gap]
- [ ] CHK022 - Are zero-error build requirements documented with failure handling? [Completeness, Gap]
- [ ] CHK023 - Are build timeout requirements quantified for each project/solution? [Clarity, Gap]
- [ ] CHK024 - Are build reproducibility requirements defined (deterministic builds)? [Completeness, Gap]
- [ ] CHK025 - Are build cache invalidation requirements specified? [Coverage, Gap]

### Pre-commit Build Validation

- [ ] CHK026 - Are pre-commit hook build validation requirements aligned with [testing-conventions.md](../../../docs/testing-conventions.md) §Validation Gates? [Consistency, Convention]
- [ ] CHK027 - Are pre-commit test filter requirements consistent with convention (Unit|System|Simulation|Architecture)? [Consistency, Convention]
- [ ] CHK028 - Are pre-commit hook failure requirements defined with clear error messages? [Completeness, Gap]
- [ ] CHK029 - Are pre-commit hook timeout requirements quantified to prevent infinite hangs? [Clarity, Gap]
- [ ] CHK030 - Are pre-commit hook bypass requirements restricted (no --no-verify without explicit permission)? [Completeness, Gap]

### CI/CD Build Requirements

- [ ] CHK031 - Are CI build requirements consistent across local and CI environments? [Consistency, Gap]
- [ ] CHK032 - Are CI build artifact retention requirements defined? [Completeness, Gap]
- [ ] CHK033 - Are CI build failure notification requirements specified? [Completeness, Gap]
- [ ] CHK034 - Are CI build performance baseline requirements defined with regression thresholds? [Clarity, Gap]

---

## Process Lifecycle Requirements Quality

### Startup Requirements

- [ ] CHK035 - Are SimulationEngine startup requirements defined with initialization sequence in FR-013a? [Completeness, Spec §FR-013a]
- [ ] CHK036 - Is the timer initialization requirement quantified (100ms interval, 10 ticks/sec) in FR-013a? [Clarity, Spec §FR-013a]
- [ ] CHK037 - Are first-tick timing requirements measurable (<100-200ms after Start()) in FR-013a? [Measurability, Spec §FR-013a]
- [ ] CHK038 - Are LiteDB initialization requirements defined with connection timeout (5 seconds per plan.md PR-001)? [Completeness, Spec §PR-001]
- [ ] CHK039 - Are LiteDB retry requirements specified with exponential backoff (1s, 2s, 4s per PR-001)? [Clarity, Spec §PR-001]
- [ ] CHK040 - Are graceful degradation requirements defined for LiteDB connection failure (fallback to in-memory)? [Completeness, Spec §PR-001]
- [ ] CHK041 - Are initialization failure logging requirements specified with error message format? [Completeness, Spec §FR-013a]
- [ ] CHK042 - Are user-facing error message requirements defined for initialization failures? [Completeness, Spec §FR-013a]
- [ ] CHK043 - Are initialization total time budget requirements quantified (<500ms per plan.md CL-001)? [Clarity, Spec §CL-001]

### Shutdown Requirements

- [ ] CHK044 - Are SimulationEngine disposal requirements defined in FR-013c and plan.md CL-001? [Completeness, Spec §FR-013c]
- [ ] CHK045 - Are shutdown sequence order requirements specified (unsubscribe, stop, persist, dispose)? [Completeness, Spec §CL-001]
- [ ] CHK046 - Are shutdown timeout requirements quantified (<100ms total per CL-001)? [Clarity, Spec §CL-001]
- [ ] CHK047 - Are resource cleanup requirements defined for all allocated resources (timer, handlers, database)? [Completeness, Gap]
- [ ] CHK048 - Are disposal idempotency requirements specified (safe to call multiple times)? [Coverage, Gap]
- [ ] CHK049 - Are disposal failure handling requirements defined (log but don't throw)? [Completeness, Gap]
- [ ] CHK050 - Are final state persistence requirements specified for clean shutdown (Settlement saved to LiteDB)? [Completeness, Spec §CL-001]

### Tab Visibility Lifecycle

- [ ] CHK051 - Are tab hidden sequence requirements defined in FR-013b (stop timer, persist state)? [Completeness, Spec §FR-013b]
- [ ] CHK052 - Are tab visible sequence requirements defined in FR-013b (calculate offline progress, resume timer)? [Completeness, Spec §FR-013b]
- [ ] CHK053 - Are Page Visibility API requirements specified (document.hidden detection per plan.md)? [Completeness, Spec §Offline Progress]
- [ ] CHK054 - Are tab visibility state transition timeout requirements quantified? [Clarity, Gap]
- [ ] CHK055 - Are repeated tab switch handling requirements defined (<60s rapid switches per spec edge cases)? [Coverage, Spec §Edge Cases]

### Timeout & Hang Prevention

- [ ] CHK056 - Are operation timeout requirements quantified for all async operations? [Completeness, Gap]
- [ ] CHK057 - Are timeout handling requirements defined (log timeout, fail gracefully)? [Completeness, Gap]
- [ ] CHK058 - Are hung process detection requirements specified? [Completeness, Gap]
- [ ] CHK059 - Are timeout configuration requirements externalized (not hardcoded)? [Coverage, Gap]

---

## Logging Requirements Quality

### Structured Logging

- [ ] CHK060 - Are structured logging requirements defined for all critical operations? [Completeness, Gap]
- [ ] CHK061 - Are log message format requirements specified (consistent structure)? [Clarity, Gap]
- [ ] CHK062 - Are log context requirements defined (operation ID, correlation ID, timestamp)? [Completeness, Gap]
- [ ] CHK063 - Are log serialization requirements specified (JSON format)? [Clarity, Gap]
- [ ] CHK064 - Are sensitive data redaction requirements defined for logs? [Completeness, Gap]

### Log Levels & Severity

- [ ] CHK065 - Are log level usage requirements defined for each operation type? [Completeness, Gap]
- [ ] CHK066 - Are Error-level logging requirements specified for all failure scenarios? [Coverage, Gap]
- [ ] CHK067 - Are Warning-level logging requirements defined for recoverable errors? [Completeness, Gap]
- [ ] CHK068 - Are Information-level logging requirements specified for normal operations? [Completeness, Gap]
- [ ] CHK069 - Are Debug/Trace-level logging requirements defined for diagnostics? [Completeness, Gap]

### Diagnostic Context

- [ ] CHK070 - Are logging requirements defined for LiteDB initialization success/failure (per PR-001)? [Completeness, Spec §PR-001]
- [ ] CHK071 - Are logging requirements defined for SimulationEngine timer failures? [Gap]
- [ ] CHK072 - Are logging requirements defined for offline progress calculation errors? [Gap]
- [ ] CHK073 - Are logging requirements defined for tab visibility state changes (per plan.md)? [Completeness, Spec §Offline Progress]
- [ ] CHK074 - Are exception logging requirements specified with full stack trace capture? [Completeness, Gap]
- [ ] CHK075 - Are performance logging requirements defined for operations >threshold? [Coverage, Gap]

### Log Retention & Management

- [ ] CHK076 - Are log retention requirements specified (duration, size limits)? [Completeness, Gap]
- [ ] CHK077 - Are log rotation requirements defined? [Completeness, Gap]
- [ ] CHK078 - Are log cleanup requirements specified (automatic purging)? [Completeness, Gap]
- [ ] CHK079 - Are log export requirements defined for diagnostics? [Coverage, Gap]

---

## Observability Requirements Quality

### Health Checks

- [ ] CHK080 - Are health check endpoint requirements defined for all services? [Completeness, Gap]
- [ ] CHK081 - Are health check response format requirements specified (standardized structure)? [Clarity, Gap]
- [ ] CHK082 - Are health check timeout requirements quantified? [Clarity, Gap]
- [ ] CHK083 - Are health check failure threshold requirements defined? [Completeness, Gap]
- [ ] CHK084 - Are dependency health check requirements specified (LiteDB, services)? [Coverage, Gap]
- [ ] CHK085 - Are health check logging requirements defined? [Completeness, Gap]

### Metrics & Telemetry

- [ ] CHK086 - Are performance metric requirements defined (game loop tick rate per spec 10/sec)? [Completeness, Spec §Technical Context]
- [ ] CHK087 - Are latency metric requirements specified (possession switch <100ms per spec)? [Clarity, Spec §Technical Context]
- [ ] CHK088 - Are throughput metric requirements defined (Settlement writes 10/sec per plan.md PR-003)? [Completeness, Spec §PR-003]
- [ ] CHK089 - Are error rate metric requirements specified? [Completeness, Gap]
- [ ] CHK090 - Are resource utilization metric requirements defined (memory, CPU)? [Coverage, Gap]
- [ ] CHK091 - Are metric aggregation requirements specified (time windows, percentiles)? [Clarity, Gap]
- [ ] CHK092 - Are metric retention requirements defined? [Completeness, Gap]

### Tracing & Correlation

- [ ] CHK093 - Are distributed tracing requirements defined for user actions? [Completeness, Gap]
- [ ] CHK094 - Are correlation ID requirements specified for request tracking? [Completeness, Gap]
- [ ] CHK095 - Are trace context propagation requirements defined? [Completeness, Gap]
- [ ] CHK096 - Are trace sampling requirements specified? [Coverage, Gap]

### Diagnostic Endpoints

- [ ] CHK097 - Are diagnostic endpoint requirements defined for troubleshooting? [Completeness, Gap]
- [ ] CHK098 - Are diagnostic data export requirements specified? [Completeness, Gap]
- [ ] CHK099 - Are diagnostic endpoint security requirements defined (authentication)? [Completeness, Gap]

---

## Test Automation Requirements Quality

### Automation-First Philosophy

- [ ] CHK100 - Are automation-first requirements aligned with [testing-strategy.md](../../../docs/testing-strategy.md) §Core Principles ("If it's not tested, it doesn't exist")? [Consistency, Convention]
- [ ] CHK101 - Are TDD requirements enforced with Red-Green-Refactor verification (per plan.md TR-003)? [Completeness, Spec §TR-003]
- [ ] CHK102 - Are test-first requirements verified via commit message documentation? [Measurability, Spec §TR-003]
- [ ] CHK103 - Are manual testing exception requirements explicitly justified? [Completeness, Gap]

### CI/CD Test Automation

- [ ] CHK104 - Are CI test execution requirements aligned with [testing-conventions.md](../../../docs/testing-conventions.md) §Validation Gates? [Consistency, Convention]
- [ ] CHK105 - Are automated test coverage requirements quantified (minimum percentage)? [Clarity, Gap]
- [ ] CHK106 - Are automated test failure blocking requirements defined (PR merge gates)? [Completeness, Gap]
- [ ] CHK107 - Are automated test performance requirements specified (execution time limits)? [Clarity, Gap]
- [ ] CHK108 - Are automated test retry requirements defined for transient failures? [Coverage, Gap]

### Test Maintainability Automation

- [ ] CHK109 - Are automated test naming requirements consistent with [testing-conventions.md](../../../docs/testing-conventions.md) §Namespace Convention? [Consistency, Convention]
- [ ] CHK110 - Are automated test organization requirements aligned with convention directory structure? [Consistency, Convention]
- [ ] CHK111 - Are automated test refactoring requirements defined (prevent duplication)? [Completeness, Gap]

---

## Convention Compliance Requirements Quality

### Test Categorization Compliance

- [ ] CHK112 - Are test category trait requirements consistent with [testing-conventions.md](../../../docs/testing-conventions.md) §Test Categorization (Unit, System, Integration, E2E, Simulation, Architecture)? [Consistency, Convention]
- [ ] CHK113 - Are test project naming requirements consistent with convention suffix patterns (.Tests, .SystemTests, .IntegrationTests, .E2ETests)? [Consistency, Convention]
- [ ] CHK114 - Are test namespace requirements aligned with convention (match test project name, not source project)? [Consistency, Convention]

### Execution Speed Compliance

- [ ] CHK115 - Are Unit test speed requirements compliant with convention (<1s per test)? [Consistency, Convention]
- [ ] CHK116 - Are System test speed requirements compliant with convention (<5s per test)? [Consistency, Convention]
- [ ] CHK117 - Are E2E test speed requirements compliant with convention (30s-2min per test)? [Consistency, Convention]
- [ ] CHK118 - Are test timeout enforcement requirements defined to ensure compliance? [Completeness, Gap]

### BDD Compliance

- [ ] CHK119 - Are BDD feature specification requirements aligned with [testing-strategy.md](../../../docs/testing-strategy.md) §BDD Feature Specifications? [Consistency, Convention]
- [ ] CHK120 - Are Reqnroll configuration requirements consistent with convention (reqnroll.json with category traits)? [Consistency, Convention]
- [ ] CHK121 - Are step definition organization requirements aligned with strategy directory structure? [Consistency, Convention]
- [ ] CHK122 - Are Gherkin Given-When-Then requirements enforced per spec acceptance scenarios? [Consistency, Spec §Acceptance Scenarios]

### Test Fixture Compliance

- [ ] CHK123 - Are test fixture requirements aligned with plan.md TR-001 (SettlementTestFixtures pattern)? [Consistency, Spec §TR-001]
- [ ] CHK124 - Are test fixture determinism requirements enforced (no GUIDs per TR-001)? [Completeness, Spec §TR-001]
- [ ] CHK125 - Are test fixture immutability requirements verified (new instances per call per TR-001)? [Consistency, Spec §TR-001]

---

## Test Data Management Requirements Quality

### Test Fixture Patterns

- [ ] CHK126 - Are test fixture factory method requirements defined per plan.md TR-001? [Completeness, Spec §TR-001]
- [ ] CHK127 - Are test fixture naming requirements specified (CreateTestSettlement, CreateTestNPC patterns)? [Clarity, Spec §TR-001]
- [ ] CHK128 - Are test fixture parameter requirements defined (id, state, type overloads)? [Completeness, Spec §TR-001]

### Test Data Cleanup

- [ ] CHK129 - Are test data cleanup requirements defined for all test categories? [Completeness, Gap]
- [ ] CHK130 - Are test data isolation requirements specified (no shared state)? [Completeness, Gap]
- [ ] CHK131 - Are test database reset requirements defined between test runs? [Completeness, Gap]
- [ ] CHK132 - Are test data disposal requirements specified in teardown? [Completeness, Gap]

### Test Builders

- [ ] CHK133 - Are fluent builder requirements aligned with [testing-strategy.md](../../../docs/testing-strategy.md) §Fluent Builders pattern? [Consistency, Convention]
- [ ] CHK134 - Are test builder chainability requirements defined? [Completeness, Gap]
- [ ] CHK135 - Are test builder default value requirements specified? [Completeness, Gap]

---

## Error Handling & Graceful Degradation Requirements Quality

### Exception Handling Requirements

- [ ] CHK136 - Are LiteDB corruption recovery requirements defined in NFR-001a? [Completeness, Spec §NFR-001]
- [ ] CHK137 - Are initialization failure recovery requirements specified in NFR-001b? [Completeness, Spec §NFR-001]
- [ ] CHK138 - Are timer failure recovery requirements defined in NFR-001c? [Completeness, Spec §NFR-001]
- [ ] CHK139 - Are offline calculation failure recovery requirements specified in NFR-001d? [Completeness, Spec §NFR-001]

### Graceful Degradation Requirements

- [ ] CHK140 - Are graceful degradation requirements defined for LiteDB failure (fallback to in-memory per PR-001)? [Completeness, Spec §PR-001]
- [ ] CHK141 - Are user notification requirements specified for degraded mode? [Completeness, Spec §PR-001]
- [ ] CHK142 - Are degraded mode limitation requirements documented? [Clarity, Gap]
- [ ] CHK143 - Are degraded mode recovery requirements defined (retry, restore)? [Coverage, Gap]

### Error Boundary Requirements

- [ ] CHK144 - Are ErrorBoundary component requirements defined per plan.md CL-003? [Completeness, Spec §CL-003]
- [ ] CHK145 - Are error boundary fallback UI requirements specified (error message + reload button per CL-003)? [Clarity, Spec §CL-003]
- [ ] CHK146 - Are error boundary recovery action requirements defined (clear state, reinitialize per CL-003)? [Completeness, Spec §CL-003]
- [ ] CHK147 - Are error boundary logging requirements specified? [Gap]

### Clean Failure Modes

- [ ] CHK148 - Are clean failure requirements defined (no resource leaks)? [Completeness, Gap]
- [ ] CHK149 - Are failure logging requirements specified for all error paths? [Completeness, Gap]
- [ ] CHK150 - Are failure user notification requirements defined? [Completeness, Gap]
- [ ] CHK151 - Are failure recovery requirements specified (manual or automatic)? [Coverage, Gap]

---

## Test Automation Hooks & Basic Accessibility Requirements Quality

**Purpose**: Verify that requirements define ARIA labels and semantic markup primarily for automated UI testing (Playwright/Selenium) rather than full WCAG compliance. This is a visual mobile game requiring sight and dexterity - full accessibility for disabled users is not the goal.

### ARIA Labels for Test Automation

- [ ] CHK152 - Are aria-label requirements defined for major interactive components to enable test getByLabel() queries? [Completeness, Spec §NFR-002a]
- [ ] CHK153 - Are aria-label requirements specified for test selector stability (not verbose descriptions)? [Clarity, Spec §NFR-002a]
- [ ] CHK154 - Are data-testid fallback requirements defined for components without aria-labels? [Completeness, Spec §NFR-002a]
- [ ] CHK155 - Are test selector priorities documented (prefer aria-label > data-testid > CSS)? [Clarity, Gap]

### Semantic HTML for Test Framework Compatibility

- [ ] CHK156 - Are button element requirements defined (<button> improves test reliability vs <div>)? [Completeness, Spec §NFR-002b]
- [ ] CHK157 - Are list element requirements specified (ul/ol enables test framework list queries)? [Clarity, Spec §NFR-002b]
- [ ] CHK158 - Are form label requirements defined (enables form validation tests)? [Completeness, Spec §NFR-002b]
- [ ] CHK159 - Are heading hierarchy requirements specified (h1 → h2 → h3 for test landmark navigation)? [Coverage, Spec §NFR-002b]

### ARIA Roles for Component Identification

- [ ] CHK160 - Are ARIA role requirements defined for custom components (role="grid", "toolbar", "dialog")? [Completeness, Spec §NFR-002c]
- [ ] CHK161 - Are role requirements specified to enable test framework component navigation? [Clarity, Spec §NFR-002c]
- [ ] CHK162 - Are modal identification requirements defined (role="dialog", aria-modal="true" for test detection)? [Completeness, Spec §NFR-002c]

### ARIA States for Test Assertions

- [ ] CHK163 - Are aria-pressed requirements defined for stateful buttons (enables test state assertions)? [Completeness, Spec §NFR-002d]
- [ ] CHK164 - Are aria-disabled requirements specified for unavailable actions (test can verify state)? [Clarity, Spec §NFR-002d]
- [ ] CHK165 - Are aria-busy requirements defined for loading states (test can wait for completion)? [Completeness, Spec §NFR-002d]

### Error Detection for Test Automation

- [ ] CHK166 - Are role="alert" requirements defined for error messages (test framework can detect errors)? [Completeness, Spec §NFR-002e]
- [ ] CHK167 - Are aria-live="assertive" requirements specified for critical errors (triggers test listeners)? [Clarity, Spec §NFR-002e]
- [ ] CHK168 - Are error message content requirements defined for test validation? [Coverage, Gap]

### Known Accessibility Limitations

- [ ] CHK169 - Are visual game requirements documented (vision required, not accessible to blind users)? [Completeness, Spec §NFR-002f]
- [ ] CHK170 - Are manual dexterity requirements documented (mouse/touch expected)? [Clarity, Spec §NFR-002f]
- [ ] CHK171 - Are resolution support requirements specified (mobile 360x640 to ultrawide 3440x1440, responsive design)? [Completeness, Spec §NFR-002f]
- [ ] CHK172 - Is lack of screen reader support documented as known limitation? [Coverage, Spec §NFR-002f]
- [ ] CHK173 - Is lack of keyboard-only navigation documented as known limitation? [Completeness, Spec §NFR-002f]

---

## UI Test Automation Requirements Quality

### bUnit Component Test Requirements

- [ ] CHK190 - Are bUnit component test requirements defined for all Blazor components per tasks.md? [Completeness, Tasks §T038, §T039, §T055, §T056]
- [ ] CHK191 - Are bUnit test organization requirements aligned with convention directory structure? [Consistency, Convention]
- [ ] CHK192 - Are bUnit component rendering test requirements specified (SettlementView, NPCSidebar, ActionPanel per tasks)? [Completeness, Tasks §Phase 3, §Phase 4]
- [ ] CHK193 - Are bUnit event handler test requirements defined (button clicks, possession triggers)? [Completeness, Gap]
- [ ] CHK194 - Are bUnit parameter passing test requirements specified (component inputs validated)? [Coverage, Gap]
- [ ] CHK195 - Are bUnit component lifecycle test requirements defined (OnInitialized, OnDispose per CL-001)? [Completeness, Spec §CL-001]
- [ ] CHK196 - Are bUnit async operation test requirements specified (timer-based components per research.md)? [Clarity, Spec §Research]
- [ ] CHK197 - Are bUnit WaitForState requirements defined for async component updates? [Completeness, Spec §Research]

### Playwright E2E Test Requirements

- [ ] CHK198 - Are Playwright browser configuration requirements specified (browsers, viewports, headless mode)? [Completeness, Convention]
- [ ] CHK199 - Are Playwright test stability requirements defined (wait strategies, retry policies)? [Completeness, Gap]
- [ ] CHK200 - Are Playwright selector requirements specified (prefer data-testid, avoid brittle CSS selectors)? [Clarity, Gap]
- [ ] CHK201 - Are Playwright action requirements defined (click, fill, select patterns)? [Completeness, Gap]
- [ ] CHK202 - Are Playwright assertion requirements specified (visibility, content, state verification)? [Coverage, Gap]
- [ ] CHK203 - Are Playwright screenshot requirements defined for test failures? [Completeness, Gap]
- [ ] CHK204 - Are Playwright video recording requirements specified for debugging? [Gap]

### UI Interaction Test Coverage Requirements

- [ ] CHK205 - Are possession flow test requirements defined (click portrait → possess → execute action → release per spec US2)? [Completeness, Spec §US2]
- [ ] CHK206 - Are observer mode test requirements specified (autonomous NPC behavior visible per spec US1)? [Completeness, Spec §US1]
- [ ] CHK207 - Are context-aware action test requirements defined (different actions per building/class per spec US3)? [Completeness, Spec §US3]
- [ ] CHK208 - Are action timer test requirements specified (countdown display, completion trigger)? [Coverage, Gap]
- [ ] CHK209 - Are activity log test requirements defined (entries appear, scrolling behavior)? [Completeness, Gap]
- [ ] CHK210 - Are favorites system test requirements specified (add, remove, quick-possess per spec US5)? [Completeness, Spec §US5]

### Visual Regression Test Requirements

- [ ] CHK211 - Are visual regression test requirements defined (screenshot comparison)? [Completeness, Gap]
- [ ] CHK212 - Are visual regression baseline requirements specified (approved snapshots)? [Clarity, Gap]
- [ ] CHK213 - Are visual regression threshold requirements quantified (acceptable pixel difference)? [Clarity, Gap]
- [ ] CHK214 - Are visual regression update requirements defined (baseline update process)? [Completeness, Gap]

### Animation & Timing Test Requirements

- [ ] CHK215 - Are animation test requirements defined (transitions complete, no visual glitches)? [Completeness, Gap]
- [ ] CHK216 - Are timer accuracy test requirements specified (10 ticks/sec game loop verification)? [Clarity, Spec §Technical Context]
- [ ] CHK217 - Are action countdown test requirements defined (5-30 second timers accurate)? [Completeness, Spec §FR-006]
- [ ] CHK218 - Are debounce/throttle test requirements specified for performance optimization? [Coverage, Gap]

### Component State Test Requirements

- [ ] CHK219 - Are component state update test requirements defined (StateHasChanged triggering per CL-002)? [Completeness, Spec §CL-002]
- [ ] CHK220 - Are component re-render test requirements specified (avoid unnecessary renders per CL-002)? [Clarity, Spec §CL-002]
- [ ] CHK221 - Are component disposal test requirements defined (cleanup verification)? [Completeness, Gap]
- [ ] CHK222 - Are component error boundary test requirements specified (error recovery per CL-003)? [Completeness, Spec §CL-003]

### UI Accessibility Test Automation Requirements

- [ ] CHK223 - Are automated accessibility test requirements defined (axe-core, pa11y integration)? [Completeness, Gap]
- [ ] CHK224 - Are ARIA validation test requirements specified (correct roles, labels, states)? [Clarity, Gap]
- [ ] CHK225 - Are keyboard navigation test requirements defined (tab order, focus management automation)? [Completeness, Gap]
- [ ] CHK226 - Are color contrast test requirements specified (automated WCAG checks)? [Coverage, Gap]
- [ ] CHK227 - Are screen reader simulation test requirements defined? [Gap]

### Performance Test Requirements

- [ ] CHK228 - Are UI rendering performance test requirements defined (<16ms render target for 60 FPS per CL-002)? [Completeness, Spec §CL-002]
- [ ] CHK229 - Are component load time test requirements specified (<2s page load per spec)? [Clarity, Spec §Technical Context]
- [ ] CHK230 - Are UI interaction latency test requirements defined (<100ms possession switch per spec)? [Completeness, Spec §Technical Context]
- [ ] CHK231 - Are UI memory leak test requirements specified (component disposal cleanup)? [Coverage, Gap]
- [ ] CHK232 - Are UI CPU usage test requirements defined (game loop efficiency)? [Gap]

### Test Data Requirements for UI Tests

- [ ] CHK233 - Are UI test fixture requirements defined (deterministic NPCs, buildings, actions)? [Completeness, Gap]
- [ ] CHK234 - Are UI test snapshot requirements specified (component HTML snapshots)? [Clarity, Gap]
- [ ] CHK235 - Are UI test mock requirements defined (API responses, timer mocking)? [Completeness, Gap]
- [ ] CHK236 - Are UI test seed data requirements specified (reproducible test scenarios)? [Coverage, Gap]

---

## Summary Statistics

**Total Items**: 236 (was 151, added 85 UX/accessibility and UI test automation items)
**Categories**: 11 (added UX/Accessibility and UI Test Automation)
**Traceability**: ~70% items reference spec sections, tasks, or conventions
**Coverage Focus**:

- E2E infrastructure (19)
- Build (15)
- Process lifecycle (25)
- Logging (20)
- Observability (20)
- Automation (11)
- Conventions (14)
- Test data (10)
- Error handling (16)
- **UX/Accessibility (38 items)** ✨ NEW
- **UI Test Automation (47 items)** ✨ NEW

**High-Impact Gaps Identified**:

**Original Gaps**:

- E2E test isolation and fixture lifecycle (CHK008-CHK013)
- Clean build definition and metrics (CHK020-CHK025)
- Timeout and hang prevention (CHK056-CHK059)
- Structured logging requirements (CHK060-CHK064)
- Health check and metrics infrastructure (CHK080-CHK092)
- Test automation coverage thresholds (CHK105-CHK108)

**Test Automation Hooks & Basic Accessibility** ✨ UPDATED:

- ARIA labels for test selectors (CHK152-CHK155)
- Semantic HTML for test compatibility (CHK156-CHK159)
- ARIA roles for component identification (CHK160-CHK162)
- ARIA states for test assertions (CHK163-CHK165)
- Error detection for test automation (CHK166-CHK168)
- Known accessibility limitations documented (CHK169-CHK173)

**UI Test Automation Gaps** ✨ NEW:

- bUnit async component testing (CHK196-CHK197)
- Playwright selector and stability strategies (CHK199-CHK204)
- Visual regression testing (CHK211-CHK214)
- Automated accessibility testing (CHK223-CHK227)
- UI performance testing (CHK228-CHK232)

**Convention Alignment**: 14+ items explicitly validate requirements against testing-conventions.md and testing-strategy.md

**Next Steps**:

1. **Verify test automation hooks**: Confirm ARIA labels and semantic HTML requirements support Playwright/Selenium test stability (CHK152-CHK173)
2. **Define UI test automation requirements**: Add bUnit and Playwright testing requirements to plan.md (CHK190-CHK236)
3. **Update spec.md**: Add missing NFRs for logging, observability, health checks (accessibility now complete in NFR-002)
4. **Update plan.md**: Add E2E test infrastructure and UI test automation requirements
5. **Document limitations**: Ensure README.md documents known accessibility limitations (vision/dexterity required, no screen reader support)
6. **Ensure all convention references**: Verify all references to testing-conventions.md and testing-strategy.md are complete
