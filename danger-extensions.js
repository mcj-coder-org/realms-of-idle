// Enhanced DangerJS extensions for comprehensive automated code review
const { danger, fail, warn, message, markdown } = require('danger')
const fs = require('fs')
const path = require('path')

// Load project configuration
const config = require('./danger.config.js').project

// Get PR information
const pr = danger.github.pr
const modifiedFiles = danger.git.modifiedFiles
const createdFiles = danger.git.createdFiles
const deletedFiles = danger.git.deletedFiles
const baseBranch = danger.github.pr.base.ref
const headBranch = danger.github.pr.head.ref

// Helper functions
const hasChangesIn = (paths) => {
  const allFiles = [...modifiedFiles, ...createdFiles]
  return allFiles.some(file => paths.some(path => file.startsWith(path)))
}

const hasFileType = (extension) => {
  const allFiles = [...modifiedFiles, ...createdFiles]
  return allFiles.some(file => file.endsWith(extension))
}

const getFileContent = (filePath) => {
  try {
    return fs.readFileSync(filePath, 'utf8')
  } catch (e) {
    return ''
  }
}

const countLines = (content) => {
  return content.split('\n').length
}

const countAddedLines = (filePath) => {
  const diff = danger.github.diffForFile(filePath)
  if (diff && diff.added) {
    return diff.added
  }
  return 0
}

const countDeletedLines = (filePath) => {
  const diff = danger.github.diffForFile(filePath)
  if (diff && diff.removed) {
    return diff.removed
  }
  return 0
}

// Code Quality Analysis
const analyzeCodeQuality = () => {
  const issues = []

  modifiedFiles.forEach(file => {
    if (file.endsWith('.cs') || file.endsWith('.js') || file.endsWith('.ts')) {
      const content = getFileContent(file)
      const lines = content.split('\n')

      // Check for long methods/functions
      lines.forEach((line, index) => {
        if (line.trim().match(/(?:public|private|protected|internal|function|def|class|struct)/)) {
          const methodStart = index
          let methodEnd = index + 1
          let braceCount = 0

          if (line.includes('{')) braceCount++

          for (let i = index + 1; i < lines.length && braceCount >= 0; i++) {
            if (lines[i].includes('{')) braceCount++
            if (lines[i].includes('}')) braceCount--
            methodEnd = i
          }

          const methodLines = methodEnd - methodStart + 1
          if (methodLines > 50) {
            issues.push({
              file,
              line: methodStart + 1,
              message: `Long method detected (${methodLines} lines). Consider breaking it down.`,
              type: 'warning'
            })
          }
        }
      })

      // Check for complex conditionals
      const complexConditionalPattern = /if\s*\([^)]+&&[^)]+\)|if\s*\([^)]+\|\|[^)]+\)/
      lines.forEach((line, index) => {
        if (complexConditionalPattern.test(line)) {
          issues.push({
            file,
            line: index + 1,
            message: 'Complex conditional detected. Consider simplifying or extracting to a method.',
            type: 'warning'
          })
        }
      })

      // Check for magic numbers
      const magicNumberPattern = /\b\d{4,}\b/
      lines.forEach((line, index) => {
        if (magicNumberPattern.test(line) && !line.includes('const') && !line.includes('static')) {
          issues.push({
            file,
            line: index + 1,
            message: 'Magic number detected. Consider using a named constant.',
            type: 'warning'
          })
        }
      })
    }
  })

  return issues
}

