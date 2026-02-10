#!/usr/bin/env python3
"""
update_frontmatter.py
Updates frontmatter in content files to match minimal schema
Part of 002-doc-migration-rationalization

Usage:
    ./update_frontmatter.py [--dry-run] <directory>

What this script does:
    - Classes: Keep title, convert tier→tree_tier, add gdd_ref + parent
    - Skills: Keep title, add gdd_ref
    - Items/Creatures: Keep title, add gdd_ref
    - Index files: Keep only title
    - Removes: type, category, tags, tracked_actions, unlocks, summary, etc.
"""

import re
import sys
from pathlib import Path
from typing import Optional, Dict

def infer_gdd_ref(file_path: Path, repo_root: Path) -> str:
    """Infer gdd_ref based on file path"""
    try:
        rel_path = file_path.relative_to(repo_root / "docs" / "design" / "content")
    except ValueError:
        return "systems/core-progression-system-gdd.md#progression"

    parts = rel_path.parts

    # Classes
    if parts[0] == "classes":
        # Remove 'index.md' from parts to count directory depth
        dir_parts = parts[:-1] if parts[-1] == 'index.md' else parts
        tier = len(dir_parts) - 1  # Same logic as determine_tree_tier

        if tier == 1:  # classes/fighter
            return "systems/class-system-gdd.md#foundation-classes"
        elif tier == 2:  # classes/fighter/warrior
            return "systems/class-system-gdd.md#specialization-classes"
        elif tier == 3:  # classes/fighter/warrior/knight
            return "systems/class-system-gdd.md#advanced-classes"
        else:
            return "systems/class-system-gdd.md#classes"

    # Skills
    elif parts[0] == "skills":
        if len(parts) > 1:
            if parts[1] == "pools":
                return "systems/skill-recipe-system-gdd.md#skill-pools"
            elif parts[1] == "tiered":
                return "systems/skill-recipe-system-gdd.md#tiered-skills"
            elif parts[1] == "common":
                return "systems/skill-recipe-system-gdd.md#common-skills"
        return "systems/skill-recipe-system-gdd.md#skills"

    # Items
    elif parts[0] == "items":
        if len(parts) > 1:
            if parts[1] == "weapons":
                return "systems/item-system-gdd.md#weapons"
            elif parts[1] == "armor":
                return "systems/item-system-gdd.md#armor"
        return "systems/item-system-gdd.md#items"

    # Creatures
    elif parts[0] == "creatures":
        return "systems/creature-system-gdd.md#creatures"

    # Actions
    elif parts[0] == "actions":
        return "systems/action-system-gdd.md#actions"

    return "systems/core-progression-system-gdd.md#progression"


def infer_parent(file_path: Path, repo_root: Path) -> Optional[str]:
    """Infer parent from directory structure"""
    try:
        rel_path = file_path.relative_to(repo_root / "docs" / "design" / "content")
    except ValueError:
        return None

    parts = rel_path.parts

    # Classes with depth > 2 have parents
    if parts[0] == "classes":
        # parts = ('classes', 'fighter', 'warrior', 'index.md') for Tier 2
        # parts = ('classes', 'fighter', 'warrior', 'knight', 'index.md') for Tier 3

        # Remove 'index.md' from parts to get directory structure
        dir_parts = parts[:-1] if parts[-1] == 'index.md' else parts

        if len(dir_parts) == 2:  # Tier 1: classes/fighter
            return None  # No parent
        elif len(dir_parts) == 3:  # Tier 2: classes/fighter/warrior
            # Parent is classes/fighter/index.md
            return f"classes/{dir_parts[1]}/index.md"
        elif len(dir_parts) == 4:  # Tier 3: classes/fighter/warrior/knight
            # Parent is classes/fighter/warrior/index.md
            return f"classes/{dir_parts[1]}/{dir_parts[2]}/index.md"

    # Skills with tiered structure might have parents
    if parts[0] == "skills" and len(parts) > 1 and parts[1] == "tiered":
        dir_parts = parts[:-1] if parts[-1] == 'index.md' else parts
        if len(dir_parts) > 3:
            # Parent is one level up
            parent_path = Path(*dir_parts[:-1]) / "index.md"
            return str(parent_path)

    return None


def determine_tree_tier(file_path: Path, repo_root: Path) -> Optional[int]:
    """Determine tree_tier from path depth"""
    try:
        rel_path = file_path.relative_to(repo_root / "docs" / "design" / "content")
    except ValueError:
        return None

    parts = rel_path.parts

    # Only for classes
    if parts[0] == "classes":
        # parts = ('classes', 'fighter', 'index.md') for Tier 1
        # parts = ('classes', 'fighter', 'warrior', 'index.md') for Tier 2
        # parts = ('classes', 'fighter', 'warrior', 'knight', 'index.md') for Tier 3

        # Remove 'index.md' from parts to count directory depth
        dir_parts = parts[:-1] if parts[-1] == 'index.md' else parts

        # Tier is: (number of directories - 1)
        # classes/fighter (2 parts) = Tier 1
        # classes/fighter/warrior (3 parts) = Tier 2
        # classes/fighter/warrior/knight (4 parts) = Tier 3
        tier = len(dir_parts) - 1

        if 1 <= tier <= 3:
            return tier

    return None


