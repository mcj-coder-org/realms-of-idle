<!-- ADAPTATION REQUIRED -->
<!-- This file was migrated from source but needs manual review: -->
<!-- - Update terminology (dormant classes, XP split, etc.) -->
<!-- - Align with current GDD architecture -->
<!-- - Add missing sections as needed -->
<!-- - Update frontmatter with correct gdd_ref -->

---

title: Unified Tag System Reference
type: system
category: content
summary: Complete reference for the hierarchical tag system with C# code references

---

# Unified Tag System Reference

## Overview

The **unified tag system** provides a hierarchical, strongly-typed architecture for organizing and controlling access to game content (classes, skills, recipes, materials, items). Tags use `Category/Subcategory/Specialization` format with inheritance-based access control.

### Core Principles

| Principle                  | Description                                             |
| -------------------------- | ------------------------------------------------------- |
| **Hierarchical Structure** | Tags use `Category/Subcategory/Specialization` format   |
| **Strongly Typed**         | All tags defined in `SkillTags.cs` - no string literals |
| **Inheritance Access**     | Parent tags grant access to all child tags              |
| **Specificity = Power**    | Deeper tags indicate more specialized content           |
| **Wildcard Matching**      | Pattern-based matching for flexible queries             |

---

## Tag Structure Mechanics

### SkillTag Struct (Core Implementation)

**Source:** `src/CozyFantasyRpg/Shared/SkillTag.cs`

```csharp
public readonly record struct SkillTag(string Path)
{
    // Split path into segments
    public string[] Segments => Path.Split('/');

    // Depth = number of segments (Crafting = 1, Crafting/Smithing = 2)
    public int Depth => Segments.Length;

    // Check if this tag contains another (parent-child relationship)
    public bool Contains(SkillTag other) =>
        other.Path.StartsWith(Path + "/", StringComparison.Ordinal) || other.Path == Path;

    // Check if accessible at maximum depth
    public bool IsAccessibleAtDepth(int maxDepth) => Depth <= maxDepth;

    // Wildcard pattern matching (e.g., "Crafting/*/Apprentice")
    public bool Matches(SkillTag candidate) { ... }

    // Implicit string conversion
    public static implicit operator SkillTag(string path) => new(path);
}
```

### Depth Calculation

```
"Crafting"                           = depth 1
"Crafting/Smithing"                  = depth 2
"Crafting/Smithing/Weapon"           = depth 3
"Crafting/Smithing/Weapon/Sword"     = depth 4
"Crafting/Smithing/Weapon/Journeyman" = depth 4 (wildcard branch)
```

**Rule:** Count slashes, add 1.

### Inheritance Rules

**Parent tags grant access to all child tags:**

- Character with `Crafting/Smithing` (depth 2) can access:
  - `Crafting` (depth 1) ✓
  - `Crafting/Smithing` (depth 2) ✓
  - `Crafting/Smithing/Weapon` (depth 3) ✓
  - `Crafting/Smithing/Armor` (depth 3) ✓

### Wildcard Matching

**Wildcard tags use `*` to match any segment:**

```csharp
SkillTags.Crafting.Wildcards.Apprentice  // "Crafting/*/Apprentice"
```

This matches:

- `Crafting/Smithing/Apprentice` ✓
- `Crafting/Leatherworking/Apprentice` ✓
- `Crafting/Woodworking/Apprentice` ✓

---

## Complete Tag Hierarchy

**Source:** `src/CozyFantasyRpg/Shared/SkillTags.cs`

### 1. Crafting Tags

