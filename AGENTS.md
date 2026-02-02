---
type: reference
scope: high-level
status: approved
version: 1.0.0
created: 2026-02-01
updated: 2026-02-02
subjects:
  - agents
  - documentation
  - claude-code
dependencies: []
---

# AGENTS.md - Agent Documentation for Realms of Idle

This document describes specialized agents available for working on the Realms of Idle project.

---

## GDD Designer - Game Design Documentation Specialist

**Agent File**: `.claude/skills/gdd-designer.md`

### Purpose

Expert RPG Game Designer and Architect responsible for authoring, reviewing, and curating all Game Design Documents (GDDs) for the project.

### Core Responsibilities

1. **Author GDDs**: Create clear, structured design documents following best practices
2. **Review Documentation**: Audit existing docs for compliance with standards
3. **Curate Architecture**: Maintain overall documentation structure for optimal navigation
4. **Enforce Standards**: Ensure all docs follow "Document >> Documentation" principles

### Key Principles

- **Single Source of Truth**: Each fact lives in ONE place
- **Progressive Disclosure**: Overview before details, split when too long
- **Discoverable Frontmatter**: Find the right doc without reading it all
- **Cross-Link Everything**: Related docs reference each other bidirectionally
- **Concise by Default**: Short as possible while complete

### When to Use

Invoke the GDD Designer agent when:

- ✅ Creating new game design documents
- ✅ Reviewing existing documentation for quality
- ✅ Restructuring or reorganizing docs
- ✅ Adding frontmatter to legacy documents
- ✅ Resolving documentation conflicts or duplication
- ✅ Planning documentation architecture for new features

### How to Invoke

```bash
# Via skill invocation
/skill gdd-designer

# Or reference the persona in conversation
"GDD Designer: Please review the alchemy scenario document"
```

### Document Type Schema

The GDD Designer enforces these document types:

- **scenario**: One of 7 idle game scenarios (Inn, Guild, Farm, Alchemy, Territory, Summoner, Caravan)
- **system**: Cross-cutting game system (progression, economy, combat, NPC, etc.)
- **mechanic**: Specific gameplay mechanic (crafting, trading, prestige, etc.)
- **technical**: Technical implementation details
- **narrative**: Story, world-building, character design
- **reference**: Data tables, reference info, glossaries

### Frontmatter Requirements

All documents MUST have this frontmatter:

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

Expert Agent Skill Author and Persona Architect responsible for reviewing, optimizing, and designing agent skill files to ensure progress disclosure, effective directives, and token efficiency.

### Core Responsibilities

1. **Review Skills**: Audit agent skills for quality, efficiency, and effectiveness
2. **Optimize Tokens**: Reduce token waste while maintaining clarity and functionality
3. **Enforce Progress Disclosure**: Ensure all skills report progress clearly
4. **Design Templates**: Create and maintain agent skill templates
5. **Directive Quality**: Verify imperative, concrete, unambiguous instructions

### Key Principles

- **Token Efficiency**: Every token must earn its place
- **Progress Disclosure**: Make agent state visible to user
- **Directive Quality**: Imperative, concrete, unambiguous
- **Structured Over Prose**: Tables, lists, templates beat paragraphs
- **Metrics-Driven**: Measure tokens, count savings, verify effectiveness

### When to Use

Invoke the Agent Skill Author when:

- ✅ Creating new agent skill files
- ✅ Reviewing existing skills for optimization
- ✅ Auditing skills for progress disclosure compliance
- ✅ Refactoring verbose skills into efficient versions
- ✅ Designing agent skill templates
- ✅ Token budget analysis required

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

The Agent Skill Author applies these optimization strategies:

1. **Remove filler words**: "In order to" → "To"
2. **Use tables**: Replace 5-line prose with 3-row table
3. **List with inline descriptions**: Don't separate into paragraphs
4. **Truncate examples**: Show 1 good example, not 3
5. **Delete politeness**: Agents are tools, not guests
6. **Use symbols**: ✅/❌ beats "Yes/No"
7. **Consolidate**: One heading, not three

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

Comprehensive multi-perspective code review performed BEFORE deeming a task complete, using specialized expert personas to identify and fix issues in-scope or track follow-on work.

### Core Responsibilities

