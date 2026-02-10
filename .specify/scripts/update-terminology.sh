#!/usr/bin/env bash
# update-terminology.sh
# Updates terminology in content files to match corrected architecture
# Part of 002-doc-migration-rationalization Phase D1
#
# Replacements:
#   - "dormant classes" → DELETE (entire phrase/concept)
#   - "XP split" / "XP distribution" → "automatic XP distribution"
#   - "transition to" (class progression) → "unlocks eligibility for"
#   - "active slots" / "3 active classes" → DELETE

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

echo "=== Terminology Update ==="
echo "Target: $TARGET_DIR"
echo "Dry run: $DRY_RUN"
echo ""

UPDATED_COUNT=0
TOTAL_COUNT=0

# Terminology replacement patterns
# Format: "search_pattern|replacement|description"
declare -a REPLACEMENTS=(
    # Remove deprecated concepts entirely
    "dormant class(es)?||Remove 'dormant class' concept"
    "active (class )?slot(s)?||Remove 'active slot' concept"
    "[0-9]+ active class(es)?||Remove 'X active classes' limit"
    "configured? XP split||Remove 'XP split configuration'"
    "player-configured? XP||Remove 'player-configured XP'"

    # Replace with correct terms
    "XP split|automatic XP distribution|Update XP distribution term"
    "XP distribution|automatic XP distribution|Clarify XP distribution is automatic"
    "transition(s)? to( a)?( new)?( class)?|unlock(s) eligibility for|Update class progression term"
    "becomes? a |unlock(s) eligibility for |Update class unlock term"

    # Tier disambiguation (flag for manual review)
    "tier ([0-9]+)|TIER_REVIEW:\1|Flag tier references for manual review"
)

# Function to update a single file
update_file() {
    local file="$1"

    # Skip if not a markdown file
    if [[ ! "$file" =~ \.md$ ]]; then
        return
    fi

    ((TOTAL_COUNT++))

    local original_content
    original_content=$(cat "$file")
    local new_content="$original_content"
    local file_modified=false

    # Apply each replacement
    for replacement in "${REPLACEMENTS[@]}"; do
        IFS='|' read -r search replace desc <<< "$replacement"

        # Skip empty search patterns
        if [[ -z "$search" ]]; then
            continue
        fi

        # Apply replacement (case-insensitive)
        local before_count
        before_count=$(echo "$new_content" | grep -ic "$search" || true)

        if [[ $before_count -gt 0 ]]; then
            # For deletions (empty replacement), remove entire sentences/paragraphs
            if [[ -z "$replace" ]]; then
                # Remove entire sentences containing the term
                new_content=$(echo "$new_content" | sed -E "s/[^.!?]*${search}[^.!?]*[.!?]//gi")
                # Remove bullet points containing the term
                new_content=$(echo "$new_content" | sed -E "/[-*].*${search}/Id")
            else
                # Direct replacement
                new_content=$(echo "$new_content" | sed -E "s/${search}/${replace}/gi")
            fi

            local after_count
            after_count=$(echo "$new_content" | grep -ic "$search" || true)

            if [[ $after_count -ne $before_count ]]; then
                file_modified=true
                if [[ "$DRY_RUN" == "true" ]]; then
                    echo "  Would apply: $desc ($before_count occurrences)"
                fi
            fi
        fi
    done

    # Check if content changed
    if [[ "$new_content" != "$original_content" ]]; then
        if [[ "$DRY_RUN" == "true" ]]; then
            echo "Would update: ${file#$REPO_ROOT/}"
        else
            echo "$new_content" > "$file"
            echo "✓ Updated: ${file#$REPO_ROOT/}"
            ((UPDATED_COUNT++))
        fi
    fi
}

# Process all markdown files
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
    echo ""
    echo "Next steps:"
    echo "  1. Review files with 'TIER_REVIEW:' markers for manual disambiguation"
    echo "  2. Run validate-terminology.sh to verify all changes"
fi
