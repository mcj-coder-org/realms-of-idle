---
title: Server
gdd_ref: systems/core-progression-system-gdd.md#general
---

# Server

## Lore

### Origin

The Server masters the art of food and beverage service - reading customers, anticipating needs, managing multiple tables with grace under pressure. Where innkeepers can serve basic meals adequately, servers excel through specialized techniques: perfect timing, efficient movement patterns, and intuitive understanding of dining preferences.

Food service is performance art. The server orchestrates the dining experience from greeting to departure. They read the room: the couple celebrating an anniversary needs attentive service without intrusion, the merchant wants quick service to return to business, the adventurer group desires entertainment and camaraderie. Each table requires different approaches, and skilled servers adapt seamlessly.

The Servers' Guild teaches efficiency through endless practice. Balance three plates without spilling. Remember six orders without writing. Navigate crowded dining rooms at speed. Defuse angry customers with practiced charm. Calculate tips instantly. These skills separate adequate food delivery from exceptional dining experiences that keep customers returning.

### In the World

Every tavern, inn, and restaurant needs servers. During meal rushes, servers transform into whirlwinds of controlled motion - greeting new arrivals, delivering food, clearing dishes, taking payments, all while maintaining pleasant demeanor despite sore feet and demanding customers.

A skilled server can handle twice the customers in half the time, turning table service from a chore into profitable efficiency. They remember regulars' favorite drinks without asking. Know which dishes pair well and can upsell premium items without seeming pushy. Can spot when customers need immediate attention versus when to give space. These insights come from experience - thousands of meals served, hundreds of customers satisfied.

City servers often specialize further. Fine dining servers master wine pairings and formal service protocols. Tavern servers excel at rapid beer delivery and crowd management. Street food servers perfect quick transactions and suggestive selling. But all share core competencies: speed, accuracy, and customer satisfaction.

---

## Mechanics

### Prerequisites

| Requirement        | Value                                      |
| ------------------ | ------------------------------------------ |
| XP Threshold       | 5,000 XP from food service tracked actions |
| Related Foundation | Host (optional, provides bonuses)          |
| Tag Depth          | 3 levels (`Service/Hospitality/Food`)      |

### Requirements

| Requirement         | Value                                    |
| ------------------- | ---------------------------------------- |
| Unlock Trigger      | Serve meals in tavern/restaurant setting |
| Primary Attribute   | DEX (Dexterity), CHA (Charisma)          |
| Starting Level      | 1                                        |
| Facilities Required | Dining area with tables                  |

### Stats

#### Base Class Stats

| Level | HP Bonus | Stamina Bonus | Trait             |
| ----- | -------- | ------------- | ----------------- |
| 1     | +4       | +15           | Apprentice Server |
| 10    | +12      | +45           | Journeyman Server |
| 25    | +25      | +90           | Master Server     |
| 50    | +42      | +150          | Legendary Server  |

**Note**: Servers have high stamina due to constant movement during shifts.

#### Service Bonuses (Food Service Specialist)

| Class Level | Service Speed | Multi-table Capacity | Upsell Success | Tip Income | Customer Satisfaction |
| ----------- | ------------- | -------------------- | -------------- | ---------- | --------------------- |
| 1-9         | +25%          | +1 table             | +10%           | +15%       | +10%                  |
| 10-24       | +50%          | +2 tables            | +20%           | +30%       | +25%                  |
| 25-49       | +75%          | +3 tables            | +35%           | +50%       | +40%                  |
| 50+         | +100%         | +4 tables            | +50%           | +75%       | +60%                  |

**Key Principle**: Servers are **specialized** in food service - significantly faster and more profitable than generalist Innkeepers.

#### Starting Skills (Auto-Awarded on Class Acceptance)

Skills automatically awarded when accepting this class:

| Skill             | Tier    | Description                                          |
| ----------------- | ------- | ---------------------------------------------------- |
| Efficient Service | Greater | Serve multiple customers simultaneously (+50% speed) |
| Menu Knowledge    | Lesser  | Recommend dishes, answer food questions accurately   |
| Customer Reading  | Lesser  | Anticipate needs before customer asks                |
| Tray Balance      | Passive | Carry more items without spills (+50% capacity)      |

#### Synergy Skills

Skills with strong synergies for Server:

**Service Skills**:

- Speed Service (tiered) - Faster service actions and table turnover
- Memory Mastery (mechanic) - Remember customer preferences and complex orders
- Charm (common) - Increase tips and customer satisfaction scores
- Stress Management (passive) - Maintain quality during rush periods

**Communication Skills**:

- Persuasion (common) - Successful upselling of premium items
- Conflict Resolution (lesser) - Handle complaints without escalation
- Active Listening (passive) - Accurately capture special requests

#### Synergy Bonuses

Server provides context-specific bonuses to food service skills:

**Food Service Skills** (Strong synergy):

- **Efficient Service**: Core specialization skill
  - Faster learning: 2.5x XP from table service actions
  - Better speed: +40% service speed at Server 15
  - More tables: +2 simultaneous table capacity
- **Menu Knowledge**: Direct application
  - Faster learning: 2x XP from menu recommendations
  - Better accuracy: +30% recommendation success at Server 15
  - Higher upsell: +25% premium item sales
- **Customer Reading**: Essential skill
  - Faster learning: 2x XP from customer interactions
  - Better predictions: +35% need anticipation accuracy at Server 15
  - Fewer complaints: -40% negative feedback

**Synergy Strength Scales with Class Level**:

| Server Level | XP Multiplier | Effectiveness Bonus | Multi-table | Example: Efficient Service |
| ------------ | ------------- | ------------------- | ----------- | -------------------------- |
| Level 5      | 1.75x (+75%)  | +25%                | +1 table    | Strong baseline            |
| Level 10     | 2.0x (+100%)  | +35%                | +2 tables   | Very strong                |
| Level 15     | 2.5x (+150%)  | +45%                | +3 tables   | Exceptional                |
| Level 30+    | 3.0x (+200%)  | +60%                | +4 tables   | Elite mastery              |

### Tracked Actions for XP

Server gains XP from actions tagged with `Service/Hospitality/Food` (and its children):

| Action Category        | Specific Actions                                 | Example Tag                | XP Value              |
| ---------------------- | ------------------------------------------------ | -------------------------- | --------------------- |
| Table Service          | Take orders, deliver food, clear tables          | `Service/Hospitality/Food` | 8-20 per table        |
| Customer Satisfaction  | Handle special requests, resolve issues          | `Service/Hospitality/Food` | 15-35 per interaction |
| Upselling              | Recommend premium items, suggest pairings        | `Service/Hospitality/Food` | 10-25 per success     |
| Multi-table Management | Handle 3+ tables simultaneously during rush      | `Service/Hospitality/Food` | 20-40 per shift       |
| Bar Service            | Serve beverages, mix drinks, manage bar          | `Service/Hospitality/Food` | 5-15 per drink        |
| Customer Relations     | Greet regulars, remember preferences, build tips | `Service/Hospitality/Food` | 10-30 per interaction |
| Event Service          | Cater parties, weddings, banquets                | `Service/Hospitality/Food` | 50-120 per event      |
| Training               | Train new servers, demonstrate techniques        | `Service/Hospitality/Food` | 20-50 per session     |

**Note**: Server only tracks food service actions - does NOT gain XP from room preparation or general management (unlike Innkeeper).

#### Class Consolidation

See [Class Consolidation System](../../../systems/character/class-consolidation.md) for full mechanics.

| Consolidation Path                             | Requirements                                     | Result Class      | Tier |
| ---------------------------------------------- | ------------------------------------------------ | ----------------- | ---- |
| [Entertainer](../consolidation/index.md)       | Server + [Bard](../bard/index.md)                | Entertainer       | 1    |
| [Sommelier](../consolidation/index.md)         | Server + [Brewer](../../crafter/brewer/index.md) | Sommelier         | 1    |
| [Event Coordinator](../consolidation/index.md) | Server + [Innkeeper](../innkeeper/index.md)      | Event Coordinator | 2    |

### Interactions

| System                                                        | Interaction                                                          |
| ------------------------------------------------------------- | -------------------------------------------------------------------- |
| [Economy](../../../systems/economy/index.md)                  | Generates tip income; increases customer throughput and revenue      |
| [Crafting](../../../systems/crafting/crafting-progression.md) | Coordinates with Cook for meal preparation and delivery              |
| [Reputation](../../../systems/social/factions-reputation.md)  | Excellent service builds inn reputation; attracts higher-tier guests |
| [Social](../../../systems/social/index.md)                    | Frequent customer interactions build relationships                   |

---

## Progression

### Specializations (Tier 3)

- [Master Server](./master-server/) - Elite fine dining and high-society service specialist

---

## Related Content

- **Requires:** Dining area, serving trays, menu knowledge
- **Works With:** [Cook](../../crafter/cook/index.md) (food preparation), [Innkeeper](../innkeeper/index.md) (manager)
- **Services Provided:** Table service, beverage service, customer satisfaction
- **Synergy Classes:** [Cook](../../crafter/cook/index.md), [Innkeeper](../innkeeper/index.md), [Bard](../bard/index.md)
- **See Also:** [Service Economy](../../../systems/economy/services.md), [Hospitality System](../../../systems/hospitality/index.md)

---

## MVP Integration (001-minimal-possession-demo)

**Role**: Hireable NPC specialist

**Hiring Details**:

- **Contract Cost**: 50 gold (one-time)
- **Daily Wage**: 5 gold per day
- **Efficiency**: +50% food service speed vs Innkeeper
- **Capacity**: Can handle 2-3 tables simultaneously (vs Innkeeper's 1 table)

**Core Actions** (when hired):

1. **Serve Customer** (Specialist)
   - Tag: `Service/Hospitality/Food`
   - Duration: 2.5 seconds (vs Innkeeper's 5 seconds)
   - Resources: -1 Food, +10 gold
   - XP: 15 per service (Server gains more XP than Innkeeper)
   - Effect: Faster table turnover = more customers served

2. **Take Multiple Orders**
   - Tag: `Service/Hospitality/Food`
   - Duration: 5 seconds for 3 customers
   - Resources: Queues food preparation for Cook
   - XP: 20 per multi-order
   - Effect: Batch processing improves efficiency

3. **Upsell Premium Item**
   - Tag: `Service/Hospitality/Food`
   - Duration: 3 seconds (during order taking)
   - Success Rate: 25% base (+10% per Server level)
   - Effect: Customer orders premium meal (+5 gold revenue)
   - XP: 12 per successful upsell

**Autonomous Behavior** (when not possessed):

- Prioritizes serving waiting customers
- Automatically suggests premium items (upselling)
- Clears tables between customers
- Reports low food inventory to Innkeeper

**Design Notes**:

- Server is hireable specialist (not player-controlled at MVP start)
- Demonstrates specialist efficiency (2x speed vs generalist)
- Shows tag-based action filtering (only food service actions)
- Autonomous AI uses same action system as possessed control
