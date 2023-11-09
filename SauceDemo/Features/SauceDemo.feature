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
	When Select following <PRODUCTS> in product page
	And Click on cart icon in product page
	Then Cart page is loaded
	When Remove a <PRODUCT> from cart
	And Click on checkout button in cart page
	Then Checkout info page is loaded
	When Provide user information <FIRST_NAME> and <LAST_NAME> and <POSTAL_CODE>
	And Click on continue button in checkout info page
	Then Payment page is loaded
	When Click on finish button in payment page
	Then Checkout complete page is loaded

	Examples:    
	| USER_NAME     | PRODUCTS                         | PRODUCT       | FIRST_NAME | LAST_NAME | POSTAL_CODE |
	| Standard_User | Backpack,Bolt T-Shirt,Bike Light | Bolt T-Shirt  | rohit      | saraswat  | 282007      |

	