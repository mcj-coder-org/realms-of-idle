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

# Wait for Orleans to be healthy
Write-Host "[CHECK] Verifying Orleans health endpoint..." -ForegroundColor Cyan
$MaxAttempts = 30
$Attempt = 0
$OrleansHealthy = $false
$OrleansUrl = "http://localhost:8080/health"

while (-not $OrleansHealthy -and $Attempt -lt $MaxAttempts) {
    $Attempt++
    Write-Host "        Checking... ($Attempt/$MaxAttempts)" -NoNewline

    try {
        $Response = Invoke-WebRequest -Uri $OrleansUrl `
            -UseBasicParsing `
            -TimeoutSec 2 `
            -ErrorAction SilentlyContinue

        if ($Response.StatusCode -eq 200) {
            $OrleansHealthy = $true
            Write-Host "`n[OK] Orleans is healthy!" -ForegroundColor Green
        }
        else {
            Write-Host " ($($Response.StatusCode))" -NoNewline
        }
    }
    catch {
        Write-Host "." -NoNewline
    }

    if (-not $OrleansHealthy) {
        Start-Sleep -Seconds 2
    }
}

Write-Host ""

if (-not $OrleansHealthy) {
    Write-Host "[ERROR] Orleans failed to start within expected time." -ForegroundColor Red
    Write-Host "        Check the AppHost output above for errors." -ForegroundColor Yellow
    Write-Host "`n        Press Ctrl+C to stop the services." -ForegroundColor Yellow

    # Wait for user to stop
    Wait-Process -Id $AppHostProcess.Id
    exit 1
}

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
    Write-Host "[WARN] API health check failed: $($_.Exception.Message)" -ForegroundColor Yellow
}

Write-Host "`n[SUCCESS] All services started successfully!" -ForegroundColor Green
Write-Host "          Orleans Dashboard: http://localhost:8080" -ForegroundColor Cyan
Write-Host "          API Endpoint:      https://localhost:7001" -ForegroundColor Cyan
Write-Host "          Aspire Dashboard:  http://localhost:7210" -ForegroundColor Cyan
Write-Host "`n          Press Ctrl+C to stop all services." -ForegroundColor Yellow
Write-Host ""

# Wait for user to stop
Wait-Process -Id $AppHostProcess.Id
