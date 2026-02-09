# Minimal Possession Demo v1 - Design Document

> **Created**: 2026-02-09
> **Status**: Draft
> **Target Platform**: Blazor WASM (Web)
> **Goal**: Demonstrate core possession mechanics and living world simulation

---

## Executive Summary

Build a minimal but playable proof-of-concept that demonstrates the core architecture: a living world simulation where players can possess NPCs to experience different roles and see how the world continues autonomously when not controlled.

**What Success Looks Like:**

- Open the web app → see a small settlement with buildings and NPCs
- Watch NPCs work autonomously (innkeeper serves customers, blacksmith forges)
- Click an NPC → possess them → perform actions based on their role/location
- Release possession → NPC continues autonomously with your priority changes persisted
- Switch to different NPC → experience different gameplay loop

**Time to Demo:** User can understand the core concept in 5 minutes of play

---

## Scope Definition

### IN SCOPE (MVP Features)

**World:**

- 1 Settlement: "Millbrook" (fixed, pre-generated)
- 2 Buildings: Inn ("The Rusty Tankard") + Workshop ("Tomas' Forge")
- Simple tile-based layout (10x10 grid, buildings occupy multiple tiles)

**NPCs (Total: 4):**

- Mara: [Innkeeper] Lvl 5, owner of The Rusty Tankard
- Cook: [Cook] Lvl 3, employee at the inn
- Tomas: [Blacksmith] Lvl 8, owner of Tomas' Forge
- Customer: Generic customer NPC (respawns/cycles)

**Core Mechanics:**

- ✅ **Autonomous NPC Behavior**: NPCs work their jobs when not possessed
- ✅ **Possession**: Click NPC portrait → take control
- ✅ **Location-Contextual Actions**: Actions available based on building + role
- ✅ **Action Execution**: Perform one action → see immediate result
- ✅ **Release Control**: NPC resumes autonomous behavior
- ✅ **Favorites System**: Bookmark NPCs for quick access
- ✅ **Time Passage**: Real-time simulation (accelerated: 1 real second = 1 game minute)

**Demonstrable Scenarios:**

- **Inn Scenario**: Possess Mara → Serve customers, manage cook, earn gold
- **Blacksmith Scenario**: Possess Tomas → Craft items, manage workshop

### OUT OF SCOPE (Future Versions)

❌ Multiplayer (single-player only)
❌ Multiple settlements
❌ Settlement building/construction
❌ Character creation/customization
❌ Quest system
❌ Combat
❌ Inventory management (simplified: just show gold)
❌ Skill trees/leveling (NPCs have fixed skills)
❌ Equipment/gear
❌ Save/load (state resets on page refresh)
❌ Mobile-optimized UI (desktop web only)
❌ Advanced NPC AI (simple state machines)

---

## User Flow

### 1. Application Load

```
User opens web app
→ Loading screen: "Generating Millbrook..."
→ Settlement appears on screen
→ 2 buildings visible: Inn (left), Workshop (right)
→ 4 NPCs visible as avatars on the map
→ Top bar: "Observer Mode - Click an NPC to possess"
```

### 2. Observer Mode (Initial State)

**What User Sees:**

- Top-down or isometric view of settlement
- Buildings labeled ("The Rusty Tankard", "Tomas' Forge")
- NPCs moving between locations (walking animations optional, can be instant teleport)
- Activity log (bottom): "Mara served Customer. +5 gold", "Tomas crafted Iron Sword"
- NPC portraits (left sidebar): Mara, Cook, Tomas, Customer

**What User Can Do:**

- Watch NPCs work autonomously
- Click NPC portrait to see their status (name, class, location, current action, gold)
- Click "Possess" button on NPC portrait to take control
- Click "Add to Favorites" (⭐) to bookmark an NPC

### 3. Possessing Mara (Innkeeper)

```
User clicks Mara's portrait → "Possess"
→ View changes: "Possessed: Mara [Innkeeper] Lvl 5"
→ Top bar shows: Current Location: The Rusty Tankard Inn
→ Action panel appears (center): Available Actions
→ NPC status panel (right): Mara's stats, gold, current priorities
```

**Available Actions (Mara at Inn):**

- 🍽️ **Serve Customer** (5 seconds) - Earn 5 gold, increase reputation
- 👨‍🍳 **Manage Cook** - Open priority panel, adjust cook's focus (speed vs quality)
- 📈 **Check Income** - View today's earnings, customer count
- 🚪 **Release Control** - Return to observer mode

