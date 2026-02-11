---
title: Game Design Documentation
gdd_ref: systems/core-progression-system-gdd.md#general
---

# Cozy Fantasy RPG - Design Documentation

This documentation is organized into two main sections:

- **[Systems](systems/index.md)** - How game mechanics work (mechanisms)
- **[Content](content/index.md)** - What specifically exists in the game (libraries)

## Quick Navigation

### Core Systems

| System         | Description                               | Entry Point                                                      |
| -------------- | ----------------------------------------- | ---------------------------------------------------------------- |
| Overview       | High-level game vision and design pillars | [systems/core/overview.md](systems/core/overview.md)             |
| Time & Rest    | Day/night cycle, stamina, rest mechanics  | [systems/core/time-and-rest.md](systems/core/time-and-rest.md)   |
| Currency       | Gold/silver/copper economy                | [systems/core/currency.md](systems/core/currency.md)             |
| Regional Tones | Geography-gated content and difficulty    | [systems/core/regional-tones.md](systems/core/regional-tones.md) |

### Character Systems

| System            | Description                       | Entry Point                                                                        |
| ----------------- | --------------------------------- | ---------------------------------------------------------------------------------- |
| Class Progression | Unlocking classes, XP, leveling   | [systems/character/class-progression.md](systems/character/class-progression.md)   |
| Attributes        | Strength, Finesse, Wit, etc.      | [systems/character/attributes.md](systems/character/attributes.md)                 |
| Skills            | Common and class-specific skills  | [systems/character/skills-overview.md](systems/character/skills-overview.md)       |
| Personality       | Trait system for NPCs and players | [systems/character/personality-traits.md](systems/character/personality-traits.md) |

### Combat Systems

| System             | Description                      | Entry Point                                                                      |
| ------------------ | -------------------------------- | -------------------------------------------------------------------------------- |
| Combat Resolution  | Damage, defense, positioning     | [systems/combat/combat-resolution.md](systems/combat/combat-resolution.md)       |
| Morale & Surrender | Rout, capture, ransom mechanics  | [systems/combat/morale-and-surrender.md](systems/combat/morale-and-surrender.md) |
| Party Mechanics    | Party formation, loyalty, orders | [systems/combat/party-mechanics.md](systems/combat/party-mechanics.md)           |
| Weapons & Armor    | Equipment categories and mastery | [systems/combat/weapons-and-armor.md](systems/combat/weapons-and-armor.md)       |

### Crafting Systems

| System               | Description                     | Entry Point                                                                            |
| -------------------- | ------------------------------- | -------------------------------------------------------------------------------------- |
| Crafting Progression | Specialties, recipes, discovery | [systems/crafting/crafting-progression.md](systems/crafting/crafting-progression.md)   |
| Gathering            | Resource nodes, harvesting      | [systems/crafting/gathering.md](systems/crafting/gathering.md)                         |
| Enchantment          | Item enchantment mechanics      | [systems/crafting/enchantment-mechanics.md](systems/crafting/enchantment-mechanics.md) |

### Economy Systems

| System          | Description                          | Entry Point                                                                  |
| --------------- | ------------------------------------ | ---------------------------------------------------------------------------- |
| Trade & Pricing | Supply/demand, merchant tiers        | [systems/economy/trade-and-pricing.md](systems/economy/trade-and-pricing.md) |
| Black Market    | Underground economy, dual reputation | [systems/economy/black-market.md](systems/economy/black-market.md)           |
| Fairs & Events  | Annual events, competitions          | [systems/economy/fairs-and-events.md](systems/economy/fairs-and-events.md)   |

### World Systems

| System              | Description                      | Entry Point                                                                  |
| ------------------- | -------------------------------- | ---------------------------------------------------------------------------- |
| Settlements         | Building, population, leadership | [systems/world/settlements.md](systems/world/settlements.md)                 |
| Settlement Policy   | Taxes, corruption, enforcement   | [systems/world/settlement-policy.md](systems/world/settlement-policy.md)     |
| Housing & Storage   | Player housing, containers       | [systems/world/housing-and-storage.md](systems/world/housing-and-storage.md) |
| Exploration         | Discovery, POIs, mapping         | [systems/world/exploration.md](systems/world/exploration.md)                 |
| Environment Hazards | Weather, terrain, dangers        | [systems/world/environment-hazards.md](systems/world/environment-hazards.md) |

