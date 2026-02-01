# The Wandering Inn: Class Tag Mapping
## Extracted Classes with Tag Requirements & Evolution Paths

---

## Combat Classes

### [Warrior]
```yaml
tags_primary: [combat.melee]
tags_secondary: [combat.defense, combat.tactical]
acquisition:
  combat.melee: 100
evolution_paths:
  Lv.10: [Knight], [Soldier], [Berserker], [Duelist]
  Lv.20: [Champion], [Veteran], [Slayer], [Blademaster]
  Lv.30: [Warlord], [Weapon Saint], [Destroyer]
```

### [Knight]
```yaml
tags_primary: [combat.melee, combat.defense.shield, combat.defense.armor]
tags_secondary: [social.reputation.faction, utility.service.protect]
acquisition:
  combat.melee: 150
  combat.defense.shield: 100
  social.reputation.faction: 50  # Sworn to a cause/lord
evolution_paths:
  Lv.20: [Knight-Captain], [Oath Knight], [Templar], [Cavalier]
  Lv.30: [Knight-Commander], [Paladin], [Champion of (Faction)]
```

### [Soldier]
```yaml
tags_primary: [combat.melee, combat.tactical.group]
tags_secondary: [combat.defense, utility.service.mercenary]
acquisition:
  combat.melee: 100
  combat.tactical.group: 75
evolution_paths:
  Lv.20: [Veteran], [Sergeant], [Elite Soldier], [Shieldwall Guard]
  Lv.30: [Captain], [Battalion Leader], [War Veteran]
```

### [General]
```yaml
tags_primary: [combat.tactical, social.leadership.army]
tags_secondary: [combat.melee, knowledge.lore.military]
acquisition:
  combat.tactical: 300
  social.leadership.army: 200
  combat.melee: 150
evolution_paths:
  Lv.30: [Grand General], [Warlord], [Marshal]
  Lv.40: [Legendary General], [Conqueror]
```

### [Strategist]
```yaml
tags_primary: [combat.tactical, knowledge.lore.military]
tags_secondary: [social.leadership, knowledge.research]
acquisition:
  combat.tactical: 250
  knowledge.lore.military: 150
evolution_paths:
  Lv.20: [War Strategist], [Battle Planner], [Siege Master]
  Lv.30: [Grand Strategist], [Mastermind]
```

### [Archer]
```yaml
tags_primary: [combat.ranged.bow]
tags_secondary: [gather.hunting, utility.exploration]
acquisition:
  combat.ranged.bow: 100
evolution_paths:
  Lv.10: [Sharpshooter], [Hunter], [Skirmisher], [Longbowman]
  Lv.20: [Sniper], [Ranger], [Arcane Archer]
  Lv.30: [Legendary Marksman]
```

### [Duelist]
```yaml
tags_primary: [combat.melee.sword, combat.defense.evasion]
tags_secondary: [combat.melee.dagger, social.reputation]
acquisition:
  combat.melee.sword: 150
  combat.defense.evasion: 100
  combat.melee.sword.parry: 75
evolution_paths:
  Lv.20: [Blade Dancer], [Fencer], [Swashbuckler]
  Lv.30: [Sword Saint], [Master Duelist]
```

### [Assassin]
```yaml
tags_primary: [combat.melee.dagger, utility.stealth]
tags_secondary: [craft.alchemy.poison, combat.tactical.ambush]
acquisition:
  utility.stealth: 200
  combat.melee.dagger: 150
  combat.tactical.ambush: 100
evolution_paths:
  Lv.20: [Shadow Blade], [Silent Killer], [Poison Master]
  Lv.30: [Deathbringer], [Phantom]
red_class_risk: High if targeting innocents
```

### [Boxer] / [Martial Artist]
```yaml
tags_primary: [combat.melee.unarmed]
tags_secondary: [combat.defense.evasion, combat.melee.unarmed.combo]
acquisition:
  combat.melee.unarmed: 150
evolution_paths:
  Lv.10: [Pugilist], [Brawler], [Monk], [Street Fighter]
  Lv.20: [Iron Fist], [Grandmaster], [Ki Striker]
  Lv.30: [Fist Saint], [Legendary Martial Artist]
```

