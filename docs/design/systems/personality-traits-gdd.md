---
type: system
scope: detailed
status: authoritative
version: 1.0.0
created: 2026-02-08
updated: 2026-02-08
subjects: [personality, traits, npc-behavior, character-generation, idle]
dependencies: []
---

# Personality Traits System - Authoritative Game Design

## Executive Summary

The Personality Traits system defines six bidirectional trait axes that shape NPC behavior throughout Realms of Idle's automated systems. Traits act as simple numeric modifiers on idle decisions: combat targeting, trade pricing, loyalty growth, stance preferences, and goal prioritization. Rather than simulating deep behavioral AI in real-time, traits produce multipliers that the batch-processing engine applies at each check-in cycle. Traits evolve slowly over time based on accumulated experiences, creating the illusion of personality growth without per-tick simulation.

**This document resolves:**

- Six personality trait axes with numeric ranges and modifier tables
- Trait generation at NPC creation (random distribution with racial/class modifiers)
- Trait evolution over time (batch-calculated drift based on experiences)
- Trait visibility through observable NPC behavior patterns
- Trait interaction rules for multi-axis decision-making
- Impact on party loyalty, stance preferences, and combat AI targeting
- Integration with idle automation (traits as multipliers, not behavioral simulation)

**Design Philosophy:** Traits are modifiers, not minds. Each trait axis produces a numeric value that scales existing game systems: a Brave NPC gets a loyalty bonus in dangerous zones, a Greedy NPC adjusts trade margins, a Patient NPC crafts at higher quality. The system avoids per-tick behavioral simulation in favor of batch-calculated multipliers applied at check-in. Players observe traits indirectly through NPC behavior patterns, creating the illusion of personality without the computational cost of deep simulation. Trait evolution is slow and deterministic, calculated from event logs during batch processing.

---

## 1. The Six Trait Axes

Each trait is a single axis between two named extremes, stored as an integer from -100 to +100. Zero represents a neutral disposition with no modifier effect.

| Axis            | Negative Extreme (-100) | Positive Extreme (+100) | Core Tension             |
| --------------- | ----------------------- | ----------------------- | ------------------------ |
| **Courage**     | Cowardly                | Brave                   | Safety vs. Risk          |
| **Generosity**  | Greedy                  | Generous                | Self vs. Others          |
| **Sociability** | Solitary                | Social                  | Isolation vs. Connection |
| **Honesty**     | Deceitful               | Honest                  | Advantage vs. Truth      |
| **Patience**    | Impulsive               | Patient                 | Speed vs. Thoroughness   |
| **Curiosity**   | Cautious                | Curious                 | Known vs. Unknown        |

### 1.1 Value Interpretation

| Range       | Label        | Modifier Strength | Behavioral Tendency                     |
| ----------- | ------------ | ----------------- | --------------------------------------- |
| -100 to -71 | Extreme Low  | Strong negative   | Defining characteristic, rarely deviate |
| -70 to -31  | Low          | Moderate negative | Strong tendency, occasional exceptions  |
| -30 to -11  | Mild Low     | Weak negative     | Noticeable lean, flexible               |
| -10 to +10  | Neutral      | None              | No strong tendency either way           |
| +11 to +30  | Mild High    | Weak positive     | Noticeable lean, flexible               |
| +31 to +70  | High         | Moderate positive | Strong tendency, occasional exceptions  |
| +71 to +100 | Extreme High | Strong positive   | Defining characteristic, rarely deviate |

### 1.2 Modifier Calculation

All trait effects derive from a single formula that converts the raw trait value into a percentage modifier:

```
Trait Modifier = Trait Value / 100

Examples:
  Courage = +60  → +0.60 (applied as +60% to courage-affected calculations)
  Courage = -40  → -0.40 (applied as -40% to courage-affected calculations)
  Courage = 0    → 0.00  (no modification)

Clamped Modifier (where system requires bounded range):
  Effective Modifier = clamp(Trait Value / 100, -MaxEffect, +MaxEffect)
  MaxEffect varies per system (see individual sections)
```

---

## 2. Trait Effects on Idle Systems

Traits modify automated decisions that the batch-processing engine calculates at check-in. Each subsection maps trait values to specific system multipliers.

### 2.1 Courage

Courage governs responses to danger, flee thresholds, and willingness to accept risky assignments.

| System                | Effect                                     | Modifier Formula                           |
| --------------------- | ------------------------------------------ | ------------------------------------------ |
| Combat flee threshold | HP% at which NPC attempts to disengage     | `30% - (Courage × 0.25%)`                  |
| Morale resistance     | Resistance to morale loss from defeats     | `Base + (Courage × 0.30%)`                 |
| Dangerous zone bonus  | Loyalty growth modifier in dangerous areas | `+0.5/day × (Courage / 100)`               |
| Intimidation resist   | Resistance to intimidation effects         | `Base + (Courage × 0.40%)`                 |
| Stance preference     | NPC prefers aggressive stances             | Courage > +50 prefers Offensive/Aggressive |

**Flee threshold examples:**