```
Crafting (depth 1)
├── Smithing (depth 2)
│   ├── Value         ("Crafting/Smithing")
│   ├── Apprentice    ("Crafting/Smithing/Apprentice")
│   ├── Iron          ("Crafting/Smithing/Iron")
│   ├── Weapon        ("Crafting/Smithing/Weapon")
│   ├── Armor         ("Crafting/Smithing/Armor")
│   ├── Shield        ("Crafting/Smithing/Shield")
│   └── Tools         ("Crafting/Smithing/Tools")
│
├── Leatherworking (depth 2)
│   ├── Value         ("Crafting/Leatherworking")
│   └── Armor         ("Crafting/Leatherworking/Armor")
│
├── Woodworking (depth 2)
│   ├── Value         ("Crafting/Woodworking")
│   ├── Weapon        ("Crafting/Woodworking/Weapon")
│   └── Shield        ("Crafting/Woodworking/Shield")
│
└── Stations (depth 2)
    ├── Forge             ("Crafting/Smithing/Forge")
    ├── LeatherworkingStation ("Crafting/Leatherworking/Station")
    ├── CarpenterBench     ("Crafting/Woodworking/Bench")
    ├── TailorsTable       ("Crafting/Tailoring/Table")
    ├── Kitchen            ("Crafting/Cooking/Kitchen")
    ├── JewelryStation     ("Crafting/Jewelry/Station")
    ├── EnchantersTable    ("Magic/Enchantment/Table")
    └── Laboratory         ("Magic/Alchemy/Laboratory")
└── Wildcards
    ├── Apprentice    ("Crafting/*/Apprentice")
    └── Journeyman    ("Crafting/*/Journeyman")
```

### C# Reference - Crafting Tags

| Tag Path                          | C# Reference                                        | Purpose                   |
| --------------------------------- | --------------------------------------------------- | ------------------------- |
| `Crafting`                        | `SkillTags.Crafting.Value`                          | Universal crafting access |
| `Crafting/Smithing`               | `SkillTags.Crafting.Smithing.Value`                 | Smithing profession       |
| `Crafting/Smithing/Apprentice`    | `SkillTags.Crafting.Smithing.Apprentice`            | Apprentice smithing tier  |
| `Crafting/Smithing/Iron`          | `SkillTags.Crafting.Smithing.Iron`                  | Iron working              |
| `Crafting/Smithing/Weapon`        | `SkillTags.Crafting.Smithing.Weapon`                | Weapon smithing           |
| `Crafting/Smithing/Armor`         | `SkillTags.Crafting.Smithing.Armor`                 | Armor smithing            |
| `Crafting/Smithing/Shield`        | `SkillTags.Crafting.Smithing.Shield`                | Shield smithing           |
| `Crafting/Smithing/Tools`         | `SkillTags.Crafting.Smithing.Tools`                 | Tool smithing             |
| `Crafting/Leatherworking`         | `SkillTags.Crafting.Leatherworking.Value`           | Leatherworking profession |
| `Crafting/Leatherworking/Armor`   | `SkillTags.Crafting.Leatherworking.Armor`           | Leather armor             |
| `Crafting/Woodworking`            | `SkillTags.Crafting.Woodworking.Value`              | Woodworking profession    |
| `Crafting/Woodworking/Weapon`     | `SkillTags.Crafting.Woodworking.Weapon`             | Wooden weapons            |
| `Crafting/Woodworking/Shield`     | `SkillTags.Crafting.Woodworking.Shield`             | Wooden shields            |
| `Crafting/Smithing/Forge`         | `SkillTags.Crafting.Stations.Forge`                 | Forge station             |
| `Crafting/Leatherworking/Station` | `SkillTags.Crafting.Stations.LeatherworkingStation` | Leatherworking station    |
| `Crafting/Woodworking/Bench`      | `SkillTags.Crafting.Stations.CarpenterBench`        | Carpenter's bench         |
| `Crafting/Tailoring/Table`        | `SkillTags.Crafting.Stations.TailorsTable`          | Tailor's table            |
| `Crafting/Cooking/Kitchen`        | `SkillTags.Crafting.Stations.Kitchen`               | Kitchen station           |
| `Crafting/Jewelry/Station`        | `SkillTags.Crafting.Stations.JewelryStation`        | Jewelry station           |
| `Magic/Enchantment/Table`         | `SkillTags.Crafting.Stations.EnchantersTable`       | Enchanter's table         |
| `Magic/Alchemy/Laboratory`        | `SkillTags.Crafting.Stations.Laboratory`            | Laboratory station        |
| `Crafting/*/Apprentice`           | `SkillTags.Crafting.Wildcards.Apprentice`           | Any apprentice tier       |
| `Crafting/*/Journeyman`           | `SkillTags.Crafting.Wildcards.Journeyman`           | Any journeyman tier       |

