namespace RealmsOfIdle.Core.Scenarios.Inn;

/// <summary>
/// Represents a food order placed by a customer
/// </summary>
public sealed record class CustomerOrder(
    string ItemName,
    int Price)
{
}
