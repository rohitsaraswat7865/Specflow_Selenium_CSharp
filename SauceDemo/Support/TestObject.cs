namespace SauceDemo.Support
{
    /// <summary>
    /// Disposable test object
    /// </summary>
    public class TestObject : IDisposable
    {
        private IWebDriver driver;
                
        public TestObject() 
        {
            var options = new ChromeOptions();
            options.PageLoadStrategy = PageLoadStrategy.Normal;
            options.AddArgument("--disable-notifications");
            options.AddArgument("--disable-sync");
            options.AddArgument("--disbale-gpu");
            options.AddArguments("--mute-audio");
            options.AddArguments("--no-sandbox");
            //options.AddArgument("--headless");
            //if (alignmentOptions.Equals(AlignmentOptions.Desktop)) 
            //if (alignmentOptions.Equals(AlignmentOptions.MobileSite)) options.EnableMobileEmulation("iPhone 6");
            options.AddArgument("--start-maximized");
            this.driver = new ChromeDriver(options);
            this.driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(60);
            this.driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(100);
        }

        public IWebDriver Driver => driver;
                        
        public void Dispose()
        {
            this.driver.Close();
            this.driver.Quit();
            this.driver.Dispose();
            
        }
    }
}
