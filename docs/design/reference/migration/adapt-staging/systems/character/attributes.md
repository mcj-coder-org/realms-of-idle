<!-- ADAPTATION REQUIRED -->
<!-- This file was migrated from source but needs manual review: -->
<!-- - Update terminology (dormant classes, XP split, etc.) -->
<!-- - Align with current GDD architecture -->
<!-- - Add missing sections as needed -->
<!-- - Update frontmatter with correct gdd_ref -->

---

title: Attributes System
type: system
summary: Six core attributes (STR, FIN, END, WIT, AWR, CHA) measuring character capability with thresholds, checks, and progression

---

# Attributes System

## 1. Overview

Attributes represent a character's innate capabilities. They affect nearly every action in the game, from combat effectiveness to crafting quality to social interactions. All characters—players and NPCs—use the same attribute system.

| Principle                 | Description                                                |
| ------------------------- | ---------------------------------------------------------- |
| Universal                 | Same 6 attributes for all races and classes                |
| Foundational              | Attributes modify action outcomes, not determine them      |
| Progression               | Grow through class levels and milestones                   |
| Distinct from Personality | Attributes = what you can do; Traits = what you tend to do |

---

## 2. The Six Attributes

| Attribute     | Abbr | Description                                  |
| ------------- | ---- | -------------------------------------------- |
| **Strength**  | STR  | Physical power, raw force, muscular capacity |
| **Finesse**   | FIN  | Precision, agility, coordination, dexterity  |
| **Endurance** | END  | Stamina, toughness, resilience, constitution |
| **Wit**       | WIT  | Mental acuity, learning capacity, reasoning  |
| **Awareness** | AWR  | Perception, alertness, situational reading   |
| **Charm**     | CHA  | Social presence, persuasiveness, leadership  |

---

## 3. Attribute Functions

### Strength (STR)

| Function             | Effect                                  |
| -------------------- | --------------------------------------- |
| Melee damage         | Base damage modifier for melee weapons  |
| Carry capacity       | Maximum inventory weight                |
| Physical labor       | Efficiency at mining, chopping, lifting |
| Intimidation         | Force-based social pressure             |
| Grappling            | Wrestling, restraining, breaking free   |
| Breaking objects     | Doors, locks (brute force), obstacles   |
| Thrown weapons       | Damage (not accuracy)                   |
| Knockback resistance | Resist being pushed/moved               |

#### Strength Thresholds

| STR | Carry Capacity | Melee Modifier |
| --- | -------------- | -------------- |
| 5   | 25 kg          | -15% damage    |
| 8   | 40 kg          | -5% damage     |
| 10  | 50 kg          | Baseline       |
| 12  | 60 kg          | +5% damage     |
| 15  | 75 kg          | +12% damage    |
| 18  | 90 kg          | +20% damage    |
| 20+ | 100+ kg        | +25%+ damage   |

---

### Finesse (FIN)

| Function           | Effect                                  |
| ------------------ | --------------------------------------- |
| Ranged accuracy    | Hit chance with bows, crossbows, thrown |
| Dodge/Evasion      | Avoid incoming attacks                  |
| Stealth            | Move unseen, hide effectively           |
| Lockpicking        | Open locks without keys                 |
| Pickpocketing      | Steal from persons                      |
| Crafting precision | Fine detail work quality                |
| Aimed attacks      | Success rate for called shots           |
| Attack speed       | Slight modifier to attack frequency     |
| Trap disarming     | Safely disable traps                    |

#### Finesse Thresholds

| FIN | Dodge Bonus | Ranged Accuracy |
| --- | ----------- | --------------- |
| 5   | -10%        | -15%            |
| 8   | -3%         | -5%             |
| 10  | Baseline    | Baseline        |
| 12  | +4%         | +5%             |
| 15  | +10%        | +12%            |
| 18  | +16%        | +20%            |
| 20+ | +20%+       | +25%+           |

