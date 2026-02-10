---
title: 'Healer'
type: 'class'
category: 'magic'
tier: 2
prerequisite_xp: 5000
prerequisite_actions: healing magic
summary: 'Restoration specialist using magic to heal injuries, cure diseases, and support others'
---

# Healer

## Tags

> **Required Section:** Tags enable compile-time safe tag access in C# code and drive the skill/class/recipe systems.

### C# Reference

**Strongly Typed Tags:** Use these tags in C# code for compile-time safety.

| Tag Path                    | C# Reference                          | Purpose                |
| --------------------------- | ------------------------------------- | ---------------------- |
| `Magic/Healing`             | `SkillTags.Magic.Healing.Value`       | Healer class access    |
| `Magic/Healing/Restoration` | `SkillTags.Magic.Healing.Restoration` | Restoration magic      |
| `Magic/Healing/Cure`        | `SkillTags.Magic.Healing.Cure`        | Disease/poison curing  |
| `Gathering/Herbalism`       | `SkillTags.Gathering.Herbalism.Value` | Herbal healing synergy |

**How to find class tags:**

1. Check `ClassDefinition.GrantedTags` in class definition files (e.g., `src/CozyFantasyRpg/Content/Classes/MagicClasses.cs`)
2. Verify against `src/CozyFantasyRpg/Shared/SkillTags.cs` for exact C# reference

### Tag Access

This class requires the following tags for access/synergy:

| Tag                   | Depth | Classes With Access              |
| --------------------- | ----- | -------------------------------- |
| `Magic/Healing`       | 2     | Healer, Mage, Channeler          |
| `Magic/Healing/*`     | 3     | Healer (full access to sub-tags) |
| `Gathering/Herbalism` | 2     | Healer, Herbalist, Gatherer      |

**Note:** Tag depth determines which classes can access this content. See [Tag System](../../../systems/content/tag-system/index.md) for details.

---

## Lore

### Origin

Life is fragile. Injuries kill, diseases ravage, poisons destroy. Before magical healing, people suffered and died from wounds that should have been survivable, illnesses that could have been cured, conditions that could have been remedied. Healers changed everything - using restoration magic to mend flesh, purge toxins, fight disease, and restore vitality. Where mundane medicine fails or takes weeks, Healers succeed in moments. This power makes them among the most valued members of any community.

Healing magic emerged from understanding life energy. Living beings possess vital force that maintains health and enables recovery. Injuries, diseases, and poisons disrupt this force. Healers learned to channel magical energy that reinforces and restores life force, accelerating natural healing dramatically or making impossible recoveries possible. The process requires deep understanding of living systems - anatomy, physiology, disease processes. Healers aren't just spellcasters but medical practitioners using magic as their primary tool.

The profession attracts those who genuinely wish to help others. Unlike combat magic that destroys or crafting magic that enriches its practitioners, healing magic primarily benefits others. Healers spend their power easing suffering, preventing deaths, restoring health. This selfless focus creates specific psychological profile - Healers who pursue the craft for money or prestige rarely develop the compassion and dedication that excellence requires.

### In the World

Every settlement needs Healers. Accidents happen, diseases spread, violence causes injuries. Without magical healing, communities face high mortality from preventable causes. A single competent Healer dramatically improves quality of life and survival rates. Consequently, Healers receive respect, welcome, and often financial support from grateful communities.

Healer specializations reflect different aspects of restoration magic. Field Healers focus on trauma - quickly stabilizing combat injuries, mending broken bones, stopping bleeding. They accompany military forces, adventuring parties, and disaster response teams. Clinical Healers maintain practices treating routine ailments - curing common diseases, healing minor injuries, addressing chronic conditions. They serve as magical doctors in stable communities. Plague Healers specialize in disease, developing deep knowledge of illness and how restoration magic combats it. Each role requires different skills and temperament.

The profession involves emotional weight. Healers witness suffering constantly - screaming patients, grieving families, preventable deaths when help arrives too late. They make impossible decisions about who receives limited healing resources when triage is necessary. They feel personal failure when patients die despite their efforts. Many Healers burn out, overwhelmed by accumulated trauma. Those who persist develop either strong emotional boundaries or deep spiritual practices helping them process the work's psychological costs.

Apprentice Healers begin with basic restoration - mending minor cuts, easing pain, curing simple ailments. They study anatomy and disease, learning what they're actually healing. They practice on willing patients with minor conditions, building skill before handling serious cases. They learn ethical standards - confidentiality, consent, fair treatment regardless of ability to pay. Most importantly, they learn their limitations - when cases exceed their skill, when to refer to more experienced Healers, when healing magic can't help.

Master Healers perform miraculous recoveries. They reattach severed limbs, cure supposedly terminal diseases, neutralize deadly poisons, heal injuries that should be fatal. Their reputation draws desperate cases from vast distances. They often teach, training next generations and advancing healing knowledge. The greatest develop new restoration techniques, expanding what healing magic can accomplish. Their work saves thousands of lives throughout their careers.

