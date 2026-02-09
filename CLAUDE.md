# CLAUDE.md

<!-- VERBATIM: EXTREMELY_IMPORTANT -->

<EXTREMELY_IMPORTANT>

These apply during active development (implementation, fixes, refactoring), not exploration/research.

- NEVER skip or weaken GitHooks (--no-verify requires explicit user permission); always fix root causes
- NEVER create merge commits; use rebase for linear history, then --ff-only for merges
- ALWAYS use Conventional Commit messages
- ALWAYS use git worktrees and feature branches (isolates work, prevents commits to main)
- ALWAYS commit after self-verification but before declaring complete (run tests/build/lint first)
- ALWAYS treat Warnings as Errors (configure in tooling)
  - 0 Linting Issues
  - 0 Build Warnings
  - 0 Commit Warnings
  - 0 Build Errors
  - 0 Test Failures (tests must be run)
- ALWAYS TDD + Automated Tests First during implementation (not during planning/exploration)
- ALWAYS write REAL tests that genuinely test behavior
  - NEVER create fake tests, TODO tests, or Assert.True(true) placeholders
  - NEVER skip tests with [Fact(Skip = "TODO: ...")] - if it can't run, don't commit it
  - Tests must pass and provide actual verification of behavior
- ALWAYS automate repetitive tasks (git hooks, scripts, tools); never manual repeat
- ALWAYS DRY, YAGNI, Less Code >> More Code (avoid premature abstractions and over-engineering)
- NEVER create files not specified in the task (scripts, reports, summaries, documentation)
  - If a utility script seems needed, ask first

</EXTREMELY_IMPORTANT>

<!-- END VERBATIM -->

**Scope**: Development only (not exploration)

## Standards Matrix

| Area    | Rule                           |
| ------- | ------------------------------ |
| Git     | Conventional commits           |
| Commits | Type scope: description        |
| Quality | 0 lint, 0 warnings, 0 failures |
| Process | TDD first, automate repeats    |

## Core Principles

1. **Think First**: State assumptions, present alternatives, ask when unclear
2. **Simple**: Minimal code, no speculation, single-use = no abstraction
3. **Surgical**: Edit only required, match style, clean only your orphans
4. **Verifiable**: Transform goals → tests/checks (e.g., "add validation" → "write tests → pass")

## Development Workflow (Agent Teams)

This project uses Claude Code Agent Teams for automated quality enforcement.
Start with `claude` in the project root — Agent Teams is enabled via settings.

### Team Structure

**Lead** (delegate mode — coordination only)

- Plans feature: defines tasks with acceptance criteria
- Spawns implementer and reviewer teammates
- Requires plan approval before implementer begins coding
- Reviews are dispatched automatically via task dependencies
- Merges branch after reviewer approves

**Implementer** (teammate — plan approval required)

- Creates feature branch
- Processes all tasks from the plan in order
- TDD cycle: write test → implement → verify
- 1 task = 1 commit (atomic, reviewable, conventional message)
- TaskCompleted hook enforces build + test (hard gate)
- Updates plan file with task status and commit hashes

**Reviewer** (teammate — activated after implementation)

- Reviews implementer's changes with fresh context (no sunk cost bias)
- Checks each task against plan deliverables and quality gates
- Messages implementer directly with findings
- Implementer addresses findings in follow-up commits

### Workflow

1. User describes feature to lead
2. Lead creates plan at `docs/plans/<feature>.md` with all tasks
3. Lead spawns implementer (with plan approval required) and reviewer
4. Lead creates tasks with dependencies (review tasks blocked by implementation tasks)
5. Implementer submits plan → lead approves → implementer executes all tasks
6. Review tasks auto-unblock → reviewer reviews all changes
7. Lead merges after reviewer approves

### Quality Gate Layers

| Layer         | Mechanism                                         | Enforcement               |
| ------------- | ------------------------------------------------- | ------------------------- |
| Deterministic | Git hooks (lint, format, tests, commits)          | Hard gate — blocks commit |
| Deterministic | TaskCompleted hook (build + test)                 | Hard gate — blocks task   |
| Structural    | Separate reviewer teammate (fresh context)        | Architectural             |
| Structural    | Plan approval (lead approves before coding)       | Architectural             |
| Structural    | Delegate mode (lead can't code, only coordinates) | Architectural             |
| Behavioral    | CLAUDE.md conventions loaded at session start     | Shapes initial behavior   |

### Quality Gates

#### Pre-commit (automatic via husky)

- Prettier formatting (all files)
- Markdown lint
- C# formatting (if .cs files staged)
- Unit tests (if .cs files staged, excludes integration/e2e)

#### Pre-push (recommended)

```bash
npm run quality
```

Runs:

- Full lint check (format + markdown + links)
- Unit tests
- Build verification

#### Continuous Integration (automatic on push to main)

- Format validation
- Linting
- Unit tests
- Build verification
- Security scanning

### Available Tools

When you need context, use the appropriate search tool:

- **QMD**: Design docs, specs, GDDs, architecture docs
- **grepai**: Code implementations, functions, patterns
- **CodeContext**: Codebase structure, directory layouts, symbols

---

## Conventions

**Branches**: `type/description` (feat/inventory, fix/leak, docs/readme, refactor/engine)

**Commits**: `type(scope): description`

Types: feat, fix, docs, style, refactor, test, chore

Scopes: module name or omit

Examples:

- `feat(auth): add JWT token validation`
- `fix(ui): resolve layout overlap on mobile`
- `docs(readme): update setup instructions`

---

**Success**: Minimal diffs, clarifying questions before mistakes, no rewrites.

## Active Technologies

- C# / .NET 10.0 (as per global.json) + Blazor WASM, System.Timers (game loop) (001-minimal-possession-demo)
- In-memory only (MVP - state resets on page refresh) (001-minimal-possession-demo)

## Recent Changes

- 001-minimal-possession-demo: Added C# / .NET 10.0 (as per global.json) + Blazor WASM, System.Timers (game loop)