def extract_frontmatter_and_content(file_path: Path) -> tuple[Optional[Dict], str]:
    """Extract existing frontmatter and content from file"""
    with open(file_path, 'r', encoding='utf-8') as f:
        content = f.read()

    # Check if file has frontmatter
    if not content.startswith('---\n'):
        return None, content

    # Find closing ---
    match = re.match(r'^---\n(.*?)\n---\n(.*)', content, re.DOTALL)
    if not match:
        return None, content

    frontmatter_text = match.group(1)
    body = match.group(2)

    # Parse frontmatter (simple YAML parsing)
    frontmatter = {}
    for line in frontmatter_text.split('\n'):
        if ':' in line:
            key, value = line.split(':', 1)
            key = key.strip()
            value = value.strip().strip('"').strip("'")
            frontmatter[key] = value

    return frontmatter, body


def extract_title(file_path: Path, existing_frontmatter: Optional[Dict], content: str) -> Optional[str]:
    """Extract title from frontmatter or first heading"""
    # Try frontmatter first
    if existing_frontmatter and 'title' in existing_frontmatter:
        return existing_frontmatter['title']

    # Try first heading
    match = re.search(r'^# (.+)$', content, re.MULTILINE)
    if match:
        return match.group(1).strip()

    return None


def is_navigation_index(file_path: Path, repo_root: Path) -> bool:
    """Check if this is a navigation index (category level) vs content file"""
    if file_path.name != "index.md":
        return False

    try:
        rel_path = file_path.relative_to(repo_root / "docs" / "design" / "content")
    except ValueError:
        return False

    # Navigation indexes are at category level: classes/index.md, skills/index.md
    parts = rel_path.parts
    return len(parts) == 2 and parts[1] == "index.md"


def update_file(file_path: Path, repo_root: Path, dry_run: bool = False) -> bool:
    """Update a single file's frontmatter"""
    if not file_path.suffix == '.md':
        return False

    # Extract existing frontmatter and content
    existing_fm, content = extract_frontmatter_and_content(file_path)

    # Get title
    title = extract_title(file_path, existing_fm, content)
    if not title:
        print(f"⚠️  Skipping {file_path.relative_to(repo_root)}: no title found")
        return False

    # Build new frontmatter
    new_fm = {
        'title': title
    }

    # Check if navigation index
    if not is_navigation_index(file_path, repo_root):
        # Content file: add gdd_ref
        new_fm['gdd_ref'] = infer_gdd_ref(file_path, repo_root)

        # Add parent if applicable
        parent = infer_parent(file_path, repo_root)
        if parent:
            new_fm['parent'] = parent

        # Add tree_tier for classes
        tree_tier = determine_tree_tier(file_path, repo_root)
        if tree_tier:
            new_fm['tree_tier'] = tree_tier

    # Build new file content
    new_frontmatter = "---\n"
    new_frontmatter += f"title: {title}\n"

    if 'gdd_ref' in new_fm:
        new_frontmatter += f"gdd_ref: {new_fm['gdd_ref']}\n"

    if 'parent' in new_fm:
        new_frontmatter += f"parent: {new_fm['parent']}\n"

    if 'tree_tier' in new_fm:
        new_frontmatter += f"tree_tier: {new_fm['tree_tier']}\n"

    new_frontmatter += "---\n"

    new_content = new_frontmatter + "\n" + content

    if dry_run:
        print(f"Would update: {file_path.relative_to(repo_root)}")
        print(f"  Title: {title}")
        if 'gdd_ref' in new_fm:
            print(f"  gdd_ref: {new_fm['gdd_ref']}")
        if 'parent' in new_fm:
            print(f"  parent: {new_fm['parent']}")
        if 'tree_tier' in new_fm:
            print(f"  tree_tier: {new_fm['tree_tier']}")
        if is_navigation_index(file_path, repo_root):
            print("  (index file - title only)")
    else:
        with open(file_path, 'w', encoding='utf-8') as f:
            f.write(new_content)
        print(f"✓ Updated: {file_path.relative_to(repo_root)}")

    return True


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
        print("Usage: update_frontmatter.py [--dry-run] <directory>")
        sys.exit(1)

    target_path = Path(target_dir).resolve()
    if not target_path.exists():
        print(f"Error: Directory not found: {target_dir}")
        sys.exit(1)

    # Find repo root (look for .git or RealmsOfIdle.slnx)
    repo_root = Path(__file__).resolve().parent.parent.parent
    while repo_root != repo_root.parent:
        if (repo_root / '.git').exists() or (repo_root / 'RealmsOfIdle.slnx').exists():
            break
        repo_root = repo_root.parent

    print("=== Frontmatter Update ===")
    print(f"Target: {target_path}")
    print(f"Dry run: {dry_run}")
    print("")

    updated_count = 0
    total_count = 0

    # Find and process all markdown files
    for md_file in target_path.rglob("*.md"):
        total_count += 1
        if update_file(md_file, repo_root, dry_run):
            updated_count += 1

    print("")
    print("=== Summary ===")
    print(f"Total files: {total_count}")
    if dry_run:
        print(f"Would update: {updated_count} files")
        print("")
        print("Run without --dry-run to apply changes")
    else:
        print(f"Updated: {updated_count} files")


if __name__ == '__main__':
    main()
