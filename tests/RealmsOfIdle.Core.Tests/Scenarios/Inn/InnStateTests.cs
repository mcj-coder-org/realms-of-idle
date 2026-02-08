using RealmsOfIdle.Core.Engine.Spatial;
using RealmsOfIdle.Core.Scenarios.Inn;
using Xunit;

namespace RealmsOfIdle.Core.Tests.Scenarios.Inn;

/// <summary>
/// Unit tests for InnState
/// </summary>
[Trait("Category", "Unit")]
public class InnStateTests
{
    [Fact]
    public void Constructor_WithRequiredParameters_CreatesState()
    {
        // Arrange
        var layout = new WorldLayout();
        var facilities = new Dictionary<string, InnFacility>
        {
            { "kitchen", new InnFacility("Kitchen", 1, 1, 1.0, 100) }
        };

        // Act
        var state = new InnState(layout, facilities);

        // Assert
        Assert.Same(layout, state.Layout);
        Assert.NotEmpty(state.Facilities);
        Assert.Empty(state.Customers);
        Assert.Empty(state.Staff);
        Assert.Equal(0, state.Gold);
        Assert.Equal(0, state.Reputation);
        Assert.Equal(1, state.InnLevel);
    }

    [Fact]
    public void AddCustomer_AddsToCustomersList()
    {
        // Arrange
        var state = CreateDefaultState();
        var customer = new Customer("Hero");

        // Act
        var updated = state.AddCustomer(customer);

        // Assert
        Assert.Single(updated.Customers);
        Assert.Contains(customer, updated.Customers);
    }

    [Fact]
    public void RemoveCustomer_RemovesFromList()
    {
        // Arrange
        var state = CreateDefaultState();
        var customer = new Customer("Hero");
        var withCustomer = state.AddCustomer(customer);

        // Act
        var updated = withCustomer.RemoveCustomer(customer);

        // Assert
        Assert.Empty(updated.Customers);
    }

    [Fact]
    public void AddStaff_AddsToStaffList()
    {
        // Arrange
        var state = CreateDefaultState();
        var staff = new StaffMember("Barbara", "Waitress");

        // Act
        var updated = state.AddStaff(staff);

        // Assert
        Assert.Single(updated.Staff);
        Assert.Contains(staff, updated.Staff);
    }

    [Fact]
    public void RemoveStaff_RemovesFromList()
    {
        // Arrange
        var state = CreateDefaultState();
        var staff = new StaffMember("Barbara", "Waitress");
        var withStaff = state.AddStaff(staff);

        // Act
        var updated = withStaff.RemoveStaff(staff);

        // Assert
        Assert.Empty(updated.Staff);
    }

    [Fact]
    public void AddGold_IncreasesGold()
    {
        // Arrange
        var state = CreateDefaultState();

        // Act
        var updated = state.AddGold(50);

        // Assert
        Assert.Equal(50, updated.Gold);
    }

    [Fact]
    public void RemoveGold_DecreasesGold()
    {
        // Arrange
        var state = CreateDefaultState().AddGold(100);

        // Act
        var updated = state.RemoveGold(30);

        // Assert
        Assert.Equal(70, updated.Gold);
    }

    [Fact]
    public void RemoveGold_ClampsAtZero()
    {
        // Arrange
        var state = CreateDefaultState().AddGold(20);

        // Act
        var updated = state.RemoveGold(50);

        // Assert
        Assert.Equal(0, updated.Gold);
    }

    [Fact]
    public void CanAfford_WithSufficientGold_ReturnsTrue()
    {
        // Arrange
        var state = CreateDefaultState().AddGold(100);

        // Act
        var result = state.CanAfford(50);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void CanAfford_WithInsufficientGold_ReturnsFalse()
    {
        // Arrange
        var state = CreateDefaultState().AddGold(20);

        // Act
        var result = state.CanAfford(50);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void AddReputation_IncreasesReputation()
    {
        // Arrange
        var state = CreateDefaultState();

        // Act
        var updated = state.AddReputation(10);

        // Assert
        Assert.Equal(10, updated.Reputation);
    }

    [Fact]
    public void LevelUp_IncreasesInnLevel()
    {
        // Arrange
        var state = CreateDefaultState();

        // Act
        var updated = state.LevelUp();

        // Assert
        Assert.Equal(2, updated.InnLevel);
    }

    [Fact]
    public void GetFacility_ById_ReturnsFacility()
    {
        // Arrange
        var facilities = new Dictionary<string, InnFacility>
        {
            { "kitchen", new InnFacility("Kitchen", 1, 5, 1.0, 100) }
        };
        var state = new InnState(new WorldLayout(), facilities);

        // Act
        var facility = state.GetFacility("kitchen");

        // Assert
        Assert.NotNull(facility);
        Assert.Equal("Kitchen", facility?.Type);
    }

    [Fact]
    public void GetFacility_WithUnknownId_ReturnsNull()
    {
        // Arrange
        var state = CreateDefaultState();

        // Act
        var facility = state.GetFacility("unknown");

        // Assert
        Assert.Null(facility);
    }

    [Fact]
    public void UpgradeFacility_UpdatesFacility()
    {
        // Arrange
        var facilities = new Dictionary<string, InnFacility>
        {
            { "kitchen", new InnFacility("Kitchen", 1, 5, 1.0, 100) }
        };
        var state = new InnState(new WorldLayout(), facilities);

        // Act
        var updated = state.UpgradeFacility("kitchen");

        // Assert
        var facility = updated.GetFacility("kitchen");
        Assert.Equal(2, facility?.Level);
    }

    [Fact]
    public void GetAvailableGuestRooms_CountsUnoccupiedBeds()
    {
        // Arrange
        var facilities = new Dictionary<string, InnFacility>
        {
            { "guest_bed_1", new InnFacility("GuestRoom", 1, 1, 0, 50) },
            { "guest_bed_2", new InnFacility("GuestRoom", 1, 1, 0, 50) },
            { "guest_bed_3", new InnFacility("GuestRoom", 1, 1, 0, 50) }
        };
        var state = new InnState(new WorldLayout(), facilities);
        var customer = new Customer("Hero").WithAssignedBed(new GridPosition(0, 0));
        state = state.AddCustomer(customer);

        // Act
        var available = state.GetAvailableGuestRooms();

        // Assert
        Assert.Equal(3, available); // All beds are available (none assigned to customers yet)
    }

    [Fact]
    public void GetAvailableStaffBeds_CountsUnassignedBeds()
    {
        // Arrange
        var facilities = new Dictionary<string, InnFacility>
        {
            { "staff_bed_1", new InnFacility("StaffBed", 1, 1, 0, 30) },
            { "staff_bed_2", new InnFacility("StaffBed", 1, 1, 0, 30) }
        };
        var state = new InnState(new WorldLayout(), facilities);
        var staff = new StaffMember("Barbara", "Waitress", null, 1.0, null, new GridPosition(0, 0), 0.0);
        state = state.AddStaff(staff);

        // Act
        var available = state.GetAvailableStaffBeds();

        // Assert
        Assert.Equal(2, available); // Only counts beds, not assignments
    }

    private static InnState CreateDefaultState()
    {
        return new InnState(new WorldLayout(), new Dictionary<string, InnFacility>());
    }
}
