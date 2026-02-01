# Automated Testing Strategy
## Test-First Development for Game Features

---

## 1. Testing Philosophy

### Core Principles

```
EVERY GAME FEATURE IS A TESTABLE SPECIFICATION
━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━

Game Design Document → Gherkin Feature Files → Automated Tests → Implementation

The GDD IS the test specification.
Tests are written BEFORE code.
If it's not tested, it doesn't exist.
```

### What Makes This Possible

| Enabler | How It Helps Testing |
|---------|---------------------|
| **Shared Game Core** | Same logic runs in tests as in production |
| **Deterministic RNG** | Reproducible outcomes, no flaky tests |
| **Event Sourcing** | Any state can be reconstructed and verified |
| **Pure Domain Logic** | No external dependencies in core calculations |
| **Unified NPC/Player Systems** | Test once, applies to both |

---

## 2. Test Layer Architecture

```
┌─────────────────────────────────────────────────────────────────────────┐
│                        GAME DESIGN DOCUMENTS                            │
│            (Source of truth for game behavior)                          │
└─────────────────────────────────────────────────────────────────────────┘
                                    │
                                    ▼
┌─────────────────────────────────────────────────────────────────────────┐
│                     FEATURE SPECIFICATIONS                              │
│                    (Reqnroll .feature files)                            │
├─────────────────────────────────────────────────────────────────────────┤
│  Features/                                                              │
│  ├── ClassSystem/                                                       │
│  │   ├── ClassAcquisition.feature                                      │
│  │   ├── ClassEvolution.feature                                        │
│  │   └── MultiClassing.feature                                         │
│  ├── SkillSystem/                                                       │
│  ├── TagSystem/                                                         │
│  ├── NpcBehavior/                                                       │
│  ├── Combat/                                                            │
│  ├── Crafting/                                                          │
│  └── Economy/                                                           │
└─────────────────────────────────────────────────────────────────────────┘
                                    │
                    ┌───────────────┼───────────────┐
                    ▼               ▼               ▼
            ┌─────────────┐ ┌─────────────┐ ┌─────────────┐
            │    Unit     │ │ Integration │ │    E2E      │
            │   Tests     │ │    Tests    │ │   Tests     │
            └─────────────┘ └─────────────┘ └─────────────┘
                    │               │               │
                    ▼               ▼               ▼
            ┌─────────────┐ ┌─────────────┐ ┌─────────────┐
            │   Domain    │ │   Grains    │ │  Full UI    │
            │   Logic     │ │  Database   │ │   Flows     │
            │   Only      │ │    API      │ │             │
            └─────────────┘ └─────────────┘ └─────────────┘
```

---

## 3. BDD Feature Specifications

### 3.1 Feature File Structure

Each game system has a corresponding feature directory:

```
tests/IdleWorlds.Core.Tests/Features/
├── ClassSystem/
│   ├── ClassAcquisition.feature      # Earning first class
│   ├── ClassEvolution.feature        # Level 10/20/30 evolutions
│   ├── MultiClassing.feature         # Multiple class handling
│   └── RedClasses.feature            # Criminal class acquisition
├── SkillSystem/
│   ├── SkillAcquisition.feature      # Gaining skills on level up
│   ├── SkillFiltering.feature        # Tag-based skill eligibility
│   ├── SignatureSkills.feature       # Unique milestone skills
│   └── SkillEffects.feature          # Skill activation and effects
├── TagSystem/
│   ├── TagGeneration.feature         # Actions generating tags
│   ├── TagDecay.feature              # Weekly decay mechanics
│   └── TagRequirements.feature       # Threshold checking
├── Actions/
│   ├── Strike.feature                # Combat action
│   ├── Gather.feature                # Resource collection
│   ├── Craft.feature                 # Item creation
│   ├── Cast.feature                  # Magic usage
│   └── Rest.feature                  # Recovery and level-up
├── NpcBehavior/
│   ├── GoalGeneration.feature        # NPC goal creation
│   ├── GoalEvaluation.feature        # Progress and completion
│   ├── DailyLoopGeneration.feature   # Schedule creation
│   ├── ActionSelection.feature       # Decision making
│   ├── ArchetypeShift.feature        # Maintainer → Hero/Villain
│   └── MaintainerBehavior.feature    # World stability actions
├── WorldEvents/
│   ├── EventTriggering.feature       # When events fire
│   ├── EventEffects.feature          # World state changes
│   └── PlayerParticipation.feature   # Player interaction
├── Economy/
│   ├── Trading.feature               # Buy/sell mechanics
│   ├── PriceCalculation.feature      # Supply/demand
│   └── ResourceFlow.feature          # Production/consumption
├── Progression/
│   ├── XpCalculation.feature         # Experience formulas
│   ├── LevelUp.feature               # Level advancement
│   └── OfflineProgress.feature       # Idle calculations
└── Sync/
    ├── SoloToMultiplayer.feature     # Upload local save
    ├── ConflictResolution.feature    # Merge conflicts
    └── EventVerification.feature     # Deterministic checks
```