| Courage | Flee At HP% | Description                      |
| ------- | ----------- | -------------------------------- |
| -100    | 55%         | Flees at first significant wound |
| -50     | 42%         | Flees when meaningfully wounded  |
| 0       | 30%         | Standard flee threshold          |
| +50     | 17%         | Fights until seriously wounded   |
| +100    | 5%          | Fights to near-death             |

### 2.2 Generosity

Generosity governs resource sharing, trade behavior, and loot distribution in auto-resolved encounters.

| System           | Effect                                  | Modifier Formula                               |
| ---------------- | --------------------------------------- | ---------------------------------------------- |
| Trade pricing    | NPC buy/sell price adjustment           | `Price × (1 - Generosity × 0.0015)`            |
| Loot sharing     | Priority in auto-distributed party loot | Generous NPCs defer; Greedy claim first        |
| Gift acceptance  | Loyalty bonus from receiving gifts      | `Base Gift Loyalty × (1 + Generosity × 0.005)` |
| Resource sharing | Willingness to share consumables        | Generosity > +30: shares healing items         |
| Recruitment cost | Gold cost to recruit this NPC           | `Base × (1 - Generosity × 0.002)`              |

**Trade pricing examples:**

| Generosity | Sell Price Mod | Buy Price Mod | Description         |
| ---------- | -------------- | ------------- | ------------------- |
| -100       | +15%           | -15%          | Exploitative trader |
| -50        | +7%            | -7%           | Hard bargainer      |
| 0          | 0%             | 0%            | Standard pricing    |
| +50        | -7%            | +7%           | Favorable deals     |
| +100       | -15%           | +15%          | Gives things away   |

### 2.3 Sociability

Sociability governs loyalty growth speed, party tolerance, and loneliness effects during idle periods.

| System              | Effect                               | Modifier Formula                         |
| ------------------- | ------------------------------------ | ---------------------------------------- |
| Loyalty growth rate | Daily loyalty gain modifier          | `Base × (1 + Sociability × 0.005)`       |
| Party join chance   | Likelihood of accepting recruitment  | `Base% + (Sociability × 0.20%)`          |
| Loneliness penalty  | Morale loss when assigned solo tasks | `-(max(0, Sociability - 30) × 0.1)%/day` |
| Relationship speed  | How fast NPC bonds form in party     | `Base × (1 + Sociability × 0.005)`       |
| Solo efficiency     | Bonus when operating independently   | `+(max(0, -Sociability - 30) × 0.1)%`    |

**Loyalty growth examples:**

| Sociability | Daily Growth Modifier | Description                  |
| ----------- | --------------------- | ---------------------------- |
| -100        | -50%                  | Very slow to bond            |
| -50         | -25%                  | Reluctant bonds              |
| 0           | 0%                    | Standard loyalty growth      |
| +50         | +25%                  | Quick bonds                  |
| +100        | +50%                  | Bonds rapidly, needs company |

### 2.4 Honesty

Honesty governs trade reliability, contract honoring, and trust-based interactions with factions and other NPCs.

| System            | Effect                                   | Modifier Formula                      |
| ----------------- | ---------------------------------------- | ------------------------------------- |
| Trust modifier    | NPC trust from factions and settlements  | `Base + (Honesty × 0.25%)`            |
| Contract honor    | Chance NPC follows through on agreements | `70% + (Honesty × 0.25%)`             |
| Deception resist  | Resistance to being scammed/ambushed     | `Base + (Honesty × 0.20%)`            |
| Crime willingness | NPC accepts shady goals/assignments      | Honesty < -30: accepts theft missions |
| Faction standing  | Starting reputation with lawful factions | `Base + (Honesty × 0.10)`             |

**Contract honor examples:**

| Honesty | Honor Chance | Description                  |
| ------- | ------------ | ---------------------------- |
| -100    | 45%          | Breaks deals when convenient |
| -50     | 57%          | Unreliable                   |
| 0       | 70%          | Usually keeps word           |
| +50     | 82%          | Reliable                     |
| +100    | 95%          | Almost never breaks a deal   |

### 2.5 Patience

Patience governs crafting quality, training efficiency, and ambush effectiveness during idle timers.

| System             | Effect                              | Modifier Formula                |
| ------------------ | ----------------------------------- | ------------------------------- |
| Crafting quality   | Quality modifier when NPC crafts    | `Base + (Patience × 0.20%)`     |
| Crafting speed     | Timer modifier (patient = slower)   | `Base × (1 + Patience × 0.003)` |
| Training retention | XP retention from training sessions | `Base × (1 + Patience × 0.002)` |
| Ambush patience    | Watch duty effectiveness            | `Base + (Patience × 0.15%)`     |
| Initiative         | Combat initiative modifier          | `Base - (Patience × 0.15)`      |

**Crafting trade-off examples:**

| Patience | Quality Mod | Speed Mod   | Description                      |
| -------- | ----------- | ----------- | -------------------------------- |
| -100     | -20%        | +30% faster | Rushes jobs, lower quality       |
| -50      | -10%        | +15% faster | Quick but sloppy                 |
| 0        | 0%          | 0%          | Standard                         |
| +50      | +10%        | -15% slower | Methodical, better results       |
| +100     | +20%        | -30% slower | Perfectionist, excellent quality |

### 2.6 Curiosity

Curiosity governs exploration preference, new skill acceptance, and discovery bonuses during idle exploration timers.

