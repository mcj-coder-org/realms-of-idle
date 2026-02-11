<!-- ADAPTATION REQUIRED -->
<!-- This file was migrated from source but needs manual review: -->
<!-- - Update terminology (dormant classes, XP split, etc.) -->
<!-- - Align with current GDD architecture -->
<!-- - Add missing sections as needed -->
<!-- - Update frontmatter with correct gdd_ref -->

---

title: Economy and Trade System
type: system
summary: Currency, dynamic pricing, trade classes, shops, caravans, and merchant guilds

---

# Economy & Trade System

## 1. Currency

| Denomination | Value      | Typical Use                                |
| ------------ | ---------- | ------------------------------------------ |
| Copper       | 1          | Common goods, food, basic supplies         |
| Silver       | 10 copper  | Quality goods, services, daily wages       |
| Gold         | 100 copper | Luxury items, property, large transactions |

Physical currency can be stolen/looted. Deposit in Merchant Guild bank for safety.

---

## 2. Dynamic Pricing

### Price Factors

| Factor               | Effect                        |
| -------------------- | ----------------------------- |
| Local supply         | Abundant = cheap              |
| Local demand         | Needed = expensive            |
| Distance from source | Further = more expensive      |
| Settlement size      | Cities have more competition  |
| Trade route status   | Disruption = price spike      |
| Events               | Mine collapse = ore expensive |

### Regional Specialization

Each region has exports/imports based on local resources:

- Coastal: exports fish, imports ore
- Mining: exports ore/gems, imports food
- Agricultural: exports food, imports manufactured goods

---

## 3. Trade Class Progression

### Trader Class

```
Barter goods repeatedly
    ↓ (10+ successful barters)
Trader class offered
    ↓ (accumulate trade value/profit)
Merchant class offered (1000+ silver value OR 200+ profit)
    ↓ (at Merchant, choose specialization)
<Specialty> Apprentice → Journeyman → Master
    ↓ (all specialties at Master)
Guildmaster eligible
```

### Merchant Specializations

| Specialty    | Focus             | Typical Goods               |
| ------------ | ----------------- | --------------------------- |
| Agricultural | Farm produce      | Crops, animals, seeds       |
| Mineral      | Ore and gems      | Raw ore, ingots, gems       |
| Artisan      | Crafted goods     | Weapons, armor, tools       |
| Alchemical   | Potions, reagents | Potions, herbs, chemicals   |
| Exotic       | Rare imports      | Spices, art, magical items  |
| Flesh        | Black market      | Body parts, slaves, corpses |

### Trader/Merchant Skills

| Stage     | Example Skills                                   |
| --------- | ------------------------------------------------ |
| Trader    | "Keen Eye" — see true item value                 |
| Trader    | "Haggler" — bonus to barter exchanges            |
| Merchant  | "Bulk Discount" — better rates on quantities     |
| Merchant  | "Market Sense" — see regional price differences  |
| Specialty | "Agricultural Network" — know all farm suppliers |
| Master    | "Price Setter" — influence regional prices       |

---

## 4. Caravaner Class

### Unlock

```
Acquire caravan (purchase, inherit, build)
    ↓
Caravaner class offered
    ↓
Levels through successful trade journeys
```

### Caravaner vs Merchant

| Aspect   | Merchant            | Caravaner                     |
| -------- | ------------------- | ----------------------------- |
| Focus    | Stationary trade    | Mobile trade                  |
| Location | Based in settlement | Travels between               |
| Party    | Shop assistants     | Caravan guards                |
| Income   | Shop profits        | Trade margins, transport fees |

### Caravaner Progression & Upgrades

| Level | Unlocks                        |
| ----- | ------------------------------ |
| 1-3   | Pack animal, small cart        |
| 4-6   | Wagon, 1-2 guards              |
| 7-9   | Large wagon, 3-4 guards        |
| 10-12 | Multiple wagons, 5-6 guards    |
| 13-15 | Armored wagon, outriders       |
| 16-18 | Convoy (multiple large wagons) |
| 19-20 | Trade fleet, permanent staff   |

