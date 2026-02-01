---
type: playbook
scope: detailed
status: approved
version: 1.0.0
created: 2026-02-01
updated: 2026-02-01
subjects:
  - branch-protection
  - ci-cd
  - github
  - workflow
dependencies:
  - doc: dual-account-workflow.md
    type: extends
  - doc: dor-dod-guide.md
    type: requires
---

# Branch Protection Setup Guide

This guide walks you through configuring branch protection rules for the `realms-of-idle` repository.

## Prerequisites

- GitHub CLI installed and authenticated
- Admin access to `mcj-coder-org/realms-of-idle`
- Understanding of the dual-account workflow (Contributor vs Maintainer)

## Setup Steps

### Step 1: Navigate to Branch Protection Settings

1. Go to: <https://github.com/mcj-coder-org/realms-of-idle/settings/branches>
2. Click "Add rule" or edit existing `main` branch rule

### Step 2: Configure Branch Protection Rule

#### Branch Name Pattern

- **Branch name pattern**: `main`

#### Rule Settings

**✅ Require a pull request before merging**

- **Require approvals**: `1` approval
- **Dismiss stale reviews**: ✅ checked
- **Require review from Code Owners**: ✅ checked
- **Allow specified actors to bypass**: Add `mcj-coder` (Maintainer)

**✅ Require status checks to pass before merging**

- **Require branches to be up to date before merging**: ✅ checked

**Required Status Checks** (add all 7):

1. `validate-pr-metadata`
2. `lint-validation`
3. `dotnet-build`
4. `test-and-coverage`
5. `danger-validation` ← **CRITICAL** (DoR/DoD enforcement)
6. `security-scan`
7. `pr-status`

**✅ Require branches to be up to date before merging**: ✅ checked

**✅ Do not allow bypassing the above settings**: ✅ checked

### Step 3: Additional Protection Rules

**✅ Require linear history**: ✅ checked (prevents merge commits)

**✅ Require conversation resolution before merging**: ✅ checked

**❌ Allow force pushes**: ❌ unchecked (block force pushes)

**❌ Allow deletions**: ❌ unchecked (block branch deletion)

**✅ Restrict who can push to this branch**:

- Only `mcj-coder` (Maintainer account)

### Step 4: Save the Rule

Click "Create" or "Save changes" to apply the branch protection rule.

## Verification

After setup, verify the configuration:

```bash
# View current branch protection
gh api repos/mcj-coder-org/realms-of-idle/branches/main/protection
```

Expected output should show:

- `required_pull_request_reviews.required_approving_review_count: 1`
- `required_status_checks.contexts` includes all 7 checks
- `enforce_admins: true`
- `allow_deletions: false`
- `allow_force_pushes: false`
- `required_linear_history: true`

## What This Enforces

### Before Merge (Contributor)

1. Must create PR from feature branch
2. All 7 CI checks must pass:
   - PR metadata validation (branch naming, commit signatures, issue refs)
   - Lint validation (0 issues)
   - .NET build (0 warnings)
   - Tests (0 failures, ≥80% coverage)
   - **DangerJS validation** (DoR/DoD - **BLOCKING**)
   - Security scan (0 vulnerabilities)
   - Final status gate
3. Maintainer (@mcj-coder) must approve
4. All conversation threads must be resolved
5. Branch must be up to date with main

### During Merge (Maintainer Only)

1. Only `mcj-coder` account can merge to main
2. Must use rebase (no merge commits allowed)
3. Auto-merge can be enabled after approval
4. All checks must pass before merge completes

### Quality Gates (Automated)

- **Definition of Ready (DoR)**:
  - Issue has acceptance criteria section
  - Issue is assigned to Contributor
  - No blocking dependencies

- **Definition of Done (DoD)**:
  - All acceptance criteria checked
  - 0 test failures
  - 0 build warnings
  - 0 linting issues
  - Coverage ≥80%
  - Maintainer approved
  - Evidence artifacts attached

## Troubleshooting

### Issue: "danger-validation" check not available

**Solution**: This check will appear automatically after you create your first PR and the DangerJS workflow runs for the first time. You can add it to the required checks list after it appears.

### Issue: Can't bypass rules as maintainer

**Solution**: Add `mcj-coder` to the "Allow specified actors to bypass pull request requirements" list in the PR review section.

### Issue: Rebase not working

**Solution**: Ensure your local git is configured to use rebase:

```bash
git config pull.rebase true
git config pull.ff only
```

### Issue: Force push blocked

**Solution**: This is intentional. If you need to force push (rare), temporarily disable branch protection, force push, then re-enable protection immediately.

## Testing the Configuration

### Test 1: Verify PR Requirements

1. Switch to Contributor account:

   ```bash
   git config user.email "m.c.j@live.co.uk"
   git config user.name "mcj-codificer"
   gh auth switch
   ```

2. Create a test branch and PR:

   ```bash
   git checkout -b feat/test-branch-protection
   echo "test" > test.txt
   git add test.txt
   git commit -S -m "test: verify branch protection #1"
   git push -u origin feat/test-branch-protection
   gh pr create --body "Fixes #1"
   ```

3. Verify that:
   - PR cannot be merged (should see "Waiting for required status checks")
   - All 7 checks appear in the checks list
   - Maintainer approval is required

### Test 2: Verify Maintainer Merge

1. Switch to Maintainer account:

   ```bash
   git config user.email "martin.cjarvis@googlemail.com"
   git config user.name "mcj-coder"
   gh auth switch
   ```

2. After all checks pass, verify:
   - You can approve the PR
   - You can enable auto-merge
   - Non-maintainer accounts cannot merge

## Alternative: Automated Setup via GitHub CLI

If you prefer automation, you can use this command (requires jq):

```bash
gh api \
  --method PUT \
  -H "Accept: application/vnd.github+json" \
  repos/mcj-coder-org/realms-of-idle/branches/main/protection \
  -f required_pull_request_reviews='{"required_approving_review_count":1}' \
  -f required_status_checks='{"strict":true,"checks":[{"context":"validate-pr-metadata"},{"context":"lint-validation"},{"context":"dotnet-build"},{"context":"test-and-coverage"},{"context":"danger-validation"},{"context":"security-scan"},{"context":"pr-status"}]}' \
  -f enforce_admins=true \
  -f allow_deletions=false \
  -f allow_force_pushes=false \
  -f allow_deletions=false
```

**Note**: The automated setup via CLI may fail depending on repository settings. Manual setup via UI is recommended.

## Related Documentation

- [Dual-Account Workflow Guide](./dual-account-workflow.md)
- [DoR/DoD Guide](./dor-dod-guide.md)
- [CI/CD Setup Guide](./ci-cd-setup.md)

## Support

If you encounter issues:

1. Check GitHub Actions logs for failed workflows
2. Verify DangerJS configuration in `.github/dangerfile.ts`
3. Ensure all status checks are properly named
4. Confirm both GitHub accounts have proper permissions
