# Realms of Idle

A multi-scenario idle RPG where players build interconnected fantasy enterprises—from cozy inns to sprawling empires—in a world where Classes, Levels, and the System govern all.

> **Inspired by LitRPG and progression fantasy**, _Realms of Idle_ rewards both deep focus and broad management across seven unique playstyles.

## Overview

Realms of Idle is an offline-first solo play game with scalable multiplayer support. Build your fantasy empire through idle mechanics, strategic decision-making, and meaningful progression.

### The Seven Scenarios

| Scenario             | Core Fantasy       | Playstyle           |
| -------------------- | ------------------ | ------------------- |
| **Inn/Tavern**       | Beloved innkeeper  | Social, cozy        |
| **Adventurer Guild** | Heroic coordinator | Strategic, combat   |
| **Monster Farm**     | Beast tamer        | Collection, nurture |
| **Alchemy**          | Master brewer      | Crafting, puzzle    |
| **Territory**        | Domain ruler       | Builder, strategy   |
| **Summoner**         | Planar contractor  | Collection, risk    |
| **Merchant Caravan** | Trade prince       | Economic, risk      |

## Technology Stack

### Core Architecture

- **Shared Game Core** - Portable Class Library running everywhere
- **Client** - .NET MAUI (Mobile/Desktop) + Blazor WASM (Web)
- **Server** - ASP.NET Core + Microsoft Orleans (Azure Container Apps, scale-to-zero)
- **Storage** - LiteDB (client) / Marten + PostgreSQL (server)
- **Testing** - xUnit, Reqnroll (BDD), TestContainers, Playwright, NBomber

### Key Design Decisions

- **Offline Solo Play** - Full game experience without network
- **Online Multiplayer** - Serverless-first, pay only when active
- **Shared Logic** - Same game code runs client and server
- **Event Sourcing** - Deterministic replay, conflict-free sync
- **LitRPG Authenticity** - System notifications, class evolution, skill trees

## Development

### Prerequisites

- .NET SDK (see `global.json` for version)
- Node.js 22+ (for tooling)
- Git
- GitHub account with repository admin access (for branch protection)

### Getting Started

```bash
# Clone the repository
git clone https://github.com/yourusername/realms-of-idle.git
cd realms-of-idle

# Restore dependencies
dotnet restore

# Run tests
npm test

# Run linting
npm run lint

# Format code
npm run format
```

### Development Workflow

This project follows strict development standards:

- **Git Hooks** - Never skip (--no-verify requires explicit permission)
- **Linear History** - Use rebase, then --ff-only for merges
- **Conventional Commits** - Follow commitlint configuration
- **Feature Branches** - Always use git worktrees for isolation
- **Branch Protection** - Automated CI/CD and PR review requirements

### Branch Protection Setup

To set up branch protection for the repository:

```bash
# Copy the environment configuration
cp .github.env.example .github.env

# Edit .github.env with your GitHub token
nano .github.env

# Install dependencies and setup branch protection
npm install
npm run branch:setup

# Verify the setup
npm run branch:verify
```

See [docs/BRANCH-PROTECTION.md](docs/BRANCH-PROTECTION.md) for detailed documentation.

- **TDD** - Tests first, then implementation
- **Quality Gates** - Zero warnings, zero errors, zero test failures

See [CLAUDE.md](CLAUDE.md) for complete development standards.

### Building

```bash
# Build all projects
dotnet build

# Run specific project
dotnet run --project src/IdleWorlds.Client.Maui

# Run with hot reload (development)
dotnet watch --project src/IdleWorlds.Client.Maui
```

### Testing

```bash
# Run all tests
dotnet test

# Run specific test project
dotnet test tests/IdleWorlds.Core.Tests

# Run with coverage
dotnet test --collect:"XPlat Code Coverage"

# Run performance benchmarks
dotnet run --project tests/IdleWorlds.Performance.Tests
```

## Project Structure

```
realms-of-idle/
├── docs/
│   └── design/           # Game design documents
├── src/
│   ├── IdleWorlds.Core/              # Shared game engine
│   ├── IdleWorlds.Client.Core/       # Shared client logic
│   ├── IdleWorlds.Client.Maui/       # MAUI application
│   ├── IdleWorlds.Server.Api/        # ASP.NET Core API
│   ├── IdleWorlds.Server.Orleans/    # Orleans grains
│   └── IdleWorlds.Shared/            # Client-server contracts
├── tests/
│   ├── IdleWorlds.Core.Tests/        # Unit + BDD tests
│   ├── IdleWorlds.Integration.Tests/ # Integration tests
│   ├── IdleWorlds.E2E.Tests/         # End-to-end tests
│   └── IdleWorlds.Performance.Tests/ # Performance benchmarks
└── deploy/
    ├── infrastructure/     # Infrastructure as Code
    └── docker/             # Container definitions
```

## Documentation

- [Game Overview](docs/design/idle-game-overview.md) - Complete game design summary
- [Tech Stack](docs/design/tech-stack.md) - Detailed technical architecture
- [Testing Strategy](docs/design/testing-strategy.md) - Testing approach and tools
- [NPC Design](docs/design/npc-design.md) - NPC AI and behavior systems
- [Scenario Designs](docs/design/) - Individual scenario specifications

## Contributing

We welcome contributions! Please:

1. Check existing issues and design docs
2. Follow the development workflow in [CLAUDE.md](CLAUDE.md)
3. Write tests first (TDD)
4. Ensure all tests pass before submitting PR
5. Use Conventional Commit messages

## License

[Specify your license here]

## Roadmap

### Launch Content

- 3 starting scenarios (Inn, Guild, Farm)
- Core progression through first Ascension
- Basic synergy system
- Introductory story

### Year 1 Updates

- Q1: Alchemy scenario
- Q2: Territory scenario + synergy expansion
- Q3: Summoner scenario
- Q4: Caravan scenario + Transcendence system

---

**Built with C# and .NET — offline-first, cloud-scalable, endlessly playable.**
