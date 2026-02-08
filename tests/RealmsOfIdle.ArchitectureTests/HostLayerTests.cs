using AwesomeAssertions;
using NetArchTest.Rules;

namespace RealmsOfIdle.ArchitectureTests;

[Trait("Category", "Architecture")]
public class HostLayerTests
{
    // Blazor WebAssembly not compatible with .NET 10 - temporarily excluded
#pragma warning disable CS0169, CA1805 // Disable unused field and unnecessary initialization warnings
    private static readonly Type? BlazorProgramType = null;
#pragma warning restore CS0169, CA1805

    [Fact(Skip = "Blazor WebAssembly not compatible with .NET 10 - temporarily excluded")]
    public void BlazorHost_ShouldNot_DependOnGameLogic_Assemblies()
    {
#pragma warning disable CS8602 // Dereference of a possibly null reference
        // Blazor host should only reference UI component library, not game logic
        var result = Types.InAssembly(BlazorProgramType!.Assembly)
            .ShouldNot()
            .HaveDependencyOnAny("RealmsOfIdle.Core.Engine", "RealmsOfIdle.Core.GameLogic")
            .GetResult();
#pragma warning restore CS8602

        if (!result.IsSuccessful)
        {
            var failingTypes = result.FailingTypeNames != null ? string.Join(", ", result.FailingTypeNames) : "None";
            result.IsSuccessful.Should().BeTrue($"Blazor host should not directly depend on game logic assemblies. Failing types: {failingTypes}");
        }
    }

    [Fact(Skip = "Blazor WebAssembly not compatible with .NET 10 - temporarily excluded")]
    public void BlazorHost_ShouldHave_ClientUI_AssemblyReference()
    {
#pragma warning disable CS8602 // Dereference of a possibly null reference
        // The Blazor project file includes a ProjectReference to Client.UI
        // We verify by checking if types from Client.UI can be loaded
        var blazorAssembly = BlazorProgramType!.Assembly;
#pragma warning restore CS8602
        var referencedAssemblyNames = blazorAssembly.GetReferencedAssemblies()
            .Select(a => a.Name)
            .ToList();

        // Note: Project references may not appear in GetReferencedAssemblies the same way
        // Instead, we verify the assembly exists at expected location
        var clientUiPath = Path.Combine(AppContext.BaseDirectory, "RealmsOfIdle.Client.UI.dll");
        File.Exists(clientUiPath).Should().BeTrue("Client.UI assembly should be available");
    }

    [Fact(Skip = "Blazor WebAssembly not compatible with .NET 10 - temporarily excluded")]
    public void BlazorHost_Program_ShouldExist()
    {
        // The Program.cs should exist
        BlazorProgramType.Should().NotBeNull("Blazor host should have a Program class");
    }

    [Fact(Skip = "Blazor WebAssembly not compatible with .NET 10 - temporarily excluded")]
    public void BlazorHost_ShouldHaveMinimalTypes()
    {
#pragma warning disable CS8602 // Dereference of a possibly null reference
        // Host should only have a few types (Program, maybe some configuration)
        var types = Types.InAssembly(BlazorProgramType!.Assembly)
            .GetTypes()
            .ToList();
#pragma warning restore CS8602

        // Host should be minimal - typically just Program and maybe a few others
        types.Count.Should().BeLessThan(20, "Blazor host should have minimal type count");
    }
}
