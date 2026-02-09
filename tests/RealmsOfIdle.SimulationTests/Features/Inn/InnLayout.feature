@Simulation
Feature: Inn Layout Generation
  As a player starting a new game
  I want the inn to be procedurally generated with all required facilities
  So that I can start playing immediately with a functional inn

Scenario: Generate new inn layout with seed produces consistent layout
  Given I create a new game with seed 42
  When the inn layout is generated
  Then the inn should have a kitchen
  And the inn should have a bar
  And the inn should have 3 tables
  And the inn should have a fireplace
  And the inn should have guest rooms
  And the inn should have an entrance
  And all areas should be path-connected
  When I create another game with the same seed 42
  Then the layout should be identical to the first layout

Scenario: Kitchen placement follows constraints
  Given I create a new game with seed 123
  When the inn layout is generated
  Then the kitchen should be in a corner or on the back wall
  And the kitchen should be connected to the main hall

Scenario: Tables are spaced appropriately
  Given I create a new game with seed 456
  When the inn layout is generated
  Then tables should be placed in the open floor area
  And no two tables should overlap

Scenario: Fireplace is on a wall
  Given I create a new game with seed 789
  When the inn layout is generated
  Then the fireplace should be placed on a wall
  And the fireplace should not block any doorways

Scenario: Staff quarters have designated beds
  Given I create a new game with seed 101
  When the inn layout is generated
  Then the inn should have staff quarters
  And the staff quarters should be connected to the main hall
  And each staff member should have a designated bed

Scenario: Guest wing has individual rooms
  Given I create a new game with seed 202
  When the inn layout is generated
  Then the inn should have a guest wing
  And the guest wing should be connected to the main hall
  And the guest wing should have individual rooms with beds