### 3.2 Feature File Examples

#### Class Acquisition Feature
```gherkin
# Features/ClassSystem/ClassAcquisition.feature

Feature: Class Acquisition
  As a player
  I want to earn classes through my actions
  So that my character develops based on how I play

  Background:
    Given a new player "Hero" exists
    And the player has no classes
    And the world has standard class definitions

  # ============================================
  # BASIC CLASS ACQUISITION
  # ============================================

  Scenario: Acquire Warrior class through melee combat
    Given the player has the following tag affinities:
      | Tag          | Value |
      | combat.melee | 50    |
    When the player performs 50 "Strike" actions with weapon "sword"
    Then the player's tag affinity for "combat.melee" should be at least 100
    When the player performs a "Rest" action
    Then the player should have class "[Warrior]" at level 1
    And the player should have received a skill with tags containing "combat"

  Scenario: Acquire Herbalist class through gathering
    When the player performs the following actions:
      | Action | Count | Location       | Tool   |
      | Gather | 60    | Moonpetal Grove| sickle |
      | Gather | 40    | Forest Edge    | sickle |
    And the player performs a "Rest" action
    Then the player should have class "[Herbalist]" at level 1
    And the player's tag affinity for "gather.herbalism" should be at least 100

  Scenario: Acquire Blacksmith class through crafting
    Given the player has the following inventory:
      | Item       | Quantity |
      | Iron Ore   | 200      |
      | Coal       | 100      |
    When the player performs 100 "Craft" actions with recipe "Iron Ingot"
    And the player performs a "Rest" action
    Then the player should have class "[Blacksmith]" at level 1

  # ============================================
  # TAG REQUIREMENTS
  # ============================================

  Scenario: Class not acquired when tag threshold not met
    Given the player has the following tag affinities:
      | Tag          | Value |
      | combat.melee | 90    |
    When the player performs a "Rest" action
    Then the player should not have any classes

  Scenario: Supporting tags accelerate acquisition
    Given the player has the following tag affinities:
      | Tag              | Value |
      | combat.melee     | 80    |
      | combat.defense   | 50    |
      | combat.tactical  | 25    |
    When the player performs 20 "Strike" actions with weapon "sword"
    And the player performs a "Rest" action
    Then the player should have class "[Warrior]" at level 1

  # ============================================
  # MULTIPLE CLASS ELIGIBILITY
  # ============================================

  Scenario: Player earns most qualified class when multiple eligible
    Given the player has the following tag affinities:
      | Tag              | Value |
      | combat.melee     | 100   |
      | gather.hunting   | 150   |
      | combat.ranged    | 80    |
    When the player performs a "Rest" action
    Then the player should have class "[Hunter]" at level 1
    And the player should not have class "[Warrior]"

  # ============================================
  # CLASS ACQUISITION TIMING
  # ============================================

  Scenario: Class is only granted during rest
    Given the player has the following tag affinities:
      | Tag          | Value |
      | combat.melee | 150   |
    Then the player should not have any classes
    When the player performs a "Rest" action
    Then the player should have class "[Warrior]" at level 1

  Scenario Outline: Various classes acquired through appropriate actions
    When the player performs <count> "<action>" actions with <modifier>
    And the player performs a "Rest" action
    Then the player should have class "<class>" at level 1

    Examples:
      | action | count | modifier                  | class        |
      | Strike | 100   | weapon "bow"              | [Archer]     |
      | Gather | 100   | location "Iron Mine"      | [Miner]      |
      | Craft  | 100   | recipe "Health Potion"    | [Alchemist]  |
      | Cast   | 100   | spell "Fireball"          | [Mage]       |
      | Trade  | 100   | transaction "sell"        | [Merchant]   |
      | Rest   | 50    | location "The Lucky Inn"  | [Innkeeper]  |
```

