# Validation Gaps Implementation Plan

> **Plan Version**: 2.1 (Updated with Task 1: Rename and Create Proper MAUI)
> **Date**: 2026-02-05
> **Author**: Claude Sonnet 4.5
> **Status**: Ready for execution - Task 1 ready to start

---

## Executive Summary

**Current State Analysis**:

- ✅ Build: 0 errors, 0 warnings
- ✅ Tests: 40/40 passing (13 Core + 2 System + 6 Tests + 5 Orleans + 6 E2E + 8 Integration)
- ✅ Orleans: Configured and running with health endpoint
- ✅ API: Configured with Orleans client and health checks
- ⚠️ **MAUI Client**: Services exist but not wired to app entry point
- ❌ **Blazor WASM Client**: Does not exist

**Remaining Validation Gaps**:

1. "MAUI client" project is misnamed - it's actually a shared client library
2. Proper MAUI project doesn't exist
3. MAUI app entry point not created (App.xaml.cs, MauiProgram.cs)
4. MAUI app doesn't use registered services
5. Blazor WASM client doesn't exist
6. Solution file may need updates
7. Start script doesn't include Orleans
8. Full stack E2E validation not performed

---

## Task 1: Rename Client Library and Create Proper MAUI Project

**DoR (Definition of Ready)**:

- [ ] Current MAUI project structure reviewed
- [ ] ServiceCollectionExtensions.cs dependencies documented
- [ ] MAUI project template requirements understood (.NET 10, MAUI workloads)
- [ ] Solution file structure reviewed
- [ ] Project references to "RealmsOfIdle.Client.Maui" identified

**DoD (Definition of Done)**:

- [ ] `RealmsOfIdle.Client.Maui` renamed to `RealmsOfIdle.Client.Shared`
- [ ] New `RealmsOfIdle.Client.Maui` project created with proper MAUI SDK
- [ ] MAUI project has App.xaml, App.xaml.cs, MauiProgram.cs
- [ ] MAUI project references `RealmsOfIdle.Client.Shared`
- [ ] All project references updated across solution
- [ ] Solution builds with 0 errors, 0 warnings
- [ ] All existing tests still pass (40/40)

**Implementation**:

**Step 1: Identify all references to the project**

```bash
# Find all project references
grep -r "RealmsOfIdle.Client.Maui" --include="*.csproj" --include="*.slnx"

# Check test projects
grep -r "RealmsOfIdle.Client.Maui" tests/
```

Document all files that reference the MAUI project.

**Step 2: Create proper MAUI project**

```bash
# Create new MAUI project (if MAUI workload installed)
dotnet new maui -n RealmsOfIdle.Client.MauiTemp -o src/RealmsOfIdle.Client.MauiNew

# OR manually create project structure
mkdir -p src/RealmsOfIdle.Client.MauiNew
```

Create: `src/RealmsOfIdle.Client.MauiNew/RealmsOfIdle.Client.Maui.csproj`

```xml
<Project Sdk="Microsoft.NET.Sdk.Maui">
  <PropertyGroup>
    <TargetFramework>net10.0</TargetFramework>
    <ImplicitUsings>enable</ImplicitUsings>
    <Nullable>enable</Nullable>
    <RootNamespace>RealmsOfIdle.Client.Maui</RootNamespace>
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

**Step 3: Create MAUI app infrastructure**

Create: `src/RealmsOfIdle.Client.MauiNew/MauiProgram.cs`

```csharp
using Microsoft.Extensions.Logging;
using RealmsOfIdle.Client.Maui.DependencyInjection;

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

Create: `src/RealmsOfIdle.Client.MauiNew/App.xaml`

```xml
<?xml version="1.0" encoding="UTF-8" ?>
<Application xmlns="http://schemas.microsoft.com/dotnet/2021/maui"
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
             x:Class="RealmsOfIdle.Client.Maui.App">
    <Application.Resources>
        <ResourceDictionary>
            <ResourceDictionary.MergedDictionaries>
                <ResourceDictionary Source="Resources/Styles/Colors.xaml" />
                <ResourceDictionary Source="Resources/Styles/Styles.xaml" />
            </ResourceDictionary.MergedDictionaries>
        </ResourceDictionary>
    </Application.Resources>
</Application>
```

Create: `src/RealmsOfIdle.Client.MauiNew/App.xaml.cs`

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

Create: `src/RealmsOfIdle.Client.MauiNew/MainPage.xaml` (placeholder)

