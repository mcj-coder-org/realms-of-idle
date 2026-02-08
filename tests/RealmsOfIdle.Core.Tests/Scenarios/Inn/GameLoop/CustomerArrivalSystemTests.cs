using RealmsOfIdle.Core.Infrastructure;
using RealmsOfIdle.Core.Scenarios.Inn;
using Xunit;

namespace RealmsOfIdle.Core.Tests.Scenarios.Inn.GameLoop;

/// <summary>
/// Unit tests for CustomerArrivalSystem
/// </summary>
[Trait("Category", "Unit")]
public class CustomerArrivalSystemTests
{
    [Fact]
    public void Constructor_WithConfig_CreatesSystem()
    {
        // Arrange
        var rng = new DeterministicRng(42);
        var config = new ArrivalConfig(MinReputation: 0, MaxReputation: 100, BaseInterval: 100);

        // Act
        var system = new CustomerArrivalSystem(config, rng);

        // Assert
        Assert.Equal(0, system.TicksUntilNextArrival);
    }

    [Fact]
    public void ProcessTick_DecrementsTimer()
    {
        // Arrange
        var rng = new DeterministicRng(42);
        var config = new ArrivalConfig(0, 100, 100);
        var system = new CustomerArrivalSystem(config, rng);
        system.CalculateNextArrival(0);

        // Act
        system.ProcessTick();

        // Assert
        Assert.Equal(99, system.TicksUntilNextArrival);
    }

    [Fact]
    public void ProcessTick_WhenTimerReachesZero_ReturnsCustomer()
    {
        // Arrange
        var rng = new DeterministicRng(42);
        var config = new ArrivalConfig(0, 100, 10);
        var system = new CustomerArrivalSystem(config, rng);
        system.CalculateNextArrival(0);

        // Act
        for (int i = 0; i < 10; i++)
        {
            system.ProcessTick();
        }

        // Assert
        Assert.True(system.ShouldArrive);
    }

    [Fact]
    public void GenerateCustomer_CreatesCustomerWithRandomName()
    {
        // Arrange
        var rng = new DeterministicRng(42);
        var config = new ArrivalConfig(0, 100, 100);
        var system = new CustomerArrivalSystem(config, rng);

        // Act
        var customer = system.GenerateCustomer();

        // Assert
        Assert.NotNull(customer);
        Assert.False(string.IsNullOrEmpty(customer.Name));
    }

    [Fact]
    public void CalculateNextArrival_HigherReputation_ShorterInterval()
    {
        // Arrange
        var rng = new DeterministicRng(42);
        var config = new ArrivalConfig(0, 100, 100);
        var system = new CustomerArrivalSystem(config, rng);

        // Act
        system.CalculateNextArrival(reputation: 50);
        var highRepTicks = system.TicksUntilNextArrival;

        system.CalculateNextArrival(reputation: 10);
        var lowRepTicks = system.TicksUntilNextArrival;

        // Assert
        Assert.True(lowRepTicks > highRepTicks);
    }

    [Fact]
    public void CalculateNextArrival_RespectsCapacity()
    {
        // Arrange
        var rng = new DeterministicRng(42);
        var config = new ArrivalConfig(0, 100, 100, MaxCapacity: 2);
        var system = new CustomerArrivalSystem(config, rng);

        // Act
        var result1 = system.CanArrive(currentCustomerCount: 0);
        var result2 = system.CanArrive(currentCustomerCount: 2);
        var result3 = system.CanArrive(currentCustomerCount: 3);

        // Assert
        Assert.True(result1);
        Assert.True(result2);
        Assert.False(result3);
    }
}
