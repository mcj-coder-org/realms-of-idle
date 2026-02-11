using Bunit;
using RealmsOfIdle.Client.Blazor.Components;

namespace RealmsOfIdle.Client.Blazor.Tests.Components;

/// <summary>
/// bUnit component tests for OfflineProgressModal
/// </summary>
[Trait("Category", "Unit")]
public class OfflineProgressModalTests : TestContext
{
    // T138: Modal renders with correct content
    [Fact]
    public void OfflineProgressModal_RendersWelcomeBackMessage()
    {
        var cut = RenderComponent<OfflineProgressModal>(parameters => parameters
            .Add(p => p.ElapsedTime, TimeSpan.FromMinutes(5))
            .Add(p => p.TotalGoldEarned, 500)
            .Add(p => p.TotalReputationEarned, 120)
            .Add(p => p.TotalActionsPerformed, 70));

        cut.Markup.Contains("Welcome back").Should().BeTrue("Modal should display welcome message");
    }

    // T138: Modal displays elapsed time
    [Fact]
    public void OfflineProgressModal_DisplaysElapsedTime()
    {
        var cut = RenderComponent<OfflineProgressModal>(parameters => parameters
            .Add(p => p.ElapsedTime, TimeSpan.FromMinutes(5))
            .Add(p => p.TotalGoldEarned, 500)
            .Add(p => p.TotalReputationEarned, 120)
            .Add(p => p.TotalActionsPerformed, 70));

        cut.Markup.Contains("5 minutes", StringComparison.Ordinal).Should().BeTrue("Modal should display elapsed time");
    }

    // T138: Modal displays gold earned
    [Fact]
    public void OfflineProgressModal_DisplaysGoldEarned()
    {
        var cut = RenderComponent<OfflineProgressModal>(parameters => parameters
            .Add(p => p.ElapsedTime, TimeSpan.FromMinutes(5))
            .Add(p => p.TotalGoldEarned, 500)
            .Add(p => p.TotalReputationEarned, 120)
            .Add(p => p.TotalActionsPerformed, 70));

        cut.Markup.Contains("500").Should().BeTrue("Modal should display gold earned");
    }

    // T138: Modal displays actions performed
    [Fact]
    public void OfflineProgressModal_DisplaysActionsPerformed()
    {
        var cut = RenderComponent<OfflineProgressModal>(parameters => parameters
            .Add(p => p.ElapsedTime, TimeSpan.FromMinutes(5))
            .Add(p => p.TotalGoldEarned, 500)
            .Add(p => p.TotalReputationEarned, 120)
            .Add(p => p.TotalActionsPerformed, 70));

        cut.Markup.Contains("70").Should().BeTrue("Modal should display actions performed");
    }

    // T138: Dismiss button exists and fires callback
    [Fact]
    public void OfflineProgressModal_DismissButton_FiresCallback()
    {
        var dismissed = false;
        var cut = RenderComponent<OfflineProgressModal>(parameters => parameters
            .Add(p => p.ElapsedTime, TimeSpan.FromMinutes(5))
            .Add(p => p.TotalGoldEarned, 500)
            .Add(p => p.TotalReputationEarned, 120)
            .Add(p => p.TotalActionsPerformed, 70)
            .Add(p => p.OnDismiss, () => dismissed = true));

        var button = cut.Find("button");
        button.Click();

        dismissed.Should().BeTrue("Clicking dismiss should fire OnDismiss callback");
    }
}

// Extension for AwesomeAssertions compatibility with bUnit
internal static class BoolAssertionExtensions
{
    public static BoolAssertionResult Should(this bool value) => new(value);
}

internal record BoolAssertionResult(bool Value)
{
    public void BeTrue(string because = "")
    {
        Assert.True(Value, because);
    }
}