1. **Code Quality Review**: Comprehensive code review for maintainability, readability, and best practices
2. **Security Audit**: Security vulnerability detection and mitigation
3. **Architecture Review**: Architectural validation and design pattern verification
4. **Issue Triage**: Categorize findings by severity (HIGH/MEDIUM/LOW) and scope
5. **Fix or Track**: Fix HIGH+ severity issues in-scope, create follow-on issues for others
6. **Plan Update**: Update plan progress and include in PR documentation

### Expert Personas Invoked

The Brutal Self Code-Review invokes these specialist agents sequentially:

| Persona               | Purpose                                                     | Model | Agent Type                         |
| --------------------- | ----------------------------------------------------------- | ----- | ---------------------------------- |
| **Code Reviewer**     | Comprehensive code quality, maintainability, best practices | opus  | `oh-my-claudecode:code-review`     |
| **Security Reviewer** | Security vulnerabilities, OWASP Top 10, secrets detection   | opus  | `oh-my-claudecode:security-review` |
| **Architect**         | Architectural validation, design patterns, system design    | opus  | `oh-my-claudecode:architect`       |

### When to Use

Invoke Brutal Self Code-Review AFTER implementation and verification, BEFORE pushing and creating PR:

- ✅ After all tests pass and verification complete
- ✅ Before git rebase and git push
- ✅ Before updating Issue and PR checklists
- ✅ For all non-trivial code changes (>50 lines or >3 files)
- ✅ For any security-sensitive changes
- ✅ For architectural changes or new features

### Workflow Integration

**Step 5 in GitHub Workflow** (between verify and rebase):

```bash
# After Step 4: Verify complete (0 issues, 0 warnings, 0 failures)

# Step 5: Brutal Self Code-Review
1. Invoke Code Reviewer: "Review this PR for code quality issues"
2. Invoke Security Reviewer: "Audit for security vulnerabilities"
3. Invoke Architect: "Validate architectural approach"
4. Triage findings: HIGH/MEDIUM/LOW × in-scope/out-of-scope
5. Fix all HIGH+ in-scope issues
6. Create follow-on issues for MEDIUM/LOW or out-of-scope
7. Update plan progress (docs/plans/{issue-number}.md)
8. Commit fixes and plan updates

# Then continue to Step 6: git rebase origin/main
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

1. **Always Invoke Base Trio**: Code Reviewer, Security Reviewer, Architect (for every PR)
2. **Detect Changes**: Use `git diff --name-only origin/main` to get changed files
3. **Match Patterns**: Apply file pattern globs to determine which additional experts to invoke
4. **Sequential Review**: Invoke experts in logical order (Architecture → Security → Code Quality → Specialized)
5. **Consolidate Findings**: Merge all expert feedback into single issue triage

**Specialized Persona Focus Areas**:

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

**HIGH+ (Must Fix Before PR)**:

- Security vulnerabilities (OWASP Top 10)
- Breaking changes or regressions
- Data loss or corruption risks
- Performance critical issues
- Test coverage gaps on critical paths

**MEDIUM (Fix or Track)**:

- Code smells or anti-patterns
- Minor performance optimizations
- Missing error handling on edge cases
- Documentation gaps
- Inconsistent naming or style

**LOW (Track as Follow-On)**:

- Nice-to-have improvements
- Refactoring opportunities
- Enhanced error messages
- Additional test cases
- Code cleanup or optimization

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

- **Time Budget**: Limit review to 15-30 minutes for typical PRs
- **Scope**: Review only changed files, not entire codebase
- **Fix Threshold**: Fix only HIGH+ severity in-scope; track others
- **Brutal but Constructive**: Critical feedback with actionable recommendations
- **No Blockers**: If unable to fix, document clearly and proceed

---

## PR Monitor - Pull Request Monitoring Specialist

**Agent File**: `.claude/skills/pr-monitor.md`

### Purpose

Expert Pull Request Monitoring Specialist responsible for tracking PRs from opening to successful auto-merge, ensuring all status checks pass, review comments are resolved, and dual-account workflow is maintained.

### Core Responsibilities

1. **Monitor PRs**: Track every PR from creation to successful merge
2. **Verify Account Credentials**: Ensure PRs opened by Contributor (mcj-codificer) and approved by Maintainer (mcj-coder)
3. **Check Status**: Verify all CI/CD, security, and quality checks passing
4. **Resolve Review Comments**: Track all comment chains to resolution (fix or follow-on issue)
5. **Confirm Merge Readiness**: Verify auto-merge enabled and all conditions met

### Key Principles

- **Monitor to completion**: Every PR tracked until auto-merge succeeds
- **Account integrity**: Contributor opens, Maintainer approves (no self-approval)
- **Automated first**: CI/CD and quality gates must pass before manual review
- **Comment resolution**: All review threads addressed before merge
- **Clean history**: Rebase workflow, no merge commits
- **Follow-on issues**: Out-of-scope discussions tracked as new GitHub issues

### When to Use

Invoke the PR Monitor agent when:

- ✅ A new PR is opened and needs monitoring
- ✅ Status checks fail and need diagnosis
- ✅ Review comments need tracking to resolution
- ✅ Account workflow violations need detection
- ✅ Merge readiness needs verification
- ✅ Follow-on issues need creation for out-of-scope discussions

### How to Invoke

```bash
# Via skill invocation
/skill pr-monitor