// C# Specific Analysis
const analyzeCSharpCode = () => {
  const issues = []

  modifiedFiles.forEach(file => {
    if (file.endsWith('.cs')) {
      const content = getFileContent(file)

      // Check for using statements organization
      const usingPattern = /^using\s+.*$/gm
      const usingMatches = content.match(usingPattern)
      if (usingMatches && usingMatches.length > 0) {
        const lastSystemUsing = usingMatches.findIndex(u => u.startsWith('using System.'))
        const firstThirdPartyUsing = usingMatches.findIndex(u => !u.startsWith('using System.'))

        if (lastSystemUsing !== -1 && firstThirdPartyUsing !== -1 && lastSystemUsing > firstThirdPartyUsing) {
          issues.push({
            file,
            line: 1,
            message: 'Using statements should be organized: System namespaces first, then third-party.',
            type: 'warning'
          })
        }
      }

      // Check for async/await usage
      const asyncPattern = /async\s+.*$/gm
      const awaitPattern = /await\s+/gm
      const asyncMatches = content.match(asyncPattern)
      const awaitMatches = content.match(awaitPattern)

      if (asyncMatches && asyncMatches.length > 0 && !awaitMatches) {
        issues.push({
          file,
            line: 1,
            message: 'Async method found without await usage. Ensure proper async/await pattern.',
            type: 'warning'
          })
        }
      }

      // Check for null checks
      const nullCheckPattern = /if\s*\([^)]+==\s*null|\bif\s*\([^)]+!=\s*null/;
      if (nullCheckPattern.test(content)) {
        issues.push({
          file,
          line: 1,
          message: 'Use null conditional operator (?.) or pattern matching for null checks.',
          type: 'warning'
        })
      }
    }
  })

  return issues
}

