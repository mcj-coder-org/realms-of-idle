# ADR-0002: Test Type Conventions

## Status

Accepted

## Context

As the project grows, we need to organize tests into logical categories. We need conventions for:

1. Test project naming (what suffix means what)
2. Directory structure under `tests/`
3. Which tests run at which validation gates (pre-commit, pre-push, CI)
4. Namespace conventions for test classes

Without clear conventions, we risk:

- Tests running at the wrong time (slow tests blocking commits)
- Unclear boundaries between test types
- Difficulty finding specific tests
- Accidentally testing implementation details

## Decision

### Test Project Naming Convention

All test projects use a suffix to indicate test type:

| Suffix               | Purpose                                       | Example                              | Validation Gate |
| -------------------- | --------------------------------------------- | ------------------------------------ | --------------- |
| `.Tests`             | Unit tests only                               | `IdleWorlds.Core.Tests`              | Pre-commit      |
| `.SystemTests`       | System tests (TestServer, mocked externals)   | `IdleWorlds.Server.SystemTests`      | Pre-commit      |
| `.IntegrationTests`  | Integration tests (TestContainers, real deps) | `IdleWorlds.Server.IntegrationTests` | Pre-push        |
| `.E2ETests`          | User journey BDD (manual)                     | `IdleWorlds.E2ETests`                | Manual          |
| `.SimulationTests`   | Simulation BDD (pure game logic)              | `IdleWorlds.SimulationTests`         | Pre-commit      |
| `.ArchitectureTests` | Architecture validation                       | `IdleWorlds.ArchitectureTests`       | Pre-commit      |
| `.Performance.Tests` | Load/stress/soak                              | `IdleWorlds.Performance.Tests`       | CI/Scheduled    |

### Directory Structure

```
tests/
├── IdleWorlds.Core.Tests/              # Unit tests only
│   ├── Actions/
│   ├── Classes/
│   ├── Skills/
│   └── Tags/
│
├── IdleWorlds.Server.Tests/            # Unit tests for server code
│
├── IdleWorlds.Server.SystemTests/      # System tests (TestServer, mocked)
│   ├── Api/
│   ├── Middleware/
│   └── Orleans/
│
├── IdleWorlds.Server.IntegrationTests/ # Integration tests (TestContainers)
│   ├── Features/                       # Reqnroll .feature files
│   ├── StepDefinitions/
│   ├── Database/
│   └── Orleans/
│
├── IdleWorlds.Simulation.Tests/        # Unit tests for simulation
│
├── IdleWorlds.Simulation.SystemTests/  # System tests for simulation
│
├── IdleWorlds.E2ETests/                # Solution-level E2E
│   ├── Features/
│   │   ├── UserJourney/
│   │   └── MultiplayerFlow/
│   └── Playwright/
│
├── IdleWorlds.SimulationTests/         # Solution-level BDD simulation
│   └── Features/
│       ├── DailyLife/
│       ├── Economy/
│       └── WorldEvents/
│
├── IdleWorlds.ArchitectureTests/       # Architecture rules
│   ├── VerticalSlices/
│   ├── Dependencies/
│   └── NamingConventions/
│
└── IdleWorlds.Performance.Tests/       # NBomber
    ├── Load/
    ├── Stress/
    └── Soak/
```

### Namespace Convention

**All test namespaces match their project name** (not the source project):

```csharp
// Correct
namespace IdleWorlds.Core.Tests.Actions;

// Incorrect
namespace IdleWorlds.Core.Actions;
```

This convention:

- Prevents namespace collisions with source code
- Makes it immediately clear which file is a test
- Simplifies InternalsVisibleTo configuration

### Test Categorization with xUnit Traits

Every test class must have a category trait:

```csharp
// Unit tests (.Tests)
[Trait("Category", "Unit")]
public class ActionTests { }

// System tests (.SystemTests)
[Trait("Category", "System")]
public class ApiEndpointTests { }

// Integration tests (.IntegrationTests)
[Trait("Category", "Integration")]
public class DatabaseTests { }

// Simulation tests (.SimulationTests)
[Trait("Category", "Simulation")]
public class DailyLifeScenarioTests { }

// Architecture tests (.ArchitectureTests)
[Trait("Category", "Architecture")]
public class VerticalSliceTests { }

// Performance tests (.Performance.Tests)
[Trait("Category", "Performance")]
public class LoadScenarios { }
```

### Git Hook Filters

| Hook       | Categories Filter                        | Command                           |
| ---------- | ---------------------------------------- | --------------------------------- |
| Pre-commit | `Unit\|System\|Simulation\|Architecture` | Fast tests only (<1s each)        |
| Pre-push   | `Category!=E2E & Category!=Performance`  | All except manual/expensive tests |
| CI         | Full suite                               | All tests                         |

## Consequences

### Positive

- Clear separation of test types enables selective execution
- Fast tests run frequently (pre-commit), slow tests run appropriately
- Project structure is self-documenting
- Easy to add new test projects following the pattern

### Negative

- More test projects than monolithic approaches
- Requires discipline to maintain categorization
- Solution file has more projects

## Alternatives Considered

### Single Test Project per Source Project

**Rejected**: Would make selective test execution difficult. Separating by test type enables performance-optimized hooks.

### Folder-based Organization (single project)

**Rejected**: .NET test discovery and filtering works better with separate projects. Category traits help, but project boundaries are clearer.

### 命名 projects after source (e.g., `Core.Tests` vs `IdleWorlds.Core.Tests`)

**Rejected**: Keeping full names prevents collisions and maintains clarity in solution explorer.

## References

- Parent ADR: [ADR-0001: Test Framework Selection](./0001-test-frameworks.md)
- Parent ADR: [ADR-0004: Test Validation Gates](./0004-test-validation-gates.md)
