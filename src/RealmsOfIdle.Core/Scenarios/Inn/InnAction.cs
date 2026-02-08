namespace RealmsOfIdle.Core.Scenarios.Inn;

/// <summary>
/// Common action type constants for Inn actions
/// </summary>
public static class InnActionTypes
{
    public const string Cook = "Cook";
    public const string Serve = "Serve";
    public const string TendBar = "TendBar";
    public const string SeatGuest = "SeatGuest";
    public const string Clean = "Clean";
    public const string UpgradeKitchen = "UpgradeKitchen";
    public const string UpgradeBar = "UpgradeBar";
    public const string UpgradeRooms = "UpgradeRooms";
    public const string AddGuestRoom = "AddGuestRoom";
    public const string AddStaffBed = "AddStaffBed";
    public const string Advertise = "Advertise";
}

/// <summary>
/// Represents an action that can be performed in the Inn scenario
/// </summary>
public sealed record class InnAction(
    string Type,
    string? TargetId = null,
    Dictionary<string, object>? Parameters = null)
{
    /// <summary>
    /// Factory method for creating a Cook action
    /// </summary>
    public static InnAction Cook(string kitchenId) =>
        new(InnActionTypes.Cook, kitchenId);

    /// <summary>
    /// Factory method for creating a Serve action
    /// </summary>
    public static InnAction Serve(string tableId, string customerId) =>
        new(InnActionTypes.Serve, tableId, new Dictionary<string, object> { { "customerId", customerId } });

    /// <summary>
    /// Factory method for creating a TendBar action
    /// </summary>
    public static InnAction TendBar(string barId) =>
        new(InnActionTypes.TendBar, barId);

    /// <summary>
    /// Factory method for creating a SeatGuest action
    /// </summary>
    public static InnAction SeatGuest(string tableId, string customerId) =>
        new(InnActionTypes.SeatGuest, tableId, new Dictionary<string, object> { { "customerId", customerId } });

    /// <summary>
    /// Factory method for creating a Clean action
    /// </summary>
    public static InnAction Clean(string areaId) =>
        new(InnActionTypes.Clean, areaId);

    /// <summary>
    /// Factory method for creating an UpgradeKitchen action
    /// </summary>
    public static InnAction UpgradeKitchen() =>
        new(InnActionTypes.UpgradeKitchen);

    /// <summary>
    /// Factory method for creating an UpgradeBar action
    /// </summary>
    public static InnAction UpgradeBar() =>
        new(InnActionTypes.UpgradeBar);

    /// <summary>
    /// Factory method for creating an UpgradeRooms action
    /// </summary>
    public static InnAction UpgradeRooms() =>
        new(InnActionTypes.UpgradeRooms);

    /// <summary>
    /// Factory method for creating an AddGuestRoom action
    /// </summary>
    public static InnAction AddGuestRoom() =>
        new(InnActionTypes.AddGuestRoom);

    /// <summary>
    /// Factory method for creating an AddStaffBed action
    /// </summary>
    public static InnAction AddStaffBed() =>
        new(InnActionTypes.AddStaffBed);

    /// <summary>
    /// Factory method for creating an Advertise action
    /// </summary>
    public static InnAction Advertise() =>
        new(InnActionTypes.Advertise);
}
