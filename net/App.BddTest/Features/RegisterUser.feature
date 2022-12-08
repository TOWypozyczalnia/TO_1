Feature: RegisterUser
// Jako nowy użytkownik chcę stworzyć swoje konto, żeby móc korzystać z wypożyczalni.

@good
Scenario: User registers correctly
	When I make POST request to /api/User/Register with body containing username string in json format
    Then The response status code is OK
	And The response contains user key
    And LoggedUser table contains new record

#@bad
#Scenario: User registers incorrectly
#	Given I am user
#	And Movie repository contains records
#	When I make POST request to /api/Reservation/AddReservation with body containing Reservation in wrong format
#	Then The response status code is BadRequest