### Social Systems

| System                | Description                            | Entry Point                                                                    |
| --------------------- | -------------------------------------- | ------------------------------------------------------------------------------ |
| NPC Simulation        | Daily life, needs, decisions           | [systems/social/npc-simulation.md](systems/social/npc-simulation.md)           |
| Factions & Reputation | Witnessed actions, standing            | [systems/social/factions-reputation.md](systems/social/factions-reputation.md) |
| Favourite NPCs        | Possession, priorities, chronicle mode | [systems/social/favourite-npcs.md](systems/social/favourite-npcs.md)           |
| Quests                | Quest system mechanics                 | [systems/social/quests.md](systems/social/quests.md)                           |
| Death & Undead        | Body decay, reanimation                | [systems/social/death-and-undead.md](systems/social/death-and-undead.md)       |

## Content Libraries

| Library      | Description                     | Entry Point                                                    |
| ------------ | ------------------------------- | -------------------------------------------------------------- |
| Classes      | All character class definitions | [content/classes/index.md](content/classes/index.md)           |
| Skills       | Skill definitions by pool       | [content/skills/index.md](content/skills/index.md)             |
| Materials    | Crafting materials              | [content/materials/index.md](content/materials/index.md)       |
| Items        | Weapons, armor, tools           | [content/items/index.md](content/items/index.md)               |
| Recipes      | Crafting recipes                | [content/recipes/index.md](content/recipes/index.md)           |
| Enchantments | Enchantment definitions         | [content/enchantments/index.md](content/enchantments/index.md) |
| Creatures    | Wildlife, monsters, bosses      | [content/creatures/index.md](content/creatures/index.md)       |
| Races        | Playable races and tribes       | [content/races/index.md](content/races/index.md)               |

## Development Tools

### SimulationViewer

Interactive console-based visualization and debugging tool for the game simulation.

| Resource                                                                               | Description                                                                    |
| -------------------------------------------------------------------------------------- | ------------------------------------------------------------------------------ |
| [User Guide](../tools/simulation-viewer.md)                                            | Complete usage documentation with keyboard commands and accessibility features |
| [Developer Guide](../tools/simulation-viewer-developer-guide.md)                       | Extending SimulationViewer with new renderers, panels, and commands            |
| [Testing Guide](../tools/simulation-viewer-testing.md)                                 | Writing automated tests, snapshot testing, and ViewerTestDriver usage          |
| [ADR: Terminal Choice](../adr/0011-simulation-viewer-terminal-choice.md)               | Why console UI over other options                                              |
| [ADR: Rendering Architecture](../adr/0012-simulation-viewer-rendering-architecture.md) | Pixel-art rendering approach                                                   |
| [ADR: Testing Strategy](../adr/0013-simulation-viewer-testing-strategy.md)             | Automatable UI testing approach                                                |

**Quick Start:**

```bash
dotnet run --project src/CozyFantasyRpg.SimulationViewer
```

## System Interconnections

```
TIME & WORLD ─────────────────────────────────────────────────────────────────
    │
    ├── STAMINA ──────────── REST ──────────── HEALTH
    │       │                  │                  │
    └───────┴──────────────────┼──────────────────┘
                               │
                            COMBAT
                               │
         ┌─────────────────────┼─────────────────────┐
         │                     │                     │
      CLASSES               MAGIC               CRAFTING
         │                     │                     │
         └─────────────────────┼─────────────────────┘
                               │
                         ECONOMY & TRADE
                               │
         ┌─────────────────────┼─────────────────────┐
         │                     │                     │
   MERCHANT GUILD      CRAFTING GUILD         BLACK MARKET
                               │
                        DUAL REPUTATION
                               │
         ┌─────────────────────┼─────────────────────┐
         │                     │                     │
    PERSONALITY            FACTIONS            SETTLEMENTS
                               │
                         PARTY SYSTEM
                               │
                       DEATH & BODIES
```
