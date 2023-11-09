using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Interactions;
using TechTalk.SpecFlow;
using SauceDemo.Support;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SauceDemo.StepDefinitions
{
    [Binding]
    public sealed class Hooks
    {
        readonly TestObject testObject;
        readonly ResourceManager resourceManager;
        public Hooks(ResourceManager resourceManager, TestObject testObject)
        {
            this.testObject = testObject;
            this.resourceManager = resourceManager;
        }       

        [BeforeScenario]
        public void FirstBeforeScenario()
        {
            try
            {
                var locators = resourceManager.GetLocators(SupportedResourceType.xpath);
                var configElements = resourceManager.GetConfiguration();
                var url = configElements["SauceDemo_Site"];
                this.testObject.Driver.Navigate().GoToUrl(url);
            }
            catch(Exception ex)
            {
                Assert.Fail(ex.Message);
            }
            
        }

        [AfterScenario]
        public void AfterScenario()
        {
            //this.testObject?.Dispose();
        }
    }
}