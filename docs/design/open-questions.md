---
type: system
scope: detailed
status: draft
version: 1.0.0
created: 2026-02-02
updated: 2026-02-02
subjects: ['design', 'mechanics', 'gameplay']
dependencies: ['gdd', 'minimal-actions', 'npc-design']
---

# Open Questions Register

## Consolidated from Game Design Conversation

---

## Summary

| Category               | Open    | With Recommendation | Priority |
| ---------------------- | ------- | ------------------- | -------- |
| Core Systems           | 12      | 0                   | High     |
| Economy & Balance      | 8       | 0                   | High     |
| UI/UX                  | 6       | 0                   | High     |
| Content                | 9       | 0                   | Medium   |
| Technical (Original)   | 7       | 0                   | Medium   |
| Social/Multiplayer     | 6       | 0                   | Medium   |
| Monetization           | 4       | 0                   | Low      |
| Technical Architecture | 14      | 0                   | High     |
| Performance Testing    | 6       | 0                   | High     |
| Testing Infrastructure | 6       | 5                   | High     |
| Asset Pipeline         | 9       | 1                   | Medium   |
| NPC Systems            | 24      | 0                   | High     |
| **Total**              | **111** | **6**               |          |

---

## 1. Core Systems

### 1.1 Tag System

| #     | Question                                                            | Document        | Notes                                                                                            |
| ----- | ------------------------------------------------------------------- | --------------- | ------------------------------------------------------------------------------------------------ |
| 1.1.1 | What are the exact formulas for tag affinity decay?                 | GDD §3.4        | Mentioned as "5% per week" but needs: linear vs compound, floor values, activity reset mechanics |
| 1.1.2 | How do tag affinities interact with the idle loop efficiency curve? | GDD §6.2        | Do tags decay faster during low-efficiency idle periods?                                         |
| 1.1.3 | What is the tag XP scaling formula per action?                      | Minimal Actions | Base XP per action × modifiers not defined                                                       |
| 1.1.4 | How are "wildcard" tags (e.g., `craft.*`) resolved in requirements? | GDD §4.1        | Does any craft tag satisfy, or sum of all?                                                       |

### 1.2 Class System

| #     | Question                                                        | Document    | Notes                                                             |
| ----- | --------------------------------------------------------------- | ----------- | ----------------------------------------------------------------- |
| 1.2.1 | What happens to dormant classes?                                | GDD §4.4    | Can they be reactivated? Do they decay?                           |
| 1.2.2 | How is class evolution choice presented to the player?          | GDD §4.3    | Automatic based on tags, or player choice from qualified options? |
| 1.2.3 | Can players reject a class evolution and stay at current class? | GDD §4.3    | Strategic choice for tag optimization?                            |
| 1.2.4 | How do "excluding_tags" work for class acquisition?             | TWI Classes | Hard block or soft penalty?                                       |

### 1.3 Skill System

| #     | Question                                                               | Document   | Notes                                              |
| ----- | ---------------------------------------------------------------------- | ---------- | -------------------------------------------------- |
| 1.3.1 | How many skills can a player have active simultaneously?               | GDD §5     | Unlimited? Slot-based? Per-class limit?            |
| 1.3.2 | Can skills be forgotten/replaced?                                      | GDD §5     | Respec mechanics?                                  |
| 1.3.3 | How do skill tiers (Basic → Legendary) interact with tag requirements? | TWI Skills | Does [Greater Heal] require more tags than [Heal]? |
| 1.3.4 | What is the rarity roll formula for skill acquisition?                 | GDD §5.1   | Level modifier, tag affinity modifier, luck stat?  |

---

## 2. Economy & Balance

### 2.1 Resource Economy

| #     | Question                                                     | Document        | Notes                                                         |
| ----- | ------------------------------------------------------------ | --------------- | ------------------------------------------------------------- |
| 2.1.1 | What are the resource sink mechanisms?                       | GDD §9.2        | Repair costs, taxes, consumables mentioned but not quantified |
| 2.1.2 | How does inflation control work in a persistent MMO economy? | GDD             | Not addressed                                                 |
| 2.1.3 | What is the resource-to-XP conversion rate?                  | Minimal Actions | Gathering 100 ore = how much XP?                              |

### 2.2 Progression Balance

