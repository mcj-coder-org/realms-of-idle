#!/usr/bin/env bash
set -euo pipefail

# Phase Completion Validation Script
# Runs the full test suite including E2E tests against a live AppHost.
#
# Usage: bash scripts/validate-phase.sh
#
# Requirements:
#   - Docker running (for TestContainers integration tests)
#   - aspire CLI installed (dotnet tool)
#   - .env file (copy from .env.example)

# ── Colors ──────────────────────────────────────────────────────────────────

RED='\033[0;31m'
GREEN='\033[0;32m'
YELLOW='\033[1;33m'
CYAN='\033[0;36m'
BOLD='\033[1m'
NC='\033[0m' # No Color

# ── Helpers ─────────────────────────────────────────────────────────────────

info()  { echo -e "${CYAN}[INFO]${NC}  $*"; }
ok()    { echo -e "${GREEN}[OK]${NC}    $*"; }
warn()  { echo -e "${YELLOW}[WARN]${NC}  $*"; }
fail()  { echo -e "${RED}[FAIL]${NC}  $*"; }
header() { echo -e "\n${BOLD}═══ $* ═══${NC}\n"; }

# ── Locate repo root ───────────────────────────────────────────────────────

SCRIPT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
REPO_ROOT="$(cd "$SCRIPT_DIR/.." && pwd)"
cd "$REPO_ROOT"

# ── State tracking ──────────────────────────────────────────────────────────

APPHOST_PID=""
APPHOST_LOG="/tmp/apphost-validate-$$.log"
STEP_RESULTS=()
FAILED=0

record_result() {
    local step="$1" result="$2"
    STEP_RESULTS+=("$result $step")
    if [[ "$result" == "FAIL" ]]; then
        FAILED=1
    fi
}

# ── Cleanup trap ────────────────────────────────────────────────────────────

cleanup() {
    if [[ -n "$APPHOST_PID" ]]; then
        info "Stopping AppHost..."
        # Kill all processes in the AppHost tree (aspire spawns dotnet children)
        pkill -f "RealmsOfIdle.AppHost" 2>/dev/null || true
        sleep 2
        # Force kill any survivors
        pkill -9 -f "RealmsOfIdle.AppHost" 2>/dev/null || true
        wait "$APPHOST_PID" 2>/dev/null || true
        APPHOST_PID=""
    fi

    # Print summary
    header "Validation Summary"
    for entry in "${STEP_RESULTS[@]}"; do
        local result="${entry%% *}"
        local step="${entry#* }"
        if [[ "$result" == "PASS" ]]; then
            ok "$step"
        elif [[ "$result" == "SKIP" ]]; then
            warn "$step (skipped)"
        else
            fail "$step"
        fi
    done
    echo ""

    if [[ "$FAILED" -eq 0 ]]; then
        echo -e "${GREEN}${BOLD}All validations passed.${NC}"
        rm -f "$APPHOST_LOG"
    else
        echo -e "${RED}${BOLD}Validation failed.${NC}"
        if [[ -f "$APPHOST_LOG" ]]; then
            echo -e "\n${YELLOW}Last 50 lines of AppHost log:${NC}"
            tail -50 "$APPHOST_LOG"
            echo -e "\n${YELLOW}Full log: ${APPHOST_LOG}${NC}"
            echo -e "${YELLOW}Tip: Use Aspire MCP tools (list_resources, list_console_logs) for deeper investigation.${NC}"
        fi
    fi

    exit "$FAILED"
}

trap cleanup EXIT

# ── Load environment ────────────────────────────────────────────────────────

load_env() {
    local env_file="$REPO_ROOT/.env"
    if [[ -f "$env_file" ]]; then
        # shellcheck source=/dev/null
        set -a
        source "$env_file"
        set +a
    fi
}

# Defaults (overridden by .env if present)
API_HTTP_PORT="${API_HTTP_PORT:-5214}"
ORLEANS_HTTP_PORT="${ORLEANS_HTTP_PORT:-5001}"

load_env

APPHOST_PROJECT="src/RealmsOfIdle.AppHost/RealmsOfIdle.AppHost.csproj"

# ── Step 1: Pre-flight checks ──────────────────────────────────────────────

header "Pre-flight Checks"

# Docker
if docker info &>/dev/null; then
    ok "Docker is running"
else
    fail "Docker is not running. Start it with: sudo service docker start"
    record_result "Pre-flight: Docker" "FAIL"
    exit 1
fi

# aspire CLI
if command -v aspire &>/dev/null; then
    ok "aspire CLI found ($(aspire --version 2>/dev/null | head -1))"
else
    fail "aspire CLI not found. Install with: dotnet tool install -g aspire"
    record_result "Pre-flight: aspire CLI" "FAIL"
    exit 1
fi

# .env file
if [[ -f "$REPO_ROOT/.env" ]]; then
    ok ".env file found"
else
    warn ".env file not found — using defaults from .env.example"
    if [[ -f "$REPO_ROOT/.env.example" ]]; then
        cp "$REPO_ROOT/.env.example" "$REPO_ROOT/.env"
        load_env
        ok "Created .env from .env.example"
    else
        fail ".env.example not found either"
        record_result "Pre-flight: .env" "FAIL"
        exit 1
    fi
fi