#### NPC Goal System Feature
```gherkin
# Features/NpcBehavior/GoalGeneration.feature

Feature: NPC Goal Generation
  As an NPC in the world
  I want to have meaningful goals
  So that I can contribute to a living world

  Background:
    Given the world is initialized with standard zones
    And the economy is in equilibrium

  # ============================================
  # MAINTAINER GOAL GENERATION
  # ============================================

  Scenario: Maintainer NPC generates survival goals when resources low
    Given an NPC "Tomas" exists with:
      | Property    | Value           |
      | Archetype   | Maintainer      |
      | Class       | [Blacksmith]    |
      | Gold        | 10              |
      | Satisfaction| 0.5             |
    When the NPC evaluates goals
    Then the NPC should have a goal of type "survival"
    And the goal should have priority greater than 0.8

  Scenario: Maintainer NPC generates economic goals when stable
    Given an NPC "Tomas" exists with:
      | Property    | Value           |
      | Archetype   | Maintainer      |
      | Class       | [Blacksmith]    |
      | Gold        | 500             |
      | Satisfaction| 0.7             |
    When the NPC evaluates goals
    Then the NPC should have a goal of type "economic"
    And the NPC should have a goal of type "skill_acquisition"

  Scenario: Maintainer NPC generates protection goals when threatened
    Given an NPC "Tomas" exists with archetype "Maintainer"
    And the NPC has family members in the world
    And the zone "Millbrook" has world health metric "crime_rate" at 0.4
    When the NPC evaluates goals
    Then the NPC should have a goal of type "protection"

  # ============================================
  # HERO GOAL GENERATION
  # ============================================

  Scenario: Hero NPC generates exploration goals
    Given an NPC "Elena" exists with:
      | Property    | Value           |
      | Archetype   | Hero            |
      | Subtype     | Adventurer      |
      | Class       | [Scout]         |
      | Level       | 25              |
    When the NPC evaluates goals
    Then the NPC should have a goal of type "exploration"
    And the goal should have scope "regional" or greater

  Scenario: Hero NPC responds to world threats
    Given an NPC "Sir Roland" exists with:
      | Property    | Value           |
      | Archetype   | Hero            |
      | Subtype     | Crusader        |
      | Class       | [Knight]        |
    And a world event "Monster Surge" is active in zone "Northern Forest"
    When the NPC evaluates goals
    Then the NPC should have a goal of type "protection"
    And the goal should reference the world event

  # ============================================
  # VILLAIN GOAL GENERATION
  # ============================================

  Scenario: Villain NPC generates domination goals
    Given an NPC "Lord Varen" exists with:
      | Property    | Value           |
      | Archetype   | Villain         |
      | Subtype     | Tyrant          |
      | Class       | [Lord]          |
    When the NPC evaluates goals
    Then the NPC should have a goal of type "domination"
    And the NPC should have a goal of type "destruction" or "acquisition"

  Scenario: Villain goals create observable effects
    Given an NPC "Serena" exists with:
      | Property    | Value           |
      | Archetype   | Villain         |
      | Subtype     | Crime Lord      |
    And the NPC has an active goal "Control grain supply"
    When the NPC progresses the goal by 50%
    Then the zone "Market District" should have economic effect "grain_price_increase"

  # ============================================
  # GOAL HIERARCHY
  # ============================================

  Scenario: Long-term goals decompose into short-term goals
    Given an NPC "Elena" exists with archetype "Maintainer"
    And the NPC has a long-term goal "Learn enchanting"
    When the NPC decomposes goals
    Then the NPC should have a medium-term goal "Find enchanting teacher"
    And the NPC should have a short-term goal related to the medium-term goal

  Scenario: Goal completion triggers new goal generation
    Given an NPC "Tomas" exists with archetype "Maintainer"
    And the NPC has exactly 2 active goals
    When the NPC completes a goal
    Then the NPC should trigger goal evaluation
    And the NPC should have at least 2 active goals
```

#### Offline Progress Feature
```gherkin
# Features/Progression/OfflineProgress.feature

Feature: Offline Progress Calculation
  As a player
  I want my character to progress while I'm away
  So that I can enjoy an idle game experience

  Background:
    Given a player "Hero" exists with:
      | Property       | Value         |
      | Class          | [Miner]       |
      | Level          | 15            |
      | Current Zone   | Iron Mine     |
    And the player has an active activity loop

  # ============================================
  # BASIC OFFLINE CALCULATION
  # ============================================

  Scenario: Player gains resources during offline period
    Given the player's activity loop is:
      | Phase  | Action | Location  | Duration |
      | gather | Gather | Iron Mine | 4 hours  |
    And the player was last online 4 hours ago
    When the offline progress is calculated
    Then the player should have gained Iron Ore
    And the player should have gained tag affinity for "gather.mining"

  Scenario: Diminishing returns apply to long offline periods
    Given the player's activity loop is configured for gathering
    And the player was last online 24 hours ago
    When the offline progress is calculated
    Then the efficiency for hours 1-4 should be 100%
    And the efficiency for hours 4-8 should be 75%
    And the efficiency for hours 8-16 should be 50%
    And the efficiency for hours 16-24 should be 25%

  Scenario: Player can level up during offline period
    Given the player is 50 XP away from level 16
    And the player's activity loop generates approximately 20 XP per hour
    And the player was last online 8 hours ago
    When the offline progress is calculated
    Then the player should be at least level 16
    And the player should have a pending skill selection

  # ============================================
  # ACTIVITY LOOP EXECUTION
  # ============================================

  Scenario: Multi-phase activity loop executes correctly
    Given the player's activity loop is:
      | Phase    | Action | Target          | Condition              |
      | gather   | Gather | Iron Mine       | until inventory 80%    |
      | travel   | Travel | Millbrook       |                        |
      | sell     | Trade  | sell Iron Ore   | when inventory > 50    |
      | travel   | Travel | Iron Mine       |                        |
      | repeat   |        |                 |                        |
    And the player was last online 8 hours ago
    When the offline progress is calculated
    Then the player should have completed multiple loop iterations
    And the player should have earned gold from sales
    And the player's inventory should not be full

  Scenario: Activity loop respects stop conditions
    Given the player's activity loop has condition "Stop if HP < 30%"
    And the mine has hostile creatures
    And the player was last online 8 hours ago
    When the offline progress is calculated
    Then the activity loop should have stopped when HP dropped below 30%
    And remaining time should use "rest" action

  # ============================================
  # DETERMINISTIC CALCULATION
  # ============================================

  Scenario: Offline calculation is deterministic
    Given the player's RNG state is saved
    And the player was last online 4 hours ago
    When the offline progress is calculated
    And the offline progress is calculated again with the same RNG state
    Then both calculations should produce identical results

  Scenario: Offline calculation can be verified
    Given the player was last online 4 hours ago
    When the offline progress is calculated
    Then the result should include a verification hash
    And replaying the events should produce the same hash
```

