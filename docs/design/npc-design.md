---
type: system
scope: detailed
status: review
version: 1.0.0
created: 2026-02-02
updated: 2026-02-02
subjects: [npc, artificial-intelligence, dialogue]
dependencies: []
---

# NPC Systems Design Document

## Living World Through Unified Character Systems

---

## 1. Design Philosophy

### Core Principle

**NPCs are not props—they are characters.** Every NPC uses the same class, skill, tag, and action systems as players. The difference is not in capability, but in goals and priorities.

### Design Goals

| Goal                       | Implementation                                               |
| -------------------------- | ------------------------------------------------------------ |
| **Authenticity**           | NPCs level up, learn skills, and evolve classes like players |
| **World Coherence**        | NPC actions produce real economic and social effects         |
| **Emergent Narrative**     | Hero/Villain goals create dynamic world events               |
| **Player Agency**          | Player actions can influence NPC goals and outcomes          |
| **Sustainable Simulation** | Most NPCs maintain equilibrium; few drive change             |

### The 95/5 Rule

- **95% of NPCs** are Maintainers—content with their role, working to keep the world functional
- **5% of NPCs** are Ambitious—Heroes and Villains whose goals drive world events

---

## 2. NPC Architecture

### 2.1 Unified Character Model

NPCs share the player character data structure with additions:

```yaml
NPC_Character:
  # === Shared with Players ===
  identity:
    name: 'Tomas Ironhand'
    appearance: { ... }
    titles: ['Master Smith of Millbrook']

  progression:
    classes:
      active: [{ name: '[Blacksmith]', level: 34 }]
      dormant: [{ name: '[Warrior]', level: 12 }]
    skills: [...]
    tag_affinities: { ... }

  state:
    inventory: [...]
    equipment: { ... }
    resources: { gold: 2340, reputation: { ... } }
    location: 'Millbrook Smithy'

  activity:
    current_action: 'Craft'
    activity_loop: { ... }
    action_history: [...] # Rolling 1000

  # === NPC-Specific ===
  npc_core:
    archetype: 'Maintainer' # Maintainer | Hero | Villain
    personality: { ... }
    relationships: { ... }

  goals:
    life_purpose: { ... } # 5+ year vision
    long_term: [...] # 1-5 year goals
    medium_term: [...] # 1-12 month goals
    short_term: [...] # 1-4 week goals
    daily: [...] # Today's objectives

  ai_state:
    satisfaction: 0.78 # 0-1, affects goal urgency
    stress: 0.23 # 0-1, affects decision making
    world_model: { ... } # NPC's knowledge/beliefs
```

### 2.2 NPC Archetypes

```
┌─────────────────────────────────────────────────────────────┐
│                     NPC POPULATION                          │
├─────────────────────────────────────────────────────────────┤
│                                                             │
│   MAINTAINERS (95%)                                         │
│   ┌─────────────────────────────────────────────────────┐  │
│   │  Laborers    Craftsmen    Merchants    Guards       │  │
│   │  Farmers     Innkeepers   Healers      Teachers     │  │
│   │                                                     │  │
│   │  Goal: Sustain self, family, community              │  │
│   │  Behavior: Predictable, stabilizing                 │  │
│   └─────────────────────────────────────────────────────┘  │
│                                                             │
│   AMBITIOUS (5%)                                            │
│   ┌───────────────────────┐ ┌───────────────────────────┐  │
│   │       HEROES          │ │        VILLAINS           │  │
│   │                       │ │                           │  │
│   │  Adventurers          │ │  Crime Lords              │  │
│   │  Crusaders            │ │  Tyrants                  │  │
│   │  Reformers            │ │  Cultists                 │  │
│   │  Explorers            │ │  Conquerors               │  │
│   │  Innovators           │ │  Corruptors               │  │
│   │                       │ │                           │  │
│   │  Goal: Change world   │ │  Goal: Change world       │  │
│   │        for better     │ │        for self/ideology  │  │
│   └───────────────────────┘ └───────────────────────────┘  │
│                                                             │
└─────────────────────────────────────────────────────────────┘
```

---

## 3. The Goal System

### 3.1 Goal Hierarchy

Goals cascade from abstract life purpose down to concrete daily actions:

```
LIFE PURPOSE (5+ years)
    ↓ informs
LONG-TERM GOALS (1-5 years)
    ↓ decompose into
MEDIUM-TERM GOALS (1-12 months)
    ↓ decompose into
SHORT-TERM GOALS (1-4 weeks)
    ↓ decompose into
DAILY OBJECTIVES
    ↓ translate to
ACTION SELECTION
```

