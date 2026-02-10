---
title: 'Appraiser'
type: 'class'
category: 'trade'
tier: 2
prerequisite_xp: 5000
prerequisite_actions: appraisal
summary: 'Expert valuator determining authenticity, quality, and fair prices of goods'
tags:
  - Trade
  - Appraisal
  - Knowledge
---

# Appraiser

## Lore

### Origin

Value is subjective until expertise makes it objective. A painted canvas might be worthless decoration or priceless masterwork - only knowledge reveals the difference. An old sword could be rusty junk or legendary blade. A gemstone might be glass or fortune. Appraisers possess the expertise to determine truth, making them invaluable in economies where fraud is common and valuable items change hands regularly.

The Appraiser class emerged from specialized Traders who developed deep knowledge of specific goods. A jewel Trader handling thousands of stones learns to identify genuine gems from fakes instantly. A weapons dealer examining countless blades recognizes quality craftsmanship and historical significance. An art collector distinguishes masterworks from competent copies. This accumulated expertise becomes more valuable than the goods themselves - people pay generously for confident assessments.

True appraisal requires both knowledge and judgment. Book learning provides fundamentals - identifying materials, recognizing styles, understanding construction techniques. Experience develops intuition - sensing when something feels wrong despite appearing correct, recognizing subtle signs of quality or fraud. Master Appraisers combine encyclopedic knowledge with refined instincts, identifying items others can't categorize and detecting forgeries craftsmen consider perfect.

### In the World

Every market of substance employs Appraisers. Merchants hire them to evaluate potential purchases, avoiding expensive mistakes. Nobles consult them before acquiring artworks or heirlooms. Adventurers seek their services to assess dungeon treasures. Courts call them as expert witnesses in fraud cases. Insurance companies use them to verify claimed values. Anyone dealing in valuable goods eventually needs an Appraiser's expertise.

Most Appraisers specialize - the breadth of potential items exceeds any individual's capacity for mastery. Gem Appraisers focus solely on precious stones and jewelry. Weapons Appraisers know arms and armor intimately. Art Appraisers evaluate paintings, sculptures, and decorative pieces. Antiquities Appraisers specialize in historical artifacts. Each specialty requires years of study and practical experience before reliable expertise develops.

The profession demands absolute integrity. Appraisers who overvalue items to please clients lose credibility quickly. Those who undervalue goods to benefit buyers develop bad reputations. Honest Appraisers sometimes deliver unwelcome news - the "priceless heirloom" is actually common, the "investment piece" is worthless. Maintaining honesty despite pressure requires both professional ethics and financial security to refuse compromising jobs.

Apprentice Appraisers train under established experts for years, learning to identify genuine items versus fakes, recognize quality levels, assess condition impacts, and understand market values. They study authentic pieces extensively, building mental catalogs of legitimate examples. They learn common fraud techniques and how to detect them. Eventually they develop confidence to render independent judgments, though wise Appraisers continue consulting references and colleagues when uncertain.

Expert Appraisers become famous within their specialties. Their authentication stamps increase item values significantly - buyers trust their judgments. They're consulted on rare pieces even by other Appraisers. They write reference texts other professionals study. They develop such refined expertise they identify items from brief descriptions or poor sketches. Their knowledge becomes irreplaceable institutional memory preserving understanding across generations.

---

## Mechanics

### Prerequisites

| Requirement        | Value                                             |
| ------------------ | ------------------------------------------------- |
| XP Threshold       | 5,000 XP from appraisal tracked actions           |
| Related Foundation | [Trader](../trader/) (optional, provides bonuses) |
| Tag Depth Access   | 2 levels (e.g., `[Trade]`)                        |

### Requirements

| Requirement       | Value                                     |
| ----------------- | ----------------------------------------- |
| Unlock Trigger    | Assess item values, identify authenticity |
| Primary Attribute | INT (Intelligence), AWR (Awareness)       |
| Starting Level    | 1                                         |
| Tools Required    | Examination tools, reference materials    |
| Prerequisite      | Often requires [Trader](../trader/) class |

