# Implementation Plan: Documentation Migration & Rationalization

**Branch**: `002-doc-migration-rationalization` | **Date**: 2026-02-10 | **Spec**: [spec.md](spec.md)
**Input**: Feature specification from `/specs/002-doc-migration-rationalization/spec.md`

## Summary

Migrate and rationalize 679 design documents from previous project (cozy-fantasy-rpg) to current project (realms-of-idle), resolving 8 critical terminology conflicts and establishing progressive-loading documentation architecture. This is a **documentation-only** feature with no code implementation - focused on creating coherent, cross-referenced GDD and content examples that enable confident MVP implementation.

**Key Deliverables**:

1. Corrected authoritative GDD (fix multi-class mechanics, add missing specs)
2. Restructured folder architecture (`<name>/index.md` pattern with co-located examples)
3. Comprehensive cross-reference system (GDD ↔ Content mappings)
4. Minimal frontmatter with `gdd_ref` links
5. Migration audit trail and terminology glossary

## Technical Context

**Language/Version**: Markdown (CommonMark spec)
**Primary Dependencies**:

- markdownlint-cli2 (linting)
- prettier (formatting)
- bash scripts (automation)

**Storage**: File-based documentation in `docs/design/`
**Testing**:

- Validation scripts (terminology consistency)
- Cross-reference validation (no dangling links)
- Frontmatter schema validation

**Target Platform**: Documentation consumed by:

- Human developers (via GitHub/IDE)
- AI agents (progressive loading via index pages)
- Future doc generation tools

**Project Type**: Documentation maintenance (not code)

**Performance Goals**:

- Agent can load relevant context in <3 file reads (via index navigation)
- <5% broken links after migration
- 100% terminology consistency (zero conflicts)

**Constraints**:

- Must not break existing MVP (001) implementation work
- Must complete before MVP implementation begins
- Zero tolerance for terminology conflicts

**Scale/Scope**:

- Source: 679 markdown files (60 system specs, 619 content examples)
- Target: ~700 files after restructuring (adding index pages, examples/ folders)
- Updates: ~650 files need frontmatter/terminology changes

## Constitution Check

_GATE: Must pass before Phase 0 research. Re-check after Phase 1 design._

Verify compliance with [Constitution v1.0.0](../../.specify/memory/constitution.md):

- [x] **Test-First Development**: Feature spec includes testable acceptance criteria (validation scripts = tests for docs)
- [x] **Quality Standards**: Pre-commit hooks enforce markdown linting, all quality gates applicable
- [x] **Git Discipline**: Feature branch documented (`002-doc-migration-rationalization`), conventional commits planned
- [x] **Simplicity First**: Minimal frontmatter, progressive loading via indexes (simpler than metadata-heavy approach)
- [x] **Automation**: Validation scripts, terminology update scripts, cross-reference generation - all automated

**Complexity Justification**: None required - documentation maintenance with automation is standard practice.

## Project Structure

### Documentation (this feature)

```text
specs/002-doc-migration-rationalization/
├── spec.md                 # Feature specification (COMPLETE)
├── plan.md                 # This file (IN PROGRESS)
├── research.md             # Phase 0 output (PENDING)
├── data-model.md           # N/A (doc feature, no data model)
├── contracts/              # N/A (doc feature, no API contracts)
├── quickstart.md           # Phase 1: Validation guide
└── tasks.md                # Phase 2 output (PENDING)
```

### Target Documentation Structure