| System               | Effect                                    | Modifier Formula                     |
| -------------------- | ----------------------------------------- | ------------------------------------ |
| Discovery bonus      | Bonus to finding loot/recipes/locations   | `Base + (Curiosity × 0.20%)`         |
| New skill interest   | Likelihood of pursuing cross-class skills | `Base% + (Curiosity × 0.25%)`        |
| Exploration priority | Preference for unexplored zones           | Curiosity > +30: prefers new areas   |
| Route preference     | Chooses known vs unknown paths            | Curiosity > +50: takes unknown paths |
| Class diversity      | Willingness to multi-class                | `Base% + (Curiosity × 0.20%)`        |

**Discovery bonus examples:**

| Curiosity | Discovery Mod | Description              |
| --------- | ------------- | ------------------------ |
| -100      | -20%          | Misses hidden finds      |
| -50       | -10%          | Sticks to known paths    |
| 0         | 0%            | Standard discovery rate  |
| +50       | +10%          | Actively searches        |
| +100      | +20%          | Finds things others miss |

---

## 3. Trait Generation

Traits are assigned at NPC creation time through a layered generation process. The result is a set of six integers, each clamped to the range -100 to +100.

### 3.1 Generation Algorithm

```
FOR EACH trait axis:
  Step 1: Base = 0 (neutral)
  Step 2: Random variance = bell curve sample, standard deviation 30
  Step 3: Racial modifier (see Section 3.2)
  Step 4: Class modifier (see Section 3.3)
  Step 5: Faction modifier (see Section 3.4)
  Step 6: Final = clamp(Base + Variance + Racial + Class + Faction, -100, +100)
```

The bell curve distribution ensures most NPCs cluster near neutral with a spread, while racial, class, and faction modifiers shift the center of that distribution.

### 3.2 Racial Modifiers

Racial modifiers shift the generation center. Individual variance is significant; these are tendencies, not deterministic.

| Race       | Courage | Generosity | Sociability | Honesty | Patience | Curiosity |
| ---------- | ------- | ---------- | ----------- | ------- | -------- | --------- |
| Humans     | 0       | 0          | 0           | 0       | 0        | 0         |
| Elves      | -5      | 0          | -5          | +5      | +15      | +5        |
| Dwarves    | +5      | -5         | 0           | +10     | +10      | -15       |
| Lizardfolk | 0       | -10        | -10         | +5      | +15      | -10       |
| Kobolds    | -10     | +5         | +20         | 0       | 0        | +5        |
| Gnolls     | +10     | 0          | +15         | 0       | -5       | -5        |
| Goblins    | -5      | -5         | +20         | -10     | -10      | +10       |
| Orcs       | +20     | -10        | +5          | -5      | -15      | -10       |
| Ogres      | +10     | -20        | -15         | -15     | -20      | -20       |
| Trolls     | +5      | -30        | -30         | -20     | -10      | -25       |

### 3.3 Class Modifiers

Class modifiers represent training and professional inclination. Applied on top of racial modifiers.

| Class      | Courage | Generosity | Sociability | Honesty | Patience | Curiosity |
| ---------- | ------- | ---------- | ----------- | ------- | -------- | --------- |
| Guard      | +15     | 0          | +5          | +10     | +5       | -5        |
| Merchant   | 0       | -5         | +10         | 0       | +5       | 0         |
| Thief      | +5      | -10        | 0           | -20     | +5       | +10       |
| Scholar    | -5      | 0          | -5          | +10     | +15      | +20       |
| Farmer     | 0       | +5         | +5          | +10     | +10      | -10       |
| Adventurer | +20     | 0          | +5          | 0       | -5       | +15       |
| Priest     | -5      | +15        | +10         | +15     | +10      | 0         |
| Assassin   | +10     | -15        | -10         | -20     | +20      | +5        |

### 3.4 Faction Modifiers

Faction membership applies an additional small bias. Values listed are per-trait shifts (typically +/- 5 to 10):

| Faction Type    | Trait Tendencies                         |
| --------------- | ---------------------------------------- |
| Military        | Courage +10, Patience +5, Curiosity -5   |
| Merchant Guild  | Patience +5, Generosity -10              |
| Thieves Guild   | Honesty -15, Courage +5, Curiosity +5    |
| Religious Order | Honesty +10, Generosity +10, Patience +5 |
| Academic        | Curiosity +15, Patience +10, Courage -5  |
| Tribal          | Courage +10, Sociability +10             |

### 3.5 Generation Distribution

With standard deviation 30 and modifiers applied, the expected population distribution:

| Range       | % of NPCs | Description       |
| ----------- | --------- | ----------------- |
| -100 to -71 | ~5%       | Extreme negative  |
| -70 to -31  | ~20%      | Moderate negative |
| -30 to +30  | ~50%      | Near neutral      |
| +31 to +70  | ~20%      | Moderate positive |
| +71 to +100 | ~5%       | Extreme positive  |

### 3.6 Representative Generated NPCs

**Example 1: Dwarven Guard**

