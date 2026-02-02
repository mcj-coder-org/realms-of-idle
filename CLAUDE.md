---
type: reference
scope: high-level
status: approved
version: 1.1.0
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
Apply during development (implementation/fixes/refactoring), not exploration.

## Git & Quality Standards

- **Issue Required**: No issue = no work
- **Worktree Branches**: `git worktree add ../feature-42 -b feat/42-description`
- **No Direct Commits to main**: Protected
- **Rebase Workflow**: `git rebase main` → `git merge --ff-only` → delete worktree
- **Conventional Commits**: Reference issue (e.g., `feat(module): description #42`)
- **Zero Tolerance**: 0 lint issues, 0 build warnings, 0 test failures
- **TDD First**: Tests before implementation
- **Automate**: Never repeat manually

## Dual-Account Workflow

This project uses two GitHub accounts for separation of concerns:

- **Contributor Account**: Opens PRs, implements features, fixes bugs, rebases branches
- **Maintainer Account**: Reviews PRs, approves changes, merges via auto-merge (rebase)

PRs must be opened by Contributor and approved/merged by Maintainer. Account details are stored in `CLAUDE.local.md` (never committed).

**Switching Accounts**:

```bash
# Switch to contributor (for opening PRs)
git config user.email "CONTRIBUTOR_EMAIL"
git config user.name "CONTRIBUTOR_USERNAME"
gh auth switch

# Switch to maintainer (for reviewing/merging)
git config user.email "MAINTAINER_EMAIL"
git config user.name "MAINTAINER_USERNAME"
gh auth switch --maintainer
```

See `CLAUDE.local.md` for actual account values and GitHub CLI setup.

</EXTREMELY_IMPORTANT>

**Tradeoff:** Caution over speed. Use judgment for trivial tasks.

## 1. Think Before Coding

State assumptions. Present alternatives for unclear requirements. Ask when uncertain.

## 2. Simplicity First

Minimum code. No speculative features. Single-use code needs no abstraction.
Test: "Would a senior engineer call this overcomplicated?"

## 3. Surgical Changes

Edit only what's required. Match existing style. Clean only your own orphans.
Test: Every changed line traces to user request.

## 4. Goal-Driven Execution

Transform to verifiable goals:

- "Add validation" → "Write tests for invalid inputs, then pass"
- "Fix bug" → "Write reproducing test, then pass"

Multi-step: `1. Step → verify: check`

## 5. GitHub Workflow (Dual-Account)

1. **Maintainer** creates issue (#N)
2. **Contributor**: `git worktree add ../feature-N -b type/N-description`
3. **Contributor** TDD: test → implement → commit
4. **Contributor** verify: 0 issues, 0 warnings, 0 failures
5. **Contributor** `git rebase origin/main` → fix conflicts → re-test
6. **Contributor** `git push` + `gh pr create`
7. **PR Monitor**: Monitor PR until auto-merge completes
   - Verify PR opened by Contributor account
   - Track all status checks (CI/CD, security, quality)
   - Ensure review comments resolved (fix or follow-on issue)
   - Confirm Maintainer approval and auto-merge enabled
   - Verify successful merge
8. **Maintainer** reviews, approves, enables auto-merge (rebase)
9. **Maintainer** (when checks pass): `git rebase origin/main` → `git merge --ff-only` → push
10. **Maintainer** delete worktree + branch, close issue

**Branch pattern**: `type/N-description` (feat/42-inventory, fix/117-leak, docs/23-readme, refactor/88-engine)

**Conventional Commits**: Reference the issue

- `feat(inventory): add item storage system #42`
- `fix(npc): resolve memory leak in simulation tick #117`

---

**Success indicators**: Minimal diffs, clarifying questions before mistakes, no rewrites from overcomplication.
