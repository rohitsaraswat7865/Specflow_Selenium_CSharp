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
        private readonly ResourceManager resourceManager;
        private Dictionary<string, By> locators;
        private Dictionary<string, string> configElements;
        public SauceDemoStepDefinitions(ResourceManager resourceManager, TestObject testObject)
        {
            this.testObject = testObject;
            this.resourceManager = resourceManager;
            var locators = resourceManager.GetLocators(SupportedResourceType.xpath);
            var configElements = resourceManager.GetConfiguration();
        }
        [Given(@"Login page is loaded")]
        public void GivenLoginPageIsLoaded()
        {
            var headerText = locators["login_header"];
        }

        [When(@"Enter username as (.*) in login page")]
        public void WhenEnterUsernameAsStandard_UserInLoginPage(string userName)
        {
            
        }


        [When(@"Click submit button in login page")]
        public void WhenClickSubmitButtonInLoginPage()
        {
            
        }
    }
}
