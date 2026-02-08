namespace RealmsOfIdle.Core.Scenarios.Inn;

/// <summary>
/// Represents the current state of a customer in the Inn
/// </summary>
public enum CustomerState
{
    /// <summary>Customer is arriving at the Inn entrance</summary>
    Arriving,

    /// <summary>Customer is waiting for a table</summary>
    Waiting,

    /// <summary>Customer is seated and waiting to order</summary>
    Seated,

    /// <summary>Customer is waiting for their food</summary>
    WaitingForFood,

    /// <summary>Customer is eating</summary>
    Eating,

    /// <summary>Customer has finished eating and is leaving</summary>
    Leaving,

    /// <summary>Customer is sleeping in a guest room</summary>
    Sleeping,

    /// <summary>Customer has left the Inn</summary>
    Gone
}
