# AGENTS.md - Agent Documentation for Realms of Idle

This document describes specialized agents available for working on the Realms of Idle project.

---

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
- ALWAYS automate repetitive tasks (git hooks, scripts, tools); never manual repeat
- ALWAYS DRY, YAGNI, Less Code >> More Code (avoid premature abstractions and over-engineering)

</EXTREMELY_IMPORTANT>

<!-- END VERBATIM -->

## GDD Designer - Game Design Documentation Specialist

**Agent File**: `.claude/skills/gdd-designer.md`

### Purpose

Author, review, and curate all Game Design Documents (GDDs).

### Core Responsibilities

| Responsibility          | Action                             |
| ----------------------- | ---------------------------------- |
| **Author GDDs**         | Create structured design documents |
| **Review Docs**         | Audit compliance with standards    |
| **Curate Architecture** | Maintain doc structure             |
| **Enforce Standards**   | Apply "Document >> Documentation"  |

### Key Principles

- **Single Source of Truth**: Each fact lives in ONE place
- **Progressive Disclosure**: Overview before details, split when too long
- **Discoverable Frontmatter**: Find the right doc without reading it all
- **Cross-Link Everything**: Related docs reference each other bidirectionally
- **Concise by Default**: Short as possible while complete

### When to Use

Invoke when:

- ✅ Creating new game design documents with `docs/design`
- ✅ Reviewing documentation quality
- ✅ Restructuring/reorganizing docs
- ✅ Adding frontmatter to legacy docs
- ✅ Resolving doc conflicts/duplication
- ✅ Planning doc architecture

### How to Invoke

```bash
# Via skill invocation
/skill gdd-designer

# Or reference the persona in conversation
"GDD Designer: Please review the alchemy scenario document"
```

### Document Types

| Type      | Purpose                                 | Examples                        |
| --------- | --------------------------------------- | ------------------------------- |
| scenario  | One of 7 idle game scenarios            | Inn, Guild, Farm, Alchemy, etc. |
| system    | Cross-cutting game system               | Progression, economy, combat    |
| mechanic  | Specific gameplay mechanic              | Crafting, trading, prestige     |
| technical | Implementation details                  | Architecture, patterns          |
| narrative | Story, world-building, character design | Lore, dialog, NPC backstories   |
| reference | Data tables, reference info, glossaries | Item stats, formulas            |

### Frontmatter Requirements

All `docs/design` documents MUST have this frontmatter:

```yaml
---
type: [scenario|system|mechanic|technical|narrative|reference]
scope: [high-level|detailed|implementation]
status: [draft|review|approved|deprecated]
version: 1.0.0
created: 2026-02-01
updated: 2026-02-01
subjects:
  - [subject-tag-1]
  - [subject-tag-2]
dependencies:
  - doc: [document-name]
    type: [requires|extends|refines|implements]
---
```

---

## Agent Skill Author - Persona Quality Assurance Specialist

**Agent File**: `.claude/skills/agent-skill-author.md`

### Purpose

Review, optimize, and design agent skill files for token efficiency and progress disclosure.

### Core Responsibilities

| Responsibility        | Action                                   |
| --------------------- | ---------------------------------------- |
| **Review Skills**     | Audit quality, efficiency, effectiveness |
| **Optimize Tokens**   | Reduce waste while maintaining clarity   |
| **Enforce Progress**  | Ensure clear progress reporting          |
| **Design Templates**  | Create/maintain agent skill templates    |
| **Verify Directives** | Ensure imperative, concrete instructions |

### Key Principles

- **Token Efficiency**: Every token must earn its place
- **Progress Disclosure**: Make agent state visible to user
- **Directive Quality**: Imperative, concrete, unambiguous
- **Structured Over Prose**: Tables, lists, templates beat paragraphs
- **Metrics-Driven**: Measure tokens, count savings, verify effectiveness

### When to Use

Invoke when:

- ✅ Creating new agent skill files
- ✅ Reviewing skills for optimization
- ✅ Auditing progress disclosure compliance
- ✅ Refactoring verbose → efficient
- ✅ Designing agent skill templates
- ✅ Token budget analysis needed

