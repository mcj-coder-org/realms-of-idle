<!-- ADAPTATION REQUIRED -->
<!-- This file was migrated from source but needs manual review: -->
<!-- - Update terminology (dormant classes, XP split, etc.) -->
<!-- - Align with current GDD architecture -->
<!-- - Add missing sections as needed -->
<!-- - Update frontmatter with correct gdd_ref -->

---

title: Racial Affinity System
type: system
summary: Racial bonuses and penalties using hierarchical tag system

---

# Racial Affinity System

## 1. Overview

Racial affinities use the hierarchical tag system to provide learning and effectiveness bonuses for skills matching a race's cultural and biological strengths. These bonuses stack with class synergies, creating unique character builds based on race-class combinations.

### Core Principles

| Principle                 | Description                                                                           |
| ------------------------- | ------------------------------------------------------------------------------------- |
| **Tag-Based**             | Racial affinities use same hierarchical tags as skills and classes                    |
| **Mostly Broad**          | Most affinities are top-level (`+Crafting` affects all `Crafting/*`)                  |
| **Some Specific**         | Some races have specific cultural affinities (`+Crafting/Woodworking` for Wood Elves) |
| **Learning Bonuses**      | Affects XP requirements and level-up appearance rates                                 |
| **Effectiveness Bonuses** | Affects skill power when used                                                         |
| **Stacks with Class**     | Racial and class bonuses add together                                                 |
| **Can Have Penalties**    | Some races have negative affinities (harder to learn, less effective)                 |

---

## 2. Affinity Levels

Racial affinities come in four levels:

| Symbol | Level           | Learning Effect                                 | Effectiveness Effect |
| ------ | --------------- | ----------------------------------------------- | -------------------- |
| **+**  | Strong Positive | -30% XP required, 2x level-up appearance rate   | +20% effectiveness   |
| **○**  | Positive        | -15% XP required, 1.5x level-up appearance rate | +10% effectiveness   |
| **−**  | Neutral         | No effect                                       | No effect            |
| **×**  | Negative        | +30% XP required, 0.5x level-up appearance rate | -10% effectiveness   |

### Learning Effect Details

**XP Required:**

- Affects BASE XP requirement before class modifiers apply
- Example: Strong Positive reduces 1000 XP requirement to 700 XP

**Level-Up Appearance Rate:**

- Affects probability of skill appearing in level-up offers
- Strong Positive: 2x more likely to appear
- Negative: 0.5x (half as likely) to appear

**Effectiveness:**

- Bonus/penalty applies to skill power when used
- Adds directly with class synergy bonuses
- Example: +20% racial + +25% class = +45% total effectiveness

---

## 3. Stacking with Class Synergies

Racial and class bonuses stack additively with distinct mechanics:

### XP Acquisition

**Racial Affinity** affects BASE XP requirement:

```
Base XP Requirement × (1 + racial_modifier)
```

**Class Synergy** affects XP GAIN multiplier:

```
XP Gained × class_multiplier
```

**Combined Example:**

Dwarf Blacksmith learning a `Crafting/Smithing` skill:

1. **Base XP Requirement**: 1000 XP
2. **Racial Affinity (Strong +Crafting)**: 1000 × 0.7 = 700 XP required (30% reduction)
3. **Class Synergy (Blacksmith)**: 2x XP gain from forging actions
4. **Result**: Need only 350 forging actions (700 XP ÷ 2x) vs 1000 for non-synergy character

### Effectiveness Stacking

Bonuses add together directly:

```
Total Effectiveness = Base + Racial Bonus + Class Bonus
```

**Example:**

Dwarf Blacksmith using "Metal Sense":

- Base effectiveness: 100%
- Racial bonus (Strong +Crafting): +20%
- Class bonus (Blacksmith 15): +25%
- **Total: 145% effectiveness**

---

## 4. Complete Racial Affinity Matrices

### 4.1 Human

