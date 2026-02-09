# Feature Specification: Minimal Possession Demo v1

**Feature Branch**: `001-minimal-possession-demo`
**Created**: 2026-02-09
**Status**: Draft
**Input**: Demonstration of core possession mechanics in a living world simulation

## User Scenarios & Testing _(mandatory)_

### User Story 1 - Observer Mode (Priority: P1)

Experience the living world simulation running autonomously before any player interaction.

**Why this priority**: Core concept validation - users must see that NPCs have their own lives and the world runs without player input.

**Independent Test**: Open the web app, watch for 30 seconds without clicking anything. NPCs perform actions, gold increases, activity log updates. World is alive.

**Acceptance Scenarios**:

1. **Given** I open the web app, **When** the settlement loads, **Then** I see 2 buildings (Inn, Workshop) and 4 NPCs on a 10x10 grid
2. **Given** I watch in observer mode, **When** 10 seconds pass, **Then** activity log shows at least 2 NPC actions ("Mara served Customer", "Tomas crafted Iron Sword")
3. **Given** I'm observing Mara, **When** she completes serving a customer, **Then** her gold counter increases by 5

---

### User Story 2 - Possess and Control NPC (Priority: P1)

Take direct control of an NPC to experience their role and perform actions manually.

**Why this priority**: Core mechanic - possession is the fundamental interaction model.

**Independent Test**: Click Mara's portrait → click "Possess" → see action panel with inn-specific actions → execute "Serve Customer" → watch timer count down → see result (+5 gold).

**Acceptance Scenarios**:

1. **Given** I click Mara's portrait in sidebar, **When** I click "Possess", **Then** view changes to "Possessed: Mara [Innkeeper] Lvl 5" and action panel appears
2. **Given** I'm possessing Mara at the Inn, **When** I view available actions, **Then** I see "Serve Customer", "Manage Cook", "Check Income", "Release Control"
3. **Given** I click "Serve Customer", **When** the 5-second timer completes, **Then** I see "+5 gold, +2 reputation" and Mara's gold increases from 100 to 105
4. **Given** I click "Release Control", **When** I return to observer mode, **Then** Mara continues serving customers autonomously

---

### User Story 3 - Context-Aware Actions (Priority: P2)

Experience how available actions change based on NPC class and current building location.

**Why this priority**: Demonstrates the "scenarios as emergent gameplay" concept - same world, different role = different experience.

**Independent Test**: Possess Mara at Inn (see innkeeper actions) → release → possess Tomas at Workshop (see blacksmith actions) → understand context dependency.

**Acceptance Scenarios**:

1. **Given** I possess Tomas at Tomas' Forge, **When** I view available actions, **Then** I see "Craft Iron Sword", "Check Materials", "Set Priority", "Release Control"
2. **Given** I possess Tomas, **When** I click "Craft Iron Sword", **Then** timer starts at 30 seconds and completes with "Iron Sword crafted! Worth 20 gold"
3. **Given** I possess blacksmith Tomas at the Inn, **When** I view available actions, **Then** blacksmith actions are NOT available (only customer actions like "Order Drink")

---

### User Story 4 - Persistent Priority Changes (Priority: P2)

Adjust NPC priorities while possessed and confirm changes persist after releasing control.

**Why this priority**: Demonstrates lasting influence - you're guiding NPCs, not just puppeting them temporarily.

**Independent Test**: Possess Mara → click "Manage Cook" → adjust priority slider to 80% Quality → release → observe Cook's behavior follows new priority.

**Acceptance Scenarios**:

1. **Given** I possess Mara, **When** I click "Manage Cook", **Then** priority panel opens showing current balance (50/50 Speed vs Quality)
2. **Given** I adjust Cook's priority to 80% Quality, **When** I click "Apply", **Then** I see confirmation "Cook priorities updated. Changes persist after release."
3. **Given** I release possession of Mara after changing Cook's priority, **When** I observe Cook working, **Then** Cook's actions reflect the new priority (takes longer but produces higher quality)

---

### User Story 5 - Favorites System (Priority: P3)

Bookmark frequently-used NPCs for quick access and receive notifications about their activities.

**Why this priority**: Quality of life feature that demonstrates multi-NPC management pattern.

**Independent Test**: Click star icon on Mara → she appears in Favorites panel → quick-possess from favorites → notification badge appears when she completes action while not possessed.

**Acceptance Scenarios**:

1. **Given** I click the star icon (⭐) on Mara's portrait, **When** she's added to favorites, **Then** Favorites panel shows "⭐ Mara"
2. **Given** Mara is favorited, **When** I click her name in Favorites panel, **Then** I immediately possess her (skips portrait click step)
3. **Given** Mara is favorited and not possessed, **When** she completes an action, **Then** notification badge appears on her Favorites entry

---

### Edge Cases

- What happens when **NPC is already working** and you try to start another action? → Action button disabled until current action completes
- What happens when **no materials available** for crafting? → "Craft Iron Sword" shows red with tooltip "Insufficient materials: need 2 Iron Ore"
- What happens when **you possess NPC mid-action**? → Timer continues, you see progress bar with remaining time
- What happens when **you release possession mid-action**? → Action continues autonomously, completes as scheduled
- What happens when **multiple NPCs need the same resource simultaneously**? → First-come-first-served (for MVP, no resource contention)

## Requirements _(mandatory)_

### Functional Requirements

- **FR-001**: System MUST display a 10x10 tile grid with 2 buildings (Inn, Workshop) and 4 NPCs
- **FR-002**: System MUST run game loop at 10 ticks/second advancing simulation time
- **FR-003**: NPCs MUST autonomously select and perform actions when not possessed
- **FR-004**: System MUST allow player to possess any NPC by clicking portrait + "Possess" button
- **FR-005**: System MUST display context-appropriate actions based on NPC class and current building
- **FR-006**: Actions MUST have execution timers (5-30 seconds) with visible countdown
- **FR-007**: Action completion MUST grant rewards (gold, reputation) and update UI
- **FR-008**: System MUST allow releasing possession, returning to observer mode
- **FR-009**: NPC priority changes MUST persist after possession release
- **FR-010**: System MUST maintain activity log showing last 20 NPC actions
- **FR-011**: System MUST support favorites list for bookmarking NPCs
- **FR-012**: System MUST show notification badges for favorited NPC activity

### Key Entities _(include if feature involves data)_

- **Settlement**: Represents Millbrook with buildings and NPCs, tracks world time
- **Building**: Physical structure (Inn, Workshop) with position, type, and resources
- **NPC**: Character with name, class, level, position, state, gold, reputation, priorities
- **NPCAction**: Executable action with duration, resource costs, rewards, class/building requirements
- **ActivityLogEntry**: Record of NPC action with timestamp, actor, action type, result

## Success Criteria _(mandatory)_

### Measurable Outcomes

- **SC-001**: User can identify that NPCs work autonomously within 30 seconds of observing
- **SC-002**: User can successfully possess an NPC and execute one action within 60 seconds
- **SC-003**: User can identify that innkeeper vs blacksmith actions feel distinct within 2 minutes
- **SC-004**: 100% of playtesters understand "you control NPCs, not yourself" concept after one possession
- **SC-005**: Game loop maintains 10 ticks/second without visible lag (60 FPS UI updates)
- **SC-006**: Action execution feels responsive (<50ms from click to timer start)
- **SC-007**: User finds demo "interesting enough to watch for 5+ minutes" (playtesting metric)
