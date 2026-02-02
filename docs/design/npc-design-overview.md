---
type: reference
scope: high-level
status: draft
version: 1.0.0
created: 2026-02-02
updated: 2026-02-02
subjects: [npc, architecture, design-philosophy, unified-character-model]
dependencies: []
---

# NPC Systems: Design Philosophy & Architecture

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

## Related Documents

- **[NPC Goals & Behavior](npc-goals-and-behavior.md)** — Goal system and daily loop mechanics
- **[NPC Archetypes](npc-archetypes.md)** — Maintainer, Hero, and Villain behaviors
- **[NPC Implementation](npc-implementation.md)** — Technical specifications and world health

---

_Document Version 1.0 — NPC Design Overview_