**Cultural Identity:** Adaptable generalists, excel through versatility

| Tag Category   | Affinity      | Notes                                            |
| -------------- | ------------- | ------------------------------------------------ |
| All categories | **−** Neutral | Universal adaptability - no bonuses or penalties |

**Racial Trait:** "Human Versatility"

- No racial affinities (all neutral)
- Can learn any skill at base rate
- No penalties or bonuses
- Excel through class synergies and flexibility

**Thematic Rationale:**
Humans adapt to any environment and profession through determination and versatility. They lack specialized racial traits but compensate through flexibility and drive.

---

### 4.2 Dwarf

**Cultural Identity:** Master craftsmen and warriors, especially with metal and stone

| Tag Category         | Affinity       | Notes                                              |
| -------------------- | -------------- | -------------------------------------------------- |
| **Crafting**         | **+** Strong   | All crafting skills; cultural emphasis on creation |
| **Physical**         | **+** Strong   | Stocky, strong build; natural physical prowess     |
| **Combat/Melee**     | **○** Positive | Warrior culture; skilled melee fighters            |
| **Gathering/Mining** | **○** Positive | Underground living; natural miners                 |
| **Magic**            | **×** Negative | Cultural distrust; biological resistance           |
| **Stealth**          | **×** Negative | Stocky build; cultural directness                  |

**Racial Traits:**

- "Forgeborn": Strong affinity with Crafting (especially metallurgy)
- "Stoneblood": Strong physical constitution
- "Magicbane": Resistant to magic but harder to learn

**Thematic Rationale:**
Dwarves excel at physical crafts, especially smithing and stonework. Their dense musculature makes them strong but not agile. Cultural values emphasize honest labor over sneakiness and physical craft over magic.

**Example Synergy:**

- Dwarf Blacksmith: Strong racial (Crafting) + Strong class (Crafting/Smithing) = Legendary smith
- Dwarf Mage: Negative racial (Magic) + Strong class (Magic) = Still capable but harder path

---

### 4.3 High Elf

**Cultural Identity:** Scholarly mages and refined artisans

| Tag Category          | Affinity       | Notes                                               |
| --------------------- | -------------- | --------------------------------------------------- |
| **Magic**             | **+** Strong   | Centuries of magical study; innate mana sensitivity |
| **Knowledge**         | **+** Strong   | Long-lived scholars; cultural emphasis on learning  |
| **Crafting**          | **○** Positive | Refined aesthetics; quality over quantity           |
| **Social/Persuasion** | **○** Positive | Diplomatic culture; silver-tongued nobility         |
| **Physical/Strength** | **×** Negative | Slender build; prioritize grace over power          |
| **Agriculture**       | **×** Negative | Aristocratic culture; view farming as beneath them  |

**Racial Traits:**

- "Arcane Heritage": Strong magical affinity
- "Timeless Wisdom": Strong knowledge acquisition
- "Refined Grace": Prefer finesse to strength

**Thematic Rationale:**
High Elves dedicate centuries to magical and scholarly pursuits. Their refined culture values elegance and intellect over physical labor. Slender builds make brute strength challenging.

---

### 4.4 Wood Elf

**Cultural Identity:** Forest dwellers in harmony with nature

| Tag Category             | Affinity       | Notes                                               |
| ------------------------ | -------------- | --------------------------------------------------- |
| **Magic**                | **+** Strong   | Natural magic affinity; druidic traditions          |
| **Nature**               | **+** Strong   | Forest culture; deep animal and plant understanding |
| **Gathering**            | **○** Positive | Foraging culture; natural resource awareness        |
| **Stealth**              | **○** Positive | Forest hunters; natural camouflage                  |
| **Crafting/Woodworking** | **○** Positive | **Specific cultural affinity** - wood-based crafts  |
| **Physical/Agility**     | **○** Positive | Nimble build; tree-climbing agility                 |
| **Combat/Melee**         | **×** Negative | Prefer ranged combat; lighter build                 |
| **Crafting/Smithing**    | **×** Negative | Forest culture; limited metal access                |

