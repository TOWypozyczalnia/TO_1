Feature: ReturnMovie
// Jako pracownik chcę oznaczyć film jako zwrócony, żeby stan filmów wyświetlanych na stronie był zgodny ze stanem filmów w magazynie.

@good
Scenario: Employee sets status of movie to unavailable correctly
	Given Movie repository contains records
	When I make POST request to /api/Employee/Return with body containing id of movie in json format
    Then The response status code is OK
    And Movie with given id is updated in table

@bad
Scenario: Employee sets status of movie to unavailable incorrectly
	Given Movie repository contains records
	When I make POST request to /api/Employee/Return with empty body
    Then The response status code is BadRequest