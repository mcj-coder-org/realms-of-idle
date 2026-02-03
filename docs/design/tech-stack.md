---
type: technical
scope: high-level
status: review
version: 1.0.0
created: 2026-02-02
updated: 2026-02-02
subjects: [dotnet, technology, architecture]
dependencies: []
---

# .NET Technology Stack Recommendation

## Offline-First Solo Play + Scalable Multiplayer

## 10. Asset Pipeline

### 10.1 Graphical Asset Generation

```
ASSET GENERATION WORKFLOW
━━━━━━━━━━━━━━━━━━━━━━━━━

┌─────────────────────┐
│    Pixellab.ai      │  ◄── AI-generated game art
│   (Subscription)    │
├─────────────────────┤
│ • Character sprites │
│ • NPC portraits     │
│ • Item icons        │
│ • Environment tiles │
│ • UI elements       │
│ • Skill/spell FX    │
└──────────┬──────────┘
           │
           ▼
┌─────────────────────┐
│   Post-Processing   │
├─────────────────────┤
│ • Consistency pass  │
│ • Color palette     │
│ • Resolution export │
│ • Animation sheets  │
└──────────┬──────────┘
           │
           ▼
┌─────────────────────┐
│   Asset Repository  │
├─────────────────────┤
│ • Version control   │
│ • Tagging/metadata  │
│ • Multiple sizes    │
│ • Platform variants │
└─────────────────────┘
```

### 10.2 Asset Categories

| Category                   | Count Estimate | Pixellab.ai Use                         |
| -------------------------- | -------------- | --------------------------------------- |
| Character sprites (player) | 50-100         | Base generation, customization variants |
| NPC portraits              | 200-500        | Procedural generation for variety       |
| Class icons                | 50-80          | Consistent style badges                 |
| Skill icons                | 200-400        | Per-skill unique icons                  |
| Item icons                 | 500-1000       | Weapons, armor, consumables, materials  |
| Environment tiles          | 100-200        | Zone-specific tilesets                  |
| UI elements                | 50-100         | Buttons, frames, backgrounds            |
| Spell/action effects       | 50-100         | Animated VFX sprites                    |

### 10.3 Art Style: Cozy Fantasy

The visual identity is **Cozy Fantasy** — warm, inviting, and approachable while maintaining the depth and variety of a living world.

```
COZY FANTASY STYLE GUIDE
━━━━━━━━━━━━━━━━━━━━━━━━

MOOD
├─ Warm and inviting, not grimdark
├─ Sense of home, comfort, community
├─ Wonder without menace (mostly)
├─ Storybook quality with depth

COLOR PALETTE
├─ Warm earth tones (browns, ambers, ochres)
├─ Soft greens and teals
├─ Warm lighting (candlelight, hearth, sunset)
├─ Muted rather than saturated
├─ Accent colors for magic/rare items
└─ Seasonal variations per zone

CHARACTER STYLE
├─ Approachable proportions (not hyper-realistic)
├─ Expressive faces, readable emotions
├─ Variety in body types, ages, species
├─ Clothing shows profession/class clearly
├─ NPCs look like they belong in their role
└─ Heroes/Villains more dramatic but still grounded

ENVIRONMENT STYLE
├─ Lived-in spaces (clutter, wear, personality)
├─ Cozy interiors (inns, shops, homes)
├─ Nature is lush, not threatening
├─ Weather adds atmosphere, not menace
├─ Architecture varies by region/culture
└─ Day/night cycle affects mood

UI STYLE
├─ Parchment/leather textures
├─ Hand-drawn quality to frames
├─ Readable but characterful fonts
├─ Icons clear at small sizes
├─ Warm background tones
└─ Subtle animations (flickering, breathing)
```

**Pixellab.ai Prompt Framework:**

```
Base prompt elements:
- "cozy fantasy style"
- "warm lighting"
- "storybook illustration"
- "soft colors"
- "[specific subject]"
- "game asset, clean edges"

Example prompts:
- "cozy fantasy style, warm tavern interior, hearth fire,
   wooden furniture, storybook illustration, game background"
- "cozy fantasy style, friendly blacksmith NPC, leather apron,
   warm smile, soft lighting, character portrait, game asset"
- "cozy fantasy style, iron sword icon, warm metal tones,
   clean edges, game item icon, transparent background"
```

**Style Consistency Rules:**

1. All assets generated with "cozy fantasy" base prompt
2. Reference sheet of approved examples for each category
3. Color palette hex codes defined and enforced
4. Post-processing pass to harmonize batches
5. Style guide document for any manual touch-ups

### 10.4 Asset Integration

```
MAUI ASSET HANDLING
━━━━━━━━━━━━━━━━━━━

Resources/
├── Images/
│   ├── Characters/
│   │   ├── player_warrior.png
│   │   ├── player_warrior@2x.png    # Retina
│   │   └── player_warrior@3x.png    # High DPI
│   ├── Npcs/
│   ├── Items/
│   ├── Skills/
│   └── UI/
├── Fonts/
└── Raw/                              # Platform-specific

Auto-scaling handled by MAUI resource system.
SVG supported for resolution-independent UI elements.
```

---

## 1. Executive Summary