**Racial Traits:**

- "Forest's Child": Strong nature affinity
- "Woodshaper": Specific bonus to woodworking (cultural trait)
- "Silent Stride": Natural stealth in forests

**Thematic Rationale:**
Wood Elves live in forest canopies, developing harmony with nature and skill with wood. Their culture avoids heavy metalwork, preferring bows and wooden tools. The Woodworking bonus is **cultural** (learned tradition) rather than biological.

**Note on Specificity:**
`Crafting/Woodworking` is a **specific cultural affinity** - Wood Elves don't have general Crafting affinity, only this specialized traditional craft.

---

### 4.5 Dark Elf (Drow)

**Cultural Identity:** Underground survivors, masters of shadows

| Tag Category             | Affinity       | Notes                                            |
| ------------------------ | -------------- | ------------------------------------------------ |
| **Magic**                | **+** Strong   | Magical culture; innate dark magic affinity      |
| **Stealth**              | **+** Strong   | Underground hunting; natural shadow affinity     |
| **Combat/Assassination** | **○** Positive | Assassination culture; precision killing         |
| **Criminal**             | **○** Positive | Underground society; moral flexibility           |
| **Social/Deception**     | **○** Positive | Survival through manipulation                    |
| **Physical/Endurance**   | **×** Negative | Adapted to underground; less stamina in sunlight |
| **Agriculture**          | **×** Negative | Underground living; limited farming knowledge    |

**Racial Traits:**

- "Shadowborn": Strong stealth and magic affinity
- "Darkvision": Perfect underground vision
- "Sunlight Sensitivity": Reduced endurance in daylight

**Thematic Rationale:**
Dark Elves adapted to underground survival, developing stealth, magic, and morally flexible cultures. Sunlight sensitivity reduces their physical endurance above ground.

---

### 4.6 Orc

**Cultural Identity:** Warrior nomads valuing strength and honor

| Tag Category           | Affinity       | Notes                                                |
| ---------------------- | -------------- | ---------------------------------------------------- |
| **Physical**           | **+** Strong   | Massive build; incredible physical prowess           |
| **Combat**             | **+** Strong   | Warrior culture; lives for battle                    |
| **Gathering/Hunting**  | **○** Positive | Nomadic hunters; natural trackers                    |
| **Leadership/Command** | **○** Positive | Clan-based hierarchy; strong war chiefs              |
| **Magic**              | **×** Negative | Cultural distrust; "magic is for weaklings"          |
| **Crafting/Fine**      | **×** Negative | Value function over form; impatient with detail work |
| **Knowledge**          | **×** Negative | Oral tradition culture; written learning uncommon    |

**Racial Traits:**

- "Warrior's Blood": Strong combat and physical affinity
- "Iron Constitution": Natural toughness
- "Crude Efficiency": Struggle with fine detailed work

**Thematic Rationale:**
Orcs excel at physical combat and survival. Their culture values strength and direct action over subtlety and scholarship. Impatient temperament makes detailed craftwork frustrating.

---

### 4.7 Goblin

**Cultural Identity:** Scrappy survivors, clever tinkerers

| Tag Category           | Affinity       | Notes                                                       |
| ---------------------- | -------------- | ----------------------------------------------------------- |
| **Stealth**            | **+** Strong   | Small size; natural sneakiness                              |
| **Crafting/Tinkering** | **○** Positive | **Specific affinity** - improvised gadgets and contraptions |
| **Criminal**           | **○** Positive | Survival through theft; marginal social position            |
| **Gathering**          | **○** Positive | Scavenger culture; find value in trash                      |
| **Physical/Agility**   | **○** Positive | Quick and nimble                                            |
| **Physical/Strength**  | **×** Negative | Small size; limited strength                                |
| **Combat/Melee**       | **×** Negative | Small build; struggle in direct combat                      |
| **Leadership**         | **×** Negative | Social underclass; rarely respected as leaders              |

