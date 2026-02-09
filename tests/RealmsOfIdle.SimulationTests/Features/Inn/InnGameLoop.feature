@Simulation
Feature: Inn Game Loop
  As a player running an inn
  I want customers to arrive, eat, and pay
  So that I can earn gold and grow my business

Scenario: Customer arrives and is seated
  Given I have an inn with an entrance and tables
  And the inn reputation is 10
  When a customer arrives
  Then the customer should appear at the entrance
  And the customer should walk to an available table
  And the customer should be in a waiting state

Scenario: Customer orders and is served
  Given a customer is waiting at a table
  And I have a waitress assigned to serve
  When the waitress takes the order
  Then the waitress should walk to the kitchen
  And the kitchen should start preparing the order
  When the food is ready
  Then the waitress should walk to the table
  And deliver the food
  And the customer should start eating

Scenario: Customer eats and pays
  Given a customer is eating at a table
  When the customer finishes eating
  Then the customer should pay gold
  And the customer satisfaction should affect reputation
  And the customer should walk to the entrance
  And leave the inn

Scenario: Full customer lifecycle
  Given I have an inn with all facilities
  And the inn has 50 gold
  When a customer arrives
  And the customer is seated
  And the customer orders food
  And the customer is served
  And the customer eats
  And the customer pays
  Then the inn gold should have increased
  And the customer should have left
