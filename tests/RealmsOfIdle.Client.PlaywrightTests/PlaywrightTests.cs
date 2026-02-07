using System.Text.RegularExpressions;
using Microsoft.Playwright.Xunit;

namespace RealmsOfIdle.Client.PlaywrightTests;

[Trait("Category", "E2E")]
public class PlaywrightTests : PageTest
{
    [Fact]
    public async Task HomePage_ShouldLoad_Successfully()
    {
        // Arrange & Act
        await Page.GotoAsync("http://localhost:5004");

        // Assert
        await Expect(Page.Locator("body")).ToBeVisibleAsync();
    }

    [Fact]
    public async Task ComponentsPage_ShouldDisplay_AllComponents()
    {
        // Arrange & Act
        await Page.GotoAsync("http://localhost:5004/components");

        // Assert
        await Expect(Page.Locator("h1")).ToContainTextAsync("Components");
    }

    [Fact]
    public async Task Buttons_ShouldRender_AllVariants()
    {
        // Arrange & Act
        await Page.GotoAsync("http://localhost:5004/components");

        // Assert
        await Expect(Page.Locator(".btn")).ToBeVisibleAsync();
        await Expect(Page.Locator(".btn-primary")).ToBeVisibleAsync();
        await Expect(Page.Locator(".btn-danger")).ToBeVisibleAsync();
    }

    [Fact]
    public async Task ProgressBar_ShouldRender_WithVariants()
    {
        // Arrange & Act
        await Page.GotoAsync("http://localhost:5004/components");

        // Assert
        await Expect(Page.Locator(".progress-bar")).ToBeVisibleAsync();
    }

    [Fact]
    public async Task ThemeSwitcher_ShouldBe_Visible()
    {
        // Arrange & Act
        await Page.GotoAsync("http://localhost:5004/theme-demo");

        // Assert
        await Expect(Page.Locator(".theme-switcher")).ToBeVisibleAsync();
    }

    [Fact]
    public async Task ThemeSwitcher_ShouldCycleThemes_OnClick()
    {
        // Arrange
        await Page.GotoAsync("http://localhost:5004/theme-demo");
        var themeButton = Page.Locator(".theme-switcher");

        // Act
        await themeButton.ClickAsync();

        // Assert - Theme should change (data-theme attribute changes)
        var htmlElement = Page.Locator("html");
        await Expect(htmlElement).ToHaveAttributeAsync("data-theme", new Regex("color|green|amber"));
    }

    [Fact]
    public async Task HudDemo_ShouldDisplay_AllHUDComponents()
    {
        // Arrange & Act
        await Page.GotoAsync("http://localhost:5004/hud-demo");

        // Assert
        await Expect(Page.Locator(".top-hud")).ToBeVisibleAsync();
        await Expect(Page.Locator(".action-bar")).ToBeVisibleAsync();
        await Expect(Page.Locator(".help-panel")).ToBeVisibleAsync();
    }

    [Fact]
    public async Task ActionBar_ShouldRespond_ToClicks()
    {
        // Arrange
        await Page.GotoAsync("http://localhost:5004/hud-demo");
        var actionSlot = Page.Locator(".action-slot").First;

        // Act
        await actionSlot.ClickAsync();

        // Assert - Slot should become active
        await Expect(actionSlot).ToHaveClassAsync("active");
    }

    [Fact]
    public async Task CanvasDemo_ShouldRender_CanvasElement()
    {
        // Arrange & Act
        await Page.GotoAsync("http://localhost:5004/canvas-demo");

        // Assert
        await Expect(Page.Locator("canvas")).ToBeVisibleAsync();
    }

    [Fact]
    public async Task Application_ShouldApply_CRTTheme()
    {
        // Arrange & Act
        await Page.GotoAsync("http://localhost:5004");

        // Assert - Check for CRT theme styles
        var body = Page.Locator("body");
        await Expect(body).ToHaveCSSAsync("font-family", new Regex("VT323|monospace"));
    }
}
