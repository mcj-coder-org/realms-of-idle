---
type: system
scope: foundational
status: authoritative
version: 1.0.0
created: 2026-02-09
updated: 2026-02-09
subjects: [architecture, world-structure, possession, multiplayer, simulation]
dependencies: []
---

# Core Architecture - Unified World Simulation

## Executive Summary

**Realms of Idle** is built on a unified simulation model where a single persistent world runs continuously, governed by consistent rules that apply to all characters regardless of player control. Players are not characters in the world—they are **observers with god-like influence** who can possess NPCs to experience different roles and gameplay patterns.

**Key Principles:**

1. **One World, One Rule Set** - All characters follow the same systems
2. **Possession-Based Gameplay** - Players control NPCs temporarily through possession
3. **Scenarios are Emergent** - Gameplay patterns emerge from role focus, not separate modes
4. **Persistent Simulation** - World continues running offline
5. **Multiplayer via Proxies** - Players interact through favorited NPCs

---

## Section 1: World Architecture

### The Simulation Hierarchy

```
World (Persistent, Always Running)
├── Regions (Climate zones: Frozen North, Desert Wastes, Temperate Plains)
│   └── Zones (Distinct areas: Darkwood Forest, Iron Mountains, Millbrook Valley)
│       └── Settlements (Player-founded or NPC towns: Millbrook, Ironhaven)
│           └── Buildings (Physical structures: Rusty Tankard Inn, Fighter's Guild)
│               └── Characters (NPCs and Player Avatars)
│                   └── Actions (Contextual: Cook, Serve, Fight, Craft, Trade)
```

### Core Principles

**1. Unified Rule Set**
All characters—whether controlled by the player or running autonomously—use the same systems: attributes, skills, classes, inventory, needs, and progression. There is no separate "NPC logic" vs "player logic."

**2. Location-Contextual Gameplay**
Available actions depend on your current location and the character's role. An innkeeper at the Rusty Tankard has access to inn-related actions (serve drinks, manage staff). The same character walking into a smithy has no special smithing abilities unless they have the [Blacksmith] class.

**3. Possession-Based Control**
Players can "possess" any favorited NPC to take direct control. While possessed, you make decisions for that character. When you release control, the NPC resumes autonomous behavior following their goals, personality, and occupation.

**4. Persistent Simulation**
The world runs continuously, even when offline. NPCs work their jobs, customers visit establishments, resources accumulate, and time-based events trigger. When you return, you see the results of the simulation.

---

## Section 2: Buildings & Locations

Buildings are physical structures within settlements where characters perform location-specific activities. Each building type enables a distinct set of actions and serves a functional role in the settlement economy.

### Building Types & Functions

**Residential Buildings**

- **Purpose**: Housing for NPCs, determines population capacity
- **Examples**: Tents, Cottages, Houses, Manors
- **Actions Available**: Rest, Store personal items, Social interactions

**Workshop (Generic Crafting Building)**

- **Purpose**: Customizable crafting space for any production class
- **Ownership**: Single owner or partnership
- **Equipment**: Owner installs crafting stations (Forge, Anvil, Loom, Workbench, Alchemy Table, etc.)
- **Actions Available**:
  - **Owner/Partner**: Install stations, Craft, Manage apprentices, Set permissions
  - **Apprentice**: Craft (with restrictions), Learn from master, Use designated stations
  - **Visitor**: Request services, Place orders, Negotiate apprenticeship
- **Access Control**: Equipment usage requires owner permission or formal relationship (apprentice, partner, employee)

**Inn/Tavern**

- **Purpose**: Food, drink, lodging, social hub, information gathering
- **Examples**: The Rusty Tankard, The Prancing Pony
- **Actions Available**: Serve customers, Cook, Tend bar, Rent rooms, Manage staff

**Commercial Buildings**

- **Purpose**: Trade, services, and gold generation
- **Examples**: Market Stall, Trading Post, Warehouse, General Store
- **Actions Available**: Buy, Sell, Trade, Manage inventory

