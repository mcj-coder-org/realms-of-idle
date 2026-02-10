#!/usr/bin/env python3
"""
update_links.py
Updates markdown links to point to new <name>/index.md structure
Part of 002-doc-migration-rationalization
"""

import re
import sys
from pathlib import Path

def update_links_in_file(file_path):
    """Update all markdown links in a file to point to /index.md structure"""

    with open(file_path, 'r', encoding='utf-8') as f:
        content = f.read()

    original_content = content

    # Pattern to match markdown links: [text](path.md) or [text](path.md#anchor)
    # But NOT if path already ends with index.md
    def replace_link(match):
        full_match = match.group(0)
        link_path = match.group(1)

        # Skip if already points to index.md
        if link_path.endswith('index.md'):
            return full_match

        # Skip if it's an external link
        if link_path.startswith('http://') or link_path.startswith('https://'):
            return full_match

        # Split path and anchor if present
        if '#' in link_path:
            path_part, anchor = link_path.split('#', 1)
            anchor = '#' + anchor
        else:
            path_part = link_path
            anchor = ''

        # If path ends with .md, convert to /index.md
        if path_part.endswith('.md'):
            new_path = path_part[:-3] + '/index.md' + anchor
            return f']({new_path})'

        return full_match

    # Replace all markdown links
    content = re.sub(r'\]\(([^)]+)\)', replace_link, content)

    # Only write if content changed
    if content != original_content:
        with open(file_path, 'w', encoding='utf-8') as f:
            f.write(content)
        return True

    return False

def main():
    docs_dir = Path(__file__).parent.parent.parent / 'docs' / 'design' / 'content'

    if not docs_dir.exists():
        print(f"Error: docs directory not found: {docs_dir}")
        sys.exit(1)

    print("=== Updating Markdown Links ===")
    print(f"Processing files in: {docs_dir}")
    print()

    updated_count = 0
    total_count = 0

    # Find all markdown files
    for md_file in docs_dir.rglob('*.md'):
        total_count += 1
        if update_links_in_file(md_file):
            rel_path = md_file.relative_to(docs_dir)
            print(f"Updated: {rel_path}")
            updated_count += 1

    print()
    print(f"=== Summary ===")
    print(f"Total files: {total_count}")
    print(f"Updated files: {updated_count}")
    print(f"Links updated successfully!")

if __name__ == '__main__':
    main()