### Core Architecture Principle

```
┌─────────────────────────────────────────────────────────────────┐
│                    SHARED GAME CORE                             │
│         (Portable Class Library - runs EVERYWHERE)              │
├─────────────────────────────────────────────────────────────────┤
│  Domain Models │ Tag System │ Action Engine │ NPC AI            │
│  Goal System │ Class/Skill Logic │ World Simulation             │
│  Event Sourcing │ Deterministic RNG │ Offline Calculator        │
└─────────────────────────────────────────────────────────────────┘
          │                                       │
          ▼                                       ▼
┌─────────────────────────┐         ┌─────────────────────────────┐
│      SOLO MODE          │         │      MULTIPLAYER MODE       │
│   (100% Offline)        │         │                             │
├─────────────────────────┤         ├─────────────────────────────┤
│ .NET MAUI App           │         │ ┌─────────────────────────┐ │
│ Local LiteDB            │         │ │  .NET ASPIRE (Local)     │ │
│ Embedded Game Engine    │         │ │  Development Option    │ │
│ Local NPC Simulation    │         │ ├─────────────────────────┤ │
│ No network required     │         │ │ Full stack locally      │ │
│                         │         │ │ Orleans + PostgreSQL   │ │
│ Full game experience    │         │ │ Redis + SignalR         │ │
└─────────────────────────┘         │ │ No cloud required       │ │
                                     │ └─────────────────────────┘ │
                                     │              ▲                │
                                     │              │  OR            │
                                     │              ▼                │
                                     │ ┌─────────────────────────┐ │
                                     │ │  AZURE (Production)     │ │
                                     │ ├─────────────────────────┤ │
                                     │ │ Container Apps (scale)  │ │
                                     │ │ Managed infrastructure  │ │
                                     │ └─────────────────────────┘ │
                                     └─────────────────────────────┘
```

### Key Design Decisions

| Requirement        | Solution                        | Rationale                               |
| ------------------ | ------------------------------- | --------------------------------------- |
| Offline Solo       | Embedded game engine            | Same code runs client-side              |
| Online Multiplayer | Orleans on Azure Container Apps | Scale-to-zero, pay per use              |
| Shared Logic       | .NET Standard library           | Runs on client AND server               |
| Local Storage      | LiteDB (embedded)               | NoSQL document DB, zero config          |
| Sync Protocol      | Event sourcing + vector clocks  | Conflict-free merge on reconnect        |
| Cheap Scaling      | Serverless-first                | Only pay when players are active        |
| Testability        | Deterministic core              | Same tests run against local and server |

---

## 2. Architecture Layers

### 2.1 Layer Overview

```
┌─────────────────────────────────────────────────────────────────┐
│                      PRESENTATION LAYER                         │
├─────────────────────────────────────────────────────────────────┤
│  .NET MAUI (Mobile/Desktop)    │    Blazor WASM (Web)          │
│  - Native UI                   │    - Browser play              │
│  - Platform services           │    - Optional client           │
└─────────────────────────────────────────────────────────────────┘
                                │
                    IGameService interface
                                │
┌───────────────────────────────┴─────────────────────────────────┐
│                      APPLICATION LAYER                          │
├─────────────────────────────────────────────────────────────────┤
│  LocalGameService              │    MultiplayerGameService      │
│  (Solo Mode)                   │    (Online Mode)               │
│  - Hosts GameEngine locally    │    - Connects to server        │
│  - Manages local persistence   │    - SignalR real-time         │
│  - NPC simulation on device    │    - Handles sync              │
└─────────────────────────────────────────────────────────────────┘
                                │
                    GameEngine (shared)
                                │
┌─────────────────────────────────────────────────────────────────┐
│                       DOMAIN LAYER                              │
│                    (IdleWorlds.Core)                            │
├─────────────────────────────────────────────────────────────────┤
│  ┌─────────────┐ ┌─────────────┐ ┌─────────────┐               │
│  │   Domain    │ │   Engine    │ │   Events    │               │
│  │   Models    │ │  Services   │ │  (Sourcing) │               │
│  ├─────────────┤ ├─────────────┤ ├─────────────┤               │
│  │ Player      │ │ ActionProc  │ │ PlayerEvts  │               │
│  │ NPC         │ │ NpcSimultr  │ │ NpcEvents   │               │
│  │ Classes     │ │ GoalEval    │ │ WorldEvents │               │
│  │ Skills      │ │ ClassEval   │ │             │               │
│  │ Tags        │ │ SkillAcq    │ │             │               │
│  │ Goals       │ │ OfflineCalc │ │             │               │
│  │ Actions     │ │ WorldSim    │ │             │               │
│  └─────────────┘ └─────────────┘ └─────────────┘               │
└─────────────────────────────────────────────────────────────────┘
                                │
                    IEventStore / IReadModelStore
                                │
┌───────────────────────────────┴─────────────────────────────────┐
│                    PERSISTENCE LAYER                            │
├─────────────────────────────────────────────────────────────────┤
│  LiteDB (Client)               │    Marten/PostgreSQL (Server) │
│  - Embedded in app             │    - Cloud hosted              │
│  - Event store                 │    - Event store               │
│  - Read models                 │    - Read models               │
│  - Sync checkpoints            │    - Projections               │
└─────────────────────────────────────────────────────────────────┘
```

