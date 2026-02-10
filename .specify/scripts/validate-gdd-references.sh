#!/usr/bin/env bash
# validate-gdd-references.sh
# Validates gdd_ref links in content files point to existing GDD sections
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
REFS_CHECKED=0

echo "=== GDD Reference Validation ==="
echo "Checking gdd_ref links point to existing GDD sections..."
echo ""

# Search paths
DOCS_DIR="$REPO_ROOT/docs"
CONTENT_DIR="$DOCS_DIR/design/content"
SYSTEMS_DIR="$DOCS_DIR/design/systems"

if [[ ! -d "$CONTENT_DIR" ]]; then
    echo -e "${YELLOW}WARNING: Content directory not found at $CONTENT_DIR${NC}"
    echo "Skipping validation."
    exit 0
fi

if [[ ! -d "$SYSTEMS_DIR" ]]; then
    echo -e "${YELLOW}WARNING: Systems directory not found at $SYSTEMS_DIR${NC}"
    echo "Skipping validation."
    exit 0
fi

# Function to extract gdd_ref from frontmatter
extract_gdd_ref() {
    local file="$1"
    awk '/^---$/{flag=!flag; next} flag' "$file" | grep "^gdd_ref:" | sed 's/gdd_ref:[[:space:]]*//;s/["'\''"]//g' || true
}

# Function to validate a gdd_ref
validate_gdd_ref() {
    local content_file="$1"
    local gdd_ref="$2"

    # Parse gdd_ref format: "systems/system-name/index.md#section-id"
    local gdd_path="${gdd_ref%%#*}"  # Everything before #
    local section_id="${gdd_ref##*#}" # Everything after #

    # Construct full path to GDD file
    local full_gdd_path="$DOCS_DIR/design/$gdd_path"

    # Check if GDD file exists
    if [[ ! -f "$full_gdd_path" ]]; then
        echo -e "    ${RED}ERROR: GDD file not found: $gdd_path${NC}"
        return 1
    fi

    # If section ID is same as path (no # in ref), only check file exists
    if [[ "$gdd_path" == "$section_id" ]]; then
        return 0
    fi

    # Check if section exists in GDD file
    if ! grep -q "^## .*{#$section_id}" "$full_gdd_path" && \
       ! grep -q "^### .*{#$section_id}" "$full_gdd_path" && \
       ! grep -q "^#### .*{#$section_id}" "$full_gdd_path"; then
        echo -e "    ${YELLOW}WARNING: Section '#$section_id' not found in $gdd_path${NC}"
        ((WARNINGS++))
        return 0
    fi

    return 0
}

# Find all content files and check their gdd_ref
echo "Scanning content files for gdd_ref links..."
while IFS= read -r -d '' file; do
    # Extract gdd_ref
    gdd_ref=$(extract_gdd_ref "$file")

    if [[ -z "$gdd_ref" ]]; then
        continue  # Skip files without gdd_ref
    fi

    ((REFS_CHECKED++))
    echo "Checking: ${file#$REPO_ROOT/}"
    echo "  → $gdd_ref"

    # Validate the reference
    if ! validate_gdd_ref "$file" "$gdd_ref"; then
        ((ERRORS++))
    else
        echo -e "  ${GREEN}✓ Valid${NC}"
    fi
    echo ""

done < <(find "$CONTENT_DIR" -name "*.md" -type f -print0 2>/dev/null)

# Summary
echo "=== Validation Summary ==="
echo "References checked: $REFS_CHECKED"
echo -e "Errors:             ${RED}$ERRORS${NC}"
echo -e "Warnings:           ${YELLOW}$WARNINGS${NC}"
echo ""

if [[ $ERRORS -gt 0 ]]; then
    echo -e "${RED}FAILED: Found $ERRORS broken gdd_ref links${NC}"
    exit 1
elif [[ $WARNINGS -gt 0 ]]; then
    echo -e "${YELLOW}PASSED with $WARNINGS warnings${NC}"
    exit 0
else
    echo -e "${GREEN}PASSED: All gdd_ref links valid${NC}"
    exit 0
fi