// JavaScript/TypeScript Analysis
const analyzeJavaScriptCode = () => {
  const issues = []

  modifiedFiles.forEach(file => {
    if (file.endsWith('.js') || file.endsWith('.ts')) {
      const content = getFileContent(file)

      // Check for var usage
      if (content.includes('var ')) {
        issues.push({
          file,
          line: content.indexOf('var ') + 1,
          message: 'Use const or let instead of var.',
          type: 'warning'
        })
      }

      // Check for console.log in production code
      if (baseBranch === 'main' && content.includes('console.log')) {
        issues.push({
          file,
          line: content.split('\n').findIndex(line => line.includes('console.log')) + 1,
          message: 'Console.log found in production code. Remove or replace with logger.',
          type: 'error'
        })
      }

      // Check for deprecated patterns
      const deprecatedPatterns = [
        { pattern: /new\s+Promise\s*\(/, message: 'Use async/await instead of Promise constructor.' },
        { pattern: /\.map\s*\(\s*function\s*\(/, message: 'Use arrow function in map().' }
      ]

      deprecatedPatterns.forEach(({ pattern, message }) => {
        if (pattern.test(content)) {
          issues.push({
            file,
            line: 1,
            message,
            type: 'warning'
          })
        }
      })
    }
  })

  return issues
}

// Documentation Analysis
const analyzeDocumentation = () => {
  const issues = []

  modifiedFiles.forEach(file => {
    if (file.endsWith('.cs') || file.endsWith('.js') || file.endsWith('.ts')) {
      const content = getFileContent(file)

      // Check for public API documentation
      const publicApiPattern = /(public|protected)\s+(static\s+)?\w+\s+\w+\s*\(/g
      const publicApiMatches = content.match(publicApiPattern)

      if (publicApiMatches) {
        publicApiMatches.forEach(match => {
          const lineNum = content.substring(0, content.indexOf(match)).split('\n').length
          const beforeLine = content.split('\n')[lineNum - 2]

          if (!beforeLine || !beforeLine.trim().startsWith('///')) {
            issues.push({
              file,
              line: lineNum - 1,
              message: 'Public API method should have XML documentation.',
              type: 'warning'
            })
          }
        })
      }
    }
  })

  return issues
}

// Performance Analysis
const analyzePerformance = () => {
  const issues = []

  modifiedFiles.forEach(file => {
    if (file.endsWith('.cs') || file.endsWith('.js') || file.endsWith('.ts')) {
      const content = getFileContent(file)

      // Check for string concatenation in loops
      const stringConcatPattern = /\+\s*["\']/g
      const lines = content.split('\n')
      let inLoop = false

      lines.forEach((line, index) => {
        if (line.includes('for') || line.includes('while') || line.includes('.forEach')) {
          inLoop = true
        } else if (line.trim() === '') {
          // Empty line, might be end of block
        } else if (inLoop && stringConcatPattern.test(line)) {
          issues.push({
            file,
            line: index + 1,
            message: 'String concatenation in loop detected. Consider using StringBuilder or template literals.',
            type: 'warning'
          })
          inLoop = false
        }
      })
    }
  })

  return issues
}

// Security Analysis
const analyzeSecurity = () => {
  const issues = []

  modifiedFiles.forEach(file => {
    if (file.endsWith('.cs') || file.endsWith('.js') || file.endsWith('.ts') || file.endsWith('.json')) {
      const content = getFileContent(file)

      // Check for hardcoded URLs
      const urlPattern = /https?:\/\/[^\s"']+/g
      const urls = content.match(urlPattern)

      if (urls) {
        urls.forEach(url => {
          const lineNum = content.substring(0, content.indexOf(url)).split('\n').length
          issues.push({
            file,
            line: lineNum,
            message: `Hardcoded URL detected: ${url}. Consider using configuration.`,
            type: 'warning'
          })
        })
      }

      // Check for insecure random
      if (file.endsWith('.cs')) {
        const insecureRandomPattern = /new\s+Random\(\)/g
        if (insecureRandomPattern.test(content)) {
          issues.push({
            file,
            line: 1,
            message: 'Use System.Security.Cryptography.RandomNumberGenerator instead of Random.',
            type: 'error'
          })
        }
      }
    }
  })

  return issues
}

// Main review logic
const runEnhancedReview = () => {
  const qualityIssues = analyzeCodeQuality()
  const csharpIssues = analyzeCSharpCode()
  const jsIssues = analyzeJavaScriptCode()
  const docIssues = analyzeDocumentation()
  const perfIssues = analyzePerformance()
  const securityIssues = analyzeSecurity()

  const allIssues = [...qualityIssues, ...csharpIssues, ...jsIssues, ...docIssues, ...perfIssues, ...securityIssues]

  // Group issues by type
  const issuesByFile = allIssues.reduce((acc, issue) => {
    if (!acc[issue.file]) acc[issue.file] = []
    acc[issue.file].push(issue)
    return acc
  }, {})

  // Generate detailed comments
  Object.entries(issuesByFile).forEach(([file, issues]) => {
    const issuesByType = issues.reduce((acc, issue) => {
      if (!acc[issue.type]) acc[issue.type] = []
      acc[issue.type].push(issue)
      return acc
    }, {})

    let comment = `### 🔍 Code Quality Review for \`${file}\`\n\n`

    if (issuesByType.error) {
      comment += `❌ **Errors**\n`
      issuesByType.error.forEach(issue => {
        comment += `- Line ${issue.line}: ${issue.message}\n`
      })
      comment += '\n'
    }

    if (issuesByType.warning) {
      comment += `⚠️ **Warnings**\n`
      issuesByType.warning.forEach(issue => {
        comment += `- Line ${issue.line}: ${issue.message}\n`
      })
      comment += '\n'
    }

    if (Object.keys(issuesByType).length > 0) {
      comment += `📊 **Summary**: ${issues.length} issues found in ${file}\n`
    }

    markdown(comment)
  })

  // Overall summary
  const totalIssues = allIssues.length
  const errorCount = allIssues.filter(i => i.type === 'error').length
  const warningCount = allIssues.filter(i => i.type === 'warning').length

  message(`📋 **Enhanced Code Review Complete**\n\n- Total issues found: ${totalIssues}\n- Errors: ${errorCount}\n- Warnings: ${warningCount}`)

  if (errorCount > 0) {
    fail(`❌ Found ${errorCount} critical issues that must be addressed before merging.`)
  }

  if (totalIssues > 10) {
    warn(`⚠️ Found ${totalIssues} total issues. Consider addressing the most critical ones.`)
  }
}

// Execute enhanced review
runEnhancedReview()

// Additional checks
const checkPRQuality = () => {
  const prTitle = pr.title || ''
  const prBody = pr.body || ''

  // Check PR description quality
  if (prBody.length < 50) {
    warn('PR description is quite short. Consider adding more details about the changes.')
  }

  // Check for performance impact
  if (hasChangesIn(['Assets/Scripts/'])) {
    message('🎮 Game scripts modified. Consider testing performance impact on target devices.')
  }

  // Check for breaking changes
  if (hasChangesIn(['Assets/Plugins/', '*.csproj'])) {
    warn('Breaking changes detected in plugin/project files. Ensure backward compatibility.')
  }
}

checkPRQuality()