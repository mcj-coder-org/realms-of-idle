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
- Rationale for each decision using measurable criteria below
- Stakeholder approval before migration proceeds

**Categorization Decision Matrix**:

Each file is evaluated on 4 dimensions, then categorized based on the scoring:

#### 1. Relevance (Alignment with v2 Game Vision)

**PASS** - File references mechanics in current GDD or future roadmap:

- References systems in `docs/design/systems/`
- Uses current terminology (class rank, class tree tier, skill tier)
- References future roadmap features (tribes, guilds, crafting, combat classes)
- No deprecated concepts (dormant classes, XP split, active slots)

**FAIL** - File references deprecated mechanics only:

- References "dormant classes", "active class slots", "XP splitting" without correction path
- Contradicts current GDD mechanics (multi-class slots model, player-configured XP split)
- Uses deprecated terminology exclusively with no salvageable content

**Note**: Files referencing future roadmap features (tribes, guilds, crafting, combat) should PASS Relevance but may score as "Future" in Scope dimension.

#### 2. Quality (Specification Completeness)

**High Quality**:

- Has complete YAML frontmatter (title, type, etc.)
- Uses consistent terminology throughout
- <5% broken internal links
- Formatting valid (passes markdownlint)

**Medium Quality**:

- Missing frontmatter OR has terminology conflicts OR has broken links
- Correctable with automated scripts + manual review

**Low Quality**:

- Missing frontmatter AND terminology conflicts AND broken links
- Would require complete rewrite (>50% content changes)

#### 3. Scope (MVP vs Future)

**MVP-Required** - File documents mechanics needed for 001-minimal-possession-demo:

**Core Possession Mechanics**:

- Possession system (player possesses NPCs to control them directly)
- Observer mode (autonomous simulation when not possessing)
- Context-aware actions (different actions based on class + building location)
- Release control (return to observer mode, NPC continues autonomously)

**Core Class/Progression Systems** (NPCs use these systems):

- Class system (NPCs ARE classes, not just roles)
- XP distribution (hierarchical automatic split - NPCs gain XP from actions)
- Multi-class mechanics (all classes active, primary class selection)
- Skill system (classes have skills that enable their actions)
- Character progression (NPCs level up, though MVP may start with pre-leveled NPCs)

**MVP-Required Classes** (DOCUMENTATION GAP IDENTIFIED):

**Tier 1 Foundation Class**:

- ❌ **Host** - **GAP**: No current GDD documentation
  - Foundation class for Service/Hospitality tree
  - Tag: `Service` (depth 1)
  - Unlocks: Innkeeper, Server, Housekeeper (Tier 2 specializations)
  - **BLOCKER**: Must document before 001 implementation

**Tier 2 Specialization Classes** (Inn Scenario):

