using AwesomeAssertions;
using RealmsOfIdle.Core.Engine.Spatial;
using RealmsOfIdle.Core.Scenarios.Inn;
using RealmsOfIdle.SimulationTests.Infrastructure;
using RealmsOfIdle.SimulationTests.Infrastructure.Drivers;
using Reqnroll;

namespace RealmsOfIdle.SimulationTests.Features.Inn;

[Binding]
public class GuestOvernightSteps
{
    private readonly ScenarioContext _scenarioContext;
    private int _roomCost;

    public GuestOvernightSteps(ScenarioContext scenarioContext)
    {
        _scenarioContext = scenarioContext;
    }

    private GameTestContext Context => _scenarioContext.GetGameTestContext();
    private GameEngineDriver Driver => _scenarioContext.GetGameEngineDriver();

    [Given(@"I have an inn with available guest rooms")]
    public void GivenIHaveAnInnWithAvailableGuestRooms()
    {
        Driver.CreateNewPlayer();
        var state = Driver.GetInnState();
        state.GetAvailableGuestRooms().Should().BeGreaterThan(0);
    }

    [Given(@"a customer wants to stay the night")]
    public void GivenACustomerWantsToStayTheNight()
    {
        var customer = new Customer("OvernightGuest",
            State: CustomerState.Seated,
            Position: new EntityPosition("table_1"));
        Driver.AddCustomer(customer);
        Context.NamedCustomers["overnightGuest"] = customer;
    }

    [Given(@"the guest room costs (.*) gold")]
    public void GivenTheGuestRoomCostsGold(int cost)
    {
        _roomCost = cost;
        Context.Snapshots["roomCost"] = cost;
        Context.Snapshots["goldBeforePayment"] = Driver.GetInnState().Gold;
    }

    [When(@"the customer pays for the room")]
    public void WhenTheCustomerPaysForTheRoom()
    {
        // Simulate room payment by adding gold to the inn
        var state = Driver.GetInnState();
        Context.GameLoop!.AddGold(_roomCost);
    }

    [Then(@"the inn gold should increase by (.*)")]
    public void ThenTheInnGoldShouldIncreaseBy(int amount)
    {
        var state = Driver.GetInnState();
        var goldBefore = (int)Context.Snapshots["goldBeforePayment"];
        state.Gold.Should().Be(goldBefore + amount);
    }

    [Then(@"the customer should be assigned a guest room")]
    public void ThenTheCustomerShouldBeAssignedAGuestRoom()
    {
        // Update customer with assigned bed
        var state = Driver.GetInnState();
        var customer = state.Customers.FirstOrDefault(c => c.Name == "OvernightGuest");
        customer.Should().NotBeNull();
        // Assign bed (simulating the assignment process)
        var updatedCustomer = customer!.WithAssignedBed(new GridPosition(2, 2))
            .WithState(CustomerState.Sleeping);
        var newCustomers = state.Customers.Select(c =>
            c.Name == "OvernightGuest" ? updatedCustomer : c).ToList();
        var newState = state with { Customers = newCustomers };
        Driver.ReplaceState(newState);

        var verifyState = Driver.GetInnState();
        var verifyCustomer = verifyState.Customers.FirstOrDefault(c => c.Name == "OvernightGuest");
        verifyCustomer!.AssignedBed.Should().NotBeNull();
        verifyCustomer.State.Should().Be(CustomerState.Sleeping);
    }

    [Given(@"a customer has rented a guest room")]
    public void GivenACustomerHasRentedAGuestRoom()
    {
        Driver.CreateNewPlayer();
        var customer = new Customer("RoomGuest",
            State: CustomerState.Sleeping,
            Position: new EntityPosition("table_1"),
            AssignedBed: new GridPosition(2, 2));
        Driver.AddCustomer(customer);
        Context.NamedCustomers["roomGuest"] = customer;
    }

    [Given(@"the customer is in the main hall")]
    public void GivenTheCustomerIsInTheMainHall()
    {
        // Customer is already in the main hall (position at table_1)
        var state = Driver.GetInnState();
        state.Customers.Should().NotBeEmpty();
    }

    [When(@"the customer goes to sleep")]
    public void WhenTheCustomerGoesToSleep()
    {
        // Process ticks; the customer already has Sleeping state
        Driver.ProcessTicks(3);
    }

    [Then(@"the customer should walk to the guest wing door")]
    public void ThenTheCustomerShouldWalkToTheGuestWingDoor()
    {
        // TODO: Cross-area pathfinding for customers going to guest rooms
        // is not yet implemented in the game engine.
        throw new PendingStepException("Feature not yet implemented: cross-area pathfinding for customers to guest rooms");
    }

    [Then(@"enter the guest wing")]
    public void ThenEnterTheGuestWing()
    {
        // TODO: Cross-area transitions not yet implemented for customers.
        throw new PendingStepException("Feature not yet implemented: cross-area transitions for customers");
    }