### 2.2 Shared Core Benefits

The `IdleWorlds.Core` library is the heart of the system:

| Benefit                    | Description                                         |
| -------------------------- | --------------------------------------------------- |
| **Single Source of Truth** | Game rules defined once, run everywhere             |
| **Offline Parity**         | Solo players get the exact same game as multiplayer |
| **Testability**            | Test core logic without network/UI dependencies     |
| **Determinism**            | Seeded RNG ensures reproducible outcomes            |
| **Sync Verification**      | Client and server can verify state independently    |

---

## 3. Solo Mode Architecture

### 3.1 Component Overview

```
┌─────────────────────────────────────────────────────────────────┐
│                    MOBILE DEVICE                                │
├─────────────────────────────────────────────────────────────────┤
│                                                                 │
│   ┌─────────────────────────────────────────────────────────┐  │
│   │                    .NET MAUI App                        │  │
│   ├─────────────────────────────────────────────────────────┤  │
│   │                                                         │  │
│   │   ┌─────────────┐    ┌────────────────────────────┐    │  │
│   │   │     UI      │    │     LocalGameService       │    │  │
│   │   │   (Views)   │◄──►│                            │    │  │
│   │   └─────────────┘    │  ┌──────────────────────┐  │    │  │
│   │                      │  │     GameEngine       │  │    │  │
│   │                      │  │    (embedded)        │  │    │  │
│   │                      │  └──────────────────────┘  │    │  │
│   │                      │                            │    │  │
│   │                      │  ┌──────────────────────┐  │    │  │
│   │                      │  │   NPC Simulation     │  │    │  │
│   │                      │  │   (background)       │  │    │  │
│   │                      │  └──────────────────────┘  │    │  │
│   │                      └────────────────────────────┘    │  │
│   │                                   │                     │  │
│   │                      ┌────────────▼────────────┐       │  │
│   │                      │        LiteDB           │       │  │
│   │                      │  ┌──────┐ ┌──────────┐  │       │  │
│   │                      │  │Events│ │ReadModels│  │       │  │
│   │                      │  └──────┘ └──────────┘  │       │  │
│   │                      └─────────────────────────┘       │  │
│   │                                                         │  │
│   └─────────────────────────────────────────────────────────┘  │
│                                                                 │
│   NO INTERNET REQUIRED                                          │
│                                                                 │
└─────────────────────────────────────────────────────────────────┘
```

### 3.2 Key Characteristics

| Aspect               | Approach                                    |
| -------------------- | ------------------------------------------- |
| **Storage**          | LiteDB embedded NoSQL database              |
| **Simulation**       | Background timer runs NPC ticks             |
| **NPC Scale**        | Tiered simulation based on player proximity |
| **Offline Progress** | Calculated on app resume using event replay |
| **Save Games**       | Multiple save slots supported               |
| **Performance**      | Optimized for mobile CPU/battery            |

### 3.3 NPC Simulation Tiers (Solo)

```
Solo mode uses simplified tiers to manage device resources:

TIER 1 - FULL (Same zone as player)
├─ Full action resolution
├─ Real-time goal evaluation
└─ ~50-100 NPCs max

TIER 2 - SIMPLIFIED (Adjacent zones)
├─ Aggregated daily outcomes
├─ Goal progress calculated statistically
└─ ~500 NPCs

TIER 3 - BACKGROUND (Distant zones)
├─ Population-level simulation only
├─ Economic outputs calculated in batch
└─ Unlimited NPCs (data only, not active)
```

---

## 4. Multiplayer Mode Architecture

### 4.1 Scale-to-Zero Cloud Architecture

```
┌─────────────────────────────────────────────────────────────────┐
│                    AZURE CONTAINER APPS                         │
│                   (Serverless Containers)                       │
├─────────────────────────────────────────────────────────────────┤
│                                                                 │
│   ┌─────────────────┐         ┌─────────────────────────────┐  │
│   │   API Gateway   │         │      Orleans Silo Cluster   │  │
│   │   (min: 0)      │────────▶│      (min: 0, max: N)       │  │
│   │                 │         │                             │  │
│   │ • REST API      │         │  • PlayerGrain (1:1)        │  │
│   │ • SignalR Hub   │         │  • NpcGrain (1:1)           │  │
│   │ • Auth/Rate Lmt │         │  • ZoneGrain (1:zone)       │  │
│   │                 │         │  • WorldEventGrain          │  │
│   └─────────────────┘         │  • EconomyGrain             │  │
│                               └─────────────────────────────┘  │
│                                            │                    │
└────────────────────────────────────────────│────────────────────┘
                                             │
              ┌──────────────────────────────┼──────────────────────────────┐
              │                              │                              │
  ┌───────────▼───────────┐    ┌─────────────▼─────────────┐    ┌─────────▼─────────┐
  │  Azure PostgreSQL     │    │       Azure Redis         │    │   Blob Storage    │
  │  Flexible Server      │    │       (Basic tier)        │    │                   │
  │  (Burstable B1ms)     │    │                           │    │  • Static assets  │
  │                       │    │  • Orleans clustering     │    │  • Backups        │
  │  • Event store        │    │  • Grain state cache      │    │  • World seeds    │
  │  • Read models        │    │  • Pub/Sub                │    │                   │
  │  • Marten projections │    │  • Session state          │    │                   │
  └───────────────────────┘    └───────────────────────────┘    └───────────────────┘
```

