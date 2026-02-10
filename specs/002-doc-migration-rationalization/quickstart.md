# Quickstart: Documentation Validation Guide

**Feature**: 002-doc-migration-rationalization
**Purpose**: Validate documentation changes for consistency, integrity, and quality
**Last Updated**: 2026-02-10

---

## Overview

This guide explains how to validate documentation changes in the `docs/design/` directory. All validation scripts are located in `.specify/scripts/` and are designed to be fast, automated, and suitable for pre-commit hooks.

**Validation Goals**:

- ✅ Terminology consistency (class rank, tree tier, skill tier)
- ✅ Cross-reference integrity (no broken links)
- ✅ Frontmatter schema compliance
- ✅ Progressive loading effectiveness (<3 file reads)

---

## Prerequisites

### Required Tools

```bash
# Check if tools are installed
bash --version      # GNU Bash 4.0+
grep --version      # GNU grep 3.0+
find --version      # GNU findutils 4.6+
yq --version        # yq 4.0+ (for YAML parsing)

# Install missing tools (Ubuntu/Debian)
sudo apt-get install bash grep findutils yq

# Install missing tools (macOS with Homebrew)
brew install bash grep findutils yq
```

### Repository Setup

```bash
# Clone repository
git clone git@github-martincjarvis:owner/realms-of-idle.git
cd realms-of-idle

# Ensure scripts are executable
chmod +x .specify/scripts/*.sh
```

---

## Validation Scripts

### 1. Terminology Validation

**Script**: `.specify/scripts/validate-terminology.sh`

**What It Checks**:

- Consistent use of "class rank" (not "tier") for Apprentice/Journeyman/Master
- Consistent use of "class tree tier" (not "tier") for Foundation/Specialization/Advanced
- Consistent use of "skill tier" for Lesser/Greater/Enhanced
- No references to deprecated terms: "dormant classes", "XP split", "active slots"

**Usage**:

```bash
# Validate all documentation
.specify/scripts/validate-terminology.sh

# Validate specific directory
.specify/scripts/validate-terminology.sh docs/design/systems/

# Validate single file
.specify/scripts/validate-terminology.sh docs/design/systems/class-system-gdd.md
```

**Success Output**:

```
✅ Checking terminology in 654 files...
✅ No terminology conflicts found
✅ All files use consistent terminology
```

**Failure Output**:

```
❌ docs/design/systems/class-system-gdd.md:245
   Found deprecated term: "dormant classes"
   Replace with: "all classes are active"

❌ docs/design/content/classes/warrior/index.md:15
   Found ambiguous term: "tier 2"
   Replace with: "class tree tier 2" or "class rank: Journeyman"

❌ Found 2 terminology issues
```

**Common Issues**:

| Issue                          | Fix                                                       |
| ------------------------------ | --------------------------------------------------------- |
| "tier" without context         | Specify: "class rank", "class tree tier", or "skill tier" |
| "dormant classes"              | Remove or replace with "all classes active"               |
| "XP split" (player-configured) | Replace with "automatic XP distribution"                  |
| "3 active slots"               | Remove (all classes are always active)                    |

---

### 2. Cross-Reference Validation

**Script**: `.specify/scripts/validate-cross-references.sh`

**What It Checks**:

- All `gdd_ref` frontmatter links point to existing files
- All `gdd_ref` section IDs exist in target files
- All internal markdown links resolve
- No circular references in `parent` chains
- All content files have `gdd_ref` (except index.md files)

**Usage**:

```bash
# Validate all cross-references
.specify/scripts/validate-cross-references.sh

# Validate specific file
.specify/scripts/validate-cross-references.sh docs/design/content/classes/warrior/index.md
```

**Success Output**:

```
✅ Checking 654 content files...
✅ All gdd_ref targets exist
✅ All section IDs valid
✅ No circular references found
✅ Cross-reference map is fresh (generated 2 minutes ago)
```

**Failure Output**:

```
❌ docs/design/content/classes/warrior/index.md
   gdd_ref: systems/class-system/index.md#specialization-classes
   Error: Section ID 'specialization-classes' not found
   Available sections: #terminology, #foundation-classes, #specialization-system

❌ docs/design/content/skills/weapon-mastery/index.md
   gdd_ref: systems/skill-system/index.md#tiered-skills
   Error: File 'systems/skill-system/index.md' does not exist
   Did you mean: systems/skill-recipe-system-gdd.md?

⚠️  Cross-reference map is stale (content modified 5 minutes ago, map generated 1 hour ago)
   Run: .specify/scripts/generate-xref-report.sh

❌ Found 2 cross-reference issues
```

