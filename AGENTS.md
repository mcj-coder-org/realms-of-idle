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

### Escalation Path

When agents disagree or need human input:

1. **Agent identifies issue**: States clearly what needs decision
2. **Provides options**: Shows 2-3 approaches with trade-offs
3. **Recommends**: States agent's preference with rationale
4. **Human decides**: You make the call
5. **Agent implements**: Agent executes based on your decision

---

**Version**: 1.0
**Last Updated**: 2026-02-01
**Maintained By**: GDD Designer persona
