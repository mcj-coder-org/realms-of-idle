using AwesomeAssertions;
using RealmsOfIdle.Core.Engine.Spatial;
using RealmsOfIdle.Core.Scenarios.Inn;
using RealmsOfIdle.Core.Scenarios.Inn.Persistence;

namespace RealmsOfIdle.Client.Shared.Tests.Services;

[Trait("Category", "Unit")]
public sealed class InnStateDtoSerializationTests
{
    [Fact]
    public void FromDomainAndToDomain_RoundTrips_AllFields()
    {
        // Arrange - Create a complex InnState
        var layout = new WorldLayout();
        var area = new SceneArea("main_hall", "Main Hall", 10, 10);
        area.Grid.SetTile(1, 1, TileType.Floor);
        area.Grid.SetTile(2, 2, TileType.Wall);
        area.AddDoorTile(new GridPosition(3, 3));
        layout.AddArea(area);

        var facilities = new Dictionary<string, InnFacility>
        {
            { "kitchen", new InnFacility("Kitchen", Level: 3, Capacity: 4, ProductionRate: 1.728, UpgradeCost: 337) },
            { "bar", new InnFacility("Bar", Level: 2, Capacity: 3, ProductionRate: 0.6, UpgradeCost: 225) },
            { "tables", new InnFacility("Tables", Level: 1, Capacity: 4, ProductionRate: 0, UpgradeCost: 200) },
        };

        var customer1 = new Customer("Alice")
            .WithState(CustomerState.Eating)
            .WithOrder(new CustomerOrder("Roast Chicken", 15))
            .AdvanceEatingProgress(0.5)
            .WithPosition(new EntityPosition("table_1"))
            .WithPaymentAmount(15);

        var customer2 = new Customer("Bob")
            .WithState(CustomerState.Waiting)
            .WithPosition(new EntityPosition("entrance", "lobby", 0.3));

        var staff1 = new StaffMember("Chef Gordon", "Cook")
            .WithEfficiency(1.5)
            .WithFatigue(0.3)
            .WithPosition(new EntityPosition("kitchen_1"))
            .WithTask(new StaffTask(StaffTaskType.Cook, "order_1"));

        var staff2 = new StaffMember("Server Lisa", "Server")
            .WithFatigue(0.7)
            .WithPosition(new EntityPosition("table_2", "bar_counter", 0.6))
            .WithTask(new StaffTask(StaffTaskType.Serve, "Alice"));

        var state = new InnState(
            layout,
            facilities,
            new List<Customer> { customer1, customer2 },
            new List<StaffMember> { staff1, staff2 },
            Gold: 500,
            Reputation: 25,
            InnLevel: 2
        );

        // Act - Round-trip through DTO
        var dto = InnStateDto.FromDomain(state, "test-player", currentTick: 42);
        var restored = dto.ToDomain();

        // Assert - Verify all scalar fields
        restored.Gold.Should().Be(500);
        restored.Reputation.Should().Be(25);
        restored.InnLevel.Should().Be(2);

        // Verify facilities
        restored.Facilities.Count.Should().Be(3);
        restored.Facilities.Should().ContainKey("kitchen");
        restored.Facilities["kitchen"].Type.Should().Be("Kitchen");
        restored.Facilities["kitchen"].Level.Should().Be(3);
        restored.Facilities["kitchen"].Capacity.Should().Be(4);
        restored.Facilities["kitchen"].ProductionRate.Should().Be(1.728);
        restored.Facilities["kitchen"].UpgradeCost.Should().Be(337);

        restored.Facilities["bar"].Type.Should().Be("Bar");
        restored.Facilities["bar"].Level.Should().Be(2);
        restored.Facilities["bar"].Capacity.Should().Be(3);

        // Verify customers
        restored.Customers.Count.Should().Be(2);

        var restoredAlice = restored.Customers.First(c => c.Name == "Alice");
        restoredAlice.State.Should().Be(CustomerState.Eating);
        restoredAlice.Order.Should().NotBeNull();
        restoredAlice.Order!.ItemName.Should().Be("Roast Chicken");
        restoredAlice.Order.Price.Should().Be(15);
        restoredAlice.EatingProgress.Should().Be(0.5);
        restoredAlice.Position.Should().NotBeNull();
        restoredAlice.Position!.CurrentNode.Should().Be("table_1");
        restoredAlice.PaymentAmount.Should().Be(15);

        var restoredBob = restored.Customers.First(c => c.Name == "Bob");
        restoredBob.State.Should().Be(CustomerState.Waiting);
        restoredBob.Position.Should().NotBeNull();
        restoredBob.Position!.CurrentNode.Should().Be("entrance");
        restoredBob.Position.TargetNode.Should().Be("lobby");
        restoredBob.Position.TravelProgress.Should().Be(0.3);

        // Verify staff
        restored.Staff.Count.Should().Be(2);

        var restoredGordon = restored.Staff.First(s => s.Name == "Chef Gordon");
        restoredGordon.Role.Should().Be("Cook");
        restoredGordon.Efficiency.Should().Be(1.5);
        restoredGordon.Fatigue.Should().Be(0.3);
        restoredGordon.Position.Should().NotBeNull();
        restoredGordon.Position!.CurrentNode.Should().Be("kitchen_1");
        restoredGordon.CurrentTask.Should().NotBeNull();
        restoredGordon.CurrentTask!.Type.Should().Be(StaffTaskType.Cook);
        restoredGordon.CurrentTask.TargetId.Should().Be("order_1");

        var restoredLisa = restored.Staff.First(s => s.Name == "Server Lisa");
        restoredLisa.Role.Should().Be("Server");
        restoredLisa.Fatigue.Should().Be(0.7);
        restoredLisa.Position.Should().NotBeNull();
        restoredLisa.Position!.CurrentNode.Should().Be("table_2");
        restoredLisa.Position.TargetNode.Should().Be("bar_counter");
        restoredLisa.Position.TravelProgress.Should().Be(0.6);
        restoredLisa.CurrentTask.Should().NotBeNull();
        restoredLisa.CurrentTask!.Type.Should().Be(StaffTaskType.Serve);
        restoredLisa.CurrentTask.TargetId.Should().Be("Alice");

        // Verify layout
        restored.Layout.Should().NotBeNull();
        restored.Layout.Areas.Count.Should().Be(1);
        restored.Layout.Areas[0].Id.Should().Be("main_hall");
        restored.Layout.Areas[0].Name.Should().Be("Main Hall");
        restored.Layout.Areas[0].Grid.Width.Should().Be(10);
        restored.Layout.Areas[0].Grid.Height.Should().Be(10);

        // Verify DTO metadata
        dto.PlayerId.Should().Be("test-player");
        dto.CurrentTick.Should().Be(42);
    }

