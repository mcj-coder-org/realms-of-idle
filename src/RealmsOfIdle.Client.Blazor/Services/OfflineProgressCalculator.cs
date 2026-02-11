using RealmsOfIdle.Client.Blazor.Models;

namespace RealmsOfIdle.Client.Blazor.Services;

/// <summary>
/// Result of offline progress calculation
/// </summary>
public sealed record OfflineProgressResult(
    Settlement UpdatedSettlement,
    int TotalGoldEarned,
    int TotalReputationEarned,
    int TotalActionsPerformed,
    TimeSpan CappedElapsedTime)
{
    public static OfflineProgressResult NoProgress(Settlement settlement)
    {
        return new OfflineProgressResult(settlement, 0, 0, 0, TimeSpan.Zero);
    }
}

/// <summary>
/// Calculates offline progress when player returns after tab hidden.
/// Minimum threshold: 60 seconds. Maximum cap: 24 hours (86400 seconds).
/// </summary>
public class OfflineProgressCalculator
{
    private const int MinimumElapsedSeconds = 60;
    private const int MaximumElapsedSeconds = 86400; // 24 hours

    /// <summary>
    /// Calculate progress for the time the player was away.
    /// Returns NoProgress if elapsed &lt; 60 seconds.
    /// Caps elapsed time at 24 hours.
    /// </summary>
    public OfflineProgressResult CalculateProgress(Settlement lastState, TimeSpan elapsed)
    {
        if (elapsed.TotalSeconds < MinimumElapsedSeconds)
            return OfflineProgressResult.NoProgress(lastState);

        var cappedSeconds = Math.Min(elapsed.TotalSeconds, MaximumElapsedSeconds);
        var cappedElapsed = TimeSpan.FromSeconds(cappedSeconds);

        var updatedNPCs = new List<NPC>();
        var totalGold = 0;
        var totalReputation = 0;
        var totalActions = 0;

        foreach (var npc in lastState.NPCs)
        {
            if (npc.IsPossessed)
            {
                // Clear possession, no rewards for possessed NPCs
                updatedNPCs.Add(npc with
                {
                    IsPossessed = false,
                    State = NPCState.Idle,
                    CurrentAction = null,
                    ActionStartTime = null,
                    ActionDurationSeconds = null
                });
                continue;
            }

            // Get NPC's primary action based on class and available resources
            var action = GetPrimaryAction(npc, lastState);
            if (action == null || !HasGoldOrReputationReward(action))
            {
                // Reset working NPCs to idle, keep unchanged otherwise
                updatedNPCs.Add(npc.State == NPCState.Working
                    ? npc with { State = NPCState.Idle, CurrentAction = null, ActionStartTime = null, ActionDurationSeconds = null }
                    : npc);
                continue;
            }

            // Calculate complete action cycles in elapsed time
            var cycleCount = (int)(cappedSeconds / action.DurationSeconds);
            var goldReward = cycleCount * action.Rewards.GetValueOrDefault("Gold", 0);
            var repReward = cycleCount * action.Rewards.GetValueOrDefault("Reputation", 0);

            updatedNPCs.Add(npc with
            {
                Gold = npc.Gold + goldReward,
                Reputation = npc.Reputation + repReward,
                State = NPCState.Idle,
                CurrentAction = null,
                ActionStartTime = null,
                ActionDurationSeconds = null
            });

            totalGold += goldReward;
            totalReputation += repReward;
            totalActions += cycleCount;
        }

        var updatedSettlement = lastState with
        {
            NPCs = updatedNPCs,
            WorldTime = lastState.WorldTime + cappedElapsed
        };

        return new OfflineProgressResult(
            updatedSettlement,
            totalGold,
            totalReputation,
            totalActions,
            cappedElapsed);
    }

    /// <summary>
    /// Determines the primary action for an NPC based on class and building resources.
    /// Mirrors NPCAIService priority logic for offline calculation.
    /// </summary>
    private static NPCAction? GetPrimaryAction(NPC npc, Settlement settlement)
    {
        var building = settlement.Buildings.FirstOrDefault(b => b.Id == npc.CurrentBuilding);

        return npc.ClassName switch
        {
            "Innkeeper" when building != null && HasResource(building, "Food", 1) => ActionCatalog.ServeCustomer,
            "Innkeeper" => ActionCatalog.CheckIncome,
            "Blacksmith" when building != null && HasResource(building, "IronOre", 2) => ActionCatalog.CraftSword,
            "Blacksmith" => ActionCatalog.CheckMaterials,
            "Cook" => ActionCatalog.ProduceFood,
            "Customer" => ActionCatalog.Rest,
            _ => null
        };
    }

    private static bool HasResource(Building building, string resource, int amount)
    {
        return building.Resources.TryGetValue(resource, out var available) && available >= amount;
    }

    private static bool HasGoldOrReputationReward(NPCAction action)
    {
        return action.Rewards.GetValueOrDefault("Gold", 0) > 0
            || action.Rewards.GetValueOrDefault("Reputation", 0) > 0;
    }
}
