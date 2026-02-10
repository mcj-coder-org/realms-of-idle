# Tags Index

This index provides a comprehensive lookup of all tags in the game system, their C# references, and which content uses them.

## Purpose

- **Tag lookup**: Find the C# reference for any tag
- **Usage tracking**: See which classes, skills, recipes use each tag
- **Access control**: Understand tag depth and class permissions
- **Cross-reference**: Navigate from tags to content that grants/requires them

## Crafting Tags

### Smithing

| Tag Path                       | C# Reference                             | Depth | Classes With Access | Content Usage           |
| ------------------------------ | ---------------------------------------- | ----- | ------------------- | ----------------------- |
| `Crafting/Smithing`            | `SkillTags.Crafting.Smithing.Value`      | 2     | Blacksmith          | Base smithing access    |
| `Crafting/Smithing/Apprentice` | `SkillTags.Crafting.Smithing.Apprentice` | 3     | Blacksmith (Lv10+)  | Apprentice-tier recipes |
| `Crafting/Smithing/Iron`       | `SkillTags.Crafting.Smithing.Iron`       | 3     | Blacksmith          | Iron working            |
| `Crafting/Smithing/Weapon`     | `SkillTags.Crafting.Smithing.Weapon`     | 3     | Weaponsmith         | Weapon crafting         |
| `Crafting/Smithing/Armor`      | `SkillTags.Crafting.Smithing.Armor`      | 3     | Armorsmith          | Armor crafting          |
| `Crafting/Smithing/Shield`     | `SkillTags.Crafting.Smithing.Shield`     | 3     | Blacksmith          | Shield crafting         |
| `Crafting/Smithing/Tools`      | `SkillTags.Crafting.Smithing.Tools`      | 3     | Blacksmith          | Tool crafting           |

### Leatherworking

| Tag Path                        | C# Reference                              | Depth | Classes With Access | Content Usage              |
| ------------------------------- | ----------------------------------------- | ----- | ------------------- | -------------------------- |
| `Crafting/Leatherworking`       | `SkillTags.Crafting.Leatherworking.Value` | 2     | Leatherworker       | Base leatherworking access |
| `Crafting/Leatherworking/Armor` | `SkillTags.Crafting.Leatherworking.Armor` | 3     | Leatherworker       | Leather armor crafting     |

### Woodworking

| Tag Path                          | C# Reference                                | Depth | Classes With Access    | Content Usage           |
| --------------------------------- | ------------------------------------------- | ----- | ---------------------- | ----------------------- |
| `Crafting/Woodworking`            | `SkillTags.Crafting.Woodworking.Value`      | 2     | Carpenter              | Base woodworking access |
| `Crafting/Woodworking/Weapon`     | `SkillTags.Crafting.Woodworking.Weapon`     | 3     | Carpenter (Apprentice) | Wooden weapon crafting  |
| `Crafting/Woodworking/Shield`     | `SkillTags.Crafting.Woodworking.Shield`     | 3     | Carpenter (Apprentice) | Wooden shield crafting  |
| `Crafting/Woodworking/Furniture`  | `SkillTags.Crafting.Woodworking.Furniture`  | 3     | Carpenter (Journeyman) | Furniture crafting      |
| `Crafting/Woodworking/Structural` | `SkillTags.Crafting.Woodworking.Structural` | 3     | Carpenter (Master)     | Building components     |

### Stations

| Tag Path                          | C# Reference                                        | Depth | Classes With Access | Station Type           |
| --------------------------------- | --------------------------------------------------- | ----- | ------------------- | ---------------------- |
| `Crafting/Smithing/Forge`         | `SkillTags.Crafting.Stations.Forge`                 | 3     | Blacksmith          | Forge station          |
| `Crafting/Leatherworking/Station` | `SkillTags.Crafting.Stations.LeatherworkingStation` | 3     | Leatherworker       | Leatherworking station |
| `Crafting/Woodworking/Bench`      | `SkillTags.Crafting.Stations.CarpenterBench`        | 3     | Carpenter           | Carpenter's bench      |
| `Crafting/Tailoring/Table`        | `SkillTags.Crafting.Stations.TailorsTable`          | 3     | Tailor              | Tailor's table         |
| `Crafting/Cooking/Kitchen`        | `SkillTags.Crafting.Stations.Kitchen`               | 3     | Cook                | Kitchen station        |
| `Crafting/Jewelry/Station`        | `SkillTags.Crafting.Stations.JewelryStation`        | 3     | Jeweler             | Jewelry station        |