**User Experience:**

```
Click "Serve Customer"
→ Action timer starts: "Serving... 5s remaining"
→ Timer counts down
→ Action completes: "Customer satisfied! +5 gold, +2 reputation"
→ Gold counter updates: 100 → 105
→ Activity log: "You (as Mara) served a customer"
→ Can perform another action or release
```

### 4. Adjusting NPC Priorities

```
Click "Manage Cook"
→ Priority panel opens
→ Slider: "Speed ←→ Quality"
→ Current: 50/50 balanced
→ Adjust to 80% Quality
→ Click "Apply"
→ Confirmation: "Cook priorities updated. Changes persist after release."
→ Close panel
```

### 5. Releasing Possession

```
Click "Release Control"
→ View returns to Observer Mode
→ Mara continues working autonomously
→ Activity log shows: "Mara served Customer" (autonomous action)
→ Cook now prioritizes quality over speed (your change persisted)
```

### 6. Possessing Tomas (Blacksmith)

```
Click Tomas' portrait → "Possess"
→ View changes: "Possessed: Tomas [Blacksmith] Lvl 8"
→ Current Location: Tomas' Forge
→ Different action panel appears
```

**Available Actions (Tomas at Workshop):**

- 🔨 **Craft Iron Sword** (30 seconds) - Consume 2 Iron Ore, produce Iron Sword worth 20 gold
- ⚙️ **Check Materials** - View available resources
- 🎯 **Set Priority** - Focus on orders vs experimentation
- 🚪 **Release Control** - Return to observer mode

**User Experience:**

```
Click "Craft Iron Sword"
→ Check: Do you have 2 Iron Ore? (Yes, starting materials)
→ Timer: "Forging... 30s remaining"
→ Timer counts down (can release during crafting, work continues)
→ Completion: "Iron Sword crafted! Added to inventory. Worth 20 gold."
→ Can sell to auto-generated customer or keep
```

### 7. Context Awareness Demo

```
While possessing Tomas, click "Walk to Inn"
→ Tomas moves from Workshop to Inn
→ Action panel updates: "No blacksmith actions available here"
→ New action: 🍺 "Order Drink" (customer action, not blacksmith action)
→ Demonstrates context-dependent actions
```

### 8. Favorites System

```
Star (⭐) Mara and Tomas
→ Favorites panel appears in sidebar (collapsed by default)
→ Shows: ⭐ Mara, ⭐ Tomas
→ Click favorite → instant possession
→ Notification badge: "Mara's inn earned 50 gold while you were away"
```

---

## Technical Architecture

### Data Model

**Settlement:**

```csharp
public class Settlement
{
    public string Id { get; set; } = "millbrook";
    public string Name { get; set; } = "Millbrook";
    public List<Building> Buildings { get; set; }
    public List<NPC> NPCs { get; set; }
    public DateTime WorldTime { get; set; }
}
```

**Building:**

```csharp
public class Building
{
    public string Id { get; set; }
    public string Name { get; set; }
    public BuildingType Type { get; set; } // Inn, Workshop
    public GridPosition Position { get; set; }
    public int Width { get; set; }
    public int Height { get; set; }
    public Dictionary<string, int> Resources { get; set; } // e.g., IronOre: 10
}

public enum BuildingType { Inn, Workshop, Residential, Market }
```

**NPC:**

```csharp
public class NPC
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string ClassName { get; set; } // "Innkeeper", "Blacksmith", "Cook"
    public int Level { get; set; }
    public GridPosition Position { get; set; }
    public string CurrentBuilding { get; set; } // Building ID
    public NPCState State { get; set; } // Working, Idle, Traveling
    public string CurrentAction { get; set; } // "Serving Customer", "Crafting Sword"
    public int Gold { get; set; }
    public int Reputation { get; set; }
    public Dictionary<string, object> Priorities { get; set; } // e.g., "Quality": 80
    public bool IsPossessed { get; set; }
    public DateTime ActionStartTime { get; set; }
    public int ActionDurationSeconds { get; set; }
}

public enum NPCState { Idle, Working, Traveling, Resting }
```

**Action:**