---

## 4. Step Definition Architecture

### 4.1 Shared Test Context

```
StepDefinitions/
├── Hooks/
│   ├── TestSetup.cs              # Before/After scenario hooks
│   └── TestTeardown.cs
├── Context/
│   ├── GameTestContext.cs        # Shared state across steps
│   ├── PlayerContext.cs          # Player-specific state
│   ├── NpcContext.cs             # NPC-specific state
│   └── WorldContext.cs           # World state
├── Drivers/
│   ├── GameEngineDriver.cs       # Direct engine interaction
│   ├── ApiDriver.cs              # HTTP API interaction
│   └── GrainDriver.cs            # Orleans grain interaction
├── Steps/
│   ├── Common/
│   │   ├── GivenSteps.cs         # Shared Given steps
│   │   ├── WhenSteps.cs          # Shared When steps
│   │   └── ThenSteps.cs          # Shared Then steps
│   ├── ClassSystem/
│   │   └── ClassSteps.cs
│   ├── SkillSystem/
│   │   └── SkillSteps.cs
│   ├── TagSystem/
│   │   └── TagSteps.cs
│   ├── NpcBehavior/
│   │   └── NpcSteps.cs
│   └── ...
└── Builders/
    ├── PlayerBuilder.cs          # Fluent player creation
    ├── NpcBuilder.cs             # Fluent NPC creation
    ├── WorldBuilder.cs           # Fluent world setup
    └── ActionBuilder.cs          # Fluent action creation
```

### 4.2 Test Context Pattern

```csharp
// Context/GameTestContext.cs

/// <summary>
/// Shared context for all BDD scenarios.
/// Injected via Reqnroll's dependency injection.
/// </summary>
public class GameTestContext : IDisposable
{
    // Core engine (same code as production)
    public GameEngine Engine { get; }
    public DeterministicRng Rng { get; }
    public InMemoryEventStore EventStore { get; }
    
    // Current scenario state
    public PlayerState? CurrentPlayer { get; set; }
    public NpcState? CurrentNpc { get; set; }
    public WorldState World { get; set; }
    
    // Results from actions
    public ActionResult? LastActionResult { get; set; }
    public IReadOnlyList<IGameEvent> LastEvents { get; set; }
    
    public GameTestContext()
    {
        // Deterministic seed for reproducibility
        Rng = new DeterministicRng(12345);
        EventStore = new InMemoryEventStore();
        Engine = new GameEngine(EventStore, Rng);
        World = WorldBuilder.CreateDefault();
        LastEvents = Array.Empty<IGameEvent>();
    }
    
    public void Dispose()
    {
        // Cleanup
    }
}
```

### 4.3 Step Definition Examples

