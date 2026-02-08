---
type: system
scope: detailed
status: authoritative
version: 1.0.0
created: 2026-02-08
updated: 2026-02-08
subjects: [death, permadeath, undead, looting, party-management, idle]
dependencies: []
---

# Death Mechanics - Authoritative Game Design

## Executive Summary

The Death Mechanics system governs what happens when NPCs die in combat or from other causes. Realms of Idle uses NPC permadeath with party management consequences: dead characters are permanently removed, their equipment is recoverable through automatic post-combat looting, and undead enemy types are generated from the lore of the death system. The complex body persistence, decay, and monstrous transformation systems from the predecessor are simplified to immediate outcomes.

**This document resolves:**

- NPC permadeath and party management consequences
- Automatic post-combat looting with strategy settings
- Undead enemy type generation rules
- Simplified death outcomes (no real-time decay)
- Equipment recovery mechanics

**Design Philosophy:** Death is a meaningful consequence that drives party management decisions. Losing an experienced NPC hurts because their accumulated skills, equipment, and trait synergies are lost permanently. This creates tension in combat expedition decisions: push deeper into a dungeon for better loot but risk losing valuable party members. The system avoids the complexity of body persistence and real-time decay in favour of immediate, clear outcomes.

---

## 1. NPC Death

### 1.1 Death Triggers

| Trigger            | Context                        | Recovery Possible |
| ------------------ | ------------------------------ | ----------------- |
| Health reaches 0   | Combat expedition              | No (permadeath)   |
| Expedition failure | Critical failure on expedition | No (permadeath)   |
| Hazard event       | Environmental risk event       | No (permadeath)   |

### 1.2 Death Consequences

When an NPC dies:

```
NPC Health reaches 0
    |
    v
NPC marked as DEAD (permanent)
    |
    v
Equipment enters loot pool
    |
    v
Party slot freed
    |
    v
Any active assignments cancelled
    |
    v
Death notification queued for check-in
```

### 1.3 What Is Lost

| Asset              | On Death                               |
| ------------------ | -------------------------------------- |
| Character          | Permanently removed                    |
| XP and levels      | Lost (not transferable)                |
| Skills and classes | Lost                                   |
| Personality traits | Lost                                   |
| Equipment          | Enters loot pool (see Section 3)       |
| Relationships      | Lost (other NPCs mourn, morale impact) |

### 1.4 Party Management Impact

| Impact             | Effect                                               |
| ------------------ | ---------------------------------------------------- |
| Party slot freed   | Can recruit replacement NPC                          |
| Skill gap          | Lost skills must be rebuilt on new recruit           |
| Morale penalty     | Surviving party members suffer temporary morale loss |
| Equipment recovery | Dead NPC's gear enters loot pool for redistribution  |
| Quest impact       | Active quests requiring dead NPC may fail            |

---

## 2. Death Protection

### 2.1 Flee Threshold

NPCs flee combat based on their Courage trait (see personality-traits-gdd.md):

| Courage Range | Flee Threshold | Behaviour               |
| ------------- | -------------- | ----------------------- |
| 0-15          | 70% HP         | Flees very early        |
| 16-30         | 50% HP         | Flees when wounded      |
| 31-45         | 40% HP         | Cautious retreat        |
| 46-55         | 30% HP         | Standard retreat        |
| 56-70         | 20% HP         | Fights until badly hurt |
| 71-85         | 15% HP         | Very stubborn           |
| 86-100        | Never          | Fights to death         |

### 2.2 Auto-Retreat

Party-level auto-retreat can be configured:

| Setting      | Behaviour                                   |
| ------------ | ------------------------------------------- |
| Conservative | Party retreats when any member below 40% HP |
| Standard     | Party retreats when any member below 25% HP |
| Aggressive   | Party retreats when any member below 10% HP |
| No retreat   | Party fights to the end                     |

Auto-retreat aborts the current expedition stage. Loot collected up to that point is kept. Timer returns party to settlement.

### 2.3 Revival Limitations

There is no revival mechanic. Death is permanent. This is a deliberate design choice:

- Creates meaningful stakes for combat expeditions
- Drives recruitment and training investment
- Prevents death from becoming trivial
- Encourages careful party composition and equipment choices

---

## 3. Looting System

### 3.1 Automatic Post-Combat Looting

Looting is automatic after combat encounters during expeditions. No manual looting is required.

```
Combat encounter resolved
    |
    v
Surviving party collects loot
    |
    v
Loot added to expedition results
    |
    v
Player collects at check-in
```

### 3.2 Loot Strategy Settings

Players configure a loot strategy that applies to all automatic looting:

| Strategy           | Behaviour                        | Best For                  |
| ------------------ | -------------------------------- | ------------------------- |
| Auto-loot all      | Collect everything post-combat   | Early game, maximum value |
| Auto-loot valuable | Only items above value threshold | Mid-game, inventory mgmt  |
| Prioritise weapons | Weapons and armour first         | Equipment upgrades        |
| Prioritise gold    | Currency and gems first          | Economy focus             |