---

### Endurance (END)

| Function                 | Effect                        |
| ------------------------ | ----------------------------- |
| Health pool              | Maximum health points         |
| Stamina pool             | Maximum stamina points        |
| Stamina recovery         | Base regeneration rate        |
| Health recovery          | Natural healing rate          |
| Poison resistance        | Reduce poison damage/duration |
| Disease resistance       | Resist contracting illness    |
| Environmental resistance | Tolerate temperature extremes |
| Fatigue resistance       | Slower stamina drain          |
| Knockout threshold       | Resist being stunned/KO'd     |

#### Endurance Thresholds

| END | Health Modifier | Stamina Modifier | Resistance |
| --- | --------------- | ---------------- | ---------- |
| 5   | -25%            | -25%             | -15%       |
| 8   | -10%            | -10%             | -5%        |
| 10  | Baseline        | Baseline         | Baseline   |
| 12  | +10%            | +10%             | +5%        |
| 15  | +20%            | +20%             | +12%       |
| 18  | +32%            | +32%             | +20%       |
| 20+ | +40%+           | +40%+            | +25%+      |

---

### Wit (WIT)

| Function                 | Effect                                     |
| ------------------------ | ------------------------------------------ |
| Mana pool                | Maximum mana points (if magic-capable)     |
| Mana regeneration        | Recovery rate                              |
| Learning speed           | XP gain modifier                           |
| Recipe discovery         | Chance to discover through experimentation |
| Investigation            | Find clues, analyze situations             |
| Deception (intellectual) | Construct convincing lies                  |
| Insight                  | Detect lies, read motives                  |
| Appraisal                | Assess item value accurately               |
| Strategy                 | Combat AI priority targeting (NPCs)        |

#### Wit Thresholds

| WIT | Mana Modifier | XP Modifier | Discovery Chance |
| --- | ------------- | ----------- | ---------------- |
| 5   | -25%          | -10%        | -30%             |
| 8   | -10%          | -3%         | -10%             |
| 10  | Baseline      | Baseline    | Baseline         |
| 12  | +10%          | +3%         | +10%             |
| 15  | +20%          | +7%         | +20%             |
| 18  | +32%          | +12%        | +35%             |
| 20+ | +40%+         | +15%+       | +50%+            |

---

### Awareness (AWR)

| Function              | Effect                               |
| --------------------- | ------------------------------------ |
| Detection             | Spot hidden enemies, traps, nodes    |
| Tracking              | Follow trails, interpret signs       |
| Combat initiative     | Act earlier in combat                |
| Ambush prevention     | Chance to avoid surprise             |
| Ambush execution      | Bonus when ambushing others          |
| Ranged defense        | Awareness of incoming projectiles    |
| Environmental reading | Notice weather changes, danger signs |
| Eavesdropping         | Overhear conversations               |
| Search thoroughness   | Find hidden items, compartments      |

#### Awareness Thresholds

| AWR | Detection Modifier | Initiative Modifier |
| --- | ------------------ | ------------------- |
| 5   | -20%               | -15%                |
| 8   | -7%                | -5%                 |
| 10  | Baseline           | Baseline            |
| 12  | +8%                | +5%                 |
| 15  | +18%               | +12%                |
| 18  | +30%               | +20%                |
| 20+ | +40%+              | +25%+               |

---

### Charm (CHA)

| Function              | Effect                          |
| --------------------- | ------------------------------- |
| Trade prices          | Buy low, sell high              |
| Persuasion            | Convince NPCs of positions      |
| Leadership            | Party order compliance          |
| Recruitment           | Attract followers, hire rates   |
| NPC disposition       | Starting attitude modifier      |
| Morale effect         | Inspire allies, intimidate foes |
| Bribery success       | Effectiveness of bribes         |
| Faction standing gain | Bonus to reputation gains       |
| Performance           | Entertainment, distraction      |

#### Charm Thresholds

