Feature: SauceDemo


@regression
Scenario Outline: Test 1
	Given Login page is loaded
    When Enter username as <USER_NAME> in login page
	And Click submit button in login page
	Examples: 
	| USER_NAME     |
	| standard_user |
	