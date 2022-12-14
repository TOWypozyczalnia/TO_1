Feature: AddReview
// Jako użytkownik chcę ocenić film, żeby proponowane mi filmy w przyszłości lepiej spełniały moje oczekiwania

@good
Scenario: User adds review correctly
    Given I am user
    And Movie repository contains records
    When I make POST request to /api/Review/AddReview with body containing Review in json format
    Then The response status code is OK
    And Review table contains new record

@bad
Scenario: User adds review incorrectly
    Given I am user
    And Movie repository contains records
    When I make POST request to /api/Review/AddReview with body containing Review in wrong format
	Then The response status code is BadRequest
