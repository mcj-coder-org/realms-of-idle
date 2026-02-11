# Content vs GDD Inconsistency Analysis

**Date:** 2026-02-10
**Scope:** Comparison of migrated content (docs/design/content/) against authoritative GDD documents (docs/design/systems/)
**Status:** Initial analysis - requires resolution before implementation

---

## Executive Summary

The migrated content documentation uses several conceptual frameworks that conflict with or differ from the authoritative GDD specifications. While the XP formulas and basic progression mechanics align, there are significant terminology conflicts around "tiers," different interpretations of class acquisition, and parallel skill systems that need reconciliation.

**Impact Level:** HIGH - These inconsistencies affect core game systems and would create confusion during implementation.

---

## 1. CRITICAL: "Tier" Terminology Conflict

### Issue

The term "tier" is used to mean **two completely different concepts** in the GDD vs. content:

#### GDD Definition (class-system-gdd.md)

```
Tier = Within-class progression stages
- Apprentice → Journeyman → Master
- Example: [Warrior - Apprentice] → [Warrior - Journeyman] → [Warrior - Master]
- Automatic advancement when class XP reaches threshold
- Display format: [Class Name - Tier]
```

#### Content Definition (e.g., fighter/index.md, crafter/index.md)

```
Tier = Position in class tree hierarchy
- Tier 1 = Foundation classes (Fighter, Crafter, Gatherer, Rogue, Trader)
- Tier 2 = Specialization classes (Warrior, Blacksmith, Archer, Hunter)
- Tier 3 = Advanced classes (Knight, Ranger, Weaponsmith, Armorsmith)
- Represents "transition to" different classes, not progression within one class
```

#### Content Also Uses (e.g., warrior/index.md stats table)

```
Trait = Milestone labels within a class
- Level 1: "Apprentice Warrior"
- Level 10: "Journeyman Warrior"
- Level 25: "Master Warrior"
- Level 50: "Legendary Warrior"
- These appear as cosmetic labels, not separate mechanical tiers
```

### Recommendation

**Adopt distinct terminology:**

- **Class Tree Tier** (1/2/3) = Position in class tree (Foundation/Specialization/Advanced)
- **Class Rank** (Apprentice/Journeyman/Master) = Mastery progression within a single class
- Update GDD to use "Class Rank" instead of "Tier" for Apprentice/Journeyman/Master
- Update content to consistently use "Class Tree Tier" or just "Tree Tier"

---

## 2. CRITICAL: Skill Tier System Conflict

### Issue

Content introduces a **third tier system** for skills that doesn't appear in the GDD:

#### Content Definition (skills/tiered/weapon-mastery.md)

```
Skill Tiers = Progression stages within ONE skill
- Lesser → Greater → Enhanced
- Example: Weapon Mastery (Lesser) → Weapon Mastery (Greater) → Weapon Mastery (Enhanced)
- Each tier has increasing effects and scaling
- XP thresholds for tier advancement
```

#### GDD Definition (skill-recipe-system-gdd.md)

```
Skill Rarity = Quality/power level of skills
- Common → Rare → Legendary
- Achievement bonuses increase legendary chances
- Does NOT mention Lesser/Greater/Enhanced tiers
```

### Current State

The content treats these as **two separate systems**:

- Skills have **both** "tier: tiered" AND "rarity: common"
- Skills progress through Lesser/Greater/Enhanced tiers
- Rarity affects discovery probability

### Recommendation

**Choose one approach:**

**Option A: Keep both systems** (RECOMMENDED)

- **Skill Tier** (Lesser/Greater/Enhanced) = Progression within a skill (you improve it over time)
- **Skill Rarity** (Common/Rare/Legendary) = Discovery difficulty / power level
- Update GDD to formally define the Lesser/Greater/Enhanced progression system
- Clarify that rarity affects initial availability, tiers affect ongoing power

**Option B: Merge into single system**

- Eliminate Lesser/Greater/Enhanced
- Use only Common/Rare/Legendary with scaling bonuses
- Would require significant content changes

---

## 3. HIGH: Class Acquisition Model Conflict

### Issue