```xml
<?xml version="1.0" encoding="utf-8" ?>
<ContentPage xmlns="http://schemas.microsoft.com/dotnet/2021/maui"
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
             x:Class="RealmsOfIdle.Client.Maui.MainPage">

    <ScrollView>
        <VerticalStackLayout
            Padding="30,0"
            Spacing="25">
            <Label
                Text="Realms of Idle"
                SemanticProperties.HeadingLevel="Level1"
                FontSize="32"
                HorizontalOptions="Center" />

            <Label
                Text="MAUI Client - Under Construction"
                SemanticProperties.HeadingLevel="Level2"
                SemanticProperties.Description="Welcome to the Realms of Idle MAUI client"
                FontSize="18"
                HorizontalOptions="Center" />
        </VerticalStackLayout>
    </ScrollView>
</ContentPage>
```

Create: `src/RealmsOfIdle.Client.MauiNew/MainPage.xaml.cs`

```csharp
namespace RealmsOfIdle.Client.Maui;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
    }
}
```

**Step 4: Rename existing project to Shared**

```bash
# Move old project
mv src/RealmsOfIdle.Client.Maui src/RealmsOfIdle.Client.Shared

# Update project file
# Change: <Project Sdk="Microsoft.NET.Sdk">
# To: <Project Sdk="Microsoft.NET.Sdk"> (keep as-is, it's correct for shared library)
```

Update: `src/RealmsOfIdle.Client.Shared/RealmsOfIdle.Client.Shared.csproj`

- Just change filename and AssemblyTitle in project file

**Step 5: Move new MAUI project into place**

```bash
# Remove temp MAUI project if needed
rm -rf src/RealmsOfIdle.Client.Maui

# Move new MAUI project
mv src/RealmsOfIdle.Client.MauiNew src/RealmsOfIdle.Client.Maui
```

**Step 6: Update solution file**

```bash
# Remove old reference
dotnet sln remove src/RealmsOfIdle.Client.Maui/RealmsOfIdle.Client.Maui.csproj

# Add shared project
dotnet sln add src/RealmsOfIdle.Client.Shared/RealmsOfIdle.Client.Shared.csproj

# Add new MAUI project
dotnet sln add src/RealmsOfIdle.Client.Maui/RealmsOfIdle.Client.Maui.csproj
```

**Step 7: Update all project references**

For each project that references `RealmsOfIdle.Client.Maui`:

- If it needs the shared services: Change to `RealmsOfIdle.Client.Shared`
- If it's the MAUI app itself: Already updated to reference Shared

```bash
# Find and update references
# (Manual process - update .csproj files)
```

**Step 8: Update namespaces**

In `src/RealmsOfIdle.Client.Shared`:

- Update all files: `namespace RealmsOfIdle.Client.Maui` → `namespace RealmsOfIdle.Client.Shared`

**Step 9: Update test projects**

If any tests reference the old MAUI project:

- Update project references to use `RealmsOfIdle.Client.Shared`
- Update using statements

**Step 10: Verify build**

```bash
dotnet build
```

Expected: 0 errors, 0 warnings

**Step 11: Run tests**

```bash
dotnet test
```

Expected: 40/40 passing

**Step 12: Commit**

```bash
git add .
git commit -m "refactor(client): rename MAUI to Shared, create proper MAUI project

- Rename RealmsOfIdle.Client.Maui to RealmsOfIdle.Client.Shared
- Create new RealmsOfIdle.Client.Maui with proper MAUI SDK
- MAUI app has App.xaml, MauiProgram.cs, MainPage
- MAUI references Shared for services
- Update all project references across solution
- All tests passing (40/40)"
```

**Quality Criteria**:

- MAUI project uses correct SDK: `Microsoft.NET.Sdk.Maui`
- Shared library preserves all existing services
- Solution builds with 0 errors, 0 warnings
- All existing tests pass (40/40)
- Namespaces correctly updated
- Project references resolve correctly

---

## Task 2: Verify MAUI Application Entry Point and Service Resolution

**DoR (Definition of Ready)**:

- [ ] Task 1 completed (proper MAUI project created, old project renamed to Shared)
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

**Implementation**:

**Step 1: Verify Task 1 completion - project structure**

