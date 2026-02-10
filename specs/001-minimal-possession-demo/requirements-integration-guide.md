# Requirements Integration Guide

**Purpose**: Step-by-step guide to integrate all critical gap resolutions into specification documents

**Date**: 2026-02-09

**Status**: Ready for integration

---

## 📋 Summary of Changes

All critical gaps from the MVP checklist have been addressed with complete, copy-paste ready requirements.

| Gap Category                  | Document   | Requirements Added             | Checklist Items Resolved         |
| ----------------------------- | ---------- | ------------------------------ | -------------------------------- |
| Game Loop Lifecycle           | spec.md    | FR-013 (a/b/c)                 | CHK003, CHK043, CHK044           |
| Offline Progress              | spec.md    | FR-014 (a/b/c), NFR-001d       | CHK076-CHK090 (15 new items)     |
| Exception Handling            | spec.md    | NFR-001 (a/b/c/d)              | CHK060, CHK061                   |
| LiteDB Persistence            | plan.md    | PR-001 through PR-006          | CHK007, CHK027, CHK051           |
| Testing Requirements          | plan.md    | TR-001, TR-002, TR-003         | CHK036, CHK037, CHK038           |
| Component Lifecycle           | plan.md    | CL-001, CL-002, CL-003         | CHK006                           |
| ActivityLogEntry Persistence  | data-model | Updated persistence model      | CHK030 (ambiguity resolved)      |
| Offline Progress Architecture | plan.md    | Technical approach, pseudocode | CHK043, CHK078, CHK083           |
| **Total**                     | **3 docs** | **25 requirement sections**    | **25+ checklist items resolved** |

---

## 🔧 Integration Steps

### Step 1: Update spec.md

**File**: `/home/mcjarvis/projects/realms-of-idle/specs/001-minimal-possession-demo/spec.md`

**Source**: `spec-updates.md` (already created)

**Actions**:

1. Open `spec.md` in editor
2. Find section `### FR-012: System MUST show notification badges for favorited NPC activity` (last FR)
3. **After FR-012**, paste the following from `spec-updates.md`:
   - FR-013: Game Loop Lifecycle Management (3 sub-requirements)
   - FR-014: Offline Progress Calculation (3 sub-requirements)
4. Find section `## Success Criteria` or create if not exists
5. **Replace SC-008, SC-009, SC-010** (or add if not present) with updated versions from `spec-updates.md`
6. **Add SC-011 through SC-014** (new success criteria)
7. Find section `## Edge Cases`
8. **Append** "Offline Progress Edge Cases" section from `spec-updates.md`
9. Find section `## Non-Functional Requirements` or create new section before Success Criteria
10. **Add** NFR-001: Exception Handling & Recovery (4 sub-requirements)
11. Save file

**Expected Result**: spec.md now contains complete functional requirements for offline progress, game loop lifecycle, and exception handling.

---

### Step 2: Update plan.md

**File**: `/home/mcjarvis/projects/realms-of-idle/specs/001-minimal-possession-demo/plan.md`

**Source**: `plan-updates.md` (already created)

**Actions**:

1. Open `plan.md` in editor
2. Find section `## Technical Context` (around line 20)
3. **After Technical Context**, add new section:
   - `## Persistence Architecture (LiteDB)` (paste PR-001 through PR-006 from `plan-updates.md`)
4. Find section `## Phase 2: Implementation Phases` or equivalent
5. **Before Phase 2**, add new sections:
   - `## Testing Strategy` (TR-001, TR-002, TR-003)
   - `## Component Architecture` (CL-001, CL-002, CL-003)
   - `## Offline Progress Technical Approach` (algorithm, rationale, pseudocode)
6. Save file

**Expected Result**: plan.md now contains complete technical specifications for LiteDB, testing, component lifecycle, and offline progress.

---

### Step 3: Update data-model.md

**File**: `/home/mcjarvis/projects/realms-of-idle/specs/001-minimal-possession-demo/data-model.md`

**Source**: `data-model-updates.md` (already created)

**Actions**:

1. Open `data-model.md` in editor
2. Find section `### ActivityLogEntry` (around line 299)
3. **Replace entire section** with updated version from `data-model-updates.md`
4. Key change: Persistence from "transient, not persisted" → "Stored in LiteDB 'activityLog' collection"
5. Save file

**Expected Result**: ActivityLogEntry persistence ambiguity resolved, LiteDB storage documented.

---

### Step 4: Verify Checklist Items

**File**: `/home/mcjarvis/projects/realms-of-idle/specs/001-minimal-possession-demo/checklists/mvp.md`

**Actions**:

1. Open `mvp.md` checklist
2. Review updated items:
   - CHK003: Now references FR-013
   - CHK043: Now references FR-013b, FR-014
   - CHK044: Now references FR-013c, CL-001
3. Review new section: "Offline Progress Requirements Quality" (CHK076-CHK090)
4. Mark items as resolved by checking them off: `- [X]` (or leave unchecked for implementation validation)

**Expected Result**: Checklist accurately reflects new requirements and can guide implementation.

---

## 📊 Requirements Coverage Matrix