### [Guard] / [Watchman]
```yaml
tags_primary: [combat.defense, utility.service.protect]
tags_secondary: [combat.melee, social.diplomacy.intimidate]
acquisition:
  combat.defense: 100
  utility.service.protect: 75
evolution_paths:
  Lv.10: [City Guard], [Patrol Leader], [Sentinel]
  Lv.20: [Guard Captain], [Warden], [Protector]
```

### [Pirate]
```yaml
tags_primary: [combat.melee, utility.exploration.travel.sea]
tags_secondary: [social.leadership, combat.ranged]
acquisition:
  utility.exploration.travel.sea: 150
  combat.melee: 100
  social.trade: 50  # Plunder counts
evolution_paths:
  Lv.10: [Corsair], [Buccaneer], [Sea Raider]
  Lv.20: [Pirate Captain], [Dread Pirate]
  Lv.30: [Pirate Lord], [Admiral of the Black]
red_class_risk: Moderate depending on actions
```

---

## Magic Classes

### [Mage]
```yaml
tags_primary: [magic.arcane]
tags_secondary: [magic.elemental, knowledge.lore.magical]
acquisition:
  magic.arcane: 100
evolution_paths:
  Lv.10: [Pyromancer], [Cryomancer], [Aeromancer], [Geomancer], [Generalist Mage]
  Lv.20: [Battle Mage], [Arcane Scholar], [Elementalist]
  Lv.30: [Archmage], [Magus]
```

### [Pyromancer]
```yaml
tags_primary: [magic.elemental.fire]
tags_secondary: [magic.arcane, combat.tactical]
acquisition:
  magic.elemental.fire: 150
  magic.arcane: 75
evolution_paths:
  Lv.20: [Flame Lord], [Inferno Mage], [Hellfire Caster]
  Lv.30: [Archmage of Flames], [Phoenix Mage]
```

### [Cryomancer]
```yaml
tags_primary: [magic.elemental.water, magic.elemental.ice]
tags_secondary: [magic.arcane]
acquisition:
  magic.elemental.water: 100
  magic.elemental.ice: 100
evolution_paths:
  Lv.20: [Frost Lord], [Glacier Mage], [Winter Witch]
  Lv.30: [Archmage of Ice]
```

### [Necromancer]
```yaml
tags_primary: [magic.death, magic.summoning]
tags_secondary: [knowledge.lore.undead, magic.arcane]
acquisition:
  magic.death: 150
  magic.summoning.spirit: 100
evolution_paths:
  Lv.20: [Death Mage], [Bone Lord], [Soul Binder]
  Lv.30: [Lich], [Arch-Necromancer], [Deathless One]
red_class_risk: High in many regions
```

### [Healer]
```yaml
tags_primary: [magic.restoration.healing]
tags_secondary: [knowledge.lore.medical, social.diplomacy]
acquisition:
  magic.restoration.healing: 100
evolution_paths:
  Lv.10: [Medic], [Herbalist-Healer], [Combat Medic]
  Lv.20: [Master Healer], [Lifebringer], [Sanctuary Keeper]
  Lv.30: [Arcane Healer], [Miracle Worker]
```

### [Enchanter]
```yaml
tags_primary: [magic.arcane.enchant]
tags_secondary: [craft.smithing, craft.textile, magic.arcane]
acquisition:
  magic.arcane.enchant: 150
  craft.*: 100  # Any crafting domain
evolution_paths:
  Lv.20: [Runesmith], [Artifcer], [Spellweaver]
  Lv.30: [Arcane Artificer], [Master Enchanter]
```

### [Summoner]
```yaml
tags_primary: [magic.summoning]
tags_secondary: [magic.arcane, social.leadership.command]
acquisition:
  magic.summoning: 150
evolution_paths:
  Lv.20: [Beast Summoner], [Elemental Caller], [Spirit Binder]
  Lv.30: [Arch-Summoner], [Legion Caller]
```

### [Witch]
```yaml
tags_primary: [magic.arcane, craft.alchemy, gather.herbalism]
tags_secondary: [magic.death, knowledge.lore.magical]
acquisition:
  magic.arcane: 100
  craft.alchemy: 100
  gather.herbalism: 75
evolution_paths:
  Lv.20: [Hedge Witch], [Curse Weaver], [Green Witch]
  Lv.30: [Coven Mother], [Arch-Witch]
```