**Guild Buildings**

- **Purpose**: Class-specific coordination and training
- **Examples**: Fighter's Guild, Mage Tower, Thieves' Guild, Adventurer's Guild
- **Actions Available**: Train skills, Recruit members, Dispatch quests, Coordinate parties

**Service Buildings**

- **Purpose**: Community needs and buffs
- **Examples**: Temple (healing/buffs), Academy (learning)
- **Actions Available**: Receive services, Gather information, Learn skills

### Building State

Each building has:

- **Ownership**: Who controls it (single owner, partnership, guild, settlement)
- **Upgrade Level** (1-5): Determines capacity, output quality, and available features
- **Equipment/Stations**: Installed furniture and tools (especially important for workshops)
- **Staff Roster**: NPCs assigned to work there (owner, employees, apprentices)
- **Inventory**: Resources, goods, equipment stored on-site
- **Customers/Visitors**: Characters currently present and interacting
- **Production Queues**: Active crafting, cooking, or service timers
- **Access Permissions**: Who can use what facilities

### Context-Dependent Actions

When you possess a character and enter a building, available actions depend on:

1. **Your relationship to the building**: Owner, partner, employee, apprentice, customer, or visitor?
2. **Your skills**: Do you have relevant class levels or trained skills?
3. **Available facilities**: What equipment and workstations are installed and accessible to you?
4. **Permissions**: Has the owner granted you access to specific equipment?

Example: A [Blacksmith] Lvl 20 walking into another blacksmith's workshop is just a visitor unless they negotiate an apprenticeship, partnership, or equipment rental agreement.

---

## Section 3: Characters & Possession

All characters in the world—whether player-controlled avatars or autonomous NPCs—share the same underlying systems. The only difference is **who makes the decisions**.

### The Unified Character Model

Every character has:

- **Identity**: Name, appearance, titles, reputation
- **Progression**: Classes (active/dormant), skills, tag affinities, experience
- **State**: Inventory, equipment, resources, current location, health/stamina/mana
- **Activity**: Current action, task queue, action history
- **Relationships**: Connections to other characters, faction standings
- **Goals** (NPCs): Life purpose, long/medium/short-term objectives, daily tasks
- **AI State** (NPCs): Satisfaction, stress, personality traits, decision-making model

### Possession Mechanics

**Possessing a Character:**

1. **Favorite/Bookmark** characters you want quick access to
2. **Click their portrait** or select from favorites menu
3. **Take control**: You now make all decisions for this character
4. **See their context**: Available actions based on location, skills, relationships
5. **Issue commands**: Move, perform actions, manage inventory, interact with others

**While Possessed:**

- You see the world from their perspective
- Actions are limited by their skills, classes, and permissions
- Their needs (hunger, fatigue) affect performance
- Their relationships affect social interactions
- Time continues to pass (idle-friendly: queue actions and release)

**Releasing Possession:**

- Click "Release Control" or possess a different character
- The NPC resumes autonomous behavior following their goals and personality
- Queued actions continue to execute
- The NPC makes decisions based on their AI state and world conditions
- **Priority changes you made persist** - if you adjusted their goals or task priorities, they keep those changes

### Autonomous NPC Behavior

When **not possessed**, NPCs follow their programming:

**Goal-Driven Actions**

- NPCs have hierarchical goals (daily → short-term → medium-term → long-term → life purpose)
- They evaluate their current situation and select actions that advance their goals
- Personality traits influence decision-making priorities

**Routine Behavior**

- Work their occupation (blacksmith forges, innkeeper serves customers)
- Maintain basic needs (eat, sleep, rest)
- Social interactions (talk to friends, avoid enemies)
- Economic actions (buy supplies, sell goods)

**State Machine**

- **Working**: Assigned to a task, producing output
- **Idle**: No assignment, available for new tasks
- **Resting**: Recovering health/stamina
- **Traveling**: Moving between locations

### The Player's Role

You are **not a character in the world**—you are a presence that can:

- **Observe**: Watch the simulation unfold
- **Possess**: Take direct control of favorited NPCs
- **Guide**: Set long-term goals or task priorities for NPCs when not possessed
- **Build**: Construct buildings, manage settlements strategically

Think of it as a **director/actor duality**: You direct the world from above, but can jump into any actor's role to experience their story.

---

## Section 4: Role-Playing Scenarios (Emergent Gameplay)

"Scenarios" are not separate game modes—they are **gameplay patterns that emerge** when you possess an NPC with a specific role and focus deeply on that playstyle. The world simulation creates different challenges and opportunities based on the character's occupation and location.

### What Makes a Scenario

A scenario emerges from:

1. **Character's Role**: Their primary class/occupation ([Innkeeper], [Guild Master], [Blacksmith], [Merchant])
2. **Building Ownership**: What facilities they control
3. **Location Context**: Where they spend most of their time
4. **Player Focus**: How deeply you engage with that character's gameplay loop

### Example: The "Inn Scenario"

**Setup:**

- Possess "Mara", an [Innkeeper] Lvl 12, owner of The Rusty Tankard
- Building has: Kitchen (Tier 2), Bar (Tier 1), 6 Guest Rooms, 2 Staff Beds
- Current staff: 1 cook (NPC), 1 server (NPC)

**Gameplay Loop:**

```
Morning: Check overnight guest satisfaction, collect room payments
→ Review kitchen supplies, queue meal prep for the day
→ Adjust staff task assignments based on expected customer traffic

Afternoon: Customers arrive, orders flow in
→ Monitor service quality, intervene if staff is overwhelmed
→ Chat with customers, gather rumors/quest hooks
→ Make strategic decisions: hire more staff? upgrade the bar?

Evening: Peak hours, inn is full
→ Balance cooking speed vs quality, manage queue
→ Handle special requests, negotiate with traveling merchants
→ Build reputation through excellent service

Night: Prep for tomorrow, review finances
→ Spend gold on upgrades or save for expansion
→ Adjust NPC priorities: "Cook should prioritize quality over speed"
→ Release possession → Mara continues running the inn autonomously
```

**The Persistence of Your Choices:**

- If you adjusted Mara's priorities ("Focus on building reputation over gold"), she keeps that priority after release
- If you hired a new staff member, they remain employed
- If you queued kitchen upgrades, they continue building
- Mara makes operational decisions following the priorities you set

**Why It Feels Like a Distinct Game:**

- **Social Management**: Staff satisfaction, customer relationships, reputation building
- **Economic Puzzle**: Pricing, supply chains, upgrade timing
- **Cozy Optimization**: Perfect the layout, perfect the menu, perfect the vibe
- **Story Moments**: Regulars become characters, events happen in your tavern

### Example: The "Adventurer Guild Scenario"

**Setup:**

- Possess "Kara", a [Guild Master] Lvl 18, leader of the Millbrook Fighter's Guild
- Building has: Training Grounds, Quest Board, Armory, Barracks
- Current members: 12 fighters (various levels), 3 apprentices

**Gameplay Loop:**

```
Morning: Review available quests from settlements and NPCs
→ Assign parties based on quest difficulty and member skills
→ Send expeditions with timers (2 hours → 3 days depending on quest)

Afternoon: Recruit new members (interview NPCs, assess skills)
→ Train apprentices, level up guild member skills
→ Manage equipment (repair, upgrade, distribute loot)

Evening: Expeditions complete, collect results
→ Review performance, distribute rewards
→ Plan next wave of contracts
→ Adjust member priorities: "Focus on defensive training"
→ Release possession → Kara continues guild operations

Days Later (Offline Progress):
→ Multiple expeditions completed while you were away
→ New members recruited by Kara following your criteria
→ Guild reputation increased from successful quests
```

**Why It Feels Distinct:**

- **Strategic Coordination**: Party composition, quest selection, risk management
- **Long-Term Planning**: Member development, equipment progression
- **Combat Focus**: Success depends on party strength and tactics
- **Business Management**: Quest contracts, member salaries, equipment costs