---

## Workshops and Stations

### Workshop Buildings

**Workshops are settlement buildings** where crafting happens. They are not tag-restricted and any character can enter them.

**Workshop Types:**

- Blacksmith's Workshop
- Carpenter's Workshop
- Leatherworker's Workshop
- Tailor's Workshop
- Enchanter's Laboratory
- Alchemist's Laboratory
- Jeweler's Workshop
- Cook's Kitchen

**Key Design:** Workshops support **multiclass crafters**. A character with Blacksmith/Carpenter/Enchanter classes can outfit one workshop with multiple stations (Forge, Carpenter's Bench, Enchanter's Table) and work efficiently without managing multiple buildings.

### Station Equipment

**Stations are crafting equipment** within workshops. Access is controlled by crafting tags.

| Station                | Required Tag              | Workshop                 | Classes       |
| ---------------------- | ------------------------- | ------------------------ | ------------- |
| Forge                  | `Crafting/Smithing`       | Blacksmith's Workshop    | Blacksmith    |
| Carpenter's Bench      | `Crafting/Woodworking`    | Carpenter's Workshop     | Carpenter     |
| Leatherworking Station | `Crafting/Leatherworking` | Leatherworker's Workshop | Leatherworker |
| Tailor's Table         | `Crafting/Tailoring`      | Tailor's Workshop        | Tailor        |
| Kitchen                | `Crafting/Cooking`        | Cook's Kitchen           | Cook          |
| Jewelry Station        | `Crafting/Jewelry`        | Jeweler's Workshop       | Jeweler       |
| Enchanter's Table      | `Magic/Enchantment`       | Enchanter's Laboratory   | Enchanter     |
| Laboratory             | `Magic/Alchemy`           | Alchemist's Laboratory   | Alchemist     |

**How It Works:**

1. Character accepts a crafting class (e.g., Blacksmith)
2. Class grants the crafting tag (e.g., `Crafting/Smithing`)
3. Character can now use any station that requires that tag
4. Multiclass characters accumulate tags and can use multiple stations
5. Apprentices/partners can share workshop space and stations

### Example: Multiclass Blacksmith/Carpenter/Enchanter

**Character Tags:** `Crafting/Smithing`, `Crafting/Woodworking`, `Magic/Enchantment`

**Can Use Stations:**

- Forge (requires `Crafting/Smithing`) ✓
- Carpenter's Bench (requires `Crafting/Woodworking`) ✓
- Enchanter's Table (requires `Magic/Enchantment`) ✓

**Workshop Setup:** Single workshop outfitted with all three stations.

---

### 2. Gathering Tags

```
Gathering (depth 1)
└── (currently empty - tags to be added)
```

**Planned hierarchy:**

- `Gathering/Mining` - Ore and mineral extraction
- `Gathering/Herbalism` - Plant gathering
- `Gathering/Hunting` - Animal tracking
- `Gathering/Fishing` - Aquatic resources
- `Gathering/Lumbering` - Wood harvesting

---

### 3. Material Tags

```
Material (depth 1)
├── Metal (depth 2)
│   ├── Value         ("Material/Metal")
│   ├── Iron          ("Material/Metal/Iron")
│   ├── Copper        ("Material/Metal/Copper")
│   ├── Steel         ("Material/Metal/Steel")
│   ├── Bronze        ("Material/Metal/Bronze")
│   ├── Silver        ("Material/Metal/Silver")
│   └── Gold          ("Material/Metal/Gold")
│
├── Wood (depth 2)
│   ├── Value         ("Material/Wood")
│   ├── Oak           ("Material/Wood/Oak")
│   ├── Pine          ("Material/Wood/Pine")
│   ├── Maple         ("Material/Wood/Maple")
│   └── Ebony         ("Material/Wood/Ebony")
│
├── Leather (depth 2)
│   ├── Value         ("Material/Leather")
│   ├── Hide          ("Material/Leather/Hide")
│   └── Tanned        ("Material/Leather/Tanned")
│
├── Fabric (depth 2)
│   ├── Value         ("Material/Fabric")
│   ├── Cotton        ("Material/Fabric/Cotton")
│   ├── Linen         ("Material/Fabric/Linen")
│   └── Wool          ("Material/Fabric/Wool")
│
├── Stone (depth 2)
│   ├── Value         ("Material/Stone")
│   ├── Granite       ("Material/Stone/Granite")
│   └── Marble        ("Material/Stone/Marble")
│
├── Bone  (depth 2)   ("Material/Bone")
├── Horn  (depth 2)   ("Material/Horn")
└── Silk  (depth 2)   ("Material/Silk")
```