| #     | Question                                                              | Document    | Notes                                                        |
| ----- | --------------------------------------------------------------------- | ----------- | ------------------------------------------------------------ |
| 2.2.1 | What is the exact XP curve formula?                                   | GDD §7.2    | `Base XP = 100 × (Level ^ 1.5)` but needs full specification |
| 2.2.2 | How long should it take to reach level 10? 20? 30?                    | GDD §7      | Target hours/days not specified                              |
| 2.2.3 | What are the tag thresholds for each class tier?                      | TWI Classes | Level 10 evolution requires X tags, level 20 requires Y?     |
| 2.2.4 | How does the diminishing returns curve interact with premium players? | GDD §6.2    | Can rest skips reset efficiency?                             |
| 2.2.5 | What is the catch-up mechanic XP bonus percentage?                    | GDD §7.3    | Mentorship bonus = ?%                                        |

---

## 3. UI/UX Design

### 3.1 Mobile Interface

| #     | Question                                                                   | Document        | Notes                                   |
| ----- | -------------------------------------------------------------------------- | --------------- | --------------------------------------- |
| 3.1.1 | What are the detailed UI wireframes for each screen?                       | Minimal Actions | Layout sketch provided but not detailed |
| 3.1.2 | How does the context-sensitive action bar determine which actions to show? | Minimal Actions | Priority algorithm needed               |
| 3.1.3 | What is the notification strategy for idle progress?                       | GDD             | Push notifications? In-game mail?       |

### 3.2 Player Experience

| #     | Question                                                           | Document        | Notes                                             |
| ----- | ------------------------------------------------------------------ | --------------- | ------------------------------------------------- |
| 3.2.1 | What is the new player onboarding flow (first 30 minutes)?         | Minimal Actions | Mentioned as potential expansion                  |
| 3.2.2 | How is the class acquisition "voice in your head" presented in UI? | GDD             | Modal? Toast? Cinematic?                          |
| 3.2.3 | How are Signature Skills named procedurally?                       | GDD §5.4        | Algorithm for "[PlayerName]'s [Adjective] [Noun]" |

---

## 4. Content Design

### 4.1 World & Locations

| #     | Question                              | Document        | Notes                              |
| ----- | ------------------------------------- | --------------- | ---------------------------------- |
| 4.1.1 | What is the world map structure?      | GDD             | Zones? Nodes? Open world?          |
| 4.1.2 | How many starting areas/paths exist?  | GDD             | Single start or class-based?       |
| 4.1.3 | What locations support which actions? | Minimal Actions | Full location-action matrix needed |

### 4.2 Combat Content

| #     | Question                                    | Document | Notes                              |
| ----- | ------------------------------------------- | -------- | ---------------------------------- |
| 4.2.1 | How do dungeons/raids work in idle context? | GDD      | Not addressed                      |
| 4.2.2 | What is the enemy scaling formula?          | GDD      | Level-based? Zone-based?           |
| 4.2.3 | How does boss content work when idling?     | GDD      | Auto-attempt? Require active play? |

### 4.3 Recipes & Spells

| #     | Question                                 | Document   | Notes                                  |
| ----- | ---------------------------------------- | ---------- | -------------------------------------- |
| 4.3.1 | How many recipes are needed for launch?  | GDD §5.5   | Per craft category                     |
| 4.3.2 | How many spells per magic school?        | TWI Skills | Minimum viable spell list              |
| 4.3.3 | What is the recipe/spell discovery rate? | GDD §5.5   | Random drops vs guaranteed progression |

---

## 5. Technical Implementation

### 5.1 Idle Simulation

| #     | Question                                                      | Document        | Notes                                                                |
| ----- | ------------------------------------------------------------- | --------------- | -------------------------------------------------------------------- |
| 5.1.1 | What is the offline simulation algorithm in detail?           | GDD §10.1       | Compressed simulation with "randomized variance" needs specification |
| 5.1.2 | How are combat encounters resolved during offline simulation? | Minimal Actions | Simplified combat model needed                                       |
| 5.1.3 | What is the maximum offline time supported?                   | GDD             | 24h? 48h? Unlimited with diminishing returns?                        |

### 5.2 Anti-Exploitation

| #     | Question                                    | Document  | Notes                         |
| ----- | ------------------------------------------- | --------- | ----------------------------- |
| 5.2.1 | What constitutes "botting" in an idle game? | GDD §10.2 | Detection heuristics          |
| 5.2.2 | How does activity pattern analysis work?    | GDD §10.2 | Machine learning? Rule-based? |

### 5.3 Data Architecture

