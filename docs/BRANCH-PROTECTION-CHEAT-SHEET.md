# Branch Protection Cheat Sheet

## Quick Setup

### 1. Get GitHub Token

- Go to GitHub → Settings → Developer settings → Personal access tokens
- Generate token with `repo` and `admin:org` scopes
- Store in `.github.env`:

```env
REPO_OWNER=your-username
REPO_NAME=idle-world
GITHUB_TOKEN=your-token
```

### 2. Run Setup

```bash
# Node.js version (recommended)
npm run branch:setup

# Shell version (alternative)
npm run branch:setup:shell
```

### 3. Verify

```bash
npm run branch:verify
```

## Protected Branches

- `main` - Production with CI/CD and maintainer review
- `develop` - Integration with CI/CD

## Rules Summary

| Branch  | CI Checks                       | PR Reviews     | Admin Bypass |
| ------- | ------------------------------- | -------------- | ------------ |
| main    | ✓ Validate<br>✓ Test<br>✓ Build | 1 (maintainer) | ✓            |
| develop | ✓ Validate<br>✓ Test<br>✓ Build | 1 (maintainer) | ✓            |

## Common Commands

```bash
# Setup protection
npm run branch:setup

# Check current status
npm run branch:verify

# Remove protection (use carefully!)
npm run branch:cleanup
```

## Troubleshooting

### "Permission Denied"

- Check token has `repo` and `admin:org` scopes
- Verify user has admin access to repository

### "Repository Not Found"

- Check `REPO_OWNER` and `REPO_NAME` in `.github.env`

### "Branch Not Found"

- Ensure branch exists in repository
- Check branch name spelling

## File Locations

- **Script**: `scripts/setup-branch-protection.mjs`
- **Shell Script**: `scripts/setup-branch-protection.sh`
- **Config**: `.github.env`
- **Docs**: `docs/BRANCH-PROTECTION.md`
- **Scripts**: `package.json` branch:\* commands