### Magic Stations

| Tag Path                   | C# Reference                                  | Depth | Classes With Access | Station Type       |
| -------------------------- | --------------------------------------------- | ----- | ------------------- | ------------------ |
| `Magic/Enchantment/Table`  | `SkillTags.Crafting.Stations.EnchantersTable` | 3     | Enchanter           | Enchanter's table  |
| `Magic/Alchemy/Laboratory` | `SkillTags.Crafting.Stations.Laboratory`      | 3     | Alchemist           | Laboratory station |

### Wildcards

| Tag Path                | C# Reference                              | Depth | Purpose                      |
| ----------------------- | ----------------------------------------- | ----- | ---------------------------- |
| `Crafting/*/Apprentice` | `SkillTags.Crafting.Wildcards.Apprentice` | 3     | Any apprentice-tier crafting |
| `Crafting/*/Journeyman` | `SkillTags.Crafting.Wildcards.Journeyman` | 3     | Any journeyman-tier crafting |

## Workshop and Station Model

### Workshops (Buildings)

**Workshops are settlement buildings** where crafting occurs. They are not tag-restricted and any character can enter them.

**Workshop Types:**

- Blacksmith's Workshop
- Carpenter's Workshop
- Leatherworker's Workshop
- Tailor's Workshop
- Enchanter's Laboratory
- Alchemist's Laboratory
- Jeweler's Workshop
- Cook's Kitchen

**Multiclass Support:** Characters with multiple crafting classes can outfit one workshop with stations for all their crafts. For example, a Blacksmith/Carpenter/Enchanter can equip a workshop with a Forge, Carpenter's Bench, and Enchanter's Table.

### Stations (Equipment)

**Stations are the crafting equipment** within workshops. Station access is controlled by crafting tags.

**Station Access Model:**

- Characters must have the required crafting tag to use a station
- Tags are granted by class acceptance (e.g., Blacksmith class grants `Crafting/Smithing`)
- Multiclass characters can use all stations for which they have tags
- Apprentices and partners can share workshop space and stations

**Station Examples:**

| Station                | Required Tag              | Workshop Location        | Classes That Can Use |
| ---------------------- | ------------------------- | ------------------------ | -------------------- |
| Forge                  | `Crafting/Smithing`       | Blacksmith's Workshop    | Blacksmith           |
| Carpenter's Bench      | `Crafting/Woodworking`    | Carpenter's Workshop     | Carpenter            |
| Leatherworking Station | `Crafting/Leatherworking` | Leatherworker's Workshop | Leatherworker        |
| Enchanter's Table      | `Magic/Enchantment`       | Enchanter's Laboratory   | Enchanter            |
| Laboratory             | `Magic/Alchemy`           | Alchemist's Laboratory   | Alchemist            |

**Key Design Principle:** Workshops are flexible spaces. One workshop can support multiple crafts through multiple stations, enabling multiclass crafters to work efficiently without managing multiple buildings.

## Material Tags

### Metal

| Tag Path                | C# Reference                      | Depth | Classes With Access | Content Usage     |
| ----------------------- | --------------------------------- | ----- | ------------------- | ----------------- |
| `Material/Metal`        | `SkillTags.Material.Metal.Value`  | 2     | Blacksmith, Jeweler | Base metalworking |
| `Material/Metal/Iron`   | `SkillTags.Material.Metal.Iron`   | 3     | Blacksmith          | Iron recipes      |
| `Material/Metal/Copper` | `SkillTags.Material.Metal.Copper` | 3     | Blacksmith, Jeweler | Copper recipes    |
| `Material/Metal/Steel`  | `SkillTags.Material.Metal.Steel`  | 3     | Blacksmith          | Steel recipes     |
| `Material/Metal/Bronze` | `SkillTags.Material.Metal.Bronze` | 3     | Blacksmith, Jeweler | Bronze recipes    |
| `Material/Metal/Silver` | `SkillTags.Material.Metal.Silver` | 3     | Jeweler             | Silver recipes    |
| `Material/Metal/Gold`   | `SkillTags.Material.Metal.Gold`   | 3     | Jeweler             | Gold recipes      |