```bash
# Verify MAUI project uses correct SDK
grep "Sdk" src/RealmsOfIdle.Client.Maui/RealmsOfIdle.Client.Maui.csproj
# Expected: <Project Sdk="Microsoft.NET.Sdk.Maui">

# Verify Shared project exists
ls src/RealmsOfIdle.Client.Shared/RealmsOfIdle.Client.Shared.csproj
# Expected: File exists

# Verify MAUI has required files
ls src/RealmsOfIdle.Client.Maui/{MauiProgram.cs,App.xaml.cs,App.xaml}
# Expected: All files exist

# Verify MAUI references Shared
grep "RealmsOfIdle.Client.Shared" src/RealmsOfIdle.Client.Maui/RealmsOfIdle.Client.Maui.csproj
# Expected: <ProjectReference include="..\RealmsOfIdle.Client.Shared\..." />
```

**Step 2: Verify MauiProgram.cs configuration**

```bash
cat src/RealmsOfIdle.Client.Maui/MauiProgram.cs
```

Verify:

- `CreateMauiApp()` method exists
- Calls `AddMauiClient()` to register shared services
- Configures MAUI app properly

**Step 3: Verify App.xaml.cs entry point**

```bash
cat src/RealmsOfIdle.Client.Maui/App.xaml.cs
```

Verify:

- `public App()` constructor exists
- Calls `InitializeComponent()`
- Sets `MainPage`

**Step 4: Verify shared services exist**

```bash
cat src/RealmsOfIdle.Client.Shared/DependencyInjection/ServiceCollectionExtensions.cs
```

Verify:

- `AddMauiClient()` method exists
- Registers all required services (IGameModeService, IGameService, etc.)

**Step 5: Verify build**

```bash
dotnet build src/RealmsOfIdle.Client.Maui/RealmsOfIdle.Client.Maui.csproj
```

Expected: Build succeeds with 0 errors, 0 warnings

**Step 6: Create test for service resolution**
Create: `tests/RealmsOfIdle.Client.Maui.Tests/MauiServiceResolutionTests.cs`

```csharp
using RealmsOfIdle.Client.Maui;
using RealmsOfIdle.Core.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace RealmsOfIdle.Client.Maui.Tests;

public class MauiServiceResolutionTests
{
    [Fact]
    public void MauiProgram_CreatesMauiApp()
    {
        // Act
        var mauiApp = MauiProgram.CreateMauiApp();

        // Assert
        Assert.NotNull(mauiApp);
        Assert.NotNull(mauiApp.Services);
    }

    [Fact]
    public void Services_CanBeResolved()
    {
        // Arrange & Act
        var mauiApp = MauiProgram.CreateMauiApp();
        var services = mauiApp.Services;

        // Assert - Core services
        Assert.NotNull(services.GetService<IEventStore>());
        Assert.NotNull(services.GetService<IGameLogger>());

        // Assert - Game mode services
        Assert.NotNull(services.GetService<IGameModeService>());

        // Assert - Primary IGameService (default is LocalGameService)
        var gameService = services.GetService<IGameService>();
        Assert.NotNull(gameService);
        Assert.IsType<LocalGameService>(gameService);
    }

    [Fact]
    public void MauiProject_UsesCorrectMauiSdk()
    {
        // Arrange
        var solutionRoot = GetSolutionRoot();
        var projectFile = Path.Combine(solutionRoot, "src", "RealmsOfIdle.Client.Maui", "RealmsOfIdle.Client.Maui.csproj");
        var content = File.ReadAllText(projectFile);

        // Assert
        Assert.Contains("Microsoft.NET.Sdk.Maui", content);
    }

    [Fact]
    public void MauiProject_ReferencesSharedProject()
    {
        // Arrange
        var solutionRoot = GetSolutionRoot();
        var projectFile = Path.Combine(solutionRoot, "src", "RealmsOfIdle.Client.Maui", "RealmsOfIdle.Client.Maui.csproj");
        var content = File.ReadAllText(projectFile);

        // Assert
        Assert.Contains("RealmsOfIdle.Client.Shared", content);
    }

    private string GetSolutionRoot()
    {
        // Traverse up to find .slnx file
        var dir = Directory.GetCurrentDirectory();
        while (dir != null)
        {
            if (Directory.GetFiles(dir, "*.slnx").Length > 0)
                return dir;
            dir = Path.GetDirectoryName(dir);
        }
        throw new DirectoryNotFoundException("Solution root not found");
    }
}
```

**Step 7: Run tests**

```bash
dotnet test tests/RealmsOfIdle.Client.Maui.Tests -v d
```

Expected: All tests pass

**Step 8: Update Task 1 in plan as complete**

```bash
# Update plan to mark Task 1 complete
```

