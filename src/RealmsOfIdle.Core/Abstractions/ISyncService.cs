using RealmsOfIdle.Core.Domain;

namespace RealmsOfIdle.Core.Abstractions;

/// <summary>
/// Sync service interface for synchronizing game state between devices/sessions
/// </summary>
public interface ISyncService
{
    /// <summary>
    /// Register a new sync session
    /// </summary>
    Task<SyncSession> RegisterSessionAsync(string playerId, string clientId);

    /// <summary>
    /// Sync game state for a session
    /// </summary>
    Task<SyncResult> SyncGameStateAsync(string sessionId);

    /// <summary>
    /// Push local changes to server
    /// </summary>
    Task<SyncResult> PushChangesAsync(string sessionId, IEnumerable<GameEvent> changes);

    /// <summary>
    /// Pull remote changes to client
    /// </summary>
    Task<SyncResult> PullChangesAsync(string sessionId);

    /// <summary>
    /// Resolve conflicts between local and remote state
    /// </summary>
    Task<SyncResult> ResolveConflictsAsync(string sessionId, ConflictResolutionStrategy strategy);

    /// <summary>
    /// Get sync status for a session
    /// </summary>
    Task<SyncStatus> GetSyncStatusAsync(string sessionId);

    /// <summary>
    /// Disconnect sync session
    /// </summary>
    Task DisconnectSessionAsync(string sessionId);

    /// <summary>
    /// Check if a player has pending sync conflicts
    /// </summary>
    Task<bool> HasConflictsAsync(string playerId);

    /// <summary>
    /// Get sync history
    /// </summary>
    Task<IEnumerable<SyncRecord>> GetSyncHistoryAsync(string playerId, int limit = 50);

    /// <summary>
    /// Optimize sync storage
    /// </summary>
    Task OptimizeSyncStorageAsync();
}

/// <summary>
/// Sync session information
/// </summary>
public record SyncSession(
    string Id,
    string PlayerId,
    string ClientId,
    DateTime CreatedAt,
    DateTime LastActivity,
    SyncStatus Status);

/// <summary>
/// Sync result information
/// </summary>
public record SyncResult(
    bool Success,
    int ChangesPushed,
    int ChangesPulled,
    int ConflictsResolved,
    string? ErrorMessage,
    DateTime Timestamp);

/// <summary>
/// Sync status enumeration
/// </summary>
public enum SyncStatus
{
    Connected,
    Syncing,
    Conflicted,
    Disconnected,
    Error,
    Optimizing
}

/// <summary>
/// Conflict resolution strategy
/// </summary>
public enum ConflictResolutionStrategy
{
    ServerWins,
    ClientWins,
    Merge,
    Manual
}

/// <summary>
/// Sync record model
/// </summary>
public record SyncRecord(
    string Id,
    string SessionId,
    DateTime Timestamp,
    SyncStatus Status,
    int ChangesPushed,
    int ChangesPulled,
    int ConflictsResolved,
    long DurationMs);
