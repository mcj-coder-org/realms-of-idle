---
title: Host
gdd_ref: systems/core-progression-system-gdd.md#general
---

# Host

## Lore

The Host represents the foundation of service - understanding that providing comfort, care, and hospitality to others creates value and strengthens community bonds. Before becoming a renowned innkeeper, skilled server, or expert housekeeper, every service professional first learns to host: anticipating needs, creating welcoming environments, and ensuring others feel cared for.

Hosts emerge wherever people gather. The farmer who offers travelers a place by the fire. The neighbor who brings soup to the sick. The elder who coordinates community celebrations. Service flows through them, transforming strangers into guests, spaces into sanctuaries, and simple gestures into meaningful connections.

The Hospitality Guild teaches hosting fundamentals to all who seek service careers. Reading social cues, maintaining pleasant spaces, handling complaints gracefully, and creating comfort with limited resources. These skills form the foundation upon which all specialized service professions are built.

## Mechanics

### Requirements

| Requirement    | Value                       |
| -------------- | --------------------------- |
| Unlock Trigger | 100 service-related actions |
| Primary Tags   | `Service`                   |
| Tier           | 1 (Foundation)              |
| Tag Depth      | 1 level                     |

### Unlock Requirements

**Actions tracked for class unlock**:

Host tracks actions tagged with `Service` (and its children):

- Serve food or drink to others
- Provide lodging or shelter
- Clean or maintain welcoming spaces
- Assist guests with requests
- Create comfortable environments
- Greet and welcome visitors
- Handle customer concerns

**Threshold:** 100 tracked actions (guaranteed unlock) or early unlock via probability

### Starting Skills

Skills automatically awarded when accepting this class:

| Skill          | Type            | Description                                |
| -------------- | --------------- | ------------------------------------------ |
| Service Mind   | Mechanic Unlock | Intuitively understand customer needs      |
| Welcoming Aura | Passive         | +5% customer satisfaction in your presence |

### XP Progression

Uses Tier 1 formula: `XP = 100 × 1.5^(level - 1)`

| Level | XP to Next | Cumulative |
| ----- | ---------- | ---------- |
| 1→2   | 100        | 100        |
| 5→6   | 506        | 1,131      |
| 10→11 | 3,844      | 6,513      |

### Tracked Actions for XP

Host gains XP from actions tagged with `Service` (and its children):

| Action Category       | Specific Actions                                  | Example Tag                   | XP Value             |
| --------------------- | ------------------------------------------------- | ----------------------------- | -------------------- |
| Customer Service      | Greet guests, fulfill requests, handle issues     | `Service`                     | 5-15 per action      |
| Space Maintenance     | Clean areas, arrange furniture, create comfort    | `Service`                     | 3-10 per task        |
| Basic Food Service    | Serve simple meals or beverages                   | `Service/Hospitality/Food`    | 5-12 per serve       |
| Basic Lodging         | Prepare basic sleeping areas                      | `Service/Hospitality/Lodging` | 5-12 per task        |
| Problem Resolution    | Resolve complaints, fix issues, soothe upset      | `Service`                     | 10-25 per issue      |
| Relationship Building | Build rapport with regulars, remember preferences | `Service`                     | 8-20 per interaction |

**Note**: Host can perform actions at any depth of Service tag tree, but specializations (Tier 2) are more efficient at specific branches.

### Progression Paths

Host can transition to the following Tier 2 classes upon reaching 5,000 XP:

| Tier 2 Class | Focus                               | Tag Branch                    |
| ------------ | ----------------------------------- | ----------------------------- |
| Innkeeper    | Hospitality management (generalist) | `Service/Hospitality`         |
| Server       | Food service specialist             | `Service/Hospitality/Food`    |
| Housekeeper  | Lodging service specialist          | `Service/Hospitality/Lodging` |

### Tag Depth Access

As a Tier 1 class, Host tracks actions at depth 1 (`Service`) and can access children tags but gains less XP than specialists:

| Tag                           | Host XP | Specialist XP      | Notes                         |
| ----------------------------- | ------- | ------------------ | ----------------------------- |
| `Service`                     | 1.0x    | N/A                | Full XP (Tier 1 tag)          |
| `Service/Hospitality`         | 0.75x   | 1.0x (Innkeeper)   | Reduced XP for generalist     |
| `Service/Hospitality/Food`    | 0.5x    | 1.0x (Server)      | Reduced XP for non-specialist |
| `Service/Hospitality/Lodging` | 0.5x    | 1.0x (Housekeeper) | Reduced XP for non-specialist |

**Design Principle**: Hosts can do everything service-related, but specialists level faster in their domain.

## Progression

### Specializations

- [Innkeeper](./innkeeper/) - Hospitality management and operations
- [Server](./server/) - Food service excellence
- [Housekeeper](./housekeeper/) - Lodging and facility maintenance

## Related Content

- **Tier System:** [Class Tiers](../../../systems/character/class-tiers.md)
- **Service Systems:** [Hospitality Economy](../../../systems/economy/services.md)
- **See Also:** [Service Classes Index](./index.md)
