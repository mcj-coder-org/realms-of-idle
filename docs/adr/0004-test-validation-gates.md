# ADR-0004: Test Validation Gates

## Status

Accepted

## Context

We need to decide when different types of tests run to balance:

- Developer productivity (fast feedback)
- Code quality (catch issues early)
- CI efficiency (don't waste resources)

Running all tests on every commit is too slow. Running none risks bugs reaching main.

## Decision

### Test Execution Gates

| Gate           | Trigger                  | Categories Run                                 | Duration Target |
| -------------- | ------------------------ | ---------------------------------------------- | --------------- |
| **Pre-commit** | On every commit          | `Unit`, `System`, `Simulation`, `Architecture` | < 30 seconds    |
| **Pre-push**   | Before pushing to remote | All except `E2E`, `Performance`                | < 2 minutes     |
| **CI (PR)**    | On pull request          | All except `E2E`, `Performance`                | < 5 minutes     |
| **CI (Main)**  | On push to main          | Full suite including `Performance`             | < 10 minutes    |
| **Nightly**    | Scheduled                | `E2E`, load/stress/soak tests                  | 30+ minutes     |
| **Release**    | Manual before release    | Full suite + manual E2E verification           | 1+ hours        |

### Category Definitions

| Category     | Description                          | Speed     | External Deps |
| ------------ | ------------------------------------ | --------- | ------------- |
| Unit         | Isolated domain logic, no externals  | < 1s each | None          |
| System       | TestServer/mocked externals          | < 5s each | Mocked        |
| Simulation   | Game logic BDD scenarios             | < 2s each | None          |
| Architecture | NetArchTest rules, compile-time only | < 5s      | None          |
| Integration  | TestContainers, real database/Redis  | 10-30s    | Docker        |
| E2E          | Full stack with browser              | 30s-2min  | Full stack    |
| Performance  | NBomber load tests                   | 1-5min    | Full stack    |

### Hook Configuration

**Pre-commit (.husky/pre-commit)**:

```bash
dotnet test --filter "Category=Unit|Category=System|Category=Simulation|Category=Architecture"
```

**Pre-push (.husky/pre-push)**:

```bash
dotnet test --filter "Category!=E2E&Category!=Performance"
```

### Reqnroll Trait Configuration

Each Reqnroll project has a `reqnroll.json`:

```json
{
  "bindingAssemblies": ["IdleWorlds.Server.IntegrationTests"],
  "traits": {
    "category": "Integration"
  }
}
```

This ensures Reqnroll tests inherit the correct category automatically.

## Consequences

### Positive

- Fast local development (pre-commit is quick)
- Confidence before pushing (pre-push catches integration issues)
- CI remains fast for PRs
- Full coverage happens regularly (scheduled/main branch)
- Manual tests run only when needed

### Negative

- More configuration complexity
- Developers must remember to categorize tests correctly
- Pre-push can still be slow for large integration test suites

### Enforcement

Tests without proper category traits will not run in any gate. Build will fail if:

- New tests are added without `[Trait("Category", "...")]`
- Category names don't match the defined set

## Alternatives Considered

### All tests in pre-commit

**Rejected**: Too slow for frequent commits. Would hurt developer productivity.

### No pre-push gate

**Rejected**: Integration tests wouldn't run locally before pushing, leading to CI failures that could have been caught earlier.

### Manual test execution

**Rejected**: Relies on developer discipline. Automation ensures quality standards.

### Single category for all tests

**Rejected**: Defeats the purpose of selective execution. We need fine-grained control over which tests run when.

## References

- Parent ADR: [ADR-0001: Test Framework Selection](./0001-test-frameworks.md)
- Parent ADR: [ADR-0002: Test Type Conventions](./0002-test-types-conventions.md)