---

## Mechanics

### Prerequisites

| Requirement        | Value                                                 |
| ------------------ | ----------------------------------------------------- |
| XP Threshold       | 5,000 XP from healing magic tracked actions           |
| Related Foundation | [Channeler](../index.md) (optional, provides bonuses) |
| Tag Depth Access   | 2 levels (e.g., `[Magic/Healing]`)                    |

### Requirements

| Requirement       | Value                                    |
| ----------------- | ---------------------------------------- |
| Unlock Trigger    | Heal others using restoration magic      |
| Primary Attribute | WIS (Wisdom), CHA (Charisma)             |
| Starting Level    | 1                                        |
| Tools Required    | Medical supplies, holy symbol (optional) |

### Stats

#### Base Class Stats

| Level | HP Bonus | Mana Bonus | Trait             |
| ----- | -------- | ---------- | ----------------- |
| 1     | +5       | +18        | Apprentice Healer |
| 10    | +15      | +55        | Journeyman Healer |
| 25    | +30      | +115       | Master Healer     |
| 50    | +55      | +210       | Legendary Healer  |

#### Healing Bonuses

| Class Level | Heal Power | Heal Efficiency | Cure Success | Support Range |
| ----------- | ---------- | --------------- | ------------ | ------------- |
| 1-9         | +20%       | +15%            | 70%          | 5m            |
| 10-24       | +50%       | +35%            | 85%          | 15m           |
| 25-49       | +100%      | +65%            | 95%          | 30m           |
| 50+         | +180%      | +100%           | 100%         | 60m           |

#### Starting Skills

| Skill                                                            | Type    | Effect                                   |
| ---------------------------------------------------------------- | ------- | ---------------------------------------- |
| Basic Healing                                                    | Active  | Can heal minor injuries and ease pain    |
| [Spell Focus](../../skills/tiered/spell-focus/index.md) (Lesser) | Passive | Enhanced healing spell effectiveness     |
| [Mana Well](../../skills/tiered/mana-well/index.md) (Lesser)     | Passive | Expanded mana for sustained healing work |

#### Synergy Skills

Skills that have strong synergies with Healer. These skills can be learned by any character, but Healers gain significant bonuses (2x XP acquisition, enhanced effectiveness, reduced costs). Higher Healer levels provide stronger synergy bonuses.

**Core Magic Skills**:

- [Spell Focus](../../skills/tiered/spell-focus/index.md) - Lesser/Greater/Enhanced - Enhanced spell effectiveness
- [Mana Well](../../skills/tiered/mana-well/index.md) - Lesser/Greater/Enhanced - Expanded magical reserves
- [School Mastery](../../skills/mechanic-unlock/school-mastery/index.md) - Mechanic - Deep expertise in one school
- [Ritual Casting](../../skills/mechanic-unlock/ritual-casting/index.md) - Lesser/Greater/Enhanced - Perform elaborate magical rituals
- [Mana Transfer](../../skills/mechanic-unlock/mana-transfer/index.md) - Lesser/Greater/Enhanced - Share magical energy

**Core Restoration Skills**:

- Healing Touch - Lesser/Greater/Enhanced - Increased restoration power
- Cure Disease - Lesser/Greater/Enhanced - Cure various illnesses
- Purge Poison - Lesser/Greater/Enhanced - Neutralize toxins
- Pain Relief - Lesser/Greater/Enhanced - Reduce patient suffering
- Regeneration - Lesser/Greater/Enhanced - Continuous healing over time
- Resurrection - Lesser/Greater/Enhanced - Revive the recently deceased
- Mass Healing - Lesser/Greater/Enhanced - Heal multiple targets
- Mana Efficiency - Lesser/Greater/Enhanced - Reduced healing costs
- Diagnosis - Lesser/Greater/Enhanced - Identify ailments
- Protective Aura - Lesser/Greater/Enhanced - Damage reduction field
- Stamina Restoration - Lesser/Greater/Enhanced - Restore energy reserves
- Limb Reattachment - Lesser/Greater/Enhanced - Restore severed body parts
- Disease Immunity - Lesser/Greater/Enhanced - Boost disease resistance
- Life Link - Lesser/Greater/Enhanced - Transfer life force
- Miracle Healing - Epic - Heal any condition

**Note**: All skills listed have strong synergies because they are core restoration skills. Characters without Healer class can still learn these skills but progress at base rate (1x XP) without effectiveness bonuses.

#### Synergy Bonuses

Healer provides context-specific bonuses to restoration-related skills based on logical specialization:

**Core Restoration Skills** (Direct specialization - Strong synergy):

- **Healing Touch**: Essential for restoring health and vitality
  - Faster learning: 2x XP from healing actions (scales with class level)
  - Better effectiveness: +25% healing power at Healer 15, +35% at Healer 30+
  - Reduced cost: -25% mana at Healer 15, -35% at Healer 30+
