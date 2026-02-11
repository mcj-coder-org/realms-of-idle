---
title: Innkeeper
gdd_ref: systems/core-progression-system-gdd.md#general
---

# Innkeeper

## Lore

### Origin

The Innkeeper stands at the crossroads of civilization - where travelers rest, locals gather, and stories are shared. More than just managers of room and board, innkeepers create sanctuaries from the road's hardships. The warm glow of inn windows promises safety, comfort, and a hot meal after long journeys.

Unlike specialized staff who master specific domains, innkeepers maintain competence across all aspects of inn operation. They can cook a basic meal when the kitchen is swamped, prepare a room when housekeepers are busy, and serve customers when servers are overwhelmed. This generalist approach makes innkeepers invaluable as managers - they understand every role because they can perform each one adequately, though specialists outperform them in focused tasks.

The Innkeepers' Association teaches this philosophy of "jack-of-all-trades" leadership. An innkeeper who cannot recognize when food is burning lacks authority in the kitchen. One who has never scrubbed floors cannot fairly judge a housekeeper's work. One who has never handled a rush cannot properly schedule servers. Competence across all domains earns staff respect and enables effective management.

### In the World

Every settlement needs an inn. Not just for travelers, though they bring coin and news from distant lands, but as community gathering places. The common room where farmers share harvest tales, where merchants strike deals, where adventurers recruit companions. The innkeeper knows everyone's stories, holds everyone's secrets, and ensures everyone leaves satisfied.

Children gather outside the inn on festival nights, drawn by music and laughter. Farmers arrive seeking hot meals after market days. Travelers stumble in weary and leave refreshed. The innkeeper orchestrates all of it: coordinating staff, managing supplies, greeting guests, resolving conflicts, tracking finances. They move between roles fluidly - now cooking, now cleaning, now serving, now bookkeeping - wherever they're needed most.

A successful innkeeper builds reputation through consistency. Rooms always clean. Food always decent. Service always friendly. Prices always fair. Word spreads: "The Rusty Tankard always has a warm bed and a filling meal." This reputation attracts staff, customers, and opportunities. The most successful inns become institutions, their innkeepers pillars of their communities.

---

## Mechanics

### Prerequisites

| Requirement        | Value                                     |
| ------------------ | ----------------------------------------- |
| XP Threshold       | 5,000 XP from hospitality tracked actions |
| Related Foundation | Host (optional, provides bonuses)         |
| Tag Depth          | 2 levels (`Service/Hospitality`)          |

### Requirements

| Requirement         | Value                                      |
| ------------------- | ------------------------------------------ |
| Unlock Trigger      | Manage lodging and food service operations |
| Primary Attribute   | CHA (Charisma), PAT (Patience)             |
| Starting Level      | 1                                          |
| Facilities Required | Inn, tavern, or boarding house             |

### Stats

#### Base Class Stats

| Level | HP Bonus | Stamina Bonus | Trait                |
| ----- | -------- | ------------- | -------------------- |
| 1     | +5       | +12           | Apprentice Innkeeper |
| 10    | +15      | +35           | Journeyman Innkeeper |
| 25    | +30      | +70           | Master Innkeeper     |
| 50    | +50      | +120          | Legendary Innkeeper  |

#### Service Bonuses (Generalist)

| Class Level | Food Quality | Lodging Quality | Service Speed | Staff Efficiency | Customer Satisfaction |
| ----------- | ------------ | --------------- | ------------- | ---------------- | --------------------- |
| 1-9         | +0%          | +0%             | +0%           | +0%              | +5%                   |
| 10-24       | +5%          | +5%             | +10%          | +15%             | +15%                  |
| 25-49       | +10%         | +10%            | +20%          | +30%             | +30%                  |
| 50+         | +15%         | +15%            | +30%          | +50%             | +50%                  |

**Note**: Specialists (Server, Housekeeper, Cook) gain +25-50% bonuses in their specific domain, but Innkeeper gains modest bonuses across ALL domains.

#### Starting Skills (Auto-Awarded on Class Acceptance)

Skills automatically awarded when accepting this class:

| Skill               | Tier     | Description                                               |
| ------------------- | -------- | --------------------------------------------------------- |
| Basic Cooking       | Common   | Prepare simple meals (50% speed of Cook specialist)       |
| Basic Housekeeping  | Common   | Prepare basic rooms (50% speed of Housekeeper specialist) |
| Customer Service    | Lesser   | Handle customer interactions professionally               |
| Staff Management    | Lesser   | Hire, assign, and coordinate employees                    |
| Business Operations | Mechanic | Track income, expenses, manage inventory                  |

**Key Principle**: Innkeeper skills are **basic and general** - adequate for solo operation, but specialists are more efficient.

#### Synergy Skills

Skills with strong synergies for Innkeeper:

**Management Skills**:

- Staff Coordination (tiered) - Improved staff efficiency and task delegation
- Resource Management (tiered) - Better inventory control and supply forecasting
- Conflict Resolution (common) - Handle disputes and complaints effectively
- Multi-tasking (common) - Handle multiple responsibilities simultaneously

**Business Skills**:

- Reputation Building (mechanic) - Attract higher-paying guests and repeat customers
- Pricing Strategy (common) - Optimize room rates and menu pricing
- Marketing (common) - Promote inn services to travelers and locals

**Note**: Innkeeper gains moderate synergies across many skills rather than strong synergies in one domain.

#### Synergy Bonuses

Innkeeper provides context-specific bonuses to hospitality-related skills:

**Generalist Skills** (Moderate synergy across domains):

- **Customer Service**: Core management skill
  - Faster learning: 1.5x XP from customer interactions
  - Better satisfaction: +15% customer satisfaction at Innkeeper 15
  - Reduced conflicts: -25% complaint escalation rate
- **Staff Management**: Direct management skill
  - Faster learning: 2x XP from hiring, training, delegating
  - Better efficiency: +25% staff productivity at Innkeeper 15
  - Lower turnover: -30% staff quit chance
- **Resource Management**: Essential for operations
  - Faster learning: 1.5x XP from inventory management
  - Better forecasting: +20% supply prediction accuracy at Innkeeper 15
  - Lower waste: -15% spoilage and losses

**Synergy Strength Scales with Class Level**:

| Innkeeper Level | XP Multiplier | Effectiveness Bonus | Example: Staff Management |
| --------------- | ------------- | ------------------- | ------------------------- |
| Level 5         | 1.25x (+25%)  | +10%                | Moderate improvements     |
| Level 10        | 1.5x (+50%)   | +15%                | Strong improvements       |
| Level 15        | 1.75x (+75%)  | +20%                | Excellent synergy         |
| Level 30+       | 2.0x (+100%)  | +30%                | Masterful synergy         |

### Tracked Actions for XP

Innkeeper gains XP from actions tagged with `Service/Hospitality` (and its children):

| Action Category     | Specific Actions                                | Example Tag                   | XP Value              |
| ------------------- | ----------------------------------------------- | ----------------------------- | --------------------- |
| Management          | Hire staff, assign tasks, delegate work         | `Service/Hospitality`         | 20-50 per task        |
| Customer Relations  | Greet guests, resolve complaints, build rapport | `Service/Hospitality`         | 10-30 per interaction |
| Business Operations | Track finances, order supplies, set prices      | `Service/Hospitality`         | 15-40 per task        |
| Backup Food Service | Serve meals when short-staffed                  | `Service/Hospitality/Food`    | 5-15 per serve        |
| Backup Housekeeping | Prepare rooms when short-staffed                | `Service/Hospitality/Lodging` | 5-15 per room         |
| Facility Oversight  | Inspect rooms, plan upgrades, maintain building | `Service/Hospitality`         | 10-25 per task        |
| Event Hosting       | Organize gatherings, weddings, celebrations     | `Service/Hospitality`         | 50-150 per event      |
| Staff Training      | Train employees, demonstrate techniques         | `Service/Hospitality`         | 20-60 per session     |

**Note**: Innkeeper tracks actions across both `Service/Hospitality/Food` and `Service/Hospitality/Lodging` but gains less XP than specialists (Server/Housekeeper) when performing specialized tasks.

#### Class Consolidation

See [Class Consolidation System](../../../systems/character/class-consolidation.md) for full mechanics.

