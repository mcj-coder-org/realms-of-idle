#!/usr/bin/env bash
# validate-navigation-links.sh
# Validates all markdown links in navigation tables point to existing files
# Part of 002-doc-migration-rationalization

set -euo pipefail

SCRIPT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
REPO_ROOT="$(cd "$SCRIPT_DIR/../.." && pwd)"

# Color output
RED='\033[0;31m'
GREEN='\033[0;32m'
YELLOW='\033[1;33m'
NC='\033[0m' # No Color

# Counters
ERRORS=0
WARNINGS=0
LINKS_CHECKED=0
FILES_CHECKED=0

echo "=== Navigation Link Validation ==="
echo "Checking all markdown links point to existing files..."
echo ""

# Search paths
DOCS_DIR="$REPO_ROOT/docs"

if [[ ! -d "$DOCS_DIR" ]]; then
    echo -e "${YELLOW}WARNING: docs directory not found at $DOCS_DIR${NC}"
    echo "Skipping validation."
    exit 0
fi

# Function to extract all markdown links from a file
extract_links() {
    local file="$1"
    # Match markdown links: [text](path)
    grep -oP '\[.*?\]\(\K[^)]+' "$file" 2>/dev/null || true
}

# Function to resolve relative path
resolve_link() {
    local source_file="$1"
    local link="$2"

    # Skip external links (http://, https://, mailto:, #anchors-only)
    if [[ "$link" =~ ^https?:// ]] || [[ "$link" =~ ^mailto: ]] || [[ "$link" =~ ^# ]]; then
        return 0
    fi

    # Handle anchor links (#section-id or path#section-id)
    local link_path="${link%%#*}"  # Remove anchor if present
    local anchor="${link##*#}"     # Extract anchor if present

    # If link is only an anchor, skip (internal page reference)
    if [[ -z "$link_path" ]]; then
        return 0
    fi

    # Resolve relative to source file directory
    local source_dir=$(dirname "$source_file")
    local target_path="$source_dir/$link_path"

    # Normalize path
    target_path=$(realpath -m "$target_path" 2>/dev/null || echo "$target_path")

    # Check if target exists
    if [[ ! -f "$target_path" ]] && [[ ! -d "$target_path" ]]; then
        echo -e "    ${RED}ERROR: Broken link: $link${NC}"
        echo -e "           → Target not found: ${target_path#$REPO_ROOT/}"
        return 1
    fi

    # If target is directory, check for index.md
    if [[ -d "$target_path" ]]; then
        if [[ ! -f "$target_path/index.md" ]]; then
            echo -e "    ${YELLOW}WARNING: Directory link missing index.md: $link${NC}"
            echo -e "             → Directory: ${target_path#$REPO_ROOT/}"
            ((WARNINGS++))
        fi
    fi

    return 0
}

# Scan all markdown files
echo "Scanning markdown files for links..."
while IFS= read -r -d '' file; do
    ((FILES_CHECKED++))

    # Extract all links
    links=$(extract_links "$file")

    if [[ -z "$links" ]]; then
        continue  # Skip files with no links
    fi

    file_has_errors=0
    echo "Checking: ${file#$REPO_ROOT/}"

    while IFS= read -r link; do
        ((LINKS_CHECKED++))

        if ! resolve_link "$file" "$link"; then
            ((ERRORS++))
            ((file_has_errors++))
        fi
    done <<< "$links"

    if [[ $file_has_errors -eq 0 ]]; then
        echo -e "  ${GREEN}✓ All links valid${NC}"
    fi
    echo ""

done < <(find "$DOCS_DIR" -name "*.md" -type f -print0 2>/dev/null)

# Summary
echo "=== Validation Summary ==="
echo "Files checked:  $FILES_CHECKED"
echo "Links checked:  $LINKS_CHECKED"
echo -e "Errors:         ${RED}$ERRORS${NC}"
echo -e "Warnings:       ${YELLOW}$WARNINGS${NC}"
echo ""

if [[ $ERRORS -gt 0 ]]; then
    echo -e "${RED}FAILED: Found $ERRORS broken links${NC}"
    exit 1
elif [[ $WARNINGS -gt 0 ]]; then
    echo -e "${YELLOW}PASSED with $WARNINGS warnings${NC}"
    exit 0
else
    echo -e "${GREEN}PASSED: All navigation links valid${NC}"
    exit 0
fi
