#!/usr/bin/env python3
"""
audit_source_files.py
Audits remaining unmigrated files from cozy-fantasy-rpg source
Part of 002-doc-migration-rationalization Phase E (T047)
"""

from pathlib import Path
from typing import Set, List, Dict
import re

def get_source_files(source_root: Path) -> Dict[str, Path]:
    """Get all markdown files from source with relative paths as keys"""
    source_design = source_root / "docs" / "design"

    files = {}
    for md_file in source_design.rglob("*.md"):
        rel_path = md_file.relative_to(source_design)
        files[str(rel_path)] = md_file

    return files


def get_target_files(target_root: Path) -> Dict[str, Path]:
    """Get all markdown files from target with relative paths as keys"""
    target_design = target_root / "docs" / "design"

    files = {}
    for md_file in target_design.rglob("*.md"):
        rel_path = md_file.relative_to(target_design)
        files[str(rel_path)] = md_file

    return files


def normalize_path(path_str: str) -> str:
    """Normalize path for comparison (handle index.md restructuring)"""
    # Convert old flat structure to new <name>/index.md structure
    # e.g., "content/classes/fighter.md" -> "content/classes/fighter/index.md"

    if path_str.endswith('/index.md'):
        return path_str

    if path_str.endswith('.md') and not path_str.endswith('index.md'):
        # Could be flat file that's now <name>/index.md
        base = path_str[:-3]  # Remove .md
        return f"{base}/index.md"

    return path_str


def categorize_files(source_files: Dict[str, Path], target_files: Dict[str, Path]) -> Dict[str, List[str]]:
    """Categorize source files as migrated or unmigrated"""

    # Build normalized target paths for quick lookup
    target_normalized = set()
    for target_path in target_files.keys():
        target_normalized.add(target_path)
        # Also add the flat version
        if target_path.endswith('/index.md'):
            flat = target_path.replace('/index.md', '.md')
            target_normalized.add(flat)

    migrated = []
    unmigrated = []

    for source_path, source_file in source_files.items():
        # Check if this file exists in target (either as-is or normalized)
        normalized = normalize_path(source_path)

        if source_path in target_files or normalized in target_normalized:
            migrated.append(source_path)
        else:
            unmigrated.append(source_path)

    return {
        'migrated': sorted(migrated),
        'unmigrated': sorted(unmigrated)
    }


def analyze_unmigrated(unmigrated: List[str], source_root: Path) -> Dict[str, List[Dict[str, str]]]:
    """Analyze unmigrated files and suggest categorization"""

    source_design = source_root / "docs" / "design"

    categories = {
        'systems': [],      # System GDD files
        'content': [],      # Content example files
        'reference': [],    # Reference/index files
        'obsolete': []      # Likely obsolete or duplicate
    }

    for file_path in unmigrated:
        full_path = source_design / file_path

        # Read first few lines to get context
        try:
            with open(full_path, 'r', encoding='utf-8') as f:
                content = f.read(500)  # First 500 chars
        except:
            content = ""

        # Categorize based on path
        if file_path.startswith('systems/'):
            categories['systems'].append({
                'path': file_path,
                'size': full_path.stat().st_size,
                'preview': content[:100].replace('\n', ' ')
            })
        elif file_path.startswith('content/'):
            categories['content'].append({
                'path': file_path,
                'size': full_path.stat().st_size,
                'preview': content[:100].replace('\n', ' ')
            })
        elif file_path.startswith('reference/') or 'index' in file_path.lower():
            categories['reference'].append({
                'path': file_path,
                'size': full_path.stat().st_size,
                'preview': content[:100].replace('\n', ' ')
            })
        else:
            categories['obsolete'].append({
                'path': file_path,
                'size': full_path.stat().st_size,
                'preview': content[:100].replace('\n', ' ')
            })

    return categories


def main():
    # Paths
    source_root = Path("/home/mcjarvis/projects/cozy-fantasy-rpg")
    target_root = Path("/home/mcjarvis/projects/realms-of-idle")

    print("=== Source File Audit ===")
    print(f"Source: {source_root}/docs/design")
    print(f"Target: {target_root}/docs/design")
    print("")

    # Get files
    print("Scanning source files...")
    source_files = get_source_files(source_root)
    print(f"  Found {len(source_files)} source files")

    print("Scanning target files...")
    target_files = get_target_files(target_root)
    print(f"  Found {len(target_files)} target files")
    print("")

    # Categorize
    print("Categorizing files...")
    categorized = categorize_files(source_files, target_files)

    migrated = categorized['migrated']
    unmigrated = categorized['unmigrated']

    print(f"  Migrated: {len(migrated)} files")
    print(f"  Unmigrated: {len(unmigrated)} files")
    print("")

    if unmigrated:
        print("=== Unmigrated Files Analysis ===")
        print("")

        analysis = analyze_unmigrated(unmigrated, source_root)

        for category, files in analysis.items():
            if files:
                print(f"{category.upper()} ({len(files)} files):")
                for file_info in files[:10]:  # Show first 10
                    size_kb = file_info['size'] / 1024
                    print(f"  - {file_info['path']} ({size_kb:.1f} KB)")
                    print(f"    Preview: {file_info['preview'][:80]}...")

                if len(files) > 10:
                    print(f"  ... and {len(files) - 10} more")
                print("")

        # Write full report to file
        output_dir = target_root / "docs" / "design" / "reference" / "migration"
        output_dir.mkdir(parents=True, exist_ok=True)

        audit_file = output_dir / "audit-report.md"
        with open(audit_file, 'w', encoding='utf-8') as f:
            f.write("# Source File Migration Audit\n\n")
            f.write(f"**Generated**: {Path(__file__).name}\n")
            f.write(f"**Feature**: 002-doc-migration-rationalization Phase E\n\n")
            f.write("---\n\n")
            f.write("## Summary\n\n")
            f.write(f"- **Total Source Files**: {len(source_files)}\n")
            f.write(f"- **Already Migrated**: {len(migrated)}\n")
            f.write(f"- **Unmigrated**: {len(unmigrated)}\n\n")
            f.write("---\n\n")
            f.write("## Unmigrated Files\n\n")

            for category, files in analysis.items():
                if files:
                    f.write(f"### {category.title()} ({len(files)} files)\n\n")
                    for file_info in files:
                        f.write(f"#### `{file_info['path']}`\n\n")
                        f.write(f"- **Size**: {file_info['size'] / 1024:.1f} KB\n")
                        f.write(f"- **Preview**: {file_info['preview'][:150]}...\n")
                        f.write(f"- **Recommendation**: [KEEP/ADAPT/DISCARD] - [Rationale needed]\n\n")

            f.write("---\n\n")
            f.write("_Generated audit report for Phase E source migration_\n")

        print(f"Full audit report written to: {audit_file}")
    else:
        print("All source files have been migrated!")


if __name__ == '__main__':
    main()
