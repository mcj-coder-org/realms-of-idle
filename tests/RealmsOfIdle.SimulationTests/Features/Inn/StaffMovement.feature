@Simulation
Feature: Staff Movement
  As a player managing staff
  I want staff to move efficiently between facilities
  So that customers are served quickly

Scenario: Waitress serves table after pickup from kitchen
  Given I have an inn with a kitchen and tables
  And I have a waitress named Barbara
  And Barbara is at the bar
  And table 2 has a customer waiting for food
  When Barbara is assigned to serve table 2
  Then Barbara should walk to the kitchen
  And pick up the food
  And walk to table 2
  And deliver the food

Scenario: Waitress collects orders from tables
  Given I have an inn with a kitchen and tables
  And I have a waitress named Barbara
  And Barbara is at the bar
  And table 3 has a new customer
  When Barbara is assigned to seat the customer
  Then Barbara should walk to table 3
  And take the customer's order
  And walk to the kitchen
  And submit the order

Scenario: Customer satisfaction includes travel time
  Given I have an inn with a kitchen and tables
  And I have a waitress named Barbara
  And table 1 is far from the kitchen
  When a customer at table 1 orders food
  And Barbara serves the customer
  Then the customer satisfaction should be affected by the travel time
  And longer travel times should decrease satisfaction