### C# Reference - Material Tags

| Tag Path                  | C# Reference                        | Purpose                |
| ------------------------- | ----------------------------------- | ---------------------- |
| `Material`                | `SkillTags.Material.Value`          | Universal material tag |
| `Material/Metal`          | `SkillTags.Material.Metal.Value`    | Metal materials        |
| `Material/Metal/Iron`     | `SkillTags.Material.Metal.Iron`     | Iron material          |
| `Material/Metal/Copper`   | `SkillTags.Material.Metal.Copper`   | Copper material        |
| `Material/Metal/Steel`    | `SkillTags.Material.Metal.Steel`    | Steel material         |
| `Material/Metal/Bronze`   | `SkillTags.Material.Metal.Bronze`   | Bronze material        |
| `Material/Metal/Silver`   | `SkillTags.Material.Metal.Silver`   | Silver material        |
| `Material/Metal/Gold`     | `SkillTags.Material.Metal.Gold`     | Gold material          |
| `Material/Wood`           | `SkillTags.Material.Wood.Value`     | Wood materials         |
| `Material/Wood/Oak`       | `SkillTags.Material.Wood.Oak`       | Oak wood               |
| `Material/Wood/Pine`      | `SkillTags.Material.Wood.Pine`      | Pine wood              |
| `Material/Wood/Maple`     | `SkillTags.Material.Wood.Maple`     | Maple wood             |
| `Material/Wood/Ebony`     | `SkillTags.Material.Wood.Ebony`     | Ebony wood             |
| `Material/Leather`        | `SkillTags.Material.Leather.Value`  | Leather materials      |
| `Material/Leather/Hide`   | `SkillTags.Material.Leather.Hide`   | Raw hide               |
| `Material/Leather/Tanned` | `SkillTags.Material.Leather.Tanned` | Tanned leather         |
| `Material/Fabric`         | `SkillTags.Material.Fabric.Value`   | Fabric materials       |
| `Material/Fabric/Cotton`  | `SkillTags.Material.Fabric.Cotton`  | Cotton fabric          |
| `Material/Fabric/Linen`   | `SkillTags.Material.Fabric.Linen`   | Linen fabric           |
| `Material/Fabric/Wool`    | `SkillTags.Material.Fabric.Wool`    | Wool fabric            |
| `Material/Stone`          | `SkillTags.Material.Stone.Value`    | Stone materials        |
| `Material/Stone/Granite`  | `SkillTags.Material.Stone.Granite`  | Granite stone          |
| `Material/Stone/Marble`   | `SkillTags.Material.Stone.Marble`   | Marble stone           |
| `Material/Bone`           | `SkillTags.Material.Bone.Value`     | Bone material          |
| `Material/Horn`           | `SkillTags.Material.Horn.Value`     | Horn material          |
| `Material/Silk`           | `SkillTags.Material.Silk.Value`     | Silk material          |

---

### 4. Combat Tags

```
Combat (depth 1)
├── Value         ("Combat")
├── Ranged        ("Combat/Ranged")
│
├── Melee (depth 2)
│   ├── Value     ("Combat/Melee")
│   ├── Sword     ("Combat/Melee/Sword")
│   ├── Axe       ("Combat/Melee/Axe")
│   ├── Mace      ("Combat/Melee/Mace")
│   └── Dagger    ("Combat/Melee/Dagger")
│
└── Defense (depth 2)
    ├── Value     ("Combat/Defense")
    ├── Shield    ("Combat/Defense/Shield")
    └── Dodge     ("Combat/Defense/Dodge")
```

### C# Reference - Combat Tags

