---
type: reference
scope: testing
status: accepted
version: 1.0.0
created: 2026-02-04
updated: 2026-02-04
subjects: [testing, conventions, standards]
dependencies: [testing-strategy.md]
---

# Testing Conventions & Standards

This document provides quick-reference conventions for testing in the Realms of Idle project. For comprehensive testing philosophy and BDD patterns, see [testing-strategy.md](./testing-strategy.md).

---

## Quick Reference

| Topic               | See Section                                 |
| ------------------- | ------------------------------------------- |
| Test project naming | [Project Naming](#project-naming)           |
| xUnit traits        | [Test Categorization](#test-categorization) |
| Package versions    | [Framework Stack](#framework-stack)         |
| Git hook filters    | [Validation Gates](#validation-gates)       |
| InternalsVisibleTo  | [Visibility](#internalsvisibleto)           |
| Shared helpers      | [Test Utilities](#test-utilities)           |

---

## Project Naming

### Suffix Convention

| Suffix               | Purpose                       | Example                              | Runs At      |
| -------------------- | ----------------------------- | ------------------------------------ | ------------ |
| `.Tests`             | Unit tests                    | `IdleWorlds.Core.Tests`              | Pre-commit   |
| `.SystemTests`       | System tests (TestServer)     | `IdleWorlds.Server.SystemTests`      | Pre-commit   |
| `.IntegrationTests`  | Integration (TestContainers)  | `IdleWorlds.Server.IntegrationTests` | Pre-push     |
| `.E2ETests`          | End-to-end user journeys      | `IdleWorlds.E2ETests`                | Manual       |
| `.SimulationTests`   | BDD simulation scenarios      | `IdleWorlds.SimulationTests`         | Pre-commit   |
| `.ArchitectureTests` | Architecture rules validation | `IdleWorlds.ArchitectureTests`       | Pre-commit   |
| `.Performance.Tests` | Load/stress/soak testing      | `IdleWorlds.Performance.Tests`       | CI/Scheduled |

### Directory Structure

```
tests/
├── IdleWorlds.Core.Tests/              # Unit tests only
│   ├── Actions/
│   ├── Classes/
│   ├── Skills/
│   └── Tags/
│
├── IdleWorlds.Server.SystemTests/      # System tests (TestServer, mocked)
│   ├── Api/
│   ├── Middleware/
│   └── Orleans/
│
├── IdleWorlds.Server.IntegrationTests/ # Integration tests (TestContainers)
│   ├── Features/                       # Reqnroll .feature files
│   ├── StepDefinitions/
│   └── Helpers/
│
├── IdleWorlds.SimulationTests/         # Solution-level BDD simulation
│   ├── Features/
│   └── Helpers/
│
├── IdleWorlds.ArchitectureTests/       # Architecture rules
│   └── VerticalSlices/
│
└── IdleWorlds.Performance.Tests/       # NBomber
    └── Load/
```

### Namespace Convention

Test namespaces match their **test project name**, not the source project:

```csharp
// Correct
namespace IdleWorlds.Core.Tests.Actions;

// Incorrect - this is the source namespace
namespace IdleWorlds.Core.Actions;
```

---

## Framework Stack

### Test Frameworks

| Component          | Package           | Version |
| ------------------ | ----------------- | ------- |
| Test Runner        | xUnit             | 2.9.3   |
| Assertions         | AwesomeAssertions | 9.3.0   |
| BDD                | Reqnroll          | 2.0.0   |
| Load Testing       | NBomber           | 5.0.0   |
| Architecture Tests | NetArchTest.Rules | 1.4.0   |

### Integration Testing

| Component      | Package                   | Version |
| -------------- | ------------------------- | ------- |
| Testcontainers | Testcontainers            | 4.0.0   |
| PostgreSQL     | Testcontainers.PostgreSql | 4.0.0   |
| Redis          | Testcontainers.Redis      | 4.0.0   |

### Source Generators

| Purpose            | Package           | Version |
| ------------------ | ----------------- | ------- |
| DTO ↔ Entity       | Riok.Mapperly     | 4.1.1   |
| Strongly-Typed IDs | StronglyTypedId   | 1.16.0  |
| Smart Enums        | Ardalis.SmartEnum | 8.0.0   |

### Analyzers

| Package                                | Version |
| -------------------------------------- | ------- |
| Microsoft.CodeAnalysis.NetAnalyzers    | 9.0.0   |
| xUnit.Analyzers                        | 1.15.0  |
| AwesomeAssertions.Analyzers            | 9.3.0   |
| Microsoft.Extensions.Logging.Analyzers | 9.0.0   |

---

## Test Categorization

### Required Traits

Every test class must have a `[Trait]` attribute:

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

### Reqnroll Configuration

Each Reqnroll project must have a `reqnroll.json`:

```json
{
  "bindingAssemblies": ["IdleWorlds.Server.IntegrationTests"],
  "traits": {
    "category": "Integration"
  }
}
```

---

## Validation Gates

### Git Hook Filters

| Hook       | Filter                                                                             |
| ---------- | ---------------------------------------------------------------------------------- |
| Pre-commit | `Category=Unit \| Category=System \| Category=Simulation \| Category=Architecture` |
| Pre-push   | `Category!=E2E & Category!=Performance`                                            |

### Execution Speed Targets

| Category     | Speed per Test | External Dependencies   |
| ------------ | -------------- | ----------------------- |
| Unit         | < 1s           | None                    |
| System       | < 5s           | Mocked                  |
| Simulation   | < 2s           | None                    |
| Architecture | < 5s total     | None                    |
| Integration  | 10-30s         | Docker (Testcontainers) |
| E2E          | 30s-2min       | Full stack              |
| Performance  | 1-5min         | Full stack              |

---

## InternalsVisibleTo

### Automatic Configuration

`Directory.Build.props` automatically configures `InternalsVisibleTo` for `.Tests` projects:

```xml
<ItemGroup Condition="$(MSBuildProjectDirectory.EndsWith('tests')) == false AND ...">
  <InternalsVisibleTo Include="$(MSBuildProjectName).Tests" />
  <InternalsVisibleTo Include="$(MSBuildProjectName).SystemTests" />
  <InternalsVisibleTo Include="$(MSBuildProjectName).IntegrationTests" />
</ItemGroup>
```

### Manual Override

When needed, add manually:

```csharp
[assembly: InternalsVisibleTo("IdleWorlds.Core.Tests")]
```

---

## Test Utilities

### CapturingLoggerProvider

**Location:** `tests/IdleWorlds.Server.IntegrationTests/Helpers/CapturingLoggerProvider.cs`

Captures and asserts on log output in integration tests:

```csharp
public class DatabaseIntegrationTests
{
    private readonly CapturingLoggerProvider _logger;

    public DatabaseIntegrationTests(ITestOutputHelper output)
    {
        _logger = new CapturingLoggerProvider(output);
        _logger.SetCurrentTest(nameof(DatabaseIntegrationTests));
    }

    [Fact]
    public async Task PlayerAction_LogsExpectedMessages()
    {
        _logger.Clear();
        // ... act ...

        _logger.GetLogsForLevel(LogLevel.Warning).Should().BeEmpty();
        _logger.GetLogsContaining("Strike").Should().ContainSingle();
    }
}
```

### SimulationHealthValidator

**Location:** `tests/IdleWorlds.SimulationTests/Helpers/SimulationHealthValidator.cs`

Validates simulation invariants after BDD scenarios:

```csharp
[AfterScenario]
public void ValidateSimulationHealth()
{
    var expectation = new SimulationHealthExpectation()
        .WithExpectedDeath(_scenarioContext["CombatVictimId"]);

    SimulationHealthValidator.ValidateSimulationHealth(_simulationResult, expectation);
}
```

Validates:

- No unexpected NPC deaths
- Currency conservation
- Item conservation
- NPC time allocation (if specified)
- No unexpected errors in logs

---

## Source Generators

### Mapperly (DTO ↔ Entity)

```csharp
[Mapper]
public static partial class PlayerMapper
{
    public static partial PlayerDto ToDto(this Player entity);
    public static partial Player ToEntity(this.PlayerDto dto);
}
```

### StronglyTypedId

```csharp
[StronglyTypedId]
public partial struct PlayerId { }

// Generates: JSON converters, EF Core converters, equality members
```

### SmartEnum

```csharp
public abstract class NpcArchetype : SmartEnum<NpcArchetype>
{
    public static readonly NpcArchetype Maintainer = new MaintainerType();
    public static readonly NpcArchetype Hero = new HeroType();
    public static readonly NpcArchetype Villain = new VillainType();

    public abstract IEnumerable<GoalType> GetDefaultGoals();

    private NpcArchetype(string name, int value) : base(name, value) { }

    private class MaintainerType : NpcArchetype
    {
        public MaintainerType() : base("Maintainer", 1) { }
        public override IEnumerable<GoalType> GetDefaultGoals() =>
            new[] { GoalType.Survival, GoalType.Economic };
    }
}
```

---

## Related Documents

- [Testing Strategy](./testing-strategy.md) - Comprehensive BDD testing philosophy
- [ADR-0001: Test Framework Selection](./adr/0001-test-frameworks.md)
- [ADR-0002: Test Type Conventions](./adr/0002-test-types-conventions.md)
- [ADR-0003: Source Generator Selection](./adr/0003-source-generators.md)
- [ADR-0004: Test Validation Gates](./adr/0004-test-validation-gates.md)