| CHA | Price Modifier        | Disposition Modifier  |
| --- | --------------------- | --------------------- |
| 5   | +15% buy, -15% sell   | -15 starting opinion  |
| 8   | +5% buy, -5% sell     | -5 starting opinion   |
| 10  | Baseline              | Baseline              |
| 12  | -4% buy, +4% sell     | +5 starting opinion   |
| 15  | -10% buy, +10% sell   | +12 starting opinion  |
| 18  | -16% buy, +16% sell   | +20 starting opinion  |
| 20+ | -20%+ buy, +20%+ sell | +25+ starting opinion |

---

## 4. Attribute Checks

When an action's success is uncertain, an attribute check determines the outcome.

### Check Formula

```
Success Chance = Base Difficulty + (Attribute × Scaling) + Modifiers
```

### Difficulty Levels

| Difficulty        | Base Chance | Example                        |
| ----------------- | ----------- | ------------------------------ |
| Trivial           | 95%         | Lifting a chair                |
| Easy              | 80%         | Climbing a rope                |
| Moderate          | 60%         | Picking a standard lock        |
| Hard              | 40%         | Tracking day-old trail         |
| Very Hard         | 25%         | Spotting invisible creature    |
| Extreme           | 10%         | Lifting a portcullis alone     |
| Nearly Impossible | 2%          | Convincing sworn enemy to help |

### Attribute Scaling

| Attribute Value | Modifier to Base |
| --------------- | ---------------- |
| 5               | -15%             |
| 8               | -5%              |
| 10              | +0%              |
| 12              | +6%              |
| 15              | +15%             |
| 18              | +24%             |
| 20              | +30%             |

### Additional Modifiers

| Source                   | Modifier Range |
| ------------------------ | -------------- |
| Relevant skill           | +5% to +30%    |
| Quality equipment        | +5% to +20%    |
| Environmental conditions | -20% to +10%   |
| Status effects           | -30% to +15%   |
| Assistance               | +10% to +25%   |

### Opposed Checks

When two characters compete:

```
Attacker Roll vs Defender Roll
Higher wins (ties favor defender)

Roll = Base 50 + (Attribute × 3) + Skill Bonus + Modifiers + Random(-10 to +10)
```

| Opposed Check Type   | Attacker Attribute | Defender Attribute |
| -------------------- | ------------------ | ------------------ |
| Grapple              | STR                | STR or FIN         |
| Stealth vs Detection | FIN                | AWR                |
| Pickpocket           | FIN                | AWR                |
| Intimidation         | STR or CHA         | END or CHA         |
| Deception            | WIT + CHA          | WIT + AWR          |
| Persuasion           | CHA                | WIT                |
| Pursuit (foot chase) | END                | END                |

---

## 5. Attribute Progression

### Sources of Attribute Growth

| Source                | Frequency             | Amount                            |
| --------------------- | --------------------- | --------------------------------- |
| Class level-up        | Each level            | +1-2 to class-relevant attributes |
| Total level milestone | Every 10 total levels | +1 to all attributes              |
| Skill passive         | When unlocked         | +1-3 to specific attribute        |
| Equipment             | While worn            | Temporary bonus                   |
| Consumables           | Duration-limited      | Temporary bonus                   |
| Permanent items       | Rare                  | Permanent +1-2                    |
| Training              | Time + cost           | Permanent (limited)               |

### Class Attribute Bonuses

Each class has primary and secondary attributes that gain bonuses on level-up.

| Class Category              | Primary  | Secondary |
| --------------------------- | -------- | --------- |
| Gathering (Miner, etc.)     | END, STR | AWR       |
| Crafting (Blacksmith, etc.) | FIN, WIT | END       |
| Combat (Warrior, etc.)      | STR, END | AWR       |
| Stealth (Thief, etc.)       | FIN, AWR | WIT       |
| Magic (Mage, etc.)          | WIT      | END, AWR  |
| Social (Trader, etc.)       | CHA, WIT | AWR       |
| Hybrid (Ranger, etc.)       | Varies   | Varies    |

