# DangerJS Configuration

This project uses [DangerJS](https://danger.systems/js/) to automate pull request checks and maintain code quality.

## Overview

DangerJS runs automatically on every pull request to:

- Validate PR titles and descriptions
- Check for conventional commits
- Ensure proper branching practices
- Detect potential security issues
- Enforce documentation requirements
- Validate code quality standards

## Configuration

### Files

- `danger.js` - Main DangerJS rules and checks
- `danger.config.js` - Project-specific configuration
- `.github/workflows/danger.yml` - GitHub Actions workflow
- `scripts/danger-local.mjs` - Local testing script

### Key Checks

#### PR Validation

- **Conventional Commits**: Validates PR title format (`feat: description`, `fix: description`, etc.)
- **Issue References**: Warns if no issue reference is included
- **Branch Naming**: Ensures branches follow `feat/description` or `fix/description` pattern

#### Code Quality

- **Large Changes**: Warns on PRs with >500 lines added
- **Test Coverage**: Warns if source changes don't include tests
- **Documentation**: Reminds about doc updates when needed

#### Security

- **Secret Detection**: Scans for potential API keys, tokens, and secrets
- **Merge Conflicts**: Detects and reports merge conflicts

## Usage

### Local Testing

Run DangerJS locally to test checks before opening a PR:

```bash
npm run danger:local
```

### CI Integration

DangerJS runs automatically in CI on:

- PR opened
- PR synchronized
- PR reopened

### Manual Runs

Force run DangerJS on a specific PR:

```bash
npx danger ci --dangerfile=danger.js --pr=<PR_NUMBER>
```

## Custom Rules

The main DangerJS checks are defined in `danger.js`. The configuration file `danger.config.js` contains project-specific settings:

```javascript
module.exports = {
  project: {
    conventionalCommits: {
      enabled: true,
      pattern: /^(feat|fix|docs|style|refactor|test|chore)(\([^)]+\))?: .+/,
    },
    prRules: {
      maxFilesChanged: 50,
      maxLinesAdded: 1000,
      requireTestsForSourceChanges: true,
    },
  },
};
```

## Integrations

### GitHub

- Uses GitHub API for PR information
- Posts comments directly on PRs
- Generates detailed reports

### Other Tools

- **Commitlint**: Ensures conventional commits
- **Markdownlint**: Validates documentation
- **Prettier**: Enforces code formatting
- **dotnet format**: Ensures C# code quality

## Troubleshooting

### Common Issues

1. **PR not running**: Check workflow permissions and ensure branch is in `main` or `develop`
2. **False positives**: Adjust rules in `danger.js` or add ignore patterns
3. **Missing secrets**: Ensure `DANGER_GITHUB_API_TOKEN` is properly configured

### Debugging

1. Run locally with `npm run danger:local`
2. Check GitHub Actions logs for detailed error messages
3. Enable verbose logging with `DANGER_VERBOSE=1`

## Extending

### Adding New Checks

1. Add rules to `danger.js`
2. Test locally with `npm run danger:local`
3. Commit changes and test in CI

### Custom Plugins

Create custom DangerJS plugins in `danger.plugins.js`:

```javascript
// danger.plugins.js
export default {
  myCustomPlugin: (danger, utils) => {
    // Custom logic here
  },
};
```

## Best Practices

1. Keep checks focused and relevant
2. Use warnings for suggestions, errors for blockers
3. Provide clear, actionable feedback
4. Test rules before committing
5. Balance automation with manual review
