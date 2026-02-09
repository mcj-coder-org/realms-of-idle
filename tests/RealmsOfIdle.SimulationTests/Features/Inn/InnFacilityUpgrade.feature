@Simulation
Feature: Inn Facility Upgrades
  As a player growing my inn
  I want to upgrade facilities to improve efficiency
  So that I can serve more customers and earn more gold

Scenario: Upgrade kitchen improves cooking speed
  Given I have an inn with a level 1 kitchen
  And the kitchen cooking time is 10 seconds
  And I have 200 gold
  When I upgrade the kitchen to level 2
  Then the kitchen level should be 2
  And the kitchen cooking time should be reduced
  And my gold should decrease by the upgrade cost

Scenario: Upgrade bar increases drink variety
  Given I have an inn with a level 1 bar
  And the bar has 2 drink options
  And I have 150 gold
  When I upgrade the bar to level 2
  Then the bar level should be 2
  And the bar should have more drink options
  And my gold should decrease by the upgrade cost

Scenario: Higher reputation increases customer frequency
  Given I have an inn with reputation 5
  And customers arrive every 60 seconds
  When I increase my reputation to 20
  Then customers should arrive more frequently
  And the arrival interval should be less than 60 seconds

Scenario: More customers with upgraded facilities
  Given I have an inn with a level 1 kitchen
  And the inn serves 5 customers per hour
  When I upgrade the kitchen to level 2
  And upgrade the bar to level 2
  Then the inn should serve more than 5 customers per hour