```csharp
public class NPCAction
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int DurationSeconds { get; set; }
    public Dictionary<string, int> ResourceCosts { get; set; } // IronOre: 2
    public Dictionary<string, int> Rewards { get; set; } // Gold: 20
    public List<string> RequiredClasses { get; set; } // ["Blacksmith"]
    public List<string> RequiredBuildings { get; set; } // ["Workshop"]
}
```

### Core Systems

**1. Game Loop (Simulation Engine):**

```csharp
public class SimulationEngine
{
    private Timer _gameTimer;
    private Settlement _settlement;

    public void Start()
    {
        // Run every 100ms (10 ticks per second)
        _gameTimer = new Timer(100);
        _gameTimer.Elapsed += OnTick;
        _gameTimer.Start();
    }

    private void OnTick(object sender, ElapsedEventArgs e)
    {
        // Advance world time (1 real second = 1 game minute)
        _settlement.WorldTime = _settlement.WorldTime.AddMinutes(1);

        // Update all NPCs
        foreach (var npc in _settlement.NPCs)
        {
            if (!npc.IsPossessed)
            {
                UpdateAutonomousNPC(npc);
            }
            else
            {
                UpdatePossessedNPC(npc);
            }
        }

        // Trigger UI update
        OnStateChanged?.Invoke();
    }
}
```

**2. NPC AI (Autonomous Behavior):**

```csharp
private void UpdateAutonomousNPC(NPC npc)
{
    // Check if current action is complete
    if (npc.State == NPCState.Working)
    {
        var elapsed = DateTime.Now - npc.ActionStartTime;
        if (elapsed.TotalSeconds >= npc.ActionDurationSeconds)
        {
            CompleteAction(npc);
            npc.State = NPCState.Idle;
        }
        return; // Still working, don't start new action
    }

    // NPC is idle, choose next action based on class and location
    if (npc.State == NPCState.Idle)
    {
        var action = ChooseNextAction(npc);
        if (action != null)
        {
            StartAction(npc, action);
        }
    }
}

private NPCAction ChooseNextAction(NPC npc)
{
    // Simple priority-based selection
    if (npc.ClassName == "Innkeeper")
    {
        // Check if customers are waiting
        if (HasWaitingCustomers(_settlement))
            return GetAction("ServeCustomer");
        else
            return GetAction("CheckIncome"); // Idle action
    }
    else if (npc.ClassName == "Blacksmith")
    {
        // Check if has materials
        if (HasMaterials(npc, "IronOre", 2))
            return GetAction("CraftIronSword");
        else
            return GetAction("Rest"); // Idle
    }
    return null;
}
```

**3. Possession System:**

```csharp
public class PossessionManager
{
    private NPC _currentlyPossessed;

    public void PossessNPC(NPC npc)
    {
        // Release current possession if any
        if (_currentlyPossessed != null)
        {
            ReleasePossession();
        }

        npc.IsPossessed = true;
        _currentlyPossessed = npc;

        // Pause autonomous behavior
        // Player now controls this NPC
    }

    public void ReleasePossession()
    {
        if (_currentlyPossessed != null)
        {
            _currentlyPossessed.IsPossessed = false;

            // NPC resumes autonomous behavior with updated priorities
            _currentlyPossessed = null;
        }
    }

    public List<NPCAction> GetAvailableActions(NPC npc)
    {
        var actions = new List<NPCAction>();

        // Get actions based on class
        var classActions = ActionDatabase.GetByClass(npc.ClassName);

        // Filter by current building
        var currentBuilding = GetBuilding(npc.CurrentBuilding);
        var contextActions = classActions.Where(a =>
            a.RequiredBuildings.Contains(currentBuilding.Type.ToString())
        );

        return contextActions.ToList();
    }
}
```

**4. Action Execution:**

```csharp
public void ExecuteAction(NPC npc, NPCAction action)
{
    // Validate requirements
    if (!CanExecuteAction(npc, action))
    {
        ShowError($"Cannot execute {action.Name}: requirements not met");
        return;
    }

    // Consume resources
    foreach (var cost in action.ResourceCosts)
    {
        ConsumeResource(npc, cost.Key, cost.Value);
    }

    // Start action
    npc.State = NPCState.Working;
    npc.CurrentAction = action.Name;
    npc.ActionStartTime = DateTime.Now;
    npc.ActionDurationSeconds = action.DurationSeconds;

    // Schedule completion
    Task.Delay(action.DurationSeconds * 1000).ContinueWith(_ =>
    {
        CompleteAction(npc, action);
    });
}

private void CompleteAction(NPC npc, NPCAction action)
{
    // Grant rewards
    foreach (var reward in action.Rewards)
    {
        if (reward.Key == "Gold")
            npc.Gold += reward.Value;
        else if (reward.Key == "Reputation")
            npc.Reputation += reward.Value;
    }

    // Log activity
    LogActivity($"{npc.Name} completed {action.Name}. +{action.Rewards["Gold"]} gold");

    // Return to idle
    npc.State = NPCState.Idle;
    npc.CurrentAction = null;
}
```