---

## Section 5: Unified World Interconnections

Despite distinct gameplay patterns, all scenarios exist in the **same persistent world** with **shared consequences**. Actions taken in one role ripple through the simulation and affect all other characters.

### Cross-Role Dependencies

**The Inn Needs the Guild:**

- Adventurers are your customers (reliable income, high spending)
- Guild quests bring travelers through town (foot traffic)
- Successful guild = prosperous settlement = more patrons

**The Guild Needs the Inn:**

- Adventurers need food, rest, healing before/after quests
- Quest board information spreads through taverns
- Inn reputation affects guild recruitment (good town attracts fighters)

**The Blacksmith Needs Both:**

- Guild members are primary customers for weapons/armor
- Inn provides meals while crafting (NPCs have needs too)
- Successful adventurers = more gold = more expensive commissions

**Everyone Needs the Settlement:**

- Buildings require construction materials (lumber, stone, etc.)
- Population growth unlocks new building slots
- Settlement-wide bonuses (from policies, temples, etc.) affect all NPCs
- Faction reputation opens or closes opportunities

### Seamless Transitions

You can shift focus fluidly:

**Morning:** Possess Mara the Innkeeper

- Prep the inn for the day, set staff priorities
- Notice guild members gearing up for a dangerous quest
- Release Mara (she continues inn operations autonomously)

**Afternoon:** Possess Kara the Guild Master

- Review the dangerous quest details
- Realize the party needs better equipment
- Send a commission request to Tomas the Blacksmith
- Release Kara (she continues dispatching other quests)

**Evening:** Possess Tomas the Blacksmith

- Review commission: masterwork longsword needed urgently
- Check materials, start crafting timer (8 hours)
- Adjust priorities: "Rush this order, delay other work"
- Release Tomas (he continues crafting overnight)

**Next Morning:** Possess Mara again

- The guild party is preparing to leave
- They stop by the inn for a final meal (Mara earns gold)
- Tomas delivers the sword to Kara
- The quest expedition launches (timer starts: 12 hours)

**Next Evening:** Possess Kara

- Expedition completes successfully (partly due to the masterwork sword)
- Loot distributed, reputation gained
- Guild members celebrate at Mara's inn (more gold for her)
- Settlement gains fame (new NPCs migrate to town)

### Player Role: Observer + Limited God

**You are not a character in the world.** You are a **presence** that can:

**Observe:**

- Watch the simulation unfold in real-time
- Track multiple NPCs and locations simultaneously
- Receive notifications about important events

**Possess:**

- Take direct control of any favorited NPC
- Make decisions, perform actions, adjust priorities
- Experience the world through their eyes

**Influence:**

- Guide settlement development (place buildings, set policies)
- Adjust NPC priorities that persist after releasing control
- Shape the world indirectly through your interventions

**Your "Progression":**

- NOT traditional XP/levels for your account
- Instead, emergent success measured by:
  - **Settlement Health**: Population, prosperity, defenses
  - **Favorite NPCs' Status**: Their skills, wealth, reputation, happiness
  - **World Events**: Outcomes you influenced, crises averted, opportunities seized
  - **Economic Success**: Businesses you've guided, trade networks established
  - **Influence Network**: How many NPCs trust/respect your favorited characters

### The Favorites System

**Bookmarking NPCs:**

- Click "Add to Favorites" on any NPC
- Creates quick-access menu for instant possession
- Favorites persist across sessions

**Notifications:**

- Important events affecting favorited NPCs trigger alerts
  - "Mara (Innkeeper) - Supply shortage! Kitchen production halted"
  - "Kara (Guild Master) - Quest failed! 2 members injured"
  - "Tomas (Blacksmith) - Masterwork item completed!"
- Notifications appear whether online or offline (catch up on login)

**Your "Stable" of Characters:**

- Favorites = your proxy network in the world
- Each has independent progression (their skills, classes, relationships)
- Their success = your success
- Their failures = your challenges

