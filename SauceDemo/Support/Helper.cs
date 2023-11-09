using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SauceDemo.Support
{
    public class Helper
    {
        private void FluentRoutine<T,R>(T t, Func<T, R> func,TimeSpan pollingInterval, TimeSpan timeOut)
        {
            var defaultWait = new DefaultWait<T>(t);
            defaultWait.PollingInterval = pollingInterval;
            defaultWait.Timeout = timeOut;
            defaultWait.Message = $" Fluent timeout on type {t.GetType()}";
            defaultWait.Until(func);

        }
        public IWebElement? TryGetElement(IWebDriver driver, By by, Func<IWebElement, bool> func)
        {
            IWebElement element = null;
            this.FluentRoutine<IWebDriver,IWebElement>(
                driver,
                  (driver) =>
                  {
                      return driver.FindElement(by);
                  },
                  TimeSpan.FromSeconds(2),
                  TimeSpan.FromSeconds(6)
                );

            if(element != null)
            {
                this.FluentRoutine<IWebElement, bool>(
                element,
                func,
                TimeSpan.FromSeconds(1),
                TimeSpan.FromSeconds(5)
                );
            }              

            return element;
        }
    }
}
