using LiteDB;
using RealmsOfIdle.Client.Blazor.Models;
using RealmsOfIdle.Core.Abstractions;
using RealmsOfIdle.Core.Domain;

namespace RealmsOfIdle.Client.Blazor.Services;

/// <summary>
/// Game service for settlement possession demo - adapts LocalGameService pattern
/// Implements IGameService with LiteDB persistence for Settlement and ActivityLog
/// </summary>
public class SettlementGameService : IGameService
{
    private readonly LiteDatabase _database;
    private readonly ILogger<SettlementGameService> _logger;
    private Settlement? _currentSettlement;
    private const string SettlementsCollection = "settlements";
    private const string ActivityLogCollection = "activityLog";
    private const int MaxActivityLogEntries = 100;

    public SettlementGameService(LiteDatabase database, ILogger<SettlementGameService> logger)
    {
        _database = database;
        _logger = logger;
    }

    /// <summary>
    /// Gets the current settlement (creates Millbrook if none exists)
    /// </summary>
    public async Task<Settlement> GetOrCreateSettlementAsync()
    {
        if (_currentSettlement != null)
            return _currentSettlement;

        // Try to load from database
        var settlements = _database.GetCollection<Settlement>(SettlementsCollection);
        settlements.EnsureIndex(x => x.Id);

        _currentSettlement = settlements.FindById("millbrook");

        if (_currentSettlement == null)
        {
            // Create new Millbrook settlement
            _currentSettlement = Settlement.CreateMillbrook();
            settlements.Insert(_currentSettlement);
            _logger.LogInformation("Created new Millbrook settlement");
        }
        else
        {
            _logger.LogInformation("Loaded Millbrook settlement from database");
        }

        return await Task.FromResult(_currentSettlement);
    }

    /// <summary>
    /// Updates the current settlement and persists to LiteDB
    /// </summary>
    public async Task<Settlement> UpdateSettlementAsync(Settlement settlement)
    {
        _currentSettlement = settlement;

        var settlements = _database.GetCollection<Settlement>(SettlementsCollection);
        settlements.Upsert(settlement);

        _logger.LogDebug("Settlement '{SettlementId}' updated and persisted", settlement.Id);

        return await Task.FromResult(settlement);
    }

    /// <summary>
    /// Appends an entry to the activity log with automatic FIFO cleanup
    /// </summary>
    public async Task AddActivityLogEntryAsync(ActivityLogEntry entry)
    {
        var activityLog = _database.GetCollection<ActivityLogEntry>(ActivityLogCollection);
        activityLog.EnsureIndex(x => x.Timestamp);

        // Insert new entry
        activityLog.Insert(entry);

        // Cleanup: keep only last MaxActivityLogEntries
        var totalCount = activityLog.Count();
        if (totalCount > MaxActivityLogEntries)
        {
            var entriesToDelete = totalCount - MaxActivityLogEntries;
            var oldestEntries = activityLog
                .Query()
                .OrderBy(e => e.Timestamp)
                .Limit((int)entriesToDelete)
                .ToEnumerable()
                .Select(e => e.Id)
                .ToList();

            foreach (var id in oldestEntries)
            {
                activityLog.Delete(id);
            }

            _logger.LogDebug("Cleaned up {Count} old activity log entries", entriesToDelete);
        }

        await Task.CompletedTask;
    }

    /// <summary>
    /// Gets recent activity log entries (most recent first)
    /// </summary>
    public async Task<IReadOnlyList<ActivityLogEntry>> GetActivityLogAsync(int limit = 20)
    {
        var activityLog = _database.GetCollection<ActivityLogEntry>(ActivityLogCollection);
        var entries = activityLog
            .Query()
            .OrderByDescending(e => e.Timestamp)
            .Limit(limit)
            .ToList();

        return await Task.FromResult(entries.AsReadOnly());
    }

    /// <summary>
    /// Possess an NPC for direct control
    /// </summary>
    public async Task<ActionResult> PossessNPCAsync(string npcId)
    {
        var settlement = await GetOrCreateSettlementAsync();
        var npc = settlement.NPCs.FirstOrDefault(n => n.Id == npcId);

        if (npc == null)
            return ActionResult.Fail($"NPC '{npcId}' not found");

        if (npc.IsPossessed)
            return ActionResult.Fail($"NPC '{npc.Name}' is already possessed");

        // Update NPC to possessed state
        var updatedNPC = npc with { IsPossessed = true };
        var updatedSettlement = settlement with
        {
            NPCs = settlement.NPCs
                .Select(n => n.Id == npcId ? updatedNPC : n)
                .ToList()
        };

        await UpdateSettlementAsync(updatedSettlement);

        _logger.LogInformation("Possessed NPC '{NpcName}' ({NpcId})", npc.Name, npcId);

        return ActionResult.Ok($"Now possessing {npc.Name}");
    }

