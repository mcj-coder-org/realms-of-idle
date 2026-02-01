/**
 * Commitlint configuration for Fantasy RPG World
 *
 * Enforces Conventional Commits format:
 *   <type>(<scope>): <description>
 *
 * @see https://www.conventionalcommits.org/
 * @see https://commitlint.js.org/
 */
export default {
  extends: ['@commitlint/config-conventional'],
  rules: {
    // Type must be one of the allowed values
    'type-enum': [
      2,
      'always',
      [
        'feat', // New feature
        'fix', // Bug fix
        'docs', // Documentation
        'refactor', // Code restructuring
        'test', // Test changes
        'chore', // Build/tooling
        'style', // Formatting
        'perf', // Performance
        'ci', // CI configuration
        'build', // Build system
        'revert', // Revert previous
      ],
    ],
    // Type is required
    'type-empty': [2, 'never'],
    // Subject is required
    'subject-empty': [2, 'never'],
    // Subject should not end with period
    'subject-full-stop': [2, 'never', '.'],
    // Subject max length
    'subject-max-length': [2, 'always', 100],
    // Header max length (type + scope + subject)
    'header-max-length': [2, 'always', 120],
    // Body should have blank line before it
    'body-leading-blank': [2, 'always'],
    // Footer should have blank line before it
    'footer-leading-blank': [2, 'always'],
  },
};
