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

## Development Workflow (Dual-Session)

This project uses two Claude Code sessions with different providers for cost-optimized quality enforcement.

**Session 1 — Lead/Reviewer (`claude` / Opus)**

1. Plan feature: define tasks with acceptance criteria in `docs/plans/<feature>.md`
2. Each task has: deliverables, quality gates, dependencies
3. After implementer commits, review changes against plan
4. Report findings in plan file or create GitHub issues
5. Approve or request changes

**Session 2 — Implementer (`claude-zia`)**

1. Read plan file to find next task
2. TDD cycle: write test → implement → verify
3. TaskCompleted hook runs build + test (deterministic gate)
4. 1 task = 1 commit (atomic, reviewable, conventional message)
5. Read review feedback, address findings, commit fixes

**Handoff**: Git commits + plan files on disk. No shared memory.

### Roles

**Lead/Reviewer** (run with `claude` — Opus)

- Plans tasks with clear acceptance criteria
- Reviews completed work against plan (fresh context, no sunk cost bias)
- Focus: spec compliance, code quality, edge cases, test coverage
- Reports findings — does NOT implement fixes
- Coordinates merge after review approval

**Implementer** (run with `claude-zia`)

- Reads plan file for current task
- Follows TDD: write test → implement → verify
- 1 task = 1 commit (atomic, reviewable)
- TaskCompleted hook enforces build + test automatically
- Addresses review findings in follow-up commits

### Quality Gate Layers

| Layer         | Mechanism                                     | Enforcement               |
| ------------- | --------------------------------------------- | ------------------------- |
| Deterministic | Git hooks (lint, format, tests, commits)      | Hard gate — blocks commit |
| Deterministic | TaskCompleted hook (build + test)             | Hard gate — blocks task   |
| Structural    | Separate reviewer session (fresh context)     | Architectural             |
| Structural    | Plan approval (lead reviews before code)      | Architectural             |
| Behavioral    | CLAUDE.md conventions loaded at session start | Shapes initial behavior   |

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
