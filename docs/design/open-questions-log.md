# Conversation System - Open Questions Log

## How to Use This Document

Track design decisions that need resolution. Update status as decisions are made. Archive resolved items at the bottom with rationale for future reference.

---

## Open Questions

### Architecture & Technical

| # | Question | Impact | Priority | Notes |
|---|----------|--------|----------|-------|
| A1 | On-device ML vs pure lookup for response selection? | App size, performance, offline capability | High | Current leaning: compile-time LLM generation, runtime lookup |
| A2 | How is conversation state persisted? Per-save, or separate system? | Save file structure, resume behaviour | Medium | |
| A3 | Localisation strategy—do voice variants differ per language, or just translate? | Authoring workload, cultural authenticity | Medium | |
| A4 | What triggers conversation exit? Explicit farewell only, or timeout/walk-away? | UX, state handling | Low | |

### Content & Authoring

| # | Question | Impact | Priority | Notes |
|---|----------|--------|----------|-------|
| C1 | How many races/cultures need distinct voice variants? | Authoring scale: N templates × M voices | High | |
| C2 | Do NPCs share response pools by role, or is each NPC fully unique? | Content volume, consistency | High | e.g., all blacksmiths share base responses with individual quirks? |
| C3 | Are contracts fixed (hand-authored) or procedurally parameterised? | Replayability vs authoring control | Medium | |
| C4 | Minimum voice variants needed for distinctiveness? | Authoring budget | Medium | Hypothesis: 3 is enough, 5 is polish |
| C5 | Who authors content—designers, writers, or LLM-assisted pipeline? | Tooling needs, quality control | Medium | |

### Player Experience

| # | Question | Impact | Priority | Notes |
|---|----------|--------|----------|-------|
| P1 | Does the player see all available words, or are some hidden until unlocked? | UI complexity, discovery feel | High | |
| P2 | How does the UI communicate *why* a word/option is unavailable? | Player frustration, guidance | High | Risk: spoilers vs confusion |
| P3 | Can players skip/fast-forward NPC responses? | Accessibility, pacing | Medium | |
| P4 | Is there a conversation log/history players can review? | Narrative tracking, quest clarity | Medium | |
| P5 | How is tone/politeness feedback communicated? | Player learning, consequence clarity | Low | e.g., NPC expression change, text colour? |

### Game Design

| # | Question | Impact | Priority | Notes |
|---|----------|--------|----------|-------|
| G1 | Does relationship persist across all NPCs of a faction, or per individual? | World feel, consequence weight | High | |
| G2 | Can players fail contracts? What are consequences? | Stakes, player stress | Medium | |
| G3 | Are there "missable" conversations or content? | Completionist concerns, replayability | Medium | |
| G4 | Do NPCs have schedules/availability, or always present? | World simulation scope | Low | |
| G5 | Can NPCs die or become unavailable? How does that affect ongoing contracts? | Edge case handling | Low | |

### Inventory & Economy (Blacksmith-specific)

| # | Question | Impact | Priority | Notes |
|---|----------|--------|----------|-------|
| E1 | Is blacksmith inventory static, randomised, or progression-gated? | Player agency, balance | Medium | |
| E2 | Can players sell items to the blacksmith? | UI scope, economy balance | Medium | |
| E3 | Does completing contracts unlock new inventory? | Reward loop, progression | Medium | |
| E4 | Regional/racial variation in available goods? | World-building, travel incentive | Low | |

---

## Decisions Made

| # | Question | Decision | Rationale | Date |
|---|----------|----------|-----------|------|
| | | | | |

---

## Parking Lot

Items that are interesting but not blocking current work.

- Multiplayer implications (if any)
- Mod support for custom word lists / responses
- Accessibility: screen reader support for word selection UI
- Analytics: tracking which conversation paths players use most

---

## Next Review Date

_To be scheduled after initial paper prototype testing_