| #     | Question                                              | Document  | Notes                   |
| ----- | ----------------------------------------------------- | --------- | ----------------------- |
| 5.3.1 | What is the database schema for tag affinity storage? | GDD §10.3 | Sparse vs dense storage |
| 5.3.2 | How is the action history (rolling 1000) managed?     | GDD §10.3 | Ring buffer? Archival?  |

---

## 6. Social & Multiplayer

### 6.1 Guild Systems

| #     | Question                                      | Document | Notes                                       |
| ----- | --------------------------------------------- | -------- | ------------------------------------------- |
| 6.1.1 | How do guild-exclusive class evolutions work? | GDD §8.3 | Requirements, restrictions                  |
| 6.1.2 | What are the shared activity loop mechanics?  | GDD §8.3 | How does "guild mining operation" function? |

### 6.2 Player Interaction

| #     | Question                                         | Document | Notes                        |
| ----- | ------------------------------------------------ | -------- | ---------------------------- |
| 6.2.1 | Is there PvP? If so, how does it work?           | GDD      | Not addressed                |
| 6.2.2 | How does player trading work?                    | GDD      | Auction house? Direct trade? |
| 6.2.3 | Can players visit each other's inns/settlements? | GDD      | Social features undefined    |

### 6.3 Red Class Enforcement

| #     | Question                                      | Document    | Notes                            |
| ----- | --------------------------------------------- | ----------- | -------------------------------- |
| 6.3.1 | How does the bounty system work mechanically? | TWI Classes | Player-placed? System-generated? |

---

## 7. Monetization

| #   | Question                                         | Decision | Rationale                        | Date |
| --- | ------------------------------------------------ | -------- | -------------------------------- | ---- |
| 7.1 | What is the premium currency name and earn rate? | GDD §9   | Not specified                    |      |
| 7.2 | What are the exact prices for allowed purchases? | GDD §9   | Cosmetics, slots, etc.           |      |
| 7.3 | Is there a battle pass or seasonal content?      | GDD      | Not addressed                    |      |
| 7.4 | How do "limited rest cycle skips" work?          | GDD §9   | Daily limit? Diminishing effect? |      |

---

## 8. Technical Architecture (NEW)

### 8.1 Offline/Online Architecture

| #     | Question                                                              | Document        | Notes                              |
| ----- | --------------------------------------------------------------------- | --------------- | ---------------------------------- |
| 8.1.1 | What is the maximum offline play duration before sync is recommended? | Tech Stack §5   | Days? Weeks? Unlimited?            |
| 8.1.2 | How are sync conflicts presented to players?                          | Tech Stack §5.2 | UI/UX for conflict resolution      |
| 8.1.3 | Can players have multiple solo saves AND a multiplayer character?     | Tech Stack §3   | Separate or linked?                |
| 8.1.4 | What happens to a solo world when uploaded to multiplayer?            | Tech Stack §5   | NPCs merge? Fresh server world?    |
| 8.1.5 | How is cheating detected in solo mode before sync?                    | Tech Stack §5.3 | Deterministic verification limits? |

### 8.2 Multiplayer Infrastructure

| #     | Question                                                 | Document        | Notes                             |
| ----- | -------------------------------------------------------- | --------------- | --------------------------------- |
| 8.2.1 | What is acceptable cold-start latency for scale-to-zero? | Tech Stack §4.3 | 10-15 sec mentioned, is this OK?  |
| 8.2.2 | How are Orleans silos distributed geographically?        | Tech Stack §4   | Single region or multi-region?    |
| 8.2.3 | What is the shard/server strategy?                       | Tech Stack §4   | One world or multiple shards?     |
| 8.2.4 | How is player data backed up and restored?               | Tech Stack §4.1 | Backup frequency, restore process |
| 8.2.5 | What is the disaster recovery plan?                      | Tech Stack §4   | RTO/RPO requirements              |

### 8.3 Performance & Limits

| #     | Question                                                 | Document        | Notes                               |
| ----- | -------------------------------------------------------- | --------------- | ----------------------------------- |
| 8.3.1 | What is the target NPC count per zone in multiplayer?    | Tech Stack §4.2 | Grain limits per silo               |
| 8.3.2 | What is the maximum concurrent players per server/shard? | Tech Stack §4.4 | Cost scaling suggests 100k possible |
| 8.3.3 | How much local storage does solo mode require?           | Tech Stack §3   | LiteDB size limits                  |
| 8.3.4 | What are the mobile device minimum specs?                | Tech Stack §3   | iOS/Android requirements            |