- **Cure Disease**: Directly tied to medical magical expertise
  - Faster learning: 2x XP from disease treatment
  - Better success: +30% cure rate at Healer 15
  - Lower cost: Additional -20% mana cost at Healer 15
- **Diagnosis**: Core skill for identifying ailments
  - Faster learning: 2x XP from diagnostic actions
  - Better accuracy: +20% diagnosis precision at Healer 15
  - More insight: +50% chance to identify rare conditions
- **Mana Efficiency**: Fundamental to sustainable healing practice
  - Faster learning: 2x XP from mana management in healing
  - Better efficiency: +25% mana reduction at Healer 15
  - More capacity: +50% effective mana pool for healing

**Synergy Strength Scales with Class Level**:

| Healer Level | XP Multiplier | Effectiveness Bonus | Cost Reduction | Example: Healing Touch |
| ------------ | ------------- | ------------------- | -------------- | ---------------------- |
| Level 5      | 1.5x (+50%)   | +15%                | -15% mana      | Good but moderate      |
| Level 10     | 1.75x (+75%)  | +20%                | -20% mana      | Strong improvements    |
| Level 15     | 2.0x (+100%)  | +25%                | -25% mana      | Excellent synergy      |
| Level 30+    | 2.5x (+150%)  | +35%                | -35% mana      | Masterful synergy      |

**Example Progression**:

A Healer 15 learning Healing Touch:

- Performs 50 healing actions (earning 2x XP = 1000 XP total, vs 500 XP for non-Healer)
- Healing Touch available at level-up after just 500 XP (vs 1500 XP for non-healing class)
- When using Healing Touch: Base +60% healing becomes +85% healing (base +25% synergy)
- Mana cost: 15 mana instead of 20 (25% reduction)

#### Tracked Actions

Actions that grant XP to the Healer class:

| Action Category     | Specific Actions                           | XP Value                |
| ------------------- | ------------------------------------------ | ----------------------- |
| Healing             | Successfully heal injuries and ailments    | 10-80 per healing       |
| Disease Curing      | Cure diseases and infections               | 15-100 per cure         |
| Poison Treatment    | Neutralize poisons and toxins              | 15-100 per treatment    |
| Emergency Medicine  | Stabilize critical patients                | 20-150 per save         |
| Preventive Care     | Provide ongoing health maintenance         | 5-30 per session        |
| Mass Casualty       | Heal multiple injured people efficiently   | 30-200 per event        |
| Medical Research    | Study diseases and develop treatments      | 20-100 per breakthrough |
| Teaching            | Train others in healing arts               | 10-50 per student       |
| Miraculous Recovery | Achieve seemingly impossible healing       | 50-300 per miracle      |
| Life Saving         | Prevent deaths through exceptional healing | 100-500 per save        |

#### Class Consolidation

See [Class Consolidation System](../../../systems/character/class-consolidation/index.md) for full mechanics.

| Consolidation Path                         | Requirements                                     | Result Class | Tier |
| ------------------------------------------ | ------------------------------------------------ | ------------ | ---- |
| [Paladin](../../consolidation/)            | Healer + [Knight](../../fighter/warrior/knight/) | Paladin      | 1    |
| [Cleric](../../consolidation/)             | Healer + divine focus                            | Cleric       | 1    |
| [Combat Medic](../../consolidation/)       | Healer + field medicine focus                    | Combat Medic | 1    |
| [Arch-Healer](../../consolidation/healer/) | Healer + master all restoration magic            | Arch-Healer  | 2    |

### Interactions

| System                                                           | Interaction                         |
| ---------------------------------------------------------------- | ----------------------------------- |
| [Magic System](../../../systems/character/magic-system/index.md) | Uses restoration school of magic    |
| [Health](../../../../../systems/character/)                      | Restores HP and cures conditions    |
| [Disease](../../../systems/world/)                               | Cures and prevents diseases         |
| [Poison](../../../systems/combat/)                               | Neutralizes toxins and poisons      |
| [Party](../../../systems/combat/)                                | Essential support role in groups    |
| [Settlement](../../../systems/world/settlements/index.md)        | Critical community service provider |

---

## Related Content

- **Requires:** Medical knowledge, mana pool, healing supplies
- **Equipment:** [Medical Supplies](../../item/), [Holy Symbol](../../item/), [Healing Herbs](../../material/)
- **Synergy Classes:** [Mage](./mage/index.md), [Cook](../../crafter/cook/), [Alchemist](../../crafter/alchemist/)
- **Consolidates To:** [Paladin](../../consolidation/), [Cleric](../../consolidation/), [Combat Medic](../../consolidation/)
- **See Also:** [Magic System](../../../systems/character/magic-system/index.md), [Restoration School](../../../systems/magic/), [Health & Injury](../../../../../systems/character/)
