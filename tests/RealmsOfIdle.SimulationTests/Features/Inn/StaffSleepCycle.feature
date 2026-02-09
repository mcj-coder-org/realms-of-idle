@Simulation
Feature: Staff Sleep Cycle
  As a player managing staff
  I want staff to sleep when fatigued
  So that they can recover and continue working efficiently

Scenario: Staff member becomes fatigued over time
  Given I have a staff member named Tom
  And Tom's fatigue is 0%
  When Tom works for 4 hours
  Then Tom's fatigue should increase
  And Tom should be in a fatigued state

Scenario: Fatigued staff walks to staff quarters to sleep
  Given I have a staff member named Barbara
  And Barbara is in the main hall
  And Barbara's fatigue is 80%
  When Barbara becomes too fatigued to work
  Then Barbara should walk to the staff quarters door
  And enter the staff quarters
  And walk to her designated bed
  And start sleeping

Scenario: Staff recovers fatigue while sleeping
  Given I have a staff member named Tom
  And Tom is sleeping in his designated bed
  And Tom's fatigue is 90%
  When Tom sleeps for 2 hours
  Then Tom's fatigue should decrease significantly
  And Tom should be in a rested state

Scenario: Rested staff returns to work
  Given I have a staff member named Barbara
  And Barbara is sleeping in the staff quarters
  And Barbara's fatigue is 10%
  When Barbara wakes up
  Then Barbara should walk back to the main hall
  And resume her assigned duties
  And Barbara should be at her normal work station

Scenario: Staff sleep cycle repeats automatically
  Given I have a staff member named Tom
  And Tom is working in the kitchen
  When Tom accumulates enough fatigue
  Then Tom should automatically go to sleep
  And automatically return to work when rested
  And the cycle should repeat without player intervention
