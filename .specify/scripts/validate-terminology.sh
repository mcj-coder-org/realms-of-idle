#!/usr/bin/env bash
# validate-terminology.sh
# Validates terminology consistency across documentation files
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

echo "=== Terminology Validation ==="
echo "Checking documentation for deprecated and inconsistent terminology..."
echo ""

# Search paths - docs directory
DOCS_DIR="$REPO_ROOT/docs"
if [[ ! -d "$DOCS_DIR" ]]; then
    echo -e "${YELLOW}WARNING: docs directory not found at $DOCS_DIR${NC}"
    echo "Skipping validation."
    exit 0
fi

# Deprecated terminology checks
echo "Checking for deprecated terminology..."

# 1. Check for "dormant classes" (deprecated)
echo -n "  - Checking for 'dormant classes'... "
DORMANT_HITS=$(grep -r -i "dormant class" "$DOCS_DIR" 2>/dev/null | grep -v "\.git" || true)
if [[ -n "$DORMANT_HITS" ]]; then
    echo -e "${RED}FAIL${NC}"
    echo "    Found deprecated term 'dormant classes':"
    echo "$DORMANT_HITS" | while IFS= read -r line; do
        echo "      $line"
    done
    ((ERRORS++))
else
    echo -e "${GREEN}PASS${NC}"
fi

# 2. Check for "XP splitting" (deprecated)
echo -n "  - Checking for 'XP splitting'... "
XP_SPLIT_HITS=$(grep -r -i "XP split\|split XP\|splitting XP" "$DOCS_DIR" 2>/dev/null | grep -v "\.git" || true)
if [[ -n "$XP_SPLIT_HITS" ]]; then
    echo -e "${RED}FAIL${NC}"
    echo "    Found deprecated term 'XP splitting':"
    echo "$XP_SPLIT_HITS" | while IFS= read -r line; do
        echo "      $line"
    done
    ((ERRORS++))
else
    echo -e "${GREEN}PASS${NC}"
fi

# 3. Check for "active class slots" (deprecated)
echo -n "  - Checking for 'active class slots'... "
ACTIVE_SLOTS_HITS=$(grep -r -i "active class slot\|class slot" "$DOCS_DIR" 2>/dev/null | grep -v "\.git" || true)
if [[ -n "$ACTIVE_SLOTS_HITS" ]]; then
    echo -e "${RED}FAIL${NC}"
    echo "    Found deprecated term 'active class slots':"
    echo "$ACTIVE_SLOTS_HITS" | while IFS= read -r line; do
        echo "      $line"
    done
    ((ERRORS++))
else
    echo -e "${GREEN}PASS${NC}"
fi

# Terminology consistency checks
echo ""
echo "Checking for terminology consistency..."

# 4. Check for ambiguous "tier" usage (should be "class tree tier", "quality tier", or "skill tier")
echo -n "  - Checking for ambiguous 'tier' usage... "
# Look for standalone "tier" without clarifying prefix in frontmatter
TIER_HITS=$(grep -r "^tier:" "$DOCS_DIR" 2>/dev/null | grep -v "\.git" | grep -v "tree_tier:" | grep -v "quality_tier:" | grep -v "skill_tier:" || true)
if [[ -n "$TIER_HITS" ]]; then
    echo -e "${YELLOW}WARNING${NC}"
    echo "    Found ambiguous 'tier' in frontmatter (should be tree_tier, quality_tier, or skill_tier):"
    echo "$TIER_HITS" | while IFS= read -r line; do
        echo "      $line"
    done
    ((WARNINGS++))
else
    echo -e "${GREEN}PASS${NC}"
fi

# 5. Check for "class rank" vs "class tree tier" consistency
echo -n "  - Checking 'class rank' vs 'class tree tier'... "
CLASS_RANK_HITS=$(grep -r -i "class rank" "$DOCS_DIR" 2>/dev/null | grep -v "\.git" || true)
if [[ -n "$CLASS_RANK_HITS" ]]; then
    echo -e "${YELLOW}WARNING${NC}"
    echo "    Found 'class rank' (prefer 'class tree tier' for clarity):"
    echo "$CLASS_RANK_HITS" | while IFS= read -r line; do
        echo "      $line"
    done
    ((WARNINGS++))
else
    echo -e "${GREEN}PASS${NC}"
fi

# 6. Check for class-based bonuses (should be skill-based)
echo -n "  - Checking for class-based bonuses... "
CLASS_BONUS_HITS=$(grep -r -E "class (level )?bonus|class provides.*\+[0-9]+%|level [0-9]+ (grants|provides).*\+[0-9]+%" "$DOCS_DIR" 2>/dev/null | grep -v "\.git" | grep -v "skill" || true)
if [[ -n "$CLASS_BONUS_HITS" ]]; then
    echo -e "${RED}FAIL${NC}"
    echo "    Found class-based bonuses (bonuses should come from skills):"
    echo "$CLASS_BONUS_HITS" | while IFS= read -r line; do
        echo "      $line"
    done
    ((ERRORS++))
else
    echo -e "${GREEN}PASS${NC}"
fi

# Summary
echo ""
echo "=== Validation Summary ==="
echo -e "Errors:   ${RED}$ERRORS${NC}"
echo -e "Warnings: ${YELLOW}$WARNINGS${NC}"
echo ""

if [[ $ERRORS -gt 0 ]]; then
    echo -e "${RED}FAILED: Found $ERRORS terminology errors${NC}"
    exit 1
elif [[ $WARNINGS -gt 0 ]]; then
    echo -e "${YELLOW}PASSED with $WARNINGS warnings${NC}"
    exit 0
else
    echo -e "${GREEN}PASSED: All terminology checks passed${NC}"
    exit 0
fi
