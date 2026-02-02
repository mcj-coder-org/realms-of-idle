#!/bin/bash

# Branch Protection Setup Script (Shell version)
# Alternative to the Node.js version for environments without Node.js

set -e

# Configuration
REPO_OWNER="${REPO_OWNER:-mcj-coder}"
REPO_NAME="${REPO_NAME:-idle-world}"
BRANCHES_TO_PROTECT=("main" "develop")
REQUIRED_STATUS_CHECKS=('CI - Validate' 'CI - Test' 'CI - Build')
REQUIRED_PR_REVIEW_COUNT=1
REQUIRED_PR_REVIEWERS="mcj-coder"
ADMIN_BYPASS=true

# Colors for output
RED='\033[0;31m'
GREEN='\033[0;32m'
YELLOW='\033[1;33m'
NC='\033[0m' # No Color

log_info() {
    echo -e "${GREEN}[INFO]${NC} $1"
}

log_warn() {
    echo -e "${YELLOW}[WARN]${NC} $1"
}

log_error() {
    echo -e "${RED}[ERROR]${NC} $1"
}

check_dependencies() {
    log_info "Checking dependencies..."

    if ! command -v curl &> /dev/null; then
        log_error "curl is required but not installed"
        exit 1
    fi

    if ! command -v jq &> /dev/null; then
        log_error "jq is required but not installed"
        exit 1
    fi

    if [ -z "$GITHUB_TOKEN" ]; then
        log_error "GITHUB_TOKEN environment variable is required"
        exit 1
    fi
}

check_repository() {
    log_info "Checking repository access..."

    local response=$(curl -s -o /dev/null -w "%{http_code}" \
        -H "Authorization: token $GITHUB_TOKEN" \
        "https://api.github.com/repos/$REPO_OWNER/$REPO_NAME")

    if [ "$response" -eq 200 ]; then
        log_info "Repository access verified"
    else
        log_error "Repository not found or access denied (HTTP $response)"
        exit 1
    fi
}

setup_branch_protection() {
    local branch=$1
    log_info "Setting up protection for branch: $branch"

    # Convert Bash array to JSON array
    local contexts_json=$(printf '%s\n' "${REQUIRED_STATUS_CHECKS[@]}" | jq -R . | jq -s .)

    local payload=$(jq -n \
        --argjson strict true \
        --argjson contexts "$contexts_json" \
        --argjson dismiss_stale_reviews false \
        --argjson require_code_owner_reviews false \
        --argjson require_last_push_approval false \
        --argjson required_approving_review_count "$REQUIRED_PR_REVIEW_COUNT" \
        --argjson enforce_admins "$ADMIN_BYPASS" \
        --argjson allow_force_pushes false \
        --argjson allow_deletions false \
        --argjson block_creations false \
        --argjson required_linear_history true \
        --argjson required_signatures false \
        --argjson required_conversation_resolution true \
        '{
            required_status_checks: {
                strict: $strict,
                contexts: $contexts
            },
            enforce_admins: $enforce_admins,
            required_pull_request_reviews: {
                require_code_owner_reviews: $require_code_owner_reviews,
                dismiss_stale_reviews: $dismiss_stale_reviews,
                require_last_push_approval: $require_last_push_approval,
                required_approving_review_count: $required_approving_review_count
            },
            restrictions: null,
            required_conversation_resolution: $required_conversation_resolution,
            allow_force_pushes: $allow_force_pushes,
            allow_deletions: $allow_deletions,
            block_creations: $block_creations,
            required_linear_history: $required_linear_history,
            required_signatures: $required_signatures,
            push_restrictions: null,
            require_branch_protection_exception: false
        }')

    local response=$(curl -s -o /dev/null -w "%{http_code}" -X PUT \
        -H "Authorization: token $GITHUB_TOKEN" \
        -H "Accept: application/vnd.github.v3+json" \
        -H "Content-Type: application/json" \
        -d "$payload" \
        "https://api.github.com/repos/$REPO_OWNER/$REPO_NAME/branches/$branch/protection")

    if [ "$response" -eq 200 ]; then
        log_info "✅ Branch protection created for $branch"
    else
        log_error "Failed to create protection for $branch (HTTP $response)"
        exit 1
    fi
}

verify_protection() {
    local branch=$1
    log_info "Verifying protection for branch: $branch"

    local response=$(curl -s \
        -H "Authorization: token $GITHUB_TOKEN" \
        -H "Accept: application/vnd.github.v3+json" \
        "https://api.github.com/repos/$REPO_OWNER/$REPO_NAME/branches/$branch/protection")

    if [ $? -ne 0 ]; then
        log_error "Failed to verify protection for $branch"
        exit 1
    fi

    local protection=$(echo "$response" | jq -r '.required_status_checks.contexts // empty')
    if [ -n "$protection" ]; then
        log_info "✅ $branch protection verified"
        log_info "   Status checks: $protection"
    else
        log_warn "⚠️  Protection not properly configured for $branch"
    fi
}

cleanup_protection() {
    local branch=$1
    log_info "Removing protection from branch: $branch"

    local response=$(curl -s -o /dev/null -w "%{http_code}" -X DELETE \
        -H "Authorization: token $GITHUB_TOKEN" \
        -H "Accept: application/vnd.github.v3+json" \
        "https://api.github.com/repos/$REPO_OWNER/$REPO_NAME/branches/$branch/protection")

    if [ "$response" -eq 204 ]; then
        log_info "✅ Protection removed from $branch"
    elif [ "$response" -eq 404 ]; then
        log_info "ℹ️  $branch was not protected"
    else
        log_error "Failed to remove protection from $branch (HTTP $response)"
        exit 1
    fi
}

show_summary() {
    log_info "🎉 Branch protection setup completed successfully!"
    echo ""
    log_info "📝 Summary:"
    echo "   Repository: $REPO_OWNER/$REPO_NAME"
    echo "   Protected branches: ${BRANCHES_TO_PROTECT[*]}"
    echo "   Required status checks: ${REQUIRED_STATUS_CHECKS[*]}"
    echo "   PR reviews required: $REQUIRED_PR_REVIEW_COUNT (from $REQUIRED_PR_REVIEWERS)"
    echo "   Admin bypass: $ADMIN_BYPASS"
}

main() {
    local command="${1:-setup}"

    log_info "🔒 Setting up branch protection rules for idle-world..."

    # Load environment file if it exists
    if [ -f ".github.env" ]; then
        log_info "Loading environment from .github.env"
        source .github.env
    fi

    check_dependencies

    case "$command" in
        setup)
            check_repository
            for branch in "${BRANCHES_TO_PROTECT[@]}"; do
                setup_branch_protection "$branch"
            done
            show_summary
            ;;
        verify)
            check_repository
            for branch in "${BRANCHES_TO_PROTECT[@]}"; do
                verify_protection "$branch"
            done
            ;;
        cleanup)
            for branch in "${BRANCHES_TO_PROTECT[@]}"; do
                cleanup_protection "$branch"
            done
            log_info "🎉 Branch protection cleanup completed!"
            ;;
        *)
            log_error "Unknown command: $command"
            echo "Available commands: setup, verify, cleanup"
            exit 1
            ;;
    esac
}

# Run main function with all arguments
main "$@"