### How to Invoke

```bash
# Via skill invocation
/skill agent-skill-author

# Or reference the persona in conversation
"Agent Skill Author: Review the PR Monitor skill for token efficiency"
```

### Token Budget Guidelines

The Agent Skill Author enforces these token budgets:

| Section              | Max Tokens | Target       |
| -------------------- | ---------- | ------------ |
| Agent Persona        | 100        | 50-80        |
| Core Philosophy      | 200        | 100-150      |
| Agent Capabilities   | 50 per     | 30-40 per    |
| Domain Expertise     | 150        | 80-120       |
| Interaction Patterns | 150        | 100-120      |
| Constraints          | 100        | 60-80        |
| Tone and Voice       | 80         | 40-60        |
| Example Interactions | 300        | 150-200      |
| **Total Skill File** | **1500**   | **800-1200** |

### Optimization Techniques

| Technique         | Example                         |
| ----------------- | ------------------------------- |
| **Remove filler** | "In order to" → "To"            |
| **Use tables**    | 5-line prose → 3-row table      |
| **Inline lists**  | Combine descriptions with items |
| **Truncate**      | 1 example, not 3                |
| **Delete fluff**  | Agents are tools, not guests    |
| **Use symbols**   | ✅/❌ beats "Yes/No"            |
| **Consolidate**   | One heading, not three          |

### Progress Disclosure Standards

Enforces progress reporting in all agent skills:

**Required Elements**:

- **Initial Acknowledgment**: Confirm understanding (20 tokens)
- **Step Updates**: Report after each step (10 tokens/step)
- **Blocker Reporting**: State what blocks and why (25 tokens)
- **Completion Signal**: Clear indication work is done (15 tokens)

**Progress Template**:

```markdown
I'll [task]. Approach:

1. [Step 1]
2. [Step 2]

✅ [Step N]: [Result]
⚠️ BLOCKED: [What] - Reason: [Why]
✅ COMPLETE: [Summary]
```

### Review Checklist

When reviewing agent skills, verifies:

- [ ] **Token Efficiency**: No redundant words, dense packing
- [ ] **Progress Disclosure**: Clear reporting mechanisms
- [ ] **Directive Quality**: Imperative, concrete instructions
- [ ] **Output Specification**: Clear expected format
- [ ] **Constraints Stated**: Explicit boundaries
- [ ] **Examples Provided**: Good/bad examples
- [ ] **Tone Consistency**: Professional throughout
- [ ] **Version Tracked**: Version and date

---

## Brutal Self Code-Review - Quality Assurance Before PR

**Agent Files**: Multiple expert personas (oh-my-claudecode agents)

### Purpose

Multi-perspective code review BEFORE task completion using expert personas to fix in-scope issues or track follow-ons.

### Core Responsibilities

| Responsibility          | Action                                              |
| ----------------------- | --------------------------------------------------- |
| **Code Quality**        | Review maintainability, readability, best practices |
| **Security Audit**      | Detect/mitigate vulnerabilities                     |
| **Architecture Review** | Validate design patterns, system design             |
| **Issue Triage**        | Categorize by severity (HIGH/MEDIUM/LOW) and scope  |
| **Fix or Track**        | Fix HIGH+ in-scope, create follow-ons for others    |
| **Plan Update**         | Update plan progress, include in PR documentation   |

### Expert Personas Invoked

The Brutal Self Code-Review invokes these specialist agents sequentially:

| Persona               | Purpose                                                     | Model | Agent Type                         |
| --------------------- | ----------------------------------------------------------- | ----- | ---------------------------------- |
| **Code Reviewer**     | Comprehensive code quality, maintainability, best practices | opus  | `oh-my-claudecode:code-review`     |
| **Security Reviewer** | Security vulnerabilities, OWASP Top 10, secrets detection   | opus  | `oh-my-claudecode:security-review` |
| **Architect**         | Architectural validation, design patterns, system design    | opus  | `oh-my-claudecode:architect`       |

### When to Use

Invoke AFTER verification, BEFORE git push:

- ✅ Tests pass, verification complete
- ✅ Before `git rebase` and `git push`
- ✅ Before updating Issue/PR checklists
- ✅ Non-trivial changes (>50 lines or >3 files)
- ✅ Security-sensitive changes
- ✅ Architectural changes/new features

### Workflow Integration

**Step 5 in GitHub Workflow** (verify → rebase):

```bash
# After Step 4: Verify complete (0 issues, 0 warnings, 0 failures)

# Step 5: Brutal Self Code-Review
1. Invoke Code Reviewer: "Review for code quality issues"
2. Invoke Security Reviewer: "Audit for security vulnerabilities"
3. Invoke Architect: "Validate architectural approach"
4. Triage: HIGH/MEDIUM/LOW × in-scope/out-of-scope
5. Fix all HIGH+ in-scope
6. Create follow-on issues for MEDIUM/LOW/out-of-scope
7. Update plan progress (docs/plans/{issue-number}.md)
8. Commit fixes + plan updates

# Continue to Step 6: git rebase origin/main
```

### File Pattern-Based Persona Invocation

Expert personas are invoked based on detected file changes:

**Detection Command**:

```bash
# Get list of changed files in current branch
CHANGED_FILES=$(git diff --name-only origin/main)

# Check for file patterns
echo "$CHANGED_FILES" | grep -q "docs/design/.*\.md$" && invoke_gdd_designer=true
echo "$CHANGED_FILES" | grep -q "\.cs$" && invoke_dotnet_expert=true
echo "$CHANGED_FILES" | grep -E "(\.csproj|solution\.sln)" && invoke_architect=true
echo "$CHANGED_FILES" | grep -E "Tests/.*\.cs$|.*\.Tests\.csproj$" && invoke_qa_expert=true
```

**Persona Invocation Matrix**:

| File Pattern Change                    | Expert Persona                              | Agent Type                          | Purpose                               |
| -------------------------------------- | ------------------------------------------- | ----------------------------------- | ------------------------------------- |
| **Always Invoke**                      |                                             |                                     |                                       |
| Any file change                        | **Code Reviewer**                           | `oh-my-claudecode:code-review`      | Comprehensive code quality            |
| Any file change                        | **Security Reviewer**                       | `oh-my-claudecode:security-review`  | Security vulnerability audit          |
| Any file change                        | **Architect**                               | `oh-my-claudecode:architect`        | Architectural validation              |
| **Conditional Invoke**                 |                                             |                                     |                                       |
| `docs/design/**/*.md`                  | **GDD Designer**                            | `.claude/skills/gdd-designer.md`    | Design doc compliance                 |
| `**/*.cs`                              | **.NET 10 Best Practices Senior Developer** | `oh-my-claudecode:architect-medium` | C# modern practices, .NET 10 features |
| New `*.csproj` or `.sln`               | **Software Architect**                      | `oh-my-claudecode:architect`        | Component/project architecture        |
| `tests/**/*.cs` or `**/*.Tests.csproj` | **Automation QA Expert**                    | `oh-my-claudecode:qa-tester`        | Test quality and coverage             |
| `Tests/Architecture/**/*.cs`           | **Software Architect**                      | `oh-my-claudecode:architect`        | Architecture test validation          |
| `.github/workflows/*.yml`              | **DevOps/Infrastructure**                   | `oh-my-claudecode:architect-medium` | CI/CD pipeline validation             |
| `Dockerfile`, `*.dockerfile`           | **DevOps/Infrastructure**                   | `oh-my-claudecode:architect-medium` | Container configuration               |
| `docker-compose*.yml`                  | **DevOps/Infrastructure**                   | `oh-my-claudecode:architect-medium` | Multi-container orchestration         |
| `**/Controllers/**/*.cs`               | **API Documentation Specialist**            | `oh-my-claudecode:code-reviewer`    | API design and documentation          |
| `**/Routes/**`, `OpenAPI*.yml`         | **API Documentation Specialist**            | `oh-my-claudecode:code-reviewer`    | API specification quality             |
| Performance-critical paths\*           | **Performance Engineer**                    | `oh-my-claudecode:architect`        | Performance optimization              |

\*Identified by Architect during initial review

**Invocation Rules**:

