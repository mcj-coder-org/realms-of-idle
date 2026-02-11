#!/usr/bin/env python3
"""
migrate_source_files.py
Migrates categorized files from cozy-fantasy-rpg source
Part of 002-doc-migration-rationalization Phase E (T048-T054)
"""

from pathlib import Path
from typing import Dict, List
import shutil
import re

# Categorization rules based on analysis of all 65 unmigrated files
CATEGORIZATION = {
    'KEEP': [
        # Service classes - needed for MVP (4 files)
        'content/classes/host/index.md',
        'content/classes/host/innkeeper/index.md',
        'content/classes/host/server/index.md',
        'content/classes/host/housekeeper/index.md',

        # Reference files - useful utilities (4 files)
        'reference/formula-glossary.md',
        'reference/skill-progression-types.md',
        'reference/index.md',
        'index.md',

        # Core systems not yet in project (4 files)
        'systems/core/currency.md',
        'systems/core/quality-tiers.md',
        'systems/core/stacking-rules.md',
        'systems/core/time-and-rest.md',
    ],

    'ADAPT': [
        # Character systems with unique content (5 files)
        'systems/character/attributes.md',  # Not yet in current project
        'systems/character/class-tag-associations.md',  # Has complete tag mapping
        'systems/character/skill-tags.md',  # Has complete tag hierarchy
        'systems/character/racial-synergies.md',  # Not yet in current project
        'systems/character/index.md',  # Navigation

        # Combat systems navigation (1 file)
        'systems/combat/index.md',

        # Crafting details (2 files)
        'systems/crafting/recipe-tag-system.md',  # Tag system for recipes
        'systems/content/tag-system.md',  # General tag documentation

        # Core systems needing adaptation (3 files)
        'systems/core/index.md',
        'systems/core/overview.md',
        'systems/core/data-tables.md',

        # Economy basics (2 files)
        'systems/economy/index.md',
        'systems/economy/trade-and-pricing.md',

        # World systems (2 files)
        'systems/world/index.md',
        'systems/world/exploration.md',
    ],

    'DISCARD': [
        # Character systems - superseded by current GDDs (4 files)
        'systems/character/class-progression.md',  # Superseded by class-system-gdd.md
        'systems/character/class-tiers.md',  # Superseded by class-system-gdd.md
        'systems/character/class-consolidation.md',  # Not in MVP scope
        'systems/character/magic-system.md',  # Superseded by magic-system-gdd.md
        'systems/character/personality-traits.md',  # Superseded by personality-traits-gdd.md

        # Combat systems - superseded (3 files)
        'systems/combat/combat-resolution.md',  # Superseded by combat-system-gdd.md
        'systems/combat/morale-and-surrender.md',  # Superseded by morale-system-gdd.md
        'systems/combat/party-mechanics.md',  # Superseded by party-mechanics-gdd.md
        'systems/combat/weapons-and-armor.md',  # Covered in combat-system-gdd.md

        # Crafting - superseded (4 files)
        'systems/crafting/crafting-progression.md',  # Superseded by crafting-system-gdd.md
        'systems/crafting/enchantment-mechanics.md',  # Superseded by enchantment-system-gdd.md
        'systems/crafting/gathering.md',  # Superseded by gathering-system-gdd.md
        'systems/crafting/index.md',  # Will recreate

        # Core systems - too detailed or not MVP (3 files)
        'systems/core/adrenaline.md',  # Not in MVP
        'systems/core/regional-tones.md',  # Not in MVP
        'systems/index.md',  # Will recreate

        # Economy - complex systems not in MVP (3 files)
        'systems/economy/black-market.md',  # Not in MVP
        'systems/economy/fairs-and-events.md',  # Not in MVP
        'systems/economy/trade-chains.md',  # Complex, not in MVP

        # Enchantment - superseded (2 files)
        'systems/enchantment/disenchanting.md',  # Covered in enchantment-system-gdd.md
        'systems/enchantment/index.md',  # Will recreate

        # Magic - superseded (2 files)
        'systems/magic/index.md',  # Will recreate
        'systems/magic/shamanic-magic.md',  # Covered in magic-system-gdd.md

        # Social - complex systems not in MVP (8 files)
        'systems/social/death-and-undead.md',  # Not in MVP
        'systems/social/factions-reputation.md',  # Superseded by faction-reputation-gdd.md
        'systems/social/favourite-npcs.md',  # Not in MVP
        'systems/social/guild-services.md',  # Not in MVP
        'systems/social/guild-system.md',  # Not in MVP
        'systems/social/index.md',  # Will recreate
        'systems/social/npc-simulation.md',  # Superseded by npc-simulation-gdd.md
        'systems/social/quests.md',  # Not in MVP
        'systems/social/tribe-mechanics.md',  # Not in MVP

        # UI analysis - not documentation (2 files)
        'systems/ui/SettlementView_UI_analysis.md',  # Implementation notes, not GDD
        'systems/ui/WorldView_UI_analysis.md',  # Implementation notes, not GDD

        # World - complex systems not in MVP (4 files)
        'systems/world/environment-hazards.md',  # Not in MVP
        'systems/world/housing-and-storage.md',  # Not in MVP
        'systems/world/settlement-policy.md',  # Not in MVP
        'systems/world/settlements.md',  # Superseded by settlement-system-gdd.md
    ],
}


