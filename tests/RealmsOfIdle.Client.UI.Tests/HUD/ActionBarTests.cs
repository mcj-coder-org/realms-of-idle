using AwesomeAssertions;
using Bunit;
using RealmsOfIdle.Client.UI.Components.HUD;

namespace RealmsOfIdle.Client.UI.Tests.HUD;

[Trait("Category", "Unit")]
public class ActionBarTests : TestContext
{
    [Fact]
    public void ActionBar_ShouldRender_FiveQuickSlots()
    {
        // Arrange
        var slots = new List<QuickActionSlot>
        {
            new() { Id = "focus", Label = "Focus", Icon = "🎯", KeyHint = "1" },
            new() { Id = "interact", Label = "Interact", Icon = "✋", KeyHint = "2" },
            new() { Id = "talk", Label = "Talk", Icon = "💬", KeyHint = "3" },
            new() { Id = "inventory", Label = "Inventory", Icon = "🎒", KeyHint = "4" },
            new() { Id = "map", Label = "Map", Icon = "🗺️", KeyHint = "5" }
        };

        // Act
        var cut = RenderComponent<ActionBar>(parameters => parameters
            .Add(p => p.ActionSlots, slots));

        // Assert
        cut.FindAll("button.action-slot").Count.Should().Be(5);
    }

    [Fact]
    public void ActionBar_ShouldRender_KeyHints()
    {
        // Arrange
        var slots = new List<QuickActionSlot>
        {
            new() { Id = "focus", Label = "Focus", Icon = "🎯", KeyHint = "1" }
        };

        // Act
        var cut = RenderComponent<ActionBar>(parameters => parameters
            .Add(p => p.ActionSlots, slots));

        // Assert
        cut.Markup.Should().Contain("1");
    }

    [Fact]
    public void ActionBar_ShouldRender_ActiveClass_WhenSlotIsActive()
    {
        // Arrange
        var slots = new List<QuickActionSlot>
        {
            new() { Id = "focus", Label = "Focus", Icon = "🎯", KeyHint = "1", IsActive = true }
        };

        // Act
        var cut = RenderComponent<ActionBar>(parameters => parameters
            .Add(p => p.ActionSlots, slots));

        // Assert
        cut.Find("button.active").Should().NotBeNull();
    }

    [Fact]
    public void ActionBar_ShouldRender_Cooldown_WhenPresent()
    {
        // Arrange
        var slots = new List<QuickActionSlot>
        {
            new() { Id = "focus", Label = "Focus", Icon = "🎯", CooldownPercent = 50 }
        };

        // Act
        var cut = RenderComponent<ActionBar>(parameters => parameters
            .Add(p => p.ActionSlots, slots));

        // Assert
        cut.Markup.Should().Contain("action-slot-cooldown");
    }

    [Fact]
    public void ActionBar_ShouldRender_ActionSlots()
    {
        // Arrange
        var slots = new List<QuickActionSlot>
        {
            new() { Id = "focus", Label = "Focus", Icon = "🎯", KeyHint = "1" }
        };

        // Act
        var cut = RenderComponent<ActionBar>(parameters => parameters
            .Add(p => p.ActionSlots, slots));

        // Assert - Verify button renders
        cut.Find("button.action-slot").Should().NotBeNull();
    }
}
