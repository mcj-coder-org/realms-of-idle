using AwesomeAssertions;
using NetArchTest.Rules;
using RealmsOfIdle.Server.Orleans.Grains;
using RealmsOfIdle.Server.Orleans.Interfaces;

namespace RealmsOfIdle.ArchitectureTests;

[Trait("Category", "Architecture")]
public class OrleansDesignTests
{
    [Fact]
    public void All_Grains_Must_Be_Classes()
    {
        var result = Types.InAssembly(typeof(HealthGrain).Assembly)
            .That()
            .ResideInNamespace("RealmsOfIdle.Server.Orleans.Grains")
            .Should()
            .BeClasses()
            .GetResult();

        if (!result.IsSuccessful)
        {
            var failingTypes = result.FailingTypeNames != null ? string.Join(", ", result.FailingTypeNames) : "None";
            result.IsSuccessful.Should().BeTrue($"All grains must be classes. Failing types: {failingTypes}");
        }
    }

    [Fact]
    public void All_Grain_Classes_Must_End_With_Grain()
    {
        var result = Types.InAssembly(typeof(HealthGrain).Assembly)
            .That()
            .ResideInNamespace("RealmsOfIdle.Server.Orleans.Grains")
            .Should()
            .HaveNameEndingWith("Grain")
            .GetResult();

        if (!result.IsSuccessful)
        {
            var failingTypes = result.FailingTypeNames != null ? string.Join(", ", result.FailingTypeNames) : "None";
            result.IsSuccessful.Should().BeTrue($"All grain classes must end with 'Grain'. Failing types: {failingTypes}");
        }
    }

    [Fact]
    public void All_Grain_Interfaces_Must_Be_INamespace()
    {
        var result = Types.InAssembly(typeof(IHealthGrain).Assembly)
            .That()
            .ResideInNamespace("RealmsOfIdle.Server.Orleans.Interfaces")
            .And()
            .AreInterfaces()
            .Should()
            .HaveNameStartingWith("I")
            .GetResult();

        if (!result.IsSuccessful)
        {
            var failingTypes = result.FailingTypeNames != null ? string.Join(", ", result.FailingTypeNames) : "None";
            result.IsSuccessful.Should().BeTrue($"All grain interfaces must start with 'I'. Failing types: {failingTypes}");
        }
    }

    [Fact]
    public void All_Grain_Interfaces_Must_End_With_Grain()
    {
        var result = Types.InAssembly(typeof(IHealthGrain).Assembly)
            .That()
            .ResideInNamespace("RealmsOfIdle.Server.Orleans.Interfaces")
            .And()
            .AreInterfaces()
            .Should()
            .HaveNameEndingWith("Grain")
            .GetResult();

        if (!result.IsSuccessful)
        {
            var failingTypes = result.FailingTypeNames != null ? string.Join(", ", result.FailingTypeNames) : "None";
            result.IsSuccessful.Should().BeTrue($"All grain interfaces must end with 'Grain'. Failing types: {failingTypes}");
        }
    }
}
