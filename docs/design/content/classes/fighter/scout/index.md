---
title: 'Scout'
type: 'class'
category: 'combat'
tier: 2
prerequisite_xp: 5000
prerequisite_actions: reconnaissance and stealth
summary: 'Reconnaissance specialist gathering intelligence and spotting dangers'
tags:
  - Combat
  - Stealth
---

# Scout

## Lore

### Origin

Information wins battles. Knowing enemy positions, terrain features, and potential hazards allows commanders to make intelligent decisions. Scouts provide this crucial intelligence, venturing ahead of main forces to observe and report. Unlike Warriors who seek combat, Scouts avoid it - their value lies in surviving to deliver information, not engaging enemies.

The Scout's craft emerged from necessity in early warfare. Armies marching blind into unfamiliar territory suffered ambushes, supply failures, and tactical disasters. Leaders realized sending individuals ahead to observe and report prevented catastrophes. Over time, Scouting evolved from dangerous assignment given to expendables into specialized profession requiring specific skills and training.

Modern Scouts blend skills from multiple disciplines. Like Hunters, they track movement and read signs. Like Thieves, they move quietly and remain unseen. Like Warriors, they handle themselves in fights when escape proves impossible. This versatility makes Scouts valuable beyond military applications - they guide caravans, explore wilderness, locate resources, and provide early warning of dangers.

### In the World

Every military force employs Scouts. Armies send them ahead to reconnoiter routes and enemy positions. Navies use them to chart coastlines and spot pirates. City guards deploy them to patrol dangerous districts and gather intelligence on criminal activity. Even merchant caravans hire Scouts to identify safe routes and warn of bandit activity.

Scout work involves more solitude than most combat classes. They operate alone or in small groups, spending days in wilderness or enemy territory with minimal support. This isolation demands self-reliance - Scouts must navigate, survive, and make decisions without guidance. Those who need constant companionship or clear instructions rarely succeed as Scouts.

The job balances contradictions. Scouts must move quickly to cover ground yet carefully to avoid detection. They must observe closely yet remain unseen. They must remember extensive details yet summarize findings concisely. They must judge when gathering more information is worth the risk versus returning with what they've learned. These tensions make Scouting mentally exhausting beyond its physical demands.

Apprentice Scouts learn through mentorship, accompanying experienced Scouts on missions. They practice moving silently through various terrains. They develop observational skills, learning what details matter and what's irrelevant. They memorize landmarks and practice navigation. They drill reporting procedures - how to concisely convey crucial information. Mistakes during training are corrected; mistakes during actual missions often prove fatal.

Veteran Scouts develop almost supernatural awareness of their surroundings. They notice subtle signs others miss - disturbed earth indicating hidden traps, faint sounds revealing concealed enemies, unusual animal behavior suggesting nearby predators. This heightened perception comes from years of survival depending on catching details before details catch you. The best Scouts seem to have extra senses, predicting dangers before they manifest.

---

## Mechanics

### Prerequisites

| Requirement        | Value                                                       |
| ------------------ | ----------------------------------------------------------- |
| XP Threshold       | 5,000 XP from reconnaissance/stealth tracked actions        |
| Related Foundation | [Fighter](../fighter/index.md) (optional, provides bonuses) |
| Tag Depth Access   | 2 levels (e.g., `Combat`, `Stealth/Infiltration`)           |

### Requirements

| Requirement       | Value                                             |
| ----------------- | ------------------------------------------------- |
| Unlock Trigger    | Scout areas, gather intelligence, avoid detection |
| Primary Attribute | AWR (Awareness), FIN (Finesse)                    |
| Starting Level    | 1                                                 |
| Tools Required    | Light armor, navigation tools, survival gear      |

### Stats

#### Base Class Stats

| Level | HP Bonus | Stamina Bonus | Trait            |
| ----- | -------- | ------------- | ---------------- |
| 1     | +5       | +15           | Apprentice Scout |
| 10    | +16      | +45           | Journeyman Scout |
| 25    | +35      | +95           | Master Scout     |
| 50    | +65      | +175          | Legendary Scout  |

#### Combat Bonuses

