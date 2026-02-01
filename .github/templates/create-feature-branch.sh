#!/bin/bash

# Feature Branch Creation Script
# This script creates a new feature branch following the project naming convention
# Usage: ./create-feature-branch.sh <issue-number> <branch-description>

set -e

# Check if git worktree is installed
if ! command -v git &> /dev/null; then
    echo "❌ Error: Git is not installed"
    exit 1
fi

# Check if GitHub CLI is installed
if ! command -v gh &> /dev/null; then
    echo "⚠️ Warning: GitHub CLI is not installed. Some features may not work."
fi

# Get arguments
if [ $# -ne 2 ]; then
    echo "❌ Usage: $0 <issue-number> <branch-description>"
    echo "Example: $0 42 add-inventory-system"
    exit 1
fi

ISSUE_NUMBER=$1
BRANCH_DESCRIPTION=$2

# Validate issue number
if ! [[ "$ISSUE_NUMBER" =~ ^[0-9]+$ ]]; then
    echo "❌ Error: Issue number must be a positive integer"
    exit 1
fi

# Clean and format branch description
CLEAN_DESCRIPTION=$(echo "$BRANCH_DESCRIPTION" | tr '[:upper:]' '[:lower:]' | sed 's/[^a-z0-9-]/-/g' | sed 's/--*/-/g' | sed 's/-$//')

# Create branch name
BRANCH_NAME="feat/${ISSUE_NUMBER}-${CLEAN_DESCRIPTION}"

# Check if we're on main branch
CURRENT_BRANCH=$(git rev-parse --abbrev-ref HEAD)
if [ "$CURRENT_BRANCH" != "main" ]; then
    echo "⚠️ Warning: You're not on main branch. Current branch: $CURRENT_BRANCH"
    read -p "Continue? (y/N): " -n 1 -r
    echo
    if [[ ! $REPLY =~ ^[Yy]$ ]]; then
        exit 1
    fi
fi

# Check if main branch is up to date
echo "🔄 Checking if main branch is up to date..."
git fetch origin main
LOCAL_MAIN=$(git rev-parse main)
REMOTE_MAIN=$(git rev-parse origin/main)

if [ "$LOCAL_MAIN" != "$REMOTE_MAIN" ]; then
    echo "⚠️ Warning: Your local main branch is not up to date with remote."
    echo "Pulling latest changes..."
    git pull origin main
fi

# Create and switch to new branch
echo "🌱 Creating feature branch: $BRANCH_NAME"
git worktree add "../$BRANCH_NAME" -b "$BRANCH_NAME"

# Switch to the new branch
echo "🔄 Switching to $BRANCH_NAME"
cd "../$BRANCH_NAME"

# Set up git configuration for this branch
echo "⚙️ Configuring git for this branch..."
if [ -f "../../.github/templates/gitconfig-feature" ]; then
    while IFS='=' read -r key value; do
        # Skip comments and empty lines
        [[ $key =~ ^[[:space:]]*# ]] && continue
        [[ -z "$key" ]] && continue

        # Remove whitespace and set config
        CLEAN_KEY=$(echo "$key" | sed 's/^[[:space:]]*//;s/[[:space:]]*$//')
        CLEAN_VALUE=$(echo "$value" | sed 's/^[[:space:]]*//;s/[[:space:]]*$//')
        git config "$CLEAN_KEY" "$CLEAN_VALUE"
    done < "../../.github/templates/gitconfig-feature"
fi

echo "✅ Feature branch created successfully!"
echo ""
echo "Branch: $BRANCH_NAME"
echo "Directory: $(pwd)"
echo ""
echo "📋 Next steps:"
echo "1. Make your changes"
echo "2. Test thoroughly"
echo "3. Commit with conventional commits: 'git commit -m \"feat(area): description #${ISSUE_NUMBER}\"'"
echo "4. Push: 'git push -u origin $BRANCH_NAME'"
echo "5. Create PR: 'gh pr create'"

# Open issue in browser if GitHub CLI is available
if command -v gh &> /dev/null; then
    echo ""
    echo "🔗 Opening GitHub issue..."
    gh issue view "$ISSUE_NUMBER" --web
fi