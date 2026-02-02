#!/usr/bin/env node

// Automated code review report generator
import fs from 'fs'
import path from 'path'
import { fileURLToPath } from 'url'
import { execSync } from 'child_process'

const __filename = fileURLToPath(import.meta.url)
const __dirname = path.dirname(__filename)

// Configuration
const config = {
  maxFilesChanged: 50,
  maxLinesAdded: 1000,
  maxComplexity: 10,
  minCoverage: 80,
  outputDir: '.github',
  outputFile: 'review-report.md',
  metricsFile: 'code-metrics.json'
}

// Helper functions
function getGitDiffStats() {
  try {
    const { execSync } = require('child_process')
    const diffOutput = execSync('git diff --stat', { encoding: 'utf8' })
    const lines = diffOutput.split('\n').filter(line => line.trim())

    const stats = {
      filesChanged: 0,
      linesAdded: 0,
      linesDeleted: 0,
      insertions: 0,
      deletions: 0
    }

    lines.forEach(line => {
      if (line.includes('file')) {
        stats.filesChanged++
      } else {
        const parts = line.split(/\s+/)
        if (parts[1] === 'insertion') {
          stats.insertions += parseInt(parts[0])
          stats.linesAdded += parseInt(parts[0])
        } else if (parts[1] === 'deletion') {
          stats.deletions += parseInt(parts[0])
          stats.linesDeleted += parseInt(parts[0])
        }
      }
    })

    return stats
  } catch (error) {
    return {
      filesChanged: 0,
      linesAdded: 0,
      linesDeleted: 0,
      insertions: 0,
      deletions: 0
    }
  }
}

async function analyzeComplexity() {
  const issues = []

  try {
    const modifiedFiles = execSync('git diff --name-only HEAD~1', { encoding: 'utf8' })
      .split('\n')
      .filter(file => file.trim())

    modifiedFiles.forEach(file => {
      if (file.endsWith('.cs') || file.endsWith('.js') || file.endsWith('.ts')) {
        try {
          const content = fs.readFileSync(file, 'utf8')
          const lines = content.split('\n')

          // Analyze method complexity
          lines.forEach((line, index) => {
            if (line.includes('function') || line.includes('def ') || line.includes('public ') || line.includes('private ')) {
              let complexity = 1

              // Count branches
              const conditionalCount = (line.match(/if|else|switch|case|for|while|&&|\|\||\?/g) || []).length
              complexity += conditionalCount

              if (complexity > config.maxComplexity) {
                issues.push({
                  file,
                  line: index + 1,
                  complexity,
                  message: `High complexity detected (${complexity}). Consider refactoring.`
                })
              }
            }
          })

          // Check for long files
          if (lines.length > 500) {
            issues.push({
              file,
              line: 1,
              complexity: lines.length,
              message: `File is quite large (${lines.length} lines). Consider breaking it down.`
            })
          }
        } catch (error) {
          // Skip files that can't be read
        }
      }
    })
  } catch (error) {
    // Git command failed, skip complexity analysis
  }

  return issues
}

function checkDependencies() {
  const issues = []

  if (fs.existsSync('package.json')) {
    const packageJson = JSON.parse(fs.readFileSync('package.json', 'utf8'))
    const dependencies = { ...packageJson.dependencies, ...packageJson.devDependencies }

    // Check for outdated dependencies (simplified check)
    Object.entries(dependencies).forEach(([name, version]) => {
      if (version.startsWith('^') || version.startsWith('~')) {
        issues.push({
          type: 'dependency',
          name,
          version,
          message: `Dependency ${name} uses loose versioning. Consider pinning to exact version.`
        })
      }
    })
  }

  return issues
}