```csharp
// Steps/ClassSystem/ClassSteps.cs

[Binding]
public class ClassSteps
{
    private readonly GameTestContext _context;
    
    public ClassSteps(GameTestContext context)
    {
        _context = context;
    }
    
    [Given(@"a new player ""(.*)"" exists")]
    public void GivenANewPlayerExists(string playerName)
    {
        _context.CurrentPlayer = new PlayerBuilder()
            .WithName(playerName)
            .WithNoClasses()
            .Build();
    }
    
    [Given(@"the player has no classes")]
    public void GivenThePlayerHasNoClasses()
    {
        _context.CurrentPlayer!.Classes.Clear();
    }
    
    [Given(@"the player has the following tag affinities:")]
    public void GivenThePlayerHasTagAffinities(Table table)
    {
        foreach (var row in table.Rows)
        {
            var tag = row["Tag"];
            var value = int.Parse(row["Value"]);
            _context.CurrentPlayer!.TagAffinities[tag] = value;
        }
    }
    
    [When(@"the player performs (\d+) ""(.*)"" actions with weapon ""(.*)""")]
    public async Task WhenPlayerPerformsActionsWithWeapon(
        int count, string actionType, string weapon)
    {
        var weaponType = Enum.Parse<WeaponType>(weapon, ignoreCase: true);
        
        for (int i = 0; i < count; i++)
        {
            var action = new ActionBuilder()
                .OfType(actionType)
                .WithWeapon(weaponType)
                .Build();
                
            _context.LastActionResult = await _context.Engine
                .ProcessActionAsync(
                    _context.CurrentPlayer!.PlayerId,
                    action,
                    new GameState(_context.CurrentPlayer, _context.World));
                    
            // Apply results to player state
            _context.CurrentPlayer = _context.CurrentPlayer
                .Apply(_context.LastActionResult.Events);
        }
    }
    
    [When(@"the player performs a ""(.*)"" action")]
    public async Task WhenPlayerPerformsAction(string actionType)
    {
        var action = new ActionBuilder()
            .OfType(actionType)
            .Build();
            
        _context.LastActionResult = await _context.Engine
            .ProcessActionAsync(
                _context.CurrentPlayer!.PlayerId,
                action,
                new GameState(_context.CurrentPlayer, _context.World));
                
        _context.CurrentPlayer = _context.CurrentPlayer
            .Apply(_context.LastActionResult.Events);
        _context.LastEvents = _context.LastActionResult.Events;
    }
    
    [Then(@"the player should have class ""(.*)"" at level (\d+)")]
    public void ThenPlayerShouldHaveClassAtLevel(string className, int level)
    {
        _context.CurrentPlayer!.Classes
            .Should().Contain(c => 
                c.Name == className && c.Level == level,
                $"Expected player to have {className} at level {level}");
    }
    
    [Then(@"the player should not have any classes")]
    public void ThenPlayerShouldNotHaveAnyClasses()
    {
        _context.CurrentPlayer!.Classes.Should().BeEmpty();
    }
    
    [Then(@"the player should have received a skill with tags containing ""(.*)""")]
    public void ThenPlayerShouldHaveReceivedSkillWithTags(string tagFragment)
    {
        var skillAcquiredEvent = _context.LastEvents
            .OfType<SkillAcquired>()
            .FirstOrDefault();
            
        skillAcquiredEvent.Should().NotBeNull(
            "Expected a skill to be acquired during rest");
            
        skillAcquiredEvent!.Skill.Tags
            .Should().Contain(t => t.Contains(tagFragment),
                $"Expected skill tags to contain '{tagFragment}'");
    }
}
```

### 4.4 Fluent Builders

```csharp
// Builders/PlayerBuilder.cs

public class PlayerBuilder
{
    private string _playerId = Guid.NewGuid().ToString();
    private string _name = "TestPlayer";
    private readonly List<PlayerClass> _classes = new();
    private readonly List<Skill> _skills = new();
    private readonly Dictionary<string, int> _tagAffinities = new();
    private int _gold = 100;
    private string _currentZone = "Starting Village";
    
    public PlayerBuilder WithName(string name)
    {
        _name = name;
        return this;
    }
    
    public PlayerBuilder WithClass(string className, int level)
    {
        _classes.Add(new PlayerClass { Name = className, Level = level });
        return this;
    }
    
    public PlayerBuilder WithNoClasses()
    {
        _classes.Clear();
        return this;
    }
    
    public PlayerBuilder WithTagAffinity(string tag, int value)
    {
        _tagAffinities[tag] = value;
        return this;
    }
    
    public PlayerBuilder WithTagAffinities(Dictionary<string, int> affinities)
    {
        foreach (var (tag, value) in affinities)
            _tagAffinities[tag] = value;
        return this;
    }
    
    public PlayerBuilder InZone(string zone)
    {
        _currentZone = zone;
        return this;
    }
    
    public PlayerBuilder WithGold(int gold)
    {
        _gold = gold;
        return this;
    }
    
    public PlayerBuilder AtLevel(string className, int level, int xpToNext = 0)
    {
        var existingClass = _classes.FirstOrDefault(c => c.Name == className);
        if (existingClass != null)
        {
            existingClass.Level = level;
            existingClass.XpToNextLevel = xpToNext;
        }
        else
        {
            _classes.Add(new PlayerClass 
            { 
                Name = className, 
                Level = level,
                XpToNextLevel = xpToNext
            });
        }
        return this;
    }
    
    public PlayerState Build()
    {
        return new PlayerState
        {
            PlayerId = _playerId,
            Name = _name,
            Classes = _classes.ToList(),
            Skills = _skills.ToList(),
            TagAffinities = new Dictionary<string, int>(_tagAffinities),
            Gold = _gold,
            CurrentZone = _currentZone
        };
    }
}
```

---

## 5. Test Execution Modes

### 5.1 Three Execution Modes

```
┌─────────────────────────────────────────────────────────────────────┐
│                      SAME FEATURE FILES                             │
│                   DIFFERENT EXECUTION MODES                         │
└─────────────────────────────────────────────────────────────────────┘
                              │
          ┌───────────────────┼───────────────────┐
          ▼                   ▼                   ▼
   ┌─────────────┐     ┌─────────────┐     ┌─────────────┐
   │    UNIT     │     │ INTEGRATION │     │    E2E      │
   │    MODE     │     │    MODE     │     │    MODE     │
   ├─────────────┤     ├─────────────┤     ├─────────────┤
   │             │     │             │     │             │
   │ GameEngine  │     │   Orleans   │     │  Full App   │
   │   direct    │     │   Grains    │     │   + UI      │
   │             │     │             │     │             │
   │ InMemory    │     │ TestContain │     │ Playwright  │
   │  storage    │     │  Postgres   │     │  browser    │
   │             │     │             │     │             │
   │ < 1 second  │     │ 5-30 secs   │     │ 30s-2min    │
   │             │     │             │     │             │
   │ Every PR    │     │  Nightly    │     │  Release    │
   └─────────────┘     └─────────────┘     └─────────────┘
```