### 4.2 Why Orleans?

| NPC System Need                | Orleans Solution                             |
| ------------------------------ | -------------------------------------------- |
| Each NPC is independent        | Each NPC is a Grain (actor)                  |
| NPCs have isolated state       | Grains have isolated state                   |
| NPCs activate/deactivate       | Grains auto-activate/deactivate              |
| NPCs need timers (daily loops) | Grains have built-in timers/reminders        |
| Scale horizontally             | Grains distribute across silos automatically |
| Persist state efficiently      | Grain persistence is built-in                |

### 4.3 Scaling Behavior

```
SCALE-TO-ZERO FLOW
━━━━━━━━━━━━━━━━━━

[0 players online]
    │
    ▼
┌─────────────────────────┐
│ All containers stopped  │
│ Only DB running (sleep) │
│ Cost: ~$15/month        │
└─────────────────────────┘
    │
    │ Player connects
    ▼
┌─────────────────────────┐
│ Cold start (~10-15 sec) │
│ 1 API + 1 Silo spin up  │
│ Player grain activates  │
└─────────────────────────┘
    │
    │ More players join
    ▼
┌─────────────────────────┐
│ Horizontal scale-out    │
│ More silos added        │
│ Grains redistributed    │
└─────────────────────────┘
    │
    │ Players leave
    ▼
┌─────────────────────────┐
│ Grains deactivate       │
│ Silos scale down        │
│ Eventually back to 0    │
└─────────────────────────┘
```

### 4.4 Cost Estimates

| Active Players | Monthly Cost | Notes                    |
| -------------- | ------------ | ------------------------ |
| 0              | ~$15         | DB minimum only          |
| 100            | ~$50         | 1-2 containers part-time |
| 1,000          | ~$150        | 2-3 containers           |
| 10,000         | ~$500        | 5-10 containers          |
| 100,000        | ~$3,000      | Full scale-out           |

**Comparison:** Traditional always-on VMs would cost $200-500/month minimum even with 0 players.

### 4.5 Local Development with .NET Aspire

For development, testing, and self-hosted scenarios, .NET Aspire provides local orchestration of the full multiplayer stack without requiring cloud resources.

```
.NET ASPIRE LOCAL HOSTING
━━━━━━━━━━━━━━━━━━━━━━━━

┌─────────────────────────────────────────────────────────────────┐
│                    DEVELOPER WORKSTATION                         │
├─────────────────────────────────────────────────────────────────┤
│                                                                 │
│   ┌─────────────────────────────────────────────────────────┐  │
│   │              .NET ASPIRE DASHBOARD                       │  │
│   │  (http://localhost:5000)                                 │  │
│   ├─────────────────────────────────────────────────────────┤  │
│   │  ┌─────────────┐  ┌─────────────┐  ┌───────────────┐  │  │
│   │  │ API Project │  │ Orleans     │  │ PostgreSQL    │  │  │
│   │  │ (Frontend)  │  │ Silos       │  │ (Container)   │  │  │
│   │  │             │  │             │  │               │  │  │
│   │  │ • Endpoints │  │ • 4 Silos    │  │ • Port 5432   │  │  │
│   │  │ • SignalR   │  │ • Auto-scale│  │ • Persisted   │  │  │
│   │  │ • Health    │  │ • Dashboard  │  │               │  │  │
│   │  │             │  │             │  │  ┌───────────┐ │  │  │
│   │  │ Running:    │  │ Running:    │  │  │  Redis    │ │  │  │
│   │  │ ✓ Healthy   │  │ ✓ Healthy   │  │  │ (Container)│ │  │  │
│   │  └─────────────┘  └─────────────┘  │  │            │ │  │  │
│   │                                  │  │ • Port 6379│ │  │  │
│   │  ┌─────────────────────────────┐  │  │ • Clustering│ │  │  │
│   │  │         LOGS                │  │  └────────────┘ │  │  │
│   │  │  • Structured console output │  │                 │  │  │
│   │  │  • Distributed tracing      │  │                 │  │  │
│   │  │  • Metrics & telemetry      │  │                 │  │  │
│   │  └─────────────────────────────┘  │                 │  │  │
│   │                                  │                 │  │  │
│   │  [Resources]                      [Resources]       │  │
│   │  CPU: 8%   Memory: 2.1GB          CPU: 12%  Mem: 1.8GB│  │
│   └─────────────────────────────────────────────────────────┘  │
│                                                                 │
│   ALL SERVICES RUN LOCALLY VIA DOCKER CONTAINERS                  │
│   NO CLOUD CONNECTION REQUIRED                                   │
│                                                                 │
└─────────────────────────────────────────────────────────────────┘
```

#### Aspire Project Structure