function checkSecurity() {
  const issues = []

  // Check for potential secrets
  const dangerousPatterns = [
    /password\s*=\s*['"]/i,
    /secret\s*=\s*['"]/i,
    /api[_-]?key\s*=\s*['"]/i,
    /token\s*=\s*['"]/i,
    /private[_-]?key\s*=\s*['"]/i,
    /mongodb:\/\/[^:]+:[^@]+@/,
    /mysql:\/\/[^:]+:[^@]+@/
  ]

  try {
    const { execSync } = require('child_process')
    const modifiedFiles = execSync('git diff --name-only HEAD~1', { encoding: 'utf8' })
      .split('\n')
      .filter(file => file.trim())

    modifiedFiles.forEach(file => {
      try {
        const content = fs.readFileSync(file, 'utf8')

        dangerousPatterns.forEach(pattern => {
          if (pattern.test(content)) {
            issues.push({
              type: 'security',
              file,
              message: `Potential security issue: ${pattern.source}`
            })
          }
        })
      } catch (error) {
        // Skip files that can't be read
      }
    })
  } catch (error) {
    // Git command failed, skip security check
  }

  return issues
}

async function generateReport(complexityIssues, dependencyIssues, securityIssues) {
  const stats = getGitDiffStats()

  const allIssues = [...complexityIssues, ...dependencyIssues, ...securityIssues]

  // Create output directory if it doesn't exist
  const outputDirPath = path.join(process.cwd(), config.outputDir)
  if (!fs.existsSync(outputDirPath)) {
    fs.mkdirSync(outputDirPath, { recursive: true })
  }

  // Generate review report
  const report = `# Automated Code Review Report

Generated on: ${new Date().toISOString()}

## 📊 PR Summary

- **Files Changed**: ${stats.filesChanged}
- **Lines Added**: ${stats.linesAdded}
- **Lines Deleted**: ${stats.linesDeleted}
- **Total Changes**: ${stats.insertions + stats.deletions}

## 🚨 Critical Issues

${securityIssues.length > 0 ? securityIssues.map(issue =>
  `- **${issue.file}**: ${issue.message}`
).join('\n') : 'No critical security issues found.'}

## ⚠️ Quality Issues

${complexityIssues.length > 0 ? complexityIssues.map(issue =>
  `- **${issue.file}:${issue.line}**: ${issue.message}`
).join('\n') : 'No quality issues found.'}

## 🔧 Dependency Issues

${dependencyIssues.length > 0 ? dependencyIssues.map(issue =>
  `- **${issue.name}**: ${issue.message}`
).join('\n') : 'No dependency issues found.'}

## 📋 Quality Metrics

${generateMetrics(stats, allIssues)}

## 🎯 Recommendations

${generateRecommendations(stats, allIssues)}

---
*This report was automatically generated by the code review system.*
`

  // Write report
  fs.writeFileSync(path.join(outputDirPath, config.outputFile), report)

  // Generate metrics JSON
  const metrics = {
    timestamp: new Date().toISOString(),
    prStats: stats,
    issues: {
      total: allIssues.length,
      complexity: complexityIssues.length,
      dependencies: dependencyIssues.length,
      security: securityIssues.length
    },
    metrics: calculateMetrics(stats, allIssues)
  }

  fs.writeFileSync(path.join(outputDirPath, config.metricsFile), JSON.stringify(metrics, null, 2))
}

function generateMetrics(stats, issues) {
  const complexityScore = Math.max(0, 100 - (issues.length * 5))
  const sizeScore = Math.max(0, 100 - (stats.linesAdded / 10))
  const totalScore = Math.round((complexityScore + sizeScore) / 2)

  return `
- **Code Complexity Score**: ${complexityScore}/100
- **Change Size Score**: ${sizeScore}/100
- **Overall Quality Score**: ${totalScore}/100
- **Issues per File**: ${stats.filesChanged > 0 ? (issues.length / stats.filesChanged).toFixed(2) : 0}
`
}

function generateRecommendations(stats, issues) {
  const recommendations = []

  if (stats.filesChanged > config.maxFilesChanged) {
    recommendations.push('Consider breaking this PR into smaller, more focused changes.')
  }

  if (stats.linesAdded > config.maxLinesAdded) {
    recommendations.push('Large PR detected. Consider if changes can be split or simplified.')
  }

  if (issues.length > 10) {
    recommendations.push('Multiple quality issues found. Address critical ones first.')
  }

  if (issues.filter(i => i.type === 'security').length > 0) {
    recommendations.push('Security issues detected. These must be addressed before merging.')
  }

  if (recommendations.length === 0) {
    recommendations.push('No major issues detected. PR appears ready for review.')
  }

  return recommendations.map(rec => `- ${rec}`).join('\n')
}

function calculateMetrics(stats, issues) {
  return {
    complexityIndex: calculateComplexityIndex(stats, issues),
    maintainability: calculateMaintainability(stats, issues),
    technicalDebt: calculateTechnicalDebt(stats, issues)
  }
}

function calculateComplexityIndex(stats, issues) {
  const complexityPoints = issues.filter(i => i.type === 'complexity').length
  return Math.max(0, 100 - (complexityPoints * 10))
}

function calculateMaintainability(stats, issues) {
  const maintainabilityPoints = stats.filesChanged / (issues.length || 1)
  return Math.min(100, maintainabilityPoints * 10)
}

function calculateTechnicalDebt(stats, issues) {
  const debtHours = issues.length * 0.5 // Assume 30 minutes per issue
  return {
    hours: debtHours.toFixed(1),
    issues: issues.length
  }
}

// Execute report generation
(async () => {
  const complexityIssues = await analyzeComplexity()
  const dependencyIssues = checkDependencies()
  const securityIssues = checkSecurity()

  await generateReport(complexityIssues, dependencyIssues, securityIssues)
  console.log('✅ Code review report generated successfully')
})()