### Stats

#### Base Class Stats

| Level | HP Bonus | Stamina Bonus | Trait                |
| ----- | -------- | ------------- | -------------------- |
| 1     | +5       | +10           | Apprentice Appraiser |
| 10    | +15      | +30           | Journeyman Appraiser |
| 25    | +30      | +65           | Master Appraiser     |
| 50    | +55      | +115          | Legendary Appraiser  |

#### Appraisal Bonuses

| Class Level | Accuracy | Fraud Detection | Specialization | Market Knowledge |
| ----------- | -------- | --------------- | -------------- | ---------------- |
| 1-9         | +15%     | +20%            | 2 types        | +10%             |
| 10-24       | +35%     | +50%            | 4 types        | +25%             |
| 25-49       | +65%     | +85%            | 7 types        | +50%             |
| 50+         | +95%     | +100%           | All types      | +85%             |

#### Essential Starting Skills

| Skill              | Type     | Source           | Effect                                    |
| ------------------ | -------- | ---------------- | ----------------------------------------- |
| Market Sense       | Lesser   | Trade            | Understanding value and market pricing    |
| Material Intuition | Mechanic | Crafting-General | Assessing quality and material properties |

#### Synergy Skills

Skills that have strong synergies with Appraiser. These skills can be learned by any character, but Appraisers gain significant bonuses (2x XP acquisition, enhanced effectiveness, reduced costs). Higher Appraiser levels provide stronger synergy bonuses.

**Trade Skills**:

- [Market Sense](../../skills/tiered/market-sense.md) - Lesser/Greater/Enhanced - Know accurate market values
- [Silver Tongue](../../skills/tiered/silver-tongue.md) - Lesser/Greater/Enhanced - Explain valuations convincingly
- [Trade Network](../../skills/passive-generator/trade-network.md) - Passive Generator - Contacts for rare item sales
- [Trade Contacts](../../skills/passive-generator/trade-contacts.md) - Passive Generator - Specialized collectors and dealers
- [Commodity Speculation](../../skills/mechanic-unlock/commodity-speculation.md) - Lesser/Greater/Enhanced - Predict value trends

#### Synergy Bonuses

Appraiser provides context-specific bonuses to appraisal-related skills based on logical specialization:

**Core Trade Skills** (Direct specialization - Strong synergy):

- **Market Valuation**: Essential for determining fair prices
  - Faster learning: 2x XP from appraisal actions (scales with class level)
  - Better effectiveness: +25% accuracy at Appraiser 15, +35% at Appraiser 30+
  - Reduced cost: -25% stamina at Appraiser 15, -35% at Appraiser 30+
- **Fraud Detection**: Directly tied to authentication expertise
  - Faster learning: 2x XP from examination actions
  - Better detection rate: +30% fraud detection at Appraiser 15
  - Lower failure rate: -50% chance of missing forgeries
- **Quality Assessment**: Core skill for evaluating items
  - Faster learning: 2x XP from assessment actions
  - Better accuracy: +20% quality judgment at Appraiser 15
  - Faster assessment: -20% time required at Appraiser 15
- **Material Knowledge**: Fundamental to accurate appraisals
  - Faster learning: 2x XP from material examination
  - Better identification: +25% material identification at Appraiser 15
  - More details: +50% chance to identify rare materials

**Synergy Strength Scales with Class Level**:

| Appraiser Level | XP Multiplier | Effectiveness Bonus | Cost Reduction | Example: Market Valuation |
| --------------- | ------------- | ------------------- | -------------- | ------------------------- |
| Level 5         | 1.5x (+50%)   | +15%                | -15% stamina   | Good but moderate         |
| Level 10        | 1.75x (+75%)  | +20%                | -20% stamina   | Strong improvements       |
| Level 15        | 2.0x (+100%)  | +25%                | -25% stamina   | Excellent synergy         |
| Level 30+       | 2.5x (+150%)  | +35%                | -35% stamina   | Masterful synergy         |