```yaml
# IdleWorlds.AppHost/AspireApp.Host.csproj
{
  'Projects':
    [
      {
        'Name': 'IdleWorlds.Server.Api',
        'Type': 'project',
        'Path': '../IdleWorlds.Server.Api/IdleWorlds.Server.Api.csproj',
        'Args': ['--urls', 'https://localhost:7001'],
      },
      {
        'Name': 'IdleWorlds.Server.Orleans',
        'Type': 'project',
        'Path': '../IdleWorlds.Server.Orleans/IdleWorlds.Server.Orleans.csproj',
        'Args': ['--silo', 'primary'],
        'Environment': { 'ASPNETCORE_ENVIRONMENT': 'Development', 'ORLEANS_SILO_PRIMARY': 'true' },
      },
      {
        'Name': 'postgres',
        'Type': 'container',
        'Image': 'postgres:16-alpine',
        'Environment': { 'POSTGRES_PASSWORD': '{postgres.password}', 'POSTGRES_DB': 'idleworlds' },
        'Bindings': { 'tcp': { 'Port': 5432, 'HostPort': 5432 } },
        'Endpoints': [{ 'Name': 'tcp', 'TargetPort': 5432 }],
      },
      {
        'Name': 'redis',
        'Type': 'container',
        'Image': 'redis:7-alpine',
        'Bindings': { 'tcp': { 'Port': 6379, 'HostPort': 6379 } },
      },
    ],
}
```

#### Aspire Benefits

| Benefit                   | Description                                                 |
| ------------------------- | ----------------------------------------------------------- |
| **Full Stack Local**      | Entire multiplayer ecosystem runs locally with `dotnet run` |
| **Zero Cloud Cost**       | Development needs no Azure subscription                     |
| **Production Parity**     | Same Orleans, PostgreSQL, Redis stack as production         |
| **Live Reload**           | Code changes hot-reload without restarting containers       |
| **Resource Dashboard**    | Built-in monitoring of CPU, memory, logs                    |
| **Distributed Tracing**   | OpenTelemetry integration for debugging                     |
| **Environment Variables** | Config per dev/staging/production profiles                  |

#### Development Workflow

```bash
# Start entire stack with one command
dotnet run --project src/IdleWorlds.AppHost

# Aspire automatically:
# 1. Spins up PostgreSQL container
# 2. Spins up Redis container
# 3. Starts API project
# 4. Starts Orleans silos (4 silos by default)
# 5. Opens dashboard at http://localhost:5000
# 6. Applies environment variables and configuration
# 7. Enables hot reload for all projects

# Develop with full multiplayer experience
# - Multiple clients connect to localhost:7001
# - Real-time SignalR works locally
# - Orleans grains activate on local silos
# - Database persists in container (restart-safe)

# Stop everything with Ctrl+C
# Containers stop, but database persists between runs
```

#### Self-Hosting with Aspire

Aspire orchestration enables self-hosting scenarios beyond development:

| Scenario                | Configuration                                                              |
| ----------------------- | -------------------------------------------------------------------------- |
| **LAN Party**           | Aspire dashboard accessible on local network, clients connect via local IP |
| **Private Server**      | Deploy Aspire AppHost to VPS, same stack as local dev                      |
| **Testing Environment** | Spin up identical environment for integration testing                      |
| **Cost Optimization**   | Run on existing hardware instead of paying for cloud                       |

#### Transition to Cloud

Moving from Aspire local to Azure production is straightforward:

```
ASPIRE (Local)                    AZURE (Production)
    │                                    │
    ▼                                    ▼
┌─────────────┐                  ┌──────────────┐
│ PostgreSQL  │                  │ Azure        │
│ (Container) │    deploy →     │ PostgreSQL   │
│             │                  │ Flexible     │
└─────────────┘                  └──────────────┘
    │                                    │
    ▼                                    ▼
┌─────────────┐                  ┌──────────────┐
│ Redis       │                  │ Azure        │
│ (Container) │    deploy →     │ Cache for    │
│             │                  │ Redis        │
└─────────────┘                  └──────────────┘
    │                                    │
    ▼                                    ▼
┌─────────────┐                  ┌──────────────┐
│ Orleans     │    deploy →     │ Azure        │
│ Silos       │                  │ Container    │
│ (Process)   │                  │ Apps         │
└─────────────┘                  └──────────────┘

NO CODE CHANGES REQUIRED — CONNECTION STRINGS UPDATED VIA CONFIGURATION
```

---

## 5. Sync Architecture

### 5.1 Solo ↔ Multiplayer Sync