### Wood

| Tag Path              | C# Reference                    | Depth | Classes With Access | Content Usage    |
| --------------------- | ------------------------------- | ----- | ------------------- | ---------------- |
| `Material/Wood`       | `SkillTags.Material.Wood.Value` | 2     | Carpenter           | Base woodworking |
| `Material/Wood/Oak`   | `SkillTags.Material.Wood.Oak`   | 3     | Carpenter           | Oak recipes      |
| `Material/Wood/Pine`  | `SkillTags.Material.Wood.Pine`  | 3     | Carpenter           | Pine recipes     |
| `Material/Wood/Maple` | `SkillTags.Material.Wood.Maple` | 3     | Carpenter           | Maple recipes    |
| `Material/Wood/Ebony` | `SkillTags.Material.Wood.Ebony` | 3     | Carpenter           | Ebony recipes    |

### Leather

| Tag Path                  | C# Reference                        | Depth | Classes With Access | Content Usage          |
| ------------------------- | ----------------------------------- | ----- | ------------------- | ---------------------- |
| `Material/Leather`        | `SkillTags.Material.Leather.Value`  | 2     | Leatherworker       | Base leatherworking    |
| `Material/Leather/Hide`   | `SkillTags.Material.Leather.Hide`   | 3     | Leatherworker       | Raw hide recipes       |
| `Material/Leather/Tanned` | `SkillTags.Material.Leather.Tanned` | 3     | Leatherworker       | Tanned leather recipes |

### Fabric

| Tag Path                 | C# Reference                       | Depth | Classes With Access | Content Usage       |
| ------------------------ | ---------------------------------- | ----- | ------------------- | ------------------- |
| `Material/Fabric`        | `SkillTags.Material.Fabric.Value`  | 2     | Tailor, Weaver      | Base fabric working |
| `Material/Fabric/Cotton` | `SkillTags.Material.Fabric.Cotton` | 3     | Tailor, Weaver      | Cotton recipes      |
| `Material/Fabric/Linen`  | `SkillTags.Material.Fabric.Linen`  | 3     | Tailor, Weaver      | Linen recipes       |
| `Material/Fabric/Wool`   | `SkillTags.Material.Fabric.Wool`   | 3     | Tailor, Weaver      | Wool recipes        |

### Stone

| Tag Path                 | C# Reference                       | Depth | Classes With Access | Content Usage     |
| ------------------------ | ---------------------------------- | ----- | ------------------- | ----------------- |
| `Material/Stone`         | `SkillTags.Material.Stone.Value`   | 2     | Architect, Mason    | Base stoneworking |
| `Material/Stone/Granite` | `SkillTags.Material.Stone.Granite` | 3     | Architect, Mason    | Granite recipes   |
| `Material/Stone/Marble`  | `SkillTags.Material.Stone.Marble`  | 3     | Architect, Mason    | Marble recipes    |

### Other Materials

| Tag Path        | C# Reference                    | Depth | Classes With Access | Content Usage |
| --------------- | ------------------------------- | ----- | ------------------- | ------------- |
| `Material/Bone` | `SkillTags.Material.Bone.Value` | 2     | Bonecarver          | Bone working  |
| `Material/Horn` | `SkillTags.Material.Horn.Value` | 2     | Bonecarver          | Horn working  |
| `Material/Silk` | `SkillTags.Material.Silk.Value` | 2     | Weaver, Tailor      | Silk recipes  |

## Combat Tags

### Melee