**Step 9: Commit**

```bash
git add tests/RealmsOfIdle.Client.Maui.Tests/
git add docs/plans/2026-02-05-validation-gaps-v2.md
git commit -m "test(maui): verify MAUI app structure and service resolution

- Verified MAUI project uses correct SDK
- Verified MauiProgram.cs registers shared services
- Verified App.xaml.cs entry point
- Tests confirm services can be resolved
- Task 1 verified complete"
```

**Quality Criteria**:

- MAUI app builds and runs
- All services registered in MAUI DI container
- Tests verify service resolution
- Tests verify MAUI SDK is correct
- Tests verify Shared project reference
- 0 build warnings

---

## Task 3: Implement Blazor WASM Client (OR Document Decision to Skip)

**DoR (Definition of Ready)**:

- [ ] Task 2 completed (MAUI app verified)
- [ ] User confirms Blazor WASM client is needed (vs MAUI-only)
- [ ] Blazor WASM tech stack reviewed (.NET 10, Blazor WebAssembly)
- [ ] API contract reviewed (what endpoints does Blazor need?)
- [ ] Online-only game service design approved

**DoD (Definition of Done)**:

- [ ] Decision documented (implement OR skip with rationale)
- [ ] If implemented: Blazor project created and builds
- [ ] If implemented: HTTP game service communicates with API
- [ ] If implemented: Tests verify Blazor can call API
- [ ] If skipped: Rationale documented in plan

**Note**: Original task numbering was Task 2, now Task 3 due to insertion of new Task 1.

**Analysis Required**:

**Step 1: Review project requirements**
Check if Blazor WASM is needed for MVP or if MAUI client is sufficient.

**Step 2a: IF implementing Blazor**

Create: `src/RealmsOfIdle.Client.Blazor/RealmsOfIdle.Client.Blazor.csproj`

```xml
<Project Sdk="Microsoft.NET.Sdk.BlazorWebAssembly">
  <PropertyGroup>
    <TargetFramework>net10.0</TargetFramework>
    <ImplicitUsings>enable</ImplicitUsings>
    <Nullable>enable</Nullable>
  </PropertyGroup>

  <ItemGroup>
    <PackageReference Include="Microsoft.AspNetCore.Components.WebAssembly" Version="10.0.0" />
    <PackageReference Include="Microsoft.AspNetCore.Components.WebAssembly.DevServer" Version="10.0.0" />
  </ItemGroup>

  <ItemGroup>
    <ProjectReference Include="..\RealmsOfIdle.Core\RealmsOfIdle.Core.csproj" />
  </ItemGroup>
</Project>
```

Create: `src/RealmsOfIdle.Client.Blazor/Program.cs` with service registration and HttpClient.

Create: `src/RealmsOfIdle.Client.Blazor/HttpGameService.cs` for API communication.

Create: `src/RealmsOfIdle.Client.Blazor/Components/App.razor` for UI.

Create: `tests/RealmsOfIdle.Client.Blazor.Tests/ProgramTests.cs`

**Step 2b: IF skipping Blazor**

Document rationale in plan:

```markdown
## Decision: Blazor WASM Client Deferred

**Rationale**:

- MAUI client provides cross-platform coverage (Windows, macOS, iOS, Android)
- Blazor would add redundant web client
- MVP can launch with MAUI client only
- Blazor can be added later when web client is required

**Evidence**:

- MAUI client is functional
- Core domain is shared between clients
- HTTP game service exists for future online mode
```

**Step 3: Verify and commit**

```bash
dotnet build # (if implemented) or skip
git commit -m "docs(decision): defer Blazor WASM client to post-MVP"
```

**Quality Criteria**:

- Decision documented with clear rationale
- Evidence that current solution is sufficient
- Path forward documented if Blazor needed later

---

## Task 4: Update Solution File

**DoR (Definition of Ready)**:

- [ ] Current solution file (RealmsOfIdle.slnx) reviewed
- [ ] Blazor project needs to be added (if Task 2 implemented)
- [ ] Test projects need to be added (if not already present)

**DoD (Definition of Done)**:

- [ ] All projects in solution are building successfully
- [ ] Solution file includes all necessary projects
- [ ] `dotnet build` from solution root succeeds
- [ ] Tests can be run from solution root

**Implementation**:

**Step 1: Review current solution**

```bash
cat RealmsOfIdle.slnx
```

**Step 2: Add missing projects (if any)**

