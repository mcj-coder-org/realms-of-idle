#!/usr/bin/env bash
# update-migrated-links.sh
# Updates markdown links to point to new <name>/index.md structure
# Part of 002-doc-migration-rationalization

set -euo pipefail

SCRIPT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
REPO_ROOT="$(cd "$SCRIPT_DIR/../.." && pwd)"

# Color output
RED='\033[0;31m'
GREEN='\033[0;32m'
YELLOW='\033[1;33m'
BLUE='\033[0;34m'
NC='\033[0m'

DOCS_DIR="$REPO_ROOT/docs/design/content"
LINKS_UPDATED=0
FILES_PROCESSED=0

echo -e "${BLUE}=== Update Migrated Links ===${NC}"
echo "Updating links to match new <name>/index.md structure"
echo ""

# Parse arguments
DRY_RUN=true
while [[ $# -gt 0 ]]; do
    case $1 in
        --execute)
            DRY_RUN=false
            shift
            ;;
        --help)
            echo "Usage: $0 [--execute]"
            echo ""
            echo "Updates markdown links to point to migrated file locations"
            echo ""
            echo "Options:"
            echo "  --execute    Actually update links (default: dry run)"
            echo "  --help       Show this help message"
            exit 0
            ;;
        *)
            echo "Unknown option: $1"
            exit 1
            ;;
    esac
done

if [[ "$DRY_RUN" == true ]]; then
    echo -e "${YELLOW}DRY RUN MODE - No changes will be made${NC}"
    echo "Use --execute to apply changes"
    echo ""
fi

# Function to update links in a file
update_links_in_file() {
    local file="$1"
    local changes=0

    # Create temporary file
    local temp_file=$(mktemp)

    # Process the file line by line
    while IFS= read -r line; do
        local updated_line="$line"

        # Find all markdown links in this line: [text](path)
        # Use grep to extract all links
        if echo "$line" | grep -qoE '\]\([^)]+\.md[^)]*\)'; then
            # Extract each link and check if it needs updating
            local links=$(echo "$line" | grep -oE '\]\([^)]+\.md[^)]*\)' || true)

            while IFS= read -r link_match; do
                # Extract the path from ](path)
                local link_path=$(echo "$link_match" | sed 's/^][(]//; s/[)]$//')

                # Skip external links (http://, https://)
                if [[ "$link_path" =~ ^https?:// ]]; then
                    continue
                fi

                # Extract path without anchor
                local path_only="${link_path%%#*}"
                local anchor=""
                if [[ "$link_path" == *"#"* ]]; then
                    anchor="#${link_path#*#}"
                fi

                # Resolve the path relative to the current file's directory
                local file_dir=$(dirname "$file")
                local resolved_path

                if [[ "$path_only" == /* ]]; then
                    # Absolute path from repo root
                    resolved_path="$REPO_ROOT/$path_only"
                else
                    # Relative path
                    resolved_path="$file_dir/$path_only"
                fi

                # Normalize the path
                resolved_path=$(realpath -m "$resolved_path" 2>/dev/null || echo "$resolved_path")

                # Check if the file exists
                if [[ ! -f "$resolved_path" ]]; then
                    # File doesn't exist, check if it was migrated
                    local dir_path=$(dirname "$resolved_path")
                    local base_name=$(basename "$resolved_path" .md)
                    local migrated_path="$dir_path/$base_name/index.md"

                    if [[ -f "$migrated_path" ]]; then
                        # File was migrated! Update the link
                        local new_link_path

                        # Calculate relative path from current file to migrated location
                        if [[ "$path_only" == /* ]]; then
                            # Keep absolute, just add /index.md
                            new_link_path="${path_only%.md}/index.md"
                        else
                            # Relative path - update to point to new location
                            new_link_path="${path_only%.md}/index.md"
                        fi

                        # Add anchor back if present
                        new_link_path="$new_link_path$anchor"

                        # Replace in the line
                        local old_full_link="]($link_path)"
                        local new_full_link="]($new_link_path)"
                        updated_line="${updated_line//$old_full_link/$new_full_link}"

                        ((changes++))

                        if [[ "$DRY_RUN" == true ]]; then
                            echo "  Would update: $link_path → $new_link_path"
                        fi
                    fi
                fi
            done <<< "$links"
        fi

        echo "$updated_line" >> "$temp_file"
    done < "$file"

    # If changes were made and not dry run, replace the original file
    if [[ $changes -gt 0 ]]; then
        if [[ "$DRY_RUN" == false ]]; then
            mv "$temp_file" "$file"
        else
            rm "$temp_file"
        fi
    else
        rm "$temp_file"
    fi

    echo "$changes"
}

# Process all markdown files
while IFS= read -r -d '' file; do
    ((FILES_PROCESSED++))

    changes=$(update_links_in_file "$file")

    if [[ $changes -gt 0 ]]; then
        echo "$(basename "$file"): $changes links"
        ((LINKS_UPDATED+=changes))
    fi

done < <(find "$DOCS_DIR" -name "*.md" -type f -print0 2>/dev/null)

echo ""
echo -e "${BLUE}=== Summary ===${NC}"
echo "Files processed: $FILES_PROCESSED"
echo "Links updated:   $LINKS_UPDATED"
echo ""

if [[ "$DRY_RUN" == true ]]; then
    echo -e "${YELLOW}DRY RUN - No changes made${NC}"
    echo "Use --execute to apply changes"
else
    echo -e "${GREEN}Links updated successfully${NC}"
fi
