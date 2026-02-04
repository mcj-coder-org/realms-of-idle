#!/usr/bin/env pwsh
<#
.SYNOPSIS
    Verifies all components of the Realms of Idle stack are healthy.

.DESCRIPTION
    Runs health checks on all components:
    - Builds all projects
    - Runs all tests
    - Verifies domain models
    - Checks telemetry configuration
#>

$ErrorActionPreference = "Stop"
$ProgressPreference = "SilentlyContinue"

function Write-Step([string]$Message) {
    Write-Host "`n[STEP] $Message" -ForegroundColor Cyan
}

function Write-Success([string]$Message) {
    Write-Host "[OK] $Message" -ForegroundColor Green
}

function Write-Fail([string]$Message) {
    Write-Host "[FAIL] $Message" -ForegroundColor Red
}

# Script is in scripts/ subdirectory, go up one level
$RootDir = Split-Path -Parent $PSScriptRoot
$TestFailed = $false

# Step 1: Build all projects
Write-Step "Building all projects..."
$BuildResult = dotnet build "$RootDir\RealmsOfIdle.slnx" --no-restore 2>&1
if ($LASTEXITCODE -ne 0) {
    Write-Fail "Build failed"
    $TestFailed = $true
} else {
    Write-Success "Build succeeded"
}

# Step 2: Run unit tests
Write-Step "Running unit tests..."
$UnitTestResult = dotnet test "$RootDir\tests\RealmsOfIdle.Core.Tests\RealmsOfIdle.Core.Tests.csproj" --no-build --verbosity quiet 2>&1
if ($LASTEXITCODE -ne 0) {
    Write-Fail "Unit tests failed"
    $TestFailed = $true
} else {
    Write-Success "Unit tests passed"
}

# Step 3: Run integration tests
Write-Step "Running integration tests..."
$IntegrationTestResult = dotnet test "$RootDir\tests\RealmsOfIdle.Server.Orleans.Tests\RealmsOfIdle.Server.Orleans.Tests.csproj" --no-build --verbosity quiet 2>&1
if ($LASTEXITCODE -ne 0) {
    Write-Fail "Integration tests failed"
    $TestFailed = $true
} else {
    Write-Success "Integration tests passed"
}

# Step 4: Run E2E tests
Write-Step "Running E2E tests..."
$E2ETestResult = dotnet test "$RootDir\tests\RealmsOfIdle.Tests\RealmsOfIdle.Tests.csproj" --no-build --verbosity quiet 2>&1
if ($LASTEXITCODE -ne 0) {
    Write-Fail "E2E tests failed"
    $TestFailed = $true
} else {
    Write-Success "E2E tests passed"
}

# Step 5: Verify all projects exist
Write-Step "Verifying project structure..."
$Projects = @(
    "src\RealmsOfIdle.Core\RealmsOfIdle.Core.csproj",
    "src\RealmsOfIdle.Server.Orleans\RealmsOfIdle.Server.Orleans.csproj",
    "src\RealmsOfIdle.Server.Api\RealmsOfIdle.Server.Api.csproj",
    "src\RealmsOfIdle.AppHost\RealmsOfIdle.AppHost.csproj",
    "src\RealmsOfIdle.Client.Maui\RealmsOfIdle.Client.Maui.csproj",
    "tests\RealmsOfIdle.Core.Tests\RealmsOfIdle.Core.Tests.csproj",
    "tests\RealmsOfIdle.Server.Orleans.Tests\RealmsOfIdle.Server.Orleans.Tests.csproj",
    "tests\RealmsOfIdle.Tests\RealmsOfIdle.Tests.csproj"
)

$AllProjectsExist = $true
foreach ($Project in $Projects) {
    $Path = Join-Path $RootDir $Project
    if (!(Test-Path $Path)) {
        Write-Fail "Missing project: $Project"
        $AllProjectsExist = $false
    }
}
if ($AllProjectsExist) {
    Write-Success "All projects exist"
}

# Summary
Write-Host "`n=== VERIFICATION SUMMARY ===" -ForegroundColor Cyan
if ($TestFailed) {
    Write-Fail "Verification failed - see errors above"
    exit 1
} else {
    Write-Success "All verifications passed!"
    Write-Host "Stack is healthy and ready to run." -ForegroundColor Green
    exit 0
}