| Class Level | Evasion Bonus | Detection Range | Stealth Bonus | Speed Bonus |
| ----------- | ------------- | --------------- | ------------- | ----------- |
| 1-9         | +10%          | +25%            | +15%          | +5%         |
| 10-24       | +25%          | +60%            | +35%          | +15%        |
| 25-49       | +50%          | +120%           | +65%          | +30%        |
| 50+         | +85%          | +200%           | +100%         | +50%        |

#### Starting Skills (Auto-Awarded on Class Acceptance)

Skills automatically awarded when accepting this class:

| Skill           | Tier   | Link                                                   | Reasoning                                             |
| --------------- | ------ | ------------------------------------------------------ | ----------------------------------------------------- |
| Track Reader    | Lesser | [See Skill List](../../skills/common/index.md)         | Core scouting ability for tracking and reconnaissance |
| Enhanced Senses | Lesser | [See Skill](../../../skills/tiered/enhanced-senses.md) | Detection and awareness essential for scouts          |

#### Synergy Skills

Skills with strong synergies for Scout:

- [Enhanced Senses](../../skills/index.md) - Lesser/Greater/Enhanced - Extended detection range
- [Silent Movement](../../skills/index.md) - Lesser/Greater/Enhanced - Move with minimal sound
- [Camouflage](../../skills/index.md) - Lesser/Greater/Enhanced - Hide effectively in environments
- [Danger Sense](../../skills/common/danger-sense.md) - Lesser/Greater/Enhanced - Detect ambushes and traps
- [Pathfinder](../../skills/common/pathfinder.md) - Lesser/Greater/Enhanced - Navigate terrain with increased speed
- [Track Reader](../../skills/common/index.md) - Lesser/Greater/Enhanced - Follow tracks of varying ages
- [Keen Eyes](../../skills/index.md) - Lesser/Greater/Enhanced - Spot hidden details and objects
- [Quick Escape](../../skills/index.md) - Lesser/Greater/Enhanced - Disengage from combat successfully
- [Terrain Mastery](../../skills/index.md) - Lesser/Greater/Enhanced - Reduced penalties in difficult terrain
- [Intel Gathering](../../skills/index.md) - Lesser/Greater/Enhanced - Remember observed details perfectly
- [Evasive Maneuvers](../../skills/index.md) - Lesser/Greater/Enhanced - Increased dodge chance
- [Night Vision](../../skills/mechanic-unlock/night-vision.md) - Lesser/Greater/Enhanced - See in darkness
- [Wilderness Survival](../../skills/common/index.md) - Lesser/Greater/Enhanced - Survive independently in wilderness
- [Warning Cry](../../skills/index.md) - Lesser/Greater/Enhanced - Alert allies to danger at range
- [Ghost](../../skills/index.md) - No tiers - Become completely undetectable temporarily

**Note**: All skills listed have strong synergies because they are core scouting skills. Characters without Scout class can still learn these skills but progress at base rate (1x XP) without effectiveness bonuses.

#### Synergy Bonuses

Scout provides context-specific bonuses based on logical specialization:

**Core Reconnaissance Skills** (Direct specialization - Strong synergy):

- **Enhanced Senses**: Essential for detecting threats and opportunities
  - Faster learning: 2x XP from reconnaissance actions (scales with class level)
  - Better effectiveness: +25% detection range at Scout 15, +35% at Scout 30+
  - Reduced cost: -25% stamina drain during observation at Scout 15, -35% at Scout 30+
- **Silent Movement**: Directly tied to stealth reconnaissance
  - Faster learning: 2x XP from moving undetected
  - Better silence: +20% stealth bonus at Scout 15
  - Lower stamina cost: -30% movement stamina at Scout 15
- **Pathfinder**: Core skill for navigation and terrain mastery
  - Faster learning: 2x XP from navigation actions
  - Better speed: +30% movement bonus at Scout 15
  - Lower fatigue: -25% terrain penalty at Scout 15
- **Track Reader**: Fundamental to intelligence gathering
  - Faster learning: 2x XP from tracking actions
  - Better accuracy: +35% track reading success at Scout 15
  - Older tracks: Can follow tracks 50% older at Scout 15

