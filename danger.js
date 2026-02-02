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
  const diff = await danger.git.diffForFile('package.json')
  if (diff) {
    const conflictMarkerPattern = /<<<<<<<|=======|>>>>>>>/
    if (conflictMarkerPattern.test(diff.added) || conflictMarkerPattern.test(diff.diff)) {
      fail(`PR #${prNumber}: Merge conflict markers detected in package.json. Please resolve before merging.`)
    }
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