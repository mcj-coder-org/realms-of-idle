@Simulation
Feature: Guest Overnight Stays
  As a player running an inn
  I want customers to rent rooms for the night
  So that I can earn additional gold from overnight guests

Scenario: Customer rents guest room for the night
  Given I have an inn with available guest rooms
  And a customer wants to stay the night
  And the guest room costs 50 gold
  When the customer pays for the room
  Then the inn gold should increase by 50
  And the customer should be assigned a guest room

Scenario: Customer walks to guest wing and sleeps
  Given a customer has rented a guest room
  And the customer is in the main hall
  When the customer goes to sleep
  Then the customer should walk to the guest wing door
  And enter the guest wing
  And walk to their assigned room
  And start sleeping

Scenario: Customer wakes and returns to main hall
  Given a customer is sleeping in a guest room
  And the customer has slept for 8 hours
  When the customer wakes up
  Then the customer should walk back to the main hall
  And the customer should order breakfast
  And the customer should leave the inn

Scenario: Multiple guests can stay simultaneously
  Given I have an inn with 3 guest rooms
  And I have 3 customers who want to stay the night
  When all 3 customers rent rooms
  Then each customer should be assigned a unique room
  And all 3 customers should sleep simultaneously
  And the inn should earn 150 gold total
