using AwesomeAssertions;
using Bunit;
using RealmsOfIdle.Client.UI.Components;

namespace RealmsOfIdle.Client.UI.Tests.Components;

[Trait("Category", "Unit")]
public class StatCardTests : TestContext
{
    [Fact]
    public void StatCard_ShouldRender_Value()
    {
        // Act
        var cut = RenderComponent<StatCard>(parameters => parameters
            .Add(p => p.Value, "100"));

        // Assert
        cut.Find(".stat-card-value").TextContent.Should().Be("100");
    }

    [Fact]
    public void StatCard_ShouldRender_Label()
    {
        // Act
        var cut = RenderComponent<StatCard>(parameters => parameters
            .Add(p => p.Value, "100")
            .Add(p => p.Label, "Gold"));

        // Assert
        cut.Find(".stat-card-label").TextContent.Should().Be("Gold");
    }

    [Fact]
    public void StatCard_ShouldRender_Icon()
    {
        // Act
        var cut = RenderComponent<StatCard>(parameters => parameters
            .Add(p => p.Value, "100")
            .Add(p => p.Icon, "💰"));

        // Assert
        cut.Find(".stat-card-icon").TextContent.Should().Be("💰");
    }

    [Fact]
    public void StatCard_ShouldRender_HorizontalLayout_ByDefault()
    {
        // Act
        var cut = RenderComponent<StatCard>(parameters => parameters
            .Add(p => p.Value, "100"));

        // Assert
        cut.Find(".stat-card").ClassList.Should().Contain("stat-card-horizontal");
    }

    [Fact]
    public void StatCard_ShouldRender_VerticalLayout()
    {
        // Act
        var cut = RenderComponent<StatCard>(parameters => parameters
            .Add(p => p.Value, "100")
            .Add(p => p.Layout, StatCardLayout.Vertical));

        // Assert
        cut.Find(".stat-card").ClassList.Should().Contain("stat-card-vertical");
    }

    [Fact]
    public void StatCard_ShouldRender_MinimalLayout()
    {
        // Act
        var cut = RenderComponent<StatCard>(parameters => parameters
            .Add(p => p.Value, "100")
            .Add(p => p.Layout, StatCardLayout.Minimal));

        // Assert
        cut.Find(".stat-card").ClassList.Should().Contain("stat-card-minimal");
    }

    [Fact]
    public void StatCard_MinimalLayout_ShouldNotRender_Label()
    {
        // Act
        var cut = RenderComponent<StatCard>(parameters => parameters
            .Add(p => p.Value, "100")
            .Add(p => p.Label, "Gold")
            .Add(p => p.Layout, StatCardLayout.Minimal));

        // Assert
        cut.FindAll(".stat-card-label").Count.Should().Be(0);
    }

    [Fact]
    public void StatCard_HorizontalLayout_ShouldRender_Icon_EvenWithoutIcon()
    {
        // Act
        var cut = RenderComponent<StatCard>(parameters => parameters
            .Add(p => p.Value, "100")
            .Add(p => p.Layout, StatCardLayout.Horizontal));

        // Assert - Horizontal layout always renders icon div
        cut.FindAll(".stat-card-icon").Count.Should().Be(1);
    }

    [Fact]
    public void StatCard_ShouldInclude_CustomClass()
    {
        // Act
        var cut = RenderComponent<StatCard>(parameters => parameters
            .Add(p => p.Value, "100")
            .Add(p => p.Class, "custom-class"));

        // Assert
        cut.Find(".stat-card").ClassList.Should().Contain("custom-class");
    }

    [Fact]
    public void StatCard_ShouldUse_DefaultValue_WhenNotProvided()
    {
        // Act
        var cut = RenderComponent<StatCard>();

        // Assert
        cut.Find(".stat-card-value").TextContent.Should().Be("0");
    }
}
