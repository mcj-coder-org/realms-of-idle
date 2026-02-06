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

<!-- VERBATIM: Quality Gates -->

Quality Gates (VERBATIM):

1. Minimal Changes: Only implement what was specified
2. Best Practices: Follow existing code patterns and conventions
3. Testing: Write tests for all new functionality
4. No Breaking Changes: Ensure existing features continue working
<!-- END VERBATIM -->

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

### 4. Context Strategy: QMD, grepai, or CodeContext

When delegating Tasks, provide appropriate context using the right search tool:

<!-- VERBATIM: Context Strategy -->
**QMD** (Semantic Documentation Search)
- **Use for:** Design docs, specs, GDDs, README files, architecture docs
- **When to include:** Task requires understanding design decisions, game mechanics, or system architecture
- **Example prompts:**
  - "Search QMD for 'idle progression system design'"
  - "Find documentation on combat resolution mechanics"
  - "What does the GDD say about prestige mechanics?"

**grepai** (Semantic Code Search)
- **Use for:** Finding specific implementations, functions, patterns in code
- **When to include:** Task requires understanding existing code, locating related implementations
- **Example prompts:**
  - "Search grepai for 'player health management'"
  - "Find code related to Orleans grain lifecycle"
  - "Show me implementations of IGameService"

**CodeContext** (Codebase Architecture Analysis)
- **Use for:** Understanding overall structure, directory layouts, symbol extraction
- **When to include:** Task requires high-level understanding of codebase organization
- **Example prompts:**
  - "Analyze the architecture of the Core domain layer"
  - "Show me the structure of the Server.Api project"
  - "What are the main components in the Maui client?"
<!-- END VERBATIM -->

**Decision Tree:**
1. Need design requirements or game mechanics? → **QMD**
2. Need existing code implementations? → **grepai**
3. Need to understand codebase structure? → **CodeContext**

**Including in Task Delegation:**

When you need context, include search instructions in your task prompt:

```

## Context Gathering

Before implementing, gather context using the appropriate tool:

1. Search QMD for "idle progression system" to understand design requirements
2. Search grepai for "IPlayerState" to find existing implementations
3. Use CodeContext to analyze the Core domain structure

```

**Important:** Do NOT summarize search results in your task prompt. The subagent will perform the searches themselves. Include the search queries for them to execute.

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
```