### Level-Up Attribute Gains

| Level Type        | Primary Gain    | Secondary Gain  |
| ----------------- | --------------- | --------------- |
| Class level 1-10  | +1 per level    | +1 per 2 levels |
| Class level 11-20 | +1 per level    | +1 per 2 levels |
| Class level 21-30 | +1 per 2 levels | +1 per 3 levels |
| Class level 31+   | +1 per 3 levels | +1 per 4 levels |

### Total Level Milestone Bonus

| Total Levels | Bonus                |
| ------------ | -------------------- |
| 10           | +1 all attributes    |
| 20           | +1 all attributes    |
| 30           | +1 all attributes    |
| 40           | +1 all attributes    |
| 50+          | +1 all per 10 levels |

### Attribute Training

Characters can train attributes outside of leveling:

| Requirement | Description                               |
| ----------- | ----------------------------------------- |
| Trainer     | NPC with higher attribute + Trainer class |
| Time        | Days to weeks per point                   |
| Cost        | Scales with current attribute             |
| Limit       | +3 maximum per attribute via training     |

| Current Attribute | Training Time | Cost       |
| ----------------- | ------------- | ---------- |
| 10-12             | 7 days        | 50 silver  |
| 13-15             | 14 days       | 150 silver |
| 16-18             | 30 days       | 500 silver |
| 19+               | 60 days       | 2 gold     |

---

## 6. Racial Base Attributes

### Settlement Races

| Race       | STR | FIN | END | WIT | CHA | AWR | Total |
| ---------- | --- | --- | --- | --- | --- | --- | ----- |
| Humans     | 10  | 10  | 10  | 10  | 10  | 10  | 60    |
| Elves      | 8   | 12  | 8   | 12  | 11  | 12  | 63    |
| Dwarves    | 12  | 9   | 14  | 10  | 9   | 10  | 64    |
| Lizardfolk | 11  | 10  | 12  | 9   | 7   | 11  | 60    |
| Kobolds    | 8   | 12  | 9   | 11  | 8   | 11  | 59    |

### Tribal Races

| Race    | STR | FIN | END | WIT | CHA | AWR | Total |
| ------- | --- | --- | --- | --- | --- | --- | ----- |
| Gnolls  | 12  | 10  | 12  | 9   | 8   | 12  | 63    |
| Goblins | 7   | 13  | 8   | 11  | 7   | 11  | 57    |
| Orcs    | 14  | 9   | 13  | 8   | 7   | 9   | 60    |
| Ogres   | 16  | 7   | 15  | 6   | 5   | 8   | 57    |
| Trolls  | 18  | 6   | 16  | 5   | 4   | 9   | 58    |

### Racial Attribute Notes

- Humans are baseline (10 across all)
- Total variance compensates for racial abilities
- Physical races trade mental stats for physical
- No race has all high or all low attributes

---

## 7. Equipment Attribute Modifiers

### Equipment Slots with Attribute Bonuses

| Slot        | Common Attributes |
| ----------- | ----------------- |
| Head        | WIT, AWR          |
| Chest       | END, STR          |
| Hands       | FIN, STR          |
| Legs        | END, FIN          |
| Feet        | FIN, AWR          |
| Accessory 1 | Any               |
| Accessory 2 | Any               |

### Bonus by Item Quality

| Quality    | Typical Bonus |
| ---------- | ------------- |
| Common     | +0            |
| Quality    | +1            |
| Fine       | +1-2          |
| Superior   | +2            |
| Masterwork | +2-3          |
| Legendary  | +3-5          |

### Enchantment Bonuses

| Enchantment Tier | Attribute Bonus |
| ---------------- | --------------- |
| Minor            | +1              |
| Moderate         | +2              |
| Greater          | +3              |
| Superior         | +4              |
| Legendary        | +5              |

