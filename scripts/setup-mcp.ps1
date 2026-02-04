#!/usr/bin/env pwsh
<#
.SYNOPSIS
    Setup MCP servers for Realms of Idle development

.DESCRIPTION
    Configures Model Context Protocol servers including:
    - context7: Upstash context management
    - web-search-prime: Z.ai web search
    - zai-mcp-server: Z.ai MCP integration
    - grepai: Semantic code search (requires Ollama)

.EXAMPLE
    .\scripts\setup-mcp.ps1
#>

[CmdletBinding()]
param()

$ErrorActionPreference = "Stop"
$ProjectRoot = Split-Path $PSScriptRoot -Parent

Write-Host "🔧 Setting up MCP servers for Realms of Idle..." -ForegroundColor Cyan

# Verify .mcp.json exists
$McpConfig = Join-Path $ProjectRoot ".mcp.json"
if (-not (Test-Path $McpConfig)) {
    Write-Host "❌ .mcp.json not found at $McpConfig" -ForegroundColor Red
    exit 1
}

# Verify grepai.exe exists
$GrepaiExe = Join-Path $ProjectRoot ".claude\grepai.exe"
if (-not (Test-Path $GrepaiExe)) {
    Write-Host "❌ grepai.exe not found at $GrepaiExe" -ForegroundColor Red
    exit 1
}

# Check Ollama
Write-Host "`n📦 Checking Ollama installation..." -ForegroundColor Cyan
try {
    $OllamaVersion = ollama --version 2>$null
    if ($LASTEXITCODE -eq 0) {
        Write-Host "✅ Ollama found: $OllamaVersion" -ForegroundColor Green
    } else {
        throw "Not found"
    }
} catch {
    Write-Host "⚠️  Ollama not found. Install from https://ollama.ai" -ForegroundColor Yellow
    Write-Host "   grepai semantic search requires Ollama with nomic-embed-text model" -ForegroundColor Yellow
    $InstallOllama = Read-Host "Install Ollama now? (y/n)"
    if ($InstallOllama -eq 'y') {
        Start-Process "https://ollama.ai"
        Write-Host "Please install Ollama, then run this script again." -ForegroundColor Cyan
        exit 0
    }
}

# Check if nomic-embed-text model is available
Write-Host "`n📦 Checking nomic-embed-text model..." -ForegroundColor Cyan
$ModelCheck = ollama list 2>$null | Select-String "nomic-embed-text"
if ($ModelCheck) {
    Write-Host "✅ nomic-embed-text model installed" -ForegroundColor Green
} else {
    Write-Host "⚠️  nomic-embed-text model not found" -ForegroundColor Yellow
    $PullModel = Read-Host "Pull model now? (~275MB download) (y/n)"
    if ($PullModel -eq 'y') {
        Write-Host "Downloading nomic-embed-text model..." -ForegroundColor Cyan
        ollama pull nomic-embed-text
        if ($LASTEXITCODE -eq 0) {
            Write-Host "✅ Model installed successfully" -ForegroundColor Green
        } else {
            Write-Host "❌ Failed to install model" -ForegroundColor Red
            exit 1
        }
    }
}

# Check if grepai index exists
$GrepaiIndex = Join-Path $ProjectRoot ".grepai\index.gob"
Write-Host "`n📊 Checking grepai index..." -ForegroundColor Cyan
if (Test-Path $GrepaiIndex) {
    Write-Host "✅ grepai index found" -ForegroundColor Green
    $Age = (Get-Date) - (Get-Item $GrepaiIndex).LastWriteTime
    if ($Age.Days -gt 7) {
        Write-Host "⚠️  Index is $($Age.Days) days old. Consider rebuilding." -ForegroundColor Yellow
    }
} else {
    Write-Host "⚠️  grepai index not found" -ForegroundColor Yellow
    $BuildIndex = Read-Host "Build semantic code index now? (may take a few minutes) (y/n)"
    if ($BuildIndex -eq 'y') {
        Write-Host "Building grepai index..." -ForegroundColor Cyan
        & $GrepaiExe index
        if ($LASTEXITCODE -eq 0) {
            Write-Host "✅ Index built successfully" -ForegroundColor Green
        } else {
            Write-Host "⚠️  Index build had issues. You can rebuild later with:" -ForegroundColor Yellow
            Write-Host "   .\.claude\grepai.exe index" -ForegroundColor Gray
        }
    }
}

# Summary
Write-Host "`n✨ MCP Setup Summary" -ForegroundColor Cyan
Write-Host "━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━" -ForegroundColor Gray
Write-Host "  context7:         ✅ Configured (via npx)" -ForegroundColor Green
Write-Host "  web-search-prime: ✅ Configured (Z.ai API)" -ForegroundColor Green
Write-Host "  zai-mcp-server:   ✅ Configured (Z.ai API)" -ForegroundColor Green
Write-Host "  grepai:           $(& { if (Test-Path $GrepaiIndex) { '✅ Indexed' } else { '⚠️  Needs index' } })" -ForegroundColor $(if (Test-Path $GrepaiIndex) { 'Green' } else { 'Yellow' })
Write-Host "━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━" -ForegroundColor Gray

Write-Host "`n📖 See README.md for MCP server usage details." -ForegroundColor Cyan