**Racial Traits:**

- "Shadow Skulker": Strong stealth affinity
- "Scrappy Tinkerer": Specific bonus to improvised crafting
- "Diminutive": Agile but weak

**Thematic Rationale:**
Goblins survive through stealth, scavenging, and cleverness. Small size makes them agile but physically weak. The Tinkering affinity reflects their creative improvisation with found materials.

**Note on Specificity:**
`Crafting/Tinkering` represents makeshift, improvised gadgets - different from formal smithing or woodworking.

---

### 4.8 Halfling

**Cultural Identity:** Peaceful farmers and social community builders

| Tag Category         | Affinity       | Notes                                     |
| -------------------- | -------------- | ----------------------------------------- |
| **Agriculture**      | **+** Strong   | Farming culture; green-thumb heritage     |
| **Social**           | **+** Strong   | Tight-knit communities; natural diplomats |
| **Stealth**          | **+** Strong   | Small size; naturally quiet               |
| **Crafting/Cooking** | **○** Positive | Food culture; excellent chefs             |
| **Trade**            | **○** Positive | Market-day culture; skilled barterers     |
| **Combat**           | **×** Negative | Peaceful culture; avoid conflict          |
| **Magic**            | **×** Negative | Practical culture; magic seems frivolous  |

**Racial Traits:**

- "Pastoral Heritage": Strong agricultural affinity
- "Community Heart": Strong social bonds
- "Little Folk": Natural stealth from small size

**Thematic Rationale:**
Halflings build peaceful farming communities, valuing food, fellowship, and comfort over adventure. Small size aids stealth but hinders combat. Practical nature makes abstract magic study frustrating.

---

### 4.9 Gnome

**Cultural Identity:** Inventive tinkerers and curious scholars

| Tag Category           | Affinity       | Notes                                                     |
| ---------------------- | -------------- | --------------------------------------------------------- |
| **Crafting/Tinkering** | **+** Strong   | **Specific affinity** - mechanical inventions and gadgets |
| **Magic**              | **+** Strong   | Curious minds; love magical experimentation               |
| **Knowledge**          | **+** Strong   | Insatiable curiosity; natural researchers                 |
| **Crafting**           | **○** Positive | General crafting aptitude                                 |
| **Physical/Strength**  | **×** Negative | Small size; limited physical power                        |
| **Combat**             | **×** Negative | Prefer cleverness to violence                             |

**Racial Traits:**

- "Ingenious Mind": Strong tinkering and magic affinity
- "Endless Curiosity": Strong knowledge acquisition
- "Diminutive Build": Physically weak but mentally sharp

**Thematic Rationale:**
Gnomes combine magical curiosity with mechanical ingenuity. They love complex problems and intricate mechanisms. Small size limits physical strength and combat effectiveness.

**Note on Tinkering:**
Gnome Tinkering is more sophisticated than Goblin Tinkering - clockwork mechanisms, enchanted gadgets, precision instruments vs improvised contraptions.

---

### 4.10 Beastkin

**Cultural Identity:** Varies by beast type, generally physically capable and nature-attuned

**Note:** Beastkin affinities vary by beast type (wolf, cat, bear, eagle, etc.). The matrix below shows common patterns, but specific subraces may vary.

| Tag Category          | Affinity                  | Notes                                                 |
| --------------------- | ------------------------- | ----------------------------------------------------- |
| **Physical**          | **+** Strong              | Animal heritage; superior physicality                 |
| **Nature**            | **+** Strong              | Animal instincts; natural world attunement            |
| **Gathering/Hunting** | **○** Positive            | Predator instincts (carnivore types)                  |
| **Combat/Melee**      | **○** Positive            | Natural weapons (claws, fangs)                        |
| **Stealth**           | **○** Positive            | Predator heritage (cat, wolf types)                   |
| **Magic**             | **×** Negative            | Instinct-driven; struggle with abstract study         |
| **Knowledge**         | **×** Negative            | Oral tradition; formal education uncommon             |
| **Crafting**          | **○** Neutral to Negative | Varies by subrace - predators struggle with fine work |

