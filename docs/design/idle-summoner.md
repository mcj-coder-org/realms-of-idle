# Summoner's Contract - Idle Game Design Document

## Core Fantasy
You are a [Summoner] who forms pacts with otherworldly beings—demons, spirits, elementals, and celestials. Your summons fight, gather, and serve while you manage contracts, negotiate with powerful entities, and grow your supernatural network. This scenario emphasizes collection mechanics, strategic deployment, and the give-and-take relationship between summoner and summoned.

---

## Primary Game Loop

```
SUMMON → CONTRACT → DEPLOY → MAINTAIN → EVOLVE → SUMMON (stronger)
```

1. **Summon Entities** (active ritual with idle preparation)
   - Gather ritual components
   - Perform summoning (time-based)
   - Entity appears based on ritual quality and offerings

2. **Negotiate Contract** (active decision)
   - Entity proposes terms
   - Negotiate duration, services, payment
   - Accept, counter-offer, or dismiss

3. **Deploy for Tasks** (semi-idle)
   - Assign summons to activities
   - Combat, gathering, protection, research
   - Summons work autonomously

4. **Maintain Relationships** (ongoing upkeep)
   - Pay contract costs (mana, offerings, services)
   - Build trust for better terms
   - Risk: Displeased summons may rebel

5. **Evolve & Ascend** (milestone progression)
   - Strong bonds allow evolution
   - Summons grow in power
   - Unlock higher-tier entities through reputation

---

## Resource Systems

### Primary Resources
| Resource | Generation | Primary Use |
|----------|------------|-------------|
| **Mana** | Regeneration, ley lines | Summoning, contract upkeep |
| **Soul Fragments** | Defeated enemies, offerings | Advanced summons, evolution |
| **Offerings** | Crafted, purchased, gathered | Contract payments |
| **Influence** | Summon accomplishments | Unlock higher entities |

### Contract Resources
- **Trust**: Relationship quality with each summon
- **Binding Strength**: How secure the contract is
- **Contract Duration**: Time remaining before renegotiation
- **Debt**: What you owe the summon (if applicable)

---

## Summoning System

### Summoning Process
1. **Prepare Circle**: Choose ritual circle type (affects entity tier)
2. **Gather Components**: Specific items for specific entities
3. **Offer Payment**: Upfront cost influences what answers
4. **Perform Ritual**: Time-based with optional active minigame
5. **Entity Manifests**: Random within parameters, or targeted summon

### Entity Categories
| Category | Examples | Strengths | Typical Cost |
|----------|----------|-----------|--------------|
| **Elementals** | Fire Spirit, Water Sprite, Earth Golem | Combat, environment tasks | Mana |
| **Spirits** | Ghost, Ancestor, Nature Spirit | Scouting, knowledge, subtle tasks | Offerings |
| **Demons** | Imp, Fiend, Arch-Demon | Combat, dark tasks, power | Soul Fragments, Debt |
| **Celestials** | Angel, Valkyrie, Seraph | Healing, protection, holy tasks | Virtue, Restrictions |
| **Fae** | Pixie, Brownie, Sidhe | Trickery, crafting, nature | Favors, Promises |
| **Constructs** | Homunculus, Golem, Automaton | Labor, combat, no upkeep | Creation cost |

### Entity Rarity
- **Common**: Easy to summon, moderate power
- **Uncommon**: Specific components, solid abilities
- **Rare**: Difficult rituals, strong capabilities
- **Epic**: Named entities, unique powers
- **Legendary**: World-shaking beings, extreme costs

---

## Contract System

### Contract Terms
| Term | Meaning | Examples |
|------|---------|----------|
| **Duration** | How long bound | 1 day, 1 week, permanent |
| **Services** | What they'll do | Combat, labor, protection |
| **Restrictions** | What they won't do | Won't harm innocents, won't work Sundays |
| **Payment** | Ongoing cost | Mana/day, weekly offering, monthly favor |
| **Clauses** | Special conditions | Bonus for exceeding tasks, penalty for failure |

### Negotiation
Initial summon proposes terms based on:
- Entity type and personality
- Your reputation with their kind
- Quality of summoning ritual
- Initial offering

You can:
- **Accept**: Take terms as offered
- **Counter**: Propose alternatives (risk of rejection)
- **Dismiss**: Send back, lose summoning costs
- **Bind Forcefully**: Impose terms (damages trust, risks rebellion)

### Trust Mechanics
Trust builds through:
- Fair treatment
- Completing promises
- Generous offerings
- Using summon's preferred tasks

Trust decays through:
- Forced binding
- Broken promises
- Inadequate payment
- Dangerous/degrading tasks

### Trust Levels
| Level | Effects |
|-------|---------|
| Hostile | May rebel, sabotage |
| Wary | Minimum effort, seeks loopholes |
| Neutral | Performs as contracted |
| Friendly | Bonus effort, flexible |
| Devoted | Exceptional service, evolution possible |

---

## Deployment System

### Task Types
| Task | Duration | Risk | Output |
|------|----------|------|--------|
| **Combat (Auto)** | Ongoing | Medium | XP, loot |
| **Dungeon Delve** | Hours | High | Treasure, components |
| **Gathering** | Hours | Low | Resources |
| **Protection** | Ongoing | Variable | Safety |
| **Research** | Hours | None | Knowledge |
| **Labor** | Ongoing | None | Production |
| **Infiltration** | Hours | High | Information, theft |