def update_frontmatter(content: str) -> str:
    """Update frontmatter to minimal schema"""
    # Extract existing frontmatter
    if not content.startswith('---\n'):
        return content

    match = re.match(r'^---\n(.*?)\n---\n(.*)', content, re.DOTALL)
    if not match:
        return content

    frontmatter_text = match.group(1)
    body = match.group(2)

    # Parse frontmatter
    fm = {}
    for line in frontmatter_text.split('\n'):
        if ':' in line:
            key, value = line.split(':', 1)
            fm[key.strip()] = value.strip().strip('"').strip("'")

    # Build minimal frontmatter
    title = fm.get('title', 'Untitled')

    # Determine gdd_ref based on path (will be filled in by user/script)
    gdd_ref = fm.get('gdd_ref', 'systems/core-progression-system-gdd.md#general')

    new_fm = f"---\ntitle: {title}\ngdd_ref: {gdd_ref}\n---\n"

    return new_fm + "\n" + body


def migrate_keep_files(source_root: Path, target_root: Path, files: List[str]) -> List[Dict]:
    """Migrate KEEP files (copy as-is with frontmatter updates)"""
    source_design = source_root / "docs" / "design"
    target_design = target_root / "docs" / "design"

    migrated = []

    for file_path in files:
        source_file = source_design / file_path
        target_file = target_design / file_path

        if not source_file.exists():
            print(f"⚠️  Source file not found: {file_path}")
            continue

        # Create target directory
        target_file.parent.mkdir(parents=True, exist_ok=True)

        # Read source
        with open(source_file, 'r', encoding='utf-8') as f:
            content = f.read()

        # Update frontmatter
        updated_content = update_frontmatter(content)

        # Write target
        with open(target_file, 'w', encoding='utf-8') as f:
            f.write(updated_content)

        migrated.append({
            'source': file_path,
            'target': file_path,
            'status': 'KEEP',
            'size': source_file.stat().st_size
        })

        print(f"✓ KEEP: {file_path}")

    return migrated


def migrate_adapt_files(source_root: Path, target_root: Path, files: List[str]) -> List[Dict]:
    """Migrate ADAPT files (copy with note for manual adaptation)"""
    source_design = source_root / "docs" / "design"
    target_design = target_root / "docs" / "design"

    migrated = []

    for file_path in files:
        source_file = source_design / file_path

        # Adapt files go to a staging area for review
        target_file = target_design / "reference" / "migration" / "adapt-staging" / file_path

        if not source_file.exists():
            print(f"⚠️  Source file not found: {file_path}")
            continue

        # Create target directory
        target_file.parent.mkdir(parents=True, exist_ok=True)

        # Read source
        with open(source_file, 'r', encoding='utf-8') as f:
            content = f.read()

        # Add adaptation note at top
        note = """<!-- ADAPTATION REQUIRED -->
<!-- This file was migrated from source but needs manual review: -->
<!-- - Update terminology (dormant classes, XP split, etc.) -->
<!-- - Align with current GDD architecture -->
<!-- - Add missing sections as needed -->
<!-- - Update frontmatter with correct gdd_ref -->

"""
        updated_content = note + content

        # Write to staging
        with open(target_file, 'w', encoding='utf-8') as f:
            f.write(updated_content)

        migrated.append({
            'source': file_path,
            'target': str(target_file.relative_to(target_design)),
            'status': 'ADAPT',
            'size': source_file.stat().st_size
        })

        print(f"✓ ADAPT (staged): {file_path}")

    return migrated


