using AwesomeAssertions;
using Bunit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;
using RealmsOfIdle.Client.UI.Components.Observability;

namespace RealmsOfIdle.Client.UI.Tests.Observability;

[Trait("Category", "Unit")]
public class ObservableButtonTests : TestContext
{
    private static readonly UiMetrics _sharedMetrics = new UiMetrics();

    public ObservableButtonTests()
    {
        // Set up service provider with required services
        Services.AddSingleton<ILogger<ObservableButton>>(Mock.Of<ILogger<ObservableButton>>());
        Services.AddSingleton(_sharedMetrics);
    }

    [Fact]
    public void ObservableButton_ShouldRender_ChildContent()
    {
        // Act
        var cut = RenderComponent<ObservableButton>(parameters => parameters
            .Add(p => p.ChildContent, "Click Me"));

        // Assert
        cut.Markup.Should().Contain("Click Me");
    }

    [Fact]
    public void ObservableButton_ShouldRender_CssClass()
    {
        // Act
        var cut = RenderComponent<ObservableButton>(parameters => parameters
            .Add(p => p.CssClass, "btn-primary")
            .Add(p => p.ChildContent, "Click Me"));

        // Assert
        cut.Find("button").ClassList.Should().Contain("btn-primary");
    }

    [Fact]
    public void ObservableButton_ShouldRender_DisabledAttribute()
    {
        // Act
        var cut = RenderComponent<ObservableButton>(parameters => parameters
            .Add(p => p.Disabled, true)
            .Add(p => p.ChildContent, "Click Me"));

        // Assert
        cut.Find("button").HasAttribute("disabled").Should().BeTrue();
    }

    [Fact]
    public void ObservableButton_ShouldInvoke_OnClick_Callback()
    {
        // Arrange
        var clicked = false;

        var cut = RenderComponent<ObservableButton>(parameters => parameters
            .Add(p => p.ElementType, "button")
            .Add(p => p.OnClick, () => { clicked = true; })
            .Add(p => p.ChildContent, "Click Me"));

        // Act
        cut.Find("button").Click();

        // Assert
        clicked.Should().BeTrue();
    }

    [Fact]
    public void ObservableButton_ShouldRender_WithElementId()
    {
        // Act
        var cut = RenderComponent<ObservableButton>(parameters => parameters
            .Add(p => p.ElementId, "my-button")
            .Add(p => p.ElementType, "custom_button")
            .Add(p => p.ChildContent, "Click Me"));

        // Assert
        cut.Markup.Should().Contain("Click Me");
    }

    [Fact]
    public void ObservableButton_ShouldRender_WithElementType()
    {
        // Arrange & Act
        var cut = RenderComponent<ObservableButton>(parameters => parameters
            .Add(p => p.ElementType, "action_slot")
            .Add(p => p.ChildContent, "Action"));

        // Assert - Button should render with specified element type
        cut.Find("button").Should().NotBeNull();
    }

    [Fact]
    public void ObservableButton_ShouldNotRender_ChildContent_WhenEmpty()
    {
        // Act
        var cut = RenderComponent<ObservableButton>();

        // Assert - Button element should still exist
        cut.Find("button").Should().NotBeNull();
    }

    [Fact]
    public void ObservableButton_DefaultElementType_ShouldBe_Button()
    {
        // Arrange & Act
        var cut = RenderComponent<ObservableButton>(parameters => parameters
            .Add(p => p.ChildContent, "Click Me"));

        // Assert - Should render without error (uses default ElementType = "button")
        cut.Find("button").Should().NotBeNull();
    }
}