# Or reference the persona in conversation
"PR Monitor: Check the status of PR #42"
```

### Dual-Account Workflow

The PR Monitor enforces the dual-account pattern:

| Role        | Account       | Email                           | Purpose                          |
| ----------- | ------------- | ------------------------------- | -------------------------------- |
| Contributor | mcj-codificer | <m.c.j@live.co.uk>              | Opens PRs, implements features   |
| Maintainer  | mcj-coder     | <martin.cjarvis@googlemail.com> | Reviews, approves, enables merge |

**Critical Rules**:

- ✅ Contributor opens PR → Maintainer approves → Auto-merge (rebase)
- ❌ Maintainer opens PR → Cannot self-approve → Must close and reopen

### Monitoring Checklist

For each PR, the PR Monitor verifies:

- [ ] **Account**: Opened by Contributor (mcj-codificer)
- [ ] **Status Checks**: CI build, tests, security scan, code quality all passing
- [ ] **Review Comments**: All comment threads resolved
- [ ] **Maintainer Approval**: Approved by Maintainer (mcj-coder)
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

**Blocking Issues** (must fix before merge):

- Account workflow violations
- CI build failures
- Test failures
- Security vulnerabilities
- Unresolved review comments
- Merge conflicts

**Non-Blocking Issues** (can defer):

- Style preferences
- Nice-to-have improvements
- Out-of-scope enhancements → Create follow-on issue

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

### 1. Always Invoke via Skill

When you need specialized agent work, use the skill system:

```bash
# Direct invocation
/skill gdd-designer

