<!-- ADAPTATION REQUIRED -->
<!-- This file was migrated from source but needs manual review: -->
<!-- - Update terminology (dormant classes, XP split, etc.) -->
<!-- - Align with current GDD architecture -->
<!-- - Add missing sections as needed -->
<!-- - Update frontmatter with correct gdd_ref -->

---

title: Data Tables & Reference Sheets
type: system
summary: Quick reference tables for all numerical game values

---

# Data Tables & Reference Sheets

## Time & World Values

| Parameter                    | Value                |
| ---------------------------- | -------------------- |
| Real-time to game-time ratio | 1:24                 |
| Real minutes per game hour   | 2.5                  |
| Daytime hours                | 6am - 8pm (14 hours) |
| Nighttime hours              | 8pm - 6am (10 hours) |
| Full rest duration (real)    | 2-3 minutes          |
| Nap duration (real)          | ~30-60 seconds       |

---

## Currency

| Denomination | Value | Conversion             |
| ------------ | ----- | ---------------------- |
| Copper       | 1     | Base unit              |
| Silver       | 10    | 10 copper              |
| Gold         | 100   | 10 silver / 100 copper |

---

## XP Scaling Formula

```
XP needed = Base XP × (1 + (total_levels × 0.15))
```

| Total Levels | Multiplier |
| ------------ | ---------- |
| 0            | 1.0x       |
| 5            | 1.75x      |
| 10           | 2.5x       |
| 15           | 3.25x      |
| 20           | 4.0x       |

---

## Skill Offers Per Level-Up

| Event                 | Min | Max |
| --------------------- | --- | --- |
| Normal level-up       | 0   | 3   |
| Class acceptance      | 2   | 3   |
| Milestone (10, 20...) | 1   | 5   |

---

## Consolidation Scaling

| Tier     | XP Multiplier | Skill Tier Bonus |
| -------- | ------------- | ---------------- |
| 0 (Base) | 1.0x          | +0%              |
| 1        | 1.5x          | +15%             |
| 2        | 2.0x          | +30%             |
| 3        | 3.0x          | +50%             |

---

## Loyalty Scale

| Level     | Value  | Starting Point         |
| --------- | ------ | ---------------------- |
| Hostile   | 0-10   | —                      |
| Reluctant | 11-25  | Prisoners, surrendered |
| Neutral   | 26-50  | New recruits           |
| Friendly  | 51-70  | Established members    |
| Loyal     | 71-90  | Trusted companions     |
| Devoted   | 91-100 | Life-bonded            |

---

## Faction Opinion Scale

| Level      | Value  |
| ---------- | ------ |
| Allied     | 90-100 |
| Friendly   | 70-89  |
| Neutral    | 40-69  |
| Unfriendly | 20-39  |
| Hostile    | 1-19   |
| Hated      | 0      |

---

## Smuggling Profit Margins

| Contraband             | Risk      | Markup    |
| ---------------------- | --------- | --------- |
| Untaxed legal goods    | Low       | +5-10%    |
| Stolen goods (common)  | Low-Med   | +10-20%   |
| Stolen goods (notable) | Med       | +20-30%   |
| Drugs (mild)           | Med       | +25-35%   |
| Drugs (hard)           | Med-High  | +40-60%   |
| Poisons                | High      | +50-75%   |
| Illegal weapons        | High      | +50-75%   |
| Body parts             | High      | +60-100%  |
| Slaves                 | Very High | +100-200% |

---

## Bribed Guard Coverage

| Settlement Size | Coverage |
| --------------- | -------- |
| Village         | 50-70%   |
| Town            | 30-50%   |
| City            | 20-40%   |
| Capital         | 10-20%   |

---

## Corruption Tolerance Effects

| Level    | Black Market % | Bribed Guards |
| -------- | -------------- | ------------- |
| None     | 1-2%           | 0%            |
| Low      | 5-10%          | 10%           |
| Moderate | 10-20%         | 30%           |
| High     | 25-35%         | 60%           |
| Open     | 40%+           | 90%           |

---

## Material Quality Tiers

| Tier      | Source              |
| --------- | ------------------- |
| Crude     | Unskilled gathering |
| Common    | Basic gathering     |
| Quality   | Skilled gathering   |
| Fine      | Expert gathering    |
| Superior  | Master gathering    |
| Legendary | Unique sources      |

---

## Refinement Output

| Refiner Level | Max Output |
| ------------- | ---------- |
| Apprentice    | Quality    |
| Journeyman    | Fine       |
| Master        | Superior   |
| Artificer     | Legendary  |

---

## Crafting Output Quality

| Tier       | Value Multiplier |
| ---------- | ---------------- |
| Crude      | 0.5x             |
| Common     | 1x               |
| Quality    | 1.5x             |
| Fine       | 2x               |
| Superior   | 3x               |
| Masterwork | 5x               |
| Legendary  | 10x+             |

---

## Tool Quality Bonuses

