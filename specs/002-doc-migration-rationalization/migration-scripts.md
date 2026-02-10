# Migration Scripts Design

**Feature**: 002-doc-migration-rationalization
**Purpose**: Automated scripts for bulk documentation updates
**Last Updated**: 2026-02-10

---

## Overview

This document specifies the migration scripts needed to transform 679 source files from the previous project (cozy-fantasy-rpg) into the target structure for realms-of-idle.

**Design Principles**:

- ✅ **Dry-run mode**: Preview changes before applying
- ✅ **Backup strategy**: Automatic backups before modifications
- ✅ **Rollback procedure**: Easy undo if something goes wrong
- ✅ **Idempotent**: Safe to run multiple times
- ✅ **Progress reporting**: Clear feedback during long operations

---

## Script 1: Update Frontmatter

**Script**: `.specify/scripts/update-frontmatter.sh`

**Purpose**: Add/update frontmatter in content files

### What It Does

1. **Add missing frontmatter** to files without YAML headers
2. **Convert `tier:` → `tree_tier:`** in class files
3. **Add `gdd_ref` links** based on file path heuristics
4. **Preserve existing fields** (don't overwrite manual additions)

### Usage

```bash
# Dry-run (preview changes only)
.specify/scripts/update-frontmatter.sh --dry-run

# Apply to specific directory
.specify/scripts/update-frontmatter.sh docs/design/content/classes/

# Apply to all content
.specify/scripts/update-frontmatter.sh docs/design/content/

# Backup location
.specify/scripts/update-frontmatter.sh --backup-dir=/tmp/frontmatter-backup
```

### Logic

```bash
#!/bin/bash

# For each .md file in content/
for file in docs/design/content/**/*.md; do
  # Skip if already has valid frontmatter
  if has_valid_frontmatter "$file"; then
    continue
  fi

  # Determine gdd_ref based on file path
  gdd_ref=$(infer_gdd_ref_from_path "$file")

  # Extract title from first heading or filename
  title=$(extract_title "$file")

  # Check if this is a class file (needs tree_tier)
  if is_class_file "$file"; then
    tree_tier=$(infer_tree_tier_from_path "$file")

    # Add frontmatter with tree_tier
    add_frontmatter "$file" "$title" "$gdd_ref" "$tree_tier"
  else
    # Add frontmatter without tree_tier
    add_frontmatter "$file" "$title" "$gdd_ref"
  fi
done
```

### Frontmatter Inference Rules

**GDD Reference Inference**:

```
File path: docs/design/content/classes/fighter/warrior/index.md
→ gdd_ref: systems/class-system/index.md#specialization-classes

File path: docs/design/content/skills/tiered/weapon-mastery/index.md
→ gdd_ref: systems/skill-recipe-system-gdd.md#tiered-skills

File path: docs/design/content/items/weapons/sword/longsword/index.md
→ gdd_ref: systems/item-system-gdd.md#weapons
```

**Tree Tier Inference** (classes only):

```
Path depth 1 (docs/design/content/classes/fighter/)
→ tree_tier: 1 (Foundation)

Path depth 2 (docs/design/content/classes/fighter/warrior/)
→ tree_tier: 2 (Specialization)

Path depth 3 (docs/design/content/classes/fighter/warrior/knight/)
→ tree_tier: 3 (Advanced)
```

### Output Format

**Before**:

```markdown
# Warrior

Warrior is a Tier 2 specialization...
```

**After**:

```markdown
---
title: Warrior
gdd_ref: systems/class-system/index.md#specialization-classes
parent: classes/fighter/index.md
tree_tier: 2
---

# Warrior

Warrior is a class tree tier 2 specialization...
```

### Error Handling

- **Cannot infer `gdd_ref`**: Log warning, add placeholder `gdd_ref: TODO`
- **File has no heading**: Use filename for title
- **Ambiguous tree tier**: Default to no `tree_tier`, log warning
- **Backup fails**: Abort before making changes

---

## Script 2: Restructure Folders

**Script**: `.specify/scripts/restructure-folders.sh`

**Purpose**: Move files to `<name>/index.md` pattern

### What It Does

1. **Convert `warrior.md` → `warrior/index.md`**
2. **Create `examples/` and `tables/` subfolders** where needed
3. **Update internal links** to reflect new paths
4. **Preserve git history** using `git mv`

### Usage

```bash
# Dry-run (preview moves only)
.specify/scripts/restructure-folders.sh --dry-run

# Apply to specific directory
.specify/scripts/restructure-folders.sh docs/design/content/classes/

# Apply to all content
.specify/scripts/restructure-folders.sh docs/design/content/
```

### Logic

```bash
#!/bin/bash

# For each .md file (excluding index.md files)
for file in docs/design/content/**/*.md; do
  if [[ $(basename "$file") == "index.md" ]]; then
    continue  # Already in correct format
  fi

  # Determine new path
  dir=$(dirname "$file")
  name=$(basename "$file" .md)
  new_path="$dir/$name/index.md"

  # Create directory
  mkdir -p "$dir/$name"

  # Move file (preserve git history)
  git mv "$file" "$new_path"

  # Check for related files (examples, tables, diagrams)
  if has_examples "$file"; then
    mkdir -p "$dir/$name/examples"
    move_examples "$file" "$dir/$name/examples/"
  fi

  if has_tables "$file"; then
    mkdir -p "$dir/$name/tables"
    move_tables "$file" "$dir/$name/tables/"
  fi
done

# Update all internal links to point to new paths
update_internal_links docs/design/
```

### Example Transformation

**Before**:

```
docs/design/content/classes/
├── fighter.md
├── warrior.md
└── knight.md
```

**After**:

```
docs/design/content/classes/
├── fighter/
│   ├── index.md
│   └── examples/
│       └── early-game-fighter.md
├── warrior/
│   ├── index.md
│   ├── examples/
│   │   └── typical-progression.md
│   └── tables/
│       └── stat-progression.md
└── knight/
    ├── index.md
    └── examples/
        └── honorable-combat.md
```

### Link Updates

**Before**:

```markdown
See [Warrior](warrior.md) for details.
```

**After**:

```markdown
See [Warrior](warrior/index.md) for details.
```

---

## Script 3: Generate Index Pages

**Script**: `.specify/scripts/generate-indexes.sh`

**Purpose**: Create navigation hub index.md files

### What It Does

1. **Generate root index**: `docs/design/index.md`
2. **Generate category indexes**: `systems/index.md`, `content/index.md`, `reference/index.md`
3. **Generate subcategory indexes**: `content/classes/index.md`, `content/skills/index.md`, etc.
4. **Use descriptive format**: List + 1 sentence description per item (200-300 lines)

### Usage

```bash
# Generate all indexes
.specify/scripts/generate-indexes.sh

# Generate specific index
.specify/scripts/generate-indexes.sh docs/design/systems/

# Force regenerate (overwrite existing)
.specify/scripts/generate-indexes.sh --force
```

### Logic

```bash
#!/bin/bash

generate_index() {
  local dir=$1
  local index_file="$dir/index.md"

  # Don't overwrite manually-crafted indexes (unless --force)
  if [ -f "$index_file" ] && [ "$FORCE" != "true" ]; then
    echo "⚠️  $index_file exists, skipping (use --force to overwrite)"
    return
  fi

  # Gather subdirectories and files
  subdirs=$(find "$dir" -mindepth 1 -maxdepth 1 -type d -not -name "examples" -not -name "tables")
  files=$(find "$dir" -mindepth 1 -maxdepth 1 -name "*.md" -not -name "index.md")

  # Generate index content
  {
    echo "# $(basename "$dir" | title_case) Index"
    echo ""
    echo "Brief introduction paragraph about this category."
    echo ""
    echo "## Navigation"
    echo ""

    # List subdirectories
    for subdir in $subdirs; do
      name=$(basename "$subdir")
      description=$(extract_description "$subdir/index.md")
      echo "- [$name]($name/index.md) - $description"
    done

    # List files
    for file in $files; do
      name=$(basename "$file" .md)
      description=$(extract_description "$file")
      echo "- [$name]($name/index.md) - $description"
    done
  } > "$index_file"

  echo "✅ Generated $index_file"
}

# Generate indexes recursively
generate_index_recursive docs/design/
```

### Index Template

See **index-template.md** (separate document) for full template specification.

**Generated Example**:

```markdown
# Classes Index

Comprehensive catalog of all character classes in Realms of Idle, organized by class tree tier and role.

## Navigation

### Foundation Classes (Tier 1)

- [Fighter](fighter/index.md) - Basic combat specialist, foundation for melee classes
- [Crafter](crafter/index.md) - Item creation specialist, foundation for crafting classes
- [Gatherer](gatherer/index.md) - Resource collection specialist, foundation for gathering classes

### Specialization Classes (Tier 2)

- [Warrior](warrior/index.md) - Melee combat expert with weapon mastery focus
- [Blacksmith](blacksmith/index.md) - Metal crafting specialist for weapons and armor
- [Archer](archer/index.md) - Ranged combat expert with bow proficiency

### Advanced Classes (Tier 3)

- [Knight](knight/index.md) - Elite melee combatant with honor-based progression
- [Weaponsmith](weaponsmith/index.md) - Master craftsman specializing in weapon creation

## Related

- [Class System GDD](../../systems/class-system/index.md) - Authoritative class mechanics
- [Skills](../skills/index.md) - Class-associated skills
```

---

## Script 4: Update Terminology

**Script**: `.specify/scripts/update-terminology.sh`

**Purpose**: Replace deprecated/ambiguous terms with correct terminology

### What It Does

1. **Find/replace deprecated terms**: "dormant classes", "XP split", "3 active slots"
2. **Disambiguate "tier"**: Add context (class rank, tree tier, skill tier)
3. **Log ambiguous cases**: Flag cases needing manual review

### Usage

```bash
# Dry-run (preview changes only)
.specify/scripts/update-terminology.sh --dry-run

# Apply to all documentation
.specify/scripts/update-terminology.sh docs/design/

# Specific replacements only
.specify/scripts/update-terminology.sh --only="dormant-classes,xp-split"
```

### Replacement Rules

| Find                               | Replace                              | Context                  |
| ---------------------------------- | ------------------------------------ | ------------------------ |
| "dormant classes"                  | [DELETE or "all classes are active"] | Remove entire concept    |
| "XP split" (player-configured)     | "automatic XP distribution"          | Mechanics section        |
| "3 active slots"                   | [DELETE]                             | Remove slot concept      |
| "tier" (Apprentice/Journeyman)     | "class rank"                         | Within-class progression |
| "tier" (Foundation/Specialization) | "class tree tier"                    | Tree position            |
| "tier" (Lesser/Greater)            | "skill tier"                         | Skill progression        |

### Ambiguous Cases

**Flag for manual review**:

```markdown
❌ docs/design/content/classes/warrior/index.md:45
Ambiguous: "reaches tier 2"
Could be: "reaches class rank: Journeyman" OR "advances to class tree tier 2"
Manual review required
```

### Logic

```bash
#!/bin/bash

# Simple replacements (unambiguous)
sed -i 's/dormant classes/[all classes are active]/g' "$file"
sed -i 's/XP split configuration/automatic XP distribution/g' "$file"
sed -i 's/3 active class slots//g' "$file"

# Complex replacements (context-aware)
# Requires parsing markdown to understand context
replace_tier_with_context "$file"

# Log ambiguous cases
detect_ambiguous_terminology "$file"
```

---

## Script 5: Generate Cross-Reference Maps

**Script**: `.specify/scripts/generate-xref-report.sh`

**Purpose**: Build GDD → Content mappings

### What It Does

1. **Scan all content files** for `gdd_ref` frontmatter
2. **Group by GDD target** and section
3. **Generate markdown report** with bidirectional links
4. **Include statistics** and timestamp

### Usage

```bash
# Generate cross-reference map
.specify/scripts/generate-xref-report.sh

# Output location
cat docs/design/reference/cross-reference/gdd-to-content.md
```

### Logic

```bash
#!/bin/bash

declare -A gdd_map  # Associative array: gdd_section → [content_files]

# Scan all content files
for file in docs/design/content/**/*.md; do
  gdd_ref=$(yq eval '.gdd_ref' "$file" 2>/dev/null)

  if [ -n "$gdd_ref" ]; then
    gdd_map["$gdd_ref"]+="$file "
  fi
done

# Generate report
{
  echo "# GDD to Content Cross-Reference"
  echo ""
  echo "**Generated**: $(date -Iseconds)"
  echo "**Content Files Scanned**: $(find docs/design/content -name '*.md' | wc -l)"
  echo "**GDD Sections Referenced**: ${#gdd_map[@]}"
  echo ""

  # Group by GDD file
  for gdd_section in "${!gdd_map[@]}"; do
    gdd_file=$(echo "$gdd_section" | cut -d'#' -f1)
    section_id=$(echo "$gdd_section" | cut -d'#' -f2)

    echo "## $gdd_file"
    echo ""
    echo "### § $section_id"
    echo ""
    echo "Referenced by:"
    echo ""

    # List content files
    for content_file in ${gdd_map["$gdd_section"]}; do
      title=$(yq eval '.title' "$content_file" 2>/dev/null || basename "$content_file" .md)
      rel_path=$(realpath --relative-to="docs/design/reference/cross-reference" "$content_file")
      echo "- [$title]($rel_path)"
    done

    echo ""
  done
} > docs/design/reference/cross-reference/gdd-to-content.md

echo "✅ Generated cross-reference map"
```

---

## Script 6: Validate All

**Script**: `.specify/scripts/validate-all.sh`

**Purpose**: Run all validation scripts in sequence

### What It Does

1. **Run validate-frontmatter.sh**
2. **Run validate-cross-references.sh**
3. **Run validate-terminology.sh**
4. **Report summary** (pass/fail for each)

### Usage

```bash
# Run all validations
.specify/scripts/validate-all.sh

# Quick mode (stops at first failure)
.specify/scripts/validate-all.sh --fail-fast
```

### Output

```
🔍 Running all documentation validations...

✅ Frontmatter validation passed (654 files checked)
✅ Cross-reference validation passed (0 broken links)
✅ Terminology validation passed (0 conflicts)
⚠️  Cross-reference map is stale (regenerating...)
✅ Cross-reference map regenerated

✅ All validations passed!
```

---

## Backup and Rollback Strategy

### Automatic Backups

All migration scripts create automatic backups before modifying files:

```bash
# Backup location
.specify/backups/$(date -Iseconds)/

# Example
.specify/backups/2026-02-10T15:30:00/
├── docs/design/content/classes/warrior.md
├── docs/design/content/classes/fighter.md
└── manifest.txt  # List of all backed-up files
```

### Manual Backup

```bash
# Create backup before migration
.specify/scripts/backup-docs.sh

# Backup stored in
.specify/backups/manual-2026-02-10T15:30:00/
```

### Rollback Procedure

```bash
# List available backups
.specify/scripts/list-backups.sh

# Restore from specific backup
.specify/scripts/restore-backup.sh 2026-02-10T15:30:00

# Or use git to revert
git reset --hard HEAD~1  # Revert last commit
git clean -fd           # Remove untracked files
```

---

## Execution Order

**Recommended sequence for migration**:

```bash
# 1. Backup everything
.specify/scripts/backup-docs.sh

# 2. Update frontmatter (adds gdd_ref, fixes tier terminology)
.specify/scripts/update-frontmatter.sh docs/design/content/

# 3. Restructure folders (move to <name>/index.md pattern)
.specify/scripts/restructure-folders.sh docs/design/content/

# 4. Generate indexes (create navigation hubs)
.specify/scripts/generate-indexes.sh

# 5. Update terminology (fix deprecated terms)
.specify/scripts/update-terminology.sh docs/design/

# 6. Generate cross-reference maps
.specify/scripts/generate-xref-report.sh

# 7. Validate everything
.specify/scripts/validate-all.sh

# 8. Commit if all passed
git add docs/design/
git commit -m "docs: migrate and rationalize documentation structure"
```

---

## Model Recommendations

| Script                  | Model      | Rationale                                  |
| ----------------------- | ---------- | ------------------------------------------ |
| update-frontmatter.sh   | **Haiku**  | Pattern-based edits, straightforward logic |
| restructure-folders.sh  | **Haiku**  | File operations, git mv commands           |
| generate-indexes.sh     | **Sonnet** | Needs to write descriptive summaries       |
| update-terminology.sh   | **Haiku**  | Find/replace with validation               |
| generate-xref-report.sh | **Haiku**  | Data aggregation and formatting            |
| validate-all.sh         | **Haiku**  | Script orchestration                       |

---

## Testing Strategy

### Unit Tests (per script)

```bash
# Test on small sample before full migration
.specify/scripts/update-frontmatter.sh --dry-run docs/design/content/classes/fighter/

# Verify output manually
cat docs/design/content/classes/fighter/index.md
```

### Integration Tests

```bash
# Run full migration on test branch
git checkout -b test-migration
./run-migration.sh  # Executes all scripts in order
.specify/scripts/validate-all.sh

# If passes, merge to feature branch
git checkout 002-doc-migration-rationalization
git merge test-migration
```

---

_Migration scripts design for 002-doc-migration-rationalization feature_