### 3.3 Dead Party Member Equipment

When a party member dies during an expedition:

| Scenario         | Equipment Fate                                  |
| ---------------- | ----------------------------------------------- |
| Party survives   | Dead member's equipment added to loot           |
| Total party wipe | All equipment lost                              |
| Party retreats   | Dead member's equipment recovered if accessible |

### 3.4 Expedition Loot Sources

| Source              | Loot Type                      |
| ------------------- | ------------------------------ |
| Enemy drops         | Equipment, materials, currency |
| Treasure chests     | Currency, rare items           |
| Dead party members  | Their equipped gear            |
| Environmental finds | Materials, consumables         |

---

## 4. Undead Enemy Generation

The death and undead lore from the predecessor is adapted as enemy type generation rules for combat encounters. There is no real-time body persistence or decay simulation.

### 4.1 Undead Types by Tier

| Tier | Base Type | Evolved Form    | Advanced Form |
| ---- | --------- | --------------- | ------------- |
| Low  | Zombie    | Ghoul           | Draugr        |
| Mid  | Skeleton  | Skeleton Knight | Skeleton Lord |
| High | Ghost     | Phantom         | Spectre       |

### 4.2 Special Undead

| Condition                 | Undead Type  | Encounter Context       |
| ------------------------- | ------------ | ----------------------- |
| Magical area + high level | Lich         | Boss encounter, rare    |
| Cursed ground             | Death Knight | Mini-boss, dungeon deep |
| Necromancer presence      | Raised horde | Wave encounter          |

### 4.3 Undead Encounter Rules

Undead encounters are generated based on dungeon/area properties:

| Area Property         | Undead Encounter Chance | Undead Tier |
| --------------------- | ----------------------- | ----------- |
| Ruins                 | High (40%)              | Mid-High    |
| Tomb                  | Very High (60%)         | Mid-High    |
| Cursed ground         | Guaranteed              | High        |
| Necromancer territory | Very High (70%)         | Variable    |
| Forest/plains         | Low (5%)                | Low         |
| Settlement proximity  | Very Low (2%)           | Low         |

### 4.4 Natural vs Raised Undead

| Aspect       | Natural (Encounter)  | Raised (Necromancer)       |
| ------------ | -------------------- | -------------------------- |
| Intelligence | Feral/instinctive    | Varies by necromancer tier |
| Behaviour    | Hostile to all       | Controlled, tactical       |
| Loot         | Standard undead loot | Necromancer + undead loot  |
| Difficulty   | Based on undead tier | Higher (coordinated)       |

---

## 5. Morale Impact of Death

### 5.1 Party Morale Penalty

When a party member dies, surviving members suffer a temporary morale penalty:

| Relationship to Deceased | Morale Penalty | Duration |
| ------------------------ | -------------- | -------- |
| Close friend             | -25%           | 48 hours |
| Friend                   | -15%           | 24 hours |
| Acquaintance             | -10%           | 12 hours |
| Stranger (new recruit)   | -5%            | 6 hours  |

### 5.2 Settlement Morale

NPC deaths also affect settlement morale:

| Death Context      | Settlement Morale Impact |
| ------------------ | ------------------------ |
| Died on expedition | -5% for 24 hours         |
| Multiple deaths    | Stacking penalty         |
| Important NPC died | -15% for 48 hours        |

---

## 6. Replacement and Recovery

### 6.1 NPC Recruitment

After losing a party member, players can recruit replacements:

| Recruitment Source | Timer     | NPC Quality                  |
| ------------------ | --------- | ---------------------------- |
| Settlement tavern  | 15-30 min | Random, level 1              |
| Guild request      | 1-2 hours | Role-specific, level varies  |
| Faction referral   | 2-4 hours | Higher level, trait-biased   |
| Wandering NPCs     | Random    | Variable, check-in discovery |

### 6.2 Training Investment

New recruits start at low level, creating a progression cost for death:

- **Training timer**: New NPCs can be assigned to training queues
- **Skill inheritance**: No skill transfer from dead NPCs
- **Equipment transfer**: Dead NPC's recovered equipment can be given to replacement

---

## 7. Idle Integration

### 7.1 Offline Death Resolution

Deaths during offline expedition processing are queued for notification:

| Event                 | Offline Behaviour                     |
| --------------------- | ------------------------------------- |
| NPC dies in combat    | Death recorded, equipment enters loot |
| Party wipe            | All deaths recorded, expedition fails |
| Auto-retreat triggers | Party returns, surviving members safe |

### 7.2 Check-in Notifications

On check-in after a death event:

- Death notification with cause and context
- Equipment recovery summary
- Morale impact displayed
- Recruitment options immediately available

### 7.3 Progression Hooks

| Hook              | System Integration                    |
| ----------------- | ------------------------------------- |
| Combat system     | Death occurs during combat resolution |
| Party system      | Slot management, morale penalties     |
| Economy system    | Equipment recovery, recruitment costs |
| Morale system     | Death triggers morale penalties       |
| Expedition system | Party wipe ends expedition            |