### Multiplayer: Shared World, Distinct Favorites

**How It Works:**

- Multiple players inhabit the **same world simulation**
- Each player has their **own favorites list** (your NPCs vs my NPCs)
- NPCs can be favorited by multiple players (contested influence)

**Cooperative Play:**

```
Player 1 favorites: Mara (Innkeeper), Tomas (Blacksmith)
Player 2 favorites: Kara (Guild Master), Elena (Merchant)

Cooperation:
- Player 1 runs the inn and smithy
- Player 2 runs the guild and trading post
- Guild members eat at the inn (revenue for P1)
- Smithy supplies guild weapons (revenue for P1, stronger guild for P2)
- Merchant sells both inn goods and guild loot (revenue for P2)
- Settlement prospers → both players win
```

**Competitive Play:**

```
Player 1 favorites: Mara (Innkeeper at The Rusty Tankard)
Player 2 favorites: Boris (Innkeeper at The Golden Boar)

Competition:
- Both inns in the same settlement
- Compete for customers, reputation, staff
- Player 1 upgrades facilities → attracts more customers
- Player 2 lowers prices → undercuts competition
- Settlement benefits from competition (more options)
- Players race for "Best Inn in Town" reputation
```

**Contested NPCs:**

- If both players favorite the same NPC:
  - Both can possess them (not simultaneously)
  - Priority adjustments from last possessor persist
  - Creates interesting "tug of war" scenarios
  - NPC follows most recent instructions

**Asynchronous Gameplay:**

- World continues when you're offline
- Your favorited NPCs continue autonomously following last instructions
- Other players' NPCs interact with yours
- Wake up to: "While you were away, Player 2's merchant bought your entire wine stock"

### The Simulation Continues

**Key Principle:** The world doesn't pause when you're not looking.

- Release possession of Mara → she continues serving customers
- Don't log in for 3 days → favorited NPCs work their jobs, make decisions
- Other players' NPCs continue interacting with the world
- New NPCs spawn, old NPCs die/retire, the world evolves

**Offline Catchup:**

- On login, see notifications:
  - "Mara (Your Inn): 47 customers served, +382 gold, reputation +12"
  - "Kara (Your Guild): 3 quests completed successfully, 1 failed"
  - "Tomas (Your Smithy): 12 items crafted, Forge upgraded to Tier 3"
- Review what happened while you were away
- Adjust priorities based on what your NPCs did

**Emergent Consequences:**

- A rival NPC (controlled by Player 2 or autonomous) opens competing business
- Your favorited innkeeper befriends another player's favorited merchant (economic synergy)
- Settlement events affect everyone's NPCs equally
- Monster attack damages buildings regardless of who owns them

### Why This Matters

**You're not playing as a character—you're playing as an influence.**

- **Deep Focus** = Possess one NPC extensively, master their role
- **Broad Influence** = Rotate between multiple favorited NPCs, orchestrate networks
- **Collaborative** = Coordinate with other players through your respective NPCs
- **Competitive** = Outmaneuver other players in the same markets/domains

The "scenarios" are **lenses through which you experience the world**, not separate realities. The blacksmith you trade with as an innkeeper might be another player's favorited NPC. The guild members you equip could be autonomous or player-controlled.

**One world. Many players. Many NPCs. Your favorites are your story.**

---

## Summary

**Core Architecture Principles:**

1. ✅ Unified simulation with consistent rules for all characters
2. ✅ Buildings as contextual spaces with ownership and permissions
3. ✅ Possession-based control with persistent priority changes
4. ✅ Scenarios as emergent gameplay from role focus
5. ✅ Interconnected world with cross-role dependencies
6. ✅ Player as observer/god-like presence (NOT a character)
7. ✅ Favorites system for tracking and possessing NPCs
8. ✅ Multiplayer via proxy NPCs with cooperative and competitive modes

**This document is authoritative** - all other design documents should align with these core principles. When "scenarios" are mentioned elsewhere, they refer to emergent gameplay patterns, not separate game modes.
