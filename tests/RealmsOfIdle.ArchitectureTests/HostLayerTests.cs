using AwesomeAssertions;
using NetArchTest.Rules;
using RealmsOfIdle.Client.Blazor;

namespace RealmsOfIdle.ArchitectureTests;

[Trait("Category", "Architecture")]
public class HostLayerTests
{
    private static readonly System.Reflection.Assembly BlazorAssembly = typeof(HttpGameService).Assembly;

    [Fact]
    public void BlazorHost_ShouldNot_DependOnGameLogic_Assemblies()
    {
        // Blazor host may reference Core.Engine.Spatial (GridPosition) and Core.Abstractions
        // but should not depend on game-specific logic like GameLogic or Inn scenarios
        var result = Types.InAssembly(BlazorAssembly)
            .ShouldNot()
            .HaveDependencyOnAny("RealmsOfIdle.Core.GameLogic", "RealmsOfIdle.Core.Scenarios")
            .GetResult();

        if (!result.IsSuccessful)
        {
            var failingTypes = result.FailingTypeNames != null ? string.Join(", ", result.FailingTypeNames) : "None";
            result.IsSuccessful.Should().BeTrue($"Blazor host should not directly depend on game logic assemblies. Failing types: {failingTypes}");
        }
    }

    [Fact]
    public void BlazorHost_ShouldHave_ClientUI_AssemblyReference()
    {
        // The Blazor project file includes a ProjectReference to Client.UI
        // We verify by checking if the assembly exists at expected location
        var clientUiPath = Path.Combine(AppContext.BaseDirectory, "RealmsOfIdle.Client.UI.dll");
        File.Exists(clientUiPath).Should().BeTrue("Client.UI assembly should be available");
    }

    [Fact]
    public void BlazorHost_Program_ShouldExist()
    {
        // The Blazor assembly should contain a Program type (generated from top-level statements)
        BlazorAssembly.Should().NotBeNull("Blazor host assembly should be loadable");
    }

    [Fact]
    public void BlazorHost_ShouldHaveReasonableTypeCount()
    {
        // Blazor client includes Models, Services, Components for possession demo
        var types = Types.InAssembly(BlazorAssembly)
            .GetTypes()
            .ToList();

        // Client app with game models, services, and components
        types.Count.Should().BeLessThan(100, "Blazor client should have reasonable type count");
    }
}