| Tag Path                | C# Reference                      | Purpose                 |
| ----------------------- | --------------------------------- | ----------------------- |
| `Combat`                | `SkillTags.Combat.Value`          | Universal combat access |
| `Combat/Ranged`         | `SkillTags.Combat.Ranged`         | Ranged combat           |
| `Combat/Melee`          | `SkillTags.Combat.Melee.Value`    | Melee combat            |
| `Combat/Melee/Sword`    | `SkillTags.Combat.Melee.Sword`    | Sword combat            |
| `Combat/Melee/Axe`      | `SkillTags.Combat.Melee.Axe`      | Axe combat              |
| `Combat/Melee/Mace`     | `SkillTags.Combat.Melee.Mace`     | Mace combat             |
| `Combat/Melee/Dagger`   | `SkillTags.Combat.Melee.Dagger`   | Dagger combat           |
| `Combat/Defense`        | `SkillTags.Combat.Defense.Value`  | Defense combat          |
| `Combat/Defense/Shield` | `SkillTags.Combat.Defense.Shield` | Shield defense          |
| `Combat/Defense/Dodge`  | `SkillTags.Combat.Defense.Dodge`  | Dodge defense           |

---

## Usage Examples

### Class Definition Tags

**Source:** `src/CozyFantasyRpg/Content/Classes/CraftingClasses.cs`

```csharp
public static readonly ClassDefinition Blacksmith = new(
    Id: new ClassId(Guid.Parse("00000000-0000-0000-0000-000000000001")),
    Name: "Blacksmith",
    Tier: ClassTier.Specialist,
    Tags: [
        SkillTags.Crafting.Value,           // "Crafting"
        SkillTags.Crafting.Smithing.Value   // "Crafting/Smithing"
    ],
    MaxTagDepth: 2,
    StartingSkills: [ ... ],
    GrantedTags: [                         // Additional tags granted
        SkillTags.Crafting.Smithing.Value
    ],
    // ... other fields
);
```

### Recipe Tags (Single Tag Rule)

```yaml
---
title: 'Iron Dagger'
type: 'recipe'
tags:
  - Crafting/Smithing/Weapon # ONLY the deepest tag
materials:
  - Iron Ingot (2)
  - Leather Strip (1)
difficulty: basic
---
```

### Ingredient Tag Matching

**Source:** `src/CozyFantasyRpg/Crafting/IRecipeIngredient.cs`

```csharp
public interface IRecipeIngredient
{
    // Check if single tag matches
    bool MatchesTag(SkillTag tag);

    // Check if ANY of multiple tags match
    bool MatchesAny(IReadOnlyList<SkillTag> tags);
}
```

**Usage:**

```csharp
// Ingredient requires any metal material
var metalIngredient = new RecipeIngredientSlot(
    quantity: 2,
    allowedTags: [SkillTags.Material.Metal.Value]  // Matches Iron, Copper, Steel, etc.
);

// Ingredient specifically requires iron
var ironIngredient = new RecipeIngredientSlot(
    quantity: 1,
    allowedTags: [SkillTags.Material.Metal.Iron]  // Only iron
);

// Wildcard ingredient (any apprentice tier material)
var apprenticeIngredient = new RecipeIngredientSlot(
    quantity: 1,
    allowedTags: [new SkillTag("Material/*/Apprentice")]
);
```

---

## Migration Guide: String Literals to Strongly Typed Tags

### Anti-Pattern (DO NOT DO THIS)

❌ **String literal tags - breaks compile-time safety:**

```csharp
// WRONG - No compile-time checking
var tag = new SkillTag("Crafting/Smithing");

// WRONG - Typo won't be caught until runtime
var badTag = new SkillTag("Crafting/Smithnig");  // TYPO!

// WRONG - Hardcoded string in recipe definition
tags: ["Crafting/Smithing/Weapon"]  // YAML string literal
```

### Correct Pattern (MUST DO THIS)

✅ **Strongly typed tags - compile-time safety:**