    [Fact]
    public void FromDomainAndToDomain_EmptyState_RoundTrips()
    {
        // Arrange - Minimal state
        var layout = new WorldLayout();
        var facilities = new Dictionary<string, InnFacility>();
        var state = new InnState(layout, facilities);

        // Act
        var dto = InnStateDto.FromDomain(state, "empty-player");
        var restored = dto.ToDomain();

        // Assert
        restored.Gold.Should().Be(0);
        restored.Reputation.Should().Be(0);
        restored.InnLevel.Should().Be(1);
        restored.Customers.Should().BeEmpty();
        restored.Staff.Should().BeEmpty();
        restored.Facilities.Should().BeEmpty();
        restored.Layout.Areas.Should().BeEmpty();
    }

    [Fact]
    public void FromDomainAndToDomain_StaffWithNoTask_RoundTrips()
    {
        // Arrange - Staff member with no task and no position
        var layout = new WorldLayout();
        var facilities = new Dictionary<string, InnFacility>();
        var idleStaff = new StaffMember("Idle Worker", "Cleaner");

        var state = new InnState(
            layout,
            facilities,
            Array.Empty<Customer>(),
            new List<StaffMember> { idleStaff },
            Gold: 0,
            Reputation: 0,
            InnLevel: 1
        );

        // Act
        var dto = InnStateDto.FromDomain(state, "test-player");
        var restored = dto.ToDomain();

        // Assert
        restored.Staff.Count.Should().Be(1);
        var restoredStaff = restored.Staff[0];
        restoredStaff.Name.Should().Be("Idle Worker");
        restoredStaff.Role.Should().Be("Cleaner");
        restoredStaff.Efficiency.Should().Be(1.0);
        restoredStaff.Fatigue.Should().Be(0.0);
        restoredStaff.CurrentTask.Should().BeNull();
        restoredStaff.Position.Should().BeNull();
    }

    [Fact]
    public void FromDomainAndToDomain_CustomerSatisfaction_TruncatedByIntCast()
    {
        // Arrange - Customer with fractional satisfaction (DTO stores as int, domain uses double)
        var layout = new WorldLayout();
        var facilities = new Dictionary<string, InnFacility>();

        var customer = new Customer("Happy Guest")
            .WithState(CustomerState.Seated)
            .IncreaseSatisfaction(0.8);

        var state = new InnState(
            layout,
            facilities,
            new List<Customer> { customer },
            Array.Empty<StaffMember>(),
            Gold: 0,
            Reputation: 0,
            InnLevel: 1
        );

        // Act
        var dto = InnStateDto.FromDomain(state, "test-player");

        // The DTO casts satisfaction to int, so 0.8 becomes 0
        // This is a known behavior of the current DTO design
        dto.Customers[0].Satisfaction.Should().Be(0);

        var restored = dto.ToDomain();

        // Assert - After round-trip, satisfaction reflects the int truncation
        restored.Customers[0].Satisfaction.Should().Be(0.0);
        restored.Customers[0].Name.Should().Be("Happy Guest");
        restored.Customers[0].State.Should().Be(CustomerState.Seated);
    }
}