### 3.2 Goal Structure

```yaml
Goal:
  id: 'goal_tomas_master_enchant'
  description: 'Learn to enchant my smithwork'

  type: 'skill_acquisition' # See goal types below
  priority: 0.7 # 0-1, affects resource allocation

  timeframe:
    category: 'medium_term' # life_purpose | long_term | medium_term | short_term | daily
    target_date: '2026-06-01'
    flexibility: 'moderate' # rigid | moderate | flexible

  requirements:
    tag_thresholds:
      magic.arcane.enchant: 100
    class_requirements: null
    resource_requirements:
      gold: 500 # For training/materials
    relationship_requirements:
      - npc: 'Enchanter Mira'
        disposition: '>= friendly'

  progress:
    current: 0.35
    milestones:
      - { threshold: 0.25, description: 'Find enchanting teacher', complete: true }
      - { threshold: 0.50, description: 'Complete basic training', complete: false }
      - { threshold: 0.75, description: 'Enchant first item', complete: false }
      - { threshold: 1.00, description: 'Achieve self-sufficiency', complete: false }

  success_effects:
    - unlock_recipes: ['Enchanted Iron Sword', 'Glowing Steel Plate']
    - add_title: 'Enchanter-Smith'
    - satisfaction: +0.15

  failure_effects:
    - satisfaction: -0.10
    - may_generate: 'goal_find_alternative'

  sub_goals: # Decomposition into shorter-term goals
    - 'goal_tomas_befriend_mira'
    - 'goal_tomas_save_gold_training'
    - 'goal_tomas_study_basics'
```

### 3.3 Goal Types

| Type                  | Description         | Example                      |
| --------------------- | ------------------- | ---------------------------- |
| `survival`            | Basic needs         | "Earn enough to eat"         |
| `economic`            | Wealth accumulation | "Save 1000 gold"             |
| `skill_acquisition`   | Learn new abilities | "Reach [Blacksmith] Lv.30"   |
| `class_evolution`     | Evolve class        | "Become [Master Smith]"      |
| `social_relationship` | Build connections   | "Befriend the Duke"          |
| `reputation`          | Gain standing       | "Become known in the region" |
| `acquisition`         | Obtain items        | "Acquire mythril ore"        |
| `construction`        | Build something     | "Expand my shop"             |
| `protection`          | Defend something    | "Keep my family safe"        |
| `destruction`         | Destroy something   | "Ruin my rival"              |
| `domination`          | Control others      | "Rule this town"             |
| `exploration`         | Discover new things | "Map the northern caves"     |
| `creation`            | Make something new  | "Forge a legendary blade"    |
| `ideology`            | Spread beliefs      | "Convert the region"         |
| `vengeance`           | Punish wrongdoers   | "Avenge my brother"          |

### 3.4 Goal Generation

NPCs generate goals based on their archetype, personality, circumstances, and world state:

```yaml
Goal_Generation:
  triggers:
    - scheduled: 'weekly_review'
    - event: 'goal_completed'
    - event: 'goal_failed'
    - event: 'major_life_change'
    - event: 'world_event_impact'
    - threshold: 'satisfaction < 0.3'
    - threshold: 'stress > 0.8'

  generation_weights:
    maintainer:
      survival: 0.30
      economic: 0.25
      skill_acquisition: 0.15
      social_relationship: 0.15
      protection: 0.10
      other: 0.05

    hero:
      protection: 0.20
      exploration: 0.20
      creation: 0.15
      ideology: 0.15
      reputation: 0.15
      skill_acquisition: 0.10
      other: 0.05

    villain:
      domination: 0.25
      acquisition: 0.20
      destruction: 0.15
      economic: 0.15
      vengeance: 0.10
      ideology: 0.10
      other: 0.05
```

---

## 4. Daily Loop System

### 4.1 Loop Generation Algorithm

Each day (or simulation tick), NPCs generate their activity loop from goals:

```
DAILY LOOP GENERATION
━━━━━━━━━━━━━━━━━━━━━

1. COLLECT ACTIVE GOALS
   → Gather all goals with remaining progress
   → Weight by priority and deadline urgency

2. IDENTIFY REQUIRED ACTIONS
   → For each goal, determine actions that advance it
   → Map actions to available time slots

3. SCHEDULE MANDATORY ACTIVITIES
   → Survival needs (eat, sleep, shelter)
   → Occupational duties (shop hours, guard shifts)
   → Social obligations (appointments, events)

4. FILL REMAINING TIME
   → Allocate to goal-advancing actions by priority
   → Include buffer time for interruptions

5. GENERATE ACTIVITY LOOP
   → Output executable loop structure
   → Include decision points and conditionals
```

