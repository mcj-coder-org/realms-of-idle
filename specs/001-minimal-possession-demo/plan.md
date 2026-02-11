# Implementation Plan: [FEATURE]

**Branch**: `[###-feature-name]` | **Date**: [DATE] | **Spec**: [link]
**Input**: Feature specification from `/specs/[###-feature-name]/spec.md`

**Note**: This template is filled in by the `/speckit.plan` command. See `.specify/templates/commands/plan.md` for the execution workflow.

## Summary

[Extract from feature spec: primary requirement + technical approach from research]

## Technical Context

<!--
  ACTION REQUIRED: Replace the content in this section with the technical details
  for the project. The structure here is presented in advisory capacity to guide
  the iteration process.
-->

**Language/Version**: [e.g., Python 3.11, Swift 5.9, Rust 1.75 or NEEDS CLARIFICATION]  
**Primary Dependencies**: [e.g., FastAPI, UIKit, LLVM or NEEDS CLARIFICATION]  
**Storage**: [if applicable, e.g., PostgreSQL, CoreData, files or N/A]  
**Testing**: [e.g., pytest, XCTest, cargo test or NEEDS CLARIFICATION]  
**Target Platform**: [e.g., Linux server, iOS 15+, WASM or NEEDS CLARIFICATION]
**Project Type**: [single/web/mobile - determines source structure]  
**Performance Goals**: [domain-specific, e.g., 1000 req/s, 10k lines/sec, 60 fps or NEEDS CLARIFICATION]  
**Constraints**: [domain-specific, e.g., <200ms p95, <100MB memory, offline-capable or NEEDS CLARIFICATION]  
**Scale/Scope**: [domain-specific, e.g., 10k users, 1M LOC, 50 screens or NEEDS CLARIFICATION]

## Constitution Check

_GATE: Must pass before Phase 0 research. Re-check after Phase 1 design._

Verify compliance with [Constitution v1.0.0](../.specify/memory/constitution.md):

- [ ] **Test-First Development**: Feature spec includes testable acceptance criteria
- [ ] **Quality Standards**: All quality gates (pre-commit, pre-push, CI) applicable
- [ ] **Git Discipline**: Feature branch strategy documented, conventional commits planned
- [ ] **Simplicity First**: No unnecessary abstractions or speculative features
- [ ] **Automation**: Repetitive tasks identified for automation

**Complexity Justification** (complete ONLY if adding complexity):

| Added Complexity              | Justification   | Simpler Alternative Rejected Because |
| ----------------------------- | --------------- | ------------------------------------ |
| [e.g., new abstraction layer] | [specific need] | [why direct approach insufficient]   |

## Project Structure

### Documentation (this feature)

```text
specs/[###-feature]/
├── plan.md              # This file (/speckit.plan command output)
├── research.md          # Phase 0 output (/speckit.plan command)
├── data-model.md        # Phase 1 output (/speckit.plan command)
├── quickstart.md        # Phase 1 output (/speckit.plan command)
├── contracts/           # Phase 1 output (/speckit.plan command)
└── tasks.md             # Phase 2 output (/speckit.tasks command - NOT created by /speckit.plan)
```

### Source Code (repository root)

<!--
  ACTION REQUIRED: Replace the placeholder tree below with the concrete layout
  for this feature. Delete unused options and expand the chosen structure with
  real paths (e.g., apps/admin, packages/something). The delivered plan must
  not include Option labels.
-->

```text
# [REMOVE IF UNUSED] Option 1: Single project (DEFAULT)
src/
├── models/
├── services/
├── cli/
└── lib/

tests/
├── contract/
├── integration/
└── unit/

# [REMOVE IF UNUSED] Option 2: Web application (when "frontend" + "backend" detected)
backend/
├── src/
│   ├── models/
│   ├── services/
│   └── api/
└── tests/

frontend/
├── src/
│   ├── components/
│   ├── pages/
│   └── services/
└── tests/

# [REMOVE IF UNUSED] Option 3: Mobile + API (when "iOS/Android" detected)
api/
└── [same as backend above]

ios/ or android/
└── [platform-specific structure: feature modules, UI flows, platform tests]
```

**Structure Decision**: [Document the selected structure and reference the real
directories captured above]

## Complexity Tracking

> **Fill ONLY if Constitution Check has violations that must be justified**

| Violation                  | Why Needed         | Simpler Alternative Rejected Because |
| -------------------------- | ------------------ | ------------------------------------ |
| [e.g., 4th project]        | [current need]     | [why 3 projects insufficient]        |
| [e.g., Repository pattern] | [specific problem] | [why direct DB access insufficient]  |

---

## Related Documentation

**Project Documentation Structure**: Following the migration completed in feature 002-doc-migration-rationalization, the project documentation now uses a hierarchical progressive loading architecture. See [docs/design/README.md](../../docs/design/README.md) for navigation guide.

### Relevant Documentation for 001 Implementation

**Progressive Loading (3 Levels)**:

1. **System Overview**: [docs/design/index.md](../../docs/design/index.md) - Lists all system GDDs
2. **System GDDs**: [docs/design/systems/](../../docs/design/systems/) - Authoritative mechanics per domain
3. **Content Examples**: [docs/design/content/](../../docs/design/content/) - Specific implementations

**Action Templates** (highly relevant for T016 ActionCatalog):

- Service actions: [docs/design/content/actions/service/](../../docs/design/content/actions/service/)
  - `serve/index.md`, `clean/index.md`, `tend/index.md`, `entertain/index.md`, `guide/index.md`, `heal/index.md`
- Crafting actions: [docs/design/content/actions/crafting/](../../docs/design/content/actions/crafting/)
  - `craft/index.md`, `repair/index.md`, `forge/index.md`, `smelt/index.md`, `brew/index.md`, `cook/index.md`, `weave/index.md`, `enchant/index.md`

**Action Page Format** (template structure):

```yaml
---
title: [Action Name]
gdd_ref: systems/action-system-gdd.md#[section]
domain: [service|crafting|combat|etc]
ui_trigger: context-menu
base_duration: [time range]
---
## Description
## Context Requirements
## Tag Resolution (for XP distribution)
## Modified By Skills
## Prerequisites
## Failure Conditions
```

**Core Architecture**:

- [docs/design/core-architecture.md](../../docs/design/core-architecture.md) - Overall system design (referenced in quickstart.md)

**Migration Details**:

- [docs/design/MIGRATION-REPORT.md](../../docs/design/MIGRATION-REPORT.md) - Complete documentation restructuring report

**Usage Guidance**:

- When implementing ActionCatalog (T016), reference the action templates above for structure and standard fields
- Action templates provide concrete examples of context requirements, tag resolution, and failure conditions
- Use the hierarchical documentation structure (index → GDD → content) to find design details efficiently
