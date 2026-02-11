using RealmsOfIdle.Client.Blazor.Models;

namespace RealmsOfIdle.Client.Blazor.Services;

/// <summary>
/// NPC AI service for autonomous behavior
/// Implements simple state machine with priority-based action selection
/// </summary>
public class NPCAIService
{
    /// <summary>
    /// Updates an NPC's autonomous behavior (called each tick for non-possessed NPCs)
    /// </summary>
    public Settlement UpdateNPC(NPC npc, Settlement settlement)
    {
        // Skip possessed NPCs (player controls them)
        if (npc.IsPossessed)
            return settlement;

        // Check if current action is complete
        if (npc.State == NPCState.Working && npc.ActionStartTime.HasValue && npc.ActionDurationSeconds.HasValue)
        {
            var elapsed = DateTime.UtcNow - npc.ActionStartTime.Value;
            if (elapsed.TotalSeconds >= npc.ActionDurationSeconds.Value)
            {
                // Action complete - apply rewards and return to idle
                return CompleteAction(npc, settlement);
            }

            // Still working, no update needed
            return settlement;
        }

        // NPC is idle - choose next action
        if (npc.State == NPCState.Idle)
        {
            var action = ChooseNextAction(npc, settlement);
            if (action != null)
            {
                return StartAction(npc, action, settlement);
            }
        }

        return settlement;
    }

    /// <summary>
    /// Chooses the next action for an NPC based on class and context
    /// Implements priority-based selection
    /// </summary>
    private NPCAction? ChooseNextAction(NPC npc, Settlement settlement)
    {
        return npc.ClassName switch
        {
            "Innkeeper" => ChooseInnkeeperAction(npc, settlement),
            "Blacksmith" => ChooseBlacksmithAction(npc, settlement),
            "Cook" => ChooseCookAction(npc, settlement),
            "Customer" => ChooseCustomerAction(npc, settlement),
            _ => ActionCatalog.Rest
        };
    }

    /// <summary>
    /// Chooses action for Innkeeper class
    /// Priority: Serve customers if waiting, else check income (idle action)
    /// </summary>
    private NPCAction? ChooseInnkeeperAction(NPC npc, Settlement settlement)
    {
        // Check if at inn
        if (npc.CurrentBuilding != "inn")
            return ActionCatalog.Rest;

        // Find building to check resources
        var inn = settlement.Buildings.FirstOrDefault(b => b.Id == "inn");
        if (inn == null)
            return ActionCatalog.Rest;

        // Priority 1: Serve customers if we have food
        var hasFood = inn.Resources.TryGetValue("Food", out var food) && food > 0;
        if (hasFood)
            return ActionCatalog.ServeCustomer;

        // Priority 2: Check income (idle action)
        return ActionCatalog.CheckIncome;
    }

    /// <summary>
    /// Chooses action for Blacksmith class
    /// Priority: Craft if materials available, else check materials (idle action)
    /// </summary>
    private NPCAction? ChooseBlacksmithAction(NPC npc, Settlement settlement)
    {
        // Check if at workshop
        if (npc.CurrentBuilding != "workshop")
            return ActionCatalog.Rest;

        // Find building to check resources
        var workshop = settlement.Buildings.FirstOrDefault(b => b.Id == "workshop");
        if (workshop == null)
            return ActionCatalog.Rest;

        // Priority 1: Craft sword if we have iron ore
        var hasIron = workshop.Resources.TryGetValue("IronOre", out var iron) && iron >= 2;
        if (hasIron)
            return ActionCatalog.CraftSword;

        // Priority 2: Check materials (idle action)
        return ActionCatalog.CheckMaterials;
    }

    /// <summary>
    /// Chooses action for Cook class
    /// Always produces food (main job)
    /// </summary>
    private NPCAction? ChooseCookAction(NPC npc, Settlement settlement)
    {
        // Check if at inn
        if (npc.CurrentBuilding != "inn")
            return ActionCatalog.Rest;

        // Cook always produces food
        return ActionCatalog.ProduceFood;
    }

    /// <summary>
    /// Chooses action for Customer class
    /// Customers just rest (passive NPCs for MVP)
    /// </summary>
    private NPCAction? ChooseCustomerAction(NPC npc, Settlement settlement)
    {
        return ActionCatalog.Rest;
    }

    /// <summary>
    /// Starts an action for an NPC
    /// </summary>
    private Settlement StartAction(NPC npc, NPCAction action, Settlement settlement)
    {
        // Update NPC to working state
        var updatedNPC = npc with
        {
            State = NPCState.Working,
            CurrentAction = action.Id,
            ActionStartTime = DateTime.UtcNow,
            ActionDurationSeconds = action.DurationSeconds
        };

        // Replace NPC in settlement
        var updatedSettlement = settlement with
        {
            NPCs = settlement.NPCs
                .Select(n => n.Id == npc.Id ? updatedNPC : n)
                .ToList()
        };

        return updatedSettlement;
    }

    /// <summary>
    /// Completes an action and applies rewards
    /// </summary>
    private Settlement CompleteAction(NPC npc, Settlement settlement)
    {
        if (npc.CurrentAction == null)
            return settlement;

        // Get action details
        var action = GetActionById(npc.CurrentAction);
        if (action == null)
            return settlement;

        // Find building to update resources
        var building = settlement.Buildings.FirstOrDefault(b => b.Id == npc.CurrentBuilding);

        // Apply resource costs and production
        IReadOnlyDictionary<string, int>? updatedResources = null;
        if (building != null)
        {
            var resources = new Dictionary<string, int>(building.Resources);

            // Consume costs
            foreach (var cost in action.ResourceCosts)
            {
                if (resources.TryGetValue(cost.Key, out var available))
                    resources[cost.Key] = Math.Max(0, available - cost.Value);
            }

            // Add produced resources
            foreach (var produced in action.ResourceProduced)
            {
                if (resources.TryGetValue(produced.Key, out var current))
                    resources[produced.Key] = current + produced.Value;
                else
                    resources[produced.Key] = produced.Value;
            }

            updatedResources = resources;
        }

        // Apply rewards to building gold (for innkeeper/blacksmith actions)
        var updatedBuilding = building != null && updatedResources != null
            ? building with
            {
                Resources = updatedResources,
                Gold = building.Gold + (action.Rewards.TryGetValue("Gold", out var gold) ? gold : 0)
            }
            : building;

        // Update NPC to idle and apply reputation
        var reputationGain = action.Rewards.TryGetValue("Reputation", out var rep) ? rep : 0;
        var updatedNPC = npc with
        {
            State = NPCState.Idle,
            CurrentAction = null,
            ActionStartTime = null,
            ActionDurationSeconds = null,
            Reputation = npc.Reputation + reputationGain
        };

        // Update settlement
        var updatedSettlement = settlement with
        {
            Buildings = updatedBuilding != null
                ? settlement.Buildings.Select(b => b.Id == building!.Id ? updatedBuilding : b).ToList()
                : settlement.Buildings,
            NPCs = settlement.NPCs.Select(n => n.Id == npc.Id ? updatedNPC : n).ToList()
        };

        return updatedSettlement;
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
}
