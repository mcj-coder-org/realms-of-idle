# Game Design Documentation Index

**Status**: WIP - Document structure audit in progress
**Maintained By**: GDD Designer
**Last Updated**: 2026-02-01

---

## Overview

This index tracks all game design documentation for Realms of Idle, including compliance status with documentation standards and improvement needs.

### Document Statistics

- **Total Documents**: 23
- **With Frontmatter**: 0 (23 need frontmatter added)
- **Compliant with Standards**: 0 (23 need review)
- **Need Splitting**: 3 (npc-design, twi-classes, idle-game-overview)
- **Need Consolidation**: 0

---

## Document Map

### High-Level Overview (1 document)

| Document                                       | Type   | Scope      | Status   | Frontmatter | Compliance       | Notes                     |
| ---------------------------------------------- | ------ | ---------- | -------- | ----------- | ---------------- | ------------------------- |
| [idle-game-overview.md](idle-game-overview.md) | system | high-level | approved | ❌ Missing  | ❌ Review needed | Too long, should be split |

**Issues**:

- ❌ No frontmatter
- ⚠️ 270+ lines - consider splitting into overview + separate docs for monetization, roadmap, etc.
- ✅ Good cross-reference section at end

### Scenario Documents (7 documents)

| Document                                             | Type     | Scope    | Status | Frontmatter | Compliance       | Notes                   |
| ---------------------------------------------------- | -------- | -------- | ------ | ----------- | ---------------- | ----------------------- |
| [idle-inn-tavern.md](idle-inn-tavern.md)             | scenario | detailed | draft  | ❌ Missing  | ❌ Review needed | Well-structured content |
| [idle-adventurer-guild.md](idle-adventurer-guild.md) | scenario | detailed | draft  | ❌ Missing  | ❌ Review needed |                         |
| [idle-monster-farm.md](idle-monster-farm.md)         | scenario | detailed | draft  | ❌ Missing  | ❌ Review needed |                         |
| [idle-alchemy.md](idle-alchemy.md)                   | scenario | detailed | draft  | ❌ Missing  | ❌ Review needed |                         |
| [idle-territory.md](idle-territory.md)               | scenario | detailed | draft  | ❌ Missing  | ❌ Review needed |                         |
| [idle-summoner.md](idle-summoner.md)                 | scenario | detailed | draft  | ❌ Missing  | ❌ Review needed |                         |
| [idle-merchant-caravan.md](idle-merchant-caravan.md) | scenario | detailed | draft  | ❌ Missing  | ❌ Review needed |                         |

**Common Issues**:

- ❌ No frontmatter on any scenario docs
- ✅ Content appears well-structured and focused
- ⚠️ Cross-linking between scenarios needs verification

### System Documents (8 documents)

| Document                                                               | Type   | Scope    | Status   | Frontmatter | Compliance       | Notes                      |
| ---------------------------------------------------------------------- | ------ | -------- | -------- | ----------- | ---------------- | -------------------------- |
| [npc-design.md](npc-design.md)                                         | system | detailed | approved | ❌ Missing  | ❌ Review needed | **Too long (1200+ lines)** |
| [idle-shared-progression.md](idle-shared-progression.md)               | system | detailed | draft    | ❌ Missing  | ❌ Review needed |                            |
| [idle-prestige-framework.md](idle-prestige-framework.md)               | system | detailed | draft    | ❌ Missing  | ❌ Review needed |                            |
| [idle-meta-integration.md](idle-meta-integration.md)                   | system | detailed | draft    | ❌ Missing  | ❌ Review needed |                            |
| [twi-classes.md](twi-classes.md)                                       | system | detailed | draft    | ❌ Missing  | ❌ Review needed | **Needs split**            |
| [twi-skills.md](twi-skills.md)                                         | system | detailed | draft    | ❌ Missing  | ❌ Review needed |                            |
| [twi-races-cultures.md](twi-races-cultures.md)                         | system | detailed | draft    | ❌ Missing  | ❌ Review needed |                            |
| [blacksmith-conversation-design.md](blacksmith-conversation-design.md) | system | detailed | draft    | ❌ Missing  | ❌ Review needed |                            |

**Issues**:

- ❌ No frontmatter on any system docs
- ⚠️ **npc-design.md** should be split into multiple focused docs:
  - npc-architecture.md (data structures, archetypes)
  - npc-goal-system.md (goal hierarchy, types, generation)
  - npc-behavior.md (daily loops, decision making)
  - npc-world-effects.md (events, health system, narrative)
- ⚠️ **twi-classes.md** may need splitting (classes, evolutions, skills)
- ✅ Content quality is high

### Technical Documents (2 documents)