```
Racial modifiers: Courage +5, Generosity -5, Sociability 0, Honesty +10, Patience +10, Curiosity -15
Class modifiers:  Courage +15, Generosity 0, Sociability +5, Honesty +10, Patience +5, Curiosity -5
Random variance:  Courage +8, Generosity -12, Sociability +3, Honesty +5, Patience -7, Curiosity +11

Final traits:
  Courage:     +28  (Mild High — stands ground under pressure)
  Generosity:  -17  (Mild Low — keeps the better share)
  Sociability: +8   (Neutral — sociable when on duty)
  Honesty:     +25  (Mild High — trustworthy, follows rules)
  Patience:    +8   (Neutral — standard diligence)
  Curiosity:   -9   (Neutral — sticks to routine)
```

**Example 2: Goblin Thief**

```
Racial modifiers: Courage -5, Generosity -5, Sociability +20, Honesty -10, Patience -10, Curiosity +10
Class modifiers:  Courage +5, Generosity -10, Sociability 0, Honesty -20, Patience +5, Curiosity +10
Random variance:  Courage -14, Generosity +6, Sociability -8, Honesty -3, Patience +2, Curiosity +18

Final traits:
  Courage:     -14  (Mild Low — avoids fair fights)
  Generosity:  -9   (Neutral — neither generous nor greedy)
  Sociability: +12  (Mild High — enjoys company)
  Honesty:     -33  (Low — habitual liar)
  Patience:    -3   (Neutral — average patience)
  Curiosity:   +38  (High — always poking around)
```

**Example 3: Elven Scholar**

```
Racial modifiers: Courage -5, Generosity 0, Sociability -5, Honesty +5, Patience +15, Curiosity +5
Class modifiers:  Courage -5, Generosity 0, Sociability -5, Honesty +10, Patience +15, Curiosity +20
Random variance:  Courage +3, Generosity +15, Sociability -20, Honesty -4, Patience +8, Curiosity +12

Final traits:
  Courage:     -7   (Neutral — neither brave nor cowardly)
  Generosity:  +15  (Mild High — shares knowledge freely)
  Sociability: -30  (Low — prefers solitude and study)
  Honesty:     +11  (Mild High — values truth)
  Patience:    +38  (High — methodical researcher)
  Curiosity:   +37  (High — driven to discover)
```

**Example 4: Orc Adventurer**

```
Racial modifiers: Courage +20, Generosity -10, Sociability +5, Honesty -5, Patience -15, Curiosity -10
Class modifiers:  Courage +20, Generosity 0, Sociability +5, Honesty 0, Patience -5, Curiosity +15
Random variance:  Courage -3, Generosity +8, Sociability -12, Honesty +20, Patience +6, Curiosity +2

Final traits:
  Courage:     +37  (High — charges toward danger)
  Generosity:  -2   (Neutral — standard sharing)
  Sociability: -2   (Neutral — takes or leaves company)
  Honesty:     +15  (Mild High — surprisingly honorable)
  Patience:    -14  (Mild Low — acts before planning)
  Curiosity:   +7   (Neutral — moderate interest in new things)
```

**Example 5: Human Priest**

```
Racial modifiers: Courage 0, Generosity 0, Sociability 0, Honesty 0, Patience 0, Curiosity 0
Class modifiers:  Courage -5, Generosity +15, Sociability +10, Honesty +15, Patience +10, Curiosity 0
Faction (Religious): Honesty +10, Generosity +10, Patience +5
Random variance:  Courage +11, Generosity -5, Sociability +3, Honesty -8, Patience +12, Curiosity -18

Final traits:
  Courage:     +6   (Neutral — standard bravery)
  Generosity:  +20  (Mild High — charitable)
  Sociability: +13  (Mild High — enjoys congregation)
  Honesty:     +17  (Mild High — values truth)
  Patience:    +27  (Mild High — calm and measured)
  Curiosity:   -18  (Mild Low — prefers established doctrine)
```

---

## 4. Trait Evolution

Traits drift slowly over time based on NPC experiences. Evolution is batch-calculated at check-in rather than simulated in real-time. This creates the sense that NPCs grow and change through their adventures without requiring per-tick personality simulation.

### 4.1 Experience-Based Drift

When the batch processor runs at check-in, it evaluates events that occurred during the idle period and applies trait shifts:

| Event Category        | Trait Affected | Shift Range | Notes                              |
| --------------------- | -------------- | ----------- | ---------------------------------- |
| Combat victory        | Courage        | +1 to +3    | Based on enemy threat level        |
| Combat defeat/flee    | Courage        | -1 to -3    | Based on how badly outmatched      |
| Generous trade        | Generosity     | +1 to +2    | Gave favorable terms               |
| Exploitative trade    | Generosity     | -1 to -2    | Took unfavorable terms from other  |
| Extended party time   | Sociability    | +1 per week | Gradual passive shift              |
| Extended solo task    | Sociability    | -1 per week | Gradual passive shift              |
| Honored agreement     | Honesty        | +1 to +2    | Kept deal when breaking was better |
| Broke agreement       | Honesty        | -2 to -4    | Based on significance              |
| Completed long craft  | Patience       | +1 to +2    | Based on timer duration            |
| Rushed/abandoned task | Patience       | -1          | Chose speed over quality           |
| Discovered new area   | Curiosity      | +1          | Per discovery                      |
| Refused exploration   | Curiosity      | -1          | Chose known path                   |

