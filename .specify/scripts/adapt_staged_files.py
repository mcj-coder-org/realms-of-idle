#!/usr/bin/env python3
"""
adapt_staged_files.py
Adapts staged files with terminology updates and proper frontmatter
Part of 002-doc-migration-rationalization Phase E (T051-T052)
"""

from pathlib import Path
import re
from typing import Tuple, List

# Terminology replacements (same as update_terminology.py)
REPLACEMENTS = [
    (r'\b(dormant class(?:es)?)\b', '', True),
    (r'\b(active (?:class )?slot(?:s)?)\b', '', True),
    (r'\b([0-9]+ active class(?:es)?)\b', '', True),
    (r'\b(configured? XP split)\b', '', True),
    (r'\b(player-configured? XP)\b', '', True),
    (r'\b(XP split configuration)\b', '', True),
    (r'\bXP split\b', 'automatic XP distribution', False),
    (r'\btransition(?:s)? to( a)?( new)?( class)?\b', 'unlock\\1 eligibility for', False),
    (r'\bbecomes? a \b', 'unlocks eligibility for ', False),
]


def clean_deletions(text: str) -> str:
    """Remove extra whitespace after deletions"""
    text = re.sub(r' {2,}', ' ', text)
    text = re.sub(r'\s+([.,;:])', r'\1', text)
    text = re.sub(r'([.,;:])\1+', r'\1', text)
    text = re.sub(r'^[-*]\s*$', '', text, flags=re.MULTILINE)
    text = re.sub(r'\n{3,}', '\n\n', text)
    return text


def apply_terminology_updates(content: str) -> Tuple[str, List[str]]:
    """Apply terminology updates and return updated content + list of changes"""
    changes = []
    updated = content

    for search_pattern, replacement, is_deletion in REPLACEMENTS:
        matches_before = len(re.findall(search_pattern, updated, re.IGNORECASE))

        if matches_before > 0:
            if is_deletion:
                updated = re.sub(
                    rf'[^.!?\n]*{search_pattern}[^.!?\n]*[.!?]',
                    '',
                    updated,
                    flags=re.IGNORECASE
                )
                updated = re.sub(
                    rf'^[-*]\s+.*{search_pattern}.*$',
                    '',
                    updated,
                    flags=re.IGNORECASE | re.MULTILINE
                )
            else:
                updated = re.sub(
                    search_pattern,
                    replacement,
                    updated,
                    flags=re.IGNORECASE
                )

            matches_after = len(re.findall(search_pattern, updated, re.IGNORECASE))
            if matches_after < matches_before:
                changes.append(f"Updated terminology ({matches_before - matches_after} occurrences)")

    updated = clean_deletions(updated)
    return updated, changes


def update_frontmatter(content: str, file_path: str) -> str:
    """Update frontmatter to minimal schema with correct gdd_ref"""
    # Remove adaptation note
    content = re.sub(r'^<!-- ADAPTATION REQUIRED -->.*?^<!-- -->\n\n', '', content, flags=re.MULTILINE | re.DOTALL)

    # Extract existing frontmatter
    if not content.startswith('---\n'):
        # No frontmatter, add minimal one
        title = Path(file_path).stem.replace('-', ' ').title()
        gdd_ref = infer_gdd_ref(file_path)
        return f"---\ntitle: {title}\ngdd_ref: {gdd_ref}\n---\n\n{content}"

    match = re.match(r'^---\n(.*?)\n---\n(.*)', content, re.DOTALL)
    if not match:
        return content

    frontmatter_text = match.group(1)
    body = match.group(2)

    # Parse existing frontmatter
    fm = {}
    for line in frontmatter_text.split('\n'):
        if ':' in line:
            key, value = line.split(':', 1)
            fm[key.strip()] = value.strip().strip('"').strip("'")

    # Build minimal frontmatter
    title = fm.get('title', Path(file_path).stem.replace('-', ' ').title())
    gdd_ref = infer_gdd_ref(file_path)

    new_fm = f"---\ntitle: {title}\ngdd_ref: {gdd_ref}\n---\n"

    return new_fm + "\n" + body


