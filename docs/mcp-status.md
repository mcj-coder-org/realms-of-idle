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
| **web-search-prime** | ✅ Active | Z.ai web search via HTTP API |
| **zai-mcp-server** | ✅ Active | Z.ai AI assistant and search |

## Optional/Configured ⚠️

| Server | Status | Requirements |
|--------|--------|--------------|
| **context7** | ⚠️ Configured | Requires `UPSTASH_REDIS_URL` environment variable |
| **brave-search** | 🔴 Disabled | Requires `BRAVE_API_KEY` from https://api.search.brave.com/register |

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

### Z.ai Servers ✅

**Status:** Configured and working

**Servers:**
- **web-search-prime:** HTTP-based web search via Z.ai API
- **zai-mcp-server:** AI assistant and intelligent search

**Configuration:** API keys configured in `.mcp.json`

**Usage:**
These servers provide enhanced web search and AI-assisted capabilities through Claude Code. No additional setup required.

## Configuration File

Main configuration: `.mcp.json`

## Testing MCP Servers

To test if MCP servers are working, try using them in Claude Code:

1. **Web Reader:** Ask Claude to fetch and summarize a URL
2. **Filesystem:** Ask Claude to list files in a directory
3. **Image Analysis:** Ask Claude to analyze an image
4. **grepai:** Ask Claude to semantically search code (e.g., "find code related to player health")
5. **Z.ai Search:** Ask Claude to search the web for current information

## Notes

- **Working servers:** web-reader, filesystem, grepai, web-search-prime (Z.ai), zai-mcp-server (Z.ai)
- The web-reader MCP provides web content fetching
- The filesystem MCP provides access to `/home/mcjarvis/projects`
- grepai provides semantic code search using Ollama embeddings
- Z.ai servers provide enhanced web search and AI assistance
- context7 requires UPSTASH_REDIS_URL environment variable to activate
- For Windows/PowerShell environment, see `scripts/setup-mcp.ps1`