| Quality    | Bonus |
| ---------- | ----- |
| Crude      | -10%  |
| Common     | 0%    |
| Quality    | +5%   |
| Fine       | +10%  |
| Superior   | +15%  |
| Masterwork | +20%  |
| Legendary  | +30%  |

---

## Workspace Bonuses

| Tier            | Bonus |
| --------------- | ----- |
| Improvised      | -20%  |
| Basic           | 0%    |
| Standard        | +10%  |
| Professional    | +20%  |
| Master Workshop | +30%  |
| Legendary       | +40%  |

---

## Assistant Effects

| Skill Level | Speed | Quality |
| ----------- | ----- | ------- |
| Unskilled   | +20%  | 0%      |
| Basic       | +30%  | +5%     |
| Apprentice  | +40%  | +10%    |
| Journeyman  | +50%  | +15%    |
| Master      | +75%  | +20%    |

---

## Crafting Time

| Recipe Tier | Time          |
| ----------- | ------------- |
| Basic       | Minutes       |
| Apprentice  | 30 min - 1 hr |
| Journeyman  | 1-4 hours     |
| Master      | 4-12 hours    |
| Legendary   | Days          |
| Artificer   | Days-weeks    |

---

## Trade Class Progression

| Milestone       | Trigger                           |
| --------------- | --------------------------------- |
| Trader unlock   | 10+ barters                       |
| Merchant unlock | 1000+ silver value OR 200+ profit |
| Specialization  | Reach Merchant                    |
| Guildmaster     | All specialties at Master         |

---

## Caravaner Upgrades

| Level | Unlocks                     |
| ----- | --------------------------- |
| 1-3   | Pack animal, small cart     |
| 4-6   | Wagon, 1-2 guards           |
| 7-9   | Large wagon, 3-4 guards     |
| 10-12 | Multiple wagons, 5-6 guards |
| 13-15 | Armored wagon, outriders    |
| 16-18 | Convoy                      |
| 19-20 | Trade fleet                 |

---

## Ransom Values

| Level | Base Value        |
| ----- | ----------------- |
| 1-5   | 25-75 gold        |
| 6-10  | 100-300 gold      |
| 11-15 | 400-800 gold      |
| 16-20 | 1,000-2,500 gold  |
| 21-25 | 3,000-6,000 gold  |
| 26-30 | 7,500-15,000 gold |
| 31+   | 20,000+ gold      |

---

## Undead Evolution

| Age     | L1-10    | L11-20     | L21-30   | L31+         |
| ------- | -------- | ---------- | -------- | ------------ |
| Fresh   | Zombie   | Ghoul      | Draugr   | Elite        |
| Aged    | Skeleton | Sk. Knight | Sk. Lord | Death Knight |
| Ancient | Ghost    | Phantom    | Spectre  | Wraith       |

---

## Weapon Stats (Relative)

| Category  | Speed | Damage |
| --------- | ----- | ------ |
| Unarmed   | 5     | 1      |
| Daggers   | 5     | 2      |
| Swords    | 3     | 3      |
| Axes      | 2     | 4      |
| Maces     | 2     | 4      |
| Spears    | 3     | 3      |
| Bows      | 3     | 3      |
| Crossbows | 1     | 5      |

---

## Mastery Multipliers

| Level     | Damage |
| --------- | ------ |
| Untrained | 0.7x   |
| Familiar  | 1.0x   |
| Skilled   | 1.15x  |
| Expert    | 1.3x   |
| Master    | 1.5x   |

---

## Enchantment Tiers

| Tier      | Bonus | Unlock Level |
| --------- | ----- | ------------ |
| Minor     | +5%   | 1            |
| Moderate  | +15%  | 5            |
| Greater   | +25%  | 10           |
| Superior  | +40%  | 15           |
| Legendary | +60%+ | 20           |

---

## Morale Thresholds

| Level    | Value  |
| -------- | ------ |
| High     | 80-100 |
| Normal   | 50-79  |
| Shaken   | 30-49  |
| Breaking | 10-29  |
| Broken   | 0-9    |

---

## Settlement Types

| Type        | Corruption   | BM Size  | BM Value  |
| ----------- | ------------ | -------- | --------- |
| Capital     | Low-Mod      | Small    | Very High |
| Major City  | Mod          | Medium   | High      |
| Trade Hub   | Mod-High     | Medium   | High      |
| Border Town | High         | Large    | Medium    |
| Frontier    | High-Corrupt | V. Large | Low-Med   |
| Port City   | Mod-High     | Large    | High      |
| Religious   | Low-Clean    | V. Small | Low       |
| Mining Town | Mod-High     | Medium   | Medium    |
| Military    | Low          | Small    | Low       |

---

## Fair Schedule (Annual)

| Month | Event                |
| ----- | -------------------- |
| 1     | New Year Festival    |
| 3     | Spring Planting Fair |
| 5     | Merchant Exposition  |
| 7     | Midsummer Tournament |
| 9     | Harvest Festival     |
| 11    | Artisan Showcase     |