### 4.2 Natural Regression

Traits drift toward zero (neutral) over time without reinforcing experiences. This prevents extreme values from persisting without cause.

```
REGRESSION FORMULA (per week, batch-calculated):

  Distance from 0 = abs(Trait Value)

  Weekly regression:
    Distance 0-10:    No regression
    Distance 11-30:   0.5 toward 0
    Distance 31-50:   1.0 toward 0
    Distance 51-70:   1.5 toward 0
    Distance 71+:     2.0 toward 0
```

### 4.3 Trait Anchoring

Extreme traits resist change. This prevents established personality from swinging wildly from a single event.

| Trait Value Range          | Shift Modifier       | Description                     |
| -------------------------- | -------------------- | ------------------------------- |
| -100 to -81 or +81 to +100 | -50% shift magnitude | Deeply anchored, hard to change |
| -80 to -61 or +61 to +80   | -25% shift magnitude | Established, somewhat resistant |
| -60 to +60                 | Normal shifts        | Flexible, responsive to events  |

### 4.4 Trait Momentum

Repeated shifts in one direction accelerate further shifts, representing reinforced behavior patterns.

| Recent Pattern           | Effect                                |
| ------------------------ | ------------------------------------- |
| 3+ shifts same direction | +25% to further shifts that direction |
| 5+ shifts same direction | +50% to further shifts that direction |
| Mixed shifts             | Normal shift rates                    |

### 4.5 Evolution Batch Processing

```
ON CHECK-IN:

  Step 1: Collect events from idle period
  Step 2: For each event, determine trait affected and base shift
  Step 3: Apply anchoring modifier (Section 4.3)
  Step 4: Apply momentum modifier (Section 4.4)
  Step 5: Apply shift to trait, clamp to [-100, +100]
  Step 6: Apply natural regression (Section 4.2) — once per week boundary crossed
  Step 7: Recalculate all derived modifiers and cache
```

### 4.6 Evolution Rate

Trait evolution is deliberately slow. A typical NPC might shift 5-10 points on a single axis over a month of active play. This means:

- Players who check in daily see gradual, believable personality changes
- Extreme traits (high anchoring) take months of contrary experience to reverse
- An NPC's core personality remains recognizable across their lifetime
- Dramatic personality shifts require sustained, significant events

---

## 5. Trait Visibility

Players cannot directly inspect NPC trait values. Instead, traits become visible through observable behavior patterns over time. This creates a discovery mechanic where familiarity reveals personality.

### 5.1 Visibility Tiers

| Familiarity Level | Requirement                                         | Visible Traits                    |
| ----------------- | --------------------------------------------------- | --------------------------------- |
| Stranger          | No interaction                                      | Extreme traits only (abs > 70)    |
| Acquaintance      | 1+ day in party or settlement                       | Strong traits (abs > 50)          |
| Companion         | 7+ days in party                                    | Moderate traits (abs > 30)        |
| Trusted           | 30+ days in party, loyalty 50+                      | All non-neutral traits (abs > 10) |
| Bonded            | Loyalty 90+ (see `party-mechanics-gdd.md` Sec. 2.1) | Full trait values visible         |

### 5.2 Behavioral Indicators

Rather than showing numbers, the UI presents behavioral descriptions derived from trait values:

| Trait Range | UI Description Example (Courage)            |
| ----------- | ------------------------------------------- |
| -100 to -71 | "Flees at the first sign of danger"         |
| -70 to -31  | "Prefers to avoid confrontation"            |
| -30 to -11  | "Slightly cautious in dangerous situations" |
| -10 to +10  | No description shown (neutral)              |
| +11 to +30  | "Does not shy away from a fight"            |
| +31 to +70  | "Stands ground against tough odds"          |
| +71 to +100 | "Charges into danger without hesitation"    |

Each trait axis has its own set of seven descriptive strings. The UI displays only the descriptions for traits at or above the current visibility threshold.

### 5.3 Reputation Effects

Extreme traits become part of an NPC's public reputation, visible to all regardless of familiarity:

| Trait Extreme     | Reputation Label          |
| ----------------- | ------------------------- |
| Courage < -70     | "Known coward"            |
| Courage > +70     | "Fearless warrior"        |
| Generosity < -70  | "Notorious miser"         |
| Generosity > +70  | "Famously generous"       |
| Honesty < -70     | "Known liar"              |
| Honesty > +70     | "Trustworthy to a fault"  |
| Patience < -70    | "Dangerously impulsive"   |
| Patience > +70    | "Endlessly patient"       |
| Curiosity > +70   | "Compulsive explorer"     |
| Curiosity < -70   | "Set in their ways"       |
| Sociability > +70 | "Life of every gathering" |
| Sociability < -70 | "Reclusive hermit"        |

### 5.4 Trait Change Notifications

When a trait crosses a visibility threshold between check-ins, the player receives a narrative notification:

```
NOTIFICATION FORMAT:

  "[NPC Name] seems [more/less] [trait description] lately."

  Example:
    "Grimjaw seems braver lately."  (Courage crossed from +29 to +32)
    "Tella seems less patient than before." (Patience crossed from +12 to +9)

  Notifications only fire for traits the player can currently see
  (based on familiarity level).
```