### 4.2 Example: Maintainer NPC Daily Loop

```yaml
NPC: "Tomas Ironhand"
Archetype: Maintainer
Class: [Blacksmith] Lv.34
Date: "2026-02-01"

Active_Goals:
  - "Earn daily income" (survival, priority: 1.0)
  - "Learn enchanting" (skill_acquisition, priority: 0.7)
  - "Save for shop expansion" (economic, priority: 0.5)

Generated_Loop:
  - phase: "morning_routine"
    time: "06:00-07:00"
    actions:
      - Rest (wake)
      - Travel (home → smithy)
    goal_alignment: survival

  - phase: "work_morning"
    time: "07:00-12:00"
    actions:
      - Craft (customer_orders)
      - Trade (sell completed items)
    goal_alignment: [survival, economic]
    interrupts:
      - customer_arrival: → Talk, Trade

  - phase: "midday"
    time: "12:00-13:00"
    actions:
      - Travel (smithy → tavern)
      - Trade (buy meal)
      - Rest (eat)
      - Talk (social maintenance)
    goal_alignment: survival

  - phase: "work_afternoon"
    time: "13:00-17:00"
    actions:
      - Craft (inventory restocking)
      - Gather (restock materials if low)
    goal_alignment: [survival, economic]

  - phase: "study_time"
    time: "17:00-19:00"
    actions:
      - Travel (smithy → Mira's tower)
      - Study (enchanting basics)
      - Talk (build relationship with Mira)
    goal_alignment: skill_acquisition
    conditions:
      - if: "Mira available AND gold > 50"
        then: execute
        else: substitute with Craft (extra income)

  - phase: "evening"
    time: "19:00-21:00"
    actions:
      - Travel (→ tavern)
      - Trade (buy dinner)
      - Talk (social, information gathering)
      - Perform (occasional, if mood high)
    goal_alignment: social_relationship

  - phase: "night"
    time: "21:00-06:00"
    actions:
      - Travel (→ home)
      - Rest (sleep)
    goal_alignment: survival
    system_events:
      - level_up_check
      - skill_acquisition_check
      - goal_progress_review
```

### 4.3 Example: Villain NPC Daily Loop

```yaml
NPC: "Serena Blackwood"
Archetype: Villain
Class: [Merchant] Lv.28 / [Spy] Lv.19
Date: "2026-02-01"

Active_Goals:
  - "Maintain merchant cover" (survival, priority: 0.8)
  - "Control town's grain supply" (domination, priority: 0.9)
  - "Eliminate rival merchant" (destruction, priority: 0.6)
  - "Expand spy network" (acquisition, priority: 0.7)

Generated_Loop:
  - phase: "public_morning"
    time: "08:00-12:00"
    actions:
      - Trade (run shop, maintain cover)
      - Talk (gather information from customers)
    goal_alignment: [survival, acquisition]
    covert_actions:
      - Study (analyze customer patterns)
      - Tag generation: [social.trade, knowledge.identify, utility.stealth.disguise]

  - phase: "midday_meeting"
    time: "12:00-14:00"
    actions:
      - Travel (shop → private meeting location)
      - Talk (meet with grain supplier contact)
      - Trade (negotiate exclusive contract)
    goal_alignment: domination
    conditions:
      - if: "contact available AND not observed"
        covert: true

  - phase: "public_afternoon"
    time: "14:00-18:00"
    actions:
      - Trade (shop operations)
      - Talk (spread rumors about rival)
    goal_alignment: [survival, destruction]
    covert_actions:
      - social.diplomacy.deceive tag generation

  - phase: "covert_evening"
    time: "18:00-22:00"
    actions:
      - Travel (stealth route to warehouse district)
      - Study (rival's shipping schedules)
      - Command (direct agents to sabotage)
    goal_alignment: [destruction, acquisition]
    risk_level: moderate
    detection_consequences:
      - reputation_damage
      - potential_red_class: [Saboteur]

  - phase: "night"
    time: "22:00-08:00"
    actions:
      - Travel (safe house)
      - Rest
    goal_alignment: survival
```

---

## 5. Maintainer NPCs

### 5.1 Purpose

Maintainers are the backbone of the world. They:

- Produce goods and services players need
- Create economic demand that gives player activities value
- Provide social fabric and quest opportunities
- Stabilize the world against dramatic change

### 5.2 Maintainer Categories

```yaml
Economic_Maintainers:
  producers:
    - Farmers:      [gather.farming] → food supply
    - Miners:       [gather.mining] → ore supply
    - Loggers:      [gather.logging] → wood supply
    - Hunters:      [gather.hunting] → meat, leather supply
    - Fishers:      [gather.fishing] → fish supply

  processors:
    - Blacksmiths:  [craft.smithing] → tools, weapons, armor
    - Alchemists:   [craft.alchemy] → potions, medicines
    - Cooks/Chefs:  [craft.cooking] → prepared food
    - Weavers:      [craft.textile] → clothing, bags
    - Carpenters:   [craft.construction] → buildings, furniture

  distributors:
    - Merchants:    [social.trade] → goods distribution
    - Innkeepers:   [utility.service.innkeep] → lodging, food service
    - Couriers:     [utility.service.courier] → message/package delivery

Social_Maintainers:
  governance:
    - Guards:       [combat.*, utility.service.protect] → security
    - Officials:    [social.leadership, knowledge.lore] → administration
    - Judges:       [knowledge.lore, social.diplomacy] → dispute resolution

  knowledge:
    - Scholars:     [knowledge.*] → information preservation
    - Teachers:     [knowledge.teaching] → skill transmission
    - Healers:      [magic.restoration, knowledge.lore.medical] → health

  spiritual:
    - Priests:      [magic.divine, knowledge.lore.religion] → spiritual needs
    - Druids:       [magic.nature] → natural balance
```

### 5.3 Maintainer Behavioral Patterns

```yaml
Maintainer_Behavior:
  core_drives:
    - security: 'Ensure basic needs are met'
    - stability: 'Maintain current quality of life'
    - modest_improvement: 'Small gains over time'
    - community: 'Support neighbors and be supported'

  goal_characteristics:
    ambition_level: low_to_moderate
    risk_tolerance: low
    time_horizon: short_to_medium
    scope: personal_to_local

  response_to_disruption:
    minor: 'Adapt and continue'
    moderate: 'Seek help, adjust goals'
    major: 'May become refugee, seek new stability'
    catastrophic: 'Survival mode, may die or become ambitious'

  world_stabilization_effects:
    economic:
      - Produce goods at steady rate
      - Consume goods at predictable rate
      - Maintain price stability through consistent behavior
    social:
      - Fill service roles players depend on
      - Provide quest givers and information sources
      - Create community events and gatherings
    political:
      - Support existing power structures
      - Resist rapid change
      - Provide tax base and labor pool
```

### 5.4 Maintainer Simulation Optimization

Since Maintainers are 95% of NPCs, they need efficient simulation:

```yaml
Simulation_Tiers:
  tier_1_full:
    npcs: "Within player's current zone"
    update_rate: 'Real-time with player'
    detail: 'Full action resolution'

  tier_2_active:
    npcs: 'Adjacent zones, economically linked'
    update_rate: 'Every 5 minutes'
    detail: 'Simplified daily loop execution'

  tier_3_background:
    npcs: 'Distant zones'
    update_rate: 'Every hour'
    detail: 'Statistical outcomes, not individual actions'

  tier_4_dormant:
    npcs: 'Inactive regions'
    update_rate: 'On-demand or daily'
    detail: 'Aggregate population simulation'

Optimization_Techniques:
  - Batch processing of similar NPCs
  - Probabilistic outcome resolution
  - Caching of routine behaviors
  - Only simulate detail when observed
```

---

## 6. Hero & Villain NPCs

### 6.1 Purpose

Ambitious NPCs drive the narrative:

- Create world events that affect all players
- Provide high-stakes quest lines
- Introduce instability that creates opportunity
- Give the world a sense of history and change

### 6.2 Hero Archetypes