| Document                                   | Type      | Scope      | Status   | Frontmatter | Compliance       | Notes |
| ------------------------------------------ | --------- | ---------- | -------- | ----------- | ---------------- | ----- |
| [tech-stack.md](tech-stack.md)             | technical | high-level | approved | ❌ Missing  | ❌ Review needed |       |
| [testing-strategy.md](testing-strategy.md) | technical | detailed   | draft    | ❌ Missing  | ❌ Review needed |       |

**Issues**:

- ❌ No frontmatter
- ✅ Appropriate scope and length

### Analysis & Planning (2 documents)

| Document                                 | Type      | Scope    | Status | Frontmatter | Compliance       | Notes |
| ---------------------------------------- | --------- | -------- | ------ | ----------- | ---------------- | ----- |
| [gap-analysis.md](gap-analysis.md)       | reference | detailed | draft  | ❌ Missing  | ❌ Review needed |       |
| [minimal-actions.md](minimal-actions.md) | reference | detailed | draft  | ❌ Missing  | ❌ Review needed |       |

**Issues**:

- ❌ No frontmatter
- ✅ Reference material appropriately structured

### Questions & Issues (2 documents)

| Document                                       | Type      | Scope      | Status | Frontmatter | Compliance       | Notes |
| ---------------------------------------------- | --------- | ---------- | ------ | ----------- | ---------------- | ----- |
| [open-questions.md](open-questions.md)         | reference | high-level | active | ❌ Missing  | ❌ Review needed |       |
| [open-questions-log.md](open-questions-log.md) | reference | high-level | active | ❌ Missing  | ❌ Review needed |       |

**Issues**:

- ❌ No frontmatter
- ✅ Active working documents, appropriate structure

---

## Action Items

### Priority 1: Add Frontmatter (23 documents)

All documents need complete frontmatter added. Template:

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

### Priority 2: Split Overly Long Documents

1. **npc-design.md** (1200+ lines) → Split into 4 docs:
   - `npc-architecture.md` - Core data model, archetypes (95/5 rule)
   - `npc-goal-system.md` - Goals, daily loops, decision making
   - `npc-world-effects.md` - Events, world health, emergent narrative
   - `npc-player-interaction.md` - Relationships, influence, reactions

2. **idle-game-overview.md** (270+ lines) → Split into:
   - `idle-game-overview.md` - Core pillars, scenarios, progression (keep)
   - `idle-roadmap.md` - Content roadmap, launch plans (extract)
   - `idle-monetization.md` - Monetization philosophy (extract)
   - `idle-success-metrics.md` - Metrics, KPIs, competitive analysis (extract)

3. **twi-classes.md** → Consider splitting:
   - Review length and content overlap
   - May need to separate classes from evolutions

### Priority 3: Add Cross-References

1. Add "Cross-References" sections to all documents
2. Ensure bidirectional linking (if A references B, B should reference A)
3. Use consistent link patterns: `[[Document Name]]`

### Priority 4: Verify Progressive Disclosure

1. Each document should start with elevator pitch
2. High-level overview before details
3. Extract detailed examples to separate docs where appropriate

### Priority 5: Update Status Field

Review and update document status:

- **draft**: Work in progress
- **review**: Ready for review
- **approved**: Finalized, ready for implementation
- **deprecated**: Replaced or obsolete

---

## Dependency Graph

### Core Dependencies

```
idle-game-overview.md (system, high-level)
├── idle-{scenario}.md (7 scenario docs)
│   ├── idle-shared-progression.md (system)
│   ├── idle-prestige-framework.md (system)
│   ├── idle-meta-integration.md (system)
│   └── npc-design.md (system) → TO BE SPLIT
├── twi-classes.md (system)
├── twi-skills.md (system)
├── twi-races-cultures.md (system)
├── tech-stack.md (technical)
└── testing-strategy.md (technical)
```

### Missing Documents

Consider creating:

- `glossary.md` - Game terms and definitions
- `economy-design.md` - Economic systems, resource flows
- `combat-design.md` - Combat mechanics (if applicable)
- `ui-ux-design.md` - Interface design principles
- `data-models.md` - Core data structures

---

## Documentation Standards Reference

See [AGENTS.md](../AGENTS.md) for GDD Designer persona and standards.

### Frontmatter Schema

All documents MUST include:

- **type**: scenario, system, mechanic, technical, narrative, reference
- **scope**: high-level, detailed, implementation
- **status**: draft, review, approved, deprecated
- **version**: Semantic version
- **created/updated**: ISO dates
- **subjects**: Tags for discovery
- **dependencies**: Related documents with relationship types

### Document Structure

1. **Elevator Pitch** (2-3 sentences)
2. **Core Fantasy/Purpose**
3. **Key Concepts**
4. **Detailed Content** (organized with clear headings)
5. **Cross-References**
6. **Open Questions**
7. **Version History**

---

**Next Review**: After frontmatter addition complete
**Maintainer**: GDD Designer persona