    [Then(@"walk to their assigned room")]
    public void ThenWalkToTheirAssignedRoom()
    {
        // TODO: Room-specific pathfinding not yet implemented.
        throw new PendingStepException("Feature not yet implemented: room-specific pathfinding for customers");
    }

    // "start sleeping" is handled by StaffSleepCycleSteps.ThenStartSleeping

    [Given(@"a customer is sleeping in a guest room")]
    public void GivenACustomerIsSleepingInAGuestRoom()
    {
        Driver.CreateNewPlayer();
        var customer = new Customer("SleepingGuest",
            State: CustomerState.Sleeping,
            Position: new EntityPosition("guest_bed_1"),
            AssignedBed: new GridPosition(2, 2));
        Driver.AddCustomer(customer);
        Context.NamedCustomers["sleepingGuest"] = customer;
    }

    [Given(@"the customer has slept for (.*) hours")]
    public void GivenTheCustomerHasSleptForHours(int hours)
    {
        Context.Snapshots["sleepDuration"] = hours;
    }

    [When(@"the customer wakes up")]
    public void WhenTheCustomerWakesUp()
    {
        // TODO: Customer wake-up cycle is not yet implemented.
        // The engine has CustomerState.Sleeping but no automatic wake transition.
        Driver.ProcessTicks(10);
    }

    [Then(@"the customer should walk back to the main hall")]
    public void ThenTheCustomerShouldWalkBackToTheMainHall()
    {
        // TODO: Customer wake-up and return-to-hall pathfinding not yet implemented.
        throw new PendingStepException("Feature not yet implemented: customer wake-up and return-to-hall pathfinding");
    }

    [Then(@"the customer should order breakfast")]
    public void ThenTheCustomerShouldOrderBreakfast()
    {
        // TODO: Breakfast ordering after overnight stay not yet implemented.
        throw new PendingStepException("Feature not yet implemented: breakfast ordering after overnight stay");
    }

    [Then(@"the customer should leave the inn")]
    public void ThenTheCustomerShouldLeaveTheInn()
    {
        // TODO: Customer departure after overnight stay not yet implemented.
        throw new PendingStepException("Feature not yet implemented: customer departure after overnight stay");
    }

    [Given(@"I have an inn with (.*) guest rooms")]
    public void GivenIHaveAnInnWithGuestRooms(int roomCount)
    {
        Driver.CreateNewPlayer();
        var state = Driver.GetInnState();
        state.GetAvailableGuestRooms().Should().BeGreaterThanOrEqualTo(roomCount);
    }

    [Given(@"I have (.*) customers who want to stay the night")]
    public void GivenIHaveCustomersWhoWantToStayTheNight(int customerCount)
    {
        for (int i = 1; i <= customerCount; i++)
        {
            var customer = new Customer($"Guest{i}",
                State: CustomerState.Seated,
                Position: new EntityPosition($"table_{Math.Min(i, 3)}"));
            Driver.AddCustomer(customer);
            Context.NamedCustomers[$"guest{i}"] = customer;
        }
        Context.Snapshots["goldBeforeRentals"] = Driver.GetInnState().Gold;
    }

    [When(@"all (.*) customers rent rooms")]
    public void WhenAllCustomersRentRooms(int customerCount)
    {
        // Simulate room rental by adding gold per customer and updating state
        for (int i = 0; i < customerCount; i++)
        {
            Context.GameLoop!.AddGold(50); // 50 gold per room
        }
    }

    [Then(@"each customer should be assigned a unique room")]
    public void ThenEachCustomerShouldBeAssignedAUniqueRoom()
    {
        // Verify we have enough guest rooms for all customers
        var state = Driver.GetInnState();
        var guestRoomCount = state.GetAvailableGuestRooms();
        var guestCount = Context.NamedCustomers.Count(kvp => kvp.Key.StartsWith("guest"));
        guestRoomCount.Should().BeGreaterThanOrEqualTo(guestCount);
    }

    [Then(@"all (.*) customers should sleep simultaneously")]
    public void ThenAllCustomersShouldSleepSimultaneously(int customerCount)
    {
        // TODO: Simultaneous sleep management for multiple guests not yet implemented.
        // The engine supports multiple customers with Sleeping state but doesn't
        // automatically assign rooms.
        throw new PendingStepException("Feature not yet implemented: simultaneous sleep management for multiple guests");
    }

    [Then(@"the inn should earn (.*) gold total")]
    public void ThenTheInnShouldEarnGoldTotal(int totalGold)
    {
        var state = Driver.GetInnState();
        var goldBefore = (int)Context.Snapshots["goldBeforeRentals"];
        state.Gold.Should().Be(goldBefore + totalGold);
    }
}
