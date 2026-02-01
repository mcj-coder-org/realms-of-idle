#!/usr/bin/env node

// Local DangerJS runner for development/testing
// This script simulates DangerJS in a local environment

import { spawn } from 'child_process'
import fs from 'fs'
import path from 'path'

console.log('🚀 Running DangerJS locally...')

// Check if we're in a git repository
if (!fs.existsSync('.git')) {
  console.error('❌ Not a git repository')
  process.exit(1)
}

// Get current branch
const getCurrentBranch = () => {
  try {
    const result = spawnSync('git', ['branch', '--show-current'])
    return result.stdout.toString().trim()
  } catch (error) {
    console.error('❌ Failed to get current branch')
    process.exit(1)
  }
}

// Get PR info from current branch
const branch = getCurrentBranch()
console.log(`🌿 Current branch: ${branch}`)

// Run DangerJS with local setup
const dangerCommand = 'npx danger local --dangerfile=danger.js --base=main'

console.log(`🔍 Running: ${dangerCommand}`)

const child = spawn('npx', ['danger', 'local', '--dangerfile=danger.js', '--base=main'], {
  stdio: 'inherit',
  shell: true,
})

child.on('close', (code) => {
  if (code === 0) {
    console.log('✅ DangerJS checks passed')
  } else {
    console.log('❌ DangerJS checks failed')
  }
  process.exit(code)
})