```
┌──────────────────┐                      ┌──────────────────┐
│   SOLO CLIENT    │                      │   MULTIPLAYER    │
│                  │                      │     SERVER       │
├──────────────────┤                      ├──────────────────┤
│                  │                      │                  │
│  Event Store     │    SYNC PROTOCOL     │  Event Store     │
│  ┌────────────┐  │                      │  ┌────────────┐  │
│  │ Event 1    │  │  ┌──────────────┐   │  │ Event 1    │  │
│  │ Event 2    │  │  │              │   │  │ Event 2    │  │
│  │ Event 3    │──┼─▶│  Compare     │◀──┼──│ Event 3    │  │
│  │ Event 4*   │  │  │  Checkpoints │   │  │ Event 4'   │  │
│  │ Event 5*   │  │  │              │   │  │            │  │
│  └────────────┘  │  └──────┬───────┘   │  └────────────┘  │
│                  │         │            │                  │
│  Checkpoint:     │         ▼            │  Checkpoint:     │
│  Version: 3      │  ┌──────────────┐   │  Version: 4      │
│  Hash: abc123    │  │   Resolve    │   │  Hash: def456    │
│                  │  │   Strategy   │   │                  │
└──────────────────┘  └──────────────┘   └──────────────────┘
                             │
              ┌──────────────┼──────────────┐
              ▼              ▼              ▼
        ┌─────────┐   ┌───────────┐   ┌─────────┐
        │ Server  │   │  Client   │   │  Manual │
        │  Wins   │   │   Wins    │   │  Merge  │
        └─────────┘   └───────────┘   └─────────┘
```

### 5.2 Sync Scenarios

| Scenario                        | Resolution                            |
| ------------------------------- | ------------------------------------- |
| Solo-only play                  | No sync needed                        |
| First multiplayer connect       | Upload all local events               |
| Regular sync                    | Exchange events since last checkpoint |
| Conflict (same entity modified) | Player chooses or server wins         |
| Offline period in multiplayer   | Replay missed server events           |

### 5.3 Deterministic Verification

Because the game engine uses seeded RNG:

- Client can replay server events and verify outcomes match
- Server can verify client-submitted actions are legitimate
- Cheating detection through outcome verification

---

## 6. Technology Choices

### 6.1 Client Technologies

| Component            | Technology            | Rationale                                     |
| -------------------- | --------------------- | --------------------------------------------- |
| **Framework**        | .NET MAUI             | Cross-platform, native performance, shared C# |
| **Local DB**         | LiteDB                | Embedded NoSQL, zero config, good perf        |
| **State Management** | CommunityToolkit.Mvvm | Standard MVVM, reactive, testable             |
| **HTTP Client**      | Refit                 | Type-safe REST, less boilerplate              |
| **Resilience**       | Polly                 | Retry, circuit breaker for network            |
| **Graphics**         | SkiaSharp (if needed) | Custom rendering for game visuals             |
| **Web Client**       | Blazor WASM           | Optional browser play, same C# code           |

### 6.2 Server Technologies

| Component                | Technology                | Rationale                                |
| ------------------------ | ------------------------- | ---------------------------------------- |
| **Actor Framework**      | Microsoft Orleans         | Perfect fit for NPC actors, proven scale |
| **API**                  | ASP.NET Core Minimal APIs | Lightweight, fast, good DX               |
| **Real-time**            | SignalR                   | Built-in .NET, works with Orleans        |
| **Event Store**          | Marten (on PostgreSQL)    | .NET-native event sourcing, projections  |
| **Cache**                | Redis                     | Orleans clustering, caching, pub/sub     |
| **Orchestration (Dev)**  | .NET Aspire               | Full local stack, zero cloud cost        |
| **Hosting (Production)** | Azure Container Apps      | Scale-to-zero, cheap, managed            |
| **Database**             | PostgreSQL Flexible       | Burstable tier, Marten compatible        |

### 6.3 Shared Technologies

| Component         | Technology        | Rationale                           |
| ----------------- | ----------------- | ----------------------------------- |
| **Core Library**  | .NET Standard 2.1 | Maximum compatibility               |
| **Serialization** | System.Text.Json  | Fast, built-in, AOT-friendly        |
| **Validation**    | FluentValidation  | Expressive, testable rules          |
| **Mapping**       | Mapperly          | Source-generated, zero runtime cost |

---

## 7. Testing Architecture

### 7.1 Testing Pyramid

```
                         ┌─────────────────┐
                         │   Exploratory   │  ◄── MANUAL ONLY
                         │    Testing      │
                         └─────────────────┘
                        ┌───────────────────┐
                        │  Performance &    │  ◄── NBomber
                        │   Load Tests      │      BenchmarkDotNet
                        └───────────────────┘      (Automated, Maintained)
                       ┌─────────────────────┐
                       │     E2E Tests       │  ◄── Playwright
                       │    (UI flows)       │      (Automated)
                       └─────────────────────┘
                  ┌───────────────────────────────┐
                  │     Integration Tests         │  ◄── TestContainers
                  │   (API, Grains, Database)     │      Orleans.TestingHost
                  └───────────────────────────────┘
             ┌─────────────────────────────────────────┐
             │        Acceptance Tests (BDD)           │  ◄── Reqnroll
             │     Given/When/Then Game Scenarios      │
             └─────────────────────────────────────────┘
        ┌───────────────────────────────────────────────────┐
        │               Unit Tests                          │  ◄── xUnit
        │    Domain Logic, Calculations, Projections        │      AwesomeAssertions
        └───────────────────────────────────────────────────┘

ALL AUTOMATED EXCEPT EXPLORATORY — PERFORMANCE TESTS ARE MAINTAINED IN CI
```

### 7.2 Testing Technologies

