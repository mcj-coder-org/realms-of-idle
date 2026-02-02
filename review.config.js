// Automated Code Review Configuration
// Customize settings for different project requirements

module.exports = {
  // General settings
  project: {
    name: 'Realms of Idle',
    description: 'Cozy Slice-of-Life Fantasy RPG - Mobile/Web Unity Game',
    language: 'C#',
    framework: 'Unity',
    backend: '.NET'
  },

  // Review severity levels
  severity: {
    critical: {
      label: '🚨 Critical',
      action: 'fail',
      message: 'Must be fixed before merging'
    },
    major: {
      label: '⚠️ Major',
      action: 'warn',
      message: 'Should be fixed before merging'
    },
    minor: {
      label: '💡 Minor',
      action: 'message',
      message: 'Consider improving for better code quality'
    }
  },

  // File-specific configurations
  files: {
    // C# files
    '.cs': {
      enabled: true,
      rules: {
        maxComplexity: 10,
        maxFileLines: 500,
        maxMethodLines: 50,
        requireXmlDoc: true,
        checkForNullables: true,
        checkAsyncUsage: true
      },
      patterns: {
        // Regex patterns for C# specific rules
        longMethod: /(?:public|private|protected|internal)\s+(?:static\s+)?\w+\s+\w+\s*\{[\s\S]{500,}\}/g,
        missingXmlDoc: /(?:public|protected)\s+(?:static\s+)?(?:\w+\s+)+\w+\s*\([^)]*\)\s*\{/g,
        improperNullCheck: /if\s*\([^)]+==\s*null|\bif\s*\([^)]+!=\s*null/g
      }
    },

    // JavaScript/TypeScript files
    '.js': {
      enabled: true,
      rules: {
        maxComplexity: 15,
        maxFileLines: 400,
        maxFunctionLines: 30,
        preferConst: true,
        preferArrow: true,
        noConsoleLog: true
      },
      patterns: {
        longFunction: /function\s+\w+\s*\{[\s\S]{300,}\}/g,
        shouldUseConst: /\bvar\s+\w+/g,
        shouldUseArrow: /\.map\s*\(\s*function\s*\(/g,
        hasConsoleLog: /console\.(log|warn|error)\(/g
      }
    },

    '.ts': {
      enabled: true,
      rules: {
        maxComplexity: 15,
        maxFileLines: 400,
        maxFunctionLines: 30,
        requireTypes: true,
        checkInterfaces: true
      }
    }
  },

  // Project-specific rules
  projectRules: {
    // Unity specific
    unity: {
      checkSceneDependencies: true,
      checkScriptableObjects: true,
      checkPerformance: true,
      checkDrawCalls: true
    },

    // Backend specific
    backend: {
      checkApiSecurity: true,
      checkDatabaseQueries: true,
      checkErrorHandling: true,
      checkLogging: true
    },

    // Game specific
    game: {
      checkSaveLoad: true,
      checkGameState: true,
      checkBalance: true,
      checkAccessibility: true
    }
  },

  // Thresholds for automated decisions
  thresholds: {
    // PR size limits
    maxFilesChanged: 50,
    maxLinesAdded: 1000,
    maxPrTitleLength: 72,

    // Quality thresholds
    minTestCoverage: 80,
    maxComplexityScore: 10,
    minMaintainabilityScore: 65,

    // Performance thresholds
    maxFrameTime: 16.67, // 60 FPS in ms
    maxMemoryUsage: 512, // MB
    maxDrawCalls: 100
  },

  // Ignore patterns (files/directories to skip)
  ignore: [
    'node_modules/**/*',
    'dist/**/*',
    'build/**/*',
    'bin/**/*',
    'obj/**/*',
    '.git/**/*',
    '.vs/**/*',
    '*.csproj',
    '*.sln',
    'packages/**/*',
    'temp/**/*',
    '*.min.js',
    '*.min.css',
    'Assets/Plugins/**/*' // Skip third-party plugins
  ],

  // Custom review rules
  customRules: [
    {
      name: 'GameBalanceCheck',
      enabled: true,
      severity: 'major',
      description: 'Check game balance changes',
      check: (file, content) => {
        // Check for balance-related changes
        const balancePatterns = [
          /damage\s*=\s*\d+/gi,
          /health\s*=\s*\d+/gi,
          /speed\s*=\s*\d+/gi,
          /cost\s*=\s*\d+/gi
        ]

        return balancePatterns.some(pattern => pattern.test(content))
      }
    },
    {
      name: 'SaveGameCheck',
      enabled: true,
      severity: 'critical',
      description: 'Check save/load compatibility',
      check: (file, content) => {
        if (file.includes('Save') || file.includes('Load')) {
          // Check for version compatibility
          return !content.includes('version')
        }
        return false
      }
    },
    {
      name: 'LocalizationCheck',
      enabled: true,
      severity: 'major',
      description: 'Check for hardcoded strings',
      check: (file, content) => {
        const strings = content.match(/"[^"]*"/g) || []
        return strings.some(str => !str.match(/(?:Localization|Text|Constants)/i))
      }
    }
  ],

  // Report configuration
  report: {
    formats: ['markdown', 'json', 'html'],
    outputDir: '.github',
    includeMetrics: true,
    includeTrends: true,
    includeRecommendations: true,
    maxCommentsPerFile: 5
  },

  // GitHub integration
  github: {
    autoReview: true,
    blockOnCritical: true,
    requireIssueReference: true,
    conventionalCommits: true,
    scopes: ['core', 'ui', 'gameplay', 'audio', 'build', 'docs', 'ci', 'security']
  },

  // Development settings
  development: {
    enableExperimentalRules: false,
    debugMode: false,
    verboseLogging: false,
    cacheResults: true,
    cacheTTL: 3600 // 1 hour
  }
}