| Tag Path              | C# Reference                    | Depth | Classes With Access   | Content Usage     |
| --------------------- | ------------------------------- | ----- | --------------------- | ----------------- |
| `Combat/Melee`        | `SkillTags.Combat.Melee.Value`  | 2     | Warrior, Guard, Scout | Base melee combat |
| `Combat/Melee/Sword`  | `SkillTags.Combat.Melee.Sword`  | 3     | Warrior, Knight       | Sword combat      |
| `Combat/Melee/Axe`    | `SkillTags.Combat.Melee.Axe`    | 3     | Warrior               | Axe combat        |
| `Combat/Melee/Mace`   | `SkillTags.Combat.Melee.Mace`   | 3     | Warrior, Guard        | Mace combat       |
| `Combat/Melee/Dagger` | `SkillTags.Combat.Melee.Dagger` | 3     | Scout, Assassin       | Dagger combat     |

### Defense

| Tag Path                | C# Reference                      | Depth | Classes With Access | Content Usage |
| ----------------------- | --------------------------------- | ----- | ------------------- | ------------- |
| `Combat/Defense`        | `SkillTags.Combat.Defense.Value`  | 2     | Guard, Knight       | Base defense  |
| `Combat/Defense/Shield` | `SkillTags.Combat.Defense.Shield` | 3     | Guard, Knight       | Shield usage  |
| `Combat/Defense/Dodge`  | `SkillTags.Combat.Defense.Dodge`  | 3     | Scout, Assassin     | Dodge combat  |

### Ranged

| Tag Path        | C# Reference              | Depth | Classes With Access | Content Usage |
| --------------- | ------------------------- | ----- | ------------------- | ------------- |
| `Combat/Ranged` | `SkillTags.Combat.Ranged` | 2     | Archer, Scout       | Ranged combat |

## Gathering Tags

> **Status:** Empty (tags to be added)

Planned gathering tags:

- `Gathering/Mining` - Ore and mineral extraction
- `Gathering/Herbalism` - Plant gathering
- `Gathering/Hunting` - Animal tracking
- `Gathering/Fishing` - Aquatic resources
- `Gathering/Lumbering` - Wood harvesting

## Tag Depth Reference

| Depth | Description            | Example                    | Access Control        |
| ----- | ---------------------- | -------------------------- | --------------------- |
| 1     | Top-level category     | `Crafting`                 | Base category access  |
| 2     | Subcategory/Profession | `Crafting/Smithing`        | Class-specific access |
| 3     | Specialization/Feature | `Crafting/Smithing/Weapon` | Specialized access    |
| 4+    | Granular detail        | _(not currently used)_     | Future expansion      |

## How to Use This Index

### For Content Creators

1. **Find your tags**: Search this index for tags relevant to your content
2. **Get C# references**: Copy the C# reference for use in code
3. **Check access**: Verify which classes can access your content
4. **Update documentation**: Add Tags section to your content docs

### For Developers

1. **Tag lookup**: Find the exact tag path and C# reference
2. **Access control**: Understand which classes grant which tags
3. **Code references**: Use strongly typed tags (e.g., `SkillTags.Crafting.Smithing.Value`)
4. **Validation**: Verify tag depth and hierarchy

### For Auditors

1. **Cross-reference**: Verify content documentation matches tag system
2. **Completeness**: Check all tags have corresponding content
3. **Consistency**: Ensure C# references compile and work
4. **Coverage**: Track which tags are used/unused

## Maintenance

This index should be updated when:

- New tags are added to `SkillTags.cs`
- New classes are created with `GrantedTags`
- Content tags are added/modified
- Tag hierarchy changes

**Automated update process:**

```bash
# Extract tags from SkillTags.cs (manual or script)
grep -E "public static readonly SkillTag" src/CozyFantasyRpg/Shared/SkillTags.cs

# Scan class definitions for GrantedTags
grep -r "GrantedTags" src/CozyFantasyRpg/Content/Classes/

# Update index files manually or with script
```

## Related Documentation

- [Tag System Documentation](../../systems/content/tag-system.md)
- [Content Template](../_templates/content-template.md)
- [Content Curator Persona](../../../standards/content-curator-persona.md)
- [SkillTags.cs](../../../../src/CozyFantasyRpg/Shared/SkillTags.cs)

---

**Last Updated:** 2026-01-28
**Maintained By:** Content Curator persona
**Source:** `src/CozyFantasyRpg/Shared/SkillTags.cs`