```text
docs/design/
├── index.md                           # Root navigation hub
│
├── systems/                           # Authoritative GDD
│   ├── index.md                      # System overview + navigation
│   ├── class-system/
│   │   ├── index.md                  # Main spec (CORRECTED: no slots, primary class)
│   │   ├── examples/
│   │   │   ├── multi-class-stacking.md
│   │   │   └── primary-class-selection.md
│   │   └── diagrams/
│   │       └── class-tree.svg
│   ├── skill-system/
│   │   ├── index.md                  # Main spec (CORRECTED: quick slots clarified)
│   │   ├── examples/
│   │   │   ├── tiered-progression.md
│   │   │   └── quick-slots-usage.md
│   │   └── tables/
│   │       └── skill-xp-thresholds.md
│   ├── progression-system/
│   │   ├── index.md                  # Main spec (CORRECTED: XP distribution)
│   │   ├── examples/
│   │   │   └── multi-class-xp-flow.md
│   │   └── formulas/
│   │       └── threshold-calculations.md
│   └── [other systems.../]
│
├── content/                           # Content examples
│   ├── index.md                      # Content catalog navigation
│   ├── classes/
│   │   ├── index.md                  # Class index (by tier, by role)
│   │   ├── fighter/
│   │   │   ├── index.md              # Fighter spec
│   │   │   ├── warrior/
│   │   │   │   ├── index.md          # Warrior spec
│   │   │   │   ├── knight/
│   │   │   │   │   └── index.md      # Knight spec
│   │   │   │   └── examples/
│   │   │   │       └── typical-progression.md
│   │   │   └── examples/
│   │   │       └── early-game-fighter.md
│   │   └── [other classes.../]
│   ├── skills/
│   │   ├── index.md                  # Skill catalog
│   │   ├── common/
│   │   │   └── power-strike/
│   │   │       ├── index.md
│   │   │       └── examples/
│   │   └── tiered/
│   │       └── weapon-mastery/
│   │           ├── index.md          # Overview + progression
│   │           ├── lesser.md         # Lesser tier details
│   │           ├── greater.md        # Greater tier details
│   │           ├── enhanced.md       # Enhanced tier details
│   │           └── examples/
│   │               └── warrior-progression.md
│   └── [items, creatures, etc.../]
│
└── reference/                         # Migration artifacts & tools
    ├── index.md                      # Reference navigation
    ├── terminology/
    │   └── index.md                  # Glossary (class rank, tree tier, skill tier)
    ├── cross-reference/
    │   ├── index.md                  # XRef documentation
    │   ├── gdd-to-content.md         # GDD → Content mappings
    │   └── content-to-gdd.md         # Content → GDD reverse lookup
    └── migration/
        ├── index.md                  # Migration documentation
        ├── audit-report.md           # Keep/Adapt/Discard decisions
        └── source-mapping.md         # Old path → New path traceability
```

### Validation Scripts

```text
.specify/scripts/
├── validate-terminology.sh           # Check term consistency
├── validate-cross-references.sh      # Detect dangling links
├── validate-frontmatter.sh           # Schema compliance
└── generate-xref-report.sh           # Build cross-reference maps
```

## Phase 0: Research & Analysis

### Research Tasks

This feature requires research in the following areas:

#### R1: GDD Correction Requirements

**Question**: What are the complete scope of corrections needed to align GDD with actual game mechanics (per user clarifications)?

**Method**:

- Read current class-system-gdd.md § 3 (Multi-Class Slot Management)
- Read current skill-recipe-system-gdd.md § 2 (Skill Slot System)
- Read current core-progression-system-gdd.md § 5 (Multi-Classing Mechanics)
- Document all sections that reference "dormant classes", "XP splitting", "3 active slots"
- Map to correct mechanics: all classes active, primary class selection, simultaneous XP gain

**Output**: List of sections to delete/rewrite with line number ranges

#### R2: Source Documentation Quality Assessment

**Question**: What is the quality level and relevance of the 679 source files from cozy-fantasy-rpg?

**Method**:

- Sample 20 files across categories (systems, classes, skills, items, creatures)
- Assess: completeness, format consistency, terminology usage, GDD alignment
- Categorize quality: High (minimal work), Medium (adapt), Low (rewrite), Obsolete (discard)

**Output**: Quality distribution estimate (% High/Medium/Low/Obsolete)

#### R3: Progressive Loading Best Practices

**Question**: What are proven patterns for documentation that agents can efficiently navigate?

**Method**:

- Review successful large-scale documentation projects (e.g., Kubernetes docs, Rust docs)
- Identify patterns: index page structures, frontmatter minimalism, co-located examples
- Document anti-patterns to avoid: deep nesting without indexes, circular references

**Output**: Design principles for index pages and folder structure

#### R4: Cross-Reference System Design

**Question**: How should bidirectional GDD ↔ Content references be implemented?

**Method**:

- Evaluate options: frontmatter only, dedicated xref files, generated maps, inline links
- Consider tradeoffs: maintainability, agent discoverability, staleness risk
- Design validation approach: how to detect/prevent broken references

**Output**: Cross-reference architecture decision with validation strategy

### Expected Research Outcomes

**research.md** will document:

1. Complete list of GDD sections requiring correction (with before/after examples)
2. Source file quality assessment with categorization criteria
3. Progressive loading design principles for index pages
4. Cross-reference system architecture and validation approach

**Blocking**: Cannot proceed to Phase 1 until research confirms approach is sound.

## Phase 1: Design & Validation Planning

### Artifacts to Create

#### 1. quickstart.md - Validation Guide

**Purpose**: Document how to validate documentation changes

**Contents**:

- How to run terminology validation (`validate-terminology.sh`)
- How to check cross-references (`validate-cross-references.sh`)
- How to verify frontmatter schema (`validate-frontmatter.sh`)
- How to test agent navigation (load time < 3 files)
- Success criteria for each validation type

#### 2. Migration Scripts Design

**Purpose**: Define automation approach for bulk updates

**Scripts to design**:

- `update-frontmatter.sh`: Add `gdd_ref` links, convert `tier:` → `tree_tier:`
- `restructure-folders.sh`: Move files to `<name>/index.md` pattern
- `generate-indexes.sh`: Create index.md files for navigation
- `generate-xref-maps.sh`: Build GDD ↔ Content mappings