---

## 6. Trait Interactions

When multiple traits are relevant to a single automated decision, the system uses a primary/secondary weighting model.

### 6.1 Decision Mapping

Each automated decision type has a primary trait (weight 1.0) and zero or more secondary traits (weight 0.5 each):

| Decision Type       | Primary Trait | Secondary Traits                          | Resolution                  |
| ------------------- | ------------- | ----------------------------------------- | --------------------------- |
| Combat engagement   | Courage       | Patience (timing)                         | Flee vs. fight              |
| Loot distribution   | Generosity    | Honesty (fairness)                        | Claim priority              |
| Exploration choice  | Curiosity     | Courage (danger), Patience (preparation)  | Route selection             |
| Trade negotiation   | Generosity    | Honesty (fair dealing), Patience (haggle) | Price modifier              |
| Party recruitment   | Sociability   | Courage (trust), Honesty (disclosure)     | Join acceptance             |
| Crime opportunity   | Honesty       | Courage (risk), Generosity (target)       | Accept/refuse shady goals   |
| Stance preference   | Courage       | Patience (defensive wait)                 | Preferred combat stance     |
| Crafting assignment | Patience      | Curiosity (recipe experimentation)        | Quality vs. speed trade-off |

### 6.2 Combined Modifier Formula

```
Combined Modifier = (Primary Trait × 1.0 + sum(Secondary Traits × 0.5)) / (1.0 + 0.5 × count(secondary))

Example: Combat engagement decision
  Courage = +60, Patience = -20

  Combined = (+60 × 1.0 + -20 × 0.5) / (1.0 + 0.5 × 1)
           = (60 + -10) / 1.5
           = 50 / 1.5
           = +33.3 (net positive — tends toward fighting)

Example: Exploration choice
  Curiosity = +40, Courage = -60, Patience = +30

  Combined = (+40 × 1.0 + -60 × 0.5 + +30 × 0.5) / (1.0 + 0.5 × 2)
           = (40 + -30 + 15) / 2.0
           = 25 / 2.0
           = +12.5 (slight lean toward exploring, but cautiously)
```

### 6.3 Emergent Archetypes

Certain trait combinations produce recognizable behavioral patterns. These are emergent from the modifier system, displayed as UI labels when trait thresholds qualify (at least one trait > +70 and the paired trait < -30, or both > +70 for same-direction pairs). NPCs without qualifying pairs display no archetype label.

| Combination                      | Emergent Archetype | Observable Behavior                     |
| -------------------------------- | ------------------ | --------------------------------------- |
| High Courage + Low Patience      | Reckless           | Charges in without preparation          |
| High Courage + High Patience     | Calculated         | Brave but waits for optimal timing      |
| Low Courage + High Curiosity     | Conflicted         | Wants to explore but avoids danger      |
| High Generosity + Low Honesty    | Manipulator        | Appears generous, serves hidden agenda  |
| High Honesty + Low Generosity    | Blunt Miser        | Honestly tells you they will not help   |
| High Sociability + Low Honesty   | Charming Liar      | Social butterfly, not trustworthy       |
| High Patience + Low Curiosity    | Conservative       | Methodical within known bounds          |
| Low Sociability + High Curiosity | Lone Explorer      | Explores alone, avoids group activities |

---

## 7. Impact on Combat AI

Traits feed into the combat system's automated targeting and behavior selection (see `combat-resolution.md` Section 9). In background simulation mode, traits determine which targeting algorithm an NPC uses.

### 7.1 Targeting Priority by Traits

The combat system selects a targeting strategy based on the NPC's dominant combat-relevant traits:

| Trait Profile                        | Targeting Strategy      | Description                                |
| ------------------------------------ | ----------------------- | ------------------------------------------ |
| Courage < -30 and Honesty < -30      | Lowest health target    | Picks off easy kills, avoids risk          |
| Courage > +30 and Honesty > +30      | Highest threat target   | Engages strongest enemy, demands surrender |
| Courage > +50 and Patience < -30     | Charge priority targets | Rushes healers and casters                 |
| Sociability > +50                    | Protect ally            | Targets whoever threatens guarded ally     |
| Patience > +50                       | Wait for opportunity    | Holds action for optimal strike            |
| No dominant trait (all near neutral) | Nearest target          | Default proximity-based targeting          |

These strategies map directly to the personality-based targeting table in `combat-resolution.md` Section 9, translating the narrative descriptions (Cowardly/Dishonest, Brave/Honest, Charger, Protective) into numeric trait thresholds.

### 7.2 Flee Behavior

Flee decisions during auto-resolved combat use the courage-derived threshold from Section 2.1:

```
FLEE CHECK (per combat round):

  IF NPC.health_percent <= flee_threshold(Courage):
    IF Sociability > +50 AND ally_in_danger:
      Override: Do not flee (loyalty to companions)
    ELSE IF Courage > +70:
      Override: Do not flee (extreme bravery)
    ELSE:
      NPC attempts to disengage
```

### 7.3 Stance Affinity

Traits create preferences for combat stances (see `party-mechanics-gdd.md` Section 3). When a player has not manually assigned a stance, the NPC defaults to its trait-preferred stance:

| Trait Combination             | Preferred Stance | Rationale                            |
| ----------------------------- | ---------------- | ------------------------------------ |
| Courage > +50, Patience < -30 | Aggressive       | Brave and impatient — all-out attack |
| Courage > +30, Patience > -30 | Offensive        | Brave but measured                   |
| Courage < -30                 | Cautious         | Avoids danger, stays back            |
| Sociability > +50             | Guard            | Protective of companions             |
| Patience > +50, Courage < +30 | Defensive        | Patient and risk-averse              |
| No strong preference          | Offensive        | Default balanced stance              |

Player-assigned stances always override trait preferences.

### 7.4 Morale Resistance

During extended combat (multiple rounds in background simulation), traits affect morale degradation:

```
MORALE FORMULA:

  Morale Loss Per Ally Death = Base × (1 - Courage × 0.003)
  Morale Recovery Rate       = Base × (1 + Patience × 0.002)
  Morale Bonus From Numbers  = Base × (1 + Sociability × 0.002)
```

---

## 8. Impact on Party Mechanics

Traits modify the party loyalty system defined in `party-mechanics-gdd.md` Section 2.

### 8.1 Loyalty Growth Modifiers

Personality traits adjust the base loyalty growth rate of +1/day:

```
LOYALTY GROWTH FORMULA (daily, batch-calculated):

  Base Growth = +1.0

  Trait Modifiers:
    Sociability:  +(Sociability × 0.005) per day
    Honesty:      Matching high-honesty party → +0.3/day bonus
    Courage:      Party in dangerous zone → +(Courage × 0.005)/day bonus
    Generosity:   Party leader gifts valued items → Generosity amplifies gift loyalty bonus

  Final Growth = Base + sum(Trait Modifiers) + Other Modifiers (from party-mechanics-gdd.md)
```

### 8.2 Loyalty Decay Modifiers

Traits also affect loyalty decay events:

| Event                  | Base Decay | Trait Modifier                                  |
| ---------------------- | ---------- | ----------------------------------------------- |
| Party defeat           | -3         | Courage > +50: halved (-1.5)                    |
| Ally killed in combat  | -5         | Sociability > +50: doubled (-10)                |
| No activity for 7 days | -2/day     | Patience > +50: halved (-1/day)                 |
| Alignment conflict     | -1/day     | Honesty > +50: doubled if conflict is dishonest |

### 8.3 Party Composition Effects

Trait alignment within a party affects group morale:

```
PARTY MORALE MODIFIER (batch-calculated):

  FOR EACH pair of party members:
    Trait Alignment Score = average similarity across all 6 axes
    IF Score > 60%: +1 morale
    IF Score < 30%: -1 morale (personality clash)

  Exemptions:
    Members with Sociability > +50 ignore clashes (tolerant)
    Members with Sociability < -50 ignore bonuses (indifferent)
```

### 8.4 Stance Override Behavior

At extreme loyalty levels, NPC traits may override player-assigned stances:

```
STANCE OVERRIDE CHECK:

  IF Loyalty < 30 (Wary) AND assigned stance conflicts with trait preference:
    50% chance NPC uses trait-preferred stance instead
  IF Loyalty < 10 (Hostile):
    NPC always uses trait-preferred stance (ignores player assignment)
  IF Loyalty >= 50 (Loyal):
    NPC always follows player assignment
```

---

## 9. Impact on Economic Systems

Traits modify NPC behavior in automated trade and resource management.

### 9.1 Trade Behavior

When NPCs auto-trade at settlements (see `settlement-system-gdd.md`), traits adjust pricing:

```
TRADE MODIFIER:

  Sell Price = Base Price × (1 + Generosity × -0.0015 + Patience × 0.001)
  Buy Price  = Base Price × (1 + Generosity × 0.0015 + Patience × -0.001)

  Honesty check: If Honesty < -50, NPC may accept stolen goods (+10% margin)
  Honesty check: If Honesty > +50, NPC refuses stolen goods
```

### 9.2 Resource Allocation

When party resources are auto-distributed at check-in:

| Generosity Range | Behavior                                        |
| ---------------- | ----------------------------------------------- |
| -100 to -51      | Claims best items first, hoards consumables     |
| -50 to -11       | Takes fair share but keeps bonuses              |
| -10 to +10       | Standard party sharing                          |
| +11 to +50       | Lets others pick first                          |
| +51 to +100      | Gives away surplus, shares healing items freely |

---

## 10. Trait-Based Assignment Optimization

Traits inform which NPCs are best suited for which idle tasks. The UI provides suitability indicators based on trait alignment with task requirements.

| Task Type         | Optimal Traits                    | Reasoning                           |
| ----------------- | --------------------------------- | ----------------------------------- |
| Combat expedition | High Courage, High Patience       | Stays in fight, waits for openings  |
| Exploration       | High Curiosity, High Courage      | Seeks new areas, handles danger     |
| Crafting          | High Patience                     | Better quality output               |
| Trading           | Low Generosity, High Honesty      | Maximizes margins, honors contracts |
| Leadership        | High Sociability, High Courage    | Inspires loyalty, leads from front  |
| Stealth missions  | Low Honesty, High Patience        | Comfortable with deception, waits   |
| Gathering         | High Patience, High Curiosity     | Thorough and exploratory            |
| Watch duty        | High Patience, High Courage       | Stays alert, confronts intruders    |
| Recruitment       | High Sociability, High Generosity | Attractive to potential recruits    |

