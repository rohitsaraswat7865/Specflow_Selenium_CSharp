using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using SauceDemo.Support;
using System;
using TechTalk.SpecFlow;

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
                Assert.IsTrue(helper.CheckIfPageIsLoaded(testObject, "saucedemo"));
                var headerLocator = locators["login_header"];
                var headerTextElement = helper.TryGetElement(testObject, headerLocator, element => element.Displayed && element.Enabled);
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
                var userNameValue = configElements[userName];
                var userNameLocator = locators["login_username"];
                var userNameElement = helper.TryGetElement(testObject, userNameLocator, element => element.Enabled && element.Displayed);
                Assert.IsNotNull(userNameElement);
                userNameElement.SendKeys(userNameValue);
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
                var passwordValue = configElements["Password"];
                var passwordLocator = locators["login_password"];
                var passwordElement = helper.TryGetElement(testObject, passwordLocator, element => element.Enabled && element.Displayed);
                Assert.IsNotNull(passwordElement);
                passwordElement.SendKeys(passwordValue);
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
                Assert.IsTrue(helper.CheckIfPageIsLoaded(testObject, "/inventory"));
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

    }
}