**Design includes**: Dry-run mode, backup strategy, rollback procedure

#### 3. Frontmatter Schema

**Purpose**: Define minimal required fields

```yaml
---
# REQUIRED
title: [Page Title]
gdd_ref: [Link to authoritative GDD section]

# CONDITIONAL (only if applicable)
parent: [Link to parent in tree]
tree_tier: [1/2/3 for classes only]
---
```

**Validation rules**:

- `title` must be non-empty
- `gdd_ref` must point to existing GDD file#section
- `parent` must point to existing file (if present)
- `tree_tier` must be 1, 2, or 3 (if present)

#### 4. Index Page Template

**Purpose**: Standard structure for navigation hubs

```markdown
# [Category] Index

Brief introduction paragraph.

## Navigation

### By [Primary Dimension]

- [Item](path/to/item/index.md) - Short description
- ...

### By [Secondary Dimension]

- [Item](path/to/item/index.md) - Short description
- ...

## Related

- [Related GDD](../systems/[system]/index.md) - Authoritative mechanics
- [Related Content](../content/[category]/index.md) - Examples
```

### Agent Context Update

After Phase 1 design, update CLAUDE.md with:

```markdown
## Documentation Structure (002-doc-migration-rationalization)

- **Progressive Loading**: Start at index.md, navigate via links (avoid deep paths)
- **Frontmatter**: Check `gdd_ref` for authoritative mechanics
- **GDD vs Content**: systems/ = authoritative, content/ = examples
- **Cross-Reference**: Use reference/cross-reference/ for GDD ↔ Content mappings
```

**Update Path**: Append to "Active Technologies" or "Recent Changes" section

## Phase 2: Implementation Phases

### Implementation Order (tasks.md will detail)

**Phase A: GDD Corrections** (3-4 days)

- Fix multi-class mechanics in class-system-gdd.md
- Fix quick slot mechanics in skill-recipe-system-gdd.md
- Fix XP distribution in core-progression-system-gdd.md
- Add missing specifications (tag depth, skill tiers, synergy formulas)

**Phase B: Structure Reorganization** (2-3 days)

- Restructure folders to `<name>/index.md` pattern
- Create co-located examples/ and tables/ folders
- Generate comprehensive index pages
- Update all internal links

**Phase C: Frontmatter & Cross-References** (2 days)

- Add `gdd_ref` to all content frontmatter
- Convert `tier:` → `tree_tier:` in classes
- Generate GDD ↔ Content mappings
- Build terminology glossary

**Phase D: Content Updates** (2-3 days)

- Update terminology in existing 626 migrated files
- Remove references to "dormant classes", "XP splitting"
- Add "Primary Class" examples where relevant
- Validate all changes

**Phase E: Source Migration** (2-3 days)

- Audit remaining 53 source files (679 - 626 migrated)
- Categorize: Keep/Adapt/Discard
- Migrate approved files with adaptations
- Document discarded files with rationale

**Phase F: Validation & Documentation** (1 day)

- Run all validation scripts
- Generate migration report
- Create quickstart guide for future contributors
- Final cross-reference validation

---

### Phase Dependencies

**Dependency Graph**:

```
Phase 1 (Design) ────────────────┬──→ Phase A (GDD Corrections)
                                 └──→ Phase B (Structure)

Phase A (GDD Corrections) ───────┬──→ Phase C (Frontmatter) [needs corrected gdd_ref targets]
                                 └──→ Phase D (Content Updates) [needs corrected terminology]

Phase B (Structure) ─────────────┬──→ Phase C (Frontmatter) [needs <name>/index.md paths]
                                 └──→ Phase D (Content Updates) [needs new file locations]

Phase C (Frontmatter) ───────────┬──→ Phase D (Content Updates) [needs validation scripts]
                                 └──→ Phase E (Source Migration) [needs templates working]

Phase D (Content Updates) ───────────→ Phase E (Source Migration) [needs validation passing]

Phases A+B+C+D ──────────────────────→ Phase E (Source Migration) [all prerequisites]

Phase E (Source Migration) ──────────→ Phase F (Validation) [final checks]
```

**Parallelization Opportunities**:

