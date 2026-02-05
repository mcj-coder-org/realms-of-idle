# MCP Server Status - Realms of Idle

**Last Updated:** 2026-02-05
**Environment:** WSL2/Linux

## Currently Working ✅

| Server | Status | Notes |
|--------|--------|-------|
| **web-reader** | ✅ Active | Web content fetching via @executeautomation/web-reader-mcp-server |
| **filesystem** | ✅ Available | Filesystem access via @modelcontextprotocol/server-filesystem |
| **image-analysis** | ✅ Available | 4.5v MCP image analysis |
| **grepai** | ✅ Active | Semantic code search via Ollama nomic-embed-text model |

## Optional/Configured ⚠️

| Server | Status | Requirements |
|--------|--------|--------------|
| **context7** | 🔴 Disabled | Requires `UPSTASH_REDIS_URL` environment variable |
| **brave-search** | 🔴 Disabled | Requires `BRAVE_API_KEY` from https://api.search.brave.com/register |
| **grepai** | ✅ Active | Installed at `.claude/grepai`, using Ollama nomic-embed-text |

## Setup Instructions

### Enable context7 (Upstash Context Management)

1. Create Upstash Redis database at https://upstash.com
2. Set environment variable:
   ```bash
   export UPSTASH_REDIS_URL="your_redis_url_here"
   ```
3. Enable in `.mcp.json` by setting `"disabled": false`

### Enable brave-search

1. Get API key from https://api.search.brave.com/register
2. Set in `.mcp.json`:
   ```json
   "env": {
     "BRAVE_API_KEY": "your_actual_api_key"
   }
   ```
3. Set `"disabled": false`

### grepai (Semantic Code Search) ✅

**Status:** Installed and working

**Location:** `.claude/grepai`
**Model:** Ollama nomic-embed-text (274MB)
**Index:** `.grepai/index.gob` (108 files indexed)

**Usage:**
```bash
# Semantic search
.claude/grepai search "your query here"

# Trace symbol callers/callees
.claude/grepai trace SymbolName

# Rebuild index (after code changes)
.claude/grepai watch
```

**To update index:** The grepai daemon runs in background and auto-updates when files change.

## Configuration File

Main configuration: `.mcp.json`

## Testing MCP Servers

To test if MCP servers are working, try using them in Claude Code:

1. **Web Reader:** Ask Claude to fetch and summarize a URL
2. **Filesystem:** Ask Claude to list files in a directory
3. **Image Analysis:** Ask Claude to analyze an image

## Notes

- The web-reader MCP is pre-configured and working
- The filesystem MCP provides access to `/home/mcjarvis/projects`
- Additional MCP servers can be added to `.mcp.json` as needed
- For Windows/PowerShell environment, see `scripts/setup-mcp.ps1`
