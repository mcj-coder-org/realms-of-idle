using AwesomeAssertions;
using Bunit;
using RealmsOfIdle.Client.UI.Components;

namespace RealmsOfIdle.Client.UI.Tests.Components;

[Trait("Category", "Unit")]
public class ResourceIconTests : TestContext
{
    [Fact]
    public void ResourceIcon_ShouldRender_Emoji()
    {
        // Act
        var cut = RenderComponent<ResourceIcon>(parameters => parameters
            .Add(p => p.Emoji, "🪵"));

        // Assert
        cut.Find(".resource-icon-emoji").TextContent.Should().Be("🪵");
    }

    [Fact]
    public void ResourceIcon_ShouldRender_Amount()
    {
        // Act
        var cut = RenderComponent<ResourceIcon>(parameters => parameters
            .Add(p => p.Amount, "100"));

        // Assert
        cut.Find(".resource-icon-amount").TextContent.Should().Be("100");
    }

    [Fact]
    public void ResourceIcon_ShouldRender_LargeSize()
    {
        // Act
        var cut = RenderComponent<ResourceIcon>(parameters => parameters
            .Add(p => p.Size, ResourceIconSize.Large));

        // Assert
        var icon = cut.Find(".resource-icon");
        icon.ClassList.Should().Contain("resource-icon-large");
    }

    [Fact]
    public void ResourceIcon_ShouldRender_CompactSize()
    {
        // Act
        var cut = RenderComponent<ResourceIcon>(parameters => parameters
            .Add(p => p.Size, ResourceIconSize.Compact));

        // Assert
        var icon = cut.Find(".resource-icon");
        icon.ClassList.Should().Contain("resource-icon-compact");
    }

    [Fact]
    public void ResourceIcon_ShouldRender_StandardSize_ByDefault()
    {
        // Act
        var cut = RenderComponent<ResourceIcon>();

        // Assert
        var icon = cut.Find(".resource-icon");
        icon.ClassList.Should().Contain("resource-icon");
        icon.ClassList.Should().NotContain("resource-icon-large");
        icon.ClassList.Should().NotContain("resource-icon-compact");
    }

    [Fact]
    public void ResourceIcon_ShouldRender_Both_EmojiAndAmount()
    {
        // Act
        var cut = RenderComponent<ResourceIcon>(parameters => parameters
            .Add(p => p.Emoji, "💰")
            .Add(p => p.Amount, "500"));

        // Assert
        cut.Markup.Should().Contain("💰");
        cut.Markup.Should().Contain("500");
    }
}