**Racial Traits (vary by beast type):**

- "Beast Heritage": Strong physical and nature affinity
- "Predator's Instinct": Combat and hunting bonuses (predator types)
- "Wild Mind": Struggle with abstract learning

**Thematic Rationale:**
Beastkin combine human intelligence with animal physicality and instincts. They excel at physical pursuits and nature-based skills but struggle with abstract intellectual pursuits. Specific affinities vary by animal type.

**Subrace Variations:**

**Wolf Beastkin:**

- Strong: Physical, Nature, Gathering/Hunting
- Positive: Combat/Melee, Stealth, Leadership (pack mentality)
- Negative: Magic, Knowledge

**Cat Beastkin:**

- Strong: Physical/Agility, Stealth
- Positive: Combat, Nature
- Negative: Magic, Knowledge, Leadership (solitary nature)

**Bear Beastkin:**

- Strong: Physical/Strength, Physical/Endurance
- Positive: Combat/Melee, Nature
- Negative: Stealth (large size), Magic, Knowledge

**Eagle Beastkin:**

- Strong: Physical/Agility, Nature
- Positive: Gathering, Combat/Ranged
- Negative: Physical/Strength (hollow bones), Magic, Knowledge

---

## 5. Racial-Class Combination Examples

### Strong Synergy (Race + Class Aligned)

**Dwarf Blacksmith:**

- Racial: +Crafting (Strong), +Physical (Strong)
- Class: Crafting/Smithing synergy
- Result: -30% XP requirement (racial), 2x XP gain (class), +20% racial + +25% class = +45% effectiveness
- **Outcome:** Legendary smiths, learn incredibly fast, produce superior work

**High Elf Mage:**

- Racial: +Magic (Strong), +Knowledge (Strong)
- Class: Magic synergy
- Result: -30% XP requirement (racial), 2x XP gain (class), +45% effectiveness
- **Outcome:** Archmages with centuries of practice, unmatched magical mastery

**Halfling Farmer:**

- Racial: +Agriculture (Strong)
- Class: Agriculture synergy
- Result: -30% XP requirement, 2x XP gain, +45% effectiveness
- **Outcome:** Green-thumb farmers producing abundant harvests

---

### Moderate Synergy (Partial Alignment)

**Human Blacksmith:**

- Racial: Neutral (all skills)
- Class: Crafting/Smithing synergy
- Result: Base XP requirement, 2x XP gain (class), +25% effectiveness (class only)
- **Outcome:** Competent smith through hard work, no racial advantage

**Wood Elf Ranger:**

- Racial: +Nature (Strong), +Gathering (Positive), +Stealth (Positive)
- Class: Nature, Gathering, Stealth synergies
- Result: Mixed bonuses, strong in nature/gathering, moderate elsewhere
- **Outcome:** Exceptional forest ranger, natural hunter

---

### Conflicting Synergy (Race vs Class)

**Dwarf Mage:**

- Racial: ×Magic (Negative) = +30% XP requirement, -10% effectiveness
- Class: Magic synergy = 2x XP gain, +25% effectiveness
- Result: Need 1300 XP instead of 1000, but gain 2x XP, net +15% effectiveness
- **Outcome:** Can become capable mage but harder path than elf mage

**Orc Scholar:**

- Racial: ×Knowledge (Negative) = +30% XP requirement, -10% effectiveness
- Class: Knowledge synergy = 2x XP gain, +25% effectiveness
- Result: Need 1300 XP with 2x gain = need 650 actions (vs 500 for neutral), +15% effectiveness
- **Outcome:** Viable but must overcome cultural biases and natural difficulties

**High Elf Warrior:**

