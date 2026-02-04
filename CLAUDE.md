---
type: reference
scope: high-level
status: approved
version: 1.3.0
created: 2026-01-01
updated: 2026-02-02
subjects:
  - development
  - workflow
  - guidelines
dependencies: []
---

# CLAUDE.md

<EXTREMELY_IMPORTANT>
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

## Solo Development Workflow

### Feature Development

1. **Plan feature** (update design docs if needed)
2. **Create feature branch**: `git checkout -b feat/description`
3. **TDD cycle**: test → implement → commit
4. **Verify quality**: `npm run quality` (runs lint, tests, build)
5. **Optional code review**: `npm run review:full`
6. **Merge to main**: `git checkout main && git merge feat/description --ff-only`
7. **Delete branch**: `git branch -d feat/description`

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

```

## Implementation Execution Preferences

**Subagent-Driven Development**: When implementing from detailed plans, use subagent-driven approach:
- Dispatch fresh subagent per task
- Review results between tasks
- Fast iteration with checkpoints
- Stay in current session

**Worktree Strategy**: Use git worktrees for feature isolation:
- Worktrees created in `.worktrees/` directory (gitignored)
- Create feature branch before starting implementation
- Clean up worktree after merge

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
```