### 5.2 Driver Abstraction

```csharp
// Drivers/IGameDriver.cs

/// <summary>
/// Abstraction allowing same tests to run against different backends.
/// </summary>
public interface IGameDriver
{
    Task<PlayerState> CreatePlayerAsync(string name);
    Task<PlayerState> GetPlayerAsync(string playerId);
    Task<ActionResult> PerformActionAsync(string playerId, GameAction action);
    Task<NpcState> CreateNpcAsync(NpcDefinition definition);
    Task<NpcState> SimulateNpcTickAsync(Guid npcId);
    Task<WorldState> GetWorldStateAsync();
}

// Unit mode - direct engine
public class GameEngineDriver : IGameDriver
{
    private readonly GameEngine _engine;
    private readonly Dictionary<string, PlayerState> _players = new();
    
    public Task<ActionResult> PerformActionAsync(string playerId, GameAction action)
    {
        var player = _players[playerId];
        var result = _engine.ProcessActionAsync(playerId, action, 
            new GameState(player, _world));
        _players[playerId] = player.Apply(result.Events);
        return Task.FromResult(result);
    }
}

// Integration mode - Orleans grains
public class GrainDriver : IGameDriver
{
    private readonly IGrainFactory _grainFactory;
    
    public async Task<ActionResult> PerformActionAsync(string playerId, GameAction action)
    {
        var grain = _grainFactory.GetGrain<IPlayerGrain>(playerId);
        return await grain.PerformActionAsync(action);
    }
}

// E2E mode - HTTP API
public class ApiDriver : IGameDriver
{
    private readonly HttpClient _httpClient;
    
    public async Task<ActionResult> PerformActionAsync(string playerId, GameAction action)
    {
        var response = await _httpClient.PostAsJsonAsync(
            $"/api/players/{playerId}/actions", action);
        return await response.Content.ReadFromJsonAsync<ActionResult>();
    }
}
```

---

## 6. Performance Testing

### 6.1 Micro-Benchmarks

```csharp
// Performance.Tests/Benchmarks/TagCalculationBenchmarks.cs

[MemoryDiagnoser]
[SimpleJob(RuntimeMoniker.Net80)]
public class TagCalculationBenchmarks
{
    private readonly TagCalculator _calculator = new();
    private readonly StrikeAction _strikeAction;
    private readonly GatherAction _gatherAction;
    private readonly PlayerState _playerWith10Tags;
    private readonly PlayerState _playerWith1000Tags;
    
    [GlobalSetup]
    public void Setup()
    {
        _strikeAction = new StrikeAction 
        { 
            WeaponType = WeaponType.Sword,
            Technique = StrikeTechnique.Slash
        };
        
        _playerWith10Tags = CreatePlayerWithTags(10);
        _playerWith1000Tags = CreatePlayerWithTags(1000);
    }
    
    [Benchmark(Baseline = true)]
    public IReadOnlyList<TagXpGain> CalculateTags_Strike()
    {
        return _calculator.CalculateTags(_strikeAction);
    }
    
    [Benchmark]
    public IReadOnlyList<TagXpGain> CalculateTags_Gather()
    {
        return _calculator.CalculateTags(_gatherAction);
    }
    
    [Benchmark]
    public bool CheckClassEligibility_10Tags()
    {
        return _classEvaluator.IsEligible(_playerWith10Tags, ClassDef.Warrior);
    }
    
    [Benchmark]
    public bool CheckClassEligibility_1000Tags()
    {
        return _classEvaluator.IsEligible(_playerWith1000Tags, ClassDef.Warrior);
    }
}
```

### 6.2 Load Tests

```csharp
// Performance.Tests/Load/MultiplayerLoadScenarios.cs

public class MultiplayerLoadScenarios
{
    public static ScenarioProps ConcurrentPlayersScenario()
    {
        return Scenario.Create("concurrent_players", async context =>
        {
            var playerId = $"player_{context.ScenarioInfo.ThreadNumber}";
            var client = new GameApiClient(Config.ApiUrl);
            
            // Login
            await client.LoginAsync(playerId);
            
            // Perform typical idle loop actions
            for (int i = 0; i < 10; i++)
            {
                var step1 = await Step.Run("gather_action", context, async () =>
                {
                    var result = await client.PerformActionAsync(playerId, 
                        new GatherAction { Location = "Iron Mine" });
                    return Response.Ok(result);
                });
                
                var step2 = await Step.Run("check_state", context, async () =>
                {
                    var state = await client.GetPlayerStateAsync(playerId);
                    return Response.Ok(state);
                });
            }
            
            return Response.Ok();
        })
        .WithWarmUpDuration(TimeSpan.FromSeconds(10))
        .WithLoadSimulations(
            Simulation.KeepConstant(copies: 100, during: TimeSpan.FromMinutes(5)),
            Simulation.RampingInject(rate: 10, interval: TimeSpan.FromSeconds(1), 
                                     during: TimeSpan.FromMinutes(2))
        );
    }
    
    public static ScenarioProps NpcSimulationScenario()
    {
        return Scenario.Create("npc_simulation", async context =>
        {
            var client = new GameApiClient(Config.ApiUrl);
            
            // Trigger NPC simulation tick for a zone
            var step = await Step.Run("zone_tick", context, async () =>
            {
                var result = await client.TriggerZoneTickAsync("Millbrook");
                return Response.Ok(result, sizeBytes: result.NpcsSimulated * 100);
            });
            
            return Response.Ok();
        })
        .WithLoadSimulations(
            Simulation.KeepConstant(copies: 10, during: TimeSpan.FromMinutes(10))
        );
    }
}
```

