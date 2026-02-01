// DangerJS project configuration
// Override global settings for this project

module.exports = {
  // DangerJS configuration
  failOnErrors: true,
  failOnWarnings: false,

  // Project-specific settings
  project: {
    name: 'cozy-fantasy-rpg',
    description: 'Cozy Slice-of-Life Fantasy RPG - Mobile/Web Unity Game',
    conventionalCommits: {
      enabled: true,
      pattern: /^(feat|fix|docs|style|refactor|test|chore)(\([^)]+\))?: .+/,
      scopes: ['core', 'ui', 'gameplay', 'audio', 'build', 'docs', 'ci'],
    },

    // File patterns to watch
    patterns: {
      sourceFiles: ['Assets/**/*.cs', 'src/**/*', 'lib/**/*', 'core/**/*'],
      testFiles: ['**/*.test.*', '**/*.spec.*', 'test/**/*', 'tests/**/*'],
      docsFiles: ['docs/**/*', '**/*.md'],
      configFiles: ['*.json', '*.yml', '*.yaml', '*.toml'],
    },

    // PR-specific rules
    prRules: {
      maxFilesChanged: 50,
      maxLinesAdded: 1000,
      requireTestsForSourceChanges: true,
      requireIssueReference: true,
      conventionalBranchNaming: true,
      checkForMergeConflicts: true,
    },

    // Custom danger plugins
    plugins: [],

    // Ignore certain patterns
    ignore: [
      '*.min.js',
      '*.min.css',
      'node_modules/**/*',
      'dist/**/*',
      'build/**/*',
      'bin/**/*',
      '.git/**/*',
      '.DS_Store',
      'temp/**/*',
    ],
  },

  // Rules for specific file types
  fileTypeRules: {
    cs: {
      checkForTODOs: true,
      checkForDebugStatements: true,
      checkForConsoleLogs: true,
    },
    js: {
      checkForTODOs: true,
      checkForConsoleLogs: true,
      checkForAlerts: true,
    },
    md: {
      checkForBrokenLinks: false, // Handled by markdown-link-check
      checkForTODOs: true,
    },
    json: {
      checkForSecrets: true,
      checkForSensitiveData: true,
    },
  },

  // Override global settings
  settings: {
    github: {
      api: 'https://api.github.com',
      token: process.env.DANGER_GITHUB_API_TOKEN || '',
    },
  },
}