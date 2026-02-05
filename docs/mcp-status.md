# MCP Server Status - Realms of Idle

**Last Updated:** 2026-02-05
**Environment:** WSL2/Linux

## Currently Working ✅

| Server | Status | Notes |
|--------|--------|-------|
| **web-reader** | ✅ Active | Web content fetching via @executeautomation/web-reader-mcp-server |
| **filesystem** | ✅ Available | Filesystem access via @modelcontextprotocol/server-filesystem |
| **image-analysis** | ✅ Available | 4.5v MCP image analysis |

## Optional/Configured ⚠️

| Server | Status | Requirements |
|--------|--------|--------------|
| **context7** | 🔴 Disabled | Requires `UPSTASH_REDIS_URL` environment variable |
| **brave-search** | 🔴 Disabled | Requires `BRAVE_API_KEY` from https://api.search.brave.com/register |
| **grepai** | 🔴 Not Installed | See setup instructions below |

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

### Enable grepai (Semantic Code Search)

**Prerequisites:**
- Go compiler
- Ollama with nomic-embed-text model

**Installation:**
```bash
# Install Go (if not installed)
sudo apt update
sudo apt install golang

# Install grepai
go install github.com/yoanbernabeu/grepai@latest

# Install Ollama (Linux)
curl -fsSL https://ollama.ai/install.sh | sh

# Pull the embedding model
ollama pull nomic-embed-text

# Build semantic index for the project
cd /home/mcjarvis/projects/realms-of-idle
~/go/bin/grepai index

# Enable in .mcp.json
# Set command to: "/home/mcjarvis/go/bin/grepai"
# Set args to: ["mcp"]
# Set "disabled": false
```

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
