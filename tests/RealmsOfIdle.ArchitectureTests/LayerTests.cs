using AwesomeAssertions;
using NetArchTest.Rules;
using RealmsOfIdle.Core.Domain;

namespace RealmsOfIdle.ArchitectureTests;

[Trait("Category", "Architecture")]
public class LayerTests
{
    [Fact]
    public void Core_Should_Not_DependOn_ServerOrleans()
    {
        var result = Types.InAssembly(typeof(GameSession).Assembly)
            .ShouldNot()
            .HaveDependencyOnAny("RealmsOfIdle.Server.Orleans")
            .GetResult();

        if (!result.IsSuccessful)
        {
            var failingTypes = result.FailingTypeNames != null ? string.Join(", ", result.FailingTypeNames) : "None";
            result.IsSuccessful.Should().BeTrue($"Core should not depend on Server.Orleans. Failing types: {failingTypes}");
        }
    }

    [Fact]
    public void Core_Should_Not_DependOn_AspNetCore()
    {
        var result = Types.InAssembly(typeof(GameSession).Assembly)
            .ShouldNot()
            .HaveDependencyOnAny("Microsoft.AspNetCore")
            .GetResult();

        if (!result.IsSuccessful)
        {
            var failingTypes = result.FailingTypeNames != null ? string.Join(", ", result.FailingTypeNames) : "None";
            result.IsSuccessful.Should().BeTrue($"Core should not depend on ASP.NET Core. Failing types: {failingTypes}");
        }
    }

    [Fact]
    public void Core_Should_Not_DependOn_OrleansRuntime_ExcludingCodeGen()
    {
        var result = Types.InAssembly(typeof(GameSession).Assembly)
            .That()
            .DoNotResideInNamespace("OrleansCodeGen")
            .ShouldNot()
            .HaveDependencyOnAny("RealmsOfIdle.Server.Orleans")
            .GetResult();

        if (!result.IsSuccessful)
        {
            var failingTypes = result.FailingTypeNames != null ? string.Join(", ", result.FailingTypeNames) : "None";
            result.IsSuccessful.Should().BeTrue($"Core should not depend on Server.Orleans. Failing types: {failingTypes}");
        }
    }
}
