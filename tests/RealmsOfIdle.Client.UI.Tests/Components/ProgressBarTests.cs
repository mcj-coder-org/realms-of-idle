using AwesomeAssertions;
using Bunit;
using RealmsOfIdle.Client.UI.Components;

namespace RealmsOfIdle.Client.UI.Tests.Components;

[Trait("Category", "Unit")]
public class ProgressBarTests : TestContext
{
    [Fact]
    public void ProgressBar_ShouldRender_Value()
    {
        // Act
        var cut = RenderComponent<ProgressBar>(parameters => parameters
            .Add(p => p.Value, 50));

        // Assert
        cut.Find(".progress-bar-fill").GetAttribute("style").Should().Contain("width: 50%;");
    }

    [Fact]
    public void ProgressBar_ShouldRender_Label_WhenProvided()
    {
        // Act
        var cut = RenderComponent<ProgressBar>(parameters => parameters
            .Add(p => p.Value, 75)
            .Add(p => p.Label, "Loading..."));

        // Assert
        cut.Find(".progress-bar-label").TextContent.Should().Be("Loading...");
    }

    [Fact]
    public void ProgressBar_ShouldNotRender_Label_WhenNotProvided()
    {
        // Act
        var cut = RenderComponent<ProgressBar>(parameters => parameters
            .Add(p => p.Value, 75));

        // Assert
        cut.FindAll(".progress-bar-label").Count.Should().Be(0);
    }

    [Fact]
    public void ProgressBar_ShouldRender_SecondaryVariant()
    {
        // Act
        var cut = RenderComponent<ProgressBar>(parameters => parameters
            .Add(p => p.Value, 60)
            .Add(p => p.Variant, ProgressBarVariant.Secondary));

        // Assert
        cut.Find(".progress-bar-fill").ClassList.Should().Contain("secondary");
    }

    [Fact]
    public void ProgressBar_ShouldRender_DangerVariant()
    {
        // Act
        var cut = RenderComponent<ProgressBar>(parameters => parameters
            .Add(p => p.Value, 60)
            .Add(p => p.Variant, ProgressBarVariant.Danger));

        // Assert
        cut.Find(".progress-bar-fill").ClassList.Should().Contain("danger");
    }

    [Fact]
    public void ProgressBar_ShouldRender_DefaultVariant_WhenNotSpecified()
    {
        // Act
        var cut = RenderComponent<ProgressBar>(parameters => parameters
            .Add(p => p.Value, 60));

        // Assert
        var fill = cut.Find(".progress-bar-fill");
        fill.ClassList.Should().NotContain("secondary");
        fill.ClassList.Should().NotContain("danger");
    }

    [Fact]
    public void ProgressBar_ShouldRender_LargeSize()
    {
        // Act
        var cut = RenderComponent<ProgressBar>(parameters => parameters
            .Add(p => p.Value, 50)
            .Add(p => p.Size, ProgressBarSize.Large));

        // Assert
        cut.Find(".progress-bar").ClassList.Should().Contain("progress-bar-large");
    }

    [Fact]
    public void ProgressBar_ShouldRender_CompactSize()
    {
        // Act
        var cut = RenderComponent<ProgressBar>(parameters => parameters
            .Add(p => p.Value, 50)
            .Add(p => p.Size, ProgressBarSize.Compact));

        // Assert
        cut.Find(".progress-bar").ClassList.Should().Contain("progress-bar-compact");
    }

    [Fact]
    public void ProgressBar_ShouldRender_StandardSize_ByDefault()
    {
        // Act
        var cut = RenderComponent<ProgressBar>(parameters => parameters
            .Add(p => p.Value, 50));

        // Assert
        var bar = cut.Find(".progress-bar");
        bar.ClassList.Should().NotContain("progress-bar-large");
        bar.ClassList.Should().NotContain("progress-bar-compact");
    }

    [Fact]
    public void ProgressBar_ShouldReflect_Value_Changes()
    {
        // Arrange
        var cut = RenderComponent<ProgressBar>(parameters => parameters
            .Add(p => p.Value, 30));

        // Act
        cut.SetParametersAndRender(parameters => parameters
            .Add(p => p.Value, 80));

        // Assert
        cut.Find(".progress-bar-fill").GetAttribute("style").Should().Contain("width: 80%;");
    }
}