| Phases | Can Run in Parallel? | Rationale                                                                            |
| ------ | -------------------- | ------------------------------------------------------------------------------------ |
| A + B  | ✅ **Yes**           | Independent - GDD corrections don't affect folder restructuring                      |
| A + C  | ❌ **No**            | C needs corrected GDD files from A (gdd_ref targets must exist)                      |
| B + C  | ⚠️ **Partial**       | Can prepare frontmatter, but can't validate until B completes (paths change)         |
| C + D  | ❌ **No**            | D needs validation scripts from C (validate-frontmatter.sh, validate-terminology.sh) |
| D + E  | ❌ **No**            | E requires D validation passing (can't migrate if existing content broken)           |

**Critical Path** (longest dependency chain):

```
Phase 1 → Phase A → Phase C → Phase D → Phase E → Phase F
[1-2d]    [3-4d]    [2d]       [2-3d]    [2-3d]    [1d]
                              = 11-15 days minimum
```

**Optimization** (parallel A+B):

```
Phase 1 → [Phase A + Phase B in parallel] → Phase C → Phase D → Phase E → Phase F
[1-2d]    [max(3-4d, 2-3d) = 3-4d]          [2d]      [2-3d]    [2-3d]    [1d]
                              = 11-15 days (no time savings due to serial C→D→E)
```

**Blocking Relationships**:

- **Phase C blocked by**: Phase A (needs corrected GDD refs), Phase B (needs final file paths)
- **Phase D blocked by**: Phase A (needs corrected terminology), Phase B (needs file locations), Phase C (needs validation scripts)
- **Phase E blocked by**: All of A+B+C+D (requires validated, corrected, structured content)
- **Phase F blocked by**: Phase E (final migration must complete)

**Sequential Constraints**:

1. **Cannot start Phase C until**: Both Phase A AND Phase B complete
2. **Cannot start Phase D until**: All of Phase A, B, C complete
3. **Cannot start Phase E until**: All of Phase A, B, C, D complete
4. **Cannot start Phase F until**: Phase E completes

**Prerequisite Checklist**:

**Before Phase A**: Phase 1 design artifacts complete (quickstart.md, frontmatter-schema.md, index-template.md)

**Before Phase C**:

- ✅ Phase A: All GDD corrections committed (corrected refs exist)
- ✅ Phase B: All folders restructured (final paths known)

**Before Phase D**:

- ✅ Phase A: Terminology corrections defined
- ✅ Phase B: File locations finalized
- ✅ Phase C: Validation scripts created and tested

**Before Phase E**:

- ✅ Phase A: GDD is authoritative source of truth
- ✅ Phase B: Structure is stable (no more moves)
- ✅ Phase C: Cross-references working
- ✅ Phase D: Existing content validates successfully (zero errors)

**Before Phase F**:

- ✅ Phase E: All migration decisions documented, files processed

---

**Total Estimate**: 12-16 days (with A+B parallelization = 11-15 days)

## Constitution Re-Check (Post-Design)

After Phase 1 design artifacts complete, re-verify:

- [x] **Test-First**: Validation scripts designed before bulk changes (TDD for docs)
- [x] **Quality Standards**: All markdown will pass pre-commit hooks
- [x] **Git Discipline**: Atomic commits per phase (A/B/C/D/E/F)
- [x] **Simplicity**: Minimal frontmatter, simple index navigation (no complex metadata)
- [x] **Automation**: Scripts handle repetitive updates, not manual edits

**Result**: Constitution compliant - ready to proceed to tasks.md generation

## Risk Mitigation

### Risk 1: GDD Corrections Reveal Additional Conflicts

**Mitigation**: Research phase samples broadly across GDD to identify all multi-class references

**Contingency**: If new conflicts found during implementation, pause and expand correction scope

### Risk 2: Source File Quality Lower Than Estimated

**Mitigation**: Research phase samples files to estimate quality distribution

**Contingency**: If quality worse than expected, increase "Discard" rate and reduce migration scope

### Risk 3: Cross-Reference System Too Complex

**Mitigation**: Research phase evaluates maintainability vs discoverability tradeoffs

**Contingency**: If generated maps prove fragile, fall back to frontmatter-only approach

### Risk 4: Agent Navigation Not Measurably Improved

**Mitigation**: Design includes "3 file load" test - validate index effectiveness

**Contingency**: If indexes don't help, simplify structure further (fewer nested levels)

## Success Criteria

This implementation is successful when:

1. ✅ All GDD files use consistent terminology (class rank, tree tier, skill tier)
2. ✅ Zero terminology conflicts detected by validation scripts
3. ✅ All content has `gdd_ref` frontmatter pointing to authoritative GDD
4. ✅ All folders follow `<name>/index.md` pattern with co-located examples
5. ✅ Comprehensive index pages enable navigation in <3 file reads
6. ✅ Cross-reference maps document GDD ↔ Content relationships
7. ✅ Migration report provides complete audit trail
8. ✅ MVP (001) implementation can proceed without documentation ambiguity

## Next Steps

1. Generate `research.md` (Phase 0) - resolve unknowns
2. Generate `quickstart.md` (Phase 1) - validation guide
3. Generate `tasks.md` (Phase 2) - detailed implementation tasks
4. Begin implementation (after stakeholder approval)

---

_Plan complete. Ready for research phase._
