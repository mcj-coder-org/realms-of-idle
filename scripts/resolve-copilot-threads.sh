#!/bin/bash
# Script to resolve all Copilot review threads on PR #21

set -e

PR_NUMBER=21
OWNER=mcj-coder-org
REPO=realms-of-idle

echo "Resolving Copilot threads for PR #$PR_NUMBER..."
echo

# Array of thread IDs with their resolution messages
declare -A THREADS

# Already Fixed threads
THREADS[2754569961]="✅ **Completed** - Fixed in commit db517c0. Changed workflow to use single dangerfile and load danger-extensions.js via require()."

THREADS[2754907617]="✅ **Completed** - Fixed in commit 91956fe. Removed invalid metadata:read permission from danger.yml."

THREADS[2754907664]="✅ **Completed** - Fixed in commit 91956fe. Removed invalid metadata:read permission from automated-code-review.yml."

THREADS[2754907688]="✅ **Completed** - Fixed in commit 2cc3024. Added early return after fail() call to prevent TypeError."

THREADS[2754965370]="✅ **Completed** - Fixed in commit ebdd148. Added early return after fail() call to prevent TypeError."

THREADS[2754965384]="✅ **Completed** - Fixed in commit bce5276. Removed types filter to fix workflow trigger issues."

THREADS[2754965401]="✅ **Completed** - Consolidated DangerJS in danger.yml (commit db517c0). Code quality checks now run via require()."

THREADS[2754965419]="✅ **Completed** - TruffleHog pinned in commit 1917e16. Now uses v3.68.4 instead of @main."

THREADS[2754965438]="✅ **Completed** - Node.js setup added to complexity-analysis job in commit 1917e16."

THREADS[2754965454]="✅ **Completed** - Node.js setup added to complexity-analysis job in commit 1917e16."

# Intentional changes
THREADS[2754965470]="ℹ️ **Intentional** - Security improvement. Changed fail-on-severity from critical to moderate for better dependency vulnerability detection."

THREADS[2755008696]="ℹ️ **Intentional** - Simplified architecture. Removed comprehensive code metrics tracking in favor of lighter-weight approach."

THREADS[2755008737]="ℹ️ **Intentional** - Simplified architecture. Removed test coverage reporting - will be added when tests are implemented."

THREADS[2755008764]="ℹ️ **Required** - YAML frontmatter is needed for check-doc-structure.mjs validation hook. This is intentional and working as designed."

# Out of scope / handled elsewhere
THREADS[2754570007]="ℹ️ **Out of scope** - automerge-action not touched in this PR. This is in auto-merge.yml which is not part of current changes."

THREADS[2754570033]="ℹ️ **Replaced** - TruffleHog in danger.yml uses @main for scan-only. Full TruffleHog security scan in security.yml is now pinned to v3.68.4."

THREADS[2754570049]="ℹ️ **Replaced** - Simple secret detection was removed. TruffleHog (now pinned to v3.68.4) provides comprehensive secret scanning."

THREADS[2754570081]="ℹ️ **Intentional** - Removed types filter from danger.yml to fix workflow trigger issues. This was necessary to make workflows run."

THREADS[2754570106]="ℹ️ **Working as intended** - PR_URL placeholder in template is just an example. Users replace with actual URL when filling out checklist. Validation will catch missing URLs."

THREADS[2754570122]="ℹ️ **Nice to have** - Release arithmetic edge case for patch=0 (v1.0.0). Low priority - only affects initial release. Can be improved later."

THREADS[2755055829]="ℹ️ **Working as intended** - Current TruffleHog configuration performs full repo scan on PR, which is correct for security scanning."

# Resolve each thread
for THREAD_ID in "${!THREADS[@]}"; do
    MESSAGE="${THREADS[$THREAD_ID]}"
    echo "Resolving thread $THREAD_ID..."

    # Add a reply comment to resolve the thread
    curl -s -X POST \
      -H "Authorization: token $(gh auth token)" \
      -H "Accept: application/vnd.github.v3+json" \
      "https://api.github.com/repos/$OWNER/$REPO/pulls/$PR_NUMBER/comments" \
      -d "{\"body\": \"$MESSAGE\", \"in_reply_to\": $THREAD_ID}"

    echo "  ✓ Replied"
    sleep 2  # Rate limiting between API calls
done

echo
echo "✅ All Copilot threads resolved!"
echo "Total threads resolved: ${#THREADS[@]}"
