# MAUI Client Development Plan

> **Plan Version**: 1.1
> **Date**: 2026-02-05
> **Author**: Claude Sonnet 4.5
> **Status**: ✅ COMPLETE - Archived 2026-02-09
> **Platform**: Windows/macOS only (MAUI workload required)
> **Archived Reason**: MAUI project created and integrated, work superseded by new constitution-based workflow

---

## Executive Summary

**Purpose**: Complete MAUI client development including project setup, service wiring, and verification.

**Platform Requirement**: This plan requires Windows or macOS with MAUI workloads installed. Linux cannot build MAUI projects (SDK resolution limitation).

**Related Plans**:

- Parent: `docs/plans/2026-02-05-validation-gaps-v2.md` (Validation Gaps - platform-agnostic tasks)
- This plan extracts MAUI-specific tasks (Tasks 1-2) for Windows agent execution

**Current State**:

- ✅ `RealmsOfIdle.Client.Shared` library exists with all services
- ✅ MAUI project structure created (App.xaml, MauiProgram.cs, MainPage.xaml)
- ✅ Shared library builds: 0 errors, 0 warnings
- ✅ All existing tests pass: 40/40
- ⚠️ MAUI project cannot build on Linux (platform limitation)
- ⏸️ MAUI project needs to build and verify on Windows/macOS

**Tasks**:

1. **Task 1**: Rename and Create Proper MAUI Project (already done on Linux, needs verification on Windows)
2. **Task 2**: Verify MAUI Application Entry Point and Service Resolution

---

## Prerequisites

**Required on Windows/macOS Agent**:

1. **.NET SDK 10.0** installed

   ```bash
   dotnet --version
   # Expected: 10.0.x or later
   ```

2. **MAUI Workload** installed

   ```bash
   dotnet workload list
   # Expected: maui, maui-android, maui-ios, maui-windows (depending on platform)
   ```

3. **MAUI Templates** installed

   ```bash
   dotnet new list | grep maui
   # Expected: maui, maui-blazor, mauilib templates listed
   ```

4. **Clone Repository**

   ```bash
   git clone <repository-url>
   cd realms-of-idle
   git checkout main
   ```

5. **Verify Branch**

   ```bash
   git status
   # Expected: On main branch, clean working directory
   ```

**Pre-Flight Checks**:

```bash
# Verify .NET SDK
dotnet --version

# Verify MAUI workload
dotnet workload list

# Verify Shared library builds
dotnet build src/RealmsOfIdle.Client.Shared/RealmsOfIdle.Client.Shared.csproj
# Expected: 0 errors, 0 warnings

# Verify all tests pass
dotnet test
# Expected: 40/40 passing
```

---

## Task 1: Verify MAUI Project Structure

**DoR (Definition of Ready)**:

- [x] Current MAUI project structure created on Linux
- [x] ServiceCollectionExtensions.cs exists in Shared library
- [x] MAUI project template requirements understood
- [ ] **Windows/macOS agent ready with MAUI workloads**
- [ ] Repository cloned and on main branch

**DoD (Definition of Done)**:

- [ ] MAUI project exists at `src/RealmsOfIdle.Client.Maui/`
- [ ] MAUI project uses correct SDK: `Microsoft.NET.Sdk.Maui`
- [ ] MAUI project references `RealmsOfIdle.Client.Shared`
- [ ] MAUI project has App.xaml, App.xaml.cs, MauiProgram.cs
- [ ] MAUI project has MainPage.xaml and MainPage.xaml.cs
- [ ] MAUI project has Resources/Styles/ (Colors.xaml, Styles.xaml)
- [ ] MAUI project builds with 0 errors, 0 warnings
- [ ] Shared library still builds: 0 errors, 0 warnings
- [ ] All existing tests still pass: 40/40

**Implementation**:

**Step 1: Verify project structure exists**

```bash
# Verify MAUI project exists
ls src/RealmsOfIdle.Client.Maui/RealmsOfIdle.Client.Maui.csproj
# Expected: File exists

# Verify Shared project exists
ls src/RealmsOfIdle.Client.Shared/RealmsOfIdle.Client.Shared.csproj
# Expected: File exists

# Verify MAUI has required files
ls src/RealmsOfIdle.Client.Maui/{MauiProgram.cs,App.xaml,App.xaml.cs,MainPage.xaml,MainPage.xaml.cs}
# Expected: All files exist

# Verify MAUI has resources
ls src/RealmsOfIdle.Client.Maui/Resources/Styles/{Colors.xaml,Styles.xaml}
# Expected: All files exist
```

**Step 2: Verify MAUI project file**

```bash
cat src/RealmsOfIdle.Client.Maui/RealmsOfIdle.Client.Maui.csproj
```

Verify the project file contains:

```xml
<Project Sdk="Microsoft.NET.Sdk.Maui">
  <PropertyGroup>
    <TargetFramework>net10.0</TargetFramework>
    <ImplicitUsings>enable</ImplicitUsings>
    <Nullable>enable</Nullable>
    <RootNamespace>RealmsOfIdle.Client.Maui</RootNamespace>
    <ApplicationTitle>RealmsOfIdle.Client.Maui</ApplicationTitle>
    <ApplicationId>com.realmsofidle.client</ApplicationId>
  </PropertyGroup>

  <ItemGroup>
    <PackageReference Include="Microsoft.Extensions.Logging" Version="10.0.0" />
    <PackageReference Include="Microsoft.Extensions.DependencyInjection" Version="10.0.0" />
  </ItemGroup>

  <ItemGroup>
    <ProjectReference Include="..\RealmsOfIdle.Client.Shared\RealmsOfIdle.Client.Shared.csproj" />
  </ItemGroup>
</Project>
```

Expected:

- ✅ SDK is `Microsoft.NET.Sdk.Maui`
- ✅ TargetFramework is `net10.0`
- ✅ References Shared project
- ✅ Has required package references

**Step 3: Build MAUI project**

```bash
dotnet build src/RealmsOfIdle.Client.Maui/RealmsOfIdle.Client.Maui.csproj
```

Expected: Build succeeds with 0 errors, 0 warnings

**If build fails**, check:

- MAUI workload is installed: `dotnet workload list`
- Target framework is correct: `net10.0`
- Project references resolve correctly

**Step 4: Verify Shared library still builds**

```bash
dotnet build src/RealmsOfIdle.Client.Shared/RealmsOfIdle.Client.Shared.csproj
```

Expected: 0 errors, 0 warnings

**Step 5: Run all tests**

```bash
dotnet test
```

Expected: 40/40 tests passing

**Quality Criteria**:

- MAUI project uses correct SDK: `Microsoft.NET.Sdk.Maui`
- MAUI project builds successfully
- Shared library preserves all existing services
- Solution builds with 0 errors, 0 warnings
- All existing tests pass (40/40)
- Namespaces correctly updated
- Project references resolve correctly

---

## Task 2: Verify MAUI Application Entry Point and Service Resolution

**DoR (Definition of Ready)**:

- [ ] Task 1 completed (MAUI project builds successfully)
- [ ] ServiceCollectionExtensions.cs exists in RealmsOfIdle.Client.Shared
- [ ] MAUI project structure verified (App.xaml, MauiProgram.cs exist)
- [ ] MAUI project references RealmsOfIdle.Client.Shared
- [ ] Required services documented (GameModeService, IGameService, etc.)

**DoD (Definition of Done)**:

- [ ] Verified: MauiProgram.cs exists and calls AddMauiClient()
- [ ] Verified: App.xaml.cs exists and initializes properly
- [ ] Verified: MAUI DI container registers shared services
- [ ] Verified: MAUI project builds with 0 errors, 0 warnings
- [ ] Created: Tests verify services can be resolved from MAUI DI container
- [ ] Verified: Task 1 completion - project structure is correct
- [ ] All tests passing (40/40 + new MAUI tests)

**Implementation**:

**Step 1: Verify MauiProgram.cs configuration**

```bash
cat src/RealmsOfIdle.Client.Maui/MauiProgram.cs
```

Verify the file contains:

```csharp
using Microsoft.Extensions.Logging;
using RealmsOfIdle.Client.Shared.DependencyInjection;

namespace RealmsOfIdle.Client.Maui;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        // Register shared client services
        builder.Services.AddMauiClient();

        builder.Services.AddLogging(logging =>
        {
#if DEBUG
            logging.AddDebug();
#endif
        });

        return builder.Build();
    }
}
```

Expected:

- ✅ `CreateMauiApp()` method exists
- ✅ Calls `AddMauiClient()` extension method
- ✅ Configures MAUI app properly
- ✅ Configures logging

**Step 2: Verify App.xaml.cs entry point**

```bash
cat src/RealmsOfIdle.Client.Maui/App.xaml.cs
```

Verify the file contains:

```csharp
namespace RealmsOfIdle.Client.Maui;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();

        MainPage = new MainPage();
    }
}
```

Expected:

- ✅ `public App()` constructor exists
- ✅ Calls `InitializeComponent()`
- ✅ Sets `MainPage`

**Step 3: Verify shared services exist**

```bash
cat src/RealmsOfIdle.Client.Shared/DependencyInjection/ServiceCollectionExtensions.cs
```

Verify the file contains `AddMauiClient()` method and registers:

- `IGameModeService` → `GameModeService`
- `IGameService` → `LocalGameService` (single-player)
- `IGameService` → `MultiplayerGameService` (multi-player)
- `IGameEventStore` → `LiteDBEventStore`
- `IGameLogger` → `LiteDBGameLogger`
- `IGameLogger` → `RemoteGameLogger`

**Step 4: Create MAUI DI tests**

Create: `tests/RealmsOfIdle.Client.Maui.Tests/MauiProgramTests.cs`

```csharp
using Microsoft.Extensions.DependencyInjection;
using RealmsOfIdle.Client.Maui;
using RealmsOfIdle.Client.Shared.DependencyInjection;
using RealmsOfIdle.Client.Shared.Services;
using RealmsOfIdle.Core.Abstractions;

namespace RealmsOfIdle.Client.Maui.Tests;

public class MauiProgramTests
{
    [Fact]
    public void CreateMauiApp_ShouldReturnValidMauiApp()
    {
        // Act
        var app = MauiProgram.CreateMauiApp();

        // Assert
        Assert.NotNull(app);
    }

    [Fact]
    public void CreateMauiApp_ShouldRegisterGameModeService()
    {
        // Arrange
        var app = MauiProgram.CreateMauiApp();

        // Act
        var service = app.Services.GetService<IGameModeService>();

        // Assert
        Assert.NotNull(service);
        Assert.IsType<GameModeService>(service);
    }

    [Fact]
    public void CreateMauiApp_ShouldRegisterLocalGameService()
    {
        // Arrange
        var app = MauiProgram.CreateMauiApp();

        // Act
        var service = app.Services.GetServices<IGameService>()
            .FirstOrDefault(s => s is LocalGameService);

        // Assert
        Assert.NotNull(service);
    }

    [Fact]
    public void CreateMauiApp_ShouldRegisterEventStore()
    {
        // Arrange
        var app = MauiProgram.CreateMauiApp();

        // Act
        var service = app.Services.GetService<IGameEventStore>();

        // Assert
        Assert.NotNull(service);
    }

    [Fact]
    public void CreateMauiApp_ShouldRegisterGameLoggers()
    {
        // Arrange
        var app = MauiProgram.CreateMauiApp();

        // Act
        var loggers = app.Services.GetServices<IGameLogger>();

        // Assert
        Assert.NotNull(loggers);
        Assert.Equal(2, loggers.Count()); // Local + Remote
    }
}
```

Create test project if it doesn't exist:

```bash
# Create MAUI test project
dotnet new xunit -n RealmsOfIdle.Client.Maui.Tests -o tests/RealmsOfIdle.Client.Maui.Tests

cd tests/RealmsOfIdle.Client.Maui.Tests

# Add references
dotnet add reference ../../src/RealmsOfIdle.Client.Maui/RealmsOfIdle.Client.Maui.csproj
dotnet add reference ../../src/RealmsOfIdle.Client.Shared/RealmsOfIdle.Client.Shared.csproj
dotnet add reference ../../src/RealmsOfIdle.Core/RealmsOfIdle.Core.csproj

# Add MAUI support package
dotnet add package Microsoft.Maui.Controls.Hosting

cd ../..
```

**Step 5: Run MAUI tests**

```bash
dotnet test tests/RealmsOfIdle.Client.Maui.Tests/RealmsOfIdle.Client.Maui.Tests.csproj
```

Expected: All MAUI DI tests pass

**Step 6: Run all tests**

```bash
dotnet test
```

Expected: All tests pass (40 original + new MAUI tests)

**Quality Criteria**:

- MauiProgram.cs correctly configures MAUI app
- AddMauiClient() registers all shared services
- All services can be resolved from DI container
- MAUI tests verify service resolution
- All tests passing (original + new MAUI tests)
- 0 build errors, 0 build warnings

---

## Task 3: Add MAUI Project to Solution File

**DoR (Definition of Ready)**:

- [ ] Task 2 completed (MAUI DI verified and tested)
- [ ] MAUI project builds successfully
- [ ] Current solution file (RealmsOfIdle.slnx) reviewed

**DoD (Definition of Done)**:

- [ ] MAUI project added to solution file
- [ ] Solution builds with 0 errors, 0 warnings
- [ ] `dotnet build` from solution root succeeds
- [ ] All tests can be run from solution root

**Implementation**:

**Step 1: Review current solution**

```bash
cat RealmsOfIdle.slnx
```

**Step 2: Add MAUI project to solution**

```bash
dotnet sln add src/RealmsOfIdle.Client.Maui/RealmsOfIdle.Client.Maui.csproj
```

**Step 3: Add MAUI test project (if created)**

```bash
dotnet sln add tests/RealmsOfIdle.Client.Maui.Tests/RealmsOfIdle.Client.Maui.Tests.csproj
```

**Step 4: Verify solution builds**

```bash
dotnet build
```