### [Druid]
```yaml
tags_primary: [magic.nature, gather.herbalism]
tags_secondary: [magic.restoration, magic.summoning.creature]
acquisition:
  magic.nature: 150
  gather.herbalism: 100
evolution_paths:
  Lv.20: [Grove Keeper], [Wild Shaper], [Storm Druid]
  Lv.30: [Archdruid], [Nature's Avatar]
```

---

## Crafting Classes

### [Blacksmith] / [Smith]
```yaml
tags_primary: [craft.smithing]
tags_secondary: [gather.mining, social.trade]
acquisition:
  craft.smithing: 100
evolution_paths:
  Lv.10: [Weaponsmith], [Armorsmith], [Toolsmith], [Jeweler]
  Lv.20: [Master Smith], [Forge Master], [Artisan Smith]
  Lv.30: [Legendary Smith], [Divine Forger]
```

### [Alchemist]
```yaml
tags_primary: [craft.alchemy]
tags_secondary: [gather.herbalism, knowledge.research, magic.arcane]
acquisition:
  craft.alchemy: 100
evolution_paths:
  Lv.10: [Apothecary], [Poison Brewer], [Transmuter]
  Lv.20: [Master Alchemist], [Elixirist], [Bomb Maker]
  Lv.30: [Grand Alchemist], [Philosopher]
```

### [Chef] / [Cook]
```yaml
tags_primary: [craft.cooking]
tags_secondary: [gather.hunting, gather.herbalism, social.service]
acquisition:
  craft.cooking: 100
evolution_paths:
  Lv.10: [Line Cook], [Baker], [Brewmaster], [Butcher]
  Lv.20: [Master Chef], [Gourmet], [Feast Maker]
  Lv.30: [Legendary Chef], [Culinary Artist]
```

### [Carpenter] / [Builder]
```yaml
tags_primary: [craft.construction]
tags_secondary: [gather.logging, knowledge.lore.architecture]
acquisition:
  craft.construction: 100
evolution_paths:
  Lv.10: [Woodworker], [Furniture Maker], [Shipwright]
  Lv.20: [Master Carpenter], [Architect], [Siege Engineer]
  Lv.30: [Grand Architect], [Master Builder]
```

### [Weaver] / [Tailor]
```yaml
tags_primary: [craft.textile]
tags_secondary: [social.trade, gather.hunting]  # For leather
acquisition:
  craft.textile: 100
evolution_paths:
  Lv.10: [Clothier], [Leatherworker], [Dyer]
  Lv.20: [Fashion Designer], [Master Tailor], [Enchanted Weaver]
  Lv.30: [Legendary Seamstress], [Threadmaster]
```

### [Fletcher] / [Bowyer]
```yaml
tags_primary: [craft.construction.weapon, combat.ranged.bow]
tags_secondary: [gather.logging, gather.hunting]
acquisition:
  craft.construction.weapon: 75
  combat.ranged.bow: 50
evolution_paths:
  Lv.20: [Master Fletcher], [Arcane Bowyer]
```

---

## Gathering Classes

### [Miner]
```yaml
tags_primary: [gather.mining]
tags_secondary: [craft.smithing, utility.exploration]
acquisition:
  gather.mining: 100
evolution_paths:
  Lv.10: [Prospector], [Gem Cutter], [Deep Miner]
  Lv.20: [Master Miner], [Vein Seeker], [Underground Explorer]
  Lv.30: [Legendary Prospector]
```

### [Herbalist]
```yaml
tags_primary: [gather.herbalism]
tags_secondary: [craft.alchemy, magic.restoration, knowledge.identify]
acquisition:
  gather.herbalism: 100
evolution_paths:
  Lv.10: [Apothecary], [Poison Expert], [Garden Tender]
  Lv.20: [Master Herbalist], [Botanical Scholar]
  Lv.30: [Grand Herbalist], [Nature's Collector]
```

