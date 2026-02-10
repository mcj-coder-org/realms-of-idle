#!/usr/bin/env bash
# migrate-structure-simple.sh
# Simplified migration: files first, then links
# Part of 002-doc-migration-rationalization

set -euo pipefail

SCRIPT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
REPO_ROOT="$(cd "$SCRIPT_DIR/../.." && pwd)"

# Color output
RED='\033[0;31m'
GREEN='\033[0;32m'
YELLOW='\033[1;33m'
NC='\033[0m'

DOCS_DIR="$REPO_ROOT/docs/design/content"
FILES_MIGRATED=0

echo "=== Phase 1: Migrate Files to <name>/index.md Structure ==="
echo ""

# Find all .md files that are NOT already index.md
while IFS= read -r -d '' file; do
    # Skip if already index.md
    if [[ "$(basename "$file")" == "index.md" ]]; then
        continue
    fi

    filename=$(basename "$file" .md)
    dir=$(dirname "$file")
    new_dir="$dir/$filename"
    new_file="$new_dir/index.md"

    # Skip if already migrated
    if [[ -d "$new_dir" ]]; then
        continue
    fi

    # Migrate
    rel_path="${file#$DOCS_DIR/}"
    echo "Migrating: $rel_path → $(dirname "$rel_path")/$filename/index.md"

    mkdir -p "$new_dir"
    mv "$file" "$new_file"
    ((FILES_MIGRATED++))

done < <(find "$DOCS_DIR" -name "*.md" -type f -print0 2>/dev/null)

echo ""
echo -e "${GREEN}Phase 1 Complete: $FILES_MIGRATED files migrated${NC}"
echo ""
echo "=== Phase 2: Update Links (manual step) ==="
echo "Run the update-links script separately after verifying file migration"
