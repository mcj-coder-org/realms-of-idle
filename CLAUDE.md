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

---

# Autonomous Development Execution Protocol

This section defines the execution protocol for autonomous development tasks using subagents.

## Quality Gates

Every task must satisfy these quality gates before being marked complete:

1. **Minimal Changes**: Code changes ONLY what was explicitly requested. No "helpful" additions or premature abstractions.
2. **Best Practices**: Code follows existing project patterns, naming conventions, and error handling.
3. **Testing**: Tests are written and passing for all new functionality.
4. **No Breaking Changes**: Existing functionality remains intact.

## Task Delegation Requirements

When delegating to a subagent via the Task tool, you MUST include:

### 1. Quality Gates (VERBATIM)

Copy the quality gates above VERBATIM into your task prompt:

```
Quality Gates (VERBATIM):
1. Minimal Changes: Only implement what was specified
2. Best Practices: Follow existing code patterns and conventions
3. Testing: Write tests for all new functionality
4. No Breaking Changes: Ensure existing features continue working
```

### 2. Spec Reference

Include the relevant spec or design documentation. Include FULL requirements, not a summary:

```
## Spec Reference

[Full spec section or design requirements]
```

### 3. Delivery Plan Path

Always include the delivery plan path so the subagent can reference it:

```
Delivery Plan: docs/delivery-plan.md
```

## Post-Task Checklist

After each Task completes, review before accepting:

- [ ] Does this change ONLY what was requested?
- [ ] Are there "helpful" additions not in the spec?
- [ ] Can any lines be removed while still satisfying requirements?
- [ ] Does code follow project patterns?
- [ ] Are ALL quality gates satisfied?
- [ ] Tests written and passing?
- [ ] No breaking changes to existing code?

## Git Workflow

After verifying code quality:

1. Review changes: `git diff`
2. Stage relevant files: `git add <files>`
3. Create a commit: `git commit`

Uncommitted work may be lost. Commit before proceeding to next task.
