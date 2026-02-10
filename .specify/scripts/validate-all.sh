#!/usr/bin/env bash
# validate-all.sh
# Master validation script - runs all documentation validators
# Part of 002-doc-migration-rationalization

set -euo pipefail

SCRIPT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"

# Color output
RED='\033[0;31m'
GREEN='\033[0;32m'
YELLOW='\033[1;33m'
BLUE='\033[0;34m'
NC='\033[0m' # No Color

# Track overall results
TOTAL_ERRORS=0
TOTAL_WARNINGS=0
VALIDATORS_RUN=0
VALIDATORS_PASSED=0
VALIDATORS_FAILED=0

echo -e "${BLUE}========================================${NC}"
echo -e "${BLUE}   Documentation Validation Suite      ${NC}"
echo -e "${BLUE}========================================${NC}"
echo ""

# Array of validators to run
validators=(
    "validate-terminology.sh"
    "validate-frontmatter.sh"
    "validate-gdd-references.sh"
    "validate-action-references.sh"
    "validate-navigation-links.sh"
)

# Run each validator
for validator in "${validators[@]}"; do
    validator_path="$SCRIPT_DIR/$validator"

    if [[ ! -f "$validator_path" ]]; then
        echo -e "${YELLOW}WARNING: Validator not found: $validator${NC}"
        echo ""
        continue
    fi

    ((VALIDATORS_RUN++))

    echo -e "${BLUE}Running: $validator${NC}"
    echo "----------------------------------------"

    # Run validator and capture exit code
    if "$validator_path"; then
        ((VALIDATORS_PASSED++))
    else
        exit_code=$?
        if [[ $exit_code -eq 1 ]]; then
            ((VALIDATORS_FAILED++))
            ((TOTAL_ERRORS++))
        fi
    fi

    echo ""
done

# Final summary
echo -e "${BLUE}========================================${NC}"
echo -e "${BLUE}   Validation Summary                  ${NC}"
echo -e "${BLUE}========================================${NC}"
echo ""
echo "Validators run:    $VALIDATORS_RUN"
echo -e "Validators passed: ${GREEN}$VALIDATORS_PASSED${NC}"
echo -e "Validators failed: ${RED}$VALIDATORS_FAILED${NC}"
echo ""

if [[ $VALIDATORS_FAILED -gt 0 ]]; then
    echo -e "${RED}OVERALL: FAILED${NC}"
    echo -e "${RED}$VALIDATORS_FAILED validator(s) reported errors${NC}"
    exit 1
else
    echo -e "${GREEN}OVERALL: PASSED${NC}"
    echo -e "${GREEN}All validators passed successfully${NC}"
    exit 0
fi