### [Hunter]
```yaml
tags_primary: [gather.hunting]
tags_secondary: [combat.ranged, utility.stealth, utility.exploration]
acquisition:
  gather.hunting: 100
evolution_paths:
  Lv.10: [Tracker], [Trapper], [Monster Hunter], [Skinner]
  Lv.20: [Master Hunter], [Big Game Hunter], [Stalker]
  Lv.30: [Legendary Hunter], [Apex Predator]
```

### [Fisher]
```yaml
tags_primary: [gather.fishing]
tags_secondary: [craft.cooking, utility.exploration.travel.sea]
acquisition:
  gather.fishing: 100
evolution_paths:
  Lv.10: [Angler], [Net Fisher], [Deep Sea Fisher]
  Lv.20: [Master Fisher], [Whaler], [Sea Hunter]
```

### [Farmer] / [Gardener]
```yaml
tags_primary: [gather.farming]
tags_secondary: [craft.cooking, social.trade, gather.herbalism]
acquisition:
  gather.farming: 100
evolution_paths:
  Lv.10: [Crop Farmer], [Orchardist], [Rancher], [Beekeeper]
  Lv.20: [Master Farmer], [Agricultural Expert], [Seed Sage]
  Lv.30: [Legendary Farmer], [Land Steward]
```

### [Logger] / [Woodcutter]
```yaml
tags_primary: [gather.logging]
tags_secondary: [craft.construction, utility.exploration]
acquisition:
  gather.logging: 100
evolution_paths:
  Lv.10: [Lumberjack], [Forest Ranger], [Wood Collector]
  Lv.20: [Master Logger], [Timber Baron]
```

---

## Social Classes

### [Merchant] / [Trader]
```yaml
tags_primary: [social.trade]
tags_secondary: [social.diplomacy, utility.exploration.travel]
acquisition:
  social.trade: 100
evolution_paths:
  Lv.10: [Shopkeeper], [Peddler], [Broker], [Smuggler]
  Lv.20: [Master Merchant], [Caravan Master], [Trade Prince]
  Lv.30: [Magnate], [Trade Lord]
```

### [Bard] / [Singer]
```yaml
tags_primary: [social.performance, magic.arcane]
tags_secondary: [social.diplomacy, knowledge.lore]
acquisition:
  social.performance: 100
evolution_paths:
  Lv.10: [Minstrel], [Storyteller], [Dancer], [Musician]
  Lv.20: [Master Bard], [Spellsinger], [Lorekeeper]
  Lv.30: [Legendary Bard], [Voice of Ages]
```

### [Diplomat]
```yaml
tags_primary: [social.diplomacy]
tags_secondary: [knowledge.lore, social.reputation]
acquisition:
  social.diplomacy: 150
evolution_paths:
  Lv.20: [Ambassador], [Negotiator], [Peace Broker]
  Lv.30: [Grand Diplomat], [Treaty Master]
```

### [Scribe] / [Scholar]
```yaml
tags_primary: [knowledge.lore, knowledge.research]
tags_secondary: [social.diplomacy, magic.arcane]
acquisition:
  knowledge.lore: 100
  knowledge.research: 75
evolution_paths:
  Lv.10: [Copyist], [Historian], [Researcher], [Librarian]
  Lv.20: [Sage], [Archivist], [Loremaster]
  Lv.30: [Grand Scholar], [Keeper of Knowledge]
```

---

## Utility Classes

### [Innkeeper]
```yaml
tags_primary: [utility.service.innkeep]
tags_secondary: [craft.cooking, social.diplomacy, social.trade]
acquisition:
  utility.service.innkeep: 100
  craft.cooking: 50
  social.diplomacy: 50
evolution_paths:
  Lv.10: [Tavern Keeper], [Boarding House Owner], [Restaurateur]
  Lv.20: [Master Innkeeper], [Haven Keeper], [Legendary Host]
  Lv.30: [Grandmaster Innkeeper], [Sanctuary Lord]
special_note: "Erin Solstice's primary class - known for unusual Skill acquisitions"
```

### [Courier] / [Runner]
```yaml
tags_primary: [utility.service.courier, utility.exploration.travel]
tags_secondary: [combat.defense.evasion, utility.stealth]
acquisition:
  utility.service.courier: 100
  utility.exploration.travel: 75
evolution_paths:
  Lv.10: [Street Runner], [City Courier], [Long Distance Runner]
  Lv.20: [Master Courier], [Wind Runner], [Pathfinder]
  Lv.30: [Legendary Courier], [World Runner]
```