```csharp
// CORRECT - IntelliSense support, compile-time checking
var tag = SkillTags.Crafting.Smithing.Value;

// CORRECT - Typo caught at compile time
var tag = SkillTags.Crafting.Smithing.Value;  // IDE will warn if tag doesn't exist

// CORRECT - Use C# reference in YAML frontmatter where possible
// For content files, document the C# reference in a "Tags" section:
//
// ### C# Reference
//
// | Tag Path | C# Reference | Purpose |
// |----------|-------------|---------|
// | Crafting/Smithing | SkillTags.Crafting.Smithing.Value | Smithing access |
```

### Before/After Examples

#### Example 1: Class Definition

**BEFORE:**

```csharp
Tags: [
    new SkillTag("Crafting"),
    new SkillTag("Crafting/Smithing")
]
```

**AFTER:**

```csharp
Tags: [
    SkillTags.Crafting.Value,
    SkillTags.Crafting.Smithing.Value
]
```

#### Example 2: Ingredient Matching

**BEFORE:**

```csharp
var requiredTag = new SkillTag("Material/Metal/Iron");
if (ingredient.MatchesTag(requiredTag)) { ... }
```

**AFTER:**

```csharp
var requiredTag = SkillTags.Material.Metal.Iron;
if (ingredient.MatchesTag(requiredTag)) { ... }
```

#### Example 3: Recipe Access Check

**BEFORE:**

```csharp
var recipeTag = new SkillTag("Crafting/Smithing/Weapon");
var canAccess = character.HasTagOrParent(recipeTag);
```

**AFTER:**

```csharp
var recipeTag = SkillTags.Crafting.Smithing.Weapon;
var canAccess = character.HasTagOrParent(recipeTag);
```

---

## Class Tag Access (Implementation Status)

### GetAllClassTags() - CURRENT STATUS

**Source:** `src/CozyFantasyRpg/Character/GameCharacter.cs:115-119`

**Current Implementation (EMPTY):**

```csharp
public IReadOnlyList<SkillTag> GetAllClassTags()
{
    // TODO: Implement - currently returns empty
    return [];
}
```

**Planned Implementation (Task 1.3):**

```csharp
public IReadOnlyList<SkillTag> GetAllClassTags()
{
    var tags = new List<SkillTag>();

    foreach (var classProgression in _classes.Values)
    {
        // Need to fetch ClassDefinition to get GrantedTags
        // This requires refactoring ClassProgression to store reference
        var classDef = ClassDefinitionRegistry.Get(classProgression.ClassId);
        if (classDef?.GrantedTags != null)
        {
            tags.AddRange(classDef.GrantedTags);
        }
    }

    return tags;
}
```

**Current Workaround:**

Tags are currently stored in `ClassDefinition.Tags` property, but `GetAllClassTags()` doesn't aggregate them. This is a known gap to be fixed in Task 1.3.

---

## Tag-Based Access Control Patterns

### Workshop Access (Planned)

**Current:** Workshops only check `OwnerId` (hardcoded access)

**Planned:** Tag-based access control

```csharp
// Workshop requires Crafter tag to use
public bool CanAccess(Workshop workshop, GameCharacter character)
{
    var requiredTag = SkillTags.Crafting.Value;  // "Crafting"
    return character.GetAllClassTags().Any(tag => requiredTag.Contains(tag));
}
```

### Recipe Discovery (Current)

**Source:** `src/CozyFantasyRpg/Crafting/RecipeAccessChecker.cs:23`

```csharp
public bool CanAccessRecipe(Recipe recipe, GameCharacter character)
{
    var classTags = character.GetAllClassTags();  // Currently returns empty

    // Check if any class tag matches recipe tag
    return recipe.RequiredTags.Any(recipeTag =>
        classTags.Any(classTag =>
            recipeTag.Contains(classTag)  // Parent tag grants access to child
        )
    );
}
```

**Current Status:** Works once `GetAllClassTags()` is implemented (Task 1.3).

---

## Cross-References

### Related Documentation

| Document          | Path                                                 | Purpose                          |
| ----------------- | ---------------------------------------------------- | -------------------------------- |
| Skill Tag System  | `docs/design/systems/character/skill-tags.md`        | Skill progression and class tags |
| Recipe Tag System | `docs/design/systems/crafting/recipe-tag-system.md`  | Recipe access and organization   |
| Content Template  | `docs/design/content/_templates/content-template.md` | Content frontmatter structure    |