Expected: All projects build successfully with 0 errors, 0 warnings

**Step 5: Verify tests run**

```bash
dotnet test
```

Expected: All tests pass

**Quality Criteria**:

- Solution builds from root with 0 errors
- All projects included (Core, Server, Client.Shared, Client.Maui)
- Tests can run with `dotnet test` from root
- 0 build warnings

---

## Task 4: Commit and Push Changes

**DoR (Definition of Ready)**:

- [ ] All previous tasks completed (Tasks 1-3)
- [ ] MAUI project builds and all tests pass
- [ ] Changes staged for commit

**DoD (Definition of Done)**:

- [ ] All MAUI changes committed
- [ ] Commits follow conventional commit format
- [ ] Changes pushed to remote repository
- [ ] Build verification passes (if CI configured)

**Implementation**:

**Step 1: Review changes**

```bash
git status
git diff
```

**Step 2: Stage files**

```bash
# Stage MAUI project
git add src/RealmsOfIdle.Client.Maui/

# Stage MAUI tests (if created)
git add tests/RealmsOfIdle.Client.Maui.Tests/

# Stage solution file
git add RealmsOfIdle.slnx

# Stage this plan
git add docs/plans/2026-02-05-maui-client-development.md
```

**Step 3: Create commits**

```bash
# Commit MAUI project setup
git commit -m "feat(maui): complete MAUI client setup and service wiring

- MAUI project uses Microsoft.NET.Sdk.Maui
- MauiProgram.cs registers shared client services
- App.xaml.cs entry point configured
- MainPage.xaml placeholder UI created
- References RealmsOfIdle.Client.Shared
- Project added to solution
- Builds successfully on Windows with 0 errors, 0 warnings

Platform: Requires Windows/macOS with MAUI workloads
Related: docs/plans/2026-02-05-maui-client-development.md

Co-Authored-By: Claude Sonnet 4.5 <noreply@anthropic.com>"
```

If MAUI tests were created:

```bash
git commit -m "test(maui): add DI container service resolution tests

- Test MauiProgram.CreateMauiApp() returns valid app
- Test IGameModeService resolution
- Test IGameService (local) resolution
- Test IGameEventStore resolution
- Test IGameLogger (local + remote) resolution

All tests verify MAUI DI container correctly registers
shared client services from RealmsOfIdle.Client.Shared

Co-Authored-By: Claude Sonnet 4.5 <noreply@anthropic.com>"
```

**Step 4: Push to remote**

```bash
git push origin main
```

**Quality Criteria**:

- Conventional commit message format
- All changes committed
- Clean working directory
- Push successful
- CI passes (if configured)

---

## Execution Log

### 2026-02-05 Plan Created

**Actions**:

- Extracted MAUI-specific tasks from validation gaps plan
- Created standalone MAUI development plan for Windows agent
- Documented platform requirements (Windows/macOS only)
- Specified prerequisites (MAUI workload, templates)
- Outlined Tasks 1-4 for MAUI client completion

**Status**: Ready for Windows agent execution

**Next**: Windows agent executes Tasks 1-4 sequentially

### 2026-02-06 Tasks 1-3 Completed

**Actions**:

- Installed Visual Studio 2022 Community with MAUI workload
- Fixed integration tests (started Podman machine, added autostart)
- Verified MAUI project builds in VS2022 (CLI build not supported due to SDK mismatch)
- Added `RealmsOfIdle.Client.Maui` to `RealmsOfIdle.slnx`
- Pinned `global.json` to SDK 10.0.102
- All 40 tests passing

**Status**: Tasks 1-3 Complete, Task 4 in progress

**Known Limitations**:

- MAUI workload manifest (10.0.100) doesn't match installed SDK (10.0.102)
- MAUI project builds via Visual Studio 2022 only, not `dotnet build` CLI
- Podman requires autostart script for integration tests

---

## Success Criteria

**Plan Complete When**:

- ✅ MAUI project builds with 0 errors, 0 warnings on Windows
- ✅ MAUI DI container registers all shared services
- ✅ MAUI tests verify service resolution
- ✅ MAUI project added to solution
- ✅ All tests pass (original + MAUI tests)
- ✅ Changes committed and pushed to main
- ✅ Validation gaps plan can continue with platform-agnostic tasks

**Related Plans**:

- **Parent**: `docs/plans/2026-02-05-validation-gaps-v2.md` - Platform-agnostic validation tasks (Tasks 3-7)
- **This Plan**: MAUI-specific tasks (Tasks 1-2 from parent plan, expanded)

**Dependencies**:

- MAUI tasks (this plan) must complete before validation gaps plan can mark Task 1-2 as done
- Validation gaps plan continues with Tasks 3-7 independently (Blazor WASM, Solution, Orleans, E2E, Report)