- Racial: ×Physical/Strength (Negative) = +30% XP requirement, -10% effectiveness
- Class: Physical, Combat synergies = 2x XP gain, +25% effectiveness
- Result: Slower physical training but class synergies compensate
- **Outcome:** Capable warrior through technique and training, not raw strength

---

## 6. Design Philosophy

### Why Mostly Broad Affinities?

**Reasons for broad affinities:**

1. **Simplicity:** Easy to understand and apply
2. **Flexibility:** Doesn't pigeonhole races into narrow roles
3. **Biological Focus:** Represents innate physical/mental traits, not learned skills
4. **Culture-Agnostic:** Allows for cultural variation within races

**Example:**
Dwarves have +Crafting (broad) because their strength, patience, and cultural emphasis on creation apply to all crafting disciplines. A dwarf can be an excellent smith, jeweler, or carpenter.

### Why Some Specific Affinities?

**Reasons for specific affinities:**

1. **Cultural Traditions:** Deep cultural expertise (Wood Elf woodworking, Gnome clockwork)
2. **Unique Biology:** Specialized physical traits (Goblin makeshift tinkering)
3. **Flavor:** Makes races feel distinct and memorable
4. **Balance:** Prevents races from being strictly better or worse

**Example:**
Wood Elves have +Crafting/Woodworking (specific) because it's a deep cultural tradition passed through generations. They DON'T have general +Crafting because forest culture limits exposure to metalwork, stonework, etc.

### Penalties as Balance

Negative affinities balance strong positives and create interesting trade-offs:

- Dwarves excel at crafting but struggle with magic (anti-magic culture)
- Orcs dominate physical combat but find scholarly pursuits difficult
- Halflings build strong communities but avoid violence

**Design Goal:** Every race has paths they excel at and paths that challenge them.

---

## 7. Racial Skill Pools

### Overview

Each race has a **Racial Skill Pool** defined by skill tags. When a character selects their race during character creation, they receive **3 random skills** from this pool. Additional racial skills can be gained by reaching **racial milestones** during gameplay.

### Racial Skill Pool Mechanics

**At Character Creation:**

- Character receives 3 randomly selected skills from their racial skill pool
- Skills are chosen from all skills matching ANY of the race's pool tags
- No duplicates - each skill can only be selected once
- Skills are granted at base tier (Lesser/No Tier depending on skill type)

**Racial Milestones:**

- Characters gain additional racial skill selections at racial milestone levels
- Milestones occur at specific character levels or racial achievements
- Each milestone grants 1 additional skill from the racial pool
- Typical milestones: Level 10, 25, 50 (or equivalent racial achievements)

**Racial Skill Pool Tags:**

Each race defines which skill tags are included in their racial pool. Skills matching ANY of these tags are available for selection.

### Racial Skill Pool Tag Definitions

#### Human

**Racial Skill Pool Tags:** `Universal/Physical`, `Universal/Mental`, `Social`, `Physical`

**Philosophy:** Humans have access to broadly applicable universal skills reflecting their adaptability and social nature.

**Example Skills:** Ambidexterity, Iron Will, Night Vision, Danger Sense, Water Breathing, Inspiring Leader, Quick Learner

---

#### Dwarf

**Racial Skill Pool Tags:** `Physical/Endurance`, `Physical/Strength`, `Crafting`, `Gathering/Mining`, `Combat/Defense`

**Philosophy:** Dwarven racial skills emphasize physical toughness, craftsmanship, and defensive prowess.

**Example Skills:** Stout Constitution, Darkvision, Stone Sense, Master Crafter's Eye, Magic Resistance, Steady Stance, Dense Build

---

#### High Elf

**Racial Skill Pool Tags:** `Magic`, `Knowledge`, `Social/Persuasion`, `Physical/Agility`

**Philosophy:** Elven racial skills reflect centuries of magical study, scholarly pursuits, and refined grace.

**Example Skills:** Arcane Sensitivity, Timeless Wisdom, Elven Grace, Trance Meditation, Keen Senses, Diplomatic Bearing

