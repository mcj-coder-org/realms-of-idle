#!/bin/bash
# Run IntegrationTests via WSL with Podman
# This script runs inside WSL where Podman socket is natively available

set -e

echo "========================================="
echo "RealmsOfIdle Integration Tests (WSL)"
echo "========================================="

# Detect if running in WSL
if [[ ! -f /proc/version ]] || ! grep -qi "microsoft" /proc/version; then
    echo "Error: This script must be run inside WSL"
    echo "Use 'wsl bash run-tests-wsl.sh' from Windows"
    exit 1
fi

# Check if .NET is installed
if ! command -v dotnet &> /dev/null; then
    echo ".NET not found in WSL. Installing .NET 10 SDK..."

    # Download .NET install script
    DOTNET_INSTALL_DIR="$HOME/.dotnet"
    mkdir -p "$DOTNET_INSTALL_DIR"

    curl -sSL https://dot.net/v1/dotnet-install.sh -o dotnet-install.sh
    chmod +x dotnet-install.sh

    # Install .NET 10 SDK
    ./dotnet-install.sh --channel 10.0 --install-dir "$DOTNET_INSTALL_DIR"

    # Add to PATH for this session
    export DOTNET_ROOT="$DOTNET_INSTALL_DIR"
    export PATH="$DOTNET_ROOT:$PATH"

    # Add to .bashrc for future sessions
    if ! grep -q 'dotnet' ~/.bashrc 2>/dev/null; then
        echo "" >> ~/.bashrc
        echo "# .NET setup" >> ~/.bashrc
        echo "export DOTNET_ROOT=\"\$HOME/.dotnet\"" >> ~/.bashrc
        echo "export PATH=\"\$DOTNET_ROOT:\$PATH\"" >> ~/.bashrc
    fi

    rm dotnet-install.sh
    echo ".NET 10 SDK installed successfully"
fi

# Display .NET version
echo "Using .NET version: $(dotnet --version)"

# Verify Podman is running
if ! podman ps > /dev/null 2>&1; then
    echo "Error: Podman is not running or not accessible"
    echo "Start Podman Desktop or run 'podman machine start' from Windows"
    exit 1
fi

# Get Podman socket path
PODMAN_SOCKET=$(podman info --format '{{.Host.RemoteSocket.Path}}' 2>/dev/null || echo "/run/user/1001/podman/podman.sock")
echo "Podman socket: unix://$PODMAN_SOCKET"

# Setup .testcontainers.properties in WSL home
TESTCONTAINERS_PROP="$HOME/.testcontainers.properties"
echo "docker.host=unix://$PODMAN_SOCKET" > "$TESTCONTAINERS_PROP"
echo "Configured $TESTCONTAINERS_PROP"

# Set environment variables for Testcontainers
export TESTCONTAINERS_DOCKER_SOCKET_OVERRIDE=/var/run/docker.sock
export TESTCONTAINERS_RYUK_DISABLED=true

# Get the script directory (in WSL path format)
SCRIPT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"

# Determine which tests to run
TEST_FILTER="${1:-}"
TEST_PROJECT="$SCRIPT_DIR/tests/RealmsOfIdle.Server.IntegrationTests"

echo ""
echo "Running IntegrationTests..."
echo "========================================="

if [[ -n "$TEST_FILTER" ]]; then
    dotnet test "$TEST_PROJECT" --filter "$TEST_FILTER" --verbosity normal
else
    dotnet test "$TEST_PROJECT" --verbosity normal
fi

exit $?
