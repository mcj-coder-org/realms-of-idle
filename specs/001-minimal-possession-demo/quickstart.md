# Quickstart Guide: Minimal Possession Demo v1

**Feature**: Minimal Possession Demo
**Branch**: `001-minimal-possession-demo`
**Date**: 2026-02-09

---

## Prerequisites

- .NET 10.0 SDK installed
- Git installed
- Node.js (for npm scripts and quality gates)
- Chrome, Firefox, or Edge browser (desktop)

---

## Getting Started

### 1. Clone and Checkout

```bash
# Clone the repository (if not already cloned)
git clone git@github-martincjarvis:owner/realms-of-idle.git
cd realms-of-idle

# Checkout the feature branch
git checkout 001-minimal-possession-demo
```

### 2. Restore Dependencies

```bash
dotnet restore
npm install
```

### 3. Run the Application

```bash
dotnet run --project src/RealmsOfIdle.Client.Blazor
```

The application will start on `https://localhost:5001` (or similar - check console output).

### 4. Open the Demo

Navigate to: `https://localhost:5001/possession-demo`

You should see:

- 10x10 grid with 2 buildings (The Rusty Tankard, Tomas' Forge)
- 4 NPC portraits in the sidebar (Mara, Cook, Tomas, Customer)
- Activity log showing autonomous NPC actions
- World time ticking forward

**Note**: State persists in LiteDB (browser-embedded database). Refresh the page → settlement state reloads.

---

## Using the Demo

### Observer Mode (Default)

Watch the simulation run autonomously:

- Mara serves customers at the Inn
- Tomas crafts swords at the Workshop
- Activity log updates with each action
- Gold and reputation counters increase

### Possession Mode

1. Click **Mara's portrait** in the sidebar
2. Click **"Possess"** button
3. See available actions: "Serve Customer", "Manage Cook", "Check Income"
4. Click **"Serve Customer"**
5. Watch 5-second timer count down
6. See result: "+5 gold, +2 reputation"
7. Click **"Release Control"**
8. Mara resumes autonomous behavior

### Context-Aware Actions

1. Possess **Tomas** (Blacksmith, Level 8)
2. Available actions change to: "Craft Iron Sword", "Check Materials", "Set Priority"
3. Click **"Craft Iron Sword"**
4. 30-second timer starts
5. Result: "+20 gold" (costs 2 Iron Ore from building resources)

### Persistent Priority Changes

1. Possess **Mara**
2. Click **"Manage Cook"**
3. Adjust quality slider to 80%
4. Click **"Apply"**
5. Release control
6. Observe Cook's actions take longer but produce higher quality results

### Favorites System

1. Click **star icon (⭐)** on Mara's portrait
2. Mara appears in **Favorites panel**
3. Click her name in Favorites → instant possession
4. Release control
5. Notification badge appears when she completes actions while not possessed

---

## Testing

### Unit Tests Only (Fast)

```bash
dotnet test --filter "Category!=E2E&Category!=Integration"
```

Runs:

- Service tests (`SimulationEngine`, `PossessionManager`, `NPCAIService`)
- Component tests (`NPCSidebar`, `ActionPanel`)
- Excludes integration tests and E2E tests

### All Tests (Includes Integration)

```bash
dotnet test
```

**Note**: E2E tests require AppHost running (see main README for Aspire setup).

### Specific Test Projects

```bash
# Blazor client tests only
dotnet test tests/RealmsOfIdle.Client.Blazor.Tests

# Specific test class
dotnet test --filter "FullyQualifiedName~SimulationEngineTests"
```

---

## Quality Gates

### Pre-Commit (Automatic via Husky)

Hooks run automatically when you `git commit`:

- Prettier formatting (all files)
- Markdown lint
- C# formatting (if `.cs` files staged)
- Unit tests (if `.cs` files staged)

### Pre-Push (Manual, Recommended)

Before pushing to remote:

```bash
npm run quality
```

Runs:

- Full lint check (format + markdown + links)
- Unit tests
- Build verification

---

## Development Workflow

### Making Changes

1. **Edit code** (follow TDD: write test → implement → verify)
2. **Run tests**: `dotnet test --filter "Category!=E2E"`
3. **Commit**: `git commit -m "feat(possession): add NPC action filtering"`
4. **Hooks run automatically** (lint, format, tests)
5. **Push**: `git push origin 001-minimal-possession-demo`

### Debugging

**Browser DevTools**:

- Open Chrome DevTools (F12)
- Check Console for errors
- Use Blazor DevTools extension (optional)

**Hot Reload**:

- Edit `.razor` or `.cs` files
- Browser auto-refreshes (Blazor Hot Reload)
- No need to restart `dotnet run`

**Breakpoints**:

- Add `debugger;` in C# code (Blazor WASM supports browser debugging)
- Or use `Console.WriteLine()` for quick logging

---

## Project Structure

```
src/RealmsOfIdle.Client.Blazor/
├── Pages/
│   └── PossessionDemo.razor       # Main demo page (route: /possession-demo)
├── Components/
│   ├── SettlementView.razor       # Top-level container
│   ├── SettlementMap.razor        # Grid display with buildings/NPCs
│   ├── NPCSidebar.razor           # NPC list with portraits
│   ├── ActionPanel.razor          # Available actions when possessed
│   ├── ActivityLog.razor          # Scrolling action history
│   └── TopBar.razor               # Mode indicator, world time
├── Services/
│   ├── SimulationEngine.cs        # Game loop (10 ticks/sec)
│   ├── PossessionManager.cs       # Possession state management
│   └── NPCAIService.cs            # Autonomous NPC behavior
└── Models/
    ├── Settlement.cs              # Contains Buildings + NPCs
    ├── Building.cs                # Type, Position, Resources
    ├── NPC.cs                     # State, Class, Location, Gold
    ├── NPCAction.cs               # Duration, Costs, Rewards
    └── ActivityLogEntry.cs        # Timestamp, Actor, Action

tests/RealmsOfIdle.Client.Blazor.Tests/
├── Services/
│   ├── SimulationEngineTests.cs
│   ├── PossessionManagerTests.cs
│   └── NPCAIServiceTests.cs
├── Components/
│   ├── NPCSidebarTests.cs
│   └── ActionPanelTests.cs
└── Integration/
    └── PossessionDemoE2ETests.cs  # End-to-end using bUnit
```

---

## Troubleshooting

### App Won't Start

**Error**: `dotnet run` fails with "Port already in use"

**Fix**:

```bash
# Find process using port 5001
lsof -i :5001
# Kill process
kill -9 <PID>
# Or change port in launchSettings.json
```

### Tests Fail on First Run

**Error**: `Cannot find assembly RealmsOfIdle.Core`

**Fix**:

```bash
dotnet build
dotnet test
```

### Hot Reload Not Working

**Error**: Changes don't appear in browser after save

**Fix**:

- Hard refresh: `Ctrl+Shift+R` (Windows/Linux) or `Cmd+Shift+R` (Mac)
- Restart `dotnet run`
- Clear browser cache

### Game Loop Not Running

**Symptom**: NPCs don't move, activity log empty, world time frozen

**Check**:

1. Open browser console (F12)
2. Look for JavaScript errors
3. Check `SimulationEngine.Start()` is called in `PossessionDemo.OnInitialized()`
4. Verify `Timer.Enabled == true` in debugger

### State is Corrupted

**Symptom**: Settlement has invalid data, NPCs in wrong state, crashes on load

**Fix**:

```bash
# Clear LiteDB database (browser storage)
# Open browser DevTools → Application tab → IndexedDB → Delete database
# Or use Clear Storage → Clear site data
```

**Alternative**:

```csharp
// In SettlementGameService, add reset method
public async Task ResetDatabaseAsync()
{
    _db.DropCollection("settlements");
    _db.DropCollection("activityLog");
}
```

---

## Next Steps

After completing this demo:

1. **Play the demo** for 5+ minutes to validate core mechanics
2. **Review acceptance criteria** in `specs/001-minimal-possession-demo/spec.md`
3. **Check success metrics** in `specs/001-minimal-possession-demo/plan.md`
4. **Gather feedback** from playtesters
5. **Plan v1.1** features (more buildings, resource flow, customer AI)

---

## References

- **Feature Spec**: `specs/001-minimal-possession-demo/spec.md`
- **Implementation Plan**: `specs/001-minimal-possession-demo/plan.md`
- **Data Model**: `specs/001-minimal-possession-demo/data-model.md`
- **Research**: `specs/001-minimal-possession-demo/research.md`
- **Core Architecture**: `docs/design/core-architecture.md`
- **Design Document**: `docs/plans/2026-02-09-minimal-possession-demo.md`

---

## Support

If you encounter issues:

1. Check this guide's Troubleshooting section
2. Review error messages in browser console
3. Check git branch: `git status` (should be on `001-minimal-possession-demo`)
4. Verify dependencies: `dotnet restore && npm install`
5. Run quality gates: `npm run quality`

**Happy hacking! 🎮**
