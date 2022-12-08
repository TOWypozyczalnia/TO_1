Feature: AddReservation
// Jako użytkownik chcę zarezerwować film, żeby móc go później wypożyczyć w placówce wypożyczalni.

@good
Scenario: User adds reservation correctly
	Given I am user
	And Movie repository contains records
	When I make POST request to /api/Reservation/AddReservation with body containing Reservation in json format
    Then The response status code is OK
    And Reservation table contains new record

@bad
Scenario: User adds reservation incorrectly
	Given I am user
	And Movie repository contains records
	When I make POST request to /api/Reservation/AddReservation with body containing Reservation in wrong format
	Then The response status code is BadRequest
