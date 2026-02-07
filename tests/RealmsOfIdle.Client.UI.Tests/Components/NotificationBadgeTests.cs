using AwesomeAssertions;
using Bunit;
using RealmsOfIdle.Client.UI.Components;

namespace RealmsOfIdle.Client.UI.Tests.Components;

[Trait("Category", "Unit")]
public class NotificationBadgeTests : TestContext
{
    [Fact]
    public void NotificationBadge_ShouldRender_DotType_WhenShowBadgeIsTrue()
    {
        // Act
        var cut = RenderComponent<NotificationBadge>(parameters => parameters
            .Add(p => p.Type, NotificationBadgeType.Dot)
            .Add(p => p.ShowBadge, true)
            .Add(p => p.ChildContent, "Icon"));

        // Assert
        cut.Find(".notification-badge-dot").Should().NotBeNull();
    }

    [Fact]
    public void NotificationBadge_ShouldNotRender_DotType_WhenShowBadgeIsFalse()
    {
        // Act
        var cut = RenderComponent<NotificationBadge>(parameters => parameters
            .Add(p => p.Type, NotificationBadgeType.Dot)
            .Add(p => p.ShowBadge, false)
            .Add(p => p.ChildContent, "Icon"));

        // Assert
        cut.FindAll(".notification-badge-dot").Count.Should().Be(0);
    }

    [Fact]
    public void NotificationBadge_ShouldRender_CountType_WhenShowBadgeIsTrue()
    {
        // Act
        var cut = RenderComponent<NotificationBadge>(parameters => parameters
            .Add(p => p.Type, NotificationBadgeType.Count)
            .Add(p => p.ShowBadge, true)
            .Add(p => p.Count, "5")
            .Add(p => p.ChildContent, "Icon"));

        // Assert
        cut.Find(".notification-badge-count").TextContent.Should().Be("5");
    }

    [Fact]
    public void NotificationBadge_ShouldNotRender_CountType_WhenShowBadgeIsFalse()
    {
        // Act
        var cut = RenderComponent<NotificationBadge>(parameters => parameters
            .Add(p => p.Type, NotificationBadgeType.Count)
            .Add(p => p.ShowBadge, false)
            .Add(p => p.Count, "5")
            .Add(p => p.ChildContent, "Icon"));

        // Assert
        cut.FindAll(".notification-badge-count").Count.Should().Be(0);
    }

    [Fact]
    public void NotificationBadge_ShouldNotRender_Count_WhenCountIsEmpty()
    {
        // Act
        var cut = RenderComponent<NotificationBadge>(parameters => parameters
            .Add(p => p.Type, NotificationBadgeType.Count)
            .Add(p => p.ShowBadge, true)
            .Add(p => p.ChildContent, "Icon"));

        // Assert
        cut.FindAll(".notification-badge-count").Count.Should().Be(0);
    }

    [Fact]
    public void NotificationBadge_ShouldRender_ChildContent()
    {
        // Act
        var cut = RenderComponent<NotificationBadge>(parameters => parameters
            .Add(p => p.ChildContent, "Bell Icon"));

        // Assert
        cut.Markup.Should().Contain("Bell Icon");
    }

    [Fact]
    public void NotificationBadge_DotType_ShouldHaveRelativePosition()
    {
        // Act
        var cut = RenderComponent<NotificationBadge>(parameters => parameters
            .Add(p => p.Type, NotificationBadgeType.Dot)
            .Add(p => p.ShowBadge, true)
            .Add(p => p.ChildContent, "Icon"));

        // Assert
        cut.Find(".notification-badge").GetAttribute("style")
            .Should().Contain("position: relative");
    }

    [Fact]
    public void NotificationBadge_CountType_ShouldNotHaveInlineStyle()
    {
        // Act
        var cut = RenderComponent<NotificationBadge>(parameters => parameters
            .Add(p => p.Type, NotificationBadgeType.Count)
            .Add(p => p.ShowBadge, true)
            .Add(p => p.Count, "3")
            .Add(p => p.ChildContent, "Icon"));

        // Assert
        cut.Find(".notification-badge").GetAttribute("style").Should().BeNull();
    }
}