# With context
"GDD Designer: I need to create a document for the new trading mechanic"
```

### 2. Provide Clear Context

When invoking an agent, specify:

- **What**: The task or question
- **Scope**: High-level or detailed work needed
- **Context**: Relevant background information
- **Output**: What deliverable you expect

### 3. Trust Agent Expertise

The agent has specialized knowledge. Trust their:

- Domain expertise (game design, documentation standards)
- Understanding of project-specific patterns
- Recommendations for structure and organization
- Judgement on when to escalate for human input

### 4. Review Before Committing

Always review agent output before:

- Adding to version control
- Sharing with team
- Marking as approved/complete
- Using as basis for implementation

---

## Agent Coordination

### GDD Designer + Other Agents

The GDD Designer works with other agents:

- **Systems Designer**: GDD creates docs, Systems provides technical details
- **Narrative Designer**: GDD structures docs, Narrative provides content
- **Technical Architect**: GDD defines requirements, Tech specifies implementation
- **Balance Designer**: GDD describes mechanics, Balance provides math

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

**Definition of Ready (DoR)** - Must be checked BEFORE starting:

- [ ] Issue has clear acceptance criteria
- [ ] Requirements are well-defined and understood
- [ ] Design/architecture approach documented
- [ ] Dependencies identified and available
- [ ] Test strategy defined
- [ ] DoD checklist agreed upon

**Definition of Done (DoD)** - Must be checked with FRESH EVIDENCE before complete:

- [ ] **Code Quality**: 0 lint issues, 0 build warnings (evidence: CI build link)
- [ ] **Tests**: All tests passing, coverage threshold met (evidence: test run link)
- [ ] **Security**: No vulnerabilities detected (evidence: security scan link)
- [ ] **Review**: Brutal Self Code-Review completed (evidence: review summary)
- [ ] **Documentation**: Code documented, plan updated (evidence: doc links)
- [ ] **DoR Met**: All DoR items verified (evidence: checklist link)
- [ ] **DoD Met**: All DoD items verified (evidence: checklist link)
- [ ] **Plan Updated**: Progress documented in plan file (evidence: plan link)

**Evidence Link Format**:

Each checklist item must have a fresh evidence link:

```markdown
- [x] **Code Quality**: 0 lint issues, 0 build warnings
  - Evidence: [CI Build #42](https://github.com/owner/repo/actions/runs/123456)
  - Verified: 2026-02-02T14:30:00Z
```

### PR Monitor + GitHub Workflow

The PR Monitor integrates into the dual-account GitHub workflow:

**After PR Creation (Step 7)**:

1. **Contributor** completes: verify → brutal review → rebase → push → create PR
2. **PR Monitor activates automatically**: Monitors from PR open to merge
3. **PR Monitor verifies**:
   - Account: Opened by Contributor (mcj-codificer)
   - Status checks: All CI/CD, security, quality passing
   - Review comments: All threads resolved
   - Auto-merge: Enabled by Maintainer after approval
   - DoR/DoD: All items checked with fresh evidence links
4. **Maintainer** reviews and approves
5. **PR Monitor confirms**: Successful merge via auto-merge (rebase)
6. **Maintainer**: Delete worktree + branch, close issue

**Automatic Invocation**:

PR Monitor is invoked after each task completion cycle:

```
Task Complete → Self Verified → Brutal Review → Committed → Pushed → PR Opened
                                                                      ↓
                                                         PR Monitor activates
                                                         ↓
                                                         Monitors until merge
```

**When PR Monitor Detects Issues**:

- **Account violation**: Reports workflow error, instructs to reopen with correct account
- **Status check failures**: Identifies failing checks, reports specific errors
- **Unresolved comments**: Lists outstanding review threads, tracks resolution
- **Auto-merge not enabled**: Reminds Maintainer to enable after approval
- **DoD incomplete**: Verifies all DoD items have fresh evidence links

### Agent Skill Author + All Agent Skills

The Agent Skill Author ensures quality and efficiency across all agent personas:

**When Agent Skills Are Created**:

1. **Designer creates skill**: Drafts new agent persona
2. **Agent Skill Author reviews**: Token audit, progress disclosure check, directive quality
3. **Optimization applied**: Verbose prose → structured format
4. **Template compliance**: Verified against skill template
5. **Approved for use**: Skill added to available personas

**When Agent Skills Are Modified**:

1. **Changes proposed**: Updates to existing skill file
2. **Agent Skill Author audits**: Token impact, effectiveness check
3. **Regression test**: Verify skill still functions correctly
4. **Version bump**: Update skill version number and date
5. **AGENTS.md updated**: Document changes if significant

**Continuous Improvement**:

- **Token budget monitoring**: Track skill file sizes over time
- **Progress disclosure audit**: Ensure all skills report clearly
- **Pattern extraction**: Identify reusable templates across skills
- **Optimization opportunities**: Flag verbose skills for refactoring

**Skill Quality Metrics**:

| Metric              | Target               | How Measured    |
| ------------------- | -------------------- | --------------- |
| Token efficiency    | 800-1200 per skill   | Token count     |
| Progress disclosure | 100% of skills       | Checklist       |
| Directive clarity   | Imperative, concrete | Manual review   |
| Template compliance | Follows structure    | Automated check |
| Version tracking    | All skills versioned | Manual review   |

### Escalation Path

When agents disagree or need human input:

1. **Agent identifies issue**: States clearly what needs decision
2. **Provides options**: Shows 2-3 approaches with trade-offs
3. **Recommends**: States agent's preference with rationale
4. **Human decides**: You make the call
5. **Agent implements**: Agent executes based on your decision

---

**Version**: 1.4
**Last Updated**: 2026-02-02
**Maintained By**: GDD Designer persona, PR Monitor persona, Brutal Self Code-Review personas, Expert Specialist personas, Agent Skill Author persona
