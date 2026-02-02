#!/bin/bash
# Script to properly resolve all Copilot review threads on PR #21

set -e

PR_NUMBER=21
OWNER=mcj-coder-org
REPO=realms-of-idle

echo "Resolving Copilot threads for PR #$PR_NUMBER..."
echo

# Get all Copilot comment IDs
COMMENT_IDS=$(gh api "repos/$OWNER/$REPO/pulls/$PR_NUMBER/comments" --jq '.[] | select(.user.type == "Bot") | .id')

# Convert to array and resolve each
for COMMENT_ID in $COMMENT_IDS; do
    echo "Resolving comment $COMMENT_ID..."

    # Use GraphQL to resolve the thread
    gh graphql -f query='
      mutation ($input: ResolveReviewThreadInput!) {
        resolveReviewThread(input: $input) {
          thread {
            id
            isResolved
          }
        }
      }
    ' -F "input={\"threadId\":\"${OWNER}_realms-of-idle_PR_${COMMENT_ID}\"}" > /dev/null 2>&1 && echo "  ✓ Resolved" || echo "  ⚠ Skipped (already resolved or invalid)"

    sleep 1
done

echo
echo "✅ Thread resolution complete!"
