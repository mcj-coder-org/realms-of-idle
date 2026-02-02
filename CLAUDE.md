---
type: reference
scope: high-level
status: approved
version: 1.2.0
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

1. **Maintainer** creates issue (#N) with acceptance checklist
2. **Contributor**: `git worktree add ../feature-N -b type/N-description`
3. **Contributor** TDD: test → implement → commit
4. **Contributor** verify: 0 issues, 0 warnings, 0 failures
5. **Brutal Self Code-Review** (BEFORE deeming complete):
   - Invoke **Code Reviewer** persona for comprehensive review
   - Invoke **Security Reviewer** persona for security audit
   - Invoke **Architect** persona for architectural validation
   - **Fix all HIGH+ severity issues** found in-scope
   - **Create follow-on issues** for MEDIUM/LOW or out-of-scope concerns
   - **Update plan progress** with completed/in-progress tasks
   - **Include updated plan** in commit (documentation or plan file)
   - Commit fixes and plan updates
6. **Contributor** `git rebase origin/main` → fix conflicts → re-test
7. **Contributor** `git push` + `gh pr create`
8. **After push**: Update Issue checklist with PR link and completed tasks
9. **PR Monitor**: Monitor PR until auto-merge completes
   - Verify PR opened by Contributor account
   - Track all status checks (CI/CD, security, quality)
   - Ensure review comments resolved (fix or follow-on issue)
   - Confirm Maintainer approval and auto-merge enabled
   - Verify successful merge
10. **Maintainer** reviews, approves, enables auto-merge (rebase)
11. **Maintainer** (when checks pass): `git rebase origin/main` → `git merge --ff-only` → push
12. **Maintainer** delete worktree + branch, close issue

## 6. Definition of Ready (DoR) - Before Starting

**Maintainer** creates issue with DoR checklist:

- [ ] **Clear Acceptance Criteria**: Success criteria well-defined
- [ ] **Requirements Understood**: What to build is unambiguous
- [ ] **Design Documented**: Architecture approach documented (if needed)
- [ ] **Dependencies Identified**: External deps, blocked-by items listed
- [ ] **Test Strategy Defined**: How to verify acceptance criteria
- [ ] **DoD Agreed**: Definition of Done checklist attached

## 7. Definition of Done (DoD) - Before Complete

**Contributor** must verify ALL DoD items with FRESH EVIDENCE before task complete:

- [ ] **Code Quality**: 0 lint issues, 0 build warnings
  - Evidence: [CI Build Link](URL) - Verified: {timestamp}
- [ ] **Tests**: All tests passing, coverage ≥ threshold
  - Evidence: [Test Run Link](URL) - Verified: {timestamp}
- [ ] **Security**: No vulnerabilities detected
  - Evidence: [Security Scan Link](URL) - Verified: {timestamp}
- [ ] **Brutal Self Code-Review**: All expert reviews completed
  - Evidence: [Review Summary](URL) - Verified: {timestamp}
- [ ] **Documentation**: Code documented, plan updated
  - Evidence: [Doc Links](URL) - Verified: {timestamp}
- [ ] **DoR Met**: All DoR items verified
  - Evidence: [Issue Checklist](URL) - Verified: {timestamp}
- [ ] **DoD Met**: All DoD items verified with fresh evidence
  - Evidence: [PR Checklist](URL) - Verified: {timestamp}
- [ ] **Plan Updated**: Progress documented
  - Evidence: [Plan Link](URL) - Verified: {timestamp}

**CRITICAL**: Each evidence link MUST be fresh (within last hour) and point to actual verification output.

**Branch pattern**: `type/N-description` (feat/42-inventory, fix/117-leak, docs/23-readme, refactor/88-engine)

**Conventional Commits**: Reference the issue

- `feat(inventory): add item storage system #42`
- `fix(npc): resolve memory leak in simulation tick #117`

---

**Success indicators**: Minimal diffs, clarifying questions before mistakes, no rewrites from overcomplication.