---

## 8. Temporary Attribute Effects

### Consumables

| Item Type         | Attribute | Bonus | Duration   |
| ----------------- | --------- | ----- | ---------- |
| Strength Potion   | STR       | +3-5  | 1-4 hours  |
| Agility Elixir    | FIN       | +3-5  | 1-4 hours  |
| Fortitude Brew    | END       | +3-5  | 1-4 hours  |
| Clarity Tonic     | WIT       | +3-5  | 1-4 hours  |
| Alertness Draught | AWR       | +3-5  | 1-4 hours  |
| Charisma Philter  | CHA       | +3-5  | 1-4 hours  |
| Quality meal      | END       | +1-2  | Until rest |

### Status Effects

| Condition          | Attribute Effect       |
| ------------------ | ---------------------- |
| Exhausted          | -3 all attributes      |
| Well Rested        | +1 all attributes      |
| Poisoned           | -2 END, -1 STR         |
| Intoxicated        | -2 WIT, -2 FIN, +1 CHA |
| Frightened         | -2 CHA, -1 all others  |
| Inspired           | +2 CHA, +1 WIT         |
| Wounded (moderate) | -1 STR, -1 FIN         |
| Wounded (severe)   | -3 STR, -2 FIN, -2 END |

### Environmental Effects

| Condition           | Attribute Effect              |
| ------------------- | ----------------------------- |
| Extreme cold        | -2 FIN, -1 STR                |
| Extreme heat        | -2 END, -1 WIT                |
| Darkness            | -3 AWR (without darkvision)   |
| Thin air (altitude) | -2 END                        |
| Dehydration         | -2 all attributes             |
| Starvation          | -3 STR, -2 END, -1 all others |

---

## 9. Attribute Interactions by System

### Combat System

| Mechanic                        | Attributes Used |
| ------------------------------- | --------------- |
| Melee damage                    | STR             |
| Ranged accuracy                 | FIN             |
| Dodge chance                    | FIN             |
| Health pool                     | END             |
| Stamina pool                    | END             |
| Initiative                      | AWR             |
| Ambush detection                | AWR             |
| Aimed attack success            | FIN             |
| Knockdown resistance            | STR, END        |
| Intimidation (demand surrender) | STR or CHA      |

### Magic System

| Mechanic             | Attributes Used |
| -------------------- | --------------- |
| Mana pool            | WIT             |
| Mana regeneration    | WIT             |
| Spell learning speed | WIT             |
| Spell resistance     | WIT, END        |
| Enchanting success   | WIT, FIN        |

### Crafting System

| Mechanic           | Attributes Used                       |
| ------------------ | ------------------------------------- |
| Crafting quality   | FIN (precision) + WIT (understanding) |
| Recipe discovery   | WIT                                   |
| Crafting speed     | FIN                                   |
| Tool effectiveness | STR (heavy tools), FIN (fine tools)   |

### Gathering System

| Mechanic          | Attributes Used |
| ----------------- | --------------- |
| Mining efficiency | STR, END        |
| Foraging quality  | AWR, FIN        |
| Hunting tracking  | AWR             |
| Fishing           | FIN, AWR        |
| Node detection    | AWR             |

### Social System

| Mechanic               | Attributes Used |
| ---------------------- | --------------- |
| Trade prices           | CHA             |
| Persuasion             | CHA             |
| Intimidation           | STR or CHA      |
| Deception              | WIT, CHA        |
| Insight (detect lies)  | WIT, AWR        |
| Leadership (orders)    | CHA             |
| Recruitment            | CHA             |
| Faction standing gains | CHA             |

### Party System

| Mechanic                    | Attributes Used              |
| --------------------------- | ---------------------------- |
| Order compliance            | Leader's CHA vs member's WIT |
| Loyalty gain rate           | Leader's CHA                 |
| Training speed (as trainer) | WIT, relevant attribute      |
| Training speed (as trainee) | WIT                          |