```yaml
Hero_Archetypes:
  The_Adventurer:
    motivation: 'Explore the unknown'
    goals: [exploration, discovery, reputation]
    world_effects:
      - Opens new areas
      - Discovers resources/dungeons
      - Creates maps and knowledge
    player_interaction:
      - Competition for discoveries
      - Cooperation on expeditions
      - Information trading

  The_Crusader:
    motivation: 'Protect the innocent'
    goals: [protection, destruction_of_evil, ideology]
    world_effects:
      - Hunts monsters/villains
      - Establishes safe zones
      - Builds defensive infrastructure
    player_interaction:
      - Ally against threats
      - Recruitment for causes
      - Moral dilemmas

  The_Reformer:
    motivation: 'Improve society'
    goals: [ideology, construction, social_relationship]
    world_effects:
      - Changes laws/customs
      - Builds institutions
      - Shifts political power
    player_interaction:
      - Political quests
      - Support or opposition choices
      - Social consequence chains

  The_Innovator:
    motivation: 'Create something new'
    goals: [creation, skill_acquisition, reputation]
    world_effects:
      - Introduces new recipes/techniques
      - Builds unique structures
      - Advances technology/magic
    player_interaction:
      - Learning opportunities
      - Crafting competitions
      - Resource quests

  The_Unifier:
    motivation: 'Bring people together'
    goals: [social_relationship, protection, ideology]
    world_effects:
      - Forges alliances
      - Ends conflicts
      - Creates trade networks
    player_interaction:
      - Diplomatic missions
      - Faction reputation impacts
      - Wedding/festival events
```

### 6.3 Villain Archetypes

```yaml
Villain_Archetypes:
  The_Tyrant:
    motivation: 'Power and control'
    goals: [domination, acquisition, destruction_of_rivals]
    world_effects:
      - Conquers territories
      - Imposes oppressive rules
      - Creates resistance movements
    player_interaction:
      - Serve or oppose
      - Resistance quests
      - Territory control impacts
    escalation_path:
      minor: 'Controls a gang'
      moderate: 'Rules a town'
      major: 'Commands a region'
      extreme: 'Threatens the realm'

  The_Crime_Lord:
    motivation: 'Wealth through exploitation'
    goals: [economic, domination, destruction_of_competition]
    world_effects:
      - Corrupts markets
      - Creates black markets
      - Funds other villains
    player_interaction:
      - Criminal opportunity
      - Investigation quests
      - Economic disruption

  The_Cultist:
    motivation: 'Serve dark powers'
    goals: [ideology, destruction, acquisition_of_artifacts]
    world_effects:
      - Summons monsters
      - Corrupts locations
      - Recruits followers
    player_interaction:
      - Cult infiltration
      - Artifact races
      - Corruption cleansing

  The_Conqueror:
    motivation: 'Expand through force'
    goals: [domination, destruction, reputation]
    world_effects:
      - Wars and invasions
      - Refugee crises
      - Power vacuum creation
    player_interaction:
      - War participation
      - Defense quests
      - Occupation resistance

  The_Corruptor:
    motivation: 'Twist the good'
    goals: [destruction, ideology, vengeance]
    world_effects:
      - Turns heroes to villains
      - Corrupts institutions
      - Spreads despair
    player_interaction:
      - Moral temptation
      - Rescue/redemption quests
      - Reputation attacks
```

### 6.4 Ambitious NPC Goal Chains

Ambitious NPCs have interconnected goal chains that escalate:

```yaml
Example_Villain_Goal_Chain:
  npc: "Lord Varen Blackthorn"
  archetype: Tyrant

  phase_1:
    timeframe: "months 1-6"
    life_purpose: "Rule the Northern Reach"
    long_term_goal: "Become Duke of Thornhold"
    medium_term_goals:
      - "Eliminate Baron Aldric" (destruction)
      - "Control Thornhold's guard" (domination)
      - "Secure noble alliances" (social_relationship)
    observable_effects:
      - Increased bandit activity (his agents)
      - Baron Aldric's caravans raided
      - Guard captain seen at Blackthorn manor
    player_hooks:
      - Hired to protect caravans
      - Investigate bandit origins
      - Guard captain behaving strangely

  phase_2:
    timeframe: "months 6-12"
    triggers: "Baron Aldric weakened OR player intervention"
    medium_term_goals:
      - "Stage coup in Thornhold" (domination)
      - "Silence witnesses" (destruction)
      - "Legitimize rule" (reputation)
    observable_effects:
      - Political tension in Thornhold
      - Disappearances of key figures
      - Propaganda spread
    player_hooks:
      - Protect witnesses
      - Expose plot
      - Support or oppose coup

  phase_3:
    timeframe: "months 12-24"
    triggers: "Coup succeeds OR fails"
    branching:
      success:
        long_term_goal: "Expand domain"
        effects: "Thornhold under tyranny, resistance forms"
      failure:
        long_term_goal: "Revenge and retry"
        effects: "Varen becomes outlaw, builds power in shadows"
      partial:
        long_term_goal: "Consolidate gains"
        effects: "Civil war in Thornhold"
```

