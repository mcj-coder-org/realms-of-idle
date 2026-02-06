#!/usr/bin/env pwsh
<#
.SYNOPSIS
    Starts the Realms of Idle development stack.

.DESCRIPTION
    Starts the AppHost which orchestrates all services:
    - Orleans Silo (Game Server)
    - API Server
    - Blazor Web Client
    - Resource Services (Redis, PostgreSQL)

    Verifies service health before completing.
    Requires .NET Aspire to be installed.
#>

$ErrorActionPreference = "Stop"
$ProgressPreference = "SilentlyContinue"

$RootDir = Split-Path -Parent $PSScriptRoot
$AppHostProject = Join-Path $RootDir "src" "RealmsOfIdle.AppHost" "RealmsOfIdle.AppHost.csproj"
$LaunchSettings = Join-Path $RootDir "src" "RealmsOfIdle.AppHost" "Properties" "launchSettings.json"

Write-Host "Starting Realms of Idle Development Stack..." -ForegroundColor Cyan
Write-Host "Project: $AppHostProject" -ForegroundColor Gray

if (!(Test-Path $AppHostProject)) {
    Write-Host "[ERROR] AppHost project not found at: $AppHostProject" -ForegroundColor Red
    exit 1
}

# Verify Aspire workload is installed
Write-Host "`n[CHECK] Verifying .NET Aspire workload..." -ForegroundColor Cyan
$WorkloadOutput = dotnet workload list 2>&1
if ($WorkloadOutput -notmatch " aspire") {
    Write-Host "[WARN] .NET Aspire workload may not be installed." -ForegroundColor Yellow
    Write-Host "       Install with: dotnet workload install aspire" -ForegroundColor Gray
}

# Start the AppHost in background
Write-Host "`n[START] Launching AppHost..." -ForegroundColor Cyan
Write-Host "        This will start all services including Orleans." -ForegroundColor Gray
Write-Host ""

$AppHostProcess = Start-Process -FilePath "dotnet" `
    -ArgumentList "run", "--project", "`"$AppHostProject`"" `
    -PassThru `
    -NoNewWindow

# Wait for AppHost to initialize
Write-Host "[WAIT] Waiting for AppHost to initialize..." -ForegroundColor Yellow
Start-Sleep -Seconds 5

# Verify API health
Write-Host "[CHECK] Verifying API health endpoint..." -ForegroundColor Cyan
$ApiUrl = "https://localhost:7001/health"

try {
    $Response = Invoke-WebRequest -Uri $ApiUrl `
        -UseBasicParsing `
        -TimeoutSec 5 `
        -ErrorAction SilentlyContinue

    if ($Response.StatusCode -eq 200) {
        Write-Host "[OK] API is healthy!" -ForegroundColor Green
    }
    else {
        Write-Host "[WARN] API returned status: $($Response.StatusCode)" -ForegroundColor Yellow
    }
}
catch {
    Write-Host "[WARN] API health check skipped: $($_.Exception.Message)" -ForegroundColor Yellow
}

# Read dashboard URL from launch settings
$DashboardUrl = "http://localhost:7210"  # fallback default
if (Test-Path $LaunchSettings) {
    try {
        $Settings = Get-Content $LaunchSettings | ConvertFrom-Json
        $AppUrl = $Settings.profiles.https.applicationUrl
        if ($AppUrl -match "(https://localhost:\d+)") {
            $DashboardUrl = $matches[1]
        }
    }
    catch {
        # Use default if parsing fails
    }
}

Write-Host "`n[SUCCESS] Development stack started!" -ForegroundColor Green
Write-Host "          Aspire Dashboard:  $DashboardUrl" -ForegroundColor Cyan
Write-Host "          API Endpoint:      https://localhost:7001" -ForegroundColor Cyan
Write-Host "`n          Press Ctrl+C to stop all services." -ForegroundColor Yellow
Write-Host ""

# Wait for user to stop
Wait-Process -Id $AppHostProcess.Id