### 8.4 Testing Infrastructure

| #     | Question                                                     | Document              | Status | Recommendation                                                       |
| ----- | ------------------------------------------------------------ | --------------------- | ------ | -------------------------------------------------------------------- |
| 8.4.1 | What is the CI/CD pipeline structure?                        | Testing Strategy §7   | OPEN   | GitHub Actions with unit/nightly/weekly/release stages               |
| 8.4.2 | How are BDD tests organized by feature area?                 | Testing Strategy §3.1 | OPEN   | Feature directory per game system (ClassSystem/, SkillSystem/, etc.) |
| 8.4.3 | What is the test coverage target?                            | Testing Strategy §7   | OPEN   | 80% line coverage minimum                                            |
| 8.4.4 | How is test data managed and seeded?                         | Testing Strategy §8   | OPEN   | Bogus fakers + Verify snapshots                                      |
| 8.4.5 | How are test environments provisioned for integration tests? | Testing Strategy §5   | OPEN   | TestContainers for Postgres/Redis                                    |
| 8.4.6 | What is the mutation testing score target?                   | Testing Strategy §7   | OPEN   | Needs definition                                                     |

### 8.5 Performance Testing

| #     | Question                                                              | Document        | Notes                              |
| ----- | --------------------------------------------------------------------- | --------------- | ---------------------------------- |
| 8.5.1 | What are the specific performance SLAs?                               | Tech Stack §7.5 | Response times, throughput targets |
| 8.5.2 | How many concurrent players must the system support at launch?        | Tech Stack §7.5 | Baseline for load tests            |
| 8.5.3 | What is the target NPC tick rate under load?                          | Tech Stack §7.5 | NPCs per second per silo           |
| 8.5.4 | What are the mobile device performance budgets?                       | Tech Stack §7.5 | CPU, memory, battery               |
| 8.5.5 | How are performance test environments provisioned?                    | Tech Stack §7.5 | Dedicated? Shared? Cost?           |
| 8.5.6 | What is the performance regression threshold before blocking release? | Tech Stack §7.5 | 10% mentioned, confirm             |

### 8.6 Asset Pipeline

| #     | Question                                                                       | Document         | Status | Recommendation                              |
| ----- | ------------------------------------------------------------------------------ | ---------------- | ------ | ------------------------------------------- |
| 8.6.1 | What art style will be used?                                                   | Tech Stack §10.3 | OPEN   | Cozy Fantasy                                |
| 8.6.2 | What is the target resolution/DPI for assets?                                  | Tech Stack §10.4 | OPEN   | Needs definition                            |
| 8.6.3 | How will Pixellab.ai prompts be standardized for consistency?                  | Tech Stack §10.3 | OPEN   | Base prompt framework defined in Tech Stack |
| 8.6.4 | What is the asset naming and tagging convention?                               | Tech Stack §10.4 | OPEN   | Needs definition                            |
| 8.6.5 | How will NPC portrait variety be managed?                                      | Tech Stack §10.2 | OPEN   | Needs definition                            |
| 8.6.6 | What animation approach for idle game?                                         | Tech Stack §10.2 | OPEN   | Needs definition                            |
| 8.6.7 | How are assets versioned and updated post-launch?                              | Tech Stack §10.1 | OPEN   | Needs definition                            |
| 8.6.8 | What are the specific hex codes for the cozy fantasy palette?                  | Tech Stack §10.3 | OPEN   | Needs definition                            |
| 8.6.9 | How do "darker" elements (villains, dungeons, red classes) fit the cozy style? | Tech Stack §10.3 | OPEN   | Needs definition                            |

---

## 9. NPC Systems

### 8.1 Goal System

| #     | Question                                                | Document        | Notes                                                  |
| ----- | ------------------------------------------------------- | --------------- | ------------------------------------------------------ |
| 8.1.1 | How frequently are NPC goals re-evaluated?              | NPC Design §3.4 | Weekly mentioned, but what triggers immediate re-eval? |
| 8.1.2 | What is the maximum number of concurrent goals per NPC? | NPC Design §3.2 | Per timeframe category? Total?                         |
| 8.1.3 | How are conflicting goals resolved?                     | NPC Design §3   | Priority only, or more complex resolution?             |
| 8.1.4 | Can NPCs have secret goals hidden from players?         | NPC Design §6.3 | Villain covert operations imply yes, but mechanics?    |
| 8.1.5 | How is goal "failure" determined vs "abandoned"?        | NPC Design §3.2 | Timeout? Blocked progress?                             |