---

#### Wood Elf

**Racial Skill Pool Tags:** `Nature`, `Physical/Agility`, `Gathering`, `Combat/Ranged`, `Stealth`

**Philosophy:** Wood Elven skills emphasize forest adaptation, natural harmony, and ranger capabilities.

**Example Skills:** Forest Stride, Beast Empathy, Natural Camouflage, Sure-Footed, Fleet of Foot, Woodland Sense

---

#### Kobold

**Racial Skill Pool Tags:** `Crafting`, `Gathering/Mining`, `Stealth`, `Physical/Agility`, `Knowledge`

**Philosophy:** Kobold skills reflect their tinkering nature, underground living, and clever resourcefulness.

**Example Skills:** Trap Sense, Scavenger's Eye, Pack Tactics, Sunlight Sensitivity (negative), Nimble Escape, Tinkerer's Intuition

---

#### Lizardfolk

**Racial Skill Pool Tags:** `Nature`, `Physical/Endurance`, `Combat`, `Gathering/Fishing`, `Physical/Strength`

**Philosophy:** Lizardfolk skills emphasize primal survival, physical prowess, and swamp/water adaptation.

**Example Skills:** Scaly Hide, Hold Breath, Bite Attack, Cold Vulnerability (negative), Swamp Strider, Primal Instinct

---

#### Orc

**Racial Skill Pool Tags:** `Combat`, `Physical/Strength`, `Physical/Endurance`, `Tribal`, `Physical`

**Philosophy:** Orcish skills emphasize raw physical power, combat prowess, and tribal warrior culture.

**Example Skills:** Relentless Endurance, Savage Attacks, Powerful Build, Aggressive Charge, Pain Tolerance, Battle Fury

---

#### Goblin

**Racial Skill Pool Tags:** `Stealth`, `Physical/Agility`, `Crafting`, `Criminal`, `Gathering`

**Philosophy:** Goblin skills reflect their sneaky nature, makeshift crafting, and survival instincts.

**Example Skills:** Nimble Escape, Fury of the Small, Scavenger, Makeshift Mastery, Shadow Skulker, Quick Reflexes

---

#### Troll

**Racial Skill Pool Tags:** `Physical/Endurance`, `Physical/Strength`, `Nature`, `Combat`, `Physical`

**Philosophy:** Troll skills emphasize regeneration, massive size, and raw physical dominance.

**Example Skills:** Rapid Regeneration, Massive Frame, Keen Smell, Fire Vulnerability (negative), Thick Skin, Regenerative Healing

---

#### Gnoll

**Racial Skill Pool Tags:** `Combat`, `Nature/Animals`, `Physical/Strength`, `Gathering/Hunting`, `Tribal`

**Philosophy:** Gnoll skills reflect pack hunting, hyena heritage, and savage combat style.

**Example Skills:** Pack Tactics, Rampage, Hyena Heritage, Keen Hearing, Scent Tracking, Savage Bite

---

#### Ogre

**Racial Skill Pool Tags:** `Physical/Strength`, `Physical/Endurance`, `Combat/Melee`, `Physical`

**Philosophy:** Ogre skills emphasize overwhelming physical power and durability over finesse.

**Example Skills:** Massive Strength, Thick Hide, Slow Wit (negative), Crushing Blows, Giant's Endurance, Intimidating Presence

---

### Racial Skills vs Class Skills

**Key Differences:**

| Aspect               | Racial Skills                               | Class Skills                                 |
| -------------------- | ------------------------------------------- | -------------------------------------------- |
| **Acquisition**      | 3 random at creation + milestones           | Offered at class level-up based on synergies |
| **Pool Definition**  | Defined by racial skill pool tags           | Defined by skill tags matching class tags    |
| **Progression**      | Fixed at creation, new skills at milestones | Continuous progression through class levels  |
| **Synergy Impact**   | No synergy multipliers                      | XP multipliers based on class tags           |
| **Racial Affinity**  | Always applies                              | Always applies                               |
| **Selection Method** | Random from pool                            | Offered selection from eligible skills       |

