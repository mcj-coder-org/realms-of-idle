# Branch Protection Guide

## Overview

This document describes the branch protection setup for the idle-world project, which enforces quality standards and workflow requirements through GitHub's branch protection rules.

## Prerequisites

1. **GitHub Account**: Must have admin access to the repository
2. **Personal Access Token**: With repo admin permissions (stored in `.github.env`)
3. **Node.js**: v22.0.0 or higher (matches project requirements)

## Branch Protection Rules

### Protected Branches

The following branches are protected:

- `main` - Production branch
- `develop` - Integration branch

### Protection Rules for Main Branch

#### Required Status Checks (CI)

All status checks must pass before merging:

- ✅ **CI - Validate**: Code linting, formatting, and type checking
- ✅ **CI - Test**: Unit and E2E tests across all operating systems
- ✅ **CI - Build**: Application build verification

#### Pull Request Requirements

- **Minimum Reviews**: 1 approval required
- **Required Reviewers**: Only `mcj-coder` (maintainer) can approve
- **No Dismissals**: Reviews cannot be dismissed once approved
- **Conversation Resolution**: All discussions must be resolved before merge
- **Linear History**: Force push is disabled, no merge commits allowed

#### Restrictions

- **Admin Bypass**: Enabled (maintainers can bypass rules)
- **Force Pushes**: Disabled
- **Branch Deletion**: Allowed only via GitHub UI
- **Signature Verification**: Not required

### Protection Rules for Develop Branch

#### Required Status Checks

- Same as main branch (Validate, Test, Build)

#### Pull Request Requirements

- **Minimum Reviews**: 1 approval
- **Required Reviewers**: Same as main branch
- **Conversation Resolution**: Required
- **Linear History**: Enabled

## Setup Instructions

### 1. Create Personal Access Token

1. Go to GitHub → Settings → Developer settings → Personal access tokens
2. Click "Generate new token" → "Generate new token (classic)"
3. Select scopes:
   - `repo` (Full control of private repositories)
   - `admin:org` (Read and write org and team permissions)
4. Copy the token and store it in `.github.env`

### 2. Configure Environment

Copy the example configuration and fill in your values:

```bash
cp .github.env.example .github.env
```

Edit `.github.env` with your actual values:

```env
REPO_OWNER=your-username
REPO_NAME=idle-world
GITHUB_TOKEN=your_actual_token_here
```

### 3. Run Setup Script

```bash
# Install dependencies
pnpm install

# Setup branch protection
node scripts/setup-branch-protection.mjs setup

# Verify setup
node scripts/setup-branch-protection.mjs verify
```

## Manual Setup Alternative

If you prefer setting up through GitHub UI:

1. Go to repository → Settings → Branches → Branch protection rules
2. Click "Add rule"
3. Select "main" and "develop" branches
4. Configure as described in "Protection Rules"

## Script Commands

### Setup

```bash
node scripts/setup-branch-protection.mjs setup
```

Creates or updates branch protection rules.

### Verify

```bash
node scripts/setup-branch-protection.mjs verify
```

Checks that all protection rules are correctly configured.

### Cleanup

```bash
node scripts/setup-branch-protection.mjs cleanup
```

Removes all branch protection rules (use with caution).

## Troubleshooting

### Common Errors

#### Permission Denied

**Error**: `403 Forbidden`
**Solution**: Ensure your GitHub token has repo admin permissions and the correct scope.

#### Repository Not Found

**Error**: `404 Not Found`
**Solution**: Check that `REPO_OWNER` and `REPO_NAME` in `.github.env` are correct.

#### Branch Not Found

**Error**: `404 Not Found` for branch protection
**Solution**: Ensure the branch exists in the repository.

### Debugging Tips

1. **Check Token Permissions**: Verify the token has the required scopes
2. **Verify Repository Access**: Confirm you have admin access to the repository
3. **Check Branch Existence**: Ensure the protected branches exist
4. **Test with Dry Run**: Use the verify command to check current state

## Maintenance

### Updating Protection Rules

1. Edit the configuration in `scripts/setup-branch-protection.mjs`
2. Run the setup script again to update protection rules

### Adding New Branches

1. Add the branch name to `BRANCHES_TO_PROTECT` array
2. Run setup script to apply protection

### Regular Review

- Review protection rules quarterly
- Update required reviewers if team composition changes
- Check CI pipeline status to ensure all required checks are still valid

## Security Considerations

- **Token Storage**: Never commit `.github.env` to version control
- **Token Rotation**: Rotate your personal access token regularly
- **Permission Principle**: Grant minimal necessary permissions
- **Admin Bypass**: Keep enabled for maintainers but audit usage

## References

- [GitHub Branch Protection Documentation](https://docs.github.com/en/repositories/configuring-branches-and-merges-in-your-repository/managing-branch-protection-rules/about-branch-protection-rules)
- [GitHub API Documentation](https://docs.github.com/en/rest/branches/branch-protection)
- [Creating GitHub Personal Access Tokens](https://docs.github.com/en/authentication/keeping-your-account-and-data-secure/creating-a-personal-access-token)