**Common Issues**:

| Issue                     | Fix                                                     |
| ------------------------- | ------------------------------------------------------- |
| Section ID not found      | Check GDD file for correct section heading with `{#id}` |
| File path incorrect       | Use relative path from `docs/design/` root              |
| Missing `gdd_ref`         | Add frontmatter with link to authoritative GDD section  |
| Stale cross-reference map | Run `generate-xref-report.sh` to regenerate             |

**Section ID Format**:

```markdown
## 2. Specialization System {#specialization-system}

Frontmatter reference:
gdd_ref: systems/class-system/index.md#specialization-system
```

---

### 3. Frontmatter Schema Validation

**Script**: `.specify/scripts/validate-frontmatter.sh`

**What It Checks**:

- Required fields present: `title`, `gdd_ref`
- Field types correct: string values, no empty fields
- Optional fields valid: `parent` points to existing file, `tree_tier` is 1/2/3
- YAML syntax correct (no parsing errors)

**Usage**:

```bash
# Validate all frontmatter
.specify/scripts/validate-frontmatter.sh

# Validate specific file
.specify/scripts/validate-frontmatter.sh docs/design/content/classes/warrior/index.md
```

**Success Output**:

```
✅ Checking frontmatter in 654 files...
✅ All required fields present
✅ All field types valid
✅ All optional fields valid
✅ No YAML syntax errors
```

**Failure Output**:

```
❌ docs/design/content/classes/warrior/index.md
   Missing required field: gdd_ref

❌ docs/design/content/classes/fighter/index.md
   Invalid tree_tier value: 4
   Must be: 1, 2, or 3

❌ docs/design/content/skills/weapon-mastery/index.md
   YAML syntax error: unexpected character at line 5

❌ Found 3 frontmatter issues
```

**Required Frontmatter Schema**:

```yaml
---
title: Warrior # Required: non-empty string
gdd_ref: systems/class-system/index.md#specialization-classes # Required: valid path#section
parent: classes/fighter/index.md # Optional: path to parent file
tree_tier: 2 # Optional: 1, 2, or 3 (classes only)
---
```

**Common Issues**:

| Issue               | Fix                                                     |
| ------------------- | ------------------------------------------------------- |
| Missing `title`     | Add `title: [Name]` to frontmatter                      |
| Missing `gdd_ref`   | Add link to authoritative GDD section                   |
| Invalid `tree_tier` | Use 1 (Foundation), 2 (Specialization), or 3 (Advanced) |
| YAML syntax error   | Check indentation, quotes, colons                       |

---

### 4. Cross-Reference Map Generation

**Script**: `.specify/scripts/generate-xref-report.sh`

**What It Does**:

- Scans all content files for `gdd_ref` frontmatter
- Groups references by GDD target file and section
- Generates `docs/design/reference/cross-reference/gdd-to-content.md`
- Includes timestamp and statistics

**Usage**:

```bash
# Generate cross-reference map
.specify/scripts/generate-xref-report.sh

# Output location
cat docs/design/reference/cross-reference/gdd-to-content.md
```

**Success Output**:

```
✅ Scanning 654 content files...
✅ Found 127 unique GDD sections referenced
✅ Generated cross-reference map
   Location: docs/design/reference/cross-reference/gdd-to-content.md
   GDD Sections: 127
   Content Files: 654
   Timestamp: 2026-02-10 15:30:00
```

**Generated Map Format**:

```markdown
# GDD to Content Cross-Reference

**Generated**: 2026-02-10 15:30:00
**Content Files Scanned**: 654
**GDD Sections Referenced**: 127

---

## systems/class-system/index.md

### § specialization-system

Referenced by:

- [Warrior](../../content/classes/fighter/warrior/index.md)
- [Blade Dancer](../../content/classes/fighter/warrior/blade-dancer/index.md)
- [Knight](../../content/classes/fighter/warrior/knight/index.md)

### § foundation-classes

Referenced by:

- [Fighter](../../content/classes/fighter/index.md)
- [Crafter](../../content/classes/crafter/index.md)
- [Gatherer](../../content/classes/gatherer/index.md)
```

