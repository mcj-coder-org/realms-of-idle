@Simulation
Feature: Inn Layout Expansion
  As a player upgrading my inn
  I want the layout to expand gracefully
  So that I can add more capacity without breaking existing functionality

Scenario: Upgrade kitchen expands kitchen area
  Given I have an inn with a level 1 kitchen
  And I have 100 gold
  When I upgrade the kitchen to level 2
  Then the kitchen zone should expand
  And travel times should be recalculated
  And all paths should remain connected

Scenario: Add guest room increases capacity
  Given I have an inn with 2 guest rooms
  And I have 150 gold
  When I add a new guest room
  Then the inn should have 3 guest rooms
  And the new room should be connected to the guest wing
  And all paths should remain connected

Scenario: Add staff bed allows hiring more staff
  Given I have an inn with 2 staff beds
  And I have 200 gold
  When I add a new staff bed
  Then the inn should have 3 staff beds
  And the new bed should be in the staff quarters
  And all paths should remain connected
