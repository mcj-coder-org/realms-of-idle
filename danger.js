// DangerJS configuration for cozy-fantasy-rpg
// This file defines custom checks and rules for pull requests

const { danger, fail, warn, message } = require('danger')

// Get information about the PR
const pr = danger.github.pr
const modifiedFiles = danger.git.modifiedFiles || []
const createdFiles = danger.git.createdFiles || []
const deletedFiles = danger.git.deletedFiles || []

// Helper functions
const hasChangesIn = (paths) => {
  const allFiles = [...modifiedFiles, ...createdFiles]
  return allFiles.some(file => paths.some(path => file.startsWith(path)))
}

const hasFileType = (extension) => {
  const allFiles = [...modifiedFiles, ...createdFiles]
  return allFiles.some(file => file.endsWith(extension))
}

// PR Title Validation
const prTitle = pr.title || ''
const prNumber = pr.number
const branchName = pr.head.ref

// Check for conventional commit format
const conventionalCommitPattern = /^(feat|fix|docs|style|refactor|test|chore)(\([^)]+\))?: .+/
if (!conventionalCommitPattern.test(prTitle)) {
  warn(`PR #${prNumber}: Title should follow conventional commit format: <type>(<scope>): <description>`)
}

// Check for issue reference in PR title or description
const hasIssueReference = prTitle.includes('#') || (pr.body && pr.body.includes('#'))
if (!hasIssueReference) {
  warn(`PR #${prNumber}: Consider including an issue reference (e.g., #42) in the title or description`)
}

// File Change Validation
// Check for large changes that might need review
const totalFilesChanged = modifiedFiles.length + createdFiles.length + deletedFiles.length
if (totalFilesChanged > 20) {
  warn(`PR #${prNumber}: Large change detected (${totalFilesChanged} files changed). Consider breaking into smaller PRs if possible.`)
}

// Check for test files when modifying source code
const hasSourceChanges = hasChangesIn(['src/', 'lib/', 'core/', 'shared/'])
const hasTestChanges = hasChangesIn(['test/', 'tests/', '__tests__/'])
const hasTestFiles = hasFileType('.test.') || hasFileType('.spec.')

if (hasSourceChanges && !hasTestChanges && !hasTestFiles) {
  warn(`PR #${prNumber}: Source code changes detected without corresponding test changes. Consider adding tests.`)
}

// Documentation Check
if (hasChangesIn(['docs/']) && !hasChangesIn(['README.md'])) {
  message(`PR #${prNumber}: Documentation changes detected. Consider updating README.md if needed.`)
}

