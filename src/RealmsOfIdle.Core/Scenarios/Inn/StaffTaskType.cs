namespace RealmsOfIdle.Core.Scenarios.Inn;

/// <summary>
/// Represents the type of task a staff member is performing
/// </summary>
public enum StaffTaskType
{
    /// <summary>No task assigned (idle)</summary>
    None,

    /// <summary>Cooking food in the kitchen</summary>
    Cook,

    /// <summary>Serving customers at tables</summary>
    Serve,

    /// <summary>Tending the bar</summary>
    TendBar,

    /// <summary>Seating guests at tables</summary>
    SeatGuest,

    /// <summary>Cleaning the inn</summary>
    Clean,

    /// <summary>Sleeping in designated bed</summary>
    Sleep
}
