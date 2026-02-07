using AwesomeAssertions;
using RealmsOfIdle.Client.UI.Components.Theme;

namespace RealmsOfIdle.Client.UI.Tests;

[Trait("Category", "Unit")]
public class ThemeEnumTests
{
    [Fact]
    public void Theme_ShouldHave_ThreeValues()
    {
        // Assert
        Enum.GetValues<Theme>().Should().HaveCount(3);
    }

    [Fact]
    public void Theme_ShouldContain_Color()
    {
        // Assert
        Enum.TryParse<Theme>("Color", out var _).Should().BeTrue();
    }

    [Fact]
    public void Theme_ShouldContain_Green()
    {
        // Assert
        Enum.TryParse<Theme>("Green", out var _).Should().BeTrue();
    }

    [Fact]
    public void Theme_ShouldContain_Amber()
    {
        // Assert
        Enum.TryParse<Theme>("Amber", out var _).Should().BeTrue();
    }

    [Fact]
    public void ThemeChangedEventArgs_ShouldContain_NewTheme()
    {
        // Arrange
        var newTheme = Theme.Green;

        // Act
        var args = new ThemeChangedEventArgs(newTheme);

        // Assert
        args.NewTheme.Should().Be(Theme.Green);
    }
}