### Deployment Strategy
- Match entity strengths to tasks
- Consider entity preferences (affects trust)
- Balance risk vs. reward
- Manage fatigue and recovery

### Autonomous Combat
Summons can be set to:
- Guard location (engage threats)
- Hunt in area (seek enemies)
- Arena battles (scheduled combat)
- Dungeon expedition (extended mission)

Combat resolves based on summon stats vs. enemy strength, with trust and equipment modifiers.

---

## Evolution System

### Evolution Requirements
- Maximum trust level (Devoted)
- Entity at level cap
- Specific evolution materials
- Completed evolution quest/trial

### Evolution Paths
Example: **Fire Spirit**
```
Fire Spirit (Common)
├─→ Fire Elemental (Uncommon) [Power path]
│   └─→ Inferno Lord (Rare)
├─→ Ember Guardian (Uncommon) [Defense path]
│   └─→ Flame Sentinel (Rare)
└─→ Cinder Witch (Uncommon) [Utility path]
    └─→ Ash Phoenix (Rare)
```

### Evolution Benefits
- Increased stats
- New abilities
- Improved efficiency
- Reduced upkeep cost
- Extended contract options

---

## Prestige System: "True Name Mastery"

### Trigger Conditions
- Earn the True Name of an Epic+ entity
- Form permanent bond with a Legendary being
- Complete the "Planar Concordat" questline

### Reset Mechanics
- All contracts end (entities return home)
- Ritual circles reset to basic
- Retain: Summoner skills, True Names collected, ritual knowledge
- Gain: "Planar Authority" points

### Legacy Bonuses
- "Remembered Pacts" - Previously summoned entities easier to call
- "Otherworldly Reputation" - Better initial contract offers
- "Efficient Binding" - Reduced upkeep costs
- "Planar Attunement" - Higher tier rituals available earlier

---

## Active vs. Idle Balance

### Idle Mechanics
- Deployed summons continue tasks
- Mana regenerates
- Contracts tick down
- Auto-payment if resources available
- Basic combat resolves automatically

### Active Engagement Rewards
- **Ritual Minigame**: Better results from active summoning
- **Negotiation**: Manual negotiation beats auto-accept
- **Combat Direction**: Tactical control improves outcomes
- **Trust Building**: Personal interaction accelerates trust
- **Crisis Management**: Handle rebellions, contract disputes
- **Evolution Trials**: Active participation required

### Warning Systems
- Contract expiration approaching
- Trust dropping dangerously
- Summon in danger
- Payment due
- Rebellion brewing

---

## Risk & Consequence

### Rebellion
Low trust + poor treatment = rebellion risk
- Mild: Summon leaves, contract void
- Moderate: Summon sabotages before leaving
- Severe: Summon attacks, must be defeated or appeased

### Contract Breaches
If you breach terms:
- Reputation damage with entity type
- Possible curse or retaliation
- Harder future negotiations

### Entity Death
Summons can be killed:
- Entity returns home (cannot resummon for period)
- Permanent bond breaks
- Some entities permanently destroyed

---

## Event System

### Regular Events
- **Planar Alignment**: Specific entity types easier to summon
- **Spirit Festival**: Offerings more effective
- **Demon Market**: Trade summons and contracts

### Story Events
- A demon offers immense power for your soul—negotiate or refuse
- Angels investigate your activities—prove your intentions
- Fae court summons you to answer for a slight
- Ancient evil requires alliance of summons to defeat

### Seasonal Events
- **Veil Thinning (Autumn)**: All summons stronger, easier rituals
- **Celestial Convergence (Summer)**: Angels and celestials available
- **Demon's Night (Winter)**: Dark entities offer special contracts
- **Fae Crossing (Spring)**: Fae bargains and mischief

---

## Synergies with Other Scenarios

- **Adventurer Guild**: Summons join adventurer parties; guild quests provide components
- **Territory**: Summons defend territory; ley lines in territory boost mana
- **Alchemy**: Potions strengthen summons; summons gather rare ingredients
- **Monster Farm**: Summoned creatures can breed; farm provides offerings
- **Inn/Tavern**: Unusual guests include beings from other planes

---

## Sample Play Session

**Morning Check (5 min)**
- Mana regenerated overnight: 500/500
- Contract status: 2 expiring soon, 1 payment due
- Make offering payment (100 soul fragments)
- Collect deployed summon results: 2,000 gold, 50 components

**Midday Summoning (10 min)**
- Prepare ritual for new combat summon
- Gather components from storage
- Perform ritual (active minigame for bonus)
- Fire Elemental manifests—negotiate contract
- Accept terms: 10 mana/day, combat service, 30-day duration

**Evening Management (10 min)**
- Deploy new elemental to dungeon delve
- Check trust levels: One demon at "Wary"—make special offering
- Renegotiate expiring contract: Counter-offer accepted
- Evolution check: Water Spirit ready—begin evolution trial

**Evolution Trial (5 min active)**
- Navigate elemental plane challenges
- Make choices affecting evolution path
- Success: Water Spirit → Ocean Guardian

**Overnight (idle)**
- Summons continue tasks
- Mana regenerates
- Dungeon delve progresses
- Trust slowly builds from fair treatment
