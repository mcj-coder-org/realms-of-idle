# Repository Templates

This directory contains templates and scripts to help standardize development workflows for the Idle World project.

## 📁 Files

### Scripts

- **`create-feature-branch.sh`** - Creates a new feature branch following project conventions
- **`gitconfig-feature`** - Git configuration template for feature branches
- **`commit_template.txt`** - Commit message template

### Templates

- **`pull_request_template.md`** - Template for pull request descriptions
- **`documentation-template.md`** - Template for creating new documentation files
- **`bug_report.yml`** - GitHub issue template for bug reports
- **`feature_request.yml`** - GitHub issue template for feature requests

### GitHub Actions

- **`auto-merge.yml`** - Auto-merge workflow for maintainer account

## 🚀 Usage

### Creating a Feature Branch

1. Make sure you're on the main branch and it's up to date:

   ```bash
   git checkout main
   git pull origin main
   ```

2. Run the feature branch creation script:

   ```bash
   .github/templates/create-feature-branch.sh <issue-number> <branch-description>
   ```

   Example:

   ```bash
   .github/templates/create-feature-branch.sh 42 add-inventory-system
   ```

This will:

- Create a worktree for the new branch
- Switch to the feature branch
- Configure git with contributor account settings
- Open the GitHub issue in your browser

### Creating New Documentation

1. Copy the documentation template:

   ```bash
   cp docs/templates/documentation-template.md path/to/new-document.md
   ```

2. Edit the frontmatter and content
3. Follow the structure provided in the template

### Creating Issues

GitHub will automatically provide the issue templates when creating new issues. The templates include:

- Bug Report - For technical issues and defects
- Feature Request - For new ideas and enhancements

## 📋 Conventions

### Branch Naming

- Format: `feat/{issue-number}-{description}`
- Example: `feat/42-add-inventory-system`

### Commit Messages

- Format: `type(scope): description #issue-number`
- Example: `feat(inventory): add item storage system #42`

### PR Description

Use the pull request template to include:

- Description of changes
- Type of change
- Issue reference
- Testing instructions
- Checklist

## 🔧 Configuration

The templates are configured to work with the project's dual-account workflow:

- **Contributor Account**: `mcj-codificer` / `m.c.j@live.co.uk`
- **Maintainer Account**: `mcj-coder` / `martin.cjarvis@googlemail.com`

The feature branch script automatically sets the correct git configuration for contributor accounts when creating new branches.
