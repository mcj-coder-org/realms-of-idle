using AwesomeAssertions;
using RealmsOfIdle.Client.UI.Components.Observability;

namespace RealmsOfIdle.Client.UI.Tests.Observability;

[Trait("Category", "Unit")]
public class UiMetricsTests
{
    [Fact]
    public void UiMetrics_ShouldCreate_AllCounters()
    {
        // Arrange & Act
        using var metrics = new UiMetrics();

        // Assert - Just verify it doesn't throw
        metrics.Should().NotBeNull();
    }

    [Fact]
    public void UiMetrics_RecordElementClick_ShouldNotThrow()
    {
        // Arrange
        using var metrics = new UiMetrics();

        // Act & Assert - Should not throw
        metrics.RecordElementClick("button", "btn-1");
    }

    [Fact]
    public void UiMetrics_RecordPanelView_ShouldNotThrow()
    {
        // Arrange
        using var metrics = new UiMetrics();

        // Act & Assert - Should not throw
        metrics.RecordPanelView("info_panel");
    }

    [Fact]
    public void UiMetrics_RecordThemeChange_ShouldNotThrow()
    {
        // Arrange
        using var metrics = new UiMetrics();

        // Act & Assert - Should not throw
        metrics.RecordThemeChange("color", "green");
    }

    [Fact]
    public void UiMetrics_RecordCameraPan_ShouldNotThrow()
    {
        // Arrange
        using var metrics = new UiMetrics();

        // Act & Assert - Should not throw
        metrics.RecordCameraPan("north", "keyboard");
    }

    [Fact]
    public void UiMetrics_RecordQuickAction_ShouldNotThrow()
    {
        // Arrange
        using var metrics = new UiMetrics();

        // Act & Assert - Should not throw
        metrics.RecordQuickAction("action-1");
    }

    [Fact]
    public void UiMetrics_Dispose_ShouldNotThrow()
    {
        // Arrange
        var metrics = new UiMetrics();

        // Act & Assert - Should not throw
        metrics.Dispose();
    }

    [Fact]
    public void UiMetrics_RecordElementClick_WithNullElementId_ShouldNotThrow()
    {
        // Arrange
        using var metrics = new UiMetrics();

        // Act & Assert - Should not throw
        metrics.RecordElementClick("button", null);
    }

    [Fact]
    public void UiMetrics_MultipleCalls_ShouldNotThrow()
    {
        // Arrange
        using var metrics = new UiMetrics();

        // Act & Assert - Should not throw
        for (int i = 0; i < 100; i++)
        {
            metrics.RecordElementClick("button", $"btn-{i}");
            metrics.RecordPanelView("info_panel");
            metrics.RecordThemeChange("color", "green");
            metrics.RecordCameraPan("north", "keyboard");
            metrics.RecordQuickAction($"action-{i}");
        }
    }

    [Fact]
    public async Task UiMetrics_ShouldBe_ThreadSafe()
    {
        // Arrange
        using var metrics = new UiMetrics();

        // Act - Multiple threads recording metrics
        var tasks = new List<Task>();
        for (int i = 0; i < 10; i++)
        {
            var taskId = i;
            tasks.Add(Task.Run(() =>
            {
                for (int j = 0; j < 100; j++)
                {
                    metrics.RecordElementClick($"thread-{taskId}", $"btn-{j}");
                }
            }));
        }

        // Assert - Should not throw
        await Task.WhenAll(tasks);
    }
}