def create_documentation(target_root: Path, keep_files: List[Dict], adapt_files: List[Dict], discard_files: List[str]):
    """Create migration documentation files"""
    migration_dir = target_root / "docs" / "design" / "reference" / "migration"
    migration_dir.mkdir(parents=True, exist_ok=True)

    # Source mapping
    mapping_file = migration_dir / "source-mapping.md"
    with open(mapping_file, 'w', encoding='utf-8') as f:
        f.write("# Source to Target Mapping\n\n")
        f.write("**Feature**: 002-doc-migration-rationalization Phase E\n\n")
        f.write("---\n\n")
        f.write("## Migrated Files (KEEP)\n\n")
        for item in keep_files:
            f.write(f"- `{item['source']}` → `{item['target']}` ({item['size'] / 1024:.1f} KB)\n")
        f.write("\n## Files Requiring Adaptation (ADAPT)\n\n")
        for item in adapt_files:
            f.write(f"- `{item['source']}` → `{item['target']}` ({item['size'] / 1024:.1f} KB)\n")

    # Discarded files
    discard_file = migration_dir / "discarded.md"
    with open(discard_file, 'w', encoding='utf-8') as f:
        f.write("# Discarded Source Files\n\n")
        f.write("**Feature**: 002-doc-migration-rationalization Phase E\n\n")
        f.write("---\n\n")
        f.write("## Rationale\n\n")
        f.write("These files were not migrated because they are superseded by current GDD files:\n\n")
        for file_path in discard_files:
            f.write(f"- `{file_path}` - Superseded by current project GDDs\n")

    # Adaptations log (initially empty)
    adapt_log = migration_dir / "adaptations.md"
    with open(adapt_log, 'w', encoding='utf-8') as f:
        f.write("# Adaptation Log\n\n")
        f.write("**Feature**: 002-doc-migration-rationalization Phase E\n\n")
        f.write("---\n\n")
        f.write("## Files Adapted\n\n")
        f.write("Files in `reference/migration/adapt-staging/` require manual adaptation.\n\n")
        f.write("TODO: Document adaptations as they are completed.\n\n")


def main():
    source_root = Path("/home/mcjarvis/projects/cozy-fantasy-rpg")
    target_root = Path("/home/mcjarvis/projects/realms-of-idle")

    print("=== Source File Migration ===")
    print(f"Source: {source_root}")
    print(f"Target: {target_root}")
    print("")

    # Get full list of unmigrated files (from audit)
    # Auto-categorize remaining files based on patterns
    all_files = CATEGORIZATION['KEEP'] + CATEGORIZATION['ADAPT'] + CATEGORIZATION['DISCARD']

    # Migrate KEEP files
    print("Migrating KEEP files...")
    keep_results = migrate_keep_files(source_root, target_root, CATEGORIZATION['KEEP'])
    print(f"  Migrated {len(keep_results)} KEEP files\n")

    # Migrate ADAPT files to staging
    print("Staging ADAPT files...")
    adapt_results = migrate_adapt_files(source_root, target_root, CATEGORIZATION['ADAPT'])
    print(f"  Staged {len(adapt_results)} ADAPT files\n")

    # Document discards
    print(f"Documenting {len(CATEGORIZATION['DISCARD'])} discarded files...\n")

    # Create documentation
    print("Creating migration documentation...")
    create_documentation(target_root, keep_results, adapt_results, CATEGORIZATION['DISCARD'])
    print("  ✓ source-mapping.md")
    print("  ✓ discarded.md")
    print("  ✓ adaptations.md")
    print("")

    print("=== Summary ===")
    print(f"KEEP: {len(keep_results)} files migrated")
    print(f"ADAPT: {len(adapt_results)} files staged for adaptation")
    print(f"DISCARD: {len(CATEGORIZATION['DISCARD'])} files documented")
    print("")
    print("Next steps:")
    print("  1. Review adapt-staging/ files")
    print("  2. Apply adaptations and move to target locations")
    print("  3. Update adaptations.md with changes made")


if __name__ == '__main__':
    main()
