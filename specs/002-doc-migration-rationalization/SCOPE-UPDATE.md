# 002 Scope Update - Action Extraction Architecture

**Date**: 2026-02-10
**Type**: Major scope expansion
**Impact**: +200-500 files, +3-5 days implementation time

---

## Architectural Correction

### What Changed

**Discovery**: Classes should NOT provide bonuses directly. Skills provide all bonuses.

**Before (incorrect)**:

- Classes have bonus tables (e.g., "Server Level 10 = +50% speed")
- Class level directly affects action performance
- Each class has different effectiveness for same action

**After (correct)**:

- Classes **track actions** and grant XP only
- **Skills** provide all bonuses (speed, efficiency, quality)
- Class level unlocks skills or improves skill XP gain
- All characters perform actions at same base rate (modified by skills)

**Example**:

```markdown
# INCORRECT (old architecture)

Server class, Level 10:

- Serve Customer: 2.5s (50% faster than base 5s)
- Efficiency bonus: +25% from class level

# CORRECT (new architecture)

Server class:

- Tracks action: Serve Customer (gains 10 XP when performed)
- Starting skill: Efficient Service (provides +50% speed)

Efficient Service skill, Level 1:

- Speed modifier: +50% (5s → 2.5s)
- Multi-target: Can serve 2 customers simultaneously
```

---

## Scope Expansion: Action Extraction

### Original Scope (002 as planned)

**Files**:

- 679 source files from cozy-fantasy-rpg
- Migrate with minimal changes (terminology fixes, frontmatter, restructuring)
- Estimated output: ~700 files

**Work**:

- Fix GDD terminology conflicts
- Restructure folders to `<name>/index.md` pattern
- Add frontmatter with `gdd_ref` links
- Generate cross-reference maps

---

### NEW Scope (002 expanded)

**Additional Requirements**:

1. **Action Content Pages** (NEW category):
   - Extract ALL actions from ALL class files
   - Create dedicated action page for each unique action
   - Organize by tag hierarchy: `actions/service/hospitality/food/serve-customer.md`
   - Estimated: **200-500 unique action pages**

2. **Class File Corrections**:
   - Remove all class-based bonus tables from ALL classes
   - Extract "Tracked Actions" sections to separate action pages
   - Update classes to reference actions, not define them inline
   - Applies to: 679 source classes + 4 new Host/\* classes

3. **4 New Class Files** (created during spec phase):
   - Host, Innkeeper, Server, Housekeeper
   - Status: Need architectural correction before migration
   - Remove: Service Bonuses tables, Class Bonuses sections, Synergy Bonuses
   - Extract: Tracked actions to action pages

**Files**:

- 679 source files (corrected)
- 4 new class files (corrected + actions extracted)
- **200-500 action pages** (NEW)
- **Total: ~1,200 files**

---

## Action Extraction Process

### For Each Class File

**Input**: Class file with inline "Tracked Actions" section

```markdown
#### Tracked Actions

| Action Category | Specific Actions          | XP Value          |
| --------------- | ------------------------- | ----------------- |
| Table Service   | Take orders, deliver food | 8-20 per table    |
| Upselling       | Recommend premium items   | 10-25 per success |
```

**Process**:

1. Parse "Tracked Actions" section
2. Extract action names, categories, XP values, infer tags
3. Check if action already exists (deduplication by tag)
4. Create/update action page:
   - Add this class to `tracked_by` list
   - If new: create full action spec with base properties
   - If exists: append class to existing `tracked_by`
5. Update class file:
   - Replace inline actions with references to action pages
   - Keep XP values in class context (what class gains from action)

**Output**:

- Action page: `actions/service/hospitality/food/serve-customer.md`
- Updated class file: References action, shows XP gained

---

### Action Page Template