def infer_gdd_ref(file_path: str) -> str:
    """Infer appropriate gdd_ref based on file path"""
    # These are system files going to docs/design/systems/
    if 'character/attributes' in file_path:
        return 'systems/core-progression-system-gdd.md#attributes'
    elif 'character/class-tag' in file_path:
        return 'systems/class-system-gdd.md#tag-system'
    elif 'character/skill-tags' in file_path:
        return 'systems/skill-recipe-system-gdd.md#tag-system'
    elif 'character/racial' in file_path:
        return 'systems/core-progression-system-gdd.md#racial-bonuses'
    elif 'character/index' in file_path:
        return 'systems/core-progression-system-gdd.md#overview'
    elif 'combat/index' in file_path:
        return 'systems/combat-system-gdd.md#overview'
    elif 'crafting/recipe' in file_path:
        return 'systems/crafting-system-gdd.md#recipes'
    elif 'content/tag-system' in file_path:
        return 'systems/core-progression-system-gdd.md#tag-system'
    elif 'core/index' in file_path:
        return 'systems/core-progression-system-gdd.md#overview'
    elif 'core/overview' in file_path:
        return 'systems/core-progression-system-gdd.md#overview'
    elif 'core/data-tables' in file_path:
        return 'systems/core-progression-system-gdd.md#data-structures'
    elif 'economy/index' in file_path:
        return 'systems/economy-system-gdd.md#overview'
    elif 'economy/trade' in file_path:
        return 'systems/economy-system-gdd.md#trade'
    elif 'world/index' in file_path:
        return 'systems/exploration-system-gdd.md#overview'
    elif 'world/exploration' in file_path:
        return 'systems/exploration-system-gdd.md#exploration'
    else:
        return 'systems/core-progression-system-gdd.md#general'


def adapt_file(staging_file: Path, target_root: Path) -> dict:
    """Adapt a single file and move to target location"""
    # Read staged file
    with open(staging_file, 'r', encoding='utf-8') as f:
        content = f.read()

    # Apply terminology updates
    updated_content, term_changes = apply_terminology_updates(content)

    # Update frontmatter
    rel_path = str(staging_file.relative_to(target_root / "docs/design/reference/migration/adapt-staging"))
    updated_content = update_frontmatter(updated_content, rel_path)

    # Determine target location
    target_file = target_root / "docs" / "design" / rel_path
    target_file.parent.mkdir(parents=True, exist_ok=True)

    # Write to target
    with open(target_file, 'w', encoding='utf-8') as f:
        f.write(updated_content)

    return {
        'source': str(staging_file.relative_to(target_root)),
        'target': str(target_file.relative_to(target_root)),
        'changes': term_changes,
        'size': staging_file.stat().st_size
    }


def main():
    target_root = Path("/home/mcjarvis/projects/realms-of-idle")
    staging_dir = target_root / "docs" / "design" / "reference" / "migration" / "adapt-staging"

    print("=== Adapting Staged Files ===")
    print(f"Staging directory: {staging_dir}")
    print("")

    if not staging_dir.exists():
        print("No staging directory found!")
        return

    # Find all staged files
    staged_files = list(staging_dir.rglob("*.md"))
    print(f"Found {len(staged_files)} files to adapt")
    print("")

    adapted = []

    for staged_file in staged_files:
        result = adapt_file(staged_file, target_root)
        adapted.append(result)

        print(f"✓ Adapted: {result['source']}")
        print(f"  → {result['target']}")
        if result['changes']:
            for change in result['changes']:
                print(f"    {change}")
        print("")

    # Update adaptations log
    adaptations_file = target_root / "docs" / "design" / "reference" / "migration" / "adaptations.md"
    with open(adaptations_file, 'w', encoding='utf-8') as f:
        f.write("# Adaptation Log\n\n")
        f.write("**Feature**: 002-doc-migration-rationalization Phase E\n\n")
        f.write("---\n\n")
        f.write("## Summary\n\n")
        f.write(f"Total files adapted: {len(adapted)}\n\n")
        f.write("**Adaptations applied:**\n")
        f.write("- Terminology updates (removed deprecated terms)\n")
        f.write("- Frontmatter updated to minimal schema\n")
        f.write("- Files moved from staging to target locations\n\n")
        f.write("---\n\n")
        f.write("## Adapted Files\n\n")

        for item in adapted:
            f.write(f"### `{item['target']}`\n\n")
            f.write(f"- **Source**: `{item['source']}`\n")
            f.write(f"- **Size**: {item['size'] / 1024:.1f} KB\n")
            if item['changes']:
                f.write(f"- **Changes**: {', '.join(item['changes'])}\n")
            else:
                f.write("- **Changes**: Frontmatter updated\n")
            f.write("\n")

    print("=== Summary ===")
    print(f"Successfully adapted {len(adapted)} files")
    print(f"Adaptation log updated: {adaptations_file}")
    print("")
    print("All files moved from staging to final locations")


if __name__ == '__main__':
    main()
