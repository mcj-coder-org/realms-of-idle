---
title: '[System Name] - Game Design Document'
type: 'system-overview'
summary: 'Brief one-line description of system scope and purpose'
version: '1.0.0'
status: 'active' # active, deprecated, proposed, draft
created: 'YYYY-MM-DD'
updated: 'YYYY-MM-DD'
subjects: [keyword1, keyword2, keyword3] # Core topics covered
dependencies: [other-system-gdd.md] # Other GDDs this references
---

# [System Name] - Authoritative Game Design

## Executive Summary

[2-3 paragraph overview explaining what this system does, why it exists, and how it fits into the overall game design]

**This document resolves:**

- [Critical question or design decision #1]
- [Critical question or design decision #2]
- [Critical question or design decision #3]

**Design Philosophy**: [Core principle guiding this system's design - 1-2 sentences]

---

## 1. Terminology {#terminology}

Define all key terms used in this document to prevent confusion:

| Term             | Definition           | Example             |
| ---------------- | -------------------- | ------------------- |
| [Technical Term] | [Precise definition] | [Concrete example]  |
| [Game Mechanic]  | [How it works]       | [Usage in gameplay] |

**Common Confusions** (if applicable):

- ❌ **"[Term A]" is NOT "[Term B]"** - [Clarification of difference]
- ✅ **Use "[Correct Term]" for [Concept]** - [When to use it]

---

## 2. Core Mechanics {#core-mechanics}

### 2.1 [Primary Mechanic] {#primary-mechanic}

[Detailed explanation of the main mechanic this system implements]

**How It Works**:

1. [Step 1 in the process]
2. [Step 2 in the process]
3. [Step 3 in the process]

**Example Flow**:

```
Player Action: [What player does]
  → System Response: [What system calculates/triggers]
  → Outcome: [Result visible to player]
  → Feedback: [UI/game state update]
```

### 2.2 [Secondary Mechanic] {#secondary-mechanic}

[Additional mechanics that support or interact with primary mechanic]

---

## 3. Formulas & Calculations {#formulas}

### 3.1 [Formula Name] {#formula-name}

**Formula**:

```
Result = BaseValue × Modifier + Bonus
```

**Where**:

- `BaseValue`: [Definition and source]
- `Modifier`: [What affects this multiplier]
- `Bonus`: [Additive bonuses]

**Example Calculation**:

```
BaseValue = 100
Modifier = 1.5 (from skill)
Bonus = 25 (from equipment)

Result = 100 × 1.5 + 25 = 175
```

**Edge Cases**:

- Minimum cap: [Minimum value, if any]
- Maximum cap: [Maximum value, if any]
- Special handling: [Any exceptional conditions]

---

## 4. System States & Transitions {#states}

_If system involves state machines or status changes:_

**State Diagram**:

```
[Initial State]
  ↓ (trigger: [condition])
[Active State]
  ↓ (trigger: [condition])
[Final State]
```

**State Definitions**:

| State        | Description             | Valid Transitions          |
| ------------ | ----------------------- | -------------------------- |
| [State Name] | [What this state means] | → [Next State] via [Event] |

---

## 5. Integration Points {#integration}

How this system connects to other game systems:

### 5.1 Integration with [Other System]

**Dependencies**:

- This system REQUIRES: [What it needs from other systems]
- This system PROVIDES: [What other systems consume from this]

**Event Flow**:

- [Event A] triggers → [This System Response] → outputs [Event B]

### 5.2 Data Exchange

**Inputs**: [What data this system receives]
**Outputs**: [What data this system produces]
**Shared State**: [Any global state modified by this system]

---

## 6. Examples {#examples}

### 6.1 Example 1: [Common Scenario]

**Setup**: [Initial conditions]

**Process**:

1. [Step-by-step walkthrough]
2. [With actual numbers/values]
3. [Showing all calculations]

**Result**: [Final outcome]

**Takeaway**: [What this example demonstrates]

### 6.2 Example 2: [Edge Case]

**Setup**: [Unusual or complex conditions]

**Process**: [How system handles this case]

**Result**: [Outcome]

**Takeaway**: [Important insight from edge case]

---

## 7. Implementation Notes {#implementation}

### 7.1 Data Structures

_Pseudocode or structured description of key data:_

```csharp
public class [SystemComponent]
{
    public string Id { get; set; }
    public int Value { get; set; }
    public Dictionary<string, object> Properties { get; set; }

    public [ReturnType] ProcessAction([Parameters])
    {
        // Core logic summary
        return result;
    }
}
```

### 7.2 Complexity Ratings

| Component     | Implementation Complexity | Notes                   |
| ------------- | ------------------------- | ----------------------- |
| [Component A] | Low (2/5)                 | [Why it's simple]       |
| [Component B] | High (4/5)                | [What makes it complex] |

---

## 8. Design Decisions Record {#decisions}

### 8.1 [Decision Name]

**Decision**: [What was chosen]

**Rationale**:

- [Reason 1]
- [Reason 2]
- [Reason 3]

**Alternatives Considered**:

- **Option A**: [Why not chosen]
- **Option B**: [Why not chosen]

**Trade-offs**:

- ✅ Advantage: [Benefit of chosen approach]
- ❌ Disadvantage: [Cost of chosen approach]
- ⚖️ Mitigation: [How disadvantage is addressed]

---

## 9. Open Questions {#open-questions}

_Track unresolved design questions:_

| ID  | Question                   | Proposed Answer | Status      |
| --- | -------------------------- | --------------- | ----------- |
| 1.1 | [Specific design question] | [Suggestion]    | ✅ Resolved |
| 1.2 | [Another question]         | [Suggestion]    | ⏳ Pending  |

---

## 10. Cross-References {#cross-references}

**Related Documents**:

- **[Other System GDD](../other-system/index.md)** - [How it relates]
- **[Content Category](../../content/category/index.md)** - [Examples implementing this system]

**Implemented By** (content examples):

- [Class Name](../../content/classes/category/name/index.md) - [How it uses this system]
- [Skill Name](../../content/skills/type/name/index.md) - [How it implements mechanics]

---

## 11. Version History {#version-history}

| Version | Date       | Changes                            | Author |
| ------- | ---------- | ---------------------------------- | ------ |
| 1.0.0   | YYYY-MM-DD | Initial GDD                        | [Name] |
| 1.1.0   | YYYY-MM-DD | Added [feature], clarified [topic] | [Name] |

---

## Appendix A: Detailed Tables

_Large reference tables, lookup charts, or comprehensive lists that support the main document_

---

## Appendix B: [Additional Reference Material]

_Supplementary information that doesn't fit in main flow_

---

_Template for system GDD documentation - Part of 002-doc-migration-rationalization_
