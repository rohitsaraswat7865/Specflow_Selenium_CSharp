Feature: SauceDemo


@regression
Scenario Outline: Test 1 - Confirm the user is taken to the products page
	Given Login page is loaded
	When Enter username as <USER_NAME> in login page
	And Enter password in login page
	And Click submit button in login page
	Then Product page is loaded
    Examples:    
	| USER_NAME     |
	| Standard_User |


@regression
Scenario Outline: Test 2 - Add 3 items to basket and buy 2 items
    Given Login page is loaded
	When Enter username as <USER_NAME> in login page
	And Enter password in login page
	And Click submit button in login page
	Then Product page is loaded

	 Examples:    
	| USER_NAME     |
	| Standard_User |

	