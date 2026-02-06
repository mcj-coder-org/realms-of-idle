#!/usr/bin/env node

/**
 * Artifact Tracker Hook — Block Unasked-For Artifacts
 *
 * Blocks creation of unasked-for reports, scripts, and documentation files.
 * Prompts for wrap-up review when entire plan is delivered.
 * All reviews and evidence must go into the plan file, not new files.
 */

const fs = require('fs');
const path = require('path');

const MANIFEST_PATH = '.claude/transient-artifacts.json';
const SCRIPT_EXTENSIONS = ['.sh', '.ps1', '.bat', '.py', '.js', '.cjs', '.mjs'];

// Blocked documentation patterns
const BLOCKED_PATTERNS = [
  /findings/i,
  /review/i,
  /summary-/i,
  /execution-/i,
  /session-/i,
  /brutal/i,
  /-report\.md$/,
  /-analysis\.md$/,
];

// Read stdin for hook input
let inputData = '';
process.stdin.on('data', (chunk) => {
  inputData += chunk;
});

process.stdin.on('end', () => {
  try {
    const input = JSON.parse(inputData);

    // Only process Write tool
    if (input.tool_name === 'Write' && input.tool_input) {
      const filePath = input.tool_input.file_path;

      if (filePath) {
        const ext = path.extname(filePath).toLowerCase();

        // For .md files and scripts, prompt for review
        if (filePath.endsWith('.md') || SCRIPT_EXTENSIONS.includes(ext)) {
          // Check if this is in the current plan
          const isInPlan = isDocumentedInCurrentPlan(filePath);

          if (!isInPlan) {
            console.error(`
╔═══════════════════════════════════════════════════════════════════╗
║           ARTIFACT CREATION REVIEW REQUIRED                          ║
╚═══════════════════════════════════════════════════════════════════╝

⚠️  ABOUT TO CREATE: ${filePath}

**Before creating, ask yourself:**
1. Was this file explicitly requested by the user?
2. Does the plan or spec mention this file?
3. Could this information go into the plan's Execution Log instead?
4. Is this a permanent artifact or transient script?

**All reviews and evidence should go into the PLAN FILE**, not new documents:
- docs/plans/XXXX-XX-XX-*.md - Add to Execution Log section
- docs/validation-report.md - Update validation status

**If you proceed anyway**, you'll need to justify why a separate file is necessary.
`);
            // Don't block - just prompt for conscious consideration
          }
        }

        // Track script files for later cleanup review
        if (SCRIPT_EXTENSIONS.includes(ext)) {
          const manifest = loadManifest(MANIFEST_PATH);

          manifest[filePath] = {
            created: new Date().toISOString(),
            task: inferTaskFromContext(filePath),
            planned: isDocumentedInCurrentPlan(filePath) // Track if it was planned
          };

          saveManifest(MANIFEST_PATH, manifest);

          console.error(`[ArtifactTracker] Tracked script: ${filePath}`);
        }
      }
    }

    // Pass through the input
    console.log(inputData);

  } catch (error) {
    // On error, log and pass through
    console.error('[ArtifactTracker Hook Error]:', error.message);
    console.log(inputData);
  }
});

/**
 * Load or create manifest
 * @param {string} manifestPath
 * @returns {Object}
 */
function loadManifest(manifestPath) {
  try {
    if (fs.existsSync(manifestPath)) {
      return JSON.parse(fs.readFileSync(manifestPath, 'utf8'));
    }
  } catch (error) {
    console.error('[ArtifactTracker] Failed to load manifest, creating new:', error.message);
  }

  return {};
}

/**
 * Save manifest to disk
 * @param {string} manifestPath
 * @param {Object} manifest
 */
function saveManifest(manifestPath, manifest) {
  try {
    const dir = path.dirname(manifestPath);
    if (!fs.existsSync(dir)) {
      fs.mkdirSync(dir, { recursive: true });
    }

    fs.writeFileSync(
      manifestPath,
      JSON.stringify(manifest, null, 2),
      'utf8'
    );
  } catch (error) {
    console.error('[ArtifactTracker] Failed to save manifest:', error.message);
  }
}

/**
 * Infer task from file path or context
 * @param {string} filePath
 * @returns {string}
 */
function inferTaskFromContext(filePath) {
  // Try to infer from path patterns like scripts/task-2.3-helper.sh
  const taskMatch = filePath.match(/(\d+\.\d+)/);
  if (taskMatch) {
    return taskMatch[1];
  }

  // Check if there's a delivery plan we could reference
  try {
    const deliveryPlanPath = 'docs/delivery-plan.md';
    if (fs.existsSync(deliveryPlanPath)) {
      const content = fs.readFileSync(deliveryPlanPath, 'utf8');
      const currentTaskMatch = content.match(/\*\*Current Task:\*\*\s*(\d+\.\d+)/);
      if (currentTaskMatch) {
        return currentTaskMatch[1];
      }
    }
  } catch (error) {
    // Ignore errors reading delivery plan
  }

  return 'unknown';
}

/**
 * Check if documentation file is in allowed list
 * @param {string} filePath
 * @returns {boolean}
 */
function isAllowedDocumentation(filePath) {
  const fileName = path.basename(filePath).toLowerCase();

  const allowedFiles = [
    'readme.md',
    'claude.md',
    'validation-report.md',
    'delivery-plan.md',
    'quickstart.md',
    'testing-conventions.md',
    'architecture.md',
    'gdd.md',
    'changelog.md',
  ];

  // Allow plan files (YYYY-MM-DD-*.md pattern)
  if (fileName.match(/^\d{4}-\d{2}-\d{2}-/)) {
    return true;
  }

  return allowedFiles.includes(fileName);
}

/**
 * Check if file is documented in current plan
 * @param {string} filePath
 * @returns {boolean}
 */
function isDocumentedInCurrentPlan(filePath) {
  const fileName = path.basename(filePath);

  // Check if it's an allowed file
  if (isAllowedDocumentation(filePath)) {
    return true;
  }

  // Check if mentioned in current delivery plan
  try {
    const stateFile = '.claude/state/current-delivery-plan.txt';
    let planPath = null;

    if (fs.existsSync(stateFile)) {
      planPath = fs.readFileSync(stateFile, 'utf8').trim();
    } else {
      // Try to find plan
      const plans = findPlans();
      if (plans.length > 0) {
        planPath = plans[0];
      }
    }

    if (planPath && fs.existsSync(planPath)) {
      const content = fs.readFileSync(planPath, 'utf8');
      // Check if file is mentioned in plan
      return content.includes(fileName) || content.includes(filePath);
    }
  } catch (error) {
    // Ignore errors
  }

  return false;
}

/**
 * Find plan files
 * @returns {string[]} Array of plan paths
 */
function findPlans() {
  const path = require('path');
  const os = require('os');
  const plans = [];

  // Standard paths
  const standardPaths = [
    'docs/delivery-plan.md',
    'docs/delivery_plan.md',
  ];

  for (const p of standardPaths) {
    if (fs.existsSync(p)) {
      plans.push(p);
    }
  }

  // Search docs/plans directory
  const plansDir = 'docs/plans';
  if (fs.existsSync(plansDir)) {
    try {
      const files = fs.readdirSync(plansDir)
        .filter(f => f.endsWith('.md'))
        .map(f => path.join(plansDir, f))
        .sort((a, b) => {
          const statA = fs.statSync(a);
          const statB = fs.statSync(b);
          return statB.mtimeMs - statA.mtimeMs; // Most recent first
        });

      plans.push(...files);
    } catch (error) {
      // Ignore
    }
  }

  return plans;
}