---

## 11. Integration Points

### 11.1 Cross-System References

| System                 | Document                         | Integration                                        |
| ---------------------- | -------------------------------- | -------------------------------------------------- |
| Combat targeting AI    | `combat-resolution.md` Sec. 9    | Traits select targeting strategy                   |
| Combat flee thresholds | `combat-resolution.md` Sec. 9    | Courage sets HP% flee threshold                    |
| Combat stances         | `party-mechanics-gdd.md` Sec. 3  | Traits set default stance when unassigned          |
| Party loyalty          | `party-mechanics-gdd.md` Sec. 2  | Sociability/Courage modify growth and decay rates  |
| NPC goal arbitration   | `npc-core-systems-gdd.md` Sec. 3 | Trait values feed personality tiebreaker algorithm |
| NPC goal evaluation    | `npc-core-systems-gdd.md` Sec. 1 | Trait changes can trigger goal re-evaluation       |
| Crafting quality/speed | `crafting-system-gdd.md` Sec. 4  | Patience modifies quality and timer                |
| Trade pricing          | `settlement-system-gdd.md`       | Generosity/Honesty adjust automated trade margins  |
| Character progression  | `core-progression-system-gdd.md` | Traits influence skill offer weighting             |
| Class eligibility      | `class-system-gdd.md`            | Extreme traits gate certain class unlocks          |

### 11.2 NPC Core Systems Alignment

The NPC Core Systems document (`npc-core-systems-gdd.md`) defines a personality model using openness, conscientiousness, neuroticism, and agreeableness for goal conflict resolution. The six trait axes in this document map to that model:

| This System's Trait | NPC Core Systems Mapping        | Usage                    |
| ------------------- | ------------------------------- | ------------------------ |
| Curiosity           | Openness                        | Goal conflict tiebreaker |
| Patience            | Conscientiousness               | Goal conflict tiebreaker |
| Courage             | Inverse Neuroticism (partially) | Goal conflict tiebreaker |
| Generosity          | Agreeableness (partially)       | Goal conflict tiebreaker |
| Sociability         | Agreeableness (partially)       | Goal conflict tiebreaker |
| Honesty             | Conscientiousness (partially)   | Goal conflict tiebreaker |

The mapping is approximate. The six-axis model provides finer granularity for gameplay modifiers while the NPC Core Systems uses the broader Big Five dimensions for goal arbitration. Both systems read from the same underlying trait data.

### 11.3 Idle-Specific Design Constraints

| Constraint                         | Implementation                                                     |
| ---------------------------------- | ------------------------------------------------------------------ |
| No real-time behavioral simulation | Traits produce multipliers, not behavioral state machines          |
| Batch processing at check-in       | All trait effects calculated when player opens the game            |
| Timer-based queues                 | Patience affects crafting/training timers, not live interaction    |
| Strategic selection over execution | Player chooses party composition; traits affect automated outcomes |
| Offline-safe                       | Trait drift and event processing are deterministic and replayable  |
| Slow evolution, not instant change | Traits shift 5-10 points per axis per month of active play         |

---

## 12. Data Schema

### 12.1 Trait Storage

```
TraitSet {
  courage:     int  // -100 to +100
  generosity:  int  // -100 to +100
  sociability: int  // -100 to +100
  honesty:     int  // -100 to +100
  patience:    int  // -100 to +100
  curiosity:   int  // -100 to +100
}

TraitHistory {
  trait_name:  string
  old_value:   int
  new_value:   int
  cause:       string   // event that triggered the shift
  timestamp:   datetime // when the shift was applied
}
```

### 12.2 Trait Momentum Tracking

```
TraitMomentum {
  trait_name:       string
  recent_shifts:    int[]    // last 5 shift values (positive = toward +100)
  consecutive_same: int      // count of consecutive same-direction shifts
}
```

### 12.3 Trait Visibility Cache

```
TraitVisibility {
  observer_id:    string
  subject_id:     string
  familiarity:    enum { Stranger, Acquaintance, Companion, Trusted, Bonded }
  visible_traits: string[]  // trait names visible at current familiarity
  last_updated:   datetime
}
```

### 12.4 Cached Modifier Table

```
CachedTraitModifiers {
  npc_id:             string
  flee_threshold:     float   // derived from Courage
  trade_sell_mod:     float   // derived from Generosity + Patience
  trade_buy_mod:      float   // derived from Generosity + Patience
  loyalty_growth_mod: float   // derived from Sociability
  crafting_quality:   float   // derived from Patience
  crafting_speed:     float   // derived from Patience
  discovery_bonus:    float   // derived from Curiosity
  morale_resist:      float   // derived from Courage
  initiative_mod:     float   // derived from Patience
  preferred_stance:   string  // derived from Courage + Patience + Sociability
  targeting_strategy: string  // derived from Courage + Honesty + Sociability + Patience
  last_recalculated:  datetime
}
```

Cached modifiers are recalculated whenever a trait value changes (Section 4.5, Step 7) and stored for use by the batch processor and combat resolver.
