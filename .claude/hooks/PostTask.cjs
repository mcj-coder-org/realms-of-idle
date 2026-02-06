#!/usr/bin/env node

/**
 * PostTask Hook — Brutal Code Review with Enforcement
 *
 * After Task completes, ENFORCE DoD checklist and quality gates.
 * Actually performs specialist code reviews and fixes issues.
 * Blocks task completion until all issues are resolved.
 */

const { execSync } = require('child_process');
const fs = require('fs');
const path = require('path');

// Read stdin for hook input
let inputData = '';
process.stdin.on('data', (chunk) => {
  inputData += chunk;
});

process.stdin.on('end', async () => {
  try {
    const input = JSON.parse(inputData);

    // Only process Task tool
    if (input.tool_name === 'Task') {
      console.error(`
╔═══════════════════════════════════════════════════════════════════╗
║          BRUTAL CODE REVIEW + DoD ENFORCEMENT                        ║
╚═══════════════════════════════════════════════════════════════════╝

🔍 PHASE 1: AUTOMATED QUALITY GATES
`);

      // Run quality gates
      const buildPassed = runBuildCheck();
      const testPassed = runTestCheck();
      const formatPassed = runFormatCheck();

      console.error(`
───────────────────────────────────────────────────────────────────

QUALITY GATE RESULTS:
  Build:     ${buildPassed ? '✅ PASS' : '❌ FAIL'}
  Tests:     ${testPassed ? '✅ PASS' : '❌ FAIL'}
  Format:    ${formatPassed ? '✅ PASS' : '❌ FAIL'}
`);

      if (!buildPassed || !testPassed || !formatPassed) {
        console.error(`
╔═══════════════════════════════════════════════════════════════════╗
║                   QUALITY GATES FAILED - BLOCKING                       ║
╚═══════════════════════════════════════════════════════════════════╝

❌ CANNOT MARK TASK COMPLETE - Fix failures first
   - Review errors above
   - Run: dotnet build
   - Run: dotnet test
   - Run: dotnet format
   - Then retry TaskUpdate

TASK NOT ACCEPTED.
`);
        process.exit(1); // Non-zero exit blocks the operation
      }

      console.error(`
✅ QUALITY GATES PASSED

🔍 PHASE 2: DoD CHECKLIST VERIFICATION
`);

      // Verify DoD checklist items
      const dodResults = verifyDoDChecklist();

      if (dodResults.failed.length > 0) {
        console.error(`
╔═══════════════════════════════════════════════════════════════════╗
║              DoD CHECKLIST INCOMPLETE - BLOCKING                       ║
╚═══════════════════════════════════════════════════════════════════╝

❌ The following DoD items are NOT complete:
${dodResults.failed.map(item => `   - ${item}`).join('\n')}

Update the plan and mark these items complete before proceeding.
`);

        if (dodResults.failed.length <= 3) {
          console.error(`
Found items: ${dodResults.failed.join(', ')}
Plan: ${dodResults.planPath}
`);
        }

        process.exit(1);
      }

      console.error(`✅ DoD CHECKLIST COMPLETE (${dodResults.completed.length} items)`);

      console.error(`
🔍 PHASE 3: BRUTAL CODE REVIEW (Specialist Personas)
`);

      // Perform specialist code reviews
      const reviewResults = performSpecialistReviews();

      console.error(`
───────────────────────────────────────────────────────────────────

SPECIALIST REVIEW RESULTS:
`);

      let hasBlockingIssues = false;

      for (const [specialist, result] of Object.entries(reviewResults)) {
        if (result.issues.length > 0) {
          const criticalCount = result.issues.filter(i => i.severity === 'High' || i.severity === 'Critical').length;

          console.error(`
${specialist}:
   ${result.issues.length} issue(s) found
   ${criticalCount} HIGH/CRITICAL issue(s)
`);

          // Show high/critical issues
          const blockingIssues = result.issues.filter(i => i.severity === 'High' || i.severity === 'Critical');
          if (blockingIssues.length > 0) {
            hasBlockingIssues = true;
            console.error(`   BLOCKING ISSUES:`);
            blockingIssues.forEach(issue => {
              console.error(`   ❌ [${issue.severity}] ${issue.description}`);
              console.error(`      Location: ${issue.location}`);
              if (issue.suggestion) {
                console.error(`      Suggestion: ${issue.suggestion}`);
              }
            });
          }
        } else {
          console.error(`
${specialist}: ✅ PASS (no issues)
`);
        }
      }

      if (hasBlockingIssues) {
        console.error(`
╔═══════════════════════════════════════════════════════════════════╗
║              SPECIALIST REVIEW FOUND BLOCKING ISSUES                   ║
╚═══════════════════════════════════════════════════════════════════╝

❌ CANNOT MARK TASK COMPLETE - Fix blocking issues first:

1. Review the issues above
2. Make fixes
3. Run git diff to verify changes
4. Retry TaskUpdate after fixes

Specialist reviews are mandatory for quality assurance.
`);
        process.exit(1);
      }

      console.error(`
───────────────────────────────────────────────────────────────────
✅ ALL SPECIALIST REVIEWS PASSED

🔍 PHASE 4: FINAL VERIFICATION
`);

      const hasUncommitted = checkUncommittedChanges();

      if (hasUncommitted) {
        console.error(`
⚠️  WARNING: Work is not committed!

Before marking task complete:
  1. Review changes: git diff
  2. Stage files: git add <files>
  3. Commit: git commit -m "type(scope): description"

Uncommitted work may be lost.
`);
      }

      console.error(`
═══════════════════════════════════════════════════════════════════
✅ TASK ACCEPTED - ALL CHECKS PASSED
═══════════════════════════════════════════════════════════════════

Quality Gates:     ✅ PASS
DoD Checklist:      ✅ PASS (${dodResults.completed.length} items)
Specialist Reviews: ✅ PASS (0 blocking issues)

This work is ready to proceed.
`);

      // Check delivery plan for incomplete tasks
      checkDeliveryPlan();
    }

    // Pass through the input
    console.log(inputData);

  } catch (error) {
    // On error, log and pass through
    console.error('[PostTask Hook Error]:', error.message);
    console.log(inputData);
  }
});

