#!/usr/bin/env node

// Code quality metrics calculator
import fs from 'fs'
import path from 'path'
import { fileURLToPath } from 'url'

const __filename = fileURLToPath(import.meta.url)
const __dirname = path.dirname(__filename)

// Configuration
const config = {
  maxComplexity: 10,
  maxFileLines: 500,
  maxMethodLines: 50,
  excludeDirs: ['node_modules', 'dist', 'build', 'bin', '.git'],
  fileTypes: ['.cs', '.js', '.ts', '.jsx', '.tsx']
}

// Helper functions
function getAllFiles(dir) {
  const files = []

  function traverse(currentDir) {
    const entries = fs.readdirSync(currentDir, { withFileTypes: true })

    for (const entry of entries) {
      const fullPath = path.join(currentDir, entry.name)

      if (entry.isDirectory()) {
        if (!config.excludeDirs.some(exclude => entry.name.includes(exclude))) {
          traverse(fullPath)
        }
      } else {
        if (config.fileTypes.some(type => entry.name.endsWith(type))) {
          files.push(fullPath)
        }
      }
    }
  }

  traverse(dir)
  return files
}

function calculateCyclomaticComplexity(content) {
  const complexity = 0

  // Count decision points
  const decisionPatterns = [
    /\bif\b/g,
    /\belse\b/g,
    /\bswitch\b/g,
    /\bcase\b/g,
    /\bfor\b/g,
    /\bwhile\b/g,
    /\bcatch\b/g,
    /\b&&\b/g,
    /\b\|\|\b/g,
    /\?\s/g,
    /\*\*\s*\w+/g // nullish coalescing and optional chaining in JS
  ]

  let totalDecisions = 0
  decisionPatterns.forEach(pattern => {
    const matches = content.match(pattern)
    if (matches) {
      totalDecisions += matches.length
    }
  })

  // Add 1 for base complexity
  return totalDecisions + 1
}

