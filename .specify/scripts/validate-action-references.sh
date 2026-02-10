#!/usr/bin/env bash
# validate-action-references.sh
# Validates bidirectional action ↔ class references
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
CLASSES_CHECKED=0
ACTIONS_CHECKED=0

echo "=== Action Reference Validation ==="
echo "Validating bidirectional action ↔ class references..."
echo ""

# Search paths
DOCS_DIR="$REPO_ROOT/docs"
CLASSES_DIR="$DOCS_DIR/design/content/classes"
ACTIONS_DIR="$DOCS_DIR/design/content/actions"

if [[ ! -d "$CLASSES_DIR" ]]; then
    echo -e "${YELLOW}WARNING: Classes directory not found at $CLASSES_DIR${NC}"
    echo "Skipping validation."
    exit 0
fi

if [[ ! -d "$ACTIONS_DIR" ]]; then
    echo -e "${YELLOW}WARNING: Actions directory not found at $ACTIONS_DIR${NC}"
    echo "Skipping validation."
    exit 0
fi

# Temporary files for tracking
TEMP_DIR=$(mktemp -d)
trap "rm -rf $TEMP_DIR" EXIT

CLASS_ACTIONS_FILE="$TEMP_DIR/class_actions.txt"
ACTION_CLASSES_FILE="$TEMP_DIR/action_classes.txt"

# Function to extract action links from class file
extract_class_actions() {
    local class_file="$1"
    # Look for markdown links in Tracked Actions section pointing to action files
    awk '/### Tracked Actions/,/^##[^#]/ {print}' "$class_file" | \
        grep -oP '\[.*?\]\(.*?actions/.*?\.md\)' | \
        sed 's/.*(\(.*\))/\1/' || true
}

# Function to extract tracked_by from action frontmatter
extract_action_classes() {
    local action_file="$1"
    # Extract tracked_by array from frontmatter
    awk '/^---$/,/^---$/ {print}' "$action_file" | \
        awk '/tracked_by:/,/^[a-zA-Z]/ {print}' | \
        grep -v "tracked_by:" | \
        grep -v "^[a-zA-Z]" | \
        sed 's/^[[:space:]]*-[[:space:]]*//;s/["'\''"]//g' || true
}

echo "Phase 1: Scanning class files for action references..."
while IFS= read -r -d '' class_file; do
    ((CLASSES_CHECKED++))

    # Extract relative path for display
    class_rel_path="${class_file#$REPO_ROOT/}"

    # Extract action links
    action_links=$(extract_class_actions "$class_file")

    if [[ -n "$action_links" ]]; then
        echo "  $class_rel_path"
        while IFS= read -r action_link; do
            # Resolve relative path to action file
            action_file_path=$(dirname "$class_file")/$action_link

            # Normalize path
            action_file_path=$(realpath -m "$action_file_path" 2>/dev/null || echo "$action_file_path")

            # Check if action file exists
            if [[ ! -f "$action_file_path" ]]; then
                echo -e "    ${RED}ERROR: Referenced action file not found: $action_link${NC}"
                ((ERRORS++))
            else
                echo "    → $action_link ✓"
                # Record this relationship
                echo "$class_rel_path|$action_file_path" >> "$CLASS_ACTIONS_FILE"
            fi
        done <<< "$action_links"
    fi
done < <(find "$CLASSES_DIR" -name "*.md" -type f -print0 2>/dev/null)

echo ""
echo "Phase 2: Scanning action files for tracked_by..."
while IFS= read -r -d '' action_file; do
    ((ACTIONS_CHECKED++))

    # Extract relative path for display
    action_rel_path="${action_file#$REPO_ROOT/}"

    # Extract tracked_by classes
    tracked_by=$(extract_action_classes "$action_file")

    if [[ -n "$tracked_by" ]]; then
        echo "  $action_rel_path"
        while IFS= read -r class_ref; do
            # Construct full path to class file
            class_file_path="$DOCS_DIR/design/$class_ref"

            # Normalize
            class_file_path=$(realpath -m "$class_file_path" 2>/dev/null || echo "$class_file_path")

            # Check if class file exists
            if [[ ! -f "$class_file_path" ]]; then
                echo -e "    ${RED}ERROR: Referenced class file not found: $class_ref${NC}"
                ((ERRORS++))
            else
                echo "    ← $class_ref ✓"
                # Record this relationship
                echo "$action_rel_path|$class_file_path" >> "$ACTION_CLASSES_FILE"
            fi
        done <<< "$tracked_by"
    fi
done < <(find "$ACTIONS_DIR" -name "*.md" -type f -print0 2>/dev/null)

echo ""
echo "Phase 3: Checking bidirectional consistency..."
# Check for orphaned actions (action exists but no class tracks it)
if [[ -f "$ACTION_CLASSES_FILE" ]]; then
    while IFS= read -r -d '' action_file; do
        action_rel_path="${action_file#$REPO_ROOT/}"

        # Check if this action appears in action_classes mapping
        if ! grep -q "^$action_rel_path|" "$ACTION_CLASSES_FILE" 2>/dev/null; then
            echo -e "  ${YELLOW}WARNING: Orphaned action (no classes track it): $action_rel_path${NC}"
            ((WARNINGS++))
        fi
    done < <(find "$ACTIONS_DIR" -name "*.md" -type f -print0 2>/dev/null)
fi

# Summary
echo ""
echo "=== Validation Summary ==="
echo "Classes checked:  $CLASSES_CHECKED"
echo "Actions checked:  $ACTIONS_CHECKED"
echo -e "Errors:           ${RED}$ERRORS${NC}"
echo -e "Warnings:         ${YELLOW}$WARNINGS${NC}"
echo ""

if [[ $ERRORS -gt 0 ]]; then
    echo -e "${RED}FAILED: Found $ERRORS broken action references${NC}"
    exit 1
elif [[ $WARNINGS -gt 0 ]]; then
    echo -e "${YELLOW}PASSED with $WARNINGS warnings${NC}"
    exit 0
else
    echo -e "${GREEN}PASSED: All action references valid${NC}"
    exit 0
fi