// Security and Best Practices
// Check for secrets in code
const dangerousPatterns = [
  /password\s*=\s*['"]/i,
  /secret\s*=\s*['"]/i,
  /api[_-]?key\s*=\s*['"]/i,
  /token\s*=\s*['"]/i,
  /private[_-]?key\s*=\s*['"]/i
]

modifiedFiles.forEach(file => {
  if (file.endsWith('.cs') || file.endsWith('.js') || file.endsWith('.ts') || file.endsWith('.json')) {
    const content = danger.utils.fileContents(file)
    dangerousPatterns.forEach(pattern => {
      if (pattern.test(content)) {
        fail(`PR #${prNumber}: Potential secret found in ${file}. Please review and remove any sensitive information.`)
      }
    })
  }
})

// Check for TODO comments that should be addressed
const todoPattern = /TODO|FIXME|HACK/i
modifiedFiles.forEach(file => {
  if (file.endsWith('.cs') || file.endsWith('.js') || file.endsWith('.ts')) {
    const content = danger.utils.fileContents(file)
    const lines = content.split('\n')
    const todoLines = lines.map((line, index) => ({ line, number: index + 1 }))
      .filter(({ line }) => todoPattern.test(line))

    if (todoLines.length > 0) {
      message(`PR #${prNumber}: TODO/FIXME comments found in ${file}:`)
      todoLines.forEach(({ line, number }) => {
        message(`  Line ${number}: ${line.trim()}`)
      })
    }
  }
})

// Branch Naming Check
const validBranchPattern = /^(feat|fix|docs|style|refactor|test|chore)\/.+/
if (!validBranchPattern.test(branchName)) {
  warn(`PR #${prNumber}: Branch name "${branchName}" doesn't follow recommended format. Use type/description (e.g., feat/add-inventory-system)`)
}

// Check for merge conflicts
schedule(async () => {
  try {
    const diff = await danger.git.diffForFile('package.json')
    if (diff && diff.added) {
      const conflictMarkerPattern = /<<<<<<<|=======|>>>>>>>/
      if (conflictMarkerPattern.test(diff.added) || (diff.diff && conflictMarkerPattern.test(diff.diff))) {
        fail(`PR #${prNumber}: Merge conflict markers detected in package.json. Please resolve before merging.`)
      }
    }
  } catch (error) {
    // File not found in diff or other error - skip check
    warn(`PR #${prNumber}: Unable to check package.json for merge conflicts: ${error.message}`)
  }
})

// Specific checks for Unity/C# project
if (hasChangesIn(['Assets/'])) {
  // Check for .csproj changes
  if (hasChangesIn(['*.csproj'])) {
    message(`PR #${prNumber}: Unity project files modified. Ensure project builds correctly.`)
  }

  // Check for script compilation
  if (hasChangesIn(['Assets/**/*.cs'])) {
    message(`PR #${prNumber}: C# scripts modified. Consider testing compilation and gameplay.`)
  }
}

// Summary messages
message(`PR #${prNumber}: Review complete. Changes made in ${modifiedFiles.length} files, ${createdFiles.length} files created, ${deletedFiles.length} files deleted.`)

if (hasSourceChanges) {
  message(`PR #${prNumber}: Source code changes detected in ${modifiedFiles.filter(f => f.match(/\.(cs|js|ts|json)$/)).join(', ')}`)
}

if (hasChangesIn(['docs/'])) {
  message(`PR #${prNumber}: Documentation changes detected`)
}

// ============================================================================
// BRUTAL CRITICAL REVIEW & DoD VALIDATION
// ============================================================================

const prBody = pr.body || ''

// Check 1: Brutal Critical Review section exists
const hasBrutalReview = prBody.includes('## 🔬 Brutal Critical Review')
if (!hasBrutalReview) {
  fail(
    '❌ Brutal Critical Review missing from PR description. Maintainer must perform and document review before approval. See .claude/skills/pr-monitor.md for template.'
  )
}

// Check 2: DoD checklist is 100% passing WITH evidence
const dodSection = prBody.match(/### Definition of Done[\s\S]*?(?=##|$)/)
if (dodSection) {
  const uncheckedItems = (dodSection[0].match(/\[ \]/g) || []).length
  const failingItems = (dodSection[0].match(/\[❌\]/g) || []).length
  const checkedItems = (dodSection[0].match(/\[✅\]/g) || []).length

  if (uncheckedItems > 0 || failingItems > 0) {
    fail(
      `❌ DoD Checklist incomplete: ${uncheckedItems} unchecked, ${failingItems} failing. All items must be ✅ before merge.`
    )
  }

  // CRITICAL: Verify that checked items have evidence links
  if (checkedItems > 0) {
    const evidenceLinks = prBody.match(/\[.*?\]\(https:\/\/github\.com\/mcj-coder-org\/realms-of-idle\/.*?\)/g) || []

    // Count evidence links in key sections
    const ciEvidenceSection = prBody.match(/## CI Status[\s\S]*?(?=##|$)/)
    const ciLinks = ciEvidenceSection
      ? (ciEvidenceSection[0].match(/\[.*?\]\(https:\/\/github\.com\/mcj-coder-org\/realms-of-idle\/.*?\)/g) || [])
          .length
      : 0

    // Rough heuristic: Should have at least as many evidence links as checked items
    if (evidenceLinks.length < checkedItems) {
      fail(
        `❌ ${checkedItems} DoD items checked ✅ but only ${evidenceLinks.length} evidence links found. Every checked item must have supporting evidence.`
      )
    }

    if (ciLinks === 0 && checkedItems > 0) {
      fail(
        '❌ DoD checklist shows passing items but no CI evidence links found. Add CI status table with evidence links.'
      )
    }
  }
}

// Check 3: Brutal Critical Review shows APPROVED
const reviewSection = prBody.match(/## 🔬 Brutal Critical Review[\s\S]*?(?=##|$)/)
if (reviewSection) {
  const isApproved = reviewSection[0].includes('**Overall Assessment**: APPROVED')
  const needsWork = reviewSection[0].includes('**Overall Assessment**: NEEDS WORK')
  const rejected = reviewSection[0].includes('**Overall Assessment**: REJECTED')

  if (needsWork || rejected) {
    fail(`❌ Brutal Critical Review not approved. Maintainer assessment: ${needsWork ? 'NEEDS WORK' : 'REJECTED'}`)
  }

  if (!isApproved) {
    warn('⚠️ Brutal Critical Review assessment unclear. Should explicitly state "APPROVED", "NEEDS WORK", or "REJECTED".')
  }

  // Verify Brutal Critical Review has evidence
  const reviewLinks = reviewSection[0].match(/\[.*?\]\(https:\/\/github\.com\/mcj-coder-org\/realms-of-idle\/.*?\)/g) || []
  if (isApproved && reviewLinks.length === 0) {
    fail('❌ Brutal Critical Review marked APPROVED but contains no evidence links. All assessments must reference evidence.')
  }
}

// Check 4: All review threads must be resolved
schedule(async () => {
  try {
    // Verify we have the required GitHub API access
    if (!danger.github || !danger.github.api) {
      warn('⚠️ GitHub API not available. Unable to verify review thread status.')
      return
    }

    const octokit = danger.github.api
    const owner = danger.github.repo?.owner
    const repo = danger.github.repo?.repo

    if (!owner || !repo) {
      warn('⚠️ Unable to determine repository owner/name. Skipping review thread check.')
      return
    }

    const query = `query {
      repository(owner: "${owner}", name: "${repo}") {
        pullRequest(number: ${prNumber}) {
          reviewThreads(first: 100) {
            totalCount
            nodes {
              isResolved
              isOutdated
            }
          }
        }
      }
    }`

    const result = await octokit.graphql(query)

    // Validate response structure
    if (!result || !result.repository || !result.repository.pullRequest) {
      warn('⚠️ Unexpected GraphQL response structure. Skipping review thread check.')
      return
    }

    const threads = result.repository.pullRequest.reviewThreads
    if (!threads || !threads.nodes) {
      warn('⚠️ No review threads data in response. Skipping review thread check.')
      return
    }

    const unresolvedCount = threads.nodes.filter(thread => !thread.isResolved && !thread.isOutdated).length

    if (unresolvedCount > 0) {
      fail(
        `❌ ${unresolvedCount} unresolved review thread(s) found. All review comments must be resolved before merge.`
      )
    } else {
      message(`✅ All review threads resolved (${threads.totalCount} total threads)`)
    }
  } catch (error) {
    if (error.message && error.message.includes('Resource not accessible')) {
      fail(
        '❌ GitHub API token lacks permissions to read review threads. Please ensure the token has "pull-requests:read" scope.'
      )
    } else {
      warn(`⚠️ Unable to verify review thread status via GraphQL: ${error.message}. Please manually verify all review comments are resolved in GitHub UI.`)
    }
  }
})

// Check 5: Overall evidence link validation
const evidenceLinks = prBody.match(/\[.*?\]\(https:\/\/github\.com\/mcj-coder-org\/realms-of-idle\/.*?\)/g) || []
if (evidenceLinks.length === 0) {
  fail('❌ No evidence links found in PR description. All claims must have supporting evidence.')
} else {
  message(`✅ Found ${evidenceLinks.length} evidence link(s) in PR description`)
}
