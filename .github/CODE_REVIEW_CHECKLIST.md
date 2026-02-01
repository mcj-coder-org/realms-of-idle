# Code Review Checklist

This checklist provides automated and manual code review guidelines for the Realms of Idle project.

## Automated Review Items

### ✅ Code Style & Formatting

- [ ] Follows conventional commit format
- [ ] Code follows project style guide (ESLint, Prettier, EditorConfig)
- [ ] No trailing whitespace
- [ ] Consistent indentation and spacing
- [ ] Proper line length (max 120 characters)

### ✅ Code Quality

- [ ] No unused variables or imports
- [ ] Appropriate access modifiers (public, private, internal)
- [ ] Meaningful variable and function names
- [ ] Proper error handling
- [ ] No dead code or commented-out code

### ✅ Security

- [ ] No hardcoded secrets, API keys, or URLs
- [ ] Proper input validation
- [ ] SQL injection prevention
- [ ] XSS protection (web-specific)
- [ ] Use of secure random number generation

### ✅ Performance

- [ ] No string concatenation in loops
- [ ] Proper use of data structures
- [ ] Avoid unnecessary object creation
- [ ] Asynchronous operations used appropriately

### ✅ Documentation

- [ ] Public APIs documented with XML comments (C#)
- [ ] Complex logic explained
- [ ] TODO/FIXME comments addressed or justified
- [ ] README updated for new features

### ✅ Testing

- [ ] Unit tests added/updated for new features
- [ ] Integration tests pass
- [ ] Test coverage maintained or improved
- [ ] Manual testing checklist completed

## Manual Review Areas

### 🎮 Game-Specific Code Review

#### Core Game Systems

- [ ] Game loop performance is optimal
- [ ] Object pooling used for frequently created/destroyed objects
- [ ] Proper use of Unity's serialization system
- [ ] Scene dependencies clearly defined

#### Script Architecture

- [ ] Script organization follows established patterns
- [ ] Single Responsibility Principle maintained
- [ ] Proper use of ScriptableObjects where appropriate
- [ ] MonoBehaviour lifecycle methods used correctly

#### UI/UX Implementation

- [ ] UI responsive across different screen sizes
- [ ] Proper event handling and state management
- [ ] Accessibility considerations (color contrast, text size)
- [ ] Smooth animations and transitions

#### Performance Optimization

- [ ] Draw calls minimized
- [ ] Batching used where appropriate
- [ ] Profiled on target devices
- [ ] Memory usage within acceptable limits

### 🔧 Backend Code Review

#### API Design

- [ ] RESTful API design principles followed
- [ ] Proper HTTP status codes used
- [ ] Response format consistent and documented
- [ ] Rate limiting implemented

#### Database Operations

- [ ] Proper database connection management
- [ ] Queries optimized (indexes, proper joins)
- [ ] Transaction handling appropriate
- [ ] Data validation at all layers

#### Security

- [ ] Authentication and authorization implemented
- [ ] Input sanitization
- [ ] HTTPS enforced
- [ ] CORS policies properly configured

### 📊 Cross-Cutting Concerns

#### Logging & Monitoring

- [ ] Appropriate logging levels used
- [ ] No sensitive data in logs
- [ ] Performance monitoring implemented
- [ ] Error tracking configured

#### Configuration Management

- [ ] Environment-specific configurations separated
- [ ] Secrets managed securely (environment variables, vault)
- [ ] Configuration validation implemented
- [ ] Documentation for configuration options

#### Build & Deployment

- [ ] Build process automated and reliable
- [ ] Dependencies properly pinned
- [ ] Build artifacts verified
- [ ] Deployment scripts tested

## Review Severity Levels

### 🚨 Critical (Must Fix)

- Security vulnerabilities
- Breaking changes
- Performance regression
- Build failures
- Test failures

### ⚠️ Major (Should Fix)

- Code style violations
- Missing documentation
- Potential bugs
- Code duplication
- Performance concerns

### 💡 Minor (Consider Improving)

- Minor style preferences
- Code readability suggestions
- Optimization opportunities
- Best practice recommendations

## Automated Review Reports

The system generates several automated reports:

1. **Code Review Report** (`/.github/review-report.md`)
   - Summary of all issues found
   - Grouped by category and severity
   - Action items

2. **Code Metrics** (`/.github/code-metrics.json`)
   - Cyclomatic complexity
   - Maintainability index
   - Technical debt indicators

3. **Test Coverage Report** (`/coverage-report/`)
   - Line coverage percentages
   - Branch coverage metrics
   - Coverage trends

## Review Process

### Automated Review (GitHub Actions)

- Runs on all pull requests
- Provides immediate feedback
- Blocks merge on critical issues
- Generates detailed comments

### Manual Review (Maintainer)

- Review automated reports
- Check business logic correctness
- Verify game play experience
- Assess design decisions
- Final approval for merge

### Quality Gates

**Pass Criteria:**

- 0 critical issues
- < 5 major issues (unless justified)
- All tests passing
- Code coverage maintained above 80%
- Build successful

**Fail Criteria:**

- Any critical issues
- Build or test failures
- Significant performance regression
- Breaking changes without proper deprecation

## Common Anti-Patterns to Avoid

### Code Quality

- Large methods (>50 lines)
- Deeply nested conditionals
- Magic numbers without constants
- Duplicated code
- Overly complex classes

### Performance

- Premature optimization
- Ignoring memory allocation patterns
- Inefficient algorithms
- Missing caching opportunities

### Architecture

- Circular dependencies
- God objects
- Over-engineering
- Violating SOLID principles

### Security

- Hardcoded credentials
- Insecure data serialization
- Missing input validation
- Improper error handling

## Review Tips

1. **Start with automated feedback** - Address CI issues first
2. **Focus on business logic** - Understand the feature being implemented
3. **Check edge cases** - What happens with invalid input?
4. **Consider maintainability** - Will other developers understand this code?
5. **Test the changes** - Run the game, verify behavior
6. **Update documentation** - README, wiki, inline docs
7. **Think about performance** - Profile on target devices
8. **Consider security** - No data leaks, proper validation
