using AwesomeAssertions;
using NetArchTest.Rules;
using RealmsOfIdle.Client.UI.Components.HUD;

namespace RealmsOfIdle.ArchitectureTests;

[Trait("Category", "Architecture")]
public class UILayerTests
{
    [Fact]
    public void ClientUI_Should_Not_DependOn_ServerProjects()
    {
        var result = Types.InAssembly(typeof(TopHud).Assembly)
            .ShouldNot()
            .HaveDependencyOnAny("RealmsOfIdle.Server")
            .GetResult();

        if (!result.IsSuccessful)
        {
            var failingTypes = result.FailingTypeNames != null ? string.Join(", ", result.FailingTypeNames) : "None";
            result.IsSuccessful.Should().BeTrue($"Client.UI should not depend on Server.* projects. Failing types: {failingTypes}");
        }
    }

    [Fact]
    public void ClientUI_Should_Not_DependOn_CoreEngine()
    {
        var result = Types.InAssembly(typeof(TopHud).Assembly)
            .That()
            .DoNotResideInNamespace("RealmsOfIdle.Client.UI.Components.Game")
            .ShouldNot()
            .HaveDependencyOnAny("RealmsOfIdle.Core.Engine")
            .GetResult();

        if (!result.IsSuccessful)
        {
            var failingTypes = result.FailingTypeNames != null ? string.Join(", ", result.FailingTypeNames) : "None";
            result.IsSuccessful.Should().BeTrue($"Client.UI should not depend on Core.Engine (except game-specific components). Failing types: {failingTypes}");
        }
    }

    [Fact]
    public void ClientUI_Components_Should_ResideInComponentsNamespace()
    {
        var result = Types.InAssembly(typeof(TopHud).Assembly)
            .That()
            .ImplementInterface(typeof(Microsoft.AspNetCore.Components.IComponent))
            .Should()
            .ResideInNamespace("RealmsOfIdle.Client.UI.Components")
            .GetResult();

        if (!result.IsSuccessful)
        {
            var failingTypes = result.FailingTypeNames != null ? string.Join(", ", result.FailingTypeNames) : "None";
            result.IsSuccessful.Should().BeTrue($"Components should reside in Components namespace. Failing types: {failingTypes}");
        }
    }

    [Fact]
    public void ClientUI_Should_NotReference_HttpClient_InComponents()
    {
        var result = Types.InAssembly(typeof(TopHud).Assembly)
            .That()
            .ResideInNamespace("RealmsOfIdle.Client.UI.Components")
            .ShouldNot()
            .HaveDependencyOnAny("System.Net.Http")
            .GetResult();

        if (!result.IsSuccessful)
        {
            var failingTypes = result.FailingTypeNames != null ? string.Join(", ", result.FailingTypeNames) : "None";
            result.IsSuccessful.Should().BeTrue($"Components should not directly use HttpClient. Failing types: {failingTypes}");
        }
    }
}
