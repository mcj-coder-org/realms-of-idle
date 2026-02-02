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

// Check 2: DoD checklist is 100% passing WITH inline evidence links
// Find the DoD heading and extract everything until the next ## heading
const dodHeadingMatch = prBody.match(/#{2,3} Definition of Done/)
if (!dodHeadingMatch) {
  fail('❌ PR description is missing "### Definition of Done" section. Please copy the PR template and fill out the DoD checklist.')
  return
}

const dodHeadingIndex = prBody.indexOf(dodHeadingMatch[0])
const nextHeadingMatch = prBody.substring(dodHeadingIndex + dodHeadingMatch[0].length).match(/\n##[^#]/)
const dodSection = nextHeadingMatch
  ? prBody.substring(dodHeadingIndex, dodHeadingIndex + dodHeadingMatch[0].length + nextHeadingMatch.index)
  : prBody.substring(dodHeadingIndex)

// Parse DoD subsections
const acceptCriteriaSection = dodSection.match(/### Acceptance Criteria[\s\S]*?(?=###[^#]|##|$)/)
const codeQualitySection = dodSection.match(/### Code Quality[\s\S]*?(?=###[^#]|##|$)/)
const documentationSection = dodSection.match(/### Documentation[\s\S]*?(?=###[^#]|##|$)/)
const testingSection = dodSection.match(/### Testing[\s\S]*?(?=###[^#]|##|$)/)
const securitySection = dodSection.match(/### Security & Review[\s\S]*?(?=###[^#]|##|$)/)

// Validate Acceptance Criteria section exists
if (!acceptCriteriaSection) {
  fail('❌ DoD is missing "### Acceptance Criteria" subsection. Copy Acceptance Criteria from the linked Issue.')
}

// Helper: Validate that checked items have inline evidence links
// Format: - [x] Text - [Label](URL) - notes
function validateCheckedItemsWithEvidence(sectionText, sectionName) {
  if (!sectionText) return { checked: 0, withoutEvidence: 0 }

  const lines = sectionText.split('\n')
  const checkedItems = []
  const itemsWithoutEvidence = []

  lines.forEach((line, index) => {
    // Match checked checkboxes: - [x] or - [X]
    const checkedMatch = line.match(/^\s*-\s*\[([xX])\]\s+(.+)/)
    if (checkedMatch) {
      const itemText = checkedMatch[2]
      checkedItems.push(itemText)

      // Check if line contains an inline markdown link: [Text](URL)
      const hasEvidenceLink = /\[.*?\]\(https?:\/\/[^\s]+\)/.test(itemText)

      if (!hasEvidenceLink) {
        itemsWithoutEvidence.push({ line: index + 1, text: itemText })
      }
    }
  })

  return { checked: checkedItems.length, withoutEvidence: itemsWithoutEvidence }
}

// Validate each subsection
if (acceptCriteriaSection) {
  const result = validateCheckedItemsWithEvidence(acceptCriteriaSection[0], 'Acceptance Criteria')
  if (result.withoutEvidence.length > 0) {
    fail(
      `❌ Acceptance Criteria: ${result.withoutEvidence.length} checked item(s) missing inline evidence link. Format: - [x] AC description - [Evidence](URL) - notes`
    )
  }
}

if (codeQualitySection) {
  const result = validateCheckedItemsWithEvidence(codeQualitySection[0], 'Code Quality')
  if (result.withoutEvidence.length > 0) {
    fail(
      `❌ Code Quality: ${result.withoutEvidence.length} checked item(s) missing inline evidence link. Format: - [x] Item - [Evidence](URL) - notes`
    )
  }
}

if (documentationSection) {
  const result = validateCheckedItemsWithEvidence(documentationSection[0], 'Documentation')
  if (result.withoutEvidence.length > 0) {
    fail(
      `❌ Documentation: ${result.withoutEvidence.length} checked item(s) missing inline evidence link. Format: - [x] Item - [Evidence](URL) - notes`
    )
  }
}

if (testingSection) {
  const result = validateCheckedItemsWithEvidence(testingSection[0], 'Testing')
  if (result.withoutEvidence.length > 0) {
    fail(
      `❌ Testing: ${result.withoutEvidence.length} checked item(s) missing inline evidence link. Format: - [x] Item - [Evidence](URL) - notes`
    )
  }
}

if (securitySection) {
  const result = validateCheckedItemsWithEvidence(securitySection[0], 'Security & Review')
  if (result.withoutEvidence.length > 0) {
    fail(
      `❌ Security & Review: ${result.withoutEvidence.length} checked item(s) missing inline evidence link. Format: - [x] Item - [Evidence](URL) - notes`
    )
  }
}

// Check 2b: Verify linked issue has Acceptance Criteria
schedule(async () => {
  try {
    if (!danger.github || !danger.github.api) {
      warn('⚠️ GitHub API not available. Unable to verify linked issue has Acceptance Criteria.')
      return
    }

    const octokit = danger.github.api
    const owner = danger.github.repo?.owner
    const repo = danger.github.repo?.repo

    if (!owner || !repo) {
      warn('⚠️ Unable to determine repository owner/name. Skipping issue AC check.')
      return
    }

    // Extract issue number from PR body (e.g., "Closes: #42")
    const issueMatch = prBody.match(/(?:Closes|Fixes|Resolves):\s*#(\d+)/i)
    if (!issueMatch) {
      warn('⚠️ No linked issue found (format: "Closes: #42"). Please link the issue this PR addresses.')
      return
    }

    const issueNumber = issueMatch[1]

    // Fetch issue data
    const issueData = await octokit.rest.issues.get({
      owner,
      repo,
      issue_number: issueNumber
    })

    const issueBody = issueData.data.body || ''

    // Check if issue has Acceptance Criteria section
    const hasAC = /###\s*Acceptance\s+Criteria/i.test(issueBody) ||
                  /##\s*Acceptance\s+Criteria/i.test(issueBody)

    if (!hasAC) {
      fail(
        `❌ Linked issue #${issueNumber} does not contain an "Acceptance Criteria" section. The issue must define AC before this PR can be merged. Please update the issue.`
      )
    } else {
      message(`✅ Issue #${issueNumber} has Acceptance Criteria defined`)
    }
  } catch (error) {
    if (error.status === 404) {
      fail('❌ Linked issue not found. Please ensure the issue number is correct.')
    } else {
      warn(`⚠️ Unable to verify linked issue has Acceptance Criteria: ${error.message}`)
    }
  }
})

// Check 3: Brutal Critical Review shows APPROVED
// Find the review heading and extract everything until the next ## heading
const reviewHeadingMatch = prBody.match(/## 🔬 Brutal Critical Review/)
if (!reviewHeadingMatch) {
  fail(
    '❌ Brutal Critical Review missing from PR description. Maintainer must perform and document review before approval. See .claude/skills/pr-monitor.md for template.'
  )
} else {
  const reviewHeadingIndex = prBody.indexOf(reviewHeadingMatch[0])
  const nextHeadingMatch = prBody.substring(reviewHeadingIndex + reviewHeadingMatch[0].length).match(/\n##[^#]/)
  const reviewSection = nextHeadingMatch
    ? prBody.substring(reviewHeadingIndex, reviewHeadingIndex + reviewHeadingMatch[0].length + nextHeadingMatch.index)
    : prBody.substring(reviewHeadingIndex)

  const sectionText = reviewSection
  const isApproved = sectionText.includes('Overall Assessment: APPROVED') || sectionText.includes('**Overall Assessment**: APPROVED')
  const needsWork = sectionText.includes('Overall Assessment: NEEDS WORK') || sectionText.includes('**Overall Assessment**: NEEDS WORK')
  const rejected = sectionText.includes('Overall Assessment: REJECTED') || sectionText.includes('**Overall Assessment**: REJECTED')

  if (needsWork || rejected) {
    fail(`❌ Brutal Critical Review not approved. Maintainer assessment: ${needsWork ? 'NEEDS WORK' : 'REJECTED'}`)
  }

  if (!isApproved) {
    warn('⚠️ Brutal Critical Review assessment unclear. Should explicitly state "APPROVED", "NEEDS WORK", or "REJECTED".')
  }

  // Verify Brutal Critical Review has evidence
  const reviewLinks = sectionText.match(/\[.*?\]\(https:\/\/github\.com\/mcj-coder-org\/realms-of-idle\/.*?\)/g) || []
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
}

// Load danger-extensions.js for additional code quality checks
try {
  require('./danger-extensions.js')
} catch (error) {
  warn(`⚠️ Failed to load danger-extensions.js: ${error.message}`)
}