| Consolidation Path                          | Requirements                                           | Result Class   | Tier |
| ------------------------------------------- | ------------------------------------------------------ | -------------- | ---- |
| [Entrepreneur](../consolidation/index.md)   | Innkeeper + [Merchant](../../trader/merchant/index.md) | Entrepreneur   | 1    |
| [Provisioner](../consolidation/index.md)    | Innkeeper + [Cook](../../crafter/cook/index.md)        | Provisioner    | 1    |
| [Estate Manager](../consolidation/index.md) | Innkeeper + [Steward](../../trader/steward/index.md)   | Estate Manager | 2    |

### Interactions

| System                                                        | Interaction                                                         |
| ------------------------------------------------------------- | ------------------------------------------------------------------- |
| [Economy](../../../systems/economy/index.md)                  | Primary service business; generates income from lodging and meals   |
| [Reputation](../../../systems/social/factions-reputation.md)  | Quality service builds reputation; affects customer tier and prices |
| [Settlement](../../../systems/world/settlements.md)           | Inns provide settlement morale and traveler accommodation           |
| [Crafting](../../../systems/crafting/crafting-progression.md) | Coordinates Cook for meal production and Server for meal delivery   |
| [Hiring](../../../systems/economy/employment.md)              | Can hire Server, Housekeeper, Cook, and other staff                 |

---

## Progression

### Specializations (Tier 3)

- [Lodge Master](./lodge-master/) - Luxury lodging and estate management specialist
- [Tavern Keeper](./tavern-keeper/) - Entertainment and social hub management specialist

---

## Related Content

- **Requires:** Inn building, basic furnishings, food supplies, linens
- **Materials Used:** Food ingredients, cleaning supplies, linens, fuel for heating
- **Services Provided:** Lodging (room rentals), Meals (food service), Event hosting
- **Staff Managed:** [Server](../server/index.md), [Housekeeper](../housekeeper/index.md), [Cook](../../crafter/cook/index.md)
- **Synergy Classes:** [Merchant](../../trader/merchant/index.md), [Cook](../../crafter/cook/index.md)
- **See Also:** [Service Economy](../../../systems/economy/services.md), [Hospitality System](../../../systems/hospitality/index.md)

---

## MVP Integration (001-minimal-possession-demo)

**Character**: Mara (Innkeeper, Level 5)

**Starting Configuration**:

- Class: Innkeeper
- Level: 5
- Building: Rusty Tankard Inn (Level 1)
- Staff: 0 (can hire Server, Housekeeper, Cook)

**Core Actions** (for possession mechanics):

1. **Serve Customer**
   - Action: Serve meal to customer
   - Tag: `Service/Hospitality/Food`
   - Duration: 5 seconds
   - Resources: -1 Food (from inventory), +10 gold
   - XP: 10 per service
   - Notes: Basic food service (slower than Server specialist)

2. **Prepare Room**
   - Action: Clean and prepare guest room
   - Tag: `Service/Hospitality/Lodging`
   - Duration: 15 seconds
   - Resources: +12 gold when guest checks in
   - XP: 12 per room
   - Notes: Basic housekeeping (slower than Housekeeper specialist)

3. **Manage Staff**
   - Action: Assign tasks to hired staff
   - Tag: `Service/Hospitality`
   - Duration: 10 seconds
   - Resources: -5 gold (wage payment)
   - Effect: Staff efficiency +10% for 60 seconds
   - XP: 15 per management task

4. **Check Income**
   - Action: Review inn finances
   - Tag: `Service/Hospitality`
   - Duration: Instant
   - Effect: Display total gold earned, daily expenses, profit
   - XP: 0 (informational)

**Hiring Actions**:

1. **Hire Server**
   - Tag: `Service/Hospitality`
   - Cost: 50 gold (contract) + 5 gold/day (wage)
   - Effect: Server handles food service (+50% speed vs Innkeeper)

2. **Hire Housekeeper**
   - Tag: `Service/Hospitality`
   - Cost: 50 gold (contract) + 5 gold/day (wage)
   - Effect: Housekeeper handles room prep (+100% speed vs Innkeeper)

3. **Hire Cook**
   - Tag: `Service/Hospitality/Food` or `Crafting/Cooking`
   - Cost: 60 gold (contract) + 7 gold/day (wage)
   - Effect: Cook produces Food items for inventory

**Design Notes**:

- Innkeeper can do everything solo but inefficiently
- Hiring specialists improves throughput and profit
- Demonstrates multi-class system (NPCs have classes)
- Demonstrates tag-based action system (actions have tags, not classes)