1. **Always Invoke Base Reviewers**: Code Reviewer, Security Reviewer, Architect, User
2. **Detect Changes**: Use `git diff --name-only origin/main` to get changed files
3. **Match Patterns**: Apply file pattern globs to determine which additional experts to invoke
4. **Sequential Review**: Invoke experts in logical order (Architecture → Security → Code Quality → Specialized)
5. **Consolidate Findings**: Merge all expert feedback into single issue triage

**Specialized Persona Focus Areas**:

**User**

- Game is playable and accessible
- UI is graphical and not text heavy
- UI is responsive to interactions

**GDD Designer** (when `docs/design/**/*.md` changed):

- Frontmatter compliance
- Single subject focus
- Cross-linking
- Progressive disclosure
- Hierarchical organization
- Co-located supplements

**.NET 10 Best Practices Senior Developer** (when `**/*.cs` changed):

- Modern C# 12+ features (records, pattern matching, Span<T>/Memory<T>)
- Async/await best practices
- LINQ performance
- Memory management
- Exception handling patterns
- Nullable reference types
- Primary constructors
- Collection expressions

**Software Architect** (when new projects/components or architecture tests):

- Component boundaries and responsibilities
- Dependency direction
- Architecture test validity
- Integration patterns
- Coupling and cohesion
- SOLID principles
- Design patterns appropriate use

**Automation QA Expert** (when test projects modified):

- Test coverage completeness
- Test quality and maintainability
- Appropriate test use (unit, integration, e2e)
- Test isolation
- Mock/stub usage
- Assertion quality
- Test naming conventions

**DevOps/Infrastructure** (when CI/CD or container configs changed):

- Workflow logic correctness
- Security best practices (secrets management)
- Resource optimization
- Failure handling
- Container layer optimization
- Multi-stage builds
- Dependency caching

**API Documentation Specialist** (when API surfaces changed):

- RESTful conventions
- OpenAPI spec completeness
- Endpoint documentation
- Request/response schema clarity
- Error handling documentation
- Versioning strategy

**Performance Engineer** (when performance-critical code changed):

- Algorithm complexity
- Memory allocations
- Hot path optimization
- Caching strategies
- Async/await efficiency
- Database query optimization
- Profiling recommendations

### Issue Severity Classification

| Severity   | Action          | Criteria                                                              |
| ---------- | --------------- | --------------------------------------------------------------------- |
| **HIGH+**  | Fix before PR   | Security vulns, breaking changes, data loss, critical perf, test gaps |
| **MEDIUM** | Fix or track    | Code smells, minor perf, edge case handling, docs, style              |
| **LOW**    | Track follow-on | Nice-to-haves, refactoring, error messages, test cases, cleanup       |

### Follow-On Issue Template

```markdown
# Follow-Up from Brutal Self Code-Review

**Source**: PR/Issue #{N} - {Title}
**Reviewer**: {Persona Name}
**Severity**: Medium | Low
**Category**: Code Quality | Security | Architecture

## Issue Found

{Detailed description of the issue found during review}

## Location

- **File**: {path/to/file.ext}
- **Lines**: {start-end}
- **Function**: {function_name}

## Why Not Fixed in PR

{Explain why this wasn't fixed in the current PR:

- Out of scope for current issue
- Requires broader discussion
- Lower priority than main task
- Risk of introducing regressions}

## Proposed Approach

{Suggested solution or investigation approach}

## Priority Assessment

**{Medium/Low}** - {Rationale for priority}

**Labels**: `follow-up`, `from-review`, `{category}`
```

### Plan Update Format

After Brutal Self Code-Review, update the plan document:

```markdown
# Plan Progress Update: Issue #{N}

## Completed Tasks

- [x] Task 1: {description} (completed 2026-02-02)
- [x] Task 2: {description} (completed 2026-02-02)

## In-Progress Tasks

- [ ] Task 3: {description}
  - Status: {blocked by/in progress}
  - Notes: {progress notes}

## Review Findings

### Fixed In-Scope (HIGH+)

- ✅ {Issue 1} - Fixed via commit {abc123}
- ✅ {Issue 2} - Fixed via commit {def456}

### Tracked as Follow-On (MEDIUM/LOW)

- 📝 Issue #{N+1} - {Title}
- 📝 Issue #{N+2} - {Title}

### Deferred (Out of Scope)

- 📋 {Item 1} - Rationale
- 📋 {Item 2} - Rationale

## Next Steps

1. {Next action item}
2. {Next action item}

**Updated**: 2026-02-02
**Reviewed By**: Code Reviewer, Security Reviewer, Architect
```