### 8.2 NPC Simulation

| #     | Question                                                | Document        | Notes                                     |
| ----- | ------------------------------------------------------- | --------------- | ----------------------------------------- |
| 8.2.1 | What is the exact algorithm for tier assignment?        | NPC Design §5.4 | Spatial? Economic link? Player interest?  |
| 8.2.2 | How do NPCs transition between simulation tiers?        | NPC Design §5.4 | Hysteresis to prevent thrashing?          |
| 8.2.3 | What happens when player observes a Tier 4 NPC?         | NPC Design §5.4 | Instant full simulation? Interpolation?   |
| 8.2.4 | How are "statistical outcomes" calculated for Tier 3/4? | NPC Design §5.4 | Monte Carlo? Predetermined distributions? |

### 8.3 NPC Progression

| #     | Question                                                          | Document        | Notes                                 |
| ----- | ----------------------------------------------------------------- | --------------- | ------------------------------------- |
| 8.3.1 | Do NPCs gain XP at the same rate as players?                      | NPC Design §2.1 | Or scaled for world time progression? |
| 8.3.2 | Can NPCs reach legendary levels (50+)?                            | NPC Design      | Only Heroes/Villains?                 |
| 8.3.3 | How do NPCs acquire new skills?                                   | NPC Design      | Same random system as players?        |
| 8.3.4 | Can NPCs get Signature Skills?                                    | GDD §5.4        | Would make them truly unique          |
| 8.3.5 | Do NPC classes evolve automatically or based on tag distribution? | NPC Design §2.1 | Same as players presumably            |

### 8.4 NPC Population

| #     | Question                                            | Document         | Notes                                  |
| ----- | --------------------------------------------------- | ---------------- | -------------------------------------- |
| 8.4.1 | How are NPCs generated at world start?              | NPC Design §10.1 | Pre-generated? Procedural?             |
| 8.4.2 | How do new NPCs enter the world?                    | NPC Design       | Birth? Immigration?                    |
| 8.4.3 | How do NPCs die or leave?                           | NPC Design       | Age? Violence? Migration?              |
| 8.4.4 | What is the target NPC-to-player ratio?             | NPC Design §10.1 | Per server/shard                       |
| 8.4.5 | How are "important" NPCs (Heroes/Villains) spawned? | NPC Design §6    | At world gen? Emerge from Maintainers? |

### 8.5 World Events

| #     | Question                                                 | Document        | Notes                              |
| ----- | -------------------------------------------------------- | --------------- | ---------------------------------- |
| 8.5.1 | How many concurrent world events can exist?              | NPC Design §6.5 | Caps? Regional limits?             |
| 8.5.2 | What is the frequency target for major events?           | NPC Design §6.5 | Weekly? Monthly?                   |
| 8.5.3 | Can players trigger world events directly?               | NPC Design §6.5 | Or only through NPC influence?     |
| 8.5.4 | How are world events resolved if no players participate? | NPC Design §6.5 | NPC-only resolution? Timeout?      |
| 8.5.5 | How do world events affect server economy and balance?   | NPC Design §6.5 | Controlled chaos vs true emergence |

### 8.6 NPC-Player Interaction

| #     | Question                                      | Document        | Notes                                |
| ----- | --------------------------------------------- | --------------- | ------------------------------------ |
| 8.6.1 | Can players hire NPCs as permanent followers? | NPC Design §9   | Companion system?                    |
| 8.6.2 | Can players marry NPCs?                       | NPC Design §7.3 | Relationship system implies possible |
| 8.6.3 | Can players kill essential Maintainer NPCs?   | NPC Design §5   | World health consequences?           |
| 8.6.4 | How do players discover NPC goals?            | NPC Design §9   | Talk reveals? Observation? Skills?   |

---

## 10. Explicit Questions Raised in Documents

These questions were explicitly posed during the conversation:

### From GDD Creation

> "Want me to expand on any particular system, or develop a specific aspect further (economy balancing, dungeon/raid idle mechanics, PvP considerations, etc.)?"

### From Tag System Addition

> "Want me to expand on any particular aspect, such as tag balancing formulas, the decay algorithm, or how tags interact with the idle loop system?"

### From Gap Analysis

