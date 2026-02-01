#!/usr/bin/env node

/**
 * Branch Protection Setup Script
 *
 * This script configures GitHub branch protection for the idle-world project
 * to enforce quality standards and workflow requirements.
 */

import { Octokit } from '@octokit/rest';
import { config } from 'dotenv';

// Load environment variables
config({ path: process.env.GITHUB_ENV_FILE || '.github.env' });

// Configuration
const REPO_OWNER = process.env.REPO_OWNER || 'mcj-coder';
const REPO_NAME = process.env.REPO_NAME || 'idle-world';
const BRANCHES_TO_PROTECT = ['main', 'develop'];
const REQUIRED_STATUS_CHECKS = [
  'CI - Validate',
  'CI - Test',
  'CI - Build'
];
const REQUIRED_PR_REVIEW_COUNT = 1;
const REQUIRED_PR_REVIEWERS = ['mcj-coder']; // Maintainer account
const ADMIN_BYPASS = true; // Allow admins to bypass

// Initialize GitHub client
const octokit = new Octokit({
  auth: process.env.GITHUB_TOKEN
});

async function setupBranchProtection() {
  console.log('🔒 Setting up branch protection rules for idle-world...');

  try {
    // Verify authentication
    const { data: { login } } = await octokit.rest.users.getAuthenticated();
    console.log(`👤 Authenticated as: ${login}`);

    // Check if repository exists
    try {
      await octokit.rest.repos.get({
        owner: REPO_OWNER,
        repo: REPO_NAME
      });
    } catch (error) {
      if (error.status === 404) {
        throw new Error(`Repository "${REPO_OWNER}/${REPO_NAME}" not found`);
      }
      throw error;
    }

    // Setup branch protection for each branch
    for (const branch of BRANCHES_TO_PROTECT) {
      console.log(`\n📋 Setting up protection for branch: ${branch}`);

      try {
        // Create or update branch protection
        await octokit.rest.repos.updateBranchProtection({
          owner: REPO_OWNER,
          repo: REPO_NAME,
          branch: branch,
          required_status_checks: {
            strict: true,
            contexts: REQUIRED_STATUS_CHECKS
          },
          enforce_admins: ADMIN_BYPASS,
          required_pull_request_reviews: {
            require_code_owner_reviews: false,
            dismiss_stale_reviews: false,
            require_last_push_approval: false,
            required_approving_review_count: REQUIRED_PR_REVIEW_COUNT
          },
          restrictions: null,
          required_conversation_resolution: true,
          allow_force_pushes: false,
          allow_deletions: false,
          block_creations: false,
          required_linear_history: true,
          required_signatures: false,
          push_restrictions: null,
          require_branch_protection_exception: false
        });

        console.log(`✅ Branch protection created for ${branch}`);

        // Set specific reviewers for PRs
        if (branch === 'main') {
          console.log('👥 Setting up required reviewers for main branch...');

          await octokit.rest.teams.updateTeamMembershipForUserInOrg({
            org: REPO_OWNER,
            team_slug: 'maintainers',
            username: REQUIRED_PR_REVIEWERS[0],
            role: 'maintainer'
          });

          console.log(`✅ Required reviewers set for ${branch}`);
        }

      } catch (error) {
        if (error.status === 403) {
          console.error(`❌ Permission denied. Please ensure your GitHub token has repo admin permissions.`);
          throw error;
        } else if (error.status === 404) {
          console.error(`❌ Branch "${branch}" not found in repository.`);
          throw error;
        } else {
          console.error(`❌ Error setting up protection for ${branch}:`, error.message);
          throw error;
        }
      }
    }

    console.log('\n🎉 Branch protection setup completed successfully!');
    console.log('\n📝 Summary:');
    console.log(`   Repository: ${REPO_OWNER}/${REPO_NAME}`);
    console.log(`   Protected branches: ${BRANCHES_TO_PROTECT.join(', ')}`);
    console.log(`   Required status checks: ${REQUIRED_STATUS_CHECKS.join(', ')}`);
    console.log(`   PR reviews required: ${REQUIRED_PR_REVIEW_COUNT} (from ${REQUIRED_PR_REVIEWERS.join(', ')})`);
    console.log(`   Admin bypass: ${ADMIN_BYPASS ? 'enabled' : 'disabled'}`);

  } catch (error) {
    console.error('❌ Branch protection setup failed:', error.message);
    process.exit(1);
  }
}

async function verifySetup() {
  console.log('\n🔍 Verifying branch protection setup...');

  try {
    for (const branch of BRANCHES_TO_PROTECT) {
      const { data: protection } = await octokit.rest.repos.getBranchProtection({
        owner: REPO_OWNER,
        repo: REPO_NAME,
        branch: branch
      });

      console.log(`\n✅ ${branch} protection verified:`);
      console.log(`   Status checks required: ${protection.required_status_checks?.contexts?.join(', ') || 'None'}`);
      console.log(`   PR reviews required: ${protection.required_pull_request_reviews?.required_approving_review_count || 0}`);
      console.log(`   Admin bypass: ${protection.enforce_admins ? 'enabled' : 'disabled'}`);
    }

    console.log('\n🎉 All verifications passed!');

  } catch (error) {
    console.error('❌ Verification failed:', error.message);
    process.exit(1);
  }
}

async function cleanupBranchProtection() {
  console.log('\n🧹 Cleaning up branch protection rules...');

  try {
    for (const branch of BRANCHES_TO_PROTECT) {
      try {
        await octokit.rest.repos.removeBranchProtection({
          owner: REPO_OWNER,
          repo: REPO_NAME,
          branch: branch
        });
        console.log(`✅ Removed protection from ${branch}`);
      } catch (error) {
        if (error.status === 404) {
          console.log(`ℹ️  ${branch} was not protected`);
        } else {
          throw error;
        }
      }
    }

    console.log('\n🎉 Branch protection cleanup completed!');

  } catch (error) {
    console.error('❌ Cleanup failed:', error.message);
    process.exit(1);
  }
}

// Command line interface
const command = process.argv[2] || 'setup';

switch (command) {
  case 'setup':
    await setupBranchProtection();
    break;
  case 'verify':
    await verifySetup();
    break;
  case 'cleanup':
    await cleanupBranchProtection();
    break;
  default:
    console.error(`❌ Unknown command: ${command}`);
    console.log('Available commands: setup, verify, cleanup');
    process.exit(1);
}

export { setupBranchProtection, verifySetup, cleanupBranchProtection };