### Stealth System

| Mechanic            | Attributes Used |
| ------------------- | --------------- |
| Hide success        | FIN             |
| Move silently       | FIN             |
| Detection avoidance | FIN vs AWR      |
| Pickpocketing       | FIN vs AWR      |
| Lockpicking         | FIN             |
| Trap detection      | AWR             |
| Trap disarming      | FIN             |

---

## 10. Attribute Soft Caps

### Diminishing Returns

Attributes above 20 provide reduced benefit per point:

| Range | Effectiveness  |
| ----- | -------------- |
| 1-10  | 100% per point |
| 11-15 | 100% per point |
| 16-20 | 100% per point |
| 21-25 | 75% per point  |
| 26-30 | 50% per point  |
| 31+   | 25% per point  |

### Practical Maximums

| Source                     | Typical Maximum |
| -------------------------- | --------------- |
| Racial base                | 10-18           |
| Class leveling (30 levels) | +15-20 primary  |
| Training                   | +3              |
| Equipment                  | +5-8            |
| Consumables                | +5 (temporary)  |
| **Practical Total**        | **35-45**       |

---

## 11. Attribute-Based AI Behavior

NPCs use attributes to determine behavior patterns:

### Combat AI

| Attribute Balance  | Behavior                      |
| ------------------ | ----------------------------- |
| High STR, low WIT  | Aggressive, direct attacks    |
| High FIN, low END  | Hit-and-run, evasive          |
| High END, balanced | Defensive, endurance fighting |
| High AWR           | Tactical, uses terrain        |
| High WIT           | Uses skills efficiently       |

### Social AI

| Attribute Balance | Behavior                           |
| ----------------- | ---------------------------------- |
| High CHA          | Initiates conversation, negotiates |
| Low CHA, high STR | Relies on intimidation             |
| High WIT, low CHA | Indirect manipulation              |
| Low CHA, high AWR | Observes, avoids social            |

### Work AI

| Attribute Balance | Preferred Work                 |
| ----------------- | ------------------------------ |
| High STR, END     | Mining, labor, combat          |
| High FIN          | Crafting, theft, ranged        |
| High WIT          | Magic, scholarship, trade      |
| High AWR          | Scouting, hunting, guarding    |
| High CHA          | Leadership, trade, performance |

---

## 12. Data Tables Summary

### Quick Reference: Attribute Functions

| ATT | Combat        | Crafting      | Social       | Exploration    |
| --- | ------------- | ------------- | ------------ | -------------- |
| STR | Melee dmg     | Heavy tools   | Intimidate   | Climb, break   |
| FIN | Ranged, dodge | Precision     | —            | Stealth, locks |
| END | HP, stamina   | Work duration | —            | Survival       |
| WIT | Mana          | Discovery     | Insight      | Investigation  |
| AWR | Initiative    | —             | Read motives | Detection      |
| CHA | Morale        | —             | All social   | Recruitment    |

### Quick Reference: Racial Modifiers

| Race       | Highest       | Lowest        |
| ---------- | ------------- | ------------- |
| Humans     | Even          | Even          |
| Elves      | FIN, WIT, AWR | STR, END      |
| Dwarves    | END, STR      | FIN, CHA      |
| Lizardfolk | END, STR, AWR | CHA, WIT      |
| Kobolds    | FIN, WIT, AWR | STR, CHA      |
| Gnolls     | STR, END, AWR | WIT, CHA      |
| Goblins    | FIN, WIT, AWR | STR, CHA, END |
| Orcs       | STR, END      | WIT, CHA      |
| Ogres      | STR, END      | WIT, CHA, FIN |
| Trolls     | STR, END      | WIT, CHA, FIN |

---

## 13. Resource Pools (Stamina & Mana)

Characters have separate resource pools for physical and magical actions. This creates distinct identities for martial and magical characters.

