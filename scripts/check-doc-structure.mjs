#!/usr/bin/env node

/**
 * Check document structure for compliance with documentation standards
 * This is a minimal implementation for the CI/CD infrastructure setup
 */

import { readFileSync } from 'fs';
import { join } from 'path';

const FRONTMATTER_REGEX = /^---\s*\n([\s\S]*?)\n---\s*\n/;

function checkMarkdownFile(filePath) {
  try {
    const content = readFileSync(filePath, 'utf8');
    const hasFrontmatter = FRONTMATTER_REGEX.test(content);

    if (!hasFrontmatter) {
      console.warn(`⚠️  ${filePath}: Missing frontmatter`);
      return false;
    }

    return true;
  } catch (error) {
    console.error(`❌ ${filePath}: ${error.message}`);
    return false;
  }
}

// Main execution
const files = process.argv.slice(2);

if (files.length === 0) {
  console.log('✓ No markdown files to check');
  process.exit(0);
}

let allValid = true;

for (const file of files) {
  if (!file.endsWith('.md')) continue;
  if (!checkMarkdownFile(file)) {
    allValid = false;
  }
}

if (allValid) {
  console.log('✓ All markdown files have frontmatter');
}

process.exit(allValid ? 0 : 1);
