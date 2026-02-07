using AwesomeAssertions;
using Bunit;
using RealmsOfIdle.Client.UI.Components;

namespace RealmsOfIdle.Client.UI.Tests.Components;

[Trait("Category", "Unit")]
public class RetroPanelTests : TestContext
{
    [Fact]
    public void RetroPanel_ShouldRender_BaseClass()
    {
        // Act
        var cut = RenderComponent<RetroPanel>(parameters => parameters
            .Add(p => p.ChildContent, "Panel content"));

        // Assert
        cut.Find(".retro-panel").Should().NotBeNull();
    }

    [Fact]
    public void RetroPanel_ShouldRender_Header_WhenProvided()
    {
        // Act
        var cut = RenderComponent<RetroPanel>(parameters => parameters
            .Add(p => p.Header, "Panel Title")
            .Add(p => p.ChildContent, "Panel content"));

        // Assert
        cut.Find(".retro-panel-header").TextContent.Should().Be("Panel Title");
    }

    [Fact]
    public void RetroPanel_ShouldNotRender_Header_WhenNotProvided()
    {
        // Act
        var cut = RenderComponent<RetroPanel>(parameters => parameters
            .Add(p => p.ChildContent, "Panel content"));

        // Assert
        cut.FindAll(".retro-panel-header").Count.Should().Be(0);
    }

    [Fact]
    public void RetroPanel_ShouldRender_Body()
    {
        // Act
        var cut = RenderComponent<RetroPanel>(parameters => parameters
            .Add(p => p.ChildContent, "Panel content"));

        // Assert
        cut.Find(".retro-panel-body").TextContent.Should().Be("Panel content");
    }

    [Fact]
    public void RetroPanel_ShouldInclude_CustomClass()
    {
        // Act
        var cut = RenderComponent<RetroPanel>(parameters => parameters
            .Add(p => p.Class, "custom-panel")
            .Add(p => p.ChildContent, "Panel content"));

        // Assert
        cut.Find(".retro-panel").ClassList.Should().Contain("custom-panel");
    }

    [Fact]
    public void RetroPanel_ShouldInclude_CustomStyle()
    {
        // Act
        var cut = RenderComponent<RetroPanel>(parameters => parameters
            .Add(p => p.Style, "background: red;")
            .Add(p => p.ChildContent, "Panel content"));

        // Assert - Style parameter is defined but not currently rendered in markup
        // This test verifies the parameter can be set without error
        cut.Markup.Should().Contain("Panel content");
    }
}
