using RealmsOfIdle.Client.Blazor.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RealmsOfIdle.Client.Blazor.Services;

/// <summary>
/// Calculates offline progress for Settlement when player returns after tab was hidden.
/// Implements idle game mechanics: world continues progressing while player is away.
/// </summary>
public sealed class OfflineProgressCalculator
{
    private const int MinOfflineSecondsForCalculation = 60; // 1 minute
    private const int MaxOfflineSeconds = 86400; // 24 hours

    /// <summary>
    /// Calculate and apply offline progress to settlement based on elapsed time.
    /// </summary>
    /// <param name="lastState">Settlement state when tab was hidden</param>
    /// <param name="elapsedTime">Time elapsed since tab was hidden</param>
    /// <param name="actionCatalog">Available actions for NPCs</param>
    /// <returns>Updated settlement with offline progress applied</returns>
    public OfflineProgressResult CalculateProgress(
        Settlement lastState,
        TimeSpan elapsedTime,
        ActionCatalog actionCatalog)
    {
        // No catch-up for very short absences (< 1 minute)
        if (elapsedTime.TotalSeconds < MinOfflineSecondsForCalculation)
        {
            return new OfflineProgressResult(
                UpdatedSettlement: lastState,
                ActionsCompleted: new Dictionary<string, int>(),
                TotalGoldEarned: 0,
                TotalReputationEarned: 0,
                CappedElapsedTime: elapsedTime
            );
        }

        // Cap offline time to 24 hours maximum
        var cappedElapsed = TimeSpan.FromSeconds(
            Math.Min(elapsedTime.TotalSeconds, MaxOfflineSeconds)
        );

        var updatedNPCs = new List<NPC>();
        var actionsCompleted = new Dictionary<string, int>();
        var totalGold = 0;
        var totalReputation = 0;

        foreach (var npc in lastState.NPCs)
        {
            // Clear possession state - player is no longer controlling this NPC
            if (npc.IsPossessed)
            {
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

            // Calculate offline progress for autonomous NPCs
            var result = CalculateNPCOfflineProgress(npc, cappedElapsed, actionCatalog, lastState);
            updatedNPCs.Add(result.UpdatedNPC);
            actionsCompleted[npc.Name] = result.CycleCount;
            totalGold += result.GoldEarned;
            totalReputation += result.ReputationEarned;
        }

        var updatedSettlement = lastState with
        {
            NPCs = updatedNPCs,
            WorldTime = DateTime.UtcNow
        };

        return new OfflineProgressResult(
            UpdatedSettlement: updatedSettlement,
            ActionsCompleted: actionsCompleted,
            TotalGoldEarned: totalGold,
            TotalReputationEarned: totalReputation,
            CappedElapsedTime: cappedElapsed
        );
    }

    /// <summary>
    /// Calculate offline progress for a single autonomous NPC.
    /// </summary>
    private NPCOfflineResult CalculateNPCOfflineProgress(
        NPC npc,
        TimeSpan elapsed,
        ActionCatalog actionCatalog,
        Settlement settlement)
    {
        // Get the primary action this NPC performs autonomously
        var action = GetPrimaryActionForNPC(npc, actionCatalog, settlement);

        if (action == null)
        {
            // NPC has no default action (e.g., Customer NPCs)
            return new NPCOfflineResult(
                UpdatedNPC: npc with { State = NPCState.Idle },
                CycleCount: 0,
                GoldEarned: 0,
                ReputationEarned: 0
            );
        }

        // Calculate number of complete action cycles
        var cycleCount = (int)(elapsed.TotalSeconds / action.DurationSeconds);

        // Apply rewards
        var goldReward = cycleCount * action.Rewards.GetValueOrDefault("Gold", 0);
        var reputationReward = cycleCount * action.Rewards.GetValueOrDefault("Reputation", 0);

        var updatedNPC = npc with
        {
            Gold = npc.Gold + goldReward,
            Reputation = npc.Reputation + reputationReward,
            State = NPCState.Idle, // Reset to idle state
            CurrentAction = null,
            ActionStartTime = null,
            ActionDurationSeconds = null
        };

        return new NPCOfflineResult(
            UpdatedNPC: updatedNPC,
            CycleCount: cycleCount,
            GoldEarned: goldReward,
            ReputationEarned: reputationReward
        );
    }

    /// <summary>
    /// Determine the primary action an NPC performs autonomously.
    /// Matches NPCAIService action selection logic.
    /// </summary>
    private NPCAction? GetPrimaryActionForNPC(
        NPC npc,
        ActionCatalog catalog,
        Settlement settlement)
    {
        return npc.ClassName switch
        {
            "Innkeeper" => catalog.ServeCustomer,
            "Blacksmith" => HasSufficientResources(npc, settlement, "IronOre", 2)
                ? catalog.CraftSword
                : null,
            "Cook" => catalog.PrepareMeal,
            "Customer" => null, // Customers don't work autonomously
            _ => null
        };
    }

    /// <summary>
    /// Check if NPC's building has sufficient resources for an action.
    /// </summary>
    private bool HasSufficientResources(
        NPC npc,
        Settlement settlement,
        string resourceType,
        int requiredAmount)
    {
        var building = settlement.Buildings
            .FirstOrDefault(b => b.Id == npc.CurrentBuilding);

        if (building == null) return false;

        return building.Resources.GetValueOrDefault(resourceType, 0) >= requiredAmount;
    }
}

/// <summary>
/// Result of offline progress calculation for entire settlement.
/// </summary>
public sealed record OfflineProgressResult(
    Settlement UpdatedSettlement,
    IReadOnlyDictionary<string, int> ActionsCompleted, // NPC Name → Action Count
    int TotalGoldEarned,
    int TotalReputationEarned,
    TimeSpan CappedElapsedTime
);

/// <summary>
/// Result of offline progress calculation for a single NPC.
/// </summary>
internal sealed record NPCOfflineResult(
    NPC UpdatedNPC,
    int CycleCount,
    int GoldEarned,
    int ReputationEarned
);
