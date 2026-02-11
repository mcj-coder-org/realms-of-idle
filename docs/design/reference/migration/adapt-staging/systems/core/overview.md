<!-- ADAPTATION REQUIRED -->
<!-- This file was migrated from source but needs manual review: -->
<!-- - Update terminology (dormant classes, XP split, etc.) -->
<!-- - Align with current GDD architecture -->
<!-- - Add missing sections as needed -->
<!-- - Update frontmatter with correct gdd_ref -->

---

title: Game Design Overview
type: system
summary: High-level vision, core systems map, and design pillars for the slice-of-life fantasy RPG

---

# Slice-of-Life RPG - Systems Overview

## Game Summary

A single-player slice-of-life RPG for mobile/web (C#/Unity) featuring organic progression, personality-driven NPCs, and meaningful consequences. Combat is automated with manual override. Permadeath exists but can be mitigated through party mechanics, rare skills, and the favourite NPC system.

### Tone & Play Style

**Cozy by default** with optional darker content in distant regions. Players can enjoy a full experience in safe, prosperous areas or venture into harsher regions for greater challenges and darker themes.

**Two play modes** that blend naturally:

- **Hero Mode**: Traditional RPG - deep investment in one character, active roleplay
- **Chronicle Mode**: Simulation/idle - manage multiple favourited NPCs, set priorities, observe outcomes across generations

---

## Core Systems Map

```
┌─────────────────────────────────────────────────────────────────┐
│                         TIME & WORLD                             │
│  Day/Night (1hr=24hr) │ Regional Climate │ Annual Fairs         │
└─────────────────────────┬───────────────────────────────────────┘
                          │
         ┌────────────────┼────────────────┐
         ▼                ▼                ▼
┌─────────────┐   ┌─────────────┐   ┌─────────────┐
│  STAMINA    │   │    REST     │   │   HEALTH    │
│ Actions     │   │ Level-ups   │   │ Shield      │
│ Crafting    │   │ Recovery    │   │ Armour      │
│ Zero = nap  │   │ Encounters  │   │ Health      │
└──────┬──────┘   └──────┬──────┘   └──────┬──────┘
       │                 │                 │
       └────────┬────────┴────────┬────────┘
                ▼                 ▼
┌─────────────────────────────────────────────────────────────────┐
│                          COMBAT                                  │
│  Hybrid (real-time + pause) │ Row positioning │ Auto/Manual     │
│  Morale │ Surrender │ Rout/Capture │ Adrenaline                 │
└─────────────────────────┬───────────────────────────────────────┘
                          │
    ┌─────────────────────┼─────────────────────┐
    ▼                     ▼                     ▼
┌───────────┐     ┌───────────────┐     ┌───────────────┐
│ CLASSES   │     │    MAGIC      │     │   CRAFTING    │
│ Skills    │     │ Schools       │     │ Specialties   │
│ Tiers     │     │ Enchanting    │     │ Recipes       │
│ Consolid. │     │ Mana pools    │     │ Discovery     │
│ Artificer │     │ Archmage      │     │ Masterwork    │
└─────┬─────┘     └───────────────┘     └───────┬───────┘
      │                                         │
      └──────────────────┬──────────────────────┘
                         ▼
┌─────────────────────────────────────────────────────────────────┐
│                      ECONOMY & TRADE                             │
│  Currency (G/S/C) │ Dynamic pricing │ Regional supply/demand    │
│  Trader → Merchant → Specialist │ Caravaner │ Trade routes      │
└─────────────────────────┬───────────────────────────────────────┘
                          │
         ┌────────────────┼────────────────┐
         ▼                ▼                ▼
┌─────────────────┐ ┌───────────────┐ ┌───────────────┐
│ MERCHANT GUILD  │ │ CRAFTING GUILD│ │ BLACK MARKET  │
│ Banking         │ │ Workshops     │ │ Smuggling     │
│ Trade routes    │ │ Recipes       │ │ Fencing       │
│ Insurance       │ │ Certification │ │ Thieves Guild │
└─────────────────┘ └───────────────┘ └───────┬───────┘
                                              │
┌─────────────────────────────────────────────┴───────────────────┐
│                    DUAL REPUTATION                               │
│  Public (legitimate factions) │ Underground (criminal network)  │
│  Can maintain both │ Discovery risk │ Blackmail mechanics       │
└─────────────────────────┬───────────────────────────────────────┘
                          │
         ┌────────────────┼────────────────┐
         ▼                ▼                ▼
┌─────────────────┐ ┌───────────────┐ ┌───────────────┐
│   PERSONALITY   │ │   FACTIONS    │ │  SETTLEMENTS  │
│ Seeded+evolving │ │ Witnessed     │ │ Leader policy │
│ Combat AI       │ │ reported      │ │ Corruption    │
│ Trade behavior  │ │ Decay         │ │ Taxes         │
└─────────────────┘ └───────────────┘ └───────────────┘
                          │
┌─────────────────────────┴───────────────────────────────────────┐
│                      PARTY SYSTEM                                │
│  Organic join/leave │ Loyalty │ Orders │ Training │ Shared XP   │
│  Prisoners follow as untrusted │ Ransom negotiation             │
└─────────────────────────┬───────────────────────────────────────┘
                          │
┌─────────────────────────┴───────────────────────────────────────┐
│                    DEATH & BODIES                                │
│  PC death → Switch character or new game (same/new world)       │
│  Bodies persist → Decay → Reanimation risk → Undead evolution   │
│  Necromancer/Artificer control │ Cannibalism → Monster class    │
└─────────────────────────────────────────────────────────────────┘
```

---

## Key Design Pillars

### 1. Organic Progression

- Classes unlocked through play, not menus
- Skills emerge from actions
- Consolidation reflects playstyle
- NPCs use same systems

### 2. Meaningful Choices

- Skill refusal for better tiers later
- Apprenticeship (faster) vs self-taught (better skills)
- Consolidation commits you to a path
- Specialist vs. generalist
- Public vs. underground reputation

### 3. Living World

- Information spreads through NPC contact
- Factions only know what's reported
- Bodies can reanimate
- NPCs have evolving personalities
- Dynamic economy based on supply/demand

### 4. Consequence Without Frustration

- Permadeath exists but mitigated (party switch, rare skills)
- Never locked out of factions
- Reputation decays (second chances)
- Surrender/capture instead of death

### 5. Automated Depth

- Combat runs itself intelligently
- Player intervenes when desired
- AI respects personality and role
- Manual override always available

### 6. Geography-Gated Tone

- Cozy starting regions (prosperous, safe)
- Darker content in distant regions (frontier, wild, harsh, corrupted)
- Player chooses to engage with darker themes through travel
- Villains/heroes emerge naturally from regional conditions
- See [regional-tones.md](regional-tones.md)

### 7. Multi-Character Investment

- Favourite up to 15 NPCs for tracking and quick access
- Set priorities that guide NPC behavior when not possessed
- Receive notifications for achievements and life events
- Build chronicles across generations
- See [favourite-npc-system.md](../../favourite-npc-system.md)

---

## Core Mechanic Definitions

### Fully Rested Bonus

Completing a **full sleep cycle** (uninterrupted rest until natural wake) grants the "Fully Rested" status effect.

| Aspect         | Value                                              |
| -------------- | -------------------------------------------------- |
| Trigger        | Complete uninterrupted sleep                       |
| Effect         | **+15% XP gain**                                   |
| Duration       | 4-6 game hours after waking                        |
| Applies to     | All XP sources (combat, crafting, gathering, etc.) |
| Loss condition | Time expiration only                               |

**Design Purpose:**

- Encourages natural play rhythm (sleep → adventure → sleep)
- Rewards players who don't spam-nap for recovery
- Creates meaningful choice about when to push vs rest

**Variations:**

| Rest Quality            | XP Bonus | Duration |
| ----------------------- | -------- | -------- |
| Rough sleeping (ground) | +10%     | 3 hours  |
| Basic bed               | +15%     | 4 hours  |
| Quality bed             | +15%     | 5 hours  |
| Luxury accommodation    | +15%     | 6 hours  |

See [Time and Rest](time-and-rest.md) for full rest mechanics.

### Adrenaline Burst

When a character collapses from zero stamina during combat, they receive an automatic **Adrenaline burst** upon waking:

| Aspect  | Value                               |
| ------- | ----------------------------------- |
| Trigger | Wake from stamina-nap in combat     |
| Effect  | 20-30% stamina instantly            |
| Purpose | Prevent helplessness after collapse |

See [Adrenaline Mechanic](adrenaline.md) for full details.

---

## Document Index

| Document                 | Contents                                     |
| ------------------------ | -------------------------------------------- |
| Core Systems             | Time, Stamina, Rest, Currency, Guilds        |
| Classes & Skills         | Unlocking, Tiers, Quality, Dark Path         |
| Consolidation            | Merging, Inheritance, Passives               |
| Party & Training         | Joining, Loyalty, Orders, Skill transfer     |
| Personality & Factions   | Traits, Alignment, Witnesses                 |
| Combat                   | Defense, Actions, Positioning, AI            |
| Morale & Prisoners       | Surrender, Rout, Ransom, Control             |
| Magic                    | Schools, Spells, Enchanting                  |
| Enchantment              | Dual quality, Tiers, Equipment/Container     |
| Economy & Trade          | Currency, Pricing, Trader/Merchant/Caravaner |
| Black Market & Smuggling | Dual reputation, Contraband, Thieves Guild   |
| Settlement Policies      | Leader powers, Corruption, Taxes             |
| Crafting                 | Specialties, Recipes, Discovery, Masterwork  |
| Fairs & Events           | Competitions, Annual calendar                |
| Weapons & Equipment      | Categories, Mastery, Degradation             |
| Death & Undead           | Bodies, Reanimation, Monsters                |
| NPC Daily Life           | Needs, Schedules, Decisions, Motivation      |
| Favourite NPC System     | Possession, Priorities, Chronicle Mode       |
| Regional Tone            | Geography-gated darkness, Region types       |
| Data Tables              | All numerical values                         |

---

## Production Chain

```
GATHERING          →    CRAFTING           →    ECONOMY
Forager, Miner,         Blacksmith,             Trader,
Hunter, Farmer          Alchemist, etc.         Merchant,
        ↓                      ↓                Caravaner
Raw Materials          Refined + Crafted              ↓
                             ↓                  Shops, Markets,
                       Certification           Trade Routes
                       (Crafting Guild)              ↓
                             ↓                  Consumers
                       Merchant Guild ←──────→ Black Market
```

---

## Remaining Topics to Define

| Area                    | Status      |
| ----------------------- | ----------- |
| Gathering & Resources   | Not started |
| Settlement & Building   | In progress |
| Quest System            | Not started |
| NPC Daily Life          | **Defined** |
| Favourite NPC System    | **Defined** |
| Regional Tone System    | **Defined** |
| Exploration & Discovery | Not started |
| Environment & Hazards   | Not started |
| Housing & Storage       | In progress |

---

## MVP Scope Suggestion

### Phase 1: Core Loop

- [ ] Day/night cycle
- [ ] Stamina drain and rest
- [ ] One class (Farmer) with 3 skills
- [ ] Basic actions and XP gain
- [ ] Level-up at rest

### Phase 2: Combat

- [ ] Health system
- [ ] Basic combat (one row)
- [ ] Auto-battle with pause
- [ ] Death and respawn

### Phase 3: Economy

- [ ] Currency system
- [ ] Basic trading/bartering
- [ ] One shop
- [ ] Trader class unlock

### Phase 4: Crafting

- [ ] One crafting specialty
- [ ] Basic recipes
- [ ] Material quality
- [ ] Recipe discovery

### Phase 5: Party & NPCs

- [ ] One recruitable NPC
- [ ] Party join/leave
- [ ] Basic loyalty
- [ ] Simple orders

### Phase 6: World

- [ ] One faction
- [ ] Witness/report system
- [ ] Basic reputation
- [ ] Settlement policies

### Phase 7: Expansion

- [ ] Additional classes
- [ ] Magic system
- [ ] Black market
- [ ] Fairs and events
- [ ] Full undead system
