# ADR-0003: Source Generator Selection

## Status

Accepted

## Context

The Realms of Idle project requires extensive boilerplate code for:

- Entity-to-DTO mapping (API layer)
- Strongly-typed IDs (domain modeling)
- Enumeration types with behavior

We want to minimize boilerplate while maintaining:

- Type safety
- Performance (no reflection at runtime)
- Debuggability (source code is visible)
- Testability

## Decision

We have selected the following source generators and utilities:

| Purpose            | Tool              | Version | Why                                                      |
| ------------------ | ----------------- | ------- | -------------------------------------------------------- |
| DTO ↔ Entity       | Riok.Mapperly     | 4.1.1   | Zero-reflection, compile-time validated, fast            |
| Strongly-Typed IDs | StronglyTypedId   | 1.16.0  | JSON/EF Core converters, equality members, zero-overhead |
| Smart Enums        | Ardalis.SmartEnum | 8.0.0   | Type-safe enums with associated behavior                 |

### Riok.Mapperly

```csharp
[Mapper]
public static partial class PlayerMapper
{
    public static partial PlayerDto ToDto(this Player entity);
    public static partial Player ToEntity(this.PlayerDto dto);
}
```

**Why Mapperly over AutoMapper?**

- Compile-time mapping (errors at build time, not runtime)
- Significantly faster (no reflection)
- Better IDE support (go to definition works)
- Smaller binary size

### StronglyTypedId (Andrew Lock)

```csharp
[StronglyTypedId]
public partial struct PlayerId { }

// Generates:
// - JSON converters
// - EF Core value converters
// - Equality members
// - TryParse methods
```

**Why StronglyTypedId?**

- Domain modeling best practice (primitive obsession)
- Prevents ID mixing (`PlayerId` vs `NpcId` - can't accidentally assign)
- Zero-allocation struct implementation
- Comprehensive ecosystem support (JSON, EF Core, Dapper)

### Ardalis.SmartEnum

```csharp
public abstract class NpcArchetype : SmartEnum<NpcArchetype>
{
    public static readonly NpcArchetype Maintainer = new MaintainerType();
    public static readonly NpcArchetype Hero = new HeroType();
    public static readonly NpcArchetype Villain = new VillainType();

    public abstract IEnumerable<GoalType> GetDefaultGoals();

    private NpcArchetype(string name, int value) : base(name, value) { }

    private class MaintainerType : NpcArchetype
    {
        public MaintainerType() : base("Maintainer", 1) { }
        public override IEnumerable<GoalType> GetDefaultGoals() =>
            new[] { GoalType.Survival, GoalType.Economic };
    }
}
```

**Why SmartEnum?**

- Type-safe enum alternatives with behavior
- OOP inheritance for enum-specific logic
- Performance of standard enums
- Excellent for domain modeling

## Consequences

### Positive

- Drastic reduction in boilerplate code
- Compile-time safety for mappings and IDs
- Runtime performance (no reflection)
- Better debugging experience (generated code is readable)

### Negative

- Additional tooling dependency
- Generated code not visible in source (need to use "Go to Generated")
- Learning curve for team members unfamiliar with source generators

## Alternatives Considered

### AutoMapper

**Rejected**: Runtime-based mapping uses reflection, slower and errors only surface at runtime.

### Manual Mapping

**Rejected**: Too much boilerplate, error-prone, difficult to maintain.

### Built-in Enums

**Rejected**: Cannot have associated behavior or inheritance. SmartEnum provides type safety + OOP.

### String/Primitive IDs

**Rejected**: Leads to "primitive obsession" code smell. `string playerId` can accidentally be assigned a wrong value. Strongly-typed IDs prevent this.

### Custom Source Generators

**Rejected**: The selected tools are mature, well-tested, and cover our needs. Building custom generators would be maintenance overhead.

## References

- Mapperly: <https://mapperly.riok.app/>
- StronglyTypedId: <https://github.com/andrewlock/StronglyTypedId>
- SmartEnum: <https://github.com/ardalis/SmartEnum>