| Layer              | Technologies                                 | License        | Purpose                                    |
| ------------------ | -------------------------------------------- | -------------- | ------------------------------------------ |
| **Unit**           | xUnit, AwesomeAssertions, NSubstitute, Bogus | Apache/MIT/BSD | Domain logic, calculations                 |
| **BDD/Acceptance** | Reqnroll                                     | BSD-3-Clause   | Game scenarios in Gherkin                  |
| **Integration**    | TestContainers, Orleans.TestingHost          | MIT            | Real DB, real grains                       |
| **Snapshot**       | Verify                                       | MIT            | State comparison, regression detection     |
| **E2E**            | Playwright                                   | Apache 2.0     | Full UI flow automation                    |
| **Performance**    | NBomber                                      | Apache 2.0     | Load testing, stress testing, soak testing |
| **Mutation**       | Stryker.NET                                  | Apache 2.0     | Test quality validation                    |
| **Benchmarking**   | BenchmarkDotNet                              | MIT            | Micro-benchmarks, perf regression          |

All testing libraries are fully open source with permissive licenses.

### 7.3 Test-First Development Flow

```
FEATURE DEVELOPMENT CYCLE
━━━━━━━━━━━━━━━━━━━━━━━━━

1. WRITE ACCEPTANCE TEST (Reqnroll)
   ┌────────────────────────────────────────┐
   │ Feature: Class Acquisition             │
   │   Scenario: Earn Warrior through combat│
   │     Given a new player                 │
   │     When they perform 100 strikes      │
   │     And they rest                      │
   │     Then they should have [Warrior]    │
   └────────────────────────────────────────┘
              │
              ▼ TEST FAILS (Red)

2. WRITE UNIT TESTS
   ┌────────────────────────────────────────┐
   │ TagCalculator_Strike_GeneratesTags()   │
   │ ClassEvaluator_MeetsThreshold_Returns()│
   │ SkillAcquisition_OnLevelUp_Grants()    │
   └────────────────────────────────────────┘
              │
              ▼ TESTS FAIL (Red)

3. IMPLEMENT DOMAIN LOGIC
   ┌────────────────────────────────────────┐
   │ IdleWorlds.Core implementation         │
   └────────────────────────────────────────┘
              │
              ▼ UNIT TESTS PASS (Green)
              ▼ ACCEPTANCE TEST PASSES (Green)

4. WRITE INTEGRATION TESTS
   ┌────────────────────────────────────────┐
   │ Test with real DB, real Orleans grains │
   └────────────────────────────────────────┘
              │
              ▼ INTEGRATION TESTS PASS

5. ADD PERFORMANCE BASELINE (if applicable)
   ┌────────────────────────────────────────┐
   │ NBomber scenario for new endpoint      │
   │ BenchmarkDotNet for hot path code      │
   └────────────────────────────────────────┘
              │
              ▼ BASELINE ESTABLISHED

6. EXPLORATORY TESTING (Manual)
   ┌────────────────────────────────────────┐
   │ QA explores edge cases, UX issues      │
   │ Findings become new automated tests    │
   └────────────────────────────────────────┘
```

### 7.4 What Gets Tested Where

| System                 | Unit | BDD | Integration | E2E | Perf |
| ---------------------- | ---- | --- | ----------- | --- | ---- |
| Tag calculations       | ✓    |     |             |     | ✓    |
| Class acquisition      | ✓    | ✓   |             |     |      |
| Skill acquisition      | ✓    | ✓   |             |     |      |
| Action processing      | ✓    | ✓   |             |     | ✓    |
| NPC goal evaluation    | ✓    | ✓   |             |     | ✓    |
| NPC simulation         | ✓    | ✓   | ✓           |     | ✓    |
| Offline calculation    | ✓    | ✓   |             |     | ✓    |
| Event persistence      |      |     | ✓           |     | ✓    |
| Orleans grains         |      |     | ✓           |     | ✓    |
| Sync protocol          |      | ✓   | ✓           |     | ✓    |
| API endpoints          |      |     | ✓           |     | ✓    |
| Full game flows        |      |     |             | ✓   |      |
| UI interactions        |      |     |             | ✓   |      |
| Concurrent players     |      |     |             |     | ✓    |
| World simulation scale |      |     |             |     | ✓    |

### 7.5 Performance Testing Strategy

Performance tests are **maintained alongside functional tests** and run in CI/CD.

```
PERFORMANCE TEST CATEGORIES
━━━━━━━━━━━━━━━━━━━━━━━━━━━

MICRO-BENCHMARKS (BenchmarkDotNet)
├─ Tag calculation performance
├─ Action processing throughput
├─ NPC tick simulation time
├─ Event serialization/deserialization
├─ Offline progress calculation
└─ Run: On every PR affecting core logic

LOAD TESTS (NBomber)
├─ API endpoint throughput
├─ SignalR connection limits
├─ Orleans grain activation rate
├─ Concurrent player simulation
├─ Database query performance
└─ Run: Nightly or on-demand

STRESS TESTS (NBomber)
├─ Find breaking points
├─ Resource exhaustion scenarios
├─ Recovery after overload
└─ Run: Weekly or pre-release

SOAK TESTS (NBomber)
├─ Memory leak detection
├─ Long-running stability
├─ 24-48 hour continuous load
└─ Run: Pre-release milestones

PERFORMANCE BASELINES
━━━━━━━━━━━━━━━━━━━━━
All performance tests establish baselines.
CI fails if performance degrades beyond threshold (e.g., >10%).
```

