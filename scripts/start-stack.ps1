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

    Requires .NET Aspire to be installed.
#>

$ErrorActionPreference = "Stop"
$ProgressPreference = "SilentlyContinue"

$RootDir = Split-Path -Parent $PSScriptRoot
$AppHostProject = "$RootDir\src\RealmsOfIdle.AppHost\RealmsOfIdle.AppHost.csproj"

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

# Start the AppHost
Write-Host "`n[START] Launching AppHost..." -ForegroundColor Cyan
Write-Host "        This will start all services and open the dashboard." -ForegroundColor Gray
Write-Host "`n        Press Ctrl+C to stop all services." -ForegroundColor Yellow
Write-Host ""

dotnet run --project "$AppHostProject"