### UI Components (Blazor)

**1. SettlementView.razor:**

```razor
<div class="settlement-container">
    <TopBar CurrentMode="@_mode" PossessedNPC="@_possessedNPC" />

    <div class="main-view">
        <NPCSidebar NPCs="@_settlement.NPCs"
                    OnPossess="@PossessNPC"
                    OnToggleFavorite="@ToggleFavorite" />

        <SettlementMap Buildings="@_settlement.Buildings"
                       NPCs="@_settlement.NPCs" />

        @if (_possessedNPC != null)
        {
            <ActionPanel NPC="@_possessedNPC"
                         Actions="@_availableActions"
                         OnExecute="@ExecuteAction"
                         OnRelease="@ReleasePossession" />
        }
    </div>

    <ActivityLog Activities="@_activityLog" />
</div>
```

**2. NPCSidebar.razor:**

```razor
<div class="npc-sidebar">
    <h3>NPCs</h3>
    @foreach (var npc in NPCs)
    {
        <div class="npc-card @(npc.IsPossessed ? "possessed" : "")">
            <div class="npc-header">
                <span class="npc-name">@npc.Name</span>
                <button class="btn-favorite" @onclick="() => OnToggleFavorite(npc)">
                    @(IsFavorite(npc) ? "⭐" : "☆")
                </button>
            </div>
            <div class="npc-class">[@npc.ClassName] Lvl @npc.Level</div>
            <div class="npc-location">📍 @npc.CurrentBuilding</div>
            <div class="npc-action">@npc.CurrentAction</div>
            <div class="npc-gold">💰 @npc.Gold gold</div>

            @if (!npc.IsPossessed)
            {
                <button class="btn-possess" @onclick="() => OnPossess(npc)">
                    Possess
                </button>
            }
        </div>
    }
</div>
```

**3. ActionPanel.razor:**

```razor
<div class="action-panel">
    <h3>Actions - @NPC.Name</h3>
    <p class="location">📍 @NPC.CurrentBuilding</p>

    @if (NPC.State == NPCState.Working)
    {
        <div class="action-in-progress">
            <p>@NPC.CurrentAction</p>
            <progress value="@GetActionProgress()" max="100"></progress>
            <span>@GetRemainingTime()s remaining</span>
        </div>
    }
    else
    {
        <div class="action-list">
            @foreach (var action in Actions)
            {
                <button class="action-btn" @onclick="() => OnExecute(action)">
                    <span class="action-icon">@action.Icon</span>
                    <span class="action-name">@action.Name</span>
                    <span class="action-duration">⏱️ @action.DurationSeconds s</span>
                    @if (action.ResourceCosts.Any())
                    {
                        <span class="action-cost">
                            Costs: @string.Join(", ", action.ResourceCosts.Select(c => $"{c.Value} {c.Key}"))
                        </span>
                    }
                </button>
            }
        </div>
    }

    <button class="btn-release" @onclick="OnRelease">
        Release Control
    </button>
</div>
```

---

## Implementation Phases

### Phase 1: Foundation (Week 1)

- ✅ Create data models (Settlement, Building, NPC, Action)
- ✅ Implement SimulationEngine (game loop)
- ✅ Basic Blazor page with settlement view
- ✅ Display 2 buildings, 4 NPCs on a grid
- ✅ NPCs have fixed positions (no movement yet)

**Acceptance Criteria:**

- Open app → see Millbrook with 2 buildings
- See 4 NPC cards in sidebar with names, classes, locations
- Game loop runs (check console logs for ticks)

### Phase 2: Autonomous Behavior (Week 1)

- ✅ Implement NPC AI state machine
- ✅ Define 2-3 actions per NPC class
- ✅ NPCs automatically perform actions when idle
- ✅ Activity log shows NPC actions

