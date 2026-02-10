#!/usr/bin/env bash
# update-links-fast.sh
# Fast link update using sed pattern matching
# Part of 002-doc-migration-rationalization

set -euo pipefail

SCRIPT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
REPO_ROOT="$(cd "$SCRIPT_DIR/../.." && pwd)"

DOCS_DIR="$REPO_ROOT/docs/design/content"
UPDATED_COUNT=0

echo "=== Fast Link Update ==="
echo "Updating markdown links to point to /index.md structure"
echo ""

# Strategy: For any link ending in .md (but not index.md),
# change it to /index.md

# Find all .md files and update links
while IFS= read -r -d '' file; do
    # Use sed to update links in place
    # Pattern: ](something.md) where something is NOT index
    # Replace with: ](something/index.md)

    # Count changes before and after
    before=$(grep -o '\]([^)]*\.md[^)]*)' "$file" 2>/dev/null | wc -l || echo 0)

    # Update the file
    # Match: ](path/to/file.md) or ](file.md) but NOT ](path/to/index.md)
    # Replace: ](path/to/file/index.md) or ](file/index.md)
    sed -i -E '
        # Skip lines that already point to index.md
        /index\.md/!{
            # Match markdown links ending in .md
            s|\](([^)]*)/([^)/]+)\.md)|\](\1/\3/index.md)|g
            s|\](([^)/]+)\.md)|\](\1/index.md)|g
        }
    ' "$file"

    after=$(grep -o '\]([^)]*\.md[^)]*)' "$file" 2>/dev/null | wc -l || echo 0)

    if [[ $before -ne $after ]]; then
        echo "Updated: ${file#$DOCS_DIR/} ($((before - after)) links)"
        ((UPDATED_COUNT++))
    fi

done < <(find "$DOCS_DIR" -name "*.md" -type f -print0 2>/dev/null)

echo ""
echo "=== Summary ==="
echo "Files with updated links: $UPDATED_COUNT"
echo "Complete!"
