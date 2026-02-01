# CLAUDE.md

<EXTREMELY_IMPORTANT>

These apply during active development (implementation, fixes, refactoring), not exploration/research.

## Git Workflow

- **ALL work MUST have a GitHub issue** - No exceptions. No issue = no work.
- **ALWAYS use feature branches with git worktrees** - Isolates work, prevents commits to main
  - Create worktree: `git worktree add ../feature-123 -b feature/123-description`
  - Use branch name pattern: `type/issue-number-description` (e.g., `feat/42-add-player-inventory`)
- **NEVER commit directly to main** - Protected by pre-commit hook
- **ALL merges MUST use rebase** - No merge commits, linear history only
  - Update feature branch: `git rebase main`
  - Merge to main: `git merge --ff-only feature/123-description` (after rebase)
  - Delete branch after merge: `git worktree remove ../feature-123`

## Code Quality

- NEVER skip or weaken GitHooks (--no-verify requires explicit user permission); always fix root causes
- NEVER create merge commits; use rebase for linear history, then --ff-only for merges
- ALWAYS use Conventional Commit messages
- ALWAYS commit after self-verification but before declaring complete (run tests/build/lint first)
- ALWAYS treat Warnings as Errors (configure in tooling)
  - 0 Linting Issues
  - 0 Build Warnings
  - 0 Commit Warnings
  - 0 Build Errors
  - 0 Test Failures (tests must be run)
- ALWAYS TDD + Automated Tests First during implementation (not during planning/exploration)
- ALWAYS automate repetitive tasks (git hooks, scripts, tools); never manual repeat
- ALWAYS DRY, YAGNI, Less Code >> More Code (avoid premature abstractions and over-engineering)

</EXTREMELY_IMPORTANT>

**Tradeoff:** These guidelines bias toward caution over speed. For trivial tasks, use judgment.

## 1. Think Before Coding

**Don't assume. Don't hide confusion. Surface tradeoffs.**

Before implementing:

- State your assumptions explicitly. If uncertain, ask.
- If multiple interpretations exist, present them - don't pick silently.
- If a simpler approach exists, say so. Push back when warranted.
- If something is unclear, stop. Name what's confusing. Ask.

## 2. Simplicity First

**Minimum code that solves the problem. Nothing speculative.**

- No features beyond what was asked.
- No abstractions for single-use code.
- No "flexibility" or "configurability" that wasn't requested.
- No error handling for impossible scenarios.
- If you write 200 lines and it could be 50, rewrite it.

Ask yourself: "Would a senior engineer say this is overcomplicated?" If yes, simplify.

## 3. Surgical Changes

**Touch only what you must. Clean up only your own mess.**

When editing existing code:

- Don't "improve" adjacent code, comments, or formatting.
- Don't refactor things that aren't broken.
- Match existing style, even if you'd do it differently.
- If you notice unrelated dead code, mention it - don't delete it.

When your changes create orphans:

- Remove imports/variables/functions that YOUR changes made unused.
- Don't remove pre-existing dead code unless asked.

The test: Every changed line should trace directly to the user's request.

## 4. Goal-Driven Execution

**Define success criteria. Loop until verified.**

Transform tasks into verifiable goals:

- "Add validation" → "Write tests for invalid inputs, then make them pass"
- "Fix the bug" → "Write a test that reproduces it, then make it pass"
- "Refactor X" → "Ensure tests pass before and after"

For multi-step tasks, state a brief plan:

```
1. [Step] → verify: [check]
2. [Step] → verify: [check]
3. [Step] → verify: [check]
```

Strong success criteria let you loop independently. Weak criteria ("make it work") require constant clarification.

## 5. GitHub Issue Workflow

**Every piece of work starts with an issue.**

```
┌─────────────────────────────────────────────────────────────┐
│                    COMPLETE WORKFLOW                        │
├─────────────────────────────────────────────────────────────┤
│                                                             │
│ 1. CREATE ISSUE                                            │
│    • Use GitHub issue template if available                │
│    • Include: Description, acceptance criteria, priority   │
│    • Note the issue number (e.g., #42)                     │
│                                                             │
│ 2. CREATE FEATURE WORKTREE                                 │
│    git worktree add ../feature-42 -b feat/42-add-login     │
│    cd ../feature-42                                         │
│                                                             │
│ 3. IMPLEMENT (TDD)                                         │
│    • Write tests first                                     │
│    • Implement minimal code to pass                        │
│    • Commit frequently with conventional commits           │
│                                                             │
│ 4. SELF-VERIFY                                             │
│    npm run lint && npm test && dotnet build               │
│    • 0 linting issues                                     │
│    • 0 build warnings                                     │
│    • 0 test failures                                      │
│                                                             │
│ 5. REBASE ON MAIN                                          │
│    git fetch origin                                        │
│    git rebase origin/main                                  │
│    • Fix any conflicts                                     │
│    • Re-run tests                                          │
│                                                             │
│ 6. PUSH & CREATE PR                                        │
│    git push -u origin feat/42-add-login                    │
│    gh pr create --title "feat: add login system #42"       │
│    • Reference issue in title/description                  │
│                                                             │
│ 7. MERGE (after review)                                   │
│    • In main worktree:                                     │
│    git fetch origin                                        │
│    git rebase origin/main                                  │
│    git merge --ff-only feat/42-add-login                   │
│    git push origin main                                    │
│                                                             │
│ 8. CLEANUP                                                 │
│    git worktree remove ../feature-42                       │
│    git branch -d feat/42-add-login                         │
│    Close issue #42                                          │
│                                                             │
└─────────────────────────────────────────────────────────────┘
```

**Branch Naming:** `type/issue-number-description`

- `feat/42-add-player-inventory`
- `fix/117-memory-leak-in-npc-simulation`
- `docs/23-update-readme`
- `refactor/88-extract-game-engine-interface`

**Conventional Commits:** Reference the issue

- `feat(inventory): add item storage system #42`
- `fix(npc): resolve memory leak in simulation tick #117`

---

**These guidelines are working if:** fewer unnecessary changes in diffs, fewer rewrites due to overcomplication, and clarifying questions come before implementation rather than after mistakes.