function analyzeFile(filePath) {
  try {
    const content = fs.readFileSync(filePath, 'utf8')
    const lines = content.split('\n')
    const extension = path.extname(filePath)

    const metrics = {
      filePath,
      extension,
      lines: {
        total: lines.length,
        code: 0,
        comments: 0,
        blank: 0
      },
      complexity: {
        file: 0,
        methods: []
      },
      maintainability: {
        index: 0,
        score: 0
      },
      issues: []
    }

    // Count lines
    lines.forEach(line => {
      const trimmed = line.trim()
      if (trimmed === '') {
        metrics.lines.blank++
      } else if (trimmed.startsWith('//') || trimmed.startsWith('/*') || trimmed.startsWith('*')) {
        metrics.lines.comments++
      } else {
        metrics.lines.code++
      }
    })

    // Calculate complexity
    metrics.complexity.file = calculateCyclomaticComplexity(content)

    // Analyze methods
    let currentMethod = null
    const methodPattern = extension === '.cs'
      ? /(public|private|protected|internal)\s+(static\s+)?\w+\s+\w+\s*\(/g
      : /(function\s+\w+\s*\(|\w+\s*=\s*function\s*\(|\w+\s*\([^)]*\)\s*=>|class\s+\w+|const\s+\w+\s*=\s*\()/g

    let match
    while ((match = methodPattern.exec(content)) !== null) {
      const methodLine = content.substring(0, match.index).split('\n').length
      const methodName = match[0].match(/(?:function\s+|class\s+|const\s+|public\s+|private\s+|protected\s+|internal\s+)(\w+)/)?.[1] || 'anonymous'

      // Extract method body (simplified)
      let methodBody = ''
      let braceCount = 0
      let bodyStart = match.index + match[0].length
      let i = bodyStart

      while (i < content.length && braceCount >= 0) {
        if (content[i] === '{') braceCount++
        if (content[i] === '}') braceCount--
        methodBody += content[i]
        i++
      }

      const methodComplexity = calculateCyclomaticComplexity(methodBody)

      // Check for long methods
      if (methodBody.length > config.maxMethodLines * 50) { // rough estimate
        metrics.issues.push({
          type: 'warning',
          message: `Method "${methodName}" is too long (${Math.round(methodBody.length / 50)} lines)`
        })
      }

      metrics.complexity.methods.push({
        name: methodName,
        line: methodLine,
        complexity: methodComplexity
      })

      if (methodComplexity > config.maxComplexity) {
        metrics.issues.push({
          type: 'error',
          message: `Method "${methodName}" has high complexity (${methodComplexity})`
        })
      }
    }

    // Check for large files
    if (metrics.lines.total > config.maxFileLines) {
      metrics.issues.push({
        type: 'warning',
        message: `File is too large (${metrics.lines.total} lines)`
      })
    }

    // Calculate maintainability index (simplified)
    const loc = metrics.lines.code
    const cc = metrics.complexity.file
    const comments = metrics.lines.comments

    // Halstead volume (simplified)
    const n1 = (content.match(/[a-zA-Z_]\w*/g) || []).length // operators
    const n2 = (content.match(/\d+/g) || []).length // operands
    const N = n1 + n2
    const vocabulary = n1 + n2
    const volume = N * Math.log2(vocabulary)

    // Maintainability index (Card-McCabe formula)
    const maintainability = Math.max(0, 171 - 5.2 * Math.log(volume) - 0.23 * cc - 16.2 * Math.log(loc))
    metrics.maintainability.index = maintainability
    metrics.maintainability.score = Math.round(maintainability)

    return metrics
  } catch (error) {
    console.error(`Error analyzing file ${filePath}:`, error.message)
    return null
  }
}

function generateSummary(allMetrics) {
  const totalFiles = allMetrics.length
  const totalLines = allMetrics.reduce((sum, m) => sum + m.lines.code, 0)
  const avgComplexity = allMetrics.reduce((sum, m) => sum + m.complexity.file, 0) / totalFiles
  const avgMaintainability = allMetrics.reduce((sum, m) => sum + m.maintainability.score, 0) / totalFiles

  const totalIssues = allMetrics.reduce((sum, m) => sum + m.issues.length, 0)
  const errorCount = allMetrics.reduce((sum, m) => sum + m.issues.filter(i => i.type === 'error').length, 0)
  const warningCount = allMetrics.reduce((sum, m) => sum + m.issues.filter(i => i.type === 'warning').length, 0)

  return {
    summary: {
      totalFiles,
      totalLines,
      avgComplexity: avgComplexity.toFixed(2),
      avgMaintainability: avgMaintainability.toFixed(2),
      totalIssues,
      errorCount,
      warningCount
    },
    distribution: {
      complexity: allMetrics.reduce((dist, m) => {
        const range = m.complexity.file <= 5 ? 'Low' : m.complexity.file <= 10 ? 'Medium' : 'High'
        dist[range] = (dist[range] || 0) + 1
        return dist
      }, {}),
      maintainability: allMetrics.reduce((dist, m) => {
        const score = m.maintainability.score
        let range = 'Poor'
        if (score >= 65) range = 'Good'
        if (score >= 85) range = 'Excellent'
        dist[range] = (dist[range] || 0) + 1
        return dist
      }, {})
    }
  }
}

// Main execution
async function main() {
  console.log('🔍 Calculating code quality metrics...')

  const startTime = Date.now()
  const allFiles = getAllFiles('.')
  const allMetrics = []

  for (const file of allFiles) {
    const metrics = analyzeFile(file)
    if (metrics) {
      allMetrics.push(metrics)
    }
  }

  const summary = generateSummary(allMetrics)
  const endTime = Date.now()

  // Create output
  const output = {
    timestamp: new Date().toISOString(),
    duration: endTime - startTime,
    summary: summary.summary,
    distribution: summary.distribution,
    metrics: allMetrics,
    thresholds: {
      maxComplexity: config.maxComplexity,
      maxFileLines: config.maxFileLines,
      maxMethodLines: config.maxMethodLines
    }
  }

  // Save to file
  const outputDir = '.github'
  if (!fs.existsSync(outputDir)) {
    fs.mkdirSync(outputDir, { recursive: true })
  }

  fs.writeFileSync(path.join(outputDir, 'code-metrics.json'), JSON.stringify(output, null, 2))

  // Generate HTML report
  const htmlReport = generateHtmlReport(output)
  fs.writeFileSync(path.join(outputDir, 'code-metrics.html'), htmlReport)

  console.log(`✅ Metrics calculated for ${allFiles.length} files`)
  console.log(`📊 Summary: ${summary.summary.totalFiles} files, ${summary.summary.totalLines} lines, ${summary.summary.totalIssues} issues`)
  console.log(`⚠️  Errors: ${summary.summary.errorCount}, Warnings: ${summary.summary.warningCount}`)
}

function generateHtmlReport(output) {
  const { summary, distribution } = output

  return `
<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Code Quality Metrics</title>
    <style>
        body { font-family: Arial, sans-serif; margin: 20px; background-color: #f5f5f5; }
        .container { max-width: 1200px; margin: 0 auto; background: white; padding: 20px; border-radius: 8px; box-shadow: 0 2px 10px rgba(0,0,0,0.1); }
        .header { text-align: center; margin-bottom: 30px; }
        .metrics { display: grid; grid-template-columns: repeat(auto-fit, minmax(250px, 1fr)); gap: 20px; margin-bottom: 30px; }
        .metric-card { background: #f8f9fa; padding: 20px; border-radius: 8px; border-left: 4px solid #007bff; }
        .metric-value { font-size: 2em; font-weight: bold; color: #007bff; }
        .metric-label { color: #666; margin-top: 5px; }
        .distribution { display: grid; grid-template-columns: 1fr 1fr; gap: 20px; margin-bottom: 30px; }
        .dist-card { background: #fff3cd; padding: 20px; border-radius: 8px; }
        .error { color: #dc3545; }
        .warning { color: #ffc107; }
        .info { color: #17a2b8; }
        .good { color: #28a745; }
        .table { width: 100%; border-collapse: collapse; margin-top: 20px; }
        .table th, .table td { padding: 10px; text-align: left; border-bottom: 1px solid #ddd; }
        .table th { background-color: #f8f9fa; }
    </style>
</head>
<body>
    <div class="container">
        <div class="header">
            <h1>📊 Code Quality Metrics Report</h1>
            <p>Generated on ${new Date(output.timestamp).toLocaleString()}</p>
        </div>

        <div class="metrics">
            <div class="metric-card">
                <div class="metric-value">${summary.totalFiles}</div>
                <div class="metric-label">Total Files</div>
            </div>
            <div class="metric-card">
                <div class="metric-value">${summary.totalLines}</div>
                <div class="metric-label">Lines of Code</div>
            </div>
            <div class="metric-card">
                <div class="metric-value">${summary.avgComplexity}</div>
                <div class="metric-label">Avg Complexity</div>
            </div>
            <div class="metric-card">
                <div class="metric-value">${summary.avgMaintainability}</div>
                <div class="metric-label">Avg Maintainability</div>
            </div>
        </div>

        <div class="distribution">
            <div class="dist-card">
                <h3>Complexity Distribution</h3>
                ${Object.entries(distribution.complexity).map(([range, count]) =>
                    `<div class="${range.toLowerCase()}">${range}: ${count} files</div>`
                ).join('')}
            </div>
            <div class="dist-card">
                <h3>Maintainability Distribution</h3>
                ${Object.entries(distribution.maintainability).map(([range, count]) =>
                    `<div class="${range.toLowerCase()}">${range}: ${count} files</div>`
                ).join('')}
            </div>
        </div>

        <div style="background: #f8d7da; padding: 15px; border-radius: 8px; margin-bottom: 20px;">
            <h3>🚨 Issues Summary</h3>
            <p class="error">Critical Errors: ${summary.errorCount}</p>
            <p class="warning">Warnings: ${summary.warningCount}</p>
            <p class="info">Total Issues: ${summary.totalIssues}</p>
        </div>

        <h3>Thresholds Applied</h3>
        <table class="table">
            <tr>
                <th>Maximum Complexity</th>
                <th>Maximum File Lines</th>
                <th>Maximum Method Lines</th>
            </tr>
            <tr>
                <td>${output.thresholds.maxComplexity}</td>
                <td>${output.thresholds.maxFileLines}</td>
                <td>${output.thresholds.maxMethodLines}</td>
            </tr>
        </table>
    </div>
</body>
</html>
  `
}

main().catch(console.error)