### Output Format

After completing Brutal Self Code-Review, provide:

```markdown
## Brutal Self Code-Review Complete

**Issue**: #{N} - {Title}
**Reviewer**: Code Reviewer, Security Reviewer, Architect
**Duration**: {time taken}

### Summary

- **Total Issues Found**: {count}
- **HIGH (Fixed)**: {count}
- **MEDIUM (Tracked)**: {count}
- **LOW (Tracked)**: {count}

### Fixes Applied

1. **{Issue Title}** (HIGH)
   - Location: {file}:{lines}
   - Fix: {description}
   - Commit: {hash}

### Follow-On Issues Created

1. **Issue #{N+1}**: {Title}
   - Severity: Medium
   - Category: {type}
   - Link: {URL}

2. **Issue #{N+2}**: {Title}
   - Severity: Low
   - Category: {type}
   - Link: {URL}

### Plan Updated

- Plan document: `docs/plans/{N}.md`
- Progress: {X/Y tasks completed}
- Status: {Ready for PR | Needs additional work}

### Ready to Proceed

✅ All HIGH+ severity issues fixed
✅ Plan progress updated and committed
✅ Ready for git rebase and push
```

### Constraints

| Constraint        | Rule                                       |
| ----------------- | ------------------------------------------ |
| **Time Budget**   | 15-30 minutes per typical PR               |
| **Scope**         | Review only changed files                  |
| **Fix Threshold** | Fix HIGH+ in-scope only; track others      |
| **Feedback**      | Brutal but constructive with actions       |
| **No Blockers**   | Document clearly if unable to fix, proceed |

---

## PR Monitor - Pull Request Monitoring Specialist

**Agent File**: `.claude/skills/pr-monitor.md`

### Purpose

Track PRs from opening to successful auto-merge, ensuring checks pass, comments resolve, and dual-account workflow maintained.

### Core Responsibilities

| Responsibility          | Action                                         |
| ----------------------- | ---------------------------------------------- |
| **Monitor PRs**         | Track from creation to successful merge        |
| **Verify Accounts**     | Ensure Contributor opens, Maintainer approves  |
| **Check Status**        | Verify CI/CD, security, quality checks passing |
| **Resolve Comments**    | Track comment chains to resolution             |
| **Confirm Merge Ready** | Verify auto-merge enabled, conditions met      |

### Key Principles

- **Monitor to completion**: Every PR tracked until auto-merge succeeds
- **Account integrity**: Contributor opens, Maintainer approves (no self-approval)
- **Automated first**: CI/CD and quality gates must pass before manual review
- **Comment resolution**: All review threads addressed before merge
- **Clean history**: Rebase workflow, no merge commits
- **Follow-on issues**: Out-of-scope discussions tracked as new GitHub issues

### When to Use

Invoke when:

- ✅ New PR opened, needs monitoring
- ✅ Status checks failing, need diagnosis
- ✅ Review comments need tracking to resolution
- ✅ Account workflow violations detected
- ✅ Merge readiness verification needed
- ✅ Follow-on issues needed for out-of-scope discussions

### How to Invoke

```bash
# Via skill invocation
/skill pr-monitor

# Or reference the persona in conversation
"PR Monitor: Check the status of PR #42"
```

### Dual-Account Workflow

The PR Monitor enforces the dual-account pattern:

| Role        | Purpose                          |
| ----------- | -------------------------------- |
| Contributor | Opens PRs, implements features   |
| Maintainer  | Reviews, approves, enables merge |

**Critical Rules**:

- ✅ Contributor opens PR → Maintainer approves → Auto-merge (rebase)
- ❌ Maintainer opens PR → Cannot self-approve → Must close and reopen

