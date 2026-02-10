# Frontmatter Schema Specification

**Feature**: 002-doc-migration-rationalization
**Purpose**: Formal schema definition for YAML frontmatter in documentation files
**Last Updated**: 2026-02-10

---

## Overview

This document formally specifies the YAML frontmatter schema used throughout the `docs/design/` documentation. Frontmatter is minimal by design to reduce token overhead while enabling essential functionality: validation, cross-referencing, and navigation.

**Design Principles**:

- ✅ **Minimal**: Only required fields, few optional fields
- ✅ **Actionable**: Every field serves a validation or navigation purpose
- ✅ **Consistent**: Same schema across all content types
- ✅ **Validatable**: All fields have clear validation rules

---

## Schema Definition

### Content Files (Non-Index)

All content files (classes, skills, items, etc.) except `index.md` files:

```yaml
---
title: string # Required
gdd_ref: string # Required
parent: string # Optional
tree_tier: 1 | 2 | 3 # Optional (classes only)
---
```

### Index Files

Navigation hub files (`index.md`):

```yaml
---
title: string # Required
---
```

**Note**: Index files do NOT have `gdd_ref` because they are navigation tools, not content implementations.

---

## Field Specifications

### Field: `title`

**Type**: String
**Required**: Yes (all files)
**Validation**:

- Must be non-empty
- Must be valid UTF-8
- Recommended: Use title case (e.g., "Weapon Mastery", not "weapon mastery")

**Examples**:

```yaml
✅ title: Warrior
✅ title: Weapon Mastery
✅ title: Classes Index
❌ title: ""                    # Empty string
❌ title:                       # Missing value
```

**Purpose**: Human-readable name for file, used in:

- Navigation links
- Breadcrumbs
- Generated indexes
- Cross-reference reports

---

### Field: `gdd_ref`

