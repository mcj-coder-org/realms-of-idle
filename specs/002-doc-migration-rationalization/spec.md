# Feature Specification: Documentation Migration & Rationalization

**Feature Branch**: `002-doc-migration-rationalization`
**Created**: 2026-02-10
**Status**: Draft - Awaiting Approval
**Input**: Migrate and rationalize 679 design documents from previous project (cozy-fantasy-rpg) to current project (realms-of-idle), resolving conflicts with existing GDD

---

## Context & Background

### Current State

**Existing Documentation** (realms-of-idle):

- **Authoritative GDDs**: 24 files in `docs/design/systems/` - defines current game mechanics
- **Migrated Content**: 626 files in `docs/design/content/` - examples from previous project
- **Known Conflicts**: 8 documented inconsistencies in `content-gdd-inconsistencies.md`

**Source Documentation** (cozy-fantasy-rpg):

- **Total**: 679 markdown files in `docs/design/`
- **Systems**: 60 files defining game mechanics
- **Content**: 619 files (classes, skills, items, creatures, races, factions, etc.)

### Problem Statement

The current project is the **second attempt** at implementing this game after a failed first implementation. Documentation was partially migrated (626 content files) but:

1. **Terminology conflicts**: "Tier" means 3 different things across documents
2. **Missing specifications**: Content references systems not yet defined in GDD
3. **Outdated mechanics**: Source docs may contain mechanics that don't align with current vision
4. **Incomplete coverage**: Only content was migrated, not all system specs

### Why This Matters

**Blocking MVP Implementation**: The minimal possession demo (001) cannot be implemented until core terminology and mechanics are clarified. Developers would be working from conflicting specifications.

**Documentation Debt**: As implementation progresses, the gap between GDD and content examples will widen, making future integration harder.

---

## User Scenarios & Testing _(mandatory)_

### User Story 1 - Terminology Resolution (Priority: P1)

Establish single source of truth for all terminology used across documentation.

**Why this priority**: CRITICAL BLOCKER - Implementation cannot proceed with conflicting definitions of core terms like "tier", "class progression", "skill advancement".

**Independent Test**: Read class-system-gdd.md, search for "tier" → find single, unambiguous definition. Read any content file, search for "tier" → uses same definition consistently.

**Acceptance Scenarios**:

1. **Given** I read `class-system-gdd.md`, **When** I search for "tier", **Then** I find exactly ONE definition: "Class Rank (Apprentice/Journeyman/Master) - mastery progression within a single class"
2. **Given** I read `class-system-gdd.md`, **When** I search for "class tree tier", **Then** I find exactly ONE definition: "Position in class hierarchy (1=Foundation, 2=Specialization, 3=Advanced)"
3. **Given** I read `skill-recipe-system-gdd.md`, **When** I search for "skill tier", **Then** I find exactly ONE definition: "Skill upgrade level (Lesser/Greater/Enhanced) - progression within a single skill"
4. **Given** I read ANY content file in `docs/design/content/`, **When** I encounter the word "tier", **Then** it uses the correct term from GDD (either "class rank", "class tree tier", or "skill tier")

---

### User Story 2 - GDD Completeness Audit (Priority: P1)

Identify all mechanics referenced in content that lack GDD specification.

**Why this priority**: CRITICAL - Content examples reference game systems that aren't formally specified, creating implementation ambiguity.

**Independent Test**: Generate report listing all system references in content → cross-reference against GDD index → output "Missing GDD Specifications" list with priority ranking.

**Acceptance Scenarios**:

1. **Given** content references "tag depth access rules", **When** I check `class-system-gdd.md`, **Then** I find formal specification with examples
2. **Given** content references "Lesser/Greater/Enhanced skill tiers", **When** I check `skill-recipe-system-gdd.md`, **Then** I find complete progression mechanics
3. **Given** content references "synergy bonus formulas", **When** I check `skill-recipe-system-gdd.md`, **Then** I find mathematical formulas and scaling tables
4. **Given** I generate completeness report, **When** I review output, **Then** I see zero "Missing GDD Specifications" entries