**Acceptance Criteria:**

- Watch Mara automatically serve customers
- Watch Tomas automatically craft swords
- Activity log updates with each action
- Gold increases for NPCs performing actions

### Phase 3: Possession (Week 2)

- ✅ Implement PossessionManager
- ✅ Click NPC → possession modal appears
- ✅ Action panel shows available actions
- ✅ Execute action → see timer and result
- ✅ Release → NPC resumes autonomously

**Acceptance Criteria:**

- Possess Mara → see "Serve Customer" action
- Click action → timer counts down → gold increases
- Release → Mara continues serving autonomously
- Possess Tomas → see "Craft Sword" action → execute → see result

### Phase 4: Context Awareness (Week 2)

- ✅ Implement action filtering by building + class
- ✅ Actions change when NPC moves buildings (manual trigger)
- ✅ Demonstrate: Blacksmith has no actions at Inn

**Acceptance Criteria:**

- While possessing Tomas at Forge, see blacksmith actions
- Move Tomas to Inn (via button)
- Action list updates: no blacksmith actions available
- Customer actions appear instead

### Phase 5: Polish & Favorites (Week 3)

- ✅ Favorites system (star NPCs)
- ✅ Notifications for favorited NPCs
- ✅ Improve UI/UX (animations, icons, layout)
- ✅ Activity log scrolling and filtering

**Acceptance Criteria:**

- Star Mara → she appears in Favorites panel
- Quick-possess from Favorites panel
- Notification badge when Mara completes action while not possessed
- UI feels responsive and polished

---

## Success Metrics

### Technical Metrics

- ✅ Game loop runs at 10 ticks/second without lag
- ✅ Possession switch happens in <100ms
- ✅ Action execution feels responsive (<50ms to start timer)
- ✅ No errors in browser console
- ✅ Page load <2 seconds

### User Experience Metrics

- ✅ User can understand possession mechanic in <1 minute
- ✅ User can possess, execute action, and release in <30 seconds
- ✅ User can see autonomous behavior resuming after release
- ✅ User can experience 2 different "scenarios" (inn vs blacksmith)
- ✅ Demo is interesting enough to watch for 5+ minutes

### Validation Questions (Playtest)

1. Is it clear that you're controlling an NPC (not playing "as yourself")?
2. Does it feel like the NPCs have their own lives when you're not controlling them?
3. Does possessing the innkeeper vs blacksmith feel distinct?
4. Do you understand that your priority changes persist after release?
5. Would you want to possess more NPCs and see more buildings?

---

## Next Steps After MVP

**v1.1 - More Depth:**

- Add 2 more buildings (Market, Temple)
- Add 4 more NPCs (Merchant, Priest, Guard, Farmer)
- Add resource flow (inn needs food from farmer)
- Add simple customer AI (they order, eat, pay, leave)

**v1.2 - Persistence:**

- Save state to browser localStorage
- Load state on page refresh
- Offline progress calculation (simple)

**v1.3 - World Expansion:**

- Add second settlement (travel between)
- Add regional map view
- Add traveling NPCs

**v2.0 - Multiplayer Foundation:**

- Add player identity system
- Separate favorites per player
- Shared world state (backend required)

---

## Open Questions

1. **NPC Movement**: Should NPCs walk between buildings (animation) or instant teleport?
   - **Recommendation**: Instant teleport for MVP, walking in v1.1

2. **Customer NPCs**: Spawn new customers or cycle single customer?
   - **Recommendation**: Single customer that resets after being served (simpler)

3. **Time Scale**: 1 real second = 1 game minute feels right?
   - **Recommendation**: Yes, makes actions feel snappy (30s = 30 game minutes)

4. **Notifications**: How intrusive should they be?
   - **Recommendation**: Badge on favorite NPC card, no popups

5. **Mobile Support**: Worth optimizing for mobile in MVP?
   - **Recommendation**: No, desktop web only for MVP

---

## Conclusion

This Minimal Possession Demo validates the core architecture documented in `core-architecture.md`. It proves that:

- A unified simulation can run continuously
- Possession-based control works for gameplay
- Different roles create distinct experiences
- Autonomous NPCs maintain world continuity
- The concept is fun and understandable

**Success = User says:** "I want to see more buildings, more NPCs, and more actions to perform!"

**Next Document**: Implementation Plan (tasks breakdown for development)
