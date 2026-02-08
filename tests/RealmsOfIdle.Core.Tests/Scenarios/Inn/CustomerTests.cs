using RealmsOfIdle.Core.Engine.Spatial;
using RealmsOfIdle.Core.Scenarios.Inn;
using Xunit;

namespace RealmsOfIdle.Core.Tests.Scenarios.Inn;

/// <summary>
/// Unit tests for Customer
/// </summary>
[Trait("Category", "Unit")]
public class CustomerTests
{
    [Fact]
    public void Constructor_WithName_CreatesCustomer()
    {
        // Arrange
        const string name = "Adventurer";

        // Act
        var customer = new Customer(name);

        // Assert
        Assert.Equal(name, customer.Name);
        Assert.Equal(CustomerState.Arriving, customer.State);
        Assert.Equal(0.0, customer.Satisfaction);
        Assert.Null(customer.Order);
        Assert.Null(customer.AssignedBed);
    }

    [Fact]
    public void SetOrder_SetsOrder()
    {
        // Arrange
        var customer = new Customer("Hero");
        var order = new CustomerOrder("Roast Chicken", 15);

        // Act
        var updated = customer.WithOrder(order);

        // Assert
        Assert.Equal(order, updated.Order);
    }

    [Fact]
    public void SetState_UpdatesState()
    {
        // Arrange
        var customer = new Customer("Hero");

        // Act
        var updated = customer.WithState(CustomerState.Eating);

        // Assert
        Assert.Equal(CustomerState.Eating, updated.State);
    }

    [Fact]
    public void SetPosition_UpdatesPosition()
    {
        // Arrange
        var customer = new Customer("Hero");
        var position = new EntityPosition("entrance");

        // Act
        var updated = customer.WithPosition(position);

        // Assert
        Assert.Equal(position, updated.Position);
    }

    [Fact]
    public void AssignBed_SetsBed()
    {
        // Arrange
        var customer = new Customer("Hero");
        var bed = new GridPosition(2, 3);

        // Act
        var updated = customer.WithAssignedBed(bed);

        // Assert
        Assert.Equal(bed, updated.AssignedBed);
    }

    [Fact]
    public void IncreaseSatisfaction_AddsToSatisfaction()
    {
        // Arrange
        var customer = new Customer("Hero");

        // Act
        var updated = customer.IncreaseSatisfaction(0.2);

        // Assert
        Assert.Equal(0.2, updated.Satisfaction);
    }

    [Fact]
    public void DecreaseSatisfaction_SubtractsFromSatisfaction()
    {
        // Arrange
        var customer = new Customer("Hero");
        var withSatisfaction = customer.IncreaseSatisfaction(0.5);

        // Act
        var updated = withSatisfaction.DecreaseSatisfaction(0.1);

        // Assert
        Assert.Equal(0.4, updated.Satisfaction);
    }

    [Fact]
    public void DecreaseSatisfaction_ClampsAtZero()
    {
        // Arrange
        var customer = new Customer("Hero");

        // Act
        var updated = customer.DecreaseSatisfaction(0.5);

        // Assert
        Assert.Equal(0.0, updated.Satisfaction);
    }

    [Fact]
    public void AdvanceEatingProgress_IncreasesProgress()
    {
        // Arrange
        var customer = new Customer("Hero").WithState(CustomerState.Eating);

        // Act
        var updated = customer.AdvanceEatingProgress(0.1);

        // Assert
        Assert.Equal(0.1, updated.EatingProgress);
    }

    [Fact]
    public void AdvanceEatingProgress_CompletesAtOne()
    {
        // Arrange
        var customer = new Customer("Hero").WithState(CustomerState.Eating);
        var withProgress = customer.AdvanceEatingProgress(0.9);

        // Act
        var updated = withProgress.AdvanceEatingProgress(0.1);

        // Assert
        Assert.Equal(1.0, updated.EatingProgress);
    }

    [Fact]
    public void WithPaymentAmount_SetsPayment()
    {
        // Arrange
        var customer = new Customer("Hero");
        const int payment = 25;

        // Act
        var updated = customer.WithPaymentAmount(payment);

        // Assert
        Assert.Equal(payment, updated.PaymentAmount);
    }

    [Fact]
    public void IsEatingComplete_WhenProgressAtOne_ReturnsTrue()
    {
        // Arrange
        var customer = new Customer("Hero")
            .WithState(CustomerState.Eating)
            .AdvanceEatingProgress(1.0);

        // Act
        var result = customer.IsEatingComplete;

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void IsEatingComplete_WhenProgressBelowOne_ReturnsFalse()
    {
        // Arrange
        var customer = new Customer("Hero")
            .WithState(CustomerState.Eating)
            .AdvanceEatingProgress(0.5);

        // Act
        var result = customer.IsEatingComplete;

        // Assert
        Assert.False(result);
    }
}
