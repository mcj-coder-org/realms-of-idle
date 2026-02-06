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
        const fileName = path.basename(filePath).toLowerCase();

        // BLOCK: Unasked-for documentation files
        if (filePath.endsWith('.md') && !isAllowedDocumentation(filePath)) {
          // Check if it matches blocked patterns
          for (const pattern of BLOCKED_PATTERNS) {
            if (pattern.test(fileName)) {
              console.error(`
╔═══════════════════════════════════════════════════════════════════╗
║           BLOCKED: Unasked-For Documentation/Report                   ║
╚═══════════════════════════════════════════════════════════════════╝

❌ CANNOT CREATE: ${filePath}

This file matches a blocked pattern: ${pattern}

**All reviews and evidence must go into the PLAN FILE**, not separate documents.

Update the plan's Execution Log section instead:
- docs/plans/XXXX-XX-XX-*.md - Add findings to Execution Log
- docs/validation-report.md - Update validation status

**Blocked files create documentation sprawl and drift from the plan.**

If you need to create this file, ask the user for explicit permission first.
`);
              process.exit(1); // BLOCK the Write operation
            }
          }

          // Check if it's a docs/ file not in allowed list
          const dirName = path.dirname(filePath).toLowerCase();
          if (dirName.startsWith('docs/') && !isAllowedDocumentation(filePath)) {
            console.error(`
╔═══════════════════════════════════════════════════════════════════╗
║           BLOCKED: Unapproved Documentation File                   ║
╚═══════════════════════════════════════════════════════════════════╝

❌ CANNOT CREATE: ${filePath}

**Approved docs/ files:**
- docs/plans/*.md - Implementation plans (add Execution Log entries here)
- docs/validation-report.md - Official validation report
- docs/quickstart.md - Getting started guide
- docs/testing-conventions.md - Test standards
- docs/architecture.md - System architecture
- docs/gdd.md - Game design document

Please use the plan's Execution Log section instead of creating new files.
`);
            process.exit(1);
          }
        }

        // Track script files for later cleanup review
        if (SCRIPT_EXTENSIONS.includes(ext)) {
          const manifest = loadManifest(MANIFEST_PATH);

          manifest[filePath] = {
            created: new Date().toISOString(),
            task: inferTaskFromContext(filePath),
            planned: false // Will be updated during wrap-up review
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
