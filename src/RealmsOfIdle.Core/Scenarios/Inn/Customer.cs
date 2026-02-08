using RealmsOfIdle.Core.Engine.Spatial;

namespace RealmsOfIdle.Core.Scenarios.Inn;

/// <summary>
/// Represents a customer at the Inn
/// </summary>
public sealed record class Customer(
    string Name,
    CustomerState State = CustomerState.Arriving,
    CustomerOrder? Order = null,
    double Satisfaction = 0.0,
    int PaymentAmount = 0,
    double EatingProgress = 0.0,
    EntityPosition? Position = null,
    GridPosition? AssignedBed = null)
{
    /// <summary>
    /// Gets whether the customer has finished eating
    /// </summary>
    public bool IsEatingComplete => EatingProgress >= 1.0;

    /// <summary>
    /// Creates a new customer with an order
    /// </summary>
    public Customer WithOrder(CustomerOrder order)
    {
        return this with { Order = order };
    }

    /// <summary>
    /// Creates a new customer with an updated state
    /// </summary>
    public Customer WithState(CustomerState state)
    {
        return this with { State = state };
    }

    /// <summary>
    /// Creates a new customer with an updated position
    /// </summary>
    public Customer WithPosition(EntityPosition position)
    {
        return this with { Position = position };
    }

    /// <summary>
    /// Creates a new customer with an assigned bed
    /// </summary>
    public Customer WithAssignedBed(GridPosition bed)
    {
        return this with { AssignedBed = bed };
    }

    /// <summary>
    /// Creates a new customer with increased satisfaction
    /// </summary>
    public Customer IncreaseSatisfaction(double amount)
    {
        var newSatisfaction = Math.Min(1.0, Satisfaction + amount);
        return this with { Satisfaction = newSatisfaction };
    }

    /// <summary>
    /// Creates a new customer with decreased satisfaction
    /// </summary>
    public Customer DecreaseSatisfaction(double amount)
    {
        var newSatisfaction = Math.Max(0.0, Satisfaction - amount);
        return this with { Satisfaction = newSatisfaction };
    }

    /// <summary>
    /// Creates a new customer with advanced eating progress
    /// </summary>
    public Customer AdvanceEatingProgress(double amount)
    {
        var newProgress = Math.Min(1.0, EatingProgress + amount);
        return this with { EatingProgress = newProgress };
    }

    /// <summary>
    /// Creates a new customer with a payment amount set
    /// </summary>
    public Customer WithPaymentAmount(int payment)
    {
        return this with { PaymentAmount = payment };
    }
}