**Synergy Strength Scales with Class Level**:

| Scout Level | XP Multiplier | Effectiveness Bonus | Cost Reduction | Example: Enhanced Senses |
| ----------- | ------------- | ------------------- | -------------- | ------------------------ |
| Level 5     | 1.5x (+50%)   | +15%                | -15% stamina   | Good perception          |
| Level 10    | 1.75x (+75%)  | +20%                | -20% stamina   | Strong awareness         |
| Level 15    | 2.0x (+100%)  | +25%                | -25% stamina   | Excellent detection      |
| Level 30+   | 2.5x (+150%)  | +35%                | -35% stamina   | Supernatural senses      |

#### Tracked Actions

Actions that grant XP to the Scout class:

| Action Category     | Specific Actions                                    | XP Value             |
| ------------------- | --------------------------------------------------- | -------------------- |
| Reconnaissance      | Scout areas, gather intelligence                    | 10-50 per area       |
| Stealth             | Move undetected through dangerous areas             | 5-30 per success     |
| Detection           | Spot enemies, traps, or dangers before they strike  | 10-40 per detection  |
| Tracking            | Follow tracks, locate targets                       | 5-25 per track       |
| Navigation          | Guide parties through difficult terrain             | 10-40 per journey    |
| Evasion             | Avoid or escape from enemies                        | 10-50 per escape     |
| Intelligence Report | Deliver accurate, useful reconnaissance information | 15-60 per report     |
| Ambush Setup        | Position party advantageously for ambush            | 15-50 per ambush     |
| Survival            | Survive alone in wilderness or hostile territory    | 10-40 per day        |
| Critical Intel      | Discover crucial information that changes outcomes  | 50-200 per discovery |

#### Class Consolidation

See [Class Consolidation System](../../../systems/character/class-consolidation.md) for full mechanics.

| Consolidation Path                      | Requirements                                                      | Result Class | Tier |
| --------------------------------------- | ----------------------------------------------------------------- | ------------ | ---- |
| [Ranger](../consolidation/index.md)     | Scout + [Archer](./archer.md) or [Hunter](../gathering/hunter.md) | Ranger       | 1    |
| [Pathfinder](../consolidation/index.md) | Scout + [Explorer](../consolidation/index.md)                     | Pathfinder   | 1    |
| [Tracker](../consolidation/index.md)    | Scout + tracking specialization                                   | Tracker      | 1    |
| [Spy](../consolidation/index.md)        | Scout + urban/social focus                                        | Spy          | 1    |

### Interactions

| System                                                    | Interaction                                          |
| --------------------------------------------------------- | ---------------------------------------------------- |
| [Exploration](../../../systems/world/exploration.md)      | Primary reconnaissance class; discovers areas safely |
| [Combat](../../../systems/combat/combat-resolution.md)    | Avoids direct combat; provides tactical intelligence |
| [Party](../../../systems/combat/party-mechanics.md)       | Advance scout; warns party of dangers                |
| [Stealth](../../../systems/combat/index.md)               | Expert at remaining undetected                       |
| [Navigation](../../../systems/world/index.md)             | Finds paths through difficult terrain                |
| [Survival](../../../systems/world/environment-hazards.md) | Thrives in wilderness independently                  |

---

## Progression

### Consolidations

- [Ranger](../../gatherer/hunter/ranger/) - with [Hunter](../../gatherer/hunter/) (wilderness combat and tracking)

---

## Related Content

- **Requires:** Light armor or clothing, navigation tools, survival equipment
- **Equipment:** [Light Armor](../../items/index.md), [Survival Gear](../../items/index.md), [Navigation Tools](../../items/index.md)
- **Synergy Classes:** [Archer](./archer.md), [Hunter](../gathering/hunter.md), [Adventurer](./adventurer.md)
- **Consolidates To:** [Ranger](../consolidation/index.md), [Pathfinder](../consolidation/index.md), [Tracker](../consolidation/index.md)
- **See Also:** [Exploration System](../../../systems/world/exploration.md), [Stealth Mechanics](../../../systems/combat/index.md), [Survival](../../../systems/world/environment-hazards.md)