### 6.5 World Event Generation

When Ambitious NPCs reach goal milestones, they generate World Events:

```yaml
World_Event:
  id: 'event_thornhold_coup'
  triggered_by: 'Lord Varen phase_2 completion'

  announcement:
    global: 'Rumors spread of upheaval in Thornhold'
    regional: 'Baron Aldric has fallen. Lord Varen claims the throne.'
    local: 'Guards in Blackthorn colors patrol the streets'

  effects:
    immediate:
      - faction_change: 'Thornhold → Blackthorn Dominion'
      - npc_disposition_shift: 'Thornhold citizens: fear +30'
      - trade_route_disruption: 'Northern roads: danger +50%'

    ongoing:
      - tax_increase: 'Thornhold region: +25%'
      - spawn_change: 'Resistance fighters appear'
      - quest_generation: 'Resistance, collaboration, neutrality paths'

    long_term:
      - economic_shift: 'Regional trade patterns change'
      - political_ripple: 'Other nobles react'
      - refugee_generation: 'NPCs flee to neighboring regions'

  player_participation:
    before: 'Could have prevented with intervention'
    during: 'Can participate in coup or defense'
    after: 'Can join resistance, serve new lord, or profit from chaos'

  duration: 'Permanent until countered by future events'
```

---

## 7. NPC Decision Making

### 7.1 Action Selection Algorithm

```
NPC ACTION SELECTION
━━━━━━━━━━━━━━━━━━━━

Input: Current state, Active goals, World state
Output: Next action

1. CHECK IMMEDIATE NEEDS
   IF hp < 20%: prioritize survival actions
   IF threat_nearby: evaluate fight/flight
   IF critical_opportunity: evaluate interrupt

2. CONSULT DAILY LOOP
   Get current phase from generated loop
   Get expected action(s) for this phase

3. EVALUATE CONTEXT
   Is expected action still valid?
   Have conditions changed since loop generation?
   Are there better opportunities?

4. APPLY PERSONALITY MODIFIERS
   risk_tolerance: affects dangerous action likelihood
   sociability: affects interaction frequency
   diligence: affects work vs leisure balance
   ambition: affects goal-advancing vs maintenance

5. SELECT ACTION
   IF loop_action valid AND context unchanged:
     execute loop_action
   ELSE:
     regenerate_options()
     score_by_goal_advancement()
     apply_personality_weights()
     select_highest_scored()

6. EXECUTE AND RECORD
   Perform action
   Generate tags
   Update action_history
   Update goal_progress
```

### 7.2 Personality Model

```yaml
NPC_Personality:
  traits: # Big Five inspired, 0-1 scale
    openness: 0.6 # Curiosity, creativity, exploration
    conscientiousness: 0.7 # Organization, diligence, reliability
    extraversion: 0.4 # Sociability, assertiveness
    agreeableness: 0.5 # Cooperation, trust, empathy
    neuroticism: 0.3 # Anxiety, stress response

  derived_behaviors:
    risk_tolerance: f(openness, neuroticism)
    work_ethic: f(conscientiousness)
    social_frequency: f(extraversion)
    negotiation_style: f(agreeableness, extraversion)
    stress_coping: f(neuroticism, conscientiousness)

  values: # Priority weights
    wealth: 0.4
    family: 0.8
    reputation: 0.5
    power: 0.2
    knowledge: 0.3
    faith: 0.6

  quirks: # Unique behavioral patterns
    - 'Always visits temple on rest days'
    - 'Refuses to trade with [Thieves]'
    - 'Gives discounts to [Warriors]'
```

### 7.3 Relationship System

NPCs track relationships that influence decisions:

```yaml
NPC_Relationships:
  relationship_entry:
    target: 'player_001' # Or NPC ID
    disposition: 0.65 # -1 (hatred) to +1 (love)
    familiarity: 0.4 # 0 (stranger) to 1 (intimate)
    trust: 0.5 # 0 (suspicious) to 1 (complete)
    respect: 0.7 # 0 (disdain) to 1 (reverence)

  disposition_factors:
    base_attitude: 'By archetype and personality'
    past_interactions: 'Weighted history'
    reputation: "Player's public standing"
    faction_alignment: 'Shared or opposed factions'
    class_affinity: 'Some classes respected/despised'

  relationship_effects:
    trade: 'Price modifiers based on disposition'
    talk: 'Dialogue options unlocked/locked'
    quest: 'Mission availability'
    combat: 'Ally/enemy determination'
    information: 'Willingness to share knowledge'
```