---

### User Story 3 - Source Documentation Audit (Priority: P1)

Evaluate all 679 source files for relevance to current project vision.

**Why this priority**: HIGH - Not all mechanics from failed first attempt are relevant to second attempt. Need to identify what to keep vs discard.

**Independent Test**: Read each source system file → categorize as "Keep (aligns with current GDD)", "Adapt (needs modification)", "Discard (not in scope)" → generate audit report with rationale.

**Acceptance Scenarios**:

1. **Given** I audit `/cozy-fantasy-rpg/docs/design/systems/character/class-progression.md`, **When** I compare to current GDD, **Then** I categorize as "Keep" or "Adapt" with specific changes needed
2. **Given** I audit `/cozy-fantasy-rpg/docs/design/systems/social/tribe-mechanics.md`, **When** I check current project scope, **Then** I determine if tribal factions are in scope for this version
3. **Given** I complete audit, **When** I generate report, **Then** I have clear categorization (Keep/Adapt/Discard) for all 60 system files and 619 content files
4. **Given** I present audit to stakeholder, **When** they review categorizations, **Then** they can approve/reject decisions before migration proceeds

---

### User Story 4 - Content Template Creation (Priority: P2)

Create standardized templates for all content types using correct GDD terminology.

**Why this priority**: HIGH - Templates ensure consistency as new content is created and prevent re-introduction of old terminology.

**Independent Test**: Use class template to create new class → all terminology matches GDD. Use skill template to create new skill → format and fields are correct.

**Acceptance Scenarios**:

1. **Given** I use `docs/design/content/_templates/class-template.md`, **When** I fill in placeholders, **Then** frontmatter uses `tree_tier:` not `tier:`, and progression language says "unlocks eligibility for" not "transitions to"
2. **Given** I use `docs/design/content/_templates/skill-template.md`, **When** I create tiered skill, **Then** template includes Lesser/Greater/Enhanced sections with XP thresholds
3. **Given** I use `docs/design/content/_templates/system-gdd-template.md`, **When** I create new system spec, **Then** template includes: authoritative frontmatter, design philosophy, mechanics, examples, open questions
4. **Given** templates exist, **When** I update existing content, **Then** I can validate against template structure

---

### User Story 5 - Systematic Content Updates (Priority: P2)

Apply terminology and structural changes to all 626 migrated content files.

**Why this priority**: MEDIUM - Required for consistency but can be automated/batched after templates are finalized.

**Independent Test**: Run content update script → all files updated → validation passes with zero errors → spot-check 10 random files for correctness.

**Acceptance Scenarios**:

1. **Given** I run content update script, **When** it processes class files, **Then** all `tier: N` frontmatter becomes `tree_tier: N`
2. **Given** I run content update script, **When** it processes progression sections, **Then** all "transition to Tier 2" text becomes "unlocks eligibility for [Class Name]"
3. **Given** I run content update script, **When** it processes skill files, **Then** all "tier: tiered" files have Lesser/Greater/Enhanced sections
4. **Given** content updates complete, **When** I run validation, **Then** zero terminology conflicts detected

---

### User Story 6 - GDD Expansion (Priority: P2)

Add missing formal specifications to GDD based on audit findings.

**Why this priority**: MEDIUM - Fills gaps identified in Story 2, making GDD complete enough for MVP implementation.

**Independent Test**: Read updated GDD → all systems referenced in MVP feature spec have formal specification → implementation team has no ambiguity.

**Acceptance Scenarios**:

1. **Given** audit identified "tag depth access" as missing, **When** I read updated `class-system-gdd.md`, **Then** I find new section "Tag Depth Access by Class Tree Tier" with formal rules
2. **Given** audit identified "skill tier progression" as missing, **When** I read updated `skill-recipe-system-gdd.md`, **Then** I find complete Lesser/Greater/Enhanced mechanics with XP formulas
3. **Given** audit identified "synergy bonus scaling" as missing, **When** I read updated `skill-recipe-system-gdd.md`, **Then** I find standard progression formulas
4. **Given** I read all GDD updates, **When** I cross-reference with MVP spec (001), **Then** all required mechanics are formally specified

---

### User Story 7 - Migration Report & Validation (Priority: P3)

Document what was migrated, what changed, and what was discarded with full traceability.

**Why this priority**: LOW - Important for audit trail and future reference, but doesn't block implementation.

**Independent Test**: Read migration report → understand all decisions → verify sample of migrations matches stated approach → confirm discarded content documented.

**Acceptance Scenarios**:

1. **Given** I read `docs/design/MIGRATION-REPORT.md`, **When** I review statistics, **Then** I see: total files processed, kept count, adapted count, discarded count, rationale for discards
2. **Given** I read migration report, **When** I review terminology changes, **Then** I see before/after examples for "tier", "transition to", and other updated terms
3. **Given** I read migration report, **When** I check GDD additions, **Then** I see list of new sections added with page references
4. **Given** migration is complete, **When** I validate, **Then** I can trace any content file back to source and understand what changed

---

### Edge Cases