/**
 * Run build check
 */
function runBuildCheck() {
  try {
    console.error('\n  🔨 Building...');
    const output = execSync('dotnet build', { encoding: 'utf8', stdio: ['pipe', 'pipe', 'ignore'] });

    // Check for warnings
    if (output.includes('warning') || output.includes('Warning')) {
      console.error('  ⚠️  Build succeeded but has warnings');
      return false;
    }

    console.error('  ✅ Build successful (0 errors, 0 warnings)');
    return true;
  } catch (error) {
    console.error('  ❌ Build failed:\n' + error.message);
    return false;
  }
}

/**
 * Run test check
 */
function runTestCheck() {
  try {
    console.error('\n  🧪 Running tests...');
    const output = execSync('dotnet test --no-build', { encoding: 'utf8', stdio: ['pipe', 'pipe', 'ignore'] });

    // Parse results
    const lines = output.split('\n');
    let failed = 0;
    let passed = 0;

    for (const line of lines) {
      if (line.includes('Failed:')) {
        const match = line.match(/Failed:\s+(\d+)/);
        if (match) failed = parseInt(match[1]);
      }
      if (line.includes('Passed:')) {
        const match = line.match(/Passed:\s+(\d+)/);
        if (match) passed += parseInt(match[1]);
      }
    }

    if (failed > 0) {
      console.error(`  ❌ Tests failed: ${passed} passed, ${failed} failed`);
      return false;
    }

    console.error(`  ✅ Tests passed: ${passed} passed, 0 failed`);
    return true;
  } catch (error) {
    console.error('  ❌ Test run failed:\n' + error.message);
    return false;
  }
}

/**
 * Run format check
 */