# AppHost project
if [[ -f "$REPO_ROOT/$APPHOST_PROJECT" ]]; then
    ok "AppHost project found"
else
    fail "AppHost project not found at $APPHOST_PROJECT"
    record_result "Pre-flight: AppHost project" "FAIL"
    exit 1
fi

record_result "Pre-flight checks" "PASS"

# ── Step 2: Kill stale AppHost processes ────────────────────────────────────

header "Stale Process Cleanup"

kill_stale_apphost() {
    if pgrep -f "RealmsOfIdle.AppHost" &>/dev/null; then
        warn "Found stale AppHost processes"
        pkill -f "RealmsOfIdle.AppHost" 2>/dev/null || true
        sleep 2
        # Force kill any survivors
        pkill -9 -f "RealmsOfIdle.AppHost" 2>/dev/null || true
        ok "Stale processes cleaned up"
    else
        ok "No stale AppHost processes found"
    fi
}

kill_stale_apphost

# ── Step 3: Build solution ──────────────────────────────────────────────────

header "Build Solution"

if dotnet build RealmsOfIdle.slnx --verbosity quiet; then
    ok "Solution built successfully"
    record_result "Build" "PASS"
else
    fail "Solution build failed"
    record_result "Build" "FAIL"
    exit 1
fi

# ── Step 4: Unit / Architecture / System tests ─────────────────────────────

header "Unit / Architecture / System Tests"

if dotnet test RealmsOfIdle.slnx \
    --no-build \
    --filter "Category!=E2E&Category!=Integration" \
    --verbosity normal; then
    ok "Unit / Architecture / System tests passed"
    record_result "Unit / Architecture / System tests" "PASS"
else
    fail "Unit / Architecture / System tests failed"
    record_result "Unit / Architecture / System tests" "FAIL"
    exit 1
fi

# ── Step 5: Integration tests (TestContainers) ─────────────────────────────

header "Integration Tests (TestContainers)"

if dotnet test RealmsOfIdle.slnx \
    --no-build \
    --filter "Category=Integration" \
    --verbosity normal; then
    ok "Integration tests passed"
    record_result "Integration tests" "PASS"
else
    fail "Integration tests failed"
    record_result "Integration tests" "FAIL"
    exit 1
fi

# ── Step 6: Start AppHost ───────────────────────────────────────────────────

header "Start AppHost"

info "Starting AppHost (log: $APPHOST_LOG)..."

aspire run \
    --non-interactive \
    --project "$REPO_ROOT/$APPHOST_PROJECT" \
    -- --no-build \
    > "$APPHOST_LOG" 2>&1 &
APPHOST_PID=$!

info "AppHost started (PID: $APPHOST_PID)"

# ── Step 7: Poll health endpoints ──────────────────────────────────────────

header "Health Check Polling"

poll_health() {
    local url="$1" name="$2" timeout="${3:-120}" interval=3
    local elapsed=0

    info "Waiting for $name at $url (timeout: ${timeout}s)..."

    while [[ $elapsed -lt $timeout ]]; do
        # Check if AppHost process is still alive
        if ! kill -0 "$APPHOST_PID" 2>/dev/null; then
            fail "AppHost process died unexpectedly"
            return 1
        fi

        local status
        status=$(curl -s -o /dev/null -w "%{http_code}" "$url" 2>/dev/null || echo "000")

        if [[ "$status" == "200" ]]; then
            ok "$name is healthy (${elapsed}s)"
            return 0
        fi

        sleep "$interval"
        elapsed=$((elapsed + interval))
    done

    fail "$name did not become healthy within ${timeout}s (last status: $status)"
    return 1
}

API_HEALTHY=0
ORLEANS_HEALTHY=0

if poll_health "http://localhost:${API_HTTP_PORT}/health" "API"; then
    API_HEALTHY=1
fi

if poll_health "http://localhost:${ORLEANS_HTTP_PORT}/health" "Orleans"; then
    ORLEANS_HEALTHY=1
fi

if [[ $API_HEALTHY -eq 1 && $ORLEANS_HEALTHY -eq 1 ]]; then
    ok "All services healthy"
    record_result "AppHost health checks" "PASS"
else
    fail "Not all services became healthy"
    record_result "AppHost health checks" "FAIL"
    exit 1
fi

# ── Step 7.5: Extract dashboard token ──────────────────────────────────────

header "Dashboard Token Extraction"

DASHBOARD_TOKEN=$(grep -oP 'login\?t=\K[a-f0-9]+' "$APPHOST_LOG" | head -1)

if [[ -n "$DASHBOARD_TOKEN" ]]; then
    export E2E_ASPIRE_DASHBOARD_TOKEN="$DASHBOARD_TOKEN"
    ok "Dashboard token extracted"
else
    warn "Could not extract dashboard token from AppHost log"
fi

# ── Step 8: E2E tests ──────────────────────────────────────────────────────

header "E2E Tests"

if dotnet test RealmsOfIdle.slnx \
    --no-build \
    --filter "Category=E2E" \
    --verbosity normal; then
    ok "E2E tests passed"
    record_result "E2E tests" "PASS"
else
    fail "E2E tests failed"
    record_result "E2E tests" "FAIL"
fi

# ── Cleanup happens in trap ────────────────────────────────────────────────
