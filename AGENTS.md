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

### PR Monitor + GitHub Workflow

The PR Monitor integrates into the dual-account GitHub workflow:

**After PR Creation (Step 6)**:

1. **Contributor** completes: verify → rebase → push → create PR
2. **PR Monitor activates automatically**: Monitors from PR open to merge
3. **PR Monitor verifies**:
   - Account: Opened by Contributor (mcj-codificer)
   - Status checks: All CI/CD, security, quality passing
   - Review comments: All threads resolved
   - Auto-merge: Enabled by Maintainer after approval
4. **Maintainer** reviews and approves
5. **PR Monitor confirms**: Successful merge via auto-merge (rebase)
6. **Maintainer**: Delete worktree + branch, close issue

**Automatic Invocation**:

PR Monitor is invoked after each task completion cycle:

```
Task Complete → Self Verified → Committed → Pushed → PR Opened
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

### Escalation Path

When agents disagree or need human input:

1. **Agent identifies issue**: States clearly what needs decision
2. **Provides options**: Shows 2-3 approaches with trade-offs
3. **Recommends**: States agent's preference with rationale
4. **Human decides**: You make the call
5. **Agent implements**: Agent executes based on your decision

---

**Version**: 1.1
**Last Updated**: 2026-02-02
**Maintained By**: GDD Designer persona, PR Monitor persona
