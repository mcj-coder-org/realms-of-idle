@Simulation
Feature: Door Transitions Between Areas
  As an NPC moving through the inn
  I want to smoothly transition between connected areas
  So that I can reach destinations in any part of the inn

Scenario: NPC walks to door and enters connected area
  Given I have an inn with a main hall and staff quarters
  And the areas are connected by a door
  And a staff member is in the main hall
  When the staff member needs to go to the staff quarters
  Then the staff member should walk to the door tile
  And transition to the staff quarters
  And continue to their destination

Scenario: Door transition works in both directions
  Given I have an inn with a main hall and guest wing
  And the areas are connected by a door
  And a customer is in the guest wing
  When the customer needs to go to the main hall
  Then the customer should walk to the door tile
  And transition to the main hall
  And continue to their destination

# Camera tracking is a UI concern; this scenario belongs in UI/integration tests, not simulation tests
@Ignore
Scenario: Camera follows focused NPC through doors
  Given I have an inn with multiple connected areas
  And I have focused on a staff member
  When the staff member transitions through a door
  Then the camera should pan to the new area
  And the staff member should remain visible
  And the camera should track the staff member smoothly

Scenario: NPCs maintain state during door transitions
  Given I have an inn with connected areas
  And a customer is carrying food
  When the customer transitions through a door
  Then the customer should still be carrying the food
  And the customer's target should remain the same
  And the customer's progress should continue
