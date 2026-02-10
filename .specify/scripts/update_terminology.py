#!/usr/bin/env python3
"""
update_terminology.py
Updates terminology in content files to match corrected architecture
Part of 002-doc-migration-rationalization Phase D1

Replacements:
    - "dormant classes" → DELETE (entire phrase/concept)
    - "XP split" / "XP configuration" → "automatic XP distribution"
    - "transition to" (class progression) → "unlocks eligibility for"
    - "active slots" / "3 active classes" → DELETE
"""

import re
import sys
from pathlib import Path
from typing import List, Tuple

# Terminology replacement rules
# Format: (search_pattern, replacement, description, is_deletion)
REPLACEMENTS: List[Tuple[str, str, str, bool]] = [
    # Remove deprecated concepts entirely (is_deletion=True)
    (r'\b(dormant class(?:es)?)\b', '', 'Remove dormant class concept', True),
    (r'\b(active (?:class )?slot(?:s)?)\b', '', 'Remove active slot concept', True),
    (r'\b([0-9]+ active class(?:es)?)\b', '', 'Remove X active classes limit', True),
    (r'\b(configured? XP split)\b', '', 'Remove XP split configuration', True),
    (r'\b(player-configured? XP)\b', '', 'Remove player-configured XP', True),
    (r'\b(XP split configuration)\b', '', 'Remove XP split configuration', True),

    # Replace with correct terms (is_deletion=False)
    (r'\bXP split\b', 'automatic XP distribution', 'Update XP distribution term', False),
    (r'\btransition(?:s)? to( a)?( new)?( class)?\b', 'unlock\\1 eligibility for', 'Update class progression term', False),
    (r'\bbecomes? a \b', 'unlocks eligibility for ', 'Update class unlock term', False),
]


def clean_deletions(text: str) -> str:
    """Remove extra whitespace and punctuation after deletions"""
    # Remove multiple spaces
    text = re.sub(r' {2,}', ' ', text)

    # Remove orphaned punctuation
    text = re.sub(r'\s+([.,;:])', r'\1', text)
    text = re.sub(r'([.,;:])\1+', r'\1', text)

    # Remove empty list items
    text = re.sub(r'^[-*]\s*$', '', text, flags=re.MULTILINE)

    # Remove empty lines (more than 2 consecutive newlines)
    text = re.sub(r'\n{3,}', '\n\n', text)

    return text


def update_file(file_path: Path, dry_run: bool = False) -> Tuple[bool, List[str]]:
    """Update terminology in a single file

    Returns: (was_modified, list_of_changes)
    """
    if not file_path.suffix == '.md':
        return False, []

    try:
        with open(file_path, 'r', encoding='utf-8') as f:
            original_content = f.read()

        new_content = original_content
        changes = []

        # Apply each replacement
        for search_pattern, replacement, description, is_deletion in REPLACEMENTS:
            # Count matches before replacement
            matches_before = len(re.findall(search_pattern, new_content, re.IGNORECASE))

            if matches_before > 0:
                if is_deletion:
                    # For deletions, remove entire sentences/phrases
                    # Remove sentences containing the term
                    new_content = re.sub(
                        rf'[^.!?\n]*{search_pattern}[^.!?\n]*[.!?]',
                        '',
                        new_content,
                        flags=re.IGNORECASE
                    )
                    # Remove bullet points containing the term
                    new_content = re.sub(
                        rf'^[-*]\s+.*{search_pattern}.*$',
                        '',
                        new_content,
                        flags=re.IGNORECASE | re.MULTILINE
                    )
                else:
                    # Direct replacement
                    new_content = re.sub(
                        search_pattern,
                        replacement,
                        new_content,
                        flags=re.IGNORECASE
                    )

                # Count matches after replacement
                matches_after = len(re.findall(search_pattern, new_content, re.IGNORECASE))

                if matches_after < matches_before:
                    changes.append(f"{description} ({matches_before - matches_after} occurrences)")

        # Clean up deletions
        if new_content != original_content:
            new_content = clean_deletions(new_content)

        # Check if content actually changed
        if new_content != original_content:
            if not dry_run:
                with open(file_path, 'w', encoding='utf-8') as f:
                    f.write(new_content)
            return True, changes

        return False, []

    except Exception as e:
        print(f"⚠️  Error processing {file_path}: {e}")
        return False, []


def main():
    dry_run = False
    target_dir = None

    # Parse arguments
    args = sys.argv[1:]
    for arg in args:
        if arg == '--dry-run':
            dry_run = True
        else:
            target_dir = arg

    if not target_dir:
        print("Usage: update_terminology.py [--dry-run] <directory>")
        sys.exit(1)

    target_path = Path(target_dir).resolve()
    if not target_path.exists():
        print(f"Error: Directory not found: {target_dir}")
        sys.exit(1)

    # Find repo root
    repo_root = Path(__file__).resolve().parent.parent.parent

    print("=== Terminology Update ===")
    print(f"Target: {target_path}")
    print(f"Dry run: {dry_run}")
    print("")

    updated_count = 0
    total_count = 0

    # Process all markdown files
    for md_file in target_path.rglob("*.md"):
        total_count += 1
        was_modified, changes = update_file(md_file, dry_run)

        if was_modified:
            updated_count += 1
            rel_path = md_file.relative_to(repo_root)

            if dry_run:
                print(f"Would update: {rel_path}")
                for change in changes:
                    print(f"  - {change}")
            else:
                print(f"✓ Updated: {rel_path}")
                for change in changes:
                    print(f"  - {change}")

    print("")
    print("=== Summary ===")
    print(f"Total files: {total_count}")
    if dry_run:
        print(f"Would update: {updated_count} files")
        print("")
        print("Run without --dry-run to apply changes")
    else:
        print(f"Updated: {updated_count} files")
        print("")
        print("Next steps:")
        print("  1. Review changes with git diff")
        print("  2. Run validate-terminology.sh to verify all changes")


if __name__ == '__main__':
    main()
