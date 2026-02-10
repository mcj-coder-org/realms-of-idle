---
type: scenario
scope: detailed
status: draft
version: 1.0.0
created: 2026-02-02
updated: 2026-02-10
subjects: [inn, management, social]
dependencies: [idle-game-overview]
---

# Inn/Tavern Management - Idle Game Design Document

## Core Fantasy

You are an [Innkeeper] running a cozy tavern that grows into the heart of a bustling town. Balance comfort, food, drink, and entertainment to keep guests happy, attract VIPs, and build a reputation that turns your inn into a legendary destination.

---

## Primary Game Loop

```
STOCK → SERVE → SATISFY → REINVEST → EXPAND → ATTRACT (better)
```

1. **Stock Supplies** (active purchasing + passive production)
   - Ingredients, beverages, linens, firewood
   - Supplier deals and seasonal availability

2. **Serve Guests** (idle operations)
   - Rooms, meals, drinks
   - Staff handle routine service

3. **Satisfy & Review** (active touchpoints)
   - Resolve complaints
   - Customize service bundles
   - Monitor reputation

4. **Reinvest** (active management)
   - Upgrade facilities
   - Hire staff
   - Add new amenities

5. **Expand** (progression)
   - More rooms, better kitchen, entertainment hall
   - Unlock VIP clientele and special events

---

## Resource Systems

### Primary Resources

| Resource       | Generation                  | Primary Use               |
| -------------- | --------------------------- | ------------------------- |
| **Gold**       | Room fees, food, drinks     | Supplies, upgrades, staff |
| **Reputation** | Guest satisfaction, events  | VIP access, contracts     |
| **Supplies**   | Purchases, local production | Service capacity          |
| **Comfort**    | Rooms, decor, cleanliness   | Guest retention, tips     |

### Secondary Resources

- **Staff Morale**: Affects speed, errors, and guest moods
- **Entertainment**: Bards, games, performances drive revenue spikes
- **Renown Tokens**: Milestone rewards used for permanent perks

---

## Inn Systems

### Guest Types

| Guest Type      | Needs                     | Pays     | Notes                   |
| --------------- | ------------------------- | -------- | ----------------------- |
| **Travelers**   | Basic room, simple meals  | Low      | High volume             |
| **Adventurers** | Sturdy rooms, hearty food | Medium   | Buy supplies and rumors |
| **Merchants**   | Comfort, security         | Medium+  | Offer trade contracts   |
| **Nobles**      | Luxury, privacy, service  | High     | Reputation-sensitive    |
| **Mystics**     | Quiet, rare drinks        | Variable | Unlock unique events    |

### Facilities

| Facility        | Function            | Upgrade Benefits              |
| --------------- | ------------------- | ----------------------------- |
| **Guest Rooms** | Core revenue        | Capacity, comfort, VIP access |
| **Kitchen**     | Meals and buffs     | Faster service, better tips   |
| **Brewery**     | Drinks and specials | Higher margins, rare recipes  |
| **Common Hall** | Social hub          | Entertainment income          |
| **Bathhouse**   | Premium amenity     | Reputation boosts             |
| **Stables**     | Traveler services   | More guests, travel contracts |

### Staff Roles

- **Innkeeper**: Oversees operations, unlocks perks
- **Cook**: Increases meal quality and speed
- **Barkeeper**: Improves drink revenue
- **Housekeeper**: Maintains cleanliness and comfort
- **Bouncer**: Reduces incidents, protects reputation
- **Entertainer**: Generates tips and event bonuses

---

## Prestige System: "Legendary Inn Charter"

### Trigger Conditions

- Reach maximum reputation tier
- Host a royal summit event
- Maintain 100% satisfaction for 30 days

### Reset Mechanics

- Inn resets to basic facilities
- Retain: Recipes, staff training tiers, a portion of gold
- Gain: **Charter Seals** for permanent bonuses

### Legacy Bonuses

- "House of Welcome" - Base guest satisfaction increased
- "Trusted Cellar" - Premium drink margins improved
- "Famous Hearth" - Faster reputation growth
- "Efficient Staff" - Permanent service speed boost

---

## Active vs. Idle Balance

### Idle Mechanics

- Guests are served automatically by staff
- Supplies consumed and revenue generated
- Rooms filled based on reputation

### Active Engagement Rewards

- **Special Service**: Personally handle VIPs for bonus reputation
- **Menu Curation**: Rotate specials for profit spikes
- **Conflict Resolution**: Prevent incidents and reputation loss
- **Event Hosting**: Schedule tavern nights and festivals

---

## Event System

### Regular Events

- **Travel Rush**: Increased traveler volume
- **Festival Week**: Higher entertainment income
- **Supply Shortage**: Costs spike, encourages planning

### Story Events

- A famous hero requests a private banquet
- A rival inn attempts sabotage
- A mysterious patron offers a dangerous contract

### Seasonal Events

- **Winter Warmth**: Hot drinks in high demand
- **Spring Pilgrims**: Full occupancy, steady tips
- **Autumn Feast**: Premium ingredient bonuses

---

## Synergies with Other Scenarios

- **Adventurer Guild**: Quest traffic boosts occupancy
- **Alchemy**: Specialty tonics and drinks raise guest satisfaction
- **Merchant Caravan**: Trade contracts for cheaper supplies
- **Territory**: Town upgrades improve guest flow

---

## Sample Play Session

**Morning (5 min)**

- Collect overnight revenue: 1,850 gold
- Restock kitchen supplies
- Check guest reviews: reputation +3

**Midday (10 min)**

- Host a bard performance (tips surge)
- Upgrade rooms to improve comfort
- Accept merchant contract for bulk meals

**Evening (5 min)**

- Resolve a tavern dispute to avoid reputation loss
- Set menu specials for the night
- Schedule a festival event for tomorrow

**Overnight (idle)**

- Guests stay and generate revenue
- Staff clean and restock
- Reputation trends update