```yaml
---
title: Serve Customer
type: action
tags: Service/Hospitality/Food
duration_base: 5s
cooldown: 0s
success_rate_base: 100%
resources_required:
  - Food: 1
resources_produced:
  - Gold: 10
xp_base: 10
modified_by_skills:
  - Efficient Service
  - Menu Knowledge
  - Customer Reading
tracked_by:
  - classes/host/innkeeper
  - classes/host/server
  - classes/crafter/cook
---

# Serve Customer

## Description

Deliver a prepared meal to a customer at a table. The character takes food from inventory and provides it to a waiting customer, collecting payment upon completion.

## Base Properties

- **Tag**: `Service/Hospitality/Food` (implies `Service/Hospitality`, `Service`)
- **Duration**: 5 seconds (before skill modifiers)
- **Cooldown**: None (can serve customers back-to-back)
- **Success Rate**: 100% (always succeeds unless inventory empty)
- **Resources**: Consumes 1 Food, generates 10 Gold
- **XP**: 10 base XP granted to all tracking classes

## Skill Modifiers

Skills that modify this action's effectiveness:

### Efficient Service (Greater)
- **Speed**: +50% at skill level 1 (5s → 2.5s), +100% at level 10
- **Multi-target**: Can serve 2-3 customers simultaneously
- **Source**: Starting skill for Server class

### Menu Knowledge (Lesser)
- **Upsell Success**: +25% chance customer orders premium meal (+5g)
- **Accuracy**: Correct order 100% of time (prevents complaints)
- **Source**: Starting skill for Server class

### Customer Reading (Lesser)
- **Tip Income**: +15% gold from satisfied customers
- **Complaint Prevention**: -40% chance of negative feedback
- **Source**: Synergy skill for Server class

## Tracked By

Classes that gain XP when this action is performed:

- [Innkeeper](../../classes/host/innkeeper/) - General hospitality management
- [Server](../../classes/host/server/) - Specialized food service
- [Cook](../../classes/crafter/cook/) - Meal service at restaurant (when serving own food)

**Note**: All classes gain the same base 10 XP. Skill modifiers affect action effectiveness (speed, quality), not XP gained.

## Prerequisites

- **Inventory**: At least 1 Food in character or building inventory
- **Location**: Must be in building with dining area (Inn, Tavern, Restaurant)
- **Customer**: At least 1 waiting customer present

## Failure Conditions

- **No Food**: Action cannot be started if inventory has 0 Food
- **No Customers**: Action has no effect if no waiting customers
- **Building Closed**: Cannot perform if building is closed/inaccessible

## Related Actions

- [Take Order](./take-order.md) - Precedes serving (records customer request)
- [Clear Table](./clear-table.md) - Follows serving (prepares for next customer)
- [Prepare Meal](../../../crafting/cooking/prepare-meal.md) - Creates Food resource consumed by this action

---

_Action specification - Part of 002-doc-migration-rationalization_
```

---

### Updated Class File (Server example)

**Before** (inline actions):

```markdown
### Tracked Actions for XP

| Action Category | Specific Actions                        | XP Value          |
| --------------- | --------------------------------------- | ----------------- |
| Table Service   | Take orders, deliver food, clear tables | 8-20 per table    |
| Upselling       | Recommend premium items                 | 10-25 per success |
```

**After** (references to action pages):

```markdown
### Tracked Actions for XP

Server gains XP from actions tagged with `Service/Hospitality/Food` (and its children):

| Action         | XP Gained | Link                                                                       |
| -------------- | --------- | -------------------------------------------------------------------------- |
| Serve Customer | 10 XP     | [Action Spec](../../../actions/service/hospitality/food/serve-customer.md) |
| Take Order     | 8 XP      | [Action Spec](../../../actions/service/hospitality/food/take-order.md)     |
| Deliver Meal   | 12 XP     | [Action Spec](../../../actions/service/hospitality/food/deliver-meal.md)   |
| Clear Table    | 5 XP      | [Action Spec](../../../actions/service/hospitality/food/clear-table.md)    |
| Upsell Item    | 15 XP     | [Action Spec](../../../actions/service/hospitality/food/upsell-item.md)    |

**Note**: All XP values are base amounts. Skills modify action effectiveness (speed, quality), not XP gained.
```

---

## Impact Assessment

### File Count

| Category                  | Original | NEW      | Total         |
| ------------------------- | -------- | -------- | ------------- |
| Source files (migrated)   | 679      | +0       | 679           |
| New class files (created) | 0        | +4       | 4             |
| Action pages (extracted)  | 0        | +200-500 | 200-500       |
| **TOTAL**                 | 679      | +204-504 | **883-1,183** |

### Time Estimate

| Phase                     | Original | Action Extraction             | Total      |
| ------------------------- | -------- | ----------------------------- | ---------- |
| Phase A: GDD Corrections  | 3-4d     | +0                            | 3-4d       |
| Phase B: Structure        | 2-3d     | +1d (create actions/ folder)  | 3-4d       |
| Phase C: Frontmatter      | 2d       | +0                            | 2d         |
| Phase D: Content Updates  | 2-3d     | **+3-5d (action extraction)** | 5-8d       |
| Phase E: Source Migration | 2-3d     | +0                            | 2-3d       |
| Phase F: Validation       | 1d       | +0                            | 1d         |
| **TOTAL**                 | 12-16d   | **+4-6d**                     | **16-22d** |

**Critical path**: Phase D (Content Updates) now includes action extraction across ALL classes.

---

## Phase D Expansion: Action Extraction

### Subtasks

**D1: Analyze All Classes** (1 day):