### Stamina (Universal)

| Aspect       | Description                                                   |
| ------------ | ------------------------------------------------------------- |
| Who has it   | **All characters**                                            |
| Derived from | END (Endurance)                                               |
| Drains from  | Physical actions (combat, labor, travel)                      |
| Zero stamina | Character collapses ("naps") until recovered                  |
| Recovery     | Rest, food, skills, [Adrenaline](../core/adrenaline.md) burst |

**Stamina Actions:**

- Melee attacks
- Physical skills (dodge, sprint, power attack)
- Crafting
- Gathering
- Travel
- Any strenuous activity

### Mana (Caster-Only)

| Aspect       | Description                       |
| ------------ | --------------------------------- |
| Who has it   | **Only magic-trained characters** |
| Derived from | WIT (Wit)                         |
| Drains from  | Spellcasting                      |
| Zero mana    | Cannot cast spells (no collapse)  |
| Recovery     | Rest, meditation, mana potions    |

**Mana Actions:**

- Casting spells
- Channeling magic
- Enchanting (uses mana)
- Magical skills

### Per-School Mana Pools

Specialized mages may have separate mana pools per magical school:

| Specialization    | Pool Type                |
| ----------------- | ------------------------ |
| Generalist Mage   | Single shared mana pool  |
| School Specialist | Separate pool per school |
| Archmage          | Enlarged shared pool     |

See [Magic System](../magic/magic-system.md) for school-specific details.

### Shamanic Exception

[Shamans](../magic/shamanic-magic.md) use their **tribe's spirit pool** instead of personal mana:

| Aspect       | Shamanic Magic                     |
| ------------ | ---------------------------------- |
| Resource     | Shared tribal spirit pool          |
| Regeneration | Rituals, honored deeds, ceremonies |
| Limitation   | Must maintain tribal connection    |

### Resource Comparison

| Aspect             | Stamina                             | Mana                |
| ------------------ | ----------------------------------- | ------------------- |
| Universal?         | Yes (all characters)                | No (casters only)   |
| Base attribute     | END                                 | WIT                 |
| Zero state         | Collapse (nap)                      | Cannot cast (alert) |
| Combat cost        | Physical actions                    | Spell casting       |
| Non-combat cost    | Labor, travel                       | Enchanting, rituals |
| Emergency recovery | [Adrenaline](../core/adrenaline.md) | Mana potions only   |

---

## 14. Integration Points

| System      | Integration                              |
| ----------- | ---------------------------------------- |
| Combat      | Damage, accuracy, defense, initiative    |
| Magic       | Mana pool and regeneration               |
| Crafting    | Quality and discovery                    |
| Gathering   | Efficiency and detection                 |
| Classes     | Level-up bonuses                         |
| Skills      | Requirements and synergies               |
| Equipment   | Temporary bonuses                        |
| Races       | Base values                              |
| Personality | Separate system (traits, not attributes) |
| NPCs        | Same rules as players                    |
| Party       | Leadership and training                  |
| Factions    | Social interaction modifiers             |

---

## Related Documentation

### Core Systems

- [Adrenaline](../core/adrenaline.md) - Emergency stamina recovery
- [Time and Rest](../core/time-and-rest.md) - Stamina/mana recovery during rest
- [Stacking Rules](../core/stacking-rules.md) - How attribute bonuses combine

### Character Systems

- [Class Progression](class-progression.md) - Class attribute bonuses
- [NPC Simulation](../social/npc-simulation.md) - NPC attribute-based behavior

### Magic

- [Magic System](../magic/magic-system.md) - Mana pool usage
- [Shamanic Magic](../magic/shamanic-magic.md) - Spirit pool alternative

### Combat

- [Combat Resolution](../combat/combat-resolution.md) - Stamina in combat

### Reference

- [Formula Glossary](../../reference/formula-glossary.md) - Attribute formulas
