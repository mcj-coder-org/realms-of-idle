# Inn/Tavern Management - Idle Game Design Document

## Core Fantasy
You are an [Innkeeper] in a world where Classes and Levels determine everything. Your establishment grows from a roadside waystation to a legendary hub where heroes gather, secrets are exchanged, and the fate of nations is decided over hearty meals. Inspired by *The Wandering Inn*, this scenario emphasizes hospitality, reputation, and the power of a place that feels like home.

---

## Primary Game Loop

```
SERVE → EARN → UPGRADE → ATTRACT → SERVE (improved)
```

1. **Guests Arrive** (passive, time-based)
   - Travelers, adventurers, merchants, and unusual visitors appear based on reputation and location
   - Each guest type has needs (food, drink, lodging, information, safety)

2. **Fulfill Needs** (semi-automated with active optimization)
   - Kitchen produces meals from ingredients
   - Bar serves drinks from stock
   - Rooms provide rest (quality affects satisfaction)
   - Staff handle service (speed/quality trade-offs)

3. **Earn Resources**
   - Gold from payments
   - Reputation from satisfied guests
   - Secrets/rumors from conversations
   - Favors from notable patrons

4. **Upgrade & Expand**
   - Improve rooms, kitchen, common area
   - Hire and train staff
   - Unlock special amenities (bathhouse, stable, private meeting rooms)

5. **Attract Better Guests**
   - Higher reputation draws wealthier/rarer visitors
   - Specialization attracts specific clientele (adventurer inn vs. merchant rest stop vs. noble retreat)

---

## Resource Systems

### Primary Resources
| Resource | Generation | Primary Use |
|----------|------------|-------------|
| **Gold** | Guest payments | Upgrades, supplies, wages |
| **Reputation** | Guest satisfaction, events | Unlocks guest tiers, locations |
| **Ingredients** | Suppliers, gardens, guests | Cooking, special recipes |
| **Rumors/Secrets** | Conversations, events | Quests, unlocks, trade |

### Secondary Resources
- **Staff Energy**: Depletes during service, regenerates during downtime
- **Cleanliness**: Degrades over time, affects satisfaction
- **Ambiance**: Cumulative from decorations, entertainment, lighting
- **Safety Rating**: Affects who will stay; too low attracts trouble, too high bores adventurers

---

## Progression Systems

### Innkeeper Class Levels
Your personal level unlocks abilities:
- **[Basic Innkeeping]** → [Crowd Control] → [Legendary Hospitality]
- Skills like [Soothing Presence], [Remember Every Face], [The House Always Provides]
- Milestone abilities: "Guests heal 10% faster," "Kitchen never runs out of bread"

### Establishment Tiers
| Tier | Name | Capacity | Notable Features |
|------|------|----------|------------------|
| 1 | Roadside Shack | 5 guests | Basic hearth, communal sleeping |
| 2 | Traveler's Rest | 15 guests | Private rooms, stable |
| 3 | Adventurer's Haven | 40 guests | Quest board, secure storage |
| 4 | The Grand Inn | 100 guests | Multiple wings, bathhouse, entertainment |
| 5 | Legendary Establishment | 250+ guests | Pocket dimension rooms, teleportation access |

### Staff Development
- Hire NPCs with their own Classes: [Barmaid], [Cook], [Bouncer], [Stablehand]
- Staff level up through work, unlocking efficiency bonuses
- Rare staff have unique abilities (a [Goblin Chef] with monster cuisine, a retired [Assassin] bouncer)

---

## Prestige System: "Grand Reopening"

### Trigger Conditions
- Reach maximum reputation in current location
- Complete a "Legendary Guest" storyline
- Survive a catastrophic event (dragon attack, plague, war)

### Reset Mechanics
- Inn resets to Tier 1
- Retain: Innkeeper skills, recipe knowledge, staff contacts, a percentage of gold
- Gain: "Legacy Points" spent on permanent bonuses
- Unlock: New starting locations with different guest pools

### Legacy Bonuses
- "Old Regular" - Start with loyal customers
- "Famous Recipes" - Begin with advanced dishes unlocked
- "Family Business" - Staff start at higher levels
- "Storied Walls" - Base reputation bonus

---

## Active vs. Idle Balance

### Idle Mechanics (Offline Progress)
- Guests continue arriving at base rate
- Staff serve automatically (reduced efficiency)
- Kitchen produces from queue
- Gold and reputation accumulate (capped)

### Active Engagement Rewards
- **Personal Service**: Serving guests yourself grants bonus tips and reputation
- **Conversation Minigame**: Extract better rumors, build relationships
- **Crisis Management**: Handle bar fights, fires, difficult guests for major bonuses
- **Menu Optimization**: Adjust offerings based on current guest composition
- **Event Hosting**: Organize festivals, tournaments, performances

### Hybrid Activities
- **Recipe Discovery**: Combine ingredients (active) then auto-produce (idle)
- **Staff Scheduling**: Set shifts (active) that run automatically (idle)

---

## Event System

### Regular Events
- **Rush Hour**: 2x guests for limited time, tests capacity
- **Food Critic Visit**: High-stakes service for reputation multiplier
- **Traveling Merchant**: Rare ingredients available for limited time

### Story Events (Triggered by Reputation/Progression)
- A [Hero] party chooses your inn as headquarters
- A [King] travels incognito and tests your hospitality
- A monster requests lodging—do you serve all?
- War comes to your doorstep; your inn becomes a refuge

### Seasonal Events
- Harvest Festival: Food-focused bonuses
- Adventurer's Summit: Combat-class guests surge
- Merchant Caravan Season: Trade goods abundant

---

## Guest Type Examples

| Guest Type | Needs | Pays | Special |
|------------|-------|------|---------|
| Traveler | Basic food, bed | Low | Common, easy to please |
| Adventurer | Hearty meals, secure storage, rumors | Medium | May cause trouble or solve problems |
| Merchant | Quality room, private meeting space | High | Offers trade deals |
| Noble | Luxury everything | Very High | Demands perfection |
| Monster (friendly) | Unusual dietary needs | Variable | Affects reputation both ways |
| Named Character | Unique requirements | Story rewards | Unlocks questlines |

---

## Synergies with Other Scenarios

- **Adventurer Guild**: Your inn can host a guild branch; quests completed there generate guests
- **Merchant Caravan**: Caravans stop at your inn, providing supplies and customers
- **Alchemy**: On-site brewery/potion corner attracts specific clientele
- **Territory Building**: Your inn can be placed in your domain, benefiting from infrastructure

---

## Monetization Hooks (If F2P)
- Cosmetic inn decorations
- Speed-up construction timers
- Premium staff skins (not power)
- Expansion to additional inn locations earlier

---

## Sample Play Session

**Morning (5 min active)**
- Check overnight earnings (1,200 gold, +15 reputation)
- Resolve event: A [Bard] wants to perform tonight—approve for ambiance boost
- Adjust menu: Adventurer party arriving, add hearty stew to rotation
- Queue kitchen: 50 stews, 30 ales, 20 bread loaves

**Day (idle)**
- Guests served automatically
- Kitchen produces queued items
- Staff gain experience

**Evening (10 min active)**
- Bard performance event: Tap to encourage crowd, earn bonus tips
- Conversation with mysterious guest: Choose dialogue options, gain rare rumor
- Handle bar fight: Quick minigame, bouncer levels up
- Review daily summary: 3,400 gold earned, +42 reputation, one staff level-up