> "Would you like me to expand any particular area, such as developing the full `magic.divine` skill tree, creating detailed recipe examples with tag mappings, or building out the farming/agriculture system?"

### From Minimal Actions Document

> "Would you like me to:
>
> 1. Design detailed UI wireframes for the mobile interface?
> 2. Create the idle loop configuration schema in more detail?
> 3. Define the random event/interrupt system for idle play?
> 4. Build out the 'first 30 minutes' new player experience flow?"

### From Testing Strategy Document

> "How will game features be automatically tested?"
> **ANSWERED:** See Testing Strategy Document — BDD with Reqnroll, three execution modes (Unit/Integration/E2E), performance testing with NBomber/BenchmarkDotNet

---

## 11. Implicit Gaps Identified

Areas mentioned but not developed:

| Area                          | Mentioned In        | Status                           |
| ----------------------------- | ------------------- | -------------------------------- |
| Settlement building           | GDD §8.3            | Name only                        |
| Territory control             | GDD §8.3            | Name only                        |
| Passive resource generation   | GDD §8.3            | Name only                        |
| Dream events (narrative)      | Minimal Actions §12 | Name only                        |
| Random events during idle     | Minimal Actions     | Not specified                    |
| Equipment/gear system         | Multiple            | Not addressed                    |
| Inventory management          | Multiple            | Not addressed                    |
| Stat/attribute system         | Multiple            | Not addressed                    |
| Quest system                  | Multiple            | Not addressed                    |
| Achievement system            | Multiple            | Not addressed                    |
| Tutorial system               | Multiple            | Not addressed                    |
| Localization requirements     | -                   | Not addressed                    |
| Accessibility features        | -                   | Not addressed                    |
| Audio/music design            | -                   | Not addressed                    |
| Art style/visual identity     | -                   | Not addressed                    |
| NPC dialogue generation       | NPC Design §9       | Implied but not specified        |
| NPC memory of player actions  | NPC Design §7.3     | Relationship tracks, but detail? |
| NPC death/permadeath rules    | NPC Design §8       | Implied but not specified        |
| NPC naming conventions        | NPC Design §2.1     | Procedural? Pre-set lists?       |
| NPC appearance/visual variety | NPC Design          | Not addressed                    |
| Hero/Villain spawn frequency  | NPC Design §2.2     | 5% mentioned, but spawn rate?    |
| NPC faction membership        | NPC Design §7.3     | Implied, not detailed            |
| NPC schedule variations       | NPC Design §4       | Weekday vs weekend? Seasons?     |

---

## Priority Recommendation

### Immediate (Required for Prototype)

1. XP and tag formulas (§2.2.1, §1.1.3)
2. First 30 minutes flow (§3.2.1)
3. Offline simulation algorithm (§5.1.1)
4. Minimum recipe/spell lists (§4.3.1, §4.3.2)
5. Equipment system (§9)

### Short-term (Required for Alpha)

1. Full UI wireframes (§3.1.1)
2. World map structure (§4.1.1)
3. Combat encounter resolution (§5.1.2)
4. Economy sinks (§2.1.1)
5. Idle loop configuration schema

### Medium-term (Required for Beta)

1. Guild mechanics (§6.1)
2. PvP decision (§6.2.1)
3. Dungeon/raid design (§4.2.1)
4. Monetization specifics (§7)
5. Random event system

### Long-term (Post-Launch)

1. Settlement building
2. Territory control
3. Seasonal content
4. Advanced social features

---

_Document Version 1.0 — 52 Open Questions Identified_

---

## Related Documents

- **[Idle Game Overview](idle-game-overview.md)** — Overall game concept providing context for many questions
- **[Idle Inn/Tavern Management](idle-inn-gdd.md)** — Core GDD referenced in most system questions
- **[The Wandering Inn: Class Tag Mapping](twi-classes.md)** — Tag system with many open implementation questions
- **[The Wandering Inn: Skill Tag Mapping](twi-skills.md)** — Skill system with balance and progression questions
- **[Minimal Action Set for Idle Mobile Implementation](minimal-actions.md)** — Mobile implementation referenced in UI questions
- **[NPC Systems Design Document](npc-design.md)** — NPC simulation and behavior questions
- **[.NET Technology Stack Recommendation](tech-stack.md)** — Technical architecture questions
- **[Automated Testing Strategy](testing-strategy.md)** — Testing infrastructure questions
- **[Gap Analysis](gap-analysis.md)** — Content gaps identified in tag taxonomy
