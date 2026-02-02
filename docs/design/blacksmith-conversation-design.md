---
type: system
scope: detailed
status: draft
version: 1.0.0
created: 2026-02-02
updated: 2026-02-02
subjects: [conversation, npc, social]
dependencies: []
---

# Blacksmith Conversation System Design

## Overview

This document defines the conversation structure for player interactions with a blacksmith NPC, supporting two primary goals: purchasing a sword and accepting a supply contract.

---

## Conversation State

The system tracks the following state per conversation:

| State Variable      | Type | Purpose                                     |
| ------------------- | ---- | ------------------------------------------- |
| `relationship`      | enum | stranger / acquaintance / trusted           |
| `current_topic`     | enum | none / browsing / sword_purchase / contract |
| `contract_offered`  | bool | Has the blacksmith mentioned the contract?  |
| `contract_accepted` | bool | Has the player accepted?                    |
| `haggling_attempts` | int  | Limits repeated price negotiation           |
| `player_gold`       | int  | Reference for purchase validation           |

---

## Player Input Structure

Players construct phrases by selecting from categorised word/phrase lists. The UI presents slots that the player fills:

```
[Greeting/Opener] + [Intent] + [Subject] + [Modifier?]
```

Not all slots required for every exchange—context determines available slots.

---

## Word Lists by Category

### Greetings / Openers

Used to initiate or shift conversation tone.

| Word/Phrase | Tone          | Notes                                   |
| ----------- | ------------- | --------------------------------------- |
| Hello       | Neutral       |                                         |
| Greetings   | Formal        |                                         |
| Hey         | Casual        | May affect disposition with formal NPCs |
| Well met    | Respectful    | Cultural variation candidate            |
| I need help | Direct/Urgent | Skips pleasantries                      |
| Listen      | Demanding     | Risk of negative response               |

### Intent

The core action the player wants to take.

| Word/Phrase      | Mapped Intent |
| ---------------- | ------------- |
| I want to buy    | purchase      |
| Show me          | browse        |
| How much for     | price_check   |
| Tell me about    | inquire       |
| I accept         | agreement     |
| I refuse         | rejection     |
| I can help with  | offer_service |
| What do you need | solicit_quest |
| Farewell         | exit          |
| Let me think     | defer         |

### Subjects

What the intent applies to.

| Word/Phrase       | Category               | Context-Gated?                     |
| ----------------- | ---------------------- | ---------------------------------- |
| swords            | merchandise            | No                                 |
| your finest blade | merchandise (premium)  | No                                 |
| something cheap   | merchandise (budget)   | No                                 |
| that one          | merchandise (specific) | Requires prior browse              |
| the work          | contract               | Requires `contract_offered = true` |
| the terms         | contract               | Requires `contract_offered = true` |
| ore               | contract_subject       | Requires contract context          |
| iron              | contract_subject       |                                    |
| silver            | contract_subject       |                                    |
| supplies          | contract_subject       |                                    |
| your trade        | small_talk             |                                    |
| this town         | small_talk             |                                    |

### Modifiers (Optional)

Adjust tone, urgency, or add conditions.

| Word/Phrase             | Effect                                      |
| ----------------------- | ------------------------------------------- |
| ...please               | +politeness                                 |
| ...now                  | +urgency, -politeness                       |
| ...if the price is fair | Opens haggling                              |
| ...for a friend         | Flavour / possible discount trigger         |
| ...quietly              | Implies discretion, may unlock hidden stock |

---

## Conversation Flows

### Flow 1: Sword Purchase

```
┌─────────────────────────────────────────────────────────────┐
│ ENTRY                                                       │
│ Player: [Greeting] + [browse/purchase intent] + [swords]    │
└─────────────────────────────────────────────────────────────┘
                              │
                              ▼
┌─────────────────────────────────────────────────────────────┐
│ BROWSE STATE                                                │
│ Blacksmith describes available swords (2-4 options)         │
│ System sets: current_topic = browsing                       │
│                                                             │
│ Player options:                                             │
│   • [How much for] + [that one / specific sword name]       │
│   • [Tell me about] + [sword name]                          │
│   • [Show me] + [something cheap / your finest blade]       │
│   • [Farewell]                                              │
└─────────────────────────────────────────────────────────────┘
                              │
                              ▼
┌─────────────────────────────────────────────────────────────┐
│ PRICE CHECK                                                 │
│ Blacksmith states price                                     │
│                                                             │
│ Player options:                                             │
│   • [I want to buy] + [that one]                            │
│   • [How much for] + [...if the price is fair] → haggle     │
│   • [Show me] + [something else]                            │
│   • [Let me think]                                          │
└─────────────────────────────────────────────────────────────┘
                              │
              ┌───────────────┴───────────────┐
              ▼                               ▼
┌──────────────────────┐        ┌──────────────────────────┐
│ PURCHASE             │        │ HAGGLE (max 2 attempts)  │
│ If gold sufficient:  │        │ Blacksmith counters or   │
│   Transaction occurs │        │ refuses based on:        │
│   Sword added to inv │        │   • relationship         │
│ Else:                │        │   • haggling_attempts    │
│   Blacksmith refuses │        │   • item margin          │
└──────────────────────┘        └──────────────────────────┘
```

### Flow 2: Supply Contract

