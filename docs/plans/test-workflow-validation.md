# Plan: Test Workflow Validation

**Purpose**: Validate the dual-session Agent Teams workflow by implementing a small, low-risk task.

## Task 1: Add unit tests for DeterministicRng

**Status**: Completed (commit 142bb74)

### Implementation Summary

Created `tests/RealmsOfIdle.Core.Tests/Infrastructure/DeterministicRngTests.cs` with 28 tests covering all 9 required behaviors.

### Quality Gates Verified

- [x] All tests pass: 70 total non-E2E/Integration tests (41 Core.Tests + 6 Tests + 6 Orleans.Tests + 10 ArchitectureTests + 7 SystemTests)
- [x] Build clean: 0 warnings, 0 errors
- [x] No files created beyond the single test file
- [x] Tests verify real behavior (no placeholders)
- [x] Conventional commit: `test(core): add DeterministicRng unit tests`

### Context

`DeterministicRng` (`src/RealmsOfIdle.Core/Infrastructure/DeterministicRng.cs`) is a deterministic RNG wrapper with zero test coverage. It has clear, testable behavior and requires no mocks.

### Deliverables

Create `tests/RealmsOfIdle.Core.Tests/Infrastructure/DeterministicRngTests.cs` with tests covering:

1. **Determinism**: Two instances with the same seed produce identical sequences
2. **Seed property**: Returns the seed passed to the constructor
3. **Next(maxValue)**: Returns values in range [0, maxValue)
4. **Next(minValue, maxValue)**: Returns values in range [minValue, maxValue)
5. **NextDouble()**: Returns values in range [0.0, 1.0)
6. **NextBytes()**: Fills the entire buffer with data
7. **NextBool()**: Returns a boolean value
8. **NextBool(probability)**: Validates probability bounds (throws `ArgumentOutOfRangeException` for values < 0.0 or > 1.0)
9. **WithOffset()**: Creates independent instance with adjusted seed

### Quality Gates

- [x] All tests pass: `dotnet test --filter "Category!=E2E&Category!=Integration"`
- [x] Build clean: `dotnet build RealmsOfIdle.slnx --warnaserror` (0 warnings, 0 errors)
- [x] No files created beyond the single test file
- [x] Tests verify real behavior (no `Assert.True(true)` or placeholder tests)
- [x] Conventional commit: `test(core): add DeterministicRng unit tests`

### Notes

- Test project: `RealmsOfIdle.Core.Tests`
- Follow existing test patterns in that project (xUnit, `[Fact]`/`[Theory]`)
- The class is deterministic by design — leverage this for exact assertions where possible
