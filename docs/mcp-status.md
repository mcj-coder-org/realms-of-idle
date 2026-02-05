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
| **qmd** | ✅ Active | Hybrid semantic doc search (BM25 + vector + LLM reranking) |
| **code-context** | ✅ Active | Token-optimized codebase analysis via Tree-sitter |
| **web-search-prime** | ✅ Active | Z.ai web search via HTTP API |
| **zai-mcp-server** | ✅ Active | Z.ai AI assistant and search |
| **context7** | ✅ Active | Upstash library/tooling documentation lookup |

## Optional/Configured ⚠️

| Server | Status | Requirements |
|--------|--------|--------------|
| **context7** | ✅ Active | Upstash context management with API key |
| **brave-search** | 🔴 Disabled | Requires `BRAVE_API_KEY` from https://api.search.brave.com/register |

## Setup Instructions

### context7 (Upstash Documentation Lookup) ✅

**Status:** Configured and working with API key

Context7 provides access to current library and tooling documentation. It can fetch the latest docs for frameworks, libraries, and APIs to ensure AI assistance uses up-to-date information.

**Example usage:**
- "Show me the latest Orleans documentation for grains"
- "What's the current API for LiteDB in .NET 8?"
- "Get the latest documentation for ASP.NET Core Minimal APIs"

No additional setup required.

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

### QMD (Semantic Documentation Search) ✅

**Status:** Installed and working

**Location:** `~/.bun/bin/qmd`
**Version:** Latest from GitHub (installed via Bun)
**Index:** `~/.cache/qmd/index.sqlite` (4.0 MB, 52 files, 147 vectors)

**Collections:**
- `realms-docs`: 42 markdown documentation files (design docs, ADRs, plans, technical docs)
- `realms-src`: 45 C# source code files

**Features:**
- Hybrid search: BM25 + vector embeddings + LLM reranking
- GGUF models (auto-downloaded, ~2GB total):
  - Embedding: embeddinggemma-300M-Q8_0 (329MB)
  - Reranking: qwen3-reranker-0.6b-q8_0 (639MB)
  - Generation: Qwen3-0.6B-Q8_0 (downloads on first generation)

**Usage:**
```bash
# List collections
qmd collection list

# Search documentation (vector similarity)
qmd vsearch "idle progression mechanics" -c realms-docs -n 5

# Search source code
qmd vsearch "player state" -c realms-src -n 5

# Full-text search (BM25)
qmd search "IGameService" -c realms-src

# Combined search with reranking (slower but best results)
qmd query " Orleans grain lifecycle" -c realms-src

# Update index after code changes
qmd update

# Re-embed collections
qmd embed
```

**Integration:** Configured in `.mcp.json` - provides semantic documentation search to Claude Code via `qmd mcp`

### CodeContext (Codebase Analysis) ✅

**Status:** Installed and working

**Location:** `~/.nvm/versions/node/v24.13.0/bin/code-context-provider-mcp`
**Method:** Global npm install

**Features:**
- Token-optimized codebase context export
- Extracts directory structures and code symbols
- Uses WebAssembly Tree-sitter parsers for code analysis
- Supports multiple programming languages

**Usage:**
The MCP server is configured in `.mcp.json` and provides code context tools to Claude Code automatically. No manual interaction needed.

**Installation (if needed):**
```bash
npm install -g code-context-provider-mcp
```

## Configuration File

Main configuration: `.mcp.json`

## Testing MCP Servers

To test if MCP servers are working, try using them in Claude Code:

1. **Web Reader:** Ask Claude to fetch and summarize a URL
2. **Filesystem:** Ask Claude to list files in a directory
3. **Image Analysis:** Ask Claude to analyze an image
4. **grepai:** Ask Claude to semantically search code (e.g., "find code related to player health")
5. **QMD:** Ask Claude to semantically search documentation (e.g., "search docs for Orleans grain configuration")
6. **CodeContext:** Ask Claude to analyze codebase structure (e.g., "show me the architecture of the combat system")
7. **Z.ai Search:** Ask Claude to search the web for current information

## Notes

- **Working servers:** web-reader, filesystem, grepai, qmd, code-context, web-search-prime (Z.ai), zai-mcp-server (Z.ai), context7 (Upstash)
- The web-reader MCP provides web content fetching
- The filesystem MCP provides access to `/home/mcjarvis/projects`
- grepai provides semantic code search using Ollama embeddings
- qmd provides hybrid semantic documentation search (BM25 + vector + LLM reranking)
- code-context provides token-optimized codebase analysis via Tree-sitter
- Z.ai servers provide enhanced web search and AI assistance
- context7 provides current library and tooling documentation lookup
- For Windows/PowerShell environment, see `scripts/setup-mcp.ps1`
