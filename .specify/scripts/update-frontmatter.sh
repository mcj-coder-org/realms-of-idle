#!/usr/bin/env bash
# update-frontmatter.sh
# Updates frontmatter in content files to match minimal schema
# Part of 002-doc-migration-rationalization
#
# Usage:
#   ./update-frontmatter.sh [--dry-run] <directory>
#
# What this script does:
#   - Classes: Keep title, convert tier→tree_tier, add gdd_ref + parent
#   - Skills: Keep title, add gdd_ref
#   - Items/Creatures: Keep title, add gdd_ref
#   - Index files: Keep only title
#   - Removes: type, category, tags, tracked_actions, unlocks, summary, etc.

set -euo pipefail

SCRIPT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
REPO_ROOT="$(cd "$SCRIPT_DIR/../.." && pwd)"

DRY_RUN=false
TARGET_DIR=""

# Parse arguments
while [[ $# -gt 0 ]]; do
    case $1 in
        --dry-run)
            DRY_RUN=true
            shift
            ;;
        *)
            TARGET_DIR="$1"
            shift
            ;;
    esac
done

if [[ -z "$TARGET_DIR" ]]; then
    echo "Usage: $0 [--dry-run] <directory>"
    exit 1
fi

if [[ ! -d "$TARGET_DIR" ]]; then
    echo "Error: Directory not found: $TARGET_DIR"
    exit 1
fi

echo "=== Frontmatter Update ==="
echo "Target: $TARGET_DIR"
echo "Dry run: $DRY_RUN"
echo ""

UPDATED_COUNT=0
TOTAL_COUNT=0

# Function to infer gdd_ref based on file path
infer_gdd_ref() {
    local file="$1"
    local rel_path="${file#$REPO_ROOT/docs/design/content/}"

    # Classes
    if [[ "$rel_path" =~ ^classes/ ]]; then
        # Count directory depth to determine tier
        local depth=$(echo "$rel_path" | tr -cd '/' | wc -c)

        if [[ $depth -eq 2 ]]; then
            # Tier 1: classes/fighter/index.md
            echo "systems/class-system-gdd.md#foundation-classes"
        elif [[ $depth -eq 3 ]]; then
            # Tier 2: classes/fighter/warrior/index.md
            echo "systems/class-system-gdd.md#specialization-classes"
        elif [[ $depth -eq 4 ]]; then
            # Tier 3: classes/fighter/warrior/knight/index.md
            echo "systems/class-system-gdd.md#advanced-classes"
        else
            echo "systems/class-system-gdd.md#classes"
        fi

    # Skills
    elif [[ "$rel_path" =~ ^skills/pools/ ]]; then
        echo "systems/skill-recipe-system-gdd.md#skill-pools"
    elif [[ "$rel_path" =~ ^skills/tiered/ ]]; then
        echo "systems/skill-recipe-system-gdd.md#tiered-skills"
    elif [[ "$rel_path" =~ ^skills/common/ ]]; then
        echo "systems/skill-recipe-system-gdd.md#common-skills"
    elif [[ "$rel_path" =~ ^skills/ ]]; then
        echo "systems/skill-recipe-system-gdd.md#skills"

    # Items
    elif [[ "$rel_path" =~ ^items/weapons/ ]]; then
        echo "systems/item-system-gdd.md#weapons"
    elif [[ "$rel_path" =~ ^items/armor/ ]]; then
        echo "systems/item-system-gdd.md#armor"
    elif [[ "$rel_path" =~ ^items/ ]]; then
        echo "systems/item-system-gdd.md#items"

    # Creatures
    elif [[ "$rel_path" =~ ^creatures/ ]]; then
        echo "systems/creature-system-gdd.md#creatures"

    # Actions
    elif [[ "$rel_path" =~ ^actions/ ]]; then
        echo "systems/action-system-gdd.md#actions"

    else
        echo "systems/core-progression-system-gdd.md#progression"
    fi
}

# Function to infer parent from directory structure
infer_parent() {
    local file="$1"
    local rel_path="${file#$REPO_ROOT/docs/design/content/}"

    # Only classes with depth > 2 have parents
    if [[ "$rel_path" =~ ^classes/ ]]; then
        local depth=$(echo "$rel_path" | tr -cd '/' | wc -c)

        if [[ $depth -eq 3 ]]; then
            # Tier 2: classes/fighter/warrior/index.md → parent: classes/fighter/index.md
            local parent_dir=$(dirname "$(dirname "$rel_path")")
            echo "${parent_dir}/index.md"
        elif [[ $depth -eq 4 ]]; then
            # Tier 3: classes/fighter/warrior/knight/index.md → parent: classes/fighter/warrior/index.md
            local parent_dir=$(dirname "$rel_path")
            echo "${parent_dir}/index.md"
        fi
    fi

    # Skills with tiered structure might have parents
    if [[ "$rel_path" =~ ^skills/tiered/ ]]; then
        local depth=$(echo "$rel_path" | tr -cd '/' | wc -c)
        if [[ $depth -gt 3 ]]; then
            local parent_dir=$(dirname "$rel_path")
            echo "${parent_dir}/index.md"
        fi
    fi
}

