using AwesomeAssertions;
using Bunit;
using RealmsOfIdle.Client.UI.Components;

namespace RealmsOfIdle.Client.UI.Tests.Components;

[Trait("Category", "Unit")]
public class ButtonTests : TestContext
{
    [Fact]
    public void Button_ShouldRender_DefaultVariant()
    {
        // Act
        var cut = RenderComponent<Button>(parameters => parameters
            .Add(p => p.ChildContent, "Click Me"));

        // Assert
        cut.Find("button").ClassList.Should().Contain("btn");
        cut.Find("button").ClassList.Should().NotContain("btn-primary");
        cut.Find("button").ClassList.Should().NotContain("btn-danger");
    }

    [Fact]
    public void Button_ShouldRender_PrimaryVariant()
    {
        // Act
        var cut = RenderComponent<Button>(parameters => parameters
            .Add(p => p.Variant, ButtonVariant.Primary)
            .Add(p => p.ChildContent, "Click Me"));

        // Assert
        cut.Find("button").ClassList.Should().Contain("btn-primary");
    }

    [Fact]
    public void Button_ShouldRender_DangerVariant()
    {
        // Act
        var cut = RenderComponent<Button>(parameters => parameters
            .Add(p => p.Variant, ButtonVariant.Danger)
            .Add(p => p.ChildContent, "Click Me"));

        // Assert
        cut.Find("button").ClassList.Should().Contain("btn-danger");
    }

    [Fact]
    public void Button_ShouldRender_SmallSize()
    {
        // Act
        var cut = RenderComponent<Button>(parameters => parameters
            .Add(p => p.Size, ButtonSize.Small)
            .Add(p => p.ChildContent, "Click Me"));

        // Assert
        cut.Find("button").ClassList.Should().Contain("btn-small");
    }

    [Fact]
    public void Button_ShouldRender_LargeSize()
    {
        // Act
        var cut = RenderComponent<Button>(parameters => parameters
            .Add(p => p.Size, ButtonSize.Large)
            .Add(p => p.ChildContent, "Click Me"));

        // Assert
        cut.Find("button").ClassList.Should().Contain("btn-large");
    }

    [Fact]
    public void Button_ShouldRender_StandardSize_ByDefault()
    {
        // Act
        var cut = RenderComponent<Button>(parameters => parameters
            .Add(p => p.ChildContent, "Click Me"));

        // Assert
        cut.Find("button").ClassList.Should().NotContain("btn-small");
        cut.Find("button").ClassList.Should().NotContain("btn-large");
    }

    [Fact]
    public void Button_ShouldRender_ChildContent()
    {
        // Act
        var cut = RenderComponent<Button>(parameters => parameters
            .Add(p => p.ChildContent, "Click Me"));

        // Assert
        cut.Markup.Should().Contain("Click Me");
    }

    [Fact]
    public void Button_ShouldInvoke_OnClick()
    {
        // Arrange
        var clicked = false;
        var cut = RenderComponent<Button>(parameters => parameters
            .Add(p => p.OnClick, () => { clicked = true; })
            .Add(p => p.ChildContent, "Click Me"));

        // Act
        cut.Find("button").Click();

        // Assert
        clicked.Should().BeTrue();
    }

    [Fact]
    public void Button_ShouldCombine_VariantAndSize()
    {
        // Act
        var cut = RenderComponent<Button>(parameters => parameters
            .Add(p => p.Variant, ButtonVariant.Primary)
            .Add(p => p.Size, ButtonSize.Large)
            .Add(p => p.ChildContent, "Click Me"));

        // Assert
        var button = cut.Find("button");
        button.ClassList.Should().Contain("btn");
        button.ClassList.Should().Contain("btn-primary");
        button.ClassList.Should().Contain("btn-large");
    }
}
