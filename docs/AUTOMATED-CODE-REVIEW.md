# Automated Code Review System

This document describes the automated code review system implemented for the Realms of Idle project.

## Overview

The automated code review system provides comprehensive code quality checks, security scanning, and performance analysis for all pull requests. It integrates seamlessly with GitHub Actions and provides detailed feedback directly in PR comments.

## Components

### 1. GitHub Actions Workflow (`.github/workflows/automated-code-review.yml`)

- **Automated Review**: Runs DangerJS with custom extensions
- **Security Scan**: Trivy vulnerability scanner
- **Code Quality Metrics**: Complexity and maintainability analysis
- **Test Coverage**: Coverage tracking and reporting

### 2. DangerJS Configuration

#### `danger.js` - Core PR checks

- Conventional commit validation
- Branch naming conventions
- File change validation
- Security checks for hardcoded secrets
- TODO/FIXME comment tracking

#### `danger-extensions.js` - Enhanced code analysis

- Code complexity analysis
- C# specific checks (using statements, async/await)
- JavaScript/TypeScript best practices
- Documentation requirements
- Performance optimizations
- Security vulnerability detection

### 3. Review Scripts

#### `scripts/generate-review-report.mjs`

- Generates comprehensive PR review reports
- Tracks code quality metrics
- Provides actionable recommendations
- Creates artifacts for download

#### `scripts/calculate-code-metrics.mjs`

- Analyzes code complexity
- Calculates maintainability index
- Tracks technical debt
- Generates HTML reports

### 4. Configuration Files

#### `review.config.js`

- Customizable review rules
- Severity levels and thresholds
- File-specific configurations
- Project-specific rules for Unity/C#

#### `danger.config.js`

- DangerJS specific settings
- Project patterns
- Rule definitions

## Features

### Automated Checks

#### Code Quality

- **Complexity Analysis**: Cyclomatic complexity calculation
- **Maintainability Index**: Code maintainability scoring
- **Method Length**: Detection of long methods
- **Code Duplication**: Potential duplicate code detection
- **Naming Conventions**: Variable and function name analysis

#### Security

- **Secret Detection**: Hardcoded credentials, API keys
- **Input Validation**: Missing validation checks
- **SQL Injection**: Vulnerable query patterns
- **XSS Protection**: Cross-site scripting prevention

#### Performance

- **Memory Usage**: Unnecessary allocations
- **Algorithm Efficiency**: Inefficient patterns
- **String Concatenation**: In string operations
- **Caching Opportunities**: Missing optimization opportunities

#### Unity-Specific

- **Scene Dependencies**: Proper scene setup
- **ScriptableObjects**: Correct usage patterns
- **MonoBehaviour**: Lifecycle method usage
- **UI Performance**: Draw call optimization

### Quality Gates

#### Critical Issues (Block Merge)

- Security vulnerabilities
- Build failures
- Test failures
- Breaking changes

#### Major Issues (Warning)

- High complexity methods
- Missing documentation
- Potential bugs
- Performance concerns

#### Minor Issues (Info)

- Style preferences
- Best practice suggestions
- Optimization opportunities

## Usage

### Local Development

```bash
# Run full local review
npm run review:full

# Generate only metrics
npm run review:metrics

# Run DangerJS locally
npm run review:danger
```

### GitHub PR Flow

1. **PR Creation**: Automated review starts immediately
2. **Build & Test**: System builds and tests the changes
3. **Code Analysis**: Complexity and quality analysis
4. **Security Scan**: Vulnerability detection
5. **Review Comments**: Detailed comments on code issues
6. **Quality Metrics**: Reports uploaded as artifacts
7. **Coverage Report**: Test coverage displayed in comments

### Review Comments Format

The system generates structured comments:

```markdown
### 🔍 Code Quality Review for `Assets/Scripts/Player.cs`

❌ **Errors**

- Line 45: Security vulnerability detected - hardcoded API key
- Line 78: High complexity detected (15) - consider refactoring

⚠️ **Warnings**

- Line 12: Method too long (60 lines) - break into smaller methods
- Line 89: Missing XML documentation for public method

📊 **Summary**: 5 issues found in Assets/Scripts/Player.cs
```

## Customization

### Adding Custom Rules

Edit `review.config.js` to add new rules:

```javascript
customRules: [
  {
    name: 'CustomRule',
    enabled: true,
    severity: 'major',
    description: 'Check for specific pattern',
    check: (file, content) => {
      return /your-pattern/.test(content);
    },
  },
];
```

### Adjusting Thresholds

Modify thresholds in `review.config.js`:

```javascript
thresholds: {
  maxFilesChanged: 50,
  maxLinesAdded: 1000,
  minTestCoverage: 80,
  maxComplexityScore: 10
}
```

### Ignoring Files

Add patterns to the ignore array:

```javascript
ignore: ['node_modules/**/*', 'Assets/ThirdParty/**/*', 'temp/**/*'];
```

## Integration with Existing Tools

### Git Hooks

- Pre-commit linting
- Pre-push formatting
- Commit message validation

### CI/CD Pipeline

- Automated on PR creation/update
- Integration with existing workflows
- Artifact generation and storage

### GitHub Checks

- Status updates
- Annotations for specific lines
- Detailed logs

## Reporting

### Available Reports

1. **Markdown Report** (`.github/review-report.md`)
   - Human-readable summary
   - Detailed issue breakdown
   - Action recommendations

2. **JSON Metrics** (`.github/code-metrics.json`)
   - Machine-readable data
   - Quality metrics
   - Historical data

3. **HTML Report** (`.github/code-metrics.html`)
   - Interactive visualization
   - Color-coded indicators
   - Drill-down details

4. **Danger Comments**
   - Inline PR comments
   - GitHub-native format
   - Actionable feedback

### Metrics Tracked

- Code complexity (cyclomatic)
- Maintainability index
- Lines of code
- Test coverage
- Security issues
- Performance concerns
- Technical debt estimate

## Best Practices

### For Contributors

1. Run local review before pushing
2. Address all critical issues
3. Maintain test coverage
4. Follow conventional commits
5. Update documentation

### For Maintainers

1. Review automated reports
2. Check business logic correctness
3. Verify performance impact
4. Assess design decisions
5. Ensure quality gates are met

### For CI/CD

1. Keep thresholds appropriate
2. Update regularly for new patterns
3. Balance automation vs manual review
4. Monitor false positives

## Troubleshooting

### Common Issues

1. **False Positives**
   - Adjust ignore patterns
   - Customize rule severity
   - Disable specific rules

2. **Performance**
   - Reduce file scope
   - Cache results
   - Optimize patterns

3. **Missing Files**
   - Check gitignore patterns
   - Verify file extensions
   - Update config paths

### Debug Mode

Enable debug mode for troubleshooting:

```javascript
development: {
  debugMode: true,
  verboseLogging: true
}
```

## Future Enhancements

### Planned Features

1. **Machine Learning** - Improved code quality prediction
2. **Cross-File Analysis** - Detect dependencies between files
3. **Performance Profiling** - Real-world performance metrics
4. **Code Search Integration** - Find similar code patterns
5. **Automated Refactoring Suggestions** - Tool-based improvements

### Integration Opportunities

1. **IDE Plugins** - Real-time feedback during development
2. **Chat Integration** - Slack/Teams notifications
3. **Dashboard** - Centralized quality metrics
4. **Trend Analysis** - Long-term quality tracking
5. **Team Guidelines** - Custom rule sets for different teams

## Conclusion

The automated code review system significantly improves code quality while reducing manual review effort. It provides immediate feedback, maintains consistency, and helps catch issues early in the development process.

For questions or contributions, please refer to the project documentation or create an issue in the GitHub repository.