### Caravaner Income Streams

| Service               | Payment                            |
| --------------------- | ---------------------------------- |
| Trading               | Profit margin (buy low, sell high) |
| Goods transport       | Fee per weight/distance            |
| Passenger transport   | Fee per person/distance            |
| Mail/message delivery | Fee per item/urgency               |
| Special delivery      | Premium fee                        |

---

## 5. Shops & Merchants

### Inventory Sources

| Source           | Price Level |
| ---------------- | ----------- |
| Local production | Cheapest    |
| Regional trade   | Moderate    |
| Caravan imports  | Higher      |
| Rare/exotic      | Highest     |

### Shop Mechanics

- Dynamic inventory based on local production + caravans
- Shops can run out of money → barter only
- Restock based on caravan arrivals and local suppliers

---

## 6. Trade Routes & Caravans

### Establishing Routes

```
Identify two settlements
    ↓
Contract with caravan company OR own caravan
    ↓
Negotiate terms
    ↓
Caravan runs route
    ↓
Profits/losses calculated per trip
```

### Caravan Components

| Component | Description                       |
| --------- | --------------------------------- |
| Wagons    | Determines cargo capacity         |
| Goods     | What's transported                |
| Guards    | Protection level                  |
| Driver    | Skill affects speed, accidents    |
| Route     | Safer = longer, shorter = riskier |

### Caravan Risks

- Bandit/raider attacks
- Weather delays
- Accidents
- Guard quality affects outcomes

### Attack Consequences

| Outcome       | Result                          |
| ------------- | ------------------------------- |
| Guards win    | Caravan continues, minor losses |
| Attackers win | Caravan looted/destroyed        |
| Stalemate     | Partial losses, continues       |

### Destroyed Caravan Response

- Settlement guards alerted
- Bounty posted
- Merchant guild notified
- Merchant pays replacement cost
- Supply disruption, price increases

---

## 7. Price Controls

### Settlement Controls

| Control          | Purpose                         |
| ---------------- | ------------------------------- |
| Price floor      | Protect local producers         |
| Price ceiling    | Protect consumers on essentials |
| Luxury exemption | No controls on non-essentials   |

### Authorities

| Authority         | Controls                   |
| ----------------- | -------------------------- |
| Settlement leader | Overall policy, essentials |
| Merchant Guild    | Member prices, trade goods |
| Market forces     | Unregulated, black market  |

---

## 8. Merchant Guild

### Services

| Service             | Description                    |
| ------------------- | ------------------------------ |
| Banking             | Deposit, withdrawal, transfer  |
| Trade regulation    | Standards, dispute resolution  |
| Price coordination  | Prevent ruinous competition    |
| Information sharing | Prices, route dangers          |
| Insurance           | Cover caravan losses (for fee) |

### Membership Tiers

| Tier          | Benefits                            |
| ------------- | ----------------------------------- |
| Non-member    | Can trade, no protections           |
| Associate     | Basic banking, market access        |
| Member        | Full banking, insurance, price info |
| Senior Member | Voting, discounts, priority         |
| Officer       | Policy influence                    |
| Guildmaster   | Leads local chapter                 |

### Guild Leader Requirements

- Artificer OR high-level Merchant
- Trader class (independent trading, not just guild orders)
- Guild standing
- Settlement approval

---

## 9. Bartering

- Supported between any characters
- Skills affect exchange rates (bonus on acceptance)
- Trading partner not "cheated" — skills provide bonuses
- Trades can be refused based on NPC needs/personality
- Shops resort to barter when out of currency

---

## 10. Temporary Trading

| Setup Type     | Requirements          | Duration       |
| -------------- | --------------------- | -------------- |
| Roadside       | None                  | Hours          |
| Market stall   | Fee, permit           | Days           |
| Caravan market | Settlement permission | While present  |
| Trade fair     | Special event         | Event duration |

Caravaners can set up as temporary traders at any settlement.
