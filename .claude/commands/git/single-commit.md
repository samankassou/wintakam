---
allowed-tools: Bash(git add:*), Bash(git status:*), Bash(git commit:*)
description: Create a git commit
---

## Context

- Current git status: !`git status`
- Current git diff (staged and unstaged changes): !`git diff HEAD`
- Current branch: !`git branch --show-current`
- Recent commits: !`git log --oneline -10`
- Add files to staged changes: !`git add .`

## Your task

Based on the above changes, create a single git commit.

## Best practices

Use Conventional Commits

### Examples

**Commit message with description and breaking change footer**

```markdown
feat: allow provided config object to extend other configs
BREAKING CHANGE: `extends` key in config file is now used for extending other config files
```

**Commit message with ! to draw attention to breaking change**

```markdown
feat!: send an email to the customer when a product is shipped
```

**Commit message with scope and ! to draw attention to breaking change**

```markdown
feat(api)!: send an email to the customer when a product is shipped
```

**Commit message with both ! and BREAKING CHANGE footer**

```markdown
chore!: drop support for Node 6
BREAKING CHANGE: use JavaScript features not available in Node 6.
```

**Commit message with no body**

```markdown
docs: correct spelling of CHANGELOG
```

**Commit message with scope**

```markdown
feat(lang): add Polish language
```

**Commit message with multi-paragraph body and multiple footers**

```markdown
fix: prevent racing of requests

Introduce a request id and a reference to latest request. Dismiss
incoming responses other than from latest request.

Remove timeouts which were used to mitigate the racing issue but are
obsolete now.

Reviewed-by: Z
Refs: #123
```

## Specification

Don't mention the section below int your commit messages unless expicitly told:

```markdown
🤖 Generated with [Claude Code](https://claude.ai/code)

Co-Authored-By: Claude <noreply@anthropic.com>
```
