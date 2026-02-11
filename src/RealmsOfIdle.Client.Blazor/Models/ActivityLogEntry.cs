using LiteDB;

namespace RealmsOfIdle.Client.Blazor.Models;

/// <summary>
/// Record of NPC action for display in activity log UI
/// Persisted in LiteDB "activityLog" collection
/// </summary>
public sealed record ActivityLogEntry(
    ObjectId Id,
    DateTime Timestamp,
    string ActorId,
    string ActorName,
    string ActionName,
    string Result)
{
    /// <summary>
    /// Factory method for creating new entries (Id assigned on insert by LiteDB)
    /// </summary>
    public static ActivityLogEntry Create(
        string actorId,
        string actorName,
        string actionName,
        string result)
    {
        return new ActivityLogEntry(
            Id: ObjectId.Empty,  // LiteDB assigns on insert
            Timestamp: DateTime.UtcNow,
            ActorId: actorId,
            ActorName: actorName,
            ActionName: actionName,
            Result: result
        );
    }
}