**Type**: String (path#section format)
**Required**: Yes (content files only, NOT index files)
**Validation**:

- Must match pattern: `systems/[path]/[file].md#[section-id]`
- Target file must exist in `docs/design/`
- Section ID must exist in target file (heading with `{#section-id}`)
- Section ID must be kebab-case (e.g., `specialization-system`)

**Format**: `relative/path/to/gdd-file.md#section-id`

**Examples**:

```yaml
✅ gdd_ref: systems/class-system/index.md#specialization-classes
✅ gdd_ref: systems/skill-recipe-system-gdd.md#tiered-skills
✅ gdd_ref: systems/core-progression-system-gdd.md#xp-distribution
❌ gdd_ref: class-system.md#classes           # Missing systems/ prefix
❌ gdd_ref: systems/class-system/index.md     # Missing #section-id
❌ gdd_ref: systems/nonexistent.md#foo        # File doesn't exist
❌ gdd_ref: systems/class-system/index.md#SpecializationClasses  # Not kebab-case
```

**Purpose**: Links content to authoritative GDD section, enables:

- Validation (detect broken references)
- Cross-reference generation (GDD → Content maps)
- Navigation (find authoritative mechanics)

**Target GDD Section Format**:

In the GDD file, section must have ID:

```markdown
## 2. Specialization System {#specialization-system}
```

Then reference as:

```yaml
gdd_ref: systems/class-system/index.md#specialization-system
```

---

### Field: `parent`

**Type**: String (path to parent file)
**Required**: No (optional)
**Applicable**: Content files with clear parent-child relationships
**Validation**:

- Must point to existing file in `docs/design/content/`
- Must use relative path from `docs/design/content/`
- Must not create circular references

**Examples**:

```yaml
✅ parent: classes/fighter/index.md         # Warrior's parent is Fighter
✅ parent: classes/fighter/warrior/index.md  # Knight's parent is Warrior
✅ parent: skills/tiered/weapon-mastery/index.md  # Skill progression parent
❌ parent: fighter.md                       # File doesn't exist
❌ parent: ../systems/class-system.md       # Wrong directory (not in content/)
```

**Purpose**: Defines tree hierarchy, enables:

- Breadcrumb navigation
- Tree traversal validation (no circular references)
- Understanding class/skill progression paths

**When to Use**:

| Content Type         | Use `parent` | Example                                                    |
| -------------------- | ------------ | ---------------------------------------------------------- |
| Tree Tier 2+ classes | Yes          | Warrior (parent: Fighter)                                  |
| Tree Tier 1 classes  | No           | Fighter (no parent)                                        |
| Tiered skills        | Yes          | Weapon Mastery - Greater (parent: Weapon Mastery - Lesser) |
| Common skills        | No           | Power Strike (standalone)                                  |
| Items                | Maybe        | Longsword (parent: Sword category)                         |

---

### Field: `tree_tier`

**Type**: Integer (1, 2, or 3)
**Required**: No (optional)
**Applicable**: Class files only
**Validation**:

- Must be exactly 1, 2, or 3 (no other values)
- Only applicable to files in `docs/design/content/classes/`

**Examples**:

```yaml
✅ tree_tier: 1                # Foundation class (Fighter, Crafter, Gatherer)
✅ tree_tier: 2                # Specialization class (Warrior, Blacksmith)
✅ tree_tier: 3                # Advanced class (Knight, Weaponsmith)
❌ tree_tier: 4                # Invalid value
❌ tree_tier: "2"              # String instead of integer
❌ tree_tier: 0                # Invalid value
```

**Purpose**: Indicates position in class tree hierarchy:

- **Tier 1** (Foundation): Entry-level classes, no prerequisites
- **Tier 2** (Specialization): Intermediate classes, require Foundation XP
- **Tier 3** (Advanced): Elite classes, require Specialization XP

**Note**: This is "class tree tier", NOT "class rank" (Apprentice/Journeyman/Master) or "skill tier" (Lesser/Greater/Enhanced)

**When to Omit**: Non-class files (skills, items, etc.) should NOT have `tree_tier`

---

## Complete Examples

### Example 1: Tree Tier 2 Class (Warrior)

```yaml
---
title: Warrior
gdd_ref: systems/class-system/index.md#specialization-classes
parent: classes/fighter/index.md
tree_tier: 2
---
```

**Validation**:

- ✅ `title`: Non-empty string
- ✅ `gdd_ref`: Valid path, file exists, section exists
- ✅ `parent`: Points to existing Fighter class
- ✅ `tree_tier`: Valid value (2), appropriate for class file

---

### Example 2: Tree Tier 3 Class (Knight)

```yaml
---
title: Knight
gdd_ref: systems/class-system/index.md#advanced-classes
parent: classes/fighter/warrior/index.md
tree_tier: 3
---
```

**Validation**:

- ✅ All fields valid
- ✅ `parent` chain: Knight → Warrior → Fighter (valid hierarchy)

---

### Example 3: Tiered Skill

```yaml
---
title: Weapon Mastery
gdd_ref: systems/skill-recipe-system-gdd.md#tiered-skills
parent: skills/tiered/index.md
---
```

**Validation**:

- ✅ `title`, `gdd_ref`, `parent` all valid
- ✅ No `tree_tier` (correct - only for classes)

---

### Example 4: Common Skill

```yaml
---
title: Power Strike
gdd_ref: systems/skill-recipe-system-gdd.md#common-skills
---
```

**Validation**:

- ✅ Minimal frontmatter (no `parent` or `tree_tier` needed)

---

### Example 5: Index File

```yaml
---
title: Classes Index
---
```

**Validation**:

- ✅ Only `title` (correct for index files)
- ✅ No `gdd_ref` (indexes are navigation, not content)

---

## Validation Rules

### Rule 1: Required Fields

**Content files**: Must have `title` and `gdd_ref`
**Index files**: Must have `title` only

```bash
# Validation
if is_index_file "$file"; then
  require_field "title"
else
  require_field "title"
  require_field "gdd_ref"
fi
```

---

### Rule 2: Field Types

All fields must match their specified types:

```bash
# Validation
assert_type "title" "string" "$file"
assert_type "gdd_ref" "string" "$file"
assert_type "parent" "string" "$file"
assert_type "tree_tier" "integer" "$file"
```

---

### Rule 3: Field Values

**`tree_tier`**: Must be 1, 2, or 3

```bash
# Validation
if has_field "tree_tier" "$file"; then
  value=$(get_field "tree_tier" "$file")
  if [[ ! "$value" =~ ^[1-3]$ ]]; then
    error "$file: tree_tier must be 1, 2, or 3 (got: $value)"
  fi
fi
```

**`gdd_ref`**: Must follow pattern and exist

```bash
# Validation
gdd_ref=$(get_field "gdd_ref" "$file")
gdd_file=$(echo "$gdd_ref" | cut -d'#' -f1)
section_id=$(echo "$gdd_ref" | cut -d'#' -f2)

# Check file exists
if [ ! -f "docs/design/$gdd_file" ]; then
  error "$file: gdd_ref file not found: $gdd_file"
fi

# Check section exists
if ! grep -q "^#.*{#$section_id}" "docs/design/$gdd_file"; then
  error "$file: section #$section_id not found in $gdd_file"
fi
```

**`parent`**: Must exist and not create cycles

```bash
# Validation
if has_field "parent" "$file"; then
  parent=$(get_field "parent" "$file")

  # Check parent exists
  if [ ! -f "docs/design/content/$parent" ]; then
    error "$file: parent file not found: $parent"
  fi

  # Check for circular references
  if has_circular_parent "$file"; then
    error "$file: circular parent reference detected"
  fi
fi
```

---

### Rule 4: Applicability

**`tree_tier`**: Only in class files

```bash
# Validation
if has_field "tree_tier" "$file"; then
  if [[ ! "$file" =~ docs/design/content/classes/ ]]; then
    error "$file: tree_tier only applicable to class files"
  fi
fi
```

---

## YAML Syntax Rules

### Valid YAML

```yaml
---
title: Warrior
gdd_ref: systems/class-system/index.md#specialization-classes
parent: classes/fighter/index.md
tree_tier: 2
---
```

**Syntax Requirements**:

- Triple-dash delimiters (`---`) at start and end
- Key-value pairs: `key: value`
- Strings: Unquoted (unless contain special chars)
- Integers: Numeric without quotes
- Lists: Not used in this schema

---

### Common Syntax Errors

❌ **Missing delimiter**:

```yaml
title: Warrior
gdd_ref: systems/class-system/index.md#specialization-classes
---
```

**Fix**: Add opening `---`

---

❌ **Incorrect indentation**:

```yaml
---
  title: Warrior
    gdd_ref: systems/class-system/index.md#specialization-classes
---
```

**Fix**: No indentation for top-level fields

---

❌ **String as integer**:

```yaml
---
tree_tier: '2'
---
```

**Fix**: Remove quotes: `tree_tier: 2`

---

❌ **Missing colon**:

```yaml
---
title Warrior
---
```

**Fix**: Add colon: `title: Warrior`

---

## Validation Script

See `.specify/scripts/validate-frontmatter.sh` for implementation.

**Usage**:

```bash
# Validate all files
.specify/scripts/validate-frontmatter.sh

# Validate specific file
.specify/scripts/validate-frontmatter.sh docs/design/content/classes/warrior/index.md
```

**Expected Output**:

```
✅ Checking frontmatter in 654 files...
✅ All required fields present
✅ All field types valid
✅ All field values valid
✅ All applicability rules satisfied
✅ No YAML syntax errors
```

---

## Migration Guidance

### Adding Frontmatter to Existing Files

Use `update-frontmatter.sh` script:

```bash
# Dry-run (preview changes)
.specify/scripts/update-frontmatter.sh --dry-run docs/design/content/

# Apply changes
.specify/scripts/update-frontmatter.sh docs/design/content/
```

**Script will**:

- Detect files without frontmatter
- Infer `gdd_ref` from file path
- Extract `title` from first heading
- Determine `tree_tier` from path depth (classes only)
- Add properly formatted YAML frontmatter

---

### Converting Legacy Frontmatter

If migrating from previous schema with different fields:

**Legacy**:

```yaml
---
name: Warrior
tier: 2
category: combat
tags: [melee, weapon-mastery]
---
```

**Convert to**:

```yaml
---
title: Warrior
gdd_ref: systems/class-system/index.md#specialization-classes
tree_tier: 2
---
```

**Changes**:

- `name` → `title`
- `tier` → `tree_tier` (only if class file)
- Remove `category`, `tags` (not in minimal schema)
- Add `gdd_ref` (manually or via script inference)

---

## Schema Version

**Current Version**: 1.0.0

**Schema Changes** (if any):

- Must update this document
- Must update validation scripts
- Must communicate changes to team
- Must provide migration path for existing files

---

## Quick Reference

| Field       | Type    | Required | Applies To             | Purpose             |
| ----------- | ------- | -------- | ---------------------- | ------------------- |
| `title`     | string  | Yes      | All files              | Display name        |
| `gdd_ref`   | string  | Yes      | Content only           | Link to GDD         |
| `parent`    | string  | No       | Content with hierarchy | Tree structure      |
| `tree_tier` | 1\|2\|3 | No       | Classes only           | Class tree position |

**Content files**: `title`, `gdd_ref` + optional `parent`/`tree_tier`
**Index files**: `title` only

---

_Frontmatter schema specification for 002-doc-migration-rationalization feature_
