using RealmsOfIdle.Client.UI.Components.Game;
using RealmsOfIdle.Core.Engine.Spatial;
using RealmsOfIdle.Core.Scenarios.Inn;

namespace RealmsOfIdle.Client.UI.Tests.Components.Game;

/// <summary>
/// Unit tests for InnGameViewModel
/// </summary>
[Trait("Category", "Unit")]
public class InnGameViewModelTests
{
    [Fact]
    public void Constructor_WithInitialState_CreatesViewModel()
    {
        // Arrange
        var layout = new WorldLayout();
        var facilities = new Dictionary<string, InnFacility>();
        var state = new InnState(layout, facilities);

        // Act
        var viewModel = new InnGameViewModel(state);

        // Assert
        Assert.Equal(0, viewModel.Gold);
        Assert.Equal(1, viewModel.InnLevel);
        Assert.Empty(viewModel.Customers);
        Assert.Empty(viewModel.Staff);
    }

    [Fact]
    public void UpdateState_WithNewState_UpdatesAllProperties()
    {
        // Arrange
        var layout = new WorldLayout();
        var facilities = new Dictionary<string, InnFacility>
        {
            { "kitchen", new InnFacility("Kitchen", 1, 1, 1.0, 100) }
        };
        var initialState = new InnState(layout, facilities);
        var viewModel = new InnGameViewModel(initialState);

        // Act
        var newState = initialState.AddGold(100).AddReputation(10);
        viewModel.UpdateState(newState);

        // Assert
        Assert.Equal(100, viewModel.Gold);
        Assert.Equal(10, viewModel.Reputation);
    }

    [Fact]
    public void GetCustomerSprites_ReturnsCorrectCount()
    {
        // Arrange
        var layout = new WorldLayout();
        var facilities = new Dictionary<string, InnFacility>();
        var customer1 = new Customer("Hero").WithPosition(new EntityPosition("entrance"));
        var customer2 = new Customer("Merchant").WithPosition(new EntityPosition("table1"));
        var state = new InnState(layout, facilities)
            .AddCustomer(customer1)
            .AddCustomer(customer2);
        var viewModel = new InnGameViewModel(state);

        // Act
        var sprites = viewModel.GetCustomerSprites();

        // Assert
        Assert.Equal(2, sprites.Count);
    }

    [Fact]
    public void GetStaffSprites_ReturnsCorrectCount()
    {
        // Arrange
        var layout = new WorldLayout();
        var facilities = new Dictionary<string, InnFacility>();
        var staff1 = new StaffMember("Tom", "Cook").WithPosition(new EntityPosition("kitchen"));
        var staff2 = new StaffMember("Barbara", "Waitress").WithPosition(new EntityPosition("bar"));
        var state = new InnState(layout, facilities)
            .AddStaff(staff1)
            .AddStaff(staff2);
        var viewModel = new InnGameViewModel(state);

        // Act
        var sprites = viewModel.GetStaffSprites();

        // Assert
        Assert.Equal(2, sprites.Count);
    }

    [Fact]
    public void PlayerStats_ReturnsCorrectValues()
    {
        // Arrange
        var layout = new WorldLayout();
        var facilities = new Dictionary<string, InnFacility>();
        var state = new InnState(layout, facilities)
            .AddGold(100)
            .AddReputation(50)
            .LevelUp()
            .LevelUp()
            .LevelUp();
        var viewModel = new InnGameViewModel(state);

        // Act
        var stats = viewModel.PlayerStats;

        // Assert
        Assert.Equal(100, stats.Gold);
        Assert.Equal(4, stats.InnLevel);
    }
}
