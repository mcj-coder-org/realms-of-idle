# ADR-0001: Test Framework Selection

## Status

Accepted

## Context

The Realms of Idle project requires a comprehensive testing strategy covering multiple test types:

- Unit tests for domain logic
- System tests for Orleans grain interactions
- Integration tests with real dependencies
- BDD/specification tests for game features
- Architecture validation tests
- Performance/load tests

We needed to select frameworks that:

1. Work well with .NET 10
2. Support category-based test filtering for hooks
3. Provide fluent assertion syntax
4. Enable BDD-style tests where appropriate
5. Integrate with CI/CD pipelines

## Decision

We have selected the following test framework stack:

| Test Type          | Framework | Assertions        | BDD?            |
| ------------------ | --------- | ----------------- | --------------- |
| Unit Tests         | xUnit     | AwesomeAssertions | Optional        |
| System Tests       | xUnit     | AwesomeAssertions | No              |
| Integration Tests  | xUnit     | AwesomeAssertions | Yes (Reqnroll)  |
| Simulation Tests   | Reqnroll  | AwesomeAssertions | Yes (narrative) |
| Architecture Tests | xUnit     | AwesomeAssertions | No              |
| Performance Tests  | NBomber   | N/A               | No              |

### Framework Details

**xUnit 2.9.3**: The de facto standard for .NET unit testing. Chosen for:

- Community adoption and tooling support
- Clean, attribute-based syntax
- Excellent parallel test execution
- Strong CI/CD integration

**AwesomeAssertions 9.3.0**: Fluent assertion library chosen for:

- Readable, chainable assertion syntax
- Comprehensive failure messages
- Strong analyzer support for compile-time checking
- Better than FluentAssertions for modern C#

**Reqnroll 2.0.0**: BDD framework (SpecFlow+ successor) chosen for:

- Gherkin feature files for living documentation
- xUnit integration for test execution
- DI container support for shared context
- Active development and .NET 10 support

**NBomber 5.0.0**: Load testing framework chosen for:

- .NET-native (no Java/JMeter dependency)
- Simple DSL for defining load scenarios
- Real-time metrics and reporting
- CI/CD friendly

## Consequences

### Positive

- Consistent assertion library across all test types
- Category-based filtering enables efficient pre-commit/pre-push hooks
- BDD tests serve as executable game design specifications
- Strong community support for all frameworks

### Negative

- Learning curve for team members unfamiliar with Reqnroll/BDD
- NBomber is less mature than JMeter (but adequate for our needs)
- AwesomeAssertions is newer than FluentAssertions (fewer online examples)

## Alternatives Considered

### NUnit

**Rejected because**: xUnit has better .NET Core/.NET 5+ support and more modern design. NUnit's attribute model is more complex than needed.

### MSTest

**Rejected because**: Primarily Visual Studio-centric, less community adoption than xUnit, and fewer extensibility points.

### FluentAssertions

**Rejected in favor of AwesomeAssertions**: While FluentAssertions is more established, AwesomeAssertions provides:

- Better analyzer support
- More modern C# feature usage
- Cleaner API for certain assertion types
- More active development for .NET 8+

### SpecFlow

**Rejected in favor of Reqnroll**: SpecFlow went dormant for years; Reqnroll is the community fork continuing active development.

### BenchmarkDotNet for Load Testing

**Rejected**: BenchmarkDotNet is excellent for micro-benchmarks but NBomber is better for load/stress testing due to:

- Built-in load simulation patterns
- Real-time reporting
- HTTP client support out of the box
- Scenario composition capabilities

## References

- xUnit Documentation: <https://xunit.net/>
- AwesomeAssertions: <https://awesome-testing.com/awesomeassertions/>
- Reqnroll: <https://reqnroll.net/>
- NBomber: <https://nbomber.com/>