| Test Type        | Tool            | Frequency   | Failure Threshold |
| ---------------- | --------------- | ----------- | ----------------- |
| Micro-benchmarks | BenchmarkDotNet | Every PR    | >10% regression   |
| Load tests       | NBomber         | Nightly     | <95% of baseline  |
| Stress tests     | NBomber         | Weekly      | Must not crash    |
| Soak tests       | NBomber         | Pre-release | No memory leaks   |

---

## 8. Project Structure

```
IdleWorlds/
├── src/
│   ├── IdleWorlds.Core/              # Shared game engine (portable)
│   │   ├── Domain/                   # Entities, value objects
│   │   ├── Engine/                   # Game logic services
│   │   ├── Events/                   # Event definitions
│   │   └── Abstractions/             # IEventStore, etc.
│   │
│   ├── IdleWorlds.Client.Core/       # Shared client logic
│   │   ├── Services/                 # LocalGameService, SyncService
│   │   ├── Storage/                  # LiteDB implementations
│   │   └── ViewModels/               # MVVM view models
│   │
│   ├── IdleWorlds.Client.Maui/       # MAUI application
│   │
│   ├── IdleWorlds.Server.Api/        # ASP.NET Core API
│   │
│   ├── IdleWorlds.Server.Orleans/    # Orleans grains
│   │   ├── Grains/                   # Player, NPC, Zone grains
│   │   └── Persistence/              # Marten implementations
│   │
│   └── IdleWorlds.Shared/            # Client-server contracts
│
├── tests/
│   ├── IdleWorlds.Core.Tests/        # Unit + BDD tests
│   │   ├── Unit/
│   │   └── Features/                 # Reqnroll .feature files
│   │
│   ├── IdleWorlds.Integration.Tests/ # TestContainers + Orleans
│   │
│   ├── IdleWorlds.E2E.Tests/         # Playwright
│   │
│   ├── IdleWorlds.Performance.Tests/ # Performance test suite
│   │   ├── Benchmarks/               # BenchmarkDotNet micro-benchmarks
│   │   ├── Load/                     # NBomber load test scenarios
│   │   ├── Stress/                   # NBomber stress test scenarios
│   │   └── Soak/                     # NBomber long-running tests
│   │
│   └── IdleWorlds.Mutation.Tests/    # Stryker.NET configuration
│
└── deploy/
    ├── infrastructure/               # Bicep/Pulumi IaC (open source)
    └── docker/                       # Container definitions
```

---

## 11. Decision Summary

| Decision             | Choice               | License          | Alternatives Considered                                    |
| -------------------- | -------------------- | ---------------- | ---------------------------------------------------------- |
| Client Framework     | .NET MAUI            | MIT              | Unity (overkill), Flutter (not .NET)                       |
| Local Storage        | LiteDB               | MIT              | SQLite (more setup), Realm (proprietary)                   |
| Actor Framework      | Orleans              | MIT              | Akka.NET (less .NET-native), Proto.Actor (smaller)         |
| Event Store          | Marten               | MIT              | EventStoreDB (separate service), custom                    |
| Dev Orchestration    | .NET Aspire          | MIT              | Docker Compose (less integrated), manual container mgmt    |
| Cloud Host           | Azure Container Apps | N/A              | AKS (complex), App Service (no scale-to-zero)              |
| BDD Testing          | Reqnroll             | BSD-3-Clause     | SpecFlow (commercial), xBehave (less tooling)              |
| Assertions           | AwesomeAssertions    | Apache 2.0       | FluentAssertions (less permissive)                         |
| Load Testing         | NBomber              | Apache 2.0       | k6 (not .NET), Gatling (JVM)                               |
| Benchmarking         | BenchmarkDotNet      | MIT              | Manual timing (unreliable)                                 |
| API Style            | Minimal APIs         | MIT              | Controllers (ceremony), gRPC (mobile complexity)           |
| **Asset Generation** | **Pixellab.ai**      | **Subscription** | Midjourney (less game-focused), manual art (slow)          |
| **Art Style**        | **Cozy Fantasy**     | **N/A**          | Pixel art (dated), grimdark (wrong tone), chibi (too cute) |

### Open Source Commitment

All core dependencies use permissive open source licenses (MIT, Apache 2.0, BSD):

```
LICENSE SUMMARY
━━━━━━━━━━━━━━━

MIT License:
  .NET MAUI, Orleans, Marten, LiteDB, xUnit, NSubstitute,
  Bogus, Verify, CommunityToolkit.Mvvm, Mapperly, BenchmarkDotNet,
  .NET Aspire

Apache 2.0:
  Playwright, NBomber, Stryker.NET, AwesomeAssertions

BSD-3-Clause:
  Reqnroll, Polly, Redis client

PostgreSQL License:
  PostgreSQL (permissive, similar to MIT/BSD)

No proprietary or copyleft (GPL) dependencies in core stack.
```

---

_Document Version 2.4 — High-Level Architecture (.NET Aspire Added)_
