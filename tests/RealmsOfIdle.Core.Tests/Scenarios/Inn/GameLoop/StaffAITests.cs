using RealmsOfIdle.Core.Engine.Spatial;
using RealmsOfIdle.Core.Scenarios.Inn;
using Xunit;

namespace RealmsOfIdle.Core.Tests.Scenarios.Inn.GameLoop;

/// <summary>
/// Unit tests for StaffAI
/// </summary>
[Trait("Category", "Unit")]
public class StaffAITests
{
    [Fact]
    public void DecideAction_IdleStaffWithWaitingCustomer_ReturnsSeat()
    {
        // Arrange
        var staff = new StaffMember("Barbara", "Waitress");
        var state = CreateTestStateWithWaitingCustomer();

        // Act
        var action = StaffAI.DecideAction(staff, state);

        // Assert
        Assert.Equal(StaffTaskType.SeatGuest, action?.Type);
    }

    [Fact]
    public void DecideAction_IdleStaffWithSeatedCustomer_ReturnsServe()
    {
        // Arrange
        var staff = new StaffMember("Barbara", "Waitress");
        var state = CreateTestStateWithSeatedCustomer();

        // Act
        var action = StaffAI.DecideAction(staff, state);

        // Assert
        Assert.Equal(StaffTaskType.Serve, action?.Type);
    }

    [Fact]
    public void DecideAction_Cook_ReturnsCook()
    {
        // Arrange
        var staff = new StaffMember("Tom", "Cook");
        var state = CreateTestState();

        // Act
        var action = StaffAI.DecideAction(staff, state);

        // Assert
        Assert.Equal(StaffTaskType.Cook, action?.Type);
    }

    [Fact]
    public void DecideAction_TiredStaff_ReturnsSleep()
    {
        // Arrange
        var staff = new StaffMember("Barbara", "Waitress").WithFatigue(0.8).WithDesignatedBed(new GridPosition(1, 1));
        var state = CreateTestState();

        // Act
        var action = StaffAI.DecideAction(staff, state);

        // Assert
        Assert.Equal(StaffTaskType.Sleep, action?.Type);
    }

    [Fact]
    public void DecideAction_SleepingStaff_ContinuesSleep()
    {
        // Arrange
        var staff = new StaffMember("Barbara", "Waitress", new StaffTask(StaffTaskType.Sleep), 1.0, null, new GridPosition(1, 0), 0.8);
        var state = CreateTestState();

        // Act
        var action = StaffAI.DecideAction(staff, state);

        // Assert
        Assert.Equal(StaffTaskType.Sleep, action?.Type);
    }

    [Fact]
    public void DecideAction_RestedStaff_ReturnsIdle()
    {
        // Arrange
        var staff = new StaffMember("Barbara", "Waitress").WithFatigue(0.2);
        var state = CreateTestState();

        // Act
        var action = StaffAI.DecideAction(staff, state);

        // Assert
        Assert.Null(action);
    }

    private static InnState CreateTestState()
    {
        var layout = new WorldLayout();
        var facilities = new Dictionary<string, InnFacility>
        {
            { "kitchen", new InnFacility("Kitchen", 1, 1, 1.0, 100) },
            { "table_1", new InnFacility("Table", 1, 1, 0, 0) },
            { "table_2", new InnFacility("Table", 1, 1, 0, 0) }
        };
        return new InnState(layout, facilities);
    }

    private static InnState CreateTestStateWithWaitingCustomer()
    {
        var state = CreateTestState();
        var customer = new Customer("Hero").WithState(CustomerState.Waiting);
        return state.AddCustomer(customer);
    }

    private static InnState CreateTestStateWithSeatedCustomer()
    {
        var state = CreateTestState();
        var customer = new Customer("Hero")
            .WithState(CustomerState.Seated);
        return state.AddCustomer(customer);
    }

    private static SceneGraph CreateTestGraph()
    {
        var graph = new SceneGraph();
        graph.AddNode(new SceneNode("kitchen", new GridPosition(5, 5), "main_hall"));
        graph.AddNode(new SceneNode("table_1", new GridPosition(3, 3), "main_hall"));
        graph.AddNode(new SceneNode("table_2", new GridPosition(7, 3), "main_hall"));
        graph.AddNode(new SceneNode("entrance", new GridPosition(10, 0), "main_hall"));
        graph.AddNode(new SceneNode("staff_bed_1", new GridPosition(1, 1), "staff_quarters"));
        return graph;
    }
}