### [Scout]
```yaml
tags_primary: [utility.exploration, utility.stealth]
tags_secondary: [combat.ranged, gather.hunting]
acquisition:
  utility.exploration: 100
  utility.stealth: 75
evolution_paths:
  Lv.10: [Pathfinder], [Spy], [Wilderness Scout]
  Lv.20: [Master Scout], [Shadow Walker], [Intelligence Agent]
  Lv.30: [Legendary Scout], [Ghost]
```

### [Clown]
```yaml
tags_primary: [social.performance, social.diplomacy.charm]
tags_secondary: [utility.stealth, combat.defense.evasion]
acquisition:
  social.performance: 150
  social.diplomacy.charm: 100
  # Rare class - requires specific actions/reputation
evolution_paths:
  Lv.20: [Fool], [Jester], [Trickster]
  Lv.30: [Grand Fool], [Chaos Bringer]
special_note: "Rare class - Tom the Clown. Skills often involve misdirection and morale."
```

---

## Leadership Classes

### [Leader]
```yaml
tags_primary: [social.leadership]
tags_secondary: [social.diplomacy, combat.tactical]
acquisition:
  social.leadership: 150
evolution_paths:
  Lv.20: [Commander], [Chieftain], [Captain]
  Lv.30: [Warlord], [High Chief]
```

### [Lord] / [Lady]
```yaml
tags_primary: [social.leadership, social.reputation.faction]
tags_secondary: [social.diplomacy, knowledge.lore]
acquisition:
  social.leadership: 200
  social.reputation.faction: 150
  # Typically granted through inheritance or appointment
evolution_paths:
  Lv.30: [High Lord], [Landed Noble]
  Lv.40: [Duke], [Duchess], [Prince], [Princess]
```

### [King] / [Queen] / [Emperor]
```yaml
tags_primary: [social.leadership.settlement, social.reputation.global]
tags_secondary: [combat.tactical, social.diplomacy]
acquisition:
  # Typically requires ruling a nation
  social.leadership.settlement: 500
  social.reputation.global: 300
evolution_paths:
  Lv.40: [High King], [Empress], [Eternal Monarch]
  Lv.50+: [Legendary Ruler], [God-King]
```

---

## Red Classes (Negative/Stigmatized)

### [Thief]
```yaml
tags_primary: [utility.stealth, social.trade.steal]
tags_secondary: [combat.melee.dagger, utility.stealth.lockpick]
acquisition:
  # Acquired through theft actions
  social.trade.steal: 50  # Special negative tag
evolution_paths:
  Lv.10: [Pickpocket], [Burglar], [Cat Burglar]
  Lv.20: [Master Thief], [Shadow]
consequences:
  - Visible to [Merchant] class detection skills
  - Banned from most guilds
  - NPC suspicion in shops
redemption_path: [Reformed Thief] → [Security Expert]
```

### [Murderer]
```yaml
tags_primary: [combat.melee, social.reputation.criminal]
acquisition:
  # Acquired through killing innocents/non-hostiles
  social.reputation.criminal: 100  # From murder actions
evolution_paths:
  Lv.10: [Killer], [Serial Killer]
  Lv.20: [Mass Murderer], [Butcher]
consequences:
  - Bounty system activation
  - NPC hostility
  - Restricted from settlements
  - Other players can attack without penalty
redemption_path: Long quest chain, cannot fully remove
```

### [Slaver]
```yaml
tags_primary: [social.trade, social.leadership.command]
tags_secondary: [combat.melee, social.diplomacy.intimidate]
acquisition:
  # Acquired through enslaving actions
consequences:
  - Illegal in most regions
  - Faction hostility
  - Player bounties
```

### [Traitor]
```yaml
acquisition:
  # Acquired through betraying sworn oaths/factions
consequences:
  - Permanent reputation damage with betrayed faction
  - Visible to faction members
  - Trust penalties with all NPCs
```

---

*Document Version 1.0 — Classes extracted from The Wandering Inn*
