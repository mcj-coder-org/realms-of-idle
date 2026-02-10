#!/usr/bin/env bash
# migrate-to-index-structure.sh
# Restructures documentation from <name>.md to <name>/index.md pattern
# Part of 002-doc-migration-rationalization

set -euo pipefail

SCRIPT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
REPO_ROOT="$(cd "$SCRIPT_DIR/../.." && pwd)"

# Color output
RED='\033[0;31m'
GREEN='\033[0;32m'
YELLOW='\033[1;33m'
BLUE='\033[0;34m'
NC='\033[0m' # No Color

# Counters
FILES_MIGRATED=0
LINKS_UPDATED=0
ERRORS=0

# Dry run mode (default: true for safety)
DRY_RUN=true

# Parse arguments
while [[ $# -gt 0 ]]; do
    case $1 in
        --execute)
            DRY_RUN=false
            shift
            ;;
        --help)
            echo "Usage: $0 [--execute]"
            echo ""
            echo "Migrates markdown files from <name>.md to <name>/index.md structure"
            echo ""
            echo "Options:"
            echo "  --execute    Actually perform migration (default: dry run)"
            echo "  --help       Show this help message"
            echo ""
            echo "Examples:"
            echo "  $0                    # Dry run (preview changes)"
            echo "  $0 --execute          # Execute migration"
            exit 0
            ;;
        *)
            echo "Unknown option: $1"
            echo "Use --help for usage information"
            exit 1
            ;;
    esac
done

if [[ "$DRY_RUN" == true ]]; then
    echo -e "${YELLOW}=== DRY RUN MODE ===${NC}"
    echo "No changes will be made. Use --execute to perform migration."
    echo ""
fi

echo -e "${BLUE}=== Documentation Structure Migration ===${NC}"
echo "Restructuring from <name>.md to <name>/index.md"
echo ""

# Target directory
DOCS_DIR="$REPO_ROOT/docs/design/content"

if [[ ! -d "$DOCS_DIR" ]]; then
    echo -e "${RED}ERROR: Documentation directory not found: $DOCS_DIR${NC}"
    exit 1
fi

# Temporary file for tracking link updates
TEMP_DIR=$(mktemp -d)
trap "rm -rf $TEMP_DIR" EXIT

LINK_MAP_FILE="$TEMP_DIR/link_map.txt"

echo "Phase 1: Scanning files for migration..."
echo ""

# Find all .md files that are NOT already index.md
# Exclude: index.md files, README.md, top-level category indexes
while IFS= read -r -d '' file; do
    # Get relative path from DOCS_DIR
    rel_path="${file#$DOCS_DIR/}"

    # Skip if already index.md
    if [[ "$(basename "$file")" == "index.md" ]]; then
        continue
    fi

    # Get directory and filename
    dir=$(dirname "$file")
    filename=$(basename "$file" .md)

    # New structure: <dir>/<filename>/index.md
    new_dir="$dir/$filename"
    new_file="$new_dir/index.md"

    # Check if target already exists (skip in both dry run and execute mode)
    if [[ -e "$new_dir" ]]; then
        if [[ "$DRY_RUN" == true ]]; then
            echo -e "${YELLOW}SKIP: $rel_path (target directory exists)${NC}"
        fi
        continue
    fi

    # Record this migration for link updates
    old_link_path="${rel_path}"
    new_link_path="$(dirname "$rel_path")/$filename/index.md"
    echo "$old_link_path|$new_link_path" >> "$LINK_MAP_FILE"

    echo -e "${GREEN}MIGRATE:${NC} $rel_path"
    echo -e "      → $(dirname "$rel_path")/$filename/index.md"

    if [[ "$DRY_RUN" == false ]]; then
        # Create directory and move file
        mkdir -p "$new_dir"
        mv "$file" "$new_file"
        ((FILES_MIGRATED++))
    fi

done < <(find "$DOCS_DIR" -name "*.md" -type f -print0 2>/dev/null)

echo ""
echo "Phase 2: Updating links in all markdown files..."
echo ""

# If dry run, count potential updates
if [[ "$DRY_RUN" == true ]]; then
    if [[ -f "$LINK_MAP_FILE" ]]; then
        while IFS='|' read -r old_path new_path; do
            # Search for references to old path
            matches=$(grep -r -l "$old_path" "$DOCS_DIR" 2>/dev/null | wc -l || echo "0")
            if [[ $matches -gt 0 ]]; then
                echo "  Would update links: $old_path → $new_path ($matches files)"
                ((LINKS_UPDATED+=matches))
            fi
        done < "$LINK_MAP_FILE"
    fi
else
    # Execute link updates
    if [[ -f "$LINK_MAP_FILE" ]]; then
        while IFS='|' read -r old_path new_path; do
            # Find all files that reference the old path
            while IFS= read -r -d '' ref_file; do
                # Replace old path with new path in the file (with error handling)
                if sed -i "s|$old_path|$new_path|g" "$ref_file" 2>/dev/null; then
                    echo "  Updated links in: ${ref_file#$DOCS_DIR/}"
                    ((LINKS_UPDATED++))
                else
                    echo -e "  ${YELLOW}Warning: Failed to update links in: ${ref_file#$DOCS_DIR/}${NC}"
                    ((ERRORS++))
                fi
            done < <(grep -r -l "$old_path" "$DOCS_DIR" -print0 2>/dev/null || true)
        done < "$LINK_MAP_FILE"
    fi
fi

# Summary
echo ""
echo -e "${BLUE}=== Migration Summary ===${NC}"
if [[ "$DRY_RUN" == true ]]; then
    echo "Mode: DRY RUN (no changes made)"
else
    echo "Mode: EXECUTED"
fi
echo "Files migrated:  $FILES_MIGRATED"
echo "Links updated:   $LINKS_UPDATED"
echo "Errors:          $ERRORS"
echo ""

if [[ "$DRY_RUN" == true ]]; then
    echo -e "${YELLOW}This was a dry run. Use --execute to perform migration.${NC}"
    exit 0
elif [[ $ERRORS -gt 0 ]]; then
    echo -e "${RED}COMPLETED WITH ERRORS${NC}"
    exit 1
else
    echo -e "${GREEN}MIGRATION COMPLETED SUCCESSFULLY${NC}"
    exit 0
fi