### Related Code

| File                | Path                                                 | Purpose                           |
| ------------------- | ---------------------------------------------------- | --------------------------------- |
| SkillTag struct     | `src/CozyFantasyRpg/Shared/SkillTag.cs`              | Tag value type and matching logic |
| SkillTags class     | `src/CozyFantasyRpg/Shared/SkillTags.cs`             | Tag hierarchy definitions         |
| ClassDefinition     | `src/CozyFantasyRpg/Character/ClassDefinition.cs`    | Class tags and GrantedTags        |
| GameCharacter       | `src/CozyFantasyRpg/Character/GameCharacter.cs`      | Tag aggregation (GetAllClassTags) |
| RecipeAccessChecker | `src/CozyFantasyRpg/Crafting/RecipeAccessChecker.cs` | Recipe access control             |
| IRecipeIngredient   | `src/CozyFantasyRpg/Crafting/IRecipeIngredient.cs`   | Ingredient tag matching           |

---

## Best Practices

### When Adding New Tags

1. **Add to SkillTags.cs first** - Define in hierarchy before using
2. **Use strongly typed references** - `SkillTags.Category.Subcategory.Value`
3. **Update this document** - Add to hierarchy tables
4. **Run build verification** - `dotnet build` to catch typos
5. **Document in content files** - Add C# reference tables

### When Writing Content Documentation

Include a **Tags** section with C# references:

```markdown
### Tags

| Tag Path                 | C# Reference                         | Purpose               |
| ------------------------ | ------------------------------------ | --------------------- |
| Crafting/Smithing        | `SkillTags.Crafting.Smithing.Value`  | Smithing access       |
| Crafting/Smithing/Weapon | `SkillTags.Crafting.Smithing.Weapon` | Weapon specialization |
```

### When Migrating Old Code

1. **Search for string literals:**

   ```bash
   grep -rn 'new SkillTag("' src/ --include="*.cs"
   ```

2. **Replace with strongly typed:**

   ```csharp
   // BEFORE
   new SkillTag("Crafting/Smithing")

   // AFTER
   SkillTags.Crafting.Smithing.Value
   ```

3. **Verify with build:**

   ```bash
   dotnet build --warnaserror
   ```

---

## Known Gaps and Future Work

### Missing Tag Categories

- **Gathering tags** - Currently empty in SkillTags.cs
  - Need `Gathering/Mining`, `Gathering/Herbalism`, etc.
  - Future task (not currently scheduled)

### GetAllClassTags() Implementation (Task 1.3)

- Currently returns empty array
- Needs ClassDefinition refactoring
- Required for workshop and recipe access

### Content Template Updates (Task 1.4)

- Templates don't enforce tag documentation
- Need mandatory "Tags" section with C# references

### Station Access Implementation

**Current Status:** Workshops check `OwnerId` only (hardcoded access)

**Required Implementation:**

- Workshop buildings remain accessible to all (no tag restriction)
- Station equipment checks character's crafting tags
- Example: Forge station checks for `Crafting/Smithing` tag
- Multiclass support: Character can use all stations for which they have tags
- Apprentice/partner sharing: Characters can grant access to apprentices

**Code Reference:** `src/CozyFantasyRpg/Settlement/Buildings/WorkshopActionProvider.cs`

**Implementation Status:** Tag-based station access not yet implemented (planned for future)

---

## Verification Checklist

Before considering tag-related work complete:

- [ ] All tags defined in `SkillTags.cs` (no string literals)
- [ ] `dotnet build` succeeds with 0 warnings
- [ ] Tag hierarchy documented in this file
- [ ] C# references provided for all tag categories
- [ ] Content templates include tag sections
- [ ] `GetAllClassTags()` implemented and tested
- [ ] Station access uses tag-based control
- [ ] Multiclass crafters can use multiple stations
- [ ] Apprentices/partners can share workshop space
- [ ] Recipe access uses tag-based control
- [ ] Before/after examples documented for migration

---

**Last Updated:** 2025-01-27
**Related Plan:** Documentation Quality Alignment - Tasks 1.1, 1.2
**Status:** Phase 1 Foundation - In Progress (2/5 tasks complete)
