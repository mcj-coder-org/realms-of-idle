@Simulation
Feature: Staff Auto-Behavior
  As a player
  I want staff to automatically perform their duties
  So that I don't have to micromanage every action

Scenario: Cook automatically cooks when kitchen has orders
  Given I have a staff member named Tom who is a cook
  And Tom is at the kitchen
  And there are pending food orders
  When the player is idle
  Then Tom should automatically start cooking
  And Tom should process the orders in the queue
  And Tom should remain at the kitchen

Scenario: Waitress automatically seats arriving customers
  Given I have a staff member named Barbara who is a waitress
  And Barbara is at the bar
  And a new customer arrives at the entrance
  When the player is idle
  Then Barbara should automatically walk to the customer
  And guide the customer to an available table
  And return to the bar

Scenario: Waitress automatically serves food when ready
  Given I have a staff member named Barbara who is a waitress
  And there is food ready in the kitchen
  And customers are waiting at tables
  When the player is idle
  Then Barbara should automatically walk to the kitchen
  And pick up the food
  And walk to each waiting customer's table
  And deliver the food

Scenario: Staff automatically sleeps when fatigued
  Given I have a staff member named Tom
  And Tom's fatigue exceeds the threshold
  When the player is idle
  Then Tom should automatically stop working
  And walk to the staff quarters
  And go to his designated bed
  And sleep until rested

Scenario: Player can override auto-behavior with direct commands
  Given I have a staff member named Barbara
  And Barbara is automatically serving customers
  When I directly command Barbara to clean
  Then Barbara should stop serving
  And perform the clean action
  And resume auto-behavior after completing the command