- **What happens when source file directly conflicts with GDD**? → Prioritize GDD (it's authoritative for v2), note conflict in migration report, mark content as "Adapted" with changes documented
- **What happens when source content references mechanics not in current scope**? → Mark as "Deferred" in migration report, create issue tracker entry for future consideration
- **What happens when multiple source files cover same content**? → Consolidate into single canonical document, note merger in migration report
- **What happens when GDD lacks sufficient detail for content examples**? → Flag as "GDD Expansion Required" in audit, block content migration until GDD updated
- **What happens if stakeholder rejects audit categorizations**? → Re-categorize per feedback, update audit report, re-run affected migrations

---

## Functional Requirements

### FR-001: Terminology Standardization

**Must Have**:

- Single authoritative definition for each term in GDD
- Glossary mapping old terms → new terms
- Automated validation to detect terminology conflicts

**Terminology Decisions** (from content-gdd-inconsistencies.md):

```
OLD (conflicting):
- "tier" (means 3 different things)
- "transition to"
- Tier 1/2/3 classes

NEW (unambiguous):
- "class rank" (Apprentice/Journeyman/Master)
- "class tree tier" (1=Foundation, 2=Specialization, 3=Advanced)
- "skill tier" (Lesser/Greater/Enhanced)
- "unlocks eligibility for" (multi-class model)
```

### FR-002: GDD Completeness

**Must Have**:

- All mechanics referenced in MVP (001) formally specified in GDD
- Cross-reference validation (content → GDD)
- Clear "authoritative" vs "example" distinction

**Required GDD Additions**:

1. **class-system-gdd.md**: Tag depth access rules, class tree tier mechanics
2. **skill-recipe-system-gdd.md**: Lesser/Greater/Enhanced progression, synergy formulas
3. **core-progression-system-gdd.md**: Threshold formulas by class tree tier

### FR-003: Source Content Audit

**Must Have**:

- Categorization of all 679 source files (Keep/Adapt/Discard)
- Rationale for each decision
- Stakeholder approval before migration proceeds

**Audit Dimensions**:

- **Relevance**: Does it align with v2 game vision?
- **Quality**: Is it well-specified or needs work?
- **Scope**: Is it needed for MVP or future feature?
- **Conflicts**: Does it contradict current GDD?

### FR-004: Content Templates

**Must Have**:

- Template for each content type (class, skill, item, creature, system)
- Correct terminology and structure per GDD
- Validation rules for template compliance

**Template Types**:

- `class-template.md` (with tree_tier, rank progression, multi-class context)
- `skill-template.md` (with skill tiers, synergy classes, XP progression)
- `item-template.md`, `creature-template.md`, `system-gdd-template.md`

### FR-005: Automated Content Updates

**Must Have**:

- Scripts to apply terminology changes to migrated content
- Validation to confirm changes applied correctly
- Backup of original files before modification

**Update Operations**:

- Frontmatter: `tier: N` → `tree_tier: N`
- Body text: "transition to" → "unlocks eligibility for"
- Structure: Add missing sections per templates

### FR-006: GDD Expansion

**Must Have**:

- New sections in GDD for previously undefined mechanics
- Cross-references between GDD sections
- Examples tying GDD to content

**Expansion Targets**:

- Formal specifications for all mechanics used in MVP
- Formulas and algorithms where applicable
- Decision rationale (design philosophy)

### FR-007: Migration Traceability

**Must Have**:

- Migration report documenting all decisions
- Mapping source files → destination files
- Change log for adapted content

**Report Sections**:

- Executive summary (statistics)
- Terminology changes (glossary)
- GDD additions (section list)
- Content migrations (file mapping)
- Discarded content (with rationale)

---

## Technical Requirements

### TR-001: File Organization

```
docs/design/
├── systems/                    # Authoritative GDD (24 files)
│   ├── class-system-gdd.md
│   ├── skill-recipe-system-gdd.md
│   └── ...
├── content/                    # Content examples (626 migrated + new)
│   ├── _templates/            # NEW: Content templates
│   ├── classes/
│   ├── skills/
│   └── ...
├── reference/                  # NEW: Migration artifacts
│   ├── terminology-glossary.md
│   ├── source-audit.md
│   └── migration-report.md
└── content-gdd-inconsistencies.md  # EXISTING: Analysis document
```

### TR-002: Validation Rules

**Terminology Validation**:

- Detect usage of deprecated terms (tier without qualifier)
- Flag incorrect multi-class language ("transition to")
- Verify frontmatter schema compliance

**Cross-Reference Validation**:

- Detect dangling links (reference to non-existent file)
- Detect undefined mechanics (content refers to missing GDD section)
- Verify GDD status frontmatter (authoritative vs draft)

### TR-003: Automation Scripts

**Required Scripts**:

1. `audit-source-docs.sh`: Categorize source files based on criteria
2. `update-terminology.sh`: Apply terminology changes to content
3. `validate-content.sh`: Check compliance with templates and GDD
4. `generate-migration-report.sh`: Produce final traceability document

---

## Success Metrics

### Quantitative

1. **Zero terminology conflicts** detected by validation after migration
2. **100% of MVP mechanics** have formal GDD specification
3. **All 626 existing content files** updated to use new terminology
4. **All discarded content** documented with rationale

### Qualitative

1. **Implementation team confidence**: Developers can implement MVP without asking "what does this mean?"
2. **Maintainability**: New content can be created using templates without re-introducing conflicts
3. **Audit trail**: Any decision can be traced back to source and rationale

---

## Out of Scope

### Not Included in This Feature

1. **Content expansion**: Only migrating/updating existing content, not creating new content
2. **GDD completeness beyond MVP**: Only adding specifications needed for 001-minimal-possession-demo
3. **Implementation**: This is documentation work only, no code changes
4. **Visual design**: Not addressing UI/UX documentation, only game mechanics
5. **Source project refactoring**: Not fixing issues in source docs, only migrating/adapting

---

## Dependencies

### Blocking Dependencies (Must Complete First)

1. **Stakeholder approval of terminology changes**: Cannot proceed until agreed on class rank/tree tier/skill tier
2. **MVP feature spec finalization (001)**: Need to know what mechanics are actually required

### Non-Blocking Dependencies

1. **Source project access**: `/home/mcjarvis/projects/cozy-fantasy-rpg/docs/design` (already available)
2. **Existing GDD**: `docs/design/systems/*.md` (24 files, already exists)
3. **Inconsistency analysis**: `content-gdd-inconsistencies.md` (already created)

---

## Risks & Mitigations

### Risk 1: Stakeholder Rejects Terminology Changes

**Impact**: HIGH - Would require re-planning entire migration approach

**Mitigation**:

- Present clear rationale for changes (based on inconsistency analysis)
- Show examples of confusion caused by current terminology
- Offer alternative solutions if primary approach rejected

### Risk 2: GDD Expansion Reveals Design Conflicts

**Impact**: MEDIUM - May require revisiting game mechanics, delaying MVP

**Mitigation**:

- Identify conflicts early through audit process
- Flag conflicts for design decision before implementation
- Document trade-offs and alternatives in GDD

### Risk 3: Source Content Quality Lower Than Expected

**Impact**: MEDIUM - May need to rewrite rather than adapt content

**Mitigation**:

- Categorize by quality during audit
- Mark low-quality content for "Rewrite" vs "Adapt"
- Prioritize high-quality content for early migration

### Risk 4: Scope Creep (Trying to Migrate Everything)

**Impact**: MEDIUM - Could delay MVP implementation indefinitely

**Mitigation**:

- Strict focus on mechanics needed for MVP only
- Mark non-MVP content as "Deferred" with issues created
- Time-box each phase with hard deadlines

---

## Timeline Estimate

### Phase 0: Audit & Planning (2-3 days)

- Source documentation audit
- GDD gap analysis
- Stakeholder approval of approach

### Phase 1: GDD Expansion (2-3 days)

- Add missing specifications to GDD
- Create formal definitions for all MVP mechanics
- Cross-reference validation

### Phase 2: Template Creation (1 day)

- Create content templates with correct terminology
- Document validation rules
- Create example content using templates

### Phase 3: Automated Updates (1-2 days)

- Create update scripts
- Test on sample files
- Apply to all migrated content
- Validation pass

### Phase 4: Migration & Documentation (1 day)

- Generate migration report
- Create terminology glossary
- Document audit decisions

**Total Estimate**: 7-10 days

---

## Acceptance Criteria Summary

This feature is complete when:

1. ✅ Single authoritative definition exists for all terminology in GDD
2. ✅ All mechanics referenced in MVP (001) have formal GDD specification
3. ✅ All 626 migrated content files use correct terminology
4. ✅ Content templates exist for all content types
5. ✅ Validation passes with zero terminology conflicts
6. ✅ Migration report documents all decisions and changes
7. ✅ Implementation team can proceed with MVP without documentation ambiguity

---

## Appendices

### Appendix A: Terminology Glossary (Draft)

| Old Term                | New Term                | Context                                    |
| ----------------------- | ----------------------- | ------------------------------------------ |
| Tier (in class context) | Class Rank              | Apprentice/Journeyman/Master progression   |
| Tier (in class tree)    | Class Tree Tier         | 1=Foundation, 2=Specialization, 3=Advanced |
| Tier (in skills)        | Skill Tier              | Lesser/Greater/Enhanced progression        |
| Transition to           | Unlocks eligibility for | Multi-class system, not replacement        |

### Appendix B: Known Inconsistencies

See `docs/design/content-gdd-inconsistencies.md` for complete analysis of 8 identified conflicts.

### Appendix C: Source Documentation Structure

```
/cozy-fantasy-rpg/docs/design/
├── systems/ (60 files)
│   ├── character/ (10 files)
│   ├── combat/ (5 files)
│   ├── crafting/ (5 files)
│   ├── economy/ (5 files)
│   ├── social/ (9 files)
│   └── world/ (6 files)
└── content/ (619 files)
    ├── classes/
    ├── skills/
    ├── items/
    ├── creatures/
    ├── races/
    └── factions/
```

---

_This specification defines the documentation migration and rationalization work required before MVP implementation can proceed with confidence._
