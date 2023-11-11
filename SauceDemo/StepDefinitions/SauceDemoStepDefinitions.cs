using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using SauceDemo.Support;
using System;
using System.Runtime.CompilerServices;
using TechTalk.SpecFlow;
using static SauceDemo.Support.TestObject;

namespace SauceDemo.StepDefinitions
{
    [Binding]
    public class SauceDemoStepDefinitions
    {
        private readonly TestObject testObject;
        private readonly Helper helper;
        private Dictionary<string, By> locators;
        private Dictionary<string, string> configElements;
        
        public SauceDemoStepDefinitions(ResourceManager resourceManager, TestObject testObject, Helper helper)
        {
            this.testObject = testObject;
            this.locators = resourceManager.GetLocators(SupportedResourceType.xpath);
            this.configElements = resourceManager.GetConfiguration();
            this.helper = helper;
        }
        [Given(@"Login page is loaded")]
        public void GivenLoginPageIsLoaded()
        {
            try
            {
                helper.CheckIfPageIsLoaded(testObject, "saucedemo");
                var headerTextElement = helper.TryGetElement(testObject, locators["login_header"], element => element.Displayed && element.Enabled);
                Assert.IsNotNull(headerTextElement);
                Assert.IsTrue(headerTextElement.Text.Equals("Swag Labs"));
            }
            catch(Exception ex)
            {
                Assert.Fail(ex.Message);
            }         

        }