```
┌─────────────────────────────────────────────────────────────┐
│ CONTRACT TRIGGER                                            │
│ Occurs when:                                                │
│   • Player asks [What do you need]                          │
│   • OR relationship >= acquaintance and random chance       │
│   • OR player mentions [ore/supplies] in conversation       │
│                                                             │
│ Blacksmith introduces problem: needs steady ore supply      │
│ System sets: contract_offered = true                        │
└─────────────────────────────────────────────────────────────┘
                              │
                              ▼
┌─────────────────────────────────────────────────────────────┐
│ CONTRACT DETAILS                                            │
│ Player can now access contract-gated subjects               │
│                                                             │
│ Player options:                                             │
│   • [Tell me about] + [the terms]                           │
│   • [Tell me about] + [the work]                            │
│   • [I accept] + [the work]                                 │
│   • [I refuse]                                              │
│   • [Let me think]                                          │
└─────────────────────────────────────────────────────────────┘
                              │
              ┌───────────────┼───────────────┐
              ▼               ▼               ▼
┌────────────────┐  ┌────────────────┐  ┌────────────────────┐
│ INQUIRE TERMS  │  │ ACCEPT         │  │ DEFER              │
│ Blacksmith     │  │ Contract added │  │ contract_offered   │
│ explains:      │  │ to quest log   │  │ remains true       │
│  • Quantities  │  │ relationship++ │  │ Can revisit later  │
│  • Payment     │  │                │  │                    │
│  • Deadline    │  │                │  │                    │
└────────────────┘  └────────────────┘  └────────────────────┘
```

---

## NPC Response Template Structure

Each response consists of:

```
{
  "id": "blacksmith_browse_swords_neutral",
  "trigger": {
    "intent": "browse",
    "subject": "swords",
    "tone": "neutral"
  },
  "template": "I've got {sword_count} blades ready. {sword_list_description}. See anything you like?",
  "variables": ["sword_count", "sword_list_description"],
  "voice_variants": {
    "dwarf": "Aye, {sword_count} blades on the rack. {sword_list_description}. Well? Which one speaks to ye?",
    "elf": "I have crafted {sword_count} blades of late. {sword_list_description}. Perhaps one suits your need.",
    "human_gruff": "Got {sword_count} swords. {sword_list_description}. You buying or just looking?"
  },
  "next_state": "browsing"
}
```

---

## Cultural/Race Voice Dimensions

When authoring voice variants, consider:

| Dimension            | Dwarf                | Elf                   | Human (varied)  |
| -------------------- | -------------------- | --------------------- | --------------- |
| Formality            | Low, direct          | High, measured        | Varies by class |
| Vocabulary           | Trade-focused, blunt | Poetic, precise       | Practical       |
| Attitude to haggling | Respects it          | Finds it distasteful  | Expects it      |
| Contract framing     | Oath-bound, honour   | Long-term arrangement | Business deal   |
| Response to rudeness | Matches energy       | Cold withdrawal       | Varies          |

---

## Edge Cases to Handle

| Scenario                                         | System Response                                                      |
| ------------------------------------------------ | -------------------------------------------------------------------- |
| Player tries to buy sword they can't afford      | Blacksmith refuses; optionally hints at contract as way to earn gold |
| Player accepts contract then asks about it again | Blacksmith reminds them of terms, delivery location                  |
| Player is rude repeatedly                        | Disposition drops; may refuse service                                |
| Player asks about contract before it's offered   | Blacksmith doesn't understand / deflects; no spoilers                |
| Player leaves mid-conversation                   | State preserved for return                                           |

---

## Estimated Scale

| Element                       | Count    | Notes                                |
| ----------------------------- | -------- | ------------------------------------ |
| Player word/phrases           | ~40      | Across all categories                |
| Meaningful combinations       | ~150-200 | Many combinations map to same intent |
| Unique NPC response templates | ~30-40   | Before voice variants                |
| Voice variants per template   | 3-5      | Per race/culture                     |
| Total authored responses      | ~120-200 | Manageable for single NPC type       |

---

## Open Questions

1. **Inventory variation:** Does each blacksmith have different swords, or standardised per region/race?
2. **Contract parameters:** Fixed or procedurally varied (quantity, deadline, reward)?
3. **Memory across sessions:** Does the blacksmith remember previous visits beyond relationship level?
4. **Failure states:** What happens if player fails contract deadline?
5. **Hidden options:** Are some word choices unlocked by items, skills, or reputation?

---

## Next Steps

1. Paper prototype this flow with 2-3 testers
2. Identify which responses feel most repetitive
3. Determine minimum viable voice variants for distinctiveness
4. Decide whether contract is unique quest or repeatable

---

## Related Documents

- **[The Wandering Inn: Races & Cultures Reference Guide](twi-races-cultures.md)** — Provides cultural context for Dwarf, Elf, and Human voice variants
- **[NPC Systems Design Document](npc-design.md)** — Defines NPC goal systems, relationship tracking, and memory mechanics
- **[Idle Game Overview](idle-game-overview.md)** — Overview of the seven game scenarios and their interconnected nature
- **[Meta-Game Integration Layer](idle-meta-integration.md)** — Describes shared NPC system across scenarios and recurring characters
- **[Conversation System - Open Questions Log](open-questions-log.md)** — Tracks unresolved questions about conversation systems and NPC dialogue