---

## 8. World Health System

### 8.1 World Health Metrics

The game tracks overall world health that Maintainers work to preserve:

```yaml
World_Health_Metrics:
  economic:
    supply_demand_balance: 0.85 # 1.0 = perfect equilibrium
    trade_route_safety: 0.72 # Active trade routes
    resource_availability: 0.90 # Raw materials accessible
    price_stability: 0.80 # Low inflation/deflation

  social:
    population_satisfaction: 0.75 # Average NPC satisfaction
    crime_rate: 0.15 # Inverse of safety
    institutional_integrity: 0.88 # Government functioning
    cultural_vitality: 0.70 # Events, arts, religion active

  environmental:
    monster_population: 0.40 # Threat level
    resource_regeneration: 0.95 # Sustainable harvesting
    corruption_spread: 0.05 # Magical/dark influence
    natural_balance: 0.85 # Druid concern

  political:
    faction_stability: 0.80 # Low conflict
    leadership_legitimacy: 0.75 # Accepted rulers
    border_security: 0.70 # External threats managed
```

### 8.2 Maintainer Response to Health Decline

```yaml
Health_Response_System:
  trigger: 'Metric falls below threshold'

  economic_response:
    supply_shortage:
      - Producers increase output goals
      - Merchants seek new suppliers
      - Prices rise (natural correction)
      - Players see "gathering bounties"

    trade_disruption:
      - Guards increase patrol goals
      - Merchants hire escorts
      - Alternative routes explored
      - Players see "caravan protection" quests

  social_response:
    crime_increase:
      - Guards increase presence
      - Merchants increase security
      - Citizens become wary
      - Players see "bounty" and "investigation" quests

    satisfaction_drop:
      - Innkeepers host events
      - Priests increase services
      - Leaders make concessions
      - Players see "morale" and "supply" quests

  environmental_response:
    monster_surge:
      - Hunters increase activity
      - Guards post warnings
      - Adventurers called
      - Players see "monster hunting" quests

    resource_depletion:
      - Gatherers slow down
      - Druids intervene
      - Prices rise
      - Players see "conservation" or "discovery" quests
```

---

## 9. Player-NPC Interaction

### 9.1 Player Influence on NPC Goals

Players can alter NPC goal structures:

```yaml
Player_Influence_Methods:
  direct:
    quest_completion:
      - Advances NPC goals
      - Builds relationship
      - May unlock new NPC goals

    gift_giving:
      - Improves disposition
      - May satisfy NPC needs
      - Can corrupt (bribery)

    trade:
      - Satisfies economic goals
      - Builds familiarity
      - Affects NPC resource state

    combat:
      - Defeats NPC enemies (hero assist)
      - Threatens NPC (intimidation)
      - Kills NPC (permanent removal)

  indirect:
    market_manipulation:
      - Affects NPC economic calculations
      - Can create/destroy opportunities

    reputation:
      - High reputation attracts NPC interest
      - Low reputation generates NPC opposition

    world_state_changes:
      - Player actions change world health
      - NPCs respond to new conditions

    faction_influence:
      - Player faction standing affects aligned NPCs
```

### 9.2 NPC Reactions to Players

```yaml
NPC_Player_Reactions:
  player_class_reactions:
    positive_affinity:
      - "[Merchant]" respects "[Merchant]" players
      - "[Guard]" trusts "[Knight]" players
      - "[Healer]" appreciates "[Healer]" players
    negative_affinity:
      - "[Merchant]" distrusts "[Thief]" players
      - "[Priest]" fears "[Necromancer]" players
      - "[Guard]" watches "[Assassin]" players

  player_level_reactions:
    low_level: "Dismissive or protective"
    peer_level: "Respectful, potential rival/ally"
    high_level: "Deferential, seeks favor"
    legendary: "Awe, fear, or challenge"

  player_action_reactions:
    helpful_action: "Disposition +, may offer reward/quest"
    harmful_action: "Disposition -, may seek revenge/report"
    ignored_request: "Disposition -, may spread reputation"
    exceptional_deed: "Major disposition shift, title generation"
```

---

## 10. Implementation Specifications

### 10.1 Data Requirements