```bash
dotnet sln add src/RealmsOfIdle.Client.Maui/RealmsOfIdle.Client.Maui.csproj
# Add Blazor project if Task 2 implemented it
```

**Step 3: Verify solution builds**

```bash
dotnet build
```

Expected: All projects build successfully

**Step 4: Commit**

```bash
git add RealmsOfIdle.slnx
git commit -m "chore(sln): update solution file with all projects"
```

**Quality Criteria**:

- Solution builds from root with 0 errors
- All projects included
- Tests can run with `dotnet test` from root

---

## Task 5: Update Start Script to Include Orleans

**DoR (Definition of Ready)**:

- [ ] Current start script reviewed (scripts/start-stack.ps1 or similar)
- [ ] Orleans startup requirements identified (ports, timing, health check)
- [ ] AppHost orchestration reviewed

**DoD (Definition of Done)**:

- [ ] Start script includes Orleans silo startup
- [ ] Start script includes health check verification
- [ ] Script tests Orleans health endpoint before proceeding
- [ ] Script handles Orleans startup failures gracefully
- [ ] Documentation updated with startup instructions

**Implementation**:

**Step 1: Review current start script**

```bash
cat scripts/start-stack.ps1
```

**Step 2: Add Orleans startup**

```powershell
# Start Orleans Silo
Write-Host "Starting Orleans silo..." -ForegroundColor Cyan
$orleansJob = Start-Job -ScriptBlock {
    dotnet run --project src/RealmsOfIdle.Server.Orleans
}

# Wait for Orleans to be healthy
Write-Host "Waiting for Orleans health..." -ForegroundColor Yellow
$maxAttempts = 30
$attempt = 0
$orleansReady = $false

while (-not $orleansReady -and $attempt -lt $maxAttempts) {
    $attempt++
    try {
        $response = Invoke-WebRequest -Uri "http://localhost:8080/health" -UseBasicParsing -TimeoutSec 2
        if ($response.StatusCode -eq 200) {
            $orleansReady = $true
            Write-Host "✓ Orleans is ready" -ForegroundColor Green
        }
    } catch {
        Write-Host "Waiting... ($attempt/$maxAttempts)" -NoNewline
    }
    Start-Sleep -Seconds 2
}

if (-not $orleansReady) {
    Write-Host "✗ Orleans failed to start" -ForegroundColor Red
    exit 1
}
```

**Step 3: Test script**

```bash
pwsh scripts/start-stack.ps1 &
sleep 30
curl http://localhost:8080/health
# Verify other services
```

**Step 4: Commit**

```bash
git add scripts/start-stack.ps1
git commit -m "fix(scripts): add Orleans startup with health check verification"
```

**Quality Criteria**:

- Orleans starts successfully
- Health check verified before proceeding
- Script handles failures gracefully
- Documentation updated

---

## Task 6: End-to-End Validation

**DoR (Definition of Ready)**:

- [ ] All previous tasks complete (Tasks 1-4)
- [ ] All services build and test locally
- [ ] TestContainers available for integration testing

**DoD (Definition of Done)**:

- [ ] Full stack starts successfully (Orleans + API + PostgreSQL + Redis)
- [ ] Health endpoints return healthy status
- [ ] MAUI client can resolve services from DI
- [ ] E2E test validates full stack
- [ ] Validation report updated with all fixes
- [ ] No blocking issues remaining

**Implementation**:

**Step 1: Start full stack**

```bash
dotnet run --project src/RealmsOfIdle.AppHost
```

**Step 2: Verify Orleans**

```bash
curl http://localhost:8080/health
```

Expected: `{"status":"Healthy"}` or similar

**Step 3: Verify API**

```bash
curl https://localhost:7001/health
```

Expected: API returns healthy (possibly Degraded if Orleans not fully ready)

**Step 4: Verify MAUI DI**
Run MAUI tests:

```bash
dotnet test tests/RealmsOfIdle.Client.Maui.Tests
```

Expected: Service resolution tests pass

**Step 5: Create comprehensive E2E test**
Create or update: `tests/RealmsOfIdle.Server.E2ETests/FullStackTests.cs`

**Step 6: Update validation report**
Document all fixes, current state, and remaining issues.

**Step 7: Final commit**

```bash
git add docs/validation-report.md
git commit -m "docs(validation): update with full stack validation results"
```

**Quality Criteria**:

- All components start and communicate
- Health checks pass
- E2E tests validate full stack
- Validation report accurate and complete
- 0 blocking issues

---

## Task 7: Update Validation Report