The GDD and content describe different models for how players obtain specialized classes:

#### GDD Model (class-system-gdd.md, Section 2.1)

```
Specializations are NEW classes, not replacements:
- Player holds [Warrior - Apprentice]
- Earns sword bucket XP ≥ 5,000
- Level Up Event offers [Blade Dancer - Apprentice] as SEPARATE class
- Player can accept → now holds BOTH classes (2/3 slots used)
- Both classes progress independently
- Example: [Warrior - Journeyman] + [Blade Dancer - Apprentice]
```

#### Content Model (fighter/index.md, Section "Progression Paths")

```
Classes "transition to" higher tier classes:
- "Fighter can transition to the following Tier 2 classes upon reaching 5,000 XP"
- "Specializations:" section lists paths as progression options
- Implies replacement rather than accumulation
- Less clear about multi-classing mechanics
```

### Recommendation

**Update content language to match GDD:**

- Replace "transition to" with "unlocks eligibility for" or "can discover"
- Add "Progression Paths" subsection: "Upon reaching X XP, the following classes become available to learn as additional classes"
- Clarify multi-class system: "All accepted classes are always active (no slot limits)"
- Add examples showing player holding multiple classes simultaneously

---

## 4. MEDIUM: Tag Depth Access Specification

### Issue

Content explicitly defines tag depth access per class, GDD doesn't formalize this:

#### Content Definition (warrior/index.md)

```
Prerequisites:
| Tag Depth Access | 2 levels (e.g., `Combat/Melee`) |
```

#### GDD Reference (class-system-gdd.md)

```
- Mentions specializations have "unique tag requirements"
- Doesn't explicitly tie tag depth to tier/tree position
- Section 4.1 shows tag examples but no formal depth rules
```

### Recommendation

**Formalize in GDD:**

- Add section "Tag Depth Access by Class Tree Tier"
- Define clear rules:
  - Tier 1 (Foundation): Depth 1 (e.g., `Combat`)
  - Tier 2 (Specialization): Depth 2 (e.g., `Combat/Melee`)
  - Tier 3 (Advanced): Depth 3+ (e.g., `Combat/Melee/Sword`)
- Explain how this affects recipe/skill/action eligibility
- Update class-system-gdd.md with formal specification

---

## 5. MEDIUM: XP Threshold Values

### Issue

Multiple threshold values appear across content with unclear relationships:

#### Content Examples

```
Fighter/Gatherer/Crafter:
- "100 actions (guaranteed unlock)"
- Bucket threshold = 1,000 XP (implied from 100 actions × 10 XP)

Tier 2 Classes (Warrior/Blacksmith):
- "5,000 XP from tracked actions"
- Matches GDD specialization standard threshold

Knight (Tier 3):
- "50,000 XP from honorable combat"
- Significantly higher, suggests advanced class threshold

Early unlock mention:
- "Early unlock via probability after 50 actions"
- Probabilistic system not detailed in GDD
```

### Recommendation

**Document threshold formula in GDD:**

- Foundation classes (Tier 1): 1,000 bucket XP
- Specialization classes (Tier 2): 5,000 bucket XP (standard) or 15,000 (without parent)
- Advanced classes (Tier 3): 25,000-50,000 bucket XP depending on class
- Define probabilistic early unlock: 50% probability at half threshold, scales to 100% at full threshold
- Add section to core-progression-system-gdd.md: "Class Eligibility Thresholds by Tree Tier"

---

## 6. LOW: Skill Synergy Bonuses Format

### Issue

Content provides rich detail on synergy bonuses that GDD doesn't specify:

#### Content Example (warrior/index.md)

```
Synergy Bonuses:
- Faster learning: 2x XP from weapon combat
- Better effectiveness: +25% damage bonus at Warrior 15
- Reduced cost: -25% stamina at Warrior 15

Scaling with Class Level:
| Class Level | XP Multiplier | Effectiveness | Cost Reduction |
| Level 5     | 1.5x (+50%)   | +15%          | -15%           |
| Level 15    | 2.0x (+100%)  | +25%          | -25%           |
| Level 30+   | 2.5x (+150%)  | +35%          | -35%           |
```