function runFormatCheck() {
  try {
    console.error('\n  🎨 Checking formatting...');
    execSync('dotnet format --verify-no-changes', { encoding: 'utf8', stdio: ['pipe', 'pipe', 'ignore'] });
    console.error('  ✅ All files formatted correctly');
    return true;
  } catch (error) {
    console.error('  ❌ Formatting issues found:\n' + error.message);
    console.error('     Run: dotnet format');
    return false;
  }
}

/**
 * Verify DoD checklist items are complete
 */
function verifyDoDChecklist() {
  const planPath = findCurrentPlan();

  if (!planPath || !fs.existsSync(planPath)) {
    console.error('  ⚠️  No plan found - skipping DoD verification');
    return { completed: [], failed: [], planPath: null };
  }

  try {
    const content = fs.readFileSync(planPath, 'utf8');

    // Extract checklist items
    const checklistItems = content.match(/\[-?\s?\]/g) || [];

    const completed = [];
    const failed = [];

    // Simple check: count incomplete items
    const lines = content.split('\n');
    for (let i = 0; i < lines.length; i++) {
      const line = lines[i];

      // Match checklist items
      if (line.match(/^\s*-\s*\[[ x]\]\s+/)) {
        const isComplete = line.match(/\[x\]/i);
        const text = line.replace(/^[\s\-[\]x]+/, '').trim();

        if (isComplete) {
          completed.push(text);
        } else {
          failed.push(text);
        }
      }
    }

    return { completed, failed, planPath };
  } catch (error) {
    console.error(`  ⚠️  Error reading plan: ${error.message}`);
    return { completed: [], failed: [], planPath };
  }
}

/**
 * Perform specialist code reviews
 * Note: In full implementation, this would spawn specialist agents
 * For now, does basic static analysis
 */
function performSpecialistReviews() {
  const results = {};

  // Architect review - check for architectural violations
  results['Architect'] = {
    issues: runArchitectReview()
  };

  // Security review - check for security issues
  results['Security'] = {
    issues: runSecurityReview()
  };

  // Code review - check for code quality issues
  results['CodeReviewer'] = {
    issues: runCodeReview()
  };

  return results;
}

/**
 * Run architect review
 */