```yaml
NPC_Data_Storage:
  per_npc:
    static: ~500 bytes # Identity, personality (cacheable)
    dynamic: ~2 KB # State, goals, relationships
    history: ~5 KB # Action history, goal history
    total: ~7.5 KB per NPC

  population_estimates:
    small_town: 50-200 NPCs
    large_town: 500-1000 NPCs
    city: 2000-5000 NPCs
    region: 10,000-50,000 NPCs
    world: 100,000-500,000 NPCs

  storage_requirements:
    minimum_world: ~750 MB (100k NPCs)
    typical_world: ~2.5 GB (300k NPCs)
    maximum_world: ~3.75 GB (500k NPCs)
```

### 10.2 Simulation Performance

```yaml
Simulation_Budget:
  target: 'Simulate world in <10% of real-time'

  per_tick_budget:
    tier_1 (real-time): 100 NPCs × full simulation
    tier_2 (5-min): 1,000 NPCs × simplified
    tier_3 (hourly): 10,000 NPCs × statistical
    tier_4 (daily): 100,000+ NPCs × aggregate

  optimization_requirements:
    - Spatial partitioning for tier assignment
    - Behavior caching for routine actions
    - Batch processing for similar NPCs
    - Event-driven updates vs polling
    - Goal evaluation throttling
```

### 10.3 Event Bus Architecture

```yaml
NPC_Event_System:
  event_types:
    npc_action_completed:
      - Updates tag affinities
      - Checks goal progress
      - May trigger world effects

    npc_goal_completed:
      - Generates new goals
      - May trigger world events
      - Updates NPC state

    npc_level_up:
      - Skill acquisition
      - Class evolution check
      - Goal recalculation

    world_event:
      - Broadcast to affected NPCs
      - NPCs re-evaluate goals
      - May trigger migrations

    player_interaction:
      - Relationship update
      - Goal impact assessment
      - Response generation
```

---

## 11. Emergent Narrative Examples

### 11.1 Organic Story: The Baker's Daughter

```
SETUP (No designer intervention):
- NPC "Elena" is daughter of "Baker Marcus"
- Elena has trait: high openness, moderate ambition
- Elena generated goal: "Learn magic" (skill_acquisition)
- No magic teacher in small village

EMERGENT SEQUENCE:
Week 1-4:
  - Elena's daily loop includes Study (any books available)
  - Study action generates low magic.arcane tags
  - Goal progress: 5%
  - Satisfaction declining (goal blocked)

Week 5-8:
  - Traveling [Mage] NPC "Orin" passes through
  - Elena's Talk action triggers information gathering
  - Learns of magic academy in distant city
  - New goal generated: "Travel to Astoria Academy"
  - Sub-goal: "Save money for journey"

Week 9-16:
  - Elena adds extra Craft shifts (baking)
  - Trade actions increase (selling directly)
  - Tags building: craft.cooking, social.trade
  - Unintended: Elena qualifying for [Merchant] class
  - Father notices less help (his goals affected)

Week 17-20:
  - Player enters village, buys from Elena
  - Elena's Talk action: mentions academy dream
  - PLAYER HOOK: Escort quest opportunity
  - If player ignores: Elena continues saving
  - If player helps: Journey event triggers

POSSIBLE OUTCOMES:
A) Elena reaches academy → becomes [Mage] NPC in city
B) Elena gives up → satisfaction drops, new goals
C) Elena waylaid → rescue quest for other players
D) Elena becomes [Merchant] instead → opens shop
```

### 11.2 Villain Emergence: The Scorned Apprentice

```
SETUP:
- NPC "Darren" is apprentice to "Master Smith Kael"
- Darren has trait: high ambition, low agreeableness
- Darren's goal: "Become Master Smith"
- Kael has another apprentice "Lyra" he favors

TRIGGER EVENT:
- Kael chooses Lyra as successor (goal-driven decision)
- Darren's goal: FAILED
- Satisfaction: crashes to 0.15
- Stress: spikes to 0.85

ARCHETYPE SHIFT:
- System evaluates Darren's state
- High ambition + low agreeableness + goal failure + stress
- Probability of archetype shift: HIGH
- Darren shifts: Maintainer → Villain (The Corruptor subtype)

NEW GOAL CHAIN:
- Life purpose shifts: "Ruin Kael and surpass him"
- Long-term: "Become better smith than Kael ever was"
- Medium-term: "Destroy Kael's reputation"
- Short-term: "Sabotage Kael's masterwork commission"

WORLD EFFECTS:
- Kael's important commission fails (Duke angry)
- Kael's reputation damaged (world health: institutional)
- Darren leaves town (missing person notice)
- Darren seeks forbidden smithing knowledge
- Players may encounter Darren later as antagonist
```

---

_Document Version 1.0 — NPC Systems Design_