        [When(@"Enter username as (.*) in login page")]
        public void WhenEnterUsernameAsStandard_UserInLoginPage(string userName)
        {
            try
            {
                var userNameElement = helper.TryGetElement(testObject, locators["login_username"], element => element.Enabled && element.Displayed);
                Assert.IsNotNull(userNameElement);
                userNameElement.SendKeys(configElements[userName]);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [When(@"Enter password in login page")]
        public void WhenEnterPasswordInLoginPage()
        {
            try
            {
                var passwordElement = helper.TryGetElement(testObject, locators["login_password"], element => element.Enabled && element.Displayed);
                Assert.IsNotNull(passwordElement);
                passwordElement.SendKeys(configElements["Password"]);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [When(@"Click submit button in login page")]
        public void WhenClickSubmitButtonInLoginPage()
        {
            try
            {
                var loginButtonLocator = locators["login_button"];
                var loginButtonElement = helper.TryGetElement(testObject, loginButtonLocator, element => element.GetAttribute("value").Equals("Login"));
                Assert.IsNotNull(loginButtonElement);
                loginButtonElement.Click();
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [Then(@"Product page is loaded")]
        public void ThenProductPageIsLoaded()
        {
            try
            {
                helper.CheckIfPageIsLoaded(testObject, "/inventory");
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [When(@"Select following (.*) in product page")]
        public void WhenSelectFollowingProductsInProductPage(string products)
        {
            try
            {
                var listOfRequiredProducts = products.Split(',').ToList();
                var listOfAvailableProducts = helper.TryGetElements(testObject, locators["product_list"], element => element.Displayed).ToList();
                List<IWebElement> listOfNeededProducts = new List<IWebElement>();
                listOfAvailableProducts.ForEach(product =>
                {
                    listOfRequiredProducts.ForEach(requiredProduct =>
                    {
                        var txt = product.Text;
                        if (txt.Contains(requiredProduct))
                        {
                            listOfNeededProducts.Add(product);
                        }
                    });
                });
                foreach(var product in listOfNeededProducts)
                {
                    var addToCartButton = helper.TryGetSubElement(product, locators["product_addToCart"], element => element.Displayed && element.Enabled);
                    Assert.IsNotNull(addToCartButton);
                    addToCartButton.Click();
                }
            }
            catch(Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [Then(@"Following (.*) are added to cart")]
        public void ThenFollowingProductsAreAddedToCart(string products)
        {
           
        }

        [When(@"Click on cart icon in product page")]
        public void WhenClickOnCartIconInProductPage()
        {
            try
            {
                var cartIconElement = helper.TryGetElement(testObject, locators["product_cartIcon"], element => element.Enabled && element.Displayed);
                Assert.IsNotNull(cartIconElement);
                cartIconElement.Click();
            }
            catch(Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [Then(@"Cart page is loaded")]
        public void ThenCartPageIsLoaded()
        {
            try
            {
                helper.CheckIfPageIsLoaded(testObject, "/cart");
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }
        [When(@"Remove a (.*) from cart")]
        public void WhenRemoveAFleeceJacketFromTheCart(string productToRemove)
        {
            try
            {
                var listOfProductsInCart = helper.TryGetElements(testObject, locators["cart_items"], element => element.Displayed && element.Text != null);
                var removeButtons = helper.TryGetElements(testObject, locators["cart_remove"], element => element.Enabled && element.Text.Equals("Remove"));
                var buttonIterator = removeButtons.GetEnumerator();
                foreach (var product in listOfProductsInCart)
                {
                    buttonIterator.MoveNext();
                    if (product.Text.Contains(productToRemove))
                    {
                        var actions = new Actions(testObject.Driver);
                        actions.MoveToElement(buttonIterator.Current);
                        actions.Click();
                        actions.Build().Perform();
                    }                    
                }
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }


        [When(@"Click on checkout button in cart page")]
        public void WhenClickOnCheckoutButtonInCartPage()
        {
            try
            {
                var cartCheckoutElement = helper.TryGetElement(testObject, locators["cart_checkout"], element => element.Enabled && element.Displayed);
                Assert.IsNotNull(cartCheckoutElement);
                cartCheckoutElement.Click();
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [Then(@"Checkout info page is loaded")]
        public void ThenCheckoutInfoPageIsLoaded()
        {
            try
            {
                helper.CheckIfPageIsLoaded(testObject, "/checkout-step-one");
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [When(@"Provide user information ([a-z]+) and ([a-z]+) and ([0-9]+)")]
        public void WhenProvideUserInformationRohitSaraswat(string firstName, string lastName, string postalCode)
        {
            try
            {
                var firstNameElement = helper.TryGetElement(testObject, locators["checkout_firstname"], element => element.Enabled && element.Displayed);
                Assert.IsNotNull(firstNameElement);
                firstNameElement.SendKeys(firstName);
                var lastNameElement = helper.TryGetElement(testObject, locators["checkout_lastname"], element => element.Enabled && element.Displayed);
                Assert.IsNotNull(lastNameElement);
                lastNameElement.SendKeys(lastName);
                var postalCodeElement = helper.TryGetElement(testObject, locators["checkout_postalcode"], element => element.Enabled && element.Displayed);
                Assert.IsNotNull(postalCodeElement);
                postalCodeElement.SendKeys(postalCode);
            }
            catch(Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [When(@"Click on continue button in checkout info page")]
        public void WhenClickOnContinueButtonInCheckoutInfoPage()
        {
            try
            {
                var continueButtonElement = helper.TryGetElement(testObject, locators["checkout_continue"], element => element.Enabled && element.Displayed);
                Assert.IsNotNull(continueButtonElement);
                continueButtonElement.Click();
                               
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [Then(@"Payment page is loaded")]
        public void ThenPaymentPageIsLoaded()
        {
            try
            {
                helper.CheckIfPageIsLoaded(testObject, "/checkout-step-two");
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [When(@"Click on finish button in payment page")]
        public void WhenClickOnFinishButtonInPaymentPage()
        {

            try
            {
                var finishButtonElement = helper.TryGetElement(testObject, locators["payment_finish"], element => element.Enabled && element.Displayed);
                Assert.IsNotNull(finishButtonElement);
                finishButtonElement.Click();

            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [Then(@"Checkout complete page is loaded")]
        public void ThenCheckoutCompletePageIsLoaded()
        {
            try
            {
                helper.CheckIfPageIsLoaded(testObject, "/checkout-complete");
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }


    }
}
