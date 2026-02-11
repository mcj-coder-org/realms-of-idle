#!/usr/bin/env python3
"""
generate_actions.py
Generates 37 generic action pages for content/actions/
Part of 002-doc-migration-rationalization Phase D2 (T045-T046)
"""

from pathlib import Path
from typing import Dict, List

# Define the 37 generic actions organized by domain
ACTIONS: Dict[str, List[Dict[str, str]]] = {
    "combat": [
        {"name": "Attack", "desc": "Engage an enemy in combat with weapon or spell", "duration": "2-5", "trigger": "button/hotkey"},
        {"name": "Defend", "desc": "Block, parry, or dodge incoming attacks", "duration": "instant", "trigger": "hotkey/auto"},
        {"name": "Spar", "desc": "Practice combat with friendly target for training", "duration": "10-30", "trigger": "context-menu"},
        {"name": "Scout", "desc": "Reconnoiter area for threats and opportunities", "duration": "30-60", "trigger": "button"},
        {"name": "Ambush", "desc": "Set up hidden attack position", "duration": "10-20", "trigger": "button"},
    ],
    "crafting": [
        {"name": "Forge", "desc": "Shape metal into tools, weapons, or armor", "duration": "30-180", "trigger": "button"},
        {"name": "Smelt", "desc": "Process ore into refined ingots", "duration": "20-60", "trigger": "button"},
        {"name": "Craft", "desc": "Create items from raw or processed materials", "duration": "10-120", "trigger": "button"},
        {"name": "Repair", "desc": "Restore damaged equipment to working condition", "duration": "5-30", "trigger": "context-menu"},
        {"name": "Enchant", "desc": "Imbue items with magical properties", "duration": "60-300", "trigger": "button"},
        {"name": "Brew", "desc": "Create potions, elixirs, or alchemical compounds", "duration": "30-120", "trigger": "button"},
        {"name": "Cook", "desc": "Prepare food and beverages", "duration": "10-60", "trigger": "button"},
        {"name": "Weave", "desc": "Create cloth, rope, or fabric items", "duration": "20-90", "trigger": "button"},
    ],
    "gathering": [
        {"name": "Mine", "desc": "Extract ore and minerals from rock", "duration": "10-30", "trigger": "button"},
        {"name": "Chop", "desc": "Harvest wood from trees", "duration": "10-30", "trigger": "button"},
        {"name": "Harvest", "desc": "Gather crops, herbs, or plants", "duration": "5-20", "trigger": "button"},
        {"name": "Fish", "desc": "Catch fish from water sources", "duration": "30-180", "trigger": "button"},
        {"name": "Hunt", "desc": "Track and kill animals for resources", "duration": "60-300", "trigger": "button"},
        {"name": "Forage", "desc": "Search for wild food and useful items", "duration": "20-60", "trigger": "button"},
        {"name": "Skin", "desc": "Process animal corpses for leather and materials", "duration": "10-30", "trigger": "context-menu"},
        {"name": "Extract", "desc": "Remove specific resources from sources", "duration": "5-15", "trigger": "button"},
        {"name": "Collect", "desc": "Gather scattered items or resources", "duration": "2-10", "trigger": "button"},
    ],
    "service": [
        {"name": "Serve", "desc": "Provide food, drink, or hospitality services", "duration": "10-60", "trigger": "context-menu"},
        {"name": "Clean", "desc": "Maintain cleanliness and order", "duration": "10-30", "trigger": "button"},
        {"name": "Tend", "desc": "Care for animals, plants, or people", "duration": "20-60", "trigger": "button"},
        {"name": "Heal", "desc": "Restore health or cure ailments", "duration": "5-30", "trigger": "button/context-menu"},
        {"name": "Entertain", "desc": "Perform for audience pleasure", "duration": "30-120", "trigger": "button"},
        {"name": "Guide", "desc": "Lead others to destinations or provide information", "duration": "60-300", "trigger": "context-menu"},
    ],
    "trade": [
        {"name": "Buy", "desc": "Purchase goods from merchants", "duration": "5-15", "trigger": "context-menu"},
        {"name": "Sell", "desc": "Offer goods to merchants for coin", "duration": "5-15", "trigger": "context-menu"},
        {"name": "Negotiate", "desc": "Haggle for better prices", "duration": "10-60", "trigger": "button"},
        {"name": "Appraise", "desc": "Determine item value and authenticity", "duration": "5-20", "trigger": "context-menu"},
    ],
    "knowledge": [
        {"name": "Study", "desc": "Read books or documents for information", "duration": "30-180", "trigger": "button"},
        {"name": "Research", "desc": "Investigate topics or solve problems", "duration": "60-300", "trigger": "button"},
        {"name": "Teach", "desc": "Instruct others in skills or knowledge", "duration": "30-120", "trigger": "context-menu"},
        {"name": "Scribe", "desc": "Copy or create written documents", "duration": "20-90", "trigger": "button"},
    ],
    "social": [
        {"name": "Socialize", "desc": "Interact with NPCs to build relationships", "duration": "10-60", "trigger": "context-menu"},
    ],
}


