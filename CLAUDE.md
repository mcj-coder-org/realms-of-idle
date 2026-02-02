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

| Area    | Rule                              |
| ------- | --------------------------------- |
| Git     | Issue-required, worktree branches |
| Commits | Conventional, reference issue     |
| Quality | 0 lint, 0 warnings, 0 failures    |
| Process | TDD first, automate repeats       |
| main    | Protected (no direct commits)     |

## Dual-Account Workflow

| Role        | Purpose                  | Config            |
| ----------- | ------------------------ | ----------------- |
| Contributor | PRs, impl, fixes, rebase | `CLAUDE.local.md` |
| Maintainer  | Review, approve, merge   | `CLAUDE.local.md` |

```bash
# Account switch (see CLAUDE.local.md for values)
git config user.email "EMAIL" && git config user.name "NAME" && gh auth switch
```

**Tradeoff**: Caution > speed. Judge trivial tasks.

## Core Principles

1. **Think First**: State assumptions, present alternatives, ask when unclear
2. **Simple**: Minimal code, no speculation, single-use = no abstraction
3. **Surgical**: Edit only required, match style, clean only your orphans
4. **Verifiable**: Transform goals → tests/checks (e.g., "add validation" → "write tests → pass")

## 5. GitHub Workflow

| Step | Role        | Action                                                  |
| ---- | ----------- | ------------------------------------------------------- |
| 1    | Maintainer  | Create issue (#N) with DoR checklist                    |
| 2    | Contributor | `git worktree add ../feature-N -b type/N-description`   |
| 3    | Contributor | TDD: test → implement → commit                          |
| 4    | Contributor | Verify: 0 issues, 0 warnings, 0 failures                |
| 5    | Contributor | **Brutal Self Code-Review** (see below)                 |
| 6    | Contributor | `git rebase origin/main` → fix conflicts → re-test      |
| 7    | Contributor | `git push` + `gh pr create`                             |
| 8    | Contributor | Update issue checklist with PR link                     |
| 9    | Both        | Monitor PR until auto-merge completes                   |
| 10   | Maintainer  | Review, approve, enable auto-merge (rebase)             |
| 11   | Maintainer  | `git rebase origin/main` → `git merge --ff-only` → push |
| 12   | Maintainer  | Delete worktree + branch, close issue                   |

### Brutal Self Code-Review (Step 5)

**Always Invoke** (every PR):

- Code Reviewer, Security Reviewer, Architect

**Conditional Invoke** (file-pattern based):

| File Pattern                            | Persona                           |
| --------------------------------------- | --------------------------------- |
| `docs/design/**/*.md`                   | GDD Designer                      |
| `**/*.cs`                               | .NET 10 Best Practices Senior Dev |
| `**/ArchitectureTests/**/*.cs`          | Software Architect                |
| `**/Tests/**/*.cs`, `**/*.Tests.csproj` | Automation QA Expert              |
| `.github/**/*.yml`, `Dockerfile*`       | DevOps/Infrastructure             |
| API controllers, routes, OpenAPI        | API Documentation Specialist      |
| Performance-critical paths              | Performance Engineer              |

**Process**:

1. `git diff --name-only` → detect patterns
2. Invoke applicable personas
3. Fix all HIGH+ severity issues in-scope
4. Create follow-on issues for MEDIUM/LOW
5. Update plan progress, commit fixes

## 6. Definition of Ready (DoR)

Maintainer creates issue with checklist:

- [ ] Acceptance criteria well-defined
- [ ] Requirements unambiguous
- [ ] Architecture documented (if needed)
- [ ] Dependencies/blocks identified
- [ ] Test strategy defined
- [ ] DoD agreed

## 7. Definition of Done (DoD)

Contributor verifies ALL with fresh evidence (<1 hour old):

| Check         | Evidence Format                                  |
| ------------- | ------------------------------------------------ |
| Code Quality  | [CI Build](URL) - 0 lint, 0 warnings             |
| Tests         | [Test Run](URL) - all pass, coverage ≥ threshold |
| Security      | [Security Scan](URL) - 0 vulnerabilities         |
| Code Review   | [Review Summary](URL) - all experts completed    |
| Documentation | [Doc Links](URL) - code documented, plan updated |
| DoR Met       | [Issue Checklist](URL) - all items verified      |
| DoD Met       | [PR Checklist](URL) - all items verified         |
| Plan Updated  | [Plan Link](URL) - progress documented           |

## Conventions

**Branches**: `type/N-description` (feat/42-inventory, fix/117-leak, docs/23-readme, refactor/88-engine)

**Commits**: `feat(module): description #42`

---

**Success**: Minimal diffs, clarifying questions before mistakes, no rewrites.
