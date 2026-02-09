<!--
Sync Impact Report - Constitution Update

Version Change: N/A (Initial Constitution) → 1.0.0
Modified Principles: N/A (initial creation)
Added Sections:
  - Core Principles (5 principles: Test-First, Zero-Tolerance Quality, Git Discipline, Simplicity First, Automation)
  - Quality Standards (pre-commit, pre-push, CI gates)
  - Development Workflow (feature process, agent teams structure, quality gate layers)
  - Governance (amendment process, versioning policy, compliance requirements)

Templates Updated:
  ✅ plan-template.md - Added specific Constitution Check checklist with all 5 principles
  ✅ spec-template.md - Already aligned (testable acceptance scenarios, priorities)
  ✅ tasks-template.md - Updated TDD language from "OPTIONAL" to "REQUIRED - TDD NON-NEGOTIABLE"
  ⚠ No command files exist yet in .specify/templates/commands/

Follow-up TODOs:
  - None (all placeholders filled with concrete values, all templates synchronized)

Rationale for version 1.0.0:
  - Initial constitution creation for Realms of Idle project
  - Codifies existing practices from CLAUDE.md and project documentation
  - Establishes baseline governance for feature development workflow
  - Aligns all templates with non-negotiable TDD principle
-->

# Realms of Idle Constitution

## Core Principles

### I. Test-First Development (NON-NEGOTIABLE)

Test-Driven Development is mandatory for all implementation work:

- Tests MUST be written before implementation code
- Tests MUST fail before implementing the feature (Red-Green-Refactor)
- Tests MUST provide genuine behavior verification
- NEVER create placeholder tests (no `Assert.True(true)`, no `[Fact(Skip = "TODO")]`)
- Tests MUST pass before considering work complete

**Rationale**: TDD prevents defects, clarifies requirements, enables safe refactoring, and ensures code is testable by design. This is non-negotiable because untested code creates technical debt and reduces confidence in changes.

### II. Zero-Tolerance Quality Gates

All code MUST meet these standards before commit:

- 0 Linting Issues
- 0 Build Warnings
- 0 Commit Message Warnings
- 0 Build Errors
- 0 Test Failures

**Rationale**: Warnings are deferred errors. Zero tolerance prevents quality erosion and keeps the codebase maintainable. Automated git hooks enforce these gates as hard blockers.

### III. Git Discipline

Version control practices are strictly enforced:

- Feature branches MUST be created via git worktrees (isolates work, prevents main commits)
- Linear history ONLY (rebase, then `--ff-only` merges, no merge commits)
- Conventional Commit messages REQUIRED (`type(scope): description`)
- Git hooks MUST NOT be bypassed (`--no-verify` requires explicit user permission)
- Commit ONLY after self-verification (tests pass, builds succeed)

**Rationale**: Git discipline enables clean history, safe parallel work, automated tooling, and confident rollbacks.

### IV. Simplicity First

Code complexity MUST be justified:

- Start with the simplest solution that works
- YAGNI (You Aren't Gonna Need It) - no speculative features
- DRY (Don't Repeat Yourself) - but avoid premature abstraction
- Less Code > More Code
- Single-use patterns DO NOT require abstraction
- New files MUST be justified by task requirements

**Rationale**: Simple code is easier to understand, test, and modify. Complexity without justification creates maintenance burden and slows development.

### V. Automation Over Manual Repetition

Repetitive processes MUST be automated:

- Git hooks for quality gates (pre-commit, pre-push)
- CI/CD for build, test, and deployment validation
- Scripts for common operations
- Manual processes MUST be documented then automated

**Rationale**: Automation eliminates human error, enforces consistency, saves time, and enables scaling. Manual repetition is wasteful and error-prone.

## Quality Standards

### Pre-Commit Gates (Automatic via Husky)

The following checks run automatically before each commit:

- Prettier formatting (all files)
- Markdown linting
- C# formatting (if `.cs` files staged)
- Unit tests (if `.cs` files staged, excludes integration/E2E)

### Pre-Push Gates (Recommended: `npm run quality`)

Before pushing, verify:

- Full lint check (format + markdown + links)
- All unit tests pass
- Build succeeds across all projects
- No security vulnerabilities detected

### Continuous Integration Gates (Automatic on Push)

CI pipeline validates:

- Format validation
- Complete linting suite
- Full test suite (unit, integration, architecture)
- Build verification across all configurations
- Security scanning

## Development Workflow

### Feature Development Process

1. **Branch Creation**: Use git worktrees (`git wt <branch-name>`) to create isolated workspace
2. **Planning**: Create specification in `.specify/specs/<feature>/` directory
3. **Design**: Document technical approach in plan.md with constitution compliance check
4. **Task Breakdown**: Generate tasks.md with user stories as independently testable increments
5. **Implementation**: TDD cycle for each task (write test → fail → implement → pass → commit)
6. **Verification**: Run full quality gates before declaring work complete
7. **Review**: Submit PR with conventional commit messages and test evidence
8. **Integration**: Rebase on main, merge with `--ff-only`

### Agent Teams Structure (When Using Claude Code)

**Lead Agent** (delegate mode):

- Plans features and defines tasks with acceptance criteria
- Spawns implementer and reviewer teammates
- Requires plan approval before coding begins
- Coordinates review via task dependencies
- Merges after reviewer approval

**Implementer Agent** (teammate):

- Executes tasks in feature branch
- Follows TDD cycle: test → implement → verify
- One task = one atomic commit
- Updates plan with completion status

**Reviewer Agent** (teammate):

- Fresh context review (no sunk cost bias)
- Validates against plan and quality gates
- Provides direct feedback to implementer

### Quality Gate Layers

| Layer         | Mechanism                                | Enforcement               |
| ------------- | ---------------------------------------- | ------------------------- |
| Deterministic | Git hooks (lint, format, tests, commits) | Hard gate — blocks commit |
| Deterministic | TaskCompleted hook (build + test)        | Hard gate — blocks task   |
| Structural    | Separate reviewer teammate               | Architectural             |
| Structural    | Plan approval before coding              | Architectural             |
| Structural    | Delegate mode (lead coordinates only)    | Architectural             |
| Behavioral    | CLAUDE.md conventions                    | Shapes behavior           |

## Governance

### Amendment Process

1. **Proposal**: Document proposed change with rationale and impact analysis
2. **Review**: Assess impact on existing workflows, templates, and documentation
3. **Approval**: Requires explicit acceptance of breaking changes
4. **Migration**: Update all dependent templates and documentation
5. **Version**: Increment version using semantic versioning rules

### Versioning Policy

Constitution versions follow MAJOR.MINOR.PATCH:

- **MAJOR**: Backward-incompatible governance changes (principle removal/redefinition)
- **MINOR**: New principles or materially expanded guidance
- **PATCH**: Clarifications, wording fixes, non-semantic refinements

### Compliance

- All PRs and reviews MUST verify constitution compliance
- Constitution supersedes all other practices and guidelines
- Violations MUST be justified in plan.md Complexity Tracking section
- Regular audits SHOULD be conducted to ensure adherence

### Living Document

This constitution reflects current best practices for the Realms of Idle project. As the project evolves, this document MUST evolve to codify learnings and improve governance.

Use CLAUDE.md for runtime development guidance specific to Claude Code workflows.

**Version**: 1.0.0 | **Ratified**: 2026-02-09 | **Last Amended**: 2026-02-09
