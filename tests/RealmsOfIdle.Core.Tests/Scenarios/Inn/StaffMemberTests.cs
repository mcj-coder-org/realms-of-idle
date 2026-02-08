using RealmsOfIdle.Core.Engine.Spatial;
using RealmsOfIdle.Core.Scenarios.Inn;
using Xunit;

namespace RealmsOfIdle.Core.Tests.Scenarios.Inn;

/// <summary>
/// Unit tests for StaffMember
/// </summary>
[Trait("Category", "Unit")]
public class StaffMemberTests
{
    [Fact]
    public void Constructor_WithNameAndRole_CreatesStaff()
    {
        // Arrange
        const string name = "Barbara";
        const string role = "Waitress";

        // Act
        var staff = new StaffMember(name, role);

        // Assert
        Assert.Equal(name, staff.Name);
        Assert.Equal(role, staff.Role);
        Assert.Null(staff.CurrentTask);
        Assert.Equal(1.0, staff.Efficiency);
        Assert.Null(staff.Position);
        Assert.Null(staff.DesignatedBed);
    }

    [Fact]
    public void Constructor_WithDesignatedBed_SetsBed()
    {
        // Arrange
        var bed = new GridPosition(2, 3);

        // Act
        var staff = new StaffMember("Tom", "Cook", null, 1.0, null, bed, 0.0);

        // Assert
        Assert.Equal(bed, staff.DesignatedBed);
    }

    [Fact]
    public void WithTask_SetsCurrentTask()
    {
        // Arrange
        var staff = new StaffMember("Barbara", "Waitress");
        var task = new StaffTask(StaffTaskType.Serve, "table_1");

        // Act
        var updated = staff.WithTask(task);

        // Assert
        Assert.Equal(task, updated.CurrentTask);
    }

    [Fact]
    public void ClearTask_RemovesTask()
    {
        // Arrange
        var staff = new StaffMember("Barbara", "Waitress");
        var withTask = staff.WithTask(new StaffTask(StaffTaskType.Serve, "table_1"));

        // Act
        var updated = withTask.ClearTask();

        // Assert
        Assert.Null(updated.CurrentTask);
    }

    [Fact]
    public void SetPosition_UpdatesPosition()
    {
        // Arrange
        var staff = new StaffMember("Barbara", "Waitress");
        var position = new EntityPosition("kitchen");

        // Act
        var updated = staff.WithPosition(position);

        // Assert
        Assert.Equal(position, updated.Position);
    }

    [Fact]
    public void WithEfficiency_UpdatesEfficiency()
    {
        // Arrange
        var staff = new StaffMember("Barbara", "Waitress");

        // Act
        var updated = staff.WithEfficiency(1.5);

        // Assert
        Assert.Equal(1.5, updated.Efficiency);
    }

    [Fact]
    public void IsIdle_WithNoTask_ReturnsTrue()
    {
        // Arrange
        var staff = new StaffMember("Barbara", "Waitress");

        // Act
        var result = staff.IsIdle;

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void IsIdle_WithTask_ReturnsFalse()
    {
        // Arrange
        var staff = new StaffMember("Barbara", "Waitress");
        var withTask = staff.WithTask(new StaffTask(StaffTaskType.Serve, "table_1"));

        // Act
        var result = withTask.IsIdle;

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void NeedsSleep_WithFatigueAboveThreshold_ReturnsTrue()
    {
        // Arrange
        var staff = new StaffMember("Barbara", "Waitress").WithFatigue(0.8);

        // Act
        var result = staff.NeedsSleep;

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void NeedsSleep_WithFatigueBelowThreshold_ReturnsFalse()
    {
        // Arrange
        var staff = new StaffMember("Barbara", "Waitress").WithFatigue(0.5);

        // Act
        var result = staff.NeedsSleep;

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void IncreaseFatigue_IncreasesFatigue()
    {
        // Arrange
        var staff = new StaffMember("Barbara", "Waitress");

        // Act
        var updated = staff.IncreaseFatigue(0.1);

        // Assert
        Assert.Equal(0.1, updated.Fatigue);
    }

    [Fact]
    public void DecreaseFatigue_DecreasesFatigue()
    {
        // Arrange
        var staff = new StaffMember("Barbara", "Waitress").WithFatigue(0.8);

        // Act
        var updated = staff.DecreaseFatigue(0.3);

        // Assert
        Assert.Equal(0.5, updated.Fatigue);
    }

    [Fact]
    public void DecreaseFatigue_ClampsAtZero()
    {
        // Arrange
        var staff = new StaffMember("Barbara", "Waitress").WithFatigue(0.2);

        // Act
        var updated = staff.DecreaseFatigue(0.5);

        // Assert
        Assert.Equal(0.0, updated.Fatigue);
    }
}
