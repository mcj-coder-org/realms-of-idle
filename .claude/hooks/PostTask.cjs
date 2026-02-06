#!/usr/bin/env node

/**
 * PostTask Hook — Brutal Code Review with Quality Gate Enforcement
 *
 * After Task completes, ENFORCE quality gates before accepting.
 * Runs actual checks, not just reminders.
 * Triggers specialist personas for code review (architect, security, critic).
 */

const { execSync } = require('child_process');
const fs = require('fs');

// Read stdin for hook input
let inputData = '';
process.stdin.on('data', (chunk) => {
  inputData += chunk;
});

process.stdin.on('end', () => {
  try {
    const input = JSON.parse(inputData);

    // Only process Task tool
    if (input.tool_name === 'Task') {
      console.error(`
╔═══════════════════════════════════════════════════════════════════╗
║              BRUTAL CODE REVIEW + QUALITY GATE ENFORCEMENT              ║
╚═══════════════════════════════════════════════════════════════════╝

🔍 RUNNING AUTOMATED QUALITY GATE CHECKS...
`);

      // Run quality gates
      const buildPassed = runBuildCheck();
      const testPassed = runTestCheck();
      const formatPassed = runFormatCheck();
      const hasUncommitted = checkUncommittedChanges();

      console.error(`
───────────────────────────────────────────────────────────────────

QUALITY GATE RESULTS:
  Build:     ${buildPassed ? '✅ PASS' : '❌ FAIL'}
  Tests:     ${testPassed ? '✅ PASS' : '❌ FAIL'}
  Format:    ${formatPassed ? '✅ PASS' : '❌ FAIL'}
  Committed:  ${hasUncommitted ? '⚠️  PENDING' : '✅ YES'}
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
───────────────────────────────────────────────────────────────────
✅ QUALITY GATES PASSED

Now complete the BRUTAL SELF REVIEW:

🔍 MINIMAL CHANGES CHECK:
   [ ] Does this change ONLY what was requested?
   [ ] Are there "helpful" additions not in the spec?
   [ ] Can any lines be removed while still satisfying requirements?

🔍 BEST PRACTICES CHECK:
   [ ] Does code follow project patterns?
   [ ] Are naming conventions consistent?
   [ ] Is error handling appropriate?

Verify with:
  - git diff <commit> to see exact changes
  - grepai_trace to verify no unexpected callers affected

If ANY check fails: Request fixes before marking complete.
`);

      // Check delivery plan
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
    console.error('\n🔨 Building...');
    const output = execSync('dotnet build', { encoding: 'utf8', stdio: ['pipe', 'pipe', 'ignore'] });

    // Check for warnings
    if (output.includes('warning') || output.includes('Warning')) {
      console.error('⚠️  Build succeeded but has warnings');
      return false;
    }

    console.error('✅ Build successful (0 errors, 0 warnings)');
    return true;
  } catch (error) {
    console.error('❌ Build failed:\n' + error.message);
    return false;
  }
}

/**
 * Run test check
 */
function runTestCheck() {
  try {
    console.error('\n🧪 Running tests...');
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
      console.error(`❌ Tests failed: ${passed} passed, ${failed} failed`);
      return false;
    }

    console.error(`✅ Tests passed: ${passed} passed, 0 failed`);
    return true;
  } catch (error) {
    console.error('❌ Test run failed:\n' + error.message);
    return false;
  }
}

/**
 * Run format check
 */
function runFormatCheck() {
  try {
    console.error('\n🎨 Checking formatting...');
    execSync('dotnet format --verify-no-changes', { encoding: 'utf8', stdio: ['pipe', 'pipe', 'ignore'] });
    console.error('✅ All files formatted correctly');
    return true;
  } catch (error) {
    console.error('❌ Formatting issues found:\n' + error.message);
    console.error('   Run: dotnet format');
    return false;
  }
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
 * Check if delivery plan has tasks still marked as in progress
 */
function checkDeliveryPlan() {
  const path = require('path');

  // Read the delivery plan path stored by PreToolUse hook
  const stateFile = path.join('.claude/state', 'current-delivery-plan.txt');

  try {
    if (!fs.existsSync(stateFile)) {
      // No stored path - this means PreToolUse wasn't triggered or failed
      // Fall back to searching
      searchForDeliveryPlan();
      return;
    }

    const planPath = fs.readFileSync(stateFile, 'utf8').trim();
    checkPlanFile(planPath);

  } catch (error) {
    // Can't read state file, fall back to search
    searchForDeliveryPlan();
  }
}

/**
 * Fallback: Search for delivery plan if no path was stored
 */
function searchForDeliveryPlan() {
  const os = require('os');
  const path = require('path');

  // Standard delivery plan files
  const standardPaths = [
    'docs/delivery-plan.md',
    'docs/delivery_plan.md',
    'DELIVERY_PLAN.md',
    'delivery-plan.md',
    'delivery_plan.md'
  ];

  // Directories to search for delivery plans (feature-named or random)
  const searchDirectories = [
    'docs/plans',           // Superpowers
    path.join(os.homedir(), '.claude/plans'),  // Claude Code default
    '.omc',                 // oh-my-claude
    '.omc/plans'
  ];

  // Check standard paths first
  for (const planPath of standardPaths) {
    if (checkPlanFile(planPath)) {
      return; // Found and checked, done
    }
  }

  // Search directories for plan-like .md files
  for (const dir of searchDirectories) {
    try {
      if (fs.existsSync(dir)) {
        const files = fs.readdirSync(dir)
          .filter(f => f.endsWith('.md'))
          .map(f => ({
            name: f,
            path: path.join(dir, f),
            mtime: fs.statSync(path.join(dir, f)).mtime
          }))
          .sort((a, b) => b.mtime - a.mtime); // Most recent first

        for (const file of files) {
          if (checkPlanFile(file.path)) {
            return; // Found and checked, done
          }
        }
      }
    } catch (error) {
      // Can't read directory, continue to next
    }
  }
}

/**
 * Check a single plan file for in-progress tasks
 * @param {string} planPath - Path to the delivery plan
 * @returns {boolean} - True if file exists and was checked
 */
function checkPlanFile(planPath) {
  try {
    if (!fs.existsSync(planPath)) {
      return false;
    }

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

    return true;
  } catch (error) {
    return false;
  }
}