**Example Progression**:

An Appraiser 15 learning Market Valuation:

- Performs 50 appraisal actions (earning 2x XP = 1000 XP total, vs 500 XP for non-Appraiser)
- Market Valuation available at level-up after just 500 XP (vs 1500 XP for non-trade class)
- When using Market Valuation: Base +30% accuracy becomes +55% accuracy (base +25% synergy)
- Stamina cost: 7.5 stamina instead of 10 (25% reduction)

#### Tracked Actions

Actions that grant XP to the Appraiser class:

| Action Category        | Specific Actions                                   | XP Value                  |
| ---------------------- | -------------------------------------------------- | ------------------------- |
| Item Appraisal         | Assess item values accurately                      | 5-40 per item             |
| Fraud Detection        | Identify counterfeit or misrepresented items       | 15-80 per detection       |
| Expert Consultation    | Provide expert assessments on valuable items       | 20-100 per consultation   |
| Authentication         | Verify authenticity of questioned items            | 15-70 per authentication  |
| Market Analysis        | Research and determine current market values       | 10-50 per analysis        |
| Historical Research    | Identify historical or antique items               | 20-100 per identification |
| Specialization         | Develop deep expertise in specific item categories | 30-150 per mastery        |
| Teaching               | Train others in appraisal techniques               | 10-40 per session         |
| Rare Discovery         | Identify extremely valuable or rare items          | 50-300 per discovery      |
| Professional Testimony | Provide expert testimony in disputes               | 30-150 per case           |

#### Class Consolidation

See [Class Consolidation System](../../../systems/character/class-consolidation.md) for full mechanics.

| Consolidation Path                             | Requirements                          | Result Class      | Tier |
| ---------------------------------------------- | ------------------------------------- | ----------------- | ---- |
| [Master Appraiser](../consolidation/index.md)  | Appraiser + multiple specializations  | Master Appraiser  | 1    |
| [Auction Master](../consolidation/index.md)    | Appraiser + [Merchant](./merchant.md) | Auction Master    | 1    |
| [Antiquarian](../consolidation/index.md)       | Appraiser + historical focus          | Antiquarian       | 1    |
| [Gemologist Master](../consolidation/index.md) | Appraiser + exclusive gem focus       | Gemologist Master | 1    |

### Interactions

| System                                                           | Interaction                              |
| ---------------------------------------------------------------- | ---------------------------------------- |
| [Economy](../../../systems/economy/index.md)                     | Establishes accurate market values       |
| [Trade & Pricing](../../../systems/economy/trade-and-pricing.md) | Prevents fraud and overpricing           |
| [Crafting](../../../systems/crafting/crafting-progression.md)    | Evaluates crafted item quality           |
| [Enchantment](../../../systems/crafting/index.md)                | Assesses magical item properties         |
| [Social](../../../systems/social/index.md)                       | Expert authority in trade disputes       |
| [Reputation](../../../systems/social/factions-reputation.md)     | Expertise builds professional reputation |

---

## Related Content

- **Requires:** Examination tools (magnifiers, scales, reagents), reference materials
- **Equipment:** [Magnifying Glass](../../items/index.md), [Scales](../../items/index.md), [Reference Books](../../items/index.md)
- **Prerequisite:** Often consolidates from [Trader](../trader/) with specialization
- **Synergy Classes:** [Trader](../trader/), [Merchant](./merchant/), [Jeweler](../crafting/jeweler.md)
- **Consolidates To:** [Master Appraiser](../consolidation/index.md), [Auction Master](../consolidation/index.md), [Antiquarian](../consolidation/index.md)
- **See Also:** [Economy System](../../../systems/economy/index.md), [Item Quality](../../../systems/crafting/index.md), [Fraud Prevention](../../../systems/economy/index.md)