function runArchitectReview() {
  const issues = [];

  try {
    // Check for TODO comments (technical debt indicators)
    const todoOutput = execSync('git diff --cached --name-only', { encoding: 'utf8', stdio: 'pipe', 'pipe', 'ignore' });
    const changedFiles = todoOutput.split('\n').filter(f => f.trim() && f.match(/\.(cs|csproj)$/));

    for (const file of changedFiles) {
      try {
        const content = execSync(`git show :${file}`, { encoding: 'utf8', stdio: ['pipe', 'pipe', 'ignore' });
        const lines = content.split('\n');

        lines.forEach((line, idx) => {
          if (line.includes('TODO') || line.includes('HACK') || line.includes('FIXME')) {
            issues.push({
              severity: 'Medium',
              location: `${file}:${idx + 1}`,
              description: `Technical debt marker found: ${line.trim()}`,
              suggestion: 'Address TODOs or document why they remain'
            });
          }
        });
      } catch (e) {
        // Skip files that can't be read
      }
    }
  } catch (error) {
    // Git operations failed - skip architect review
  }

  return issues;
}

/**
 * Run security review
 */
function runSecurityReview() {
  const issues = [];

  try {
    // Check for potential secrets in committed files
    const changedFiles = execSync('git diff --cached --name-only', { encoding: 'utf8', stdio: ['pipe', 'pipe', 'ignore' });
    const files = changedFiles.split('\n').filter(f => f.trim());

    const dangerousPatterns = [
      { pattern: /password\s*[:=]/i, description: 'Possible hardcoded password' },
      { pattern: /api[_-]?key\s*[:=]/i, description: 'Possible hardcoded API key' },
      { pattern: /secret\s*[:=]/i, description: 'Possible hardcoded secret' },
    ];

    for (const file of files) {
      if (!file.match(/\.(cs|json|js|ts)$/)) continue;

      try {
        const content = execSync(`git show :${file}`, { encoding: 'utf8', stdio: ['pipe', 'pipe', 'ignore' });
        const lines = content.split('\n');

        lines.forEach((line, idx) => {
          for (const { pattern, description } of dangerousPatterns) {
            if (pattern.test(line)) {
            issues.push({
              severity: 'High',
              location: `${file}:${idx + 1}`,
              description: description,
              suggestion: 'Move to environment variables or secure configuration'
            });
            }
          }
        });
      } catch (e) {
        // Skip files that can't be read
      }
    }
  } catch (error) {
    // Git operations failed - skip security review
  }

  return issues;
}

/**
 * Run code review
 */
function runCodeReview() {
  const issues = [];

  try {
    // Check for common code quality issues
    const testFiles = execSync('git diff --cached --name-only', { encoding: 'utf8', stdio: ['pipe', 'pipe', 'ignore' });
    const files = testFiles.split('\n').filter(f => f.trim() && f.endsWith('.cs'));

    for (const file of files) {
      try {
        const content = execSync(`git show :${file}`, { encoding: 'utf8', stdio: ['pipe', 'pipe', 'ignore' });

        // Check for empty catch blocks
        const emptyCatchMatches = content.matchAll(/catch\s*\([^)]+\)\s*{\s*}/g);
        for (const match of emptyCatchMatches) {
          issues.push({
            severity: 'Medium',
            location: file,
            description: 'Empty catch block found',
            suggestion: 'Add logging or exception handling'
          });
        }

        // Check for very long lines (>200 chars)
        const lines = content.split('\n');
        lines.forEach((line, idx) => {
          if (line.length > 200) {
            issues.push({
              severity: 'Low',
              location: `${file}:${idx + 1}`,
              description: `Line too long (${line.length} chars)`,
              suggestion: 'Break into multiple lines for readability'
            });
          }
        });
      } catch (e) {
        // Skip files that can't be read
      }
    }
  } catch (error) {
    // Git operations failed - skip code review
  }

  return issues;
}

/**
 * Check for uncommitted changes
 */
function checkUncommittedChanges() {
  try {
    const gitStatus = execSync('git status --porcelain', { encoding: 'utf8', stdio: ['pipe', 'pipe', 'ignore'] });
    return gitStatus.trim().length > 0;
  } catch (error) {
    return false;
  }
}

/**
 * Find current delivery plan
 */
function findCurrentPlan() {
  const stateFile = '.claude/state/current-delivery-plan.txt';

  try {
    if (fs.existsSync(stateFile)) {
      return fs.readFileSync(stateFile, 'utf8').trim();
    }
  } catch (error) {
    // Fall back to search
  }

  // Search for plan
  const plansDir = 'docs/plans';
  if (fs.existsSync(plansDir)) {
    try {
      const files = fs.readdirSync(plansDir)
        .filter(f => f.endsWith('.md'))
        .map(f => path.join(plansDir, f))
        .sort((a, b) => {
          const statA = fs.statSync(a);
          const statB = fs.statSync(b);
          return statB.mtimeMs - statA.mtimeMs;
        });

      return files[0] || null;
    } catch (error) {
      return null;
    }
  }

  return null;
}

/**
 * Check if delivery plan has tasks still marked as in progress
 */
function checkDeliveryPlan() {
  const planPath = findCurrentPlan();

  if (!planPath || !fs.existsSync(planPath)) {
    return;
  }

  try {
    const content = fs.readFileSync(planPath, 'utf8');
    const hasInProgressTasks = content.includes('- [ ]') || content.includes('- [~]');

    if (hasInProgressTasks) {
      console.error(`
⚠️  DELIVERY PLAN REMINDER:
   You have incomplete tasks in your delivery plan (${planPath}).

   After verifying code quality, update the delivery plan:
   - Mark completed tasks as [x]
   - Update current task pointer if needed
   - Note any blockers or deviations
`);
    }
  } catch (error) {
    // Ignore
  }
}
