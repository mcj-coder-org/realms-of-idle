#!/bin/bash

# Development Environment Setup Script
# This script sets up a new development environment for the project

set -e

# Color codes for output
RED='\033[0;31m'
GREEN='\033[0;32m'
YELLOW='\033[1;33m'
NC='\033[0m' # No Color

echo -e "${GREEN}🚀 Setting up development environment...${NC}"

# Check Node.js version
echo "📦 Checking Node.js version..."
NODE_VERSION=$(node --version)
echo "Node.js version: $NODE_VERSION"

if ! node --version | grep -q "v22"; then
    echo -e "${YELLOW}⚠️ Warning: Recommended Node.js version is v22 or higher${NC}"
fi

# Install dependencies
echo "📥 Installing npm dependencies..."
npm install

# Set up Git hooks
echo "🔗 Setting up Git hooks..."
npm run prepare

# Verify setup
echo "✅ Verifying setup..."

# Check if required files exist
REQUIRED_FILES=(
    ".husky/pre-commit"
    ".husky/commit-msg"
    "package.json"
    ".gitignore"
)

for file in "${REQUIRED_FILES[@]}"; do
    if [ -f "$file" ]; then
        echo -e "  ✅ $file"
    else
        echo -e "  ❌ $file - Missing!"
    fi
done

# Check if pre-commit hook is executable
if [ -x ".husky/pre-commit" ]; then
    echo -e "  ✅ Pre-commit hook is executable"
else
    echo -e "  ❌ Pre-commit hook is not executable"
    chmod +x .husky/pre-commit
fi

# Display next steps
echo ""
echo -e "${GREEN}🎉 Development environment setup complete!${NC}"
echo ""
echo "📋 Next steps:"
echo "1. Configure GPG signing for commits:"
echo "   - Follow: docs/playbooks/enable-signed-commits.md"
echo "   - Run: git config --global commit.gpgsign true"
echo ""
echo "2. Switch to contributor account:"
echo "   - Run: git config user.email 'm.c.j@live.co.uk'"
echo "   - Run: git config user.name 'mcj-codificer'"
echo "   - Run: gh auth switch"
echo ""
echo "3. Create a feature branch:"
echo "   - Run: .github/templates/create-feature-branch.sh <issue-number> <description>"
echo ""
echo "4. Start coding! 🚀"