**Stacking:** Characters benefit from BOTH racial skills (from racial pool) AND class skills (from class progression), plus racial affinities apply to ALL skills regardless of source.

---

## 8. Implementation Notes

### Affinity Application Order

When calculating final values:

1. **Calculate base requirement:** Skill base XP × racial modifier
2. **Apply class XP multiplier:** Actions give XP × class multiplier
3. **Calculate effectiveness:** Base + racial % + class %

**Example Calculation:**

Wood Elf Ranger learning "Creature Lore" (Nature/Animals):

1. Base XP requirement: 1000 XP
2. Racial affinity (+Nature): 1000 × 0.7 = 700 XP required
3. Class synergy (Ranger): 2x XP gain from nature actions
4. Actions needed: 700 ÷ 2 = 350 actions
5. Effectiveness: 100% base + 20% racial + 25% class = 145%

### Level-Up Appearance Rates

Skills matching racial affinity appear more/less frequently at level-up:

```
Appearance Weight = Base Weight × Racial Modifier × Class Modifier

Racial Modifiers:
- Strong Positive (+): 2.0x
- Positive (○): 1.5x
- Neutral (−): 1.0x
- Negative (×): 0.5x
```

**Example:**

Dwarf Blacksmith level-up:

- Crafting skills: 2.0x (racial) × 2.0x (class) = 4.0x appearance rate
- Magic skills: 0.5x (racial) × 1.0x (no class) = 0.5x appearance rate

Result: Crafting skills appear 8 times more often than magic skills.

---

## 9. Racial Affinity Summary Table

| Race         | Strong Affinities (+)                | Positive Affinities (○)                                    | Negative Affinities (×)                     |
| ------------ | ------------------------------------ | ---------------------------------------------------------- | ------------------------------------------- |
| **Human**    | None                                 | None                                                       | None (Universal adaptability)               |
| **Dwarf**    | Crafting, Physical                   | Combat/Melee, Gathering/Mining                             | Magic, Stealth                              |
| **High Elf** | Magic, Knowledge                     | Crafting, Social/Persuasion                                | Physical/Strength, Agriculture              |
| **Wood Elf** | Magic, Nature                        | Gathering, Stealth, Crafting/Woodworking, Physical/Agility | Combat/Melee, Crafting/Smithing             |
| **Dark Elf** | Magic, Stealth                       | Combat/Assassination, Criminal, Social/Deception           | Physical/Endurance, Agriculture             |
| **Orc**      | Physical, Combat                     | Gathering/Hunting, Leadership/Command                      | Magic, Crafting/Fine, Knowledge             |
| **Goblin**   | Stealth                              | Crafting/Tinkering, Criminal, Gathering, Physical/Agility  | Physical/Strength, Combat/Melee, Leadership |
| **Halfling** | Agriculture, Social, Stealth         | Crafting/Cooking, Trade                                    | Combat, Magic                               |
| **Gnome**    | Crafting/Tinkering, Magic, Knowledge | Crafting                                                   | Physical/Strength, Combat                   |
| **Beastkin** | Physical, Nature                     | Gathering/Hunting, Combat/Melee, Stealth (varies by type)  | Magic, Knowledge                            |

---

## 10. Cross-References

**See Also:**

- [skill-tags.md](/workspaces/cozy-fantasy-rpg/docs/design/systems/character/skill-tags.md) - Hierarchical tag system
- [class-tag-associations.md](/workspaces/cozy-fantasy-rpg/docs/design/systems/character/class-tag-associations.md) - Class-to-tag mappings
- [class-progression.md](/workspaces/cozy-fantasy-rpg/docs/design/systems/character/class-progression.md) - Class synergy mechanics

---

**End of Racial Affinity System Documentation**