- ❌ **Innkeeper** (Mara's class - player-controlled, generalist manager) - **GAP**: No current GDD documentation
  - Tags: `Service`, `Service/Hospitality` (depth 2)
  - Skills (Basic/Generalist): Basic Cooking, Basic Housekeeping, Customer Service, Staff Management, Business Operations
  - Actions: Serve customers (basic), prepare rooms (basic), hire/manage staff, track finances
  - XP source: Management tasks, customer relations, backup service
  - **BLOCKER**: Must document before 001 implementation
- ❌ **Server** (hireable NPC - food service specialist) - **GAP**: No current GDD documentation
  - Tags: `Service`, `Service/Hospitality`, `Service/Hospitality/Food` (depth 3)
  - Skills (Specialist): Efficient Service (+50% speed), Menu Knowledge, Customer Reading, Upselling
  - Actions: Take orders, deliver food, manage multiple tables simultaneously
  - XP source: Table service, customer satisfaction, upselling
  - **BLOCKER**: Must document before 001 implementation
- ❌ **Housekeeper** (hireable NPC - room service specialist) - **GAP**: No current GDD documentation
  - Tags: `Service`, `Service/Hospitality`, `Service/Hospitality/Lodging` (depth 3)
  - Skills (Specialist): Efficient Cleaning (+100% speed), Stain Mastery, Comfort Arrangement
  - Actions: Prepare rooms, manage linens, maintenance, deep cleaning
  - XP source: Room preparation, quality cleaning, proactive maintenance
  - **BLOCKER**: Must document before 001 implementation
- ✅ **Cook** (hireable NPC - kitchen specialist) - **EXISTS**: Full documentation at `crafter/cook/index.md`
  - Tags: `Crafting`, `Crafting/Cooking` (depth 2)
  - Skills: Food preparation, recipe knowledge, ingredient management
  - Actions: Prepare meals, manage kitchen, create food items
  - XP source: Cooking actions, recipe mastery
  - **STATUS**: Documentation complete, accessible from both Crafter and Host trees

**Actions as Content Pages** (NEW REQUIREMENT - MAJOR SCOPE EXPANSION):

Actions are first-class content requiring dedicated documentation pages. This applies to **ALL classes**, not just new MVP classes.

**Architectural Clarification**:

- ❌ **INCORRECT**: Classes provide bonuses (speed, efficiency, quality modifiers)
- ✅ **CORRECT**: Skills provide bonuses; classes only track actions and grant XP
- ❌ **INCORRECT**: Class level 10 gives +25% speed bonus
- ✅ **CORRECT**: Class level 10 unlocks skills; skills give speed bonuses

**Implications**:

1. All class files currently have "Tracked Actions" sections with inline action definitions
2. These must be extracted into separate action content pages
3. Class files must be updated to remove inline definitions, reference action pages instead
4. Action pages become single source of truth for action mechanics
5. Skills (not classes) provide all performance modifiers

**Scope**:

- ❌ **Actions** (NEW content category) - **GAP**: No action documentation exists
  - Structure: `docs/design/content/actions/` organized by tag hierarchy
  - Example: `actions/service/hospitality/food/serve-customer.md`
  - Properties: Tag (most specific), duration_base, resources, xp_base, modified_by_skills, tracked_by
  - Estimate: **200-500 unique actions** across all classes (after deduplication)
  - **Extraction source**: ALL class files (679 source + 4 new) contain tracked actions
  - **BLOCKER**: Must extract and create action pages during 002 implementation

**Action Extraction Process**:

For each class file:

1. Read "Tracked Actions for XP" section
2. Extract action details (name, category, XP value, tag)
3. Check if action already exists (deduplication)
4. Create/update action page with this class in `tracked_by` list
5. Update class file to reference action page instead of defining inline

**MVP-Required Actions** (Initial Set for 001):

Service/Hospitality Actions:

- `serve-customer.md` - Deliver meal to customer (tracked by: Innkeeper, Server, Cook)
- `prepare-room.md` - Clean and prepare guest room (tracked by: Innkeeper, Housekeeper)
- `greet-guest.md` - Welcome new arrival (tracked by: Innkeeper, Server, Housekeeper)
- `manage-staff.md` - Assign tasks to employees (tracked by: Innkeeper)
- `check-income.md` - Review financial status (tracked by: Innkeeper)

**Full Action Set** (across all classes):

- Estimated 200-500 unique actions across all tag branches
- Service, Crafting, Combat, Gathering, Social, etc.
- Will be extracted during Phase 2 implementation

**NEW Class Files Created** (Expanded Scope):

During 002 specification phase, 4 new class files were created in source repository (`cozy-fantasy-rpg`) to unblock MVP:

- ⚠️ **Host (Tier 1 Foundation)** - Created at `classes/host/index.md`
  - Foundation class for Service/Hospitality tree
  - 113 lines, complete specification
  - **STATUS**: Needs correction (contains incorrect class-based bonus tables)
  - **Migration**: Correct architecture, extract actions, then copy to `realms-of-idle`

- ⚠️ **Innkeeper (Tier 2 Generalist)** - Created at `classes/host/innkeeper/index.md`
  - Hospitality manager, player-controlled in MVP
  - 259 lines, complete specification with MVP integration section
  - **STATUS**: Needs correction (contains incorrect "Service Bonuses" table, "Class Bonuses" sections)
  - **Migration**: Remove bonus tables, extract tracked actions, update MVP integration, then copy

- ⚠️ **Server (Tier 2 Food Specialist)** - Created at `classes/host/server/index.md`
  - Food service specialist, hireable NPC in MVP
  - 233 lines, complete specification with MVP integration section
  - **STATUS**: Needs correction (contains incorrect "Service Bonuses" table, "Synergy Bonuses" scaling)
  - **Migration**: Remove bonus tables, extract tracked actions, update MVP integration, then copy

- ⚠️ **Housekeeper (Tier 2 Lodging Specialist)** - Created at `classes/host/housekeeper/index.md`
  - Lodging service specialist, hireable NPC in MVP
  - 248 lines, complete specification with MVP integration section
  - **STATUS**: Needs correction (contains incorrect "Service Bonuses" table, "Class Bonuses" in actions)
  - **Migration**: Remove bonus tables, extract tracked actions, update MVP integration, then copy

**Corrections Required** (before migration):

All 4 files have architectural errors discovered after creation:

- ❌ Remove: "Service Bonuses" tables showing class-level speed/efficiency bonuses
- ❌ Remove: "Class Bonuses" sections in MVP integration showing class-specific modifiers
- ❌ Remove: "Synergy Bonuses" sections showing class level affecting skill effectiveness
- ✅ Keep: Tracked Actions sections (but extract to action pages)
- ✅ Keep: Starting Skills lists (skills provide bonuses, not classes)
- ✅ Revise: Emphasize "skills provide bonuses, class provides XP tracking"

**Expanded Scope Justification**: These files are MVP blockers and were created during 002 planning to clarify requirements. Rather than treating them as a separate feature, include them in 002 migration with categorization as "Created (needs correction before migration)" in the audit report.

**Note on Customers**: Customers are NOT a class. Any NPC can be a customer when they purchase services (lodging, meals). Customer interactions grant XP to service providers (Innkeeper, Server, etc.) but don't require a dedicated class.

**Note on Crafting Classes**: Blacksmith and other crafting classes (Carpenter, etc.) are **removed from Inn scenario**. These belong to a separate **Workshop scenario** to be implemented later. MVP Inn initializes with required tools already present (no crafting mechanics needed).

**Economic Systems**:

- Resource production/consumption (Food only for Inn scenario)
- Building treasuries (gold per building, not settlement-wide)
- NPC hiring (contract costs, daily wages for Server, Housekeeper, Cook)
- Building upgrades (Inn Level 1→3, storage/capacity/speed improvements)
- Service transactions (customers pay gold for meals and lodging)

**Simulation Systems**:

- Game loop (10 ticks/second, autonomous NPC actions)
- Offline progress calculation (time-based catch-up when tab hidden)
- Activity log (last 20 NPC actions)
- Favorites system (bookmark NPCs for quick access)
- Priority management (adjust NPC behavior, persists after release)

**World Setup**:

- 10x10 grid settlement
- 1 building (Rusty Tankard Inn - Level 1, upgradeable to Level 3)
- Town Square (hiring location for unemployed NPCs)
- Pre-initialized tools (no crafting required for MVP)

---

**Future** - File documents mechanics not in MVP but in roadmap:

**Combat & Advanced Classes** (post-MVP content):

- Combat class trees (Fighter → Warrior → Knight, etc.)
- Gathering class trees (Gatherer → Hunter → Ranger, etc.)
- Advanced crafting specializations (Blacksmith → Weaponsmith/Armorsmith)
- Class tree tiers (Foundation/Specialization/Advanced progression)

**Content Expansion**:

- **Actions Content**: Beyond MVP's 5 initial actions, hundreds of additional action specifications for combat, crafting, gathering, social interactions
- **Social Systems**: Tribes, guilds, faction mechanics
- **Combat Classes**: Fighter, Warrior, Blade Dancer, Knight, Berserker, etc.
- **Combat Actions**: Attack, defend, dodge, special abilities, combo moves
- **Crafting Classes**: Blacksmith, Carpenter, and specializations (Weaponsmith, Armorsmith)
- **Crafting Actions**: Forge, smelt, repair, enhance, craft recipes
- **Workshop Scenario**: Separate building (Forge, Workshop) with crafting mechanics, resource chains (Iron Ore → Iron Tools), tool/weapon creation
- **Gathering Classes**: Hunter, Herbalist, Ranger
- **Gathering Actions**: Mine, harvest, hunt, forage, track
- **Additional NPCs**: Beyond starter inn settlement
- **World Locations**: Multiple settlements, travel systems
- **Items & Creatures**: Combat items, enemies, loot tables

**Advanced Systems**:

- Recipe discovery and crafting progression
- Tag-based class mechanics (tag depth access)
- Synergy bonuses between classes/skills
- Complex resource chains
- Seasonal events, dynamic world mechanics

---

**Out of Scope** - File documents mechanics not planned or deprecated:

- **Deprecated Features**: Dormant classes, active class slots (3-slot limit), player-configured XP split
- **Removed Mechanics**: Any mechanics contradicting current GDD (e.g., XP split instead of hierarchical automatic distribution, slot-based multi-class instead of all-active model)

#### 4. Conflicts (GDD Contradiction)

**No Conflict** - File aligns with current GDD or has no GDD overlap

**Minor Conflict** - File uses old terminology but mechanics align:

- Can be fixed with find/replace (tier → class rank/tree tier/skill tier)
- Structural changes only (no mechanics changes)

**Major Conflict** - File contradicts current GDD mechanics:

- Describes different multi-class system (slots vs all-active)
- Different XP distribution (split vs hierarchical automatic)
- Different progression model (transitions vs eligibility unlocks)

#### Categorization Rules

**Keep** - File passes all 4 dimensions:

```
Relevance: PASS
Quality: High or Medium
Scope: MVP-Required or Future
Conflicts: No Conflict
```

**Adapt** - File fails 1-2 dimensions but is salvageable:

```
Relevance: PASS
Quality: High or Medium or Low
Scope: MVP-Required or Future
Conflicts: Minor Conflict (fixable with updates)
```

OR:

```
Relevance: FAIL (but mechanics adaptable to v2)
Quality: High or Medium
Scope: MVP-Required
Conflicts: Minor Conflict
```

**Discard** - File fails 3+ dimensions or has major conflicts:

```
Relevance: FAIL
Quality: Low
Scope: Out of Scope
Conflicts: Major Conflict
```

OR any file with:

```
Conflicts: Major Conflict
Scope: Out of Scope
```

**Rationale Format** (required for each decision):

```
File: path/to/file.md
Decision: Keep | Adapt | Discard
Relevance: PASS/FAIL - [reason]
Quality: High/Medium/Low - [metrics: frontmatter yes/no, conflicts count, broken links count]
Scope: MVP-Required/Future/Out of Scope - [which MVP feature needs this]
Conflicts: None/Minor/Major - [describe conflict if present]
Rationale: [1-2 sentence justification for Keep/Adapt/Discard decision]
```

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

### TR-002: Cross-Reference System & Validation

#### Cross-Reference System Design

**Chosen Approach**: Generated Maps + Frontmatter Links (Hybrid)

**Rationale**:

This approach combines manual frontmatter references with automated map generation to provide bidirectional navigation while maintaining validation integrity.

**Alternatives Considered**:

| Option                              | Approach                                            | Pros                                                | Cons                                                        | Verdict                                   |
| ----------------------------------- | --------------------------------------------------- | --------------------------------------------------- | ----------------------------------------------------------- | ----------------------------------------- |
| **A: Frontmatter Only**             | Every content file has `gdd_ref` in frontmatter     | Simple, self-contained, easy to validate            | One-way only (Content → GDD), no reverse lookup             | ❌ Rejected - No bidirectional navigation |
| **B: Dedicated Xref Files**         | Separate `gdd-to-content.md` mapping files          | Bidirectional, centralized                          | Requires manual maintenance, can go stale, extra file reads | ❌ Rejected - Staleness risk too high     |
| **C: Generated Maps + Frontmatter** | Frontmatter `gdd_ref` + auto-generated reverse maps | Bidirectional, validated, auto-updated, never stale | Requires generation script                                  | ✅ **Selected** - Best tradeoffs          |

**Implementation Details**:

**Manual Component** (Content frontmatter):

```yaml
---
title: Warrior
gdd_ref: systems/class-system/index.md#specialization-classes
---
```

**Automated Component** (Generated map):

```markdown
# GDD to Content Cross-Reference

## systems/class-system/index.md

### § specialization-classes

Referenced by:

- [Warrior](../../content/classes/fighter/warrior/index.md)
- [Blade Dancer](../../content/classes/fighter/blade-dancer/index.md)

[Generated: 2026-02-10 14:30:00]
```

**Auto-Regeneration Triggers**:

- Pre-commit hook: If `docs/design/content/**/*.md` modified
- CI pipeline: On every push to main
- Manual: `.specify/scripts/generate-xref-report.sh`

**Staleness Prevention**:

- Generated maps include timestamp
- Validation script compares map timestamp vs newest content file
- Stale map triggers warning + auto-regeneration

---

#### Validation Rules

**Terminology Validation**:

- Detect usage of deprecated terms (tier without qualifier)
- Flag incorrect multi-class language ("transition to")
- Verify frontmatter schema compliance

**Cross-Reference Validation**:

- Detect dangling links (reference to non-existent file)
- Detect undefined mechanics (content refers to missing GDD section)
- Verify GDD status frontmatter (authoritative vs draft)
- Check map freshness (generated timestamp vs content modification)

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
