using RealmsOfIdle.Core.Scenarios.Inn;
using RealmsOfIdle.Core.Scenarios.Inn.Persistence;
using Xunit;

namespace RealmsOfIdle.Core.Tests.Scenarios.Inn.Persistence;

[Trait("Category", "Unit")]
public class InnStateDtoTests
{
    [Fact]
    public void ToDto_FromValidInnState_CreatesCorrectDto()
    {
        // Arrange
        var layout = new RealmsOfIdle.Core.Engine.Spatial.WorldLayout();
        var facilities = new System.Collections.Generic.Dictionary<string, InnFacility>
        {
            { "kitchen", new InnFacility("Kitchen", 1, 1, 1.0, 100) }
        };
        var customer = new Customer("Hero");
        var staff = new StaffMember("Tom", "Cook");
        var state = new InnState(layout, facilities)
            .AddCustomer(customer)
            .AddStaff(staff)
            .AddGold(100);

        // Act
        var dto = InnStateDto.FromDomain(state, "player1");

        // Assert
        Assert.Equal(100, dto.Gold);
        Assert.Equal(0, dto.Reputation);
        Assert.Equal(1, dto.InnLevel);
        Assert.Single(dto.Customers);
        Assert.Single(dto.Staff);
        Assert.Single(dto.Facilities);
    }

    [Fact]
    public void FromDto_ToValidDto_RestoresInnState()
    {
        // Arrange
        var dto = new InnStateDto
        {
            Gold = 100,
            Reputation = 50,
            InnLevel = 2,
            Customers = new System.Collections.Generic.List<CustomerDto>
            {
                new() { Name = "Hero", State = "Waiting" }
            },
            Staff = new System.Collections.Generic.List<StaffDto>
            {
                new() { Name = "Tom", Role = "Cook" }
            },
            Facilities = new System.Collections.Generic.List<FacilityDto>
            {
                new() { Type = "Kitchen", Level = 1, Id = "kitchen" }
            },
            LayoutAreas = new System.Collections.Generic.List<SceneAreaDto>()
        };

        // Act
        var state = dto.ToDomain();

        // Assert
        Assert.Equal(100, state.Gold);
        Assert.Equal(50, state.Reputation);
        Assert.Equal(2, state.InnLevel);
        Assert.Single(state.Customers);
        Assert.Single(state.Staff);
        Assert.Single(state.Facilities);
    }

    [Fact]
    public void RoundTrip_StatePreserved()
    {
        // Arrange
        var layout = new RealmsOfIdle.Core.Engine.Spatial.WorldLayout();
        var facilities = new System.Collections.Generic.Dictionary<string, InnFacility>
        {
            { "kitchen", new InnFacility("Kitchen", 1, 1, 1.0, 100) }
        };
        var customer = new Customer("Hero");
        var staff = new StaffMember("Tom", "Cook");
        var originalState = new InnState(layout, facilities)
            .AddCustomer(customer)
            .AddStaff(staff)
            .AddGold(100)
            .AddReputation(50);

        // Act
        var dto = InnStateDto.FromDomain(originalState, "player1");
        var restoredState = dto.ToDomain();

        // Assert
        Assert.Equal(originalState.Gold, restoredState.Gold);
        Assert.Equal(originalState.Reputation, restoredState.Reputation);
        Assert.Equal(originalState.InnLevel, restoredState.InnLevel);
        Assert.Equal(originalState.Customers.Count, restoredState.Customers.Count);
        Assert.Equal(originalState.Staff.Count, restoredState.Staff.Count);
    }
}