- Scan all class files for "Tracked Actions" sections
- Extract action names, categories, tags, XP values
- Build action inventory (deduplicated list)
- Estimate: 200-500 unique actions

**D2: Create Action Pages** (2-3 days):

- Generate action page template for each unique action
- Fill in base properties (duration, resources, XP, tags)
- Infer skill modifiers from class synergy sections
- Organize by tag hierarchy in `actions/` folder
- Add frontmatter with proper schema

**D3: Update Class Files** (1-2 days):

- Remove inline action definitions from all classes
- Replace with references to action pages
- Maintain XP values in class context
- Update tracked actions tables to link format

**D4: Correct 4 New Classes** (0.5 day):

- Remove Service Bonuses tables (Host, Innkeeper, Server, Housekeeper)
- Remove Class Bonuses from MVP integration sections
- Remove Synergy Bonuses scaling tables
- Extract their actions (already counted in D2)

**D5: Validate** (0.5 day):

- Cross-reference validation (all action links valid)
- Ensure all tracked actions have action pages
- Verify bidirectional references (class ↔ action)

---

## Updated Phase 2 Dependencies

**Before**:

```
Phase A + Phase B → Phase C → Phase D → Phase E → Phase F
```

**After** (action extraction in Phase D):

```
Phase A + Phase B → Phase C → Phase D (expanded) → Phase E → Phase F
                                  ↓
                        D1: Analyze classes
                        D2: Create action pages
                        D3: Update class files
                        D4: Correct new classes
                        D5: Validate
```

**Blocking**: Phase E (Source Migration) now requires Phase D action extraction to complete.

---

## Validation Updates

### New Validation: Action References

**Script**: `.specify/scripts/validate-action-references.sh`

**Checks**:

- All class "Tracked Actions" tables link to existing action pages
- All action pages list valid classes in `tracked_by` frontmatter
- No orphaned actions (action page exists but no class tracks it)
- No missing actions (class references action that doesn't exist)

**Success Output**:

```
✅ Checking 150 classes with tracked actions...
✅ All action references valid (834 actions referenced)
✅ All tracked_by lists valid (456 unique actions)
✅ No orphaned actions
✅ No missing action pages

Action reference validation: PASSED
```

---

## Migration Audit Categories

### Original Categories

- Keep (aligns with GDD)
- Adapt (needs modification)
- Discard (not in scope)

### NEW Category

- **Extract Actions** (ALL class files):
  - Applies to: Every class file with tracked actions
  - Work: Extract actions to dedicated pages
  - Update: Class file to reference actions
  - Rationale: Classes track actions (not tags), architectural requirement

**Audit Report Update**:

```markdown
## Categorization Summary

| Category                      | Count | %    |
| ----------------------------- | ----- | ---- |
| Keep (minimal changes)        | 245   | 36%  |
| Adapt (terminology/structure) | 312   | 46%  |
| Extract Actions (all classes) | 122   | 18%  |
| Discard (obsolete)            | 0     | 0%   |
| **TOTAL**                     | 679   | 100% |

**Note**: All 122 class files require action extraction (architectural change).
```

---

## Next Steps

**Immediate**:

1. ✅ Document architectural correction in spec.md (DONE)
2. ✅ Update scope to include action extraction (DONE)
3. ❌ Update plan.md Phase D with action extraction subtasks (PENDING)

**Phase 0 (Research)**:

- Sample class files to verify action extraction approach
- Estimate action count more precisely (currently 200-500 range)
- Design action deduplication strategy

**Phase 2 (Implementation)**:

- Execute expanded Phase D with action extraction
- Correct 4 new class files before migration
- Create action validation scripts

---

## Questions for Clarification

1. **Action Deduplication**: How to handle same action tracked by multiple classes?
   - Create one action page, list all tracking classes in `tracked_by`?
   - Or: One action page per class (no sharing)?
   - **Recommendation**: Share action pages (DRY principle)

2. **Skill Modifiers**: How detailed should `modified_by_skills` be in action pages?
   - Just list skill names?
   - Include full modifier details (speed%, success rate%, etc.)?
   - **Recommendation**: Include modifier details (makes action page complete reference)

3. **Tag Inference**: If class file doesn't specify tags for actions, how to infer?
   - Use class category + action category?
   - Require manual tagging?
   - **Recommendation**: Infer from class category, validate manually

4. **MVP Priority**: Should we extract ALL 200-500 actions before 001, or just MVP's 5?
   - Extract all (blocks 001 longer, but complete)
   - Extract MVP only (unblocks 001 faster, partial migration)
   - **Recommendation**: Extract MVP actions first (unblock 001), rest in parallel

---

_Scope update for 002-doc-migration-rationalization feature_
