using AwesomeAssertions;
using NetArchTest.Rules;
using RealmsOfIdle.Core.Domain;
using RealmsOfIdle.Server.Orleans.Interfaces;

namespace RealmsOfIdle.ArchitectureTests;

[Trait("Category", "Architecture")]
public class NamingConventionTests
{
    [Fact]
    public void All_Interfaces_In_Core_Must_Start_With_I()
    {
        var result = Types.InAssembly(typeof(GameSession).Assembly)
            .That()
            .AreInterfaces()
            .And()
            .ArePublic()
            .Should()
            .HaveNameStartingWith("I")
            .GetResult();

        if (!result.IsSuccessful)
        {
            var failingTypes = result.FailingTypeNames != null ? string.Join(", ", result.FailingTypeNames) : "None";
            result.IsSuccessful.Should().BeTrue($"All public interfaces in Core must start with 'I'. Failing types: {failingTypes}");
        }
    }

    [Fact]
    public void All_Public_Types_In_Core_Must_Start_With_Uppercase()
    {
        var result = Types.InAssembly(typeof(GameSession).Assembly)
            .That()
            .ArePublic()
            .Should()
            .MeetCustomRule(new PascalCaseNamingRule())
            .GetResult();

        if (!result.IsSuccessful)
        {
            var failingTypes = result.FailingTypeNames != null ? string.Join(", ", result.FailingTypeNames) : "None";
            result.IsSuccessful.Should().BeTrue($"All public types in Core must start with uppercase. Failing types: {failingTypes}");
        }
    }

    [Fact]
    public void All_Grain_Interfaces_Must_Start_With_I()
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
}

/// <summary>
/// Custom rule to check if type names follow PascalCase convention.
/// </summary>
public class PascalCaseNamingRule : ICustomRule
{
    public bool MeetsRule(Mono.Cecil.TypeDefinition type)
    {
        var name = type.Name;
        return string.IsNullOrEmpty(name) || char.IsUpper(name[0]) || name.StartsWith('<');
    }

    public string ErrorMessage => "Type name should use PascalCase (start with uppercase letter)";
}