**When to Run**:

- After adding/modifying content files
- When `validate-cross-references.sh` reports stale map
- Before committing documentation changes (pre-commit hook auto-runs)

---

## Progressive Loading Validation

**Goal**: Agent loads relevant context in <3 file reads

**Manual Test**:

```bash
# Test query: "How does Weapon Mastery work?"

# Read 1: Root index
cat docs/design/index.md
# Expected: Clear navigation to systems/ and content/

# Read 2: Systems index
cat docs/design/systems/index.md
# Expected: Link to skill system with description

# Read 3: Skill system details
cat docs/design/systems/skill-recipe-system-gdd.md
# Expected: Complete Weapon Mastery specification + links to examples

# Result: 3 reads to get complete answer ✅
```

**Automated Test** (future):

```bash
# Test progressive loading efficiency
.specify/scripts/test-progressive-loading.sh

# Measures average file reads for common queries
✅ Average reads per query: 2.8 (target: <3)
✅ Max reads for any query: 5 (target: <6)
```

---

## Pre-Commit Hook Integration

**Automatic Validation**: Pre-commit hook runs validation on modified docs/ files only.

**Configuration**: `.husky/pre-commit` (already configured)

**What Runs**:

```bash
# Only if docs/design/ files are modified:
1. validate-frontmatter.sh (modified files only)
2. validate-cross-references.sh (modified files only)
3. validate-terminology.sh (modified files only)
4. generate-xref-report.sh (if content/ files changed)
```

**Bypass** (emergency only):

```bash
# Skip validation (not recommended)
git commit --no-verify -m "emergency commit"
```

**Expected Behavior**:

```bash
# Clean commit (no issues)
git commit -m "docs: update warrior class"
✅ Validating frontmatter...
✅ Validating cross-references...
✅ Validating terminology...
✅ Regenerating cross-reference map...
[main abc1234] docs: update warrior class
 1 file changed, 10 insertions(+)

# Commit with issues (blocked)
git commit -m "docs: update warrior class"
❌ Validating frontmatter...
   docs/design/content/classes/warrior/index.md: missing gdd_ref
❌ Commit blocked - fix validation issues and try again
```

---

## Success Criteria

Documentation changes are ready to commit when:

- ✅ **Terminology**: Zero conflicts (all consistent terms)
- ✅ **Cross-References**: Zero broken links (all `gdd_ref` valid)
- ✅ **Frontmatter**: Zero schema violations (all required fields present)
- ✅ **Maps**: Cross-reference map is fresh (timestamp > content)
- ✅ **Structure**: Follows `<name>/index.md` pattern
- ✅ **Progressive Loading**: <3 file reads for common queries

---

## Troubleshooting

### Scripts Not Executable

```bash
chmod +x .specify/scripts/*.sh
```

### Script Not Found

```bash
# Ensure you're in repo root
cd /path/to/realms-of-idle

# Check script exists
ls -la .specify/scripts/validate-*.sh
```

### Permission Denied

```bash
# WSL2: Ensure files not on Windows filesystem
pwd
# Should be: /home/username/projects/realms-of-idle
# Not: /mnt/c/Users/...
```

### Validation Taking Too Long

```bash
# Validate specific directory instead of all files
.specify/scripts/validate-terminology.sh docs/design/content/classes/

# Or specific file
.specify/scripts/validate-terminology.sh docs/design/content/classes/warrior/index.md
```

---

## Quick Reference

| Task                     | Command                                         |
| ------------------------ | ----------------------------------------------- |
| Validate terminology     | `.specify/scripts/validate-terminology.sh`      |
| Validate cross-refs      | `.specify/scripts/validate-cross-references.sh` |
| Validate frontmatter     | `.specify/scripts/validate-frontmatter.sh`      |
| Generate xref map        | `.specify/scripts/generate-xref-report.sh`      |
| Run all validations      | `.specify/scripts/validate-all.sh`              |
| Test progressive loading | `cat docs/design/index.md` (manual for now)     |

---

## Next Steps

After validation passes:

1. ✅ Commit changes: `git commit -m "docs: [description]"`
2. ✅ Push to branch: `git push origin 002-doc-migration-rationalization`
3. ✅ Create PR (if ready for review)

---

_Validation guide for 002-doc-migration-rationalization feature_
