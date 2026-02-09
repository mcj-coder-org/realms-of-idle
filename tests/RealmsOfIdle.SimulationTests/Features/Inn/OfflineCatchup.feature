@Simulation
Feature: Offline Catch-Up
  As a player returning to the game after being away
  I want the game to calculate what happened while I was gone
  So that I can continue progressing even when offline

Scenario: One hour offline produces expected progress
  Given I have an inn earning 10 gold per hour
  And I have 100 gold
  When I am offline for 1 hour
  And I return to the game
  Then I should have approximately 110 gold
  And the game should have processed 1 hour of ticks

Scenario: Extended offline period scales appropriately
  Given I have an inn earning 50 gold per hour
  And I have 200 gold
  When I am offline for 8 hours
  And I return to the game
  Then I should have approximately 600 gold
  And the game should have processed 8 hours of ticks

Scenario: Offline catch-up applies to reputation
  Given I have an inn with reputation 10
  And customers increase reputation when served
  When I am offline for 2 hours
  And I return to the game
  Then my reputation should have increased
  And the increase should match the expected customer count

Scenario: Diminishing returns apply to long offline periods
  Given I have an inn earning gold
  And I have 500 gold
  When I am offline for 24 hours
  And I return to the game
  Then the gold earned should be calculated with diminishing returns
  And returns should decrease for longer offline periods
