#!/usr/bin/env node
/**
 * postToolUse Hook - Auto-format files after Edit/Write operations
 *
 * Uses lint-staged configuration from package.json for centralized linter management.
 *
 * Receives JSON via stdin with fields: tool_name, tool_input, tool_response
 * Must echo input to stdout unchanged.
 */

const { execSync } = require('child_process');
const fs = require('fs');
const path = require('path');

const SKIP_EXTENSIONS = new Set([
  '.exe',
  '.dll',
  '.pdb',
  '.so',
  '.dylib',
  '.bin',
  '.png',
  '.jpg',
  '.jpeg',
  '.gif',
  '.ico',
  '.svg',
  '.zip',
  '.tar',
  '.gz',
  '.7z',
  '.rar',
  '.pdf',
  '.doc',
  '.docx',
  '.xls',
  '.xlsx',
]);

const SKIP_DIRS = new Set([
  'node_modules',
  '.git',
  'bin',
  'obj',
  'dist',
  'build',
  'target',
  'vendor',
  '.venv',
  'venv',
  '.claude',
]);

// Commands that need npx prefix
const NPX_COMMANDS = new Set([
  'prettier',
  'markdownlint-cli2',
  'eslint',
  'typescript',
  'tsc',
  'biome',
  'dprint',
]);

function shouldSkipFile(filePath) {
  const ext = path.extname(filePath).toLowerCase();
  const dirName = path.basename(path.dirname(filePath));
  return SKIP_EXTENSIONS.has(ext) || SKIP_DIRS.has(dirName);
}

function runCommand(cmd, cwd) {
  try {
    execSync(cmd, { cwd: cwd || process.cwd(), stdio: 'pipe', timeout: 10000 });
    return true;
  } catch (error) {
    return false;
  }
}

function getLintStagedConfig(projectRoot) {
  const packageJsonPath = path.join(projectRoot, 'package.json');
  if (!fs.existsSync(packageJsonPath)) return null;

  try {
    const packageJson = JSON.parse(fs.readFileSync(packageJsonPath, 'utf8'));
    return packageJson['lint-staged'] || null;
  } catch (e) {
    return null;
  }
}

function globToRegex(pattern) {
  let regex = pattern;
  regex = regex.replace(/\{([^}]+)\}/g, (match, contents) => {
    const options = contents.split(',').map(s => s.trim());
    return (
      '(' + options.map(opt => opt.replace(/\./g, '\\\\.').replace(/\*/g, '.*')).join('|') + ')'
    );
  });
  regex = regex.replace(/^\*\*\//, '.*');
  regex = regex.replace(/(?<!\.)\*/g, '[^/]*');
  regex = regex.replace(/\\\./g, '\\\\.');
  return '^' + regex + '$';
}

function matchesPattern(fileName, pattern) {
  const regex = new RegExp(globToRegex(pattern));
  return regex.test(fileName);
}

function formatWithLintStaged(filePath, projectRoot) {
  const config = getLintStagedConfig(projectRoot);
  if (!config) {
    const ext = path.extname(filePath).toLowerCase();
    const prettierExts = [
      '.js',
      '.jsx',
      '.ts',
      '.tsx',
      '.json',
      '.yaml',
      '.yml',
      '.css',
      '.scss',
      '.html',
      '.vue',
      '.svelte',
    ];
    if (prettierExts.includes(ext)) {
      runCommand(`npx prettier --write "${filePath}"`, projectRoot);
    }
    return;
  }

  const fileName = path.basename(filePath);

  for (const [pattern, commands] of Object.entries(config)) {
    if (matchesPattern(fileName, pattern)) {
      const cmds = Array.isArray(commands) ? commands : [commands];

      for (const cmd of cmds) {
        if (cmd.includes('dotnet format')) {
          runCommand(`dotnet format . --no-restore --include "${filePath}"`, projectRoot);
        } else {
          const parts = cmd.trim().split(/\s+/);
          const commandName = parts[0];
          const args = parts.slice(1).join(' ');

          // Add npx prefix for npm-based tools
          const prefix = NPX_COMMANDS.has(commandName) ? 'npx ' : '';
          runCommand(`${prefix}${commandName} ${args} "${filePath}"`, projectRoot);
        }
      }
      return;
    }
  }
}

function main() {
  let inputData = '';

  process.stdin.setEncoding('utf8');
  process.stdin.on('data', chunk => {
    inputData += chunk;
  });

  process.stdin.on('end', () => {
    console.log(inputData);

    let data;
    try {
      data = JSON.parse(inputData);
    } catch (e) {
      return;
    }

    const toolName = data.tool_name;
    const filePath = data.tool_input?.file_path;

    if ((toolName === 'Edit' || toolName === 'Write') && filePath) {
      const absolutePath = path.isAbsolute(filePath)
        ? filePath
        : path.resolve(process.cwd(), filePath);

      if (shouldSkipFile(absolutePath)) return;

      formatWithLintStaged(absolutePath, process.cwd());
    }
  });
}

main();