### 6.3 Performance Baselines

```csharp
// Performance.Tests/Baselines/PerformanceBaselines.cs

public static class PerformanceBaselines
{
    // Micro-benchmark baselines (operations per second)
    public static class Throughput
    {
        public const int TagCalculation = 100_000;       // 100k/sec minimum
        public const int ClassEligibility = 50_000;      // 50k/sec minimum
        public const int ActionProcessing = 10_000;      // 10k/sec minimum
        public const int NpcTickSimulation = 1_000;      // 1k/sec minimum
        public const int OfflineCalculation = 100;       // 100/sec (complex)
    }
    
    // API response time baselines (milliseconds)
    public static class Latency
    {
        public const int ActionEndpoint_P50 = 50;        // 50ms median
        public const int ActionEndpoint_P95 = 200;       // 200ms 95th percentile
        public const int ActionEndpoint_P99 = 500;       // 500ms 99th percentile
        public const int StateQuery_P50 = 20;            // 20ms median
        public const int StateQuery_P95 = 100;           // 100ms 95th percentile
    }
    
    // Load test baselines
    public static class Load
    {
        public const int ConcurrentPlayers = 1000;       // Support 1000 CCU
        public const int ActionsPerSecond = 5000;        // 5000 actions/sec
        public const int NpcsPerZone = 500;              // 500 NPCs per zone
        public const int ZonesSimultaneous = 100;        // 100 active zones
    }
}
```

---

## 7. CI/CD Integration

### 7.1 Test Pipeline

```yaml
# .github/workflows/test.yml

name: Test Pipeline

on:
  push:
    branches: [main, develop]
  pull_request:
    branches: [main]

jobs:
  unit-tests:
    runs-on: ubuntu-latest
    steps:
      - uses: actions/checkout@v4
      
      - name: Setup .NET
        uses: actions/setup-dotnet@v4
        with:
          dotnet-version: '8.0.x'
      
      - name: Run Unit Tests
        run: |
          dotnet test tests/IdleWorlds.Core.Tests \
            --filter "Category!=Integration" \
            --logger "trx;LogFileName=unit-results.trx" \
            --collect:"XPlat Code Coverage"
      
      - name: Run BDD Tests (Unit Mode)
        run: |
          dotnet test tests/IdleWorlds.Core.Tests \
            --filter "Category=BDD" \
            --logger "trx;LogFileName=bdd-results.trx"
      
      - name: Check Coverage
        run: |
          # Fail if coverage below 80%
          dotnet tool run reportgenerator \
            -reports:**/coverage.cobertura.xml \
            -targetdir:coverage \
            -reporttypes:TextSummary
          
          coverage=$(grep "Line coverage" coverage/Summary.txt | grep -oP '\d+')
          if [ "$coverage" -lt "80" ]; then
            echo "Coverage $coverage% is below 80% threshold"
            exit 1
          fi

  integration-tests:
    runs-on: ubuntu-latest
    needs: unit-tests
    services:
      postgres:
        image: postgres:16
        env:
          POSTGRES_PASSWORD: test
        ports:
          - 5432:5432
      redis:
        image: redis:7
        ports:
          - 6379:6379
    steps:
      - uses: actions/checkout@v4
      
      - name: Run Integration Tests
        run: |
          dotnet test tests/IdleWorlds.Integration.Tests \
            --logger "trx;LogFileName=integration-results.trx"

  performance-tests:
    runs-on: ubuntu-latest
    needs: unit-tests
    if: github.event_name == 'push' && github.ref == 'refs/heads/main'
    steps:
      - uses: actions/checkout@v4
      
      - name: Run Benchmarks
        run: |
          dotnet run --project tests/IdleWorlds.Performance.Tests/Benchmarks \
            --configuration Release \
            -- --filter "*" --exporters json
      
      - name: Compare to Baseline
        run: |
          python scripts/compare_benchmarks.py \
            --current BenchmarkDotNet.Artifacts/results/*.json \
            --baseline baselines/benchmarks.json \
            --threshold 0.10
      
      - name: Upload Results
        uses: actions/upload-artifact@v4
        with:
          name: benchmark-results
          path: BenchmarkDotNet.Artifacts/

  nightly-load-tests:
    runs-on: ubuntu-latest
    if: github.event.schedule == '0 2 * * *'  # 2 AM daily
    steps:
      - uses: actions/checkout@v4
      
      - name: Deploy Test Environment
        run: ./scripts/deploy-test-env.sh
      
      - name: Run Load Tests
        run: |
          dotnet run --project tests/IdleWorlds.Performance.Tests/Load \
            --configuration Release
      
      - name: Analyze Results
        run: |
          python scripts/analyze_load_results.py \
            --results ./load-results \
            --baseline baselines/load.json
```