# Function to extract title from existing frontmatter or first heading
extract_title() {
    local file="$1"

    # Try to extract from frontmatter first
    if grep -q "^title:" "$file"; then
        grep "^title:" "$file" | head -1 | sed 's/^title: *//' | sed "s/^['\"]//; s/['\"]$//"
    else
        # Extract from first heading
        grep "^# " "$file" | head -1 | sed 's/^# *//'
    fi
}

# Function to extract tier value from existing frontmatter
extract_tier() {
    local file="$1"

    if grep -q "^tier:" "$file"; then
        grep "^tier:" "$file" | head -1 | sed 's/^tier: *//'
    fi
}

# Function to determine tree_tier from path
determine_tree_tier() {
    local file="$1"
    local rel_path="${file#$REPO_ROOT/docs/design/content/}"

    # Only for classes
    if [[ "$rel_path" =~ ^classes/ ]]; then
        local depth=$(echo "$rel_path" | tr -cd '/' | wc -c)

        if [[ $depth -eq 2 ]]; then
            echo "1"
        elif [[ $depth -eq 3 ]]; then
            echo "2"
        elif [[ $depth -eq 4 ]]; then
            echo "3"
        fi
    fi
}

# Function to update a single file
update_file() {
    local file="$1"
    local basename=$(basename "$file")

    # Skip if not a markdown file
    if [[ ! "$file" =~ \.md$ ]]; then
        return
    fi

    ((TOTAL_COUNT++))

    # Determine if this is an index file
    local is_index=false
    if [[ "$basename" == "index.md" ]]; then
        # Check if this is a navigation index or content file
        local rel_path="${file#$REPO_ROOT/docs/design/content/}"
        # Navigation indexes are at category level: classes/index.md, skills/index.md
        if [[ $(echo "$rel_path" | tr -cd '/' | wc -c) -eq 1 ]]; then
            is_index=true
        fi
    fi

    # Extract current values
    local title=$(extract_title "$file")

    if [[ -z "$title" ]]; then
        echo "⚠️  Skipping $file: no title found"
        return
    fi

    # Build new frontmatter
    local new_frontmatter="---\ntitle: $title\n"

    if [[ "$is_index" == "false" ]]; then
        # Content file: add gdd_ref
        local gdd_ref=$(infer_gdd_ref "$file")
        new_frontmatter+="gdd_ref: $gdd_ref\n"

        # Add parent if applicable
        local parent=$(infer_parent "$file")
        if [[ -n "$parent" ]]; then
            new_frontmatter+="parent: $parent\n"
        fi

        # Add tree_tier for classes
        local tree_tier=$(determine_tree_tier "$file")
        if [[ -n "$tree_tier" ]]; then
            new_frontmatter+="tree_tier: $tree_tier\n"
        fi
    fi

    new_frontmatter+="---"

    # Extract content after existing frontmatter
    local content=""
    if grep -q "^---" "$file"; then
        # Has frontmatter, extract content after closing ---
        content=$(sed -n '/^---$/,/^---$/{ /^---$/d; p }' "$file" | tail -n +2)
        content+=$(sed -n '/^---$/{ :a; n; /^---$/q; ba }; /^---$/,$ p' "$file" | tail -n +2)
    else
        # No frontmatter, keep entire content
        content=$(cat "$file")
    fi

    # Create new file content
    local new_content=$(printf "%b\n\n%s" "$new_frontmatter" "$content")

    if [[ "$DRY_RUN" == "true" ]]; then
        echo "Would update: ${file#$REPO_ROOT/}"
        echo "  Title: $title"
        if [[ "$is_index" == "false" ]]; then
            echo "  gdd_ref: $(infer_gdd_ref "$file")"
            local parent=$(infer_parent "$file")
            if [[ -n "$parent" ]]; then
                echo "  parent: $parent"
            fi
            local tree_tier=$(determine_tree_tier "$file")
            if [[ -n "$tree_tier" ]]; then
                echo "  tree_tier: $tree_tier"
            fi
        else
            echo "  (index file - title only)"
        fi
    else
        echo "$new_content" > "$file"
        echo "✓ Updated: ${file#$REPO_ROOT/}"
        ((UPDATED_COUNT++))
    fi
}

# Find and process all markdown files
while IFS= read -r -d '' file; do
    update_file "$file"
done < <(find "$TARGET_DIR" -name "*.md" -type f -print0)

echo ""
echo "=== Summary ==="
echo "Total files: $TOTAL_COUNT"
if [[ "$DRY_RUN" == "true" ]]; then
    echo "Would update: $UPDATED_COUNT files"
    echo ""
    echo "Run without --dry-run to apply changes"
else
    echo "Updated: $UPDATED_COUNT files"
fi
