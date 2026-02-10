#!/usr/bin/env bash
# validate-frontmatter.sh
# Validates frontmatter schema compliance across documentation files
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
FILES_CHECKED=0

echo "=== Frontmatter Validation ==="
echo "Checking documentation frontmatter against schema..."
echo ""

# Search paths
DOCS_DIR="$REPO_ROOT/docs"
if [[ ! -d "$DOCS_DIR" ]]; then
    echo -e "${YELLOW}WARNING: docs directory not found at $DOCS_DIR${NC}"
    echo "Skipping validation."
    exit 0
fi

# Schema path
SCHEMA_FILE="$REPO_ROOT/specs/002-doc-migration-rationalization/frontmatter-schema.md"
if [[ ! -f "$SCHEMA_FILE" ]]; then
    echo -e "${YELLOW}WARNING: Schema file not found at $SCHEMA_FILE${NC}"
    echo "Skipping validation."
    exit 0
fi

echo "Using schema: $SCHEMA_FILE"
echo ""

# Function to extract frontmatter from a markdown file
extract_frontmatter() {
    local file="$1"
    awk '/^---$/{flag=!flag; next} flag' "$file"
}

# Function to validate required fields
validate_required_fields() {
    local file="$1"
    local frontmatter="$2"
    local errors_found=0

    # Required fields for all files
    if ! echo "$frontmatter" | grep -q "^title:"; then
        echo -e "    ${RED}ERROR: Missing required field 'title'${NC}"
        ((errors_found++))
    fi

    if ! echo "$frontmatter" | grep -q "^type:"; then
        echo -e "    ${RED}ERROR: Missing required field 'type'${NC}"
        ((errors_found++))
    fi

    if ! echo "$frontmatter" | grep -q "^summary:"; then
        echo -e "    ${RED}ERROR: Missing required field 'summary'${NC}"
        ((errors_found++))
    fi

    # Extract type for conditional validation
    local doc_type=$(echo "$frontmatter" | grep "^type:" | sed 's/type:[[:space:]]*//;s/["'\''"]//g')

    # Content files require gdd_ref
    if [[ "$doc_type" == "class-detail" || "$doc_type" == "action" || "$doc_type" == "skill" || "$doc_type" == "item-detail" ]]; then
        if ! echo "$frontmatter" | grep -q "^gdd_ref:"; then
            echo -e "    ${YELLOW}WARNING: Content file missing 'gdd_ref' field${NC}"
            ((WARNINGS++))
        fi
    fi

    # Class-specific fields
    if [[ "$doc_type" == "class-detail" ]]; then
        if ! echo "$frontmatter" | grep -q "^tree_tier:"; then
            echo -e "    ${YELLOW}WARNING: Class file missing 'tree_tier' field${NC}"
            ((WARNINGS++))
        fi
    fi

    # Action-specific fields
    if [[ "$doc_type" == "action" ]]; then
        if ! echo "$frontmatter" | grep -q "^domain:"; then
            echo -e "    ${YELLOW}WARNING: Action file missing 'domain' field${NC}"
            ((WARNINGS++))
        fi
        if ! echo "$frontmatter" | grep -q "^base_duration:"; then
            echo -e "    ${YELLOW}WARNING: Action file missing 'base_duration' field${NC}"
            ((WARNINGS++))
        fi
    fi

    return $errors_found
}

# Function to validate type values
validate_type_field() {
    local file="$1"
    local frontmatter="$2"

    local doc_type=$(echo "$frontmatter" | grep "^type:" | sed 's/type:[[:space:]]*//;s/["'\''"]//g')

    # Valid types
    local valid_types=("system-overview" "category-index" "class-detail" "action" "skill" "item-detail")

    local is_valid=0
    for valid_type in "${valid_types[@]}"; do
        if [[ "$doc_type" == "$valid_type" ]]; then
            is_valid=1
            break
        fi
    done

    if [[ $is_valid -eq 0 && -n "$doc_type" ]]; then
        echo -e "    ${RED}ERROR: Invalid type '$doc_type' (must be one of: ${valid_types[*]})${NC}"
        return 1
    fi

    return 0
}

# Find all markdown files with frontmatter
echo "Scanning markdown files..."
while IFS= read -r -d '' file; do
    ((FILES_CHECKED++))

    # Extract frontmatter
    frontmatter=$(extract_frontmatter "$file")

    if [[ -z "$frontmatter" ]]; then
        continue  # Skip files without frontmatter
    fi

    # Validate this file
    local file_errors=0
    echo "Checking: ${file#$REPO_ROOT/}"

    # Required fields
    if ! validate_required_fields "$file" "$frontmatter"; then
        ((file_errors++))
        ((ERRORS++))
    fi

    # Type field validation
    if ! validate_type_field "$file" "$frontmatter"; then
        ((file_errors++))
        ((ERRORS++))
    fi

    if [[ $file_errors -eq 0 ]]; then
        echo -e "  ${GREEN}✓ Valid${NC}"
    fi
    echo ""

done < <(find "$DOCS_DIR" -name "*.md" -type f -print0 2>/dev/null)

# Summary
echo "=== Validation Summary ==="
echo "Files checked: $FILES_CHECKED"
echo -e "Errors:        ${RED}$ERRORS${NC}"
echo -e "Warnings:      ${YELLOW}$WARNINGS${NC}"
echo ""

if [[ $ERRORS -gt 0 ]]; then
    echo -e "${RED}FAILED: Found $ERRORS frontmatter errors${NC}"
    exit 1
elif [[ $WARNINGS -gt 0 ]]; then
    echo -e "${YELLOW}PASSED with $WARNINGS warnings${NC}"
    exit 0
else
    echo -e "${GREEN}PASSED: All frontmatter checks passed${NC}"
    exit 0
fi