### 7.2 Test Reports

```
TEST EXECUTION SUMMARY
━━━━━━━━━━━━━━━━━━━━━━

Every PR:
  ✓ Unit Tests (500+)           ~30 seconds
  ✓ BDD Tests - Unit Mode (200+) ~60 seconds
  ✓ Micro-benchmarks            ~2 minutes
  
Nightly:
  ✓ Integration Tests (100+)    ~10 minutes
  ✓ BDD Tests - Integration Mode ~15 minutes
  ✓ Load Tests                  ~30 minutes
  
Weekly:
  ✓ Stress Tests                ~2 hours
  ✓ E2E Tests (50+)             ~30 minutes
  ✓ Mutation Testing            ~4 hours
  
Pre-Release:
  ✓ Soak Tests                  ~24-48 hours
  ✓ Full E2E Suite              ~2 hours
  ✓ Performance Regression      ~1 hour
```

---

## 8. Test Data Management

### 8.1 Data Generation with Bogus

```csharp
// TestData/Fakers/PlayerFaker.cs

public class PlayerFaker : Faker<PlayerState>
{
    public PlayerFaker()
    {
        RuleFor(p => p.PlayerId, f => f.Random.Guid().ToString());
        RuleFor(p => p.Name, f => f.Name.FirstName());
        RuleFor(p => p.Gold, f => f.Random.Int(0, 10000));
        RuleFor(p => p.CurrentZone, f => f.PickRandom(TestZones.All));
        RuleFor(p => p.Classes, f => new List<PlayerClass>());
        RuleFor(p => p.Skills, f => new List<Skill>());
        RuleFor(p => p.TagAffinities, f => new Dictionary<string, int>());
    }
    
    public PlayerFaker WithRandomClasses(int count = 1)
    {
        RuleFor(p => p.Classes, f => 
            new ClassFaker().Generate(count));
        return this;
    }
    
    public PlayerFaker WithTagsFor(string classType)
    {
        RuleFor(p => p.TagAffinities, f =>
            TagPresets.ForClass(classType));
        return this;
    }
}

public class NpcFaker : Faker<NpcState>
{
    public NpcFaker()
    {
        RuleFor(n => n.NpcId, f => f.Random.Guid());
        RuleFor(n => n.Name, f => f.Name.FullName());
        RuleFor(n => n.Archetype, f => f.PickRandom<NpcArchetype>());
        RuleFor(n => n.Personality, f => new PersonalityFaker().Generate());
    }
    
    public NpcFaker AsMaintainer()
    {
        RuleFor(n => n.Archetype, NpcArchetype.Maintainer);
        RuleFor(n => n.Goals, f => 
            new GoalFaker().MaintainerGoals().Generate(3));
        return this;
    }
    
    public NpcFaker AsVillain(string subtype = "Tyrant")
    {
        RuleFor(n => n.Archetype, NpcArchetype.Villain);
        RuleFor(n => n.VillainSubtype, subtype);
        RuleFor(n => n.Goals, f => 
            new GoalFaker().VillainGoals().Generate(5));
        return this;
    }
}
```

### 8.2 Snapshot Testing with Verify

```csharp
// Tests/SnapshotTests/PlayerProgressionSnapshots.cs

[UsesVerify]
public class PlayerProgressionSnapshots
{
    [Fact]
    public async Task NewPlayer_AfterFirst100Actions_StateSnapshot()
    {
        // Arrange
        var context = new GameTestContext();
        var player = new PlayerBuilder().WithName("SnapshotTest").Build();
        
        // Act - Perform standardized action sequence
        for (int i = 0; i < 100; i++)
        {
            await context.Engine.ProcessActionAsync(
                player.PlayerId,
                StandardActions.Strike,
                new GameState(player, context.World));
        }
        
        // Assert - Verify against snapshot
        await Verify(player)
            .UseDirectory("Snapshots")
            .UseFileName("NewPlayer_100Actions");
    }
    
    [Fact]
    public async Task NpcMaintainer_DailyLoopGeneration_Snapshot()
    {
        var npc = new NpcFaker().AsMaintainer().Generate();
        var loop = context.Engine.GenerateDailyLoop(npc, context.World);
        
        await Verify(loop)
            .UseDirectory("Snapshots")
            .UseFileName("Maintainer_DailyLoop");
    }
}
```

---

*Document Version 1.0 — Automated Testing Strategy*