    /// <summary>
    /// Release possession of an NPC, returning to autonomous behavior
    /// </summary>
    public async Task ReleaseNPCAsync(string npcId)
    {
        var settlement = await GetOrCreateSettlementAsync();
        var npc = settlement.NPCs.FirstOrDefault(n => n.Id == npcId);

        if (npc == null || !npc.IsPossessed)
        {
            _logger.LogWarning("Cannot release NPC '{NpcId}' - not found or not possessed", npcId);
            return;
        }

        // Update NPC to not possessed
        var updatedNPC = npc with { IsPossessed = false };
        var updatedSettlement = settlement with
        {
            NPCs = settlement.NPCs
                .Select(n => n.Id == npcId ? updatedNPC : n)
                .ToList()
        };

        await UpdateSettlementAsync(updatedSettlement);

        _logger.LogInformation("Released NPC '{NpcName}' ({NpcId})", npc.Name, npcId);
    }

    /// <summary>
    /// Execute an action while possessing an NPC
    /// </summary>
    public async Task<ActionResult> ExecuteActionAsync(string npcId, string actionId)
    {
        var settlement = await GetOrCreateSettlementAsync();
        var npc = settlement.NPCs.FirstOrDefault(n => n.Id == npcId);

        if (npc == null)
            return ActionResult.Fail($"NPC '{npcId}' not found");

        if (!npc.IsPossessed)
            return ActionResult.Fail($"NPC '{npc.Name}' is not possessed");

        if (npc.State == NPCState.Working)
            return ActionResult.Fail($"NPC '{npc.Name}' is already working");

        // Get action from catalog
        var action = GetActionById(actionId);
        if (action == null)
            return ActionResult.Fail($"Action '{actionId}' not found");

        // Validate action requirements
        if (!action.RequiredClasses.Contains(npc.ClassName))
            return ActionResult.Fail($"NPC class '{npc.ClassName}' cannot perform action '{action.Name}'");

        if (action.RequiredBuildings.Any() && !action.RequiredBuildings.Contains(npc.CurrentBuilding))
            return ActionResult.Fail($"Action '{action.Name}' requires building: {string.Join(", ", action.RequiredBuildings)}");

        // Check resource costs
        var building = settlement.Buildings.FirstOrDefault(b => b.Id == npc.CurrentBuilding);
        if (building != null)
        {
            foreach (var cost in action.ResourceCosts)
            {
                if (!building.Resources.TryGetValue(cost.Key, out var available) || available < cost.Value)
                    return ActionResult.Fail($"Insufficient {cost.Key}: need {cost.Value}, have {available}");
            }
        }

        // Start action
        var updatedNPC = npc with
        {
            State = NPCState.Working,
            CurrentAction = action.Id,
            ActionStartTime = DateTime.UtcNow,
            ActionDurationSeconds = action.DurationSeconds
        };

        var updatedSettlement = settlement with
        {
            NPCs = settlement.NPCs
                .Select(n => n.Id == npcId ? updatedNPC : n)
                .ToList()
        };

        await UpdateSettlementAsync(updatedSettlement);

        _logger.LogInformation("NPC '{NpcName}' started action '{ActionName}'", npc.Name, action.Name);

        return ActionResult.Ok($"Started {action.Name} (takes {action.DurationSeconds}s)");
    }

    private static NPCAction? GetActionById(string actionId) => actionId switch
    {
        "serve_customer" => ActionCatalog.ServeCustomer,
        "produce_food" => ActionCatalog.ProduceFood,
        "craft_sword" => ActionCatalog.CraftSword,
        "check_income" => ActionCatalog.CheckIncome,
        "manage_cook" => ActionCatalog.ManageCook,
        "check_materials" => ActionCatalog.CheckMaterials,
        "rest" => ActionCatalog.Rest,
        _ => null
    };

    #region IGameService Implementation (Stubs)

    // These methods are required by IGameService but not used in the possession demo

    public Task<GameSession> InitializeGameAsync(string playerId, GameConfiguration configuration)
    {
        throw new NotImplementedException("Use GetOrCreateSettlementAsync instead");
    }

    public Task<GameSession?> GetActiveSessionAsync(string playerId)
    {
        throw new NotImplementedException("Use GetOrCreateSettlementAsync instead");
    }

    public Task<GameSession> ProcessTickAsync(string playerId, TimeSpan deltaTime)
    {
        throw new NotImplementedException("Use SimulationEngine for tick processing");
    }

    public Task<ActionResult> HandleActionAsync(string playerId, GameAction action)
    {
        throw new NotImplementedException("Use ExecuteActionAsync instead");
    }

    public Task SaveGameAsync(string playerId)
    {
        // Settlement is auto-saved on every update
        return Task.CompletedTask;
    }

    public Task<GameSession?> LoadGameAsync(string playerId)
    {
        throw new NotImplementedException("Use GetOrCreateSettlementAsync instead");
    }

    public Task<PlayerProfile> GetOrCreatePlayerProfileAsync(string playerId)
    {
        throw new NotImplementedException("Not used in possession demo");
    }

    public Task<GameStats> GetGameStatsAsync(string playerId)
    {
        throw new NotImplementedException("Not used in possession demo");
    }

    #endregion
}
