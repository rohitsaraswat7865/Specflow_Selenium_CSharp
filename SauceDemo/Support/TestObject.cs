using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities.Resources;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using SauceDemo.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SauceDemo.Support
{
    /// <summary>
    /// Disposable test object
    /// </summary>
    public class TestObject :IDisposable
    {
        private IWebDriver driver;
        private string path;

        public TestObject()
        {
            var options = new FirefoxOptions();
            //options.AddArgument("--kiosk");
            driver ??= new FirefoxDriver(options);
            path ??= ResourceManager.path;
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(60);
            driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(100);
        }

        public IWebDriver Driver => driver;
        public string Path => path;

        public void Dispose()
        {
            this.driver.Close();
            this.driver.Quit();
            this.driver.Dispose();
        }
    }
}