> **Note**: See CLAUDE.local.md for specific account names, emails, and GPG keys configured for this project.

### Monitoring Checklist

For each PR, the PR Monitor verifies:

- [ ] **Account**: Opened by Contributor (not Maintainer)
- [ ] **Status Checks**: CI build, tests, security scan, code quality all passing
- [ ] **Review Comments**: All comment threads resolved
- [ ] **Maintainer Approval**: Approved by Maintainer
- [ ] **Auto-Merge**: Enabled with rebase method
- [ ] **No Conflicts**: Branch can be cleanly rebased
- [ ] **Conventional Commits**: All commits follow pattern
- [ ] **Issue Reference**: Commits reference issue #{N}

### Status Report Format

```markdown
## PR Monitor: #{PR Number} - {PR Title}

### Status Summary

| Category   | Status | Notes                |
| ---------- | ------ | -------------------- |
| Account    | ✅/❌  | Opened by {username} |
| CI/CD      | ✅/❌  | {details}            |
| Review     | ✅/❌  | {details}            |
| Auto-Merge | ✅/❌  | {details}            |

### Issues Requiring Action

1. **[Priority]**: {Issue description}
   - Location: {where}
   - Action: {what to do}
   - Owner: {who should do it}
```

### Issue Categories

| Category         | Issues                                                                                         | Action                   |
| ---------------- | ---------------------------------------------------------------------------------------------- | ------------------------ |
| **Blocking**     | Account violations, CI failures, test failures, security vulns, unresolved comments, conflicts | Must fix before merge    |
| **Non-Blocking** | Style preferences, nice-to-haves, out-of-scope enhancements                                    | Defer or follow-on issue |

---

## Additional Agents

As the project grows, additional specialized agents will be documented here:

### Planned Agents

- **Systems Designer**: Core game systems (economy, progression, combat)
- **Narrative Designer**: Story, world-building, character design
- **Technical Architect**: Implementation details, architecture decisions
- **Balance Designer**: Game balance, tuning, math modeling
- **Content Designer**: Specific content (items, enemies, quests)

---

## Agent Usage Guidelines

### Usage Guidelines

| Guideline             | Action                                                  |
| --------------------- | ------------------------------------------------------- |
| **Invoke via Skill**  | `/skill gdd-designer` or "GDD Designer: [task]"         |
| **Provide Context**   | What, scope, background, expected output                |
| **Trust Expertise**   | Domain knowledge, patterns, recommendations, escalation |
| **Review Before Use** | Check before VCS, sharing, approval, implementation     |

---

## Agent Coordination

### Agent Coordination

**GDD Designer + Others**:

| Agent               | Collaboration                                           |
| ------------------- | ------------------------------------------------------- |
| Systems Designer    | GDD creates docs, Systems provides tech                 |
| Narrative Designer  | GDD structures, Narrative provides content              |
| Technical Architect | GDD defines requirements, Tech specifies implementation |
| Balance Designer    | GDD describes mechanics, Balance provides math          |

### Brutal Self Code-Review + PR Workflow

The Brutal Self Code-Review is a critical quality gate BEFORE PR creation:

**Step 5 in GitHub Workflow** (After verify, Before rebase):

```
1. Maintainer creates issue with DoR/DoD checklists
2. Contributor: git worktree add
3. Contributor TDD: test → implement → commit
4. Contributor verify: 0 issues, 0 warnings, 0 failures
5. Brutal Self Code-Review:
   ├─ Invoke Code Reviewer → Find issues
   ├─ Invoke Security Reviewer → Find vulnerabilities
   ├─ Invoke Architect → Validate design
   ├─ Fix all HIGH+ issues in-scope
   ├─ Create follow-on issues for MEDIUM/LOW
   ├─ Update plan progress
   └─ Commit fixes and plan updates
6. Contributor: git rebase origin/main
7. Contributor: git push + gh pr create
8. Update Issue checklist with PR link
9. PR Monitor activates
10. Maintainer reviews/approves
11. Maintainer merges
12. Maintainer cleanup
```

**Definition of Ready (DoR)** - Check BEFORE starting:

- [ ] Clear acceptance criteria
- [ ] Requirements well-defined and understood
- [ ] Design/architecture documented
- [ ] Dependencies identified and available
- [ ] Test strategy defined
- [ ] DoD checklist agreed

**Definition of Done (DoD)** - Check with FRESH EVIDENCE:

- [ ] **Code Quality**: 0 lint, 0 build warnings (evidence: CI build link)
- [ ] **Tests**: All passing, coverage met (evidence: test run link)
- [ ] **Security**: No vulnerabilities (evidence: security scan link)
- [ ] **Review**: Brutal Self Code-Review completed (evidence: review summary)
- [ ] **Documentation**: Code documented, plan updated (evidence: doc links)
- [ ] **DoR/DoD Met**: All items verified (evidence: checklist links)
- [ ] **Plan Updated**: Progress documented (evidence: plan link)

**Evidence Link Format**:

Each checklist item must have a fresh evidence link:

```markdown
- [x] **Code Quality**: 0 lint issues, 0 build warnings
  - Evidence: [CI Build #42](https://github.com/owner/repo/actions/runs/123456)
  - Verified: 2026-02-02T14:30:00Z
```

### PR Monitor + GitHub Workflow

**After PR Creation (Step 7)**:

1. Contributor: verify → brutal review → rebase → push → create PR
2. PR Monitor activates: Monitors from open to merge
3. PR Monitor verifies:
   - Account: Opened by Contributor (mcj-codificer)
   - Status checks: All CI/CD, security, quality passing
   - Review comments: All threads resolved
   - Auto-merge: Enabled by Maintainer after approval
   - DoR/DoD: All items checked with fresh evidence links
4. Maintainer reviews and approves
5. PR Monitor confirms: Successful merge via auto-merge (rebase)
6. Maintainer: Delete worktree + branch, close issue

**Automatic Invocation**:

```
Task Complete → Self Verified → Brutal Review → Committed → Pushed → PR Opened
                                                                      ↓
                                                         PR Monitor activates
```

**Issue Detection**:

| Issue               | Action                                             |
| ------------------- | -------------------------------------------------- |
| Account violation   | Report error, instruct reopen with correct account |
| Status check fails  | Identify failing checks, report specific errors    |
| Unresolved comments | List outstanding threads, track resolution         |
| Auto-merge disabled | Remind Maintainer to enable after approval         |
| DoD incomplete      | Verify all DoD items have fresh evidence links     |

### Agent Skill Author + All Agent Skills

**Creation Workflow**:

1. Designer drafts skill
2. Agent Skill Author reviews: token audit, progress disclosure, directive quality
3. Optimize: Verbose prose → structured format
4. Verify template compliance
5. Approve and add to personas

**Modification Workflow**:

1. Propose changes to existing skill
2. Agent Skill Author audits: token impact, effectiveness
3. Regression test: Verify still functions
4. Bump version and date
5. Update AGENTS.md if significant

**Continuous Improvement**:

| Activity            | Action                           |
| ------------------- | -------------------------------- |
| Token monitoring    | Track skill file sizes over time |
| Progress disclosure | Audit all skills report clearly  |
| Pattern extraction  | Identify reusable templates      |
| Optimization flags  | Refactor verbose skills          |

**Skill Quality Metrics**:

| Metric              | Target               | How Measured    |
| ------------------- | -------------------- | --------------- |
| Token efficiency    | 800-1200 per skill   | Token count     |
| Progress disclosure | 100% of skills       | Checklist       |
| Directive clarity   | Imperative, concrete | Manual review   |
| Template compliance | Follows structure    | Automated check |
| Version tracking    | All skills versioned | Manual review   |

### Escalation Path

| Step             | Action                              |
| ---------------- | ----------------------------------- |
| Identify issue   | Agent states what needs decision    |
| Provide options  | Show 2-3 approaches with trade-offs |
| Recommend        | State preference with rationale     |
| Human decides    | You make the call                   |
| Agent implements | Execute based on your decision      |

---

**Version**: 1.4
**Last Updated**: 2026-02-02
**Maintained By**: GDD Designer persona, PR Monitor persona, Brutal Self Code-Review personas, Expert Specialist personas, Agent Skill Author persona