| Requirement ID | Type                  | Spec.md | Plan.md | Data-Model.md | Checklist Items |
| -------------- | --------------------- | ------- | ------- | ------------- | --------------- |
| FR-013         | Game Loop Lifecycle   | ✅      | -       | -             | CHK003,043,044  |
| FR-014         | Offline Progress      | ✅      | ✅      | -             | CHK076-090      |
| NFR-001        | Exception Handling    | ✅      | -       | -             | CHK060,061      |
| PR-001         | LiteDB Initialization | -       | ✅      | -             | CHK007          |
| PR-002         | LiteDB Schema         | -       | ✅      | ✅            | CHK027,030      |
| PR-003         | LiteDB Performance    | -       | ✅      | -             | CHK051          |
| PR-004         | Persistence Ops       | -       | ✅      | -             | CHK007,027      |
| PR-005         | Data Migration        | -       | ✅      | -             | CHK027          |
| PR-006         | Backup/Recovery       | -       | ✅      | -             | CHK060          |
| TR-001         | Test Fixtures         | -       | ✅      | -             | CHK036          |
| TR-002         | Immutability Tests    | -       | ✅      | -             | CHK037          |
| TR-003         | TDD Verification      | -       | ✅      | -             | CHK038          |
| CL-001         | Component Lifecycle   | -       | ✅      | -             | CHK006          |
| CL-002         | State Management      | -       | ✅      | -             | CHK006          |
| CL-003         | Error Boundaries      | -       | ✅      | -             | CHK060,061      |

---

## 🎯 Code Implementation Artifacts

**Already Generated** (in `code-snippets/` directory):

1. `OfflineProgressCalculator.cs` - Core offline progress calculation service
2. `TabVisibilityHandler.cs` - Page Visibility API integration
3. `tab-visibility.js` - JavaScript interop module
4. `OfflineProgressModal.razor` - "Welcome back" UI component
5. `PossessionDemo-Integration.razor` - Usage example showing full integration
6. `OfflineProgressCalculatorTests.cs` - TDD unit tests (7 test cases)

**Usage**: Reference these code snippets during implementation. They demonstrate the intended architecture and can be adapted to your exact project structure.

---

## ✅ Validation Checklist

Before proceeding to implementation, verify:

- [ ] spec.md contains FR-013 (Game Loop Lifecycle)
- [ ] spec.md contains FR-014 (Offline Progress)
- [ ] spec.md contains NFR-001 (Exception Handling)
- [ ] spec.md Success Criteria updated (SC-008 through SC-014)
- [ ] spec.md Edge Cases includes Offline Progress edge cases
- [ ] plan.md contains Persistence Architecture section (PR-001 through PR-006)
- [ ] plan.md contains Testing Strategy section (TR-001 through TR-003)
- [ ] plan.md contains Component Architecture section (CL-001 through CL-003)
- [ ] plan.md contains Offline Progress Technical Approach
- [ ] data-model.md ActivityLogEntry section updated (persistence clarified)
- [ ] checklists/mvp.md items CHK003, CHK043, CHK044 updated with references
- [ ] checklists/mvp.md contains new "Offline Progress Requirements Quality" section
- [ ] All 6 code snippet files exist in `code-snippets/` directory

---

## 🚀 Next Steps After Integration

1. **Commit Documentation Updates**:

   ```bash
   git add specs/001-minimal-possession-demo/spec.md
   git add specs/001-minimal-possession-demo/plan.md
   git add specs/001-minimal-possession-demo/data-model.md
   git commit -m "docs(spec): add offline progress, game loop lifecycle, and persistence requirements

   - Add FR-013 (Game Loop Lifecycle Management)
   - Add FR-014 (Offline Progress Calculation)
   - Add NFR-001 (Exception Handling & Recovery)
   - Add PR-001 through PR-006 (LiteDB persistence architecture)
   - Add TR-001 through TR-003 (Testing requirements)
   - Add CL-001 through CL-003 (Component lifecycle requirements)
   - Clarify ActivityLogEntry persistence (LiteDB storage)
   - Update success criteria and edge cases

   Resolves checklist items: CHK003, CHK006, CHK007, CHK027, CHK030,
   CHK036, CHK037, CHK038, CHK043, CHK044, CHK051, CHK060, CHK061,
   CHK076-CHK090"
   ```

2. **Review Checklist Again**: Run through `checklists/mvp.md` to verify all critical gaps resolved

3. **Begin Implementation**:
   - Start with Phase 1: Setup (tasks.md T001-T010)
   - Proceed to Phase 2: Foundational (T011-T033)
   - Follow TDD approach (tests first, RED → GREEN → REFACTOR)
   - Use code snippets as reference implementations

4. **Track Progress**: Mark checklist items as complete during implementation validation

---

## 📚 Reference Documents

All updates are ready in:

- `spec-updates.md` - Copy-paste ready additions for spec.md
- `plan-updates.md` - Copy-paste ready additions for plan.md
- `data-model-updates.md` - Replacement section for data-model.md
- `code-snippets/` directory - 6 implementation reference files
- `checklists/mvp.md` - Updated with 90 total items (15 new for offline progress)

---

## 🎉 Summary

**All critical gaps identified in the MVP checklist have been resolved with complete, implementation-ready requirements.**

**Documentation Status**:

- ✅ Functional Requirements: Complete
- ✅ Non-Functional Requirements: Complete
- ✅ Technical Architecture: Complete
- ✅ Testing Requirements: Complete
- ✅ Component Lifecycle: Complete
- ✅ Persistence Architecture: Complete
- ✅ Offline Progress: Complete (architectural shift from pause/resume to idle game mechanics)
- ✅ Exception Handling: Complete
- ✅ Code Snippets: Complete (6 reference implementations)

**Ready to proceed with implementation following TDD approach and Constitution principles.**
