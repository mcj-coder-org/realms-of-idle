#!/usr/bin/env node
/**
 * TaskCompleted hook — deterministic quality gate
 * Exit 2 = block task completion (Agent Teams protocol)
 * stderr = feedback to the agent
 */
const { execSync } = require('child_process');

try {
  execSync('dotnet build RealmsOfIdle.slnx --warnaserror', {
    stdio: ['pipe', 'pipe', 'pipe'],
    encoding: 'utf8',
  });
} catch (error) {
  console.error('BUILD FAILED — fix errors before completing this task:');
  console.error((error.stderr || error.stdout || '').split('\n').slice(-20).join('\n'));
  process.exit(2);
}

try {
  execSync(
    'dotnet test RealmsOfIdle.slnx --filter "Category!=E2E&Category!=Integration" --no-build',
    {
      stdio: ['pipe', 'pipe', 'pipe'],
      encoding: 'utf8',
    }
  );
} catch (error) {
  console.error('TESTS FAILED — fix failing tests before completing this task:');
  console.error((error.stderr || error.stdout || '').split('\n').slice(-30).join('\n'));
  process.exit(2);
}

console.error('Quality gate passed: build clean, tests green');
process.exit(0);