#### GDD (skill-recipe-system-gdd.md)

```
- Mentions synergy classes get "faster learning, better effectiveness"
- Doesn't provide specific formulas or scaling
- Section 2.3 examples show benefits but not mechanics
```

### Recommendation

**Add to GDD (low priority):**

- Create "Synergy Bonus Mechanics" section in skill-recipe-system-gdd.md
- Define standard progression curve for synergy bonuses
- Provide formula: `Bonus = BaseBonus × (1 + ClassLevel / 30)`
- Allow content to specify per-class variations from standard

---

## 7. LOW: Milestone Level Terminology

### Issue

Content uses "Legendary" as fourth trait milestone:

#### Content (warrior/index.md)

```
| Level | Trait              |
| 1     | Apprentice Warrior |
| 10    | Journeyman Warrior |
| 25    | Master Warrior     |
| 50    | Legendary Warrior  |
```

#### GDD (core-progression-system-gdd.md)

```
Three-Tier Progression:
- Apprentice → Journeyman → Master
- No mention of "Legendary" as fourth tier
```

### Recommendation

**Clarify status:**

- If "Legendary" is just a cosmetic label at L50, document as "cosmetic milestone label"
- If "Legendary" has mechanical significance (fourth rank), add to GDD tier system
- Likely just cosmetic flavor, note in content template: "Level 50+ may use Legendary, Grandmaster, etc. as flavor text"

---

## 8. OBSERVATION: Content Richness vs GDD Specification

### Pattern

Content provides significantly more detail than GDD in several areas:

- Lore and flavor text (appropriate)
- Specific synergy bonus tables (good for content)
- Stat progression tables (detailed implementation)
- Related content cross-references (good for docs)

### Assessment

**This is generally POSITIVE:**

- GDD defines mechanics and rules
- Content provides rich implementation examples
- Gap between specification (GDD) and documentation (content) is expected

**However:**

- Some mechanical details in content should be promoted to GDD
- Formal specifications should exist for: tag depth access, synergy formulas, threshold scaling

---

## Summary of Recommendations

### Must Fix (Blocking Implementation)

1. **Resolve tier terminology conflict** → Adopt "Class Tree Tier" (1/2/3) and "Class Rank" (Apprentice/Journeyman/Master)
2. **Document skill tier system in GDD** → Add Lesser/Greater/Enhanced progression mechanics to skill-recipe-system-gdd.md
3. **Clarify class acquisition model in content** → Replace "transition to" language with "unlocks eligibility for"

### Should Fix (High Value)

1. **Formalize tag depth access rules** → Add section to class-system-gdd.md
2. **Document threshold formulas** → Add section to core-progression-system-gdd.md
3. **Add synergy bonus mechanics** → Add section to skill-recipe-system-gdd.md

### Nice to Have (Low Priority)

1. **Clarify "Legendary" milestone** → Add note to content template
2. **Cross-reference GDD sections** → Add "Implemented by" notes in GDD pointing to content examples

---

## Action Items

1. **Decision needed:** Approve terminology changes (Class Tree Tier / Class Rank)
2. **GDD updates:** Add missing formal specifications
3. **Content updates:** Align language with GDD model (especially class acquisition)
4. **Template creation:** Create class content template with correct terminology

---

## Files Requiring Updates

### GDD Files (Add Specifications)

- `docs/design/systems/class-system-gdd.md` (terminology, tag depth)
- `docs/design/systems/core-progression-system-gdd.md` (thresholds)
- `docs/design/systems/skill-recipe-system-gdd.md` (skill tiers, synergy bonuses)

### Content Files (Language Alignment)

- All `docs/design/content/classes/*/index.md` files
- Replace "tier: N" with "tree_tier: N" in frontmatter
- Replace "transition to" with "unlocks eligibility for"
- Add multi-class context to progression sections

### New Files Needed

- `docs/design/content/_templates/class-template.md` (with correct terminology)
- `docs/design/content/_templates/skill-template.md` (with Lesser/Greater/Enhanced)

---

_Analysis complete. Awaiting decision on recommended terminology changes before proceeding with updates._