**DoR (Definition of Ready)**:

- [ ] Task 5 complete (full stack validated)
- [ ] All validation gaps identified and addressed
- [ ] Test results collected

**DoD (Definition of Done)**:

- [ ] Validation report updated with current state
- [ ] All tasks marked as complete
- [ ] Executive summary accurate
- [ ] Remaining issues (if any) documented
- [ ] Recommendations for future work

**Implementation**:

Update `docs/validation-report.md` with:

- Executive summary (100% status)
- Component status:
  - Orleans Server: ✅ 100% Complete
  - API Server: ✅ 100% Complete
  - AppHost: ✅ 100% Complete
  - MAUI Client: ✅ 100% Complete
- Test results summary
- Known issues (if any)
- Recommendations

**Commit**:

```bash
git add docs/validation-report.md
git commit -m "docs(validation): mark validation gaps resolved - 100% complete"
```

**Quality Criteria**:

- Report accurately reflects current state
- All gaps addressed or documented
- Clear path forward

---

## Completion Checklist

- [ ] Task 1: Client library renamed, proper MAUI project created
- [ ] Task 2: MAUI app entry point and service resolution verified
- [ ] Task 3: Blazor client implemented OR decision documented
- [ ] Task 4: Solution file updated
- [ ] Task 5: Start script includes Orleans
- [ ] Task 6: E2E validation performed
- [ ] Task 7: Validation report updated

---

## Post-Execution Validation

After completing all tasks:

```bash
# 1. Full stack should start
dotnet run --project src/RealmsOfIdle.AppHost

# 2. Health endpoints should return 200
curl http://localhost:8080/health  # Orleans
curl https://localhost:7001/health # API

# 3. All tests should pass
dotnet test

# 4. MAUI services should resolve
dotnet test tests/RealmsOfIdle.Client.Maui.Tests
```

**Expected Result**: All commands succeed, full stack operational

---

## Execution Log

### 2026-02-05 Task 1 DoR Verification - BLOCKED

**Finding**: `RealmsOfIdle.Client.Maui` is NOT a MAUI project

**Evidence**:

```xml
<!-- Current Project SDK -->
<Project Sdk="Microsoft.NET.Sdk">  <!-- ❌ Wrong - regular class library -->

<!-- Expected for MAUI -->
<Project Sdk="Microsoft.NET.Sdk.Maui">  <!-- ✅ Correct MAUI SDK -->
```

**Analysis**:

- Project is named `RealmsOfIdle.Client.Maui` but uses `Microsoft.NET.Sdk`
- No MAUI packages or infrastructure present
- No XAML files, no App.xaml, no MauiProgram.cs
- Services exist (GameModeService, LocalGameService, etc.) but it's a class library, not a MAUI app

**Implications**:

- Task 1 cannot proceed as written - there is no MAUI application to create an entry point for
- Plan assumption was incorrect: "MAUI entry point not created" assumes MAUI project exists
- Original validation report (which no longer exists) likely had incorrect information

**Required Decision**:

1. Convert project to proper MAUI project (change SDK, add packages, add MAUI infrastructure)
2. Rename project to reflect actual purpose (e.g., `RealmsOfIdle.Client.Shared`)
3. Document that MAUI client was never created and defer it to post-MVP

**Status**: BLOCKED awaiting decision on MAUI client approach

### 2026-02-05 Plan Updated - Task 1 Inserted

**Decision**: User chose option 2 - rename existing project to `RealmsOfIdle.Client.Shared` and create a proper MAUI project.

**Actions Taken**:

1. Created new Task 1: "Rename Client Library and Create Proper MAUI Project"
   - Full DoR/DoD checklists
   - 12-step implementation plan
   - Quality criteria defined
2. Updated old Task 1 → Task 2: "Verify MAUI Application Entry Point and Service Resolution"
   - Changed from creation to verification task
   - Now verifies Task 1 completion
   - Added tests for MAUI SDK, Shared project reference, service resolution
3. Renumbered remaining tasks (Task 2→Task 3, Task 3→Task 4, etc.)
4. Updated completion checklist
5. Updated plan version to 2.1

**Plan Structure**:

- Task 1: Rename client library + create proper MAUI project
- Task 2: Verify MAUI app entry point and service resolution
- Task 3: Implement Blazor WASM client (or skip)
- Task 4: Update solution file
- Task 5: Update start script
- Task 6: End-to-end validation
- Task 7: Update validation report

**Status**: Ready to execute Task 1
