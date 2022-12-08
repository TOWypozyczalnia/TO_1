Feature: GetDetails
// Jako użytkownik chcę wyswitlic szczegoly filmu

Scenario: User gets movie correctly
	Given I am user
	And Movie repository contains records
	When I make GET request to /api/Movie/GetSingle
	Then The response status code is OK