def generate_action_page(domain: str, action_data: Dict[str, str], output_dir: Path):
    """Generate a single action page"""
    name = action_data["name"]
    desc = action_data["desc"]
    duration = action_data["duration"]
    trigger = action_data["trigger"]

    # Create domain directory
    domain_dir = output_dir / domain
    domain_dir.mkdir(parents=True, exist_ok=True)

    # Create action directory
    action_dir = domain_dir / name.lower()
    action_dir.mkdir(parents=True, exist_ok=True)

    # Action file path
    action_file = action_dir / "index.md"

    content = f"""---
title: {name}
gdd_ref: systems/action-system-gdd.md#{domain}-actions
domain: {domain}
ui_trigger: {trigger}
base_duration: {duration} seconds
---

# {name}

**Domain**: {domain.title()}
**UI Trigger**: {trigger.replace('/', ' or ')}
**Base Duration**: {duration} seconds

## Description

{desc}

---

## Context Requirements

What context is needed to perform this action:

| Context Type | Required | Examples           |
| ------------ | -------- | ------------------ |
| Target       | Varies   | Depends on use     |
| Tool/Weapon  | Often    | Appropriate tools  |
| Location     | No       | Any suitable area  |
| Item         | Varies   | Context-dependent  |

---

## Tag Resolution

How context determines which tags fire for XP distribution:

### Base Tag

`{domain}` - Always fires

### Context-Specific Tags

Context determines specific tags based on tools, targets, and methods used.

**Tag Hierarchy**: More specific tags fire alongside parent tags

---

## Modified By Skills

Skills that affect this action's effectiveness:

Skills in the {domain} domain may modify speed, quality, or success rate.

**Note**: Skills modify effectiveness, NOT XP gained. XP is determined by tags only.

---

## Prerequisites

Requirements to perform this action:

- **Minimum Requirements**: None (available to all characters)
- **Resource Requirements**: May consume stamina or resources
- **Location Requirements**: Appropriate context
- **State Requirements**: Varies by context

---

## Failure Conditions

When this action can fail:

| Condition       | Probability | Consequence           |
| --------------- | ----------- | --------------------- |
| Low Skill Level | Higher      | Increased failure     |
| Poor Tools      | Moderate    | Reduced effectiveness |
| Hostile Context | Varies      | Action interrupted    |

**Failure Mitigation**: Skills reduce failure probability

---

## Related Actions

Actions in the {domain} domain often work together or follow sequences.

---

_Generated action page - Part of 002-doc-migration-rationalization Phase D2_
"""

    with open(action_file, 'w', encoding='utf-8') as f:
        f.write(content)

    return action_file


def main():
    # Find repo root
    repo_root = Path(__file__).resolve().parent.parent.parent

    # Output directory
    output_dir = repo_root / "docs" / "design" / "content" / "actions"

    print("=== Generating 37 Generic Action Pages ===")
    print(f"Output directory: {output_dir}")
    print("")

    total_count = 0

    for domain, actions in ACTIONS.items():
        print(f"Domain: {domain} ({len(actions)} actions)")

        for action_data in actions:
            action_file = generate_action_page(domain, action_data, output_dir)
            rel_path = action_file.relative_to(repo_root)
            print(f"  ✓ Created: {rel_path}")
            total_count += 1

    print("")
    print("=== Summary ===")
    print(f"Total actions created: {total_count}")
    print("")
    print("Action breakdown by domain:")
    for domain, actions in ACTIONS.items():
        print(f"  {domain}: {len(actions)} actions")


if __name__ == '__main__':
    main()
