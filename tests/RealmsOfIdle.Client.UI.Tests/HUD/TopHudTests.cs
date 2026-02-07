using AwesomeAssertions;
using Bunit;
using RealmsOfIdle.Client.UI.Components.HUD;

namespace RealmsOfIdle.Client.UI.Tests.HUD;

[Trait("Category", "Unit")]
public class TopHudTests : TestContext
{
    [Fact]
    public void TopHud_ShouldRender_GoldDisplay()
    {
        // Arrange
        var stats = new PlayerStats { Gold = 100 };

        // Act
        var cut = RenderComponent<TopHud>(parameters => parameters
            .Add(p => p.PlayerStats, stats)
            .Add(p => p.ShowInnLevel, false)
            .Add(p => p.ShowHealth, false));

        // Assert
        cut.Markup.Should().Contain("100");
    }

    [Fact]
    public void TopHud_ShouldRender_PlayerLevel()
    {
        // Arrange
        var stats = new PlayerStats { PlayerLevel = 5 };

        // Act
        var cut = RenderComponent<TopHud>(parameters => parameters
            .Add(p => p.PlayerStats, stats)
            .Add(p => p.ShowInnLevel, false)
            .Add(p => p.ShowHealth, false));

        // Assert
        cut.Markup.Should().Contain("5");
    }

    [Fact]
    public void TopHud_ShouldRender_InnLevel_WhenEnabled()
    {
        // Arrange
        var stats = new PlayerStats { InnLevel = 3 };

        // Act
        var cut = RenderComponent<TopHud>(parameters => parameters
            .Add(p => p.PlayerStats, stats)
            .Add(p => p.ShowInnLevel, true)
            .Add(p => p.ShowHealth, false));

        // Assert
        cut.Markup.Should().Contain("3");
    }

    [Fact]
    public void TopHud_ShouldNotRender_InnLevel_WhenDisabled()
    {
        // Arrange
        var stats = new PlayerStats { InnLevel = 3 };

        // Act
        var cut = RenderComponent<TopHud>(parameters => parameters
            .Add(p => p.PlayerStats, stats)
            .Add(p => p.ShowInnLevel, false)
            .Add(p => p.ShowHealth, false));

        // Assert
        cut.Markup.Should().NotContain("inn");
    }

    [Fact]
    public void TopHud_ShouldCalculate_HealthPercentage()
    {
        // Arrange
        var stats = new PlayerStats { Health = 75, MaxHealth = 100 };

        // Act
        var cut = RenderComponent<TopHud>(parameters => parameters
            .Add(p => p.PlayerStats, stats)
            .Add(p => p.ShowInnLevel, false)
            .Add(p => p.ShowHealth, true));

        // Assert
        cut.Markup.Should().Contain("75/100");
    }
}
